using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Dmnincrediant
    {
        
        public int Ing_DetailsId { get; set; }
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public int unit { get; set; }
        public decimal quantity { get; set; }
        public string Instructions { get; set; }
        public string Recipe { get; set; }
        public int Mid { get; set; }
        
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int BranchId { get; set; }
        public int ProductId { get; set; }
        public int Deductionstage { get; set; }
        public int ProductType { get; set; }
        

    }
}
