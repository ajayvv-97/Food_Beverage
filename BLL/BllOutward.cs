using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllOutward
    {
        public DmnOutward AddData(string procedureName, DmnOutward objTestClass)
        {
            DAL.DapperRepository<DmnOutward> objDapperRepository = new DAL.DapperRepository<DmnOutward>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("EType", objTestClass.EType);
            parms.Add("EntryNo", objTestClass.EntryNo);
            parms.Add("EntryDate", objTestClass.EntryDate);
            parms.Add("DocumentNo", objTestClass.DocumentNo);
            parms.Add("LedgerId", objTestClass.LedgerId);
            parms.Add("SalesAddress", objTestClass.SalesAddress);
            parms.Add("PaymentMode", objTestClass.PaymentMode);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Remarks", objTestClass.Remarks);
            parms.Add("TotalGross", objTestClass.TotalGross);
            parms.Add("TotalAmount", objTestClass.TotalAmount);
            parms.Add("TotalDiscount", objTestClass.TotalDiscount);
            parms.Add("TotalTax", objTestClass.TotalTax);
            parms.Add("TotalIGST", objTestClass.TotalIGST);
            parms.Add("TotalCGST", objTestClass.TotalCGST);
            parms.Add("TotalSGST", objTestClass.TotalSGST);
            parms.Add("DiscountPer", objTestClass.DiscountPer);
            parms.Add("DiscountAmount", objTestClass.DiscountAmount);
            parms.Add("AmountAdjust", objTestClass.AmountAdjust);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("TransportNo", objTestClass.TransportNo);
            parms.Add("CreditDays", objTestClass.CreditDays);
            parms.Add("DueDate", objTestClass.DueDate);
            parms.Add("DocumentType", objTestClass.DocumentType);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("FinancialYearId", objTestClass.FinancialYearId);
            parms.Add("TotalQuantity", objTestClass.TotalQuantity);
            parms.Add("DocumentDate", objTestClass.DocumentDate);
            parms.Add("TotalNetAmount", objTestClass.TotalNetAmount);
            parms.Add("TotalMiscCharges", objTestClass.TotalMiscCharges);
            parms.Add("TotalCess", objTestClass.TotalCess);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            parms.Add("toid", objTestClass.toid);

            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnOutward objTestClass)
        {
            DAL.DapperRepository<DmnOutward> objDapperRepository = new DAL.DapperRepository<DmnOutward>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OutwardId", objTestClass.OutwardId);
            parms.Add("EType", objTestClass.EType);
            parms.Add("EntryNo", objTestClass.EntryNo);
            parms.Add("EntryDate", objTestClass.EntryDate);
            parms.Add("DocumentNo", objTestClass.DocumentNo);
            parms.Add("LedgerId", objTestClass.LedgerId);
            parms.Add("SalesAddress", objTestClass.SalesAddress);
            parms.Add("PaymentMode", objTestClass.PaymentMode);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Remarks", objTestClass.Remarks);
            parms.Add("TotalGross", objTestClass.TotalGross);
            parms.Add("TotalAmount", objTestClass.TotalAmount);
            parms.Add("TotalDiscount", objTestClass.TotalDiscount);
            parms.Add("TotalTax", objTestClass.TotalTax);
            parms.Add("TotalIGST", objTestClass.TotalIGST);
            parms.Add("TotalCGST", objTestClass.TotalCGST);
            parms.Add("TotalSGST", objTestClass.TotalSGST);
            parms.Add("DiscountPer", objTestClass.DiscountPer);
            parms.Add("DiscountAmount", objTestClass.DiscountAmount);
            parms.Add("AmountAdjust", objTestClass.AmountAdjust);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("TransportNo", objTestClass.TransportNo);
            parms.Add("CreditDays", objTestClass.CreditDays);
            parms.Add("DueDate", objTestClass.DueDate);
            parms.Add("DocumentType", objTestClass.DocumentType);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("FinancialYearId", objTestClass.FinancialYearId);
            parms.Add("TotalQuantity", objTestClass.TotalQuantity);
            parms.Add("DocumentDate", objTestClass.DocumentDate);
            parms.Add("TotalNetAmount", objTestClass.TotalNetAmount);
            parms.Add("TotalMiscCharges", objTestClass.TotalMiscCharges);
            parms.Add("TotalCess", objTestClass.TotalCess);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("Status", objTestClass.Status);
            //parms.Add("toid", objTestClass.toid);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool UpdateQoh(string procedureName, DmnOutward objTestClass)
        {
            DAL.DapperRepository<DmnOutward> objDapperRepository = new DAL.DapperRepository<DmnOutward>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OutwardDetailsId", objTestClass.OutwardDetailsId);
            parms.Add("ItemId", objTestClass.ItemId);
            parms.Add("QuantitytoSubtract", objTestClass.QuantitytoSubtract);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }

    }
}

