using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using ReaderSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AYJCardLib;
using System.Data.SqlClient;
using System.Threading;
using MetroFramework;
using System.Diagnostics;


namespace pLocker.Forms
{
    public partial class frmLogin : Form
    {
        #region Declarations



        ClsGeneral objGeneral = new ClsGeneral();// for validation
        BllEncryption bllEncryption = new BllEncryption();
        BllUserAccount bllUserAccount = new BllUserAccount();
        byte[] passwordHash;
        byte[] passwordSalt;
        public static long UserId;
        public static string EmployeeCode;
        public static string EmployeeName;
        public static long BranchId;
        int UserStatus;
        long EmployeeId;
        public byte[] encryptPassword;
        long CompanyId;
        public static long UserAccountTypeId;
        public static int rfid = 0;


        string photo;
        bool imageExist;
        ClsDocument clsDocument = new ClsDocument();
        #endregion

        public frmLogin()
        {
            InitializeComponent();
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
        private void btnRFID_Click(object sender, EventArgs e)
        {
            string str1 = "";
            txtRFID.Clear();
            str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul();
            if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")

                str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi();
            str1 = RemoveSpecialCharacters(str1.Trim());

            if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")
                MetroMessageBox.Show(this,"Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtRFID.Text = str1;
            //txtRFID.Text = AYJCardLib.AYJReadCardLib.CardChk();
            //txtRFID.Text = "AYJ1AYJ1E0010000001000001013";

            BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "tblEmployee.ICardNo, tblUserAccount.UserAccountId, tblUserAccount.Username, tblUserAccount.PasswordHash, tblUserAccount.PasswordSalt";
            objSearch.Table = " tblEmployee INNER JOIN tblUserAccount ON tblEmployee.EmployeeId = tblUserAccount.EmployeeId";
            objSearch.WhereNdGroupBy = "where tblEmployee.ICardNo ='" + txtRFID.Text + "' And tblEmployee.Status=0";
            objSearch.OrderBy = "Order By UserAccountId ";
            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
            //mcmbUserAccountType.SelectedValue = gridSource[0].UserAccountTypeId;
            if (gridSource.Count > 0)
            {
                txtUserName.Text = Convert.ToString(gridSource[0].Username);
                txtPwd.Text = Convert.ToString(gridSource[0].PasswordHash);
                passwordSalt = gridSource[0].PasswordSalt;
                passwordHash = gridSource[0].PasswordHash;
            }

            txtUserName_Leave(null, null);
            //txtPwd_Leave(null, null);
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {

                txtUserName.Clear();
                txtPwd.Clear();
                ActiveControl = txtUserName;
                txtUserName.Focus();

                BllSearch<DmnComboBox> bllSearchBranch = new BllSearch<DmnComboBox>();
                DmnSearch dmnSearchBranch = new DmnSearch();
                dmnSearchBranch.Select = "LocId,Name";
                dmnSearchBranch.Table = " tblLocationDetails ";
                dmnSearchBranch.WhereNdGroupBy = " Where Status=1 ";
                var branch = bllSearchBranch.GetSearchData("prdComboFill", dmnSearchBranch);
                cboBranch.DataSource = branch;
                cboBranch.DisplayMember = "Value";
                cboBranch.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this,ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                if (!string.IsNullOrWhiteSpace(txtUserName.Text))
                  {
                        FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                        DmnSearch objSearch = new DmnSearch();
                        objSearch.ColumnHeaders = "Id,Photo";
                        objSearch.Select = " ua.UserAccountId as Id,emp.Photo as Field1";
                        objSearch.Table = " tblUserAccount ua Left join tblEmployee emp on emp.EmployeeId = ua.EmployeeId ";
                        objSearch.WhereNdGroupBy = " Where Username = '" + txtUserName.Text.Trim().ToUpper() + "'";
                        var userPhoto = frmGridSearch.GetSearchGrid(objSearch);
                        int count = userPhoto.Rows.Count;
                        if (count > 0 && Convert.ToString(userPhoto["Field1", 0].Value) != string.Empty)
                        {
                            photo = Convert.ToString(userPhoto["Field1", 0].Value);
                            imageExist = clsDocument.FileExistOrNot(photo, DocumentType.EmployeePhoto);
                            if (imageExist)
                                pbUser.Image = clsDocument.DownloadImage(photo, DocumentType.EmployeePhoto);
                        }

                        objSearch.ColumnHeaders = "UserAccountId,Employeename,Branchname,UserAccountTypeId";
                        objSearch.Select = " ua.UserAccountId as Id,emp.Employeename as Field1,br.Name as Field2,ua.UserAccountTypeId as Field3";
                        objSearch.Table = " tblUserAccount ua Left join  tblEmployee emp on emp.EmployeeId = ua.EmployeeId " +
                                            "Left join tblLocationDetails br on br.LocId = ua.BranchId ";
                        objSearch.WhereNdGroupBy = " Where Username = '" + txtUserName.Text.Trim().ToUpper() + "'";
                        var userdetails = frmGridSearch.GetSearchGrid(objSearch);

                        //if (userdetails.Rows.Count>0 && Convert.ToInt32(userdetails["Id",0].Value)>2)
                        

                        if (userdetails.Rows.Count > 0)
                        {
                        UserAccountTypeId = Convert.ToInt64(userdetails["Field3", 0].Value);

                        lblUser.Text = txtUserName.Text.Trim().ToUpper();
                            if (Convert.ToInt32(userdetails["Id", 0].Value) == 1)
                            {
                                cboBranch.Text = Convert.ToString(userdetails["Field2", 0].Value);
                                cboBranch.Enabled = true;

                            }
                            else
                            {
                                cboBranch.Text = Convert.ToString(userdetails["Field2", 0].Value);
                                cboBranch.Enabled = false;

                            }
                        }
                        else if (userdetails.Rows.Count <= 0)
                        {
                        MetroMessageBox.Show(this, "", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        txtUserName.Text = "";
                        txtUserName.Focus();
                        }

                    

                  } 
                              
                BranchId = Convert.ToInt64(cboBranch.SelectedValue);
                if (string.IsNullOrWhiteSpace(txtUserName.Text))
                {
                    pbUser.Image = null;
                    pbUser.Image = Properties.Resources.userProfile;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUserName.Text))
                {
                    MetroMessageBox.Show(this, "", "User is Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPwd.Text = "";
                    txtUserName.Focus();
                }

                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this,ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPwd_Leave(object sender, EventArgs e)
        {
            try
            {
                if (IsUsernameValid())
                {
                    if (!IsPasswordvalid() && !string.IsNullOrWhiteSpace(txtPwd.Text.Trim()) && !string.IsNullOrWhiteSpace(txtUserName.Text.Trim()))
                    {
                        MetroMessageBox.Show(this, "", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPwd.Text = "";
                        txtPwd.Focus();
                    }
                    else
                    {
                        if (UserAccountTypeId == 1)
                        {
                            cboBranch.Enabled = true;
                            cboBranch.Focus();
                        }

                    }

                }
                
                

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool IsUsernameValid()
        {
            BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "PasswordHash,PasswordSalt ";
            objSearch.Table = " tblUserAccount ";
            objSearch.WhereNdGroupBy = " Where Username = '" + txtUserName.Text.Trim().ToUpper() + "'   AND Status=1 ";
            var pwdDetails = bllSearch.GetSearchData("prdSearch", objSearch);
            if (pwdDetails.Count > 0)
            {
                lblUser.Text = txtUserName.Text.Trim().ToUpper();
                passwordSalt = pwdDetails[0].PasswordSalt;
                passwordHash = pwdDetails[0].PasswordHash;
                return true;
            }
            else
            {
                // MetroMessageBox.Show(this, "\nInvalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                txtUserName.Focus();
                return false;
            }
        }
        public bool IsPasswordvalid()
        {
            bool result = true;
            if (passwordSalt == null || passwordHash == null)
            {
                return false;
            }
            if (rfid == 0)
                result = bllEncryption.verifyPassword(txtPwd.Text.Trim(), passwordSalt, passwordHash);
            else
            {
                for (int i = 0; i < encryptPassword.Length; i++)
                {
                    if (encryptPassword[i] != passwordHash[i])
                        result = false;
                }
                result = true;
            }
            if (result == true)
            {
                return true;
            }
            return false;
        }
        public bool IsValid()
        {
            Control[] controllist = { txtUserName, txtPwd };
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            try
            
            {
                if (IsValid())
                {
                    if (cboBranch.SelectedIndex != 0)
                    {
                        if (IsUsernameValid() && !string.IsNullOrWhiteSpace(txtPwd.Text.Trim()) && !string.IsNullOrWhiteSpace(txtUserName.Text.Trim()))
                        {
                            if (IsPasswordvalid())
                            {
                                #region retrieve data after login

                                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                                DmnSearch objSearch = new DmnSearch();
                                objSearch.ColumnHeaders = "UserAccountId,Status,EmployeeId,BranchId,CompanyId,UserAccountTypeId,EmployeeCode,EmployeeName";
                                objSearch.Select = " ua.UserAccountId as Id,ua.[Status] as Field1,emp.EmployeeId as Field2,ua.BranchId as Field3,br.CorpId as Field4 ,ua.UserAccountTypeId as Field5,emp.EmployeeCode as Field6,emp.EmployeeName as Field7";
                                objSearch.Table = " tblUserAccount ua Left join  tblEmployee emp on emp.EmployeeId = ua.EmployeeId " +
                                    "Left join tblLocationDetails br on br.LocId = ua.BranchId ";
                                objSearch.WhereNdGroupBy = " Where Username = '" + txtUserName.Text.Trim().ToUpper() + "'";
                                var userDetails = frmGridSearch.GetSearchGrid(objSearch);

                                BranchId = Convert.ToInt64(cboBranch.SelectedValue);
                                UserAccountTypeId = Convert.ToInt64(userDetails["Field5", 0].Value);
                                if (UserAccountTypeId >= 2 && Convert.ToString(BranchId) != Convert.ToString(userDetails["Field3", 0].Value))
                                {
                                    MetroMessageBox.Show(this, "", "\nInvalid credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtUserName.Focus();
                                }
                                else
                                {
                                    UserId = Convert.ToInt64(userDetails["Id", 0].Value);
                                    UserStatus = Convert.ToInt16(userDetails["Field1", 0].Value);
                                    EmployeeId = Convert.ToInt64(userDetails["Field2", 0].Value);                               
                                    CompanyId = Convert.ToInt64(userDetails["Field4", 0].Value);
                                    UserAccountTypeId = Convert.ToInt64(userDetails["Field5", 0].Value);
                                    EmployeeCode = Convert.ToString(userDetails["Field6", 0].Value);
                                    EmployeeName = Convert.ToString(userDetails["Field7", 0].Value);

                                    objSearch.ColumnHeaders = "FinancialYearId";
                                    objSearch.Select = " geno.GTypeSubText1 as Field1 ";
                                    objSearch.Table = " tblGeneralOptions geno Left join tblGType gt on geno.GTypeId=gt.GTypeId ";
                                    objSearch.WhereNdGroupBy = "where GType='FINANCIAL YEAR'  COLLATE SQL_Latin1_General_CP1_CS_AS ";
                                    var finYr = frmGridSearch.GetSearchGrid(objSearch);
                                    #endregion
                                    Forms.FrmLockerDashBoard frmDashBoard = new FrmLockerDashBoard();
                                    frmDashBoard.MdiParent = this.MdiParent;
                                    frmDashBoard.Show();
                                    frmDashBoard.WindowState = FormWindowState.Normal;                                  
                                }
                            }
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "", "UserName and Password Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {

                        MetroMessageBox.Show(this, "", "\n Please select branch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboBranch.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }

        }

        private void cboBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
                

        }

        private void frmLogin_Activated(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPwd.Text = "";
            cboBranch.SelectedIndex = -1;
            txtUserName.Focus();
        }


        private void btnLogin_MouseHover(object sender, EventArgs e)
        {
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Login_3));
        }
        private void btnLogin_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Login_2));
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Login_1));
        }

        private void btnLogin_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Login_1));
        }

        //cancel
        private void btnCancel_MouseHover(object sender, EventArgs e)
        {
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Cancel_3));
        }

        private void btnCancel_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.cancel_1));
        }



        private void btnCancel_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.cancel_2));
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.cancel_1));
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            
        }

        //rfid
        private void btnRFID_MouseHover(object sender, EventArgs e)
        {
            this.btnRFID.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.RFID_3));
        }

        private void btnRFID_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnRFID.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.RFID_2));
        }

        private void btnRFID_MouseLeave(object sender, EventArgs e)
        {
            this.btnRFID.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.RFID_1));
        }

        private void btnRFID_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnRFID.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.RFID_1));
        }

        private void btnFace_Click(object sender, EventArgs e)
        {
           
            Process.Start(@"C:\Users\hari\Desktop\FR\project.exe");

            
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul();
            if (str1 != "Card not in contact")
            {
                timer1.Stop();
                if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")
                    str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi();
                str1 = RemoveSpecialCharacters(str1.Trim());
                if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Cardnotincontact")
                {
                    MessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    timer1.Start();
                }
                else
                {
                    txtRFID.Text = str1;
                    BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "tblEmployee.ICardNo,tblEmployee.BranchId, tblUserAccount.UserAccountId, tblUserAccount.Username, tblUserAccount.PasswordHash, tblUserAccount.PasswordSalt,tblEmployee.RFID_Login_Status";
                    objSearch.Table = " tblEmployee INNER JOIN tblUserAccount ON tblEmployee.EmployeeId = tblUserAccount.EmployeeId";
                    objSearch.WhereNdGroupBy = "where tblEmployee.ICardNo ='" + str1 + "' And tblEmployee.Status=1";
                    objSearch.OrderBy = "Order By UserAccountId ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    //mcmbUserAccountType.SelectedValue = gridSource[0].UserAccountTypeId;
                    if (gridSource.Count > 0)
                    {
                        if (gridSource[0].RFID_Login_Status == 1)
                        {
                            txtUserName.Text = Convert.ToString(gridSource[0].Username);
                            txtPwd.Text = Convert.ToString(gridSource[0].PasswordHash);
                            passwordSalt = gridSource[0].PasswordSalt;
                            passwordHash = gridSource[0].PasswordHash;
                            encryptPassword = gridSource[0].PasswordHash;
                            rfid = 1;
                        }

                    }
                    txtUserName_Leave(null, null);
                    btnLogin_Click(sender, e);
                }
            }
        }
    }
}


