using BLL;
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
        FlowLayoutPanel flpcolor = new FlowLayoutPanel();
        private Point MouseLocation;
        #endregion      

        private void Menulogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            pbappcoloepicker.BackColor = Color.FromArgb(34, 46, 80);
            Roundform(30);
            // tlp_main.BackColor = Color.FromArgb(152, 193, 217);
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
            panelDesktop.Controls.Clear();
        }

        private void CollapseMenu()
        {
            if (this.panelMenu.Width > 150) //Collapse menu
            {
                panelMenu.Width = 100;

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
                Control control;
                int columnIndex = 0;
                for (int i = 0; i < gridSource.Count; i++)
                {
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
                    button_type.Margin = new Padding(20, 10, 0, 0);
                    // Add the event handler for the button
                    button_type.Click += new EventHandler(MyButtonClickEventHandler);
                    tlp_button.Controls.Add(button_type, columnIndex, rowIndex);
                    columnIndex++;

                }
                panelDesktop.Controls.Add(tlp_button);
            }
                     
        }
        
              
        private void MyButtonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = (Button)sender;
                string bname = clickedButton.Name;
                string ptypeno = Convert.ToString(clickedButton.Tag);
                string commandName = ptypeno;
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

  
    }
}
