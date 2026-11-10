using Dapper;
using Domain;
using System;
namespace BLL
{
	public class BllProducts
	{
		public DmnProducts AddEditData(string procedureName, DmnProducts objTestClass)
		{
			DAL.DapperRepository<DmnProducts> objDapperRepository = new DAL.DapperRepository<DmnProducts>();
			DynamicParameters parms = new DynamicParameters();
			parms.Add("ProductId", objTestClass.ProductId);
			parms.Add("Product", objTestClass.Product);
			parms.Add("Description", objTestClass.Description);
			parms.Add("Tax", objTestClass.Tax);
			parms.Add("TaxSettingsId", objTestClass.TaxSettingsId);
			parms.Add("Fcid", objTestClass.Fcid);
			parms.Add("Price", objTestClass.Price);
			//parms.Add("Quantity", objTestClass.Quantity);
			parms.Add("DiscountAmt", objTestClass.DiscountAmt);
			parms.Add("DiscountPer", objTestClass.DiscountPer);
			parms.Add("OfferDescription", objTestClass.OfferDescription);
			parms.Add("MRP", objTestClass.MRP);
			parms.Add("upc", objTestClass.UPC);
			parms.Add("TaxType", objTestClass.TaxType);
			parms.Add("CreatedBy", objTestClass.CreatedBy);
			parms.Add("Status", objTestClass.Status);
			parms.Add("BranchId", objTestClass.BranchId);
			parms.Add("ItemCode", objTestClass.ItemCode);
            parms.Add("PreparationMode", objTestClass.PreparationMode);
            parms.Add("IsStockTracked", objTestClass.IsStockTracked);
			parms.Add("FoodType", objTestClass.FoodType);
			parms.Add("HasVariation", objTestClass.HasVariation);

			parms.Add("ForeColor", objTestClass.ForeColor);
			parms.Add("BackColor", objTestClass.BackColor);

			parms.Add("HSNcode", objTestClass.HSNcode); 
			parms.Add("FoodPreparationType", objTestClass.FoodPreparationType);
			parms.Add("PackagingRequired", objTestClass.PackagingRequired);
			var lstData = objDapperRepository.AddandReturnID(procedureName, parms);

            // Persist the new product settings explicitly as well. This protects
            // against older deployed prdInsert_Upd_Product definitions that accept
            // the parameters but do not write these two columns.
            int savedProductId = lstData != null && lstData.ProductId > 0
                ? lstData.ProductId
                : objTestClass.ProductId;

            if (savedProductId > 0)
            {
                using (var connection = DAL.DapperRepository<DmnProducts>.OpenConnection())
                {
                    int affectedRows = connection.Execute(
                        @"
UPDATE dbo.tblProduct
SET FoodPreparationType = @FoodPreparationType,
    PackagingRequired = @PackagingRequired
WHERE ProductId = @ProductId;",
                        new
                        {
                            ProductId = savedProductId,
                            FoodPreparationType = objTestClass.FoodPreparationType,
                            PackagingRequired = objTestClass.PackagingRequired
                        });

                    if (affectedRows <= 0)
                    {
                        throw new InvalidOperationException(
                            "Product was saved, but FoodPreparationType/PackagingRequired " +
                            "could not be persisted for ProductId " + savedProductId + ".");
                    }
                }

                if (lstData != null)
                {
                    lstData.FoodPreparationType = objTestClass.FoodPreparationType;
                    lstData.PackagingRequired = objTestClass.PackagingRequired;
                }
            }

            return lstData;
		}
		public DmnGridFields AddEditData_fav(string procedureName, DmnGridFields objTestClass)
		{
			DAL.DapperRepository<DmnGridFields> objDapperRepository = new DAL.DapperRepository<DmnGridFields>();
			DynamicParameters parms = new DynamicParameters();
			parms.Add("ProductId", objTestClass.Field1);
			parms.Add("Fav_Status", objTestClass.Field2);
			parms.Add("saletype", objTestClass.Field3);
			var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
			return lstData;
		}
		public DmnGridFields AddEditData_voidsale(string procedureName, DmnGridFields objTestClass)
		{
			DAL.DapperRepository<DmnGridFields> objDapperRepository = new DAL.DapperRepository<DmnGridFields>();
			DynamicParameters parms = new DynamicParameters();
			parms.Add("MasterId", objTestClass.Field1);
			var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
			return lstData;
		}
		
	}
}

