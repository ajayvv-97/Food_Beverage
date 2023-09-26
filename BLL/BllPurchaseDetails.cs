using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllPurchaseDetails
    {
        public bool AddData(string procedureName, DmnPurchaseMaster objTestClass)
        {
            DAL.DapperRepository<DmnPurchaseDetails> objDapperRepository = new DAL.DapperRepository<DmnPurchaseDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("PurchaseMasterId", objTestClass.PurchaseMasterId);
            parms.Add("ItemId", objTestClass.ItemId);
            parms.Add("ItemCode", objTestClass.ItemCode);
            parms.Add("ItemBarCode", objTestClass.ItemBarCode);
            parms.Add("UnitId", objTestClass.UnitId);
            parms.Add("Quantity", objTestClass.Quantity);
            parms.Add("FreeQuantity", objTestClass.FreeQuantity);
            parms.Add("PurchaseRate", objTestClass.PurchaseRate);
            parms.Add("IRate", objTestClass.IRate);
            parms.Add("Amount", objTestClass.Amount);
            parms.Add("DiscountPer", objTestClass.ItemDiscountPer);
            parms.Add("DiscountAmount", objTestClass.ItemDiscountAmount);
            parms.Add("IGSTAmount", objTestClass.IGSTAmount);
            parms.Add("CGSTAmount", objTestClass.CGSTAmount);
            parms.Add("SGSTAmount", objTestClass.SGSTAmount); 
            parms.Add("GrossAmount", objTestClass.GrossAmount);
            parms.Add("CessPer", objTestClass.CessPer);
            parms.Add("CessAmount", objTestClass.CessAmount);
            parms.Add("TaxPer", objTestClass.TaxPer);
            parms.Add("TaxAmount", objTestClass.TaxAmount);
            parms.Add("NetAmount", objTestClass.ItemNetAmount);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("FinancialYearId", objTestClass.FinancialYearId);
            parms.Add("TaxType", objTestClass.TaxType);
            parms.Add("TotalSaveAmount", objTestClass.TotalSaveAmount);
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
        public DmnPurchaseDetails getDataById(string procedureName, DmnPurchaseDetails objTestClass)
        {
            DAL.DapperRepository<DmnPurchaseDetails> objDapperRepository = new DAL.DapperRepository<DmnPurchaseDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("PurchaseDetailId", objTestClass.PurchaseDetailId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnPurchaseMaster objTestClass)
        {
            DAL.DapperRepository<DmnPurchaseDetails> objDapperRepository = new DAL.DapperRepository<DmnPurchaseDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("PurchaseDetailId", objTestClass.PurchaseDetailId);
            parms.Add("PurchaseMasterId", objTestClass.PurchaseMasterId);
            parms.Add("ItemId", objTestClass.ItemId);
            parms.Add("ItemCode", objTestClass.ItemCode);
            parms.Add("ItemBarCode", objTestClass.ItemBarCode);
            parms.Add("UnitId", objTestClass.UnitId);
            parms.Add("Quantity", objTestClass.Quantity);
            parms.Add("FreeQuantity", objTestClass.FreeQuantity);
            parms.Add("PurchaseRate", objTestClass.PurchaseRate);
            parms.Add("IRate", objTestClass.IRate);
            parms.Add("Amount", objTestClass.Amount);
            parms.Add("DiscountPer", objTestClass.ItemDiscountPer);
            parms.Add("DiscountAmount", objTestClass.ItemDiscountAmount);
            parms.Add("IGSTAmount", objTestClass.IGSTAmount);
            parms.Add("CGSTAmount", objTestClass.CGSTAmount);
            parms.Add("SGSTAmount", objTestClass.SGSTAmount);
            parms.Add("GrossAmount", objTestClass.GrossAmount);
            parms.Add("CessPer", objTestClass.CessPer);
            parms.Add("CessAmount", objTestClass.CessAmount);
            parms.Add("TaxPer", objTestClass.TaxPer);
            parms.Add("TaxAmount", objTestClass.TaxAmount);
            parms.Add("NetAmount", objTestClass.ItemNetAmount);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("FinancialYearId", objTestClass.FinancialYearId);
            parms.Add("TaxType", objTestClass.TaxType);
            parms.Add("TotalSaveAmount", objTestClass.TotalSaveAmount);
            parms.Add("StockItem", objTestClass.StockItem);
            parms.Add("QuantitytoSubtract", objTestClass.QuantitytoSubtract);
            parms.Add("Cost", objTestClass.Cost);
            parms.Add("Margin", objTestClass.Margin);
            parms.Add("MRP", objTestClass.MRP);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnPurchaseDetails objTestClass)
        {
            DAL.DapperRepository<DmnPurchaseDetails> objDapperRepository = new DAL.DapperRepository<DmnPurchaseDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("PurchaseDetailId", objTestClass.PurchaseDetailId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
        
        public bool DeleteByMasterId(string procedureName, DmnPurchaseMaster objTestClass)
        {
            DAL.DapperRepository<DmnPurchaseMaster> objDapperRepository = new DAL.DapperRepository<DmnPurchaseMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("PurchaseMasterId", objTestClass.PurchaseMasterId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
        
        public bool DeleteMiscCharges(string procedureName, DmnPurchaseMaster objTestClass)
        {
            DAL.DapperRepository<DmnPurchaseMaster> objDapperRepository = new DAL.DapperRepository<DmnPurchaseMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("PurchaseMasterId", objTestClass.PurchaseMasterId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
        public bool UpdateBranchPurcahseSrlNo(string procedureName, DmnPurchaseMaster objTestClass)
        {
            DAL.DapperRepository<DmnPurchaseMaster> objDapperRepository = new DAL.DapperRepository<DmnPurchaseMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BranchId", objTestClass.BranchId);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool AddPurchaseMiscCharges(string procedureName, DmnPurchaseMaster objTestClass)
        {
            DAL.DapperRepository<DmnPurchaseMaster> objDapperRepository = new DAL.DapperRepository<DmnPurchaseMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("PurchaseMasterId", objTestClass.PurchaseMasterId);
            parms.Add("GeneralId", objTestClass.GeneralId);
            parms.Add("MiscAmount", objTestClass.MiscAmount);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
    }
}
