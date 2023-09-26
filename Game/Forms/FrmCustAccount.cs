using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Forms.VisualStyles;

namespace pLocker.Forms
{
    public partial class FrmCustAccount : Form
    {
        long BranchId = 1;// Forms.frmLogin.BranchId;//16
        long UserId = 1;// Forms.frmLogin.UserId;//1
        //long FinancialYearId = Convert.ToInt64(FrmDashBoard.FinancialYearId);//10

        string _retValue;
        long AccID;

        int LedgerId;
        public FrmCustAccount()
        {
            InitializeComponent();
        }
        private void FrmCustAccount_Load(object sender, EventArgs e)
        {
            lblToolTipAccName.Visible = false;
            txtAccName.Focus();
            FillCombo();
            Refreshform();
            
        }
        public void FillCombo()
        {
           // BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            // ============ Country
            BllSearch<DmnComboBox> bllSearch_Country = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "GTypeSubId,Caption";
            objSearch.Table = " tblGeneralOptions";
            objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=42";
            objSearch.OrderBy = "order by value";
            var gridSource = bllSearch_Country.GetSearchData("prdComboFill", objSearch);
            cboCountry.DataSource = gridSource;
            cboCountry.ValueMember = "Id";
            cboCountry.DisplayMember = "Value";
            cboCountry.SelectedIndex = 0;

            // ============state

            BllSearch<DmnComboBox> bllSearch_State = new BllSearch<DmnComboBox>();
            objSearch = new DmnSearch();
            objSearch.Select = "GTypeSubId,Caption";
            objSearch.Table = " tblGeneralOptions";
            objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=43";
            objSearch.OrderBy = "order by value";
            gridSource = bllSearch_State.GetSearchData("prdComboFill", objSearch);
            cboState.DataSource = gridSource;
            cboState.ValueMember = "Id";
            cboState.DisplayMember = "Value";
            cboState.SelectedIndex = 0;

            //============= City

            BllSearch<DmnComboBox> bllSearch_City = new BllSearch<DmnComboBox>();
            objSearch = new DmnSearch();
            objSearch.Select = "GTypeSubId,Caption";
            objSearch.Table = " tblGeneralOptions";
            objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=44";
            objSearch.OrderBy = "order by value";
            gridSource = bllSearch_City.GetSearchData("prdComboFill", objSearch);
            cboCity.DataSource = gridSource;
            cboCity.ValueMember = "Id";
            cboCity.DisplayMember = "Value";
            cboCity.SelectedIndex = 0;

            // ============ Gender

            BllSearch<DmnComboBox> bllSearch_HireFor = new BllSearch<DmnComboBox>();
            objSearch = new DmnSearch();
            objSearch.Select = "GTypeSubId,Caption";
            objSearch.Table = " tblGeneralOptions";
            objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=25";
            objSearch.OrderBy = "order by value";
            gridSource = bllSearch_HireFor.GetSearchData("prdComboFill", objSearch);
            cboGender.DataSource = gridSource;
            cboGender.ValueMember = "Id";
            cboGender.DisplayMember = "Value";
            cboGender.SelectedIndex = 0;


            // ============ Proof
            BllSearch<DmnComboBox> bllSearch_Proof = new BllSearch<DmnComboBox>();
            objSearch = new DmnSearch();

            objSearch.Select  = "GeneralId,Caption";
            objSearch.Table = " tblGeneralOptions";
            objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=6";
            objSearch.OrderBy = "order by value";
            gridSource = bllSearch_Proof.GetSearchData("prdComboFill", objSearch);
            cboProof.DataSource = gridSource;
            cboProof.ValueMember = "Id";
            cboProof.DisplayMember = "Value";
            cboProof.SelectedIndex = 0;
            // ============ Proof

        }
        private void Refreshform()
        {
            lblToolTipAccName.Visible = false;
            cboGender.SelectedIndex = 1;
            cboProof.SelectedIndex = 1;

            txtLedgerID.Text = "";
            txtAccNO.Text = "";
            txtAccName.Text = "";
            txtAccAddress.Text = "";
            cboGender.Text = "select";
            dtpBirthDate.Value = DateTime.Now;
            txtAge.Text = "";
            txtInstituteName.Text = "";
            txtOccupation.Text = "";
            txtAccPhone.Text = "";
            txtContEmgcy.Text = "";
            cboCountry.Text = "select";
            cboState.Text = "select";
            cboCity.Text = "select";
            txtAccEmail.Text = "";
            cboProof.Text = "select";
            txtProofNo.Text = "";
            txtRemarks.Text = "";
           

            FillCombo();

        }
        private bool Isvalid()
        {

            //if (string.IsNullOrWhiteSpace(txtAccNO.Text.Trim()))
            //{
            //    MessageBox.Show("Account No must be specified", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //    txtAccNO.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrWhiteSpace(txtAccName.Text.Trim()))
            //{
            //    MessageBox.Show("Account must be specified", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //    txtAccName.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrWhiteSpace(txtAccPhone.Text.Trim()))
            //{
            //    MessageBox.Show("Phone No.must be specified", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //    txtAccPhone.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrWhiteSpace(txtCustomerPIN.Text.Trim()))
            //{
            //    MessageBox.Show("Locker Pin must be specified", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //    txtCustomerPIN.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrWhiteSpace(txtHireForNo.Text.Trim()))
            //{
            //    MessageBox.Show("Hire for No's must be specified", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //    txtHireForNo.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrWhiteSpace(txtAmount.Text.Trim()))
            //{
            //    MessageBox.Show("Amount must be specified", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //    txtAmount.Focus();
            //    return false;
            //}
            //else if (Convert.ToInt32(cboHireFor.SelectedValue)==0)
            //{
            //    MessageBox.Show("Hire for Hours/Days/Month must be specified", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //    cboHireFor.Focus();
            //    return false;
            //}

            //else
            //{
                return true;
            //}
        }


