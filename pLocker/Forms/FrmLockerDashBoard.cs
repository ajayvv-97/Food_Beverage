using BLL;
using Domain;
using pLocker.Classes;
using pLocker.Forms;
using pLocker.Forms.UserControls;
using pLocker.Interfaces;
using pLocker.Forms.UserAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RJCodeAdvance;
using RJCodeAdvance.RJControls;


namespace pLocker.Forms
{
    public partial class FrmLockerDashBoard : Form
    {
       ClsDocument clsDocument = new ClsDocument();
        public static long UserId = 0;
        public static long BranchId = 0;
        public static long FinancialYearId = 0;
        public static int UserType = 0;

        public FrmLockerDashBoard()
        {
            InitializeComponent();
            UserType = Convert.ToInt32(frmLogin.UserAccountTypeId);
            //UserId = userId;
            //// UserStatus = userStatus;
            ////  EmployeeId = employeeId;
            //BranchId = branchId;
            //// CompanyId = companyId;
            //FinancialYearId = financialYearId;

        }



      

        private void FrmLockerDashBoard_Load(object sender, EventArgs e)
        {
            FormPermissionChecking();
            pCenter.Controls.Clear();
            FrmDisplayLockers frmdisplay = new FrmDisplayLockers();
            frmdisplay.TopLevel = false;
            pCenter.Controls.Add(frmdisplay);
            frmdisplay.Show();
            bool imageExist = clsDocument.FileExistOrNot(frmLogin.EmployeeCode + ".jpg", DocumentType.EmployeePhoto);
            if (imageExist)
                pbEmployee.BackgroundImage = clsDocument.DownloadImage(frmLogin.EmployeeCode + ".jpg", DocumentType.EmployeePhoto);
                pbEmployee.BackgroundImageLayout = ImageLayout.Stretch;
               lblEmpName.Text = frmLogin.EmployeeName;        
        }

        private void btnLogout_Click(object sender, EventArgs e)
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

        private void btnLocker_Click(object sender, EventArgs e)
        {
            Forms.FrmLocker frmLocker = new FrmLocker();
            frmLocker.ShowDialog();
            frmLocker.WindowState = FormWindowState.Normal;

        }

        public void btnHire_Click(object sender, EventArgs e)
        {            
            FrmHireList frmdisplay = new FrmHireList();            
            frmdisplay.ShowDialog();
            frmdisplay.WindowState = FormWindowState.Normal;
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
           
            FrmCustomerList frmCustomerList = new FrmCustomerList();
            frmCustomerList.ShowDialog();
          
        }


