using BLL;
using CustomMessageBox;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FoodandBeverage.Interfaces;
using System.Threading;
using FoodandBeverage.Classes;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;

namespace FoodandBeverage.Forms
{
    public partial class FrmTableView : Form
    {

        #region Declaration
        public List<DmnNameList> CheckedtableNames = new List<DmnNameList>();

        public int panelsPerRow = 0;
        public int tabletype = 0;
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        List<DmnTableinfo> TableDataList = new List<DmnTableinfo>();

        public Color DefaultColor = Color.White;                           // Available
        public Color OrderColor = ColorTranslator.FromHtml("#DCEEFF");     // Order taken / running
        public Color BillingColor = ColorTranslator.FromHtml("#DDF7E7");   // Checkout complete
        public Color CheckoutLabelBG = ColorTranslator.FromHtml("#2ECC71");
        public Color ReconfirmLabelBG = ColorTranslator.FromHtml("#F39C12");

        public Color ReservationColor = ColorTranslator.FromHtml("#9ACD32");
        public Color CancelProduct_order = ColorTranslator.FromHtml("#FFB3B3");
        public Color ExpiredColor = ColorTranslator.FromHtml("#E74C3C");


        public Color Defaultfontcolor = Color.Black;
        public Color Ordertablefontcolor = Color.Black;
        public Color Netamountfontcolor = Color.Red;

        ClsGeneral clsgen = new ClsGeneral();
        int categoryId = 0, SearchModetype = 0, categorystatus = 0;
        private Dictionary<string, Control> panelLookup = new Dictionary<string, Control>();

        float PRODUCT_WIDTH_PERCENT = 0.80f;
        float PRODUCT_HEIGHT_PERCENT = 0.80f;
        int maxColumns = 10;
        int tileW = 0;
        int tileH = 0;

        int Pagecount = 60;// Convert.ToInt32(ConfigurationManager.AppSettings["FBpagescount"]);
        int currentPage = 1;
        int pageSize = 0;
        int totalPages = 0;
        private List<DmnGridFields> TableData;
        private List<DmnGridFields> pagedatadata;
     
        private ToolTip menuTooltip = new ToolTip();
        private Image menuButtonImage = (Image)Properties.Resources.fb_sidemenu.Clone();

        private System.Windows.Forms.Timer AutoRunTimer = new System.Windows.Forms.Timer();
        private int AutoRunIndex = 0;
        private List<Control> TableList = new List<Control>();
        private bool AutoRunEnabled = true;

        int count = 0;
        private bool isRefreshingTableStatus;
        private bool isApplyingTableUI;
        private bool isTableViewClosing;
        private Font tableStatusFont;
        private Font tableNameFont;

        #endregion

        #region Fields

        //private List<TableViewItem> tables = new List<TableViewItem>();

        //private int columns = 10;
        //private int spacing = 10;

        //// Colors
        //private Color FreeColor = Color.LightGreen;
        //private Color BookingColor = Color.LightBlue;
        //private Color BillingColor = Color.Orange;
        //private Color CancelColor = Color.Red;
        //private Color ExpiredColor = Color.Gray;
        #endregion
        public FrmTableView(int type)
        {
            InitializeComponent();
            SearchModetype = type;
            tableStatusFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            tableNameFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            ConfigureModernTableSurface();
        }

        /// <summary>
        /// Applies the modern status legend and double buffering once, instead of forcing repaints on each tick.
        /// </summary>
        private void ConfigureModernTableSurface()
        {
            AutoTimer.Interval = 2000; // Preserve the existing refresh cadence.
            flp_tableview.BackColor = Color.FromArgb(246, 248, 251);
            flp_tableview.Padding = new Padding(10);
            flp_tableview.WrapContents = true;
            flp_tableview.FlowDirection = FlowDirection.LeftToRight;

            // FlowLayoutPanel is a standard WinForms control; enabling optimized double-buffering avoids
            // the flash caused by child invalidation during a status update.
            try
            {
                typeof(Control).GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                    ?.SetValue(flp_tableview, true, null);
            }
            catch
            {
                // Best effort only: rendering must still work if reflection is restricted.
            }

            AddStatusLegend();
        }

