USE [FoodBeverage];
GO

/* Add the Kitchen KOT-only option. Existing installations default to the
   current interactive Kitchen behavior (KOT-only disabled). */
IF COL_LENGTH('dbo.tblGeneralSetting', 'EnableKitchenKOTOnly') IS NULL
BEGIN
    ALTER TABLE dbo.tblGeneralSetting
    ADD EnableKitchenKOTOnly BIT NOT NULL
        CONSTRAINT DF_tblGeneralSetting_EnableKitchenKOTOnly DEFAULT (0) WITH VALUES;
END;
GO

/* DailyTokenMode is already deployed by AddDailyTokenMode_tblGeneralSetting.sql.
   Keep this guard so this migration also works on older databases. */
IF COL_LENGTH('dbo.tblGeneralSetting', 'DailyTokenMode') IS NULL
BEGIN
    ALTER TABLE dbo.tblGeneralSetting
    ADD DailyTokenMode INT NOT NULL
        CONSTRAINT DF_tblGeneralSetting_DailyTokenMode_Upgrade DEFAULT (2) WITH VALUES;
END;
GO

-- Dashboard mapping:
-- DailyTokenMode = 1: Combined Pickup / Takeaway token sequence.
-- DailyTokenMode = 2: Separate Pickup and Takeaway token sequences.
-- EnableKitchenKOTOnly = 1: print new KOTs and automatically mark active kitchen
--                           rows Ready (Status = 3), without manual Start / Ready.
-- EnableKitchenKOTOnly = 0: retain the normal kitchen Start / Ready workflow.

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

