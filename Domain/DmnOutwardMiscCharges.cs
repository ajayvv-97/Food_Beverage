using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
     public class DmnOutwardMiscCharges
    {
        public long OutwardMiscId { get; set; }
        public long GeneralId { get; set; }
        public decimal MiscAmount { get; set; }
    }
}
