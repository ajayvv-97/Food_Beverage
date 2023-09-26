using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllCategory
    {
        public bool AddData(string procedureName, DmnCategory objTestClass)
        {
            DAL.DapperRepository<DmnCategory> objDapperRepository = new DAL.DapperRepository<DmnCategory>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CategoryName", objTestClass.CategoryName);
            parms.Add("CategoryCode", objTestClass.CategoryCode);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CategorySrlNoStart", objTestClass.CategorySrlNoStart);
            parms.Add("CategorySrlNoCurrent", objTestClass.CategorySrlNoCurrent);
            parms.Add("CreatedBy", objTestClass.CreatedBy);           
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnCategory getDataById(string procedureName, DmnCategory objTestClass)
        {
            DAL.DapperRepository<DmnCategory> objDapperRepository = new DAL.DapperRepository<DmnCategory>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CategoryId", objTestClass.CategoryId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnCategory objTestClass)
        {
            DAL.DapperRepository<DmnCategory> objDapperRepository = new DAL.DapperRepository<DmnCategory>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CategoryId", objTestClass.CategoryId);
            parms.Add("CategoryName", objTestClass.CategoryName);
            parms.Add("CategoryCode", objTestClass.CategoryCode);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnCategory objTestClass)
        {
            DAL.DapperRepository<DmnCategory> objDapperRepository = new DAL.DapperRepository<DmnCategory>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CategoryId", objTestClass.CategoryId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
        public bool UpdateSrl(string procedureName, DmnCategory objTestClass)
        {
            DAL.DapperRepository<DmnCategory> objDapperRepository = new DAL.DapperRepository<DmnCategory>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CategoryId", objTestClass.CategoryId);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }

    }
}
