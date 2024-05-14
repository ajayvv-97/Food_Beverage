using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnCategory
    {
        public long Fcid { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int BranchId { get; set; }
        public int CategoryType { get; set; }
        public string ColorCode { get; set; }

    }
}
