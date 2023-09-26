using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnSalesMiscCharges
    {

        public long SalesMiscId { get; set; }
        public long  SalesMasterId { get; set; }
        public long GeneralId { get; set; }
        public decimal MiscAmount { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
    }
}
