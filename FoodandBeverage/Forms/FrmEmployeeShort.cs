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
        long BranchId = FrmDashboard.BranchId;
        long UserId = Forms.FrmLogin.UserId;
        public static int Allaccess = 0;
        public static int ReportLevelID = 0;
        long DepartmentId = 0;
        public static long EmployeeId = 0, AccountId = 0;
        string EmployeeCode = "";
        public static string empImgExt = "";
        long UserAccountId = 0;
        public string Compressedimg = Application.StartupPath + "\\CompressedImg";
        Bitmap bitmap;
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
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
            try { }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
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
                _objSearch.WhereNdGroupBy = "Where emp.Status=1 and emp.BranchId = " + BranchId;

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
                            dgvforms.Visible = true;
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  button5_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
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
                txtEmpAddress.Focus();
            }
        }
        private void txtICardNo1_Enter(object sender, EventArgs e)
        {
            tlp_main.Enabled = false;
            pnlwifi.Location = new Point(this.ClientSize.Width / 2 - pnlwifi.Size.Width / 2, this.ClientSize.Height / 2 - pnlwifi.Size.Height / 2);
            pnlwifi.Visible = true;
            tm_card.Start();
        }
        public void FillCombo1()
        {
            try
            {
                //Department
                BllSearch<DmnComboBox> bllSearchDep = new BllSearch<DmnComboBox>();
                DmnSearch dmnSearchDep = new DmnSearch();
                dmnSearchDep.Select = "DepartmentId,DepartmentName";
                dmnSearchDep.Table = "tblDepartment";
                dmnSearchDep.WhereNdGroupBy = "Where Status =1 and BranchId=" + BranchId;
                var departmentSource = bllSearchDep.GetSearchData("prdComboFill", dmnSearchDep);
                mcmbDepartmentId.DisplayMember = "Value";
                mcmbDepartmentId.ValueMember = "Id";
                mcmbDepartmentId.DataSource = departmentSource;

                //Designation
                BllSearch<DmnComboBox> bllSearchDes = new BllSearch<DmnComboBox>();
                DmnSearch dmnSearchDes = new DmnSearch();
                dmnSearchDes.Select = "DesignationId,DesignationName";
                dmnSearchDes.Table = " tblDesignation ";
                dmnSearchDes.WhereNdGroupBy = "Where Status =1 and BranchId=" + BranchId;
                mcmbDesignationId.DataSource = null;
                var designationSource = bllSearchDes.GetSearchData("prdComboFill", dmnSearchDes);
                mcmbDesignationId.DataSource = designationSource;
                mcmbDesignationId.DisplayMember = "Value";
                mcmbDesignationId.ValueMember = "Id";

                //Gender

                //Account type         
                BllSearch<DmnComboBox> blllocation = new BllSearch<DmnComboBox>();
                DmnSearch dmnlocation = new DmnSearch();
                dmnlocation.Select = "LocId,Name";
                dmnlocation.Table = " tblLocationDetails ";
                dmnlocation.WhereNdGroupBy = "Where Status = 1 ";
                dmnlocation.OrderBy = "order by Value";

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  FillCombo1()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }

        }
        public void FillCombo()
        {
            try
            {
                //Department
                BllSearch<DmnComboBox> bllSearchDep = new BllSearch<DmnComboBox>();
                DmnSearch dmnSearchDep = new DmnSearch();
                dmnSearchDep.Select = "DepartmentId,DepartmentName";
                dmnSearchDep.Table = "tblDepartment";
                dmnSearchDep.WhereNdGroupBy = "Where Status =1 and BranchId=" + BranchId;
                var departmentSource = bllSearchDep.GetSearchData("prdComboFill", dmnSearchDep);
                mcmbDepartmentId.DisplayMember = "Value";
                mcmbDepartmentId.ValueMember = "Id";
                mcmbDepartmentId.DataSource = departmentSource;

                //Designation
                BllSearch<DmnComboBox> bllSearchDes = new BllSearch<DmnComboBox>();
                DmnSearch dmnSearchDes = new DmnSearch();
                dmnSearchDes.Select = "DesignationId,DesignationName";
                dmnSearchDes.Table = " tblDesignation ";
                dmnSearchDes.WhereNdGroupBy = "Where Status =1 and BranchId=" + BranchId;
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

                mcmbaccounttype.DataSource = acctype;
                mcmbaccounttype.DisplayMember = "Value";
                mcmbaccounttype.ValueMember = "Id";
                
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  FillCombo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }



        }
        public bool CheckCardAssign(string cardno)
        {
            try
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  CheckCardAssign()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
                return false;
            }
        }
        public void Keypad_Rfid(string str1)
        {
            try
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
                    BranchId = FrmDashboard.BranchId;
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
                            pbCust.Image = null;
                    }
                    if (gridSource[0].UserAccountTypeId != null)
                    {
                        dgvforms.Visible = true;
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  Keypad_Rfid()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  RemoveSpecialCharacters()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
                return "";
            }

        }
        private void btnrfid_Click(object sender, EventArgs e)
        {
            Frmkeypad keypad = new Frmkeypad(this, 0);
            keypad.StartPosition = FormStartPosition.CenterScreen;
            keypad.ShowDialog();
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
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsGeneral.LogException("Employee Page  mcmbDesignationId_SelectedIndexChanged_1()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }

        }
        public void GenerateCode()
        {
            try
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  GenerateCode()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
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
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsGeneral.LogException("Employee Page  mcmbDepartmentId_SelectedIndexChanged_1()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
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
                dtpBirthDate.Focus();
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
            FillCombo1();
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
                pbCust.Image = FoodandBeverage.Properties.Resources.employe_female_icon;
            else if (Convert.ToInt32(cboGender.SelectedIndex) == 2)
                pbCust.Image = FoodandBeverage.Properties.Resources.employe_male_icon;
            else if (Convert.ToInt32(cboGender.SelectedIndex) == 3)
                pbCust.Image = FoodandBeverage.Properties.Resources.employee_common_pic;
            pbCust.SizeMode = PictureBoxSizeMode.StretchImage;


        }

        private void dtpBirthDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtemail.Focus();
            }
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtICardNo1.Focus();
            }
        }

        private void txtEmpAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtnote.Focus();
            }
        }

        private void txtnote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private void chk_Rfid_Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Tab || e.KeyData == Keys.Enter)
            {
                cmbcam.Focus();
            }
        }
        private void mpbRemove_Click(object sender, EventArgs e)
        {
            pbCust.BackgroundImage = null;
            pbCust.Image = null;
            pbCust.Image = FoodandBeverage.Properties.Resources.employee_common_pic;
            pbCust.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void pbCust_Click(object sender, EventArgs e)
        {
            try
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
                        pbCust.Image = FoodandBeverage.Properties.Resources.employee_common_pic;
                    else if (Convert.ToInt32(cboGender.SelectedValue) == 2)
                        pbCust.Image = FoodandBeverage.Properties.Resources.employe_female_icon;
                    else
                        pbCust.Image = FoodandBeverage.Properties.Resources.employe_male_icon;
                    pbCust.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  pbCust_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  btnCapture_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }


        }
        static bool ValidatePhoneNumber(string phoneNumber)
        {
            try
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
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool IsEmployeeExist()
        {
            try
            {
                DmnEmployee dmnEmployee = new DmnEmployee();
                BllSearch<DmnEmployee> bllSearch = new BllSearch<DmnEmployee>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = " EmployeeName";
                dmnSearch.Table = " tblEmployee";
                if (EmployeeId == 0)
                    dmnSearch.WhereNdGroupBy = "Where EmployeeName='" + txtEmpName.Text + "' and Status=0 and BranchId=" + BranchId;
                else
                    dmnSearch.WhereNdGroupBy = "Where EmployeeName='" + txtEmpName.Text + "' and Status=0 and EmployeeId!='" + EmployeeId + "' and BranchId=" + BranchId;

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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  IsEmployeeExist()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
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
            try
            {
                if (string.IsNullOrEmpty(txtEmpName.Text))
                {
                    tb_empolyee.SelectedTab = tp_employee;
                    CMessageBox.Show("Employee name is mandatory This field is mandatory", "Mandatory Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmpName.Focus();
                    return false;
                }
                else if (string.IsNullOrEmpty(txtphone.Text))
                {
                    tb_empolyee.SelectedTab = tp_employee;
                    CMessageBox.Show("Phone number is mandatory This field is mandatory", "Mandatory Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtphone.Focus();
                    return false;
                }
                else if (cboGender.SelectedIndex == 0 || cboGender.SelectedIndex == -1)
                {
                    tb_empolyee.SelectedTab = tp_employee;
                    CMessageBox.Show("Please specify your gender This field is mandatory", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboGender.Focus();
                    return false;
                }
                else if (mcmbDesignationId.SelectedIndex == 0 || mcmbDesignationId.SelectedIndex == -1)
                {
                    tb_empolyee.SelectedTab = tp_employee;
                    CMessageBox.Show("Please specify your designation. This field is mandatory", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mcmbDesignationId.Focus();
                    return false;
                }
                else if (mcmbDepartmentId.SelectedIndex == 0 || mcmbDepartmentId.SelectedIndex == -1)
                {
                    tb_empolyee.SelectedTab = tp_employee;
                    CMessageBox.Show("Please specify your department. This field is mandatory", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mcmbDepartmentId.Focus();
                    return false;
                }
                else if (dtpBirthDate.Value.Date == DateTime.Today.Date)
                {
                    tb_empolyee.SelectedTab = tp_employee;
                    CMessageBox.Show("Date of birth cannot be the current date. Please enter a valid date of birth", "Invalid Date of Birth", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpBirthDate.Focus();
                    return false;
                }
                else if (string.IsNullOrEmpty(txtemail.Text))
                {
                    tb_empolyee.SelectedTab = tp_employee;
                    CMessageBox.Show("Email is mandatory. This field is mandatory.", "Mandatory Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtemail.Focus();
                    return false;
                }
                else if (string.IsNullOrEmpty(txtCardnumber.Text))
                {
                    tb_empolyee.SelectedTab = tp_employee;
                    CMessageBox.Show("Card number is mandatory. This field is mandatory", "Mandatory Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCardnumber.Focus();
                    return false;
                }
                else if (string.IsNullOrEmpty(txtEmpAddress.Text))
                {
                    tb_empolyee.SelectedTab = tp_employee;
                    CMessageBox.Show("Address is mandatory. This field is mandatory", "Mandatory Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpAddress.Focus();
                    return false;
                }
                else if (!IsEmployeeExist())
                {
                    tb_empolyee.SelectedTab = tp_employee;
                    CMessageBox.Show("Employee name already exists. Please use a different name.", "Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmpName.Focus();
                    return false;
                }

                else if (cmbUsreAccount.SelectedIndex == 0 || cmbUsreAccount.SelectedIndex == -1)
                {
                    CMessageBox.Show("Please specify the access type. This field is mandatory.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tb_empolyee.SelectedTab = tp_rolereg;
                    cmbUsreAccount.Focus();
                    return false;
                }
                else if (mcmbaccounttype.SelectedIndex == 0 || mcmbaccounttype.SelectedIndex == -1)
                {
                    CMessageBox.Show("User Type Need To Be specify", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tb_empolyee.SelectedTab = tp_userreg;
                    mcmbaccounttype.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  MandatoryCheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
                return true;
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  fillcmbcame()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
        }
        public void clear()
        {
            try
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
                txtutypeid.Text = "0";
                pbCust.BackgroundImage = null;
                pbCust.Image = FoodandBeverage.Properties.Resources.employee_common_pic;
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
                tb_empolyee.SelectedTab = tp_employee;
                tlp_employe_reg.Focus();
                txtEmpName.Focus();
                dtpBirthDate.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  clear()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
        }
        public void LocationFill()
        {
            try
            {
                //location
                BllSearch<DmnComboBox> blllocation = new BllSearch<DmnComboBox>();
                DmnSearch dmnlocation = new DmnSearch();
                dmnlocation.Select = "LocId,Name";
                dmnlocation.Table = " tblLocationDetails ";
                dmnlocation.WhereNdGroupBy = "Where Status = 1 ";
                dmnlocation.OrderBy = "order by Value";
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  LocationFill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
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
                    dmnEmployee.BranchId = FrmLogin.BranchId;
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
                            BllUserAccount bllUserAccount = new BllUserAccount();
                            DmnUserAccount dmnUserAccount = new DmnUserAccount();
                            BllEncryption bllEncryption = new BllEncryption();
                            dmnUserAccount.UserAccountId = UserAccountId;
                            dmnUserAccount.UserAccountTypeId = Convert.ToInt32(mcmbaccounttype.SelectedValue);
                            dmnUserAccount.Username = txtuname.Text;
                            dmnUserAccount.BranchId = FrmLogin.BranchId;
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
                                    // CMessageBox.Show("User Account Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //clear();
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
                                    // CMessageBox.Show("User Account Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //clear();
                                }
                                else
                                {
                                    CMessageBox.Show("Unable To Save ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        
                    }

                    #endregion

                    EmployeeId = 0; UserAccountId = 0;
                    CMessageBox.Show("Successfully Saved " + txtEmpName.Text, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clear();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  btnSave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  mbtnList_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  txtuname_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
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
            try
            {
                if (string.IsNullOrEmpty(txtutypeid.Text))
                {
                    txtutypeid.Text = "0";
                }
                if (txtusertype.Text == "" || txtusertype.Text == null)
                {
                    CMessageBox.Show("Please Enter UserType", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  fillcmbcame()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
                return false;
            }

        }
        public void FillComboUserType()
        {
            try
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  FillComboUserType()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
        }
        private void HeaderCheckBoxwrite_Clicked(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  HeaderCheckBoxwrite_Clicked()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
        }
        private void HeaderCheckBoxread_Clicked(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  HeaderCheckBoxread_Clicked()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
        }

        private void FrmEmployeeShort_Load(object sender, EventArgs e)
        {
            try
            {
                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    AppSession.RegisterForm(this, (int)LogoutFormList.employee);
                }
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

                LocationFill();

                if (Convert.ToInt32(FrmLogin.BranchId) == 0)
                {
                    BllSearch<DmnHRegistration> bllSearch = new BllSearch<DmnHRegistration>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = "tblHotelRegistration";
                    objSearch.WhereNdGroupBy = "WHERE Status=1";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource.Count > 0)
                        BranchId = Convert.ToInt32(gridSource[0].Id);
                    else
                        BranchId = FrmDashboard.BranchId;
                }
                FrmDashboard frmDash = new FrmDashboard();


                if (frmDash.ChkWritePermission(Convert.ToInt32(formsenum.User_Permission)) && frmDash.ChkWritePermission(Convert.ToInt32(formsenum.Employee)))
                {
                    btnSave.Enabled = true;
                    btnusertype.Enabled = true;
                    BranchId = FrmDashboard.BranchId;
                }
                else
                {
                    btnSave.Enabled = false;
                    btnusertype.Enabled = false;
                    BranchId = FrmDashboard.BranchId;
                }

                if (FrmLogin.UserId == 1 && Convert.ToString(FrmLogin.EmployeName) == "test")
                {
                    btnusertype.Enabled = true;
                    btnSave.Enabled = true;
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
                    objSearch.WhereNdGroupBy = "Where e.EmployeeId = " + empid + "e.Branchid = " + BranchId;
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  FrmEmployeeShort_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }

        }
        public void FillcardDetails()
        {
            try
            {
                string str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul(FrmLogin.Compid);
                if (str1 != "Card not in contact")
                {
                    tm_card.Stop();
                    if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")
                        str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi(FrmLogin.Compid);
                    str1 = RemoveSpecialCharacters(str1.Trim());
                    if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")
                    {
                        CMessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tm_card.Start();
                    }
                    else
                    {
                        tlp_main.Enabled = true;
                        pnlwifi.Visible = false;
                        Keypad_Rfid(str1);
                    }
                }
                //mcmbDesignationId.Focus();                
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  FillcardDetails()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FillcardDetails();
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
                _objSearch.WhereNdGroupBy = "Where emp.Status=1 and emp.BranchId = " + BranchId;

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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  button9_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
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
                _objSearch.WhereNdGroupBy = "Where emp.Status=1 and emp.BranchId = " + BranchId;
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
                clsGeneral.LogException("Employee Page  button5_Click_1()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
        }

        private void btnaddusertype_Click(object sender, EventArgs e)
        {
            tlp_main.Enabled = false;
            pnlusertype.Location = new Point(this.Location.X + 150, this.Location.Y + 75);
            pnlusertype.Show();
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
            try
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
                            dmnUser.CreatedBy = Convert.ToInt32(FrmLogin.UserId);
                            dmnUser.Status = 1;
                            var usertype = bllUser.AddUserType("prdInsertUserType", dmnUser);
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  btnusertype_Click_1()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
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
            #region setting Corner Round 

            UIHelper.RoundCorners(tlp_user_reg, 25, null);
            UIHelper.RoundCorners(tlp_role_design, 10, null);
            UIHelper.RoundCorners(tp_employee, 25, null);
            UIHelper.RoundCorners(tlp_employe_header, 10, null);
            UIHelper.RoundCorners(dgvforms, 25, null);

            #endregion

            textb_shape();
            if (tb_empolyee.SelectedIndex == 1)
            {
                clsGeneral.GrideAutoSize(dgvforms);

            }
        }

        private void mcmbDesignationId_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mcmbDepartmentId.Focus();
            }
        }

        private void cboGender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mcmbDesignationId.Focus();
            }
        }

        private void btn_file_Click(object sender, EventArgs e)
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
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsGeneral.LogException("Employee Page  btn_file_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
        }

        private void lblsubcatclose_Click_1(object sender, EventArgs e)
        {
            tlp_main.Enabled = true;
            pnlusertype.Visible = false;
        }

        private void btn_usertype_close_Click(object sender, EventArgs e)
        {
            tlp_main.Enabled = true;
            pnlusertype.Visible = false;
        }

        private void btnusertype_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheckuser())
                {
                    if (Convert.ToInt32(txtutypeid.Text) > 0)
                    {
                        if (txtusertype.Text != "")
                        {
                            DmnUserAccountType dmnUser = new DmnUserAccountType();
                            BllUserAccount bllUser = new BllUserAccount();
                            dmnUser.UserAccountTypeId = Convert.ToInt32(txtutypeid.Text);
                            dmnUser.UserAccountType = txtusertype.Text;
                            dmnUser.Note = txtdescr.Text;
                            dmnUser.Status = 1;
                            var usertype = bllUser.AddUserType("prdInsertUserType", dmnUser);
                            if (usertype.UserAccountTypeId > 0)
                            {
                                if (Convert.ToInt32(txtutypeid.Text) > 0)
                                    CMessageBox.Show("Update Successful", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    CMessageBox.Show("Save Successful", "Save Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                FillCombo();
                                pnlusertype.Hide();
                                txtusertype.Text = "";
                                txtdescr.Text = "";
                                txtutypeid.Text = "";
                                tlp_main.Enabled = true;
                            }
                        }
                    }
                    else
                    {

                        if (clsGeneral.NameExist_UserType(txtusertype.Text))
                        {
                            if (txtusertype.Text != "")
                            {
                                DmnUserAccountType dmnUser = new DmnUserAccountType();
                                BllUserAccount bllUser = new BllUserAccount();
                                dmnUser.UserAccountTypeId = Convert.ToInt32(txtutypeid.Text);
                                dmnUser.UserAccountType = txtusertype.Text;
                                dmnUser.Note = txtdescr.Text;
                                dmnUser.Status = 1;
                                var usertype = bllUser.AddUserType("prdInsertUserType", dmnUser);
                                if (usertype.UserAccountTypeId > 0)
                                {
                                    if (Convert.ToInt32(txtutypeid.Text) > 0)
                                        CMessageBox.Show("Update Successful", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                        CMessageBox.Show("Save Successful", "Save Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    FillCombo();
                                    pnlusertype.Hide();
                                    txtusertype.Text = "";
                                    txtdescr.Text = "";
                                    txtutypeid.Text = "";
                                    tlp_main.Enabled = true;
                                }
                            }
                        }
                        else
                            txtusertype.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  btnusertype_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
            }

        }

        private void txtusertype_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }
        private void txtusertype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                Usertypepopup();
            }
        }
        public void Usertypepopup()
        {
            try
            {
                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,User Type,Description";
                _objSearch.Select = "UserAccountTypeId as Id,UserAccountType as Field1,Note as Field2";
                _objSearch.Table = "tblUserAccountType ";
                _objSearch.WhereNdGroupBy = "Where Status=1";
                _objSearch.OrderBy = " Order By UserAccountType ASC ";

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
                    int utid = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "UserAccountTypeId as Id,UserAccountType as Field1,Note as Field2";
                    objSearch.Table = "tblUserAccountType ";
                    objSearch.WhereNdGroupBy = "Where UserAccountTypeId = " + utid;
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource.Count > 0)
                    {
                        txtusertype.Text = gridSource[0].Field1;
                        txtdescr.Text = Convert.ToString(gridSource[0].Field2);
                        txtutypeid.Text = Convert.ToString(gridSource[0].Id);

                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  Usertypepopup()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
            }
        }

        private void txtusertype_Enter(object sender, EventArgs e)
        {
            lblusertypeinfo.Visible = true;
        }

        private void txtusertype_Leave(object sender, EventArgs e)
        {
            lblusertypeinfo.Visible = false;
        }

        private void btn_depart_reg_Click(object sender, EventArgs e)
        {
            FrmDepartment frmDepartment = new FrmDepartment();
            frmDepartment.StartPosition = FormStartPosition.CenterParent;
            frmDepartment.ShowDialog();
            FillCombo1();
        }

        private void txtemail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page txtemail_Validating()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
        }

        private void txtphone_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtphone.Text))
                {
                    bool isValidPhoneNumber = ValidatePhoneNumber(txtphone.Text);
                    if (!isValidPhoneNumber)
                    {
                        //CMessageBox.Show("The phone number entered is not valid. Please enter a valid phone number.", "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtphone.Focus();
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page txtphone_Validating()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
        }
        private void txtICardNo1_DoubleClick(object sender, EventArgs e)
        {
            //Frmkeypad keypad = new Frmkeypad(this, 0);
            //keypad.StartPosition = FormStartPosition.CenterScreen;
            //keypad.ShowDialog();
        }

        private void btnwificancel_Click(object sender, EventArgs e)
        {
            tm_card.Stop();
            pnlwifi.Visible = false;
            tlp_main.Enabled = true;
        }

        private void dgvforms_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {

                int firstDisplayedRowIndex = dgvforms.FirstDisplayedScrollingRowIndex;
                int firstDisplayedColumnIndex = dgvforms.FirstDisplayedScrollingColumnIndex;


                int displayedRowCount = dgvforms.DisplayedRowCount(true);
                int totalRowCount = dgvforms.RowCount;

                int lastDisplayedRowIndex = firstDisplayedRowIndex + displayedRowCount - 1;
                bool isAtRightEnd = lastDisplayedRowIndex >= totalRowCount - 1;
                bool isAtLeftEnd = firstDisplayedColumnIndex == 0;
                if (isAtRightEnd)
                {
                    Rectangle headerCellRect = dgvforms.GetColumnDisplayRectangle(3, true);
                    int checkBoxX = headerCellRect.X + (headerCellRect.Width - headerCheckBoxwrite.Width) / 2;
                    Point headerCellLocation = this.dgvforms.GetCellDisplayRectangle(3, -1, true).Location;
                    headerCheckBoxwrite.Location = new Point(checkBoxX, headerCellLocation.Y + 27);

                     headerCellRect = dgvforms.GetColumnDisplayRectangle(4, true);
                     checkBoxX = headerCellRect.X + (headerCellRect.Width - headerCheckBoxwrite.Width) / 2;
                     headerCellLocation = this.dgvforms.GetCellDisplayRectangle(4, -1, true).Location;
                    headerCheckBoxread.Location = new Point(checkBoxX, headerCellLocation.Y + 27);
                }
                else
                {
                    headerCheckBoxwrite.Visible = false;
                    headerCheckBoxread.Visible = false;

                }
                if (isAtLeftEnd)
                {
                    headerCheckBoxwrite.Visible = false;
                    headerCheckBoxread.Visible = false;
                }
            }
        }

        private void dtpBirthDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                string dob = dtpBirthDate.Value.ToString("yyyy");
                string dt = DateTime.Now.ToString("yyyy");
                int age = Convert.ToInt32(dt) - Convert.ToInt32(dob);
                if (age <= 18)
                {
                    dtpBirthDate.Focus();
                    CMessageBox.Show("Age must be above 18!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page dtpBirthDate_ValueChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
        }

        private void tb_empolyee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tb_empolyee.SelectedIndex == 1)
            {
                checkboxreset();
            }

        }

        private void FrmEmployeeShort_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                AppSession.UnregisterForm(this);
            }
        }

        public void FillForms(int id)
        {
            try
            {
                dgvforms.Rows.Clear();
                headerCheckBoxwrite.Checked = false;
                headerCheckBoxread.Checked = false;               

                /*int x = dgvforms.Width;
                headerCheckBoxwrite.Location = new Point(x - 400, 10);
                headerCheckBoxwrite.BackColor = Color.FromArgb(20, 25, 72);
                headerCheckBoxwrite.Size = new Size(18, 18);
                headerCheckBoxwrite.Click += new EventHandler(HeaderCheckBoxwrite_Clicked);
                dgvforms.Controls.Add(headerCheckBoxwrite);

                headerCheckBoxread.Location = new Point(x - 150, 10);
                headerCheckBoxread.BackColor = Color.FromArgb(20, 25, 72);
                headerCheckBoxread.Size = new Size(18, 18);
                headerCheckBoxread.Click += new EventHandler(HeaderCheckBoxread_Clicked);
                dgvforms.Controls.Add(headerCheckBoxread);*/

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

                    checkboxreset();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page  FillForms()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
        }
        public void checkboxreset()
        {
            try
            {
                #region Remove existing Check Box Controls
                if (dgvforms.Controls.Contains(headerCheckBoxwrite))
                {
                    dgvforms.Controls.Remove(headerCheckBoxwrite);
                }
                if (dgvforms.Controls.Contains(headerCheckBoxread))
                {
                    dgvforms.Controls.Remove(headerCheckBoxread);
                }
                #endregion

                #region Check Box Select all
                Rectangle headerCellRect = dgvforms.GetColumnDisplayRectangle(3, true);
                int checkBoxX = headerCellRect.X + (headerCellRect.Width - headerCheckBoxwrite.Width) / 2;
                Point headerCellLocation = this.dgvforms.GetCellDisplayRectangle(3, -1, true).Location;
                headerCheckBoxwrite.Location = new Point(checkBoxX, headerCellLocation.Y + 27);
                headerCheckBoxwrite.BackColor = Color.FromArgb(20, 25, 72);
                headerCheckBoxwrite.Size = new Size(18, 18);
                headerCheckBoxwrite.Click += new EventHandler(HeaderCheckBoxwrite_Clicked);
                dgvforms.Controls.Add(headerCheckBoxwrite);

                headerCellRect = dgvforms.GetColumnDisplayRectangle(4, true);
                checkBoxX = headerCellRect.X + (headerCellRect.Width - headerCheckBoxread.Width) / 2;
                headerCellLocation = this.dgvforms.GetCellDisplayRectangle(4, -1, true).Location;
                headerCheckBoxread.Location = new Point(checkBoxX, headerCellLocation.Y + 27);
                headerCheckBoxread.BackColor = Color.FromArgb(20, 25, 72);
                headerCheckBoxread.Size = new Size(18, 18);
                headerCheckBoxread.Click += new EventHandler(HeaderCheckBoxread_Clicked);
                dgvforms.Controls.Add(headerCheckBoxread);
                #endregion
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("Employee Page checkboxreset()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Employee));
            }
        }

    }
}
