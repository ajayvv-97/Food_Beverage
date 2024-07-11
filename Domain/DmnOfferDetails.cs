using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnOfferDetails
    {

        public int Odid { get; set; }
        public int ProductId { get; set; }
        public int Omid { get; set; }
        public decimal Discountper { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal FinalPrice { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int BranchId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public int Prid { get; set; }
        public string Productname { get; set; }
        public int Producttype { get; set; }
        public string Tax { get; set; }
        public int TaxSettingsId { get; set; }
        public string TaxType { get; set; }
        public decimal Price { get; set; }
        public string ItemCode_UPC { get; set; }

        public int AddonStatus { get; set; }
        public string  AV_id { get; set; }
        public int Addoncount { get; set; }
        public int Pavid { get; set; }
        public decimal Srate { get; set; }
        public string VariatiionName { get; set; }


    }
}
