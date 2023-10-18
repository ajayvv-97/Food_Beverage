using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPurchaseReturnDetails
    {
        public long PurchaseReturnDetailsId { get; set; }
        public long PurchaseReturnMasterId { get; set; }
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
        public decimal NetAmount { get; set; }
        public long BranchId { get; set; }
        public long FinancialYearId { get; set; }
        public string TaxType { get; set; }
        public string Tax { get; set; }
        public decimal Cost { get; set; }
        public decimal Margin { get; set; }
        public decimal MRP { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public decimal QuantitytoSubtract { get; set; }
        public int StockItem { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public decimal LockSaleRate { get; set; }
        public decimal SellingPer { get; set; }
        public long PacketQty { get; set; }
        public decimal QuantityOnHand { get; set; }
        public int PurchaseTaxIncl { get; set; }
        public string Caption { get; set; }

        /*------------------------------------*/
        public string taxname { get; set; }
        public string PMDocumentNo { get; set; }
        public string srate { get; set; }
        public string PurchaseMasterId { get; set; }
        public string PurchasedetailsId { get; set; }
        public string ProductType { get; set; }
        public string EntryNo { get; set; }
        public int PrId { get; set; }

        /*------------------------------------*/
    }
}
