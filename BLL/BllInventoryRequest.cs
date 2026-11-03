using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BLL
{
    #region Inventory Request Master

    public DmnInventoryRequestMaster AddMasterData(
        string procedureName,
        DmnInventoryRequestMaster objMaster)
    {
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


    public DmnInventoryRequestMaster AddDraftMasterData(
        string procedureName,
        DmnInventoryRequestMaster objMaster)
    {
        DAL.DapperRepository<DmnInventoryRequestMaster> repository =
            new DAL.DapperRepository<DmnInventoryRequestMaster>();

        DynamicParameters parameters = new DynamicParameters();

        parameters.Add("DraftId", objMaster.DraftId);
        parameters.Add("DraftNo", objMaster.DraftNo);
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

    #endregion


    #region Inventory Request Details

    public DmnInventoryRequestDetail AddDetailsData(
        string procedureName,
        DmnInventoryRequestDetail objDetails)
    {
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

    #endregion


    #region Inventory Request Draft Details

    public DmnInventoryRequestDetail AddDraftDetailsData(
        string procedureName,
        DmnInventoryRequestDetail objDetails)
    {
        DAL.DapperRepository<DmnInventoryRequestDetail> repository =
            new DAL.DapperRepository<DmnInventoryRequestDetail>();

        DynamicParameters parameters = new DynamicParameters();

        parameters.Add("DraftDetailId", objDetails.DraftDetailId);
        parameters.Add("DraftId", objDetails.DraftId);
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

    #endregion


    #region Inventory Request Item Details

    public DmnInventoryRequestItem AddDetailsData(
        string procedureName,
        DmnInventoryRequestItem objDetails)
    {
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

    #endregion
}