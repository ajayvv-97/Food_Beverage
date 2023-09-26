using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnAccountLedger:DmnItem
    {
        public long LedgerId { get; set; }
        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
        public long AccountGroupId { get; set; }
        public string ICardNo { get; set; }
        public long SrlNoCurrent { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string LedgerAddress { get; set; }
        public string TINNo { get; set; }
        public string EmailId { get; set; }
        public string Remarks { get; set; }
        public decimal DiscountPer { get; set; }
        public int CreditDays { get; set; }
        public decimal CreditLimit { get; set; }
        public long BranchId { get; set; }
        public decimal InterestPerDay { get; set; }
        public long BankId { get; set; }
        public string BankAccountNo { get; set; }
        public string BankBranch { get; set; }
        public string BankIFSC { get; set; }
        public string PAN { get; set; }
        public string GSTNo { get; set; }
        public string CompanyRegNo { get; set; }
        public string HSNCode { get; set; }
        public long State { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public string Photo { get; set; }
        public decimal loypoints { get; set; }
        public decimal redimpoints { get; set; }
        public decimal loycash { get; set; }
        public decimal Total_loypoints { get; set; }
        public int cardstatus { get; set; }
        public int cardtype { get; set; }

        public DateTime vfrom { get; set; }

        public DateTime vto { get; set; }

        public DateTime dob { get; set; }

        public int age { get; set; }

        public int gender { get; set; }
        public int MemberShip { get; set; }
        


    }
}
