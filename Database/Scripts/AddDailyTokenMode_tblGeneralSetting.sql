USE [FoodBeverage];
GO

IF COL_LENGTH('dbo.tblGeneralSetting', 'DailyTokenMode') IS NULL
BEGIN
    ALTER TABLE dbo.tblGeneralSetting
    ADD DailyTokenMode INT NOT NULL
        CONSTRAINT DF_tblGeneralSetting_DailyTokenMode DEFAULT (2) WITH VALUES;
END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.check_constraints
    WHERE name = 'CK_tblGeneralSetting_DailyTokenMode'
      AND parent_object_id = OBJECT_ID('dbo.tblGeneralSetting')
)
BEGIN
    ALTER TABLE dbo.tblGeneralSetting
    ADD CONSTRAINT CK_tblGeneralSetting_DailyTokenMode
        CHECK (DailyTokenMode IN (1, 2, 3));
END;
GO

-- Mode 1: Pickup + Takeaway + Delivery use one shared daily sequence.
-- Mode 2: Pickup, Takeaway and Delivery use separate daily sequences.
-- Mode 3: Pickup + Takeaway share; Delivery uses a separate sequence.
--
-- Existing installations keep the current separate Pickup / Takeaway behavior by default.
SELECT Gid, BranchId, DailyTokenMode
FROM dbo.tblGeneralSetting
WHERE Status = 1;
GO
