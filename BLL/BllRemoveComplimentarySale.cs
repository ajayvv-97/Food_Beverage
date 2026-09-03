using Dapper;
using Domain;
using System;

namespace BLL
{
    public class BllRemoveComplimentarySale
    {
        public DmnRemoveComplimentarySale RemoveComplimentarySale(string procedureName, DmnRemoveComplimentarySale obj)
        {
            DAL.DapperRepository<DmnRemoveComplimentarySale> dapperRepository = new DAL.DapperRepository<DmnRemoveComplimentarySale>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@FromDate", obj.FromDate);
            parms.Add("@ToDate", obj.ToDate);
            parms.Add("@BranchId", obj.BranchId);
            parms.Add("@CreatedBy", obj.CreatedBy);
            parms.Add("@PaymentMode", obj.PaymentMode);            
            var result = dapperRepository.AddandReturnID(procedureName, parms);
            return result;
        }
    }
}