using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public class DmnCustAccount : DmnAccountLedger
    {
     
        public int AccID { get; set; }
        public string AccNo { get; set; }
        public string AccName { get; set; }
        public string AccPhone { get; set; }
        public string AccEmail { get; set; }
        public int Gender { get; set; }
        public string Gen { get; set; }
        public DateTime DOB { get; set; }
        public string AccAddress { get; set; }
        public int Age { get; set; }
        public int Proof { get; set; }
        public string ProofName { get; set; }
        public string ProofNo { get; set; }
        public string Remark { get; set; }

        public DateTime EnrollDate { get; set; }
        public int ZipCode { get; set; }
        public string Proofimg1 { get; set; }
        public string Occupation { get; set; }
        public string CInstitute { get; set; }
        public int AllowEmail { get; set; }
        public int AllowSMS { get; set; }
        public string cCountry { get; set; }//change
        public string State { get; set; }//change
        public string cEmergency { get; set; }
        
        public string City { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public long BranchId { get; set; }
        public string ICardNo { get; set; }
        


    }
}
