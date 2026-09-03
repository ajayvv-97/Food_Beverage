using System;

namespace Domain
{
    public class DmnRemoveComplimentarySale
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public int PaymentMode { get; set; }
        
    }
}