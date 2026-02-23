using Dapper;
using Domain;

namespace BLL
{
    public class BllEventCommonItems
    {
        public DmnEventCommonItems AddeditData(string procedureName, DmnEventCommonItems obj)
        {
            DAL.DapperRepository<DmnEventCommonItems> repo =
                new DAL.DapperRepository<DmnEventCommonItems>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("EventItemId", obj.EventItemId);
            parms.Add("EventCategoryType", obj.EventCategoryType);
            parms.Add("ItemName", obj.ItemName);
            parms.Add("Quantity", obj.Quantity);
            parms.Add("Price", obj.Price);
            parms.Add("TaxSettingsId", obj.TaxSettingsId);
            parms.Add("Tax", obj.Tax);
            parms.Add("TaxType", obj.TaxType);
            parms.Add("DiscountPer", obj.DiscountPer);
            parms.Add("DiscountAmt", obj.DiscountAmt);
            parms.Add("TotalAmount", obj.TotalAmount);
            parms.Add("Description", obj.Description);
            parms.Add("BranchId", obj.BranchId);
            parms.Add("CreatedBy", obj.CreatedBy);
            parms.Add("Status", obj.Status);

            var data = repo.AddandReturnID(procedureName, parms);
            return data;
        }
    }
}