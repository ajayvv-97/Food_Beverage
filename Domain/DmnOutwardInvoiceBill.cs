using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnOutwardInvoiceBill
    {
        public string EntryNo { get; set; }
        public DateTime EntryDate { get; set; }
        public string LedgerName { get; set; }
        public string Username { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalCGST { get; set; }
        public decimal TotalSGST { get; set; }
        public decimal MasterNetAmount { get; set; }
        public decimal TotalCess { get; set; }
        public string PaymentMode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string GSTNo { get; set; }
        public string TransportationNo { get; set; }
        public string AmountInWords { get; set; }
        public string TaxAmountInWords { get; set; }


        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string CompanyGSTNo { get; set; }
        public string CompanyName { get; set; }

        public long SrlNo { get; set; }
        public string SearchName { get; set; }
        public decimal Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal SalesRate { get; set; }
        public string Caption { get; set; }
        public decimal NetAmount { get; set; }
        public string HSNCode { get; set; }
        public string UnitName { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal CGSTPer { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal SGSTPer { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal TaxAmount { get; set; }
    }
}

