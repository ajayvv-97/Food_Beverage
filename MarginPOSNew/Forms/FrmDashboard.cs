using BLL;
using Domain;
using MarginPOSNew.Forms.Masters;
using MarginPOSNew.Forms.Transactions;
using MarginPOSNew.Forms.UserAccounts;
using MetroFramework;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarginPOSNew.Forms
{
    public partial class FrmDashboard : MetroFramework.Forms.MetroForm
    {
        #region Declarations
        public static long UserId=0;
        public static int UserStatus = 0;
        public static long EmployeeId = 0;
        public static long BranchId=0;
        public static long CompanyId=0;
        public static long FinancialYearId = 0;
        public TimeSpan PressTime= DateTime.Now.TimeOfDay;
        public bool PressNdHold = false;
        #endregion

        #region Functions
        public FrmDashboard()
        {
            InitializeComponent();
        }
        public FrmDashboard(long userId, int userStatus, long employeeId, long branchId, long companyId, long financialYearId)
        {
            InitializeComponent();
            UserId = userId;
            UserStatus = userStatus;
            EmployeeId = employeeId;
            BranchId = branchId;
            CompanyId = companyId;
            FinancialYearId = financialYearId;

        }
        #endregion
        public void CloseAllPanels()
        {
            //foreach (var Ctrl in this.Controls)
            //{
            //    if (Ctrl is MetroFramework.Controls.MetroPanel)
            //    {
            //        if (Convert.ToString(((MetroFramework.Controls.MetroPanel)Ctrl).Tag) == "SubMenu")
            //        {
            //            if (((MetroFramework.Controls.MetroPanel)Ctrl).InvokeRequired)
            //            {
            //                ((MetroFramework.Controls.MetroPanel)Ctrl).Invoke(new MethodInvoker(delegate { ((MetroFramework.Controls.MetroPanel)Ctrl).Visible = false; }));
            //            }
            //        }
            //    }
            //}
            mpMaster.Visible = false;
            mpTransaction.Visible = false;
            mpReports.Visible = false;
            mpSettings.Visible = false;
            
        }

        #region Events
        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                culture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
                culture.DateTimeFormat.LongTimePattern = "";
                Thread.CurrentThread.CurrentCulture = culture;
                tmrClock.Start();
                this.BringToFront();
                mpMaster.Visible = false;
                mpTransaction.Visible = false;
                mpReports.Visible = false;
                mpSettings.Visible = false;
                #region GetLoginDetails
                DmnSearch objSearch = new DmnSearch();
                BllSearch<DmnDashboard> bllSearch = new BllSearch<DmnDashboard>();
                objSearch = new DmnSearch();
                objSearch.Select = " tblUserAccount.Username,"+
                                   " tblUserAccount.UserAccountTypeId,"+
                                   " tblBranch.BranchName,"+
                                   " tblCompany.CompanyName";
                objSearch.Table = " tblUserAccount "+
                                  " inner join tblBranch on tblBranch.BranchId = tblUserAccount.BranchId "+
                                  " inner join tblCompany on tblCompany.CompanyId = tblBranch.CompanyId ";
                objSearch.WhereNdGroupBy = "Where tblUserAccount.Status = 0 AND tblUserAccount.UserAccountId= " + UserId;
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                mlCompanyname.Text ="Enterprise : " + Convert.ToString(gridSource[0].CompanyName) + " " + Convert.ToString(gridSource[0].BranchName);
                mlMarginPOSVersion.Text ="MarginPOS " + Assembly.GetEntryAssembly().GetName().Version.ToString();
                mlStatus1.Text ="User: " + gridSource[0].Username.ToString();
                #endregion

                #region privilages
                if(FrmLogin.UserAccountTypeId==3)
                {

                }
                #endregion
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtExit_Click_1(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TmrClock_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    TimeSpan duration=DateTime.Parse(DateTime.Now.TimeOfDay.ToString()) - DateTime.Parse(PressTime.ToString());

            //    mlDateTime.Text = DateTime.Now.ToLongDateString() + "    " + DateTime.Now.ToLongTimeString();
            //    if (PressNdHold==true && duration.Seconds==2)
            //    {
            //        foreach (Form frm in this.MdiChildren)
            //        {
            //            frm.Close();

            //        }
            //        CloseAllPanels();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        private void MtMasters_Click(object sender, EventArgs e)
        {
            try
            {
                CloseAllPanels();
                mpMaster.Visible = true;
                mpMaster.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                CloseAllPanels();
                mpTransaction.Visible = true;
                mpTransaction.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtReports_Click(object sender, EventArgs e)
        {
            try
            {
                CloseAllPanels();
                if (FrmLogin.UserAccountTypeId == 1 || FrmLogin.UserAccountTypeId == 5)
                {
                    mpReports.Visible = true;
                    mpReports.Dock = DockStyle.Fill;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtSettings_Click(object sender, EventArgs e)
        {
            try
            {
                CloseAllPanels();
                if (FrmLogin.UserAccountTypeId == 1)
                {
                    mpSettings.Visible = true;
                    mpSettings.Dock = DockStyle.Fill;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtCloseCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                Form activeChild = this.ActiveMdiChild;
                if (activeChild != null)
                {
                    this.ActiveMdiChild.Close();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtAccountGroup_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus= Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmAccountGroup frmAccountGroup = new FrmAccountGroup();
                frmAccountGroup.MdiParent = this;
                frmAccountGroup.Show();
                frmAccountGroup.BringToFront();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtAccountLedger_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmAccountLedger frmAccountLedger = new FrmAccountLedger();
                frmAccountLedger.MdiParent = this;
                frmAccountLedger.Show();
               
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtCategory_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmCategory frmCategory = new FrmCategory();
                frmCategory.MdiParent = this;
                frmCategory.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtCompany_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmCompanyNBranch frmCompanyNBranch = new FrmCompanyNBranch();
                frmCompanyNBranch.MdiParent = this;
                frmCompanyNBranch.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmDepartment frmDepartment = new FrmDepartment();
                frmDepartment.MdiParent = this;
                frmDepartment.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtDesignation_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmDesignation frmDesignation = new FrmDesignation();
                frmDesignation.MdiParent = this;
                frmDesignation.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MyEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmEmployee frmEmployee = new FrmEmployee();
                frmEmployee.MdiParent = this;
                frmEmployee.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtFinancialYear_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmFinancialYear frmFinancialYear = new FrmFinancialYear();
                frmFinancialYear.MdiParent = this;
                frmFinancialYear.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtItemClass_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());

                FrmItemClass frmItemClass = new FrmItemClass();
                frmItemClass.MdiParent = this;
                frmItemClass.Show();



            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtItem_Click(object sender, EventArgs e)
        {
            try
            {
                // var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmItem frmItem = new FrmItem();
                frmItem.MdiParent = this;
                frmItem.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtUnit_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmUnit frmUnit = new FrmUnit();
                frmUnit.MdiParent = this;
                frmUnit.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtPurchase_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmPurchase frmPurchase = new FrmPurchase();
                frmPurchase.MdiParent = this;
                frmPurchase.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtSales_Click(object sender, EventArgs e)
        {
            try
            {
                if (FrmLogin.UserAccountTypeId == 1)
                {
                    DialogResult dialogResult = MetroMessageBox.Show(this, "Do you want to open Sales POS screen " + "?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        CloseAllPanels();
                        FrmSalesPOS frmSalesPOS = new FrmSalesPOS();
                        frmSalesPOS.Show();
                        frmSalesPOS.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.None;
                        frmSalesPOS.WindowState = FormWindowState.Maximized;
                    }
                    else
                    {
                        //var panelStatus = Task.Run(() => CloseAllPanels());
                        CloseAllPanels();
                        FrmSales frmSales = new FrmSales();
                        frmSales.MdiParent = this;
                        frmSales.Show();
                    }
                }
                else
                {
                    //var panelStatus = Task.Run(() => CloseAllPanels());
                    CloseAllPanels();
                    FrmSales frmSales = new FrmSales();
                    frmSales.MdiParent = this;
                    frmSales.Show();
                }
                

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtGeneralType_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmGType frmGType = new FrmGType();
                frmGType.MdiParent = this;
                frmGType.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtGeneral_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmGeneralOptions frmGeneralOptions = new FrmGeneralOptions();
                frmGeneralOptions.MdiParent = this;
                frmGeneralOptions.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtUserAccountType_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmUserAccountType frmUserAccountType = new FrmUserAccountType();
                frmUserAccountType.MdiParent = this;
                frmUserAccountType.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtUserAccount_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmUserAccount frmUserAccount = new FrmUserAccount();
                frmUserAccount.MdiParent = this;
                frmUserAccount.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtEmployeeEarnDedu_Click(object sender, EventArgs e)
        {
            try
            {
                CloseAllPanels();
                FrmEmployeeEarnDedu frmEmployeeEarnDedu = new FrmEmployeeEarnDedu();
                frmEmployeeEarnDedu.MdiParent = this;
                frmEmployeeEarnDedu.Show();
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtPurchaseReturn_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmPurchaseReturn frmPurchaseReturn = new FrmPurchaseReturn();
                frmPurchaseReturn.MdiParent = this;
                frmPurchaseReturn.Show();

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mtSalesReturn_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmSalesReturn frmSalesReturn = new FrmSalesReturn();
                frmSalesReturn.MdiParent = this;
                frmSalesReturn.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mtSalesReport_Click(object sender, EventArgs e)
        {
            try
            {
                CloseAllPanels();
                Reports.FrmRptSalesDetail frmRptSales = new Reports.FrmRptSalesDetail();
                frmRptSales.MdiParent = this;
                frmRptSales.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mtSalesPosWiseReport_Click(object sender, EventArgs e)
        {
            try
            {
                CloseAllPanels();
                Reports.FrmRptSalesPosWise frmRptSalesPosWise = new Reports.FrmRptSalesPosWise();
                frmRptSalesPosWise.MdiParent = this;
                frmRptSalesPosWise.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtCloseCurrent_MouseHover(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtCloseCurrent_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                PressNdHold = true;
                PressTime = DateTime.Now.TimeOfDay;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtCloseCurrent_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                PressNdHold = false;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtSalesPOS_Click(object sender, EventArgs e)
        {
            try
            {
                CloseAllPanels();
                FrmSalesPOS frmSalesPOS = new FrmSalesPOS();
                //frmSalesPOS.MdiParent = this;
                frmSalesPOS.Show();
                //frmSalesPOS.TopMost = true;
                frmSalesPOS.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.None;
                frmSalesPOS.WindowState = FormWindowState.Maximized;


            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtClosingSale_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtListItem_Click(object sender, EventArgs e)
        {
            try
            {
                CloseAllPanels();
                FrmListItem frmListItem = new FrmListItem();
                frmListItem.MdiParent = this;
                frmListItem.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mtRptSalesDetails_Click(object sender, EventArgs e)
        {
            try
            {
                CloseAllPanels();
                Reports.FrmRptSalesDetail frmRptSalesDetail = new Reports.FrmRptSalesDetail();
                frmRptSalesDetail.MdiParent = this;
                frmRptSalesDetail.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pbclose_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void MtCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                CloseAllPanels();
                FrmCustomerPersonal frmCustomerPersonal = new FrmCustomerPersonal();
                frmCustomerPersonal.MdiParent = this;
                frmCustomerPersonal.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtAccountGroup_MouseHover_1(object sender, EventArgs e)
        {
            try
            {
                MetroTile currentTile = (MetroTile)sender;
                currentTile.ForeColor = Color.White;
                currentTile.BackColor = Color.FromArgb(27, 72, 103);
                currentTile.UseTileImage = false;
                currentTile.UseCustomBackColor = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtAccountGroup_MouseLeave_1(object sender, EventArgs e)
        {
            try
            {
                MetroTile currentTile = (MetroTile)sender;
                currentTile.ForeColor = Color.FromArgb(27, 72, 103);
                currentTile.BackColor = Color.White;
                currentTile.UseTileImage = true;
                currentTile.UseCustomBackColor = false;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtPurchase_MouseHover(object sender, EventArgs e)
        {
            try
            {
                MetroTile currentTile = (MetroTile)sender;
                currentTile.ForeColor = Color.White;
                currentTile.BackColor = Color.FromArgb(27, 72, 103);
                currentTile.UseTileImage = false;
                currentTile.UseCustomBackColor = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtPurchase_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                MetroTile currentTile = (MetroTile)sender;
                currentTile.ForeColor = Color.FromArgb(27, 72, 103);
                currentTile.BackColor = Color.White;
                currentTile.UseTileImage = true;
                currentTile.UseCustomBackColor = false;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtListItem_MouseHover(object sender, EventArgs e)
        {
            try
            {
                MetroTile currentTile = (MetroTile)sender;
                currentTile.ForeColor = Color.White;
                currentTile.BackColor = Color.FromArgb(27, 72, 103);
                currentTile.UseTileImage = false;
                currentTile.UseCustomBackColor = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtListItem_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                MetroTile currentTile = (MetroTile)sender;
                currentTile.ForeColor = Color.FromArgb(27, 72, 103);
                currentTile.BackColor = Color.White;
                currentTile.UseTileImage = true;
                currentTile.UseCustomBackColor = false;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mtInward_Click(object sender, EventArgs e)
        {
            try
            {
                //var panelStatus = Task.Run(() => CloseAllPanels());
                CloseAllPanels();
                FrmInward frmPurchase = new FrmInward();
                frmPurchase.MdiParent = this;
                frmPurchase.Show();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mtOutward_Click(object sender, EventArgs e)
        {
            //var panelStatus = Task.Run(() => CloseAllPanels());
            CloseAllPanels();
            FrmOutward frmPurchase = new FrmOutward();
            frmPurchase.MdiParent = this;
            frmPurchase.Show();


        }

        private void mtOffer_Click(object sender, EventArgs e)
        {
            CloseAllPanels();
            FrmOffer frmPurchase = new FrmOffer();
            frmPurchase.MdiParent = this;
            frmPurchase.Show();
        }
    }
}
