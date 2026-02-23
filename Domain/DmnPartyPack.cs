using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPartyPack
    {
        public int PartyPackId { get; set; }
        public string PackName { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal DiscountAmt { get; set; }
        public string TaxType { get; set; }
        public int TaxSettingsId { get; set; }

        public int CreatedBy { get; set; }
        public int BranchId { get; set; }

        public string DetailsXML { get; set; } // 🔥 important
    }
    public class DmnPartyPackDetails
    {
        public int PartyPackDetailsId { get; set; }
        public int PartyPackId { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }

        public int CreatedBy { get; set; }
        public int BranchId { get; set; }
        public int Status { get; set; }
        public int MenuCategoryId { get; set; }
    }
    

}
