using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllFoodCategory
    {
        public DmnFoodCategory AddData(string procedureName, DmnFoodCategory objTestClass)
        {
            DAL.DapperRepository<DmnFoodCategory> objDapperRepository = new DAL.DapperRepository<DmnFoodCategory>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Fcid", objTestClass.Fcid);
            parms.Add("CategoryName", objTestClass.CategoryName); 
            parms.Add("Description", objTestClass.Description);
            parms.Add("BranchId", objTestClass.BranchId); ;
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}