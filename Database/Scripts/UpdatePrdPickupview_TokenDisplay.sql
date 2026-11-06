USE [FoodBeverage]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Prd_Pickupview]
    @Serachmode INT
AS
BEGIN
    SET NOCOUNT ON;

    IF @Serachmode = 0
    BEGIN
        SELECT
            om.PkId AS Field1,
            om.PickupOrderFlag AS Field2,
            om.OrderMasterId AS Field3,
            om.NetAmount AS Field4,
            om.KitchenNumber AS Field5,
            ISNULL(NULLIF(LTRIM(RTRIM(ot.TokenDisplay)), ''), '') AS Field6
        FROM dbo.tblOrderMaster AS om
        LEFT JOIN dbo.tblOrderToken AS ot
            ON ot.OrderMasterId = om.OrderMasterId
           AND ot.OrderTypeId = 1
        WHERE om.PickupOrderFlag = 1
          AND om.Status IN (1, 2, 3);
    END
    ELSE IF @Serachmode = 1
    BEGIN
        SELECT
            om.PkId AS Field1,
            om.PickupOrderFlag AS Field2,
            om.OrderMasterId AS Field3,
            om.NetAmount AS Field4,
            om.KitchenNumber AS Field5,
            ISNULL(NULLIF(LTRIM(RTRIM(ot.TokenDisplay)), ''), '') AS Field6,
            om.IsCustomerInformedCancel AS Field7
        FROM dbo.tblOrderMaster AS om
        LEFT JOIN dbo.tblOrderToken AS ot
            ON ot.OrderMasterId = om.OrderMasterId
           AND ot.OrderTypeId = 1
        WHERE om.PickupOrderFlag = 1
          AND (
                (om.Status IN (1, 2) AND om.IsCustomerInformedCancel IS NULL)
                OR
                (om.Status = 0 AND om.IsCustomerInformedCancel = 1)
              );
    END
    ELSE IF @Serachmode = 3
    BEGIN
        SELECT
            om.PkId AS Field1,
            om.PickupOrderFlag AS Field2,
            om.OrderMasterId AS Field3,
            om.NetAmount AS Field4,
            om.KitchenNumber AS Field5,
            ISNULL(NULLIF(LTRIM(RTRIM(ot.TokenDisplay)), ''), '') AS Field6
        FROM dbo.tblOrderMaster AS om
        LEFT JOIN dbo.tblOrderToken AS ot
            ON ot.OrderMasterId = om.OrderMasterId
           AND ot.OrderTypeId = 1
        WHERE om.PickupOrderFlag = 1
          AND om.Status = 3;
    END
END
GO
