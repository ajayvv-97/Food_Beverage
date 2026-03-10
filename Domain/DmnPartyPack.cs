using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPartyPack
    {
        public int PartyPackId { get; set; }
        public string PackName { get; set; }
        public decimal AdultPrice { get; set; }
        public decimal ChildPrice { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal DiscountAmt { get; set; }
        public string TaxType { get; set; }
        public int TaxSettingsId { get; set; }

        public int CreatedBy { get; set; }
        public int BranchId { get; set; }
        public int Status { get; set; }
        public int DetailsCount { get; set; }
        public string Tax { get; set; }
        public decimal MinimumAdvanceAmount { get; set; }
    }
    public class DmnPartyPackDetails
    {
        public int PartyPackDetailsId { get; set; }
        public int PartyPackId { get; set; }
        public string Item { get; set; }
        public int MenuCategoryId { get; set; }
        public int Quantity { get; set; }

        public int CreatedBy { get; set; }
        public int BranchId { get; set; }
        public int Status { get; set; }
        public int PackItemType { get; set; }

        public int Productid { get; set; }
        public int ProductType{ get; set; }

    }
    
}
