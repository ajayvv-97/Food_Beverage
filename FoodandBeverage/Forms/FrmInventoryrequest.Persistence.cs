using BLL;
using CustomMessageBox;
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;

namespace FoodandBeverage.Forms
{
    public partial class FrmInventoryrequest
    {

        private sealed class InventoryRequestComboItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private void LoadRequestComboData()
        {
            try
            {
                BllSearch<InventoryRequestComboItem> bllSearch =
                    new BllSearch<InventoryRequestComboItem>();

                DmnSearch departmentSearch = new DmnSearch
                {
                    Select = "DepartmentId AS Id, DepartmentName AS Name",
                    Table = "tblDepartment",
                    WhereNdGroupBy = "where Status = 1",
                    OrderBy = "ORDER BY DepartmentName"
                };

                DmnSearch employeeSearch = new DmnSearch
                {
                    Select = "EmployeeId AS Id, EmployeeName AS Name",
                    Table = "tblEmployee",
                    WhereNdGroupBy = "where Status = 1",
                    OrderBy = "ORDER BY EmployeeName"
                };

                List<InventoryRequestComboItem> departments =
                    bllSearch.GetSearchData("prdSearch", departmentSearch) ??
                    new List<InventoryRequestComboItem>();
                List<InventoryRequestComboItem> employees =
                    bllSearch.GetSearchData("prdSearch", employeeSearch) ??
                    new List<InventoryRequestComboItem>();

                cmb_req_department.DataSource = null;
                cmb_req_department.DisplayMember = "Name";
                cmb_req_department.ValueMember = "Id";
                cmb_req_department.DataSource = departments;
                cmb_req_department.SelectedIndex = -1;

                cmb_req_employee.DataSource = null;
                cmb_req_employee.DisplayMember = "Name";
                cmb_req_employee.ValueMember = "Id";
                cmb_req_employee.DataSource = employees;
                cmb_req_employee.SelectedIndex = -1;

                if (FrmLogin.EmployeeId > 0 &&
                    employees.Any(x => x.Id == FrmLogin.EmployeeId))
                {
                    cmb_req_employee.SelectedValue = FrmLogin.EmployeeId;
                }

                if (FrmLogin.DepartmentId > 0 &&
                    departments.Any(x => x.Id == FrmLogin.DepartmentId))
                {
                    cmb_req_department.SelectedValue = FrmLogin.DepartmentId;
                }

                UpdateRequesterName();
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest LoadRequestComboData", ex);
                ShowError("Unable to load department and employee details. " + ex.Message);
            }
        }

        private void cmb_req_employee_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRequesterName();
        }

        private void UpdateRequesterName()
        {
            string employeeName = cmb_req_employee == null ? string.Empty : cmb_req_employee.Text.Trim();
            if (string.IsNullOrWhiteSpace(employeeName) && FrmLogin.EmployeName != null)
                employeeName = FrmLogin.EmployeName;
            txt_requ_by.Text = employeeName;
        }

        private void UpdateRequestTotals()
        {
            if (dgv_batch == null || dgv_raw == null) return;
            CacheVisibleIngredientRows();

            decimal foodQty = dgv_batch.Rows.Cast<DataGridViewRow>()
                .Where(x => !x.IsNewRow)
                .Sum(x => ToDecimal(x.Cells[Bplannedqtyno].Value));

            // Raw material total is based only on manually added rows in dgv_raw.
            // Batch product recipe ingredients are not included in this total.
            decimal rawQty = dgv_raw.Rows.Cast<DataGridViewRow>()
                .Where(x => !x.IsNewRow && x.Cells[Ritemidno].Value != null)
                .Sum(x => ToDecimal(x.Cells[Rissuedqtyno].Value));
            txt_batchfoodqty.Text = foodQty.ToString("0.####");
            txt_totalrawmaterialqty.Text = rawQty.ToString("0.####");
            total_food_raw_qty.Text = (foodQty + rawQty).ToString("0.####");
        }

        private static int? ToNullableInt(object value)
        {
            if (value == null || value == DBNull.Value) return null;
            int result;
            return int.TryParse(Convert.ToString(value), out result) ? (int?)result : null;
        }

        private static void SetComboValue(ComboBox combo, int? value)
        {
            if (combo == null) return;
            try
            {
                if (value.HasValue)
                {
                    combo.SelectedValue = value.Value;
                    if (combo.SelectedValue == null || combo.SelectedValue == DBNull.Value)
                        combo.Text = value.Value.ToString();
                }
                else
                {
                    combo.SelectedIndex = -1;
                    combo.Text = string.Empty;
                }
            }
            catch
            {
                combo.Text = value.HasValue ? value.Value.ToString() : string.Empty;
            }
        }

