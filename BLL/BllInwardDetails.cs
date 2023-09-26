using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
   public class BllInwardDetails
    {
        public bool AddData(string procedureName, DmnInward objTestClass)
        {
            DAL.DapperRepository<DmnInwardDetails> objDapperRepository = new DAL.DapperRepository<DmnInwardDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("InwardMasterId", objTestClass.InwardMasterId);
            parms.Add("PMDocumentNo", objTestClass.PMDocumentNo);
            parms.Add("ItemId", objTestClass.ItemId);
            parms.Add("ItemCode", objTestClass.ItemCode);
            parms.Add("ItemBarCode", objTestClass.ItemBarCode);
            parms.Add("UnitId", objTestClass.UnitId);
            parms.Add("Quantity", objTestClass.Quantity);
            parms.Add("FreeQuantity", objTestClass.FreeQuantity);
            parms.Add("PurchaseRate", objTestClass.PurchaseRate);
            parms.Add("IRate", objTestClass.IRate);
            parms.Add("Amount", objTestClass.Amount);
            parms.Add("DiscountPer", objTestClass.DiscountPer);
            parms.Add("DiscountAmount", objTestClass.DiscountAmount);
            parms.Add("IGSTAmount", objTestClass.IGSTAmount);
            parms.Add("CGSTAmount", objTestClass.CGSTAmount);
            parms.Add("SGSTAmount", objTestClass.SGSTAmount);
            parms.Add("GrossAmount", objTestClass.GrossAmount);
            parms.Add("CessPer", objTestClass.CessPer);
            parms.Add("CessAmount", objTestClass.CessAmount);
            parms.Add("TaxPer", objTestClass.TaxPer);
            parms.Add("TaxAmount", objTestClass.TaxAmount);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("FinancialYearId", objTestClass.FinancialYearId);
            parms.Add("TaxType", objTestClass.TaxType);
            //parms.Add("TotalSaveAmount", objTestClass.TotalSaveAmount);
            parms.Add("StockItem", objTestClass.StockItem);
            parms.Add("QuantitytoSubtract", objTestClass.QuantitytoSubtract);
            parms.Add("Cost", objTestClass.Cost);
            parms.Add("Margin", objTestClass.Margin);
            parms.Add("MRP", objTestClass.MRP);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public bool AddPurchaseMiscCharges(string procedureName, DmnInward objTestClass)
        {
            DAL.DapperRepository<DmnInward> objDapperRepository = new DAL.DapperRepository<DmnInward>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("InwardMasterId", objTestClass.InwardMasterId);
            parms.Add("GeneralId", objTestClass.GeneralId);
            parms.Add("MiscAmount", objTestClass.MiscAmount);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public bool DeleteByMasterId(string procedureName, DmnInward objTestClass)
        {
            DAL.DapperRepository<DmnInward> objDapperRepository = new DAL.DapperRepository<DmnInward>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("InwardMasterId", objTestClass.InwardMasterId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
        public bool DeleteMiscCharges(string procedureName, DmnInward objTestClass)
        {
            DAL.DapperRepository<DmnInward> objDapperRepository = new DAL.DapperRepository<DmnInward>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("InwardMasterId", objTestClass.InwardMasterId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }

    }
}

