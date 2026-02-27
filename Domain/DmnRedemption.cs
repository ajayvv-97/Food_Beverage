using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class DmnRedemption 
	{
		public int ProductId { get; set; }
		public string Description { get; set; }
		public decimal SuggestedPointValue { get; set; }
		public decimal SuggestedCouponValue { get; set; }
		public string UPC { get; set; }
		public int DepartmentId { get; set; }
		public int CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public int ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
		public int BranchId { get; set; }
		public int Status { get; set; }
		public decimal Price { get; set; }
		public int QOH { get; set; }
		public int TaxSettingsId { get; set; }
		public string Tax { get; set; }
		//my code
		public decimal Prate { get; set; }
		public decimal Srate { get; set; }
		public decimal MRP { get; set; }
		public decimal Prate1 { get; set; }
		public decimal Srate1 { get; set; }
		public decimal MRP1 { get; set; }
		public decimal IgstPrcnt { get; set; }
		public decimal SgstPrcnt { get; set; }
		public decimal CgstPrcnt { get; set; }
		public decimal VatPrcnt { get; set; }
		public int CategoryId { get; set; }
		public int Product_id { get; set; }
		public string Branch { get; set; }
		public int ProductType { get; set; }
		public int PrId { get; set; }
		public decimal DiscountPer { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal TaxPer { get; set; }
		public decimal Amount { get; set; }

	}
}
