using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnInwardItemDetails
    {
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemBarcode { get; set; }
        public string SearchName { get; set; }
        public int PurchaseTaxIncl { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal IGSTPer { get; set; }
        public decimal CessPer { get; set; }
        public long TaxType { get; set; }
        public decimal MRP { get; set; }
        public decimal SellingPer { get; set; }
        public decimal LockSaleRate { get; set; }
        public long PacketQty { get; set; }
        public decimal QuantityOnhand { get; set; }
        public int StockItem { get; set; }
        public string TaxTypeName { get; set; }
        public decimal TaxPer { get; set; }

        public Decimal SaleRate { get; set; }
    }
}
