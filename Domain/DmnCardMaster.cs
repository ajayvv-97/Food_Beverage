using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnCardMaster
    {
        public long CardTypeId { get; set; }
        public string CardType { get; set; }
        public string CardCode { get; set; }
        public decimal CardCost { get; set; }
        public decimal BonusPer { get; set; }
        public long Points { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
    }
}
