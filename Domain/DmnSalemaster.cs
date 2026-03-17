using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnSalemaster
    {
   
            public int SalesMasterId { get; set; }
            public string EntryNo { get; set; }
            public DateTime EntryDate { get; set; }
            public int Product_id { get; set; }
            public string Barcode { get; set; }          

            public string Unit { get; set; }
            public decimal QTY { get; set; }
            public string Type { get; set; }
            public string Tax { get; set; }
            public string TaxType { get; set; }
            public string CardNo { get; set; }
            public decimal TaxPer { get; set; }
            public decimal cashbalance { get; set; }

            public decimal Prate { get; set; }
            public decimal Srate { get; set; }
            public decimal MRP { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal Amount { get; set; }
            public decimal DisPer { get; set; }
            public decimal DisAmnt { get; set; }
            public decimal TotalAmnt { get; set; }
            public decimal ItemId { get; set; }
            public decimal UnitId { get; set; }
            public int BranchId { get; set; }
            public long CreatedBy { get; set; }
            public DateTime CreatedDateTime { get; set; }
            public long ModifiedBy { get; set; }
            public DateTime ModifiedDateTime { get; set; }
            public int Status { get; set; }
            public string terms { get; set; }
            public int lblBalAmt { get; set; }
            public int rcvamt { get; set; }

            public DateTime CardDate { get; set; }
            public string DocNo { get; set; }
            public DateTime DocDate { get; set; }

            public decimal TotQty { get; set; }
            public decimal GrandTot { get; set; }
            public decimal TotalDiscount { get; set; }
            public decimal OaAmount { get; set; }
            public string OaDesc { get; set; }
            public decimal OdAmount { get; set; }
            public string OdDesc { get; set; }
            public string oddesc { get; set; }
            public decimal NetAmount { get; set; }
            public decimal RcvAmnt { get; set; }
            public decimal BalAmnt { get; set; }
            public string Remark { get; set; }
            public string Terms { get; set; }
            public decimal LoylityPoint { get; set; }
            public decimal BonusPoint { get; set; }
            public decimal CashBonusBalance { get; set; }
            public decimal TokenBalance { get; set; }
            public decimal PointBalance { get; set; }
            public decimal TotalTax { get; set; }
            public decimal TotalIGST { get; set; }
            public decimal TotalCGST { get; set; }
            public decimal TotalSGST { get; set; }
            public decimal TotalVAT { get; set; }
            public decimal IgstAmount { get; set; }
            public decimal CgstAmount { get; set; }
            public decimal SgstAmount { get; set; }
            public decimal VatAmount { get; set; }
            public string mDevice { get; set; }
            public string mDeviceIP { get; set; }
            public int Prid { get; set; }
            public decimal CardPrice { get; set; }
            public int NewCardCount { get; set; }
            public int NameFlag { get; set; }
        public int PayId { get; set; }
        public int PromoDId { get; set; }
        public int PromoMId { get; set; }
        public int Promostatus { get; set; }
        public int Tid { get; set; }
        public int OrderMasterid { get; set; }
        public decimal PromoAmount  { get; set; }
        public string SalesPrefix { get; set; }
        public string SalesSerialNo { get; set; }
        public int CombinedOrderBill_TYPE { get; set; }
        public int BookmasterId { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal ReservationTotal { get; set; }
        public int TimeDuration { get; set; }
        public int Rev_Status { get; set; }
        public string ICardNo { get; set; }
        public decimal CBalance { get; set; }
        public int TapType { get; set; }
        public int LedgerId { get; set; }
        public int BillCopyStatus { get; set; }
        public int CombineStatus { get; set; }
        public int OrderType { get; set; }
        public int PkId { get; set; }
        public decimal PkChargeAmount { get; set; }
        public bool IsDirectSale { get; set; }

        public string DebitICardNo { get; set; }
        public int DebitLedgerId { get; set; }
        public int CardType { get; set; }
        public int FMCardType { get; set; }
        public string PartPayId { get; set; }
        public string PartAmount { get; set; }
        public string Caption { get; set; }
        public int FormType { get; set; }
        public string DBname { get; set; }

        // ============================
        // NEW CUSTOMER DETAILS (ADDED)
        // ============================

        public int cusid { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string CompanyName { get; set; }
        public string GSTNo { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FullAddress { get; set; }
        public int Gender { get; set; }

        public decimal TotalPreTaxAmt { get; set; }
        public int SaleType { get; set; }
        public int PartyBookingId { get; set; }
        public string AdditionalChargeDescription { get; set; }
        public decimal AdditionalChargeAmt { get; set; }
    }
    

}
