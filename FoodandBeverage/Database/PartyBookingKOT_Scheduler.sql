USE [FoodBeverage];
GO

/*
    Party Booking KOT routing and workstation registration.

    tblProduct.PreparationMode:
        1 = Kitchen
        2 = Counter

    tblKOTWorkstation.StationType:
        1 = Kitchen workstation
        2 = Counter workstation
        3 = Office workstation (cannot claim KOT jobs)

    Queue status:
        0 = Pending
        1 = Claimed
        2 = Printed

    Stock is consumed once by prdPartyBooking_ProcessScheduledKOT.
    Printing procedures only update print/queue status. They never deduct stock.
*/

IF COL_LENGTH('dbo.tblPartyBooking', 'KoTPrintStatus_Kitchen') IS NULL
BEGIN
    ALTER TABLE dbo.tblPartyBooking
        ADD KoTPrintStatus_Kitchen BIT NOT NULL
            CONSTRAINT DF_tblPartyBooking_KoTPrintStatus_Kitchen DEFAULT (0) WITH VALUES;
END;
GO

IF COL_LENGTH('dbo.tblPartyBooking', 'KoTPrintStatus_counter') IS NULL
BEGIN
    ALTER TABLE dbo.tblPartyBooking
        ADD KoTPrintStatus_counter BIT NOT NULL
            CONSTRAINT DF_tblPartyBooking_KoTPrintStatus_counter DEFAULT (0) WITH VALUES;
END;
GO

/* Migrate bookings already completed by the previous combined KOT status. */
UPDATE dbo.tblPartyBooking
SET KoTPrintStatus_Kitchen = 1,
    KoTPrintStatus_counter = 1
WHERE ISNULL(KoTPrintStatus, 0) = 1
  AND
  (
      ISNULL(KoTPrintStatus_Kitchen, 0) = 0
      OR ISNULL(KoTPrintStatus_counter, 0) = 0
  );
GO

