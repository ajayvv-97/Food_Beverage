using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Controls;
using FoodandBeverage.Forms.UserControl;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmCombinedBill : Form
    {
        public FrmCombinedBill()
        {
            InitializeComponent();
        }

        #region Declaration
        public List<DmnNameList> CheckedtableNames = new List<DmnNameList>();      

        public int panelsPerRow = 0;
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        ClsGeneral clsgen = new ClsGeneral();

        bool flagDrawerF = false, flagDrawer = false;

        public bool ListFillFlag = false;

        public Color CombinedBillcolor = Color.Wheat;//System.Drawing.ColorTranslator.FromHtml("#e3dbdb");
        public Color NormalBillingColor = Color.White;//System.Drawing.ColorTranslator.FromHtml("#9f9a9a");
        public Color Netamountcolor = Color.Green;//System.Drawing.ColorTranslator.FromHtml("#9f9a9a");
        public Color Description_color = Color.Black;//System.Drawing.ColorTranslator.FromHtml("#9f9a9a");

        public int categoryId = 0;

        private ToolTip menuTooltip = new ToolTip();
        private Image menuButtonImage = (Image)Properties.Resources.fb_sidemenu.Clone();
        private readonly Image imgPlus = Properties.Resources.Billaddicon;
        private readonly Image imgTick = Properties.Resources.button_complete_order;

        #endregion

        /*permission checking for manager and supervisor*/
        public string Drawerassginstaffname;
        public string Drawerassginstaffdes;
        public int Drawerassginid;
        public int Drawerassginstaffid;

        public string Drawersystemname;
        FoodandBeverage.Forms.FrmLogin existingLogin = Application.OpenForms.OfType<FoodandBeverage.Forms.FrmLogin>().FirstOrDefault();

        #region Grid Declaration
        public  int cslno = 0;
        public  int actionno = 1;
        public  int orderdatetimeno = 2;
        public  int entryno = 3;
        public  int tablecodeno = 4;
        public  int totalqtyno = 5;
        public  int netamountno = 6;
        public  int recamountno = 7;

        public  int balamountno = 8;
        public  int paytypename = 9;
        public  int statusno = 10;
        public int Salemasteridno = 11;
        public int partybookingidno = 12;
        public int tableidno = 13;
        public int pickupidno = 14;

        public  int sslno = 0;
        public  int spaymentamtno = 1;
        public  int spaymentmodeno = 2;
        public  int ssalemasteridno = 3;
        public  int spayidno = 4;
        public  int sactionno = 5;

        #endregion

        private void FrmCombinedBill_Load(object sender, EventArgs e)
        {
            try
            {
                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    AppSession.RegisterForm(this, (int)LogoutFormList.billing);
                }
         
                dgvorderview.BorderStyle = BorderStyle.None;
                dgvorderview.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvorderview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvorderview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgvorderview.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvorderview.BackgroundColor = Color.White;

                dgvorderview.EnableHeadersVisualStyles = false;
                dgvorderview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvorderview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvorderview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                DrawerCheck();
            }
            catch (Exception ex)
            {
                clsgen.LogException("CombinedBill Page FrmCombinedBill_Load", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }

        }
        private void SideMenuClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            categoryId = Convert.ToInt32(btn.Tag);
            flp_sidemenu.Visible = false;
        }

        #region Optimized Bill Code (LABEL BASED SELECTION)

        /* =========================
         *  DATA STRUCTURES
         * ========================= */

        private class TableControls
        {
            public ModernCombinedBillCard Card { get; set; }
        }

        private readonly Dictionary<int, TableControls> tableCache =
            new Dictionary<int, TableControls>();
        private readonly HashSet<int> selectedTableIds = new HashSet<int>();
        private readonly Dictionary<int, string> tableAmountCache =
            new Dictionary<int, string>();

        private string BuildCombinedBillDescription(
            int dashboardType,
            object orderMasterId,
            object tableId,
            string tableCode,
            int bookingMasterId,
            int pickupId)
        {
            return dashboardType + "," + Convert.ToString(orderMasterId) + "," +
                   Convert.ToString(tableId) + "," + Convert.ToString(tableCode) + "," +
                   bookingMasterId + "," + pickupId;
        }

        private string ReadCardAmount(ModernCombinedBillCard card)
        {
            if (card == null)
                return string.Empty;

            int tid;
            return int.TryParse(card.Name.Replace("tlp_com", string.Empty), out tid) &&
                   tableAmountCache.TryGetValue(tid, out string amount)
                ? amount
                : string.Empty;
        }
        private bool isUserSelecting = false;

        #endregion

        private void EnableDoubleBuffer(Control c)
        {
            typeof(Control)
                .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(c, true, null);
        }

        public void Billcomplete_Fill()
        {
            try
            {
                SuspendLayout();
                pnl_complebill_prevbill.Visible = true;
                pnl_empty_msg.Visible = false;

                flp_billlayout.SuspendLayout();
                flp_billlayout.Controls.Clear();
                tableCache.Clear();
                tableAmountCache.Clear();
                flp_billlayout.Dock = DockStyle.Fill;
                flp_billlayout.AutoScroll = true;
                flp_billlayout.FlowDirection = FlowDirection.LeftToRight;
                flp_billlayout.WrapContents = true;
                EnableDoubleBuffer(flp_billlayout);

                var gridSource = new BllTableData<DmncombinedBill>()
                    .serachreport("prdBillData", new DmncombinedBill
                    {
                        BookingInterwell = Convert.ToDecimal(FrmLogin.BookingInterwell),
                        Fcid = categoryId
                    });

                foreach (var tabledata in gridSource)
                {
                    int tid = Convert.ToInt32(tabledata.Tid);
                    if (tid <= 0)
                        continue;

                    bool isBooking = tabledata.BookmasterId > 0 ||
                                     Convert.ToInt32(tabledata.Cbid) > 0;
                    string description = BuildCombinedBillDescription(
                        Convert.ToInt32(isBooking
                            ? DashboardTableType.Reverse_overtime_Cancel
                            : DashboardTableType.Normalorder),
                        tabledata.OrderMasterId, tabledata.Tid, tabledata.TableCode,
                        Convert.ToInt32(tabledata.BookmasterId), Convert.ToInt32(tabledata.pkid));

                    string amount = "₹" + Convert.ToString(tabledata.NetAmount);
                    tableAmountCache[tid] = amount;

                    ModernCombinedBillCard card = new ModernCombinedBillCard
                    {
                        Name = "tlp_com" + tid,
                        Size = new Size(140, 110),
                        Margin = new Padding(5),
                        Visible = tabledata.BillStatus == 0
                    };
                    card.SelectRequested += CombinedBillCard_SelectRequested;
                    card.OpenRequested += CompleteEventHandler;
                    card.SetBillData(
                        tabledata.TableCode, amount, description,
                        Convert.ToInt32(isBooking ? CombinedBillType.BookingBill : CombinedBillType.Normal),
                        isBooking, selectedTableIds.Contains(tid));

                    tableCache[tid] = new TableControls { Card = card };
                    flp_billlayout.Controls.Add(card);
                }

                flp_billlayout.ResumeLayout(true);
                CombineBillAutoTimer.Start();
                SetEmptyIcon();
                ResumeLayout(true);
            }
            catch (Exception ex)
            {
                flp_billlayout.ResumeLayout(true);
                clsgen.LogException("Billcomplete_Fill()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.CombinedBillpage));
            }
        }

        /// <summary>
        /// Refreshes combined-bill cards after bill data changes.
        /// Called by FrmOrderConsole, so this method intentionally remains public.
        /// </summary>
        public void BillCombineBill_Update()
        {
            if (isUserSelecting || IsDisposed || Disposing)
                return;

            try
            {
                var gridSource = new BllTableData<DmncombinedBill>()
                    .serachreport("prdBillData", new DmncombinedBill
                    {
                        BookingInterwell = Convert.ToDecimal(FrmLogin.BookingInterwell),
                        Fcid = categoryId
                    });

                flp_billlayout.SuspendLayout();

                foreach (var tabledata in gridSource)
                {
                    int tid = Convert.ToInt32(tabledata.Tid);
                    if (tid <= 0)
                        continue;

                    if (!tableCache.TryGetValue(tid, out TableControls controls) ||
                        controls == null || controls.Card == null)
                        continue;

                    bool isBooking = tabledata.BookmasterId > 0 ||
                                     Convert.ToInt32(tabledata.Cbid) > 0;
                    string description = BuildCombinedBillDescription(
                        Convert.ToInt32(isBooking
                            ? DashboardTableType.Reverse_overtime_Cancel
                            : DashboardTableType.Normalorder),
                        tabledata.OrderMasterId,
                        tabledata.Tid,
                        tabledata.TableCode,
                        Convert.ToInt32(tabledata.BookmasterId),
                        Convert.ToInt32(tabledata.pkid));

                    string amount = "₹" + Convert.ToString(tabledata.NetAmount);
                    tableAmountCache[tid] = amount;

                    controls.Card.SetBillData(
                        tabledata.TableCode,
                        amount,
                        description,
                        Convert.ToInt32(isBooking
                            ? CombinedBillType.BookingBill
                            : CombinedBillType.Normal),
                        isBooking,
                        selectedTableIds.Contains(tid));

                    controls.Card.Visible = tabledata.BillStatus == 0;
                }

                flp_billlayout.ResumeLayout(true);
                SetEmptyIcon();
            }
            catch (Exception ex)
            {
                flp_billlayout.ResumeLayout(true);
                clsgen.LogException("BillCombineBill_Update()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.CombinedBillpage));
            }
        }

        /// <summary>
        /// Timer callback wired by FrmCombinedBill.Designer.cs.
        /// Refreshes the currently displayed combined-bill cards.
        /// </summary>
        private void CombineBillAutoTimer_Tick(object sender, EventArgs e)
        {
            BillCombineBill_Update();
        }

#region old code bill print
        //public void Billcomplete_Fill()
        //{
        //    try
        //    {
        //        int rowIndex_cate = 0;
        //        this.Visible = false;
        //        this.SuspendLayout();
        //        ListFillFlag = false;
        //        this.Controls.Add(pnl_complebill_prevbill);
        //        pnl_complebill_prevbill.Dock = DockStyle.Fill;
        //        Control controlToRemove = tlp_control.GetControlFromPosition(0, 1);
        //        tlp_control.Controls.Remove(controlToRemove);
        //        tlp_control.Controls.Add(pnl_compelte_bill, 0, 1);
        //        tlp_control.Dock = DockStyle.Fill;

        //        pnl_empty_msg.Visible = false;
        //        pnl_complebill_prevbill.Visible = true;

        //        #region old code fetch db to query No use currently use store procedure
        //        //BllSearch<DmnTableinfo> bllSearch = new BllSearch<DmnTableinfo>();
        //        //DmnSearch dmnSearch = new DmnSearch();
        //        //dmnSearch.Select = "ti.* ,c.CategoryName as CategoryName,om.NetAmount ,(select CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1 from tblOrderDetails WHERE status!=0 and AddonStatus in(1,0) and OrderMasterId =ti.OrderMasterId ) BillStatus,cob.Cbid," +
        //        //    "(select top (1) bd.BookmasterId from tblbookMaster bm  LEFT JOIN tblBookDetails bd on bm.BookmasterId = bd.BookmasterId where bm.Status = 1 and bd.Status = 1 and SYSDATETIME() >= DATEADD(MINUTE," + -Convert.ToDecimal(FrmLogin.BookingInterwell) + ", BookingDateTime) and MTID = ti.tid and rid=0  order by bm.BookingDateTime desc) BookmasterId";
        //        //dmnSearch.Table = "tblTable_info  ti " +
        //        //    "LEFT JOIN tblCategory_FTI c on c.Fcid=ti.Fcid " +
        //        //    "LEFT JOIN tblOrderMaster om on om.OrderMasterId=ti.OrderMasterId and om.Status !=0" +
        //        //    " LEFT JOIN tblCombinedOrderBill cob on cob.OrderMasterid=ti.OrderMasterId";
        //        //dmnSearch.WhereNdGroupBy = "where ti.Status=1";
        //        //dmnSearch.OrderBy = "order by ti.tid";
        //        //var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
        //        #endregion

        //        BllTableData<DmncombinedBill> SearchDetails = new BllTableData<DmncombinedBill>();
        //        DmncombinedBill dmnSearchtabledata = new DmncombinedBill();
        //        dmnSearchtabledata.BookingInterwell = Convert.ToDecimal(FrmLogin.BookingInterwell);
        //        dmnSearchtabledata.Fcid = categoryId;
        //        var gridSource = SearchDetails.serachreport("prdBillData", dmnSearchtabledata);
        //        //var sortedGridSource = gridSource.OrderBy(item => item.t).ToList();
        //        pnl_compelte_bill.Controls.Clear();
        //        flp_billlayout.Controls.Clear();
        //        flp_billlayout.Dock = DockStyle.Fill;
        //        flp_billlayout.AutoScroll = true;
        //        if (gridSource.Count > 0)
        //        {
        //            foreach (var tabledata in gridSource)
        //            {
        //                if (Convert.ToInt32(tabledata.Tid) > 0)
        //                {
        //                    // Configure the TableLayoutPanel for the table
        //                    TableLayoutPanel tlp_table_inner = new TableLayoutPanel();
        //                    tlp_table_inner.RowCount = 3;
        //                    tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
        //                    tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
        //                    tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
        //                    tlp_table_inner.Name = "tlp_com" + tabledata.Tid;
        //                    tlp_table_inner.Tag = Convert.ToInt32(CombinedBillType.Normal);
        //                    tlp_table_inner.BorderStyle = BorderStyle.None;
        //                    tlp_table_inner.Dock = DockStyle.None;
        //                    tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Normalorder) + "," +
        //                                                            Convert.ToString(tabledata.OrderMasterId) + "," +
        //                                                            tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," +
        //                                                            Convert.ToInt32(tabledata.BookmasterId) + "," +
        //                                                            Convert.ToInt32(tabledata.pkid);
        //                    tlp_table_inner.BackColor = Color.White;

        //                    //if (screenWidth > 1366)
        //                    //{
        //                    //    tlp_table_inner.Width = 120;
        //                    //    tlp_table_inner.Height = 80;
        //                    //    tlp_table_inner.Margin = new Padding(5, 5, 0, 5);
        //                    //}
        //                    //else if (screenWidth >= 1024 && screenWidth <= 1366)
        //                    //{
        //                    //    tlp_table_inner.Width = 120;
        //                    //    tlp_table_inner.Height = 80;
        //                    //    tlp_table_inner.Margin = new Padding(10, 10, 0, 10);
        //                    //}

        //                    Button btn_select = new Button();
        //                    btn_select.Name = "btn_select_com" + tabledata.Tid;
        //                    btn_select.AccessibleDescription = tlp_table_inner.AccessibleDescription;
        //                    btn_select.Tag = Convert.ToInt32(BillSelect.Unselect);
        //                    btn_select.FlatStyle = FlatStyle.Flat;
        //                    btn_select.FlatAppearance.BorderSize = 0;
        //                    btn_select.BackgroundImage = Properties.Resources.Billaddicon;
        //                    btn_select.BackgroundImageLayout = ImageLayout.Stretch;
        //                    btn_select.Dock = DockStyle.Fill;
        //                    btn_select.Margin = new Padding(20, 0, 20, 0);
        //                    btn_select.Click += new EventHandler(btn_select_Clicked);

        //                    Label lbltablename = new Label();
        //                    lbltablename.Name = "lb_tablename" + tabledata.Tid;
        //                    lbltablename.AccessibleDescription = tlp_table_inner.AccessibleDescription;
        //                    lbltablename.TextAlign = ContentAlignment.MiddleCenter;
        //                    lbltablename.Tag = Convert.ToInt32(CombinedBillType.Normal);
        //                    lbltablename.Text = tabledata.TableCode;
        //                    lbltablename.ForeColor = Description_color;
        //                    lbltablename.BackColor = Color.Transparent;
        //                    lbltablename.Font = new Font("Arial", 10, FontStyle.Bold);
        //                    lbltablename.Dock = DockStyle.Fill;

        //                    Label netamountlabel = new Label();
        //                    netamountlabel.Name = "lb_netamount" + tabledata.Tid;
        //                    netamountlabel.AccessibleDescription = tlp_table_inner.AccessibleDescription;
        //                    netamountlabel.TextAlign = ContentAlignment.MiddleCenter;
        //                    netamountlabel.Tag = Convert.ToInt32(CombinedBillType.Normal);
        //                    netamountlabel.Text = "\u20B9" + Convert.ToString(tabledata.NetAmount);
        //                    netamountlabel.ForeColor = Netamountcolor;
        //                    netamountlabel.BackColor = Color.Transparent;
        //                    netamountlabel.Font = new Font("Arial", 10, FontStyle.Bold);
        //                    netamountlabel.Dock = DockStyle.Fill;

        //                    tlp_table_inner.Visible = Convert.ToInt32(tabledata.BillStatus) == 0;

        //                    if (tabledata.BookmasterId > 0)
        //                    {
        //                        btn_select.Visible = false;
        //                    }
        //                    else
        //                    {
        //                        if (Convert.ToInt32(tabledata.Cbid) > 0)
        //                        {
        //                            tlp_table_inner.BackColor = CombinedBillcolor;
        //                            btn_select.Visible = false;
        //                        }
        //                        else
        //                        {
        //                            tlp_table_inner.BackColor = NormalBillingColor;
        //                            btn_select.Visible = true;
        //                        }
        //                    }

        //                    // Size settings
        //                    if (screenWidth > 1366) { tlp_table_inner.Width = 300; tlp_table_inner.Height = 150; }
        //                    else if (screenWidth >= 1024) { tlp_table_inner.Width = 120; tlp_table_inner.Height = 100; }

        //                    tlp_table_inner.Controls.Add(btn_select, 0, 0);
        //                    tlp_table_inner.Controls.Add(lbltablename, 0, 1);
        //                    tlp_table_inner.Controls.Add(netamountlabel, 0, 2);

        //                    lbltablename.Click += new EventHandler(CompleteEventHandler);
        //                    netamountlabel.Click += new EventHandler(CompleteEventHandler);
        //                    tlp_table_inner.Click += new EventHandler(CompleteEventHandler);

        //                    if (Convert.ToInt32(tabledata.BookmasterId) > 0)
        //                    {
        //                        tlp_table_inner.Tag = Convert.ToInt32(CombinedBillType.BookingBill);
        //                        lbltablename.Tag = Convert.ToInt32(CombinedBillType.BookingBill);
        //                        netamountlabel.Tag = Convert.ToInt32(CombinedBillType.BookingBill);

        //                        lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," +
        //                                                              Convert.ToString(tabledata.OrderMasterId) + "," +
        //                                                              tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," +
        //                                                              Convert.ToInt32(tabledata.BookmasterId) + "," +
        //                                                              Convert.ToInt32(tabledata.pkid);
        //                        tlp_table_inner.AccessibleDescription = lbltablename.AccessibleDescription;
        //                        netamountlabel.AccessibleDescription = lbltablename.AccessibleDescription;
        //                    }
        //                    UIHelper.RoundCorners(tlp_table_inner, 25, null);
        //                    flp_billlayout.Controls.Add(tlp_table_inner);
        //                }
        //            }

        //            pnl_compelte_bill.Controls.Add(flp_billlayout);
        //            pnl_compelte_bill.Dock = DockStyle.Fill;
        //            pnl_compelte_bill.Visible = true;
        //            CombineBillAutoTimer.Start();
        //        }

        //        SetEmptyIcon();

        //        this.ResumeLayout();
        //        this.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsgen.LogException("CombinedBill Page Billcomplete_Fill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
        //    }
        //}

        //public void BillCombineBill_Update()
        //{
        //    try
        //    {
        //        CombineBillAutoTimer.Stop();
        //        BllTableData<DmncombinedBill> SearchDetails = new BllTableData<DmncombinedBill>();
        //        DmncombinedBill dmnSearchtabledata = new DmncombinedBill();
        //        dmnSearchtabledata.BookingInterwell = Convert.ToDecimal(FrmLogin.BookingInterwell);
        //        dmnSearchtabledata.Fcid = categoryId;
        //        var gridSource = SearchDetails.serachreport("prdBillData", dmnSearchtabledata);
        //        if (gridSource.Count > 0)
        //        {
        //            #region Check order status and set visible flase and true
        //            int rowIndex_cate = 0;

        //            flp_billlayout.SuspendLayout();
        //            foreach (Control ctrl in flp_billlayout.Controls)
        //            {
        //                if (ctrl is TableLayoutPanel && ctrl.Name.StartsWith("tlp_com"))
        //                {
        //                    ctrl.Visible = false;
        //                }
        //            }
        //            flp_billlayout.ResumeLayout();


        //            foreach (var tabledata in gridSource)
        //            {
        //                if (Convert.ToInt32(tabledata.Tid) > 0)
        //                {
        //                    #region Normal , Combined ,bokking orders
        //                    string tlpname = "tlp_com" + tabledata.Tid;
        //                    TableLayoutPanel tlp_box = flp_billlayout.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
        //                    if (tlp_box != null)
        //                    {
        //                        if (tabledata.BillStatus == 0)
        //                        {
        //                            tlp_box.Visible = true;
        //                            string lblname = "lb_netamount" + tabledata.Tid;
        //                            string btniaddicon = "btn_select_com" + tabledata.Tid;
        //                            Button btn_select = flp_billlayout.Controls.Find(btniaddicon, true).FirstOrDefault() as Button;
        //                            Label lblnet = flp_billlayout.Controls.Find(lblname, true).FirstOrDefault() as Label;
        //                            if (lblnet != null)
        //                                lblnet.Text = "\u20B9" + Convert.ToString(tabledata.NetAmount);

        //                            if (tabledata.BookmasterId > 0)//&& tabledata.TableCount > 1 //Bokking order hide plus button
        //                            {
        //                                btn_select.Visible = false;
        //                            }//combined or normal order bill
        //                            else
        //                            {
        //                                if (Convert.ToInt32(tabledata.Cbid) > 0)
        //                                {
        //                                    if (btn_select != null)
        //                                    {
        //                                        btn_select.Visible = false;
        //                                        tlp_box.BackColor = CombinedBillcolor;
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    tlp_box.BackColor = NormalBillingColor;
        //                                    if (CheckedtableNames.Count == 0)
        //                                        if (btn_select != null)
        //                                        {
        //                                            btn_select.Visible = true;
        //                                            btn_select.BackgroundImage = Properties.Resources.Billaddicon;
        //                                            btn_select.Tag = Convert.ToInt32(BillSelect.Unselect);
        //                                            btn_select.BackgroundImageLayout = ImageLayout.Stretch;
        //                                        }
        //                                    //DmnNameList itemToRemove = CheckedtableNames.FirstOrDefault(item => item.Ordermasterid == omasterid);
        //                                }
        //                            }
        //                            tlp_box.Visible = true;
        //                        }
        //                        else
        //                            tlp_box.Visible = false;
        //                    }
        //                    #endregion
        //                }
        //            }
        //            #endregion
        //        }
        //        SetEmptyIcon();
        //        CombineBillAutoTimer.Start();
        //    }
        //    catch (Exception ex)
        //    {
        //        clsgen.LogException("CombinedBill Page BillCombineBill_Update()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
        //    }
        //}

        #endregion

        public void SetEmptyIcon()
        {
            try
            {
                bool anyVisible = flp_billlayout.Controls.OfType<TableLayoutPanel>().Any(tlp => tlp.Name.StartsWith("tlp_com") && tlp.Visible);
                if (!anyVisible)
                {
                    pnl_empty_msg.Anchor = AnchorStyles.None;

                    int x = (this.Width - pnl_empty_msg.Width) / 2;
                    int y = (this.Height - pnl_empty_msg.Height) / 2;
                    pnl_empty_msg.Location = new Point(x, y);

                    pnl_empty_msg.Visible = true;
                    pnl_empty_msg.BringToFront();

                    //btn_prevbill.PerformClick();
                }
                else
                {
                    pnl_empty_msg.Visible = false;
                }
            }
            catch(Exception ex)
            {
                clsgen.LogException("CombinedBill Page SetEmptyIcon()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
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

        public void CheckCombinedOrNot(int Ordermasterid)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "cob.OrderMasterid Field1,cob.tid Field2,ti.TableCode Field3,cob.Cbid Field4";
                dmnSearch.Table = "tblCombinedOrderBill cob" +
                                   " left join tblTable_info ti on ti.OrderMasterId = cob.OrderMasterid";
                dmnSearch.WhereNdGroupBy = "where cob.SalesMasterId = (select SalesMasterId from tblCombinedOrderBill where OrderMasterid=" + Ordermasterid + " )";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    ListFillFlag = true;
                    CheckedtableNames.Clear();
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        CheckedtableNames.Add(new DmnNameList
                        {
                            Id = Convert.ToInt32(gridSource[i].Field2),
                            Ordermasterid = Convert.ToInt32(gridSource[i].Field1),
                            Tablecode = Convert.ToString(gridSource[i].Field3),
                            Cbid = Convert.ToInt32(gridSource[i].Field4)
                        });

                        string btnname = "btn_select_com" + Convert.ToInt32(gridSource[i].Field2);
                        Button btn_select = flp_billlayout.Controls.Find(btnname, true).FirstOrDefault() as Button;
                        if (btn_select != null)
                        {
                            btn_select.BackgroundImage = Properties.Resources.billselectedicon;
                            btn_select.BackgroundImageLayout = ImageLayout.Stretch;
                            // btn_select.Enabled = false;
                        }
                    }
                    lbl_tbl_totalcount.Text = Convert.ToString(CheckedtableNames.Count);
                }
                else
                {
                    ListFillFlag = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("CombinedBill Page CheckCombinedOrNot()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }

        }
        public void CheckCombinedOrNot_Tid(int tid)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "cob.OrderMasterid Field1,cob.tid Field2,ti.TableCode Field3,cob.Cbid Field4";
                dmnSearch.Table = " tblCombinedOrderBill cob" +
                                    " left join tblTable_info ti on ti.OrderMasterId = cob.OrderMasterid" +
                                    " where SalesMasterId in(Select top(1)co.SalesMasterId from tblCombinedOrderBill co" +
                                    " left join tblOrderMaster om on om.OrderMasterId = co.OrderMasterid";
                dmnSearch.WhereNdGroupBy = " where co.Tid=" + tid + " and om.Status=1  ";
                dmnSearch.OrderBy = " order by cbid desc )";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    CheckedtableNames.Clear();
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        CheckedtableNames.Add(new DmnNameList
                        {
                            Id = Convert.ToInt32(gridSource[i].Field2),
                            Ordermasterid = Convert.ToInt32(gridSource[i].Field1),
                            Tablecode = Convert.ToString(gridSource[i].Field3),
                            Cbid = Convert.ToInt32(gridSource[i].Field4)
                        });

                        //string btnname = "btn_select_com" + Convert.ToInt32(gridSource[i].Field2);
                        //Button btn_select = tlp_table_dashboard.Controls.Find(btnname, true).FirstOrDefault() as Button;
                        //if (btn_select != null)
                        //{
                        //    btn_select.BackgroundImage = Properties.Resources.billselectedicon;
                        //    btn_select.BackgroundImageLayout = ImageLayout.Stretch;
                        //    // btn_select.Enabled = false;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page CheckCombinedOrNot_Tid()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }

        }
        private void btn_select_Clicked(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                //Convert.ToInt32(DashboardTableType.Reverse_Table)+","+Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) +
                //"," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                string textvalue = button.AccessibleDescription;
                string[] parts = textvalue.Split(',');

                int Btype = parts.Length > 0 ? Convert.ToInt32(parts[0]) : 0;
                int OrderMasterId = parts.Length > 1 ? Convert.ToInt32(parts[1]) : 0;
                int tid = parts.Length > 2 ? Convert.ToInt32(parts[2]) : 0;
                string tableCode = parts.Length > 3 ? parts[3] : "";
                int BmasterId = parts.Length > 4 ? Convert.ToInt32(parts[4]) : 0;
                int pkid = parts.Length > 4 ? Convert.ToInt32(parts[5]) : 0;

                if (button != null)
                {
                    CheckCombinedOrNot(OrderMasterId);

                    //cmb_tbl_names.Items.Add("Table List");

                    if (!ListFillFlag)
                    {
                        if (Convert.ToInt32(button.Tag) == Convert.ToInt32(BillSelect.Unselect))
                        {
                            CheckedtableNames.Add(new DmnNameList
                            {
                                Id = tid,
                                Ordermasterid = OrderMasterId,
                                Tablecode = tableCode,
                                BookingMasterid = BmasterId,
                                pkid = pkid,
                            });
                            button.BackgroundImage = Properties.Resources.billselectedicon;
                            button.Tag = Convert.ToInt32(BillSelect.Selected);
                            if (pkid > 0)
                            {
                                // Add to the combo box
                                string comboBoxItem = $"{pkid}";
                                cmb_tbl_names.Items.Add(comboBoxItem);
                            }
                            else
                            {
                                // Add to the combo box
                                string comboBoxItem = $"{tableCode}";
                                cmb_tbl_names.Items.Add(comboBoxItem);
                            }


                        }
                        else if (Convert.ToInt32(button.Tag) == Convert.ToInt32(BillSelect.Selected))
                        {
                            DmnNameList itemToRemove = CheckedtableNames.FirstOrDefault(item => item.Ordermasterid == OrderMasterId);
                            if (itemToRemove != null)
                            {
                                CheckedtableNames.Remove(itemToRemove);
                                if (pkid > 0)
                                {
                                    // Remove from the combo box
                                    string comboBoxItemToRemove = $"{pkid}";
                                    cmb_tbl_names.Items.Remove(comboBoxItemToRemove);
                                }
                                else
                                {
                                    // Remove from the combo box
                                    string comboBoxItemToRemove = $"{itemToRemove.Tablecode}";
                                    cmb_tbl_names.Items.Remove(comboBoxItemToRemove);
                                }
                            }

                            button.BackgroundImage = Properties.Resources.Billaddicon;
                            button.Tag = Convert.ToInt32(BillSelect.Unselect);
                        }

                        button.BackgroundImageLayout = ImageLayout.Stretch;

                        if (pkid > 0)
                            lbl_tbl_totalcount.Text = "Pickup Orders : " + Convert.ToString(CheckedtableNames.Count);
                        else
                            lbl_tbl_totalcount.Text = "Total Tables : " + Convert.ToString(CheckedtableNames.Count);

                        if (CheckedtableNames.Count > 1)
                        {
                            lbl_tbl_totalcount.Visible = true;
                            cmb_tbl_names.Visible = true;
                            cmb_tbl_names.SelectedIndex = 0;
                        }
                        else
                        {
                            lbl_tbl_totalcount.Visible = false;
                            cmb_tbl_names.Visible = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page btn_select_Clicked", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        public class CategoryInfo
        {
            public int Fcid { get; set; }
            public string CategoryName { get; set; }
        }
        private void CombinedBillCard_SelectRequested(object sender, EventArgs e)
        {
            ModernCombinedBillCard card = sender as ModernCombinedBillCard;
            if (card == null || string.IsNullOrEmpty(card.AccessibleDescription))
                return;

            try
            {
                isUserSelecting = true;

                string[] parts = card.AccessibleDescription.Split(',');
                int orderMasterId = parts.Length > 1 ? Convert.ToInt32(parts[1]) : 0;
                int tid = parts.Length > 2 ? Convert.ToInt32(parts[2]) : 0;
                string tableCode = parts.Length > 3 ? parts[3] : string.Empty;
                int bookingMasterId = parts.Length > 4 ? Convert.ToInt32(parts[4]) : 0;
                int pkid = parts.Length > 5 ? Convert.ToInt32(parts[5]) : 0;

                if (tid <= 0 ||
                    Convert.ToInt32(card.Tag) == Convert.ToInt32(CombinedBillType.BookingBill))
                    return;

                CheckCombinedOrNot(orderMasterId);

                if (selectedTableIds.Contains(tid))
                {
                    selectedTableIds.Remove(tid);
                    var existing = CheckedtableNames.FirstOrDefault(x => x.Ordermasterid == orderMasterId);
                    if (existing != null)
                        CheckedtableNames.Remove(existing);
                    cmb_tbl_names.Items.Remove(pkid > 0 ? pkid.ToString() : tableCode);
                }
                else
                {
                    selectedTableIds.Add(tid);
                    CheckedtableNames.Add(new DmnNameList
                    {
                        Id = tid,
                        Ordermasterid = orderMasterId,
                        Tablecode = tableCode,
                        BookingMasterid = bookingMasterId,
                        pkid = pkid
                    });

                    string value = pkid > 0 ? pkid.ToString() : tableCode;
                    if (!cmb_tbl_names.Items.Contains(value))
                        cmb_tbl_names.Items.Add(value);
                }

                string amount = tableAmountCache.TryGetValue(tid, out string currentAmount)
                    ? currentAmount : string.Empty;
                card.SetBillData(tableCode, amount, card.AccessibleDescription,
                    Convert.ToInt32(card.Tag), false, selectedTableIds.Contains(tid));
                UpdateSelectedCountUI(pkid);
            }
            catch (Exception ex)
            {
                clsgen.LogException("CombinedBillCard_SelectRequested()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.CombinedBillpage));
            }
            finally
            {
                isUserSelecting = false;
            }
        }

        private void UpdateSelectedCountUI(int pkid)
        {
            int count = CheckedtableNames.Count;
            if (count > 1)
            {
                lbl_tbl_totalcount.Visible = true;
                cmb_tbl_names.Visible = true;
                if (cmb_tbl_names.Items.Count > 0 && cmb_tbl_names.SelectedIndex < 0)
                    cmb_tbl_names.SelectedIndex = 0;
                lbl_tbl_totalcount.Text = pkid > 0
                    ? "Pickup Orders : " + count
                    : "Total Tables : " + count;
            }
            else
            {
                lbl_tbl_totalcount.Visible = false;
                cmb_tbl_names.Visible = false;
            }
        }

        private void CompleteEventHandler(object sender, EventArgs e)
        {
            try
            {
                CombineBillAutoTimer.Stop();

                Control clickedControl = sender as Control;
                if (clickedControl == null ||
                    string.IsNullOrWhiteSpace(clickedControl.AccessibleDescription))
                    return;

                string[] parts = clickedControl.AccessibleDescription.Split(',');
                int billType = Convert.ToInt32(clickedControl.Tag);
                int orderMasterId = parts.Length > 1 ? Convert.ToInt32(parts[1]) : 0;
                int tid = parts.Length > 2 ? Convert.ToInt32(parts[2]) : 0;
                int bookingMasterId = parts.Length > 4 ? Convert.ToInt32(parts[4]) : 0;

                CheckCombinedOrNot_Tid(tid);

                if (CheckedtableNames.Count > 1)
                {
                    if (Application.OpenForms["FrmOrderConsole"] == null)
                    {
                        using (FrmOrderConsole orderConsole = new FrmOrderConsole(
                            0,
                            Convert.ToInt32(DashboardTableType.CombinedOrderBill),
                            0,
                            CheckedtableNames,
                            Convert.ToInt32(PageType.Billpage),
                            0,
                            Convert.ToInt32(SourcepageType.Billingpage),
                            0))
                        {
                            orderConsole.WindowState = FormWindowState.Maximized;
                            orderConsole.Opacity = 0;
                            orderConsole.Visible = false;
                            orderConsole.ShowDialog();
                            orderConsole.BringToFront();
                        }
                    }

                    ResetCombinedBillSelection();
                    return;
                }

                if (billType == Convert.ToInt32(CombinedBillType.Normal))
                {
                    if (Application.OpenForms["FrmOrderConsole"] == null)
                    {
                        using (FrmOrderConsole orderConsole = new FrmOrderConsole(
                            tid,
                            Convert.ToInt32(DashboardTableType.Normalorder),
                            0,
                            CheckedtableNames,
                            Convert.ToInt32(PageType.Billpage),
                            0,
                            Convert.ToInt32(SourcepageType.Billingpage),
                            0))
                        {
                            orderConsole.WindowState = FormWindowState.Maximized;
                            orderConsole.Opacity = 0;
                            orderConsole.Visible = false;
                            orderConsole.ShowDialog();
                        }
                    }
                    else
                    {
                        FrmOrderConsole orderConsole =
                            Application.OpenForms["FrmOrderConsole"] as FrmOrderConsole;
                        if (orderConsole != null)
                        {
                            if (orderConsole.WindowState == FormWindowState.Minimized)
                                orderConsole.WindowState = FormWindowState.Maximized;
                            else
                                orderConsole.BringToFront();
                        }
                    }
                }
                else if (billType == Convert.ToInt32(CombinedBillType.BookingBill))
                {
                    if (Application.OpenForms["FrmOrderConsole"] == null)
                    {
                        using (FrmOrderConsole orderConsole = new FrmOrderConsole(
                            tid,
                            Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel),
                            bookingMasterId,
                            CheckedtableNames,
                            Convert.ToInt32(PageType.Billpage),
                            0,
                            Convert.ToInt32(SourcepageType.Billingpage),
                            0))
                        {
                            orderConsole.WindowState = FormWindowState.Maximized;
                            orderConsole.Opacity = 0;
                            orderConsole.Visible = false;
                            orderConsole.Show();
                            orderConsole.BringToFront();
                        }
                    }
                    else
                    {
                        FrmOrderConsole orderConsole =
                            Application.OpenForms["FrmOrderConsole"] as FrmOrderConsole;
                        if (orderConsole != null)
                        {
                            if (orderConsole.WindowState == FormWindowState.Minimized)
                                orderConsole.WindowState = FormWindowState.Maximized;
                            else
                                orderConsole.BringToFront();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("CombinedBill Page CompleteEventHandler()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.CombinedBillpage));
            }
            finally
            {
                if (!IsDisposed && !Disposing)
                    CombineBillAutoTimer.Start();
            }
        }

        private void ResetCombinedBillSelection()
        {
            CombineBillAutoTimer.Stop();
            CheckedtableNames.Clear();
            selectedTableIds.Clear();

            foreach (var entry in tableCache)
            {
                ModernCombinedBillCard card = entry.Value?.Card;
                if (card == null)
                    continue;

                string[] parts = (card.AccessibleDescription ?? string.Empty).Split(',');
                string tableCode = parts.Length > 3 ? parts[3] : string.Empty;
                bool isBooking = Convert.ToInt32(card.Tag) == Convert.ToInt32(CombinedBillType.BookingBill);

                card.SetBillData(
                    tableCode,
                    ReadCardAmount(card),
                    card.AccessibleDescription,
                    Convert.ToInt32(card.Tag),
                    isBooking,
                    false);
            }

            lbl_tbl_totalcount.Visible = false;
            cmb_tbl_names.Visible = false;
            cmb_tbl_names.Items.Clear();

            BillCombineBill_Update();
            CombineBillAutoTimer.Start();
        }


        private void btn_backCompleorder_Click(object sender, EventArgs e)
        {
            try
            {
                tlp_tblcount_namelist.Visible = true;
                btn_backCompleorder.Visible = false;
                btn_prevbill.Visible = true;

                pnl_previous_bill.Visible = false;
                flp_billlayout.Visible = true;
                flp_billlayout.BringToFront();

                pnl_empty_msg.Visible = false;
                pnl_complebill_prevbill.Visible = true;

                CombineBillAutoTimer.Start();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page btn_backCompleorder_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }


        }
        private void btn_serach_prev_bill_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();

                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "ID,DateTime,InvoiceNo,TableCode,PickUpNo,NetAmount";//,
                _objSearch.Select =
                    "sm.SalesMasterId as Id, " +
                    "sm.CreatedDateTime as Field1, " +
                    "sm.EntryNo as Field2, " +
                    "ti.TableCode as Field3, " +
                    "sm.PkId as Field4, " +
                    "sm.NetAmount as Field5";/*"CASE " +
                    " WHEN COALESCE(sm.Partybookingid,0) > 0 THEN 'Party Booking' " +
                    " WHEN COALESCE(sm.PkId,0) > 0 THEN 'PKID ' + CAST(sm.PkId AS VARCHAR(20)) " +
                    " WHEN COALESCE(sm.Tid,0) > 0 THEN COALESCE(ti.TableCode,'') " +
                    " ELSE 'Multiple Tables' END as Field3, " +*/
                _objSearch.Table =
                    "tblSalesMaster sm " +
                    "LEFT JOIN tblTable_info ti ON ti.tid = sm.Tid " +
                    "LEFT JOIN tblGeneralOptions g ON g.GTypeSubId = sm.PaymentMode";
                _objSearch.WhereNdGroupBy = "WHERE sm.Status = 1 AND g.GTypeId = 2";
                _objSearch.OrderBy = "ORDER BY sm.SalesMasterId DESC";

                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource == null || gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records", "Empty",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var search = new FrmSearch<DmnGridFields>(
                    _objSearch,
                    SearchMode.FirstCharacter,
                    this.Width - 150,
                    this.Height - 150,
                    this.Location.Y + 50,
                    this.Location.X + 20,
                    dtColumnAlign))
                {
                    if (search.ShowDialog() == DialogResult.OK)
                        returnValue = Convert.ToString(search._retValue);
                }

                if (!string.IsNullOrWhiteSpace(returnValue))
                {
                    int saleMasterId;
                    if (int.TryParse(returnValue, out saleMasterId) && saleMasterId > 0)
                    {
                        PrintBillForSaleMaster(saleMasterId);
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "CombinedBill Page btn_serach_prev_bill_Click()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.CombinedBillpage));
            }
        }

        private void btn_prevbill_Click(object sender, EventArgs e)
        {
            try
            {
                tlp_tblcount_namelist.Visible = false;
                CombineBillAutoTimer.Stop();

                this.Controls.Add(pnl_complebill_prevbill);
                pnl_complebill_prevbill.Dock = DockStyle.Fill;
                pnl_complebill_prevbill.BringToFront();

                pnl_previous_bill.Visible = true;
                flp_billlayout.Visible = false;
                pnl_previous_bill.BringToFront();

                pnl_previous_bill.Dock = DockStyle.Fill;
                pnl_previous_bill.Visible = true;
                pnl_empty_msg.Visible = false;

                btn_backCompleorder.Visible = true;
                btn_prevbill.Visible = false;

                #region Fill complete or cancelled order in Datagrid
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "Top 30 sm.CreatedDateTime Field1,sm.EntryNo Field2,ti.TableCode Field3,sm.TotalQuantity Field4,sm.Status Field5," +
                    "sm.SalesMasterId Field6 ,sm.NetAmount Field7,sm.RcvAmnt Field8,sm.BalAmnt Field9,sm.PaymentMode Field10,sm.Tid Field11," +
                    "sm.CombinedOrderBill_TYPE Field12,sm.OrderMasterid Field13,sm.OrderType Field14,sm.Partybookingid Field15,sm.tid Field16,sm.Pkid Field17";
                dmnSearch.Table = "tblSalesMaster sm LEFT JOIN tblTable_info ti on ti.tid=sm.Tid";
                dmnSearch.WhereNdGroupBy = "where sm.status=1";
                dmnSearch.OrderBy = "order by sm.SalesMasterId DESC";
                dgvorderview.Rows.Clear();
                int rowIndex = 0;
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    int imageSize = 32;
                    dgvorderview.Rows.Add(gridSource.Count);
                    int x = dgvorderview.RowCount;
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        dgvorderview.Rows[rowIndex].Cells[cslno].Value = rowIndex + 1;
                        dgvorderview.Rows[rowIndex].Cells[orderdatetimeno].Value = gridSource[i].Field1;
                        dgvorderview.Rows[rowIndex].Cells[entryno].Value = gridSource[i].Field2;

                        int partyBookingId = Convert.ToInt32(gridSource[i].Field15);
                        int tableId = Convert.ToInt32(gridSource[i].Field16);
                        int pickupId = Convert.ToInt32(gridSource[i].Field17);
                        int orderType = Convert.ToInt32(gridSource[i].Field14);

                        if (partyBookingId > 0)
                        {
                            SetTableCodeDisplay(rowIndex, "Party Booking", 0, true);
                        }
                        else if (pickupId > 0 || orderType == Convert.ToInt32(OrderType.PickupOrder))
                        {
                            SetTableCodeDisplay(rowIndex, "PKID " + pickupId, 0, true);
                        }
                        else if (tableId <= 0)
                        {
                            FillTablecode_Comb(
                                rowIndex,
                                Convert.ToInt32(gridSource[i].Field6),
                                Convert.ToInt32(gridSource[i].Field13),
                                Convert.ToInt32(gridSource[i].Field12));
                        }
                        else
                        {
                            SetTableCodeDisplay(rowIndex, Convert.ToString(gridSource[i].Field3), tableId, true);
                        }


                        dgvorderview.Rows[rowIndex].Cells[totalqtyno].Value = gridSource[i].Field4;
                        dgvorderview.Rows[rowIndex].Cells[Salemasteridno].Value = gridSource[i].Field6;
                        dgvorderview.Rows[rowIndex].Cells[netamountno].Value = gridSource[i].Field7;
                        dgvorderview.Rows[rowIndex].Cells[recamountno].Value = gridSource[i].Field8;
                        dgvorderview.Rows[rowIndex].Cells[balamountno].Value = gridSource[i].Field9;
                        dgvorderview.Rows[rowIndex].Cells[partybookingidno].Value = gridSource[i].Field15;
                        dgvorderview.Rows[rowIndex].Cells[tableidno].Value = gridSource[i].Field16;
                        dgvorderview.Rows[rowIndex].Cells[pickupidno].Value = gridSource[i].Field17;

                        switch (Convert.ToInt32(gridSource[i].Field10))
                        {
                            case 1:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "CASH";
                                break;
                            case 5:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "DEBITCARD";
                                break;
                            case 4:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "UPI";
                                break;
                            case 6:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "BANKINGONLINE";
                                break;
                            case 3:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "CREDIT";
                                break;
                            case 7:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "OTHERS";
                                break;
                            case 11:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "MIDAS CARD";
                                break;
                            case 12:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "SPLIT";
                                break;
                            case 10:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "COMPLEMENTRY";
                                break;
                            case 14:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "NO CHARGE";
                                break;
                        }

                        DataGridViewImageCell cell = (DataGridViewImageCell)dgvorderview.Rows[rowIndex].Cells[statusno];
                        cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        cell.Value = Properties.Resources.orderview_complete;

                        DataGridViewImageCell cell1 = (DataGridViewImageCell)dgvorderview.Rows[rowIndex].Cells[actionno];
                        cell1.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        cell1.Value = Properties.Resources.viewbill_billing;

                        rowIndex++;
                    }
                }
                else
                {
                    SetEmptyIcon();
                }

                #endregion
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page btn_prevbill_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        private void SetTableCodeDisplay(int rowIndex, string displayText, long tableId, bool readOnly)
        {
            DataGridViewComboBoxCell cell =
                (DataGridViewComboBoxCell)dgvorderview.Rows[rowIndex].Cells[tablecodeno];

            List<ComboBoxItem> items = new List<ComboBoxItem>
            {
                new ComboBoxItem(tableId, displayText ?? string.Empty)
            };

            cell.DataSource = items;
            cell.ValueMember = "Id";
            cell.DisplayMember = "Value";
            cell.Value = tableId;
            cell.ReadOnly = readOnly;
            dgvorderview.Rows[rowIndex].Cells[tablecodeno].ReadOnly = readOnly;
        }

        private void PrintBillBySaleType(int saleMasterId, int partyBookingId, int tableId, int pickupId)
        {
            if (saleMasterId <= 0)
                return;

            if (partyBookingId > 0)
                printBill(saleMasterId, Convert.ToInt32(BillType.Partybill));
            else if (pickupId > 0)
                printBill(saleMasterId, Convert.ToInt32(BillType.PickupBill));
            else
                printBill(saleMasterId, Convert.ToInt32(BillType.NormalBill));
        }

        private void PrintBillForSaleMaster(int saleMasterId)
        {
            if (saleMasterId <= 0)
                return;

            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            DmnSearch search = new DmnSearch
            {
                Select = "ISNULL(Partybookingid,0) Field1, ISNULL(Tid,0) Field2, ISNULL(PkId,0) Field3",
                Table = "tblSalesMaster",
                WhereNdGroupBy = "WHERE Status = 1 AND SalesMasterId = " + saleMasterId,
                OrderBy = ""
            };

            List<DmnGridFields> result = bllSearch.GetSearchData("prdSearch", search);
            if (result != null && result.Count > 0)
            {
                PrintBillBySaleType(
                    saleMasterId,
                    Convert.ToInt32(result[0].Field1),
                    Convert.ToInt32(result[0].Field2),
                    Convert.ToInt32(result[0].Field3));
            }
        }

        private void dgvorderview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvorderview.Rows.Count)
                    return;

                if (e.ColumnIndex == actionno)
                {
                    int sid = Convert.ToInt32(dgvorderview.Rows[e.RowIndex].Cells[Salemasteridno].Value);
                    int pbkid = Convert.ToInt32(dgvorderview.Rows[e.RowIndex].Cells[partybookingidno].Value);
                    int tidno = Convert.ToInt32(dgvorderview.Rows[e.RowIndex].Cells[tableidno].Value);
                    int pickupid = Convert.ToInt32(dgvorderview.Rows[e.RowIndex].Cells[pickupidno].Value);

                    PrintBillBySaleType(sid, pbkid, tidno, pickupid);
                }
                else if (e.ColumnIndex == paytypename)
                {
                    int sid = Convert.ToInt32(dgvorderview.Rows[e.RowIndex].Cells[Salemasteridno].Value);
                    DmnSearch obj = new DmnSearch();
                    BllSearch<DmnGridFields> bllpayments = new BllSearch<DmnGridFields>();
                    obj.Select = "PaidAmt Field1,g.Caption Field2,SalesMasterId Field3,Paymode Field4 ";
                    obj.Table = "tblPartPaymentDetails p "+
                        " left join tblGeneralOptions g on g.GTypeSubId=p.Paymode and g.GTypeId=2";
                    obj.WhereNdGroupBy = " where p.status=1 and p.SalesMasterId=" + sid;
                    var paymentresult = bllpayments.GetSearchData("prdSearch", obj);
                    if (paymentresult.Count > 0)
                    {
                        lblsplit_heading.Text = "Split Payment";
                        dgv_split.Rows.Clear();
                        dgv_split.Rows.Add(paymentresult.Count);
                        int x = dgv_split.RowCount;
                        for (int i = 0; i < paymentresult.Count; i++)
                        {
                            dgv_split.Rows[i].Cells[sslno].Value = i + 1;
                            dgv_split.Rows[i].Cells[spaymentamtno].Value = paymentresult[i].Field1;
                            dgv_split.Rows[i].Cells[spaymentmodeno].Value = paymentresult[i].Field2;
                            dgv_split.Rows[i].Cells[ssalemasteridno].Value = paymentresult[i].Field3;
                            dgv_split.Rows[i].Cells[spayidno].Value = paymentresult[i].Field4;
                        }
                    }
                    pnl_split.Location = new Point(this.ClientSize.Width / 2 - pnl_split.Size.Width / 2, this.ClientSize.Height / 2 - pnl_split.Size.Height / 2);
                    pnl_split.Visible = true;
                    pnl_split.BringToFront();

                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("CombinedBill Page dgvorderview_CellDoubleClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        public void printBill(int oid,int btype)
        {
            try
            {
                DmnSearch obj = new DmnSearch();
                BllSearch<DmnGridFields> bllpayments = new BllSearch<DmnGridFields>();
                obj.Select = "PaymentMode Field1,SalesMasterId Field2";
                obj.Table = "tblSalesMaster";
                obj.WhereNdGroupBy = " where status=1 and SalesMasterId=" + oid;
                var result = bllpayments.GetSearchData("prdSearch", obj);
                if (result.Count > 0)
                {
                    //if (Convert.ToInt32(result[0].Field1) != 12)
                    //{
                        pnl_split.Visible = false;
                        using (FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(oid, Convert.ToInt32(result[0].Field1), 0, btype))
                        {
                            frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                            frmSalesInvoice.ShowDialog();
                        }
                    //}
                    //else
                    //{
                    //    obj.Select = "Amount Field1,SplitType Field2,SalemasterId Field3,PayId Field4 ";
                    //    obj.Table = "tblpayment";
                    //    obj.WhereNdGroupBy = " where status=1 and SaleMasterId=" + oid;
                    //    var paymentresult = bllpayments.GetSearchData("prdSearch", obj);
                    //    if (paymentresult.Count > 0)
                    //    {
                    //        dgv_split.Rows.Clear();
                    //        dgv_split.Rows.Add(paymentresult.Count);
                    //        int x = dgv_split.RowCount;
                    //        for (int i = 0; i < paymentresult.Count; i++)
                    //        {
                    //            dgv_split.Rows[i].Cells[0].Value = i + 1;
                    //            dgv_split.Rows[i].Cells[1].Value = paymentresult[i].Field1;
                    //            DataGridViewImageCell cell = (DataGridViewImageCell)dgv_split.Rows[i].Cells[2];
                    //            cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                    //            cell.Value = Properties.Resources.eye;
                    //            dgv_split.Rows[i].Cells[3].Value = paymentresult[i].Field3;
                    //            dgv_split.Rows[i].Cells[4].Value = paymentresult[i].Field4;
                    //        }
                    //    }
                    //    pnl_split.Location = new Point(this.ClientSize.Width / 2 - pnl_split.Size.Width / 2, this.ClientSize.Height / 2 - pnl_split.Size.Height / 2);
                    //    pnl_split.Visible = true;
                    //    pnl_split.BringToFront();
                    //}
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page printBill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        public void FillTablecode_Comb(int rowindex, int salemasterid, int ordermasterid, int ordertype)
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch search = new DmnSearch();

                if (ordertype == 0)
                {
                    search.Select = "bd.MTID id, bd.Name value";
                    search.Table = "tblOrderMaster om " +
                                   "LEFT JOIN tblbookMaster bm ON om.BookmasterId = bm.BookmasterId " +
                                   "LEFT JOIN tblBookDetails bd ON bd.BookmasterId = bm.BookmasterId AND bd.Rid = 0";
                    search.WhereNdGroupBy = "WHERE om.OrderMasterId = " + ordermasterid;
                }
                else
                {
                    search.Select =
                        "com.tid id, " +
                        "CASE WHEN ISNULL(com.PickupOrderFlag,0) = 1 " +
                        "THEN 'PKID ' + CONVERT(VARCHAR(20),com.PkId) " +
                        "ELSE ISNULL(ti.TableCode,'') END value";
                    search.Table = "tblCombinedOrderBill com " +
                                   "LEFT JOIN tblTable_info ti ON ti.tid = com.Tid";
                    search.WhereNdGroupBy = "WHERE com.SalesMasterId = " + salemasterid;
                }

                search.OrderBy = "";
                List<DmnComboBox> source = bllSearch.GetSearchData("prdComboFill", search)
                    ?? new List<DmnComboBox>();

                DataGridViewComboBoxCell cell =
                    (DataGridViewComboBoxCell)dgvorderview.Rows[rowindex].Cells[tablecodeno];

                List<ComboBoxItem> items = new List<ComboBoxItem>();

                if (source.Count == 0)
                {
                    items.Add(new ComboBoxItem(0, "N/A"));
                    cell.ReadOnly = true;
                }
                else if (source.Count == 1)
                {
                    items.Add(new ComboBoxItem(source[0].Id, source[0].Value));
                    cell.ReadOnly = true;
                }
                else
                {
                    items.Add(new ComboBoxItem(0, "Multiple Tables (" + source.Count + ")"));
                    foreach (DmnComboBox item in source)
                        items.Add(new ComboBoxItem(item.Id, item.Value));

                    cell.ReadOnly = false;
                }

                cell.DataSource = items;
                cell.ValueMember = "Id";
                cell.DisplayMember = "Value";
                cell.Value = items[0].Id;
                dgvorderview.Rows[rowindex].Cells[tablecodeno].ReadOnly = cell.ReadOnly;
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "CombinedBill Page FillTablecode_Comb()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        public class ComboBoxItem
        {
            public long Id { get; set; }
            public string Value { get; set; }

            public ComboBoxItem(long id, string value)
            {
                Id = id;
                Value = value;
            }

            public override string ToString()
            {
                return Value;
            }
        }
        private void btn_close_split_Click(object sender, EventArgs e)
        {
            pnl_split.Visible = false;
        }
        private void FrmCombinedBill_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                AppSession.UnregisterForm(this);
            }
            if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
            {
                FrmDashboard.Instance.lbl_drawer.Visible = false;
            }
        }
        private void yout_Click(object sender, EventArgs e)
        {
            try
            {
                int permissionlevel = clsgen.CheckButtonPermission(Convert.ToInt32(FrmLogin.UserAccountTypeId), Convert.ToInt32(RolePermission.YOut));
                if (permissionlevel == Convert.ToInt32(PermissionLevel.NoPermission))
                {

                    frmExtraAccessChk frm = new frmExtraAccessChk(Convert.ToInt32(RolePermission.YOut));
                    frm.ShowDialog();
                    if (frmExtraAccessChk.access == true)
                    {
                        DrawerCheck();
                        using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.Yout), Drawerassginstaffid))
                        {
                            if (FrmDashboard.XYDalvalue)
                                frmzout.ShowDialog();
                            else
                                frmzout.PrintAll();
                        }
                        CMessageBox.Show("Yout Print Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (permissionlevel == Convert.ToInt32(PermissionLevel.FullPermission) || permissionlevel == Convert.ToInt32(PermissionLevel.ReadOnly))
                {
                    DrawerCheck();
                    using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.Yout), Drawerassginstaffid))
                    {
                        if (FrmDashboard.XYDalvalue)
                            frmzout.ShowDialog();
                        else
                            frmzout.PrintAll();
                    }
                    CMessageBox.Show("Yout Print Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("POSDashboard page yout_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        //private void zout_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int permissionlevel = clsgen.CheckButtonPermission(Convert.ToInt32(FrmLogin.UserAccountTypeId), Convert.ToInt32(RolePermission.YOut));
        //        if (permissionlevel == Convert.ToInt32(PermissionLevel.NoPermission))
        //        {
        //            frmExtraAccessChk frm = new frmExtraAccessChk(Convert.ToInt32(RolePermission.YOut));
        //            frm.ShowDialog();
        //            if (frmExtraAccessChk.access == true)
        //            {
        //                #region Zout
        //                DrawerCheck();
        //                if (FrmDrawer.DrawerID > 0)
        //                {
        //                    var confirmResult =CMessageBox.Show(
        //                     "This will print the Z-Out report and sign you out.\n\n" +
        //                     "Make sure all transactions are completed.\n\n" +
        //                     "Do you want to proceed and end your shift?",
        //                     "Confirm Z-Out and Sign Out",
        //                     MessageBoxButtons.YesNo,
        //                     MessageBoxIcon.Question
        //                 );

        //                    if (confirmResult != DialogResult.Yes)
        //                        return;

        //                    using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.Zout), Drawerassginstaffid))
        //                    {
        //                        if (FrmDashboard.XYDalvalue)
        //                            frmzout.ShowDialog();
        //                        else
        //                            frmzout.PrintAll();
        //                    }

        //                    CMessageBox.Show("Zout Print Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //                    Dmndrawer dmndrawer = new Dmndrawer();
        //                    Blldrawer blldrawer = new Blldrawer();

        //                    if (flagDrawerF)
        //                    {
        //                        dmndrawer.Id = Drawerassginid;
        //                    }
        //                    else
        //                    {
        //                        if (FrmDrawer.DrawerID > 0)
        //                            dmndrawer.Id = FrmDrawer.DrawerID;
        //                    }

        //                    dmndrawer.BranchId = FrmDashboard.BranchId;
        //                    dmndrawer.CreatedBy = FrmDashboard.UserId;
        //                    dmndrawer.Status = 0;
        //                    var InsertStatus = blldrawer.AddEditDatadrawer("prdInsertDrawer", dmndrawer);
        //                    if (InsertStatus.Id > 0)
        //                    {
        //                        FrmDrawer.DrawerID = 0;
        //                        tlp_outbtn.Visible = false;
        //                        tlp_previous_bill.Visible = false;
        //                        tlp_control.Visible = false;
        //                        FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
        //                        FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
        //                    }

        //                    #region Close all form Return Login page
        //                    Application.Exit();
        //                    //foreach (Form openForm in Application.OpenForms.Cast<Form>().ToList())
        //                    //{
        //                    //    if (!(openForm is FoodandBeverage.Forms.FrmLogin) && !(openForm is FoodandBeverage.Forms.FrmSplash))
        //                    //        openForm.Close();
        //                    //}

        //                    //System.Threading.Thread.Sleep(500);
        //                    //if (existingLogin == null)
        //                    //{
        //                    //    FoodandBeverage.Forms.FrmLogin loginForm = new FoodandBeverage.Forms.FrmLogin();
        //                    //    loginForm.Show();
        //                    //}
        //                    //else
        //                    //{
        //                    //    existingLogin.Opacity = 100;
        //                    //    existingLogin.BringToFront();
        //                    //    existingLogin.Focus();
        //                    //}
        //                    #endregion

        //                }
        //                else
        //                {
        //                    CMessageBox.Show("Zout Cannot be Completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                }
        //                #endregion
        //            }
        //        }
        //        else if (permissionlevel == Convert.ToInt32(PermissionLevel.FullPermission) || permissionlevel == Convert.ToInt32(PermissionLevel.ReadOnly))
        //        {
        //            #region Zout
        //            DrawerCheck();
        //            if (FrmDrawer.DrawerID > 0)
        //            {
        //                var confirmResult = CMessageBox.Show(
        //                 "This will print the Z-Out report and sign you out.\n\n" +
        //                 "Make sure all transactions are completed.\n\n" +
        //                 "Do you want to proceed and end your shift?",
        //                 "Confirm Z-Out and Sign Out",
        //                 MessageBoxButtons.YesNo,
        //                 MessageBoxIcon.Question
        //             );

        //                if (confirmResult != DialogResult.Yes)
        //                    return;

        //                using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.Zout), Drawerassginstaffid))
        //                {
        //                    if (FrmDashboard.XYDalvalue)
        //                        frmzout.ShowDialog();
        //                    else
        //                        frmzout.PrintAll();
        //                }

        //                CMessageBox.Show("Zout Print Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                //return;

        //                Dmndrawer dmndrawer = new Dmndrawer();
        //                Blldrawer blldrawer = new Blldrawer();

        //                if (flagDrawerF)
        //                {
        //                    dmndrawer.Id = Drawerassginid;
        //                }
        //                else
        //                {
        //                    if (FrmDrawer.DrawerID > 0)
        //                        dmndrawer.Id = FrmDrawer.DrawerID;
        //                }

        //                dmndrawer.BranchId = FrmDashboard.BranchId;
        //                dmndrawer.CreatedBy = FrmDashboard.UserId;
        //                dmndrawer.Status = 0;
        //                var InsertStatus = blldrawer.AddEditDatadrawer("prdInsertDrawer", dmndrawer);
        //                if (InsertStatus.Id > 0)
        //                {
        //                    FrmDrawer.DrawerID = 0;
        //                    tlp_outbtn.Visible = false;
        //                    tlp_previous_bill.Visible = false;
        //                    tlp_control.Visible = false;
        //                    FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
        //                    FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
        //                }
        //                #region Close all form Return Login page
        //                Application.Exit();
        //                //foreach (Form openForm in Application.OpenForms.Cast<Form>().ToList())
        //                //{
        //                //    if (!(openForm is FoodandBeverage.Forms.FrmLogin) && !(openForm is FoodandBeverage.Forms.FrmSplash))
        //                //        openForm.Close();
        //                //}

        //                //System.Threading.Thread.Sleep(500);
        //                //if (existingLogin == null)
        //                //{
        //                //    FoodandBeverage.Forms.FrmLogin loginForm = new FoodandBeverage.Forms.FrmLogin();
        //                //    loginForm.Show();
        //                //}
        //                //else
        //                //{
        //                //    existingLogin.Opacity = 100;
        //                //    existingLogin.BringToFront();
        //                //    existingLogin.Focus();
        //                //}
        //                #endregion

        //            }
        //            else
        //            {
        //                CMessageBox.Show("Zout Cannot be Completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            #endregion
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        clsgen.LogException("POSDashboard page zout_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
        //    }
        //}
        private void zout_Click(object sender, EventArgs e)
        {
            clsgen.WriteZoutSeparator();
            clsgen.StartZoutLog();
            
            clsgen.TrackZoutLog("POSDashboard", "ZOUT_BUTTON_CLICK", "Zout button clicked");

            try
            {
                clsgen.TrackZoutLog("POSDashboard", "PERMISSION_CHECK_START", "Checking Zout permission. UserAccountTypeId=" + FrmLogin.UserAccountTypeId);

                int permissionlevel = clsgen.CheckButtonPermission(Convert.ToInt32(FrmLogin.UserAccountTypeId), Convert.ToInt32(RolePermission.YOut));

                clsgen.TrackZoutLog("POSDashboard", "PERMISSION_CHECK_RESULT", "Zout permission level=" + permissionlevel);

                if (permissionlevel == Convert.ToInt32(PermissionLevel.NoPermission))
                {
                    clsgen.TrackZoutLog("POSDashboard", "EXTRA_ACCESS_REQUIRED", "User does not have direct Zout permission");

                    frmExtraAccessChk frm = new frmExtraAccessChk(Convert.ToInt32(RolePermission.YOut));

                    clsgen.TrackZoutLog("POSDashboard", "EXTRA_ACCESS_FORM_OPEN", "Opening extra access check form");

                    frm.ShowDialog();

                    clsgen.TrackZoutLog("POSDashboard", "EXTRA_ACCESS_FORM_CLOSED", "Extra access form closed. Access=" + frmExtraAccessChk.access);

                    if (frmExtraAccessChk.access == true)
                    {
                        clsgen.TrackZoutLog("POSDashboard", "EXTRA_ACCESS_GRANTED", "Zout access granted");
                        ProcessZout();
                    }
                    else
                    {
                        clsgen.TrackZoutLog("POSDashboard", "EXTRA_ACCESS_DENIED", "Zout access denied");
                    }
                }
                else if (permissionlevel == Convert.ToInt32(PermissionLevel.FullPermission) ||
                         permissionlevel == Convert.ToInt32(PermissionLevel.ReadOnly))
                {
                    clsgen.TrackZoutLog("POSDashboard", "DIRECT_ZOUT_PERMISSION", "User has direct Zout permission");
                    ProcessZout();
                }
                else
                {
                    clsgen.TrackZoutLog("POSDashboard", "ZOUT_PERMISSION_UNKNOWN", "Unexpected permission level=" + permissionlevel);
                }
            }
            catch (Exception ex)
            {
                clsgen.TrackZoutLog("POSDashboard", "ZOUT_CLICK_EXCEPTION", "Exception=" + ex.ToString());

                clsgen.LogException(
                    "POSDashboard page zout_Click()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.CombinedBillpage));
            }
        }

        private void ProcessZout()
        {
            clsgen.TrackZoutLog("POSDashboard", "PROCESS_ZOUT_START", "Zout process started");

            try
            {
                // =========================================================
                // DRAWER CHECK
                // =========================================================

                clsgen.TrackZoutLog("POSDashboard", "DRAWER_CHECK_START", "Checking assigned drawer");

                DrawerCheck();

                clsgen.TrackZoutLog("POSDashboard", "DRAWER_CHECK_RESULT", "DrawerID=" + FrmDrawer.DrawerID);

                if (FrmDrawer.DrawerID <= 0)
                {
                    clsgen.TrackZoutLog("POSDashboard", "DRAWER_NOT_ASSIGNED", "Zout cannot continue because DrawerID <= 0");

                    CMessageBox.Show(
                        "Zout Cannot be Completed",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    clsgen.TrackZoutLog("POSDashboard", "PROCESS_ZOUT_END", "Zout process stopped because drawer is not assigned");

                    return;
                }

                clsgen.TrackZoutLog("POSDashboard", "DRAWER_VALID", "Drawer is assigned. DrawerID=" + FrmDrawer.DrawerID);

                // =========================================================
                // ZOUT CONFIRMATION
                // =========================================================

                clsgen.TrackZoutLog("POSDashboard", "ZOUT_CONFIRMATION_START", "Showing Zout confirmation message");

                var confirmResult = CMessageBox.Show(
                    "This will print the Z-Out report and sign you out.\n\n" +
                    "Make sure all transactions are completed.\n\n" +
                    "Do you want to proceed and end your shift?",
                    "Confirm Z-Out and Sign Out",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                clsgen.TrackZoutLog("POSDashboard", "ZOUT_CONFIRMATION_RESULT", "Confirmation result=" + confirmResult);

                if (confirmResult != DialogResult.Yes)
                {
                    clsgen.TrackZoutLog("POSDashboard", "ZOUT_CANCELLED", "User cancelled Zout confirmation");
                    return;
                }

                clsgen.TrackZoutLog("POSDashboard", "ZOUT_CONFIRMATION_ACCEPTED", "User confirmed Zout");

                // =========================================================
                // CREATE ZOUT FORM
                // =========================================================

                clsgen.TrackZoutLog(
                    "POSDashboard",
                    "ZOUT_FORM_CREATE_START",
                    "Creating FrmZout. BillCategory=" + Convert.ToInt32(BillCategory.Zout) +
                    ", DrawerAssignStaffId=" + Drawerassginstaffid);

                using (FoodandBeverage.SaleBill.FrmZout frmzout =
                       new FoodandBeverage.SaleBill.FrmZout(
                           Convert.ToInt32(BillCategory.Zout),
                           Drawerassginstaffid))
                {
                    clsgen.TrackZoutLog("POSDashboard", "ZOUT_FORM_CREATED", "FrmZout created successfully");

                    clsgen.TrackZoutLog("POSDashboard", "ZOUT_MODE_CHECK", "XYDalvalue=" + FrmDashboard.XYDalvalue);

                    if (FrmDashboard.XYDalvalue)
                    {
                        clsgen.TrackZoutLog("POSDashboard", "ZOUT_SHOW_DIALOG_START", "Opening FrmZout.ShowDialog()");

                        frmzout.ShowDialog();

                        clsgen.TrackZoutLog("POSDashboard", "ZOUT_SHOW_DIALOG_RETURN", "FrmZout.ShowDialog() returned");
                    }
                    else
                    {
                        clsgen.TrackZoutLog("POSDashboard", "ZOUT_PRINTALL_START", "Calling FrmZout.PrintAll()");

                        frmzout.PrintAll();

                        clsgen.TrackZoutLog("POSDashboard", "ZOUT_PRINTALL_RETURN", "FrmZout.PrintAll() completed");
                    }
                }

                clsgen.TrackZoutLog("POSDashboard", "ZOUT_FORM_DISPOSED", "FrmZout disposed successfully");

                // =========================================================
                // PRINT COMPLETED MESSAGE
                // =========================================================

                clsgen.TrackZoutLog("POSDashboard", "ZOUT_PRINT_COMPLETED_MESSAGE_START", "Showing Zout Print Completed message");

                CMessageBox.Show(
                    "Zout Print Completed",
                    "Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                clsgen.TrackZoutLog("POSDashboard", "ZOUT_PRINT_COMPLETED_MESSAGE_CLOSED", "Zout Print Completed message closed");

                // =========================================================
                // DRAWER STATUS UPDATE
                // =========================================================

                clsgen.TrackZoutLog("POSDashboard", "DRAWER_STATUS_UPDATE_START", "Starting drawer status update");

                Dmndrawer dmndrawer = new Dmndrawer();
                Blldrawer blldrawer = new Blldrawer();

                if (flagDrawerF)
                {
                    dmndrawer.Id = Drawerassginid;

                    clsgen.TrackZoutLog("POSDashboard", "DRAWER_ID_SELECTED", "Using Drawerassginid=" + Drawerassginid);
                }
                else
                {
                    if (FrmDrawer.DrawerID > 0)
                    {
                        dmndrawer.Id = FrmDrawer.DrawerID;

                        clsgen.TrackZoutLog("POSDashboard", "DRAWER_ID_SELECTED", "Using FrmDrawer.DrawerID=" + FrmDrawer.DrawerID);
                    }
                    else
                    {
                        clsgen.TrackZoutLog("POSDashboard", "DRAWER_ID_MISSING", "FrmDrawer.DrawerID is not greater than zero");
                    }
                }

                dmndrawer.BranchId = FrmDashboard.BranchId;
                dmndrawer.CreatedBy = FrmDashboard.UserId;
                dmndrawer.Status = 0;

                clsgen.TrackZoutLog(
                    "POSDashboard",
                    "DRAWER_STATUS_UPDATE_PARAMETERS",
                    "DrawerId=" + dmndrawer.Id +
                    ", BranchId=" + dmndrawer.BranchId +
                    ", CreatedBy=" + dmndrawer.CreatedBy +
                    ", Status=" + dmndrawer.Status);

                clsgen.TrackZoutLog("POSDashboard", "DRAWER_STATUS_UPDATE_DATABASE_START", "Calling prdInsertDrawer");

                var InsertStatus = blldrawer.AddEditDatadrawer("prdInsertDrawer", dmndrawer);

                if (InsertStatus == null)
                {
                    clsgen.TrackZoutLog("POSDashboard", "DRAWER_STATUS_UPDATE_DATABASE_RESULT", "InsertStatus is NULL");
                }
                else
                {
                    clsgen.TrackZoutLog("POSDashboard", "DRAWER_STATUS_UPDATE_DATABASE_RESULT", "InsertStatus.Id=" + InsertStatus.Id);
                }

                if (InsertStatus != null && InsertStatus.Id > 0)
                {
                    clsgen.TrackZoutLog("POSDashboard", "DRAWER_STATUS_UPDATE_SUCCESS", "Drawer successfully closed/updated");

                    FrmDrawer.DrawerID = 0;

                    clsgen.TrackZoutLog("POSDashboard", "DRAWER_ID_RESET", "FrmDrawer.DrawerID reset to 0");

                    tlp_outbtn.Visible = false;
                    tlp_previous_bill.Visible = false;
                    tlp_control.Visible = false;

                    FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                    FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;

                    clsgen.TrackZoutLog("POSDashboard", "DASHBOARD_DRAWER_UI_UPDATED", "Dashboard drawer status changed to Not Assigned");
                }
                else
                {
                    clsgen.TrackZoutLog(
                        "POSDashboard",
                        "DRAWER_STATUS_UPDATE_FAILED",
                        "Drawer update failed. InsertStatus.Id=" +
                        (InsertStatus == null ? "NULL" : InsertStatus.Id.ToString()));
                }

                // =========================================================
                // APPLICATION EXIT
                // =========================================================

                clsgen.TrackZoutLog("POSDashboard", "APPLICATION_EXIT_START", "Calling Application.Exit()");

                Application.Exit();

                clsgen.TrackZoutLog("POSDashboard", "APPLICATION_EXIT_RETURNED", "Application.Exit() returned");
            }
            catch (Exception ex)
            {
                clsgen.TrackZoutLog("POSDashboard", "PROCESS_ZOUT_EXCEPTION", "Exception=" + ex.ToString());

                clsgen.LogException(
                    "POSDashboard page ProcessZout()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        private void normal_rpt_Click(object sender, EventArgs e)
        {
            try
            {
                DrawerCheck();
                using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.DailyCollection), Drawerassginstaffid))
                {
                    if (FrmDashboard.XYDalvalue)
                        frmzout.ShowDialog();
                    else
                        frmzout.PrintAll();
                }
               CMessageBox.Show("Normal Report Print Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("POSDashboard page normal_rpt_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        private void detailed_rpt_Click(object sender, EventArgs e)
        {
            DrawerCheck();
            using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.DetailedCollection), 0))
            {
                frmzout.PrintDetailsCollection();
            }
           CMessageBox.Show("Details Collection Report Printing Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void txtinitialamt_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enablenumberkeypad)
            {
                decimal kdata = 0;
                using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
                {
                    frm.ShowDialog();
                    kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                    frm.Dispose();
                }
                if (sender is TextBox txt)
                {
                    txt.Text = kdata.ToString("0.##");
                }

            }
        }
        private void txtinitialamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (char.IsDigit(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == '.' && !txt.Text.Contains("."))
            {
                return;
            }
            e.Handled = true;
        }
        private void dgv_split_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex == 2)
                //{
                //    FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(Convert.ToInt32(dgv_split.Rows[e.RowIndex].Cells[3].Value), Convert.ToInt32(PaymentType.SPLIT), Convert.ToInt32(dgv_split.Rows[e.RowIndex].Cells[4].Value), Convert.ToInt32(BillType.NormalBill));
                //    frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                //    frmSalesInvoice.ShowDialog();
                //}
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page dgv_split_CellContentClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }

        private void dgvorderview_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        #region Keyboard
        public void ShowKeyboard(TextBox activeTextBox)
        {
            if (activeTextBox == null) return;

            Point textBoxScreenLocation = activeTextBox.PointToScreen(Point.Empty);
            int desiredX = 0, desiredY = 0;
            if (Frmkeyboard.IsOpen)
            {
                #region Calculate Position of Keyboard 
                desiredX = textBoxScreenLocation.X + (activeTextBox.Width / 2) - (Frmkeyboard.Instance.Width / 2);
                desiredY = textBoxScreenLocation.Y + activeTextBox.Height + 5;
                Rectangle screenBounds = Screen.FromControl(activeTextBox).WorkingArea;
                if (desiredX < screenBounds.Left)
                    desiredX = screenBounds.Left;
                else if (desiredX + Frmkeyboard.Instance.Width > screenBounds.Right)
                    desiredX = screenBounds.Right - Frmkeyboard.Instance.Width;
                if (desiredY + Frmkeyboard.Instance.Height > screenBounds.Bottom)
                {
                    desiredY = textBoxScreenLocation.Y - Frmkeyboard.Instance.Height - 5;
                    if (desiredY < screenBounds.Top)
                        desiredY = screenBounds.Top;
                }
                #endregion
            }

            if (Frmkeyboard.IsOpen)
            {
                Frmkeyboard.Instance.StartPosition = FormStartPosition.Manual;
                Frmkeyboard.Instance.Location = new Point(desiredX, desiredY);
                Frmkeyboard.Instance.BringToFront();
            }
            else
            {
                Frmkeyboard frmKey = new Frmkeyboard(0);

                #region Calculate Position of Keyboard 
                desiredX = textBoxScreenLocation.X + (activeTextBox.Width / 2) - (Frmkeyboard.Instance.Width / 2);
                desiredY = textBoxScreenLocation.Y + activeTextBox.Height + 5;
                Rectangle screenBounds = Screen.FromControl(activeTextBox).WorkingArea;
                if (desiredX < screenBounds.Left)
                    desiredX = screenBounds.Left;
                else if (desiredX + Frmkeyboard.Instance.Width > screenBounds.Right)
                    desiredX = screenBounds.Right - Frmkeyboard.Instance.Width;
                if (desiredY + Frmkeyboard.Instance.Height > screenBounds.Bottom)
                {
                    desiredY = textBoxScreenLocation.Y - Frmkeyboard.Instance.Height - 5;
                    if (desiredY < screenBounds.Top)
                        desiredY = screenBounds.Top;
                }
                Frmkeyboard.Instance.StartPosition = FormStartPosition.Manual;
                Frmkeyboard.Instance.Location = new Point(desiredX, desiredY);
                #endregion


                frmKey.OnKeyPressed += (keyText) =>
                {
                    if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.backspace))
                    {
                        if (activeTextBox.Text.Length > 0)
                            activeTextBox.Text = activeTextBox.Text.Substring(0, activeTextBox.Text.Length - 1);
                    }
                    else if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.space))
                    {
                        activeTextBox.Text += " ";
                    }
                    else if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.clear))
                    {
                        activeTextBox.Text = "";
                    }
                    else
                    {
                        activeTextBox.Text += keyText;
                    }
                    activeTextBox.SelectionStart = activeTextBox.Text.Length;
                    activeTextBox.SelectionLength = 0;
                };

                frmKey.Show();
            }

        }
        #endregion        

        #region drawerAssign
        public void AssignDrawerCheck()
        {
            try
            {
                BllSearch<DmnGridFields> bllsearchdrawer = new BllSearch<DmnGridFields>();
                DmnSearch dmnsearckdrawer = new DmnSearch();
                dmnsearckdrawer.Select = "COUNT(*) OVER() AS Field1,t.CreatedBy as Field2,e.EmployeeName as Field3,d.DesignationName as Field4,t.Id as Field5,t.LoanAmount as Field6,t.Drops as Field7,u.UserAccountId as Field8,e.EmployeeId as Field9";
                dmnsearckdrawer.Table = "tblDrawerAssign t   left join tblUserAccount  u on u.EmployeeId= t.EmployeeId left join tblEmployee  e on e.EmployeeId=t.EmployeeId left join tblDesignation  d on d.DesignationId=e.DesignationId";
                dmnsearckdrawer.WhereNdGroupBy = "where t.DName='" + System.Environment.MachineName + "'  and t.Status=1";
                dmnsearckdrawer.OrderBy = "";
                var gridesource = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckdrawer);
                if (gridesource.Count > 0)
                {

                    //if (FrmDashboard.AllowDrawerpermissionFullControl) //full permission 
                    //{
                    //    if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
                    //    {
                    //        FrmDashboard.Instance.lbl_drawer.Text = "Drawer Assigned on " + gridesource[0].Field3 + " , " + gridesource[0].Field4;
                    //        FrmDashboard.Instance.lbl_drawer.Visible = true;
                    //        FrmDashboard.Instance.lbl_drawer.ForeColor = Color.White;
                    //    }
                    //}
                    //else// only self assign permission 
                    //{
                        if (Convert.ToInt32(gridesource[0].Field9) == Convert.ToInt32(FrmLogin.EmployeeId)) //assigned employee login  
                        {
                           FrmDrawer.DrawerID = Convert.ToInt32(gridesource[0].Field5);
                            FrmDashboard.Instance.lbl_drawer.Text = "Drawer Assigned on " + gridesource[0].Field3 + " , " + gridesource[0].Field4;
                            FrmDashboard.Instance.lbl_drawer.Visible = true;
                            FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Green;
                        }
                        else // other employee drawer assigned on other employee
                        {
                           CMessageBox.Show("Drawer Already Assigned on " + gridesource[0].Field3 + " , " + gridesource[0].Field4, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
                            {
                                FrmDashboard.Instance.lbl_drawer.Text = "Drawer Assigned on " + gridesource[0].Field3 + " , " + gridesource[0].Field4;
                                FrmDashboard.Instance.lbl_drawer.Visible = true;
                                FrmDashboard.Instance.lbl_drawer.ForeColor = Color.White;
                            }
                            return;
                        }
                   // }
                }
                else //drawer not assign
                {
                    //if (FrmDashboard.AllowDrawerpermissionFullControl)
                    //{

                    //}
                    if (FrmDashboard.AllowCashDrawer)
                    {
                        FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                        FrmDashboard.Instance.lbl_drawer.Visible = true;
                        FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
                        if (DrawerCheckBranch(Convert.ToInt32(FrmLogin.EmployeeId)))
                        {
                           CMessageBox.Show("Drawer Already Assigned on " + Drawersystemname + "System", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        DialogResult result =CMessageBox.Show("Please Assign Drawer First", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                        if (result == DialogResult.OK)
                        {
                            if (System.Windows.Forms.Application.OpenForms["FrmDrawer"] != null)
                            {
                                FrmDrawer _instanceFrmDrawer = Application.OpenForms["FrmDrawer"] as FrmDrawer;
                                if (_instanceFrmDrawer.WindowState == FormWindowState.Minimized)
                                    _instanceFrmDrawer.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                else
                                {
                                    _instanceFrmDrawer.Visible = true;
                                    _instanceFrmDrawer.Dock = DockStyle.Fill;
                                    _instanceFrmDrawer.ShowDialog();
                                    _instanceFrmDrawer.BringToFront();
                                }
                            }
                            else
                            {
                                using (FrmDrawer _instanceFrmDrawer = new FrmDrawer())
                                {
                                    _instanceFrmDrawer.BringToFront();
                                    _instanceFrmDrawer.ShowDialog();
                                }
                            }
                        }
                        else
                        {
                            if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
                            {
                                FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                                FrmDashboard.Instance.lbl_drawer.Visible = true;
                                FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
                            }
                        }
                    }

                    if (FrmDashboard.AllowDrawerpermissionFullControl == false && FrmDashboard.AllowCashDrawer == false) //Both permission are false
                    {
                       CMessageBox.Show("You do not have permission to assign a drawer.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                        FrmDashboard.Instance.lbl_drawer.Visible = true;
                        FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("CombinedBillpage page  AssignDrawerCheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }

        }

        public void DrawerCheck()
        {
            AssignDrawerCheck();
            if (FrmDrawer.DrawerID == 0)
            {
                return;
            }
            else
            {
                if (FrmDrawer.DrawerID > 0 && !CombineBillAutoTimer.Enabled)
                {
                    tlp_outbtn.Visible = true;
                    Billcomplete_Fill();
                    cmb_tbl_names.Items.Add("List");

                    pnl_previous_bill.Visible = false;
                    flp_billlayout.Visible = true;
                    flp_billlayout.BringToFront();

                }
                else if(!tlp_control.Visible)
                {
                    tlp_outbtn.Visible = true;
                    tlp_previous_bill.Visible = true;
                    tlp_control.Visible = true;
                }
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

        private void FrmCombinedBill_Resize(object sender, EventArgs e)
        {
            UIHelper.RoundCorners(flp_sidemenu, 25);
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            FrmDashboard.Instance.Menuhome.PerformClick();
            this.Close(); 
        }

        private void btn_pickup_Click(object sender, EventArgs e)
        {
            long mem = Process.GetCurrentProcess().PrivateMemorySize64 / (1024 * 1024); // MB

            if (mem > 1000)
            {
                this.Close();
                FrmDashboard.Instance.StartSyncAnimation();
            }
            else
            {
                FrmDashboard.Instance.Pickuppage();
            }
           
        }



        public bool DrawerCheckBranch(int empid)
        {
            try
            {
                Drawersystemname = "";
                BllSearch<DmnGridFields> bllsearchdrawer = new BllSearch<DmnGridFields>();
                DmnSearch dmnsearckdrawer = new DmnSearch();
                dmnsearckdrawer.Select = "COUNT(*) OVER() AS Field1,t.CreatedBy as Field2,e.EmployeeName as Field3,d.DesignationName as Field4,t.Id as Field5,t.LoanAmount as Field6,t.Drops as Field7,u.UserAccountId as Field8,e.EmployeeId as Field9,t.DName as Field10";
                dmnsearckdrawer.Table = "tblDrawerAssign t   left join tblUserAccount  u on u.EmployeeId= t.EmployeeId left join tblEmployee  e on e.EmployeeId=t.EmployeeId left join tblDesignation  d on d.DesignationId=e.DesignationId";
                dmnsearckdrawer.WhereNdGroupBy = "where  t.BranchId=" + FrmDashboard.BranchId + " and t.Status=1 and t.EmployeeId =" + empid;
                dmnsearckdrawer.OrderBy = "";
                var gridesource = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckdrawer);
                if (gridesource.Count > 0)
                {
                    Drawersystemname = gridesource[0].Field10;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Admin page DrawerCheckBranch()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
        }
        #endregion

    }
}

