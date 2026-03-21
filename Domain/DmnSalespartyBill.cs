using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{

    public class DmnSalespartyBill
    {
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public DateTime BookingDate { get; set; }
        public TimeSpan BookingTime { get; set; }
        public string EventType { get; set; }

        public int NoOfAdults { get; set; }
        public int NoOfChildren { get; set; }

        public string EventDescription { get; set; }
        public string DiscountReason { get; set; }

    }
}
