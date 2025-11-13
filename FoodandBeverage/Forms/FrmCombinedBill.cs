using BLL;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FoodandBeverage.Forms.UserControl;

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
        TableLayoutPanel tlp_table_dashboard = new TableLayoutPanel();

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

        #endregion

        /*permission checking for manager and supervisor*/
        public static int DrawerID;   

        public string Drawerassginstaffname;
        public string Drawerassginstaffdes;
        public int Drawerassginid;
        public int Drawerassginstaffid;

        public string Drawersystemname;

        #region Grid Declaration
        public static int cslno = 0;
        public static int actionno = 1;
        public static int orderdatetimeno = 2;
        public static int entryno = 3;
        public static int tablecodeno = 4;
        public static int totalqtyno = 5;
        public static int netamountno = 6;
        public static int recamountno = 7;

        public static int balamountno = 8;
        public static int paytypename = 9;
        public static int statusno = 10;
        public static int ordermasteridno = 11;

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

                CheckPermission();
                AssignDrawerCheck();
                if(DrawerID>0 && !CombineBillAutoTimer.Enabled)
                {
                    tlp_outbtn.Visible = true;
                   
                    Billcomplete_Fill();
                    cmb_tbl_names.Items.Add("List");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page FrmCombinedBill_Load", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }

        }

        #region AssignDrawerCheck
        public void AssignDrawerCheck()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtdrawerid.Text))
                    txtdrawerid.Text = "0";
                DrawerID = 0;
                BllSearch<DmnGridFields> bllsearchdrawer = new BllSearch<DmnGridFields>();
                DmnSearch dmnsearckdrawer = new DmnSearch();
                dmnsearckdrawer.Select = "COUNT(*) OVER() AS Field1,t.CreatedBy as Field2,e.EmployeeName as Field3,d.DesignationName as Field4,t.Id as Field5,t.LoanAmount as Field6,t.Drops as Field7,u.UserAccountId as Field8,e.EmployeeId as Field9";
                dmnsearckdrawer.Table = "tblDrawerAssign t   left join tblUserAccount  u on u.EmployeeId= t.EmployeeId left join tblEmployee  e on e.EmployeeId=t.EmployeeId left join tblDesignation  d on d.DesignationId=e.DesignationId";
                dmnsearckdrawer.WhereNdGroupBy = "where t.DName='" + System.Environment.MachineName + "'  and t.Status=1";
                dmnsearckdrawer.OrderBy = "";
                var gridesource = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckdrawer);

                #region Cash Drawer Assign on Employee
                if (gridesource.Count > 0)
                {                    
                    if (Convert.ToInt32(gridesource[0].Field9) == Convert.ToInt32(FrmLogin.EmployeeId))
                    {
                        //asigned employee
                    }
                    else
                    {
                        //Drawer assigned on other employe
                        MessageBox.Show("Drawer Already Assigned on " + gridesource[0].Field3 + " , " + gridesource[0].Field4 + "  DateTime " + gridesource[0].Field10, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    if (flagDrawerF) //full permission 
                    {
                        tlp_outbtn.Visible = true;
                        tlp_previous_bill.Visible = true;
                        tlp_compl_pre_bill.Visible = true;
                        if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
                        {
                            FrmDashboard.Instance.lbl_drawer.Text = "Drawer Assigned on " + gridesource[0].Field3 + " , " + gridesource[0].Field4;
                            FrmDashboard.Instance.lbl_drawer.Visible = true;
                            FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Green;
                        }
                    }
                    else// only self assign permission 
                    {
                        if (Convert.ToInt32(gridesource[0].Field9) == Convert.ToInt32(FrmLogin.EmployeeId)) //assigned employee login  
                        {
                            DrawerID = Convert.ToInt32(gridesource[0].Field5);
                            tlp_outbtn.Visible = true;
                            tlp_previous_bill.Visible = true;
                            tlp_compl_pre_bill.Visible = true;
                            ShowDrawerEmployee();
                        }
                        else // other employee drawer assigned on other employee
                        {
                            MessageBox.Show("Drawer Already Assigned on " + gridesource[0].Field3 + " , " + gridesource[0].Field4, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
                            {
                                FrmDashboard.Instance.lbl_drawer.Text = "Drawer Assigned on " + gridesource[0].Field3 + " , " + gridesource[0].Field4;
                                FrmDashboard.Instance.lbl_drawer.Visible = true;
                                FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Green;
                            }
                            tlp_outbtn.Visible = false;
                            tlp_previous_bill.Visible = false;
                            tlp_compl_pre_bill.Visible = false;
                        }
                    }
                }
                #endregion

                #region Cash Drawer Not Assigned
                else //drawer not assign
                {
                    label15.Text = System.Environment.MachineName;

                    #region Full permission 
                    //if (FrmDashboard.AllowDrawerpermissionFullControl)
                    //{
                    //    pnl_Drawer.Location = new Point(this.ClientSize.Width / 2 - pnl_Drawer.Size.Width / 2, this.ClientSize.Height / 2 - pnl_Drawer.Size.Height / 2);
                    //    pnl_Drawer.Visible = true;

                    //    if (FrmDashboard.DrawerId > 0)
                    //    {
                    //        tlp_outbtn.Visible = false;
                    //        tlp_previous_bill.Visible = false;
                    //        tlp_compl_pre_bill.Visible = false;
                    //        ShowDrawerEmployee();
                    //    }
                    //    else
                    //    {
                    //        tlp_outbtn.Visible = false;
                    //        tlp_previous_bill.Visible = false;
                    //        tlp_compl_pre_bill.Visible = false;
                    //        if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
                    //        {
                    //            FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                    //            FrmDashboard.Instance.lbl_drawer.Visible = true;
                    //            FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
                    //        }

                    //    }

                    //}
                    #endregion

                    #region only self Assigned 
                    if (FrmDashboard.AllowCashDrawer)
                    {
                        FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                        FrmDashboard.Instance.lbl_drawer.Visible = true;
                        FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
                        if (DrawerCheckBranch(Convert.ToInt32(FrmLogin.EmployeeId)))
                        {
                            MessageBox.Show("Drawer Already Assigned on " + Drawersystemname + "System", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        DialogResult result = MessageBox.Show("Please Assign Drawer First", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.OK)
                        {
                            pnl_Drawer.BringToFront();
                            pnl_Drawer.Location = new Point(this.ClientSize.Width / 2 - pnl_Drawer.Size.Width / 2, this.ClientSize.Height / 2 - pnl_Drawer.Size.Height / 2);
                            pnl_Drawer.Visible = true;
                            txtinitialamt.Text = "";
                            if (FrmDashboard.DrawerId > 0)
                            {
                                ShowDrawerEmployee();
                                tlp_outbtn.Visible = true;
                                tlp_previous_bill.Visible = true;
                                tlp_compl_pre_bill.Visible = true;
                            }
                            else
                            {
                                tlpemplist.Visible = false;

                                lbl_staffname.Visible = true;
                                lbl_staffname.Text = FrmLogin.EmployeName;

                                tlp_outbtn.Visible = false;
                                tlp_previous_bill.Visible = false;
                                tlp_compl_pre_bill.Visible = false;
                                if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
                                {
                                    FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                                    FrmDashboard.Instance.lbl_drawer.Visible = true;
                                    FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
                                }
                            }

                        }
                        else
                        {
                            tlp_outbtn.Visible = false;
                            tlp_previous_bill.Visible = false;
                            tlp_compl_pre_bill.Visible = false;
                            if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
                            {
                                FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                                FrmDashboard.Instance.lbl_drawer.Visible = true;
                                FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
                            }
                        }
                    }
                    #endregion

                    if (FrmDashboard.AllowCashDrawer == false && FrmDashboard.AllowDrawerpermissionFullControl == false) //Both permission are false
                    {
                        MessageBox.Show("You do not have permission to assign a drawer.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                        FrmDashboard.Instance.lbl_drawer.Visible = true;
                        FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                // MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("POSDashboard page  AssignDrawerCheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }

        }
        public void CheckPermission()
        {
            try
            {
                flagDrawerF = false; flagDrawer = false;
                if (Convert.ToInt32(FrmDashboard.AllowCashDrawer) == Convert.ToInt32(formsenum.AllowCashDrawer))
                {
                    lbl_staffname.Visible = true;
                    tlpemplist.Visible = false;
                    lbl_staffname.Text = FrmLogin.EmployeName;
                    lbl_desigtnation.Text = FrmLogin.Designation;
                    txtempoyeId.Text = Convert.ToString(FrmLogin.EmployeeId);
                    lbl_staffname_tip.Visible = false;
                    flagDrawer = true;

                }
                else if (Convert.ToInt32(FrmDashboard.AllowDrawerpermissionFullControl) == Convert.ToInt32(formsenum.AllowDrawerpermissionFullControl))
                {
                    lbl_staffname.Visible = false;
                    tlpemplist.Visible = true;
                    lbl_desigtnation.Text = FrmLogin.Designation;
                    lbl_staffname_tip.Visible = true;
                    flagDrawerF = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void ShowDrawerEmployee()
        {
            try
            {
                BllSearch<DmnGridFields> bllsearchdrawer = new BllSearch<DmnGridFields>();
                DmnSearch dmnsearckdrawer = new DmnSearch();
                //lbl_msg2.Text = "welcome " + FrmLogin.EmployeName + " , " + FrmLogin.Designation;
                dmnsearckdrawer.Select = "COUNT(*) OVER() AS Field1,t.CreatedBy as Field2,e.EmployeeName as Field3,d.DesignationName as Field4,t.Id as Field5,t.LoanAmount as Field6,t.Drops as Field7,u.UserAccountId as Field8,e.EmployeeId as Field9";
                dmnsearckdrawer.Table = "tblDrawerAssign t   left join tblUserAccount  u on u.EmployeeId= t.EmployeeId left join tblEmployee  e on e.EmployeeId=t.EmployeeId left join tblDesignation  d on d.DesignationId=e.DesignationId";
                dmnsearckdrawer.WhereNdGroupBy = "where t.DName='" + System.Environment.MachineName + "' and t.Status=1";
                dmnsearckdrawer.OrderBy = "";
                var gridesource1 = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckdrawer);
                if (gridesource1.Count > 0)
                {
                    if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
                    {
                        FrmDashboard.Instance.lbl_drawer.Text = "Drawer Assigned on " + gridesource1[0].Field3 + " , " + gridesource1[0].Field4;
                        FrmDashboard.Instance.lbl_drawer.Visible = true;
                        FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Green;
                    }

                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("POSDashboard page  ShowDrawerEmployee()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
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
                dmnsearckdrawer.WhereNdGroupBy = "where t.Status=1 and t.EmployeeId =" + empid;
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


        public void Billcomplete_Fill()
        {
            try
            {
                int rowIndex_cate = 0;
                this.Visible = false;
                this.SuspendLayout();
                ListFillFlag = false;
                this.Controls.Add(pnl_complebill_prevbill);
                pnl_complebill_prevbill.Dock = DockStyle.Fill;
                Control controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
                tlp_compl_pre_bill.Controls.Remove(controlToRemove);
                tlp_compl_pre_bill.Controls.Add(pnl_compelte_bill, 0, 1);
                tlp_compl_pre_bill.Dock = DockStyle.Fill;

                pnl_empty_msg.Visible = false;
                pnl_complebill_prevbill.Visible = true;

                #region old code fetch db to query No use currently use store procedure
                //BllSearch<DmnTableinfo> bllSearch = new BllSearch<DmnTableinfo>();
                //DmnSearch dmnSearch = new DmnSearch();
                //dmnSearch.Select = "ti.* ,c.CategoryName as CategoryName,om.NetAmount ,(select CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1 from tblOrderDetails WHERE status!=0 and AddonStatus in(1,0) and OrderMasterId =ti.OrderMasterId ) BillStatus,cob.Cbid," +
                //    "(select top (1) bd.BookmasterId from tblbookMaster bm  LEFT JOIN tblBookDetails bd on bm.BookmasterId = bd.BookmasterId where bm.Status = 1 and bd.Status = 1 and SYSDATETIME() >= DATEADD(MINUTE," + -Convert.ToDecimal(FrmLogin.BookingInterwell) + ", BookingDateTime) and MTID = ti.tid and rid=0  order by bm.BookingDateTime desc) BookmasterId";
                //dmnSearch.Table = "tblTable_info  ti " +
                //    "LEFT JOIN tblCategory_FTI c on c.Fcid=ti.Fcid " +
                //    "LEFT JOIN tblOrderMaster om on om.OrderMasterId=ti.OrderMasterId and om.Status !=0" +
                //    " LEFT JOIN tblCombinedOrderBill cob on cob.OrderMasterid=ti.OrderMasterId";
                //dmnSearch.WhereNdGroupBy = "where ti.Status=1";
                //dmnSearch.OrderBy = "order by ti.tid";
                //var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                #endregion

                BllTableData<DmncombinedBill> SearchDetails = new BllTableData<DmncombinedBill>();
                DmncombinedBill dmnSearchtabledata = new DmncombinedBill();
                dmnSearchtabledata.BookingInterwell = Convert.ToDecimal(FrmLogin.BookingInterwell);
                var gridSource = SearchDetails.serachreport("prdBillData", dmnSearchtabledata);
                //var sortedGridSource = gridSource.OrderBy(item => item.t).ToList();
                pnl_compelte_bill.Controls.Clear();
                tlp_table_dashboard.Controls.Clear();
                tlp_table_dashboard.Dock = DockStyle.Fill;
                tlp_table_dashboard.AutoScroll = true;

                #region Pickup Bill
                TableLayoutPanel tlp_category_PK = new TableLayoutPanel();
                tlp_category_PK.Name = "tlp_category_Com_Pickup";
                tlp_category_PK.Tag = 0;
                tlp_category_PK.Dock = DockStyle.Top;
                tlp_category_PK.AutoSize = true;
                //tlp_category_PK.BackColor = Color.Green;
                tlp_category_PK.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                tlp_category_PK.RowCount = 2;
                tlp_category_PK.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tlp_category_PK.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                FlowLayoutPanel flp_box_PK = new FlowLayoutPanel();
                flp_box_PK.Name = "flp_box_Com_Pickup";
                flp_box_PK.Tag = 0;
                flp_box_PK.Dock = DockStyle.Fill;
                flp_box_PK.AutoSize = true;
                flp_box_PK.FlowDirection = FlowDirection.LeftToRight;
                //flp_box.BackColor = Color.Blue;

                // Configure the Label
                Label lblcategory_head_pk = new Label();
                lblcategory_head_pk.Name = "lbcate_head_Com_Pickup";
                lblcategory_head_pk.Tag = 0;
                lblcategory_head_pk.Text = "PICK UP";
                lblcategory_head_pk.ForeColor = Color.Black;
                lblcategory_head_pk.BackColor = Color.Transparent;
                lblcategory_head_pk.Dock = DockStyle.Top;
                lblcategory_head_pk.Font = new Font(lblcategory_head_pk.Font.FontFamily, 12, FontStyle.Bold);

                tlp_category_PK.Controls.Add(lblcategory_head_pk, 0, 0);
                tlp_category_PK.Controls.Add(flp_box_PK, 0, 1);
                tlp_table_dashboard.Controls.Add(tlp_category_PK, 0, rowIndex_cate);
                rowIndex_cate++;
                #endregion

                if (gridSource.Count > 0)
                {

                    foreach (var tabledata in gridSource)
                    {
                        if (Convert.ToInt32(tabledata.PickupOrderFlag) == 1 && Convert.ToInt32(tabledata.BillStatus) == 0)
                        {
                            #region Pickup Box creation 
                            string flpname = "flp_box_Com_Pickup";
                            FlowLayoutPanel flp_box_check_pk = tlp_table_dashboard.Controls.Find(flpname, true).FirstOrDefault() as FlowLayoutPanel;

                            #region pickup Box creation

                            #region tlp_table_inner_pk
                            // Configure the TableLayoutPanel
                            TableLayoutPanel tlp_table_inner_pk = new TableLayoutPanel();
                            tlp_table_inner_pk.RowCount = 3;
                            tlp_table_inner_pk.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
                            tlp_table_inner_pk.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
                            tlp_table_inner_pk.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
                            tlp_table_inner_pk.Name = "tlp_com_pk" + tabledata.pkid;
                            tlp_table_inner_pk.Tag = Convert.ToInt32(CombinedBillType.PickupBill);
                            tlp_table_inner_pk.BorderStyle = BorderStyle.FixedSingle;
                            tlp_table_inner_pk.Dock = DockStyle.None;
                            tlp_table_inner_pk.Tag = Convert.ToInt32(DashboardTableType.PickupOrder) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                            //tlp_table_inner_pk.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;

                            tlp_table_inner_pk.BackColor = Color.White;

                            if (screenWidth > 1366)
                            {
                                tlp_table_inner_pk.Width = 120;
                                tlp_table_inner_pk.Height = 80;
                                tlp_table_inner_pk.Margin = new Padding(5, 5, 0, 5);
                            }
                            else if (screenWidth >= 1024 && screenWidth <= 1366)
                            {
                                tlp_table_inner_pk.Width = 120;
                                tlp_table_inner_pk.Height = 80;
                                tlp_table_inner_pk.Margin = new Padding(10, 10, 0, 10);
                            }
                            #endregion

                            #region btn_select_pk
                            Button btn_select_pk = new Button();
                            btn_select_pk.Name = "btn_select_pk_com" + tabledata.pkid;
                            btn_select_pk.AccessibleDescription = Convert.ToInt32(DashboardTableType.PickupOrder) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                            btn_select_pk.Tag = Convert.ToInt32(BillSelect.Unselect);
                            btn_select_pk.FlatStyle = FlatStyle.Flat;
                            btn_select_pk.FlatAppearance.BorderSize = 0;
                            btn_select_pk.BackgroundImage = Properties.Resources.Billaddicon;
                            btn_select_pk.BackgroundImageLayout = ImageLayout.Stretch;
                            btn_select_pk.Dock = DockStyle.Fill;
                            btn_select_pk.Margin = new Padding(20, 0, 20, 0);
                            btn_select_pk.Click += new EventHandler(btn_select_Clicked);
                            #endregion

                            #region lbltablename_pk
                            // Configure the Label
                            Label lbltablename_pk = new Label();
                            lbltablename_pk.Name = "lb_tablename" + tabledata.pkid;
                            lbltablename_pk.AccessibleDescription = Convert.ToInt32(DashboardTableType.PickupOrder) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                            lbltablename_pk.TextAlign = ContentAlignment.MiddleCenter;
                            lbltablename_pk.Tag = Convert.ToInt32(CombinedBillType.PickupBill);
                            lbltablename_pk.Text = Convert.ToString(tabledata.pkid);
                            lbltablename_pk.ForeColor = Description_color;
                            lbltablename_pk.BackColor = Color.Transparent;
                            lbltablename_pk.Font = new Font("Arial", 10, FontStyle.Bold);
                            lbltablename_pk.Dock = DockStyle.Fill;
                            #endregion

                            #region netamountlabel_pk
                            // Configure the Label
                            Label netamountlabel_pk = new Label();
                            netamountlabel_pk.Name = "lb_netamount" + tabledata.pkid;
                            netamountlabel_pk.AccessibleDescription = Convert.ToInt32(DashboardTableType.PickupOrder) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                            netamountlabel_pk.TextAlign = ContentAlignment.MiddleCenter;
                            netamountlabel_pk.Tag = Convert.ToInt32(CombinedBillType.PickupBill);
                            netamountlabel_pk.Text = "\u20B9" + Convert.ToString(tabledata.NetAmount);
                            netamountlabel_pk.ForeColor = Netamountcolor;
                            netamountlabel_pk.BackColor = Color.Transparent;
                            netamountlabel_pk.Font = new Font("Arial", 10, FontStyle.Bold);
                            netamountlabel_pk.Dock = DockStyle.Fill;
                            #endregion

                            if (Convert.ToInt32(tabledata.BillStatus) == 0)
                                tlp_table_inner_pk.Visible = true;
                            else
                                tlp_table_inner_pk.Visible = false;

                            if (tabledata.BookmasterId > 0)// && tabledata.TableCount > 1//Bokking order hide plus button
                            {
                                btn_select_pk.Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt32(tabledata.Cbid) > 0)
                                {
                                    tlp_table_inner_pk.BackColor = CombinedBillcolor;
                                    btn_select_pk.Visible = false;
                                }
                                else
                                {
                                    tlp_table_inner_pk.BackColor = NormalBillingColor;
                                    btn_select_pk.Visible = true;
                                }
                            }

                            tlp_table_inner_pk.Controls.Add(btn_select_pk, 0, 0);
                            tlp_table_inner_pk.Controls.Add(lbltablename_pk, 0, 1);
                            tlp_table_inner_pk.Controls.Add(netamountlabel_pk, 0, 2);

                            lbltablename_pk.Click += new EventHandler(CompleteEventHandler);
                            netamountlabel_pk.Click += new EventHandler(CompleteEventHandler);
                            tlp_table_inner_pk.Click += new EventHandler(CompleteEventHandler);

                            flp_box_check_pk.Controls.Add(tlp_table_inner_pk);
                            #endregion

                            #endregion
                        }
                        else if (Convert.ToInt32(tabledata.Tid) > 0)
                        {
                            #region Normal , Combined ,bokking orders
                            string tlpname = "tlp_category_Com_" + tabledata.CategoryName;
                            TableLayoutPanel tlp_box = tlp_table_dashboard.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                            if (tlp_box == null)
                            {
                                TableLayoutPanel tlp_category = new TableLayoutPanel();
                                tlp_category.Name = "tlp_category_Com_" + tabledata.CategoryName;
                                tlp_category.Tag = tabledata.CategoryName;
                                tlp_category.Dock = DockStyle.Top;
                                tlp_category.AutoSize = true;
                                //tlp_category.BackColor = Color.Green;
                                tlp_category.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                                tlp_category.RowCount = 2;
                                tlp_category.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                tlp_category.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                                FlowLayoutPanel flp_box = new FlowLayoutPanel();
                                flp_box.Name = "flp_box_Com_" + tabledata.CategoryName;
                                flp_box.Tag = tabledata.CategoryName;
                                flp_box.Dock = DockStyle.Fill;
                                flp_box.AutoSize = true;
                                flp_box.FlowDirection = FlowDirection.LeftToRight;
                                //flp_box.BackColor = Color.Blue;

                                // Configure the Label
                                Label lblcategory_head = new Label();
                                lblcategory_head.Name = "lbcate_head_Com_" + tabledata.CategoryName;
                                lblcategory_head.Tag = tabledata.Fcid;
                                lblcategory_head.Text = tabledata.CategoryName;
                                lblcategory_head.ForeColor = Color.Black;
                                lblcategory_head.BackColor = Color.Transparent;
                                lblcategory_head.Dock = DockStyle.Top;
                                lblcategory_head.Font = new Font(lblcategory_head.Font.FontFamily, 12, FontStyle.Bold);

                                tlp_category.Controls.Add(lblcategory_head, 0, 0);
                                tlp_category.Controls.Add(flp_box, 0, 1);
                                tlp_table_dashboard.Controls.Add(tlp_category, 0, rowIndex_cate);
                                rowIndex_cate++;
                            }

                            string flpname = "flp_box_Com_" + tabledata.CategoryName;
                            FlowLayoutPanel flp_box_check = tlp_table_dashboard.Controls.Find(flpname, true).FirstOrDefault() as FlowLayoutPanel;

                            #region Table Box 
                            // Configure the TableLayoutPanel
                            TableLayoutPanel tlp_table_inner = new TableLayoutPanel();
                            tlp_table_inner.RowCount = 3;
                            tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
                            tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
                            tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
                            tlp_table_inner.Name = "tlp_com" + tabledata.Tid;
                            tlp_table_inner.Tag = Convert.ToInt32(CombinedBillType.Normal);
                            tlp_table_inner.BorderStyle = BorderStyle.FixedSingle;
                            tlp_table_inner.Dock = DockStyle.None;
                            tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Normalorder) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                            //tlp_table_inner.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;

                            tlp_table_inner.BackColor = Color.White;

                            if (screenWidth > 1366)
                            {
                                tlp_table_inner.Width = 120;
                                tlp_table_inner.Height = 80;
                                tlp_table_inner.Margin = new Padding(5, 5, 0, 5);
                            }
                            else if (screenWidth >= 1024 && screenWidth <= 1366)
                            {
                                tlp_table_inner.Width = 120;
                                tlp_table_inner.Height = 80;
                                tlp_table_inner.Margin = new Padding(10, 10, 0, 10);
                            }

                            Button btn_select = new Button();
                            btn_select.Name = "btn_select_com" + tabledata.Tid;
                            btn_select.AccessibleDescription = Convert.ToInt32(DashboardTableType.Normalorder) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                            btn_select.Tag = Convert.ToInt32(BillSelect.Unselect);
                            btn_select.FlatStyle = FlatStyle.Flat;
                            btn_select.FlatAppearance.BorderSize = 0;
                            btn_select.BackgroundImage = Properties.Resources.Billaddicon;
                            btn_select.BackgroundImageLayout = ImageLayout.Stretch;
                            btn_select.Dock = DockStyle.Fill;
                            btn_select.Margin = new Padding(20, 0, 20, 0);
                            btn_select.Click += new EventHandler(btn_select_Clicked);

                            // Configure the Label
                            Label lbltablename = new Label();
                            lbltablename.Name = "lb_tablename" + tabledata.Tid;
                            lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Normalorder) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                            lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                            lbltablename.Tag = Convert.ToInt32(CombinedBillType.Normal);
                            lbltablename.Text = tabledata.TableCode;
                            lbltablename.ForeColor = Description_color;
                            lbltablename.BackColor = Color.Transparent;
                            lbltablename.Font = new Font("Arial", 10, FontStyle.Bold);
                            lbltablename.Dock = DockStyle.Fill;

                            // Configure the Label
                            Label netamountlabel = new Label();
                            netamountlabel.Name = "lb_netamount" + tabledata.Tid;
                            netamountlabel.AccessibleDescription = Convert.ToInt32(DashboardTableType.Normalorder) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                            netamountlabel.TextAlign = ContentAlignment.MiddleCenter;
                            netamountlabel.Tag = Convert.ToInt32(CombinedBillType.Normal);
                            netamountlabel.Text = "\u20B9" + Convert.ToString(tabledata.NetAmount);
                            netamountlabel.ForeColor = Netamountcolor;
                            netamountlabel.BackColor = Color.Transparent;
                            netamountlabel.Font = new Font("Arial", 10, FontStyle.Bold);
                            netamountlabel.Dock = DockStyle.Fill;

                            if (Convert.ToInt32(tabledata.BillStatus) == 0)
                                tlp_table_inner.Visible = true;
                            else
                                tlp_table_inner.Visible = false;

                            if (tabledata.BookmasterId > 0)// && tabledata.TableCount > 1//Bokking order hide plus button
                            {
                                btn_select.Visible = false;
                            }
                            else
                            {
                                if (Convert.ToInt32(tabledata.Cbid) > 0)
                                {
                                    tlp_table_inner.BackColor = CombinedBillcolor;
                                    btn_select.Visible = false;
                                }
                                else
                                {
                                    tlp_table_inner.BackColor = NormalBillingColor;
                                    btn_select.Visible = true;
                                }
                            }

                            tlp_table_inner.Controls.Add(btn_select, 0, 0);
                            tlp_table_inner.Controls.Add(lbltablename, 0, 1);
                            tlp_table_inner.Controls.Add(netamountlabel, 0, 2);

                            lbltablename.Click += new EventHandler(CompleteEventHandler);
                            netamountlabel.Click += new EventHandler(CompleteEventHandler);
                            tlp_table_inner.Click += new EventHandler(CompleteEventHandler);

                            if (Convert.ToInt32(tabledata.BookmasterId) > 0)
                            {
                                tlp_table_inner.Tag = Convert.ToInt32(CombinedBillType.BookingBill);
                                lbltablename.Tag = Convert.ToInt32(CombinedBillType.BookingBill);
                                netamountlabel.Tag = Convert.ToInt32(CombinedBillType.BookingBill);

                                lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                                tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                                netamountlabel.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                            }

                            #endregion

                            flp_box_check.Controls.Add(tlp_table_inner);
                            #endregion
                        }

                    }
                    pnl_compelte_bill.Controls.Add(tlp_table_dashboard);
                    pnl_compelte_bill.Dock = DockStyle.Fill;
                    pnl_compelte_bill.Visible = true;


                    CombineBillAutoTimer.Start();
                }
                else
                {                    
                    pnl_empty_msg.BringToFront();
                    pnl_empty_msg.Anchor = AnchorStyles.None;

                    int x = (this.Width - pnl_empty_msg.Width) / 2;
                    int y = (this.Height - pnl_empty_msg.Height) / 2;
                    pnl_empty_msg.Location = new Point(x, y);
                    pnl_empty_msg.Visible = true;

                    //controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
                    //tlp_compl_pre_bill.Controls.Remove(controlToRemove);
                    //tlp_compl_pre_bill.Controls.Add(pnl_empty_msg, 0, 1);
                    //pnl_empty_msg.Dock = DockStyle.Fill;
                    //pnl_empty_msg.Visible = true;
                    btn_prevbill.PerformClick();
                }
                this.ResumeLayout();
                this.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page Billcomplete_Fill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        private void CombineBillAutoTimer_Tick(object sender, EventArgs e)
        {
            BillCombineBill_Update();
        }

        public void BillCombineBill_Update()
        {
            try
            {
                CombineBillAutoTimer.Stop();

                #region old code fetch db to query No use currently use store procedure
                //BllSearch<DmnTableinfo> bllSearch = new BllSearch<DmnTableinfo>();
                //DmnSearch dmnSearch = new DmnSearch();
                //dmnSearch.Select = "ti.* ,c.CategoryName as CategoryName,om.NetAmount ,(select CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1 from tblOrderDetails WHERE status!=0 and AddonStatus in(1,0) and OrderMasterId =ti.OrderMasterId ) BillStatus,cob.Cbid,"+
                //                " (select top (1) bd.BookmasterId from tblbookMaster bm  LEFT JOIN tblBookDetails bd on bm.BookmasterId = bd.BookmasterId where bm.Status = 1 and bd.Status = 1 and SYSDATETIME() >= DATEADD(MINUTE," + -Convert.ToDecimal(FrmLogin.BookingInterwell) + ", BookingDateTime) and MTID = ti.tid and rid=0  order by bm.BookingDateTime desc) BookmasterId"; 
                //dmnSearch.Table = "tblTable_info  ti " +
                //    " LEFT JOIN tblCategory_FTI c on c.Fcid=ti.Fcid " +
                //    " LEFT JOIN tblOrderMaster om on om.OrderMasterId=ti.OrderMasterId and om.Status !=0" +
                //    " LEFT JOIN tblCombinedOrderBill cob on cob.OrderMasterid=ti.OrderMasterId";
                //dmnSearch.WhereNdGroupBy = "where ti.Status=1";
                //dmnSearch.OrderBy = "order by ti.tid";
                //var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                #endregion

                BllTableData<DmncombinedBill> SearchDetails = new BllTableData<DmncombinedBill>();
                DmncombinedBill dmnSearchtabledata = new DmncombinedBill();
                dmnSearchtabledata.BookingInterwell = Convert.ToDecimal(FrmLogin.BookingInterwell);
                var gridSource = SearchDetails.serachreport("prdBillData", dmnSearchtabledata);

                if (gridSource.Count > 0)
                {
                    string flpname = "flp_box_Com_Pickup";
                    FlowLayoutPanel flp_box_check_pk = tlp_table_dashboard.Controls.Find(flpname, true).FirstOrDefault() as FlowLayoutPanel;

                    #region Remove Pickup on complete or cancel Orders

                    var preserveNames = gridSource
                        .Where(order => order.PickupOrderFlag == 1 && order.BillStatus == 0)
                        .Select(order => "tlp_com_pk" + order.pkid)
                        .ToList();

                    foreach (Control control in flp_box_check_pk.Controls.Cast<TableLayoutPanel>().ToArray())
                    {
                        if (control is TableLayoutPanel tlp && !preserveNames.Contains(tlp.Name))
                        {
                            flp_box_check_pk.Controls.Remove(control);
                            control.Dispose();
                        }
                    }
                    #endregion

                    #region Check order status and set visible flase and true
                    int rowIndex_cate = 0;
                    foreach (var tabledata in gridSource)
                    {
                        if (Convert.ToInt32(tabledata.PickupOrderFlag) == 1 && Convert.ToInt32(tabledata.BillStatus) == 0)
                        {
                            #region Pickup Box 

                            string tlpname = "tlp_com_pk" + tabledata.pkid;
                            TableLayoutPanel tlp_box_check_pk = tlp_table_dashboard.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                            if (tlp_box_check_pk == null)
                            {
                                #region pickup bill Box creation 
                                // Configure the TableLayoutPanel
                                TableLayoutPanel tlp_table_inner_pk = new TableLayoutPanel();
                                tlp_table_inner_pk.RowCount = 3;
                                tlp_table_inner_pk.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
                                tlp_table_inner_pk.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
                                tlp_table_inner_pk.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
                                tlp_table_inner_pk.Name = "tlp_com_pk" + tabledata.pkid;
                                tlp_table_inner_pk.Tag = Convert.ToInt32(CombinedBillType.PickupBill);
                                tlp_table_inner_pk.BorderStyle = BorderStyle.FixedSingle;
                                tlp_table_inner_pk.Dock = DockStyle.None;
                                tlp_table_inner_pk.Tag = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                                //tlp_table_inner_pk.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;

                                tlp_table_inner_pk.BackColor = Color.White;

                                if (screenWidth > 1366)
                                {
                                    tlp_table_inner_pk.Width = 120;
                                    tlp_table_inner_pk.Height = 80;
                                    tlp_table_inner_pk.Margin = new Padding(5, 5, 0, 5);
                                }
                                else if (screenWidth >= 1024 && screenWidth <= 1366)
                                {
                                    tlp_table_inner_pk.Width = 120;
                                    tlp_table_inner_pk.Height = 80;
                                    tlp_table_inner_pk.Margin = new Padding(10, 10, 0, 10);
                                }

                                Button btn_select_pk = new Button();
                                btn_select_pk.Name = "btn_select_pk_com" + tabledata.pkid;
                                btn_select_pk.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                                btn_select_pk.Tag = Convert.ToInt32(BillSelect.Unselect);
                                btn_select_pk.FlatStyle = FlatStyle.Flat;
                                btn_select_pk.FlatAppearance.BorderSize = 0;
                                btn_select_pk.BackgroundImage = Properties.Resources.Billaddicon;
                                btn_select_pk.BackgroundImageLayout = ImageLayout.Stretch;
                                btn_select_pk.Dock = DockStyle.Fill;
                                btn_select_pk.Margin = new Padding(20, 0, 20, 0);
                                btn_select_pk.Click += new EventHandler(btn_select_Clicked);

                                // Configure the Label
                                Label lbltablename_pk = new Label();
                                lbltablename_pk.Name = "lb_tablename" + tabledata.pkid;
                                lbltablename_pk.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                                lbltablename_pk.TextAlign = ContentAlignment.MiddleCenter;
                                lbltablename_pk.Tag = Convert.ToInt32(CombinedBillType.PickupBill);
                                lbltablename_pk.Text = Convert.ToString(tabledata.pkid);
                                lbltablename_pk.ForeColor = Description_color;
                                lbltablename_pk.BackColor = Color.Transparent;
                                lbltablename_pk.Font = new Font("Arial", 10, FontStyle.Bold);
                                lbltablename_pk.Dock = DockStyle.Fill;

                                // Configure the Label
                                Label netamountlabel_pk = new Label();
                                netamountlabel_pk.Name = "lb_netamount" + tabledata.pkid;
                                netamountlabel_pk.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                                netamountlabel_pk.TextAlign = ContentAlignment.MiddleCenter;
                                netamountlabel_pk.Tag = Convert.ToInt32(CombinedBillType.PickupBill);
                                netamountlabel_pk.Text = "\u20B9" + Convert.ToString(tabledata.NetAmount);
                                netamountlabel_pk.ForeColor = Netamountcolor;
                                netamountlabel_pk.BackColor = Color.Transparent;
                                netamountlabel_pk.Font = new Font("Arial", 10, FontStyle.Bold);
                                netamountlabel_pk.Dock = DockStyle.Fill;

                                if (Convert.ToInt32(tabledata.BillStatus) == 0)
                                    tlp_table_inner_pk.Visible = true;
                                else
                                    tlp_table_inner_pk.Visible = false;

                                if (tabledata.BookmasterId > 0)// && tabledata.TableCount > 1//Bokking order hide plus button
                                {
                                    btn_select_pk.Visible = false;
                                }
                                else
                                {
                                    if (Convert.ToInt32(tabledata.Cbid) > 0)
                                    {
                                        tlp_table_inner_pk.BackColor = CombinedBillcolor;
                                        btn_select_pk.Visible = false;
                                    }
                                    else
                                    {
                                        tlp_table_inner_pk.BackColor = NormalBillingColor;
                                        btn_select_pk.Visible = true;
                                    }
                                }

                                tlp_table_inner_pk.Controls.Add(btn_select_pk, 0, 0);
                                tlp_table_inner_pk.Controls.Add(lbltablename_pk, 0, 1);
                                tlp_table_inner_pk.Controls.Add(netamountlabel_pk, 0, 2);

                                lbltablename_pk.Click += new EventHandler(CompleteEventHandler);
                                netamountlabel_pk.Click += new EventHandler(CompleteEventHandler);
                                tlp_table_inner_pk.Click += new EventHandler(CompleteEventHandler);


                                if (Convert.ToInt32(tabledata.BookmasterId) > 0)
                                {
                                    lbltablename_pk.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                                    tlp_table_inner_pk.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                                    netamountlabel_pk.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) + "," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                                }
                                flp_box_check_pk.Controls.Add(tlp_table_inner_pk);
                                #endregion
                            }
                            else
                            {
                                string btniaddicon = "btn_select_pk_com" + tabledata.pkid;
                                Button btn_select = tlp_table_dashboard.Controls.Find(btniaddicon, true).FirstOrDefault() as Button;
                                tlp_box_check_pk.BackColor = NormalBillingColor;
                                if (CheckedtableNames.Count == 0)
                                    if (btn_select != null)
                                    {
                                        btn_select.Visible = true;
                                        btn_select.BackgroundImage = Properties.Resources.Billaddicon;
                                        btn_select.Tag = Convert.ToInt32(BillSelect.Unselect);
                                        btn_select.BackgroundImageLayout = ImageLayout.Stretch;
                                    }
                            }
                            #endregion
                        }
                        else if (Convert.ToInt32(tabledata.Tid) > 0)
                        {
                            #region Normal , Combined ,bokking orders
                            string tlpname = "tlp_com" + tabledata.Tid;
                            TableLayoutPanel tlp_box = tlp_table_dashboard.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                            if (tlp_box != null)
                            {
                                if (tabledata.BillStatus == 0)
                                {
                                    string lblname = "lb_netamount" + tabledata.Tid;
                                    string btniaddicon = "btn_select_com" + tabledata.Tid;
                                    Button btn_select = tlp_table_dashboard.Controls.Find(btniaddicon, true).FirstOrDefault() as Button;
                                    Label lblnet = tlp_table_dashboard.Controls.Find(lblname, true).FirstOrDefault() as Label;
                                    if (lblnet != null)
                                        lblnet.Text = "\u20B9" + Convert.ToString(tabledata.NetAmount);

                                    if (tabledata.BookmasterId > 0)//&& tabledata.TableCount > 1 //Bokking order hide plus button
                                    {
                                        btn_select.Visible = false;
                                    }//combined or normal order bill
                                    else
                                    {
                                        if (Convert.ToInt32(tabledata.Cbid) > 0)
                                        {
                                            if (btn_select != null)
                                            {
                                                btn_select.Visible = false;
                                                tlp_box.BackColor = CombinedBillcolor;
                                            }
                                        }
                                        else
                                        {
                                            tlp_box.BackColor = NormalBillingColor;
                                            if (CheckedtableNames.Count == 0)
                                                if (btn_select != null)
                                                {
                                                    btn_select.Visible = true;
                                                    btn_select.BackgroundImage = Properties.Resources.Billaddicon;
                                                    btn_select.Tag = Convert.ToInt32(BillSelect.Unselect);
                                                    btn_select.BackgroundImageLayout = ImageLayout.Stretch;
                                                }
                                            //DmnNameList itemToRemove = CheckedtableNames.FirstOrDefault(item => item.Ordermasterid == omasterid);
                                        }
                                    }
                                    tlp_box.Visible = true;
                                }
                                else
                                    tlp_box.Visible = false;
                            }
                            #endregion
                        }
                    }
                    #endregion
                }

                CombineBillAutoTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page BillCombineBill_Update()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
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
                        Button btn_select = tlp_table_dashboard.Controls.Find(btnname, true).FirstOrDefault() as Button;
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
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
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
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
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
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page btn_select_Clicked", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        public class CategoryInfo
        {
            public int Fcid { get; set; }
            public string CategoryName { get; set; }
        }
        private void CompleteEventHandler(object sender, EventArgs e)
        {
            try
            {
                CombineBillAutoTimer.Stop();

                Control clickedControl = sender as Control;
                if (clickedControl != null)
                {
                    int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                    int screenHeight = Screen.PrimaryScreen.Bounds.Height;

                    //Convert.ToInt32(DashboardTableType.Reverse_Table)+","+Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode) +
                    //"," + Convert.ToInt32(tabledata.BookmasterId) + "," + Convert.ToInt32(tabledata.pkid);
                    string textvalue = clickedControl.AccessibleDescription;
                    //if(txtval)
                    string[] parts = textvalue.Split(',');

                    int Btype = Convert.ToInt32(clickedControl.Tag); //parts.Length > 0 ? Convert.ToInt32(parts[0]) : 0;
                    int OrderMasterId = parts.Length > 1 ? Convert.ToInt32(parts[1]) : 0;
                    int tid = parts.Length > 2 ? Convert.ToInt32(parts[2]) : 0;
                    string tableCode = parts.Length > 3 ? parts[3] : "";
                    int BmasterId = parts.Length > 4 ? Convert.ToInt32(parts[4]) : 0;
                    int pkid = parts.Length > 4 ? Convert.ToInt32(parts[5]) : 0;

                    CheckCombinedOrNot_Tid(tid);

                    #region Combined Bill Order
                    if (CheckedtableNames.Count > 1)
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
                                _instanceFrmOrder.Show();
                                _instanceFrmOrder.BringToFront();
                            }
                        }
                        else
                        {
                            FrmOrder _instanceFrmOrder = new FrmOrder(0, Convert.ToInt32(DashboardTableType.CombinedOrderBill), 0, CheckedtableNames, Convert.ToInt32(PageType.Billpage), 0, Convert.ToInt32(SourcepageType.Billingpage));
                            _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                            _instanceFrmOrder.Opacity = 0;
                            _instanceFrmOrder.Visible = false;
                            _instanceFrmOrder.Show();
                            _instanceFrmOrder.BringToFront();
                        }

                    }
                    #endregion

                    else
                    {
                        #region Normal Order 
                        if (Btype == Convert.ToInt32(CombinedBillType.Normal))
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
                                    _instanceFrmOrder.Show();
                                    _instanceFrmOrder.BringToFront();
                                }
                            }
                            else
                            {
                                int tagValue;
                                if (clickedControl.Tag != null)
                                {
                                    FrmOrder _instanceFrmOrder = new FrmOrder(tid, Convert.ToInt32(DashboardTableType.Normalorder), 0, CheckedtableNames, Convert.ToInt32(PageType.Billpage), 0, Convert.ToInt32(SourcepageType.Billingpage));
                                    _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                    _instanceFrmOrder.Opacity = 0;
                                    _instanceFrmOrder.Visible = false;
                                    _instanceFrmOrder.Show();
                                    _instanceFrmOrder.BringToFront();
                                }
                            }
                        }
                        #endregion

                        #region Booking Order 
                        else if (Btype == Convert.ToInt32(CombinedBillType.BookingBill))
                        {
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
                                        _instanceFrmOrder.Show();
                                        _instanceFrmOrder.BringToFront();
                                    }
                                }
                                else
                                {
                                    FrmOrder _instanceFrmOrder = new FrmOrder(tid, Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), BmasterId, CheckedtableNames, Convert.ToInt32(PageType.Billpage), 0, Convert.ToInt32(SourcepageType.Billingpage));
                                    _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                    _instanceFrmOrder.Opacity = 0;
                                    _instanceFrmOrder.Visible = false;
                                    _instanceFrmOrder.Show();
                                    _instanceFrmOrder.BringToFront();

                                }
                            }
                        }
                        #endregion

                        #region Pick UP Order
                        else if (Btype == Convert.ToInt32(CombinedBillType.PickupBill))
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
                                    FrmOrder _instanceFrmOrder = new FrmOrder(tid, Convert.ToInt32(DashboardTableType.PickupOrder), 0, CheckedtableNames, Convert.ToInt32(PageType.Billpage), OrderMasterId, Convert.ToInt32(SourcepageType.Pickuppage));
                                    _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                    _instanceFrmOrder.Opacity = 0;
                                    _instanceFrmOrder.Visible = false;
                                    _instanceFrmOrder.ShowDialog();
                                    _instanceFrmOrder.BringToFront();
                                }
                            }
                        }

                        #endregion
                    }

                }
                CombineBillAutoTimer.Start();
            }

            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page CompleteEventHandler", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }

        }
        private void tlp_category_com_MouseHover(object sender, EventArgs e)
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
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page tlp_category_com_MouseHover()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }

        }
        private void tlp_category_com_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                TableLayoutPanel tlpcate = sender as TableLayoutPanel;
                string tlpName = "tlp_category_com_" + tlpcate.Tag;
                // Find the TableLayoutPanel control by name
                TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
                if (tlp != null)
                {
                    tlp.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page tlp_category_com_MouseLeave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }

        }
        private void btn_backCompleorder_Click(object sender, EventArgs e)
        {
            try
            {
                tlp_tblcount_namelist.Visible = true;
                btn_backCompleorder.Visible = false;
                btn_prevbill.Visible = true;

                this.Controls.Add(pnl_complebill_prevbill);
                pnl_complebill_prevbill.Dock = DockStyle.Fill;
                Control controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
                tlp_compl_pre_bill.Controls.Remove(controlToRemove);
                tlp_compl_pre_bill.Controls.Add(pnl_compelte_bill, 0, 1);
                tlp_compl_pre_bill.Dock = DockStyle.Fill;

                pnl_empty_msg.Visible = false;
                pnl_complebill_prevbill.Visible = true;

                CombineBillAutoTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
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
                _objSearch.ColumnHeaders = "ID,DateTime,Invoice No,Table Code,TotalQuantity,NetAmount,RcvAmnt,BalAmnt,PaymentMode";
                _objSearch.Select = "sm.SalesMasterId ID,sm.CreatedDateTime Field1,sm.EntryNo Field2,ti.TableCode Field3,sm.TotalQuantity Field4,sm.NetAmount Field5,sm.RcvAmnt Field6,sm.BalAmnt Field7,g.Caption Field8";
                _objSearch.Table = "tblSalesMaster sm LEFT JOIN tblTable_info ti on ti.tid=sm.Tid LEFT JOIN tblGeneralOptions g ON g.GTypeSubId=sm.PaymentMode";
                _objSearch.WhereNdGroupBy = "where sm.status=1 and  g.GTypeId=2";
                _objSearch.OrderBy = "order by sm.SalesMasterId DESC";
                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource.Count == 0)
                {
                    MessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))
                    {
                        var dialogResult = search.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                        {
                            returnValue = Convert.ToString(search._retValue);
                        }
                    }
                    if (returnValue != "")
                    {
                        int oid = Convert.ToInt32(returnValue);
                        printBill(oid);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page btn_serach_prev_bill_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
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

                Control controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
                tlp_compl_pre_bill.Controls.Remove(controlToRemove);
                tlp_compl_pre_bill.Controls.Add(pnl_previous_bill, 0, 1);
                tlp_compl_pre_bill.Dock = DockStyle.Fill;
                pnl_previous_bill.Dock = DockStyle.Fill;
                pnl_previous_bill.Visible = true;
                pnl_empty_msg.Visible = false;

                btn_backCompleorder.Visible = true;
                btn_prevbill.Visible = false;

                #region Fill complete or cancelled order in Datagrid
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "Top 20 sm.CreatedDateTime Field1,sm.EntryNo Field2,ti.TableCode Field3,sm.TotalQuantity Field4,sm.Status Field5,sm.SalesMasterId Field6 ,sm.NetAmount Field7,sm.RcvAmnt Field8,sm.BalAmnt Field9,sm.PaymentMode Field10,sm.Tid Field11,sm.CombinedOrderBill_TYPE Field12,sm.OrderMasterid Field13,sm.OrderType Field14";
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

                        dgvorderview.Rows[rowIndex].Cells[tablecodeno].Value = gridSource[i].Field3;

                        #region multiple table code case reservation 
                        if (Convert.ToInt32(gridSource[i].Field11) == 0)
                        {
                            FillTablecode_Comb(rowIndex, Convert.ToInt32(gridSource[i].Field6), Convert.ToInt32(gridSource[i].Field13), Convert.ToInt32(gridSource[i].Field12));
                        }
                        else
                        {
                            DataGridViewComboBoxCell dropDown = (DataGridViewComboBoxCell)dgvorderview.Rows[rowIndex].Cells[tablecodeno];
                            List<ComboBoxItem> comboBoxItems = new List<ComboBoxItem>();
                            ComboBoxItem comboBoxItem;
                            if (Convert.ToInt32(gridSource[i].Field14) == Convert.ToInt32(OrderType.PickupOrder))
                            {
                                comboBoxItem = new ComboBoxItem(0, "PICKUP ORDER");
                            }
                            else
                            {
                                comboBoxItem = new ComboBoxItem(Convert.ToInt32(gridSource[i].Field11), gridSource[i].Field3);
                            }
                            comboBoxItems.Add(comboBoxItem);
                            dropDown.DataSource = comboBoxItems;
                            dropDown.ValueMember = "Id";
                            dropDown.DisplayMember = "Value";
                            dropDown.Value = comboBoxItems[0].Id;
                            dropDown.ReadOnly = true;
                        }
                        #endregion


                        dgvorderview.Rows[rowIndex].Cells[totalqtyno].Value = gridSource[i].Field4;
                        dgvorderview.Rows[rowIndex].Cells[ordermasteridno].Value = gridSource[i].Field6;
                        dgvorderview.Rows[rowIndex].Cells[netamountno].Value = gridSource[i].Field7;
                        dgvorderview.Rows[rowIndex].Cells[recamountno].Value = gridSource[i].Field8;
                        dgvorderview.Rows[rowIndex].Cells[balamountno].Value = gridSource[i].Field9;
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
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "PART";
                                break;
                            case 12:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "SPLIT";
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

                    controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
                    tlp_compl_pre_bill.Controls.Remove(controlToRemove);
                    tlp_compl_pre_bill.Controls.Add(pnl_empty_msg, 0, 1);
                    pnl_empty_msg.Dock = DockStyle.Fill;
                    pnl_empty_msg.Visible = true;
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page btn_prevbill_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        private void dgvorderview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == actionno)
                {
                    int oid = Convert.ToInt32(dgvorderview.Rows[e.RowIndex].Cells[ordermasteridno].Value);
                    printBill(oid);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page dgvorderview_CellDoubleClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        public void printBill(int oid)
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
                    if (Convert.ToInt32(result[0].Field1) != 12)
                    {
                        pnl_split.Visible = false;
                        FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(oid, Convert.ToInt32(result[0].Field1), 0, Convert.ToInt32(BillType.NormalBill));
                        frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                        frmSalesInvoice.ShowDialog();
                    }
                    else
                    {
                        obj.Select = "Amount Field1,SplitType Field2,SalemasterId Field3,PayId Field4 ";
                        obj.Table = "tblpayment";
                        obj.WhereNdGroupBy = " where status=1 and SaleMasterId=" + oid;
                        var paymentresult = bllpayments.GetSearchData("prdSearch", obj);
                        if (paymentresult.Count > 0)
                        {
                            dgv_split.Rows.Clear();
                            dgv_split.Rows.Add(paymentresult.Count);
                            int x = dgv_split.RowCount;
                            for (int i = 0; i < paymentresult.Count; i++)
                            {
                                dgv_split.Rows[i].Cells[0].Value = i + 1;
                                dgv_split.Rows[i].Cells[1].Value = paymentresult[i].Field1;
                                DataGridViewImageCell cell = (DataGridViewImageCell)dgv_split.Rows[i].Cells[2];
                                cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                cell.Value = Properties.Resources.eye;
                                dgv_split.Rows[i].Cells[3].Value = paymentresult[i].Field3;
                                dgv_split.Rows[i].Cells[4].Value = paymentresult[i].Field4;
                            }
                        }
                        pnl_split.Location = new Point(this.ClientSize.Width / 2 - pnl_split.Size.Width / 2, this.ClientSize.Height / 2 - pnl_split.Size.Height / 2);
                        pnl_split.Visible = true;
                        pnl_split.BringToFront();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page printBill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        public void FillTablecode_Comb(int rowindex, int salemasterid, int ordermasterid, int ordertype)
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                DataGridViewComboBoxCell dropDown = (DataGridViewComboBoxCell)dgvorderview.Rows[rowindex].Cells[tablecodeno];
                if (ordertype == 0)
                {
                    objSearch.Select = "bd.MTID id,bd.Name value";
                    objSearch.Table = "tblOrderMaster om " +
                        "left join tblbookMaster bm on om.BookmasterId=bm.BookmasterId " +
                        "left join tblBookDetails bd on bd.BookmasterId=bm.BookmasterId and bd.Rid=0 ";
                    objSearch.WhereNdGroupBy = "where om.OrderMasterId=" + ordermasterid;
                    objSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                    gridSource[0].Id = 0;
                    gridSource[0].Value = "Show Table";
                    dropDown.DataSource = gridSource;
                    dropDown.ValueMember = "Id";
                    dropDown.DisplayMember = "Value";

                    dropDown.Value = gridSource[0].Id;
                    dropDown.ReadOnly = false;
                    dgvorderview.Rows[rowindex].Cells[tablecodeno].ReadOnly = false;
                }
                else
                {
                    objSearch.Select = "com.tid id,case when PickupOrderFlag=1 then 'PICK NO '+ CONVERT(varchar, PkId) else ti.TableCode end value";
                    objSearch.Table = "tblCombinedOrderBill com left join tblTable_info ti on ti.tid=com.Tid";
                    objSearch.WhereNdGroupBy = "where SalesMasterId =" + salemasterid;
                    objSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                    gridSource[0].Id = 0;
                    gridSource[0].Value = "Show Table";
                    dropDown.DataSource = gridSource;
                    dropDown.ValueMember = "Id";
                    dropDown.DisplayMember = "Value";

                    dropDown.Value = gridSource[0].Id;
                    dropDown.ReadOnly = false;
                    dgvorderview.Rows[rowindex].Cells[tablecodeno].ReadOnly = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page FillTablecode_Comb()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        public class ComboBoxItem
        {
            public int Id { get; set; }
            public string Value { get; set; }

            public ComboBoxItem(int id, string value)
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



        private void btnclose_drawer_Click_1(object sender, EventArgs e)
        {
            pnl_Drawer.Visible = false;
        }


        public bool MandatoryCheckDrawer()
        {
            try
            {

                if (flagDrawerF && string.IsNullOrWhiteSpace(txtstaffname.Text))
                {
                    MessageBox.Show("Please select Employee", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtstaffname.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtinitialamt.Text) || Convert.ToDecimal(txtinitialamt.Text) == 0)
                {
                    MessageBox.Show("Please Enter Initial Amount", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                //clsg.LogException("Admin page MandatoryCheckDrawer()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.Admin));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btndrawersave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtdrawerid.Text))
                    txtdrawerid.Text = "0";
                if (string.IsNullOrWhiteSpace(txtinitialamt.Text))
                    txtinitialamt.Text = "0";
                if (MandatoryCheckDrawer())
                {
                    Dmndrawer dmndrawer = new Dmndrawer();
                    Blldrawer blldrawer = new Blldrawer();
                    dmndrawer.Id = Convert.ToInt32(txtdrawerid.Text);
                    dmndrawer.DName = System.Environment.MachineName;
                    dmndrawer.InitilAmount = Convert.ToDecimal(txtinitialamt.Text);
                    dmndrawer.EmployeeId = FrmLogin.UserId;
                    dmndrawer.LoanAmount = 0;
                    dmndrawer.Drops = 0;
                    dmndrawer.BranchId = FrmDashboard.BranchId;
                    dmndrawer.CreatedBy = FrmDashboard.UserId;
                    dmndrawer.Status = 1;
                    var InsertStatus = blldrawer.AddEditDatadrawer("prdInsertDrawer", dmndrawer); 

                    if (InsertStatus != null)
                    {
                        if (txtdrawerid.Text == null || txtdrawerid.Text == "0")
                        {
                            DrawerID = InsertStatus.Id;
                            MessageBox.Show("Drawer Assign Completed", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtinitialamt.ReadOnly = true;
                            ShowDrawerEmployee();
                            tlp_outbtn.Visible = true;
                            if (!CombineBillAutoTimer.Enabled)
                            {
                                tlp_previous_bill.Visible = true;
                                tlp_compl_pre_bill.Visible = true;
                                Billcomplete_Fill();
                            }
                                
                        }
                        else
                        {
                            DrawerID = Convert.ToInt32(txtdrawerid.Text);
                            MessageBox.Show("Update Sucessfull", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        pnl_Drawer.Visible = false;
                    }
                    else
                        MessageBox.Show("Assign Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (DrawerID > 0)
                    {
                        txtdrawerid.Text = Convert.ToString(DrawerID);
                    }
                }
            }
            catch (Exception ex)
            {
                //clsg.LogException("Admin page btndrawersave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.Admin));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            pnl_Drawer.Visible = false;
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
        public void StaffPopup()
        {
            try
            {
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,EmployeeCode,EmployeeName,DesignationName,Mobile";
                _objSearch.Select = "emp.EmployeeId as Id,emp.EmployeeCode as Field1,emp.EmployeeName as Field2,d.DesignationName as Field3,emp.Mobile as Field4";
                _objSearch.Table = "tblEmployee  emp Left join tblDesignation d on d.DesignationId = emp.DesignationId ";
                _objSearch.WhereNdGroupBy = "WHERE isnull(emp.Status,0) = 1 ";
                _objSearch.OrderBy = " ORDER BY emp.EmployeeName ASC";
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))

                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);
                    }
                }
                if (returnValue.ToString() != "")
                {
                    int eid = Convert.ToInt32(returnValue);
                    if (DrawerCheckBranch(eid))
                    {
                        MessageBox.Show("Drawer Already Assigned on " + Drawersystemname + "System", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }

                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "COUNT(*) OVER() AS Field1,emp.EmployeeName as Field2,d.DesignationName as Field3";
                    objSearch.Table = "tblEmployee  emp Left join tblDesignation d on d.DesignationId = emp.DesignationId";
                    objSearch.WhereNdGroupBy = "Where emp.status=1 and emp.EmployeeId = " + eid;
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        txtstaffname.Text = Convert.ToString(itemSource[0].Field2);
                        lbl_desigtnation.Text = Convert.ToString(itemSource[0].Field3);
                        txtempoyeId.Text = Convert.ToString(eid);
                        lbl_staffname_tip.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ///clsg.LogException("Admin page  StaffPopup()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.Admin));
                ////MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

       
        public bool Checkcardstatus(string cardno)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "COUNT(*) OVER() AS Field1,emp.EmployeeName as Field2,d.DesignationName as Field3,emp.EmployeeId as Field4";
                objSearch.Table = "tblEmployee  emp Left join tblDesignation d on d.DesignationId = emp.DesignationId";
                objSearch.WhereNdGroupBy = "Where emp.status=1 and emp.ICardNo = '" + cardno + "'";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    txtstaffname.Text = Convert.ToString(itemSource[0].Field2);
                    lbl_desigtnation.Text = Convert.ToString(itemSource[0].Field3);
                    txtempoyeId.Text = Convert.ToString(itemSource[0].Field4);
                    lbl_staffname_tip.Visible = false;
                    return true;
                }
                else
                {
                    MessageBox.Show(this, "Invalid card number. Please try again.", "Card Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }
            catch (Exception ex)
            {
                //clsg.LogException("Admin page  Checkcardstatus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.Admin));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }



        }
        public string RemoveSpecialCharacters(string str)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (char c in str)
                {
                    if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                    {
                        sb.Append(c);
                    }
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                //clsg.LogException("Admin page  RemoveSpecialCharacters()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.Admin));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

        }
        public void FetchDetails(string cardno)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "emp.EmployeeId as Field1";
                objSearch.Table = "tblEmployee  emp Left join tblDesignation d on d.DesignationId = emp.DesignationId";
                objSearch.WhereNdGroupBy = "Where emp.status=1 and emp.ICardNo = '" + cardno + "'";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    if (DrawerCheckBranch(Convert.ToInt32(itemSource[0].Field1)))
                    {
                        MessageBox.Show("Drawer Already Assigned on " + Drawersystemname + "System", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    else
                    {
                        if (Checkcardstatus(cardno) == false)
                        {
                            MessageBox.Show("Staff Details Not Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //clsg.LogException("Admin page FetchDetails()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.Admin));
            }
        }
        private void txtstaffname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                    StaffPopup();
            }
            catch (Exception ex)
            {
                //clsg.LogException("Admin page txtstaffname_KeyDown", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.Admin));
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul(101);
                if (str1 != "Card not in contact")
                {
                    timer1.Stop();
                    if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Invalid Card")
                        str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi(101);
                    str1 = RemoveSpecialCharacters(str1.Trim());
                    if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Cardnotincontact" || str1 == "InvalidCard")
                    {
                        MessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        FetchDetails(str1);
                    }
                }

            }
            catch (Exception ex)
            {
                // clsg.LogException("Admin page  timer1_Tick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.Admin));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtstaffname_Enter(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void txtstaffname_Leave(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void yout_Click(object sender, EventArgs e)
        {
            try
            {
                AssignDrawerCheck();
                using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.Yout), Drawerassginstaffid))
                {
                    if (FrmDashboard.XYDalvalue)
                        frmzout.ShowDialog();
                    else
                        frmzout.PrintAll();
                }
                MessageBox.Show("Yout Print Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("POSDashboard page yout_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }

        private void zout_Click(object sender, EventArgs e)
        {
            try
            {
                if (DrawerID > 0)
                {
                    var confirmResult = MessageBox.Show(
                     "This will print the Z-Out report and sign you out.\n\n" +
                     "Make sure all transactions are completed.\n\n" +
                     "Do you want to proceed and end your shift?",
                     "Confirm Z-Out and Sign Out",
                     MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question
                 );

                    if (confirmResult != DialogResult.Yes)
                        return;

                    using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.Zout), Drawerassginstaffid))
                    {
                        if (FrmDashboard.XYDalvalue)
                            frmzout.ShowDialog();
                        else
                            frmzout.PrintAll();
                    }

                    MessageBox.Show("Zout Print Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Dmndrawer dmndrawer = new Dmndrawer();
                    Blldrawer blldrawer = new Blldrawer();

                    if (flagDrawerF)
                    {
                        dmndrawer.Id = Drawerassginid;
                    }
                    else
                    {
                        if (DrawerID > 0)
                            dmndrawer.Id = DrawerID;
                    }

                    dmndrawer.BranchId = FrmDashboard.BranchId;
                    dmndrawer.CreatedBy = FrmDashboard.UserId;
                    dmndrawer.Status = 0;
                    var InsertStatus = blldrawer.AddEditDatadrawer("prdInsertDrawer", dmndrawer);
                    if (InsertStatus.Id > 0)
                    {

                        DrawerID = 0;
                        tlp_outbtn.Visible = false;
                        tlp_previous_bill.Visible = false;
                        tlp_compl_pre_bill.Visible = false;
                        FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                        FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
                    }

                }
                else
                {
                    MessageBox.Show("Zout Cannot be Completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AssignDrawerCheck();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("POSDashboard page zout_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }

        private void normal_rpt_Click(object sender, EventArgs e)
        {
            try
            {
                AssignDrawerCheck();
                using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.DailyCollection), Drawerassginstaffid))
                {
                    if (FrmDashboard.XYDalvalue)
                        frmzout.ShowDialog();
                    else
                        frmzout.PrintAll();
                }
                MessageBox.Show("Normal Report Print Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("POSDashboard page normal_rpt_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }

        private void detailed_rpt_Click(object sender, EventArgs e)
        {
            using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.DetailedCollection), 0))
            {
                frmzout.PrintDetailsCollection();
            }
            MessageBox.Show("Details Collection Report Printing Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (e.ColumnIndex == 2)
                {
                    FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(Convert.ToInt32(dgv_split.Rows[e.RowIndex].Cells[3].Value), Convert.ToInt32(PaymentType.SPLIT), Convert.ToInt32(dgv_split.Rows[e.RowIndex].Cells[4].Value), Convert.ToInt32(BillType.NormalBill));
                    frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                    frmSalesInvoice.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
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

        #endregion

    }
}

