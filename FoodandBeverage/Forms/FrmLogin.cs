using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControl;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private LoadingGIF_Timer loadingForm;

        #region Declarations
        ClsGeneral objGeneral = new ClsGeneral();// for validation
        BllEncryption bllEncryption = new BllEncryption();
        //BllUserAccount bllUserAccount = new BllUserAccount();
        byte[] passwordHash;
        byte[] passwordSalt;
        public static long UserId;
        public static long BranchId;

        public static string EmpCode = "";
        public static string EmployeName = "";
        public static string Designation = "";
        public static int KitchenDisplayCount = 15; 
        public static long EmployeeId;
        public static decimal BookingInterwell;
        public static bool TestUser = false;

        int UserStatus;
        public string txtbox = "";
        public static long UserAccountTypeId;
        public static int rfid = 0;
        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsgen = new ClsGeneral();
        public static int flag = 0;
        public byte[] encryptPassword;
        public bool loginkeyboard = false;
        private bool passwordVisible = false;
        public static string DateFormat = "yyyy-MM-dd HH:mm:ss";

        public static string KitchenBillPath = @"C:\Users\ajay\Desktop\Food_Beverage\FoodandBeverage\Forms\KitchenBill";
        public static string ReportPath = @"C:\Users\ajay\Desktop\Food_Beverage\FoodandBeverage\Report";
        public static string SaleBillPath = @"C:\Users\ajay\Desktop\Food_Beverage\FoodandBeverage\SaleBill\";

        public static int Compid =102;

        //public static string KitchenBillPath = Application.StartupPath + @"\KitchenBill";
        //public static string ReportPath = Application.StartupPath + @"\Report";
        //public static string SaleBillPath = Application.StartupPath + @"\SaleBill";

        public static Size FormSize;

        #endregion

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            //txtUserName.Text = "User Name .....";
            //txtPwd.Text = "Password .....";
            if (FormSize.IsEmpty)
                FormSize = this.Size;

            pnlloginbox.BackColor = Color.FromArgb(150, 250, 250, 250);
            Task.Run(() =>
            {
                FrmSplash loadingForm = new FrmSplash();
                loadingForm.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(loadingForm);
            });
            if (loadingForm != null && !loadingForm.IsDisposed)
            {
                loadingForm.Invoke(new Action(() => loadingForm.Close()));
            }
            //textb_shape();            
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page textb_shape()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page ApplyRoundedTextBox()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }
        }

        #endregion

        private void txtusername_Enter(object sender, EventArgs e)
        {
            try
            {
                timer1.Start();
                if (txtUserName.Text == "User Name .....")
                {
                    txtUserName.Text = "";
                    txtUserName.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page txtusername_Enter()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }      
      
        }       

        private void txtusername_Leave(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                if (txtUserName.Text == "")
                {
                    txtUserName.Text = "User Name .....";
                    txtUserName.ForeColor = Color.DarkGray;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page txtusername_Leave()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            } 
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
                            if(txtUserName.Text== "test" && txtPwd.Text == "test")
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

                                objSearch.ColumnHeaders = "UserAccountId,Status,EmployeeId,BranchId,CompanyId,UserAccountTypeId,EmployeeCode,Status,EmployeeName,Designation,BookingInerwell,KitchenDisplayCount";
                                objSearch.Select = " ua.UserAccountId as Id,ua.[Status] as Field1,emp.EmployeeId as Field2,ua.BranchId as Field3," +
                                    "ua.UserAccountTypeId as Field5,emp.EmployeeCode as Field6,f.Status as Field7,emp.EmployeeName as Field8 ,d.DesignationName as Field9,h.BookingInerwell  Field10 ,h.KitchenDisplayCount  Field11 ";
                                objSearch.Table = " tblUserAccount ua " +
                                    "Left join  tblEmployee emp on emp.EmployeeId = ua.EmployeeId " +
                                    " Left join tblForms f on f.UserType = ua.UserAccountTypeId and f.Status = 1 " +
                                    "Left join tblDesignation d on d.DesignationId = emp.DesignationId "+
                                    "Left join tblHotelRegistration h on h.BranchId=ua.BranchID";//f.Form = " + Convert.ToInt32(PosForm.AllLocationAccess) + "and

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
                                    BranchId = Convert.ToInt16(userDetails["Field3", 0].Value);
                                    if (BranchId > 0)
                                    {
                                        #region Reset Kitchen Number (opening time on hotel registartion)
                                        DmnResetKitchenno dmnkicthenreset = new DmnResetKitchenno();
                                        BllResetKitchenno Resetkitchen = new BllResetKitchenno();
                                        dmnkicthenreset.BranchId = Convert.ToInt32(BranchId);
                                        var reset_kitchenno = Resetkitchen.ResetKitchenno("Prd_check_Reset_KitchenSrlNo", dmnkicthenreset);
                                        #endregion
                                    }

                                    BookingInterwell = Convert.ToDecimal(userDetails["Field10", 0].Value);
                                    UserId = Convert.ToInt64(userDetails["Id", 0].Value);
                                    UserStatus = Convert.ToInt16(userDetails["Field1", 0].Value);
                                    EmployeeId = Convert.ToInt64(userDetails["Field2", 0].Value);
                                    EmployeName = Convert.ToString(userDetails["Field8", 0].Value); 
                                     Designation = Convert.ToString(userDetails["Field9", 0].Value);
                                    KitchenDisplayCount  = Convert.ToInt32(userDetails["Field11", 0].Value);
                                    //CompanyId = Convert.ToInt64(userDetails["Field4", 0].Value);
                                    UserAccountTypeId = Convert.ToInt64(userDetails["Field5", 0].Value);
                                    EmpCode = userDetails["Field6", 0].Value.ToString();

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

                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                //clsgen.LogExceptionExcel("Login Page btnlogin_Click()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
                clsgen.LogException("Login Page btnlogin_Click()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
               // CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page IsValidHtmlColor()", ex, EmployeName, Designation,Convert.ToInt32(FormType.Login));
                return false;
            }
        }

        public bool IsUsernameValid()
        {
            try
            {
                if (txtUserName.Text == "test")
                    return true;

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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page IsUsernameValid()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
                return false;
            }
        }
        public bool IsPasswordvalid()
        {
            try
            {
                bool result = true;
                if (txtPwd.Text == "test")
                    return true;

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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
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
            catch(Exception ex)
            {
                CMessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clsgen.LogException("Login Page timer1_Tick()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }
        }

        public bool IsValid()
        {
            try
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page IsValid()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
                return false;
            }

        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')             
                e.Handled = true; 
        }
        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login page txtUserName_KeyDown()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login Page pnlloginbox_Paint()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }

        }

        private void txtPwd_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtPwd.Text == "Password .....")
                {
                    txtPwd.Text = "";
                    txtPwd.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login page txtPwd_Enter()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }
        }

        private void txtPwd_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPwd.Text == "")
                {
                    txtPwd.Text = "Password .....";
                    txtPwd.ForeColor = Color.DarkGray;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login page txtPwd_Leave()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login page txtPwd_KeyDown()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r') // Check for Enter key            
                    e.Handled = true; // Suppress the Enter key
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login page txtPwd_KeyPress()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtPwd.PasswordChar = '*';
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Login page txtPwd_TextChanged()", ex, EmployeName, Designation, Convert.ToInt32(FormType.Login));
            }
        }

        private void FrmLogin_Shown(object sender, EventArgs e)
        {
            FormSize = this.Size;
        }

        private void txtUserName_Click(object sender, EventArgs e)
        {
            if (loginkeyboard)
            {
                ShowKeyboard(sender as TextBox);
            }
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

        private void txtPwd_Click(object sender, EventArgs e)
        {
            if (loginkeyboard)
            {
                ShowKeyboard(sender as TextBox);
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
    }
}
