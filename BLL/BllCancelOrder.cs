using Dapper;
using Domain;
using System;

namespace BLL
{
    public class BllCancelOrder
    {
        public DmnCancelOrder CancelOrder(string procedureName, DmnCancelOrder obj)
        {
            DAL.DapperRepository<DmnCancelOrder> dapperRepository = new DAL.DapperRepository<DmnCancelOrder>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@SalesMasterId", obj.SalesMasterId);
            parms.Add("@OrderMasterId", obj.OrderMasterid);
            parms.Add("@BranchId", obj.BranchId);
            parms.Add("@CreatedBy", obj.CreatedBy);
            parms.Add("@CancelReason", obj.CancelReason);  
            var result = dapperRepository.AddandReturnID(procedureName, parms);
            return result;
        }
    }
}