        private void FillData(int requestId)
        {
            BllSearch<DmnInventoryRequestMaster> bllSearchMaster =
                new BllSearch<DmnInventoryRequestMaster>();
            DmnSearch masterSearch = new DmnSearch
            {
                Select = "InventoryRequestId AS RequestId, RequestNo, RequestDate, RequiredDate, " +
                         "DepartmentId, EmployeeId, Remarks, RequestType, CreatedBy, CreatedOn, " +
                         "ModifiedBy, ModifiedOn, BranchId, Status, ISNULL(ApprovalStatus,0) AS ApprovalStatus, ApprovedBy, ApprovedOn, ApprovalRemarks",
                Table = "tblInventoryRequestMaster",
                WhereNdGroupBy = "where InventoryRequestId = " + requestId +
                                 " AND ISNULL(Status,1) = 1 AND BranchId = " +
                                 Convert.ToString(FrmDashboard.BranchId),
                OrderBy = ""
            };

            List<DmnInventoryRequestMaster> masterRows =
                bllSearchMaster.GetSearchData("prdSearch", masterSearch);
            DmnInventoryRequestMaster master =
                masterRows != null && masterRows.Count > 0 ? masterRows[0] : null;

            if (master == null)
            {
                ShowError("The selected inventory request was not found or is inactive.");
                return;
            }

            BllSearch<DmnInventoryRequestDetail> bllSearchDetails =
                new BllSearch<DmnInventoryRequestDetail>();
            DmnSearch detailSearch = new DmnSearch
            {
                Select = "d.InventoryRequestDetailId, d.InventoryRequestId, d.RequestType, d.SlNo, " +
                         "d.ProductId, d.PlannedQty, d.RecipeItemId, d.PerPortionQty, " +
                         "d.TotalRequiredQty, d.ItemId, d.ProductType, d.CurrentStock, " +
                         "d.IssuedQty, d.ProjectedBalance, d.Unit, d.PurposeRemarks, " +
                         "d.CreatedBy, d.CreatedOn, d.ModifiedBy, d.ModifiedOn, d.BranchId, d.Status, " +
                         "i.Itemname AS ItemName, u.UnitName AS UnitName",
                Table = "tblInventoryRequestDetails d " +
                        "LEFT JOIN tblItem i ON i.ItemId = d.ItemId " +
                        "LEFT JOIN tblUnit u ON u.UId = d.Unit",
                WhereNdGroupBy = "where d.InventoryRequestId = " + requestId +
                                 " AND ISNULL(d.Status,1) = 1",
                OrderBy = "ORDER BY d.RequestType, d.SlNo, d.InventoryRequestDetailId"
            };

            List<DmnInventoryRequestDetail> details =
                bllSearchDetails.GetSearchData("prdSearch", detailSearch) ??
                new List<DmnInventoryRequestDetail>();

            LoadBatchProductNames(details);

            _currentRequestId = requestId;
            _currentDraftId = null;
            _currentApprovalStatus = master.ApprovalStatus;
            btn_deletesaved_draft.Visible = false;
            PopulateRequestForm(master, details);
        }

        private void LoadBatchProductNames(List<DmnInventoryRequestDetail> details)
        {
            if (details == null || details.Count == 0)
                return;

            BllSearch<DmnProducts> bllSearchproduct =
                new BllSearch<DmnProducts>();

            List<int> productIds = details
                .Where(x => x.RequestType == 1 && x.ProductId.HasValue)
                .Select(x => x.ProductId.Value)
                .Distinct()
                .ToList();

            foreach (int Pid in productIds)
            {
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblProduct";
                objSearch.WhereNdGroupBy = "where status=1 and ProductId = " + Pid;
                objSearch.OrderBy = "";

                var gridSource = bllSearchproduct.GetSearchData("prdSearch", objSearch);
                if (gridSource != null && gridSource.Count > 0)
                {
                    string productName = gridSource[0].Product;
                    foreach (DmnInventoryRequestDetail detail in details.Where(
                        x => x.RequestType == 1 && x.ProductId == Pid))
                    {
                        detail.ProductName = productName;
                    }
                }
            }
        }

