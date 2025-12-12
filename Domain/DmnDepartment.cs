using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnDepartment : DmnDesignation
    {
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public long BranchId { get; set; }
        public int EmployeeSrlNoStart { get; set; }
        public long EmployeeSrlNoCurrent { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int Count { get; set; }
        public int RFID_Login_Status { get; set; }
        public int TabletModestatus { get; set; }

    }
}
