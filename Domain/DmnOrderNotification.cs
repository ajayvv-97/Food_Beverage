using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OrderNotification
    {
        public string Message { get; set; }
        public int OrderMasterId { get; set; }
        public int TableOrPickupId { get; set; }
        public DateTime Time { get; set; }
    }

}