        private void FillDraftData(int draftId)
        {
            BllSearch<DmnInventoryRequestMaster> bllSearchMaster =
                new BllSearch<DmnInventoryRequestMaster>();
            DmnSearch masterSearch = new DmnSearch
            {
                Select = "DraftId AS RequestId, RequestNo, RequestDate, RequiredDate, " +
                         "DepartmentId, EmployeeId, Remarks, RequestType, CreatedBy, CreatedOn, " +
                         "ModifiedBy, ModifiedOn, BranchId, Status",
                Table = "tblInventoryRequestDraftMaster",
                WhereNdGroupBy = "where DraftId = " + draftId +
                                 " AND ISNULL(Status,1) = 1 AND BranchId = " +
                                 Convert.ToString(FrmDashboard.BranchId),
                OrderBy = ""
            };

            List<DmnInventoryRequestMaster> masterRows =
                bllSearchMaster.GetSearchData("prdSearch", masterSearch);
            DmnInventoryRequestMaster master =
                masterRows != null && masterRows.Count > 0 ? masterRows[0] : null;

            if (master == null)
            {
                ShowError("The selected draft was not found or is inactive.");
                return;
            }

            BllSearch<DmnInventoryRequestDetail> bllSearchDetails =
                new BllSearch<DmnInventoryRequestDetail>();
            DmnSearch detailSearch = new DmnSearch
            {
                Select = "CAST(0 AS INT) AS InventoryRequestDetailId, " +
                         "d.DraftDetailId AS DraftDetailId, " +
                         "d.DraftId AS InventoryRequestId, d.RequestType, d.SlNo, " +
                         "d.ProductId, d.PlannedQty, d.RecipeItemId, d.PerPortionQty, " +
                         "d.TotalRequiredQty, d.ItemId, d.ProductType, d.CurrentStock, " +
                         "d.IssuedQty, d.ProjectedBalance, d.Unit, d.PurposeRemarks, " +
                         "d.CreatedBy, d.CreatedOn, d.ModifiedBy, d.ModifiedOn, d.BranchId, d.Status, " +
                         "i.Itemname AS ItemName, u.UnitName AS UnitName",
                Table = "tblInventoryRequestDraftDetails d " +
                        "LEFT JOIN tblItem i ON i.ItemId = d.ItemId " +
                        "LEFT JOIN tblUnit u ON u.UId = d.Unit",
                WhereNdGroupBy = "where d.DraftId = " + draftId +
                                 " AND ISNULL(d.Status,1) = 1",
                OrderBy = "ORDER BY d.RequestType, d.SlNo, d.DraftDetailId"
            };

            List<DmnInventoryRequestDetail> details =
                bllSearchDetails.GetSearchData("prdSearch", detailSearch) ??
                new List<DmnInventoryRequestDetail>();

            LoadBatchProductNames(details);

            _currentDraftId = draftId;
            _currentRequestId = null;
            _currentApprovalStatus = 0;
            PopulateRequestForm(master, details);
            btn_deletesaved_draft.Visible = true;
        }

        private List<DmnInventoryRequestDetail> NormalizeLoadedRequestDetails(
            IEnumerable<DmnInventoryRequestDetail> source)
        {
            if (source == null)
                return new List<DmnInventoryRequestDetail>();

            // Keep the newest persisted row for each logical batch ingredient/raw row.
            return source
                .Where(x => x != null)
                .GroupBy(x => x.RequestType == 1
                    ? "B|" + Convert.ToString(x.ProductId ?? 0) + "|" +
                      Convert.ToString(x.RecipeItemId ?? 0) + "|" + Convert.ToString(x.ItemId ?? 0)
                    : x.RequestType == 2
                        ? "R|" + Convert.ToString(x.ItemId ?? 0) + "|" + Convert.ToString(x.Unit ?? 0)
                        : "D|" + Convert.ToString(x.InventoryRequestDetailId))
                .Select(group => group
                    .OrderByDescending(x => x.InventoryRequestDetailId)
                    .ThenByDescending(x => x.ModifiedOn ?? x.CreatedOn ?? DateTime.MinValue)
                    .First())
                .OrderBy(x => x.RequestType)
                .ThenBy(x => x.SlNo ?? int.MaxValue)
                .ThenBy(x => x.InventoryRequestDetailId)
                .ToList();
        }

