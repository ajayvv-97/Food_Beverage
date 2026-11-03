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
        private int? _currentRequestId;
        private int? _currentDraftId;
        private int _visibleIngredientProductId;
        private List<DmnInventoryRequestDetail> _loadedRequestDetails = new List<DmnInventoryRequestDetail>();

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
        public int BIitemidno = 9;
        public int BIunitidno = 10;
        
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
            btn_deletesaved_draft.Visible = false;
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
            cmb_req_employee.SelectedIndexChanged += cmb_req_employee_SelectedIndexChanged;
            txt_requ_by.ReadOnly = true;
            txt_batchfoodqty.ReadOnly = true;
            txt_totalrawmaterialqty.ReadOnly = true;
            total_food_raw_qty.ReadOnly = true;
            lbl_hint_raw.Text = "Stock balance is indicative. It will be updated only after the request is approved by authorized staff.";
            UpdateRequesterName();
            UpdateRequestTotals();
        }

        private void ConfigureRequestGrids()
        {
            ReplaceActionColumnWithIcon(dgv_batch, Bactionno, "Remove product");
            ReplaceActionColumnWithIcon(dgv_raw, Ractionno, "Remove raw material");

            ApplyInventoryGridStyle(dgv_batch);
            ApplyInventoryGridStyle(dgv_batch_ingrediant);
            ApplyInventoryGridStyle(dgv_raw);

            // Fill mode uses all available width at every screen resolution.
            // The item/ingredient name column receives the largest share.
            dgv_batch.Columns[Bslno].FillWeight = 8;
            dgv_batch.Columns[Bmenuitemno].FillWeight = 42;
            dgv_batch.Columns[Bplannedqtyno].FillWeight = 13;
            dgv_batch.Columns[Bunitno].FillWeight = 9;
            dgv_batch.Columns[Brecipeingredientsno].FillWeight = 21;
            dgv_batch.Columns[Bactionno].FillWeight = 7;
            dgv_batch.Columns[Bitemidno].Visible = false;

            dgv_batch_ingrediant.Columns[BIslno].FillWeight = 7;
            dgv_batch_ingrediant.Columns[BIingredientno].FillWeight = 31;
            dgv_batch_ingrediant.Columns[Biperportionqtyno].FillWeight = 13;
            dgv_batch_ingrediant.Columns[BItotalrequiredqtyno].FillWeight = 15;
            dgv_batch_ingrediant.Columns[BIunitno].FillWeight = 9;
            dgv_batch_ingrediant.Columns[BIcurrentstockno].FillWeight = 12;
            dgv_batch_ingrediant.Columns[BIprojectedbalanceno].FillWeight = 13;
            dgv_batch_ingrediant.Columns[BIproductidno].Visible = false;
            dgv_batch_ingrediant.Columns[BIing_DetailsIdno].Visible = false;
            if (!dgv_batch_ingrediant.Columns.Contains("biItemId"))
                dgv_batch_ingrediant.Columns.Add(new DataGridViewTextBoxColumn { Name = "biItemId", Visible = false });
            if (!dgv_batch_ingrediant.Columns.Contains("biUnitId"))
                dgv_batch_ingrediant.Columns.Add(new DataGridViewTextBoxColumn { Name = "biUnitId", Visible = false });
            BIitemidno = dgv_batch_ingrediant.Columns["biItemId"].Index;
            BIunitidno = dgv_batch_ingrediant.Columns["biUnitId"].Index;

            dgv_raw.Columns[Rslno].FillWeight = 7;
            dgv_raw.Columns[Rrawmaterialno].FillWeight = 34;
            dgv_raw.Columns[Rcurrentstockno].FillWeight = 11;
            dgv_raw.Columns[Rissuedqtyno].FillWeight = 11;
            dgv_raw.Columns[Runitno].FillWeight = 9;
            dgv_raw.Columns[Rprojectbalanceno].FillWeight = 12;
            dgv_raw.Columns[RpurposeRemarksno].FillWeight = 15;
            dgv_raw.Columns[Ractionno].FillWeight = 7;
            dgv_raw.Columns[Ritemidno].Visible = false;
            dgv_raw.Columns[Rproducttypeno].Visible = false;

            dgv_batch.Columns[Bactionno].ReadOnly = true;
            dgv_batch.Columns[Brecipeingredientsno].ReadOnly = true;

            dgv_batch_ingrediant.Columns[BIingredientno].ReadOnly = true;
            dgv_batch_ingrediant.Columns[Biperportionqtyno].ReadOnly = false;
            dgv_batch_ingrediant.Columns[BItotalrequiredqtyno].ReadOnly = true;
            dgv_batch_ingrediant.Columns[BIunitno].ReadOnly = true;
            dgv_batch_ingrediant.Columns[BIcurrentstockno].ReadOnly = true;
            dgv_batch_ingrediant.Columns[BIprojectedbalanceno].ReadOnly = true;
            dgv_batch_ingrediant.Columns[Biperportionqtyno].DefaultCellStyle.Format = "0.####";

            dgv_raw.Columns[Rcurrentstockno].ReadOnly = true;
            dgv_raw.Columns[Rprojectbalanceno].ReadOnly = true;
            dgv_batch.Resize += InventoryGrid_Resize;
            dgv_batch_ingrediant.Resize += InventoryGrid_Resize;
            dgv_raw.Resize += InventoryGrid_Resize;
        }

        private void ApplyInventoryGridStyle(DataGridView grid)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.RowHeadersVisible = false;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn column in grid.Columns)
            {
                // Explicit designer sizing modes (AllCells/ColumnHeader) override the grid default.
                // Set every visible column to Fill so item-name columns can use the free width.
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight = 56;
            grid.RowTemplate.Height = 36;
            grid.BackgroundColor = Color.FromArgb(236, 220, 244);
            grid.GridColor = Color.FromArgb(236, 220, 244);

            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(101, 80, 185);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(7, 2, 7, 2);
            grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(45, 42, 60);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 226, 250);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(45, 42, 60);
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            grid.DefaultCellStyle.Padding = new Padding(7, 2, 7, 2);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 246, 252);
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 226, 250);
            grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(45, 42, 60);
        }

        private void ReplaceActionColumnWithIcon(DataGridView grid, int columnIndex, string toolTip)
        {
            if (grid == null || columnIndex < 0 || columnIndex >= grid.Columns.Count)
            {
                return;
            }

            DataGridViewColumn oldColumn = grid.Columns[columnIndex];
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = oldColumn.Name;
            imageColumn.HeaderText = string.Empty;
            imageColumn.Image = Properties.Resources.food_cancel_icon;
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imageColumn.ToolTipText = toolTip;
            imageColumn.ReadOnly = true;
            imageColumn.Resizable = DataGridViewTriState.False;
            imageColumn.MinimumWidth = 34;
            imageColumn.FillWeight = 7;
            imageColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            imageColumn.DefaultCellStyle.NullValue = Properties.Resources.food_cancel_icon;

            grid.Columns.RemoveAt(columnIndex);
            grid.Columns.Insert(columnIndex, imageColumn);
        }

        private void InventoryGrid_Resize(object sender, EventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid == null)
            {
                return;
            }

            // Reapply Fill mode after the parent layout recalculates its client width.
            if (grid.AutoSizeColumnsMode != DataGridViewAutoSizeColumnsMode.Fill)
            {
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
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
            LoadRequestComboData();
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
                _visibleIngredientProductId = 0;
                txtsearch_batch.Clear();
                UpdateRequestTotals();
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
                HideItemSearchPopup();
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
                row.Cells[Bactionno].Value = Properties.Resources.food_cancel_icon;
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
            if (productRow == null || productRow.IsNewRow || productRow.Cells[Bitemidno].Value == null)
                return;

            CacheVisibleIngredientRows();
            int productId = Convert.ToInt32(productRow.Cells[Bitemidno].Value);
            decimal plannedQty = Math.Max(0m, ToDecimal(productRow.Cells[Bplannedqtyno].Value));
            List<DmnInventoryRequestIngredient> ingredients;

            bool hasSavedRows = _loadedRequestDetails != null &&
                _loadedRequestDetails.Any(x => x.RequestType == 1 && x.ProductId == productId);

            if (hasSavedRows)
            {
                ingredients = _loadedRequestDetails
                    .Where(x => x.RequestType == 1 && x.ProductId == productId && x.ItemId.HasValue)
                    .OrderBy(x => x.SlNo)
                    .Select(x => new DmnInventoryRequestIngredient
                    {
                        IngredientDetailId = (int)(x.RecipeItemId ?? 0),
                        ItemId = Convert.ToInt32(x.ItemId.Value),
                        IngredientName = x.ItemName,
                        PerPortionQty = x.PerPortionQty ?? 0m,
                        TotalRequiredQty = (x.PerPortionQty ?? 0m) * plannedQty,
                        UnitId = x.Unit,
                        UnitName = x.UnitName,
                        CurrentStock = x.CurrentStock ?? 0m,
                        ProjectedBalance = (x.CurrentStock ?? 0m) - ((x.PerPortionQty ?? 0m) * plannedQty),
                        ProductId = productId
                    }).ToList();
            }
            else
            {
                ingredients = LoadProductIngredients(productId, plannedQty);
            }

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
                        ? Convert.ToString(ingredient.UnitId) : ingredient.UnitName;
                    row.Cells[BIcurrentstockno].Value = ingredient.CurrentStock;
                    row.Cells[BIprojectedbalanceno].Value = ingredient.ProjectedBalance;
                    row.Cells[BIproductidno].Value = ingredient.ProductId;
                    row.Cells[BIing_DetailsIdno].Value = ingredient.IngredientDetailId;
                    row.Cells[BIitemidno].Value = ingredient.ItemId;
                    row.Cells[BIunitidno].Value = ingredient.UnitId;
                    _originalIngredientQuantities[ingredient.IngredientDetailId] = ingredient.PerPortionQty;
                }
                _visibleIngredientProductId = productId;
            }
            finally
            {
                _loadingGrid = false;
            }

            UpdateRequestTotals();
        }

        private void CacheVisibleIngredientRows()
        {
            if (_loadedRequestDetails == null || _visibleIngredientProductId <= 0)
                return;

            foreach (DataGridViewRow row in dgv_batch_ingrediant.Rows)
            {
                if (row.IsNewRow || row.Cells[BIitemidno].Value == null) continue;

                long itemId;
                long recipeItemId;
                if (!long.TryParse(Convert.ToString(row.Cells[BIitemidno].Value), out itemId)) continue;
                long.TryParse(Convert.ToString(row.Cells[BIing_DetailsIdno].Value), out recipeItemId);

                DmnInventoryRequestDetail saved = _loadedRequestDetails.FirstOrDefault(x =>
                    x.RequestType == 1 && x.ProductId == _visibleIngredientProductId &&
                    x.ItemId == itemId && x.RecipeItemId == recipeItemId);

                if (saved == null)
                {
                    saved = new DmnInventoryRequestDetail
                    {
                        RequestType = 1, ProductId = _visibleIngredientProductId,
                        ItemId = itemId, RecipeItemId = recipeItemId, ProductType = 1, Status = 1
                    };
                    _loadedRequestDetails.Add(saved);
                }

                saved.PerPortionQty = ToDecimal(row.Cells[Biperportionqtyno].Value);
                saved.TotalRequiredQty = ToDecimal(row.Cells[BItotalrequiredqtyno].Value);
                saved.CurrentStock = ToDecimal(row.Cells[BIcurrentstockno].Value);
                saved.ProjectedBalance = ToDecimal(row.Cells[BIprojectedbalanceno].Value);
                saved.Unit = ToNullableInt(row.Cells[BIunitidno].Value);
                saved.UnitName = Convert.ToString(row.Cells[BIunitno].Value);
                saved.ItemName = Convert.ToString(row.Cells[BIingredientno].Value);
            }
        }

        private List<DmnInventoryRequestDetail> BuildRequestDetails()
        {
            CacheVisibleIngredientRows();
            List<DmnInventoryRequestDetail> details = new List<DmnInventoryRequestDetail>();
            int slNo = 1;

            foreach (DataGridViewRow productRow in dgv_batch.Rows)
            {
                if (productRow.IsNewRow || productRow.Cells[Bitemidno].Value == null) continue;
                int productId = Convert.ToInt32(productRow.Cells[Bitemidno].Value);
                decimal plannedQty = ToDecimal(productRow.Cells[Bplannedqtyno].Value);
                List<DmnInventoryRequestIngredient> ingredients = new List<DmnInventoryRequestIngredient>();

                if (productId == _visibleIngredientProductId)
                {
                    foreach (DataGridViewRow ingredientRow in dgv_batch_ingrediant.Rows)
                    {
                        if (ingredientRow.IsNewRow || ingredientRow.Cells[BIitemidno].Value == null) continue;
                        ingredients.Add(new DmnInventoryRequestIngredient
                        {
                            IngredientDetailId = (int)ToDecimal(ingredientRow.Cells[BIing_DetailsIdno].Value),
                            ItemId = Convert.ToInt32(ingredientRow.Cells[BIitemidno].Value),
                            IngredientName = Convert.ToString(ingredientRow.Cells[BIingredientno].Value),
                            PerPortionQty = ToDecimal(ingredientRow.Cells[Biperportionqtyno].Value),
                            TotalRequiredQty = ToDecimal(ingredientRow.Cells[BItotalrequiredqtyno].Value),
                            UnitId = ToNullableInt(ingredientRow.Cells[BIunitidno].Value),
                            UnitName = Convert.ToString(ingredientRow.Cells[BIunitno].Value),
                            CurrentStock = ToDecimal(ingredientRow.Cells[BIcurrentstockno].Value),
                            ProjectedBalance = ToDecimal(ingredientRow.Cells[BIprojectedbalanceno].Value),
                            ProductId = productId
                        });
                    }
                }
                else if (_loadedRequestDetails != null &&
                    _loadedRequestDetails.Any(x => x.RequestType == 1 && x.ProductId == productId))
                {
                    ingredients = _loadedRequestDetails
                        .Where(x => x.RequestType == 1 && x.ProductId == productId && x.ItemId.HasValue)
                        .OrderBy(x => x.SlNo)
                        .Select(x => new DmnInventoryRequestIngredient
                        {
                            IngredientDetailId = (int)(x.RecipeItemId ?? 0),
                            ItemId = Convert.ToInt32(x.ItemId.Value),
                            IngredientName = x.ItemName,
                            PerPortionQty = x.PerPortionQty ?? 0m,
                            TotalRequiredQty = (x.PerPortionQty ?? 0m) * plannedQty,
                            UnitId = x.Unit,
                            UnitName = x.UnitName,
                            CurrentStock = x.CurrentStock ?? 0m,
                            ProjectedBalance = (x.CurrentStock ?? 0m) - ((x.PerPortionQty ?? 0m) * plannedQty),
                            ProductId = productId
                        }).ToList();
                }
                else
                {
                    ingredients = LoadProductIngredients(productId, plannedQty);
                }

                if (ingredients.Count == 0)
                {
                    details.Add(new DmnInventoryRequestDetail
                    {
                        RequestType = 1, SlNo = slNo++, ProductId = productId,
                        PlannedQty = plannedQty, ProductType = 1, CreatedBy = FrmLogin.UserId,
                        CreatedOn = DateTime.Now, BranchId = FrmDashboard.BranchId, Status = 1
                    });
                    continue;
                }

                foreach (DmnInventoryRequestIngredient ingredient in ingredients)
                {
                    details.Add(new DmnInventoryRequestDetail
                    {
                        RequestType = 1, SlNo = slNo++, ProductId = productId,
                        PlannedQty = plannedQty, RecipeItemId = ingredient.IngredientDetailId,
                        PerPortionQty = ingredient.PerPortionQty,
                        TotalRequiredQty = ingredient.PerPortionQty * plannedQty,
                        ItemId = ingredient.ItemId, ProductType = 1,
                        CurrentStock = ingredient.CurrentStock,
                        ProjectedBalance = ingredient.CurrentStock - (ingredient.PerPortionQty * plannedQty),
                        Unit = ingredient.UnitId, CreatedBy = FrmLogin.UserId,
                        CreatedOn = DateTime.Now, BranchId = FrmDashboard.BranchId, Status = 1
                    });
                }
            }

            foreach (DataGridViewRow rawRow in dgv_raw.Rows)
            {
                if (rawRow.IsNewRow || rawRow.Cells[Ritemidno].Value == null) continue;
                decimal stock = ToDecimal(rawRow.Cells[Rcurrentstockno].Value);
                decimal issuedQty = ToDecimal(rawRow.Cells[Rissuedqtyno].Value);

                details.Add(new DmnInventoryRequestDetail
                {
                    RequestType = 2, SlNo = slNo++,
                    ItemId = Convert.ToInt64(rawRow.Cells[Ritemidno].Value),
                    ProductType = 2, CurrentStock = stock, IssuedQty = issuedQty,
                    ProjectedBalance = stock - issuedQty,
                    Unit = ToNullableInt(rawRow.Cells[Runitno].Value),
                    PurposeRemarks = Convert.ToString(rawRow.Cells[RpurposeRemarksno].Value),
                    CreatedBy = FrmLogin.UserId, CreatedOn = DateTime.Now,
                    BranchId = FrmDashboard.BranchId, Status = 1
                });
            }

            return details;
        }

        private void dgv_batch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == Bactionno)
            {
                DataGridViewRow row = dgv_batch.Rows[e.RowIndex];
                int productId = Convert.ToInt32(row.Cells[Bitemidno].Value);

                if (!DeleteInventoryRowFromDatabase(1, productId, null))
                    return;

                CacheVisibleIngredientRows();
                dgv_batch.Rows.RemoveAt(e.RowIndex);
                if (_loadedRequestDetails != null)
                    _loadedRequestDetails.RemoveAll(x => x.RequestType == 1 && x.ProductId == productId);

                RenumberRows(dgv_batch, Bslno);
                dgv_batch_ingrediant.Rows.Clear();
                _originalIngredientQuantities.Clear();
                _visibleIngredientProductId = 0;
                UpdateRequestTotals();
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
                CacheVisibleIngredientRows();
                UpdateRequestTotals();
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

            CacheVisibleIngredientRows();
            UpdateRequestTotals();
        }
        #endregion

        #region Main Buttons
        private void btn_saverequest_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_batch.EndEdit();
                dgv_batch_ingrediant.EndEdit();
                dgv_raw.EndEdit();

                List<DmnInventoryRequestDetail> details = BuildRequestDetails();
                if (details.Count == 0)
                {
                    ShowError("Add at least one batch product or raw material before saving the request.");
                    return;
                }

                DialogResult confirmation = CMessageBox.Show(
                    "Are you sure you want to submit this inventory request?",
                    "Confirm Inventory Request",
                    MessageBoxButtons.YesNo);

                if (confirmation != DialogResult.Yes)
                    return;

                int requestType = details.Any(x => x.RequestType == 1) && details.Any(x => x.RequestType == 2)
                    ? 3 : details[0].RequestType;

                DmnInventoryRequestMaster master = new DmnInventoryRequestMaster
                {
                    RequestId = _currentRequestId ?? 0,
                    RequestNo = txt_reqno.Text.Trim(),
                    RequestDate = dtp_reqdate.Value,
                    RequiredDate = dtp_required_date.Value,
                    DepartmentId = GetComboId(cmb_req_department),
                    EmployeeId = GetComboId(cmb_req_employee),
                    Remarks = txtremarks.Text.Trim(),
                    RequestType = requestType,
                    CreatedBy = FrmLogin.UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = _currentRequestId.HasValue ? (int?)FrmLogin.UserId : null,
                    ModifiedOn = _currentRequestId.HasValue ? (DateTime?)DateTime.Now : null,
                    BranchId = FrmDashboard.BranchId,
                    Status = 1
                };

                DmnInventoryRequestMaster savedMaster = _inventoryBll.AddMasterData(
                    "prdInsert_Upd_InventoryRequestMaster", master);

                if (savedMaster == null || savedMaster.RequestId <= 0)
                    throw new InvalidOperationException("The inventory request master procedure did not return a valid request ID.");

                int requestId = savedMaster.RequestId;
                int savedDetailCount = 0;

                foreach (DmnInventoryRequestDetail detail in details)
                {
                    detail.InventoryRequestId = requestId;
                    detail.CreatedBy = FrmLogin.UserId;
                    detail.CreatedOn = DateTime.Now;
                    detail.BranchId = FrmDashboard.BranchId;
                    detail.Status = 1;

                    DmnInventoryRequestDetail savedDetail = _inventoryBll.AddDetailsData(
                        "prdInsert_Upd_InventoryRequestDetails", detail);

                    if (savedDetail == null)
                        throw new InvalidOperationException("Unable to save inventory request detail row " + detail.SlNo + ".");

                    savedDetailCount++;
                }

                if (savedDetailCount != details.Count)
                    throw new InvalidOperationException("Some inventory request details were not saved.");

                MessageBox.Show(
                    "Inventory request saved successfully. Request No: " + master.RequestNo,
                    "Inventory Request",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ResetRequest();
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_saverequest_Click", ex);
                ShowError("Unable to save inventory request. " + ex.Message);
            }
        }

        private void btnSearchrequest_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                int returnValue = 0;
                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();

                DmnSearch objSearch = new DmnSearch();
                objSearch.ColumnHeaders = "ID,Request No,Request Date,Required Date,Request Type";
                objSearch.Select =
                    "InventoryRequestId as Id,RequestNo as Field1,CAST(RequestDate AS varchar(30)) as Field2," +
                    "CAST(RequiredDate AS varchar(30)) as Field3,"+
                    "CASE RequestType WHEN 1 THEN 'Batch Preparation' WHEN 2 THEN 'Raw Material' ELSE 'Mixed' END as Field4";
                objSearch.Table = "tblInventoryRequestMaster";
                objSearch.WhereNdGroupBy =
                    "where Status = 1 AND BranchId = " + Convert.ToString(FrmDashboard.BranchId);
                objSearch.OrderBy = " ORDER BY InventoryRequestId DESC";

                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource == null || gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records", "Empty",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var search = new FrmSearch<DmnGridFields>(
                    objSearch, SearchMode.FirstCharacter,
                    this.Width - 150, this.Height - 150,
                    this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))
                {
                    if (search.ShowDialog() == DialogResult.OK)
                        returnValue = Convert.ToInt32(search._retValue);
                }

                if (returnValue > 0)
                    FillData(returnValue);
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btnSearchrequest_Click", ex);
                ShowError("Unable to search inventory requests. " + ex.Message);
            }
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
            _currentRequestId = null;
            _currentDraftId = null;
            btn_deletesaved_draft.Visible = false;
            _loadedRequestDetails = new List<DmnInventoryRequestDetail>();
            _visibleIngredientProductId = 0;
            txt_requ_by.Clear();
            UpdateRequesterName();
            UpdateRequestTotals();
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
                    DataGridViewRow row = dgv_raw.Rows[e.RowIndex];
                    long itemId = Convert.ToInt64(row.Cells[Ritemidno].Value);

                    if (!DeleteInventoryRowFromDatabase(2, null, itemId))
                        return;

                    dgv_raw.Rows.RemoveAt(e.RowIndex);
                    if (_loadedRequestDetails != null)
                        _loadedRequestDetails.RemoveAll(x => x.RequestType == 2 && x.ItemId == itemId);

                    RenumberRows(dgv_raw, Rslno);
                    UpdateRequestTotals();
                }
            }
            catch (Exception ex) { LogInventoryException("FrmInventoryrequest dgv_raw_CellClick", ex); }
        }

        private void btn_clearall_raw_Click(object sender, EventArgs e)
        {
            try
            {
                HideItemSearchPopup();

                dgv_raw.Rows.Clear();
                dgv_raw.ClearSelection();

                txtsearch_raw.Clear();
                UpdateRequestTotals();
                txtsearch_raw.Focus();

                if (_itemSearchTextBox == txtsearch_raw)
                {
                    _itemSearchTextBox = null;
                    _itemSearchRequestType = 0;
                }
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_clearall_raw_Click", ex);
                ShowError("Unable to clear the raw material list. " + ex.Message);
            }
        }

        private void btn_addtolist_raw_Click(object sender, EventArgs e)
        {
            try
            {
                HideItemSearchPopup();
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
                row.Cells[Ractionno].Value = Properties.Resources.food_cancel_icon;
                row.Cells[Ritemidno].Value = selected.ItemId;
                row.Cells[Rproducttypeno].Value = 2;
                dgv_raw.CurrentCell = row.Cells[Rrawmaterialno];
                UpdateRequestTotals();
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
            UpdateRequestTotals();
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
            _itemSearchPopup.BorderStyle = BorderStyle.None;
            _itemSearchPopup.BackColor = Color.FromArgb(236, 220, 244);
            _itemSearchPopup.Size = new Size(430, 220);
            _itemSearchPopup.Padding = new Padding(5);

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
            _itemSearchGrid.BorderStyle = BorderStyle.None;
            _itemSearchGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            _itemSearchGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            _itemSearchGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            _itemSearchGrid.EnableHeadersVisualStyles = false;
            _itemSearchGrid.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _itemSearchGrid.ColumnHeadersHeight = 32;
            _itemSearchGrid.RowTemplate.Height = 36;
            _itemSearchGrid.BackgroundColor = Color.FromArgb(236, 220, 244);
            _itemSearchGrid.GridColor = Color.FromArgb(236, 220, 244);
            _itemSearchGrid.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(101, 80, 185);
            _itemSearchGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _itemSearchGrid.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9f, FontStyle.Bold);
            _itemSearchGrid.ColumnHeadersDefaultCellStyle.Padding = new Padding(9, 2, 9, 2);
            _itemSearchGrid.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            _itemSearchGrid.DefaultCellStyle.Font =
                new Font("Segoe UI", 9.25f, FontStyle.Regular);
            _itemSearchGrid.DefaultCellStyle.ForeColor = Color.FromArgb(45, 42, 60);
            _itemSearchGrid.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(72, 61, 190);
            _itemSearchGrid.DefaultCellStyle.SelectionForeColor = Color.White;
            _itemSearchGrid.DefaultCellStyle.Padding = new Padding(9, 2, 9, 2);
            _itemSearchGrid.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(246, 241, 250);
            _itemSearchGrid.AlternatingRowsDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(72, 61, 190);
            _itemSearchGrid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

            _itemSearchGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "searchField1",
                HeaderText = "Product",
                DataPropertyName = "Field1",
                FillWeight = 72,
                MinimumWidth = 120
            });
            _itemSearchGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "searchField2",
                HeaderText = "Category",
                DataPropertyName = "Field2",
                FillWeight = 28,
                MinimumWidth = 85
            });
            _itemSearchGrid.CellClick += ItemSearchGrid_CellClick;

            _itemSearchPopup.Controls.Add(_itemSearchGrid);
            Controls.Add(_itemSearchPopup);
            _itemSearchPopup.BringToFront();
        }

        private void ApplyRoundedItemSearchPopup()
        {
            if (_itemSearchPopup == null ||
                _itemSearchPopup.Width <= 0 || _itemSearchPopup.Height <= 0)
            {
                return;
            }

            int radius = 14;
            int diameter = radius * 2;
            Rectangle bounds = new Rectangle(0, 0,
                _itemSearchPopup.Width - 1, _itemSearchPopup.Height - 1);

            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
                path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
                path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter,
                    diameter, diameter, 0, 90);
                path.AddArc(bounds.X, bounds.Bottom - diameter,
                    diameter, diameter, 90, 90);
                path.CloseFigure();

                Region previousRegion = _itemSearchPopup.Region;
                _itemSearchPopup.Region = new Region(path);
                if (previousRegion != null)
                {
                    previousRegion.Dispose();
                }
            }
        }

        private DmnSearch CreateItemSearchCriteria(int requestType, string searchText)
        {
            string escapedSearch = (searchText ?? string.Empty).Replace("'", "''");
            DmnSearch searchCriteria = new DmnSearch();

            if (requestType == 1)
            {
                searchCriteria.ColumnHeaders = "ID,Product,Price";
                searchCriteria.Select =
                    "p.ProductId as Id,p.Product as Field1,CAST(p.Price AS VARCHAR(30)) as Field2";
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
                searchCriteria.ColumnHeaders = "ID,Product,Category";
                searchCriteria.Select =
                    "i.ItemId as Id,i.Itemname as Field1,c.CategoryName as Field2";
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
                searchCriteria, SearchMode.FirstCharacter, 0, 0, 0, 0,
                dtColumnAlign))
            {
                if (search.ShowDialog(this) == DialogResult.OK)
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
            ShowItemSearchPopup(searchTextBox, requestType, false);
        }

        private void ShowItemSearchPopup(
            TextBox searchTextBox, int requestType, bool showAllWhenEmpty)
        {
            try
            {
                string searchText = searchTextBox.Text.Trim();
                _itemSearchTextBox = searchTextBox;
                _itemSearchRequestType = requestType;

                if (searchText.Length == 0 && !showAllWhenEmpty)
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
                _itemSearchGrid.Columns["searchField1"].HeaderText = "Product";
                _itemSearchGrid.Columns["searchField2"].HeaderText =
                    requestType == 1 ? "Price" : "Category";
                _itemSearchGrid.Columns["searchField2"].FillWeight =
                    requestType == 1 ? 25 : 34;
                _itemSearchGrid.Columns["searchField1"].FillWeight =
                    requestType == 1 ? 75 : 66;
                _itemSearchGrid.Columns["searchField2"].DefaultCellStyle.Alignment =
                    requestType == 1
                        ? DataGridViewContentAlignment.MiddleRight
                        : DataGridViewContentAlignment.MiddleLeft;
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
                ApplyRoundedItemSearchPopup();
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

        private bool DeleteInventoryRowFromDatabase(int requestType, int? productId, long? itemId)
        {
            bool isDraft = _currentDraftId.HasValue;

            if (isDraft)
            {
                DialogResult confirmation = CMessageBox.Show(
                    "Remove this item from the saved draft?",
                    "Confirm Draft Item Delete",
                    MessageBoxButtons.YesNo);

                if (confirmation != DialogResult.Yes)
                    return false;
            }

            // New, unsaved requests only need an in-memory grid removal.
            if (!isDraft && !_currentRequestId.HasValue)
                return true;

            _inventoryBll.DeleteInventoryRequestRow(
                "prdDelete_InventoryRequestRow",
                isDraft,
                _currentDraftId,
                _currentRequestId,
                requestType,
                productId,
                itemId,
                FrmLogin.UserId,
                FrmDashboard.BranchId);

            return true;
        }

        private static int? GetComboId(ComboBox combo)
        {
            if (combo == null || combo.SelectedValue == null || combo.SelectedValue == DBNull.Value)
                return null;

            int value;
            return int.TryParse(Convert.ToString(combo.SelectedValue), out value)
                ? (int?)value
                : null;
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

        private bool HandleItemSearchNavigation(KeyEventArgs e, int requestType)
        {
            if (_itemSearchPopup == null || !_itemSearchPopup.Visible ||
                _itemSearchRequestType != requestType ||
                _itemSearchGrid == null || _itemSearchGrid.Rows.Count == 0)
            {
                return false;
            }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                int currentIndex = _itemSearchGrid.CurrentRow == null
                    ? 0
                    : _itemSearchGrid.CurrentRow.Index;

                int nextIndex = e.KeyCode == Keys.Down
                    ? Math.Min(_itemSearchGrid.Rows.Count - 1, currentIndex + 1)
                    : Math.Max(0, currentIndex - 1);

                _itemSearchGrid.ClearSelection();
                _itemSearchGrid.Rows[nextIndex].Selected = true;
                _itemSearchGrid.CurrentCell =
                    _itemSearchGrid.Rows[nextIndex].Cells[0];
                _itemSearchGrid.FirstDisplayedScrollingRowIndex =
                    Math.Max(0, Math.Min(nextIndex,
                        _itemSearchGrid.Rows.Count - 1));

                e.Handled = true;
                e.SuppressKeyPress = true;
                return true;
            }

            return false;
        }

        private void txtsearch_batch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (HandleItemSearchNavigation(e, 1))
                {
                    return;
                }

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
                if (HandleItemSearchNavigation(e, 2))
                {
                    return;
                }

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

        private void btn_opendraft_Click(object sender, EventArgs e)
        {
            OpenDraftWorkflow();
        }

        private void btnsave_draft_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_batch.EndEdit();
                dgv_batch_ingrediant.EndEdit();
                dgv_raw.EndEdit();

                List<DmnInventoryRequestDetail> details = BuildRequestDetails();
                if (details.Count == 0)
                {
                    ShowError("Add at least one batch product or raw material before saving a draft.");
                    return;
                }

                int requestType = details.Any(x => x.RequestType == 1) && details.Any(x => x.RequestType == 2)
                    ? 3 : details[0].RequestType;

                DmnInventoryRequestMaster draftMaster = new DmnInventoryRequestMaster
                {
                    RequestId = _currentDraftId ?? 0,
                    RequestNo = txt_reqno.Text.Trim(),
                    RequestDate = dtp_reqdate.Value,
                    RequiredDate = dtp_required_date.Value,
                    DepartmentId = GetComboId(cmb_req_department),
                    EmployeeId = GetComboId(cmb_req_employee),
                    Remarks = txtremarks.Text.Trim(),
                    RequestType = requestType,
                    CreatedBy = FrmLogin.UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = _currentDraftId.HasValue ? (int?)FrmLogin.UserId : null,
                    ModifiedOn = _currentDraftId.HasValue ? (DateTime?)DateTime.Now : null,
                    BranchId = FrmDashboard.BranchId,
                    Status = 1
                };

                string draftNo = "DR-" + DateTime.Now.ToString("yyyyMMdd-HHmmssfff");
                DmnInventoryRequestMaster savedDraftMaster = _inventoryBll.AddDraftMasterData(
                    "prdInsert_Upd_InventoryRequestDraftMaster",
                    draftNo,
                    _currentDraftId ?? 0,
                    draftMaster);

                if (savedDraftMaster == null || savedDraftMaster.DraftId <= 0)
                    throw new InvalidOperationException("The draft master procedure did not return a valid draft ID.");

                int draftId = savedDraftMaster.DraftId;
                int savedDetailCount = 0;

                foreach (DmnInventoryRequestDetail detail in details)
                {
                    detail.CreatedBy = FrmLogin.UserId;
                    detail.CreatedOn = DateTime.Now;
                    detail.BranchId = FrmDashboard.BranchId;
                    detail.Status = 1;

                    DmnInventoryRequestDetail savedDetail = _inventoryBll.AddDraftDetailsData(
                        "prdInsert_Upd_InventoryRequestDraftDetails",
                        draftId,
                        detail);

                    if (savedDetail == null)
                        throw new InvalidOperationException("Unable to save draft detail row " + detail.SlNo + ".");

                    savedDetailCount++;
                }

                if (savedDetailCount != details.Count)
                    throw new InvalidOperationException("Some draft detail rows were not saved.");

                _currentDraftId = draftId;
                _currentRequestId = null;
                btn_deletesaved_draft.Visible = true;

                MessageBox.Show("Draft saved successfully.", "Inventory Request",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btnsave_draft_Click", ex);
                ShowError("Unable to save the draft. " + ex.Message);
            }
        }

        private void btn_deletesaved_draft_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_currentDraftId.HasValue)
                    return;

                DialogResult confirmation = CMessageBox.Show(
                    "Delete this saved draft? It will be marked inactive and will not be removed permanently.",
                    "Confirm Delete Draft",
                    MessageBoxButtons.YesNo);

                if (confirmation != DialogResult.Yes)
                    return;

                _inventoryBll.DeleteInventoryRequestDraft(
                    "prdDelete_InventoryRequestDraft",
                    _currentDraftId.Value,
                    FrmLogin.UserId,
                    FrmDashboard.BranchId);

                MessageBox.Show("Draft deleted successfully.", "Inventory Request",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetRequest();
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_deletesaved_draft_Click", ex);
                ShowError("Unable to delete the saved draft. " + ex.Message);
            }
        }
    }
}