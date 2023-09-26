using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnHappyHours:DmnGeneralOptions
    {
        public int id { get; set; }
        public int gmid { get; set; }
        public string htitle { get; set; }
        public int Dayid { get; set; }
        public decimal ftime { get; set; }
        public decimal ttime { get; set; }
        public decimal gDur { get; set; }
        public decimal cardrate { get; set; }
        public decimal CardDiscRate { get; set; }
        public decimal CardDiscPer { get; set; }
        public decimal VIPRate { get; set; }
        public decimal VIPDiscRate { get; set; }
        public decimal VIPDiscPer { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int flage { get; set; }
        
    }
    
}
