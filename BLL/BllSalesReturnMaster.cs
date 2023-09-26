using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllSalesReturnMaster
    {
        public DmnSalesReturnMaster AddData(string procedureName, DmnSalesReturnMaster objTestClass)
        {
            DAL.DapperRepository<DmnSalesReturnMaster> objDapperRepository = new DAL.DapperRepository<DmnSalesReturnMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("EType", objTestClass.EType);
            parms.Add("EntryNo", objTestClass.EntryNo);
            parms.Add("EntryDate", objTestClass.EntryDate);
            parms.Add("LedgerId", objTestClass.LedgerId);
            parms.Add("CustomerId", objTestClass.CustomerId);
            parms.Add("SalesAddress", objTestClass.SalesAddress);
            parms.Add("PaymentMode", objTestClass.PaymentMode);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Remarks", objTestClass.Remarks);
            parms.Add("TotalGross", objTestClass.TotalGross);
            parms.Add("TotalAmount", objTestClass.TotalAmount);
            parms.Add("TotalTax", objTestClass.TotalTax);
            parms.Add("TotalDiscount", objTestClass.TotalDiscount);
            parms.Add("TotalCess", objTestClass.TotalCess);
            parms.Add("TotalIGST", objTestClass.TotalIGST);
            parms.Add("TotalCGST", objTestClass.TotalCGST);
            parms.Add("TotalSGST", objTestClass.TotalSGST);
            parms.Add("DiscountPer", objTestClass.DiscountPer);
            parms.Add("DiscountAmount", objTestClass.DiscountAmount);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("AmountAdjust", objTestClass.AmountAdjust);
            parms.Add("PaidAmount", objTestClass.PaidAmount);
            parms.Add("BalanceAmount", objTestClass.BalanceAmount);
            parms.Add("EmployeeId", objTestClass.EmployeeId);
            parms.Add("TotalMiscCharges", objTestClass.TotalMiscCharges);
            parms.Add("ICardNo", objTestClass.ICardNo);          
            parms.Add("CreditDays", objTestClass.CreditDays);
            parms.Add("DueDate", objTestClass.DueDate);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("FinancialYearId", objTestClass.FinancialYearId);
            parms.Add("TotalQuantity", objTestClass.TotalQuantity);
            parms.Add("TotalNetAmount", objTestClass.TotalNetAmount);
            parms.Add("TotalSaveAmount", objTestClass.TotalSaveAmount);
            parms.Add("DeviceDescription", objTestClass.DeviceDescription);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnSalesReturnMaster getDataById(string procedureName, DmnSalesReturnMaster objTestClass)
        {
            DAL.DapperRepository<DmnSalesReturnMaster> objDapperRepository = new DAL.DapperRepository<DmnSalesReturnMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesReturnMasterId", objTestClass.SalesReturnMasterId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnSalesReturnMaster objTestClass)
        {
            DAL.DapperRepository<DmnSalesReturnMaster> objDapperRepository = new DAL.DapperRepository<DmnSalesReturnMaster>();
            DynamicParameters parms = new DynamicParameters();
            
            parms.Add("SalesReturnMasterId", objTestClass.SalesReturnMasterId);
            parms.Add("EType", objTestClass.EType);
            parms.Add("EntryNo", objTestClass.EntryNo);
            parms.Add("EntryDate", objTestClass.EntryDate);
            parms.Add("LedgerId", objTestClass.LedgerId);
            parms.Add("CustomerId", objTestClass.CustomerId);
            parms.Add("SalesAddress", objTestClass.SalesAddress);
            parms.Add("PaymentMode", objTestClass.PaymentMode);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Remarks", objTestClass.Remarks);
            parms.Add("TotalGross", objTestClass.TotalGross);
            parms.Add("TotalAmount", objTestClass.TotalAmount);
            parms.Add("TotalTax", objTestClass.TotalTax);
            parms.Add("TotalDiscount", objTestClass.TotalDiscount);
            parms.Add("TotalCess", objTestClass.TotalCess);
            parms.Add("TotalIGST", objTestClass.TotalIGST);
            parms.Add("TotalCGST", objTestClass.TotalCGST);
            parms.Add("TotalSGST", objTestClass.TotalSGST);
            parms.Add("DiscountPer", objTestClass.DiscountPer);
            parms.Add("AmountAdjust", objTestClass.AmountAdjust);
            parms.Add("DiscountAmount", objTestClass.DiscountAmount);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("PaidAmount", objTestClass.PaidAmount);
            parms.Add("BalanceAmount", objTestClass.BalanceAmount);
            parms.Add("EmployeeId", objTestClass.EmployeeId);
            parms.Add("TotalMiscCharges", objTestClass.TotalMiscCharges);
            parms.Add("ICardNo", objTestClass.ICardNo);
            parms.Add("CreditDays", objTestClass.CreditDays);           
            parms.Add("DueDate", objTestClass.DueDate);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("FinancialYearId", objTestClass.FinancialYearId);
            parms.Add("TotalQuantity", objTestClass.TotalQuantity);
            parms.Add("TotalNetAmount", objTestClass.TotalNetAmount);
            parms.Add("TotalSaveAmount", objTestClass.TotalSaveAmount);
            parms.Add("DeviceDescription", objTestClass.DeviceDescription);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnSalesReturnMaster objTestClass)
        {
            DAL.DapperRepository<DmnSalesReturnMaster> objDapperRepository = new DAL.DapperRepository<DmnSalesReturnMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesReturnMasterId", objTestClass.SalesReturnMasterId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
        public bool UpdateQoh(string procedureName, DmnSalesReturnDetails objTestClass)


        {
            DAL.DapperRepository<DmnSalesReturnDetails> objDapperRepository = new DAL.DapperRepository<DmnSalesReturnDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesReturnDetailsId", objTestClass.SalesReturnDetailsId);
            parms.Add("ItemId", objTestClass.ItemId);
            parms.Add("@QuantitytoSubtract", objTestClass.QuantitytoSubtract);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }

    }
}
