using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnAccountGroup
    {
        public long AccountGroupId { get; set; }
        public string AccountGroupName { get; set; }
        public string AccountGroupCode { get; set; }
        public long SrlNoStart { get; set; }
        public long SrlNoCurrent { get; set; }
        public long BranchId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int Count { get; set; }
        public long LedgerCode { get; set; }
    }
}
