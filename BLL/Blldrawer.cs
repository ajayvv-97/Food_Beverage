using System;
using System.Collections.Generic;
using System.Text;

using Dapper;
using Domain;

namespace BLL
{
    public class Blldrawer
    {
        public Dmndrawer AddEditDatadrawer(string procedureName, Dmndrawer objTestClass)
        {
            DAL.DapperRepository<Dmndrawer> dapperRepository = new DAL.DapperRepository<Dmndrawer>();
        DynamicParameters parms = new DynamicParameters();
            parms.Add("Id", objTestClass.Id);
            parms.Add("DName", objTestClass.DName);
            parms.Add("InitilAmount", objTestClass.InitilAmount);
            parms.Add("LoanAmount", objTestClass.LoanAmount);
            parms.Add("Drops", objTestClass.Drops);
            parms.Add("Status", objTestClass.Status);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("EmployeeId", objTestClass.EmployeeId);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
