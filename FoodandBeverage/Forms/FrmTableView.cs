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
using FoodandBeverage.Controls;
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
        int Pagecount = 60;// Convert.ToInt32(ConfigurationManager.AppSettings["FBpagescount"]);
        int currentPage = 1;
        int pageSize = 0;
        int totalPages = 0;
        private ToolTip menuTooltip = new ToolTip();
        private Image menuButtonImage = (Image)Properties.Resources.fb_sidemenu.Clone();

        private bool isRefreshingTableStatus;
        private bool isApplyingTableUI;
        private bool isTableViewClosing;
        private Thread _loadingAnimationThread;
        private LoadingGIF_Timer _loadingAnimationForm;

        private void StartLoadingAnimation()
        {
            if (_loadingAnimationThread != null && _loadingAnimationThread.IsAlive)
                return;

            _loadingAnimationThread = new Thread(() =>
            {
                using (LoadingGIF_Timer loader = new LoadingGIF_Timer())
                {
                    loader.StartPosition = FormStartPosition.CenterScreen;
                    loader.TitleText = "Opening tables";
                    loader.StatusText = "Loading table status and orders…";
                    _loadingAnimationForm = loader;
                    try { Application.Run(loader); }
                    finally { _loadingAnimationForm = null; }
                }
            });
            _loadingAnimationThread.IsBackground = true;
            _loadingAnimationThread.SetApartmentState(ApartmentState.STA);
            _loadingAnimationThread.Start();
            Thread.Sleep(100);
        }

        private void StopLoadingAnimation()
        {
            LoadingGIF_Timer loader = _loadingAnimationForm;
            if (loader != null && !loader.IsDisposed)
            {
                try { loader.BeginInvoke(new Action(() => loader.Close())); }
                catch { /* Loader may already be closing. */ }
            }

            if (_loadingAnimationThread != null && _loadingAnimationThread.IsAlive &&
                Thread.CurrentThread != _loadingAnimationThread)
                _loadingAnimationThread.Join(500);

            _loadingAnimationThread = null;
        }

        #endregion

        public FrmTableView(int type)
        {
            InitializeComponent();
            SearchModetype = type;
            ConfigureModernTableSurface();
        }

        /// <summary>
        /// Leaves the form design unchanged and enables buffered painting for the table area.
        /// </summary>
        private void ConfigureModernTableSurface()
        {
            AutoTimer.Interval = 2000;
            try
            {
                typeof(Control).GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                    ?.SetValue(flp_tableview, true, null);
            }
            catch
            {
                // Best-effort anti-flicker optimization only.
            }
        }

        private void FrmTableView_Load(object sender, EventArgs e)
        {
            StartLoadingAnimation();
            try
            {
                pageSize = Pagecount;
                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    //AppSession.RegisterForm(this, (int)LogoutFormList.Home);
                }
                LoadTableStatus(tabletype);
            }
            finally
            {
                StopLoadingAnimation();
            }
        }

        #region Modern Table View

        private readonly List<ModernTableCard> reusablePanels =
            new List<ModernTableCard>();

        private readonly Dictionary<ModernTableCard, TableCardBinding> tableUIRefs =
            new Dictionary<ModernTableCard, TableCardBinding>();

        private readonly Dictionary<int, TableOrderStatus> orderStatusCache =
            new Dictionary<int, TableOrderStatus>();

        private List<DmnGridFields> allTables = new List<DmnGridFields>();
        private List<DmnGridFields> currentPageTables = new List<DmnGridFields>();

        #region Helper Classes

        private sealed class TableCardBinding
        {
            public bool IsReservation;
            public bool IsReservationReady;
            public bool IsExpiredReservation;
            public string ReservationBookmasterId;
        }

        private sealed class TableOrderStatus
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
                "MAX(ti.OrderMasterId) AS Field1, " +
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

            allTables = search.serachreport("prdTableView", param) ?? new List<DmnGridFields>();
        }

        #endregion

        #region Custom Table Cards (ONCE)

        private void CreateReusablePanels()
        {
            flp_tableview.SuspendLayout();

            try
            {
                flp_tableview.Controls.Clear();
                reusablePanels.Clear();
                tableUIRefs.Clear();

                for (int i = 0; i < pageSize; i++)
                {
                    ModernTableCard card = CreateEmptyTableCard();
                    reusablePanels.Add(card);
                    tableUIRefs[card] = new TableCardBinding();
                    flp_tableview.Controls.Add(card);
                }
            }
            finally
            {
                flp_tableview.ResumeLayout(true);
            }
        }

        private ModernTableCard CreateEmptyTableCard()
        {
            ModernTableCard card = new ModernTableCard
            {
                Size = new Size(140, 110),
                Margin = new Padding(7),
                Visible = false,
                Name = "tableCard"
            };

            card.Click += tableClickEventHandler;
            return card;
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

            try
            {
                for (int i = 0; i < reusablePanels.Count; i++)
                {
                    ModernTableCard card = reusablePanels[i];
                    card.Visible = false;

                    if (i >= currentPageTables.Count)
                        continue;

                    DmnGridFields row = currentPageTables[i];
                    int tableId = Convert.ToInt32(row.Field3);
                    TableCardBinding binding = tableUIRefs[card];

                    card.Tag = tableId;
                    card.Name = "pnl_" + tableId;
                    card.Text = Convert.ToString(row.Field4) ?? string.Empty;
                    card.AccessibleName = "0";
                    card.AccessibleDescription = Convert.ToString((int)DashboardTableType.Normalorder);

                    binding.IsReservation = Convert.ToInt32(row.Field5) == 0
                        && row.EndDate != null && row.Field7 != null;
                    binding.IsReservationReady = false;
                    binding.IsExpiredReservation = false;
                    binding.ReservationBookmasterId = row.Field7;

                    if (binding.IsReservation)
                    {
                        DateTime reservationEnd = row.EndDate;
                        DateTime now = DateTime.Now;
                        DateTime reservationWindowStart = reservationEnd.AddMinutes(
                            -Math.Max(0, Convert.ToDouble(FrmLogin.BookingInterwell)));

                        binding.IsExpiredReservation = now > reservationEnd;
                        binding.IsReservationReady = now >= reservationWindowStart && now <= reservationEnd;

                        string description = binding.IsExpiredReservation
                            ? Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + row.Field7
                            : (binding.IsReservationReady
                                ? Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + row.Field7
                                : Convert.ToString((int)DashboardTableType.Normalorder));

                        card.AccessibleDescription = description;
                    }

                    card.Visible = true;
                }
            }
            finally
            {
                flp_tableview.ResumeLayout(true);
            }
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
                foreach (ModernTableCard card in reusablePanels)
                {
                    if (!card.Visible || card.Tag == null)
                        continue;

                    int tableId = Convert.ToInt32(card.Tag);
                    if (!tableUIRefs.TryGetValue(card, out TableCardBinding binding))
                        continue;

                    ModernTableCardState nextState = ModernTableCardState.Available;
                    string statusText = string.Empty;
                    int cancelledCount = 0;

                    card.AccessibleName = "0";

                    if (binding.IsReservation && binding.IsExpiredReservation)
                    {
                        nextState = ModernTableCardState.ReservationExpired;
                        statusText = "Reservation Expired";
                        card.AccessibleDescription =
                            Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel)
                            + "," + binding.ReservationBookmasterId;
                    }
                    else if (binding.IsReservation && binding.IsReservationReady)
                    {
                        nextState = ModernTableCardState.Reservation;
                        statusText = "Reservation";
                        card.AccessibleDescription =
                            Convert.ToInt32(DashboardTableType.Reverse_Table)
                            + "," + binding.ReservationBookmasterId;
                    }
                    else if (binding.IsReservation)
                    {
                        nextState = ModernTableCardState.Reservation;
                        statusText = "Reserved";
                        card.AccessibleDescription =
                            Convert.ToString((int)DashboardTableType.Normalorder);
                    }
                    else if (orderStatusCache.TryGetValue(tableId, out TableOrderStatus status))
                    {
                        card.AccessibleDescription =
                            Convert.ToString((int)DashboardTableType.Normalorder);

                        if (status.CustomerInformedCancel)
                        {
                            nextState = ModernTableCardState.StaffCancelled;
                            statusText = "Staff Cancel Order";
                            card.AccessibleName = "1";
                        }
                        else if (status.CancelCount > 0)
                        {
                            nextState = ModernTableCardState.CancelledItems;
                            statusText = "Cancelled Items";
                            cancelledCount = status.CancelCount;
                        }
                        else if (status.KitchenCancelled)
                        {
                            nextState = ModernTableCardState.ReconfirmOrder;
                            statusText = "Reconfirm Order";
                        }
                        else if (status.TotalItems > 0 && status.PendingCount > 0)
                        {
                            nextState = ModernTableCardState.OrderTaken;
                            statusText = "Order Taken";
                        }
                        else if (status.TotalItems > 0 && status.Checkoutstatus == 1)
                        {
                            nextState = ModernTableCardState.Checkout;
                            statusText = "Checkout";
                        }
                        else if (status.Ordermid > 0 && status.TotalItems > 0)
                        {
                            nextState = ModernTableCardState.OrderTaken;
                            statusText = "Order Taken";
                        }
                        else
                        {
                            nextState = ModernTableCardState.Available;
                            statusText = "Available";
                        }
                    }
                    else
                    {
                        nextState = ModernTableCardState.Available;
                        statusText = "Available";
                        card.AccessibleDescription =
                            Convert.ToString((int)DashboardTableType.Normalorder);
                    }

                    card.SetVisualState(nextState, statusText, cancelledCount);
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

        #endregion // Modern Table View

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isTableViewClosing = true;
            AutoTimer?.Stop();
            AutoTimer?.Dispose();
            base.OnFormClosing(e);
        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            FrmDashboard.Instance.Menuhome.PerformClick();
            Close();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            categoryId = 0;
            categorystatus = 0;
            SearchModetype = 0;
            flp_sidemenu.Visible = false;
            txttablecode_search.Clear();
            LoadTableStatus(0);
        }

        private void FrmTableView_FormClosing(object sender, FormClosingEventArgs e)
        {
            clsgen.Memoryrelease();
            if (FrmDashboard.AutoLogoutWhenIdle)
                AppSession.UnregisterForm(this);

            if (menuTooltip != null)
                menuTooltip.RemoveAll();
            if (menuButtonImage != null)
            {
                menuButtonImage.Dispose();
                menuButtonImage = null;
            }
        }

        private void FrmTableView_Resize(object sender, EventArgs e)
        {
            // FlowLayoutPanel performs wrapping itself; keep the current cards and avoid rebuilding them.
            if (flp_tableview != null)
            {
                flp_tableview.WrapContents = true;
                flp_tableview.PerformLayout();
            }

            if (flp_sidemenu != null && flp_sidemenu.Visible)
                flp_sidemenu.Height = Math.Max(120, (int)(ClientSize.Height * 0.5));
        }

        private void btn_showmenu_Click(object sender, EventArgs e)
        {
            flp_sidemenu.Visible = !flp_sidemenu.Visible;
            if (!flp_sidemenu.Visible)
                return;

            if (flp_sidemenu.Controls.Count == 0)
            {
                flp_sidemenu.SuspendLayout();
                try
                {
                    flp_sidemenu.FlowDirection = FlowDirection.TopDown;
                    flp_sidemenu.WrapContents = false;
                    flp_sidemenu.AutoScroll = true;
                    flp_sidemenu.BackColor = Color.FromArgb(32, 41, 58);
                    flp_sidemenu.Width = Math.Max(180, btn_showmenu.Width);
                    flp_sidemenu.Height = Math.Max(120, (int)(ClientSize.Height * 0.5));

                    AddTableCategoryButton("All tables", 0, false);

                    BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                    DmnSearch query = new DmnSearch
                    {
                        Select = "Fcid,CategoryName",
                        Table = "tblCategory_FTI",
                        WhereNdGroupBy = "WHERE Status = 1 AND CategoryType = 1",
                        OrderBy = ""
                    };

                    var categories = bllSearch.GetSearchData("prdSearch", query);
                    if (categories != null)
                    {
                        foreach (var category in categories)
                            AddTableCategoryButton(category.CategoryName, category.Fcid, true);
                    }
                }
                finally
                {
                    flp_sidemenu.ResumeLayout(true);
                }
            }

            flp_sidemenu.BringToFront();
        }

        private void AddTableCategoryButton(string caption, long id, bool isCategory)
        {
            var button = new Button
            {
                Name = isCategory ? "btnTableCategory_" + id : "btnTableCategory_All",
                Tag = id,
                Text = caption,
                Width = Math.Max(160, flp_sidemenu.ClientSize.Width - 24),
                Height = 38,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(43, 54, 73),
                ForeColor = Color.White,
                Font = SystemFonts.MessageBoxFont,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 4, 0),
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(58, 75, 100);
            button.Click += SideMenuClick;
            menuTooltip.SetToolTip(button, caption);
            flp_sidemenu.Controls.Add(button);
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            AutoTimer.Stop();
            try
            {
                BllTableView<DmnGridFields> search = new BllTableView<DmnGridFields>();
                DmnGridFields param = new DmnGridFields
                {
                    Field30 = Convert.ToString(FrmLogin.BookingInterwell),
                    Field29 = Convert.ToString(SearchModetype),
                    Field28 = Convert.ToString(categoryId),
                    Field26 = "1",
                    Field27 = Convert.ToString(txttablecode_search.Text),
                    Field25 = Convert.ToString(categorystatus),
                    Field24 = Convert.ToString(Pagecount)
                };

                allTables = search.serachreport("prdTableView", param) ?? new List<DmnGridFields>();
                currentPage = 1;
                pageSize = Pagecount;
                ApplyPaging();

                if (reusablePanels.Count == 0)
                    CreateReusablePanels();

                BindPageToPanels();
                LoadOrderStatusCache();
                UpdateTableUI();
            }
            catch (Exception ex)
            {
                clsgen.LogException("TableView page btnsearch_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
            finally
            {
                if (!isTableViewClosing && !IsDisposed && !Disposing)
                    AutoTimer.Start();
            }
        }

        private void SideMenuClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            categoryId = Convert.ToInt32(button.Tag);
            categorystatus = categoryId == 0 ? 0 : 1;
            currentPage = 1;
            flp_sidemenu.Visible = false;
            LoadTableStatus(0);
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
                            if (clickedControl.Tag != null)
                            {
                                // A checkout card is an existing order, not a fresh table.
                                // Pass its exact OrderMasterId so Order Console reopens the saved KOT.
                                int existingOrderMasterId = 0;
                                TableOrderStatus tableOrder;
                                if (orderStatusCache.TryGetValue(clickedTableId, out tableOrder))
                                    existingOrderMasterId = tableOrder.Ordermid;

                                using (FrmOrderConsole memtest = new FrmOrderConsole(
                                    clickedTableId,
                                    Convert.ToInt32(DashboardTableType.Normalorder),
                                    0,
                                    CheckedtableNames,
                                    Convert.ToInt32(PageType.OrderPage),
                                    existingOrderMasterId,
                                    Convert.ToInt32(SourcepageType.Tableviewpage),
                                    Cancelorderstatus))
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
                        }
                        else if (result == DialogResult.No)
                        {
                            // The user chose not to cancel the expired reservation.
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

