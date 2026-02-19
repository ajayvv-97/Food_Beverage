using FoodandBeverage.Classes;
using FoodandBeverage.Forms;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage
{
    public partial class FrmPartyBooking : Form
    {
        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsgen = new ClsGeneral();
        public FrmPartyBooking()
        {
            InitializeComponent();
        }

        private void FrmPartyBooking_Load(object sender, EventArgs e)
        {

        }


        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnfoodpack_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ TOGGLE (Show / Hide)
                if (pnl_foodpacks.Visible)
                {
                    pnl_foodpacks.Visible = false;
                    return;
                }

                // ✅ Set 80% size of form
                pnl_foodpacks.Width = (int)(this.ClientSize.Width * 0.9);
                pnl_foodpacks.Height = (int)(this.ClientSize.Height * 0.9);

                // ✅ Center panel
                pnl_foodpacks.Left = (this.ClientSize.Width - pnl_foodpacks.Width) / 2;
                pnl_foodpacks.Top = (this.ClientSize.Height - pnl_foodpacks.Height) / 2;

                // ✅ Show + Bring Front
                pnl_foodpacks.Visible = true;
                pnl_foodpacks.BringToFront();
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Partybooking btnfoodpack_Click()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking)
                );
            }
        }



        private void btn_pack_close_Click(object sender, EventArgs e)
        {
            pnl_foodpacks.Visible=false;
        }

        private void btn_orderdesc_submit_Click(object sender, EventArgs e)
        {
            pnl_foodpacks.Visible=false;
        }

        private void btn_packsitems_Click(object sender, EventArgs e)
        {

        }

        private void btnresturant_menu_Click(object sender, EventArgs e)
        {

        }

        private void FrmPartyBooking_Resize(object sender, EventArgs e)
        {

            if (pnl_foodpacks.Visible)
            {
                pnl_foodpacks.Width = (int)(this.ClientSize.Width * 0.8);
                pnl_foodpacks.Height = (int)(this.ClientSize.Height * 0.8);

                pnl_foodpacks.Left = (this.ClientSize.Width - pnl_foodpacks.Width) / 2;
                pnl_foodpacks.Top = (this.ClientSize.Height - pnl_foodpacks.Height) / 2;
            }
        }

    }

}
