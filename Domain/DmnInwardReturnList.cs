using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public class DmnInwardReturnList
    {
        public long InwardMasterId { get; set; }
        public long InwDetailId { get; set; }
        public string EntryNo { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentNo { get; set; }
        public string LedgerName { get; set; }
        public long ItemId { get; set; }
        public string SearchName { get; set; }
        public decimal Quantity { get; set; }
        public decimal ReturnQuantity { get; set; }
        public decimal QuantityOnHand { get; set; }
    }
}
