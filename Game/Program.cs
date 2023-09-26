using Game.Forms;
using pLocker.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
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
            //Application.Run(new FrmDashBoard());
            //Application.Run(new frmgame_tree_strucher());
            Application.Run(new Game.Forms.FrmOffer());
        }
    }
}
