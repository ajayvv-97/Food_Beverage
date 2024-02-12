using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnHRegistration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string GstNo { get; set; }
        public int ZipCode { get; set; }
        public string Certificate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int BranchId { get; set; }
        public int Status { get; set; }
        public string Certificate1 { get; set; }
        public string Certificate2 { get; set; }
        public int BookingInerwell { get; set; }
        public string website { get; set; }


    }
}
