/*
    Party Pack fixed-price mode migration
    Run against the application database before using fixed-price mode.
*/
IF COL_LENGTH('dbo.tblPartyPackMaster', 'FixedPrice') IS NULL
BEGIN
    ALTER TABLE dbo.tblPartyPackMaster
        ADD FixedPrice DECIMAL(18, 2) NOT NULL
            CONSTRAINT DF_tblPartyPackMaster_FixedPrice DEFAULT (0);
END;
GO

IF COL_LENGTH('dbo.tblPartyPackMaster', 'FixedPriceStatus') IS NULL
BEGIN
    ALTER TABLE dbo.tblPartyPackMaster
        ADD FixedPriceStatus BIT NOT NULL
            CONSTRAINT DF_tblPartyPackMaster_FixedPriceStatus DEFAULT (0);
END;
GO

/*
    Update dbo.prdInsert_Upd_PartyPack to accept @FixedPrice DECIMAL(18,2)
    and @FixedPriceStatus BIT, and include both fields in its INSERT/UPDATE
    statements for dbo.tblPartyPackMaster. The stored procedure definition
    is maintained in the database and is not present in this repository.
*/
