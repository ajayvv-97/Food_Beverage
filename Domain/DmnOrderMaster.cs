using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnOrderMaster:DmnOrderDetails
    {
        public int OrderMasterId { get; set; }
        public string EntryNo { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public int LedgerId { get; set; }
        public string Address { get; set; }
        public int PaymentMode { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Remarks { get; set; }
        public int BranchId { get; set; }
        public int TotalQuantity { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DocumentDate { get; set; }
        public int Tid { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int koid { get; set; }
        public int BookmasterId { get; set; }
        public decimal ReservationTotal { get; set; }
        public decimal AdvancePay { get; set; }
        public decimal CustomerName { get; set; }
        
    }
}
