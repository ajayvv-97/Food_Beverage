using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CustomMessageBox;
using DAL;
using Domain;
using FastMember;
using FoodandBeverage.Classes;
using FoodandBeverage.Controls;
using FoodandBeverage.Interfaces;
using FoodandBeverage.Services;
using Microsoft.AspNet.SignalR.Client;

namespace FoodandBeverage.Forms
{
    public partial class FrmKitchenView  : Form
    {
        public static FrmKitchenView ActiveInstance { get; private set; }

        private SoundPlayer soundPlayer;
        ReportDocument reportDocument = new ReportDocument();
        private BlurPopupOverlay _blurPopupOverlay;

        public FrmKitchenView()
        {
            InitializeComponent();
            ActiveInstance = this;
            _blurPopupOverlay = new BlurPopupOverlay(this,
                pnl_products_rem_descr,
                pnlproductviewmore,
                pnl_kitchenorderBill);
            soundPlayer = new SoundPlayer(FoodandBeverage.Properties.Resources.kitchen_order_sound);
            soundPlayer.Load();
        }

        #region Declaration Grid
        public int dslno = 0;
        public int dactionno = 1;
        public int dptypeno = 2;
        public int ditemno = 3;
        public int dqtyno = 4;
        public int dnotes = 5;
        public int dremnoteno = 6;
        public int ddetailsidno = 7;
        public int dkoidno = 8;
        public int dtidno = 9;
        public int dordermasteridno = 10;
        public int ddatetimeno = 11;
        public int dstatusno = 12;
        public int ditemidno = 13;
        public int dtabletypeno = 14;
        public int dorderno = 15;
        public int dAddonStatusno = 16;
        public int dAVIDno = 17;

        public static int addslno = 0;
        public int additemnameno = 1;
        public int addqtyno = 2;
        public int addordermasteridno = 3;
        public int addkoidno = 4;
        public int addorderdetailidno = 5;
        public int addactionno = 6;
        public int addavidno = 7;
        public int addpavidno = 8;
        public int addaddonstatusno = 9;
        public int addonpriceno = 10;
        public int addonorderno = 11;
        public int addccodeno = 12;
        #endregion

        Color boxcolor = Color.FromArgb(0x42, 0x67, 0xB2); //System.Drawing.ColorTranslator.FromHtml("#3AAFA9"); //
        public int dgv_view_rowno = 0;
        public string dgv_view_name = "";
        public List<datagrid> orderbox = new List<datagrid>();
        public bool pbcancelfoodclciked = false;
        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _hubProxy;
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        public string RPTPath = Convert.ToString(FrmLogin.KitchenBillPath);
        TableLayoutPanel tlp_order_col = new TableLayoutPanel();
        bool addingControls = false;
        public int KitchenDisplayCount = 0;
        ClsGeneral clsgen = new ClsGeneral();
        public int ReprintOrderNo = 0;
        public int ReprintId = 0;

        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        private bool isCancelPrintingRunning = false;
        private bool isOrderPrintingRunning = false;
        private bool emptyStateVisibilityInitialized = false;
        private bool emptyStateShouldBeVisible = false;

        private bool _enableKitchenKOTOnly;
        private bool _hasLoadedKitchenKOTOnlySetting;

        private bool IsKitchenKOTOnlyMode
        {
            get
            {
                return _hasLoadedKitchenKOTOnlySetting
                    ? _enableKitchenKOTOnly
                    : FrmDashboard.IsKitchenKOTOnlyEnabled;
            }
        }

