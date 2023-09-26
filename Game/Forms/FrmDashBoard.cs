using pLocker.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Forms
{
    public partial class FrmDashBoard : Form
    {
        public FrmDashBoard()
        {
            InitializeComponent();
        }

        private void FrmDashBoard_Load(object sender, EventArgs e)
        {
            pnlgame_Click(sender, e);
          
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            Color color = ColorTranslator.FromHtml("#171c44");
            pnlgame.BackColor = color;


            pnldash.Controls.Clear();
            FrmCustomerList f = new FrmCustomerList();
            f.TopLevel = false;
            pnldash.Controls.Add(f);
            f.Show();





        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
          
        }
        public void Gamedetails(int id)
        {
            pnldash.Controls.Clear();
            frmGame f = new frmGame(id);
            f.TopLevel = false;
            pnldash.Controls.Add(f);
            f.Show();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            pnldash.Controls.Clear();
            FrmCustomerList f = new FrmCustomerList();
            f.TopLevel = false;
            pnldash.Controls.Add(f);
            f.Show();
        }

        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            Color color = ColorTranslator.FromHtml("#1d3364");
            picgame.BackColor = color;
        }

        private void pnlgame_Paint(object sender, PaintEventArgs e)
        {
            pnldash.Controls.Clear();
            frmgame_tree_strucher f = new frmgame_tree_strucher();
            f.TopLevel = false;
            pnldash.Controls.Add(f);
            f.Show();
        }

       

        private void pnlgame_MouseDown(object sender, MouseEventArgs e)
        {
            Color color = ColorTranslator.FromHtml("#2b2b2b");
            pnlgame.BackColor = color;

            color = ColorTranslator.FromHtml("#2b2b2b00");
            pnluser.BackColor=color;

        }

        private void pnluser_MouseDown(object sender, MouseEventArgs e)
        {
            Color color = ColorTranslator.FromHtml("#171c44");
            pnlgame.BackColor = color;
             color = ColorTranslator.FromHtml("#1d3364");
            pnluser.BackColor = color;
        }

        private void pnlgame_Click(object sender, EventArgs e)
        {
            pnldash.Controls.Clear();
            frmgame_tree_strucher f = new frmgame_tree_strucher();
            f.TopLevel = false;
            pnldash.Controls.Add(f);
            f.Show();
        }    

        private void panel1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pnldash.Controls.Clear();
            FrmHappyHours f = new FrmHappyHours();
            f.TopLevel = false;
            pnldash.Controls.Add(f);
            f.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pnldash.Controls.Clear();
            FrmOffer f = new FrmOffer();
            f.TopLevel = false;
            pnldash.Controls.Add(f);
            f.Show();
        }
    }
}
