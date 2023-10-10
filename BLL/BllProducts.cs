using Dapper;
using Domain;
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


			parms.Add("DiscountAmt", objTestClass.DiscountAmt);
			parms.Add("DiscountPer", objTestClass.DiscountPer);
			parms.Add("OfferDescription", objTestClass.OfferDescription);
			parms.Add("MRP", objTestClass.MRP);
			parms.Add("upc", objTestClass.upc);

			parms.Add("CreatedBy", objTestClass.CreatedBy);		
			parms.Add("Status", objTestClass.Status);
			parms.Add("BranchId", objTestClass.BranchId);
			var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
			return lstData;
		}
	}
}

