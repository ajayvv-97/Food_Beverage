using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmSplash : Form
    {
        public FrmSplash()
        {
            InitializeComponent();
            //CenterPanelOnScreen(pnl_all);
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            progressbar.Value += 1;
            if (progressbar.Value == 1000)
            {
                lblloading.Text = "Loading Completed";
            }
            if (progressbar.Value == 100)
            {
                Timer1.Enabled = false;                       
                Timer1.Stop();

                if (System.Windows.Forms.Application.OpenForms["FrmLogin"] != null)
                {
                    FrmLogin _instanceFrmLogin = Application.OpenForms["FrmLogin"] as FrmLogin;
                    if (_instanceFrmLogin.InvokeRequired)
                    {
                        _instanceFrmLogin.Invoke(new Action(() => { _instanceFrmLogin.Opacity = 1.0; }));
                    }
                    else
                    {
                        _instanceFrmLogin.Opacity = 1.0;
                    }
                }
                this.Close();
            }
        }

        private void FrmSplash_Load(object sender, EventArgs e)
        {
            //this.BackgroundImage = Properties.Resources.F___B_SCREEN__LOADING; 
            //this.BackgroundImageLayout = ImageLayout.Stretch;
            Timer1.Start();
        }
        private void CenterPanelOnScreen(Panel panel)
        {
            pnl_all.Location = new Point(
                (this.ClientSize.Width / 2) - (pnl_all.Size.Width / 2),
                this.ClientSize.Height - pnl_all.Size.Height
            );

        }
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }
    }
}
