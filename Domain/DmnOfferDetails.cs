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
        
    }
}
