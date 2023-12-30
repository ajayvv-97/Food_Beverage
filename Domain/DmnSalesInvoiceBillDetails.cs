using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public  class DmnSalesInvoiceBillDetails
    {
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal SalesRate { get; set; }
        public string Caption { get; set; }
        public decimal NetAmount { get; set; }
        public long SalesMasterId { get; set; }
        public decimal Taxper { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Amount { get; set; }

        public decimal TotalVAT { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalCGST { get; set; }
        public decimal TotalSGST { get; set; }
    }
}
