using BLL;
using CustomMessageBox;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FoodandBeverage.Interfaces;
using FoodandBeverage.Forms.UserAccount;
using FoodandBeverage.Forms.UserControls;
using System.Threading.Tasks;
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
                //dmnSearch.Select = "c.Fcid as Field1,c.CategoryName as Field2,ti.Tid Field3,ti.TableCode Field4,ti.OrderStatus Field5,ti.OrderMasterId Field6, " +
                //    "(select top (1) bd.BookmasterId from tblbookMaster bm  LEFT JOIN tblBookDetails bd on bm.BookmasterId = bd.BookmasterId and bm.Status = 1 where bm.Status = 1 and bd.Status = 1 and SYSDATETIME() >= DATEADD(MINUTE, -3, BookingDateTime) and MTID = ti.tid ) Field7" +
                //    ",(select top (1) bm.BookingDateTime from tblbookMaster bm  LEFT JOIN tblBookDetails bd on bm.BookmasterId=bd.BookmasterId and bm.Status=1 where bm.Status = 1 and bd.Status = 1 and SYSDATETIME() >= DATEADD(MINUTE, -3, BookingDateTime) and MTID = ti.tid )  EndDate";
                //dmnSearch.Table = " tblTable_info  ti " +
                //    " LEFT JOIN tblCategory_FTI c on c.Fcid=ti.Fcid ";//and SYSDATETIME() <= DATEADD(MINUTE," + Convert.ToDecimal(FrmLogin.BookingInterwell) + ",BookingDateTime)";
                //dmnSearch.OrderBy = "";

                dmnSearch.Select = "c.Fcid as Field1,c.CategoryName as Field2,ti.Tid Field3,ti.TableCode Field4,ti.OrderStatus Field5,ti.OrderMasterId Field6, " +
                    "(select top (1) bd.BookmasterId from tblbookMaster bm  LEFT JOIN tblBookDetails bd on bm.BookmasterId = bd.BookmasterId where bm.Status = 1 and bd.Status = 1 and SYSDATETIME() >= DATEADD(MINUTE," + -Convert.ToDecimal(FrmLogin.BookingInterwell) + ", BookingDateTime) and MTID = ti.tid and rid=0  order by bm.BookingDateTime desc) Field7" +
                    ",(select top (1) bm.BookingDateTime from tblbookMaster bm  LEFT JOIN tblBookDetails bd on bm.BookmasterId=bd.BookmasterId  where bm.Status = 1 and bd.Status = 1 and SYSDATETIME() >= DATEADD(MINUTE, " + -Convert.ToDecimal(FrmLogin.BookingInterwell) + ", BookingDateTime) and MTID = ti.tid and rid=0  order by bm.BookingDateTime desc)  EndDate" +
                    ", (select NetAmount from tblOrderMaster where OrderMasterId=ti.OrderMasterId and Status in(1,2,3)) Field8";
                dmnSearch.Table = " tblTable_info  ti " +
                    " LEFT JOIN tblCategory_FTI c on c.Fcid=ti.Fcid ";//and SYSDATETIME() <= DATEADD(MINUTE," + Convert.ToDecimal(FrmLogin.BookingInterwell) + ",BookingDateTime)";
                dmnSearch.OrderBy = "";
                switch (serachmode)
                {
                    case 0:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1";
                        break;
                    case 1:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=1";
                        break;
                    case 2:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=1 and ti.OrderMasterId >0";
                        break;
                    case 3:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=0 and ti.OrderMasterId=0";
                        break;
                }
                dmnSearch.OrderBy = "group by c.Fcid ,c.CategoryName ,ti.Tid,ti.TableCode ,ti.OrderStatus ,ti.OrderMasterId ";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                bool flag = false;
                for (int j = 0; j < gridSource.Count; j++)
                {
                    if (gridSource[j].EndDate != null && gridSource[j].Field7 != null && Convert.ToInt32(gridSource[j].Field7) > 1) //gridSource[j].Field7 bookinmasterid gridSource[j].Field7 ordermasterid 
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
                    gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

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

                if (gridSource.Count > 0)
                {
                    tlp_table_dashboard.Visible = false;
                    tlp_table_dashboard.Controls.Clear();                   
                    tlp_table_dashboard.AutoScroll = true;
                    tlp_table_dashboard.BackColor = Color.Transparent;
                    int rowIndex_cate = 1;

                    foreach (var category in categoryList)
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

                        int rowIndex = 1;
                        int columnIndex = 0;

                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            if (Convert.ToInt32(gridSource[i].Field1) == category.Fcid)
                            {
                                int panelWidth = 0;
                                int panelHeight = 0;

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
                                    lbltablename.ForeColor =  Ordertablefontcolor;
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
                                    dmnSearch.Select = "CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1";
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
                                    }
                                    else
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
                                            dmnSearch.Select = "CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1";
                                            dmnSearch.Table = "tblOrderDetails";
                                            dmnSearch.WhereNdGroupBy = "WHERE status!=0 and OrderMasterId = " + gridSource[i].Field6;
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
                                            }
                                            else
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

                                            lbltablename.ForeColor = Ordertablefontcolor;
                                            tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                            lbltablename.AccessibleDescription = Convert.ToString(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
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
                                                lbltablename.AccessibleDescription = Convert.ToString(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
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
                    //tlp_table_dashboard.BorderStyle = BorderStyle.FixedSingle;
                    this.Controls.Add(tlp_table_dashboard);
                    tlp_table_dashboard.Visible = true;
                    tlp_table_dashboard.Dock = DockStyle.Fill;

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
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "c.Fcid as Field1,c.CategoryName as Field2,ti.Tid Field3,ti.TableCode Field4,ti.OrderStatus Field5,ti.OrderMasterId Field6, " +
                    "(select top (1) bd.BookmasterId from tblbookMaster bm  LEFT JOIN tblBookDetails bd on bm.BookmasterId = bd.BookmasterId where bm.Status = 1 and bd.Status = 1 and SYSDATETIME() >= DATEADD(MINUTE, " + -Convert.ToDecimal(FrmLogin.BookingInterwell) + ", BookingDateTime) and MTID = ti.tid and rid=0  order by bm.BookingDateTime desc) Field7" +
                    ",(select top (1) bm.BookingDateTime from tblbookMaster bm  LEFT JOIN tblBookDetails bd on bm.BookmasterId=bd.BookmasterId  where bm.Status = 1 and bd.Status = 1 and SYSDATETIME() >= DATEADD(MINUTE, " + -Convert.ToDecimal(FrmLogin.BookingInterwell) + ", BookingDateTime) and MTID = ti.tid and rid=0  order by bm.BookingDateTime desc)  EndDate" +
                   ", (select NetAmount from tblOrderMaster where OrderMasterId=ti.OrderMasterId and Status in(1,2,3)) Field8";
                dmnSearch.Table = " tblTable_info  ti " +
                    " LEFT JOIN tblCategory_FTI c on c.Fcid=ti.Fcid ";//and SYSDATETIME() <= DATEADD(MINUTE," + Convert.ToDecimal(FrmLogin.BookingInterwell) + ",BookingDateTime)";
                dmnSearch.OrderBy = "";
                switch (serachmode)
                {
                    case 0:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1";
                        break;
                    case 1:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=1";
                        break;
                    case 2:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.RevStatus=1";
                        break;
                    case 3:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=0 and ti.RevStatus=0";
                        break;
                }
                dmnSearch.OrderBy = "group by c.Fcid ,c.CategoryName ,ti.Tid,ti.TableCode ,ti.OrderStatus ,ti.OrderMasterId ";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    bool flag = false;
                    for (int j = 0; j < gridSource.Count; j++)
                    {
                        if (gridSource[j].EndDate != null && gridSource[j].Field7 != null && Convert.ToInt32(gridSource[j].Field7) > 1) //gridSource[j].Field7 bookinmasterid gridSource[j].Field7 ordermasterid 
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
                        gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

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
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            string tlpboxname = "tlpbox" + gridSource[i].Field4;
                            string lbtable = "lbtablename" + gridSource[i].Field4;
                            string lblnetamountname = "lb_netamount" + gridSource[i].Field4;

                            TableLayoutPanel tlp_table_inner = this.Controls.Find(tlpboxname, true).FirstOrDefault() as TableLayoutPanel;
                            Label lbltablename = this.Controls.Find(lbtable, true).FirstOrDefault() as Label;
                            Label netamountlabel = this.Controls.Find(lblnetamountname, true).FirstOrDefault() as Label;
                            if(tlp_table_inner==null)
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
                                    dmnSearch.Select = "CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1,(select NetAmount from tblOrderMaster where Status!=0 and OrderMasterId =" + gridSource[i].Field6+ ") Field2";
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
                                        netamountlabel.Text= "\u20B9" + Convert.ToString(gridSource_order[0].Field2);
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
                                            dmnSearch.Select = "CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1";
                                            dmnSearch.Table = "tblOrderDetails";
                                            dmnSearch.WhereNdGroupBy = "WHERE status!=0 and OrderMasterId = " + gridSource[i].Field6;
                                            dmnSearch.OrderBy = "";
                                            var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);
                                            if (gridSource_order.Count > 0)
                                            {
                                                if (Convert.ToInt32(gridSource_order[0].Field1) == 1)
                                                {
                                                    netamountlabel.Visible = false;
                                                    tlp_table_inner.BackColor = OrderColor;
                                                    tlp_table_inner.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                                    tlp_table_inner.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                                    lbltablename.Dock = DockStyle.Fill;
                                                    lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                                                }
                                                else
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

                                                lbltablename.ForeColor = Ordertablefontcolor;
                                                tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                                lbltablename.AccessibleDescription = Convert.ToString(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
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
                                                lbltablename.AccessibleDescription = Convert.ToString(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;

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
                                FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Normalorder),0,CheckedtableNames);
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
                            FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid),CheckedtableNames);
                            _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                            _instanceFrmOrder.Opacity = 0;
                            _instanceFrmOrder.Visible = false;
                            _instanceFrmOrder.Show();
                            _instanceFrmOrder.BringToFront();

                        }
                    }
                    else if (Btype == Convert.ToInt32(DashboardTableType.Reverse_Table))
                    {
                        FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid),CheckedtableNames);
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
