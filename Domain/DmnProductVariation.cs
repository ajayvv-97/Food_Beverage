using System;
using System.Collections.Generic;
using System.Text;


namespace Domain
{
    public class DmnProductVariation
    {
        public int VariationId { get; set; }
        public int ProductId { get; set; }

        public string VariationName { get; set; }
        public int? VariationTypeId { get; set; }
        public decimal Price { get; set; }

        public decimal DiscountAmt { get; set; }
        public decimal DiscountPer { get; set; }

        public int? QOH { get; set; }
        public bool IsUnlimited { get; set; }

        public long BranchId { get; set; }
        public long CreatedBy { get; set; }
        public int Status { get; set; }
    }

}
