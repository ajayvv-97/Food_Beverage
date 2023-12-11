using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        #region Declarations



        //ClsGeneral objGeneral = new ClsGeneral();// for validation
        BllEncryption bllEncryption = new BllEncryption();
        //BllUserAccount bllUserAccount = new BllUserAccount();
        byte[] passwordHash;
        byte[] passwordSalt;
        public static long UserId;
        public static long BranchId;
        public static string EmpCode = "";
        public static string EmployeName = "";
        public static string Designation = "";
        public static long EmployeeId;
        int UserStatus;
        public string txtbox = "";
        long CompanyId;
        public static long UserAccountTypeId;
        public static int rfid = 0;
        string photo;
        bool imageExist;
        ClsDocument clsDocument = new ClsDocument();
        public static int flag = 0;
        public byte[] encryptPassword;
        public bool loginkeyboard = false;
        private bool passwordVisible = false;
        #endregion

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            textb_shape();
        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtPwd, txtUserName })
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

        private void txtusername_Enter(object sender, EventArgs e)
        {
            timer1.Start();
            if (txtUserName.Text == "User Name .....")
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.Black;
            }
        }

        private void txtpassword_Enter(object sender, EventArgs e)
        {
            if (txtPwd.Text == "Password .....")
            {
                txtPwd.Text = "";
                txtPwd.ForeColor = Color.Black;
            }
        }

        private void txtusername_Leave(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Text = "User Name .....";
                txtUserName.ForeColor = Color.DarkGray;
            }
        }

        private void txtpassword_Leave(object sender, EventArgs e)
        {
            if (txtPwd.Text == "")
            {
                txtPwd.Text = "Password .....";
                txtPwd.ForeColor = Color.DarkGray;
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    if (IsUsernameValid())
                    {
                        if (IsPasswordvalid())
                        {
                            #region retrieve data after login

                            FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                            DmnSearch objSearch = new DmnSearch();

                            objSearch.ColumnHeaders = "UserAccountId,Status,EmployeeId,BranchId,CompanyId,UserAccountTypeId,EmployeeCode,Status,EmployeeName,Designation";
                            objSearch.Select = " ua.UserAccountId as Id,ua.[Status] as Field1,emp.EmployeeId as Field2,ua.BranchId as Field3," +
                                "ua.UserAccountTypeId as Field5,emp.EmployeeCode as Field6,f.Status as Field7,emp.EmployeeName as Field8 ,d.DesignationName as Field9 ";
                            objSearch.Table = " tblUserAccount ua " +
                                "Left join  tblEmployee emp on emp.EmployeeId = ua.EmployeeId " +
                                " Left join tblForms f on f.UserType = ua.UserAccountTypeId and f.Status = 1 " +
                                "Left join tblDesignation d on d.DesignationId = emp.DesignationId ";//f.Form = " + Convert.ToInt32(PosForm.AllLocationAccess) + "and

                            objSearch.WhereNdGroupBy = " Where Username = '" + txtUserName.Text.Trim().ToUpper() + "'";
                            var userDetails = frmGridSearch.GetSearchGrid(objSearch);
                            UserAccountTypeId = Convert.ToInt64(userDetails["Field5", 0].Value);
                            int Allaccess = Convert.ToInt32(userDetails["Field7", 0].Value);
                            if (Allaccess != 1 && Convert.ToString(BranchId) != Convert.ToString(userDetails["Field3", 0].Value))
                            {
                                CMessageBox.Show("Invalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtUserName.Focus();
                            }
                            else
                            {
                                UserId = Convert.ToInt64(userDetails["Id", 0].Value);
                                UserStatus = Convert.ToInt16(userDetails["Field1", 0].Value);
                                EmployeeId = Convert.ToInt64(userDetails["Field2", 0].Value);
                                EmployeName = Convert.ToString(userDetails["Field8", 0].Value);
                                Designation = Convert.ToString(userDetails["Field9", 0].Value);
                                //CompanyId = Convert.ToInt64(userDetails["Field4", 0].Value);
                                UserAccountTypeId = Convert.ToInt64(userDetails["Field5", 0].Value);
                                EmpCode = userDetails["Field6", 0].Value.ToString();

                                #endregion
                                this.Visible = false;
                                Forms.FrmDashboard DashBoard = new FrmDashboard();
                                DashBoard.MdiParent = this.MdiParent;
                                DashBoard.ShowDialog();
                                DashBoard.WindowState = FormWindowState.Normal;

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool IsUsernameValid()
        {
            BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "PasswordHash,PasswordSalt ";
            objSearch.Table = " tblUserAccount ";
            objSearch.WhereNdGroupBy = " Where Username = '" + txtUserName.Text.Trim().ToUpper() + "'AND Status=1 ";
            var pwdDetails = bllSearch.GetSearchData("prdSearch", objSearch);
            if (pwdDetails.Count > 0)
            {
                //lblUser.Text = txtUserName.Text.Trim().ToUpper();
                passwordSalt = pwdDetails[0].PasswordSalt;
                passwordHash = pwdDetails[0].PasswordHash;
                encryptPassword = pwdDetails[0].PasswordHash;
                return true;
            }
            else
            {
                txtUserName.Focus();
                CMessageBox.Show("Invalid User Name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            CMessageBox.Show("Invalid Password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtPwd.Focus();
            return false;
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
                    CMessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            encryptPassword = gridSource[0].PasswordHash;
                            var data = new List<byte>();
                            rfid = 1;
                        }
                        btnlogin_Click(sender, e);
                    }
                }

            }
        }
        public bool IsValid()
        {
            if (txtUserName.Text == "User Name .....")
            {
                CMessageBox.Show("Please Enter UserName", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return false;
            }
            if (txtPwd.Text == "Password .....")
            {
                CMessageBox.Show("Please Enter Password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPwd.Focus();
                return false;
            }
            return true;
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
        }



        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
        }


        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    CMessageBox.Show("Please Enter UserName", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUserName.Focus();
                    return;
                }
                else
                    txtPwd.Focus();                
            }
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtPwd.Text))
                {
                    CMessageBox.Show("Please Enter Password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPwd.Focus();
                    return;
                }
                else
                    btnlogin.Focus();
            }
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {         
           txtPwd.PasswordChar = '*'; // Display asterisks
        }
    }
}
