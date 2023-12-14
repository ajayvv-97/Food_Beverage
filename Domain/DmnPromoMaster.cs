using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class DmnPromoMaster
	{
		public int PromoMId { get; set; }
		public string Caption { get; set; }
		public string Description { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public decimal MinBillAmt { get; set; }
		public decimal MaxDiscount { get; set; }
		public decimal DiscountPer { get; set; }
		public string PromoLabel { get; set; }
		public int TotalPromo { get; set; }
		public int AvilablePromo { get; set; }
		public int CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public int ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
		public int Status { get; set; }
	}
	public class DmnPromoDetails
	{
		public int PromoDId { get; set; }
		public string Code { get; set; }
		public int Status { get; set; }
		public int CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public int ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
		public int PromoMId { get; set; }
	}
}
