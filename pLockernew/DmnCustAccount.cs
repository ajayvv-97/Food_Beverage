using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public class DmnCustAccount
    {
        
        public int AccID { get; set; }
        public string AccNo { get; set; }
        public string AccName { get; set; }
        public string AccPhone { get; set; }
        public string AccEmail { get; set; }
        public int Gender { get; set; }
        public DateTime DOB { get; set; }
        public string AccAddress { get; set; }
        public int Age { get; set; }
        public int Proof { get; set; }
        public string ProofNo { get; set; }
        public string Remark { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public long BranchId { get; set; }
        


    }
}
