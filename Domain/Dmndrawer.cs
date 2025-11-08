using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Dmndrawer
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InitilAmount { get; set; }
        public decimal Drops { get; set; }
        public string MacId { get; set; }
        public string DName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int EmployeeId { get; set; }
        public DateTime openTime { get; set; }
    }
}
