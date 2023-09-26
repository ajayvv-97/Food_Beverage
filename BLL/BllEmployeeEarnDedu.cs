using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllEmployeeEarnDedu
    {
        public bool AddData(string procedureName, DmnEmployeeEarnDedu objTestClass)
        {
            DAL.DapperRepository<DmnEmployeeEarnDedu> objDapperRepository = new DAL.DapperRepository<DmnEmployeeEarnDedu>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("EmployeeId", objTestClass.EmployeeId);
            parms.Add("EarnTypeId", objTestClass.EarnTypeId);
            parms.Add("EarnedAmount", objTestClass.EarnedAmount);
            parms.Add("EarnedDescription", objTestClass.EarnedDescription);
            parms.Add("DeductedTypeId", objTestClass.DeductedTypeId);
            parms.Add("DeductedAmount", objTestClass.DeductedAmount);
            parms.Add("DeductedDescription", objTestClass.DeductedDescription);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnEmployeeEarnDedu getDataById(string procedureName, DmnEmployeeEarnDedu objTestClass)
        {
            DAL.DapperRepository<DmnEmployeeEarnDedu> objDapperRepository = new DAL.DapperRepository<DmnEmployeeEarnDedu>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("EarnTypeId", objTestClass.EarnTypeId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnEmployeeEarnDedu objTestClass)
        {
            DAL.DapperRepository<DmnEmployeeEarnDedu> objDapperRepository = new DAL.DapperRepository<DmnEmployeeEarnDedu>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("EmployeeEarnedId", objTestClass.EmployeeEarnedId);
            parms.Add("EmployeeId", objTestClass.EmployeeId);
            parms.Add("EarnTypeId", objTestClass.EarnTypeId);
            parms.Add("EarnedAmount", objTestClass.EarnedAmount);
            parms.Add("EarnedDescription", objTestClass.EarnedDescription);
            parms.Add("DeductedTypeId", objTestClass.DeductedTypeId);
            parms.Add("DeductedAmount", objTestClass.DeductedAmount);
            parms.Add("DeductedDescription", objTestClass.DeductedDescription);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnEmployeeEarnDedu objTestClass)
        {
            DAL.DapperRepository<DmnEmployeeEarnDedu> objDapperRepository = new DAL.DapperRepository<DmnEmployeeEarnDedu>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("EmployeeEarnedId", objTestClass.EmployeeEarnedId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }

    }
}
