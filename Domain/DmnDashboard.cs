using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnDashboard
    {
        public long UserId { get; set; }
        public long UserAccountTypeId { get; set; }
        public string Username { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public long BranchId { get; set; }
        public string BranchName { get; set; }

    }
}
