using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnSalesMaster
    {
        public long SalesMasterId { get; set; }
        public long EType { get; set; }
        public string EntryNo { get; set; }
        public DateTime EntryDate { get; set; }
        public string DocumentNo { get; set; }
        public long LedgerId { get; set; }
        public long CustomerId { get; set; }
        public string SalesAddress { get; set; }
        public long PaymentMode { get; set; }
        public string Telephone { get; set; }
        public string Remarks { get; set; }
        public decimal TotalGross { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalCess { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalIGST { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalCGST { get; set; }
        public decimal TotalSGST { get; set; }
        public decimal OtherDeduction { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal OtherAdditions { get; set; }
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public long EmployeeId { get; set; }
  
        public string ICardNo { get; set; }
        
        public int CreditDays { get; set; }
        public DateTime DueDate { get; set; }
      
        public long BranchId { get; set; }
        public long FinancialYearId { get; set; }
        public decimal TotalQuantity { get; set; }
        // public string SalesTerms { get; set; }
        public decimal AmountAdjust { get; set; }
        public decimal TotalNetAmount { get; set; }
        public decimal TotalMiscCharges { get; set; }
        public decimal TotalSaveAmount { get; set; }
        public string DeviceDescription { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public decimal loypoints { get; set; }

        public decimal redimvalue { get; set; }
        public Int16 lck { get; set; }
        public decimal loybalvalue { get; set; }



    }
}
