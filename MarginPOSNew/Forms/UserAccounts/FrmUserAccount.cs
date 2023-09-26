using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.Masters;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using MetroFramework;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarginPOSNew.Forms.UserAccounts
{
    public partial class FrmUserAccount : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        string imageFolder = Path.GetDirectoryName(Application.ExecutablePath) + "\\Resources\\Icons\\";
        ClsGeneral objGeneral = new ClsGeneral();
        long UserAccountId = 0;
        long BranchId = FrmDashboard.BranchId;
        //int UserStatus = -1;// FrmDashboard.UserStatus;
        long EmployeeId = 0;
        byte[] PasswordSalt, PasswordHash;
        bool EditPasswordMode = false;

        #endregion

        #region Functions
        public FrmUserAccount()
        {
            InitializeComponent();
        }
        public bool IsSavable()
        {
            Control[] controllist;
            if (EditPasswordMode)
            {
                controllist = new Control[] { mtxtEmployee, mcmbUserAccountType, mtxtUsername };

            }
            else
            {
                controllist = new Control[] { mtxtEmployee, mcmbUserAccountType, mtxtUsername, mtxtPassword, mtxtConfirmPwd };

            }
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)

            {
                return false;
            }
            else if (IsUserExist() == true)
            {
                MetroMessageBox.Show(this, "\nUsername already exists", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                return false;
            }
            else if (string.Compare(mtxtPassword.Text, mtxtConfirmPwd.Text) != 0)
            {
                MetroMessageBox.Show(this, "\nPassword mismatch", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                return false;
            }

            else
            {
                return true;
            }
        }
        public bool IsUserExist()
        {
            DmnUserAccount dmnUserAccount = new DmnUserAccount();
            BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = " Username";
            dmnSearch.Table = " tblUserAccount";
            if (UserAccountId == 0)
                dmnSearch.WhereNdGroupBy = "Where Username='" + mtxtUsername.Text.Trim() + "'  and Status=0 ";
            else
                dmnSearch.WhereNdGroupBy = "Where Username='" + mtxtUsername.Text.Trim() + "' and Status=0 and UserAccountId<>'" + UserAccountId + "'";
            var user = bllSearch.GetSearchData("prdSearch", dmnSearch);
            if (user.Count > 0)
            {
                return true;
            }

            else
            {
                return false;
            }

        }
        public void FillCombo()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = " UserAccountTypeId,UserAccountType";
            dmnSearch.Table = " tblUserAccountType";
            dmnSearch.WhereNdGroupBy = " Where Status = 0";
            var comboSource = bllSearch.GetSearchData("prdComboFill", dmnSearch);
            mcmbUserAccountType.DataSource = comboSource;
            mcmbUserAccountType.DisplayMember = "Value";
            mcmbUserAccountType.ValueMember = "Id";
        }
        public void RefreshForm()
        {
            UserAccountId = 0;
            hlTitle.Text = "User Account";
            objGeneral.ResetAllTextBoxes(this);
            FillCombo();
            // mcmbUserAccountType.Focus();
            mbEmployee.Focus();
            mbtnDelete.Enabled = false;
            // pbPassword.Image = null;-----------------COMMENTED NEED O CHANGE VISI]BILITY
            mtxtPassword.ShowButton = false;
            EditPasswordMode = false;
            pbPassword.Visible = false;
            pbIncorrectPwd.Visible = false;
            if (FrmEmployee.EmployeeId > 0)
            {
                EmployeeId = FrmEmployee.EmployeeId;
                mbEmployee.Enabled = false;
                BllSearch<DmnEmployee> bllSearch = new BllSearch<DmnEmployee>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "EmployeeId,EmployeeName,BranchId";
                objSearch.Table = "tblEmployee";
                objSearch.WhereNdGroupBy = "Where EmployeeId = " + EmployeeId;
                objSearch.OrderBy = "Order By EmployeeId ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                mtxtEmployee.Text = Convert.ToString(gridSource[0].EmployeeName);
                EmployeeId = gridSource[0].EmployeeId;
                BranchId = gridSource[0].BranchId;
            }
        }
        public void SetpbImage()
        {
            if (EditPasswordMode)
            {
                pbPassword.Visible = false;
                pbIncorrectPwd.Visible = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(mtxtPassword.Text) && !string.IsNullOrEmpty(mtxtConfirmPwd.Text))
                {
                    if (string.Compare(mtxtPassword.Text, mtxtConfirmPwd.Text) == 0)
                    {
                        pbPassword.Visible = true;
                        pbIncorrectPwd.Visible = false;
                    }
                    else
                    {
                        pbIncorrectPwd.Visible = true;
                        pbPassword.Visible = false;
                    }
                }
                else
                {

                }
                
               

            }

        }
        #endregion

        #region Events
        private void FrmUserAccount_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshForm();
                if (FrmLogin.UserAccountTypeId != 1)
                {
                    BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "ua.UserAccountTypeId,ua.EmployeeId,ua.Username,emp.BranchId,ua.PasswordSalt,ua.PasswordHash,emp.EmployeeName";
                    objSearch.Table = " tblUserAccount ua " +
                        "left join tblEmployee emp on ua.EmployeeId= emp.EmployeeId";
                    objSearch.WhereNdGroupBy = "Where UserAccountId = " + FrmLogin.UserId;
                    objSearch.OrderBy = "Order By UserAccountId ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    mcmbUserAccountType.SelectedValue = gridSource[0].UserAccountTypeId;
                    mtxtUsername.Text = Convert.ToString(gridSource[0].Username);
                    mtxtEmployee.Text = Convert.ToString(gridSource[0].EmployeeName);
                    EmployeeId = gridSource[0].EmployeeId;
                    BranchId = gridSource[0].BranchId;
                    PasswordSalt = gridSource[0].PasswordSalt;
                    PasswordHash = gridSource[0].PasswordHash;
                    mtxtPassword.Text = "    ";
                    mtxtConfirmPwd.Text = "    ";
                    hlTitle.Text = "User Account [Edit]";
                    mbtnDelete.Enabled = true;
                    mtxtPassword.ShowButton = true;
                    EditPasswordMode = true;
                    pbPassword.Visible = false;
                    pbIncorrectPwd.Visible = false;

                    mbEmployee.Enabled = false;
                    mcmbUserAccountType.Enabled = false;
                    mbtnDelete.Enabled = false;
                }

                }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        private void FrmUserAccount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode==Keys.Enter)
                    objGeneral.NextConrolOnEnterPress(sender, e);
                if (e.KeyCode == Keys.F2)
                    MbtnSave_Click(sender, e);
                if (e.KeyCode == Keys.F3)
                    MbtnList_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    MbtnRefresh_Click(sender, e);
                if (e.KeyCode == Keys.F7)
                    MbtnDelete_Click(sender, e);
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnSave_Click(object sender, EventArgs e)
        {
            DmnUserAccount userAccount;
            bool saveStatus;
            BllEncryption bllEncryption = new BllEncryption();
            try
            {
                if (IsSavable())
                {
                    BllUserAccount bllUserAccount = new BllUserAccount();
                    DmnUserAccount dmnUserAccount = new DmnUserAccount();
                    dmnUserAccount.UserAccountTypeId = Convert.ToInt32(mcmbUserAccountType.SelectedValue);
                    dmnUserAccount.Username = mtxtUsername.Text.Trim().ToUpper();
                    dmnUserAccount.BranchId = BranchId;
                    dmnUserAccount.EmployeeId = EmployeeId;


                    if (UserAccountId == 0)

                    {
                        if (IsEmployeeExists())
                        {
                            MetroMessageBox.Show(this, "\nEmployee is an existing user ", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                        else
                        {

                            bllEncryption.encrptPassword(mtxtPassword.Text, out PasswordSalt, out PasswordHash);
                            dmnUserAccount.PasswordSalt = PasswordSalt;
                            dmnUserAccount.PasswordHash = PasswordHash;
                            dmnUserAccount.CreatedBy = FrmDashboard.UserId;
                            userAccount = bllUserAccount.AddData("prdInsertUserAccount", dmnUserAccount);
                            UserAccountId = userAccount.UserAccountId;
                            MetroMessageBox.Show(this, "\nSaved successfully " + mtxtUsername.Text, "Success ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                            if (FrmEmployee.EmployeeId > 0)
                            {
                                this.Close();
                            }
                                RefreshForm();
                        }
                    }
                    else
                    {
                        dmnUserAccount.UserAccountId = UserAccountId;
                        dmnUserAccount.ModifiedBy = FrmDashboard.UserId;
                        if (!EditPasswordMode)
                        {
                            bllEncryption.encrptPassword(mtxtPassword.Text, out PasswordSalt, out PasswordHash);
                            dmnUserAccount.PasswordSalt = PasswordSalt;
                            dmnUserAccount.PasswordHash = PasswordHash;
                        }
                        else
                        {
                            dmnUserAccount.PasswordSalt = PasswordSalt;
                            dmnUserAccount.PasswordHash = PasswordHash;
                        }
                        saveStatus = bllUserAccount.UpdateData("prdUpdateUserAccount", dmnUserAccount);
                        if (saveStatus)
                            MetroMessageBox.Show(this, "\nUpdated successfully " + mtxtUsername.Text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        RefreshForm();
                    }

                }


            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        public bool IsEmployeeExists()
        {
            FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.ColumnHeaders = "Username";
            objSearch.Select = " count(UserAccountId)  as Id";
            objSearch.Table = "tblUserAccount  ";
            objSearch.WhereNdGroupBy = "where  EmployeeId='" + EmployeeId + "' And Status=0 ";
            var result = frmGridSearch.GetSearchGrid(objSearch);
            int count = Convert.ToInt32(result["Id", 0].Value);
            if (count > 0)

            {
                return true;
            }
            return false;
        }
        private void MbtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshForm();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        private void MbtnDelete_Click(object sender, EventArgs e)
        {
            bool deleteStatus;
            try
            {
                DialogResult dialogResult = MetroMessageBox.Show(this, "\nDo you want to delete ? ", "Delete ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150);
                if (dialogResult == DialogResult.Yes)
                {
                    BllUserAccount bllUserAccount = new BllUserAccount();
                    DmnUserAccount dmnUserAccount = new DmnUserAccount();
                    dmnUserAccount.UserAccountId = UserAccountId;
                    dmnUserAccount.ModifiedBy = FrmDashboard.UserId;
                    deleteStatus = bllUserAccount.DeleteById("prdDeleteUserAccount", dmnUserAccount);
                    if (deleteStatus)
                        MetroMessageBox.Show(this, "\nDeleted successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                    RefreshForm();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        private void MbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Username,User Account Type,Employee Name";
                _objSearch.Select = "UserAccountId as Id,ua.Username as Field1, ut.UserAccountType as Field2,emp.EmployeeName as Field3";
                _objSearch.Table = "tblUserAccount as ua " +
                               "left join tblUserAccountType as ut on ua.UserAccountTypeId = ut.UserAccountTypeId " +
                               "left join tblEmployee as emp on ua.EmployeeId = emp.EmployeeId";
                if (FrmLogin.UserAccountTypeId == 1)
                {
                    _objSearch.WhereNdGroupBy = "Where ua.Status=0";
                }
                else
                {
                    _objSearch.WhereNdGroupBy = "Where ua.Status=0 and ua.UserAccountId=" + FrmLogin.UserId;
                }
                _objSearch.OrderBy = " Order By Username ASC ";
                
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 800, 400, 190, 250))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);

                    }
                }
                if (returnValue.ToString() != "")
                {
                    UserAccountId = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "ua.UserAccountTypeId,ua.EmployeeId,ua.Username,emp.BranchId,ua.PasswordSalt,ua.PasswordHash,emp.EmployeeName";
                    objSearch.Table = " tblUserAccount ua " +
                        "left join tblEmployee emp on ua.EmployeeId= emp.EmployeeId";
                    objSearch.WhereNdGroupBy = "Where UserAccountId = " + UserAccountId;
                    objSearch.OrderBy = "Order By UserAccountId ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    mcmbUserAccountType.SelectedValue = gridSource[0].UserAccountTypeId;
                    mtxtUsername.Text = Convert.ToString(gridSource[0].Username);
                    mtxtEmployee.Text = Convert.ToString(gridSource[0].EmployeeName);
                    EmployeeId = gridSource[0].EmployeeId;
                    BranchId = gridSource[0].BranchId;
                    PasswordSalt = gridSource[0].PasswordSalt;
                    PasswordHash = gridSource[0].PasswordHash;
                    mtxtPassword.Text = "    ";
                    mtxtConfirmPwd.Text = "    ";
                    hlTitle.Text = "User Account [Edit]";
                    mbtnDelete.Enabled = true;
                    mtxtPassword.ShowButton = true;
                    EditPasswordMode = true;
                    pbPassword.Visible = false;
                    pbIncorrectPwd.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtConfirmPwd_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                EditPasswordMode = false;
                SetpbImage();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    EditPasswordMode = true;
                    mtxtPassword.Text = "    ";
                    mtxtConfirmPwd.Text = "    ";
                }
                else
                {
                    EditPasswordMode = false;
                }
                SetpbImage();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void MtxtPassword_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                mtxtPassword.Text = "    ";
                mtxtConfirmPwd.Text = "    ";
                EditPasswordMode = true;
                pbPassword.Visible = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void MbEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Employee Name,Employee Code";
                _objSearch.Select = "EmployeeId as Id,EmployeeName as Field1,EmployeeCode as Field2";
                _objSearch.Table = "tblEmployee";
                _objSearch.WhereNdGroupBy = "Where Status=0";
                _objSearch.OrderBy = " Order By EmployeeName ASC ";
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 800, 400, 190, 250))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);

                    }
                }
                if (returnValue.ToString() != "")
                {
                    EmployeeId = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnEmployee> bllSearch = new BllSearch<DmnEmployee>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "EmployeeId,EmployeeName,BranchId";
                    objSearch.Table = "tblEmployee";
                    objSearch.WhereNdGroupBy = "Where EmployeeId = " + EmployeeId;
                    objSearch.OrderBy = "Order By EmployeeId ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    mtxtEmployee.Text = Convert.ToString(gridSource[0].EmployeeName);
                    EmployeeId = gridSource[0].EmployeeId;
                    BranchId = gridSource[0].BranchId;

                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        #endregion
    }
}
