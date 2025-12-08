
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnCardAccountDebit
    {
        public int DebitLedgerId { get; set; }
        public string DebitICardNo { get; set; }
        public int FMCardType { get; set; }
        public int CardType { get; set; }

        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int Status { get; set; }
        public string mDevice { get; set; }
        public string mDeviceIP { get; set; }
        public string DBname { get; set; }
    }

}
