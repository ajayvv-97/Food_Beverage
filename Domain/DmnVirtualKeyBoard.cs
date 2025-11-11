using System;

namespace Domain
{
    public class DmnVirtualKeyBoard
    {
        public int Id { get; set; }
        public int BranchId { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public string mDeviceIP { get; set; }
        

        // === Number Keyboard Access ===
        public bool Enb_Home_Number { get; set; }
        public bool Enb_AllowVirtualKeyboardLogin_Number { get; set; }
        public bool Enb_TablePickUp_Number { get; set; }
        public bool Enb_Operation_Number { get; set; }
        public bool Enb_Employee_Number { get; set; }
        public bool Enb_Billing_Number { get; set; }
        public bool Enb_Report_Number { get; set; }
        public bool Enb_OrderPage_Number { get; set; }
        public bool Enb_KitchenView_Number { get; set; }
        public bool Enb_SearchPage_Number { get; set; }
        public bool Enb_SaleBill_YZoutPage_Number { get; set; }

        // === Normal Keyboard Access ===
        public bool Enb_Home_Normal { get; set; }
        public bool Enb_AllowVirtualKeyboardLogin_Normal { get; set; }
        public bool Enb_TablePickUp_Normal { get; set; }
        public bool Enb_Operation_Normal { get; set; }
        public bool Enb_Employee_Normal { get; set; }
        public bool Enb_Billing_Normal { get; set; }
        public bool Enb_Report_Normal { get; set; }
        public bool Enb_OrderPage_Normal { get; set; }
        public bool Enb_KitchenView_Normal { get; set; }
        public bool Enb_SearchPage_Normal { get; set; }
        public bool Enb_SaleBill_YZoutPage_Normal { get; set; }
    }
}
