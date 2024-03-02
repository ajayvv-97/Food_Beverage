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
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using FoodandBeverage.Forms.UserAccount;
using FoodandBeverage.Forms.UserControls;
using CrystalDecisions.CrystalReports.Engine;
using FastMember;
using CrystalDecisions.Shared;

namespace FoodandBeverage.Forms
{
    public partial class FrmDashboard : Form
    {

        public static int BranchId = 0;
        public static int UserId = 0;
        public static int UserType = 0;
        public static int Allaccess = 0;

        public FrmDashboard()
        {
            InitializeComponent();
            UserId = Convert.ToInt32(FrmLogin.UserId);
            BranchId = Convert.ToInt32(FrmLogin.BranchId);
            UserType = Convert.ToInt32(FrmLogin.UserAccountTypeId);
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

        public static int cslno = 0;
        public static int actionno = 1;
        public static int orderdatetimeno = 2;
        public static int entryno = 3;
        public static int tablecodeno = 4;
        public static int totalqtyno = 5;
        public static int netamountno = 6;
        public static int recamountno = 7;

        public static int balamountno = 8;
        public static int paytypename = 9;
        public static int statusno = 10;
        public static int ordermasteridno = 11;

        #endregion
        private bool isClicked = false;
        Color menubuttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        Color opbuttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        TableLayoutPanel tlp_button = new TableLayoutPanel();
        TableLayoutPanel tlp_table_dashboard_main = new TableLayoutPanel();
        TableLayoutPanel tlp_table_dashboard = new TableLayoutPanel();
        TableLayoutPanel tlp_searchproduct_sidebar = new TableLayoutPanel();
        TableLayoutPanel tlp_searchproduct_dashboard = new TableLayoutPanel();
        private Color originalBackColor; // Variable to store the original background color
        FlowLayoutPanel flpcolor = new FlowLayoutPanel();
        private Point MouseLocation;
        Color product_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        Color seleproduct_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        public bool collapsemenu_click = false;
        // Set the size based on your conditions
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        Timer AutoTimer;
        public int AddMinutes = 0;
        public List<Namelist> CheckedtableNames = new List<Namelist>();
        public void initTimer()
        {
            AutoTimer = new Timer();
            AutoTimer.Tick += AutoTimer_tick;
            AutoTimer.Interval = 2000; //timer interval in mili seconds;
            AutoTimer.Start();
        }
        public class Namelist
        {
            public int Id { get; set; }
            public int Ordermasterid { get; set; }
            public string Cnames { get; set; }
            public string Tablecode { get; set; }
        }
        public void AutoTimer_tick(object sender, EventArgs e)
        {
            TableStaus(0);
        }
        private void Menulogout_Click(object sender, EventArgs e)
        {
            this.Close();
            if (System.Windows.Forms.Application.OpenForms["FrmLogin"] != null)
            {
                FrmLogin _instanceFrmLogin = Application.OpenForms["FrmLogin"] as FrmLogin;
                _instanceFrmLogin.Show();
                _instanceFrmLogin.BringToFront();
                _instanceFrmLogin.Visible = true;
            }
        }

 

        private void FrmDashboard_Load(object sender, EventArgs e)
        {

            if (screenWidth > 1367 && screenHeight > 769)
            {
                desiredWidth = 52;
                desiredHeight = 52;

                HdesiredWidth = 60;
                HdesiredHeight = 60;
                foreach (Control control in flp_operations.Controls)
                {
                    if (control is Button button)
                    {
                        button.Size = new Size(300, 120);
                        button.Margin = new Padding(90, 35, 10, 2);
                        button.Font = new Font(button.Font.FontFamily, 16, button.Font.Style);
                    }
                }
            }
            else
            {
                desiredWidth = 32;
                desiredHeight = 32;

                HdesiredWidth = 40;
                HdesiredHeight = 40;
                foreach (Control control in flp_operations.Controls)
                {
                    if (control is Button button)
                    {
                        button.Size = new Size(212, 89);
                    }
                }
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
            timer1.Start();
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

            dgvorderview.BorderStyle = BorderStyle.None;
            dgvorderview.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvorderview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvorderview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvorderview.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvorderview.BackgroundColor = Color.White;

            dgvorderview.EnableHeadersVisualStyles = false;
            dgvorderview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvorderview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvorderview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            panelDesktop.Controls.Clear();

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
            tlp_tablestatus.Visible = false;
            flpEmployee.Visible = false;
            foreach (Button menuButton in flp_menu.Controls.OfType<Button>())
            {
                menuButton.FlatAppearance.BorderColor = Color.White;
                menuButton.FlatAppearance.BorderSize = 0;
                menuButton.BackColor = Color.White;
                menuButton.ForeColor = Color.Black;
                menuButton.BackColor = menubuttoncolor;
                menuButton.ForeColor = Color.White;
            }
            button.BackColor = Color.White;
            button.ForeColor = Color.Black;
            button.FlatAppearance.BorderColor = Color.Black;
            button.FlatAppearance.BorderSize = 1;
        }
 
        private void Menuhome_Click(object sender, EventArgs e)
        {
            try
            {

                if (screenWidth > 1367 && screenHeight > 769)
                {
                    foreach (Control control in flp_operations.Controls)
                    {
                        if (control is Button buttonm)
                        {
                            buttonm.Size = new Size(300, 120);
                        }
                    }
                }
                else
                {
                    foreach (Control control in flp_operations.Controls)
                    {
                        if (control is Button buttonm)
                        {
                            buttonm.Size = new Size(212, 89);
                        }
                    }
                }
                if (sender is Button button)
                {
                    ChangeButtonBorder(button);
                }
                panelDesktop.Controls.Clear();
                if (System.Windows.Forms.Application.OpenForms["FrmDashboardReport"] != null)
                {
                    FrmDashboardReport _instancePurchaseReturn = Application.OpenForms["FrmDashboardReport"] as FrmDashboardReport;
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
                    FrmDashboardReport _instancePurchaseReturn = new FrmDashboardReport();
                    _instancePurchaseReturn.Dock = DockStyle.None;
                    ResizeForm(_instancePurchaseReturn);
                    _instancePurchaseReturn.TopLevel = false;
                    panelDesktop.Controls.Add(_instancePurchaseReturn);
                    _instancePurchaseReturn.Show();
                    _instancePurchaseReturn.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instancePurchaseReturn.BringToFront();
                }
                AutoTimer.Stop();
            }
            catch(Exception ex)
            {

            }  
        }
        public class CategoryInfo
        {
            public int Fcid { get; set; }
            public string CategoryName { get; set; }
        }
        public void LoadTableStaus(int serachmode)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "c.Fcid as Field1,c.CategoryName as Field2,ti.Tid Field3,ti.TableCode Field4,ti.OrderStatus Field5,ti.OrderMasterId Field6,bm.BookmasterId Field7,bm.BookingDateTime EndDate, " +
                    "(select COUNT(m.BookmasterId) from tblBookDetails d left join tblbookMaster m on d.BookmasterId=m.BookmasterId where m.Status=1 and Rid=0 and d.Status=1 and SYSDATETIME() >=DATEADD(MINUTE,-" + Convert.ToDecimal(FrmLogin.BookingInterwell) + ",BookingDateTime) and d.MTID=ti.Tid) as Field8";
                dmnSearch.Table = " tblTable_info  ti " +
                    "LEFT JOIN tblCategory_FTI c on c.Fcid=ti.Fcid " +
                    "LEFT JOIN tblBookDetails bd ON bd.MTID=ti.Tid and Rid=0 and  bd.Status=1 " +
                    "LEFT JOIN tblbookMaster bm on bm.BookmasterId=bd.BookmasterId and bm.Status=1 and SYSDATETIME() >= DATEADD(MINUTE,-" + Convert.ToDecimal(FrmLogin.BookingInterwell) + ",BookingDateTime) ";//and SYSDATETIME() <= DATEADD(MINUTE," + Convert.ToDecimal(FrmLogin.BookingInterwell) + ",BookingDateTime)";
                dmnSearch.OrderBy = "";
                switch (serachmode)
                {
                    case 0:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1";
                        break;
                    case 1:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=1";
                        break;
                    case 2:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.RevStatus=1";
                        break;
                    case 3:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=0 and ti.RevStatus=0";
                        break;
                }
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                bool flag = false;
                
                for (int j = 0; j < gridSource.Count; j++)
                {
                    if (gridSource[j].EndDate != null && gridSource[j].Field7 != null && Convert.ToInt32(gridSource[j].Field7) > 1) // gridSource[j].Field7 bookinmasterid
                    {
                        DateTime modifiedEndDate = gridSource[j].EndDate.AddMinutes(-Convert.ToDouble(FrmLogin.BookingInterwell));
                        DateTime afterAddMinutes = gridSource[j].EndDate.AddMinutes(Convert.ToDouble(AddMinutes));
                        if (Convert.ToInt32(gridSource[j].Field7) > 0 &&DateTime.Now >afterAddMinutes)
                        {
                            CheckexpireTable(Convert.ToInt32(gridSource[j].Field3), Convert.ToInt32(gridSource[j].Field7));
                            flag = true;
                        }
                    }
                }
                if (flag)
                    gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                var distinctCategories = gridSource.Select(item => item.Field1).Distinct().ToList();
                List<CategoryInfo> categoryList = new List<CategoryInfo>();
                foreach (var fcid in distinctCategories)
                {
                    string categoryName = gridSource.FirstOrDefault(item => item.Field1 == fcid)?.Field2;

                    if (categoryName != null)
                    {
                        categoryList.Add(new CategoryInfo { Fcid = Convert.ToInt32(fcid), CategoryName = categoryName });
                    }
                }

                if (gridSource.Count > 0)
                {
                    panelDesktop.Controls.Clear();
                    tlp_table_dashboard.Controls.Clear();
                    tlp_table_dashboard.Dock = DockStyle.Fill;
                    tlp_table_dashboard.AutoScroll = true;

                    tlp_table_dashboard_main.RowCount = 2;
                    tlp_table_dashboard_main.Controls.Clear();
                    tlp_table_dashboard_main.Dock = DockStyle.Fill;
                    tlp_table_dashboard_main.AutoScroll = true;
                    tlp_tablestatus.Dock = DockStyle.Right;
                    tlp_table_dashboard_main.Controls.Add(tlp_tablestatus, 0, 0);
                    tlp_tablestatus.Visible = true;

                    int rowIndex_cate = 1;

                    foreach (var category in categoryList)
                    {
                        int countcateitem = 0;
                        TableLayoutPanel tlp_category = new TableLayoutPanel();

                        #region main heading tablelayoutpanel and label  Category
                        tlp_category.Name = "tlp_category_" + category.CategoryName;
                        tlp_category.Tag = category.CategoryName;
                        tlp_category.Dock = DockStyle.Top;
                        tlp_category.Controls.Clear();
                        tlp_category.AutoScroll = true;
                        //tlp_category.BorderStyle = BorderStyle.FixedSingle;
                        ///tlp_category.CellBorderStyle =TableLayoutPanelCellBorderStyle.InsetDouble;
                        tlp_category.MouseHover += tlp_category_MouseHover;
                        tlp_category.MouseLeave += tlp_category_MouseLeave;

                        //Frist Row Cateory Heading
                        // Configure the Label
                        Label lblcategory_head = new Label();
                        lblcategory_head.Name = "lbcate_head_" + category.CategoryName;
                        lblcategory_head.Tag = category.Fcid;
                        lblcategory_head.Text = category.CategoryName;
                        lblcategory_head.ForeColor = Color.Black;
                        lblcategory_head.BackColor = Color.Transparent;
                        lblcategory_head.Dock = DockStyle.Top;
                        lblcategory_head.Font = new Font(lblcategory_head.Font.FontFamily, 12, FontStyle.Bold);
                        #endregion

                        int rowIndex = 1;
                        int columnIndex = 0;

                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            if (Convert.ToInt32(gridSource[i].Field1) == category.Fcid)
                            {
                                if (columnIndex == 7)
                                {
                                    columnIndex = 0;
                                    rowIndex++;
                                }

                                TableLayoutPanel tlp_table_inner = new TableLayoutPanel();
                                PictureBox pictureBox = new PictureBox();
                                Label lbltablename = new Label();

                                #region  Panel inside tablelayoutpanel
                                Panel pnl_box = new Panel();
                                pnl_box.Name = "pnl_box" + gridSource[i].Field4;
                                pnl_box.Tag = gridSource[i].Field3;
                                pnl_box.BackColor = Color.Transparent;
                                pnl_box.Width = 120;
                                pnl_box.Height = 80;
                                pnl_box.BackColor = Color.Transparent;
                                #endregion

                                #region  TableLayoutPanel inner 
                                // Configure the TableLayoutPanel
                                tlp_table_inner.RowCount = 2;
                                tlp_table_inner.Name = "tlpbox" + gridSource[i].Field4;
                                tlp_table_inner.Tag = gridSource[i].Field3;
                                tlp_table_inner.BorderStyle = BorderStyle.FixedSingle;
                                tlp_table_inner.Dock = DockStyle.None;
                                tlp_table_inner.Margin = new Padding(30, 10, 20, 10);
                                //tlp_table_inner.Width = 100;
                                //tlp_table_inner.Height = 65;
                                tlp_table_inner.Dock = DockStyle.Fill;
                                #endregion

                                #region  picture for table inside tablelayoutpanel inner
                                // Configure the PictureBox
                                pictureBox.Name = "pbtable" + gridSource[i].Field4;
                                pictureBox.Tag = gridSource[i].Field3;
                                pictureBox.Image = FoodandBeverage.Properties.Resources.restaurant;
                                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                                tlp_table_inner.Dock = DockStyle.Fill;
                                #endregion

                                #region  tablename  for table inside tablelayoutpanel inner
                                // Configure the Label
                                lbltablename.Name = "lbtablename" + gridSource[i].Field4;
                                lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                                lbltablename.Tag = gridSource[i].Field3;
                                lbltablename.Text = gridSource[i].Field4;
                                lbltablename.ForeColor = Color.Black;
                                lbltablename.BackColor = Color.Transparent;
                                lbltablename.Dock = DockStyle.Top;
                                //lbltablename.Margin = new Padding(10, 10, 0, 0);
                                lbltablename.Font = new Font("Arial", 10, FontStyle.Bold);
                                if (Convert.ToInt32(gridSource[i].Field5) == 1)
                                    lbltablename.ForeColor = Color.Red;
                                //else if (gridSource[i].RevStatus == 1)
                                //    lbltablename.ForeColor = Color.Blue;
                                else
                                    lbltablename.ForeColor = Color.Green;
                                #endregion

                                // Add the PictureBox and Label to the TableLayoutPanel
                                tlp_table_inner.Controls.Add(pictureBox, 0, 0); // PictureBox in the first row
                                tlp_table_inner.Controls.Add(lbltablename, 0, 1); // Label in the second row


                                pictureBox.MouseHover += pictureBox_MouseHover;
                                pictureBox.MouseLeave += pictureBox_MouseLeave;
                                pictureBox.Click += new EventHandler(tableClickEventHandler);
                                lbltablename.Click += new EventHandler(tableClickEventHandler);
                                tlp_table_inner.Click += new EventHandler(tableClickEventHandler);

                                pnl_box.Controls.Add(tlp_table_inner);

                                tlp_table_inner.AccessibleDescription = "0";
                                pictureBox.AccessibleDescription = "0";
                                lbltablename.AccessibleDescription = "0";
                                pnl_box.AccessibleDescription = "0";

                                #region Normal Order 
                                if (Convert.ToInt32(gridSource[i].Field5) == 1)//not reservation order 
                                {
                                    BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                                    dmnSearch.Select = "CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1";
                                    dmnSearch.Table = "tblOrderDetails";
                                    dmnSearch.WhereNdGroupBy = "WHERE status!=0 and OrderMasterId = " + gridSource[i].Field6;
                                    dmnSearch.OrderBy = "";
                                    var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                                    PictureBox dynamic_pic = new PictureBox();
                                    dynamic_pic.Name = "pbgif" + gridSource[i].Field4;
                                    dynamic_pic.Tag = gridSource[i].Field3;

                                    if (Convert.ToInt32(gridSource_order[0].Field1) == 1)
                                        dynamic_pic.Image = FoodandBeverage.Properties.Resources.running_table_pic;
                                    else
                                        dynamic_pic.Image = FoodandBeverage.Properties.Resources.billcomplete_GIF;

                                    dynamic_pic.SizeMode = PictureBoxSizeMode.StretchImage;
                                    dynamic_pic.BackColor = Color.Transparent;
                                    dynamic_pic.Width = 50;
                                    dynamic_pic.Height = 50;
                                    pnl_box.Controls.Add(dynamic_pic);
                                    dynamic_pic.BringToFront();
                                    dynamic_pic.Margin = new Padding(20, 10, 0, 0);
                                    dynamic_pic.Click += new EventHandler(tableClickEventHandler);
                                    dynamic_pic.AccessibleDescription = "0";
                                }
                                #endregion

                                #region Rservation Bokking Order
                                else//reservation booking order
                                {
                                    if (gridSource[i].EndDate != null && gridSource[i].Field7 != null)
                                    {
                                        PictureBox dynamic_pic = new PictureBox();
                                        dynamic_pic.Name = "pbgif" + gridSource[i].Field4;
                                        dynamic_pic.Tag = gridSource[i].Field3;
                                        dynamic_pic.SizeMode = PictureBoxSizeMode.StretchImage;
                                        dynamic_pic.BackColor = Color.Transparent;
                                        dynamic_pic.Width = 60;
                                        dynamic_pic.Height = 35;
                                        pnl_box.Controls.Add(dynamic_pic);
                                        dynamic_pic.BringToFront();
                                        dynamic_pic.Margin = new Padding(20, 10, 0, 0);
                                        dynamic_pic.Click += new EventHandler(tableClickEventHandler);

                                        #region order sent to kitchen ( running or complete )
                                        if (Convert.ToInt32(gridSource[i].Field6) > 0)//order sent to kitchen ( running or complete )
                                        {
                                            BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                                            dmnSearch.Select = "CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1";
                                            dmnSearch.Table = "tblOrderDetails";
                                            dmnSearch.WhereNdGroupBy = "WHERE status!=0 and OrderMasterId = " + gridSource[i].Field6;
                                            dmnSearch.OrderBy = "";
                                            var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                                            if (Convert.ToInt32(gridSource_order[0].Field1) == 1)
                                                dynamic_pic.Image = FoodandBeverage.Properties.Resources.running_table_pic;
                                            else
                                                dynamic_pic.Image = FoodandBeverage.Properties.Resources.billcomplete_GIF;
                                            lbltablename.ForeColor = Color.Red;
                                            tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                            pictureBox.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                            lbltablename.AccessibleDescription = Convert.ToString(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                            pnl_box.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                            dynamic_pic.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                        }
                                        #endregion

                                        #region order not  sent to kitchen reservation or overtime 
                                        else //order not  sent to kitchen reservation or overtime 
                                        {
                                            DateTime modifiedEndDate = gridSource[i].EndDate.AddMinutes(-Convert.ToDouble(FrmLogin.BookingInterwell));
                                            if ((modifiedEndDate <= DateTime.Now) && (DateTime.Now <= gridSource[i].EndDate.AddMinutes(AddMinutes)))
                                            {
                                                dynamic_pic.Image = FoodandBeverage.Properties.Resources.reserved_icon;
                                                tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                                pictureBox.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                                lbltablename.AccessibleDescription = Convert.ToString(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                                pnl_box.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                                dynamic_pic.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                            }
                                            else
                                            {
                                                dynamic_pic.Image = FoodandBeverage.Properties.Resources.reserve_icon_overtime;
                                                tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;
                                                pictureBox.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;
                                                lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;
                                                pnl_box.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;
                                                dynamic_pic.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;
                                            }

                                        }
                                        #endregion

                                    }
                                }
                                #endregion

                                tlp_category.Controls.Add(pnl_box, columnIndex, rowIndex);
                                columnIndex++;
                            }

                        }
                        tlp_table_dashboard.Controls.Add(lblcategory_head, 0, rowIndex_cate);
                        rowIndex_cate++;
                        tlp_table_dashboard.Controls.Add(tlp_category, 0, rowIndex_cate);
                        rowIndex_cate++;
                    }

                    //tlp_table_dashboard.BorderStyle = BorderStyle.FixedSingle;
                    tlp_table_dashboard_main.Controls.Add(tlp_table_dashboard, 0, 1);
                    panelDesktop.Controls.Add(tlp_table_dashboard_main);
                }

                initTimer();
            }
            catch (Exception ex)
            {

            }
        }
        public void TableStaus(int serachmode)
        {           
            try
            {
                AutoTimer.Stop();
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "c.Fcid as Field1,c.CategoryName as Field2,ti.Tid Field3,ti.TableCode Field4,ti.OrderStatus Field5,ti.OrderMasterId Field6,bm.BookmasterId Field7,bm.BookingDateTime EndDate, " +
                    "(select COUNT(m.BookmasterId) from tblBookDetails d left join tblbookMaster m on d.BookmasterId=m.BookmasterId where m.Status=1 and Rid=0 and d.Status=1 and SYSDATETIME() >=DATEADD(MINUTE,-" + Convert.ToDecimal(FrmLogin.BookingInterwell) + ",BookingDateTime) and d.MTID=ti.Tid) as Field8";
                dmnSearch.Table = " tblTable_info  ti " +
                    "LEFT JOIN tblCategory_FTI c on c.Fcid=ti.Fcid " +
                    "LEFT JOIN tblBookDetails bd ON bd.MTID=ti.Tid and Rid=0 and  bd.Status=1 " +
                    "LEFT JOIN tblbookMaster bm on bm.BookmasterId=bd.BookmasterId and bm.Status=1 and SYSDATETIME() >= DATEADD(MINUTE,-" + Convert.ToDecimal(FrmLogin.BookingInterwell) + ",BookingDateTime) ";//and SYSDATETIME() <= DATEADD(MINUTE," + Convert.ToDecimal(FrmLogin.BookingInterwell) + ",BookingDateTime)";
                dmnSearch.OrderBy = "";
                switch (serachmode)
                {
                    case 0:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1";
                        break;
                    case 1:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=1";
                        break;
                    case 2:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.RevStatus=1";
                        break;
                    case 3:
                        dmnSearch.WhereNdGroupBy = "where ti.Status=1 and ti.OrderStatus=0 and ti.RevStatus=0";
                        break;
                }
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                bool flag = false;
                for (int j = 0; j < gridSource.Count; j++)
                {
                    if (gridSource[j].EndDate != null && gridSource[j].Field7 != null && Convert.ToInt32(gridSource[j].Field7) > 1) //gridSource[j].Field7 bookinmasterid gridSource[j].Field7 ordermasterid 
                    {
                        DateTime beforeAddMinutes = gridSource[j].EndDate.AddMinutes(-Convert.ToDouble(FrmLogin.BookingInterwell));
                        DateTime afterAddMinutes = gridSource[j].EndDate.AddMinutes(Convert.ToDouble(FrmLogin.BookingInterwell));
                        if (Convert.ToInt32(gridSource[j].Field7) > 0 && DateTime.Now > afterAddMinutes)
                        {
                            CheckexpireTable(Convert.ToInt32(gridSource[j].Field3), Convert.ToInt32(gridSource[j].Field7));
                            flag = true;
                        }
                    }
                }

                //if (flag)
                //    gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                var distinctCategories = gridSource.Select(item => item.Field1).Distinct().ToList();
                List<CategoryInfo> categoryList = new List<CategoryInfo>();
                foreach (var fcid in distinctCategories)
                {
                    string categoryName = gridSource.FirstOrDefault(item => item.Field1 == fcid)?.Field2;

                    if (categoryName != null)
                    {
                        categoryList.Add(new CategoryInfo { Fcid = Convert.ToInt32(fcid), CategoryName = categoryName });
                    }
                }

                if (gridSource.Count > 0)
                {
                    foreach (var category in categoryList)
                    {
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            string pnl_boxname = "pnl_box" + gridSource[i].Field4;
                            string tlpboxname = "tlpbox" + gridSource[i].Field4;
                            string pbtablename = "pbtable" + gridSource[i].Field4;
                            string lbtable = "lbtablename" + gridSource[i].Field4;
                            string dynamicPicName = "pbgif" + gridSource[i].Field4;
                            Panel pnl_box = this.Controls.Find(pnl_boxname, true).FirstOrDefault() as Panel;
                            TableLayoutPanel tlp_table_inner = this.Controls.Find(tlpboxname, true).FirstOrDefault() as TableLayoutPanel;
                            PictureBox pictureBox = this.Controls.Find(pbtablename, true).FirstOrDefault() as PictureBox;
                            Label lbltablename = this.Controls.Find(lbtable, true).FirstOrDefault() as Label;
                            PictureBox existingPictureBox = pnl_box.Controls.Find(dynamicPicName, true).FirstOrDefault() as PictureBox;
                            //if (existingPictureBox != null)
                            //    existingPictureBox.Visible = false;

                            if (Convert.ToInt32(gridSource[i].Field1) == category.Fcid)
                            {
                                #region  tablename  for table inside tablelayoutpanel inner

                                if (Convert.ToInt32(gridSource[i].Field5) == 1)
                                    lbltablename.ForeColor = Color.Red;
                                //else if (gridSource[i].RevStatus == 1)
                                //    lbltablename.ForeColor = Color.Blue;
                                else
                                    lbltablename.ForeColor = Color.Green;
                                #endregion

                                tlp_table_inner.AccessibleDescription = "0";
                                pictureBox.AccessibleDescription = "0";
                                lbltablename.AccessibleDescription = "0";
                                pnl_box.AccessibleDescription = "0";

                                #region Normal Order -- not reservation order 
                                if (Convert.ToInt32(gridSource[i].Field5) == 1)//not reservation order 
                                {
                                    BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                                    dmnSearch.Select = "CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1";
                                    dmnSearch.Table = "tblOrderDetails";
                                    dmnSearch.WhereNdGroupBy = "WHERE status!=0 and OrderMasterId = " + gridSource[i].Field6;
                                    dmnSearch.OrderBy = "";
                                    var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);
                                    if (existingPictureBox == null)
                                    {
                                        PictureBox dynamic_pic = new PictureBox();
                                        dynamic_pic.Name = "pbgif" + gridSource[i].Field4;
                                        dynamic_pic.Tag = gridSource[i].Field3;
                                        dynamic_pic.SizeMode = PictureBoxSizeMode.StretchImage;
                                        dynamic_pic.BackColor = Color.Transparent;
                                        dynamic_pic.Width = 50;
                                        dynamic_pic.Height = 50;
                                        pnl_box.Controls.Add(dynamic_pic);
                                        dynamic_pic.BringToFront();
                                        dynamic_pic.Margin = new Padding(20, 10, 0, 0);
                                        dynamic_pic.Click += new EventHandler(tableClickEventHandler);

                                        if (Convert.ToInt32(gridSource_order[0].Field1) == 1)
                                            dynamic_pic.Image = FoodandBeverage.Properties.Resources.running_table_pic;
                                        else
                                            dynamic_pic.Image = FoodandBeverage.Properties.Resources.billcomplete_GIF;
                                        dynamic_pic.AccessibleDescription = "0";

                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(gridSource_order[0].Field1) == 1)
                                            existingPictureBox.Image = FoodandBeverage.Properties.Resources.running_table_pic;
                                        else
                                            existingPictureBox.Image = FoodandBeverage.Properties.Resources.billcomplete_GIF;

                                        existingPictureBox.AccessibleDescription = "0";
                                        existingPictureBox.Visible = true;
                                        existingPictureBox.Width = 50;
                                        existingPictureBox.Height = 50;
                                    }


                                }
                                #endregion

                                #region Rservation Bokking Order
                                else if (Convert.ToInt32(gridSource[i].Field5) == 0)//reservation booking order
                                {
                                    if (gridSource[i].EndDate != null && gridSource[i].Field7 != null)
                                    {
                                        #region order sent to kitchen ( running or complete )
                                        if (Convert.ToInt32(gridSource[i].Field6) > 0)//order sent to kitchen ( running or complete )
                                        {
                                            BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                                            dmnSearch.Select = "CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1";
                                            dmnSearch.Table = "tblOrderDetails";
                                            dmnSearch.WhereNdGroupBy = "WHERE status!=0 and OrderMasterId = " + gridSource[i].Field6;
                                            dmnSearch.OrderBy = "";
                                            var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                                            if (existingPictureBox == null)
                                            {
                                                PictureBox dynamic_pic = new PictureBox();
                                                dynamic_pic.Name = "pbgif" + gridSource[i].Field4;
                                                dynamic_pic.Tag = gridSource[i].Field3;
                                                dynamic_pic.SizeMode = PictureBoxSizeMode.StretchImage;
                                                dynamic_pic.BackColor = Color.Transparent;
                                                dynamic_pic.Width = 50;
                                                dynamic_pic.Height = 50;
                                                pnl_box.Controls.Add(dynamic_pic);
                                                dynamic_pic.BringToFront();
                                                dynamic_pic.Margin = new Padding(20, 10, 0, 0);
                                                dynamic_pic.Click += new EventHandler(tableClickEventHandler);

                                                if (Convert.ToInt32(gridSource_order[0].Field1) == 1)
                                                    dynamic_pic.Image = FoodandBeverage.Properties.Resources.running_table_pic;
                                                else
                                                    dynamic_pic.Image = FoodandBeverage.Properties.Resources.billcomplete_GIF;

                                                dynamic_pic.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                            }
                                            else
                                            {
                                                if (Convert.ToInt32(gridSource_order[0].Field1) == 1)
                                                        existingPictureBox.Image = FoodandBeverage.Properties.Resources.running_table_pic;
                                                    else
                                                        existingPictureBox.Image = FoodandBeverage.Properties.Resources.billcomplete_GIF;
                                                existingPictureBox.AccessibleDescription = "0";
                                                existingPictureBox.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                                existingPictureBox.Visible = true;
                                            }      
                                            
                                            lbltablename.ForeColor = Color.Red;
                                            tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                            pictureBox.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                            lbltablename.AccessibleDescription = Convert.ToString(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                            pnl_box.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;

                                        }
                                        #endregion

                                        #region order not  sent to kitchen reservation or overtime 
                                        else //order not  sent to kitchen reservation or overtime 
                                        {
                                            DateTime modifiedEndDate = gridSource[i].EndDate.AddMinutes(-Convert.ToDouble(FrmLogin.BookingInterwell));
                                            if ((modifiedEndDate <= DateTime.Now) && (DateTime.Now <= gridSource[i].EndDate.AddMinutes(AddMinutes)))
                                            {
                                                if (existingPictureBox == null)
                                                {
                                                    PictureBox dynamic_pic = new PictureBox();
                                                    dynamic_pic.Name = "pbgif" + gridSource[i].Field4;
                                                    dynamic_pic.Tag = gridSource[i].Field3;
                                                    dynamic_pic.SizeMode = PictureBoxSizeMode.StretchImage;
                                                    dynamic_pic.BackColor = Color.Transparent;
                                                    dynamic_pic.Width = 60;
                                                    dynamic_pic.Height = 35;
                                                    pnl_box.Controls.Add(dynamic_pic);
                                                    dynamic_pic.BringToFront();
                                                    dynamic_pic.Margin = new Padding(20, 10, 0, 0);
                                                    dynamic_pic.Click += new EventHandler(tableClickEventHandler);
                                                    dynamic_pic.Image = FoodandBeverage.Properties.Resources.reserved_icon;
                                                    dynamic_pic.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                                }
                                                else
                                                {    
                                                    existingPictureBox.Image = FoodandBeverage.Properties.Resources.reserved_icon;
                                                    existingPictureBox.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                                    existingPictureBox.Width = 60;
                                                    existingPictureBox.Height = 35;
                                                } 
                                                tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                                pictureBox.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                                lbltablename.AccessibleDescription = Convert.ToString(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                                pnl_box.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_Table) + "," + gridSource[i].Field7;
                                                existingPictureBox.Visible = true;

                                            }
                                            else
                                            {
                                                if (existingPictureBox == null)
                                                {
                                                    PictureBox dynamic_pic = new PictureBox();
                                                    dynamic_pic.Name = "pbgif" + gridSource[i].Field4;
                                                    dynamic_pic.Tag = gridSource[i].Field3;
                                                    dynamic_pic.SizeMode = PictureBoxSizeMode.StretchImage;
                                                    dynamic_pic.BackColor = Color.Transparent;
                                                    dynamic_pic.Width = 60;
                                                    dynamic_pic.Height = 35;
                                                    pnl_box.Controls.Add(dynamic_pic);
                                                    dynamic_pic.BringToFront();
                                                    dynamic_pic.Margin = new Padding(20, 10, 0, 0);
                                                    dynamic_pic.Click += new EventHandler(tableClickEventHandler);
                                                    dynamic_pic.Image = FoodandBeverage.Properties.Resources.reserved_icon;
                                                    dynamic_pic.Image = FoodandBeverage.Properties.Resources.reserve_icon_overtime;
                                                    dynamic_pic.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;
                                                }
                                                else
                                                {
                                                    existingPictureBox.Image = FoodandBeverage.Properties.Resources.reserve_icon_overtime;
                                                    existingPictureBox.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;
                                                    existingPictureBox.Visible = true;
                                                    existingPictureBox.Width = 60;
                                                    existingPictureBox.Height = 35;
                                                }
                      
                                                tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;
                                                pictureBox.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;
                                                lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;
                                                pnl_box.AccessibleDescription = Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel) + "," + gridSource[i].Field7;
                         

                                            }

                                        }
                                        #endregion

                                    }
                                    else
                                    {
                                        if (existingPictureBox != null)
                                            existingPictureBox.Visible = false;
                                    }


                                }
                                #endregion
                                else
                                {
                                    if (existingPictureBox != null)
                                        existingPictureBox.Visible = false;
                                }
                            }
                        }
                    }
                }

