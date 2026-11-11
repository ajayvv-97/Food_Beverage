
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnGeneralSetting
    {
        public int Gid { get; set; }
        public int BranchId { get; set; }
        public bool printdal { get; set; }
        public bool xyprintdal { get; set; }
        public bool dalydal { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public int EmployeeId { get; set; }
        public bool RequireCustomerInfo { get; set; }
        public bool EnableRFID { get; set; }
        public bool EnableDefaultGmail { get; set; }
        public string DefaultGmail { get; set; }

        public int PrintBillType { get; set; }
        public int BillTaxType { get; set; }

        public string FooterContent { get; set; }

        public int PhoneNoLength { get; set; }
        public int ZipCodeLength { get; set; }
        public byte[] BillLogoicon { get; set; }
        public int Logostatus { get; set; }

        public string CompanyNameOtherLanguage { get; set; }
        public string CompanyAddressOtherLanguage { get; set; }
        public string FooterOtherLanguage { get; set; }
        public bool AutoPrint { get; set; }
        public bool AutoDrawer { get; set; }
        public bool RequireCustomerInfo_recharge { get; set; }
        public bool Save_YZout { get; set; }
        public bool Save_Daily { get; set; }
        public string YZPath { get; set; }
        public string DailyPath { get; set; }


        public bool AutoLogoutWhenIdle { get; set; }
        public decimal AutoLogoutIdleTimeoutMinutes { get; set; }
        public bool AllowManualCardInputDuringPayment { get; set; }
        public bool EnableButtonView_Family { get; set; }
        public bool EnableButtonView_GroupSale { get; set; }
        public bool EnableFamilyCardInPayment { get; set; }
        public bool EnableGroupCardInPayment { get; set; }

        public string LogoutFormscustom { get; set; }
        public string DefaultUserName { get; set; }
        public bool EnableUserPrompt { get; set; }
        public bool EnableCompbillPrompt { get; set; }
        public int ProductpageperCount { get; set; }
        public bool EnableCompReasonPrompt { get; set; }
        public bool EnablePartyBookingKOT { get; set; }
        public bool EnableKitchenKOTOnly { get; set; }
        public string Tax { get; set; }
        public string TaxType { get; set; }
        public int? TaxSettingId { get; set; }
        public string PackigHsncode { get; set; }

        // 1 = all order types share one daily token sequence
        // 2 = Pickup, Takeaway and Delivery use separate sequences
        // 3 = Pickup + Takeaway share; Delivery is separate
        public int DailyTokenMode { get; set; } = 2;
    }

}
