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

    DECLARE @CurrentApprovalStatus INT = NULL;

    DECLARE @StockToDeduct TABLE
    (
        ItemId BIGINT NOT NULL PRIMARY KEY,
        RequiredQty DECIMAL(18,4) NOT NULL,
        ActiveRateCount INT NULL,
        AvailableQty DECIMAL(18,4) NULL
    );

    BEGIN TRY
        BEGIN TRANSACTION;

        IF ISNULL(@InventoryRequestId, 0) <= 0
            THROW 50204, 'A valid inventory request ID is required.', 1;

        IF ISNULL(@EmployeeId, 0) <= 0
            THROW 50205, 'A valid employee ID is required for this action.', 1;

        /*
            Lock the request master row before checking its status.
            This prevents two users from approving the same request and
            deducting its stock more than once.
        */
        SELECT @CurrentApprovalStatus = ISNULL(ApprovalStatus, 0)
        FROM dbo.tblInventoryRequestMaster WITH (UPDLOCK, HOLDLOCK)
        WHERE InventoryRequestId = @InventoryRequestId
          AND BranchId = @BranchId
          AND ISNULL(Status, 1) = 1;

        IF @CurrentApprovalStatus IS NULL
            THROW 50207, 'The active inventory request was not found for this branch.', 1;

        /* Status 0 = resubmit a request that was previously sent back. */
        IF @ApprovalStatus = 0
        BEGIN
            IF @CurrentApprovalStatus <> 3
                THROW 50201, 'Only an active Sent Back request can be resubmitted.', 1;

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

            /* Keep ApprovalRemarks so the requester can still see the send-back reason. */
        END
        /* Status 1 = Approved, 2 = Rejected, 3 = Sent Back. */
        ELSE IF @ApprovalStatus IN (1, 2, 3)
        BEGIN
            IF @CurrentApprovalStatus <> 0
                THROW 50202, 'Only an active Pending request can be approved, rejected, or sent back.', 1;

            IF @ApprovalStatus IN (2, 3)
               AND NULLIF(LTRIM(RTRIM(@ApprovalRemarks)), '') IS NULL
            BEGIN
                THROW 50206, 'Remarks are required when rejecting or sending back a request.', 1;
            END;

            /*
                Deduct stock only when approving.

                tblItem.unit       = purchase unit
                tblItem.Baseunit   = stock/base conversion unit
                tblItem.Conversionfactor = quantity of Baseunit in one purchase unit

                If the inventory request detail Unit is the purchase unit,
                RequiredQty is multiplied by Conversionfactor.
                If it is already Baseunit, it is deducted without conversion.

                The request form stores direct raw-material rows in Baseunit.
                Batch ingredient rows retain the Unit selected in the recipe.
            */
            IF @ApprovalStatus = 1
            BEGIN
                /* Prevent negative stock requirements. */
                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND
                      (
                          (d.RequestType = 1 AND ISNULL(d.TotalRequiredQty, 0) < 0)
                          OR
                          (d.RequestType = 2 AND ISNULL(d.IssuedQty, 0) < 0)
                      )
                )
                BEGIN
                    THROW 50208, 'A raw-material quantity cannot be negative. Stock was not deducted.', 1;
                END;

                /*
                    A stock-bearing detail must reference a valid item and use
                    either that item's purchase unit or Baseunit. Do not guess
                    a conversion for an unrelated unit.
                */
                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                    LEFT JOIN dbo.tblItem AS i
                        ON i.ItemId = d.ItemId
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND d.RequestType IN (1, 2)
                      AND d.ItemId IS NOT NULL
                      AND
                      (
                          (d.RequestType = 1 AND ISNULL(d.TotalRequiredQty, 0) > 0)
                          OR
                          (d.RequestType = 2 AND ISNULL(d.IssuedQty, 0) > 0)
                      )
                      AND
                      (
                          i.ItemId IS NULL
                          OR d.Unit IS NULL
                          OR
                          (
                              d.Unit <> i.unit
                              AND (i.Baseunit IS NULL OR d.Unit <> i.Baseunit)
                          )
                      )
                )
                BEGIN
                    THROW 50211, 'A request detail has an invalid unit. The unit must match the item purchase unit or Baseunit.', 1;
                END;

                /*
                    Conversionfactor must be positive whenever a detail is
                    entered in the purchase unit and Baseunit is different.
                */
                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                    INNER JOIN dbo.tblItem AS i
                        ON i.ItemId = d.ItemId
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND d.RequestType IN (1, 2)
                      AND d.Unit = i.unit
                      AND i.Baseunit IS NOT NULL
                      AND i.Baseunit <> i.unit
                      AND ISNULL(i.Conversionfactor, 0) <= 0
                      AND
                      (
                          (d.RequestType = 1 AND ISNULL(d.TotalRequiredQty, 0) > 0)
                          OR
                          (d.RequestType = 2 AND ISNULL(d.IssuedQty, 0) > 0)
                      )
                )
                BEGIN
                    THROW 50212, 'The item has an invalid Conversionfactor. Stock was not deducted.', 1;
                END;

                /*
                    Calculate all required stock in Baseunit and group by ItemId.
                    This also combines an item if it is used by both a batch
                    product recipe and the direct raw-material section.
                */
                INSERT INTO @StockToDeduct (ItemId, RequiredQty)
                SELECT
                    d.ItemId,
                    SUM
                    (
                        CONVERT
                        (
                            DECIMAL(18,4),
                            CASE
                                WHEN d.RequestType = 1 THEN
                                    ISNULL(d.TotalRequiredQty, 0) *
                                    CASE
                                        WHEN d.Unit = i.unit
                                         AND i.Baseunit IS NOT NULL
                                         AND i.Baseunit <> i.unit
                                            THEN i.Conversionfactor
                                        ELSE 1
                                    END
                                WHEN d.RequestType = 2 THEN
                                    ISNULL(d.IssuedQty, 0) *
                                    CASE
                                        WHEN d.Unit = i.unit
                                         AND i.Baseunit IS NOT NULL
                                         AND i.Baseunit <> i.unit
                                            THEN i.Conversionfactor
                                        ELSE 1
                                    END
                                ELSE 0
                            END
                        )
                    ) AS RequiredQty
                FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                INNER JOIN dbo.tblItem AS i
                    ON i.ItemId = d.ItemId
                WHERE d.InventoryRequestId = @InventoryRequestId
                  AND ISNULL(d.Status, 1) = 1
                  AND d.RequestType IN (1, 2)
                  AND d.ItemId IS NOT NULL
                GROUP BY d.ItemId
                HAVING SUM
                (
                    CASE
                        WHEN d.RequestType = 1 THEN ISNULL(d.TotalRequiredQty, 0)
                        WHEN d.RequestType = 2 THEN ISNULL(d.IssuedQty, 0)
                        ELSE 0
                    END
                ) > 0;

                /*
                    QOH is the sum of every active price/rate row for the item.
                    This matches the inventory request search and supports items
                    that have multiple purchase prices/rate records.
                */
                UPDATE stock
                SET
                    ActiveRateCount = rate.RateCount,
                    AvailableQty = CONVERT(DECIMAL(18,4), ISNULL(rate.AvailableQty, 0))
                FROM @StockToDeduct AS stock
                OUTER APPLY
                (
                    SELECT
                        COUNT(*) AS RateCount,
                        SUM(CONVERT(DECIMAL(18,4), ISNULL(pr.QOH, 0))) AS AvailableQty
                    FROM dbo.tblProductRate AS pr WITH (UPDLOCK, HOLDLOCK)
                    WHERE pr.ProductId = stock.ItemId
                      AND pr.Status = 1
                ) AS rate;

                IF EXISTS
                (
                    SELECT 1
                    FROM @StockToDeduct
                    WHERE ISNULL(ActiveRateCount, 0) = 0
                )
                BEGIN
                    THROW 50209, 'No active stock records were found in tblProductRate for one or more raw materials. Approval was cancelled.', 1;
                END;

                IF EXISTS
                (
                    SELECT 1
                    FROM @StockToDeduct
                    WHERE ISNULL(AvailableQty, 0) < RequiredQty
                )
                BEGIN
                    THROW 50210, 'Insufficient total QOH across active tblProductRate rows for one or more raw materials after unit conversion. Approval was cancelled.', 1;
                END;

                /*
                    Deduct from active rate rows in FIFO order (oldest rate row
                    first). If one row does not contain enough quantity, the
                    remaining issue quantity continues into the next row.
                    All affected rows remain inside this approval transaction.
                */
                ;WITH RateRows AS
                (
                    SELECT
                        pr.Id,
                        CAST
                        (
                            CASE
                                WHEN ISNULL(pr.QOH, 0) > 0
                                    THEN ISNULL(pr.QOH, 0)
                                ELSE 0
                            END
                            AS DECIMAL(18,4)
                        ) AS RowQty,
                        stock.RequiredQty,
                        ISNULL
                        (
                            SUM
                            (
                                CAST
                                (
                                    CASE
                                        WHEN ISNULL(pr.QOH, 0) > 0
                                            THEN ISNULL(pr.QOH, 0)
                                        ELSE 0
                                    END
                                    AS DECIMAL(18,4)
                                )
                            ) OVER
                            (
                                PARTITION BY pr.ProductId
                                ORDER BY pr.Id
                                ROWS BETWEEN UNBOUNDED PRECEDING AND 1 PRECEDING
                            ),
                            0
                        ) AS QtyBefore
                    FROM dbo.tblProductRate AS pr WITH (UPDLOCK, HOLDLOCK)
                    INNER JOIN @StockToDeduct AS stock
                        ON stock.ItemId = pr.ProductId
                    WHERE pr.Status = 1
                ),
                RateDeductions AS
                (
                    SELECT
                        Id,
                        CASE
                            WHEN RowQty <= 0 OR QtyBefore >= RequiredQty
                                THEN CONVERT(DECIMAL(18,4), 0)
                            WHEN QtyBefore + RowQty <= RequiredQty
                                THEN RowQty
                            ELSE RequiredQty - QtyBefore
                        END AS DeductQty
                    FROM RateRows
                )
                UPDATE pr
                SET pr.QOH =
                    CONVERT(DECIMAL(18,4), ISNULL(pr.QOH, 0)) - deduction.DeductQty
                FROM dbo.tblProductRate AS pr
                INNER JOIN RateDeductions AS deduction
                    ON deduction.Id = pr.Id
                WHERE deduction.DeductQty > 0;
            END;

            /*
                Update approval only after stock validation and deduction.
                Any error rolls back both stock changes and approval status.
            */
            UPDATE dbo.tblInventoryRequestMaster
            SET ApprovalStatus = @ApprovalStatus,
                ApprovedBy = @EmployeeId,
                ApprovedOn = GETDATE(),
                ApprovalRemarks = ISNULL(NULLIF(LTRIM(RTRIM(@ApprovalRemarks)), ''), ''),
                ModifiedBy = @EmployeeId,
                ModifiedOn = GETDATE()
            WHERE InventoryRequestId = @InventoryRequestId
              AND BranchId = @BranchId
              AND ISNULL(Status, 1) = 1
              AND ISNULL(ApprovalStatus, 0) = 0;

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
    END CATCH;
END;
GO