                initTimer();
            }
            catch (Exception ex)
            {

            }

        }

        public void CheckexpireTable(int tid, int Bmid)
        {
            try
            {
                BllReservation bllReservation = new BllReservation();
                DmnReservation dmnReservation = new DmnReservation();
                dmnReservation.BookmasterId = Bmid;
                dmnReservation.Tid = tid;
                dmnReservation.CreatedBy = FrmDashboard.UserId;
                dmnReservation.Status = 0;
                dmnReservation.BranchId = 1030;
                var InsertStatus = bllReservation.Reservation_Delete("prd_CheckexpireTableReservation", dmnReservation);
                if (InsertStatus.BookmasterId > 0)
                {

                }

            }
            catch (Exception ex)
            {

            }
        }
        private void chkBox_sele_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox checkBox = (CheckBox)sender;
                string textvalue = checkBox.AccessibleDescription;
                string[] parts = textvalue.Split(',');
                int omasterid = 0, tid = 0;
                if (parts.Length >= 2)
                {
                    omasterid = Convert.ToInt32(parts[0]);
                    tid = Convert.ToInt32(parts[1]);
                }
                if (checkBox.Checked)
                {
                    CheckedtableNames.Add(new Namelist
                    {
                        Id = tid,
                        Ordermasterid = omasterid,
                        Tablecode = Convert.ToString(checkBox.Tag)

                    });
                }
                else
                {
                    Namelist itemToRemove = CheckedtableNames.FirstOrDefault(item => item.Ordermasterid == omasterid);
                    if (itemToRemove != null)
                    {
                        CheckedtableNames.Remove(itemToRemove);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CompleteEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;

                if (clickedControl != null)
                {
                    string tableCode = clickedControl.Name;
                    int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                    int screenHeight = Screen.PrimaryScreen.Bounds.Height;
                    if (CheckedtableNames.Count > 0)
                    {
                        if (System.Windows.Forms.Application.OpenForms["FrmOrder"] != null)
                        {
                            FrmOrder _instanceFrmOrder = Application.OpenForms["FrmOrder"] as FrmOrder;
                            if (_instanceFrmOrder.WindowState == FormWindowState.Minimized)
                            {
                                _instanceFrmOrder.WindowState = FormWindowState.Maximized;
                            }
                            else
                            {
                                _instanceFrmOrder.Show();
                                _instanceFrmOrder.BringToFront();
                            }
                        }
                        else
                        {

                            FrmOrder _instanceFrmOrder = new FrmOrder(0, Convert.ToInt32(DashboardTableType.CombinedOrderBill), 0, CheckedtableNames);
                            _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                            _instanceFrmOrder.Opacity = 0;
                            _instanceFrmOrder.Visible = false;
                            _instanceFrmOrder.Show();
                            _instanceFrmOrder.BringToFront();
                        }
                        
                    }
                    else
                    {
                        string textvalue = clickedControl.AccessibleDescription;
                        string[] parts = textvalue.Split(',');
                        string bmasterid = "";
                        int Btype = 0;
                        if (parts.Length >= 2)
                        {
                            Btype = Convert.ToInt32(parts[0]);
                            bmasterid = parts[1];
                        }
                        else
                            Btype = Convert.ToInt32(parts[0]);
                        if (Btype == Convert.ToInt32(DashboardTableType.Normalorder))
                        {
                            if (System.Windows.Forms.Application.OpenForms["FrmOrder"] != null)
                            {
                                FrmOrder _instanceFrmOrder = Application.OpenForms["FrmOrder"] as FrmOrder;
                                if (_instanceFrmOrder.WindowState == FormWindowState.Minimized)
                                {
                                    _instanceFrmOrder.WindowState = FormWindowState.Maximized;
                                }
                                else
                                {
                                    _instanceFrmOrder.Show();
                                    _instanceFrmOrder.BringToFront();
                                }
                            }
                            else
                            {
                                int tagValue;
                                if (clickedControl.Tag != null)
                                {
                                    FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Normalorder), 0, CheckedtableNames);
                                    _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                    _instanceFrmOrder.Opacity = 0;
                                    _instanceFrmOrder.Visible = false;
                                    _instanceFrmOrder.Show();
                                    _instanceFrmOrder.BringToFront();
                                }
                            }
                        }
                        else
                        {
                            if (System.Windows.Forms.Application.OpenForms["FrmOrder"] != null)
                            {
                                FrmOrder _instanceFrmOrder = Application.OpenForms["FrmOrder"] as FrmOrder;
                                if (_instanceFrmOrder.WindowState == FormWindowState.Minimized)
                                {
                                    _instanceFrmOrder.WindowState = FormWindowState.Maximized;
                                }
                                else
                                {
                                    _instanceFrmOrder.Show();
                                    _instanceFrmOrder.BringToFront();
                                }
                            }
                            else
                            {
                                int tagValue;
                                if (clickedControl.Tag != null)
                                {
                                    FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid), CheckedtableNames);
                                    _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                    _instanceFrmOrder.Opacity = 0;
                                    _instanceFrmOrder.Visible = false;
                                    _instanceFrmOrder.Show();
                                    _instanceFrmOrder.BringToFront();
                                }
                            }
                        }

                    }


                }
            }
            catch (Exception ex)
            {
            }
        }

        private void tableClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                AutoTimer.Stop();
                CheckedtableNames.Clear();
                Control clickedControl = sender as Control;
                string textvalue = clickedControl.AccessibleDescription;
                string[] parts = textvalue.Split(',');
                string bmasterid = "";
                int Btype = 0;
                if (parts.Length >= 2)
                {
                    Btype = Convert.ToInt32(parts[0]);
                    bmasterid = parts[1];
                }
                else
                    Btype = Convert.ToInt32(parts[0]);

                if (clickedControl != null)
                {
                    string tableCode = clickedControl.Name;
                    int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                    int screenHeight = Screen.PrimaryScreen.Bounds.Height;
                    if (Btype == Convert.ToInt32(DashboardTableType.Normalorder))
                    {
                        if (System.Windows.Forms.Application.OpenForms["FrmOrder"] != null)
                        {
                            FrmOrder _instanceFrmOrder = Application.OpenForms["FrmOrder"] as FrmOrder;
                            if (_instanceFrmOrder.WindowState == FormWindowState.Minimized)
                            {
                                _instanceFrmOrder.WindowState = FormWindowState.Maximized;
                            }
                            else
                            {
                                _instanceFrmOrder.Show();
                                _instanceFrmOrder.BringToFront();
                            }
                        }
                        else
                        {
                            int tagValue;
                            if (clickedControl.Tag != null)
                            {
                                FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Normalorder), 0, CheckedtableNames);
                                _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                _instanceFrmOrder.Opacity = 0;
                                _instanceFrmOrder.Visible = false;
                                _instanceFrmOrder.Show();
                                _instanceFrmOrder.BringToFront();
                            }
                        }

                    }
                    else if (Btype == Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel))
                    {
                        var result = CMessageBox.Show("Do You want Cancel  This Reservation", "Confirm The Action", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            //BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                            //DmnSearch dmnSearch = new DmnSearch();
                            //dmnSearch.Select = " bm.BookmasterId Field1";
                            //dmnSearch.Table = "tblBookDetails bd " +
                            //    "LEFT JOIN tblbookMaster bm on bm.BookmasterId=bd.BookmasterId ";
                            //dmnSearch.WhereNdGroupBy = " where bm.Status=1 and Rid=0 and bd.MTID= " + clickedControl.Tag;
                            //dmnSearch.OrderBy = "";
                            //var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                            //if(gridSource.Count>0)
                            //{
                            BllReservation bllReservation = new BllReservation();
                            DmnReservation dmnReservation = new DmnReservation();
                            dmnReservation.BookmasterId = Convert.ToInt32(bmasterid);
                            dmnReservation.CreatedBy = FrmDashboard.UserId;
                            dmnReservation.Status = 0;
                            dmnReservation.BranchId = 1030;
                            var InsertStatus = bllReservation.Reservation_Delete("prd_deleteReservation", dmnReservation);
                            if (InsertStatus.BookmasterId > 0)
                            {
                                CMessageBox.Show("Reservation Cancel Sucessfull", "Information", MessageBoxButtons.OK);
                                TableStaus(0);

                            }
                            //}
                        }
                        else if (result == DialogResult.No)
                        {
                            FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid), CheckedtableNames);
                            _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                            _instanceFrmOrder.Opacity = 0;
                            _instanceFrmOrder.Visible = false;
                            _instanceFrmOrder.Show();
                            _instanceFrmOrder.BringToFront();

                        }
                    }
                    else if (Btype == Convert.ToInt32(DashboardTableType.Reverse_Table))
                    {
                        FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid), CheckedtableNames);
                        _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        _instanceFrmOrder.Opacity = 0;
                        _instanceFrmOrder.Visible = false;
                        _instanceFrmOrder.Show();
                        _instanceFrmOrder.BringToFront();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SidebuttonfavClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId=p.ProductId ";
                dmnSearch.WhereNdGroupBy = " where p.Status=1 and p.Fcid= " + clickedControl.Tag + "and m.Status=1";
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

                        //// Create a circular Region
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
                //else
                //{
                //    CMessageBox.Show("No Products On This Category", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
            }
            catch (Exception ex)
            {
            }
        }
        private void SidebuttonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId=p.ProductId ";
                dmnSearch.WhereNdGroupBy = " where p.Status=1 and p.Fcid= " + clickedControl.Tag + "and m.Status=1";
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

                        //// Create a circular Region
                        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                        button.Font = new Font(button.Font.FontFamily, 9, FontStyle.Bold);
                        //button.Dock = DockStyle.Fill;
                        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                        tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                        columnIndex++;
                    }
                    pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                }
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
        private void tlp_category_com_MouseHover(object sender, EventArgs e)
        {
            TableLayoutPanel tlpcate = sender as TableLayoutPanel;
            string tlpName = "tlp_category_com_" + tlpcate.Tag;
            // Find the TableLayoutPanel control by name
            TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
            if (tlp != null)
            {
                tlp.BackColor = Color.LightBlue;
                // Show tooltip with the item name
                toolTip1.SetToolTip(tlp, tlpcate.Tag.ToString());
            }
        }
        private void tlp_category_com_MouseLeave(object sender, EventArgs e)
        {
            TableLayoutPanel tlpcate = sender as TableLayoutPanel;
            string tlpName = "tlp_category_com_" + tlpcate.Tag;
            // Find the TableLayoutPanel control by name
            TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
            if (tlp != null)
            {
                tlp.BackColor = Color.White;
            }
        }
        private void tlp_category_MouseHover(object sender, EventArgs e)
        {
            TableLayoutPanel tlpcate = sender as TableLayoutPanel;
            string tlpName = "tlp_category_com_" + tlpcate.Tag;
            // Find the TableLayoutPanel control by name
            TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
            if (tlp != null)
            {
                tlp.BackColor = Color.LightBlue;
                // Show tooltip with the item name
                toolTip1.SetToolTip(tlp, tlpcate.Tag.ToString());
            }
        }
        private void tlp_category_MouseLeave(object sender, EventArgs e)
        {
            TableLayoutPanel tlpcate = sender as TableLayoutPanel;
            string tlpName = "tlp_category_" + tlpcate.Tag;
            // Find the TableLayoutPanel control by name
            TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
            if (tlp != null)
            {
                tlp.BackColor = Color.White;
            }
        }
        private void pictureBox_com_MouseHover(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            string tlpName = "tlp_com" + pic.Tag;
            // Find the TableLayoutPanel control by name
            TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
            if (tlp != null)
            {
                tlp.BackColor = Color.LightBlue;
                tlp.BorderStyle = BorderStyle.FixedSingle;
            }
        }
        private void pictureBox_com_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            string tlpName = "tlp_com" + pic.Tag;
            // Find the TableLayoutPanel control by name
            TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
            if (tlp != null)
            {
                tlp.BackColor = Color.White;
                tlp.BorderStyle = BorderStyle.None;
            }
        }
        private void pictureBox_MouseHover(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            string tlpName = "tlp" + pic.Tag;
            // Find the TableLayoutPanel control by name
            TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
            if (tlp != null)
            {
                tlp.BackColor = Color.LightBlue;
                tlp.BorderStyle = BorderStyle.FixedSingle;
            }
        }
        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            string tlpName = "tlp" + pic.Tag;
            // Find the TableLayoutPanel control by name
            TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
            if (tlp != null)
            {
                tlp.BackColor = Color.White;
                tlp.BorderStyle = BorderStyle.None;
            }
        }



        private void RoundPictureBoxCorners(PictureBox pictureBox, int cornerRadius)
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

        private void CollapseMenu()
        {
            AutoTimer.Stop();
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
            TableStaus(0);
        }

        private void pnl_header_Click(object sender, EventArgs e)
        {
            collapsemenu_click = !collapsemenu_click;
            //CollapseMenu();
        }

        private void Menuoperation_Click(object sender, EventArgs e)
        {
            AutoTimer.Stop();
            panelDesktop.Controls.Clear();

            ib_Company.Visible = true;

            if (sender is Button button)
            {
                ChangeButtonBorder(button);
            }
            panelDesktop.Controls.Add(flp_operations);
            flp_operations.Dock = DockStyle.Fill;
            flp_operations.Visible = true;
            if (screenWidth > 1367 && screenHeight > 769)
            {
                desiredWidth = 52;
                desiredHeight = 52;
                HdesiredWidth = 60;
                HdesiredHeight = 60;
                foreach (Control control in flp_operations.Controls)
                {
                    if (control is Button buttonm)
                    {
                        buttonm.Size = new Size(300, 120);
                        buttonm.Margin = new Padding(105, 35, 10, 2);
                        buttonm.Font = new Font(buttonm.Font.FontFamily, 16, buttonm.Font.Style);
                    }
                }

            }
            else
            {
                desiredWidth = 32;
                desiredHeight = 32;
                HdesiredWidth = 40;
                HdesiredHeight = 40;
                foreach (Control control in flp_operations.Controls)
                {
                    if (control is Button buttonm)
                    {
                        buttonm.Size = new Size(212, 89);
                    }
                }
            }
        }


        private void Menubilling_Click(object sender, EventArgs e)
        {
            AutoTimer.Stop();
            if (sender is Button button)
            {
                ChangeButtonBorder(button);
            }
            CheckedtableNames.Clear();
            Billcomplete_Fill();
        }
        public void Billcomplete_Fill()
        {
            try
            {
                panelDesktop.Controls.Clear();
                panelDesktop.Controls.Add(pnl_complebill_prevbill);
                pnl_complebill_prevbill.Dock = DockStyle.Fill;
                Control controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
                tlp_compl_pre_bill.Controls.Remove(controlToRemove);
                tlp_compl_pre_bill.Controls.Add(pnl_compelte_bill, 0, 1);
                tlp_compl_pre_bill.Dock = DockStyle.Fill;
                pnl_empty_msg.Visible = false;
                pnl_complebill_prevbill.Visible = true;
                BllSearch<DmnTableinfo> bllSearch = new BllSearch<DmnTableinfo>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "ti.* ,c.CategoryName as CategoryName,om.BookmasterId BookmasterId";
                dmnSearch.Table = "tblTable_info  ti LEFT JOIN tblCategory_FTI c on c.Fcid=ti.Fcid LEFT JOIN tblOrderMaster om on om.OrderMasterId=ti.OrderMasterId";
                dmnSearch.WhereNdGroupBy = "where ti.Status=1 and (ti.OrderStatus=1 OR ti.OrderMasterId > 0 )";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                var distinctCategories = gridSource.Select(item => item.Fcid).Distinct().ToList();
                List<CategoryInfo> categoryList = new List<CategoryInfo>();
                foreach (var fcid in distinctCategories)
                {
                    string categoryName = gridSource.FirstOrDefault(item => item.Fcid == fcid)?.CategoryName;

                    if (categoryName != null)
                    {
                        categoryList.Add(new CategoryInfo { Fcid = fcid, CategoryName = categoryName });
                    }
                }

                if (gridSource.Count > 0)
                {
                    pnl_compelte_bill.Controls.Clear();
                    tlp_table_dashboard.Controls.Clear();
                    tlp_table_dashboard.Dock = DockStyle.Fill;
                    tlp_table_dashboard.AutoScroll = true;

                    tlp_table_dashboard_main.RowCount = 2;
                    tlp_table_dashboard_main.Controls.Clear();
                    tlp_table_dashboard_main.Dock = DockStyle.Fill;
                    tlp_table_dashboard_main.AutoScroll = true;
                    tlp_tablestatus.Dock = DockStyle.Right;
                    tlp_table_dashboard_main.Controls.Add(tlp_tablestatus, 0, 0);

                    #region order complete only dispaly code
                    int rowIndex_cate = 1;
                    foreach (var category in categoryList)
                    {
                        bool categoryHasTables = false;
                        TableLayoutPanel tlp_category = new TableLayoutPanel();
                        tlp_category.Name = "tlp_category_Com_" + category.CategoryName;
                        tlp_category.Tag = category.CategoryName;
                        tlp_category.Dock = DockStyle.Top;
                        tlp_category.Controls.Clear();
                        tlp_category.AutoScroll = true;
                        //tlp_category.BorderStyle = BorderStyle.FixedSingle;
                        ///tlp_category.CellBorderStyle =TableLayoutPanelCellBorderStyle.InsetDouble;
                        tlp_category.MouseHover += tlp_category_com_MouseHover;
                        tlp_category.MouseLeave += tlp_category_com_MouseLeave;

                        //Frist Row Cateory Heading
                        // Configure the Label
                        Label lblcategory_head = new Label();
                        lblcategory_head.Name = "lbcate_head_Com_" + category.CategoryName;
                        lblcategory_head.Tag = category.Fcid;
                        lblcategory_head.Text = category.CategoryName;
                        lblcategory_head.ForeColor = Color.Black;
                        lblcategory_head.BackColor = Color.Transparent;
                        lblcategory_head.Dock = DockStyle.Top;
                        lblcategory_head.Font = new Font(lblcategory_head.Font.FontFamily, 12, FontStyle.Bold);

                        int rowIndex = 1;
                        int columnIndex = 0;

                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            if (gridSource[i].OrderStatus == 1 || Convert.ToInt32(gridSource[i].OrderMasterId) > 0)
                            {
                                BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                                dmnSearch.Select = "CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1";
                                dmnSearch.Table = "tblOrderDetails";
                                dmnSearch.WhereNdGroupBy = "WHERE status!=0 and OrderMasterId = " + gridSource[i].OrderMasterId;
                                dmnSearch.OrderBy = "";
                                var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);
                                if (gridSource_order.Count > 0 && Convert.ToInt32(gridSource_order[0].Field1) == 0)
                                {
                                    categoryHasTables = true;
                                    if (gridSource[i].Fcid == category.Fcid)
                                    {
                                        if (columnIndex == 7)
                                        {
                                            columnIndex = 0;
                                            rowIndex++;
                                        }
                                        // Configure the TableLayoutPanel
                                        TableLayoutPanel tlp_table_inner = new TableLayoutPanel();
                                        tlp_table_inner.RowCount = 3;
                                        tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
                                        tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
                                        tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
                                        tlp_table_inner.Name = "tlp_com" + gridSource[i].TableCode;
                                        tlp_table_inner.Tag = gridSource[i].Tid;
                                        tlp_table_inner.BorderStyle = BorderStyle.FixedSingle;
                                        // tlp_table_inner.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                                        tlp_table_inner.Margin = new Padding(30, 10, 20, 10);
                                        tlp_table_inner.Dock = DockStyle.Fill;
                                        // tlp_table_inner.BackColor = Color.Aqua;

                                        PictureBox pictureBox = new PictureBox();
                                        Label lbltablename = new Label();

                                        Panel pnl_box = new Panel();
                                        pnl_box.Name = "pnl_box_com" + gridSource[i].TableCode;
                                        pnl_box.Tag = gridSource[i].Tid;
                                        pnl_box.BackColor = Color.Transparent;
                                        pnl_box.Width = 120;
                                        pnl_box.Height = 80;
                                        pnl_box.BackColor = Color.Transparent;

                                        CheckBox chkBox_sele = new CheckBox();
                                        chkBox_sele.Name = "chkBox_com" + gridSource[i].TableCode;
                                        chkBox_sele.AccessibleDescription = Convert.ToString(gridSource[i].OrderMasterId) + "," + gridSource[i].Tid;
                                        chkBox_sele.Tag = gridSource[i].TableCode;
                                        chkBox_sele.BackColor = Color.Transparent;
                                        chkBox_sele.Text = "";
                                        chkBox_sele.Margin = new Padding(95, 2, 0, 0);
                                        chkBox_sele.CheckedChanged += new EventHandler(chkBox_sele_CheckedChanged);


                                        // Configure the PictureBox
                                        pictureBox.Name = "pb_com" + gridSource[i].TableCode;
                                        pictureBox.Tag = gridSource[i].Tid;
                                        pictureBox.Image = FoodandBeverage.Properties.Resources.restaurant;
                                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                                        tlp_table_inner.Dock = DockStyle.Fill;

                                        // Configure the Label
                                        lbltablename.Name = "lb_com" + gridSource[i].TableCode;
                                        lbltablename.TextAlign = ContentAlignment.MiddleLeft;
                                        lbltablename.Tag = gridSource[i].Tid;
                                        lbltablename.Text = gridSource[i].TableCode;
                                        lbltablename.ForeColor = Color.Black;
                                        lbltablename.BackColor = Color.Transparent;
                                        lbltablename.Font = new Font("Arial", 10, FontStyle.Bold);        
                                        lbltablename.ForeColor = Color.Red;
                      
                                        if (Convert.ToInt32(gridSource[i].OrderStatus) == 1)
                                            tlp_table_inner.Controls.Add(chkBox_sele, 0, 0);
                                        // Add the PictureBox and Label to the TableLayoutPanel 
                                        tlp_table_inner.Controls.Add(pictureBox, 0, 1); // PictureBox in the sec row
                                        tlp_table_inner.Controls.Add(lbltablename, 0, 2); // Label in the thrid row

                                        lbltablename.Dock = DockStyle.Fill;

                                        pictureBox.MouseHover += pictureBox_com_MouseHover;
                                        pictureBox.MouseLeave += pictureBox_com_MouseLeave;
                                        pictureBox.Click += new EventHandler(CompleteEventHandler);
                                        lbltablename.Click += new EventHandler(CompleteEventHandler);
                                        tlp_table_inner.Click += new EventHandler(CompleteEventHandler);

                                        if (gridSource[i].OrderStatus == 1)
                                        {
                                            pictureBox.AccessibleDescription = "0";
                                            lbltablename.AccessibleDescription = "0";
                                            tlp_table_inner.AccessibleDescription = "0";
                                            pnl_box.AccessibleDescription = "0";
                                        }
                                        else if (Convert.ToInt32(gridSource[i].OrderMasterId) > 0)
                                        {
                                            pictureBox.AccessibleDescription = Convert.ToInt32(DashboardTableType.CombinedOrderBill) + "," + Convert.ToInt32(gridSource[i].BookmasterId);
                                            lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.CombinedOrderBill) + "," + Convert.ToInt32(gridSource[i].BookmasterId);
                                            tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.CombinedOrderBill) + "," + Convert.ToInt32(gridSource[i].BookmasterId);
                                            pnl_box.AccessibleDescription = Convert.ToInt32(DashboardTableType.CombinedOrderBill) + "," + Convert.ToInt32(gridSource[i].BookmasterId);
                                        }

                                        pnl_box.Controls.Add(tlp_table_inner);
                                        PictureBox dynamic_pic = new PictureBox();
                                        dynamic_pic.Name = "pb_com" + gridSource[i].TableCode;
                                        dynamic_pic.Tag = gridSource[i].Tid;

                                        if (gridSource[i].OrderStatus == 1)
                                        {

                                            dynamic_pic.AccessibleDescription = "0";
                                        }
                                        else if (Convert.ToInt32(gridSource[i].OrderMasterId) > 0)
                                        {
                                            dynamic_pic.AccessibleDescription = Convert.ToInt32(DashboardTableType.CombinedOrderBill) + "," + Convert.ToInt32(gridSource[i].BookmasterId);
                                        }

                                        if (Convert.ToInt32(gridSource_order[0].Field1) == 1)
                                            dynamic_pic.Image = FoodandBeverage.Properties.Resources.running_table_pic;
                                        else
                                            dynamic_pic.Image = FoodandBeverage.Properties.Resources.billcomplete_GIF;

                                        dynamic_pic.SizeMode = PictureBoxSizeMode.StretchImage;
                                        dynamic_pic.BackColor = Color.Transparent;
                                        dynamic_pic.Width = 40;
                                        dynamic_pic.Height = 40;
                                        pnl_box.Controls.Add(dynamic_pic);
                                        dynamic_pic.BringToFront();
                                        dynamic_pic.Margin = new Padding(20, 10, 0, 0);
                                        dynamic_pic.Click += new EventHandler(CompleteEventHandler);

                                        tlp_category.Controls.Add(pnl_box, columnIndex, rowIndex);
                                        columnIndex++;
                                    }
                                }

                            }

                        }
                        int current_rowIndex_cate = rowIndex_cate;
                        tlp_table_dashboard.Controls.Add(lblcategory_head, 0, rowIndex_cate);
                        rowIndex_cate++;
                        tlp_table_dashboard.Controls.Add(tlp_category, 0, rowIndex_cate);
                        rowIndex_cate++;

                        int count = tlp_category.Controls.OfType<Panel>().Count();
                        if (count == 0)
                        {
                            tlp_table_dashboard.Controls.Remove(lblcategory_head);
                            tlp_table_dashboard.Controls.Remove(tlp_category);
                        }
                    }

                    //tlp_table_dashboard.BorderStyle = BorderStyle.FixedSingle;
                    tlp_table_dashboard_main.Controls.Add(tlp_table_dashboard, 0, 1);
                    pnl_compelte_bill.Controls.Add(tlp_table_dashboard_main);
                    pnl_compelte_bill.Dock = DockStyle.Fill;
                    pnl_compelte_bill.Visible = true;

                    if (tlp_table_dashboard.Controls.Count == 0)
                    {
                        controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
                        tlp_compl_pre_bill.Controls.Remove(controlToRemove);
                        tlp_compl_pre_bill.Controls.Add(pnl_empty_msg, 0, 1);
                        pnl_empty_msg.Dock = DockStyle.Fill;
                        pnl_empty_msg.Visible = true;
                        btn_prevbill.PerformClick();
                    }


                    #endregion
                }
                else
                {
                    controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
                    tlp_compl_pre_bill.Controls.Remove(controlToRemove);
                    tlp_compl_pre_bill.Controls.Add(pnl_empty_msg, 0, 1);
                    pnl_empty_msg.Dock = DockStyle.Fill;
                    pnl_empty_msg.Visible = true;
                    btn_prevbill.PerformClick();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void Menureport_Click(object sender, EventArgs e)
        {
            try
            {
                AutoTimer.Stop();

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

            }
            catch(Exception ex)
            {
            }           

        }
        
    private void VendorReports_Click(object sender, EventArgs e)
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
        private void TableTurnoverReports_Click(object sender, EventArgs e)
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
        private void MenuAnalysisReports_Click(object sender, EventArgs e)
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

        private void PurchaseitemItem_Click(object sender, EventArgs e)
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

        private void ExpenseReportItem_Click(object sender, EventArgs e)
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
        private void ReservationSeatingItem_Click(object sender, EventArgs e)
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
        private void EmployeePerformanceItem_Click(object sender, EventArgs e)
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
        private void PromotionsDiscountsItem_Click(object sender, EventArgs e)
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
        private void SalesReportItem_Click(object sender, EventArgs e)
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

        private void InventoryReportItem_Click(object sender, EventArgs e)
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

        private void Menusetting_Click(object sender, EventArgs e)
        {
            AutoTimer.Stop();
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
            button_type.BackColor = menubuttoncolor;
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
            button_message.BackColor = menubuttoncolor;
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
            //lbldateTime.Text = DateTime.Now.ToLongTimeString()+"      "+ DateTime.Now.ToShortDateString(); 
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

        private void navibar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousepos = Control.MousePosition;
                mousepos.Offset(MouseLocation.X, MouseLocation.Y);
                Location = mousepos;
            }
        }

        private void navibar_MouseDown(object sender, MouseEventArgs e)
        {
            MouseLocation = new Point(-e.X, -e.Y);

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

        private void pbappcoloepicker_Click(object sender, EventArgs e)
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

        private void btn_avial_Click(object sender, EventArgs e)
        {
            AutoTimer.Stop();
            LoadTableStaus(3);
        }

        private void btn_rev_Click(object sender, EventArgs e)
        {
            AutoTimer.Stop();
            LoadTableStaus(2);
        }

        private void btn_running_Click(object sender, EventArgs e)
        {
            AutoTimer.Stop();
            LoadTableStaus(1);
        }

        private void btntrefresh_Click(object sender, EventArgs e)
        {
            AutoTimer.Stop();
            LoadTableStaus(0);
        }

        private void ib_Category_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["FrmFTICategory"] != null) {
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

        private void ib_Item_Click(object sender, EventArgs e)
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

        private void ib_menu_Click(object sender, EventArgs e)
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

        private void ib_unit_Click(object sender, EventArgs e)
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

        private void ib_table_Click(object sender, EventArgs e)
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

        private void ib_purchase_Click(object sender, EventArgs e)
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

        private void ib_purchasereturn_Click(object sender, EventArgs e)
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

        private void ib_tax_Click(object sender, EventArgs e)
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

        private void SetRoundedButton(Button button, int cornerRadius)
        {
            using (var path = new GraphicsPath())
            {
                Rectangle rect = new Rectangle(0, 0, button.Width, button.Height);

                path.StartFigure();
                path.AddArc(rect.Left, rect.Top, cornerRadius, cornerRadius, 180, 90);
                path.AddArc(rect.Right - cornerRadius, rect.Top, cornerRadius, cornerRadius, 270, 90);
                path.AddArc(rect.Right - cornerRadius, rect.Bottom - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                path.AddArc(rect.Left, rect.Bottom - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                path.CloseFigure();

                button.Region = new Region(path);
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

        private void ib_withdrawal_Click(object sender, EventArgs e)
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

        private void ib_kitchenview_MouseLeave(object sender, EventArgs e)
        {
            ib_kitchenview.Text = Convert.ToString(ib_kitchenview.Tag);
            SetButtonImage(ib_kitchenview, FoodandBeverage.Properties.Resources.kitchen_order_view, desiredWidth, desiredHeight);
        }

        private void ib_kitchenview_Click(object sender, EventArgs e)
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

        private void ib_PromoCode_Click(object sender, EventArgs e)
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

        private void ib_PromoCode_MouseLeave(object sender, EventArgs e)
        {
            ib_PromoCode.Text = Convert.ToString(ib_PromoCode.Tag);
            SetButtonImage(ib_PromoCode, FoodandBeverage.Properties.Resources.promo_code, desiredWidth, desiredHeight);
        }

        private void pbopbuttoncoloepicker_Click(object sender, EventArgs e)
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
            }
        }

        private void pbopbuttoncoloepicker_Click_1(object sender, EventArgs e)
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
                Button btnsetting = this.Controls.Find("Appcolor", true).FirstOrDefault() as Button;
                if (btnsetting != null)
                    btnsetting.BackColor = opbuttoncolor;
            }
        }

        private void btn_minimium_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void chkdef_CheckedChanged(object sender, EventArgs e)
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

        private void ib_Company_Click(object sender, EventArgs e)
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

        private void ib_Company_MouseLeave(object sender, EventArgs e)
        {
            ib_Company.Text = Convert.ToString(ib_Company.Tag);
            SetButtonImage(ib_Company, FoodandBeverage.Properties.Resources.comapny, desiredWidth, desiredHeight);
        }


        private void menuEmployee_Click(object sender, EventArgs e)
        {
            AutoTimer.Stop();
            panelDesktop.Controls.Clear();
            flpEmployee.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(flpEmployee);
            flpEmployee.Visible = true;
            if (screenWidth > 1367 && screenHeight > 769)
            {
                foreach (Control control in flpEmployee.Controls)
                {
                    if (control is Button buttonm)
                    {
                        buttonm.Size = new Size(300, 120);
                        buttonm.Margin = new Padding(105, 35, 10, 2);
                        buttonm.Font = new Font(buttonm.Font.FontFamily, 16, buttonm.Font.Style);
                    }
                }
            }
            else
            {
                foreach (Control control in flpEmployee.Controls)
                {
                    if (control is Button buttonm)
                    {
                        buttonm.Size = new Size(212, 89);
                    }
                }
            }
        }

        private void ib_employee_Click(object sender, EventArgs e)
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

        private void ib_department_Click(object sender, EventArgs e)
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

        private void ib_designation_Click(object sender, EventArgs e)
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

        private void ib_useraccount_Click(object sender, EventArgs e)
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

        private void ib_reservation_MouseLeave(object sender, EventArgs e)
        {
            ib_reservation.Text = Convert.ToString(ib_reservation.Tag);
            SetButtonImage(ib_reservation, FoodandBeverage.Properties.Resources.reserved, desiredWidth, desiredHeight);
        }

        private void ib_userpermission_Click(object sender, EventArgs e)
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

            foreach (Control control in flp_operations.Controls)
                control.Margin = new Padding(marginLeft, marginTop, marginRight, marginBottom);

        }
        public void FormPermissionChecking()
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
                }
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
        public class ComboBoxItem
        {
            public int Id { get; set; }
            public string Value { get; set; }

            public ComboBoxItem(int id, string value)
            {
                Id = id;
                Value = value;
            }

            public override string ToString()
            {
                return Value;
            }
        }
        public void FillTablecode_Comb(int rowindex, int salemasterid,int ordermasterid, int ordertype)
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                DataGridViewComboBoxCell dropDown = (DataGridViewComboBoxCell)dgvorderview.Rows[rowindex].Cells[tablecodeno];
                if (ordertype==0)
                {
                    objSearch.Select = "bd.MTID id,bd.Name value";
                    objSearch.Table = "tblOrderMaster om " +
                        "left join tblbookMaster bm on om.BookmasterId=bm.BookmasterId " +
                        "left join tblBookDetails bd on bd.BookmasterId=bm.BookmasterId and bd.Rid=0 ";
                    objSearch.WhereNdGroupBy = "where om.OrderMasterId=" + ordermasterid;
                    objSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                    gridSource[0].Id = 0;
                    gridSource[0].Value = "Show Table";
                    dropDown.DataSource = gridSource;
                    dropDown.ValueMember = "Id";
                    dropDown.DisplayMember = "Value";

                    dropDown.Value = gridSource[0].Id;
                    dropDown.ReadOnly = false;
                    dgvorderview.Rows[rowindex].Cells[tablecodeno].ReadOnly = false;
                }
                else
                {
                    objSearch.Select = "com.tid id,ti.TableCode value";
                    objSearch.Table = "tblCombinedOrderBill com left join tblTable_info ti on ti.tid=com.Tid";
                    objSearch.WhereNdGroupBy = "where SalesMasterId =" + salemasterid;
                    objSearch.OrderBy = "";
                    var  gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                    gridSource[0].Id = 0;
                    gridSource[0].Value = "Show Table";
                    dropDown.DataSource = gridSource;
                    dropDown.ValueMember = "Id";
                    dropDown.DisplayMember = "Value";

                    dropDown.Value = gridSource[0].Id;
                    dropDown.ReadOnly = false;
                    dgvorderview.Rows[rowindex].Cells[tablecodeno].ReadOnly = false;
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void btn_prevbill_Click(object sender, EventArgs e)
        {
            panelDesktop.Controls.Clear();
            panelDesktop.Controls.Add(pnl_complebill_prevbill);
            pnl_complebill_prevbill.Dock = DockStyle.Fill;

            Control controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
            tlp_compl_pre_bill.Controls.Remove(controlToRemove);
            tlp_compl_pre_bill.Controls.Add(pnl_previous_bill, 0, 1);
            tlp_compl_pre_bill.Dock = DockStyle.Fill;
            pnl_previous_bill.Dock = DockStyle.Fill;
            pnl_previous_bill.Visible = true;
            pnl_empty_msg.Visible = false;

            btn_backCompleorder.Visible = true;
            btn_prevbill.Visible = false;
            try
            {
                #region Fill complete or cancelled order in Datagrid
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "Top 20 sm.CreatedDateTime Field1,sm.EntryNo Field2,ti.TableCode Field3,sm.TotalQuantity Field4,sm.Status Field5,sm.SalesMasterId Field6 ,sm.NetAmount Field7,sm.RcvAmnt Field8,sm.BalAmnt Field9,sm.PaymentMode Field10,sm.Tid Field11,sm.CombinedOrderBill_TYPE Field12,sm.OrderMasterid Field13";
                dmnSearch.Table = "tblSalesMaster sm LEFT JOIN tblTable_info ti on ti.tid=sm.Tid";
                dmnSearch.WhereNdGroupBy = "where sm.status=1";
                dmnSearch.OrderBy = "order by sm.SalesMasterId DESC";
                dgvorderview.Rows.Clear();
                int rowIndex = 0;
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    int imageSize = 32;
                    dgvorderview.Rows.Add(gridSource.Count);
                    int x = dgvorderview.RowCount;
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        dgvorderview.Rows[rowIndex].Cells[cslno].Value = rowIndex + 1;
                        dgvorderview.Rows[rowIndex].Cells[orderdatetimeno].Value = gridSource[i].Field1;
                        dgvorderview.Rows[rowIndex].Cells[entryno].Value = gridSource[i].Field2;

                        dgvorderview.Rows[rowIndex].Cells[tablecodeno].Value = gridSource[i].Field3;
                        #region multiple table code case reservation 
                        if (Convert.ToInt32(gridSource[i].Field11) == 0)
                        {
                            FillTablecode_Comb(rowIndex, Convert.ToInt32(gridSource[i].Field6), Convert.ToInt32(gridSource[i].Field13), Convert.ToInt32(gridSource[i].Field12));
                        }
                        else
                        {
                            DataGridViewComboBoxCell dropDown = (DataGridViewComboBoxCell)dgvorderview.Rows[rowIndex].Cells[tablecodeno];
                            List<ComboBoxItem> comboBoxItems = new List<ComboBoxItem>();
                            ComboBoxItem comboBoxItem = new ComboBoxItem(Convert.ToInt32(gridSource[i].Field11), gridSource[i].Field3);
                            comboBoxItems.Add(comboBoxItem);
                            dropDown.DataSource = comboBoxItems;
                            dropDown.ValueMember = "Id";
                            dropDown.DisplayMember = "Value";
                            dropDown.Value = comboBoxItems[0].Id;
                            dropDown.ReadOnly = true;
                        }
                        #endregion


                        dgvorderview.Rows[rowIndex].Cells[totalqtyno].Value = gridSource[i].Field4;
                        dgvorderview.Rows[rowIndex].Cells[ordermasteridno].Value = gridSource[i].Field6;
                        dgvorderview.Rows[rowIndex].Cells[netamountno].Value = gridSource[i].Field7;
                        dgvorderview.Rows[rowIndex].Cells[recamountno].Value = gridSource[i].Field8;
                        dgvorderview.Rows[rowIndex].Cells[balamountno].Value = gridSource[i].Field9;
                        switch (Convert.ToInt32(gridSource[i].Field10))
                        {
                            case 1:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "CASH";
                                break;
                            case 5:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "DEBITCARD";
                                break;
                            case 4:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "UPI";
                                break;
                            case 6:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "BANKINGONLINE";
                                break;
                            case 3:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "CREDIT";
                                break;
                            case 7:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "OTHERS";
                                break;
                            case 11:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "PART";
                                break;
                            case 12:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "SPLIT";
                                break;
                        }

                        DataGridViewImageCell cell = (DataGridViewImageCell)dgvorderview.Rows[rowIndex].Cells[statusno];
                        cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        cell.Value = Properties.Resources.orderview_complete;

                        DataGridViewImageCell cell1 = (DataGridViewImageCell)dgvorderview.Rows[rowIndex].Cells[actionno];
                        cell1.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        cell1.Value = Properties.Resources.viewbill_billing;

                        rowIndex++;
                    }
                }
                else
                {

                    controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
                    tlp_compl_pre_bill.Controls.Remove(controlToRemove);
                    tlp_compl_pre_bill.Controls.Add(pnl_empty_msg, 0, 1);
                    pnl_empty_msg.Dock = DockStyle.Fill;
                    pnl_empty_msg.Visible = true;
                }

                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_serach_prev_bill_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "ID,DateTime,Invoice No,Table Code,TotalQuantity,NetAmount,RcvAmnt,BalAmnt,PaymentMode";
                _objSearch.Select = "sm.SalesMasterId ID,sm.CreatedDateTime Field1,sm.EntryNo Field2,ti.TableCode Field3,sm.TotalQuantity Field4,sm.NetAmount Field5,sm.RcvAmnt Field6,sm.BalAmnt Field7,g.Caption Field8";
                _objSearch.Table = "tblSalesMaster sm LEFT JOIN tblTable_info ti on ti.tid=sm.Tid LEFT JOIN tblGeneralOptions g ON g.GTypeSubId=sm.PaymentMode";
                _objSearch.WhereNdGroupBy = "where sm.status=1 and  g.GTypeId=2";
                _objSearch.OrderBy = "order by sm.SalesMasterId DESC";
                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))
                    {
                        var dialogResult = search.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                        {
                            returnValue = Convert.ToString(search._retValue);
                        }
                    }
                    if (returnValue != "")
                    {
                        int oid = Convert.ToInt32(returnValue);
                        printBill(oid);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_backCompleorder_Click(object sender, EventArgs e)
        {
            btn_backCompleorder.Visible = false;
            btn_prevbill.Visible = true;
            Billcomplete_Fill();
        }
        private void dgvorderview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == actionno)
                {
                    int oid = Convert.ToInt32(dgvorderview.Rows[e.RowIndex].Cells[ordermasteridno].Value);
                    printBill(oid);
                }

            }
            catch (Exception ex)
            {

            }

        }
        public void printBill(int oid)
        {
            try
            {
                DmnSearch obj = new DmnSearch();
                BllSearch<DmnGridFields> bllpayments = new BllSearch<DmnGridFields>();
                obj.Select = "PaymentMode Field1,SalesMasterId Field2";
                obj.Table = "tblSalesMaster";
                obj.WhereNdGroupBy = " where status=1 and SalesMasterId=" + oid;
                var result = bllpayments.GetSearchData("prdSearch", obj);
                if (result.Count > 0)
                {
                    if (Convert.ToInt32(result[0].Field1) != 12)
                    {
                        pnl_split.Visible = false;
                        FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(oid, Convert.ToInt32(result[0].Field1), 0);
                        frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                        frmSalesInvoice.ShowDialog();
                    }
                    else
                    {
                        obj.Select = "Amount Field1,SplitType Field2,SalemasterId Field3,PayId Field4 ";
                        obj.Table = "tblpayment";
                        obj.WhereNdGroupBy = " where status=1 and SaleMasterId=" + oid;
                        var paymentresult = bllpayments.GetSearchData("prdSearch", obj);
                        if (paymentresult.Count > 0)
                        {
                            dgv_split.Rows.Clear();
                            dgv_split.Rows.Add(paymentresult.Count);
                            int x = dgv_split.RowCount;
                            for (int i = 0; i < paymentresult.Count; i++)
                            {
                                dgv_split.Rows[i].Cells[0].Value = i + 1;
                                dgv_split.Rows[i].Cells[1].Value = paymentresult[i].Field1;
                                DataGridViewImageCell cell = (DataGridViewImageCell)dgv_split.Rows[i].Cells[2];
                                cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                cell.Value = Properties.Resources.eye;
                                dgv_split.Rows[i].Cells[3].Value = paymentresult[i].Field3;
                                dgv_split.Rows[i].Cells[4].Value = paymentresult[i].Field4;
                            }
                        }
                        pnl_split.Location = new Point(this.ClientSize.Width / 2 - pnl_split.Size.Width + 500, this.ClientSize.Height / 2 - pnl_split.Size.Height / 2);
                        pnl_split.Visible = true;
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }
        private void dgv_split_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(Convert.ToInt32(dgv_split.Rows[e.RowIndex].Cells[3].Value), Convert.ToInt32(PaymentType.SPLIT), Convert.ToInt32(dgv_split.Rows[e.RowIndex].Cells[4].Value));
                frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                frmSalesInvoice.ShowDialog();
            }
        }

        private void btn_close_split_Click(object sender, EventArgs e)
        {
            pnl_split.Visible = false;
        }

        private void btn_save_msg_Click(object sender, EventArgs e)
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

        private void btn_close_msg_Click(object sender, EventArgs e)
        {
            panl_msgsettings.Visible = false;
        }

        private void dgvorderview_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void Menutable_Click(object sender, EventArgs e)
        {
            if (screenWidth > 1367 && screenHeight > 769)
            {

                foreach (Control control in flp_operations.Controls)
                {
                    if (control is Button buttonm)
                    {
                        buttonm.Size = new Size(300, 120);
                    }
                }
            }
            else
            {
                foreach (Control control in flp_operations.Controls)
                {
                    if (control is Button buttonm)
                    {
                        buttonm.Size = new Size(212, 89);
                    }
                }
            }
            if (sender is Button button)
            {
                ChangeButtonBorder(button);
            }
            LoadTableStaus(0);
        }
    }

}
