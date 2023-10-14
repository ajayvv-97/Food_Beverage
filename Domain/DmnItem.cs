using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnItem :DmnTaxSettings
    {
        public long ItemId { get; set; }
        public string Itemname { get; set; }
        public string summary { get; set; }
        public int Fcid { get; set; }
        public decimal price { get; set; }
        public decimal quantity { get; set; }
        public int unit { get; set; }        
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int BranchId { get; set; }
        public int TaxSettingsId { get; set; }
        public string Tax { get; set; }
        public string TaxType { get; set; }
        public string UnitName { get; set; }
        public string CategoryName { get; set; }
        
            

    }
}
