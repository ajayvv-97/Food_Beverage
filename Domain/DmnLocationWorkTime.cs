using System;

namespace Domain
{
    public class DmnLocationWorkTime
    {
        public int Tid { get; set; }
        public int? LocId { get; set; }
        public int? DayId { get; set; }

        // SQL time maps best to TimeSpan
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }

        public int? Status { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }

        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
