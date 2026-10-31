using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Bllincrediant
    {
        public Dmnincrediant AddData(string procedureName, Dmnincrediant objTestClass)
        {
            DAL.DapperRepository<Dmnincrediant> objDapperRepository = new DAL.DapperRepository<Dmnincrediant>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Ing_DetailsId", objTestClass.Ing_DetailsId);
            parms.Add("ItemId", objTestClass.ItemId); 
            parms.Add("quantity", objTestClass.quantity);
            parms.Add("unit", objTestClass.unit);
            parms.Add("Instructions", objTestClass.Instructions); 
            parms.Add("Recipe", objTestClass.Recipe);
            parms.Add("ProductId", objTestClass.ProductId);
            parms.Add("BranchId", objTestClass.BranchId); 
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            parms.Add("Deductionstage", objTestClass.Deductionstage);
            parms.Add("ProductType", objTestClass.ProductType);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}