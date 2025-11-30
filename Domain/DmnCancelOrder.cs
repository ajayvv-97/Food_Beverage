using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnCancelOrder
    {
        public int SalesMasterId { get; set; }
        public int OrderMasterid { get; set; }
        public int BranchId { get; set; }
        public int tid { get; set; }
        public int CreatedBy { get; set; }
        public string CancelReason { get; set; }
    }
}
