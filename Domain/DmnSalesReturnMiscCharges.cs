using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnSalesReturnMiscCharges
    {
        public long SalesReturnMiscId { get; set; }
        public long SalesReturnMasterId { get; set; }
        public long GeneralId { get; set; }
        public decimal MiscAmount { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
    }
}
