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
using System.Drawing.Drawing2D;
using FoodandBeverage.Forms.UserControls;

namespace FoodandBeverage.Forms
{
    public partial class FrmInventoryrequest : Form
    {
        private readonly BllInventoryRequest _inventoryBll = new BllInventoryRequest();
        private bool _loadingGrid;
        private int _activeRequestType = 1; // 1 = batch preparation, 2 = raw material
        private readonly Dictionary<int, decimal> _originalIngredientQuantities =
            new Dictionary<int, decimal>();

        private Panel _itemSearchPopup;
        private DataGridView _itemSearchGrid;
        private int _itemSearchRequestType;
        private TextBox _itemSearchTextBox;
        private bool _searchUpdatePending;

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
                             ISNULL(pr.QOH,0) AS CurrentStock, i.Baseunit AS UnitId, CAST(2 AS INT) AS ProductType
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
                             u.UnitName AS UnitName,
                             ISNULL(pr.QOH,0) AS CurrentStock,
                             ISNULL(pr.QOH,0) - ISNULL(d.quantity,0) * @PlannedQty AS ProjectedBalance,
                             d.ProductId
                      FROM dbo.tblIngredientDetails d
                      LEFT JOIN dbo.tblItem i ON i.ItemId = d.ItemId
                      LEFT JOIN dbo.tblUnit u ON u.UId = d.Unit
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
        public int BIing_DetailsIdno = 8;
        
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
            InitializeItemSearchPopup();
            ConfigureRequestGrids();
            InitializeRequestHeader();
            ShowRequestTab(1);
            dgv_batch.CellValueChanged += dgv_batch_CellValueChanged;
            dgv_batch.CurrentCellDirtyStateChanged += dgv_batch_CurrentCellDirtyStateChanged;
            dgv_batch_ingrediant.CellValueChanged += dgv_batch_ingrediant_CellValueChanged;
            dgv_raw.CellValueChanged += dgv_raw_CellValueChanged;
            txtsearch_batch.KeyDown += txtsearch_batch_KeyDown;
            txtsearch_raw.KeyDown += txtsearch_raw_KeyDown;
            txtsearch_raw.KeyPress += txtsearch_raw_KeyPress;
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
            dgv_batch_ingrediant.ReadOnly = false;
            dgv_batch_ingrediant.MultiSelect = false;
            dgv_batch_ingrediant.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_batch_ingrediant.Columns[BIingredientno].ReadOnly = true;
            dgv_batch_ingrediant.Columns[Biperportionqtyno].ReadOnly = false;
            dgv_batch_ingrediant.Columns[BItotalrequiredqtyno].ReadOnly = true;
            dgv_batch_ingrediant.Columns[BIunitno].ReadOnly = true;
            dgv_batch_ingrediant.Columns[BIcurrentstockno].ReadOnly = true;
            dgv_batch_ingrediant.Columns[BIprojectedbalanceno].ReadOnly = true;
            dgv_batch_ingrediant.Columns[BIproductidno].Visible = false;
            dgv_batch_ingrediant.Columns[BIing_DetailsIdno].Visible = false;
            dgv_batch_ingrediant.Columns[Biperportionqtyno].DefaultCellStyle.Format = "0.####";

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
        private void FrmInventoryrequest_Load(object sender, EventArgs e)
        {
            textb_shape();

            UIHelper.RoundCorners(tlp_ingrediant, 25);
            UIHelper.RoundCorners(tlp_batch, 25);
            UIHelper.RoundCorners(tlp_info, 25);
            UIHelper.RoundCorners(tlp_req_from, 25); 

            UIHelper.RoundCorners(tlp_raw_control, 25);
            
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
            try
            {
                HideItemSearchPopup();
                bool isBatchPreparation = requestType == 1;
                _activeRequestType = isBatchPreparation ? 1 : 2;

                if (isBatchPreparation)
                {
                    tlp_Batchpreparation.BringToFront();
                }
                else
                {
                    tlp_raw.BringToFront();
                }

                // Apply the matching active/inactive resource images to the navigation tabs.
                btn_batch_tab.BackgroundImage = isBatchPreparation
                    ? Properties.Resources.Inv_BatchPreparation_Active
                    : Properties.Resources.Inv_BatchPreparation_Inactive;

                btn_raw_tab.BackgroundImage = isBatchPreparation
                    ? Properties.Resources.Inv_RawMaterialIssue_Inactive
                    : Properties.Resources.Inv_RawMaterialIssue_Active;

                btn_batch_tab.BackgroundImageLayout = ImageLayout.Stretch;
                btn_raw_tab.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest ShowRequestTab", ex);
                throw;
            }
        }
        #endregion

