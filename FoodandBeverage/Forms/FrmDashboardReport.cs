using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FoodandBeverage.Forms
{
    public partial class FrmDashboardReport : Form
    {
        public static int BranchId = 0;
        public static int UserId = 0;
        public static int UserType = 0;
        public static int Allaccess = 0;
        public int Reportid = 0;
        public FrmDashboardReport()
        {
            InitializeComponent();
            UserId = Convert.ToInt32(FrmLogin.UserId);
            BranchId = Convert.ToInt32(FrmLogin.BranchId);
            UserType = Convert.ToInt32(FrmLogin.UserAccountTypeId);
        }

        #region Declaration
        ClsGeneral clsgen = new ClsGeneral();
        private List<DmnGridFields> Expencedata;
        public bool flagexit = false;
        private List<DmnGridFields> Result_table_menu; 
        public int companystartYear = 0;

        public DateTime startDate;
        public DateTime endDate;

        private int currentPage = 1;
        private int totalPages = 0;
        private List<DmnGridFields> searchData_Activeorder = new List<DmnGridFields>();
        private List<DmnGridFields> searchData_CancelOrder = new List<DmnGridFields>();
        private List<DmnGridFields> searchData_ActiveFood = new List<DmnGridFields>();
        private List<DmnGridFields> searchData_Activereservation = new List<DmnGridFields>();
        private List<DmnGridFields> searchData_ActivePromotion_offer = new List<DmnGridFields>();
        private List<DmnGridFields> searchData_ActivePromotion_combo = new List<DmnGridFields>();
        int pageSize = 5;

        public bool RefreshFlag=false;

        Color baseColor = Color.DarkGray; 
        Color pressedColor = Color.LightGray;
        Color unpressedColor = Color.FromArgb(225, 242, 255); 
        Color activeColor = Color.FromArgb(255, 255, 255);

        List<DmnGridFields> resultslowfastdata = new List<DmnGridFields>();
        #endregion

        #region Grid Declaration
        //active dine grid
        public static int adslno = 0;
        public static int adordermasteridno = 1;
        public static int addateno = 2;
        public static int adtotalqtyno = 3;

        //Cancel order  grid
        public static int cslno = 0;
        public static int cordermasteridno = 1;
        public static int cdateno = 2;
        public static int cresdesno = 3;

        //active Promotion grid
        public static int apslno = 0;
        public static int apoffernameno = 1;
        public static int apstartdateno = 2;
        public static int apenddateno = 3;
        public static int apdiscountperno = 4;
        public static int appriceno = 5;

        //active Food grid
        public static int fslno = 0;
        public static int ffoodnameno = 1;
        public static int fcurrentqtyno = 2;
        public static int fcategoryno = 3;
        public static int fpriceno = 4;

        //upcoming reservation grid
        public static int rslno = 0;
        public static int rdatetimeno = 1;
        public static int rcutsnameno = 2;
        public static int rphoneno = 3;
        public static int rgenderno = 4;
        public static int raddressno = 5;
        public static int rtablecode = 6;
        public static int rfooddetailsno = 7;

        #endregion
        private void FrmDashboardReport_Load(object sender, EventArgs e)
        {
            try
            {
                startDate = DateTime.Today;
                endDate = DateTime.Today.AddHours(23);

                lbl_fastslow.Text = "DATE : " + Convert.ToString(startDate.Date);
                lbl_discount.Text = "DATE : " + Convert.ToString(startDate.Date);
                lbl_paymentdetails.Text = "DATE : " + Convert.ToString(startDate.Date);
                lblexpence.Text = "DATE : " + Convert.ToString(startDate.Date);
                lbl_res_dine_sales.Text = "DATE : " + Convert.ToString(startDate.Date);

                if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                {
                    FrmDashboard instanceFrmDashboard = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                    instanceFrmDashboard.StartTimerAndRefreshLabel();
                }
                RefreshFlag = true;
                pnl_dashboard.SuspendLayout();
                Fill_combo_finyear();
                SetupChartTwoBar();
                SetupChart_Revenue();
                SetupCharttotaldiscount();
                SetupChartExpence();

                if(!FrmLogin.TestUser)
                {
                    RunMethodInSeparateThread(Filldashboard);
                    RunMethodInSeparateThread(FillChartTwoBarChart);
                    RunMethodInSeparateThread(FillbarRrevenuChart);
                    RunMethodInSeparateThread(FillChartExpence);
                    RunMethodInSeparateThread(FillCharttotaldiscountChart);
                    RunMethodInSeparateThread(FillBoxdetailed);
                    RunMethodInSeparateThread(LoadSlowfastData);
                    RunMethodInSeparateThread(Fillpaymentdetailed);
                    tcslow_move.SelectedIndex = 1;
                }       

                this.Focus();
                pnl_dashboard.ResumeLayout();

                #region Box Details Grid 
                //dine active
                dgv_activedine.BorderStyle = BorderStyle.None;
                dgv_activedine.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgv_activedine.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgv_activedine.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgv_activedine.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgv_activedine.BackgroundColor = Color.White;

                dgv_activedine.EnableHeadersVisualStyles = false;
                dgv_activedine.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgv_activedine.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgv_activedine.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                //dgv_cancelorders

                dgv_cancelorders.BorderStyle = BorderStyle.None;
                dgv_cancelorders.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgv_cancelorders.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgv_cancelorders.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgv_cancelorders.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgv_cancelorders.BackgroundColor = Color.White;

                dgv_cancelorders.EnableHeadersVisualStyles = false;
                dgv_cancelorders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgv_cancelorders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgv_cancelorders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                // Styling for dgv_activefood
                dgv_activefood.BorderStyle = BorderStyle.None;
                dgv_activefood.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgv_activefood.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgv_activefood.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgv_activefood.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgv_activefood.BackgroundColor = Color.White;

                dgv_activefood.EnableHeadersVisualStyles = false;
                dgv_activefood.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgv_activefood.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgv_activefood.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                // Styling for dgv_reservation
                dgv_reservation.BorderStyle = BorderStyle.None;
                dgv_reservation.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgv_reservation.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgv_reservation.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgv_reservation.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgv_reservation.BackgroundColor = Color.White;

                dgv_reservation.EnableHeadersVisualStyles = false;
                dgv_reservation.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgv_reservation.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgv_reservation.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                // Styling for dgv_promotion
                dgv_promotion.BorderStyle = BorderStyle.None;
                dgv_promotion.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgv_promotion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgv_promotion.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgv_promotion.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgv_promotion.BackgroundColor = Color.White;

                dgv_promotion.EnableHeadersVisualStyles = false;
                dgv_promotion.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgv_promotion.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgv_promotion.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                rdproductoffer.Checked = true;

                SetColumnPadding(tlp_pagination, 2, 5, new Padding(20, 10, 20, 10));

                #endregion
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("DashboardChart FrmDashboardReport_Load", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        #region Refresh Chart 
        public void RefreshCharts()
        {
            try
            {
                if (!FrmLogin.TestUser)
                {
                    pnl_dashboard.SuspendLayout();
                    if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                    {
                        FrmDashboard instanceFrmDashboard = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                        instanceFrmDashboard.StartTimerAndRefreshLabel();
                    }
                    RefreshFlag = true;
                    ShowLoadingdGif();
                    RunMethodInSeparateThread(LoadSlowfastData);
                    RunMethodInSeparateThread(Filldashboard);
                    RunMethodInSeparateThread(FillChartTwoBarChart);
                    RunMethodInSeparateThread(FillCharttotaldiscountChart);
                    RunMethodInSeparateThread(FillChartExpence);
                    RunMethodInSeparateThread(Fillpaymentdetailed);

                    tcslow_move.SelectedIndex = 1;
                    this.Focus();
                    pnl_dashboard.ResumeLayout();
                }
            }
            catch (Exception ex)
            {

                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);      
                clsgen.LogException("DashboardChart RefreshCharts", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }
        #endregion 

        private static void RunMethodInSeparateThread(Action action)
        {
            var thread = new Thread(new ThreadStart(action));
            thread.Start();
        }          

        #region  BOX DETAILS ACTIVE,CANCELLED,PROMOTION,FOOD ITEM
        private void dgv_reservation_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        public void FillTablecode_Reservation(int bmid, int rowno)
        {
            try
            {
                #region Fill Food Item 
                var filteredResult = Result_table_menu
                    .Where(item => Convert.ToInt32(item.Field6) == bmid && Convert.ToInt32(item.Field8) == 1)
                    .Select(item => new DmnGridFields
                    {
                        Field1 = item.Field7, // Name
                        Field2 = item.Field9, // MTID
                    })
                    .ToList();

                DataGridViewComboBoxCell dropDown = (DataGridViewComboBoxCell)dgv_reservation.Rows[rowno].Cells[rfooddetailsno];
                dropDown.DataSource = filteredResult;
                dropDown.ValueMember = "Field2";
                dropDown.DisplayMember = "Field1";

                DmnGridFields newItem = new DmnGridFields
                {
                    Field1 = "Show Items",
                    Field2 = "0",
                };
                filteredResult.Add(newItem);
                dropDown.Value = filteredResult.Last().Field2;
                #endregion

                #region Fill Combo Table code
                var filteredResulttablecode = Result_table_menu
                    .Where(item => Convert.ToInt32(item.Field6) == bmid && Convert.ToInt32(item.Field8) == 0)
                    .Select(item => new DmnGridFields
                    {
                        Field1 = item.Field7,
                        Field2 = item.Field9,
                    })
                    .ToList();

                DataGridViewComboBoxCell dropDown_table = (DataGridViewComboBoxCell)dgv_reservation.Rows[rowno].Cells[rtablecode];
                dropDown_table.DataSource = filteredResulttablecode;
                dropDown_table.ValueMember = "Field2";
                dropDown_table.DisplayMember = "Field1";

                DmnGridFields newItemtable = new DmnGridFields
                {
                    Field1 = "Show Table Code",
                    Field2 = "0",
                };
                filteredResulttablecode.Add(newItemtable);
                dropDown_table.Value = filteredResulttablecode.Last().Field2;
                #endregion
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart FillTablecode_Reservation", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        private void btn_close_boxdetails_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_boxdetails_dash.Visible = false;

                dgv_activefood.Visible = false;
                dgv_activedine.Visible = false;
                dgv_cancelorders.Visible = false;
                dgv_reservation.Visible = false;
                tlp_offer_type_main.Visible = false;

                dgv_activefood.Rows.Clear();
                dgv_activedine.Rows.Clear();
                dgv_cancelorders.Rows.Clear();
                dgv_reservation.Rows.Clear();
                dgv_promotion.Rows.Clear();

                tlp_pagination.Visible = false;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart btn_close_boxdetails_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }


        private void rdproductoffer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdproductoffer.Checked)
                {
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "OfferName Field1,OfferstartDatetime Field2,OfferEndDatetime Field3,Discountper Field4,FinalPrice Field5";
                    dmnSearch.Table = "tblOfferMaster";
                    dmnSearch.WhereNdGroupBy = "where  OfferType = 0 AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME() and status = 1";
                    dgv_promotion.Columns[appriceno].Visible = false;
                    dmnSearch.OrderBy = "order by  Omid desc";
                    searchData_ActivePromotion_offer = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    totalPages = (int)Math.Ceiling((double)searchData_ActivePromotion_offer.Count / pageSize);

                    currentPage = 1;
                    lblpageno.Text = "Page No : " + Convert.ToString(currentPage) + " / " + totalPages;

                    DisplayDataForCurrentPage();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart rdproductoffer_CheckedChanged", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        private void rdcombooffer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdcombooffer.Checked)
                {
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch dmnSearch = new DmnSearch();

                    dmnSearch.Select = "OfferName Field1,OfferstartDatetime Field2,OfferEndDatetime Field3,Discountper Field4,FinalPrice Field5";
                    dmnSearch.Table = "tblOfferMaster";
                    dmnSearch.WhereNdGroupBy = "where  OfferType = 1 AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME() and status = 1";
                    dgv_promotion.Columns[appriceno].Visible = true;

                    dmnSearch.OrderBy = "order by  Omid desc";
                    searchData_ActivePromotion_combo = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    totalPages = (int)Math.Ceiling((double)searchData_ActivePromotion_combo.Count / pageSize);

                    currentPage = 1;
                    lblpageno.Text = "Page No : " + Convert.ToString(currentPage) + " / " + totalPages;
                    DisplayDataForCurrentPage();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart rdcombooffer_CheckedChanged", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }


        public void FillBoxdetailed()
        {
            try
            {
                DateTime currentdate = DateTime.Now;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = " count(*) Field1,0 Field2,0 Field3,0 Field4,0 Field5,1 Field6 ";
                dmnSearch.Table = " tblTable_info where Status=1  AND OrderStatus>0 or OrderMasterId>0 " +
                                  " union select 0 Field1,count(*) Field2,0 Field3,0 Field4,0 Field5,2 Field6 from tblOrderMaster where Status = 0 AND CONVERT(DATE, CreatedDateTime) = CONVERT(DATE,'" + currentdate.ToString("yyyy-MM-dd") + "')" +
                                  " union select 0 Field1,0 Field2,count(*) Field3,0 Field4,0 Field5,3 Field6 from tblMenu where Status = 1" +
                                  " union select 0 Field1,0 Field2,0 Field3,count(*) Field4,0 Field5,4 Field6 from tblOfferMaster where  OfferType in(1,0) and status=1 AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME()" +
                                  " union select 0 Field1,0 Field2,0 Field3,0 Field4,count(*) Field5,5 Field6 from tblbookMaster where  Status = 1 AND BookingDateTime >= SYSDATETIME()";
                dmnSearch.WhereNdGroupBy = " ";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                var columnSums = new
                {
                    Field1 = gridSource.Sum(item => Convert.ToDecimal(item.Field1)),
                    Field2 = gridSource.Sum(item => Convert.ToDecimal(item.Field2)),
                    Field3 = gridSource.Sum(item => Convert.ToDecimal(item.Field3)),
                    Field4 = gridSource.Sum(item => Convert.ToDecimal(item.Field4)),
                    Field5 = gridSource.Sum(item => Convert.ToDecimal(item.Field5))
                };

                if (gridSource.Count > 0)
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblactive.Text = Convert.ToString(columnSums.Field1);
                            lblcancelled.Text = Convert.ToString(columnSums.Field2);
                            lblfoodmenu.Text = Convert.ToString(columnSums.Field3);
                            lblactivepromo.Text = Convert.ToString(columnSums.Field4);
                            lblupreserva.Text = Convert.ToString(columnSums.Field5);
                        }));
                    }
                }
                else
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblactive.Text = "0.00";
                            lblcancelled.Text = "0.00";
                            lblfoodmenu.Text = "0.00";
                            lblactivepromo.Text = "0.00";
                            lblupreserva.Text = "0.00";
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart FillBoxdetailed", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        #region Pagination

        private void ResetOtherButtonsBackColor(Button currentButton)
        {
            foreach (Control control in tlp_pagination.Controls)
            {
                if (control is Button button && button != currentButton)
                {
                    button.BackColor = baseColor;
                }
            }
        }

        private void btnprevious_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    DisplayDataForCurrentPage();
                    lblpageno.Text = "Page No : " + Convert.ToString(currentPage) + " / " + totalPages;

                    btnfrist.BackColor = pressedColor;
                    ResetOtherButtonsBackColor(btnfrist);
                }
                UpdatePageButtonsText();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart btnprevious_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            try
            {

                if (currentPage < totalPages)
                {
                    currentPage++;
                    DisplayDataForCurrentPage();
                    UpdatePageButtonsText();
                    lblpageno.Text = "Page No : " + Convert.ToString(currentPage) + " / " + totalPages;

                    btnfrist.BackColor = pressedColor;
                    ResetOtherButtonsBackColor(btnfrist);

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart btnnext_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }
        private void btnfrist_Click(object sender, EventArgs e)
        {
            try
            {
                currentPage = Convert.ToInt32(btnfrist.Text);
                lblpageno.Text = "Page No : " + Convert.ToString(currentPage) + " / " + totalPages;
                DisplayDataForCurrentPage();
                btnfrist.BackColor = pressedColor;
                ResetOtherButtonsBackColor(btnfrist);
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart btnfrist_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        private void btn_two_Click(object sender, EventArgs e)
        {
            try
            {
                currentPage = Convert.ToInt32(btn_two.Text);
                lblpageno.Text = "Page No : " + Convert.ToString(currentPage) + " / " + totalPages;
                DisplayDataForCurrentPage();
                btn_two.BackColor = pressedColor;
                ResetOtherButtonsBackColor(btn_two);
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart btn_two_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        private void btn_three_Click(object sender, EventArgs e)
        {
            try
            {
                currentPage = Convert.ToInt32(btn_three.Text);
                lblpageno.Text = "Page No : " + Convert.ToString(currentPage) + " / " + totalPages;
                DisplayDataForCurrentPage();
                btn_three.BackColor = pressedColor;
                ResetOtherButtonsBackColor(btn_three);
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart btn_three_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        private void btn_four_Click(object sender, EventArgs e)
        {
            try
            {
                currentPage = Convert.ToInt32(btn_four.Text);
                lblpageno.Text = "Page No : " + Convert.ToString(currentPage) + " / " + totalPages;
                DisplayDataForCurrentPage();
                btn_four.BackColor = pressedColor;
                ResetOtherButtonsBackColor(btn_four);
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart btn_four_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }



        private void UpdatePageButtonsText()
        {
            try
            {
               if (currentPage <= totalPages)
                {
                    int startPage = currentPage;
                    btnfrist.Text = (startPage).ToString();
                    btn_two.Text = (startPage + 1).ToString();
                    btn_three.Text = (startPage + 2).ToString();
                    btn_four.Text = (startPage + 3).ToString();

                    btnfrist.Visible = startPage <= totalPages;
                    btn_two.Visible = (startPage + 1) <= totalPages;
                    btn_three.Visible = (startPage + 2) <= totalPages;
                    btn_four.Visible = (startPage + 3) <= totalPages;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart UpdatePageButtonsText", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }   
        private void UpdateData()
        {
            try
            {
                //searchData_Activeorder,searchData_CancelOrder,searchData_ActivePromotion,searchData_ActiveFood,searchData_Activereservation

                currentPage = 1;
                btnfrist.Text = "1";
                btn_two.Text = "2";
                btn_three.Text = "3";
                btn_four.Text = "4";

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                if (dgv_activefood.Visible)
                {
                    searchData_ActiveFood.Clear();
                    dmnSearch.Select = "Name Field1,m.Quantity Field2,c.CategoryName Field3,p.Price Field4 ";
                    dmnSearch.Table = "tblMenu m " +
                                      "left join tblCategory_FTI c on c.Fcid=m.Fcid " +
                                      "left join tblProduct p on p.ProductId=m.ProductId";
                    dmnSearch.WhereNdGroupBy = "where m.Status = 1 ";
                    dmnSearch.OrderBy = "order by  m.Name";
                    var resultdate = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    searchData_ActiveFood = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    totalPages = (int)Math.Ceiling((double)searchData_ActiveFood.Count / pageSize);
                }

                else if (dgv_activedine.Visible)
                {
                    searchData_Activeorder.Clear();
                    dmnSearch.Select = "ti.OrderMasterId Field1 ,CreatedDateTime Field2,TotalQuantity Field3";
                    dmnSearch.Table = "tblTable_info ti left join tblOrderMaster om on om.OrderMasterId =ti.OrderMasterId ";
                    dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderMasterId >0";
                    dmnSearch.OrderBy = "order by  ti.OrderMasterId Desc";
                    var resultdate = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    searchData_Activeorder = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    totalPages = (int)Math.Ceiling((double)searchData_Activeorder.Count / pageSize);
                }

                else if (dgv_reservation.Visible)
                {
                    searchData_Activereservation.Clear();
                    dmnSearch.Select = "BookingDateTime Field1,bm.cusName Field2,Mobile Field3,gender Field4,Address Field5,bm.BookmasterId Field6,bd.Name Field7,bd.Rid Field8,bd.MTID Field9";
                    dmnSearch.Table = "tblbookMaster bm left join tblBookDetails bd on bm.BookmasterId=bd.BookmasterId";
                    dmnSearch.WhereNdGroupBy = "where bm.Status = 1 AND  BookingDateTime >= SYSDATETIME()";
                    dmnSearch.OrderBy = "order by bm.BookmasterId DESC ";
                    Result_table_menu = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    var uniqueResults = Result_table_menu
                        .GroupBy(item => item.Field6)
                        .Select(group => group.First())
                        .ToList();
                    searchData_Activereservation = uniqueResults;
                    totalPages = (int)Math.Ceiling((double)searchData_Activereservation.Count / pageSize);
                }

                else if (dgv_cancelorders.Visible)
                {
                    searchData_CancelOrder.Clear();
                    dmnSearch.Select = "DISTINCT om.OrderMasterId AS Field1,om.CreatedDateTime AS Field2,ko.OrderCancelNotes AS Field3";
                    dmnSearch.Table = " tblOrderMaster om LEFT JOIN tblkitchenOrder ko ON ko.OrderMasterId = om.OrderMasterId AND ko.Status = 0 ";
                    dmnSearch.WhereNdGroupBy = "where om.Status=0  AND CONVERT(DATE, om.CreatedDateTime) = CONVERT(DATE, '" + dtp_dashboard.Value.ToString("yyyy-MM-dd") + "')";
                    dmnSearch.OrderBy = "order by  om.OrderMasterId Desc";
                    searchData_CancelOrder = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    totalPages = (int)Math.Ceiling((double)searchData_CancelOrder.Count / pageSize);
                }

                else if (tlp_offer_type_main.Visible)
                {
                    dmnSearch.Select = "OfferName Field1,OfferstartDatetime Field2,OfferEndDatetime Field3,Discountper Field4,FinalPrice Field5";
                    dmnSearch.Table = "tblOfferMaster";

                    searchData_ActivePromotion_offer.Clear();
                    dgv_promotion.Columns[appriceno].Visible = false;
                    dmnSearch.WhereNdGroupBy = "where  OfferType = 0 AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME() and status = 1";
                    dmnSearch.OrderBy = "order by  Omid desc";
                    searchData_ActivePromotion_offer = bllSearch.GetSearchData("prdSearch", dmnSearch);

                    searchData_ActivePromotion_combo.Clear();
                    dgv_promotion.Columns[appriceno].Visible = true;
                    dmnSearch.WhereNdGroupBy = "where  OfferType = 1 AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME() and status = 1";
                    dmnSearch.OrderBy = "order by  Omid desc";
                    searchData_ActivePromotion_combo = bllSearch.GetSearchData("prdSearch", dmnSearch);

                    if (rdproductoffer.Checked)
                        totalPages = (int)Math.Ceiling((double)searchData_ActivePromotion_offer.Count / pageSize);
                    
                    if (rdcombooffer.Checked)                  
                        totalPages = (int)Math.Ceiling((double)searchData_ActivePromotion_combo.Count / pageSize);                   

                }

                DisplayDataForCurrentPage();
                lblpageno.Text = "Page No : " + Convert.ToString(currentPage) + " / " + totalPages;
                if (currentPage <= totalPages)
                {
                    int startPage = currentPage;
                    btnfrist.Visible = startPage <= totalPages;
                    btn_two.Visible = (startPage + 1) <= totalPages;
                    btn_three.Visible = (startPage + 2) <= totalPages;
                    btn_four.Visible = (startPage + 3) <= totalPages;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart UpdateData", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }
        private void DisplayDataForCurrentPage()
        {
            try
            {
                int startIndex = 0;
                int endIndex = 0;
                lblpageno.Text = "Page No : " + Convert.ToString(currentPage) + " / " + totalPages;
                tlp_pagination.Visible = true;

                if (dgv_activedine.Visible)
                {
                    if (searchData_Activeorder.Count > 0)
                    {
                        startIndex = (currentPage - 1) * pageSize;
                        endIndex = Math.Min(startIndex + pageSize, searchData_Activeorder.Count);

                        lblnodata.Visible = false;
                        dgv_activedine.Visible = true;
                        Control controlToRemove = tlp_boxdetails_sub.GetControlFromPosition(1, 1);
                        tlp_boxdetails_sub.Controls.Remove(controlToRemove);

                        tlp_boxdetails_sub.Controls.Add(dgv_activedine, 1, 1);
                        dgv_activedine.Dock = DockStyle.Fill;

                        dgv_activedine.Rows.Clear();
                        dgv_activedine.Rows.Add(endIndex - startIndex);

                        for (int i = startIndex, j = 0; i < endIndex; i++, j++)
                        {
                            dgv_activedine.Rows[j].Cells[adslno].Value = i + 1;
                            dgv_activedine.Rows[j].Cells[adordermasteridno].Value = searchData_Activeorder[i].Field1;
                            dgv_activedine.Rows[j].Cells[addateno].Value = searchData_Activeorder[i].Field2;
                            dgv_activedine.Rows[j].Cells[adtotalqtyno].Value = searchData_Activeorder[i].Field3;
                        }

                    }
                    else
                    {
                        Control controlToRemove = tlp_boxdetails_sub.GetControlFromPosition(1, 1);
                        tlp_boxdetails_sub.Controls.Remove(controlToRemove);

                        tlp_boxdetails_sub.Controls.Add(lblnodata, 1, 1);
                        lblnodata.Dock = DockStyle.Fill;
                        lblnodata.Visible = true;

                        dgv_activefood.Visible = false;
                        dgv_activedine.Visible = false;
                        dgv_cancelorders.Visible = false;
                        dgv_reservation.Visible = false;
                        tlp_offer_type_main.Visible = false;
                        tlp_pagination.Visible = false;
                    }
                }
                else if (dgv_cancelorders.Visible)
                {
                    if (searchData_CancelOrder.Count > 0)
                    {
                        startIndex = (currentPage - 1) * pageSize;
                        endIndex = Math.Min(startIndex + pageSize, searchData_CancelOrder.Count);
                        lblnodata.Visible = false;
                        dgv_cancelorders.Visible = true;
                        Control controlToRemove = tlp_boxdetails_sub.GetControlFromPosition(1, 1);
                        tlp_boxdetails_sub.Controls.Remove(controlToRemove);

                        tlp_boxdetails_sub.Controls.Add(dgv_cancelorders, 1, 1);
                        dgv_cancelorders.Dock = DockStyle.Fill;

                        dgv_cancelorders.Rows.Clear();
                        dgv_cancelorders.Rows.Add(endIndex - startIndex);

                        for (int i = startIndex, j = 0; i < endIndex; i++, j++)
                        {
                            dgv_cancelorders.Rows[j].Cells[cslno].Value = i + 1;
                            dgv_cancelorders.Rows[j].Cells[cordermasteridno].Value = searchData_CancelOrder[i].Field1;
                            dgv_cancelorders.Rows[j].Cells[cdateno].Value = searchData_CancelOrder[i].Field2;
                            dgv_cancelorders.Rows[j].Cells[cresdesno].Value = searchData_CancelOrder[i].Field3;
                        }

                    }
                    else
                    {
                        Control controlToRemove = tlp_boxdetails_sub.GetControlFromPosition(1, 1);
                        tlp_boxdetails_sub.Controls.Remove(controlToRemove);

                        tlp_boxdetails_sub.Controls.Add(lblnodata, 1, 1);
                        lblnodata.Dock = DockStyle.Fill;
                        lblnodata.Visible = true;

                        dgv_activefood.Visible = false;
                        dgv_activedine.Visible = false;
                        dgv_cancelorders.Visible = false;
                        dgv_reservation.Visible = false;
                        tlp_offer_type_main.Visible = false;
                        tlp_pagination.Visible = false;
                    }

                }
                else if (dgv_activefood.Visible)
                {
                    if (searchData_ActiveFood.Count > 0)
                    {
                        startIndex = (currentPage - 1) * pageSize;
                        endIndex = Math.Min(startIndex + pageSize, searchData_ActiveFood.Count);

                        lblnodata.Visible = false;
                        dgv_activefood.Visible = true;
                        Control controlToRemove = tlp_boxdetails_sub.GetControlFromPosition(1, 1);
                        tlp_boxdetails_sub.Controls.Remove(controlToRemove);

                        tlp_boxdetails_sub.Controls.Add(dgv_activefood, 1, 1);
                        dgv_activefood.Dock = DockStyle.Fill;

                        dgv_activefood.Rows.Clear();
                        dgv_activefood.Rows.Add(endIndex - startIndex);

                        for (int i = startIndex, j = 0; i < endIndex; i++, j++)
                        {
                            dgv_activefood.Rows[j].Cells[fslno].Value = i + 1;
                            dgv_activefood.Rows[j].Cells[ffoodnameno].Value = searchData_ActiveFood[i].Field1;
                            dgv_activefood.Rows[j].Cells[fcurrentqtyno].Value = searchData_ActiveFood[i].Field2;
                            dgv_activefood.Rows[j].Cells[fcategoryno].Value = searchData_ActiveFood[i].Field3;
                            dgv_activefood.Rows[j].Cells[fpriceno].Value = searchData_ActiveFood[i].Field4;
                        }

                    }
                    else
                    {
                        Control controlToRemove = tlp_boxdetails_sub.GetControlFromPosition(1, 1);
                        tlp_boxdetails_sub.Controls.Remove(controlToRemove);

                        tlp_boxdetails_sub.Controls.Add(lblnodata, 1, 1);
                        lblnodata.Dock = DockStyle.Fill;
                        lblnodata.Visible = true;

                        dgv_activefood.Visible = false;
                        dgv_activedine.Visible = false;
                        dgv_cancelorders.Visible = false;
                        dgv_reservation.Visible = false;
                        tlp_offer_type_main.Visible = false;
                        tlp_pagination.Visible = false;
                    }

                }
                else if (dgv_reservation.Visible)
                {
                    if (searchData_Activereservation.Count > 0)
                    {
                        startIndex = (currentPage - 1) * pageSize;
                        endIndex = Math.Min(startIndex + pageSize, searchData_Activereservation.Count);

                        lblnodata.Visible = false;
                        dgv_reservation.Visible = true;
                        Control controlToRemove = tlp_boxdetails_sub.GetControlFromPosition(1, 1);
                        tlp_boxdetails_sub.Controls.Remove(controlToRemove);

                        tlp_boxdetails_sub.Controls.Add(dgv_reservation, 1, 1);
                        dgv_reservation.Dock = DockStyle.Fill;

                        dgv_reservation.Rows.Clear();
                        dgv_reservation.Rows.Add(endIndex - startIndex);

                        for (int i = startIndex, j = 0; i < endIndex; i++, j++)
                        {
                            dgv_reservation.Rows[j].Cells[rslno].Value = i + 1;
                            dgv_reservation.Rows[j].Cells[rdatetimeno].Value = searchData_Activereservation[i].Field1;
                            dgv_reservation.Rows[j].Cells[rcutsnameno].Value = searchData_Activereservation[i].Field2;
                            dgv_reservation.Rows[j].Cells[rphoneno].Value = searchData_Activereservation[i].Field3;

                            if (Convert.ToInt32(searchData_Activereservation[i].Field4) == 1)
                                dgv_reservation.Rows[i].Cells[rgenderno].Value = "male";
                            else if (Convert.ToInt32(searchData_Activereservation[i].Field4) == 2)
                                dgv_reservation.Rows[i].Cells[rgenderno].Value = "female";
                            else
                                dgv_reservation.Rows[i].Cells[rgenderno].Value = "Others";

                            dgv_reservation.Rows[i].Cells[raddressno].Value = searchData_Activereservation[i].Field5;
                            FillTablecode_Reservation(Convert.ToInt32(searchData_Activereservation[i].Field6), i);
                        }

                    }
                    else
                    {
                        Control controlToRemove = tlp_boxdetails_sub.GetControlFromPosition(1, 1);
                        tlp_boxdetails_sub.Controls.Remove(controlToRemove);

                        tlp_boxdetails_sub.Controls.Add(lblnodata, 1, 1);
                        lblnodata.Dock = DockStyle.Fill;
                        lblnodata.Visible = true;

                        dgv_activefood.Visible = false;
                        dgv_activedine.Visible = false;
                        dgv_cancelorders.Visible = false;
                        dgv_reservation.Visible = false;
                        tlp_offer_type_main.Visible = false;
                        tlp_pagination.Visible = false;
                    }

                }
                else if (tlp_offer_type_main.Visible)
                {
                    if (rdproductoffer.Checked)
                    {
                        if (searchData_ActivePromotion_offer.Count > 0)
                        {
                            Control controlToRemove = tlp_offer_type_main.GetControlFromPosition(1, 1);
                            tlp_offer_type_main.Controls.Remove(controlToRemove);

                            tlp_offer_type_main.Controls.Add(dgv_promotion, 1, 1);
                            dgv_promotion.Dock = DockStyle.Fill;
                            lblnodata_offer.Visible = false;
                            dgv_promotion.Visible = true;

                            startIndex = (currentPage - 1) * pageSize;
                            endIndex = Math.Min(startIndex + pageSize, searchData_ActivePromotion_offer.Count);

                            lblnodata.Visible = false;
                            tlp_offer_type_main.Visible = true;
                            controlToRemove = tlp_boxdetails_sub.GetControlFromPosition(1, 1);
                            tlp_boxdetails_sub.Controls.Remove(controlToRemove);

                            tlp_boxdetails_sub.Controls.Add(tlp_offer_type_main, 1, 1);
                            tlp_offer_type_main.Dock = DockStyle.Fill;

                            dgv_promotion.Rows.Clear();
                            dgv_promotion.Rows.Add(endIndex - startIndex);

                            for (int i = startIndex, j = 0; i < endIndex; i++, j++)
                            {
                                dgv_promotion.Rows[j].Cells[apslno].Value = i + 1;
                                dgv_promotion.Rows[j].Cells[apoffernameno].Value = searchData_ActivePromotion_offer[i].Field1;
                                dgv_promotion.Rows[j].Cells[apstartdateno].Value = searchData_ActivePromotion_offer[i].Field2;
                                dgv_promotion.Rows[j].Cells[apenddateno].Value = searchData_ActivePromotion_offer[i].Field3;
                                dgv_promotion.Rows[j].Cells[apdiscountperno].Value = searchData_ActivePromotion_offer[i].Field4;
                                dgv_promotion.Rows[j].Cells[appriceno].Value = searchData_ActivePromotion_offer[i].Field5;
                            }   
                        }
                        //else
                        //{
                        //    Control controlToRemove = tlp_offer_type_main.GetControlFromPosition(1, 1);
                        //    tlp_offer_type_main.Controls.Remove(controlToRemove);

                        //    tlp_offer_type_main.Controls.Add(lblnodata_offer, 1, 1);
                        //    lblnodata_offer.Dock = DockStyle.Fill;
                        //    lblnodata_offer.Visible = true;
                        //    dgv_promotion.Visible = false;

                        //    ////dgv_activefood.Visible = false;
                        //    ////dgv_activedine.Visible = false;
                        //    ////dgv_cancelorders.Visible = false;
                        //    ////dgv_reservation.Visible = false;
                        //    ////tlp_offer_type_main.Visible = false;
                        //    tlp_pagination.Visible = false;
                        //}
                    }
                    else if (rdcombooffer.Checked)
                    {
                        if (searchData_ActivePromotion_combo.Count > 0)
                        {
                            Control controlToRemove = tlp_offer_type_main.GetControlFromPosition(1, 1);
                            tlp_offer_type_main.Controls.Remove(controlToRemove);

                            tlp_offer_type_main.Controls.Add(dgv_promotion, 1, 1);
                            dgv_promotion.Dock = DockStyle.Fill;
                            lblnodata_offer.Visible = false;
                            dgv_promotion.Visible = true;

                            startIndex = (currentPage - 1) * pageSize;
                            endIndex = Math.Min(startIndex + pageSize, searchData_ActivePromotion_combo.Count);

                            lblnodata.Visible = false;
                            tlp_offer_type_main.Visible = true;
                             controlToRemove = tlp_boxdetails_sub.GetControlFromPosition(1, 1);
                            tlp_boxdetails_sub.Controls.Remove(controlToRemove);

                            tlp_boxdetails_sub.Controls.Add(tlp_offer_type_main, 1, 1);
                            tlp_offer_type_main.Dock = DockStyle.Fill;

                            dgv_promotion.Rows.Clear();
                            dgv_promotion.Rows.Add(endIndex - startIndex);

                            for (int i = startIndex, j = 0; i < endIndex; i++, j++)
                            {
                                dgv_promotion.Rows[j].Cells[apslno].Value = i + 1;
                                dgv_promotion.Rows[j].Cells[apoffernameno].Value = searchData_ActivePromotion_combo[i].Field1;
                                dgv_promotion.Rows[j].Cells[apstartdateno].Value = searchData_ActivePromotion_combo[i].Field2;
                                dgv_promotion.Rows[j].Cells[apenddateno].Value = searchData_ActivePromotion_combo[i].Field3;
                                dgv_promotion.Rows[j].Cells[apdiscountperno].Value = searchData_ActivePromotion_combo[i].Field4;
                                dgv_promotion.Rows[j].Cells[appriceno].Value = searchData_ActivePromotion_combo[i].Field5;
                            }

                        }                        
                        //else
                        //{
                        //    Control controlToRemove = tlp_offer_type_main.GetControlFromPosition(1, 1);
                        //    tlp_offer_type_main.Controls.Remove(controlToRemove);

                        //    tlp_offer_type_main.Controls.Add(lblnodata_offer, 1, 1);
                        //    lblnodata_offer.Dock = DockStyle.Fill;
                        //    lblnodata_offer.Visible = true;
                        //    dgv_promotion.Visible = false;
                        //    tlp_pagination.Visible = false;
                        //}

                    }

                    if (searchData_ActivePromotion_combo.Count == 0 && searchData_ActivePromotion_offer.Count == 0)
                    {
                        Control controlToRemove = tlp_offer_type_main.GetControlFromPosition(1, 1);
                        tlp_offer_type_main.Controls.Remove(controlToRemove);

                        tlp_offer_type_main.Controls.Add(lblnodata_offer, 1, 1);
                        lblnodata_offer.Dock = DockStyle.Fill;
                        lblnodata_offer.Visible = true;
                        dgv_promotion.Visible = false;
                        tlp_pagination.Visible = false;
                    }
                    else
                    {
                        Control controlToRemove = tlp_offer_type_main.GetControlFromPosition(1, 1);
                        tlp_offer_type_main.Controls.Remove(controlToRemove);

                        tlp_offer_type_main.Controls.Add(dgv_promotion, 1, 1);
                        dgv_promotion.Dock = DockStyle.Fill;
                        lblnodata_offer.Visible = false;
                        dgv_promotion.Visible = true;

                        lblnodata.Visible = false;
                        tlp_offer_type_main.Visible = true;
                        controlToRemove = tlp_boxdetails_sub.GetControlFromPosition(1, 1);
                        tlp_boxdetails_sub.Controls.Remove(controlToRemove);

                        tlp_boxdetails_sub.Controls.Add(tlp_offer_type_main, 1, 1);
                        tlp_offer_type_main.Dock = DockStyle.Fill;

                        if (searchData_ActivePromotion_offer.Count > 0)
                            rdproductoffer.Checked = true;
                        else if (searchData_ActivePromotion_combo.Count > 0)
                            rdcombooffer.Checked = true;

                    }
                }
               // UpdatePageButtonsText();
            }
            catch(Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart DisplayDataForCurrentPage()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        private void SetColumnPadding(TableLayoutPanel tlp, int columnStart, int columnEnd, Padding padding)
        {
            for (int col = columnStart; col <= columnEnd; col++)
            {
                Control control = tlp.GetControlFromPosition(col, 0); 
                if (control != null)
                {
                    control.Margin = padding;
                }
            }
        }

        #endregion

        #endregion

        #region PAYMENT DETAILED
        public void Fillpaymentdetailed()
        {
            try
            {

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "  ISNULL([CASH],0.00) Field1, ISNULL([CREDIT CARD],0.00) Field2, ISNULL([UPI],0.00) Field3,ISNULL([DEBIT CARD],0.00) Field4,ISNULL([BANKING ONLINE],0.00) Field5, ISNULL([OTHERS (Loyalty)],0.00) Field6,ISNULL([PART],0.00) Field7, ISNULL([SPLIT],0.00) Field8    FROM ( SELECT sm.netamount,g.Caption ";
                dmnSearch.Table = " tblSalesMaster sm LEFT JOIN tblGeneralOptions g ON g.GTypeSubId = sm.PaymentMode and GTypeId=2  " +
                    "where  sm.Status=1 AND  CONVERT(DATE, sm.EntryDate) BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "'  ) AS SourceTable" +
                                    " PIVOT( SUM(netamount) FOR Caption IN([CASH], [CREDIT CARD],[UPI], [DEBIT CARD], [BANKING ONLINE], [OTHERS (Loyalty)], [PART], [SPLIT]) " +
                                    ") AS PivotTable";
                dmnSearch.WhereNdGroupBy = " ";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            ibcash.Text = "\u20B9" + Convert.ToString(gridSource[0].Field1);
                            ibcreditcard.Text = "\u20B9" + Convert.ToString(gridSource[0].Field2);
                            ibupi.Text = "\u20B9" + Convert.ToString(gridSource[0].Field3);
                            ibrupay.Text = "\u20B9" + Convert.ToString(gridSource[0].Field4);
                            ibnetbanking.Text = "\u20B9" + Convert.ToString(gridSource[0].Field5);
                            ibothers.Text = "\u20B9" + Convert.ToString(gridSource[0].Field6);
                            ibpart.Text = "\u20B9" + Convert.ToString(gridSource[0].Field7);
                            ibsplit.Text = "\u20B9" + Convert.ToString(gridSource[0].Field8);
                            decimal sumtotal = Convert.ToDecimal(gridSource[0].Field1) + Convert.ToDecimal(gridSource[0].Field2) + Convert.ToDecimal(gridSource[0].Field3) +
                                               Convert.ToDecimal(gridSource[0].Field4) + Convert.ToDecimal(gridSource[0].Field5) + Convert.ToDecimal(gridSource[0].Field6) +
                                               Convert.ToDecimal(gridSource[0].Field7) + Convert.ToDecimal(gridSource[0].Field8);
                            ibtotalsumpayment.Text = "\u20B9" + Convert.ToString(sumtotal);
                        }));
                    }
                }
                else
                {
                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() =>
                            {
                                ibcash.Text = "0.00";
                                ibcreditcard.Text = "0.00";
                                ibupi.Text = "0.00";
                                ibrupay.Text = "0.00";
                                ibnetbanking.Text = "0.00";
                                ibothers.Text = "0.00";
                                ibpart.Text = "0.00";
                                ibsplit.Text = "0.00";
                                ibtotalsumpayment.Text = "0.00";
                            }));
                        }
                }
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                        {
                            FrmDashboard instanceFrmDashboard = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                            instanceFrmDashboard.time_syn_lal.Stop();
                            instanceFrmDashboard.StopTimerAndRefreshLabel();
                            instanceFrmDashboard.lblsync.Visible = true;
                        }
                    }));
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart Fillpaymentdetailed()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }
        #endregion

        #region SALEDETAILS 
        public void Filldashboard()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "1 Field3,COUNT(*) AS Field1,isnull(SUM(netamount),0) AS Field2 from tblSalesMaster  where Status=1 and CONVERT(DATE,EntryDate) BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "'" +
                " union select 2 Field3,SUM(CASE WHEN CONVERT(DATE, sm.EntryDate) BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "' THEN 1 ELSE 0 END) AS Field1," +
                " isnull(SUM(CASE WHEN CONVERT(DATE, sm.EntryDate) BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "' THEN netamount ELSE 0 END),0) AS Field2" +
                " from tblSalesMaster sm where sm.Rev_Status=0 and sm.Status = 1" +
                " union select 3 Field3 , isnull( SUM(CASE WHEN CONVERT(DATE, sm.EntryDate) BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "' THEN 1 ELSE 0 END),0) AS Field1 ," +
                " isnull(SUM(CASE WHEN CONVERT(DATE, sm.EntryDate) BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "' THEN sm.netamount ELSE 0 END),0) AS Field2";
                dmnSearch.Table = " tblSalesMaster sm left join tblOrderMaster om on om.OrderMasterId = sm.OrderMasterid ";
                dmnSearch.WhereNdGroupBy = "where sm.Rev_Status=1 and sm.Status = 1 ";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (gridSource.Count > 0)
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            ibsaletotal.Text = "\u20B9" + Convert.ToString(gridSource[0].Field2);//sale
                            if (Convert.ToDecimal(gridSource[0].Field2) == 0)
                                lbltotalsaleno.Text = "0 Orders";
                            else
                                lbltotalsaleno.Text = Convert.ToString(gridSource[0].Field1) + " Orders";

                            ibdinetotal.Text = "\u20B9" + Convert.ToString(gridSource[1].Field2);//dine
                            if (Convert.ToDecimal(gridSource[1].Field2) == 0)
                                lbldinetotalno.Text = "0 Orders";
                            else
                                lbldinetotalno.Text = Convert.ToString(gridSource[1].Field1) + " Orders";

                            ibreservatiototal.Text = "\u20B9" + Convert.ToString(gridSource[2].Field2);//reservation
                            if (Convert.ToDecimal(gridSource[2].Field2) == 0)
                                lblresno.Text = "0 Orders";
                            else
                                lblresno.Text = Convert.ToString(gridSource[2].Field1) + " Orders";
                        }));
                    }
                    else
                    {
                        ibsaletotal.Text = "\u20B9" + Convert.ToString(gridSource[0].Field2);//sale
                        if (Convert.ToDecimal(gridSource[0].Field2) == 0)
                            lbltotalsaleno.Text = "0 Orders";
                        else
                            lbltotalsaleno.Text = Convert.ToString(gridSource[0].Field1) + " Orders";

                        ibdinetotal.Text = "\u20B9" + Convert.ToString(gridSource[1].Field2);//dine
                        if (Convert.ToDecimal(gridSource[1].Field2) == 0)
                            lbldinetotalno.Text = "0 Orders";
                        else
                            lbldinetotalno.Text = Convert.ToString(gridSource[1].Field1) + " Orders";

                        ibreservatiototal.Text = "\u20B9" + Convert.ToString(gridSource[2].Field2);//reservation
                        if (Convert.ToDecimal(gridSource[2].Field2) == 0)
                            lblresno.Text = "0 Orders";
                        else
                            lblresno.Text = Convert.ToString(gridSource[2].Field1) + " Orders";
                    }

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart Filldashboard()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }
        #endregion

        #region TWO BAR CHART SALE AND RESVERTION TIME PEROD 24HRS
        public void FillChartTwoBarChart()
        {
            try
            {
                DateTime C_startDate = DateTime.Now;
                DateTime C_endDate = DateTime.Now;
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        if (Cmbserachtype.SelectedIndex == Convert.ToInt32(DMY.MONTH) || Cmbserachtype.SelectedIndex == Convert.ToInt32(DMY.YEAR))
                        {
                            C_startDate = DateTime.Now;
                            C_endDate = DateTime.Now;
                            lbl_res_dine_sales.Text = "DATE : " + Convert.ToString(C_startDate.Date);
                        }
                        else
                        {
                            C_startDate = startDate;
                            C_endDate = endDate;
                        }
                    }));
                }

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "0 as Field13, SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 00 AND 4 THEN netamount ELSE 0 END) AS Field1, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 4 AND 8 THEN netamount ELSE 0 END) AS Field2, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 8 AND 12 THEN netamount ELSE 0 END) AS Field3, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 12 AND 16 THEN netamount ELSE 0 END) AS Field4, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 16 AND 20 THEN netamount ELSE 0 END) AS Field5, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 20 AND 24 THEN netamount ELSE 0 END) AS Field6, " +
                                   " COUNT(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 00 AND 4 THEN 1 END) AS Field7," +
                                   " COUNT(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 4 AND 8 THEN 1 END) AS Field8," +
                                    " COUNT(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 8 AND 12 THEN 1 END) AS Field9," +
                                    " COUNT(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 12 AND 16 THEN 1 END) AS Field10," +
                                    " COUNT(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 16 AND 20 THEN 1 END) AS Field11," +
                                    " COUNT(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 20 AND 24 THEN 1 END) AS Field12" +
                                    " FROM tblSalesMaster WHERE Status = 1 and Rev_Status=0 AND   CONVERT(DATE, CreatedDateTime) BETWEEN '" + C_startDate.ToString("yyyy-MM-dd") + "' AND '" + C_endDate.ToString("yyyy-MM-dd") + "'"+
                                    "union select  1 as Field13,SUM(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 00 AND 4 THEN netamount ELSE 0 END) AS Field1," +
                                    "SUM(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 4 AND 8 THEN netamount ELSE 0 END) AS Field2," +
                                    "SUM(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 8 AND 12 THEN netamount ELSE 0 END) AS Field3," +
                                    "SUM(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 12 AND 16 THEN netamount ELSE 0 END) AS Field4," +
                                    "SUM(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 16 AND 20 THEN netamount ELSE 0 END) AS Field5," +
                                    "SUM(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 20 AND 24 THEN netamount ELSE 0 END) AS Field6," +
                                    "COUNT(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 00 AND 4 THEN 1 END) AS Field7," +
                                    "COUNT(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 4 AND 8 THEN 1 END) AS Field8," +
                                    "COUNT(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 8 AND 12 THEN 1 END) AS Field9," +
                                    "COUNT(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 12 AND 16 THEN 1 END) AS Field10," +
                                    "COUNT(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 16 AND 20 THEN 1 END) AS Field11," +
                                    "COUNT(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 20 AND 24 THEN 1 END) AS Field12";
                dmnSearch.Table = "tblSalesMaster sm  ";
                dmnSearch.WhereNdGroupBy = " where sm.Rev_Status=1 AND sm.Status=1 AND CONVERT(DATE, sm.CreatedDateTime) BETWEEN '" + C_startDate.ToString("yyyy-MM-dd") + "' AND '" + C_endDate.ToString("yyyy-MM-dd") + "'";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            chart_res_dine_sales.Series["s_dine"].Points.Clear();
                            chart_res_dine_sales.Series["s_reservation"].Points.Clear();

                            chart_res_dine_sales.Series["s_dine"].Points.AddXY(1, gridSource[0].Field1);  // 12am-4am Dine
                            chart_res_dine_sales.Series["s_reservation"].Points.AddXY(1, gridSource[1].Field1);  // 12am-4am Reservation

                            chart_res_dine_sales.Series["s_dine"].Points[0].ToolTip = "Dine 12am-4am - Total sales :" + gridSource[0].Field1 + " Orders: " + gridSource[0].Field7;
                            chart_res_dine_sales.Series["s_reservation"].Points[0].ToolTip = "Reservation 12am-4am - Total sales :" + gridSource[1].Field1 + " Orders: " + gridSource[1].Field7;

                            chart_res_dine_sales.Series["s_dine"].Points.AddXY(2, gridSource[0].Field2);  // 4am-8am Dine
                            chart_res_dine_sales.Series["s_reservation"].Points.AddXY(2, gridSource[1].Field2);  // 4am-8am Reservation

                            chart_res_dine_sales.Series["s_dine"].Points[1].ToolTip = "Dine 4am-8am  - Total sales :" + gridSource[0].Field2 + " Orders: " + gridSource[0].Field8;
                            chart_res_dine_sales.Series["s_reservation"].Points[1].ToolTip = "Reservation 4am-8am  - Total sales :" + gridSource[1].Field2 + " Orders: " + gridSource[1].Field8;

                            chart_res_dine_sales.Series["s_dine"].Points.AddXY(3, gridSource[0].Field3);  // 8am-12pm Dine
                            chart_res_dine_sales.Series["s_reservation"].Points.AddXY(3, gridSource[1].Field3);  // 8am-12pm Reservation

                            chart_res_dine_sales.Series["s_dine"].Points[2].ToolTip = "Dine 8am-12pm  - Total sales :" + gridSource[0].Field3 + " Orders: " + gridSource[0].Field9;
                            chart_res_dine_sales.Series["s_reservation"].Points[2].ToolTip = "Reservation 8am-12pm  - Total sales :" + gridSource[1].Field3 + " Orders: " + gridSource[1].Field9;

                            chart_res_dine_sales.Series["s_dine"].Points.AddXY(4, gridSource[0].Field4);  // 12pm-4pm Dine
                            chart_res_dine_sales.Series["s_reservation"].Points.AddXY(4, gridSource[1].Field4);  // 12pm-4pm Reservation

                            chart_res_dine_sales.Series["s_dine"].Points[3].ToolTip = "Dine 12pm-4pm  - Total sales :" + gridSource[0].Field4 + " Orders: " + gridSource[0].Field10;
                            chart_res_dine_sales.Series["s_reservation"].Points[3].ToolTip = "Reservation 12pm-4pm  - Total sales :" + gridSource[1].Field4 + " Orders: " + gridSource[1].Field10;

                            chart_res_dine_sales.Series["s_dine"].Points.AddXY(5, gridSource[0].Field5);  // 4pm-8pm Dine
                            chart_res_dine_sales.Series["s_reservation"].Points.AddXY(5, gridSource[1].Field5);  // 4pm-8pm Reservation

                            chart_res_dine_sales.Series["s_dine"].Points[4].ToolTip = "Dine 4pm-8pm  - Total sales :" + gridSource[0].Field5 + " Orders: " + gridSource[0].Field11;
                            chart_res_dine_sales.Series["s_reservation"].Points[4].ToolTip = "Reservation 4pm-8pm  - Total sales :" + gridSource[1].Field5 + " Orders: " + gridSource[1].Field11;

                            chart_res_dine_sales.Series["s_dine"].Points.AddXY(6, gridSource[0].Field6);  // 8pm-12am Dine
                            chart_res_dine_sales.Series["s_reservation"].Points.AddXY(6, gridSource[1].Field6);  // 8pm-12am Reservation

                            chart_res_dine_sales.Series["s_dine"].Points[5].ToolTip = "Dine 8pm-12am  - Total sales :" + gridSource[0].Field6 + " Orders: " + gridSource[0].Field12;
                            chart_res_dine_sales.Series["s_reservation"].Points[5].ToolTip = "Reservation 8pm-12am  - Total sales :" + gridSource[1].Field6 + " Orders: " + gridSource[1].Field12;
                        }));
                    }
                    else
                    {
                        chart_res_dine_sales.Series["s_dine"].Points.Clear();
                        chart_res_dine_sales.Series["s_reservation"].Points.Clear();

                        chart_res_dine_sales.Series["s_dine"].Points.AddXY(1, gridSource[0].Field1);  // 12am-4am Dine
                        chart_res_dine_sales.Series["s_reservation"].Points.AddXY(1, gridSource[1].Field1);  // 12am-4am Reservation

                        chart_res_dine_sales.Series["s_dine"].Points[0].ToolTip = "Dine 12am-4am - Total sales :" + gridSource[0].Field1 + " Orders: " + gridSource[0].Field7;
                        chart_res_dine_sales.Series["s_reservation"].Points[0].ToolTip = "Reservation 12am-4am - Total sales :" + gridSource[1].Field1 + " Orders: " + gridSource[1].Field7;

                        chart_res_dine_sales.Series["s_dine"].Points.AddXY(2, gridSource[0].Field2);  // 4am-8am Dine
                        chart_res_dine_sales.Series["s_reservation"].Points.AddXY(2, gridSource[1].Field2);  // 4am-8am Reservation

                        chart_res_dine_sales.Series["s_dine"].Points[1].ToolTip = "Dine 4am-8am  - Total sales :" + gridSource[0].Field2 + " Orders: " + gridSource[0].Field8;
                        chart_res_dine_sales.Series["s_reservation"].Points[1].ToolTip = "Reservation 4am-8am  - Total sales :" + gridSource[1].Field2 + " Orders: " + gridSource[1].Field8;

                        chart_res_dine_sales.Series["s_dine"].Points.AddXY(3, gridSource[0].Field3);  // 8am-12pm Dine
                        chart_res_dine_sales.Series["s_reservation"].Points.AddXY(3, gridSource[1].Field3);  // 8am-12pm Reservation

                        chart_res_dine_sales.Series["s_dine"].Points[2].ToolTip = "Dine 8am-12pm  - Total sales :" + gridSource[0].Field3 + " Orders: " + gridSource[0].Field9;
                        chart_res_dine_sales.Series["s_reservation"].Points[2].ToolTip = "Reservation 8am-12pm  - Total sales :" + gridSource[1].Field3 + " Orders: " + gridSource[1].Field9;

                        chart_res_dine_sales.Series["s_dine"].Points.AddXY(4, gridSource[0].Field4);  // 12pm-4pm Dine
                        chart_res_dine_sales.Series["s_reservation"].Points.AddXY(4, gridSource[1].Field4);  // 12pm-4pm Reservation

                        chart_res_dine_sales.Series["s_dine"].Points[3].ToolTip = "Dine 12pm-4pm  - Total sales :" + gridSource[0].Field4 + " Orders: " + gridSource[0].Field10;
                        chart_res_dine_sales.Series["s_reservation"].Points[3].ToolTip = "Reservation 12pm-4pm  - Total sales :" + gridSource[1].Field4 + " Orders: " + gridSource[1].Field10;

                        chart_res_dine_sales.Series["s_dine"].Points.AddXY(5, gridSource[0].Field5);  // 4pm-8pm Dine
                        chart_res_dine_sales.Series["s_reservation"].Points.AddXY(5, gridSource[1].Field5);  // 4pm-8pm Reservation

                        chart_res_dine_sales.Series["s_dine"].Points[4].ToolTip = "Dine 4pm-8pm  - Total sales :" + gridSource[0].Field5 + " Orders: " + gridSource[0].Field11;
                        chart_res_dine_sales.Series["s_reservation"].Points[4].ToolTip = "Reservation 4pm-8pm  - Total sales :" + gridSource[1].Field5 + " Orders: " + gridSource[1].Field11;

                        chart_res_dine_sales.Series["s_dine"].Points.AddXY(6, gridSource[0].Field6);  // 8pm-12am Dine
                        chart_res_dine_sales.Series["s_reservation"].Points.AddXY(6, gridSource[1].Field6);  // 8pm-12am Reservation

                        chart_res_dine_sales.Series["s_dine"].Points[5].ToolTip = "Dine 8pm-12am  - Total sales :" + gridSource[0].Field6 + " Orders: " + gridSource[0].Field12;
                        chart_res_dine_sales.Series["s_reservation"].Points[5].ToolTip = "Reservation 8pm-12am  - Total sales :" + gridSource[1].Field6 + " Orders: " + gridSource[1].Field12;
                    }


                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart FillChartTwoBarChart()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }
        private void SetupChartTwoBar()
        {
            try
            {
                chart_res_dine_sales.Series.Clear();
                // Dine series
                Series seriesDine = new Series("s_dine");
                seriesDine.ChartType = SeriesChartType.Column;
                chart_res_dine_sales.Series.Add(seriesDine);

                // Reservation series
                Series seriesReservation = new Series("s_reservation");
                seriesReservation.ChartType = SeriesChartType.Column;
                chart_res_dine_sales.Series.Add(seriesReservation);

                chart_res_dine_sales.Legends.Add(new Legend("Legend"));
                chart_res_dine_sales.Legends["Legend"].Docking = Docking.Bottom;

                seriesDine.LegendText = "Dine";
                seriesReservation.LegendText = "Reservation";

                chart_res_dine_sales.Legends["Legend"].Font = new Font("Arial", 12, FontStyle.Bold);

                // Title
                Title chartTitledineres = new Title("Dine and Reservation Total");
                chartTitledineres.Font = new Font(chartTitledineres.Font.FontFamily, 12, FontStyle.Bold);
                chart_res_dine_sales.Titles.Add(chartTitledineres);

                chart_res_dine_sales.ChartAreas[0].AxisX.Interval = 1;
                chart_res_dine_sales.ChartAreas[0].AxisX.Minimum = 0;
                chart_res_dine_sales.ChartAreas[0].AxisX.Maximum = 7;

                chart_res_dine_sales.ChartAreas[0].AxisX.CustomLabels.Add(0.5, 1.5, "12am-4am");
                chart_res_dine_sales.ChartAreas[0].AxisX.CustomLabels.Add(1.5, 2.5, "4am-8am");
                chart_res_dine_sales.ChartAreas[0].AxisX.CustomLabels.Add(2.5, 3.5, "8am-12pm");
                chart_res_dine_sales.ChartAreas[0].AxisX.CustomLabels.Add(3.5, 4.5, "12pm-4pm");
                chart_res_dine_sales.ChartAreas[0].AxisX.CustomLabels.Add(4.5, 5.5, "4pm-8pm");
                chart_res_dine_sales.ChartAreas[0].AxisX.CustomLabels.Add(5.5, 6.5, "8pm-12am");

                chart_res_dine_sales.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                chart_res_dine_sales.ChartAreas[0].AxisX.LabelStyle.Angle = 0;


            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart SetupChartTwoBar()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }

        }
        #endregion

        #region Anuuval Chart
        public void FillbarRrevenuChart()
        {
            try
            {
                if (flagexit)
                {
                    DateTime financialYearStart;
                    DateTime financialYearEnd;
                    int fin_year=0;
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (cmb_fin_year.SelectedIndex == -1)
                            {
                                DateTime currentDate = DateTime.Now;
                                fin_year = (currentDate.Month < 4) ? currentDate.Year - 1 : currentDate.Year;
                            }
                            else
                            {
                                fin_year = Convert.ToInt32(cmb_fin_year.SelectedValue);
                            }
                        }));
                    }

                    financialYearStart = new DateTime(fin_year, 4, 1);
                    financialYearEnd = new DateTime(fin_year + 1, 3, 31);

                    BllAnnuvalreport<DmnRpt_1> SearchDetails = new BllAnnuvalreport<DmnRpt_1>();
                    DmnRpt_1 dmnSearchDetBill = new DmnRpt_1();

                    dmnSearchDetBill.sFld2 = financialYearStart.ToString("yyyy-MM-dd");
                    dmnSearchDetBill.sFld3 = financialYearEnd.ToString("yyyy-MM-dd");
                    var invoiceBillMasters = SearchDetails.serachreport("prdISerachAnnuvalreport", dmnSearchDetBill);
                    var sortedResult = invoiceBillMasters
                        .OrderBy(item => CustomOrder(item.nFld1))
                        .ToList();

                    int CustomOrder(int nFld1)
                    {
                        if (nFld1 >= 4 && nFld1 <= 12)
                            return nFld1;
                        else
                            return nFld1 + 100;
                    }

                    if (sortedResult.Count > 0)
                    {
                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() =>
                            {
                                Totalrevenuechart.Series["Sales"].Points.Clear();
                                Totalrevenuechart.Series["Purchase"].Points.Clear();
                                Totalrevenuechart.Series["PurchaseReturn"].Points.Clear();

                                for (int i = 0; i < sortedResult.Count; i++)
                                {
                                    Totalrevenuechart.Series["Sales"].Points.AddXY(i + 1, sortedResult[i].dFld1);
                                    Totalrevenuechart.Series["Purchase"].Points.AddXY(i + 1, sortedResult[i].dFld2);
                                    Totalrevenuechart.Series["PurchaseReturn"].Points.AddXY(i + 1, sortedResult[i].dFld3);

                                    Totalrevenuechart.Series["Sales"].Points[i].ToolTip = $"Sales: {sortedResult[i].dFld1}";
                                    Totalrevenuechart.Series["Purchase"].Points[i].ToolTip = $"Purchase: {sortedResult[i].dFld2}";
                                    Totalrevenuechart.Series["PurchaseReturn"].Points[i].ToolTip = $"Purchase Return: {sortedResult[i].dFld3}";
                                }

                                Totalrevenuechart.ChartAreas[0].AxisX.Interval = 1;
                                Totalrevenuechart.ChartAreas[0].AxisX.Minimum = 0;
                                Totalrevenuechart.ChartAreas[0].AxisX.Maximum = 13;

                                Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Clear();
                                for (int i = 0; i < 12; i++)
                                {
                                    Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(i + 0.5, i + 1.5, financialYearStart.AddMonths(i).ToString("MMM-yyyy"));
                                }
                                Totalrevenuechart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                                Totalrevenuechart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;

                                Title existingTitle = Totalrevenuechart.Titles.FirstOrDefault(t => t.Text.StartsWith("Annual Report"));
                                if (existingTitle != null)
                                {
                                    existingTitle.Text = $"Annual Report {financialYearStart:MMM-yyyy} - {financialYearEnd:MMM-yyyy}";
                                }
                            }));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart FillbarRrevenuChart()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        public void Fill_combo_finyear()
        {
            try
            {
                BllSearch<DmnHRegistration> bllSearch = new BllSearch<DmnHRegistration>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblHotelRegistration";
                objSearch.WhereNdGroupBy = "Where status=1 and BranchId=" + BranchId;
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    flagexit = true;
                     companystartYear = Convert.ToInt32(itemSource[0].CreatedOn.ToString("yyyy"));
                    int startYear = Convert.ToInt32(itemSource[0].CreatedOn.ToString("yyyy"));
                    DateTime currentDate = DateTime.Now;
                    List<DmnComboBox> finYearList = new List<DmnComboBox>();
                    for (int i = currentDate.Year - startYear; i >= 0; i--)
                    {
                        int year = startYear + i;
                        DateTime financialYearStart = new DateTime(year, 4, 1);
                        DateTime financialYearEnd = new DateTime(year + 1, 3, 31);
                        string financialYear = string.Format("{0}-{1}", financialYearStart.ToString("yyyy"), financialYearEnd.ToString("yyyy"));
                        DmnComboBox finYear = new DmnComboBox { Id = year, Value = financialYear };
                        finYearList.Add(finYear);
                    }
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            cmb_fin_year.DataSource = finYearList;
                            cmb_fin_year.DisplayMember = "Value";
                            cmb_fin_year.ValueMember = "Id";
                        }));
                    }
                    else
                    {
                        cmb_fin_year.DataSource = finYearList;
                        cmb_fin_year.DisplayMember = "Value";
                        cmb_fin_year.ValueMember = "Id";
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart Fill_combo_finyear()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }
        private void SetupChart_Revenue()
        {
            try
            {
                if (flagexit)
                {
                    DateTime financialYearStart;
                    DmnComboBox selectedFinYear = (DmnComboBox)cmb_fin_year.SelectedItem;
                    int year = Convert.ToInt32(selectedFinYear.Id);
                    financialYearStart = new DateTime(year, 4, 1);
                    DateTime financialYearEnd = financialYearStart.AddYears(1).AddDays(-1);

                    Totalrevenuechart.Series.Clear();
                    // Sales series
                    Series seriesSales = new Series("Sales");
                    seriesSales.ChartType = SeriesChartType.Column;
                    Totalrevenuechart.Series.Add(seriesSales);

                    // Purchase series
                    Series seriesPurchase = new Series("Purchase");
                    seriesPurchase.ChartType = SeriesChartType.Column;
                    Totalrevenuechart.Series.Add(seriesPurchase);

                    // Purchase Return series
                    Series seriesPurchaseReturn = new Series("PurchaseReturn");
                    seriesPurchaseReturn.ChartType = SeriesChartType.Column;
                    Totalrevenuechart.Series.Add(seriesPurchaseReturn);


                    Totalrevenuechart.Legends.Add(new Legend("Legend"));
                    Totalrevenuechart.Legends["Legend"].Docking = Docking.Bottom;
                    seriesSales.LegendText = "Sales";
                    seriesPurchase.LegendText = "Purchase";
                    seriesPurchaseReturn.LegendText = "Purchase Return";
                    Totalrevenuechart.Legends["Legend"].Font = new Font("Arial", 12, FontStyle.Bold);

                    // Title
                    Title chartTitle = new Title($"Annual Report {financialYearStart:MMM-yyyy} - {financialYearEnd:MMM-yyyy}");
                    chartTitle.Font = new Font(chartTitle.Font.FontFamily, 12, FontStyle.Bold);
                    Totalrevenuechart.Titles.Add(chartTitle);

                    Totalrevenuechart.ChartAreas[0].AxisX.Interval = 1;
                    Totalrevenuechart.ChartAreas[0].AxisX.Minimum = 0;
                    Totalrevenuechart.ChartAreas[0].AxisX.Maximum = 13;

                    Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(0.5, 1.5, $"{financialYearStart:MMM-yyyy}");
                    Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(1.5, 2.5, $"{financialYearStart.AddMonths(1):MMM-yyyy}");
                    Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(2.5, 3.5, $"{financialYearStart.AddMonths(2):MMM-yyyy}");
                    Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(3.5, 4.5, $"{financialYearStart.AddMonths(3):MMM-yyyy}");
                    Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(4.5, 5.5, $"{financialYearStart.AddMonths(4):MMM-yyyy}");
                    Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(5.5, 6.5, $"{financialYearStart.AddMonths(5):MMM-yyyy}");
                    Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(6.5, 7.5, $"{financialYearStart.AddMonths(6):MMM-yyyy}");
                    Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(7.5, 8.5, $"{financialYearStart.AddMonths(7):MMM-yyyy}");
                    Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(8.5, 9.5, $"{financialYearStart.AddMonths(8):MMM-yyyy}");
                    Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(9.5, 10.5, $"{financialYearStart.AddMonths(9):MMM-yyyy}");
                    Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(10.5, 11.5, $"{financialYearStart.AddMonths(10):MMM-yyyy}");
                    Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(11.5, 12.5, $"{financialYearStart.AddMonths(11):MMM-yyyy}");

                    Totalrevenuechart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 9, FontStyle.Bold);

                    Totalrevenuechart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart SetupChart_Revenue()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }
        #endregion

        #region ONE BAR CHART TOTAL DISCOUNT TIME PEROD 24HRS
        public void FillCharttotaldiscountChart()
        {
            try
            {
                DateTime C_startDate = DateTime.Now;
                DateTime C_endDate = DateTime.Now;
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        if (Cmbserachtype.SelectedIndex == Convert.ToInt32(DMY.MONTH) || Cmbserachtype.SelectedIndex == Convert.ToInt32(DMY.YEAR))
                        {
                            C_startDate = DateTime.Now;
                            C_endDate = DateTime.Now;
                            lbl_discount.Text = "DATE : " + Convert.ToString(C_startDate.Date);
                        }
                        else
                        {
                            C_startDate = startDate;
                            C_endDate = endDate;
                        }
                    }));
                }
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 00 AND 4 THEN DiscountAmount ELSE 0 END) AS Field1, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 4 AND 8 THEN DiscountAmount ELSE 0 END) AS Field2, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 8 AND 12 THEN DiscountAmount ELSE 0 END) AS Field3, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 12 AND 16 THEN DiscountAmount ELSE 0 END) AS Field4, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 16 AND 20 THEN DiscountAmount ELSE 0 END) AS Field5, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 20 AND 24 THEN DiscountAmount ELSE 0 END) AS Field6";
                dmnSearch.Table = " tblSalesMaster ";
                dmnSearch.WhereNdGroupBy = " where  Status=1 AND CONVERT(DATE, CreatedDateTime) BETWEEN '" + C_startDate.ToString("yyyy-MM-dd") + "' AND '" + C_endDate.ToString("yyyy-MM-dd") + "'" ;
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {

                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            totaldisc_chart.Series["s_totaldiscount"].Points.Clear();
                            totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(1, gridSource[0].Field1);  // 12am-4am 
                            totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(2, gridSource[0].Field2);  // 4am-8am 
                            totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(3, gridSource[0].Field3);  // 8am-12pm 
                            totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(4, gridSource[0].Field4);  // 12pm-4pm 
                            totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(5, gridSource[0].Field5);  // 4pm-8pm
                            totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(6, gridSource[0].Field6);  // 8pm-12am 

                            totaldisc_chart.Series["s_totaldiscount"].Points[0].ToolTip = $"Discount : " + gridSource[0].Field1;
                            totaldisc_chart.Series["s_totaldiscount"].Points[1].ToolTip = $"Discount : " + gridSource[0].Field2;
                            totaldisc_chart.Series["s_totaldiscount"].Points[2].ToolTip = $"Discount : " + gridSource[0].Field3;
                            totaldisc_chart.Series["s_totaldiscount"].Points[3].ToolTip = $"Discount : " + gridSource[0].Field4;
                            totaldisc_chart.Series["s_totaldiscount"].Points[4].ToolTip = $"Discount : " + gridSource[0].Field5;
                            totaldisc_chart.Series["s_totaldiscount"].Points[5].ToolTip = $"Discount : " + gridSource[0].Field6;
                        }));
                    }
                    else
                    {
                        totaldisc_chart.Series["s_totaldiscount"].Points.Clear();
                        totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(1, gridSource[0].Field1);  // 12am-4am 
                        totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(2, gridSource[0].Field2);  // 4am-8am 
                        totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(3, gridSource[0].Field3);  // 8am-12pm 
                        totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(4, gridSource[0].Field4);  // 12pm-4pm 
                        totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(5, gridSource[0].Field5);  // 4pm-8pm
                        totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(6, gridSource[0].Field6);  // 8pm-12am 

                        totaldisc_chart.Series["s_totaldiscount"].Points[0].ToolTip = $"Discount : " + gridSource[0].Field1;
                        totaldisc_chart.Series["s_totaldiscount"].Points[1].ToolTip = $"Discount : " + gridSource[0].Field2;
                        totaldisc_chart.Series["s_totaldiscount"].Points[2].ToolTip = $"Discount : " + gridSource[0].Field3;
                        totaldisc_chart.Series["s_totaldiscount"].Points[3].ToolTip = $"Discount : " + gridSource[0].Field4;
                        totaldisc_chart.Series["s_totaldiscount"].Points[4].ToolTip = $"Discount : " + gridSource[0].Field5;
                        totaldisc_chart.Series["s_totaldiscount"].Points[5].ToolTip = $"Discount : " + gridSource[0].Field6;
                    }

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart FillCharttotaldiscountChart()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }
        private void SetupCharttotaldiscount()
        {
            try
            {
                totaldisc_chart.Series.Clear();
                // Dine series
                Series seriestotalsi = new Series("s_totaldiscount");
                seriestotalsi.ChartType = SeriesChartType.Column;
                totaldisc_chart.Series.Add(seriestotalsi);

                // Title
                Title chartTitletotaldis = new Title("Total Discount");
                chartTitletotaldis.Font = new Font(chartTitletotaldis.Font.FontFamily, 12, FontStyle.Bold);
                totaldisc_chart.Titles.Add(chartTitletotaldis);

                totaldisc_chart.ChartAreas[0].AxisX.Interval = 1;
                totaldisc_chart.ChartAreas[0].AxisX.Minimum = 0;
                totaldisc_chart.ChartAreas[0].AxisX.Maximum = 7;

                totaldisc_chart.ChartAreas[0].AxisX.CustomLabels.Add(0.5, 1.5, "12am-4am");
                totaldisc_chart.ChartAreas[0].AxisX.CustomLabels.Add(1.5, 2.5, "4am-8am");
                totaldisc_chart.ChartAreas[0].AxisX.CustomLabels.Add(2.5, 3.5, "8am-12pm");
                totaldisc_chart.ChartAreas[0].AxisX.CustomLabels.Add(3.5, 4.5, "12pm-4pm");
                totaldisc_chart.ChartAreas[0].AxisX.CustomLabels.Add(4.5, 5.5, "4pm-8pm");
                totaldisc_chart.ChartAreas[0].AxisX.CustomLabels.Add(5.5, 6.5, "8pm-12am");

                totaldisc_chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                totaldisc_chart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;


            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart SetupCharttotaldiscount()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }

        }
        #endregion

        #region PIE CHART EXPENCE
        public void SetupChartExpence()
        {
            try
            {
                expencepiechart.Series.Clear();
                expencepiechart.Legends.Clear();

                Series seriesExpence = new Series("s_expence");
                seriesExpence.ChartType = SeriesChartType.Doughnut;
                expencepiechart.Series.Add(seriesExpence);

                // Add legend
                expencepiechart.Legends.Add(new Legend("Legend"));
                expencepiechart.Legends["Legend"].Docking = Docking.Right;
                seriesExpence.LegendText = "Expence";
                expencepiechart.Legends["Legend"].Font = new Font("Arial", 8, FontStyle.Regular);
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart SetupChartExpence()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        public void FillChartExpence()
        {
            try
            {
                decimal totalexpence = 0;
                BllExpenceChart<DmnGridFields> bllSearch = new BllExpenceChart<DmnGridFields>();
                DmnGridFields dmnSearch = new DmnGridFields();
                dmnSearch.StartDate = startDate;
                dmnSearch.EndDate = endDate;
                Expencedata = bllSearch.serachexpencedata("prdexpencechartdata", dmnSearch);

                if (Expencedata.Count > 0)
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            expencepiechart.Series["s_expence"].Points.Clear();

                            int counter = 0;
                            foreach (var row in Expencedata)
                            {
                                var category = row.Field1;
                                var amount = row.Field2;

                                var dataPoint = new DataPoint();
                                dataPoint.SetValueXY(category, amount);
                                expencepiechart.Series["s_expence"].Points.Add(dataPoint);

                                dataPoint.LegendText = $"{category}: {amount}";
                                dataPoint.ToolTip = $"{category}: {amount}";
                                totalexpence = totalexpence + Convert.ToDecimal(amount);
                                counter++;
                                if (counter >= 4)
                                    break;
                            }

                            if (counter < Expencedata.Count)
                            {
                                decimal remainingAmount = Expencedata.Skip(4).Sum(row => Convert.ToDecimal(row.Field2));

                                var dataPointOthers = new DataPoint();
                                dataPointOthers.SetValueXY("Remaining", remainingAmount);

                                expencepiechart.Series["s_expence"].Points.Add(dataPointOthers);

                                dataPointOthers.LegendText = $"Remaining [Click to viwe more] : {remainingAmount}";
                                dataPointOthers.ToolTip = "Remaining: " + remainingAmount + "  [Click to viwe more....]";
                                //dataPointOthers.Font = new Font("Arial", 10, FontStyle.Bold);

                                totalexpence = totalexpence + remainingAmount;
                            }

                            expencepiechart.Titles.Clear();
                            Title chartTitleExpence = new Title("Total Expense : " + totalexpence);
                            chartTitleExpence.Font = new Font(chartTitleExpence.Font.FontFamily, 12, FontStyle.Bold);
                            expencepiechart.Titles.Add(chartTitleExpence);
                        }));
                    }

                }
                else
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            expencepiechart.Series.Clear();
                            expencepiechart.Legends.Clear();

                            Series seriesExpence = new Series("s_expence");
                            seriesExpence.ChartType = SeriesChartType.Doughnut;
                            expencepiechart.Series.Add(seriesExpence);

                            // Add legend
                            expencepiechart.Legends.Add(new Legend("Legend"));
                            expencepiechart.Legends["Legend"].Docking = Docking.Right;
                            seriesExpence.LegendText = "Expence";
                            expencepiechart.Legends["Legend"].Font = new Font("Arial", 8, FontStyle.Regular);

                            expencepiechart.Titles.Clear();
                            Title chartTitleExpence = new Title("No Data");
                            chartTitleExpence.Font = new Font(chartTitleExpence.Font.FontFamily, 12, FontStyle.Bold);
                            expencepiechart.Titles.Add(chartTitleExpence);
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart FillChartExpence()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }
        private void expencepiechart_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var r = expencepiechart.HitTest(e.X, e.Y);
                int index = r.PointIndex;

                if (index == expencepiechart.Series["s_expence"].Points.Count - 1 && expencepiechart.Series["s_expence"].Points.Any())
                {
                    int panelWidthPercentage = 30;
                    int panelheightPercentage = 50;
                    int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                    int panelHeight = (int)(this.Height * panelheightPercentage / 100);

                    pnlexpence_viewmore.Size = new Size(panelWidth, panelHeight);
                    pnlexpence_viewmore.Location = new Point(this.ClientSize.Width / 2 - pnlexpence_viewmore.Size.Width / 2, this.ClientSize.Height / 2 - pnlexpence_viewmore.Size.Height / 2);
                    pnlexpence_viewmore.Visible = true;

                    int rowIndex = 0;
                    int columnIndex = 0;
                    decimal remexptotal = 0;
                    tlp_expence_viewmore.Controls.Clear();

                    #region Column heading Expence View more
                    Label categoryname_h = new Label()
                    {
                        Text = "Product Name",
                        Name = "lbl_categoryname_heading",
                        Dock = DockStyle.Top,
                        ForeColor = Color.Black,
                        Font = new Font("Arial", 12, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleLeft,
                        BackColor = Color.FromArgb(159, 166, 178),
                    };

                    Label amount_h = new Label()
                    {
                        Text = "Amount",
                        Name = "lbl_amount_heading",
                        Dock = DockStyle.Top,
                        ForeColor = Color.Black,
                        Font = new Font("Arial", 10, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter,
                        BackColor = Color.FromArgb(159, 166, 178),
                    };

                    tlp_expence_viewmore.Controls.Add(categoryname_h, columnIndex, rowIndex);
                    columnIndex++;
                    tlp_expence_viewmore.Controls.Add(amount_h, columnIndex, rowIndex);
                    rowIndex++;
                    #endregion

                    // Loop through Expencedata to populate labels in the TableLayoutPanel
                    for (int i = 2; i < Expencedata.Count; i++)
                    {
                        columnIndex = 0;
                        Label categoryname = new Label()
                        {
                            Text = Expencedata[i].Field1,
                            Name = "lbl_categoryname_" + Expencedata[i].Field1,
                            Dock = DockStyle.Top,
                            ForeColor = Color.Black,
                            Font = new Font("Arial", 12, FontStyle.Bold),
                            TextAlign = ContentAlignment.MiddleLeft,
                            BackColor = Color.AliceBlue,
                        };

                        Label amount = new Label()
                        {
                            Text = Expencedata[i].Field2,
                            Name = "lbl_amount_" + Expencedata[i].Field1,
                            Dock = DockStyle.Top,
                            ForeColor = Color.Black,
                            TextAlign = ContentAlignment.MiddleCenter,
                            BackColor = Color.AliceBlue,
                        };
                        remexptotal = remexptotal + Convert.ToDecimal(Expencedata[i].Field2);
                        tlp_expence_viewmore.Controls.Add(categoryname, columnIndex, rowIndex);
                        columnIndex++;
                        tlp_expence_viewmore.Controls.Add(amount, columnIndex, rowIndex);

                        rowIndex++;
                    }
                    ibrem_exp_total.Text = Convert.ToString(remexptotal);
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart expencepiechart_MouseClick", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        private Image ResizeImage(Image image, Size size)
        {
            return new Bitmap(image, size);
        }


        #endregion

        #region  SLOW FAST MOVING ITEMS

        public void LoadSlowfastData()
        {
            try
            {                
                BllFastSolwItemSerach<DmnGridFields> bllSearch = new BllFastSolwItemSerach<DmnGridFields>();
                DmnGridFields dmnSearch = new DmnGridFields();
                dmnSearch.Field1 = startDate.ToString("yyyy-MM-dd");
                dmnSearch.Field2 = endDate.ToString("yyyy-MM-dd");
                resultslowfastdata = bllSearch.serachSolwItemSerach("prdSolwFastItemchartdata", dmnSearch);
                //if (this.IsHandleCreated)
                //{
                //    this.Invoke(new Action(() =>
                //    {
                //        tcslow_move.SelectedIndex = 1;
                //    }));
                //}
        
            }
            catch(Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("DashboardChart LoadSlowfastData", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }
        private void tcslow_move_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!FrmLogin.TestUser)
                {
                    int rowIndex = 0;
                    int columnIndex = 0;
                    var resultdata = resultslowfastdata;
                    if (tcslow_move.SelectedIndex == 0)
                    {
                        rowIndex = 0;
                        tlp_fastmovingitem.Controls.Clear();

                        #region Column heading Fast Moving Item
                        Label fastmoveitem_h = new Label()
                        {
                            Text = "Product Name",
                            Name = "lbl_product_fast",
                            Dock = DockStyle.Top,
                            ForeColor = Color.Black,
                            Font = new Font("Arial", 13, FontStyle.Bold),
                            TextAlign = ContentAlignment.MiddleLeft,
                            BackColor = Color.FromArgb(159, 166, 178),
                        };

                        Label totalamount_h = new Label()
                        {
                            Text = "Total Amount",
                            Name = "lbl_amount_fast",
                            Dock = DockStyle.Top,
                            ForeColor = Color.Black,
                            Font = new Font("Arial", 13, FontStyle.Bold),
                            TextAlign = ContentAlignment.MiddleCenter,
                            BackColor = Color.FromArgb(159, 166, 178),
                        };

                        Label occurancefast_h = new Label()
                        {
                            Text = "Total No Sales",
                            Name = "lbl_totalsaleno",
                            Dock = DockStyle.Top,
                            ForeColor = Color.Black,
                            Font = new Font("Arial", 13, FontStyle.Bold),
                            TextAlign = ContentAlignment.MiddleCenter,
                            BackColor = Color.FromArgb(159, 166, 178),
                        };
                        tlp_fastmovingitem.Controls.Add(fastmoveitem_h, columnIndex, rowIndex);
                        columnIndex++;
                        tlp_fastmovingitem.Controls.Add(totalamount_h, columnIndex, rowIndex);
                        columnIndex++;
                        tlp_fastmovingitem.Controls.Add(occurancefast_h, columnIndex, rowIndex);
                        rowIndex++;
                        #endregion

                        for (int i = 0; i < 10; i++)
                        {
                            columnIndex = 0;
                            //tlp_expence_viewmore.RowStyles.Add(new RowStyle(SizeType.Percent,10));
                            Label fastmoveitem = new Label()
                            {
                                Text = resultdata[i].Field1,
                                Name = "lbl_categoryname_" + resultdata[i].Field1,
                                Dock = DockStyle.Top,
                                ForeColor = Color.Black,
                                Font = new Font("Arial", 12, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleLeft,
                                BackColor = Color.AliceBlue,
                            };

                            Label totalamount = new Label()
                            {
                                Text = resultdata[i].Field2,
                                Name = "lbl_amount_" + resultdata[i].Field1,
                                Dock = DockStyle.Top,
                                ForeColor = Color.Black,
                                Font = new Font("Arial", 10, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BackColor = Color.AliceBlue,
                            };

                            Label occurancefast = new Label()
                            {
                                Text = resultdata[i].Field3,
                                Name = "lbl_occurancefast_" + resultdata[i].Field1,
                                Dock = DockStyle.Top,
                                ForeColor = Color.Black,
                                Font = new Font("Arial", 10, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BackColor = Color.AliceBlue,
                            };


                            tlp_fastmovingitem.Controls.Add(fastmoveitem, columnIndex, rowIndex);
                            columnIndex++;
                            tlp_fastmovingitem.Controls.Add(totalamount, columnIndex, rowIndex);
                            columnIndex++;
                            tlp_fastmovingitem.Controls.Add(occurancefast, columnIndex, rowIndex);
                            rowIndex++;
                        }
                    }
                    else if (tcslow_move.SelectedIndex == 1)
                    {
                        rowIndex = 0;
                        tlp_slowingitems.Controls.Clear();

                        #region Column heading Fast Solw Item
                        Label fastmoveitem_h = new Label()
                        {
                            Text = "Product Name",
                            Name = "lbl_product_Solw",
                            Dock = DockStyle.Top,
                            ForeColor = Color.Black,
                            Font = new Font("Arial", 13, FontStyle.Bold),
                            TextAlign = ContentAlignment.MiddleLeft,
                            BackColor = Color.FromArgb(159, 166, 178),
                        };

                        Label totalamount_h = new Label()
                        {
                            Text = "Total Amount",
                            Name = "lbl_amount_solw",
                            Dock = DockStyle.Top,
                            ForeColor = Color.Black,
                            Font = new Font("Arial", 13, FontStyle.Bold),
                            TextAlign = ContentAlignment.MiddleCenter,
                            BackColor = Color.FromArgb(159, 166, 178),
                        };

                        Label occurancefast_h = new Label()
                        {
                            Text = "Total No Sales",
                            Name = "lbl_totalsaleno_slow",
                            Dock = DockStyle.Top,
                            ForeColor = Color.Black,
                            Font = new Font("Arial", 13, FontStyle.Bold),
                            TextAlign = ContentAlignment.MiddleCenter,
                            BackColor = Color.FromArgb(159, 166, 178),
                        };

                        tlp_slowingitems.Controls.Add(fastmoveitem_h, columnIndex, rowIndex);
                        columnIndex++;
                        tlp_slowingitems.Controls.Add(totalamount_h, columnIndex, rowIndex);
                        columnIndex++;
                        tlp_slowingitems.Controls.Add(occurancefast_h, columnIndex, rowIndex);
                        rowIndex++;
                        #endregion

                        for (int i = resultdata.Count - 1; i >= Math.Max(0, resultdata.Count - 10); i--)
                        {
                            if (Convert.ToDecimal(resultdata[i].Field3) < 5)
                            {
                                columnIndex = 0;
                                Label fastmoveitem = new Label()
                                {
                                    Text = resultdata[i].Field1,
                                    Name = "lbl_categoryname_" + resultdata[i].Field1,
                                    Dock = DockStyle.Top,
                                    ForeColor = Color.Black,
                                    Font = new Font("Arial", 12, FontStyle.Bold),
                                    TextAlign = ContentAlignment.MiddleLeft,
                                    BackColor = Color.AliceBlue,
                                };

                                Label totalamount = new Label()
                                {
                                    Text = resultdata[i].Field2,
                                    Name = "lbl_amount_" + resultdata[i].Field1,
                                    Dock = DockStyle.Top,
                                    ForeColor = Color.Black,
                                    Font = new Font("Arial", 10, FontStyle.Bold),
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    BackColor = Color.AliceBlue,
                                };
                                Label occuranceslow = new Label()
                                {
                                    Text = resultdata[i].Field3,
                                    Name = "lbl_occuranceslow_" + resultdata[i].Field1,
                                    Dock = DockStyle.Top,
                                    ForeColor = Color.Black,
                                    Font = new Font("Arial", 10, FontStyle.Bold),
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    BackColor = Color.AliceBlue,
                                };

                                tlp_slowingitems.Controls.Add(fastmoveitem, columnIndex, rowIndex);
                                columnIndex++;
                                tlp_slowingitems.Controls.Add(totalamount, columnIndex, rowIndex);
                                columnIndex++;
                                tlp_slowingitems.Controls.Add(occuranceslow, columnIndex, rowIndex);
                                rowIndex++;
                            }

                        }
                    }

                    if (RefreshFlag)
                        tlp_date_dashboard.Focus();
                    else
                        tcslow_move.Focus();

                    RefreshFlag = false;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart tcslow_move_SelectedIndexChanged", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }

        }
        #endregion

        private void dtp_dashboard_ValueChanged(object sender, EventArgs e)
        {
            RefreshCharts();
        }

        private void cmb_fin_year_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillbarRrevenuChart();
        }
        private void btnclose_exp_view_Click(object sender, EventArgs e)
        {
            pnlexpence_viewmore.Visible = false;
        }

        #region please wait loading
        private async void ShowLoadingdGif()
        {
            this.Opacity = 0;
            LoadingGif gifForm = new LoadingGif();
            gifForm.StartPosition = FormStartPosition.CenterScreen;
            gifForm.ShowDialog();
            this.Opacity = 100;
        }
        #endregion

        private void btnrefreshreport_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
            {
                FrmDashboard instanceFrmDashboard = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                instanceFrmDashboard.time_syn_lal.Start(); 
            }
            RefreshCharts();
          
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:       
                    return true;

                case Keys.Enter:           
                    return true;

                case Keys.Escape:
                    if (pnl_boxdetails_dash.Visible)
                    {
                        pnl_boxdetails_dash.Visible = false; 
                        dgv_activefood.Visible = false;
                        dgv_activedine.Visible = false;
                        dgv_cancelorders.Visible = false;
                        dgv_reservation.Visible = false;
                        tlp_offer_type_main.Visible = false;

                        dgv_activefood.Rows.Clear();
                        dgv_activedine.Rows.Clear();
                        dgv_cancelorders.Rows.Clear();
                        dgv_reservation.Rows.Clear();
                        dgv_promotion.Rows.Clear();

                        tlp_pagination.Visible = false;
                    }

                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }


        private void Cmbserachtype_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cmbserachvalue.Items.Clear(); 
                if (Cmbserachtype.SelectedIndex == Convert.ToInt32(DMY.DAY))
                {
                    for (int i = 1; i <= 31; i++)
                    {
                        cmbserachvalue.Items.Add(i);
                    }
                }
                else if (Cmbserachtype.SelectedIndex == Convert.ToInt32(DMY.MONTH))
                {
                    string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
                    foreach (string monthName in monthNames)
                    {
                        cmbserachvalue.Items.Add(monthName);
                    }
                }
                else if (Cmbserachtype.SelectedIndex == Convert.ToInt32(DMY.YEAR))
                {
                    int currentYear = DateTime.Today.Year;
                    for (int year = companystartYear; year <= currentYear; year++)
                    {
                        cmbserachvalue.Items.Add(year);
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart Cmbserachtype_SelectionChangeCommitted", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        private void cmbserachvalue_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DateTime currentDate = DateTime.Now; 

                if (Cmbserachtype.SelectedIndex == Convert.ToInt32(DMY.DAY))
                {
                    int selectedDay = Convert.ToInt32(cmbserachvalue.SelectedItem); 
                    startDate = new DateTime(currentDate.Year, currentDate.Month, selectedDay);
                    endDate = startDate.AddDays(1).AddSeconds(-1);

                    lbl_fastslow.Text = "DATE : " + Convert.ToString(startDate.Date);
                    lbl_discount.Text = "DATE : " + Convert.ToString(startDate.Date);
                    lbl_paymentdetails.Text = "DATE : " +  Convert.ToString(startDate.Date);
                    lblexpence.Text = "DATE : " +  Convert.ToString(startDate.Date);
                    lbl_res_dine_sales.Text = "DATE : " +  Convert.ToString(startDate.Date);
                }
                else if (Cmbserachtype.SelectedIndex == Convert.ToInt32(DMY.MONTH))
                {
                    string selectedMonthName = cmbserachvalue.SelectedItem.ToString(); 
                    int selectedMonthIndex = Array.IndexOf(CultureInfo.CurrentCulture.DateTimeFormat.MonthNames, selectedMonthName);
                    startDate = new DateTime(currentDate.Year, selectedMonthIndex + 1, 1); 
                    endDate = startDate.AddMonths(1).AddSeconds(-1);

                    lbl_fastslow.Text = "MONTH : " + selectedMonthName;
                    lbl_discount.Text = "MONTH : " + selectedMonthName;
                    lbl_paymentdetails.Text = "MONTH : " + selectedMonthName;
                    lblexpence.Text = "MONTH : " + selectedMonthName;
                    lbl_res_dine_sales.Text = "MONTH : " + selectedMonthName;
                }
                else if (Cmbserachtype.SelectedIndex == Convert.ToInt32(DMY.YEAR))
                {
                    int selectedYear = Convert.ToInt32(cmbserachvalue.SelectedItem); 
                    startDate = new DateTime(selectedYear, 1, 1); 
                    endDate = startDate.AddYears(1).AddSeconds(-1);

                    lbl_fastslow.Text = "YEAR : " + Convert.ToString(selectedYear);
                    lbl_discount.Text = "YEAR : " + Convert.ToString(selectedYear);
                    lbl_paymentdetails.Text = "YEAR : " + Convert.ToString(selectedYear);
                    lblexpence.Text = "YEAR : " +  Convert.ToString(selectedYear);
                    lbl_res_dine_sales.Text = "YEAR : " +  Convert.ToString(selectedYear);
                }
                RefreshCharts();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart cmbserachvalue_SelectionChangeCommitted", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        private void label16_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                lblboxdetailsheader.Text = "Active Dine List";
                int panelWidthPercentage = 90;
                int panelheightPercentage = 70;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                pnl_boxdetails_dash.Size = new Size(panelWidth, panelHeight);
                pnl_boxdetails_dash.Visible = true;

                pnl_boxdetails_dash.Location = new Point(this.ClientSize.Width / 2 - pnl_boxdetails_dash.Size.Width / 2, this.ClientSize.Height / 2 - pnl_boxdetails_dash.Size.Height / 2);

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dgv_activedine.Visible = true;
                UpdateData();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart pictureBox2_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        private void label14_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                lblboxdetailsheader.Text = "Cancel Order List";
                int panelWidthPercentage = 90;
                int panelheightPercentage = 70;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                pnl_boxdetails_dash.Size = new Size(panelWidth, panelHeight);

                pnl_boxdetails_dash.Visible = true;
                pnl_boxdetails_dash.Location = new Point(this.ClientSize.Width / 2 - pnl_boxdetails_dash.Size.Width / 2, this.ClientSize.Height / 2 - pnl_boxdetails_dash.Size.Height / 2);
                dgv_cancelorders.Visible = true;
                UpdateData();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart lblcancelled_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));

            }
        }

        private void label18_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                lblboxdetailsheader.Text = "Active Menu List";
                int panelWidthPercentage = 90;
                int panelheightPercentage = 70;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                pnl_boxdetails_dash.Size = new Size(panelWidth, panelHeight);

                pnl_boxdetails_dash.Visible = true;
                pnl_boxdetails_dash.Location = new Point(this.ClientSize.Width / 2 - pnl_boxdetails_dash.Size.Width / 2, this.ClientSize.Height / 2 - pnl_boxdetails_dash.Size.Height / 2);
                dgv_activefood.Visible = true;
                UpdateData();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart label18_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        private void lbl_act_promo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                lblboxdetailsheader.Text = "Active Promotion List";
                int panelWidthPercentage = 90;
                int panelheightPercentage = 70;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                pnl_boxdetails_dash.Size = new Size(panelWidth, panelHeight);

                pnl_boxdetails_dash.Visible = true;
                pnl_boxdetails_dash.Location = new Point(this.ClientSize.Width / 2 - pnl_boxdetails_dash.Size.Width / 2, this.ClientSize.Height / 2 - pnl_boxdetails_dash.Size.Height / 2);
                tlp_offer_type_main.Visible = true;
                UpdateData();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart lblactivepromo_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }

        private void lbl_up_res_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                lblboxdetailsheader.Text = "Upcoming Reservation List";
                int panelWidthPercentage = 90;
                int panelheightPercentage = 70;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                pnl_boxdetails_dash.Size = new Size(panelWidth, panelHeight);

                pnl_boxdetails_dash.Visible = true;
                pnl_boxdetails_dash.Location = new Point(this.ClientSize.Width / 2 - pnl_boxdetails_dash.Size.Width / 2, this.ClientSize.Height / 2 - pnl_boxdetails_dash.Size.Height / 2);
                dgv_reservation.Visible = true;
                UpdateData();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("DashboardChart lbl_up_res_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.DashboardChart));
            }
        }
    }
}
