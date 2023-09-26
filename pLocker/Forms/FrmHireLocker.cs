using BLL;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using CrystalDecisions.CrystalReports.Engine;
using pLocker.Interfaces;
using pLocker.Forms.UserControls;
using pLocker.Classes;

namespace pLocker.Forms
{
    public partial class FrmHireLocker : Form
    {
        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsGeneral = new ClsGeneral();
        long BranchId = Forms.frmLogin.BranchId;//16
        long UserId = Forms.frmLogin.UserId;//1
        long FinancialYearId = Convert.ToInt64(FrmLockerDashBoard.FinancialYearId);//10

        string _retValue;
        long hLockerID;
        int flag;
        int LedgerId;
        List<int> hLocCart = new List<int>();
        List<decimal> hLocCartPrice = new List<decimal>();
        public FrmHireLocker(long LockerID, int f)
        {
            InitializeComponent();
            hLockerID = LockerID;
            flag = f;
        }
        public FrmHireLocker(List<int> LocCart, int f)
        {
            InitializeComponent();
            hLocCart = LocCart;
            flag = f;
            dgvRfidList.Rows.Clear();
            pnlRfidList.Visible = true;            
        }
        private void FrmHireLocker_Load(object sender, EventArgs e)
        {
            //txtAccName.Focus();
            grpBoxPrint.Visible = false;
            lblToolTipAccName.Visible = true;
            Refreshform();
            if (hLocCart.Count == 0)
            {
                IsHired(hLockerID);
            }
            txtAccName.Focus();
            setprefix();
        }
        public void FillCombo()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            // ============ Locker 
            DmnSearch objSearch = new DmnSearch();
            if (hLocCart.Count == 0)
            {
                objSearch.Select = "Id,Locker";
                objSearch.Table = " tblLocker";
                objSearch.WhereNdGroupBy = "where BranchId=" + BranchId;
                objSearch.OrderBy = "order by value";
                var gridloc = bllSearch.GetSearchData("prdComboFill", objSearch);
                cboLocker.DataSource = gridloc;
                cboLocker.ValueMember = "Id";
                cboLocker.DisplayMember = "Value";
                // cboLocker.SelectedIndex = 0;
                cboLocker.SelectedValue = hLockerID;

                cboLocker.Enabled = false;

                // ============ Locker
            }
            else
            {
                string locid = hLocCart[0].ToString();
                for (int i = 1; i < hLocCart.Count; i++)
                {
                    locid += "," + hLocCart[i].ToString();
                }
                objSearch.Select = "Id,Locker";
                objSearch.Table = " tblLocker";
                objSearch.WhereNdGroupBy = "where BranchId=" + BranchId + " and Id in(" + locid + ")";
                objSearch.OrderBy = "order by value";
                var gridloc = bllSearch.GetSearchData("prdComboFill", objSearch);
                cboLocker.DataSource = gridloc;
                cboLocker.ValueMember = "Id";
                cboLocker.DisplayMember = "Value";
                cboLocker.SelectedIndex = 1;
                cboLocker.Enabled = true;
            }

            // ============ Locker Status

            BllSearch<DmnComboBox> bllSearch_LockerStatus = new BllSearch<DmnComboBox>();
            objSearch = new DmnSearch();

            objSearch.Select = "GTypeSubId,Caption";
            objSearch.Table = " tblGeneralOptions";
            objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=41";
            objSearch.OrderBy = "order by value";
            var gridSource = bllSearch_LockerStatus.GetSearchData("prdComboFill", objSearch);
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
            objSearch.OrderBy = "order by Id";
            gridSource = bllSearch_HireFor.GetSearchData("prdComboFill", objSearch);
            cboHireFor.DataSource = gridSource;
            cboHireFor.ValueMember = "Id";
            cboHireFor.DisplayMember = "Value";
            //cboHireFor.SelectedIndex = 0;

            //Payment Mode
            BllSearch<DmnComboBox> bllSearchPMode1 = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchPMode1 = new DmnSearch();
            dmnSearchPMode1.Select = "GTypeSubId,Caption";
            dmnSearchPMode1.Table = "tblGeneralOptions g ";
            dmnSearchPMode1.WhereNdGroupBy = "Where g.GTypeId=2 AND g.Status = 0 ";
            dmnSearchPMode1.OrderBy = "order by Value";
            var paymentmodeSource = bllSearchPMode1.GetSearchData("prdComboFill", dmnSearchPMode1);

