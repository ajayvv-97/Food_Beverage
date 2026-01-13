using System;

namespace Domain
{
    public class DmnCancelItemPrint
    {
        public long OrderDetailId { get; set; }       
        public long OrderMasterId { get; set; }       
        public int Tid { get; set; }
        public string TableCode { get; set; }
        public string itemName { get; set; }
        public int KOTNo { get; set; }                
        public int OrderNo { get; set; }           
        public long ProductId { get; set; }           
        public decimal Quantity { get; set; }         
        public string RemoveNotes { get; set; }  
        public DateTime CreatedDateTime { get; set; }  
        public string EmployeeName { get; set; }     
        public bool IsItemCancelPrintPending { get; set; } 
        public int OrderType { get; set; }             
        public int SalesMasterId { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public int Pkid { get; set; }
    }
    
}
