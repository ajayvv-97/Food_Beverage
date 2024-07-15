using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllCombinedOrderBill
    {
        public DmnCombinedOrderBill AddCombinedOrderBill(string procedureName, DmnCombinedOrderBill obj)
        {
            DAL.DapperRepository<DmnCombinedOrderBill> dapperRepository = new DAL.DapperRepository<DmnCombinedOrderBill>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Cbid", obj.Cbid);
            parameters.Add("OrderMasterID", obj.OrderMasterID);
            parameters.Add("Tid", obj.Tid);
            parameters.Add("SalesMasterId", obj.SalemasterId);
            parameters.Add("BranchId", obj.BranchId);
            parameters.Add("CreatedBy", obj.CreatedBy);
            parameters.Add("Status", obj.Status); 
            parameters.Add("TimeDuration", obj.TimeDuration);
            parameters.Add("BillCopyStatus", obj.BillCopyStatus);
            var lstData = dapperRepository.AddandReturnID(procedureName, parameters);
            return lstData;
        }

    }
}