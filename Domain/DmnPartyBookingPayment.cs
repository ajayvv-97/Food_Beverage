using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPartyBookingPayment
    {
        public int PaymentId { get; set; }
        public int PartyBookingId { get; set; }
        public int PaymentMode { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal RcvAmnt { get; set; }
        public decimal BalAmnt { get; set; }
        public bool IsAdvance { get; set; }
        public string ReferenceNo { get; set; }
        public string PaymentDescription { get; set; }
        public int CreatedBy { get; set; }
        public int BranchId { get; set; }
        public int Status { get; set; }
        public bool IsSplitPayment { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int RefundStatus { get; set; }
    }
    
}

