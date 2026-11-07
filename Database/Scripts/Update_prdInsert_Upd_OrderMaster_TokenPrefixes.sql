USE [FoodBeverage];
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/*
    Token prefix + grouping correction.

    Actual token type is always preserved:
      1 = Pickup   -> P
      2 = Takeaway -> T
      3 = Delivery -> D

    DailyTokenMode from tblGeneralSetting:
      1 = all three order types share one daily number sequence
      2 = all three order types use separate daily number sequences
      3 = Pickup + Takeaway share; Delivery uses a separate sequence

    IMPORTANT:
    The token's OrderTypeId remains the actual order type. Only the
    sequence-table key / application-lock key is grouped.
*/

IF NOT EXISTS (SELECT 1 FROM dbo.tblOrderType WHERE OrderTypeId = 1 AND Status = 1)
    THROW 51010, 'Active Pickup order type (OrderTypeId=1) is missing.', 1;

IF NOT EXISTS (SELECT 1 FROM dbo.tblOrderType WHERE OrderTypeId = 2 AND Status = 1)
    THROW 51011, 'Active Takeaway order type (OrderTypeId=2) is missing.', 1;

/*
    Delivery is optional in installations that do not use Delivery.
    If OrderTypeId=3 is configured and active, its prefix is set to D.
    The procedure below still supports Delivery once that master row exists.
*/
UPDATE dbo.tblOrderType
SET TokenPrefix = CASE OrderTypeId
    WHEN 1 THEN 'P'
    WHEN 2 THEN 'T'
    WHEN 3 THEN 'D'
END
WHERE OrderTypeId IN (1, 2, 3)
  AND Status = 1;
GO

