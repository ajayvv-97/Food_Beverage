using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnkitchenBilldetails
    {
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        public string TableCode { get; set; }
        public int OrderMasterId { get; set; }
        public int AddonStatus { get; set; }
        public string AV_id { get; set; }
        public string MainCategory { get; set; }
        public int TableType { get; set; }
        public int offerqty { get; set; }
        public string offeritemname { get; set; }
        public string cobmoitemname { get; set; }

    }
}
