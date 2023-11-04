using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{    public class DmnDesignation
    {
        public long DesignationId { get; set; }
        public long DepartmentId { get; set; }
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string WorkingHours { get; set; }
        public long BranchId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
    }
}
