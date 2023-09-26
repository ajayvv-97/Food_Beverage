using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using System.Runtime.InteropServices;


namespace MarginPOSNew.Forms
{
    public partial class FrmSplash :Form
    {
        public FrmSplash()
        {
            InitializeComponent();
        }

        private void FrmSplash_Load(object sender, EventArgs e)
        {
            tmrsplash.Interval = 100;
            tmrsplash.Tick += new EventHandler(tmrsplash_Tick);
            tmrsplash.Start();
        }

        private void tmrsplash_Tick(object sender, EventArgs e)
        {
            tmrsplash.Stop();
            UserAccounts.FrmLogin frmLogin = new UserAccounts.FrmLogin();
            frmLogin.ShowDialog();
        }
    }
}
