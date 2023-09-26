using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnSalesDetails
    {
        public long SalesDetailsId { get; set; }
        public long SalesMasterId { get; set; }
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemBarCode { get; set; }
        public long UnitId { get; set; }
        public decimal Quantity { get; set; }
        public decimal FreeQuantity { get; set; }
        public decimal SalesRate { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public long BranchId { get; set; }
        public long FinancialYearId { get; set; }
        public long  TaxType { get; set; }
        public long Isrno { get; set; }
        public decimal TotalSaveAmount { get; set; }
        public decimal MRP { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }

        public decimal TaxPer { get; set; }//added
        public decimal IRate { get; set; }//added
        public decimal ItemDiscountAmount { get; set; }//added      
        public decimal ItemDiscountPer { get; set; }//added
        public decimal ItemNetAmount { get; set; }//added
        public decimal CessPer { get; set; }//added
        public decimal CessAmount { get; set; }//added

        public decimal QuantitytoSubtract { get; set; }//this value is used to reduce qoh

        public int StockItem { get; set; }//to determine whete=her need to update qoh

        public string UnitName { get; set; }


    }
}
