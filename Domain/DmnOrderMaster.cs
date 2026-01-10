using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnOrderMaster : DmnOrderDetails
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
        public string CustomerName { get; set; }
        public int Tablecount { get; set; }
        public int OrderDetailsId { get; set; }
        public int AddonStatus { get; set; }
        public int SalesMasterId { get; set; }
        public decimal FinalAmount { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal OtherAdd { get; set; }
        public decimal OtherDed { get; set; }
        public string OtherAddText { get; set; }
        public string OtherDedText { get; set; }
        public int PickupOrderFlag { get; set; }
        public int PkId { get; set; }

        // ============================
        // NEW CUSTOMER DETAILS (ADDED)
        // ============================

        public int cusid { get; set; }
        public string FirstName { get; set; }      
        public string SecondName { get; set; }      
        public string CompanyName { get; set; }     
        public string GSTNo { get; set; }           
        public string PhoneNumber { get; set; }    
        public string EmailAddress { get; set; }
        public string FullAddress { get; set; }
        public int Gender { get; set; }

        // Guest counts (matching your DB columns)
        public int AdultCount { get; set; } = 1;
        public int ChildCount { get; set; } = 0;
        public int OtherGuestCount { get; set; } = 0;
        public int TotalGuestCount { get; set; } = 1;


        public int OrderPlatformmaster { get; set; } = 1;
        public string OrderPlatformmasterName { get; set; } = "1";
    }
}
