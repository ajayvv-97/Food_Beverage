using System;

namespace Domain
{
    public class DmnPaymentAdjustments
    {
        public int CardSalesMasterId { get; set; }
        public string EntryNo { get; set; }
        public DateTime EntryDate { get; set; }

        public int BranchId { get; set; }
        public int Status { get; set; }
        public int SaleEmpid { get; set; }
        public int CreatedBy { get; set; }

        public string Remark { get; set; }

        public int Frompayid { get; set; }
        public int PayId { get; set; }

        public int SaleType { get; set; }
        public int SuperadminId { get; set; }

        public string mDeviceIP { get; set; }
    }
    
}
