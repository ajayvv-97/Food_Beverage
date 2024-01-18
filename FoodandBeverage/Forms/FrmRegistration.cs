using BLL;
using Domain;
using FoodandBeverage.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FoodandBeverage.Interfaces;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
namespace FoodandBeverage.Forms
{
    public partial class FrmRegistration : Form
    {
        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsGeneral = new ClsGeneral();
        public string Compressedimg = Application.StartupPath + "\\CompressedImg";
        long BranchId ;// Forms.frmLogin.BranchId;//16
        long UserId ;// Forms.frmLogin.UserId;//1
                        //long FinancialYearId = Convert.ToInt64(FrmDashBoard.FinancialYearId);//10
        Bitmap bitmap;
        string _retValue;
        long AccID;
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
           



        }
        private bool Isvalid()
        {


            if (string.IsNullOrWhiteSpace(txtname.Text.Trim()))
            {

                MessageBox.Show(this, "", "Account Name must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtname.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtph.Text.Trim()))
            {
                MessageBox.Show(this, "", "Phone No.must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtph.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtname, txtaddress, txtph, txtpin, txtg, certi1, certi2, certi3, tbl_BookingIn })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception)
            {

            }
        }

        private void ApplyRoundedTextBox(TextBox textBox, int radius)
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
                dmnRegistration.Country = Convert.ToString(comboBox3.SelectedValue);
                dmnRegistration.State = Convert.ToString(cmdstate.SelectedValue);
                dmnRegistration.City = Convert.ToString(cmdcity.SelectedValue);
                dmnRegistration.Certificate = Convert.ToString(certi1.Text);
                dmnRegistration.Certificate1 = Convert.ToString(certi2.Text);
                dmnRegistration.Certificate2 = Convert.ToString(certi3.Text);
                dmnRegistration.CreatedBy = 1;
                dmnRegistration.BranchId = 1030;
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
                        MessageBox.Show(this, txtname.Text + "  ", "Detail Successfully Updated", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        this.Close();
                    }
                    else
                        MessageBox.Show(this, "  ", "Successfully Saved", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    Refreshform();
                }
                else
                {
                    MessageBox.Show(this, " ", " Save Failed  ", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        private void Refreshform()
        {
            //txtid.Text = LedgerId;
            if (txtid.Text != null && txtid.Text !="")
            {
                filldetails();
            }
            else
            {
                txtname.Text = "";
                txtaddress.Text = "";

                txtpin.Text = "";
                txtph.Text = "";
                certi1.Text = "";
                certi2.Text = "";
                certi3.Text = "";
                comboBox3.Text = "select";
                cmdstate.Text = "select";
                cmdcity.Text = "select";
                certipic1.Text = "select";
                certipic2.Text = "select";
                certipic3.Text = "select";
                pb1.BackgroundImage = null;
                pb1.Image = null;
                pb2.Image = null;
                pb2.Image = null;
                pb3.BackgroundImage = null;
                pb3.BackgroundImage = null;
                FillCombo();
            }
            }
            private void getNewAccNo()
            {
                int sel = 143;
            }
            public void FillCombo()
            {
                BllSearch<DmnComboBox> bllSearch_Country = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "GTypeSubId,Caption";
                objSearch.Table = " tblGeneralOptions";
                objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=42";
                objSearch.OrderBy = "order by value";
                var gridSource = bllSearch_Country.GetSearchData("prdComboFill", objSearch);
            comboBox3.DataSource = gridSource;
            comboBox3.ValueMember = "Id";
            comboBox3.DisplayMember = "Value";
            comboBox3.SelectedIndex = 0;

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
                objSearch.Select = "GTypeSubId,Caption";
                objSearch.Table = " tblGeneralOptions";
                objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=44";
                objSearch.OrderBy = "order by value";
                gridSource = bllSearch_City.GetSearchData("prdComboFill", objSearch);
            cmdcity.DataSource = gridSource;
            cmdcity.ValueMember = "Id";
            cmdcity.DisplayMember = "Value";
            cmdcity.SelectedIndex = 0;
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
                txtid.Text = Convert.ToString(returnValue);
                Id = Convert.ToInt32(txtid.Text);
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
                comboBox3.SelectedValue = Convert.ToInt64(gridSource[0].cCountry);//change
                cmdstate.SelectedValue = Convert.ToInt64(gridSource[0].State);//change
                cmdcity.Text = gridSource[0].City;
                    var imageExist = clsDocument.FileExistOrNot(Id.ToString() + ".jpeg", DocumentType.CustomerPhoto);
                    if (imageExist)
                    pb1.BackgroundImage = clsDocument.DownloadImage(Id.ToString() + ".jpeg", DocumentType.CustomerPhoto);
                pb1.BackgroundImageLayout = ImageLayout.Stretch;

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
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
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
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                        );
                }
            }
            private void cboCountry_Leave(object sender, EventArgs e)
            {
                if (comboBox3.Text != null || comboBox3.Text != "")

                    Insert_Combo1(comboBox3.Text.Trim().ToUpper(), 42);
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

            private void pbCust_DoubleClick(object sender, EventArgs e)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Work for Capture Device", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }



            private void txtAccName_TextChanged(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtAccName.Text) == false)
                {
                    getNewAccNo();
                }
            }

            private void txtAccName_Leave(object sender, EventArgs e)
            {
               
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

            private void cboProof_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SendKeys.Send("{TAB}");
                }
            }

            private void cboProof1_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SendKeys.Send("{TAB}");
                }
            }

