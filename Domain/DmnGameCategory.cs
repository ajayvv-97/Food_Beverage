using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public  class DmnGameCategory
    {
        public int Id { get; set; }
        public string Gcategory { get; set; }
        public string gDesc { get; set; }
        public long Createdby { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime modifiedDatetime { get; set; }

    }
}
