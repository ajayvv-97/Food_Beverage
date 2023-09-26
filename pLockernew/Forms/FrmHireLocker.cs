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

namespace pLocker.Forms
{
    public partial class FrmHireLocker : Form
    {
        long BranchId = Forms.frmLogin.BranchId;//16
        long UserId = Forms.frmLogin.UserId;//1
        long FinancialYearId = Convert.ToInt64(FrmDashBoard.FinancialYearId);//10

        string _retValue;
        long hLockerID;

        int LedgerId;
        public FrmHireLocker(long LockerID)
        {
            InitializeComponent();
            hLockerID = LockerID;
        }
        private void FrmHireLocker_Load(object sender, EventArgs e)
        {
            lblToolTipAccName.Visible = false;
            txtAccName.Focus();
            FillCombo();
            Refreshform();
            
            IsHired(hLockerID);
        }
        public void FillCombo()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            // ============ Locker
            DmnSearch objSearch = new DmnSearch();

            objSearch.Select = "Id,Locker";
            objSearch.Table = " tblLocker";
            objSearch.WhereNdGroupBy = "where BranchId=" + BranchId;
            objSearch.OrderBy = "order by value";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            cboLocker.DataSource = gridSource;
            cboLocker.ValueMember = "Id";
            cboLocker.DisplayMember = "Value";
            cboLocker.SelectedIndex = 0;
            // ============ Locker
            // ============ Locker Status

            BllSearch<DmnComboBox> bllSearch_LockerStatus = new BllSearch<DmnComboBox>();
            objSearch = new DmnSearch();

            objSearch.Select = "GTypeSubId,Caption";
            objSearch.Table = " tblGeneralOptions";
            objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=41";
            objSearch.OrderBy = "order by value";
            gridSource = bllSearch_LockerStatus.GetSearchData("prdComboFill", objSearch);
            cboLockStatus.DataSource = gridSource;
            cboLockStatus.ValueMember = "Id";
            cboLockStatus.DisplayMember = "Value";
            cboLockStatus.SelectedIndex = 1;
            // ============ Locker Status

            // ============ Hire for

            BllSearch<DmnComboBox> bllSearch_HireFor = new BllSearch<DmnComboBox>();
            objSearch = new DmnSearch();
            objSearch.Select = "GTypeSubId,Caption";
            objSearch.Table = " tblGeneralOptions";
            objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=28";
            objSearch.OrderBy = "order by value";
            gridSource = bllSearch_HireFor.GetSearchData("prdComboFill", objSearch);
            cboHireFor.DataSource = gridSource;
            cboHireFor.ValueMember = "Id";
            cboHireFor.DisplayMember = "Value";
            cboHireFor.SelectedIndex = 0;

            // ============ Hire for

