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
            parms.Add("Quantity", objTestClass.Quantity);
            parms.Add("ProductType", objTestClass.ProductType);
            parms.Add("PreparationMode", objTestClass.PreparationMode);
            parms.Add("ResetStatus", objTestClass.ResetStatus);
            parms.Add("defaultqty", objTestClass.defaultqty);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnMenu SetMenuActiveStatus(string procedureName, DmnMenu obj)
        {
            DAL.DapperRepository<DmnMenu> repo =
                new DAL.DapperRepository<DmnMenu>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("MId", obj.MId);
            parms.Add("BranchId", obj.BranchId);
            parms.Add("CreatedBy", obj.CreatedBy);
            parms.Add("Status", obj.Status);
            parms.Add("ProductType", obj.ProductType);

            var lstData = repo.AddandReturnID(procedureName, parms);
            return lstData;
        }

    }
}