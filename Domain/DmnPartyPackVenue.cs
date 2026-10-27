using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnPartyPackVenue
    {
        public int PartyPackVenueId { get; set; }
        public int PartyPackId { get; set; }

        public string VenueName { get; set; }
        public decimal VenueCost { get; set; }
        public int NoOfAttendees { get; set; }
        public string Description { get; set; }


        public decimal IgstPrcnt { get; set; }
        public decimal VatPrcnt { get; set; }

        public string TaxType { get; set; }
        public int TaxSettingsId { get; set; }
        public string Tax { get; set; }

        public int CreatedBy { get; set; }
        public int BranchId { get; set; }
        public int Status { get; set; }
    }
}
