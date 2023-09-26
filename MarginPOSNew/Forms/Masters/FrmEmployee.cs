using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
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

namespace MarginPOSNew.Forms.Masters
{
    public partial class FrmEmployee : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        ClsGeneral objGeneral = new ClsGeneral();
        ClsDocument clsDocument = new ClsDocument();
        public static long EmployeeId = 0;
        long BranchId = FrmDashboard.BranchId;
        Bitmap bitmap, bitmapId1, bitmapId2;
        long DepartmentId = 0;
        public static string empImgExt = "";
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
                    objSearch.Table = "  tblGeneralOptions G inner join tblGType GT on G.GTypeId=GT.GTypeId  ";
                    objSearch.WhereNdGroupBy = " where GT.GType='ICARDSERIES' and Caption like 'EMPLOYEE%' AND  G.GTypeSubText1 in('" + RcvdCardSeries + "') AND G.Status=0";
                    var CardCount = frmGridSearch.GetSearchGrid(objSearch);

                    if (Convert.ToInt64(CardCount["Id", 0].Value) > 0)
                    {
                        tmrRFID.Enabled = true;
                        return true;
                    }
                    MetroMessageBox.Show(this, " This card not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    tmrRFID.Enabled = true;
                    return false;

                }
                else
                {
                    MetroMessageBox.Show(this, "Not an employee card", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    tmrRFID.Enabled = true;
                    return false;
                }
            }

            MetroMessageBox.Show(this, "Invalid card series", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            tmrRFID.Enabled = true;
            return false;


        }
    
