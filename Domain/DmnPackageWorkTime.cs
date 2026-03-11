using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPackageWorkTime
    {
        public int PkgTimeId { get; set; }

        public int? BranchId { get; set; }

        public int? DayId { get; set; }

        public TimeSpan? FromTime { get; set; }

        public TimeSpan? ToTime { get; set; }

        public int? Status { get; set; }

        public int? CreatedBy { get; set; }
        public int? PartyPackId { get; set; }
        
        public DateTime? CreatedDateTime { get; set; }
    }
}
