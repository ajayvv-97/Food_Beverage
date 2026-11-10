USE [FoodBeverage];
GO

/*
    Enables or disables scheduled Party Booking KOT processing per branch.
    Run this migration before deploying the updated FoodandBeverage application.
    Existing branches default to disabled to preserve current behaviour.
*/

IF COL_LENGTH(N'dbo.tblGeneralSetting', N'EnablePartyBookingKOT') IS NULL
BEGIN
    ALTER TABLE dbo.tblGeneralSetting
        ADD EnablePartyBookingKOT BIT NOT NULL
            CONSTRAINT DF_tblGeneralSetting_EnablePartyBookingKOT
            DEFAULT (0) WITH VALUES;
END;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/*
    The existing prdInsertGeneralsetting procedure is intentionally unchanged.
    FrmDashboard saves the main General Settings row first, then calls this
    focused updater with the returned Gid.
*/
CREATE OR ALTER PROCEDURE dbo.prdUpdateGeneralSettingPartyBookingKOT
    @Gid INT,
    @BranchId INT,
    @EnablePartyBookingKOT BIT,
    @ModifiedBy BIGINT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE dbo.tblGeneralSetting
        SET
            EnablePartyBookingKOT = ISNULL(@EnablePartyBookingKOT, 0),
            ModifiedBy = @ModifiedBy,
            ModifiedDateTime = GETDATE()
        WHERE Gid = @Gid
          AND BranchId = @BranchId
          AND ISNULL(Status, 1) = 1;

        IF @@ROWCOUNT = 0
            THROW 51020, 'The active General Settings record was not found for this branch.', 1;

        COMMIT TRANSACTION;

        SELECT Gid, EnablePartyBookingKOT
        FROM dbo.tblGeneralSetting
        WHERE Gid = @Gid
          AND BranchId = @BranchId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;
GO

SELECT Gid, BranchId, ISNULL(EnablePartyBookingKOT, 0) AS EnablePartyBookingKOT
FROM dbo.tblGeneralSetting
WHERE ISNULL(Status, 1) = 1
ORDER BY BranchId, Gid;
GO
