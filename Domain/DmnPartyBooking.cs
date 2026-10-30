using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPartyBooking
    {
        public int PartyBookingId { get; set; }
        public int PartyPackId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime BookingDateTime { get; set; }

        public int EventType { get; set; }

        public int NoOfAdults { get; set; }
        public int NoOfChildren { get; set; }

        public decimal PackPrice { get; set; }

        public string Tax { get; set; }
        public string TaxType { get; set; }
        public decimal TaxPer { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal VATAmt { get; set; }

        public decimal DiscountPer { get; set; }
        public decimal DiscountAmount { get; set; }

        public decimal PreTaxAmount { get; set; }
        public decimal PostTaxAmount { get; set; }

        public decimal NetAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal BalanceAmount { get; set; }

        public int PaymentMode { get; set; }

        public int CreatedBy { get; set; }
        public int BranchId { get; set; }
        public int Status { get; set; }
        public string DiscountReason { get; set; }
        public string EventDescription { get; set; }

        // 🔹 Cake Totals
        public decimal CakeGrandTotal { get; set; }
        public decimal CakePreTaxAmount { get; set; }
        public decimal CakeTaxAmount { get; set; }

        // 🔹 Card Totals
        public decimal CardGrandTotal { get; set; }
        public decimal CardPreTaxAmount { get; set; }
        public decimal CardTaxAmount { get; set; }

        // 🔹 Gift Totals
        public decimal GiftGrandTotal { get; set; }
        public decimal GiftPreTaxAmount { get; set; }
        public decimal GiftTaxAmount { get; set; }

        // 🔹 Package Totals
        public decimal PackGrandTotal { get; set; }
        public decimal PackPreTaxAmount { get; set; }
        public decimal PackTaxAmount { get; set; }

        public string AdditionalChargeDescription { get; set; }
        public decimal AdditionalChargeAmt { get; set; }

        public decimal ExtraChargeGrandTotal { get; set; }
        public decimal ExtraChargePreTaxAmount { get; set; }
        public decimal ExtraChargeTaxAmount { get; set; }

        public string HSNCode { get; set; }

        public DateTime? KOTDatetime { get; set; }
        public bool KoTPrintStatus { get; set; }
        public bool ConsumedRawMaterialStatus { get; set; }

          
    }

    
}
