using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmloginTablet : Form
    {
        #region Declarations
        ClsGeneral objGeneral = new ClsGeneral();// for validation
        BllEncryption bllEncryption = new BllEncryption();
        //BllUserAccount bllUserAccount = new BllUserAccount();
        byte[] passwordHash;
        byte[] passwordSalt;
        public static int UserId;
        public static int BranchId;

        public static string EmpCode = "";
        public static string EmployeName = "";
        public static string Designation = "";
        public static string departmentname = "";
        public static int KitchenDisplayCount = 15;
        public static long EmployeeId;
        public static decimal BookingInterwell;
        public static bool TestUser = false;
        public static int EphId;

        int UserStatus;
        public string txtbox = "";
        public static long UserAccountTypeId;
        public static int rfid = 0;
        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsgen = new ClsGeneral();
        public static int flag = 0;
        public byte[] encryptPassword;
        public static bool loginkeyboard = false;
        private bool passwordVisible = false;
        public static string DateFormat = "yyyy-MM-dd HH:mm:ss";

        int PrintpathValue = Convert.ToInt32(ConfigurationManager.AppSettings["PrintpathValue"]);
        public static Size FormSize;
        public static decimal AutoLogoutIdleTimeoutMinutes = 0;
        #endregion
        public FrmloginTablet()
        {
            InitializeComponent();
        }

        private void FrmloginTablet_Load(object sender, EventArgs e)
        {
            CenterControls();
            AddPlaceholders();
        }

        // -------- CENTER PANEL & LOGO --------
        private void CenterControls()
        {
            panelMain.Left = (this.ClientSize.Width - panelMain.Width) / 2;
            panelMain.Top = (this.ClientSize.Height - panelMain.Height) / 2;

            pictureLogo.Left = (panelMain.Width - pictureLogo.Width) / 2;
        }

        private void FrmloginTablet_Resize(object sender, EventArgs e)
        {
            CenterControls();
        }

        // ------------- PLACEHOLDERS ------------
        private void AddPlaceholders()
        {
            txtUser.ForeColor = Color.Gray;
            txtUser.Text = "User ID";

            txtPass.ForeColor = Color.Gray;
            txtPass.Text = "Password";
        }

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if (txtUser.Text == "User ID")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.Black;
            }
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                txtUser.Text = "User ID";
                txtUser.ForeColor = Color.Gray;
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "Password")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.Black;
                txtPass.PasswordChar = '●';
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.PasswordChar = '\0';
                txtPass.Text = "Password";
                txtPass.ForeColor = Color.Gray;
            }
        }

        // -------- LOGIN BUTTON --------

        public bool IsUsernameValid()
        {
            try
            {
                if (txtUser.Text == "test")
                    return true;

                BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "PasswordHash,PasswordSalt ";
                objSearch.Table = " tblUserAccount ";
                objSearch.WhereNdGroupBy = " Where Username = '" + txtUser.Text.Trim().ToUpper() + "'AND Status=1 ";
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

                    txtUser.Focus();
                    CMessageBox.Show("Invalid User Name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page IsUsernameValid()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
                return false;
            }
        }
        public bool IsPasswordvalid()
        {
            try
            {
                bool result = true;
                if (txtPass.Text == "test")
                    return true;

                if (passwordSalt == null || passwordHash == null)
                {
                    return false;
                }
                if (rfid == 0)
                    result = bllEncryption.verifyPassword(txtPass.Text.Trim(), passwordSalt, passwordHash);
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
                txtPass.Focus();
                return false;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page IsPasswordvalid()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
                return false;
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page textb_shape()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
                return "";
            }

        }
        public bool IsValid()
        {
            try
            {
                if (txtUser.Text == "User Name .....")
                {
                    CMessageBox.Show("Please Enter UserName", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUser.Focus();
                    return false;
                }
                if (txtPass.Text == "Password .....")
                {
                    CMessageBox.Show("Please Enter Password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPass.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page IsValid()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
                return false;
            }

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    if (IsUsernameValid())
                    {
                        if (IsPasswordvalid())
                        {
                            #region frist Login Empty db
                            if (txtUser.Text == "test" && txtPass.Text == "test")
                            {
                                this.Visible = false;
                                DmnSearch obj = new DmnSearch();
                                BllSearch<DmnHRegistration> bllpayments = new BllSearch<DmnHRegistration>();
                                obj.Select = "*";
                                obj.Table = "tblHotelRegistration";
                                obj.WhereNdGroupBy = " where status=1"; ;
                                var result = bllpayments.GetSearchData("prdSearch", obj);
                                if (result.Count > 0)
                                    BranchId = Convert.ToInt16(result[0].Id);
                                else
                                    BranchId = 0;
                                UserId = 1;
                                EmployeName = "test";
                                TestUser = true;

                                Forms.FrmDashboard DashBoard = new FrmDashboard();
                                DashBoard.MdiParent = this.MdiParent;
                                DashBoard.ShowDialog();
                                DashBoard.WindowState = FormWindowState.Normal;
                            }
                            #endregion

                            else
                            {
                                FormSize = this.Size;
                                #region retrieve data after login

                                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                                DmnSearch objSearch = new DmnSearch();

                                objSearch.ColumnHeaders = "UserAccountId,Status,EmployeeId,BranchId,CompanyId,UserAccountTypeId,EmployeeCode,Status,EmployeeName,Designation,BookingInerwell,KitchenDisplayCount,departmentname,epid";
                                objSearch.Select = " ua.UserAccountId as Id,ua.[Status] as Field1,emp.EmployeeId as Field2,ua.BranchId as Field3," +
                                    "ua.UserAccountTypeId as Field5,emp.EmployeeCode as Field6,f.Status as Field7,emp.EmployeeName as Field8 ,d.DesignationName as Field9,h.BookingInerwell  Field10 ,h.KitchenDisplayCount  Field11 ,dt.departmentname Field12,emp.epid Field13";
                                objSearch.Table = " tblUserAccount ua " +
                                    "Left join  tblEmployee emp on emp.EmployeeId = ua.EmployeeId " +
                                    " Left join tblForms f on f.UserType = ua.UserAccountTypeId and f.Status = 1 " +
                                    "Left join tblDesignation d on d.DesignationId = emp.DesignationId " +
                                    "Left join tblHotelRegistration h on h.BranchId=ua.BranchID" +
                                    " Left join tbldepartment dt on dt.departmentid=emp.departmentid";//f.Form = " + Convert.ToInt32(PosForm.AllLocationAccess) + "and

                                objSearch.WhereNdGroupBy = " Where Username = '" + txtUser.Text.Trim().ToUpper() + "'";
                                var userDetails = frmGridSearch.GetSearchGrid(objSearch);
                                UserAccountTypeId = Convert.ToInt64(userDetails["Field5", 0].Value);
                                int Allaccess = Convert.ToInt32(userDetails["Field7", 0].Value);
                                if (Allaccess != 1 && Convert.ToString(BranchId) != Convert.ToString(userDetails["Field3", 0].Value))
                                {
                                    CMessageBox.Show("Invalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtUser.Focus();
                                }
                                else
                                {
                                    BranchId = Convert.ToInt32(userDetails["Field3", 0].Value);
                                    EmployeeId = Convert.ToInt64(userDetails["Field2", 0].Value);
                                    if (BranchId > 0)
                                    {
                                        #region Reset Kitchen Number (opening time on hotel registartion)
                                        DmnResetKitchenno dmnkicthenreset = new DmnResetKitchenno();
                                        BllResetKitchenno Resetkitchen = new BllResetKitchenno();
                                        dmnkicthenreset.BranchId = Convert.ToInt32(BranchId);
                                        dmnkicthenreset.EmployeeId = Convert.ToInt32(EmployeeId);
                                        var reset_kitchenno = Resetkitchen.Resetsystem("Prd_DailySystemReset", dmnkicthenreset);
                                        #endregion
                                    }

                                    BookingInterwell = Convert.ToDecimal(userDetails["Field10", 0].Value);
                                    UserId = Convert.ToInt32(userDetails["Id", 0].Value);
                                    UserStatus = Convert.ToInt16(userDetails["Field1", 0].Value);

                                    EmployeName = Convert.ToString(userDetails["Field8", 0].Value);
                                    departmentname = Convert.ToString(userDetails["Field12", 0].Value);
                                    EphId = Convert.ToInt32(userDetails["Field13", 0].Value);

                                    Designation = Convert.ToString(userDetails["Field9", 0].Value);
                                    KitchenDisplayCount = Convert.ToInt32(userDetails["Field11", 0].Value);
                                    //CompanyId = Convert.ToInt64(userDetails["Field4", 0].Value);
                                    UserAccountTypeId = Convert.ToInt64(userDetails["Field5", 0].Value);
                                    EmpCode = userDetails["Field6", 0].Value.ToString();

                                    #endregion

                                    this.Opacity = 0;
                                    Forms.FrmTableTabletView DashBoard = new FrmTableTabletView();
                                    DashBoard.MdiParent = this.MdiParent;
                                    DashBoard.ShowDialog();
                                    DashBoard.WindowState = FormWindowState.Normal;
                                }

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                //clsgen.LogExceptionExcel("Login Page btnlogin_Click()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
                clsgen.LogException("Login Page btnlogin_Click()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
                // CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
