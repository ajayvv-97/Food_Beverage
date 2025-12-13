using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnAccountLedger
    {
        public int CusId { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int Gender { get; set; }   
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string CompanyName { get; set; }
        public string GSTNo { get; set; }
        public string FullAddress { get; set; }
        public int Status { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        public decimal CardBalance { get; set; }
        public decimal CashBonusBalance { get; set; }
        public decimal TokenBalance { get; set; }
        public decimal PointBalance { get; set; }
        public int LedgerId { get; set; }
        public int SalesMasterId { get; set; }

    }
}
