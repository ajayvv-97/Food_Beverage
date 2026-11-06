/*
    Party booking scheduled KOT and raw-material deduction.

    Execute this script once against the Food_Beverage database before enabling
    the POS scheduler. Status values in tblPartyBookingKOTQueue:
      0 = Pending, 1 = Claimed for printing, 2 = Printed.
*/
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
        TableType INT NOT NULL CONSTRAINT DF_PartyBookingKOTQueue_TableType DEFAULT (0),
        CategoryName NVARCHAR(150) NULL,
        CustomerName NVARCHAR(200) NULL,
        Phone NVARCHAR(40) NULL,
        EventDescription NVARCHAR(1000) NULL,
        BookingDateTime DATETIME NULL,
        KOTDatetime DATETIME NOT NULL,
        NoOfAdults INT NOT NULL CONSTRAINT DF_PartyBookingKOTQueue_Adults DEFAULT (0),
        NoOfChildren INT NOT NULL CONSTRAINT DF_PartyBookingKOTQueue_Children DEFAULT (0),
        BranchId INT NOT NULL,
        CreatedBy INT NOT NULL,
        CreatedOn DATETIME NOT NULL CONSTRAINT DF_PartyBookingKOTQueue_CreatedOn DEFAULT (GETDATE()),
        Status TINYINT NOT NULL CONSTRAINT DF_PartyBookingKOTQueue_Status DEFAULT (0),
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

        /*
          Deduct only recipe rows which belong to Raw Material (ProductType=1).
          MadetoOrder (1): deduct at the scheduled KOT time.
          BatchPrepared (2): do not deduct here; batch-preparation stock handling
                             owns that deduction.
          PartiallyPrepared (3): deduct Any (0) and Order Preparation (1) rows.
                                 Advance Preparation (2) is deducted by the
                                 advance-preparation workflow, not at KOT time.
          NotApplicable (4): no recipe deduction at this stage.
          Recipe quantities and tblItem.quantity are both in Baseunit.
        */
        IF @ConsumedRawMaterialStatus = 0
        BEGIN
            DECLARE @RawMaterialDemand TABLE
            (
                ItemId BIGINT NOT NULL PRIMARY KEY,
                RequiredQty DECIMAL(18,6) NOT NULL
            );

            INSERT INTO @RawMaterialDemand (ItemId, RequiredQty)
            SELECT
                id.ItemId,
                SUM(CAST(ISNULL(bd.Qty, 0) * ISNULL(id.quantity, 0) AS DECIMAL(18,6)))
            FROM dbo.tblPartyBookingDetails bd
            INNER JOIN dbo.tblProduct p
                ON p.ProductId = bd.ProductId
               AND p.Status = 1
            INNER JOIN dbo.tblIngredientDetails id
                ON id.ProductId = p.ProductId
               AND id.Status = 1
               AND id.ProductType = 1
            WHERE bd.PartyBookingId = @PartyBookingId
              AND bd.BranchId = @BranchId
              AND bd.Status = 1
              AND bd.ProductType = 9
              AND bd.ProductId > 0
              AND
              (
                    p.FoodPreparationType = 1
                    OR
                    (
                        p.FoodPreparationType = 3
                        AND ISNULL(id.Deductionstage, 0) IN (0, 1)
                    )
              )
            GROUP BY id.ItemId
            HAVING SUM(CAST(ISNULL(bd.Qty, 0) * ISNULL(id.quantity, 0) AS DECIMAL(18,6))) > 0;

            DECLARE @ShortageMessage NVARCHAR(2048);

            SELECT TOP (1)
                @ShortageMessage =
                    N'Insufficient raw-material stock for ItemId ' +
                    CONVERT(NVARCHAR(30), d.ItemId) +
                    N'. Required: ' + CONVERT(NVARCHAR(40), d.RequiredQty) +
                    N'; Available: ' +
                    CONVERT(NVARCHAR(40), ISNULL(i.quantity, 0)) + N'.'
            FROM @RawMaterialDemand d
            LEFT JOIN dbo.tblItem i WITH (UPDLOCK, HOLDLOCK)
                ON i.ItemId = d.ItemId
               AND i.BranchId = @BranchId
               AND i.Status = 1
            WHERE i.ItemId IS NULL
               OR ISNULL(i.quantity, 0) < d.RequiredQty;

            IF @ShortageMessage IS NOT NULL
                THROW 51001, @ShortageMessage, 1;

            UPDATE i
                SET i.quantity = ISNULL(i.quantity, 0) - d.RequiredQty,
                    i.ModifiedBy = @EmployeeId,
                    i.ModifiedDateTime = GETDATE()
            FROM dbo.tblItem i
            INNER JOIN @RawMaterialDemand d
                ON d.ItemId = i.ItemId
            WHERE i.BranchId = @BranchId
              AND i.Status = 1;

            IF @@ROWCOUNT <> (SELECT COUNT(*) FROM @RawMaterialDemand)
                THROW 51002, 'One or more raw-material stock rows could not be updated. No stock deduction was committed.', 1;

            UPDATE dbo.tblPartyBooking
                SET ConsumedRawMaterialStatus = 1
            WHERE PartyBookingId = @PartyBookingId
              AND BranchId = @BranchId
              AND ISNULL(ConsumedRawMaterialStatus, 0) = 0;
        END;

        /*
          Queue each selected party-menu line exactly once. The product's
          PreparationMode chooses Kitchen (1) or Counter (2).
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
            CASE WHEN p.PreparationMode IN (1, 2) THEN p.PreparationMode ELSE 1 END,
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
                AND q.PreparationMode =
                    CASE WHEN p.PreparationMode IN (1, 2) THEN p.PreparationMode ELSE 1 END
          );

        UPDATE b
            SET KoTPrintStatus =
                CASE
                    WHEN EXISTS
                    (
                        SELECT 1
                        FROM dbo.tblPartyBookingKOTQueue q
                        WHERE q.PartyBookingId = b.PartyBookingId
                          AND q.BranchId = b.BranchId
                    )
                    AND NOT EXISTS
                    (
                        SELECT 1
                        FROM dbo.tblPartyBookingKOTQueue q
                        WHERE q.PartyBookingId = b.PartyBookingId
                          AND q.BranchId = b.BranchId
                          AND q.Status <> 2
                    )
                    THEN 1
                    WHEN NOT EXISTS
                    (
                        SELECT 1
                        FROM dbo.tblPartyBookingKOTQueue q
                        WHERE q.PartyBookingId = b.PartyBookingId
                          AND q.BranchId = b.BranchId
                    )
                    THEN 1
                    ELSE 0
                END
        FROM dbo.tblPartyBooking b
        WHERE b.PartyBookingId = @PartyBookingId
          AND b.BranchId = @BranchId;

        COMMIT TRANSACTION;

        SELECT
            PartyBookingId,
            ConsumedRawMaterialStatus,
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
    @PartyBookingKOTQueueId BIGINT,
    @EmployeeId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PartyBookingId INT;
    DECLARE @BranchId INT;

    UPDATE dbo.tblPartyBookingKOTQueue
        SET Status = 2,
            PrintedOn = GETDATE(),
            PrintedBy = @EmployeeId,
            LastError = NULL
    WHERE PartyBookingKOTQueueId = @PartyBookingKOTQueueId
      AND Status = 1;

    SELECT
        @PartyBookingId = PartyBookingId,
        @BranchId = BranchId
    FROM dbo.tblPartyBookingKOTQueue
    WHERE PartyBookingKOTQueueId = @PartyBookingKOTQueueId;

    IF @PartyBookingId IS NOT NULL
    BEGIN
        UPDATE dbo.tblPartyBooking
            SET KoTPrintStatus =
                CASE WHEN EXISTS
                (
                    SELECT 1
                    FROM dbo.tblPartyBookingKOTQueue
                    WHERE PartyBookingId = @PartyBookingId
                      AND BranchId = @BranchId
                      AND Status <> 2
                )
                THEN 0 ELSE 1 END
        WHERE PartyBookingId = @PartyBookingId
          AND BranchId = @BranchId;
    END;
END;
GO

CREATE OR ALTER PROCEDURE dbo.prdPartyBookingKOT_MarkFailed
    @PartyBookingKOTQueueId BIGINT,
    @ErrorMessage NVARCHAR(2000)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.tblPartyBookingKOTQueue
        SET Status = 0,
            ClaimedOn = NULL,
            LastError = @ErrorMessage
    WHERE PartyBookingKOTQueueId = @PartyBookingKOTQueueId
      AND Status = 1;
END;
GO