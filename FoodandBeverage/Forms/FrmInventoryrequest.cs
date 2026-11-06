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
using System.Globalization;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using FoodandBeverage.Forms.UserControls;

namespace FoodandBeverage.Forms
{
    public partial class FrmInventoryrequest : Form
    {
        private readonly BllInventoryRequest _inventoryBll = new BllInventoryRequest();
        private BlurPopupOverlay _blurPopupOverlay;
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
        private int _currentApprovalStatus; // 0 Pending, 1 Approved, 2 Rejected, 3 Sent Back
        private string _currentDraftNo;
        private int _visibleIngredientProductId;
        private List<DmnInventoryRequestDetail> _loadedRequestDetails = new List<DmnInventoryRequestDetail>();

        ClsGeneral clsgen = new ClsGeneral();

        #region Database Search Helpers
        private static string EscapeSearchText(string value)
        {
            return (value ?? string.Empty).Replace("'", "''");
        }

        private List<DmnInventoryRequestItem> SearchBatchProducts(string search)
        {
            string escapedSearch = EscapeSearchText(search);
            DmnSearch criteria = new DmnSearch
            {
                Select = "p.ProductId AS ItemId, p.Product AS ItemName, " +
                         "CAST(0 AS DECIMAL(18,2)) AS CurrentStock, CAST(1 AS INT) AS ProductType",
                Table = "tblProduct p",
                WhereNdGroupBy = "WHERE ISNULL(p.Status,1) <> 0 AND " +
                    "(N'" + escapedSearch + "' = N'' OR p.Product LIKE N'%' + N'" +
                    escapedSearch + "' + N'%')",
                OrderBy = "ORDER BY p.Product"
            };

            BllSearch<DmnInventoryRequestItem> bllSearch =
                new BllSearch<DmnInventoryRequestItem>();
            List<DmnInventoryRequestItem> results =
                bllSearch.GetSearchData("prdSearch", criteria);

            return results ?? new List<DmnInventoryRequestItem>();
        }

        private List<DmnInventoryRequestItem> SearchRawMaterialItems(string search)
        {
            string escapedSearch = EscapeSearchText(search);
            DmnSearch criteria = new DmnSearch
            {
                Select = "CAST(i.ItemId AS INT) AS ItemId, i.Itemname AS ItemName, " +
                         "ISNULL(pr.QOH,0) * CASE WHEN ISNULL(i.Conversionfactor,0) > 0 " +
                         "THEN i.Conversionfactor ELSE 1 END AS CurrentStock, i.Baseunit AS UnitId, " +
                         "u.UnitName AS UnitName, CAST(2 AS INT) AS ProductType",
                Table = "tblItem i " +
                        "LEFT JOIN tblUnit u ON u.UId = i.Baseunit " +
                        "LEFT JOIN (SELECT ProductId, SUM(ISNULL(QOH,0)) AS QOH " +
                        "FROM tblProductRate WHERE Status = 1 GROUP BY ProductId) pr " +
                        "ON pr.ProductId = i.ItemId",
                WhereNdGroupBy = "WHERE ISNULL(i.Saletype,0) = 0 AND ISNULL(i.Status,1) <> 0 " +
                    "AND (N'" + escapedSearch + "' = N'' OR i.Itemname LIKE N'%' + N'" +
                    escapedSearch + "' + N'%')",
                OrderBy = "ORDER BY i.Itemname"
            };

            BllSearch<DmnInventoryRequestItem> bllSearch =
                new BllSearch<DmnInventoryRequestItem>();
            List<DmnInventoryRequestItem> results =
                bllSearch.GetSearchData("prdSearch", criteria);

            return results ?? new List<DmnInventoryRequestItem>();
        }

        private List<DmnInventoryRequestIngredient> LoadProductIngredients(int productId, decimal plannedQty)
        {
            string plannedQuantity = plannedQty.ToString(CultureInfo.InvariantCulture);
            DmnSearch criteria = new DmnSearch
            {
                Select = "d.Ing_DetailsId AS IngredientDetailId, d.ItemId, " +
                         "i.Itemname AS IngredientName, ISNULL(d.quantity,0) AS PerPortionQty, " +
                         "(ISNULL(d.quantity,0) * " + plannedQuantity + ") AS TotalRequiredQty, " +
                         "d.Unit AS UnitId, u.UnitName AS UnitName, " +
                         "CASE WHEN d.Unit = i.unit " +
                         "THEN ISNULL(pr.QOH,0) " +
                         "ELSE ISNULL(pr.QOH,0) * CASE " +
                         "WHEN ISNULL(i.Baseunit,i.unit) = i.unit THEN 1 " +
                         "ELSE ISNULL(i.Conversionfactor,0) END END AS CurrentStock, " +
                         "(CASE WHEN d.Unit = i.unit " +
                         "THEN ISNULL(pr.QOH,0) " +
                         "ELSE ISNULL(pr.QOH,0) * CASE " +
                         "WHEN ISNULL(i.Baseunit,i.unit) = i.unit THEN 1 " +
                         "ELSE ISNULL(i.Conversionfactor,0) END END - ISNULL(d.quantity,0) * " +
                         plannedQuantity + ") AS ProjectedBalance, d.ProductId",
                Table = "tblIngredientDetails d " +
                        "LEFT JOIN tblItem i ON i.ItemId = d.ItemId " +
                        "LEFT JOIN tblUnit u ON u.UId = d.Unit " +
                        "LEFT JOIN (SELECT ProductId, SUM(ISNULL(QOH,0)) AS QOH " +
                        "FROM tblProductRate WHERE Status = 1 GROUP BY ProductId) pr " +
                        "ON pr.ProductId = d.ItemId",
                WhereNdGroupBy = "WHERE d.ProductId = " + productId +
                                 " AND ISNULL(d.ProductType,1) = 1 " +
                                 " AND ISNULL(d.Status,1) <> 0",
                OrderBy = "ORDER BY d.Ing_DetailsId"
            };

            BllSearch<DmnInventoryRequestIngredient> bllSearch =
                new BllSearch<DmnInventoryRequestIngredient>();
            List<DmnInventoryRequestIngredient> results =
                bllSearch.GetSearchData("prdSearch", criteria);

            return results ?? new List<DmnInventoryRequestIngredient>();
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
        public int BInventoryRequestDetailIdno = 7;

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
        public int BIInventoryRequestDetailIdno = 11;

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
        public int RInventoryRequestDetailIdno = 10; 
        #endregion

        public FrmInventoryrequest() 
        {
            InitializeComponent();

            // Reuse the same low-memory blurred/dimmed popup backdrop as FrmOrderConsole.
            // BlurPopupOverlay listens to pnl_reqapproval.VisibleChanged and manages
            // showing, hiding, resizing and releasing the captured image automatically.
            _blurPopupOverlay = new BlurPopupOverlay(this, pnl_reqapproval);

            btn_deletesaved_draft.Visible = false;
            InitializeItemSearchPopup();
            ConfigureRequestGrids();
            InitializeRequestApprovalPopup();
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
            txt_requ_by.Enabled = true;
            txt_batchfoodqty.Enabled = true;
            txt_totalrawmaterialqty.Enabled = true;
            total_food_raw_qty.Enabled = true;
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
            dgv_batch.Columns[BInventoryRequestDetailIdno].Visible = false;

            dgv_batch_ingrediant.Columns[BIproductidno].Visible = false;
            dgv_batch_ingrediant.Columns[BIing_DetailsIdno].Visible = false;
            if (!dgv_batch_ingrediant.Columns.Contains("biItemId"))
                dgv_batch_ingrediant.Columns.Add(new DataGridViewTextBoxColumn { Name = "biItemId", Visible = false });
            if (!dgv_batch_ingrediant.Columns.Contains("biUnitId"))
                dgv_batch_ingrediant.Columns.Add(new DataGridViewTextBoxColumn { Name = "biUnitId", Visible = false });
            if (!dgv_batch_ingrediant.Columns.Contains("biInventoryRequestDetailId"))
            {
                dgv_batch_ingrediant.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "biInventoryRequestDetailId",
                    HeaderText = "InventoryRequestDetailId",
                    Visible = false
                });
            }
            BIitemidno = dgv_batch_ingrediant.Columns["biItemId"].Index;
            BIunitidno = dgv_batch_ingrediant.Columns["biUnitId"].Index;
            BIInventoryRequestDetailIdno = dgv_batch_ingrediant.Columns["biInventoryRequestDetailId"].Index;

