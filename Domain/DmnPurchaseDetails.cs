using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPurchaseDetails 
    {
        public long PurchaseDetailId { get; set; }
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemBarCode { get; set; }
        public long UnitId { get; set; }
        public decimal Quantity { get; set; }
        public decimal FreeQuantity { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal IRate { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxPer { get; set; }
        public decimal CessPer { get; set; }
        public decimal CessAmount { get; set; }
        public decimal ItemNetAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string TaxType { get; set; }
        public string Tax { get; set; }
        public decimal Cost { get; set; }
        public decimal Margin { get; set; }
        public decimal MRP { get; set; }
        public decimal Srate { get; set; }
        public decimal QuantitytoSubtract { get; set; }
        public decimal TotalSaveAmount { get; set; }

    }
}
