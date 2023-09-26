using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllDesignation
    {
        public bool AddData(string procedureName, DmnDesignation objTestClass)
        {
            DAL.DapperRepository<DmnDesignation> objDapperRepository = new DAL.DapperRepository<DmnDesignation>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("DepartmentId", objTestClass.DepartmentId);
            parms.Add("DesignationName", objTestClass.DesignationName);
            parms.Add("WorkingHours", objTestClass.WorkingHours);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnDesignation getDataById(string procedureName, DmnDesignation objTestClass)
        {
            DAL.DapperRepository<DmnDesignation> objDapperRepository = new DAL.DapperRepository<DmnDesignation>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("DesignationId", objTestClass.DesignationId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnDesignation objTestClass)
        {
            DAL.DapperRepository<DmnDesignation> objDapperRepository = new DAL.DapperRepository<DmnDesignation>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("DesignationId", objTestClass.DesignationId);
            parms.Add("DepartmentId", objTestClass.DepartmentId);
            parms.Add("DesignationName", objTestClass.DesignationName);
            parms.Add("WorkingHours", objTestClass.WorkingHours);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            //parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnDesignation objTestClass)
        {
            DAL.DapperRepository<DmnDesignation> objDapperRepository = new DAL.DapperRepository<DmnDesignation>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("DesignationId", objTestClass.DesignationId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
    }
}
