using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPartyBookingDetails
    {
        public int PartyBookingDetailsId { get; set; }
        public int PartyBookingId { get; set; }

        public int ProductId { get; set; }
        public int ProductType { get; set; } // Card=1, Gift=2, Cake=3
        public int TableType { get; set; }   // 1=Card, 2=Gift, 3=Cake

        public decimal Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }

        public string Tax { get; set; }
        public string TaxType { get; set; }
        public decimal TaxPer { get; set; }
        public decimal TaxAmount { get; set; }
        public int TaxSettingId { get; set; }

        public decimal DiscountPer { get; set; }
        public decimal DiscountAmount { get; set; }

        public decimal IGSTAmt { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal VATAmt { get; set; }

        public decimal PreTaxAmount { get; set; }
        public decimal PostTaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        

        public int CreatedBy { get; set; }
        public int BranchId { get; set; }
        public int Status { get; set; }
        public int Prid { get; set; }
        public string Product { get; set; }
        public string CakeRemarks { get; set; }
        public string CakeMessage { get; set; }
        public bool IsPackage { get; set; }

        public int SalesMasterId { get; set; }
        public int SalesDetailsId { get; set; }

        public string CategoryName { get; set; }
    }
}

    
