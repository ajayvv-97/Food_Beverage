using BLL;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.RegularExpressions;
using FoodandBeverage.Forms.UserAccount;
using FoodandBeverage.Interfaces;
using System.Drawing.Drawing2D;


using System.Runtime.InteropServices;
using CustomMessageBox;

namespace FoodandBeverage.Forms
{
    public partial class FrmRegistration : Form
    {
        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsgen = new ClsGeneral();
        FilterInfoCollection FilterInfoCollection;
        VideoCaptureDevice VideoCaptureDevice;
        public string Compressedimg = Application.StartupPath + "\\CompressedImg";
        long BranchId = FrmDashboard.BranchId;
        long UserId = Forms.FrmLogin.UserId;
        public int VideoCaptureDevice_NewFrame { get; private set; }
        public static string empImgExt = "";

        public int Id = 0;
        public FrmRegistration()
        {
            InitializeComponent();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
          int nLeftRect,
          int nTopRect,
          int nRightRect,
          int nBottomRect,
          int nWidthEllipse,
          int nHeightEllipse
      );
        private void FrmRegistration_Load(object sender, EventArgs e)
        {
            try
            {
                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    AppSession.RegisterForm(this, (int)LogoutFormList.operation);
                }
                panel1.Visible = false;
                ActiveControl = txtname;
                txtname.Focus();
                FillCombo();
                filldetails();
                FrmDashboard frmDash = new FrmDashboard();
                if (frmDash.ChkWritePermission(Convert.ToInt32(formsenum.Company)))
                {
                    btsave.Enabled = true;
                }
                else
                {
                    btsave.Enabled = false;
                }
                if(FrmLogin.TestUser)
                    btsave.Enabled = true;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page FrmRegistration_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }
        }
        private bool Isvalid()
        {
            try
            {
                string urlPattern = @"^(https?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
                string website = txtwebsite.Text.Trim();
                if (string.IsNullOrWhiteSpace(txtname.Text.Trim()))
                {
                    CMessageBox.Show("Company name must be specified. Please enter the required information.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtname.Focus();
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(txtph.Text.Trim()))
                {
                    CMessageBox.Show("Phone number must be specified. Please enter the required information.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtph.Focus();
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(txtpin.Text.Trim()))
                {
                    CMessageBox.Show("Pincode must be specified. Please enter the required information.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtpin.Focus();
                    return false;
                }
                else if (cmbcountry.SelectedIndex == 0)
                {
                    CMessageBox.Show("Country must be selected. Please select a country.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbcountry.Focus();
                    return false;
                }
                else if (cmdstate.SelectedIndex == 0)
                {
                    CMessageBox.Show("State must be selected. Please select a state.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmdstate.Focus();
                    return false;
                }
                else if (cmdcity.SelectedIndex == 0 && string.IsNullOrWhiteSpace(cmdcity.Text.Trim()))
                {
                    CMessageBox.Show("City must be selected. Please select a city.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmdcity.Focus();
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(txtg.Text.Trim()))
                {
                    CMessageBox.Show("Field 'GST NO' must be specified. Please enter the required information.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtg.Focus();
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(txtaddress.Text.Trim()))
                {
                    CMessageBox.Show("Address must be specified. Please enter the required information.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtaddress.Focus();
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(tbl_BookingIn.Text.Trim()))
                {
                    CMessageBox.Show("Booking Interval must be specified. Please enter the required information.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbl_BookingIn.Focus();
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(txtemail.Text.Trim()))
                {
                    CMessageBox.Show("Email must be specified. Please enter the required information.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtemail.Focus();
                    return false;
                }
                //else if(string.IsNullOrWhiteSpace(txtwebsite.Text.Trim()))
                //{
                //    CMessageBox.Show("Website must be specified. Please enter the required information.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtwebsite.Focus();
                //    return false;
                //}
                else if (txtwebsite.Text.Trim() != "")
                {
                    if (!Regex.IsMatch(website, urlPattern, RegexOptions.IgnoreCase))
                        {
                            CMessageBox.Show("Please enter a valid website URL.", "Invalid Website", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtwebsite.Focus();
                            return false;
                        }
                }
                // Add more checks if there are more fields to validate
                return true;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page Isvalid()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
                return false;
            }
        }



        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtname, txtaddress, txtph, txtpin, txtg, tbl_BookingIn, txtemail, txtwebsite })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }
        }

        private void ApplyRoundedTextBox(TextBox textBox, int radius)
        {
            try
            {
                // Create a new GraphicsPath object
                GraphicsPath path = new GraphicsPath();
                // Define the shape of the path using a rounded rectangle
                Rectangle rect = new Rectangle(0, 0, textBox.Width, textBox.Height);
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                // Set the Region property of the textbox to the new path
                textBox.Region = new Region(path);

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }
           
        }

        #endregion
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Isvalid() == false)
                    return;

                DmnHRegistration dmnRegistration = new DmnHRegistration();
                BllHotelRegistration bllregistration = new BllHotelRegistration();
                dmnRegistration.Name = Convert.ToString(txtname.Text);
                dmnRegistration.Address = Convert.ToString(txtaddress.Text);
                dmnRegistration.Phone = Convert.ToString(txtph.Text);
                if (string.IsNullOrWhiteSpace(txtpin.Text))
                    txtpin.Text = "0";
                dmnRegistration.ZipCode = Convert.ToInt32(txtpin.Text);
                dmnRegistration.GstNo = Convert.ToString(txtg.Text);
                dmnRegistration.Country = Convert.ToString(cmbcountry.SelectedValue);
                dmnRegistration.State = Convert.ToString(cmdstate.SelectedValue);
                dmnRegistration.City = Convert.ToString(cmdcity.SelectedValue);
               
                dmnRegistration.CreatedBy = FrmDashboard.BranchId;
                dmnRegistration.BranchId = FrmDashboard.BranchId;
                dmnRegistration.Status = 1;
                var InsertStatus = bllregistration.AddEditData("prdInsert_Upd_HotelReg", dmnRegistration);
                if (InsertStatus.Id != 0)
                {
                    if (InsertStatus.Id > 0)
                    {
                        if (pb1.BackgroundImage != null)
                        {

                            Bitmap bm = new Bitmap(pb1.BackgroundImage);
                            //clsDocument.UploadImage(bm, InsertStatus.Id + ".jpeg", DocumentType.HotelRegistrationProofPics);
                        }
                        if (pb2.BackgroundImage != null)
                        {
                            Bitmap bm2 = new Bitmap(pb2.BackgroundImage);
                            //clsDocument.UploadImage(bm2, InsertStatus.Id + "_2.jpeg", DocumentType.HotelRegistrationProofPics);
                        }

                        if (pb3.BackgroundImage != null)
                        {
                            Bitmap bm3 = new Bitmap(pb3.BackgroundImage);
                            //clsDocument.UploadImage(bm3, InsertStatus.Id + "_3.jpeg", DocumentType.HotelRegistrationProofPics);
                        }
                        else
                        {
                            bool imageExist = clsDocument.FileExistOrNot(InsertStatus.Id + ".jpeg", DocumentType.HotelRegistrationProofPics);
                            if (imageExist)
                                clsDocument.DeleteImage(InsertStatus.Id + ".jpeg", DocumentType.HotelRegistrationProofPics);
                            InsertStatus.Photo = "";
                            InsertStatus.Photo = "";
                        }
                        CMessageBox.Show(this, txtname.Text + "  ", "Detail Successfully Updated", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        this.Close();
                    }
                    else
                        CMessageBox.Show("Successfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    Refreshform();
                }
                else
                {
                    CMessageBox.Show(this, " ", " Save Failed  ", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page btnSave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }
        }
        private void Refreshform()
        {
            try
            {
                if (txtcid.Text != null && txtcid.Text != "")
                {
                    filldetails();
                }
                else
                {
                    txtname.Text = "";
                    txtaddress.Text = "";

                    txtpin.Text = "";
                    txtph.Text = "";
                    
                    txtg.Text = "";
                    txtemail.Text = "";
                    txtwebsite.Text = "";
                    tbl_BookingIn.Text = "";
                    cmbcountry.Text = "select";
                    cmdstate.Text = "select";
                    cmdcity.Text = "select";

                    pb1.BackgroundImage = null;
                    pb1.BackgroundImage = FoodandBeverage.Properties.Resources.reg_certificate_icon;
                    pb2.BackgroundImage = null;
                    pb2.BackgroundImage = FoodandBeverage.Properties.Resources.reg_certificate_icon;
                    pb3.BackgroundImage = null;
                    pb3.BackgroundImage = FoodandBeverage.Properties.Resources.reg_certificate_icon;
                    FillCombo();
                }

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page Refreshform()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }
            
        }
        private void getNewAccNo()
        {
            int sel = 143;
        }
        public void FillCombo()
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch_Country = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "GTypeSubId,Caption";
                objSearch.Table = " tblGeneralOptions";
                objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=42";
                objSearch.OrderBy = "order by value";
                var gridSource = bllSearch_Country.GetSearchData("prdComboFill", objSearch);
                cmbcountry.DataSource = gridSource;
                cmbcountry.ValueMember = "Id";
                cmbcountry.DisplayMember = "Value";
                cmbcountry.SelectedIndex = 0;

                // ============state

                BllSearch<DmnComboBox> bllSearch_State = new BllSearch<DmnComboBox>();
                objSearch = new DmnSearch();
                objSearch.Select = "GTypeSubId,Caption";
                objSearch.Table = " tblGeneralOptions";
                objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=43";
                objSearch.OrderBy = "order by value";
                gridSource = bllSearch_State.GetSearchData("prdComboFill", objSearch);
                cmdstate.DataSource = gridSource;
                cmdstate.ValueMember = "Id";
                cmdstate.DisplayMember = "Value";
                cmdstate.SelectedIndex = 0;

                //============= City

                BllSearch<DmnComboBox> bllSearch_City = new BllSearch<DmnComboBox>();
                objSearch = new DmnSearch();
                objSearch.Select = "GTypeSubId Id,Caption Value";
                objSearch.Table = " tblGeneralOptions";
                objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=44";
                objSearch.OrderBy = "order by value";
                gridSource = bllSearch_City.GetSearchData("prdComboFill", objSearch);
                cmdcity.DataSource = gridSource;
                cmdcity.ValueMember = "Id";
                cmdcity.DisplayMember = "Value";
                cmdcity.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page FillCombo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refreshform();
        }

        private void txtAccName_DoubleClick(object sender, EventArgs e)
        {
            LedgerPopup();
        }
        public void LedgerPopup()
        {
            try
            {
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Name,Account no,Mobile,Email,Address,Valid To";
                _objSearch.Select = "L.LedgerId as Id,L.LedgerName as Field1,LedgerCode as Field2,L.Mobile as Field3,L.EmailId as Field4, L.LedgerAddress as Field5, L.vto as Field6";
                _objSearch.Table = " tblCustomer L LEFT JOIN tblAccountGroup Ac  ON L.AccountGroupId = Ac.AccountGroupId";
                _objSearch.WhereNdGroupBy = " WHERE isnull(L.Status,0) = 1  and  isnull(L.cardstatus,0)=1 and L.BranchId = " + FrmLogin.BranchId;
                _objSearch.OrderBy = " ORDER BY L.LedgerName ASC";
                if (returnValue.ToString() != "")
                {
                    txtcid.Text = Convert.ToString(returnValue);
                    Id = Convert.ToInt32(txtcid.Text);
                    BllSearch<DmnCustAccount> bllSearch = new BllSearch<DmnCustAccount>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = " tblCustomer ";
                    objSearch.WhereNdGroupBy = "Where LedgerId = " + Id;
                    objSearch.OrderBy = "Order By LedgerName ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    txtname.Text = Convert.ToString(gridSource[0].LedgerName);
                    txtaddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                    txtpin.Text = gridSource[0].ZipCode.ToString();
                    txtph.Text = gridSource[0].Mobile;
                    int i = gridSource[0].AllowSMS;
                    cmbcountry.SelectedValue = Convert.ToInt64(gridSource[0].cCountry);//change
                    cmdstate.SelectedValue = Convert.ToInt64(gridSource[0].State);//change
                    cmdcity.Text = gridSource[0].City;
                    var imageExist = clsDocument.FileExistOrNot(Id.ToString() + ".jpeg", DocumentType.CustomerPhoto);
                    if (imageExist)
                        pb1.BackgroundImage = clsDocument.DownloadImage(Id.ToString() + ".jpeg", DocumentType.CustomerPhoto);
                    pb1.BackgroundImageLayout = ImageLayout.Stretch;

                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page LedgerPopup()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }

        }

        private void txtAccName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAccAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtAccPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void cboCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }
        public void Insert_Combo1(string caption, int GtypeId)
        {
            try
            {
                DmnGeneralOptions dmnGen = new DmnGeneralOptions();
                BllGeneralOptions bllGen = new BllGeneralOptions();

                dmnGen.Caption = caption;
                dmnGen.GTypeId = GtypeId;

                bool insertStatus = bllGen.AddComboData("prdComboFillCountry", dmnGen);

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page Insert_Combo1()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }


        }

        public void Insert_Combo2(string caption, int GtypeId)
        {
            try
            {
                DmnGeneralOptions dmnGen = new DmnGeneralOptions();
                BllGeneralOptions bllGen = new BllGeneralOptions();

                dmnGen.Caption = caption;
                dmnGen.GTypeId = GtypeId;

                bool insertStatus = bllGen.AddComboData("prdComboFillCountry", dmnGen);

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page Insert_Combo2()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }


        }


        public void Insert_Combo3(string caption, int GtypeId)
        {
            try
            {
                DmnGeneralOptions dmnGen = new DmnGeneralOptions();
                BllGeneralOptions bllGen = new BllGeneralOptions();

                dmnGen.Caption = caption;
                dmnGen.GTypeId = GtypeId;

                bool insertStatus = bllGen.AddComboData("prdComboFillCountry", dmnGen);

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page Insert_Combo3()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
                        
            }
        }
        private void cboCountry_Leave(object sender, EventArgs e)
        {
            if (cmbcountry.Text != null || cmbcountry.Text != "")

                Insert_Combo1(cmbcountry.Text.Trim().ToUpper(), 42);
        }

        private void cboState_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cboState_Leave(object sender, EventArgs e)
        {
            if (cboState.Text != null || cboState.Text != "")

                Insert_Combo2(cboState.Text.Trim().ToUpper(), 43);
        }

        private void cboCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cboCity_Leave(object sender, EventArgs e)
        {
            if (cboCity.Text != null || cboCity.Text != "")

                Insert_Combo3(cboCity.Text.Trim().ToUpper(), 43);
        }

        private void txtZipcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtgst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtcerti1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtcerti2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtcerti3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtAccName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAccName.Text) == false)
            {
                getNewAccNo();
            }
        }

        private void txtAccName_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }

            if (e.KeyCode == Keys.F8)
                LedgerPopup();
        }

        private void txtAccName_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (txtAccName.Text.Length <= 0) return;
                string s = txtAccName.Text.Substring(0, 1);
                if (s != s.ToUpper())
                {
                    int curSelStart = txtAccName.SelectionStart;
                    int curSelLength = txtAccName.SelectionLength;
                    txtAccName.SelectionStart = 0;
                    txtAccName.SelectionLength = 1;
                    txtAccName.SelectedText = s.ToUpper();
                    txtAccName.SelectionStart = curSelStart;
                    txtAccName.SelectionLength = curSelLength;
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page txtAccName_KeyPress()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }

        }  


        

        public void filldetails()
        {
            try
            {
                DmnSearch dmnSearch = new DmnSearch();
                BllSearch<DmnHRegistration> _bllSearch = new BllSearch<DmnHRegistration>();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblHotelRegistration";
                dmnSearch.WhereNdGroupBy = " Where id =" + FrmLogin.BranchId;
                dmnSearch.OrderBy = "";
                var gridSource1 = _bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource1.Count > 0)
                {
                    txtcid.Text = Convert.ToString(gridSource1[0].Id);
                    txtname.Text = Convert.ToString(gridSource1[0].Name);
                    txtaddress.Text = Convert.ToString(gridSource1[0].Address);
                    txtph.Text = Convert.ToString(gridSource1[0].Phone);
                    txtpin.Text = Convert.ToString(gridSource1[0].ZipCode);
                    txtg.Text = Convert.ToString(gridSource1[0].GstNo);
                    txtemail.Text = Convert.ToString(gridSource1[0].Email);
                    txtwebsite.Text = Convert.ToString(gridSource1[0].website);
                 
                    tbl_BookingIn.Text = Convert.ToString(gridSource1[0].BookingInerwell);
                    cmbcountry.SelectedValue = Convert.ToInt64(gridSource1[0].Country);
                    cmdstate.SelectedValue = Convert.ToInt64(gridSource1[0].State);
                    cmdcity.Text = Convert.ToString(gridSource1[0].City);
                    bool imageExist = clsDocument.FileExistOrNot(gridSource1[0].Id + ".jpeg", DocumentType.HotelRegistrationProofPics);
                    if (imageExist)
                    {
                        pb1.BackgroundImage = clsDocument.DownloadImage(gridSource1[0].Id + ".jpeg", DocumentType.HotelRegistrationProofPics);
                        pb1.BackgroundImageLayout = ImageLayout.Stretch;
                        pb1.Image = null;
                    }
                    if (imageExist)
                    {
                        pb2.BackgroundImage = clsDocument.DownloadImage(gridSource1[0].Id + "_2.jpeg", DocumentType.HotelRegistrationProofPics);
                        pb2.BackgroundImageLayout = ImageLayout.Stretch;
                        pb2.Image = null;
                    }
                    if (imageExist)
                    {
                        pb3.BackgroundImage = clsDocument.DownloadImage(gridSource1[0].Id + "_3.jpeg", DocumentType.HotelRegistrationProofPics);
                        pb3.BackgroundImageLayout = ImageLayout.Stretch;
                        pb3.Image = null;
                    }

                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page filldetails()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }
        }
        private void txtZipcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void txtname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SendKeys.Send("{TAB}");
                }

