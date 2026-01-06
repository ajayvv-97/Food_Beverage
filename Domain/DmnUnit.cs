using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnUnit
    {
        public long UId { get; set; }
        public string Description { get; set; }
        public string UnitName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int BranchId { get; set; }

    }
    public class DmnUnitConversion
    {
        public long id { get; set; }
        public long FromUnit { get; set; }
        public long ToUnit { get; set; }
        public Decimal Factor { get; set; }
        public string FUnit { get; set; }
        public string TUnit { get; set; }
        public Decimal Fctr { get; set; }
    }
}
