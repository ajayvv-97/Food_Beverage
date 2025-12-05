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


namespace FoodandBeverage.Forms
{
    public partial class FrmTableView : Form
    {

        #region Declaration
        private ToolTip _toolTip = new ToolTip();
        //public Timer AutoTimer;
        public List<DmnNameList> CheckedtableNames = new List<DmnNameList>();

        public int panelsPerRow = 0;
        public int tabletype = 0;
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        List<DmnTableinfo> TableDataList = new List<DmnTableinfo>();

        public Color DefaultColor = Color.White;
        public Color OrderColor = System.Drawing.ColorTranslator.FromHtml("#ADD8E6");
        //public Color BillingColor  = System.Drawing.ColorTranslator.FromHtml("#ADD8E6");
        public Color ReservationColor  = Color.YellowGreen;
       /// public Color ExpiredColor = Color.Red;
        public Color CancelProduct_order = ColorTranslator.FromHtml("#FFB3B3");

        public Color Defaultfontcolor = Color.Black;
        public Color Ordertablefontcolor = Color.Red;
        public Color Netamountfontcolor = Color.Red;

        ClsGeneral clsgen = new ClsGeneral();
        int categoryId = 0, SearchModetype = 0;
        private Dictionary<string, Control> panelLookup = new Dictionary<string, Control>();        

        float PRODUCT_WIDTH_PERCENT = 0.80f;
        float PRODUCT_HEIGHT_PERCENT = 0.80f;
        int maxColumns = 10;
        int tileW = 0;
        int tileH = 0;
        #endregion

        #region Fields

        private List<TableViewItem> tables = new List<TableViewItem>();
      
        private int columns = 10;
        private int spacing = 10;

        // Colors
        private Color FreeColor = Color.LightGreen;
        private Color BookingColor = Color.LightBlue;
        private Color BillingColor = Color.Orange;
        private Color CancelColor = Color.Red;
        private Color ExpiredColor = Color.Gray;
        #endregion

        public FrmTableView(int type)
        {
            InitializeComponent();
            InitializeTableView();

            _toolTip.AutoPopDelay = 5000;    // Time the tooltip stays visible (ms)
            _toolTip.InitialDelay = 500;     // Delay before showing tooltip (ms)
            _toolTip.ReshowDelay = 100;      // Delay for subsequent tooltips (ms)
            _toolTip.ShowAlways = true;
            SearchModetype = type;
        }

        #region Initialization
        private void InitializeTableView()
        {
            // Enable double buffering for smooth rendering
            ControlExtensions.SetDoubleBuffered(flp_tableview);

            // Subscribe to events
            flp_tableview.Paint += FlpTableview_Paint;
            flp_tableview.MouseClick += FlpTableview_MouseClick;

            UpdateTimer.Start();

            LoadTables();
        }

        #endregion

        #region table view advanced

        #region Load Tables
        private void LoadTables()
        {
            tables.Clear();

            #region Sample Data Generation (replace with your DB call)
            int tableId = 1;
            int categoryId = 11; // example category
            for (int fcid = 11; fcid <= 14; fcid++)
            {
                for (int t = 1; t <= 1000; t++) // 100 tables per category
                {
                    tables.Add(new TableViewItem
                    {
                        TableId = tableId,
                        TableCode = "TB" + tableId,
                        CategoryId = fcid,
                        OrderStatus = 0, // free
                        OrderMasterId = 0,
                        NetAmount = 0,
                        CancelCount = 0
                    });
                    tableId++;
                }
            }
            #endregion

            CalculateTableBounds();
            flp_tableview.Invalidate();
        }
        #endregion

        #region Calculate Table Positions
        private void CalculateTableBounds()
        {
            int containerWidth = flp_tableview.ClientSize.Width;

            int columns = maxColumns;
            int x = spacing;
            int y = spacing;

            for (int i = 0; i < tables.Count; i++)
            {
                if (i % columns == 0 && i != 0)
                {
                    x = spacing;
                    y += tileH + spacing;
                }

                tables[i].Bounds = new Rectangle(x, y, tileW, tileH);

                x += tileW + spacing;
            }
        }

