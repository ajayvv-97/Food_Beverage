/*
    Kitchen View preparation timing support.

    Run this script once against the Food_Beverage database.
    It is safe to re-run and preserves existing timestamps.
*/
SET NOCOUNT ON;
GO

IF OBJECT_ID(N'dbo.tblkitchenOrder', N'U') IS NULL
BEGIN
    THROW 50001, 'Required table dbo.tblkitchenOrder was not found.', 1;
END
GO

IF COL_LENGTH(N'dbo.tblkitchenOrder', N'StartDate') IS NULL
BEGIN
    ALTER TABLE dbo.tblkitchenOrder
        ADD StartDate DATETIME NULL;
END
GO

IF COL_LENGTH(N'dbo.tblkitchenOrder', N'EndDate') IS NULL
BEGIN
    ALTER TABLE dbo.tblkitchenOrder
        ADD EndDate DATETIME NULL;
END
GO
