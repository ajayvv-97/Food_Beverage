using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPurchaseMaster 
    {
        public int PurchaseMasterId { get; set; }
        public int PurchaseReturnDetailId { get; set; }
        public int PurchaseReturnMasterId { get; set; }
        public int PurchaseDetailId { get; set; }
        public string EntryNo { get; set; }
        public int Product_id { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal QTY { get; set; }
        public string Type { get; set; }
        public string Tax { get; set; }
        public string TaxType { get; set; }
        public decimal Prate { get; set; }
        public decimal Srate { get; set; }
        public decimal MRP { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmnt { get; set; }
        public decimal TaxPer { get; set; }
        public decimal TotalAmnt { get; set; }
        public decimal ItemId { get; set; }
        public decimal UnitId { get; set; }
        public int BranchId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public string DocNo { get; set; }
        public DateTime DocDate { get; set; }
        public string Customer { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public decimal TotQty { get; set; }
        public decimal GrandTot { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal OaAmount { get; set; }
        public string OaDesc { get; set; }
        public decimal OdAmount { get; set; }
        public string OdDesc { get; set; }
        public decimal NetAmount { get; set; }
        public decimal RcvAmnt { get; set; }
        public decimal BalAmnt { get; set; }
        public string Remark { get; set; }
        public string Terms { get; set; }
        public decimal PRate1 { get; set; }
        public decimal SRate1 { get; set; }
        public decimal MRP1 { get; set; }
        public decimal Qoh1 { get; set; }
        public bool MultiValueProduct { get; set; }
        public int PrId { get; set; }
        public int Fcid { get; set; }


        public int EType { get; set; }
        public DateTime EntryDate { get; set; }
        public string DocumentNo { get; set; }
        public long LedgerId { get; set; }
        public string PurchaseAddress { get; set; }
        public string PaymentText { get; set; }
        public int PaymentMode { get; set; }
        public string Telephone { get; set; }
        public string Remarks { get; set; }
        public decimal TotalGross { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal TotalTax { get; set; }
        public decimal TotalCess { get; set; }
        public decimal TotalIGST { get; set; }
        public decimal TotalCGST { get; set; }
        public decimal TotalSGST { get; set; }
        public decimal TotalMiscCharges { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal AmountAdjust { get; set; }

        public string TransportNo { get; set; }
        public int CreditDays { get; set; }
        public DateTime DueDate { get; set; }
        public string DocumentType { get; set; }

        public long FinancialYearId { get; set; }
        public long TotalQuantity { get; set; }
        public DateTime DocumentDate { get; set; }
        public decimal TotalNetAmount { get; set; }

        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
        public string LedgerAddress { get; set; }
        public string Mobile { get; set; }
        public decimal MiscAmount { get; set; }
        public long GeneralId { get; set; }

        public decimal OtherDeduction { get; set; }
        public decimal OtherAdditions { get; set; }
        public string OtherAddionDesc { get; set; }
        public string OtherDeductionDesc { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public string purmasterEntryNo { get; set; }
        public string SalesTerms { get; set; }
        public string Description { get; set; }

    }
}
