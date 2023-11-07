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
    public partial class FrmOfferDiscount : Form
    {
        public FrmOfferDiscount()
        {
            InitializeComponent();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsingle_Click(object sender, EventArgs e)
        {
            Control controlToRemove = tlp_main.GetControlFromPosition(0, 2);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_single.Visible = true;
            tlp_main.Controls.Add(tlp_single);
            tlp_single.Dock = DockStyle.Fill;
        }

        private void btn_combo_Click(object sender, EventArgs e)
        {
            tlp_single.Visible = false;
            Control controlToRemove = tlp_main.GetControlFromPosition(0, 2);
            tlp_main.Controls.Remove(controlToRemove);
        }

        private void FrmOfferDiscount_Load(object sender, EventArgs e)
        {
            tlp_single.Visible = true;
            tlp_main.Controls.Add(tlp_single);
            tlp_single.Dock = DockStyle.Fill;
        }
    }
}