        #region Batch Preparation
        private void btn_clearall_batch_Click(object sender, EventArgs e)
        {
            try
            {
                HideItemSearchPopup();
                dgv_batch.Rows.Clear();
                dgv_batch_ingrediant.Rows.Clear();
                _originalIngredientQuantities.Clear();
                txtsearch_batch.Clear();
            }
            catch (Exception ex) { LogInventoryException("FrmInventoryrequest btn_clearall_batch_Click", ex); }
        }

        private void btn_importbatch_Click(object sender, EventArgs e)
        {
            try
            {
                
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
                HideItemSearchPopup();
                int productId = ShowAdvancedItemSearch(1, txtsearch_batch.Text.Trim());
                if (productId > 0)
                {
                    AddBatchProductById(productId);
                }
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest SelectAndAddBatchProduct", ex);
                ShowError("Unable to load batch products. " + ex.Message);
            }
        }

        private void AddBatchProductById(int productId)
        {
            try
            {
                DmnInventoryRequestItem selected = LoadBatchProductById(productId);
                if (selected == null)
                {
                    ShowError("The selected product is no longer active.");
                    return;
                }

                foreach (DataGridViewRow existing in dgv_batch.Rows)
                {
                    int existingId;
                    if (int.TryParse(Convert.ToString(existing.Cells[Bitemidno].Value), out existingId) &&
                        existingId == selected.ItemId)
                    {
                        existing.Cells[Bplannedqtyno].Value =
                            Math.Max(1m, ToDecimal(existing.Cells[Bplannedqtyno].Value) + 1m);
                        LoadIngredientsForRow(existing);
                        dgv_batch.CurrentCell = existing.Cells[Bmenuitemno];
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
                dgv_batch.CurrentCell = row.Cells[Bmenuitemno];
                LoadIngredientsForRow(row);
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest AddBatchProductById", ex);
                ShowError("Unable to load the selected product. " + ex.Message);
            }
        }

        private DmnInventoryRequestItem LoadBatchProductById(int productId)
        {
            using (IDbConnection connection =
                DAL.DapperRepository<DmnInventoryRequestItem>.OpenConnection())
            {
                return connection.QueryFirstOrDefault<DmnInventoryRequestItem>(
                    @"SELECT p.ProductId AS ItemId, p.Product AS ItemName,
                             CAST(0 AS DECIMAL(18,2)) AS CurrentStock,
                             CAST(1 AS INT) AS ProductType
                      FROM dbo.tblProduct p
                      LEFT JOIN dbo.tblCategory_FTI c ON c.Fcid = p.Fcid
                      WHERE p.ProductId = @ProductId
                        AND p.Status = 1
                        AND c.Status = 1",
                    new { ProductId = productId });
            }
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
                _originalIngredientQuantities.Clear();

                foreach (DmnInventoryRequestIngredient ingredient in ingredients)
                {
                    int index = dgv_batch_ingrediant.Rows.Add();
                    DataGridViewRow row = dgv_batch_ingrediant.Rows[index];
                    row.Cells[BIslno].Value = index + 1;
                    row.Cells[BIingredientno].Value = ingredient.IngredientName;
                    row.Cells[Biperportionqtyno].Value = ingredient.PerPortionQty;
                    row.Cells[BItotalrequiredqtyno].Value = ingredient.TotalRequiredQty;
                    row.Cells[BIunitno].Value = string.IsNullOrWhiteSpace(ingredient.UnitName)
                        ? Convert.ToString(ingredient.UnitId)
                        : ingredient.UnitName;
                    row.Cells[BIcurrentstockno].Value = ingredient.CurrentStock;
                    row.Cells[BIprojectedbalanceno].Value = ingredient.ProjectedBalance;
                    row.Cells[BIproductidno].Value = ingredient.ProductId;

                    // Hidden column 8 stores tblIngredientDetails.Ing_DetailsId for updates.
                    row.Cells[BIing_DetailsIdno].Value = ingredient.IngredientDetailId;
                    _originalIngredientQuantities[ingredient.IngredientDetailId] =
                        ingredient.PerPortionQty;
                }
            }
            finally
            {
                _loadingGrid = false;
            }
        }

        private void dgv_batch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == Bactionno)
            {
                dgv_batch.Rows.RemoveAt(e.RowIndex);
                RenumberRows(dgv_batch, Bslno);
                dgv_batch_ingrediant.Rows.Clear();
                _originalIngredientQuantities.Clear();
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
            try
            {
                if (_loadingGrid || e.RowIndex < 0 ||
                    e.ColumnIndex != Bplannedqtyno)
                {
                    return;
                }

                if (dgv_batch.Rows[e.RowIndex].Cells[Bitemidno].Value == null)
                {
                    return;
                }

                // Preserve unsaved ingredient edits when the planned product quantity changes.
                decimal plannedQty = ToDecimal(
                    dgv_batch.Rows[e.RowIndex].Cells[Bplannedqtyno].Value);
                RecalculateBatchIngredientTotals(plannedQty);
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest dgv_batch_CellValueChanged", ex);
            }
        }

