USE [FoodBeverage]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
    Daily-token search for Kitchen View.

    Search type values:
      0 = All active kitchen orders
      1 = Table Code
      2 = Daily Pickup / Takeaway token (for example P005 or T005)
      3 = Kitchen Number

    Token numbers are scoped to the same BusinessDate used by
    prdInsert_Upd_OrderMaster when allocating the daily token.
*/
ALTER PROCEDURE [dbo].[prdKitchenData]
    @Serachdata VARCHAR(50),
    @Serachtype INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @BusinessDate DATE = CONVERT(DATE, SYSDATETIME());

    IF @Serachtype = 0
    BEGIN
        SELECT
            ko.OrderMasterId AS Field1,
            ko.OrderNo AS Field2,
            ko.KOTPrintStatus AS Field3
        FROM dbo.tblkitchenOrder AS ko
        WHERE ko.Status IN (1, 2)
          AND ko.AddonStatus IN (1, 0)
          AND ko.PreparationMode = 1
        ORDER BY ko.Koid DESC;
    END
    ELSE IF @Serachtype = 1
    BEGIN
        SELECT
            ko.OrderMasterId AS Field1,
            ko.OrderNo AS Field2,
            ko.KOTPrintStatus AS Field3
        FROM dbo.tblkitchenOrder AS ko
        LEFT JOIN dbo.tblTable_info AS tb
            ON tb.Tid = ko.Tid
           AND tb.Status = 1
        WHERE ko.Status IN (1, 2)
          AND ko.AddonStatus IN (1, 0)
          AND tb.TableCode LIKE '%' + @Serachdata + '%'
          AND ko.PreparationMode = 1
        ORDER BY ko.Koid DESC;
    END
    ELSE IF @Serachtype = 2
    BEGIN
        SELECT
            ko.OrderMasterId AS Field1,
            ko.OrderNo AS Field2,
            ko.KOTPrintStatus AS Field3
        FROM dbo.tblkitchenOrder AS ko
        INNER JOIN dbo.tblOrderToken AS ot
            ON ot.OrderMasterId = ko.OrderMasterId
           AND ot.BusinessDate = @BusinessDate
           AND ot.OrderTypeId IN (1, 2)
        WHERE ko.Status IN (1, 2)
          AND ko.AddonStatus IN (1, 0)
          AND ko.PreparationMode = 1
          AND UPPER(LTRIM(RTRIM(ot.TokenDisplay))) =
              UPPER(LTRIM(RTRIM(@Serachdata)))
        ORDER BY ko.Koid DESC;
    END
    ELSE IF @Serachtype = 3
    BEGIN
        SELECT
            ko.OrderMasterId AS Field1,
            ko.OrderNo AS Field2,
            ko.KOTPrintStatus AS Field3
        FROM dbo.tblkitchenOrder AS ko
        WHERE ko.Status IN (1, 2)
          AND ko.AddonStatus IN (1, 0)
          AND ko.KitchenNumber = @Serachdata
          AND ko.PreparationMode = 1
        ORDER BY ko.Koid DESC;
    END
END
GO
