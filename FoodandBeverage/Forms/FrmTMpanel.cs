using BLL;
using Dapper;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserAccount;
using FoodandBeverage.Forms.UserControl;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Frmkeypad = FoodandBeverage.Forms.UserAccount.Frmkeypad;

namespace FoodandBeverage.Forms
{
    public partial class FrmTMpanel : Form
    {
        public static int BranchId = 0;
        public static int UserId = 0;
        public static int UserType = 0;

        public static int sactionno = 0;
        public static int sslno = 1;
        public static int stidpkikno = 2;
        public static int sinvno = 3;
        public static int sentrydateno = 4;
        public static int spaymodeno = 5;
        public static int stotalqtyno = 6;
        public static int stotalnetamtno = 7;
        public static int sdeviceo = 8;
        public static int semployenameno = 9;
        public static int sviewmoreno = 10;  
        public static int ssalemateridno = 11;
        public static int spayiddno = 12;
        public static int stranstypeno = 13;
        public static int sempidno = 14;
        public static int ssaletype = 15;
        public static int stidno = 16;
        public static int spkidno = 17;

        public static int sdslno = 0;
        public static int sbarcodecardno = 1;
        public static int sdproductno = 2;
        public static int sdqtyno = 3;
        public static int sdnetamtno = 4;

        int Pagecount = 10;// Convert.ToInt32(ConfigurationManager.AppSettings["TMPANELShowCount"]);
        private List<DmnGridFields> Transactiondata;
        int currentPage = 1;
        int pageSize = 0;
        int totalPages = 0;
        private List<DmnGridFields> pagedatadata;

        public  int rowno = 0;

        public DateTime Dassigntime;
        public string Dempname;
        public string Ddesignation;
        public bool Fullpermissionflag = false;
        ClsGeneral clsGeneral = new ClsGeneral();
        private AutoLogoutManager autoLogout;
        public FrmTMpanel()
        {
            InitializeComponent();

            BranchId = Convert.ToInt32(FrmLogin.BranchId);
            UserId = Convert.ToInt32(FrmLogin.UserId);
            UserType = Convert.ToInt32(FrmLogin.UserAccountTypeId);
        }
        private void FrmTMpanel_Load(object sender, EventArgs e)
        {
            try
            {
                pageSize = Pagecount;
                FetchDrawerAssignTime();
                dtppicker_from.MinDate = Dassigntime.Date;
                dtppicker_from.Value = Dassigntime.Date;
                dtppicker_from_time.Value = Dassigntime;

                if (!Fullpermissionflag)
                {
                    dtppicker_from.Enabled = false;
                }
                combofill();
                FillData();
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  FrmTMpanel_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }

        }

        public void FillData()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch _objSearch = new DmnSearch();

                DateTime selectedDateTime = dtppicker_from.Value.Date + dtppicker_from_time.Value.TimeOfDay;
                DateTime fromDateTime = Fullpermissionflag ? selectedDateTime :
                    (selectedDateTime < Dassigntime ? Dassigntime : selectedDateTime);

                string dateCondition = $"AND EntryDate >= '{fromDateTime:yyyy-MM-dd HH:mm:ss}'";

                    _objSearch.Select = "sm.SalesMasterId as Field1, sm.EntryNo as Field2, sm.CreatedDateTime as Field3, " +
                                        "g.Caption as Field4, sm.TotalQuantity as Field5, sm.NetAmount as Field6,sm.Tid Field7,sm.PkId Field8, " +
                                        "sm.mDevice as Field9, " +
                                        "e.EmployeeName as Field10, sm.PaymentMode as Field11, 'POS Sale' as Field12, sm.CreatedBy as Field13, 5 AS Field14,1 AS Field15,t.TableCode AS Field16";

                    _objSearch.Table = "tblSalesMaster sm " +
                                       " LEFT JOIN tblEmployee e ON e.EmployeeId = sm.CreatedBy " +
                                       " LEFT JOIN tblGeneralOptions g ON sm.PaymentMode = g.GTypeSubId AND GTypeId = 2"+
                                       " LEFT JOIN tblTable_info t ON sm.Tid = t.Tid AND t.status=1";

