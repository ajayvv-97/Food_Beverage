using System;

namespace Domain
{
    public class DmnInventoryRequestMaster
    {
        public int RequestId { get; set; }
        public int InventoryRequestId { get; set; }
        public int DraftId { get; set; }
        public string DraftNo { get; set; }
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
        public int ApprovalStatus { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public string ApprovalRemarks { get; set; }
    }

    public class DmnInventoryRequestItem
    {
        public int RequestDetailId { get; set; }
        public int RequestId { get; set; }
        public int RequestType { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal PlannedQty { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal IssuedQty { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
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
        public int InventoryRequestDetailId { get; set; }
        public long ItemId { get; set; }
        public string IngredientName { get; set; }
        public decimal PerPortionQty { get; set; }
        public decimal TotalRequiredQty { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal ProjectedBalance { get; set; }
        public int ProductId { get; set; }
    }

    // Detail row matching dbo.tblInventoryRequestDetails.
    // Batch products are stored with their recipe ingredient rows; raw issues
    // are stored as RequestType = 2 rows.
    public class DmnInventoryRequestDetail
    {
        public int InventoryRequestDetailId { get; set; }
        public int DraftDetailId { get; set; }
        public int DraftId { get; set; }
        public int InventoryRequestId { get; set; }
        public int RequestType { get; set; }
        public int? SlNo { get; set; }
        public int? ProductId { get; set; }
        public decimal? PlannedQty { get; set; }
        public long? RecipeItemId { get; set; }
        public decimal? PerPortionQty { get; set; }
        public decimal? TotalRequiredQty { get; set; }
        public long? ItemId { get; set; }
        public int? ProductType { get; set; }
        public decimal? CurrentStock { get; set; }
        public decimal? IssuedQty { get; set; }
        public decimal? ProjectedBalance { get; set; }
        public int? Unit { get; set; }
        public string PurposeRemarks { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? BranchId { get; set; }
        public int? Status { get; set; }

        // Lookup-only properties used when a saved request is loaded into the grids.
        public string ProductName { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
    }
}