            cmbPaymentMode.DataSource = paymentmodeSource;
            cmbPaymentMode.DisplayMember = "Value";
            cmbPaymentMode.ValueMember = "Id";
            cmbPaymentMode.SelectedIndex = cmbPaymentMode.FindString("CASH");


        }
        private void Refreshform()
        {
            FillCombo();
            lblToolTipAccName.Visible = false;
            chkHired.Visible = true;
            cboLockStatus.SelectedIndex = 1;
            txtLedgerID.Text = "0";
            txtLockHireID.Text = "";
            txtAccNO.Text = "";
            txtAccName.Text = "";
            txtAccPhone.Text = "";
            txtAccEmail.Text = "";
            txtAccAddress.Text = "";
            txtRemarks.Text = "";
            txtCustomerPIN.Text = "";
            txtHireForNo.Text = "1";
            txtAmount.Text = "0";
            lblRate.Text = "0";
            txtHireDetails.Text = "...";
            txtNetAmt.Text = Convert.ToString("0.00");
            txtPaidAmt.Text = Convert.ToString("0.00");
            txtBalAmt.Text = Convert.ToString("0.00");
            txtAmtAdjust.Text = Convert.ToString("0.00");
            txtDiscountAmt.Text = Convert.ToString("0.00");
            //dtpHireTime.Text = "09:00:00 PM";
            //dtpExpTime.Text = "06:00:00 AM";
            //if (System.Windows.Forms.Application.OpenForms["FrmDisplayLockers"] != null)
            //{
            //    FrmDisplayLockers FrmDisLock = Application.OpenForms["FrmDisplayLockers"] as FrmDisplayLockers;
            //    FrmDisLock.FillComboLocker(0);
            //}
            for (int i = 0; i < hLocCart.Count(); i++)
            {
                dgvRfidList.Rows.Add(1);
                dgvRfidList.Rows[i].Cells[0].Value = i + 1;
            }
        }
        public void IsHired(long hLockerID)
        {
            txtHireDetails.Text = "Locker" + hLockerID + " is Hire for " + txtHireForNo.Text + cboHireFor.Text;
            //var dt = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
            chkHired.Visible = true;
            BllSearch<DmnHireLocker> bllSearch = new BllSearch<DmnHireLocker>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "tblLockerHire.*,dbo.tblAccountLedger.LedgerName as AccName,dbo.tblAccountLedger.LedgerAddress as AccAddress,tblAccountLedger.LedgerId";
            objSearch.Table = "tblLockerHire  INNER JOIN dbo.tblAccountLedger ON dbo.tblLockerHire.AccID = dbo.tblAccountLedger.LedgerId";
            //objSearch.WhereNdGroupBy = "Where LockID = " + hLockerID + " and EntryDate=(SELECT convert(varchar(10), Convert(date,'" + dt + "', 105),23)) and tblLockerHire.Status = 0 and tblLockerHire.BranchId=" + BranchId ;
            if (flag == 0)
                objSearch.WhereNdGroupBy = "Where LockID = " + hLockerID + "  and tblLockerHire.Status = 1 and LockOpen=1 and tblLockerHire.BranchId=" + BranchId;
            else
                objSearch.WhereNdGroupBy = "Where LockID = " + hLockerID + "  and tblLockerHire.Status = 1 and Hired =1 and  tblLockerHire.BranchId=" + BranchId;
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            if (itemSource.Count > 0)
            {

                cboLockStatus.SelectedValue = Convert.ToInt64(itemSource[0].LockOpen);
                if (Convert.ToInt32(cboLockStatus.SelectedValue) == 2)
                    chkHired.Checked = false;
                else
                    chkHired.Checked = true;

                txtLockHireID.Text = Convert.ToString(itemSource[0].LockHireID);
                cboLocker.SelectedValue = Convert.ToInt64(itemSource[0].LockID);
                txtLedgerID.Text = Convert.ToString(itemSource[0].AccID);
                txtAccNO.Text = Convert.ToString(itemSource[0].AccNo);
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
                txtRemarks.Text = Convert.ToString(itemSource[0].Inotes);
                txtHireDetails.Text = Convert.ToString(itemSource[0].Hiredetail);
                txtHireForNo.Text = Convert.ToString(itemSource[0].HireNo);
                if (itemSource[0].ICardNo != "" && itemSource[0].ICardNo != null)
                {
                    string str1 = Convert.ToString(itemSource[0].ICardNo);
                    txtICardNo.Text = str1;
                    string s = "XXXX XX" + str1.Substring(str1.Length - 6, 6);
                    txtICardNo_2.Text = s;
                }
                txtDiscountAmt.Text = Convert.ToString(itemSource[0].DiscountAmount);
                txtAmtAdjust.Text = Convert.ToString(itemSource[0].AmountAdjust);
                txtNetAmt.Text = Convert.ToString(itemSource[0].NetAmount);
                cmbPaymentMode.SelectedValue = Convert.ToInt64(itemSource[0].PaymentMode);
                txtPaidAmt.Text = Convert.ToString(itemSource[0].PaidAmount);
                txtBalAmt.Text = Convert.ToString(itemSource[0].BalanceAmount);

                bool imageExist = clsDocument.FileExistOrNot(itemSource[0].LedgerId + ".jpeg", DocumentType.Locker);
                if (imageExist)
                    pbCapture.BackgroundImage = clsDocument.DownloadImage(itemSource[0].LedgerId + ".jpeg", DocumentType.Locker);
                pbCapture.BackgroundImageLayout = ImageLayout.Stretch;

                foreach (Control c in panel1.Controls)
                {
                    c.Enabled = false;
                }
                chkHired.Enabled = true;
                btnSave.Enabled = true;
                btnClose.Enabled = true;
                btnRefresh.Enabled = true;
                btnMin.Enabled = true;
                btnFrmClose.Enabled = true;
            }
            else
            {

                cboLockStatus.SelectedIndex = 2;
                chkHired.Checked = true;

            }

        }
        private bool Isvalid()
        {
            if (string.IsNullOrWhiteSpace(txtAccName.Text.Trim()))
            {
                MetroMessageBox.Show(this, "", "Account must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtAccName.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtAccPhone.Text.Trim()))
            {
                MetroMessageBox.Show(this, "", "Phone No.must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtAccPhone.Focus();
                return false;
            }
            else if (Convert.ToInt32(txtAccPhone.Text.Count()) < 10)
            {
                MetroMessageBox.Show(this, "", "Invalid Phone Number ", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtAccPhone.Focus();
                return false;
            }
			else if (Convert.ToInt32(cboHireFor.SelectedIndex)<1)
			{
				MetroMessageBox.Show(this, "", "Locker hire for must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
				cboHireFor.Focus();
				return false;
			}
			else if (string.IsNullOrWhiteSpace(txtHireForNo.Text.Trim()))
            {
                MetroMessageBox.Show(this, "", "Hire for No's must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtHireForNo.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtAmount.Text.Trim()))
            {
                MetroMessageBox.Show(this, "", "Amount must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtAmount.Focus();
                return false;
            }
            else if (Convert.ToInt32(cboHireFor.SelectedValue) == 0)
            {
                MetroMessageBox.Show(this, "", "Hire for Hours/Days/Month must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                cboHireFor.Focus();
                return false;
            }
            else if (Convert.ToInt32(cmbPaymentMode.SelectedValue) == 0)
            {
                MetroMessageBox.Show(this, "", "Payment mode must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                cmbPaymentMode.Focus();
                return false;
            }
            else if (Convert.ToInt32(cmbPaymentMode.SelectedValue) == 1 && Convert.ToDecimal(txtPaidAmt.Text)<=0)
            {
                MetroMessageBox.Show(this, "", "PaidAmount mode must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                cmbPaymentMode.Focus();
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(txtAccEmail.Text) && clsGeneral.CheckEmailId(this, txtAccEmail.Text) == false)
            {
                return false;
            }
            else if (txtHireForNo.Text == "" || Convert.ToInt32(txtHireForNo.Text)<1)
			{
				MetroMessageBox.Show(this, "", "Hire for must be greater than or equal to one", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtHireForNo.Focus();
				return false;
			}
            //else if (txtCustomerPIN.Text != "" && txtCustomerPIN.Text.Count() < 6)
            //{
            //    MetroMessageBox.Show(this, "", "Customer pin must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //    txtCustomerPIN.Focus();
            //    return false;
            //}
            else
            {
                for(int i=0;i<dgvRfidList.Rows.Count;i++)
				{
                    if(string.IsNullOrWhiteSpace(Convert.ToString(dgvRfidList.Rows[i].Cells[2].Value)))
                    {
                        MetroMessageBox.Show(this, "", "Locker icardno must be specifed", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        return false;
					}
				}
                return true;
            }
        }
        public void setprefix()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "SalesPrefix as Field1,SalesSrlNo as Field2,TaxCalBasedOnGameRev as Field3,CardPrice as Field4";
                dmnSearch.Table = "tblLocationDetails";
                dmnSearch.WhereNdGroupBy = "where Status=1 and LocId=" + BranchId; /*" Where LocId = " + BranchId;*/
                dmnSearch.OrderBy = " Order By LocId ASC ";
                var gridSource1 = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource1.Count > 0)
                {
                    txtinvoiceno.Text = gridSource1[0].Field1 + BranchId + DateTime.Now.Year.ToString() + (Convert.ToInt32(gridSource1[0].Field2) + 1);
                }
            }
            catch(Exception ex)
            {

            }
            
        }
      
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool StatusUpd = false;
                var InsertStatus=new DmnHireLocker() ;
                DmnHireLocker dmnHireLocker = new DmnHireLocker();
                BllHireLocker bllHireLocker = new BllHireLocker();
                // grpBoxPrint.Visible = true;
                if (hLocCart.Count == 0)
                {
                    hLocCart.Add(Convert.ToInt32(hLockerID));
                    dgvRfidList.Rows.Add(1);
                    dgvRfidList.Rows[0].Cells[2].Value = txtICardNo.Text;
                }
                else
                {
                    dmnHireLocker.MultiLockHireID = GetMaxMultiLockId();
                }
                if (Isvalid() == false)
                    return;
                
                for (int i = 0; i < hLocCart.Count; i++)
                {
                    if (String.IsNullOrEmpty(txtLockHireID.Text) == false)
                    {
                        dmnHireLocker.LockHireID = Convert.ToInt32(txtLockHireID.Text);
                        StatusUpd = true;
                    }
                    if (String.IsNullOrEmpty(txtLedgerID.Text))
                        txtLedgerID.Text = "0";
                    dmnHireLocker.EntryDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    //dmnHireLocker.LockID = FrmDisplayLockers.locid;
                    dmnHireLocker.LockID = Convert.ToInt32(hLocCart[i]);
                    dmnHireLocker.AccID = Convert.ToInt32(txtLedgerID.Text);
                    dmnHireLocker.AccNo = Convert.ToString(txtAccNO.Text);
                    dmnHireLocker.AccPhone = Convert.ToString(txtAccPhone.Text);
                    dmnHireLocker.AccEmail = Convert.ToString(txtAccEmail.Text);
                    dmnHireLocker.AccName = Convert.ToString(txtAccName.Text);
                    dmnHireLocker.AccAddress = Convert.ToString(txtAccAddress.Text);
                    dmnHireLocker.CustomerPin = Convert.ToString(txtCustomerPIN.Text);
                    dmnHireLocker.HireFor = Convert.ToInt32(cboHireFor.SelectedValue);
                    dmnHireLocker.RateType = Convert.ToInt32(cboHireFor.SelectedValue);

                    if (hLocCartPrice.Count > 0)
                        dmnHireLocker.Rate = Convert.ToDecimal(hLocCartPrice[i]);
                    else
                        dmnHireLocker.Rate = Convert.ToDecimal(lblRate.Text);

                    dmnHireLocker.ExpDate = Convert.ToDateTime(dtpExpDate.Text);
                    dmnHireLocker.ExpTime = Convert.ToDateTime(dtpExpTime.Text);
                    dmnHireLocker.HireDate = Convert.ToDateTime(dtpHireDate.Text);
                    dmnHireLocker.HireTime = Convert.ToDateTime(dtpHireTime.Text);
                    dmnHireLocker.LockOpen = Convert.ToInt32(cboLockStatus.SelectedValue);
                    dmnHireLocker.Inotes = Convert.ToString(txtRemarks.Text);
                    dmnHireLocker.Hiredetail = Convert.ToString(txtHireDetails.Text);
                    dmnHireLocker.HireNo = Convert.ToInt32(txtHireForNo.Text);
                    dmnHireLocker.Amt = Convert.ToDecimal(dmnHireLocker.Rate*dmnHireLocker.HireNo);
                    dmnHireLocker.ICardNo = Convert.ToString(dgvRfidList.Rows[i].Cells[2].Value);
                    dmnHireLocker.PaymentMode = Convert.ToInt32(cmbPaymentMode.SelectedValue);//new
                    dmnHireLocker.NetAmount = decimal.Parse(txtNetAmt.Text == string.Empty ? "0.00" : txtNetAmt.Text);
                    dmnHireLocker.PaidAmount = decimal.Parse(txtPaidAmt.Text == string.Empty ? "0.00" : txtPaidAmt.Text);
                    dmnHireLocker.BalanceAmount = decimal.Parse(txtBalAmt.Text == string.Empty ? "0.00" : txtBalAmt.Text);
                    dmnHireLocker.AmountAdjust = decimal.Parse(txtAmtAdjust.Text == string.Empty ? "0.00" : txtAmtAdjust.Text);
                    dmnHireLocker.DiscountAmount = decimal.Parse(txtDiscountAmt.Text == string.Empty ? "0.00" : txtDiscountAmt.Text);

                    if (chkHired.Checked == true)
                        dmnHireLocker.Hired = 1;
                    else
                        dmnHireLocker.Hired = 0;

                    
                    
                    dmnHireLocker.CreatedBy = UserId;
                    dmnHireLocker.CreatedDateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    dmnHireLocker.ModifiedBy = UserId;
                    dmnHireLocker.ModifiedDateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    dmnHireLocker.BranchId = BranchId;
                    dmnHireLocker.Status = 1;
                    dmnHireLocker.EntryNo =txtinvoiceno.Text;

                     InsertStatus = bllHireLocker.AddEditData("prdInsert_Upd_LockerHire", dmnHireLocker);
                    if (InsertStatus.LockHireID==0)
                    {
                        break;
                    }
                }
                if (pbCapture.BackgroundImage != null)
                {
                    Bitmap bm = new Bitmap(pbCapture.BackgroundImage);
                    clsDocument.UploadImage(bm, txtLedgerID.Text, DocumentType.Locker);
                }
                if (InsertStatus.LockHireID>0 )
                {
                    if (StatusUpd == false)
                    {
                        int biltval = Convert.ToInt32(BillType.SingleLockerBill);
                        if (hLocCart.Count > 1)
                            biltval = Convert.ToInt32(BillType.MultiLockerBill);

                        pLocker.SaleBill.FrmSaleInvoice frmSalesInvoice = new pLocker.SaleBill.FrmSaleInvoice(Convert.ToInt32(InsertStatus.LockHireID), biltval);
                        // Calculate the centered position
                        int centerX = Screen.PrimaryScreen.Bounds.Width / 2 - frmSalesInvoice.Width / 2;
                        int centerY = Screen.PrimaryScreen.Bounds.Height / 2 - frmSalesInvoice.Height / 2;
                        frmSalesInvoice.StartPosition = FormStartPosition.Manual;
                        frmSalesInvoice.Location = new Point(centerX, centerY);
                        // Show the form centered on the screen
                        frmSalesInvoice.ShowDialog();
                        MetroMessageBox.Show(this, cboLocker.Text + "Transaction Completed Sucessfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                        FrmDisplayLockers FrmDisplayLockers = Application.OpenForms["FrmDisplayLockers"] as FrmDisplayLockers;
                        if (FrmDisplayLockers.WindowState == FormWindowState.Minimized)
                        {
                            FrmDisplayLockers.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                            FrmDisplayLockers.FillComboLocker(0);
                        }
                    }
                    else
					{
                        MetroMessageBox.Show(this, cboLocker.Text + "Transaction Completed Sucessfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    }
                    this.Close();
					if (System.Windows.Forms.Application.OpenForms["FrmDisplayLockers"] != null)
					{
						FrmDisplayLockers FrmDisLock = Application.OpenForms["FrmDisplayLockers"] as FrmDisplayLockers;
						FrmDisLock.FillComboLocker(0);
					}
				}
                else
                {
                    MetroMessageBox.Show(this, " Save failed ", " error  ", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        private void cboHireFor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            calcamt();
            lDateChange();
            lhourChange();

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
            _objSearch.WhereNdGroupBy = " WHERE isnull(L.Status,0) = 1 ";
            _objSearch.OrderBy = "ORDER BY L.LedgerName ASC";
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
                txtLedgerID.Text = Convert.ToString(LedgerId);
                BllSearch<DmnAccountLedger> bllSearch = new BllSearch<DmnAccountLedger>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = " tblAccountLedger tal ";
                    //" left Join tblLockerHire tlh on tal.LedgerId=tlh.AccId ";
                objSearch.WhereNdGroupBy = "Where tal.LedgerId = " + LedgerId;
                objSearch.OrderBy = "Order By LedgerName ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                txtAccName.Text = Convert.ToString(gridSource[0].LedgerName);
                txtAccNO.Text = Convert.ToString(gridSource[0].LedgerCode);
                txtAccAddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                txtAccPhone.Text = Convert.ToString(gridSource[0].Mobile);
                txtAccEmail.Text = Convert.ToString(gridSource[0].EmailId);
                //txtCustomerPIN.Text = Convert.ToString(gridSource[0].CustomerPin);
                //cboHireFor.SelectedValue = Convert.ToInt64(gridSource[0].RateType);
                //txtLockHireID.Text = Convert.ToString(gridSource[0].LockHireID);
                //lblRate.Text = Convert.ToString(gridSource[0].Rate);
                //txtAmount.Text = Convert.ToString(gridSource[0].Amt);
                //dtpExpDate.Value = gridSource[0].ExpDate;
                //dtpHireTime.Value = gridSource[0].HireDate;
                // cboLocker.SelectedValue= Convert.ToInt64(gridSource[0].LockID);

                //cboLockStatus.SelectedValue = Convert.ToInt64(gridSource[0].LockOpen);
                //if (Convert.ToInt32(cboLockStatus.SelectedValue) == 2)
                //    chkHired.Checked = false;
                //else
                //    chkHired.Checked = true;
                //txtRemarks.Text = Convert.ToString(gridSource[0].Inotes);
                //txtHireForNo.Text = Convert.ToString(gridSource[0].HireNo);
                if (gridSource[0].ICardNo != null && hLocCart.Count<1)
                {
                    txtICardNo.Text = Convert.ToString(gridSource[0].ICardNo);
                    txtICardNo_2.Text = "XXXX XX" + gridSource[0].ICardNo.Substring(gridSource[0].ICardNo.Length - 6, 6);
                }
                //cmbPaymentMode.SelectedValue = Convert.ToInt64(gridSource[0].PaymentMode);
                //txtNetAmt.Text = Convert.ToString(gridSource[0].NetAmount);
                //txtPaidAmt.Text = Convert.ToString(gridSource[0].PaidAmount);
                //txtBalAmt.Text = Convert.ToString(gridSource[0].BalanceAmount);
                //txtAmtAdjust.Text = Convert.ToString(gridSource[0].AmountAdjust);           
                //txtDiscountAmt.Text = Convert.ToString(gridSource[0].DiscountAmount);
                bool imageExist = clsDocument.FileExistOrNot(LedgerId.ToString(), DocumentType.Locker);
                if (imageExist)
                    pbCapture.BackgroundImage = clsDocument.DownloadImage(LedgerId.ToString(), DocumentType.Locker);
                pbCapture.BackgroundImageLayout = ImageLayout.Stretch;


            }
        }
        private void GetAccDetails(string SearchIn, string SearchVal)
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
            if (gridSource.Count > 0)
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
                MetroMessageBox.Show(this, " No data found ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAccName.Text = "";
                txtAccNO.Text = "";
                txtAccAddress.Text = "";
                txtAccPhone.Text = "";
                txtAccEmail.Text = "";
                txtLedgerID.Text = "0";

            }

        }
        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }
        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
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
                SendKeys.Send("{TAB}");
            }
        }
        private void txtHireFor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }
        private void txtExp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
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

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        private void txtAccName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
            if (e.KeyCode == Keys.F8)
                LedgerPopup();
           // cboHireFor.Focus();
        }
        private void btnFrmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {

            //FrmLockerDashBoard frmLockerDashBoard = new FrmLockerDashBoard();
            //frmLockerDashBoard.btnHire_Click(sender, e);
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
            //cboHireFor.Focus();

            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //LedgerPopup();
            Forms.FrmCustAccount frmCustAccount = new FrmCustAccount(0);
            frmCustAccount.ShowDialog();
            frmCustAccount.WindowState = FormWindowState.Normal;

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

            //cboLocker.Text = "select";
            //cboLockStatus.Text = "select";
            txtAccNO.Text = "";
            txtAccName.Text = "";
            txtAccPhone.Text = "";
            txtICardNo.Text = "";
            txtICardNo_2.Text = "";
            txtAccEmail.Text = "";
            txtAccAddress.Text = "";
            txtRemarks.Text = "";
            txtCustomerPIN.Text = "";
            cboHireFor.SelectedIndex = 0;
            txtAmount.Text = "";
            dtpHireDate.Value = DateTime.Now;
            dtpHireTime.Value = DateTime.Now;
            dtpExpDate.Value = DateTime.Now;
            dtpExpTime.Value = DateTime.Now;
            txtHireDetails.Text = "";
            txtHireDetails.Text = "";
            txtNetAmt.Text = Convert.ToString("0.00");
            txtPaidAmt.Text = Convert.ToString("0.00");
            txtBalAmt.Text = Convert.ToString("0.00");
            txtAmtAdjust.Text = Convert.ToString("0.00");
            txtDiscountAmt.Text = Convert.ToString("0.00");
            pbCapture.BackgroundImage = null;
            pbCapture.Image = null;
            cmbPaymentMode.SelectedIndex = 0;
            if (hLocCart.Count()>0)
            {
                dgvRfidList.Rows.Clear();
                dgvRfidList.Rows.Add(hLocCart.Count);
            }
            foreach (Control c in panel1.Controls)
            {
                c.Enabled = true;
            }
            //groupBox1.Enabled = false; 
        }


        private void txtHireForNo_TextChanged(object sender, EventArgs e)
        {
            txtHireDetails.Text = "Locker"  +hLockerID+  " is Hire for ("  +txtHireForNo.Text+")"+  cboHireFor.Text;
        }
        public void lDateChange()
        {
            int n1 = Convert.ToInt32(cboHireFor.SelectedValue);
            int n2 = Convert.ToInt32(txtHireForNo.Text);

            if (n1 == 1)
            {
                dtpExpDate.Value = DateTime.Now.Date;
                dtpExpTime.Value = dtpHireTime.Value.AddHours(n2);

            }
            else if (n1 == 2)
            {
                dtpExpDate.Value = dtpHireDate.Value.AddDays(n2);
                dtpHireTime.Value = DateTime.Now;
                dtpExpTime.Value = DateTime.Now;

            }
            else if (n1 == 3)
            {
                dtpExpDate.Value = dtpHireDate.Value.AddMonths(n2);
                dtpHireTime.Value = DateTime.Now;
                dtpExpTime.Value = DateTime.Now;
            }
        }


        private void txtHireForNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHireForNo.Text) || Convert.ToInt32(txtHireForNo.Text)<1)
            {
                txtHireForNo.Text = "1";
            }
            lDateChange();
            lhourChange();
            calcamt();
        }

        public void lhourChange()
        {
            int h1 = Convert.ToInt32(cboHireFor.SelectedValue);
            int m1 = Convert.ToInt32(txtHireForNo.Text);

            if (h1 == 1 && (m1 < 0 || m1 > 24))
            {
                txtHireForNo.Text = "24";
                MetroMessageBox.Show(this, " Hour must be selected < 24 ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHireForNo.Focus();

            }
            else if (h1 == 3 && (m1 < 0 || m1 > 12))
            {
                txtHireForNo.Text = "12";
                MetroMessageBox.Show(this, " Month must be selected < 12 ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHireForNo.Focus();
            }

        }

        private void cboLockStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cboLockStatus.SelectedValue) == 2)
                chkHired.Checked = false;
            else
               if (Convert.ToInt32(cboLockStatus.SelectedValue) == 1)
                chkHired.Checked = true;
        }

        private void txtAccName_DoubleClick(object sender, EventArgs e)
        {
            LedgerPopup();
        }

        private void dtpHireTime_ValueChanged(object sender, EventArgs e)
        {
            lDateChange();
            lhourChange();
        }

        private void dtpHireDate_ValueChanged(object sender, EventArgs e)
        {
            lDateChange();

        }

        private void txtDiscountAmt_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDiscountAmt.Text))
            {
                //txtNetAmt.Text = Convert.ToString((Decimal.Parse(txtNetAmt.Text)) - (Decimal.Parse(txtDiscountAmt.Text)));
                calcamt();
            }
               
        }

