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
        public static long BranchId;
        int UserStatus;
        long EmployeeId;

        long CompanyId;
        public static long UserAccountTypeId;

        string photo;
        bool imageExist;
        ClsDocument clsDocument = new ClsDocument();
        #endregion


        public frmLogin()
        {
            InitializeComponent();
        }



        private void btnRFID_Click(object sender, EventArgs e)
        {
            txtRFID.Text = AYJCardLib.AYJReadCardLib.CardChk();
            //txtRFID.Text = "AYJ1AYJ1E0010000001000001013";

            BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "tblEmployee.ICardNo, tblUserAccount.UserAccountId, tblUserAccount.Username, tblUserAccount.PasswordHash, tblUserAccount.PasswordSalt";
            objSearch.Table = " tblEmployee INNER JOIN tblUserAccount ON tblEmployee.EmployeeId = tblUserAccount.EmployeeId";
            objSearch.WhereNdGroupBy = "where tblEmployee.ICardNo ='" + txtRFID.Text + "' And tblEmployee.Status=0";
            objSearch.OrderBy = "Order By UserAccountId ";
            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
            //mcmbUserAccountType.SelectedValue = gridSource[0].UserAccountTypeId;
            txtUserName.Text = Convert.ToString(gridSource[0].Username);
            txtPwd.Text = Convert.ToString(gridSource[0].PasswordHash);
            passwordSalt = gridSource[0].PasswordSalt;
            passwordHash = gridSource[0].PasswordHash;


            txtUserName_Leave(null, null);
            //txtPwd_Leave(null, null);
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {

                BllSearch<DmnComboBox> bllSearchBranch = new BllSearch<DmnComboBox>();
                DmnSearch dmnSearchBranch = new DmnSearch();
                dmnSearchBranch.Select = "BranchId,BranchName";
                dmnSearchBranch.Table = " tblBranch ";
                dmnSearchBranch.WhereNdGroupBy = " Where Status=0 ";
                var branch = bllSearchBranch.GetSearchData("prdComboFill", dmnSearchBranch);
                cboBranch.DataSource = branch;
                cboBranch.DisplayMember = "Value";
                cboBranch.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            try
            {
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
                    objSearch.Select = " ua.UserAccountId as Id,emp.Employeename as Field1,br.Branchname as Field2,ua.UserAccountTypeId as Field3";
                    objSearch.Table = " tblUserAccount ua Left join  tblEmployee emp on emp.EmployeeId = ua.EmployeeId " +
                                        "Left join tblBranch br on br.BranchId = ua.BranchId ";
                    objSearch.WhereNdGroupBy = " Where Username = '" + txtUserName.Text.Trim().ToUpper() + "'";
                    var userdetails = frmGridSearch.GetSearchGrid(objSearch);

                    //if (userdetails.Rows.Count>0 && Convert.ToInt32(userdetails["Id",0].Value)>2)
                    UserAccountTypeId = Convert.ToInt64(userdetails["Field3", 0].Value);

                    if (userdetails.Rows.Count > 0)
                    {
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
                        MessageBox.Show( "Invalid UserName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUserName.Text))
                {
                    MessageBox.Show(this, "User is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPwd.Text = "";
                    txtUserName.Focus();
                }
                if (e.KeyCode == Keys.Enter)
                    SendKeys.Send("{TAB}");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show(this, "Invalid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool IsUsernameValid()
        {
            BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "PasswordHash,PasswordSalt ";
            objSearch.Table = " tblUserAccount ";
            objSearch.WhereNdGroupBy = " Where Username = '" + txtUserName.Text.Trim().ToUpper() + "'   AND Status=0 ";
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
            if (passwordSalt == null || passwordHash == null)
            {
                return false;
            }
            bool result = bllEncryption.verifyPassword(txtPwd.Text.Trim(), passwordSalt, passwordHash);
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
                        if (IsUsernameValid())
                        {
                            if (IsPasswordvalid())
                            {
                                #region retrieve data after login

                                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                                DmnSearch objSearch = new DmnSearch();
                                objSearch.ColumnHeaders = "UserAccountId,Status,EmployeeId,BranchId,CompanyId,UserAccountTypeId";
                                objSearch.Select = " ua.UserAccountId as Id,ua.[Status] as Field1,emp.EmployeeId as Field2,ua.BranchId as Field3,br.CompanyId as Field4 ,ua.UserAccountTypeId as Field5";
                                objSearch.Table = " tblUserAccount ua Left join  tblEmployee emp on emp.EmployeeId = ua.EmployeeId " +
                                    "Left join tblBranch br on br.BranchId = ua.BranchId ";
                                objSearch.WhereNdGroupBy = " Where Username = '" + txtUserName.Text.Trim().ToUpper() + "'";
                                var userDetails = frmGridSearch.GetSearchGrid(objSearch);

                                BranchId = Convert.ToInt64(cboBranch.SelectedValue);
                                UserAccountTypeId = Convert.ToInt64(userDetails["Field5", 0].Value);
                                if (UserAccountTypeId >= 2 && Convert.ToString(BranchId) != Convert.ToString(userDetails["Field3", 0].Value))
                                {
                                    MessageBox.Show(this, "\nInvalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtUserName.Focus();
                                }
                                else
                                {
                                    UserId = Convert.ToInt64(userDetails["Id", 0].Value);
                                    UserStatus = Convert.ToInt16(userDetails["Field1", 0].Value);
                                    EmployeeId = Convert.ToInt64(userDetails["Field2", 0].Value);

                                    CompanyId = Convert.ToInt64(userDetails["Field4", 0].Value);
                                    UserAccountTypeId = Convert.ToInt64(userDetails["Field5", 0].Value);

                                    objSearch.ColumnHeaders = "FinancialYearId";
                                    objSearch.Select = " geno.GTypeSubText1 as Field1 ";
                                    objSearch.Table = " tblGeneralOptions geno Left join tblGType gt on geno.GTypeId=gt.GTypeId ";
                                    objSearch.WhereNdGroupBy = "where GType='FINANCIAL YEAR'  COLLATE SQL_Latin1_General_CP1_CS_AS ";
                                    var finYr = frmGridSearch.GetSearchGrid(objSearch);
                                    //FinYrId = Convert.ToInt64(userDetails["Field1", 0].Value);
                                    // File obj = new File;
                                    //MessageBox.Show(this, "\n Ok", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    #endregion

                                    //CloseAllPanels();
                                    Forms.FrmDashBoard frmDashBoard = new FrmDashBoard();
                                    frmDashBoard.Show();
                                    frmDashBoard.WindowState = FormWindowState.Maximized;


                                    //Forms.FrmDashBoard.layoutForm = new Transactions.FrmSalesPOS(UserId, UserStatus, EmployeeId, BranchId, CompanyId, FinYrId);
                                    //this.Hide();
                                    //layoutForm.ShowDialog();

                                    //if (UserAccountTypeId == 2) // User
                                    //{
                                    //    Transactions.FrmSalesPOS layoutForm = new Transactions.FrmSalesPOS(UserId, UserStatus, EmployeeId, BranchId, CompanyId, FinYrId);
                                    //    this.Hide();
                                    //    layoutForm.ShowDialog();
                                    //}
                                    //else if (UserAccountTypeId == 5) // Loyalty
                                    //{
                                    //    Masters.FrmAccountLedger layoutForm = new Masters.FrmAccountLedger();
                                    //    this.Hide();
                                    //    layoutForm.ShowDialog();

                                    //}
                                    //else
                                    //{
                                    //    FrmDashboard layoutForm = new FrmDashboard(UserId, UserStatus, EmployeeId, BranchId, CompanyId, FinYrId);
                                    //    this.Hide();
                                    //    layoutForm.ShowDialog();
                                    //}
                                }
                            }
                            //else
                            //{
                            //    MetroMessageBox.Show(this, "\nInvalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                            //    mtxtUsername.Focus();

                            //}
                        }
                    }
                    else
                    {

                        MessageBox.Show(this, "\n Please select branch", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboBranch.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");

        }

        private void cboBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");

        }

        private void frmLogin_Activated(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPwd.Text = "";
            cboBranch.SelectedIndex = -1;
            txtUserName.Focus();
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
