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
        public decimal Prate { get; set; }
        public decimal Srate { get; set; }

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
        public string ItemCode_UPC { get; set; }
        public int Saletype { get; set; }
        public decimal MRP { get; set; }
        public decimal QOH { get; set; }
        public int PrId { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal DiscountPer { get; set; }

        public string HSNcode { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int ReorderLevel { get; set; }
        public bool Isexpire { get; set; }
        public bool LockSaleRate { get; set; }
        public int Baseunit { get; set; }
        public decimal Conversionfactor { get; set; }

    }
    
}
