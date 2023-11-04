using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{

    public class DmnVendor
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }

        public string Email { get; set; }
        public string Description { get; set; }

        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }


}
