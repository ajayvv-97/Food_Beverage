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

namespace FoodandBeverage.Forms
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
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
        #region Declaration

        private bool isClicked = false;
        Color menubuttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
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

        public int desiredWidth = 32;
        public int desiredHeight = 32;

        public int HdesiredWidth = 10;
        public int HdesiredHeight = 10;
        public bool collapsemenu_click = false;
        #endregion

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
            txtname_search.Text = "Food Name .....";
            txtitemcode_serach.Text = "Item Code .....";
            tlp_main.BackColor = menubuttoncolor;
            flp_navbar.BackColor = menubuttoncolor;
            Menuhome.Focus();
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            RoundPictureBoxCorners(pb_appicon, 10);
            TableStaus(0);
            pbappcoloepicker.BackColor = Color.FromArgb(34, 46, 80);
            Roundform(30);
            // tlp_main.BackColor = Color.FromArgb(152, 193, 217);
            //set color on solidebar header
            pnl_header.BackColor = menubuttoncolor;

            tlp_main_sidebar_pnldesktop.BackColor = Color.FromArgb(34, 46, 80);
            this.FormBorderStyle = FormBorderStyle.None;
            timer1.Start();
            Menuhome.BackColor = menubuttoncolor;
            Menuoperation.BackColor = menubuttoncolor;
            Menubilling.BackColor = menubuttoncolor;
            Menureport.BackColor = menubuttoncolor;
            Menulogout.BackColor = menubuttoncolor;
            Menusetting.BackColor = menubuttoncolor;
            button_imageset();
            flp_menu.BackColor = Color.White;
            tlp_main_sidebar_pnldesktop.CellPaint += new TableLayoutCellPaintEventHandler(this.tlp_main_CellPaint);

            //operation button color 
            ib_Category.BackColor = menubuttoncolor;
            ib_Item.BackColor = menubuttoncolor;
            ib_ingrediant.BackColor = menubuttoncolor;
            ib_menu.BackColor = menubuttoncolor;
            ib_unit.BackColor = menubuttoncolor;
            ib_table.BackColor = menubuttoncolor;
            ib_purchase.BackColor = menubuttoncolor;
            ib_purchasereturn.BackColor = menubuttoncolor;
            ib_tax.BackColor = menubuttoncolor;
            ib_vendor.BackColor = menubuttoncolor;
            ib_Discount_offer.BackColor = menubuttoncolor;
            ib_withdrawal.BackColor = menubuttoncolor;
            ib_orderview.BackColor = menubuttoncolor;
            ib_kitchenview.BackColor = menubuttoncolor;
            textb_shape();
        }
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
            SetButtonImage(ib_withdrawal, FoodandBeverage.Properties.Resources.withdraw, desiredWidth, desiredHeight);
            SetButtonImage(ib_Discount_offer, FoodandBeverage.Properties.Resources.discount, desiredWidth, desiredHeight);
            SetButtonImage(ib_orderview, FoodandBeverage.Properties.Resources.orderview, desiredWidth, desiredHeight);
            SetButtonImage(ib_kitchenview, FoodandBeverage.Properties.Resources.kitchen_order_view, desiredWidth, desiredHeight);

            SetRoundedButton(ib_Category, 70);
            SetRoundedButton(ib_Item, 70);
            SetRoundedButton(ib_ingrediant, 70);
            SetRoundedButton(ib_menu, 70);
            SetRoundedButton(ib_unit, 70);
            SetRoundedButton(ib_table, 70);
            SetRoundedButton(ib_purchase, 70);
            SetRoundedButton(ib_purchasereturn, 70);
            SetRoundedButton(ib_tax, 70);
            SetRoundedButton(ib_vendor, 70);
            SetRoundedButton(ib_withdrawal, 70);
            SetRoundedButton(ib_Discount_offer, 70);
            SetRoundedButton(ib_orderview, 70);
            SetRoundedButton(ib_kitchenview, 70);

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
            if (sender is Button button)
            {
                ChangeButtonBorder(button);
            }
            TableStaus(0);
        }
        public class CategoryInfo
        {
            public int Fcid { get; set; }
            public string CategoryName { get; set; }
        }
        public void TableStaus(int serachmode)
        {
            BllSearch<DmnTableinfo> bllSearch = new BllSearch<DmnTableinfo>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = "ti.* ,c.CategoryName as CategoryName";
            dmnSearch.Table = "tblTable_info  ti LEFT JOIN tblCategory_FTI c on c.Fcid=ti.Fcid";
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
                    TableLayoutPanel tlp_category = new TableLayoutPanel();

                    tlp_category.Name = "tlp_category_" + category.CategoryName;
                    tlp_category.Tag = category.CategoryName;
                    tlp_category.Dock = DockStyle.Top;
                    tlp_category.Controls.Clear();
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

                    int rowIndex = 1;
                    int columnIndex = 0;

                    for (int i = 0; i < gridSource.Count; i++)
                    {                    
                        if(gridSource[i].Fcid== category.Fcid)
                        {
                            if (columnIndex == 7)
                            {
                                columnIndex = 0;
                                rowIndex++;
                            }
         
                            TableLayoutPanel tlp_table_inner = new TableLayoutPanel(); 
                            PictureBox pictureBox = new PictureBox();
                            Label lbltablename = new Label();

                            Panel pnl_box = new Panel();
                            pnl_box.Name = "pnl_box" + gridSource[i].TableCode;
                            pnl_box.Tag = gridSource[i].Tid;
                            pnl_box.BackColor = Color.Transparent;
                            pnl_box.Width = 120;
                            pnl_box.Height = 80;
                            pnl_box.BackColor = Color.Transparent;

                            // Configure the PictureBox
                            pictureBox.Name = "pb" + gridSource[i].TableCode;
                            pictureBox.Tag = gridSource[i].Tid;
                            pictureBox.Image = FoodandBeverage.Properties.Resources.restaurant;
                            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            ////pictureBox.BackColor = menubuttoncolor;
                            //pictureBox.Width = 45;
                            //pictureBox.Height = 45;
                            tlp_table_inner.Dock = DockStyle.Fill;
                            //pictureBox.Margin = new Padding(15, 0, 0, 0);

                            // Configure the Label
                            lbltablename.Name = "lb" + gridSource[i].TableCode;
                            lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                            lbltablename.Tag = gridSource[i].Tid;
                            lbltablename.Text = gridSource[i].TableCode;
                            lbltablename.ForeColor = Color.Black;
                            lbltablename.BackColor = Color.Transparent;
                            lbltablename.Dock = DockStyle.Top;
                            //lbltablename.Margin = new Padding(10, 10, 0, 0);
                            lbltablename.Font = new Font("Arial", 10, FontStyle.Bold);
                            if (gridSource[i].OrderStatus == 1)
                                lbltablename.ForeColor = Color.Red;
                            else if (gridSource[i].RevStatus == 1)
                                lbltablename.ForeColor = Color.Yellow;
                            else
                                lbltablename.ForeColor = Color.Green;


                            // Add the PictureBox and Label to the TableLayoutPanel
                            tlp_table_inner.Controls.Add(pictureBox, 0, 0); // PictureBox in the first row
                            tlp_table_inner.Controls.Add(lbltablename, 0, 1); // Label in the second row

                            // Configure the TableLayoutPanel
                            tlp_table_inner.RowCount = 2;
                            tlp_table_inner.Name = "tlp" + gridSource[i].TableCode;
                            tlp_table_inner.Tag = gridSource[i].Tid;
                            tlp_table_inner.BorderStyle = BorderStyle.FixedSingle;
                            tlp_table_inner.Dock = DockStyle.None;
                            tlp_table_inner.Margin = new Padding(20, 10, 20, 10);
                            //tlp_table_inner.Width = 100;
                            //tlp_table_inner.Height = 65;
                            tlp_table_inner.Dock = DockStyle.Fill;
                            pictureBox.MouseHover += pictureBox_MouseHover;
                            pictureBox.MouseLeave += pictureBox_MouseLeave;
                            pictureBox.Click += new EventHandler(tableClickEventHandler);
                            lbltablename.Click += new EventHandler(tableClickEventHandler);
                            tlp_table_inner.Click += new EventHandler(tableClickEventHandler);

                            pnl_box.Controls.Add(tlp_table_inner);

                            if (gridSource[i].OrderStatus == 1)
                            {
                                PictureBox dynamic_pic = new PictureBox();
                                dynamic_pic.Name = "pb" + gridSource[i].TableCode;
                                dynamic_pic.Tag = gridSource[i].Tid;
                                dynamic_pic.Image = FoodandBeverage.Properties.Resources.running_table_pic;
                                dynamic_pic.SizeMode = PictureBoxSizeMode.StretchImage;
                                dynamic_pic.BackColor = Color.Transparent;
                                dynamic_pic.Width = 50;
                                dynamic_pic.Height = 50;      
                                pnl_box.Controls.Add(dynamic_pic);
                                dynamic_pic.BringToFront();
                                dynamic_pic.Margin = new Padding(20, 10, 0, 0);
                                dynamic_pic.Click += new EventHandler(tableClickEventHandler);
                            }

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
        }
        private void tableClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                if (clickedControl != null)
                {
                    string tableCode = clickedControl.Name;
                    int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                    int screenHeight = Screen.PrimaryScreen.Bounds.Height;
                    if (screenWidth > 1024 && screenHeight > 800)
                    {
                        Control controlToRemove = tlp_main.GetControlFromPosition(0, 1);
                        tlp_main.Controls.Remove(controlToRemove);
                        tlp_main.Controls.Add(pnl_tableorder);
                        pnl_tableorder.Dock = DockStyle.Fill;
                        pnl_tableorder.Visible = true;

                        #region Side Bar for Search box
                        //sidebar 
                        tlp_main_sidebar_pnldesktop.Visible = false;
                        pnlsidebar_searchproduct.Controls.Clear();
                        tlp_searchproduct_sidebar.Controls.Clear();
                        tlp_searchproduct_sidebar.Dock = DockStyle.Fill;
                        tlp_searchproduct_sidebar.AutoScroll = true;
                        BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "*";
                        dmnSearch.Table = "tblCategory_FTI";
                        dmnSearch.WhereNdGroupBy = "where Status=1 and CategoryType=0";
                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            int rowIndex = 0;
                            // Configure the Button
                            Button buttonfav = new Button();
                            buttonfav.Text = "Favouriate Item";
                            buttonfav.Name = "btn_fav";
                            buttonfav.Tag = 0;
                            buttonfav.BackColor = Color.Beige;
                            buttonfav.Width = 145;
                            buttonfav.Height = 40;

                            buttonfav.Font = new Font(buttonfav.Font.FontFamily, 8, FontStyle.Bold);
                            buttonfav.Click += new EventHandler(SidebuttonfavClickEventHandler);
                            tlp_searchproduct_sidebar.Controls.Add(buttonfav, 0, rowIndex);
                            rowIndex++;

                            for (int i = 0; i < gridSource.Count; i++)
                            {
                                // Configure the Button
                                Button button = new Button();
                                button.Text = gridSource[i].CategoryName;
                                button.Name = "btn" + gridSource[i].CategoryName;
                                button.Tag = gridSource[i].Fcid;
                                button.BackColor = Color.Beige;
                                button.Width = 145;
                                button.Height = 40;

                                button.Font = new Font(button.Font.FontFamily, 8, FontStyle.Bold);
                                button.Click += new EventHandler(SidebuttonClickEventHandler);
                                tlp_searchproduct_sidebar.Controls.Add(button, 0, rowIndex);
                                rowIndex++;
                            }
                            pnlsidebar_searchproduct.Controls.Add(tlp_searchproduct_sidebar);
                            pnl_toggle_btn.Dock = DockStyle.Fill;
                            btn_sidebar_toggle.Location = new Point(pnl_toggle_btn.Width / 2 - btn_sidebar_toggle.Size.Width / 2, pnl_toggle_btn.Height / 2 - btn_sidebar_toggle.Size.Height / 2);
                            pnlsidebar_searchproduct.Visible = false;
                            lblcategory.Visible = false;
                        }
                        #endregion
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
                            if (clickedControl.Tag != null )
                            {
                                FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag));
                                _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                _instanceFrmOrder.Show();
                                _instanceFrmOrder.BringToFront();
                            }
                            
                        }

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
        private void tlp_category_MouseHover(object sender, EventArgs e)
        {
            TableLayoutPanel tlpcate = sender as TableLayoutPanel;    
            string tlpName = "tlp_category_" + tlpcate.Tag;
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
            if (Menuhome.Width > 150) //Collapse menu
            {
                flp_menu.Dock = DockStyle.None;
                flp_menu.Width = 108;
                panelMenu.Width = 108;
                panelMenu.Dock = DockStyle.Fill;
                flp_menu.Dock = DockStyle.Fill;
                pnl_header.Width = 95;
                pb_appicon.Width = 30;
                RoundPictureBoxCorners(pb_appicon, 10);
                foreach (Button menuButton in flp_menu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.Width = 80;
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(20, 0, 0, 0);
                }
                tlp_main_sidebar_pnldesktop.ColumnStyles[1] = new ColumnStyle(SizeType.AutoSize);
                tlp_main_sidebar_pnldesktop.ColumnStyles[2] = new ColumnStyle(SizeType.AutoSize);
            }
            else
            { //Expand menu
                flp_menu.Dock = DockStyle.Fill;
                pnl_header.Width = 179;
                panelMenu.Width = 180;
                pb_appicon.Width = 97;
                RoundPictureBoxCorners(pb_appicon, 10);
                foreach (Button menuButton in flp_menu.Controls.OfType<Button>())
                {
                    menuButton.Width = 162;
                    menuButton.Text = menuButton.Tag.ToString();
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
            }
            TableStaus(0);
        }

        private void pnl_header_Click(object sender, EventArgs e)
        {
            collapsemenu_click = !collapsemenu_click;
            CollapseMenu();
        }

        private void Menuoperation_Click(object sender, EventArgs e)
        {
            panelDesktop.Controls.Clear();
            if (sender is Button button)
            {
                ChangeButtonBorder(button);
            }
            panelDesktop.Controls.Add(flp_operations);
            flp_operations.Dock = DockStyle.Fill;
            flp_operations.Visible = true;
        } 
       

        private void Menubilling_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                ChangeButtonBorder(button);
            }
            TableStaus(0);
        }

        private void Menureport_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                ChangeButtonBorder(button);
            }
        }

        private void Menusetting_Click(object sender, EventArgs e)
        {
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
            Button button_type = new Button();
            button_type.Text = "Appcolor";
            button_type.Name = "Appcolor";
            //button_type.BackgroundImage = new Bitmap(Application.StartupPath + "\\Background_Images\\bg4.png");
            //button_type.BackgroundImageLayout = ImageLayout.Stretch;
            button_type.FlatStyle = FlatStyle.Flat;
            button_type.BackColor = menubuttoncolor;
            button_type.ForeColor = System.Drawing.Color.White;
            button_type.Font = new Font("Georgia", 9);
            button_type.Width = 100;
            button_type.Height = 70;
            button_type.Margin = new Padding(20, 10, 0, 0);
            // Add the event handler for the button
            button_type.Click += new EventHandler(BgClickEventHandler);
            flpcolor.Controls.Add(button_type);
            columnIndex++;
            panelDesktop.Controls.Add(flpcolor);
        }
        private void BgClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                pnlappcolor.Visible = true;
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
            lblTime.Text = DateTime.Now.ToLongTimeString();
            datelbl.Text = DateTime.Now.ToShortDateString();
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
            int colorcode = Convert.ToInt32(txtcolorcode.Text);
            if (chkentercolor.Checked)
                tlp_main_sidebar_pnldesktop.BackColor = Color.FromArgb(colorcode);
            pnlappcolor.Visible = false;
        }

        private void pbappcoloepicker_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color selectedColor = colorDialog1.Color;
                tlp_main_sidebar_pnldesktop.BackColor = selectedColor;
                pbappcoloepicker.BackColor = selectedColor;
            }
        }

        private void btn_avial_Click(object sender, EventArgs e)
        {
            TableStaus(3);
        }

        private void btn_rev_Click(object sender, EventArgs e)
        {
            TableStaus(2);
        }

        private void btn_running_Click(object sender, EventArgs e)
        {
            TableStaus(1);
        }

        private void btntrefresh_Click(object sender, EventArgs e)
        {
            TableStaus(0);
        }

        private void ib_Category_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["FrmFTICategory"] != null)
            {
                FrmFTICategory _instanceFrmFTICategory = Application.OpenForms["FrmFTICategory"] as FrmFTICategory;
                _instanceFrmFTICategory.Dock = DockStyle.None;
                _instanceFrmFTICategory.Margin = new Padding(10);
                if (_instanceFrmFTICategory.WindowState == FormWindowState.Minimized)
                    _instanceFrmFTICategory.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                else
                {
                    _instanceFrmFTICategory.Dock = DockStyle.None;
                    _instanceFrmFTICategory.Margin = new Padding(20); // Adjust these values as needed
                    panelDesktop.Controls.Add(_instanceFrmFTICategory);
                    _instanceFrmFTICategory.Show();
                    _instanceFrmFTICategory.BringToFront();
                }
            }
            else
            {
                FrmFTICategory _instanceFrmFTICategory = new FrmFTICategory();
                _instanceFrmFTICategory.Dock = DockStyle.None;
                _instanceFrmFTICategory.Margin = new Padding(20); // Adjust these values as needed
                _instanceFrmFTICategory.TopLevel = false;

                panelDesktop.Controls.Add(_instanceFrmFTICategory);
                _instanceFrmFTICategory.Show();
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
                    _instanceItemRegistartion.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed
                    panelDesktop.Controls.Add(_instanceItemRegistartion);
                    _instanceItemRegistartion.Show();
                    _instanceItemRegistartion.BringToFront();
                }
            }
            else
            {
                FrmItemRegistartion _instanceItemRegistartion = new FrmItemRegistartion();
                _instanceItemRegistartion.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed

                _instanceItemRegistartion.TopLevel = false;
                _instanceItemRegistartion.Dock = DockStyle.None;
                panelDesktop.Controls.Add(_instanceItemRegistartion);
                _instanceItemRegistartion.Show();
                _instanceItemRegistartion.BringToFront();
            }

        }

        private void ib_ingrediant_Click(object sender, EventArgs e)
        {

            if (System.Windows.Forms.Application.OpenForms["FrmIngredient"] != null)
            {
                FrmIngredient _instanceFrmIngredient = Application.OpenForms["FrmIngredient"] as FrmIngredient;
                if (_instanceFrmIngredient.WindowState == FormWindowState.Minimized)
                    _instanceFrmIngredient.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                else
                {
                    _instanceFrmIngredient.Dock = DockStyle.None;
                    _instanceFrmIngredient.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed
                    panelDesktop.Controls.Add(_instanceFrmIngredient);
                    _instanceFrmIngredient.Show();
                    _instanceFrmIngredient.BringToFront();
                }
            }
            else
            {
                FrmIngredient _instanceFrmIngredient = new FrmIngredient();
                _instanceFrmIngredient.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed

                _instanceFrmIngredient.TopLevel = false;
                _instanceFrmIngredient.Dock = DockStyle.None;
                panelDesktop.Controls.Add(_instanceFrmIngredient);
                _instanceFrmIngredient.Show();
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
                    _instanceFrmMenu.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed
                    panelDesktop.Controls.Add(_instanceFrmMenu);
                    _instanceFrmMenu.Show();
                    _instanceFrmMenu.BringToFront();
                }
            }
            else
            {
                FrmMenu _instanceFrmMenu = new FrmMenu();
                _instanceFrmMenu.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed

                _instanceFrmMenu.TopLevel = false;
                _instanceFrmMenu.Dock = DockStyle.None;
                panelDesktop.Controls.Add(_instanceFrmMenu);
                _instanceFrmMenu.Show();
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
                    _instanceFrmUnit.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed
                    panelDesktop.Controls.Add(_instanceFrmUnit);
                    _instanceFrmUnit.Show();
                    _instanceFrmUnit.BringToFront();
                }
            }
            else
            {
                FrmUnit _instanceFrmUnit = new FrmUnit();
                _instanceFrmUnit.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed

                _instanceFrmUnit.TopLevel = false;
                _instanceFrmUnit.Dock = DockStyle.None;
                panelDesktop.Controls.Add(_instanceFrmUnit);
                _instanceFrmUnit.Show();
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
                    _instanceTableInfo.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed
                    panelDesktop.Controls.Add(_instanceTableInfo);
                    _instanceTableInfo.Show();
                    _instanceTableInfo.BringToFront();
                }
            }
            else
            {
                FrmTableInfo _instanceTableInfo = new FrmTableInfo();
                _instanceTableInfo.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed

                _instanceTableInfo.TopLevel = false;
                _instanceTableInfo.Dock = DockStyle.None;
                panelDesktop.Controls.Add(_instanceTableInfo);
                _instanceTableInfo.Show();
                _instanceTableInfo.BringToFront();
            }
        }

        private void ib_purchase_Click(object sender, EventArgs e)
        {

            if (System.Windows.Forms.Application.OpenForms["FrmPurchase"] != null)
            {
                FrmPurchase _instanceFrmPurchase = Application.OpenForms["FrmPurchase"] as FrmPurchase;
                _instanceFrmPurchase.Dock = DockStyle.None;
                _instanceFrmPurchase.Margin = new Padding(10);
                if (_instanceFrmPurchase.WindowState == FormWindowState.Minimized)
                    _instanceFrmPurchase.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                else
                {
                    _instanceFrmPurchase.Dock = DockStyle.None;
                    _instanceFrmPurchase.Margin = new Padding(20); // Adjust these values as needed
                    panelDesktop.Controls.Add(_instanceFrmPurchase);
                    _instanceFrmPurchase.Show();
                    _instanceFrmPurchase.BringToFront();
                }
            }
            else
            {
                FrmPurchase _instanceFrmPurchase = new FrmPurchase();
                _instanceFrmPurchase.Dock = DockStyle.None;
                _instanceFrmPurchase.Margin = new Padding(20); // Adjust these values as needed
                _instanceFrmPurchase.TopLevel = false;

                panelDesktop.Controls.Add(_instanceFrmPurchase);
                _instanceFrmPurchase.Show();
                _instanceFrmPurchase.BringToFront();
            }



        }

        private void ib_purchasereturn_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["FrmPurchaseReturn"] != null)
            {
                FrmPurchaseReturn _instancePurchaseReturn = Application.OpenForms["FrmPurchaseReturn"] as FrmPurchaseReturn;
                _instancePurchaseReturn.Dock = DockStyle.None;
                _instancePurchaseReturn.Margin = new Padding(10);
                if (_instancePurchaseReturn.WindowState == FormWindowState.Minimized)
                    _instancePurchaseReturn.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                else
                {
                    _instancePurchaseReturn.Dock = DockStyle.None;
                    _instancePurchaseReturn.Margin = new Padding(20); // Adjust these values as needed
                    panelDesktop.Controls.Add(_instancePurchaseReturn);
                    _instancePurchaseReturn.Show();
                    _instancePurchaseReturn.BringToFront();
                }
            }
            else
            {
                FrmPurchaseReturn _instancePurchaseReturn = new FrmPurchaseReturn();
                _instancePurchaseReturn.Dock = DockStyle.None;
                _instancePurchaseReturn.Margin = new Padding(20); // Adjust these values as needed
                _instancePurchaseReturn.TopLevel = false;

                panelDesktop.Controls.Add(_instancePurchaseReturn);
                _instancePurchaseReturn.Show();
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
                    _instanceTaxSetting.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed
                    panelDesktop.Controls.Add(_instanceTaxSetting);
                    _instanceTaxSetting.Show();
                    _instanceTaxSetting.BringToFront();
                }
            }
            else
            {
                FrmTaxSetting _instanceTaxSetting = new FrmTaxSetting();
                _instanceTaxSetting.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed

                _instanceTaxSetting.TopLevel = false;
                _instanceTaxSetting.Dock = DockStyle.None;
                panelDesktop.Controls.Add(_instanceTaxSetting);
                _instanceTaxSetting.Show();
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
                }

                if (originalImage != null)
                {
                    Image resizedImage = new Bitmap(originalImage, new Size(HdesiredWidth, HdesiredHeight));
                    button.BackgroundImage = resizedImage;
                    button.BackgroundImageLayout = ImageLayout.Center;
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
                    _instanceFrmVendor.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed
                    panelDesktop.Controls.Add(_instanceFrmVendor);
                    _instanceFrmVendor.Show();
                    _instanceFrmVendor.BringToFront();
                }
            }
            else
            {
                FrmVendor _instanceFrmVendor = new FrmVendor();
                _instanceFrmVendor.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed

                _instanceFrmVendor.TopLevel = false;
                _instanceFrmVendor.Dock = DockStyle.None;
                panelDesktop.Controls.Add(_instanceFrmVendor);
                _instanceFrmVendor.Show();
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
                    if(pnl_tableorder.Visible)
                    {
                        Control controlToRemove = tlp_main.GetControlFromPosition(0, 1);
                        tlp_main.Controls.Remove(controlToRemove);
                        tlp_main.Controls.Add(tlp_main_sidebar_pnldesktop);
                        tlp_main_sidebar_pnldesktop.Dock = DockStyle.Fill;
                        tlp_main_sidebar_pnldesktop.Visible = true;
                    }
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void ib_Discount_MouseLeave(object sender, EventArgs e)
        {
            ib_Discount_offer.Text = Convert.ToString(ib_Discount_offer.Tag);
            SetButtonImage(ib_Discount_offer, FoodandBeverage.Properties.Resources.discount, desiredWidth, desiredHeight);
        }
        private void ib_withdrawal_MouseLeave(object sender, EventArgs e)
        {
            ib_withdrawal.Text = Convert.ToString(ib_withdrawal.Tag);
            SetButtonImage(ib_withdrawal, FoodandBeverage.Properties.Resources.withdraw, desiredWidth, desiredHeight);
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
                    _instanceFrmOfferDiscountr.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed
                    panelDesktop.Controls.Add(_instanceFrmOfferDiscountr);
                    _instanceFrmOfferDiscountr.Show();
                    _instanceFrmOfferDiscountr.BringToFront();
                }
            }
            else
            {
                FrmOfferDiscount _instanceFrmOfferDiscountr = new FrmOfferDiscount();
                _instanceFrmOfferDiscountr.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed

                _instanceFrmOfferDiscountr.TopLevel = false;
                _instanceFrmOfferDiscountr.Dock = DockStyle.None;
                panelDesktop.Controls.Add(_instanceFrmOfferDiscountr);
                _instanceFrmOfferDiscountr.Show();
                _instanceFrmOfferDiscountr.BringToFront();
            }
        }

        private void ib_withdrawal_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["FrmVendor"] != null)
            {
                FrmVendor _instanceFrmVendor = Application.OpenForms["FrmVendor"] as FrmVendor;
                if (_instanceFrmVendor.WindowState == FormWindowState.Minimized)
                    _instanceFrmVendor.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                else
                {
                    _instanceFrmVendor.Dock = DockStyle.None;
                    _instanceFrmVendor.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed
                    panelDesktop.Controls.Add(_instanceFrmVendor);
                    _instanceFrmVendor.Show();
                    _instanceFrmVendor.BringToFront();
                }
            }
            else
            {
                FrmVendor _instanceFrmVendor = new FrmVendor();
                _instanceFrmVendor.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed

                _instanceFrmVendor.TopLevel = false;
                _instanceFrmVendor.Dock = DockStyle.None;
                panelDesktop.Controls.Add(_instanceFrmVendor);
                _instanceFrmVendor.Show();
                _instanceFrmVendor.BringToFront();
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
    }
}
