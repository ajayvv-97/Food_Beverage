using BLL;
using Dapper;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmInventoryrequest : Form
    {
        private readonly BllInventoryRequest _inventoryBll = new BllInventoryRequest();
        private bool _loadingGrid;
        private int _activeRequestType = 1; // 1 = batch preparation, 2 = raw material

        ClsGeneral clsgen = new ClsGeneral();

        #region Database Search Helpers
        private List<DmnInventoryRequestItem> SearchBatchProducts(string search)
        {
            using (IDbConnection connection = DAL.DapperRepository<DmnInventoryRequestItem>.OpenConnection())
            {
                return connection.Query<DmnInventoryRequestItem>(
                    @"SELECT p.ProductId AS ItemId, p.Product AS ItemName,
                             CAST(0 AS DECIMAL(18,2)) AS CurrentStock, CAST(1 AS INT) AS ProductType
                      FROM dbo.tblProduct p
                      WHERE ISNULL(p.Status,1) <> 0
                        AND (@Search = '' OR p.Product LIKE '%' + @Search + '%')
                      ORDER BY p.Product",
                    new { Search = search ?? string.Empty }).ToList();
            }
        }

        private List<DmnInventoryRequestItem> SearchRawMaterialItems(string search)
        {
            using (IDbConnection connection = DAL.DapperRepository<DmnInventoryRequestItem>.OpenConnection())
            {
                return connection.Query<DmnInventoryRequestItem>(
                    @"SELECT CAST(i.ItemId AS INT) AS ItemId, i.Itemname AS ItemName,
                             ISNULL(pr.QOH,0) AS CurrentStock, i.unit AS UnitId, CAST(2 AS INT) AS ProductType
                      FROM dbo.tblItem i
                      OUTER APPLY
                      (
                          SELECT TOP (1) r.QOH FROM dbo.tblProductRate r
                          WHERE r.ProductId = i.ItemId AND ISNULL(r.ProductType,0) = 0
                          ORDER BY r.Id DESC
                      ) pr
                      WHERE ISNULL(i.Saletype,0) = 0 AND ISNULL(i.Status,1) <> 0
                        AND (@Search = '' OR i.Itemname LIKE '%' + @Search + '%')
                      ORDER BY i.Itemname",
                    new { Search = search ?? string.Empty }).ToList();
            }
        }

        private List<DmnInventoryRequestIngredient> LoadProductIngredients(int productId, decimal plannedQty)
        {
            using (IDbConnection connection = DAL.DapperRepository<DmnInventoryRequestIngredient>.OpenConnection())
            {
                return connection.Query<DmnInventoryRequestIngredient>(
                    @"SELECT d.Ing_DetailsId AS IngredientDetailId, d.ItemId, i.Itemname AS IngredientName,
                             ISNULL(d.quantity,0) AS PerPortionQty,
                             ISNULL(d.quantity,0) * @PlannedQty AS TotalRequiredQty, d.Unit AS UnitId,
                             ISNULL(pr.QOH,0) AS CurrentStock,
                             ISNULL(pr.QOH,0) - ISNULL(d.quantity,0) * @PlannedQty AS ProjectedBalance,
                             d.ProductId
                      FROM dbo.tblIngredientDetails d
                      LEFT JOIN dbo.tblItem i ON i.ItemId = d.ItemId
                      OUTER APPLY
                      (
                          SELECT TOP (1) r.QOH FROM dbo.tblProductRate r
                          WHERE r.ProductId = d.ItemId AND ISNULL(r.ProductType,0) = 0
                          ORDER BY r.Id DESC
                      ) pr
                      WHERE d.ProductId = @ProductId AND ISNULL(d.Status,1) <> 0
                      ORDER BY d.Ing_DetailsId",
                    new { ProductId = productId, PlannedQty = plannedQty }).ToList();
            }
        }
        #endregion

        #region Grid Declaration
        // Batch product grid
        public int Bslno = 0;
        public int Bmenuitemno = 1;
        public int Bplannedqtyno = 2;
        public int Bunitno = 3;
        public int Brecipeingredientsno = 4;
        public int Bactionno = 5;
        public int Bitemidno = 6;

        // Batch ingredient grid
        public int BIslno = 0;
        public int BIingredientno = 1;
        public int Biperportionqtyno = 2;
        public int BItotalrequiredqtyno = 3;
        public int BIunitno = 4;
        public int BIcurrentstockno = 5;
        public int BIprojectedbalanceno = 6;
        public int BIproductidno = 7;

        // Raw material grid
        public int Rslno = 0;
        public int Rrawmaterialno = 1;
        public int Rcurrentstockno = 2;
        public int Rissuedqtyno = 3;
        public int Runitno = 4;
        public int Rprojectbalanceno = 5;
        public int RpurposeRemarksno = 6;
        public int Ractionno = 7;
        public int Ritemidno = 8;
        public int Rproducttypeno = 9;
        #endregion

        public FrmInventoryrequest()
        {
            InitializeComponent();
            ConfigureRequestGrids();
            InitializeRequestHeader();
            ShowRequestTab(1);
            dgv_batch.CellValueChanged += dgv_batch_CellValueChanged;
            dgv_batch.CurrentCellDirtyStateChanged += dgv_batch_CurrentCellDirtyStateChanged;
            dgv_raw.CellValueChanged += dgv_raw_CellValueChanged;
            txtsearch_batch.KeyDown += txtsearch_batch_KeyDown;
            txtsearch_raw.KeyDown += txtsearch_raw_KeyDown;
        }

        private void ConfigureRequestGrids()
        {
            dgv_batch.AllowUserToAddRows = false;
            dgv_batch.MultiSelect = false;
            dgv_batch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_batch.Columns[Bitemidno].Visible = false;
            dgv_batch.Columns[Bactionno].ReadOnly = true;
            dgv_batch.Columns[Brecipeingredientsno].ReadOnly = true;

            dgv_batch_ingrediant.AllowUserToAddRows = false;
            dgv_batch_ingrediant.ReadOnly = true;
            dgv_batch_ingrediant.MultiSelect = false;
            dgv_batch_ingrediant.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_batch_ingrediant.Columns[BIproductidno].Visible = false;

            dgv_raw.AllowUserToAddRows = false;
            dgv_raw.MultiSelect = false;
            dgv_raw.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_raw.Columns[Ritemidno].Visible = false;
            dgv_raw.Columns[Rproducttypeno].Visible = false;
            dgv_raw.Columns[Rcurrentstockno].ReadOnly = true;
            dgv_raw.Columns[Rprojectbalanceno].ReadOnly = true;
        }

        private void InitializeRequestHeader()
        {
            dtp_reqdate.Value = DateTime.Today;
            dtp_required_date.Value = DateTime.Today;
            txt_reqno.Text = "IR-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
            txt_reqno.ReadOnly = true;
        }

        #region Tab
        private void btn_batch_tab_Click(object sender, EventArgs e)
        {
            try
            {
                ShowRequestTab(1);
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_batch_tab_Click", ex);
            }
        }
        private void btn_raw_tab_Click(object sender, EventArgs e)
        {
            try
            {
                ShowRequestTab(2);
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_raw_tab_Click", ex);
            }
        }

        private void ShowRequestTab(int requestType)
        {
            _activeRequestType = requestType;
            if (requestType == 1)
            {
                tlp_Batchpreparation.BringToFront();
                btn_batch_tab.BackColor = Color.FromArgb(116, 68, 173);
                btn_batch_tab.ForeColor = Color.White;
                btn_raw_tab.BackColor = SystemColors.Control;
                btn_raw_tab.ForeColor = Color.Black;
            }
            else
            {
                tlp_raw.BringToFront();
                btn_raw_tab.BackColor = Color.FromArgb(116, 68, 173);
                btn_raw_tab.ForeColor = Color.White;
                btn_batch_tab.BackColor = SystemColors.Control;
                btn_batch_tab.ForeColor = Color.Black;
            }
        }
        #endregion

        #region Batch Preparation
        private void btn_clearall_batch_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_batch.Rows.Clear();
                dgv_batch_ingrediant.Rows.Clear();
                txtsearch_batch.Clear();
            }
            catch (Exception ex) { LogInventoryException("FrmInventoryrequest btn_clearall_batch_Click", ex); }
        }

        private void btn_importbatch_Click(object sender, EventArgs e)
        {
            try
            {
                SelectAndAddBatchProduct();
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_importbatch_Click", ex);
                ShowError("Unable to import batch products. " + ex.Message);
            }
        }
        private void btn_editingrediant_batch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_batch.CurrentRow == null) return;
                LoadIngredientsForCurrentBatchRow();
            }
            catch (Exception ex) { LogInventoryException("FrmInventoryrequest btn_editingrediant_batch_Click", ex); }
        }
        private void btn_advsearch_batch_Click(object sender, EventArgs e)
        {
            try
            {
                SelectAndAddBatchProduct();
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_advsearch_batch_Click", ex);
                ShowError("Unable to search batch products. " + ex.Message);
            }
        }
        private void btn_addtolist_batch_Click(object sender, EventArgs e)
        {
            try
            {
                SelectAndAddBatchProduct();
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_addtolist_batch_Click", ex);
                ShowError("Unable to add the selected batch product. " + ex.Message);
            }
        }

        private void SelectAndAddBatchProduct()
        {
            try
            {
                List<DmnInventoryRequestItem> products = SearchBatchProducts(txtsearch_batch.Text.Trim());
                DmnInventoryRequestItem selected = ShowItemPicker("Select batch preparation product", products);
                if (selected == null) return;

                foreach (DataGridViewRow existing in dgv_batch.Rows)
                {
                    if (Convert.ToInt32(existing.Cells[Bitemidno].Value) == selected.ItemId)
                    {
                        existing.Cells[Bplannedqtyno].Value = Math.Max(1m, ToDecimal(existing.Cells[Bplannedqtyno].Value) + 1m);
                        LoadIngredientsForRow(existing);
                        return;
                    }
                }

                int rowIndex = dgv_batch.Rows.Add();
                DataGridViewRow row = dgv_batch.Rows[rowIndex];
                row.Cells[Bslno].Value = rowIndex + 1;
                row.Cells[Bmenuitemno].Value = selected.ItemName;
                row.Cells[Bplannedqtyno].Value = 1m;
                row.Cells[Bunitno].Value = string.Empty;
                row.Cells[Brecipeingredientsno].Value = "View ingredients";
                row.Cells[Bactionno].Value = "Remove";
                row.Cells[Bitemidno].Value = selected.ItemId;
                LoadIngredientsForRow(row);
            }
            catch (Exception ex) { LogInventoryException("FrmInventoryrequest SelectAndAddBatchProduct", ex); ShowError("Unable to load batch products. " + ex.Message); }
        }

        private void LoadIngredientsForCurrentBatchRow()
        {
            try
            {
                if (dgv_batch.CurrentRow != null)
                {
                    LoadIngredientsForRow(dgv_batch.CurrentRow);
                }
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest LoadIngredientsForCurrentBatchRow", ex);
                throw;
            }
        }

        private void LoadIngredientsForRow(DataGridViewRow productRow)
        {
            if (productRow == null || productRow.IsNewRow) return;
            int productId = Convert.ToInt32(productRow.Cells[Bitemidno].Value);
            decimal plannedQty = Math.Max(0m, ToDecimal(productRow.Cells[Bplannedqtyno].Value));
            List<DmnInventoryRequestIngredient> ingredients = LoadProductIngredients(productId, plannedQty);

            _loadingGrid = true;
            try
            {
                dgv_batch_ingrediant.Rows.Clear();
                foreach (DmnInventoryRequestIngredient ingredient in ingredients)
                {
                    int index = dgv_batch_ingrediant.Rows.Add();
                    DataGridViewRow row = dgv_batch_ingrediant.Rows[index];
                    row.Cells[BIslno].Value = index + 1;
                    row.Cells[BIingredientno].Value = ingredient.IngredientName;
                    row.Cells[Biperportionqtyno].Value = ingredient.PerPortionQty;
                    row.Cells[BItotalrequiredqtyno].Value = ingredient.TotalRequiredQty;
                    row.Cells[BIunitno].Value = ingredient.UnitId;
                    row.Cells[BIcurrentstockno].Value = ingredient.CurrentStock;
                    row.Cells[BIprojectedbalanceno].Value = ingredient.ProjectedBalance;
                    row.Cells[BIproductidno].Value = ingredient.ProductId;
                }
            }
            finally { _loadingGrid = false; }
        }

        private void dgv_batch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == Bactionno)
            {
                dgv_batch.Rows.RemoveAt(e.RowIndex);
                RenumberRows(dgv_batch, Bslno);
                dgv_batch_ingrediant.Rows.Clear();
                return;
            }
            LoadIngredientsForRow(dgv_batch.Rows[e.RowIndex]);
        }

        private void dgv_batch_ingrediant_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Ingredient rows are display-only.
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest dgv_batch_ingrediant_CellClick", ex);
            }
        }

        private void dgv_batch_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_loadingGrid || e.RowIndex < 0 || e.ColumnIndex != Bplannedqtyno) return;
            if (dgv_batch.Rows[e.RowIndex].Cells[Bitemidno].Value == null) return;
            LoadIngredientsForRow(dgv_batch.Rows[e.RowIndex]);
        }

        private void dgv_batch_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgv_batch.IsCurrentCellDirty)
                {
                    dgv_batch.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest dgv_batch_CurrentCellDirtyStateChanged", ex);
            }
        }
        #endregion

        #region Main Buttons
        private void btn_saverequest_Click(object sender, EventArgs e)
        {
            try
            {
                List<DmnInventoryRequestItem> details = BuildRequestDetails();
                if (details.Count == 0)
                {
                    ShowError("Add at least one product before saving the request.");
                    return;
                }

                DmnInventoryRequestMaster master = new DmnInventoryRequestMaster
                {
                    RequestNo = txt_reqno.Text.Trim(),
                    RequestDate = dtp_reqdate.Value,
                    RequiredDate = dtp_required_date.Value,
                    DepartmentId = GetComboId(cmb_req_department),
                    EmployeeId = GetComboId(cmb_req_employee),
                    Remarks = txtremarks.Text.Trim(),
                    RequestType = details.Any(x => x.RequestType == 1) && details.Any(x => x.RequestType == 2) ? 3 : details[0].RequestType,
                    CreatedBy = GetComboId(cmb_req_employee) ?? 0,
                    BranchId = 0,
                    Status = 1
                };

                int requestId = _inventoryBll.Save(master, details);
                MessageBox.Show("Inventory request saved successfully. Request ID: " + requestId,
                    "Inventory Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetRequest();
            }
            catch (Exception ex) { LogInventoryException("FrmInventoryrequest btn_saverequest_Click", ex); ShowError("Unable to save inventory request. " + ex.Message); }
        }

        private List<DmnInventoryRequestItem> BuildRequestDetails()
        {
            // A request header can contain both preparation products and direct raw materials.
            // RequestType on each detail row identifies which grid the item came from.
            List<DmnInventoryRequestItem> details = new List<DmnInventoryRequestItem>();

            foreach (DataGridViewRow row in dgv_batch.Rows)
            {
                if (row.IsNewRow || row.Cells[Bitemidno].Value == null) continue;
                details.Add(new DmnInventoryRequestItem
                {
                    RequestType = 1,
                    ItemId = Convert.ToInt32(row.Cells[Bitemidno].Value),
                    ItemName = Convert.ToString(row.Cells[Bmenuitemno].Value),
                    PlannedQty = ToDecimal(row.Cells[Bplannedqtyno].Value),
                    CurrentStock = 0,
                    IssuedQty = 0,
                    UnitId = null,
                    ProjectBalance = 0,
                    Purpose = txtremarks.Text.Trim(),
                    ProductType = 1,
                    CreatedBy = GetComboId(cmb_req_employee) ?? 0,
                    BranchId = 0,
                    Status = 1
                });
            }

            foreach (DataGridViewRow row in dgv_raw.Rows)
            {
                if (row.IsNewRow || row.Cells[Ritemidno].Value == null) continue;
                decimal stock = ToDecimal(row.Cells[Rcurrentstockno].Value);
                decimal issued = ToDecimal(row.Cells[Rissuedqtyno].Value);
                details.Add(new DmnInventoryRequestItem
                {
                    RequestType = 2,
                    ItemId = Convert.ToInt32(row.Cells[Ritemidno].Value),
                    ItemName = Convert.ToString(row.Cells[Rrawmaterialno].Value),
                    PlannedQty = issued,
                    CurrentStock = stock,
                    IssuedQty = issued,
                    UnitId = null,
                    ProjectBalance = stock - issued,
                    Purpose = Convert.ToString(row.Cells[RpurposeRemarksno].Value),
                    ProductType = 2,
                    CreatedBy = GetComboId(cmb_req_employee) ?? 0,
                    BranchId = 0,
                    Status = 1
                });
            }

            return details;
        }

        private void btnSearchrequest_Click(object sender, EventArgs e)
        {
            try
            {
                List<DmnInventoryRequestMaster> requests;
                using (IDbConnection connection = DAL.DapperRepository<DmnInventoryRequestMaster>.OpenConnection())
                {
                    requests = connection.Query<DmnInventoryRequestMaster>(
                        @"SELECT InventoryRequestId AS RequestId, RequestNo, RequestDate, RequiredDate,
                                 DepartmentId, EmployeeId, Remarks, RequestType, CreatedBy, CreatedOn,
                                 ModifiedBy, ModifiedOn, BranchId, Status
                          FROM dbo.tblInventoryRequestMaster
                          WHERE (@Search = '' OR RequestNo LIKE '%' + @Search + '%')
                          ORDER BY InventoryRequestId DESC",
                        new { Search = txt_reqno.Text.Trim() }).ToList();
                }
                using (Form picker = new Form())
                using (DataGridView grid = new DataGridView())
                {
                    picker.Text = "Inventory Requests";
                    picker.StartPosition = FormStartPosition.CenterParent;
                    picker.Size = new Size(760, 420);
                    grid.Dock = DockStyle.Fill;
                    grid.ReadOnly = true;
                    grid.AutoGenerateColumns = true;
                    grid.DataSource = requests;
                    picker.Controls.Add(grid);
                    if (picker.ShowDialog(this) == DialogResult.OK && grid.CurrentRow != null)
                        txt_reqno.Text = Convert.ToString(grid.CurrentRow.Cells["RequestNo"].Value);
                }
            }
            catch (Exception ex) { LogInventoryException("FrmInventoryrequest btnSearchrequest_Click", ex); ShowError("Unable to search requests. " + ex.Message); }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            try { ResetRequest(); }
            catch (Exception ex) { LogInventoryException("FrmInventoryrequest btnrefresh_Click", ex); }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            try { Close(); }
            catch (Exception ex) { LogInventoryException("FrmInventoryrequest btnclose_Click", ex); }
        }

        private void ResetRequest()
        {
            dgv_batch.Rows.Clear();
            dgv_batch_ingrediant.Rows.Clear();
            dgv_raw.Rows.Clear();
            txtremarks.Clear();
            txtsearch_batch.Clear();
            txtsearch_raw.Clear();
            dtp_reqdate.Value = DateTime.Today;
            dtp_required_date.Value = DateTime.Today;
            txt_reqno.Text = "IR-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
        }
        #endregion

        #region Raw
        private void dgv_raw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex == Ractionno)
                {
                    dgv_raw.Rows.RemoveAt(e.RowIndex);
                    RenumberRows(dgv_raw, Rslno);
                }
            }
            catch (Exception ex) { LogInventoryException("FrmInventoryrequest dgv_raw_CellClick", ex); }
        }

        private void btn_addtolist_raw_Click(object sender, EventArgs e)
        {
            try
            {
                SelectAndAddRawMaterial();
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_addtolist_raw_Click", ex);
                ShowError("Unable to add the selected raw material. " + ex.Message);
            }
        }
        private void btn_advsearch_raw_Click(object sender, EventArgs e)
        {
            try
            {
                SelectAndAddRawMaterial();
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_advsearch_raw_Click", ex);
                ShowError("Unable to search raw materials. " + ex.Message);
            }
        }
        private void btn_import_raw_Click(object sender, EventArgs e)
        {
            try
            {
                SelectAndAddRawMaterial();
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_import_raw_Click", ex);
                ShowError("Unable to import raw materials. " + ex.Message);
            }
        }

        private void SelectAndAddRawMaterial()
        {
            try
            {
                List<DmnInventoryRequestItem> items = SearchRawMaterialItems(txtsearch_raw.Text.Trim());
                DmnInventoryRequestItem selected = ShowItemPicker("Select raw material", items);
                if (selected == null) return;
                foreach (DataGridViewRow existing in dgv_raw.Rows)
                {
                    if (Convert.ToInt32(existing.Cells[Ritemidno].Value) == selected.ItemId)
                    {
                        existing.Cells[Rissuedqtyno].Value = ToDecimal(existing.Cells[Rissuedqtyno].Value) + 1m;
                        UpdateRawBalance(existing);
                        return;
                    }
                }

                int index = dgv_raw.Rows.Add();
                DataGridViewRow row = dgv_raw.Rows[index];
                row.Cells[Rslno].Value = index + 1;
                row.Cells[Rrawmaterialno].Value = selected.ItemName;
                row.Cells[Rcurrentstockno].Value = selected.CurrentStock;
                row.Cells[Rissuedqtyno].Value = 1m;
                row.Cells[Runitno].Value = selected.UnitId;
                row.Cells[Rprojectbalanceno].Value = selected.CurrentStock - 1m;
                row.Cells[RpurposeRemarksno].Value = string.Empty;
                row.Cells[Ractionno].Value = "Remove";
                row.Cells[Ritemidno].Value = selected.ItemId;
                row.Cells[Rproducttypeno].Value = 2;
            }
            catch (Exception ex) { LogInventoryException("FrmInventoryrequest SelectAndAddRawMaterial", ex); ShowError("Unable to load raw materials. " + ex.Message); }
        }

        private void dgv_raw_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (_loadingGrid || e.RowIndex < 0 || e.ColumnIndex != Rissuedqtyno) return;
                UpdateRawBalance(dgv_raw.Rows[e.RowIndex]);
            }
            catch (Exception ex) { LogInventoryException("FrmInventoryrequest dgv_raw_CellValueChanged", ex); }
        }

        private void UpdateRawBalance(DataGridViewRow row)
        {
            if (row == null || row.Cells[Rcurrentstockno].Value == null) return;
            decimal stock = ToDecimal(row.Cells[Rcurrentstockno].Value);
            decimal issued = ToDecimal(row.Cells[Rissuedqtyno].Value);
            row.Cells[Rprojectbalanceno].Value = stock - issued;
        }
        #endregion

        #region Helpers
        private DmnInventoryRequestItem ShowItemPicker(string title, List<DmnInventoryRequestItem> items)
        {
            if (items == null || items.Count == 0)
            {
                MessageBox.Show("No matching items were found.", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            using (Form picker = new Form())
            using (DataGridView grid = new DataGridView())
            using (Button selectButton = new Button())
            using (Button cancelButton = new Button())
            using (TableLayoutPanel buttons = new TableLayoutPanel())
            {
                picker.Text = title;
                picker.StartPosition = FormStartPosition.CenterParent;
                picker.Size = new Size(720, 480);
                picker.MinimizeBox = false;
                picker.MaximizeBox = false;
                grid.Dock = DockStyle.Fill;
                grid.ReadOnly = true;
                grid.AllowUserToAddRows = false;
                grid.AutoGenerateColumns = true;
                grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grid.MultiSelect = false;
                grid.DataSource = items;
                grid.Columns["ItemId"].Visible = false;
                grid.Columns["ProductType"].Visible = false;
                grid.Columns["UnitPrice"].Visible = false;
                buttons.Dock = DockStyle.Bottom;
                buttons.Height = 48;
                buttons.ColumnCount = 2;
                buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                selectButton.Text = "Add Selected";
                cancelButton.Text = "Cancel";
                selectButton.DialogResult = DialogResult.OK;
                cancelButton.DialogResult = DialogResult.Cancel;
                selectButton.Dock = DockStyle.Fill;
                cancelButton.Dock = DockStyle.Fill;
                buttons.Controls.Add(selectButton, 0, 0);
                buttons.Controls.Add(cancelButton, 1, 0);
                picker.Controls.Add(grid);
                picker.Controls.Add(buttons);
                picker.AcceptButton = selectButton;
                picker.CancelButton = cancelButton;
                if (picker.ShowDialog(this) != DialogResult.OK || grid.CurrentRow == null) return null;
                return grid.CurrentRow.DataBoundItem as DmnInventoryRequestItem;
            }
        }

        private static int? GetComboId(ComboBox combo)
        {
            if (combo == null || combo.SelectedValue == null || combo.SelectedValue == DBNull.Value) return null;
            int value;
            if (int.TryParse(Convert.ToString(combo.SelectedValue), out value)) return value;
            return null;
        }

        private static decimal ToDecimal(object value)
        {
            decimal result;
            return decimal.TryParse(Convert.ToString(value), out result) ? result : 0m;
        }

        private void RenumberRows(DataGridView grid, int slnoColumn)
        {
            try
            {
                for (int i = 0; i < grid.Rows.Count; i++)
                {
                    grid.Rows[i].Cells[slnoColumn].Value = i + 1;
                }
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest RenumberRows", ex);
                throw;
            }
        }

        private void LogInventoryException(string eventName, Exception ex)
        {
            try
            {
                clsgen.LogException(
                    "InventoryManagement page " + eventName,
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.InventoryManagement));
            }
            catch
            {
                // Logging must not interrupt the inventory request workflow.
            }
        }

        private static void ShowError(string message)
        {
            MessageBox.Show(message, "Inventory Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtsearch_batch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SelectAndAddBatchProduct();
                }
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest txtsearch_batch_KeyDown", ex);
            }
        }

        private void txtsearch_raw_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SelectAndAddRawMaterial();
                }
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest txtsearch_raw_KeyDown", ex);
            }
        }
        #endregion
    }
}