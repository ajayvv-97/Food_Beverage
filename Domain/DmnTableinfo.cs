using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnTableinfo
    {
        public int Tid { get; set; }        
        public string TableCode { get; set; }
        public int Capcity { get; set; }
        public int Fcid { get; set; }
        public int RevStatus { get; set; }      
        public string Description { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int BranchId { get; set; }
        public int OrderStatus { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int OrderMasterId { get; set; }
        public int BookmasterId { get; set; }
        

    }
}
