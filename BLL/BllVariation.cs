using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllVariation
    {
        public DmnVariation AddData(string procedureName, DmnVariation objTestClass)
        {
            DAL.DapperRepository<DmnVariation> objDapperRepository = new DAL.DapperRepository<DmnVariation>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Vid", objTestClass.Vid);
            parms.Add("VariationType", objTestClass.VariationType);
            parms.Add("Price", objTestClass.Price);
            parms.Add("VariationCode", objTestClass.VariationCode);
            parms.Add("ProductId", objTestClass.ProductId); ;
            parms.Add("ItemCode_UPC", objTestClass.ItemCode_UPC); 
            parms.Add("BranchId", objTestClass.BranchId); ;
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}