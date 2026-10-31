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

        public int Update(DmnInventoryRequestMaster master, IList<DmnInventoryRequestItem> details)
        {
            if (master == null)
            {
                throw new ArgumentNullException(nameof(master));
            }

            if (master.RequestId <= 0)
            {
                throw new ArgumentException("A valid request ID is required to update an inventory request.", nameof(master));
            }

            if (details == null || details.Count == 0)
            {
                throw new InvalidOperationException("Add at least one batch product or raw material before updating the request.");
            }

            using (IDbConnection connection = DAL.DapperRepository<DmnInventoryRequestMaster>.OpenConnection())
            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    connection.Execute(
                        @"UPDATE dbo.tblInventoryRequestMaster
                          SET RequestNo = @RequestNo,
                              RequestDate = @RequestDate,
                              RequiredDate = @RequiredDate,
                              DepartmentId = @DepartmentId,
                              EmployeeId = @EmployeeId,
                              Remarks = @Remarks,
                              RequestType = @RequestType,
                              ModifiedBy = @ModifiedBy,
                              ModifiedOn = GETDATE(),
                              BranchId = @BranchId,
                              Status = @Status
                          WHERE InventoryRequestId = @RequestId",
                        master, transaction);

                    connection.Execute(
                        "DELETE FROM dbo.tblInventoryRequestDetails WHERE InventoryRequestId = @RequestId",
                        new { master.RequestId }, transaction);

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
                               @ProjectedBalance, @Unit, @PurposeRemarks, @CreatedBy, GETDATE(), @BranchId, @Status)",
                            new
                            {
                                InventoryRequestId = master.RequestId,
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
                                detail.BranchId,
                                detail.Status
                            }, transaction);
                    }

                    transaction.Commit();
                    return master.RequestId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

    }
}