ALTER PROCEDURE [dbo].[prdInsertGeneralsetting]
    @Gid INT,
    @printdal BIT,
    @Status INT,
    @BranchId INT,
    @CreatedBy INT,
    @EmployeeId INT,
    @xyprintdal BIT,
    @dalydal BIT,
    @EnableRFID BIT,
    @RequireCustomerInfo BIT,
    @EnableDefaultGmail BIT,
    @DefaultGmail VARCHAR(100),
    @AutoPrint BIT,
    @AutoDrawer BIT,
    @RequireCustomerInfo_recharge BIT,
    @Save_YZout BIT,
    @Save_Daily BIT,
    @YZPath VARCHAR(100),
    @DailyPath VARCHAR(100),
    @EnableGroupCardInPayment BIT,
    @EnableFamilyCardInPayment BIT,
    @EnableButtonView_GroupSale BIT,
    @EnableButtonView_Family BIT,
    @AllowManualCardInputDuringPayment BIT,
    @AutoLogoutWhenIdle BIT,
    @AutoLogoutIdleTimeoutMinutes DECIMAL(18,1),
    @LogoutFormscustom VARCHAR(200),
    @DefaultUserName VARCHAR(100),
    @EnableUserPrompt BIT,
    @EnableCompbillPrompt BIT,
    @ProductpageperCount INT,
    @EnableCompReasonPrompt BIT,

    -- Package Tax Settings
    @PackigHsncode VARCHAR(50),
    @Tax VARCHAR(10),
    @TaxType VARCHAR(50),
    @TaxSettingId INT,
    @EnablePartyBookingKOT BIT,

    -- Kitchen and daily token settings (optional for backward compatibility)
    @EnableKitchenKOTOnly BIT = NULL,
    @DailyTokenMode INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @genid BIGINT;

    IF @Gid > 0
    BEGIN
        UPDATE dbo.tblGeneralSetting
        SET
            BranchId = @BranchId,
            printdal = @printdal,
            xyprintdal = @xyprintdal,
            dalydal = @dalydal,
            ModifiedDateTime = SYSDATETIME(),
            ModifiedBy = @CreatedBy,
            Status = @Status,
            RequireCustomerInfo = @RequireCustomerInfo,
            EnableRFID = @EnableRFID,
            EmployeeId = @EmployeeId,
            EnableDefaultGmail = @EnableDefaultGmail,
            DefaultGmail = @DefaultGmail,
            AutoPrint = @AutoPrint,
            AutoDrawer = @AutoDrawer,
            RequireCustomerInfo_recharge = @RequireCustomerInfo_recharge,
            Save_YZout = @Save_YZout,
            Save_Daily = @Save_Daily,
            YZPath = @YZPath,
            DailyPath = @DailyPath,
            EnableGroupCardInPayment = @EnableGroupCardInPayment,
            EnableFamilyCardInPayment = @EnableFamilyCardInPayment,
            EnableButtonView_GroupSale = @EnableButtonView_GroupSale,
            EnableButtonView_Family = @EnableButtonView_Family,
            AllowManualCardInputDuringPayment = @AllowManualCardInputDuringPayment,
            AutoLogoutWhenIdle = @AutoLogoutWhenIdle,
            AutoLogoutIdleTimeoutMinutes = @AutoLogoutIdleTimeoutMinutes,
            LogoutFormscustom = @LogoutFormscustom,
            DefaultUserName = @DefaultUserName,
            EnableUserPrompt = @EnableUserPrompt,
            EnableCompbillPrompt = @EnableCompbillPrompt,
            ProductpageperCount = @ProductpageperCount,
            EnableCompReasonPrompt = @EnableCompReasonPrompt,

            -- Package Tax Settings
            PackigHsncode = @PackigHsncode,
            Tax = @Tax,
            TaxType = @TaxType,
            TaxSettingId = @TaxSettingId,
            EnablePartyBookingKOT = @EnablePartyBookingKOT,

            -- Kitchen and daily token settings
            EnableKitchenKOTOnly = ISNULL(@EnableKitchenKOTOnly, EnableKitchenKOTOnly),
            DailyTokenMode = CASE
                WHEN @DailyTokenMode IN (1, 2) THEN @DailyTokenMode
                ELSE ISNULL(DailyTokenMode, 2)
            END
        WHERE Gid = @Gid;

        SET @genid = @Gid;
    END
    ELSE
    BEGIN
        INSERT INTO dbo.tblGeneralSetting
        (
            printdal,
            CreatedBy,
            CreatedDateTime,
            BranchId,
            Status,
            EmployeeId,
            xyprintdal,
            dalydal,
            RequireCustomerInfo,
            EnableRFID,
            DefaultGmail,
            EnableDefaultGmail,
            AutoPrint,
            AutoDrawer,
            RequireCustomerInfo_recharge,
            Save_YZout,
            Save_Daily,
            YZPath,
            DailyPath,
            EnableGroupCardInPayment,
            EnableFamilyCardInPayment,
            EnableButtonView_GroupSale,
            EnableButtonView_Family,
            AllowManualCardInputDuringPayment,
            AutoLogoutWhenIdle,
            AutoLogoutIdleTimeoutMinutes,
            LogoutFormscustom,
            DefaultUserName,
            EnableUserPrompt,
            EnableCompbillPrompt,
            ProductpageperCount,
            EnableCompReasonPrompt,

            -- Package Tax Settings
            PackigHsncode,
            Tax,
            TaxType,
            TaxSettingId,
            EnablePartyBookingKOT,

            -- Kitchen and daily token settings
            EnableKitchenKOTOnly,
            DailyTokenMode
        )
        VALUES
        (
            @printdal,
            @CreatedBy,
            SYSDATETIME(),
            @BranchId,
            @Status,
            @EmployeeId,
            @xyprintdal,
            @dalydal,
            @RequireCustomerInfo,
            @EnableRFID,
            @DefaultGmail,
            @EnableDefaultGmail,
            @AutoPrint,
            @AutoDrawer,
            @RequireCustomerInfo_recharge,
            @Save_YZout,
            @Save_Daily,
            @YZPath,
            @DailyPath,
            @EnableGroupCardInPayment,
            @EnableFamilyCardInPayment,
            @EnableButtonView_GroupSale,
            @EnableButtonView_Family,
            @AllowManualCardInputDuringPayment,
            @AutoLogoutWhenIdle,
            @AutoLogoutIdleTimeoutMinutes,
            @LogoutFormscustom,
            @DefaultUserName,
            @EnableUserPrompt,
            @EnableCompbillPrompt,
            @ProductpageperCount,
            @EnableCompReasonPrompt,

            -- Package Tax Settings
            @PackigHsncode,
            @Tax,
            @TaxType,
            @TaxSettingId,
            @EnablePartyBookingKOT,

            -- Kitchen and daily token settings
            ISNULL(@EnableKitchenKOTOnly, 0),
            CASE WHEN @DailyTokenMode IN (1, 2) THEN @DailyTokenMode ELSE 2 END
        );

        SET @genid = SCOPE_IDENTITY();
    END

    SELECT @genid AS Gid;
END;
GO

-- Verify the new settings and their defaults.
SELECT Gid, BranchId, EnableKitchenKOTOnly, DailyTokenMode
FROM dbo.tblGeneralSetting
WHERE Status = 1;
GO
