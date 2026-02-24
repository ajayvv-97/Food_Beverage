using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPartyPackCustom
    {
        public int PartyPackCustomDetailsId { get; set; }
        public int PartyPackId { get; set; }
        public string Product { get; set; }
        public int MenuCategoryId { get; set; }

        public decimal Qty { get; set; }
        public decimal Price { get; set; }

        public int CreatedBy { get; set; }
        public int BranchId { get; set; }
        public int Status { get; set; }
    }

}
