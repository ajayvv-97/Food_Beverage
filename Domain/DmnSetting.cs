using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnSetting
    {
        public int SId { get; set; }
        public string APPColor { get; set; }
        public string MenuButtonColor { get; set; }
        public string OperationButtonColor { get; set; }
        public string Description { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }


}
