USE [FoodBeverage];
GO

/* Ensure approval audit columns exist on databases created from earlier request scripts. */
IF OBJECT_ID(N'dbo.tblInventoryRequestMaster', N'U') IS NULL
    THROW 50200, 'tblInventoryRequestMaster must be created before installing the approval workflow.', 1;
GO

IF COL_LENGTH(N'dbo.tblInventoryRequestMaster', N'ApprovalStatus') IS NULL
BEGIN
    ALTER TABLE dbo.tblInventoryRequestMaster
        ADD ApprovalStatus INT NOT NULL
            CONSTRAINT DF_tblInventoryRequestMaster_ApprovalStatus DEFAULT (0) WITH VALUES;
END;
GO
IF COL_LENGTH(N'dbo.tblInventoryRequestMaster', N'ApprovedBy') IS NULL
    ALTER TABLE dbo.tblInventoryRequestMaster ADD ApprovedBy INT NULL;
GO
IF COL_LENGTH(N'dbo.tblInventoryRequestMaster', N'ApprovedOn') IS NULL
    ALTER TABLE dbo.tblInventoryRequestMaster ADD ApprovedOn DATETIME NULL;
GO
IF COL_LENGTH(N'dbo.tblInventoryRequestMaster', N'ApprovalRemarks') IS NULL
    ALTER TABLE dbo.tblInventoryRequestMaster ADD ApprovalRemarks VARCHAR(MAX) NULL;
GO

