using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnTaxSettings
    {

        public int TaxSettingsId { get; set; }
        public string TaxType { get; set; }
        public decimal CgstPrcnt { get; set; }
        public decimal IgstPrcnt { get; set; }
        public decimal SgstPrcnt { get; set; }
        public decimal VatPrcnt { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public int Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; } = DateTime.Now;

    }
}
