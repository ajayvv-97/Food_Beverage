using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnBranch
    {
        public long  BranchId{ get; set; }
        public long CompanyId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string PostalCode { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        public long CompanyBankId { get; set; }
        public string PAN { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public string GSTNo { get; set; }
        public string TINNo { get; set; }
        public string SalesPrefix { get; set; }
        public long SalesSrlNo { get; set; }
        public long PurchaseSrlNo { get; set; }
        public long PurchaseReturnSrlNo { get; set; }
        public long SalesReturnSrlNo { get; set; }
        public string InwardPrefix { get; set; }
        public long InwardSrlNo { get; set; }
        public string OutwardPrefix { get; set; }
        public long OutwardSrlNo { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }




    }
}