        public void Insert_Combo1(string caption,int GtypeId)
        {
            try
            {
                DmnGeneralOptions dmnGen = new DmnGeneralOptions();
                BllGeneralOptions bllGen = new BllGeneralOptions();

                dmnGen.Caption = caption;
                dmnGen.GTypeId = GtypeId;
               
                bool insertStatus = bllGen.AddComboData("prdComboFillCountry", dmnGen);
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        public void Insert_Combo2(string caption, int GtypeId)
        {
            try
            {
                DmnGeneralOptions dmnGen = new DmnGeneralOptions();
                BllGeneralOptions bllGen = new BllGeneralOptions();

                dmnGen.Caption = caption;
                dmnGen.GTypeId = GtypeId;

                bool insertStatus = bllGen.AddComboData("prdComboFillCountry", dmnGen);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }


        public void Insert_Combo3(string caption, int GtypeId)
        {
            try
            {
                DmnGeneralOptions dmnGen = new DmnGeneralOptions();
                BllGeneralOptions bllGen = new BllGeneralOptions();

                dmnGen.Caption = caption;
                dmnGen.GTypeId = GtypeId;

                bool insertStatus = bllGen.AddComboData("prdComboFillCountry", dmnGen);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }





        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Isvalid() == false)
                    return;

                DmnCustAccount dmnCustAccount = new DmnCustAccount();
                BllCustAccount bllCustAccount = new BllCustAccount();

                if (string.IsNullOrWhiteSpace(txtLedgerID.Text))
                    txtLedgerID.Text = "0";

                dmnCustAccount.AccID = Convert.ToInt32(txtLedgerID.Text);
                dmnCustAccount.DOB = Convert.ToDateTime(dtpBirthDate.Text);
                dmnCustAccount.Gender = Convert.ToInt32(cboGender.SelectedValue);
                dmnCustAccount.Proof = Convert.ToInt32(cboProof.SelectedValue);
                dmnCustAccount.ProofNo = Convert.ToString(txtProofNo.Text);
                dmnCustAccount.AccNo = Convert.ToString(txtAccNO.Text);
                dmnCustAccount.AccName = Convert.ToString(txtAccName.Text);
                dmnCustAccount.AccPhone = Convert.ToString(txtAccPhone.Text);
                dmnCustAccount.AccEmail = Convert.ToString(txtAccEmail.Text);
                dmnCustAccount.AccAddress = Convert.ToString(txtAccAddress.Text);
                dmnCustAccount.Remark = Convert.ToString(txtRemarks.Text);


                if (string.IsNullOrWhiteSpace(txtAge.Text))
                                       txtAge.Text = "0";

                if (string.IsNullOrWhiteSpace(txtZipcode.Text))
                    txtZipcode.Text = "0";



                dmnCustAccount.Age = Convert.ToInt32(txtAge.Text);

                dmnCustAccount.EnrollDate = Convert.ToDateTime(dtpEntrollDate.Text);
                dmnCustAccount.CInstitute = Convert.ToString(txtInstituteName.Text);
                dmnCustAccount.Occupation = Convert.ToString(txtOccupation.Text);
                dmnCustAccount.ZipCode = Convert.ToInt32(txtZipcode.Text);
                dmnCustAccount.cEmergency = Convert.ToString(txtContEmgcy.Text);
                dmnCustAccount.cCountry = Convert.ToString(cboCountry.SelectedValue);
                dmnCustAccount.State = Convert.ToString(cboState.SelectedValue);
                dmnCustAccount.City = Convert.ToString(cboCity.SelectedValue);


                if (chBAllowEmail.Checked == true)
                    dmnCustAccount.AllowEmail = 1;
                else
                    dmnCustAccount.AllowEmail = 0;

                if (chBAllowSms.Checked == true)
                    dmnCustAccount.AllowSMS = 1;
                else
                    dmnCustAccount.AllowSMS = 0;


                //  dmnCustAccount.Proofimg1 = Convert.ToString(.Text);           
                dmnCustAccount.CreatedBy = UserId;
                //dmnCustAccount.ModifiedBy = UserId;
                dmnCustAccount.BranchId = BranchId;
                dmnCustAccount.Status = 0;

                bool InsertStatus = bllCustAccount.AddEditData("prdInsert_Upd_CustAccount", dmnCustAccount);
                if (InsertStatus == true)
                {
                    if (dmnCustAccount.AccID > 0)
                    {
                        MessageBox.Show(this, txtAccName.Text + " detail Successfully updated ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        this.Close();
                    }
                    else
                        MessageBox.Show(this, " Successfully saved ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    Refreshform();
                }
                else
                {
                    MessageBox.Show(this, " Save failed ", " error  ", MessageBoxButtons.OK);
                }
            }
             catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        public void LedgerPopup()
        {
           
            string returnValue = string.Empty;
            DataTable dtColumnAlign = new DataTable();
            //dtColumnAlign.Columns.Add();
            //dtColumnAlign.Rows.Add("6", "R");
            //dtColumnAlign.Rows.Add("7", "R");
            //dtColumnAlign.Rows.Add("8", "R");
            //dtColumnAlign.Rows.Add("9", "R");
            //dtColumnAlign.Rows.Add("10", "R");
            DmnSearch _objSearch = new DmnSearch();
            _objSearch.ColumnHeaders = "Id,Name,Account no,Mobile,Email,Address,Valid To";
            _objSearch.Select = "L.LedgerId as Id,L.LedgerName as Field1,LedgerCode as Field2,L.Mobile as Field3,L.EmailId as Field4, L.LedgerAddress as Field5, L.vto as Field6";
            _objSearch.Table = " tblAccountLedger L LEFT JOIN tblAccountGroup Ac  ON L.AccountGroupId = Ac.AccountGroupId";
            _objSearch.WhereNdGroupBy = " WHERE isnull(L.Status,0) = 0 And L.cardtype=143 and  isnull(L.cardstatus,0)=1 and L.BranchId = " + BranchId;
            _objSearch.OrderBy = " ORDER BY L.LedgerName ASC";
            using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 100, (this.Height - 10), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))

            {
                var dialogResult = search.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    returnValue = Convert.ToString(search._retValue);
                }
            }
            if (returnValue.ToString() != "")
            {
                LedgerId = Convert.ToInt32(returnValue.ToString());
                txtLedgerID.Text= Convert.ToString(LedgerId);
                BllSearch<DmnAccountLedger> bllSearch = new BllSearch<DmnAccountLedger>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = " tblAccountLedger ";
                objSearch.WhereNdGroupBy = "Where LedgerId = " + LedgerId;
                objSearch.OrderBy = "Order By LedgerName ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                txtAccName.Text = Convert.ToString(gridSource[0].LedgerName);
                txtAccNO.Text = Convert.ToString(gridSource[0].LedgerCode);
                txtAccAddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                txtAccPhone.Text = Convert.ToString(gridSource[0].Mobile);
                txtAccEmail.Text = Convert.ToString(gridSource[0].EmailId);
               
            }
        }
        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        private void cboRateType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        private void txtHireFor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        private void txtExp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        private void FrmHireLocker_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Escape)
                        this.Close();
                else if (e.KeyData == Keys.F5)
                    Refreshform();

