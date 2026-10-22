using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControl;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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

            // Keep the visible modern fields connected to the existing login logic.
            txt_username.Placeholder = "Username";
            txt_username.ShowIcon = true;
            txt_username.IconText = "\uE77B";
            txt_username.CornerRadius = 11;
            txt_username.Size = new Size(286, 48);
txt_password.Placeholder = "Password";
            txt_password.ShowIcon = true;
            txt_password.IconText = "\uE72E";
            txt_password.UsePassword = true;
            txt_password.CornerRadius = 11;
            txt_password.Size = new Size(286, 48);
Resize += delegate { ApplyLoginRoundedCorners(); };
            ApplyLoginRoundedCorners();
        }

        private void ApplyLoginRoundedCorners()
        {
            ApplyRoundedRegion(this, 22);
            ApplyRoundedRegion(pnlloginbox, 24);
        }

        private static void ApplyRoundedRegion(Control control, int radius)
        {
            if (control == null || control.IsDisposed ||
                control.Width < radius * 2 || control.Height < radius * 2)
                return;

            using (GraphicsPath path = new GraphicsPath())
            {
                int diameter = radius * 2;
                path.AddArc(0, 0, diameter, diameter, 180, 90);
                path.AddArc(control.Width - diameter, 0, diameter, diameter, 270, 90);
                path.AddArc(control.Width - diameter, control.Height - diameter,
                    diameter, diameter, 0, 90);
                path.AddArc(0, control.Height - diameter, diameter, diameter, 90, 90);
                path.CloseFigure();

                Region oldRegion = control.Region;
                control.Region = new Region(path);
                if (oldRegion != null)
                    oldRegion.Dispose();
            }
        }
        private LoadingGIF_Timer loadingForm;

        #region Declarations
        ClsGeneral objGeneral = new ClsGeneral();// for validation
        BllEncryption bllEncryption = new BllEncryption();
        //BllUserAccount bllUserAccount = new BllUserAccount();
        byte[] passwordHash;
        byte[] passwordSalt;
        public static int UserId;
        public static int  BranchId;

        public static string EmpCode = "";
        public static string EmployeName = "";
        public static string Designation = "";
        public static string departmentname = "";
        public static int KitchenDisplayCount = 15; 
        public static int EmployeeId;
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
        public static string PostponePath = "";
        public static string SaleBillPath = "";
        public static string XYZPath = "";
        public static string logoPath = "";
        public static string KitchenBillPath = "";
        public static string ReportPath = "";
        public static int Compid = Convert.ToInt32(ConfigurationManager.AppSettings["Corpid"]);

        // public static string KitchenBillPath = Application.StartupPath + @"\KitchenBill";
        //public static string ReportPath = Application.StartupPath + @"\Report";
        // public static string SaleBillPath = Application.StartupPath + @"\SaleBill";

        public static Size FormSize;
        public static decimal AutoLogoutIdleTimeoutMinutes = 0;
        public static int CorpID=102;

        private bool isMouseDown = false;
        private int pressDuration = 0;
        public static int GameBranchId = 0;
        #endregion

        private async void UpdateDatabaseStatusAsync()
        {
            if (pb_online_offline == null)
                return;

            // Default to offline until the configured SQL connection succeeds.
            pb_online_offline.BackgroundImage = Properties.Resources.sql_offline_icon;
            pb_online_offline.BackgroundImageLayout = ImageLayout.Stretch;
            pb_online_offline.AccessibleName = "Database offline";
            pb_online_offline.AccessibleDescription = "SQL Server connection is unavailable";

            try
            {
                bool isOnline = await Task.Run(() =>
                {
                    try
                    {
                        using (System.Data.IDbConnection connection = DAL.DapperRepository<object>.OpenConnection())
                        {
                            return connection.State == ConnectionState.Open;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                });

                if (IsDisposed || pb_online_offline.IsDisposed)
                    return;

                pb_online_offline.BackgroundImage = isOnline
                    ? Properties.Resources.sql_online_icon
                    : Properties.Resources.sql_offline_icon;
                pb_online_offline.AccessibleName = isOnline ? "Database online" : "Database offline";
                pb_online_offline.AccessibleDescription = isOnline
                    ? "SQL Server connection is available"
                    : "SQL Server connection is unavailable";
            }
            catch
            {
                if (!IsDisposed && pb_online_offline != null && !pb_online_offline.IsDisposed)
                {
                    pb_online_offline.BackgroundImage = Properties.Resources.sql_offline_icon;
                    pb_online_offline.AccessibleName = "Database offline";
                    pb_online_offline.AccessibleDescription = "SQL Server connection is unavailable";
                }
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                // Show SQL Server availability without blocking the login screen.
                UpdateDatabaseStatusAsync();

                if (PrintpathValue == 1)
                {
                    //    //KitchenBillPath = @"C:\Users\silpa\Desktop\Food_Beverage\FoodandBeverage\Forms\KitchenBill\";
                    //    //ReportPath = @"C:\Users\silpa\Desktop\Food_Beverage\FoodandBeverage\Report\";
                    //    //SaleBillPath = @"C:\Users\silpa\Desktop\Food_Beverage\FoodandBeverage\SaleBill\";
                    //    //XYZPath = @"C:\Users\silpa\Desktop\Food_Beverage\FoodandBeverage\SaleBill\";
                    //    //logoPath = @"C:\Users\silpa\Desktop\Food_Beverage\FoodandBeverage\logoFile\";

                    //KitchenBillPath = @"C:\Users\ajay\Desktop\Food_Beverage\FoodandBeverage\Forms\KitchenBill\";
                    //ReportPath = @"C:\Users\ajay\Desktop\Food_Beverage\FoodandBeverage\Report\";
                    //SaleBillPath = @"C:\Users\ajay\Desktop\Food_Beverage\FoodandBeverage\SaleBill\";
                    //XYZPath = @"C:\Users\ajay\Desktop\Food_Beverage\FoodandBeverage\SaleBill\";
                    //logoPath = @"C:\Users\ajay\Desktop\Food_Beverage\FoodandBeverage\logoFile\";

                    KitchenBillPath = @"D:\Developers\Ajay\Food_Beverage\FoodandBeverage\Forms\KitchenBill\";
                    ReportPath = @"D:\Developers\Ajay\Food_Beverage\FoodandBeverage\Report\";
                    SaleBillPath = @"D:\Developers\Ajay\Food_Beverage\FoodandBeverage\SaleBill\";
                    XYZPath = @"D:\Developers\Ajay\Food_Beverage\FoodandBeverage\SaleBill\";
                    logoPath = @"D:\Developers\Ajay\Food_Beverage\FoodandBeverage\logoFile\";

                    //    //KitchenBillPath = @"C:\Users\Asus\Desktop\Food_Beverage\FoodandBeverage\Forms\KitchenBill\";
                    //    //ReportPath = @"C:\Users\Asus\Desktop\Food_Beverage\FoodandBeverage\Report";
                    //    //SaleBillPath = @"C:\Users\Asus\Desktop\Food_Beverage\FoodandBeverage\SaleBill";
                    //    //XYZPath = @"C:\Users\Asus\Desktop\Food_Beverage\FoodandBeverage\SaleBill\";
                    //    //logoPath = @"C:\Users\Asus\Desktop\Food_Beverage\FoodandBeverage\logoFile\";
                }
                else
                {
                    KitchenBillPath = Application.StartupPath + @"\KitchenBill\";
                    ReportPath = Application.StartupPath + @"\Report\";
                    SaleBillPath = Application.StartupPath + @"\SaleBill\";
                    XYZPath = Application.StartupPath + @"\SaleBill\";
                    logoPath = Application.StartupPath + @"\logoFile\";
                }

                if (FormSize.IsEmpty)
                    FormSize = this.Size;

                pnlloginbox.BackColor = Color.Transparent;
                ApplyLoginRoundedCorners();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Login page FrmLogin_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Login));
            }
        }

        #region TexBox Round function
        public void textb_shape()
        {
            txt_username.CornerRadius = 11;
            txt_password.CornerRadius = 11;
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page ApplyRoundedTextBox()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }
        }

        #endregion

        public void virtualkeyboardCheck()
        {
            try
            {
                BllSearch<DmnVirtualKeyBoard> Bllvirtualkeyboard = new BllSearch<DmnVirtualKeyBoard>();
                DmnSearch dmnsearchvir = new DmnSearch
                {
                    Select = @"
                AllowVirtualKeyboardLoginNumber AS Enb_AllowVirtualKeyboardLogin_Number,
                AllowVirtualKeyboardLoginKeyboard AS Enb_AllowVirtualKeyboardLogin_Normal,
                Id",
                    Table = "tblVirtualKeyBoard",
                    WhereNdGroupBy = $"WHERE mDeviceIP='{System.Environment.MachineName}' AND BranchId={BranchId} AND Status=1",
                    OrderBy = ""
                };

                var gridesource = Bllvirtualkeyboard.GetSearchData("prdSearch", dmnsearchvir);
                if (gridesource.Count > 0)
                {
                    var data = gridesource[0];
                    //Enb_AllowVirtualKeyboardLogin_Number = data.Enb_AllowVirtualKeyboardLogin_Number;
                    loginkeyboard = data.Enb_AllowVirtualKeyboardLogin_Normal;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Login page virtualkeyboardCheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Login));
            }
        }
        private void txt_username_Enter(object sender, EventArgs e)
        {
            // Keep the existing virtual-keyboard timer behavior.
            timer1.Start();
        }

        private void txt_username_Leave(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
                            #region frist Login Empty db
                            if(txt_username.TextValue== "test" && txt_password.TextValue == "test")
                            {
                                this.Visible = false;
                                DmnSearch obj = new DmnSearch();
                                BllSearch<DmnHRegistration> bllpayments = new BllSearch<DmnHRegistration>();
                                obj.Select = "*";
                                obj.Table = "tblHotelRegistration";
                                obj.WhereNdGroupBy = " where status=1";;
                                var result = bllpayments.GetSearchData("prdSearch", obj);
                                if (result.Count > 0)                                
                                    BranchId = Convert.ToInt16(result[0].Id);
                                else
                                    BranchId =0;
                                UserId =1;
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

                                objSearch.ColumnHeaders = "UserAccountId,Status,EmployeeId,BranchId,CompanyId,UserAccountTypeId,EmployeeCode,Status,EmployeeName,Designation,BookingInerwell,KitchenDisplayCount,departmentname,epid,gamebranchid";
                                objSearch.Select = " ua.UserAccountId as Id,ua.[Status] as Field1,emp.EmployeeId as Field2,ua.BranchId as Field3," +
                                    "ua.UserAccountTypeId as Field5,emp.EmployeeCode as Field6,f.Status as Field7,emp.EmployeeName as Field8 ,d.DesignationName as Field9,h.BookingInerwell  Field10 ,h.KitchenDisplayCount  Field11 ,dt.departmentname Field12,emp.epid Field13,h.GameBranchId Field14 ";
                                objSearch.Table = " tblUserAccount ua " +
                                    "Left join  tblEmployee emp on emp.EmployeeId = ua.EmployeeId " +
                                    " Left join tblForms f on f.UserType = ua.UserAccountTypeId and f.Status = 1 " +
                                    "Left join tblDesignation d on d.DesignationId = emp.DesignationId "+
                                    "Left join tblHotelRegistration h on h.BranchId=ua.BranchID"+
                                    " Left join tbldepartment dt on dt.departmentid=emp.departmentid";//f.Form = " + Convert.ToInt32(PosForm.AllLocationAccess) + "and

                                objSearch.WhereNdGroupBy = " Where Username = '" + txt_username.TextValue.Trim().ToUpper() + "'";
                                var userDetails = frmGridSearch.GetSearchGrid(objSearch);
                                UserAccountTypeId = Convert.ToInt64(userDetails["Field5", 0].Value);
                                int Allaccess = Convert.ToInt32(userDetails["Field7", 0].Value);
                                if (Allaccess != 1 && Convert.ToString(BranchId) != Convert.ToString(userDetails["Field3", 0].Value))
                                {
                                    CMessageBox.Show("Invalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txt_username.FocusInput();
                                }
                                else
                                {
                                    EmployeeId = Convert.ToInt32(userDetails["Field2", 0].Value);
                                    BranchId = Convert.ToInt32(userDetails["Field3", 0].Value);
                                    if (BranchId > 0)
                                    {
                                        #region Reset Kitchen Number (opening time on hotel registartion)
                                        DmnResetKitchenno dmnkicthenreset = new DmnResetKitchenno();
                                        BllResetKitchenno Resetkitchen = new BllResetKitchenno();
                                        dmnkicthenreset.BranchId = Convert.ToInt32(BranchId);
                                        dmnkicthenreset.EmployeeId = Convert.ToInt32(EmployeeId); ;
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
                                    KitchenDisplayCount  = Convert.ToInt32(userDetails["Field11", 0].Value);
                                    //CompanyId = Convert.ToInt64(userDetails["Field4", 0].Value);
                                    UserAccountTypeId = Convert.ToInt64(userDetails["Field5", 0].Value);
                                    EmpCode = userDetails["Field6", 0].Value.ToString();

                                    GameBranchId= Convert.ToInt32(userDetails["Field14", 0].Value);
                                    #endregion

                                    this.Opacity = 0;
                                    Forms.FrmDashboard DashBoard = new FrmDashboard();
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
                clsgen.LogException("Login Page btnlogin_Click()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }
       
        }
        private bool IsValidHtmlColor(string color)
        {
            try
            {
                ColorTranslator.FromHtml(color);
                return true;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page IsValidHtmlColor()", ex, EmployeName, Designation,Convert.ToInt32(FormType.Login));
                return false;
            }
        }

        public bool IsUsernameValid()
        {
            try
            {
                if (txt_username.TextValue == "test")
                    return true;

                BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "PasswordHash,PasswordSalt ";
                objSearch.Table = " tblUserAccount ";
                objSearch.WhereNdGroupBy = " Where Username = '" + txt_username.TextValue.Trim().ToUpper() + "'AND Status=1 ";
                var pwdDetails = bllSearch.GetSearchData("prdSearch", objSearch);
                if (pwdDetails.Count > 0)
                {
                    //lblUser.Text = txt_username.TextValue.Trim().ToUpper();
                    passwordSalt = pwdDetails[0].PasswordSalt;
                    passwordHash = pwdDetails[0].PasswordHash;
                    encryptPassword = pwdDetails[0].PasswordHash;
                    return true;
                }
                else
                {

                    txt_username.FocusInput();
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
                if (txt_password.TextValue == "test")
                    return true;

                if (passwordSalt == null || passwordHash == null)
                {
                    return false;
                }
                if (rfid == 0)
                    result = bllEncryption.verifyPassword(txt_password.TextValue.Trim(), passwordSalt, passwordHash);
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
                txt_password.FocusInput();
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul(Compid);
                if (str1 != "Card not in contact")
                {
                    timer1.Stop();
                    if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")
                        str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi(Compid);
                    str1 = RemoveSpecialCharacters(str1.Trim());
                    if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Cardnotincontact")
                    {
                        CMessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    {
                        ToggleWifiPopup(0);
                        Employeefetch(str1);
                    }
                }
            }
            catch(Exception ex)
            {
                //CMessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clsgen.LogException("Login Page timer1_Tick()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }
        }
        public void Employeefetch(string cardno)
        {
            try
            {
                BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "tblEmployee.ICardNo,tblEmployee.BranchId, tblUserAccount.UserAccountId, tblUserAccount.Username, tblUserAccount.PasswordHash, tblUserAccount.PasswordSalt,tblEmployee.RFID_Login_Status";
                objSearch.Table = " tblEmployee INNER JOIN tblUserAccount ON tblEmployee.EmployeeId = tblUserAccount.EmployeeId";
                objSearch.WhereNdGroupBy = "where tblEmployee.ICardNo ='" + cardno + "' And tblEmployee.Status=1";
                objSearch.OrderBy = "Order By UserAccountId ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                //mcmbUserAccountType.SelectedValue = gridSource[0].UserAccountTypeId;
                if (gridSource.Count > 0)
                {
                    if (gridSource[0].RFID_Login_Status == 1)
                    {
                        timer1.Stop();
                        txt_username.TextValue = Convert.ToString(gridSource[0].Username);
                        txt_password.TextValue = Convert.ToString(gridSource[0].PasswordHash);
                        encryptPassword = gridSource[0].PasswordHash;
                        var data = new List<byte>();
                        rfid = 1;
                    }
                    btnlogin.PerformClick(); 
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clsgen.LogException("Login Page Employefetch()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }
        }
        public bool IsValid()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_username.TextValue))
                {
                    CMessageBox.Show("Please enter username.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_username.FocusInput();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txt_password.TextValue))
                {
                    CMessageBox.Show("Please enter password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_password.FocusInput();
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

        private void txt_username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')             
                e.Handled = true; 
        }
        private void txt_username_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txt_username.TextValue))
                    {
                        CMessageBox.Show("Please Enter UserName", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_username.FocusInput();
                        return;
                    }
                    else
                        txt_password.FocusInput();
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login page txt_username_KeyDown()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }
        }


        private void pnlloginbox_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                float borderRadius = 10f;
                GraphicsPath path = new GraphicsPath();
                RectangleF rect = new RectangleF(pnlloginbox.ClientRectangle.X, pnlloginbox.ClientRectangle.Y, pnlloginbox.ClientRectangle.Width, pnlloginbox.ClientRectangle.Height);
                path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
                path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
                path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
                path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
                path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
                path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
                path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
                path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
                path.CloseFigure();
                pnlloginbox.Region = new Region(path);
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page pnlloginbox_Paint()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }

        }

        private void txt_password_Enter(object sender, EventArgs e)
        {
            // Placeholder text is handled by ModernTextBox.
        }

        private void txt_password_Leave(object sender, EventArgs e)
        {
            // Placeholder text is handled by ModernTextBox.
        }

        private void txt_password_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txt_password.TextValue))
                    {
                        CMessageBox.Show("Please Enter Password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_password.FocusInput();
                        return;
                    }
                    else
                        btnlogin.Focus();
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login page txt_password_KeyDown()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }
        }

        private void txt_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r') // Check for Enter key            
                    e.Handled = true; // Suppress the Enter key
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login page txt_password_KeyPress()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }
        }

        private void FrmLogin_Shown(object sender, EventArgs e)
        {
            FormSize = this.Size;
        }

        private void txt_username_Click(object sender, EventArgs e)
        {
            if (loginkeyboard)
            {
                ShowKeyboard(FindInputTextBox(txt_username));
            }
        }

        #region Keyboard

        // Find the real WinForms TextBox inside ModernTextBox without depending on an
        // InputTextBox property being present in an older locally referenced FBControl assembly.
        private static TextBox FindInputTextBox(Control parent)
        {
            if (parent == null)
                return null;

            TextBox directTextBox = parent as TextBox;
            if (directTextBox != null)
                return directTextBox;

            foreach (Control child in parent.Controls)
            {
                TextBox nestedTextBox = FindInputTextBox(child);
                if (nestedTextBox != null)
                    return nestedTextBox;
            }

            return null;
        }

        public void ShowKeyboard(TextBox activeTextBox)
        {
            if (activeTextBox == null) return;

            Point textBoxScreenLocation = activeTextBox.PointToScreen(Point.Empty);
            int desiredX = 0, desiredY = 0;
            if (Frmkeyboard.IsOpen)
            {
                #region Calculate Position of Keyboard 
                desiredX = textBoxScreenLocation.X + (activeTextBox.Width / 2) - (Frmkeyboard.Instance.Width / 2);
                desiredY = textBoxScreenLocation.Y + activeTextBox.Height + 5;
                Rectangle screenBounds = Screen.FromControl(activeTextBox).WorkingArea;
                if (desiredX < screenBounds.Left)
                    desiredX = screenBounds.Left;
                else if (desiredX + Frmkeyboard.Instance.Width > screenBounds.Right)
                    desiredX = screenBounds.Right - Frmkeyboard.Instance.Width;
                if (desiredY + Frmkeyboard.Instance.Height > screenBounds.Bottom)
                {
                    desiredY = textBoxScreenLocation.Y - Frmkeyboard.Instance.Height - 5;
                    if (desiredY < screenBounds.Top)
                        desiredY = screenBounds.Top;
                }
                #endregion
            }

            if (Frmkeyboard.IsOpen)
            {
                Frmkeyboard.Instance.StartPosition = FormStartPosition.Manual;
                Frmkeyboard.Instance.Location = new Point(desiredX, desiredY);
                Frmkeyboard.Instance.BringToFront();
            }
            else
            {
                Frmkeyboard frmKey = new Frmkeyboard(0);

                #region Calculate Position of Keyboard 
                desiredX = textBoxScreenLocation.X + (activeTextBox.Width / 2) - (Frmkeyboard.Instance.Width / 2);
                desiredY = textBoxScreenLocation.Y + activeTextBox.Height + 5;
                Rectangle screenBounds = Screen.FromControl(activeTextBox).WorkingArea;
                if (desiredX < screenBounds.Left)
                    desiredX = screenBounds.Left;
                else if (desiredX + Frmkeyboard.Instance.Width > screenBounds.Right)
                    desiredX = screenBounds.Right - Frmkeyboard.Instance.Width;
                if (desiredY + Frmkeyboard.Instance.Height > screenBounds.Bottom)
                {
                    desiredY = textBoxScreenLocation.Y - Frmkeyboard.Instance.Height - 5;
                    if (desiredY < screenBounds.Top)
                        desiredY = screenBounds.Top;
                }
                Frmkeyboard.Instance.StartPosition = FormStartPosition.Manual;
                Frmkeyboard.Instance.Location = new Point(desiredX, desiredY);
                #endregion


                frmKey.OnKeyPressed += (keyText) =>
                {
                    if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.backspace))
                    {
                        if (activeTextBox.Text.Length > 0)
                            activeTextBox.Text = activeTextBox.Text.Substring(0, activeTextBox.Text.Length - 1);
                    }
                    else if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.space))
                    {
                        activeTextBox.Text += " ";
                    }
                    else if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.clear))
                    {
                        activeTextBox.Text = "";
                    }
                    else
                    {
                        activeTextBox.Text += keyText;
                    }
                    activeTextBox.SelectionStart = activeTextBox.Text.Length;
                    activeTextBox.SelectionLength = 0;
                };

                frmKey.Show();
            }

        }
        #endregion

        private void txt_password_Click(object sender, EventArgs e)
        {
            if (loginkeyboard)
            {
                ShowKeyboard(FindInputTextBox(txt_password));
            }
        }

        private void ToggleWifiPopup(int inp)
        {
            if (inp == 1)
            {
                Pnl_wifiPopup.Left = (this.ClientSize.Width - Pnl_wifiPopup.Width) / 2;
                Pnl_wifiPopup.Top = (this.ClientSize.Height - Pnl_wifiPopup.Height) / 2;

                Pnl_wifiPopup.Visible = true;
            }
            else
            {
                Pnl_wifiPopup.Visible = false;
            }
        }

        private void btnrfid_Click_1(object sender, EventArgs e)
        {
            ToggleWifiPopup(1);
            if (!timer1.Enabled)
                timer1.Start();
        }

        private void btnwificancel_Click(object sender, EventArgs e)
        {
            Pnl_wifiPopup.Visible = false;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timer1?.Dispose();
            base.OnFormClosed(e);
        }

        private void Pnl_wifiPopup_Paint(object sender, PaintEventArgs e)
        {
            objGeneral.ApplyRoundedCorners(Pnl_wifiPopup, 10f);
        }

        private void longPressTimer_Tick(object sender, EventArgs e)
        {
            if (isMouseDown)
            {
                pressDuration += 100; 
                if (pressDuration >= 3000) // 3 seconds long press
                {
                    longPressTimer.Stop();
                    isMouseDown = false;
                    int x = (this.ClientSize.Width - pnlDBLogin.Width) / 2;
                    int y = (this.ClientSize.Height - pnlDBLogin.Height) / 2;

                    pnlDBLogin.Location = new Point(x, y);
                    pnlDBLogin.BringToFront();
                    pnlDBLogin.Visible = true;
                    txtusernamedb.Focus();
                    txtpwddb.Text = "";
                    txtusernamedb.Text = "";
                }
            }
        }

        private void btndbconfiglogin_Click(object sender, EventArgs e)
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DBConfig", "dbsettings.txt");
            string username = txtusernamedb.Text.Trim();
            string password = txtpwddb.Text.Trim();

            string validUser = ConfigurationManager.AppSettings["ValidUser"];
            string validPass = ConfigurationManager.AppSettings["ValidPass"];

            if (username == validUser && password == validPass)
            {
                pnlDBLogin.Visible = false;
                CMessageBox.Show("Login successful!", "Authentication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (File.Exists(configPath))
                {
                    string savedConnection = File.ReadAllText(configPath).Trim();
                    txtext_conn.Text = savedConnection;
                }
                pnlDbSettings.Visible = true;
                int x = (this.ClientSize.Width - pnlDbSettings.Width) / 2;
                int y = (this.ClientSize.Height - pnlDbSettings.Height) / 2;

                pnlDbSettings.Location = new Point(x, y);
                pnlDbSettings.BringToFront();
            }
            else
            {
                CMessageBox.Show("Invalid username or password.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsavedbconfig_Click(object sender, EventArgs e)
        {
            try
            {
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DBConfig", "dbsettings.txt");

                // Get connection string from textbox
                string newConnection = txtnew_conn.Text.Trim();

                if (string.IsNullOrWhiteSpace(newConnection))
                {
                    CMessageBox.Show("Please enter a connection string before saving.",
                                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Save to file
                File.WriteAllText(configPath, newConnection);

                CMessageBox.Show("✅ New database connection has been saved successfully!",
                                "DB Config Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (File.Exists(configPath))
                {
                    string savedConnection = File.ReadAllText(configPath).Trim();
                    txtext_conn.Text = savedConnection;
                    txtnew_conn.Text = "";
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show("❌ Failed to save database connection!\n\nError: " + ex.Message,"DB Config Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btntstconn_Click(object sender, EventArgs e)
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DBConfig", "dbsettings.txt");

            if (!File.Exists(configPath))
            {
                CMessageBox.Show("Database settings file not found:\n" + configPath,
                                "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = File.ReadAllText(configPath).Trim();

            try
            {
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        CMessageBox.Show("✅ Database connection successful!",
                                        "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        CMessageBox.Show("⚠ Database connection could not be established.",
                                        "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Database connection failed!\n\nError: " + ex.Message,"Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }      
        private void pblogo_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            pressDuration = 0;
            longPressTimer.Start();
        }

        private void pblogo_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            longPressTimer.Stop();
        }

        private void btnrfid_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            pressDuration = 0;
            longpress_midascard.Start();
        }

        private void btnrfid_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            longpress_midascard.Stop();
        }

        private void longpress_midascard_Tick(object sender, EventArgs e)
        {

            if (isMouseDown)
            {
                pressDuration += 100; // increase time by 100ms

                if (pressDuration >= 5000) // 5 seconds long press
                {
                    longpress_midascard.Stop();
                    isMouseDown = false;
                    ToggleWifiPopup(0);
                    using (FoodandBeverage.Forms.UserControl.Frmkeypad frm = new FoodandBeverage.Forms.UserControl.Frmkeypad())
                    {
                        frm.ShowDialog();
                        string IcardNo = frm.ICardNo;
                        if (IcardNo != "" && IcardNo != null)
                            Employeefetch(IcardNo);
                    }
                }
            }
        }

        private void btndblogin_close_Click(object sender, EventArgs e)
        {
            pnlDBLogin.Visible = false;
        }

        private void btn_dbconf_close_Click(object sender, EventArgs e)
        {
            pnlDbSettings.Visible = false;
        }

        private void pb_online_offline_Click(object sender, EventArgs e)
        {

        }
    }
}
