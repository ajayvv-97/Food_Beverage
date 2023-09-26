using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnSalesInvoiceBillDetails_Plocker
    {        
        public int LockID { get; set; }
        public int LockHireID { get; set; }
        public int AccNo { get; set; }
        public string AccPhone { get; set; }
        public string AccEmail { get; set; }
        public decimal NetAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public int PaymentMode { get; set; }

        public decimal Rate { get; set; }
        public decimal Amt { get; set; }
        public DateTime ExpDate { get; set; }
        public string Hiredetail { get; set; }
        public DateTime HireTime { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime ExpTime { get; set; }
        public DateTime EntryDate { get; set; }
        public string Locker { get; set; }
        public decimal PaidAmount { get; set; }
        public string AccName { get; set; }
        public string EntryNo { get; set; }
        
    }
}
