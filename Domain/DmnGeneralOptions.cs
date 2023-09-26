using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnGeneralOptions
    {
        public long GeneralId { get; set; }
        public string Caption { get; set; }
        public long GTypeId { get; set; }
        public string GType { get; set; }
        public long GTypeSubId { get; set; }
        public decimal? GTypeSubValue1 { get; set; }
        public decimal? GTypeSubValue2 { get; set; }
        public decimal? GTypeSubValue3 { get; set; }
        public string GTypeSubText1 { get; set; }
        public string GTypeSubText2 { get; set; }
        public string GTypeSubText3 { get; set; }
        public DateTime? GTypeSubDate1 { get; set; }
        public DateTime? GTypeSubDate2 { get; set; }
        public DateTime? GTypeSubDate3 { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }

    }
}
