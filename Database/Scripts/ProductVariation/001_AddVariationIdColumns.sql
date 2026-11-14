/*
    Product variation recipe / inventory / sales migration.
    Run this script once against the target FoodBeverage database.
    Existing rows remain NULL and are treated as the legacy/default variation (VariationId = 0).
*/
SET XACT_ABORT ON;
GO

IF OBJECT_ID(N'dbo.tblIngredientDetails', N'U') IS NULL
    THROW 51001, 'dbo.tblIngredientDetails was not found.', 1;
IF OBJECT_ID(N'dbo.tblInventoryRequestDetails', N'U') IS NULL
    THROW 51002, 'dbo.tblInventoryRequestDetails was not found.', 1;
IF OBJECT_ID(N'dbo.tblInventoryRequestDraftDetails', N'U') IS NULL
    THROW 51003, 'dbo.tblInventoryRequestDraftDetails was not found.', 1;
IF OBJECT_ID(N'dbo.tblSalesDetails', N'U') IS NULL
    THROW 51004, 'dbo.tblSalesDetails was not found.', 1;
GO

IF COL_LENGTH(N'dbo.tblIngredientDetails', N'VariationId') IS NULL
    ALTER TABLE dbo.tblIngredientDetails ADD VariationId BIGINT NULL;
GO
IF COL_LENGTH(N'dbo.tblInventoryRequestDetails', N'VariationId') IS NULL
    ALTER TABLE dbo.tblInventoryRequestDetails ADD VariationId BIGINT NULL;
GO
IF COL_LENGTH(N'dbo.tblInventoryRequestDraftDetails', N'VariationId') IS NULL
    ALTER TABLE dbo.tblInventoryRequestDraftDetails ADD VariationId BIGINT NULL;
GO
IF COL_LENGTH(N'dbo.tblSalesDetails', N'VariationId') IS NULL
    ALTER TABLE dbo.tblSalesDetails ADD VariationId BIGINT NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.tblIngredientDetails') AND name = N'IX_tblIngredientDetails_Product_Variation_Status')
    CREATE NONCLUSTERED INDEX IX_tblIngredientDetails_Product_Variation_Status
        ON dbo.tblIngredientDetails(ProductId, VariationId, Status)
        INCLUDE(ItemId, quantity, Unit, ProductType, Deductionstage, BranchId);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.tblInventoryRequestDetails') AND name = N'IX_tblInventoryRequestDetails_Request_Variation')
    CREATE NONCLUSTERED INDEX IX_tblInventoryRequestDetails_Request_Variation
        ON dbo.tblInventoryRequestDetails(InventoryRequestId, RequestType, ProductId, VariationId, Status);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.tblSalesDetails') AND name = N'IX_tblSalesDetails_Sales_Variation')
    CREATE NONCLUSTERED INDEX IX_tblSalesDetails_Sales_Variation
        ON dbo.tblSalesDetails(SalesMasterId, ItemId, VariationId);
GO

PRINT 'VariationId columns and supporting indexes are ready.';
GO
