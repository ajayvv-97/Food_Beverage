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
                    Match the existing stock lookup:
                    ProductRate.ProductId = ItemId,
                    ProductType = 0, latest rate row by Id.
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
                    THROW 50210, 'Insufficient QOH in tblProductRate for one or more raw materials after unit conversion. Approval was cancelled.', 1;
                END;

                /* Deduct the converted Baseunit quantity once per item. */
                UPDATE pr
                SET pr.QOH = CONVERT(DECIMAL(18,4), ISNULL(pr.QOH, 0)) - stock.RequiredQty
                FROM dbo.tblProductRate AS pr
                INNER JOIN @StockToDeduct AS stock
                    ON stock.ProductRateId = pr.Id;
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