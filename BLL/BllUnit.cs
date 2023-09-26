using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllUnit
    {
        public bool AddData(string procedureName, DmnUnit objTestClass)
        {
            DAL.DapperRepository<DmnUnit> objDapperRepository = new DAL.DapperRepository<DmnUnit>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("UnitName", objTestClass.UnitName);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnUnit getDataById(string procedureName, DmnUnit objTestClass)
        {
            DAL.DapperRepository<DmnUnit> objDapperRepository = new DAL.DapperRepository<DmnUnit>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("UnitId", objTestClass.UnitId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnUnit objTestClass)
        {
            DAL.DapperRepository<DmnUnit> objDapperRepository = new DAL.DapperRepository<DmnUnit>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("UnitId", objTestClass.UnitId);
            parms.Add("UnitName", objTestClass.UnitName);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnUnit objTestClass)
        {
            DAL.DapperRepository<DmnUnit> objDapperRepository = new DAL.DapperRepository<DmnUnit>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("UnitId", objTestClass.UnitId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }

    }
}