            ApplyBatchIngredientGridLayout();

            ApplyRawMaterialGridLayout();
            dgv_raw.Columns[Ritemidno].Visible = false;
            dgv_raw.Columns[Rproducttypeno].Visible = false;
            dgv_raw.Columns[RInventoryRequestDetailIdno].Visible = false;

            // Keep the numeric base-unit ID hidden; show UnitName in the visible Unit column.
            if (!dgv_raw.Columns.Contains("rawUnitId"))
                dgv_raw.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "rawUnitId",
                    HeaderText = "UnitId",
                    Visible = false
                });

            dgv_batch.Columns[Bactionno].ReadOnly = true;
            dgv_batch.Columns[Bmenuitemno].ReadOnly = true;
            dgv_batch.Columns[Brecipeingredientsno].ReadOnly = true;

            // Product names are display-only; quantities remain editable.
            dgv_raw.Columns[Rrawmaterialno].ReadOnly = true;

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

        private void ApplyBatchIngredientGridLayout()
        {
            DataGridView grid = dgv_batch_ingrediant;
            if (grid == null || grid.Columns.Count <= BIprojectedbalanceno)
                return;

            // Keep every ingredient column visible in the available client width.
            // The horizontal scrollbar previously appeared because designer AllCells
            // sizing and the original minimum widths competed with the Fill layout.
            grid.ScrollBars = ScrollBars.Vertical;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid.RowTemplate.Height = 36;

            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (!column.Visible)
                    continue;

                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }

            ApplyResponsiveFillColumnLayout(
                grid,
                new[] { BIslno, BIingredientno, Biperportionqtyno, BItotalrequiredqtyno,
                        BIunitno, BIcurrentstockno, BIprojectedbalanceno },
                new[] { 6F, 25F, 12F, 14F, 8F, 16F, 19F },
                new[] { 38, 105, 75, 82, 55, 86, 92 });
        }

        private static void ApplyResponsiveFillColumnLayout(
            DataGridView grid,
            int[] columnIndexes,
            float[] fillWeights,
            int[] preferredMinimumWidths)
        {
            if (grid == null || columnIndexes == null || fillWeights == null ||
                preferredMinimumWidths == null ||
                columnIndexes.Length != fillWeights.Length ||
                columnIndexes.Length != preferredMinimumWidths.Length)
            {
                return;
            }

            int availableWidth = Math.Max(
                1,
                grid.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 8);

            int totalPreferredWidth = preferredMinimumWidths.Sum();
            double widthScale = totalPreferredWidth > availableWidth && totalPreferredWidth > 0
                ? (double)availableWidth / totalPreferredWidth
                : 1D;

            for (int i = 0; i < columnIndexes.Length; i++)
            {
                int columnIndex = columnIndexes[i];
                if (columnIndex < 0 || columnIndex >= grid.Columns.Count)
                    continue;

                DataGridViewColumn column = grid.Columns[columnIndex];
                if (!column.Visible)
                    continue;

                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.FillWeight = fillWeights[i];
                column.MinimumWidth = Math.Max(
                    28,
                    (int)Math.Floor(preferredMinimumWidths[i] * widthScale));
            }
        }

        private void ApplyRawMaterialGridLayout()
        {
            if (dgv_raw == null || dgv_raw.Columns.Count <= Ractionno)
                return;

            // Keep the raw-material grid readable at different screen resolutions.
            // Extra width is shared by the item name and purpose/remarks columns.
            dgv_raw.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn column in dgv_raw.Columns)
            {
                if (column.Visible)
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgv_raw.Columns[Rslno].FillWeight = 6F;
            dgv_raw.Columns[Rslno].MinimumWidth = 45;

            dgv_raw.Columns[Rrawmaterialno].FillWeight = 22F;
            dgv_raw.Columns[Rrawmaterialno].MinimumWidth = 130;

            dgv_raw.Columns[Rcurrentstockno].FillWeight = 14F;
            dgv_raw.Columns[Rcurrentstockno].MinimumWidth = 110;

            dgv_raw.Columns[Rissuedqtyno].FillWeight = 10F;
            dgv_raw.Columns[Rissuedqtyno].MinimumWidth = 85;

            dgv_raw.Columns[Runitno].FillWeight = 10F;
            dgv_raw.Columns[Runitno].MinimumWidth = 85;

            dgv_raw.Columns[Rprojectbalanceno].FillWeight = 15F;
            dgv_raw.Columns[Rprojectbalanceno].MinimumWidth = 115;

            dgv_raw.Columns[RpurposeRemarksno].FillWeight = 18F;
            dgv_raw.Columns[RpurposeRemarksno].MinimumWidth = 145;

            dgv_raw.Columns[Ractionno].FillWeight = 5F;
            dgv_raw.Columns[Ractionno].MinimumWidth = 38;

            // Wrap long values and headers instead of displaying ellipses.
            dgv_raw.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv_raw.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv_raw.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv_raw.RowTemplate.Height = 36;

            dgv_raw.Columns[Rrawmaterialno].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv_raw.Columns[Runitno].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv_raw.Columns[Rprojectbalanceno].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv_raw.Columns[RpurposeRemarksno].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void InventoryGrid_Resize(object sender, EventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid == null)
                return;

            if (grid == dgv_raw)
            {
                ApplyRawMaterialGridLayout();
                return;
            }

            if (grid == dgv_batch_ingrediant)
            {
                ApplyBatchIngredientGridLayout();
                return;
            }

            // Reapply Fill mode after the parent layout recalculates its client width.
            if (grid.AutoSizeColumnsMode != DataGridViewAutoSizeColumnsMode.Fill)
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            CenterApprovalPopup();
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
                row.Cells[BInventoryRequestDetailIdno].Value = 0;
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
            DmnSearch criteria = new DmnSearch
            {
                Select = "p.ProductId AS ItemId, p.Product AS ItemName, " +
                         "CAST(0 AS DECIMAL(18,2)) AS CurrentStock, CAST(1 AS INT) AS ProductType",
                Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON c.Fcid = p.Fcid",
                WhereNdGroupBy = "WHERE p.ProductId = " + productId +
                                 " AND p.Status = 1 AND c.Status = 1",
                OrderBy = ""
            };

            BllSearch<DmnInventoryRequestItem> bllSearch =
                new BllSearch<DmnInventoryRequestItem>();
            List<DmnInventoryRequestItem> results =
                bllSearch.GetSearchData("prdSearch", criteria);

            return results != null && results.Count > 0 ? results[0] : null;
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
                        InventoryRequestDetailId = x.InventoryRequestDetailId,
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
                    row.Cells[BIInventoryRequestDetailIdno].Value = ingredient.InventoryRequestDetailId;
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
                int inventoryRequestDetailId;
                if (!long.TryParse(Convert.ToString(row.Cells[BIitemidno].Value), out itemId)) continue;
                long.TryParse(Convert.ToString(row.Cells[BIing_DetailsIdno].Value), out recipeItemId);
                int.TryParse(Convert.ToString(row.Cells[BIInventoryRequestDetailIdno].Value), out inventoryRequestDetailId);

                DmnInventoryRequestDetail saved = inventoryRequestDetailId > 0
                    ? _loadedRequestDetails.FirstOrDefault(x => x.InventoryRequestDetailId == inventoryRequestDetailId)
                    : _loadedRequestDetails.FirstOrDefault(x =>
                        x.RequestType == 1 && x.ProductId == _visibleIngredientProductId &&
                        x.ItemId == itemId && x.RecipeItemId == recipeItemId);

                if (saved == null)
                {
                    saved = new DmnInventoryRequestDetail
                    {
                        InventoryRequestDetailId = inventoryRequestDetailId,
                        RequestType = 1, ProductId = _visibleIngredientProductId,
                        ItemId = itemId, RecipeItemId = recipeItemId, ProductType = 1, Status = 1
                    };
                    _loadedRequestDetails.Add(saved);
                }

                saved.InventoryRequestDetailId = inventoryRequestDetailId;
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
                            InventoryRequestDetailId = (int)ToDecimal(ingredientRow.Cells[BIInventoryRequestDetailIdno].Value),
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
                            InventoryRequestDetailId = x.InventoryRequestDetailId,
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
                        InventoryRequestDetailId = (int)ToDecimal(productRow.Cells[BInventoryRequestDetailIdno].Value),
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
                        InventoryRequestDetailId = ingredient.InventoryRequestDetailId,
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
                    InventoryRequestDetailId = (int)ToDecimal(rawRow.Cells[RInventoryRequestDetailIdno].Value),
                    RequestType = 2, SlNo = slNo++,
                    ItemId = Convert.ToInt64(rawRow.Cells[Ritemidno].Value),
                    ProductType = 2, CurrentStock = stock, IssuedQty = issuedQty,
                    ProjectedBalance = stock - issuedQty,
                    Unit = ToNullableInt(rawRow.Cells["rawUnitId"].Value),
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
                // Existing submitted rows can be removed only while correcting a Sent Back request.
                if (_currentRequestId.HasValue && _currentApprovalStatus != 3)
                    return;

                DataGridViewRow row = dgv_batch.Rows[e.RowIndex];
                int productId = Convert.ToInt32(row.Cells[Bitemidno].Value);

                if (!ConfirmInventoryRowRemoval())
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
                if (_currentRequestId.HasValue && _currentApprovalStatus != 3)
                {
                    ShowError("Only a Sent Back request can be edited and resubmitted.");
                    return;
                }

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

                DmnInventoryRequestMaster savedMaster = _inventoryBll.AddMasterData( "prdInsert_Upd_InventoryRequestMaster", master);

                if (savedMaster == null ||
                    (savedMaster.InventoryRequestId <= 0 && savedMaster.RequestId <= 0))
                    throw new InvalidOperationException("The inventory request master procedure did not return a valid request ID.");

                int requestId = savedMaster.InventoryRequestId > 0
                    ? savedMaster.InventoryRequestId
                    : savedMaster.RequestId;
                int savedDetailCount = 0;
                List<int> retainedDetailIds = new List<int>();

                foreach (DmnInventoryRequestDetail detail in details)
                {
                    detail.InventoryRequestId = requestId;
                    detail.BranchId = FrmDashboard.BranchId;
                    detail.Status = 1;

                    if (detail.InventoryRequestDetailId > 0)
                    {
                        // Existing Sent Back detail: update this row and preserve Created audit fields.
                        detail.ModifiedBy = FrmLogin.UserId;
                        detail.ModifiedOn = DateTime.Now;
                    }
                    else
                    {
                        // New detail: the existing stored procedure inserts the row and returns its ID.
                        detail.CreatedBy = FrmLogin.UserId;
                        detail.CreatedOn = DateTime.Now;
                    }

                    DmnInventoryRequestDetail savedDetail =
                        _inventoryBll.AddDetailsData(
                            "prdInsert_Upd_InventoryRequestDetails",
                            detail);

                    if (savedDetail == null)
                        throw new InvalidOperationException(
                            "Unable to save inventory request detail row " + detail.SlNo + ".");

                    int savedDetailId = savedDetail.InventoryRequestDetailId > 0
                        ? savedDetail.InventoryRequestDetailId
                        : detail.InventoryRequestDetailId;

                    if (savedDetailId <= 0)
                    {
                        savedDetailId = _inventoryBll.FindInventoryRequestDetailId(
                            requestId,
                            FrmDashboard.BranchId,
                            detail);
                    }

                    if (savedDetailId <= 0)
                    {
                        throw new InvalidOperationException(
                            "The detail procedure did not return InventoryRequestDetailId for row " +
                            detail.SlNo + ". The request was not resubmitted.");
                    }

                    detail.InventoryRequestDetailId = savedDetailId;
                    retainedDetailIds.Add(savedDetailId);
                    savedDetailCount++;
                }

                if (savedDetailCount != details.Count)
                    throw new InvalidOperationException("Some inventory request details were not saved.");

                // Mark removed/duplicate active rows inactive in the existing detail table.
                _inventoryBll.DeactivateInventoryRequestDetailsNotInIds(
                    requestId,
                    FrmDashboard.BranchId,
                    FrmLogin.UserId,
                    retainedDetailIds);

                // After correcting a Sent Back request, return it to Pending for review.
                // The stored procedure preserves the Send Back remarks for the request list.
                if (_currentRequestId.HasValue && _currentApprovalStatus == 3)
                {
                    int employeeId = FrmLogin.EmployeeId > 0
                        ? FrmLogin.EmployeeId
                        : FrmLogin.UserId;

                    _inventoryBll.UpdateInventoryRequestApproval(
                        "dbo.prdUpdate_InventoryRequestApproval",
                        requestId,
                        0,
                        employeeId,
                        null,
                        FrmDashboard.BranchId);
                }

                // The request and all its detail rows are saved; now close the source draft.
                if (_currentDraftId.HasValue && _currentDraftId.Value > 0)
                {
                    _inventoryBll.DeleteInventoryRequestDraft(
                        "prdDelete_InventoryRequestDraft",
                        _currentDraftId.Value,
                        FrmLogin.UserId,
                        FrmDashboard.BranchId);
                }

                CMessageBox.Show(
                    "Inventory request saved successfully. Request No: " + master.RequestNo,
                    "Inventory Request",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ResetRequest();
                SetRequestViewMode(false);
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
                {
                    FillData(returnValue);
                    SetRequestViewMode(true);
                }
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btnSearchrequest_Click", ex);
                ShowError("Unable to search inventory requests. " + ex.Message);
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ResetRequest();
                SetRequestViewMode(false);
            }
            catch (Exception ex) { LogInventoryException("FrmInventoryrequest btnrefresh_Click", ex); }
        }

        private void SetRequestViewMode(bool viewingExistingRequest)
        {
            // Submitted requests are locked except when a manager has sent them back.
            bool canEditSentBack = viewingExistingRequest &&
                                   _currentRequestId.HasValue &&
                                   _currentApprovalStatus == 3;
            bool readOnly = viewingExistingRequest && !canEditSentBack;

            btnsave_draft.Visible = !viewingExistingRequest;
            btn_saverequest.Visible = !viewingExistingRequest || canEditSentBack;
            btnrefresh.Visible = true;
            btn_deletesaved_draft.Visible = !viewingExistingRequest && _currentDraftId.HasValue;

            txt_reqno.ReadOnly = viewingExistingRequest;
            dtp_reqdate.Enabled = !readOnly;
            dtp_required_date.Enabled = !readOnly;
            cmb_req_department.Enabled = !readOnly;
            cmb_req_employee.Enabled = !readOnly;
            txtremarks.ReadOnly = readOnly;

            dgv_batch.ReadOnly = readOnly;
            dgv_raw.ReadOnly = readOnly;
            dgv_batch_ingrediant.ReadOnly = readOnly;

            btn_addtolist_batch.Enabled = !readOnly;
            btn_addtolist_raw.Enabled = !readOnly;
            btn_clearall_batch.Enabled = !readOnly;
            btn_clearall_raw.Enabled = !readOnly;
            btn_importbatch.Enabled = !readOnly;
            btn_import_raw.Enabled = !readOnly;
            txtsearch_batch.Enabled = !readOnly;
            txtsearch_raw.Enabled = !readOnly;

            if (canEditSentBack)
            {
                CMessageBox.Show(
                    "This request was sent back for correction. Update the details and click Save Request to resubmit it for approval.",
                    "Request Sent Back",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
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
            _currentDraftNo = null;
            _currentApprovalStatus = 0;
            btn_deletesaved_draft.Visible = false;
            _loadedRequestDetails = new List<DmnInventoryRequestDetail>();
            _visibleIngredientProductId = 0;
            txt_requ_by.Text = "";
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
                    if (_currentRequestId.HasValue && _currentApprovalStatus != 3)
                        return;

                    DataGridViewRow row = dgv_raw.Rows[e.RowIndex];
                    long itemId = Convert.ToInt64(row.Cells[Ritemidno].Value);

                    if (!ConfirmInventoryRowRemoval())
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
                row.Cells[Runitno].Value = selected.UnitName;
                row.Cells["rawUnitId"].Value = selected.UnitId;
                row.Cells[Rprojectbalanceno].Value = selected.CurrentStock - 1m;
                row.Cells[RpurposeRemarksno].Value = string.Empty;
                row.Cells[Ractionno].Value = Properties.Resources.food_cancel_icon;
                row.Cells[Ritemidno].Value = selected.ItemId;
                row.Cells[Rproducttypeno].Value = 2;
                row.Cells[RInventoryRequestDetailIdno].Value = 0;
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
            DmnSearch criteria = new DmnSearch
            {
                Select = "CAST(i.ItemId AS INT) AS ItemId, i.Itemname AS ItemName, " +
                         "ISNULL(pr.QOH,0) * CASE WHEN ISNULL(i.Conversionfactor,0) > 0 " +
                         "THEN i.Conversionfactor ELSE 1 END AS CurrentStock, i.Baseunit AS UnitId, " +
                         "u.UnitName AS UnitName, CAST(2 AS INT) AS ProductType",
                Table = "tblItem i " +
                        "LEFT JOIN tblUnit u ON u.UId = i.Baseunit " +
                        "LEFT JOIN tblCategory_FTI c ON c.Fcid = i.Fcid " +
                        "LEFT JOIN (SELECT ProductId, SUM(ISNULL(QOH,0)) AS QOH " +
                        "FROM tblProductRate WHERE Status = 1 GROUP BY ProductId) pr " +
                        "ON pr.ProductId = i.ItemId",
                WhereNdGroupBy = "WHERE i.ItemId = " + itemId +
                                 " AND i.Status = 1 AND i.Saletype = 0 AND c.Status = 1",
                OrderBy = ""
            };

            BllSearch<DmnInventoryRequestItem> bllSearch =
                new BllSearch<DmnInventoryRequestItem>();
            List<DmnInventoryRequestItem> results =
                bllSearch.GetSearchData("prdSearch", criteria);

            return results != null && results.Count > 0 ? results[0] : null;
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

        private bool ConfirmInventoryRowRemoval()
        {
            // Removing a row is an in-memory operation only.
            // The database detail snapshot is updated when Save Request / Save Draft runs:
            // previous active details are marked Status = 0 by the master update procedure,
            // and the remaining grid rows are saved with Status = 1.
            if (!_currentDraftId.HasValue)
                return true;

            DialogResult confirmation = CMessageBox.Show(
                "Remove this item from the saved draft? The change will be stored when you save the draft.",
                "Confirm Draft Item Removal",
                MessageBoxButtons.YesNo);

            return confirmation == DialogResult.Yes;
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
            CMessageBox.Show(message, "Inventory Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                int requestType = details.Any(x => x.RequestType == 1) &&
                                  details.Any(x => x.RequestType == 2)
                    ? 3
                    : details[0].RequestType;

                DmnInventoryRequestMaster draftMaster = new DmnInventoryRequestMaster
                {
                    DraftId = _currentDraftId ?? 0,
                    DraftNo = string.IsNullOrWhiteSpace(_currentDraftNo)
                        ? "DR-" + DateTime.Now.ToString("yyyyMMdd-HHmmssfff")
                        : _currentDraftNo,
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

                DmnInventoryRequestMaster savedDraftMaster =
                    _inventoryBll.AddDraftMasterData(
                        "prdInsert_Upd_InventoryRequestDraftMaster",
                        draftMaster);

                if (savedDraftMaster == null || savedDraftMaster.DraftId <= 0)
                {
                    throw new InvalidOperationException(
                        "The draft master procedure did not return a valid draft ID.");
                }

                int draftId = savedDraftMaster.DraftId;
                _currentDraftNo = string.IsNullOrWhiteSpace(savedDraftMaster.DraftNo)
                    ? draftMaster.DraftNo
                    : savedDraftMaster.DraftNo;

                int savedDetailCount = 0;

                foreach (DmnInventoryRequestDetail detail in details)
                {
                    detail.DraftId = draftId;
                    detail.CreatedBy = FrmLogin.UserId;
                    detail.CreatedOn = DateTime.Now;
                    detail.BranchId = FrmDashboard.BranchId;
                    detail.Status = 1;

                    DmnInventoryRequestDetail savedDetail =
                        _inventoryBll.AddDraftDetailsData(
                            "prdInsert_Upd_InventoryRequestDraftDetails",
                            detail);

                    if (savedDetail == null)
                    {
                        throw new InvalidOperationException(
                            "Unable to save draft detail row " + detail.SlNo + ".");
                    }

                    savedDetailCount++;
                }

                if (savedDetailCount != details.Count)
                {
                    throw new InvalidOperationException(
                        "Some draft detail rows were not saved.");
                }

                _currentDraftId = draftId;
                _currentRequestId = null;
                btn_deletesaved_draft.Visible = true;

                CMessageBox.Show(
                    "Draft saved successfully.",
                    "Inventory Request",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
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

        #region Request Approval Popup State
        private DataTable _requestApprovalData;
        private DataTable _requestApprovalView;
        private int _requestApprovalPage = 1;
        private const int RequestApprovalPageSize = 8;
        private string _requestApprovalStatus = "All";
        private bool _requestApprovalEventsWired;

        private void InitializeRequestApprovalPopup()
        {
            pnl_reqapproval.Visible = false;
            pnl_reqapproval.BringToFront();
            pnl_reqapproval.BackColor = Color.White;
            pnl_reqapproval.BorderStyle = BorderStyle.FixedSingle;
            pnl_reqapproval.Anchor = AnchorStyles.None;

            dgv_req_app.AutoGenerateColumns = false;
            dgv_req_app.Columns.Clear();
            dgv_req_app.AllowUserToAddRows = false;
            dgv_req_app.AllowUserToDeleteRows = false;
            dgv_req_app.AllowUserToResizeRows = false;
            dgv_req_app.ReadOnly = true;
            dgv_req_app.MultiSelect = false;
            dgv_req_app.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_req_app.RowHeadersVisible = false;
            dgv_req_app.BorderStyle = BorderStyle.None;
            dgv_req_app.BackgroundColor = Color.FromArgb(245, 241, 252);
            dgv_req_app.EnableHeadersVisualStyles = false;
            dgv_req_app.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(99, 76, 184);
            dgv_req_app.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_req_app.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgv_req_app.ColumnHeadersHeight = 42;
            dgv_req_app.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            // Override the 12 pt bold font assigned to RowTemplate in the Designer.
            // That larger font was forcing Request No and Type text to be clipped.
            dgv_req_app.RowTemplate.DefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Regular);
            dgv_req_app.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv_req_app.DefaultCellStyle.ForeColor = Color.FromArgb(35, 35, 65);
            dgv_req_app.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 216, 250);
            dgv_req_app.DefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 35, 65);
            dgv_req_app.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(239, 235, 250);
            dgv_req_app.RowTemplate.Height = 38;

            // Keep approval columns at readable widths and allow horizontal navigation.
            // The total column width intentionally exceeds the popup grid viewport.
            dgv_req_app.ScrollBars = ScrollBars.Both;
            dgv_req_app.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv_req_app.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv_req_app.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv_req_app.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "InventoryRequestId",
                DataPropertyName = "InventoryRequestId",
                Visible = false
            });
            AddApprovalGridColumn("SlNo", "Sl No", 55);
            AddApprovalGridColumn("RequestNo", "Request No", 230);
            AddApprovalGridColumn("RequestDate", "Request Date", 120);
            AddApprovalGridColumn("RequiredDate", "Required Date", 120);
            AddApprovalGridColumn("DepartmentName", "Department", 135);
            AddApprovalGridColumn("EmployeeName", "Requested By", 130);
            AddApprovalGridColumn("RequestTypeName", "Type", 200);
            AddApprovalGridColumn("ApprovalStatusText", "Status", 105);
            AddApprovalGridColumn("ApprovedByName", "Approved By", 135);
            AddApprovalGridColumn("ApprovedOnText", "Approved On", 140);
            AddApprovalGridColumn("ApprovalRemarks", "Remarks", 220);
            dgv_req_app.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "ViewRequest",
                HeaderText = "",
                Text = "View",
                UseColumnTextForButtonValue = true,
                Width = 75,
                MinimumWidth = 75,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                FlatStyle = FlatStyle.Flat
            });

            if (!_requestApprovalEventsWired)
            {
                txt_search_reqapp.TextChanged += txt_search_reqapp_TextChanged;

                // Each status tab is a three-control group: TableLayoutPanel + Label + PictureBox.
                WireApprovalTab(tlp_all, lbl_all, pb_all, "All");
                WireApprovalTab(tlp_pending, lbl_pending, pb_pending, "Pending");
                WireApprovalTab(tlp_approved, lbl_approved, pb_approved, "Approved");
                WireApprovalTab(tlp_rejected, lbl_rejected, pb_rejected, "Rejected");

                _requestApprovalEventsWired = true;
            }
            UpdateApprovalTabAppearance();
        }

        private void WireApprovalTab(TableLayoutPanel tab, Label caption, PictureBox icon, string status)
        {
            tab.Tag = status;
            caption.Tag = status;
            icon.Tag = status;

            tab.Cursor = Cursors.Hand;
            caption.Cursor = Cursors.Hand;
            icon.Cursor = Cursors.Hand;

            tab.Click += ApprovalTab_Click;
            caption.Click += ApprovalTab_Click;
            icon.Click += ApprovalTab_Click;

            tab.MouseEnter += ApprovalTab_MouseEnter;
            caption.MouseEnter += ApprovalTab_MouseEnter;
            icon.MouseEnter += ApprovalTab_MouseEnter;

            tab.MouseLeave += ApprovalTab_MouseLeave;
            caption.MouseLeave += ApprovalTab_MouseLeave;
            icon.MouseLeave += ApprovalTab_MouseLeave;
        }

        private void ApprovalTab_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control == null) return;
            SetApprovalStatusFilter(Convert.ToString(control.Tag));
        }

        private void ApprovalTab_MouseEnter(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control == null) return;
            string status = Convert.ToString(control.Tag);
            TableLayoutPanel tab = GetApprovalTab(status);
            if (tab != null && status != _requestApprovalStatus)
                tab.BackColor = GetApprovalTabHoverColor(status);
        }

        private void ApprovalTab_MouseLeave(object sender, EventArgs e)
        {
            UpdateApprovalTabAppearance();
        }

        private TableLayoutPanel GetApprovalTab(string status)
        {
            switch (status)
            {
                case "All": return tlp_all;
                case "Pending": return tlp_pending;
                case "Approved": return tlp_approved;
                case "Rejected": return tlp_rejected;
                default: return null;
            }
        }

        private Color GetApprovalTabBaseColor(string status)
        {
            switch (status)
            {
                case "All": return Color.FromArgb(238, 233, 255);
                case "Pending": return Color.FromArgb(255, 242, 218);
                case "Approved": return Color.FromArgb(226, 247, 226);
                case "Rejected": return Color.FromArgb(255, 230, 230);
                default: return Color.White;
            }
        }

        private Color GetApprovalTabSelectedColor(string status)
        {
            switch (status)
            {
                case "All": return Color.FromArgb(76, 55, 220);
                case "Pending": return Color.FromArgb(239, 145, 28);
                case "Approved": return Color.FromArgb(35, 166, 75);
                case "Rejected": return Color.FromArgb(218, 63, 63);
                default: return Color.FromArgb(99, 76, 184);
            }
        }

        private Color GetApprovalTabHoverColor(string status)
        {
            switch (status)
            {
                case "All": return Color.FromArgb(220, 211, 255);
                case "Pending": return Color.FromArgb(255, 228, 185);
                case "Approved": return Color.FromArgb(204, 239, 207);
                case "Rejected": return Color.FromArgb(255, 207, 207);
                default: return Color.LightGray;
            }
        }

        private void AddApprovalGridColumn(string name, string header, int width)
        {
            dgv_req_app.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                DataPropertyName = name,
                Width = width,
                MinimumWidth = width,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    WrapMode = DataGridViewTriState.False
                },
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
        }

        private void CenterApprovalPopup()
        {
            int width = Math.Min(1250, Math.Max(760, ClientSize.Width - 40));
            int height = Math.Min(690, Math.Max(430, ClientSize.Height - 40));
            pnl_reqapproval.Size = new Size(width, height);
            pnl_reqapproval.Location = new Point(
                Math.Max(0, (ClientSize.Width - width) / 2),
                Math.Max(0, (ClientSize.Height - height) / 2));
            pnl_reqapproval.BringToFront();
        }

        private void LoadRequestApprovalData()
        {
            // Detect approval fields through the same BllSearch/prdSearch data-access pattern.
            BllSearch<DmnGridFields> bllSchemaSearch = new BllSearch<DmnGridFields>();
            DmnSearch approvalColumnSearch = new DmnSearch
            {
                Select = "c.name AS Field1",
                Table = "sys.columns c",
                WhereNdGroupBy = "where c.object_id = OBJECT_ID('dbo.tblInventoryRequestMaster') " +
                                 "AND c.name IN ('ApprovalStatus','ApprovalStatusName','RequestStatus'," +
                                 "'ApprovalState','IsApproved','IsRejected')",
                OrderBy = "ORDER BY CASE c.name WHEN 'ApprovalStatus' THEN 1 " +
                          "WHEN 'ApprovalStatusName' THEN 2 WHEN 'RequestStatus' THEN 3 " +
                          "WHEN 'ApprovalState' THEN 4 WHEN 'IsApproved' THEN 5 ELSE 6 END"
            };

            List<DmnGridFields> approvalColumns =
                bllSchemaSearch.GetSearchData("prdSearch", approvalColumnSearch);
            string approvalColumn = approvalColumns != null && approvalColumns.Count > 0
                ? approvalColumns[0].Field1
                : null;

            string statusExpression = "CAST('Pending' AS NVARCHAR(30))";
            if (!string.IsNullOrWhiteSpace(approvalColumn))
            {
                string col = "[m].[" + approvalColumn.Replace("]", "]]") + "]";
                statusExpression = approvalColumn == "IsRejected"
                    ? "CASE WHEN CONVERT(NVARCHAR(30)," + col + ")='1' THEN 'Rejected' ELSE 'Pending' END"
                    : "CASE " +
                      "WHEN LOWER(CONVERT(NVARCHAR(30)," + col + ")) LIKE '%send back%' OR CONVERT(NVARCHAR(30)," + col + ")='3' THEN 'Sent Back' " +
                      "WHEN LOWER(CONVERT(NVARCHAR(30)," + col + ")) LIKE '%reject%' OR CONVERT(NVARCHAR(30)," + col + ")='2' THEN 'Rejected' " +
                      "WHEN LOWER(CONVERT(NVARCHAR(30)," + col + ")) LIKE '%approv%' OR CONVERT(NVARCHAR(30)," + col + ")='1' THEN 'Approved' " +
                      "WHEN LOWER(CONVERT(NVARCHAR(30)," + col + ")) LIKE '%pend%' OR CONVERT(NVARCHAR(30)," + col + ")='0' THEN 'Pending' " +
                      "ELSE CONVERT(NVARCHAR(30)," + col + ") END";
            }

            // Resolve optional audit columns without direct Dapper queries.
            DmnSearch masterSchemaSearch = new DmnSearch
            {
                Select = "c.name AS Field1",
                Table = "sys.columns c",
                WhereNdGroupBy = "where c.object_id = OBJECT_ID('dbo.tblInventoryRequestMaster')",
                OrderBy = ""
            };

            List<DmnGridFields> schemaRows =
                bllSchemaSearch.GetSearchData("prdSearch", masterSchemaSearch);
            HashSet<string> masterColumns = new HashSet<string>(
                (schemaRows ?? new List<DmnGridFields>())
                    .Where(x => !string.IsNullOrWhiteSpace(x.Field1))
                    .Select(x => x.Field1),
                StringComparer.OrdinalIgnoreCase);

            string approvedByColumn = new[] { "ApprovedBy", "ApprovedByEmployeeId", "ApprovedById", "ApprovalBy" }
                .FirstOrDefault(masterColumns.Contains);
            string approvedOnColumn = new[] { "ApprovedOn", "ApprovedDate", "ApprovalDate", "ApprovalOn" }
                .FirstOrDefault(masterColumns.Contains);
            string approvalRemarksColumn = new[] { "ApprovalRemarks", "ApprovalRemark", "ApprovalComments", "ApprovalComment" }
                .FirstOrDefault(masterColumns.Contains);

            string approvedBySelect = approvedByColumn == null
                ? "CAST('-' AS NVARCHAR(200))"
                : "ISNULL(approver.EmployeeName, CONVERT(NVARCHAR(200),m.[" + approvedByColumn + "]))";
            string approvedOnSelect = approvedOnColumn == null
                ? "CAST(NULL AS NVARCHAR(30))"
                : "CONVERT(NVARCHAR(30),TRY_CONVERT(DATETIME,m.[" + approvedOnColumn + "]),121)";
            string remarksSelect = approvalRemarksColumn == null
                ? "ISNULL(m.Remarks,'-')"
                : "ISNULL(NULLIF(CONVERT(NVARCHAR(MAX),m.[" + approvalRemarksColumn + "]),''),ISNULL(m.Remarks,'-'))";
            string approverJoin = approvedByColumn == null
                ? ""
                : " LEFT JOIN tblEmployee approver ON approver.EmployeeId = TRY_CONVERT(INT,m.[" + approvedByColumn + "]) ";

            DmnSearch requestSearch = new DmnSearch
            {
                Select = "m.InventoryRequestId AS Id, m.RequestNo AS Field1, " +
                         "CONVERT(NVARCHAR(30),m.RequestDate,121) AS Field2, " +
                         "CONVERT(NVARCHAR(30),m.RequiredDate,121) AS Field3, " +
                         "ISNULL(d.DepartmentName,'-') AS Field4, " +
                         "ISNULL(e.EmployeeName,'-') AS Field5, " +
                         "CASE m.RequestType WHEN 1 THEN 'Batch Preparation' " +
                         "WHEN 2 THEN 'Raw Material' ELSE 'Mixed' END AS Field6, " +
                         statusExpression + " AS Field7, " +
                         approvedBySelect + " AS Field8, " +
                         approvedOnSelect + " AS Field9, " +
                         remarksSelect + " AS Field10",
                Table = "tblInventoryRequestMaster m " +
                        "LEFT JOIN tblDepartment d ON d.DepartmentId = m.DepartmentId " +
                        "LEFT JOIN tblEmployee e ON e.EmployeeId = m.EmployeeId " +
                        approverJoin,
                WhereNdGroupBy = "WHERE ISNULL(m.Status,1) = 1 AND m.BranchId = " +
                                 Convert.ToString(FrmDashboard.BranchId),
                OrderBy = "ORDER BY m.InventoryRequestId DESC"
            };

            BllSearch<DmnGridFields> bllRequestSearch = new BllSearch<DmnGridFields>();
            List<DmnGridFields> requestRows =
                bllRequestSearch.GetSearchData("prdSearch", requestSearch);

            _requestApprovalData = new DataTable();
            _requestApprovalData.Columns.Add("InventoryRequestId", typeof(long));
            _requestApprovalData.Columns.Add("RequestNo", typeof(string));
            _requestApprovalData.Columns.Add("RequestDate", typeof(DateTime));
            _requestApprovalData.Columns.Add("RequiredDate", typeof(DateTime));
            _requestApprovalData.Columns.Add("DepartmentName", typeof(string));
            _requestApprovalData.Columns.Add("EmployeeName", typeof(string));
            _requestApprovalData.Columns.Add("RequestTypeName", typeof(string));
            _requestApprovalData.Columns.Add("ApprovalStatusText", typeof(string));
            _requestApprovalData.Columns.Add("ApprovedByName", typeof(string));
            _requestApprovalData.Columns.Add("ApprovedOn", typeof(DateTime));
            _requestApprovalData.Columns.Add("ApprovalRemarks", typeof(string));

            foreach (DmnGridFields source in requestRows ?? new List<DmnGridFields>())
            {
                DateTime parsedRequestDate;
                DateTime parsedRequiredDate;
                DateTime parsedApprovedOn;
                object requestDate = DateTime.TryParse(source.Field2, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out parsedRequestDate) ? (object)parsedRequestDate : DBNull.Value;
                object requiredDate = DateTime.TryParse(source.Field3, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out parsedRequiredDate) ? (object)parsedRequiredDate : DBNull.Value;
                object approvedOn = DateTime.TryParse(source.Field9, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out parsedApprovedOn) ? (object)parsedApprovedOn : DBNull.Value;

                _requestApprovalData.Rows.Add(
                    source.Id, source.Field1 ?? string.Empty, requestDate, requiredDate,
                    source.Field4 ?? "-", source.Field5 ?? "-", source.Field6 ?? "Mixed",
                    source.Field7 ?? "Pending", source.Field8 ?? "-", approvedOn,
                    source.Field10 ?? "-");
            }

            ApplyApprovalFilterAndPaging();
        }

        private void ApplyApprovalFilterAndPaging()
        {
            if (_requestApprovalData == null)
                _requestApprovalData = new DataTable();

            string search = txt_search_reqapp.Text.Trim();
            IEnumerable<DataRow> rows = _requestApprovalData.AsEnumerable();

            if (_requestApprovalStatus != "All")
                rows = rows.Where(r => string.Equals(Convert.ToString(r["ApprovalStatusText"]),
                    _requestApprovalStatus, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(search))
                rows = rows.Where(r =>
                    Convert.ToString(r["RequestNo"]).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    Convert.ToString(r["EmployeeName"]).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    Convert.ToString(r["DepartmentName"]).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);

            _requestApprovalView = _requestApprovalData.Clone();
            foreach (DataRow row in rows.OrderByDescending(r => Convert.ToInt32(r["InventoryRequestId"])))
                _requestApprovalView.ImportRow(row);

            _requestApprovalPage = Math.Max(1, Math.Min(_requestApprovalPage, TotalApprovalPages));
            BindApprovalPage();
            UpdateApprovalTabAppearance();
        }

        private int TotalApprovalPages
        {
            get
            {
                int count = _requestApprovalView == null ? 0 : _requestApprovalView.Rows.Count;
                return Math.Max(1, (int)Math.Ceiling(count / (double)RequestApprovalPageSize));
            }
        }

        private void BindApprovalPage()
        {
            DataTable page = _requestApprovalView == null ? new DataTable() : _requestApprovalView.Clone();
            int total = _requestApprovalView == null ? 0 : _requestApprovalView.Rows.Count;
            int start = (_requestApprovalPage - 1) * RequestApprovalPageSize;
            if (_requestApprovalView != null)
            {
                for (int i = start; i < Math.Min(start + RequestApprovalPageSize, total); i++)
                {
                    DataRow row = page.NewRow();
                    row.ItemArray = _requestApprovalView.Rows[i].ItemArray;
                    page.Rows.Add(row);
                }
            }

            if (!page.Columns.Contains("SlNo"))
                page.Columns.Add("SlNo", typeof(int));
            for (int i = 0; i < page.Rows.Count; i++)
                page.Rows[i]["SlNo"] = start + i + 1;

            if (!page.Columns.Contains("ApprovedOnText"))
                page.Columns.Add("ApprovedOnText", typeof(string));
            foreach (DataRow approvalRow in page.Rows)
            {
                approvalRow["ApprovedOnText"] = approvalRow["ApprovedOn"] == DBNull.Value
                    ? "-"
                    : Convert.ToDateTime(approvalRow["ApprovedOn"]).ToString("dd MMM yyyy hh:mm tt");
            }

            dgv_req_app.DataSource = page;
            if (dgv_req_app.Columns.Contains("InventoryRequestId"))
                dgv_req_app.Columns["InventoryRequestId"].Visible = false;
            if (dgv_req_app.Columns.Contains("ViewRequest"))
                dgv_req_app.Columns["ViewRequest"].DisplayIndex = dgv_req_app.Columns.Count - 1;

            int first = total == 0 ? 0 : start + 1;
            int last = Math.Min(start + RequestApprovalPageSize, total);
            lblpagenodescrption.Text = "Showing " + first + " to " + last + " of " + total + " requests";

            Button[] pageButtons = { btn_page1, btn_page2, btn_page3, btn_page4, btn_page5 };
            int firstPage = Math.Max(1, Math.Min(_requestApprovalPage - 2, Math.Max(1, TotalApprovalPages - 4)));
            for (int i = 0; i < pageButtons.Length; i++)
            {
                int pageNo = firstPage + i;
                pageButtons[i].Text = pageNo.ToString();
                pageButtons[i].Visible = pageNo <= TotalApprovalPages;
                pageButtons[i].BackColor = pageNo == _requestApprovalPage
                    ? Color.FromArgb(99, 76, 184) : Color.FromArgb(239, 235, 250);
                pageButtons[i].ForeColor = pageNo == _requestApprovalPage ? Color.White : Color.FromArgb(35, 35, 65);
                pageButtons[i].Tag = pageNo;
            }

            btn_movefrist.Enabled = btn_previous.Enabled = _requestApprovalPage > 1;
            btn_next.Enabled = btn_movelast.Enabled = _requestApprovalPage < TotalApprovalPages;
        }

        private void SetApprovalStatusFilter(string status)
        {
            _requestApprovalStatus = status;
            _requestApprovalPage = 1;
            ApplyApprovalFilterAndPaging();
        }

        private int CountApprovalStatus(string status)
        {
            if (_requestApprovalData == null || !_requestApprovalData.Columns.Contains("ApprovalStatusText"))
                return 0;
            return _requestApprovalData.AsEnumerable().Count(r =>
                string.Equals(Convert.ToString(r["ApprovalStatusText"]), status, StringComparison.OrdinalIgnoreCase));
        }

        private void UpdateApprovalTabAppearance()
        {
            string[] labels = { "All", "Pending", "Approved", "Rejected" };
            int[] counts =
            {
                _requestApprovalData == null ? 0 : _requestApprovalData.Rows.Count,
                CountApprovalStatus("Pending"),
                CountApprovalStatus("Approved"),
                CountApprovalStatus("Rejected")
            };

            for (int i = 0; i < labels.Length; i++)
            {
                string status = labels[i];
                TableLayoutPanel tab = GetApprovalTab(status);
                bool selected = status == _requestApprovalStatus;
                Color baseColor = selected ? GetApprovalTabSelectedColor(status) : GetApprovalTabBaseColor(status);

                if (tab != null)
                {
                    tab.BackColor = baseColor;
                    tab.BorderStyle = BorderStyle.FixedSingle;
                }

                Label caption = status == "All" ? lbl_all :
                                status == "Pending" ? lbl_pending :
                                status == "Approved" ? lbl_approved : lbl_rejected;
                PictureBox icon = status == "All" ? pb_all :
                                  status == "Pending" ? pb_pending :
                                  status == "Approved" ? pb_approved : pb_rejected;

                caption.Text = labels[i] + " (" + counts[i] + ")";
                caption.ForeColor = selected ? Color.White : Color.FromArgb(35, 35, 65);
                caption.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                caption.BackColor = Color.Transparent;
                caption.AutoSize = false;
                caption.Dock = DockStyle.Fill;
                caption.TextAlign = ContentAlignment.MiddleCenter;
                caption.AutoEllipsis = false;
                caption.Margin = new Padding(0);
                icon.BackColor = Color.Transparent;
                icon.SizeMode = PictureBoxSizeMode.Zoom;

            }
        }

        private void txt_search_reqapp_TextChanged(object sender, EventArgs e)
        {
            _requestApprovalPage = 1;
            ApplyApprovalFilterAndPaging();
        }

        private void GoToApprovalPage(int page)
        {
            _requestApprovalPage = Math.Max(1, Math.Min(page, TotalApprovalPages));
            BindApprovalPage();
        }
        #endregion

        #region Show approval Pending request
        private void btn_openrequest_Click(object sender, EventArgs e)
        {
            try
            {
                InitializeRequestApprovalPopup();
                CenterApprovalPopup();
                pnl_reqapproval.Visible = true;
                pnl_reqapproval.BringToFront();
                txt_search_reqapp.Clear();
                _requestApprovalStatus = "All";
                _requestApprovalPage = 1;
                LoadRequestApprovalData();
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_openrequest_Click", ex);
                ShowError("Unable to load inventory requests. " + ex.Message);
            }
        }

        private void btnclose_reqapp_Click(object sender, EventArgs e)
        {
            pnl_reqapproval.Visible = false;
        }

        private void btn_all_Click(object sender, EventArgs e) { SetApprovalStatusFilter("All"); }
        private void btn_Pending_req_Click(object sender, EventArgs e) { SetApprovalStatusFilter("Pending"); }
        private void btn_approvalreq_Click(object sender, EventArgs e) { SetApprovalStatusFilter("Approved"); }
        private void btn_rejectreq_Click(object sender, EventArgs e) { SetApprovalStatusFilter("Rejected"); }
        private void btn_movefrist_Click(object sender, EventArgs e) { GoToApprovalPage(1); }
        private void btn_previous_Click(object sender, EventArgs e) { GoToApprovalPage(_requestApprovalPage - 1); }
        private void btn_page1_Click(object sender, EventArgs e) { GoToApprovalPage(Convert.ToInt32(btn_page1.Tag ?? btn_page1.Text)); }
        private void btn_page2_Click(object sender, EventArgs e) { GoToApprovalPage(Convert.ToInt32(btn_page2.Tag ?? btn_page2.Text)); }
        private void btn_page3_Click(object sender, EventArgs e) { GoToApprovalPage(Convert.ToInt32(btn_page3.Tag ?? btn_page3.Text)); }
        private void btn_page4_Click(object sender, EventArgs e) { GoToApprovalPage(Convert.ToInt32(btn_page4.Tag ?? btn_page4.Text)); }
        private void btn_page5_Click(object sender, EventArgs e) { GoToApprovalPage(Convert.ToInt32(btn_page5.Tag ?? btn_page5.Text)); }
        private void btn_next_Click(object sender, EventArgs e) { GoToApprovalPage(_requestApprovalPage + 1); }
        private void btn_movelast_Click(object sender, EventArgs e) { GoToApprovalPage(TotalApprovalPages); }

        private void dgv_req_app_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dgv_req_app.Columns[e.ColumnIndex].Name != "ViewRequest")
                return;

            object id = dgv_req_app.Rows[e.RowIndex].Cells["InventoryRequestId"].Value;
            if (id == null || id == DBNull.Value) return;

            try
            {
                int requestId = Convert.ToInt32(id);
                pnl_reqapproval.Visible = false;
                FillData(requestId); // Same master/detail loading path used by request search.
                SetRequestViewMode(true);
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest dgv_req_app_CellClick", ex);
                ShowError("Unable to open the selected inventory request. " + ex.Message);
            }
        }

        private void btn_adv_search_reqapp_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch search = new DmnSearch
                {
                    ColumnHeaders = "ID,Request No,Request Date,Required Date,Department,Requested By",
                    Select = "m.InventoryRequestId as Id,m.RequestNo as Field1," +
                             "CAST(m.RequestDate AS varchar(30)) as Field2," +
                             "CAST(m.RequiredDate AS varchar(30)) as Field3," +
                             "ISNULL(d.DepartmentName,'-') as Field4,ISNULL(e.EmployeeName,'-') as Field5",
                    Table = "tblInventoryRequestMaster m LEFT JOIN tblDepartment d ON d.DepartmentId=m.DepartmentId " +
                            "LEFT JOIN tblEmployee e ON e.EmployeeId=m.EmployeeId",
                    WhereNdGroupBy = "WHERE ISNULL(m.Status,1)=1 AND m.BranchId=" + Convert.ToString(FrmDashboard.BranchId),
                    OrderBy = " ORDER BY m.InventoryRequestId DESC"
                };
                DataTable alignment = new DataTable();
                alignment.Columns.Add();
                int selectedId = 0;
                using (var popup = new FrmSearch<DmnGridFields>(
                    search, SearchMode.FirstCharacter,
                    this.Width - 150, this.Height - 150,
                    this.Location.Y + 50, this.Location.X + 20, alignment))
                {
                    if (popup.ShowDialog() == DialogResult.OK)
                        selectedId = Convert.ToInt32(popup._retValue);
                }

                if (selectedId > 0 && _requestApprovalData != null)
                {
                    DataRow selected = _requestApprovalData.AsEnumerable()
                        .FirstOrDefault(r => Convert.ToInt32(r["InventoryRequestId"]) == selectedId);
                    if (selected != null)
                    {
                        txt_search_reqapp.Text = Convert.ToString(selected["RequestNo"]);
                        _requestApprovalPage = 1;
                        ApplyApprovalFilterAndPaging();
                    }
                }
            }
            catch (Exception ex)
            {
                LogInventoryException("FrmInventoryrequest btn_adv_search_reqapp_Click", ex);
                ShowError("Unable to open advanced request search. " + ex.Message);
            }
        }

        #endregion

        
    }
}