/*
 Product variation registration migration for FoodBeverage.
 Existing tblProductVariation is retained to preserve its VariationId references.
*/
IF OBJECT_ID(N'dbo.tblProductVariation', N'U') IS NULL
BEGIN
 CREATE TABLE dbo.tblProductVariation
 (
  VariationId BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tblProductVariation PRIMARY KEY,
  ProductId BIGINT NOT NULL,
  VariationName VARCHAR(200) NOT NULL,
  VariationTypeId INT NULL,
  Price DECIMAL(18,2) NOT NULL,
  DiscountAmt DECIMAL(18,2) NULL CONSTRAINT DF_tblProductVariation_DiscountAmt DEFAULT (0),
  DiscountPer DECIMAL(18,2) NULL CONSTRAINT DF_tblProductVariation_DiscountPer DEFAULT (0),
  QOH INT NULL CONSTRAINT DF_tblProductVariation_QOH DEFAULT (0),
  IsUnlimited BIT NULL CONSTRAINT DF_tblProductVariation_IsUnlimited DEFAULT (1),
  BranchId BIGINT NULL,
  CreatedBy BIGINT NULL,
  CreatedDateTime DATETIME NULL CONSTRAINT DF_tblProductVariation_CreatedDateTime DEFAULT (GETDATE()),
  ModifiedBy BIGINT NULL,
  ModifiedDateTime DATETIME NULL,
  Status INT NULL CONSTRAINT DF_tblProductVariation_Status DEFAULT (1)
 );
END;
GO

IF COL_LENGTH(N'dbo.tblProductVariation', N'VariationTypeId') IS NOT NULL
 ALTER TABLE dbo.tblProductVariation ALTER COLUMN VariationTypeId INT NULL;
GO

IF NOT EXISTS
(
 SELECT 1 FROM sys.indexes
 WHERE object_id = OBJECT_ID(N'dbo.tblProductVariation')
   AND name = N'IX_tblProductVariation_Product_Branch_Status'
)
BEGIN
 CREATE NONCLUSTERED INDEX IX_tblProductVariation_Product_Branch_Status
 ON dbo.tblProductVariation(ProductId, BranchId, Status)
 INCLUDE (VariationName, Price, DiscountAmt, DiscountPer, QOH, IsUnlimited);
END;
GO

IF COL_LENGTH(N'dbo.tblProduct', N'HasVariation') IS NULL
 ALTER TABLE dbo.tblProduct ADD HasVariation BIT NULL;
GO
