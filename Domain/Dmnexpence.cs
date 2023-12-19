
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{

    public class Dmnexpence
    {
        public int Eid { get; set; }
        public int reason { get; set; }
        public decimal Amount { get; set; }
        public string description { get; set; }
        public int Empolyeeid { get; set; }
        public int paymentid { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }


}
