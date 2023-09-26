using System;

namespace Domain
{
    public class DmnCategory
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public long BranchId { get; set; }
        public long CategorySrlNoStart { get; set; }
        public long CategorySrlNoCurrent { get; set; }
        public long PriceTypeId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
    }
}
