/*
    Product Add-on Management
    Target branch: ProductVariation
    SQL Server migration. Run once against the F&B database before enabling the
    add-on recipe UI / inventory integration.

    Existing tables reused:
      tblAddon
      tblProduct_Addon_Variation
      tblIngredientDetails / tblItem / tblUnit (existing product recipe model)

    Add-ons are priced independently from their inventory recipe. IsFree only
    controls the customer charge; recipe quantities are still consumed.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

IF COL_LENGTH('dbo.tblAddon', 'IsFree') IS NULL
    ALTER TABLE dbo.tblAddon ADD IsFree bit NOT NULL
        CONSTRAINT DF_tblAddon_IsFree DEFAULT (0) WITH VALUES;
GO

/* Legacy add-on registration stores the customer price in tblAddon.Price.
   Keep the explicit flag synchronized so existing save procedures continue
   to support free add-ons without changing the shared BLL/DmnAddon assembly. */
CREATE OR ALTER TRIGGER dbo.trg_tblAddon_SyncIsFree
ON dbo.tblAddon
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF TRIGGER_NESTLEVEL() > 1 RETURN;

    UPDATE a
       SET IsFree = CASE WHEN ISNULL(a.Price, 0) = 0 THEN 1 ELSE 0 END
    FROM dbo.tblAddon a
    INNER JOIN inserted i ON i.Aid = a.Aid;
END;
GO

UPDATE dbo.tblAddon
   SET IsFree = CASE WHEN ISNULL(Price, 0) = 0 THEN 1 ELSE 0 END
 WHERE IsFree <> CASE WHEN ISNULL(Price, 0) = 0 THEN 1 ELSE 0 END;
GO

IF COL_LENGTH('dbo.tblAddon', 'TrackInventory') IS NULL
    ALTER TABLE dbo.tblAddon ADD TrackInventory bit NOT NULL
        CONSTRAINT DF_tblAddon_TrackInventory DEFAULT (0) WITH VALUES;
GO

IF COL_LENGTH('dbo.tblAddon', 'FoodPreparationType') IS NULL
    ALTER TABLE dbo.tblAddon ADD FoodPreparationType int NOT NULL
        CONSTRAINT DF_tblAddon_FoodPreparationType DEFAULT (0) WITH VALUES;
GO

