using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnItem : DmnCategory
    {
        public int slno { get; set; }
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        public string Barcode { get; set; }
        public string ItemName { get; set; }
        public int BarcodeMode { get; set; }
        public string BrandName { get; set; }
        public string SearchName { get; set; }
        public long PacketQty { get; set; }
        public decimal? QuantityOnHand { get; set; }
        public decimal? CGSTPer { get; set; }
        public decimal? SGSTPer { get; set; }
        public decimal? IGSTPer { get; set; }
        public decimal? CessPer { get; set; }
        public int? SalesTaxIncl { get; set; }
        public int? PurchaseTaxIncl { get; set; }
        public long? ManufactureId { get; set; }
        public string Rack { get; set; }
        public decimal? SellingPer { get;set;}

        public decimal? SaleRate { get; set; }
        public decimal? SubSaleRate { get; set; }
        public decimal? LastSaleRate { get; set; }
        public decimal? LastSubSaleRate { get; set; }

        public long? MainUnitId { get; set; }
        public decimal? PurchaseRate1 { get; set; }
        public decimal? MRP1 { get; set; }
        public long? SubUnitId { get; set; }
        public decimal? PurchaseRate2 { get; set; }
        public decimal? MRP2 { get; set; }
        public string HSNCode { get; set; }
        public string Image { get; set; }
        public long? TaxType { get; set; }
        public decimal? LastSellingPer { get; set; }
        public decimal? LastPurchaseRate1 { get; set; }
        public decimal? LastMRP1 { get; set; }
        public decimal? LastPurchaseRate2 { get; set; }
        public decimal? LastMRP2 { get; set; }
        public int? StockItem { get; set; }
        public int? LockSaleRate { get; set; }
        public long? ItemClassId { get; set; }
        public string Remarks { get; set; }
        public decimal? GTypeSubValue1 { get; set; }
        public string UnitName { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string FieldName { get; set; }
        public decimal? FieldValue { get; set; }
        public decimal? UnitId { get; set; }
        public string ItemClassName { get; set; }
        public string Caption { get; set; }
        public string SubUnitName { get; set; }
         
    }
}
