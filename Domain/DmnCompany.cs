using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnCompany
    {
        public long CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string PostalCode { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public long CompanyBankId { get; set; }
        public string PAN { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public long BranchId { get; set; }
        public long FinancialYearId { get; set; }
        public string Logo { get; set; }
        public string GSTNo { get; set; }
        public string TINNo { get; set; }
        public string STaxNo { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        //
    }
}
