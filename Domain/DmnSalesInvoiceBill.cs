using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnSalesInvoiceBillHeaders
    {
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public long BranchId { get; set; }
        public string GSTNo { get; set; }
        public string CompanyName { get; set; }
    }
    public class DmnSalesInvoiceBill
    {
        public string EntryNo { get; set; }
        public DateTime EntryDate { get; set; }
        public string LedgerName { get; set; }
        public string Username { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalCGST { get; set; }
        public decimal TotalSGST { get; set; }
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public long UserAccountId { get; set; }
        public decimal TotalCess { get; set; }
        public decimal TotalQuantity { get; set; }
        public string PaymentMode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string GSTNo { get; set; }
        public string TransportationNo { get; set; }

    }
    public class DmnSalesInvoiceBillDetails
    {
        public string SearchName { get; set; }
        public decimal Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal SalesRate { get; set; }
        public string Caption { get; set; }
        public decimal NetAmount { get; set; }
        public long SalesMasterId { get; set; }
        public long PurchaseReturnMasterId { get; set; }
        public string HSNCode { get; set; }
        public string UnitName { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal PurcahseRate { get; set; }


    }
}
