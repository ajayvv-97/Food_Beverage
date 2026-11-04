USE [FoodBeverage];
GO

/*
    QOH must support decimal quantities because inventory request detail
    quantities are DECIMAL(18,4). Existing integer QOH values convert exactly.
*/
ALTER TABLE dbo.tblProductRate
    ALTER COLUMN QOH DECIMAL(18,4) NULL;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/*
    Inventory request approval workflow:
      0 = Pending / resubmitted after Send Back
      1 = Approved
      2 = Rejected
      3 = Sent Back

    Stock deduction when approving:
      RequestType = 1: batch recipe ingredients -> TotalRequiredQty
      RequestType = 2: direct raw materials -> IssuedQty

    Both types are grouped by ItemId. Stock is reduced from the latest
    tblProductRate row with ProductType = 0, matching FrmInventoryrequest's
    current raw-material QOH lookup.
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
        ProductRateId INT NULL,
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
                Deduct stock only on approval.
                Batch recipe ingredient rows store their required quantity in
                TotalRequiredQty. Direct raw-material rows store their issue
                quantity in IssuedQty.
            */
            IF @ApprovalStatus = 1
            BEGIN
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

                INSERT INTO @StockToDeduct (ItemId, RequiredQty)
                SELECT
                    d.ItemId,
                    SUM
                    (
                        CASE
                            WHEN d.RequestType = 1 THEN ISNULL(d.TotalRequiredQty, 0)
                            WHEN d.RequestType = 2 THEN ISNULL(d.IssuedQty, 0)
                            ELSE 0
                        END
                    ) AS RequiredQty
                FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
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
                    Use the same rate-row selection as FrmInventoryrequest:
                    ProductId = ItemId, ProductType = 0, newest Id.
                    UPDLOCK + HOLDLOCK keeps the selected stock rows locked
                    until the approval transaction completes.
                */
                UPDATE stock
                SET
                    ProductRateId = rate.Id,
                    AvailableQty = CONVERT(DECIMAL(18,4), ISNULL(rate.QOH, 0))
                FROM @StockToDeduct AS stock
                OUTER APPLY
                (
                    SELECT TOP (1)
                        pr.Id,
                        pr.QOH
                    FROM dbo.tblProductRate AS pr WITH (UPDLOCK, HOLDLOCK)
                    WHERE pr.ProductId = stock.ItemId
                      AND ISNULL(pr.ProductType, 0) = 0
                    ORDER BY pr.Id DESC
                ) AS rate;

                IF EXISTS
                (
                    SELECT 1
                    FROM @StockToDeduct
                    WHERE ProductRateId IS NULL
                )
                BEGIN
                    THROW 50209, 'A stock record was not found in tblProductRate for one or more raw materials. Approval was cancelled.', 1;
                END;

                IF EXISTS
                (
                    SELECT 1
                    FROM @StockToDeduct
                    WHERE ISNULL(AvailableQty, 0) < RequiredQty
                )
                BEGIN
                    THROW 50210, 'Insufficient QOH in tblProductRate for one or more raw materials. Approval was cancelled.', 1;
                END;

                /* Deduct the grouped requirement once per item. */
                UPDATE pr
                SET pr.QOH = CONVERT(DECIMAL(18,4), ISNULL(pr.QOH, 0)) - stock.RequiredQty
                FROM dbo.tblProductRate AS pr
                INNER JOIN @StockToDeduct AS stock
                    ON stock.ProductRateId = pr.Id;
            END;

            /*
                Update the approval state only after the stock checks and
                deduction succeed. Any failure rolls back both stock and status.
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
