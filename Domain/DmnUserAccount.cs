using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnUserAccount : DmnEmployee
    {
        public long UserAccountId { get; set; }
        public long UserAccountTypeId { get; set; }
        public string UserAccountType { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastLogOutDate { get; set; }
        public string EmailId { get; set; }
        public long BranchId { get; set; }
        public string Mobile { get; set; }
        public int LoginStatus { get; set; }
        public long EmployeeId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public string EmployeeName { get; set; }
        public string Note { get; set; }
        public int RFID_Login_Status { get; set; }
    }
}
