using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pLocker.Forms
{
    public partial class FrmDashBoard : Form
    {

      
        public static long UserId = 0;
        public static long BranchId = 0;
        public static long FinancialYearId = 0;


        public FrmDashBoard()
        {
            InitializeComponent();
        }

        public FrmDashBoard(long userId, int userStatus, long employeeId, long branchId, long companyId, long financialYearId)
        {
            InitializeComponent();
            UserId = userId;
           // UserStatus = userStatus;
          //  EmployeeId = employeeId;
            BranchId = branchId;
           // CompanyId = companyId;
            FinancialYearId = financialYearId;

        }
        private void FrmDashBoard_Load(object sender, EventArgs e)
        {

        }

        private void pBLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            foreach (Form openForm in Application.OpenForms)
            {
                if (string.Compare(openForm.TopLevelControl.Name, "FrmLogin") == 0)
                {
                    openForm.Activate();
                    openForm.Show();
                    break;
                }
            }

        }

        private void pBLocker_Click(object sender, EventArgs e)
        {
            Forms.FrmLocker frmLocker = new FrmLocker();
            frmLocker.Show();
            frmLocker.WindowState = FormWindowState.Normal;

        }

        private void pBHLocker_Click(object sender, EventArgs e)
        {
            Forms.FrmDisplayLockers frmDisplayLockers = new FrmDisplayLockers();
            frmDisplayLockers.Show();
            frmDisplayLockers.WindowState = FormWindowState.Maximized;

        }

        private void FrmDashBoard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult results = MessageBox.Show("Want to Quit  ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (results == DialogResult.Yes)
                    this.Close();

            }
        }
    }
}
