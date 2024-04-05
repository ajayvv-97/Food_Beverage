using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Dmntable 
    {
        
        public int @Tid { get; set; }
        public string TableCode { get; set; }
        public decimal Capcity { get; set; }
        public int TCid { get; set; }       
        public int RevStatus { get; set; }
        public string Decription { get; set; }
        public int Status { get; set; }
        public long BranchId { get; set; }
        public long CreatedBy { get; set; }
        public decimal Price { get; set; }
        public int tblCnt { get; set; }

    }
}

