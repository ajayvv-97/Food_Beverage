USE [FoodBeverage];
GO

/*
    QOH is stored in Purchase Units. The approval procedure converts QOH to
    Base Units for consumption calculations, then saves the remainder back
    in Purchase Units. QOH must support fractional purchase quantities.
*/
IF OBJECT_ID(N'dbo.tblProductRate', N'U') IS NOT NULL
   AND COL_LENGTH(N'dbo.tblProductRate', N'QOH') IS NOT NULL
   AND EXISTS
   (
       SELECT 1
       FROM sys.columns
       WHERE object_id = OBJECT_ID(N'dbo.tblProductRate')
         AND name = N'QOH'
         AND
         (
             system_type_id NOT IN (106, 108)
             OR precision <> 18
             OR scale <> 4
         )
   )
BEGIN
    ALTER TABLE dbo.tblProductRate
        ALTER COLUMN QOH DECIMAL(18,4) NULL;
END;
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

    Both types are grouped by ItemId. Available QOH is the sum of every active
    tblProductRate row (Status = 1), including items with multiple price records.
    Stock deduction is allocated FIFO by ProductRate.Id (oldest row first).
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

    /*
        RequiredQty and AvailableBaseQty are calculated in Base Units.
        ConversionFactor converts each rate row's Purchase Unit QOH to Base Units.
    */
    DECLARE @StockToDeduct TABLE
    (
        ItemId BIGINT NOT NULL PRIMARY KEY,
        RequiredQty DECIMAL(18,4) NOT NULL,
        ConversionFactor DECIMAL(18,4) NOT NULL,
        ActiveRateCount INT NULL,
        AvailableBaseQty DECIMAL(18,4) NULL
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
                INSERT INTO @StockToDeduct
                (
                    ItemId,
                    RequiredQty,
                    ConversionFactor
                )
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
                                         AND ISNULL(i.Baseunit,i.unit) <> i.unit
                                            THEN i.Conversionfactor
                                        ELSE 1
                                    END
                                WHEN d.RequestType = 2 THEN
                                    ISNULL(d.IssuedQty, 0) *
                                    CASE
                                        WHEN d.Unit = i.unit
                                         AND ISNULL(i.Baseunit,i.unit) <> i.unit
                                            THEN i.Conversionfactor
                                        ELSE 1
                                    END
                                ELSE 0
                            END
                        )
                    ) AS RequiredQty,
                    MAX
                    (
                        CASE
                            WHEN ISNULL(i.Baseunit,i.unit) = i.unit THEN 1
                            ELSE i.Conversionfactor
                        END
                    ) AS ConversionFactor
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
                    QOH is stored in Purchase Units. Convert every active rate row
                    to Base Units before checking total available stock.
                */
                UPDATE stock
                SET
                    ActiveRateCount = rate.RateCount,
                    AvailableBaseQty = CONVERT(DECIMAL(18,4), ISNULL(rate.AvailableBaseQty, 0))
                FROM @StockToDeduct AS stock
                OUTER APPLY
                (
                    SELECT
                        COUNT(*) AS RateCount,
                        SUM
                        (
                            CONVERT
                            (
                                DECIMAL(18,4),
                                CASE
                                    WHEN ISNULL(pr.QOH, 0) > 0
                                        THEN ISNULL(pr.QOH, 0) * stock.ConversionFactor
                                    ELSE 0
                                END
                            )
                        ) AS AvailableBaseQty
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
                    WHERE ISNULL(AvailableBaseQty, 0) < RequiredQty
                )
                BEGIN
                    THROW 50210, 'Insufficient total stock across active tblProductRate rows after unit conversion. Approval was cancelled.', 1;
                END;

                /*
                    Allocate the requested Base Units across active price rows
                    in FIFO order (oldest Id first). Convert each row's remaining
                    Base Units back to Purchase Units before saving QOH.
                */
                ;WITH RateRows AS
                (
                    SELECT
                        pr.Id,
                        stock.ConversionFactor,
                        CONVERT
                        (
                            DECIMAL(18,4),
                            CASE
                                WHEN ISNULL(pr.QOH, 0) > 0
                                    THEN ISNULL(pr.QOH, 0) * stock.ConversionFactor
                                ELSE 0
                            END
                        ) AS RowQtyBase,
                        stock.RequiredQty,
                        ISNULL
                        (
                            SUM
                            (
                                CONVERT
                                (
                                    DECIMAL(18,4),
                                    CASE
                                        WHEN ISNULL(pr.QOH, 0) > 0
                                            THEN ISNULL(pr.QOH, 0) * stock.ConversionFactor
                                        ELSE 0
                                    END
                                )
                            ) OVER
                            (
                                PARTITION BY pr.ProductId
                                ORDER BY pr.Id
                                ROWS BETWEEN UNBOUNDED PRECEDING AND 1 PRECEDING
                            ),
                            0
                        ) AS QtyBeforeBase
                    FROM dbo.tblProductRate AS pr WITH (UPDLOCK, HOLDLOCK)
                    INNER JOIN @StockToDeduct AS stock
                        ON stock.ItemId = pr.ProductId
                    WHERE pr.Status = 1
                ),
                RateDeductions AS
                (
                    SELECT
                        Id,
                        ConversionFactor,
                        CASE
                            WHEN RowQtyBase <= 0 OR QtyBeforeBase >= RequiredQty
                                THEN CONVERT(DECIMAL(18,4), 0)
                            WHEN QtyBeforeBase + RowQtyBase <= RequiredQty
                                THEN RowQtyBase
                            ELSE RequiredQty - QtyBeforeBase
                        END AS DeductQtyBase
                    FROM RateRows
                )
                UPDATE pr
                SET pr.QOH =
                    CONVERT
                    (
                        DECIMAL(18,4),
                        (
                            (CONVERT(DECIMAL(18,8), ISNULL(pr.QOH, 0)) *
                             deduction.ConversionFactor) -
                            deduction.DeductQtyBase
                        ) / NULLIF(deduction.ConversionFactor, 0)
                    )
                FROM dbo.tblProductRate AS pr
                INNER JOIN RateDeductions AS deduction
                    ON deduction.Id = pr.Id
                WHERE deduction.DeductQtyBase > 0;
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