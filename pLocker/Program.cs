using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pLocker
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
            Application.Run(new pLocker.Forms.frmLogin());
           // Application.Run(new pLocker.Forms.FrmRegistration());
           // Application.Run(new pLocker.Forms.FrmLocker());
         //Application.Run(n546ew pLocker.Forms.FrmLockerDashBoard());
          // Application.Run(new pLocker.Forms.FrmEmployee());
         

        }
    }
}
