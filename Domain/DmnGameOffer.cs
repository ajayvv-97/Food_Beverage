using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public  class DmnGameOffer
    {
        public int id { get; set; }
        public DateTime entrydate { get; set; }
        public DateTime fDt { get; set; }
        public DateTime tDt { get; set; }
        public string OfferDesc { get; set; }
        public int gmid { get; set; }
        public decimal offerRate { get; set; }
        public decimal DiscRate { get; set; }
        public decimal DiscPer { get; set; }
        public decimal VIPOfferRate { get; set; }
        public decimal VIPDiscRate { get; set; }
        public decimal VIPDiscPer { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int status { get; set; }
        public string Remark { get; set; }
        public int BranchId { get; set; }



    }
}
