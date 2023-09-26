using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using AForge.Video;
using AForge.Video.DirectShow;
using MetroFramework;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using pLocker.Classes;
using ClsGeneral = pLocker.Classes.ClsGeneral;

namespace pLocker.Forms.UserAccount
{
    public partial class FrmUserAccount : Form
    {
        string imageFolder = Path.GetDirectoryName(Application.ExecutablePath) + "\\Resources\\Icons\\";
        ClsGeneral objGeneral = new ClsGeneral();
        long UserAccountId = 0;
        long BranchId = frmLogin.BranchId;
        //int UserStatus = -1;// FrmDashboard.UserStatus;
        long EmployeeId = 0;
        byte[] PasswordSalt, PasswordHash;
        bool EditPasswordMode = false;
        public FrmUserAccount()
        {
            InitializeComponent();
        }

        private void FrmUserAccount_Load(object sender, EventArgs e)
        {

        }
        public bool MandatoryCheck()
        {
            if (mcmbaccounttype.SelectedIndex == 0)
            {
                MessageBox.Show(" UserType is A Mandatory Field And All Red Colored Field Are Mandatory", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtemp.Text == "" || txtemp.Text == string.Empty)
            {
                //MessageBox.Show(" Employee is A Mandatory Field And All Red Colored Field Are Mandatory", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return false;
            }
            else if (txtuname.Text == "" || txtuname.Text == string.Empty)
            {
                MessageBox.Show(" UserName is A Mandatory Field And All Red Colored Field Are Mandatory", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtpassword.Text == "" || txtpassword.Text == string.Empty)
            {
                MessageBox.Show(" Password is A Mandatory Field And All Red Colored Field Are Mandatory", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtcconpassword.Text == "" || txtcconpassword.Text == string.Empty)
            {
                MessageBox.Show("Confirm Password is A Mandatory Field And All Red Colored Field Are Mandatory", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtpassword.Text.Trim() != txtcconpassword.Text.Trim())
            {
                MessageBox.Show(" Password and Confirm password are not same", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void mcmbaccounttype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtemp_Enter(object sender, EventArgs e)
        {
            lblToolTipAccName.Visible = true;
        }
        public void clear()
        {
            txtemp.Text = "";
            txtuname.Text = "";
            txtpassword.Text = "";
            txtcconpassword.Text = "";
            FillCombo();

        }
        public void FillCombo()
        {
            BllSearch<DmnComboBox> bllacctype = new BllSearch<DmnComboBox>();
            DmnSearch dmnacctype = new DmnSearch();
            dmnacctype.Select = "UserAccountTypeId,UserAccountType";
            dmnacctype.Table = " tblUserAccountType ";
            dmnacctype.WhereNdGroupBy = "Where Status = 1 ";//AND DepartmentId = " + mcmbDepartmentId.SelectedValue;
            dmnacctype.OrderBy = "order by Value";
            var acctype = bllacctype.GetSearchData("prdComboFill", dmnacctype);
            mcmbaccounttype.DataSource = acctype;
            mcmbaccounttype.DisplayMember = "Value";
            mcmbaccounttype.ValueMember = "Id";
        }
        public void filldetails(int empid)
        {
            BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = " u.*,e.EmployeeId,e.EmployeeName,t.UserAccountType ";
            objSearch.Table = " tblEmployee e left join tblUserAccount u on u.EmployeeId=e.EmployeeId " +
                "left join tblUserAccountType t on t.UserAccountTypeId = u.UserAccountTypeId";
            objSearch.WhereNdGroupBy = "Where e.EmployeeId= " + empid;
            objSearch.OrderBy = "Order By UserAccountId ";
            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
            UserAccountId = gridSource[0].UserAccountId;
            mcmbaccounttype.SelectedValue = gridSource[0].UserAccountTypeId;
            txtuname.Text = Convert.ToString(gridSource[0].Username);
            txtemp.Text = Convert.ToString(gridSource[0].EmployeeName);
            EmployeeId = gridSource[0].EmployeeId;
            BranchId = gridSource[0].BranchId;
            PasswordSalt = gridSource[0].PasswordSalt;
            PasswordHash = gridSource[0].PasswordHash;
            txtpassword.Text = "";
            txtcconpassword.Text = "";
        }
        private void txtemp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
            if (e.KeyCode == Keys.F8)
            {
                try
                {
                    clear();
                    string returnValue = string.Empty;
                    DmnSearch _objSearch = new DmnSearch();
                    _objSearch.ColumnHeaders = "Id,Employee,Code,Department,Designation";
                    _objSearch.Select = "EmployeeId as Id, emp.EmployeeName as Field1,emp.EmployeeCode as Field2,dp.DepartmentName as Field3,ds.DesignationName as Field4";
                    _objSearch.Table = "tblEmployee as emp " +
                                   "left join tblDepartment as dp on emp.DepartmentId = dp.DepartmentId " +
                                   "left join tblDesignation as ds on emp.DesignationId = ds.DesignationId";
                    _objSearch.WhereNdGroupBy = "Where emp.Status=1 and emp.BranchId = " + frmLogin.BranchId;
                    _objSearch.OrderBy = " Order By EmployeeName ASC ";
                    using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20))
                    {
                        var dialogResult = search.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                        {
                            returnValue = Convert.ToString(search._retValue);

                        }
                    }
                    if (returnValue.ToString() != "")
                    {
                        filldetails(Convert.ToInt32(returnValue));
                    }
                }
                catch (Exception ex)
                {

                }
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

        private void txtcconpassword_Leave(object sender, EventArgs e)
        {
            if (txtpassword.Text.Trim() != txtcconpassword.Text.Trim())
            {
                MessageBox.Show(" Password and Confirm password are not same", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chkshowpassword_CheckedChanged(object sender, EventArgs e)
        {
            txtpassword.PasswordChar = chkshowpassword.Checked ? '\0' : '*';
            txtcconpassword.PasswordChar = chkshowpassword.Checked ? '\0' : '*';
        }

        private void mbtnSave_Click(object sender, EventArgs e)
        {
            if (MandatoryCheck())
            {
                BllUserAccount bllUserAccount = new BllUserAccount();
                DmnUserAccount dmnUserAccount = new DmnUserAccount();
                BllEncryption bllEncryption = new BllEncryption();
                dmnUserAccount.UserAccountId = UserAccountId;
                dmnUserAccount.UserAccountTypeId = Convert.ToInt32(mcmbaccounttype.SelectedValue);
                dmnUserAccount.Username = txtuname.Text;
                dmnUserAccount.BranchId = frmLogin.BranchId;
                dmnUserAccount.EmployeeId = EmployeeId;
                dmnUserAccount.UserAccountId = UserAccountId;
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
                        MessageBox.Show("User Account Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Unable To Save ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
                else
                {

                    bool saveStatus = bllUserAccount.UpdateData("prdUpdateUserAccount", dmnUserAccount);
                    if (saveStatus)
                    {
                        MessageBox.Show("User Account Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Unable To Save ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void mbtnRefresh_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void mbtnList_Click(object sender, EventArgs e)
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

                //if (frmLogin.UserAccountTypeId == 1)
                //{
                _objSearch.WhereNdGroupBy = "Where ua.Status=1 and ua.branchid = " + frmLogin.BranchId;
                //}
                //else
                //{
                //    _objSearch.WhereNdGroupBy = "Where ua.Status=1 and branchid =" + frmLogin.BranchId +" and ua.UserAccountId=" + frmLogin.UserId;
                //}
                _objSearch.OrderBy = " Order By Username ASC ";

                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);
                        UserAccountId = Convert.ToInt32(returnValue);
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
                    mcmbaccounttype.SelectedValue = gridSource[0].UserAccountTypeId;
                    txtuname.Text = Convert.ToString(gridSource[0].Username);
                    txtemp.Text = Convert.ToString(gridSource[0].EmployeeName);
                    EmployeeId = gridSource[0].EmployeeId;
                    BranchId = gridSource[0].BranchId;
                    PasswordSalt = gridSource[0].PasswordSalt;
                    PasswordHash = gridSource[0].PasswordHash;
                    txtpassword.Text = "";
                    txtcconpassword.Text = "";


                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtemp_Leave(object sender, EventArgs e)
        {
            lblToolTipAccName.Visible = false;
        }
    }
}
