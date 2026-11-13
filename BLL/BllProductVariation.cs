using Dapper;
using Domain;

namespace BLL
{
    public class BllProductVariation
    {
        public DmnProductVariation AddEditData(string procedureName, DmnProductVariation obj)
        {
            DAL.DapperRepository<DmnProductVariation> repo =  new DAL.DapperRepository<DmnProductVariation>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("VariationId", obj.VariationId);
            parms.Add("VariationName", obj.VariationName);
            parms.Add("VariationTypeId", obj.VariationTypeId);
            parms.Add("Price", obj.Price);
            parms.Add("PriceAddOn", obj.PriceAddOn);
            parms.Add("IsDefault", obj.IsDefault);
            parms.Add("DiscountAmt", obj.DiscountAmt);
            parms.Add("DiscountPer", obj.DiscountPer);
            parms.Add("QOH", obj.QOH);
            parms.Add("IsUnlimited", obj.IsUnlimited);
            parms.Add("ProductId", obj.ProductId);
            parms.Add("BranchId", obj.BranchId);
            parms.Add("CreatedBy", obj.CreatedBy);
            parms.Add("Status", obj.Status);

            var result = repo.AddandReturnID(procedureName, parms);
            return result;
        }
        public DmnProductVariation DeleteVariation(string procedureName, DmnProductVariation obj)
        {
            DAL.DapperRepository<DmnProductVariation> repo = new DAL.DapperRepository<DmnProductVariation>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("VariationId", obj.VariationId);
            parms.Add("ProductId", obj.ProductId);
            parms.Add("BranchId", obj.BranchId);
            parms.Add("CreatedBy", obj.CreatedBy); 
            var result = repo.AddandReturnID(procedureName, parms);
            return result;
        }
        public DmnProducts UpdateHasVariation(string procedureName, DmnProducts obj)
        {
            DAL.DapperRepository<DmnProducts> repo = new DAL.DapperRepository<DmnProducts>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("ProductId", obj.ProductId);
            parms.Add("HasVariation", obj.HasVariation);
            var result = repo.AddandReturnID(procedureName, parms);
            return result;
        }
    }
}