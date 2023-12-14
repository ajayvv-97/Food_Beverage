using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnUserAccountType
    {
        public long UserAccountTypeId { get; set; }
        public string UserAccountType { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
