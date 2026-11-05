USE [FoodBeverage];
GO

/*
    Packing Tax configuration for tblGeneralSetting.

    Run this script once before using the updated FoodandBeverage application.
    Existing General Settings records remain valid; the new columns are nullable.
*/

IF COL_LENGTH(N'dbo.tblGeneralSetting', N'Tax') IS NULL
BEGIN
    ALTER TABLE dbo.tblGeneralSetting
        ADD Tax VARCHAR(20) NULL;
END;
GO

IF COL_LENGTH(N'dbo.tblGeneralSetting', N'TaxType') IS NULL
BEGIN
    ALTER TABLE dbo.tblGeneralSetting
        ADD TaxType VARCHAR(20) NULL;
END;
GO

IF COL_LENGTH(N'dbo.tblGeneralSetting', N'TaxSettingId') IS NULL
BEGIN
    ALTER TABLE dbo.tblGeneralSetting
        ADD TaxSettingId INT NULL;
END;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/*
    Separate update procedure used by Bllgeneralsetting.UpdatePackingTaxSettings.

    The existing prdInsertGeneralsetting procedure is intentionally unchanged.
    FrmDashboard.btnsavegen_Click first saves the normal General Settings row,
    then calls this procedure with the returned Gid.

    Tax:
        Inclusive / Exclusive
    TaxType:
        GST / VAT
    TaxSettingId:
        Selected row from tblTaxSettings.
*/
CREATE OR ALTER PROCEDURE dbo.prdUpdateGeneralSettingPackingTax
    @Gid INT,
    @BranchId INT,
    @Tax VARCHAR(20) = NULL,
    @TaxType VARCHAR(20) = NULL,
    @TaxSettingId INT = NULL,
    @ModifiedBy BIGINT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE dbo.tblGeneralSetting
        SET
            Tax = NULLIF(LTRIM(RTRIM(@Tax)), ''),
            TaxType = NULLIF(LTRIM(RTRIM(@TaxType)), ''),
            TaxSettingId = NULLIF(@TaxSettingId, 0),
            ModifiedBy = @ModifiedBy,
            ModifiedDateTime = GETDATE()
        WHERE Gid = @Gid
          AND BranchId = @BranchId
          AND ISNULL(Status, 1) = 1;

        IF @@ROWCOUNT = 0
            THROW 51010, 'The active General Settings record was not found for this branch.', 1;

        COMMIT TRANSACTION;

        SELECT Gid
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
