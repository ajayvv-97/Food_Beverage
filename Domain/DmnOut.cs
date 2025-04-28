using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnOutCard
    {
        public decimal TotalQuantity { get; set; }
        public string Itemname { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Totaltax { get; set; }
        public string ProductType { get; set; }
        public int ProductId { get; set; }

    }



    public class DmnOutDrawer
    {
        public decimal TotalAmount { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal Drops { get; set; }
        public decimal TotalCardCashRefund { get; set; }
        public decimal TotalCashRefund { get; set; }
    }
    public class DmnOutGenInfo
    {
        public decimal TotalQuantity { get; set; }
        public string Itemname { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalCompAmt { get; set; }
        public decimal Others { get; set; }
        public decimal TotalCash { get; set; }
        public decimal TotalBankonline { get; set; }
        public decimal TotalUPI { get; set; }
        public decimal TotalDebitAmt { get; set; }
        public decimal TotalCreditAmt { get; set; }
        public decimal Totaltax { get; set; }
    }
    
    
}

