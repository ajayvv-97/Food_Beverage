
using Dapper;
using Domain;

namespace BLL
{
    public class BllAddonGroup
    {
        // tblAddonGroup - Insert / Update
        public DmnAddonGroup AddEditData(
            string procedureName,
            DmnAddonGroup objTestClass)
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

            var result = dapperRepository.AddandReturnID(
                procedureName, parms);

            return result;
        }

        // tblAddonGroupDetail - Insert / Update
        public DmnAddonGroupDetail AddEditDataDetail(
            string procedureName,
            DmnAddonGroupDetail objTestClass)
        {
            DAL.DapperRepository<DmnAddonGroupDetail> dapperRepository =
                new DAL.DapperRepository<DmnAddonGroupDetail>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("GroupDetailId", objTestClass.GroupDetailId);
            parms.Add("GroupId", objTestClass.GroupId);
            parms.Add("ProductId", objTestClass.ProductId);
            parms.Add("DisplayOrder", objTestClass.DisplayOrder);
            parms.Add("Status", objTestClass.Status);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("ResetGroup", objTestClass.ResetGroup);

            var result = dapperRepository.AddandReturnID(
                procedureName, parms);

            return result;
        }

        // tblProductAddonGroup - Insert / Update
        public DmnProductAddonGroup AddEditProductAddonGroup(
            string procedureName,
            DmnProductAddonGroup objTestClass)
        {
            DAL.DapperRepository<DmnProductAddonGroup> dapperRepository =
                new DAL.DapperRepository<DmnProductAddonGroup>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("ProductAddonGroupId", objTestClass.ProductAddonGroupId);
            parms.Add("ProductId", objTestClass.ProductId);
            parms.Add("GroupId", objTestClass.GroupId);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("DisplayOrder", objTestClass.DisplayOrder);
            parms.Add("Status", objTestClass.Status);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);

            var result = dapperRepository.AddandReturnID(
                procedureName, parms);

            return result;
        }
    }
}
