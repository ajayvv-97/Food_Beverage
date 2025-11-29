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

namespace FoodandBeverage.Forms
{
    public partial class FrmTableView : Form
    {

        #region Declaration
        private ToolTip _toolTip = new ToolTip();
        //public Timer AutoTimer;
        TableLayoutPanel tlp_table_dashboard = new TableLayoutPanel();
        public List<DmnNameList> CheckedtableNames = new List<DmnNameList>();

        public int panelsPerRow = 0;
        public int tabletype = 0;
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        List<DmnTableinfo> TableDataList = new List<DmnTableinfo>();

        public Color DefaultColor = Color.White;
        public Color OrderColor = System.Drawing.ColorTranslator.FromHtml("#ADD8E6");
        public Color BillingColor  = System.Drawing.ColorTranslator.FromHtml("#ADD8E6");
        public Color ReservationColor  = Color.YellowGreen;
        public Color ExpiredColor = Color.Red;

        public Color Defaultfontcolor = Color.Black;
        public Color Ordertablefontcolor = Color.Green;
        public Color Netamountfontcolor = Color.Green;

        ClsGeneral clsgen = new ClsGeneral();
        #endregion

        public FrmTableView(int type)
        {
            InitializeComponent();
            _toolTip.AutoPopDelay = 5000;    // Time the tooltip stays visible (ms)
            _toolTip.InitialDelay = 500;     // Delay before showing tooltip (ms)
            _toolTip.ReshowDelay = 100;      // Delay for subsequent tooltips (ms)
            _toolTip.ShowAlways = true;
            tabletype = type;
        }

        private void FrmTableView_Load(object sender, EventArgs e)
        {
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                //AppSession.RegisterForm(this, (int)LogoutFormList.Home);
            }
            LoadTableStaus(tabletype);
        }

