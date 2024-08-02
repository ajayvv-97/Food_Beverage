using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{    public class DmnPickup
    {        public int PkId { get; set; }
        public string CusName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string EmailId { get; set; }
        public string Remarks { get; set; }
        public int BranchId { get; set; }
        public int Gender { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal OtherAdd { get; set; }
        public decimal OtherDed { get; set; }
        public string OtherAddText { get; set; }
        public string OtherDedText { get; set; }
        public decimal NetAmount { get; set; }
        public decimal CashRev { get; set; }
        public decimal BalnceAmt { get; set; }
        public string RemoveNotes { get; set; }
        public int SalesMasterId { get; set; }
        public DateTime OrderCancelDateTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
    }
}
