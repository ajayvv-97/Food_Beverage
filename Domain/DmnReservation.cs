using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{

    public class DmnReservation:DmnReservationDetails
    {
        public int BookmasterId { get; set; }
        public int OrderMasterId { get; set; }
        
        public string cusName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string EmailId { get; set; }
        public string Remarks { get; set; }
        public int BranchId { get; set; }
        public string gender { get; set; }
        public string Token { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public string BookingType { get; set; }
        public string NoOfPeople { get; set; }
        public DateTime BookingDateTime { get; set; }
        public Decimal ReservationTotal { get; set; }
        public Decimal MenuTotal { get; set; }
        public Decimal GrandTotal { get; set; }
        public Decimal DiscountAmt { get; set; }
        public Decimal DiscountPer { get; set; }
        public Decimal OtherAdd { get; set; }
        public Decimal OtherDed { get; set; }
        public string OtherAddText { get; set; }
        public string OtherDedText { get; set; }
        public Decimal NetAmount { get; set; }
        public string Terms { get; set; }
        public Decimal AdvancePay { get; set; }
        public Decimal CashRev { get; set; }
        public Decimal BalnceAmt { get; set; }
        public Decimal BalnceNeed { get; set; }
        public Decimal FinalAmount { get; set; }
        public int RevStatus { get; set; }
        public int Tid { get; set; }
        public int Cancel_Type { get; set; }
        public Decimal PaymentTimes { get; set; }
        public string AdvanceAmts_List { get; set; }
        public Decimal CashGivenAmt { get; set; }
        public int CashGivenflag { get; set; }
        

    }
}
