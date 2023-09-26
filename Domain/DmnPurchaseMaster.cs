using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPurchaseMaster : DmnPurchaseDetails
    {
        public long PurchaseMasterId { get; set; }
        public long EType { get; set; }
        public string EntryNo { get; set; }
        public DateTime EntryDate { get; set; }
        public string DocumentNo { get; set; }
        public long LedgerId { get; set; }
        public string PurchaseAddress { get; set; }
        public long PaymentMode { get; set; }
        public string Telephone { get; set; }
        public string Remarks { get; set; }
        public decimal TotalGross { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalIGST { get; set; }
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
        public string OtherAddionDesc { get; set; }
        public string OtherDeductionDesc { get; set; }
        public long TransportNo { get; set; }
        public long AdminId { get; set; }
        public int CreditDays { get; set; }
        public DateTime DueDate { get; set; }
        public string DocumentType { get; set; }
        public long BranchId { get; set; }
        public long FinancialYearId { get; set; }
        public long TotalQuantity { get; set; }
        public string SalesTerms { get; set; }
        public DateTime DocumentDate { get; set; }
        public decimal TotalNetAmount { get; set; }
        public decimal AmountAdjust { get; set; }
        public decimal TotalMiscCharges { get; set; }
        public decimal TotalCess { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public string Caption { get; set; }
        public DateTime ExpDate { get; set; }
    }
}
