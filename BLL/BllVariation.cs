using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllVariation
    {
        public DmnAddonVariation AddData(string procedureName, DmnAddonVariation objTestClass)
        {
            DAL.DapperRepository<DmnAddonVariation> objDapperRepository = new DAL.DapperRepository<DmnAddonVariation>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Pavid", objTestClass.Pavid);
            parms.Add("Caption", objTestClass.Caption);
            parms.Add("Price", objTestClass.Price);
            parms.Add("VariationCode", objTestClass.VariationCode);
            parms.Add("ConsumedQty", objTestClass.ConsumedQty);
            parms.Add("Type", objTestClass.Type);
            parms.Add("Aid", objTestClass.Aid);
            parms.Add("ProductId", objTestClass.ProductId); ;
            parms.Add("ItemCode_UPC", objTestClass.ItemCode_UPC);
            parms.Add("BranchId", objTestClass.BranchId); ;
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnAddonVariation Delete_Variation_Data(string procedureName, DmnAddonVariation objTestClass)
        {
            DAL.DapperRepository<DmnAddonVariation> objDapperRepository = new DAL.DapperRepository<DmnAddonVariation>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Pavid", objTestClass.Pavid);
            parms.Add("BranchId", objTestClass.BranchId); ;
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}