using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnSaleBillHeaders_PLocker
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int LocId { get; set; }
        public string GSTNo { get; set; }
        public string barcode_img_path { get; set; }
        public string EntryNo { get; set; }
    }
}
