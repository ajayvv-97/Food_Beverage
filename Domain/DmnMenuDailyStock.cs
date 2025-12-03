
using System;

namespace Domain
{
    public class DmnMenuDailyStock
    {
        public int DailyStockId { get; set; }     // PK
        public int MenuId { get; set; }           // FK -> tblMenu.MId
        public int BranchId { get; set; }         // Branch-wise stock
        public DateTime StockDate { get; set; }   // Date of stock
        public int OpeningQty { get; set; }       // Morning qty
        public int UsedQty { get; set; }          // Sale reduced qty
        public int RemainingQty { get; set; }     // Available qty
        public int ResetQty { get; set; }         // Added qty during day
        public long CreatedBy { get; set; }       // User
        public DateTime CreatedOn { get; set; }   // Time
    }
}