ALTER PROCEDURE [dbo].[prdInsert_Upd_OrderMaster]
    @OrderMasterId INT,
    @EntryNo VARCHAR(20),
    @TotQty DECIMAL(10,2),
    @Remark NVARCHAR(50),
    @CreatedBy INT,
    @BranchId INT,
    @Status INT,
    @Tid INT,
    @BookmasterId INT,
    @PickupOrderFlag INT,
    @PkId INT,
    @KitchenNumber VARCHAR(100),
    @FirstName VARCHAR(100),
    @SecondName VARCHAR(100),
    @CompanyName VARCHAR(150),
    @GSTNo VARCHAR(50),
    @PhoneNumber VARCHAR(50),
    @EmailAddress VARCHAR(100),
    @FullAddress VARCHAR(MAX),
    @Gender INT,
    @cusid INT,
    @NetAmount DECIMAL(10,2),
    @AdultCount INT,
    @ChildCount INT,
    @OtherGuestCount INT,
    @TotalGuestCount INT,
    @OrderPlatformmaster INT,
    @OrderPlatformmasterName NVARCHAR(250),
    @OrderTypeId INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Id INT;
    DECLARE @EffectiveOrderTypeId INT = ISNULL(@OrderTypeId, 0);
    DECLARE @DailyTokenMode INT = 2;
    DECLARE @SequenceOrderTypeId INT = 0;
    DECLARE @BusinessDate DATE = CONVERT(DATE, SYSDATETIME());
    DECLARE @TokenNumber INT = NULL;
    DECLARE @TokenDisplay VARCHAR(20) = NULL;
    DECLARE @TokenPrefix VARCHAR(5) = NULL;
    DECLARE @LastTokenNumber INT = 0;
    DECLARE @MaxExistingTokenNumber INT = 0;
    DECLARE @EffectiveLastTokenNumber INT = 0;
    DECLARE @AppLockResult INT = 0;
    DECLARE @AppLockResource NVARCHAR(255);
    DECLARE @SequenceRowExists BIT = 0;

    BEGIN TRY
        BEGIN TRANSACTION;

        /* Compatibility for older Pickup callers that do not pass OrderTypeId. */
        IF @EffectiveOrderTypeId = 0 AND @PickupOrderFlag = 1
            SET @EffectiveOrderTypeId = 1;

        /* Existing order update / new order insert. */
        IF @OrderMasterId > 0
        BEGIN
            IF @PkId > 0
            BEGIN
                UPDATE dbo.tblOrderMaster
                SET EntryNo = @EntryNo,
                    EntryDate = SYSDATETIME(),
                    TotalQuantity = @TotQty,
                    Remarks = @Remark,
                    ModifiedBy = @CreatedBy,
                    ModifiedDateTime = SYSDATETIME(),
                    BranchId = @BranchId,
                    Status = @Status,
                    Tid = @Tid,
                    BookmasterId = @BookmasterId,
                    FirstName = @FirstName,
                    SecondName = @SecondName,
                    CompanyName = @CompanyName,
                    GSTNo = @GSTNo,
                    PhoneNumber = @PhoneNumber,
                    EmailAddress = @EmailAddress,
                    FullAddress = @FullAddress,
                    Gender = @Gender,
                    cusid = @cusid,
                    NetAmount = @NetAmount,
                    AdultCount = @AdultCount,
                    ChildCount = @ChildCount,
                    OtherGuestCount = @OtherGuestCount,
                    TotalGuestCount = @TotalGuestCount,
                    OrderPlatformmaster = @OrderPlatformmaster,
                    OrderPlatformmasterName = @OrderPlatformmasterName
                WHERE OrderMasterId = @OrderMasterId;

                UPDATE dbo.tblOrderDetails
                SET KitchenCancelStatus = 0,
                    CancelNotifyStatus = 0,
                    ModifiedDateTime = SYSDATETIME(),
                    ModifiedBy = @CreatedBy,
                    BranchId = @BranchId
                WHERE OrderMasterId = @OrderMasterId;
            END
            ELSE
            BEGIN
                UPDATE dbo.tblOrderMaster
                SET EntryNo = @EntryNo,
                    EntryDate = SYSDATETIME(),
                    TotalQuantity = TotalQuantity + @TotQty,
                    Remarks = @Remark,
                    ModifiedBy = @CreatedBy,
                    ModifiedDateTime = SYSDATETIME(),
                    BranchId = @BranchId,
                    Status = @Status,
                    Tid = @Tid,
                    BookmasterId = @BookmasterId,
                    FirstName = @FirstName,
                    SecondName = @SecondName,
                    CompanyName = @CompanyName,
                    GSTNo = @GSTNo,
                    PhoneNumber = @PhoneNumber,
                    EmailAddress = @EmailAddress,
                    FullAddress = @FullAddress,
                    Gender = @Gender,
                    cusid = @cusid,
                    NetAmount = @NetAmount,
                    AdultCount = @AdultCount,
                    ChildCount = @ChildCount,
                    OtherGuestCount = @OtherGuestCount,
                    TotalGuestCount = @TotalGuestCount,
                    OrderPlatformmaster = @OrderPlatformmaster,
                    OrderPlatformmasterName = @OrderPlatformmasterName
                WHERE OrderMasterId = @OrderMasterId;
            END;

            SET @Id = @OrderMasterId;

            IF @PkId = 0
            BEGIN
                UPDATE dbo.tblTable_info
                SET OrderMasterId = @Id
                WHERE Tid IN
                (
                    SELECT MTID
                    FROM dbo.tblBookDetails
                    WHERE rid = 0
                      AND BookmasterId = @BookmasterId
                      AND Status = 1
                );

                UPDATE dbo.tblHotelRegistration
                SET KitchenSrlNo = KitchenSrlNo + 1
                WHERE Id = @BranchId;
            END;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.tblOrderMaster
            (
                EntryNo, EntryDate, TotalQuantity, Remarks, CreatedBy,
                BranchId, Status, CreatedDateTime, Tid, BookmasterId,
                PickupOrderFlag, PkId, KitchenNumber,
                FirstName, SecondName, CompanyName, GSTNo, PhoneNumber,
                EmailAddress, FullAddress, Gender, cusid, NetAmount,
                AdultCount, ChildCount, OtherGuestCount, TotalGuestCount,
                OrderPlatformmaster, OrderPlatformmasterName
            )
            VALUES
            (
                @EntryNo, SYSDATETIME(), @TotQty, @Remark, @CreatedBy,
                @BranchId, @Status, SYSDATETIME(), @Tid, @BookmasterId,
                @PickupOrderFlag, @PkId, @KitchenNumber,
                @FirstName, @SecondName, @CompanyName, @GSTNo, @PhoneNumber,
                @EmailAddress, @FullAddress, @Gender, @cusid, @NetAmount,
                @AdultCount, @ChildCount, @OtherGuestCount, @TotalGuestCount,
                @OrderPlatformmaster, @OrderPlatformmasterName
            );

            SET @Id = CONVERT(INT, SCOPE_IDENTITY());

            UPDATE dbo.tblHotelRegistration
            SET KitchenSrlNo = KitchenSrlNo + 1
            WHERE Id = @BranchId;

            UPDATE dbo.tblPickup
            SET OrderMasterId = @Id
            WHERE PkId = @PkId;

            IF @BookmasterId = 0
            BEGIN
                UPDATE dbo.tblTable_info
                SET OrderStatus = 1,
                    OrderMasterId = @Id
                WHERE Tid = @Tid;
            END;
        END;

        /* Existing order keeps its stored actual token type and number. */
        SELECT @EffectiveOrderTypeId = OrderTypeId,
               @TokenNumber = TokenNumber,
               @TokenDisplay = TokenDisplay
        FROM dbo.tblOrderToken WITH (UPDLOCK, HOLDLOCK)
        WHERE OrderMasterId = @Id;

        IF @TokenNumber IS NULL AND @EffectiveOrderTypeId IN (1, 2, 3)
        BEGIN
            /* Read the latest active branch setting. */
            SET @DailyTokenMode = 2;

            SELECT TOP (1)
                @DailyTokenMode = ISNULL(DailyTokenMode, 2)
            FROM dbo.tblGeneralSetting
            WHERE BranchId = @BranchId
              AND Status = 1
            ORDER BY Gid DESC;

            IF @DailyTokenMode NOT IN (1, 2, 3)
                SET @DailyTokenMode = 2;

            /*
                Sequence group is intentionally separate from actual type:
                Mode 1: all types -> sequence group 1
                Mode 2: each actual type -> its own sequence group
                Mode 3: Pickup/Takeaway -> group 1; Delivery -> group 3
            */
            SET @SequenceOrderTypeId =
                CASE
                    WHEN @DailyTokenMode = 1 THEN 1
                    WHEN @DailyTokenMode = 3
                         AND @EffectiveOrderTypeId IN (1, 2) THEN 1
                    ELSE @EffectiveOrderTypeId
                END;

            /* Prefix always comes from actual order type, never sequence group. */
            SELECT @TokenPrefix = TokenPrefix
            FROM dbo.tblOrderType
            WHERE OrderTypeId = @EffectiveOrderTypeId
              AND Status = 1;

            IF NULLIF(@TokenPrefix, '') IS NULL
                THROW 51003, 'The actual order type is missing or inactive, or its TokenPrefix is blank in dbo.tblOrderType.', 1;

            /* Serialize all orders that share the same sequence group. */
            SET @AppLockResource = N'DailyOrderToken:'
                + CONVERT(NVARCHAR(8), @BusinessDate, 112)
                + N':' + CONVERT(NVARCHAR(10), @SequenceOrderTypeId);

            EXEC @AppLockResult = sys.sp_getapplock
                @Resource = @AppLockResource,
                @LockMode = N'Exclusive',
                @LockOwner = N'Transaction',
                @LockTimeout = 15000,
                @DbPrincipal = N'public';

            IF @AppLockResult < 0
                THROW 51004, 'Could not acquire the daily order token allocation lock. Please retry the order.', 1;

            /* Recheck current order after the lock to avoid allocating twice. */
            SELECT @EffectiveOrderTypeId = OrderTypeId,
                   @TokenNumber = TokenNumber,
                   @TokenDisplay = TokenDisplay
            FROM dbo.tblOrderToken WITH (UPDLOCK, HOLDLOCK)
            WHERE OrderMasterId = @Id;

            IF @TokenNumber IS NULL
            BEGIN
                /* Preserve the actual prefix/type if this is a new allocation. */
                SELECT @TokenPrefix = TokenPrefix
                FROM dbo.tblOrderType
                WHERE OrderTypeId = @EffectiveOrderTypeId
                  AND Status = 1;

                IF NULLIF(@TokenPrefix, '') IS NULL
                    THROW 51003, 'The actual order type is missing or inactive, or its TokenPrefix is blank in dbo.tblOrderType.', 1;

                /* Determine the highest already-issued number in this sequence group. */
                SET @MaxExistingTokenNumber = 0;

                IF @DailyTokenMode = 1
                BEGIN
                    SELECT @MaxExistingTokenNumber = ISNULL(MAX(TokenNumber), 0)
                    FROM dbo.tblOrderToken WITH (UPDLOCK, HOLDLOCK)
                    WHERE BusinessDate = @BusinessDate
                      AND OrderTypeId IN (1, 2, 3);
                END
                ELSE IF @DailyTokenMode = 3
                     AND @EffectiveOrderTypeId IN (1, 2)
                BEGIN
                    SELECT @MaxExistingTokenNumber = ISNULL(MAX(TokenNumber), 0)
                    FROM dbo.tblOrderToken WITH (UPDLOCK, HOLDLOCK)
                    WHERE BusinessDate = @BusinessDate
                      AND OrderTypeId IN (1, 2);
                END
                ELSE
                BEGIN
                    SELECT @MaxExistingTokenNumber = ISNULL(MAX(TokenNumber), 0)
                    FROM dbo.tblOrderToken WITH (UPDLOCK, HOLDLOCK)
                    WHERE BusinessDate = @BusinessDate
                      AND OrderTypeId = @EffectiveOrderTypeId;
                END;

                /* Read the shared sequence row, repairing if it is behind. */
                SET @LastTokenNumber = 0;
                SET @SequenceRowExists = 0;

                SELECT @SequenceRowExists = 1,
                       @LastTokenNumber = ISNULL(LastTokenNumber, 0)
                FROM dbo.tblOrderTokenSequence WITH (UPDLOCK, HOLDLOCK)
                WHERE BusinessDate = @BusinessDate
                  AND OrderTypeId = @SequenceOrderTypeId;

                SET @EffectiveLastTokenNumber =
                    CASE
                        WHEN ISNULL(@LastTokenNumber, 0) > @MaxExistingTokenNumber
                            THEN ISNULL(@LastTokenNumber, 0)
                        ELSE @MaxExistingTokenNumber
                    END;

                SET @TokenNumber = @EffectiveLastTokenNumber + 1;

                IF @SequenceRowExists = 0
                BEGIN
                    INSERT INTO dbo.tblOrderTokenSequence
                    (
                        BusinessDate, OrderTypeId, LastTokenNumber,
                        CreatedDateTime, ModifiedDateTime
                    )
                    VALUES
                    (
                        @BusinessDate, @SequenceOrderTypeId, @TokenNumber,
                        SYSDATETIME(), SYSDATETIME()
                    );
                END
                ELSE
                BEGIN
                    UPDATE dbo.tblOrderTokenSequence
                    SET LastTokenNumber = @TokenNumber,
                        ModifiedDateTime = SYSDATETIME()
                    WHERE BusinessDate = @BusinessDate
                      AND OrderTypeId = @SequenceOrderTypeId;
                END;

                /* Apply the prefix for the actual order type: P / T / D. */
                SET @TokenDisplay = @TokenPrefix +
                    CASE
                        WHEN @TokenNumber < 1000
                            THEN RIGHT('000' + CONVERT(VARCHAR(20), @TokenNumber), 3)
                        ELSE CONVERT(VARCHAR(20), @TokenNumber)
                    END;

                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.tblOrderToken WITH (UPDLOCK, HOLDLOCK)
                    WHERE OrderMasterId = @Id
                )
                BEGIN
                    UPDATE dbo.tblOrderToken
                    SET OrderId = @Id,
                        OrderTypeId = @EffectiveOrderTypeId,
                        BusinessDate = @BusinessDate,
                        TokenNumber = @TokenNumber,
                        TokenDisplay = @TokenDisplay,
                        Status = 1,
                        CreatedBy = @CreatedBy,
                        CreatedDateTime = SYSDATETIME()
                    WHERE OrderMasterId = @Id;
                END
                ELSE
                BEGIN
                    INSERT INTO dbo.tblOrderToken
                    (
                        OrderId,
                        OrderMasterId,
                        OrderTypeId,
                        BusinessDate,
                        TokenNumber,
                        TokenDisplay,
                        Status,
                        CreatedBy,
                        CreatedDateTime
                    )
                    VALUES
                    (
                        @Id,
                        @Id,
                        @EffectiveOrderTypeId,
                        @BusinessDate,
                        @TokenNumber,
                        @TokenDisplay,
                        1,
                        @CreatedBy,
                        SYSDATETIME()
                    );
                END;
            END;
        END;

        COMMIT TRANSACTION;

        SELECT @Id AS OrderMasterId,
               ISNULL(@EffectiveOrderTypeId, 0) AS OrderTypeId,
               @TokenNumber AS TokenNumber,
               @TokenDisplay AS TokenDisplay;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

/* Verify actual prefix configuration after applying the script. */
SELECT OrderTypeId, TokenPrefix, Status
FROM dbo.tblOrderType
WHERE OrderTypeId IN (1, 2, 3)
ORDER BY OrderTypeId;
GO

/* Verify branch-level grouping mode. */
SELECT Gid, BranchId, DailyTokenMode, Status
FROM dbo.tblGeneralSetting
WHERE Status = 1
ORDER BY BranchId, Gid DESC;
GO
