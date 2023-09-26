using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public class DmnInwardHistoryList
    {
         public long InwardMasterId { get; set; }
        public long InwDetailId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string PurchaseEntryNo { get; set; }
        public string LedgerName { get; set; }
        public long ItemId { get; set; }
        public string SearchName { get; set; }
        public decimal ReturnedQOH { get; set; }
    }
}
