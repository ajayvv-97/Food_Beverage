using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private bool isClicked = false;
        Color menubuttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        private void Menulogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
  
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

        }

        private void CollapseMenu()
        {
            if (this.panelMenu.Width > 150) //Collapse menu
            {
                panelMenu.Width = 100;

                foreach (Button menuButton in flp_menu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.Width = 90;
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(28,0,0,0);
                }
            }
            else
            { //Expand menu
                panelMenu.Width = 180;
                foreach (Button menuButton in flp_menu.Controls.OfType<Button>())
                {
                    menuButton.Width = 179;
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
        }
    }
}
