USE [FoodBeverage];
GO
/****** Object:  StoredProcedure [dbo].[prdUpdate_InventoryRequestApproval]    Script Date: 09-11-2026 21:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[prdUpdate_InventoryRequestApproval]
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
        RequestType:
            1 = Product request (Batch Preparation)
            2 = Direct raw-material request

        tblIngredientDetails.ProductType:
            1 = Raw material
            2 = Packing item

        Product request handling:
            ProductId + PlannedQty are read from tblInventoryRequestDetails.
            A product can appear in multiple detail rows because its recipe
            ingredients are stored as separate rows. Therefore, distinct
            ProductId + PlannedQty combinations are expanded only once.

        FoodPreparationType (dbo.tblProduct):
            1 = MadeToOrder       : do not deduct in this approval procedure;
                                   deduct all raw ingredients at order time.
            2 = BatchPrepared     : deduct all raw ingredients when staff
                                   approves the batch-preparation request.
            3 = PartiallyPrepared : on approval, deduct ingredients whose
                                   Deductionstage is not 1. Deductionstage=1
                                   is reserved for order-time deduction.
            4 = NotApplicable     : do not deduct raw ingredients.

        Deductionstage (dbo.tblIngredientDetails, for type 3 products):
            0 = Any / Default     : deduct during staff batch preparation.
            1 = Order Preparation : deduct at order time, not on approval.
            2 = Advance Preparation: deduct during staff batch preparation.

            Only ProductType=1 (raw material) recipe rows are deducted here.
            ProductType=2 (packing item) is not deducted by this request.

            Recipe Quantity is stored in the ingredient item's Baseunit.

        Direct raw-material handling:
            IssuedQty is read from tblInventoryRequestDetails and converted
            to Baseunit when the saved Unit is the purchase unit.

        tblProductRate.QOH is stored in purchase units.
        ConversionFactor = number of Baseunits in one purchase unit.
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

        /* =========================================================
           STEP 1: VALIDATE AND LOCK REQUEST MASTER
        ========================================================= */

        IF ISNULL(@InventoryRequestId, 0) <= 0
            THROW 50204, 'A valid inventory request ID is required.', 1;

        IF ISNULL(@EmployeeId, 0) <= 0
            THROW 50205, 'A valid employee ID is required for this action.', 1;

        SELECT
            @CurrentApprovalStatus = ISNULL(ApprovalStatus, 0)
        FROM dbo.tblInventoryRequestMaster WITH (UPDLOCK, HOLDLOCK)
        WHERE InventoryRequestId = @InventoryRequestId
          AND BranchId = @BranchId
          AND ISNULL(Status, 1) = 1;

        IF @CurrentApprovalStatus IS NULL
            THROW 50207, 'The active inventory request was not found for this branch.', 1;


        /* =========================================================
           STEP 2: RESUBMIT SENT-BACK REQUEST
           ApprovalStatus = 0
        ========================================================= */

        IF @ApprovalStatus = 0
        BEGIN
            IF @CurrentApprovalStatus <> 3
                THROW 50201, 'Only an active Sent Back request can be resubmitted.', 1;

            UPDATE dbo.tblInventoryRequestMaster
            SET
                ApprovalStatus = 0,
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

            -- Keep previous ApprovalRemarks so the requester can see the send-back reason.
        END

        /* =========================================================
           STEP 3: APPROVE / REJECT / SEND BACK

           ApprovalStatus:
               1 = Approved
               2 = Rejected
               3 = Sent Back
        ========================================================= */

        ELSE IF @ApprovalStatus IN (1, 2, 3)
        BEGIN
            IF @CurrentApprovalStatus <> 0
                THROW 50202, 'Only an active Pending request can be approved, rejected, or sent back.', 1;

            IF @ApprovalStatus IN (2, 3)
               AND NULLIF(LTRIM(RTRIM(@ApprovalRemarks)), '') IS NULL
            BEGIN
                THROW 50206, 'Remarks are required when rejecting or sending back a request.', 1;
            END;

            /* =====================================================
               STEP 4: STOCK VALIDATION AND DEDUCTION
               Run only when approving.
            ===================================================== */

            IF @ApprovalStatus = 1
            BEGIN
                /* -------------------------------------------------
                   VALIDATION 1: Prevent negative quantities.
                ------------------------------------------------- */

                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND
                      (
                          (d.RequestType = 1 AND ISNULL(d.PlannedQty, 0) < 0)
                          OR
                          (d.RequestType = 2 AND ISNULL(d.IssuedQty, 0) < 0)
                      )
                )
                BEGIN
                    THROW 50208, 'A requested quantity cannot be negative. Stock was not deducted.', 1;
                END;


                /* -------------------------------------------------
                   VALIDATION 2: Product requests need a ProductId.
                ------------------------------------------------- */

                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND d.RequestType = 1
                      AND ISNULL(d.PlannedQty, 0) > 0
                      AND ISNULL(d.ProductId, 0) <= 0
                )
                BEGIN
                    THROW 50213, 'A product request contains an invalid ProductId.', 1;
                END;

                /* -------------------------------------------------
                   VALIDATION 2A: Product must exist, be active, and
                   have a valid FoodPreparationType (1 to 4).
                ------------------------------------------------- */

                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                    LEFT JOIN dbo.tblProduct AS p WITH (UPDLOCK, HOLDLOCK)
                        ON p.ProductId = d.ProductId
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND d.RequestType = 1
                      AND ISNULL(d.PlannedQty, 0) > 0
                      AND
                      (
                          p.ProductId IS NULL
                          OR ISNULL(p.Status, 1) <> 1
                          OR ISNULL(p.FoodPreparationType, 0) NOT IN (1, 2, 3, 4)
                      )
                )
                BEGIN
                    THROW 50219, 'A requested product is missing, inactive, or has an invalid FoodPreparationType. Set FoodPreparationType to 1, 2, 3, or 4.', 1;
                END;

                /* -------------------------------------------------
                   VALIDATION 2B: PartiallyPrepared ingredients must
                   use a supported Deductionstage (0, 1, or 2).
                ------------------------------------------------- */

                IF EXISTS
                (
                    SELECT 1
                    FROM
                    (
                        SELECT DISTINCT
                            d.ProductId,
                            ISNULL(d.VariationId, 0) AS VariationId
                        FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                        WHERE d.InventoryRequestId = @InventoryRequestId
                          AND ISNULL(d.Status, 1) = 1
                          AND d.RequestType = 1
                          AND ISNULL(d.PlannedQty, 0) > 0
                    ) AS requestedProduct
                    INNER JOIN dbo.tblProduct AS p
                        ON p.ProductId = requestedProduct.ProductId
                    INNER JOIN dbo.tblIngredientDetails AS ing WITH (UPDLOCK, HOLDLOCK)
                        ON ing.ProductId = requestedProduct.ProductId
                       AND ISNULL(ing.VariationId, 0) = requestedProduct.VariationId
                       AND ing.ProductType = 1
                       AND ISNULL(ing.Status, 1) = 1
                    WHERE p.FoodPreparationType = 3
                      AND ISNULL(ing.Deductionstage, 0) NOT IN (0, 1, 2)
                )
                BEGIN
                    THROW 50220, 'A PartiallyPrepared product contains an invalid Deductionstage. Use 0 (Any/Default), 1 (Order Preparation), or 2 (Advance Preparation).', 1;
                END;


                /* -------------------------------------------------
                   VALIDATION 3: Each BatchPrepared or
                   PartiallyPrepared product must have at least one
                   active raw-material recipe row. MadeToOrder and
                   NotApplicable products are not batch-deducted.
                   Packing items (ProductType = 2) are ignored.
                ------------------------------------------------- */

                IF EXISTS
                (
                    SELECT 1
                    FROM
                    (
                        SELECT DISTINCT
                            d.ProductId,
                            ISNULL(d.VariationId, 0) AS VariationId,
                            d.PlannedQty
                        FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                        WHERE d.InventoryRequestId = @InventoryRequestId
                          AND ISNULL(d.Status, 1) = 1
                          AND d.RequestType = 1
                          AND ISNULL(d.PlannedQty, 0) > 0
                    ) AS requestedProduct
                    INNER JOIN dbo.tblProduct AS p
                        ON p.ProductId = requestedProduct.ProductId
                    WHERE p.FoodPreparationType IN (2, 3)
                      AND NOT EXISTS
                    (
                        SELECT 1
                        FROM dbo.tblIngredientDetails AS ing WITH (UPDLOCK, HOLDLOCK)
                        WHERE ing.ProductId = requestedProduct.ProductId
                          AND ISNULL(ing.VariationId, 0) = requestedProduct.VariationId
                          AND ing.ProductType = 1
                          AND ISNULL(ing.Status, 1) = 1
                    )
                )
                BEGIN
                    THROW 50214, 'One or more requested products do not have active raw-material ingredients. Approval was cancelled.', 1;
                END;


                /* -------------------------------------------------
                   VALIDATION 4: Recipe raw-material quantities must
                   be positive. Recipe quantity is in Baseunit.
                ------------------------------------------------- */

                IF EXISTS
                (
                    SELECT 1
                    FROM
                    (
                        SELECT DISTINCT
                            d.ProductId,
                            ISNULL(d.VariationId, 0) AS VariationId,
                            d.PlannedQty
                        FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                        WHERE d.InventoryRequestId = @InventoryRequestId
                          AND ISNULL(d.Status, 1) = 1
                          AND d.RequestType = 1
                          AND ISNULL(d.PlannedQty, 0) > 0
                    ) AS requestedProduct
                    INNER JOIN dbo.tblProduct AS p
                        ON p.ProductId = requestedProduct.ProductId
                       AND p.FoodPreparationType IN (2, 3)
                    INNER JOIN dbo.tblIngredientDetails AS ing WITH (UPDLOCK, HOLDLOCK)
                        ON ing.ProductId = requestedProduct.ProductId
                       AND ISNULL(ing.VariationId, 0) = requestedProduct.VariationId
                       AND ing.ProductType = 1
                       AND ISNULL(ing.Status, 1) = 1
                    WHERE ISNULL(ing.Quantity, 0) <= 0
                )
                BEGIN
                    THROW 50215, 'A raw-material recipe contains a zero, negative, or missing ingredient quantity.', 1;
                END;


                /* -------------------------------------------------
                   VALIDATION 5: Recipe ingredients must map to a
                   valid raw-material item with a Baseunit.
                ------------------------------------------------- */

                IF EXISTS
                (
                    SELECT 1
                    FROM
                    (
                        SELECT DISTINCT
                            d.ProductId,
                            ISNULL(d.VariationId, 0) AS VariationId,
                            d.PlannedQty
                        FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                        WHERE d.InventoryRequestId = @InventoryRequestId
                          AND ISNULL(d.Status, 1) = 1
                          AND d.RequestType = 1
                          AND ISNULL(d.PlannedQty, 0) > 0
                    ) AS requestedProduct
                    INNER JOIN dbo.tblProduct AS p
                        ON p.ProductId = requestedProduct.ProductId
                       AND p.FoodPreparationType IN (2, 3)
                    INNER JOIN dbo.tblIngredientDetails AS ing WITH (UPDLOCK, HOLDLOCK)
                        ON ing.ProductId = requestedProduct.ProductId
                       AND ISNULL(ing.VariationId, 0) = requestedProduct.VariationId
                       AND ing.ProductType = 1
                       AND ISNULL(ing.Status, 1) = 1
                    LEFT JOIN dbo.tblItem AS i
                        ON i.ItemId = ing.ItemId
                    WHERE i.ItemId IS NULL
                       OR NULLIF(LTRIM(RTRIM(i.Baseunit)), '') IS NULL
                )
                BEGIN
                    THROW 50216, 'A recipe raw material has an invalid ItemId or missing Baseunit.', 1;
                END;


                /* -------------------------------------------------
                   VALIDATION 6: Direct raw-material request ItemId
                   and Unit must be valid.
                ------------------------------------------------- */

                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                    LEFT JOIN dbo.tblItem AS i
                        ON i.ItemId = d.ItemId
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND d.RequestType = 2
                      AND ISNULL(d.IssuedQty, 0) > 0
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
                    THROW 50211, 'A direct raw-material request has an invalid unit. The unit must match the item purchase unit or Baseunit.', 1;
                END;


                /* -------------------------------------------------
                   VALIDATION 7: Conversionfactor must be positive
                   when a direct request uses Purchase Unit and the
                   Purchase Unit differs from Baseunit.
                ------------------------------------------------- */

                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                    INNER JOIN dbo.tblItem AS i
                        ON i.ItemId = d.ItemId
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND d.RequestType = 2
                      AND d.IssuedQty > 0
                      AND d.Unit = i.unit
                      AND i.Baseunit IS NOT NULL
                      AND i.Baseunit <> i.unit
                      AND ISNULL(i.Conversionfactor, 0) <= 0
                )
                BEGIN
                    THROW 50212, 'The item has an invalid Conversionfactor. Stock was not deducted.', 1;
                END;


                /* -------------------------------------------------
                   VALIDATION 8: Recipe raw materials also need a
                   valid conversion factor for QOH purchase-to-base
                   conversion.
                ------------------------------------------------- */

                IF EXISTS
                (
                    SELECT 1
                    FROM
                    (
                        SELECT DISTINCT
                            d.ProductId,
                            ISNULL(d.VariationId, 0) AS VariationId,
                            d.PlannedQty
                        FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                        WHERE d.InventoryRequestId = @InventoryRequestId
                          AND ISNULL(d.Status, 1) = 1
                          AND d.RequestType = 1
                          AND ISNULL(d.PlannedQty, 0) > 0
                    ) AS requestedProduct
                    INNER JOIN dbo.tblProduct AS p
                        ON p.ProductId = requestedProduct.ProductId
                    INNER JOIN dbo.tblIngredientDetails AS ing WITH (UPDLOCK, HOLDLOCK)
                        ON ing.ProductId = requestedProduct.ProductId
                       AND ISNULL(ing.VariationId, 0) = requestedProduct.VariationId
                       AND ing.ProductType = 1
                       AND ISNULL(ing.Status, 1) = 1
                    INNER JOIN dbo.tblItem AS i
                        ON i.ItemId = ing.ItemId
                    WHERE
                      (
                          p.FoodPreparationType = 2
                          OR
                          (
                              p.FoodPreparationType = 3
                              AND ISNULL(ing.Deductionstage, 0) <> 1
                          )
                      )
                      AND i.Baseunit <> i.unit
                      AND ISNULL(i.Conversionfactor, 0) <= 0
                )
                BEGIN
                    THROW 50217, 'A recipe raw material has an invalid Conversionfactor.', 1;
                END;


                /* =================================================
                   STEP 5: BUILD REQUIRED RAW-MATERIAL QUANTITIES

                   RequestType = 1:
                       Only FoodPreparationType 2 and 3 products are
                       considered for staff batch-preparation deduction.
                       Type 2 includes all ProductType=1 ingredients.
                       Type 3 includes Deductionstage 0 and 2 only;
                       Deductionstage 1 is left for order-time deduction.
                       Types 1 and 4 are not deducted here.
                       Required Base Qty = PlannedQty * Recipe Quantity.

                   RequestType = 2:
                       Direct raw material uses IssuedQty regardless of
                       product FoodPreparationType. Purchase Unit quantities
                       are converted to Baseunit.

                   Both sources are combined by ItemId, so the same
                   raw material is deducted once for the combined total.
                ================================================= */

                ;WITH ProductRequests AS
                (
                    /* Expand each ProductId + PlannedQty combination once. */
                    SELECT DISTINCT
                        d.ProductId,
                        ISNULL(d.VariationId, 0) AS VariationId,
                        CONVERT(DECIMAL(18,4), d.PlannedQty) AS PlannedQty,
                        p.FoodPreparationType
                    FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                    INNER JOIN dbo.tblProduct AS p
                        ON p.ProductId = d.ProductId
                       AND ISNULL(p.Status, 1) = 1
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND d.RequestType = 1
                      AND ISNULL(d.PlannedQty, 0) > 0
                      AND p.FoodPreparationType IN (2, 3)
                ),
                RawMaterialRequirements AS
                (
                    /*
                       BatchPrepared (2): deduct every raw ingredient now.
                       PartiallyPrepared (3): deduct stage 0 (Any/Default)
                       and stage 2 (Advance Preparation) now. Stage 1 is
                       reserved for the order-time sales procedure.
                    */
                    SELECT
                        ing.ItemId,
                        CONVERT
                        (
                            DECIMAL(18,4),
                            requestedProduct.PlannedQty * ing.Quantity
                        ) AS RequiredBaseQty
                    FROM ProductRequests AS requestedProduct
                    INNER JOIN dbo.tblIngredientDetails AS ing WITH (UPDLOCK, HOLDLOCK)
                        ON ing.ProductId = requestedProduct.ProductId
                       AND ISNULL(ing.VariationId, 0) = requestedProduct.VariationId
                       AND ing.ProductType = 1
                       AND ISNULL(ing.Status, 1) = 1
                       AND
                       (
                           requestedProduct.FoodPreparationType = 2
                           OR
                           (
                               requestedProduct.FoodPreparationType = 3
                               AND ISNULL(ing.Deductionstage, 0) <> 1
                           )
                       )

                    UNION ALL

                    /* Direct raw-material request. */
                    SELECT
                        d.ItemId,
                        CONVERT
                        (
                            DECIMAL(18,4),
                            d.IssuedQty *
                            CASE
                                WHEN d.Unit = i.unit
                                 AND ISNULL(i.Baseunit, i.unit) <> i.unit
                                    THEN i.Conversionfactor
                                ELSE 1
                            END
                        ) AS RequiredBaseQty
                    FROM dbo.tblInventoryRequestDetails AS d WITH (UPDLOCK, HOLDLOCK)
                    INNER JOIN dbo.tblItem AS i
                        ON i.ItemId = d.ItemId
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND d.RequestType = 2
                      AND ISNULL(d.IssuedQty, 0) > 0
                )
                INSERT INTO @StockToDeduct
                (
                    ItemId,
                    RequiredQty,
                    ConversionFactor
                )
                SELECT
                    req.ItemId,
                    CONVERT(DECIMAL(18,4), SUM(req.RequiredBaseQty)) AS RequiredQty,
                    CONVERT
                    (
                        DECIMAL(18,4),
                        MAX
                        (
                            CASE
                                WHEN ISNULL(i.Baseunit, i.unit) = i.unit
                                    THEN 1
                                ELSE i.Conversionfactor
                            END
                        )
                    ) AS ConversionFactor
                FROM RawMaterialRequirements AS req
                INNER JOIN dbo.tblItem AS i
                    ON i.ItemId = req.ItemId
                GROUP BY req.ItemId
                HAVING SUM(req.RequiredBaseQty) > 0;


                /* =================================================
                   STEP 6: CALCULATE AVAILABLE STOCK IN BASE UNITS

                   Available Base Qty = SUM(Active QOH) * ConversionFactor
                ================================================= */

                UPDATE stock
                SET
                    ActiveRateCount = rate.RateCount,
                    AvailableBaseQty = CONVERT
                    (
                        DECIMAL(18,4),
                        ISNULL(rate.AvailablePurchaseQty, 0) * stock.ConversionFactor
                    )
                FROM @StockToDeduct AS stock
                OUTER APPLY
                (
                    SELECT
                        COUNT(*) AS RateCount,
                        SUM
                        (
                            CASE
                                WHEN ISNULL(pr.QOH, 0) > 0
                                    THEN CONVERT(DECIMAL(18,4), pr.QOH)
                                ELSE CONVERT(DECIMAL(18,4), 0)
                            END
                        ) AS AvailablePurchaseQty
                    FROM dbo.tblProductRate AS pr WITH (UPDLOCK, HOLDLOCK)
                    WHERE pr.ProductId = stock.ItemId
                      AND pr.Status = 1
                ) AS rate;


                /* -------------------------------------------------
                   VALIDATION 9: Active stock records must exist.
                ------------------------------------------------- */

                IF EXISTS
                (
                    SELECT 1
                    FROM @StockToDeduct
                    WHERE ISNULL(ActiveRateCount, 0) = 0
                )
                BEGIN
                    THROW 50209, 'No active stock records were found in tblProductRate for one or more raw materials. Approval was cancelled.', 1;
                END;


                /* -------------------------------------------------
                   VALIDATION 10: Sufficient stock must be available.
                ------------------------------------------------- */

                IF EXISTS
                (
                    SELECT 1
                    FROM @StockToDeduct
                    WHERE ISNULL(AvailableBaseQty, 0) < RequiredQty
                )
                BEGIN
                    THROW 50210, 'Insufficient total stock across active tblProductRate rows after unit conversion. Approval was cancelled.', 1;
                END;


                /* =================================================
                   STEP 7: FIFO STOCK DEDUCTION

                   Deduct oldest tblProductRate.Id first.
                   QOH remains stored in Purchase Units.
                ================================================= */

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
                            WHEN RowQtyBase <= 0
                              OR QtyBeforeBase >= RequiredQty
                                THEN CONVERT(DECIMAL(18,4), 0)
                            WHEN QtyBeforeBase + RowQtyBase <= RequiredQty
                                THEN RowQtyBase
                            ELSE RequiredQty - QtyBeforeBase
                        END AS DeductQtyBase
                    FROM RateRows
                )
                UPDATE pr
                SET pr.QOH = CONVERT
                (
                    DECIMAL(18,4),
                    (
                        (
                            CONVERT(DECIMAL(18,8), ISNULL(pr.QOH, 0))
                            * deduction.ConversionFactor
                        )
                        - deduction.DeductQtyBase
                    ) / NULLIF(deduction.ConversionFactor, 0)
                )
                FROM dbo.tblProductRate AS pr
                INNER JOIN RateDeductions AS deduction
                    ON deduction.Id = pr.Id
                WHERE deduction.DeductQtyBase > 0;

            END;


            /* =====================================================
               STEP 8: UPDATE APPROVAL STATUS
               Stock deduction and approval status are atomic.
            ===================================================== */

            UPDATE dbo.tblInventoryRequestMaster
            SET
                ApprovalStatus = @ApprovalStatus,
                ApprovedBy = @EmployeeId,
                ApprovedOn = GETDATE(),
                ApprovalRemarks = ISNULL
                (
                    NULLIF(LTRIM(RTRIM(@ApprovalRemarks)), ''),
                    ''
                ),
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

GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

ALTER PROCEDURE [dbo].[prdInsert_Upd_SalesDetails]
(
    @SalesDetailsId      INT,
    @SalesMasterId       INT,
    @ItemId              INT,
    @MId                 INT,
    @UnitId              INT,
    @Quantity            DECIMAL(18,2),
    @SalesRate           DECIMAL(18,2),
    @MRP                 DECIMAL(18,2),
    @Amount              DECIMAL(18,2),
    @DiscountPer         DECIMAL(18,2),
    @DiscountAmount      DECIMAL(18,2),
    @IGSTAmount          DECIMAL(18,2),
    @CGSTAmount          DECIMAL(18,2),
    @SGSTAmount          DECIMAL(18,2),
    @TaxPer              DECIMAL(18,2),
    @TaxAmount           DECIMAL(18,2),
    @NetAmount           DECIMAL(18,2),
    @BranchId            BIGINT,
    @TaxType             VARCHAR(100),
    @Tax                 VARCHAR(50),
    @CreatedBy           BIGINT,
    @Status              INT,
    @ProductType         INT,
    @VatAmount           DECIMAL(18,2),
    @CusId               INT,
    @ItemName            NVARCHAR(100),
    @ItemBarcode         VARCHAR(MAX),
    @Prid                INT,
    @MarketPrice         DECIMAL(18,2),
    @TableType            INT,
    @Pavid               INT,
    @AddonStatus         INT,
    @AV_id               VARCHAR(MAX),
    @Addoncount          INT,
    @Cmid                VARCHAR(MAX),
    @VariatiionName      VARCHAR(MAX),
    @OrderDetailsId      VARCHAR(MAX),
    @FoodPreparationType INT,
    @PreTaxAmt           DECIMAL(18,2),
    @OrderTypeId         INT = 0,
    @VariationId         BIGINT = 0
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Id BIGINT = NULL;
    DECLARE @Count INT = 0;
    DECLARE @DiscAmt DECIMAL(18,2);
    DECLARE @DeductStock BIT = 0;
    DECLARE @SaleQtyForStock DECIMAL(18,4) = CONVERT(DECIMAL(18,4), ISNULL(@Quantity, 0));

    /*
        FoodPreparationType:
            1 = MadeToOrder       : deduct raw-material recipe stock at sale
            2 = BatchPrepared     : raw materials were deducted during batch prep
            3 = PartiallyPrepared : deduct raw material rows with Deductionstage = 1
            4 = NotApplicable     : no raw-material recipe deduction

        tblIngredientDetails.ProductType:
            1 = Raw material
            2 = Packaging material

        OrderTypeId:
            1 = Pickup, 2 = Takeaway, 3 = Delivery
            Packaging material is deducted only for Pickup and Delivery.

        STOCK ACCOUNTING (same model as prdUpdate_InventoryRequestApproval):
            - tblIngredientDetails.Quantity is treated as Baseunit quantity.
            - tblItem.Conversionfactor is the number of Baseunits in one purchase unit.
            - tblProductRate.QOH is stored in purchase units.
            - SaleType = 0 is checked on tblItem.
            - Stock is deducted from active tblProductRate rows (Status = 1)
              in FIFO order by tblProductRate.Id.
    */

    DECLARE @ProductPreparation TABLE
    (
        ProductId INT NOT NULL,
        VariationId BIGINT NOT NULL,
        SaleQuantity DECIMAL(18,4) NOT NULL,
        FoodPreparationType INT NOT NULL
    );

    DECLARE @RequiredStock TABLE
    (
        ItemId BIGINT NOT NULL PRIMARY KEY,
        RequiredBaseQty DECIMAL(18,4) NOT NULL
    );

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

        /* =========================================================
           STEP 1: SAVE SALES DETAILS
        ========================================================= */

        IF ISNULL(@Quantity, 0) <= 0
            THROW 50301, 'Sales quantity must be greater than zero.', 1;

        IF @SalesDetailsId > 0
        BEGIN
            UPDATE dbo.tblSalesDetails
            SET
                SalesMasterId    = @SalesMasterId,
                MId              = @MId,
                UnitId           = @UnitId,
                Quantity         = @Quantity,
                SalesRate        = @SalesRate,
                MRP              = @MRP,
                Amount           = @Amount,
                DiscountPer      = @DiscountPer,
                DiscountAmount   = @DiscountAmount,
                IGSTAmount       = @IGSTAmount,
                CGSTAmount       = @CGSTAmount,
                SGSTAmount       = @SGSTAmount,
                TaxPer           = @TaxPer,
                TaxAmount        = @TaxAmount,
                NetAmount        = @NetAmount,
                BranchId         = @BranchId,
                TaxType          = @TaxType,
                Tax              = @Tax,
                ModifiedBy       = @CreatedBy,
                ModifiedDateTime = SYSDATETIME(),
                Status           = @Status,
                ProductType      = @ProductType,
                VatAmount        = @VatAmount,
                CusId            = @CusId,
                ItemName         = @ItemName,
                ItemId           = @ItemId,
                VariationId      = ISNULL(@VariationId, 0),
                ItemBarcode      = @ItemBarcode,
                Prid             = @Prid,
                MarketPrice      = @MarketPrice,
                TableType        = @TableType,
                Pavid             = @Pavid,
                AddonStatus      = @AddonStatus,
                AV_id            = @AV_id,
                Addoncount       = @Addoncount,
                Cmid             = @Cmid,
                PreTaxAmt        = @PreTaxAmt
            WHERE SalesDetailsId = @SalesDetailsId;

            IF @@ROWCOUNT = 0
                THROW 50302, 'The sales detail row was not found for update.', 1;

            SET @Id = @SalesDetailsId;
            SET @DeductStock = 1;
        END
        ELSE
        BEGIN
            IF @TableType > 0
            BEGIN
                IF @AddonStatus IN (0,1)
                BEGIN
                    SELECT @Count = COUNT(*)
                    FROM dbo.tblSalesDetails WITH (UPDLOCK, HOLDLOCK)
                    WHERE Cmid = @Cmid
                      AND TableType = @TableType
                      AND SalesMasterId = @SalesMasterId
                      AND ISNULL(VariationId, 0) = ISNULL(@VariationId, 0)
                      AND AddonStatus IN (0,1);
                END
                ELSE IF @AddonStatus = 2
                BEGIN
                    SELECT @Count = COUNT(*)
                    FROM dbo.tblSalesDetails WITH (UPDLOCK, HOLDLOCK)
                    WHERE Cmid = @Cmid
                      AND Pavid = @Pavid
                      AND AddonStatus = 2
                      AND TableType = @TableType
                      AND SalesMasterId = @SalesMasterId;
                END
            END;

            /* Existing combo/add-on row: this call adds @Quantity. */
            IF @Count > 0
            BEGIN
                IF @AddonStatus IN (0,1)
                BEGIN
                    UPDATE dbo.tblSalesDetails
                    SET
                        Quantity        = Quantity + @Quantity,
                        NetAmount       = NetAmount + (MarketPrice * @Quantity),
                        DiscountAmount  = DiscountAmount + @DiscountAmount,
                        Amount          = Amount + @Amount,
                        IGSTAmount      = IGSTAmount + @IGSTAmount,
                        CGSTAmount      = CGSTAmount + @CGSTAmount,
                        SGSTAmount      = SGSTAmount + @SGSTAmount,
                        TaxAmount       = TaxAmount + @TaxAmount
                    WHERE Cmid = @Cmid
                      AND TableType = @TableType
                      AND SalesMasterId = @SalesMasterId
                      AND ISNULL(VariationId, 0) = ISNULL(@VariationId, 0)
                      AND AddonStatus IN (0,1);
                END
                ELSE IF @AddonStatus = 2
                BEGIN
                    UPDATE dbo.tblSalesDetails
                    SET
                        Quantity        = Quantity + @Quantity,
                        DiscountAmount  = DiscountAmount + @DiscountAmount,
                        Amount          = Amount + @Amount,
                        IGSTAmount      = IGSTAmount + @IGSTAmount,
                        CGSTAmount      = CGSTAmount + @CGSTAmount,
                        SGSTAmount      = SGSTAmount + @SGSTAmount,
                        TaxAmount       = TaxAmount + @TaxAmount
                    WHERE Cmid = @Cmid
                      AND Pavid = @Pavid
                      AND AddonStatus = 2
                      AND TableType = @TableType
                      AND SalesMasterId = @SalesMasterId;
                END;

                SELECT TOP (1) @Id = SalesDetailsId
                FROM dbo.tblSalesDetails
                WHERE SalesMasterId = @SalesMasterId
                  AND Cmid = @Cmid
                  AND TableType = @TableType
                  AND ISNULL(VariationId, 0) = ISNULL(@VariationId, 0)
                ORDER BY SalesDetailsId DESC;

                SET @DeductStock = 1;
            END
            ELSE
            BEGIN
                INSERT INTO dbo.tblSalesDetails
                (
                    SalesMasterId, MId, UnitId, Quantity, SalesRate, MRP, Amount,
                    DiscountPer, DiscountAmount, IGSTAmount, CGSTAmount, SGSTAmount,
                    TaxPer, TaxAmount, NetAmount, BranchId, TaxType, Tax,
                    CreatedBy, CreatedDateTime, Status, ProductType, VatAmount,
                    CusId, ItemName, ItemId, ItemBarcode, Prid, MarketPrice,
                    TableType, Pavid, AddonStatus, AV_id, Addoncount, Cmid,
                    VariatiionName, PreTaxAmt, VariationId
                )
                VALUES
                (
                    @SalesMasterId, @MId, @UnitId, @Quantity, @SalesRate, @MRP, @Amount,
                    @DiscountPer, @DiscountAmount, @IGSTAmount, @CGSTAmount, @SGSTAmount,
                    @TaxPer, @TaxAmount, @NetAmount, @BranchId, @TaxType, @Tax,
                    @CreatedBy, SYSDATETIME(), @Status, @ProductType, @VatAmount,
                    @CusId, @ItemName, @ItemId, @ItemBarcode, @Prid, @MarketPrice,
                    @TableType, @Pavid, @AddonStatus, @AV_id, @Addoncount, @Cmid,
                    @VariatiionName, @PreTaxAmt, ISNULL(@VariationId, 0)
                );

                SET @Id = SCOPE_IDENTITY();

                UPDATE dbo.tblOrderDetails
                SET SalesDetailsId = @Id
                WHERE OrderDetailsId IN
                (
                    SELECT TRY_CAST(value AS INT)
                    FROM STRING_SPLIT(@OrderDetailsId, ',')
                    WHERE TRY_CAST(value AS INT) IS NOT NULL
                );

                SET @DeductStock = 1;
            END;
        END;

        /* =========================================================
           STEP 2: PREPARE PRODUCT(S) FOR INGREDIENT DEDUCTION
           Deduct @Quantity for this procedure call.
        ========================================================= */

        IF @DeductStock = 1 AND @SaleQtyForStock > 0
        BEGIN
            IF @TableType = 3
            BEGIN
                /* Combo: each component uses its own FoodPreparationType. */
                INSERT INTO @ProductPreparation
                (
                    ProductId,
                    VariationId,
                    SaleQuantity,
                    FoodPreparationType
                )
                SELECT
                    M.ProductId,
                    CONVERT(BIGINT, 0),
                    @SaleQtyForStock,
                    CASE
                        WHEN P.FoodPreparationType IN (1,2,3,4)
                            THEN P.FoodPreparationType
                        WHEN @FoodPreparationType IN (1,2,3,4)
                            THEN @FoodPreparationType
                        ELSE 4
                    END
                FROM dbo.tblOfferDetails AS O
                INNER JOIN dbo.tblMenu AS M
                    ON M.ProductId = O.ProductId
                LEFT JOIN dbo.tblProduct AS P
                    ON P.ProductId = M.ProductId
                WHERE O.Omid = @ItemId;

                IF NOT EXISTS (SELECT 1 FROM @ProductPreparation)
                    THROW 50303, 'No component products were found for this combo. Stock was not deducted.', 1;
            END
            ELSE
            BEGIN
                /* Normal product: database value is authoritative. */
                SELECT
                    @FoodPreparationType =
                        CASE
                            WHEN P.FoodPreparationType IN (1,2,3,4)
                                THEN P.FoodPreparationType
                            WHEN @FoodPreparationType IN (1,2,3,4)
                                THEN @FoodPreparationType
                            ELSE 4
                        END
                FROM dbo.tblProduct AS P
                WHERE P.ProductId = @ItemId;

                SET @FoodPreparationType =
                    CASE
                        WHEN @FoodPreparationType IN (1,2,3,4)
                            THEN @FoodPreparationType
                        ELSE 4
                    END;

                INSERT INTO @ProductPreparation
                (
                    ProductId,
                    VariationId,
                    SaleQuantity,
                    FoodPreparationType
                )
                VALUES
                (
                    @ItemId,
                    ISNULL(@VariationId, 0),
                    @SaleQtyForStock,
                    @FoodPreparationType
                );
            END;

            /* Validate recipe rows that must be deducted. */
            IF EXISTS
            (
                SELECT 1
                FROM @ProductPreparation AS P
                WHERE P.FoodPreparationType IN (1,3)
                  AND NOT EXISTS
                  (
                      SELECT 1
                      FROM dbo.tblIngredientDetails AS D
                      WHERE D.ProductId = P.ProductId
                        AND ISNULL(D.VariationId, 0) = P.VariationId
                        AND D.ProductType = 1
                        AND ISNULL(D.Status, 1) = 1
                        AND
                        (
                            P.FoodPreparationType = 1
                            OR (P.FoodPreparationType = 3 AND ISNULL(D.Deductionstage, 0) = 1)
                        )
                  )
            )
                THROW 50304, 'A MadeToOrder or PartiallyPrepared product has no active raw-material recipe rows for the required deduction stage.', 1;

            /* Every selected recipe row must have a positive quantity. */
            IF EXISTS
            (
                SELECT 1
                FROM @ProductPreparation AS P
                INNER JOIN dbo.tblIngredientDetails AS D
                    ON D.ProductId = P.ProductId
               AND ISNULL(D.VariationId, 0) = P.VariationId
                   AND ISNULL(D.Status, 1) = 1
                WHERE
                    (
                        (
                            D.ProductType = 1
                            AND
                            (
                                P.FoodPreparationType = 1
                                OR
                                (P.FoodPreparationType = 3 AND ISNULL(D.Deductionstage, 0) = 1)
                            )
                        )
                        OR
                        (D.ProductType = 2 AND @OrderTypeId IN (1,3))
                    )
                    AND ISNULL(D.Quantity, 0) <= 0
            )
                THROW 50305, 'An ingredient or packaging recipe quantity is zero, negative, or missing. Stock was not deducted.', 1;

            /* Selected ingredients must point to a valid item with Baseunit. */
            IF EXISTS
            (
                SELECT 1
                FROM @ProductPreparation AS P
                INNER JOIN dbo.tblIngredientDetails AS D
                    ON D.ProductId = P.ProductId
                   AND ISNULL(D.VariationId, 0) = P.VariationId
                   AND ISNULL(D.Status, 1) = 1
                LEFT JOIN dbo.tblItem AS I
                    ON I.ItemId = D.ItemId
                WHERE
                    (
                        (
                            D.ProductType = 1
                            AND
                            (
                                P.FoodPreparationType = 1
                                OR
                                (P.FoodPreparationType = 3 AND ISNULL(D.Deductionstage, 0) = 1)
                            )
                        )
                        OR
                        (D.ProductType = 2 AND @OrderTypeId IN (1,3))
                    )
                    AND
                    (
                        I.ItemId IS NULL
                        OR ISNULL(I.Status, 0) <> 1
                        OR NULLIF(LTRIM(RTRIM(I.Baseunit)), '') IS NULL
                    )
            )
                THROW 50306, 'A recipe ingredient has an invalid ItemId or missing Baseunit.', 1;

            /* A conversion factor is required when purchase unit differs from Baseunit. */
            IF EXISTS
            (
                SELECT 1
                FROM @ProductPreparation AS P
                INNER JOIN dbo.tblIngredientDetails AS D
                    ON D.ProductId = P.ProductId
                   AND ISNULL(D.VariationId, 0) = P.VariationId
                   AND ISNULL(D.Status, 1) = 1
                INNER JOIN dbo.tblItem AS I
                    ON I.ItemId = D.ItemId
                WHERE
                    (
                        (
                            D.ProductType = 1
                            AND
                            (
                                P.FoodPreparationType = 1
                                OR
                                (P.FoodPreparationType = 3 AND ISNULL(D.Deductionstage, 0) = 1)
                            )
                        )
                        OR
                        (D.ProductType = 2 AND @OrderTypeId IN (1,3))
                    )
                    AND I.Baseunit <> I.Unit
                    AND ISNULL(I.Conversionfactor, 0) <= 0
            )
                THROW 50307, 'A recipe ingredient has an invalid Conversionfactor. Stock was not deducted.', 1;

            /* =====================================================
               STEP 3: BUILD REQUIRED STOCK IN BASE UNITS
               Recipe quantity is assumed to be stored in Baseunit.
            ===================================================== */

            INSERT INTO @RequiredStock (ItemId, RequiredBaseQty)
            SELECT
                D.ItemId,
                CONVERT
                (
                    DECIMAL(18,4),
                    SUM(D.Quantity * P.SaleQuantity)
                ) AS RequiredBaseQty
            FROM @ProductPreparation AS P
            INNER JOIN dbo.tblIngredientDetails AS D
                ON D.ProductId = P.ProductId
               AND ISNULL(D.VariationId, 0) = P.VariationId
               AND ISNULL(D.Status, 1) = 1
            WHERE
                (
                    D.ProductType = 1
                    AND
                    (
                        P.FoodPreparationType = 1
                        OR
                        (P.FoodPreparationType = 3 AND ISNULL(D.Deductionstage, 0) = 1)
                    )
                )
                OR
                (D.ProductType = 2 AND @OrderTypeId IN (1,3))
            GROUP BY D.ItemId
            HAVING SUM(D.Quantity * P.SaleQuantity) > 0;

            /* Convert required base quantities to purchase-unit stock rows. */
            INSERT INTO @StockToDeduct
            (
                ItemId,
                RequiredQty,
                ConversionFactor
            )
            SELECT
                R.ItemId,
                R.RequiredBaseQty,
                CONVERT
                (
                    DECIMAL(18,4),
                    CASE
                        WHEN I.Baseunit = I.Unit THEN 1
                        ELSE I.Conversionfactor
                    END
                ) AS ConversionFactor
            FROM @RequiredStock AS R
            INNER JOIN dbo.tblItem AS I
                ON I.ItemId = R.ItemId
               AND I.Status = 1
               AND I.SaleType = 0;

            IF EXISTS
            (
                SELECT 1
                FROM @StockToDeduct
                WHERE ISNULL(ConversionFactor, 0) <= 0
            )
                THROW 50308, 'Invalid item conversion factor. Stock deduction was cancelled.', 1;

            /* =====================================================
               STEP 4: CALCULATE AVAILABLE BASE STOCK
               SaleType = 0 was validated on tblItem above.
               tblProductRate contains active QOH rows (Status = 1).
            ===================================================== */

            UPDATE S
            SET
                ActiveRateCount = R.RateCount,
                AvailableBaseQty = CONVERT
                (
                    DECIMAL(18,4),
                    ISNULL(R.AvailablePurchaseQty, 0) * S.ConversionFactor
                )
            FROM @StockToDeduct AS S
            OUTER APPLY
            (
                SELECT
                    COUNT(*) AS RateCount,
                    SUM
                    (
                        CASE
                            WHEN ISNULL(PR.QOH, 0) > 0
                                THEN CONVERT(DECIMAL(18,4), PR.QOH)
                            ELSE CONVERT(DECIMAL(18,4), 0)
                        END
                    ) AS AvailablePurchaseQty
                FROM dbo.tblProductRate AS PR WITH (UPDLOCK, HOLDLOCK)
                WHERE PR.ProductId = S.ItemId
                  AND PR.Status = 1
            ) AS R;

            IF EXISTS
            (
                SELECT 1
                FROM @StockToDeduct
                WHERE ISNULL(ActiveRateCount, 0) = 0
            )
                THROW 50309, 'No active tblProductRate stock records were found for one or more SaleType = 0 items.', 1;

            IF EXISTS
            (
                SELECT 1
                FROM @StockToDeduct
                WHERE ISNULL(AvailableBaseQty, 0) < RequiredQty
            )
                THROW 50310, 'Insufficient stock in tblProductRate after Baseunit conversion. Sale was cancelled.', 1;

            /* =====================================================
               STEP 5: FIFO STOCK DEDUCTION
               Oldest tblProductRate.Id is deducted first.
               QOH remains stored in purchase units.
            ===================================================== */

            ;WITH RateRows AS
            (
                SELECT
                    PR.Id,
                    PR.ProductId,
                    S.ConversionFactor,
                    CONVERT
                    (
                        DECIMAL(18,4),
                        CASE
                            WHEN ISNULL(PR.QOH, 0) > 0
                                THEN PR.QOH * S.ConversionFactor
                            ELSE 0
                        END
                    ) AS RowQtyBase,
                    S.RequiredQty,
                    ISNULL
                    (
                        SUM
                        (
                            CONVERT
                            (
                                DECIMAL(18,4),
                                CASE
                                    WHEN ISNULL(PR.QOH, 0) > 0
                                        THEN PR.QOH * S.ConversionFactor
                                    ELSE 0
                                END
                            )
                        ) OVER
                        (
                            PARTITION BY PR.ProductId
                            ORDER BY PR.Id
                            ROWS BETWEEN UNBOUNDED PRECEDING AND 1 PRECEDING
                        ),
                        0
                    ) AS QtyBeforeBase
                FROM dbo.tblProductRate AS PR WITH (UPDLOCK, HOLDLOCK)
                INNER JOIN @StockToDeduct AS S
                    ON S.ItemId = PR.ProductId
                WHERE PR.Status = 1
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
            UPDATE PR
            SET PR.QOH = CONVERT
            (
                DECIMAL(18,4),
                (
                    (CONVERT(DECIMAL(18,8), ISNULL(PR.QOH, 0)) * D.ConversionFactor)
                    - D.DeductQtyBase
                ) / NULLIF(D.ConversionFactor, 0)
            )
            FROM dbo.tblProductRate AS PR
            INNER JOIN RateDeductions AS D
                ON D.Id = PR.Id
            WHERE D.DeductQtyBase > 0;
        END;

        /* =========================================================
           STEP 6: UPDATE SALES MASTER DISCOUNT
        ========================================================= */

        SELECT @DiscAmt = ISNULL(SUM(DiscountAmount), 0)
        FROM dbo.tblSalesDetails
        WHERE SalesMasterId = @SalesMasterId;

        UPDATE dbo.tblSalesMaster
        SET TotalDiscount = @DiscAmt
        WHERE SalesMasterId = @SalesMasterId;

        COMMIT TRANSACTION;

        SELECT @Id AS SalesDetailsId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;
GO
