using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage
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
            //Application.Run(new Forms.FrmItemRegistartion());
            Application.Run(new Forms.FrmLogin()); 
            //Application.Run(new Forms.FrmSplash());
            //Application.Run(new SaleBill.FrmSaleInvoice(0,0,0));
            //Application.Run(new Forms.FrmIngredient());
        }
    }
}