        private void RecalculateBatchIngredientTotals(decimal plannedQty)
        {
            _loadingGrid = true;
            try
            {
                foreach (DataGridViewRow ingredientRow in dgv_batch_ingrediant.Rows)
                {
                    if (ingredientRow.IsNewRow)
                    {
                        continue;
                    }

                    decimal perPortionQty =
                        ToDecimal(ingredientRow.Cells[Biperportionqtyno].Value);
                    decimal totalRequiredQty = perPortionQty * plannedQty;
                    decimal currentStock =
                        ToDecimal(ingredientRow.Cells[BIcurrentstockno].Value);

                    ingredientRow.Cells[BItotalrequiredqtyno].Value = totalRequiredQty;
                    ingredientRow.Cells[BIprojectedbalanceno].Value =
                        currentStock - totalRequiredQty;
                }
            }
            finally
            {
                _loadingGrid = false;
            }
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

        private void dgv_batch_ingrediant_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_loadingGrid || e.RowIndex < 0 ||
                e.ColumnIndex != Biperportionqtyno)
            {
                return;
            }

            try
            {
                DataGridViewRow row = dgv_batch_ingrediant.Rows[e.RowIndex];
                decimal perPortionQty = ToDecimal(row.Cells[Biperportionqtyno].Value);
                decimal plannedQty = dgv_batch.CurrentRow == null
                    ? 0m
                    : ToDecimal(dgv_batch.CurrentRow.Cells[Bplannedqtyno].Value);
                decimal totalRequiredQty = perPortionQty * plannedQty;
                decimal currentStock = ToDecimal(row.Cells[BIcurrentstockno].Value);

                _loadingGrid = true;
                row.Cells[BItotalrequiredqtyno].Value = totalRequiredQty;
                row.Cells[BIprojectedbalanceno].Value = currentStock - totalRequiredQty;
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest dgv_batch_ingrediant_CellValueChanged", ex);
            }
            finally
            {
                _loadingGrid = false;
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
                HideItemSearchPopup();
                int itemId = ShowAdvancedItemSearch(2, txtsearch_raw.Text.Trim());
                if (itemId > 0)
                {
                    AddRawMaterialById(itemId);
                }
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest SelectAndAddRawMaterial", ex);
                ShowError("Unable to load raw materials. " + ex.Message);
            }
        }