                //    btnSave_Click(sender, e);
            }
            catch (Exception ex)
            {
               MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        private void txtAccName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    e.SuppressKeyPress = true;
            //    System.Windows.Forms.SendKeys.Send("{TAB}");
            //}
            if (e.KeyCode == Keys.F8)
                LedgerPopup();
        }
        private void btnFrmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtAccName_Enter(object sender, EventArgs e)
        {
            lblToolTipAccName.Visible = true;

        }
        private void getNewAccNo()
        {
            int sel = 143;
            if (sel > 0)
            {
                BllSearch<DmnAccountGroup> bllSearch = new BllSearch<DmnAccountGroup>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "MAX(STUFF(LedgerCode, 1, PATINDEX('%[0-9]%', LedgerCode) - 1, '')) AS LedgerCode";
                objSearch.Table = " tblAccountLedger ";
                objSearch.WhereNdGroupBy = "Where cardtype = 143";
                //objSearch.OrderBy = "";

                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                // mtxtCode.Text = Convert.ToString(gridSource[0].AccountGroupCode) + Convert.ToString(gridSource[0].SrlNoCurrent + 1);
                long lc = Convert.ToInt64(gridSource[0].LedgerCode) + 1;

               txtAccNO.Text= Convert.ToString(lc);

            }
            else
            {

            }
        }
        private void txtAccName_Leave(object sender, EventArgs e)
        {
            lblToolTipAccName.Visible = false;

        }
        private void txtAccNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
                LedgerPopup();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            Process process = Process.Start(Application.StartupPath + "\\webcam\\webcamDemo.exe");
            int id = process.Id;
            Process tempProc = Process.GetProcessById(id);
            this.Visible = false;
            tempProc.WaitForExit();
            this.Visible = true;
        }

        private void cboCountry_Leave(object sender, EventArgs e)
        {
            if(cboCountry.Text!=null|| cboCountry.Text!="" )

            Insert_Combo1(cboCountry.Text.Trim().ToUpper(), 42);
        }

        private void cboState_Leave(object sender, EventArgs e)
        {
            if (cboState.Text!= null || cboState.Text!= "")

                Insert_Combo2(cboState.Text.Trim().ToUpper(), 43);
        }

        private void cboCity_Leave(object sender, EventArgs e)
        {
            if (cboCity.Text!= null || cboCity.Text!= "")

                Insert_Combo3(cboCity.Text.Trim().ToUpper(), 44);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LedgerPopup();
        }

        private void txtAccName_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtAccName.Text) == false && txtLedgerID.Text == "0" || string.IsNullOrWhiteSpace(txtLedgerID.Text))
            {
                getNewAccNo();
            }
        }

        private void btnAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void cboGender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void dtpBirthDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void txtAge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refreshform();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
