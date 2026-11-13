USE [FoodBeverage]
GO

IF COL_LENGTH(N'dbo.tblSalesDetails', N'TotalPackingPrice') IS NULL
    ALTER TABLE dbo.tblSalesDetails ADD TotalPackingPrice DECIMAL(18,2) NOT NULL
        CONSTRAINT DF_tblSalesDetails_TotalPackingPrice DEFAULT (0);
GO
IF COL_LENGTH(N'dbo.tblSalesDetails', N'TotalPackingTax') IS NULL
    ALTER TABLE dbo.tblSalesDetails ADD TotalPackingTax DECIMAL(18,2) NOT NULL
        CONSTRAINT DF_tblSalesDetails_TotalPackingTax DEFAULT (0);
GO
IF COL_LENGTH(N'dbo.tblSalesDetails', N'VariationId') IS NULL
    ALTER TABLE dbo.tblSalesDetails ADD VariationId INT NOT NULL
        CONSTRAINT DF_tblSalesDetails_VariationId DEFAULT (0) WITH VALUES;
GO

/****** Object:  StoredProcedure [dbo].[prdInsert_Upd_SalesDetails]    Script Date: 09-11-2026 21:03:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
    @TotalPackingPrice   DECIMAL(18,2) = 0,
    @TotalPackingTax     DECIMAL(18,2) = 0,
    @OrderTypeId         INT = 0,
     @VariationId         INT = 0
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
    DECLARE @PreviousQuantity DECIMAL(18,4) = 0;
    DECLARE @PreviousItemId INT = NULL;
    DECLARE @PreviousTableType INT = NULL;
    DECLARE @IsStockRestock BIT = 0;
    DECLARE @PackagingRequired BIT = 0;
    DECLARE @DatabaseFoodPreparationType INT = NULL;
    DECLARE @DatabaseProductName NVARCHAR(100) = NULL;
    DECLARE @SelectedVariationId INT = ISNULL(@VariationId, 0);

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
            1 = Pickup, 2 = Takeaway, 3 = Delivery; Table order uses 0.
            Packaging material is deducted only for Pickup (OrderTypeId=1) and only when tblProduct.PackagingRequired=1.

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
        VariationId INT NOT NULL,
        SaleQuantity DECIMAL(18,4) NOT NULL,
        FoodPreparationType INT NOT NULL,
        PackagingRequired BIT NOT NULL
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
            /*
               Existing sales detail rows store the final quantity, unlike a new
               combo/add-on call which adds @Quantity. Reconcile only the delta.
               Product identity must remain the same for a quantity-only edit so
               the original recipe is not accidentally replaced during restock.
            */
            SELECT
                @PreviousQuantity = CONVERT(DECIMAL(18,4), ISNULL(Quantity, 0)),
                @PreviousItemId = ItemId,
                @PreviousTableType = TableType
            FROM dbo.tblSalesDetails WITH (UPDLOCK, HOLDLOCK)
            WHERE SalesDetailsId = @SalesDetailsId;

            IF @@ROWCOUNT = 0
                THROW 50302, 'The sales detail row was not found for update.', 1;

            IF ISNULL(@PreviousItemId, -1) <> ISNULL(@ItemId, -1)
               OR ISNULL(@PreviousTableType, -1) <> ISNULL(@TableType, -1)
                THROW 50313, 'A saved sales detail cannot change its product during quantity reconciliation. Remove the old row and add the new product instead.', 1;

            SET @SaleQtyForStock = ABS(CONVERT(DECIMAL(18,4), @Quantity) - @PreviousQuantity);
            SET @IsStockRestock = CASE WHEN CONVERT(DECIMAL(18,4), @Quantity) < @PreviousQuantity THEN 1 ELSE 0 END;

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
                ItemBarcode      = @ItemBarcode,
                Prid             = @Prid,
                MarketPrice      = @MarketPrice,
                TableType        = @TableType,
                Pavid             = @Pavid,
                AddonStatus      = @AddonStatus,
                AV_id            = @AV_id,
                Addoncount       = @Addoncount,
                Cmid             = @Cmid,
                PreTaxAmt        = @PreTaxAmt,
                TotalPackingPrice = @TotalPackingPrice,
                TotalPackingTax   = @TotalPackingTax,
                VariationId       = CASE WHEN ISNULL(@VariationId, 0) > 0
                                          THEN @VariationId ELSE ISNULL(VariationId, 0) END
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
                        TaxAmount       = TaxAmount + @TaxAmount,
                        TotalPackingPrice = ISNULL(TotalPackingPrice,0) + @TotalPackingPrice,
                        TotalPackingTax   = ISNULL(TotalPackingTax,0) + @TotalPackingTax
                    WHERE Cmid = @Cmid
                      AND TableType = @TableType
                      AND SalesMasterId = @SalesMasterId
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
                        TaxAmount       = TaxAmount + @TaxAmount,
                        TotalPackingPrice = ISNULL(TotalPackingPrice,0) + @TotalPackingPrice,
                        TotalPackingTax   = ISNULL(TotalPackingTax,0) + @TotalPackingTax
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
                    VariatiionName, PreTaxAmt, TotalPackingPrice, TotalPackingTax, VariationId
                )
                VALUES
                (
                    @SalesMasterId, @MId, @UnitId, @Quantity, @SalesRate, @MRP, @Amount,
                    @DiscountPer, @DiscountAmount, @IGSTAmount, @CGSTAmount, @SGSTAmount,
                    @TaxPer, @TaxAmount, @NetAmount, @BranchId, @TaxType, @Tax,
                    @CreatedBy, SYSDATETIME(), @Status, @ProductType, @VatAmount,
                    @CusId, @ItemName, @ItemId, @ItemBarcode, @Prid, @MarketPrice,
                    @TableType, @Pavid, @AddonStatus, @AV_id, @Addoncount, @Cmid,
                    @VariatiionName, @PreTaxAmt, @TotalPackingPrice, @TotalPackingTax, ISNULL(@VariationId, 0)
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
           STEP 2: PREPARE PRODUCT(S) FOR STOCK RECONCILIATION
           New rows/additive combo calls use @Quantity. Existing rows use
           ABS(new quantity - previous quantity).
        ========================================================= */

        IF @DeductStock = 1
           AND @SaleQtyForStock > 0
           AND @TableType IN (1, 3)
        BEGIN
            IF @TableType = 3
            BEGIN
                /* Combo: every component must resolve to an active configured product. */
                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.tblOfferDetails AS O
                    INNER JOIN dbo.tblMenu AS M
                        ON M.ProductId = O.ProductId
                    LEFT JOIN dbo.tblProduct AS P
                        ON P.ProductId = M.ProductId
                    WHERE O.Omid = @ItemId
                      AND
                      (
                          P.ProductId IS NULL
                          OR ISNULL(P.Status, 0) <> 1
                          OR
                          (
                              ISNULL(P.FoodPreparationType, 0) NOT IN (1, 2, 3, 4)
                              AND EXISTS
                              (
                                  SELECT 1
                                  FROM dbo.tblIngredientDetails AS RD
                                  WHERE RD.ProductId = P.ProductId
                                    AND ISNULL(RD.VariationId, 0) = 0
                                    AND RD.ProductType = 1
                                    AND ISNULL(RD.Status, 1) = 1
                              )
                          )
                      )
                )
                    THROW 50314, 'A combo component is missing, inactive, or has an invalid FoodPreparationType. Stock was not deducted.', 1;

                INSERT INTO @ProductPreparation
                (
                    ProductId,
                    VariationId,
                    SaleQuantity,
                    FoodPreparationType,
                    PackagingRequired
                )
                SELECT
                    M.ProductId,
                    0,
                    @SaleQtyForStock * ISNULL(CONVERT(DECIMAL(18,4), O.Quantity), 1),
                    CASE
                        WHEN P.FoodPreparationType IN (1, 2, 3, 4)
                            THEN P.FoodPreparationType
                        ELSE 4
                    END,
                    CONVERT(BIT, ISNULL(P.PackagingRequired, 0))
                FROM dbo.tblOfferDetails AS O
                INNER JOIN dbo.tblMenu AS M
                    ON M.ProductId = O.ProductId
                INNER JOIN dbo.tblProduct AS P
                    ON P.ProductId = M.ProductId
                   AND P.Status = 1
                WHERE O.Omid = @ItemId;

                IF NOT EXISTS (SELECT 1 FROM @ProductPreparation)
                    THROW 50303, 'No active component products were found for this combo. Stock was not deducted.', 1;
            END
            ELSE
            BEGIN
                /* Normal product: tblProduct is the authoritative source.
                   Do not silently convert a missing/invalid product to NotApplicable. */
                SELECT
                    @DatabaseFoodPreparationType = P.FoodPreparationType,
                    @DatabaseProductName = P.Product,
                    @PackagingRequired = CONVERT(BIT, ISNULL(P.PackagingRequired, 0))
                FROM dbo.tblProduct AS P
                WHERE P.ProductId = @ItemId
                  AND P.Status = 1;

                IF @@ROWCOUNT = 0
                    THROW 50315, 'The sales detail ItemId does not match an active tblProduct.ProductId. Stock was not deducted.', 1;

                /*
                   Prefer the current product-master value. The BLL fallback is
                   permitted only when it is valid (1-4). The BLL supplies
                   NotApplicable=4 only when no active raw-material recipe exists.
                */
                IF ISNULL(@DatabaseFoodPreparationType, 0) IN (1, 2, 3, 4)
                    SET @FoodPreparationType = @DatabaseFoodPreparationType;
                ELSE IF ISNULL(@FoodPreparationType, 0) NOT IN (1, 2, 3, 4)
                BEGIN
                    DECLARE @PreparationTypeError NVARCHAR(2048);
                    SET @PreparationTypeError =
                        N'Invalid FoodPreparationType for menu product ProductId ' +
                        CONVERT(NVARCHAR(20), @ItemId) +
                        CASE WHEN NULLIF(LTRIM(RTRIM(@DatabaseProductName)), '') IS NULL
                             THEN N''
                             ELSE N' (' + @DatabaseProductName + N')'
                        END +
                        N'. Configure MadeToOrder=1, BatchPrepared=2, PartiallyPrepared=3, or NotApplicable=4 in Product Registration.';
                    THROW 50316, @PreparationTypeError, 1;
                END;

                /* Resolve the selected product variation from the order detail row.
                   Older/non-variation orders have vid = 0 and continue using the base recipe. */
                IF ISNULL(@SelectedVariationId, 0) = 0
                BEGIN
                    SELECT TOP (1)
                        @SelectedVariationId = ISNULL(OD.vid, 0)
                    FROM dbo.tblOrderDetails AS OD
                WHERE
                    (
                        OD.OrderDetailsId IN
                        (
                            SELECT TRY_CAST(value AS INT)
                            FROM STRING_SPLIT(ISNULL(@OrderDetailsId, ''), ',')
                            WHERE TRY_CAST(value AS INT) IS NOT NULL
                        )
                        OR (@SalesDetailsId > 0 AND OD.SalesDetailsId = @SalesDetailsId)
                    )
                    ORDER BY OD.OrderDetailsId DESC;
                END;

                IF ISNULL(@SelectedVariationId, 0) > 0
                BEGIN
                    UPDATE dbo.tblSalesDetails
                    SET VariationId = @SelectedVariationId
                    WHERE SalesDetailsId = @Id
                      AND ISNULL(VariationId, 0) = 0;
                END;

                INSERT INTO @ProductPreparation
                (
                    ProductId,
                    VariationId,
                    SaleQuantity,
                    FoodPreparationType,
                    PackagingRequired
                )
                VALUES
                (
                    @ItemId,
                    @SelectedVariationId,
                    @SaleQtyForStock,
                    @FoodPreparationType,
                    @PackagingRequired
                );
            END;

        /*
               PartiallyPrepared raw-material rows must be assigned to one of
               the two supported stages. Stage 1 is deducted here at sale;
               stage 2 is deducted only by inventory-request approval.
            */
            IF EXISTS
            (
                SELECT 1
                FROM @ProductPreparation AS P
                INNER JOIN dbo.tblIngredientDetails AS D
                    ON D.ProductId = P.ProductId
                   AND ISNULL(D.VariationId, 0) = P.VariationId
                   AND D.ProductType = 1
                   AND ISNULL(D.Status, 1) = 1
                WHERE P.FoodPreparationType = 3
                  AND ISNULL(D.Deductionstage, 0) NOT IN (1, 2)
            )
                THROW 50311, 'A PartiallyPrepared raw-material ingredient must use Deductionstage 1 (OrderTime) or 2 (BatchPreparation).', 1;

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
                        (D.ProductType = 2 AND @OrderTypeId = 1 AND P.PackagingRequired = 1)
                    )
                    AND
                    (
                        ISNULL(D.Quantity, 0) <= 0
                        OR D.Unit IS NULL
                    )
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
                        (D.ProductType = 2 AND @OrderTypeId = 1 AND P.PackagingRequired = 1)
                    )
                    AND
                    (
                        I.ItemId IS NULL
                        OR ISNULL(I.Status, 0) <> 1
                        OR I.Baseunit IS NULL
                        OR (D.Unit <> I.Unit AND D.Unit <> I.Baseunit)
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
                        (D.ProductType = 2 AND @OrderTypeId = 1 AND P.PackagingRequired = 1)
                    )
                    AND D.Unit = I.Unit
                    AND ISNULL(I.Baseunit, I.Unit) <> I.Unit
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
                    SUM
                    (
                        D.Quantity * P.SaleQuantity *
                        CASE
                            WHEN D.Unit = I.Unit
                             AND ISNULL(I.Baseunit, I.Unit) <> I.Unit
                                THEN I.Conversionfactor
                            ELSE 1
                        END
                    )
                ) AS RequiredBaseQty
            FROM @ProductPreparation AS P
            INNER JOIN dbo.tblIngredientDetails AS D
                ON D.ProductId = P.ProductId
                   AND ISNULL(D.VariationId, 0) = P.VariationId
               AND ISNULL(D.Status, 1) = 1
            INNER JOIN dbo.tblItem AS I
                ON I.ItemId = D.ItemId
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
                (D.ProductType = 2 AND @OrderTypeId = 1 AND P.PackagingRequired = 1)
            GROUP BY D.ItemId
            HAVING SUM
            (
                D.Quantity * P.SaleQuantity *
                CASE
                    WHEN D.Unit = I.Unit
                     AND ISNULL(I.Baseunit, I.Unit) <> I.Unit
                        THEN I.Conversionfactor
                    ELSE 1
                END
            ) > 0;

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

            /*
               Do not allow an invalid/inactive/SaleType item to disappear
               from the stock allocation because the insert above uses an
               INNER JOIN. Every required ingredient must be represented.
            */
            IF EXISTS
            (
                SELECT 1
                FROM @RequiredStock AS R
                LEFT JOIN @StockToDeduct AS S
                    ON S.ItemId = R.ItemId
                WHERE S.ItemId IS NULL
            )
                THROW 50312, 'A required raw-material or packaging item is inactive, missing, or not configured as a stock item (SaleType = 0). Sale was cancelled.', 1;

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

            IF @IsStockRestock = 0 AND EXISTS
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

            IF @IsStockRestock = 1
            BEGIN
                /* Return the reduced quantity to the oldest active stock row.
                   QOH is purchase-unit quantity; RequiredQty is Baseunit quantity. */
                ;WITH FirstRate AS
                (
                    SELECT
                        PR.Id,
                        S.RequiredQty,
                        S.ConversionFactor,
                        ROW_NUMBER() OVER (PARTITION BY PR.ProductId ORDER BY PR.Id) AS RowNo
                    FROM dbo.tblProductRate AS PR WITH (UPDLOCK, HOLDLOCK)
                    INNER JOIN @StockToDeduct AS S
                        ON S.ItemId = PR.ProductId
                    WHERE PR.Status = 1
                )
                UPDATE PR
                SET PR.QOH = CONVERT
                (
                    DECIMAL(18,4),
                    ISNULL(PR.QOH, 0) + (F.RequiredQty / NULLIF(F.ConversionFactor, 0))
                )
                FROM dbo.tblProductRate AS PR
                INNER JOIN FirstRate AS F
                    ON F.Id = PR.Id
                WHERE F.RowNo = 1;
            END
            ELSE
            BEGIN
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