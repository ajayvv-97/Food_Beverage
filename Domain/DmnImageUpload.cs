using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnImageUpload
    {
        public int PpId { get; set; }
        public int ProductId { get; set; }
        public byte[] Photo { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int Status { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Inseration_Type { get; set; }
    }
}