/* One shared table-valued parameter for main requests and saved drafts. */
IF TYPE_ID(N'dbo.TVP_InventoryRequestDetail') IS NULL
BEGIN
    EXEC(N'
        CREATE TYPE dbo.TVP_InventoryRequestDetail AS TABLE
        (
            RequestType INT NOT NULL,
            SlNo INT NULL,
            ProductId INT NULL,
            PlannedQty DECIMAL(18,4) NULL,
            RecipeItemId BIGINT NULL,
            PerPortionQty DECIMAL(18,4) NULL,
            TotalRequiredQty DECIMAL(18,4) NULL,
            ItemId BIGINT NULL,
            ProductType INT NULL,
            CurrentStock DECIMAL(18,4) NULL,
            IssuedQty DECIMAL(18,4) NULL,
            ProjectedBalance DECIMAL(18,4) NULL,
            Unit INT NULL,
            PurposeRemarks VARCHAR(MAX) NULL
        );');
END;
GO

/* Save the final inventory request master and all its details atomically.
   When @DraftId is supplied, the source draft is also marked inactive. */
CREATE OR ALTER PROCEDURE dbo.prdSave_InventoryRequest
    @InventoryRequestId INT,
    @RequestNo VARCHAR(40),
    @RequestDate DATETIME,
    @RequiredDate DATETIME,
    @DepartmentId INT = NULL,
    @EmployeeId INT = NULL,
    @Remarks VARCHAR(MAX) = NULL,
    @RequestType INT,
    @CreatedBy INT,
    @ModifiedBy INT = NULL,
    @BranchId INT,
    @Status INT = 1,
    @DraftId INT = NULL,
    @Details dbo.TVP_InventoryRequestDetail READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @SavedRequestId INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF ISNULL(@InventoryRequestId, 0) > 0
        BEGIN
            UPDATE dbo.tblInventoryRequestMaster
            SET RequestNo = @RequestNo,
                RequestDate = @RequestDate,
                RequiredDate = @RequiredDate,
                DepartmentId = @DepartmentId,
                EmployeeId = @EmployeeId,
                Remarks = @Remarks,
                RequestType = @RequestType,
                ModifiedBy = ISNULL(@ModifiedBy, @CreatedBy),
                ModifiedOn = GETDATE(),
                BranchId = @BranchId,
                Status = @Status
            WHERE InventoryRequestId = @InventoryRequestId
              AND BranchId = @BranchId;

            IF @@ROWCOUNT = 0
                THROW 50101, 'Inventory request was not found.', 1;

            SET @SavedRequestId = @InventoryRequestId;

            UPDATE dbo.tblInventoryRequestDetails
            SET Status = 0,
                ModifiedBy = @CreatedBy,
                ModifiedOn = GETDATE()
            WHERE InventoryRequestId = @SavedRequestId
              AND BranchId = @BranchId
              AND ISNULL(Status, 1) = 1;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.tblInventoryRequestMaster
            (
                RequestNo, RequestDate, RequiredDate, DepartmentId,
                EmployeeId, Remarks, RequestType, CreatedBy, CreatedOn,
                BranchId, Status
            )
            VALUES
            (
                @RequestNo, @RequestDate, @RequiredDate, @DepartmentId,
                @EmployeeId, @Remarks, @RequestType, @CreatedBy, GETDATE(),
                @BranchId, @Status
            );

            SET @SavedRequestId = CONVERT(INT, SCOPE_IDENTITY());
        END;

        INSERT INTO dbo.tblInventoryRequestDetails
        (
            InventoryRequestId, RequestType, SlNo, ProductId, PlannedQty,
            RecipeItemId, PerPortionQty, TotalRequiredQty, ItemId, ProductType,
            CurrentStock, IssuedQty, ProjectedBalance, Unit, PurposeRemarks,
            CreatedBy, CreatedOn, BranchId, Status
        )
        SELECT
            @SavedRequestId, d.RequestType, d.SlNo, d.ProductId, d.PlannedQty,
            d.RecipeItemId, d.PerPortionQty, d.TotalRequiredQty, d.ItemId, d.ProductType,
            d.CurrentStock, d.IssuedQty, d.ProjectedBalance, d.Unit, d.PurposeRemarks,
            @CreatedBy, GETDATE(), @BranchId, 1
        FROM @Details d;

        IF @DraftId IS NOT NULL AND @DraftId > 0
        BEGIN
            IF NOT EXISTS
            (
                SELECT 1
                FROM dbo.tblInventoryRequestDraftMaster
                WHERE DraftId = @DraftId
                  AND BranchId = @BranchId
                  AND ISNULL(Status, 1) = 1
            )
                THROW 50102, 'The source draft was not found or is inactive.', 1;

            UPDATE dbo.tblInventoryRequestDraftDetails
            SET Status = 0, ModifiedBy = @CreatedBy, ModifiedOn = GETDATE()
            WHERE DraftId = @DraftId AND ISNULL(Status, 1) = 1;

            UPDATE dbo.tblInventoryRequestDraftMaster
            SET Status = 0, ModifiedBy = @CreatedBy, ModifiedOn = GETDATE()
            WHERE DraftId = @DraftId AND BranchId = @BranchId
              AND ISNULL(Status, 1) = 1;
        END;

        COMMIT TRANSACTION;
        SELECT @SavedRequestId AS InventoryRequestId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

/* Save or update one draft and replace its active detail snapshot atomically. */
CREATE OR ALTER PROCEDURE dbo.prdSave_InventoryRequestDraft
    @DraftId INT,
    @DraftNo VARCHAR(40) = NULL,
    @RequestNo VARCHAR(40) = NULL,
    @RequestDate DATETIME,
    @RequiredDate DATETIME,
    @DepartmentId INT = NULL,
    @EmployeeId INT = NULL,
    @Remarks VARCHAR(MAX) = NULL,
    @RequestType INT,
    @CreatedBy INT,
    @BranchId INT,
    @Status INT = 1,
    @Details dbo.TVP_InventoryRequestDetail READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @SavedDraftId INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF ISNULL(@DraftId, 0) > 0
        BEGIN
            UPDATE dbo.tblInventoryRequestDraftMaster
            SET RequestNo = @RequestNo,
                RequestDate = @RequestDate,
                RequiredDate = @RequiredDate,
                DepartmentId = @DepartmentId,
                EmployeeId = @EmployeeId,
                Remarks = @Remarks,
                RequestType = @RequestType,
                ModifiedBy = @CreatedBy,
                ModifiedOn = GETDATE(),
                BranchId = @BranchId,
                Status = @Status
            WHERE DraftId = @DraftId
              AND BranchId = @BranchId
              AND ISNULL(Status, 1) = 1;

            IF @@ROWCOUNT = 0
                THROW 50103, 'The selected draft was not found or is inactive.', 1;

            SET @SavedDraftId = @DraftId;

            UPDATE dbo.tblInventoryRequestDraftDetails
            SET Status = 0, ModifiedBy = @CreatedBy, ModifiedOn = GETDATE()
            WHERE DraftId = @SavedDraftId AND ISNULL(Status, 1) = 1;
        END
        ELSE
        BEGIN
            IF NULLIF(LTRIM(RTRIM(@DraftNo)), '') IS NULL
                SET @DraftNo = 'DR-' + CONVERT(VARCHAR(8), GETDATE(), 112)
                    + '-' + REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(12), GETDATE(), 114), ':', ''), '.', ''), ' ', '');

            INSERT INTO dbo.tblInventoryRequestDraftMaster
            (
                DraftNo, RequestNo, RequestDate, RequiredDate, DepartmentId,
                EmployeeId, Remarks, RequestType, CreatedBy, CreatedOn,
                BranchId, Status
            )
            VALUES
            (
                @DraftNo, @RequestNo, @RequestDate, @RequiredDate, @DepartmentId,
                @EmployeeId, @Remarks, @RequestType, @CreatedBy, GETDATE(),
                @BranchId, @Status
            );

            SET @SavedDraftId = CONVERT(INT, SCOPE_IDENTITY());
        END;

        INSERT INTO dbo.tblInventoryRequestDraftDetails
        (
            DraftId, RequestType, SlNo, ProductId, PlannedQty,
            RecipeItemId, PerPortionQty, TotalRequiredQty, ItemId, ProductType,
            CurrentStock, IssuedQty, ProjectedBalance, Unit, PurposeRemarks,
            CreatedBy, CreatedOn, BranchId, Status
        )
        SELECT
            @SavedDraftId, d.RequestType, d.SlNo, d.ProductId, d.PlannedQty,
            d.RecipeItemId, d.PerPortionQty, d.TotalRequiredQty, d.ItemId, d.ProductType,
            d.CurrentStock, d.IssuedQty, d.ProjectedBalance, d.Unit, d.PurposeRemarks,
            @CreatedBy, GETDATE(), @BranchId, 1
        FROM @Details d;

        COMMIT TRANSACTION;
        SELECT @SavedDraftId AS DraftId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

