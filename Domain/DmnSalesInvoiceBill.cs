using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public  class DmnSalesInvoiceBill
    {
        public string EntryNo { get; set; }
        public DateTime EntryDate { get; set; }
        public string CusName { get; set; }
        public string Username { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalCGST { get; set; }
        public decimal TotalSGST { get; set; }
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalAmnt { get; set; }
        public long UserAccountId { get; set; }
        public decimal TotalVAT { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal RcvAmnt { get; set; }
        public string PaymentMode { get; set; }
        public string SalesAddress { get; set; }
        public string Telephone { get; set; }
        public string GSTNo { get; set; }
        public string TransportationNo { get; set; }
        public string Remarks { get; set; }
        public string Email { get; set; }
        public decimal OaAmount { get; set; }
        public decimal OdAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public int PromoDId { get; set; }
        public decimal PromoAmount { get; set; }
        public decimal ReservationTotal { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal PkChargeAmount { get; set; }
        public int Rev_Status { get; set; }
        public int OrderType { get; set; }
        public int PkId { get; set; }

    }
    
}
