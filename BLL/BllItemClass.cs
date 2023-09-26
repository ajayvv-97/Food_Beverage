using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllItemClass
    {
        public bool AddData(string procedureName, DmnItemClass objTestClass)
        {
            DAL.DapperRepository<DmnItemClass> objDapperRepository = new DAL.DapperRepository<DmnItemClass>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("ItemClassName", objTestClass.ItemClassName);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnItemClass getDataById(string procedureName, DmnItemClass objTestClass)
        {
            DAL.DapperRepository<DmnItemClass> objDapperRepository = new DAL.DapperRepository<DmnItemClass>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("ItemClassId", objTestClass.ItemClassId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnItemClass objTestClass)
        {
            DAL.DapperRepository<DmnItemClass> objDapperRepository = new DAL.DapperRepository<DmnItemClass>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("ItemClassId", objTestClass.ItemClassId);
            parms.Add("ItemClassName", objTestClass.ItemClassName);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnItemClass objTestClass)
        {
            DAL.DapperRepository<DmnItemClass> objDapperRepository = new DAL.DapperRepository<DmnItemClass>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("ItemClassId", objTestClass.ItemClassId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }

    }
}
