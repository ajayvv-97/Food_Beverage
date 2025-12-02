using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnOrderCancel
    {
        public int SalesMasterId { get; set; }
        public int OrderMasterId { get; set; }
        public int CancelPrintStatus { get; set; }
        public string CancelDescription { get; set; }
        public int TableCode { get; set; }
        public int Pkid { get; set; }
        public string Station { get; set; }
        public string EmployeeName { get; set; }
        public int CreatedBy { get; set; }
        public int BranchId { get; set; }
        public int Tid { get; set; }
        

    }
}
