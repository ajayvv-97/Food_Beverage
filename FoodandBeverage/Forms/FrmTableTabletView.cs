// FrmTableTabletView.cs
using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmTableTabletView : Form
    {
        #region Decalarion


        ClsGeneral clsgen = new ClsGeneral();
        public List<DmnNameList> CheckedtableNames = new List<DmnNameList>();
        int categoryId=0;

        public static int downloadServiceValue = Convert.ToInt32(ConfigurationManager.AppSettings["ImageDownloadService"]);
        ClsDocument clsDocument = new ClsDocument();

        public Color DefaultColor = Color.White;
        public Color OrderColor = System.Drawing.ColorTranslator.FromHtml("#ADD8E6");
        public Color BillingColor = System.Drawing.ColorTranslator.FromHtml("#ADD8E6");
        public Color ReservationColor = Color.YellowGreen;
        public Color ExpiredColor = Color.Red;
        public Color CancelProduct_order = ColorTranslator.FromHtml("#FFB3B3");

        public Color Defaultfontcolor = Color.Black;
        public Color Ordertablefontcolor = Color.Red;
        public Color Netamountfontcolor = Color.Red;

        private List<DmnGridFields> TableData;
        private List<DmnGridFields> pagedatadata;

        float PRODUCT_WIDTH_PERCENT = 1.5f;
        float PRODUCT_HEIGHT_PERCENT = 1f;
        int maxColumns = 10;
        int tileW = 0;
        int tileH = 0;

        int Pagecount = 60;// Convert.ToInt32(ConfigurationManager.AppSettings["FBpagescount"]);
        int currentPage = 1;
        int pageSize = 0;
        int totalPages = 0;

        int  SearchModetype = 0, categorystatus = 0;
        #endregion
        public FrmTableTabletView()
        {
            InitializeComponent();
        }
        public void sidemenutable()
        {
            try
            {
                flp_sidemenu.Controls.Clear();
                flp_sidemenu.FlowDirection = FlowDirection.TopDown; // buttons stack vertically
                flp_sidemenu.WrapContents = false;                 // do not wrap horizontally
                flp_sidemenu.AutoScroll = true;                   // enable vertical scroll
                flp_sidemenu.HorizontalScroll.Enabled = false;    // hide horizontal scrollbar
                flp_sidemenu.HorizontalScroll.Visible = false;
                flp_sidemenu.HorizontalScroll.Maximum = 0;
                flp_sidemenu.AutoScrollMargin = new Size(0, 0);

                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Fcid,CategoryName";
                objSearch.Table = "tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where status = 1 and CategoryType=1";
                objSearch.OrderBy = "";

                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);

                if (itemSource.Count > 0)
                {
                    ToolTip tooltip = new ToolTip();

                    int padding = 20; // total horizontal padding/margin to leave
                    int btnHeight = 50; // fixed button height

                    // --- Add default "All" button ---
                    Button btnAll = new Button();
                    btnAll.Name = "btnSideMenu_All";
                    btnAll.Tag = 0; // default tag
                    btnAll.Text = "All";
                    btnAll.Width = Math.Max(80, flp_sidemenu.ClientSize.Width - padding);
                    btnAll.Height = btnHeight;

                    btnAll.FlatStyle = FlatStyle.Flat;
                    btnAll.FlatAppearance.BorderSize = 0;
                    btnAll.BackColor = Color.Transparent;
                    btnAll.ForeColor = Color.White;
                    btnAll.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    btnAll.Margin = new Padding(0, 5, 0, 5);

                    btnAll.BackgroundImage = Properties.Resources.fb_sidemenu;
                    btnAll.BackgroundImageLayout = ImageLayout.Stretch;
                    btnAll.TextAlign = ContentAlignment.MiddleLeft;
                    btnAll.Padding = new Padding(10, 0, 0, 0);

                    btnAll.Click += SideMenuClick;

                    tooltip.SetToolTip(btnAll, "All");

                    flp_sidemenu.Controls.Add(btnAll);

                    // --- Add dynamic category buttons ---
                    foreach (var item in itemSource)
                    {
                        Button btn = new Button();

                        btn.Name = "btnSideMenu_" + item.Fcid;
                        btn.Tag = item.Fcid;
                        btn.Text = item.CategoryName;

                        // Dynamically calculate width based on FlowLayoutPanel width
                        btn.Width = Math.Max(80, flp_sidemenu.ClientSize.Width - padding);
                        btn.Height = btnHeight;

                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 0;
                        btn.BackColor = Color.Transparent;
                        btn.ForeColor = Color.White;
                        btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                        btn.Margin = new Padding(0, 5, 0, 5);

                        btn.BackgroundImage = Properties.Resources.fb_sidemenu;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                        btn.TextAlign = ContentAlignment.MiddleLeft;
                        btn.Padding = new Padding(10, 0, 0, 0);

                        btn.Click += SideMenuClick;

                        tooltip.SetToolTip(btn, item.CategoryName);

                        flp_sidemenu.Controls.Add(btn);
                    }


                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("TableView page sidemenutable()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }

        #region Table Creation & Updation    

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
                        AccessibleName = "0",
                    };
                    TableLayoutPanel tlp_table_inner = new TableLayoutPanel
                    {
                        Name = "tlp_table_inner" + pagedatadata[i].Field4,
                        Dock = DockStyle.Fill,
                        ColumnCount = 1,
                        RowCount = 2,
                        Tag = pagedatadata[i].Field3,
                        BackColor = Color.White,   // IMPORTANT: visible border
                        Margin = new Padding(0),
                        Padding = new Padding(2),
                        AccessibleName = "0",
                        //CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,  // SHOW BORDER
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

                    this.BeginInvoke(new Action(() =>
                    {
                        UIHelper.AutoFitLabelFont(lbltablename);
                        UIHelper.AutoFitLabelFont(netamountlabel);
                    }));

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

                        netamountlabel.Visible = false;
                        tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                        tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                        netamountlabel.TextAlign = ContentAlignment.MiddleCenter;

                        //if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                        //    tlp_table_inner.BackColor = BillingColor;
                        //else
                        tlp_table_inner.BackColor = DefaultColor;
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
        public void initTimer()
        {
            AutoTimer.Start();
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
                    if (lbltablename != null)
                        lbltablename.ForeColor = Convert.ToInt32(pagedatadata[i].Field5) == 1 ? Ordertablefontcolor : Defaultfontcolor;

                    tlp_table_inner.AccessibleDescription = "0";
                    lbltablename.AccessibleDescription = "0";
                    netamountlabel.AccessibleDescription = "0";

                    // Update NetAmount & color logic for normal/reservation orders

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

                        lbltablename.AccessibleName = "1";
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
                        //if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                        //    tlp_table_inner.BackColor = BillingColor;
                        //else
                        tlp_table_inner.BackColor = DefaultColor;
                    }
                }

                //initTimer();
            }
            catch (Exception ex)
            {
                clsgen.LogException("TabletTableView page  LoadPagedata()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TabletTableView));
            }
        }

        #endregion
        private void SideMenuClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            categoryId = Convert.ToInt32(btn.Tag);
            categorystatus = 1;
            AutoTimer.Stop();
            LoadTableStaus(0);
        }

        private void tableClickEventHandler(object sender, EventArgs e)
        {
            try
            {
               // AutoTimer.Stop();
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
                        if (System.Windows.Forms.Application.OpenForms["FrmOrderConsoleTabletmode"] != null)
                        {
                            FrmOrderConsoleTabletmode _instanceFrmOrder = Application.OpenForms["FrmOrderConsoleTabletmode"] as FrmOrderConsoleTabletmode;
                            if (_instanceFrmOrder.WindowState == FormWindowState.Minimized)
                            {
                                _instanceFrmOrder.WindowState = FormWindowState.Maximized;
                            }
                            else
                            {
                                _instanceFrmOrder.ShowDialog();
                                _instanceFrmOrder.BringToFront();
                            }
                        }
                        else
                        {
                            int tagValue;
                            if (clickedControl.Tag != null)
                            {
                                this.Visible = false;
                                using (FrmOrderConsoleTabletmode _instanceFrmOrder = new FrmOrderConsoleTabletmode(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Normalorder), 0, CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage),0))
                                {
                                    _instanceFrmOrder.Opacity = 0;
                                    _instanceFrmOrder.Visible = false;
                                    _instanceFrmOrder.ShowDialog();


                                }
                                this.Visible = true;
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
                                //TableStaus(0);

                            }
                            //}
                        }
                        else if (result == DialogResult.No)
                        {
                            using (FrmOrderConsoleTabletmode _instanceFrmOrder = new FrmOrderConsoleTabletmode(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid), CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage),0))
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
                        using (FrmOrderConsoleTabletmode _instanceFrmOrder = new FrmOrderConsoleTabletmode(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid), CheckedtableNames, Convert.ToInt32(PageType.OrderPage), 0, Convert.ToInt32(SourcepageType.Tableviewpage),0))
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

        public class CategoryInfo
        {
            public int Fcid { get; set; }
            public string CategoryName { get; set; }
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
        private int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }


        private void FrmTableTabletView_Load(object sender, EventArgs e)
        {
            pageSize = Pagecount;

            ProfilPIC_update();
            sidemenutable();

            LoadTableStaus(0);
        }

        private void pblogo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void ProfilPIC_update()
        {
            try
            {
                bool imageExist, brLogoExist;
                string imageName = Convert.ToString(FrmLogin.EmployeeId) + ".jpeg";
                if (downloadServiceValue == Convert.ToInt32(Interfaces.DownloadService_Type.FTP))
                {
                    imageExist = clsDocument.FileExistOrNot(imageName, DocumentType.FoodPhoto);
                    if (imageExist)
                    {
                        pbusericon.Image = clsDocument.DownloadImage(imageName, DocumentType.FoodPhoto);
                    }
                }
                else if (downloadServiceValue == Convert.ToInt32(Interfaces.DownloadService_Type.DB))
                {
                    if (FrmLogin.EphId > 0)
                    {
                        int ins_type = Convert.ToInt32(Inseration_Type.Employee_Photo);
                        pbusericon.Image = null;
                        pbusericon.Image = clsDocument.DownloadImageDatabase(ins_type, FrmLogin.EphId);
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage Page ProfilPIC_update()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
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
                clsgen.LogException("TabletTableView page  btnsearch_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TabletTableView));
            }
        }

        private void txttablecode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // ignore Enter key
            }

        }

        private void pbusericon_Click(object sender, EventArgs e)
        {
            pnl_userdetails.Visible = !pnl_userdetails.Visible;

            if (!pnl_userdetails.Visible)
                return;

            lbl_ename.Text = FrmloginTablet.EmployeName;
            lbl_edesign.Text = FrmloginTablet.Designation;
            lbl_edepart.Text = FrmloginTablet.departmentname;

            lbl_ename.Font = AppFonts.GetFont(10);
            lbl_edesign.Font = AppFonts.GetFont(10);
            lbl_edepart.Font = AppFonts.GetFont(10);

            btn_elogout.Font = AppFonts.GetFont(10);
            btn_lock.Font = AppFonts.GetFont(10);

            UIHelper.ResizeOnly(pnl_userdetails, 15, 45);
            UIHelper.RoundCorners(pnl_userdetails, 25,Color.White);
            Point iconScreen = pbusericon.PointToScreen(Point.Empty);
            int desiredX = iconScreen.X + (pbusericon.Width / 2) - (pnl_userdetails.Width / 2);
            int desiredY = iconScreen.Y + pbusericon.Height + 10;
            Rectangle screenBounds = Screen.FromControl(pbusericon).WorkingArea;
            if (desiredX < screenBounds.Left)
                desiredX = screenBounds.Left;
            else if (desiredX + pnl_userdetails.Width > screenBounds.Right)
                desiredX = screenBounds.Right - pnl_userdetails.Width;
            if (desiredY + pnl_userdetails.Height > screenBounds.Bottom)
            {
                desiredY = iconScreen.Y - pnl_userdetails.Height - 10;
                if (desiredY < screenBounds.Top)
                    desiredY = screenBounds.Top;
            }
            Point clientPos = this.PointToClient(new Point(desiredX, desiredY));
            pnl_userdetails.Location = clientPos;
        }

        private void btn_lock_Click(object sender, EventArgs e)
        {

        }

        private void btn_elogout_Click(object sender, EventArgs e)
        {
            this.Opacity = 0;
            this.Close();
            if (System.Windows.Forms.Application.OpenForms["FrmloginTablet"] != null)
            {
                FrmloginTablet _instanceFrmLogin = Application.OpenForms["FrmloginTablet"] as FrmloginTablet;
                _instanceFrmLogin.Show();
                _instanceFrmLogin.BringToFront();
                _instanceFrmLogin.Visible = true;
                _instanceFrmLogin.Opacity = 100;
            }
        }

        private void AutoTimer_Tick(object sender, EventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            long memoryInBytes = currentProcess.PrivateMemorySize64;
            double memoryInMB = memoryInBytes / (1024.0 * 1024.0);
            lblmemory.Text = $"Memory: {memoryInMB:F2} MB";

            TableStaus(0);
        }

        private void btn_pickup_Click(object sender, EventArgs e)
        {

        }

        private void btnaviltable_Click(object sender, EventArgs e)
        {

        }

        private void btncanceltable_Click(object sender, EventArgs e)
        {

        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            txttablecode_search.Text = "";
            currentPage = 1;
            categoryId = 0;
            categorystatus = 0;
            SearchModetype = 0;
            AutoTimer.Stop();
            LoadTableStaus(0);
        }

        private void FrmTableTabletView_Resize(object sender, EventArgs e)
        {
            int containerWidth = flp_tableview.ClientSize.Width;
            tileW = (int)(containerWidth * PRODUCT_WIDTH_PERCENT / maxColumns);
            tileH = (int)(tileW * PRODUCT_HEIGHT_PERCENT);
        }

        #region Next Previous Button
        private void btnprev_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPage > 1) // <-- correct check
                {
                    AutoTimer.Stop();
                    currentPage--;
                    LoadTableStaus(0);
                    //pagedatadata = TableData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                    //lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {TableData.Count}";
                    btnprev.Enabled = currentPage > 1;
                    btnnext.Enabled = currentPage < totalPages;

                    //UpdatePagerInfo(); // <-- ensure pager label updates
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btnprev_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPage < totalPages)
                {
                    AutoTimer.Stop();
                    currentPage++;
                    LoadTableStaus(0);
                    //pagedatadata = TableData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                    //lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {TableData.Count}";
                    btnprev.Enabled = currentPage > 1;
                    btnnext.Enabled = currentPage < totalPages;
                    //UpdatePagerInfo(); // <-- update pager label
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btnnext_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        #endregion

        private void btn_minimium_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
