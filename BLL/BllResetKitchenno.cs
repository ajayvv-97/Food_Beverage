using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllResetKitchenno 
    {
        public DmnResetKitchenno Resetsystem(string procedureName, DmnResetKitchenno objTestClass)
        {
            DAL.DapperRepository<DmnResetKitchenno> objDapperRepository = new DAL.DapperRepository<DmnResetKitchenno>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("EmployeeId", objTestClass.EmployeeId);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}