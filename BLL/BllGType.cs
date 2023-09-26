using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllGType
    {
        public bool AddData(string procedureName, DmnGType objTestClass)
        {
            DAL.DapperRepository<DmnGType> objDapperRepository = new DAL.DapperRepository<DmnGType>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("GType", objTestClass.GType);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnGType getDataById(string procedureName, DmnGType objTestClass)
        {
            DAL.DapperRepository<DmnGType> objDapperRepository = new DAL.DapperRepository<DmnGType>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("GTypeId", objTestClass.GTypeId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnGType objTestClass)
        {
            DAL.DapperRepository<DmnGType> objDapperRepository = new DAL.DapperRepository<DmnGType>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("GTypeId", objTestClass.GTypeId);
            parms.Add("GType", objTestClass.GType);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnGType objTestClass)
        {
            DAL.DapperRepository<DmnGType> objDapperRepository = new DAL.DapperRepository<DmnGType>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("GTypeId", objTestClass.GTypeId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }

    }
}
