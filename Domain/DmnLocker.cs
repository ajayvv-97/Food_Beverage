using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnLocker
    {
            public long ID { get; set; }
            public int LocationID { get; set; }
            public string LocationDesc { get; set; }
            public string Locker { get; set; }
            public string LockerRowno { get; set; }
            public string MacID { get; set; }
            public string RFID { get; set; }
            public string PinNo { get; set; }
            public string LockerColor { get; set; }
            public string LockDesc { get; set; }
            public decimal HrRate { get; set; }
            public decimal DyRate { get; set; }
            public decimal MnRate { get; set; }
            public long CreatedBy { get; set; }
            public DateTime CreatedDateTime { get; set; }
            public long ModifiedBy { get; set; }
            public DateTime ModifiedDateTime { get; set; }
            public long Status { get; set; }
        public long BranchId { get; set; }
        public int Hired { get; set; }
        

    }
    
}

