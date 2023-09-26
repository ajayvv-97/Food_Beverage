using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPurchaseMiscCharges:DmnAccountLedger
    {
        public long PurchaseMiscId { get; set; }
        public long  GeneralId { get; set; }
        public decimal MiscAmount { get; set; }

    }
}
