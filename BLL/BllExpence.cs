using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllExpence
    {
        public Dmnexpence AddEditDataExpence(string procedureName, Dmnexpence objTestClass)
        {
            DAL.DapperRepository<Dmnexpence> dapperRepository = new DAL.DapperRepository<Dmnexpence>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Eid", objTestClass.Eid);
            parms.Add("reason", objTestClass.reason);
            parms.Add("Amount", objTestClass.Amount);
            parms.Add("description", objTestClass.description);
            parms.Add("Empolyeeid", objTestClass.Empolyeeid);
            parms.Add("paymentid", objTestClass.paymentid);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
