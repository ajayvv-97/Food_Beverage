/*
  Add-on Group Setup schema for Food & Beverage
  Product references use dbo.tblProduct.ProductId.
  Run against a BACKUP/TEST database first.
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
        CreatedBy INT NULL,
        CreatedDateTime DATETIME NOT NULL CONSTRAINT DF_tblAddonGroup_CreatedDateTime DEFAULT (GETDATE()),
        ModifiedBy INT NULL,
        ModifiedDateTime DATETIME NULL,
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
        CreatedBy INT NULL,
        CreatedDateTime DATETIME NOT NULL CONSTRAINT DF_tblAddonGroupDetail_CreatedDateTime DEFAULT (GETDATE()),
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
        CreatedBy INT NULL,
        CreatedDateTime DATETIME NOT NULL CONSTRAINT DF_tblProductAddonGroup_CreatedDateTime DEFAULT (GETDATE()),
        CONSTRAINT FK_tblProductAddonGroup_Product FOREIGN KEY (ProductId) REFERENCES dbo.tblProduct(ProductId),
        CONSTRAINT FK_tblProductAddonGroup_Group FOREIGN KEY (GroupId) REFERENCES dbo.tblAddonGroup(GroupId)
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tblProductAddonGroup') AND name=N'UX_tblProductAddonGroup_Product_Group_Branch')
    CREATE UNIQUE INDEX UX_tblProductAddonGroup_Product_Group_Branch
        ON dbo.tblProductAddonGroup(ProductId, GroupId, BranchId);
GO

PRINT 'Add-on group schema created/verified.';
GO
