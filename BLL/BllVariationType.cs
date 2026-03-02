using Dapper;
using Domain;

namespace BLL
{
    public  class BllVariationType
    {
        public DmnVariationType AddEdit(string procedure, DmnVariationType obj)
        {
            var repo = new DAL.DapperRepository<DmnVariationType>();
            DynamicParameters param = new DynamicParameters();

            param.Add("VariationId", obj.VariationId);
            param.Add("VariationName", obj.VariationName);
            param.Add("Price", obj.Price);
            param.Add("ProductId", obj.ProductId);
            param.Add("BranchId", obj.BranchId);
            param.Add("CreatedBy", obj.CreatedBy);
            param.Add("Status", obj.Status);

            return repo.AddandReturnID(procedure, param);
        }

        public DmnVariationType Delete(string procedure, DmnVariationType obj)
        {
            var repo = new DAL.DapperRepository<DmnVariationType>();
            DynamicParameters param = new DynamicParameters();

            param.Add("VariationId", obj.VariationId);
            param.Add("CreatedBy", obj.CreatedBy);
            param.Add("Status", obj.Status);

            return repo.AddandReturnID(procedure, param);
        }
    }
}
