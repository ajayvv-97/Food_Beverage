/*
    Food preparation, ingredient-stage and product-packaging schema.
    Run this script in the FoodBeverage database before applying:
      - UpdateInventoryRequestApproval_DeductRawStock.sql
      - UpdateSalesDetails_FoodPreparationDeduction.sql

    The script is idempotent: existing columns are left unchanged.
*/
USE [FoodBeverage];
GO

IF OBJECT_ID(N'dbo.tblProduct', N'U') IS NULL
    THROW 50401, 'dbo.tblProduct was not found.', 1;
IF OBJECT_ID(N'dbo.tblIngredientDetails', N'U') IS NULL
    THROW 50402, 'dbo.tblIngredientDetails was not found.', 1;
IF OBJECT_ID(N'dbo.tblItem', N'U') IS NULL
    THROW 50403, 'dbo.tblItem was not found.', 1;
IF OBJECT_ID(N'dbo.tblSalesDetails', N'U') IS NULL
    THROW 50404, 'dbo.tblSalesDetails was not found.', 1;
GO

/* 1 = MadeToOrder, 2 = BatchPrepared, 3 = PartiallyPrepared, 4 = NotApplicable */
IF COL_LENGTH(N'dbo.tblProduct', N'FoodPreparationType') IS NULL
BEGIN
    ALTER TABLE dbo.tblProduct
        ADD FoodPreparationType INT NOT NULL
            CONSTRAINT DF_tblProduct_FoodPreparationType DEFAULT (4) WITH VALUES;
END;
GO

/* Product-level switch: packaging recipe is charged/deducted only when enabled. */
IF COL_LENGTH(N'dbo.tblProduct', N'PackagingRequired') IS NULL
BEGIN
    ALTER TABLE dbo.tblProduct
        ADD PackagingRequired BIT NOT NULL
            CONSTRAINT DF_tblProduct_PackagingRequired DEFAULT (0) WITH VALUES;
END;
GO

/* Ingredient ProductType: 1 = RawMaterial, 2 = PackagingMaterial.
   Deductionstage: 1 = OrderTime, 2 = BatchPreparation.
   Stage is used only for FoodPreparationType = 3. */
IF COL_LENGTH(N'dbo.tblIngredientDetails', N'ProductType') IS NULL
BEGIN
    ALTER TABLE dbo.tblIngredientDetails
        ADD ProductType INT NOT NULL
            CONSTRAINT DF_tblIngredientDetails_ProductType DEFAULT (1) WITH VALUES;
END;
GO

IF COL_LENGTH(N'dbo.tblIngredientDetails', N'Deductionstage') IS NULL
BEGIN
    ALTER TABLE dbo.tblIngredientDetails
        ADD Deductionstage INT NOT NULL
            CONSTRAINT DF_tblIngredientDetails_Deductionstage DEFAULT (1) WITH VALUES;
END;
GO

/* Stock-unit conversion: Conversionfactor = base units per purchase unit. */
IF COL_LENGTH(N'dbo.tblItem', N'Baseunit') IS NULL
BEGIN
    ALTER TABLE dbo.tblItem
        ADD Baseunit INT NULL;
END;
GO

IF COL_LENGTH(N'dbo.tblItem', N'Conversionfactor') IS NULL
BEGIN
    ALTER TABLE dbo.tblItem
        ADD Conversionfactor DECIMAL(18,4) NOT NULL
            CONSTRAINT DF_tblItem_Conversionfactor DEFAULT (1) WITH VALUES;
END;
GO

/* Per-sale product packaging totals saved by prdInsert_Upd_SalesDetails. */
IF COL_LENGTH(N'dbo.tblSalesDetails', N'TotalPackingPrice') IS NULL
BEGIN
    ALTER TABLE dbo.tblSalesDetails
        ADD TotalPackingPrice DECIMAL(18,2) NOT NULL
            CONSTRAINT DF_tblSalesDetails_TotalPackingPrice DEFAULT (0) WITH VALUES;
END;
GO

IF COL_LENGTH(N'dbo.tblSalesDetails', N'TotalPackingTax') IS NULL
BEGIN
    ALTER TABLE dbo.tblSalesDetails
        ADD TotalPackingTax DECIMAL(18,2) NOT NULL
            CONSTRAINT DF_tblSalesDetails_TotalPackingTax DEFAULT (0) WITH VALUES;
END;
GO

/* Supports recipe lookup by product, category and deduction stage. */
IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.tblIngredientDetails')
      AND name = N'IX_tblIngredientDetails_Product_Deduction'
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_tblIngredientDetails_Product_Deduction
        ON dbo.tblIngredientDetails
        (
            ProductId,
            ProductType,
            Status,
            Deductionstage
        )
        INCLUDE (ItemId, quantity, Unit);
END;
GO

/* Existing item records must be reviewed and assigned a real Baseunit.
   Do not automatically infer Baseunit from purchase unit because that can
   produce incorrect stock conversion for grams, kilograms, packs, etc. */
SELECT
    ItemId,
    Itemname,
    unit AS PurchaseUnit,
    Baseunit,
    Conversionfactor
FROM dbo.tblItem
WHERE Status = 1
  AND (Baseunit IS NULL OR ISNULL(Conversionfactor, 0) <= 0);
GO
