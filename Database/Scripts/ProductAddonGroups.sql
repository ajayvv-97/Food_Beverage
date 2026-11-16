/*
    Add-on Group tables and three separate insert/update procedures.
    Run this script in the FoodBeverage database after dbo.tblProduct exists.
*/
SET NOCOUNT ON;
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
        ModifiedBy BIGINT NULL,
        ModifiedDateTime DATETIME2 NULL,
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
        ModifiedBy BIGINT NULL,
        ModifiedDateTime DATETIME2 NULL,
        CONSTRAINT FK_tblProductAddonGroup_Product FOREIGN KEY (ProductId) REFERENCES dbo.tblProduct(ProductId),
        CONSTRAINT FK_tblProductAddonGroup_Group FOREIGN KEY (GroupId) REFERENCES dbo.tblAddonGroup(GroupId)
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tblProductAddonGroup') AND name=N'UX_tblProductAddonGroup_Product_Group_Branch')
    CREATE UNIQUE INDEX UX_tblProductAddonGroup_Product_Group_Branch
        ON dbo.tblProductAddonGroup(ProductId, GroupId, BranchId);
GO

/* 1. Master: dbo.tblAddonGroup */
IF OBJECT_ID(N'dbo.prdInsertAddonGroup', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.prdInsertAddonGroup AS BEGIN SET NOCOUNT ON; END');
GO
ALTER PROCEDURE dbo.prdInsertAddonGroup
    @GroupId INT,
    @GroupName NVARCHAR(120),
    @Description NVARCHAR(500),
    @SelectionMode TINYINT,
    @MinSelection INT,
    @MaxSelection INT,
    @IsRequired BIT,
    @Status BIT,
    @BranchId INT,
    @CreatedBy BIGINT,
    @ModifiedBy BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Id INT;

    IF NULLIF(LTRIM(RTRIM(@GroupName)), '') IS NULL
        THROW 51101, 'Group name is required.', 1;

    IF @SelectionMode NOT IN (1,2)
       OR @MinSelection < 0
       OR @MaxSelection < 1
       OR @MinSelection > @MaxSelection
       OR (@SelectionMode = 1 AND @MaxSelection <> 1)
       OR (@IsRequired = 1 AND @MinSelection < 1)
        THROW 51102, 'Invalid add-on group selection settings.', 1;

    IF @GroupId > 0
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
        WHERE GroupId = @GroupId AND BranchId = @BranchId;

        IF @@ROWCOUNT = 0
            THROW 51103, 'Add-on group not found for this branch.', 1;

        SET @Id = @GroupId;
    END
    ELSE
    BEGIN
        INSERT INTO dbo.tblAddonGroup
        (GroupName, Description, SelectionMode, MinSelection, MaxSelection,
         IsRequired, Status, BranchId, CreatedBy, CreatedDateTime)
        VALUES
        (@GroupName, @Description, @SelectionMode, @MinSelection, @MaxSelection,
         @IsRequired, @Status, @BranchId, @CreatedBy, SYSDATETIME());

        SET @Id = CONVERT(INT, SCOPE_IDENTITY());
    END

    SELECT @Id AS GroupId;
END;
GO

/* 2. Group products: dbo.tblAddonGroupDetail.
   Set @ResetGroup=1 once before saving the selected product rows.
   Then call again for every selected product with @ResetGroup=0. */
IF OBJECT_ID(N'dbo.prdInsertAddonGroupDetail', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.prdInsertAddonGroupDetail AS BEGIN SET NOCOUNT ON; END');
GO
ALTER PROCEDURE dbo.prdInsertAddonGroupDetail
    @GroupDetailId BIGINT,
    @GroupId INT,
    @ProductId INT,
    @DisplayOrder INT,
    @Status BIT,
    @CreatedBy BIGINT,
    @ResetGroup BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Id BIGINT;

    IF @ResetGroup = 1
    BEGIN
        UPDATE dbo.tblAddonGroupDetail
        SET Status = 0,
            ModifiedBy = @CreatedBy,
            ModifiedDateTime = SYSDATETIME()
        WHERE GroupId = @GroupId;

        SELECT CAST(0 AS BIGINT) AS GroupDetailId;
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.tblAddonGroup WHERE GroupId = @GroupId)
        THROW 51104, 'Add-on group does not exist.', 1;

    IF NOT EXISTS (SELECT 1 FROM dbo.tblProduct WHERE ProductId = @ProductId)
        THROW 51105, 'Selected product does not exist.', 1;

    IF @GroupDetailId > 0
    BEGIN
        UPDATE dbo.tblAddonGroupDetail
        SET GroupId = @GroupId,
            ProductId = @ProductId,
            DisplayOrder = @DisplayOrder,
            Status = @Status,
            ModifiedBy = @CreatedBy,
            ModifiedDateTime = SYSDATETIME()
        WHERE GroupDetailId = @GroupDetailId;

        IF @@ROWCOUNT = 0
            THROW 51106, 'Add-on group detail not found.', 1;

        SET @Id = @GroupDetailId;
    END
    ELSE
    BEGIN
        UPDATE dbo.tblAddonGroupDetail
        SET DisplayOrder = @DisplayOrder,
            Status = @Status,
            ModifiedBy = @CreatedBy,
            ModifiedDateTime = SYSDATETIME()
        WHERE GroupId = @GroupId AND ProductId = @ProductId;

        IF @@ROWCOUNT > 0
            SELECT @Id = GroupDetailId
            FROM dbo.tblAddonGroupDetail
            WHERE GroupId = @GroupId AND ProductId = @ProductId;
        ELSE
        BEGIN
            INSERT INTO dbo.tblAddonGroupDetail
            (GroupId, ProductId, DisplayOrder, Status, CreatedBy, CreatedDateTime)
            VALUES
            (@GroupId, @ProductId, @DisplayOrder, @Status, @CreatedBy, SYSDATETIME());

            SET @Id = SCOPE_IDENTITY();
        END
    END

    SELECT @Id AS GroupDetailId;
END;
GO

/* 3. Parent product to add-on group mapping: dbo.tblProductAddonGroup */
IF OBJECT_ID(N'dbo.prdInsertProductAddonGroup', N'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.prdInsertProductAddonGroup AS BEGIN SET NOCOUNT ON; END');
GO
ALTER PROCEDURE dbo.prdInsertProductAddonGroup
    @ProductAddonGroupId BIGINT,
    @ProductId INT,
    @GroupId INT,
    @BranchId INT,
    @DisplayOrder INT,
    @Status BIT,
    @CreatedBy BIGINT,
    @ModifiedBy BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Id BIGINT;

    IF @ProductAddonGroupId > 0
    BEGIN
        UPDATE dbo.tblProductAddonGroup
        SET ProductId = @ProductId,
            GroupId = @GroupId,
            BranchId = @BranchId,
            DisplayOrder = @DisplayOrder,
            Status = @Status,
            ModifiedBy = @ModifiedBy,
            ModifiedDateTime = SYSDATETIME()
        WHERE ProductAddonGroupId = @ProductAddonGroupId;

        IF @@ROWCOUNT = 0
            THROW 51107, 'Product add-on group mapping not found.', 1;

        SET @Id = @ProductAddonGroupId;
    END
    ELSE
    BEGIN
        UPDATE dbo.tblProductAddonGroup
        SET DisplayOrder = @DisplayOrder,
            Status = @Status,
            ModifiedBy = @ModifiedBy,
            ModifiedDateTime = SYSDATETIME()
        WHERE ProductId = @ProductId
          AND GroupId = @GroupId
          AND BranchId = @BranchId;

        IF @@ROWCOUNT > 0
            SELECT @Id = ProductAddonGroupId
            FROM dbo.tblProductAddonGroup
            WHERE ProductId = @ProductId
              AND GroupId = @GroupId
              AND BranchId = @BranchId;
        ELSE
        BEGIN
            INSERT INTO dbo.tblProductAddonGroup
            (ProductId, GroupId, BranchId, DisplayOrder, Status, CreatedBy, CreatedDateTime)
            VALUES
            (@ProductId, @GroupId, @BranchId, @DisplayOrder, @Status, @CreatedBy, SYSDATETIME());

            SET @Id = SCOPE_IDENTITY();
        END
    END

    SELECT @Id AS ProductAddonGroupId;
END;
GO
