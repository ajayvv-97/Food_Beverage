using BLL;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Forms.UserControls
{
    public partial class frmExtraAccessChk : Form
    {
        ClsGeneral objGeneral = new ClsGeneral();
        public  long UserAccountTypeId;
        public  long UserId;
        public  long BranchId;
        byte[] passwordHash;
        byte[] passwordSalt;
        public byte[] encryptPassword;
        public  int rfid = 0;
        BllEncryption bllEncryption = new BllEncryption();
        public  bool access = false;
        public  long EmployeeId;
        int FormId = 0;
        public frmExtraAccessChk(int formId)
        {
            FormId = formId;
            InitializeComponent();
        }

        private void frmExtraAccessChk_Load(object sender, EventArgs e)
        {
           
            //BllSearch<DmnComboBox> bllSearchBranch = new BllSearch<DmnComboBox>();
            //DmnSearch dmnSearchBranch = new DmnSearch();
            //dmnSearchBranch.Select = "LocId,Name";
            //dmnSearchBranch.Table = " tblLocationDetails ";
            //dmnSearchBranch.WhereNdGroupBy = " Where Status=1 ";
            //var branch = bllSearchBranch.GetSearchData("prdComboFill", dmnSearchBranch);
            //cboBranch.DataSource = branch;
            //cboBranch.DisplayMember = "Value";
            //cboBranch.ValueMember = "Id";
            //cboBranch.SelectedIndex = 0;
            access = false;
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            try
            {
                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Extra Aceess txtUserName_Enter()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.ExtraLoginAccess));

            }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    SendKeys.Send("{TAB}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Extra login Access page txtUserName_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.ExtraLoginAccess));

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
                   
                    objSearch.ColumnHeaders = "UserAccountId,Employeename,Branchname,UserAccountTypeId,Status";
                    objSearch.Select = " ua.UserAccountId as Id,emp.Employeename as Field1,ua.UserAccountTypeId as Field3,f.Status Field4";
                    objSearch.Table = " tblUserAccount ua Left join  tblEmployee emp on emp.EmployeeId = ua.EmployeeId " +
                                        " Left join tblForms f on f.UserType=ua.UserAccountTypeId and f.Form=" + Convert.ToInt32(FormId) + " and f.Status=1";
                    objSearch.WhereNdGroupBy = " Where emp.Status=1 and ua.Status=1 and Username = '" + txtUserName.Text.Trim().ToUpper() + "'";
                    var userdetails = frmGridSearch.GetSearchGrid(objSearch);

                    //if (userdetails.Rows.Count>0 && Convert.ToInt32(userdetails["Id",0].Value)>2)

                    if (userdetails.Rows.Count > 0)
                    {
                        UserAccountTypeId = Convert.ToInt64(userdetails["Field3", 0].Value);
                        UserId = Convert.ToInt64(userdetails["Id", 0].Value);
                        lblUser.Text = txtUserName.Text.Trim().ToUpper();
                        //if (Convert.ToInt32(userdetails["Field4", 0].Value) == 1)
                        //{
                        //    cboBranch.Text = Convert.ToString(userdetails["Field2", 0].Value);
                        //    cboBranch.Enabled = true;
                        //}
                        //else
                        //{
                        //    cboBranch.Text = Convert.ToString(userdetails["Field2", 0].Value);
                        //    cboBranch.Enabled = false;
                        //}
                    }
                    else if (userdetails.Rows.Count <= 0)
                    {
                        MessageBox.Show("Invalid UserName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Text = "";
                        txtUserName.Focus();
                    }

                }
                else
                {
                    txtUserName.Focus();
                    timer1.Start();
                }


                //BranchId = Convert.ToInt64(cboBranch.SelectedValue);
                if (string.IsNullOrWhiteSpace(txtUserName.Text))
                {
                    //pbUser.Image = null;
                    //pbUser.Image = Properties.Resources.Admin_1;
                }

                if ( txtUserName.Text != "" && txtPwd.Text != "")//cboBranch.Text != "" &&
                    btnLogin.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Login page txtUserName_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.ExtraLoginAccess));

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
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Login page txtPwd_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.ExtraLoginAccess));

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
                objGeneral.LogException("Login page txtPwd_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.ExtraLoginAccess));

            }
        }
        public bool IsUsernameValid()
        {
            try
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
                    encryptPassword = pwdDetails[0].PasswordHash;
                    return true;
                }
                else
                {
                    // MetroMessageBox.Show(this, "\nInvalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    txtUserName.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Login page IsUsernameValid()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.ExtraLoginAccess));
                return false;
            }
        }
        public bool IsPasswordvalid()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Login page IsPasswordvalid()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.ExtraLoginAccess));
                return false;
            }
        }

        private void cboBranch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    SendKeys.Send("{TAB}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Login page cboBranch_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.ExtraLoginAccess));


            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    //if (cboBranch.SelectedIndex != 0)
                    //{
                        if (IsUsernameValid())
                        {
                            if (IsPasswordvalid())
                            {
                            #region retrieve data after login

                            FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                            DmnSearch objSearch = new DmnSearch();
                            objSearch.ColumnHeaders = "UserAccountId,Status,EmployeeId,BranchId,CompanyId,UserAccountTypeId,EmployeeCode,Status,EmployeeName";
                            objSearch.Select = " ua.UserAccountId as Id,ua.[Status] as Field1,emp.EmployeeId as Field2,emp.BranchId as Field3 ,ua.UserAccountTypeId as Field5,emp.EmployeeCode as Field6,f.Status as Field7,emp.EmployeeName as Field8  ";
                            objSearch.Table = " tblUserAccount ua " +
                                "Left join  tblEmployee emp on emp.EmployeeId = ua.EmployeeId " +
                                " Left join tblForms f on f.UserType=ua.UserAccountTypeId and f.Form=" + Convert.ToInt32(formsenum.ExtraLoginAccess);
                            objSearch.WhereNdGroupBy = " Where Username = '" + txtUserName.Text.Trim().ToUpper() + "'";
                            var userDetails = frmGridSearch.GetSearchGrid(objSearch);
                            int Allaccess = Convert.ToInt32(userDetails["Field7", 0].Value);
                            if (Allaccess != 1)
                            {
                                MessageBox.Show(this, "\nInvalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtUserName.Focus();
                                access = false;
                            }
                            else
                            {
                                access = true;
                                EmployeeId = Convert.ToInt64(userDetails["Field2", 0].Value);
                                UserId = Convert.ToInt64(userDetails["Id", 0].Value);
                                this.Close();
                            }

                            #endregion
                        }

                    }
                    //}
                    //else
                    //{

                    //    MessageBox.Show(this, "\n Please select branch", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    cboBranch.Focus();
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Login page btnLogin_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.ExtraLoginAccess));

            }
        }
        public bool IsValid()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Login page IsValid()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.ExtraLoginAccess));
                return false;
            }
        }

        private void btnRFID_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            try
            {
                string str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul(FrmLogin.CorpID);
                if (str1 != "Card not in contact")
                {
                    timer1.Stop();
                    if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Invalid Card")
                        str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi(FrmLogin.CorpID);
                    str1 = RemoveSpecialCharacters(str1.Trim());
                    if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Cardnotincontact" || str1 == "InvalidCard")
                    {
                        MessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    {
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
                                timer1.Stop();
                                txtUserName.Text = Convert.ToString(gridSource[0].Username);
                                txtPwd.Text = Convert.ToString(gridSource[0].PasswordHash);
                                cboBranch.SelectedValue = Convert.ToInt64(gridSource[0].BranchId);
                                encryptPassword = gridSource[0].PasswordHash;
                                var data = new List<byte>();
                                rfid = 1;
                            }
                        }
                        txtUserName_Leave(null, null);

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Login page timer1_Tick_1()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.ExtraLoginAccess));

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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Extra Login Access page RemoveSpecialCharacters()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.ExtraLoginAccess));
                return string.Empty;
            }
        }

        private void frmExtraAccessChk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                switch (keyData)
                {                    
                    default:
                        return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Extra Login Access  page  ProcessCmdKey()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.ExtraLoginAccess));
                return false;
            }
        }
    }
}