                if (e.KeyCode == Keys.F8)
                    LedgerPopup();
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page txtname_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtname.Text.Length <= 0) return;
                string s = txtname.Text.Substring(0, 1);
                if (s != s.ToUpper())
                {
                    int curSelStart = txtname.SelectionStart;
                    int curSelLength = txtname.SelectionLength;
                    txtname.SelectionStart = 0;
                    txtname.SelectionLength = 1;
                    txtname.SelectedText = s.ToUpper();
                    txtname.SelectionStart = curSelStart;
                    txtname.SelectionLength = curSelLength;
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page txtname_KeyPress()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }



        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtname.Text) == false)
            {
                getNewAccNo();
            }
        }

        private void txtaddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtph_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtpin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtpin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void certi1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void certi2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void certi3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void comboBox3_Leave(object sender, EventArgs e)
        {
            if (cmbcountry.Text != null || cmbcountry.Text != "")

                Insert_Combo1(cmbcountry.Text.Trim().ToUpper(), 42);
        }

        private void cmdstate_Leave(object sender, EventArgs e)
        {
            if (cmdstate.Text != null || cmdstate.Text != "")

                Insert_Combo2(cmdstate.Text.Trim().ToUpper(), 43);
        }

        private void cmdcity_Leave(object sender, EventArgs e)
        {
            if (cmdcity.Text != null || cmdcity.Text != "")

                Insert_Combo3(cmdcity.Text.Trim().ToUpper(), 43);
        }



        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Isvalid() == false)
                    return;
                DmnHRegistration dmnRegistration = new DmnHRegistration();
                BllHotelRegistration bllregistration = new BllHotelRegistration();
                if (string.IsNullOrWhiteSpace(txtcid.Text))
                    txtcid.Text = "0";
                dmnRegistration.Id = Convert.ToInt32(txtcid.Text);
                dmnRegistration.Name = Convert.ToString(txtname.Text);
                dmnRegistration.Address = Convert.ToString(txtaddress.Text);
                dmnRegistration.Phone = Convert.ToString(txtph.Text);
                dmnRegistration.Email = Convert.ToString(txtemail.Text);
                dmnRegistration.website = Convert.ToString(txtwebsite.Text);
                if (string.IsNullOrWhiteSpace(txtpin.Text))
                    txtpin.Text = "0";
                dmnRegistration.ZipCode = Convert.ToInt32(txtpin.Text);
                dmnRegistration.GstNo = Convert.ToString(txtg.Text);
                dmnRegistration.Country = Convert.ToString(cmbcountry.SelectedValue);
                dmnRegistration.State = Convert.ToString(cmdstate.SelectedValue);
                dmnRegistration.City = Convert.ToString(cmdcity.Text);

                dmnRegistration.BookingInerwell = Convert.ToInt32(tbl_BookingIn.Text);
                //pb2.BackgroundImage = DocumentType.HotelRegistrationProofPics;
                dmnRegistration.CreatedBy = FrmDashboard.UserId;
                dmnRegistration.BranchId = FrmDashboard.BranchId;
                dmnRegistration.Status = 1;
                if (empImgExt != "")
                {
                    if (!Directory.Exists(Compressedimg))
                    {
                        Directory.CreateDirectory(Compressedimg);
                        DirectoryInfo dInfo = new DirectoryInfo(Compressedimg);
                        DirectorySecurity dSecurity = dInfo.GetAccessControl();
                        dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                        dInfo.SetAccessControl(dSecurity);
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(Compressedimg))
                        {
                            System.GC.Collect();
                            System.GC.WaitForPendingFinalizers();
                            File.Delete(file);
                        }
                    }
                    ClsImgComprossor.CompressImage(empImgExt, Compressedimg, 30);


                }
                var InsertStatus = bllregistration.AddEditData("prdInsert_Upd_HotelReg", dmnRegistration);
                if (InsertStatus.Id != 0)
                {
                    txtcid.Text = Convert.ToString(InsertStatus.Id);

                    #region certificate upload
                    if (pb1.BackgroundImage != null)
                    {
                        if (!Directory.Exists(Compressedimg))
                        {
                            Directory.CreateDirectory(Compressedimg);
                            DirectoryInfo dInfo = new DirectoryInfo(Compressedimg);
                            DirectorySecurity dSecurity = dInfo.GetAccessControl();
                            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                            dInfo.SetAccessControl(dSecurity);
                        }
                        else
                        {
                            foreach (string file in Directory.GetFiles(Compressedimg))
                            {
                                System.GC.Collect();
                                System.GC.WaitForPendingFinalizers();
                                File.Delete(file);
                            }
                        }
                        if (empImgExt != null && empImgExt != "")
                            ClsImgComprossor.CompressImage(empImgExt, Compressedimg, 30);

                        if (FrmDashboard.downloadServiceValue == Convert.ToInt32(Interfaces.DownloadService_Type.DB))
                        {
                            #region  certificate photo upload in Database
                           
                           
                            if (pb1.BackgroundImage != null && (empImgExt != null && empImgExt != ""))
                            {
                                Bitmap bitmap = new Bitmap(new Bitmap(pb1.BackgroundImage));
                                int ePid = Convert.ToInt32(txtcid.Text);
                                int Emp_branchid = Convert.ToInt32(FrmDashboard.BranchId);
                                int Upstatus = Convert.ToInt32(1);
                                int Emp_userid = Convert.ToInt32(FrmLogin.UserId);
                                int cid = InsertStatus.Id;
                                int ins_type = Convert.ToInt32(Interfaces.Inseration_Type.Hotel_Cert_Photo);
                                clsDocument.UploadImage_Database(bitmap, ePid, ins_type, cid, Emp_branchid, Upstatus, Emp_userid);
                                FrmDashboard.Instance.ProfilPIC_update();
                            }
                            if (pb2.BackgroundImage != null && (empImgExt != null && empImgExt != ""))
                            {
                                Bitmap bitmap = new Bitmap(new Bitmap(pb2.BackgroundImage));
                                int ePid = Convert.ToInt32(txtcid.Text);
                                int Emp_branchid = Convert.ToInt32(FrmDashboard.BranchId);
                                int Upstatus = Convert.ToInt32(1);
                                int Emp_userid = Convert.ToInt32(FrmLogin.UserId);
                                int cid = InsertStatus.Id;
                                int ins_type = Convert.ToInt32(Interfaces.Inseration_Type.Hotel_Cert_Photo);
                                clsDocument.UploadImage_Database(bitmap, ePid, ins_type, cid, Emp_branchid, Upstatus, Emp_userid);
                                FrmDashboard.Instance.ProfilPIC_update();
                            }
                            if (pb3.BackgroundImage != null && (empImgExt != null && empImgExt != ""))
                            {
                                Bitmap bitmap = new Bitmap(new Bitmap(pb3.BackgroundImage));
                                int ePid = Convert.ToInt32(txtcid.Text);
                                int Emp_branchid = Convert.ToInt32(FrmDashboard.BranchId);
                                int Upstatus = Convert.ToInt32(1);
                                int Emp_userid = Convert.ToInt32(FrmLogin.UserId);
                                int cid = InsertStatus.Id;
                                int ins_type = Convert.ToInt32(Interfaces.Inseration_Type.Hotel_Cert_Photo);
                                clsDocument.UploadImage_Database(bitmap, ePid, ins_type, cid, Emp_branchid, Upstatus, Emp_userid);
                                FrmDashboard.Instance.ProfilPIC_update();
                            }


                        }

                        #endregion
                    }




                    #endregion
                    if (Convert.ToInt32(txtcid.Text) > 0)
                    {
                        CMessageBox.Show("update Successfully ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        CMessageBox.Show("Registration Successfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }

                else
                {
                    CMessageBox.Show("Save Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
        }

        catch (Exception ex)
        {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page btsave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
        }

        }


        private void btfresh_Click(object sender, EventArgs e)
        {
            Refreshform();
        }

        private void btclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void FrmRegistration_Resize(object sender, EventArgs e)
        {
            #region setting Corner Round 
            UIHelper.RoundCorners(tlp_reg, 25, null);
            UIHelper.RoundCorners(tlp_header_nav, 10, null);
            #endregion
            textb_shape();
        }

        private void txtid_Leave(object sender, EventArgs e)
        {
            if (txtcid.Text == "")
            {
                txtcid.Text = "0";

            }
        }

        private void tbl_BookingIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtph_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

   
        private bool ImagesAreEqual(Image img1, Image img2)
        {
            try
            {
                if (img1 == null || img2 == null)
                    return false;
                using (MemoryStream ms1 = new MemoryStream())
                using (MemoryStream ms2 = new MemoryStream())
                {
                    img1.Save(ms1, ImageFormat.Png);
                    img2.Save(ms2, ImageFormat.Png);

                    byte[] img1Bytes = ms1.ToArray();
                    byte[] img2Bytes = ms2.ToArray();

                    if (img1Bytes.Length != img2Bytes.Length)
                        return false;

                    for (int i = 0; i < img1Bytes.Length; i++)
                    {
                        if (img1Bytes[i] != img2Bytes[i])
                            return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page ImagesAreEqual()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
                return false;
            }

        }
        private void btn_cert1_dow_Click(object sender, EventArgs e)
        {
            try
            {

                if (!ImagesAreEqual(pb1.BackgroundImage, FoodandBeverage.Properties.Resources.reg_certificate_icon))
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png",
                        Title = "Save an Image File",
                        DefaultExt = "jpg"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pb1.BackgroundImage.Save(saveFileDialog.FileName);
                    }
                }
                else
                {
                    CMessageBox.Show("No image to Download.");
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page btn_cert1_dow_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }
        }

        private void btn_cert2_dow_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ImagesAreEqual(pb2.BackgroundImage, FoodandBeverage.Properties.Resources.reg_certificate_icon))
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png",
                        Title = "Save an Image File",
                        DefaultExt = "jpg"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pb2.BackgroundImage.Save(saveFileDialog.FileName);
                    }
                }
                else
                {
                    CMessageBox.Show("No image to Download.");
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page btn_cert1_dow_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }
        }

        private void btn_cert3_dow_Click(object sender, EventArgs e)
        {
            try
            {

                if (!ImagesAreEqual(pb3.BackgroundImage, FoodandBeverage.Properties.Resources.reg_certificate_icon))
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png",
                        Title = "Save an Image File",
                        DefaultExt = "jpg"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pb3.BackgroundImage.Save(saveFileDialog.FileName);
                    }
                }
                else
                {
                    CMessageBox.Show("No image to Download.");
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page btn_cert1_dow_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }
        }

        private void btn_cert1_view_Click(object sender, EventArgs e)
        {
            if (pb1.BackgroundImage == null || ImagesAreEqual(pb1.BackgroundImage, FoodandBeverage.Properties.Resources.reg_certificate_icon))
            {
                CMessageBox.Show("No image to view.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //lbl_cert_view_name.Text = certi1.Text;
                pnlcerti_view.Location = new Point(
          this.ClientSize.Width / 2 - pnlcerti_view.Size.Width / 2,
       (this.ClientSize.Height / 5 - pnlcerti_view.Size.Height / 6));
                pnlcerti_view.Anchor = AnchorStyles.None;
                pnlcerti_view.Visible = true;

                pb_cert_view_box.BackgroundImage = null;
                pb_cert_view_box.BackgroundImage = pb1.BackgroundImage;
            } 

        }

        private void btn_cert2_view_Click(object sender, EventArgs e)
        {
            if (pb2.BackgroundImage == null || ImagesAreEqual(pb2.BackgroundImage, FoodandBeverage.Properties.Resources.reg_certificate_icon))
            {
                CMessageBox.Show("No image to view.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //lbl_cert_view_name.Text = certi2.Text;
                pnlcerti_view.Location = new Point(
          this.ClientSize.Width / 2 - pnlcerti_view.Size.Width / 2,
       (this.ClientSize.Height / 5 - pnlcerti_view.Size.Height / 6));
                pnlcerti_view.Anchor = AnchorStyles.None;
                pnlcerti_view.Visible = true;

                pb_cert_view_box.BackgroundImage = null;
                pb_cert_view_box.BackgroundImage = pb2.BackgroundImage;
            }
        }

        private void btn_cert3_view_Click(object sender, EventArgs e)
        {
            if (pb3.BackgroundImage == null || ImagesAreEqual(pb3.BackgroundImage, FoodandBeverage.Properties.Resources.reg_certificate_icon))
            {
                CMessageBox.Show("No image to view.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //lbl_cert_view_name.Text = certi3.Text;
                pnlcerti_view.Location = new Point(
          this.ClientSize.Width / 2 - pnlcerti_view.Size.Width / 2,
       (this.ClientSize.Height / 5 - pnlcerti_view.Size.Height / 6));
                pnlcerti_view.Anchor = AnchorStyles.None;
                pnlcerti_view.Visible = true;

                pb_cert_view_box.BackgroundImage = null;
                pb_cert_view_box.BackgroundImage = pb3.BackgroundImage;
            }
        }

        private void btn_cert1_remove_Click(object sender, EventArgs e)
        {
            //certi1.Text = "";
            pb1.BackgroundImage = null;
            pb1.BackgroundImage = FoodandBeverage.Properties.Resources.reg_certificate_icon;
        }

        private void btn_cert2_remove_Click(object sender, EventArgs e)
        {
            //certi2.Text = "";
            pb2.BackgroundImage = null;
            pb2.BackgroundImage = FoodandBeverage.Properties.Resources.reg_certificate_icon;
        }

        private void btn_cert3_remove_Click(object sender, EventArgs e)
        {
            //certi3.Text = "";
            pb3.BackgroundImage = null;
            pb3.BackgroundImage = FoodandBeverage.Properties.Resources.reg_certificate_icon;
        }

        private void btnclose_certi_view_Click(object sender, EventArgs e)
        {
            pnlcerti_view.Visible = false;
        }

        private void txtemail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtemail.Text != "")
            {
                ClsGeneral cls = new ClsGeneral();
                if (!cls.CheckEmailId(this, txtemail.Text))
                {
                    e.Cancel = true;
                }
            }
        }

        private void tbl_BookingIn_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void FrmRegistration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                AppSession.UnregisterForm(this);
            }
        }

        private void btn_cert_upload_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
        }

        private void proof1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
                DialogResult dialogResult = openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    empImgExt = openFileDialog.FileName;
                    pb1.BackgroundImage = Image.FromFile(openFileDialog.FileName);
                }

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page proof1_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }
        }

        private void proof2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
                DialogResult dialogResult = openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    empImgExt = openFileDialog.FileName;
                    pb2.BackgroundImage = Image.FromFile(openFileDialog.FileName);
                }

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page proof2_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }
        }

        private void proof3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
                DialogResult dialogResult = openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    empImgExt = openFileDialog.FileName;
                    pb3.BackgroundImage = Image.FromFile(openFileDialog.FileName);
                }

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("company Page proof3_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.company));
            }
        }

        private void btn_cert_ok__Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}