        private void AddRawMaterialById(int itemId)
        {
            try
            {
                DmnInventoryRequestItem selected = LoadRawMaterialById(itemId);
                if (selected == null)
                {
                    ShowError("The selected raw material is no longer active.");
                    return;
                }

                foreach (DataGridViewRow existing in dgv_raw.Rows)
                {
                    int existingId;
                    if (int.TryParse(Convert.ToString(existing.Cells[Ritemidno].Value), out existingId) &&
                        existingId == selected.ItemId)
                    {
                        existing.Cells[Rissuedqtyno].Value =
                            ToDecimal(existing.Cells[Rissuedqtyno].Value) + 1m;
                        UpdateRawBalance(existing);
                        dgv_raw.CurrentCell = existing.Cells[Rrawmaterialno];
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
                dgv_raw.CurrentCell = row.Cells[Rrawmaterialno];
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest AddRawMaterialById", ex);
                ShowError("Unable to load the selected raw material. " + ex.Message);
            }
        }

        private DmnInventoryRequestItem LoadRawMaterialById(int itemId)
        {
            using (IDbConnection connection =
                DAL.DapperRepository<DmnInventoryRequestItem>.OpenConnection())
            {
                return connection.QueryFirstOrDefault<DmnInventoryRequestItem>(
                    @"SELECT CAST(i.ItemId AS INT) AS ItemId,
                             i.Itemname AS ItemName,
                             ISNULL(pr.QOH,0) AS CurrentStock,
                             i.Baseunit AS UnitId,
                             CAST(2 AS INT) AS ProductType
                      FROM dbo.tblItem i
                      LEFT JOIN dbo.tblCategory_FTI c ON c.Fcid = i.Fcid
                      OUTER APPLY
                      (
                          SELECT TOP (1) r.QOH
                          FROM dbo.tblProductRate r
                          WHERE r.ProductId = i.ItemId
                            AND ISNULL(r.ProductType,0) = 0
                          ORDER BY r.Id DESC
                      ) pr
                      WHERE i.ItemId = @ItemId
                        AND i.Status = 1
                        AND i.Saletype = 0
                        AND c.Status = 1",
                    new { ItemId = itemId });
            }
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
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {//
                foreach (TextBox textBox in new TextBox[] { txtsearch_batch, txtremarks, txt_reqno, txtsearch_raw })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("InventoryManagement page textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.InventoryManagement));
            }
        }

        private void ApplyRoundedTextBox(TextBox textBox, int radius)
        {
            try
            {
                // Create a new GraphicsPath object
                GraphicsPath path = new GraphicsPath();
                // Define the shape of the path using a rounded rectangle
                Rectangle rect = new Rectangle(0, 0, textBox.Width, textBox.Height);
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                // Set the Region property of the textbox to the new path
                textBox.Region = new Region(path);
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("InventoryManagement page ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.InventoryManagement));
            }
        }

        #endregion
        private void InitializeItemSearchPopup()
        {
            _itemSearchPopup = new Panel();
            _itemSearchPopup.Visible = false;
            _itemSearchPopup.BorderStyle = BorderStyle.FixedSingle;
            _itemSearchPopup.BackColor = Color.White;
            _itemSearchPopup.Size = new Size(430, 220);
            _itemSearchPopup.Padding = new Padding(2);

            _itemSearchGrid = new DataGridView();
            _itemSearchGrid.Dock = DockStyle.Fill;
            _itemSearchGrid.ReadOnly = true;
            _itemSearchGrid.AllowUserToAddRows = false;
            _itemSearchGrid.AllowUserToDeleteRows = false;
            _itemSearchGrid.AllowUserToResizeRows = false;
            _itemSearchGrid.MultiSelect = false;
            _itemSearchGrid.RowHeadersVisible = false;
            _itemSearchGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _itemSearchGrid.AutoGenerateColumns = false;
            _itemSearchGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _itemSearchGrid.ColumnHeadersHeight = 34;
            _itemSearchGrid.RowTemplate.Height = 42;
            _itemSearchGrid.DefaultCellStyle.Font = new Font("Segoe UI", 10.5f);
            _itemSearchGrid.DefaultCellStyle.Padding = new Padding(4);
            _itemSearchGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "searchField1",
                HeaderText = "Product / Code",
                DataPropertyName = "Field1",
                FillWeight = 40
            });
            _itemSearchGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "searchField2",
                HeaderText = "Name / Category",
                DataPropertyName = "Field2",
                FillWeight = 40
            });
            _itemSearchGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "searchField3",
                HeaderText = "Category",
                DataPropertyName = "Field3",
                FillWeight = 30
            });
            _itemSearchGrid.CellClick += ItemSearchGrid_CellClick;

            _itemSearchPopup.Controls.Add(_itemSearchGrid);
            Controls.Add(_itemSearchPopup);
            _itemSearchPopup.BringToFront();
        }

        private DmnSearch CreateItemSearchCriteria(int requestType, string searchText)
        {
            string escapedSearch = (searchText ?? string.Empty).Replace("'", "''");
            DmnSearch searchCriteria = new DmnSearch();

            if (requestType == 1)
            {
                searchCriteria.ColumnHeaders = "ID,Product,Category";
                searchCriteria.Select = "p.ProductId as Id,p.Product as Field1,c.CategoryName as Field2";
                searchCriteria.Table = "tblProduct p left join tblCategory_FTI c on p.Fcid=c.Fcid";
                searchCriteria.WhereNdGroupBy = "WHERE p.Status = 1 AND c.Status = 1";
                if (!string.IsNullOrWhiteSpace(escapedSearch))
                {
                    searchCriteria.WhereNdGroupBy +=
                        " AND p.Product LIKE '%" + escapedSearch + "%'";
                }
                searchCriteria.OrderBy = "ORDER BY p.Product";
            }
            else
            {
                searchCriteria.ColumnHeaders = "ID,ItemCode_UPC,Itemname,CategoryName";
                searchCriteria.Select =
                    "i.ItemId as Id,i.ItemCode_UPC as Field1,i.Itemname as Field2,c.CategoryName as Field3";
                searchCriteria.Table =
                    "tblItem i left join tblCategory_FTI c on c.Fcid=i.Fcid";
                searchCriteria.WhereNdGroupBy =
                    "WHERE i.Status = 1 AND i.Saletype = 0 AND c.Status = 1";
                if (!string.IsNullOrWhiteSpace(escapedSearch))
                {
                    searchCriteria.WhereNdGroupBy +=
                        " AND i.Itemname LIKE '%" + escapedSearch + "%'";
                }
                searchCriteria.OrderBy = "ORDER BY i.Itemname";
            }

            return searchCriteria;
        }

        private List<DmnGridFields> SearchItemGridRows(int requestType, string searchText)
        {
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            DmnSearch searchCriteria = CreateItemSearchCriteria(requestType, searchText);
            return bllSearch.GetSearchData("prdSearch", searchCriteria);
        }

        private int ShowAdvancedItemSearch(int requestType, string searchText)
        {
            int returnValue = 0;
            DataTable dtColumnAlign = new DataTable();
            dtColumnAlign.Columns.Add();

            DmnSearch searchCriteria = CreateItemSearchCriteria(requestType, searchText);
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            List<DmnGridFields> gridSource =
                bllSearch.GetSearchData("prdSearch", searchCriteria);

            if (gridSource == null || gridSource.Count == 0)
            {
                CMessageBox.Show("No Records", "Empty",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }

            using (var search = new FrmSearch<DmnGridFields>(
                searchCriteria, SearchMode.FirstCharacter, this.Width - 150,
                this.Height - 150, this.Location.Y + 50, this.Location.X + 20,
                dtColumnAlign))
            {
                if (search.ShowDialog() == DialogResult.OK)
                {
                    int.TryParse(Convert.ToString(search._retValue), out returnValue);
                }
            }

            return returnValue;
        }

        private void QueueItemSearchPopup(TextBox searchTextBox, int requestType)
        {
            if (searchTextBox == null || IsDisposed || !IsHandleCreated)
            {
                return;
            }

            try
            {
                if (_searchUpdatePending)
                {
                    return;
                }

                _searchUpdatePending = true;
                BeginInvoke(new MethodInvoker(delegate
                {
                    try
                    {
                        ShowItemSearchPopup(searchTextBox, requestType);
                    }
                    finally
                    {
                        _searchUpdatePending = false;
                    }
                }));
            }
            catch (InvalidOperationException)
            {
                _searchUpdatePending = false;
                // The form can close while a pending key event is being processed.
            }
        }

        private void ShowItemSearchPopup(TextBox searchTextBox, int requestType)
        {
            try
            {
                string searchText = searchTextBox.Text.Trim();
                _itemSearchTextBox = searchTextBox;
                _itemSearchRequestType = requestType;

                if (searchText.Length == 0)
                {
                    HideItemSearchPopup();
                    return;
                }

                List<DmnGridFields> results =
                    SearchItemGridRows(requestType, searchText);

                if (results == null || results.Count == 0)
                {
                    HideItemSearchPopup();
                    return;
                }

                _itemSearchGrid.DataSource = null;
                _itemSearchGrid.Columns["searchField1"].HeaderText =
                    requestType == 1 ? "Product" : "Item Code";
                _itemSearchGrid.Columns["searchField2"].HeaderText =
                    requestType == 1 ? "Category" : "Item Name";
                _itemSearchGrid.Columns["searchField3"].Visible = requestType != 1;
                _itemSearchGrid.Columns["searchField2"].Visible = true;
                _itemSearchGrid.DataSource = results;

                int popupWidth = Math.Min(
                    Math.Max(searchTextBox.Width, 390),
                    Math.Max(300, ClientSize.Width - 16));
                int popupHeight = Math.Min(
                    Math.Min(230, Math.Max(90, ClientSize.Height - 16)),
                    42 + results.Count * 42);

                Point belowSearch = PointToClient(
                    searchTextBox.PointToScreen(new Point(0, searchTextBox.Height)));
                Point aboveSearch = PointToClient(
                    searchTextBox.PointToScreen(Point.Empty));

                int x = Math.Max(8, Math.Min(belowSearch.X,
                    ClientSize.Width - popupWidth - 8));
                int y = belowSearch.Y;
                if (y + popupHeight > ClientSize.Height)
                {
                    y = Math.Max(8, aboveSearch.Y - popupHeight);
                }

                _itemSearchPopup.Location = new Point(x, y);
                _itemSearchPopup.Size = new Size(popupWidth, popupHeight);
                _itemSearchPopup.Visible = true;
                _itemSearchPopup.BringToFront();

                if (_itemSearchGrid.Rows.Count > 0)
                {
                    _itemSearchGrid.ClearSelection();
                    _itemSearchGrid.Rows[0].Selected = true;
                    _itemSearchGrid.CurrentCell =
                        _itemSearchGrid.Rows[0].Cells[0];
                }
            }
            catch (Exception ex)
            {
                HideItemSearchPopup();
                LogInventoryException("FrmInventoryrequest ShowItemSearchPopup", ex);
            }
        }

        private void HideItemSearchPopup()
        {
            if (_itemSearchPopup != null)
            {
                _itemSearchPopup.Visible = false;
            }
        }

        private void ItemSearchGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    SelectItemSearchGridRow(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest ItemSearchGrid_CellClick", ex);
            }
        }

        private void SelectItemSearchGridRow(int rowIndex)
        {
            if (_itemSearchGrid == null || rowIndex < 0 ||
                rowIndex >= _itemSearchGrid.Rows.Count)
            {
                return;
            }

            DmnGridFields selected =
                _itemSearchGrid.Rows[rowIndex].DataBoundItem as DmnGridFields;
            if (selected == null || selected.Id <= 0)
            {
                return;
            }

            int selectedId = Convert.ToInt32(selected.Id);
            int requestType = _itemSearchRequestType;
            TextBox searchTextBox = _itemSearchTextBox;

            HideItemSearchPopup();

            if (requestType == 1)
            {
                AddBatchProductById(selectedId);
            }
            else
            {
                AddRawMaterialById(selectedId);
            }

            if (searchTextBox != null)
            {
                searchTextBox.Clear();
                searchTextBox.Focus();
            }
        }

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
                    if (_itemSearchPopup.Visible && _itemSearchRequestType == 1 &&
                        _itemSearchGrid.Rows.Count > 0)
                    {
                        int rowIndex = _itemSearchGrid.CurrentRow == null
                            ? 0
                            : _itemSearchGrid.CurrentRow.Index;
                        SelectItemSearchGridRow(rowIndex);
                    }
                    else
                    {
                        SelectAndAddBatchProduct();
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    HideItemSearchPopup();
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
                    if (_itemSearchPopup.Visible && _itemSearchRequestType == 2 &&
                        _itemSearchGrid.Rows.Count > 0)
                    {
                        int rowIndex = _itemSearchGrid.CurrentRow == null
                            ? 0
                            : _itemSearchGrid.CurrentRow.Index;
                        SelectItemSearchGridRow(rowIndex);
                    }
                    else
                    {
                        SelectAndAddRawMaterial();
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    HideItemSearchPopup();
                }
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest txtsearch_raw_KeyDown", ex);
            }
        }

        #endregion

        private void FrmInventoryrequest_Resize(object sender, EventArgs e)
        {
            HideItemSearchPopup();
            textb_shape();
        }

        private void txtsearch_batch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r' || e.KeyChar == '\n')
                {
                    return;
                }

                if (!char.IsControl(e.KeyChar) || e.KeyChar == '\b')
                {
                    QueueItemSearchPopup(txtsearch_batch, 1);
                }
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest txtsearch_batch_KeyPress", ex);
            }
        }

        private void txtsearch_raw_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r' || e.KeyChar == '\n')
                {
                    return;
                }

                if (!char.IsControl(e.KeyChar) || e.KeyChar == '\b')
                {
                    QueueItemSearchPopup(txtsearch_raw, 2);
                }
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest txtsearch_raw_KeyPress", ex);
            }
        }

        private void btn_save_ingrediant_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_batch.CurrentRow == null ||
                    dgv_batch.CurrentRow.Cells[Bitemidno].Value == null)
                {
                    ShowError("Select a batch product before saving ingredient details.");
                    return;
                }

                dgv_batch_ingrediant.EndEdit();
                int productId = Convert.ToInt32(
                    dgv_batch.CurrentRow.Cells[Bitemidno].Value);

                List<DmnInventoryRequestIngredient> changedIngredients =
                    new List<DmnInventoryRequestIngredient>();

                foreach (DataGridViewRow row in dgv_batch_ingrediant.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    int detailId;
                    if (!int.TryParse(
                        Convert.ToString(row.Cells[BIing_DetailsIdno].Value),
                        out detailId) || detailId <= 0)
                    {
                        continue;
                    }

                    int rowProductId;
                    if (!int.TryParse(
                        Convert.ToString(row.Cells[BIproductidno].Value),
                        out rowProductId) || rowProductId != productId)
                    {
                        ShowError("The ingredient rows do not belong to the selected product. Reload the product and try again.");
                        return;
                    }

                    decimal quantity;
                    if (!decimal.TryParse(
                        Convert.ToString(row.Cells[Biperportionqtyno].Value),
                        out quantity) || quantity <= 0m)
                    {
                        ShowError("Enter a per-portion ingredient quantity greater than zero.");
                        dgv_batch_ingrediant.CurrentCell =
                            row.Cells[Biperportionqtyno];
                        dgv_batch_ingrediant.BeginEdit(true);
                        return;
                    }

                    decimal originalQuantity;
                    if (_originalIngredientQuantities.TryGetValue(
                        detailId, out originalQuantity) &&
                        originalQuantity != quantity)
                    {
                        changedIngredients.Add(new DmnInventoryRequestIngredient
                        {
                            IngredientDetailId = detailId,
                            ProductId = productId,
                            PerPortionQty = quantity
                        });
                    }
                }

                if (changedIngredients.Count == 0)
                {
                    MessageBox.Show("There are no ingredient quantity changes to save.",
                        "Ingredient Details", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                DialogResult confirmation = CMessageBox.Show(
                    "Save the updated ingredient details for this product?",
                    "Confirm Ingredient Update",
                    MessageBoxButtons.YesNo);

                if (confirmation != DialogResult.Yes)
                {
                    return;
                }

                using (IDbConnection connection =
                    DAL.DapperRepository<DmnInventoryRequestIngredient>.OpenConnection())
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (DmnInventoryRequestIngredient ingredient in changedIngredients)
                        {
                            int affectedRows = connection.Execute(
                                @"UPDATE dbo.tblIngredientDetails
                                  SET quantity = @PerPortionQty
                                  WHERE Ing_DetailsId = @IngredientDetailId
                                    AND ProductId = @ProductId
                                    AND ISNULL(Status,1) <> 0",
                                new
                                {
                                    ingredient.PerPortionQty,
                                    ingredient.IngredientDetailId,
                                    ingredient.ProductId
                                },
                                transaction);

                            if (affectedRows != 1)
                            {
                                throw new InvalidOperationException(
                                    "An ingredient detail could not be updated. Reload the product and try again.");
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                LoadIngredientsForRow(dgv_batch.CurrentRow);

                MessageBox.Show("Ingredient details updated successfully.",
                    "Ingredient Details", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_save_ingrediant_Click", ex);
                ShowError("Unable to save ingredient details. " + ex.Message);
            }
        }
    }
}