                //BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                //DmnSearch objSearch = new DmnSearch();
                //objSearch = new DmnSearch();
                //objSearch.Select = "GTypeSubId, Caption";
                //objSearch.Table = " tblGeneralOptions";
                //objSearch.WhereNdGroupBy = " Where GtypeId = 28";
                //objSearch.OrderBy = " ORDER BY ID ASC";
                //var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                //cboHireFor.DataSource = gridSource;
                //cboHireFor.ValueMember = "Id";
                //cboHireFor.DisplayMember = "Value";
                //cboHireFor.SelectedIndex = cboHireFor.FindString(" --Select-- ");

        }
        private void Refreshform()
        {
            lblToolTipAccName.Visible = false;
            chkHired.Visible = false;
            cboLockStatus.SelectedIndex = 1;
            txtLedgerID.Text = "";
            txtLockHireID.Text = "";
            txtAccNO.Text = "";
            txtAccName.Text = "";
            txtAccPhone.Text = "";
            txtAccEmail.Text = "";
            txtAccAddress.Text = "";
            txtRemarks.Text = "";
            txtCustomerPIN.Text = "";
            txtHireForNo.Text = "0";
            txtAmount.Text = "0";
            lblRate.Text = "0";
            txtHireDetails.Text = "...";
            //dtpHireTime.Text = "09:00:00 PM";
            //dtpExpTime.Text = "06:00:00 AM";

            FillCombo();

        }
        public void IsHired(long hLockerID)
        {
            var dt=Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));

            BllSearch<DmnHireLocker> bllSearch = new BllSearch<DmnHireLocker>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "tblLockerHire.*,dbo.tblAccountLedger.LedgerName as AccName,dbo.tblAccountLedger.LedgerAddress as AccAddress";
            objSearch.Table = "tblLockerHire  INNER JOIN dbo.tblAccountLedger ON dbo.tblLockerHire.AccID = dbo.tblAccountLedger.LedgerId";
            //objSearch.WhereNdGroupBy = "Where LockID = " + hLockerID + " and EntryDate=(SELECT convert(varchar(10), Convert(date,'" + dt + "', 105),23)) and tblLockerHire.Status = 0 and tblLockerHire.BranchId=" + BranchId ;
            objSearch.WhereNdGroupBy = "Where LockID = " + hLockerID + "  and tblLockerHire.Status = 0 and tblLockerHire.BranchId=" + BranchId;
            //(SELECT convert(varchar(10), Convert(date,'" + mdtpfrom.Text + "', 105),23))
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            if (itemSource.Count > 0)
            {
                chkHired.Visible = true;

                txtLockHireID.Text= Convert.ToString(itemSource[0].LockHireID);
                cboLocker.SelectedValue = Convert.ToInt64(itemSource[0].LockID);
                txtLedgerID.Text = Convert.ToString(itemSource[0].AccID);
                txtAccNO.Text= Convert.ToString(itemSource[0].AccNo);
                txtAccName.Text = Convert.ToString(itemSource[0].AccName);
                txtAccAddress.Text = Convert.ToString(itemSource[0].AccAddress);
                txtAccPhone.Text = Convert.ToString(itemSource[0].AccPhone);
                txtAccEmail.Text = Convert.ToString(itemSource[0].AccEmail);
                txtCustomerPIN.Text = Convert.ToString(itemSource[0].CustomerPin);
                cboHireFor.SelectedValue = Convert.ToInt64(itemSource[0].HireFor);
                lblRate.Text = Convert.ToString(itemSource[0].Rate);
                txtAmount.Text = Convert.ToString(itemSource[0].Amt);
                dtpExpDate.Value = Convert.ToDateTime(itemSource[0].ExpDate);
                dtpExpTime.Value = Convert.ToDateTime(itemSource[0].ExpTime);
                dtpHireDate.Value = Convert.ToDateTime(itemSource[0].HireDate);
                dtpHireTime.Value = Convert.ToDateTime(itemSource[0].HireTime);
                cboLockStatus.SelectedValue = Convert.ToInt64(itemSource[0].LockOpen);
                txtRemarks.Text = Convert.ToString(itemSource[0].Inotes);
                txtHireDetails.Text = Convert.ToString(itemSource[0].Hiredetail);
                txtHireForNo.Text = Convert.ToString(itemSource[0].HireNo);

                if (Convert.ToInt32(itemSource[0].Hired) == 1)
                    chkHired.Checked = true;
                else
                    chkHired.Checked = false;

                //dtgjournaldet.Rows[i].Cells[idate].Value = gridSource[i].EntryDate.ToString();
                //dmnHireLocker.EntryDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            }
            else
            {
                cboLocker.SelectedValue = Convert.ToInt64(hLockerID);
                cboLockStatus.SelectedIndex = 1;

            }

        }
        private bool Isvalid()
        {

            if (string.IsNullOrWhiteSpace(txtAccNO.Text.Trim()))
            {
                MessageBox.Show("Account No must be specified", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtAccNO.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtAccName.Text.Trim()))
            {
                MessageBox.Show("Account must be specified", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtAccName.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtAccPhone.Text.Trim()))
            {
                MessageBox.Show("Phone No.must be specified", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtAccPhone.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtCustomerPIN.Text.Trim()))
            {
                MessageBox.Show("Locker Pin must be specified", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtCustomerPIN.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtHireForNo.Text.Trim()))
            {
                MessageBox.Show("Hire for No's must be specified", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtHireForNo.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtAmount.Text.Trim()))
            {
                MessageBox.Show("Amount must be specified", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtAmount.Focus();
                return false;
            }
            else if (Convert.ToInt32(cboHireFor.SelectedValue)==0)
            {
                MessageBox.Show("Hire for Hours/Days/Month must be specified", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                cboHireFor.Focus();
                return false;
            }

            else
            {
                return true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Isvalid() == false)
                    return;

                DmnHireLocker dmnHireLocker = new DmnHireLocker();
                BllHireLocker bllHireLocker = new BllHireLocker();
                //dmnPurchaseMaster.EntryDate = Convert.ToDateTime(mdtpInvDate.Text);
                dmnHireLocker.LockHireID= Convert.ToInt32(txtLockHireID.Text);
                dmnHireLocker.EntryDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                //dmnHireLocker.LockID = FrmDisplayLockers.locid;
                dmnHireLocker.LockID = Convert.ToInt32(cboLocker.SelectedValue);
                dmnHireLocker.AccID= Convert.ToInt32(txtLedgerID.Text);
                dmnHireLocker.AccNo = Convert.ToString(txtAccNO.Text);
                dmnHireLocker.AccPhone = Convert.ToString(txtAccPhone.Text);
                dmnHireLocker.AccEmail = Convert.ToString(txtAccEmail.Text);
                dmnHireLocker.CustomerPin = Convert.ToString(txtCustomerPIN.Text);
                dmnHireLocker.HireFor = Convert.ToInt32(cboHireFor.SelectedValue);
                dmnHireLocker.RateType = Convert.ToInt32(cboHireFor.SelectedValue);
                dmnHireLocker.Rate = Convert.ToDecimal(lblRate.Text);
                dmnHireLocker.Amt = Convert.ToDecimal(txtAmount.Text);
                dmnHireLocker.ExpDate = Convert.ToDateTime(dtpExpDate.Text);
                dmnHireLocker.ExpTime = Convert.ToDateTime(dtpExpTime.Text);
                dmnHireLocker.HireDate = Convert.ToDateTime(dtpHireDate.Text);
                dmnHireLocker.HireTime= Convert.ToDateTime(dtpHireTime.Text);
                dmnHireLocker.LockOpen = Convert.ToInt32(cboLockStatus.SelectedValue);
                dmnHireLocker.Inotes = Convert.ToString(txtRemarks.Text);
                dmnHireLocker.Hiredetail= Convert.ToString(txtHireDetails.Text);
                dmnHireLocker.HireNo= Convert.ToInt32(txtHireForNo.Text);
                if (chkHired.Checked == true)
                    dmnHireLocker.Hired = 1;
                else
                    dmnHireLocker.Hired = 0;

                dmnHireLocker.CreatedBy = UserId;
                //dmnHireLocker.CreatedDateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                dmnHireLocker.ModifiedBy = UserId;
                //dmnHireLocker.ModifiedDateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                dmnHireLocker.BranchId = BranchId;
                dmnHireLocker.Status = 0;

                bool InsertStatus = bllHireLocker.AddEditData("prdInsert_Upd_LockerHire", dmnHireLocker);
                if (InsertStatus == true)
                {
                    if (dmnHireLocker.LockHireID > 0)
                    {
                        MessageBox.Show(this, cboLocker.Text + " detail Successfully updated ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
        private void cboRateType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int n1 ;
            Decimal nRate;
            BllSearch<DmnLocker> bllSearch = new BllSearch<DmnLocker>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = "tblLocker";
            objSearch.WhereNdGroupBy = "Where ID = " + FrmDisplayLockers.locid ;
            //objSearch.OrderBy = "Order By I.ItemName ";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            if (itemSource.Count > 0)
            {



                nRate = 0;
                n1 = Convert.ToInt32(cboHireFor.SelectedValue);
                switch (n1)
                {
                    case 1:
                        nRate =Convert.ToDecimal(itemSource[0].HrRate);
                        // code block

                        break;
                    case 2:
                        nRate = Convert.ToDecimal(itemSource[0].DyRate);
                        // code block
                        break;
                    case 3:
                        // code
                        nRate = Convert.ToDecimal(itemSource[0].MnRate);
                        break;
                    default:
                        // code block
                        break;
                }
                lblRate.Text = Convert.ToString(nRate);
                //txtAmt.Text = Convert.ToString(nRate * Convert.ToDecimal(txtHireForNo.Text));

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
        private void GetAccDetails(string SearchIn,string SearchVal)
        {
            BllSearch<DmnAccountLedger> bllSearch = new BllSearch<DmnAccountLedger>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = " tblAccountLedger ";
            switch (Convert.ToString(SearchIn.ToUpper()))
            {
                case "EMAIL":
                    objSearch.WhereNdGroupBy = "Where EmailId='" + SearchVal + "' and isnull(L.Status,0) = 0 And L.cardtype=143 and  isnull(L.cardstatus,0)=1 and L.BranchId = " + BranchId;
                    break;

                case "ACCNO":
                    objSearch.WhereNdGroupBy = "Where LedgerCode='" + SearchVal + "' isnull(L.Status,0) = 0 And L.cardtype=143 and  isnull(L.cardstatus,0)=1 and L.BranchId = " + BranchId;
                    break;

                case "PHONE":
                    objSearch.WhereNdGroupBy = "Where Mobile='" + SearchVal + "' isnull(L.Status,0) = 0 And L.cardtype=143 and  isnull(L.cardstatus,0)=1 and L.BranchId = " + BranchId;
                    break;
                default:
                    objSearch.WhereNdGroupBy = "Where LedgerId = " + LedgerId;
                    break;
            }
            objSearch.OrderBy = "Order By LedgerName ";
            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
            if (gridSource.Count>0)
            {
                txtLedgerID.Text = Convert.ToString(gridSource[0].LedgerId);
                txtAccName.Text = Convert.ToString(gridSource[0].LedgerName);
                txtAccNO.Text = Convert.ToString(gridSource[0].LedgerCode);
                txtAccAddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                txtAccPhone.Text = Convert.ToString(gridSource[0].Mobile);
                txtAccEmail.Text = Convert.ToString(gridSource[0].EmailId);

            }
            else
            {
                MessageBox.Show(this, " No data found ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAccName.Text = "";
                txtAccNO.Text = "";
                txtAccAddress.Text = "";
                txtAccPhone.Text = "";
                txtAccEmail.Text = "";
                txtLedgerID.Text = "";

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
            if (e.KeyCode == Keys.F12)
                LedgerPopup();
            cboHireFor.Focus();
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
        private void txtAccName_Leave(object sender, EventArgs e)
        {
            lblToolTipAccName.Visible = false;
        }
        private void txtAccNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                LedgerPopup();
            cboHireFor.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Forms.FrmCustAccount frmCustAccount = new FrmCustAccount();
            frmCustAccount.Show();
            frmCustAccount.WindowState = FormWindowState.Normal;

        }
    }   
}
