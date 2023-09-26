using System;
using System.Collections.Generic;
using System.Text;
using Domain;


namespace Domain
{
   public class DmnOfferDetails 
    {
        public DateTime EntryDate { get; set; }

        public string OfferDesc { get; set; }
        public long ItemId { get; set; }
        public int ItemCode { get; set; }
        public long IBarCode { get; set; }
        public string ItemName { get; set; }
        public decimal pRate { get; set; }
        public decimal sRate { get; set; }
        public decimal MRP { get; set; }
        public decimal OffrRate { get; set; }
        public decimal DiscRate { get; set; }
        public decimal DiscPer { get; set; }
        public long BillAmt { get; set; }
        public DateTime fDt { get; set; }
        public DateTime tDt { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public long Status { get; set; }
        public long BranchId { get; set; }
        public string Remarks { get; set; }
        
    }
}
