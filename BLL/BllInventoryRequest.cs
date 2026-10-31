using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BLL
{
    public class BllInventoryRequest
    {
        public List<DmnInventoryRequestItem> SearchProducts(string search)
        {
            using (IDbConnection connection = DAL.DapperRepository<DmnInventoryRequestItem>.OpenConnection())
                return connection.Query<DmnInventoryRequestItem>(
                    @"SELECT p.ProductId AS ItemId, p.Product AS ItemName,
                             CAST(0 AS DECIMAL(18,2)) AS CurrentStock, CAST(1 AS INT) AS ProductType
                      FROM dbo.tblProduct p
                      WHERE ISNULL(p.Status,1) <> 0 AND (@Search = '' OR p.Product LIKE '%' + @Search + '%')
                      ORDER BY p.Product",
                    new { Search = search ?? string.Empty }).ToList();
        }

        public List<DmnInventoryRequestItem> SearchRawMaterials(string search)
        {
            using (IDbConnection connection = DAL.DapperRepository<DmnInventoryRequestItem>.OpenConnection())
                return connection.Query<DmnInventoryRequestItem>(
                    @"SELECT CAST(i.ItemId AS INT) AS ItemId, i.Itemname AS ItemName,
                             ISNULL(pr.QOH,0) AS CurrentStock, i.unit AS UnitId, CAST(2 AS INT) AS ProductType
                      FROM dbo.tblItem i
                      OUTER APPLY
                      (
                          SELECT TOP (1) r.QOH FROM dbo.tblProductRate r
                          WHERE r.ProductId = i.ItemId AND ISNULL(r.ProductType,0) = 0 ORDER BY r.Id DESC
                      ) pr
                      WHERE ISNULL(i.Saletype,0) = 0 AND ISNULL(i.Status,1) <> 0
                        AND (@Search = '' OR i.Itemname LIKE '%' + @Search + '%')
                      ORDER BY i.Itemname",
                    new { Search = search ?? string.Empty }).ToList();
        }

        public List<DmnInventoryRequestIngredient> GetProductIngredients(int productId, decimal plannedQty)
        {
            using (IDbConnection connection = DAL.DapperRepository<DmnInventoryRequestIngredient>.OpenConnection())
                return connection.Query<DmnInventoryRequestIngredient>(
                    @"SELECT d.Ing_DetailsId AS IngredientDetailId, d.ItemId, i.Itemname AS IngredientName,
                             ISNULL(d.quantity,0) AS PerPortionQty,
                             ISNULL(d.quantity,0) * @PlannedQty AS TotalRequiredQty, d.Unit AS UnitId,
                             ISNULL(pr.QOH,0) AS CurrentStock,
                             ISNULL(pr.QOH,0) - ISNULL(d.quantity,0) * @PlannedQty AS ProjectedBalance,
                             d.ProductId
                      FROM dbo.tblIngredientDetails d
                      LEFT JOIN dbo.tblItem i ON i.ItemId = d.ItemId
                      OUTER APPLY
                      (
                          SELECT TOP (1) r.QOH FROM dbo.tblProductRate r
                          WHERE r.ProductId = d.ItemId AND ISNULL(r.ProductType,0) = 0 ORDER BY r.Id DESC
                      ) pr
                      WHERE d.ProductId = @ProductId AND ISNULL(d.Status,1) <> 0
                      ORDER BY d.Ing_DetailsId",
                    new { ProductId = productId, PlannedQty = plannedQty }).ToList();
        }

        public int Save(DmnInventoryRequestMaster master, IList<DmnInventoryRequestItem> details)
        {
            if (master == null) throw new ArgumentNullException(nameof(master));
            if (details == null || details.Count == 0)
                throw new InvalidOperationException("Add at least one batch product or raw material before saving.");

            using (IDbConnection connection = DAL.DapperRepository<DmnInventoryRequestMaster>.OpenConnection())
            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    int requestId = connection.Query<int>(
                        @"INSERT INTO dbo.tblInventoryRequest
                          (RequestNo, RequestDate, RequiredDate, DepartmentId, EmployeeId, Remarks, RequestType,
                           CreatedBy, CreatedOn, BranchId, Status)
                          VALUES (@RequestNo, @RequestDate, @RequiredDate, @DepartmentId, @EmployeeId, @Remarks, @RequestType,
                                  @CreatedBy, GETDATE(), @BranchId, 1);
                          SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        master, transaction).Single();

                    foreach (DmnInventoryRequestItem detail in details)
                    {
                        detail.RequestId = requestId;
                        connection.Execute(
                            @"INSERT INTO dbo.tblInventoryRequestDetails
                              (RequestId, RequestType, ItemId, ItemName, PlannedQty, CurrentStock, IssuedQty,
                               UnitId, ProjectBalance, Purpose, ProductType, CreatedBy, CreatedOn, BranchId, Status)
                              VALUES (@RequestId, @RequestType, @ItemId, @ItemName, @PlannedQty, @CurrentStock, @IssuedQty,
                                      @UnitId, @ProjectBalance, @Purpose, @ProductType, @CreatedBy, GETDATE(), @BranchId, 1)",
                            detail, transaction);
                    }
                    transaction.Commit();
                    return requestId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<DmnInventoryRequestMaster> SearchRequests(string search)
        {
            using (IDbConnection connection = DAL.DapperRepository<DmnInventoryRequestMaster>.OpenConnection())
                return connection.Query<DmnInventoryRequestMaster>(
                    @"SELECT RequestId, RequestNo, RequestDate, RequiredDate, DepartmentId, EmployeeId, Remarks, RequestType
                      FROM dbo.tblInventoryRequest
                      WHERE (@Search = '' OR RequestNo LIKE '%' + @Search + '%')
                      ORDER BY RequestId DESC",
                    new { Search = search ?? string.Empty }).ToList();
        }
    }
}