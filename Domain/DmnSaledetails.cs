using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnSaledetails
    {      
            public int SalesDetailsId { get; set; }
            public int SalesMasterId { get; set; }
            public int MId { get; set; }
            public int UnitId { get; set; }
            public decimal Quantity { get; set; }
            public decimal SalesRate { get; set; }
            public decimal MRP { get; set; }
            public decimal Amount { get; set; }
            public decimal DiscountPer { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal IGSTAmount { get; set; }
            public decimal CGSTAmount { get; set; }
            public decimal SGSTAmount { get; set; }
            public decimal GrossAmount { get; set; }
            public decimal TaxPer { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal CessPer { get; set; }
            public decimal CessAmount { get; set; }
            public decimal NetAmount { get; set; }
            public decimal TotalSaveAmount { get; set; }
            public int BranchId { get; set; }
            public string TaxType { get; set; }
            public string Tax { get; set; }
            public long CreatedBy { get; set; }
            public DateTime CreatedDateTime { get; set; }
            public long ModifiedBy { get; set; }
            public DateTime ModifiedDateTime { get; set; }
            public int Status { get; set; }
            public string ProductType { get; set; }
            public decimal VatAmount { get; set; }
            public int CusId { get; set; }
            public string ItemName { get; set; }
        public int Prid { get; set; }
        public int itemid { get; set; }
        public string ItemBarcode { get; set; }
        public decimal MarketPrice { get; set; }
        public int TableType { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal ReservationTotal { get; set; }
        public int Pavid { get; set; }
        public int AddonStatus { get; set; }
        public string AV_id { get; set; }
        public int Addoncount { get; set; }
        public string Cmid { get; set; }
        public string VariatiionName { get; set; }
        
    }    
}
