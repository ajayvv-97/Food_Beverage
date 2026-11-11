USE [FoodBeverage];
GO

/*
 Party Booking KOT per-detail stock processing.
 ProductType=9 = food/menu detail.
 tblProduct.PreparationMode: 1 Kitchen, 2 Counter.
 FoodPreparationType: 1 MadeToOrder, 2 BatchPrepared, 3 PartiallyPrepared, 4 NotApplicable.
 MadeToOrder deducts all active ProductType=1 recipe rows.
 PartiallyPrepared deducts only Deductionstage=1 (matches prdInsert_Upd_SalesDetails).
 BatchPrepared and NotApplicable are skipped here.
 Stock calculation matches prdInsert_Upd_SalesDetails:
 recipe unit conversion via tblItem.unit/Baseunit/Conversionfactor;
 SaleType=0; FIFO tblProductRate.Id; QOH stored in purchase units.
*/

IF OBJECT_ID(N'dbo.tblPartyBookingKOTStockLog', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblPartyBookingKOTStockLog
    (
        PartyBookingKOTStockLogId BIGINT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_tblPartyBookingKOTStockLog PRIMARY KEY,
        PartyBookingId INT NOT NULL,
        PartyBookingDetailsId INT NOT NULL,
        BranchId INT NOT NULL,
        ProductId INT NULL,
        FoodPreparationType INT NULL,
        StockStatus TINYINT NOT NULL
            CONSTRAINT DF_tblPartyBookingKOTStockLog_StockStatus DEFAULT (0),
        StatusMessage NVARCHAR(2000) NULL,
        RequiredBaseQty DECIMAL(18,4) NULL,
        KOTQueued BIT NOT NULL
            CONSTRAINT DF_tblPartyBookingKOTStockLog_KOTQueued DEFAULT (0),
        KOTStatusMessage NVARCHAR(2000) NULL,
        AttemptCount INT NOT NULL
            CONSTRAINT DF_tblPartyBookingKOTStockLog_AttemptCount DEFAULT (0),
        CreatedBy INT NULL,
        CreatedOn DATETIME NOT NULL
            CONSTRAINT DF_tblPartyBookingKOTStockLog_CreatedOn DEFAULT (GETDATE()),
        ModifiedBy INT NULL,
        ModifiedOn DATETIME NULL
    );

    CREATE UNIQUE INDEX UX_tblPartyBookingKOTStockLog_BookingDetail
        ON dbo.tblPartyBookingKOTStockLog(PartyBookingId, PartyBookingDetailsId);

    CREATE INDEX IX_tblPartyBookingKOTStockLog_Retry
        ON dbo.tblPartyBookingKOTStockLog(PartyBookingId, BranchId, StockStatus);
END;
GO

/*
 StockStatus:
 0 Pending; 1 Deducted; 2 No applicable recipe;
 3 BatchPrepared skipped; 4 NotApplicable skipped; 5 invalid product/preparation;
 6 invalid recipe/item/unit; 7 no active rate rows; 8 insufficient stock;
 9 retryable unexpected failure.
*/
CREATE OR ALTER PROCEDURE dbo.prdPartyBooking_DeductKOTDetailStock
    @PartyBookingId INT,
    @PartyBookingDetailsId INT,
    @BranchId INT,
    @EmployeeId INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @DetailFound BIT = 0, @ProductFound BIT = 0;
    DECLARE @ProductId INT = NULL, @SaleQty DECIMAL(18,4) = 0;
    DECLARE @FoodPreparationType INT = NULL, @ExistingStatus TINYINT = NULL;
    DECLARE @RequiredBaseTotal DECIMAL(18,4) = 0;
    DECLARE @Message NVARCHAR(2000);

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

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.tblPartyBookingKOTStockLog WITH (UPDLOCK, HOLDLOCK)
            WHERE PartyBookingId = @PartyBookingId
              AND PartyBookingDetailsId = @PartyBookingDetailsId
        )
        BEGIN
            INSERT INTO dbo.tblPartyBookingKOTStockLog
            (
                PartyBookingId, PartyBookingDetailsId, BranchId,
                StockStatus, CreatedBy
            )
            VALUES (@PartyBookingId, @PartyBookingDetailsId, @BranchId, 0, @EmployeeId);
        END;

        SELECT @ExistingStatus = StockStatus
        FROM dbo.tblPartyBookingKOTStockLog WITH (UPDLOCK, HOLDLOCK)
        WHERE PartyBookingId = @PartyBookingId
          AND PartyBookingDetailsId = @PartyBookingDetailsId
          AND BranchId = @BranchId;

        IF ISNULL(@ExistingStatus, 0) NOT IN (0, 9)
        BEGIN
            COMMIT TRANSACTION;
            RETURN;
        END;

        SELECT
            @DetailFound = 1,
            @ProductId = bd.ProductId,
            @SaleQty = CONVERT(DECIMAL(18,4), ISNULL(bd.Qty, 0))
        FROM dbo.tblPartyBookingDetails bd WITH (UPDLOCK, HOLDLOCK)
        WHERE bd.PartyBookingId = @PartyBookingId
          AND bd.PartyBookingDetailsId = @PartyBookingDetailsId
          AND bd.BranchId = @BranchId
          AND bd.ProductType = 9
          AND bd.Status = 1;

        IF @DetailFound = 0 OR ISNULL(@ProductId, 0) <= 0 OR @SaleQty <= 0
        BEGIN
            UPDATE dbo.tblPartyBookingKOTStockLog
            SET StockStatus = 5,
                StatusMessage = N'Skipped: active ProductType=9 detail, ProductId or positive quantity is missing.',
                RequiredBaseQty = 0,
                AttemptCount = AttemptCount + 1,
                ModifiedBy = @EmployeeId,
                ModifiedOn = GETDATE()
            WHERE PartyBookingId = @PartyBookingId
              AND PartyBookingDetailsId = @PartyBookingDetailsId
              AND BranchId = @BranchId;
            COMMIT TRANSACTION;
            RETURN;
        END;

        SELECT
            @ProductFound = 1,
            @FoodPreparationType = p.FoodPreparationType
        FROM dbo.tblProduct p WITH (UPDLOCK, HOLDLOCK)
        WHERE p.ProductId = @ProductId
          AND p.Status = 1;

        IF @ProductFound = 0
        BEGIN
            UPDATE dbo.tblPartyBookingKOTStockLog
            SET ProductId = @ProductId,
                StockStatus = 5,
                StatusMessage = N'Skipped: menu product is missing or inactive in tblProduct.',
                RequiredBaseQty = 0,
                AttemptCount = AttemptCount + 1,
                ModifiedBy = @EmployeeId,
                ModifiedOn = GETDATE()
            WHERE PartyBookingId = @PartyBookingId
              AND PartyBookingDetailsId = @PartyBookingDetailsId
              AND BranchId = @BranchId;
            COMMIT TRANSACTION;
            RETURN;
        END;

        IF @FoodPreparationType = 2
        BEGIN
            UPDATE dbo.tblPartyBookingKOTStockLog
            SET ProductId = @ProductId, FoodPreparationType = 2, StockStatus = 3,
                StatusMessage = N'Skipped: BatchPrepared stock is handled by batch preparation.',
                RequiredBaseQty = 0, AttemptCount = AttemptCount + 1,
                ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
            WHERE PartyBookingId = @PartyBookingId
              AND PartyBookingDetailsId = @PartyBookingDetailsId
              AND BranchId = @BranchId;
            COMMIT TRANSACTION;
            RETURN;
        END;

        IF @FoodPreparationType = 4
        BEGIN
            UPDATE dbo.tblPartyBookingKOTStockLog
            SET ProductId = @ProductId, FoodPreparationType = 4, StockStatus = 4,
                StatusMessage = N'Skipped: FoodPreparationType is NotApplicable.',
                RequiredBaseQty = 0, AttemptCount = AttemptCount + 1,
                ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
            WHERE PartyBookingId = @PartyBookingId
              AND PartyBookingDetailsId = @PartyBookingDetailsId
              AND BranchId = @BranchId;
            COMMIT TRANSACTION;
            RETURN;
        END;

        IF ISNULL(@FoodPreparationType, 0) NOT IN (1, 3)
        BEGIN
            UPDATE dbo.tblPartyBookingKOTStockLog
            SET ProductId = @ProductId,
                FoodPreparationType = ISNULL(@FoodPreparationType, 0),
                StockStatus = 5,
                StatusMessage = N'Skipped: invalid FoodPreparationType. Expected 1, 2, 3 or 4.',
                RequiredBaseQty = 0,
                AttemptCount = AttemptCount + 1,
                ModifiedBy = @EmployeeId,
                ModifiedOn = GETDATE()
            WHERE PartyBookingId = @PartyBookingId
              AND PartyBookingDetailsId = @PartyBookingDetailsId
              AND BranchId = @BranchId;
            COMMIT TRANSACTION;
            RETURN;
        END;

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.tblIngredientDetails d WITH (UPDLOCK, HOLDLOCK)
            WHERE d.ProductId = @ProductId
              AND d.ProductType = 1
              AND ISNULL(d.Status, 1) = 1
              AND
              (
                  @FoodPreparationType = 1
                  OR (@FoodPreparationType = 3 AND ISNULL(d.Deductionstage, 0) = 1)
              )
        )
        BEGIN
            UPDATE dbo.tblPartyBookingKOTStockLog
            SET ProductId = @ProductId,
                FoodPreparationType = @FoodPreparationType,
                StockStatus = 2,
                StatusMessage = CASE
                    WHEN @FoodPreparationType = 1
                        THEN N'Skipped: MadeToOrder product has no active raw-material recipe.'
                    ELSE N'Skipped: PartiallyPrepared product has no active stage-1 raw-material recipe.'
                END,
                RequiredBaseQty = 0,
                AttemptCount = AttemptCount + 1,
                ModifiedBy = @EmployeeId,
                ModifiedOn = GETDATE()
            WHERE PartyBookingId = @PartyBookingId
              AND PartyBookingDetailsId = @PartyBookingDetailsId
              AND BranchId = @BranchId;
            COMMIT TRANSACTION;
            RETURN;
        END;

        IF EXISTS
        (
            SELECT 1
            FROM dbo.tblIngredientDetails d
            LEFT JOIN dbo.tblItem i ON i.ItemId = d.ItemId
            WHERE d.ProductId = @ProductId
              AND d.ProductType = 1
              AND ISNULL(d.Status, 1) = 1
              AND
              (
                  @FoodPreparationType = 1
                  OR (@FoodPreparationType = 3 AND ISNULL(d.Deductionstage, 0) = 1)
              )
              AND
              (
                  ISNULL(d.Quantity, 0) <= 0
                  OR d.Unit IS NULL
                  OR i.ItemId IS NULL
                  OR ISNULL(i.Status, 0) <> 1
                  OR ISNULL(i.Saletype, -1) <> 0
                  OR i.Baseunit IS NULL
                  OR (d.Unit <> i.unit AND d.Unit <> i.Baseunit)
                  OR
                  (
                      ISNULL(i.Baseunit, i.unit) <> i.unit
                      AND ISNULL(i.Conversionfactor, 0) <= 0
                  )
              )
        )
        BEGIN
            UPDATE dbo.tblPartyBookingKOTStockLog
            SET ProductId = @ProductId, FoodPreparationType = @FoodPreparationType,
                StockStatus = 6,
                StatusMessage = N'Skipped: one or more recipe quantities, units, items, SaleType, Baseunit or Conversionfactor are invalid.',
                RequiredBaseQty = 0, AttemptCount = AttemptCount + 1,
                ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
            WHERE PartyBookingId = @PartyBookingId
              AND PartyBookingDetailsId = @PartyBookingDetailsId
              AND BranchId = @BranchId;
            COMMIT TRANSACTION;
            RETURN;
        END;

        INSERT INTO @RequiredStock(ItemId, RequiredBaseQty)
        SELECT
            d.ItemId,
            CONVERT
            (
                DECIMAL(18,4),
                SUM
                (
                    d.Quantity * @SaleQty *
                    CASE
                        WHEN d.Unit = i.unit
                         AND ISNULL(i.Baseunit, i.unit) <> i.unit
                            THEN i.Conversionfactor
                        ELSE 1
                    END
                )
            )
        FROM dbo.tblIngredientDetails d
        INNER JOIN dbo.tblItem i ON i.ItemId = d.ItemId
        WHERE d.ProductId = @ProductId
          AND d.ProductType = 1
          AND ISNULL(d.Status, 1) = 1
          AND
          (
              @FoodPreparationType = 1
              OR (@FoodPreparationType = 3 AND ISNULL(d.Deductionstage, 0) = 1)
          )
        GROUP BY d.ItemId
        HAVING SUM
        (
            d.Quantity * @SaleQty *
            CASE
                WHEN d.Unit = i.unit
                 AND ISNULL(i.Baseunit, i.unit) <> i.unit
                    THEN i.Conversionfactor
                ELSE 1
            END
        ) > 0;

        IF NOT EXISTS (SELECT 1 FROM @RequiredStock)
        BEGIN
            UPDATE dbo.tblPartyBookingKOTStockLog
            SET ProductId = @ProductId, FoodPreparationType = @FoodPreparationType,
                StockStatus = 2,
                StatusMessage = N'Skipped: no positive raw-material quantity was generated.',
                RequiredBaseQty = 0, AttemptCount = AttemptCount + 1,
                ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
            WHERE PartyBookingId = @PartyBookingId
              AND PartyBookingDetailsId = @PartyBookingDetailsId
              AND BranchId = @BranchId;
            COMMIT TRANSACTION;
            RETURN;
        END;

        SET @RequiredBaseTotal = (SELECT SUM(RequiredBaseQty) FROM @RequiredStock);

        INSERT INTO @StockToDeduct(ItemId, RequiredQty, ConversionFactor)
        SELECT
            r.ItemId,
            r.RequiredBaseQty,
            CONVERT(DECIMAL(18,4),
                CASE WHEN i.Baseunit = i.unit THEN 1 ELSE i.Conversionfactor END)
        FROM @RequiredStock r
        INNER JOIN dbo.tblItem i
            ON i.ItemId = r.ItemId
           AND i.Status = 1
           AND i.Saletype = 0;

        IF EXISTS
        (
            SELECT 1
            FROM @RequiredStock r
            LEFT JOIN @StockToDeduct s ON s.ItemId = r.ItemId
            WHERE s.ItemId IS NULL OR ISNULL(s.ConversionFactor, 0) <= 0
        )
        BEGIN
            UPDATE dbo.tblPartyBookingKOTStockLog
            SET ProductId = @ProductId, FoodPreparationType = @FoodPreparationType,
                StockStatus = 6,
                StatusMessage = N'Skipped: a required material is inactive, missing, not SaleType=0, or has invalid stock conversion.',
                RequiredBaseQty = @RequiredBaseTotal, AttemptCount = AttemptCount + 1,
                ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
            WHERE PartyBookingId = @PartyBookingId
              AND PartyBookingDetailsId = @PartyBookingDetailsId
              AND BranchId = @BranchId;
            COMMIT TRANSACTION;
            RETURN;
        END;

        UPDATE stock
        SET ActiveRateCount = rates.RateCount,
            AvailableBaseQty = CONVERT(DECIMAL(18,4),
                ISNULL(rates.AvailablePurchaseQty, 0) * stock.ConversionFactor)
        FROM @StockToDeduct stock
        OUTER APPLY
        (
            SELECT COUNT(*) AS RateCount,
                   SUM(CASE WHEN ISNULL(pr.QOH, 0) > 0
                            THEN CONVERT(DECIMAL(18,4), pr.QOH)
                            ELSE CONVERT(DECIMAL(18,4), 0) END) AS AvailablePurchaseQty
            FROM dbo.tblProductRate pr WITH (UPDLOCK, HOLDLOCK)
            WHERE pr.ProductId = stock.ItemId
              AND pr.Status = 1
        ) rates;

        IF EXISTS (SELECT 1 FROM @StockToDeduct WHERE ISNULL(ActiveRateCount, 0) = 0)
        BEGIN
            UPDATE dbo.tblPartyBookingKOTStockLog
            SET ProductId = @ProductId, FoodPreparationType = @FoodPreparationType,
                StockStatus = 7,
                StatusMessage = N'Skipped: no active tblProductRate rows exist for a required raw material.',
                RequiredBaseQty = @RequiredBaseTotal, AttemptCount = AttemptCount + 1,
                ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
            WHERE PartyBookingId = @PartyBookingId
              AND PartyBookingDetailsId = @PartyBookingDetailsId
              AND BranchId = @BranchId;
            COMMIT TRANSACTION;
            RETURN;
        END;

        IF EXISTS
        (
            SELECT 1 FROM @StockToDeduct
            WHERE ISNULL(AvailableBaseQty, 0) < RequiredQty
        )
        BEGIN
            UPDATE dbo.tblPartyBookingKOTStockLog
            SET ProductId = @ProductId, FoodPreparationType = @FoodPreparationType,
                StockStatus = 8,
                StatusMessage = N'Skipped: insufficient raw-material stock. No partial deduction was made for this product.',
                RequiredBaseQty = @RequiredBaseTotal, AttemptCount = AttemptCount + 1,
                ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
            WHERE PartyBookingId = @PartyBookingId
              AND PartyBookingDetailsId = @PartyBookingDetailsId
              AND BranchId = @BranchId;
            COMMIT TRANSACTION;
            RETURN;
        END;

        ;WITH RateRows AS
        (
            SELECT
                pr.Id,
                pr.ProductId,
                stock.ConversionFactor,
                CONVERT(DECIMAL(18,4),
                    CASE WHEN ISNULL(pr.QOH, 0) > 0
                         THEN pr.QOH * stock.ConversionFactor ELSE 0 END) AS RowQtyBase,
                stock.RequiredQty,
                ISNULL
                (
                    SUM(CONVERT(DECIMAL(18,4),
                        CASE WHEN ISNULL(pr.QOH, 0) > 0
                             THEN pr.QOH * stock.ConversionFactor ELSE 0 END))
                    OVER
                    (
                        PARTITION BY pr.ProductId
                        ORDER BY pr.Id
                        ROWS BETWEEN UNBOUNDED PRECEDING AND 1 PRECEDING
                    ),
                    0
                ) AS QtyBeforeBase
            FROM dbo.tblProductRate pr WITH (UPDLOCK, HOLDLOCK)
            INNER JOIN @StockToDeduct stock ON stock.ItemId = pr.ProductId
            WHERE pr.Status = 1
        ),
        RateDeductions AS
        (
            SELECT
                Id,
                ConversionFactor,
                CASE
                    WHEN RowQtyBase <= 0 OR QtyBeforeBase >= RequiredQty THEN 0
                    WHEN QtyBeforeBase + RowQtyBase <= RequiredQty THEN RowQtyBase
                    ELSE RequiredQty - QtyBeforeBase
                END AS DeductQtyBase
            FROM RateRows
        )
        UPDATE pr
        SET pr.QOH = CONVERT
        (
            DECIMAL(18,4),
            (
                (CONVERT(DECIMAL(18,8), ISNULL(pr.QOH, 0)) * d.ConversionFactor)
                - d.DeductQtyBase
            ) / NULLIF(d.ConversionFactor, 0)
        )
        FROM dbo.tblProductRate pr
        INNER JOIN RateDeductions d ON d.Id = pr.Id
        WHERE d.DeductQtyBase > 0;

        UPDATE dbo.tblPartyBookingKOTStockLog
        SET ProductId = @ProductId,
            FoodPreparationType = @FoodPreparationType,
            StockStatus = 1,
            StatusMessage = N'Raw materials deducted successfully using FIFO allocation.',
            RequiredBaseQty = @RequiredBaseTotal,
            AttemptCount = AttemptCount + 1,
            ModifiedBy = @EmployeeId,
            ModifiedOn = GETDATE()
        WHERE PartyBookingId = @PartyBookingId
          AND PartyBookingDetailsId = @PartyBookingDetailsId
          AND BranchId = @BranchId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0
            ROLLBACK TRANSACTION;

        SET @Message = LEFT(
            N'Per-product deduction failed; retry will be attempted. ' +
            ISNULL(ERROR_MESSAGE(), N'Unknown SQL error.'),
            2000);

        BEGIN TRY
            IF NOT EXISTS
            (
                SELECT 1 FROM dbo.tblPartyBookingKOTStockLog
                WHERE PartyBookingId = @PartyBookingId
                  AND PartyBookingDetailsId = @PartyBookingDetailsId
                  AND BranchId = @BranchId
            )
            BEGIN
                INSERT INTO dbo.tblPartyBookingKOTStockLog
                (
                    PartyBookingId, PartyBookingDetailsId, BranchId,
                    ProductId, StockStatus, StatusMessage, AttemptCount,
                    CreatedBy, ModifiedBy, ModifiedOn
                )
                VALUES
                (
                    @PartyBookingId, @PartyBookingDetailsId, @BranchId,
                    @ProductId, 9, @Message, 1, @EmployeeId, @EmployeeId, GETDATE()
                );
            END
            ELSE
            BEGIN
                UPDATE dbo.tblPartyBookingKOTStockLog
                SET ProductId = COALESCE(@ProductId, ProductId),
                    FoodPreparationType = COALESCE(@FoodPreparationType, FoodPreparationType),
                    StockStatus = 9,
                    StatusMessage = @Message,
                    AttemptCount = AttemptCount + 1,
                    ModifiedBy = @EmployeeId,
                    ModifiedOn = GETDATE()
                WHERE PartyBookingId = @PartyBookingId
                  AND PartyBookingDetailsId = @PartyBookingDetailsId
                  AND BranchId = @BranchId;
            END;
        END TRY
        BEGIN CATCH
            /* Retry remains visible if an audit write itself fails. */
        END CATCH;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.prdPartyBooking_ProcessScheduledKOT
    @PartyBookingId INT,
    @BranchId INT,
    @EmployeeId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @LockResult INT = -999;
    DECLARE @LockResource NVARCHAR(255) =
        N'PartyBookingKOT:' + CONVERT(NVARCHAR(20), @BranchId) +
        N':' + CONVERT(NVARCHAR(20), @PartyBookingId);
    DECLARE @BookingStatus INT = NULL;
    DECLARE @KOTDatetime DATETIME = NULL;
    DECLARE @ConsumedRawMaterialStatus BIT = 0;
    DECLARE @EnablePartyBookingKOT BIT = 0;
    DECLARE @PartyBookingDetailsId INT;

    EXEC @LockResult = sys.sp_getapplock
        @Resource = @LockResource,
        @LockMode = 'Exclusive',
        @LockOwner = 'Session',
        @LockTimeout = 0;

    IF @LockResult < 0
        RETURN;

    BEGIN TRY
        SELECT
            @BookingStatus = b.Status,
            @KOTDatetime = b.KOTDatetime,
            @ConsumedRawMaterialStatus = ISNULL(b.ConsumedRawMaterialStatus, 0)
        FROM dbo.tblPartyBooking b WITH (UPDLOCK, HOLDLOCK)
        WHERE b.PartyBookingId = @PartyBookingId
          AND b.BranchId = @BranchId;

        IF ISNULL(@BookingStatus, 0) <> 1
            THROW 51200, 'Party booking was not found or is inactive.', 1;

        IF @KOTDatetime IS NULL OR @KOTDatetime > GETDATE()
        BEGIN
            EXEC sys.sp_releaseapplock @Resource = @LockResource, @LockOwner = 'Session';
            RETURN;
        END;

        IF ISNULL(@EmployeeId, 0) <= 0
            THROW 51201, 'A valid employee is required for Party Booking KOT processing.', 1;

        SELECT TOP (1)
            @EnablePartyBookingKOT = ISNULL(gs.EnablePartyBookingKOT, 0)
        FROM dbo.tblGeneralSetting gs
        WHERE gs.BranchId = @BranchId
          AND ISNULL(gs.Status, 1) = 1
        ORDER BY gs.Gid DESC;

        IF ISNULL(@EnablePartyBookingKOT, 0) <> 1
        BEGIN
            EXEC sys.sp_releaseapplock @Resource = @LockResource, @LockOwner = 'Session';
            RETURN;
        END;

        INSERT INTO dbo.tblPartyBookingKOTStockLog
        (
            PartyBookingId, PartyBookingDetailsId, BranchId, ProductId,
            StockStatus, StatusMessage, CreatedBy
        )
        SELECT
            bd.PartyBookingId,
            bd.PartyBookingDetailsId,
            bd.BranchId,
            bd.ProductId,
            CASE WHEN @ConsumedRawMaterialStatus = 1 THEN 1 ELSE 0 END,
            CASE WHEN @ConsumedRawMaterialStatus = 1
                 THEN N'Previously processed before per-detail log installation; no second deduction was made.'
                 ELSE N'Pending per-detail stock processing.' END,
            @EmployeeId
        FROM dbo.tblPartyBookingDetails bd
        WHERE bd.PartyBookingId = @PartyBookingId
          AND bd.BranchId = @BranchId
          AND bd.Status = 1
          AND bd.ProductType = 9
          AND ISNULL(bd.Qty, 0) > 0
          AND NOT EXISTS
          (
              SELECT 1
              FROM dbo.tblPartyBookingKOTStockLog l WITH (UPDLOCK, HOLDLOCK)
              WHERE l.PartyBookingId = bd.PartyBookingId
                AND l.PartyBookingDetailsId = bd.PartyBookingDetailsId
          );

        /* Queue every routable ProductType=9 line, independent from recipe availability. */
        INSERT INTO dbo.tblPartyBookingKOTQueue
        (
            PartyBookingId, PartyBookingDetailsId, ProductId, ProductName,
            Quantity, PreparationMode, TableType, CategoryName, CustomerName,
            Phone, EventDescription, BookingDateTime, KOTDatetime,
            NoOfAdults, NoOfChildren, BranchId, CreatedBy
        )
        SELECT
            b.PartyBookingId,
            bd.PartyBookingDetailsId,
            bd.ProductId,
            LEFT(CONVERT(NVARCHAR(250), COALESCE(NULLIF(bd.Product, N''), p.Product)), 250),
            CONVERT(DECIMAL(18,3), bd.Qty),
            p.PreparationMode,
            ISNULL(bd.TableType, 0),
            bd.CategoryName,
            b.CustomerName,
            b.Phone,
            b.EventDescription,
            b.BookingDateTime,
            b.KOTDatetime,
            ISNULL(b.NoOfAdults, 0),
            ISNULL(b.NoOfChildren, 0),
            b.BranchId,
            @EmployeeId
        FROM dbo.tblPartyBooking b
        INNER JOIN dbo.tblPartyBookingDetails bd
            ON bd.PartyBookingId = b.PartyBookingId
           AND bd.BranchId = b.BranchId
           AND bd.Status = 1
           AND bd.ProductType = 9
           AND bd.ProductId > 0
           AND ISNULL(bd.Qty, 0) > 0
        INNER JOIN dbo.tblProduct p
            ON p.ProductId = bd.ProductId
           AND p.Status = 1
           AND p.PreparationMode IN (1, 2)
        WHERE b.PartyBookingId = @PartyBookingId
          AND b.BranchId = @BranchId
          AND b.Status = 1
          AND b.KOTDatetime IS NOT NULL
          AND NOT EXISTS
          (
              SELECT 1
              FROM dbo.tblPartyBookingKOTQueue q WITH (UPDLOCK, HOLDLOCK)
              WHERE q.PartyBookingId = bd.PartyBookingId
                AND q.PartyBookingDetailsId = bd.PartyBookingDetailsId
                AND q.PreparationMode = p.PreparationMode
          );

        UPDATE l
        SET
            KOTQueued = CASE WHEN EXISTS
            (
                SELECT 1 FROM dbo.tblPartyBookingKOTQueue q
                WHERE q.PartyBookingId = l.PartyBookingId
                  AND q.PartyBookingDetailsId = l.PartyBookingDetailsId
                  AND q.BranchId = l.BranchId
            ) THEN 1 ELSE 0 END,
            KOTStatusMessage = CASE
                WHEN EXISTS
                (
                    SELECT 1 FROM dbo.tblPartyBookingKOTQueue q
                    WHERE q.PartyBookingId = l.PartyBookingId
                      AND q.PartyBookingDetailsId = l.PartyBookingDetailsId
                      AND q.BranchId = l.BranchId
                ) THEN NULL
                WHEN p.ProductId IS NULL THEN N'KOT skipped: product is missing or inactive.'
                WHEN p.PreparationMode NOT IN (1, 2) OR p.PreparationMode IS NULL
                    THEN N'KOT skipped: set tblProduct.PreparationMode to 1 (Kitchen) or 2 (Counter).'
                ELSE N'KOT was not queued for this detail.'
            END,
            ModifiedBy = @EmployeeId,
            ModifiedOn = GETDATE()
        FROM dbo.tblPartyBookingKOTStockLog l
        INNER JOIN dbo.tblPartyBookingDetails bd
            ON bd.PartyBookingId = l.PartyBookingId
           AND bd.PartyBookingDetailsId = l.PartyBookingDetailsId
           AND bd.BranchId = l.BranchId
        LEFT JOIN dbo.tblProduct p
            ON p.ProductId = bd.ProductId
           AND p.Status = 1
        WHERE l.PartyBookingId = @PartyBookingId
          AND l.BranchId = @BranchId;

        IF @ConsumedRawMaterialStatus = 0
        BEGIN
            DECLARE detail_cursor CURSOR LOCAL FAST_FORWARD FOR
                SELECT l.PartyBookingDetailsId
                FROM dbo.tblPartyBookingKOTStockLog l
                INNER JOIN dbo.tblPartyBookingDetails bd
                    ON bd.PartyBookingId = l.PartyBookingId
                   AND bd.PartyBookingDetailsId = l.PartyBookingDetailsId
                   AND bd.BranchId = l.BranchId
                WHERE l.PartyBookingId = @PartyBookingId
                  AND l.BranchId = @BranchId
                  AND l.StockStatus IN (0, 9)
                  AND bd.Status = 1
                  AND bd.ProductType = 9
                  AND ISNULL(bd.Qty, 0) > 0
                ORDER BY l.PartyBookingDetailsId;

            OPEN detail_cursor;
            FETCH NEXT FROM detail_cursor INTO @PartyBookingDetailsId;
            WHILE @@FETCH_STATUS = 0
            BEGIN
                EXEC dbo.prdPartyBooking_DeductKOTDetailStock
                    @PartyBookingId = @PartyBookingId,
                    @PartyBookingDetailsId = @PartyBookingDetailsId,
                    @BranchId = @BranchId,
                    @EmployeeId = @EmployeeId;

                FETCH NEXT FROM detail_cursor INTO @PartyBookingDetailsId;
            END;
            CLOSE detail_cursor;
            DEALLOCATE detail_cursor;
        END;

        UPDATE dbo.tblPartyBooking
        SET
            ConsumedRawMaterialStatus = CASE
                WHEN NOT EXISTS
                (
                    SELECT 1 FROM dbo.tblPartyBookingKOTStockLog l
                    WHERE l.PartyBookingId = @PartyBookingId
                      AND l.BranchId = @BranchId
                      AND l.StockStatus IN (0, 9)
                ) THEN 1 ELSE 0 END,
            KoTPrintStatus_Kitchen = CASE
                WHEN EXISTS
                (
                    SELECT 1
                    FROM dbo.tblPartyBookingDetails bd
                    LEFT JOIN dbo.tblProduct p
                        ON p.ProductId = bd.ProductId AND p.Status = 1
                    WHERE bd.PartyBookingId = @PartyBookingId
                      AND bd.BranchId = @BranchId
                      AND bd.Status = 1
                      AND bd.ProductType = 9
                      AND ISNULL(bd.Qty, 0) > 0
                      AND (p.ProductId IS NULL OR p.PreparationMode NOT IN (1, 2) OR p.PreparationMode IS NULL)
                ) THEN 0
                WHEN EXISTS
                (
                    SELECT 1 FROM dbo.tblPartyBookingKOTQueue q
                    WHERE q.PartyBookingId = @PartyBookingId
                      AND q.BranchId = @BranchId
                      AND q.PreparationMode = 1
                      AND q.Status <> 2
                ) THEN 0 ELSE 1 END,
            KoTPrintStatus_counter = CASE
                WHEN EXISTS
                (
                    SELECT 1
                    FROM dbo.tblPartyBookingDetails bd
                    LEFT JOIN dbo.tblProduct p
                        ON p.ProductId = bd.ProductId AND p.Status = 1
                    WHERE bd.PartyBookingId = @PartyBookingId
                      AND bd.BranchId = @BranchId
                      AND bd.Status = 1
                      AND bd.ProductType = 9
                      AND ISNULL(bd.Qty, 0) > 0
                      AND (p.ProductId IS NULL OR p.PreparationMode NOT IN (1, 2) OR p.PreparationMode IS NULL)
                ) THEN 0
                WHEN EXISTS
                (
                    SELECT 1 FROM dbo.tblPartyBookingKOTQueue q
                    WHERE q.PartyBookingId = @PartyBookingId
                      AND q.BranchId = @BranchId
                      AND q.PreparationMode = 2
                      AND q.Status <> 2
                ) THEN 0 ELSE 1 END
        WHERE PartyBookingId = @PartyBookingId
          AND BranchId = @BranchId;

        EXEC sys.sp_releaseapplock @Resource = @LockResource, @LockOwner = 'Session';

        SELECT
            b.PartyBookingId,
            b.ConsumedRawMaterialStatus,
            b.KoTPrintStatus_Kitchen,
            b.KoTPrintStatus_counter,
            b.KOTDatetime,
            (SELECT COUNT(*) FROM dbo.tblPartyBookingKOTStockLog l
             WHERE l.PartyBookingId = b.PartyBookingId AND l.BranchId = b.BranchId
               AND l.StockStatus = 1) AS DeductedDetailCount,
            (SELECT COUNT(*) FROM dbo.tblPartyBookingKOTStockLog l
             WHERE l.PartyBookingId = b.PartyBookingId AND l.BranchId = b.BranchId
               AND l.StockStatus BETWEEN 2 AND 8) AS SkippedDetailCount,
            (SELECT COUNT(*) FROM dbo.tblPartyBookingKOTStockLog l
             WHERE l.PartyBookingId = b.PartyBookingId AND l.BranchId = b.BranchId
               AND l.StockStatus = 9) AS RetryDetailCount
        FROM dbo.tblPartyBooking b
        WHERE b.PartyBookingId = @PartyBookingId
          AND b.BranchId = @BranchId;
    END TRY
    BEGIN CATCH
        EXEC sys.sp_releaseapplock @Resource = @LockResource, @LockOwner = 'Session';
        THROW;
    END CATCH;
END;
GO

/* Review per-detail deductions, skips and KOT routing after deployment. */
SELECT TOP (100)
    l.PartyBookingId,
    l.PartyBookingDetailsId,
    l.ProductId,
    p.Product,
    l.FoodPreparationType,
    l.StockStatus,
    l.StatusMessage,
    l.RequiredBaseQty,
    l.KOTQueued,
    p.PreparationMode,
    l.KOTStatusMessage,
    l.AttemptCount,
    l.ModifiedOn
FROM dbo.tblPartyBookingKOTStockLog l
LEFT JOIN dbo.tblProduct p ON p.ProductId = l.ProductId
ORDER BY l.PartyBookingKOTStockLogId DESC;
GO
