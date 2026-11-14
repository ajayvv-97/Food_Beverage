using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Bllincrediant
    {
        public int InsertVariationIngredient(Dmnincrediant ingredient, int variationId)
        {
            if (ingredient == null || ingredient.ProductId <= 0 || ingredient.ItemId <= 0 ||
                variationId < 0)
                return 0;

            DAL.DapperRepository<Dmnincrediant> repository =
                new DAL.DapperRepository<Dmnincrediant>();
            const string sql = @"
INSERT INTO dbo.tblIngredientDetails
    (ItemId, quantity, Unit, ProductId, BranchId, CreatedBy, Status,
     Deductionstage, ProductType, VariationId)
OUTPUT INSERTED.Ing_DetailsId
VALUES
    (@ItemId, @Quantity, @Unit, @ProductId, @BranchId, @CreatedBy, @Status,
     @Deductionstage, @ProductType, @VariationId);";

            return repository.ExecuteScalarText<int>(sql, new
            {
                ingredient.ItemId,
                Quantity = ingredient.quantity,
                Unit = ingredient.unit,
                ingredient.ProductId,
                ingredient.BranchId,
                ingredient.CreatedBy,
                ingredient.Status,
                ingredient.Deductionstage,
                ingredient.ProductType,
                VariationId = variationId
            });
        }

        public int UpdateVariationIngredient(Dmnincrediant ingredient, int variationId)
        {
            // VariationId = 0 is the valid legacy/base recipe for products
            // without configured variations. Only negative temporary IDs are invalid.
            if (ingredient == null || ingredient.Ing_DetailsId <= 0 ||
                ingredient.ProductId <= 0 || variationId < 0)
                return 0;

            DAL.DapperRepository<Dmnincrediant> repository =
                new DAL.DapperRepository<Dmnincrediant>();
            const string sql = @"
UPDATE dbo.tblIngredientDetails
SET ItemId = @ItemId,
    quantity = @Quantity,
    Unit = @Unit,
    ProductType = @ProductType,
    Deductionstage = @Deductionstage,
    Status = @Status,
    VariationId = @VariationId
WHERE Ing_DetailsId = @Ing_DetailsId
  AND ProductId = @ProductId
  AND BranchId = @BranchId;";
            return repository.ExecuteText(sql, new
            {
                ingredient.ItemId,
                Quantity = ingredient.quantity,
                Unit = ingredient.unit,
                ingredient.ProductType,
                ingredient.Deductionstage,
                ingredient.Status,
                VariationId = variationId,
                ingredient.Ing_DetailsId,
                ingredient.ProductId,
                ingredient.BranchId
            });
        }

        public int SetIngredientVariation(int ingredientDetailId, int variationId)
        {
            if (ingredientDetailId <= 0 || variationId < 0) return 0;
            DAL.DapperRepository<Dmnincrediant> repository = new DAL.DapperRepository<Dmnincrediant>();
            const string sql = @"
UPDATE dbo.tblIngredientDetails
SET VariationId = @VariationId
WHERE Ing_DetailsId = @Ing_DetailsId;";
            // Treat this as an idempotent link operation. SQL Server triggers or
            // NOCOUNT settings can make Execute return 0 even when the row exists
            // and the requested VariationId is already assigned.
            repository.ExecuteText(sql, new { VariationId = variationId, Ing_DetailsId = ingredientDetailId });
            return 1;
        }

        public int DeactivateVariationIngredientsExcept(
            int productId, int variationId, int branchId, string retainedIngredientIds)
        {
            if (productId <= 0 || variationId < 0)
                return 0;

            DAL.DapperRepository<Dmnincrediant> repository =
                new DAL.DapperRepository<Dmnincrediant>();
            const string sql = @"
UPDATE dbo.tblIngredientDetails
SET Status = 0
WHERE ProductId = @ProductId
  AND BranchId = @BranchId
  AND ISNULL(VariationId, 0) = @VariationId
  AND Status = 1
  AND
  (
      ISNULL(@RetainedIds, '') = ''
      OR Ing_DetailsId NOT IN
      (
          SELECT TRY_CAST(value AS INT)
          FROM STRING_SPLIT(@RetainedIds, ',')
          WHERE TRY_CAST(value AS INT) IS NOT NULL
      )
  );";
            return repository.ExecuteText(sql, new
            {
                ProductId = productId,
                BranchId = branchId,
                VariationId = variationId,
                RetainedIds = retainedIngredientIds ?? string.Empty
            });
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