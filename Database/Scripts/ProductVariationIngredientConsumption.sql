/*
    Product variation-wise ingredient consumption.
    Existing ingredient records remain intact and are assigned to VariationId = 0
    (the legacy/base recipe). New variation recipes use tblProductVariation.VariationId.
    Run once against the Food_Beverage database before using the updated FrmIngredient.
*/
SET NOCOUNT ON;
GO

IF OBJECT_ID(N'dbo.tblIngredientDetails', N'U') IS NULL
    THROW 50021, 'Required table dbo.tblIngredientDetails was not found.', 1;
GO

IF COL_LENGTH(N'dbo.tblIngredientDetails', N'VariationId') IS NULL
BEGIN
    ALTER TABLE dbo.tblIngredientDetails
        ADD VariationId INT NOT NULL
            CONSTRAINT DF_tblIngredientDetails_VariationId DEFAULT (0) WITH VALUES;
END
GO

IF NOT EXISTS
(
    SELECT 1 FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.tblIngredientDetails')
      AND name = N'IX_tblIngredientDetails_Product_Variation_Status'
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_tblIngredientDetails_Product_Variation_Status
        ON dbo.tblIngredientDetails(ProductId, VariationId, BranchId, Status)
        INCLUDE (ItemId, quantity, Unit, Deductionstage, ProductType);
END
GO
