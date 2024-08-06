using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmncombinedBill
    {
        public int Tid { get; set; }
        public string TableCode { get; set; }
        public string CategoryName { get; set; }
        public int OrderMasterId { get; set; }
        public int OrderStatus { get; set; }
        public int BookmasterId { get; set; }
        public decimal NetAmount { get; set; }
        public int BillStatus { get; set; }
        public int Cbid { get; set; }
        public decimal BookingInterwell { get; set; }
        public int TableCount { get; set; }
        public int Fcid { get; set; }
        public int pkid { get; set; }
        public int PickupOrderFlag { get; set; }
        
    }
}
