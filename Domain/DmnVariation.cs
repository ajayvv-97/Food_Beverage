
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnVariation:DmnItem
    {
        public long MId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Fcid { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int ProductId { get; set; }
        public int BranchId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal DisAmt { get; set; }
        public decimal DisPer { get; set; }
        public int Quantity { get; set; }

        public int pavType { get; set; }
        public string Variationname { get; set; }
        public decimal VariationPrice { get; set; }
        public string AddonName { get; set; }
        public decimal AddonNamePrice { get; set; }
        public int Pavid { get; set; }

    }
}
