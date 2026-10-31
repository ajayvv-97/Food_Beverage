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

        public DmnInventoryRequestMaster AddMasterData(string procedureName, DmnInventoryRequestMaster objMaster)
        {
            if (objMaster == null)
            {
                throw new ArgumentNullException(nameof(objMaster));
            }

            DAL.DapperRepository<DmnInventoryRequestMaster> repository =
                new DAL.DapperRepository<DmnInventoryRequestMaster>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("InventoryRequestId", objMaster.RequestId);
            parameters.Add("RequestNo", objMaster.RequestNo);
            parameters.Add("RequestDate", objMaster.RequestDate);
            parameters.Add("RequiredDate", objMaster.RequiredDate);
            parameters.Add("DepartmentId", objMaster.DepartmentId);
            parameters.Add("EmployeeId", objMaster.EmployeeId);
            parameters.Add("Remarks", objMaster.Remarks);
            parameters.Add("RequestType", objMaster.RequestType);
            parameters.Add("CreatedBy", objMaster.CreatedBy);
            parameters.Add("CreatedOn", objMaster.CreatedOn);
            parameters.Add("ModifiedBy", objMaster.ModifiedBy);
            parameters.Add("ModifiedOn", objMaster.ModifiedOn);
            parameters.Add("BranchId", objMaster.BranchId);
            parameters.Add("Status", objMaster.Status);

            return repository.AddandReturnID(procedureName, parameters);
        }

        public DmnInventoryRequestItem AddDetailsData(string procedureName, DmnInventoryRequestItem objDetails)
        {
            if (objDetails == null)
            {
                throw new ArgumentNullException(nameof(objDetails));
            }

            DAL.DapperRepository<DmnInventoryRequestItem> repository =
                new DAL.DapperRepository<DmnInventoryRequestItem>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("RequestDetailId", objDetails.RequestDetailId);
            parameters.Add("RequestId", objDetails.RequestId);
            parameters.Add("RequestType", objDetails.RequestType);
            parameters.Add("ItemId", objDetails.ItemId);
            parameters.Add("ItemName", objDetails.ItemName);
            parameters.Add("PlannedQty", objDetails.PlannedQty);
            parameters.Add("CurrentStock", objDetails.CurrentStock);
            parameters.Add("IssuedQty", objDetails.IssuedQty);
            parameters.Add("UnitId", objDetails.UnitId);
            parameters.Add("ProjectBalance", objDetails.ProjectBalance);
            parameters.Add("Purpose", objDetails.Purpose);
            parameters.Add("ProductType", objDetails.ProductType);
            parameters.Add("CreatedBy", objDetails.CreatedBy);
            parameters.Add("CreatedOn", objDetails.CreatedOn);
            parameters.Add("ModifiedBy", objDetails.ModifiedBy);
            parameters.Add("ModifiedOn", objDetails.ModifiedOn);
            parameters.Add("BranchId", objDetails.BranchId);
            parameters.Add("Status", objDetails.Status);
            parameters.Add("UnitPrice", objDetails.UnitPrice);

            return repository.AddandReturnID(procedureName, parameters);
        }

        public int Save(DmnInventoryRequestMaster master, IList<DmnInventoryRequestItem> details)
        {
            if (master == null)
            {
                throw new ArgumentNullException(nameof(master));
            }

            if (details == null || details.Count == 0)
            {
                throw new InvalidOperationException("Add at least one batch product or raw material before saving.");
            }

            using (IDbConnection connection = DAL.DapperRepository<DmnInventoryRequestMaster>.OpenConnection())
            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    int requestId = connection.Query<int>(
                        @"INSERT INTO dbo.tblInventoryRequestMaster
                          (RequestNo, RequestDate, RequiredDate, DepartmentId, EmployeeId, Remarks, RequestType,
                           CreatedBy, CreatedOn, BranchId, Status)
                          VALUES (@RequestNo, @RequestDate, @RequiredDate, @DepartmentId, @EmployeeId, @Remarks, @RequestType,
                                  @CreatedBy, GETDATE(), @BranchId, 1);
                          SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        master, transaction).Single();

                    foreach (DmnInventoryRequestItem detail in details)
                    {
                        connection.Execute(
                            @"INSERT INTO dbo.tblInventoryRequestDetails
                              (InventoryRequestId, RequestType, SlNo, ProductId, PlannedQty, RecipeItemId,
                               PerPortionQty, TotalRequiredQty, ItemId, ProductType, CurrentStock, IssuedQty,
                               ProjectedBalance, Unit, PurposeRemarks, CreatedBy, CreatedOn, BranchId, Status)
                              VALUES
                              (@InventoryRequestId, @RequestType, @SlNo, @ProductId, @PlannedQty, @RecipeItemId,
                               @PerPortionQty, @TotalRequiredQty, @ItemId, @ProductType, @CurrentStock, @IssuedQty,
                               @ProjectedBalance, @Unit, @PurposeRemarks, @CreatedBy, GETDATE(), @BranchId, 1)",
                            new
                            {
                                InventoryRequestId = requestId,
                                detail.RequestType,
                                SlNo = detail.RequestDetailId,
                                ProductId = detail.RequestType == 1 ? (int?)detail.ItemId : null,
                                detail.PlannedQty,
                                RecipeItemId = detail.RequestType == 1 ? (long?)detail.ItemId : null,
                                PerPortionQty = (decimal?)null,
                                TotalRequiredQty = detail.RequestType == 1 ? (decimal?)detail.PlannedQty : null,
                                ItemId = detail.RequestType == 2 ? (long?)detail.ItemId : null,
                                detail.ProductType,
                                detail.CurrentStock,
                                detail.IssuedQty,
                                ProjectedBalance = detail.ProjectBalance,
                                Unit = detail.UnitId,
                                PurposeRemarks = detail.Purpose,
                                detail.CreatedBy,
                                detail.BranchId
                            }, transaction);
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
            {
                return connection.Query<DmnInventoryRequestMaster>(
                    @"SELECT InventoryRequestId AS RequestId, RequestNo, RequestDate, RequiredDate,
                             DepartmentId, EmployeeId, Remarks, RequestType, CreatedBy, CreatedOn,
                             ModifiedBy, ModifiedOn, BranchId, Status
                      FROM dbo.tblInventoryRequestMaster
                      WHERE (@Search = '' OR RequestNo LIKE '%' + @Search + '%')
                      ORDER BY InventoryRequestId DESC",
                    new { Search = search ?? string.Empty }).ToList();
            }
        }
    }
}