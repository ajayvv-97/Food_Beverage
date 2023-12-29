
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{

    public class DmnPayment
    {
        public int PayId { get; set; }
        public int OrderMasterID { get; set; }
        public int PayType { get; set; }
        public decimal Amount { get; set; }
        public int Tid { get; set; }
        public int SalemasterId { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string OrderDetailsId { get; set; }
        
    }


}

