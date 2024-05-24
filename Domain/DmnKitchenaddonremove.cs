using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class DmnKitchenaddonremove
    {
        public int Koid { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public int KOTPrintStatus { get; set; }
        public int OrderMasterId { get; set; }
        public int OrderDetailsId { get; set; }
        public int OrderNo { get; set; }
        public int koid { get; set; }
        public int Status { get; set; }
        public string AVid { get; set; }
        public string Update_AVid { get; set; }

        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Srate { get; set; }
        public decimal MarketPrice { get; set; }

    }
}
