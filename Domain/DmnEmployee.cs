using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnEmployee: DmnDepartment
    {
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string GuardianName { get; set; }
        public long DesignationId { get; set; }
        public string Qualification { get; set; }
        public string EmployeeAddress { get; set; }
        public string PostalCode { get; set; }
        public DateTime DOB { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string MaritalStatus { get; set; }
        public int MaritalStatusId { get; set; }//change
        public string EmployeeStatus { get; set; }
        public int EmployeeStatusId { get; set; }//change
        public long ECast { get; set; }
        public string SpouseName { get; set; }
        public string PresentAddress { get; set; }

        public string PaymentMode { get; set; }
        public int PaymentModeId { get; set; }
        public long BankId { get; set; }
        public string BankBranch { get; set; }
        public string BankAccountNo { get; set; }
        public string BankIFSC { get; set; }
        public string PFNo { get; set; }
        public string ESINo { get; set; }
        public DateTime PFDate { get; set; }
        public DateTime ESIDate { get; set; }
        public string IdentityProof1 { get; set; }
        public int IdentityProof1Id { get; set; }//new
        public string IdentityProof1No { get; set; }
        public string IdentityProof2 { get; set; }
        public int IdentityProof2Id { get; set; }//new
        public string IdentityProof2No { get; set; }
        public string IdentityProof2Atch { get; set; }
        public string IdentityProof1Atch { get; set; }
        public string Experience { get; set; }
        public decimal TotalExperience { get; set; }
        public string WeeklyOff { get; set; }
        public int WeeklyOffId { get; set; }//new
        public int MonthlyLeave { get; set; }
        public decimal BalanceLeave { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public int MaxLate { get; set; }
        public string LateAffect { get; set; }
        public int LateAffectId { get; set; }//new
        public decimal OverTimeStart { get; set; }
        public DateTime DateOfJoin { get; set; }
        public DateTime DateOfResign { get; set; }
        public string ResignReason { get; set; }
        public decimal Salary { get; set; }
        public string PaymentTerm { get; set; }
        public int PaymentTermId { get; set; }//new
        public string Photo { get; set; }
        public string Remarks { get; set; }
        public string ICardNo { get; set; }

    }
}
