using BLL;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
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
    public partial class FrmEmployeeShort : Form
    {
        ClsDocument clsDocument = new ClsDocument();
        FilterInfoCollection FilterInfoCollection;
        VideoCaptureDevice VideoCaptureDevice;
        long BranchId = 1030;//16
        long UserId = Forms.FrmLogin.UserId;//1
        public static int Allaccess = 0;
        public static int ReportLevelID = 0;
        long DepartmentId = 0;
        public static long EmployeeId = 0, AccountId = 0;
        string EmployeeCode = "";
        public static string empImgExt = "";
        long UserAccountId = 0;
        public string Compressedimg = Application.StartupPath + "\\CompressedImg";
        Bitmap bitmap;
        //Allaccess = Convert.ToInt32(frmLogin.Allaccess);
        byte[] PasswordSalt, PasswordHash;
        CheckBox headerCheckBoxwrite = new CheckBox();
        CheckBox headerCheckBoxread = new CheckBox();
        public static int iid = 0;
        public static int iformid = 1;
        public static int iforms = 2;
        public static int iwrite = 3;
        public static int iread = 4;
        public static int istatus = 5;
        int empid = 0;
        ClsGeneral clsGeneral = new ClsGeneral();

        public int VideoCaptureDevice_NewFrame { get; private set; }
      

        public FrmEmployeeShort()
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
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtusertype, txtdescr, txtempcode, txtEmpName, txtphone, txtEmpAddress, txtnote, txtICardNo1, txtemail, txtUserTypeNote, txtuname, txtpassword, txtcconpassword })
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

        private void mbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtempcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtEmpName_Leave(object sender, EventArgs e)
        {
            lblToolTipAccName.Visible = false;
        }

        private void txtEmpName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
            if (e.KeyCode == Keys.F8)
            {
                button5_Click(sender, e);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Employee,Code,Department,Designation";
                _objSearch.Select = "EmployeeId as Id, emp.EmployeeName as Field1,emp.EmployeeCode as Field2,dp.DepartmentName as Field3,ds.DesignationName as Field4";
                _objSearch.Table = "tblEmployee as emp " +
                               "left join tblDepartment as dp on emp.DepartmentId = dp.DepartmentId " +
                               "left join tblDesignation as ds on emp.DesignationId = ds.DesignationId";
                _objSearch.WhereNdGroupBy = "Where emp.Status=1 and emp.BranchId = " +1030;

                _objSearch.OrderBy = " Order By EmployeeName ASC ";

                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);

                    }
                }
                if (returnValue.ToString() != null && returnValue.ToString() != "")
                {
                    clear();
                    EmployeeId = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
                    DmnSearch objSearch = new DmnSearch();
                    //objSearch.Select = "tblEmployee.*,CONVERT(nvarchar(MAX),TimeIn,108) as sTimeIn,CONVERT(nvarchar(MAX),TimeOut,108) as sTimeOut,CONVERT(nvarchar(MAX),OverTimeStart,108) as sOverTimeStart";
                    objSearch.Select = "e.*,UserAccountId,UserAccountTypeId,Username,PasswordHash,PasswordSalt";
                    objSearch.Table = " tblEmployee e " +
                                      " left join tblUserAccount a on a.EmployeeId=e.EmployeeId ";
                    objSearch.WhereNdGroupBy = "Where e.EmployeeId = " + EmployeeId;
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource.Count > 0)
                    {
                        FillCombo();
                        txtEmpName.Text = gridSource[0].EmployeeName;
                        mcmbDesignationId.SelectedValue = Convert.ToInt64(gridSource[0].DesignationId);
                        mcmbDepartmentId.SelectedValue = Convert.ToInt64(gridSource[0].DepartmentId);
                        txtEmpAddress.Text = gridSource[0].EmployeeAddress;
                        cboGender.SelectedValue = Convert.ToInt64(gridSource[0].Gender);
                        dtpBirthDate.Value = gridSource[0].DOB;
                        txtphone.Text = gridSource[0].Mobile;
                        txtemail.Text = gridSource[0].Email;
                        txtCardnumber.Text = gridSource[0].ICardNo;
                        txtICardNo1.Text = "XXXX XX" + gridSource[0].ICardNo.Substring(gridSource[0].ICardNo.Length - 6, 6);
                        txtnote.Text = gridSource[0].Remarks;
                        txtempcode.Text = gridSource[0].EmployeeCode;
                        if (gridSource[0].Status == 1)
                            chkactive.Checked = true;
                        else
                            chkactive.Checked = false;
                        //mcmblocation.SelectedValue = gridSource[0].BranchId;

                        if (gridSource[0].RFID_Login_Status == 1)
                            chk_Rfid_Login.Checked = true;
                        else
                            chk_Rfid_Login.Checked = false;

                        bool imageExist = clsDocument.FileExistOrNot(gridSource[0].EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                        if (imageExist)
                        {
                            pbCust.BackgroundImage = clsDocument.DownloadImage(gridSource[0].EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                            pbCust.BackgroundImageLayout = ImageLayout.Stretch;
                            pbCust.Image = null;
                        }

                        if (gridSource[0].UserAccountTypeId != null)
                        {
                            UserAccountId = gridSource[0].UserAccountId;
                            cmbUsreAccount.SelectedValue = Convert.ToInt64(gridSource[0].UserAccountTypeId);
                            FillForms(Convert.ToInt32(gridSource[0].UserAccountTypeId));
                            mcmbaccounttype.SelectedValue = Convert.ToInt64(gridSource[0].UserAccountTypeId);
                            txtuname.Text = gridSource[0].Username;
                            PasswordSalt = gridSource[0].PasswordSalt;
                            PasswordHash = gridSource[0].PasswordHash;
                            txtpassword.Text = "";
                            txtcconpassword.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtEmpName_Enter(object sender, EventArgs e)
        {
            lblToolTipAccName.Visible = true;

           
       
        }

        private void txtICardNo1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtICardNo1_Enter(object sender, EventArgs e)
        {
            timer1.Start();
        }
        public void FillCombo()
        {
            //Department
            BllSearch<DmnComboBox> bllSearchDep = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchDep = new DmnSearch();
            dmnSearchDep.Select = "DepartmentId,DepartmentName";
            dmnSearchDep.Table = "tblDepartment";
            //if (FrmDashBoard.Allaccess == 1)
            //    dmnSearchDep.WhereNdGroupBy = "Where Status =1";
            //else
            //    dmnSearchDep.WhereNdGroupBy = "Where Status =1 and BranchId=" + Convert.ToInt32(mcmblocation.SelectedValue);
            //dmnSearchDep.OrderBy = "order by Value";
            //mcmbDepartmentId.DataSource = null;
            var departmentSource = bllSearchDep.GetSearchData("prdComboFill", dmnSearchDep);
            mcmbDepartmentId.DisplayMember = "Value";
            mcmbDepartmentId.ValueMember = "Id";
            mcmbDepartmentId.DataSource = departmentSource;

            //Designation
            BllSearch<DmnComboBox> bllSearchDes = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchDes = new DmnSearch();
            dmnSearchDes.Select = "DesignationId,DesignationName";
            dmnSearchDes.Table = " tblDesignation ";
         
            mcmbDesignationId.DataSource = null;
            var designationSource = bllSearchDes.GetSearchData("prdComboFill", dmnSearchDes);
            mcmbDesignationId.DataSource = designationSource;
            mcmbDesignationId.DisplayMember = "Value";
            mcmbDesignationId.ValueMember = "Id";

            //Gender
            BllSearch<DmnComboBox> bllGender = new BllSearch<DmnComboBox>();
            DmnSearch dmngender = new DmnSearch();
            dmngender.Select = "GTypeSubId,Caption";
            dmngender.Table = " tblGeneralOptions ";
            dmngender.WhereNdGroupBy = "Where GTypeId = 25 ";//AND DepartmentId = " + mcmbDepartmentId.SelectedValue;
            dmngender.OrderBy = "order by Value";
            var gender = bllGender.GetSearchData("prdComboFill", dmngender);
            cboGender.DataSource = gender;
            cboGender.DisplayMember = "Value";
            cboGender.ValueMember = "Id";

            //Account type         
            BllSearch<DmnComboBox> blllocation = new BllSearch<DmnComboBox>();
            DmnSearch dmnlocation = new DmnSearch();
            dmnlocation.Select = "LocId,Name";
            dmnlocation.Table = " tblLocationDetails ";
            dmnlocation.WhereNdGroupBy = "Where Status = 1 ";//AND DepartmentId = " + mcmbDepartmentId.SelectedValue;
            dmnlocation.OrderBy = "order by Value";
            ///var location = blllocation.GetSearchData("prdComboFill", dmnlocation);
            //mcmblocation.DataSource = location;
            //mcmblocation.DisplayMember = "Value";
            //mcmblocation.ValueMember = "Id";
        }
        public bool CheckCardAssign(string cardno)
        {
            string mastercardfullno = cardno;
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "SUM(cnt) AS Field1";
            objSearch.Table = " ( SELECT COUNT(*) AS cnt FROM tblEmployee WHERE Status=1 and ICardNo = '" + cardno + "') AS counts";
            objSearch.WhereNdGroupBy = "";
            objSearch.OrderBy = "";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            //return false;
            if (itemSource.Count > 0)
            {
                if (Convert.ToInt32(itemSource[0].Field1) == 0)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }
        public void Keypad_Rfid(string str1)
        {
            if (CheckCardAssign(str1))
            {
                CMessageBox.Show("Card Already Used !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
            DmnSearch objSearch = new DmnSearch();
            //objSearch.Select = "tblEmployee.*,CONVERT(nvarchar(MAX),TimeIn,108) as sTimeIn,CONVERT(nvarchar(MAX),TimeOut,108) as sTimeOut,CONVERT(nvarchar(MAX),OverTimeStart,108) as sOverTimeStart";
            objSearch.Select = "e.*,UserAccountId,UserAccountTypeId,Username,PasswordHash,PasswordSalt";
            objSearch.Table = " tblEmployee e " +
                "left join tblUserAccount a on a.EmployeeId = e.EmployeeId";
            objSearch.WhereNdGroupBy = "Where e.Status=1 and ICardNo = '" + str1 + "'";
            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
            if (gridSource.Count > 0)
            {
                FillCombo();
                txtEmpName.Text = gridSource[0].EmployeeName;
                mcmbDesignationId.SelectedValue = Convert.ToInt64(gridSource[0].DesignationId);
                mcmbDepartmentId.SelectedValue = Convert.ToInt64(gridSource[0].DepartmentId);

                txtEmpAddress.Text = gridSource[0].EmployeeAddress;
                cboGender.SelectedValue = Convert.ToInt64(gridSource[0].Gender);
                dtpBirthDate.Value = gridSource[0].DOB;
                txtphone.Text = gridSource[0].Mobile;
                txtemail.Text = gridSource[0].Email;
                txtnote.Text = gridSource[0].Remarks;
                txtempcode.Text = gridSource[0].EmployeeCode;
                EmployeeId = gridSource[0].EmployeeId;
                UserAccountId = gridSource[0].UserAccountId;
                BranchId = 1030;
                if (gridSource[0].Status == 1)
                    chkactive.Checked = true;
                else
                    chkactive.Checked = false;

                if (gridSource[0].RFID_Login_Status == 1)
                    chk_Rfid_Login.Checked = true;
                else
                    chk_Rfid_Login.Checked = false;

                bool imageExist = clsDocument.FileExistOrNot(gridSource[0].EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                if (imageExist)
                {
                    pbCust.BackgroundImage = clsDocument.DownloadImage(gridSource[0].EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                    pbCust.BackgroundImageLayout = ImageLayout.Stretch;
                    pbCust.Image = null;
                }
                else
                {
                    pbCust.SizeMode = PictureBoxSizeMode.StretchImage;
                    if (Convert.ToInt32(cboGender.SelectedValue) == 1)
                        pbCust.Image = null;

                    else if (Convert.ToInt32(cboGender.SelectedValue) == 2)
                        pbCust.Image =null;
                }
                if (gridSource[0].UserAccountTypeId != null)
                {
                    cmbUsreAccount.SelectedValue = Convert.ToInt64(gridSource[0].UserAccountTypeId);
                    FillForms(Convert.ToInt32(gridSource[0].UserAccountTypeId));
                    mcmbaccounttype.SelectedValue = Convert.ToInt64(gridSource[0].UserAccountTypeId);
                    txtuname.Text = gridSource[0].Username;
                    PasswordSalt = gridSource[0].PasswordSalt;
                    PasswordHash = gridSource[0].PasswordHash;
                    txtpassword.Text = "";
                    txtcconpassword.Text = "";
                }
            }

            string cardno = str1;
            cardno = cardno.Substring(cardno.Length - 6, 6);
            cardno = "XXXX XX" + cardno;
            txtICardNo1.Text = cardno;
            txtCardnumber.Text = str1;

        }
        public string RemoveSpecialCharacters(string str)
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
        private void btnrfid_Click(object sender, EventArgs e)
        {
            try
            {
                string str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul();
                if (str1 != "Card not in contact")
                {
                    timer1.Stop();
                    if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")
                        str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi();

                    str1 = RemoveSpecialCharacters(str1.Trim());

                    if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")
                        CMessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        Keypad_Rfid(str1);
                    }
                }

                //mcmbDesignationId.Focus();
            }
            catch (Exception ex)
            {

            }
        }

        private void mcmbDesignationId_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(mcmbDesignationId.SelectedIndex) > 0)
                {
                    int DesignationId = Convert.ToInt32(mcmbDesignationId.SelectedValue);
                    BllSearch<DmnDesignation> bllSearch = new BllSearch<DmnDesignation>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "des.DepartmentId,dep.DepartmentName";
                    objSearch.Table = "tblDesignation des left join tblDepartment dep on dep.DepartmentId=des.DepartmentId";
                    objSearch.WhereNdGroupBy = " Where des.status=1 and des.DesignationId = " + DesignationId;
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    mcmbDepartmentId.SelectedValue = Convert.ToInt64(gridSource[0].DepartmentId);
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GenerateCode()
        {
            int departmentId = Convert.ToInt32(mcmbDepartmentId.SelectedIndex);
            if (departmentId > 0)
            {
                // int n1 = 0;
                BllSearch<DmnDepartment> bllSearch = new BllSearch<DmnDepartment>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "DepartmentId,DepartmentCode,EmployeeSrlNoCurrent";
                objSearch.Table = "tblDepartment";
                objSearch.WhereNdGroupBy = "Where DepartmentId = " + mcmbDepartmentId.SelectedValue;
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                txtempcode.Text = gridSource[0].DepartmentCode + (gridSource[0].EmployeeSrlNoCurrent + 1);

            }
        }
        private void mcmbDepartmentId_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (EmployeeId != 0)
                {
                    if (DepartmentId != Convert.ToInt32(mcmbDepartmentId.SelectedValue))
                    {
                        GenerateCode();
                    }
                    else
                        txtempcode.Text = EmployeeCode;
                }
                else
                {
                    GenerateCode();
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mcmbDesignationId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void mcmbDepartmentId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtphone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void btnDesgtn_Click(object sender, EventArgs e)
        {
            FrmDesignation frmDesignation = new FrmDesignation();
            frmDesignation.StartPosition = FormStartPosition.CenterParent;
            frmDesignation.ShowDialog();
            FillCombo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmDepartment frmDepartment = new FrmDepartment();
            frmDepartment.StartPosition = FormStartPosition.CenterParent;
            frmDepartment.ShowDialog();
            FillCombo();
        }

        private void txtphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cboGender_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cboGender.SelectedIndex) == 1)
                pbCust.Image = FoodandBeverage.Properties.Resources.Profilepicfemale;
            else if (Convert.ToInt32(cboGender.SelectedIndex) == 2)
                pbCust.Image = FoodandBeverage.Properties.Resources. Profilepicmale;
            pbCust.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void dtpBirthDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtEmpAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtnote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void chk_Rfid_Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Tab || e.KeyData == Keys.Enter)
            {
                cmbcam.Focus();
            }
        }

        private void mpbAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
                DialogResult dialogResult = openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    // empImgExt = Path.GetExtension(openFileDialog.FileName);
                    empImgExt = openFileDialog.FileName;
                    pbCust.BackgroundImage = Image.FromFile(openFileDialog.FileName);
                    pbCust.BackgroundImageLayout = ImageLayout.Stretch;
                    pbCust.Image = null;
                    Bitmap bitmap = new Bitmap(Image.FromFile(openFileDialog.FileName));
                    bitmap.Save(Compressedimg + "\\Image.jpeg");
                }
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                CMessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mpbRemove_Click(object sender, EventArgs e)
        {
            pbCust.BackgroundImage = null;
            pbCust.Image = null;
        }

        private void pbCust_Click(object sender, EventArgs e)
        {
            if (cmbcam.Items.Count > 0)
            {
                //VideoCaptureDevice = new VideoCaptureDevice(FilterInfoCollection[cmbcam.SelectedIndex].MonikerString);
                VideoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
                VideoCaptureDevice.Start();
            }
            else
            {
                CMessageBox.Show("No Cameras Detected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                pbCust.Image = null;

                if (Convert.ToInt32(cboGender.SelectedValue) == 1)
                    pbCust.Image = FoodandBeverage.Properties.Resources.Profilepicmale;
                else if (Convert.ToInt32(cboGender.SelectedValue) == 2)
                    pbCust.Image = FoodandBeverage.Properties.Resources.Profilepicfemale;
                else
                    pbCust.Image = FoodandBeverage.Properties.Resources.Profilepicmale;
                pbCust.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (cmbcam.Items.Count > 0)
            {
                //VideoCaptureDevice.Stop();
                Bitmap bitmap = new Bitmap(pbCust.BackgroundImage);
                bitmap.Save(Compressedimg + "\\Image.jpeg");
            }
            else
                CMessageBox.Show("No Cameras Detected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            btnSave.Enabled = true;
        }
        static bool ValidatePhoneNumber(string phoneNumber)
        {
            // Regular expression pattern for a phone number with a minimum of 10 digits and a maximum of 12 digits
            string pattern = @"^\d{10,12}$";
            bool isLengthValid = Regex.IsMatch(phoneNumber, pattern);

            if (!isLengthValid)
            {
                if (phoneNumber.Length < 10)
                {
                    CMessageBox.Show("Phone number should have a minimum of 10 digits.", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (phoneNumber.Length > 12)
                {
                    CMessageBox.Show("Phone number should have a maximum of 12 digits.", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            return isLengthValid;
        }
        public bool IsEmployeeExist()
        {
            DmnEmployee dmnEmployee = new DmnEmployee();
            BllSearch<DmnEmployee> bllSearch = new BllSearch<DmnEmployee>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = " EmployeeName";
            dmnSearch.Table = " tblEmployee";
            if (EmployeeId == 0)
                dmnSearch.WhereNdGroupBy = "Where EmployeeName='" + txtEmpName.Text + "' and Status=0 and BranchId=" + 1030;
            else
                dmnSearch.WhereNdGroupBy = "Where EmployeeName='" + txtEmpName.Text + "' and Status=0 and EmployeeId!='" + EmployeeId + "' and BranchId=" + 1030;

            var employee = bllSearch.GetSearchData("prdSearch", dmnSearch);
            if (employee.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool IsValidEmail(string email)
        {

            try
            {
                // Use a regular expression to validate the email address
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email, pattern);
            }
            catch (Exception ex)
            {
                // Catch any format exceptions thrown by the regular expression
                return false;
            }
        }
        public bool MandatoryCheck()
        {
            if (string.IsNullOrEmpty(txtEmpName.Text))
            {
                CMessageBox.Show("Employee Name is mandatory, Fields  in star are also mandatory.Ensure all information is filled.", "Mandatory Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmpName.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtCardnumber.Text))
            {
                CMessageBox.Show("Card Number is mandatory,Fields  in star are also mandatory.Ensure all information is filled.", "Mandatory Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCardnumber.Focus();
                return false;
            }

            else if (mcmbDesignationId.SelectedIndex == 0 || mcmbDesignationId.SelectedIndex == -1)
            {
                CMessageBox.Show("Please specify your designation. This field is mandatory.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mcmbDesignationId.Focus();
                return false;
            }
            else if (mcmbDepartmentId.SelectedIndex == 0 || mcmbDepartmentId.SelectedIndex == -1)
            {
                CMessageBox.Show("Please specify your department. This field is mandatory.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mcmbDepartmentId.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtphone.Text))
            {
                CMessageBox.Show("Phone Number is mandatory,Fields  in star are also mandatory.Ensure all information is filled ", "Mandatory Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtphone.Focus();
                return false;
            }

            if (txtphone.Text != "")
            {
                bool isValidPhoneNumber = ValidatePhoneNumber(txtphone.Text);
                if (isValidPhoneNumber == false)
                {
                    txtphone.Focus();
                    return false;
                }
            }
            if (cboGender.SelectedIndex == 0 || cboGender.SelectedIndex == -1)
            {
                CMessageBox.Show("Please specify your gender. This field is mandatory.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGender.Focus();
                return false;
            }

            if (txtemail.Text != "")
            {
                string email = txtemail.Text;
                bool isValid = IsValidEmail(email);
                if (isValid == false)
                {
                    CMessageBox.Show("Please Enter Valid Mail ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtemail.Focus();
                    return false;
                }

            }

            else if (!IsEmployeeExist())
            {
                CMessageBox.Show("Employee name already exists. Please use a different name.", "Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmpName.Focus();
                return false;
            }

            else if (cmbUsreAccount.SelectedIndex == 0 || cmbUsreAccount.SelectedIndex == -1)
            {
                CMessageBox.Show("Please specify the access type. This field is mandatory.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedTab = tabPage2;
                cmbUsreAccount.Focus();
                return false;
            }
            else if (mcmbaccounttype.SelectedIndex == 0 || mcmbaccounttype.SelectedIndex == -1)
            {
                CMessageBox.Show("User Type Need To Be specify", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedTab = tabPage3;
                mcmbaccounttype.Focus();
                return false;
            }

            //else if (Convert.ToInt32(mcmblocation.SelectedValue) < 0)
            //{
            //    MessageBox.Show("Location Need To Be Specifay", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    mcmblocation.Focus();
            //    return false;
            //}

            return true;
        }
       
        private void mbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {
                    BllEmployee bllEmployee = new BllEmployee();
                    DmnEmployee dmnEmployee = new DmnEmployee();
                    dmnEmployee.EmployeeId = EmployeeId;
                    dmnEmployee.EmployeeCode = txtempcode.Text;
                    dmnEmployee.EmployeeName = txtEmpName.Text;
                    dmnEmployee.EmployeeAddress = txtEmpAddress.Text;
                    dmnEmployee.Gender = Convert.ToInt32(cboGender.SelectedValue);
                    dmnEmployee.DesignationId = Convert.ToInt32(mcmbDesignationId.SelectedValue);
                    dmnEmployee.DepartmentId = Convert.ToInt32(mcmbDepartmentId.SelectedValue);
                    dmnEmployee.DOB = dtpBirthDate.Value;
                    dmnEmployee.Mobile = txtphone.Text;
                    dmnEmployee.Email = txtemail.Text;
                    dmnEmployee.ICardNo = txtCardnumber.Text;
                    dmnEmployee.Remarks = txtnote.Text;
                    dmnEmployee.ESIDate = dtpBirthDate.MinDate;
                    dmnEmployee.PFDate = dtpBirthDate.MinDate;
                    dmnEmployee.TimeIn = dtpBirthDate.MinDate;
                    dmnEmployee.TimeOut = dtpBirthDate.MinDate;
                    dmnEmployee.DateOfJoin = dtpBirthDate.MinDate;
                    dmnEmployee.DateOfResign = dtpBirthDate.MinDate;
                    dmnEmployee.BranchId = 1030;
                    dmnEmployee.RFID_Login_Status = Convert.ToInt32(chk_Rfid_Login.CheckState);
                    dmnEmployee.Status = Convert.ToInt32(chkactive.CheckState);
                    dmnEmployee.ModifiedBy = 1;
                    dmnEmployee.CreatedBy = 1;
                    //dmnEmployee.UserAccountTypeId = Convert.ToInt32(mcmbaccounttype.SelectedValue);
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
                    if (EmployeeId == 0)
                    {
                        var saveStatus = bllEmployee.AddData("prdInsertEmployee", dmnEmployee);
                        EmployeeId = saveStatus.EmployeeId;
                        BllDepartment bllDepartment = new BllDepartment();
                        DmnDepartment dmnDepartment = new DmnDepartment();
                        dmnDepartment.DepartmentId = Convert.ToInt64(mcmbDepartmentId.SelectedValue);
                        var lstDataSrlno = bllDepartment.UpdateSrl("prdUpdateDepartEmpSrl", dmnDepartment);

                        if (pbCust.BackgroundImage != null)
                        {
                            // string Compressedimg = Application.StartupPath + "\\CompressedImg";
                            bool status = clsDocument.CopyFile(Compressedimg + "\\Image.jpeg", dmnEmployee.EmployeeCode.ToString() + ".jpeg", DocumentType.EmployeePhoto);
                            if (status == false)
                            {
                                CMessageBox.Show("Unable To Upload Image Please Try Again ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }
                        }
                    }
                    else
                    {
                        bool saveStatus = bllEmployee.UpdateData("prdUpdateEmployee", dmnEmployee);
                        // 
                        if (pbCust.BackgroundImage != null)
                        {
                            bitmap = new Bitmap(pbCust.BackgroundImage);
                            bool imageExist = clsDocument.FileExistOrNot(dmnEmployee.EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                            if (imageExist)
                            {
                                clsDocument.DeleteImage(dmnEmployee.EmployeeCode.ToString() + ".jpeg", DocumentType.EmployeePhoto);
                            }
                            bitmap.Save(Compressedimg + "\\Image.jpeg");
                            bool status = clsDocument.CopyFile(Compressedimg + "\\Image.jpeg", dmnEmployee.EmployeeCode.ToString() + ".jpeg", DocumentType.EmployeePhoto);
                            if (status == false)
                            {
                                CMessageBox.Show(this, "Unable To Upload Image Please Try Again ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }


                        }
                        else
                        {
                            bool imageExist = clsDocument.FileExistOrNot(dmnEmployee.EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                            if (imageExist)
                            {
                                clsDocument.DeleteImage(dmnEmployee.EmployeeCode.ToString() + ".jpeg", DocumentType.EmployeePhoto);
                            }

                        }

                    }
                    #region Inserting Roles
                    if (cmbUsreAccount.SelectedIndex > 0)
                    {
                        DmnForms dmnForms = new DmnForms();
                        BllUserAccount bllUserAccount = new BllUserAccount();
                        if (cmbUsreAccount.SelectedIndex >= 0)
                        {
                            bool flag = false;
                            foreach (DataGridViewRow row in dgvforms.Rows)
                            {
                                if (Convert.ToInt32(dgvforms.Rows[row.Index].Cells[iread].Value) == 1 || Convert.ToInt32(dgvforms.Rows[row.Index].Cells[iread].Value) == 1)
                                {
                                    flag = true;
                                    break;
                                }


                            }
                            if (flag == true && MandatoryCheck())
                            {
                                dmnForms.CreatedBy = Convert.ToInt32(FrmLogin.UserId);
                                dmnForms.UserType = Convert.ToInt32(cmbUsreAccount.SelectedValue);
                                //if (cmbapptype.Text == "BackOffice")
                                //    dmnForms.FormType = 1;
                                //else
                                //    dmnForms.FormType = 2;
                                for (int i = 0; i < dgvforms.Rows.Count; i++)
                                {

                                    dmnForms.id = Convert.ToInt32(dgvforms.Rows[i].Cells[iid].Value);
                                    dmnForms.Form = Convert.ToInt32(dgvforms.Rows[i].Cells[iformid].Value);
                                    dmnForms.Read = Convert.ToInt32(dgvforms.Rows[i].Cells[iread].Value);
                                    dmnForms.Write = Convert.ToInt32(dgvforms.Rows[i].Cells[iwrite].Value);
                                    dmnForms.Note = txtUserTypeNote.Text.ToString();
                                    if (dmnForms.Read == 0 && dmnForms.Write == 0)
                                    {
                                        dmnForms.Status = 0;
                                    }
                                    else
                                        dmnForms.Status = 1;

                                    bool status = bllUserAccount.AddUserPermission("prdInsertUpdUserPermission", dmnForms);

                                }
                            }
                            else
                            {
                                CMessageBox.Show(" Need To Assign One Permission Atleast", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }
                        }
                        else
                        {
                            CMessageBox.Show(" Please Select The Account Group", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                    }
                    #endregion
                    #region User Registartion

                    FrmUserAccount frmUserAccount = new FrmUserAccount();
                    if (txtuname.Text != "")
                    {
                        if (frmUserAccount.MandatoryCheck())
                        {
                            BllUserAccount bllUserAccount = new BllUserAccount();
                            DmnUserAccount dmnUserAccount = new DmnUserAccount();
                            BllEncryption bllEncryption = new BllEncryption();
                            dmnUserAccount.UserAccountId = UserAccountId;
                            dmnUserAccount.UserAccountTypeId = Convert.ToInt32(mcmbaccounttype.SelectedValue);
                            dmnUserAccount.Username = txtuname.Text;
                            dmnUserAccount.BranchId = 1030;
                            dmnUserAccount.CreatedBy = FrmLogin.UserId;
                            dmnUserAccount.EmployeeId = EmployeeId;
                            dmnUserAccount.Status = Convert.ToInt32(chkstatus.CheckState);
                            DmnUserAccount userAccount;
                            if (txtpassword.Text.Trim() != "" && txtcconpassword.Text.Trim() != "")
                            {
                                bllEncryption.encrptPassword(txtpassword.Text, out PasswordSalt, out PasswordHash);
                                dmnUserAccount.PasswordSalt = PasswordSalt;
                                dmnUserAccount.PasswordHash = PasswordHash;
                            }
                            else
                            {
                                dmnUserAccount.PasswordSalt = PasswordSalt;
                                dmnUserAccount.PasswordHash = PasswordHash;
                            }
                            if (dmnUserAccount.UserAccountId == 0)
                            {

                                userAccount = bllUserAccount.AddData("prdInsertUserAccount", dmnUserAccount);
                                if (userAccount.UserAccountId != 0)
                                {
                                    CMessageBox.Show("User Account Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    clear();
                                }
                                else
                                {
                                    CMessageBox.Show("Unable To Save ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }

                            }
                            else
                            {

                                bool saveStatus = bllUserAccount.UpdateData("prdUpdateUserAccount", dmnUserAccount);
                                if (saveStatus)
                                {
                                    CMessageBox.Show("User Account Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    clear();
                                }
                                else
                                {
                                    CMessageBox.Show("Unable To Save ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }

                    #endregion

                    EmployeeId = 0; UserAccountId = 0;
                    //MessageBox.Show("Successfully Saved " + txtEmpName.Text, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clear();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.ToString());
            }
        }
        public void fillcmbcame()
        {
            try
            {
                //FilterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                //foreach (FilterInfo filterInfo in FilterInfoCollection)
                //    cmbcam.Items.Add(filterInfo.);
                //cmbcam.SelectedIndex = 0;
                //VideoCaptureDevice = new VideoCaptureDevice();
            }
            catch (Exception ex)
            {

            }
        }
        public void clear()
        {
            dgvforms.Visible = false;
            chk_Rfid_Login.Checked = false;
            txtempcode.Text = "";
            txtEmpName.Text = "";
            mcmbDesignationId.Text = "";
            mcmbDepartmentId.Text = "";
            txtEmpAddress.Text = "";
            cboGender.Text = "";
            txtphone.Text = "";
            txtemail.Text = "";
            txtCardnumber.Text = "";
            txtICardNo1.Text = "";
            txtnote.Text = "";
            pbCust.BackgroundImage = null;
            //cmbapptype.SelectedIndex = 0;
            //pbCust.Image = Game.Properties.Resources.webcam;
            pbCust.SizeMode = PictureBoxSizeMode.StretchImage;
            fillcmbcame();
            FillCombo();
            FillComboUserType();
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
            cmbUsreAccount.SelectedIndex = 0;
            txtUserTypeNote.Text = "";
            dgvforms.Rows.Clear();
            EmployeeId = 0;
            UserAccountId = 0;
            mcmbaccounttype.SelectedIndex = 0;
            PasswordHash = null;
            PasswordSalt = null;
            txtpassword.Text = "";
            txtcconpassword.Text = "";
            txtuname.Text = "";
            LocationFill();
            //mcmblocation.SelectedValue = Convert.ToInt64(FrmDashBoard.BranchId);
        }
        public void LocationFill()
        {
            //location
            BllSearch<DmnComboBox> blllocation = new BllSearch<DmnComboBox>();
            DmnSearch dmnlocation = new DmnSearch();
            dmnlocation.Select = "LocId,Name";
            dmnlocation.Table = " tblLocationDetails ";
            dmnlocation.WhereNdGroupBy = "Where Status = 1 ";//AND DepartmentId = " + mcmbDepartmentId.SelectedValue;
            dmnlocation.OrderBy = "order by Value";
            //var location = blllocation.GetSearchData("prdComboFill", dmnlocation);
            //mcmblocation.DataSource = location;
            //mcmblocation.DisplayMember = "Value";
            //mcmblocation.ValueMember = "Id";
        }
        private void mbtnRefresh_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void cmbUsreAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {

            if (cmbUsreAccount.SelectedIndex <= 0)
            {
                dgvforms.Rows.Clear();
            }
            else
            {
                dgvforms.Visible = true;
                FillForms(Convert.ToInt32(cmbUsreAccount.SelectedValue));
            }


        }

        private void cmbUsreAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbapptype_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillForms(Convert.ToInt32(cmbUsreAccount.SelectedValue));
        }

        private void txtUserTypeNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void dgvforms_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == iwrite && Convert.ToInt32(dgvforms.Rows[e.RowIndex].Cells[iwrite].Value) == 1)
            {
                dgvforms.Rows[e.RowIndex].Cells[iread].Value = 1;
            }
            if (e.ColumnIndex == iread && Convert.ToInt32(dgvforms.Rows[e.RowIndex].Cells[iwrite].Value) == 1)
            {
                dgvforms.Rows[e.RowIndex].Cells[iread].Value = 1;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {
                    BllEmployee bllEmployee = new BllEmployee();
                    DmnEmployee dmnEmployee = new DmnEmployee();
                    dmnEmployee.EmployeeId = EmployeeId;
                    dmnEmployee.EmployeeCode = txtempcode.Text;
                    dmnEmployee.EmployeeName = txtEmpName.Text;
                    dmnEmployee.EmployeeAddress = txtEmpAddress.Text;
                    dmnEmployee.Gender = Convert.ToInt32(cboGender.SelectedValue);
                    dmnEmployee.DesignationId = Convert.ToInt32(mcmbDesignationId.SelectedValue);
                    dmnEmployee.DepartmentId = Convert.ToInt32(mcmbDepartmentId.SelectedValue);
                    dmnEmployee.DOB = dtpBirthDate.Value;
                    dmnEmployee.Mobile = txtphone.Text;
                    dmnEmployee.Email = txtemail.Text;
                    dmnEmployee.ICardNo = txtCardnumber.Text;
                    dmnEmployee.Remarks = txtnote.Text;
                    dmnEmployee.ESIDate = dtpBirthDate.MinDate;
                    dmnEmployee.PFDate = dtpBirthDate.MinDate;
                    dmnEmployee.TimeIn = dtpBirthDate.MinDate;
                    dmnEmployee.TimeOut = dtpBirthDate.MinDate;
                    dmnEmployee.DateOfJoin = dtpBirthDate.MinDate;
                    dmnEmployee.DateOfResign = dtpBirthDate.MinDate;
                    dmnEmployee.BranchId =1030;
                    dmnEmployee.RFID_Login_Status = Convert.ToInt32(chk_Rfid_Login.CheckState);
                    dmnEmployee.Status = Convert.ToInt32(chkactive.CheckState);
                    dmnEmployee.ModifiedBy = FrmLogin.UserId;
                    dmnEmployee.CreatedBy = FrmLogin.UserId;
                    //dmnEmployee.UserAccountTypeId = Convert.ToInt32(mcmbaccounttype.SelectedValue);
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
                    if (EmployeeId == 0)
                    {
                        var saveStatus = bllEmployee.AddData("prdInsertEmployee", dmnEmployee);
                        EmployeeId = saveStatus.EmployeeId;
                        BllDepartment bllDepartment = new BllDepartment();
                        DmnDepartment dmnDepartment = new DmnDepartment();
                        dmnDepartment.DepartmentId = Convert.ToInt64(mcmbDepartmentId.SelectedValue);
                        var lstDataSrlno = bllDepartment.UpdateSrl("prdUpdateDepartEmpSrl", dmnDepartment);

                        if (pbCust.BackgroundImage != null)
                        {
                            string Compressedimg = Application.StartupPath + "\\CompressedImg";
                            bool status = clsDocument.CopyFile(Compressedimg + "\\Image.jpeg", dmnEmployee.EmployeeCode.ToString() + ".jpeg", DocumentType.EmployeePhoto);
                            if (status == false)
                            {
                                CMessageBox.Show("Unable To Upload Image Please Try Again ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }
                        }
                    }
                    else
                    {
                        bool saveStatus = bllEmployee.UpdateData("prdUpdateEmployee", dmnEmployee);
                        // 
                        if (pbCust.BackgroundImage != null)
                        {
                            bitmap = new Bitmap(pbCust.BackgroundImage);
                            bool imageExist = clsDocument.FileExistOrNot(dmnEmployee.EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                            if (imageExist)
                            {
                                clsDocument.DeleteImage(dmnEmployee.EmployeeCode.ToString() + ".jpeg", DocumentType.EmployeePhoto);
                            }
                            bitmap.Save(Compressedimg + "\\Image.jpeg");
                            bool status = clsDocument.CopyFile(Compressedimg + "\\Image.jpeg", dmnEmployee.EmployeeCode.ToString() + ".jpeg", DocumentType.EmployeePhoto);
                            if (status == false)
                            {
                                CMessageBox.Show(this, "Unable To Upload Image Please Try Again ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }


                        }
                        else
                        {
                            bool imageExist = clsDocument.FileExistOrNot(dmnEmployee.EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                            if (imageExist)
                            {
                                clsDocument.DeleteImage(dmnEmployee.EmployeeCode.ToString() + ".jpeg", DocumentType.EmployeePhoto);
                            }

                        }

                    }
                    #region Inserting Roles
                    if (cmbUsreAccount.SelectedIndex > 0)
                    {
                        DmnForms dmnForms = new DmnForms();
                        BllUserAccount bllUserAccount = new BllUserAccount();
                        if (cmbUsreAccount.SelectedIndex >= 0)
                        {
                            bool flag = false;
                            foreach (DataGridViewRow row in dgvforms.Rows)
                            {
                                if (Convert.ToInt32(dgvforms.Rows[row.Index].Cells[iread].Value) == 1 || Convert.ToInt32(dgvforms.Rows[row.Index].Cells[iwrite].Value) == 1)
                                {
                                    flag = true;
                                    break;
                                }


                            }
                            if (flag == true && MandatoryCheck())
                            {
                                dmnForms.CreatedBy = 1;
                                dmnForms.UserType = Convert.ToInt32(cmbUsreAccount.SelectedValue);
                                //if (cmbapptype.Text == "BackOffice")
                                //    dmnForms.FormType = 1;
                                //else
                                //    dmnForms.FormType = 2;
                                for (int i = 0; i < dgvforms.Rows.Count; i++)
                                {

                                    dmnForms.id = Convert.ToInt32(dgvforms.Rows[i].Cells[iid].Value);
                                    dmnForms.Form = Convert.ToInt32(dgvforms.Rows[i].Cells[iformid].Value);
                                    dmnForms.Read = Convert.ToInt32(dgvforms.Rows[i].Cells[iread].Value);
                                    dmnForms.Write = Convert.ToInt32(dgvforms.Rows[i].Cells[iwrite].Value);
                                    dmnForms.Note = txtUserTypeNote.Text.ToString();
                                    if (dmnForms.Read == 0 && dmnForms.Write == 0)
                                    {
                                        dmnForms.Status = 0;
                                    }
                                    else
                                        dmnForms.Status = 1;

                                    bool status = bllUserAccount.AddUserPermission("prdInsertUpdUserPermission", dmnForms);

                                }
                            }
                            else
                            {
                                CMessageBox.Show(" Need To Assign One Permission Atleast", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }
                        }
                        else
                        {
                            CMessageBox.Show(" Please Select The Account Group", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                    }
                    #endregion
                    #region User Registartion

                    FrmUserAccount frmUserAccount = new FrmUserAccount();
                    if (txtuname.Text != "")
                    {
                        if (frmUserAccount.MandatoryCheck())
                        {
                            BllUserAccount bllUserAccount = new BllUserAccount();
                            DmnUserAccount dmnUserAccount = new DmnUserAccount();
                            BllEncryption bllEncryption = new BllEncryption();
                            dmnUserAccount.UserAccountId = UserAccountId;
                            dmnUserAccount.UserAccountTypeId = Convert.ToInt32(mcmbaccounttype.SelectedValue);
                            dmnUserAccount.Username = txtuname.Text;
                            dmnUserAccount.BranchId = 1030;
                            dmnUserAccount.CreatedBy = FrmLogin.UserId;
                            dmnUserAccount.EmployeeId = EmployeeId;
                            dmnUserAccount.Status = Convert.ToInt32(chkstatus.CheckState);
                            DmnUserAccount userAccount;
                            if (txtpassword.Text.Trim() != "" && txtcconpassword.Text.Trim() != "")
                            {
                                bllEncryption.encrptPassword(txtpassword.Text, out PasswordSalt, out PasswordHash);
                                dmnUserAccount.PasswordSalt = PasswordSalt;
                                dmnUserAccount.PasswordHash = PasswordHash;
                            }
                            else
                            {
                                dmnUserAccount.PasswordSalt = PasswordSalt;
                                dmnUserAccount.PasswordHash = PasswordHash;
                            }
                            if (dmnUserAccount.UserAccountId == 0)
                            {

                                userAccount = bllUserAccount.AddData("prdInsertUserAccount", dmnUserAccount);
                                if (userAccount.UserAccountId != 0)
                                {
                                    CMessageBox.Show("User Account Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    clear();
                                }
                                else
                                {
                                    CMessageBox.Show("Unable To Save ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }

                            }
                            else
                            {

                                bool saveStatus = bllUserAccount.UpdateData("prdUpdateUserAccount", dmnUserAccount);
                                if (saveStatus)
                                {
                                    CMessageBox.Show("User Account Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    clear();
                                }
                                else
                                {
                                    CMessageBox.Show("Unable To Save ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }

                    #endregion

                    EmployeeId = 0; UserAccountId = 0;
                    //MessageBox.Show("Successfully Saved " + txtEmpName.Text, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clear();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void mbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Employee,Code,Department,Designation";
                _objSearch.Select = "EmployeeId as Id, emp.EmployeeName as Field1,emp.EmployeeCode as Field2,dp.DepartmentName as Field3,ds.DesignationName as Field4";
                _objSearch.Table = "tblEmployee as emp " +
                               "left join tblDepartment as dp on emp.DepartmentId = dp.DepartmentId " +
                               "left join tblDesignation as ds on emp.DesignationId = ds.DesignationId";
                _objSearch.WhereNdGroupBy = "Where emp.Status=1 and emp.BranchId = " + Convert.ToInt32(FrmLogin.BranchId);

                _objSearch.OrderBy = " Order By EmployeeName ASC ";
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);

                    }
                }
                if (returnValue.ToString() != null && returnValue.ToString() != "")
                {
                    clear();
                    dgvforms.Visible = true;
                    EmployeeId = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
                    DmnSearch objSearch = new DmnSearch();
                    //objSearch.Select = "tblEmployee.*,CONVERT(nvarchar(MAX),TimeIn,108) as sTimeIn,CONVERT(nvarchar(MAX),TimeOut,108) as sTimeOut,CONVERT(nvarchar(MAX),OverTimeStart,108) as sOverTimeStart";
                    objSearch.Select = "e.*,UserAccountId,UserAccountTypeId,Username,PasswordHash,PasswordSalt";
                    objSearch.Table = " tblEmployee e " +
                                      " left join tblUserAccount a on a.EmployeeId=e.EmployeeId ";
                    objSearch.WhereNdGroupBy = "Where e.EmployeeId = " + EmployeeId;
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource.Count > 0)
                    {
                        FillCombo();
                        txtEmpName.Text = gridSource[0].EmployeeName;
                        mcmbDepartmentId.SelectedValue = Convert.ToInt64(gridSource[0].DepartmentId);
                        mcmbDesignationId.SelectedValue = Convert.ToInt64(gridSource[0].DesignationId);
                        txtEmpAddress.Text = gridSource[0].EmployeeAddress;
                        cboGender.SelectedValue = Convert.ToInt64(gridSource[0].Gender);
                        dtpBirthDate.Value = gridSource[0].DOB;
                        txtphone.Text = gridSource[0].Mobile;
                        txtemail.Text = gridSource[0].Email;
                        txtCardnumber.Text = gridSource[0].ICardNo;
                        txtICardNo1.Text = "XXXX XX" + gridSource[0].ICardNo.Substring(gridSource[0].ICardNo.Length - 6, 6);
                        txtnote.Text = gridSource[0].Remarks;
                        txtempcode.Text = gridSource[0].EmployeeCode;
                        if (gridSource[0].Status == 1)
                            chkactive.Checked = true;
                        else
                            chkactive.Checked = false;
                        //mcmblocation.SelectedValue = gridSource[0].BranchId;

                        if (gridSource[0].RFID_Login_Status == 1)
                            chk_Rfid_Login.Checked = true;
                        else
                            chk_Rfid_Login.Checked = false;

                        bool imageExist = clsDocument.FileExistOrNot(gridSource[0].EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                        if (imageExist)
                        {
                            pbCust.BackgroundImage = clsDocument.DownloadImage(gridSource[0].EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                            pbCust.BackgroundImageLayout = ImageLayout.Stretch;
                            pbCust.Image = null;
                        }

                        if (gridSource[0].UserAccountTypeId != null)
                        {
                            UserAccountId = gridSource[0].UserAccountId;
                            cmbUsreAccount.SelectedValue = Convert.ToInt64(gridSource[0].UserAccountTypeId);
                            FillForms(Convert.ToInt32(gridSource[0].UserAccountTypeId));
                            mcmbaccounttype.SelectedValue = Convert.ToInt64(gridSource[0].UserAccountTypeId);
                            txtuname.Text = gridSource[0].Username;
                            PasswordSalt = gridSource[0].PasswordSalt;
                            PasswordHash = gridSource[0].PasswordHash;
                            txtpassword.Text = "";
                            txtcconpassword.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mcmbaccounttype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtuname_Leave(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblUserAccount ";
                objSearch.WhereNdGroupBy = " Where Status=1 and Username='" + txtuname.Text + "'";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    CMessageBox.Show("This User Name Is Alredy Exist", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtuname.Text = "";
                    txtuname.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtuname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtcconpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void chkshowpassword_CheckedChanged(object sender, EventArgs e)
        {
            txtpassword.PasswordChar = chkshowpassword.Checked ? '\0' : '*';
            txtcconpassword.PasswordChar = chkshowpassword.Checked ? '\0' : '*';
        }
        public bool MandatoryCheckuser()
        {
            if (txtusertype.Text == "" || txtusertype.Text == null)
            {
                CMessageBox.Show("Please Enter UserType", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public void FillComboUserType()
        {
            BllSearch<DmnComboBox> bllacctype = new BllSearch<DmnComboBox>();
            DmnSearch dmnacctype = new DmnSearch();
            dmnacctype.Select = "UserAccountTypeId,UserAccountType";
            dmnacctype.Table = " tblUserAccountType ";
            dmnacctype.WhereNdGroupBy = "Where Status = 1 ";//AND DepartmentId = " + mcmbDepartmentId.SelectedValue;
            dmnacctype.OrderBy = "order by Value";
            var acctype = bllacctype.GetSearchData("prdComboFill", dmnacctype);
            cmbUsreAccount.DataSource = acctype;
            cmbUsreAccount.DisplayMember = "Value";
            cmbUsreAccount.ValueMember = "Id";
            cmbUsreAccount.SelectedIndex = 0;

            BllSearch<DmnComboBox> bllacctype1 = new BllSearch<DmnComboBox>();
            DmnSearch dmnacctype1 = new DmnSearch();
            dmnacctype1.Select = "UserAccountTypeId,UserAccountType";
            dmnacctype1.Table = " tblUserAccountType ";
            dmnacctype1.WhereNdGroupBy = "Where Status = 1 ";//AND DepartmentId = " + mcmbDepartmentId.SelectedValue;
            dmnacctype1.OrderBy = "order by Value";
            var acctype1 = bllacctype.GetSearchData("prdComboFill", dmnacctype1);
            mcmbaccounttype.DataSource = acctype1;
            mcmbaccounttype.DisplayMember = "Value";
            mcmbaccounttype.ValueMember = "Id";
            mcmbaccounttype.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void HeaderCheckBoxwrite_Clicked(object sender, EventArgs e)
        {
            dgvforms.EndEdit();
            foreach (DataGridViewRow row in dgvforms.Rows)
            {
                if (headerCheckBoxwrite.Checked == true)
                {
                    dgvforms.Rows[row.Index].Cells[iwrite].Value = true;
                    dgvforms.Rows[row.Index].Cells[iread].Value = true;
                }
                else
                {
                    dgvforms.Rows[row.Index].Cells[iwrite].Value = false;
                    dgvforms.Rows[row.Index].Cells[iread].Value = false;
                }
            }
        }
        private void HeaderCheckBoxread_Clicked(object sender, EventArgs e)
        {
            dgvforms.EndEdit();
            foreach (DataGridViewRow row in dgvforms.Rows)
            {
                if (headerCheckBoxread.Checked == true)
                {
                    //dgvforms.Rows[row.Index].Cells[iwrite].Value = true;
                    dgvforms.Rows[row.Index].Cells[iread].Value = true;
                }
                else
                {
                    //dgvforms.Rows[row.Index].Cells[iwrite].Value = false;
                    dgvforms.Rows[row.Index].Cells[iread].Value = false;
                }
            }
        }

        private void FrmEmployeeShort_Load(object sender, EventArgs e)
        {

            richTextBox1.Rtf = "{\\rtf1\\ansi " +
            "User Type \\i0 *\\ulnone\\i\\b " +
               "}";
            richTextBox1.Select(richTextBox1.Text.IndexOf("*"), 1);
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            richTextBox2.Rtf = "{\\rtf1\\ansi " +
           "User Name \\i0 *\\ulnone\\i\\b " +
              "}";
            richTextBox2.Select(richTextBox2.Text.IndexOf("*"), 1);
            richTextBox2.SelectionColor = Color.Red;
            richTextBox2.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            richTextBox3.Rtf = "{\\rtf1\\ansi " +
          "Password \\i0 *\\ulnone\\i\\b " +
             "}";
            richTextBox3.Select(richTextBox3.Text.IndexOf("*"), 1);
            richTextBox3.SelectionColor = Color.Red;
            richTextBox3.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            richTextBox4.Rtf = "{\\rtf1\\ansi " +
         "Confirm \\i0 *\\ulnone\\i\\b " +
            "}";
            richTextBox4.Select(richTextBox4.Text.IndexOf("*"), 1);
            richTextBox4.SelectionColor = Color.Red;
            richTextBox4.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            dgvforms.BorderStyle = BorderStyle.None;
            dgvforms.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvforms.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvforms.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvforms.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvforms.BackgroundColor = Color.White;

            dgvforms.EnableHeadersVisualStyles = false;
            dgvforms.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvforms.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvforms.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            richTextBoxName.Rtf = "{\\rtf1\\ansi " +
             "Name \\i0 *\\ulnone\\i\\b " +
                "}";
            richTextBoxName.Select(richTextBoxName.Text.IndexOf("*"), 1);
            richTextBoxName.SelectionColor = Color.Red;
            richTextBoxName.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            richTextBoxPhone.Rtf = "{\\rtf1\\ansi " +
             "Phone No \\i0 *\\ulnone\\i\\b " +
                "}";
            richTextBoxPhone.Select(richTextBoxPhone.Text.IndexOf("*"), 1);
            richTextBoxPhone.SelectionColor = Color.Red;
            richTextBoxPhone.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            richTextBoxGender.Rtf = "{\\rtf1\\ansi " +
           "Gender \\i0 *\\ulnone\\i\\b " +
              "}";
            richTextBoxGender.Select(richTextBoxGender.Text.IndexOf("*"), 1);
            richTextBoxGender.SelectionColor = Color.Red;
            richTextBoxGender.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            richTextBoxDesignation.Rtf = "{\\rtf1\\ansi " +
          "Designation \\i0 *\\ulnone\\i\\b " +
             "}";
            richTextBoxDesignation.Select(richTextBoxDesignation.Text.IndexOf("*"), 1);
            richTextBoxDesignation.SelectionColor = Color.Red;
            richTextBoxDesignation.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            richTextBoxDepartment.Rtf = "{\\rtf1\\ansi " +
        "Department \\i0 *\\ulnone\\i\\b " +
           "}";
            richTextBoxDepartment.Select(richTextBoxDepartment.Text.IndexOf("*"), 1);
            richTextBoxDepartment.SelectionColor = Color.Red;
            richTextBoxDepartment.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            richTextBoxEmail.Rtf = "{\\rtf1\\ansi " +
       "Email \\i0 *\\ulnone\\i\\b " +
          "}";
            richTextBoxEmail.Select(richTextBoxEmail.Text.IndexOf("*"), 1);
            richTextBoxEmail.SelectionColor = Color.Red;
            richTextBoxEmail.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            richTextBoxCardno.Rtf = "{\\rtf1\\ansi " +
      "Cardno \\i0 *\\ulnone\\i\\b " +
         "}";
            richTextBoxCardno.Select(richTextBoxCardno.Text.IndexOf("*"), 1);
            richTextBoxCardno.SelectionColor = Color.Red;
            richTextBoxCardno.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            LocationFill();
           
            BranchId = 1030;

            FrmDashboard frmDash = new FrmDashboard();
            if (frmDash.ChkWritePermission(Convert.ToInt32(formsenum.Employee)))
            {
                mbtnSave.Enabled = true;
               
            }
            else
            {
                mbtnSave.Enabled = false;
                

            }

            if (frmDash.ChkWritePermission(Convert.ToInt32(formsenum.User_Permission)))
            {
                mbtnSave.Enabled = true;
                btnusertype.Enabled = true;
                BranchId = 1030;
            }
            else
            {
                mbtnSave.Enabled = false;
                btnusertype.Enabled = false;
                BranchId = 1030;
            }
            if(FrmLogin.UserId==1 && Convert.ToString(FrmLogin.EmployeName) == "test")
            {
                mbtnSave.Enabled = true;
                btnusertype.Enabled = true;
            }

            clear();
            txtEmpName.Focus();


            if (empid != 0)
            {
                BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
                DmnSearch objSearch = new DmnSearch();
                //objSearch.Select = "tblEmployee.*,CONVERT(nvarchar(MAX),TimeIn,108) as sTimeIn,CONVERT(nvarchar(MAX),TimeOut,108) as sTimeOut,CONVERT(nvarchar(MAX),OverTimeStart,108) as sOverTimeStart";
                objSearch.Select = "e.*,UserAccountId,UserAccountTypeId,Username,PasswordHash,PasswordSalt";
                objSearch.Table = " tblEmployee e " +
                                  " left join tblUserAccount a on a.EmployeeId=e.EmployeeId ";
                objSearch.WhereNdGroupBy = "Where e.EmployeeId = " + empid + "e.Branchid = " + 1030;
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                EmployeeId = empid;
                txtEmpName.Text = gridSource[0].EmployeeName;
                mcmbDepartmentId.SelectedValue = gridSource[0].DepartmentId;
                mcmbDesignationId.SelectedValue = gridSource[0].DesignationId;
                txtEmpAddress.Text = gridSource[0].EmployeeAddress;
                cboGender.SelectedValue = Convert.ToInt64(gridSource[0].Gender);
                dtpBirthDate.Value = gridSource[0].DOB;
                txtphone.Text = gridSource[0].Mobile;
                txtemail.Text = gridSource[0].Email;
                txtCardnumber.Text = gridSource[0].ICardNo;
                txtICardNo1.Text = "XXXX XX" + gridSource[0].ICardNo.Substring(gridSource[0].ICardNo.Length - 6, 6);
                txtnote.Text = gridSource[0].Remarks;
                txtempcode.Text = gridSource[0].EmployeeCode;
                if (gridSource[0].Status == 1)
                    chkactive.Checked = true;
                else
                    chkactive.Checked = false;
                //mcmblocation.SelectedValue = gridSource[0].BranchId;

                bool imageExist = clsDocument.FileExistOrNot(gridSource[0].EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                if (imageExist)
                {
                    pbCust.BackgroundImage = clsDocument.DownloadImage(gridSource[0].EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                    pbCust.BackgroundImageLayout = ImageLayout.Stretch;
                    pbCust.Image = null;
                }

                if (gridSource[0].UserAccountTypeId != null)
                {
                    UserAccountId = gridSource[0].UserAccountId;
                    cmbUsreAccount.SelectedValue = Convert.ToInt64(gridSource[0].UserAccountTypeId);
                    FillForms(Convert.ToInt32(gridSource[0].UserAccountTypeId));
                    mcmbaccounttype.SelectedValue = Convert.ToInt64(gridSource[0].UserAccountTypeId);
                    txtuname.Text = gridSource[0].Username;
                    PasswordSalt = gridSource[0].PasswordSalt;
                    PasswordHash = gridSource[0].PasswordHash;
                    txtpassword.Text = "";
                    txtcconpassword.Text = "";
                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            btnrfid_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {
                    BllEmployee bllEmployee = new BllEmployee();
                    DmnEmployee dmnEmployee = new DmnEmployee();
                    dmnEmployee.EmployeeId = EmployeeId;
                    dmnEmployee.EmployeeCode = txtempcode.Text;
                    dmnEmployee.EmployeeName = txtEmpName.Text;
                    dmnEmployee.EmployeeAddress = txtEmpAddress.Text;
                    dmnEmployee.Gender = Convert.ToInt32(cboGender.SelectedValue);
                    dmnEmployee.DesignationId = Convert.ToInt32(mcmbDesignationId.SelectedValue);
                    dmnEmployee.DepartmentId = Convert.ToInt32(mcmbDepartmentId.SelectedValue);
                    dmnEmployee.DOB = dtpBirthDate.Value;
                    dmnEmployee.Mobile = txtphone.Text;
                    dmnEmployee.Email = txtemail.Text;
                    dmnEmployee.ICardNo = txtCardnumber.Text;
                    dmnEmployee.Remarks = txtnote.Text;
                    dmnEmployee.ESIDate = dtpBirthDate.MinDate;
                    dmnEmployee.PFDate = dtpBirthDate.MinDate;
                    dmnEmployee.TimeIn = dtpBirthDate.MinDate;
                    dmnEmployee.TimeOut = dtpBirthDate.MinDate;
                    dmnEmployee.DateOfJoin = dtpBirthDate.MinDate;
                    dmnEmployee.DateOfResign = dtpBirthDate.MinDate;
                    dmnEmployee.BranchId = 1030;
                    dmnEmployee.RFID_Login_Status = Convert.ToInt32(chk_Rfid_Login.CheckState);
                    dmnEmployee.Status = Convert.ToInt32(chkactive.CheckState);
                    dmnEmployee.ModifiedBy = FrmLogin.UserId;
                    dmnEmployee.CreatedBy = FrmLogin.UserId;
                    //dmnEmployee.UserAccountTypeId = Convert.ToInt32(mcmbaccounttype.SelectedValue);
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
                    if (EmployeeId == 0)
                    {
                        var saveStatus = bllEmployee.AddData("prdInsertEmployee", dmnEmployee);
                        EmployeeId = saveStatus.EmployeeId;
                        BllDepartment bllDepartment = new BllDepartment();
                        DmnDepartment dmnDepartment = new DmnDepartment();
                        dmnDepartment.DepartmentId = Convert.ToInt64(mcmbDepartmentId.SelectedValue);
                        var lstDataSrlno = bllDepartment.UpdateSrl("prdUpdateDepartEmpSrl", dmnDepartment);

                        if (pbCust.BackgroundImage != null)
                        {
                            // string Compressedimg = Application.StartupPath + "\\CompressedImg";
                            bool status = clsDocument.CopyFile(Compressedimg + "\\Image.jpeg", dmnEmployee.EmployeeCode.ToString() + ".jpeg", DocumentType.EmployeePhoto);
                            if (status == false)
                            {
                                CMessageBox.Show("Unable To Upload Image Please Try Again ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }
                        }
                    }
                    else
                    {
                        bool saveStatus = bllEmployee.UpdateData("prdUpdateEmployee", dmnEmployee);
                        // 
                        if (pbCust.BackgroundImage != null)
                        {
                            bitmap = new Bitmap(pbCust.BackgroundImage);
                            bool imageExist = clsDocument.FileExistOrNot(dmnEmployee.EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                            if (imageExist)
                            {
                                clsDocument.DeleteImage(dmnEmployee.EmployeeCode.ToString() + ".jpeg", DocumentType.EmployeePhoto);
                            }
                            bitmap.Save(Compressedimg + "\\Image.jpeg");
                            bool status = clsDocument.CopyFile(Compressedimg + "\\Image.jpeg", dmnEmployee.EmployeeCode.ToString() + ".jpeg", DocumentType.EmployeePhoto);
                            if (status == false)
                            {
                                CMessageBox.Show(this, "Unable To Upload Image Please Try Again ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            }


                        }
                        else
                        {
                            bool imageExist = clsDocument.FileExistOrNot(dmnEmployee.EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                            if (imageExist)
                            {
                                clsDocument.DeleteImage(dmnEmployee.EmployeeCode.ToString() + ".jpeg", DocumentType.EmployeePhoto);
                            }

                        }

                    }
                    #region Inserting Roles
                    if (cmbUsreAccount.SelectedIndex > 0)
                    {
                        DmnForms dmnForms = new DmnForms();
                        BllUserAccount bllUserAccount = new BllUserAccount();
                        if (cmbUsreAccount.SelectedIndex >= 0)
                        {
                            bool flag = false;
                            foreach (DataGridViewRow row in dgvforms.Rows)
                            {
                                if (Convert.ToInt32(dgvforms.Rows[row.Index].Cells[iread].Value) == 1 || Convert.ToInt32(dgvforms.Rows[row.Index].Cells[iread].Value) == 1)
                                {
                                    flag = true;
                                    break;
                                }


                            }
                            if (flag == true && MandatoryCheck())
                            {
                                dmnForms.CreatedBy = 1;
                                dmnForms.UserType = Convert.ToInt32(cmbUsreAccount.SelectedValue);
                                //if (cmbapptype.Text == "BackOffice")
                                //    dmnForms.FormType = 1;
                                //else
                                //    dmnForms.FormType = 2;
                                for (int i = 0; i < dgvforms.Rows.Count; i++)
                                {

                                    dmnForms.id = Convert.ToInt32(dgvforms.Rows[i].Cells[iid].Value);
                                    dmnForms.Form = Convert.ToInt32(dgvforms.Rows[i].Cells[iformid].Value);
                                    dmnForms.Read = Convert.ToInt32(dgvforms.Rows[i].Cells[iread].Value);
                                    dmnForms.Write = Convert.ToInt32(dgvforms.Rows[i].Cells[iwrite].Value);
                                    dmnForms.Note = txtUserTypeNote.Text.ToString();
                                    if (dmnForms.Read == 0 && dmnForms.Write == 0)
                                    {
                                        dmnForms.Status = 0;
                                    }
                                    else
                                        dmnForms.Status = 1;

                                    bool status = bllUserAccount.AddUserPermission("prdInsertUpdUserPermission", dmnForms);

                                }
                            }
                            else
                            {
                                CMessageBox.Show(" Need To Assign One Permission Atleast", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }
                        }
                        else
                        {
                            CMessageBox.Show(" Please Select The Account Group", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                    }
                    #endregion
                    #region User Registartion

                    FrmUserAccount frmUserAccount = new FrmUserAccount();
                    if (txtuname.Text != "")
                    {
                        if (frmUserAccount.MandatoryCheck())
                        {
                            BllUserAccount bllUserAccount = new BllUserAccount();
                            DmnUserAccount dmnUserAccount = new DmnUserAccount();
                            BllEncryption bllEncryption = new BllEncryption();
                            dmnUserAccount.UserAccountId = UserAccountId;
                            dmnUserAccount.UserAccountTypeId = Convert.ToInt32(mcmbaccounttype.SelectedValue);
                            dmnUserAccount.Username = txtuname.Text;
                            dmnUserAccount.BranchId = 1030;
                            dmnUserAccount.CreatedBy = 1;
                            dmnUserAccount.EmployeeId = EmployeeId;
                            dmnUserAccount.Status = Convert.ToInt32(chkstatus.CheckState);
                            DmnUserAccount userAccount;
                            if (txtpassword.Text.Trim() != "" && txtcconpassword.Text.Trim() != "")
                            {
                                bllEncryption.encrptPassword(txtpassword.Text, out PasswordSalt, out PasswordHash);
                                dmnUserAccount.PasswordSalt = PasswordSalt;
                                dmnUserAccount.PasswordHash = PasswordHash;
                            }
                            else
                            {
                                dmnUserAccount.PasswordSalt = PasswordSalt;
                                dmnUserAccount.PasswordHash = PasswordHash;
                            }
                            if (dmnUserAccount.UserAccountId == 0)
                            {

                                userAccount = bllUserAccount.AddData("prdInsertUserAccount", dmnUserAccount);
                                if (userAccount.UserAccountId != 0)
                                {
                                    CMessageBox.Show("User Account Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    clear();
                                }
                                else
                                {
                                    CMessageBox.Show("Unable To Save ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }

                            }
                            else
                            {

                                bool saveStatus = bllUserAccount.UpdateData("prdUpdateUserAccount", dmnUserAccount);
                                if (saveStatus)
                                {
                                    CMessageBox.Show("User Account Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    clear();
                                }
                                else
                                {
                                    CMessageBox.Show("Unable To Save ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }

                    #endregion

                    EmployeeId = 0; UserAccountId = 0;
                    //MessageBox.Show("Successfully Saved " + txtEmpName.Text, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clear();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.ToString());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Employee,Code,Department,Designation";
                _objSearch.Select = "EmployeeId as Id, emp.EmployeeName as Field1,emp.EmployeeCode as Field2,dp.DepartmentName as Field3,ds.DesignationName as Field4";
                _objSearch.Table = "tblEmployee as emp " +
                               "left join tblDepartment as dp on emp.DepartmentId = dp.DepartmentId " +
                               "left join tblDesignation as ds on emp.DesignationId = ds.DesignationId";
                _objSearch.WhereNdGroupBy = "Where emp.Status=1 and emp.BranchId = " + 1030;

                _objSearch.OrderBy = " Order By EmployeeName ASC ";
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);

                    }
                }
                if (returnValue.ToString() != null && returnValue.ToString() != "")
                {
                    clear();
                    dgvforms.Visible = true;
                    EmployeeId = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
                    DmnSearch objSearch = new DmnSearch();
                    //objSearch.Select = "tblEmployee.*,CONVERT(nvarchar(MAX),TimeIn,108) as sTimeIn,CONVERT(nvarchar(MAX),TimeOut,108) as sTimeOut,CONVERT(nvarchar(MAX),OverTimeStart,108) as sOverTimeStart";
                    objSearch.Select = "e.*,UserAccountId,UserAccountTypeId,Username,PasswordHash,PasswordSalt";
                    objSearch.Table = " tblEmployee e " +
                                      " left join tblUserAccount a on a.EmployeeId=e.EmployeeId ";
                    objSearch.WhereNdGroupBy = "Where e.EmployeeId = " + EmployeeId;
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource.Count > 0)
                    {
                        FillCombo();
                        txtEmpName.Text = gridSource[0].EmployeeName;
                        mcmbDepartmentId.SelectedValue = Convert.ToInt64(gridSource[0].DepartmentId);
                        mcmbDesignationId.SelectedValue = Convert.ToInt64(gridSource[0].DesignationId);
                        txtEmpAddress.Text = gridSource[0].EmployeeAddress;
                        cboGender.SelectedValue = Convert.ToInt64(gridSource[0].Gender);
                        dtpBirthDate.Value = gridSource[0].DOB;
                        txtphone.Text = gridSource[0].Mobile;
                        txtemail.Text = gridSource[0].Email;
                        txtCardnumber.Text = gridSource[0].ICardNo;
                        txtICardNo1.Text = "XXXX XX" + gridSource[0].ICardNo.Substring(gridSource[0].ICardNo.Length - 6, 6);
                        txtnote.Text = gridSource[0].Remarks;
                        txtempcode.Text = gridSource[0].EmployeeCode;
                        if (gridSource[0].Status == 1)
                            chkactive.Checked = true;
                        else
                            chkactive.Checked = false;
                        BranchId = gridSource[0].BranchId;

                        if (gridSource[0].RFID_Login_Status == 1)
                            chk_Rfid_Login.Checked = true;
                        else
                            chk_Rfid_Login.Checked = false;

                        bool imageExist = clsDocument.FileExistOrNot(gridSource[0].EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                        if (imageExist)
                        {
                            pbCust.BackgroundImage = clsDocument.DownloadImage(gridSource[0].EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                            pbCust.BackgroundImageLayout = ImageLayout.Stretch;
                            pbCust.Image = null;
                        }

                        if (gridSource[0].UserAccountTypeId != null)
                        {
                            UserAccountId = gridSource[0].UserAccountId;
                            cmbUsreAccount.SelectedValue = Convert.ToInt64(gridSource[0].UserAccountTypeId);
                            FillForms(Convert.ToInt32(gridSource[0].UserAccountTypeId));
                            mcmbaccounttype.SelectedValue = Convert.ToInt64(gridSource[0].UserAccountTypeId);
                            txtuname.Text = gridSource[0].Username;
                            PasswordSalt = gridSource[0].PasswordSalt;
                            PasswordHash = gridSource[0].PasswordHash;
                            txtpassword.Text = "";
                            txtcconpassword.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Employee,Code,Department,Designation";
                _objSearch.Select = "EmployeeId as Id, emp.EmployeeName as Field1,emp.EmployeeCode as Field2,dp.DepartmentName as Field3,ds.DesignationName as Field4";
                _objSearch.Table = "tblEmployee as emp " +
                               "left join tblDepartment as dp on emp.DepartmentId = dp.DepartmentId " +
                               "left join tblDesignation as ds on emp.DesignationId = ds.DesignationId";
                _objSearch.WhereNdGroupBy = "Where emp.Status=1 and emp.BranchId = " + 1030;
                _objSearch.OrderBy = " Order By EmployeeName ASC ";

                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);

                    }
                }
                if (returnValue.ToString() != null && returnValue.ToString() != "")
                {
                    clear();
                    dgvforms.Visible = true;
                    EmployeeId = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
                    DmnSearch objSearch = new DmnSearch();
                    //objSearch.Select = "tblEmployee.*,CONVERT(nvarchar(MAX),TimeIn,108) as sTimeIn,CONVERT(nvarchar(MAX),TimeOut,108) as sTimeOut,CONVERT(nvarchar(MAX),OverTimeStart,108) as sOverTimeStart";
                    objSearch.Select = "e.*,UserAccountId,UserAccountTypeId,Username,PasswordHash,PasswordSalt";
                    objSearch.Table = " tblEmployee e " +
                                      " left join tblUserAccount a on a.EmployeeId=e.EmployeeId ";
                    objSearch.WhereNdGroupBy = "Where e.EmployeeId = " + EmployeeId;
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource.Count > 0)
                    {
                        FillCombo();
                        txtEmpName.Text = gridSource[0].EmployeeName;
                        mcmbDepartmentId.SelectedValue = Convert.ToInt64(gridSource[0].DepartmentId);
                        mcmbDesignationId.SelectedValue = Convert.ToInt64(gridSource[0].DesignationId);
                        txtEmpAddress.Text = gridSource[0].EmployeeAddress;
                        cboGender.SelectedValue = Convert.ToInt64(gridSource[0].Gender);
                        dtpBirthDate.Value = gridSource[0].DOB;
                        txtphone.Text = gridSource[0].Mobile;
                        txtemail.Text = gridSource[0].Email;
                        txtCardnumber.Text = gridSource[0].ICardNo;
                        txtICardNo1.Text = "XXXX XX" + gridSource[0].ICardNo.Substring(gridSource[0].ICardNo.Length - 6, 6);
                        txtnote.Text = gridSource[0].Remarks;
                        txtempcode.Text = gridSource[0].EmployeeCode;
                        if (gridSource[0].Status == 1)
                            chkactive.Checked = true;
                        else
                            chkactive.Checked = false;
                        BranchId = gridSource[0].BranchId;

                        if (gridSource[0].RFID_Login_Status == 1)
                            chk_Rfid_Login.Checked = true;
                        else
                            chk_Rfid_Login.Checked = false;

                        bool imageExist = clsDocument.FileExistOrNot(gridSource[0].EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                        if (imageExist)
                        {
                            pbCust.BackgroundImage = clsDocument.DownloadImage(gridSource[0].EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                            pbCust.BackgroundImageLayout = ImageLayout.Stretch;
                            pbCust.Image = null;
                        }

                        if (gridSource[0].UserAccountTypeId != null)
                        {
                            UserAccountId = gridSource[0].UserAccountId;
                            cmbUsreAccount.SelectedValue = Convert.ToInt64(gridSource[0].UserAccountTypeId);
                            FillForms(Convert.ToInt32(gridSource[0].UserAccountTypeId));
                            mcmbaccounttype.SelectedValue = Convert.ToInt64(gridSource[0].UserAccountTypeId);
                            txtuname.Text = gridSource[0].Username;
                            PasswordSalt = gridSource[0].PasswordSalt;
                            PasswordHash = gridSource[0].PasswordHash;
                            txtpassword.Text = "";
                            txtcconpassword.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.ToString());
            }
        }

        private void btnaddusertype_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                // Check if the control is not the pnlbgbtn panel
                if (control != pnlusertype)
                {
                    control.Enabled = false;
                    //pnlusertype.Enabled = true;
                }
            }
            pnlusertype.Location = new Point(this.Location.X + 150, this.Location.Y + 75);
            pnlusertype.Show();
            btnusertype.Enabled = true;
        }

       

        private void lblsubcatclose_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                control.Enabled = true;
            }
            pnlusertype.Hide();
        }
        private void btnusertype_Click_1(object sender, EventArgs e)
        {
            if (MandatoryCheckuser())
            {
                if (clsGeneral.NameExist_UserType(txtusertype.Text))
                {
                    if (txtusertype.Text != "")
                    {
                        foreach (Control control in this.Controls)
                        {
                            control.Enabled = true;
                        }
                        DmnUserAccountType dmnUser = new DmnUserAccountType();
                        BllUserAccount bllUser = new BllUserAccount();
                        dmnUser.UserAccountType = txtusertype.Text;
                        dmnUser.Note = txtdescr.Text;
                        dmnUser.CreatedBy = 1;
                        dmnUser.Status = 1;
                        bool usertype = bllUser.AddUserType("prdInsertUserType", dmnUser);
                        FillComboUserType();
                        pnlusertype.Hide();
                        txtusertype.Text = "";
                        txtdescr.Text = "";
                    }
                }
                else
                    txtusertype.Focus();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void FrmEmployeeShort_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }

        private void mcmbDesignationId_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void tableLayoutPanel11_Enter(object sender, EventArgs e)
        {

        }

        private void btnCapture_MouseDown(object sender, MouseEventArgs e)
        {
            btnCapture.BackgroundImage = FoodandBeverage.Properties.Resources.orderview_complete;
        }

        private void btnCapture_MouseUp(object sender, MouseEventArgs e)
        {
            btnCapture.BackgroundImage = FoodandBeverage.Properties.Resources.orderview_complete;
        }

        private void mpbAdd_MouseDown(object sender, MouseEventArgs e)
        {
            mpbAdd.BackgroundImage = FoodandBeverage.Properties.Resources.plus;
        }

        private void mpbAdd_MouseUp(object sender, MouseEventArgs e)
        {
            mpbAdd.BackgroundImage = FoodandBeverage.Properties.Resources.plus;
        }

        public void FillForms(int id)
        {
            dgvforms.Rows.Clear();
            headerCheckBoxwrite.Checked = false;
            headerCheckBoxread.Checked = false;
            int x = dgvforms.Width;
            headerCheckBoxwrite.Location = new Point(x - 400, 10);
            headerCheckBoxwrite.BackColor = Color.White;
            headerCheckBoxwrite.Size = new Size(18, 18);
            headerCheckBoxwrite.Click += new EventHandler(HeaderCheckBoxwrite_Clicked);
            dgvforms.Controls.Add(headerCheckBoxwrite);
            headerCheckBoxread.Location = new Point(x - 150, 10);
            headerCheckBoxread.BackColor = Color.White;
            headerCheckBoxread.Size = new Size(18, 18);
            headerCheckBoxread.Click += new EventHandler(HeaderCheckBoxread_Clicked);
            dgvforms.Controls.Add(headerCheckBoxread);
            BllSearch<DmnForms> bllacctype = new BllSearch<DmnForms>();
            DmnSearch dmnforms = new DmnSearch();
            dmnforms.Select = "f.Id,g.Caption,g.GTypeSubId,[Read],Write,f.UserType,f.Status,Note";
           
                dmnforms.Table = " tblGeneralOptions g" +
                " left join tblForms f on f.Form=g.GTypeSubId and f.Status=1 and f.UserType=" + id;
                dmnforms.WhereNdGroupBy = " where g.GTypeId=55 and g.Status=0";
            dmnforms.OrderBy = "order by GTypeSubId";
            var gridesource = bllacctype.GetSearchData("prdSearch", dmnforms);
            if (gridesource.Count > 0)
            {
                dgvforms.Rows.Add(gridesource.Count());
                txtUserTypeNote.Text = gridesource[0].Note;
                for (int i = 0; i < gridesource.Count; i++)
                {
                    dgvforms.Rows[i].Cells[iid].Value = gridesource[i].id;
                    dgvforms.Rows[i].Cells[iformid].Value = gridesource[i].GTypeSubId;
                    dgvforms.Rows[i].Cells[iforms].Value = gridesource[i].Caption;
                    dgvforms.Rows[i].Cells[iread].Value = gridesource[i].Read;
                    dgvforms.Rows[i].Cells[iwrite].Value = gridesource[i].Write;
                }
            }
        }
        private void txtICardNo1_DoubleClick(object sender, EventArgs e)
        {
            Frmkeypad keypad = new Frmkeypad(this, 0);
            keypad.StartPosition = FormStartPosition.CenterScreen;
            keypad.ShowDialog();
        }
    }
}
