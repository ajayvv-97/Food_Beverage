using System;

namespace Domain
{
    public class DmnAddonGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public int SelectionMode { get; set; }
        public int MinSelection { get; set; }
        public int MaxSelection { get; set; }
        public bool IsRequired { get; set; }
        public bool Status { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int ProductCount { get; set; }
    }

    public class DmnAddonGroupProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ItemCode { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public int DisplayOrder { get; set; }
        public int AddonGroupID { get; set; }
    }
}
