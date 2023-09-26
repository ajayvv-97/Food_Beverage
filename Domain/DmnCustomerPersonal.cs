using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnCustomerPersonal
    {
        public long CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string Photo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ICardNo { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string CustomerAddress { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public long BranchId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
    }
}
