/*
 Product-specific variation pricing migration.
 Price stores the final variation selling price for existing POS compatibility.
 PriceAddOn stores the amount added to tblProduct.Price.
 Existing stock columns are retained for backward compatibility but are not used by this UI.
*/
IF OBJECT_ID(N'dbo.tblProductVariation', N'U') IS NULL
BEGIN
 CREATE TABLE dbo.tblProductVariation
 (
  VariationId BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tblProductVariation PRIMARY KEY,
  ProductId BIGINT NOT NULL,
  VariationName VARCHAR(200) NOT NULL,
  VariationTypeId INT NULL,
  Price DECIMAL(18,2) NOT NULL CONSTRAINT DF_tblProductVariation_Price DEFAULT (0),
  PriceAddOn DECIMAL(18,2) NOT NULL CONSTRAINT DF_tblProductVariation_PriceAddOn DEFAULT (0),
  DiscountAmt DECIMAL(18,2) NULL CONSTRAINT DF_tblProductVariation_DiscountAmt DEFAULT (0),
  DiscountPer DECIMAL(18,2) NULL CONSTRAINT DF_tblProductVariation_DiscountPer DEFAULT (0),
  IsDefault BIT NOT NULL CONSTRAINT DF_tblProductVariation_IsDefault DEFAULT (0),
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

IF COL_LENGTH(N'dbo.tblProductVariation', N'PriceAddOn') IS NULL
 ALTER TABLE dbo.tblProductVariation ADD PriceAddOn DECIMAL(18,2) NOT NULL
  CONSTRAINT DF_tblProductVariation_PriceAddOn_Migration DEFAULT (0) WITH VALUES;
GO

IF COL_LENGTH(N'dbo.tblProductVariation', N'IsDefault') IS NULL
 ALTER TABLE dbo.tblProductVariation ADD IsDefault BIT NOT NULL
  CONSTRAINT DF_tblProductVariation_IsDefault_Migration DEFAULT (0) WITH VALUES;
GO

/* Backfill add-ons for legacy rows where Price previously stored the full variation price. */
UPDATE v
 SET PriceAddOn = CASE WHEN v.Price > p.Price THEN v.Price - p.Price ELSE 0 END
FROM dbo.tblProductVariation v
INNER JOIN dbo.tblProduct p ON p.ProductId = v.ProductId
WHERE ISNULL(v.PriceAddOn, 0) = 0;
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
 INCLUDE (VariationName, Price, PriceAddOn, DiscountAmt, DiscountPer, IsDefault);
END;
GO

/* Ensure there is at most one default variation per product and branch.
   Existing rows are normalized before creating the filtered unique index. */
;WITH RankedDefaults AS
(
 SELECT VariationId,
        ROW_NUMBER() OVER
        (PARTITION BY ProductId, BranchId ORDER BY CASE WHEN IsDefault = 1 THEN 0 ELSE 1 END, VariationId) AS RowNo
 FROM dbo.tblProductVariation
 WHERE Status = 1
)
UPDATE v
 SET IsDefault = CASE WHEN r.RowNo = 1 THEN 1 ELSE 0 END
FROM dbo.tblProductVariation v
INNER JOIN RankedDefaults r ON r.VariationId = v.VariationId;
GO

IF NOT EXISTS
(
 SELECT 1 FROM sys.indexes
 WHERE object_id = OBJECT_ID(N'dbo.tblProductVariation')
   AND name = N'UX_tblProductVariation_OneDefault'
)
BEGIN
 CREATE UNIQUE NONCLUSTERED INDEX UX_tblProductVariation_OneDefault
 ON dbo.tblProductVariation(ProductId, BranchId)
 WHERE Status = 1 AND IsDefault = 1;
END;
GO

IF COL_LENGTH(N'dbo.tblProduct', N'HasVariation') IS NULL
 ALTER TABLE dbo.tblProduct ADD HasVariation BIT NULL;
GO


/* =========================================================
   BLL-compatible variation save procedure.
   New parameters are optional so existing restaurant callers
   using the legacy parameter list continue to work.
   ========================================================= */
IF OBJECT_ID(N'dbo.prdInsert_upd_ProductVariation', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.prdInsert_upd_ProductVariation AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE dbo.prdInsert_upd_ProductVariation
    @VariationId BIGINT,
    @VariationName VARCHAR(200),
    @VariationTypeId INT,
    @Price DECIMAL(18,2),
    @DiscountAmt DECIMAL(18,2),
    @DiscountPer DECIMAL(18,2),
    @QOH INT,
    @IsUnlimited BIT,
    @ProductId BIGINT,
    @BranchId BIGINT,
    @CreatedBy BIGINT,
    @Status INT,
    @PriceAddOn DECIMAL(18,2) = NULL,
    @IsDefault BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @SavedVariationId BIGINT;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF ISNULL(@IsDefault, 0) = 1
        BEGIN
            UPDATE dbo.tblProductVariation
            SET IsDefault = 0
            WHERE ProductId = @ProductId
              AND BranchId = @BranchId
              AND Status = 1
              AND VariationId <> ISNULL(@VariationId, 0);
        END;

        IF ISNULL(@VariationId, 0) > 0
        BEGIN
            UPDATE dbo.tblProductVariation
            SET VariationName = @VariationName,
                VariationTypeId = @VariationTypeId,
                Price = @Price,
                PriceAddOn = ISNULL(@PriceAddOn, PriceAddOn),
                DiscountAmt = @DiscountAmt,
                DiscountPer = @DiscountPer,
                QOH = CASE WHEN @IsUnlimited = 1 THEN NULL ELSE @QOH END,
                IsUnlimited = @IsUnlimited,
                IsDefault = ISNULL(@IsDefault, IsDefault),
                ModifiedBy = @CreatedBy,
                ModifiedDateTime = SYSDATETIME(),
                Status = @Status
            WHERE VariationId = @VariationId
              AND ProductId = @ProductId
              AND BranchId = @BranchId;

            SET @SavedVariationId = @VariationId;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.tblProductVariation
            (
                VariationName, VariationTypeId, Price, PriceAddOn,
                DiscountAmt, DiscountPer, QOH, IsUnlimited, IsDefault,
                ProductId, BranchId, CreatedBy, CreatedDateTime, Status
            )
            VALUES
            (
                @VariationName, @VariationTypeId, @Price, ISNULL(@PriceAddOn, 0),
                @DiscountAmt, @DiscountPer,
                CASE WHEN @IsUnlimited = 1 THEN NULL ELSE @QOH END,
                @IsUnlimited, ISNULL(@IsDefault, 0),
                @ProductId, @BranchId, @CreatedBy, SYSDATETIME(), @Status
            );

            SET @SavedVariationId = SCOPE_IDENTITY();
        END;

        COMMIT TRANSACTION;
        SELECT @SavedVariationId AS VariationId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

/* Soft-delete procedure used by BllProductVariation. */
IF OBJECT_ID(N'dbo.prdDelete_ProductVariation', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.prdDelete_ProductVariation AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE dbo.prdDelete_ProductVariation
    @VariationId BIGINT,
    @ProductId BIGINT,
    @BranchId BIGINT,
    @CreatedBy BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.tblProductVariation
    SET Status = 0,
        IsDefault = 0,
        ModifiedBy = @CreatedBy,
        ModifiedDateTime = SYSDATETIME()
    WHERE VariationId = @VariationId
      AND ProductId = @ProductId
      AND BranchId = @BranchId;

    SELECT @VariationId AS VariationId;
END;
GO