        #endregion

        #region Paint Event - Draw Tables
        private void FlpTableview_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(flp_tableview.BackColor);

            foreach (var table in tables)
            {
                #region Determine Background Color

                Color bgColor;

                if (table.OrderStatus == 0)
                    bgColor = FreeColor;
                else if (table.OrderStatus == 1)
                    bgColor = BookingColor;
                else if (table.OrderStatus == 2)
                    bgColor = BillingColor;
                else
                    bgColor = ExpiredColor;

                using (SolidBrush brush = new SolidBrush(bgColor))
                    g.FillRectangle(brush, table.Bounds);
                g.DrawRectangle(Pens.Black, table.Bounds);
                #endregion

                #region Draw Table Code
                using (Font font = new Font("Arial", 10, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.Black))
                {
                    StringFormat format = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString(table.TableCode, font, textBrush, table.Bounds, format);
                }
                #endregion

                #region Draw Net Amount
                if (table.NetAmount > 0)
                {
                    Rectangle netRect = new Rectangle(
                        table.Bounds.X, table.Bounds.Bottom - 25, table.Bounds.Width, 25);
                    using (SolidBrush netBrush = new SolidBrush(Color.White))
                    {
                        g.FillRectangle(netBrush, netRect);
                        g.DrawRectangle(Pens.Black, netRect);
                        g.DrawString("Checkout: " + table.NetAmount,
                            new Font("Arial", 8, FontStyle.Bold), Brushes.Black, netRect, new StringFormat
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            });
                    }
                }
                #endregion

                #region Draw Cancel Count
                if (table.CancelCount > 0)
                {
                    Rectangle cancelRect = new Rectangle(
                        table.Bounds.Right - 25, table.Bounds.Y + 5, 20, 20);
                    g.FillEllipse(Brushes.Red, cancelRect);
                    g.DrawEllipse(Pens.Black, cancelRect);
                    g.DrawString(table.CancelCount.ToString(),
                        new Font("Arial", 8, FontStyle.Bold), Brushes.White, cancelRect,
                        new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
                #endregion
            }
        }
        #endregion

        #region Mouse Click Event
        private void FlpTableview_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (var table in tables)
            {
                if (table.Bounds.Contains(e.Location))
                {
                    OnTableClick(table);
                    break;
                }
            }
        }

        private void OnTableClick(TableViewItem table)
        {
            // Handle table click
            MessageBox.Show($"Clicked Table: {table.TableCode}\nStatus: {table.OrderStatus}");
        }
        #endregion

        #region Timer Event - Update Table Status

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // Simulate updating table status from DB
            Random rnd = new Random();
            foreach (var table in tables)
            {
                table.OrderStatus = rnd.Next(0, 3); // 0=Free,1=Booking,2=Billing
                table.NetAmount = rnd.Next(0, 2) * 100; // random amount for example
                table.CancelCount = rnd.Next(0, 2); // random cancel count
            }