        private void AddStatusLegend()
        {
            if (tlp_main.Controls.ContainsKey("pnlTableStatusLegend"))
                return;

            var legend = new FlowLayoutPanel
            {
                Name = "pnlTableStatusLegend",
                Dock = DockStyle.Fill,
                AutoSize = false,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.White,
                Padding = new Padding(8, 2, 8, 2),
                Margin = new Padding(0)
            };

            AddLegendItem(legend, "Available", Color.White, Color.FromArgb(190, 198, 208));
            AddLegendItem(legend, "Order Taken", ColorTranslator.FromHtml("#DCEEFF"), ColorTranslator.FromHtml("#4B8DCA"));
            AddLegendItem(legend, "Checkout", ColorTranslator.FromHtml("#DDF7E7"), ColorTranslator.FromHtml("#239B56"));
            AddLegendItem(legend, "Reservation", ColorTranslator.FromHtml("#EAF5C7"), ColorTranslator.FromHtml("#7D9D26"));
            AddLegendItem(legend, "Cancelled Items", ColorTranslator.FromHtml("#FFE3E3"), ColorTranslator.FromHtml("#D64545"));

            tlp_main.RowStyles[0] = new RowStyle(SizeType.Absolute, 34F);
            tlp_main.Controls.Add(legend, 0, 0);
            legend.BringToFront();
        }

        private static void AddLegendItem(FlowLayoutPanel legend, string text, Color swatch, Color border)
        {
            var item = new FlowLayoutPanel
            {
                AutoSize = true,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.Transparent,
                Margin = new Padding(4, 3, 10, 3),
                Padding = new Padding(0)
            };
            var dot = new Panel
            {
                Size = new Size(12, 12),
                BackColor = swatch,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 3, 5, 0)
            };
            var label = new Label
            {
                AutoSize = true,
                Text = text,
                ForeColor = Color.FromArgb(55, 65, 81),
                Font = SystemFonts.MessageBoxFont,
                Margin = new Padding(0, 1, 0, 0)
            };
            dot.Paint += (s, e) =>
            {
                using (var pen = new Pen(border))
                    e.Graphics.DrawRectangle(pen, 0, 0, dot.Width - 1, dot.Height - 1);
            };
            item.Controls.Add(dot);
            item.Controls.Add(label);
            legend.Controls.Add(item);
        }

        private void FrmTableView_Load(object sender, EventArgs e)
        {
            pageSize = Pagecount;
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                //AppSession.RegisterForm(this, (int)LogoutFormList.Home);
            }
            LoadTableStatus(tabletype);

