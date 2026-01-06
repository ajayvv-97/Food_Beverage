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
        public Color OrderColor = ColorTranslator.FromHtml("#ADD8E6");     // Running
        public Color BillingColor = ColorTranslator.FromHtml("#ADD8E6");   // Checkout 2ECC71
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

            using (FrmOrder memtest = new FrmOrder(Convert.ToInt32(tableId), Convert.ToInt32(DashboardTableType.Normalorder), 0, CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage)))
            {
                memtest.ShowDialog();
            }
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

                AutoTimer.Start();
            }
            catch (Exception ex)
            {
                clsgen.LogException("LoadTableStatus()", ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.TableView));
            }
        }
        public void LoadOrderStatusCache()
        {
            orderStatusCache.Clear();

            BllSearch<DmnGridFields> bll = new BllSearch<DmnGridFields>();
            DmnSearch dmn = new DmnSearch();

            dmn.Select =
                "od.OrderMasterId AS Field1, " +
                "COUNT(*) AS Field2, " +
                "SUM(CASE WHEN od.Status IN (1,2) THEN 1 ELSE 0 END) AS Field3, " +
                "SUM(CASE WHEN od.Status = 0 AND CancelNotifyStatus = 1 THEN 1 ELSE 0 END) AS Field4, " +
                "MAX(CASE WHEN od.KitchenCancelStatus = 1 THEN 1 ELSE 0 END) AS Field5, " +
                "od.Tid AS Field6," +
                "CASE WHEN MAX(CASE WHEN od.Status IN (1,2) THEN 1 ELSE 0 END) = 1 THEN 0  ELSE 1  END AS Field7 ";
            dmn.Table =
                "tblOrderDetails od " +
                "INNER JOIN tblOrderMaster om ON om.OrderMasterId = od.OrderMasterId";
            dmn.WhereNdGroupBy =
                "WHERE od.AddonStatus IN (0,1,2) AND om.Status <> 3 " +
                "GROUP BY od.OrderMasterId, od.Tid";

            var list = bll.GetSearchData("prdSearch", dmn);

            if (list == null || list.Count == 0)
                return;

            foreach (var r in list)
            {
                int tableId = Convert.ToInt32(r.Field6);

                orderStatusCache[tableId] = new TableOrderStatus
                {
                    TotalItems = Convert.ToInt32(r.Field2),
                    PendingCount = Convert.ToInt32(r.Field3),
                    CancelCount = Convert.ToInt32(r.Field4),
                    KitchenCancelled = Convert.ToInt32(r.Field5) == 1,
                    Tid = tableId,
                    Ordermid = Convert.ToInt32(r.Field1),
                    Checkoutstatus=Convert.ToInt32(r.Field7),
                };
            }
        }


        public void RefreshTableOrders()
        {
            try
            {
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
                Margin = new Padding(10),
                BackColor = Color.Transparent,
                Visible = false
            };

            TableLayoutPanel tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                BackColor = Color.White,
                AccessibleDescription = "0"
            };

            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 0));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 0));

            Label lblName = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(Font, FontStyle.Bold),
                ForeColor = Ordertablefontcolor,
                AccessibleDescription = "0"
            };

            Label lblNet = new Label
            {
                Dock = DockStyle.Fill,
                Visible = false,
                Font = new Font(Font, FontStyle.Bold),
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
            lblName.Click += tableClickEventHandler;
            lblNet.Click += tableClickEventHandler;

            return pnl;
        }


        #endregion

        #region Paging

        private void ApplyPaging()
        {
            totalPages = (int)Math.Ceiling(allTables.Count / (double)pageSize);

            currentPageTables = allTables
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            lblpagerinfo.Text =
                $"Page {currentPage} of {totalPages} | Total Records: {allTables.Count}";

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

                pnl.Visible = true;
            }

            flp_tableview.ResumeLayout();
        }


        #endregion

        #region UI Update

        public void UpdateTableUI()
        {
            foreach (var pnl in reusablePanels)
            {
                if (!pnl.Visible)
                    continue;

                int tableId = Convert.ToInt32(pnl.Tag);

                // Initialize BLL search
                BllSearch<DmnGridFields> bll = new BllSearch<DmnGridFields>();
                DmnSearch dmn = new DmnSearch();

                // Select the IsCustomerInformedCancel field
                dmn.Select = "IsCustomerInformedCancel AS Field1";
                dmn.Table = "tblTable_info";
                dmn.WhereNdGroupBy = "WHERE Tid = " + tableId + " AND Status = 1 AND IsCustomerInformedCancel = 1";
                var resStatus = bll.GetSearchData("prdSearch", dmn);
                int isCancelNotified = 0; 
                if (resStatus != null && resStatus.Count > 0)
                {
                    isCancelNotified =Convert.ToInt32(resStatus[0].Field1) ;
                }
                
                if (!tableUIRefs.TryGetValue(pnl.GetHashCode(), out var ui))
                    continue;

                // =========================
                // 🔁 HARD RESET (MANDATORY)
                // =========================
                ui.Inner.BackColor = Color.White;

                // NetAmount reset
                ui.NetAmount.Visible = false;
                ui.NetAmount.Text = "";
                ui.NetAmount.BackColor = Color.Transparent;

                // CancelCount reset (prevents ghost badge)
                ui.CancelCount.Visible = false;
                ui.CancelCount.Text = "";
                ui.CancelCount.BackColor = Color.Transparent;
                ui.CancelCount.ForeColor = Color.Black;
                ui.CancelCount.FlatStyle = FlatStyle.Standard;
                ui.CancelCount.Size = new Size(0, 0);
                ui.CancelCount.Location = new Point(-100, -100);
                ui.CancelCount.Region = null;

                ui.Container.AccessibleName = "0";
                ui.Inner.AccessibleName = "0";
                ui.TableName.AccessibleName = "0";
                ui.NetAmount.AccessibleName = "0";
                ui.CancelCount.AccessibleName = "0";

                // Layout reset
                ui.Inner.RowStyles[0].Height = 0;
                ui.Inner.RowStyles[1].Height = 60;
                ui.Inner.RowStyles[2].Height = 0;

                ui.TableName.TextAlign = ContentAlignment.MiddleCenter;


                // =========================
                // ❌ KITCHEN CANCEL full order
                // =========================
                if (isCancelNotified==1)
                {
                    ui.Inner.BackColor = CancelProduct_order;
                    ui.NetAmount.Text = "staff cancel ordfer";
                    ui.NetAmount.ForeColor = Color.White;
                    ui.NetAmount.BackColor = ReconfirmLabelBG;
                    ui.NetAmount.Font = new Font(Font, FontStyle.Bold);
                    ui.NetAmount.Visible = true;

                    ui.Inner.RowStyles[0].Height = 25;
                    ui.Inner.RowStyles[2].Height = 25;

                    ui.Container.AccessibleName = "1";
                    ui.Inner.AccessibleName = "1";
                    ui.TableName.AccessibleName = "1";
                    ui.NetAmount.AccessibleName = "1";
                    ui.CancelCount.AccessibleName = "1";

                    continue;
                }

                // =========================
                // 🚫 NO ORDER
                // =========================
                if (!orderStatusCache.TryGetValue(tableId, out var status))
                    continue;

                // =========================
                // ⚠ CANCEL COUNT BADGE
                // =========================
                if (status.CancelCount > 0)
                {
                    ui.Inner.BackColor = CancelProduct_order;

                    ui.CancelCount.Visible = true;
                    ui.CancelCount.Text = status.CancelCount.ToString();
                    ui.CancelCount.ForeColor = Color.White;
                    ui.CancelCount.BackColor = Color.Black;
                    ui.CancelCount.Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold);
                    ui.CancelCount.Size = new Size(22, 22);

                    ui.CancelCount.FlatStyle = FlatStyle.Flat;
                    ui.CancelCount.FlatAppearance.BorderSize = 0;

                    ui.CancelCount.Location = new Point(
                        ui.Container.Width - ui.CancelCount.Width - 5, 5);

                    // 🔵 Circular badge
                    var gp = new System.Drawing.Drawing2D.GraphicsPath();
                    gp.AddEllipse(0, 0, ui.CancelCount.Width, ui.CancelCount.Height);
                    ui.CancelCount.Region = new Region(gp);

                    ui.CancelCount.BringToFront();
                    continue;
                }

                // =========================
                // ❌ KITCHEN CANCEL → RECONFIRM
                // =========================
                if (status.KitchenCancelled)
                {
                    ui.Inner.BackColor = CancelProduct_order;
                    ui.NetAmount.Text = "Reconfirm Order";
                    ui.NetAmount.ForeColor = Color.White;
                    ui.NetAmount.BackColor = ReconfirmLabelBG;
                    ui.NetAmount.Font = new Font(Font, FontStyle.Bold);
                    ui.NetAmount.Visible = true;

                    ui.Inner.RowStyles[0].Height = 25;
                    ui.Inner.RowStyles[2].Height = 25;

                    continue;
                }

                //// =========================
                //// 🟦 RUNNING ORDER
                //// =========================
                if (status.PendingCount > 0)
                {
                    ui.Inner.BackColor = OrderColor;
                    continue;
                }

                // =========================
                // 🟩 CHECKOUT
                // =========================
                if (status.Checkoutstatus == 1)
                {
                    ui.Inner.BackColor = BillingColor;

                    ui.Inner.RowStyles[0].Height = 25;
                    ui.Inner.RowStyles[2].Height = 25;

                    ui.NetAmount.Text = "Checkout";
                    ui.NetAmount.ForeColor = Color.White;
                    ui.NetAmount.BackColor = CheckoutLabelBG;
                    ui.NetAmount.Font = new Font(Font, FontStyle.Bold);
                    ui.NetAmount.Visible = true;
                }

            }
            if(!AutoTimer.Enabled)
                AutoTimer.Start();
        }


        #endregion

        #region Timer

        private void AutoTimer_Tick(object sender, EventArgs e)
        {
            Process p = Process.GetCurrentProcess();
            lblmemory.Text =
                $"Memory: {(p.PrivateMemorySize64 / 1024d / 1024d):F2} MB";

            LoadOrderStatusCache();
            UpdateTableUI();
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

        #region Table Creation & Updation    
        /*
   private void AutoTimer_Tick(object sender, EventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            long memoryInBytes = currentProcess.PrivateMemorySize64;
            double memoryInMB = memoryInBytes / (1024.0 * 1024.0);
            lblmemory.Text = $"Memory: {memoryInMB:F2} MB";

            TableStaus(0);
        }
        private void BuildPanelLookup()
        {
            panelLookup.Clear();

            foreach (Control pnl in flp_tableview.Controls)
            {
                if (!panelLookup.ContainsKey(pnl.Name))
                    panelLookup[pnl.Name] = pnl;
            }
        }
        public class DoubleBufferedFlowLayoutPanel : FlowLayoutPanel
        {
            public DoubleBufferedFlowLayoutPanel()
            {
                this.DoubleBuffered = true;
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                this.UpdateStyles();
            }
        }

        public void LoadTableStaus(int shmode)
        {
            try
            {
                clsgen.Memoryrelease();

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                BllTableView<DmnGridFields> SearchDetails = new BllTableView<DmnGridFields>();
                DmnGridFields dmnSearchtabledata = new DmnGridFields();

                TableData = new List<DmnGridFields>();
                pagedatadata = new List<DmnGridFields>();

                dmnSearchtabledata.Field30 = Convert.ToString(FrmLogin.BookingInterwell);
                dmnSearchtabledata.Field28 = Convert.ToString(categoryId);
                dmnSearchtabledata.Field29 = Convert.ToString(SearchModetype);
                dmnSearchtabledata.Field25 = Convert.ToString(categorystatus);
                dmnSearchtabledata.Field24 = Convert.ToString(Pagecount);

                TableData = SearchDetails.serachreport("prdTableView", dmnSearchtabledata);
                totalPages = (int)Math.Ceiling((double)TableData.Count / pageSize);

                pagedatadata = TableData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {TableData.Count}";
                btnprev.Enabled = currentPage > 1;
                btnnext.Enabled = currentPage < totalPages;

                flp_tableview.SuspendLayout();

                foreach (Control ctrl in flp_tableview.Controls)
                {
                    ctrl.Dispose();
                }

                flp_tableview.Controls.Clear();

                flp_tableview.Refresh();
                flp_tableview.AutoScroll = true;
                flp_tableview.WrapContents = true;
                flp_tableview.FlowDirection = FlowDirection.LeftToRight;
                flp_tableview.BackColor = Color.Transparent;

                bool flag = false;
                for (int j = 0; j < pagedatadata.Count; j++)
                {
                    if (pagedatadata[j].EndDate != null && pagedatadata[j].Field7 != null &&
                        Convert.ToInt32(pagedatadata[j].Field7) > 1 && Convert.ToInt32(pagedatadata[j].Field10) == 0)
                    {
                        if (Convert.ToInt32(pagedatadata[j].Field7) > 0)
                        {
                            CheckexpireTable(Convert.ToInt32(pagedatadata[j].Field3), Convert.ToInt32(pagedatadata[j].Field7));
                            flag = true;
                        }
                    }
                }
                if (flag)
                {
                    TableData = SearchDetails.serachreport("prdTableView", dmnSearchtabledata);
                    totalPages = (int)Math.Ceiling((double)TableData.Count / pageSize);

                    pagedatadata = TableData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                    lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {TableData.Count}";
                    btnprev.Enabled = currentPage > 1;
                    btnnext.Enabled = currentPage < totalPages;
                }
                for (int i = 0; i < pagedatadata.Count; i++)
                {
                    // Parent panel
                    Panel pnl_container = new Panel
                    {
                        Name = "pnl_container" + pagedatadata[i].Field4,
                        BackColor = Color.Transparent,
                        //Margin = new Padding(10),
                        Size = new Size(tileW, tileH),
                        Tag = pagedatadata[i].Field3,
                        Margin = new Padding(10),
                        BorderStyle = BorderStyle.None,
                        Width = 140,
                        Height = 110,
                        AccessibleName = "0",
                    };

                    TableLayoutPanel tlp_table_inner = new TableLayoutPanel
                    {
                        Name = "tlp_table_inner" + pagedatadata[i].Field4,
                        Dock = DockStyle.Fill,
                        ColumnCount = 1,
                        RowCount = 2,
                        Tag = pagedatadata[i].Field3,
                        BackColor = Color.Yellow,
                        Margin = new Padding(0),
                        Padding = new Padding(0),
                        AccessibleName = "0",
                    };
                    tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 70));  // Table Name
                    tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 30));  // Net Amount

                    Label lbltablename = new Label
                    {
                        Name = "lbtablename" + pagedatadata[i].Field4,
                        Tag = pagedatadata[i].Field3,
                        Text = pagedatadata[i].Field4,
                        BackColor = Color.Transparent,
                        Font = new Font(this.Font, FontStyle.Bold),
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Convert.ToInt32(pagedatadata[i].Field5) == 1 ? Ordertablefontcolor : Defaultfontcolor,
                        AccessibleName = "0",
                    };

                    Label netamountlabel = new Label
                    {
                        Name = "lb_netamount" + pagedatadata[i].Field4,
                        Text = "Checkout",
                        Tag = pagedatadata[i].Field3,
                        ForeColor = Netamountfontcolor,
                        BackColor = Color.Transparent,
                        Font = new Font(this.Font, FontStyle.Bold),
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Visible = false,
                        AccessibleName = "0",
                    };

                    tlp_table_inner.AccessibleDescription = "0";
                    lbltablename.AccessibleDescription = "0";
                    netamountlabel.AccessibleDescription = "0";

                    // Add Labels into TLP
                    tlp_table_inner.Controls.Add(lbltablename, 0, 0);
                    tlp_table_inner.Controls.Add(netamountlabel, 0, 1);

                    // Add TLP to Panel
                    pnl_container.Controls.Add(tlp_table_inner);

                    Button btnCancelCount = new Button
                    {
                        Name = "btnCancelCount" + pagedatadata[i].Field4,
                        Width = 22,
                        Height = 22,
                        BackColor = Color.White,
                        ForeColor = Color.Black,
                        Font = new Font(this.Font, FontStyle.Bold),
                        FlatStyle = FlatStyle.Flat,
                        Text = "5",
                        Visible = false,
                        Anchor = AnchorStyles.Top | AnchorStyles.Right,
                        Cursor = Cursors.Hand,
                        UseCompatibleTextRendering = true,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    btnCancelCount.FlatAppearance.BorderSize = 0;
                    // Make round button
                    using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        gp.AddEllipse(0, 0, btnCancelCount.Width, btnCancelCount.Height);
                        btnCancelCount.Region = new Region(gp);
                    }
                    pnl_container.Controls.Add(btnCancelCount);
                    btnCancelCount.Location = new Point(pnl_container.Width - btnCancelCount.Width - 5, 5);
                    btnCancelCount.BringToFront();
                    bool ordstatus = flag;
                    #region Normal Order 
                    if (Convert.ToInt32(pagedatadata[i].Field5) == 1)//not reservation order 
                    {
                        BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                        dmnSearch.Select = "  SUM(CASE WHEN Status = 3 AND AddonStatus IN (1,0) THEN 1 ELSE 0 END) AS Field1,
        SUM(CASE WHEN Status = 0 AND AddonStatus IN (1,0) AND CancelNotifyStatus = 1 THEN 1 ELSE 0 END) AS Field2";//;= SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS
                        dmnSearch.Table = "tblOrderDetails";
                        dmnSearch.WhereNdGroupBy = "WHERE OrderMasterId = " + pagedatadata[i].Field6;
                        dmnSearch.OrderBy = "";
                        var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                        if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                        {
                            ordstatus = true;
                            netamountlabel.Visible = true;
                            tlp_table_inner.BackColor = BillingColor;
                            tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                            tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.AutoSize);
                            netamountlabel.Dock = DockStyle.Fill;
                            lbltablename.Dock = DockStyle.Fill;
                            lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                            netamountlabel.TextAlign = ContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            netamountlabel.Visible = false;
                            tlp_table_inner.BackColor = OrderColor;
                            tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                            tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                            lbltablename.Dock = DockStyle.Fill;
                            lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                        }
                        if (Convert.ToInt32(gridSource_order[0].Field2) > 0)
                        {
                            btnCancelCount.Visible = true;
                            tlp_table_inner.BackColor = CancelProduct_order;
                            btnCancelCount.Text = gridSource_order[0].Field2;
                        }
                        else
                        {
                            btnCancelCount.Visible = false;
                            if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                                tlp_table_inner.BackColor = BillingColor;
                            else
                                tlp_table_inner.BackColor = OrderColor;
                        }
                    }
                    #endregion

                    #region Rservation Bokking Order
                    else//reservation booking order
                    {
                        if (pagedatadata[i].EndDate != null && pagedatadata[i].Field7 != null)
                        {
                            #region order sent to kitchen ( running or complete )
                            if (Convert.ToInt32(pagedatadata[i].Field6) > 0)//order sent to kitchen ( running or complete )
                            {
                                BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                                dmnSearch.Select = "COUNT(*)  Field1";//;= SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS
                                dmnSearch.Table = "tblOrderDetails";
                                dmnSearch.WhereNdGroupBy = "WHERE status=3 and AddonStatus in(1,0) and OrderMasterId = " + pagedatadata[i].Field6;
                                dmnSearch.OrderBy = "";
                                var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                                if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                                {
                                    netamountlabel.Visible = true;
                                    tlp_table_inner.BackColor = BillingColor;
                                    tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                                    tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.AutoSize);
                                    netamountlabel.Dock = DockStyle.Fill;
                                    lbltablename.Dock = DockStyle.Fill;
                                    lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                                    netamountlabel.TextAlign = ContentAlignment.MiddleCenter;
                                }
                                else
                                {
                                    netamountlabel.Visible = false;
                                    tlp_table_inner.BackColor = OrderColor;
                                    tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                    tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                    lbltablename.Dock = DockStyle.Fill;
                                    lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                                }

                                lbltablename.ForeColor = Ordertablefontcolor;
                                tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + pagedatadata[i].Field7;
                                lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + pagedatadata[i].Field7;
                            }
                            #endregion

                            #region order not  sent to kitchen reservation or overtime 
                            else //order not  sent to kitchen reservation or overtime 
                            {
                                DateTime modifiedEndDate_before = pagedatadata[i].EndDate.AddMinutes(-Convert.ToDouble(FrmLogin.BookingInterwell));
                                DateTime modifiedEndDate_after = pagedatadata[i].EndDate;//.AddMinutes(Convert.ToDouble(FrmLogin.BookingInterwell))
                                DateTime ExpdateNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                DateTime Bookdate = Convert.ToDateTime(pagedatadata[i].EndDate.ToString("yyyy-MM-dd HH:mm"));
                                if (ExpdateNow >= modifiedEndDate_before && ExpdateNow <= modifiedEndDate_after)
                                {

                                    tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + pagedatadata[i].Field7;
                                    lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + pagedatadata[i].Field7;
                                    tlp_table_inner.BackColor = ReservationColor;

                                    tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                    tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                    lbltablename.Dock = DockStyle.Fill;
                                    lbltablename.TextAlign = ContentAlignment.MiddleCenter;

                                }
                                else if (ExpdateNow > Bookdate)// set expired  icon .AddMinutes(FrmLogin.BookingInterwell))
                                {
                                    tlp_table_inner.BackColor = ExpiredColor;
                                    tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + pagedatadata[i].Field7;
                                    lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + pagedatadata[i].Field7;

                                    tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                    tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                    lbltablename.Dock = DockStyle.Fill;
                                    lbltablename.TextAlign = ContentAlignment.MiddleCenter;

                                }
                            }
                            #endregion

                        }
                        else
                        {
                            netamountlabel.Visible = false;
                            tlp_table_inner.BackColor = DefaultColor;
                            tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                            tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                            lbltablename.Dock = DockStyle.Fill;
                            lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                            tlp_table_inner.AccessibleDescription = "0";
                            lbltablename.AccessibleDescription = "0";
                            netamountlabel.AccessibleDescription = "0";
                        }
                    }
                    #endregion

                    if (Convert.ToInt32(pagedatadata[i].Field9) == 1)
                    {
                        tlp_table_inner.AccessibleName = "1";
                        lbltablename.AccessibleName = "1";
                        netamountlabel.AccessibleName = "1";

                        tlp_table_inner.BackColor = CancelProduct_order;
                        netamountlabel.TextAlign = ContentAlignment.TopLeft;
                        netamountlabel.Text = "Cancelled by Kitchen";
                        tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 50);
                        tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 50);
                        netamountlabel.Visible = true;
                    }
                    else
                    {
                        tlp_table_inner.AccessibleName = "0";
                        lbltablename.AccessibleName = "0";
                        netamountlabel.AccessibleName = "0";

                        //netamountlabel.Visible = false;
                        //tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                        //tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                        //netamountlabel.TextAlign = ContentAlignment.MiddleCenter;

                        //if (ordstatus)
                        //    tlp_table_inner.BackColor = BillingColor;
                        //else
                        //    tlp_table_inner.BackColor = OrderColor;
                    }

                    lbltablename.Click += tableClickEventHandler;
                    pnl_container.Click += tableClickEventHandler;
                    netamountlabel.Click += tableClickEventHandler;

                    UIHelper.RoundCorners(pnl_container, 25, null);
                    flp_tableview.Controls.Add(pnl_container);
                }

                flp_tableview.ResumeLayout();
                clsgen.Memoryrelease();

               initTimer();
            }
            catch (Exception ex)
            {
                clsgen.LogException("TableView page  LoadTableStaus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }

        public void TableStaus(int serachmode)
        {
            try
            {
                clsgen.Memoryrelease();
                AutoTimer.Stop();
                flp_tableview.SuspendLayout();

                TableData = new List<DmnGridFields>();
                pagedatadata = new List<DmnGridFields>();

                DmnSearch dmnSearch = new DmnSearch();
                BllTableView<DmnGridFields> SearchDetails = new BllTableView<DmnGridFields>();
                DmnGridFields dmnSearchtabledata = new DmnGridFields();
                dmnSearchtabledata.Field30 = Convert.ToString(FrmLogin.BookingInterwell);
                dmnSearchtabledata.Field28 = Convert.ToString(categoryId);
                dmnSearchtabledata.Field29 = Convert.ToString(SearchModetype);
                dmnSearchtabledata.Field25 = Convert.ToString(categorystatus);
                dmnSearchtabledata.Field24 = Convert.ToString(Pagecount);

                TableData = SearchDetails.serachreport("prdTableView", dmnSearchtabledata);
                totalPages = (int)Math.Ceiling((double)TableData.Count / pageSize);

                pagedatadata = TableData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {TableData.Count}";
                btnprev.Enabled = currentPage > 1;
                btnnext.Enabled = currentPage < totalPages;

                //if (categoryId > 0)
                //{
                //    foreach (var pnl in panelLookup.Values)
                //        if (!pnl.Visible) pnl.Visible = true;
                //}
                //else
                //{
                //    foreach (var pnl in panelLookup.Values)
                //        if (pnl.Visible) pnl.Visible = false;
                //}


                //foreach (var row in pagedatadata)
                //{
                //    string pname = "pnl_container" + row.Field4;

                //    if (panelLookup.TryGetValue(pname, out Control pnl))
                //    {
                //        int tagValue = Convert.ToInt32(pnl.Tag);
                //        bool shouldShow = (tagValue == categoryId || categoryId > 0);
                //        if (pnl.Visible != shouldShow)
                //            pnl.Visible = shouldShow;
                //    }
                //}
                LoadPagedata();
                flp_tableview.ResumeLayout();
           
            }
            catch (Exception ex)
            {
                clsgen.LogException("TableView page  TableStaus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }

        public void LoadPagedata()
        {
            try
            {
                DmnSearch dmnSearch = new DmnSearch();
                for (int i = 0; i < pagedatadata.Count; i++)
                {
                    Panel pnlcont = this.Controls.Find("pnl_container" + pagedatadata[i].Field4, true).FirstOrDefault() as Panel;
                    TableLayoutPanel tlp_table_inner = this.Controls.Find("tlp_table_inner" + pagedatadata[i].Field4, true).FirstOrDefault() as TableLayoutPanel;
                    Label lbltablename = this.Controls.Find("lbtablename" + pagedatadata[i].Field4, true).FirstOrDefault() as Label;
                    Label netamountlabel = this.Controls.Find("lb_netamount" + pagedatadata[i].Field4, true).FirstOrDefault() as Label;
                    Button btnCancelCount = this.Controls.Find("btnCancelCount" + pagedatadata[i].Field4, true).FirstOrDefault() as Button;
                    if (tlp_table_inner == null)
                        return;
                    if(lbltablename!=null)
                        lbltablename.ForeColor = Convert.ToInt32(pagedatadata[i].Field5) == 1 ? Ordertablefontcolor : Defaultfontcolor;

                    tlp_table_inner.AccessibleDescription = "0";
                    lbltablename.AccessibleDescription = "0";
                    netamountlabel.AccessibleDescription = "0";

                    // Update NetAmount & color logic for normal/reservation orders
                    bool ordstatus = false;
                    #region Normal Order 
                    if (Convert.ToInt32(pagedatadata[i].Field5) == 1)//not reservation order 
                    {
                        BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                        dmnSearch.Select = "  SUM(CASE WHEN Status = 3 AND AddonStatus IN (1,0) THEN 1 ELSE 0 END) AS Field1,SUM(CASE WHEN Status = 0 AND AddonStatus IN (1,0) AND CancelNotifyStatus = 1 THEN 1 ELSE 0 END) AS Field2";//;= SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS
                        dmnSearch.Table = "tblOrderDetails";
                        dmnSearch.WhereNdGroupBy = "WHERE OrderMasterId = " + pagedatadata[i].Field6;
                        dmnSearch.OrderBy = "";
                        var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                        if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                        {
                            ordstatus = true;
                            netamountlabel.Visible = true;
                            tlp_table_inner.BackColor = BillingColor;
                            tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                            tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.AutoSize);
                            netamountlabel.Dock = DockStyle.Fill;
                            lbltablename.Dock = DockStyle.Fill;
                            lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                            netamountlabel.TextAlign = ContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            netamountlabel.Visible = false;
                            tlp_table_inner.BackColor = OrderColor;
                            tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                            tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                            lbltablename.Dock = DockStyle.Fill;
                            lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                        }
                        if (Convert.ToInt32(gridSource_order[0].Field2) > 0)
                        {
                            btnCancelCount.Visible = true;
                            tlp_table_inner.BackColor = CancelProduct_order;
                            btnCancelCount.Text = gridSource_order[0].Field2;
                        }
                        else
                        {
                            btnCancelCount.Visible = false;
                            if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                                tlp_table_inner.BackColor = BillingColor;
                            else
                                tlp_table_inner.BackColor = OrderColor;
                        }

              

                    }
                    #endregion

                    #region Rservation Bokking Order
                    else if (Convert.ToInt32(pagedatadata[i].Field5) == 0 && (Convert.ToInt32(pagedatadata[i].Field7) > 0))//reservation booking order
                    {
                        if (pagedatadata[i].EndDate != null && pagedatadata[i].Field7 != null)
                        {
                            #region order sent to kitchen ( running or complete )
                            if (Convert.ToInt32(pagedatadata[i].Field6) > 0)//order sent to kitchen ( running or complete )
                            {
                                BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                                dmnSearch.Select = " COUNT(*)  Field1";//= SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS
                                dmnSearch.Table = "tblOrderDetails";
                                dmnSearch.WhereNdGroupBy = "WHERE status=3 and OrderMasterId = " + pagedatadata[i].Field6;
                                dmnSearch.OrderBy = "";
                                var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);
                                if (gridSource_order.Count > 0)
                                {
                                    if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                                    {
                                        netamountlabel.Visible = true;
                                        tlp_table_inner.BackColor = BillingColor;
                                        tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                                        tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.AutoSize);
                                        netamountlabel.Dock = DockStyle.Fill;
                                        lbltablename.Dock = DockStyle.Fill;
                                        lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                                        netamountlabel.TextAlign = ContentAlignment.MiddleCenter;
                                        netamountlabel.Text = "\u20B9" + Convert.ToString(pagedatadata[i].Field8);
                                    }
                                    else
                                    {
                                        netamountlabel.Visible = false;
                                        tlp_table_inner.BackColor = OrderColor;
                                        tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                        tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                        lbltablename.Dock = DockStyle.Fill;
                                        lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                                    }

                                    lbltablename.ForeColor = Ordertablefontcolor;
                                    tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + pagedatadata[i].Field7;
                                    lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + pagedatadata[i].Field7;
                                    netamountlabel.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + pagedatadata[i].Field7;
                                }
                            }
                            #endregion

                            #region order not  sent to kitchen reservation or overtime 
                            else //order not  sent to kitchen reservation or overtime 
                            {
                                DateTime modifiedEndDate_before = pagedatadata[i].EndDate.AddMinutes(-Convert.ToDouble(FrmLogin.BookingInterwell));
                                DateTime modifiedEndDate_after = pagedatadata[i].EndDate;//.AddMinutes(Convert.ToDouble(FrmLogin.BookingInterwell));
                                DateTime ExpdateNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                DateTime Bookdate = Convert.ToDateTime(pagedatadata[i].EndDate.ToString("yyyy-MM-dd HH:mm"));
                                if (ExpdateNow >= modifiedEndDate_before && ExpdateNow <= modifiedEndDate_after)
                                {
                                    tlp_table_inner.BackColor = ReservationColor;
                                    tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + pagedatadata[i].Field7;
                                    lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + pagedatadata[i].Field7;

                                    tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                    tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                    lbltablename.Dock = DockStyle.Fill;
                                    lbltablename.TextAlign = ContentAlignment.MiddleCenter;

                                }
                                else if (ExpdateNow >= Bookdate)// set expired  icon .AddMinutes(FrmLogin.BookingInterwell))
                                {

                                    tlp_table_inner.BackColor = ExpiredColor;
                                    tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + pagedatadata[i].Field7;
                                    lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + pagedatadata[i].Field7;
                                    tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                    tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                    lbltablename.Dock = DockStyle.Fill;
                                    lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            tlp_table_inner.BackColor = DefaultColor;
                            tlp_table_inner.AccessibleDescription = "0";
                            lbltablename.AccessibleDescription = "0";

                            netamountlabel.Text = "";
                            netamountlabel.Visible = false;
                            tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                            tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                            lbltablename.Dock = DockStyle.Fill;
                            lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                            tlp_table_inner.AccessibleDescription = "0";
                            lbltablename.AccessibleDescription = "0";

                        }
                    }
                    #endregion

                    else
                    {
                        netamountlabel.Text = "";
                        netamountlabel.Visible = false;

                        tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                        tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                        lbltablename.Dock = DockStyle.Fill;
                        lbltablename.TextAlign = ContentAlignment.MiddleCenter;

                        tlp_table_inner.BackColor = DefaultColor;
                        tlp_table_inner.AccessibleDescription = "0";
                        lbltablename.AccessibleDescription = "0";
                    }

                    if (Convert.ToInt32(pagedatadata[i].Field9) == 1)
                    {
                        tlp_table_inner.BackColor = CancelProduct_order;
                        netamountlabel.TextAlign = ContentAlignment.TopLeft;
                        netamountlabel.Text = "Cancelled by Kitchen";
                        tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 50);
                        tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 50);
                        netamountlabel.Visible = true;

                        lbltablename.AccessibleName ="1";
                        pnlcont.AccessibleName = "1";
                        netamountlabel.AccessibleName = "1";
                    }
                    else
                    {
                        lbltablename.AccessibleName = "0";
                        pnlcont.AccessibleName = "0";
                        netamountlabel.AccessibleName = "0";

                        netamountlabel.Visible = false;
                        tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                        tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                        netamountlabel.TextAlign = ContentAlignment.MiddleCenter;

                        //if (ordstatus)
                        //    tlp_table_inner.BackColor = BillingColor;
                        //else
                        //    tlp_table_inner.BackColor = OrderColor;
                    }
                }

                initTimer();
            }
            catch (Exception ex)
            {
                clsgen.LogException("TableView page  LoadPagedata()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }
        */
        #endregion

        private void DisposeControlRecursive(Control ctrl)
        {
            foreach (Control child in ctrl.Controls)
            {
                DisposeControlRecursive(child);
            }

            if (ctrl is PictureBox pb && pb.Image != null)
            {
                pb.Image.Dispose();
                pb.Image = null;
            }

            ctrl.Dispose();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            AutoTimer?.Stop();
            AutoTimer?.Dispose();
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
                CheckedtableNames.Clear();
                Control clickedControl = sender as Control;
                string textvalue = clickedControl.AccessibleDescription;
                int Cancelorderstatus = Convert.ToInt32(clickedControl.AccessibleName);
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
                    int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                    int screenHeight = Screen.PrimaryScreen.Bounds.Height;
                    if (Btype == Convert.ToInt32(DashboardTableType.Normalorder))
                    {
                        if (System.Windows.Forms.Application.OpenForms["FrmOrder"] != null)
                        {
                            FrmOrder _instanceFrmOrder = Application.OpenForms["FrmOrder"] as FrmOrder;
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
                                using (FrmOrderConsole memtest = new FrmOrderConsole(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Normalorder), 0, CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage), Cancelorderstatus))
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
                            using (FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid), CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage)))
                            {
                                _instanceFrmOrder.ShowDialog();
                            }

                        }
                    }
                    else if (Btype == Convert.ToInt32(DashboardTableType.Reverse_Table))
                    {
                        using (FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid), CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage)))
                        {
                            _instanceFrmOrder.ShowDialog();
                        }
                    }
                }
                clsgen.Memoryrelease();
            }
            catch (Exception ex)
            {
                clsgen.LogException("TableView page  tableClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }


        #region No Refrence
        private void tlp_category_MouseHover(object sender, EventArgs e)
        {
            try
            {
                TableLayoutPanel tlpcate = sender as TableLayoutPanel;
                string tlpName = "tlp_category_com_" + tlpcate.Tag;
                TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
                if (tlp != null)
                {
                    tlp.BackColor = Color.LightBlue;
                    //_toolTip.SetToolTip(tlp, tlpcate.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("TableView page  tlp_category_MouseHover()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }
        private void tlp_category_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                TableLayoutPanel tlpcate = sender as TableLayoutPanel;
                string tlpName = "tlp_category_" + tlpcate.Tag;
                // Find the TableLayoutPanel control by name
                TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
                if (tlp != null)
                {
                    tlp.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("TableView page  tlp_category_MouseLeave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }

        }
        private void pictureBox_com_MouseHover(object sender, EventArgs e)
        {
            try
            {
                PictureBox pic = sender as PictureBox;
                string tlpName = "tlp_com" + pic.Tag;
                // Find the TableLayoutPanel control by name
                TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
                if (tlp != null)
                {
                    tlp.BackColor = Color.LightBlue;
                    tlp.BorderStyle = BorderStyle.FixedSingle;
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("TableView page  pictureBox_com_MouseHover()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }

        }
        private void pictureBox_com_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                PictureBox pic = sender as PictureBox;
                string tlpName = "tlp_com" + pic.Tag;
                // Find the TableLayoutPanel control by name
                TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
                if (tlp != null)
                {
                    tlp.BackColor = Color.White;
                    tlp.BorderStyle = BorderStyle.None;
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("TableView page  pictureBox_com_MouseLeave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }

        }
        private void pictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                PictureBox pic = sender as PictureBox;
                string tlpName = "tlp" + pic.Tag;
                // Find the TableLayoutPanel control by name
                TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
                if (tlp != null)
                {
                    tlp.BackColor = Color.LightBlue;
                    tlp.BorderStyle = BorderStyle.FixedSingle;
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("TableView page  pictureBox_MouseHover()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }

        }
        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                PictureBox pic = sender as PictureBox;
                string tlpName = "tlp" + pic.Tag;
                // Find the TableLayoutPanel control by name
                TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
                if (tlp != null)
                {
                    tlp.BackColor = Color.White;
                    tlp.BorderStyle = BorderStyle.None;
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("TableView page  pictureBox_MouseLeave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }

        }
        public void FillTabelData()
        {
            try
            {
                BllSearch<DmnTableinfo> bllSearch = new BllSearch<DmnTableinfo>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblTable_info";
                dmnSearch.WhereNdGroupBy = "where Status=1";
                dmnSearch.OrderBy = "";
                TableDataList = bllSearch.GetSearchData("prdSearch", dmnSearch);
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("TableView page  FillTabelData()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }
        #endregion

        private void FrmTableView_FormClosing(object sender, FormClosingEventArgs e)
        {
            clsgen.Memoryrelease();
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                AppSession.UnregisterForm(this);
            }
        }

        private void btn_showmenu_Click(object sender, EventArgs e)
        {
            clsgen.Memoryrelease();

            flp_sidemenu.Visible = !flp_sidemenu.Visible;
            if (!flp_sidemenu.Visible)
                return;

            // 1. Dispose old controls safely
            foreach (Control ctrl in flp_sidemenu.Controls)
            {
                menuTooltip.SetToolTip(ctrl, null);  // unlink tooltip
                ctrl.Dispose();
            }
            flp_sidemenu.Controls.Clear();

            flp_sidemenu.AutoScroll = false;
            flp_sidemenu.Refresh();

            // 2. Setup panel
            flp_sidemenu.FlowDirection = FlowDirection.LeftToRight;
            flp_sidemenu.Width = btn_showmenu.Width;
            flp_sidemenu.Height = (int)(this.Height * 0.5);

            flp_sidemenu.AutoScroll = true;
            flp_sidemenu.HorizontalScroll.Enabled = true;
            flp_sidemenu.HorizontalScroll.Visible = true;
            flp_sidemenu.VerticalScroll.Enabled = false;
            flp_sidemenu.VerticalScroll.Visible = false;

            // 3. Positioning
            Point btnScreen = btn_showmenu.PointToScreen(Point.Empty);
            int desiredX = btnScreen.X + (btn_showmenu.Width / 2) - (flp_sidemenu.Width / 2);
            int desiredY = btnScreen.Y + btn_showmenu.Height + 20;

            Rectangle screenBounds = Screen.FromControl(btn_showmenu).WorkingArea;

            if (desiredX < screenBounds.Left)
                desiredX = screenBounds.Left;
            else if (desiredX + flp_sidemenu.Width > screenBounds.Right)
                desiredX = screenBounds.Right - flp_sidemenu.Width;

            if (desiredY + flp_sidemenu.Height > screenBounds.Bottom)
            {
                desiredY = btnScreen.Y - flp_sidemenu.Height - 10;
                if (desiredY < screenBounds.Top)
                    desiredY = screenBounds.Top;
            }

            Point clientPos = this.PointToClient(new Point(desiredX, desiredY));
            flp_sidemenu.Location = clientPos;

            // 4. Button sizing
            int bheight = (int)(btn_showmenu.Height * 0.9);
            int bWidth = btn_showmenu.Width - 25;

            // 5. Fetch categories
            BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
            DmnSearch objSearch = new DmnSearch
            {
                Select = "Fcid,CategoryName",
                Table = "tblCategory_FTI",
                WhereNdGroupBy = "WHERE Status = 1 AND CategoryType = 1",
                OrderBy = ""
            };

            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);

            // 6. ADD "ALL" BUTTON
            Button btnAll = new Button
            {
                Name = "btnSideMenu_All",
                Tag = 0,
                Text = "All",
                Width = bWidth,
                Height = bheight,
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat,
                Font = AppFonts.GetFont(10),
                BackgroundImage = (Image)menuButtonImage.Clone(), // SAFE IMAGE
                BackgroundImageLayout = ImageLayout.Stretch,
            };
            btnAll.FlatAppearance.BorderSize = 0;
            btnAll.Click += SideMenuClick;
            menuTooltip.SetToolTip(btnAll, "All");
            flp_sidemenu.Controls.Add(btnAll);

            // 7. ADD CATEGORY BUTTONS
            if (itemSource != null && itemSource.Count > 0)
            {
                foreach (var item in itemSource)
                {
                    Button btn = new Button
                    {
                        Name = "btnSideMenu_" + item.Fcid,
                        Tag = item.Fcid,
                        Text = item.CategoryName,
                        Width = bWidth,
                        Height = bheight,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.Transparent,
                        ForeColor = Color.White,
                        Font = AppFonts.GetFont(10),
                        BackgroundImage = (Image)menuButtonImage.Clone(),
                        BackgroundImageLayout = ImageLayout.Stretch,
                    };

                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += SideMenuClick;

                    menuTooltip.SetToolTip(btn, item.CategoryName);

                    flp_sidemenu.Controls.Add(btn);
                }
            }

            // 8. Round Corners (SAFE)
            UIHelper.RoundCorners(flp_sidemenu, 25);
        }

        private void SideMenuClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            categoryId = Convert.ToInt32(btn.Tag);
            categorystatus = 1;

            flp_sidemenu.Visible = false;

            AutoTimer.Stop();
            LoadTableStatus(0);
        }


        private void FrmTableView_Resize(object sender, EventArgs e)
        {
            // Auto size
            int containerWidth = flp_tableview.ClientSize.Width;

             PRODUCT_WIDTH_PERCENT = 0.80f;
             PRODUCT_HEIGHT_PERCENT = 0.80f;

             tileW = (int)(containerWidth * PRODUCT_WIDTH_PERCENT / maxColumns);
             tileH = (int)(tileW * PRODUCT_HEIGHT_PERCENT);
        }


        private void btnclose_Click(object sender, EventArgs e)
        {
            FrmDashboard.Instance.Menuhome.PerformClick();
            this.Close();
          
        }


        private void btn_refresh_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            categoryId = 0;
            categorystatus = 0;
            SearchModetype =0;
            flp_sidemenu.Visible = false;
            AutoTimer.Stop();
            LoadTableStatus(0);
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                clsgen.Memoryrelease();

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                BllTableView<DmnGridFields> SearchDetails = new BllTableView<DmnGridFields>();
                DmnGridFields dmnSearchtabledata = new DmnGridFields();
                dmnSearchtabledata.Field30 = Convert.ToString(FrmLogin.BookingInterwell);
                dmnSearchtabledata.Field29 = Convert.ToString(SearchModetype);
                dmnSearchtabledata.Field28 = Convert.ToString(categoryId);
                dmnSearchtabledata.Field26 = Convert.ToString("1");
                dmnSearchtabledata.Field27 = Convert.ToString(txttablecode_search.Text);
                var gridSource = SearchDetails.serachreport("prdTableView", dmnSearchtabledata);

                bool flag = false;
                for (int j = 0; j < gridSource.Count; j++)
                {
                    if (gridSource[j].EndDate != null && gridSource[j].Field7 != null &&
                        Convert.ToInt32(gridSource[j].Field7) > 1 && Convert.ToInt32(gridSource[j].Field10) == 0)
                    {
                        if (Convert.ToInt32(gridSource[j].Field7) > 0)
                        {
                            CheckexpireTable(Convert.ToInt32(gridSource[j].Field3), Convert.ToInt32(gridSource[j].Field7));
                            flag = true;
                        }
                    }
                }
                if (flag)
                    gridSource = SearchDetails.serachreport("prdTableView", dmnSearchtabledata);

                if (gridSource.Count == 0)
                    return;

                flp_tableview.SuspendLayout();
                foreach (Control ctrl in flp_tableview.Controls)
                {
                    ctrl.Dispose();
                }

                flp_tableview.Controls.Clear();

                flp_tableview.Refresh();
                flp_tableview.AutoScroll = true;
                flp_tableview.WrapContents = true;
                flp_tableview.FlowDirection = FlowDirection.LeftToRight;
                flp_tableview.BackColor = Color.Transparent;

                for (int i = 0; i < gridSource.Count; i++)
                {
                    // Parent panel
                    Panel pnl_container = new Panel
                    {
                        Name = "pnl_container" + gridSource[i].Field4,
                        BackColor = Color.Transparent,
                        //Margin = new Padding(10),
                        Size = new Size(tileW, tileH),
                        Margin = new Padding(10),
                        BorderStyle = BorderStyle.None,
                        Width = 140,
                        Height = 110,
                    };

                    TableLayoutPanel tlp_table_inner = new TableLayoutPanel
                    {
                        Name = "tlp_table_inner" + gridSource[i].Field4,
                        Dock = DockStyle.Fill,
                        ColumnCount = 1,
                        RowCount = 2,
                        BackColor = Color.Yellow,
                        Margin = new Padding(0),
                        Padding = new Padding(0),
                    };
                    tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 70));  // Table Name
                    tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 30));  // Net Amount

                    Label lbltablename = new Label
                    {
                        Name = "lbtablename" + gridSource[i].Field4,
                        Tag = gridSource[i].Field3,
                        Text = gridSource[i].Field4,
                        BackColor = Color.Transparent,
                        Font = AppFonts.GetFont(8),
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Convert.ToInt32(gridSource[i].Field5) == 1 ? Ordertablefontcolor : Defaultfontcolor,
                    };

                    Label netamountlabel = new Label
                    {
                        Name = "lb_netamount" + gridSource[i].Field4,
                        Text = "Checkout",
                        Tag = gridSource[i].Field3,
                        ForeColor = Netamountfontcolor,
                        BackColor = Color.Transparent,
                        Font = AppFonts.GetFont(10),
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Visible = false
                    };

                    // Add Labels into TLP
                    tlp_table_inner.Controls.Add(lbltablename, 0, 0);
                    tlp_table_inner.Controls.Add(netamountlabel, 0, 1);

                    // Add TLP to Panel
                    pnl_container.Controls.Add(tlp_table_inner);

                    Button btnCancelCount = new Button
                    {
                        Name = "btnCancelCount" + gridSource[i].Field4,
                        Width = 22,
                        Height = 22,
                        BackColor = Color.White,
                        ForeColor = Color.Black,
                        Font = AppFonts.GetFont(3),
                        FlatStyle = FlatStyle.Flat,
                        Text = "5",
                        Visible = false,
                        Anchor = AnchorStyles.Top | AnchorStyles.Right,
                        Cursor = Cursors.Hand,
                        UseCompatibleTextRendering = true,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    btnCancelCount.FlatAppearance.BorderSize = 0;

                    // Make round button
                    using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        gp.AddEllipse(0, 0, btnCancelCount.Width, btnCancelCount.Height);
                        btnCancelCount.Region = new Region(gp);
                    }
                    pnl_container.Controls.Add(btnCancelCount);
                    btnCancelCount.Location = new Point(pnl_container.Width - btnCancelCount.Width - 5, 5);
                    btnCancelCount.BringToFront();

                    #region Normal Order 
                    if (Convert.ToInt32(gridSource[i].Field5) == 1)//not reservation order 
                    {
                        BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                        dmnSearch.Select = "  SUM(CASE WHEN Status = 3 AND AddonStatus IN (1,0) THEN 1 ELSE 0 END) AS Field1,SUM(CASE WHEN Status = 0 AND AddonStatus IN (1,0) AND CancelNotifyStatus = 1 THEN 1 ELSE 0 END) AS Field2";//;= SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS
                        dmnSearch.Table = "tblOrderDetails";
                        dmnSearch.WhereNdGroupBy = "WHERE OrderMasterId = " + gridSource[i].Field6;
                        dmnSearch.OrderBy = "";
                        var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                        if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                        {
                            netamountlabel.Visible = true;
                            tlp_table_inner.BackColor = BillingColor;
                            tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                            tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.AutoSize);
                            netamountlabel.Dock = DockStyle.Fill;
                            lbltablename.Dock = DockStyle.Fill;
                            lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                            netamountlabel.TextAlign = ContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            netamountlabel.Visible = false;
                            tlp_table_inner.BackColor = OrderColor;
                            tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                            tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                            lbltablename.Dock = DockStyle.Fill;
                            lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                        }
                        if (Convert.ToInt32(gridSource_order[0].Field2) > 0)
                        {
                            btnCancelCount.Visible = true;
                            tlp_table_inner.BackColor = CancelProduct_order;
                            btnCancelCount.Text = gridSource_order[0].Field2;
                        }
                        else
                        {
                            btnCancelCount.Visible = false;
                            if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                                tlp_table_inner.BackColor = BillingColor;
                            else
                                tlp_table_inner.BackColor = OrderColor;
                        }

                    }
                    #endregion

                    #region Rservation Bokking Order
                    else//reservation booking order
                    {
                        if (gridSource[i].EndDate != null && gridSource[i].Field7 != null)
                        {
                            #region order sent to kitchen ( running or complete )
                            if (Convert.ToInt32(gridSource[i].Field6) > 0)//order sent to kitchen ( running or complete )
                            {
                                BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                                dmnSearch.Select = "COUNT(*)  Field1";//;= SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS
                                dmnSearch.Table = "tblOrderDetails";
                                dmnSearch.WhereNdGroupBy = "WHERE status=3 and AddonStatus in(1,0) and OrderMasterId = " + gridSource[i].Field6;
                                dmnSearch.OrderBy = "";
                                var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                                if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                                {
                                    netamountlabel.Visible = true;
                                    tlp_table_inner.BackColor = BillingColor;
                                    tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                                    tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.AutoSize);
                                    netamountlabel.Dock = DockStyle.Fill;
                                    lbltablename.Dock = DockStyle.Fill;
                                    lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                                    netamountlabel.TextAlign = ContentAlignment.MiddleCenter;
                                }
                                else
                                {
                                    netamountlabel.Visible = false;
                                    tlp_table_inner.BackColor = OrderColor;
                                    tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                    tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                    lbltablename.Dock = DockStyle.Fill;
                                    lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                                }

                                lbltablename.ForeColor = Ordertablefontcolor;
                                tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                            }
                            #endregion

                            #region order not  sent to kitchen reservation or overtime 
                            else //order not  sent to kitchen reservation or overtime 
                            {
                                DateTime modifiedEndDate_before = gridSource[i].EndDate.AddMinutes(-Convert.ToDouble(FrmLogin.BookingInterwell));
                                DateTime modifiedEndDate_after = gridSource[i].EndDate;//.AddMinutes(Convert.ToDouble(FrmLogin.BookingInterwell))
                                DateTime ExpdateNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                DateTime Bookdate = Convert.ToDateTime(gridSource[i].EndDate.ToString("yyyy-MM-dd HH:mm"));
                                if (ExpdateNow >= modifiedEndDate_before && ExpdateNow <= modifiedEndDate_after)
                                {

                                    tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                    lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                    tlp_table_inner.BackColor = ReservationColor;

                                    tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                    tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                    lbltablename.Dock = DockStyle.Fill;
                                    lbltablename.TextAlign = ContentAlignment.MiddleCenter;

                                }
                                else if (ExpdateNow > Bookdate)// set expired  icon .AddMinutes(FrmLogin.BookingInterwell))
                                {
                                    tlp_table_inner.BackColor = ExpiredColor;
                                    tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;
                                    lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;

                                    tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                    tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                    lbltablename.Dock = DockStyle.Fill;
                                    lbltablename.TextAlign = ContentAlignment.MiddleCenter;

                                }
                            }
                            #endregion

                        }
                        else
                        {
                            netamountlabel.Visible = false;
                            tlp_table_inner.BackColor = DefaultColor;
                            tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                            tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                            lbltablename.Dock = DockStyle.Fill;
                            lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                            tlp_table_inner.AccessibleDescription = "0";
                            lbltablename.AccessibleDescription = "0";
                            netamountlabel.AccessibleDescription = "0";
                        }
                    }
                    #endregion

                    lbltablename.Click += tableClickEventHandler;
                    pnl_container.Click += tableClickEventHandler;
                    netamountlabel.Click += tableClickEventHandler;

                    UIHelper.RoundCorners(pnl_container, 25, null);
                    flp_tableview.Controls.Add(pnl_container);
                }

                //BuildPanelLookup();

                flp_tableview.ResumeLayout();

                //initTimer();
            }
            catch (Exception ex)
            {
                clsgen.LogException("TableView page  btnsearch_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
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