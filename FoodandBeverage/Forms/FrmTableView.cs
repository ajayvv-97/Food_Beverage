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
        //public Timer AutoTimer;
        TableLayoutPanel tlp_table_dashboard = new TableLayoutPanel();
        public List<DmnNameList> CheckedtableNames = new List<DmnNameList>();

        public int panelsPerRow = 0;
        public int tabletype = 0;
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        List<DmnTableinfo> TableDataList = new List<DmnTableinfo>();

        public Color DefaultColor = Color.White;
        public Color OrderColor = System.Drawing.ColorTranslator.FromHtml("#e3dbdb");
        public Color BillingColor  = System.Drawing.ColorTranslator.FromHtml("#9f9a9a");
        public Color ReservationColor  = Color.YellowGreen;
        public Color ExpiredColor = Color.Red;

        public Color Defaultfontcolor = Color.Black;
        public Color Ordertablefontcolor = Color.Green;
        public Color Netamountfontcolor = Color.Green;

        ClsGeneral clsgen = new ClsGeneral();

    public FrmTableView(int type)
        {
            InitializeComponent();
            tabletype = type;
        }

        private void FrmTableView_Load(object sender, EventArgs e)
        {
            LoadTableStaus(tabletype);
        }

        public void LoadTableStaus(int serachmode)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                int pkrowIndex = 1, pkcolumnIndex = 0;
                //dmnSearch.Select = "c.Fcid as Field1,c.CategoryName as Field2,ti.Tid Field3,ti.TableCode Field4,ti.OrderStatus Field5,ti.OrderMasterId Field6, " +
                //    "(select top (1) bd.BookmasterId from tblbookMaster bm  LEFT JOIN tblBookDetails bd on bm.BookmasterId = bd.BookmasterId and bm.Status = 1 where bm.Status = 1 and bd.Status = 1 and SYSDATETIME() >= DATEADD(MINUTE, -3, BookingDateTime) and MTID = ti.tid ) Field7" +
                //    ",(select top (1) bm.BookingDateTime from tblbookMaster bm  LEFT JOIN tblBookDetails bd on bm.BookmasterId=bd.BookmasterId and bm.Status=1 where bm.Status = 1 and bd.Status = 1 and SYSDATETIME() >= DATEADD(MINUTE, -3, BookingDateTime) and MTID = ti.tid )  EndDate";
                //dmnSearch.Table = " tblTable_info  ti " +
                //    " LEFT JOIN tblCategory_FTI c on c.Fcid=ti.Fcid ";//and SYSDATETIME() <= DATEADD(MINUTE," + Convert.ToDecimal(FrmLogin.BookingInterwell) + ",BookingDateTime)";
                //dmnSearch.OrderBy = "";

                BllTableView<DmnGridFields> SearchDetails = new BllTableView<DmnGridFields>();
                DmnGridFields dmnSearchtabledata = new DmnGridFields();
                dmnSearchtabledata.Field30 = Convert.ToString(FrmLogin.BookingInterwell);


                //dmnSearch.Select = "c.Fcid as Field1,c.CategoryName as Field2,ti.Tid Field3,ti.TableCode Field4,ti.OrderStatus Field5,ti.OrderMasterId Field6, " +
                //    "(select top (1) bd.BookmasterId from tblbookMaster bm  LEFT JOIN tblBookDetails bd on bm.BookmasterId = bd.BookmasterId where bm.Status = 1 and bd.Status = 1 and SYSDATETIME() >= DATEADD(MINUTE," + -Convert.ToDecimal(FrmLogin.BookingInterwell) + ", BookingDateTime) and MTID = ti.tid and rid=0  order by bm.BookingDateTime desc) Field7" +
                //    ",(select top (1) bm.BookingDateTime from tblbookMaster bm  LEFT JOIN tblBookDetails bd on bm.BookmasterId=bd.BookmasterId  where bm.Status = 1 and bd.Status = 1 and SYSDATETIME() >= DATEADD(MINUTE, " + -Convert.ToDecimal(FrmLogin.BookingInterwell) + ", BookingDateTime) and MTID = ti.tid and rid=0  order by bm.BookingDateTime desc)  EndDate" +
                //    ", (select NetAmount from tblOrderMaster where OrderMasterId=ti.OrderMasterId and Status in(1,2,3)) Field8";
                //dmnSearch.Table = " tblTable_info  ti " +
                //    " LEFT JOIN tblCategory_FTI c on c.Fcid=ti.Fcid "; //and SYSDATETIME() <= DATEADD(MINUTE," + Convert.ToDecimal(FrmLogin.BookingInterwell) + ",BookingDateTime)";
                //dmnSearch.OrderBy = "";

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
                var gridSource = SearchDetails.serachreport("TableView", dmnSearchtabledata);

                //dmnSearch.OrderBy = "group by c.Fcid ,c.CategoryName ,ti.Tid,ti.TableCode ,ti.OrderStatus ,ti.OrderMasterId ";
                //var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                bool flag = false;
                for (int j = 0; j < gridSource.Count; j++)
                {
                    if (gridSource[j].EndDate != null && gridSource[j].Field7 != null && Convert.ToInt32(gridSource[j].Field7) > 1 && Convert.ToInt32(gridSource[j].Field10) ==0) //gridSource[j].Field7 bookinmasterid gridSource[j].Field7 ordermasterid 
                    {
                        DateTime beforeAddMinutes = gridSource[j].EndDate.AddMinutes(-Convert.ToDouble(FrmLogin.BookingInterwell));
                        DateTime afterAddMinutes = gridSource[j].EndDate;//.AddMinutes(Convert.ToDouble(FrmLogin.BookingInterwell));
                        if (Convert.ToInt32(gridSource[j].Field7) > 0)//&& DateTime.Now > afterAddMinutes
                        {
                            CheckexpireTable(Convert.ToInt32(gridSource[j].Field3), Convert.ToInt32(gridSource[j].Field7));
                            flag = true;
                        }
                    }
                }
                if (flag)
                     gridSource = SearchDetails.serachreport("TableView", dmnSearchtabledata);
                //gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                var distinctCategories = gridSource.Select(item => item.Field1).Distinct().ToList();
                List<CategoryInfo> categoryList = new List<CategoryInfo>();
                foreach (var fcid in distinctCategories)
                {
                  string categoryName = gridSource.FirstOrDefault(item => item.Field1 == fcid)?.Field2;
                        if (categoryName != null)
                        {
                            categoryList.Add(new CategoryInfo { Fcid = Convert.ToInt32(fcid), CategoryName = categoryName });
                        }                       
                }
                
                int rowIndex_cate = 1;
                tlp_table_dashboard.Controls.Clear();

                #region Pickup Feature

                #region Pickup main heading tablelayoutpanel and label  Category
                    TableLayoutPanel tlp_categoryPkup = new TableLayoutPanel();
                    tlp_categoryPkup.Name = "tlp_category_Pickup";
                    tlp_categoryPkup.Tag = 0;
                    tlp_categoryPkup.Dock = DockStyle.Top;
                    tlp_categoryPkup.Controls.Clear();
                    tlp_categoryPkup.AutoSize = true;
                    tlp_categoryPkup.BackColor = Color.Transparent;
                    //tlp_category.BorderStyle = BorderStyle.FixedSingle;
                    //tlp_categoryPkup.CellBorderStyle =TableLayoutPanelCellBorderStyle.InsetDouble;

                    //Frist Row Cateory Heading
                    // Configure the Label Pickup
                    Label lblcategory_head_pkup = new Label();
                    lblcategory_head_pkup.Name = "lbcate_head_Pickup";
                    lblcategory_head_pkup.Tag = 0;
                    lblcategory_head_pkup.Text = "PICK UP";
                    lblcategory_head_pkup.ForeColor = Color.Black;
                    lblcategory_head_pkup.BackColor = Color.Transparent;
                    lblcategory_head_pkup.Dock = DockStyle.Top;
                    lblcategory_head_pkup.Font = new Font(lblcategory_head_pkup.Font.FontFamily, 12, FontStyle.Bold);

                 tlp_table_dashboard.Controls.Add(lblcategory_head_pkup, 0, rowIndex_cate);
                rowIndex_cate++;
                tlp_table_dashboard.Controls.Add(tlp_categoryPkup, 0, rowIndex_cate);
                rowIndex_cate++;
                #endregion

                #region Pickup box Adding with puls button
                int panelWidth = 0,panelHeight = 0,rowIndex = 1,columnIndex = 0;

                #region  TableLayoutPanel inner 
                // Configure the tlp_table_inner_pkup
                TableLayoutPanel tlp_table_inner_pkup = new TableLayoutPanel();
                tlp_table_inner_pkup.RowCount = 2;
                tlp_table_inner_pkup.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tlp_table_inner_pkup.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tlp_table_inner_pkup.Name = "tlpboxpkup_add";
                tlp_table_inner_pkup.Tag = 0;
                //tlp_table_inner_pkup.BorderStyle = BorderStyle.Fixed3D;
                tlp_table_inner_pkup.Dock = DockStyle.Left;
                tlp_table_inner_pkup.Margin = new Padding(15, 10, 15, 10);
                //tlp_table_inner_pkup.CellBorderStyle =TableLayoutPanelCellBorderStyle.InsetDouble;
                #endregion

                #region Adding Button with Icon
                Button btnaddpickup = new Button
                {
                    Name = "btn_add_pickup",
                    Tag = 0,
                    BackColor = Color.Transparent,
                    ForeColor = Color.Transparent,
                    Dock = DockStyle.Fill,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    Size = new Size(50, 50),
                    AccessibleDescription = "0",
                };
                btnaddpickup.FlatStyle = FlatStyle.Flat;
                btnaddpickup.FlatAppearance.BorderSize = 0;
                btnaddpickup.BackgroundImage = Properties.Resources.plus;
                ToolTip toolTip = new ToolTip();
                toolTip.SetToolTip(btnaddpickup, "Add New Pickup");
                btnaddpickup.Click += new EventHandler(Button_pickupClickEventHandler);
                #endregion

                #region  tablename  for table inside tablelayoutpanel inner
                // Configure the Label
                Label lbltablename_pkup = new Label();
                lbltablename_pkup.Name = "lbtablename_pkup";
                lbltablename_pkup.Tag = 0;
                lbltablename_pkup.Text = "Pick Up";
                lbltablename_pkup.ForeColor = Color.Black;
                lbltablename_pkup.BackColor = Color.Transparent;
                lbltablename_pkup.Font = new Font("Arial", 10, FontStyle.Bold);
                lbltablename_pkup.Dock = DockStyle.Fill;
                lbltablename_pkup.ForeColor = Defaultfontcolor;
                lbltablename_pkup.TextAlign = ContentAlignment.MiddleCenter;
                #endregion

                #region Size setting Pickup Box
                if (screenWidth > 1366)
                {
                    tlp_table_inner_pkup.Width = 300;
                    tlp_table_inner_pkup.Height = 100;
                }
                else if (screenWidth >= 1024 && screenWidth <= 1366)
                {
                    tlp_table_inner_pkup.Width = 120;
                    tlp_table_inner_pkup.Height = 80;
                }

                if (screenWidth > 1366)
                {
                    panelsPerRow = (int)Math.Floor((double)this.Width / (tlp_table_inner_pkup.Width + 30));
                }
                else if (screenWidth >= 1024 && screenWidth <= 1366)
                {
                    panelsPerRow = (int)Math.Floor((double)this.Width / (tlp_table_inner_pkup.Width + 30));
                }

                if (columnIndex == panelsPerRow)
                {
                    columnIndex = 0;
                    rowIndex++;
                }
                #endregion

                tlp_table_inner_pkup.Controls.Add(btnaddpickup, 0, 0);
                tlp_table_inner_pkup.Controls.Add(lbltablename_pkup , 0, 1);

                tlp_categoryPkup.Controls.Add(tlp_table_inner_pkup, pkcolumnIndex, pkrowIndex);
                pkcolumnIndex++;

                #endregion

                #endregion

                if (gridSource.Count > 0)
                {
                    tlp_table_dashboard.Visible = false;             
                    tlp_table_dashboard.AutoScroll = true;
                    tlp_table_dashboard.BackColor = Color.Transparent;        

                    foreach (var category in categoryList)
                    {
                        #region Pickup Order
                        if (Convert.ToInt32(category.Fcid) == 0)
                        {
                            var sortedList = gridSource
                                            .Where(item => Convert.ToInt32(item.Field10) == 1 && Convert.ToInt32(item.Field1) == category.Fcid)
                                            .OrderBy(item => item.Field11)
                                            .ToList();
                            for (int i = 0; i < sortedList.Count; i++)
                            {       
                                    #region Pickup box Fetching Orders  

                                    #region  TableLayoutPanel inner pickup
                                    // Configure the tlp_table_inner_pkup
                                    TableLayoutPanel tlp_table_inner_pkup_fetch = new TableLayoutPanel();
                                    tlp_table_inner_pkup_fetch.RowCount = 2;
                                    tlp_table_inner_pkup_fetch.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                    tlp_table_inner_pkup_fetch.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                    tlp_table_inner_pkup_fetch.Name = "tlpboxpkup_add_" + sortedList[i].Field11; 
                                    tlp_table_inner_pkup_fetch.Tag = sortedList[i].Field11;
                                    tlp_table_inner_pkup_fetch.BorderStyle = BorderStyle.Fixed3D;
                                    tlp_table_inner_pkup_fetch.Dock = DockStyle.Left;
                                    tlp_table_inner_pkup_fetch.Margin = new Padding(15, 10, 15, 10);
                                    tlp_table_inner_pkup_fetch.AccessibleDescription = sortedList[i].Field9;
                                    //tlp_table_inner_pkup_fetch.CellBorderStyle =TableLayoutPanelCellBorderStyle.InsetDouble;
                                    #endregion

                                    #region  tablename  for table inside tablelayoutpanel inner
                                    // Configure the Label
                                    Label lbltablename_pkup_fetch = new Label();
                                    lbltablename_pkup_fetch.Name = "lbtablename_pkup_" + sortedList[i].Field11;
                                    lbltablename_pkup_fetch.Tag = sortedList[i].Field11;
                                    lbltablename_pkup_fetch.Text = "Pick Up";
                                    lbltablename_pkup_fetch.ForeColor = Color.Black;
                                    lbltablename_pkup_fetch.BackColor = Color.Transparent;
                                    lbltablename_pkup_fetch.Font = new Font("Arial", 10, FontStyle.Bold);
                                    lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                                    lbltablename_pkup_fetch.ForeColor = Defaultfontcolor;
                                    lbltablename_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                    lbltablename_pkup_fetch.AccessibleDescription = sortedList[i].Field9;
                                    #endregion

                                    #region Net Amount
                                    // Configure the Label
                                    Label netamountlabel_pkup_fetch = new Label();
                                    netamountlabel_pkup_fetch.Name = "lb_netamount_pkup_" + sortedList[i].Field11;
                                    netamountlabel_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                    netamountlabel_pkup_fetch.Tag = sortedList[i].Field11;
                                    netamountlabel_pkup_fetch.Text = "\u20B9" + Convert.ToString(sortedList[i].Field12);
                                    netamountlabel_pkup_fetch.ForeColor = Netamountfontcolor;
                                    netamountlabel_pkup_fetch.BackColor = Color.Transparent;
                                    netamountlabel_pkup_fetch.Font = new Font("Arial", 10, FontStyle.Bold);
                                    netamountlabel_pkup_fetch.Dock = DockStyle.Fill;
                                    netamountlabel_pkup_fetch.Visible = false;
                                    netamountlabel_pkup_fetch.AccessibleDescription = sortedList[i].Field9;
                                #endregion

                                #region Netamount Show and hide order complete
                                BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                                dmnSearch.Select = "COUNT(*)  Field1";//;= SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS
                                dmnSearch.Table = "tblOrderDetails";
                                dmnSearch.WhereNdGroupBy = "WHERE status=3 and AddonStatus in(1,0) and OrderMasterId = " + sortedList[i].Field11;
                                dmnSearch.OrderBy = "";
                                var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                                if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                                {
                                    netamountlabel_pkup_fetch.Visible = true;
                                    tlp_table_inner_pkup_fetch.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                                    tlp_table_inner_pkup_fetch.RowStyles[1] = new RowStyle(SizeType.AutoSize);
                                    netamountlabel_pkup_fetch.Dock = DockStyle.Fill;
                                    lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                                    netamountlabel_pkup_fetch.Text = "\u20B9" + Convert.ToString(sortedList[0].Field2);
                                    lbltablename_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                    netamountlabel_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                }
                                else
                                {
                                    netamountlabel_pkup_fetch.Visible = false;
                                    tlp_table_inner_pkup_fetch.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                    tlp_table_inner_pkup_fetch.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                    lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                                    lbltablename_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                    netamountlabel_pkup_fetch.Text = "";
                                }
                                #endregion

                                #region Size setting Pickup Box
                                if (screenWidth > 1366)
                                    {
                                        tlp_table_inner_pkup_fetch.Width = 300;
                                        tlp_table_inner_pkup_fetch.Height = 100;
                                    }
                                    else if (screenWidth >= 1024 && screenWidth <= 1366)
                                    {
                                        tlp_table_inner_pkup_fetch.Width = 120;
                                        tlp_table_inner_pkup_fetch.Height = 80;
                                    }

                                    if (screenWidth > 1366)
                                    {
                                        panelsPerRow = (int)Math.Floor((double)this.Width / (tlp_table_inner_pkup_fetch.Width + 30));
                                    }
                                    else if (screenWidth >= 1024 && screenWidth <= 1366)
                                    {
                                        panelsPerRow = (int)Math.Floor((double)this.Width / (tlp_table_inner_pkup_fetch.Width + 30));
                                    }

                                    if (pkcolumnIndex == panelsPerRow)
                                    {
                                        pkcolumnIndex = 0;
                                        pkrowIndex++;
                                    }
                                    #endregion

                                    tlp_table_inner_pkup_fetch.Controls.Add(lbltablename_pkup_fetch, 0, 0);
                                    tlp_table_inner_pkup_fetch.Controls.Add(netamountlabel_pkup_fetch, 0, 1);

                                    lbltablename_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);
                                    netamountlabel_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);
                                    tlp_table_inner_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);

                                    tlp_categoryPkup.Controls.Add(tlp_table_inner_pkup_fetch, pkcolumnIndex, pkrowIndex);
                                    pkcolumnIndex++;

                                    #endregion                                
                            }
                        }
                        #endregion

                        #region Normal or Reservation Order
                        else
                        {
                            int countcateitem = 0;
                            TableLayoutPanel tlp_category = new TableLayoutPanel();

                            #region main heading tablelayoutpanel and label  Category
                            tlp_category.Name = "tlp_category_" + category.CategoryName;
                            tlp_category.Tag = category.CategoryName;
                            tlp_category.Dock = DockStyle.Top;
                            tlp_category.Controls.Clear();
                            //tlp_category.AutoScroll = true;
                            tlp_category.AutoSize = true;
                            tlp_category.BackColor = Color.Transparent;
                            //tlp_category.BorderStyle = BorderStyle.FixedSingle;
                            ///tlp_category.CellBorderStyle =TableLayoutPanelCellBorderStyle.InsetDouble;
                            //tlp_category.MouseHover += tlp_category_MouseHover;
                            //tlp_category.MouseLeave += tlp_category_MouseLeave;

                            //Frist Row Cateory Heading
                            // Configure the Label
                            Label lblcategory_head = new Label();
                            lblcategory_head.Name = "lbcate_head_" + category.CategoryName;
                            lblcategory_head.Tag = category.Fcid;
                            lblcategory_head.Text = category.CategoryName;
                            lblcategory_head.ForeColor = Color.Black;
                            lblcategory_head.BackColor = Color.Transparent;
                            lblcategory_head.Dock = DockStyle.Top;
                            lblcategory_head.Font = new Font(lblcategory_head.Font.FontFamily, 12, FontStyle.Bold);
                            #endregion

                            rowIndex = 1;
                            columnIndex = 0;

                            for (int i = 0; i < gridSource.Count; i++)
                            {
                                if (Convert.ToInt32(gridSource[i].Field1) == category.Fcid)
                                {
                                    panelWidth = 0;
                                    panelHeight = 0;

                                    #region  TableLayoutPanel inner 
                                    // Configure the TableLayoutPanel
                                    TableLayoutPanel tlp_table_inner = new TableLayoutPanel();
                                    tlp_table_inner.RowCount = 2;
                                    tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                    tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                    tlp_table_inner.Name = "tlpbox" + gridSource[i].Field4;
                                    tlp_table_inner.Tag = gridSource[i].Field3;
                                    tlp_table_inner.BorderStyle = BorderStyle.Fixed3D;
                                    tlp_table_inner.Dock = DockStyle.Left;
                                    tlp_table_inner.Margin = new Padding(15, 10, 15, 10);
                                    //tlp_table_inner.BackgroundImage = FoodandBeverage.Properties.Resources.bgdashboard_tiltlbar; ;
                                    //tlp_table_inner.BackgroundImageLayout = ImageLayout.Stretch;
                                    //tlp_table_inner.CellBorderStyle =TableLayoutPanelCellBorderStyle.InsetDouble;
                                    //tlp_table_inner.Width = 100;
                                    //tlp_table_inner.Height = 65;
                                    #endregion

                                    #region  tablename  for table inside tablelayoutpanel inner
                                    // Configure the Label
                                    Label lbltablename = new Label();
                                    lbltablename.Name = "lbtablename" + gridSource[i].Field4;
                                    lbltablename.Tag = gridSource[i].Field3;
                                    lbltablename.Text = gridSource[i].Field4;
                                    lbltablename.ForeColor = Color.Black;
                                    lbltablename.BackColor = Color.Transparent;
                                    lbltablename.Font = new Font("Arial", 10, FontStyle.Bold);
                                    if (Convert.ToInt32(gridSource[i].Field5) == 1)
                                        lbltablename.ForeColor = Ordertablefontcolor;
                                    else
                                        lbltablename.ForeColor = Defaultfontcolor;
                                    #endregion

                                    #region Net Amount
                                    // Configure the Label
                                    Label netamountlabel = new Label();
                                    netamountlabel.Name = "lb_netamount" + gridSource[i].Field4;
                                    netamountlabel.TextAlign = ContentAlignment.MiddleCenter;
                                    netamountlabel.Tag = gridSource[i].Field3;
                                    netamountlabel.Text = "\u20B9" + Convert.ToString(gridSource[i].Field8);
                                    netamountlabel.ForeColor = Netamountfontcolor;
                                    netamountlabel.BackColor = Color.Transparent;
                                    netamountlabel.Font = new Font("Arial", 10, FontStyle.Bold);
                                    netamountlabel.Dock = DockStyle.Fill;
                                    netamountlabel.Visible = false;
                                    #endregion         

                                    #region size setting
                                    if (screenWidth > 1366)
                                    {
                                        tlp_table_inner.Width = 300;
                                        tlp_table_inner.Height = 100;
                                    }
                                    else if (screenWidth >= 1024 && screenWidth <= 1366)
                                    {
                                        tlp_table_inner.Width = 120;
                                        tlp_table_inner.Height = 80;
                                    }

                                    if (screenWidth > 1366)
                                    {
                                        panelsPerRow = (int)Math.Floor((double)this.Width / (tlp_table_inner.Width + 30));
                                    }
                                    else if (screenWidth >= 1024 && screenWidth <= 1366)
                                    {
                                        panelsPerRow = (int)Math.Floor((double)this.Width / (tlp_table_inner.Width + 30));
                                    }

                                    if (columnIndex == panelsPerRow)
                                    {
                                        columnIndex = 0;
                                        rowIndex++;
                                    }
                                    #endregion

                                    tlp_table_inner.Controls.Add(lbltablename, 0, 0);
                                    tlp_table_inner.Controls.Add(netamountlabel, 0, 1);

                                    tlp_table_inner.AccessibleDescription = "0";
                                    lbltablename.AccessibleDescription = "0";
                                    netamountlabel.AccessibleDescription = "0";

                                    lbltablename.Click += new EventHandler(tableClickEventHandler);
                                    tlp_table_inner.Click += new EventHandler(tableClickEventHandler);
                                    netamountlabel.Click += new EventHandler(tableClickEventHandler);

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

                                    tlp_category.Controls.Add(tlp_table_inner, columnIndex, rowIndex);
                                    columnIndex++;
                                }
                            }

                            tlp_table_dashboard.Controls.Add(lblcategory_head, 0, rowIndex_cate);
                            rowIndex_cate++;
                            tlp_table_dashboard.Controls.Add(tlp_category, 0, rowIndex_cate);
                            rowIndex_cate++;
                        }
                        #endregion

                    }
                   //tlp_table_dashboard.BorderStyle = BorderStyle.FixedSingle;
                    this.Controls.Add(tlp_table_dashboard);
                    tlp_table_dashboard.Visible = true;
                    tlp_table_dashboard.Dock = DockStyle.Fill;
                    tlp_table_dashboard.Focus();

                    initTimer();
                }
                else                
                    CMessageBox.Show("No Records", "Information", MessageBoxButtons.OK);

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("TableView page  LoadTableStaus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }

        public void TableStaus(int serachmode)
        {
            try
            {
                AutoTimer.Stop();
                tlp_table_dashboard.SuspendLayout();
                DmnSearch dmnSearch = new DmnSearch();
                BllTableView<DmnGridFields> SearchDetails = new BllTableView<DmnGridFields>();
                DmnGridFields dmnSearchtabledata = new DmnGridFields();
                dmnSearchtabledata.Field30 = Convert.ToString(FrmLogin.BookingInterwell);
                switch (serachmode)
                {
                    case 0:
                        dmnSearchtabledata.Field29 = "0";
                        break;
                    case 1:
                        dmnSearchtabledata.Field29 = "1";
                        break;
                    case 2:
                        dmnSearchtabledata.Field29 = "2";
                        break;
                    case 3:
                        dmnSearchtabledata.Field29 = "3";
                        break;
                }
                var gridSource = SearchDetails.serachreport("TableView", dmnSearchtabledata);
                if (gridSource.Count > 0)
                {
                    bool flag = false;
                    for (int j = 0; j < gridSource.Count; j++)
                    {
                        if (gridSource[j].EndDate != null && gridSource[j].Field7 != null && Convert.ToInt32(gridSource[j].Field7) > 1 && Convert.ToInt32(gridSource[j].Field10) == 0) //gridSource[j].Field7 bookinmasterid gridSource[j].Field7 ordermasterid 
                        {
                            DateTime beforeAddMinutes = gridSource[j].EndDate.AddMinutes(-Convert.ToDouble(FrmLogin.BookingInterwell));
                            DateTime afterAddMinutes = gridSource[j].EndDate;//.AddMinutes(Convert.ToDouble(FrmLogin.BookingInterwell));
                            if (Convert.ToInt32(gridSource[j].Field7) > 0)//
                            {
                                CheckexpireTable(Convert.ToInt32(gridSource[j].Field3), Convert.ToInt32(gridSource[j].Field7));
                                flag = true;
                            }
                        }
                    }
                    if (flag)
                        gridSource = SearchDetails.serachreport("TableView", dmnSearchtabledata);

                    var distinctCategories = gridSource.Select(item => item.Field1).Distinct().ToList();
                    List<CategoryInfo> categoryList = new List<CategoryInfo>();
                    foreach (var fcid in distinctCategories)
                    {
                        string categoryName = gridSource.FirstOrDefault(item => item.Field1 == fcid)?.Field2;

                        if (categoryName != null)
                        {
                            categoryList.Add(new CategoryInfo { Fcid = Convert.ToInt32(fcid), CategoryName = categoryName });
                        }
                    }
                    foreach (var category in categoryList)
                    {
                        #region Pickup Order
                        if (Convert.ToInt32(category.Fcid) == 0)
                        {
                            var sortedList = gridSource
                                            .Where(item => Convert.ToInt32(item.Field10) == 1 && Convert.ToInt32(item.Field1) == category.Fcid)
                                            .OrderBy(item => item.Field11)
                                            .ToList();
                            for (int i = 0; i < sortedList.Count; i++)
                            {
                                TableLayoutPanel tlp_table_pickup_main = this.Controls.Find("tlp_category_Pickup", true).FirstOrDefault() as TableLayoutPanel;
                                string tlpboxname = "tlpboxpkup_add_" + sortedList[i].Field11;
                                string lbtable = "lbtablename_pkup_" + sortedList[i].Field11;
                                string lblnetamountname = "lb_netamount_pkup_" + sortedList[i].Field11;

                                TableLayoutPanel tlp_table_pbox = this.Controls.Find(tlpboxname, true).FirstOrDefault() as TableLayoutPanel;
                                Label lbltablename_p = this.Controls.Find(lbtable, true).FirstOrDefault() as Label;
                                Label netamountlabel_p = this.Controls.Find(lblnetamountname, true).FirstOrDefault() as Label;
                                if (tlp_table_pbox != null)//update 
                                {
                                    #region Netamount Show and hide order complete
                                    BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                                    dmnSearch.Select = "COUNT(*)  Field1";//;= SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS
                                    dmnSearch.Table = "tblOrderDetails";
                                    dmnSearch.WhereNdGroupBy = "WHERE status=3 and AddonStatus in(1,0) and OrderMasterId = " + sortedList[i].Field11;
                                    dmnSearch.OrderBy = "";
                                    var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                                    if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                                    {
                                        netamountlabel_p.Visible = true;
                                        tlp_table_pbox.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                                        tlp_table_pbox.RowStyles[1] = new RowStyle(SizeType.AutoSize);
                                        netamountlabel_p.Dock = DockStyle.Fill;
                                        lbltablename_p.Dock = DockStyle.Fill;
                                        netamountlabel_p.Text = "\u20B9" + Convert.ToString(sortedList[0].Field2);
                                        lbltablename_p.TextAlign = ContentAlignment.MiddleCenter;
                                        netamountlabel_p.TextAlign = ContentAlignment.MiddleCenter;
                                    }
                                    else
                                    {

                                        netamountlabel_p.Visible = false;
                                        tlp_table_pbox.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                        tlp_table_pbox.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                        lbltablename_p.Dock = DockStyle.Fill;
                                        lbltablename_p.TextAlign = ContentAlignment.MiddleCenter;
                                        netamountlabel_p.Text = "";
                                    }
                                    #endregion
                                }
                                else // insert new 
                                {
                                    int pkrowIndex=0, pkcolumnIndex=0;
                                    int totalcount_pk = tlp_table_pickup_main.Controls.OfType<TableLayoutPanel>().Count();
                                    pkcolumnIndex = totalcount_pk ;
                                    #region Pickup box Fetching Orders  

                                    #region  TableLayoutPanel inner pickup
                                    // Configure the tlp_table_inner_pkup
                                    TableLayoutPanel tlp_table_inner_pkup_fetch = new TableLayoutPanel();
                                    tlp_table_inner_pkup_fetch.RowCount = 2;
                                    tlp_table_inner_pkup_fetch.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                    tlp_table_inner_pkup_fetch.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                    tlp_table_inner_pkup_fetch.Name = "tlpboxpkup_add_" + sortedList[i].Field11; 
                                    tlp_table_inner_pkup_fetch.Tag = sortedList[i].Field11;
                                    tlp_table_inner_pkup_fetch.BorderStyle = BorderStyle.Fixed3D;
                                    tlp_table_inner_pkup_fetch.Dock = DockStyle.Left;
                                    tlp_table_inner_pkup_fetch.Margin = new Padding(15, 10, 15, 10);
                                    tlp_table_inner_pkup_fetch.AccessibleDescription = sortedList[i].Field9;
                                    //tlp_table_inner_pkup_fetch.CellBorderStyle =TableLayoutPanelCellBorderStyle.InsetDouble;
                                    #endregion

                                    #region  tablename  for table inside tablelayoutpanel inner
                                    // Configure the Label
                                    Label lbltablename_pkup_fetch = new Label();
                                    lbltablename_pkup_fetch.Name = "lbtablename_pkup_" + sortedList[i].Field11;
                                    lbltablename_pkup_fetch.Tag = sortedList[i].Field11;
                                    lbltablename_pkup_fetch.Text = "Pick Up";
                                    lbltablename_pkup_fetch.ForeColor = Color.Black;
                                    lbltablename_pkup_fetch.BackColor = Color.Transparent;
                                    lbltablename_pkup_fetch.Font = new Font("Arial", 10, FontStyle.Bold);
                                    lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                                    lbltablename_pkup_fetch.ForeColor = Defaultfontcolor;
                                    lbltablename_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                    lbltablename_pkup_fetch.AccessibleDescription = sortedList[i].Field9;
                                    #endregion

                                    #region Net Amount
                                    // Configure the Label
                                    Label netamountlabel_pkup_fetch = new Label();
                                    netamountlabel_pkup_fetch.Name = "lb_netamount_pkup_" + sortedList[i].Field11;
                                    netamountlabel_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                    netamountlabel_pkup_fetch.Tag = sortedList[i].Field11;
                                    netamountlabel_pkup_fetch.Text = "\u20B9" + Convert.ToString(sortedList[i].Field12);
                                    netamountlabel_pkup_fetch.ForeColor = Netamountfontcolor;
                                    netamountlabel_pkup_fetch.BackColor = Color.Transparent;
                                    netamountlabel_pkup_fetch.Font = new Font("Arial", 10, FontStyle.Bold);
                                    netamountlabel_pkup_fetch.Dock = DockStyle.Fill;
                                    netamountlabel_pkup_fetch.Visible = false;
                                    netamountlabel_pkup_fetch.AccessibleDescription = sortedList[i].Field9;
                                    #endregion

                                    #region Netamount Show and hide order complete
                                    BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                                    dmnSearch.Select = "COUNT(*)  Field1";//;= SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS
                                    dmnSearch.Table = "tblOrderDetails";
                                    dmnSearch.WhereNdGroupBy = "WHERE status=3 and AddonStatus in(1,0) and OrderMasterId = " + sortedList[i].Field11;
                                    dmnSearch.OrderBy = "";
                                    var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                                    if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                                    {
                                        netamountlabel_pkup_fetch.Visible = true;
                                        tlp_table_inner_pkup_fetch.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                                        tlp_table_inner_pkup_fetch.RowStyles[1] = new RowStyle(SizeType.AutoSize);
                                        netamountlabel_pkup_fetch.Dock = DockStyle.Fill;
                                        lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                                        netamountlabel_pkup_fetch.Text = "\u20B9" + Convert.ToString(sortedList[0].Field2);
                                        lbltablename_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                        netamountlabel_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                    }
                                    else
                                    {
                                        netamountlabel_pkup_fetch.Visible = false;
                                        tlp_table_inner_pkup_fetch.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                        tlp_table_inner_pkup_fetch.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                        lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                                        lbltablename_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                        netamountlabel_pkup_fetch.Text = "";
                                    }
                                    #endregion

                                    #region Size setting Pickup Box
                                    if (screenWidth > 1366)
                                    {
                                        tlp_table_inner_pkup_fetch.Width = 300;
                                        tlp_table_inner_pkup_fetch.Height = 100;
                                    }
                                    else if (screenWidth >= 1024 && screenWidth <= 1366)
                                    {
                                        tlp_table_inner_pkup_fetch.Width = 120;
                                        tlp_table_inner_pkup_fetch.Height = 80;
                                    }

                                    if (screenWidth > 1366)
                                    {
                                        panelsPerRow = (int)Math.Floor((double)this.Width / (tlp_table_inner_pkup_fetch.Width + 30));
                                    }
                                    else if (screenWidth >= 1024 && screenWidth <= 1366)
                                    {
                                        panelsPerRow = (int)Math.Floor((double)this.Width / (tlp_table_inner_pkup_fetch.Width + 30));
                                    }

                                    pkrowIndex =( (int)totalcount_pk / (int)panelsPerRow ) + 1;

                                    if (totalcount_pk == panelsPerRow)
                                    {
                                        totalcount_pk = 0; 
                                         pkrowIndex++;
                                    }
                                    #endregion

                                    tlp_table_inner_pkup_fetch.Controls.Add(lbltablename_pkup_fetch, 0, 0);
                                    tlp_table_inner_pkup_fetch.Controls.Add(netamountlabel_pkup_fetch, 0, 1);

                                    lbltablename_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);
                                    netamountlabel_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);
                                    tlp_table_inner_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);

                                    tlp_table_pickup_main.Controls.Add(tlp_table_inner_pkup_fetch, pkcolumnIndex, pkrowIndex);
                                    pkcolumnIndex++;

                                    #endregion
                                }

                            }
                        }
                        #endregion

                        #region Normal or Reservation Order 
                        else
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                string tlpboxname = "tlpbox" + gridSource[i].Field4;
                                string lbtable = "lbtablename" + gridSource[i].Field4;
                                string lblnetamountname = "lb_netamount" + gridSource[i].Field4;

                                TableLayoutPanel tlp_table_inner = this.Controls.Find(tlpboxname, true).FirstOrDefault() as TableLayoutPanel;
                                Label lbltablename = this.Controls.Find(lbtable, true).FirstOrDefault() as Label;
                                Label netamountlabel = this.Controls.Find(lblnetamountname, true).FirstOrDefault() as Label;
                                if (tlp_table_inner == null)
                                {
                                    AutoTimer.Stop();
                                    return;
                                }

                                if (Convert.ToInt32(gridSource[i].Field1) == category.Fcid)
                                {
                                    if (Convert.ToInt32(gridSource[i].Field5) == 1)
                                        lbltablename.ForeColor = Ordertablefontcolor;
                                    else
                                        lbltablename.ForeColor = Defaultfontcolor;

                                    tlp_table_inner.AccessibleDescription = "0";
                                    lbltablename.AccessibleDescription = "0";

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
                                    else if (Convert.ToInt32(gridSource[i].Field5) == 0)//reservation booking order
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
                        }
                        #endregion
                    }
                }
                tlp_table_dashboard.ResumeLayout();
                tlp_table_dashboard.Dock = DockStyle.Fill;
                initTimer();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("TableView page  TableStaus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }

        }

        private void Button_pickupClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                AutoTimer.Stop();
                CheckedtableNames.Clear();
                Control clickedControl = sender as Control;
                int Ptype = Convert.ToInt32(clickedControl.AccessibleDescription);
                if (clickedControl != null)
                {
                    string tableCode = clickedControl.Name;
                    if (Ptype == Convert.ToInt32(PickupType.AddNew))
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
                                _instanceFrmOrder.ShowDialog();
                                _instanceFrmOrder.BringToFront();

                            }
                        }
                        else
                        {
                            if (clickedControl.Tag != null)
                            {
                                FrmOrder _instanceFrmOrder = new FrmOrder(0, Convert.ToInt32(DashboardTableType.PickupOrder), 0, CheckedtableNames, Convert.ToInt32(PageType.Billpage),0);
                                _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                _instanceFrmOrder.Opacity = 0;
                                _instanceFrmOrder.Visible = false;
                                _instanceFrmOrder.ShowDialog();
                                _instanceFrmOrder.BringToFront();
                            }
                        }

                    }
                    //else if (Ptype == Convert.ToInt32(PickupType.Existing))
                    //{
                    //    FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(0), CheckedtableNames, Convert.ToInt32(PageType.OrderPage),0);
                    //    _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    //    _instanceFrmOrder.Opacity = 0;
                    //    _instanceFrmOrder.Visible = false;
                    //    _instanceFrmOrder.Show();
                    //    _instanceFrmOrder.BringToFront();
                    //}
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("TableView page  Button_pickupClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("TableView page  CheckexpireTable()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }
        public void initTimer()
        {
            AutoTimer.Start();
        }
        private void PickupboxClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                AutoTimer.Stop();
                CheckedtableNames.Clear();
                Control clickedControl = sender as Control;
                int  pkid =Convert.ToInt32(clickedControl.AccessibleDescription);
                if (clickedControl != null)
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
                            _instanceFrmOrder.ShowDialog();
                            _instanceFrmOrder.BringToFront();
                        }
                    }
                    else
                    {
                        if (clickedControl.Tag != null)
                        {
                            FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.PickupOrder), 0, CheckedtableNames, Convert.ToInt32(PageType.OrderPage),pkid);
                            _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                            _instanceFrmOrder.Opacity = 0;
                            _instanceFrmOrder.Visible = false;
                            _instanceFrmOrder.ShowDialog();
                            _instanceFrmOrder.BringToFront();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("TableView page  PickupboxClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
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
                                FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Normalorder),0,CheckedtableNames, Convert.ToInt32(PageType.OrderPage),0);
                                _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                _instanceFrmOrder.Opacity = 0;
                                _instanceFrmOrder.Visible = false;
                                _instanceFrmOrder.ShowDialog();
                                _instanceFrmOrder.BringToFront();
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
                            FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid),CheckedtableNames, Convert.ToInt32(PageType.OrderPage),0);
                            _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                            _instanceFrmOrder.Opacity = 0;
                            _instanceFrmOrder.Visible = false;
                            _instanceFrmOrder.Show();
                            _instanceFrmOrder.BringToFront();

                        }
                    }
                    else if (Btype == Convert.ToInt32(DashboardTableType.Reverse_Table))
                    {
                        FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid),CheckedtableNames, Convert.ToInt32(PageType.OrderPage),0);
                        _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        _instanceFrmOrder.Opacity = 0;
                        _instanceFrmOrder.Visible = false;
                        _instanceFrmOrder.Show();
                        _instanceFrmOrder.BringToFront();
                    }
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("TableView page  tableClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }
        private void tlp_category_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ToolTip toolTip1 = new ToolTip();
                TableLayoutPanel tlpcate = sender as TableLayoutPanel;
                string tlpName = "tlp_category_com_" + tlpcate.Tag;
                // Find the TableLayoutPanel control by name
                TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
                if (tlp != null)
                {
                    tlp.BackColor = Color.LightBlue;
                    // Show tooltip with the item name
                    toolTip1.SetToolTip(tlp, tlpcate.Tag.ToString());
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

        private static void RunMethodInSeparateThread(Action action)
        {
            var thread = new Thread(new ThreadStart(action));
            thread.Start();
        }

        private void AutoTimer_Tick(object sender, EventArgs e)
        {
            //RunMethodInSeparateThread(() => TableStaus(0));
            TableStaus(0);
        }


    }
}