/* Soft-delete a complete saved draft. */
CREATE OR ALTER PROCEDURE dbo.prdDelete_InventoryRequestDraft
    @DraftId INT,
    @EmployeeId INT,
    @BranchId INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS
        (
            SELECT 1 FROM dbo.tblInventoryRequestDraftMaster
            WHERE DraftId = @DraftId AND BranchId = @BranchId
              AND ISNULL(Status, 1) = 1
        )
            THROW 50104, 'The selected draft was not found or is already inactive.', 1;

        UPDATE dbo.tblInventoryRequestDraftDetails
        SET Status = 0, ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
        WHERE DraftId = @DraftId AND ISNULL(Status, 1) = 1;

        UPDATE dbo.tblInventoryRequestDraftMaster
        SET Status = 0, ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
        WHERE DraftId = @DraftId AND BranchId = @BranchId
          AND ISNULL(Status, 1) = 1;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

/* Soft-delete one product group or one raw-material row from a draft/request.
   Draft rows require confirmation in the form; normal request rows do not. */
CREATE OR ALTER PROCEDURE dbo.prdDelete_InventoryRequestRow
    @IsDraft BIT,
    @DraftId INT = NULL,
    @InventoryRequestId INT = NULL,
    @RequestType INT,
    @ProductId INT = NULL,
    @ItemId BIGINT = NULL,
    @EmployeeId INT,
    @BranchId INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @RequestType = 1 AND ISNULL(@ProductId, 0) <= 0
            THROW 50105, 'Product ID is required to delete a batch product.', 1;

        IF @RequestType = 2 AND ISNULL(@ItemId, 0) <= 0
            THROW 50106, 'Item ID is required to delete a raw material.', 1;

        IF @IsDraft = 1
        BEGIN
            IF NOT EXISTS
            (
                SELECT 1 FROM dbo.tblInventoryRequestDraftMaster
                WHERE DraftId = @DraftId AND BranchId = @BranchId
                  AND ISNULL(Status, 1) = 1
            )
                THROW 50107, 'The draft is not available for editing.', 1;

            UPDATE dbo.tblInventoryRequestDraftDetails
            SET Status = 0, ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
            WHERE DraftId = @DraftId
              AND RequestType = @RequestType
              AND ISNULL(Status, 1) = 1
              AND
              (
                  (@RequestType = 1 AND ProductId = @ProductId)
                  OR (@RequestType = 2 AND ItemId = @ItemId)
              );
        END
        ELSE
        BEGIN
            IF NOT EXISTS
            (
                SELECT 1 FROM dbo.tblInventoryRequestMaster
                WHERE InventoryRequestId = @InventoryRequestId
                  AND BranchId = @BranchId
                  AND ISNULL(Status, 1) = 1
            )
                THROW 50108, 'The inventory request is not available for editing.', 1;

            UPDATE dbo.tblInventoryRequestDetails
            SET Status = 0, ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
            WHERE InventoryRequestId = @InventoryRequestId
              AND BranchId = @BranchId
              AND RequestType = @RequestType
              AND ISNULL(Status, 1) = 1
              AND
              (
                  (@RequestType = 1 AND ProductId = @ProductId)
                  OR (@RequestType = 2 AND ItemId = @ItemId)
              );
        END;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

