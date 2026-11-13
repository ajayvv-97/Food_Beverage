using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Bllincrediant
    {
        public int SetIngredientVariation(int ingredientDetailId, int variationId)
        {
            if (ingredientDetailId <= 0 || variationId < 0) return 0;
            DAL.DapperRepository<Dmnincrediant> repository = new DAL.DapperRepository<Dmnincrediant>();
            const string sql = @"
UPDATE dbo.tblIngredientDetails
SET VariationId = @VariationId
WHERE Ing_DetailsId = @Ing_DetailsId;";
            return repository.ExecuteText(sql, new { VariationId = variationId, Ing_DetailsId = ingredientDetailId });
        }

        public int DeactivateVariationIngredients(int productId, int variationId, int branchId)
        {
            if (productId <= 0 || variationId < 0) return 0;
            DAL.DapperRepository<Dmnincrediant> repository = new DAL.DapperRepository<Dmnincrediant>();
            const string sql = @"
UPDATE dbo.tblIngredientDetails
SET Status = 0
WHERE ProductId = @ProductId
  AND BranchId = @BranchId
  AND ISNULL(VariationId, 0) = @VariationId
  AND Status = 1;";
            return repository.ExecuteText(sql, new { ProductId = productId, BranchId = branchId, VariationId = variationId });
        }

        public Dmnincrediant AddData(string procedureName, Dmnincrediant objTestClass)
        {
            DAL.DapperRepository<Dmnincrediant> objDapperRepository = new DAL.DapperRepository<Dmnincrediant>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Ing_DetailsId", objTestClass.Ing_DetailsId);
            parms.Add("ItemId", objTestClass.ItemId); 
            parms.Add("quantity", objTestClass.quantity);
            parms.Add("unit", objTestClass.unit);
            parms.Add("Instructions", objTestClass.Instructions); 
            parms.Add("Recipe", objTestClass.Recipe);
            parms.Add("ProductId", objTestClass.ProductId);
            parms.Add("BranchId", objTestClass.BranchId); 
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            parms.Add("Deductionstage", objTestClass.Deductionstage);
            parms.Add("ProductType", objTestClass.ProductType);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}