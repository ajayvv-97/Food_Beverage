using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using ReaderSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AYJCardLib;
using System.Data.SqlClient;

namespace pLocker.Forms
{
    public partial class FrmSplash : Form
    {
        public FrmSplash()
        {
            InitializeComponent();
        }

       

        private void tmrsplash_Tick(object sender, EventArgs e)
        {
            tmrsplash.Stop();
            frmLogin frmLogin = new frmLogin();
            frmLogin.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmSplash_Load(object sender, EventArgs e)
        {
                tmrsplash.Interval = 100;
                tmrsplash.Tick += new EventHandler(tmrsplash_Tick);
                tmrsplash.Start();
            
        }
    }
}
