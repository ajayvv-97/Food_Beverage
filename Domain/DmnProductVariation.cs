using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnProductVariation
    {
        public long VariationId { get; set; }
        public string VariationName { get; set; }
        public decimal Price { get; set; }
        public long ProductId { get; set; }
        public long BranchId { get; set; }
        public long CreatedBy { get; set; }
        public int Status { get; set; }
    }
}