            flp_tableview.Invalidate(); // redraw all tables efficiently
        }

        #endregion


        #region Table Model
        public class TableViewItem
        {
            public int TableId { get; set; }
            public string TableCode { get; set; }
            public int CategoryId { get; set; }
            public int OrderStatus { get; set; } // 0=Free,1=Booking,2=Billing
            public int OrderMasterId { get; set; }
            public decimal NetAmount { get; set; }
            public int CancelCount { get; set; }
            public DateTime? BookingEndTime { get; set; }
            public Rectangle Bounds { get; set; } // For rendering and click detection
        }
        #endregion

        #region Control Extension for DoubleBuffer
        public static class ControlExtensions
        {
            public static void SetDoubleBuffered(Control control)
            {
                if (SystemInformation.TerminalServerSession)
                    return;

                System.Reflection.PropertyInfo aProp =
                    typeof(Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

                aProp.SetValue(control, true, null);
            }
        }

        #endregion

        #endregion

        private void FrmTableView_Load(object sender, EventArgs e)
        {
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                //AppSession.RegisterForm(this, (int)LogoutFormList.Home);
            }
           /// LoadTableStaus(tabletype);
        }
        public void updateserach()
        {
            categoryId = tabletype;
        }

        #region Table Creation & Updation    

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
                dmnSearchtabledata.Field30 = Convert.ToString(FrmLogin.BookingInterwell);
                dmnSearchtabledata.Field28 = Convert.ToString(categoryId);
                dmnSearchtabledata.Field29 = Convert.ToString(SearchModetype); 

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
                    lbltablename.Click += tableClickEventHandler;

                    UIHelper.RoundCorners(pnl_container, 25, null);
                    flp_tableview.Controls.Add(pnl_container);
                }

                BuildPanelLookup();

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

                DmnSearch dmnSearch = new DmnSearch();
                BllTableView<DmnGridFields> SearchDetails = new BllTableView<DmnGridFields>();
                DmnGridFields dmnSearchtabledata = new DmnGridFields();
                dmnSearchtabledata.Field30 = Convert.ToString(FrmLogin.BookingInterwell);
                dmnSearchtabledata.Field28 = Convert.ToString(categoryId);
                dmnSearchtabledata.Field29 = Convert.ToString(SearchModetype);

                var gridSource = SearchDetails.serachreport("prdTableView", dmnSearchtabledata);
                if (gridSource.Count == 0)
                {
                    flp_tableview.ResumeLayout();
                    return;
                }

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

               
                //foreach (var row in gridSource)
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

                for (int i = 0; i < gridSource.Count; i++)
                {

                        TableLayoutPanel tlp_table_inner = this.Controls.Find("tlpbox" + gridSource[i].Field4, true).FirstOrDefault() as TableLayoutPanel;
                        Label lbltablename = this.Controls.Find("lbtablename" + gridSource[i].Field4, true).FirstOrDefault() as Label;
                        Label netamountlabel = this.Controls.Find("lb_netamount" + gridSource[i].Field4, true).FirstOrDefault() as Label;
                        Button btnCancelCount = this.Controls.Find("btnCancelCount" + gridSource[i].Field4, true).FirstOrDefault() as Button;
                        if (tlp_table_inner == null) continue;

                        lbltablename.ForeColor = Convert.ToInt32(gridSource[i].Field5) == 1 ? Ordertablefontcolor : Defaultfontcolor;
                        tlp_table_inner.AccessibleDescription = "0";
                        lbltablename.AccessibleDescription = "0";

                        // Update NetAmount & color logic for normal/reservation orders

                        #region Normal Order -- not reservation order 
                        if (Convert.ToInt32(gridSource[i].Field5) == 1)//not reservation order 
                        {
                            //BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                            //dmnSearch.Select = " SUM(CASE WHEN Status = 3 AND AddonStatus IN (1,0) THEN 1 ELSE 0 END) AS Field1," +
                            //                    " (SELECT NetAmount FROM tblOrderMaster WHERE Status != 0 AND OrderMasterId = "+ gridSource[i].Field6 + ") AS Field2,"+
                            //                    " SUM(CASE WHEN Status = 0 AND AddonStatus IN(1, 0) AND CancelNotifyStatus = 1 THEN 1 ELSE 0 END ) AS Field3";
                            //dmnSearch.Table = "tblOrderDetails";
                            //dmnSearch.WhereNdGroupBy = "where AddonStatus IN (1,0)   AND OrderMasterId = " + gridSource[i].Field6;
                            //dmnSearch.OrderBy = "";
                            //var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);
                            BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                            dmnSearch.Select = "  SUM(CASE WHEN Status = 3 AND AddonStatus IN (1,0) THEN 1 ELSE 0 END) AS Field1,SUM(CASE WHEN Status = 0 AND AddonStatus IN (1,0) AND CancelNotifyStatus = 1 THEN 1 ELSE 0 END) AS Field2";//;= SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS
                            dmnSearch.Table = "tblOrderDetails";
                            dmnSearch.WhereNdGroupBy = "WHERE OrderMasterId = " + gridSource[i].Field6;
                            dmnSearch.OrderBy = "";
                            var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);
                            if (gridSource_order.Count > 0)
                            {
                                if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                                {
                                    netamountlabel.Visible = false;
                                    tlp_table_inner.BackColor = OrderColor;
                                    tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                    tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                    lbltablename.Dock = DockStyle.Fill;
                                    lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                                    netamountlabel.Text = "";
                                }
                                else
                                {
                                    netamountlabel.Visible = true;
                                    tlp_table_inner.BackColor = BillingColor;
                                    tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                                    tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.AutoSize);
                                    netamountlabel.Dock = DockStyle.Fill;
                                    lbltablename.Dock = DockStyle.Fill;
                                    netamountlabel.Text = "Checkout";//"\u20B9" + Convert.ToString(gridSource_order[0].Field2);
                                    lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                                    netamountlabel.TextAlign = ContentAlignment.MiddleCenter;
                                }
                                if (Convert.ToInt32(gridSource_order[0].Field2) == 1)
                                {
                                    btnCancelCount.Visible = true;
                                    tlp_table_inner.BackColor = CancelProduct_order;
                                    btnCancelCount.Text = gridSource_order[0].Field3;
                                }
                                else
                                {
                                    btnCancelCount.Visible = false;
                                    if (Convert.ToInt32(gridSource_order[0].Field1) == 1)
                                        tlp_table_inner.BackColor = OrderColor;
                                    else
                                        tlp_table_inner.BackColor = BillingColor;
                                }
                            }
                        }
                        #endregion

                        #region Rservation Bokking Order
                        else if (Convert.ToInt32(gridSource[i].Field5) == 0 && (Convert.ToInt32(gridSource[i].Field7) > 0))//reservation booking order
                        {
                            if (gridSource[i].EndDate != null && gridSource[i].Field7 != null)
                            {
                                #region order sent to kitchen ( running or complete )
                                if (Convert.ToInt32(gridSource[i].Field6) > 0)//order sent to kitchen ( running or complete )
                                {
                                    BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                                    dmnSearch.Select = " COUNT(*)  Field1";//= SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS
                                    dmnSearch.Table = "tblOrderDetails";
                                    dmnSearch.WhereNdGroupBy = "WHERE status=3 and OrderMasterId = " + gridSource[i].Field6;
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
                                            netamountlabel.Text = "\u20B9" + Convert.ToString(gridSource[i].Field8);
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
                                        netamountlabel.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                    }
                                }
                                #endregion

                                #region order not  sent to kitchen reservation or overtime 
                                else //order not  sent to kitchen reservation or overtime 
                                {
                                    DateTime modifiedEndDate_before = gridSource[i].EndDate.AddMinutes(-Convert.ToDouble(FrmLogin.BookingInterwell));
                                    DateTime modifiedEndDate_after = gridSource[i].EndDate;//.AddMinutes(Convert.ToDouble(FrmLogin.BookingInterwell));
                                    DateTime ExpdateNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                    DateTime Bookdate = Convert.ToDateTime(gridSource[i].EndDate.ToString("yyyy-MM-dd HH:mm"));
                                    if (ExpdateNow >= modifiedEndDate_before && ExpdateNow <= modifiedEndDate_after)
                                    {
                                        tlp_table_inner.BackColor = ReservationColor;
                                        tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                        lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;

                                        tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                        tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                        lbltablename.Dock = DockStyle.Fill;
                                        lbltablename.TextAlign = ContentAlignment.MiddleCenter;

                                    }
                                    else if (ExpdateNow >= Bookdate)// set expired  icon .AddMinutes(FrmLogin.BookingInterwell))
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

                    }
                

                flp_tableview.ResumeLayout(); 
   

                initTimer();
            }
            catch (Exception ex)
            {
                clsgen.LogException("TableView page  TableStaus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }
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
        public void initTimer()
        {
            AutoTimer.Start();
        }
        private void tableClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                AutoTimer.Stop();
                CheckedtableNames.Clear();
                Control clickedControl = sender as Control;
                string textvalue = clickedControl.AccessibleDescription;
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
                                //this.Visible = false;
                                //this.Hide();
                                using (FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Normalorder), 0, CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage)))
                                {
                                    _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                    _instanceFrmOrder.Opacity = 0;
                                    _instanceFrmOrder.Visible = false;
                                    _instanceFrmOrder.ShowDialog();
                                    _instanceFrmOrder.BringToFront();
                                }
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
                                TableStaus(0);

                            }
                            //}
                        }
                        else if (result == DialogResult.No)
                        {
                            using (FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid), CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage)))
                            {
                                _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                _instanceFrmOrder.Opacity = 0;
                                _instanceFrmOrder.Visible = false;
                                _instanceFrmOrder.ShowDialog();
                                _instanceFrmOrder.BringToFront();
                            }

                        }
                    }
                    else if (Btype == Convert.ToInt32(DashboardTableType.Reverse_Table))
                    {
                        using (FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid), CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage)))
                        {
                            _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                            _instanceFrmOrder.Opacity = 0;
                            _instanceFrmOrder.Visible = false;
                            _instanceFrmOrder.ShowDialog();
                            _instanceFrmOrder.BringToFront();
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
                    _toolTip.SetToolTip(tlp, tlpcate.Tag.ToString());
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

        public class CategoryInfo
        {
            public int Fcid { get; set; }
            public string CategoryName { get; set; }
        }
        private static void RunMethodInSeparateThread(Action action)
        {
            var thread = new Thread(new ThreadStart(action));
            thread.Start();
        }

        private void AutoTimer_Tick(object sender, EventArgs e)
        {
            TableStaus(0);
        }

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

            foreach (Control ctrl in flp_sidemenu.Controls)
                ctrl.Dispose();
            flp_sidemenu.Controls.Clear();
            flp_sidemenu.Refresh();

            flp_sidemenu.FlowDirection = FlowDirection.LeftToRight;
            flp_sidemenu.Width = btn_showmenu.Width;
            flp_sidemenu.Height = (int)(this.Height * 0.5);

            flp_sidemenu.AutoScroll = true;
            flp_sidemenu.HorizontalScroll.Enabled = true;
            flp_sidemenu.HorizontalScroll.Visible = true;
            flp_sidemenu.VerticalScroll.Enabled = false;
            flp_sidemenu.VerticalScroll.Visible = false;

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

            int bheight =(int)(btn_showmenu.Height * 0.9);
            int bWidth = btn_showmenu.Width - 25;


            BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
            DmnSearch objSearch = new DmnSearch
            {
                Select = "Fcid,CategoryName",
                Table = "tblCategory_FTI",
                WhereNdGroupBy = "WHERE Status = 1 AND CategoryType = 1",
                OrderBy = ""
            };

            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);

            ToolTip tooltip = new ToolTip();

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
                BackgroundImage = Properties.Resources.fb_sidemenu,
                BackgroundImageLayout = ImageLayout.Stretch,

            };
            btnAll.FlatAppearance.BorderSize = 0;
            btnAll.Click += SideMenuClick;
            tooltip.SetToolTip(btnAll, "All");
            flp_sidemenu.Controls.Add(btnAll);

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
                        BackgroundImage = Properties.Resources.fb_sidemenu,
                        BackgroundImageLayout = ImageLayout.Stretch,

                    };

                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += SideMenuClick;
                    tooltip.SetToolTip(btn, item.CategoryName);
                    flp_sidemenu.Controls.Add(btn);
                }
            }
            UIHelper.RoundCorners(flp_sidemenu, 25);
        }
        private void SideMenuClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            categoryId = Convert.ToInt32(btn.Tag);
            flp_sidemenu.Visible = false;
            AutoTimer.Stop();
            LoadTableStaus(0);
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

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            categoryId = 0;
            SearchModetype=0;
            flp_sidemenu.Visible = false;
            AutoTimer.Stop();
            LoadTableStaus(0);
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

                BuildPanelLookup();

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
