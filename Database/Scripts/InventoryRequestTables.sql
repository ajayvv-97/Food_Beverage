USE [FoodBeverage];
GO
IF OBJECT_ID('dbo.tblInventoryRequest', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblInventoryRequest
    (
        RequestId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tblInventoryRequest PRIMARY KEY,
        RequestNo VARCHAR(40) NOT NULL,
        RequestDate DATETIME NOT NULL,
        RequiredDate DATETIME NOT NULL,
        DepartmentId INT NULL,
        EmployeeId INT NULL,
        Remarks VARCHAR(MAX) NULL,
        RequestType INT NOT NULL, -- 1 = Batch, 2 = Raw material, 3 = mixed request
        CreatedBy INT NULL,
        CreatedOn DATETIME NULL,
        ModifiedBy INT NULL,
        ModifiedOn DATETIME NULL,
        BranchId INT NULL,
        Status INT NULL
    );
    CREATE UNIQUE INDEX UX_tblInventoryRequest_RequestNo ON dbo.tblInventoryRequest(RequestNo);
END
GO
IF OBJECT_ID('dbo.tblInventoryRequestDetails', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblInventoryRequestDetails
    (
        RequestDetailId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tblInventoryRequestDetails PRIMARY KEY,
        RequestId INT NOT NULL,
        RequestType INT NOT NULL,
        ItemId INT NOT NULL,
        ItemName VARCHAR(250) NULL,
        PlannedQty DECIMAL(18,4) NULL,
        CurrentStock DECIMAL(18,4) NULL,
        IssuedQty DECIMAL(18,4) NULL,
        UnitId INT NULL,
        ProjectBalance DECIMAL(18,4) NULL,
        Purpose VARCHAR(MAX) NULL,
        ProductType INT NOT NULL,
        CreatedBy INT NULL,
        CreatedOn DATETIME NULL,
        ModifiedBy INT NULL,
        ModifiedOn DATETIME NULL,
        BranchId INT NULL,
        Status INT NULL,
        CONSTRAINT FK_tblInventoryRequestDetails_Master FOREIGN KEY(RequestId)
            REFERENCES dbo.tblInventoryRequest(RequestId)
    );
    CREATE INDEX IX_tblInventoryRequestDetails_RequestId ON dbo.tblInventoryRequestDetails(RequestId);
END
GO