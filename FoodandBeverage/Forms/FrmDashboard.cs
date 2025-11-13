using BLL;
using CustomMessageBox;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FoodandBeverage.Interfaces;
using FoodandBeverage.Forms.UserAccount;
using FoodandBeverage.Forms.UserControls;
using System.Threading.Tasks;
using System.Threading;
using FoodandBeverage.Classes;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace FoodandBeverage.Forms
{
    public partial class FrmDashboard : Form
    {

        #region Delcaration
        public static int BranchId = 0;
        public static int UserId = 0;
        public static int UserType = 0;
        public static int Allaccess = 0;

        public static string KitchenBillPath = "";
        public static string ReportPath = "";
        public static string SaleBillPath = "";
        public int Emp_RowCount = 0, Emp_ColCount = 0;
        public int OP_RowCount = 0, OP_ColCount = 0;
        public int ButtonID = 0;
        /*permission checking for manager and supervisor*/
        public static int DrawerId;
        public int ExitDrawerID = 0;
        public decimal Drawerloan = 0;
        public decimal Drawerdrops = 0;

        public string Drawerassginstaffname;
        public string Drawerassginstaffdes;
        public int Drawerassginid;
        public int Drawerassginstaffid;

        public string Drawersystemname;
        public FrmDashboard()
        {
            InitializeComponent();
            Instance = this;
            UserId = Convert.ToInt32(FrmLogin.UserId);
            BranchId = Convert.ToInt32(FrmLogin.BranchId);
            UserType = Convert.ToInt32(FrmLogin.UserAccountTypeId);

            KitchenBillPath = Convert.ToString(FrmLogin.KitchenBillPath);
            SaleBillPath = Convert.ToString(FrmLogin.SaleBillPath);
            ReportPath = Convert.ToString(FrmLogin.ReportPath);
        }
        #region grid Declarationassign

        public int desiredWidth = 0;
        public int desiredHeight = 0;
        public int HdesiredWidth = 0;
        public int HdesiredHeight = 0;
        #endregion

        Color menubuttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        Color opbuttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        TableLayoutPanel tlp_searchproduct_dashboard = new TableLayoutPanel();
        FlowLayoutPanel flpcolor = new FlowLayoutPanel();
        private Point MouseLocation;
        Color product_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        public bool collapsemenu_click = false;

        // Set the size based on your conditions
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;

        public double AddMinutes = Convert.ToDouble(FrmLogin.BookingInterwell);
        public List<DmnNameList> CheckedtableNames = new List<DmnNameList>();
        public bool menutable_click = false;
        private LoadingGIF_Timer loadingForm;
        public DateTime startTime = DateTime.Now;
        ClsGeneral clsgen = new ClsGeneral();

        /*permission checking for manager and supervisor*/
        public static bool AllowCashDrawer = false;
        public static bool AllowDrawerpermissionFullControl = false;


        bool flagDrawerF = false, flagDrawer = false;
        public static FrmDashboard Instance { get; private set; }
        public static bool Enablenumberkeypad = false;
        public static string logoutformsno = "";
        public static bool Logout_zout_status = false;
        public bool isUpdatingAll = false;
        Dictionary<int, int> logoutSettings = new Dictionary<int, int>();

        #region Keyboard & Number Enable/Disable
        // === Number Keyboard Access ===
        public static bool Enb_Home_Number { get; set; } = false;
        public static bool Enb_AllowVirtualKeyboardLogin_Number { get; set; } = false;
        public static bool Enb_TablePickUp_Number { get; set; } = false;
        public static bool Enb_Operation_Number { get; set; } = false;
        public static bool Enb_Employee_Number { get; set; } = false;
        public static bool Enb_Billing_Number { get; set; } = false;
        public static bool Enb_Report_Number { get; set; } = false;
        public static bool Enb_OrderPage_Number { get; set; } = false;
        public static bool Enb_KitchenView_Number { get; set; } = false;
        public static bool Enb_SearchPage_Number { get; set; } = false;
        public static bool Enb_SaleBill_YZoutPage_Number { get; set; } = false;

        // === Normal Keyboard Access ===
        public static bool Enb_Home_Normal { get; set; } = false;
        public static bool Enb_AllowVirtualKeyboardLogin_Normal { get; set; } = false;
        public static bool Enb_TablePickUp_Normal { get; set; } = false;
        public static bool Enb_Operation_Normal { get; set; } = false;
        public static bool Enb_Employee_Normal { get; set; } = false;
        public static bool Enb_Billing_Normal { get; set; } = false;
        public static bool Enb_Report_Normal { get; set; } = false;
        public static bool Enb_OrderPage_Normal { get; set; } = false;
        public static bool Enb_KitchenView_Normal { get; set; } = false;
        public static bool Enb_SearchPage_Normal { get; set; } = false;
        public static bool Enb_SaleBill_YZoutPage_Normal { get; set; } = false;
        #endregion

        public string Compressedimg = Application.StartupPath + "\\CompressedImg";
        string logoname = "";
        public static bool PrintDalvalue = false, XYDalvalue = false, DalDalvalue = false;
        public static bool AutoLogoutWhenIdle = false;
        public static bool UserSavePrompt = false;
        private bool isMenuCollapsed = false;
        private int originalMenuWidth;

        #endregion

        // Function to create a rounded rectangle region
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private void Menulogout_Click(object sender, EventArgs e)
        {
            try
            {
                this.Opacity = 0;
                this.Close();

                if (System.Windows.Forms.Application.OpenForms["FrmLogin"] != null)
                {
                    FrmLogin _instanceFrmLogin = Application.OpenForms["FrmLogin"] as FrmLogin;
                    _instanceFrmLogin.Show();
                    _instanceFrmLogin.BringToFront();
                    _instanceFrmLogin.Visible = true;
                    _instanceFrmLogin.Opacity=100;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("homepage Page Menulogout_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }


        #region please wait loading
        private void ShowLoadingdGif()
        {
            this.Opacity = 0;
            LoadingGIF_Timer gifForm = new LoadingGIF_Timer();
            gifForm.StartPosition = FormStartPosition.CenterScreen;
            gifForm.ShowDialog();
        }
        #endregion

        private async void FrmDashboard_Load(object sender, EventArgs e)
        {
            originalMenuWidth = flp_sidemenu.Width;
            //lbluser_designation.Text = Convert.ToString(FrmLogin.EmployeName) + "\n" + Convert.ToString(FrmLogin.Designation);
            lbl_empname.Text = Convert.ToString(FrmLogin.EmployeName) + "  " + Convert.ToString(FrmLogin.Designation)+"    ";
            AdjustButtonsToPanel();
            Task.Run(() =>
            {
                loadingForm = new LoadingGIF_Timer();
                loadingForm.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(loadingForm);
            });
            LoadMainForm();
            await Task.Delay(1000);
            if (loadingForm != null && !loadingForm.IsDisposed)
            {
                loadingForm.Invoke(new Action(() => loadingForm.Close()));
            }
        }
        private void LoadMainForm()
        {
            try
            {
                // panelDesktop.Controls.Add(flp_operationbtn);
                // flp_operationbtn.Dock = DockStyle.Fill;
                // flp_operationbtn.Visible = true;


                int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = Screen.PrimaryScreen.Bounds.Height;

                if (screenWidth > 1367 && screenHeight > 769)
                {
                    desiredWidth = 52;
                    desiredHeight = 52;

                    HdesiredWidth = 60;
                    HdesiredHeight = 60;
                }
                else
                {
                    desiredWidth = 32;
                    desiredHeight = 32;

                    HdesiredWidth = 40;
                    HdesiredHeight = 40;
                }

                //btn_minimium.BackColor = menubuttoncolor;

                // pbappcoloepicker.BackColor = Color.FromArgb(34, 46, 80);
                Roundform(30);
                // tlp_main.BackColor = Color.FromArgb(152, 193, 217);
                // set color on solidebar header
                //pnl_header.BackColor = menubuttoncolor;
                pbopbuttoncoloepicker.BackColor = menubuttoncolor;
                pbsidebuttoncoloepicker.BackColor = menubuttoncolor;


                this.FormBorderStyle = FormBorderStyle.None;

                //Menuhome.BackColor = menubuttoncolor;
                //Menuoperation.BackColor = menubuttoncolor;
                //Menubilling.BackColor = menubuttoncolor;
                //Menureport.BackColor = menubuttoncolor;
                //Menulogout.BackColor = menubuttoncolor;
                //Menusetting.BackColor = menubuttoncolor;
                //menuEmployee.BackColor = menubuttoncolor;
                //Menutable.BackColor = menubuttoncolor;

                foreach (Button btn in flp_sidemenu.Controls.OfType<Button>())
                {
                    UIHelper.StyleMenuButton(btn);
                    UIHelper.AutoAdjustButtonFont(btn); 
                }
                combofill();
                button_imageset();

                // operation button color 
                //ib_Category.BackColor = opbuttoncolor;
                //ib_Item.BackColor = opbuttoncolor;
                //ib_ingrediant.BackColor = opbuttoncolor;
                //ib_menu.BackColor = opbuttoncolor;
                //ib_unit.BackColor = opbuttoncolor;
                //ib_table.BackColor = opbuttoncolor;
                //ib_purchase.BackColor = opbuttoncolor;
                //ib_purchasereturn.BackColor = opbuttoncolor;
                //ib_tax.BackColor = opbuttoncolor;
                //ib_vendor.BackColor = opbuttoncolor;
                //ib_Discount_offer.BackColor = opbuttoncolor;
                //ib_Expence.BackColor = opbuttoncolor;
                //ib_orderview.BackColor = opbuttoncolor;
                //ib_kitchenview.BackColor = opbuttoncolor;
                //ib_PromoCode.BackColor = opbuttoncolor;

                //ib_Company.BackColor = opbuttoncolor;
                //ib_useraccount.BackColor = opbuttoncolor;
                //ib_designation.BackColor = opbuttoncolor;
                //ib_department.BackColor = opbuttoncolor;
                //ib_employee.BackColor = opbuttoncolor;
                //ib_reservation.BackColor = opbuttoncolor;
                //ib_userpermission.BackColor = opbuttoncolor;
                //ib_addon.BackColor = opbuttoncolor;

                RunMethodInSeparateThread(AppColorSetting);
                GeneralsettingFetching();
                // flpEmployee.Visible = false;
                // ib_employee.Visible = false;
                ib_designation.Visible = false;
                ib_department.Visible = false;
                ib_useraccount.Visible = false;
                ib_userpermission.Visible = false;
                ib_Discount_offer.Visible = false;
                ib_Expence.Visible = false;
                ib_PromoCode.Visible = false;
                FormPermissionChecking();
                panelDesktop.Visible = false;
                Loadchart();
                panelDesktop.Visible = true;

                if (FrmLogin.TestUser)
                {
                    Emp_RowCount = 0; Emp_ColCount = 0;
                    OP_RowCount = 0; OP_ColCount = 0;
                    Menuoperation.Visible = true;
                    ib_Company.Visible = true;
                    ib_employee.Visible = true;
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("homepage Page LoadMainForm()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        public void Loadchart()
        {
            try
            {
                #region Load Chart Form with thread
                if (ChkWritePermission(Convert.ToInt32(formsenum.DashboardCharts)))
                {
                    if (System.Windows.Forms.Application.OpenForms["FrmDashboardReport"] != null)
                    {
                        FrmDashboardReport _instanceDashboardReport = Application.OpenForms["FrmDashboardReport"] as FrmDashboardReport;
                        if (_instanceDashboardReport.WindowState == FormWindowState.Minimized)
                        {
                            _instanceDashboardReport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        }
                        else
                        {
                            _instanceDashboardReport.Dock = DockStyle.Fill;
                            ResizeForm(_instanceDashboardReport);
                            panelDesktop.Controls.Add(_instanceDashboardReport);
                            _instanceDashboardReport.Filldashboard();
                            _instanceDashboardReport.Show();
                            _instanceDashboardReport.BringToFront();

                        }
                    }
                    else
                    {
                        FrmDashboardReport _instanceDashboardReport = new FrmDashboardReport();
                        _instanceDashboardReport.Dock = DockStyle.Fill;
                        ResizeForm(_instanceDashboardReport);
                        _instanceDashboardReport.TopLevel = false;
                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() =>
                            {
                                panelDesktop.Controls.Add(_instanceDashboardReport);
                                _instanceDashboardReport.Show();
                            }));
                        }
                        // _instanceDashboardReport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        _instanceDashboardReport.BringToFront();

                    }                
                }
                this.Opacity = 100;
                #endregion
            }
            catch (Exception ex)
                           
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("homepage Page Loadchart()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
       
        private static void RunMethodInSeparateThread(Action action)
        {
            var thread = new Thread(new ThreadStart(action));
            thread.Start();
        }

        #region Color Setting
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
                clsgen.LogException("homepage IsValidHtmlColor()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
                return false;
            }
        }

        public void AppColorSetting()
        {
            try
            {
                BllSearch<DmnSetting> bllSearch = new BllSearch<DmnSetting>();
                DmnSearch dmnSearch = new DmnSearch
                {
                    Select = "*",
                    Table = "tblsettings",
                    WhereNdGroupBy = " where Status=1 ",
                    OrderBy = ""
                };
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            if ((gridSource[0].APPColor == null || gridSource[0].APPColor == "") && 
                            (gridSource[0].OperationButtonColor == null || gridSource[0].OperationButtonColor == "") && 
                            (gridSource[0].MenuButtonColor == null || gridSource[0].MenuButtonColor == ""))
                            {
                                opbuttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
                                pbopbuttoncoloepicker.BackColor = opbuttoncolor;
                                ib_Category.BackColor = opbuttoncolor;

                                //ib_Item.BackColor = opbuttoncolor;
                                //ib_ingrediant.BackColor = opbuttoncolor;
                                //ib_menu.BackColor = opbuttoncolor;
                                //ib_unit.BackColor = opbuttoncolor;
                                //ib_table.BackColor = opbuttoncolor;
                                //ib_purchase.BackColor = opbuttoncolor;
                                //ib_purchasereturn.BackColor = opbuttoncolor;
                                //ib_tax.BackColor = opbuttoncolor;
                                //ib_vendor.BackColor = opbuttoncolor;
                                //ib_Discount_offer.BackColor = opbuttoncolor;
                                //ib_Expence.BackColor = opbuttoncolor;
                                //ib_orderview.BackColor = opbuttoncolor;
                                //ib_kitchenview.BackColor = opbuttoncolor;
                                //ib_PromoCode.BackColor = opbuttoncolor;
                                //ib_Company.BackColor = opbuttoncolor;
                                //ib_useraccount.BackColor = opbuttoncolor;
                                //ib_designation.BackColor = opbuttoncolor;
                                //ib_department.BackColor = opbuttoncolor;
                                //ib_employee.BackColor = opbuttoncolor;
                                //ib_reservation.BackColor = opbuttoncolor;
                                //ib_userpermission.BackColor = opbuttoncolor;
                                //ib_addon.BackColor = opbuttoncolor;

                                menubuttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
                                pbsidebuttoncoloepicker.BackColor = menubuttoncolor;

                                //Menuhome.BackColor = menubuttoncolor;
                                //Menuoperation.BackColor = menubuttoncolor;
                                //Menubilling.BackColor = menubuttoncolor;
                                //Menureport.BackColor = menubuttoncolor;
                                //Menulogout.BackColor = menubuttoncolor;
                                //Menusetting.BackColor = menubuttoncolor;
                                //menuEmployee.BackColor = menubuttoncolor;
                                //Menutable.BackColor = menubuttoncolor;

                                pbappcoloepicker.BackColor = Color.FromArgb(34, 46, 80);

                                Button btnsetting = this.Controls.Find("Appcolor", true).FirstOrDefault() as Button;
                                if (btnsetting != null)
                                    btnsetting.BackColor = opbuttoncolor;
                            }
                            else
                            {
                                Color colorapp, coloropera, colormenu;
                                if (IsValidHtmlColor(gridSource[0].APPColor))
                                    colorapp = ColorTranslator.FromHtml(gridSource[0].APPColor);
                                else
                                    colorapp = Color.FromArgb(34, 46, 80);

                                if (IsValidHtmlColor(gridSource[0].OperationButtonColor))
                                    coloropera = ColorTranslator.FromHtml(gridSource[0].OperationButtonColor);
                                else
                                    coloropera = Color.FromArgb(0x42, 0x67, 0xB2);

                                if (IsValidHtmlColor(gridSource[0].MenuButtonColor))
                                    colormenu = ColorTranslator.FromHtml(gridSource[0].MenuButtonColor);
                                else
                                    colormenu = Color.FromArgb(0x42, 0x67, 0xB2);

                                if (gridSource[0].Gmail)
                                    chk_gmail.Checked = true;
                                if (gridSource[0].Whatsapp)
                                    chk_whatsapp.Checked = true;

                                opbuttoncolor = coloropera;
                                menubuttoncolor = colormenu;
                                txtappcolorname.Text = Convert.ToString(gridSource[0].APPColor);
                                txtmenubuttoncolorname.Text = Convert.ToString(gridSource[0].MenuButtonColor);
                                txtoperbuttoncolorname.Text = Convert.ToString(gridSource[0].OperationButtonColor);
                                txtsid.Text = Convert.ToString(gridSource[0].SId);

                                ib_Category.BackColor = opbuttoncolor;

                                //ib_Item.BackColor = opbuttoncolor;
                                //ib_ingrediant.BackColor = opbuttoncolor;
                                //ib_menu.BackColor = opbuttoncolor;
                                //ib_unit.BackColor = opbuttoncolor;
                                //ib_table.BackColor = opbuttoncolor;
                                //ib_purchase.BackColor = opbuttoncolor;
                                //ib_purchasereturn.BackColor = opbuttoncolor;
                                //ib_tax.BackColor = opbuttoncolor;
                                //ib_vendor.BackColor = opbuttoncolor;
                                //ib_Discount_offer.BackColor = opbuttoncolor;
                                //ib_Expence.BackColor = opbuttoncolor;
                                //ib_orderview.BackColor = opbuttoncolor;
                                //ib_kitchenview.BackColor = opbuttoncolor;
                                //ib_PromoCode.BackColor = opbuttoncolor;
                                //ib_Company.BackColor = opbuttoncolor;
                                //ib_useraccount.BackColor = opbuttoncolor;
                                //ib_designation.BackColor = opbuttoncolor;
                                //ib_department.BackColor = opbuttoncolor;
                                //ib_employee.BackColor = opbuttoncolor;
                                //ib_reservation.BackColor = opbuttoncolor;
                                //ib_userpermission.BackColor = opbuttoncolor;
                                //ib_addon.BackColor = opbuttoncolor;

                                pbopbuttoncoloepicker.BackColor = opbuttoncolor;

                                //Menuhome.BackColor = menubuttoncolor;
                                //Menuoperation.BackColor = menubuttoncolor;
                                //Menubilling.BackColor = menubuttoncolor;
                                //Menureport.BackColor = menubuttoncolor;
                                //Menulogout.BackColor = menubuttoncolor;
                                //Menusetting.BackColor = menubuttoncolor;
                                //menuEmployee.BackColor = menubuttoncolor;
                                //Menutable.BackColor = menubuttoncolor;
                                pbsidebuttoncoloepicker.BackColor = menubuttoncolor;

                                pbappcoloepicker.BackColor = colorapp;

                            }
                        }));
                    }
                }
                else
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            pbappcoloepicker.BackColor = Color.FromArgb(34, 46, 80);
                            txtsid.Text = "0";
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page AppColorSetting()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
        #endregion

        #region TexBox Round function
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
                clsgen.LogException("homepage Page ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }
        #endregion
        private void ApplyMenuStyle()
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int iconSize = screenWidth > 1366 ? 36 : 28;
            int cornerRadius = screenWidth > 1366 ? 20 : 15;

            Color menuColor = Color.FromArgb(150, 120, 190);

            // Apply style to all menu buttons
            foreach (Control c in flp_sidemenu.Controls)
            {
                if (c is Button btn)
                {
                    UIHelper.StyleSideMenuButton(btn, cornerRadius, iconSize, menuColor);
                }
            }
        }
        private void SetButtonImage(Button btn, Image img, int width, int height)
        {
            if (img == null || btn == null) return;

            btn.ImageAlign = ContentAlignment.TopCenter;
            btn.TextAlign = ContentAlignment.BottomCenter;

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int pd = screenWidth > 1366 ? 20 : 5;
            btn.Padding = new Padding(0, pd, 0, 0);
            btn.Image = new Bitmap(img, new Size(width, height));
            btn.Font = AppFonts.GetFont(12);
            btn.ForeColor = Color.White;  
        }
        private void SetButtonImage_mainmenu(Button btn, Image img, int width, int height)
        {
            if (img == null || btn == null) return;

            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Image = new Bitmap(img, new Size(width, height));
            btn.Padding = new Padding(30, 0, 0, 0);
            btn.ForeColor = Color.White;
            btn.Font = AppFonts.GetFont(12F);

        }
        private void ApplyGlobalFont(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Font = AppFonts.DefaultFont;
                if (ctrl.HasChildren)
                    ApplyGlobalFont(ctrl);
            }
        }
        public void button_imageset()
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        UIHelper.AutoResizeSideMenuButtons(flp_sidemenu);
                        foreach (Button btn in flp_sidemenu.Controls.OfType<Button>())
                        {
                            Size iconSize = UIHelper.GetScaledIconSize(btn.Height);
                            switch (btn.Name)
                            {
                                case "Menuhome":
                                    SetButtonImage_mainmenu(btn, Properties.Resources.Home_icon, iconSize.Width, iconSize.Height);
                                    break;
                                case "Menuoperation":
                                    SetButtonImage_mainmenu(btn, Properties.Resources.operation, iconSize.Width, iconSize.Height);
                                    break;
                                case "Menubilling":
                                    SetButtonImage_mainmenu(btn, Properties.Resources.bill, iconSize.Width, iconSize.Height);
                                    break;
                                case "Menureport":
                                    SetButtonImage_mainmenu(btn, Properties.Resources.report, iconSize.Width, iconSize.Height);
                                    break;
                                case "Menusetting":
                                    SetButtonImage_mainmenu(btn, Properties.Resources.setting, iconSize.Width, iconSize.Height);
                                    break;
                                case "Menulogout":
                                    SetButtonImage_mainmenu(btn, Properties.Resources.logout, iconSize.Width, iconSize.Height);
                                    break;
                                case "menuEmployee":
                                    SetButtonImage_mainmenu(btn, Properties.Resources.Employee, iconSize.Width, iconSize.Height);
                                    break;
                                case "Menutable":
                                    SetButtonImage_mainmenu(btn, Properties.Resources.menutable, iconSize.Width, iconSize.Height);
                                    break;
                            }
                        }

                        // Set images for the 'ib_' buttons
                        SetButtonImage(ib_Category, FoodandBeverage.Properties.Resources.category, desiredWidth, desiredHeight);
                        SetButtonImage(ib_Item, FoodandBeverage.Properties.Resources.item, desiredWidth, desiredHeight);
                        SetButtonImage(ib_ingrediant, FoodandBeverage.Properties.Resources.Product, desiredWidth, desiredHeight);
                        SetButtonImage(ib_menu, FoodandBeverage.Properties.Resources.menu, desiredWidth, desiredHeight);
                        SetButtonImage(ib_unit, FoodandBeverage.Properties.Resources.unit, desiredWidth, desiredHeight);
                        SetButtonImage(ib_table, FoodandBeverage.Properties.Resources.table_reg, desiredWidth, desiredHeight);
                        SetButtonImage(ib_purchase, FoodandBeverage.Properties.Resources.Purchase, desiredWidth, desiredHeight);
                        SetButtonImage(ib_purchasereturn, FoodandBeverage.Properties.Resources.Purchase_return, desiredWidth, desiredHeight);
                        SetButtonImage(ib_tax, FoodandBeverage.Properties.Resources.tax, desiredWidth, desiredHeight);
                        SetButtonImage(ib_vendor, FoodandBeverage.Properties.Resources.vendericon, desiredWidth, desiredHeight);
                        SetButtonImage(ib_Expence, FoodandBeverage.Properties.Resources.withdraw, desiredWidth, desiredHeight);
                        SetButtonImage(ib_Discount_offer, FoodandBeverage.Properties.Resources.discount, desiredWidth, desiredHeight);
                        SetButtonImage(ib_orderview, FoodandBeverage.Properties.Resources.orderview, desiredWidth, desiredHeight);
                        SetButtonImage(ib_kitchenview, FoodandBeverage.Properties.Resources.kitchen_order_view, desiredWidth, desiredHeight);
                        SetButtonImage(ib_PromoCode, FoodandBeverage.Properties.Resources.promo_code, desiredWidth, desiredHeight);
                        SetButtonImage(ib_Company, FoodandBeverage.Properties.Resources.comapny, desiredWidth, desiredHeight);
                        SetButtonImage(ib_addon, FoodandBeverage.Properties.Resources.addon, desiredWidth, desiredHeight);
                        SetButtonImage(ib_reservation, FoodandBeverage.Properties.Resources.reserved, desiredWidth, desiredHeight);

                        SetButtonImage(ib_employee, FoodandBeverage.Properties.Resources.Employee, desiredWidth, desiredHeight);
                        SetButtonImage(ib_department, FoodandBeverage.Properties.Resources.Department_icon, desiredWidth, desiredHeight);
                        SetButtonImage(ib_designation, FoodandBeverage.Properties.Resources.Designation_icon, desiredWidth, desiredHeight);
                        SetButtonImage(ib_useraccount, FoodandBeverage.Properties.Resources.User_account, desiredWidth, desiredHeight);
                        SetButtonImage(ib_userpermission, FoodandBeverage.Properties.Resources.userpermisson, desiredWidth, desiredHeight);


                        SetButtonImage(ib_virtualkeybpard, FoodandBeverage.Properties.Resources.Employee, desiredWidth, desiredHeight);
                        SetButtonImage(ib_msgsetting, FoodandBeverage.Properties.Resources.add_des_dep_user, desiredWidth, desiredHeight);
                        SetButtonImage(ib_gensetting, FoodandBeverage.Properties.Resources.add_des_dep_user, desiredWidth, desiredHeight);
                        SetButtonImage(ib_billsetting, FoodandBeverage.Properties.Resources.add_des_dep_user, desiredWidth, desiredHeight);
                        SetButtonImage(ib_appcolor, FoodandBeverage.Properties.Resources.reserved, desiredWidth, desiredHeight);
                        SetButtonImage(ib_drawer, FoodandBeverage.Properties.Resources.userpermisson, desiredWidth, desiredHeight);

                    }));
                }
                // SetRoundedButton(ib_Category, 70);
                //SetRoundedButton(ib_Item, 70);
                //SetRoundedButton(ib_ingrediant, 70);
                //SetRoundedButton(ib_menu, 70);
                //SetRoundedButton(ib_unit, 70);
                //SetRoundedButton(ib_table, 70);
                //SetRoundedButton(ib_purchase, 70);
                //SetRoundedButton(ib_purchasereturn, 70);
                //SetRoundedButton(ib_tax, 70);
                //SetRoundedButton(ib_vendor, 70);
                //SetRoundedButton(ib_withdrawal, 70);
                //SetRoundedButton(ib_Discount_offer, 70);
                //SetRoundedButton(ib_orderview, 70);
                //SetRoundedButton(ib_kitchenview, 70);
                //SetRoundedButton(ib_PromoCode, 70);

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("homepage Page button_imageset()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void tlp_main_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Column == 3) // Check if it's the second column (change to the desired column index)
            {
                using (SolidBrush brush = new SolidBrush(Color.Red)) // Replace with your desired color
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                }
            }
        }


        private void Menuhome_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnl_Drawer.Visible)
                    pnl_Drawer.Visible = false;
                ButtonID = Convert.ToInt32(Menubutton.Menuhome);
                panelDesktop.Controls.Clear();
                btn_pickup.Visible = false;
                if (ChkWritePermission(Convert.ToInt32(formsenum.DashboardCharts)))
                {
                    if (System.Windows.Forms.Application.OpenForms["FrmDashboardReport"] != null)
                    {
                        FrmDashboardReport _instanceDashboardReport = Application.OpenForms["FrmDashboardReport"] as FrmDashboardReport;
                        if (_instanceDashboardReport.WindowState == FormWindowState.Minimized)
                            _instanceDashboardReport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        else
                        {
                            _instanceDashboardReport.Dock = DockStyle.Fill;
                            _instanceDashboardReport.Visible = true;
                            ResizeForm(_instanceDashboardReport);
                            panelDesktop.Controls.Add(_instanceDashboardReport);
                            _instanceDashboardReport.RefreshCharts();
                            _instanceDashboardReport.Show();
                            _instanceDashboardReport.BringToFront();
                        }
                    }
                    else
                    {
                        FrmDashboardReport _instanceDashboardReport = new FrmDashboardReport();
                        _instanceDashboardReport.Dock = DockStyle.Fill;
                        ResizeForm(_instanceDashboardReport);
                        _instanceDashboardReport.TopLevel = false;
                        panelDesktop.Controls.Add(_instanceDashboardReport);
                        _instanceDashboardReport.Show();
                        _instanceDashboardReport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        _instanceDashboardReport.BringToFront();
                    }
                }

                if (menutable_click)
                {
                    CloseTableViewPage();
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  Menuhome_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
        private void DashboardButtonClickEventHandler(object sender, EventArgs e)
        {
            //item Chosse from serach Box 
            try
            {
                Control clickedControl = sender as Control;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  DashboardButtonClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
        private void ToggleSideMenu()
        {
            try
            {
                CloseTableViewPage();
                if (ButtonID == Convert.ToInt32(Menubutton.Menutable))
                {
                    tlp_tablestatus.SuspendLayout();
                    btn_rev.Visible = true;
                    btn_running.Visible = true;
                    btn_avial.Visible = true;
                    btntrefresh.Visible = true;
                    tlp_tablestatus.ResumeLayout();

                    FrmTableView _instanceFrmTableView = new FrmTableView(0);
                    ResizeForm(_instanceFrmTableView);
                    _instanceFrmTableView.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmTableView);
                    _instanceFrmTableView.Show();
                    _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmTableView.BringToFront();

                }
                else if (ButtonID == Convert.ToInt32(Menubutton.Menubilling))
                {
                    FrmCombinedBill _instanceCombinedBill = new FrmCombinedBill();
                    ResizeForm(_instanceCombinedBill);
                    _instanceCombinedBill.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceCombinedBill);
                    _instanceCombinedBill.Show();
                    _instanceCombinedBill.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceCombinedBill.BringToFront();

                }

                // If collapsing
                if (!isMenuCollapsed)
                {
                    // Store original width
                    originalMenuWidth = flp_sidemenu.Width;

                    // Collapse animation (optional)
                    flp_sidemenu.Width = 60;

                    // Loop through menu buttons and show only icons
                    foreach (Control ctrl in flp_sidemenu.Controls)
                    {
                        if (ctrl is Button btn)
                        {
                            btn.Tag = btn.Text; // store text for restore
                            btn.Text = string.Empty;
                            btn.TextAlign = ContentAlignment.MiddleCenter;
                            btn.ImageAlign = ContentAlignment.MiddleCenter;
                        }
                    }

                    isMenuCollapsed = true;
                }
                else // Expand
                {
                    flp_sidemenu.Width = originalMenuWidth;

                    foreach (Control ctrl in flp_sidemenu.Controls)
                    {
                        if (ctrl is Button btn && btn.Tag != null)
                        {
                            btn.Text = btn.Tag.ToString();
                            btn.TextAlign = ContentAlignment.MiddleLeft;
                            btn.ImageAlign = ContentAlignment.MiddleLeft;
                        }
                    }

                    isMenuCollapsed = false;
                }
                UIHelper.RoundCorners(flp_sidemenu, 25, ColorTranslator.FromHtml("#AC3CC6"));
            }
            catch (Exception ex)
            {
                clsgen.LogException("FrmDashboard ToggleSideMenu()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

   

        private void Menuoperation_Click(object sender, EventArgs e)
        {
            try
            {
                AdjustButtonsToPanel();
                if (pnl_Drawer.Visible)
                    pnl_Drawer.Visible = false;
                ButtonID = Convert.ToInt32(Menubutton.Menuoperation);
                time_syn_lal.Stop();
                reset_sync.Stop();
                lblsync.Visible = false;
                panelDesktop.SuspendLayout();
                if (menutable_click)
                {
                    CloseTableViewPage();
                }
                panelDesktop.Controls.Clear();
                panelDesktop.Controls.Add(flp_operabuttons);
                flp_operabuttons.Dock = DockStyle.Fill;

                flp_operabuttons.Visible = true;
                btn_pickup.Visible = false;

                if (screenWidth > 1367 && screenHeight > 769)
                {
                    desiredWidth = 52;
                    desiredHeight = 52;
                    HdesiredWidth = 60;
                    HdesiredHeight = 60;
                }
                else
                {
                    desiredWidth = 32;
                    desiredHeight = 32;
                    HdesiredWidth = 40;
                    HdesiredHeight = 40;
                }
                panelDesktop.ResumeLayout();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  Menuoperation_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }


        private void Menubilling_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnl_Drawer.Visible)
                    pnl_Drawer.Visible = false;
                
                ButtonID = Convert.ToInt32(Menubutton.Menubilling);
                time_syn_lal.Stop();
                reset_sync.Stop();
                lblsync.Visible = false;
                btn_pickup.Visible = false;
                

                if (System.Windows.Forms.Application.OpenForms["FrmDashboardReport"] != null)
                {
                    FrmDashboardReport _instanceFrmDashboardReport = Application.OpenForms["FrmDashboardReport"] as FrmDashboardReport;
                    _instanceFrmDashboardReport.Visible = false;
                }

                if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
                {
                    FrmTableView _insFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                    _insFrmTableView.Visible = false;
                }
                panelDesktop.SuspendLayout();

                if (flp_operabuttons.Visible)
                    flp_operabuttons.Visible = false;

                if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
                {
                    FrmTableView _instanceFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                    //_instanceFrmTableView.AutoTimer.Stop();
                    _instanceFrmTableView.Close();
                    _instanceFrmTableView.Dispose();
                    GC.Collect();

                    tlp_tablestatus.SuspendLayout();
                    btn_rev.Visible = false;
                    btn_running.Visible = false;
                    btn_avial.Visible = false;
                    btntrefresh.Visible = false;
                    tlp_tablestatus.ResumeLayout();
                }

                if (System.Windows.Forms.Application.OpenForms["FrmCombinedBill"] != null)
                {
                    FrmCombinedBill _instanceCombinedBill = Application.OpenForms["FrmCombinedBill"] as FrmCombinedBill;
                    if (_instanceCombinedBill.WindowState == FormWindowState.Minimized)
                        _instanceCombinedBill.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceCombinedBill.Visible = true;
                        _instanceCombinedBill.AssignDrawerCheck();
                        //_instanceCombinedBill.AutoTimer.Start();
                        _instanceCombinedBill.Dock = DockStyle.Fill;
                        ResizeForm(_instanceCombinedBill);
                        panelDesktop.Controls.Add(_instanceCombinedBill);
                        _instanceCombinedBill.Show();
                        _instanceCombinedBill.BringToFront();
                    }
                }
                else
                {
                    btn_rev.Visible = false;
                    btn_running.Visible = false;
                    btn_avial.Visible = false;
                    btntrefresh.Visible = false;

                    FrmCombinedBill _instanceCombinedBill = new FrmCombinedBill();
                    ResizeForm(_instanceCombinedBill);
                    _instanceCombinedBill.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceCombinedBill);
                    _instanceCombinedBill.Show();
                    _instanceCombinedBill.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceCombinedBill.BringToFront();

                }
                menutable_click = false;
                panelDesktop.ResumeLayout();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  Menubilling_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }


        private void Menureport_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnl_Drawer.Visible)
                    pnl_Drawer.Visible = false;

                ButtonID = Convert.ToInt32(Menubutton.Menureport);
                time_syn_lal.Stop();
                reset_sync.Stop();
                lblsync.Visible = false;
                btn_pickup.Visible = false;
                panelDesktop.SuspendLayout();

                if (menutable_click)
                {
                    CloseTableViewPage();
                }

                float largerFontSize = 12;
                ContextMenuStrip contextMenu = new ContextMenuStrip();

                ToolStripMenuItem salesReportItem = new ToolStripMenuItem("Sales Report");
                ToolStripMenuItem inventoryReportItem = new ToolStripMenuItem("Inventory Report");
                ToolStripMenuItem employeePerformanceItem = new ToolStripMenuItem("Employee Report");
                ToolStripMenuItem expenseReportItem = new ToolStripMenuItem("Expense Report");
                ToolStripMenuItem reservationSeatingItem = new ToolStripMenuItem("Reservation and Seating Report");
                //ToolStripMenuItem feedbackReportItem = new ToolStripMenuItem("Customer Feedback Report");
                //ToolStripMenuItem loyaltyProgramItem = new ToolStripMenuItem("Loyalty Program Report");
                ToolStripMenuItem promotionsDiscountsItem = new ToolStripMenuItem("Promotions and Discounts Report");
                ToolStripMenuItem Purchaseitem = new ToolStripMenuItem("Purchase Report");
                ToolStripMenuItem MenuAnalysisReports = new ToolStripMenuItem("Menu Analysis Reports");
                ToolStripMenuItem TableTurnoverReports = new ToolStripMenuItem("Table Turnover Reports");
                ToolStripMenuItem VendorReports = new ToolStripMenuItem("Supplier Reports");


                salesReportItem.Click += SalesReportItem_Click;
                inventoryReportItem.Click += InventoryReportItem_Click;
                employeePerformanceItem.Click += EmployeePerformanceItem_Click;
                expenseReportItem.Click += ExpenseReportItem_Click;
                reservationSeatingItem.Click += ReservationSeatingItem_Click;
                //feedbackReportItem.Click += FeedbackReportItem_Click;
                //loyaltyProgramItem.Click += LoyaltyProgramItem_Click;
                promotionsDiscountsItem.Click += PromotionsDiscountsItem_Click;
                Purchaseitem.Click += PurchaseitemItem_Click;
                MenuAnalysisReports.Click += MenuAnalysisReports_Click;
                TableTurnoverReports.Click += TableTurnoverReports_Click;
                VendorReports.Click += VendorReports_Click;

                contextMenu.Items.Add(salesReportItem);
                contextMenu.Items.Add(inventoryReportItem);
                contextMenu.Items.Add(employeePerformanceItem);
                contextMenu.Items.Add(expenseReportItem);
                contextMenu.Items.Add(reservationSeatingItem);
                //contextMenu.Items.Add(feedbackReportItem);
                //contextMenu.Items.Add(loyaltyProgramItem);
                contextMenu.Items.Add(promotionsDiscountsItem);
                contextMenu.Items.Add(Purchaseitem);
                contextMenu.Items.Add(TableTurnoverReports);
                contextMenu.Items.Add(MenuAnalysisReports);
                contextMenu.Items.Add(VendorReports);

                contextMenu.Show(Cursor.Position);

                panelDesktop.ResumeLayout();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  Menureport_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }

        private void VendorReports_Click(object sender, EventArgs e)
        {
            try
            {
                panelDesktop.Controls.Clear();
                if (System.Windows.Forms.Application.OpenForms["Frmreport"] != null)
                {
                    Report.Frmreport _instanceFrmreport = Application.OpenForms["Frmreport"] as Report.Frmreport;

                    if (_instanceFrmreport.WindowState == FormWindowState.Minimized)
                    {
                        _instanceFrmreport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    }
                    else
                    {
                        _instanceFrmreport.Close();
                        Report.Frmreport newFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.VendorReports));
                        newFrmreport.TopLevel = false;
                        newFrmreport.Dock = DockStyle.Fill;
                        panelDesktop.Controls.Add(newFrmreport);
                        newFrmreport.Show();
                    }
                }
                else
                {
                    Report.Frmreport _instanceFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.VendorReports));
                    _instanceFrmreport.TopLevel = false;
                    _instanceFrmreport.Dock = DockStyle.Fill;
                    panelDesktop.Controls.Add(_instanceFrmreport);
                    _instanceFrmreport.Show();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  VendorReports_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }
        private void TableTurnoverReports_Click(object sender, EventArgs e)
        {
            try
            {
                panelDesktop.Controls.Clear();
                if (System.Windows.Forms.Application.OpenForms["Frmreport"] != null)
                {
                    Report.Frmreport _instanceFrmreport = Application.OpenForms["Frmreport"] as Report.Frmreport;

                    if (_instanceFrmreport.WindowState == FormWindowState.Minimized)
                    {
                        _instanceFrmreport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    }
                    else
                    {
                        _instanceFrmreport.Close();
                        Report.Frmreport newFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.TableTurnoverReports));
                        newFrmreport.TopLevel = false;
                        newFrmreport.Dock = DockStyle.Fill;
                        panelDesktop.Controls.Add(newFrmreport);
                        newFrmreport.Show();
                    }
                }
                else
                {
                    Report.Frmreport _instanceFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.TableTurnoverReports));
                    _instanceFrmreport.TopLevel = false;
                    _instanceFrmreport.Dock = DockStyle.Fill;
                    panelDesktop.Controls.Add(_instanceFrmreport);
                    _instanceFrmreport.Show();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  TableTurnoverReports_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }
        private void MenuAnalysisReports_Click(object sender, EventArgs e)
        {
            try
            {
                panelDesktop.Controls.Clear();
                if (System.Windows.Forms.Application.OpenForms["Frmreport"] != null)
                {
                    Report.Frmreport _instanceFrmreport = Application.OpenForms["Frmreport"] as Report.Frmreport;

                    if (_instanceFrmreport.WindowState == FormWindowState.Minimized)
                    {
                        _instanceFrmreport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    }
                    else
                    {
                        _instanceFrmreport.Close();
                        Report.Frmreport newFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.MenuAnalysisReports));
                        newFrmreport.TopLevel = false;
                        newFrmreport.Dock = DockStyle.Fill;
                        panelDesktop.Controls.Add(newFrmreport);
                        newFrmreport.Show();
                    }
                }
                else
                {
                    Report.Frmreport _instanceFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.MenuAnalysisReports));
                    _instanceFrmreport.TopLevel = false;
                    _instanceFrmreport.Dock = DockStyle.Fill;
                    panelDesktop.Controls.Add(_instanceFrmreport);
                    _instanceFrmreport.Show();
                }
            }
               catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page ", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }

        private void PurchaseitemItem_Click(object sender, EventArgs e)
        {
            try
            {
                panelDesktop.Controls.Clear();
                if (System.Windows.Forms.Application.OpenForms["Frmreport"] != null)
                {
                    Report.Frmreport _instanceFrmreport = Application.OpenForms["Frmreport"] as Report.Frmreport;

                    if (_instanceFrmreport.WindowState == FormWindowState.Minimized)
                    {
                        _instanceFrmreport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    }
                    else
                    {
                        _instanceFrmreport.Close();
                        Report.Frmreport newFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.Purchasereportitem));
                        newFrmreport.TopLevel = false;
                        newFrmreport.Dock = DockStyle.Fill;
                        panelDesktop.Controls.Add(newFrmreport);
                        newFrmreport.Show();
                    }
                }
                else
                {
                    Report.Frmreport _instanceFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.Purchasereportitem));
                    _instanceFrmreport.TopLevel = false;
                    _instanceFrmreport.Dock = DockStyle.Fill;
                    panelDesktop.Controls.Add(_instanceFrmreport);
                    _instanceFrmreport.Show();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  TableTurnoverReports_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }

        private void ExpenseReportItem_Click(object sender, EventArgs e)
        {
            try
            {
                panelDesktop.Controls.Clear();
                if (System.Windows.Forms.Application.OpenForms["Frmreport"] != null)
                {
                    Report.Frmreport _instanceFrmreport = Application.OpenForms["Frmreport"] as Report.Frmreport;

                    if (_instanceFrmreport.WindowState == FormWindowState.Minimized)
                    {
                        _instanceFrmreport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    }
                    else
                    {
                        _instanceFrmreport.Close();
                        Report.Frmreport newFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.expenseReportItem));
                        newFrmreport.TopLevel = false;
                        newFrmreport.Dock = DockStyle.Fill;
                        panelDesktop.Controls.Add(newFrmreport);
                        newFrmreport.Show();
                    }
                }
                else
                {
                    Report.Frmreport _instanceFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.expenseReportItem));
                    _instanceFrmreport.TopLevel = false;
                    _instanceFrmreport.Dock = DockStyle.Fill;
                    panelDesktop.Controls.Add(_instanceFrmreport);
                    _instanceFrmreport.Show();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  TableTurnoverReports_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }
        private void ReservationSeatingItem_Click(object sender, EventArgs e)
        {
            try
            {
                panelDesktop.Controls.Clear();
                if (System.Windows.Forms.Application.OpenForms["Frmreport"] != null)
                {
                    Report.Frmreport _instanceFrmreport = Application.OpenForms["Frmreport"] as Report.Frmreport;

                    if (_instanceFrmreport.WindowState == FormWindowState.Minimized)
                    {
                        _instanceFrmreport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    }
                    else
                    {
                        _instanceFrmreport.Close();
                        Report.Frmreport newFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.reservationSeatingItem));
                        newFrmreport.TopLevel = false;
                        newFrmreport.Dock = DockStyle.Fill;
                        panelDesktop.Controls.Add(newFrmreport);
                        newFrmreport.Show();
                    }
                }
                else
                {
                    Report.Frmreport _instanceFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.reservationSeatingItem));
                    _instanceFrmreport.TopLevel = false;
                    _instanceFrmreport.Dock = DockStyle.Fill;
                    panelDesktop.Controls.Add(_instanceFrmreport);
                    _instanceFrmreport.Show();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  TableTurnoverReports_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }
        private void EmployeePerformanceItem_Click(object sender, EventArgs e)
        {
            try
            {
                panelDesktop.Controls.Clear();
                if (System.Windows.Forms.Application.OpenForms["Frmreport"] != null)
                {
                    Report.Frmreport _instanceFrmreport = Application.OpenForms["Frmreport"] as Report.Frmreport;

                    if (_instanceFrmreport.WindowState == FormWindowState.Minimized)
                    {
                        _instanceFrmreport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    }
                    else
                    {
                        _instanceFrmreport.Close();
                        Report.Frmreport newFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.employeePerformanceItem));
                        newFrmreport.TopLevel = false;
                        newFrmreport.Dock = DockStyle.Fill;
                        panelDesktop.Controls.Add(newFrmreport);
                        newFrmreport.Show();
                    }
                }
                else
                {
                    Report.Frmreport _instanceFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.employeePerformanceItem));
                    _instanceFrmreport.TopLevel = false;
                    _instanceFrmreport.Dock = DockStyle.Fill;
                    panelDesktop.Controls.Add(_instanceFrmreport);
                    _instanceFrmreport.Show();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  TableTurnoverReports_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }
        private void PromotionsDiscountsItem_Click(object sender, EventArgs e)
        {
            try
            {
                panelDesktop.Controls.Clear();
                if (System.Windows.Forms.Application.OpenForms["Frmreport"] != null)
                {
                    Report.Frmreport _instanceFrmreport = Application.OpenForms["Frmreport"] as Report.Frmreport;

                    if (_instanceFrmreport.WindowState == FormWindowState.Minimized)
                    {
                        _instanceFrmreport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    }
                    else
                    {
                        _instanceFrmreport.Close();
                        Report.Frmreport newFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.promotionsDiscountsItem));
                        newFrmreport.TopLevel = false;
                        newFrmreport.Dock = DockStyle.Fill;
                        panelDesktop.Controls.Add(newFrmreport);
                        newFrmreport.Show();
                    }
                }
                else
                {
                    Report.Frmreport _instanceFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.promotionsDiscountsItem));
                    _instanceFrmreport.TopLevel = false;
                    _instanceFrmreport.Dock = DockStyle.Fill;
                    panelDesktop.Controls.Add(_instanceFrmreport);
                    _instanceFrmreport.Show();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  TableTurnoverReports_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }
        private void SalesReportItem_Click(object sender, EventArgs e)
        {
            try
            {
                panelDesktop.Controls.Clear();
                if (System.Windows.Forms.Application.OpenForms["Frmreport"] != null)
                {
                    Report.Frmreport _instanceFrmreport = Application.OpenForms["Frmreport"] as Report.Frmreport;

                    if (_instanceFrmreport.WindowState == FormWindowState.Minimized)
                    {
                        _instanceFrmreport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    }
                    else
                    {
                        _instanceFrmreport.Close();
                        Report.Frmreport newFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.salesReportItem));
                        newFrmreport.TopLevel = false;
                        newFrmreport.Dock = DockStyle.Fill;
                        panelDesktop.Controls.Add(newFrmreport);
                        newFrmreport.Show();
                    }
                }
                else
                {
                    Report.Frmreport _instanceFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.salesReportItem));
                    _instanceFrmreport.TopLevel = false;
                    _instanceFrmreport.Dock = DockStyle.Fill;
                    panelDesktop.Controls.Add(_instanceFrmreport);
                    _instanceFrmreport.Show();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  TableTurnoverReports_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }

        private void InventoryReportItem_Click(object sender, EventArgs e)
        {
            try
            {
                panelDesktop.Controls.Clear();
                if (System.Windows.Forms.Application.OpenForms["Frmreport"] != null)
                {
                    Report.Frmreport _instanceFrmreport = Application.OpenForms["Frmreport"] as Report.Frmreport;

                    if (_instanceFrmreport.WindowState == FormWindowState.Minimized)
                    {
                        _instanceFrmreport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    }
                    else
                    {
                        _instanceFrmreport.Close();
                        Report.Frmreport newFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.inventoryReportItem));
                        newFrmreport.TopLevel = false;
                        newFrmreport.Dock = DockStyle.Fill;
                        panelDesktop.Controls.Add(newFrmreport);
                        newFrmreport.Show();
                    }
                }
                else
                {
                    Report.Frmreport _instanceFrmreport = new Report.Frmreport(Convert.ToInt32(ReportType.inventoryReportItem));
                    _instanceFrmreport.TopLevel = false;
                    _instanceFrmreport.Dock = DockStyle.Fill;
                    panelDesktop.Controls.Add(_instanceFrmreport);
                    _instanceFrmreport.Show();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("HomePage  TableTurnoverReports_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));

            }

        }
        public void CloseTableViewPage()
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
                {
                    FrmTableView _instanceFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                    //_instanceFrmTableView.AutoTimer.Stop();
                    _instanceFrmTableView.Close();
                    _instanceFrmTableView.Dispose();
                    GC.Collect();

                    tlp_tablestatus.SuspendLayout();
                    btn_rev.Visible = false;
                    btn_running.Visible = false;
                    btn_avial.Visible = false;
                    btntrefresh.Visible = false;
                    btn_pickup.Visible = false;
                    tlp_tablestatus.ResumeLayout();
                }
                if (System.Windows.Forms.Application.OpenForms["FrmCombinedBill"] != null)
                {
                    FrmCombinedBill _instanceFrmCombinedBill = Application.OpenForms["FrmCombinedBill"] as FrmCombinedBill;
                    //_instanceFrmTableView.AutoTimer.Stop();
                    _instanceFrmCombinedBill.Close();
                    _instanceFrmCombinedBill.Dispose();
                    GC.Collect();
                }
                if (System.Windows.Forms.Application.OpenForms["FrmPickup"] != null)
                {
                    FrmPickup _instanceFrmPickup = Application.OpenForms["FrmPickup"] as FrmPickup;
                    //_instanceFrmTableView.AutoTimer.Stop();
                    _instanceFrmPickup.Close();
                    _instanceFrmPickup.Dispose();
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("HomePage CloseTableViewPage()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));

            }
        }
        private void Menusetting_Click(object sender, EventArgs e)
        {
            try
            {
                ButtonID = Convert.ToInt32(Menubutton.Menusetting);
                reset_sync.Stop();
                lblsync.Visible = false;
                btn_pickup.Visible = false;
                panelDesktop.Controls.Clear();
                if (menutable_click)
                {
                    CloseTableViewPage();
                }
                panelDesktop.Controls.Add(flp_settingbtn);
                flp_settingbtn.Dock = DockStyle.Fill;
                flp_settingbtn.BringToFront();
                flp_settingbtn.Visible = true;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("HomePage Menusetting_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }


        public bool DrawerCheckBranch(int empid)
        {
            try
            {
                Drawersystemname = "";
                BllSearch<DmnGridFields> bllsearchdrawer = new BllSearch<DmnGridFields>();
                DmnSearch dmnsearckdrawer = new DmnSearch();
                dmnsearckdrawer.Select = "COUNT(*) OVER() AS Field1,t.CreatedBy as Field2,e.EmployeeName as Field3,d.DesignationName as Field4,t.Id as Field5,t.LoanAmount as Field6,t.Drops as Field7,u.UserAccountId as Field8,e.EmployeeId as Field9,t.DName as Field10";
                dmnsearckdrawer.Table = "tblDrawerAssign t   left join tblUserAccount  u on u.EmployeeId= t.EmployeeId left join tblEmployee  e on e.EmployeeId=t.EmployeeId left join tblDesignation  d on d.DesignationId=e.DesignationId";
                dmnsearckdrawer.WhereNdGroupBy = "where  t.BranchId=" + BranchId + " and t.Status=1 and t.EmployeeId =" + empid;
                dmnsearckdrawer.OrderBy = "";
                var gridesource = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckdrawer);
                if (gridesource.Count > 0)
                {
                    Drawersystemname = gridesource[0].Field10;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Admin page DrawerCheckBranch()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
        }
        #region drawerAssign
        public void AssigndrawerCheck()
        {
            try
            {
                pnl_Drawer.Visible = false;
                BllSearch<DmnGridFields> bllsearchdrawer = new BllSearch<DmnGridFields>();
                DmnSearch dmnsearckdrawer = new DmnSearch();
                dmnsearckdrawer.Select = "COUNT(*) OVER() AS Field1,t.CreatedBy as Field2,e.EmployeeName as Field3,d.DesignationName as Field4,t.Id as Field5,t.LoanAmount as Field6,t.Drops as Field7,InitilAmount as Field8,e.EmployeeId as Field9,t.CreatedDateTime Field10";
                dmnsearckdrawer.Table = "tblDrawerAssign t  left join tblEmployee  e on e.EmployeeId=t.EmployeeId left join tblDesignation  d on d.DesignationId=e.DesignationId";
                dmnsearckdrawer.WhereNdGroupBy = "where t.DName='" + System.Environment.MachineName + "'  and t.Status=1";
                dmnsearckdrawer.OrderBy = "";
                var gridesource = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckdrawer);
                if (gridesource.Count > 0)
                {
                    pnl_Drawer.Location = new Point(this.ClientSize.Width / 2 - pnl_Drawer.Size.Width / 2, this.ClientSize.Height / 2 - pnl_Drawer.Size.Height / 2);

                    if (Convert.ToInt32(gridesource[0].Field9) == Convert.ToInt32(FrmLogin.EmployeeId))
                    {

                        txtdrawerid.Text = Convert.ToString(gridesource[0].Field5);
                        txtinitialamt.Text = Convert.ToString(gridesource[0].Field8);
                        txtextraaddamt.Text = Convert.ToString(gridesource[0].Field6);
                        txtdrops.Text = Convert.ToString(gridesource[0].Field7);
                        txtempoyeId.Text = Convert.ToString(gridesource[0].Field9);

                        txtextraaddamt_backup.Text = Convert.ToString(gridesource[0].Field6);
                        txtdrops_backup.Text = Convert.ToString(gridesource[0].Field7);

                        label15.Text = System.Environment.MachineName;
                        lbl_staffname.Text = Convert.ToString(gridesource[0].Field3);
                        lbl_desigtnation.Text = Convert.ToString(gridesource[0].Field4);
                        lbl_staffname.Visible = true;
                        txtdrops.Visible = true;
                        txtextraaddamt.Visible = true;
                        label12.Visible = true;
                        label12.Visible = true;
                        label8.Visible = true;
                        txtstaffname.Visible = false;
                        lbl_staffname_tip.Visible = false;
                        btndrawersave.Visible = true;
                        pnl_Drawer.Location = new Point(this.ClientSize.Width / 2 - pnl_Drawer.Size.Width / 2, this.ClientSize.Height / 2 - pnl_Drawer.Size.Height / 2);
                        pnl_Drawer.Visible = true;

                    }
                    else
                    {

                        MessageBox.Show("Drawer Already Assigned on " + gridesource[0].Field3 + " , " + gridesource[0].Field4 + "  DateTime " + gridesource[0].Field10, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtdrawerid.Text = Convert.ToString(gridesource[0].Field5);
                        txtinitialamt.Text = Convert.ToString(gridesource[0].Field8);
                        txtinitialamt.ReadOnly = true;
                        txtextraaddamt.Text = Convert.ToString(gridesource[0].Field6);
                        txtdrops.Text = Convert.ToString(gridesource[0].Field7);
                        txtempoyeId.Text = Convert.ToString(gridesource[0].Field9);

                        txtextraaddamt_backup.Text = Convert.ToString(gridesource[0].Field6);
                        txtdrops_backup.Text = Convert.ToString(gridesource[0].Field7);

                        lbl_staffname.Text = Convert.ToString(gridesource[0].Field3);
                        lbl_desigtnation.Text = Convert.ToString(gridesource[0].Field4);
                        lbl_staffname.Visible = true;
                        txtdrops.Visible = true;
                        txtextraaddamt.Visible = true;
                        label12.Visible = true;
                        label12.Visible = true;
                        label8.Visible = true;
                        txtstaffname.Visible = false;
                        lbl_staffname_tip.Visible = false;
                        pnl_Drawer.Location = new Point(this.ClientSize.Width / 2 - pnl_Drawer.Size.Width / 2, this.ClientSize.Height / 2 - pnl_Drawer.Size.Height / 2);
                        pnl_Drawer.Visible = true;

                    }
                    pnl_Drawer.Visible = true;
                }
                else
                {
                    MessageBox.Show("No drawer has been assigned", "Drawer Not Assigned", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #region Small
                #endregion

                //    flagDrawerF = false; flagDrawer = false;
                //    BllSearch<DmnForms> bllacctype = new BllSearch<DmnForms>();
                //    DmnSearch dmnforms = new DmnSearch();
                //    dmnforms.Select = "*";
                //    dmnforms.Table = " tblforms ";
                //    dmnforms.WhereNdGroupBy = " where Form=2 and Status=1 and UserType=" + UserType;
                //    dmnforms.OrderBy = "order by Form";
                //    var gridesourcepermission = bllacctype.GetSearchData("prdSearch", dmnforms);
                //    if (gridesourcepermission.Count > 0)
                //    {
                //        for (int i = 0; i < gridesourcepermission.Count(); i++)
                //        {
                //            if (gridesourcepermission[i].Form == Convert.ToInt32(FrmDashboard.AllowCashDrawer) && gridesourcepermission[i].Status == 1)
                //            {
                //                lbl_staffname.Visible = true;
                //                txtstaffname.Visible = false;
                //                lbl_staffname.Text = FrmLogin.EmployeName;
                //                lbl_desigtnation.Text = FrmLogin.Designation;
                //                txtempoyeId.Text = Convert.ToString(FrmLogin.EmployeeId);
                //                lbl_staffname_tip.Visible = false;
                //                flagDrawer = true;

                //            }
                //            else if (gridesourcepermission[i].Form == Convert.ToInt32(FrmDashboard.AllowDrawerpermissionFullControl) && gridesourcepermission[i].Status == 1)
                //            {
                //                lbl_staffname.Visible = false;
                //                txtstaffname.Visible = true;
                //                lbl_desigtnation.Text = FrmLogin.Designation;
                //                lbl_staffname_tip.Visible = true;
                //                flagDrawerF = true;
                //            }
                //        }
                //    }
                //    label15.Text = System.Environment.MachineName;
                //    if (flagDrawer && flagDrawerF)
                //    {
                //        lbl_staffname.Visible = false;
                //        txtstaffname.Visible = true;
                //        lbl_desigtnation.Text = FrmLogin.Designation;
                //        lbl_staffname_tip.Visible = true;
                //    }

                //    txtdrops.Visible = false;
                //    txtextraaddamt.Visible = false;
                //    label12.Visible = false;
                //    label12.Visible = false;
                //    label8.Visible = false;

                //}
            }
            catch (Exception ex)
            {
                clsgen.LogException("Admin page AssigndrawerCheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
#endregion

        #region TaskBar Time
        private void timer1_Tick(object sender, EventArgs e)
        {
            //lbltime.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToShortDateString();
        }
        #endregion

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        public void Roundform(int size)
        {
            try
            {
                int borderRadius = size;

                Rectangle bounds = new Rectangle(0, 0, this.Width, this.Height);

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(bounds.Left, bounds.Top, borderRadius, borderRadius, 180, 90);
                    path.AddArc(bounds.Right - borderRadius, bounds.Top, borderRadius, borderRadius, 270, 90);
                    path.AddArc(bounds.Right - borderRadius, bounds.Bottom - borderRadius, borderRadius, borderRadius, 0, 90);
                    path.AddArc(bounds.Left, bounds.Bottom - borderRadius, borderRadius, borderRadius, 90, 90);
                    path.CloseFigure();

                    this.Region = new Region(path);
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("homepage Roundform()", ex, FrmLogin.EmployeName, FrmLogin.Designation, (int)FormType.homepage);
            }
        }



        private void chkentercolor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkentercolor.Checked)
            {
                btnappok.Visible = true;
                txtcolorcode.Visible = true;
            }
            else
            {
                btnappok.Visible = false;
                txtcolorcode.Visible = false;
            }
        }

        private void btnappcolse_Click(object sender, EventArgs e)
        {
            //tlp_main_sidebar_pnldesktop.Enabled = true;
            pnlappcolor.Visible = false;
        }

        private void btnappok_Click(object sender, EventArgs e)
        {
            try
            {
                DmnSetting dmnsettingr = new DmnSetting();
                BllSettins BllSettin = new BllSettins();
                if (string.IsNullOrWhiteSpace(txtsid.Text))
                    txtsid.Text = "0";
                dmnsettingr.SId = Convert.ToInt32(txtsid.Text);
                dmnsettingr.APPColor = txtappcolorname.Text;
                dmnsettingr.MenuButtonColor = txtmenubuttoncolorname.Text;
                dmnsettingr.OperationButtonColor = txtoperbuttoncolorname.Text;
                //dmnsettingr.Description = txtdesc.Text;
                dmnsettingr.Status = 1;
                dmnsettingr.BranchId = FrmDashboard.BranchId;
                dmnsettingr.CreatedBy = FrmDashboard.UserId;
                var InsertStatus = BllSettin.AddEditDataSetting("prdInsert_Upd_Setting", dmnsettingr);
                if (InsertStatus == null)
                {
                    CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //break;
                }
                if (InsertStatus != null)
                {
                    if (Convert.ToInt32(txtsid.Text) > 0)
                        CMessageBox.Show("Update Successfuly ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        CMessageBox.Show("Save Successfuly ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AppColorSetting();
                }
                pnlappcolor.Visible = false;
                //tlp_main_sidebar_pnldesktop.Enabled = true;

                //operation button color 
                ib_Category.BackColor = opbuttoncolor;
                ib_Item.BackColor = opbuttoncolor;
                ib_ingrediant.BackColor = opbuttoncolor;
                ib_menu.BackColor = opbuttoncolor;
                ib_unit.BackColor = opbuttoncolor;
                ib_table.BackColor = opbuttoncolor;
                ib_purchase.BackColor = opbuttoncolor;
                ib_purchasereturn.BackColor = opbuttoncolor;
                ib_tax.BackColor = opbuttoncolor;
                ib_vendor.BackColor = opbuttoncolor;
                ib_Discount_offer.BackColor = opbuttoncolor;
                ib_Expence.BackColor = opbuttoncolor;
                ib_orderview.BackColor = opbuttoncolor;
                ib_kitchenview.BackColor = opbuttoncolor;
                ib_PromoCode.BackColor = opbuttoncolor;
                ib_Company.BackColor = opbuttoncolor;
                ib_useraccount.BackColor = opbuttoncolor;
                ib_designation.BackColor = opbuttoncolor;
                ib_department.BackColor = opbuttoncolor;
                ib_employee.BackColor = opbuttoncolor;
                ib_reservation.BackColor = opbuttoncolor;
                ib_userpermission.BackColor = opbuttoncolor;
                ib_addon.BackColor = opbuttoncolor;

                Button btnsetting = this.Controls.Find("Appcolor", true).FirstOrDefault() as Button;
                if (btnsetting != null)
                    btnsetting.BackColor = opbuttoncolor;

                Button btnwhatsappsetting = this.Controls.Find("btn_Message", true).FirstOrDefault() as Button;
                if (btnwhatsappsetting != null)
                    btnwhatsappsetting.BackColor = opbuttoncolor;

                Button btnbillsetting = this.Controls.Find("btn_Bill", true).FirstOrDefault() as Button;
                if (btnbillsetting != null)
                    btnbillsetting.BackColor = opbuttoncolor;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  TableTurnoverReports_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }

        private void pbappcoloepicker_Click(object sender, EventArgs e)
        {
            try
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog1.Color;
                    pbappcoloepicker.BackColor = selectedColor;

                    string hexColor = ColorTranslator.ToHtml(selectedColor);
                    if (!hexColor.StartsWith("#"))
                    {
                        hexColor = "#" + selectedColor.R.ToString("X2") +
                                        selectedColor.G.ToString("X2") +
                                        selectedColor.B.ToString("X2");
                    }
                    txtappcolorname.Text = hexColor;
                    //if (pbappcoloepicker.BackColor.Name.Contains("ff"))
                    //    txtappcolorname.Text = "#" + pbappcoloepicker.BackColor.Name.ToString().Remove(0, 2);
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  TableTurnoverReports_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void ib_Category_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmFTICategory"] != null)
                {
                    FrmFTICategory _instanceFrmFTICategory = Application.OpenForms["FrmFTICategory"] as FrmFTICategory;
                    if (_instanceFrmFTICategory.WindowState == FormWindowState.Minimized)
                        _instanceFrmFTICategory.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmFTICategory.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmFTICategory);
                        panelDesktop.Controls.Add(_instanceFrmFTICategory);
                        _instanceFrmFTICategory.Show();
                        _instanceFrmFTICategory.BringToFront();
                    }
                }
                else
                {
                    FrmFTICategory _instanceFrmFTICategory = new FrmFTICategory();
                    ResizeForm(_instanceFrmFTICategory);
                    _instanceFrmFTICategory.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmFTICategory);
                    _instanceFrmFTICategory.Show();
                    _instanceFrmFTICategory.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmFTICategory.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  TableTurnoverReports_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }


        }

        private void ib_Item_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmItemRegistartion"] != null)
                {
                    FrmItemRegistartion _instanceItemRegistartion = Application.OpenForms["FrmItemRegistartion"] as FrmItemRegistartion;
                    if (_instanceItemRegistartion.WindowState == FormWindowState.Minimized)
                        _instanceItemRegistartion.WindowState = FormWindowState.Maximized;
                    else
                    {
                        _instanceItemRegistartion.Dock = DockStyle.None;
                        ResizeForm(_instanceItemRegistartion);
                        panelDesktop.Controls.Add(_instanceItemRegistartion);
                        _instanceItemRegistartion.Show();
                        _instanceItemRegistartion.BringToFront();
                    }
                }
                else
                {
                    FrmItemRegistartion _instanceItemRegistartion = new FrmItemRegistartion();
                    ResizeForm(_instanceItemRegistartion);
                    _instanceItemRegistartion.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceItemRegistartion);
                    _instanceItemRegistartion.Show();
                    _instanceItemRegistartion.WindowState = FormWindowState.Maximized;
                    _instanceItemRegistartion.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("homepage ib_Item_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, (int)FormType.homepage);
            }
        }

        private void ResizeForm(Form form)
        {
            //if (screenWidth > 1367 && screenHeight > 769)
            //{
            //    form.Height = 768;
            //    form.Width = 1367;pnl_products_descr
            //}
            //else
            form.Dock = DockStyle.Fill;
        }

        private void ib_menu_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmMenu"] != null)
                {
                    FrmMenu _instanceFrmMenu = Application.OpenForms["FrmMenu"] as FrmMenu;
                    if (_instanceFrmMenu.WindowState == FormWindowState.Minimized)
                        _instanceFrmMenu.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmMenu.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmMenu);
                        panelDesktop.Controls.Add(_instanceFrmMenu);
                        _instanceFrmMenu.Show();
                        _instanceFrmMenu.BringToFront();
                    }
                }
                else
                {
                    FrmMenu _instanceFrmMenu = new FrmMenu();
                    ResizeForm(_instanceFrmMenu);
                    _instanceFrmMenu.TopLevel = false;
                    _instanceFrmMenu.Dock = DockStyle.Fill;
                    panelDesktop.Controls.Add(_instanceFrmMenu);
                    _instanceFrmMenu.Show();
                    _instanceFrmMenu.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmMenu.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_menu_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }



        private void ib_ingrediant_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmIngredient"] != null)
                {
                    FrmIngredient _instanceFrmIngredient = Application.OpenForms["FrmIngredient"] as FrmIngredient;
                    if (_instanceFrmIngredient.WindowState == FormWindowState.Minimized)
                        _instanceFrmIngredient.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmIngredient.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmIngredient);
                        panelDesktop.Controls.Add(_instanceFrmIngredient);
                        _instanceFrmIngredient.Show();
                        _instanceFrmIngredient.BringToFront();
                    }
                }
                else
                {
                    FrmIngredient _instanceFrmIngredient = new FrmIngredient();
                    ResizeForm(_instanceFrmIngredient);
                    _instanceFrmIngredient.TopLevel = false;
                    _instanceFrmIngredient.Dock = DockStyle.Fill;
                    panelDesktop.Controls.Add(_instanceFrmIngredient);
                    _instanceFrmIngredient.Show();
                    _instanceFrmIngredient.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmIngredient.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_ingrediant_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void ib_unit_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmUnit"] != null)
                {
                    FrmUnit _instanceFrmUnit = Application.OpenForms["FrmUnit"] as FrmUnit;
                    if (_instanceFrmUnit.WindowState == FormWindowState.Minimized)
                        _instanceFrmUnit.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmUnit.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmUnit);
                        panelDesktop.Controls.Add(_instanceFrmUnit);
                        _instanceFrmUnit.Show();
                        _instanceFrmUnit.BringToFront();
                    }
                }
                else
                {
                    FrmUnit _instanceFrmUnit = new FrmUnit();
                    ResizeForm(_instanceFrmUnit);
                    _instanceFrmUnit.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmUnit);
                    _instanceFrmUnit.Show();
                    _instanceFrmUnit.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmUnit.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_unit_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void ib_table_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmTableInfo"] != null)
                {
                    FrmTableInfo _instanceTableInfo = Application.OpenForms["FrmTableInfo"] as FrmTableInfo;
                    if (_instanceTableInfo.WindowState == FormWindowState.Minimized)
                        _instanceTableInfo.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceTableInfo.Dock = DockStyle.None;
                        ResizeForm(_instanceTableInfo);
                        panelDesktop.Controls.Add(_instanceTableInfo);
                        _instanceTableInfo.Show();
                        _instanceTableInfo.BringToFront();
                    }
                }
                else
                {
                    FrmTableInfo _instanceTableInfo = new FrmTableInfo();
                    ResizeForm(_instanceTableInfo);
                    _instanceTableInfo.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceTableInfo);
                    _instanceTableInfo.Show();
                    _instanceTableInfo.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceTableInfo.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_table_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
        private void ib_purchase_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmPurchase"] != null)
                {
                    FrmPurchase _instanceFrmPurchase = Application.OpenForms["FrmPurchase"] as FrmPurchase;
                    if (_instanceFrmPurchase.WindowState == FormWindowState.Minimized)
                        _instanceFrmPurchase.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmPurchase.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmPurchase);
                        panelDesktop.Controls.Add(_instanceFrmPurchase);
                        _instanceFrmPurchase.Show();
                        _instanceFrmPurchase.BringToFront();
                    }
                }
                else
                {
                    FrmPurchase _instanceFrmPurchase = new FrmPurchase();
                    ResizeForm(_instanceFrmPurchase);
                    _instanceFrmPurchase.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmPurchase);
                    _instanceFrmPurchase.Show();
                    _instanceFrmPurchase.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmPurchase.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_purchase_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
        private void ib_purchasereturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmPurchaseReturn"] != null)
                {
                    FrmPurchaseReturn _instancePurchaseReturn = Application.OpenForms["FrmPurchaseReturn"] as FrmPurchaseReturn;
                    if (_instancePurchaseReturn.WindowState == FormWindowState.Minimized)
                        _instancePurchaseReturn.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instancePurchaseReturn.Dock = DockStyle.None;
                        ResizeForm(_instancePurchaseReturn);
                        panelDesktop.Controls.Add(_instancePurchaseReturn);
                        _instancePurchaseReturn.Show();
                        _instancePurchaseReturn.BringToFront();
                    }
                }
                else
                {
                    FrmPurchaseReturn _instancePurchaseReturn = new FrmPurchaseReturn();
                    _instancePurchaseReturn.Dock = DockStyle.None;
                    ResizeForm(_instancePurchaseReturn);
                    _instancePurchaseReturn.TopLevel = false;
                    panelDesktop.Controls.Add(_instancePurchaseReturn);
                    _instancePurchaseReturn.Show();
                    _instancePurchaseReturn.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instancePurchaseReturn.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_purchasereturn_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
        private void ib_tax_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmTaxSetting"] != null)
                {
                    FrmTaxSetting _instanceTaxSetting = Application.OpenForms["FrmTaxSetting"] as FrmTaxSetting;
                    if (_instanceTaxSetting.WindowState == FormWindowState.Minimized)
                        _instanceTaxSetting.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceTaxSetting.Dock = DockStyle.None;
                        ResizeForm(_instanceTaxSetting);
                        panelDesktop.Controls.Add(_instanceTaxSetting);
                        _instanceTaxSetting.Show();
                        _instanceTaxSetting.BringToFront();
                    }
                }
                else
                {
                    FrmTaxSetting _instanceTaxSetting = new FrmTaxSetting();
                    ResizeForm(_instanceTaxSetting);
                    _instanceTaxSetting.TopLevel = false;
                    _instanceTaxSetting.Dock = DockStyle.None;
                    panelDesktop.Controls.Add(_instanceTaxSetting);
                    _instanceTaxSetting.Show();
                    _instanceTaxSetting.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceTaxSetting.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_tax_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void ib_Category_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button == null) return;

                button.Text = string.Empty;
                button.ImageAlign = ContentAlignment.MiddleCenter;

                //Button button = (Button)sender;
                //button.Text = string.Empty;
                //Image originalImage = null;
                //switch (button.Name)
                //{
                //    case "ib_Category":
                //        originalImage = FoodandBeverage.Properties.Resources.category;
                //        break;

                //    case "ib_Item":
                //        originalImage = FoodandBeverage.Properties.Resources.item;
                //        break;

                //    case "ib_Ingredient":
                //        originalImage = FoodandBeverage.Properties.Resources.Product;
                //        break;

                //    case "ib_menu":
                //        originalImage = FoodandBeverage.Properties.Resources.menu;
                //        break;

                //    case "ib_unit":
                //        originalImage = FoodandBeverage.Properties.Resources.unit;
                //        break;

                //    case "ib_table":
                //        originalImage = FoodandBeverage.Properties.Resources.table_reg;
                //        break;

                //    case "ib_purchase":
                //        originalImage = FoodandBeverage.Properties.Resources.Purchase;
                //        break;

                //    case "ib_purchasereturn":
                //        originalImage = FoodandBeverage.Properties.Resources.Purchase_return;
                //        break;

                //    case "ib_tax":
                //        originalImage = FoodandBeverage.Properties.Resources.tax;
                //        break;
                //    case "ib_vendor":
                //        originalImage = FoodandBeverage.Properties.Resources.vendericon;
                //        break;
                //    case "ib_withdrawal":
                //        originalImage = FoodandBeverage.Properties.Resources.withdraw;
                //        break;
                //    case "ib_Discount":
                //        originalImage = FoodandBeverage.Properties.Resources.discount;
                //        break;
                //    case "ib_orderview":
                //        originalImage = FoodandBeverage.Properties.Resources.orderview;
                //        break;
                //    case "ib_kitchenview":
                //        originalImage = FoodandBeverage.Properties.Resources.kitchen_order_view;
                //        break;
                //    case "ib_PromoCode":
                //        originalImage = FoodandBeverage.Properties.Resources.promo_code;
                //        break;
                //    case "ib_Company":
                //        originalImage = FoodandBeverage.Properties.Resources.comapny;
                //        break;
                //    case "ib_useraccount":
                //        originalImage = FoodandBeverage.Properties.Resources.add_des_dep_user;
                //        break;
                //    case "ib_designation":
                //        originalImage = FoodandBeverage.Properties.Resources.add_des_dep_user;
                //        break;
                //    case "ib_department":
                //        originalImage = FoodandBeverage.Properties.Resources.add_des_dep_user;
                //        break;
                //    case "ib_employee":
                //        originalImage = FoodandBeverage.Properties.Resources.Employee;
                //        break;
                //    case "ib_reservation":
                //        originalImage = FoodandBeverage.Properties.Resources.reserved;
                //        break;
                //    case "ib_userpermission":
                //        originalImage = FoodandBeverage.Properties.Resources.userpermisson;
                //        break;
                //    case "ib_addon":
                //        originalImage = FoodandBeverage.Properties.Resources.addon;
                //        break;
                //}


                //if (originalImage != null)
                //{
                //    button.BackgroundImage = null;
                //    Image resizedImage = new Bitmap(originalImage, new Size(HdesiredWidth, HdesiredHeight));
                //    button.Image = resizedImage;
                //    //button.Image = ImageLayout.Center;
                //}
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_Category_MouseHover()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }
        private void OPButton_MouseLeftEffect(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button == null) return;

                if (button.Tag != null)
                    button.Text = button.Tag.ToString();
                else
                    button.Text = string.Empty;

                button.TextAlign = ContentAlignment.BottomCenter;
                button.ImageAlign = ContentAlignment.TopCenter;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage OPButton_MouseLeftEffect()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }



        private void ib_vendor_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmVendor"] != null)
                {
                    FrmVendor _instanceFrmVendor = Application.OpenForms["FrmVendor"] as FrmVendor;
                    if (_instanceFrmVendor.WindowState == FormWindowState.Minimized)
                        _instanceFrmVendor.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmVendor.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmVendor);
                        panelDesktop.Controls.Add(_instanceFrmVendor);
                        _instanceFrmVendor.Show();
                        _instanceFrmVendor.BringToFront();
                    }
                }
                else
                {
                    FrmVendor _instanceFrmVendor = new FrmVendor();
                    ResizeForm(_instanceFrmVendor);
                    _instanceFrmVendor.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmVendor);
                    _instanceFrmVendor.Show();
                    _instanceFrmVendor.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmVendor.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_vendor_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:

                    if (pnlappcolor.Visible)
                    {
                        //tlp_main_sidebar_pnldesktop.Enabled = true;
                        pnlappcolor.Visible = false;
                    }
                    else if (panl_msgsettings.Visible)
                    {
                        //tlp_main_sidebar_pnldesktop.Enabled = true;
                        panl_msgsettings.Visible = false;
                    }
                    else if (pnl_billsetting.Visible)
                    {
                        //tlp_main_sidebar_pnldesktop.Enabled = true;
                        pnl_billsetting.Visible = false;
                    }
                    else
                        Closeform();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        public void Closeform()
        {
            try
            {
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm.Visible && (openForm.WindowState == FormWindowState.Normal || openForm.WindowState == FormWindowState.Maximized))
                    {
                        string formName = openForm.GetType().Name;

                        List<string> formsToCheck = new List<string>
                {
                    "FrmDepartment", "FrmDesignation", "FrmEmployeeShort", "FrmFTICategory",
                    "FrmIngredient", "FrmItemRegistartion", "Frmkeypad", "FrmKitchenView",
                    "FrmMenu", "FrmOfferDiscount", "FrmOrder", "FrmOrderView", "FrmPromo",
                    "FrmPurchase", "FrmPurchaseReturn", "FrmRegistration", "FrmReservation",
                    "FrmTableInfo", "FrmTaxSetting", "FrmUnit", "FrmUserAccount",
                    "FrmUserPermission", "FrmVendor", "FrmExpence", "FrmAddon"
                };

                        if (formsToCheck.Contains(formName))
                        {
                            openForm.Close();
                            openForm.Dispose();
                            break; // Exit loop after closing the first matching form
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage Closeform()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }


        private void ib_Discount_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmOfferDiscount"] != null)
                {
                    FrmOfferDiscount _instanceFrmOfferDiscount = Application.OpenForms["FrmOfferDiscount"] as FrmOfferDiscount;
                    if (_instanceFrmOfferDiscount.WindowState == FormWindowState.Minimized)
                        _instanceFrmOfferDiscount.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmOfferDiscount.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmOfferDiscount);
                        panelDesktop.Controls.Add(_instanceFrmOfferDiscount);
                        _instanceFrmOfferDiscount.Show();
                        _instanceFrmOfferDiscount.BringToFront();
                    }
                }
                else
                {
                    FrmOfferDiscount _instanceFrmOfferDiscount = new FrmOfferDiscount();
                    ResizeForm(_instanceFrmOfferDiscount);
                    _instanceFrmOfferDiscount.TopLevel = false;
                    _instanceFrmOfferDiscount.Dock = DockStyle.None;
                    panelDesktop.Controls.Add(_instanceFrmOfferDiscount);
                    _instanceFrmOfferDiscount.Show();
                    _instanceFrmOfferDiscount.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmOfferDiscount.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_Discount_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }


        private void ib_withdrawal_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmExpence"] != null)
                {
                    FrmExpence _instanceFrmexpence = Application.OpenForms["FrmExpence"] as FrmExpence;
                    if (_instanceFrmexpence.WindowState == FormWindowState.Minimized)
                        _instanceFrmexpence.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmexpence.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmexpence);
                        panelDesktop.Controls.Add(_instanceFrmexpence);
                        _instanceFrmexpence.Show();
                        _instanceFrmexpence.BringToFront();
                    }
                }
                else
                {
                    FrmExpence _instanceFrmexpence = new FrmExpence();
                    ResizeForm(_instanceFrmexpence);
                    _instanceFrmexpence.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmexpence);
                    _instanceFrmexpence.Show();
                    _instanceFrmexpence.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_withdrawal_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void ib_orderview_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                {
                    FrmOrderView _instanceFrmorderview = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                    if (_instanceFrmorderview.WindowState == FormWindowState.Minimized)
                    {
                        _instanceFrmorderview.WindowState = System.Windows.Forms.FormWindowState.Maximized;

                        if (_instanceFrmorderview.pnl_details_view.Visible != true)
                            _instanceFrmorderview.refreshtimer_sub.Start();

                        if (_instanceFrmorderview.pnl_ordergallery.Visible != true)                        
                            _instanceFrmorderview.refreshtimer_main.Start();                        
                    }
                    else
                    {
                        _instanceFrmorderview.Show();
                        _instanceFrmorderview.BringToFront();
                        _instanceFrmorderview.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    }
                }
                else
                {
                    FrmOrderView _instanceFrmorderview = new FrmOrderView();
                    _instanceFrmorderview.Show();
                    _instanceFrmorderview.BringToFront();
                    _instanceFrmorderview.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_orderview_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void ib_kitchenview_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmKitchenView"] != null)
                {
                    FrmKitchenView _instanceFrmKitchenView = Application.OpenForms["FrmKitchenView"] as FrmKitchenView;
                    if (_instanceFrmKitchenView.WindowState == FormWindowState.Minimized)
                        _instanceFrmKitchenView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmKitchenView.Show();
                        _instanceFrmKitchenView.BringToFront();
                        _instanceFrmKitchenView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    }
                }
                else
                {
                    FrmKitchenView _instanceFrmKitchenView = new FrmKitchenView();
                    _instanceFrmKitchenView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmKitchenView.Show();
                    _instanceFrmKitchenView.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_kitchenview_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }

        private void ib_PromoCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmPromo"] != null)
                {
                    FrmPromo _instanceFrmPromo = Application.OpenForms["FrmPromo"] as FrmPromo;
                    if (_instanceFrmPromo.WindowState == FormWindowState.Minimized)
                        _instanceFrmPromo.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmPromo.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmPromo);
                        panelDesktop.Controls.Add(_instanceFrmPromo);
                        _instanceFrmPromo.Show();
                        _instanceFrmPromo.BringToFront();
                    }
                }
                else
                {
                    FrmPromo _instanceFrmPromo = new FrmPromo();
                    ResizeForm(_instanceFrmPromo);
                    _instanceFrmPromo.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmPromo);
                    _instanceFrmPromo.Show();
                    _instanceFrmPromo.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmPromo.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_PromoCode_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }

        private void pbopbuttoncoloepicker_Click(object sender, EventArgs e)
        {
            try
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog1.Color;
                    menubuttoncolor = selectedColor;
                    pbsidebuttoncoloepicker.BackColor = selectedColor;

                    string hexColor = ColorTranslator.ToHtml(selectedColor);

                    if (!hexColor.StartsWith("#"))
                    {
                        hexColor = "#" + selectedColor.R.ToString("X2") +
                                        selectedColor.G.ToString("X2") +
                                        selectedColor.B.ToString("X2");
                    }
                    txtmenubuttoncolorname.Text = hexColor;

                    //if (pbsidebuttoncoloepicker.BackColor.Name.Contains("ff"))
                    //    txtmenubuttoncolorname.Text = "#" + pbsidebuttoncoloepicker.BackColor.Name.ToString().Remove(0, 2);

                    //Menuhome.BackColor = menubuttoncolor;
                    //Menuoperation.BackColor = menubuttoncolor;
                    //Menubilling.BackColor = menubuttoncolor;
                    //Menureport.BackColor = menubuttoncolor;
                    //Menulogout.BackColor = menubuttoncolor;
                    //Menusetting.BackColor = menubuttoncolor;
                    //Menutable.BackColor = menubuttoncolor;
                    //menuEmployee.BackColor = menubuttoncolor;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage pbopbuttoncoloepicker_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }

        private void pbopbuttoncoloepicker_Click_1(object sender, EventArgs e)
        {

            try
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog1.Color;
                    opbuttoncolor = selectedColor;
                    pbopbuttoncoloepicker.BackColor = selectedColor;

                    string hexColor = ColorTranslator.ToHtml(selectedColor);

                    if (!hexColor.StartsWith("#"))
                    {
                        hexColor = "#" + selectedColor.R.ToString("X2") +
                                        selectedColor.G.ToString("X2") +
                                        selectedColor.B.ToString("X2");
                    }                     
                    txtoperbuttoncolorname.Text = hexColor;                       
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage pbopbuttoncoloepicker_Click_1", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }

        private void btn_minimium_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void chkdef_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkdef.Checked)
                {
                    opbuttoncolor = Color.FromArgb(0x42, 0x67, 0xB2); ;
                    pbopbuttoncoloepicker.BackColor = opbuttoncolor;
                    ib_Category.BackColor = opbuttoncolor;
                    ib_Item.BackColor = opbuttoncolor;
                    ib_ingrediant.BackColor = opbuttoncolor;
                    ib_menu.BackColor = opbuttoncolor;
                    ib_unit.BackColor = opbuttoncolor;
                    ib_table.BackColor = opbuttoncolor;
                    ib_purchase.BackColor = opbuttoncolor;
                    ib_purchasereturn.BackColor = opbuttoncolor;
                    ib_tax.BackColor = opbuttoncolor;
                    ib_vendor.BackColor = opbuttoncolor;
                    ib_Discount_offer.BackColor = opbuttoncolor;
                    ib_Expence.BackColor = opbuttoncolor;
                    ib_orderview.BackColor = opbuttoncolor;
                    ib_kitchenview.BackColor = opbuttoncolor;
                    ib_PromoCode.BackColor = opbuttoncolor;
                    ib_Company.BackColor = opbuttoncolor;
                    ib_useraccount.BackColor = opbuttoncolor;
                    ib_designation.BackColor = opbuttoncolor;
                    ib_department.BackColor = opbuttoncolor;
                    ib_employee.BackColor = opbuttoncolor;
                    ib_reservation.BackColor = opbuttoncolor;
                    ib_userpermission.BackColor = opbuttoncolor;
                    ib_addon.BackColor = opbuttoncolor;

                    menubuttoncolor = Color.FromArgb(0x42, 0x67, 0xB2); ;
                    pbsidebuttoncoloepicker.BackColor = menubuttoncolor;
                    //Menuhome.BackColor = menubuttoncolor;
                    //Menuoperation.BackColor = menubuttoncolor;
                    //Menubilling.BackColor = menubuttoncolor;
                    //Menureport.BackColor = menubuttoncolor;
                    //Menulogout.BackColor = menubuttoncolor;
                    //Menusetting.BackColor = menubuttoncolor;
                    //menuEmployee.BackColor = menubuttoncolor;
                    //Menutable.BackColor = menubuttoncolor;

                    pbappcoloepicker.BackColor = Color.FromArgb(34, 46, 80);

                    Button btnsetting = this.Controls.Find("Appcolor", true).FirstOrDefault() as Button;
                    if (btnsetting != null)
                        btnsetting.BackColor = opbuttoncolor;

                    Button btnwhatsappsetting = this.Controls.Find("btn_Message", true).FirstOrDefault() as Button;
                    if (btnwhatsappsetting != null)
                        btnwhatsappsetting.BackColor = opbuttoncolor;


                    Button btnbillsetting = this.Controls.Find("btn_Bill", true).FirstOrDefault() as Button;
                    if (btnbillsetting != null)
                        btnbillsetting.BackColor = opbuttoncolor;
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage chkdef_CheckedChanged", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
        private void ib_Company_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmRegistration"] != null)
                {
                    FrmRegistration _instanceFrmreg = Application.OpenForms["FrmRegistration"] as FrmRegistration;
                    if (_instanceFrmreg.WindowState == FormWindowState.Minimized)
                        _instanceFrmreg.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmreg.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmreg);
                        panelDesktop.Controls.Add(_instanceFrmreg);
                        _instanceFrmreg.Show();
                        _instanceFrmreg.BringToFront();
                    }
                }
                else
                {
                    FrmRegistration _instanceFrmreg = new FrmRegistration();
                    _instanceFrmreg.Margin = new Padding(20, 10, 0, 0);
                    _instanceFrmreg.TopLevel = false;
                    _instanceFrmreg.Dock = DockStyle.None;
                    panelDesktop.Controls.Add(_instanceFrmreg);
                    _instanceFrmreg.Show();
                    _instanceFrmreg.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmreg.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_Company_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void menuEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnl_Drawer.Visible)
                    pnl_Drawer.Visible = false;
                ButtonID = Convert.ToInt32(Menubutton.MenuEmployee);
                time_syn_lal.Stop();
                time_syn_lal.Stop();
                lblsync.Visible = false;
                reset_sync.Stop();
                if (menutable_click)
                {
                    CloseTableViewPage();
                }

                panelDesktop.Controls.Clear();
                flp_empbuttons.Dock = DockStyle.Fill;
                panelDesktop.Controls.Add(flp_empbuttons);
                flp_empbuttons.Visible = true;
                btn_pickup.Visible = false;

                //if (screenWidth > 1367 && screenHeight > 769)
                //{
                //    foreach (Control control in flpEmployee.Controls)
                //    {
                //        if (control is Button buttonm)
                //        {
                //            buttonm.Size = new Size(300, 120);
                //            buttonm.Margin = new Padding(105, 35, 10, 2);
                //            buttonm.Font = new Font(buttonm.Font.FontFamily, 16, buttonm.Font.Style);
                //        }
                //    }
                //}
                //else
                //{
                //    foreach (Control control in flpEmployee.Controls)
                //    {
                //        if (control is Button buttonm)
                //        {
                //            buttonm.Size = new Size(212, 89);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage menuEmployee_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }

        private void ib_employee_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmEmployeeShort"] != null)
                {
                    FrmEmployeeShort _instanceFrmEmp = Application.OpenForms["FrmEmployeeShort"] as FrmEmployeeShort;
                    if (_instanceFrmEmp.WindowState == FormWindowState.Minimized)
                        _instanceFrmEmp.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmEmp.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmEmp);
                        panelDesktop.Controls.Add(_instanceFrmEmp);
                        _instanceFrmEmp.Show();
                        _instanceFrmEmp.BringToFront();
                    }
                }
                else
                {
                    FrmEmployeeShort _instanceFrmEmp = new FrmEmployeeShort();
                    ResizeForm(_instanceFrmEmp);
                    _instanceFrmEmp.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmEmp);
                    _instanceFrmEmp.Show();
                    _instanceFrmEmp.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmEmp.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_employee_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }


        }

        private void ib_department_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmDepartment"] != null)
                {
                    FrmDepartment _instanceFrmDep = Application.OpenForms["FrmDepartment"] as FrmDepartment;
                    if (_instanceFrmDep.WindowState == FormWindowState.Minimized)
                        _instanceFrmDep.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmDep.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmDep);
                        panelDesktop.Controls.Add(_instanceFrmDep);
                        _instanceFrmDep.Show();
                        _instanceFrmDep.BringToFront();
                    }
                }
                else
                {
                    FrmDepartment _instanceFrmDep = new FrmDepartment();
                    ResizeForm(_instanceFrmDep);
                    _instanceFrmDep.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmDep);
                    _instanceFrmDep.Show();
                    _instanceFrmDep.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmDep.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_department_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }

        private void ib_designation_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmDesignation"] != null)
                {
                    FrmDesignation _instanceFrmDesig = Application.OpenForms["FrmDesignation"] as FrmDesignation;
                    if (_instanceFrmDesig.WindowState == FormWindowState.Minimized)
                        _instanceFrmDesig.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmDesig.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmDesig);
                        panelDesktop.Controls.Add(_instanceFrmDesig);
                        _instanceFrmDesig.Show();
                        _instanceFrmDesig.BringToFront();
                    }
                }
                else
                {
                    FrmDesignation _instanceFrmDesig = new FrmDesignation();
                    ResizeForm(_instanceFrmDesig);
                    _instanceFrmDesig.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmDesig);
                    _instanceFrmDesig.Show();
                    _instanceFrmDesig.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmDesig.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_designation_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }

        private void ib_useraccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmUserAccount"] != null)
                {
                    FrmUserAccount _instanceFrmuser = Application.OpenForms["FrmUserAccount"] as FrmUserAccount;
                    if (_instanceFrmuser.WindowState == FormWindowState.Minimized)
                        _instanceFrmuser.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmuser.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmuser);
                        panelDesktop.Controls.Add(_instanceFrmuser);
                        _instanceFrmuser.Show();
                        _instanceFrmuser.BringToFront();
                    }
                }
                else
                {
                    FrmUserAccount _instanceFrmuser = new FrmUserAccount();
                    ResizeForm(_instanceFrmuser);
                    _instanceFrmuser.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmuser);
                    _instanceFrmuser.Show();
                    _instanceFrmuser.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmuser.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_useraccount_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }

        private void ib_reservation_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmReservation"] != null)
                {
                    FrmReservation _instanceFrmres = Application.OpenForms["FrmReservation"] as FrmReservation;
                    if (_instanceFrmres.WindowState == FormWindowState.Minimized)
                        _instanceFrmres.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmres.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmres);
                        panelDesktop.Controls.Add(_instanceFrmres);
                        _instanceFrmres.Show();
                        _instanceFrmres.BringToFront();
                    }
                }
                else
                {
                    FrmReservation _instanceFrmres = new FrmReservation();
                    ResizeForm(_instanceFrmres);
                    _instanceFrmres.TopLevel = false;
                    _instanceFrmres.Dock = DockStyle.None;
                    panelDesktop.Controls.Add(_instanceFrmres);
                    _instanceFrmres.Show();
                    _instanceFrmres.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmres.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_reservation_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }



        private void ib_userpermission_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmUserPermission"] != null)
                {
                    FrmUserPermission _instanceFrmuserper = Application.OpenForms["FrmUserPermission"] as FrmUserPermission;
                    if (_instanceFrmuserper.WindowState == FormWindowState.Minimized)
                        _instanceFrmuserper.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmuserper.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmuserper);
                        panelDesktop.Controls.Add(_instanceFrmuserper);
                        _instanceFrmuserper.Show();
                        _instanceFrmuserper.BringToFront();
                    }
                }
                else
                {
                    FrmUserPermission _instanceFrmuserper = new FrmUserPermission();
                    ResizeForm(_instanceFrmuserper);
                    _instanceFrmuserper.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmuserper);
                    _instanceFrmuserper.Show();
                    _instanceFrmuserper.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmuserper.BringToFront();
                }
            }
            catch (Exception ex)
            {

                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Homepage ib_userpermission_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));

            }
        }

        private void pb_appicon_Click(object sender, EventArgs e)
        {
            //ToggleSideMenu();
        }

        private void FrmDashboard_Resize(object sender, EventArgs e)
        {
            try
            {
                AdjustButtonsToPanel();
                int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = Screen.PrimaryScreen.Bounds.Height;

                //UIHelper.RoundCorners(flp_sidemenu, 25, ColorTranslator.FromHtml("#AC3CC6"));
                //UIHelper.RoundCorners(pnl_navbar, 25, ColorTranslator.FromHtml("#AC3CC6"));
                //UIHelper.RoundCorners(panelDesktop, 25, ColorTranslator.FromHtml("#AC3CC6"));
                //UIHelper.RoundCorners(pnl_footer, 25, ColorTranslator.FromHtml("#AC3CC6"));
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("homepage FrmDashboard_Resize", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void AdjustButtonsToPanel()
        {
            if (panelDesktop == null || panelDesktop.Width == 0)
                return;

            int panelWidth = panelDesktop.ClientSize.Width;
            int columns = 4;
            int marginLeftRight = 40;
            int marginTopBottom = 12;

            int totalHorizontalMargin = (marginLeftRight * 2 * columns);
            int availableWidth = panelWidth - totalHorizontalMargin;

            columns = screenWidth > 1366 ? 3 : 4;
            lblinact_timer.Text = columns+" "+ screenWidth.ToString();
            int buttonWidth = availableWidth / columns;  ;

            //int buttonHeight = (int)(buttonWidth * 0.35);
            int buttonHeight = (int)(Menuhome.Height * 0.92);

            ResizeButtonsInFlow(flp_empbuttons, buttonWidth, buttonHeight, marginLeftRight, marginTopBottom);
            ResizeButtonsInFlow(flp_operabuttons, buttonWidth, buttonHeight, marginLeftRight, marginTopBottom);
            ResizeButtonsInFlow(flp_settingbtn, buttonWidth, buttonHeight, marginLeftRight, marginTopBottom);

        }

        private void ResizeButtonsInFlow(FlowLayoutPanel flow, int width, int height, int marginX, int marginY)
        {
            if (flow == null) return;

            flow.WrapContents = true;
            flow.AutoSize = false;
            flow.FlowDirection = FlowDirection.LeftToRight;
            flow.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            foreach (Control ctrl in flow.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Width = width;
                    btn.Height = height;

                    // Margins
                    btn.Margin = new Padding(marginX, marginY, marginX, marginY);

                    // Adjust font size to match smaller height
                    float fontSize = Math.Max(9f, width / 18f);
                    btn.Font = AppFonts.GetFont(12);
                }
            }
        }

        public void FormPermissionChecking()
        {
            try
            {
                BllSearch<DmnForms> bllacctype = new BllSearch<DmnForms>();
                DmnSearch dmnforms = new DmnSearch();
                dmnforms.Select = "*";
                dmnforms.Table = " tblforms ";
                dmnforms.WhereNdGroupBy = " where  Status=1 and UserType=" + UserType;
                dmnforms.OrderBy = "order by Form";
                var gridesource = bllacctype.GetSearchData("prdSearch", dmnforms);
                if (gridesource.Count > 0)
                {
                    for (int i = 0; i < gridesource.Count(); i++)
                    {
                        #region Order Table
                        if (gridesource[i].Form == Convert.ToInt32(formsenum.Order) && gridesource[i].Write == 1)
                        {
                            Menutable.Visible = true;
                        }
                        #endregion

                        #region Operation Buttons
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Kitchen_View) && gridesource[i].Write == 1)
                        {
                            ib_kitchenview.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Category) && gridesource[i].Write == 1)
                        {
                            ib_Category.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Item) && gridesource[i].Write == 1)
                        {
                            ib_Item.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Product) && gridesource[i].Write == 1)
                        {
                            ib_ingrediant.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Menu) && gridesource[i].Write == 1)
                        {
                            ib_menu.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Unit) && gridesource[i].Write == 1)
                        {
                            ib_unit.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Table) && gridesource[i].Write == 1)
                        {
                            ib_table.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Purchase) && gridesource[i].Write == 1)
                        {
                            ib_purchase.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Purchase_Return) && gridesource[i].Write == 1)
                        {
                            ib_purchasereturn.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Tax) && gridesource[i].Write == 1)
                        {
                            ib_tax.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Vendor) && gridesource[i].Write == 1)
                        {
                            ib_vendor.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Discount_And_Offer) && gridesource[i].Write == 1)
                        {
                            ib_Discount_offer.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Expence) && gridesource[i].Write == 1)
                        {
                            ib_Expence.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.PromoCode) && gridesource[i].Write == 1)
                        {
                            ib_PromoCode.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Company) && gridesource[i].Write == 1)
                        {
                            ib_Company.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Reservation) && gridesource[i].Write == 1)
                        {
                            ib_reservation.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.OrderView) && gridesource[i].Write == 1)
                        {
                            ib_orderview.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Addon) && gridesource[i].Write == 1)
                        {
                            ib_addon.Visible = true;
                            Menuoperation.Visible = true;
                        }
                        #endregion

                        #region Employee Buttons
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.User_Permission) && gridesource[i].Write == 1)
                        {
                            menuEmployee.Visible = true;
                            ib_userpermission.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.User_Account) && gridesource[i].Write == 1)
                        {
                            menuEmployee.Visible = true;
                            ib_useraccount.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Designation) && gridesource[i].Write == 1)
                        {
                            menuEmployee.Visible = true;
                            ib_designation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Department) && gridesource[i].Write == 1)
                        {
                            menuEmployee.Visible = true;
                            ib_department.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Employee) && gridesource[i].Write == 1)
                        {
                            menuEmployee.Visible = true;
                            ib_employee.Visible = true;
                        }
                        #endregion

                        #region Billing
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Billing) && gridesource[i].Write == 1)
                        {
                            Menubilling.Visible = true;
                        }
                        #endregion

                        #region Setting
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Settings) && gridesource[i].Write == 1)
                        {
                            Menusetting.Visible = true;

                            ib_msgsetting.Visible = true;
                            ib_virtualkeybpard.Visible = true;
                            ib_gensetting.Visible = true;
                            ib_billsetting.Visible = true;
                            ib_appcolor.Visible = true;
                            ib_drawer.Visible = true;
                        }
                        #endregion

                        #region AllowCashDrawer
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.AllowCashDrawer) && gridesource[i].Write == 1)
                        {
                            AllowCashDrawer = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.AllowDrawerpermissionFullControl) && gridesource[i].Write == 1)
                        {
                            AllowDrawerpermissionFullControl = true;
                        }
                        #endregion

                        #region Report
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.report) && gridesource[i].Write == 1)
                        {
                            Menureport.Visible = true;
                        }
                        #endregion
                    }

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("homepage FormPermissionChecking", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }


        public bool ChkWritePermission(int formid)
        {
            try
            {
                BllSearch<DmnForms> bllacctype = new BllSearch<DmnForms>();
                DmnSearch dmnforms = new DmnSearch();
                dmnforms.Select = "*";
                dmnforms.Table = " tblforms ";
                dmnforms.WhereNdGroupBy = " where UserType=" + UserType + " and Status=1 and Form=" + formid;
                dmnforms.OrderBy = "order by Form";
                var gridesource = bllacctype.GetSearchData("prdSearch", dmnforms);

                if (gridesource.Count > 0)
                {
                    return gridesource[0].Write == 1;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("homepage ChkWritePermission()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
                return false;
            }
        }


        private void btn_save_msg_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtsid.Text))
                    txtsid.Text = "0";

                DmnSetting dmnmsg = new DmnSetting();
                BllSettins bllmessage = new BllSettins();

                dmnmsg.SId = Convert.ToInt32(txtsid.Text);
                dmnmsg.Whatsapp = chk_whatsapp.Checked;
                dmnmsg.Gmail = chk_gmail.Checked;
                dmnmsg.BranchId = FrmDashboard.BranchId;
                dmnmsg.CreatedBy = FrmDashboard.UserId;
                dmnmsg.Status = 1;

                var InsertStatus = bllmessage.AddEditDataSetting_msg("prdInsertMessage", dmnmsg);

                if (InsertStatus.SId > 0)
                {
                    if (Convert.ToInt32(txtsid.Text) > 0)
                        CMessageBox.Show(this, "Update successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        CMessageBox.Show(this, "Insert successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtsid.Text = Convert.ToString(InsertStatus.SId);
                    }
                    panl_msgsettings.Visible = false;
                }
                else
                {
                    CMessageBox.Show(this, "Operation failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //tlp_main_sidebar_pnldesktop.Enabled = true;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("homepage btn_save_msg_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }


        private void btn_close_msg_Click(object sender, EventArgs e)
        {
            panl_msgsettings.Visible = false;
            //tlp_main_sidebar_pnldesktop.Enabled = true;
        }

        private void Menutable_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnl_Drawer.Visible)
                    pnl_Drawer.Visible = false;
                ButtonID = Convert.ToInt32(Menubutton.Menutable);
                if (System.Windows.Forms.Application.OpenForms["FrmDashboardReport"] != null)
                {
                    FrmDashboardReport _instanceFrmDashboardReport = Application.OpenForms["FrmDashboardReport"] as FrmDashboardReport;
                    _instanceFrmDashboardReport.Visible = false;
                }
                if (System.Windows.Forms.Application.OpenForms["FrmCombinedBill"] != null)
                {
                    FrmCombinedBill _instanceFrmCombinedBill = Application.OpenForms["FrmCombinedBill"] as FrmCombinedBill;
                    //_instanceFrmTableView.AutoTimer.Stop();
                    _instanceFrmCombinedBill.Close();
                    _instanceFrmCombinedBill.Dispose();
                    GC.Collect();
                }
                time_syn_lal.Stop();
                reset_sync.Stop();
                lblsync.Visible = false;

                panelDesktop.SuspendLayout();

                if (flp_operabuttons.Visible)
                    flp_operabuttons.Visible = false;

                btn_pickup.Visible = true;

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "COUNT(*) OVER() AS Field1";
                objSearch.Table = "tbltable_info";
                objSearch.WhereNdGroupBy = "Where Status=1";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
                    {
                        FrmTableView _instanceFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                        if (_instanceFrmTableView.WindowState == FormWindowState.Minimized)
                            _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        else
                        {
                            tlp_tablestatus.SuspendLayout();
                            btn_rev.Visible = true;
                            btn_running.Visible = true;
                            btn_avial.Visible = true;
                            btntrefresh.Visible = true;
                            tlp_tablestatus.ResumeLayout();

                            _instanceFrmTableView.Visible = true;
                            _instanceFrmTableView.AutoTimer.Start();
                            _instanceFrmTableView.Dock = DockStyle.Fill;
                            ResizeForm(_instanceFrmTableView);
                            panelDesktop.Controls.Add(_instanceFrmTableView);
                            _instanceFrmTableView.Show();
                            _instanceFrmTableView.BringToFront();
                        }
                    }
                    else
                    {
                        tlp_tablestatus.SuspendLayout();
                        btn_rev.Visible = true;
                        btn_running.Visible = true;
                        btn_avial.Visible = true;
                        btntrefresh.Visible = true;
                        tlp_tablestatus.ResumeLayout();

                        FrmTableView _instanceFrmTableView = new FrmTableView(0);
                        ResizeForm(_instanceFrmTableView);
                        _instanceFrmTableView.TopLevel = false;
                        panelDesktop.Controls.Add(_instanceFrmTableView);
                        _instanceFrmTableView.Show();
                        _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        _instanceFrmTableView.BringToFront();

                    }
                }
                else
                {
                    btn_pickup.PerformClick();
                }

                menutable_click = true;
                panelDesktop.ResumeLayout();              
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("homepage Menutable_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }



        private void ib_addon_Click(object sender, EventArgs e)
        {
            try
            {
                panelDesktop.BackColor = SystemColors.Control;
                if (System.Windows.Forms.Application.OpenForms["FrmAddon"] != null)
                {
                    FrmAddon _instanceFrmAddon = Application.OpenForms["FrmAddon"] as FrmAddon;
                    if (_instanceFrmAddon.WindowState == FormWindowState.Minimized)
                        _instanceFrmAddon.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        tlp_tablestatus.SuspendLayout();
                        btn_rev.Visible = true;
                        btn_running.Visible = true;
                        btn_avial.Visible = true;
                        btntrefresh.Visible = true;
                        tlp_tablestatus.ResumeLayout();

                        _instanceFrmAddon.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmAddon);
                        panelDesktop.Controls.Add(_instanceFrmAddon);
                        _instanceFrmAddon.Show();
                        _instanceFrmAddon.BringToFront();
                    }
                }
                else
                {
                    FrmAddon _instanceFrmAddon = new FrmAddon();
                    ResizeForm(_instanceFrmAddon);
                    _instanceFrmAddon.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmAddon);
                    _instanceFrmAddon.Show();
                    _instanceFrmAddon.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmAddon.BringToFront();
                    UIHelper.ApplyRoundedCorners(_instanceFrmAddon,30);
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("homepage ib_addon_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
        private void btn_avial_Click(object sender, EventArgs e)
        {
            try
            {
                if (menutable_click)
                {
                    CloseTableViewPage();
                }

                if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
                {
                    FrmTableView _instanceFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                    if (_instanceFrmTableView.WindowState == FormWindowState.Minimized)
                        _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        tlp_tablestatus.SuspendLayout();
                        btn_rev.Visible = true;
                        btn_running.Visible = true;
                        btn_avial.Visible = true;
                        btntrefresh.Visible = true;
                        tlp_tablestatus.ResumeLayout();

                        _instanceFrmTableView.Visible = true;
                        _instanceFrmTableView.AutoTimer.Start();
                        _instanceFrmTableView.Dock = DockStyle.Fill;
                        ResizeForm(_instanceFrmTableView);
                        panelDesktop.Controls.Add(_instanceFrmTableView);
                        _instanceFrmTableView.Show();
                        _instanceFrmTableView.BringToFront();
                    }
                }
                else
                {
                    tlp_tablestatus.SuspendLayout();
                    btn_rev.Visible = true;
                    btn_running.Visible = true;
                    btn_avial.Visible = true;
                    btntrefresh.Visible = true;
                    tlp_tablestatus.ResumeLayout();

                    FrmTableView _instanceFrmTableView = new FrmTableView(Convert.ToInt32(ButtonType.avilabletbl));
                    ResizeForm(_instanceFrmTableView);
                    _instanceFrmTableView.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmTableView);
                    _instanceFrmTableView.Show();
                    _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmTableView.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("homepage btn_avial_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void btn_rev_Click(object sender, EventArgs e)
        {
            try
            {
                if (menutable_click)
                {
                    CloseTableViewPage();
                }
                if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
                {
                    FrmTableView _instanceFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                    if (_instanceFrmTableView.WindowState == FormWindowState.Minimized)
                        _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        tlp_tablestatus.SuspendLayout();
                        btn_rev.Visible = true;
                        btn_running.Visible = true;
                        btn_avial.Visible = true;
                        btntrefresh.Visible = true;
                        tlp_tablestatus.ResumeLayout();

                        _instanceFrmTableView.Visible = true;
                        _instanceFrmTableView.AutoTimer.Start();
                        _instanceFrmTableView.Dock = DockStyle.Fill;
                        ResizeForm(_instanceFrmTableView);
                        panelDesktop.Controls.Add(_instanceFrmTableView);
                        _instanceFrmTableView.Show();
                        _instanceFrmTableView.BringToFront();
                    }
                }
                else
                {
                    tlp_tablestatus.SuspendLayout();
                    btn_rev.Visible = true;
                    btn_running.Visible = true;
                    btn_avial.Visible = true;
                    btntrefresh.Visible = true;
                    tlp_tablestatus.ResumeLayout();

                    FrmTableView _instanceFrmTableView = new FrmTableView(Convert.ToInt32(ButtonType.Revtbl));
                    ResizeForm(_instanceFrmTableView);
                    _instanceFrmTableView.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmTableView);
                    _instanceFrmTableView.Show();
                    _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmTableView.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("homepage btn_rev_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void btn_running_Click(object sender, EventArgs e)
        {
            try
            {
                if (menutable_click)
                {
                    CloseTableViewPage();
                }

                if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
                {
                    FrmTableView _instanceFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                    if (_instanceFrmTableView.WindowState == FormWindowState.Minimized)
                        _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        tlp_tablestatus.SuspendLayout();
                        btn_rev.Visible = true;
                        btn_running.Visible = true;
                        btn_avial.Visible = true;
                        btntrefresh.Visible = true;
                        tlp_tablestatus.ResumeLayout();

                        _instanceFrmTableView.Visible = true;
                        _instanceFrmTableView.AutoTimer.Start();
                        _instanceFrmTableView.Dock = DockStyle.Fill;
                        ResizeForm(_instanceFrmTableView);
                        panelDesktop.Controls.Add(_instanceFrmTableView);
                        _instanceFrmTableView.Show();
                        _instanceFrmTableView.BringToFront();
                    }
                }
                else
                {
                    tlp_tablestatus.SuspendLayout();
                    btn_rev.Visible = true;
                    btn_running.Visible = true;
                    btn_avial.Visible = true;
                    btntrefresh.Visible = true;
                    tlp_tablestatus.ResumeLayout();

                    FrmTableView _instanceFrmTableView = new FrmTableView(Convert.ToInt32(ButtonType.Activetbl));
                    ResizeForm(_instanceFrmTableView);
                    _instanceFrmTableView.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmTableView);
                    _instanceFrmTableView.Show();
                    _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmTableView.BringToFront();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("homepage btn_running_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void btntrefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (menutable_click)
                {
                    CloseTableViewPage();
                }
                if (ButtonID == Convert.ToInt32(Menubutton.Pickup))
                {
                    if (System.Windows.Forms.Application.OpenForms["FrmPickup"] != null)
                    {
                        FrmPickup instanceFrmPickup = Application.OpenForms["FrmPickup"] as FrmPickup;
                        //_instanceFrmTableView.AutoTimer.Stop();
                        instanceFrmPickup.Close();
                        instanceFrmPickup.Dispose();
                        GC.Collect();
                    }

                    tlp_tablestatus.SuspendLayout();
                    btn_rev.Visible = true;
                    btn_running.Visible = true;
                    btn_avial.Visible = true;
                    btntrefresh.Visible = true;
                    tlp_tablestatus.ResumeLayout();

                    FrmPickup _instanceFrmPickup = new FrmPickup(0);
                    ResizeForm(_instanceFrmPickup);
                    _instanceFrmPickup.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmPickup);
                    _instanceFrmPickup.Show();
                    _instanceFrmPickup.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmPickup.BringToFront();

                }
                else if (ButtonID == Convert.ToInt32(Menubutton.Menutable))
                {
                    if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
                    {
                        FrmTableView _instanceFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                        if (_instanceFrmTableView.WindowState == FormWindowState.Minimized)
                            _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        else
                        {
                            tlp_tablestatus.SuspendLayout();
                            btn_rev.Visible = true;
                            btn_running.Visible = true;
                            btn_avial.Visible = true;
                            btntrefresh.Visible = true;
                            btn_pickup.Visible = true;
                            tlp_tablestatus.ResumeLayout();

                            _instanceFrmTableView.Visible = true;
                            _instanceFrmTableView.AutoTimer.Start();
                            _instanceFrmTableView.Dock = DockStyle.Fill;
                            ResizeForm(_instanceFrmTableView);
                            panelDesktop.Controls.Add(_instanceFrmTableView);
                            _instanceFrmTableView.Show();
                            _instanceFrmTableView.BringToFront();
                        }
                    }
                    else
                    {
                        tlp_tablestatus.SuspendLayout();
                        btn_rev.Visible = true;
                        btn_running.Visible = true;
                        btn_avial.Visible = true;
                        btntrefresh.Visible = true;
                        btn_pickup.Visible = true;
                        tlp_tablestatus.ResumeLayout();

                        FrmTableView _instanceFrmTableView = new FrmTableView(Convert.ToInt32(ButtonType.refresh));
                        ResizeForm(_instanceFrmTableView);
                        _instanceFrmTableView.TopLevel = false;
                        panelDesktop.Controls.Add(_instanceFrmTableView);
                        _instanceFrmTableView.Show();
                        _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        _instanceFrmTableView.BringToFront();
                    }
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("homepage btntrefresh_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }


        private void time_syn_lal_Tick(object sender, EventArgs e)
        {
            try
            {
                lblsync.Visible = !lblsync.Visible;
            }             
            catch(Exception ex)
            {
            }
        }
        public void StopTimerAndRefreshLabel()
        {
            try
            {
                time_syn_lal.Stop();
                tlp_tablestatus.SuspendLayout();
                TimeSpan elapsedTime = DateTime.Now.Subtract(startTime);
                string formattedTime = string.Format("{0:D2}:{1:D2}:{2}",
                                                     elapsedTime.Hours,
                                                     elapsedTime.Minutes,
                                                     elapsedTime.Seconds );
                lblsync.Text = $"Synchronized {formattedTime} ago";
                tlp_tablestatus.ResumeLayout();
                reset_sync.Start();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("homepage StopTimerAndRefreshLabel", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void btn_pickup_Click(object sender, EventArgs e)
        {
            try
            {
                if (menutable_click)
                {
                    CloseTableViewPage();
                }
                ButtonID = Convert.ToInt32(Menubutton.Pickup);
                if (System.Windows.Forms.Application.OpenForms["FrmDashboardReport"] != null)
                {
                    FrmDashboardReport _instanceFrmDashboardReport = Application.OpenForms["FrmDashboardReport"] as FrmDashboardReport;
                    _instanceFrmDashboardReport.Visible = false;
                }
                time_syn_lal.Stop();
                reset_sync.Stop();
                lblsync.Visible = false;

                panelDesktop.SuspendLayout();

                if (flp_operabuttons.Visible)
                    flp_operabuttons.Visible = false;

                if (System.Windows.Forms.Application.OpenForms["FrmPickup"] != null)
                {
                    FrmPickup _instanceFrmPickup = Application.OpenForms["FrmPickup"] as FrmPickup;
                    if (_instanceFrmPickup.WindowState == FormWindowState.Minimized)
                        _instanceFrmPickup.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        tlp_tablestatus.SuspendLayout();
                        btn_rev.Visible = false;
                        btn_running.Visible = false;
                        btn_avial.Visible = false;
                        btntrefresh.Visible = false;
                        btn_pickup.Visible = false;
                        tlp_tablestatus.ResumeLayout();

                        _instanceFrmPickup.Visible = true;
                       // _instanceFrmPickup.AutoTimer.Start();
                        _instanceFrmPickup.Dock = DockStyle.Fill;
                        ResizeForm(_instanceFrmPickup);
                        panelDesktop.Controls.Add(_instanceFrmPickup);
                        _instanceFrmPickup.Show();
                        _instanceFrmPickup.BringToFront();
                    }
                }
                else
                {
                    tlp_tablestatus.SuspendLayout();
                    btn_rev.Visible = false;
                    btn_running.Visible = false;
                    btn_avial.Visible = false;
                    btntrefresh.Visible = false;
                    btn_pickup.Visible = false;
                    tlp_tablestatus.ResumeLayout();

                    FrmPickup _instanceFrmPickup = new FrmPickup(Convert.ToInt32(ButtonType.pickup));
                    ResizeForm(_instanceFrmPickup);
                    _instanceFrmPickup.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmPickup);
                    _instanceFrmPickup.Show();
                    _instanceFrmPickup.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmPickup.BringToFront();

                }

                menutable_click = true;
                panelDesktop.ResumeLayout();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("homepage btn_pickup_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void btn_billsetting_Click(object sender, EventArgs e)
        {
            //tlp_main_sidebar_pnldesktop.Enabled = true;
            pnl_billsetting.Visible = false;
        }

        private void btnclose_drawer_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_Drawer.Visible = false;

            }
            catch (Exception ex)
            {
                clsgen.LogException("Admin page btnclose_drawer_Click_1()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndrawersave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtdrawerid.Text))
                    txtdrawerid.Text = "0";
                if (string.IsNullOrWhiteSpace(txtinitialamt.Text))
                    txtinitialamt.Text = "0";
                if (string.IsNullOrWhiteSpace(txtdrops.Text))
                    txtdrops.Text = "0";
                if (string.IsNullOrWhiteSpace(txtextraaddamt.Text))
                    txtextraaddamt.Text = "0";

                /* updation on loan and drops then adding */

                if (string.IsNullOrWhiteSpace(txtdrops_backup.Text))
                    txtdrops_backup.Text = "0";
                if (string.IsNullOrWhiteSpace(txtextraaddamt_backup.Text))
                    txtextraaddamt_backup.Text = "0";

                /* end */

                if (MandatoryCheckDrawer())
                {
                    Dmndrawer dmndrawer = new Dmndrawer();
                    Blldrawer blldrawer = new Blldrawer();
                    dmndrawer.Id = Convert.ToInt32(txtdrawerid.Text);
                    dmndrawer.DName = System.Environment.MachineName;
                    dmndrawer.InitilAmount = Convert.ToDecimal(txtinitialamt.Text);
                    dmndrawer.EmployeeId = Convert.ToInt32(txtempoyeId.Text);
                    if (txtextraaddamt.Text != txtextraaddamt_backup.Text)
                        dmndrawer.LoanAmount = Convert.ToDecimal(txtextraaddamt.Text);
                    else
                        dmndrawer.LoanAmount = 0;

                    if (txtdrops.Text != txtdrops_backup.Text)
                        dmndrawer.Drops = Convert.ToDecimal(txtdrops.Text);
                    else
                        dmndrawer.Drops = 0;

                  
                    dmndrawer.CreatedBy = FrmDashboard.UserId;
                    dmndrawer.Status = 1;
                    var InsertStatus = blldrawer.AddEditDatadrawer("prdInsertDrawer", dmndrawer);

                    if (InsertStatus != null)
                    {
                        if (txtdrawerid.Text == null || txtdrawerid.Text == "0")
                        {
                            DrawerId = InsertStatus.Id;
                            MessageBox.Show("Drawer Assign Completed", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtinitialamt.ReadOnly = true;
                        }
                        else
                        {
                            DrawerId = Convert.ToInt32(txtdrawerid.Text);
                            MessageBox.Show("Update Sucessfull", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        pnl_Drawer.Visible = false;
                    }
                    else
                        MessageBox.Show("Assign Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (DrawerId > 0)
                    {
                        txtdrawerid.Text = Convert.ToString(DrawerId);
                        txtextraaddamt_backup.Text = txtextraaddamt.Text;
                        txtdrops.Text = txtdrops_backup.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Admin page btndrawersave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool MandatoryCheckDrawer()
        {
            try
            {

                if (flagDrawerF && string.IsNullOrWhiteSpace(txtstaffname.Text))
                {
                    MessageBox.Show("Please select Employee", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtstaffname.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtinitialamt.Text) || Convert.ToDecimal(txtinitialamt.Text) == 0)
                {
                    MessageBox.Show("Please Enter Initial Amount", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Admin page MandatoryCheckDrawer()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void tmtime_Tick(object sender, EventArgs e)
        {

            lbl_time.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToShortDateString();

            // Get current process memory usage
            Process currentProcess = Process.GetCurrentProcess();
            long memoryInBytes = currentProcess.PrivateMemorySize64; // or WorkingSet64
            double memoryInMB = memoryInBytes / (1024.0 * 1024.0);

            // Display in label (rounded to 2 decimal places)
            lblmemory.Text = $"Memory: {memoryInMB:F2} MB";
        }

  
        public void StaffPopup()
        {
            try
            {
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,EmployeeCode,EmployeeName,DesignationName,Mobile";
                _objSearch.Select = "emp.EmployeeId as Id,emp.EmployeeCode as Field1,emp.EmployeeName as Field2,d.DesignationName as Field3,emp.Mobile as Field4";
                _objSearch.Table = "tblEmployee  emp Left join tblDesignation d on d.DesignationId = emp.DesignationId ";
                _objSearch.WhereNdGroupBy = "WHERE isnull(emp.Status,0) = 1 and emp.BranchId=" + BranchId;
                _objSearch.OrderBy = " ORDER BY emp.EmployeeName ASC";
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))

                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);
                    }
                }
                if (returnValue.ToString() != "")
                {
                    int eid = Convert.ToInt32(returnValue);
                    if (DrawerCheckBranch(eid))
                    {
                        MessageBox.Show("Drawer Already Assigned on " + Drawersystemname + "System", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }

                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "COUNT(*) OVER() AS Field1,emp.EmployeeName as Field2,d.DesignationName as Field3";
                    objSearch.Table = "tblEmployee  emp Left join tblDesignation d on d.DesignationId = emp.DesignationId";
                    objSearch.WhereNdGroupBy = "Where emp.status=1 and emp.EmployeeId = " + eid;
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        txtstaffname.Text = Convert.ToString(itemSource[0].Field2);
                        lbl_desigtnation.Text = Convert.ToString(itemSource[0].Field3);
                        txtempoyeId.Text = Convert.ToString(eid);
                        lbl_staffname_tip.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Admin page  StaffPopup()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
                ////MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
   

        private void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_Drawer.Visible = false;

            }
            catch (Exception ex)
            {
                clsgen.LogException("Admin page btncancel_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmDashboard_Shown(object sender, EventArgs e)
        {
            UIHelper.RoundCorners(flp_sidemenu, 25, ColorTranslator.FromHtml("#AC3CC6"));
            UIHelper.RoundCorners(pnl_navbar, 25, ColorTranslator.FromHtml("#AC3CC6"));
            UIHelper.RoundCorners(panelDesktop, 25, ColorTranslator.FromHtml("#AC3CC6"));
            UIHelper.RoundCorners(pnl_footer, 25, ColorTranslator.FromHtml("#AC3CC6"));
        }

        private void btn_adv_idlelogout_Click(object sender, EventArgs e)
        {
            tlp_timoutforms.Visible = true;
            btn_cle_logoutpages.Visible = true;
            btn_previous.Visible = false;
            tlp_timoutforms.BringToFront();
        }

        private void btn_cle_logoutpages_Click(object sender, EventArgs e)
        {
            tlp_timoutforms.Visible = false;
        }

        private void chk_logoutall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is CheckBox tickAll)
                {
                    isUpdatingAll = true;
                    bool isChecked = tickAll.Checked;

                    chk_settings.Checked = isChecked;
                    chk_report.Checked = isChecked;
                    chk_billing.Checked = isChecked;
                    chk_emp.Checked = isChecked;
                    chk_op.Checked = isChecked;
                    chk_table.Checked = isChecked;
                    chk_home.Checked = isChecked;
                    chk_cmnpages.Checked = isChecked;
                    chk_salebill.Checked = isChecked;
                    chk_yzdailycollle.Checked = isChecked;

                    isUpdatingAll = false;

                    logoutSettings.Clear();
                    for (int i = 1; i <= 10; i++)
                    {
                        logoutSettings[i] = isChecked ? 1 : 0;
                    }

                    UpdateLogoutFormsNo();
                }
            }
            catch (Exception ex)
            {
                // Optionally log or display the error
            }
        }
        private void UpdateLogoutFormsNo()
        {
            logoutformsno = string.Join(",", logoutSettings.Where(kvp => kvp.Value == 1).Select(kvp => kvp.Key));
        }

        private void chk_home_CheckedChanged(object sender, EventArgs e)
        {
            if (isUpdatingAll) return;

            if (sender is CheckBox rjb)
            {
                int value = rjb.Checked ? 1 : 0;
                if (rjb.Tag != null && int.TryParse(rjb.Tag.ToString(), out int tagKey))
                {
                    if (logoutSettings.ContainsKey(tagKey))
                    {
                        logoutSettings[tagKey] = value;
                    }
                    else
                    {
                        logoutSettings.Add(tagKey, value);
                    }

                }
                UpdateLogoutFormsNo();
            }
        }

        private void btnsavegen_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtgenid.Text))
                    txtgenid.Text = "0";

                if (string.IsNullOrEmpty(txtlogouttime.Text))
                    txtlogouttime.Text = "0";

                DmnGeneralSetting dmngen = new DmnGeneralSetting();
                Bllgeneralsetting bllgen = new Bllgeneralsetting();
                dmngen.Gid = Convert.ToInt32(txtgenid.Text);
                dmngen.printdal = rtb_gen.Checked;
                dmngen.xyprintdal = rtb_zyout.Checked;
                dmngen.dalydal = rtb_dlycoll.Checked;
                dmngen.EmployeeId = Convert.ToInt32(FrmLogin.EmployeeId);
                dmngen.BranchId = FrmDashboard.BranchId;
                dmngen.CreatedBy = FrmDashboard.UserId;

                dmngen.EnableDefaultGmail = rtbdefgmail.Checked;
                dmngen.DefaultGmail = txtdefgmail.Text.Trim();
                dmngen.AutoPrint = AutoPrint.Checked;
                dmngen.AutoDrawer = AutoDrawer.Checked;
                dmngen.Save_YZout = Save_YZout.Checked;
                dmngen.Save_Daily = Save_Daily.Checked;
                dmngen.YZPath = Convert.ToString(YZPath.Text);
                dmngen.DailyPath = Convert.ToString(DailyPath.Text);

                dmngen.EnableGroupCardInPayment = rtb_group_payment.Checked;
                dmngen.EnableFamilyCardInPayment = rtb_family_payment.Checked;

                dmngen.AllowManualCardInputDuringPayment = rtb_cardnotype_payment.Checked;

                dmngen.AutoLogoutWhenIdle = rtb_logouttime.Checked;
                dmngen.AutoLogoutIdleTimeoutMinutes = Convert.ToDecimal(txtlogouttime.Text);
                dmngen.LogoutFormscustom = logoutformsno;
                dmngen.EnableUserPrompt = rtb_userpmpt.Checked;

                dmngen.Status = 1;
                if (Save_YZout.Checked == true && string.IsNullOrEmpty(YZPath.Text))
                {
                    MessageBox.Show("Bill Pdf Path Should not Be Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Save_Daily.Checked == true && string.IsNullOrEmpty(DailyPath.Text))
                {
                    MessageBox.Show("Bill Pdf Path Should not Be Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var InsertStatus = bllgen.AddEditDataGen("prdInsertGeneralsetting", dmngen);
                    if (InsertStatus.Gid > 0)
                    {
                        MessageBox.Show("Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Generalsettingcheck();
                        txtgenid.Text = Convert.ToString(InsertStatus.Gid);
                        FrmLogin.AutoLogoutIdleTimeoutMinutes = Convert.ToDecimal(txtlogouttime.Text);

                    }
                    else
                    {
                        MessageBox.Show("Saving Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage btnsavegen_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
        public void GeneralsettingFetching()
        {
            try
            {
                BllSearch<DmnGeneralSetting> bllsearchdrawer = new BllSearch<DmnGeneralSetting>();
                DmnSearch dmnsearckgen = new DmnSearch
                {
                    Select = "*",
                    Table = "tblGeneralSetting",
                    WhereNdGroupBy = $"where BranchId={BranchId} and Status=1",
                    OrderBy = ""
                };
                var gridesource = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckgen);
                if (gridesource != null && gridesource.Count > 0)
                {
                    var settings = gridesource[0];
                    PrintDalvalue = settings.printdal;
                    XYDalvalue = settings.xyprintdal;
                    DalDalvalue = settings.dalydal;

                    AutoLogoutWhenIdle = settings.AutoLogoutWhenIdle;
                    FrmLogin.AutoLogoutIdleTimeoutMinutes = Convert.ToDecimal(settings.AutoLogoutIdleTimeoutMinutes);

                    logoutformsno = settings.LogoutFormscustom;
                    UserSavePrompt = settings.EnableUserPrompt;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage Page GeneralsettingFetching()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
        public void Generalsettingcheck()
        {
            try
            {
                tlp_timoutforms.Visible = false;
                btn_cle_logoutpages.PerformClick();

                previouspage();

                btn_cle_logoutpages.Visible = false;

                pictureBox13.Visible = false;
                pictureBox15.Visible = false;
                int Pagecount = Convert.ToInt32(ConfigurationManager.AppSettings["POSpagescount"]);
                txtperpagecount.Text = Pagecount.ToString();
                BllSearch<DmnGeneralSetting> bllsearchdrawer = new BllSearch<DmnGeneralSetting>();
                DmnSearch dmnsearckgen = new DmnSearch
                {
                    Select = "*",
                    Table = "tblGeneralSetting",
                    WhereNdGroupBy = $"where BranchId={BranchId} and Status=1",
                    OrderBy = ""
                };
                var gridesource = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckgen);
                if (gridesource.Count > 0)
                {
                    var settings = gridesource[0];

                    rtb_gen.Checked = settings.printdal;
                    rtb_zyout.Checked = settings.xyprintdal;
                    rtb_dlycoll.Checked = settings.dalydal;

                    txtgenid.Text = Convert.ToString(settings.Gid);
                    rtbdefgmail.Checked = settings.EnableDefaultGmail;
                    txtdefgmail.Text = Convert.ToString(settings.DefaultGmail);
                    AutoPrint.Checked = settings.AutoPrint;
                    AutoDrawer.Checked = settings.AutoDrawer;
                    Save_YZout.Checked = settings.Save_YZout;
                    if (Save_YZout.Checked == true)
                    {
                        pictureBox13.Visible = true;
                    }
                    Save_Daily.Checked = settings.Save_Daily;
                    if (Save_Daily.Checked == true)
                    {
                        pictureBox15.Visible = true;
                    }
                    YZPath.Text = Convert.ToString(settings.YZPath);
                    DailyPath.Text = Convert.ToString(settings.DailyPath);
                    rtb_group_payment.Checked = settings.EnableGroupCardInPayment;
                    rtb_family_payment.Checked = settings.EnableFamilyCardInPayment;

                    rtb_cardnotype_payment.Checked = settings.AllowManualCardInputDuringPayment;
                    rtb_logouttime.Checked = settings.AutoLogoutWhenIdle;
                    txtlogouttime.Text = settings.AutoLogoutIdleTimeoutMinutes.ToString();
                    FrmLogin.AutoLogoutIdleTimeoutMinutes = Convert.ToDecimal(settings.AutoLogoutIdleTimeoutMinutes);
                    if (Convert.ToString(settings.LogoutFormscustom) != null)
                    {
                        logoutformsno = Convert.ToString(settings.LogoutFormscustom);

                        var selectedForms = logoutformsno.Split(',')
                                      .Select(s => int.TryParse(s, out int val) ? val : 0)
                                      .Where(v => v > 0)
                                      .ToList();

                        bool IsChecked(int formNo) => selectedForms.Contains(formNo);

                        chk_home.Checked = IsChecked((int)LogoutFormList.Home);
                        chk_table.Checked = IsChecked((int)LogoutFormList.tablepickup);
                        chk_op.Checked = IsChecked((int)LogoutFormList.operation);
                        chk_report.Checked = IsChecked((int)LogoutFormList.report);
                        chk_billing.Checked = IsChecked((int)LogoutFormList.billing);
                        chk_emp.Checked = IsChecked((int)LogoutFormList.employee);
                        chk_settings.Checked = IsChecked((int)LogoutFormList.settings);

                        chk_cmnpages.Checked = IsChecked((int)LogoutFormList.coomonpage);
                        chk_salebill.Checked = IsChecked((int)LogoutFormList.Salebill);
                        chk_yzdailycollle.Checked = IsChecked((int)LogoutFormList.yzout);

                        chk_logoutall.Checked = selectedForms.Count == 10;
                    }
                    if (rtb_logouttime.Checked)
                    {
                        if (rtb_logouttime.Checked)
                        {
                            AppSession.RegisterForm(this, (int)LogoutFormList.Home);
                        }
                        btn_adv_idlelogout.Enabled = true;
                    }
                    else
                        btn_adv_idlelogout.Enabled = false;

                    rtb_userpmpt.Checked = settings.EnableUserPrompt;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage Generalsettingcheck", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
        private void btnclose_gen_Click(object sender, EventArgs e)
        {
            pnlgenralsetting.Visible = false;
        }

        private void rtb_logouttime_CheckedChanged(object sender, EventArgs e)
        {

            if (rtb_logouttime.Checked)
            {
                txtlogouttime.Visible = true;
                btn_adv_idlelogout.Enabled = true;
                txtlogouttime.Focus();
            }
            else
            {
                txtlogouttime.Visible = false;
                btn_adv_idlelogout.Enabled = false;
                txtlogouttime.Text = "";
            }
        }

        private void txtlogouttime_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            // Allow control keys (Backspace, Delete, etc.)
            if (char.IsControl(e.KeyChar))
                return;

            // Allow only digits and one decimal point
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            // Allow only one decimal point
            if (e.KeyChar == '.' && txt.Text.Contains('.'))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtlogouttime_Leave(object sender, EventArgs e)
        {
            if (rtb_logouttime.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtlogouttime.Text) || txtlogouttime.Text.Trim() == "0")
                {
                    MessageBox.Show("Please enter a valid timeout value.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rtb_logouttime.Checked = false;
                }
            }
        }

        private void txtlogouttime_Click(object sender, EventArgs e)
        {
            if (Enb_Home_Number)
            {
                decimal kdata = 0;
                using (FoodandBeverage.Forms.UserControl.Frmnumdiskeypad frm = new FoodandBeverage.Forms.UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.Number)))
                {
                    frm.ShowDialog();
                    kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                    frm.Dispose();
                }
                if (sender is TextBox txt)
                {
                    txt.Text = kdata.ToString("0.##");
                }
            }
        }

        private void btn_close_vir_Click(object sender, EventArgs e)
        {
            pnl_virtualkeyboard.Visible = false;
        }

        private void btn_vir_cls_Click(object sender, EventArgs e)
        {
            pnl_virtualkeyboard.Visible = false;
        }

        private void btn_vir_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtvirId.Text))
                    txtvirId.Text = "0";

                DmnVirtualKeyBoard dmnVirtual = new DmnVirtualKeyBoard();
                BllVirtualkeyBorad bllVirtual = new BllVirtualkeyBorad();
                dmnVirtual.Id = Convert.ToInt32(txtvirId.Text);

                // === Home ===
                dmnVirtual.Enb_Home_Normal = chk_home_normal.Checked;
                dmnVirtual.Enb_Home_Number = chk_home_number.Checked;

                // === Allow Virtual Keyboard Login ===
                dmnVirtual.Enb_AllowVirtualKeyboardLogin_Normal = chk_login_normal.Checked;
                dmnVirtual.Enb_AllowVirtualKeyboardLogin_Number = chk_login_number.Checked;

                // === Table Pick Up ===
                dmnVirtual.Enb_TablePickUp_Normal = chk_tablep_normal.Checked;
                dmnVirtual.Enb_TablePickUp_Number = chk_tablep_number.Checked;

                // === Operation ===
                dmnVirtual.Enb_Operation_Normal = chk_oper_normal.Checked;
                dmnVirtual.Enb_Operation_Number = chk_oper_number.Checked;

                // === Employee ===
                dmnVirtual.Enb_Employee_Normal = chk_emp_normal.Checked;
                dmnVirtual.Enb_Employee_Number = chk_emp_number.Checked;

                // === Billing ===
                dmnVirtual.Enb_Billing_Normal = chk_billing_normal.Checked;
                dmnVirtual.Enb_Billing_Number = chk_billing_number.Checked;

                // === Report ===
                dmnVirtual.Enb_Report_Normal = chk_report_normal.Checked;
                dmnVirtual.Enb_Report_Number = chk_report_number.Checked;

                // === Order Page ===
                dmnVirtual.Enb_OrderPage_Normal = chk_order_normal.Checked;
                dmnVirtual.Enb_OrderPage_Number = chk_order_number.Checked;

                // === Kitchen View ===
                dmnVirtual.Enb_KitchenView_Normal = chk_kitchen_normal.Checked;
                dmnVirtual.Enb_KitchenView_Number = chk_kitchen_number.Checked;

                // === Search Page ===
                dmnVirtual.Enb_SearchPage_Normal = chk_serach_normal.Checked;
                dmnVirtual.Enb_SearchPage_Number = chk_serach_number.Checked;

                // === Sale Bill / Y-Z Out Page ===
                dmnVirtual.Enb_SaleBill_YZoutPage_Normal = chk_billyz_normal.Checked;
                dmnVirtual.Enb_SaleBill_YZoutPage_Number = chk_billyz_number.Checked;

                // === System Fields ===
                //dmnVirtual.mDeviceIP = System.Environment.MachineName;
                dmnVirtual.BranchId = FrmDashboard.BranchId;
                dmnVirtual.CreatedBy = FrmDashboard.UserId; 
                dmnVirtual.Status = 1;

                var InsertStatus = bllVirtual.AddEditDataVirtualkeyboard("prdInsertVirtualKeyboard", dmnVirtual);

                if (InsertStatus.Id > 0)
                {
                    txtvirId.Text = Convert.ToString(InsertStatus.Id);
                    virtualkeyboardCheck();
                    pnl_virtualkeyboard.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage page btnsavevirtual_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }
        public void virtualkeyboardCheck()
        {
            try
            {
                BllSearch<DmnVirtualKeyBoard> Bllvirtualkeyboard = new BllSearch<DmnVirtualKeyBoard>();
                DmnSearch dmnsearchvir = new DmnSearch
                {
                    Select = @"
                HomeNumberKeyboard AS Enb_Home_Number,
                AllowVirtualKeyboardLoginNumber AS Enb_AllowVirtualKeyboardLogin_Number,
                TablePickUpNumberKeyboard AS Enb_TablePickUp_Number,
                OperationNumberKeyboard AS Enb_Operation_Number,
                EmployeeNumberKeyboard AS Enb_Employee_Number,
                BillingNumberKeyboard AS Enb_Billing_Number,
                ReportNumberKeyboard AS Enb_Report_Number,
                OrderPageNumberKeyboard AS Enb_OrderPage_Number,
                KitchenViewNumberKeyboard AS Enb_KitchenView_Number,
                SearchPageNumberKeyboard AS Enb_SearchPage_Number,
                SaleBillYZoutPageNumberKeyboard AS Enb_SaleBill_YZoutPage_Number,

                HomeKeyboard AS Enb_Home_Normal,
                AllowVirtualKeyboardLoginKeyboard AS Enb_AllowVirtualKeyboardLogin_Normal,
                TablePickUpKeyboard AS Enb_TablePickUp_Normal,
                OperationKeyboard AS Enb_Operation_Normal,
                EmployeeKeyboard AS Enb_Employee_Normal,
                BillingKeyboard AS Enb_Billing_Normal,
                ReportKeyboard AS Enb_Report_Normal,
                OrderPageKeyboard AS Enb_OrderPage_Normal,
                KitchenViewKeyboard AS Enb_KitchenView_Normal,
                SearchPageKeyboard AS Enb_SearchPage_Normal,
                SaleBillYZoutPageKeyboard AS Enb_SaleBill_YZoutPage_Normal,
                Id",
                    Table = "tblVirtualKeyBoard",
                    WhereNdGroupBy = $"WHERE mDeviceIP='{System.Environment.MachineName}' AND BranchId={BranchId} AND Status=1",
                    OrderBy = ""
                };

                var gridesource = Bllvirtualkeyboard.GetSearchData("prdSearch", dmnsearchvir);
                if (gridesource.Count > 0)
                {
                    var data = gridesource[0];

                    // === Set Number Keyboard CheckBoxes ===
                    chk_billyz_number.Checked = data.Enb_SaleBill_YZoutPage_Number;
                    chk_serach_number.Checked = data.Enb_SearchPage_Number;
                    chk_kitchen_number.Checked = data.Enb_KitchenView_Number;
                    chk_order_number.Checked = data.Enb_OrderPage_Number;
                    chk_report_number.Checked = data.Enb_Report_Number;
                    chk_billing_number.Checked = data.Enb_Billing_Number;
                    chk_emp_number.Checked = data.Enb_Employee_Number;
                    chk_oper_number.Checked = data.Enb_Operation_Number;
                    chk_home_number.Checked = data.Enb_Home_Number;
                    chk_login_number.Checked = data.Enb_AllowVirtualKeyboardLogin_Number;
                    chk_tablep_number.Checked = data.Enb_TablePickUp_Number;

                    // === Set Normal Keyboard CheckBoxes ===
                    chk_billyz_normal.Checked = data.Enb_SaleBill_YZoutPage_Normal;
                    chk_serach_normal.Checked = data.Enb_SearchPage_Normal;
                    chk_kitchen_normal.Checked = data.Enb_KitchenView_Normal;
                    chk_order_normal.Checked = data.Enb_OrderPage_Normal;
                    chk_report_normal.Checked = data.Enb_Report_Normal;
                    chk_billing_normal.Checked = data.Enb_Billing_Normal;
                    chk_emp_normal.Checked = data.Enb_Employee_Normal;
                    chk_oper_normal.Checked = data.Enb_Operation_Normal;
                    chk_home_normal.Checked = data.Enb_Home_Normal;
                    chk_login_normal.Checked = data.Enb_AllowVirtualKeyboardLogin_Normal;
                    chk_tablep_normal.Checked = data.Enb_TablePickUp_Normal;

                    // === Assign Data to Variables ===
                    Enb_SaleBill_YZoutPage_Number = data.Enb_SaleBill_YZoutPage_Number;
                    Enb_SearchPage_Number = data.Enb_SearchPage_Number;
                    Enb_KitchenView_Number = data.Enb_KitchenView_Number;
                    Enb_OrderPage_Number = data.Enb_OrderPage_Number;
                    Enb_Report_Number = data.Enb_Report_Number;
                    Enb_Billing_Number = data.Enb_Billing_Number;
                    Enb_Employee_Number = data.Enb_Employee_Number;
                    Enb_Operation_Number = data.Enb_Operation_Number;
                    Enb_Home_Number = data.Enb_Home_Number;
                    FrmLogin.loginkeyboard = data.Enb_AllowVirtualKeyboardLogin_Number;
                    Enb_TablePickUp_Number = data.Enb_TablePickUp_Number;

                    Enb_SaleBill_YZoutPage_Normal = data.Enb_SaleBill_YZoutPage_Normal;
                    Enb_SearchPage_Normal = data.Enb_SearchPage_Normal;
                    Enb_KitchenView_Normal = data.Enb_KitchenView_Normal;
                    Enb_OrderPage_Normal = data.Enb_OrderPage_Normal;
                    Enb_Report_Normal = data.Enb_Report_Normal;
                    Enb_Billing_Normal = data.Enb_Billing_Normal;
                    Enb_Employee_Normal = data.Enb_Employee_Normal;
                    Enb_Operation_Normal = data.Enb_Operation_Normal;
                    Enb_Home_Normal = data.Enb_Home_Normal;
                    Enb_AllowVirtualKeyboardLogin_Normal = data.Enb_AllowVirtualKeyboardLogin_Normal;
                    Enb_TablePickUp_Normal = data.Enb_TablePickUp_Normal;

                    txtvirId.Text = Convert.ToString(data.Id);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("home page virtualkeyboardCheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
        private void chk_number_all_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool isChecked = chk_number_all.Checked;

                // === Set all Number Keyboard checkboxes ===
                chk_tablep_number.Checked = isChecked;
                chk_login_number.Checked = isChecked;
                chk_home_number.Checked = isChecked;

                chk_billyz_number.Checked = isChecked;
                chk_serach_number.Checked = isChecked;
                chk_kitchen_number.Checked = isChecked;
                chk_order_number.Checked = isChecked;
                chk_report_number.Checked = isChecked;
                chk_billing_number.Checked = isChecked;
                chk_emp_number.Checked = isChecked;
                chk_oper_number.Checked = isChecked;
            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage page chk_number_all_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void chk_normal_all_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool isChecked = chk_normal_all.Checked;

                // === Set all Normal Keyboard checkboxes ===
                chk_tablep_normal.Checked = isChecked;
                chk_login_normal.Checked = isChecked;
                chk_home_normal.Checked = isChecked;

                chk_billyz_normal.Checked = isChecked;
                chk_serach_normal.Checked = isChecked;
                chk_kitchen_normal.Checked = isChecked;
                chk_order_normal.Checked = isChecked;
                chk_report_normal.Checked = isChecked;
                chk_billing_normal.Checked = isChecked;
                chk_emp_normal.Checked = isChecked;
                chk_oper_normal.Checked = isChecked;
            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage page chk_normal_all_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void txtperpagecount_Click(object sender, EventArgs e)
        {
            if (Enb_Home_Number)
            {
                decimal kdata = 0;
                using (FoodandBeverage.Forms.UserControl.Frmnumdiskeypad frm = new FoodandBeverage.Forms.UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.Number)))
                {
                    frm.ShowDialog();
                    kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                    frm.Dispose();
                }
                if (sender is TextBox txt)
                {
                    txt.Text = kdata.ToString("0.##");
                }
            }
        }

        #region previous and next
        private void btn_previous_Click(object sender, EventArgs e)
        {
            previouspage();
        }

        public void previouspage()
        {
            btn_previous.Visible = false;
            btn_next.Visible = true;

            tlp_Fristpage.Visible = true;
            tlp_secondpage.Visible = false;
        }
        private void btn_next_Click(object sender, EventArgs e)
        {
            btn_previous.Visible = true;
            btn_next.Visible = false;

            tlp_Fristpage.Visible = false;
            tlp_secondpage.Visible = true;
        }

        #endregion

        public bool MandatoryCheck()
        {
            try
            {
                if (Convert.ToInt32(cmb_bill_lang_type.SelectedIndex) == Convert.ToInt32(SalePrintbilltype.English_Arabic))
                {
                    if (Convert.ToInt32(cmb_taxcate.SelectedIndex) == -1)
                    {

                        MessageBox.Show("Please Select GST or TRN and all red color fields are mandatory",
                                        "Mandatory Field",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        return false;

                    }

                    //if (string.IsNullOrWhiteSpace(txt_cmpname.Text))
                    //{
                    //    MessageBox.Show("Enter Company Name and all red color fields are mandatory",
                    //                    "Mandatory Field",
                    //                    MessageBoxButtons.OK,
                    //                    MessageBoxIcon.Warning);
                    //    txt_cmpname.Focus();
                    //    return false;
                    //}

                    //if (string.IsNullOrWhiteSpace(txt_cmpaddress.Text))
                    //{
                    //    MessageBox.Show("Enter Company Address and all red color fields are mandatory",
                    //                    "Mandatory Field",
                    //                    MessageBoxButtons.OK,
                    //                    MessageBoxIcon.Warning);
                    //    txt_cmpaddress.Focus();
                    //    return false;
                    //}

                    //if (string.IsNullOrWhiteSpace(txt_footer_lang.Text))
                    //{
                    //    MessageBox.Show("Enter Footer Language and all red color fields are mandatory",
                    //                    "Mandatory Field",
                    //                    MessageBoxButtons.OK,
                    //                    MessageBoxIcon.Warning);
                    //    txt_footer_lang.Focus();
                    //    return false;
                    //}
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage page MandatoryCheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
                return false;
            }
        }
        private void btn_bill_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {
                    DmnGeneralSetting dmngen = new DmnGeneralSetting();
                    Bllgeneralsetting bllgen = new Bllgeneralsetting();
                    if (string.IsNullOrWhiteSpace(txtgid.Text))
                        txtgid.Text = "0";

                    dmngen.Gid = Convert.ToInt32(txtgid.Text);
                    dmngen.PrintBillType = Convert.ToInt32(cmb_bill_lang_type.SelectedIndex);
                    dmngen.BillTaxType = Convert.ToInt32(cmb_taxcate.SelectedValue);

                    #region Upload logo DB
                    if (pblogobrowse.Image != null)
                    {
                        if (!System.IO.Directory.Exists(Compressedimg))
                        {
                            System.IO.Directory.CreateDirectory(Compressedimg);
                            System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(Compressedimg);
                            System.Security.AccessControl.DirectorySecurity dSecurity = dInfo.GetAccessControl();
                            dSecurity.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(new System.Security.Principal.SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                            dInfo.SetAccessControl(dSecurity);
                        }
                        else
                        {
                            foreach (string file in Directory.GetFiles(Compressedimg))
                            {
                                System.GC.Collect();
                                System.GC.WaitForPendingFinalizers();
                                File.Delete(file);
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtimgpath.Text) && File.Exists(txtimgpath.Text))
                        {
                            dmngen.Logostatus = 1;
                            if (!Directory.Exists(FrmLogin.logoPath))
                            {
                                Directory.CreateDirectory(FrmLogin.logoPath);
                            }

                            string savePath = Path.Combine(FrmLogin.logoPath, "sblogo.png");
                            if (File.Exists(savePath))
                            {
                                File.Delete(savePath);
                            }
                            pblogobrowse.Image.Save(savePath, System.Drawing.Imaging.ImageFormat.Png);

                            ClsImgComprossor.CompressImage(txtimgpath.Text, Compressedimg, 30);
                            Bitmap bitmap = new Bitmap(Compressedimg + "\\Image.jpeg");

                            byte[] imageData;
                            using (var ms = new MemoryStream())
                            {
                                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                imageData = ms.ToArray();
                            }
                            dmngen.BillLogoicon = imageData.ToArray();
                            dmngen.Logostatus = 1;
                        }
                        else
                        {

                            dmngen.Logostatus = 0;

                            using (Bitmap bitmap = new Bitmap(pblogobrowse.Image))
                            {
                                byte[] imageData;
                                using (var ms = new MemoryStream())
                                {
                                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    imageData = ms.ToArray();
                                }
                                dmngen.BillLogoicon = imageData;
                            }
                        }

                    }
                    else
                    {
                        // No logo selected – send null to SQL
                        dmngen.Logostatus = 0;
                        dmngen.BillLogoicon = null;
                    }
                    //else
                    //{
                    //    MessageBox.Show("Please select a logo image before uploading.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}

                    #endregion

                    dmngen.FooterContent = txt_footer.Text;
                    dmngen.PhoneNoLength = Convert.ToInt32(txt_phno.Text);
                    dmngen.ZipCodeLength = Convert.ToInt32(txt_zip.Text);

                    dmngen.BranchId = FrmDashboard.BranchId;
                    dmngen.CreatedBy = FrmDashboard.UserId;
                    dmngen.EmployeeId = FrmDashboard.UserId;
                    dmngen.Status = 1;

                    dmngen.FooterOtherLanguage = txt_footer_lang.Text.Trim();
                    dmngen.CompanyAddressOtherLanguage = txt_cmpaddress.Text.Trim();
                    dmngen.CompanyNameOtherLanguage = txt_cmpname.Text.Trim();

                    var InsertStatus = bllgen.AddEditDataGenConf("prdInsertUpdateGenConf", dmngen);
                    if (InsertStatus.Gid > 0)
                    {
                        MessageBox.Show("General settings saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FetchBillDetails();
                    }

                    if (!System.IO.Directory.Exists(FrmLogin.logoPath))
                    {
                        System.IO.Directory.CreateDirectory(FrmLogin.logoPath);
                    }
                    string targetFile = System.IO.Path.Combine(FrmLogin.logoPath, "companylogo.png");
                    //System.IO.File.Copy(logoname, targetFile, true);
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage btnGen_conf_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void btn_billsett_cls_Click(object sender, EventArgs e)
        {
            pnl_billsetting.Visible = false;
        }
        public void combofill()
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = " GTypeId  as Id,Caption as Value";
                objSearch.Table = "tblGeneralOptions";
                objSearch.WhereNdGroupBy = " where status=1 and GTypeId=69";
                objSearch.OrderBy = "order by Id desc";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_bill_lang_type.DataSource = gridSource;
                cmb_bill_lang_type.ValueMember = "Id";
                cmb_bill_lang_type.DisplayMember = "Value";
                cmb_bill_lang_type.SelectedIndex = -1;

                objSearch.Select = " GTypeSubId  as Id,Caption as Value";
                objSearch.Table = "tblGeneralOptions";
                objSearch.WhereNdGroupBy = " where status=1 and GTypeId=70";
                objSearch.OrderBy = "order by Id ";
                var gridSourcetax = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_taxcate.DataSource = gridSourcetax;
                cmb_taxcate.ValueMember = "Id";
                cmb_taxcate.DisplayMember = "Value";
                cmb_taxcate.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage combofill", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void txt_phno_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (like Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_zip_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmb_bill_lang_type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmb_bill_lang_type.SelectedIndex) == Convert.ToInt32(SalePrintbilltype.English_Arabic))
            {
                tbl_Sub.Visible = true;
            }
            else if (Convert.ToInt32(cmb_bill_lang_type.SelectedIndex) == Convert.ToInt32(SalePrintbilltype.English))
            {
                tbl_Sub.Visible = false;
            }
        }

        private void pblogobrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Select Logo Image";
                    ofd.Filter = "PNG Files (*.png)|*.png";  // Only PNG

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo fileInfo = new FileInfo(ofd.FileName);
                        long expectedSize = 431 * 1024;
                        long tolerance = 5 * 1024;
                        using (Image img = Image.FromFile(ofd.FileName))
                        {
                            if (Convert.ToInt32(cmb_bill_lang_type.SelectedIndex) == 1)
                            {
                                pblogobrowse.Image = new Bitmap(img);
                                pblogobrowse.SizeMode = PictureBoxSizeMode.StretchImage;
                                logoname = ofd.FileName;
                                txtimgpath.Text = ofd.FileName;
                            }
                            else
                            {
                                if (img.Width == 2938 && img.Height == 2463 &&
                                fileInfo.Length >= (expectedSize - tolerance) &&
                                fileInfo.Length <= (expectedSize + tolerance))
                                {
                                    pblogobrowse.Image = new Bitmap(img);
                                    pblogobrowse.SizeMode = PictureBoxSizeMode.StretchImage;
                                    logoname = ofd.FileName;
                                    txtimgpath.Text = ofd.FileName;
                                }
                                else
                                {
                                    MessageBox.Show("Invalid image! Please select a PNG image with:\n" +
                                        "• Dimensions: 2498 × 2463 pixels\n" +
                                        "• Size: ~430 KB",
                                        "Invalid Image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage pblogobrowse_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void txtdefgmail_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Home_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void ib_drawer_Click(object sender, EventArgs e)
        {
            try
            {
                AssigndrawerCheck();
            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage page ib_drawer_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void ib_appcolor_Click(object sender, EventArgs e)
        {
            chkdef.Checked = false;
            AppColorSetting();
            pnlappcolor.Visible = true;
            pnlappcolor.Location = new Point(this.ClientSize.Width / 2 - pnlappcolor.Size.Width / 2, this.ClientSize.Height / 2 - pnlappcolor.Size.Height / 2);
            pnlappcolor.BringToFront();
        }

        private void ib_billsetting_Click(object sender, EventArgs e)
        {
            try
            {
                FetchBillDetails();
                //tlp_main_sidebar_pnldesktop.Enabled = false;
                pnl_billsetting.Visible = true;
                pnl_billsetting.Location = new Point(this.ClientSize.Width / 2 - pnl_billsetting.Size.Width / 2, this.ClientSize.Height / 2 - pnl_billsetting.Size.Height / 2);
                pnl_billsetting.BringToFront();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  ib_billsetting_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void ib_gensetting_Click(object sender, EventArgs e)
        {
            try
            {
                Generalsettingcheck();
                pnlgenralsetting.Left = (this.ClientSize.Width - pnlgenralsetting.Width) / 2;
                pnlgenralsetting.Top = (this.ClientSize.Height - pnlgenralsetting.Height) / 2;
                pnlgenralsetting.Visible = true;
                pnlgenralsetting.BringToFront();
            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage page ib_gensetting_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void ib_virtualkeybpard_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_virtualkeyboard.Left = (this.ClientSize.Width - pnl_virtualkeyboard.Width) / 2;
                pnl_virtualkeyboard.Top = (this.ClientSize.Height - pnl_virtualkeyboard.Height) / 2;

                pnl_virtualkeyboard.Visible = true;
                pnl_virtualkeyboard.BringToFront();
            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage page ib_virtualkeybpard_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void ib_msgsetting_Click(object sender, EventArgs e)
        {
            try
            {
                //tlp_main_sidebar_pnldesktop.Enabled = false;
                panl_msgsettings.Visible = true;
                panl_msgsettings.Location = new Point(this.ClientSize.Width / 2 - panl_msgsettings.Size.Width / 2, this.ClientSize.Height / 2 - panl_msgsettings.Size.Height / 2);
                panl_msgsettings.BringToFront();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("Home Page  ib_msgsetting_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void FrmDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                AppSession.UnregisterForm(this);
            }
        }

        public void FetchBillDetails()
        {
            try
            {

                BllSearch<DmnGeneralSetting> bllSearch = new BllSearch<DmnGeneralSetting>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblGeneralSetting";
                objSearch.WhereNdGroupBy = "WHERE BranchId = " + FrmDashboard.BranchId + " AND Status = 1";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    txtgid.Text = Convert.ToString(itemSource[0].Gid);
                    if (Convert.ToInt32(itemSource[0].PrintBillType) == Convert.ToInt32(SalePrintbilltype.English_Arabic))
                    {
                        tbl_Sub.Visible = true;
                    }

                    cmb_bill_lang_type.SelectedIndex = Convert.ToInt32(itemSource[0].PrintBillType);
                    cmb_taxcate.SelectedValue = Convert.ToInt64(itemSource[0].BillTaxType);

                    txt_footer.Text = Convert.ToString(itemSource[0].FooterContent);
                    txt_phno.Text = Convert.ToString(itemSource[0].PhoneNoLength);
                    txt_zip.Text = Convert.ToString(itemSource[0].ZipCodeLength);

                    txt_footer_lang.Text = Convert.ToString(itemSource[0].FooterOtherLanguage);
                    txt_cmpname.Text = Convert.ToString(itemSource[0].CompanyNameOtherLanguage);
                    txt_cmpaddress.Text = Convert.ToString(itemSource[0].CompanyAddressOtherLanguage);

                    if (itemSource[0].BillLogoicon != null && itemSource[0].BillLogoicon.Length > 0)
                    {
                        Bitmap bitmap = null;
                        byte[] logoData = itemSource[0].BillLogoicon;
                        var mStream = new MemoryStream(logoData);
                        bitmap = new Bitmap(mStream);
                        pblogobrowse.Image = bitmap;
                        pblogobrowse.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                    {
                       // pblogobrowse.Image = Properties.Resources.logobrowse;
                    }


                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage FetchBillDetails", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }

        private void btncls_gen_Click(object sender, EventArgs e)
        {
            pnlgenralsetting.Visible = false;
        }

        public void StartTimerAndRefreshLabel()
        {
            try
            {
                tlp_tablestatus.SuspendLayout();
                startTime = DateTime.Now;
                reset_sync.Stop();
                time_syn_lal.Start();
                lblsync.Text = "Synchronizing... ";
                tlp_tablestatus.ResumeLayout();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("homepage StartTimerAndRefreshLabel", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }

        private void reset_sync_Tick(object sender, EventArgs e)
        {
            try
            {
                tlp_tablestatus.SuspendLayout();
                TimeSpan elapsedTime = DateTime.Now.Subtract(startTime);
                string formattedTime = string.Format("{0:D2}:{1:D2}:{2}",
                                                     elapsedTime.Hours,
                                                     elapsedTime.Minutes,
                                                     elapsedTime.Seconds );
                lblsync.Text = $"Synchronized {formattedTime} ago";
                tlp_tablestatus.ResumeLayout();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("homepage reset_sync_Tick", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
        #region Keyboard
        public void ShowKeyboard(Control activeControl, int kbtype)
        {
            try
            {
                if (activeControl == null) return;

                if (activeControl is TextBox activeTextBox)
                {
                    Point textBoxScreenLocation = activeTextBox.PointToScreen(Point.Empty);
                    int desiredX = 0, desiredY = 0;
                    if (UserControl.Frmkeyboard.IsOpen)
                    {
                        UserControl.Frmkeyboard.Instance.Close();
                    }

                    UserControl.Frmkeyboard frmKey = new UserControl.Frmkeyboard(kbtype);

                    #region Calculate Position of Keyboard 
                    desiredX = textBoxScreenLocation.X + (activeTextBox.Width / 2) - (UserControl.Frmkeyboard.Instance.Width / 2);
                    desiredY = textBoxScreenLocation.Y + activeTextBox.Height + 5;
                    Rectangle screenBounds = Screen.FromControl(activeTextBox).WorkingArea;
                    if (desiredX < screenBounds.Left)
                        desiredX = screenBounds.Left;
                    else if (desiredX + UserControl.Frmkeyboard.Instance.Width > screenBounds.Right)
                        desiredX = screenBounds.Right - UserControl.Frmkeyboard.Instance.Width;
                    if (desiredY + UserControl.Frmkeyboard.Instance.Height > screenBounds.Bottom)
                    {
                        desiredY = textBoxScreenLocation.Y - UserControl.Frmkeyboard.Instance.Height - 5;
                        if (desiredY < screenBounds.Top)
                            desiredY = screenBounds.Top;
                    }
                    UserControl.Frmkeyboard.Instance.StartPosition = FormStartPosition.Manual;
                    UserControl.Frmkeyboard.Instance.Location = new Point(desiredX, desiredY);
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
                        else if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.keyf8))
                        {
                            //if (Convert.ToInt32(activeTextBox.Tag) == 1)
                            //    StaffPopup();
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
            catch (Exception ex)
            {
                clsgen.LogException("homepage ShowKeyboard", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }

        }
        #endregion


    }

}
