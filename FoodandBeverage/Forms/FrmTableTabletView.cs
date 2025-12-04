// FrmTableTabletView.cs
using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmTableTabletView : Form
    {
        #region Decalarion
        public Color DefaultColor = Color.White;
        public Color OrderColor = System.Drawing.ColorTranslator.FromHtml("#ADD8E6");
        public Color BillingColor = System.Drawing.ColorTranslator.FromHtml("#ADD8E6");
        public Color ReservationColor = Color.YellowGreen;
        public Color ExpiredColor = Color.Red;

        public Color Defaultfontcolor = Color.Black;
        public Color Ordertablefontcolor = Color.Green;
        public Color Netamountfontcolor = Color.Green;

        ClsGeneral clsgen = new ClsGeneral();
        public List<DmnNameList> CheckedtableNames = new List<DmnNameList>();
        int categoryId=0;
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
        private void SideMenuClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            categoryId = Convert.ToInt32(btn.Tag);
            LoadTableStaus(0);
        }

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
                dmnSearchtabledata.Field28 = Convert.ToString(categoryId);
                
                switch (serachmode)
                {
                    case 0:
                        dmnSearchtabledata.Field29 = "0";
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1";
                        break;
                    case 1:
                        dmnSearchtabledata.Field29 = "1";
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=1";
                        break;
                    case 2:
                        dmnSearchtabledata.Field29 = "2";
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=1 and ti.OrderMasterId >0";
                        break;
                    case 3:
                        dmnSearchtabledata.Field29 = "3";
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=0 and ti.OrderMasterId=0";
                        break;
                }

                var gridSource = SearchDetails.serachreport("prdTableView", dmnSearchtabledata);

                // expire check
                bool flag = false;
                for (int j = 0; j < gridSource.Count; j++)
                {
                    if (gridSource[j].EndDate != null && gridSource[j].Field7 != null &&
                        Convert.ToInt32(gridSource[j].Field7) > 1 && Convert.ToInt32(gridSource[j].Field10) == 0)
                    {
                        CheckexpireTable(Convert.ToInt32(gridSource[j].Field3), Convert.ToInt32(gridSource[j].Field7));
                        flag = true;
                    }
                }
                if (flag)
                    gridSource = SearchDetails.serachreport("prdTableView", dmnSearchtabledata);

                // Layout preparation
                flp_tableview.SuspendLayout();
                flp_tableview.Controls.Clear();
                flp_tableview.AutoScroll = true;
                flp_tableview.WrapContents = true;
                flp_tableview.FlowDirection = FlowDirection.LeftToRight;
                flp_tableview.BackColor = Color.Transparent;
                flp_tableview.Padding = new Padding(12);

                int tablesPerRow = 6; // You decide how many tables per row
                int spacing = 12;      // Space between tables (margin/padding)
                int totalSpacing = (tablesPerRow + 1) * spacing; // total spacing in row
                int availableWidth = flp_tableview.ClientSize.Width - totalSpacing; // space left for tables

                if (availableWidth <= 0) availableWidth = 1024; // fallback

                int computedCardWidth = availableWidth / tablesPerRow; // width per table
                int computedCardHeight = (int)(computedCardWidth * 0.6); // proportional height
                if (computedCardHeight < 80) computedCardHeight = 80;   // minimum height


                foreach (var item in gridSource)
                {
                    TableLayoutPanel tlp_table_inner = new TableLayoutPanel
                    {
                        RowCount = 2,
                        Name = "tlpbox" + item.Field4,
                        Tag = item.Field3,
                        BorderStyle = BorderStyle.None,
                        BackColor = Color.White,
                        //BackgroundImageLayout = ImageLayout.Zoom
                    };

                    tlp_table_inner.Width = computedCardWidth;
                    tlp_table_inner.Height = computedCardHeight;
                    tlp_table_inner.Margin = new Padding(spacing); // same spacing all around

                    tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
                    tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 40));

                    Label lbltablename = new Label
                    {
                        Name = "lbtablename" + item.Field4,
                        Tag = item.Field3,
                        Text = item.Field4,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Arial", 10, FontStyle.Bold),
                        BackColor = Color.Transparent
                    };

                    Label netamountlabel = new Label
                    {
                        Name = "lb_netamount" + item.Field4,
                        Text = "\u20B9" + Convert.ToString(item.Field8),
                        Tag = item.Field3,
                        ForeColor = Netamountfontcolor,
                        BackColor = Color.Transparent,
                        Font = new Font("Arial", 10, FontStyle.Bold),
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Visible = false
                    };

                    // --- STATUS LOGIC ---
                    if (Convert.ToInt32(item.Field5) == 1) // normal order
                    {
                        BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                        dmnSearch.Select = "COUNT(*) Field1";
                        dmnSearch.Table = "tblOrderDetails";
                        dmnSearch.WhereNdGroupBy = "WHERE status=3 and AddonStatus in(1,0) and OrderMasterId = " + item.Field6;
                        dmnSearch.OrderBy = "";
                        var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                        if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                        {
                            netamountlabel.Visible = true;
                            tlp_table_inner.BackColor = BillingColor;
                        }
                        else
                        {
                            netamountlabel.Visible = false;
                            tlp_table_inner.BackColor = OrderColor;
                        }
                        lbltablename.Dock = DockStyle.Fill;
                    }
                    else // reservation / booking order
                    {
                        if (item.EndDate != null && item.Field7 != null)
                        {
                            if (Convert.ToInt32(item.Field6) > 0) // order sent to kitchen
                            {
                                BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                                dmnSearch.Select = "COUNT(*) Field1";
                                dmnSearch.Table = "tblOrderDetails";
                                dmnSearch.WhereNdGroupBy = "WHERE status=3 and AddonStatus in(1,0) and OrderMasterId = " + item.Field6;
                                dmnSearch.OrderBy = "";
                                var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                                if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                                {
                                    netamountlabel.Visible = true;
                                    tlp_table_inner.BackColor = BillingColor;
                                }
                                else
                                {
                                    netamountlabel.Visible = false;
                                    tlp_table_inner.BackColor = OrderColor;
                                }

                                lbltablename.ForeColor = Ordertablefontcolor;
                                tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + item.Field7;
                                lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + item.Field7;
                            }
                            else // not sent to kitchen -> reservation / overtime checks
                            {
                                DateTime modifiedEndDate_before = item.EndDate.AddMinutes(-Convert.ToDouble(FrmLogin.BookingInterwell));
                                DateTime modifiedEndDate_after = item.EndDate;
                                DateTime ExpdateNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                DateTime Bookdate = Convert.ToDateTime(item.EndDate.ToString("yyyy-MM-dd HH:mm"));

                                if (ExpdateNow >= modifiedEndDate_before && ExpdateNow <= modifiedEndDate_after)
                                {
                                    tlp_table_inner.BackColor = ReservationColor;
                                    tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + item.Field7;
                                    lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + item.Field7;
                                }
                                else if (ExpdateNow > Bookdate)
                                {
                                    tlp_table_inner.BackColor = ExpiredColor;
                                    tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + item.Field7;
                                    lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + item.Field7;
                                }
                                lbltablename.Dock = DockStyle.Fill;
                                lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                            }
                        }
                        else
                        {
                            netamountlabel.Visible = false;
                            tlp_table_inner.BackColor = DefaultColor;
                            lbltablename.Dock = DockStyle.Fill;
                            lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                            tlp_table_inner.AccessibleDescription = "0";
                            lbltablename.AccessibleDescription = "0";
                            netamountlabel.AccessibleDescription = "0";
                        }
                    }

                    lbltablename.Click += tableClickEventHandler;
                    tlp_table_inner.Click += tableClickEventHandler;
                    netamountlabel.Click += tableClickEventHandler;

                    // Add labels to inner table
                    tlp_table_inner.Controls.Add(lbltablename, 0, 0);
                    tlp_table_inner.Controls.Add(netamountlabel, 0, 1);

                    UIHelper.RoundCorners(tlp_table_inner, 25);

                    // Add table to FlowLayoutPanel
                    flp_tableview.Controls.Add(tlp_table_inner);
                }

                flp_tableview.ResumeLayout();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }


        //public void LoadTableStaus(int serachmode)
        //{
        //    try
        //    {
        //        clsgen.Memoryrelease();
        //        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
        //        DmnSearch dmnSearch = new DmnSearch();
        //        BllTableView<DmnGridFields> SearchDetails = new BllTableView<DmnGridFields>();
        //        DmnGridFields dmnSearchtabledata = new DmnGridFields();
        //        dmnSearchtabledata.Field30 = Convert.ToString(FrmLogin.BookingInterwell);

        //        switch (serachmode)
        //        {
        //            case 0:
        //                dmnSearchtabledata.Field29 = "0";
        //                dmnSearch.WhereNdGroupBy = "where ti.Status=1";
        //                break;
        //            case 1:
        //                dmnSearchtabledata.Field29 = "1";
        //                dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=1";
        //                break;
        //            case 2:
        //                dmnSearchtabledata.Field29 = "2";
        //                dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=1 and ti.OrderMasterId >0";
        //                break;
        //            case 3:
        //                dmnSearchtabledata.Field29 = "3";
        //                dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=0 and ti.OrderMasterId=0";
        //                break;
        //        }

        //        var gridSource = SearchDetails.serachreport("prdTableView", dmnSearchtabledata);

        //        // expire check (unchanged)
        //        bool flag = false;
        //        for (int j = 0; j < gridSource.Count; j++)
        //        {
        //            if (gridSource[j].EndDate != null && gridSource[j].Field7 != null &&
        //                Convert.ToInt32(gridSource[j].Field7) > 1 && Convert.ToInt32(gridSource[j].Field10) == 0)
        //            {
        //                if (Convert.ToInt32(gridSource[j].Field7) > 0)
        //                {
        //                    CheckexpireTable(Convert.ToInt32(gridSource[j].Field3), Convert.ToInt32(gridSource[j].Field7));
        //                    flag = true;
        //                }
        //            }
        //        }
        //        if (flag)
        //            gridSource = SearchDetails.serachreport("prdTableView", dmnSearchtabledata);

        //        // categories
        //        var distinctCategories = gridSource.Select(item => item.Field1).Distinct().ToList();
        //        List<CategoryInfo> categoryList = new List<CategoryInfo>();
        //        foreach (var fcid in distinctCategories)
        //        {
        //            string categoryName = gridSource.FirstOrDefault(item => item.Field1 == fcid)?.Field2;
        //            if (categoryName != null)
        //                categoryList.Add(new CategoryInfo { Fcid = Convert.ToInt32(fcid), CategoryName = categoryName });
        //        }

        //        // layout preparation
        //        tlp_table_dashboard.SuspendLayout();
        //        tlp_table_dashboard.Controls.Clear();
        //        tlp_table_dashboard.AutoScroll = true;
        //        tlp_table_dashboard.BackColor = Color.Transparent;

        //        int rowIndex_cate = 0;

        //        // Determine available width for cards. Prefer tlp_table_dashboard client width; fallback to form width.
        //        int availableWidth = tlp_table_dashboard.ClientSize.Width;
        //        if (availableWidth <= 0) availableWidth = this.ClientSize.Width;
        //        if (availableWidth <= 0) availableWidth = 1024; // final fallback

        //        // Decide panels per row based on available width.
        //        // Try to place between 3 and 8 cards per row depending on width, but not exceed reasonable limits.
        //        int panelsPerRowCalc = Clamp(availableWidth / 220, 1, 8); // 220 is an approximate ideal card width
        //        if (panelsPerRowCalc < 1) panelsPerRowCalc = 1;

        //        // Calculate dynamic card size (accounting for margins)
        //        int horizontalPaddingPerCard = 36; // approximate margin + spacing per card
        //        int computedCardWidth = Math.Max(120, (availableWidth - (panelsPerRowCalc + 1) * horizontalPaddingPerCard) / Math.Max(1, panelsPerRowCalc));
        //        int computedCardHeight = (int)(computedCardWidth * 0.6); // height ratio - tweak as needed
        //        if (computedCardHeight < 80) computedCardHeight = 80;

        //        // iterate categories and add cards
        //        foreach (var category in categoryList)
        //        {
        //            // Category Label
        //            Label lblcategory_head = new Label
        //            {
        //                Name = "lbcate_head_" + category.CategoryName,
        //                Tag = category.Fcid,
        //                Text = category.CategoryName,
        //                ForeColor = Color.Black,
        //                BackColor = Color.Transparent,
        //                Dock = DockStyle.Top,
        //                Font = new Font("Arial", 12, FontStyle.Bold),
        //                AutoSize = false,
        //                Height = 30,
        //                TextAlign = ContentAlignment.MiddleLeft,
        //                Padding = new Padding(8, 4, 8, 4)
        //            };

        //            // Category TableLayoutPanel - we will set columns = panelsPerRowCalc
        //            TableLayoutPanel tlp_category = new TableLayoutPanel
        //            {
        //                Name = "tlp_category_" + category.CategoryName,
        //                Tag = category.CategoryName,
        //                Dock = DockStyle.Top,
        //                AutoSize = false, // we manage sizes manually for better control
        //                BackColor = Color.Transparent,
        //                Margin = new Padding(0, 6, 0, 18)
        //            };

        //            // Configure columns for the category grid
        //            tlp_category.ColumnCount = panelsPerRowCalc;
        //            tlp_category.RowCount = 1;
        //            tlp_category.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        //            tlp_category.AutoScroll = false;
        //            tlp_category.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
        //            tlp_category.Padding = new Padding(12, 6, 12, 6);

        //            // Clear any old styles and set new column styles (percent-based)
        //            tlp_category.ColumnStyles.Clear();
        //            for (int c = 0; c < panelsPerRowCalc; c++)
        //                tlp_category.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, computedCardWidth + 2 * 12 /* inner padding estimate */));

        //            // We'll add rows as needed below
        //            tlp_category.RowStyles.Clear();
        //            tlp_category.RowStyles.Add(new RowStyle(SizeType.Absolute, computedCardHeight + 20)); // initial row height

        //            // counters
        //            int rowIndex = 0;
        //            int columnIndex = 0;

        //            // loop through the gridSource items for this category
        //            for (int i = 0; i < gridSource.Count; i++)
        //            {
        //                if (Convert.ToInt32(gridSource[i].Field1) != category.Fcid)
        //                    continue;

        //                // create inner table panel (card)
        //                TableLayoutPanel tlp_table_inner = new TableLayoutPanel
        //                {
        //                    RowCount = 2,
        //                    Name = "tlpbox" + gridSource[i].Field4,
        //                    Tag = gridSource[i].Field3,
        //                    BorderStyle = BorderStyle.None,
        //                    Dock = DockStyle.Fill,
        //                    Margin = new Padding(12),
        //                    AccessibleDescription = "0",
        //                    BackColor = Color.Transparent
        //                };

        //                // background and rows
        //                tlp_table_inner.BackgroundImage = Properties.Resources.table_view;
        //                tlp_table_inner.BackgroundImageLayout = ImageLayout.Zoom;
        //                tlp_table_inner.RowStyles.Clear();
        //                tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
        //                tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 40));

        //                // create labels
        //                Label lbltablename = new Label
        //                {
        //                    Name = "lbtablename" + gridSource[i].Field4,
        //                    Tag = gridSource[i].Field3,
        //                    Text = gridSource[i].Field4,
        //                    BackColor = Color.Transparent,
        //                    Font = new Font("Arial", Math.Max(8, (int)(10 * (computedCardWidth / 200.0))), FontStyle.Bold),
        //                    Dock = DockStyle.Fill,
        //                    TextAlign = ContentAlignment.MiddleCenter,
        //                    ForeColor = Convert.ToInt32(gridSource[i].Field5) == 1 ? Ordertablefontcolor : Defaultfontcolor,
        //                    AccessibleDescription = "0"
        //                };

        //                Label netamountlabel = new Label
        //                {
        //                    Name = "lb_netamount" + gridSource[i].Field4,
        //                    Text = "\u20B9" + Convert.ToString(gridSource[i].Field8),
        //                    Tag = gridSource[i].Field3,
        //                    ForeColor = Netamountfontcolor,
        //                    BackColor = Color.Transparent,
        //                    Font = new Font("Arial", Math.Max(8, (int)(10 * (computedCardWidth / 200.0))), FontStyle.Bold),
        //                    Dock = DockStyle.Fill,
        //                    TextAlign = ContentAlignment.MiddleCenter,
        //                    Visible = false,
        //                    AccessibleDescription = "0"
        //                };

        //                // size of the card: given we are using TableLayout columns with absolute widths,
        //                // we set a preferred size to help layout engine (and RoundCorners)
        //                tlp_table_inner.Width = computedCardWidth;
        //                tlp_table_inner.Height = computedCardHeight;

        //                // apply status logic (preserve your business logic; slightly refactored for clarity)
        //                if (Convert.ToInt32(gridSource[i].Field5) == 1) // normal order
        //                {
        //                    BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
        //                    dmnSearch.Select = "COUNT(*)  Field1";
        //                    dmnSearch.Table = "tblOrderDetails";
        //                    dmnSearch.WhereNdGroupBy = "WHERE status=3 and AddonStatus in(1,0) and OrderMasterId = " + gridSource[i].Field6;
        //                    dmnSearch.OrderBy = "";
        //                    var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

        //                    if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
        //                    {
        //                        netamountlabel.Visible = true;
        //                        tlp_table_inner.BackColor = BillingColor;
        //                        netamountlabel.Dock = DockStyle.Fill;
        //                        lbltablename.Dock = DockStyle.Fill;
        //                    }
        //                    else
        //                    {
        //                        netamountlabel.Visible = false;
        //                        tlp_table_inner.BackColor = OrderColor;
        //                        lbltablename.Dock = DockStyle.Fill;
        //                    }
        //                }
        //                else // reservation/booking order
        //                {
        //                    if (gridSource[i].EndDate != null && gridSource[i].Field7 != null)
        //                    {
        //                        if (Convert.ToInt32(gridSource[i].Field6) > 0) // order sent to kitchen
        //                        {
        //                            BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
        //                            dmnSearch.Select = "COUNT(*)  Field1";
        //                            dmnSearch.Table = "tblOrderDetails";
        //                            dmnSearch.WhereNdGroupBy = "WHERE status=3 and AddonStatus in(1,0) and OrderMasterId = " + gridSource[i].Field6;
        //                            dmnSearch.OrderBy = "";
        //                            var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

        //                            if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
        //                            {
        //                                netamountlabel.Visible = true;
        //                                tlp_table_inner.BackColor = BillingColor;
        //                                netamountlabel.Dock = DockStyle.Fill;
        //                                lbltablename.Dock = DockStyle.Fill;
        //                            }
        //                            else
        //                            {
        //                                netamountlabel.Visible = false;
        //                                tlp_table_inner.BackColor = OrderColor;
        //                                lbltablename.Dock = DockStyle.Fill;
        //                            }

        //                            lbltablename.ForeColor = Ordertablefontcolor;
        //                            tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
        //                            lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
        //                        }
        //                        else // not sent to kitchen -> reservation / overtime checks
        //                        {
        //                            DateTime modifiedEndDate_before = gridSource[i].EndDate.AddMinutes(-Convert.ToDouble(FrmLogin.BookingInterwell));
        //                            DateTime modifiedEndDate_after = gridSource[i].EndDate;
        //                            DateTime ExpdateNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        //                            DateTime Bookdate = Convert.ToDateTime(gridSource[i].EndDate.ToString("yyyy-MM-dd HH:mm"));

        //                            if (ExpdateNow >= modifiedEndDate_before && ExpdateNow <= modifiedEndDate_after)
        //                            {
        //                                tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
        //                                lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
        //                                tlp_table_inner.BackColor = ReservationColor;
        //                                lbltablename.Dock = DockStyle.Fill;
        //                                lbltablename.TextAlign = ContentAlignment.MiddleCenter;
        //                            }
        //                            else if (ExpdateNow > Bookdate)
        //                            {
        //                                tlp_table_inner.BackColor = ExpiredColor;
        //                                tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;
        //                                lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;
        //                                lbltablename.Dock = DockStyle.Fill;
        //                                lbltablename.TextAlign = ContentAlignment.MiddleCenter;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        netamountlabel.Visible = false;
        //                        tlp_table_inner.BackColor = DefaultColor;
        //                        lbltablename.Dock = DockStyle.Fill;
        //                        lbltablename.TextAlign = ContentAlignment.MiddleCenter;
        //                        tlp_table_inner.AccessibleDescription = "0";
        //                        lbltablename.AccessibleDescription = "0";
        //                        netamountlabel.AccessibleDescription = "0";
        //                    }
        //                }

        //                // add labels into inner table (two rows)
        //                // ensure columns of inner table exist (we use a single column)
        //                tlp_table_inner.ColumnCount = 1;
        //                tlp_table_inner.ColumnStyles.Clear();
        //                tlp_table_inner.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

        //                tlp_table_inner.Controls.Add(lbltablename, 0, 0);
        //                tlp_table_inner.Controls.Add(netamountlabel, 0, 1);

        //                // wire up events
        //                lbltablename.Click += tableClickEventHandler;
        //                tlp_table_inner.Click += tableClickEventHandler;
        //                netamountlabel.Click += tableClickEventHandler;

        //                // rounding helper (your existing helper)
        //                UIHelper.RoundCorners(tlp_table_inner, 25, null);

        //                // Ensure tlp_category has enough rows
        //                if (columnIndex >= tlp_category.ColumnCount)
        //                {
        //                    columnIndex = 0;
        //                    rowIndex++;
        //                }
        //                if (rowIndex >= tlp_category.RowCount)
        //                {
        //                    // add a new row
        //                    tlp_category.RowCount = tlp_category.RowCount + 1;
        //                    tlp_category.RowStyles.Add(new RowStyle(SizeType.Absolute, computedCardHeight + 20));
        //                }

        //                // Place the control into table at (columnIndex, rowIndex)
        //                // If ColumnCount is absolute widths, Controls.Add(control, col, row) expects that row exists.
        //                tlp_category.Controls.Add(tlp_table_inner, columnIndex, rowIndex);

        //                columnIndex++;
        //            } // end for items in gridSource

        //            // Add category header and category panel to dashboard table layout
        //            tlp_table_dashboard.Controls.Add(lblcategory_head, 0, rowIndex_cate++);
        //            tlp_table_dashboard.Controls.Add(tlp_category, 0, rowIndex_cate++);
        //        } // end foreach category

        //        // finalize
        //        // If tlp_table_dashboard is not already parented on the form, ensure it's added
        //        if (tlp_table_dashboard.Parent == null)
        //            this.Controls.Add(tlp_table_dashboard);

        //        tlp_table_dashboard.Dock = DockStyle.Fill;
        //        tlp_table_dashboard.ResumeLayout(false);
        //        tlp_table_dashboard.PerformLayout();

        //        tlp_table_dashboard.Focus();
        //        //initTimer();
        //    }
        //    catch (Exception ex)
        //    {
        //        clsgen.LogException("TableView page  LoadTableStaus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
        //    }
        //}
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
                                //TableStaus(0);

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
            sidemenutable();
            LoadTableStaus(0);
        }

        private void pblogo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            
            BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
            DmnSearch objSearch = new DmnSearch();

            objSearch.Select = "Tid,TableCode";
            objSearch.Table = "tblTable_info";
            objSearch.WhereNdGroupBy = "where status = 1 and TableCode like '%"+ txttablecode.Text.Trim() + "%'";
            objSearch.OrderBy = "";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);   
            if (itemSource.Count > 0)
            {
            }
        }

        private void txttablecode_KeyPress(object sender, KeyPressEventArgs e)
        {

                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true; // ignore Enter key
                }           

        }
    }
}
