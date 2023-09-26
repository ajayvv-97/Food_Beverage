using BLL;
using Domain;
using pLocker.Classes;
using pLocker.Classes;
using pLocker.Forms.UserControls;
using pLocker.Interfaces;
using MetroFramework;
using MetroFramework.Controls;
using ReaderSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Security.AccessControl;
using System.Security.Principal;


namespace pLocker.Forms
{
    public partial class FrmEmployee : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        ClsGeneral objGeneral = new ClsGeneral();
        ClsDocument clsDocument = new ClsDocument();
        ClsImgComprossor clsImgComprossor = new ClsImgComprossor();
        public string Compressedimg = Application.StartupPath + "\\CompressedImg";
        long AccountId = 0;
        byte[] PasswordSalt, PasswordHash;
        bool EditPasswordMode = false;
        public static long EmployeeId = 0;

        FilterInfoCollection FilterInfoCollection;
        VideoCaptureDevice VideoCaptureDevice;


        long UserId = frmLogin.UserId;//1
        long BranchId = frmLogin.BranchId;
        long pUserType = frmLogin.UserAccountTypeId;
        long FinancialYearId = Convert.ToInt64(FrmLockerDashBoard.FinancialYearId);



        Bitmap bitmap, bitmapId1, bitmapId2;
        long DepartmentId = 0;
        public static string empImgExt = "";
        public static string empImgExt1 = "";
        public static string empImgExt2 = "";
        public const int tabPersonal = 0;
        public const int tabOfficial = 1;
        string EmployeeCode = "";
        bool imageExist;


        #region RFID

        #endregion
        #endregion