        public FrmEmployee()
        {
            InitializeComponent();
        }
        public bool IsSavable()
        {
            Control[] controllist = { mtxtName, mtxtCode, mtxtMobile, mcmbDepartmentId, mcmbDesignationId };
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            else if (IsEmployeeExist() == false)
            {
                MetroMessageBox.Show(this, "Employee already exist", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(mtxtEmail.Text) && objGeneral.CheckEmailId(this, mtxtEmail.Text) == false)
            {
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
                dmnSearch.WhereNdGroupBy = "Where EmployeeName='" + mtxtName.Text + "' and Status=0 and BranchId="+BranchId;
            else
                dmnSearch.WhereNdGroupBy = "Where EmployeeName='" + mtxtName.Text + "' and Status=0 and EmployeeId!='" + EmployeeId + "' and BranchId="+BranchId;

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
            var designationSource = bllSearchDes.GetSearchData("prdComboFill", dmnSearchDes);
            mcmbDesignationId.DataSource = designationSource;
            mcmbDesignationId.DisplayMember = "Value";
            mcmbDesignationId.ValueMember = "Id";

            //Payment Mode
            BllSearch<DmnComboBox> bllSearchPMode = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchPMode = new DmnSearch();
            dmnSearchPMode.Select = "GeneralId,Caption";
            dmnSearchPMode.Table = "tblGeneralOptions g " +
                           "left join tblGType gt on g.GTypeId =  gt.GTypeId";
            dmnSearchPMode.WhereNdGroupBy = " Where gt.GType='PAYMENT MODE' AND g.Status = 0 ";
            var paymentmodeSource = bllSearchPMode.GetSearchData("prdComboFill", dmnSearchPMode);
            mcmbPaymentMode.DataSource = paymentmodeSource;
            mcmbPaymentMode.DisplayMember = "Value";
            mcmbPaymentMode.ValueMember = "Id";
            mcmbPaymentMode.SelectedIndex = mcmbPaymentMode.FindString("CASH");

            //Weekly Off
            BllSearch<DmnComboBox> bllSearchWeeklyOff = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchWeeklyOff = new DmnSearch();
            dmnSearchWeeklyOff.Select = "GeneralId,Caption";
            dmnSearchWeeklyOff.Table = "tblGeneralOptions g " +
                           "left join tblGType gt on g.GTypeId =  gt.GTypeId";
            dmnSearchWeeklyOff.WhereNdGroupBy = " Where gt.GType='WEEK' AND g.Status = 0 ";
            var WeeklyOffSource = bllSearchWeeklyOff.GetSearchData("prdComboFill", dmnSearchWeeklyOff);
            mcmbWeeklyOff.DataSource = WeeklyOffSource;
            mcmbWeeklyOff.DisplayMember = "Value";
            mcmbWeeklyOff.ValueMember = "Id";

            //Employee Status
            BllSearch<DmnComboBox> bllSearchEStatus = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchEStatus = new DmnSearch();
            dmnSearchEStatus.Select = "GeneralId,Caption";
            dmnSearchEStatus.Table = "tblGeneralOptions g " +
                           "left join tblGType gt on g.GTypeId =  gt.GTypeId";
            dmnSearchEStatus.WhereNdGroupBy = " Where gt.GType='EMPLOYEE STATUS' AND g.Status = 0 ";
            var EStatusSource = bllSearchEStatus.GetSearchData("prdComboFill", dmnSearchEStatus);
            mcmbEmpStatus.DataSource = EStatusSource;
            mcmbEmpStatus.DisplayMember = "Value";
            mcmbEmpStatus.ValueMember = "Id";
            mcmbEmpStatus.SelectedIndex = mcmbEmpStatus.FindString("ACTIVE");

            //Bank
            BllSearch<DmnComboBox> bllSearchBank = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchBank = new DmnSearch();
            dmnSearchBank.Select = "GeneralId,Caption";
            dmnSearchBank.Table = "tblGeneralOptions g " +
                "left join tblGType gt on g.GTypeId =  gt.GTypeId";
            dmnSearchBank.WhereNdGroupBy = "Where gt.GType='BANK' AND g.Status = 0 ";
            var bankSource = bllSearchBank.GetSearchData("prdComboFill", dmnSearchBank);
            mcmbBank.DataSource = bankSource;
            mcmbBank.DisplayMember = "Value";
            mcmbBank.ValueMember = "Id";

            //Marital Status
            BllSearch<DmnComboBox> bllSearchMarital = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchMarital = new DmnSearch();
            dmnSearchMarital.Select = "GeneralId,Caption";
            dmnSearchMarital.Table = "tblGeneralOptions g " +
                "left join tblGType gt on g.GTypeId =  gt.GTypeId";
            dmnSearchMarital.WhereNdGroupBy = "Where gt.GType='MARITAL STATUS' AND g.Status = 0 ";
            var maritalSource = bllSearchMarital.GetSearchData("prdComboFill", dmnSearchMarital);
            mcmbMaritalStatus.DataSource = maritalSource;
            mcmbMaritalStatus.DisplayMember = "Value";
            mcmbMaritalStatus.ValueMember = "Id";
            mcmbMaritalStatus.SelectedIndex = mcmbMaritalStatus.FindString("SINGLE");

            //Identity Proof1
            BllSearch<DmnComboBox> bllSearchIdProof1 = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchIdProof1 = new DmnSearch();
            dmnSearchIdProof1.Select = "GeneralId,Caption";
            dmnSearchIdProof1.Table = "tblGeneralOptions g " +
                "inner join tblGType gt on g.GTypeId =  gt.GTypeId";
            dmnSearchIdProof1.WhereNdGroupBy = "Where gt.GType='DOCUMENTS' AND g.Status = 0 ";
            var IdProof1Source = bllSearchIdProof1.GetSearchData("prdComboFill", dmnSearchIdProof1);
            mcmbIdProof1.DataSource = IdProof1Source;
            mcmbIdProof1.DisplayMember = "Value";
            mcmbIdProof1.ValueMember = "Id";
            mcmbIdProof1.SelectedIndex = mcmbIdProof1.FindString("AADHAAR CARD");

            //Identity Proof2
            BllSearch<DmnComboBox> bllSearchIdProof2 = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchIdProof2 = new DmnSearch();
            dmnSearchIdProof2.Select = "GeneralId,Caption";
            dmnSearchIdProof2.Table = "tblGeneralOptions g " +
                "inner join tblGType gt on g.GTypeId =  gt.GTypeId";
            dmnSearchIdProof2.WhereNdGroupBy = "Where gt.GType='DOCUMENTS' AND g.Status = 0 ";
            var IdProof2Source = bllSearchIdProof2.GetSearchData("prdComboFill", dmnSearchIdProof2);
            mcmbIdProof2.DataSource = IdProof2Source;
            mcmbIdProof2.DisplayMember = "Value";
            mcmbIdProof2.ValueMember = "Id";
            mcmbIdProof2.SelectedIndex = mcmbIdProof2.FindString("AADHAAR CARD");

            //Payment Term
            BllSearch<DmnComboBox> bllSearchPaymentTerm = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchPaymentTerm = new DmnSearch();
            dmnSearchPaymentTerm.Select = "GeneralId,Caption";
            dmnSearchPaymentTerm.Table = "tblGeneralOptions g " +
                "left join tblGType gt on g.GTypeId =  gt.GTypeId";
            dmnSearchPaymentTerm.WhereNdGroupBy = " Where gt.GType='PAYMENT TERM' AND g.Status = 0 ";
            var paymentTermSource = bllSearchPaymentTerm.GetSearchData("prdComboFill", dmnSearchPaymentTerm);
            mcmbPaymentTerm.DataSource = paymentTermSource;
            mcmbPaymentTerm.DisplayMember = "Value";
            mcmbPaymentTerm.ValueMember = "Id";

            //Late Affect
            BllSearch<DmnComboBox> bllSearchLateAffect = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchLateAffect = new DmnSearch();
            dmnSearchLateAffect.Select = "GeneralId,Caption";
            dmnSearchLateAffect.Table = "tblGeneralOptions g " +
                           "left join tblGType gt on g.GTypeId =  gt.GTypeId";
            dmnSearchLateAffect.WhereNdGroupBy = " Where gt.GType='LATE AFFECT' AND g.Status = 0 ";
            var LateAffectSource = bllSearchLateAffect.GetSearchData("prdComboFill", dmnSearchLateAffect);
            mcmbLateAffect.DataSource = LateAffectSource;
            mcmbLateAffect.DisplayMember = "Value";
            mcmbLateAffect.ValueMember = "Id";
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
            pbIdProof1.Image = null;
            pbIdProof2.Image = null;
            mtxtMaxLate.Text = "1";
            mtxtMonthlyLeave.Text = "1";
            dtpTimeIn.Text = "09:00:00 PM";
            dtpTimeOut.Text = "06:00:00 AM";
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
            if(string.IsNullOrWhiteSpace(mtxtName.Text))
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

        #region Event
        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshForm();
                //Activate Tab Controls (Had issue when retrieving date picker values from official details tab)----------
                mtEmployeeDetails.SelectedIndex = tabOfficial;
                mtEmployeeDetails.SelectedIndex = tabPersonal;
                //--------------------------------------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
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
                    //if (mrbtnMale.Checked == true)
                    //    dmnEmployee.Gender = "Male";
                    //else
                    //    dmnEmployee.Gender = "Female";a
                    if (mrbtnMale.Checked == true)
                        dmnEmployee.Gender = 1;
                    else
                        dmnEmployee.Gender = 2;
                    dmnEmployee.MaritalStatus = mcmbMaritalStatus.Text;
                    dmnEmployee.EmployeeStatus = mcmbEmpStatus.Text;
                   // dmnEmployee.Cast = mtxtCast.Text;
                    dmnEmployee.SpouseName = mtxtSpouseName.Text;
                    dmnEmployee.PresentAddress = mtxtPresentAddress.Text;
                    dmnEmployee.PaymentMode = mcmbPaymentMode.Text;
                    dmnEmployee.BankId = Convert.ToInt64(mcmbBank.SelectedValue);
                    dmnEmployee.BankBranch = mtxtBankBranch.Text;
                    dmnEmployee.BankAccountNo = mtxtBankAcountNo.Text;
                    dmnEmployee.BankIFSC = mtxtBankIFSC.Text;
                    dmnEmployee.PFNo = mtxtPFNo.Text;
                    dmnEmployee.ESINo = mtxtESINo.Text;
                    dmnEmployee.PFDate = Convert.ToDateTime(mdtPFDate.Text);
                    dmnEmployee.ESIDate = Convert.ToDateTime(mdtESIDate.Text);
                    dmnEmployee.IdentityProof1 = mcmbIdProof1.Text;
                    dmnEmployee.IdentityProof1No = mtxtIdProof1No.Text;
                    dmnEmployee.IdentityProof2 = mcmbIdProof2.Text;
                    dmnEmployee.IdentityProof2No = mtxtIdProof2No.Text;
                    dmnEmployee.Experience = mtxtExperience.Text;
                    dmnEmployee.TotalExperience = decimal.Parse(mtxtTotalExperience.Text == string.Empty ? "0" : mtxtTotalExperience.Text);
                    dmnEmployee.WeeklyOff = Convert.ToString(mcmbWeeklyOff.Text);
                   // dmnEmployee.MonthlyLeave = int.Parse(mtxtMonthlyLeave.Text == string.Empty ? "0" : mtxtMonthlyLeave.Text);
                    dmnEmployee.BalanceLeave = decimal.Parse(mtxtBalanceLeave.Text == string.Empty ? "0" : mtxtBalanceLeave.Text);
                    dmnEmployee.TimeIn = dtpTimeIn.Value;
                    dmnEmployee.TimeOut = dtpTimeOut.Value;
                    dmnEmployee.MaxLate = int.Parse(mtxtMaxLate.Text == string.Empty ? "0" : mtxtMaxLate.Text);
                    dmnEmployee.LateAffect = mcmbLateAffect.Text;
                    dmnEmployee.OverTimeStart = decimal.Parse(mtxtOverTimeStart.Text == string.Empty ? "0" : mtxtOverTimeStart.Text);
                    dmnEmployee.DateOfJoin = Convert.ToDateTime(mdtDateOfJoin.Text);
                    dmnEmployee.DateOfResign = Convert.ToDateTime(mdtDateOfResign.Text);
                    dmnEmployee.ResignReason = mtxtResignReason.Text;
                    dmnEmployee.Salary = decimal.Parse(mtxtSalary.Text == string.Empty ? "0.00" : mtxtSalary.Text);
                    dmnEmployee.PaymentTerm = mcmbPaymentTerm.Text;
                    dmnEmployee.Remarks = mtxtRemarks.Text;
                    dmnEmployee.ICardNo = mtxtICardNo.Text;
                    dmnEmployee.BranchId = BranchId;
                    dmnEmployee.EmployeeCode = mtxtCode.Text;
                    if (pbEmployee.Image != null)
                    {
                        dmnEmployee.Photo = mtxtCode.Text + ".jpeg";
                        bitmap = new Bitmap(pbEmployee.Image);
                    }
                    else
                    {
                        imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + ".jpeg", DocumentType.EmployeePhoto);
                        if (imageExist)
                            clsDocument.DeleteImage(mtxtCode.Text + ".jpeg", DocumentType.EmployeePhoto);
                        dmnEmployee.Photo = "";
                    }
                    dmnEmployee.IdentityProof1Atch = mtxtCode.Text + "_1" + ".jpeg";
                    dmnEmployee.IdentityProof2Atch = mtxtCode.Text + "_2" + ".jpeg";

                    if (EmployeeId == 0)
                    {
                        dmnEmployee.Status = 0;
                        GenerateCode();
                        dmnEmployee.EmployeeCode = mtxtCode.Text;
                        dmnEmployee.CreatedBy = FrmDashboard.UserId;
                        var saveStatus = bllEmployee.AddData("prdInsertEmployee", dmnEmployee);
                        EmployeeId = saveStatus.EmployeeId;
                        if (!string.IsNullOrWhiteSpace(saveStatus.EmployeeCode))
                        {
                            if (pbEmployee.Image != null)
                                clsDocument.UploadImage(bitmap, saveStatus.EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                            if (pbIdProof1.Image != null)
                                clsDocument.UploadImage(bitmapId1, saveStatus.EmployeeCode + "_1" + ".jpeg", DocumentType.EmployeeIdentityProof1);
                            if (pbIdProof2.Image != null)
                                clsDocument.UploadImage(bitmapId2, saveStatus.EmployeeCode + "_2" + ".jpeg", DocumentType.EmployeeIdentityProof2);
                            DialogResult dialogResult = MetroMessageBox.Show(this, "Do you want to give login privilege to " + mtxtName.Text +"?"," ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if(dialogResult == DialogResult.Yes)
                            {
                                MarginPOSNew.Forms.UserAccounts.FrmUserAccount frmUserAccount = new UserAccounts.FrmUserAccount();
                                frmUserAccount.ShowDialog();
                            }
                            MetroMessageBox.Show(this, "Saved successfully " + mtxtName.Text, "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    else
                    {
                        dmnEmployee.EmployeeId = EmployeeId;
                        dmnEmployee.ModifiedBy = FrmDashboard.UserId;
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
                            if (pbEmployee.Image != null)
                            {
                                if (mtxtCode.Text != EmployeeCode)
                                {
                                    imageExist = clsDocument.FileExistOrNot(EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                                    if (imageExist)
                                        clsDocument.DeleteImage(EmployeeCode + ".jpeg", DocumentType.EmployeePhoto);
                                }
                                imageExist = false;
                                clsDocument.UploadImage(bitmap, mtxtCode.Text + ".jpeg", DocumentType.EmployeePhoto);
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
                _objSearch.WhereNdGroupBy = "Where emp.Status=0 and emp.BranchId ="+BranchId;
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
                    objSearch.Select = "EmployeeId,EmployeeCode,EmployeeName,GuardianName,Qualification,DepartmentId,DesignationId,EmployeeAddress,PostalCode," +
                        " DOB,Telephone,Mobile,Email,Gender,MaritalStatus,EmployeeStatus,Cast,SpouseName,PresentAddress,ICardNo,IdentityProof1,IdentityProof2,IdentityProof1No," +
                        " IdentityProof2No,IdentityProof1Atch,IdentityProof2Atch,PaymentMode,BankId,BankBranch,BankAccountNo,BankIFSC,PFNo,PFDate,ESINo,ESIDate," +
                        " Experience,TotalExperience,WeeklyOff,MonthlyLeave,BalanceLeave,CONVERT(nvarchar(MAX),TimeIn,108) as TimeIn,CONVERT(nvarchar(MAX),TimeOut,108) as TimeOut,MaxLate,LateAffect," +
                        " CONVERT(nvarchar(MAX),OverTimeStart,108) as OverTimeStart,DateOfJoin,DateOfResign,ResignReason,Salary,PaymentTerm,Remarks,Photo";
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
                   // mtxtCast.Text = gridSource[0].Cast;
                    mtxtSpouseName.Text = gridSource[0].SpouseName;
                    mtxtPermenantAddress.Text = gridSource[0].PresentAddress;
                    mtxtICardNo.Text = gridSource[0].ICardNo;
                    mcmbIdProof1.Text = gridSource[0].IdentityProof1;
                    mtxtIdProof1No.Text = gridSource[0].IdentityProof1No;
                    mcmbIdProof2.Text = gridSource[0].IdentityProof2;
                    mtxtIdProof2No.Text = gridSource[0].IdentityProof2No;
                    mcmbPaymentMode.Text = gridSource[0].PaymentMode;
                    mcmbBank.SelectedValue = gridSource[0].BankId;
                    mtxtBankBranch.Text = gridSource[0].BankBranch;
                    mtxtBankAcountNo.Text = gridSource[0].BankAccountNo;
                    mtxtBankIFSC.Text = gridSource[0].BankIFSC;
                    mtxtPFNo.Text = Convert.ToString(gridSource[0].PFNo);
                    mdtPFDate.Text = Convert.ToString(gridSource[0].PFDate);
                    mtxtESINo.Text = Convert.ToString(gridSource[0].ESINo);
                    mdtESIDate.Text = Convert.ToString(gridSource[0].ESIDate);
                    mtxtExperience.Text = Convert.ToString(gridSource[0].Experience);
                    mtxtTotalExperience.Text = Convert.ToString(gridSource[0].TotalExperience);
                    mcmbWeeklyOff.Text = Convert.ToString(gridSource[0].WeeklyOff);
                    mtxtMonthlyLeave.Text = Convert.ToString(gridSource[0].MonthlyLeave);
                    mtxtBalanceLeave.Text = Convert.ToString(gridSource[0].BalanceLeave);
                    dtpTimeIn.Value = gridSource[0].TimeIn;
                    dtpTimeOut.Value = gridSource[0].TimeOut;
                    mtxtMaxLate.Text = Convert.ToString(gridSource[0].MaxLate);
                    mcmbLateAffect.Text = gridSource[0].LateAffect;
                    mtxtOverTimeStart.Text = Convert.ToString(gridSource[0].OverTimeStart);
                    mdtDateOfJoin.Text = Convert.ToString(gridSource[0].DateOfJoin);
                    mdtDateOfResign.Text = Convert.ToString(gridSource[0].DateOfResign);
                    mtxtResignReason.Text = gridSource[0].ResignReason;
                    mtxtSalary.Text = Convert.ToString(gridSource[0].Salary);
                    mcmbPaymentTerm.Text = gridSource[0].PaymentTerm;
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
                DialogResult dialogResult = MetroMessageBox.Show(this, "Are you sure you want to delete " + " ? ", "Delete ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    BllEmployee bllEmployee = new BllEmployee();
                    DmnEmployee dmnEmployee = new DmnEmployee();
                    dmnEmployee.EmployeeId = EmployeeId;
                    dmnEmployee.ModifiedBy = FrmDashboard.UserId;
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

                        MetroMessageBox.Show(this, "Deleted successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                if (e.KeyCode == Keys.F2 && mbtnSave.Enabled==true)
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
                    if (mtEmployeeDetails.SelectedTab ==mtEmployeeDetails.TabPages["mpEarnDeduction"])
                    {
                        if (EmployeeId > 0)
                        {
                            FrmEmployeeEarnDedu frmEmployeeEarnDedu = new FrmEmployeeEarnDedu();
                            frmEmployeeEarnDedu.ShowDialog();
                        }
                        else
                            MetroMessageBox.Show(this, "\n" + "Please select an employee ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    empImgExt = Path.GetExtension(openFileDialog.FileName);
                    pbEmployee.Image = Image.FromFile(openFileDialog.FileName);

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
                
                if (e.KeyCode == Keys.F2 && mbtnSave.Enabled==true)
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
            try
            {
                pnlRFID.Visible = true;
                #region RFID
                
                try
                {
                
                }
                catch (Exception ex)
                {
                    //  pnlRFID.Visible = false;
                    if (ex.Message.StartsWith("Index"))
                    {

                        MetroMessageBox.Show(this, "\nInstall proper driver first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    }
                    else
                    {
                        MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    }
                }
                #endregion
                tmrRFID.Enabled = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
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
                if (Convert.ToInt32( mcmbDesignationId.SelectedIndex)>0)
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
            catch(Exception ex)
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
        private void pbAddIdProof2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "|*.jpeg;*.jpg;*.png;*.gif;";
                DialogResult dialogResult = openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    pbIdProof2.Image = Image.FromFile(openFileDialog.FileName);
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
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
        #endregion
    }
}