        private void PopulateRequestForm(DmnInventoryRequestMaster master, List<DmnInventoryRequestDetail> details)
        {
            dgv_batch.Rows.Clear();
            dgv_batch_ingrediant.Rows.Clear();
            dgv_raw.Rows.Clear();
            _originalIngredientQuantities.Clear();
            _visibleIngredientProductId = 0;
            _loadedRequestDetails = details ?? new List<DmnInventoryRequestDetail>();

            txt_reqno.Text = master.RequestNo ?? string.Empty;
            dtp_reqdate.Value = master.RequestDate == DateTime.MinValue ? DateTime.Today : master.RequestDate;
            dtp_required_date.Value = master.RequiredDate == DateTime.MinValue ? DateTime.Today : master.RequiredDate;
            txtremarks.Text = master.Remarks ?? string.Empty;
            SetComboValue(cmb_req_department, master.DepartmentId);
            SetComboValue(cmb_req_employee, master.EmployeeId);
            UpdateRequesterName();

            var products = _loadedRequestDetails
                .Where(x => x.RequestType == 1 && x.ProductId.HasValue)
                .GroupBy(x => x.ProductId.Value)
                .OrderBy(x => x.Min(y => y.SlNo ?? int.MaxValue))
                .ToList();

            foreach (var product in products)
            {
                DmnInventoryRequestDetail first = product.First();
                int index = dgv_batch.Rows.Add();
                DataGridViewRow row = dgv_batch.Rows[index];
                row.Cells[Bslno].Value = index + 1;
                row.Cells[Bmenuitemno].Value = string.IsNullOrWhiteSpace(first.ProductName)
                    ? "Product " + product.Key : first.ProductName;
                row.Cells[Bplannedqtyno].Value = first.PlannedQty ?? 0m;
                row.Cells[Bunitno].Value = string.Empty;
                row.Cells[Brecipeingredientsno].Value = "View ingredients";
                row.Cells[Bactionno].Value = Properties.Resources.food_cancel_icon;
                row.Cells[Bitemidno].Value = product.Key;
            }

            foreach (DmnInventoryRequestDetail detail in _loadedRequestDetails
                .Where(x => x.RequestType == 2 && x.ItemId.HasValue).OrderBy(x => x.SlNo))
            {
                int index = dgv_raw.Rows.Add();
                DataGridViewRow row = dgv_raw.Rows[index];
                row.Cells[Rslno].Value = index + 1;
                row.Cells[Rrawmaterialno].Value = detail.ItemName ?? ("Raw Material " + detail.ItemId.Value);
                row.Cells[Rcurrentstockno].Value = detail.CurrentStock ?? 0m;
                row.Cells[Rissuedqtyno].Value = detail.IssuedQty ?? 0m;
                row.Cells[Runitno].Value = detail.UnitName ?? string.Empty;
                row.Cells["rawUnitId"].Value = detail.Unit.HasValue
                    ? (object)detail.Unit.Value
                    : DBNull.Value;
                row.Cells[Rprojectbalanceno].Value = detail.ProjectedBalance ?? 0m;
                row.Cells[RpurposeRemarksno].Value = detail.PurposeRemarks ?? string.Empty;
                row.Cells[Ractionno].Value = Properties.Resources.food_cancel_icon;
                row.Cells[Ritemidno].Value = detail.ItemId.Value;
                row.Cells[Rproducttypeno].Value = detail.ProductType ?? 2;
            }

            if (dgv_batch.Rows.Count > 0)
            {
                dgv_batch.CurrentCell = dgv_batch.Rows[0].Cells[Bmenuitemno];
                LoadIngredientsForRow(dgv_batch.Rows[0]);
            }

            RenumberRows(dgv_batch, Bslno);
            RenumberRows(dgv_raw, Rslno);
            UpdateRequestTotals();
        }

        private int SelectDraftId()
        {
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            DataTable dtColumnAlign = new DataTable();
            dtColumnAlign.Columns.Add();

            DmnSearch searchCriteria = new DmnSearch();
            searchCriteria.ColumnHeaders = "ID,Draft No,Request No,Request Date,Required Date";
            searchCriteria.Select =
                "DraftId as Id,DraftNo as Field1,RequestNo as Field2," +
                "CAST(RequestDate AS varchar(30)) as Field3,CAST(RequiredDate AS varchar(30)) as Field4";
            searchCriteria.Table = "tblInventoryRequestDraftMaster";
            searchCriteria.WhereNdGroupBy =
                "where Status = 1 AND BranchId = " + Convert.ToString(FrmDashboard.BranchId);
            searchCriteria.OrderBy = " ORDER BY DraftId DESC";

            List<DmnGridFields> gridSource = bllSearch.GetSearchData("prdSearch", searchCriteria);
            if (gridSource == null || gridSource.Count == 0)
            {
                CMessageBox.Show("No saved drafts found.", "Empty",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }

            using (var search = new FrmSearch<DmnGridFields>(
                searchCriteria, SearchMode.FirstCharacter, this.Width - 150,
                this.Height - 150, this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))
            {
                if (search.ShowDialog() == DialogResult.OK)
                    return Convert.ToInt32(search._retValue);
            }
            return 0;
        }

        private void OpenDraftWorkflow()
        {
            try
            {
                int draftId = SelectDraftId();
                if (draftId > 0) FillDraftData(draftId);
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_opendraft_Click", ex);
                ShowError("Unable to open the selected draft. " + ex.Message);
            }
        }


    }
}