            private void cboProof2_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SendKeys.Send("{TAB}");
                }
            }

            private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
            {
            }

            private void pbAdd_Click(object sender, EventArgs e)
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
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                }
            }

            private void btnCapture_Click(object sender, EventArgs e)
            {
            }

            private void cboProof1_Click(object sender, EventArgs e)
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
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                }
            }

            private void cboProof2_Click(object sender, EventArgs e)
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
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                }
            }

            private void FrmRegistration_Load_1(object sender, EventArgs e)
            {    

            //Refreshform();
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
                btsave.Enabled = true;
        }
        public void filldetails()
        {
           
                DmnSearch dmnSearch = new DmnSearch();
                BllSearch<DmnHRegistration> _bllSearch = new BllSearch<DmnHRegistration>();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblHotelRegistration";
                dmnSearch.WhereNdGroupBy = " Where BranchId =" + 1030;
                dmnSearch.OrderBy = "";
                var gridSource1 = _bllSearch.GetSearchData("prdSearch", dmnSearch);
            if (gridSource1.Count > 0)
            {
                txtid.Text = Convert.ToString(gridSource1[0].Id);
                txtname.Text = Convert.ToString(gridSource1[0].Name);
                txtaddress.Text = Convert.ToString(gridSource1[0].Address);
                txtph.Text = Convert.ToString(gridSource1[0].Phone);
                txtpin.Text = Convert.ToString(gridSource1[0].ZipCode);
                txtg.Text = Convert.ToString(gridSource1[0].GstNo);
                certi1.Text = Convert.ToString(gridSource1[0].Certificate);
                certi2.Text = Convert.ToString(gridSource1[0].Certificate1);
                certi3.Text = Convert.ToString(gridSource1[0].Certificate2);
                tbl_BookingIn.Text = Convert.ToString(gridSource1[0].BookingInerwell);
                comboBox3.SelectedValue = Convert.ToInt64(gridSource1[0].Country);
                cmdstate.SelectedValue = Convert.ToInt64(gridSource1[0].State);
                cmdcity.SelectedValue = Convert.ToInt64(gridSource1[0].City);
                bool imageExist = clsDocument.FileExistOrNot(gridSource1[0].Id + ".jpeg", DocumentType.HotelRegistrationProofPics);
                if (imageExist)
                {
                    pb1.BackgroundImage = clsDocument.DownloadImage(gridSource1[0].Id + ".jpeg", DocumentType.HotelRegistrationProofPics);
                    pb1.BackgroundImageLayout = ImageLayout.Stretch;
                    pb1.Image = null;
                }
                if (imageExist)
                {
                    pb2.BackgroundImage = clsDocument.DownloadImage(gridSource1[0].Photo + ".jpeg", DocumentType.HotelRegistrationProofPics);
                    pb2.BackgroundImageLayout = ImageLayout.Stretch;
                    pb2.Image = null;
                }
                if (imageExist)
                {
                    pb3.BackgroundImage = clsDocument.DownloadImage(gridSource1[0].Photo + ".jpeg", DocumentType.HotelRegistrationProofPics);
                    pb3.BackgroundImageLayout = ImageLayout.Stretch;
                    pb3.Image = null;
                }

            }

        }
        private void txtZipcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void txtname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }

            if (e.KeyCode == Keys.F8)
                LedgerPopup();
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
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
            if (comboBox3.Text != null || comboBox3.Text != "")

                Insert_Combo1(comboBox3.Text.Trim().ToUpper(), 42);
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

        private void certipic1_Click(object sender, EventArgs e)
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
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void certipic2_Click(object sender, EventArgs e)
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
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void certipic3_Click(object sender, EventArgs e)
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
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
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
                if (string.IsNullOrWhiteSpace(txtid.Text))
                    txtid.Text = "0";
                dmnRegistration.Id = Convert.ToInt32(txtid.Text);
                dmnRegistration.Name = Convert.ToString(txtname.Text);
                dmnRegistration.Address = Convert.ToString(txtaddress.Text);
                dmnRegistration.Phone = Convert.ToString(txtph.Text);
                if (string.IsNullOrWhiteSpace(txtpin.Text))
                    txtpin.Text = "0";
                dmnRegistration.ZipCode = Convert.ToInt32(txtpin.Text);
                dmnRegistration.GstNo = Convert.ToString(txtg.Text);
                dmnRegistration.Country = Convert.ToString(comboBox3.SelectedValue);
                dmnRegistration.State = Convert.ToString(cmdstate.SelectedValue);
                dmnRegistration.City = Convert.ToString(cmdcity.SelectedValue);
                dmnRegistration.Certificate = Convert.ToString(certi1.Text);
                dmnRegistration.Certificate1 = Convert.ToString(certi2.Text);
                dmnRegistration.Certificate2 = Convert.ToString(certi3.Text);
                dmnRegistration.BookingInerwell = Convert.ToInt32(tbl_BookingIn.Text);
                dmnRegistration.CreatedBy = 1;
                dmnRegistration.BranchId = 1030;
                dmnRegistration.Status = 1;
                var InsertStatus = bllregistration.AddEditData("prdInsert_Upd_HotelReg", dmnRegistration);
                if (InsertStatus.Id != 0)
                {
                    if (InsertStatus.Id > 0)
                    {
                        if (pb1.BackgroundImage != null)
                        {

                            Bitmap bm = new Bitmap(pb1.BackgroundImage);
                            clsDocument.UploadImage(bm, InsertStatus.Id + ".jpeg", DocumentType.HotelRegistrationProofPics);
                        }
                        if (pb2.BackgroundImage != null)
                        {
                            Bitmap bm2 = new Bitmap(pb2.BackgroundImage);
                            clsDocument.UploadImage(bm2, InsertStatus.Id + "_2.jpeg", DocumentType.HotelRegistrationProofPics);
                        }

                        if (pb3.BackgroundImage != null)
                        {
                            Bitmap bm3 = new Bitmap(pb3.BackgroundImage);
                            clsDocument.UploadImage(bm3, InsertStatus.Id + "_3.jpeg", DocumentType.HotelRegistrationProofPics);
                        }
                        else
                        {
                            bool imageExist = clsDocument.FileExistOrNot(InsertStatus.Id + ".jpeg", DocumentType.HotelRegistrationProofPics);
                            if (imageExist)
                                clsDocument.DeleteImage(InsertStatus.Id + ".jpeg", DocumentType.HotelRegistrationProofPics);
                            InsertStatus.Photo = "";
                            InsertStatus.Photo = "";
                        }
                        FrmDashboard.BranchId = 1030;
                        MessageBox.Show(this, txtAccName.Text + "  ", "Registration Successfully Updated", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        this.Close();
                    }
                    else
                    {
                        FrmDashboard.BranchId = 1030;
                        MessageBox.Show(this, "  ", "Successfully Saved", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    //Refreshform();
                }
                else
                {
                    MessageBox.Show(this, " ", " Save Failed  ", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                Refreshform();
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmRegistration_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }
    }
}
