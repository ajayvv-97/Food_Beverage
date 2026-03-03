using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class DmnProducts : DmnTaxSettings
	{
		public int ProductId { get; set; }
		public int BranchId { get; set; }
		public string Product { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string UPC { get; set; }
		public string UPCBarcode { get; set; }
		public int CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public int ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
		public int Status { get; set; }
		public string Tax { get; set; }
		public string TaxType { get; set; }
		public int TaxSettingsId { get; set; }
		public int Quantity { get; set; }
		public int Branchid { get; set; }
		public int Fcid { get; set; }
		public decimal DiscountAmt { get; set; }
		public decimal DiscountPer { get; set; }
		public string OfferDescription { get; set; }
		public decimal MRP { get; set; }

		public string ItemCode { get; set; }

		public int menustatus { get; set; }
		public string Name { get; set; }

		public int Ppid { get; set; }
		public int PreparationMode { get; set; }

        public bool IsStockTracked { get; set; }
        public int FoodType { get; set; }
		public bool HasVariation { get; set; }
		public string ForeColor { get; set; }
		public string BackColor { get; set; }
	}
}