        Dictionary<string, int> rowCounters = new Dictionary<string, int>(); 
        Dictionary<string, bool> rowColorState = new Dictionary<string, bool>(); 
        int blinkPeriod = 1; 
        public int korderno = 0;

   
        private void FrmKitchenView_Load(object sender, EventArgs e)
        {
            try
            {
                cmbserachtype.SelectedIndex = 0;
                int stype = cmbserachtype.SelectedIndex;
                if (stype == 0)
                {
                    txtsearchData.Text = "Enter Kitchen Number...";
                }
                else if (stype == 1)
                {
                    txtsearchData.Text = "Enter Table Code...";
                }
                else if (stype == 2)
                {
                    txtsearchData.Text = "Enter Daily Token No...";
                }

                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    //AppSession.RegisterForm(this, (int)LogoutFormList.Home);
                }

                dgv_viewmore.BorderStyle = BorderStyle.None;
                dgv_viewmore.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgv_viewmore.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgv_viewmore.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgv_viewmore.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgv_viewmore.BackgroundColor = Color.White;

                dgv_viewmore.EnableHeadersVisualStyles = false;
                dgv_viewmore.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgv_viewmore.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgv_viewmore.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                txt_product_remove_des.Text = "Reason type here .....";
                textb_shape();
                tlp_order_col.Dock = DockStyle.Fill;

                ConfigureKitchenCardHost();
                ConfigureKitchenDetailPopup();
                RefreshKitchenKOTOnlySetting();
                InitTimer();

                KitchenDisplayCount = FrmLogin.KitchenDisplayCount;
                txt_product_remove_des.ForeColor = Color.DarkGray;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View FrmKitchenView_Load", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        private void btncolse_Click(object sender, EventArgs e)
        {
            refreshtimer.Stop();
            this.Close();
            this.Dispose();
        }


        /// <summary>
        /// Reads the Kitchen KOT-only switch for this branch. Dashboard's cached
        /// value is retained as a safe fallback if the settings row cannot be read.
        /// </summary>
        private void RefreshKitchenKOTOnlySetting()
        {
            bool fallbackValue = FrmDashboard.IsKitchenKOTOnlyEnabled;
            _enableKitchenKOTOnly = fallbackValue;
            _hasLoadedKitchenKOTOnlySetting = true;

            try
            {
                BllSearch<DmnGeneralSetting> search = new BllSearch<DmnGeneralSetting>();
                DmnSearch query = new DmnSearch
                {
                    Select = "ISNULL(EnableKitchenKOTOnly, 0) AS EnableKitchenKOTOnly",
                    Table = "tblGeneralSetting",
                    WhereNdGroupBy = "WHERE BranchId = " + FrmDashboard.BranchId + " AND Status = 1",
                    OrderBy = "ORDER BY Gid DESC"
                };

                List<DmnGeneralSetting> settings =
                    search.GetSearchData("prdSearch", query);

                if (settings != null && settings.Count > 0)
                    _enableKitchenKOTOnly = settings[0].EnableKitchenKOTOnly;
            }
            catch (Exception ex)
            {
                _enableKitchenKOTOnly = fallbackValue;
                clsgen.LogException("Kitchen_View RefreshKitchenKOTOnlySetting", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        private static bool HasKitchenLineStatus(
            List<Dmnkitchendetails> details, int requiredStatus)
        {
            if (details == null)
                return false;

            foreach (Dmnkitchendetails detail in details)
            {
                int status;
                if (detail != null &&
                    int.TryParse(Convert.ToString(detail.Field12), out status) &&
                    status == requiredStatus)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// KOT-only mode has OKAY + REPRINT.
        /// Normal mode swaps START for READY after the ticket is started;
        /// CANCEL and REPRINT remain available while the order is active.
        /// Reprint is never hidden by the optional customer-wait flag.
        /// </summary>
        private void ApplyKitchenActionVisibility(
            OrderCardCache cache, List<Dmnkitchendetails> details)
        {
            if (cache == null || details == null)
                return;

            bool kotOnlyMode = IsKitchenKOTOnlyMode;
            bool hasPendingStart = HasKitchenLineStatus(details, 1);
            bool hasInProgress = HasKitchenLineStatus(details, 2);
            bool hasActiveRows = hasPendingStart || hasInProgress;

            if (cache.BtnStart != null)
            {
                cache.BtnStart.Visible = !kotOnlyMode && hasPendingStart;
                cache.BtnStart.Enabled = !kotOnlyMode && hasPendingStart;
            }

            if (cache.BtnReady != null)
            {
                ModernKitchenActionButton readyButton =
                    cache.BtnReady as ModernKitchenActionButton;

                if (readyButton != null)
                {
                    readyButton.ActionStyle = kotOnlyMode
                        ? KitchenActionStyle.Okay
                        : KitchenActionStyle.Ready;
                    readyButton.ButtonText = kotOnlyMode ? "OKAY" : "READY";
                    readyButton.Invalidate();
                }

                bool canComplete = kotOnlyMode
                    ? true
                    : (!hasPendingStart && hasInProgress);

                // Normal mode is a strict one-step-at-a-time flow:
                // first START, then READY after the START update is persisted.
                cache.BtnReady.Visible = canComplete;
                cache.BtnReady.Enabled = canComplete;
            }

            if (cache.BtnCancel != null)
            {
                cache.BtnCancel.Visible = !kotOnlyMode && hasActiveRows;
                cache.BtnCancel.Enabled = !kotOnlyMode && hasActiveRows;
            }

            if (cache.BtnReprint != null)
            {
                cache.BtnReprint.Visible = true;
                cache.BtnReprint.Enabled = true;
            }
        }

        /// <summary>
        /// Size tickets against the current POS viewport so the footer actions are not clipped.
        /// </summary>
        private void ConfigureKitchenCardHost()
        {
            flp_order_view.Dock = DockStyle.Fill;
            flp_order_view.AutoScroll = true;
            flp_order_view.WrapContents = true;
            flp_order_view.FlowDirection = FlowDirection.LeftToRight;
            flp_order_view.Padding = new Padding(6);
            flp_order_view.Margin = Padding.Empty;
            flp_order_view.Resize -= flp_order_view_ResponsiveResize;
            flp_order_view.Resize += flp_order_view_ResponsiveResize;
        }

        /// <summary>
        /// Applies a consistent modern layout to the combo/add-on order details popup.
        /// The existing grid columns and click handlers are retained.
        /// </summary>
        private void ConfigureKitchenDetailPopup()
        {
            if (pnlproductviewmore == null || tlp_viewmore == null ||
                dgv_viewmore == null || tlp_header_viewmore == null)
                return;

            int popupWidth = Math.Min(920, Math.Max(620, (int)(ClientSize.Width * 0.68)));
            int popupHeight = Math.Min(560, Math.Max(380, (int)(ClientSize.Height * 0.68)));
            if (ClientSize.Width > 40)
                popupWidth = Math.Min(popupWidth, ClientSize.Width - 32);
            if (ClientSize.Height > 40)
                popupHeight = Math.Min(popupHeight, ClientSize.Height - 32);

            pnlproductviewmore.SuspendLayout();
            pnlproductviewmore.BackColor = Color.White;
            pnlproductviewmore.BorderStyle = BorderStyle.FixedSingle;
            pnlproductviewmore.Padding = new Padding(8);
            pnlproductviewmore.Size = new Size(Math.Max(320, popupWidth), Math.Max(260, popupHeight));

            tlp_viewmore.SuspendLayout();
            tlp_viewmore.SetColumn(dgv_viewmore, 0);
            tlp_viewmore.SetColumn(tlp_header_viewmore, 0);
            tlp_viewmore.SetColumn(label9, 0);
            tlp_viewmore.ColumnCount = 1;
            tlp_viewmore.ColumnStyles.Clear();
            tlp_viewmore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_viewmore.RowCount = 3;
            tlp_viewmore.RowStyles.Clear();
            tlp_viewmore.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tlp_viewmore.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_viewmore.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tlp_viewmore.BackColor = Color.White;
            tlp_viewmore.Margin = Padding.Empty;
            tlp_viewmore.Padding = Padding.Empty;

            tlp_header_viewmore.SuspendLayout();
            tlp_header_viewmore.SetColumn(lblviewmore, 0);
            tlp_header_viewmore.SetColumn(btn_close_viewmore, 1);
            tlp_header_viewmore.ColumnCount = 2;
            tlp_header_viewmore.ColumnStyles.Clear();
            tlp_header_viewmore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_header_viewmore.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 48F));
            tlp_header_viewmore.RowStyles.Clear();
            tlp_header_viewmore.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_header_viewmore.BackColor = Color.FromArgb(76, 45, 128);
            tlp_header_viewmore.Margin = Padding.Empty;
            tlp_header_viewmore.Padding = new Padding(8, 4, 8, 4);

            lblviewmore.AutoSize = false;
            lblviewmore.Dock = DockStyle.Fill;
            lblviewmore.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblviewmore.ForeColor = Color.White;
            lblviewmore.Padding = new Padding(12, 0, 4, 0);
            lblviewmore.TextAlign = ContentAlignment.MiddleLeft;

            btn_close_viewmore.AutoSize = true;
            btn_close_viewmore.Dock = DockStyle.Fill;
            btn_close_viewmore.Text = "×";
            btn_close_viewmore.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            btn_close_viewmore.ForeColor = Color.White;
            btn_close_viewmore.BackColor = Color.FromArgb(92, 57, 148);
            btn_close_viewmore.BackgroundImage = null;
            btn_close_viewmore.FlatStyle = FlatStyle.Flat;
            btn_close_viewmore.FlatAppearance.BorderSize = 0;
            btn_close_viewmore.FlatAppearance.MouseOverBackColor = Color.FromArgb(115, 76, 174);
            btn_close_viewmore.UseVisualStyleBackColor = false;
            btn_close_viewmore.Margin = new Padding(2);

            dgv_viewmore.SuspendLayout();
            dgv_viewmore.BorderStyle = BorderStyle.None;
            dgv_viewmore.BackgroundColor = Color.White;
            dgv_viewmore.GridColor = Color.FromArgb(230, 225, 239);
            dgv_viewmore.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_viewmore.EnableHeadersVisualStyles = false;
            dgv_viewmore.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_viewmore.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(244, 239, 251);
            dgv_viewmore.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(76, 45, 128);
            dgv_viewmore.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv_viewmore.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_viewmore.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 6, 0);
            dgv_viewmore.ColumnHeadersHeight = 46;
            dgv_viewmore.DefaultCellStyle.BackColor = Color.White;
            dgv_viewmore.DefaultCellStyle.ForeColor = Color.FromArgb(38, 43, 55);
            dgv_viewmore.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 226, 249);
            dgv_viewmore.DefaultCellStyle.SelectionForeColor = Color.FromArgb(49, 33, 77);
            dgv_viewmore.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv_viewmore.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            dgv_viewmore.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_viewmore.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 248, 253);
            dgv_viewmore.RowTemplate.Height = 44;
            dgv_viewmore.RowHeadersVisible = false;
            dgv_viewmore.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_viewmore.MultiSelect = false;
            dgv_viewmore.ResumeLayout(true);

            if (dgv_viewmore.Columns.Contains("vslno"))
                dgv_viewmore.Columns["vslno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgv_viewmore.Columns.Contains("vqty"))
                dgv_viewmore.Columns["vqty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgv_viewmore.Columns.Contains("addaction"))
                dgv_viewmore.Columns["addaction"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            label9.AutoSize = false;
            label9.Dock = DockStyle.Fill;
            label9.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            label9.ForeColor = Color.FromArgb(112, 105, 124);
            label9.Text = "ESC  •  Close";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            label9.Padding = new Padding(8, 0, 0, 0);

            tlp_header_viewmore.ResumeLayout(true);
            tlp_viewmore.ResumeLayout(true);
            pnlproductviewmore.ResumeLayout(true);
        }

        private void flp_order_view_ResponsiveResize(object sender, EventArgs e)
        {
            if (flp_order_view.ClientSize.Width <= 0)
                return;

            int availableWidth = Math.Max(320, flp_order_view.ClientSize.Width -
                flp_order_view.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth - 12);
            int columns = availableWidth >= 1120 ? 3 : availableWidth >= 760 ? 2 : 1;
            int cardWidth = Math.Max(320, (availableWidth - (columns - 1) * 18) / columns);
            int cardHeight = IsKitchenKOTOnlyMode ? 480 : 530;
            cardHeight = Math.Max(440, Math.Min(530, cardHeight));

            foreach (OrderCardCache cache in KitchenCardCache.Values)
            {
                if (cache == null || cache.CardPanel == null || cache.CardPanel.IsDisposed)
                    continue;

                cache.CardPanel.SuspendLayout();
                cache.CardPanel.Size = new Size(cardWidth, cardHeight);
                cache.CardPanel.Margin = new Padding(4, 5, 4, 5);
                cache.CardPanel.ResumeLayout(true);
            }
        }

        #region KitchenView - Cached card UI (Layout A) and blinking
        public class OrderCardCache
        {
            public ModernKitchenOrderCard CardPanel;
            public DataGridView Grid;
            public Label TimeLabel;
            public Label StartTimeLabel;
            public Label ElapsedTimeLabel;
            public Label StatusLabel;
            public Label TableCodeLabel;
            public Label TokenLabel;
            public Label OrderTypeLabel;
            public TextBox Description;
            public PictureBox BtnStart;
            public PictureBox BtnReady;
            public PictureBox BtnCancel;
            public PictureBox BtnReprint;
            public DateTime CreatedTime;

            // DescriptionPanel remains a Control reference for the existing status/update path.
            public Control DescriptionPanel;
            public int CancelStatus { get; set; } = -1;

        }
        private Dictionary<string, OrderCardCache> KitchenCardCache = new Dictionary<string, OrderCardCache>();
        private Dictionary<string, int> RowBlinkCounter = new Dictionary<string, int>();
        private Dictionary<string, bool> RowBlinkState = new Dictionary<string, bool>();
        private const int BlinkThreshold = 1;

        /// <summary>
        /// Main optimized FillOrdersAsync - creates cards once and updates on subsequent calls.
        /// </summary>
        public void FillOrders()
        {
            try
            {
                // Empty state is updated once the refreshed cards are reconciled.
                //+ " and p.PreparationMode ="+ Convert.ToInt32(PreparationType.kitchen);

                txt_product_remove_des.Text = "Reason type here .....";

                // Query active kitchen tickets directly. Older databases may still have a
                // prdKitchenData version that excludes legacy NULL/0 route values.
                List<DmnGridFields> gridSourcemaster = GetKitchenMasterOrders();

                if (gridSourcemaster == null || gridSourcemaster.Count == 0)
                {
                    btntotalorders.Text = "0";
                    btnprintcount.Text = "0";
                    ClearKitchenUI();
                    // Empty state is applied after reconciliation below, not during each refresh.
                    SetEmptyIcon();
                    return;
                }

                // distinct orders grouping
                var distinctOrders = gridSourcemaster
                   .GroupBy(order => new { order.Field1, order.Field2 })
                   .Select(group => group.First())
                   .OrderBy(order => order.Field1)
                   .ThenBy(order => order.Field2)
                   .ToList();

                btntotalorders.Text = Convert.ToString(distinctOrders.Count);
                int countpendingorder = distinctOrders.Count(order => Convert.ToInt32(order.Field3) == 1);
                btnprintcount.Text = Convert.ToString(countpendingorder);


                HashSet<string> currentKeys = new HashSet<string>();
                foreach (var od in distinctOrders)
                {
                    // unique key: Field1 (OrderMasterId) + '_' + Field17 (OrderNo will be fetched later with details, but use Field2? we will fetch details soon)
                    // We'll use Field1 + "_" + Field2 initially; after fetching details we will actually use Field1 + "_" + Field17.
                    // To keep consistent, we'll fetch details below and compute the real key.
                    // For removal, compute keys from existing cache and then check existence after creating keys list below.
                }
                List<string> newKeysInOrder = new List<string>();
                Dictionary<string, (DmnGridFields Header, List<Dmnkitchendetails> Details)> orderDataMap =
                    new Dictionary<string, (DmnGridFields, List<Dmnkitchendetails>)>();

                int rowCountDisplay = 0;
                foreach (var orders in distinctOrders)
                {
                    //if (rowCountDisplay >= KitchenDisplayCount) break;

                    List<Dmnkitchendetails> gridSourcedetails = GetKitchenOrderDetails(orders);

                    if (gridSourcedetails == null || gridSourcedetails.Count == 0)
                        continue;

                    // Unique key: Field1 + "_" + Field17 (OrderNo from details)
                    string orderNoSuffix = Convert.ToString(gridSourcedetails[0].Field17);
                    string key = orders.Field1 + "_" + orderNoSuffix;

                    newKeysInOrder.Add(key);
                    orderDataMap[key] = (orders, gridSourcedetails);

                    rowCountDisplay++;
                }

                // Remove cached cards that are no longer present
                List<string> toRemove = new List<string>();
                foreach (var existingKey in KitchenCardCache.Keys)
                {
                    if (!newKeysInOrder.Contains(existingKey))
                        toRemove.Add(existingKey);
                }
                foreach (var rem in toRemove)
                {
                    try
                    {
                        var dead = KitchenCardCache[rem];
                        if (dead != null && dead.CardPanel != null)
                        {
                            flp_order_view.Controls.Remove(dead.CardPanel);
                            if (dead.Grid != null)
                                orderbox.RemoveAll(x => string.Equals(
                                    x.datagridviewname, dead.Grid.Name, StringComparison.OrdinalIgnoreCase));
                            dead.CardPanel.Dispose();
                        }
                    }
                    catch 
                    { 

                    }
                    KitchenCardCache.Remove(rem);
                }

                // Add or update cards
                foreach (var kv in orderDataMap)
                {
                    string key = kv.Key;
                    DmnGridFields header = kv.Value.Header;
                    List<Dmnkitchendetails> details = kv.Value.Details;

                    OrderCardCache cache;
                    if (!KitchenCardCache.TryGetValue(key, out cache))
                    {
                        // create new card (build and add)
                        cache = BuildOrderCard(header, details);
                        if (cache != null && cache.CardPanel != null)
                        {
                            KitchenCardCache[key] = cache;
                            flp_order_view.Controls.Add(cache.CardPanel);
                        }
                    }
                    else
                    {
                        // update existing
                        UpdateOrderCard(cache, header, details);
                    }
                }

                flp_order_view_ResponsiveResize(flp_order_view, EventArgs.Empty);
                if (!timer1.Enabled) timer1.Start();
                if (!refreshtimer.Enabled) refreshtimer.Start();
                SetEmptyIcon();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View Page FillOrdersAsync()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        /// <summary>
        /// Returns active Kitchen-routed KOT masters without depending on the deployed
        /// prdKitchenData version. NULL/0 route values are treated as legacy Kitchen;
        /// Counter (2) tickets are always excluded.
        /// </summary>
        private List<DmnGridFields> GetKitchenMasterOrders()
        {
            try
            {
                BllSearch<DmnGridFields> search = new BllSearch<DmnGridFields>();
                DmnSearch query = new DmnSearch
                {
                    Select = "ko.OrderMasterId AS Field1, ko.OrderNo AS Field2, ISNULL(ko.KOTPrintStatus, 1) AS Field3",
                    Table = "tblkitchenOrder ko",
                    WhereNdGroupBy =
                        "WHERE ko.Status IN (1, 2) " +
                        "AND ISNULL(ko.AddonStatus, 0) IN (0, 1) " +
                        "AND ISNULL(NULLIF(ko.PreparationMode, 0), 1) = " +
                        Convert.ToInt32(KOTRouting.kitchen),
                    OrderBy = "ORDER BY ko.Koid DESC"
                };

                List<DmnGridFields> rows = search.GetSearchData("prdSearch", query);
                // An empty/null result is authoritative: do not fall through to an older
                // stored procedure that may still return inactive Status = 0 tickets.
                return rows ?? new List<DmnGridFields>();
            }
            catch (Exception ex)
            {
                clsgen.LogException("FrmKitchenView GetKitchenMasterOrders direct query", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Kitchen_View));
            }

            // Older installations may not have all of the columns used by the direct
            // compatibility query. Keep the deployed stored procedure as a fallback.
            try
            {
                BllKitchenData<DmnGridFields> legacySearch = new BllKitchenData<DmnGridFields>();
                DmnGridFields parameters = new DmnGridFields
                {
                    Searchtype = Convert.ToInt32(KitchenSearchType.AllOrders)
                };
                return legacySearch.serachreport("prdKitchenData", parameters) ??
                    new List<DmnGridFields>();
            }
            catch (Exception ex)
            {
                clsgen.LogException("FrmKitchenView GetKitchenMasterOrders procedure fallback", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Kitchen_View));
                return new List<DmnGridFields>();
            }
        }

        /// <summary>
        /// Explicit detail status 0 means cancelled and must never be shown on a KOT.
        /// NULL/blank legacy statuses keep the previous default-active behavior.
        /// </summary>
        private static bool IsActiveKitchenDetail(Dmnkitchendetails detail)
        {
            if (detail == null)
                return false;

            string value = Convert.ToString(detail.Field12);
            if (string.IsNullOrWhiteSpace(value))
                return true;

            int status;
            return int.TryParse(value, out status) && (status == 1 || status == 2);
        }

        /// <summary>
        /// Reads details through the installed procedure first. If that procedure still
        /// filters out legacy route values, fetch the visible KOT lines directly.
        /// </summary>
        private List<Dmnkitchendetails> GetKitchenOrderDetails(DmnGridFields order)
        {
            if (order == null)
                return new List<Dmnkitchendetails>();

            int orderMasterId;
            if (!int.TryParse(Convert.ToString(order.Field1), out orderMasterId) ||
                orderMasterId <= 0)
                return new List<Dmnkitchendetails>();

            int orderNo;
            bool hasOrderNo = int.TryParse(Convert.ToString(order.Field2), out orderNo);

            if (hasOrderNo)
            {
                try
                {
                    BllkitchenDataDetails<Dmnkitchendetails> detailsService =
                        new BllkitchenDataDetails<Dmnkitchendetails>();
                    Dmnkitchendetails parameters = new Dmnkitchendetails
                    {
                        OrderMasterId = orderMasterId,
                        OrderNo = orderNo
                    };

                    List<Dmnkitchendetails> details =
                        detailsService.serachData("prdKitchendetails", parameters);
                    if (details != null && details.Count > 0)
                    {
                        List<Dmnkitchendetails> activeDetails =
                            details.Where(IsActiveKitchenDetail).ToList();
                        if (activeDetails.Count > 0)
                            return activeDetails;
                    }
                }
                catch (Exception ex)
                {
                    clsgen.LogException("FrmKitchenView GetKitchenOrderDetails procedure", ex,
                        FrmLogin.EmployeName, FrmLogin.Designation,
                        Convert.ToInt32(FormType.Kitchen_View));
                }
            }

            try
            {
                BllSearch<Dmnkitchendetails> search = new BllSearch<Dmnkitchendetails>();
                DmnSearch query = new DmnSearch
                {
                    Select =
                        "ko.OrderMasterId AS Field1, " +
                        "ISNULL(tb.TableCode, '') AS Field2, " +
                        "'' AS Field3, " +
                        "od.ItemName AS Field4, " +
                        "od.Quantity AS Field5, " +
                        "ISNULL(od.Notes, '') AS Field6, " +
                        "'' AS Field7, " +
                        "ko.Koid AS Field8, " +
                        "od.OrderDetailsId AS Field9, " +
                        "ko.Tid AS Field10, " +
                        "ISNULL(od.RemoveNotes, '') AS Field11, " +
                        "ISNULL(od.Status, 1) AS Field12, " +
                        "od.TableType AS Field13, " +
                        "od.ItemId AS Field14, " +
                        "od.TableType AS Field15, " +
                        "ISNULL(od.MainRemarks, '') AS Field16, " +
                        "ko.OrderNo AS Field17, " +
                        "0 AS Field18, " +
                        "ISNULL(od.AddonStatus, 0) AS Field19, " +
                        "ISNULL(od.AV_id, '') AS Field20, " +
                        "0 AS Field21, " +
                        "ko.OrderNo AS Field22, " +
                        "ISNULL(od.Addoncount, 0) AS Field23, " +
                        "ISNULL(od.VariatiionName, '') AS Field24, " +
                        "ISNULL(om.Pkid, 0) AS Field25, " +
                        "0 AS Field26, " +
                        "0 AS Field27, " +
                        "od.CreatedDateTime AS Fielddatetime",
                    Table =
                        "tblkitchenOrder ko " +
                        "INNER JOIN tblOrderDetails od ON od.OrderDetailsId = ko.OrderDetailsId " +
                        "LEFT JOIN tblOrderMaster om ON om.OrderMasterId = ko.OrderMasterId " +
                        "LEFT JOIN tblTable_info tb ON tb.Tid = ko.Tid",
                    WhereNdGroupBy =
                        "WHERE ko.OrderMasterId = " + orderMasterId +
                        (hasOrderNo ? " AND ko.OrderNo = " + orderNo : string.Empty) +
                        " AND ko.Status IN (1, 2) " +
                        "AND ISNULL(od.Status, 1) IN (1, 2) " +
                        "AND ISNULL(ko.AddonStatus, 0) IN (0, 1) " +
                        "AND ISNULL(NULLIF(ko.PreparationMode, 0), 1) = " +
                        Convert.ToInt32(KOTRouting.kitchen),
                    OrderBy = "ORDER BY ko.Koid, od.OrderDetailsId"
                };

                List<Dmnkitchendetails> fallback =
                    search.GetSearchData("prdSearch", query);
                if (fallback != null && fallback.Count > 0)
                    return fallback;
            }
            catch (Exception ex)
            {
                clsgen.LogException("FrmKitchenView GetKitchenOrderDetails direct fallback", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Kitchen_View));
            }

            return new List<Dmnkitchendetails>();
        }

        private Tuple<int, string> GetDailyOrderToken(int orderMasterId)
        {
            try
            {
                BllSearch<DmnGridFields> search = new BllSearch<DmnGridFields>();
                DmnSearch query = new DmnSearch
                {
                    Select = "OrderTypeId Field1, TokenDisplay Field2",
                    Table = "tblOrderToken",
                    WhereNdGroupBy = "WHERE OrderMasterId = " + orderMasterId,
                    OrderBy = ""
                };

                List<DmnGridFields> rows = search.GetSearchData("prdSearch", query);
                if (rows != null && rows.Count > 0 &&
                    !string.IsNullOrWhiteSpace(Convert.ToString(rows[0].Field2)))
                {
                    return Tuple.Create(
                        Convert.ToInt32(rows[0].Field1),
                        Convert.ToString(rows[0].Field2));
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("FrmKitchenView GetDailyOrderToken()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Kitchen_View));
            }

            return null;
        }

        /// <summary>
        /// Builds one reusable KOT-style ticket. The product grid remains the same
        /// DataGridView used by the existing cancel, combo/add-on and order-status flows.
        /// </summary>
        private OrderCardCache BuildOrderCard(DmnGridFields orders, List<Dmnkitchendetails> details)
        {
            try
            {
                if (details == null || details.Count == 0)
                    return null;

                string suffix = Convert.ToString(details[0].Field17);
                string orderId = Convert.ToString(orders.Field1);
                string cardName = "tlp_product_box_k_" + orderId + suffix;
                OrderCardCache cache = new OrderCardCache();

                int hostWidth = flp_order_view.ClientSize.Width > 0
                    ? flp_order_view.ClientSize.Width
                    : ClientSize.Width;
                int availableWidth = Math.Max(320, hostWidth - flp_order_view.Padding.Horizontal -
                    SystemInformation.VerticalScrollBarWidth - 18);
                int columns = availableWidth >= 1120 ? 3 : availableWidth >= 760 ? 2 : 1;
                int controlWidth = Math.Max(320, (availableWidth - (columns - 1) * 18) / columns);
                bool kotOnlyMode = IsKitchenKOTOnlyMode;

                Tuple<int, string> dailyToken = GetDailyOrderToken(Convert.ToInt32(orderId));
                int dailyOrderTypeId = dailyToken == null ? 0 : dailyToken.Item1;
                string dailyTokenDisplay = dailyToken == null ? string.Empty : dailyToken.Item2;
                bool isPickupOrder = Convert.ToInt32(details[0].Field25) > 0 ||
                    dailyOrderTypeId == 1 || dailyOrderTypeId == 2;

                string orderNumber = !string.IsNullOrWhiteSpace(dailyTokenDisplay)
                    ? dailyTokenDisplay
                    : isPickupOrder
                        ? Convert.ToString(details[0].Field25)
                        : Convert.ToString(details[0].Field22);
                if (string.IsNullOrWhiteSpace(orderNumber))
                    orderNumber = Convert.ToString(details[0].Field17);

                string orderType = dailyOrderTypeId == 2
                    ? "TAKEAWAY"
                    : dailyOrderTypeId == 1 || isPickupOrder
                        ? "PICKUP"
                        : "DINE IN";
                string tableCodeText = isPickupOrder ||
                    string.IsNullOrWhiteSpace(Convert.ToString(details[0].Field2))
                    ? "—"
                    : Convert.ToString(details[0].Field2);


                int controlHeight = kotOnlyMode ? 480 : 530;

                ModernKitchenOrderCard card = new ModernKitchenOrderCard
                {
                    Name = cardName,
                    Size = new Size(controlWidth, controlHeight),
                    MinimumSize = new Size(320, 390),
                    Margin = new Padding(8, 8, 8, 12),
                    Padding = new Padding(7),
                    BackColor = Color.White,
                    Visible = true,
                    RowCount = 6,
                    ColumnCount = 4
                };

                card.ColumnStyles.Clear();
                card.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
                card.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
                card.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
                card.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));

                card.RowStyles.Clear();
                card.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
                card.RowStyles.Add(new RowStyle(SizeType.Absolute, 98F));
                card.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                card.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
                card.RowStyles.Add(new RowStyle(SizeType.Absolute, kotOnlyMode ? 52F : 96F));
                card.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
                card.SuspendLayout();

                // Purple ticket masthead with chef-hat artwork.
                RoundedKitchenHeaderPanel masthead = new RoundedKitchenHeaderPanel
                {
                    Name = "pnl_kot_header_" + orderId + suffix,
                    Dock = DockStyle.Fill,
                    BackColor = ModernKitchenOrderCard.TicketPurple,
                    CornerRadius = 10,
                    Margin = new Padding(1, 1, 1, 2),
                    Padding = new Padding(5, 2, 5, 2)
                };

                PictureBox chefHat = CreateKitchenIcon(
                    "KOT_ChefHat.png", "pb_kot_chefhat_" + orderId + suffix, new Size(27, 27));
                chefHat.Dock = DockStyle.None;
                chefHat.Size = new Size(27, 27);
                chefHat.SizeMode = PictureBoxSizeMode.Zoom;
                chefHat.Margin = new Padding(0, 0, 7, 0);

                Label titleLabel = new Label
                {
                    Name = "lbl_kot_title_" + orderId + suffix,
                    Text = "KITCHEN ORDER TICKET (KOT)",
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Margin = Padding.Empty,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    AutoEllipsis = true,
                    ForeColor = Color.White,
                    BackColor = Color.Transparent
                };

                FlowLayoutPanel titleGroup = new FlowLayoutPanel
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false,
                    Margin = Padding.Empty,
                    Padding = Padding.Empty,
                    BackColor = Color.Transparent
                };
                titleGroup.Controls.Add(chefHat);
                titleGroup.Controls.Add(titleLabel);

                TableLayoutPanel mastheadLayout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 3,
                    RowCount = 1,
                    Margin = Padding.Empty,
                    Padding = Padding.Empty,
                    BackColor = Color.Transparent
                };
                mastheadLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                mastheadLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                mastheadLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                mastheadLayout.Controls.Add(titleGroup, 1, 0);
                masthead.Controls.Add(mastheadLayout);
                card.Controls.Add(masthead, 0, 0);
                card.SetColumnSpan(masthead, 4);

                // Token number is the strongest visual element, with icon-led metadata.
                TableLayoutPanel orderMeta = new TableLayoutPanel
                {
                    Name = "pnl_kot_meta_" + orderId + suffix,
                    Dock = DockStyle.Fill,
                    ColumnCount = 3,
                    RowCount = 1,
                    Padding = new Padding(5, 4, 5, 4),
                    Margin = Padding.Empty,
                    BackColor = Color.White
                };
                orderMeta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
                orderMeta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
                orderMeta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));

                Panel orderNoPanel = new Panel
                {
                    Name = "pnl_kot_orderno_" + orderId + suffix,
                    Dock = DockStyle.Fill,
                    BackColor = Color.FromArgb(244, 239, 251),
                    Margin = new Padding(2, 3, 8, 3),
                    Padding = new Padding(4, 3, 4, 3)
                };
                TableLayoutPanel orderNoLayout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 2,
                    Margin = Padding.Empty,
                    Padding = Padding.Empty,
                    BackColor = Color.Transparent
                };
                orderNoLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
                orderNoLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 58F));
                Label orderCaption = new Label
                {
                    Name = "lbl_tokkenno_caption_k_" + orderId + suffix,
                    Text = "Order No.",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.BottomCenter,
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                    ForeColor = ModernKitchenOrderCard.TicketPurple,
                    BackColor = Color.Transparent,
                    Margin = Padding.Empty
                };
                Label orderNoLabel = new Label
                {
                    Name = "lbl_tokkenno_k_" + orderId + suffix,
                    Text = orderNumber,
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 19F, FontStyle.Bold),
                    ForeColor = ModernKitchenOrderCard.TicketPurple,
                    BackColor = Color.Transparent,
                    Margin = Padding.Empty,
                    AutoEllipsis = true
                };
                orderNoLayout.Controls.Add(orderCaption, 0, 0);
                orderNoLayout.Controls.Add(orderNoLabel, 0, 1);
                orderNoPanel.Controls.Add(orderNoLayout);

                Panel typePanel = CreateKitchenMetaIconPanel(
                    "pnl_kot_type_" + orderId + suffix,
                    "KOT_OrderTypeUtensils.png",
                    "Order Type",
                    orderType,
                    "lbl_order_type_k_" + orderId + suffix,
                    24);
                Panel timePanel = CreateKitchenTimePanel(
                    "pnl_kot_time_" + orderId + suffix,
                    orderId + suffix,
                    details[0].Fielddatetime,
                    GetKitchenStartTime(details));
                orderMeta.Controls.Add(orderNoPanel, 0, 0);
                orderMeta.Controls.Add(typePanel, 1, 0);
                orderMeta.Controls.Add(timePanel, 2, 0);
                card.Controls.Add(orderMeta, 0, 1);
                card.SetColumnSpan(orderMeta, 4);

                DataGridView grid = BuildProductGrid(orders, details);
                grid.Dock = DockStyle.Fill;
                grid.Margin = new Padding(5, 8, 5, 5);
                grid.ColumnHeadersHeight = 36;
                grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                grid.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                grid.RowTemplate.Height = 40;
                grid.Columns[0].Width = 42;
                grid.Columns[1].Width = 38;
                grid.Columns[2].Width = 38;
                grid.Columns[3].FillWeight = 240F;
                grid.Columns[4].Width = 58;
                grid.Columns[5].Visible = false;
                card.Controls.Add(grid, 0, 2);
                card.SetColumnSpan(grid, 4);

                Panel notesPanel = new Panel
                {
                    Name = "pnl_kot_notes_" + orderId + suffix,
                    Dock = DockStyle.Fill,
                    BackColor = Color.FromArgb(244, 239, 251),
                    Margin = new Padding(5, 5, 5, 3),
                    Padding = new Padding(8, 4, 8, 4)
                };
                PictureBox notesIcon = CreateKitchenIcon(
                    "KOT_NotesClipboard.png", "pb_kot_notes_" + orderId + suffix, new Size(24, 24));
                Label notesCaption = new Label
                {
                    Text = "Notes :",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = ModernKitchenOrderCard.TicketPurple,
                    BackColor = Color.Transparent,
                    Margin = new Padding(4, 0, 0, 0)
                };
                TextBox description = new TextBox
                {
                    Name = "txt_Description_k_" + orderId + suffix,
                    Text = string.IsNullOrWhiteSpace(Convert.ToString(details[0].Field16))
                        ? "—" : Convert.ToString(details[0].Field16).Trim(),
                    Dock = DockStyle.Fill,
                    ForeColor = Color.FromArgb(35, 30, 55),
                    BackColor = notesPanel.BackColor,
                    ReadOnly = true,
                    Multiline = false,
                    ScrollBars = ScrollBars.None,
                    BorderStyle = BorderStyle.None,
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    Margin = new Padding(4, 0, 0, 0),
                    Padding = new Padding(2),
                    Visible = true,
                    TabStop = false
                };
                TableLayoutPanel notesLayout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 2,
                    Margin = Padding.Empty,
                    Padding = Padding.Empty,
                    BackColor = Color.Transparent
                };
                notesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
                notesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                notesLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
                notesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                notesLayout.Controls.Add(notesIcon, 0, 0);
                notesLayout.Controls.Add(notesCaption, 1, 0);
                notesLayout.Controls.Add(description, 1, 1);
                notesPanel.Controls.Add(notesLayout);
                card.Controls.Add(notesPanel, 0, 3);
                card.SetColumnSpan(notesPanel, 4);

                PictureBox startButton = kotOnlyMode ? null : CreateKitchenActionButton(
                    "pb_start_k_" + orderId + suffix, "START", KitchenActionStyle.Start,
                    suffix, orderId, pbstart_Click);
                PictureBox readyButton = CreateKitchenActionButton(
                    "pb_ready_k_" + orderId + suffix,
                    kotOnlyMode ? "OKAY" : "READY",
                    kotOnlyMode ? KitchenActionStyle.Okay : KitchenActionStyle.Ready,
                    suffix, orderId, pbreadyfood_Click);
                readyButton.Visible = kotOnlyMode;
                PictureBox cancelButton = kotOnlyMode ? null : CreateKitchenActionButton(
                    "pictureBoxRemove_k_" + orderId + suffix, "CANCEL ORDER",
                    KitchenActionStyle.Cancel, suffix, orderId, pbcancelfood_Click);
                PictureBox reprintButton = CreateKitchenActionButton(
                    "reprintbutton_k_" + orderId + suffix, "REPRINT",
                    KitchenActionStyle.Reprint, suffix, orderId, reprintbutton_Click);

                TableLayoutPanel actionStrip = new TableLayoutPanel
                {
                    Name = "pnl_actions_k_" + orderId + suffix,
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    GrowStyle = TableLayoutPanelGrowStyle.FixedSize,
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                    Margin = new Padding(2, 0, 2, 2),
                    Padding = Padding.Empty,
                    BackColor = Color.White,
                    ColumnCount = 2,
                    RowCount = kotOnlyMode ? 1 : 2
                };
                actionStrip.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                actionStrip.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                if (kotOnlyMode)
                    actionStrip.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                else
                {
                    actionStrip.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                    actionStrip.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                }

                foreach (PictureBox actionButton in new[] { startButton, readyButton, cancelButton, reprintButton })
                {
                    if (actionButton == null)
                        continue;
                    actionButton.Dock = DockStyle.Fill;
                    actionButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    actionButton.Margin = new Padding(4, 3, 4, 3);
                }

                Panel startReadySlot = new Panel
                {
                    Name = "pnl_start_ready_slot_" + orderId + suffix,
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    Margin = new Padding(4, 3, 4, 3),
                    Padding = Padding.Empty
                };

                if (startButton != null)
                {
                    startButton.Dock = DockStyle.Fill;
                    startButton.Margin = Padding.Empty;
                    startReadySlot.Controls.Add(startButton);
                }

                readyButton.Dock = DockStyle.Fill;
                readyButton.Margin = Padding.Empty;
                startReadySlot.Controls.Add(readyButton);

                // START and READY are layered in one slot; only one is visible at a time.
                actionStrip.Controls.Add(startReadySlot, 0, 0);

                if (kotOnlyMode)
                {
                    actionStrip.Controls.Add(reprintButton, 1, 0);
                }
                else
                {
                    // START/READY remains top-left. CANCEL moves to the READY slot;
                    // REPRINT moves to the old CANCEL slot. Bottom-right stays empty.
                    actionStrip.Controls.Add(cancelButton, 1, 0);
                    actionStrip.Controls.Add(reprintButton, 0, 1);
                }
                card.Controls.Add(actionStrip, 0, 4);
                card.SetColumnSpan(actionStrip, 4);

                // Receipt perforation + kitchen copy stamp is the last section in the card.
                Panel receiptFooter = new Panel
                {
                    Name = "pnl_kot_receipt_footer_" + orderId + suffix,
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    Margin = new Padding(5, 0, 5, 0),
                    Padding = Padding.Empty
                };
                PictureBox copyHat = CreateKitchenIcon(
                    "KOT_KitchenCopyChefHat.png",
                    "pb_kot_copy_hat_" + orderId + suffix,
                    new Size(28, 28));
                Label copyLabel = new Label
                {
                    Name = "lbl_kot_copy_" + orderId + suffix,
                    Text = "KITCHEN COPY",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(20, 20, 48),
                    BackColor = Color.White,
                    Margin = Padding.Empty
                };
                TableLayoutPanel footerLayout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 2,
                    Margin = Padding.Empty,
                    Padding = Padding.Empty,
                    BackColor = Color.White
                };
                footerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                footerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                footerLayout.Controls.Add(copyHat, 0, 0);
                footerLayout.Controls.Add(copyLabel, 0, 1);
                receiptFooter.Controls.Add(footerLayout);
                card.Controls.Add(receiptFooter, 0, 5);
                card.SetColumnSpan(receiptFooter, 4);

                bool waitingForCustomer = Convert.ToInt32(details[0].Field27) == 1;

                cache.CardPanel = card;
                cache.Grid = grid;
                cache.TimeLabel = timePanel.Controls.Find("lbl_time_K_" + orderId + suffix, true).FirstOrDefault() as Label;
                cache.StartTimeLabel = timePanel.Controls.Find("lbl_start_time_K_" + orderId + suffix, true).FirstOrDefault() as Label;
                cache.ElapsedTimeLabel = timePanel.Controls.Find("lbl_elapsed_time_K_" + orderId + suffix, true).FirstOrDefault() as Label;
                cache.StatusLabel = null;
                cache.TableCodeLabel = null;
                cache.TokenLabel = orderNoLabel;
                cache.OrderTypeLabel = typePanel.Controls.Find("lbl_order_type_k_" + orderId + suffix, true).FirstOrDefault() as Label;
                cache.Description = description;
                cache.DescriptionPanel = notesPanel;
                cache.BtnStart = startButton;
                cache.BtnReady = readyButton;
                cache.BtnCancel = cancelButton;
                cache.BtnReprint = reprintButton;
                cache.CreatedTime = details[0].Fielddatetime;
                cache.CancelStatus = Convert.ToInt32(details[0].Field27);

                ApplyKitchenActionVisibility(cache, details);
                card.SetKitchenState(GetKitchenVisualState(waitingForCustomer, details));

                orderbox.RemoveAll(x => string.Equals(
                    x.datagridviewname, grid.Name, StringComparison.OrdinalIgnoreCase));
                orderbox.Add(new datagrid
                {
                    Id = Convert.ToInt32(details[0].Field8),
                    datagridviewname = grid.Name,
                    createtime = details[0].Fielddatetime
                });

                UpdateGridFull(grid, details);
                if (waitingForCustomer)
                    description.Text = "WAITING FOR CUSTOMER";

                ToggleDescriptionRow(card, !string.IsNullOrWhiteSpace(description.Text));

                FrmDashboard dashboard = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                if (dashboard != null)
                    card.Enabled = dashboard.ChkWritePermission(Convert.ToInt32(formsenum.Kitchen_View));

                card.ResumeLayout(true);
                return cache;
            }
            catch (Exception ex)
            {
                clsgen.LogException("FrmKitchenView BuildOrderCard()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Kitchen_View));
                return null;
            }
        }


        private static readonly Dictionary<string, Image> KitchenIconCache =
            new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
        private static readonly object KitchenIconCacheLock = new object();

        private static Image LoadKitchenIcon(string fileName)
        {
            lock (KitchenIconCacheLock)
            {
                Image cached;
                if (KitchenIconCache.TryGetValue(fileName, out cached))
                    return cached;

                string path = Path.Combine(Application.StartupPath, "Resources", fileName);
                if (!File.Exists(path))
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", fileName);
                if (!File.Exists(path))
                    return null;

                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (Image source = Image.FromStream(stream))
                {
                    cached = new Bitmap(source);
                    KitchenIconCache[fileName] = cached;
                    return cached;
                }
            }
        }

        private static PictureBox CreateKitchenIcon(string fileName, string name, Size size)
        {
            return new PictureBox
            {
                Name = name,
                Image = LoadKitchenIcon(fileName),
                Size = size,
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                TabStop = false
            };
        }

        private static Panel CreateKitchenMetaIconPanel(
            string name, string iconFile, string caption, string value, string valueLabelName, int iconWidth)
        {
            Panel panel = new Panel
            {
                Name = name,
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(3, 2, 3, 2),
                Padding = new Padding(2, 2, 2, 2)
            };

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Margin = Padding.Empty,
                Padding = Padding.Empty,
                BackColor = Color.Transparent
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Math.Max(22, iconWidth)));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            PictureBox icon = CreateKitchenIcon(
                iconFile, name + "_icon", new Size(19, 19));
            icon.Dock = DockStyle.None;
            icon.Anchor = AnchorStyles.None;
            icon.Size = new Size(19, 19);
            icon.Margin = Padding.Empty;

            Label captionLabel = new Label
            {
                Text = caption,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(86, 65, 117),
                BackColor = Color.Transparent,
                Margin = new Padding(1, 0, 0, 0),
                AutoEllipsis = true
            };

            Label valueLabel = new Label
            {
                Name = valueLabelName,
                Text = string.IsNullOrWhiteSpace(value) ? "—" : value,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 26, 64),
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                AutoEllipsis = true
            };

            layout.Controls.Add(icon, 0, 0);
            layout.Controls.Add(captionLabel, 1, 0);
            layout.Controls.Add(valueLabel, 0, 1);
            layout.SetColumnSpan(valueLabel, 2);
            panel.Controls.Add(layout);
            return panel;
        }

        private Panel CreateKitchenTimePanel(
            string name, string suffix, DateTime orderTime, DateTime? startTime)
        {
            Panel panel = new Panel
            {
                Name = name,
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(3, 2, 3, 2),
                Padding = new Padding(2, 1, 2, 1)
            };

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4,
                Margin = Padding.Empty,
                Padding = Padding.Empty,
                BackColor = Color.Transparent
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 26F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 26F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));

            FlowLayoutPanel heading = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = Padding.Empty,
                Padding = Padding.Empty,
                BackColor = Color.Transparent
            };
            PictureBox clock = CreateKitchenIcon(
                "KOT_OrderTimeClock.png", "pb_kot_time_icon_" + suffix, new Size(17, 17));
            clock.Dock = DockStyle.None;
            clock.Size = new Size(17, 17);
            clock.Margin = new Padding(0, 1, 4, 0);
            Label headingLabel = new Label
            {
                Text = "TIME DETAILS",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(86, 65, 117),
                BackColor = Color.Transparent,
                Margin = new Padding(0, 2, 0, 0)
            };
            heading.Controls.Add(clock);
            heading.Controls.Add(headingLabel);
            layout.Controls.Add(heading, 0, 0);
            layout.SetColumnSpan(heading, 2);

            AddTimeInfoRow(layout, 1, "Order", FormatKitchenOrderTime(orderTime, DateTime.Now),
                "lbl_time_K_" + suffix, false);
            AddTimeInfoRow(layout, 2, "Started", startTime.HasValue
                ? FormatKitchenOrderTime(startTime.Value, DateTime.Now) : "—",
                "lbl_start_time_K_" + suffix, false);
            AddTimeInfoRow(layout, 3, "Elapsed", startTime.HasValue
                ? FormatKitchenElapsed(DateTime.Now - startTime.Value) : "Not started",
                "lbl_elapsed_time_K_" + suffix, true);

            panel.Controls.Add(layout);
            return panel;
        }

        private static void AddTimeInfoRow(
            TableLayoutPanel layout, int row, string caption, string value,
            string valueName, bool highlight)
        {
            Label captionLabel = new Label
            {
                Text = caption,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(102, 91, 119),
                BackColor = Color.Transparent,
                Margin = new Padding(2, 0, 0, 0),
                AutoEllipsis = true
            };
            Label valueLabel = new Label
            {
                Name = valueName,
                Text = value,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = highlight
                    ? Color.FromArgb(77, 43, 135)
                    : Color.FromArgb(35, 26, 64),
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, 2, 0),
                AutoEllipsis = true
            };
            layout.Controls.Add(captionLabel, 0, row);
            layout.Controls.Add(valueLabel, 1, row);
        }

        private static DateTime? GetKitchenStartTime(IEnumerable<Dmnkitchendetails> details)
        {
            if (details == null)
                return null;

            DateTime? firstStart = null;
            foreach (Dmnkitchendetails detail in details)
            {
                if (detail == null)
                    continue;

                DateTime value = detail.StartDate;
                if (value == DateTime.MinValue || value.Year < 2000)
                    continue;

                if (!firstStart.HasValue || value < firstStart.Value)
                    firstStart = value;
            }

            return firstStart;
        }

        private static TextBox FindNotesTextBox(Control parent)
        {
            if (parent == null)
                return null;

            TextBox textBox = parent as TextBox;
            if (textBox != null && textBox.Name.StartsWith("txt_Description_k_", StringComparison.Ordinal))
                return textBox;

            foreach (Control child in parent.Controls)
            {
                TextBox found = FindNotesTextBox(child);
                if (found != null)
                    return found;
            }
            return null;
        }

        private Label CreateKitchenMetaLabel(string name, string caption, string value)
        {
            return new Label
            {
                Name = name,
                Text = caption + Environment.NewLine +
                    (string.IsNullOrWhiteSpace(value) ? "—" : value),
                Dock = DockStyle.Fill,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(3, 2, 3, 2),
                Margin = new Padding(3, 4, 3, 4),
                Font = ModernKitchenOrderCard.MetaFont,
                ForeColor = Color.FromArgb(49, 31, 83),
                BackColor = Color.FromArgb(244, 239, 251)
            };
        }

        private static string BuildKitchenNotesText(Dmnkitchendetails details)
        {
            string note = details == null ? string.Empty : Convert.ToString(details.Field16);
            return "NOTES" + Environment.NewLine +
                (string.IsNullOrWhiteSpace(note) ? "—" : note.Trim());
        }

        private DataGridView BuildProductGrid(DmnGridFields orders, List<Dmnkitchendetails> details)
        {
            DataGridView grid = new RoundedKitchenGridView();
            string suffix = Convert.ToString(details[0].Field17);

            grid.Name = "dgv_product_details_k_" + orders.Field1 + suffix;
            grid.Tag = orders.Field1 + suffix;
            grid.CellClick += dgv_product_details_CellClick;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = true;
            grid.AllowUserToResizeRows = false;
            grid.AllowUserToResizeColumns = false;
            grid.RowHeadersVisible = false;
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = Color.White;
            grid.Margin = new Padding(5, 6, 5, 5);
            grid.GridColor = Color.FromArgb(226, 218, 239);
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(88, 48, 143);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = ModernKitchenOrderCard.GridHeaderFont;
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 4, 0);
            grid.ColumnHeadersHeight = 34;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(29, 25, 45);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 226, 247);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 27, 65);
            grid.DefaultCellStyle.Font = ModernKitchenOrderCard.GridFont;
            grid.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 248, 253);
            grid.RowTemplate.Height = screenHeight > 850 ? 36 : 32;
            grid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            grid.ScrollBars = ScrollBars.Vertical;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            grid.Columns.Clear();
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Slno", HeaderText = "#" });
            grid.Columns.Add(new DataGridViewImageColumn { Name = "Action", HeaderText = "" });
            grid.Columns.Add(new DataGridViewImageColumn { Name = "PType", HeaderText = "" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemName", HeaderText = "Item Name" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Qty", HeaderText = "Qty" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Notes", HeaderText = "Notes" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RemoveNotes", HeaderText = "Remove Notes" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "orderdetailsid", HeaderText = "orderdetailsid" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "koid", HeaderText = "koid" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "tid", HeaderText = "tid" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ordermasterid", HeaderText = "Ordermasterid" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "datetime", HeaderText = "datetime" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "status", HeaderText = "status" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "itemid", HeaderText = "itemid" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "tabletype", HeaderText = "tabletype" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "orderno", HeaderText = "orderno" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "AddonStatus", HeaderText = "AddonStatus" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "AVID", HeaderText = "AVID" });

            // Preserve every original cell index used by row cancellation and addon/combos.
            for (int index = 6; index < grid.Columns.Count; index++)
            {
                grid.Columns[index].Visible = false;
                grid.Columns[index].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grid.Columns[index].Width = 1;
            }

            grid.Columns[0].Width = 34;
            grid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns[1].Width = 38;
            grid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns[1].DefaultCellStyle.Padding = new Padding(2);
            grid.Columns[2].Width = 38;
            grid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns[2].DefaultCellStyle.Padding = new Padding(2);
            grid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grid.Columns[3].FillWeight = 220F;
            grid.Columns[4].Width = 48;
            grid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grid.Columns[5].FillWeight = 95F;

            return grid;
        }

        private TextBox BuildDescription(DmnGridFields orders, List<Dmnkitchendetails> details)
        {
            return new TextBox
            {
                Name = "txt_Description_k_" + orders.Field1 + details[0].Field17,
                Text = BuildKitchenNotesText(details[0]),
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(76, 45, 128),
                BackColor = Color.FromArgb(244, 239, 251),
                ReadOnly = true,
                Multiline = true,
                ScrollBars = ScrollBars.None,
                BorderStyle = BorderStyle.None,
                Font = ModernKitchenOrderCard.MetaFont,
                Margin = new Padding(4),
                Padding = new Padding(8),
                Visible = true,
                TabStop = false
            };
        }

        private PictureBox CreateKitchenActionButton(
            string name, string text, KitchenActionStyle style, object suffix,
            object orderMasterId, EventHandler clickHandler)
        {
            ModernKitchenActionButton button = new ModernKitchenActionButton
            {
                Name = name,
                ButtonText = text,
                ActionStyle = style,
                SizeMode = PictureBoxSizeMode.Normal,
                Dock = DockStyle.Fill,
                Margin = new Padding(4, 3, 4, 3),
                Tag = suffix,
                AccessibleDescription = Convert.ToString(orderMasterId),
                Cursor = Cursors.Hand
            };
            button.Click += clickHandler;
            return button;
        }

        private static string FormatKitchenOrderTime(DateTime orderTime, DateTime now)
        {
            return orderTime.ToString("hh:mm tt");
        }

        private static string FormatKitchenElapsed(TimeSpan elapsed)
        {
            if (elapsed < TimeSpan.Zero)
                elapsed = TimeSpan.Zero;

            if (elapsed.TotalHours >= 1)
                return ((int)elapsed.TotalHours).ToString("00") + "h " +
                    elapsed.Minutes.ToString("00") + "m";

            if (elapsed.TotalMinutes >= 1)
                return elapsed.Minutes.ToString("00") + "m " +
                    elapsed.Seconds.ToString("00") + "s";

            return elapsed.Seconds.ToString("00") + "s";
        }

        private static KitchenOrderVisualState GetKitchenVisualState(
            bool waitingForCustomer, IEnumerable<Dmnkitchendetails> details)
        {
            if (waitingForCustomer)
                return KitchenOrderVisualState.WaitingForCustomer;

            if (details == null)
                return KitchenOrderVisualState.NewOrder;

            // Keep the ticket in NEW ORDER while any line is not started. If all
            // lines have started, status 2 represents active kitchen preparation.
            if (details.Any(item => Convert.ToInt32(item.Field12) == 1))
                return KitchenOrderVisualState.NewOrder;

            if (details.Any(item => Convert.ToInt32(item.Field12) == 2))
                return KitchenOrderVisualState.InProgress;

            return KitchenOrderVisualState.Ready;
        }

        private void UpdateOrderCard(OrderCardCache cache, DmnGridFields orders, List<Dmnkitchendetails> details)
        {
            try
            {
                if (cache == null || cache.CardPanel == null ||
                    cache.Grid == null || details == null || details.Count == 0)
                    return;

                cache.CreatedTime = details[0].Fielddatetime;
                DateTime now = DateTime.Now;
                DateTime? startTime = GetKitchenStartTime(details);

                if (cache.TimeLabel != null)
                    cache.TimeLabel.Text = FormatKitchenOrderTime(cache.CreatedTime, now);

                if (cache.StartTimeLabel != null)
                    cache.StartTimeLabel.Text = startTime.HasValue
                        ? FormatKitchenOrderTime(startTime.Value, now)
                        : "—";

                if (cache.ElapsedTimeLabel != null)
                    cache.ElapsedTimeLabel.Text = startTime.HasValue
                        ? FormatKitchenElapsed(now - startTime.Value)
                        : "Not started";

                if (cache.Grid.Rows.Count == details.Count)
                    UpdateGridLight(cache.Grid, details);
                else
                    UpdateGridFull(cache.Grid, details);

                bool waitingForCustomer = Convert.ToInt32(details[0].Field27) == 1;

                if (cache.Description != null)
                {
                    string note = waitingForCustomer
                        ? "WAITING FOR CUSTOMER"
                        : (Convert.ToString(details[0].Field16) ?? string.Empty).Trim();

                    cache.Description.Text = note;
                }

                ApplyKitchenActionVisibility(cache, details);

                bool showNotes = cache.Description != null &&
                    !string.IsNullOrWhiteSpace(cache.Description.Text);
                ToggleDescriptionRow(cache.CardPanel, showNotes);

                KitchenOrderVisualState nextVisualState =
                    GetKitchenVisualState(waitingForCustomer, details);
                if (!(cache.CardPanel.Tag is KitchenOrderVisualState) ||
                    (KitchenOrderVisualState)cache.CardPanel.Tag != nextVisualState)
                {
                    cache.CardPanel.Tag = nextVisualState;
                    cache.CardPanel.SetKitchenState(nextVisualState);
                }

                cache.CancelStatus = Convert.ToInt32(details[0].Field27);
            }
            catch (Exception ex)
            {
                clsgen.LogException("FrmKitchenView UpdateOrderCard()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        private void ToggleDescriptionRow(TableLayoutPanel card, bool show)
        {
            if (card == null || card.IsDisposed || card.RowStyles.Count < 6)
                return;

            card.SuspendLayout();
            try
            {
                // A hidden notes band gives its full height back to the product grid.
                card.RowStyles[2].SizeType = SizeType.Percent;
                card.RowStyles[2].Height = 100F;
                card.RowStyles[3].SizeType = SizeType.Absolute;
                card.RowStyles[3].Height = show ? 44F : 0F;

                Control notesPanel = card.Controls.Cast<Control>()
                    .FirstOrDefault(control => control.Name.StartsWith(
                        "pnl_kot_notes_", StringComparison.Ordinal));

                if (notesPanel != null)
                    notesPanel.Visible = show;

                TextBox notes = FindNotesTextBox(card);
                if (notes != null)
                    notes.Visible = show;
            }
            finally
            {
                card.ResumeLayout(true);
            }
        }

        /// <summary>
        /// UpdateGrid with blinking canceled rows logic.
        /// Uses KOID (Field8) as unique row key; falls back to orderdetailsid (Field9)
        /// </summary>
        private void UpdateGridFull( DataGridView dgv_product_details, List<Dmnkitchendetails> gridSourcedetails)
        {
            if (dgv_product_details == null || gridSourcedetails == null)
                return;

            // Save scroll position
            int scrollIndex = -1;
            if (dgv_product_details.FirstDisplayedScrollingRowIndex >= 0)
                scrollIndex = dgv_product_details.FirstDisplayedScrollingRowIndex;

            dgv_product_details.SuspendLayout();
            dgv_product_details.Rows.Clear();

            bool comboflag = false;
            bool addonflag = false;
            int mainNo=1, subNo = 1;
            for (int k = 0; k < gridSourcedetails.Count; k++)
            {
                var rowObj = gridSourcedetails[k];
                dgv_product_details.Rows.Add();

                // Unique key for blinking
                string keyRow = Convert.ToString(rowObj.Field8);
                if (string.IsNullOrEmpty(keyRow))
                    keyRow = Convert.ToString(rowObj.Field9);

                if (!RowBlinkCounter.ContainsKey(keyRow))
                    RowBlinkCounter[keyRow] = 0;

                if (!RowBlinkState.ContainsKey(keyRow))
                    RowBlinkState[keyRow] = false;

                // Sl No
                dgv_product_details.Rows[k].Cells[dslno].Value = k + 1;

                #region Slno Setting
                int prodStatus = Convert.ToInt32(rowObj.Field19);
                // MAIN PRODUCT
                if (prodStatus == 1)
                {
                    dgv_product_details.Rows[k].Cells[dslno].Value = mainNo;
                    dgv_product_details.Rows[k].Cells[dslno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    mainNo++;
                    subNo = 1;
                }
                // SUB PRODUCT
                else if (prodStatus == 2)
                {
                    dgv_product_details.Rows[k].Cells[dslno].Value = subNo;
                    dgv_product_details.Rows[k].Cells[dslno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_product_details.Rows[k].DefaultCellStyle.BackColor =   Color.WhiteSmoke;
                    subNo++;
                }
                #endregion

                // Item name + variant
                string variant = Convert.ToString(rowObj.Field24);
                string itemName = Convert.ToString(rowObj.Field4);
                dgv_product_details.Rows[k].Cells[ditemno].Value =
                    !string.IsNullOrEmpty(variant)
                        ? itemName + " ( " + variant + " )"
                        : itemName;

                // PType icon
                DataGridViewImageCell typeCell =
                    (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dptypeno];

                if (Convert.ToInt32(rowObj.Field13) == Convert.ToInt32(Tabletype.ComboProduct))
                {
                    comboflag = true;
                    typeCell.Value = Properties.Resources.kitchengridcomboicon;
                }
                else if (Convert.ToInt32(rowObj.Field23) >= 1)
                {
                    addonflag = true;
                    typeCell.Value = Properties.Resources.addonbutton;
                }
                else
                {
                    typeCell.Value = (k % 2 == 0)
                        ? Properties.Resources.orderview_blank
                        : Properties.Resources.orderview_blank_2;
                }

                typeCell.ImageLayout = DataGridViewImageCellLayout.Zoom;

                // Qty
                decimal qty;
                decimal.TryParse(Convert.ToString(rowObj.Field5), out qty);
                dgv_product_details.Rows[k].Cells[dqtyno].Value = (int)Math.Floor(qty);


                // Backend fields
                dgv_product_details.Rows[k].Cells[dnotes].Value = rowObj.Field6;
                dgv_product_details.Rows[k].Cells[dremnoteno].Value = rowObj.Field11;
                dgv_product_details.Rows[k].Cells[ddetailsidno].Value = rowObj.Field9;
                dgv_product_details.Rows[k].Cells[dkoidno].Value = rowObj.Field8;
                dgv_product_details.Rows[k].Cells[dtidno].Value = rowObj.Field10;
                dgv_product_details.Rows[k].Cells[dordermasteridno].Value = rowObj.Field1;
                dgv_product_details.Rows[k].Cells[ddatetimeno].Value = rowObj.Fielddatetime;
                dgv_product_details.Rows[k].Cells[dstatusno].Value = rowObj.Field12;
                dgv_product_details.Rows[k].Cells[ditemidno].Value = rowObj.Field14;
                dgv_product_details.Rows[k].Cells[dtabletypeno].Value = rowObj.Field15;
                dgv_product_details.Rows[k].Cells[dorderno].Value = rowObj.Field17;

                dgv_product_details.Rows[k].Cells[dAddonStatusno].Value = rowObj.Field19;
                dgv_product_details.Rows[k].Cells[dAVIDno].Value = rowObj.Field20;

                // Addon
                //if (Convert.ToInt32(rowObj.Field19) == Convert.ToInt32(AddonType.AddonType))
                //{
  
                //}
                //else
                //{
                //    dgv_product_details.Rows[k].Cells[dAddonStatusno].Value =  Convert.ToInt32(Product_Addon_Variation_Type.Addon);
                //    dgv_product_details.Rows[k].Cells[dAVIDno].Value = rowObj.Field20;
                //}

                // Action + blinking
                DataGridViewImageCell actionCell =
                    (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dactionno];

                int status;
                int.TryParse(Convert.ToString(rowObj.Field12), out status);

                if (status == 0)
                {
                    RowBlinkCounter[keyRow]++;
                    if (RowBlinkCounter[keyRow] >= BlinkThreshold)
                    {
                        RowBlinkCounter[keyRow] = 0;
                        RowBlinkState[keyRow] = !RowBlinkState[keyRow];
                    }

                    dgv_product_details.Rows[k].DefaultCellStyle.BackColor =
                        RowBlinkState[keyRow] ? Color.LightCoral : Color.White;

                    actionCell.Value = Properties.Resources.orderview_cancelled;
                }
                else
                {
                    actionCell.Value = Properties.Resources.kitchenfood_cancel_icon;

                    if (Convert.ToInt32(rowObj.Field19) == 1)
                        dgv_product_details.Rows[k].DefaultCellStyle.BackColor = Color.White;

                }

                actionCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dgv_product_details.Rows[k].ReadOnly = true;
            }

            dgv_product_details.Columns[dptypeno].Visible = comboflag || addonflag;

            dgv_product_details.ResumeLayout(true);

            // Restore scroll
            if (scrollIndex >= 0 && scrollIndex < dgv_product_details.RowCount)
                dgv_product_details.FirstDisplayedScrollingRowIndex = scrollIndex;
        }


        private void UpdateGridLight(DataGridView dgv, List<Dmnkitchendetails> details)
        {
            if (dgv == null || details == null)
                return;

            bool comboPresent = false;
            bool addonPresent = false;

            for (int k = 0; k < details.Count && k < dgv.Rows.Count; k++)
            {
                Dmnkitchendetails rowObj = details[k];
                DataGridViewRow row = dgv.Rows[k];

                string keyRow = Convert.ToString(rowObj.Field8);
                if (string.IsNullOrEmpty(keyRow))
                    keyRow = Convert.ToString(rowObj.Field9);

                if (!RowBlinkCounter.ContainsKey(keyRow))
                    RowBlinkCounter[keyRow] = 0;
                if (!RowBlinkState.ContainsKey(keyRow))
                    RowBlinkState[keyRow] = false;

                // Keep the displayed product and backend values synchronized without rebuilding rows.
                string itemName = Convert.ToString(rowObj.Field4);
                string variant = Convert.ToString(rowObj.Field24);
                row.Cells[ditemno].Value = string.IsNullOrWhiteSpace(variant)
                    ? itemName
                    : itemName + " ( " + variant + " )";

                decimal quantity;
                decimal.TryParse(Convert.ToString(rowObj.Field5), out quantity);
                row.Cells[dqtyno].Value = (int)Math.Floor(quantity);
                row.Cells[dnotes].Value = rowObj.Field6;
                row.Cells[dremnoteno].Value = rowObj.Field11;
                row.Cells[ddetailsidno].Value = rowObj.Field9;
                row.Cells[dkoidno].Value = rowObj.Field8;
                row.Cells[dtidno].Value = rowObj.Field10;
                row.Cells[dordermasteridno].Value = rowObj.Field1;
                row.Cells[ddatetimeno].Value = rowObj.Fielddatetime;
                row.Cells[dstatusno].Value = rowObj.Field12;
                row.Cells[ditemidno].Value = rowObj.Field14;
                row.Cells[dtabletypeno].Value = rowObj.Field15;
                row.Cells[dorderno].Value = rowObj.Field17;
                row.Cells[dAddonStatusno].Value = rowObj.Field19;
                row.Cells[dAVIDno].Value = rowObj.Field20;

                DataGridViewImageCell typeCell = (DataGridViewImageCell)row.Cells[dptypeno];
                if (Convert.ToInt32(rowObj.Field13) == Convert.ToInt32(Tabletype.ComboProduct))
                {
                    comboPresent = true;
                    typeCell.Value = Properties.Resources.kitchengridcomboicon;
                }
                else if (Convert.ToInt32(rowObj.Field23) >= 1)
                {
                    addonPresent = true;
                    typeCell.Value = Properties.Resources.addonbutton;
                }
                else
                {
                    typeCell.Value = (k % 2 == 0)
                        ? Properties.Resources.orderview_blank
                        : Properties.Resources.orderview_blank_2;
                }
                typeCell.ImageLayout = DataGridViewImageCellLayout.Zoom;

                int status = Convert.ToInt32(rowObj.Field12);
                DataGridViewImageCell actionCell = (DataGridViewImageCell)row.Cells[dactionno];
                Image targetActionImage;
                if (status == 0)
                {
                    // Keep cancelled rows visually stable. Reassigning the image on every
                    // refresh causes DataGridView to repaint the Action cell repeatedly,
                    // which looks like the cancel icon is vibrating.
                    targetActionImage = Properties.Resources.orderview_cancelled;
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
                    row.ReadOnly = true;
                }
                else
                {
                    targetActionImage = Properties.Resources.kitchenfood_cancel_icon;
                    if (Convert.ToInt32(rowObj.Field19) == 1 &&
                        row.DefaultCellStyle.BackColor != Color.White)
                        row.DefaultCellStyle.BackColor = Color.White;
                }

                if (!ReferenceEquals(actionCell.Value, targetActionImage))
                    actionCell.Value = targetActionImage;

                if (actionCell.ImageLayout != DataGridViewImageCellLayout.Zoom)
                    actionCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            if (dgv.Columns.Count > dptypeno)
                dgv.Columns[dptypeno].Visible = comboPresent || addonPresent;
        }


        /// <summary>
        /// Clear all cached UI and dispose controls
        /// </summary>
        private void ClearKitchenUI()
        {
            try
            {
                refreshtimer.Stop();
                timer1.Stop();

                foreach (KeyValuePair<string, OrderCardCache> kv in KitchenCardCache.ToList())
                {
                    try
                    {
                        if (kv.Value != null && kv.Value.CardPanel != null)
                        {
                            flp_order_view.Controls.Remove(kv.Value.CardPanel);
                            kv.Value.CardPanel.Dispose();
                        }
                    }
                    catch { }
                }
                KitchenCardCache.Clear();
                orderbox.Clear();

                foreach (Control ctrl in flp_order_view.Controls)
                {
                    try { ctrl.Dispose(); } catch { }
                }
                flp_order_view.Controls.Clear();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Pickuppage ClearKitchenUI()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        #endregion

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PlaySound()
        {
        //    try
        //    {
        //        soundPlayer.Play();
        //    }
        //    catch (Exception ex)
        //    {
        //        //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        clsgen.LogException("Kitchen_View Page PlaySound()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
        //    }
        }

        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtsearchData, txt_product_remove_des, txt_product_remove_des })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception)
            {

            }
        }

        private void ApplyRoundedTextBox(TextBox textBox, int radius)
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


        #endregion

        public void InitTimer()
        {
            refreshtimer.Start();
        }

        public void PrintPartyBookingKOT(DataTable ticketTable, string eventType)
        {
            if (ticketTable == null || ticketTable.Rows.Count == 0)
                return;

            if (IsDisposed || Disposing)
                throw new ObjectDisposedException("FrmKitchenView");

            if (InvokeRequired)
            {
                Invoke(
                    new Action<DataTable, string>(PrintPartyBookingKOT),
                    ticketTable.Copy(),
                    eventType);
                return;
            }

            ReportDocument partyReport = null;
            DataSet reportData = null;
            try
            {
                partyReport = new FoodandBeverage.Forms.KitchenBill.CRKitchenBill_Party();
                reportData = new DataSet();
                reportData.Tables.Add(ticketTable.Copy());
                partyReport.SetDataSource(reportData);
                partyReport.SetParameterValue("Eventtype", eventType ?? string.Empty);

                crvkitchenbill.ReportSource = partyReport;
                crvkitchenbill.RefreshReport();
                partyReport.PrintToPrinter(1, false, 0, 0);
            }
            finally
            {
                try { crvkitchenbill.ReportSource = null; } catch { }
                if (partyReport != null)
                {
                    try { partyReport.Close(); } catch { }
                    partyReport.Dispose();
                }
                if (reportData != null)
                    reportData.Dispose();
            }
        }

        private void PrintOrderAsync()
        {
            if (isOrderPrintingRunning) return;
            isOrderPrintingRunning = true;
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                BllKitchenOrderPrint<DmnGridFields> SearchDetails = new BllKitchenOrderPrint<DmnGridFields>();
                DmnGridFields dmnSearchKitchendata = new DmnGridFields();
                var gridSourcemaster = SearchDetails.serachreport("prdPrintOrder", dmnSearchKitchendata);
                if (gridSourcemaster == null || gridSourcemaster.Count == 0)
                {
                    if (this.IsHandleCreated && !this.IsDisposed)
                    {
                        Action clearPendingCount = () => btnprintcount.Text = "0";
                        if (this.InvokeRequired) this.Invoke(clearPendingCount);
                        else clearPendingCount();
                    }
                    return;
                }
                // The loading indicator is started only after pending tickets
                // are counted below. The outer refresh-timer handler owns timer state.
                var distinctOrders = gridSourcemaster
                    .GroupBy(order => new { order.Field1, order.Field2 })
                    .Select(group => group.First())
                    .OrderBy(order => order.Field1)
                    .ThenBy(order => order.Field2)
                    .ToList();

                int countpendingorder = distinctOrders.Count(
                    order => Convert.ToInt32(order.Field3) == 1);
                int remainingPendingCount = countpendingorder;

                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    Action updatePrintStatus = () =>
                    {
                        btnprintcount.Text = remainingPendingCount.ToString();

                        if (remainingPendingCount > 0)
                        {
                            if (string.IsNullOrWhiteSpace(Convert.ToString(lblprintloading.Tag)))
                                lblprintloading.Tag = "Printing.....";

                            lblprintloading.Text = Convert.ToString(lblprintloading.Tag);
                            lblprintloading.Visible = true;
                            if (!blinkTimer.Enabled)
                                blinkTimer.Start();
                        }
                        else
                        {
                            blinkTimer.Stop();
                            lblprintloading.Visible = false;
                            lblprintloading.Text = Convert.ToString(lblprintloading.Tag);
                        }
                    };

                    if (this.InvokeRequired) this.Invoke(updatePrintStatus);
                    else updatePrintStatus();
                }

                foreach (var orders in distinctOrders)
                {
                    // Only status 1 rows are waiting for printing. Do not reprint
                    // already-printed rows if the procedure returns mixed statuses.
                    if (Convert.ToInt32(orders.Field3) != 1)
                        continue;
                    // Use the same daily Pickup / Takeaway token lookup as the kitchen order card.
                    Tuple<int, string> dailyOrderToken =
                        GetDailyOrderToken(Convert.ToInt32(orders.Field1));

                    string dailyTokenDisplay =
                        dailyOrderToken != null && !string.IsNullOrWhiteSpace(dailyOrderToken.Item2)
                            ? dailyOrderToken.Item2.Trim()
                            : string.Empty;

                    #region Crystal Report Declaration
                    DataSet dsReport = new DataSet();
                    DataTable dtSalesDetail = new DataTable("DT_kitchenbill");
                    BllSearch<DmnkitchenBilldetails> bllSearchDetails = new BllSearch<DmnkitchenBilldetails>();
                    DmnSearch dmnSearchDetails = new DmnSearch();

                    //dmnSearchDetails.Select = "od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType,'' offeritemname, 0 offerqty, od.PrId PrId, ko.Kitchennumber,od.VariatiionName VariatiionName" +
                    //" from tblkitchenOrder ko " +
                    //    " Left Join tblOrderDetails od on od.OrderDetailsId=ko.OrderDetailsId " +
                    //    " Left Join tblOrderMaster om on om.OrderMasterId=ko.OrderMasterId " +
                    //    " Left Join tblTable_info ti on ti.Tid=ko.Tid " +
                    //    $" where om.status=1 and ko.OrderMasterId={orders.Field1} and ko.OrderNo={orders.Field2}" + " and od.TableType!=" + Convert.ToInt32(Tabletype.ComboProduct)+
                    //" union select od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType, ofd.Productname offeritemname, ofd.Quantity offerqty, od.PrId PrId, ko.Kitchennumber, od.VariatiionName VariatiionName ";
                    //dmnSearchDetails.Table = " tblkitchenOrder ko" +
                    //                        " Left Join tblOrderDetails od on od.OrderDetailsId = ko.OrderDetailsId" +
                    //                        " Left Join tblOrderMaster om on om.OrderMasterId = ko.OrderMasterId" +
                    //                        " Left Join tblTable_info ti on ti.Tid = ko.Tid" +
                    //                        " Left Join tblOfferMaster ofm on ofm.Omid = od.ItemId" +
                    //                        " Left Join tblOfferDetails ofd on ofd.Omid = ofm.Omid";
                    //dmnSearchDetails.WhereNdGroupBy = $" where om.status=1 and ko.OrderMasterId={orders.Field1} and ko.OrderNo={orders.Field2}" + " and od.TableType=" + Convert.ToInt32(Tabletype.ComboProduct);
                    //dmnSearchDetails.OrderBy = "order by AddonStatus";

                    dmnSearchDetails.Select = "od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType, ofd.Productname offeritemname, ofd.Quantity offerqty, od.PrId PrId, ko.Kitchennumber, od.VariatiionName VariatiionName," +
                        "( select sum(Quantity) from  tblOrderDetails  where status in(1,2) and OrderMasterId=" + orders.Field1 + " and OrderNo=" + orders.Field2 + " and AddonStatus in(0,1)) as TotalQTY,om.pkid Pkid,ISNULL(sm.OrderType,0) OrderType,'' as Station,e.EmployeeName,(" +
                        " 'PAX : ' + CAST(om.TotalGuestCount AS VARCHAR) + " +
                        " ' (A:' + CAST(om.AdultCount AS VARCHAR) + " +
                        " ' C:' + CAST(om.ChildCount AS VARCHAR) + " +
                        " ' O:' + CAST(om.OtherGuestCount AS VARCHAR) + ')'" +
                        ") AS GuestDetails,ko.PriorityType";
                    dmnSearchDetails.Table = "tblkitchenOrder ko " +
                        "Left Join tblOrderDetails od  on od.OrderDetailsId=ko.OrderDetailsId " +
                        "Left Join tblOrderMaster om  on om.OrderMasterId=ko.OrderMasterId " +
                        "Left Join tblSalesMaster sm on sm.SalesMasterId=om.SalesMasterId " +
                        "Left Join tblTable_info  ti on ti.Tid=ko.Tid" +
                        " Left Join tblOfferMaster ofm  on od.ItemId = ofm.Omid and od.TableType = 3" +
                        " Left Join tblOfferDetails ofd  on ofd.Omid = ofm.Omid" +
                        " Left Join tblEmployee e  on e.EmployeeId = om.CreatedBy";
                    dmnSearchDetails.WhereNdGroupBy = "where om.status=1 and ko.OrderMasterId=" + orders.Field1 + " and ko.OrderNo =" + orders.Field2+" and ko.PreparationMode = "+ Convert.ToInt32(KOTRouting.kitchen);
                    dmnSearchDetails.OrderBy = "order by od.AddonStatus ";

                    var BillDetails = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetails);

                    if (BillDetails == null || BillDetails.Count == 0)
                    {
                        continue;
                    }

                    var processedData = BillDetails.Select(item => new
                    {
                        ItemName = !string.IsNullOrEmpty(item.ItemName)
                                   ? item.ItemName + (!string.IsNullOrEmpty(item.VariatiionName) ? " (" + item.VariatiionName + ")" : string.Empty)
                                   : item.ItemName,
                        Quantity = item.Quantity,
                        Notes = item.Notes,
                        Description = item.Description,
                        TableCode = item.TableCode,
                        OrderMasterId = item.OrderMasterId,
                        TableType = item.TableType,
                        AddonStatus = item.AddonStatus,
                        offeritemname = item.offeritemname,
                        offerqty = item.offerqty,
                        Kitchennumber = item.Kitchennumber,
                        MainCategory = string.IsNullOrEmpty(item.AV_id) ? $"{item.ItemName}{item.OrderMasterId}{item.PrId}" : item.AV_id,
                        MainCategoryCount = BillDetails.Count(b => b.MainCategory == item.MainCategory),
                        TotalQTY = item.TotalQTY,
                        Pkid = item.Pkid,
                        EmployeeName = item.EmployeeName,
                        Station = item.Station,
                        GuestDetails = item.GuestDetails,
                        Orderno= orders.Field2,
                        PriorityType = item.PriorityType,
                        OrderType = item.OrderType,
                        TokenNo = !string.IsNullOrWhiteSpace(dailyTokenDisplay)
                            ? dailyTokenDisplay
                            : (!string.IsNullOrWhiteSpace(item.TokenNo) &&
                               item.TokenNo.Trim() != "0"
                                ? item.TokenNo.Trim()
                                : string.Empty)
                    }).ToList();

                    //if (item.VariatiionNam != null && item.VariatiionNam != "")
                    //    Description = item.Description + "(" + item.VariatiionNam + ")";
                    //else
                    //    Description = itemname;

                    var mainCategoryCounts = processedData
                        .GroupBy(item => item.MainCategory)
                        .Select(group => new
                        {
                            MainCategory = group.Key,
                            MainCategoryCount = group.Count()
                        }).ToList();


                    var mergedData = processedData
                        .Join(mainCategoryCounts,
                              processedItem => processedItem.MainCategory,
                              mainCategoryCount => mainCategoryCount.MainCategory,
                              (processedItem, mainCategoryCount) => new
                              {
                                  MainCategoryCount = mainCategoryCount.MainCategoryCount,
                                  processedItem.MainCategory,
                                  processedItem.TableType,
                                  processedItem.AddonStatus,
                                  processedItem.ItemName,
                                  processedItem.Quantity,
                                  processedItem.Notes,
                                  processedItem.Description,
                                  processedItem.TableCode,
                                  processedItem.OrderMasterId,
                                  processedItem.offeritemname,
                                  processedItem.offerqty,
                                  processedItem.Kitchennumber,
                                  processedItem.TotalQTY,
                                  processedItem.Pkid,
                                  processedItem.EmployeeName,
                                  processedItem.Station,
                                  processedItem.GuestDetails,
                                  processedItem.Orderno,
                                  processedItem.PriorityType,
                                  processedItem.TokenNo,
                                  processedItem.OrderType,

                              })
                        .ToList();

                    if (mergedData != null && mergedData.Any())
                    {
                        using (var reader = ObjectReader.Create(mergedData, "ItemName", "Quantity", "Notes", "Description", "TableCode", "OrderMasterId",
                            "MainCategory", "TableType", "AddonStatus", "MainCategoryCount", "offeritemname", "offerqty", "Kitchennumber", "TotalQTY",
                            "Pkid", "EmployeeName", "Station", "GuestDetails", "Orderno", "PriorityType", "TokenNo", "OrderType"))
                        {
                            dtSalesDetail.Load(reader);
                        }
                        dsReport.Tables.Add(dtSalesDetail);
                    }

                    // Pickup bills use Pkid > 0. Takeaway orders use
                    // tblSalesMaster.OrderType = 4 and share the same Pickup RPT.
                    bool isPickupOrTakeaway =
                        Convert.ToInt32(BillDetails[0].Pkid) > 0 ||
                        Convert.ToInt32(BillDetails[0].OrderType) == 4 ||
                        (dailyOrderToken != null && dailyOrderToken.Item1 == 2);

                    string reportPath = isPickupOrTakeaway
                        ? RPTPath + "CRkitchenbill_Pickup.rpt"
                        : RPTPath + "crkitchenbill.rpt";

                    if (!File.Exists(reportPath))
                        throw new FileNotFoundException("Report file not found.", reportPath);

                    Action printTicket = () =>
                    {
                        ReportDocument ticketReport = null;
                        try
                        {
                            ticketReport = new ReportDocument();
                            ticketReport.Load(reportPath);
                            ticketReport.SetDataSource(dsReport);
                            ticketReport.PrintToPrinter(1, false, 0, 0);
                        }
                        finally
                        {
                            if (ticketReport != null)
                            {
                                try { ticketReport.Close(); } catch { }
                                ticketReport.Dispose();
                            }
                        }
                    };

                    if (!this.IsHandleCreated || this.IsDisposed)
                        throw new ObjectDisposedException("FrmKitchenView");

                    if (this.InvokeRequired)
                        this.Invoke(printTicket);
                    else
                        printTicket();
                    #endregion

                    #region Update Print Status to 2
                    BllKitchenorder bllkitchen = new BllKitchenorder();
                    Dmnkitchen dmnkitchen = new Dmnkitchen
                    {
                        OrderMasterId = Convert.ToInt32(orders.Field1),
                        OrderNo = Convert.ToInt32(orders.Field2),
                        CreatedBy = FrmDashboard.UserId,
                        KOTPrintStatus = 2,
                        BranchId = FrmDashboard.BranchId
                    };
                    var InsertStatus = bllkitchen.Kitchen_statusupdate("prd_updateKitchenorderstatus", dmnkitchen);
                    #endregion

                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            PlaySound();
                            remainingPendingCount = Math.Max(0, remainingPendingCount - 1);
                            btnprintcount.Text = remainingPendingCount.ToString();
                        }));
                    }

                }

            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                        lblprintloading.Text = "Printing Stopped";
                        clsgen.LogException("Kitchen_View Page PrintOrderAsync()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
                    }));
                }
            }
            finally
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    Action finishPrintUi = () =>
                    {
                        blinkTimer.Stop();
                        lblprintloading.Visible = false;
                        lblprintloading.Text = Convert.ToString(lblprintloading.Tag);
                    };

                    try
                    {
                        if (this.InvokeRequired) this.Invoke(finishPrintUi);
                        else finishPrintUi();
                    }
                    catch (ObjectDisposedException) { }
                    catch (InvalidOperationException) { }
                }

                isOrderPrintingRunning = false;
                GC.WaitForPendingFinalizers();
            }
        }

        private void PrintcancelOrder()
        {
            if (isCancelPrintingRunning) return;
            isCancelPrintingRunning = true;

            ReportDocument reportDocument = null;
            DataSet dsReport = new DataSet();
            DataTable DT_cancel = new DataTable("DT_CancelOrder");

            try
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() => refreshtimer.Stop()));
                }

                // Fetch Cancel Orders
                BllCancelsale<DmnOrderCancel> SearchDetails = new BllCancelsale<DmnOrderCancel>();
                DmnOrderCancel dmncancel = new DmnOrderCancel();
                var gridSourcemaster = SearchDetails.serachreport("prdPrintcancelOrder", dmncancel);

                if (gridSourcemaster == null || gridSourcemaster.Count == 0)
                {
                   
                    return;
                }
                int omid = gridSourcemaster[0].OrderMasterId;
                int pkidno = gridSourcemaster[0].Pkid;
                using (var reader = ObjectReader.Create(gridSourcemaster,
                    "SalesMasterId",
                    "OrderMasterId",
                    "CancelPrintStatus",
                    "CancelDescription",
                    "TableCode",
                    "Pkid",
                    "Station",
                    "EmployeeName",
                    "Tid",
                    "OrderType"))
                {
                    DT_cancel.Load(reader);
                }

                dsReport.Tables.Add(DT_cancel);
                reportDocument = new ReportDocument();
                string reportPath = RPTPath + "CRCancelPrint.rpt";

                if (!File.Exists(reportPath))
                {
                    CMessageBox.Show(this, "Report file not found: " + reportPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new FileNotFoundException("Report file not found.", reportPath);
                }
                reportDocument.Load(reportPath);
                reportDocument.SetDataSource(dsReport);

                // LOOP AND PRINT ONLY FOR RECORDS WITH CancelPrintStatus = 1
                foreach (var orders in gridSourcemaster)
                {
                    if (orders.CancelPrintStatus == 1)
                    {
                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() => blinkTimer.Start()));
                        }

                        reportDocument.PrintToPrinter(1, false, 0, 0);
                        BllKitchenorder bllkitchen = new BllKitchenorder();
                        DmnOrderCancel dmncnl = new DmnOrderCancel
                        {
                            Pkid = pkidno,
                            OrderMasterId = omid,
                            SalesMasterId = Convert.ToInt32(orders.SalesMasterId),
                            CreatedBy = FrmDashboard.UserId,
                            BranchId = FrmDashboard.BranchId
                        };

                        bllkitchen.Cancel_statusupdate("prd_updateCancelprintstatus", dmncnl);

                      
                    }
                }

                if (this.IsHandleCreated)
                    this.Invoke(new Action(() => blinkTimer.Stop()));
            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                        lblprintloading.Text = "Printing Stopped";
                        CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clsgen.LogException("Kitchen_View Page PrintOrderAsync()", ex,
                            FrmLogin.EmployeName, FrmLogin.Designation,
                            Convert.ToInt32(FormType.Kitchen_View));
                    }));
                }
            }
            finally
            {
                if (reportDocument != null)
                {
                    reportDocument.Close();
                    reportDocument.Dispose();
                }
                GC.WaitForPendingFinalizers();
                isCancelPrintingRunning = false;
            }
        }
        private void Printcancelitems()
        {
            if (isCancelPrintingRunning) return;
            isCancelPrintingRunning = true;

            ReportDocument reportDocument = null;
            DataSet dsReport = new DataSet();
            DataTable DT_cancelitems = new DataTable("DT_CancelItems");

            try
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() => refreshtimer.Stop()));
                }

                Bllcancelitemprint<DmnCancelItemPrint> SearchDetails = new Bllcancelitemprint<DmnCancelItemPrint>();
                DmnCancelItemPrint dmncancel = new DmnCancelItemPrint();
                var gridSourcemaster = SearchDetails.serachreport("prdCancelItemPrint", dmncancel);

                if (gridSourcemaster == null || gridSourcemaster.Count == 0)
                {
                    return;
                }

                using (var reader = ObjectReader.Create(gridSourcemaster,
                    "OrderMasterId",
                    "Tid",
                    "TableCode",
                    "KOTNo",
                    "OrderNo",
                    "Quantity",
                    "RemoveNotes",
                    "CreatedDateTime",
                    "EmployeeName",
                    "itemName"))
                {
                    DT_cancelitems.Load(reader);
                }

                dsReport.Tables.Add(DT_cancelitems);
                reportDocument = new ReportDocument();
                string reportPath = RPTPath + "CRcancelitems.rpt";

                if (!File.Exists(reportPath))
                {
                    CMessageBox.Show(this, "Report file not found: " + reportPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new FileNotFoundException("Report file not found.", reportPath);
                }

                reportDocument.Load(reportPath);
                reportDocument.SetDataSource(dsReport);

                foreach (var item in gridSourcemaster)
                {
                    if (item.IsItemCancelPrintPending)
                    {
                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() => blinkTimer.Start()));
                        }

                        reportDocument.PrintToPrinter(1, false, 0, 0);

                        BllKitchenorder bllkitchen = new BllKitchenorder();
                        DmnCancelItemPrint dmncnl = new DmnCancelItemPrint
                        {
                            OrderDetailId = item.OrderDetailId,
                            //CreatedBy = FrmDashboard.UserId,
                            //BranchId = FrmDashboard.BranchId
                        };
                        bllkitchen.Cancelitemprint_statusupdate("prd_updateCancelprintstatus_item", dmncnl);
                    }
                }

                if (this.IsHandleCreated)
                    this.Invoke(new Action(() => blinkTimer.Stop()));
            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                        lblprintloading.Text = "Printing Stopped";
                        CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clsgen.LogException("Kitchen_View Page PrintCancelItems()", ex,
                            FrmLogin.EmployeName, FrmLogin.Designation,
                            Convert.ToInt32(FormType.Kitchen_View));
                    }));
                }
            }
            finally
            {
                if (reportDocument != null)
                {
                    reportDocument.Close();
                    reportDocument.Dispose();
                }
                isCancelPrintingRunning = false;
            }
        }


        private void dgv_product_details_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dactionno && e.RowIndex >= 0)// Remove Prodcut one by one by clciking column 
                {
                    refreshtimer.Stop();
                    DataGridView clickedDataGridView = sender as DataGridView;
                    if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value) == 1)//  Food processing Not started
                    {
                        korderno = 0;
                        tlp_main.Enabled = false;
                        txt_product_remove_des.Text = "Reason type here .....";
                        txt_product_remove_des.ForeColor = Color.LightGray;
                        dgv_view_name = clickedDataGridView.Name;
                        dgv_view_rowno = e.RowIndex;
                        pnl_products_rem_descr.Visible = true;
                        int panelWidthPercentage = 25;
                        int panelheightPercentage = 25;
                        int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                        int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                        pnl_products_rem_descr.Size = new Size(panelWidth, panelHeight);
                        pnl_products_rem_descr.Location = new Point(this.ClientSize.Width / 2 - pnl_products_rem_descr.Size.Width / 2, this.ClientSize.Height / 2 - pnl_products_rem_descr.Size.Height / 2);
                        tlp_products_descr_main.Dock = DockStyle.Fill;
                        lblheader_product_des.Text = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[ditemno].Value);
                        korderno=Convert.ToInt32(clickedDataGridView.Tag);
                        //txt_product_remove_des.Focus();
                    }
                    else if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value) == 2)//  Food processing started
                    {
                        var result = CMessageBox.Show("Food processing has started. Are you sure you want to cancel this item?", "Confirm The Action", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            tlp_main.Enabled = false;
                            txt_product_remove_des.Text = "Reason type here .....";
                            txt_product_remove_des.ForeColor = Color.LightGray;
                            dgv_view_name = clickedDataGridView.Name;
                            dgv_view_rowno = e.RowIndex;
                            pnl_products_rem_descr.Visible = true;
                            int panelWidthPercentage = 20;
                            int panelheightPercentage = 20;
                            int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                            int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                            pnl_products_rem_descr.Size = new Size(panelWidth, panelHeight);
                            pnl_products_rem_descr.Location = new Point(this.ClientSize.Width / 2 - pnl_products_rem_descr.Size.Width / 2, this.ClientSize.Height / 2 - pnl_products_rem_descr.Size.Height / 2);
                            lblheader_product_des.Text = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[ditemno].Value);
                        }

                    }
                }
                else if (e.ColumnIndex == dptypeno && e.RowIndex >= 0)//Addon or Combo product View more panel show
                {
                    if (pnlproductviewmore.Visible)
                    {
                        tlp_main.Enabled = true;
                        pnlproductviewmore.Visible = false;
                    }
                    else
                    {

                        DataGridView clickedDataGridView = sender as DataGridView;
                        if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value) == 1 && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dtabletypeno].Value) == Convert.ToInt32(Tabletype.ComboProduct))//cmbo products
                        {
                            lblviewmore.Text = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[ditemno].Value);
                            int itemid = Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[ditemidno].Value);
                            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                            BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                            DmnSearch dmnSearch = new DmnSearch();
                            dmnSearch.Select = "ofd.Productname Field1,ofd.Quantity Field2";
                            dmnSearch.Table = " tblOfferDetails ofd LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid";
                            dmnSearch.WhereNdGroupBy = "where ofm.Status=1 and ofd.Status=1 and ofm.Omid = " + itemid;
                            dmnSearch.OrderBy = "";
                            var gridSource_offer = bllSearch.GetSearchData("prdSearch", dmnSearch);
                            if (gridSource_offer.Count > 0)
                            {
                                tlp_main.Enabled = false;
                                dgv_viewmore.Rows.Clear();
                                dgv_viewmore.Columns[addactionno].Visible = false;
                                for (int k = 0; k < gridSource_offer.Count; k++)
                                {
                                    dgv_viewmore.Rows.Add();
                                    dgv_viewmore.Rows[k].Cells[addslno].Value = k + 1;
                                    dgv_viewmore.Rows[k].Cells[additemnameno].Value = gridSource_offer[k].Field1;
                                    dgv_viewmore.Rows[k].Cells[addqtyno].Value = gridSource_offer[k].Field2;
                                }
                                // pnlproductviewmore.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                                pnlproductviewmore.Location = new Point(this.ClientSize.Width / 2 - pnlproductviewmore.Size.Width / 2, this.ClientSize.Height / 2 - pnlproductviewmore.Size.Height / 2);
                                pnlproductviewmore.Visible = true;
                            }

                        }
                        else if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value) == 1 && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dAddonStatusno].Value) == Convert.ToInt32(AddonType.AddonType))//addon products
                        {
                            lblviewmore.Text = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[ditemno].Value);
                            string itemid = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[dAVIDno].Value);
                            int ordermasid = Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dordermasteridno].Value);
                            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                            BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                            DmnSearch dmnSearch = new DmnSearch();
                            dmnSearch.Select = "itemName Field1,Quantity Field2,od.AddonStatus Field3,ko.OrderMasterId Field4,ko.Koid Field5,od.OrderDetailsId Field6,od.AV_id Field7,od.Pavid Field8,pav.Price Field9,od.OrderNo Field10,pav.CCode Field11";
                            dmnSearch.Table = "tblOrderDetails od left join tblkitchenOrder ko on  ko.OrderDetailsId=od.OrderDetailsId " +
                                "left join tblProduct_Addon_Variation pav on  pav.Pavid=od.Pavid and pav.status=1";
                            dmnSearch.WhereNdGroupBy = "where od.Status!=0 and od.OrderMasterId= " + ordermasid + " and  od.AV_id = '" + itemid + "'";
                            dmnSearch.OrderBy = "order by od.Pavid";
                            var gridSource_offer = bllSearch.GetSearchData("prdSearch", dmnSearch);
                            if (gridSource_offer.Count > 1)
                            {
                                tlp_main.Enabled = false;
                                dgv_viewmore.Rows.Clear();
                                dgv_viewmore.Columns[addactionno].Visible = true;
                                int c = 0;
                                for (int k = 0; k < gridSource_offer.Count; k++)
                                {
                                    if (Convert.ToInt32(gridSource_offer[k].Field3) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                                    {
                                        int rowIndex = dgv_viewmore.Rows.Add();
                                        dgv_viewmore.Rows[rowIndex].Cells[addslno].Value = c + 1;
                                        dgv_viewmore.Rows[rowIndex].Cells[additemnameno].Value = gridSource_offer[k].Field1;
                                        dgv_viewmore.Rows[rowIndex].Cells[addqtyno].Value = gridSource_offer[k].Field2;
                                        dgv_viewmore.Rows[rowIndex].Cells[addordermasteridno].Value = gridSource_offer[k].Field4;
                                        dgv_viewmore.Rows[rowIndex].Cells[addkoidno].Value = gridSource_offer[k].Field5;
                                        dgv_viewmore.Rows[rowIndex].Cells[addorderdetailidno].Value = gridSource_offer[k].Field6;
                                        dgv_viewmore.Rows[rowIndex].Cells[addavidno].Value = gridSource_offer[k].Field7;
                                        dgv_viewmore.Rows[rowIndex].Cells[addpavidno].Value = gridSource_offer[k].Field8;
                                        dgv_viewmore.Rows[rowIndex].Cells[addaddonstatusno].Value = gridSource_offer[k].Field3;
                                        dgv_viewmore.Rows[rowIndex].Cells[addonpriceno].Value = gridSource_offer[k].Field9;
                                        dgv_viewmore.Rows[rowIndex].Cells[addonorderno].Value = gridSource_offer[k].Field10;
                                        dgv_viewmore.Rows[rowIndex].Cells[addccodeno].Value = gridSource_offer[k].Field11;
                                        c = c + 1;
                                    }
                                }
                                // pnlproductviewmore.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                                pnlproductviewmore.Location = new Point(this.ClientSize.Width / 2 - pnlproductviewmore.Size.Width / 2, this.ClientSize.Height / 2 - pnlproductviewmore.Size.Height / 2);
                                pnlproductviewmore.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View dgv_product_details_CellClick", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }

        }
        public void ReprintBill()
        {
            ReportDocument reportDocument = new ReportDocument();
            try
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Start();
                        refreshtimer.Stop();
                    }));
                }

                dgv_view_name = "dgv_product_details_k_" + ReprintOrderNo + ReprintId;
                DataGridView dgv_product_details = this.Controls.Find(dgv_view_name, true).FirstOrDefault() as DataGridView;
                if (dgv_product_details != null)
                {
                    int orderno = 0, ordermid = 0;
                    for (int i = 0; i < dgv_product_details.RowCount; i++)
                    {
                        if (Convert.ToInt32(dgv_product_details.Rows[i].Cells[dstatusno].Value) == 1 || Convert.ToInt32(dgv_product_details.Rows[i].Cells[dstatusno].Value) == 2)
                        {
                            orderno = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dorderno].Value);
                            ordermid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dordermasteridno].Value);
                            break;
                        }
                    }
                    #region Crystal Report Dclaration
                    DataSet dsReport = new DataSet();
                    DataTable dtSalesDetail = new DataTable("DT_kitchenbill");
                    BllSearch<DmnkitchenBilldetails> bllSearchDetails = new BllSearch<DmnkitchenBilldetails>();
                    DmnSearch dmnSearchDetails = new DmnSearch();
                    dmnSearchDetails.Select = "od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType,'' offeritemname, 0 offerqty, od.PrId PrId, ko.Kitchennumber,od.VariatiionName VariatiionName,'' as Station,e.EmployeeName," +
                        "( select sum(Quantity) from  tblOrderDetails  where status in(1,2) and OrderMasterId=" + ordermid + " and OrderNo=" + orderno + " and AddonStatus in(0,1)) as TotalQTY,om.pkid Pkid,ISNULL(sm.OrderType,0) OrderType,'' as Station,e.EmployeeName,(" +
                        " 'PAX : ' + CAST(om.TotalGuestCount AS VARCHAR) + " +
                        " ' (A:' + CAST(om.AdultCount AS VARCHAR) + " +
                        " ' C:' + CAST(om.ChildCount AS VARCHAR) + " +
                        " ' O:' + CAST(om.OtherGuestCount AS VARCHAR) + ')'" +
                        ") AS GuestDetails,ko.PriorityType " +

                    " from tblkitchenOrder ko " +
                        " Left Join tblOrderDetails od on od.OrderDetailsId=ko.OrderDetailsId " +
                        " Left Join tblOrderMaster om on om.OrderMasterId=ko.OrderMasterId " +
                        " Left Join tblSalesMaster sm on sm.SalesMasterId=om.SalesMasterId " +
                        " Left Join tblTable_info ti on ti.Tid=ko.Tid " +
                        " Left Join tblEmployee e  on e.EmployeeId = om.CreatedBy " +
                    " where om.status=1 and ko.OrderMasterId=" + ordermid + " and ko.OrderNo=" + orderno + " and od.TableType!=" + Convert.ToInt32(Tabletype.ComboProduct) + " and ko.status in(1,2)" +
                    " union select od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType, ofd.Productname offeritemname, ofd.Quantity offerqty, od.PrId PrId, ko.Kitchennumber, od.VariatiionName VariatiionName,'' as Station,e.EmployeeName," +
                         "( select sum(Quantity) from  tblOrderDetails  where status in(1,2) and OrderMasterId=" + ordermid + " and OrderNo=" + orderno + " and AddonStatus in(0,1)) as TotalQTY,om.pkid Pkid,ISNULL(sm.OrderType,0) OrderType,'' as Station,e.EmployeeName,(" +
                        " 'PAX : ' + CAST(om.TotalGuestCount AS VARCHAR) + " +
                        " ' (A:' + CAST(om.AdultCount AS VARCHAR) + " +
                        " ' C:' + CAST(om.ChildCount AS VARCHAR) + " +
                        " ' O:' + CAST(om.OtherGuestCount AS VARCHAR) + ')'" +
                        ") AS GuestDetails,ko.PriorityType ";
                    dmnSearchDetails.Table = " tblkitchenOrder ko" +
                                            " Left Join tblOrderDetails od on od.OrderDetailsId = ko.OrderDetailsId" +
                                            " Left Join tblOrderMaster om on om.OrderMasterId = ko.OrderMasterId" +
                                            " Left Join tblSalesMaster sm on sm.SalesMasterId = om.SalesMasterId" +
                                            " Left Join tblTable_info ti on ti.Tid = ko.Tid" +
                                            " Left Join tblOfferMaster ofm on ofm.Omid = od.ItemId" +
                                            " Left Join tblOfferDetails ofd on ofd.Omid = ofm.Omid"+
                                            " Left Join tblEmployee e  on e.EmployeeId = om.CreatedBy";
                    dmnSearchDetails.WhereNdGroupBy = $" where om.status=1 and ko.OrderMasterId=" + ordermid + " and ko.OrderNo=" + orderno + " and od.TableType=" + Convert.ToInt32(Tabletype.ComboProduct) + " and ko.status in(1,2) and ko.PreparationMode = "+ Convert.ToInt32(KOTRouting.kitchen);
                    dmnSearchDetails.OrderBy = "order by AddonStatus";
                    var BillDetails = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetails);
                    if (BillDetails.Count > 0)
                    {
                        var processedData = BillDetails.Select(item => new
                        {
                            ItemName = !string.IsNullOrEmpty(item.ItemName)
                                  ? item.ItemName + (!string.IsNullOrEmpty(item.VariatiionName) ? " (" + item.VariatiionName + ")" : string.Empty)
                                  : item.ItemName,
                            Quantity = item.Quantity,
                            Notes = item.Notes,
                            Description = item.Description,
                            TableCode = item.TableCode,
                            OrderMasterId = item.OrderMasterId,
                            TableType = item.TableType,
                            AddonStatus = item.AddonStatus,
                            offeritemname = item.offeritemname,
                            offerqty = item.offerqty,
                            Kitchennumber = item.Kitchennumber,
                            MainCategory = string.IsNullOrEmpty(item.AV_id) ? $"{item.ItemName}{item.OrderMasterId}{item.PrId}" : item.AV_id,
                            MainCategoryCount = BillDetails.Count(b => b.MainCategory == item.MainCategory),
                            TotalQTY = item.TotalQTY,
                            Pkid = item.Pkid,
                            EmployeeName = item.EmployeeName,
                            Station = item.Station,
                            GuestDetails = item.GuestDetails,
                            Orderno = orderno,
                            PriorityType = item.PriorityType,
                            OrderType = item.OrderType,
                        }).ToList();

                        //if (item.VariatiionNam != null && item.VariatiionNam != "")
                        //    Description = item.Description + "(" + item.VariatiionNam + ")";
                        //else
                        //    Description = itemname;

                        var mainCategoryCounts = processedData
                            .GroupBy(item => item.MainCategory)
                            .Select(group => new
                            {
                                MainCategory = group.Key,
                                MainCategoryCount = group.Count()
                            }).ToList();


                        var mergedData = processedData
                            .Join(mainCategoryCounts,
                                  processedItem => processedItem.MainCategory,
                                  mainCategoryCount => mainCategoryCount.MainCategory,
                                  (processedItem, mainCategoryCount) => new
                                  {
                                      MainCategoryCount = mainCategoryCount.MainCategoryCount,
                                      processedItem.MainCategory,
                                      processedItem.TableType,
                                      processedItem.AddonStatus,
                                      processedItem.ItemName,
                                      processedItem.Quantity,
                                      processedItem.Notes,
                                      processedItem.Description,
                                      processedItem.TableCode,
                                      processedItem.OrderMasterId,
                                      processedItem.offeritemname,
                                      processedItem.offerqty,
                                      processedItem.Kitchennumber,
                                      processedItem.TotalQTY,
                                      processedItem.Pkid,
                                      processedItem.EmployeeName,
                                      processedItem.Station,
                                      processedItem.GuestDetails,
                                      processedItem.Orderno,
                                      processedItem.PriorityType,
                                      processedItem.OrderType,

                                  })
                            .ToList();

                        if (mergedData != null && mergedData.Count > 0)
                        {
                            using (var reader = ObjectReader.Create(mergedData, "ItemName", "Quantity", "Notes", "Description", "TableCode", "OrderMasterId", "MainCategory", "TableType", "AddonStatus", "MainCategoryCount", "offeritemname", "offerqty", "Kitchennumber", "TotalQTY", "Pkid", "EmployeeName", "Station", "GuestDetails", "Orderno", "PriorityType", "OrderType"))
                            {
                                dtSalesDetail.Load(reader);
                            }
                            dsReport.Tables.Add(dtSalesDetail);
                        }

                        //MessageBox.Show(RPTPath + "crkitchenbill.rpt.rpt" + 2);
                        //textBox2.Text = RPTPath + "CRkitchenbill_Pickup.rpt";
                        // Pickup (OrderType 1) and Takeaway (OrderType 4) share the
                        // Pickup kitchen report. Keep Pkid and daily-token fallbacks for
                        // older orders where SalesMaster.OrderType may be null or zero.
                        Tuple<int, string> dailyOrderToken = GetDailyOrderToken(ordermid);
                        int salesOrderType = Convert.ToInt32(BillDetails[0].OrderType);
                        int dailyOrderTypeId = dailyOrderToken == null ? 0 : dailyOrderToken.Item1;
                        bool isPickupOrTakeaway =
                            Convert.ToInt32(BillDetails[0].Pkid) > 0 ||
                            salesOrderType == 1 ||
                            salesOrderType == 4 ||
                            dailyOrderTypeId == 1 ||
                            dailyOrderTypeId == 2;

                        string reportPath = isPickupOrTakeaway
                            ? RPTPath + "CRkitchenbill_Pickup.rpt"
                            : RPTPath + "crkitchenbill.rpt";

                        if (!File.Exists(reportPath))
                        {
                            throw new FileNotFoundException("Report file not found.", reportPath);
                        }
                        reportDocument.Load(reportPath);

                        reportDocument.SetDataSource(dsReport);

                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() =>
                            {
                                crvkitchenbill.ReportSource = reportDocument;
                            }));
                        }
                        //reportDocument.SetParameterValue("TotalQtyMain", mainCategoryCounts.Count);
                        reportDocument.PrintToPrinter(1, false, 0, 0);
                    }
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            refreshtimer.Start();
                        }));
                    }

                }
                #endregion

                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                    }));
                }
            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                        lblprintloading.Text = "Reprinting Stopped";

                        //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clsgen.LogException("Kitchen_View ReprintBill", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
                    }));
                }

            }
            finally
            {
                reportDocument.Close();
                reportDocument.Dispose();
                GC.WaitForPendingFinalizers();
            }
        }
        private void reprintbutton_Click(object sender, EventArgs e)
        {
            ReprintOrderNo = 0;
            ReprintId = 0;
            PictureBox clickedPictureBox = (PictureBox)sender;
            ReprintOrderNo = Convert.ToInt32(clickedPictureBox.AccessibleDescription);
            ReprintId = Convert.ToInt32(clickedPictureBox.Tag);
            RunMethodInSeparateThread(ReprintBill);

        }
        private void pbcancelfood_Click(object sender, EventArgs e)
        {
            try
            {
                tlp_main.Enabled = false;
                refreshtimer.Stop();
                PictureBox clickedPictureBox = (PictureBox)sender;
                int id = Convert.ToInt32(clickedPictureBox.Tag);
                dgv_view_name = "dgv_product_details_k_" + clickedPictureBox.AccessibleDescription + id;
                pnl_products_rem_descr.Visible = true;
                int panelWidthPercentage = 20;
                int panelheightPercentage = 20;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                pnl_products_rem_descr.Size = new Size(panelWidth, panelHeight);
                pnl_products_rem_descr.Location = new Point(this.ClientSize.Width / 2 - pnl_products_rem_descr.Size.Width / 2, this.ClientSize.Height / 2 - pnl_products_rem_descr.Size.Height / 2);
                pbcancelfoodclciked = true;
                lblheader_product_des.Text = "Order Cancel";
                //txt_product_remove_des.Focus();
                pnl_products_rem_descr.Focus();

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View pbcancelfood_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }

        }
        private void pbstart_Click(object sender, EventArgs e)
        {
            try
            {
                refreshtimer.Stop();
                PictureBox clicked = sender as PictureBox;
                if (clicked == null) return;

                string gridName = "dgv_product_details_k_" + clicked.AccessibleDescription + Convert.ToString(clicked.Tag);
                DataGridView grid = this.Controls.Find(gridName, true).FirstOrDefault() as DataGridView;
                if (grid == null) return;

                // Start every pending line in this ticket. Updating only row zero leaves
                // mixed status values, causing Start/Ready to alternate on refresh.
                bool updated = UpdateKitchenOrderRows(grid, 1, 2);
                FillOrders();

                if (!updated)
                    System.Diagnostics.Debug.WriteLine("Kitchen Start: no pending rows were updated.");

                if (Application.OpenForms["FrmOrderView"] is FrmOrderView orderView)
                    orderView.FillOrders();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View pbstart_Click", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
            finally
            {
                if (!IsDisposed && !refreshtimer.Enabled)
                    refreshtimer.Start();
            }
        }

        private void pbreadyfood_Click(object sender, EventArgs e)
        {
            try
            {
                refreshtimer.Stop();
                PictureBox clicked = sender as PictureBox;
                if (clicked == null) return;

                string gridName = "dgv_product_details_k_" + clicked.AccessibleDescription + Convert.ToString(clicked.Tag);
                DataGridView grid = this.Controls.Find(gridName, true).FirstOrDefault() as DataGridView;
                if (grid == null) return;

                bool updated;
                if (IsKitchenKOTOnlyMode)
                {
                    // In KOT-only mode, OK is the only completion action.
                    // Complete both pending (1) and started (2) lines together.
                    updated = CompleteKitchenOrderRows(grid);
                    if (!updated)
                        System.Diagnostics.Debug.WriteLine(
                            "Kitchen OK: no active order rows were completed.");
                }
                else
                {
                    // Keep the normal kitchen workflow unchanged.
                    updated = UpdateKitchenOrderRows(grid, 2, 3);
                    if (!updated)
                        System.Diagnostics.Debug.WriteLine(
                            "Kitchen Ready: no in-progress rows were updated.");
                }

                FillOrders();

                if (Application.OpenForms["FrmOrderView"] is FrmOrderView orderView)
                    orderView.FillOrders();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View pbreadyfood_Click", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
            finally
            {
                if (!IsDisposed && !refreshtimer.Enabled)
                    refreshtimer.Start();
            }
        }

        private bool CompleteKitchenOrderRows(DataGridView grid)
        {
            if (grid == null || grid.IsDisposed || grid.Rows.Count == 0)
                return false;

            BllOrder orderService = new BllOrder();
            HashSet<string> processedRows =
                new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            bool foundActiveRows = false;
            bool allUpdatesSucceeded = true;

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                DataGridViewRow row = grid.Rows[i];
                if (row.IsNewRow || row.Cells[dstatusno].Value == null)
                    continue;

                int currentStatus;
                if (!int.TryParse(Convert.ToString(row.Cells[dstatusno].Value), out currentStatus) ||
                    (currentStatus != 1 && currentStatus != 2))
                    continue;

                int orderDetailsId;
                int koid;
                int orderMasterId;
                int orderNo;
                int addonStatus;

                if (!int.TryParse(Convert.ToString(row.Cells[ddetailsidno].Value), out orderDetailsId) ||
                    !int.TryParse(Convert.ToString(row.Cells[dkoidno].Value), out koid) ||
                    !int.TryParse(Convert.ToString(row.Cells[dordermasteridno].Value), out orderMasterId) ||
                    !int.TryParse(Convert.ToString(row.Cells[dorderno].Value), out orderNo) ||
                    orderDetailsId <= 0 || koid <= 0)
                    continue;

                int.TryParse(Convert.ToString(row.Cells[dAddonStatusno].Value), out addonStatus);
                string avid = Convert.ToString(row.Cells[dAVIDno].Value) ?? string.Empty;
                string rowKey = koid + "|" + addonStatus + "|" + avid;
                if (!processedRows.Add(rowKey))
                    continue;

                foundActiveRows = true;
                DmnOrderMaster update = new DmnOrderMaster
                {
                    OrderDetailsId = orderDetailsId,
                    koid = koid,
                    OrderMasterId = orderMasterId,
                    OrderNo = orderNo,
                    AddonStatus = addonStatus,
                    AV_id = avid,
                    CreatedBy = FrmDashboard.UserId,
                    BranchId = FrmDashboard.BranchId,
                    Status = 3
                };

                try
                {
                    DmnOrderMaster result = orderService.Remove_order_KitchenOrder_Start_Rady(
                        "prd_KitchenOrder_Updates", update);
                    if (result == null || result.Status != 1)
                        allUpdatesSucceeded = false;
                }
                catch (Exception ex)
                {
                    allUpdatesSucceeded = false;
                    clsgen.LogException("Kitchen_View CompleteKitchenOrderRows", ex,
                        FrmLogin.EmployeName, FrmLogin.Designation,
                        Convert.ToInt32(FormType.Kitchen_View));
                }
            }

            return foundActiveRows && allUpdatesSucceeded;
        }

        private bool UpdateKitchenOrderRows(DataGridView grid, int expectedStatus, int newStatus)
        {
            if (grid == null || grid.IsDisposed || grid.Rows.Count == 0)
                return false;

            BllOrder orderService = new BllOrder();
            HashSet<string> processedRows = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            bool foundEligibleRow = false;
            bool allUpdatesSucceeded = true;

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                DataGridViewRow row = grid.Rows[i];
                if (row.IsNewRow || row.Cells[dstatusno].Value == null)
                    continue;

                int currentStatus;
                if (!int.TryParse(Convert.ToString(row.Cells[dstatusno].Value), out currentStatus) ||
                    currentStatus != expectedStatus)
                    continue;

                string koid = Convert.ToString(row.Cells[dkoidno].Value);
                string addonStatus = Convert.ToString(row.Cells[dAddonStatusno].Value);
                string avid = Convert.ToString(row.Cells[dAVIDno].Value);
                string rowKey = koid + "|" + addonStatus + "|" + avid;
                if (!processedRows.Add(rowKey))
                    continue;

                foundEligibleRow = true;

                DmnOrderMaster update = new DmnOrderMaster
                {
                    OrderDetailsId = Convert.ToInt32(row.Cells[ddetailsidno].Value),
                    koid = Convert.ToInt32(row.Cells[dkoidno].Value),
                    OrderMasterId = Convert.ToInt32(row.Cells[dordermasteridno].Value),
                    AddonStatus = Convert.ToInt32(row.Cells[dAddonStatusno].Value),
                    AV_id = Convert.ToString(row.Cells[dAVIDno].Value),
                    OrderNo = Convert.ToInt32(row.Cells[dorderno].Value),
                    CreatedBy = FrmDashboard.UserId,
                    BranchId = FrmDashboard.BranchId,
                    Status = newStatus
                };

                try
                {
                    var result = orderService.Remove_order_KitchenOrder_Start_Rady(
                        "prd_KitchenOrder_Updates", update);
                    if (result == null || result.Status != 1)
                        allUpdatesSucceeded = false;
                }
                catch (Exception ex)
                {
                    allUpdatesSucceeded = false;
                    clsgen.LogException("Kitchen_View UpdateKitchenOrderRows", ex,
                        FrmLogin.EmployeName, FrmLogin.Designation,
                        Convert.ToInt32(FormType.Kitchen_View));
                }
            }

            return foundEligibleRow && allUpdatesSucceeded;
        }

        private void txt_product_remove_des_Enter(object sender, EventArgs e)
        {
            if (txt_product_remove_des.Text == "Reason type here .....")
            {
                txt_product_remove_des.Text = "";
            }
            txt_product_remove_des.ForeColor = Color.Black;
        }
        private void txt_product_remove_des_Leave(object sender, EventArgs e)
        {
            if (txt_product_remove_des.Text == "")
            {
                txt_product_remove_des.Text = "Reason type here .....";

            }
            txt_product_remove_des.ForeColor = Color.DarkGray;
        }
        private void btn_cancel_product_des_Click(object sender, EventArgs e)
        {
            tlp_main.Enabled = true;
            pnl_products_rem_descr.Visible = false;
            txt_product_remove_des.Text = "Reason type here .....";
            refreshtimer.Start();
        }
        private void btn_close_prod_des_Click(object sender, EventArgs e)
        {
            tlp_main.Enabled = true;
            pnl_products_rem_descr.Visible = false;
            txt_product_remove_des.Text = "Reason type here .....";
            refreshtimer.Start();
        }
        public bool MandatoryCheck()
        {
            //if (string.IsNullOrEmpty(txtordermasterid.Text))
            //    txtordermasterid.Text = "0";

            if (txt_product_remove_des.Text == "Reason type here ....." || txt_product_remove_des.Text == "")
            {
                CMessageBox.Show("Please Enter Reason", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_product_remove_des.Focus();
                return false;
            }

            return true;
        }
        private void btn_rem_product_des_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {
                    DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
                    BllOrder bll_order = new BllOrder();
                    int tid = 0,addonrowindex=0;
                    if (pbcancelfoodclciked)
                    {
                        DataGridView dgv_product_details = this.Controls.Find(dgv_view_name, true).FirstOrDefault() as DataGridView;
                        if (dgv_product_details != null)
                        {
                            int omid = 0, orderno = 0;
                            for (int i = 0; i < dgv_product_details.RowCount; i++)
                            {
                                if (Convert.ToInt32(dgv_product_details.Rows[i].Cells[dstatusno].Value) == 1)
                                {
                                    omid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dordermasteridno].Value);
                                    tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                                    orderno = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dorderno].Value);
                                    dmnRemove_order.OrderDetailsId = Convert.ToInt32(dgv_product_details.Rows[i].Cells[ddetailsidno].Value);
                                    dmnRemove_order.koid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dkoidno].Value);
                                    dmnRemove_order.OrderMasterId = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dordermasteridno].Value);
                                    dmnRemove_order.Tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                                    decimal originalValue = Convert.ToDecimal(dgv_product_details.Rows[i].Cells[dqtyno].Value);

                                    dmnRemove_order.AddonStatus = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dAddonStatusno].Value);
                                    dmnRemove_order.AV_id = Convert.ToString(dgv_product_details.Rows[i].Cells[dAVIDno].Value);

                                    dmnRemove_order.Quantity = (int)Math.Floor(originalValue);
                                    dmnRemove_order.Status = 0;
                                    dmnRemove_order.CancelNotifyStatus = 1;
                                    dmnRemove_order.OrderCancelNotes = txt_product_remove_des.Text;
                                    dmnRemove_order.Sourcepage = Convert.ToInt32(CancelSourcePage.Kitchen);
                                    var InsertStatusP = bll_order.Remove_order_ALL("prd_remove_KitchenOrder", dmnRemove_order);
                                }
                            }

                            tlp_main.Enabled = true;
                            pbcancelfoodclciked = false;
                            pnl_products_rem_descr.Visible = false;
                            txt_product_remove_des.Text = "";

                            string tlpname = "tlp_product_box_k_" + omid + orderno;
                            TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;

                            string dgvName = "dgv_product_details_k_" + omid + orderno;
                            string startButtonName = "pb_start_k_" + omid + orderno;
                            string readyButtonName = "pb_ready_k_" + omid + orderno;
                            string cancelFoodButtonName = "pictureBoxRemove_k_" + omid + orderno;
                            string lblTimeName = "lbl_time_K_" + omid + orderno;
                            string lblTableCodeName = "lbl_tablecod_k_" + omid + orderno;

                            Label lblTime = this.Controls.Find(lblTimeName, true).FirstOrDefault() as Label;
                            Label lblTableCode = this.Controls.Find(lblTableCodeName, true).FirstOrDefault() as Label;
                            PictureBox btnReady = this.Controls.Find(readyButtonName, true).FirstOrDefault() as PictureBox;
                            PictureBox btnStart = this.Controls.Find(startButtonName, true).FirstOrDefault() as PictureBox;
                            PictureBox btnCancelFood = this.Controls.Find(cancelFoodButtonName, true).FirstOrDefault() as PictureBox;


                            if (btnReady != null && btnStart != null && btnCancelFood != null && lblTime != null && lblTableCode != null)
                            {
                                btnReady.Dispose();
                                btnStart.Dispose();
                                btnCancelFood.Dispose();
                                lblTime?.Dispose();
                                lblTableCode?.Dispose();
                            }

                            if (tlp_sele != null && flp_order_view.Controls.Contains(tlp_sele))
                            {
                                flp_order_view.Controls.Remove(tlp_sele);
                                tlp_sele.Dispose();
                                dgv_product_details.Dispose();
                            }
                        }
                        refreshtimer.Start();

                    }
                    else
                    {
                        DataGridView dgv_product_details = this.Controls.Find(dgv_view_name, true).FirstOrDefault() as DataGridView;
                        if (Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dAddonStatusno].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                        {
                            #region Load data to Grid
                            string itemid = Convert.ToString(dgv_product_details.Rows[dgv_view_rowno].Cells[dAVIDno].Value);
                            int ordermasid = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dordermasteridno].Value);
                            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                            BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                            DmnSearch dmnSearch = new DmnSearch();
                            dmnSearch.Select = "itemName Field1,Quantity Field2,od.AddonStatus Field3,ko.OrderMasterId Field4,ko.Koid Field5,od.OrderDetailsId Field6,od.AV_id Field7,od.Pavid Field8,pav.Price Field9,od.OrderNo Field10,pav.CCode Field11";
                            dmnSearch.Table = "tblOrderDetails od left join tblkitchenOrder ko on  ko.OrderDetailsId=od.OrderDetailsId " +
                                "left join tblProduct_Addon_Variation pav on  pav.Pavid=od.Pavid and pav.status=1";
                            dmnSearch.WhereNdGroupBy = "where od.Status!=0 and od.OrderMasterId= " + ordermasid + " and  od.AV_id = '" + itemid + "'";
                            dmnSearch.OrderBy = "order by od.Pavid";
                            var gridSource_offer = bllSearch.GetSearchData("prdSearch", dmnSearch);
                            if (gridSource_offer.Count > 1)
                            {
                                dgv_viewmore.Rows.Clear();
                                dgv_viewmore.Columns[addactionno].Visible = true;
                                int c = 0;
                                for (int k = 0; k < gridSource_offer.Count; k++)
                                {
                                    if (Convert.ToInt32(gridSource_offer[k].Field3) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                                    {
                                        int rowIndex = dgv_viewmore.Rows.Add();
                                        dgv_viewmore.Rows[rowIndex].Cells[addslno].Value = c + 1;
                                        dgv_viewmore.Rows[rowIndex].Cells[additemnameno].Value = gridSource_offer[k].Field1;
                                        dgv_viewmore.Rows[rowIndex].Cells[addqtyno].Value = gridSource_offer[k].Field2;
                                        dgv_viewmore.Rows[rowIndex].Cells[addordermasteridno].Value = gridSource_offer[k].Field4;
                                        dgv_viewmore.Rows[rowIndex].Cells[addkoidno].Value = gridSource_offer[k].Field5;
                                        dgv_viewmore.Rows[rowIndex].Cells[addorderdetailidno].Value = gridSource_offer[k].Field6;
                                        dgv_viewmore.Rows[rowIndex].Cells[addavidno].Value = gridSource_offer[k].Field7;
                                        dgv_viewmore.Rows[rowIndex].Cells[addpavidno].Value = gridSource_offer[k].Field8;
                                        dgv_viewmore.Rows[rowIndex].Cells[addaddonstatusno].Value = gridSource_offer[k].Field3;
                                        dgv_viewmore.Rows[rowIndex].Cells[addonpriceno].Value = gridSource_offer[k].Field9;
                                        dgv_viewmore.Rows[rowIndex].Cells[addonorderno].Value = gridSource_offer[k].Field10;
                                        dgv_viewmore.Rows[rowIndex].Cells[addccodeno].Value = gridSource_offer[k].Field11;
                                        c = c + 1;
                                    }
                                }
                                // pnlproductviewmore.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                                pnlproductviewmore.Location = new Point(this.ClientSize.Width / 2 - pnlproductviewmore.Size.Width / 2, this.ClientSize.Height / 2 - pnlproductviewmore.Size.Height / 2);
                                pnlproductviewmore.Visible = true;
                            }
                            #endregion

                            #region Remove Addon
                            DmnKitchenaddonremove dmnRemove_addon = new DmnKitchenaddonremove();
                            BllKitchenorder bll_rem_addon = new BllKitchenorder();
                            decimal newprice = 0, discountprice = 0, TaxAmount = 0, finalprice = 0, disAmnt = 0;
                            string newpart = "";
                            //var result = CMessageBox.Show("Are you sure you want to cancel this Addon?", "Confirm The Action", MessageBoxButtons.YesNo);
                            //if (result == DialogResult.Yes)
                            //{
                                refreshtimer.Stop();

                            #region To Reset Addon ID(remove id form avid name)
                            for (int i = 0; i < dgv_viewmore.RowCount; i++)
                            {
                                if (Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addaddonstatusno].Value) == 2 && Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addkoidno].Value) == Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dkoidno].Value))
                                {
                                    addonrowindex = i;
                                    break;
                                }
                            }

                                string avidValue = Convert.ToString(dgv_product_details.Rows[dgv_view_rowno].Cells[dAVIDno].Value);
                                string[] avidParts = avidValue.Split('_');
                                string extractedpid = $"{avidParts[0]}";
                                newpart = newpart + extractedpid + "_";
                                int remove_koid = Convert.ToInt32(dgv_viewmore.Rows[addonrowindex].Cells[addkoidno].Value);
                                for (int i = 0; i < dgv_viewmore.RowCount; i++)
                                {
                                    if (Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addaddonstatusno].Value) == 2 && Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addkoidno].Value) != remove_koid)
                                    {
                                        string ccode = Convert.ToString(dgv_viewmore.Rows[i].Cells[addccodeno].Value);
                                        decimal quantityDecimal = Convert.ToDecimal(dgv_viewmore.Rows[i].Cells[addqtyno].Value);
                                        int quantityInt = (int)Math.Floor(quantityDecimal);
                                        newpart = newpart + ccode + quantityInt.ToString();
                                    }
                                }

                                for (int i = 0; i < dgv_viewmore.RowCount; i++)
                                {
                                    if (Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addaddonstatusno].Value) == 2 && Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addkoidno].Value) != remove_koid)
                                    {
                                        dgv_viewmore.Rows[i].Cells[addavidno].Value = newpart;
                                    }
                                }

                                #endregion

                                #region Price Reset main product after remove addon

                                dmnSearch.Select = "DiscountPer Field1,TaxType Field2,TaxPer Field3,Tax Field4,srate Field5";
                                dmnSearch.Table = "tblOrderDetails";
                                dmnSearch.WhereNdGroupBy = "where OrderMasterId = " + Convert.ToInt32(dgv_viewmore.Rows[addonrowindex].Cells[addordermasteridno].Value) + " and OrderNo = " + Convert.ToInt32(dgv_viewmore.Rows[addonrowindex].Cells[addonorderno].Value) + " and AddonStatus = 1 and AV_id = '" + avidValue + "'";
                                dmnSearch.OrderBy = "";
                                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                                if (gridSource.Count > 0)
                                {
                                    newprice = Convert.ToDecimal(gridSource[0].Field5) - (Convert.ToDecimal(dgv_viewmore.Rows[addonrowindex].Cells[addqtyno].Value) * Convert.ToDecimal(dgv_viewmore.Rows[addonrowindex].Cells[addonpriceno].Value));
                                    disAmnt = (Convert.ToDecimal(gridSource[0].Field1) * newprice) / 100;

                                    discountprice = newprice - disAmnt;
                                    if (gridSource[0].Field2 == "GST")
                                        TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].Field3) * discountprice / 100);
                                    else if (gridSource[0].Field2 == "VAT")
                                        TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].Field3) * discountprice / 100);

                                    if (Convert.ToString(gridSource[0].Field4) == "Exclusive")
                                        finalprice = (newprice - disAmnt) + TaxAmount;
                                    else if (Convert.ToString(gridSource[0].Field4) == "Inclusive")
                                        finalprice = (newprice - disAmnt);

                                    #endregion

                                    dmnRemove_addon.Amount = newprice;
                                    dmnRemove_addon.DiscountAmount = disAmnt;
                                    if (gridSource[0].Field2 == "GST")
                                    {
                                        dmnRemove_addon.IGSTAmount = TaxAmount;
                                        dmnRemove_addon.CGSTAmount = TaxAmount / 2;
                                        dmnRemove_addon.SGSTAmount = TaxAmount / 2;
                                        dmnRemove_addon.VATAmount = 0;
                                    }
                                    else if (gridSource[0].Field2 == "VAT")
                                    {
                                        dmnRemove_addon.VATAmount = TaxAmount;
                                        dmnRemove_addon.IGSTAmount = 0;
                                        dmnRemove_addon.CGSTAmount = 0;
                                        dmnRemove_addon.SGSTAmount = 0;
                                    }
                                //}
                                dmnRemove_addon.TaxAmount = TaxAmount;
                                dmnRemove_addon.NetAmount = finalprice;
                                dmnRemove_addon.Srate = newprice;
                                dmnRemove_addon.MarketPrice = finalprice;
                                dmnRemove_addon.OrderNo = Convert.ToInt32(dgv_viewmore.Rows[addonrowindex].Cells[addonorderno].Value);

                                dmnRemove_addon.OrderDetailsId = Convert.ToInt32(dgv_viewmore.Rows[addonrowindex].Cells[addorderdetailidno].Value);
                                dmnRemove_addon.koid = Convert.ToInt32(dgv_viewmore.Rows[addonrowindex].Cells[addkoidno].Value);
                                dmnRemove_addon.OrderMasterId = Convert.ToInt32(dgv_viewmore.Rows[addonrowindex].Cells[addordermasteridno].Value);
                                dmnRemove_addon.AVid = Convert.ToString(dgv_viewmore.Rows[addonrowindex].Cells[addavidno].Value);
                                dmnRemove_addon.Status = 0;
                                dmnRemove_addon.CreatedBy = FrmDashboard.UserId;
                                dmnRemove_addon.Update_AVid = newpart;
                                var InsertStatusaddon = bll_rem_addon.Kitchenremove_addon("prd_remove_KitchenOrder_addon", dmnRemove_addon);

                                dgv_viewmore.Rows.RemoveAt(addonrowindex);

                                if (dgv_viewmore.RowCount == 0)
                                {
                                    pnlproductviewmore.Visible = false;
                                    tlp_main.Enabled = true;
                                }
                                else
                                {
                                    for (int i = 0; i < dgv_viewmore.RowCount; i++)
                                        dgv_viewmore.Rows[i].Cells[addslno].Value = i + 1;
                                }

                            }
                            #endregion
                        }
                        tid = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dtidno].Value);
                        dmnRemove_order.OrderDetailsId = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[ddetailsidno].Value);
                        dmnRemove_order.koid = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dkoidno].Value);
                        dmnRemove_order.OrderMasterId = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dordermasteridno].Value);
                        dmnRemove_order.Tid = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dtidno].Value);
                        decimal originalValue = Convert.ToDecimal(dgv_product_details.Rows[dgv_view_rowno].Cells[dqtyno].Value);

                        dmnRemove_order.AddonStatus = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dAddonStatusno].Value);
                        dmnRemove_order.AV_id = Convert.ToString(dgv_product_details.Rows[dgv_view_rowno].Cells[dAVIDno].Value);

                        dmnRemove_order.Quantity = (int)Math.Floor(originalValue);
                        dmnRemove_order.Status = 0;
                        dmnRemove_order.RemoveNotes = txt_product_remove_des.Text;
                        dmnRemove_order.CancelNotifyStatus = 1;
                        dmnRemove_order.KitchenCancelStatus = 1;
                        dmnRemove_order.Sourcepage = Convert.ToInt32(CancelSourcePage.Kitchen);
                        var InsertStatusP = bll_order.Remove_order_KitchenOrder_ByProduct("prd_remove_Order_kitchen", dmnRemove_order);

                        string startbuttonname = "pb_start_k_" + korderno;
                        string readybuttonname = "pb_ready_k_" + korderno;
                        string cancelfoodbuttonname = "pictureBoxRemove_k_" + korderno;
                        string rprint = "reprintbutton_k_" + korderno;
                        string descrip = "txt_Description_k_" + korderno;
                        string tlpname = "tlp_product_box_k_" +korderno;
                        TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel; 
                        PictureBox btn_ready = this.Controls.Find(readybuttonname, true).FirstOrDefault() as PictureBox;
                        PictureBox btn_start = this.Controls.Find(startbuttonname, true).FirstOrDefault() as PictureBox;
                        PictureBox btn_cancelfood = this.Controls.Find(cancelfoodbuttonname, true).FirstOrDefault() as PictureBox;
                        TextBox txtdes = this.Controls.Find(descrip, true).FirstOrDefault() as TextBox;

                        PictureBox pb_rprint = this.Controls.Find(rprint, true).FirstOrDefault() as PictureBox;
                        if (btn_ready != null)
                            btn_ready.Visible = false;
                        if (btn_start != null)
                            btn_start.Visible = false;
                        if (btn_cancelfood != null)
                            btn_cancelfood.Visible = false;
                        if (pb_rprint != null)
                            pb_rprint.Visible = false;
                        if (txtdes != null)
                        {
                            txtdes.Visible = true;
                            txtdes.Text = "Waiting for customer confirmation.";
                        }

                        if(tlp_sele!=null)
                            tlp_sele.RowStyles[2] = new RowStyle(SizeType.AutoSize);

                        DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[dgv_view_rowno].Cells[dactionno];
                        cell.Value = Properties.Resources.orderview_cancelled;
                        DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_product_details.Rows[dgv_view_rowno].Cells[dactionno];
                        dgv_product_details.Rows[dgv_view_rowno].Cells[dremnoteno].Value = txt_product_remove_des.Text;
                        pnl_products_rem_descr.Visible = false;
                        tlp_main.Enabled = true;
                        txt_product_remove_des.Text = "";
                        refreshtimer.Start();
                    }
                    if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                    {
                        FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                        _instanceFrmOrderView.FillOrders();
                    }

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View btn_rem_product_des_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        public void GridSlno_Refresh()
        {
            /*
            try
            {
                if (Dgv_Selected_Item.RowCount == 0)
                    return;

                int mainNo = 1;
                int subNo = 1;

                for (int j = 0; j < Dgv_Selected_Item.RowCount; j++)
                {
                    var itemId = Dgv_Selected_Item.Rows[j].Cells[iqty].Value;
                    if (itemId == null || Convert.ToInt32(itemId) <= 0)//|| Convert.ToInt32(Dgv_Selected_Item.Rows[j].Cells[iMainProductStatusno].Value)== (int)MainProductStatus.Secondary
                        continue;
             int mainNo = 1;
                int subNo = 1;
                    int prodStatus = Convert.ToInt32(Dgv_Selected_Item.Rows[j].Cells[iMainProductStatusno].Value);

                    // MAIN PRODUCT
                    if (prodStatus == (int)MainProductStatus.Primary)
                    {
                        Dgv_Selected_Item.Rows[j].Cells[islno].Value = mainNo;
                        Dgv_Selected_Item.Rows[j].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        mainNo++;
                        subNo = 1;
                    }
                    // SUB PRODUCT
                    else if (prodStatus == (int)MainProductStatus.Secondary)
                    {
                        Dgv_Selected_Item.Rows[j].Cells[islno].Value = subNo;
                        Dgv_Selected_Item.Rows[j].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        subNo++;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page GridSlno_Refresh()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
            */
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;
                foreach (OrderCardCache cache in KitchenCardCache.Values)
                {
                    if (cache == null || cache.TimeLabel == null || cache.TimeLabel.IsDisposed)
                        continue;

                    string timeText = FormatKitchenOrderTime(cache.CreatedTime, now);
                    if (!string.Equals(cache.TimeLabel.Text, timeText, StringComparison.Ordinal))
                        cache.TimeLabel.Text = timeText;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View timer1_Tick", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        private string GetPlaceholder()
        {
            switch (cmbserachtype.SelectedIndex)
            {
                case 0:
                    return "Enter Kitchen Number...";
                case 1:
                    return "Enter Table Code...";
                case 2:
                    return "Enter Daily Token No...";
                default:
                    return "Enter Value...";
            }
        }
        private void txtorderid_search_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtsearchData.Text))
            {
                txtsearchData.Text = GetPlaceholder();
                txtsearchData.ForeColor = Color.DarkGray;
            }
        }
        private void txtorderid_search_Enter(object sender, EventArgs e)
        {
            string placeholder = GetPlaceholder();

            if (txtsearchData.Text == placeholder)
            {
                txtsearchData.Text = "";
                txtsearchData.ForeColor = Color.Black;
            }
        }
        private void txtorderid_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                return;
            }

            // Daily tokens include a prefix (P005 / T005), and table codes can be alphanumeric.
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
                e.Handled = true;
        }
        private void txtorderid_search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnsearch.PerformClick();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View txtorderid_search_KeyDown", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            try
            {
                refreshtimer.Stop();
                RefreshKitchenKOTOnlySetting();
                cmbserachtype.SelectedIndex = 0;
                KitchenDisplayCount = FrmLogin.KitchenDisplayCount;
                //btn_viewmore_order.Visible = true;
                // Empty state is controlled only by SetEmptyIcon after the refresh resolves.

                txtsearchData.Text = GetPlaceholder();
                txtsearchData.ForeColor = Color.DarkGray;
                ClearKitchenCards();
                refreshtimer.Start();
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Kitchen_View btn_refresh_Click",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Kitchen_View)
                );
            }
        }
        private void ClearKitchenCards()
        {          
            flp_order_view.SuspendLayout();
            foreach (Control ctrl in flp_order_view.Controls)
            {
                ctrl.Dispose();
            }

            flp_order_view.Controls.Clear();
            flp_order_view.ResumeLayout(true);
            KitchenCardCache.Clear();

            // Do not leave the previous no-orders panel visible while a manual refresh
            // is fetching new data. SetEmptyIcon will decide after results are loaded.
            emptyStateVisibilityInitialized = false;
        }


        private void pnl_products_rem_descr_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                float borderRadius = 10f; // adjust this value to change the roundness of the corners
                GraphicsPath path = new GraphicsPath();
                RectangleF rect = new RectangleF(pnl_products_rem_descr.ClientRectangle.X, pnl_products_rem_descr.ClientRectangle.Y, pnl_products_rem_descr.ClientRectangle.Width, pnl_products_rem_descr.ClientRectangle.Height);
                path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
                path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
                path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
                path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
                path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
                path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
                path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
                path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
                path.CloseFigure();
                pnl_products_rem_descr.Region = new Region(path);
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View pnl_products_rem_descr_Paint", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    if (pnl_products_rem_descr.Visible)
                    {
                        tlp_main.Enabled = true;
                        pnl_products_rem_descr.Visible = false;
                        refreshtimer.Start();
                    }
                    else if (pnlproductviewmore.Visible)
                    {
                        tlp_main.Enabled = true;
                        pnlproductviewmore.Visible = false;
                    }
                    else
                    {
                        FrmDashboard _instancedash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                        _instancedash.Closeform();
                    }
                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        private void btn_close_viewmore_Click(object sender, EventArgs e)
        {
            tlp_main.Enabled = true;
            pnlproductviewmore.Visible = false;
        }
        private void FrmKitchenView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (ReferenceEquals(ActiveInstance, this))
                    ActiveInstance = null;

                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    AppSession.UnregisterForm(this);
                }
                refreshtimer.Stop();
                this.Dispose();
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View FrmKitchenView_FormClosing", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        private void flp_order_view_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                flp_order_view.SuspendLayout();
                flp_order_view.ResumeLayout();
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View flp_order_view_Scroll", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        private void txtdisplaycount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If the pressed key is not a digit and not a control key (like backspace), suppress it
                e.Handled = true;
            }
        }
        private void btn_viewmore_order_Click(object sender, EventArgs e)
        {
            KitchenDisplayCount = 25;
            btn_viewmore_order.Visible = false;
        }
        private void blinkTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                lblprintloading.Text = Convert.ToString(lblprintloading.Tag);
                lblprintloading.Visible = !lblprintloading.Visible;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View blinkTimer_Tick", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        private void btnprintcount_Click(object sender, EventArgs e)
        {
            if (refreshtimer.Enabled)
            {
                refreshtimer.Start();
            }
        }

        //private void refreshtimer_Tick(object sender, EventArgs e)
        //{
        //    RunMethodInSeparateThread(PrintcancelOrder);
        //    FillOrdersAsync();
        //    if (Convert.ToInt32(btnprintcount.Text) > 0)
        //        RunMethodInSeparateThread(PrintOrderAsync);
        //    else
        //    {
        //        this.Invoke(new Action(() =>
        //        {
        //            lblprintloading.Visible = false;
        //        }));
        //    }

        //}

        private void refreshtimer_Tick(object sender, EventArgs e)
        {
            refreshtimer.Stop();
            try
            {
                // Party booking Kitchen KOTs are pulled by the Kitchen PC and
                // printed on this workstation's default kitchen printer.
                PartyBookingKOTQueuePrinter.ProcessPendingAsync(
                    FrmDashboard.BranchId,
                    Convert.ToInt32(KOTRouting.kitchen),
                    FrmDashboard.UserId);

                // Print pending orders/cancellations first. Refresh cards and both
                // counters afterward so the displayed totals reflect committed print
                // status changes rather than the pre-print snapshot.
                PrintOrderAsync();
                PrintcancelOrder();
                Printcancelitems();
                FillOrders();
            }
            finally
            {
                if (!IsDisposed && !refreshtimer.Enabled)
                    refreshtimer.Start();
            }
        }

        private static void RunMethodInSeparateThread(Action action)
        {
            var thread = new Thread(new ThreadStart(action));
            thread.Start();
        }

        private void dgv_viewmore_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == addactionno && e.RowIndex>=0)
                {
                    DmnKitchenaddonremove dmnRemove_addon = new DmnKitchenaddonremove();
                    BllKitchenorder bll_rem_addon = new BllKitchenorder();
                    decimal newprice = 0, discountprice = 0, TaxAmount = 0, finalprice = 0, disAmnt = 0;
                    string newpart = "";
                    var result = CMessageBox.Show("Are you sure you want to cancel this Addon?", "Confirm The Action", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        refreshtimer.Stop();

                        #region To Reset Addon ID(remove id form avid name)

                        string avidValue = Convert.ToString(dgv_viewmore.Rows[e.RowIndex].Cells[addavidno].Value);
                        string[] avidParts = avidValue.Split('_');
                        string extractedpid = $"{avidParts[0]}";
                        newpart = newpart + extractedpid + "_";
                        int remove_koid = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addkoidno].Value);
                        for (int i = 0; i < dgv_viewmore.RowCount; i++)
                        {
                            if (Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addaddonstatusno].Value) == 2 && Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addkoidno].Value) != remove_koid)
                            {
                                string ccode = Convert.ToString(dgv_viewmore.Rows[i].Cells[addccodeno].Value);
                                decimal quantityDecimal = Convert.ToDecimal(dgv_viewmore.Rows[i].Cells[addqtyno].Value);
                                int quantityInt = (int)Math.Floor(quantityDecimal);
                                newpart = newpart + ccode + quantityInt.ToString();
                            }
                        }

                        for (int i = 0; i < dgv_viewmore.RowCount; i++)
                        {
                            if (Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addaddonstatusno].Value) == 2 && Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addkoidno].Value) != remove_koid)
                            {
                                dgv_viewmore.Rows[i].Cells[addavidno].Value = newpart;
                            }
                        }

                        #endregion

                        #region Price Reset main product after remove addon
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "DiscountPer Field1,TaxType Field2,TaxPer Field3,Tax Field4,srate Field5";
                        dmnSearch.Table = "tblOrderDetails";
                        dmnSearch.WhereNdGroupBy = "where OrderMasterId = " + Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addordermasteridno].Value) + " and OrderNo = " + Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addonorderno].Value) + " and AddonStatus = 1 and AV_id = '" + avidValue + "'";
                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            newprice = Convert.ToDecimal(gridSource[0].Field5) - Convert.ToDecimal(dgv_viewmore.Rows[e.RowIndex].Cells[addonpriceno].Value);
                            disAmnt = (Convert.ToDecimal(gridSource[0].Field1) * newprice) / 100;

                            discountprice = newprice - disAmnt;
                            if (gridSource[0].Field2 == "GST")
                                TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].Field3) * discountprice / 100);
                            else if (gridSource[0].Field2 == "VAT")
                                TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].Field3) * discountprice / 100);

                            if (Convert.ToString(gridSource[0].Field4) == "Exclusive")
                                finalprice = (newprice - disAmnt) + TaxAmount;
                            else if (Convert.ToString(gridSource[0].Field4) == "Inclusive")
                                finalprice = (newprice - disAmnt);

                            #endregion

                            dmnRemove_addon.Amount = newprice;
                            dmnRemove_addon.DiscountAmount = disAmnt;
                            if (gridSource[0].Field2 == "GST")
                            {
                                dmnRemove_addon.IGSTAmount = TaxAmount;
                                dmnRemove_addon.CGSTAmount = TaxAmount / 2;
                                dmnRemove_addon.SGSTAmount = TaxAmount / 2;
                                dmnRemove_addon.VATAmount = 0;
                            }
                            else if (gridSource[0].Field2 == "VAT")
                            {
                                dmnRemove_addon.VATAmount = TaxAmount;
                                dmnRemove_addon.IGSTAmount = 0;
                                dmnRemove_addon.CGSTAmount = 0;
                                dmnRemove_addon.SGSTAmount = 0;
                            }
                        }
                        dmnRemove_addon.TaxAmount = TaxAmount;
                        dmnRemove_addon.NetAmount = finalprice;
                        dmnRemove_addon.Srate = newprice;
                        dmnRemove_addon.MarketPrice = finalprice;
                        dmnRemove_addon.OrderNo = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addonorderno].Value);

                        dmnRemove_addon.OrderDetailsId = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addorderdetailidno].Value);
                        dmnRemove_addon.koid = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addkoidno].Value);
                        dmnRemove_addon.OrderMasterId = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addordermasteridno].Value);
                        dmnRemove_addon.AVid = Convert.ToString(dgv_viewmore.Rows[e.RowIndex].Cells[addavidno].Value);
                        dmnRemove_addon.Status = 0;
                        dmnRemove_addon.CreatedBy = FrmDashboard.UserId;
                        dmnRemove_addon.Update_AVid = newpart;
                        var InsertStatusaddon = bll_rem_addon.Kitchenremove_addon("prd_remove_KitchenOrder_addon", dmnRemove_addon);

                        dgv_viewmore.Rows.RemoveAt(e.RowIndex);
                        if(dgv_viewmore.RowCount==0)
                        {
                            pnlproductviewmore.Visible = false;
                            tlp_main.Enabled = true;
                        }
                        else
                        {
                            for (int i = 0; i < dgv_viewmore.RowCount; i++)
                                dgv_viewmore.Rows[i].Cells[addslno].Value = i + 1;
                        }
                        refreshtimer.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View dgv_viewmore_CellDoubleClick", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }

        }

        private void pnl_kitchenorderBill_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void pnl_kitchenorderBill_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point p = PointToScreen(e.Location);
                pnl_kitchenorderBill.Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }

        private void pnl_kitchenorderBill_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void tm_memory_Tick(object sender, EventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            long memoryInBytes = currentProcess.PrivateMemorySize64;
            double memoryInMB = memoryInBytes / (1024.0 * 1024.0);
            lblmemory.Text = $"Memory: {memoryInMB:F2} MB";
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            // Mandatory textbox check
            if (string.IsNullOrWhiteSpace(txtsearchData.Text) ||
                string.Equals(txtsearchData.Text.Trim(), GetPlaceholder(), StringComparison.OrdinalIgnoreCase))
            {
                CMessageBox.Show("Please enter search text.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtsearchData.Focus();
                return;
            }

            // Mandatory combobox check
            if (cmbserachtype.SelectedIndex == -1)
            {
                CMessageBox.Show("Please select search type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbserachtype.DroppedDown = true;
                return;
            }

            string searchText = txtsearchData.Text.Trim();
            int stype = cmbserachtype.SelectedIndex;
            if (stype == 0)
                SerachTable_Pickup(searchText, Convert.ToInt32(KitchenSearchType.ByKitchenNumber));
            else if (stype == 1)
                SerachTable_Pickup(searchText, Convert.ToInt32(KitchenSearchType.ByTableCode));
            else if (stype == 2)
                SerachTable_Pickup(searchText, Convert.ToInt32(KitchenSearchType.ByDailyToken));

        }
        public void SerachTable_Pickup(string sdata, int serachtype)
        {
            try
            {
                refreshtimer.Stop();
                txt_product_remove_des.Text = "Reason type here .....";

                BllKitchenData<DmnGridFields> searchDetails = new BllKitchenData<DmnGridFields>();
                DmnGridFields searchKitchenData = new DmnGridFields
                {
                    Searchtype = serachtype,
                    Field30 = sdata
                };

                var gridSourceMaster = searchDetails.serachreport("prdKitchenData", searchKitchenData);
                if (gridSourceMaster == null || gridSourceMaster.Count == 0)
                {
                    btntotalorders.Text = "0";
                    btnprintcount.Text = "0";
                    ClearKitchenUI();
                    SetEmptyIcon();
                    return;
                }

                var distinctOrders = gridSourceMaster
                    .GroupBy(order => new { order.Field1, order.Field2 })
                    .Select(group => group.First())
                    .OrderBy(order => order.Field1)
                    .ThenBy(order => order.Field2)
                    .ToList();

                btntotalorders.Text = Convert.ToString(distinctOrders.Count);
                btnprintcount.Text = Convert.ToString(
                    distinctOrders.Count(order => Convert.ToInt32(order.Field3) == 1));

                HashSet<string> visibleKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                BllkitchenDataDetails<Dmnkitchendetails> detailsService =
                    new BllkitchenDataDetails<Dmnkitchendetails>();

                foreach (var order in distinctOrders)
                {
                    Dmnkitchendetails query = new Dmnkitchendetails
                    {
                        OrderNo = Convert.ToInt32(order.Field2),
                        OrderMasterId = Convert.ToInt32(order.Field1)
                    };

                    List<Dmnkitchendetails> details =
                        detailsService.serachData("prdKitchendetails", query);
                    if (details == null || details.Count == 0)
                        continue;

                    string key = Convert.ToString(order.Field1) + "_" +
                        Convert.ToString(details[0].Field17);
                    visibleKeys.Add(key);

                    OrderCardCache cache;
                    if (!KitchenCardCache.TryGetValue(key, out cache) ||
                        cache == null || cache.CardPanel == null || cache.CardPanel.IsDisposed)
                    {
                        cache = BuildOrderCard(order, details);
                        if (cache != null && cache.CardPanel != null)
                        {
                            KitchenCardCache[key] = cache;
                            flp_order_view.Controls.Add(cache.CardPanel);
                        }
                    }
                    else
                    {
                        UpdateOrderCard(cache, order, details);
                    }
                }

                foreach (string oldKey in KitchenCardCache.Keys.ToList())
                {
                    if (visibleKeys.Contains(oldKey))
                        continue;

                    OrderCardCache oldCache = KitchenCardCache[oldKey];
                    if (oldCache != null && oldCache.CardPanel != null)
                    {
                        flp_order_view.Controls.Remove(oldCache.CardPanel);
                        if (oldCache.Grid != null)
                        {
                            orderbox.RemoveAll(x => string.Equals(
                                x.datagridviewname, oldCache.Grid.Name,
                                StringComparison.OrdinalIgnoreCase));
                        }
                        oldCache.CardPanel.Dispose();
                    }
                    KitchenCardCache.Remove(oldKey);
                }

                flp_order_view_ResponsiveResize(flp_order_view, EventArgs.Empty);
                SetEmptyIcon();
                // Keep periodic all-orders refresh paused while the search result is displayed.
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View Page SerachTable_Pickup()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        private void cmbserachtype_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtsearchData.Text = GetPlaceholder();
            txtsearchData.ForeColor = Color.DarkGray;
        }
        public void SetEmptyIcon()
        {
            try
            {
                // Cache membership is stable during parent hide/show events; checking
                // Control.Visible here can flicker when the form is minimized or repainted.
                bool hasOrders = KitchenCardCache.Values.Any(cache =>
                    cache != null && cache.CardPanel != null &&
                    !cache.CardPanel.IsDisposed &&
                    flp_order_view.Controls.Contains(cache.CardPanel));
                bool showEmptyState = !hasOrders;

                if (showEmptyState)
                {
                    int x = Math.Max(0, (ClientSize.Width - pnl_empty_msg.Width) / 2);
                    int y = Math.Max(0, (ClientSize.Height - pnl_empty_msg.Height) / 2);
                    Point location = new Point(x, y);

                    if (pnl_empty_msg.Location != location)
                        pnl_empty_msg.Location = location;

                    pnl_empty_msg.Anchor = AnchorStyles.None;
                }

                // Track the requested state separately from Control.Visible, which can
                // report false while the form or a parent is temporarily hidden/minimized.
                if (emptyStateVisibilityInitialized &&
                    emptyStateShouldBeVisible == showEmptyState)
                    return;

                pnl_empty_msg.Visible = showEmptyState;
                emptyStateShouldBeVisible = showEmptyState;
                emptyStateVisibilityInitialized = true;

                if (showEmptyState)
                    pnl_empty_msg.BringToFront();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View Page SetEmptyIcon()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Kitchen_View));
            }
        }

    }
}
