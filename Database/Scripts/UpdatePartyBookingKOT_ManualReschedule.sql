USE [FoodBeverage];
GO

/*
    Party Booking KOT event display and manual reschedule/reprint fix.

    - Existing printed queue rows are reopened only for routes whose booking
      print flag was explicitly reset to 0.
    - Queue data is synchronized with the current booking schedule and details.
    - Queue claiming is gated by the current booking KOTDatetime, preventing
      an old pending queue row from printing before a newly selected future time.
    - A ticket printed during a concurrent reschedule cannot mark the new
      schedule as printed.
    - No stock quantity or tblPartyBookingKOTStockLog status is modified here.
*/

CREATE OR ALTER PROCEDURE dbo.prdPartyBookingKOT_RequeueResetRoutes
    @PartyBookingId INT,
    @BranchId INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF ISNULL(@PartyBookingId, 0) <= 0 OR ISNULL(@BranchId, 0) <= 0
        RETURN;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE q
        SET
            q.ProductName = LEFT(
                CONVERT(NVARCHAR(250),
                    COALESCE(
                        NULLIF(LTRIM(RTRIM(bd.Product)), N''),
                        NULLIF(LTRIM(RTRIM(p.Product)), N''),
                        N'')),
                250),
            q.Quantity = CONVERT(DECIMAL(18,3), bd.Qty),
            q.TableType = ISNULL(bd.TableType, 0),
            q.CategoryName = bd.CategoryName,
            q.CustomerName = b.CustomerName,
            q.Phone = b.Phone,
            q.EventDescription = b.EventDescription,
            q.BookingDateTime = b.BookingDateTime,
            q.KOTDatetime = b.KOTDatetime,
            q.NoOfAdults = ISNULL(b.NoOfAdults, 0),
            q.NoOfChildren = ISNULL(b.NoOfChildren, 0),
            q.Status = 0,
            q.ClaimedOn = NULL,
            q.PrintedOn = NULL,
            q.PrintedBy = NULL,
            q.PrintMachine = NULL,
            q.LastError = NULL
        FROM dbo.tblPartyBookingKOTQueue q WITH (UPDLOCK, ROWLOCK)
        INNER JOIN dbo.tblPartyBooking b
            ON b.PartyBookingId = q.PartyBookingId
           AND b.BranchId = q.BranchId
        INNER JOIN dbo.tblPartyBookingDetails bd
            ON bd.PartyBookingId = q.PartyBookingId
           AND bd.PartyBookingDetailsId = q.PartyBookingDetailsId
           AND bd.BranchId = q.BranchId
           AND bd.Status = 1
           AND bd.ProductType = 9
           AND ISNULL(bd.Qty, 0) > 0
        INNER JOIN dbo.tblProduct p
            ON p.ProductId = bd.ProductId
           AND p.Status = 1
           AND p.PreparationMode = q.PreparationMode
        WHERE q.PartyBookingId = @PartyBookingId
          AND q.BranchId = @BranchId
          AND q.Status IN (0, 2)
          AND b.Status = 1
          AND b.KOTDatetime IS NOT NULL
          AND
          (
              (q.PreparationMode = 1 AND ISNULL(b.KoTPrintStatus_Kitchen, 0) = 0)
              OR
              (q.PreparationMode = 2 AND ISNULL(b.KoTPrintStatus_counter, 0) = 0)
          );

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0
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
        SELECT TOP (300) q.*
        FROM dbo.tblPartyBookingKOTQueue q WITH (UPDLOCK, READPAST, ROWLOCK)
        INNER JOIN dbo.tblPartyBooking b
            ON b.PartyBookingId = q.PartyBookingId
           AND b.BranchId = q.BranchId
        WHERE q.BranchId = @BranchId
          AND q.PreparationMode = @PreparationMode
          AND b.Status = 1
          AND b.KOTDatetime IS NOT NULL
          AND b.KOTDatetime <= GETDATE()
          AND q.KOTDatetime = b.KOTDatetime
          AND q.KOTDatetime <= GETDATE()
          AND
          (
              (@PreparationMode = 1 AND ISNULL(b.KoTPrintStatus_Kitchen, 0) = 0)
              OR
              (@PreparationMode = 2 AND ISNULL(b.KoTPrintStatus_counter, 0) = 0)
          )
          AND
          (
              q.Status = 0
              OR (q.Status = 1 AND q.ClaimedOn < DATEADD(MINUTE, -5, GETDATE()))
          )
        ORDER BY q.KOTDatetime, q.PartyBookingKOTQueueId
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

    IF @PreparationMode NOT IN (1, 2)
        THROW 51111, 'Invalid KOT station route.', 1;

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
            UPDATE b
            SET KoTPrintStatus_Kitchen =
                CASE
                    WHEN b.KOTDatetime IS NULL OR b.KOTDatetime > GETDATE() THEN 0
                    WHEN EXISTS
                    (
                        SELECT 1
                        FROM dbo.tblPartyBookingDetails bd
                        LEFT JOIN dbo.tblProduct p
                            ON p.ProductId = bd.ProductId
                           AND p.Status = 1
                        WHERE bd.PartyBookingId = b.PartyBookingId
                          AND bd.BranchId = b.BranchId
                          AND bd.Status = 1
                          AND bd.ProductType = 9
                          AND ISNULL(bd.Qty, 0) > 0
                          AND
                          (
                              p.ProductId IS NULL
                              OR p.PreparationMode NOT IN (1, 2)
                              OR p.PreparationMode IS NULL
                          )
                    ) THEN 0
                    WHEN EXISTS
                    (
                        SELECT 1
                        FROM dbo.tblPartyBookingDetails bd
                        INNER JOIN dbo.tblProduct p
                            ON p.ProductId = bd.ProductId
                           AND p.Status = 1
                           AND p.PreparationMode = 1
                        WHERE bd.PartyBookingId = b.PartyBookingId
                          AND bd.BranchId = b.BranchId
                          AND bd.Status = 1
                          AND bd.ProductType = 9
                          AND ISNULL(bd.Qty, 0) > 0
                          AND NOT EXISTS
                          (
                              SELECT 1
                              FROM dbo.tblPartyBookingKOTQueue q
                              WHERE q.PartyBookingId = bd.PartyBookingId
                                AND q.PartyBookingDetailsId = bd.PartyBookingDetailsId
                                AND q.BranchId = bd.BranchId
                                AND q.PreparationMode = 1
                                AND q.KOTDatetime = b.KOTDatetime
                                AND q.Status = 2
                          )
                    ) THEN 0
                    ELSE 1
                END
            FROM dbo.tblPartyBooking b
            WHERE b.PartyBookingId = @PartyBookingId
              AND b.BranchId = @BranchId;
        END
        ELSE
        BEGIN
            UPDATE b
            SET KoTPrintStatus_counter =
                CASE
                    WHEN b.KOTDatetime IS NULL OR b.KOTDatetime > GETDATE() THEN 0
                    WHEN EXISTS
                    (
                        SELECT 1
                        FROM dbo.tblPartyBookingDetails bd
                        LEFT JOIN dbo.tblProduct p
                            ON p.ProductId = bd.ProductId
                           AND p.Status = 1
                        WHERE bd.PartyBookingId = b.PartyBookingId
                          AND bd.BranchId = b.BranchId
                          AND bd.Status = 1
                          AND bd.ProductType = 9
                          AND ISNULL(bd.Qty, 0) > 0
                          AND
                          (
                              p.ProductId IS NULL
                              OR p.PreparationMode NOT IN (1, 2)
                              OR p.PreparationMode IS NULL
                          )
                    ) THEN 0
                    WHEN EXISTS
                    (
                        SELECT 1
                        FROM dbo.tblPartyBookingDetails bd
                        INNER JOIN dbo.tblProduct p
                            ON p.ProductId = bd.ProductId
                           AND p.Status = 1
                           AND p.PreparationMode = 2
                        WHERE bd.PartyBookingId = b.PartyBookingId
                          AND bd.BranchId = b.BranchId
                          AND bd.Status = 1
                          AND bd.ProductType = 9
                          AND ISNULL(bd.Qty, 0) > 0
                          AND NOT EXISTS
                          (
                              SELECT 1
                              FROM dbo.tblPartyBookingKOTQueue q
                              WHERE q.PartyBookingId = bd.PartyBookingId
                                AND q.PartyBookingDetailsId = bd.PartyBookingDetailsId
                                AND q.BranchId = bd.BranchId
                                AND q.PreparationMode = 2
                                AND q.KOTDatetime = b.KOTDatetime
                                AND q.Status = 2
                          )
                    ) THEN 0
                    ELSE 1
                END
            FROM dbo.tblPartyBooking b
            WHERE b.PartyBookingId = @PartyBookingId
              AND b.BranchId = @BranchId;
        END;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
