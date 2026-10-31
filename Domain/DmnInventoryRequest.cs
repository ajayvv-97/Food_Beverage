using System;

namespace Domain
{
    public class DmnInventoryRequestMaster
    {
        public int RequestId { get; set; }
        public string RequestNo { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? EmployeeId { get; set; }
        public string Remarks { get; set; }
        public int RequestType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? BranchId { get; set; }
        public int Status { get; set; }
    }

    public class DmnInventoryRequestItem
    {
        public int RequestDetailId { get; set; }
        public int RequestId { get; set; }
        public int RequestType { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal PlannedQty { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal IssuedQty { get; set; }
        public int? UnitId { get; set; }
        public decimal ProjectBalance { get; set; }
        public string Purpose { get; set; }
        public int ProductType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? BranchId { get; set; }
        public int Status { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class DmnInventoryRequestIngredient
    {
        public int IngredientDetailId { get; set; }
        public int ItemId { get; set; }
        public string IngredientName { get; set; }
        public decimal PerPortionQty { get; set; }
        public decimal TotalRequiredQty { get; set; }
        public int? UnitId { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal ProjectedBalance { get; set; }
        public int ProductId { get; set; }
    }
}