using BLL;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
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
//using AForge.Video;
//using AForge.Video.DirectShow;
using MetroFramework;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using ClsGeneral = FoodandBeverage.Classes.ClsGeneral;

using System.Drawing.Drawing2D;

using System.Runtime.InteropServices;
using CustomMessageBox;

namespace FoodandBeverage.Forms.UserAccount
{
    public partial class FrmUserAccount : Form
    {
        string imageFolder = Path.GetDirectoryName(Application.ExecutablePath) + "\\Resources\\Icons\\";
        ClsGeneral objGeneral = new ClsGeneral();
        long UserAccountId = 0;
        long BranchId = FrmLogin.BranchId;
        //int UserStatus = -1;// FrmDashboard.UserStatus;
        long EmployeeId = 0;
        byte[] PasswordSalt, PasswordHash;
        bool EditPasswordMode = false;
        public FrmUserAccount()
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

        private void FrmUserAccount_Load(object sender, EventArgs e)
        {
            try
            {
                clear();
                FrmDashboard frmDash = new FrmDashboard();
                if (frmDash.ChkWritePermission(Convert.ToInt32(formsenum.User_Account)))
                {
                    mbtnSave.Enabled = true;

                }
                else
                {
                    mbtnSave.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                objGeneral.LogException("User Account Page  FrmUserAccount_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Account));
            }


        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtemp, txtuname, txtpassword, txtcconpassword })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }
            }

            catch (Exception ex)
            {
                objGeneral.LogException("User Account Page  textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Account));
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
                objGeneral.LogException("User Account Page  ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Account));
            }

        }

        #endregion


        public bool MandatoryCheck()
        {

            try
            {
                if (mcmbaccounttype.SelectedIndex == 0)
                {
                    CMessageBox.Show(" UserType is A Mandatory Field And All Red Colored Field Are Mandatory", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //else if (txtemp.Text == "" || txtemp.Text == string.Empty)
                //{
                //    //MessageBox.Show(" Employee is A Mandatory Field And All Red Colored Field Are Mandatory", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    //return false;
                //}
                else if (txtuname.Text == "" || txtuname.Text == string.Empty)
                {
                    CMessageBox.Show(" UserName is A Mandatory Field And All Red Colored Field Are Mandatory", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (txtpassword.Text == "" || txtpassword.Text == string.Empty)
                {
                    CMessageBox.Show(" Password is A Mandatory Field And All Red Colored Field Are Mandatory", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (txtcconpassword.Text == "" || txtcconpassword.Text == string.Empty)
                {
                    CMessageBox.Show("Confirm Password is A Mandatory Field And All Red Colored Field Are Mandatory", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (txtpassword.Text.Trim() != txtcconpassword.Text.Trim())
                {
                    CMessageBox.Show(" Password and Confirm password are not same", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                objGeneral.LogException("User Account Page  FrmUserAccount_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Account));
                return false;
            }


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

            try
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
            catch (Exception ex)
            {
                objGeneral.LogException("User Account Page  FillCombo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Account));
            }
        }
        public void filldetails(int empid)
        {
            try
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
            catch (Exception ex)
            {
                objGeneral.LogException("User Account Page  filldetails()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Account));

            }

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
                    _objSearch.WhereNdGroupBy = "Where emp.Status=1 and emp.BranchId = " + BranchId;
                    _objSearch.OrderBy = " Order By EmployeeName ASC ";
                    using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 30, (this.Height - 30), this.Location.Y + 50, this.Location.X + 50))
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
                    objGeneral.LogException("User Account Page  txtemp_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Account));
                }
            }
        }

        private void txtuname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SendKeys.Send("{TAB}");
                }
            }
            catch (Exception ex)
            {
                objGeneral.LogException("User Account Page  txtuname_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Account));

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
                CMessageBox.Show(" Password and Confirm password are not same", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chkshowpassword_CheckedChanged(object sender, EventArgs e)
        {
            txtpassword.PasswordChar = chkshowpassword.Checked ? '\0' : '*';
            txtcconpassword.PasswordChar = chkshowpassword.Checked ? '\0' : '*';
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
                _objSearch.WhereNdGroupBy = "Where ua.Status=1 and ua.branchid = " + FrmLogin.BranchId;
                //}
                //else
                //{
                //    _objSearch.WhereNdGroupBy = "Where ua.Status=1 and branchid =" + frmLogin.BranchId +" and ua.UserAccountId=" + frmLogin.UserId;
                //}
                _objSearch.OrderBy = " Order By Username ASC ";

                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 80, (this.Height - 130), this.Location.Y + 50, this.Location.X + 20))
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
                else
                {
                    CMessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                objGeneral.LogException("User Account Page  mbtnList_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Account));
                // CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void mbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {
                    BllUserAccount bllUserAccount = new BllUserAccount();
                    DmnUserAccount dmnUserAccount = new DmnUserAccount();
                    BllEncryption bllEncryption = new BllEncryption();
                    dmnUserAccount.UserAccountId = UserAccountId;
                    dmnUserAccount.UserAccountTypeId = Convert.ToInt32(mcmbaccounttype.SelectedValue);
                    dmnUserAccount.Username = txtuname.Text;
                    dmnUserAccount.BranchId = FrmLogin.BranchId;
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
            catch (Exception ex)
            {
                objGeneral.LogException("User Account Page  mbtnSave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Account));
            }

        }
        private void FrmUserAccount_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtemp_Leave(object sender, EventArgs e)
        {
            lblToolTipAccName.Visible = false;
        }
    }
}
