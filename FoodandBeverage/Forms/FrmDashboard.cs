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

namespace FoodandBeverage.Forms
{
    public partial class FrmDashboard : Form
    {

        public static int BranchId = 0;
        public static int UserId = 0;
        public static int UserType = 0;
        public static int Allaccess = 0;

        public static string KitchenBillPath = "";
        public static string ReportPath = "";
        public static string SaleBillPath = "";

        public FrmDashboard()
        {
            InitializeComponent();
            UserId = Convert.ToInt32(FrmLogin.UserId);
            BranchId = Convert.ToInt32(FrmLogin.BranchId);
            UserType = Convert.ToInt32(FrmLogin.UserAccountTypeId);

            KitchenBillPath = Convert.ToString(FrmLogin.KitchenBillPath);
            SaleBillPath = Convert.ToString(FrmLogin.SaleBillPath);
            SaleBillPath = Convert.ToString(FrmLogin.SaleBillPath);
        }

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
        #region grid Declaration
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
                }
            }
            catch (Exception ex)
            {

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
            lbluser_designation.Text =Convert.ToString(FrmLogin.EmployeName ) + "\n"+ Convert.ToString(FrmLogin.Designation);
            this.Opacity = 0;
            Task.Run(() =>
            {
                loadingForm = new LoadingGIF_Timer();
                loadingForm.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(loadingForm);
            });
            LoadMainForm();
            //await Task.Delay(10000); 
            if (loadingForm != null && !loadingForm.IsDisposed)
            {
                loadingForm.Invoke(new Action(() => loadingForm.Close()));
            }
            this.Opacity = 100;
        }


        private void LoadMainForm()
        {
            try
            {
                panelDesktop.Controls.Add(tlp_op_menu);
                tlp_op_menu.Dock = DockStyle.Fill;
                tlp_op_menu.Visible = true;

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

                txtname_search.Text = "Food Name .....";
                txtitemcode_serach.Text = "Item Code .....";
                tlp_main.BackColor = menubuttoncolor;
                flp_navbar.BackColor = menubuttoncolor;
                btn_minimium.BackColor = menubuttoncolor;
                Menuhome.Focus();
                this.DoubleBuffered = true;
                RoundPictureBoxCorners(pb_appicon, 10);
                //pbappcoloepicker.BackColor = Color.FromArgb(34, 46, 80);
                Roundform(30);
                // tlp_main.BackColor = Color.FromArgb(152, 193, 217);
                //set color on solidebar header
                pnl_header.BackColor = menubuttoncolor;
                pbopbuttoncoloepicker.BackColor = menubuttoncolor;
                pbsidebuttoncoloepicker.BackColor = menubuttoncolor;

                tlp_main_sidebar_pnldesktop.BackColor = Color.FromArgb(34, 46, 80);
                this.FormBorderStyle = FormBorderStyle.None;
                Menuhome.BackColor = menubuttoncolor;
                Menuoperation.BackColor = menubuttoncolor;
                Menubilling.BackColor = menubuttoncolor;
                Menureport.BackColor = menubuttoncolor;
                Menulogout.BackColor = menubuttoncolor;
                Menusetting.BackColor = menubuttoncolor;
                menuEmployee.BackColor = menubuttoncolor;
                Menutable.BackColor = menubuttoncolor;
                button_imageset();
                //flp_menu.BackColor = Color.White;
                tlp_main_sidebar_pnldesktop.CellPaint += new TableLayoutCellPaintEventHandler(this.tlp_main_CellPaint);

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
                textb_shape();
                AppColorSetting();
                //flpEmployee.Visible = false;
                //ib_employee.Visible = false;
                ib_designation.Visible = false;
                ib_department.Visible = false;
                ib_useraccount.Visible = false;
                ib_userpermission.Visible = false;
                ib_Discount_offer.Visible = false;
                ib_Expence.Visible = false;
                ib_PromoCode.Visible = false;
                FormPermissionChecking();
                #region Load Chart Form
                panelDesktop.Controls.Clear();

                if (System.Windows.Forms.Application.OpenForms["FrmDashboardReport"] != null)
                {
                    FrmDashboardReport _instanceDashboardReport = Application.OpenForms["FrmDashboardReport"] as FrmDashboardReport;
                    if (_instanceDashboardReport.WindowState == FormWindowState.Minimized)
                        _instanceDashboardReport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
                    panelDesktop.Controls.Add(_instanceDashboardReport);
                    _instanceDashboardReport.Show();
                    // _instanceDashboardReport.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceDashboardReport.BringToFront();
                }
                #endregion

                //if (System.Windows.Forms.Application.OpenForms["LoadingGIF_Timer"] != null)
                //{
                //    FrmRegistration _instanceLoadingGIF = Application.OpenForms["LoadingGIF_Timer"] as FrmRegistration;
                //    _instanceLoadingGIF.Close();
                //}
                //this.Opacity = 100;

            }
            catch (Exception ex)
            {

            }
        }



        #region Color Setting
        private bool IsValidHtmlColor(string color)
        {
            try
            {
                ColorTranslator.FromHtml(color);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void AppColorSetting()
        {
            try
            {
                BllSearch<DmnSetting> bllSearch = new BllSearch<DmnSetting>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblsettings";
                dmnSearch.WhereNdGroupBy = " where Status=1 ";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    if (gridSource[0].APPColor == null && gridSource[0].OperationButtonColor == null && gridSource[0].MenuButtonColor == null)
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
                        Menuhome.BackColor = menubuttoncolor;
                        Menuoperation.BackColor = menubuttoncolor;
                        Menubilling.BackColor = menubuttoncolor;
                        Menureport.BackColor = menubuttoncolor;
                        Menulogout.BackColor = menubuttoncolor;
                        Menusetting.BackColor = menubuttoncolor;
                        menuEmployee.BackColor = menubuttoncolor;
                        Menutable.BackColor = menubuttoncolor;

                        tlp_main_sidebar_pnldesktop.BackColor = Color.FromArgb(34, 46, 80);
                        pbappcoloepicker.BackColor = Color.FromArgb(34, 46, 80);

                        if (tlp_main_sidebar_pnldesktop.BackColor.Name.Contains("ff"))
                            txtoperbuttoncolorname.Text = "#" + pbopbuttoncoloepicker.BackColor.Name.ToString().Remove(0, 2);
                        if (tlp_main_sidebar_pnldesktop.BackColor.Name.Contains("ff"))
                            txtmenubuttoncolorname.Text = "#" + pbsidebuttoncoloepicker.BackColor.Name.ToString().Remove(0, 2);
                        if (tlp_main_sidebar_pnldesktop.BackColor.Name.Contains("ff"))
                            txtappcolorname.Text = "#" + tlp_main_sidebar_pnldesktop.BackColor.Name.ToString().Remove(0, 2);

                        Button btnsetting = this.Controls.Find("Appcolor", true).FirstOrDefault() as Button;
                        if (btnsetting != null)
                            btnsetting.BackColor = opbuttoncolor;
                    }
                    else
                    {
                        Color colorapp, coloropera, colormenu;
                        if (IsValidHtmlColor(gridSource[0].APPColor))
                            colorapp = System.Drawing.ColorTranslator.FromHtml(gridSource[0].APPColor);
                        else
                            colorapp = Color.FromArgb(34, 46, 80);
                        // opbuttoncolor = Color.FromArgb(0x42, 0x67, 0xB2); ;  menubuttoncolor = Color.FromArgb(0x42, 0x67, 0xB2); ;

                        if (IsValidHtmlColor(gridSource[0].OperationButtonColor))
                            coloropera = System.Drawing.ColorTranslator.FromHtml(gridSource[0].OperationButtonColor);
                        else
                            coloropera = Color.FromArgb(0x42, 0x67, 0xB2);

                        if (IsValidHtmlColor(gridSource[0].MenuButtonColor))
                            colormenu = System.Drawing.ColorTranslator.FromHtml(gridSource[0].MenuButtonColor);
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
                        pbopbuttoncoloepicker.BackColor = opbuttoncolor;

                        Menuhome.BackColor = menubuttoncolor;
                        Menuoperation.BackColor = menubuttoncolor;
                        Menubilling.BackColor = menubuttoncolor;
                        Menureport.BackColor = menubuttoncolor;
                        Menulogout.BackColor = menubuttoncolor;
                        Menusetting.BackColor = menubuttoncolor;
                        menuEmployee.BackColor = menubuttoncolor;
                        Menutable.BackColor = menubuttoncolor;
                        pbsidebuttoncoloepicker.BackColor = menubuttoncolor;

                        pbappcoloepicker.BackColor = colorapp;
                        tlp_main_sidebar_pnldesktop.BackColor = colorapp;
                    }

                }
                else
                {
                    if (tlp_main_sidebar_pnldesktop.BackColor.Name.Contains("ff"))
                        txtoperbuttoncolorname.Text = "#" + pbopbuttoncoloepicker.BackColor.Name.ToString().Remove(0, 2);
                    if (tlp_main_sidebar_pnldesktop.BackColor.Name.Contains("ff"))
                        txtmenubuttoncolorname.Text = "#" + pbsidebuttoncoloepicker.BackColor.Name.ToString().Remove(0, 2);
                    if (tlp_main_sidebar_pnldesktop.BackColor.Name.Contains("ff"))
                        txtappcolorname.Text = "#" + pbappcoloepicker.BackColor.Name.ToString().Remove(0, 2);
                    txtsid.Text = "0";
                }
            }
            catch (Exception ex)
            {

            }


        }
        #endregion
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtitemcode_serach, txtname_search })
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

        public void SetButtonImage(Button button, Image originalImage, int desiredWidth, int desiredHeight)
        {
            Image resizedImage = new Bitmap(originalImage, new Size(desiredWidth, desiredHeight));
            button.Image = resizedImage;
        }

        public void button_imageset()
        {
            try
            {
                SetButtonImage(Menuhome, FoodandBeverage.Properties.Resources.icons8_home_50, desiredWidth, desiredHeight);
                SetButtonImage(Menuoperation, FoodandBeverage.Properties.Resources.operation, desiredWidth, desiredHeight);
                SetButtonImage(Menubilling, FoodandBeverage.Properties.Resources.bill, desiredWidth, desiredHeight);
                SetButtonImage(Menureport, FoodandBeverage.Properties.Resources.report, desiredWidth, desiredHeight);
                SetButtonImage(Menusetting, FoodandBeverage.Properties.Resources.setting, desiredWidth, desiredHeight);
                SetButtonImage(Menulogout, FoodandBeverage.Properties.Resources.logout, desiredWidth, desiredHeight);
                SetButtonImage(menuEmployee, FoodandBeverage.Properties.Resources.Employee, desiredWidth, desiredHeight);
                SetButtonImage(Menutable, FoodandBeverage.Properties.Resources.menutable, desiredWidth, desiredHeight);

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

                SetButtonImage(ib_employee, FoodandBeverage.Properties.Resources.Employee, desiredWidth, desiredHeight);
                SetButtonImage(ib_department, FoodandBeverage.Properties.Resources.add_des_dep_user, desiredWidth, desiredHeight);
                SetButtonImage(ib_designation, FoodandBeverage.Properties.Resources.add_des_dep_user, desiredWidth, desiredHeight);
                SetButtonImage(ib_useraccount, FoodandBeverage.Properties.Resources.add_des_dep_user, desiredWidth, desiredHeight);
                SetButtonImage(ib_reservation, FoodandBeverage.Properties.Resources.reserved, desiredWidth, desiredHeight);
                SetButtonImage(ib_userpermission, FoodandBeverage.Properties.Resources.userpermisson, desiredWidth, desiredHeight);
                SetButtonImage(ib_addon, FoodandBeverage.Properties.Resources.addon, desiredWidth, desiredHeight);

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

        private void ChangeButtonBorder(Button button)
        {
            try
            {
                tlp_empl_menu.Visible = false;
                //foreach (Button menuButton in flp_menu.Controls.OfType<Button>())
                //{
                //    menuButton.FlatAppearance.BorderColor = Color.White;
                //    menuButton.FlatAppearance.BorderSize = 0;
                //    menuButton.BackColor = Color.White;
                //    menuButton.ForeColor = Color.Black;
                //    menuButton.BackColor = menubuttoncolor;
                //    menuButton.ForeColor = Color.White;
                //}
                //button.BackColor = Color.White;
                //button.ForeColor = Color.Black;
                //button.FlatAppearance.BorderColor = Color.Black;
                //button.FlatAppearance.BorderSize = 1;
            }
            catch (Exception ex)
            {

            }

        }

        private void Menuhome_Click(object sender, EventArgs e)
        {
            try
            {

                //if (screenWidth > 1367 && screenHeight > 769)
                //{
                //    foreach (Control control in flp_operations.Controls)
                //    {
                //        if (control is Button buttonm)
                //        {
                //            buttonm.Size = new Size(300, 120);
                //        }
                //    }
                //}
                //else
                //{
                //    foreach (Control control in flp_operations.Controls)
                //    {
                //        if (control is Button buttonm)
                //        {
                //            buttonm.Size = new Size(212, 89);
                //        }
                //    }
                //}
                panelDesktop.SuspendLayout();

                if (sender is Button button)
                {
                    ChangeButtonBorder(button);
                }
                panelDesktop.Controls.Clear();
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

                if (menutable_click)
                {
                    CloseTableViewPage();
                }
                panelDesktop.ResumeLayout();
            }
            catch (Exception ex)
            {

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
            }
        }
        private void RoundPictureBoxCorners(PictureBox pictureBox, int cornerRadius)
        {
            try
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90); // Top-left corner
                path.AddArc(pictureBox.Width - 2 * cornerRadius, 0, cornerRadius * 2, cornerRadius * 2, 270, 90); // Top-right corner
                path.AddArc(pictureBox.Width - 2 * cornerRadius, pictureBox.Height - 2 * cornerRadius, cornerRadius * 2, cornerRadius * 2, 0, 90); // Bottom-right corner
                path.AddArc(0, pictureBox.Height - 2 * cornerRadius, cornerRadius * 2, cornerRadius * 2, 90, 90); // Bottom-left corner
                path.CloseFigure();

                pictureBox.Region = new Region(path);
                pb_appicon.Location = new Point((pnl_header.Width - pb_appicon.Width) / 2, (pnl_header.Height - pb_appicon.Height) / 2);
            }
            catch (Exception ex)
            {

            }

        }

        private void CollapseMenu()
        {
            try
            {
                if (menutable_click)
                {
                    if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
                    {
                        FrmTableView _instanceFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                        //_instanceFrmTableView.AutoTimer.Stop();
                        _instanceFrmTableView.Close();
                        _instanceFrmTableView.Dispose();
                        GC.Collect();
                    }
                }

                if (Menuhome.Width > 150) //Collapse menu
                {
                    flp_menu.Dock = DockStyle.None;
                    flp_menu.Width = 108;
                    panelMenu.Width = 108;
                    panelMenu.Dock = DockStyle.Fill;
                    flp_menu.Dock = DockStyle.Fill;
                    pnl_header.Width = 95;
                    pb_appicon.Dock = DockStyle.Fill;


                    foreach (Button menuButton in flp_menu.Controls.OfType<Button>())
                    {
                        menuButton.Text = "";
                        menuButton.Width = 80;
                        menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                        menuButton.Padding = new Padding(20, 0, 0, 0);
                    }
                    tlp_main_sidebar_pnldesktop.ColumnStyles[1] = new ColumnStyle(SizeType.AutoSize);
                    tlp_main_sidebar_pnldesktop.ColumnStyles[2] = new ColumnStyle(SizeType.AutoSize);
                    RoundPictureBoxCorners(pb_appicon, 10);
                }
                else
                { //Expand menu
                    flp_menu.Dock = DockStyle.Fill;
                    pnl_header.Width = 179;
                    panelMenu.Width = 180;
                    pb_appicon.Width = 97;

                    foreach (Button menuButton in flp_menu.Controls.OfType<Button>())
                    {
                        menuButton.Width = 162;
                        menuButton.Text = menuButton.Tag.ToString();
                        menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                        menuButton.Padding = new Padding(10, 0, 0, 0);
                    }
                    tlp_main_sidebar_pnldesktop.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 18.23f);
                    tlp_main_sidebar_pnldesktop.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 81.27f);
                    RoundPictureBoxCorners(pb_appicon, 10);

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void Menuoperation_Click(object sender, EventArgs e)
        {
            try
            {
                panelDesktop.SuspendLayout();
                if (menutable_click)
                {
                    CloseTableViewPage();
                }

                panelDesktop.Controls.Clear();

                ib_Company.Visible = true;

                if (sender is Button button)
                {
                    ChangeButtonBorder(button);
                }
                panelDesktop.Controls.Add(tlp_op_menu);
                tlp_op_menu.Dock = DockStyle.Fill;
                tlp_op_menu.Visible = true;

                if (screenWidth > 1367 && screenHeight > 769)
                {
                    desiredWidth = 52;
                    desiredHeight = 52;
                    HdesiredWidth = 60;
                    HdesiredHeight = 60;
                    //foreach (Control control in flp_operations.Controls)
                    //{
                    //    if (control is Button buttonm)
                    //    {
                    //        buttonm.Size = new Size(300, 120);
                    //        buttonm.Margin = new Padding(105, 35, 10, 2);
                    //        buttonm.Font = new Font(buttonm.Font.FontFamily, 16, buttonm.Font.Style);
                    //    }
                    //}

                }
                else
                {
                    desiredWidth = 32;
                    desiredHeight = 32;
                    HdesiredWidth = 40;
                    HdesiredHeight = 40;
                    //foreach (Control control in flp_operations.Controls)
                    //{
                    //    if (control is Button buttonm)
                    //    {
                    //        buttonm.Size = new Size(212, 89);
                    //    }
                    //}
                }
                panelDesktop.ResumeLayout();
            }
            catch (Exception ex)
            {

            }

        }


        private void Menubilling_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmDashboardReport"] != null)
                {
                    FrmDashboardReport _instanceFrmDashboardReport = Application.OpenForms["FrmDashboardReport"] as FrmDashboardReport;
                    _instanceFrmDashboardReport.Visible = false;
                }
                panelDesktop.SuspendLayout();

                if (tlp_op_menu.Visible)
                    tlp_op_menu.Visible = false;

                if (sender is Button button)
                {
                    ChangeButtonBorder(button);
                }
                if (System.Windows.Forms.Application.OpenForms["FrmCombinedBill"] != null)
                {
                    FrmCombinedBill _instanceCombinedBill = Application.OpenForms["FrmCombinedBill"] as FrmCombinedBill;
                    if (_instanceCombinedBill.WindowState == FormWindowState.Minimized)
                        _instanceCombinedBill.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceCombinedBill.Visible = true;
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
                menutable_click = true;
                panelDesktop.ResumeLayout();
            }
            catch (Exception ex)
            {

            }

        }


        private void Menureport_Click(object sender, EventArgs e)
        {
            try
            {
                panelDesktop.SuspendLayout();

                if (menutable_click)
                {
                    CloseTableViewPage();
                }

                if (sender is Button button)
                {
                    ChangeButtonBorder(button);
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


            }

        }
        public void CloseTableViewPage()
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
        }

        private void Menusetting_Click(object sender, EventArgs e)
        {
            try
            {
                if (menutable_click)
                {
                    CloseTableViewPage();
                }

                if (sender is Button button)
                {
                    ChangeButtonBorder(button);
                }
                panelDesktop.Controls.Clear();
                flpcolor.Controls.Clear();
                flpcolor.Dock = DockStyle.Fill;
                flpcolor.AutoScroll = true;

                int rowIndex = 0;
                Control control;
                int columnIndex = 0;
                #region Button App Color
                Button button_type = new Button();
                button_type.Text = "Appcolor";
                button_type.Name = "Appcolor";
                //button_type.BackgroundImage = new Bitmap(Application.StartupPath + "\\Background_Images\\bg4.png");
                //button_type.BackgroundImageLayout = ImageLayout.Stretch;
                button_type.FlatStyle = FlatStyle.Flat;
                button_type.BackColor = opbuttoncolor;
                button_type.ForeColor = System.Drawing.Color.White;
                button_type.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                if (screenWidth > 1367 && screenHeight > 769)
                {
                    button_type.Width = 350;
                    button_type.Height = 120;
                }
                else
                {
                    button_type.Width = 212;
                    button_type.Height = 89;
                }
                button_type.Margin = new Padding(20, 10, 0, 0);
                // Add the event handler for the button
                button_type.Click += new EventHandler(BgClickEventHandler);
                flpcolor.Controls.Add(button_type);
                #endregion

                columnIndex++;

                #region Button Message Setting
                Button button_message = new Button();
                button_message.Text = "Message";
                button_message.Name = "btn_Message";
                //button_message.BackgroundImage = new Bitmap(Application.StartupPath + "\\Background_Images\\bg4.png");
                //button_message.BackgroundImageLayout = ImageLayout.Stretch;
                button_message.FlatStyle = FlatStyle.Flat;
                button_message.BackColor = opbuttoncolor;
                button_message.ForeColor = System.Drawing.Color.White;
                button_message.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                if (screenWidth > 1367 && screenHeight > 769)
                {
                    button_message.Width = 350;
                    button_message.Height = 120;
                }
                else
                {
                    button_message.Width = 212;
                    button_message.Height = 89;
                }
                button_message.Margin = new Padding(20, 10, 0, 0);
                // Add the event handler for the button
                button_message.Click += new EventHandler(button_messageClickEventHandler);
                flpcolor.Controls.Add(button_message);
                columnIndex++;
                #endregion

                panelDesktop.Controls.Add(flpcolor);
            }
            catch (Exception)
            {

            }

        }
        private void button_messageClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                panl_msgsettings.Visible = true;
                panl_msgsettings.Location = new Point(this.ClientSize.Width / 2 - panl_msgsettings.Size.Width / 2, this.ClientSize.Height / 2 - panl_msgsettings.Size.Height / 2);
            }
            catch (Exception ex)
            {

            }
        }
        private void BgClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                AppColorSetting();
                pnlappcolor.Visible = true;
                pnlappcolor.Location = new Point(this.ClientSize.Width / 2 - pnlappcolor.Size.Width / 2, this.ClientSize.Height / 2 - pnlappcolor.Size.Height / 2);
            }
            catch (Exception ex)
            {

            }
        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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
            int borderRadius = size; // Adjust this value as needed for your desired corner radius

            Rectangle bounds = new Rectangle(0, 0, this.Width, this.Height);

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(bounds.Left, bounds.Top, borderRadius, borderRadius, 180, 90); // Top-left corner
                path.AddArc(bounds.Right - borderRadius, bounds.Top, borderRadius, borderRadius, 270, 90); // Top-right corner
                path.AddArc(bounds.Right - borderRadius, bounds.Bottom - borderRadius, borderRadius, borderRadius, 0, 90); // Bottom-right corner
                path.AddArc(bounds.Left, bounds.Bottom - borderRadius, borderRadius, borderRadius, 90, 90); // Bottom-left corner
                path.CloseFigure();

                this.Region = new Region(path);
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
            }
            catch (Exception ex)
            {

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
                    tlp_main_sidebar_pnldesktop.BackColor = selectedColor;
                    if (tlp_main_sidebar_pnldesktop.BackColor.Name.Contains("ff"))
                        txtappcolorname.Text = "#" + pbappcoloepicker.BackColor.Name.ToString().Remove(0, 2);
                }
            }
            catch (Exception ex)
            {

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
                        _instanceItemRegistartion.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
                    _instanceItemRegistartion.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceItemRegistartion.BringToFront();
                }
            }
            catch (Exception ex)
            {

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

        private void ib_ingrediant_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmIngredient"] != null)
                {
                    FrmIngredient _instanceFrmIngredient = Application.OpenForms["FrmIngredient"] as FrmIngredient;
                    if (_instanceFrmIngredient.WindowState == FormWindowState.Minimized)
                    {
                        _instanceFrmIngredient.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        //ResizeForm(_instanceFrmIngredient);
                    }
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
                    panelDesktop.Controls.Add(_instanceFrmIngredient);
                    _instanceFrmIngredient.Show();
                    _instanceFrmIngredient.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmIngredient.BringToFront();

                }
            }
            catch (Exception ex)
            {

            }

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

            }
        }



        private void ib_Category_MouseLeave(object sender, EventArgs e)
        {
            SetButtonImage(ib_Category, FoodandBeverage.Properties.Resources.category, desiredWidth, desiredHeight);
            ib_Category.Text = Convert.ToString(ib_Category.Tag);

        }


        private void ib_Item_MouseLeave(object sender, EventArgs e)
        {
            ib_Item.Text = Convert.ToString(ib_Item.Tag);
            SetButtonImage(ib_Item, FoodandBeverage.Properties.Resources.item, desiredWidth, desiredHeight);

        }


        private void ib_ingrediant_MouseLeave(object sender, EventArgs e)
        {
            ib_ingrediant.Text = Convert.ToString(ib_ingrediant.Tag);
            SetButtonImage(ib_ingrediant, FoodandBeverage.Properties.Resources.Product, desiredWidth, desiredHeight);

        }



        private void ib_menu_MouseLeave(object sender, EventArgs e)
        {
            ib_menu.Text = Convert.ToString(ib_menu.Tag);
            SetButtonImage(ib_menu, FoodandBeverage.Properties.Resources.menu, desiredWidth, desiredHeight);

        }


        private void ib_unit_MouseLeave(object sender, EventArgs e)
        {
            ib_unit.Text = Convert.ToString(ib_unit.Tag);
            SetButtonImage(ib_unit, FoodandBeverage.Properties.Resources.unit, desiredWidth, desiredHeight);

        }


        private void ib_table_MouseLeave(object sender, EventArgs e)
        {
            ib_table.Text = Convert.ToString(ib_table.Tag);
            SetButtonImage(ib_table, FoodandBeverage.Properties.Resources.table_reg, desiredWidth, desiredHeight);

        }

        private void ib_purchase_MouseLeave(object sender, EventArgs e)
        {
            ib_purchase.Text = Convert.ToString(ib_purchase.Tag);
            SetButtonImage(ib_purchase, FoodandBeverage.Properties.Resources.Purchase, desiredWidth, desiredHeight);

        }



        private void ib_purchasereturn_MouseLeave(object sender, EventArgs e)
        {
            ib_purchasereturn.Text = Convert.ToString(ib_purchasereturn.Tag);
            SetButtonImage(ib_purchasereturn, FoodandBeverage.Properties.Resources.Purchase_return, desiredWidth, desiredHeight);
        }

        private void ib_tax_MouseLeave(object sender, EventArgs e)
        {
            ib_tax.Text = Convert.ToString(ib_tax.Tag);
            SetButtonImage(ib_tax, FoodandBeverage.Properties.Resources.tax, desiredWidth, desiredHeight);
        }

        private void ib_Category_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                button.Text = string.Empty;
                Image originalImage = null;
                switch (button.Name)
                {
                    case "ib_Category":
                        originalImage = FoodandBeverage.Properties.Resources.category;
                        break;

                    case "ib_Item":
                        originalImage = FoodandBeverage.Properties.Resources.item;
                        break;

                    case "ib_Ingredient":
                        originalImage = FoodandBeverage.Properties.Resources.Product;
                        break;

                    case "ib_menu":
                        originalImage = FoodandBeverage.Properties.Resources.menu;
                        break;

                    case "ib_unit":
                        originalImage = FoodandBeverage.Properties.Resources.unit;
                        break;

                    case "ib_table":
                        originalImage = FoodandBeverage.Properties.Resources.table_reg;
                        break;

                    case "ib_purchase":
                        originalImage = FoodandBeverage.Properties.Resources.Purchase;
                        break;

                    case "ib_purchasereturn":
                        originalImage = FoodandBeverage.Properties.Resources.Purchase_return;
                        break;

                    case "ib_tax":
                        originalImage = FoodandBeverage.Properties.Resources.tax;
                        break;
                    case "ib_vendor":
                        originalImage = FoodandBeverage.Properties.Resources.vendericon;
                        break;
                    case "ib_withdrawal":
                        originalImage = FoodandBeverage.Properties.Resources.withdraw;
                        break;
                    case "ib_Discount":
                        originalImage = FoodandBeverage.Properties.Resources.discount;
                        break;
                    case "ib_orderview":
                        originalImage = FoodandBeverage.Properties.Resources.orderview;
                        break;
                    case "ib_kitchenview":
                        originalImage = FoodandBeverage.Properties.Resources.kitchen_order_view;
                        break;
                    case "ib_PromoCode":
                        originalImage = FoodandBeverage.Properties.Resources.promo_code;
                        break;
                    case "ib_Company":
                        originalImage = FoodandBeverage.Properties.Resources.comapny;
                        break;
                    case "ib_useraccount":
                        originalImage = FoodandBeverage.Properties.Resources.add_des_dep_user;
                        break;
                    case "ib_designation":
                        originalImage = FoodandBeverage.Properties.Resources.add_des_dep_user;
                        break;
                    case "ib_department":
                        originalImage = FoodandBeverage.Properties.Resources.add_des_dep_user;
                        break;
                    case "ib_employee":
                        originalImage = FoodandBeverage.Properties.Resources.Employee;
                        break;
                    case "ib_reservation":
                        originalImage = FoodandBeverage.Properties.Resources.reserved;
                        break;
                    case "ib_userpermission":
                        originalImage = FoodandBeverage.Properties.Resources.userpermisson;
                        break;
                    case "ib_addon":
                        originalImage = FoodandBeverage.Properties.Resources.addon;
                        break;
                }


                if (originalImage != null)
                {
                    button.BackgroundImage = null;
                    Image resizedImage = new Bitmap(originalImage, new Size(HdesiredWidth, HdesiredHeight));
                    button.Image = resizedImage;
                    //button.Image = ImageLayout.Center;
                }
            }
            catch (Exception ex)
            {

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

            }

        }

        private void ib_vendor_MouseLeave(object sender, EventArgs e)
        {
            ib_vendor.Text = Convert.ToString(ib_vendor.Tag);
            SetButtonImage(ib_vendor, FoodandBeverage.Properties.Resources.vendericon, desiredWidth, desiredHeight);
        }

        private void btn_tableorder_Click(object sender, EventArgs e)
        {
            Control controlToRemove = tlp_main.GetControlFromPosition(0, 1);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_main_sidebar_pnldesktop);
            tlp_main_sidebar_pnldesktop.Dock = DockStyle.Fill;
            tlp_main_sidebar_pnldesktop.Visible = true;
            pnl_tableorder.Visible = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    if (pnl_tableorder.Visible)
                    {
                        Control controlToRemove = tlp_main.GetControlFromPosition(0, 1);
                        tlp_main.Controls.Remove(controlToRemove);
                        tlp_main.Controls.Add(tlp_main_sidebar_pnldesktop);
                        tlp_main_sidebar_pnldesktop.Dock = DockStyle.Fill;
                        tlp_main_sidebar_pnldesktop.Visible = true;
                    }
                    else if (pnlappcolor.Visible)
                        pnlappcolor.Visible = false;
                    else if (panl_msgsettings.Visible)
                        panl_msgsettings.Visible = false;
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
                                    "FrmUserPermission", "FrmVendor","FrmExpence"
                                };

                        if (formsToCheck.Contains(formName))
                        {
                            openForm.Close();
                            openForm.Dispose();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ib_Discount_MouseLeave(object sender, EventArgs e)
        {
            ib_Discount_offer.Text = Convert.ToString(ib_Discount_offer.Tag);
            SetButtonImage(ib_Discount_offer, FoodandBeverage.Properties.Resources.discount, desiredWidth, desiredHeight);
        }
        private void ib_withdrawal_MouseLeave(object sender, EventArgs e)
        {
            ib_Expence.Text = Convert.ToString(ib_Expence.Tag);
            SetButtonImage(ib_Expence, FoodandBeverage.Properties.Resources.withdraw, desiredWidth, desiredHeight);
        }


        private void ib_Discount_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmOfferDiscount"] != null)
                {
                    FrmOfferDiscount _instanceFrmOfferDiscountr = Application.OpenForms["FrmOfferDiscount"] as FrmOfferDiscount;
                    if (_instanceFrmOfferDiscountr.WindowState == FormWindowState.Minimized)
                        _instanceFrmOfferDiscountr.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                    {
                        _instanceFrmOfferDiscountr.Dock = DockStyle.None;
                        ResizeForm(_instanceFrmOfferDiscountr);
                        panelDesktop.Controls.Add(_instanceFrmOfferDiscountr);
                        _instanceFrmOfferDiscountr.Show();
                        _instanceFrmOfferDiscountr.BringToFront();
                    }
                }
                else
                {
                    FrmOfferDiscount _instanceFrmOfferDiscountr = new FrmOfferDiscount();
                    ResizeForm(_instanceFrmOfferDiscountr);
                    _instanceFrmOfferDiscountr.TopLevel = false;
                    _instanceFrmOfferDiscountr.Dock = DockStyle.None;
                    panelDesktop.Controls.Add(_instanceFrmOfferDiscountr);
                    _instanceFrmOfferDiscountr.Show();
                    _instanceFrmOfferDiscountr.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmOfferDiscountr.BringToFront();
                }
            }
            catch (Exception ex)
            {

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

            }

        }

        private void btn_sidebar_toggle_Click(object sender, EventArgs e)
        {
            if (pnlsidebar_searchproduct.Visible)
            {
                pnlsidebar_searchproduct.Visible = false;
                lblcategory.Visible = false;
                textb_shape();
            }
            else
            {
                pnlsidebar_searchproduct.Visible = true;
                lblcategory.Visible = true;
            }
        }

        private void txtname_search_Enter(object sender, EventArgs e)
        {
            if (txtname_search.Text == "Food Name .....")
            {
                txtname_search.Text = "";
                txtname_search.ForeColor = Color.Black;
            }

        }

        private void txtitemcode_serach_Enter(object sender, EventArgs e)
        {
            if (txtitemcode_serach.Text == "Item Code .....")
            {
                txtitemcode_serach.Text = "";
                txtitemcode_serach.ForeColor = Color.Black;
            }
        }

        private void txtname_search_Leave(object sender, EventArgs e)
        {
            if (txtname_search.Text == "")
            {
                txtname_search.Text = "Food Name .....";
                txtname_search.ForeColor = Color.Black;
            }
        }

        private void txtitemcode_serach_Leave(object sender, EventArgs e)
        {
            if (txtitemcode_serach.Text == "")
            {
                txtitemcode_serach.Text = "Item Code .....";
                txtitemcode_serach.ForeColor = Color.Black;
            }
        }

        private void txtname_search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "*";
                    dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId = p.ProductId";
                    dmnSearch.WhereNdGroupBy = " where p.Status = 1 and m.Status = 1 and Product LIKE '%" + txtname_search.Text + "%'";
                    dmnSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    pnl_serachproduct_dashboard.Controls.Clear();
                    tlp_searchproduct_dashboard.Controls.Clear();
                    tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                    tlp_searchproduct_dashboard.AutoScroll = true;
                    if (gridSource.Count > 0)
                    {
                        int rowIndex = 1;
                        int columnIndex = 0;
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            if (columnIndex == 2)
                            {
                                columnIndex = 0;
                                rowIndex++;
                            }
                            // Configure the Button
                            Button button = new Button();
                            button.Text = gridSource[i].Product;
                            button.Name = "btn" + gridSource[i].Product;
                            button.Tag = gridSource[i].ProductId;
                            button.Width = 140;
                            button.Height = 40;

                            // Set the FlatAppearance properties to remove the border
                            button.FlatAppearance.BorderSize = 0; // Set border width to 0
                            button.FlatAppearance.BorderColor = button.BackColor;
                            button.FlatStyle = FlatStyle.Flat;
                            button.BackColor = product_buttoncolor;
                            button.ForeColor = Color.White;

                            // Create a circular Region
                            //System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                            //path.AddEllipse(0, 0, button.Width, button.Height);
                            //button.Region = new Region(path);
                            button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));

                            button.Font = new Font(button.Font.FontFamily, 9, FontStyle.Bold);
                            //button.Dock = DockStyle.Fill;
                            button.Click += new EventHandler(DashboardButtonClickEventHandler);
                            tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                            columnIndex++;
                        }
                        pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                    }
                    else
                        CMessageBox.Show("No Products", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtitemcode_serach_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "*";
                    dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId = p.ProductId";
                    dmnSearch.WhereNdGroupBy = " where p.Status = 1 and m.Status = 1 and ItemCode = '" + txtitemcode_serach.Text + "'";
                    dmnSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    pnl_serachproduct_dashboard.Controls.Clear();
                    tlp_searchproduct_dashboard.Controls.Clear();
                    tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                    tlp_searchproduct_dashboard.AutoScroll = true;
                    if (gridSource.Count > 0)
                    {
                        int rowIndex = 1;
                        int columnIndex = 0;
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            if (columnIndex == 2)
                            {
                                columnIndex = 0;
                                rowIndex++;
                            }
                            // Configure the Button
                            Button button = new Button();
                            button.Text = gridSource[i].Product;
                            button.Name = "btn" + gridSource[i].Product;
                            button.Tag = gridSource[i].ProductId;
                            button.Width = 140;
                            button.Height = 40;

                            // Set the FlatAppearance properties to remove the border
                            button.FlatAppearance.BorderSize = 0; // Set border width to 0
                            button.FlatAppearance.BorderColor = button.BackColor;
                            button.FlatStyle = FlatStyle.Flat;
                            button.BackColor = product_buttoncolor;
                            button.ForeColor = Color.White;

                            // Create a circular Region
                            button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                            //System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                            //path.AddEllipse(0, 0, button.Width, button.Height);
                            //button.Region = new Region(path);

                            button.Font = new Font(button.Font.FontFamily, 9, FontStyle.Bold);
                            //button.Dock = DockStyle.Fill;
                            button.Click += new EventHandler(DashboardButtonClickEventHandler);
                            tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                            columnIndex++;
                        }
                        pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                    }
                    else
                        CMessageBox.Show("No Products", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtname_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }

        private void txtitemcode_serach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }

        private void ib_orderview_MouseLeave(object sender, EventArgs e)
        {
            ib_orderview.Text = Convert.ToString(ib_orderview.Tag);
            SetButtonImage(ib_orderview, FoodandBeverage.Properties.Resources.orderview, desiredWidth, desiredHeight);
        }

        private void ib_orderview_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                {
                    FrmOrderView _instanceFrmorderview = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                    if (_instanceFrmorderview.WindowState == FormWindowState.Minimized)
                        _instanceFrmorderview.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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

            }
        }

        private void ib_kitchenview_MouseLeave(object sender, EventArgs e)
        {
            ib_kitchenview.Text = Convert.ToString(ib_kitchenview.Tag);
            SetButtonImage(ib_kitchenview, FoodandBeverage.Properties.Resources.kitchen_order_view, desiredWidth, desiredHeight);
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

            }

        }

        private void ib_PromoCode_MouseLeave(object sender, EventArgs e)
        {
            ib_PromoCode.Text = Convert.ToString(ib_PromoCode.Tag);
            SetButtonImage(ib_PromoCode, FoodandBeverage.Properties.Resources.promo_code, desiredWidth, desiredHeight);
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
                    if (tlp_main_sidebar_pnldesktop.BackColor.Name.Contains("ff"))
                        txtmenubuttoncolorname.Text = "#" + pbsidebuttoncoloepicker.BackColor.Name.ToString().Remove(0, 2);

                    Menuhome.BackColor = menubuttoncolor;
                    Menuoperation.BackColor = menubuttoncolor;
                    Menubilling.BackColor = menubuttoncolor;
                    Menureport.BackColor = menubuttoncolor;
                    Menulogout.BackColor = menubuttoncolor;
                    Menusetting.BackColor = menubuttoncolor;
                    Menutable.BackColor = menubuttoncolor;
                    menuEmployee.BackColor = menubuttoncolor;
                }
            }
            catch (Exception ex)
            {

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
                    if (tlp_main_sidebar_pnldesktop.BackColor.Name.Contains("ff"))
                        txtoperbuttoncolorname.Text = "#" + pbopbuttoncoloepicker.BackColor.Name.ToString().Remove(0, 2);

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
                }
            }
            catch (Exception ex)
            {

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
                    Menuhome.BackColor = menubuttoncolor;
                    Menuoperation.BackColor = menubuttoncolor;
                    Menubilling.BackColor = menubuttoncolor;
                    Menureport.BackColor = menubuttoncolor;
                    Menulogout.BackColor = menubuttoncolor;
                    Menusetting.BackColor = menubuttoncolor;
                    menuEmployee.BackColor = menubuttoncolor;
                    Menutable.BackColor = menubuttoncolor;

                    tlp_main_sidebar_pnldesktop.BackColor = Color.FromArgb(34, 46, 80);
                    pbappcoloepicker.BackColor = Color.FromArgb(34, 46, 80);

                    if (tlp_main_sidebar_pnldesktop.BackColor.Name.Contains("ff"))
                        txtoperbuttoncolorname.Text = "#" + pbopbuttoncoloepicker.BackColor.Name.ToString().Remove(0, 2);
                    if (tlp_main_sidebar_pnldesktop.BackColor.Name.Contains("ff"))
                        txtmenubuttoncolorname.Text = "#" + pbsidebuttoncoloepicker.BackColor.Name.ToString().Remove(0, 2);
                    if (tlp_main_sidebar_pnldesktop.BackColor.Name.Contains("ff"))
                        txtappcolorname.Text = "#" + tlp_main_sidebar_pnldesktop.BackColor.Name.ToString().Remove(0, 2);

                    Button btnsetting = this.Controls.Find("Appcolor", true).FirstOrDefault() as Button;
                    if (btnsetting != null)
                        btnsetting.BackColor = opbuttoncolor;

                    Button btnwhatsappsetting = this.Controls.Find("btn_Message", true).FirstOrDefault() as Button;
                    if (btnwhatsappsetting != null)
                        btnwhatsappsetting.BackColor = opbuttoncolor;
                }

            }
            catch (Exception ex)
            {

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

            }



        }

        private void ib_Company_MouseLeave(object sender, EventArgs e)
        {
            ib_Company.Text = Convert.ToString(ib_Company.Tag);
            SetButtonImage(ib_Company, FoodandBeverage.Properties.Resources.comapny, desiredWidth, desiredHeight);
        }


        private void menuEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                if (menutable_click)
                {
                    CloseTableViewPage();
                }

                panelDesktop.Controls.Clear();
                tlp_empl_menu.Dock = DockStyle.Fill;
                panelDesktop.Controls.Add(tlp_empl_menu);
                tlp_empl_menu.Visible = true;

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

            }

        }

        private void ib_employee_MouseLeave(object sender, EventArgs e)
        {
            ib_employee.Text = Convert.ToString(ib_employee.Tag);
            SetButtonImage(ib_table, FoodandBeverage.Properties.Resources.Employee, desiredWidth, desiredHeight);
        }

        private void ib_department_MouseLeave(object sender, EventArgs e)
        {
            ib_department.Text = Convert.ToString(ib_department.Tag);
            SetButtonImage(ib_table, FoodandBeverage.Properties.Resources.add_des_dep_user, desiredWidth, desiredHeight);
        }

        private void ib_designation_MouseLeave(object sender, EventArgs e)
        {
            ib_designation.Text = Convert.ToString(ib_designation.Tag);
            SetButtonImage(ib_table, FoodandBeverage.Properties.Resources.add_des_dep_user, desiredWidth, desiredHeight);
        }

        private void ib_useraccount_MouseLeave(object sender, EventArgs e)
        {
            ib_useraccount.Text = Convert.ToString(ib_useraccount.Tag);
            SetButtonImage(ib_table, FoodandBeverage.Properties.Resources.add_des_dep_user, desiredWidth, desiredHeight);
        }

        private void ib_reservation_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms[""] != null)
                {
                    FrmReservation _instanceFrmres = Application.OpenForms[""] as FrmReservation;
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

            }

        }

        private void ib_reservation_MouseLeave(object sender, EventArgs e)
        {
            ib_reservation.Text = Convert.ToString(ib_reservation.Tag);
            SetButtonImage(ib_reservation, FoodandBeverage.Properties.Resources.reserved, desiredWidth, desiredHeight);
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

            }

        }

        private void ib_userpermission_MouseLeave(object sender, EventArgs e)
        {
            ib_userpermission.Text = Convert.ToString(ib_userpermission.Tag);
            SetButtonImage(ib_userpermission, FoodandBeverage.Properties.Resources.userpermisson, desiredWidth, desiredHeight);
        }

        private void pb_appicon_Click(object sender, EventArgs e)
        {
            CollapseMenu();
        }

        private void FrmDashboard_Resize(object sender, EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            int marginLeft, marginTop, marginRight, marginBottom;

            if (screenWidth >= 1366 && screenHeight >= 768)
            {
                marginLeft = 43;
                marginTop = 35;
                marginRight = 10;
                marginBottom = 2;
            }
            else
            {
                marginLeft = 30;
                marginTop = 35;
                marginRight = 10;
                marginBottom = 2;
            }

            //foreach (Control control in tlp_empl_menu.Controls)
            //    control.Margin = new Padding(marginLeft, marginTop, marginRight, marginBottom);

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
                    for (int i = gridesource.Count() - 1; i >= 0; i--)
                    {
                        if (gridesource[i].Form == Convert.ToInt32(formsenum.Order) && gridesource[i].Status == 1)
                        {
                            ib_orderview.Visible = true;

                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Kitchen_View) && gridesource[i].Status == 1)
                        {
                            ib_kitchenview.Visible = true;

                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Category) && gridesource[i].Status == 1)
                        {
                            ib_Category.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Item) && gridesource[i].Status == 1)
                        {
                            ib_Item.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Product) && gridesource[i].Status == 1)
                        {
                            ib_ingrediant.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Menu) && gridesource[i].Status == 1)
                        {
                            ib_menu.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Unit) && gridesource[i].Status == 1)
                        {
                            ib_unit.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Table) && gridesource[i].Status == 1)
                        {
                            ib_table.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Purchase) && gridesource[i].Status == 1)
                        {
                            ib_purchase.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Purchase_Return) && gridesource[i].Status == 1)
                        {
                            ib_purchasereturn.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Tax) && gridesource[i].Status == 1)
                        {
                            ib_tax.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Vendor) && gridesource[i].Status == 1)
                        {
                            ib_vendor.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Discount_And_Offer) && gridesource[i].Status == 1)
                        {
                            ib_Discount_offer.Visible = true;

                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Expence) && gridesource[i].Status == 1)
                        {
                            ib_Expence.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.PromoCode) && gridesource[i].Status == 1)
                        {
                            ib_PromoCode.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Company) && gridesource[i].Status == 1)
                        {
                            ib_Company.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Reservation) && gridesource[i].Status == 1)
                        {
                            ib_reservation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Employee) && gridesource[i].Status == 1)
                        {
                            ib_employee.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Department) && gridesource[i].Status == 1)
                        {
                            ib_department.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Designation) && gridesource[i].Status == 1)
                        {
                            ib_designation.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.User_Account) && gridesource[i].Status == 1)
                        {
                            ib_useraccount.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.User_Permission) && gridesource[i].Status == 1)
                        {
                            ib_userpermission.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.OrderView) && gridesource[i].Status == 1)
                        {
                            ib_orderview.Visible = true;
                        }
                        else if (gridesource[i].Form == Convert.ToInt32(formsenum.Addon) && gridesource[i].Status == 1)
                        {
                            ib_addon.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public bool ChkWritePermission(int formid)
        {
            //int UserType = 1;
            BllSearch<DmnForms> bllacctype = new BllSearch<DmnForms>();
            DmnSearch dmnforms = new DmnSearch();
            dmnforms.Select = "*";
            dmnforms.Table = " tblforms ";
            dmnforms.WhereNdGroupBy = " where UserType=" + UserType + " and Status=1   and Form=" + formid;
            dmnforms.OrderBy = "order by Form";
            var gridesource = bllacctype.GetSearchData("prdSearch", dmnforms);
            if (gridesource.Count > 0)
            {
                if (gridesource[0].Write == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
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
                if (chk_whatsapp.Checked)
                    dmnmsg.Whatsapp = true;
                else
                    dmnmsg.Whatsapp = false;

                if (chk_gmail.Checked)
                    dmnmsg.Gmail = true;
                else
                    dmnmsg.Gmail = false;
                dmnmsg.BranchId = FrmDashboard.BranchId;
                dmnmsg.CreatedBy = FrmDashboard.UserId;
                dmnmsg.Status = 1;

                var InsertStatus = bllmessage.AddEditDataSetting_msg("prdInsertMessage", dmnmsg);
                if (InsertStatus.SId > 0)
                {
                    if (Convert.ToInt32(txtsid.Text) > 0)
                        CMessageBox.Show("update sucessfull", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        CMessageBox.Show("sucessfull", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtsid.Text = Convert.ToString(InsertStatus.SId);
                    }
                    panl_msgsettings.Visible = false;
                }
                else
                    CMessageBox.Show("Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {

            }

        }

        private void btn_close_msg_Click(object sender, EventArgs e)
        {
            panl_msgsettings.Visible = false;
        }

        private void Menutable_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmDashboardReport"] != null)
                {
                    FrmDashboardReport _instanceFrmDashboardReport = Application.OpenForms["FrmDashboardReport"] as FrmDashboardReport;
                    _instanceFrmDashboardReport.Visible = false;
                }
                panelDesktop.SuspendLayout();

                if (tlp_op_menu.Visible)
                    tlp_op_menu.Visible = false;

                if (sender is Button button)
                {
                    ChangeButtonBorder(button);
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

                    FrmTableView _instanceFrmTableView = new FrmTableView(0);
                    ResizeForm(_instanceFrmTableView);
                    _instanceFrmTableView.TopLevel = false;
                    panelDesktop.Controls.Add(_instanceFrmTableView);
                    _instanceFrmTableView.Show();
                    _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmTableView.BringToFront();
                 
                }
                menutable_click = true;
                panelDesktop.ResumeLayout();              
            }
            catch (Exception ex)
            {

            }

        }

        private void ib_addon_MouseLeave(object sender, EventArgs e)
        {
            ib_addon.Text = Convert.ToString(ib_addon.Tag);
            SetButtonImage(ib_addon, FoodandBeverage.Properties.Resources.addon, desiredWidth, desiredHeight);
        }

        private void ib_addon_Click(object sender, EventArgs e)
        {
            try
            {
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
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_avial_Click(object sender, EventArgs e)
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

                FrmTableView _instanceFrmTableView = new FrmTableView(3);
                ResizeForm(_instanceFrmTableView);
                _instanceFrmTableView.TopLevel = false;
                panelDesktop.Controls.Add(_instanceFrmTableView);
                _instanceFrmTableView.Show();
                _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                _instanceFrmTableView.BringToFront();

            }
        }

        private void btn_rev_Click(object sender, EventArgs e)
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

                FrmTableView _instanceFrmTableView = new FrmTableView(2);
                ResizeForm(_instanceFrmTableView);
                _instanceFrmTableView.TopLevel = false;
                panelDesktop.Controls.Add(_instanceFrmTableView);
                _instanceFrmTableView.Show();
                _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                _instanceFrmTableView.BringToFront();

            }
        }

        private void btn_running_Click(object sender, EventArgs e)
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

                FrmTableView _instanceFrmTableView = new FrmTableView(1);
                ResizeForm(_instanceFrmTableView);
                _instanceFrmTableView.TopLevel = false;
                panelDesktop.Controls.Add(_instanceFrmTableView);
                _instanceFrmTableView.Show();
                _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                _instanceFrmTableView.BringToFront();

            }
        }

        private void btntrefresh_Click(object sender, EventArgs e)
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

                FrmTableView _instanceFrmTableView = new FrmTableView(0);
                ResizeForm(_instanceFrmTableView);
                _instanceFrmTableView.TopLevel = false;
                panelDesktop.Controls.Add(_instanceFrmTableView);
                _instanceFrmTableView.Show();
                _instanceFrmTableView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                _instanceFrmTableView.BringToFront();

            }

        }
        
    }

}
