using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllAccountGroup
    {
        public bool AddData(string procedureName, DmnAccountGroup objTestClass)
        {
            DAL.DapperRepository<DmnAccountGroup> objDapperRepository = new DAL.DapperRepository<DmnAccountGroup>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("AccountGroupName", objTestClass.AccountGroupName);
            parms.Add("AccountGroupCode", objTestClass.AccountGroupCode);
            parms.Add("SrlNoStart", objTestClass.SrlNoStart);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnAccountGroup getDataById(string procedureName, DmnAccountGroup objTestClass)
        {
            DAL.DapperRepository<DmnAccountGroup> objDapperRepository = new DAL.DapperRepository<DmnAccountGroup>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("AccountGroupId", objTestClass.AccountGroupId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnAccountGroup objTestClass)
        {
            DAL.DapperRepository<DmnAccountGroup> objDapperRepository = new DAL.DapperRepository<DmnAccountGroup>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("AccountGroupId", objTestClass.AccountGroupId);
            parms.Add("AccountGroupName", objTestClass.AccountGroupName);
            parms.Add("AccountGroupCode", objTestClass.AccountGroupCode);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnAccountGroup objTestClass)
        {
            DAL.DapperRepository<DmnAccountGroup> objDapperRepository = new DAL.DapperRepository<DmnAccountGroup>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("AccountGroupId", objTestClass.AccountGroupId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
        public List<DmnAccountGroup> getDataAll(string procedureName, DmnAccountGroup objTestClass)
        {
            DAL.DapperRepository<DmnAccountGroup> objDapperRepository = new DAL.DapperRepository<DmnAccountGroup>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.SelectQuery(procedureName, parms);
            return lstData;

        }
        public bool UpdateSrl(string procedureName, DmnAccountGroup objTestClass)
        {
            DAL.DapperRepository<DmnAccountGroup> objDapperRepository = new DAL.DapperRepository<DmnAccountGroup>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("AccountGroupId", objTestClass.AccountGroupId);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }

    }
}
