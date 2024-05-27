using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnOfferMaster:DmnTaxSettings
    {
        public int Omid { get; set; }
        public string OfferName { get; set; }
        public int OfferType { get; set; }
        public decimal Discountper { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal FinalPrice { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int BranchId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OfferstartDatetime { get; set; }
        public DateTime OfferEndDatetime { get; set; }
        public int TaxSettingsId { get; set; }
        public string Tax { get; set; }
        public string TaxType { get; set; }
        public string Description { get; set; }
        public decimal TaxPer { get; set; }
        public decimal TaxAmount { get; set; }
         


    }
}
