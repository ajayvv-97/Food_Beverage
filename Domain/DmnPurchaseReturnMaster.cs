using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPurchaseReturnMaster : DmnPurchaseReturnDetails
    {

        /*-----------------*/
        
        public string itemName { get; set; }
        public string Description { get; set; }
        public string PurchaseRate { get; set; }
        public string Quantity { get; set; }
        public string ItemId { get; set; }
        public string ProductType { get; set; }
        public string PurchaseDetailId { get; set; }
        public string PurchaseMasterId { get; set; }
        public string PurchaseRetPrefix { get; set; }
        public string PurchaseRetSrlNo { get; set; }

        /*-----------------*/



        public long PurchaseReturnMasterId { get; set; }
        public int EType { get; set; }
        public string EntryNo { get; set; }
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

        public string otheraddamount { get; set; }
        public string otheraddamountdes { get; set; }
        public string otherdedamount { get; set; }
        public string otherdedamountdes { get; set; }
        public string recamount { get; set; }
        public string balamount { get; set; }
        public string Terms { get; set; }
        public string purmasterEntryNo { get; set; }
        public int Prid { get; set; }

        public int AvaQOH { get; set; }
    }
}
