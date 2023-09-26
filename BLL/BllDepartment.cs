using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllDepartment
    {
        public bool AddData(string procedureName, DmnDepartment objTestClass)
        {
            DAL.DapperRepository<DmnDepartment> objDapperRepository = new DAL.DapperRepository<DmnDepartment>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("DepartmentName", objTestClass.DepartmentName);
            parms.Add("DepartmentCode", objTestClass.DepartmentCode);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("EmployeeSrlNo", objTestClass.EmployeeSrlNoStart);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnDepartment getDataById(string procedureName, DmnDepartment objTestClass)
        {
            DAL.DapperRepository<DmnDepartment> objDapperRepository = new DAL.DapperRepository<DmnDepartment>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("DepartmentId", objTestClass.DepartmentId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnDepartment objTestClass)
        {
            DAL.DapperRepository<DmnDepartment> objDapperRepository = new DAL.DapperRepository<DmnDepartment>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("DepartmentId", objTestClass.DepartmentId);
            parms.Add("DepartmentName", objTestClass.DepartmentName);
            parms.Add("DepartmentCode", objTestClass.DepartmentCode);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnDepartment objTestClass)
        {
            DAL.DapperRepository<DmnDepartment> objDapperRepository = new DAL.DapperRepository<DmnDepartment>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("DepartmentId", objTestClass.DepartmentId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
        public List<DmnDepartment> getDataAll(string procedureName, DmnDepartment objTestClass)
        {
            DAL.DapperRepository<DmnDepartment> objDapperRepository = new DAL.DapperRepository<DmnDepartment>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.SelectQuery(procedureName, parms);
            return lstData;

        }
        public bool UpdateSrl(string procedureName, DmnDepartment objTestClass)
        {
            DAL.DapperRepository<DmnDepartment> dapperRepository = new DAL.DapperRepository<DmnDepartment>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("DepartmentId", objTestClass.DepartmentId);
            var lstData = dapperRepository.Update(procedureName, parms);
            return lstData;
        }

    }
}