IF OBJECT_ID('dbo.tblKOTWorkstation', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblKOTWorkstation
    (
        KOTWorkstationId INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_tblKOTWorkstation PRIMARY KEY,
        BranchId INT NOT NULL,
        MachineName NVARCHAR(128) NOT NULL,
        StationType TINYINT NOT NULL,
        IsActive BIT NOT NULL
            CONSTRAINT DF_tblKOTWorkstation_IsActive DEFAULT (1),
        RegisteredBy INT NOT NULL,
        RegisteredOn DATETIME NOT NULL
            CONSTRAINT DF_tblKOTWorkstation_RegisteredOn DEFAULT (GETDATE()),
        ModifiedBy INT NULL,
        ModifiedOn DATETIME NULL,
        CONSTRAINT CK_tblKOTWorkstation_StationType
            CHECK (StationType IN (1, 2, 3))
    );

    CREATE UNIQUE INDEX UX_tblKOTWorkstation_BranchMachine
        ON dbo.tblKOTWorkstation(BranchId, MachineName);
    CREATE INDEX IX_tblKOTWorkstation_BranchStation
        ON dbo.tblKOTWorkstation(BranchId, StationType, IsActive);
END;
GO

IF OBJECT_ID('dbo.tblPartyBookingKOTQueue', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblPartyBookingKOTQueue
    (
        PartyBookingKOTQueueId BIGINT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_tblPartyBookingKOTQueue PRIMARY KEY,
        PartyBookingId INT NOT NULL,
        PartyBookingDetailsId INT NOT NULL,
        ProductId INT NOT NULL,
        ProductName NVARCHAR(250) NOT NULL,
        Quantity DECIMAL(18,3) NOT NULL,
        PreparationMode INT NOT NULL,
        TableType INT NOT NULL
            CONSTRAINT DF_PartyBookingKOTQueue_TableType DEFAULT (0),
        CategoryName NVARCHAR(150) NULL,
        CustomerName NVARCHAR(200) NULL,
        Phone NVARCHAR(40) NULL,
        EventDescription NVARCHAR(1000) NULL,
        BookingDateTime DATETIME NULL,
        KOTDatetime DATETIME NOT NULL,
        NoOfAdults INT NOT NULL
            CONSTRAINT DF_PartyBookingKOTQueue_Adults DEFAULT (0),
        NoOfChildren INT NOT NULL
            CONSTRAINT DF_PartyBookingKOTQueue_Children DEFAULT (0),
        BranchId INT NOT NULL,
        CreatedBy INT NOT NULL,
        CreatedOn DATETIME NOT NULL
            CONSTRAINT DF_PartyBookingKOTQueue_CreatedOn DEFAULT (GETDATE()),
        Status TINYINT NOT NULL
            CONSTRAINT DF_PartyBookingKOTQueue_Status DEFAULT (0),
        ClaimedOn DATETIME NULL,
        PrintedOn DATETIME NULL,
        PrintedBy INT NULL,
        PrintMachine NVARCHAR(128) NULL,
        LastError NVARCHAR(2000) NULL
    );

    CREATE UNIQUE INDEX UX_PartyBookingKOTQueue_BookingDetailRoute
        ON dbo.tblPartyBookingKOTQueue
        (
            PartyBookingId,
            PartyBookingDetailsId,
            PreparationMode
        );

    CREATE INDEX IX_PartyBookingKOTQueue_PendingRoute
        ON dbo.tblPartyBookingKOTQueue
        (
            BranchId,
            PreparationMode,
            Status,
            KOTDatetime
        );
END;
GO

CREATE OR ALTER PROCEDURE dbo.prdKOTWorkstation_Register
    @BranchId INT,
    @MachineName NVARCHAR(128),
    @StationType TINYINT,
    @EmployeeId INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF ISNULL(@BranchId, 0) <= 0
        THROW 51100, 'A valid branch is required.', 1;

    IF NULLIF(LTRIM(RTRIM(@MachineName)), '') IS NULL
        THROW 51101, 'A valid machine name is required.', 1;

    IF @StationType NOT IN (1, 2, 3)
        THROW 51102, 'Station type must be Kitchen, Counter, or Office.', 1;

    IF ISNULL(@EmployeeId, 0) <= 0
        THROW 51103, 'A valid employee is required to register this workstation.', 1;

    BEGIN TRANSACTION;

    UPDATE dbo.tblKOTWorkstation WITH (UPDLOCK, HOLDLOCK)
    SET StationType = @StationType,
        IsActive = 1,
        ModifiedBy = @EmployeeId,
        ModifiedOn = GETDATE()
    WHERE BranchId = @BranchId
      AND MachineName = @MachineName;

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO dbo.tblKOTWorkstation
        (
            BranchId, MachineName, StationType,
            IsActive, RegisteredBy, RegisteredOn
        )
        VALUES
        (
            @BranchId, @MachineName, @StationType,
            1, @EmployeeId, GETDATE()
        );
    END;

    COMMIT TRANSACTION;

    SELECT KOTWorkstationId, BranchId, MachineName, StationType, IsActive
    FROM dbo.tblKOTWorkstation
    WHERE BranchId = @BranchId
      AND MachineName = @MachineName;
END;
GO

CREATE OR ALTER PROCEDURE dbo.prdPartyBooking_ProcessScheduledKOT
    @PartyBookingId INT,
    @BranchId INT,
    @EmployeeId INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @ConsumedRawMaterialStatus BIT;
        DECLARE @KOTDatetime DATETIME;
        DECLARE @BookingStatus INT;

        SELECT
            @ConsumedRawMaterialStatus = ISNULL(ConsumedRawMaterialStatus, 0),
            @KOTDatetime = KOTDatetime,
            @BookingStatus = Status
        FROM dbo.tblPartyBooking WITH (UPDLOCK, HOLDLOCK)
        WHERE PartyBookingId = @PartyBookingId
          AND BranchId = @BranchId;

        IF @BookingStatus IS NULL OR @BookingStatus <> 1
            THROW 51000, 'Party booking was not found or is inactive.', 1;

        IF @KOTDatetime IS NULL
            THROW 51000, 'KOT date and time is not assigned for this party booking.', 1;

        IF @KOTDatetime > GETDATE()
            THROW 51000, 'Party booking KOT time has not arrived yet.', 1;

        IF ISNULL(@EmployeeId, 0) <= 0
            THROW 51005, 'A valid employee is required for scheduled raw-material deduction.', 1;

        /*
          Validate route configuration independently of stock preparation.
          All ProductType=9 menu products must map to a Kitchen or Counter mode.
        */
        IF EXISTS
        (
            SELECT 1
            FROM dbo.tblPartyBookingDetails bd
            LEFT JOIN dbo.tblProduct p
                ON p.ProductId = bd.ProductId
               AND p.Status = 1
            WHERE bd.PartyBookingId = @PartyBookingId
              AND bd.BranchId = @BranchId
              AND bd.Status = 1
              AND bd.ProductType = 9
              AND ISNULL(bd.Qty, 0) > 0
              AND
              (
                  p.ProductId IS NULL
                  OR p.PreparationMode NOT IN (1, 2)
                  OR p.PreparationMode IS NULL
              )
        )
            THROW 51003, 'A party menu product has no valid PreparationMode. Set tblProduct.PreparationMode to 1 (Kitchen) or 2 (Counter).', 1;

        IF EXISTS
        (
            SELECT 1
            FROM dbo.tblPartyBookingDetails bd
            WHERE bd.PartyBookingId = @PartyBookingId
              AND bd.BranchId = @BranchId
              AND bd.Status = 1
              AND bd.ProductType = 9
              AND ISNULL(bd.Qty, 0) < 0
        )
            THROW 51004, 'A party menu product quantity cannot be negative.', 1;

        /*
          RAW MATERIAL DEDUCTION (once per booking)

          The demand includes menu products routed to BOTH Kitchen and Counter.
          PreparationMode is deliberately not used to filter stock demand.
          The FoodPreparationType/Deductionstage rules remain in effect:
            1 = Made-to-order: deduct active raw-material recipe rows.
            2 = Batch-prepared: handled by batch-preparation workflow.
            3 = Partially-prepared: deduct stages 0 and 1 here.
            4 = Not applicable: no recipe deduction here.

          Stock is validated and deducted FIFO from tblProductRate.QOH, using
          Baseunit and Conversionfactor exactly like prdUpdate_InventoryRequestApproval.
          This transaction sets ConsumedRawMaterialStatus only after deduction.
        */
        IF @ConsumedRawMaterialStatus = 0
        BEGIN
            DECLARE @BookingProducts TABLE
            (
                ProductId INT NOT NULL PRIMARY KEY,
                PlannedQty DECIMAL(18,4) NOT NULL
            );

            INSERT INTO @BookingProducts(ProductId, PlannedQty)
            SELECT
                bd.ProductId,
                CONVERT(DECIMAL(18,4), SUM(bd.Qty))
            FROM dbo.tblPartyBookingDetails bd WITH (UPDLOCK, HOLDLOCK)
            WHERE bd.PartyBookingId = @PartyBookingId
              AND bd.BranchId = @BranchId
              AND bd.Status = 1
              AND bd.ProductType = 9
              AND bd.ProductId > 0
              AND ISNULL(bd.Qty, 0) > 0
            GROUP BY bd.ProductId;

            /* Each eligible product must have an active raw-material recipe. */
            IF EXISTS
            (
                SELECT 1
                FROM @BookingProducts bp
                INNER JOIN dbo.tblProduct p
                    ON p.ProductId = bp.ProductId
                   AND p.Status = 1
                WHERE p.FoodPreparationType = 1
                  AND NOT EXISTS
                  (
                      SELECT 1
                      FROM dbo.tblIngredientDetails ing WITH (UPDLOCK, HOLDLOCK)
                      WHERE ing.ProductId = bp.ProductId
                        AND ing.ProductType = 1
                        AND ISNULL(ing.Status, 1) = 1
                  )
            )
                THROW 51014, 'One or more made-to-order party products do not have active raw-material ingredients.', 1;

            IF EXISTS
            (
                SELECT 1
                FROM @BookingProducts bp
                INNER JOIN dbo.tblProduct p
                    ON p.ProductId = bp.ProductId
                   AND p.Status = 1
                INNER JOIN dbo.tblIngredientDetails ing WITH (UPDLOCK, HOLDLOCK)
                    ON ing.ProductId = bp.ProductId
                   AND ing.ProductType = 1
                   AND ISNULL(ing.Status, 1) = 1
                WHERE
                    (
                        p.FoodPreparationType = 1
                        OR
                        (
                            p.FoodPreparationType = 3
                            AND ISNULL(ing.Deductionstage, 0) IN (0, 1)
                        )
                    )
                  AND ISNULL(ing.Quantity, 0) <= 0
            )
                THROW 51015, 'A party booking recipe contains a zero, negative, or missing raw-material quantity.', 1;

            DECLARE @RawMaterialRequirements TABLE
            (
                ItemId BIGINT NOT NULL,
                RequiredBaseQty DECIMAL(18,4) NOT NULL
            );

            INSERT INTO @RawMaterialRequirements(ItemId, RequiredBaseQty)
            SELECT
                ing.ItemId,
                CONVERT(DECIMAL(18,4), SUM(bp.PlannedQty * ing.Quantity))
            FROM @BookingProducts bp
            INNER JOIN dbo.tblProduct p
                ON p.ProductId = bp.ProductId
               AND p.Status = 1
            INNER JOIN dbo.tblIngredientDetails ing WITH (UPDLOCK, HOLDLOCK)
                ON ing.ProductId = bp.ProductId
               AND ing.ProductType = 1
               AND ISNULL(ing.Status, 1) = 1
            WHERE
                p.FoodPreparationType = 1
                OR
                (
                    p.FoodPreparationType = 3
                    AND ISNULL(ing.Deductionstage, 0) IN (0, 1)
                )
            GROUP BY ing.ItemId
            HAVING SUM(bp.PlannedQty * ing.Quantity) > 0;

            IF EXISTS
            (
                SELECT 1
                FROM @RawMaterialRequirements req
                LEFT JOIN dbo.tblItem i
                    ON i.ItemId = req.ItemId
                WHERE i.ItemId IS NULL
                   OR NULLIF(LTRIM(RTRIM(i.Baseunit)), '') IS NULL
            )
                THROW 51016, 'A party recipe raw material has an invalid ItemId or missing Baseunit.', 1;

            IF EXISTS
            (
                SELECT 1
                FROM @RawMaterialRequirements req
                INNER JOIN dbo.tblItem i
                    ON i.ItemId = req.ItemId
                WHERE i.Baseunit <> i.unit
                  AND ISNULL(i.Conversionfactor, 0) <= 0
            )
                THROW 51017, 'A party recipe raw material has an invalid Conversionfactor.', 1;

            DECLARE @StockToDeduct TABLE
            (
                ItemId BIGINT NOT NULL PRIMARY KEY,
                RequiredQty DECIMAL(18,4) NOT NULL,
                ConversionFactor DECIMAL(18,4) NOT NULL,
                ActiveRateCount INT NULL,
                AvailableBaseQty DECIMAL(18,4) NULL
            );

            INSERT INTO @StockToDeduct(ItemId, RequiredQty, ConversionFactor)
            SELECT
                req.ItemId,
                CONVERT(DECIMAL(18,4), SUM(req.RequiredBaseQty)),
                CONVERT
                (
                    DECIMAL(18,4),
                    MAX
                    (
                        CASE
                            WHEN ISNULL(i.Baseunit, i.unit) = i.unit THEN 1
                            ELSE i.Conversionfactor
                        END
                    )
                )
            FROM @RawMaterialRequirements req
            INNER JOIN dbo.tblItem i
                ON i.ItemId = req.ItemId
            GROUP BY req.ItemId
            HAVING SUM(req.RequiredBaseQty) > 0;

            UPDATE stock
            SET
                ActiveRateCount = rate.RateCount,
                AvailableBaseQty = CONVERT
                (
                    DECIMAL(18,4),
                    ISNULL(rate.AvailablePurchaseQty, 0) * stock.ConversionFactor
                )
            FROM @StockToDeduct stock
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
                FROM dbo.tblProductRate pr WITH (UPDLOCK, HOLDLOCK)
                WHERE pr.ProductId = stock.ItemId
                  AND pr.Status = 1
            ) rate;

            IF EXISTS
            (
                SELECT 1
                FROM @StockToDeduct
                WHERE ISNULL(ActiveRateCount, 0) = 0
            )
                THROW 51009, 'No active tblProductRate stock rows were found for one or more party booking raw materials.', 1;

            IF EXISTS
            (
                SELECT 1
                FROM @StockToDeduct
                WHERE ISNULL(AvailableBaseQty, 0) < RequiredQty
            )
                THROW 51010, 'Insufficient raw-material stock for this party booking. KOT was not queued and stock was not deducted.', 1;

            /* FIFO: deduct oldest tblProductRate.Id first; QOH remains in purchase units. */
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
                FROM dbo.tblProductRate pr WITH (UPDLOCK, HOLDLOCK)
                INNER JOIN @StockToDeduct stock
                    ON stock.ItemId = pr.ProductId
                WHERE pr.Status = 1
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
            UPDATE pr
            SET pr.QOH = CONVERT
            (
                DECIMAL(18,4),
                (
                    (
                        CONVERT(DECIMAL(18,8), ISNULL(pr.QOH, 0))
                        * deduction.ConversionFactor
                    ) - deduction.DeductQtyBase
                ) / NULLIF(deduction.ConversionFactor, 0)
            )
            FROM dbo.tblProductRate pr
            INNER JOIN RateDeductions deduction
                ON deduction.Id = pr.Id
            WHERE deduction.DeductQtyBase > 0;

            UPDATE dbo.tblPartyBooking
            SET ConsumedRawMaterialStatus = 1
            WHERE PartyBookingId = @PartyBookingId
              AND BranchId = @BranchId
              AND ISNULL(ConsumedRawMaterialStatus, 0) = 0;
        END;

        /*
          Queue all active menu lines. Product PreparationMode routes tickets:
            1 -> Kitchen
            2 -> Counter
          This insert is independent of stock deduction and is idempotent.
        */
        INSERT INTO dbo.tblPartyBookingKOTQueue
        (
            PartyBookingId,
            PartyBookingDetailsId,
            ProductId,
            ProductName,
            Quantity,
            PreparationMode,
            TableType,
            CategoryName,
            CustomerName,
            Phone,
            EventDescription,
            BookingDateTime,
            KOTDatetime,
            NoOfAdults,
            NoOfChildren,
            BranchId,
            CreatedBy
        )
        SELECT
            b.PartyBookingId,
            bd.PartyBookingDetailsId,
            bd.ProductId,
            COALESCE(NULLIF(bd.Product, N''), p.Product),
            bd.Qty,
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
        INNER JOIN dbo.tblProduct p
            ON p.ProductId = bd.ProductId
           AND p.Status = 1
           AND p.PreparationMode IN (1, 2)
        WHERE b.PartyBookingId = @PartyBookingId
          AND b.BranchId = @BranchId
          AND b.Status = 1
          AND bd.Qty > 0
          AND NOT EXISTS
          (
              SELECT 1
              FROM dbo.tblPartyBookingKOTQueue q WITH (UPDLOCK, HOLDLOCK)
              WHERE q.PartyBookingId = bd.PartyBookingId
                AND q.PartyBookingDetailsId = bd.PartyBookingDetailsId
                AND q.PreparationMode = p.PreparationMode
          );

        /* Each station flag is true only when its own route has no pending row. */
        UPDATE b
        SET
            KoTPrintStatus_Kitchen =
                CASE
                    WHEN EXISTS
                    (
                        SELECT 1 FROM dbo.tblPartyBookingKOTQueue q
                        WHERE q.PartyBookingId = b.PartyBookingId
                          AND q.BranchId = b.BranchId
                          AND q.PreparationMode = 1
                          AND q.Status <> 2
                    ) THEN 0 ELSE 1
                END,
            KoTPrintStatus_counter =
                CASE
                    WHEN EXISTS
                    (
                        SELECT 1 FROM dbo.tblPartyBookingKOTQueue q
                        WHERE q.PartyBookingId = b.PartyBookingId
                          AND q.BranchId = b.BranchId
                          AND q.PreparationMode = 2
                          AND q.Status <> 2
                    ) THEN 0 ELSE 1
                END
        FROM dbo.tblPartyBooking b
        WHERE b.PartyBookingId = @PartyBookingId
          AND b.BranchId = @BranchId;

        UPDATE dbo.tblPartyBooking
        SET KoTPrintStatus =
            CASE
                WHEN ISNULL(KoTPrintStatus_Kitchen, 0) = 1
                 AND ISNULL(KoTPrintStatus_counter, 0) = 1
                    THEN 1
                ELSE 0
            END
        WHERE PartyBookingId = @PartyBookingId
          AND BranchId = @BranchId;

        COMMIT TRANSACTION;

        SELECT
            PartyBookingId,
            ConsumedRawMaterialStatus,
            KoTPrintStatus_Kitchen,
            KoTPrintStatus_counter,
            KoTPrintStatus,
            KOTDatetime
        FROM dbo.tblPartyBooking
        WHERE PartyBookingId = @PartyBookingId
          AND BranchId = @BranchId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.prdPartyBookingKOT_ClaimPending
    @BranchId INT,
    @PreparationMode INT,
    @MachineName NVARCHAR(128)
AS
BEGIN
    SET NOCOUNT ON;

    IF @PreparationMode NOT IN (1, 2)
        RETURN;

    /* A machine can only claim the route matching its registered role. */
    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.tblKOTWorkstation
        WHERE BranchId = @BranchId
          AND MachineName = @MachineName
          AND IsActive = 1
          AND StationType = @PreparationMode
    )
        RETURN;

    ;WITH PendingRows AS
    (
        SELECT TOP (300) *
        FROM dbo.tblPartyBookingKOTQueue WITH (UPDLOCK, READPAST, ROWLOCK)
        WHERE BranchId = @BranchId
          AND PreparationMode = @PreparationMode
          AND
          (
              Status = 0
              OR (Status = 1 AND ClaimedOn < DATEADD(MINUTE, -5, GETDATE()))
          )
        ORDER BY KOTDatetime, PartyBookingKOTQueueId
    )
    UPDATE PendingRows
    SET Status = 1,
        ClaimedOn = GETDATE(),
        PrintMachine = @MachineName,
        LastError = NULL
    OUTPUT inserted.*;
