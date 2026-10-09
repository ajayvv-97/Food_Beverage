using System;

namespace Domain
{
    public class DmnReason
    {
        public long ReasonId { get; set; }
        public string ReasonName { get; set; }
        public string Description { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public int Status { get; set; }
        public long BranchId { get; set; }
    }
}