        #region Functions
        public bool IsICardValid(string CardNo)
        {
            tmrRFID.Enabled = false;

            if (CardNo.Length == 28)
            {
                string RcvdCardSeries = CardNo.Substring(0, 12);
                string CardType = CardNo.Substring(8, 4);

                if (CardType.StartsWith("E"))
                {
                    FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.ColumnHeaders = "Id";
                    objSearch.Select = "  count(*) AS Id ";
                    objSearch.Table = "  tblGeneralOptions G Left join tblGType GT on G.GTypeId=GT.GTypeId  ";
                    objSearch.WhereNdGroupBy = " where GT.GType='ICARDSERIES' and Caption like 'EMPLOYEE%' AND  G.GTypeSubText1 in('" + RcvdCardSeries + "') AND G.Status=0";
                    var CardCount = frmGridSearch.GetSearchGrid(objSearch);

                    if (Convert.ToInt64(CardCount["Id", 0].Value) > 0)
                    {
                        tmrRFID.Enabled = true;
                        return true;
                    }
                    MetroMessageBox.Show(this, " ", "This card not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    tmrRFID.Enabled = true;
                    return false;

                }
                else
                {
                    MetroMessageBox.Show(this, "", "Not an employee card", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    tmrRFID.Enabled = true;
                    return false;
                }
            }

            MetroMessageBox.Show(this, "", "Invalid card series", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            tmrRFID.Enabled = true;
            return false;


        }

        public FrmEmployee()
        {
            InitializeComponent();
        }
        public bool IsSavable()
        {
            Control[] controllist = { mtxtName, mtxtCode, mtxtMobile, mcmbDepartmentId, mcmbDesignationId, cmbUserAccountType, txtUsername, txtPassword, txtConfirmPwd };
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            else if (IsEmployeeExist() == false)
            {
                MetroMessageBox.Show(this, "", "Employee already exist !", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(mtxtEmail.Text) && objGeneral.CheckEmailId(this, mtxtEmail.Text) == false)
            {
                return false;
            }
            else if (IsUserExist() == true)
            {
                MetroMessageBox.Show(this, "", "\nUsername already exists !", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                return false;
            }
            else if (string.Compare(txtPassword.Text, txtConfirmPwd.Text) != 0)
            {
                MetroMessageBox.Show(this, "", "\nPassword mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                return false;
            }
            else
            {
                return true;
            }
         
        }
        public bool IsEmployeeExist()
        {
            DmnEmployee dmnEmployee = new DmnEmployee();
            BllSearch<DmnEmployee> bllSearch = new BllSearch<DmnEmployee>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = " EmployeeName";
            dmnSearch.Table = " tblEmployee";
            if (EmployeeId == 0)
                dmnSearch.WhereNdGroupBy = "Where EmployeeName='" + mtxtName.Text + "' and Status=0 and BranchId=" + BranchId;
            else
                dmnSearch.WhereNdGroupBy = "Where EmployeeName='" + mtxtName.Text + "' and Status=0 and EmployeeId!='" + EmployeeId + "' and BranchId=" + BranchId;

            var employee = bllSearch.GetSearchData("prdSearch", dmnSearch);
            if (employee.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void FillCombo()
        {
            //Department
            BllSearch<DmnComboBox> bllSearchDep = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchDep = new DmnSearch();
            dmnSearchDep.Select = "DepartmentId,DepartmentName";
            dmnSearchDep.Table = "tblDepartment";
            dmnSearchDep.WhereNdGroupBy = "Where Status =0";
            dmnSearchDep.OrderBy = "order by Value";

            var departmentSource = bllSearchDep.GetSearchData("prdComboFill", dmnSearchDep);
            mcmbDepartmentId.DataSource = departmentSource;
            mcmbDepartmentId.DisplayMember = "Value";
            mcmbDepartmentId.ValueMember = "Id";

            //Designation
            BllSearch<DmnComboBox> bllSearchDes = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchDes = new DmnSearch();
            dmnSearchDes.Select = "DesignationId,DesignationName";
            dmnSearchDes.Table = " tblDesignation ";
            dmnSearchDes.WhereNdGroupBy = "Where Status = 0 ";//AND DepartmentId = " + mcmbDepartmentId.SelectedValue;
            dmnSearchDes.OrderBy = "order by Value";

            var designationSource = bllSearchDes.GetSearchData("prdComboFill", dmnSearchDes);
            mcmbDesignationId.DataSource = designationSource;
            mcmbDesignationId.DisplayMember = "Value";
            mcmbDesignationId.ValueMember = "Id";

            //Payment Mode
            BllSearch<DmnComboBox> bllSearchPMode = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchPMode = new DmnSearch();
            dmnSearchPMode.Select = "GTypeSubId,Caption";
            dmnSearchPMode.Table = "tblGeneralOptions g ";
            dmnSearchPMode.WhereNdGroupBy = "Where g.GTypeId=2 AND g.Status = 0 ";
            dmnSearchPMode.OrderBy = "order by Value";
            var paymentmodeSource = bllSearchPMode.GetSearchData("prdComboFill", dmnSearchPMode);

            mcmbPaymentMode.DataSource = paymentmodeSource;
            mcmbPaymentMode.DisplayMember = "Value";
            mcmbPaymentMode.ValueMember = "Id";
            mcmbPaymentMode.SelectedIndex = mcmbPaymentMode.FindString("CASH");

            //Weekly Off
            BllSearch<DmnComboBox> bllSearchWeeklyOff = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchWeeklyOff = new DmnSearch();
            dmnSearchWeeklyOff.Select = "GTypeSubId,Caption";
            dmnSearchWeeklyOff.Table = "tblGeneralOptions g ";
            dmnSearchWeeklyOff.WhereNdGroupBy = "Where g.GTypeId=19 AND g.Status = 0 ";
            //dmnSearchWeeklyOff.OrderBy = "order by Value";

            var WeeklyOffSource = bllSearchWeeklyOff.GetSearchData("prdComboFill", dmnSearchWeeklyOff);
            mcmbWeeklyOff.DataSource = WeeklyOffSource;
            mcmbWeeklyOff.DisplayMember = "Value";
            mcmbWeeklyOff.ValueMember = "Id";

            //Employee Status
            BllSearch<DmnComboBox> bllSearchEStatus = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchEStatus = new DmnSearch();
            dmnSearchEStatus.Select = "GTypeSubId,Caption";
            dmnSearchEStatus.Table = "tblGeneralOptions g ";
            dmnSearchEStatus.WhereNdGroupBy = "Where g.GTypeId=20 AND g.Status = 0 ";
            dmnSearchEStatus.OrderBy = "order by Value";

            var EStatusSource = bllSearchEStatus.GetSearchData("prdComboFill", dmnSearchEStatus);
            mcmbEmpStatus.DataSource = EStatusSource;
            mcmbEmpStatus.DisplayMember = "Value";
            mcmbEmpStatus.ValueMember = "Id";
            mcmbEmpStatus.SelectedIndex = mcmbEmpStatus.FindString("ACTIVE");

            //Bank
            BllSearch<DmnComboBox> bllSearchBank = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchBank = new DmnSearch();
            dmnSearchBank.Select = "GTypeSubId,Caption";
            dmnSearchBank.Table = "tblGeneralOptions g ";
            dmnSearchBank.WhereNdGroupBy = "Where g.GTypeId=3 AND g.Status = 0 ";
            dmnSearchBank.OrderBy = "order by Value";

            var bankSource = bllSearchBank.GetSearchData("prdComboFill", dmnSearchBank);
            mcmbBank.DataSource = bankSource;
            mcmbBank.DisplayMember = "Value";
            mcmbBank.ValueMember = "Id";

            //Marital Status
            BllSearch<DmnComboBox> bllSearchMarital = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchMarital = new DmnSearch();
            dmnSearchMarital.Select = "GTypeSubId,Caption";
            dmnSearchMarital.Table = "tblGeneralOptions g ";
            dmnSearchMarital.WhereNdGroupBy = "Where g.GTypeId=8 AND g.Status = 0 ";
            dmnSearchMarital.OrderBy = "order by Value";

            var maritalSource = bllSearchMarital.GetSearchData("prdComboFill", dmnSearchMarital);
            mcmbMaritalStatus.DataSource = maritalSource;
            mcmbMaritalStatus.DisplayMember = "Value";
            mcmbMaritalStatus.ValueMember = "Id";
            mcmbMaritalStatus.SelectedIndex = mcmbMaritalStatus.FindString("SINGLE");

            //Identity Proof1
            BllSearch<DmnComboBox> bllSearchIdProof1 = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchIdProof1 = new DmnSearch();
            dmnSearchIdProof1.Select = "GTypeSubId,Caption";
            dmnSearchIdProof1.Table = "tblGeneralOptions g ";
            dmnSearchIdProof1.WhereNdGroupBy = "Where g.GTypeId=6 AND g.Status = 0 ";
            dmnSearchIdProof1.OrderBy = "order by Value";

            var IdProof1Source = bllSearchIdProof1.GetSearchData("prdComboFill", dmnSearchIdProof1);
            mcmbIdProof1.DataSource = IdProof1Source;
            mcmbIdProof1.DisplayMember = "Value";
            mcmbIdProof1.ValueMember = "Id";
            mcmbIdProof1.SelectedIndex = mcmbIdProof1.FindString("AADHAAR CARD");

            //Identity Proof2
            BllSearch<DmnComboBox> bllSearchIdProof2 = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchIdProof2 = new DmnSearch();
            dmnSearchIdProof2.Select = "GTypeSubId,Caption";
            dmnSearchIdProof2.Table = "tblGeneralOptions g ";
            dmnSearchIdProof2.WhereNdGroupBy = "Where g.GTypeId=6 AND g.Status = 0 ";
            dmnSearchIdProof2.OrderBy = "order by Value";

            var IdProof2Source = bllSearchIdProof2.GetSearchData("prdComboFill", dmnSearchIdProof2);
            mcmbIdProof2.DataSource = IdProof2Source;
            mcmbIdProof2.DisplayMember = "Value";
            mcmbIdProof2.ValueMember = "Id";
            mcmbIdProof2.SelectedIndex = mcmbIdProof2.FindString("AADHAAR CARD");

            //Payment Term
            BllSearch<DmnComboBox> bllSearchPaymentTerm = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchPaymentTerm = new DmnSearch();
            dmnSearchPaymentTerm.Select = "GTypeSubId,Caption";
            dmnSearchPaymentTerm.Table = "tblGeneralOptions g ";
            dmnSearchPaymentTerm.WhereNdGroupBy = "Where g.GTypeId=7 AND g.Status = 0 ";
            dmnSearchPaymentTerm.OrderBy = "order by Value";

            var paymentTermSource = bllSearchPaymentTerm.GetSearchData("prdComboFill", dmnSearchPaymentTerm);
            mcmbPaymentTerm.DataSource = paymentTermSource;
            mcmbPaymentTerm.DisplayMember = "Value";
            mcmbPaymentTerm.ValueMember = "Id";

            //Late Affect
            BllSearch<DmnComboBox> bllSearchLateAffect = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchLateAffect = new DmnSearch();
            dmnSearchLateAffect.Select = "GTypeSubId,Caption";
            dmnSearchLateAffect.Table = "tblGeneralOptions g ";
            dmnSearchLateAffect.WhereNdGroupBy = "Where g.GTypeId=11 AND g.Status = 0 ";
            dmnSearchLateAffect.OrderBy = "order by Value";

            var LateAffectSource = bllSearchLateAffect.GetSearchData("prdComboFill", dmnSearchLateAffect);
            mcmbLateAffect.DataSource = LateAffectSource;
            mcmbLateAffect.DisplayMember = "Value";
            mcmbLateAffect.ValueMember = "Id";

            //Caste
            BllSearch<DmnComboBox> bllSearchCaste = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchCaste = new DmnSearch();
            dmnSearchCaste.Select = "GTypeSubId,Caption";
            dmnSearchCaste.Table = "tblGeneralOptions g ";
            dmnSearchCaste.WhereNdGroupBy = "Where g.GTypeId=49 AND g.Status = 0 ";
            dmnSearchCaste.OrderBy = "order by Value";

            var CasteSouce = bllSearchCaste.GetSearchData("prdComboFill", dmnSearchCaste);
            mcmbECast.DataSource = CasteSouce;
            mcmbECast.DisplayMember = "Value";
            mcmbECast.ValueMember = "Id";


            BllSearch<DmnComboBox> bllSearchuUserAccount = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchUserAccount = new DmnSearch();
            dmnSearchUserAccount.Select = " UserAccountTypeId,UserAccountType";
            dmnSearchUserAccount.Table = " tblUserAccountType";
            dmnSearchUserAccount.WhereNdGroupBy = " Where Status = 0";
            var comboSource = bllSearchuUserAccount.GetSearchData("prdComboFill", dmnSearchUserAccount);
            cmbUserAccountType.DataSource = comboSource;
            cmbUserAccountType.DisplayMember = "Value";
            cmbUserAccountType.ValueMember = "Id";
        }

        
        

        public void ResetAllTabTextBoxes()
        {
            for (int tabIndex = 0; tabIndex < mtEmployeeDetails.TabCount; ++tabIndex)
            {
                foreach (var c in mtEmployeeDetails.TabPages[tabIndex].Controls)
                {
                    if (c is MetroTextBox)
                    {
                        ((MetroTextBox)c).Text = String.Empty;
                    }
                }

            }

        }
        public void RefreshForm()
        {
            EmployeeId = 0;
            hlTitle.Text = "Employee";
            ResetAllTabTextBoxes();
            FillCombo();
            mtxtName.Focus();
            mlblEName.Text = "";
            ActiveControl = mtxtName;
            pbEmployee.Image = null;
            pbEmployee.BackgroundImage = null;
            pbIdProof1.Image = null;
            pbIdProof2.Image = null;
            mtxtMaxLate.Text = "1";
            mtxtMonthlyLeave.Text = "1";
            dtpTimeIn.Text = "09:00:00 PM";
            dtpTimeOut.Text = "06:00:00 AM";
            pbPassword.Visible = false;
            pbIncorrectPwd.Visible = false;
            cmbUserAccountType.SelectedValue = "select";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConfirmPwd.Text = "";
            fillGridView();

            if (bitmap != null)
            {
                bitmap.Dispose();
                bitmap = null;
            }

            if (bitmapId1 != null)
            {
                bitmapId1.Dispose();
                bitmapId1 = null;
            }
            if (bitmapId2 != null)
            {
                bitmapId2.Dispose();
                bitmapId2 = null;
            }
            mbtnDelete.Enabled = false;
            if (string.IsNullOrWhiteSpace(mtxtName.Text))
            {
                mbtnSave.Enabled = false;
            }

        }
        public void GenerateCode()
        {
            int departmentId = Convert.ToInt32(mcmbDepartmentId.SelectedIndex);
            if (departmentId > 0)
            {
               
                BllSearch<DmnDepartment> bllSearch = new BllSearch<DmnDepartment>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "DepartmentId,DepartmentCode,EmployeeSrlNoCurrent";
                objSearch.Table = "tblDepartment";
                objSearch.WhereNdGroupBy = "Where DepartmentId = " + mcmbDepartmentId.SelectedValue;
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                 mtxtCode.Text = gridSource[0].DepartmentCode + (gridSource[0].EmployeeSrlNoCurrent + 1);
                
            }
        }
        public void fillGridView()
        {
            try
            {
                dgvEarnDeduction.Columns.Clear();
                dgvEarnDeduction.DataSource = null;
                dgvEarnDeduction.Refresh();
                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.ColumnHeaders = "Id,Earn Type,Earned Amount,Deduct Type,Deducted Amount";
                dmnSearch.Select = "E.EmployeeEarnedId as Id,G.Caption as Field1,E.EarnedAmount as Field2,Gp.Caption as Field3,E.DeductedAmount as Field4";
                dmnSearch.Table = " tblEmployeeEarnDedu as E " +
                    "left join tblGeneralOptions as G on G.GeneralId = E.EarnTypeId " +
                    "left join tblGeneralOptions as Gp on Gp.GeneralId = E.DeductedTypeId";
                dmnSearch.WhereNdGroupBy = " Where E.Status = 0 and E.EmployeeId=" + EmployeeId + " AND BranchId = " + BranchId;
                dmnSearch.OrderBy = " Order By E.EmployeeEarnedId ASC";
                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);

                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvEarnDeduction.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvEarnDeduction.DataSource = gridSource.DataSource;

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, "Error Message/n" + ex.Message + "\nError code\n" + ex.StackTrace);
            }
        }

        #endregion

        public void SetpbImage()
        {
            if (EditPasswordMode)
            {
                pbPassword.Visible = false;
                pbIncorrectPwd.Visible = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtConfirmPwd.Text))
                {
                    if (string.Compare(txtPassword.Text, txtConfirmPwd.Text) == 0)
                    {
                        pbPassword.Visible = true;
                        pbIncorrectPwd.Visible = false;
                    }
                    else
                    {
                        pbIncorrectPwd.Visible = true;
                        pbPassword.Visible = false;
                    }
                }
                else
                {

                }



            }

        }

        public bool IsUserExist()
        {
            DmnUserAccount dmnUserAccount = new DmnUserAccount();
            BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
            DmnSearch dmnSearch = new DmnSearch();

            dmnSearch.Select = " Username";
            dmnSearch.Table = " tblUserAccount";

            if (EmployeeId == 0)
                dmnSearch.WhereNdGroupBy = "Where Username='" + txtUsername.Text.Trim() + "'  and Status=0 ";
            else
                dmnSearch.WhereNdGroupBy = "Where Username='" + txtUsername.Text.Trim() + "' and Status=0 and EmployeeId<>'" + EmployeeId + "'";
            var user = bllSearch.GetSearchData("prdSearch", dmnSearch);
            if (user.Count > 0)
            {
                return true;
            }

            else
            {
                return false;
            }

        }



        #region Event

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshForm();
                EditPasswordMode = true;
                pbPassword.Visible = false;
                pbIncorrectPwd.Visible = false;
                //Activate Tab Controls (Had issue when retrieving date picker values from official details tab)----------
                mtEmployeeDetails.SelectedIndex = tabOfficial;
                mtEmployeeDetails.SelectedIndex = tabPersonal;
                //--------------------------------------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

            try
            {
                FilterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                foreach (FilterInfo filterInfo in FilterInfoCollection)
                cmbImage.Items.Add(filterInfo.Name);
                cmbImage.SelectedIndex = 0;
                VideoCaptureDevice = new VideoCaptureDevice();
            }
            catch (Exception ex)
            {

            }

            if (!Directory.Exists(Compressedimg))
            {
                Directory.CreateDirectory(Compressedimg);
                DirectoryInfo dInfo = new DirectoryInfo(Compressedimg);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                dInfo.SetAccessControl(dSecurity);
            }
            else
            {
                foreach (string file in Directory.GetFiles(Compressedimg))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(file);
                }
            }


        }
        private void FrmEmployee_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);
                //if(e.KeyCode==Keys.Escape)
                //{
                //    this.Close();
                //}


            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnSave_Click(object sender, EventArgs e)
        {
           try
            {
                
                
                if (IsSavable())
                {
                    BllEmployee bllEmployee = new BllEmployee();
                    DmnEmployee dmnEmployee = new DmnEmployee();
                    dmnEmployee.EmployeeName = mtxtName.Text;
                    dmnEmployee.GuardianName = mtxtGName.Text;
                    dmnEmployee.DepartmentId = Convert.ToInt32(mcmbDepartmentId.SelectedValue);
                    dmnEmployee.DesignationId = Convert.ToInt32(mcmbDesignationId.SelectedValue);
                    dmnEmployee.Qualification = mtxtQualification.Text;
                    dmnEmployee.EmployeeAddress = mtxtPermenantAddress.Text;
                    dmnEmployee.PostalCode = mtxtPostalCode.Text;
                    dmnEmployee.DOB = Convert.ToDateTime(mdtDob.Text);
                    dmnEmployee.Telephone = mtxtTelephone.Text;
                    dmnEmployee.Mobile = mtxtMobile.Text;
                    dmnEmployee.Email = mtxtEmail.Text;
                    // if (mrbtnMale.Checked == true)
                    //  dmnEmployee.Gender = "Male";
                    // else
                    // dmnEmployee.Gender = "Female";
                    if (mrbtnMale.Checked == true)
                        dmnEmployee.Gender = 1;
                    else
                        dmnEmployee.Gender = 2;

                    dmnEmployee.MaritalStatusId = Convert.ToInt32(mcmbMaritalStatus.SelectedValue);
                    dmnEmployee.EmployeeStatusId = Convert.ToInt32(mcmbEmpStatus.SelectedValue);
                    dmnEmployee.ECast = Convert.ToInt32(mcmbECast.SelectedValue);
                    dmnEmployee.SpouseName = mtxtSpouseName.Text;
                    dmnEmployee.PresentAddress = mtxtPresentAddress.Text;
                    dmnEmployee.PaymentModeId = Convert.ToInt32(mcmbPaymentMode.SelectedValue);
                    dmnEmployee.BankId = Convert.ToInt64(mcmbBank.SelectedValue);
                    dmnEmployee.BankBranch = mtxtBankBranch.Text;
                    dmnEmployee.BankAccountNo = mtxtBankAcountNo.Text;
                    dmnEmployee.BankIFSC = mtxtBankIFSC.Text;
                    dmnEmployee.PFNo = mtxtPFNo.Text;
                    dmnEmployee.ESINo = mtxtESINo.Text;
                    dmnEmployee.PFDate = Convert.ToDateTime(mdtPFDate.Text);
                    dmnEmployee.ESIDate = Convert.ToDateTime(mdtESIDate.Text);
                    dmnEmployee.IdentityProof1Id = Convert.ToInt32(mcmbIdProof1.SelectedValue);
                    dmnEmployee.IdentityProof1No = mtxtIdProof1No.Text;
                    dmnEmployee.IdentityProof2Id = Convert.ToInt32(mcmbIdProof2.SelectedValue);
                    dmnEmployee.IdentityProof2No = mtxtIdProof2No.Text;
                    dmnEmployee.Experience = mtxtExperience.Text;
                    dmnEmployee.TotalExperience = decimal.Parse(mtxtTotalExperience.Text == string.Empty ? "0" : mtxtTotalExperience.Text);
                    dmnEmployee.WeeklyOffId = Convert.ToInt32(mcmbWeeklyOff.SelectedValue);
                    dmnEmployee.MonthlyLeave = int.Parse(mtxtMonthlyLeave.Text == string.Empty ? "0" : mtxtMonthlyLeave.Text);
                    dmnEmployee.BalanceLeave = decimal.Parse(mtxtBalanceLeave.Text == string.Empty ? "0" : mtxtBalanceLeave.Text);
                    dmnEmployee.TimeIn = dtpTimeIn.Value;
                    dmnEmployee.TimeOut = dtpTimeOut.Value;
                    dmnEmployee.MaxLate = int.Parse(mtxtMaxLate.Text == string.Empty ? "0" : mtxtMaxLate.Text);
                    dmnEmployee.LateAffectId = Convert.ToInt32(mcmbLateAffect.SelectedValue);
                    dmnEmployee.OverTimeStart = decimal.Parse(mtxtOverTimeStart.Text == string.Empty ? "0" : mtxtOverTimeStart.Text);
                    dmnEmployee.DateOfJoin = Convert.ToDateTime(mdtDateOfJoin.Text);
                    dmnEmployee.DateOfResign = Convert.ToDateTime(mdtDateOfResign.Text);
                    dmnEmployee.ResignReason = mtxtResignReason.Text;
                    dmnEmployee.Salary = decimal.Parse(mtxtSalary.Text == string.Empty ? "0.00" : mtxtSalary.Text);
                    dmnEmployee.PaymentTermId = Convert.ToInt32(mcmbPaymentTerm.SelectedValue);
                    dmnEmployee.Remarks = mtxtRemarks.Text;
                    dmnEmployee.ICardNo = mtxtICardNo.Text;
                    dmnEmployee.BranchId = BranchId;
                    dmnEmployee.EmployeeCode = mtxtCode.Text;

                    //if (pbEmployee.BackgroundImage != null)
                    //{
                    //    dmnEmployee.Photo = mtxtCode.Text + ".jpeg";
                    //    bitmap = new Bitmap(pbEmployee.BackgroundImage);
                    //}
                    //else if (pbEmployee.Image != null)
                    //{
                    //    dmnEmployee.Photo = mtxtCode.Text + ".jpeg";
                    //    bitmap = new Bitmap(pbEmployee.Image);
                    //}
                    //else
                    //{
                    //    imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + ".jpeg", DocumentType.EmployeePhoto);
                    //    if (imageExist)
                    //        clsDocument.DeleteImage(mtxtCode.Text + ".jpeg", DocumentType.EmployeePhoto);
                    //    dmnEmployee.Photo = "";
                    //}
                    dmnEmployee.IdentityProof1Atch = EmployeeId + "_1" + ".jpeg";
                    dmnEmployee.IdentityProof2Atch = EmployeeId + "_2" + ".jpeg";

                    if (pbIdProof1.Image != null)
                    {
                        dmnEmployee.IdentityProof1 = EmployeeId + ".jpeg";
                        bitmapId1 = new Bitmap(pbIdProof1.Image);
                    }
                    if (pbIdProof2.Image != null)
                    {
                        dmnEmployee.IdentityProof2 = EmployeeId + ".jpeg";
                        bitmapId2 = new Bitmap(pbIdProof2.Image);
                    }

                    BllUserAccount bllUserAccount = new BllUserAccount();//user account
                    DmnUserAccount dmnUserAccount = new DmnUserAccount();
                    BllEncryption bllEncryption = new BllEncryption();
                    dmnUserAccount.UserAccountTypeId = Convert.ToInt32(cmbUserAccountType.SelectedValue);
                    dmnUserAccount.Username = txtUsername.Text;
                    dmnUserAccount.BranchId = FrmLockerDashBoard.BranchId;
                    dmnUserAccount.EmployeeId = EmployeeId;
                    bllEncryption.encrptPassword(txtPassword.Text, out PasswordSalt, out PasswordHash);
                    dmnUserAccount.PasswordSalt = PasswordSalt;
                    dmnUserAccount.PasswordHash = PasswordHash;
                    dmnUserAccount.UserAccountId = AccountId;

                    DmnUserAccount userAccount;
                    bool SaveStatus;
                    if (cmbUserAccountType.Text !="" && txtUsername.Text != "" && txtPassword.Text != "" && txtConfirmPwd.Text != "")
                    {
                        userAccount = bllUserAccount.AddData("prdInsertUserAccount", dmnUserAccount);

                        SaveStatus = bllUserAccount.UpdateData("prdUpdateUserAccount", dmnUserAccount);
                    }

                    if (EmployeeId == 0)
                    {
                        dmnEmployee.Status = 0;
                        GenerateCode();
                        dmnEmployee.EmployeeCode = mtxtCode.Text;
                        dmnEmployee.CreatedBy = FrmLockerDashBoard.UserId;
                        
                            var saveStatus = bllEmployee.AddData("prdInsertEmployee", dmnEmployee);
                            EmployeeId = saveStatus.EmployeeId;
                       
                       
                        
                        #region Update EmployeeSrlNo
                        BllDepartment bllDepartment = new BllDepartment();
                        DmnDepartment dmnDepartment = new DmnDepartment();
                        dmnDepartment.DepartmentId = Convert.ToInt64(mcmbDepartmentId.SelectedValue);
                        var lstDataSrlno = bllDepartment.UpdateSrl("prdUpdateDepartEmpSrl", dmnDepartment);
                        #endregion


                        if (!string.IsNullOrWhiteSpace(saveStatus.EmployeeId.ToString()))
                        {
                            if (pbEmployee.BackgroundImage != null)
                            {
                                
                                if (empImgExt!="")
                                {                               
                                    Compressedimg = ClsImgComprossor.CompressImage(empImgExt, Compressedimg, 30);
                                }
                               
                                bool status = clsDocument.CopyFile(Compressedimg + "\\Image.jpeg", saveStatus.EmployeeId.ToString() + ".jpeg", DocumentType.EmployeePhoto);

                                //clsDocument.UploadImage(bitmap, dmnEmployee.EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                                bitmap = null;
                            }
                            if (pbIdProof1.Image != null)
                            {
                                string Compressedimg = Application.StartupPath + "\\CompressedImg";
                                if (empImgExt1 != "")
                                {

                                    if (!Directory.Exists(Compressedimg))
                                    {
                                        Directory.CreateDirectory(Compressedimg);
                                        DirectoryInfo dInfo = new DirectoryInfo(Compressedimg);
                                        DirectorySecurity dSecurity = dInfo.GetAccessControl();
                                        dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                                        dInfo.SetAccessControl(dSecurity);
                                    }
                                    else
                                    {
                                        foreach (string file in Directory.GetFiles(Compressedimg))
                                        {
                                            System.GC.Collect();
                                            System.GC.WaitForPendingFinalizers();
                                            File.Delete(file);
                                        }
                                    }
                                    Compressedimg = ClsImgComprossor.CompressImage(empImgExt1, Compressedimg, 30);
                                }
                                clsDocument.UploadImage(bitmapId1, dmnEmployee.IdentityProof1 + "_1" + ".jpeg", DocumentType.EmployeeIdentityProof1);
                            }
                            if (pbIdProof2.Image != null)
                            {
                                string Compressedimg = Application.StartupPath + "\\CompressedImg";
                                if (empImgExt2 != "")
                                {

                                    if (!Directory.Exists(Compressedimg))
                                    {
                                        Directory.CreateDirectory(Compressedimg);
                                        DirectoryInfo dInfo = new DirectoryInfo(Compressedimg);
                                        DirectorySecurity dSecurity = dInfo.GetAccessControl();
                                        dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                                        dInfo.SetAccessControl(dSecurity);
                                    }
                                    else
                                    {
                                        foreach (string file in Directory.GetFiles(Compressedimg))
                                        {
                                            System.GC.Collect();
                                            System.GC.WaitForPendingFinalizers();
                                            File.Delete(file);
                                        }
                                    }
                                    Compressedimg = ClsImgComprossor.CompressImage(empImgExt2, Compressedimg, 30);
                                }
                                clsDocument.UploadImage(bitmapId2, dmnEmployee.IdentityProof2 + "_2" + ".jpeg", DocumentType.EmployeeIdentityProof2);
                            }

                            //DialogResult dialogResult = MetroMessageBox.Show(this, "Want to set login privilege to " + mtxtName.Text + "?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            //if (dialogResult == DialogResult.Yes)
                            //{
                            //    MarginPOSNew.Forms.UserAccounts.FrmUserAccount frmUserAccount = new UserAccounts.FrmUserAccount();
                            //    frmUserAccount.ShowDialog();
                            //}
                            MetroMessageBox.Show(this, "Saved successfully " + mtxtName.Text, "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        RefreshForm();
                    }
                    
                    else
                    {
                        dmnEmployee.EmployeeId = EmployeeId;
                        dmnEmployee.ModifiedBy = FrmLockerDashBoard.UserId;
                        dmnEmployee.EmployeeCode = EmployeeCode;
                        if (DepartmentId != Convert.ToInt32(mcmbDepartmentId.SelectedValue))
                        {
                            GenerateCode();
                            dmnEmployee.EmployeeCode = mtxtCode.Text;
                            #region Update EmployeeSrlNo
                            BllDepartment bllDepartment = new BllDepartment();
                            DmnDepartment dmnDepartment = new DmnDepartment();
                            dmnDepartment.DepartmentId = Convert.ToInt64(mcmbDepartmentId.SelectedValue);
                            var lstDataSrlno = bllDepartment.UpdateSrl("prdUpdateDepartEmpSrl", dmnDepartment);
                            #endregion
                        }
                        bool saveStatus = bllEmployee.UpdateData("prdUpdateEmployee", dmnEmployee);
                        if (saveStatus)
                        {
                            if (pbEmployee.Image != null || pbEmployee.BackgroundImage != null)
                            {
                                if (mtxtCode.Text != EmployeeCode)
                                {
                                    imageExist = clsDocument.FileExistOrNot(EmployeeId + ".jpeg", DocumentType.EmployeePhoto);
                                    if (imageExist)
                                        clsDocument.DeleteImage(EmployeeId + ".jpeg", DocumentType.EmployeePhoto);
                                }
                                imageExist = false;
                                if (empImgExt != "")
                                {
                                     ClsImgComprossor.CompressImage(empImgExt, Compressedimg, 30);
                                }
                               
                                bool status = clsDocument.CopyFile(Compressedimg + "\\Image.jpeg", EmployeeId.ToString() + ".jpeg", DocumentType.EmployeePhoto);

                                // clsDocument.UploadImage(bitmap, mtxtCode.Text + ".jpeg", DocumentType.EmployeePhoto);
                            }
                            if (pbIdProof1.Image != null)
                            {
                                if (mtxtCode.Text != EmployeeCode)
                                {
                                    imageExist = clsDocument.FileExistOrNot(EmployeeCode + "_1" + ".jpeg", DocumentType.EmployeeIdentityProof1);
                                    if (imageExist)
                                        clsDocument.DeleteImage(EmployeeCode + "_1" + ".jpeg", DocumentType.EmployeeIdentityProof1);
                                }
                                imageExist = false;
                                clsDocument.UploadImage(bitmapId1, mtxtCode.Text + "_1" + ".jpeg", DocumentType.EmployeeIdentityProof1);
                            }
                            if (pbIdProof2.Image != null)
                            {
                                if (mtxtCode.Text != EmployeeCode)
                                {
                                    imageExist = clsDocument.FileExistOrNot(EmployeeCode + "_2" + ".jpeg", DocumentType.EmployeeIdentityProof2);
                                    if (imageExist)
                                        clsDocument.DeleteImage(EmployeeCode + "_2" + ".jpeg", DocumentType.EmployeeIdentityProof2);
                                }
                                imageExist = false;
                                clsDocument.UploadImage(bitmapId2, mtxtCode.Text + "_2" + ".jpeg", DocumentType.EmployeeIdentityProof2);
                            }
                            MetroMessageBox.Show(this, "Updated successfully " + mtxtName.Text, "Updated  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    RefreshForm();

                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshForm();
                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Employee,Code,Department,Designation";
                _objSearch.Select = "EmployeeId as Id, emp.EmployeeName as Field1,emp.EmployeeCode as Field2,dp.DepartmentName as Field3,ds.DesignationName as Field4";
                _objSearch.Table = "tblEmployee as emp " +
                               "left join tblDepartment as dp on emp.DepartmentId = dp.DepartmentId " +
                               "left join tblDesignation as ds on emp.DesignationId = ds.DesignationId";
                _objSearch.WhereNdGroupBy = "Where emp.Status=0 and emp.BranchId = 1";
                _objSearch.OrderBy = " Order By EmployeeName ASC ";
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 800, 400, 140, 180))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);

                    }
                }
                if (returnValue.ToString() != "")
                {
                    EmployeeId = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnEmployee> bllSearch = new BllSearch<DmnEmployee>();
                    DmnSearch objSearch = new DmnSearch();
                    //objSearch.Select = "tblEmployee.*,CONVERT(nvarchar(MAX),TimeIn,108) as sTimeIn,CONVERT(nvarchar(MAX),TimeOut,108) as sTimeOut,CONVERT(nvarchar(MAX),OverTimeStart,108) as sOverTimeStart";
                    objSearch.Select = "tblEmployee.*";
                    objSearch.Table = " tblEmployee ";
                    objSearch.WhereNdGroupBy = "Where EmployeeId = " + EmployeeId;
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);

                    mtxtName.Text = gridSource[0].EmployeeName;
                    mlblEName.Text = gridSource[0].EmployeeName;
                    mtxtGName.Text = gridSource[0].GuardianName;
                    mtxtQualification.Text = gridSource[0].Qualification;
                    mcmbDepartmentId.SelectedValue = gridSource[0].DepartmentId;
                    DepartmentId = gridSource[0].DepartmentId;
                    mcmbDesignationId.SelectedValue = gridSource[0].DesignationId;
                    mtxtCode.Text = gridSource[0].EmployeeCode;
                    EmployeeCode = gridSource[0].EmployeeCode;
                    mtxtPresentAddress.Text = gridSource[0].EmployeeAddress;
                    mtxtPostalCode.Text = gridSource[0].PostalCode;
                    if (gridSource[0].DOB.Year != 0001)
                        mdtDob.Text = Convert.ToString(gridSource[0].DOB);

                    mtxtTelephone.Text = gridSource[0].Telephone;
                    mtxtMobile.Text = gridSource[0].Mobile;
                    mtxtEmail.Text = gridSource[0].Email;
                    if (Convert.ToInt32(gridSource[0].Gender) == 2)
                        mrbtnFemale.Checked = true;
                    else
                        mrbtnMale.Checked = true;
                    mcmbMaritalStatus.Text = gridSource[0].MaritalStatus;
                    mcmbEmpStatus.Text = gridSource[0].EmployeeStatus;
                    mcmbECast.SelectedValue =gridSource[0].ECast;
                    mcmbIdProof1.Text = gridSource[0].IdentityProof1;
                    mcmbIdProof2.Text = gridSource[0].IdentityProof2;
                    mcmbPaymentMode.Text = gridSource[0].PaymentMode;
                    mcmbLateAffect.Text = gridSource[0].LateAffect;
                    mcmbPaymentTerm.Text = gridSource[0].PaymentTerm;

                    mcmbMaritalStatus.SelectedValue = Convert.ToInt64(gridSource[0].MaritalStatus);
                    mcmbPaymentMode.SelectedIndex = mcmbPaymentMode.FindString("CASH");

                    mcmbEmpStatus.SelectedValue = Convert.ToInt64(gridSource[0].EmployeeStatus);
                  
                    mcmbIdProof1.SelectedValue = Convert.ToInt64(gridSource[0].IdentityProof1);
                    mcmbIdProof2.SelectedValue = Convert.ToInt64(gridSource[0].IdentityProof2);
                    mcmbPaymentMode.SelectedValue = Convert.ToInt64(gridSource[0].PaymentMode);
                    mcmbLateAffect.SelectedValue = Convert.ToInt64(gridSource[0].LateAffect);
                    mcmbPaymentTerm.SelectedValue = Convert.ToInt64(gridSource[0].PaymentTerm);
                    mcmbWeeklyOff.SelectedValue = Convert.ToInt64(gridSource[0].WeeklyOff);

                    mtxtSpouseName.Text = gridSource[0].SpouseName;
                    mtxtPermenantAddress.Text = gridSource[0].PresentAddress;
                    mtxtICardNo.Text = gridSource[0].ICardNo;
                    mtxtIdProof1No.Text = gridSource[0].IdentityProof1No;
                    mtxtIdProof2No.Text = gridSource[0].IdentityProof2No;
                    mcmbBank.SelectedValue = gridSource[0].BankId;
                    mtxtBankBranch.Text = gridSource[0].BankBranch;
                    mtxtBankAcountNo.Text = gridSource[0].BankAccountNo;
                    mtxtBankIFSC.Text = gridSource[0].BankIFSC;
                    mtxtPFNo.Text = Convert.ToString(gridSource[0].PFNo);
                    mtxtESINo.Text = Convert.ToString(gridSource[0].ESINo);
                    mtxtExperience.Text = Convert.ToString(gridSource[0].Experience);
                    mtxtTotalExperience.Text = Convert.ToString(gridSource[0].TotalExperience);

                    mtxtMonthlyLeave.Text = Convert.ToString(gridSource[0].MonthlyLeave);
                    mtxtBalanceLeave.Text = Convert.ToString(gridSource[0].BalanceLeave);
                    mtxtMaxLate.Text = Convert.ToString(gridSource[0].MaxLate);

                    mtxtOverTimeStart.Text = Convert.ToString(gridSource[0].OverTimeStart);

                    if (gridSource[0].TimeIn.Year != 0001)
                        dtpTimeIn.Value = gridSource[0].TimeIn;

                    if (gridSource[0].TimeOut.Year != 0001)
                        dtpTimeOut.Value = gridSource[0].TimeOut;

                    if (gridSource[0].PFDate.Year != 0001)
                        mdtPFDate.Text = Convert.ToString(gridSource[0].PFDate);

                    if (gridSource[0].ESIDate.Year != 0001)
                        mdtESIDate.Text = Convert.ToString(gridSource[0].ESIDate);

                    if (gridSource[0].DateOfJoin.Year != 0001)
                        mdtDateOfJoin.Text = Convert.ToString(gridSource[0].DateOfJoin);

                    if (gridSource[0].DateOfResign.Year != 0001)
                        mdtDateOfResign.Text = Convert.ToString(gridSource[0].DateOfResign);

                    mtxtResignReason.Text = gridSource[0].ResignReason;
                    mtxtSalary.Text = Convert.ToString(gridSource[0].Salary);

                    mtxtRemarks.Text = gridSource[0].Remarks;
                    if (gridSource[0].Photo != "")
                    {
                        imageExist = clsDocument.FileExistOrNot(EmployeeId + ".jpeg", DocumentType.EmployeePhoto);
                        if (imageExist)
                            pbEmployee.Image = clsDocument.DownloadImage(EmployeeId + ".jpeg", DocumentType.EmployeePhoto);
                          
                        imageExist = false;
                    }
                    if (gridSource[0].IdentityProof1Atch != "")
                    {
                        imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + "_1" + ".jpeg", DocumentType.EmployeeIdentityProof1);
                        if (imageExist)
                            pbIdProof1.Image = clsDocument.DownloadImage(mtxtCode.Text + "_1" + ".jpeg", DocumentType.EmployeeIdentityProof1);
                        imageExist = false;
                    }
                    if (gridSource[0].IdentityProof2Atch != "")
                    {
                        imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + "_2" + ".jpeg", DocumentType.EmployeeIdentityProof2);
                        if (imageExist)
                            pbIdProof2.Image = clsDocument.DownloadImage(mtxtCode.Text + "_2" + ".jpeg", DocumentType.EmployeeIdentityProof2);
                        imageExist = false;
                    }
                    hlTitle.Text = "Employee [Edit]";
                    mbtnDelete.Enabled = true;
                    mtxtName.Focus();
                    fillGridView();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshForm();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnDelete_Click(object sender, EventArgs e)
        {
            bool deleteStatus;
            try
            {
                DialogResult dialogResult = MetroMessageBox.Show(this, "Confirm to delete " + " ? ", "Delete ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    BllEmployee bllEmployee = new BllEmployee();
                    DmnEmployee dmnEmployee = new DmnEmployee();
                    dmnEmployee.EmployeeId = EmployeeId;
                    dmnEmployee.ModifiedBy = FrmLockerDashBoard.UserId;
                    deleteStatus = bllEmployee.DeleteById("prdDeleteEmployee", dmnEmployee);
                    if (deleteStatus)
                    {
                        bool empId1Exist, empId2Exist;
                        imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + ".jpeg", DocumentType.EmployeePhoto);
                        if (imageExist)
                            clsDocument.DeleteImage(mtxtCode.Text + ".jpeg", DocumentType.EmployeePhoto);
                        empId1Exist = clsDocument.FileExistOrNot(mtxtCode.Text + "_1" + ".jpeg", DocumentType.EmployeeIdentityProof1);
                        if (empId1Exist)
                            clsDocument.DeleteImage(mtxtCode.Text + "_1" + ".jpeg", DocumentType.EmployeeIdentityProof1);
                        empId2Exist = clsDocument.FileExistOrNot(mtxtCode.Text + "_2" + ".jpeg", DocumentType.EmployeeIdentityProof2);
                        if (empId2Exist)
                            clsDocument.DeleteImage(mtxtCode.Text + "_2" + ".jpeg", DocumentType.EmployeeIdentityProof2);

                        MetroMessageBox.Show(this, "", "Deleted successfully", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    RefreshForm();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtxtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.TelephoneTextBox(sender, e);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void McmbDepartmentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (EmployeeId != 0)
                {
                    if (DepartmentId != Convert.ToInt32(mcmbDepartmentId.SelectedValue))
                    {
                        GenerateCode();
                    }
                    else
                        mtxtCode.Text = EmployeeCode;
                }
                else
                {
                    GenerateCode();
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtEmployeeDetails_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);
                if (!string.IsNullOrWhiteSpace(mtxtName.Text))
                {
                    if (e.KeyCode != Keys.F2 && e.KeyCode != Keys.F3 && e.KeyCode != Keys.F5 && e.KeyCode != Keys.F7 && e.KeyCode != Keys.F9 && e.KeyCode != Keys.F10 && e.KeyCode != Keys.F11 && e.KeyCode != Keys.F12)
                    {
                        mbtnSave.Enabled = true;
                    }
                }
                if (e.KeyCode == Keys.F2 && mbtnSave.Enabled == true)
                    MbtnSave_Click(sender, e);
                if (e.KeyCode == Keys.F3)
                    MbtnList_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    MbtnRefresh_Click(sender, e);
                if (e.KeyCode == Keys.F7)
                    MbtnDelete_Click(sender, e);
                if (e.KeyCode == Keys.F9)
                {
                    tmrRFID.Enabled = true;
                    pnlRFID.Visible = true;
                    mbtnRFIDRead_Click(sender, e);
                }
                if (e.KeyCode == Keys.F10)
                    MpbAdd_Click(sender, e);
                if (e.KeyCode == Keys.F11)
                    MpbRemove_Click(sender, e);
                if (e.KeyCode == Keys.F12)
                {
                    if (mtEmployeeDetails.SelectedTab == mtEmployeeDetails.TabPages["mpEarnDeduction"])
                    {
                        if (EmployeeId > 0)
                        {
                            FrmEmployeeEarnDedu frmEmployeeEarnDedu = new FrmEmployeeEarnDedu();
                            frmEmployeeEarnDedu.ShowDialog();
                        }
                        else
                            MetroMessageBox.Show(this, " ", "\n" + "Please select an employee", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (e.KeyCode == Keys.Escape)
                    if (pnlRFID.Visible == true)
                    {
                        tmrRFID.Enabled = false;
                        pnlRFID.Visible = false;

                    }
                    else
                    {
                        this.Close();
                    }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MpbAdd_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
                DialogResult dialogResult = openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    // empImgExt = Path.GetExtension(openFileDialog.FileName);
                    empImgExt = openFileDialog.FileName;
                    pbEmployee.BackgroundImage = Image.FromFile(openFileDialog.FileName);
                    pbEmployee.BackgroundImageLayout = ImageLayout.Stretch;
                    

                }
                mbtnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MpbRemove_Click(object sender, EventArgs e)
        {
            try
            {
                imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + ".jpeg", DocumentType.EmployeePhoto);
                if (imageExist)
                    clsDocument.DeleteImage(mtxtCode.Text + ".jpeg", DocumentType.EmployeePhoto);
                pbEmployee.Image = null;
                mbtnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void FrmEmployee_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.F2 && mbtnSave.Enabled == true)
                    MbtnSave_Click(sender, e);
                if (e.KeyCode == Keys.F3)
                    MbtnList_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    MbtnRefresh_Click(sender, e);
                if (e.KeyCode == Keys.F7)
                    MbtnDelete_Click(sender, e);
                //if (e.KeyCode == Keys.Escape)
                //    this.Close();

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mbtnRFIDRead_Click(object sender, EventArgs e)
        {



        }
        private void tmrRFID_Tick(object sender, EventArgs e)
        {
        }
        private void mtxtPostalCode_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                objGeneral.IntegerTextBox(sender, e);


            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mbtnCancelRFID_Click(object sender, EventArgs e)
        {
            try
            {
                tmrRFID.Enabled = false;
                pnlRFID.Visible = false;
                mtxtICardNo.Focus();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mbtnNew_Click(object sender, EventArgs e)
        {
            try
            {
                FrmEmployeeEarnDedu frmEmployeeEarnDedu = new FrmEmployeeEarnDedu();
                frmEmployeeEarnDedu.ShowDialog();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void pbAddIdProof1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "|*.jpeg;*.jpg;*.png;*.gif;";
                DialogResult dialogResult = openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    empImgExt1 = openFileDialog.FileName;
                    pbIdProof1.Image = Image.FromFile(openFileDialog.FileName);
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void pbRemoveIdProof1_Click(object sender, EventArgs e)
        {
            try
            {
                bool empId1Exist = clsDocument.FileExistOrNot(mtxtCode.Text + "_1" + ".jpeg", DocumentType.EmployeeIdentityProof1);
                if (empId1Exist)
                    clsDocument.DeleteImage(mtxtCode.Text + "_1" + ".jpeg", DocumentType.EmployeeIdentityProof1);
                pbIdProof1.Image = null;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mcmbDesignationId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(mcmbDesignationId.SelectedIndex) > 0)
                {
                    int DesignationId = Convert.ToInt32(mcmbDesignationId.SelectedValue);
                    BllSearch<DmnDesignation> bllSearch = new BllSearch<DmnDesignation>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "DepartmentId";
                    objSearch.Table = " tblDesignation ";
                    objSearch.WhereNdGroupBy = " Where DesignationId = " + DesignationId;
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    mcmbDepartmentId.SelectedValue = gridSource[0].DepartmentId;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mtxtESINo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.IntegerTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        private void mcmbDesignationId_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F12)
                {
                    FrmDesignation frmDesignation = new FrmDesignation();
                    frmDesignation.ShowDialog();
                    //Designation
                    BllSearch<DmnComboBox> bllSearchDes = new BllSearch<DmnComboBox>();
                    DmnSearch dmnSearchDes = new DmnSearch();
                    dmnSearchDes.Select = "DesignationId,DesignationName";
                    dmnSearchDes.Table = " tblDesignation ";
                    dmnSearchDes.WhereNdGroupBy = "Where Status = 0 ";//AND DepartmentId = " + mcmbDepartmentId.SelectedValue;
                    var designationSource = bllSearchDes.GetSearchData("prdComboFill", dmnSearchDes);
                    mcmbDesignationId.DataSource = designationSource;
                    mcmbDesignationId.DisplayMember = "Value";
                    mcmbDesignationId.ValueMember = "Id";
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtCast_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    CultureInfo cI = Thread.CurrentThread.CurrentCulture;
            //    TextInfo textInfo = cI.TextInfo;
            //    mtxtCast.Text = textInfo.ToTitleCase(mtxtCast.Text);
            //    mtxtCast.Select(mtxtCast.Text.Length, 1);

            //}
            //catch (Exception ex)
            //{
            //    MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void mtxtSpouseName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cI.TextInfo;
                mtxtSpouseName.Text = textInfo.ToTitleCase(mtxtSpouseName.Text);
                mtxtSpouseName.Select(mtxtSpouseName.Text.Length, 1);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mtxtMonthlyLeave_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.DecimalTextBox(sender, e);


                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true; // Ignore the character
                }
                else
                {
                    // If it's a digit, check if it's below 5
                    int value;
                    if (int.TryParse(mtxtMonthlyLeave.Text + e.KeyChar, out value) && value >= 0 && value < 5)
                    {
                        e.Handled = false; // Allow the character
                    }
                    else
                    {
                        e.Handled = true; // Ignore the character
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void mtxtBalanceLeave_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.DecimalTextBox(sender, e);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void mtxtOverTimeStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.DecimalTextBox(sender, e);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mtxtMaxLate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.DecimalTextBox(sender, e);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mtxtName_Leave(object sender, EventArgs e)
        {
            try
            {
                CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cI.TextInfo;
                mtxtName.Text = textInfo.ToTitleCase(mtxtName.Text);
                mtxtName.Select(mtxtName.Text.Length, 1);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mtxtGName_Leave(object sender, EventArgs e)
        {
            try
            {
                CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cI.TextInfo;
                mtxtGName.Text = textInfo.ToTitleCase(mtxtGName.Text);
                mtxtGName.Select(mtxtGName.Text.Length, 1);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void mbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRFID_Click(object sender, EventArgs e)
        {
            string str1 = "";
            mtxtICardNo.Clear();
            str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul();
            if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")

                str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi();
            str1 = RemoveSpecialCharacters(str1.Trim());
            if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")
                MessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //new
            else
            {

                BllSearch<DmnEmployee> bllSearch = new BllSearch<DmnEmployee>();
                DmnSearch objSearch = new DmnSearch();
                //objSearch.Select = "tblEmployee.*,CONVERT(nvarchar(MAX),TimeIn,108) as sTimeIn,CONVERT(nvarchar(MAX),TimeOut,108) as sTimeOut,CONVERT(nvarchar(MAX),OverTimeStart,108) as sOverTimeStart";
                objSearch.Select = "tblEmployee.*";
                objSearch.Table = " tblEmployee ";
                objSearch.WhereNdGroupBy = "Where ICardNo = '" + str1 + "'";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    mtxtName.Text = gridSource[0].EmployeeName;
                    mlblEName.Text = gridSource[0].EmployeeName;
                    mtxtGName.Text = gridSource[0].GuardianName;
                    mtxtQualification.Text = gridSource[0].Qualification;
                    mcmbDepartmentId.SelectedValue = gridSource[0].DepartmentId;
                    DepartmentId = gridSource[0].DepartmentId;
                    mcmbDesignationId.SelectedValue = gridSource[0].DesignationId;
                    mtxtCode.Text = gridSource[0].EmployeeCode;
                    EmployeeCode = gridSource[0].EmployeeCode;
                    mtxtPresentAddress.Text = gridSource[0].EmployeeAddress;
                    mtxtPostalCode.Text = gridSource[0].PostalCode;
                    if (gridSource[0].DOB.Year != 0001)
                        mdtDob.Text = Convert.ToString(gridSource[0].DOB);

                    mtxtTelephone.Text = gridSource[0].Telephone;
                    mtxtMobile.Text = gridSource[0].Mobile;
                    mtxtEmail.Text = gridSource[0].Email;
                    if (Convert.ToInt32(gridSource[0].Gender) == 2)
                        mrbtnFemale.Checked = true;
                    else
                        mrbtnMale.Checked = true;
                    mcmbMaritalStatus.Text = gridSource[0].MaritalStatus;
                    mcmbEmpStatus.Text = gridSource[0].EmployeeStatus;
                    mcmbECast.SelectedValue = gridSource[0].ECast;
                    mcmbIdProof1.Text = gridSource[0].IdentityProof1;
                    mcmbIdProof2.Text = gridSource[0].IdentityProof2;
                    mcmbPaymentMode.Text = gridSource[0].PaymentMode;
                    mcmbLateAffect.Text = gridSource[0].LateAffect;
                    mcmbPaymentTerm.Text = gridSource[0].PaymentTerm;

                    mcmbMaritalStatus.SelectedValue = Convert.ToInt64(gridSource[0].MaritalStatus);
                    mcmbPaymentMode.SelectedIndex = mcmbPaymentMode.FindString("CASH");

                    mcmbEmpStatus.SelectedValue = Convert.ToInt64(gridSource[0].EmployeeStatus);

                    mcmbIdProof1.SelectedValue = Convert.ToInt64(gridSource[0].IdentityProof1);
                    mcmbIdProof2.SelectedValue = Convert.ToInt64(gridSource[0].IdentityProof2);
                    mcmbPaymentMode.SelectedValue = Convert.ToInt64(gridSource[0].PaymentMode);
                    mcmbLateAffect.SelectedValue = Convert.ToInt64(gridSource[0].LateAffect);
                    mcmbPaymentTerm.SelectedValue = Convert.ToInt64(gridSource[0].PaymentTerm);
                    mcmbWeeklyOff.SelectedValue = Convert.ToInt64(gridSource[0].WeeklyOff);

                    mtxtSpouseName.Text = gridSource[0].SpouseName;
                    mtxtPermenantAddress.Text = gridSource[0].PresentAddress;
                    mtxtICardNo.Text = gridSource[0].ICardNo;
                    mtxtIdProof1No.Text = gridSource[0].IdentityProof1No;
                    mtxtIdProof2No.Text = gridSource[0].IdentityProof2No;
                    mcmbBank.SelectedValue = gridSource[0].BankId;
                    mtxtBankBranch.Text = gridSource[0].BankBranch;
                    mtxtBankAcountNo.Text = gridSource[0].BankAccountNo;
                    mtxtBankIFSC.Text = gridSource[0].BankIFSC;
                    mtxtPFNo.Text = Convert.ToString(gridSource[0].PFNo);
                    mtxtESINo.Text = Convert.ToString(gridSource[0].ESINo);
                    mtxtExperience.Text = Convert.ToString(gridSource[0].Experience);
                    mtxtTotalExperience.Text = Convert.ToString(gridSource[0].TotalExperience);

                    mtxtMonthlyLeave.Text = Convert.ToString(gridSource[0].MonthlyLeave);
                    mtxtBalanceLeave.Text = Convert.ToString(gridSource[0].BalanceLeave);
                    mtxtMaxLate.Text = Convert.ToString(gridSource[0].MaxLate);

                    mtxtOverTimeStart.Text = Convert.ToString(gridSource[0].OverTimeStart);

                    if (gridSource[0].TimeIn.Year != 0001)
                        dtpTimeIn.Value = gridSource[0].TimeIn;

                    if (gridSource[0].TimeOut.Year != 0001)
                        dtpTimeOut.Value = gridSource[0].TimeOut;

                    if (gridSource[0].PFDate.Year != 0001)
                        mdtPFDate.Text = Convert.ToString(gridSource[0].PFDate);

                    if (gridSource[0].ESIDate.Year != 0001)
                        mdtESIDate.Text = Convert.ToString(gridSource[0].ESIDate);

                    if (gridSource[0].DateOfJoin.Year != 0001)
                        mdtDateOfJoin.Text = Convert.ToString(gridSource[0].DateOfJoin);

                    if (gridSource[0].DateOfResign.Year != 0001)
                        mdtDateOfResign.Text = Convert.ToString(gridSource[0].DateOfResign);

                    mtxtResignReason.Text = gridSource[0].ResignReason;
                    mtxtSalary.Text = Convert.ToString(gridSource[0].Salary);

                    mtxtRemarks.Text = gridSource[0].Remarks;
                    if (gridSource[0].Photo != "")
                    {
                        imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + ".jpeg", DocumentType.EmployeePhoto);
                        if (imageExist)
                            pbEmployee.Image = clsDocument.DownloadImage(mtxtCode.Text + ".jpeg", DocumentType.EmployeePhoto);

                        imageExist = false;
                    }
                    if (gridSource[0].IdentityProof1Atch != "")
                    {
                        imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + "_1" + ".jpeg", DocumentType.EmployeeIdentityProof1);
                        if (imageExist)
                            pbIdProof1.Image = clsDocument.DownloadImage(mtxtCode.Text + "_1" + ".jpeg", DocumentType.EmployeeIdentityProof1);
                        imageExist = false;
                    }
                    if (gridSource[0].IdentityProof2Atch != "")
                    {
                        imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + "_2" + ".jpeg", DocumentType.EmployeeIdentityProof2);
                        if (imageExist)
                            pbIdProof2.Image = clsDocument.DownloadImage(mtxtCode.Text + "_2" + ".jpeg", DocumentType.EmployeeIdentityProof2);
                        imageExist = false;
                    }
                    hlTitle.Text = "Employee [Edit]";
                    mbtnDelete.Enabled = true;
                    mtxtName.Focus();
                    fillGridView();
                }
                else
                {
                    mtxtICardNo.Text = str1;
                }
            }



           
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            VideoCaptureDevice.Stop();
            empImgExt = "";
            Bitmap bm = new Bitmap(pbEmployee.BackgroundImage);
            bm.Save(Compressedimg + "\\Image.jpeg"); //new
        }

        private void pbEmployee_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if(cmbImage.Items.Count > 0)
                
                    pbEmployee.BackgroundImage = null;
                    pbEmployee.Image = null;
                    empImgExt = "";
                    VideoCaptureDevice = new VideoCaptureDevice(FilterInfoCollection[cmbImage.SelectedIndex].MonikerString);
                    VideoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
                    VideoCaptureDevice.Start();
                


                
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Work for Capture Device!!!! ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
           
        }


        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pbEmployee.BackgroundImage = (Bitmap)eventArgs.Frame.Clone();
            pbEmployee.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pbEmployee_Click(object sender, EventArgs e)
        {

        }

        private void btnDesgtn_Click(object sender, EventArgs e)
        {
            FrmDesignation frmDesignation = new FrmDesignation();
            frmDesignation.ShowDialog();
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            try
            {
                txtPassword.Text = "    ";
                txtConfirmPwd.Text = "    ";
                EditPasswordMode = true;
                pbPassword.Visible = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    EditPasswordMode = true;
                    txtPassword.Text = "    ";
                    txtConfirmPwd.Text = "    ";
                }
                else
                {
                    EditPasswordMode = false;
                }
                SetpbImage();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void txtConfirmPwd_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                EditPasswordMode = false;
                SetpbImage();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (mtxtName.Text.Length <= 0) return;
            string s = mtxtName.Text.Substring(0, 1);
            if (s != s.ToUpper())
            {
                int curSelStart = mtxtName.SelectionStart;
                int curSelLength = mtxtName.SelectionLength;
                mtxtName.SelectionStart = 0;
                mtxtName.SelectionLength = 1;
                mtxtName.SelectedText = s.ToUpper();
                mtxtName.SelectionStart = curSelStart;
                mtxtName.SelectionLength = curSelLength;
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDept_Click(object sender, EventArgs e)
        {
            FrmDepartment frmDepartment = new FrmDepartment();
            frmDepartment.ShowDialog(); 
        }

        //save
        private void mbtnSave_MouseHover(object sender, EventArgs e)
        {
            this.mbtnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_3));
        }

        private void mbtnSave_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_2));
        }

        private void mbtnSave_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_1));
        }

        private void mbtnSave_MouseUp(object sender, MouseEventArgs e)
        {
            this.mbtnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_1));
        }

        //search
        private void mbtnList_MouseHover(object sender, EventArgs e)
        {
            this.mbtnList.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_3));
        }

        private void mbtnList_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnList.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_2));
        }

        private void mbtnList_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnList.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_1));
        }

        private void mbtnList_MouseUp(object sender, MouseEventArgs e)
        {
            this.mbtnList.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_1));
        }


        //Refresh
        private void mbtnRefresh_MouseHover(object sender, EventArgs e)
        {
            this.mbtnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_3));
        }

        private void mbtnRefresh_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_2));
        }

        private void mbtnRefresh_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_1));
        }

        private void mbtnRefresh_MouseUp(object sender, MouseEventArgs e)
        {
            this.mbtnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_1));
        }

        //delete
        private void mbtnDelete_MouseHover(object sender, EventArgs e)
        {
            this.mbtnDelete.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.delete_3));
        }

        private void mbtnDelete_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnDelete.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.delete_2));
        }

        private void mbtnDelete_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnDelete.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.delete_1));
        }

        private void mbtnDelete_MouseUp(object sender, MouseEventArgs e)
        {
            this.mbtnDelete.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.delete_1));
        }

        //close
        private void mbtnClose_MouseHover(object sender, EventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_3));
        }

        private void mbtnClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_2));
        }

        private void mbtnClose_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1));
        }

        private void mbtnClose_MouseUp(object sender, MouseEventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1));
        }

        //capture
        private void btnCapture_MouseHover(object sender, EventArgs e)
        {
            this.btnCapture.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.camera_3));
        }

        private void btnCapture_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnCapture.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.camera_2));
        }

        private void btnCapture_MouseLeave(object sender, EventArgs e)
        {
            this.btnCapture.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.camera_1));
        }

        private void btnCapture_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnCapture.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.camera_1));
        }

        //add
        private void mpbAdd_MouseHover(object sender, EventArgs e)
        {
            this.mpbAdd.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Add_3));
        }

        private void mpbAdd_MouseDown(object sender, MouseEventArgs e)
        {
            this.mpbAdd.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Add_2));
        }

        private void mpbAdd_MouseLeave(object sender, EventArgs e)
        {
            this.mpbAdd.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Add_1));
        }

        private void mpbAdd_MouseUp(object sender, MouseEventArgs e)
        {
            this.mpbAdd.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Add_1));
        }

        //remove
        private void mpbRemove_MouseHover(object sender, EventArgs e)
        {
            this.mpbRemove.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_3_button));
        }

        private void mpbRemove_MouseDown(object sender, MouseEventArgs e)
        {
            this.mpbRemove.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_2_button));
        }

        private void mpbRemove_MouseLeave(object sender, EventArgs e)
        {
            this.mpbRemove.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1_button));
        }

        private void mpbRemove_MouseUp(object sender, MouseEventArgs e)
        {
            this.mpbRemove.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1_button));
        }

        private void btnRFID_MouseHover(object sender, EventArgs e)
        {
            this.btnRFID.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.RFID_3));
        }

        private void btnRFID_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnRFID.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.RFID_2));
        }

        private void btnRFID_MouseLeave(object sender, EventArgs e)
        {
            this.btnRFID.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.RFID_1));
        }

        private void btnRFID_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnRFID.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.RFID_1));
        }

        private void pbAddIdProof2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "|*.jpeg;*.jpg;*.png;*.gif;";
                DialogResult dialogResult = openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    empImgExt2 = openFileDialog.FileName;
                    pbIdProof2.Image = Image.FromFile(openFileDialog.FileName);
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mcmbMaritalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void mcmbMaritalStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (mcmbMaritalStatus.SelectedIndex == 2)
            {
                mtxtSpouseName.Enabled = true;
            }
            else 
            {
                mtxtSpouseName.Enabled = false;
            }
        }

        private void pbRemoveIdProof2_Click(object sender, EventArgs e)
        {
            try
            {
                bool empId2Exist = clsDocument.FileExistOrNot(mtxtCode.Text + "_2" + ".jpeg", DocumentType.EmployeeIdentityProof2);
                if (empId2Exist)
                    clsDocument.DeleteImage(mtxtCode.Text + "_2" + ".jpeg", DocumentType.EmployeeIdentityProof2);
                pbIdProof2.Image = null;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void Minimize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }


    }
}
#endregion