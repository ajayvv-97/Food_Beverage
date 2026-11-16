/*
  Add-on Group master/detail schema and save/update procedure.
  Product foreign key uses dbo.tblProduct(ProductId).
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

IF OBJECT_ID(N'dbo.tblProduct', N'U') IS NULL
    THROW 51100, 'dbo.tblProduct is required before creating add-on groups.', 1;
GO

IF OBJECT_ID(N'dbo.tblAddonGroup', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblAddonGroup
    (
        GroupId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tblAddonGroup PRIMARY KEY,
        GroupName NVARCHAR(120) NOT NULL,
        Description NVARCHAR(500) NULL,
        SelectionMode TINYINT NOT NULL CONSTRAINT DF_tblAddonGroup_SelectionMode DEFAULT (1),
        MinSelection INT NOT NULL CONSTRAINT DF_tblAddonGroup_MinSelection DEFAULT (0),
        MaxSelection INT NOT NULL CONSTRAINT DF_tblAddonGroup_MaxSelection DEFAULT (1),
        IsRequired BIT NOT NULL CONSTRAINT DF_tblAddonGroup_IsRequired DEFAULT (0),
        BranchId INT NOT NULL,
        Status BIT NOT NULL CONSTRAINT DF_tblAddonGroup_Status DEFAULT (1),
        CreatedBy BIGINT NULL,
        CreatedDateTime DATETIME2 NOT NULL CONSTRAINT DF_tblAddonGroup_CreatedDateTime DEFAULT (SYSDATETIME()),
        ModifiedBy BIGINT NULL,
        ModifiedDateTime DATETIME2 NULL,
        CONSTRAINT CK_tblAddonGroup_SelectionMode CHECK (SelectionMode IN (1,2)),
        CONSTRAINT CK_tblAddonGroup_SelectionRange CHECK
            (MinSelection >= 0 AND MaxSelection >= 1 AND MinSelection <= MaxSelection),
        CONSTRAINT CK_tblAddonGroup_SingleMax CHECK (SelectionMode <> 1 OR MaxSelection = 1),
        CONSTRAINT CK_tblAddonGroup_RequiredMin CHECK (IsRequired = 0 OR MinSelection >= 1)
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tblAddonGroup') AND name=N'UX_tblAddonGroup_Branch_GroupName')
    CREATE UNIQUE INDEX UX_tblAddonGroup_Branch_GroupName
        ON dbo.tblAddonGroup(BranchId, GroupName) WHERE Status = 1;
GO

IF OBJECT_ID(N'dbo.tblAddonGroupDetail', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblAddonGroupDetail
    (
        GroupDetailId BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tblAddonGroupDetail PRIMARY KEY,
        GroupId INT NOT NULL,
        ProductId INT NOT NULL,
        DisplayOrder INT NOT NULL CONSTRAINT DF_tblAddonGroupDetail_DisplayOrder DEFAULT (0),
        Status BIT NOT NULL CONSTRAINT DF_tblAddonGroupDetail_Status DEFAULT (1),
        CreatedBy BIGINT NULL,
        CreatedDateTime DATETIME2 NOT NULL CONSTRAINT DF_tblAddonGroupDetail_CreatedDateTime DEFAULT (SYSDATETIME()),
        CONSTRAINT FK_tblAddonGroupDetail_Group FOREIGN KEY (GroupId) REFERENCES dbo.tblAddonGroup(GroupId),
        CONSTRAINT FK_tblAddonGroupDetail_Product FOREIGN KEY (ProductId) REFERENCES dbo.tblProduct(ProductId)
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tblAddonGroupDetail') AND name=N'UX_tblAddonGroupDetail_Group_Product')
    CREATE UNIQUE INDEX UX_tblAddonGroupDetail_Group_Product
        ON dbo.tblAddonGroupDetail(GroupId, ProductId);
GO

IF OBJECT_ID(N'dbo.tblProductAddonGroup', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblProductAddonGroup
    (
        ProductAddonGroupId BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tblProductAddonGroup PRIMARY KEY,
        ProductId INT NOT NULL,
        GroupId INT NOT NULL,
        BranchId INT NOT NULL,
        DisplayOrder INT NOT NULL CONSTRAINT DF_tblProductAddonGroup_DisplayOrder DEFAULT (0),
        Status BIT NOT NULL CONSTRAINT DF_tblProductAddonGroup_Status DEFAULT (1),
        CreatedBy BIGINT NULL,
        CreatedDateTime DATETIME2 NOT NULL CONSTRAINT DF_tblProductAddonGroup_CreatedDateTime DEFAULT (SYSDATETIME()),
        CONSTRAINT FK_tblProductAddonGroup_Product FOREIGN KEY (ProductId) REFERENCES dbo.tblProduct(ProductId),
        CONSTRAINT FK_tblProductAddonGroup_Group FOREIGN KEY (GroupId) REFERENCES dbo.tblAddonGroup(GroupId)
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tblProductAddonGroup') AND name=N'UX_tblProductAddonGroup_Product_Group_Branch')
    CREATE UNIQUE INDEX UX_tblProductAddonGroup_Product_Group_Branch
        ON dbo.tblProductAddonGroup(ProductId, GroupId, BranchId);
GO

CREATE OR ALTER PROCEDURE dbo.prdInsert_upd_AddonGroup
    @GroupId INT,
    @GroupName NVARCHAR(120),
    @Description NVARCHAR(500) = NULL,
    @SelectionMode TINYINT,
    @MinSelection INT,
    @MaxSelection INT,
    @IsRequired BIT,
    @Status BIT,
    @BranchId INT,
    @CreatedBy BIGINT,
    @ModifiedBy BIGINT,
    @ProductsXml XML
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF NULLIF(LTRIM(RTRIM(@GroupName)), '') IS NULL
        THROW 51101, 'Group name is required.', 1;

    IF @SelectionMode NOT IN (1,2)
        THROW 51102, 'Selection mode must be Single or Multiple.', 1;

    IF @MinSelection < 0 OR @MaxSelection < 1 OR @MinSelection > @MaxSelection
        THROW 51103, 'Invalid minimum or maximum selection.', 1;

    IF @SelectionMode = 1 AND @MaxSelection <> 1
        THROW 51104, 'Single selection groups must have maximum selection 1.', 1;

    IF @IsRequired = 1 AND @MinSelection < 1
        THROW 51105, 'Required groups must have minimum selection at least 1.', 1;

    IF @ProductsXml IS NULL OR @ProductsXml.exist('/Products/Product[1]') = 0
        THROW 51106, 'Select at least one product for this group.', 1;

    IF EXISTS
    (
        SELECT ProductId
        FROM
        (
            SELECT N.X.value('@ProductId','INT') AS ProductId
            FROM @ProductsXml.nodes('/Products/Product') AS N(X)
        ) AS Selected
        GROUP BY ProductId
        HAVING COUNT(1) > 1
    )
        THROW 51107, 'Duplicate products are not allowed in a group.', 1;

    DECLARE @SelectedProducts TABLE
    (
        ProductId INT NOT NULL PRIMARY KEY,
        DisplayOrder INT NOT NULL
    );

    INSERT INTO @SelectedProducts (ProductId, DisplayOrder)
    SELECT N.X.value('@ProductId','INT'),
           N.X.value('@DisplayOrder','INT')
    FROM @ProductsXml.nodes('/Products/Product') AS N(X);


    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @SavedGroupId INT;

        IF ISNULL(@GroupId, 0) > 0
        BEGIN
            UPDATE dbo.tblAddonGroup
            SET GroupName = @GroupName,
                Description = @Description,
                SelectionMode = @SelectionMode,
                MinSelection = @MinSelection,
                MaxSelection = @MaxSelection,
                IsRequired = @IsRequired,
                Status = @Status,
                ModifiedBy = @ModifiedBy,
                ModifiedDateTime = SYSDATETIME()
            WHERE GroupId = @GroupId
              AND BranchId = @BranchId;

            IF @@ROWCOUNT = 0
                THROW 51108, 'Add-on group not found for this branch.', 1;

            SET @SavedGroupId = @GroupId;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.tblAddonGroup
                (GroupName, Description, SelectionMode, MinSelection, MaxSelection,
                 IsRequired, Status, BranchId, CreatedBy, CreatedDateTime)
            VALUES
                (@GroupName, @Description, @SelectionMode, @MinSelection, @MaxSelection,
                 @IsRequired, @Status, @BranchId, @CreatedBy, SYSDATETIME());

            SET @SavedGroupId = CONVERT(INT, SCOPE_IDENTITY());
        END;

        -- Synchronize the selected products: remove deselected rows and insert
        -- new rows while preserving the selected order.
        DELETE D
        FROM dbo.tblAddonGroupDetail D
        WHERE D.GroupId = @SavedGroupId
          AND NOT EXISTS
          (
              SELECT 1
              FROM @SelectedProducts S
              WHERE S.ProductId = D.ProductId
          );

        UPDATE D
        SET D.DisplayOrder = S.DisplayOrder,
            D.Status = 1
        FROM dbo.tblAddonGroupDetail D
        INNER JOIN @SelectedProducts S ON S.ProductId = D.ProductId
        WHERE D.GroupId = @SavedGroupId;

        INSERT INTO dbo.tblAddonGroupDetail
            (GroupId, ProductId, DisplayOrder, Status, CreatedBy, CreatedDateTime)
        SELECT @SavedGroupId, S.ProductId, S.DisplayOrder,
               1, @CreatedBy, SYSDATETIME()
        FROM @SelectedProducts S
        WHERE NOT EXISTS
        (
            SELECT 1 FROM dbo.tblAddonGroupDetail D
            WHERE D.GroupId = @SavedGroupId
              AND D.ProductId = S.ProductId
        );

        COMMIT TRANSACTION;

        SELECT @SavedGroupId AS GroupId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

PRINT 'Add-on group schema and save/update procedure created/verified.';
GO