        #region Table Creation & Updation
        public void LoadTableStaus(int serachmode)
        {
            try
            {
                clsgen.Memoryrelease();

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                BllTableView<DmnGridFields> SearchDetails = new BllTableView<DmnGridFields>();
                DmnGridFields dmnSearchtabledata = new DmnGridFields();
                dmnSearchtabledata.Field30 = Convert.ToString(FrmLogin.BookingInterwell);
                dmnSearchtabledata.Field28 = "0";
                switch (serachmode)
                {
                    case 0: dmnSearchtabledata.Field29 = "0"; dmnSearch.WhereNdGroupBy = "where ti.Status=1"; break;
                    case 1: dmnSearchtabledata.Field29 = "1"; dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=1"; break;
                    case 2: dmnSearchtabledata.Field29 = "2"; dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=1 and ti.OrderMasterId >0"; break;
                    case 3: dmnSearchtabledata.Field29 = "3"; dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=0 and ti.OrderMasterId=0"; break;
                }

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

                var distinctCategories = gridSource.Select(item => item.Field1).Distinct().ToList();
                List<CategoryInfo> categoryList = new List<CategoryInfo>();
                foreach (var fcid in distinctCategories)
                {
                    string categoryName = gridSource.FirstOrDefault(item => item.Field1 == fcid)?.Field2;
                    if (categoryName != null)
                        categoryList.Add(new CategoryInfo { Fcid = Convert.ToInt32(fcid), CategoryName = categoryName });
                }

                // **Suspend layout before adding multiple controls**
                tlp_table_dashboard.SuspendLayout();
                tlp_table_dashboard.Controls.Clear();
                tlp_table_dashboard.AutoScroll = true;
                tlp_table_dashboard.BackColor = Color.Transparent;

                int rowIndex_cate = 0;

                foreach (var category in categoryList)
                {
                    // Category Label
                    Label lblcategory_head = new Label
                    {
                        Name = "lbcate_head_" + category.CategoryName,
                        Tag = category.Fcid,
                        Text = category.CategoryName,
                        ForeColor = Color.Black,
                        BackColor = Color.Transparent,
                        Dock = DockStyle.Top,
                        Font = new Font("Arial", 12, FontStyle.Bold) 
                    };


                    // Category TableLayoutPanel
                    TableLayoutPanel tlp_category = new TableLayoutPanel
                    {
                        Name = "tlp_category_" + category.CategoryName,
                        Tag = category.CategoryName,
                        Dock = DockStyle.Top,
                        AutoSize = true,
                        BackColor = Color.Transparent,
                    };
    

                    int rowIndex = 1;
                    int columnIndex = 0;

                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        if (Convert.ToInt32(gridSource[i].Field1) == category.Fcid)
                        {
                            // Inner table panel
                            TableLayoutPanel tlp_table_inner = new TableLayoutPanel
                            {
                                RowCount = 2,
                                Name = "tlpbox" + gridSource[i].Field4,
                                Tag = gridSource[i].Field3,
                                BorderStyle = BorderStyle.None,
                                Dock = DockStyle.Left,
                                Margin = new Padding(15, 10, 15, 10),
                                AccessibleDescription = "0",
                                BackColor=Color.White
                            };
                           // tlp_table_inner.BackgroundImage = Properties.Resources.table_view;
                            tlp_table_inner.BackgroundImageLayout = ImageLayout.Zoom;
                            tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                            tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                            // Table Name Label
                            Label lbltablename = new Label
                            {
                                Name = "lbtablename" + gridSource[i].Field4,
                                Tag = gridSource[i].Field3,
                                Text = gridSource[i].Field4,
                                BackColor = Color.Transparent,
                                Font = new Font("Arial", 10, FontStyle.Bold),
                                Dock = DockStyle.Fill,
                                TextAlign = ContentAlignment.MiddleCenter,
                                ForeColor = Convert.ToInt32(gridSource[i].Field5) == 1 ? Ordertablefontcolor : Defaultfontcolor,
                                AccessibleDescription = "0"
                            };

                            // Net Amount Label
                            Label netamountlabel = new Label
                            {
                                Name = "lb_netamount" + gridSource[i].Field4,
                                Text = "\u20B9" + Convert.ToString(gridSource[i].Field8),
                                Tag = gridSource[i].Field3,
                                ForeColor = Netamountfontcolor,
                                BackColor = Color.Transparent,
                                Font = new Font("Arial", 10, FontStyle.Bold),
                                Dock = DockStyle.Fill,
                                TextAlign = ContentAlignment.MiddleCenter,
                                Visible = false,
                                AccessibleDescription = "0"
                            };

                            // Size settings
                            if (screenWidth > 1366) { tlp_table_inner.Width = 300; tlp_table_inner.Height = 100; }
                            else if (screenWidth >= 1024) { tlp_table_inner.Width = 120; tlp_table_inner.Height = 80; }

                            if (columnIndex == panelsPerRow) { columnIndex = 0; rowIndex++; }

                               #region Normal Order 
                                    if (Convert.ToInt32(gridSource[i].Field5) == 1)//not reservation order 
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

                            // Add controls
                            tlp_table_inner.Controls.Add(lbltablename, 0, 0);
                            tlp_table_inner.Controls.Add(netamountlabel, 0, 1);

                            lbltablename.Click += tableClickEventHandler;
                            tlp_table_inner.Click += tableClickEventHandler;
                            netamountlabel.Click += tableClickEventHandler;

                            UIHelper.RoundCorners(tlp_table_inner, 25, null);

                            tlp_category.Controls.Add(tlp_table_inner, columnIndex, rowIndex);
                            columnIndex++;
                        }
                    }

                    tlp_table_dashboard.Controls.Add(lblcategory_head, 0, rowIndex_cate++);
                    tlp_table_dashboard.Controls.Add(tlp_category, 0, rowIndex_cate++);
                }

                this.Controls.Add(tlp_table_dashboard);
                tlp_table_dashboard.Dock = DockStyle.Fill;
                tlp_table_dashboard.ResumeLayout(); 

                tlp_table_dashboard.Focus();
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
                tlp_table_dashboard.SuspendLayout();

                DmnSearch dmnSearch = new DmnSearch();
                BllTableView<DmnGridFields> SearchDetails = new BllTableView<DmnGridFields>();
                DmnGridFields dmnSearchtabledata = new DmnGridFields();
                dmnSearchtabledata.Field30 = Convert.ToString(FrmLogin.BookingInterwell);
                dmnSearchtabledata.Field29 = serachmode.ToString();

                var gridSource = SearchDetails.serachreport("prdTableView", dmnSearchtabledata);
                if (gridSource.Count == 0) { tlp_table_dashboard.ResumeLayout(); return; }

                var distinctCategories = gridSource.Select(item => item.Field1).Distinct().ToList();
                List<CategoryInfo> categoryList = distinctCategories.Select(fcid =>
                {
                    var name = gridSource.FirstOrDefault(item => item.Field1 == fcid)?.Field2;
                    return name != null ? new CategoryInfo { Fcid = Convert.ToInt32(fcid), CategoryName = name } : null;
                }).Where(x => x != null).ToList();

                foreach (var category in categoryList)
                {
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        if (Convert.ToInt32(gridSource[i].Field1) != category.Fcid) continue;

                        TableLayoutPanel tlp_table_inner = this.Controls.Find("tlpbox" + gridSource[i].Field4, true).FirstOrDefault() as TableLayoutPanel;
                        Label lbltablename = this.Controls.Find("lbtablename" + gridSource[i].Field4, true).FirstOrDefault() as Label;
                        Label netamountlabel = this.Controls.Find("lb_netamount" + gridSource[i].Field4, true).FirstOrDefault() as Label;

                        if (tlp_table_inner == null) continue;

                        lbltablename.ForeColor = Convert.ToInt32(gridSource[i].Field5) == 1 ? Ordertablefontcolor : Defaultfontcolor;
                        tlp_table_inner.AccessibleDescription = "0";
                        lbltablename.AccessibleDescription = "0";

                        // Update NetAmount & color logic for normal/reservation orders

                        #region Normal Order -- not reservation order 
                        if (Convert.ToInt32(gridSource[i].Field5) == 1)//not reservation order 
                        {
                            BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                            dmnSearch.Select = "CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1,(select NetAmount from tblOrderMaster where Status!=0 and OrderMasterId =" + gridSource[i].Field6 + ") Field2";
                            dmnSearch.Table = "tblOrderDetails";
                            dmnSearch.WhereNdGroupBy = "WHERE status!=0 and AddonStatus in(1,0) and OrderMasterId = " + gridSource[i].Field6;
                            dmnSearch.OrderBy = "";
                            var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                            if (Convert.ToInt32(gridSource_order[0].Field1) == 1)
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
                                netamountlabel.Text = "\u20B9" + Convert.ToString(gridSource_order[0].Field2);
                                lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                                netamountlabel.TextAlign = ContentAlignment.MiddleCenter;
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
                }

                tlp_table_dashboard.ResumeLayout(); // Resume after updates
                tlp_table_dashboard.Dock = DockStyle.Fill;
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