        private void txtPaidAmt_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPaidAmt.Text))
            {
                // txtBalAmt.Text = Convert.ToString((Decimal.Parse(txtNetAmt.Text)) - ((Decimal.Parse(txtPaidAmt.Text))));
                calcamt();
            }
               
        }

        private void txtAmtAdjust_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAmtAdjust.Text)) ;
            {
                //txtNetAmt.Text = Convert.ToString((Decimal.Parse(txtNetAmt.Text)) - (Decimal.Parse(txtAmtAdjust.Text)));
                calcamt();
            }
                
        }


        public void txtFlow(object sender, KeyEventArgs e)
            {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
               // calcamt();
                SendKeys.Send("{TAB}");
               // calcamt();
                e.Handled = true;
            }
           


        }

        private void calcamt()
        {
            int n1;
            Decimal nRate;
            string val;
            if (hLocCart.Count == 0)
            {
                val = Convert.ToString(hLockerID);
            }
            else
            {
                val = hLocCart[0].ToString();
                for (int i = 1; i < hLocCart.Count; i++)
                {
                    val += "," + hLocCart[i];

                }
            }
            hLocCartPrice.Clear();
            BllSearch<DmnLocker> bllSearch = new BllSearch<DmnLocker>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = "tblLocker";
            objSearch.WhereNdGroupBy = "Where ID in(" + val + ")";
            //objSearch.OrderBy = "Order By I.ItemName ";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            if (itemSource.Count > 0)
            {

                nRate = 0;
                n1 = Convert.ToInt32(cboHireFor.SelectedValue);
                for (int i = 0; i < itemSource.Count; i++)
                {
                    switch (n1)
                    {
                        case 1:
                            nRate += Convert.ToDecimal(itemSource[i].HrRate);
                            hLocCartPrice.Add(Convert.ToDecimal(itemSource[i].HrRate));
                            //lblRate.Text = Convert.ToString(itemSource[0].HrRate);

                            // code block

                            break;
                        case 2:
                            nRate += Convert.ToDecimal(itemSource[i].DyRate);
                            hLocCartPrice.Add(Convert.ToDecimal(itemSource[i].DyRate));
                            //lblRate.Text = Convert.ToString(itemSource[0].DyRate);

                            // code block
                            break;
                        case 3:
                            // code
                            nRate += Convert.ToDecimal(itemSource[i].MnRate);
                            hLocCartPrice.Add(Convert.ToDecimal(itemSource[i].MnRate));

                            //lblRate.Text = Convert.ToString(itemSource[0].MnRate);

                            break;
                        default:
                            // code block
                            break;
                    }
                }
                lblRate.Text = Convert.ToString(nRate);

                if (string.IsNullOrEmpty(txtAmount.Text))
                    txtAmount.Text = "0";
                if (string.IsNullOrEmpty(txtDiscountAmt.Text))
                    txtDiscountAmt.Text = "0";
                if (string.IsNullOrEmpty(txtAmtAdjust.Text))
                    txtAmtAdjust.Text = "0";
                if (string.IsNullOrEmpty(txtAmtAdjust.Text))
                    txtAmtAdjust.Text = "0";
                if (string.IsNullOrEmpty(txtBalAmt.Text))
                    txtBalAmt.Text = "0";
                if (string.IsNullOrEmpty(txtPaidAmt.Text))
                    txtPaidAmt.Text = "0";

               
                txtAmount.Text = Convert.ToString(nRate * Convert.ToDecimal(txtHireForNo.Text));
                txtNetAmt.Text = Convert.ToString(txtAmount.Text);

                Decimal nAmt, nNetAmt, nDisc, nAdjAmt, nPaidAmt, nBalAmt;
                nAmt = Convert.ToDecimal(txtAmount.Text);
                nDisc = Convert.ToDecimal(txtDiscountAmt.Text);
                nAdjAmt = Convert.ToDecimal(txtAmtAdjust.Text);
                if(nAmt<nDisc)
				{
                    MetroMessageBox.Show(this, "", "Discount must be less than amount", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    txtDiscountAmt.Focus();
                    txtDiscountAmt.Text =Convert.ToString(nAmt);
                    return ;
                }

                nNetAmt = nAmt - nDisc + nAdjAmt;
                txtNetAmt.Text = Convert.ToString(nNetAmt);
                nPaidAmt = Convert.ToDecimal(txtPaidAmt.Text);
                nBalAmt = nNetAmt - nPaidAmt;
                txtBalAmt.Text = Convert.ToString(nBalAmt);
            }

        }

        private void btnRFID_Click(object sender, EventArgs e)
        {
            string str1 = "";
            txtICardNo.Clear();
            str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul();
            if (str1 != "Card not in contact")
            {
                timer1.Stop();
                if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")
                    str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi();
                str1 = RemoveSpecialCharacters(str1.Trim());
                if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Cardnotincontact")
                {
                    MessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    timer1.Start();
                }
                else
                {
                    BllSearch<DmnHireLocker> bllSearch = new BllSearch<DmnHireLocker>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = " tblAccountLedger tal left Join tblLockerHire tlh on tal.LedgerId=tlh.AccId ";
                    objSearch.WhereNdGroupBy = "Where tlh.ICardNo='" + str1 + "'";
                    objSearch.OrderBy = "Order By LedgerName ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource.Count > 0)
                    {
                        txtAccName.Text = Convert.ToString(gridSource[0].LedgerName);
                        txtAccNO.Text = Convert.ToString(gridSource[0].LedgerCode);
                        txtAccAddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                        txtAccPhone.Text = Convert.ToString(gridSource[0].Mobile);
                        txtAccEmail.Text = Convert.ToString(gridSource[0].EmailId);
                        txtCustomerPIN.Text = Convert.ToString(gridSource[0].CustomerPin);
                        cboHireFor.SelectedValue = Convert.ToInt64(gridSource[0].RateType);
                        txtLockHireID.Text = Convert.ToString(gridSource[0].LockHireID);
                        lblRate.Text = Convert.ToString(gridSource[0].Rate);
                        txtAmount.Text = Convert.ToString(gridSource[0].Amt);
                        dtpExpDate.Value = gridSource[0].ExpDate;
                        dtpHireTime.Value = gridSource[0].HireDate;
                        cboLocker.SelectedValue = Convert.ToInt64(gridSource[0].LockID);
                        cboLockStatus.SelectedValue = Convert.ToInt64(gridSource[0].LockOpen);
                        if (Convert.ToInt32(cboLockStatus.SelectedValue) == 2)
                            chkHired.Checked = false;
                        else
                            chkHired.Checked = true;
                        txtRemarks.Text = Convert.ToString(gridSource[0].Inotes);
                        txtHireForNo.Text = Convert.ToString(gridSource[0].HireNo);

                        str1 = Convert.ToString(gridSource[0].ICardNo);
                        txtICardNo.Text = str1;
                        string s = "XXXX XX" + str1.Substring(str1.Length - 6, 6);
                        txtICardNo_2.Text = s;
                        cmbPaymentMode.SelectedValue = Convert.ToInt64(gridSource[0].PaymentMode);
                        txtNetAmt.Text = Convert.ToString(gridSource[0].NetAmount);
                        txtPaidAmt.Text = Convert.ToString(gridSource[0].PaidAmount);
                        txtBalAmt.Text = Convert.ToString(gridSource[0].BalanceAmount);
                        txtAmtAdjust.Text = Convert.ToString(gridSource[0].AmountAdjust);
                        txtDiscountAmt.Text = Convert.ToString(gridSource[0].DiscountAmount);

                    }
                    else
                    {
                        txtICardNo.Text = str1;
                        string s = "XXXX XX" + str1.Substring(str1.Length - 6, 6);
                        txtICardNo_2.Text = s;

                    }
                }

            }
        }

        private void txtAccName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtAccName.Text.Length <= 0) return;
            string s = txtAccName.Text.Substring(0, 1);
            if (s!= s.ToUpper())
            {
                int curSelStart = txtAccName.SelectionStart;
                int curSelLength = txtAccName.SelectionLength;
                txtAccName.SelectionStart = 0;
                txtAccName.SelectionLength = 1;
                txtAccName.SelectedText = s.ToUpper();
                 txtAccName.SelectionStart = curSelStart;
                txtAccName.SelectionLength = curSelLength;
            }
        }

        private void txtCustomerPIN_Enter(object sender, EventArgs e)
        {
            lblPin.Visible = true;
        }


        private void txtDiscountAmt_Enter(object sender, EventArgs e)
        {
            if(txtDiscountAmt.Text =="0.00")
            {
                txtDiscountAmt.Text = "";
            }
        }

        private void txtAmtAdjust_Enter(object sender, EventArgs e)
        {
            if (txtAmtAdjust.Text == "0.00")
            {
                txtAmtAdjust.Text = "";
            }
        }

        private void txtPaidAmt_Enter(object sender, EventArgs e)
        {
            if (txtPaidAmt.Text == "0.00")
            {
                txtPaidAmt.Text = "";
            }
        }

        private void txtBalAmt_Enter(object sender, EventArgs e)
        {
            if (txtBalAmt.Text == "0.00")
            {
                txtBalAmt.Text = "";
            }
        }

        private void Minimize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Save_Print_Click(object sender, EventArgs e)
        {
           
           // FrmRpt_Option frmrpt_option = new FrmRpt_Option();
            //frmrpt_option.ShowDialog();
            
        }

   
        //save
        private void btnSave_MouseHover(object sender, EventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_3));
        }

        private void btnSave_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_2));
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_1));
        }

        private void btnSave_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_1));
        }

        //refresh
        private void btnRefresh_MouseHover(object sender, EventArgs e)
        {
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_3));
        }

        private void btnRefresh_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_2));
        }

        private void btnRefresh_MouseLeave(object sender, EventArgs e)
        {
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_1));
        }

        private void btnRefresh_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_1));
        }

        //close
        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_3));
        }

        private void btnClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_2));
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1));
        }

        private void btnClose_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1));
        }

        private void cboHireFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHireDetails.Text = "Locker"  + hLockerID +  " is Hire for ("  + txtHireForNo.Text + ")"+ cboHireFor.Text;
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

       

        private void txtAccPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
            //else if()
            //{
            //    e.Handled = true;
            //}
        }

        private void txtCustomerPIN_Leave(object sender, EventArgs e)
        {
            lblPin.Visible = false;
            if(txtCustomerPIN.Text!="" && txtCustomerPIN.Text.Count()<6)
			{
                MetroMessageBox.Show(this, "", "Customer pin must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtCustomerPIN.Focus();
                return ;
            }
        }
        private void dtpExpDate_ValueChanged(object sender, EventArgs e)
        {
            int n1 = Convert.ToInt32(cboHireFor.SelectedValue);
            int d = 0;
            if (n1 == 2)
            {
               d = Convert.ToInt32(dtpExpDate.Value.Day - dtpHireDate.Value.Day);
                
            }
            if(n1==3)
            {
                 d = Convert.ToInt32(dtpExpDate.Value.Month - dtpHireDate.Value.Month);
            }
            txtHireForNo.Text = d.ToString();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string str1 = "";
            txtICardNo.Clear();
            str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul();
            if (str1 != "Card not in contact")
            {
                timer1.Stop();
                if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")
                    str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi();
                str1 = RemoveSpecialCharacters(str1.Trim());
                if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Cardnotincontact")
                {
                    MessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    timer1.Start();
                }
                else
                {
                    BllSearch<DmnHireLocker> bllSearch = new BllSearch<DmnHireLocker>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = " tblAccountLedger tal left Join tblLockerHire tlh on tal.LedgerId=tlh.AccId ";
                    objSearch.WhereNdGroupBy = "Where tlh.ICardNo='" + str1 + "'";
                    objSearch.OrderBy = "Order By LedgerName ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource.Count > 0)
                    {
                        txtAccName.Text = Convert.ToString(gridSource[0].LedgerName);
                        txtAccNO.Text = Convert.ToString(gridSource[0].LedgerCode);
                        txtAccAddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                        txtAccPhone.Text = Convert.ToString(gridSource[0].Mobile);
                        txtAccEmail.Text = Convert.ToString(gridSource[0].EmailId);
                        txtCustomerPIN.Text = Convert.ToString(gridSource[0].CustomerPin);
                        cboHireFor.SelectedValue = Convert.ToInt64(gridSource[0].RateType);
                        txtLockHireID.Text = Convert.ToString(gridSource[0].LockHireID);
                        lblRate.Text = Convert.ToString(gridSource[0].Rate);
                        txtAmount.Text = Convert.ToString(gridSource[0].Amt);
                        dtpExpDate.Value = gridSource[0].ExpDate;
                        dtpHireTime.Value = gridSource[0].HireDate;
                        cboLocker.SelectedValue = Convert.ToInt64(gridSource[0].LockID);

                        cboLockStatus.SelectedValue = Convert.ToInt64(gridSource[0].LockOpen);
                        if (Convert.ToInt32(cboLockStatus.SelectedValue) == 2)
                            chkHired.Checked = false;
                        else
                            chkHired.Checked = true;
                        txtRemarks.Text = Convert.ToString(gridSource[0].Inotes);
                        txtHireForNo.Text = Convert.ToString(gridSource[0].HireNo);

                        str1 = Convert.ToString(gridSource[0].ICardNo);
                        txtICardNo.Text = str1;
                        string s = "XXXX XX" + str1.Substring(str1.Length - 6, 6);
                        txtICardNo_2.Text = s;

                        cmbPaymentMode.SelectedValue = Convert.ToInt64(gridSource[0].PaymentMode);
                        txtNetAmt.Text = Convert.ToString(gridSource[0].NetAmount);
                        txtPaidAmt.Text = Convert.ToString(gridSource[0].PaidAmount);
                        txtBalAmt.Text = Convert.ToString(gridSource[0].BalanceAmount);
                        txtAmtAdjust.Text = Convert.ToString(gridSource[0].AmountAdjust);
                        txtDiscountAmt.Text = Convert.ToString(gridSource[0].DiscountAmount);

                    }
                    else
                    {
                        txtICardNo.Text = str1;
                        string s = "XXXX XX" + str1.Substring(str1.Length - 6, 6);
                        txtICardNo_2.Text = s;

                    }
                }

            }
        }
        private void txtICardNo_2_Leave(object sender, EventArgs e)
        {
            lblRfid.Visible = false;
            timer1.Stop();
        }
        private void txtICardNo_2_Enter(object sender, EventArgs e)
        {
            if (hLocCart.Count > 0)
            {
                pnlRfidList.Location = new Point(
                            this.ClientSize.Width / 2 - pnlRfidList.Size.Width / 2,
                            this.ClientSize.Height / 2 - pnlRfidList.Size.Height / 2);
                pnlRfidList.Anchor = AnchorStyles.None;
                pnlRfidList.Show();
                dgvRfidList.CurrentCell = dgvRfidList.Rows[0].Cells[1];
                dgvRfidList.Focus();
            }
            else
            {
                lblRfid.Visible = true;
                timer1.Start();
            }
        }

		private void dgvRfidList_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
            if (e.ColumnIndex == 1)
            {
                timer2.Start();
            }
		}

		private void dgvRfidList_CellLeave(object sender, DataGridViewCellEventArgs e)
		{
            timer2.Stop();
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
            string str1 = "";
            str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul();
            if (str1 != "Card not in contact")
            {
                timer2.Stop();
                if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")
                    str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi();
                str1 = RemoveSpecialCharacters(str1.Trim());
                if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Cardnotincontact")
                {
                    MessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    timer2.Start();
                }
                else
                {
                    dgvRfidList.Rows[dgvRfidList.CurrentCell.RowIndex].Cells[2].Value = str1;
                    string s = "XXXX XX" + str1.Substring(str1.Length - 6, 6);
                    dgvRfidList.Rows[dgvRfidList.CurrentCell.RowIndex].Cells[1].Value = s;
                }

            }
        }
		private void btnRfidListOk_Click(object sender, EventArgs e)
		{
            pnlRfidList.Visible = false;
            txtAccPhone.Focus();
        }

        private void btnRfidListClose_Click(object sender, EventArgs e)
		{
            pnlRfidList.Visible = false;
		}

		private void txtHireForNo_KeyPress(object sender, KeyPressEventArgs e)
		{
            if (!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        public int GetMaxMultiLockId()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "ISNULL(MAX(MultiLockHireID),0) as Id";
                dmnSearch.Table = "tblLockerHire";
                var gridSource1 = bllSearch.GetSearchData("prdSearch", dmnSearch);
                return Convert.ToInt32(gridSource1[0].Id+1);
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

		private void txtICardNo_2_KeyDown(object sender, KeyEventArgs e)
		{
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

		private void txtDiscountAmt_KeyPress(object sender, KeyPressEventArgs e)
		{
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

		private void txtAmtAdjust_KeyPress(object sender, KeyPressEventArgs e)
		{
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

		private void txtPaidAmt_KeyPress(object sender, KeyPressEventArgs e)
		{
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

		private void txtCustomerPIN_KeyPress(object sender, KeyPressEventArgs e)
		{
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            
        }

		private void dgvRfidList_KeyDown(object sender, KeyEventArgs e)
		{
            int r = dgvRfidList.CurrentCell.RowIndex;
            int c = dgvRfidList.CurrentCell.ColumnIndex;
            if (e.KeyData==Keys.Enter)
			{
                if(r==dgvRfidList.Rows.Count-1)
				{
                    btnRfidListOk.Focus();
				}
                
			}
		}
	}
}
