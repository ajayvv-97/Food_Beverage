using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnEmployeeEarnDedu : DmnEmployee
    {
        public long EmployeeEarnedId { get; set; }
        public long EarnTypeId { get; set; }
        public decimal EarnedAmount { get; set; }
        public string EarnedDescription { get; set; }
        public long DeductedTypeId { get; set; }
        public decimal DeductedAmount { get; set; }
        public string DeductedDescription { get; set; }

    }
   
}
