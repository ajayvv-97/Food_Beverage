using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnMenu:DmnItem
    {
        public long MId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
         public int Fcid { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int ProductId { get; set; }
        public int BranchId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal DisAmt { get; set; }
        public decimal DisPer { get; set; }
        public decimal Quantity { get; set; }
        public int ProductType { get; set; }
        public int PreparationMode { get; set; }
        public int ResetStatus { get; set; }

        public int OrderQty { get; set; }
        public int OrderReservedQty { get; set; }
        public int OrderStatus { get; set; }
        public DateTime OrderCreatedOn { get; set; }
        public DateTime? OrderUpdatedOn { get; set; }
        public int Actiontype { get; set; }
        public int TableType { get; set; }
        public int CustomQtyFlag { get; set; }
        public int PreviousOrderQty { get; set; }

    }
}
