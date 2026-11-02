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
                List<InventoryRequestComboItem> departments;
                List<InventoryRequestComboItem> employees;

                using (IDbConnection connection =
                    DAL.DapperRepository<InventoryRequestComboItem>.OpenConnection())
                {
                    departments = connection.Query<InventoryRequestComboItem>(
                        @"SELECT DepartmentId AS Id, DepartmentName AS Name
                          FROM dbo.tblDepartment
                          WHERE Status = 1
                          ORDER BY DepartmentName").ToList();

                    employees = connection.Query<InventoryRequestComboItem>(
                        @"SELECT EmployeeId AS Id, EmployeeName AS Name
                          FROM dbo.tblEmployee
                          WHERE Status = 1
                          ORDER BY EmployeeName").ToList();
                }

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

                // Restore the currently logged-in employee and department.
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

            decimal directRawQty = dgv_raw.Rows.Cast<DataGridViewRow>()
                .Where(x => !x.IsNewRow)
                .Sum(x => ToDecimal(x.Cells[Rissuedqtyno].Value));

            decimal recipeRawQty = 0m;
            foreach (DataGridViewRow productRow in dgv_batch.Rows)
            {
                if (productRow.IsNewRow || productRow.Cells[Bitemidno].Value == null) continue;
                int productId = Convert.ToInt32(productRow.Cells[Bitemidno].Value);
                decimal plannedQty = ToDecimal(productRow.Cells[Bplannedqtyno].Value);

                if (productId == _visibleIngredientProductId)
                {
                    recipeRawQty += dgv_batch_ingrediant.Rows.Cast<DataGridViewRow>()
                        .Where(x => !x.IsNewRow)
                        .Sum(x => ToDecimal(x.Cells[BItotalrequiredqtyno].Value));
                }
                else if (_loadedRequestDetails != null &&
                    _loadedRequestDetails.Any(x => x.RequestType == 1 && x.ProductId == productId))
                {
                    recipeRawQty += _loadedRequestDetails
                        .Where(x => x.RequestType == 1 && x.ProductId == productId && x.ItemId.HasValue)
                        .Sum(x => (x.PerPortionQty ?? 0m) * plannedQty);
                }
                else
                {
                    recipeRawQty += LoadProductIngredients(productId, plannedQty)
                        .Sum(x => x.TotalRequiredQty);
                }
            }

            decimal rawQty = directRawQty + recipeRawQty;
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
            DmnInventoryRequestMaster master;
            List<DmnInventoryRequestDetail> details;

            using (IDbConnection connection = DAL.DapperRepository<DmnInventoryRequestMaster>.OpenConnection())
            {
                master = connection.QueryFirstOrDefault<DmnInventoryRequestMaster>(
                    @"SELECT InventoryRequestId AS RequestId, RequestNo, RequestDate, RequiredDate,
                             DepartmentId, EmployeeId, Remarks, RequestType, CreatedBy, CreatedOn,
                             ModifiedBy, ModifiedOn, BranchId, Status
                      FROM dbo.tblInventoryRequestMaster
                      WHERE InventoryRequestId = @RequestId AND ISNULL(Status,1) = 1
                        AND BranchId = @BranchId",
                    new { RequestId = requestId, BranchId = FrmDashboard.BranchId });

                details = connection.Query<DmnInventoryRequestDetail>(
                    @"SELECT d.InventoryRequestDetailId, d.InventoryRequestId, d.RequestType, d.SlNo,
                             d.ProductId, d.PlannedQty, d.RecipeItemId, d.PerPortionQty,
                             d.TotalRequiredQty, d.ItemId, d.ProductType, d.CurrentStock,
                             d.IssuedQty, d.ProjectedBalance, d.Unit, d.PurposeRemarks,
                             d.CreatedBy, d.CreatedOn, d.ModifiedBy, d.ModifiedOn, d.BranchId, d.Status,
                             p.Product AS ProductName, i.Itemname AS ItemName, u.UnitName
                      FROM dbo.tblInventoryRequestDetails d
                      LEFT JOIN dbo.tblProduct p ON p.ProductId = d.ProductId
                      LEFT JOIN dbo.tblItem i ON i.ItemId = d.ItemId
                      LEFT JOIN dbo.tblUnit u ON u.UId = d.Unit
                      WHERE d.InventoryRequestId = @RequestId AND ISNULL(d.Status,1) = 1
                      ORDER BY d.RequestType, d.SlNo, d.InventoryRequestDetailId",
                    new { RequestId = requestId }).ToList();
            }

            if (master == null)
            {
                ShowError("The selected inventory request was not found or is inactive.");
                return;
            }

            _currentRequestId = requestId;
            _currentDraftId = null;
            btn_deletesaved_draft.Visible = false;
            PopulateRequestForm(master, details);
        }

        private void FillDraftData(int draftId)
        {
            DmnInventoryRequestMaster master;
            List<DmnInventoryRequestDetail> details;

            using (IDbConnection connection = DAL.DapperRepository<DmnInventoryRequestMaster>.OpenConnection())
            {
                master = connection.QueryFirstOrDefault<DmnInventoryRequestMaster>(
                    @"SELECT DraftId AS RequestId, RequestNo, RequestDate, RequiredDate,
                             DepartmentId, EmployeeId, Remarks, RequestType, CreatedBy, CreatedOn,
                             ModifiedBy, ModifiedOn, BranchId, Status
                      FROM dbo.tblInventoryRequestDraftMaster
                      WHERE DraftId = @DraftId AND ISNULL(Status,1) = 1
                        AND BranchId = @BranchId",
                    new { DraftId = draftId, BranchId = FrmDashboard.BranchId });

                details = connection.Query<DmnInventoryRequestDetail>(
                    @"SELECT d.DraftDetailId AS InventoryRequestDetailId,
                             d.DraftId AS InventoryRequestId, d.RequestType, d.SlNo,
                             d.ProductId, d.PlannedQty, d.RecipeItemId, d.PerPortionQty,
                             d.TotalRequiredQty, d.ItemId, d.ProductType, d.CurrentStock,
                             d.IssuedQty, d.ProjectedBalance, d.Unit, d.PurposeRemarks,
                             d.CreatedBy, d.CreatedOn, d.ModifiedBy, d.ModifiedOn, d.BranchId, d.Status,
                             p.Product AS ProductName, i.Itemname AS ItemName, u.UnitName
                      FROM dbo.tblInventoryRequestDraftDetails d
                      LEFT JOIN dbo.tblProduct p ON p.ProductId = d.ProductId
                      LEFT JOIN dbo.tblItem i ON i.ItemId = d.ItemId
                      LEFT JOIN dbo.tblUnit u ON u.UId = d.Unit
                      WHERE d.DraftId = @DraftId AND ISNULL(d.Status,1) = 1
                      ORDER BY d.RequestType, d.SlNo, d.DraftDetailId",
                    new { DraftId = draftId }).ToList();
            }

            if (master == null)
            {
                ShowError("The selected draft was not found or is inactive.");
                return;
            }

            _currentDraftId = draftId;
            _currentRequestId = null;
            PopulateRequestForm(master, details);
            btn_deletesaved_draft.Visible = true;
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
                row.Cells[Runitno].Value = detail.Unit.HasValue ? detail.Unit.Value : (object)DBNull.Value;
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