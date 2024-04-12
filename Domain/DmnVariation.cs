
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnVariation
    {
        public int Vid { get; set; }
        public int VariationType { get; set; }
        public decimal Price { get; set; }
        public string VariationCode { get; set; }
        public int ProductId { get; set; }
        public string ItemCode_UPC { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int BranchId { get; set; }

    }
}


