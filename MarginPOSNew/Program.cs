using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarginPOSNew
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           Application.EnableVisualStyles();
           Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MarginPOSNew.Forms.UserAccounts.FrmLogin());
            // Application.Run(new MarginPOSNew.Forms.FrmSplash());
            //admin Application.Run(new MarginPOSNew.Forms.UserAccounts.FrmLogin());
            





        }
    }
}
