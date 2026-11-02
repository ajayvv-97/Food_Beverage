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

            DAL.DapperRepository<DmnInventoryRequestMaster> repository =  new DAL.DapperRepository<DmnInventoryRequestMaster>();

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

            DAL.DapperRepository<DmnInventoryRequestItem> repository = new DAL.DapperRepository<DmnInventoryRequestItem>();
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


        public DmnInventoryRequestDetail AddDetailsData(
            string procedureName, DmnInventoryRequestDetail objDetails)
        {
            if (objDetails == null)
            {
                throw new ArgumentNullException(nameof(objDetails));
            }

            DAL.DapperRepository<DmnInventoryRequestDetail> repository =
                new DAL.DapperRepository<DmnInventoryRequestDetail>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("InventoryRequestDetailId", objDetails.InventoryRequestDetailId);
            parameters.Add("InventoryRequestId", objDetails.InventoryRequestId);
            parameters.Add("RequestType", objDetails.RequestType);
            parameters.Add("SlNo", objDetails.SlNo);
            parameters.Add("ProductId", objDetails.ProductId);
            parameters.Add("PlannedQty", objDetails.PlannedQty);
            parameters.Add("RecipeItemId", objDetails.RecipeItemId);
            parameters.Add("PerPortionQty", objDetails.PerPortionQty);
            parameters.Add("TotalRequiredQty", objDetails.TotalRequiredQty);
            parameters.Add("ItemId", objDetails.ItemId);
            parameters.Add("ProductType", objDetails.ProductType);
            parameters.Add("CurrentStock", objDetails.CurrentStock);
            parameters.Add("IssuedQty", objDetails.IssuedQty);
            parameters.Add("ProjectedBalance", objDetails.ProjectedBalance);
            parameters.Add("Unit", objDetails.Unit);
            parameters.Add("PurposeRemarks", objDetails.PurposeRemarks);
            parameters.Add("CreatedBy", objDetails.CreatedBy);
            parameters.Add("CreatedOn", objDetails.CreatedOn);
            parameters.Add("ModifiedBy", objDetails.ModifiedBy);
            parameters.Add("ModifiedOn", objDetails.ModifiedOn);
            parameters.Add("BranchId", objDetails.BranchId);
            parameters.Add("Status", objDetails.Status);

            return repository.AddandReturnID(procedureName, parameters);
        }


        public int SaveInventoryRequest(
            string procedureName,
            DmnInventoryRequestMaster master,
            IEnumerable<DmnInventoryRequestDetail> details,
            int? sourceDraftId)
        {
            if (master == null) throw new ArgumentNullException(nameof(master));

            using (IDbConnection connection =
                DAL.DapperRepository<DmnInventoryRequestDetail>.OpenConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("InventoryRequestId", master.RequestId);
                parameters.Add("RequestNo", master.RequestNo);
                parameters.Add("RequestDate", master.RequestDate);
                parameters.Add("RequiredDate", master.RequiredDate);
                parameters.Add("DepartmentId", master.DepartmentId);
                parameters.Add("EmployeeId", master.EmployeeId);
                parameters.Add("Remarks", master.Remarks);
                parameters.Add("RequestType", master.RequestType);
                parameters.Add("CreatedBy", master.CreatedBy);
                parameters.Add("ModifiedBy", master.ModifiedBy);
                parameters.Add("BranchId", master.BranchId);
                parameters.Add("Status", master.Status);
                parameters.Add("DraftId", sourceDraftId);
                parameters.Add("Details", BuildDetailsTable(details)
                    .AsTableValuedParameter("dbo.TVP_InventoryRequestDetail"));

                return connection.QuerySingle<int>(
                    procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public int SaveInventoryRequestDraft(
            string procedureName,
            int draftId,
            string draftNo,
            DmnInventoryRequestMaster master,
            IEnumerable<DmnInventoryRequestDetail> details)
        {
            if (master == null) throw new ArgumentNullException(nameof(master));

            using (IDbConnection connection =
                DAL.DapperRepository<DmnInventoryRequestDetail>.OpenConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("DraftId", draftId);
                parameters.Add("DraftNo", draftNo);
                parameters.Add("RequestNo", master.RequestNo);
                parameters.Add("RequestDate", master.RequestDate);
                parameters.Add("RequiredDate", master.RequiredDate);
                parameters.Add("DepartmentId", master.DepartmentId);
                parameters.Add("EmployeeId", master.EmployeeId);
                parameters.Add("Remarks", master.Remarks);
                parameters.Add("RequestType", master.RequestType);
                parameters.Add("CreatedBy", master.CreatedBy);
                parameters.Add("BranchId", master.BranchId);
                parameters.Add("Status", master.Status);
                parameters.Add("Details", BuildDetailsTable(details)
                    .AsTableValuedParameter("dbo.TVP_InventoryRequestDetail"));

                return connection.QuerySingle<int>(
                    procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteInventoryRequestDraft(
            string procedureName, int draftId, int employeeId, int branchId)
        {
            using (IDbConnection connection =
                DAL.DapperRepository<DmnInventoryRequestDetail>.OpenConnection())
            {
                connection.Execute(
                    procedureName,
                    new { DraftId = draftId, EmployeeId = employeeId, BranchId = branchId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteInventoryRequestRow(
            string procedureName,
            bool isDraft,
            int? draftId,
            int? requestId,
            int requestType,
            int? productId,
            long? itemId,
            int employeeId,
            int branchId)
        {
            using (IDbConnection connection =
                DAL.DapperRepository<DmnInventoryRequestDetail>.OpenConnection())
            {
                connection.Execute(
                    procedureName,
                    new
                    {
                        IsDraft = isDraft,
                        DraftId = draftId,
                        InventoryRequestId = requestId,
                        RequestType = requestType,
                        ProductId = productId,
                        ItemId = itemId,
                        EmployeeId = employeeId,
                        BranchId = branchId
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        private static DataTable BuildDetailsTable(IEnumerable<DmnInventoryRequestDetail> details)
        {
            DataTable table = new DataTable();
            table.Columns.Add("RequestType", typeof(int));
            table.Columns.Add("SlNo", typeof(int));
            table.Columns.Add("ProductId", typeof(int));
            table.Columns.Add("PlannedQty", typeof(decimal));
            table.Columns.Add("RecipeItemId", typeof(long));
            table.Columns.Add("PerPortionQty", typeof(decimal));
            table.Columns.Add("TotalRequiredQty", typeof(decimal));
            table.Columns.Add("ItemId", typeof(long));
            table.Columns.Add("ProductType", typeof(int));
            table.Columns.Add("CurrentStock", typeof(decimal));
            table.Columns.Add("IssuedQty", typeof(decimal));
            table.Columns.Add("ProjectedBalance", typeof(decimal));
            table.Columns.Add("Unit", typeof(int));
            table.Columns.Add("PurposeRemarks", typeof(string));

            foreach (DmnInventoryRequestDetail detail in details ?? Enumerable.Empty<DmnInventoryRequestDetail>())
            {
                table.Rows.Add(
                    detail.RequestType,
                    (object)detail.SlNo ?? DBNull.Value,
                    (object)detail.ProductId ?? DBNull.Value,
                    (object)detail.PlannedQty ?? DBNull.Value,
                    (object)detail.RecipeItemId ?? DBNull.Value,
                    (object)detail.PerPortionQty ?? DBNull.Value,
                    (object)detail.TotalRequiredQty ?? DBNull.Value,
                    (object)detail.ItemId ?? DBNull.Value,
                    (object)detail.ProductType ?? DBNull.Value,
                    (object)detail.CurrentStock ?? DBNull.Value,
                    (object)detail.IssuedQty ?? DBNull.Value,
                    (object)detail.ProjectedBalance ?? DBNull.Value,
                    (object)detail.Unit ?? DBNull.Value,
                    (object)detail.PurposeRemarks ?? DBNull.Value);
            }

            return table;
        }

    }
}