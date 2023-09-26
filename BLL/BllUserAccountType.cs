using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllUserAccountType
    {
        public bool AddData(string procedureName, DmnUserAccountType objTestClass)
        {
            DAL.DapperRepository<DmnUserAccountType> objDapperRepository = new DAL.DapperRepository<DmnUserAccountType>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("UserAccountType", objTestClass.UserAccountType);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            //parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnUserAccountType getDataById(string procedureName, DmnUserAccountType objTestClass)
        {
            DAL.DapperRepository<DmnUserAccountType> objDapperRepository = new DAL.DapperRepository<DmnUserAccountType>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("UserAccountTypeId", objTestClass.UserAccountTypeId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnUserAccountType objTestClass)
        {
            DAL.DapperRepository<DmnUserAccountType> objDapperRepository = new DAL.DapperRepository<DmnUserAccountType>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("UserAccountTypeId", objTestClass.UserAccountTypeId);
            parms.Add("UserAccountType", objTestClass.UserAccountType);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnUserAccountType objTestClass)
        {
            DAL.DapperRepository<DmnUserAccountType> objDapperRepository = new DAL.DapperRepository<DmnUserAccountType>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("UserAccountTypeId", objTestClass.UserAccountTypeId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
    }
}
