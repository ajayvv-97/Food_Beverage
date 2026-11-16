using Dapper;
using Domain;

namespace BLL
{
    public class BllAddonGroup
    {
        // Insert or update the group and its selected products through one
        // stored procedure, following the same pattern as Blldrawer.
        public DmnAddonGroup AddEditData(string procedureName, DmnAddonGroup objTestClass)
        {
            DAL.DapperRepository<DmnAddonGroup> dapperRepository =
                new DAL.DapperRepository<DmnAddonGroup>();

            DynamicParameters parms = new DynamicParameters();
            parms.Add("GroupId", objTestClass.GroupId);
            parms.Add("GroupName", objTestClass.GroupName);
            parms.Add("Description", objTestClass.Description);
            parms.Add("SelectionMode", objTestClass.SelectionMode);
            parms.Add("MinSelection", objTestClass.MinSelection);
            parms.Add("MaxSelection", objTestClass.MaxSelection);
            parms.Add("IsRequired", objTestClass.IsRequired);
            parms.Add("Status", objTestClass.Status);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("ProductsXml", objTestClass.ProductsXml);

            var result = dapperRepository.AddandReturnID(procedureName, parms);
            return result;
        }
    }
}
