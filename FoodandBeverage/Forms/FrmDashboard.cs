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
        // Create a new Font with the specified properties
        Font ZoominFont = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
        Font ZoomoutFont = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
        private bool isCollapsed = false; // Initialize a flag to track the current size
        private int isbridge = 1; 
        private void rb_home_MouseEnter(object sender, EventArgs e)
        {       
            lbl_home.Font = ZoominFont;
            CenterLabel(1);
            rb_home.BackColor = ColorTranslator.FromHtml("#CC6666");
            lbl_home.BackColor = ColorTranslator.FromHtml("#CC6666");
            pbhome.BackColor = ColorTranslator.FromHtml("#CC6666");
        }

        private void rb_home_MouseLeave(object sender, EventArgs e)
        {
            lbl_home.Font = ZoomoutFont;
            CenterLabel(1);
            rb_home.BackColor = Color.Red;
            lbl_home.BackColor = Color.Red;
            pbhome.BackColor = Color.Red;
        }
        private void CenterLabel(int n)
        {           
            switch (n)
            {
                case 1:
                    lbl_home.Left = (rb_home.Width - lbl_home.Width) / 2;
                    lbl_home.Top = (rb_home.Height - lbl_home.Height) / 2;
                    lbl_home.Top = lbl_home.Top + 30;
                    break;
                case 2:
                    lbloper.Left = (rboperation.Width - lbloper.Width) / 2;
                    lbloper.Top = (rboperation.Height - lbloper.Height) / 2;
                    lbloper.Top = lbloper.Top + 88;
                    lbloper.Left = lbloper.Left + 5;
                    break;
                case 3:
                    lblreport.Left = (rbreport.Width - lblreport.Width) / 2;
                    lblreport.Top = (rbreport.Height - lblreport.Height) / 2;
                    lblreport.Top = lblreport.Top + 149;
                    break;
                case 4:
                    lblSettings.Left = (rbsettings.Width - lblSettings.Width) / 2;
                    lblSettings.Top = (rbsettings.Height - lblSettings.Height) / 2;
                    lblSettings.Top = lblSettings.Top + 217;
                    break;
                case 5:
                    lblBilling.Left = (rbbilling.Width - lblBilling.Width) / 2;
                    lblBilling.Top = (rbbilling.Height - lblBilling.Height) / 2;
                    lblBilling.Top = lblBilling.Top + 278;
                    break;
                case 6:
                    lbllogout.Left = (rblogout.Width - lbllogout.Width) / 2;
                    lbllogout.Top = (rblogout.Height - lbllogout.Height) / 2;
                    lbllogout.Top = lbllogout.Top + 343;
                    break;
                // Add more cases as needed for other controls
                default:
                    break;
            }
        }


        private void rboperation_MouseEnter(object sender, EventArgs e)
        {
            lbloper.Font = ZoominFont;
            CenterLabel(2);
            rboperation.BackColor = ColorTranslator.FromHtml("#CC6666");
            lbloper.BackColor = ColorTranslator.FromHtml("#CC6666");
            pb_opera.BackColor = ColorTranslator.FromHtml("#CC6666");
        }

        private void rboperation_MouseLeave(object sender, EventArgs e)
        {
            lbloper.Font = ZoomoutFont;
            CenterLabel(2);
            rboperation.BackColor = Color.Red;
            lbloper.BackColor = Color.Red;
            pb_opera.BackColor = Color.Red;
        }

        private void rboperation_Click(object sender, EventArgs e)
        {
            int newX = rboperation.Right + 30; // Adjust this value according to your desired X coordinate
            int newY = rboperation.Bottom + 7; // Set newY to rboperation's Top position
            pb_connection.Location = new Point(newX, newY);
            isbridge = 2;
        }

        private void rb_home_Click(object sender, EventArgs e)
        {
            int newX = rb_home.Right + 30; // Adjust this value according to your desired X coordinate
            int newY = rb_home.Bottom + 7; // Set newY to rboperation's Top position
            pb_connection.Location = new Point(newX, newY);
            isbridge = 1;
        }

        private void rbreport_MouseEnter(object sender, EventArgs e)
        {
            lblreport.Font = ZoominFont;
            CenterLabel(3);
            rbreport.BackColor = ColorTranslator.FromHtml("#CC6666");
            lblreport.BackColor = ColorTranslator.FromHtml("#CC6666");
            pb_report.BackColor = ColorTranslator.FromHtml("#CC6666");
        }

        private void rbreport_MouseLeave(object sender, EventArgs e)
        {
            lblreport.Font = ZoomoutFont;
            CenterLabel(3);
            rbreport.BackColor = Color.Red;
            lblreport.BackColor = Color.Red;
            pb_report.BackColor = Color.Red;

        }

        private void rbsettings_MouseEnter(object sender, EventArgs e)
        {
            lblSettings.Font = ZoominFont;
            CenterLabel(4);
            rbsettings.BackColor = ColorTranslator.FromHtml("#CC6666");
            lblSettings.BackColor = ColorTranslator.FromHtml("#CC6666");
            pb_setting.BackColor = ColorTranslator.FromHtml("#CC6666");
        }

        private void rbsettings_MouseLeave(object sender, EventArgs e)
        {
            lblSettings.Font = ZoomoutFont;
            CenterLabel(4);
            rbsettings.BackColor = Color.Red;
            lblSettings.BackColor = Color.Red;
            pb_setting.BackColor = Color.Red;
        }

        private void rbbilling_MouseEnter(object sender, EventArgs e)
        {
            lblBilling.Font = ZoominFont;
            CenterLabel(5);
            rbbilling.BackColor = ColorTranslator.FromHtml("#CC6666");
            lblBilling.BackColor = ColorTranslator.FromHtml("#CC6666");
            pb_billing.BackColor = ColorTranslator.FromHtml("#CC6666");
        }

        private void rbbilling_MouseLeave(object sender, EventArgs e)
        {
            lblBilling.Font = ZoomoutFont;
            CenterLabel(5);
            rbbilling.BackColor = Color.Red;
            lblBilling.BackColor = Color.Red;
            pb_billing.BackColor = Color.Red;
        }

        private void rblogout_MouseEnter(object sender, EventArgs e)
        {
            lbllogout.Font = ZoominFont;
            CenterLabel(6);
            rblogout.BackColor = ColorTranslator.FromHtml("#CC6666");
            lbllogout.BackColor = ColorTranslator.FromHtml("#CC6666");
            pb_logout.BackColor = ColorTranslator.FromHtml("#CC6666");
        }

        private void rblogout_MouseLeave(object sender, EventArgs e)
        {
            lbllogout.Font = ZoomoutFont;
            CenterLabel(6);
            rblogout.BackColor = Color.Red;
            lbllogout.BackColor = Color.Red;
            pb_logout.BackColor = Color.Red;
        }

        private void rbreport_Click(object sender, EventArgs e)
        {
            int newX = rbreport.Right + 30; // Adjust this value according to your desired X coordinate
            int newY = rbreport.Bottom + 7; // Set newY to rboperation's Top position
            pb_connection.Location = new Point(newX, newY);
            isbridge = 3;
        }

        private void rbsettings_Click(object sender, EventArgs e)
        {
            int newX = rbsettings.Right + 30; // Adjust this value according to your desired X coordinate
            int newY = rbsettings.Bottom + 7; // Set newY to rboperation's Top position
            pb_connection.Location = new Point(newX, newY);
            isbridge = 4;
        }

        private void rbbilling_Click(object sender, EventArgs e)
        {
            int newX = rbbilling.Right + 30; // Adjust this value according to your desired X coordinate
            int newY = rbbilling.Bottom + 7; // Set newY to rboperation's Top position
            pb_connection.Location = new Point(newX, newY);
            isbridge = 5;
        }

        private void rblogout_Click(object sender, EventArgs e)
        {
            int newX = rblogout.Right + 30; // Adjust this value according to your desired X coordinate
            int newY = rblogout.Bottom + 7; // Set newY to rboperation's Top position
            pb_connection.Location = new Point(newX, newY);
            isbridge = 6;
        }
        public void Bridge()
        {
            int newX=0, newY=0;
            switch (isbridge)
            {
                case 1:
                    newX = rb_home.Right + 30; // Adjust this value according to your desired X coordinate
                    newY = rb_home.Bottom + 7; // Set newY to rboperation's Top position
                    pb_connection.Location = new Point(newX, newY);

                    break;
                case 2:
                    newX = rboperation.Right + 30; // Adjust this value according to your desired X coordinate
                    newY = rboperation.Bottom + 7; // Set newY to rboperation's Top position
                    pb_connection.Location = new Point(newX, newY);

                    break;
                case 3:
                    newX = rbreport.Right + 30; // Adjust this value according to your desired X coordinate
                    newY = rbreport.Bottom + 7; // Set newY to rboperation's Top position
                    pb_connection.Location = new Point(newX, newY);

                    break;
                case 4:
                     newX = rbsettings.Right + 30; // Adjust this value according to your desired X coordinate
                     newY = rbsettings.Bottom + 7; // Set newY to rboperation's Top position
                    pb_connection.Location = new Point(newX, newY);

                    break;
                case 5:
                     newX = rbbilling.Right + 30; // Adjust this value according to your desired X coordinate
                     newY = rbbilling.Bottom + 7; // Set newY to rboperation's Top position
                    pb_connection.Location = new Point(newX, newY);
                    break;
                case 6:
                     newX = rblogout.Right + 30; // Adjust this value according to your desired X coordinate
                     newY = rblogout.Bottom + 7; // Set newY to rboperation's Top position
                    pb_connection.Location = new Point(newX, newY);     
                    break;
            }
        }


        private void rb_colap_Click(object sender, EventArgs e)
        {
        
            if (isCollapsed)
            {
                lbl_home.Visible = true;
                lbloper.Visible = true;
                lblreport.Visible = true;
                lblSettings.Visible = true;
                lblBilling.Visible = true;
                lbllogout.Visible = true;

                int newX = (161 - rb_colap.Width) / 2; ;
                int newY = 5; 
                rb_colap.Location = new Point(newX, newY);
                //rb_colap.Left = (rp_sidebar.Width - rb_colap.Width) / 2;
                // Expand the controls to size 150
                rp_sidebar.Width = 161;
                rb_home.Width = 150;
                rboperation.Width = 150;
                rbreport.Width = 150;
                rbsettings.Width = 150;
                rbbilling.Width = 150;
                rblogout.Width = 150;
            }
            else
            {
                lbl_home.Visible = false;
                lbloper.Visible = false;
                lblreport.Visible = false;
                lblSettings.Visible = false;
                lblBilling.Visible = false;
                lbllogout.Visible = false;

                int newX = rb_home.Left;
                int newY = 5;
                rb_colap.Location = new Point(newX, newY);
                rp_sidebar.Width = 60;
                // Collapse the controls to size 50
                rb_home.Width = 50;
                rboperation.Width = 50;
                rbreport.Width = 50;
                rbsettings.Width = 50;
                rbbilling.Width = 50;
                rblogout.Width = 50;
            }

            // Toggle the flag
            isCollapsed = !isCollapsed;
            Bridge();
            CenterLabel_ALL();
        }
        private void CenterLabel_ALL()
        {

            lbl_home.Left = (rb_home.Width - lbl_home.Width) / 2;
            lbl_home.Top = (rb_home.Height - lbl_home.Height) / 2;
            lbl_home.Top = lbl_home.Top + 30;

            lbloper.Left = (rboperation.Width - lbloper.Width) / 2;
            lbloper.Top = (rboperation.Height - lbloper.Height) / 2;
            lbloper.Top = lbloper.Top + 88;
            lbloper.Left = lbloper.Left + 5;

            lblreport.Left = (rbreport.Width - lblreport.Width) / 2;
            lblreport.Top = (rbreport.Height - lblreport.Height) / 2;
            lblreport.Top = lblreport.Top + 149;

            lblSettings.Left = (rbsettings.Width - lblSettings.Width) / 2;
            lblSettings.Top = (rbsettings.Height - lblSettings.Height) / 2;
            lblSettings.Top = lblSettings.Top + 217;

            lblBilling.Left = (rbbilling.Width - lblBilling.Width) / 2;
            lblBilling.Top = (rbbilling.Height - lblBilling.Height) / 2;
            lblBilling.Top = lblBilling.Top + 278;

            lbllogout.Left = (rblogout.Width - lbllogout.Width) / 2;
            lbllogout.Top = (rblogout.Height - lbllogout.Height) / 2;
            lbllogout.Top = lbllogout.Top + 343;


        }

        private void FrmDashboard_Resize(object sender, EventArgs e)
        {
            Bridge();
        }
    }
}