                    _objSearch.WhereNdGroupBy = $"WHERE ISNULL(sm.Status, 0) = 1 AND sm.BranchId =  {BranchId}  {dateCondition}";
                    _objSearch.OrderBy = "ORDER BY sm.SalesMasterId DESC";
                
  

                if (Transactiondata != null) Transactiondata.Clear();
                if (pagedatadata != null) pagedatadata.Clear();

                currentPage = 1;
                pageSize = Pagecount;
                totalPages = 0;

                // Fetch data
                Transactiondata = bllSearch.GetSearchData("prdSearch", _objSearch);
                totalPages = (int)Math.Ceiling((double)Transactiondata.Count / pageSize);
                lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {Transactiondata.Count}";

                LoadPagedData();
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  FillData()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }
        
        }

        private void btn_closedetails_Click(object sender, EventArgs e)
        {
            pnl_viewdetails.Visible = false;
        }
        private void CenterPanelOnScreen(Panel panel)
        {
            panel.Location = new Point(this.ClientSize.Width / 2 - panel.Size.Width / 2, (this.ClientSize.Height / 3 - panel.Size.Height / 3));
        }

   
        public void FillAction(int rowno)
        {         

            try
            {
                if (rowno >= 0 && rowno < dgv_tmpanel.Rows.Count) 
                {
                    this.Invoke(new Action(() =>
                    {

             
                            lnlentry.Text = dgv_tmpanel.Rows[rowno].Cells[sinvno].Value?.ToString();
                            label1.Text = "Entry No";
                        
                        //lblactionheader.Text =" Invoice No :  "+ dgv_tmpanel.Rows[rowno].Cells[sinvno].Value?.ToString();
                        //lbltcprice.Text = dgv_tmpanel.Rows[rowno].Cells[snewcardpriceno].Value?.ToString();
                        //lblnewcardcount.Text = dgv_tmpanel.Rows[rowno].Cells[snewcardcountno].Value?.ToString();
                        lblnetamt.Text = dgv_tmpanel.Rows[rowno].Cells[stotalnetamtno].Value?.ToString();
                        lbltqty.Text = dgv_tmpanel.Rows[rowno].Cells[stotalqtyno].Value?.ToString();
                        lbldatetime.Text = dgv_tmpanel.Rows[rowno].Cells[sentrydateno].Value?.ToString();
                        lbl_ID.Text = dgv_tmpanel.Rows[rowno].Cells[ssalemateridno].Value?.ToString();
                        lblempid.Text = dgv_tmpanel.Rows[rowno].Cells[sempidno].Value?.ToString();
                        lblfrompayid.Text = dgv_tmpanel.Rows[rowno].Cells[spayiddno].Value?.ToString();
                        cmbpaymode.SelectedValue = Convert.ToInt64(dgv_tmpanel.Rows[rowno].Cells[spayiddno].Value);

                    }));
                    
                }

            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  FillAction()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }
        }

        private void btnactionclose_Click(object sender, EventArgs e)
        {
            try
            {
                txtremarks.Text = "";
                pnl_actionpanel.Visible = false;
                if (rowno % 2 == 0)
                {
                    dgv_tmpanel.Rows[rowno].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                }
                else
                {
                    dgv_tmpanel.Rows[rowno].DefaultCellStyle.BackColor = Color.Gainsboro;
                }
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  btnactionclose_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }
        }

        public void combofill()
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch_City = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "GTypeSubId id,Caption Value";
                objSearch.Table = " tblGeneralOptions";
                objSearch.WhereNdGroupBy = "where GTypeId=2";
                objSearch.OrderBy = "";
                var gridSource = bllSearch_City.GetSearchData("prdComboFill", objSearch);
                if (gridSource.Count > 0)
                {
                    cmbpaymode.DataSource = gridSource;
                    cmbpaymode.ValueMember = "Id";
                    cmbpaymode.DisplayMember = "Value";
                    cmbpaymode.SelectedIndex = 0;

                    cmbstype.DataSource = gridSource;
                    cmbstype.ValueMember = "Id";
                    cmbstype.DisplayMember = "Value";
                    cmbstype.SelectedIndex = 0;

                }
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  combofill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }
        }
        private void btn_can_details_Click(object sender, EventArgs e)
        {
            try
            {
                txtremarks.Text = "";
                pnl_actionpanel.Visible = false;
                if (rowno % 2 == 0)
                {
                    dgv_tmpanel.Rows[rowno].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                }
                else
                {
                    dgv_tmpanel.Rows[rowno].DefaultCellStyle.BackColor = Color.Gainsboro;
                }
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  btn_can_details_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblfrompayid.Text == cmbpaymode.SelectedValue.ToString())
                {
                    MessageBox.Show("No changes in payment mode to save.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtremarks.Text))
                {
                    MessageBox.Show("Please Enter Reason ", "Information ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtremarks.Focus();
                    return;
                }
                DialogResult result = MessageBox.Show("Are you sure you want to save this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DmnPaymentAdjustments dmnsalemaster = new DmnPaymentAdjustments();
                    BllSale bllSales = new BllSale();

                    dmnsalemaster.CardSalesMasterId = Convert.ToInt32(lbl_ID.Text);
                    dmnsalemaster.EntryNo = Convert.ToString(lnlentry.Text);
                    dmnsalemaster.EntryDate = Convert.ToDateTime(lbldatetime.Text);
                    dmnsalemaster.BranchId = Convert.ToInt32(BranchId);
                    dmnsalemaster.Status = 1;
                    dmnsalemaster.SaleEmpid = Convert.ToInt32(FrmDashboard.UserId);
                    dmnsalemaster.CreatedBy = FrmDashboard.UserId;
                    dmnsalemaster.Remark = Convert.ToString(txtremarks.Text);
                    dmnsalemaster.Frompayid = Convert.ToInt32(lblfrompayid.Text);
                    dmnsalemaster.SaleType = 1;
                    dmnsalemaster.PayId = Convert.ToInt32(cmbpaymode.SelectedValue);
                    dmnsalemaster.mDeviceIP = " [ " + System.Environment.MachineName + " ]  -" +
                        Dns.GetHostByName(System.Environment.MachineName).AddressList[0].ToString();

                    var InsertStatusP = bllSales.AddEditData_SalesMaster_paymentadj(
                        "prdInsert_Upd_SalesMaster_PaymentAdjustments",
                        dmnsalemaster);

                    if (InsertStatusP.CardSalesMasterId > 0)
                    {
                        FillData();

                        if (FrmDashboard.UserSavePrompt)
                            MessageBox.Show("Record saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        pnl_actionpanel.Visible = false;
                        txtremarks.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Operation cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }


                else
                {
                    MessageBox.Show("Operation cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  btn_submit_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }
        }

        public void FillDetails(int salesMasterId)
        {           
            Task.Run(() =>
            {
                try
                {
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch _objSearch = new DmnSearch();
                    _objSearch.Select = "ItemName as Field1, Quantity as Field2, NetAmount as Field3, ItemBarCode as Field5";
                    _objSearch.Table = "tblSalesDetails";
                    _objSearch.WhereNdGroupBy = "WHERE status=1 and SalesMasterId = " + salesMasterId;
                    _objSearch.OrderBy = "ORDER BY SalesDetailsId ASC";

                    var detailsSource = bllSearch.GetSearchData("prdSearch", _objSearch);

                    if (detailsSource.Count > 0)
                    {
                        this.Invoke(new Action(() =>
                        {
                            dgv_details.Rows.Clear();
                            dgv_details.Rows.Add(detailsSource.Count);

                            for (int i = 0; i < detailsSource.Count; i++)
                            {
                                dgv_details.Rows[i].Cells[sdslno].Value = i + 1;

                                string barcode = string.IsNullOrEmpty(detailsSource[i].Field5) ? detailsSource[i].Field4 : detailsSource[i].Field5;
                                dgv_details.Rows[i].Cells[sbarcodecardno].Value = barcode;

                                dgv_details.Rows[i].Cells[sdproductno].Value = detailsSource[i].Field1;
                                dgv_details.Rows[i].Cells[sdqtyno].Value = detailsSource[i].Field2;
                                dgv_details.Rows[i].Cells[sdnetamtno].Value = detailsSource[i].Field3;
                            }
                        }));
                    }
                }
                catch (Exception ex)
                {
                    clsGeneral.LogException("TransactionManagerPanel page  FillDetails()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
                }
            });
        }
        public class PlaceholderHelper
        {
            private const int EM_SETCUEBANNER = 0x1501;

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

            public static void SetPlaceholder(TextBox textBox, string placeholderText)
            {
                SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, placeholderText);
            }
        }
        private void cmbserachtype_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbserachtype.SelectedIndex != -1)
                {
                    txtSearchBox.Visible = false;

                    dtppicker_from.Visible = true;
                    dtppickerto.Visible = true;
                    dtppicker_from_time.Visible = true;
                    dtppickerto_time.Visible = true;
                    lblfrom.Visible = true;
                    lblto.Visible = true;

                    cmbstype.Visible = false;
                    btnsearch.Visible = true;
                    lblserach_caption.Visible = false;
                    int selectedIndex = cmbserachtype.SelectedIndex;

                    if (selectedIndex == 1) ///invoice No
                    {
                        txtSearchBox.Visible = true;
                        cmbstype.Visible = false;
                        PlaceholderHelper.SetPlaceholder(txtSearchBox, "Enter Invoice No");
                    }
                    else if (selectedIndex == 2) // Payment Type
                    {
                        txtSearchBox.Visible = false;
                        cmbstype.Visible = true;
                        lblserach_caption.Text = "Select Payment Type";
                        lblserach_caption.Visible = true;
                    }
                    else if (selectedIndex == 3) // Net Amount
                    {
                        txtSearchBox.Visible = true;
                        cmbstype.Visible = false;
                        PlaceholderHelper.SetPlaceholder(txtSearchBox, "Enter Net Amount");
                    }
                    else if (selectedIndex == 4) // Icard No
                    {
                        txtSearchBox.Visible = true;
                        cmbstype.Visible = false;
                        PlaceholderHelper.SetPlaceholder(txtSearchBox, "Enter Card No");
                    }
                    else if (selectedIndex == 5) //Net Amount  Payment Type
                    {
                        txtSearchBox.Visible = true;
                        cmbstype.Visible = false;
                        PlaceholderHelper.SetPlaceholder(txtSearchBox, "Enter Net Amount");

                        cmbstype.Visible = true;
                        lblserach_caption.Text = "Select Payment Type";
                        lblserach_caption.Visible = true;
                    }

                    else
                    {
                        lblserach_caption.Visible = false;
                        txtSearchBox.Visible = false;
                        cmbstype.Visible = false;
                        //MessageBox.Show("Invalid selection.");
                    }
                }
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  cmbserachtype_SelectionChangeCommitted()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }

        }

        public void FillData_Filter()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch _objSearch = new DmnSearch();
                string paytablename = "", nettablename = "";

                
                    _objSearch.Select = "sm.SalesMasterId as Field1, sm.EntryNo as Field2, sm.CreatedDateTime as Field3, " +
                                        "g.Caption as Field4, sm.TotalQuantity as Field5, sm.NetAmount as Field6,sm.Tid Field7,sm.PkId Field8," +
                                        "sm.mDevice as Field9,e.EmployeeName as Field10, sm.PaymentMode as Field11, 'POS Sale' AS Field12, sm.CreatedBy Field13, 5 AS Field14,1 AS Field15,t.TableCode AS Field16";
                    _objSearch.Table = "tblSalesMaster sm " +
                                       "LEFT JOIN tblEmployee e ON e.EmployeeId = sm.CreatedBy " +
                                       "LEFT JOIN tblGeneralOptions g ON sm.PaymentMode = g.GTypeSubId AND GTypeId = 2" +
                                   " LEFT JOIN tblTable_info t ON sm.Tid = t.Tid AND t.status=1";
                _objSearch.WhereNdGroupBy = "WHERE ISNULL(sm.Status, 0) = 1 AND sm.BranchId = " + BranchId;        

                DateTime selectedDateTime = dtppicker_from.Value.Date + dtppicker_from_time.Value.TimeOfDay;

                DateTime fromDateTime = Fullpermissionflag ? selectedDateTime :
                    (selectedDateTime < Dassigntime ? Dassigntime : selectedDateTime);

                DateTime toDateTime = dtppickerto.Value.Date + dtppickerto_time.Value.TimeOfDay;

                if (fromDateTime > toDateTime)
                {
                    MessageBox.Show("The 'From Date & Time' cannot be later than the 'To Date & Time'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtppicker_from.Focus();
                    return;
                }

                _objSearch.WhereNdGroupBy += $" AND EntryDate >= '{fromDateTime:yyyy-MM-dd HH:mm:ss}' " +
                                             $"AND EntryDate <= '{toDateTime:yyyy-MM-dd HH:mm:ss}' ";

                // Apply Search Type Filters
                if (cmbserachtype.SelectedIndex == 1 ) // Invoice No
                {
                    string invoiceNo = txtSearchBox.Text.Trim();
                    if (!string.IsNullOrEmpty(invoiceNo))
                        _objSearch.WhereNdGroupBy += $" AND EntryNo LIKE '%{invoiceNo}%'";
                    else
                    {
                        MessageBox.Show("Please enter an Invoice No to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSearchBox.Focus();
                        return;
                    }
                }
                else if (cmbserachtype.SelectedIndex == 2) // Payment Type
                {
                    if (cmbstype.SelectedValue == null)
                    {
                        MessageBox.Show("Please select a valid Payment Type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbstype.Focus();
                        return;
                    }

                    int paymode = Convert.ToInt32(cmbstype.SelectedValue);
                    _objSearch.WhereNdGroupBy += $" AND sm.PaymentMode = {paymode}";
                }
                else if (cmbserachtype.SelectedIndex == 3) // Net Amount
                {
                    string netamt = txtSearchBox.Text.Trim();
                    if (!string.IsNullOrEmpty(netamt) && decimal.TryParse(netamt, out decimal netAmount))
                        _objSearch.WhereNdGroupBy += $" AND NetAmount = {netAmount}";
                    else
                    {
                        MessageBox.Show("Please enter a valid Net Amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSearchBox.Focus();
                        return;
                    }
                }
                else if (cmbserachtype.SelectedIndex == 4) // tablocde
                {
                    string icardNo = txtSearchBox.Text.Trim();
                    if (!string.IsNullOrEmpty(icardNo))
                    {

                        _objSearch.Table += " INNER JOIN tblSalesDetails sd ON sm.SalesMasterId = sd.SalesMasterId and sd.status = 1";
                        _objSearch.WhereNdGroupBy += $" AND t.tablecode LIKE '%{icardNo}%'";

                    }
                    else
                    {
                        MessageBox.Show("Please enter an table code to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSearchBox.Focus();
                        return;
                    }
                }
                else if (cmbserachtype.SelectedIndex == 5) // pickup no
                {
                    string icardNo = txtSearchBox.Text.Trim();
                    if (!string.IsNullOrEmpty(icardNo))
                    {

                        _objSearch.Table += " INNER JOIN tblSalesDetails sd ON sm.SalesMasterId = sd.SalesMasterId and sd.status = 1";
                        _objSearch.WhereNdGroupBy += $" AND sm.pkid LIKE '%{icardNo}%'";

                    }
                    else
                    {
                        MessageBox.Show("Please enter an pick up No to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSearchBox.Focus();
                        return;
                    }
                }
                else if (cmbserachtype.SelectedIndex == 6) // Net Amount Payment
                {
                    string netamt = txtSearchBox.Text.Trim();
                    if (string.IsNullOrEmpty(netamt))
                    {
                        MessageBox.Show("Please enter a valid Net Amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSearchBox.Focus();
                        return;
                    }
                    if (cmbstype.SelectedValue == null || Convert.ToInt32(cmbstype.SelectedIndex) == 0)
                    {
                        MessageBox.Show("Please select a valid Payment Type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbstype.Focus();
                        return;
                    }

                    int paymode = Convert.ToInt32(cmbstype.SelectedValue);
                 
                        _objSearch.WhereNdGroupBy += $" AND sm.PaymentMode = {paymode}";
                        _objSearch.WhereNdGroupBy += $" AND NetAmount = {netamt}";
                   
          

                }
                else
                {

                    if (Fullpermissionflag)
                    {
                  
                            _objSearch.WhereNdGroupBy = $"WHERE ISNULL(sm.Status, 0) = 1 AND sm.BranchId = {BranchId} " +
                                                        $"AND EntryDate >= '{selectedDateTime:yyyy-MM-dd HH:mm:ss}' " +
                                                        $"AND EntryDate <= '{toDateTime:yyyy-MM-dd HH:mm:ss}'";
              
                    }
                    else 
                    {
                        DateTime maxAllowedDateTime = DateTime.Now; 

                            _objSearch.WhereNdGroupBy = $"WHERE ISNULL(sm.Status, 0) = 1 AND sm.BranchId = {BranchId} " +
                                                        $"AND EntryDate >= '{selectedDateTime:yyyy-MM-dd HH:mm:ss}' " +
                                                        $"AND EntryDate <= '{maxAllowedDateTime:yyyy-MM-dd HH:mm:ss}'";
                       
                    }
                }
                // Fetch and bind results
                Transactiondata = bllSearch.GetSearchData("prdSearch", _objSearch);
                totalPages = (int)Math.Ceiling((double)Transactiondata.Count / pageSize);
                LoadPagedData();
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  FillData_Filter()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }

        }
        private void LoadPagedData()
        {
            try
            {
                if (Transactiondata == null || Transactiondata.Count == 0)
                {
                    MessageBox.Show("No records found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // Apply pagination
                pagedatadata = Transactiondata.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {Transactiondata.Count}";

                dgv_tmpanel.Rows.Clear();
                dgv_tmpanel.Rows.Add(pagedatadata.Count);

                for (int i = 0; i < pagedatadata.Count; i++)
                {
                    dgv_tmpanel.Rows[i].Cells[sslno].Value = (currentPage - 1) * pageSize + i + 1; 
                    dgv_tmpanel.Rows[i].Cells[sinvno].Value = pagedatadata[i].Field2;                    
                    dgv_tmpanel.Rows[i].Cells[sentrydateno].Value = pagedatadata[i].Field3;
                    dgv_tmpanel.Rows[i].Cells[spaymodeno].Value = pagedatadata[i].Field4;
                    dgv_tmpanel.Rows[i].Cells[stotalqtyno].Value = pagedatadata[i].Field5;
                    dgv_tmpanel.Rows[i].Cells[stotalnetamtno].Value = pagedatadata[i].Field6;
                    dgv_tmpanel.Rows[i].Cells[sdeviceo].Value = pagedatadata[i].Field9;
                    dgv_tmpanel.Rows[i].Cells[semployenameno].Value = pagedatadata[i].Field10;
                    dgv_tmpanel.Rows[i].Cells[ssalemateridno].Value = pagedatadata[i].Field1;
                    dgv_tmpanel.Rows[i].Cells[spayiddno].Value = pagedatadata[i].Field11;
                    dgv_tmpanel.Rows[i].Cells[stranstypeno].Value = pagedatadata[i].Field12;

                    dgv_tmpanel.Rows[i].Cells[stidno].Value = pagedatadata[i].Field7;
                    dgv_tmpanel.Rows[i].Cells[spkidno].Value = pagedatadata[i].Field8;

                    var data = pagedatadata[i];

                    int field7 = Convert.ToInt32(data.Field7);
                    int field8 = Convert.ToInt32(data.Field8);
                    int field1 = Convert.ToInt32(data.Field1);

                    dgv_tmpanel.Rows[i].Cells[stidpkikno].Value =
                        field7 > 0 ? data.Field16 :
                        field8 > 0 ? field8.ToString() :
                        field1.ToString();

                    dgv_tmpanel.Rows[i].Cells[sviewmoreno].Value = "View More";

                }

                // Enable/Disable Pagination Buttons
                btn_previous.Enabled = currentPage > 1;
                btn_next.Enabled = currentPage < totalPages;
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  LoadPagedData()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }            
        }


        private void btnsearch_Click(object sender, EventArgs e)
        {
            FillData_Filter();
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    LoadPagedData();
                    lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {Transactiondata.Count}";
                }
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  btn_previous_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }

        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPage < totalPages)
                {
                    currentPage++;
                    LoadPagedData();
                    lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {Transactiondata.Count}";
                }
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  btn_next_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }
        }


        private void rb_pos_CheckedChanged(object sender, EventArgs e)
        {
         FillData();            
        }

        private void rb_reg_CheckedChanged(object sender, EventArgs e)
        {
                FillData();
        }

        private void txtSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                btnsearch.PerformClick();
            }
        }

        private void dgv_tmpanel_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0) // Ensure it's not the header row
                {
                    if (e.ColumnIndex == sviewmoreno) // View More button clicked
                    {
                        CenterPanelOnScreen(pnl_viewdetails);
                        pnl_viewdetails.Visible = true;
                        pnl_viewdetails.BringToFront();
                        FillDetails(Convert.ToInt32(dgv_tmpanel.Rows[e.RowIndex].Cells[ssalemateridno].Value));
                    }
                    //else if (e.ColumnIndex == sactionno) // Action button clicked
                    //{
                    //    CenterPanelOnScreen(pnl_actionpanel);
                    //    FillAction(Convert.ToInt32(e.RowIndex));
                    //    pnl_actionpanel.Visible = true;
                    //    pnl_actionpanel.BringToFront();
                    //}
                }
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  dgv_tmpanel_CellDoubleClick_1()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }  

        }

        private void dgv_tmpanel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgv_tmpanel.Rows[e.RowIndex];
                if ((e.RowIndex >= 0) && (e.ColumnIndex == sactionno))
                {
                    
                    var saleTypeValue = Convert.ToInt32(row.Cells[ssaletype].Value);
                    var salereturnValue = Convert.ToInt32(row.Cells[ssalemateridno].Value);
                    //SaleType billType = (SaleType)saleTypeValue;
                    //if (ttype == 2)
                    //{
                    //    using (var frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(
                    //               salereturnValue,
                    //               (int)billType,
                    //               UserType,false))
                    //    {
                    //        frmSalesInvoice.ShowDialog();
                    //    }
                    //}
                    //else
                    //{
                        

                    //    using (var frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(
                    //               salereturnValue,
                    //               (int)billType,
                    //               UserType,false))
                    //    {
                    //        frmSalesInvoice.ShowDialog();
                    //    }
                    //}
                }
                if ((e.RowIndex >= 0) && (e.ColumnIndex != sviewmoreno) && (e.ColumnIndex != sactionno))
                {
                    CenterPanelOnScreen(pnl_actionpanel);
                    txtremarks.Text = "";
                    FillAction(Convert.ToInt32(e.RowIndex));
                    pnl_actionpanel.Visible = true;
                    pnl_actionpanel.BringToFront();
                    rowno = e.RowIndex;
                    dgv_tmpanel.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DodgerBlue;
                }
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  dgv_tmpanel_CellContentClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }      

        }

        public void FetchDrawerAssignTime()
        {
            try
            {
                BllSearch<DmnGridFields> bllsearchdrawer = new BllSearch<DmnGridFields>();
                DmnSearch dmnsearckdrawer = new DmnSearch();

                dmnsearckdrawer.Select = "t.CreatedDateTime StartDate, e.EmployeeName as Field2, d.DesignationName as Field3";
                dmnsearckdrawer.Table = "tblDrawerAssign t " +
                                        "LEFT JOIN tblUserAccount u ON u.EmployeeId = t.EmployeeId " +
                                        "LEFT JOIN tblEmployee e ON e.EmployeeId = t.EmployeeId " +
                                        "LEFT JOIN tblDesignation d ON d.DesignationId = e.DesignationId";
                dmnsearckdrawer.WhereNdGroupBy = "WHERE t.DName = '" + System.Environment.MachineName + "' AND t.BranchId = " + BranchId + " AND t.Status = 1";
                dmnsearckdrawer.OrderBy = "";

                var gridesource = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckdrawer);

                if (gridesource.Count > 0)
                {
                    var row = gridesource[0];
                    string ds = gridesource[0].StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                    Dassigntime = Convert.ToDateTime(ds);
                    Dempname = Convert.ToString(row.Field2);
                    Ddesignation = Convert.ToString(row.Field3);
                }
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  FetchDrawerAssignTime()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }    
        }

        private void btnsetting_Click(object sender, EventArgs e)
        {
            try
            {
                CenterPanelOnScreen(pnl_settinglogin);

                if (pnl_settinglogin.Visible)
                    pnl_settinglogin.Visible = false;
                else
                    pnl_settinglogin.Visible = true;
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  btnsetting_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
      
            try
            {
                string username = txtUserName.Text.Trim();
                string password = txtPwd.Text.Trim();

                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Please enter username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUserName.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPwd.Focus();
                    return;
                }
                string validUser = ConfigurationManager.AppSettings["ValidUser"];
                string validPass = ConfigurationManager.AppSettings["ValidPass"];

                if (username == validUser && password == "123")
                {
                    Fullpermissionflag = true;
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtppicker_from.MinDate = DateTimePicker.MinimumDateTime;

                    dtppicker_from.Enabled = true;

                    pnl_settinglogin.Visible = false;
                    btnlogout.Visible = true;
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPwd.Clear();
                    txtPwd.Focus();
                }
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  btnLogin_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }
            
        }


        private void btnclose_login_Click(object sender, EventArgs e)
        {
            pnl_settinglogin.Visible = false;
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            try
            {
                txtUserName.Text = "";
                txtPwd.Text = "";
                Fullpermissionflag = false;
                btnlogout.Visible = false;
                pnl_settinglogin.Visible = false;

                dtppicker_from.MinDate = Dassigntime.Date;
                dtppicker_from.Value = Dassigntime.Date;
                dtppicker_from_time.Value = Dassigntime;

                if (!Fullpermissionflag)
                {
                    dtppicker_from.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  btnlogout_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }          
        }

        private void dtppicker_from_time_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTimePicker dtp = sender as DateTimePicker;
                if (dtp.Value.TimeOfDay < Dassigntime.TimeOfDay)
                {
                    MessageBox.Show("You cannot select a time earlier than the drawer assigned time.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtp.Value = Dassigntime;
                }
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  dtppicker_from_time_ValueChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }       
        }

        private void txtSearchBox_DoubleClick(object sender, EventArgs e)
        {
            Frmkeypad keypad = new Frmkeypad(this, 0);
            keypad.StartPosition = FormStartPosition.CenterScreen;
            keypad.ShowDialog();
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
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsGeneral.LogException("transactionManagerPanel page  RemoveSpecialCharacters()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
                return string.Empty;
            }
        }
        private void txtSearchBox_Enter(object sender, EventArgs e)
        {
            try
            {
                if (cmbserachtype.SelectedIndex == 4) // cardno
                {
                    timer1.Start();
                }
                else
                {
                    if (timer1.Enabled)
                        timer1.Stop();
                }

            }
            catch (Exception ex)
            {
               clsGeneral.LogException("TransactionManagerPanel page  txtSearchBox_Enter()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }
        }

        private void txtSearchBox_Leave(object sender, EventArgs e)
        {
            if (cmbserachtype.SelectedIndex != 4) // cardno
            {
                if (timer1.Enabled)
                    timer1.Stop();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (cmbserachtype.SelectedIndex == 4) // cardno
                {
                    string str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul(FrmLogin.CorpID);
                    if (str1 != "Card not in contact")
                    {
                        if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Invalid Card")

                            str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi(FrmLogin.CorpID);
                        timer1.Stop();
                        str1 = RemoveSpecialCharacters(str1.Trim());
                        if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "InvalidCard")
                        {
                            MessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        txtSearchBox.Text = str1;
                        FillData_Filter();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGeneral.LogException("TransactionManagerPanel page  timer1_Tick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.transactionManagerPanel));
            }
                       
        }

        private void FrmTMpanel_FormClosing(object sender, FormClosingEventArgs e)
          {             
            if (timer1.Enabled)
            { 
            
                timer1.Stop();
                timer1.Dispose();
            }

            //if (pbwifi.Image != null)
            //{
            //    pbwifi.Image.Dispose();
            //    pbwifi.Image = null;
            //}
  
        }


        #region Keyboard
        public void ShowKeyboard(TextBox activeTextBox)
        {
            if (activeTextBox == null) return;

            Point textBoxScreenLocation = activeTextBox.PointToScreen(Point.Empty);
            int desiredX = 0, desiredY = 0;
            if (Frmkeyboard.IsOpen)
            {
                Frmkeyboard.Instance.Close();
            }

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
        #endregion

        private void txtSearchBox_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_TablePickUp_Normal)
            {
                ShowKeyboard(sender as TextBox);
            }
        }

        private void txtremarks_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_TablePickUp_Normal)
            {
                ShowKeyboard(sender as TextBox);
            }
        }

        private void txtUserName_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_TablePickUp_Normal)
            {
                ShowKeyboard(sender as TextBox);
            }
        }

        private void txtPwd_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_TablePickUp_Normal)
            {
                ShowKeyboard(sender as TextBox);
            }
        }
    }
}
