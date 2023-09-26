using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllFinancialYear
    {
        public bool AddData(string procedureName, DmnFinancialYear objTestClass)
        {
            DAL.DapperRepository<DmnFinancialYear> objDapperRepository = new DAL.DapperRepository<DmnFinancialYear>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Description", objTestClass.Description);
            parms.Add("FromDate", objTestClass.FromDate);
            parms.Add("ToDate", objTestClass.ToDate);
            parms.Add("Remarks", objTestClass.Remarks);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnFinancialYear getDataById(string procedureName, DmnFinancialYear objTestClass)
        {
            DAL.DapperRepository<DmnFinancialYear> objDapperRepository = new DAL.DapperRepository<DmnFinancialYear>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("FinancialYearId", objTestClass.FinancialYearId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnFinancialYear objTestClass)
        {
            DAL.DapperRepository<DmnFinancialYear> objDapperRepository = new DAL.DapperRepository<DmnFinancialYear>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("FinancialYearId", objTestClass.FinancialYearId);
            parms.Add("Description", objTestClass.Description);
            parms.Add("FromDate", objTestClass.FromDate);
            parms.Add("ToDate", objTestClass.ToDate);
            parms.Add("Remarks", objTestClass.Remarks);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            // parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
             parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnFinancialYear objTestClass)
        {
            DAL.DapperRepository<DmnFinancialYear> objDapperRepository = new DAL.DapperRepository<DmnFinancialYear>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("FinancialYearId", objTestClass.FinancialYearId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            // parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);


            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }

    }
}