        private void FrmLockerDashBoard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult results = MessageBox.Show("", "Want to Quit  ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (results == DialogResult.Yes)
                    this.Close();

            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool ChkWritePermission(int formid)
        {
            BllSearch<DmnForms> bllacctype = new BllSearch<DmnForms>();
            DmnSearch dmnforms = new DmnSearch();
            dmnforms.Select = "*";
            dmnforms.Table = " tblforms ";
            dmnforms.WhereNdGroupBy = " where UserType=" + UserType + " and Status=1 and FormType=1  and Form=" + formid;
            dmnforms.OrderBy = "order by Form";
            var gridesource = bllacctype.GetSearchData("prdSearch", dmnforms);
            if (gridesource.Count > 0)
            {
                if (gridesource[0].Write == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            lblDay.Text = DateTime.Today.ToString("ddd");
            lblMonth.Text = DateTime.Now.ToString("MMM");
            lbltoday.Text = DateTime.Now.ToString("dd");
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
            lblTT.Text = DateTime.Now.ToString("tt");
        }
        public void FormPermissionChecking()
        {

            BllSearch<DmnForms> bllacctype = new BllSearch<DmnForms>();
            DmnSearch dmnforms = new DmnSearch();
            dmnforms.Select = "*";
            dmnforms.Table = " tblforms ";
            dmnforms.WhereNdGroupBy = " where FormType=1 and Status=1 and UserType=" + UserType;
            dmnforms.OrderBy = "order by Form";
            var gridesource = bllacctype.GetSearchData("prdSearch", dmnforms);
            if (gridesource.Count > 0)
            {
                for (int i = gridesource.Count() - 1; i >= 0; i--)
                {
                    if (gridesource[i].Form == Convert.ToInt32(formsenum.Game) && gridesource[i].Status == 1)
                    {
                       
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Game_Category_SubCategory) && gridesource[i].Status == 1)
                    {
                    
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Customer) && gridesource[i].Status == 1)
                    {
                      
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Happy_Hour) && gridesource[i].Status == 1)
                    {
                       
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Offer) && gridesource[i].Status == 1)
                    {
                       
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Employee) && gridesource[i].Status == 1)
                    {
                        btnEmployee.Visible = true;
                        toolStripMenuemployee.Visible = true;

                       
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.UserRegistration) && gridesource[i].Status == 1)
                    {
                        btnEmployee.Visible = true;
                        toolStripMenuUserReg.Visible = true;
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Role_Registartion) && gridesource[i].Status == 1)
                    {
                      
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Department) && gridesource[i].Status == 1)
                    {
                        btnEmployee.Visible = true;
                        ToolStripMenuDepartment.Visible = true;
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Designation) && gridesource[i].Status == 1)
                    {
                        btnEmployee.Visible = true;
                        ToolStripMenuDesignation.Visible = true;
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Recharge) && gridesource[i].Status == 1)
                    {
                      
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Location) && gridesource[i].Status == 1)
                    {
                       
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Corporate) && gridesource[i].Status == 1)
                    {
                     

                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.BlockList) && gridesource[i].Status == 1)
                    {
                       
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.TaxSettings) && gridesource[i].Status == 1)
                    {
                       
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.ButtonCreation) && gridesource[i].Status == 1)
                    {
                        
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Product) && gridesource[i].Status == 1)
                    {
                      
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Purchase) && gridesource[i].Status == 1)
                    {
                       
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Purchase_Return) && gridesource[i].Status == 1)
                    {
                       
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.MemberShip) && gridesource[i].Status == 1)
                    {
                       
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Profile) && gridesource[i].Status == 1)
                    {
                      
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Home) && gridesource[i].Status == 1)
                    {
                    
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.CardSale) && gridesource[i].Status == 1)
                    {
                      
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.FamilyCard) && gridesource[i].Status == 1)
                    {
                     
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.GroupSale) && gridesource[i].Status == 1)
                    {
                       
                    }
                    else if (gridesource[i].Form == Convert.ToInt32(formsenum.Report) && gridesource[i].Status == 1)
                    {
                        
                        //reportToolStripMenuItem.Visible = true;
                    }

                }
            }
        }


        private void btnEmployee_Click(object sender, EventArgs e)
        {
            

            rjDropdownMenuEmployee.BackColor = Color.Black;
            rjDropdownMenuEmployee.MenuItemTextColor = Color.White;
            rjDropdownMenuEmployee.Show(btnEmployee, btnEmployee.Width, 0);
            //rjDropdownMenuEmployee.TextDirection = Left;
            //FrmEmployeeShort frmEmployee = new FrmEmployeeShort();
            //frmEmployee.ShowDialog();

            //FrmUserAccount frmEmployee = new FrmUserAccount();
            //frmEmployee.ShowDialog();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FrmRpt_Option frmRpt = new FrmRpt_Option();
            frmRpt.ShowDialog();
        }
        public void txtFlow(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
                // e.Handled = true;
            }


        }

        private void Minimize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

       
        
        // btn Settings
        private void btnSettings_MouseHover(object sender, EventArgs e)
        {
            this.btnSettings.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.settings_3));
        }
       

        private void btnSettings_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnSettings.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.settings_2nd));
        }

        private void btnSettings_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnSettings.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.settings_1st));
        }

        private void btnSettings_MouseLeave(object sender, EventArgs e)
        {
            this.btnSettings.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.settings_1st));
        }

        //btn hire
        private void btnHire_MouseHover(object sender, EventArgs e)
        {
            this.btnHire.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.hire_3));
        }

        private void btnHire_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnHire.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.hire_2nd));
        }

        private void btnHire_MouseLeave(object sender, EventArgs e)
        {
            this.btnHire.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.hire_1st));
        }

        private void btnHire_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnHire.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.hire_1st));
        }

        //customer
        private void btnCustomer_MouseHover(object sender, EventArgs e)
        {
            this.btnCustomer.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.customer_3));
        }

        private void btnCustomer_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnCustomer.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.customer_2nd));
        }

        private void btnCustomer_MouseLeave(object sender, EventArgs e)
        {
            this.btnCustomer.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.customer_1st));
        }

        private void btnCustomer_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnCustomer.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.customer_1st));
        }

        //locker
        private void btnLocker_MouseHover(object sender, EventArgs e)
        {
            this.btnLocker.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.locker__3)); ;
        }

        private void btnLocker_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnLocker.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.locker__2nd));
        }

        private void btnLocker_MouseLeave(object sender, EventArgs e)
        {
            this.btnLocker.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.locker_1st));
        }

        private void btnLocker_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnLocker.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.locker_1st));
        }

        //Employee
        private void btnEmployee_MouseHover(object sender, EventArgs e)
        {
            this.btnEmployee.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.employee_3));
        }

        private void btnEmployee_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnEmployee.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.employee_2nd));
        }

        private void btnEmployee_MouseLeave(object sender, EventArgs e)
        {
            this.btnEmployee.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.employee_1st));
        }

        private void btnEmployee_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnEmployee.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.employee_1st));
        }

        //report
        private void btnReport_MouseHover(object sender, EventArgs e)
        {
            this.btnReport.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.report_3));
        }

        private void btnReport_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnReport.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.report__2nd));
        }

        private void btnReport_MouseLeave(object sender, EventArgs e)
        {
            this.btnReport.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.report_1st));
        }

        private void btnReport_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnReport.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.report_1st));
        }

        //logout
        private void btnLogout_MouseHover(object sender, EventArgs e)
        {
            this.btnLogout.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.logout_red_3));
        }

        private void btnLogout_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnLogout.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.logout_red_2nd));
        }

        private void btnLogout_MouseLeave(object sender, EventArgs e)
        {
            this.btnLogout.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.logout_red_1st));
        }

        private void btnLogout_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnLogout.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.logout_red_1st));
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
           FrmTaxSettings frmTax = new FrmTaxSettings();
            frmTax.ShowDialog();
            
        }

        private void toolStripMenuemployee_Click(object sender, EventArgs e)
        {
            FrmEmployeeShort frmEmployee = new FrmEmployeeShort();
            frmEmployee.ShowDialog();
            frmEmployee.BringToFront();
        }

        private void toolStripMenuUserReg_Click(object sender, EventArgs e)
        {
            FrmUserAccount frmUser = new FrmUserAccount();
            frmUser.ShowDialog();
            frmUser.BringToFront();
        }

        private void ToolStripMenuDepartment_Click(object sender, EventArgs e)
        {
            FrmDepartment frmDep = new FrmDepartment();
            frmDep.ShowDialog();
            frmDep.BringToFront();
        }

        private void ToolStripMenuDesignation_Click(object sender, EventArgs e)
        {
            FrmDesignation frmDes = new FrmDesignation();
            frmDes.ShowDialog();
            frmDes.BringToFront();
        }
    }

}
