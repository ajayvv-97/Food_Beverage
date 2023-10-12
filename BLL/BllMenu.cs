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
            parms.Add("Name", objTestClass.Name);
            parms.Add("Fcid", objTestClass.Fcid);
            parms.Add("ProductId", objTestClass.ProductId);
            parms.Add("Description", objTestClass.Description);
            parms.Add("BranchId", objTestClass.BranchId); ;
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}