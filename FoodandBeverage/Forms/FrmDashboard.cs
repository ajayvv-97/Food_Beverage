using BLL;
using CustomMessageBox;
using Domain;
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
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
        }
        #region Declaration
        private bool isClicked = false;
        Color menubuttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        TableLayoutPanel tlp_button = new TableLayoutPanel();
        TableLayoutPanel tlp_table_dashboard_main = new TableLayoutPanel();
        TableLayoutPanel tlp_table_dashboard = new TableLayoutPanel();
        private Color originalBackColor; // Variable to store the original background color
        FlowLayoutPanel flpcolor = new FlowLayoutPanel();
        private Point MouseLocation;
        #endregion      

        private void Menulogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            RoundPictureBoxCorners(pb_appicon, 10);
            TableStaus(0);
            pbappcoloepicker.BackColor = Color.FromArgb(34, 46, 80);
            Roundform(30);
            // tlp_main.BackColor = Color.FromArgb(152, 193, 217);
            //set color on solidebar header
            pnl_header.BackColor = menubuttoncolor;

            tlp_main.BackColor = Color.FromArgb(34, 46, 80);
            this.FormBorderStyle = FormBorderStyle.None;
            timer1.Start();
            Menuhome.BackColor = menubuttoncolor;
            Menuoperation.BackColor = menubuttoncolor;
            Menubilling.BackColor = menubuttoncolor;
            Menureport.BackColor = menubuttoncolor;
            Menulogout.BackColor = menubuttoncolor;
            Menusetting.BackColor = menubuttoncolor;

            Image originalImage = FoodandBeverage.Properties.Resources.icons8_home_50;
            int desiredWidth = 32; 
            int desiredHeight = 32; 
            Image resizedImage = new Bitmap(originalImage, new Size(desiredWidth, desiredHeight));
            Menuhome.Image = resizedImage;
            flp_menu.BackColor = Color.White;
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
        public void TableStaus(int serachmode)
        {
            
            BllSearch<DmnTableinfo> bllSearch = new BllSearch<DmnTableinfo>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = "*";
            dmnSearch.Table = "tblTable_info";

            switch (serachmode)
            {
                case 0:
                    dmnSearch.WhereNdGroupBy = "where Status=1";
                    break;
                case 1:
                    dmnSearch.WhereNdGroupBy = "where Status=1 and OrderStatus=1";
                    break;
                case 2:
                    dmnSearch.WhereNdGroupBy = "where Status=1 and RevStatus=1";
                    break;
                case 3:
                    dmnSearch.WhereNdGroupBy = "where Status=1 and OrderStatus=0 and RevStatus=0";
                    break;

            }
        
            dmnSearch.OrderBy = "";
            var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
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
                int rowIndex = 1;
                int columnIndex = 0;
                for (int i = 0; i < gridSource.Count; i++)
                {
                    if (columnIndex == 8)
                    {
                        columnIndex = 0;
                        rowIndex++;
                    }
                    TableLayoutPanel tlp_table_inner = new TableLayoutPanel();
                    PictureBox pictureBox = new PictureBox();
                    Label lbltablename = new Label();

                    // Configure the PictureBox
                    pictureBox.Name = gridSource[i].TableCode;
                    pictureBox.Image = FoodandBeverage.Properties.Resources.restaurant;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.BackColor = menubuttoncolor;
                    pictureBox.Width = 60;
                    pictureBox.Height = 80;

                    // Configure the Label
                    lbltablename.Name = gridSource[i].TableCode;
                    lbltablename.Text = gridSource[i].TableCode;
                    lbltablename.ForeColor = Color.Black;
                    lbltablename.BackColor = Color.Transparent;
                    lbltablename.Dock = DockStyle.Top;
                    lbltablename.Width = 60;
                    //lbltablename.Margin = new Padding(10, 10, 0, 0);
                    lbltablename.Font = new Font("Arial", 10, FontStyle.Bold);

                    // Add the PictureBox and Label to the TableLayoutPanel
                    tlp_table_inner.Controls.Add(pictureBox, 0, 0); // PictureBox in the first row
                    tlp_table_inner.Controls.Add(lbltablename, 0, 1); // Label in the second row

                    // Configure the TableLayoutPanel
                    tlp_table_inner.RowCount = 2;
                    //tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
                    //tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                    if (gridSource[i].OrderStatus == 1)
                        lbltablename.BackColor = Color.Red;
                    else if (gridSource[i].RevStatus == 1)
                        lbltablename.BackColor = Color.Yellow;
                    else
                        lbltablename.BackColor = Color.Green;
                    tlp_table_inner.Name= gridSource[i].TableCode; 
                    //tlp_table_inner.BorderStyle = BorderStyle.FixedSingle;
                    tlp_table_inner.Dock = DockStyle.None;
                    tlp_table_inner.Margin = new Padding(20, 10, 20, 10);
                    tlp_table_inner.Width = 70;
                    tlp_table_inner.Height = 120;

                    pictureBox.MouseEnter += TlpTableInner_MouseEnter;
                    lbltablename.MouseEnter += TlpTableInner_MouseEnter;

                    pictureBox.MouseLeave += TlpTableInner_MouseLeave;
                    lbltablename.MouseLeave += TlpTableInner_MouseLeave;

                    tlp_table_inner.MouseEnter += TlpTableInner_MouseEnter;
                    tlp_table_inner.MouseLeave += TlpTableInner_MouseLeave;

                    pictureBox.Click += new EventHandler(tableClickEventHandler);
                    lbltablename.Click += new EventHandler(tableClickEventHandler);
                    tlp_table_inner.Click += new EventHandler(tableClickEventHandler);

                    tlp_table_dashboard.Controls.Add(tlp_table_inner, columnIndex, rowIndex);
                    columnIndex++;
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
                    // Now, you have the ID (tableCode) associated with the clicked control.
                    // You can use it as needed.
                    CMessageBox.Show("Clicked Table ID: " + tableCode);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void TlpTableInner_MouseEnter(object sender, EventArgs e)
        {
            TableLayoutPanel tlp = sender as TableLayoutPanel;
            if (tlp != null)
            {
                foreach (Control control in tlp.Controls)
                {
                    if (control is Label)
                    {
                        Label lbl = control as Label;
                        originalBackColor = lbl.BackColor;
                        lbl.BackColor = Color.LightGray;
                    }
                }
                
            }
        }

        private void TlpTableInner_MouseLeave(object sender, EventArgs e)
        {
            TableLayoutPanel tlp = sender as TableLayoutPanel;
            if (tlp != null)
            {
                foreach (Control control in tlp.Controls)
                {
                    if (control is Label)
                    {
                        Label lbl = control as Label;
                        lbl.BackColor = originalBackColor;
                    }
                }
              
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
        }

        private void CollapseMenu()
        {
            if (this.panelMenu.Width > 150) //Collapse menu
            {
    
                panelMenu.Width = 100;
                pb_appicon.Width = 30;
                RoundPictureBoxCorners(pb_appicon, 10);
                foreach (Button menuButton in flp_menu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.Width = 80;
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(20,0,0,0);
                }
            }
            else
            { //Expand menu
               
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
        }

        private void pnl_header_Click(object sender, EventArgs e)
        {
            CollapseMenu();
        }

        private void Menuoperation_Click(object sender, EventArgs e)
        {
            panelDesktop.Controls.Clear();
            if (sender is Button button)
            {
                ChangeButtonBorder(button);
            }

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if ((Application.OpenForms[i].Name != "FrmDashboard") && (Application.OpenForms[i].Name != "frmLogin"))
                {
                    Application.OpenForms[i].MinimumSize = new Size(10, 20); // Set the minimum size here
                }
            }
            //if (flagclear)
            //{
            //    flagclear = false;
            //    panelDesktop.Controls.Clear();
            //}
            tlp_button.Controls.Clear();
            tlp_button.Dock = DockStyle.Fill;
            tlp_button.AutoScroll = true;

            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = "Caption as Field1,GTypeSubId as Field2";
            dmnSearch.Table = "tblGeneralOptions";
            dmnSearch.WhereNdGroupBy = "where GTypeId=66";
            dmnSearch.OrderBy = "";
            var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
            int rowIndex = 0;
            if (gridSource.Count > 1)
            {
                int columnIndex = 0;
                for (int i = 0; i < gridSource.Count; i++)
                {
                    if (columnIndex == 7)
                    {
                        columnIndex = 0;
                        rowIndex++;
                    }
                    Button button_type = new Button();
                    button_type.Text = gridSource[i].Field1;
                    button_type.Name = gridSource[i].Field1;
                    button_type.Tag = gridSource[i].Field2;
                    //button_type.BackgroundImage = new Bitmap(Application.StartupPath + "\\Background_Images\\bg4.png");
                    //button_type.BackgroundImageLayout = ImageLayout.Stretch;
                    button_type.FlatStyle = FlatStyle.Flat;
                    button_type.BackColor = menubuttoncolor;
                    button_type.ForeColor = System.Drawing.Color.White;
                    button_type.Font = new Font("Georgia", 9);
                    button_type.Width = 100;
                    button_type.Height = 70;
                    // Add the event handler for the button
                    button_type.Click += new EventHandler(MyButtonClickEventHandler);

                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Name = gridSource[i].Field1;
                    pictureBox.Image = FoodandBeverage.Properties.Resources.cate_registration; // Set the image you want to display
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Tag = gridSource[i].Field2;
                    pictureBox.Click += new EventHandler(MyButtonClickEventHandler);
                    pictureBox.Visible = true;            
                    button_type.MouseHover += new EventHandler(MouseEnterEventHandler);          
                    pictureBox.MouseLeave += new EventHandler(MouseLeaveEventHandler);
                    
                    Panel panel = new Panel();
                    panel.Width = 100;
                    panel.Height = 70;
                    panel.Margin = new Padding(30, 30, 0, 0);
                    panel.Controls.Add(button_type);
                    panel.Controls.Add(pictureBox);
                    tlp_button.Controls.Add(panel, columnIndex, rowIndex);

                    columnIndex++;
                }
                panelDesktop.Controls.Add(tlp_button);
            }                     
        }
        private void MouseEnterEventHandler(object sender, EventArgs e)
        {
            try
            {
                Button button_type = sender as Button;
                if (button_type != null)
                {
                    foreach (Control control in button_type.Parent.Controls)
                    {
                        if (control is PictureBox)
                        {
                            control.Visible = true;
                        }
                        if (control is Button)
                        {
                            control.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void MouseLeaveEventHandler(object sender, EventArgs e)
        {
            try
            {
                PictureBox pic = sender as PictureBox;
                if (pic != null)
                {
                    foreach (Control control in pic.Parent.Controls)
                    {
                        if (control is PictureBox)
                        {
                            control.Visible = false;
                        }
                        if (control is Button)
                        {
                            control.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void MyButtonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                string commandName="";
                if (sender is Button clickedButton)
                    commandName = Convert.ToString(clickedButton.Tag);     
                else if (sender is PictureBox clickedPicture)
                    commandName = Convert.ToString(clickedPicture.Tag);
                
                switch (commandName)
                {
                    case "1":
                       
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
                            panelDesktop.Padding= new Padding(20);
                            _instanceFrmFTICategory.Show();
                            _instanceFrmFTICategory.BringToFront();
                        }
                        break;
                    case "2":
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
                        break;
                    case "3":
                        if (System.Windows.Forms.Application.OpenForms["FrmIncrediant"] != null)
                        {
                            FrmIncrediant _instanceFrmIncrediant = Application.OpenForms["FrmIncrediant"] as FrmIncrediant;
                            if (_instanceFrmIncrediant.WindowState == FormWindowState.Minimized)
                                _instanceFrmIncrediant.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                            else
                            {
                                _instanceFrmIncrediant.Dock = DockStyle.None;
                                _instanceFrmIncrediant.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed
                                panelDesktop.Controls.Add(_instanceFrmIncrediant);
                                _instanceFrmIncrediant.Show();
                                _instanceFrmIncrediant.BringToFront();
                            }
                        }
                        else
                        {
                            FrmIncrediant _instanceFrmIncrediant = new FrmIncrediant();
                            _instanceFrmIncrediant.Margin = new Padding(20, 10, 0, 0); // Adjust these values as needed

                            _instanceFrmIncrediant.TopLevel = false;
                            _instanceFrmIncrediant.Dock = DockStyle.None;
                            panelDesktop.Controls.Add(_instanceFrmIncrediant);
                            _instanceFrmIncrediant.Show();
                            _instanceFrmIncrediant.BringToFront();
                        }
                        break;
                    case "4":
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
                        break;
                    case "5":
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
                        break;

                    case "6":
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
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void Menubilling_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                ChangeButtonBorder(button);
            }
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
            this.Close();
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
        public  void Roundform(int size)
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
            if(e.Button==MouseButtons.Left)
            {
                Point mousepos = Control.MousePosition;
                mousepos.Offset(MouseLocation.X, MouseLocation.Y);
                Location = mousepos;
            }
        }

        private void navibar_MouseDown(object sender, MouseEventArgs e)
        {
            MouseLocation = new Point(-e.X,-e.Y);

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
                tlp_main.BackColor = Color.FromArgb(colorcode);
            pnlappcolor.Visible = false;
        }

        private void pbappcoloepicker_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color selectedColor = colorDialog1.Color;
                tlp_main.BackColor = selectedColor;
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
    }
}
