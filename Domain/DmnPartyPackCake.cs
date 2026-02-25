using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPartyPackCake
    {
        public int PartyPackCakeId { get; set; }
        public int PartyPackId { get; set; }

        public string CakeType { get; set; }
        public string CakeSize { get; set; }
        public string CakeShape { get; set; }

        public decimal Qty { get; set; }
        public decimal Price { get; set; }

        public int CreatedBy { get; set; }
        public int BranchId { get; set; }
        public int Status { get; set; }
    }
}
