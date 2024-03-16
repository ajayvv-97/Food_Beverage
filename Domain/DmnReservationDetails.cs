using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnReservationDetails : DmnOrderDetails
    {
        public int MTID { get; set; }
        public int BookdetailId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public int BranchId { get; set; }
        public int Rid { get; set; }
        public int modifiedby { get; set; }
        public int Status { get; set; }
        public int Branchid { get; set; }
        public long BookmasterId { get; set; }
        public decimal Net { get; set; }
        public string Tax { get; set; }
        public string TaxType { get; set; }
        public string TaxAmt { get; set; }
        public decimal IgstPrcnt { get; set; }
        public decimal CgstPrcnt { get; set; }
        public decimal SgstPrcnt { get; set; }
        public decimal VatPrcnt { get; set; }
        public decimal TaxPer { get; set; }
        public string RemoveNotes { get; set; }
        

    }
}
