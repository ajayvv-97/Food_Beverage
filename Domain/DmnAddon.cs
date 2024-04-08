using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnAddon
    {
        public int Aid { get; set; }
        public string AddonName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int BranchId { get; set; }
        public int Fcid { get; set; }
        public string CategoryName { get; set; }
        

    }
}
