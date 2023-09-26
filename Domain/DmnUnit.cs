using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnUnit
    {
        public long UnitId { get; set; }
        public string UnitName { get; set; }
        public long BranchId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
    }
}
