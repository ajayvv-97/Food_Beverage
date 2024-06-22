
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllItem
    {
        public DmnItem AddData(string procedureName, DmnItem objTestClass)
        {
            DAL.DapperRepository<DmnItem> objDapperRepository = new DAL.DapperRepository<DmnItem>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("ItemId", objTestClass.ItemId);
            parms.Add("Itemname", objTestClass.Itemname);
            parms.Add("Fcid", objTestClass.Fcid);
            parms.Add("MRP", objTestClass.MRP);
            parms.Add("Prate", objTestClass.Prate);
            parms.Add("Srate", objTestClass.Srate);
            parms.Add("quantity", objTestClass.quantity);
            parms.Add("unit", objTestClass.unit);
            parms.Add("BranchId", objTestClass.BranchId); ;
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            parms.Add("summary", objTestClass.summary);
            parms.Add("TaxSettingsId", objTestClass.TaxSettingsId);
            parms.Add("Tax", objTestClass.Tax);
            parms.Add("TaxType", objTestClass.TaxType);
            parms.Add("ItemCode_UPC", objTestClass.ItemCode_UPC);
            parms.Add("Saletype", objTestClass.Saletype);
            parms.Add("PrId", objTestClass.PrId);
            parms.Add("DiscountAmt", objTestClass.DiscountAmt);
            parms.Add("DiscountPer", objTestClass.DiscountPer); 

              var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}