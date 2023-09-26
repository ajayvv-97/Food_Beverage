using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllSalesReturnDetails
    {// 
        ///
        public bool AddData(string procedureName, DmnSalesReturnDetails objTestClass)
        {
            DAL.DapperRepository<DmnSalesReturnDetails> objDapperRepository = new DAL.DapperRepository<DmnSalesReturnDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesReturnMasterId", objTestClass.SalesReturnMasterId);
            parms.Add("ItemId", objTestClass.ItemId);
            parms.Add("ItemCode", objTestClass.ItemCode);
            parms.Add("ItemBarCode", objTestClass.ItemBarCode);
            parms.Add("UnitId", objTestClass.UnitId);
            parms.Add("Quantity", objTestClass.Quantity);
            parms.Add("FreeQuantity", objTestClass.FreeQuantity);
            parms.Add("SalesRate", objTestClass.SalesRate);
            parms.Add("IRate", objTestClass.IRate);
            parms.Add("Amount", objTestClass.Amount);
            parms.Add("DiscountPer", objTestClass.ItemDiscountPer);
            parms.Add("DiscountAmount", objTestClass.ItemDiscountAmount);
            parms.Add("IGSTAmount", objTestClass.IGSTAmount);
            parms.Add("CGSTAmount", objTestClass.CGSTAmount);
            parms.Add("SGSTAmount", objTestClass.SGSTAmount);
            parms.Add("GrossAmount", objTestClass.GrossAmount);
            parms.Add("TaxPer", objTestClass.TaxPer);
            parms.Add("CessPer", objTestClass.CessPer);
            parms.Add("CessAmount", objTestClass.CessAmount);
            parms.Add("TaxAmount", objTestClass.TaxAmount);
            parms.Add("NetAmount", objTestClass.ItemNetAmount);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("FinancialYearId", objTestClass.FinancialYearId);
            parms.Add("TaxType", objTestClass.TaxType);
            parms.Add("Isrno", objTestClass.Isrno);
            parms.Add("TotalSaveAmount", objTestClass.TotalSaveAmount);
            parms.Add("MRP", objTestClass.MRP);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("QuantitytoSubtract", objTestClass.QuantitytoSubtract);
            parms.Add("StockItem", objTestClass.StockItem);
            //      parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnSalesReturnDetails getDataById(string procedureName, DmnSalesReturnDetails objTestClass)
        {
            DAL.DapperRepository<DmnSalesReturnDetails> objDapperRepository = new DAL.DapperRepository<DmnSalesReturnDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesReturnDetailsId", objTestClass.SalesReturnDetailsId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnSalesReturnDetails objTestClass)
        {
            DAL.DapperRepository<DmnSalesReturnDetails> objDapperRepository = new DAL.DapperRepository<DmnSalesReturnDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesReturnDetailsId", objTestClass.SalesReturnDetailsId);
            parms.Add("SalesReturnMasterId", objTestClass.SalesReturnMasterId);
            parms.Add("ItemId", objTestClass.ItemId);
            parms.Add("ItemCode", objTestClass.ItemCode);
            parms.Add("ItemBarCode", objTestClass.ItemBarCode);
            parms.Add("UnitId", objTestClass.UnitId);
            parms.Add("Quantity", objTestClass.Quantity);
            parms.Add("FreeQuantity", objTestClass.FreeQuantity);
            parms.Add("SalesRate", objTestClass.SalesRate);
            parms.Add("Amount", objTestClass.Amount);
            parms.Add("DiscountPer", objTestClass.DiscountPer);
            parms.Add("DiscountAmount", objTestClass.DiscountAmount);
            parms.Add("IGSTAmount", objTestClass.IGSTAmount);
            parms.Add("CGSTAmount", objTestClass.CGSTAmount);
            parms.Add("SGSTAmount", objTestClass.SGSTAmount);
            parms.Add("GrossAmount", objTestClass.GrossAmount);
            parms.Add("TaxAmount", objTestClass.TaxAmount);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("FinancialYearId", objTestClass.FinancialYearId);
            parms.Add("TaxType", objTestClass.TaxType);
            parms.Add("Isrno", objTestClass.Isrno);
            parms.Add("TotalSaveAmount", objTestClass.TotalSaveAmount);
            parms.Add("MRP", objTestClass.MRP);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnSalesReturnDetails objTestClass)
        {
            DAL.DapperRepository<DmnSalesReturnDetails> objDapperRepository = new DAL.DapperRepository<DmnSalesReturnDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesReturnDetailsId", objTestClass.SalesReturnDetailsId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }

     public List<DmnUnit> GetItemUnits(DmnItem dmnItem)
             
        {
            DAL.DapperRepository<DmnUnit> objDapperRepository = new DAL.DapperRepository<DmnUnit>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("ItemId", dmnItem.ItemId);
            var listUnits= objDapperRepository.SelectQuery("prdFillUnit", parms);
            return listUnits; 
        }

        public bool UpdateBranchSalesReturnSrlNo(string procedureName, DmnSalesReturnMaster objTestClass)
        {
            DAL.DapperRepository<DmnSalesMaster> objDapperRepository = new DAL.DapperRepository<DmnSalesMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BranchId", objTestClass.BranchId);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }

        public bool DeleteByMasterId(string procedureName, DmnSalesReturnDetails objTestClass)
        {
            DAL.DapperRepository<DmnSalesReturnDetails> objDapperRepository = new DAL.DapperRepository<DmnSalesReturnDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesReturnMasterId", objTestClass.SalesReturnMasterId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }


    }
}
