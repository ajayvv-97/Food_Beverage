
using DAL;
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllKitchenorder 
    {
        public Dmnkitchen Kitchen_statusupdate(string procedureName, Dmnkitchen objTestClass)
        {
            DAL.DapperRepository<Dmnkitchen> objDapperRepository = new DAL.DapperRepository<Dmnkitchen>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("OrderNo", objTestClass.OrderNo);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("KOTPrintStatus", objTestClass.KOTPrintStatus);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnOrderCancel Cancel_statusupdate(string procedureName, DmnOrderCancel objTestClass)
        {
            DAL.DapperRepository<DmnOrderCancel> objDapperRepository = new DAL.DapperRepository<DmnOrderCancel>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesMasterId", objTestClass.SalesMasterId);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnKitchenaddonremove Kitchenremove_addon(string procedureName, DmnKitchenaddonremove objTestClass)
        {
            DAL.DapperRepository<DmnKitchenaddonremove> objDapperRepository = new DAL.DapperRepository<DmnKitchenaddonremove>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("OrderDetailsId", objTestClass.OrderDetailsId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("koid", objTestClass.koid);
            parms.Add("Status", objTestClass.Status);
            parms.Add("AVid", objTestClass.AVid);
            parms.Add("Update_AVid", objTestClass.Update_AVid);

            parms.Add("Amount", objTestClass.Amount);
            parms.Add("DiscountAmount", objTestClass.DiscountAmount);
            parms.Add("IGSTAmount", objTestClass.IGSTAmount);
            parms.Add("CGSTAmount", objTestClass.CGSTAmount);
            parms.Add("SGSTAmount", objTestClass.SGSTAmount);
            parms.Add("VATAmount", objTestClass.VATAmount);
            parms.Add("TaxAmount", objTestClass.TaxAmount);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("Srate", objTestClass.Srate);
            parms.Add("MarketPrice", objTestClass.MarketPrice);
            parms.Add("OrderNo", objTestClass.OrderNo);

            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