/*
    Inventory request approval workflow:
      0 = Pending
      1 = Approved
      2 = Rejected
      3 = Sent Back

    Only Pending requests can receive a manager decision.
    Only Sent Back requests can be resubmitted by the requesting employee.
*/
CREATE OR ALTER PROCEDURE dbo.prdUpdate_InventoryRequestApproval
    @InventoryRequestId INT,
    @ApprovalStatus INT,
    @EmployeeId INT,
    @ApprovalRemarks VARCHAR(MAX) = NULL,
    @BranchId INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @ApprovalStatus = 0
        BEGIN
            UPDATE dbo.tblInventoryRequestMaster
            SET ApprovalStatus = 0,
                ApprovedBy = NULL,
                ApprovedOn = NULL,
                ModifiedBy = @EmployeeId,
                ModifiedOn = GETDATE()
            WHERE InventoryRequestId = @InventoryRequestId
              AND BranchId = @BranchId
              AND ISNULL(Status, 1) = 1
              AND ApprovalStatus = 3;

            IF @@ROWCOUNT = 0
                THROW 50201, 'Only an active Sent Back request can be resubmitted.', 1;
            -- Keep the Send Back remarks so the requester can still see the reason
            -- in the Inventory Request list after resubmission.
        END
        ELSE IF @ApprovalStatus IN (1, 2, 3)
        BEGIN
            UPDATE dbo.tblInventoryRequestMaster
            SET ApprovalStatus = @ApprovalStatus,
                ApprovedBy = @EmployeeId,
                ApprovedOn = GETDATE(),
                ApprovalRemarks = NULLIF(LTRIM(RTRIM(@ApprovalRemarks)), ''),
                ModifiedBy = @EmployeeId,
                ModifiedOn = GETDATE()
            WHERE InventoryRequestId = @InventoryRequestId
              AND BranchId = @BranchId
              AND ISNULL(Status, 1) = 1
              AND ApprovalStatus = 0;

            IF @@ROWCOUNT = 0
                THROW 50202, 'Only an active Pending request can be approved, rejected, or sent back.', 1;
        END
        ELSE
        BEGIN
            THROW 50203, 'Invalid inventory request approval status.', 1;
        END;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