END;
GO

CREATE OR ALTER PROCEDURE dbo.prdPartyBookingKOT_MarkPrinted
    @PartyBookingId INT,
    @BranchId INT,
    @PreparationMode INT,
    @EmployeeId INT,
    @MachineName NVARCHAR(128)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE dbo.tblPartyBookingKOTQueue
        SET Status = 2,
            PrintedOn = GETDATE(),
            PrintedBy = @EmployeeId,
            PrintMachine = @MachineName,
            LastError = NULL
        WHERE PartyBookingId = @PartyBookingId
          AND BranchId = @BranchId
          AND PreparationMode = @PreparationMode
          AND Status = 1
          AND PrintMachine = @MachineName;

        IF @@ROWCOUNT = 0
            THROW 51110, 'No claimed party KOT rows were found for this machine and station.', 1;

        IF @PreparationMode = 1
        BEGIN
            UPDATE dbo.tblPartyBooking
            SET KoTPrintStatus_Kitchen =
                CASE
                    WHEN EXISTS
                    (
                        SELECT 1 FROM dbo.tblPartyBookingKOTQueue q
                        WHERE q.PartyBookingId = @PartyBookingId
                          AND q.BranchId = @BranchId
                          AND q.PreparationMode = 1
                          AND q.Status <> 2
                    ) THEN 0 ELSE 1
                END
            WHERE PartyBookingId = @PartyBookingId
              AND BranchId = @BranchId;
        END
        ELSE IF @PreparationMode = 2
        BEGIN
            UPDATE dbo.tblPartyBooking
            SET KoTPrintStatus_counter =
                CASE
                    WHEN EXISTS
                    (
                        SELECT 1 FROM dbo.tblPartyBookingKOTQueue q
                        WHERE q.PartyBookingId = @PartyBookingId
                          AND q.BranchId = @BranchId
                          AND q.PreparationMode = 2
                          AND q.Status <> 2
                    ) THEN 0 ELSE 1
                END
            WHERE PartyBookingId = @PartyBookingId
              AND BranchId = @BranchId;
        END
        ELSE
            THROW 51111, 'Invalid KOT station route.', 1;

        UPDATE dbo.tblPartyBooking
        SET KoTPrintStatus =
            CASE
                WHEN ISNULL(KoTPrintStatus_Kitchen, 0) = 1
                 AND ISNULL(KoTPrintStatus_counter, 0) = 1
                    THEN 1
                ELSE 0
            END
        WHERE PartyBookingId = @PartyBookingId
          AND BranchId = @BranchId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.prdPartyBookingKOT_MarkFailed
    @PartyBookingId INT,
    @BranchId INT,
    @PreparationMode INT,
    @ErrorMessage NVARCHAR(2000)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.tblPartyBookingKOTQueue
    SET Status = 0,
        ClaimedOn = NULL,
        LastError = @ErrorMessage
    WHERE PartyBookingId = @PartyBookingId
      AND BranchId = @BranchId
      AND PreparationMode = @PreparationMode
      AND Status = 1;

    IF @PreparationMode = 1
    BEGIN
        UPDATE dbo.tblPartyBooking
        SET KoTPrintStatus_Kitchen = 0,
            KoTPrintStatus = 0
        WHERE PartyBookingId = @PartyBookingId
          AND BranchId = @BranchId;
    END
    ELSE IF @PreparationMode = 2
    BEGIN
        UPDATE dbo.tblPartyBooking
        SET KoTPrintStatus_counter = 0,
            KoTPrintStatus = 0
        WHERE PartyBookingId = @PartyBookingId
          AND BranchId = @BranchId;
    END;
END;
GO
