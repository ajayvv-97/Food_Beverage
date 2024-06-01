using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnOrderDetails
    {
        public int OrderDetailsId { get; set; }
        public int OrderMasterId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemBarCode { get; set; }
        public int UnitId { get; set; }
        public int Quantity { get; set; }
        public int CurrentQuantity { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal IRate { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal VATAmount { get; set; }

        public decimal GrossAmount { get; set; }
        public decimal TaxPer { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int BranchId { get; set; }
        public string TaxType { get; set; }
        public decimal MRP { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
        public DateTime ExpDate { get; set; }
        public decimal Srate { get; set; }
        public int Fcid { get; set; }
        public string Tax { get; set; }
        public int PrId { get; set; }
        public string Notes { get; set; }
        public int OrderNo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string RemoveNotes { get; set; }
        public int ProductType { get; set; }
        public int TableType { get; set; }
        public decimal MarketPrice { get; set; }
        public int BookingFlag { get; set; }
        public string MainRemarks { get; set; }
        public int Tid { get; set; }
        public int KOTPrintStatus { get; set; }
        public string TableCode { get; set; }
        public string OrderCancelNotes { get; set; }
        public int Pavid { get; set; }
        public int AddonStatus { get; set; }
        public string AV_id { get; set; }
        public int Addoncount { get; set; }
        public string Kitchennumber { get; set; }
        public int vid { get; set; }        
        public string CCode { get; set; }
    } 
}
