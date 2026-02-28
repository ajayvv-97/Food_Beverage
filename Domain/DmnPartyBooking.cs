using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPartyBooking
    {
        public int PartyBookingId { get; set; }
        public int PartyPackId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public DateTime BookingDate { get; set; }
        public string BookingTime { get; set; }
        public string EventType { get; set; }

        public int NoOfAdults { get; set; }
        public int NoOfChildren { get; set; }

        public decimal PackPrice { get; set; }

        public string Tax { get; set; }
        public string TaxType { get; set; }
        public decimal TaxPer { get; set; }
        public decimal TaxAmount { get; set; }

        public decimal DiscountPer { get; set; }
        public decimal DiscountAmount { get; set; }

        public decimal PreTaxAmount { get; set; }
        public decimal PostTaxAmount { get; set; }

        public decimal NetAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal BalanceAmount { get; set; }

        public int PaymentMode { get; set; }

        public int CreatedBy { get; set; }
        public int BranchId { get; set; }
        public int Status { get; set; }
        public string DiscountReason { get; set; }
        public string EventDescription { get; set; }
    }
    
}
