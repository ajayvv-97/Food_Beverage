using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public  class DmnGame:DmnGeneralOptions
    {
        public int Id { get; set; }
        public string mGame { get; set; }
        public string Macid { get; set; }
        public string Rfid { get; set; }
        public decimal CardRate { get; set; }
        public decimal TokenRate { get; set; }
        public decimal CoinRate { get; set; }
        public decimal DiscRate { get; set; }
        // public string Description { get; set; }
        public string gDesc { get; set; }
        public int Createdby { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime Modifydate { get; set; }
        public int status { get; set; }
        public long Branchid { get; set; }
        public int Location { get; set; }    
        public string Icon { get; set; }
        public decimal viprate { get; set; }
        public int TimeDuration { get; set; }
        public int CategoryId { get; set; }
      
    }
}
