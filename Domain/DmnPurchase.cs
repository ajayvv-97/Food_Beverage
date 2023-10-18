using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPurchase
    {
        public int PurchaseMasterId { get; set; }
        public int PurchaseReturnDetailId { get; set; }
        public int PurchaseReturnMasterId { get; set; }
        public int PurchaseDetailId { get; set; }
        public string EntryNo { get; set; }
        public int Product_id { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal QTY { get; set; }
        public string Type { get; set; }
        public string Tax { get; set; }
        public string TaxType { get; set; }
        public decimal Prate { get; set; }
        public decimal Srate { get; set; }
        public decimal MRP { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmnt { get; set; }
        public decimal TaxPer { get; set; }
        public decimal TotalAmnt { get; set; }
        public decimal ItemId { get; set; }
        public decimal UnitId { get; set; }
        public int BranchId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public string DocNo { get; set; }
        public DateTime DocDate { get; set; }
        public string Customer { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public decimal TotQty { get; set; }
        public decimal GrandTot { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal OaAmount { get; set; }
        public string OaDesc { get; set; }
        public decimal OdAmount { get; set; }
        public string OdDesc { get; set; }
        public decimal NetAmount { get; set; }
        public decimal RcvAmnt { get; set; }
        public decimal BalAmnt { get; set; }
        public string Remark { get; set; }
        public string Terms { get; set; }
        public decimal PRate1 { get; set; }
        public decimal SRate1 { get; set; }
        public decimal MRP1 { get; set; }
        public decimal Qoh1 { get; set; }
        public bool MultiValueProduct { get; set; }
        public int PrId { get; set; }
        public int Fcid { get; set; }
    }
}