IF OBJECT_ID('dbo.tblAddonIngredientDetails', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblAddonIngredientDetails
    (
        AddonIngredientId int IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_tblAddonIngredientDetails PRIMARY KEY,
        Aid int NOT NULL,
        ItemId int NOT NULL,
        Quantity decimal(18,6) NOT NULL,
        Unit int NOT NULL,
        DeductionStage int NOT NULL
            CONSTRAINT DF_tblAddonIngredientDetails_DeductionStage DEFAULT (0),
        BranchId int NOT NULL,
        CreatedBy int NOT NULL,
        CreatedDate datetime NOT NULL
            CONSTRAINT DF_tblAddonIngredientDetails_CreatedDate DEFAULT (GETDATE()),
        ModifiedBy int NULL,
        ModifiedDate datetime NULL,
        Status bit NOT NULL
            CONSTRAINT DF_tblAddonIngredientDetails_Status DEFAULT (1),
        CONSTRAINT CK_tblAddonIngredientDetails_Quantity CHECK (Quantity > 0)
    );

    CREATE INDEX IX_tblAddonIngredientDetails_AidBranchStatus
        ON dbo.tblAddonIngredientDetails(Aid, BranchId, Status)
        INCLUDE(ItemId, Quantity, Unit, DeductionStage);
END;
GO

/* Return all recipe rows for one add-on. The caller must filter the current
   branch and must not skip recipes for free add-ons. */
CREATE OR ALTER PROCEDURE dbo.prdGet_AddonIngredientRecipe
    @Aid int,
    @BranchId int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        d.AddonIngredientId,
        d.Aid,
        d.ItemId,
        i.Itemname,
        d.Quantity,
        d.Unit,
        u.UnitName,
        d.DeductionStage,
        a.IsFree,
        a.TrackInventory,
        a.FoodPreparationType
    FROM dbo.tblAddonIngredientDetails d
    INNER JOIN dbo.tblAddon a ON a.Aid = d.Aid
    LEFT JOIN dbo.tblItem i ON i.ItemId = d.ItemId
    LEFT JOIN dbo.tblUnit u ON u.UId = d.Unit
    WHERE d.Aid = @Aid
      AND d.BranchId = @BranchId
      AND d.Status = 1
      AND a.Status = 1
    ORDER BY d.AddonIngredientId;
END;
GO

/* Atomic single-row save. Passing @AddonIngredientId = 0 inserts; otherwise
   updates the existing row. A recipe can contain any number of ingredient rows. */
CREATE OR ALTER PROCEDURE dbo.prdInsert_Upd_AddonIngredientRecipe
    @AddonIngredientId int,
    @Aid int,
    @ItemId int,
    @Quantity decimal(18,6),
    @Unit int,
    @DeductionStage int = 0,
    @BranchId int,
    @CreatedBy int,
    @Status bit = 1
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @Aid <= 0 OR @ItemId <= 0 OR @Unit <= 0 OR @Quantity <= 0
        THROW 51040, 'Valid add-on, raw material, unit and quantity are required.', 1;

    IF NOT EXISTS
    (
        SELECT 1 FROM dbo.tblAddon
        WHERE Aid = @Aid AND BranchId = @BranchId AND Status = 1
    )
        THROW 51041, 'Active add-on was not found for this branch.', 1;

    BEGIN TRANSACTION;

    IF ISNULL(@AddonIngredientId, 0) = 0
    BEGIN
        INSERT INTO dbo.tblAddonIngredientDetails
        (
            Aid, ItemId, Quantity, Unit, DeductionStage,
            BranchId, CreatedBy, Status
        )
        VALUES
        (
            @Aid, @ItemId, @Quantity, @Unit, ISNULL(@DeductionStage, 0),
            @BranchId, @CreatedBy, @Status
        );

        SET @AddonIngredientId = CONVERT(int, SCOPE_IDENTITY());
    END
    ELSE
    BEGIN
        UPDATE dbo.tblAddonIngredientDetails
        SET ItemId = @ItemId,
            Quantity = @Quantity,
            Unit = @Unit,
            DeductionStage = ISNULL(@DeductionStage, 0),
            ModifiedBy = @CreatedBy,
            ModifiedDate = GETDATE(),
            Status = @Status
        WHERE AddonIngredientId = @AddonIngredientId
          AND Aid = @Aid
          AND BranchId = @BranchId;

        IF @@ROWCOUNT = 0
            THROW 51042, 'Add-on ingredient row was not found.', 1;
    END;

    COMMIT TRANSACTION;

    SELECT @AddonIngredientId AS AddonIngredientId;
END;
GO

/* Deactivate rows omitted by the editor without deleting history. */
CREATE OR ALTER PROCEDURE dbo.prdDeactivate_AddonIngredientRecipe
    @Aid int,
    @BranchId int,
    @RetainedIds varchar(max) = ''
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.tblAddonIngredientDetails
       SET Status = 0,
           ModifiedDate = GETDATE()
     WHERE Aid = @Aid
       AND BranchId = @BranchId
       AND Status = 1
       AND
       (
           ISNULL(@RetainedIds, '') = ''
           OR CHARINDEX(',' + CONVERT(varchar(20), AddonIngredientId) + ',',
                        ',' + @RetainedIds + ',') = 0
       );
END;
GO

/* Order-time inventory demand source.
   Multiply ReturnedQuantity by parent product quantity in the stock posting
   transaction. Apply preparation-type / deduction-stage rules before posting.
   Do not filter by IsFree: free add-ons still consume raw materials. */
CREATE OR ALTER PROCEDURE dbo.prdGet_AddonInventoryDemand
    @Aid int,
    @AddonQuantity decimal(18,6),
    @ParentProductQuantity decimal(18,6),
    @BranchId int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        d.Aid,
        d.ItemId,
        d.Unit,
        d.DeductionStage,
        a.FoodPreparationType,
        d.Quantity * @AddonQuantity * @ParentProductQuantity AS RequiredQuantity
    FROM dbo.tblAddonIngredientDetails d
    INNER JOIN dbo.tblAddon a ON a.Aid = d.Aid
    WHERE d.Aid = @Aid
      AND d.BranchId = @BranchId
      AND d.Status = 1
      AND a.Status = 1;
    -- An active recipe is the inventory-tracking switch. This keeps legacy
    -- add-ons without recipe rows out of stock deduction automatically.
END;
GO
