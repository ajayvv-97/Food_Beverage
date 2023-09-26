using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public class DmnOutward : DmnOutwardDetails
    {
        public long OutwardId { get; set; }
        public int EType { get; set; }
        public string EntryNo { get; set; }
        public DateTime EntryDate { get; set; }
        public string DocumentNo { get; set; }
        public int toid { get; set; }
        public long LedgerId { get; set; }
        public string SalesAddress { get; set; }
        public string PaymentText { get; set; }
        public int PaymentMode { get; set; }
        public string Telephone { get; set; }
        public string Remarks { get; set; }
        public decimal TotalGross { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalCess { get; set; }
        public decimal TotalIGST { get; set; }
        public decimal TotalCGST { get; set; }
        public decimal TotalSGST { get; set; }
        public decimal TotalMiscCharges { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal AmountAdjust { get; set; }
        public decimal NetAmount { get; set; }
        public string TransportNo { get; set; }
        public int CreditDays { get; set; }
        public DateTime DueDate { get; set; }
        public string DocumentType { get; set; }
        public long BranchId { get; set; }
        public long FinancialYearId { get; set; }
        public long TotalQuantity { get; set; }
        public DateTime DocumentDate { get; set; }
        public decimal TotalNetAmount { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
        public string LedgerAddress { get; set; }
        public string Mobile { get; set; }
        public decimal MiscAmount { get; set; }
        public long GeneralId { get; set; }
        
    }
}

