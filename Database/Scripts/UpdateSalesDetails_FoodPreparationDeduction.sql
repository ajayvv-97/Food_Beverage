USE [FoodBeverage];
GO

IF COL_LENGTH(N'dbo.tblSalesDetails', N'TotalPackingPrice') IS NULL
    ALTER TABLE dbo.tblSalesDetails ADD TotalPackingPrice DECIMAL(18,2) NOT NULL
        CONSTRAINT DF_tblSalesDetails_TotalPackingPrice DEFAULT (0);
GO
IF COL_LENGTH(N'dbo.tblSalesDetails', N'TotalPackingTax') IS NULL
    ALTER TABLE dbo.tblSalesDetails ADD TotalPackingTax DECIMAL(18,2) NOT NULL
        CONSTRAINT DF_tblSalesDetails_TotalPackingTax DEFAULT (0);
GO

USE [FoodBeverage];
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
    @TotalPackingPrice   DECIMAL(18,2) = 0,
    @TotalPackingTax     DECIMAL(18,2) = 0
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Id BIGINT;
    DECLARE @Count INT = 0;
    DECLARE @DiscAmt DECIMAL(18,2);

    /* ===================== UPDATE ===================== */
    IF @SalesDetailsId > 0
    BEGIN
        UPDATE tblSalesDetails
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
            Pavid            = @Pavid,
            AddonStatus      = @AddonStatus,
            AV_id            = @AV_id,
            Addoncount       = @Addoncount,
            Cmid             = @Cmid,
            PreTaxAmt        = @PreTaxAmt,
            TotalPackingPrice = @TotalPackingPrice,
            TotalPackingTax   = @TotalPackingTax
        WHERE SalesDetailsId = @SalesDetailsId;

        SET @Id = @SalesDetailsId;
    END
    ELSE
    BEGIN
        IF @TableType > 0
        BEGIN
            IF @AddonStatus IN (0,1)
            BEGIN
                SELECT @Count = COUNT(*)
                FROM tblSalesDetails
                WHERE Cmid = @Cmid
                  AND TableType = @TableType
                  AND SalesMasterId = @SalesMasterId
                  AND AddonStatus IN (0,1);
            END
            ELSE IF @AddonStatus = 2
            BEGIN
                SELECT @Count = COUNT(*)
                FROM tblSalesDetails
                WHERE Cmid = @Cmid
                  AND Pavid = @Pavid
                  AND AddonStatus = 2
                  AND TableType = @TableType
                  AND SalesMasterId = @SalesMasterId;
            END
        END

        /* ===== UPDATE EXISTING ===== */
        IF @Count > 0
        BEGIN
            IF @AddonStatus IN (0,1)
            BEGIN
                UPDATE tblSalesDetails
                SET
                    Quantity        = Quantity + @Quantity,
                    NetAmount       = NetAmount + (MarketPrice * @Quantity),
                    DiscountAmount  = DiscountAmount + @DiscountAmount,
                    Amount          = Amount + @Amount,
                    IGSTAmount      = IGSTAmount + @IGSTAmount,
                    CGSTAmount      = CGSTAmount + @CGSTAmount,
                    SGSTAmount      = SGSTAmount + @SGSTAmount,
                    TaxAmount       = TaxAmount + @TaxAmount,
                    TotalPackingPrice = ISNULL(TotalPackingPrice, 0) + @TotalPackingPrice,
                    TotalPackingTax   = ISNULL(TotalPackingTax, 0) + @TotalPackingTax
                WHERE Cmid = @Cmid
                  AND TableType = @TableType
                  AND SalesMasterId = @SalesMasterId
                  AND AddonStatus IN (0,1);
            END
            ELSE IF @AddonStatus = 2
            BEGIN
                UPDATE tblSalesDetails
                SET
                    Quantity        = Quantity + @Quantity,
                    DiscountAmount  = DiscountAmount + @DiscountAmount,
                    Amount          = Amount + @Amount,
                    IGSTAmount      = IGSTAmount + @IGSTAmount,
                    CGSTAmount      = CGSTAmount + @CGSTAmount,
                    SGSTAmount      = SGSTAmount + @SGSTAmount,
                    TaxAmount       = TaxAmount + @TaxAmount,
                    TotalPackingPrice = ISNULL(TotalPackingPrice, 0) + @TotalPackingPrice,
                    TotalPackingTax   = ISNULL(TotalPackingTax, 0) + @TotalPackingTax
                WHERE Cmid = @Cmid
                  AND Pavid = @Pavid
                  AND AddonStatus = 2
                  AND TableType = @TableType
                  AND SalesMasterId = @SalesMasterId;
            END
        END
        ELSE
        BEGIN
            INSERT INTO tblSalesDetails
            (
                SalesMasterId, MId, UnitId, Quantity, SalesRate, MRP, Amount,
                DiscountPer, DiscountAmount, IGSTAmount, CGSTAmount, SGSTAmount,
                TaxPer, TaxAmount, NetAmount, BranchId, TaxType, Tax,
                CreatedBy, CreatedDateTime, Status, ProductType, VatAmount,
                CusId, ItemName, ItemId, ItemBarcode, Prid, MarketPrice,
                TableType, Pavid, AddonStatus, AV_id, Addoncount, Cmid,
                VariatiionName, PreTaxAmt, TotalPackingPrice, TotalPackingTax
            )
            VALUES
            (
                @SalesMasterId, @MId, @UnitId, @Quantity, @SalesRate, @MRP, @Amount,
                @DiscountPer, @DiscountAmount, @IGSTAmount, @CGSTAmount, @SGSTAmount,
                @TaxPer, @TaxAmount, @NetAmount, @BranchId, @TaxType, @Tax,
                @CreatedBy, SYSDATETIME(), @Status, @ProductType, @VatAmount,
                @CusId, @ItemName, @ItemId, @ItemBarcode, @Prid, @MarketPrice,
                @TableType, @Pavid, @AddonStatus, @AV_id, @Addoncount, @Cmid,
                @VariatiionName, @PreTaxAmt, @TotalPackingPrice, @TotalPackingTax
            );

            SET @Id = SCOPE_IDENTITY();

            UPDATE tblOrderDetails
            SET SalesDetailsId = @Id
            WHERE OrderDetailsId IN
            (
                SELECT TRY_CAST(value AS INT)
                FROM STRING_SPLIT(@OrderDetailsId, ',')
                WHERE TRY_CAST(value AS INT) IS NOT NULL
            );
        END
    END

    /* ===================== INGREDIENT DEDUCTION ===================== */
    /*
       FoodPreparationType:
         1 = MadeToOrder       : deduct raw-material ingredients (ProductType=1)
         2 = BatchPrepared     : raw materials were deducted during batch preparation
         3 = PartiallyPrepared : deduct only order-preparation ingredients (Deductionstage=1)
         4 = NotApplicable     : no raw-material recipe deduction

       tblIngredientDetails.ProductType:
         1 = RawMaterial
         2 = PackagingMaterial

       OrderTypeId:
         1 = Pickup, 2 = Takeaway, 3 = Delivery
       Packaging is deducted for Pickup, Takeaway and Delivery.
    */
    DECLARE @ProductPreparation TABLE
    (
        ProductId INT NOT NULL,
        SaleQuantity DECIMAL(18,2) NOT NULL,
        FoodPreparationType INT NOT NULL
    );

    IF @TableType = 3
    BEGIN
        /* Combo products: process every menu product in the combo.
           Each component's preparation type is read from tblProduct. */
        INSERT INTO @ProductPreparation (ProductId, SaleQuantity, FoodPreparationType)
        SELECT
            M.ProductId,
            @Quantity,
            CASE
                WHEN P.FoodPreparationType IN (1,2,3,4) THEN P.FoodPreparationType
                WHEN @FoodPreparationType IN (1,2,3,4) THEN @FoodPreparationType
                ELSE 4
            END
        FROM tblOfferDetails AS O
        INNER JOIN tblMenu AS M
            ON M.ProductId = O.ProductId
        LEFT JOIN tblProduct AS P
            ON P.ProductId = M.ProductId
        WHERE O.Omid = @ItemId;
    END
    ELSE
    BEGIN
        /* Normal product: take the authoritative value from tblProduct. */
        SELECT
            @FoodPreparationType =
                CASE
                    WHEN P.FoodPreparationType IN (1,2,3,4) THEN P.FoodPreparationType
                    WHEN @FoodPreparationType IN (1,2,3,4) THEN @FoodPreparationType
                    ELSE 4
                END
        FROM tblProduct AS P
        WHERE P.ProductId = @ItemId;

        SET @FoodPreparationType =
            CASE WHEN @FoodPreparationType IN (1,2,3,4)
                 THEN @FoodPreparationType ELSE 4 END;

        INSERT INTO @ProductPreparation (ProductId, SaleQuantity, FoodPreparationType)
        VALUES (@ItemId, @Quantity, @FoodPreparationType);
    END;

    ;WITH RequiredIngredients AS
    (
        SELECT
            D.ItemId,
            SUM
            (
                CONVERT
                (
                    DECIMAL(18,4),
                    D.Quantity * P.SaleQuantity * UC.Factor
                )
            ) AS RequiredQuantity
        FROM @ProductPreparation AS P
        INNER JOIN tblIngredientDetails AS D
            ON D.ProductId = P.ProductId
           AND D.Status = 1
        INNER JOIN tblItem AS I
            ON I.ItemId = D.ItemId
        INNER JOIN tblUnitConversion AS UC
            ON D.Unit = UC.FromUnit
           AND I.Unit = UC.ToUnit
        WHERE
            (
                D.ProductType = 1
                AND
                (
                    P.FoodPreparationType = 1
                    OR
                    (
                        P.FoodPreparationType = 3
                        AND ISNULL(D.Deductionstage, 0) = 1
                    )
                )
            )
            OR
            (
                D.ProductType = 2
                AND @OrderTypeId IN (1,2,3)
            )
        GROUP BY D.ItemId
    )
    UPDATE I
    SET I.Quantity = I.Quantity - R.RequiredQuantity
    FROM tblItem AS I
    INNER JOIN RequiredIngredients AS R
        ON R.ItemId = I.ItemId
    WHERE R.RequiredQuantity > 0;

    /* ===================== FINAL UPDATE ===================== */
    SELECT @DiscAmt = SUM(DiscountAmount)
    FROM tblSalesDetails
    WHERE SalesMasterId = @SalesMasterId;

    UPDATE tblSalesMaster
    SET TotalDiscount = @DiscAmt
    WHERE SalesMasterId = @SalesMasterId;

    SELECT @Id AS SalesDetailsId;
END;
GO
