USE [FoodBeverage];
GO

/*
    Inventory Request master/details tables.
    RequestType: 1 = Batch Preparation, 2 = Raw Material.
    Status: 1 = Active, 0 = Inactive. Approval status is kept separately.
*/
IF OBJECT_ID(N'dbo.tblInventoryRequestDetails', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblInventoryRequestDetails
    (
        InventoryRequestDetailId INT IDENTITY(1,1) NOT NULL,
        InventoryRequestId INT NOT NULL,
        RequestType INT NOT NULL,
        SlNo INT NULL,

        -- Batch preparation fields
        ProductId INT NULL,
        PlannedQty DECIMAL(18,4) NULL,
        RecipeItemId BIGINT NULL,
        PerPortionQty DECIMAL(18,4) NULL,
        TotalRequiredQty DECIMAL(18,4) NULL,

        -- Raw material / ingredient fields
        ItemId BIGINT NULL,
        ProductType INT NULL,
        CurrentStock DECIMAL(18,4) NULL,
        IssuedQty DECIMAL(18,4) NULL,
        ProjectedBalance DECIMAL(18,4) NULL,
        Unit INT NULL,
        PurposeRemarks VARCHAR(MAX) NULL,

        CreatedBy INT NULL,
        CreatedOn DATETIME NULL CONSTRAINT DF_tblInventoryRequestDetails_CreatedOn DEFAULT (GETDATE()),
        ModifiedBy INT NULL,
        ModifiedOn DATETIME NULL,
        BranchId INT NULL,
        Status INT NULL CONSTRAINT DF_tblInventoryRequestDetails_Status DEFAULT (1),

        CONSTRAINT PK_tblInventoryRequestDetails
            PRIMARY KEY CLUSTERED (InventoryRequestDetailId),
        CONSTRAINT FK_tblInventoryRequestDetails_Master
            FOREIGN KEY (InventoryRequestId)
            REFERENCES dbo.tblInventoryRequestMaster (InventoryRequestId)
            ON DELETE CASCADE
    );

    CREATE INDEX IX_tblInventoryRequestDetails_RequestId
        ON dbo.tblInventoryRequestDetails (InventoryRequestId);

    CREATE INDEX IX_tblInventoryRequestDetails_Item
        ON dbo.tblInventoryRequestDetails (ItemId, ProductId);
END;
GO

IF OBJECT_ID(N'dbo.tblInventoryRequestMaster', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblInventoryRequestMaster
    (
        InventoryRequestId INT IDENTITY(1,1) NOT NULL,
        RequestNo VARCHAR(40) NOT NULL,
        RequestDate DATETIME NOT NULL,
        RequiredDate DATETIME NOT NULL,
        DepartmentId INT NULL,
        EmployeeId INT NULL,
        Remarks VARCHAR(MAX) NULL,

        -- 1 = Batch Preparation, 2 = Raw Material, 3 = Mixed (optional future use)
        RequestType INT NOT NULL CONSTRAINT DF_tblInventoryRequestMaster_RequestType DEFAULT (1),
        ApprovalStatus INT NOT NULL CONSTRAINT DF_tblInventoryRequestMaster_ApprovalStatus DEFAULT (0),
        ApprovedBy INT NULL,
        ApprovedOn DATETIME NULL,
        ApprovalRemarks VARCHAR(MAX) NULL,

        CreatedBy INT NULL,
        CreatedOn DATETIME NULL CONSTRAINT DF_tblInventoryRequestMaster_CreatedOn DEFAULT (GETDATE()),
        ModifiedBy INT NULL,
        ModifiedOn DATETIME NULL,
        BranchId INT NULL,
        Status INT NULL CONSTRAINT DF_tblInventoryRequestMaster_Status DEFAULT (1),

        CONSTRAINT PK_tblInventoryRequestMaster
            PRIMARY KEY CLUSTERED (InventoryRequestId),
        CONSTRAINT UQ_tblInventoryRequestMaster_RequestNo UNIQUE (RequestNo)
    );
END;
GO

/* Helpful indexes for request listing and approval queues. */
IF NOT EXISTS
(
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_tblInventoryRequestMaster_RequestDate'
      AND object_id = OBJECT_ID(N'dbo.tblInventoryRequestMaster')
)
BEGIN
    CREATE INDEX IX_tblInventoryRequestMaster_RequestDate
        ON dbo.tblInventoryRequestMaster (RequestDate, RequestType, ApprovalStatus, BranchId);
END;
GO
