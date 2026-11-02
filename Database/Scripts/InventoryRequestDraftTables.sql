USE [FoodBeverage];
GO

/*
    Draft storage for FrmInventoryrequest.
    Status = 1 : active draft
    Status = 0 : soft-deleted draft
*/
IF OBJECT_ID(N'dbo.tblInventoryRequestDraftMaster', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblInventoryRequestDraftMaster
    (
        DraftId INT IDENTITY(1,1) NOT NULL,
        DraftNo VARCHAR(40) NOT NULL,
        RequestNo VARCHAR(40) NULL,
        RequestDate DATETIME NOT NULL,
        RequiredDate DATETIME NOT NULL,
        DepartmentId INT NULL,
        EmployeeId INT NULL,
        Remarks VARCHAR(MAX) NULL,
        RequestType INT NOT NULL CONSTRAINT DF_tblInventoryRequestDraftMaster_RequestType DEFAULT (1),
        CreatedBy INT NULL,
        CreatedOn DATETIME NULL CONSTRAINT DF_tblInventoryRequestDraftMaster_CreatedOn DEFAULT (GETDATE()),
        ModifiedBy INT NULL,
        ModifiedOn DATETIME NULL,
        BranchId INT NULL,
        Status INT NULL CONSTRAINT DF_tblInventoryRequestDraftMaster_Status DEFAULT (1),

        CONSTRAINT PK_tblInventoryRequestDraftMaster
            PRIMARY KEY CLUSTERED (DraftId),
        CONSTRAINT UQ_tblInventoryRequestDraftMaster_DraftNo
            UNIQUE NONCLUSTERED (DraftNo)
    );

    CREATE INDEX IX_tblInventoryRequestDraftMaster_Branch_Status
        ON dbo.tblInventoryRequestDraftMaster (BranchId, Status, DraftId DESC);
END;
GO

IF OBJECT_ID(N'dbo.tblInventoryRequestDraftDetails', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblInventoryRequestDraftDetails
    (
        DraftDetailId INT IDENTITY(1,1) NOT NULL,
        DraftId INT NOT NULL,
        RequestType INT NOT NULL,
        SlNo INT NULL,

        -- Batch-preparation product and recipe ingredient fields.
        ProductId INT NULL,
        PlannedQty DECIMAL(18,4) NULL,
        RecipeItemId BIGINT NULL,
        PerPortionQty DECIMAL(18,4) NULL,
        TotalRequiredQty DECIMAL(18,4) NULL,

        -- Direct raw-material issue fields.
        ItemId BIGINT NULL,
        ProductType INT NULL,
        CurrentStock DECIMAL(18,4) NULL,
        IssuedQty DECIMAL(18,4) NULL,
        ProjectedBalance DECIMAL(18,4) NULL,
        Unit INT NULL,
        PurposeRemarks VARCHAR(MAX) NULL,

        CreatedBy INT NULL,
        CreatedOn DATETIME NULL CONSTRAINT DF_tblInventoryRequestDraftDetails_CreatedOn DEFAULT (GETDATE()),
        ModifiedBy INT NULL,
        ModifiedOn DATETIME NULL,
        BranchId INT NULL,
        Status INT NULL CONSTRAINT DF_tblInventoryRequestDraftDetails_Status DEFAULT (1),

        CONSTRAINT PK_tblInventoryRequestDraftDetails
            PRIMARY KEY CLUSTERED (DraftDetailId),
        CONSTRAINT FK_tblInventoryRequestDraftDetails_Master
            FOREIGN KEY (DraftId)
            REFERENCES dbo.tblInventoryRequestDraftMaster (DraftId)
            ON DELETE CASCADE
    );

    CREATE INDEX IX_tblInventoryRequestDraftDetails_Draft_Status_SlNo
        ON dbo.tblInventoryRequestDraftDetails (DraftId, Status, SlNo);
END;
GO
