using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllMenu
    {
        public DmnMenu AddData(string procedureName, DmnMenu objTestClass)
        {
            DAL.DapperRepository<DmnMenu> objDapperRepository = new DAL.DapperRepository<DmnMenu>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("MId", objTestClass.MId);
            parms.Add("Title", objTestClass.Title);
            parms.Add("CategoryId", objTestClass.CategoryId);
            parms.Add("Description", objTestClass.Description);
            parms.Add("BranchId", objTestClass.BranchId); ;
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            parms.Add("Active", objTestClass.Aviable_status);
            parms.Add("Recipe", objTestClass.Recipe_Des);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}