            //TableList = flp_tableview.Controls.Cast<Control>().ToList();
            //AutoRunTimer.Interval = 2000; // open next table every 2 sec
            //AutoRunTimer.Tick += AutoRunTimer_Tick;
            ////AutoRunTimer.Start();            
        }

        private void AutoRunTimer_Tick(object sender, EventArgs e)
        {
            if (!AutoRunEnabled) return;

            if (AutoRunIndex >= TableList.Count)
                AutoRunIndex = 0;

            Control table = TableList[AutoRunIndex];
            AutoRunIndex++;

            int tableId = Convert.ToInt32(table.Tag);

            AutoRunTimer.Stop();   // stop main timer when opening form

            //using (FrmOrderConsole memtest = new FrmOrderConsole(Convert.ToInt32(tableId), Convert.ToInt32(DashboardTableType.Normalorder), 0, CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage)))
            //{
            //    memtest.ShowDialog();
            //}
            count = count + 1;
            lblpagerinfo.Text = Convert.ToString(count);
            AutoRunTimer.Start();
        }

        #region Table Creation & Updation (ZERO-GC PAGING | 1000+ TABLES)

        #region Fields

        private readonly List<Panel> reusablePanels = new List<Panel>();

        private readonly Dictionary<int, TableUIRefs> tableUIRefs =
            new Dictionary<int, TableUIRefs>();

        private readonly Dictionary<int, TableOrderStatus> orderStatusCache =
            new Dictionary<int, TableOrderStatus>();

        private List<DmnGridFields> allTables = new List<DmnGridFields>();
        private List<DmnGridFields> currentPageTables = new List<DmnGridFields>();

        #endregion

        #region Helper Classes

        class TableUIRefs
        {
            public Panel Container;
            public TableLayoutPanel Inner;
            public Label TableName;
            public Label NetAmount;
            public Button CancelCount;
            public bool IsReservation;
            public bool IsExpiredReservation;
            public string ReservationBookmasterId;
        }

        class TableOrderStatus
        {
            public int TotalItems;
            public int PendingCount;
            public int CancelCount;
            public bool KitchenCancelled;
            public int Tid;
            public int Ordermid;
            public int Checkoutstatus;
            public bool CustomerInformedCancel;
        }

        #endregion

        #region Load / Refresh

        public void LoadTableStatus(int searchMode)
        {
            try
            {
                AutoTimer.Stop();

                LoadTableMasterData();
                ApplyPaging();

                if (reusablePanels.Count == 0)
                    CreateReusablePanels();   

                BindPageToPanels();
                LoadOrderStatusCache();
                UpdateTableUI();
            }
            catch (Exception ex)
            {
                clsgen.LogException("LoadTableStatus()", ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.TableView));
            }
            finally
            {
                if (!isTableViewClosing && !IsDisposed && !Disposing)
                    AutoTimer.Start();
            }
        }
        public void LoadOrderStatusCache()
        {
            BllSearch<DmnGridFields> bll = new BllSearch<DmnGridFields>();
            DmnSearch dmn = new DmnSearch();

            // One grouped status query for all active tables. Never issue an individual SELECT from the UI loop.
            dmn.Select =
                "MAX(od.OrderMasterId) AS Field1, " +
                "COUNT(od.OrderDetailsId) AS Field2, " +
                "SUM(CASE WHEN od.Status IN (1,2) THEN 1 ELSE 0 END) AS Field3, " +
                "SUM(CASE WHEN od.Status = 0 AND od.CancelNotifyStatus = 1 THEN 1 ELSE 0 END) AS Field4, " +
                "MAX(CASE WHEN od.KitchenCancelStatus = 1 THEN 1 ELSE 0 END) AS Field5, " +
                "ti.Tid AS Field6, " +
                "CASE WHEN MAX(CASE WHEN od.Status IN (1,2) THEN 1 ELSE 0 END) = 1 THEN 0 ELSE 1 END AS Field7, " +
                "MAX(ISNULL(ti.IsCustomerInformedCancel, 0)) AS Field8";
            dmn.Table =
                "tblTable_info ti " +
                "LEFT JOIN tblOrderDetails od ON od.OrderMasterId = ti.OrderMasterId AND od.AddonStatus IN (0,1,2) " +
                "LEFT JOIN tblOrderMaster om ON om.OrderMasterId = od.OrderMasterId AND om.Status <> 3";
            dmn.WhereNdGroupBy =
                "WHERE ti.Status = 1 " +
                "AND ((ti.OrderMasterId > 0 AND ti.OrderStatus = 1) OR ti.IsCustomerInformedCancel = 1) " +
                "GROUP BY ti.Tid ORDER BY ti.Tid";

            var list = bll.GetSearchData("prdSearch", dmn);
            if (list == null)
                return;

            var refreshedStatus = new Dictionary<int, TableOrderStatus>();

            foreach (var r in list)
            {
                int tableId = Convert.ToInt32(r.Field6);
                refreshedStatus[tableId] = new TableOrderStatus
                {
                    TotalItems = Convert.ToInt32(r.Field2),
                    PendingCount = Convert.ToInt32(r.Field3),
                    CancelCount = Convert.ToInt32(r.Field4),
                    KitchenCancelled = Convert.ToInt32(r.Field5) == 1,
                    Tid = tableId,
                    Ordermid = Convert.ToInt32(r.Field1),
                    Checkoutstatus = Convert.ToInt32(r.Field7),
                    CustomerInformedCancel = Convert.ToInt32(r.Field8) == 1
                };
            }

            orderStatusCache.Clear();
            foreach (var entry in refreshedStatus)
                orderStatusCache[entry.Key] = entry.Value;
        }


        public void RefreshTableOrders()
        {
            try
            {
                if (IsDisposed || Disposing || isRefreshingTableStatus)
                    return;

                LoadOrderStatusCache();
                UpdateTableUI();
            }
            catch (Exception ex)
            {
                clsgen.LogException("RefreshTableOrders()", ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.TableView));
            }
        }

        #endregion

        #region Data Load

        private void LoadTableMasterData()
        {
            BllTableView<DmnGridFields> search = new BllTableView<DmnGridFields>();
            DmnGridFields param = new DmnGridFields
            {
                Field30 = FrmLogin.BookingInterwell.ToString(),
                Field28 = categoryId.ToString(),
                Field29 = SearchModetype.ToString(),
                Field25 = categorystatus.ToString(),
                Field24 = Pagecount.ToString()
            };

            allTables = search.serachreport("prdTableView", param);
        }

        #endregion

        #region Panel Creation (ONCE)

        private void CreateReusablePanels()
        {
            flp_tableview.SuspendLayout();

            flp_tableview.Controls.Clear();
            reusablePanels.Clear();
            tableUIRefs.Clear();

            for (int i = 0; i < pageSize; i++)
            {
                Panel pnl = CreateEmptyTablePanel();
                reusablePanels.Add(pnl);
                flp_tableview.Controls.Add(pnl);
            }

            flp_tableview.ResumeLayout();
        }

        private Panel CreateEmptyTablePanel()
        {
            Panel pnl = new Panel
            {
                Size = new Size(140, 110),
                Margin = new Padding(7),
                Padding = new Padding(2),
                BackColor = Color.Transparent,
                Visible = false
            };

            TableLayoutPanel tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                BackColor = Color.White,
                Padding = new Padding(3),
                Margin = new Padding(0),
                AccessibleDescription = "0"
            };

            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 0));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 0));

            Label lblName = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = tableNameFont,
                ForeColor = Ordertablefontcolor,
                AccessibleDescription = "0"
            };

            Label lblNet = new Label
            {
                Dock = DockStyle.Fill,
                Visible = false,
                Font = tableStatusFont,
                TextAlign = ContentAlignment.MiddleCenter,
                AccessibleDescription = "0"
            };

            Button btnCancel = new Button
            {
                Size = new Size(22, 22),
                Visible = false,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            tlp.Controls.Add(lblName, 0, 1);
            tlp.Controls.Add(lblNet, 0, 2);

            pnl.Controls.Add(tlp);
            pnl.Controls.Add(btnCancel);

            btnCancel.Location = new Point(pnl.Width - 27, 5);
            btnCancel.BringToFront();

            UIHelper.RoundCorners(pnl, 25, null);

            int key = pnl.GetHashCode();
            tableUIRefs[key] = new TableUIRefs
            {
                Container = pnl,
                Inner = tlp,
                TableName = lblName,
                NetAmount = lblNet,
                CancelCount = btnCancel
            };

            pnl.Click += tableClickEventHandler;
            tlp.Click += tableClickEventHandler;
            lblName.Click += tableClickEventHandler;
            lblNet.Click += tableClickEventHandler;

            return pnl;
        }


        #endregion

        #region Paging

        private void ApplyPaging()
        {
            if (pageSize <= 0)
                pageSize = Pagecount;

            totalPages = (int)Math.Ceiling(allTables.Count / (double)pageSize);

            currentPageTables = allTables
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            lblpagerinfo.Text = allTables.Count == 0
                ? "No tables found"
                : $"Page {currentPage} of {totalPages} | Total Records: {allTables.Count}";

            btnprev.Enabled = currentPage > 1;
            btnnext.Enabled = currentPage < totalPages;
        }

        #endregion

        #region Bind Page (ZERO-GC CORE)

        private void BindPageToPanels()
        {
            flp_tableview.SuspendLayout();

            for (int i = 0; i < reusablePanels.Count; i++)
            {
                Panel pnl = reusablePanels[i];
                pnl.Visible = false;

                if (i >= currentPageTables.Count)
                    continue;

                var row = currentPageTables[i];
                int tableId = Convert.ToInt32(row.Field3);

                pnl.Tag = tableId;
                pnl.Name = "pnl_" + tableId;

                var ui = tableUIRefs[pnl.GetHashCode()];

                ui.Container.Tag = tableId;
                ui.Inner.Tag = tableId;
                ui.TableName.Tag = tableId;
                ui.NetAmount.Tag = tableId;
                ui.CancelCount.Tag = tableId;

                ui.TableName.Text = row.Field4;
                ui.CancelCount.AccessibleDescription = Convert.ToString((int)DashboardTableType.Normalorder);
                ui.IsReservation = Convert.ToInt32(row.Field5) == 0
                    && row.EndDate != null && row.Field7 != null;
                ui.IsExpiredReservation = false;
                ui.ReservationBookmasterId = row.Field7;
                ui.TableName.AccessibleDescription = Convert.ToString((int)DashboardTableType.Normalorder);
                ui.Inner.AccessibleDescription = Convert.ToString((int)DashboardTableType.Normalorder);
                ui.Container.AccessibleDescription = Convert.ToString((int)DashboardTableType.Normalorder);
                ui.NetAmount.AccessibleDescription = Convert.ToString((int)DashboardTableType.Normalorder);

                if (ui.IsReservation)
                {
                    DateTime reservationEnd = row.EndDate;
                    DateTime now = DateTime.Now;
                    ui.IsExpiredReservation = now > reservationEnd;
                    if (ui.IsExpiredReservation)
                    {
                        string description = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel)
                            + "," + row.Field7;
                        ui.Container.AccessibleDescription = description;
                        ui.Inner.AccessibleDescription = description;
                        ui.TableName.AccessibleDescription = description;
                    }
                    else
                    {
                        string description = Convert.ToInt32(DashboardTableType.Reverse_Table)
                            + "," + row.Field7;
                        ui.Container.AccessibleDescription = description;
                        ui.Inner.AccessibleDescription = description;
                        ui.TableName.AccessibleDescription = description;
                    }
                }

                pnl.Visible = true;
            }

            flp_tableview.ResumeLayout();
        }


        #endregion

        #region UI Update

        public void UpdateTableUI()
        {
            if (isApplyingTableUI || IsDisposed || Disposing)
                return;

            isApplyingTableUI = true;
            flp_tableview.SuspendLayout();
            try
            {
                foreach (var pnl in reusablePanels)
                {
                    if (!pnl.Visible || pnl.Tag == null)
                        continue;

                    int tableId = Convert.ToInt32(pnl.Tag);
                    if (!tableUIRefs.TryGetValue(pnl.GetHashCode(), out var ui))
                        continue;

                    // Reusable card font and padding are initialized once, then only changed when the data changes.
                    Color nextColor = DefaultColor;
                    string statusText = string.Empty;
                    Color statusLabelColor = Color.Transparent;
                    bool showStatus = false;
                    bool showCancelBadge = false;
                    int cancelCount = 0;

                    ui.TableName.AccessibleName = "0";
                    ui.NetAmount.AccessibleName = "0";
                    ui.Container.AccessibleName = "0";
                    ui.Inner.AccessibleName = "0";
                    ui.CancelCount.AccessibleName = "0";

                    if (ui.IsReservation && ui.IsExpiredReservation)
                    {
                        string expiredDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel)
                            + "," + ui.ReservationBookmasterId;
                        ui.Container.AccessibleDescription = expiredDescription;
                        ui.Inner.AccessibleDescription = expiredDescription;
                        ui.TableName.AccessibleDescription = expiredDescription;
                        ui.NetAmount.AccessibleDescription = expiredDescription;
                        nextColor = ExpiredColor;
                        statusText = "Reservation Expired";
                        statusLabelColor = ExpiredColor;
                        showStatus = true;
                    }
                    else if (ui.IsReservation)
                    {
                        string reservationDescription = Convert.ToInt32(DashboardTableType.Reverse_Table)
                            + "," + ui.ReservationBookmasterId;
                        ui.Container.AccessibleDescription = reservationDescription;
                        ui.Inner.AccessibleDescription = reservationDescription;
                        ui.TableName.AccessibleDescription = reservationDescription;
                        ui.NetAmount.AccessibleDescription = reservationDescription;
                        nextColor = ReservationColor;
                        statusText = "Reservation";
                        statusLabelColor = ColorTranslator.FromHtml("#6F8D22");
                        showStatus = true;
                    }
                    else if (orderStatusCache.TryGetValue(tableId, out var status))
                    {
                        string normalDescription = Convert.ToString((int)DashboardTableType.Normalorder);
                        ui.Container.AccessibleDescription = normalDescription;
                        ui.Inner.AccessibleDescription = normalDescription;
                        ui.TableName.AccessibleDescription = normalDescription;
                        ui.NetAmount.AccessibleDescription = normalDescription;

                        if (status.CustomerInformedCancel)
                        {
                            nextColor = CancelProduct_order;
                            statusText = "Staff Cancel Order";
                            statusLabelColor = ReconfirmLabelBG;
                            showStatus = true;
                            ui.Container.AccessibleName = "1";
                            ui.Inner.AccessibleName = "1";
                            ui.TableName.AccessibleName = "1";
                            ui.NetAmount.AccessibleName = "1";
                            ui.CancelCount.AccessibleName = "1";
                        }
                        else if (status.CancelCount > 0)
                        {
                            nextColor = CancelProduct_order;
                            showCancelBadge = true;
                            cancelCount = status.CancelCount;
                        }
                        else if (status.KitchenCancelled)
                        {
                            nextColor = CancelProduct_order;
                            statusText = "Reconfirm Order";
                            statusLabelColor = ReconfirmLabelBG;
                            showStatus = true;
                        }
                        else if (status.TotalItems > 0 && status.PendingCount > 0)
                        {
                            nextColor = OrderColor;
                        }
                        else if (status.TotalItems > 0 && status.Checkoutstatus == 1)
                        {
                            nextColor = BillingColor;
                            statusText = "Checkout";
                            statusLabelColor = CheckoutLabelBG;
                            showStatus = true;
                        }
                        else
                        {
                            nextColor = status.Ordermid > 0 && status.TotalItems > 0
                                ? OrderColor
                                : DefaultColor;
                        }
                    }
                    else
                    {
                        // No current order/reservation means table is available.
                        nextColor = DefaultColor;
                        string availableDescription = Convert.ToString((int)DashboardTableType.Normalorder);
                        ui.Container.AccessibleDescription = availableDescription;
                        ui.Inner.AccessibleDescription = availableDescription;
                        ui.TableName.AccessibleDescription = availableDescription;
                        ui.NetAmount.AccessibleDescription = availableDescription;
                    }

                    // Preserve the existing reservation/expiry click descriptions already assigned on the card.
                    // The status-card view uses data-bound cards and never rebuilds the FlowLayoutPanel per tick.
                    if (ui.Inner.BackColor != nextColor)
                        ui.Inner.BackColor = nextColor;

                    string currentDescription = ui.Container.AccessibleDescription;
                    if (!string.IsNullOrEmpty(currentDescription))
                    {
                        ui.Inner.AccessibleDescription = currentDescription;
                        ui.TableName.AccessibleDescription = currentDescription;
                        ui.NetAmount.AccessibleDescription = currentDescription;
                    }

                    bool statusVisible = showStatus;
                    if (ui.NetAmount.Visible != statusVisible)
                        ui.NetAmount.Visible = statusVisible;
                    if (ui.NetAmount.Text != statusText)
                        ui.NetAmount.Text = statusText;
                    if (ui.NetAmount.BackColor != statusLabelColor)
                        ui.NetAmount.BackColor = statusLabelColor;
                    if (ui.NetAmount.ForeColor != Color.White)
                        ui.NetAmount.ForeColor = Color.White;
                    if (!ReferenceEquals(ui.NetAmount.Font, tableStatusFont))
                        ui.NetAmount.Font = tableStatusFont;

                    float firstRow = showStatus ? 25 : 0;
                    float middleRow = showStatus ? 60 : 100;
                    float lastRow = showStatus ? 25 : 0;
                    if (ui.Inner.RowStyles[0].Height != firstRow)
                        ui.Inner.RowStyles[0].Height = firstRow;
                    if (ui.Inner.RowStyles[1].Height != middleRow)
                        ui.Inner.RowStyles[1].Height = middleRow;
                    if (ui.Inner.RowStyles[2].Height != lastRow)
                        ui.Inner.RowStyles[2].Height = lastRow;

                    if (ui.CancelCount.Visible != showCancelBadge)
                        ui.CancelCount.Visible = showCancelBadge;
                    if (showCancelBadge)
                    {
                        string text = cancelCount.ToString();
                        if (ui.CancelCount.Text != text)
                            ui.CancelCount.Text = text;
                        if (ui.CancelCount.Size != new Size(22, 22))
                            ui.CancelCount.Size = new Size(22, 22);
                        if (ui.CancelCount.Location != new Point(ui.Container.Width - 27, 5))
                            ui.CancelCount.Location = new Point(ui.Container.Width - 27, 5);
                        if (ui.CancelCount.BackColor != Color.Black)
                            ui.CancelCount.BackColor = Color.Black;
                        if (ui.CancelCount.ForeColor != Color.White)
                            ui.CancelCount.ForeColor = Color.White;
                        ui.CancelCount.FlatStyle = FlatStyle.Flat;
                        ui.CancelCount.FlatAppearance.BorderSize = 0;

                        // Create and release the circular region only when needed.
                        if (ui.CancelCount.Region == null)
                        {
                            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                            {
                                path.AddEllipse(0, 0, ui.CancelCount.Width, ui.CancelCount.Height);
                                ui.CancelCount.Region = new Region(path);
                            }
                        }
                        ui.CancelCount.BringToFront();
                    }
                    else
                    {
                        if (ui.CancelCount.Size != Size.Empty)
                            ui.CancelCount.Size = Size.Empty;
                        ui.CancelCount.Location = new Point(-100, -100);
                        if (ui.CancelCount.Region != null)
                        {
                            ui.CancelCount.Region.Dispose();
                            ui.CancelCount.Region = null;
                        }
                        if (ui.CancelCount.Text.Length > 0)
                            ui.CancelCount.Text = string.Empty;
                    }
                }
            }
            finally
            {
                flp_tableview.ResumeLayout(true);
                isApplyingTableUI = false;
            }
        }


        #endregion

        #region Timer

        private void AutoTimer_Tick(object sender, EventArgs e)
        {
            if (isRefreshingTableStatus || IsDisposed || Disposing || !Visible)
                return;

            isRefreshingTableStatus = true;
            AutoTimer.Stop();
            try
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    lblmemory.Text = $"Memory: {(p.PrivateMemorySize64 / 1024d / 1024d):F2} MB";
                }

                LoadOrderStatusCache();
                UpdateTableUI();
            }
            catch (Exception ex)
            {
                clsgen.LogException("AutoTimer_Tick()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
            finally
            {
                isRefreshingTableStatus = false;
                if (!isTableViewClosing && !IsDisposed && !Disposing)
                    AutoTimer.Start();
            }
        }

        #endregion

        #region Paging Buttons

        private void btnprev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                ApplyPaging();
                BindPageToPanels();
                UpdateTableUI();
            }
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                ApplyPaging();
                BindPageToPanels();
                UpdateTableUI();
            }
        }

        #endregion

        #endregion

        #endregion // Modern reusable table view

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isTableViewClosing = true;
            AutoTimer?.Stop();
            AutoTimer?.Dispose();
            if (tableStatusFont != null)
            {
                tableStatusFont.Dispose();
                tableStatusFont = null;
            }
            if (tableNameFont != null)
            {
                tableNameFont.Dispose();
                tableNameFont = null;
            }
            base.OnFormClosing(e);
        }
        public void CheckexpireTable(int tid, int Bmid)
        {
            try
            {
                BllReservation bllReservation = new BllReservation();
                DmnReservation dmnReservation = new DmnReservation();
                dmnReservation.BookmasterId = Bmid;
                dmnReservation.Tid = tid;
                dmnReservation.CreatedBy = FrmDashboard.UserId;
                dmnReservation.Status = 0;
                dmnReservation.BranchId = 1030;
                var InsertStatus = bllReservation.Reservation_expire_Delete("prd_CheckexpireTableReservation", dmnReservation);
                if (InsertStatus.BookmasterId > 0)
                {
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("TableView page  CheckexpireTable()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }
        private void tableClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                AutoTimer.Stop();

                FrmDashboard dashboard = Application.OpenForms.OfType<FrmDashboard>().FirstOrDefault();
                if (dashboard != null)
                {
                    dashboard.AssignDrawerCheck();
                }

                if (FrmDrawer.DrawerID == 0)
                {
                    return;
                }


                CheckedtableNames.Clear();
                Control clickedControl = sender as Control;
                if (clickedControl == null)
                    return;

                // A click can originate from a nested control, so resolve the reusable card's data consistently.
                Control tableControl = clickedControl;
                while (tableControl != null && tableControl.Tag == null)
                    tableControl = tableControl.Parent;
                if (tableControl == null)
                    return;

                string textvalue = !string.IsNullOrEmpty(clickedControl.AccessibleDescription)
                    ? clickedControl.AccessibleDescription
                    : tableControl.AccessibleDescription;
                if (string.IsNullOrEmpty(textvalue))
                    textvalue = Convert.ToString((int)DashboardTableType.Normalorder);

                int Cancelorderstatus = Convert.ToInt32(clickedControl.AccessibleName);
                if (Cancelorderstatus == 0)
                    Cancelorderstatus = Convert.ToInt32(tableControl.AccessibleName);
                string[] parts = textvalue.Split(',');
                string bmasterid = "";
                int Btype = 0;
                if (parts.Length >= 2)
                {
                    Btype = Convert.ToInt32(parts[0]);
                    bmasterid = parts[1];
                }
                else
                    Btype = Convert.ToInt32(parts[0]);

                if (clickedControl != null)
                {
                    string tableCode = clickedControl.Name;
                    int clickedTableId = Convert.ToInt32(tableControl.Tag);
                    int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                    int screenHeight = Screen.PrimaryScreen.Bounds.Height;
                    if (Btype == Convert.ToInt32(DashboardTableType.Normalorder))
                    {
                        if (System.Windows.Forms.Application.OpenForms["FrmOrder"] != null)
                        {
                            FrmOrderConsole _instanceFrmOrder = Application.OpenForms["FrmOrder"] as FrmOrderConsole;
                            if (_instanceFrmOrder.WindowState == FormWindowState.Minimized)
                            {
                                _instanceFrmOrder.WindowState = FormWindowState.Maximized;
                            }
                            else
                            {
                                //this.Visible = false;
                                _instanceFrmOrder.ShowDialog();
                                _instanceFrmOrder.BringToFront();
                            }
                        }
                        else
                        {
                            int tagValue;
                            if (clickedControl.Tag != null)
                            {
                                using (FrmOrderConsole memtest = new FrmOrderConsole(clickedTableId, Convert.ToInt32(DashboardTableType.Normalorder), 0, CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage), Cancelorderstatus))
                                {
                                    memtest.ShowDialog();
                                }
                                //using (FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Normalorder), 0, CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage)))
                                //{
                                //    _instanceFrmOrder.ShowDialog();
                                //}
                            }
                        }

                    }

                    else if (Btype == Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel))
                    {
                        var result = CMessageBox.Show("Do You want Cancel  This Reservation", "Confirm The Action", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            //BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                            //DmnSearch dmnSearch = new DmnSearch();
                            //dmnSearch.Select = " bm.BookmasterId Field1";
                            //dmnSearch.Table = "tblBookDetails bd " +
                            //    "LEFT JOIN tblbookMaster bm on bm.BookmasterId=bd.BookmasterId ";
                            //dmnSearch.WhereNdGroupBy = " where bm.Status=1 and Rid=0 and bd.MTID= " + clickedControl.Tag;
                            //dmnSearch.OrderBy = "";
                            //var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                            //if(gridSource.Count>0)
                            //{
                            BllReservation bllReservation = new BllReservation();
                            DmnReservation dmnReservation = new DmnReservation();
                            dmnReservation.BookmasterId = Convert.ToInt32(bmasterid);
                            dmnReservation.OrderMasterId = 0;
                            dmnReservation.CreatedBy = FrmDashboard.UserId;
                            dmnReservation.Status = 0;
                            dmnReservation.BranchId = 1030;
                            dmnReservation.Cancel_Type = Convert.ToInt32(Rev_Cancel_Type.Normalcancel);
                            var InsertStatus = bllReservation.Reservation_Delete_dashboard("prd_deleteReservation", dmnReservation);
                            if (InsertStatus.BookmasterId > 0)
                            {
                                CMessageBox.Show("Reservation Cancel Sucessfull", "Information", MessageBoxButtons.OK);
                                LoadOrderStatusCache();
                                UpdateTableUI();

                            }
                            //}
                        }
                        else if (result == DialogResult.No)
                        {
                            //using (FrmOrderConsole _instanceFrmOrder = new FrmOrderConsole(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid), CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage)))
                            //{
                            //    _instanceFrmOrder.ShowDialog();
                            //}

                        }
                    }
                    else if (Btype == Convert.ToInt32(DashboardTableType.Reverse_Table))
                    {
                        //using (FrmOrderConsole _instanceFrmOrder = new FrmOrderConsole(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid), CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage)))
                        //{
                        //    _instanceFrmOrder.ShowDialog();
                        //}
                    }
                }
                clsgen.Memoryrelease();
            }
            catch (Exception ex)
            {
                clsgen.LogException("TableView page  tableClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }



    }
}


/*
 RestaurantWaiter
│
├── Controllers
│   └── TableStatusController.cs        // REST API for table status
│
├── Hubs
│   └── TableHub.cs                     // SignalR hub
│
├── Pages
│   ├── TableView.cshtml.cs
│   │   ├── Index.cshtml
│   │   ├── Index.cshtml.cs
│   │
│   ├── OrderConsole.cshtml.cs
│   │   ├── Index.cshtml
│   │   ├── Index.cshtml.cs
│   │
│   └── API
│       └── OrderService.cshtml.cs      // Razor Page APIs (save order, KOT, etc.)
│
├── Services
│   ├── TableStatusService.cs           // 🔥 SINGLE SOURCE OF TRUTH
│   ├── TableViewNotifier.cs            // SignalR broadcaster
│   └── KitchenNotifier.cs (optional)
│
├── Models
│   ├── TableOrderStatus.cs
│   ├── TableViewDto.cs
│   ├── CategoryDto.cs
│   └── Domain
│       ├── DmnOrderMaster.cs
│       ├── DmnOrderDetails.cs
│       └── DmnGridFields.cs
│
├── wwwroot
│   ├── css
│   │   └── tableview.css
│   │
│   ├── js
│   │   └── tableview.js
│   │
│   └── lib
│       └── signalr
│
├── Program.cs
└── appsettings.json

 
 
 */