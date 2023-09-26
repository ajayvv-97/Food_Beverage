using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public class DmnHireLocker
    {
        
        public int LockHireID { get; set; }
        public DateTime EntryDate { get; set; }
        public int LockID { get; set; }
        public int AccID { get; set; }
        public string AccNo { get; set; }
        public string AccPhone { get; set; }
        public string AccEmail { get; set; }
        public string CustomerPin { get; set; }
        public int HireFor { get; set; }
        public int RateType { get; set; }
        public decimal Rate { get; set; }
        public decimal Amt { get; set; }
        public DateTime ExpDate { get; set; }
        public DateTime ExpTime { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime HireTime { get; set; }
        public int LockOpen { get; set; }
        public string Inotes { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public long BranchId { get; set; }
        public string Hiredetail { get; set; }
        public int HireNo { get; set; }
        public string AccName { get; set; }
        public string AccAddress { get; set; }
        public int Hired { get; set; }
        


    }
}
