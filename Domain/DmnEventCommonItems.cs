using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnEventCommonItems
    {
        public int EventItemId { get; set; }
        public int EventCategoryType { get; set; }
        public string ItemName { get; set; }

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }

        public int TaxSettingsId { get; set; }
        public string Tax { get; set; }
        public string TaxType { get; set; }

        public decimal DiscountPer { get; set; }
        public decimal DiscountAmt { get; set; }

        public decimal TotalAmount { get; set; }

        public string Description { get; set; }

        public int BranchId { get; set; }
        public int Status { get; set; }

        public int CreatedBy { get; set; }
    }
}
