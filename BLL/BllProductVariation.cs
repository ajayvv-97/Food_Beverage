using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllProductVariation
    {
        public DmnProductVariation AddEdit(string procedure, DmnProductVariation obj)
        {
            var repo = new DAL.DapperRepository<DmnProductVariation>();
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

        public DmnProductVariation Delete(string procedure, DmnProductVariation obj)
        {
            var repo = new DAL.DapperRepository<DmnProductVariation>();
            DynamicParameters param = new DynamicParameters();

            param.Add("VariationId", obj.VariationId);
            param.Add("CreatedBy", obj.CreatedBy); 
            param.Add("Status", obj.Status);       

            return repo.AddandReturnID(procedure, param);
        }
    }
}
