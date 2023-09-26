using BLL;
using Domain;
using MarginPOSNew.Classes;
using MetroFramework;
using pLocker.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pLocker.Interfaces;
using pLocker.Forms.UserControls;

namespace pLocker.Forms
{
    public partial class Frmbulklocker : Form
    {
        public Frmbulklocker()
        {
            InitializeComponent();
        }
        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsGeneral = new ClsGeneral();
        long BranchId = Forms.frmLogin.BranchId;
        long UserId = Forms.frmLogin.UserId;
        int LedgerId;
        public int bulkid;
        #region List Control name enbling
        string[] controlNames = new string[]
{
    "txtAccName",
    "txtAccAddress",
    "txtAccPhone",
    "txtAccEmail",
    "txtNoLockers",
    "cboHireFor",
    "txtLockHireID",
    "lblRate",
    "txtAmount",
    "cboLocker",
    "cboLockStatus",
    "txtRemarks",
    "txtHireForNo",
    "cmbPaymentMode",
    "txtNetAmt",
    "txtPaidAmt",
    "txtBalAmt",
    "txtAmtAdjust",
    "txtDiscountAmt"
};
        #endregion
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refreshform();
        }
        private bool Isvalid()
        {
            if (cboLocker.SelectedIndex==0)
            {
                MetroMessageBox.Show(this, "", "Please Select Locker", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                cboLocker.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtNoLockers.Text.Trim()) || Convert.ToInt32(txtNoLockers.Text)==0 )
            {
                MetroMessageBox.Show(this, "", "Please Enter Total Number of Locker you want", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtNoLockers.Focus();
                return false;
            }
            //if (string.IsNullOrWhiteSpace(txtAccNO.Text.Trim()))
            //{
            //    MetroMessageBox.Show(this, "", "Account No must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //    txtAccNO.Focus();
            //    return false;
            //}
            else if (string.IsNullOrWhiteSpace(txtAccName.Text.Trim()))
            {
                MetroMessageBox.Show(this, "", "Account Name must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtAccName.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtAccPhone.Text.Trim()))
            {
                MetroMessageBox.Show(this, "", "Phone No.must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtAccPhone.Focus();
                return false;
            } 
            //else if(txtAccEmail.Text!="" && txtAccEmail.Text != null)
            //{
            //    if (!string.IsNullOrWhiteSpace(txtAccEmail.Text) && clsGeneral.CheckEmailId(this, txtAccEmail.Text) == false)
            //    {
            //        return false;
            //    }

            //}
            else if (string.IsNullOrWhiteSpace(txtAccAddress.Text.Trim()))
            {
                MetroMessageBox.Show(this, "", "Address must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtAccAddress.Focus();
                return false;
            }

            else if (string.IsNullOrWhiteSpace(txtHireForNo.Text.Trim()))
            {
                MetroMessageBox.Show(this, "", "Hire for No's must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtHireForNo.Focus();
                return false;
            }            
            else if (string.IsNullOrWhiteSpace(txtPaidAmt.Text.Trim()))
            {
                MetroMessageBox.Show(this, "", "Paid Amount must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtPaidAmt.Focus();
                return false;
            }
            if (cmbPaymentMode.SelectedIndex == 0)
            {
                MetroMessageBox.Show(this, "", "Please Select Payment Mode", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                cmbPaymentMode.Focus();
                return false;
            }
            
            //else if (Convert.ToInt32(cboHireFor.SelectedValue) == 0)
            //{
            //    MetroMessageBox.Show(this, "", "Hire for Hours/Days/Month must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //    cboHireFor.Focus();
            //    return false;
            //}  
            else
            {
                return true;
            }
        }
        public void setprefix()
        {
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            DmnSearch dmnSearch = new DmnSearch(); 
            dmnSearch.Select = "LockerPrefix as Field1,LockerSrlNo as Field2,TaxCalBasedOnGameRev as Field3,CardPrice as Field4";
            dmnSearch.Table = "tblLocationDetails";
            dmnSearch.WhereNdGroupBy = "where Status=1 and LocId=" +BranchId; /*" Where LocId = " + BranchId;*/
            dmnSearch.OrderBy = " Order By LocId ASC ";
            var gridSource1 = bllSearch.GetSearchData("prdSearch", dmnSearch);
            if (gridSource1.Count > 0)
            {
                txtinvoiceno.Text = gridSource1[0].Field1 + BranchId + DateTime.Now.Year.ToString() + (Convert.ToInt32(gridSource1[0].Field2) + 1);
            }
        }
       private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                int BulkId=0;
                if (Isvalid() == false)
                    return;

                DmnHireLocker dmnHireLocker = new DmnHireLocker();
                BllHireLocker bllHireLocker = new BllHireLocker();

                DmnHireLocker dmnBulkLocker = new DmnHireLocker();
                BllBulkLocker bllBulkLocker = new BllBulkLocker();

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = " TOP " + Convert.ToInt32(txtNoLockers.Text) + " *,Id as Field1,HrRate as Field2,DyRate as Field3,MnRate as Field4,RFID  as Field5";
                objSearch.Table = "tblLocker";
                objSearch.WhereNdGroupBy = "Where isnull(status,0)=1 and Hired =0 and BranchId=" + BranchId +
                    " and id >= " + cboLocker.SelectedValue;
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {

                    if (String.IsNullOrEmpty(txtLedgerID.Text))
                        txtLedgerID.Text = "0";
                    #region Bulk Locker Insertion
                    dmnBulkLocker.EntryNo = txtinvoiceno.Text;
                    dmnBulkLocker.AccName = Convert.ToString(txtAccName.Text);
                    dmnBulkLocker.AccAddress = Convert.ToString(txtAccAddress.Text);
                    dmnBulkLocker.AccEmail = Convert.ToString(txtAccEmail.Text);
                    dmnBulkLocker.AccPhone = Convert.ToString(txtAccPhone.Text);
                    dmnBulkLocker.Inotes = Convert.ToString(txtRemarks.Text);
                    dmnBulkLocker.LedgerId = Convert.ToInt32(txtLedgerID.Text);

                    dmnBulkLocker.ExpDate = Convert.ToDateTime(dtpExpDate.Text);
                    dmnBulkLocker.ExpTime = Convert.ToDateTime(dtpExpTime.Text);
                    dmnBulkLocker.HireDate = Convert.ToDateTime(dtpHireDate.Text);
                    dmnBulkLocker.HireTime = Convert.ToDateTime(dtpHireTime.Text);
                    dmnBulkLocker.LockOpen = Convert.ToInt32(cboLockStatus.SelectedValue);
                    dmnBulkLocker.Inotes = Convert.ToString(txtRemarks.Text);
                    dmnBulkLocker.Hiredetail = Convert.ToString(txtHireDetails.Text);
                    dmnBulkLocker.PaymentMode = Convert.ToInt32(cmbPaymentMode.SelectedValue);//new
                    dmnBulkLocker.NetAmount = decimal.Parse(txtNetAmt.Text == string.Empty ? "0.00" : txtNetAmt.Text);
                    dmnBulkLocker.PaidAmount = decimal.Parse(txtPaidAmt.Text == string.Empty ? "0.00" : txtPaidAmt.Text);
                    dmnBulkLocker.BalanceAmount = decimal.Parse(txtBalAmt.Text == string.Empty ? "0.00" : txtBalAmt.Text);
                    dmnBulkLocker.AmountAdjust = decimal.Parse(txtAmtAdjust.Text == string.Empty ? "0.00" : txtAmtAdjust.Text);
                    dmnBulkLocker.DiscountAmount = decimal.Parse(txtDiscountAmt.Text == string.Empty ? "0.00" : txtDiscountAmt.Text);
                    dmnBulkLocker.CreatedBy = UserId;
                    dmnBulkLocker.BranchId = BranchId;
                    dmnBulkLocker.Status = 1;
                    dmnBulkLocker.TotalLocker = Convert.ToInt32(txtNoLockers.Text);
                    var InsertStatus1 = bllBulkLocker.AddEditData("prdInsert_Upd_BulkLockerHire", dmnBulkLocker); 

                    #endregion   
                    if (InsertStatus1.BulkLockHireID>0)
                    {
                        BulkId = InsertStatus1.BulkLockHireID;
                        for (int i = 0; i < Convert.ToInt32(itemSource.Count); i++)
                        {
                            dmnHireLocker.BulkLockHireID = Convert.ToInt32(InsertStatus1.BulkLockHireID);               
                            dmnHireLocker.EntryDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                            dmnHireLocker.LockID = Convert.ToInt32(itemSource[i].Field1);
                            dmnHireLocker.AccID = Convert.ToInt32(txtLedgerID.Text);

                            dmnHireLocker.AccNo = "0";// Convert.ToString(txtAccNO.Text);
                            dmnHireLocker.ICardNo = itemSource[i].Field5;

                            dmnHireLocker.AccPhone = Convert.ToString(txtAccPhone.Text);
                            dmnHireLocker.AccEmail = Convert.ToString(txtAccEmail.Text);
                            dmnHireLocker.CustomerPin = Convert.ToString(txtCustomerPIN.Text);
                            dmnHireLocker.HireFor = Convert.ToInt32(cboHireFor.SelectedValue);
                            dmnHireLocker.RateType = Convert.ToInt32(cboHireFor.SelectedValue);
                            int n1 = Convert.ToInt32(cboHireFor.SelectedValue);
                            switch (n1)
                            {
                                case 1:
                                    dmnHireLocker.Rate = Convert.ToDecimal(itemSource[i].Field2);
                                    dmnHireLocker.Amt = Convert.ToDecimal(txtHireForNo.Text) * Convert.ToDecimal(itemSource[i].Field2);
                                    break;
                                case 2:
                                    dmnHireLocker.Rate = Convert.ToDecimal(itemSource[i].Field3);
                                    dmnHireLocker.Amt = Convert.ToDecimal(txtHireForNo.Text) * Convert.ToDecimal(itemSource[i].Field3);
                                    break;
                                case 3:
                                    dmnHireLocker.Rate = Convert.ToDecimal(itemSource[i].Field4);
                                    dmnHireLocker.Amt = Convert.ToDecimal(txtHireForNo.Text) * Convert.ToDecimal(itemSource[i].Field4);
                                    break;
                                default:
                                    // code block
                                    break;
                            }                                               
                            dmnHireLocker.ExpDate = Convert.ToDateTime(dtpExpDate.Text);
                            dmnHireLocker.ExpTime = Convert.ToDateTime(dtpExpTime.Text);
                            dmnHireLocker.HireDate = Convert.ToDateTime(dtpHireDate.Text);
                            dmnHireLocker.HireTime = Convert.ToDateTime(dtpHireTime.Text);
                            dmnHireLocker.LockOpen = Convert.ToInt32(cboLockStatus.SelectedValue);
                            dmnHireLocker.Inotes = Convert.ToString(txtRemarks.Text);
                            dmnHireLocker.Hiredetail = Convert.ToString(txtHireDetails.Text);
                            dmnHireLocker.HireNo = Convert.ToInt32(txtHireForNo.Text);
                            dmnHireLocker.PaymentMode = Convert.ToInt32(cmbPaymentMode.SelectedValue);//new
                            dmnHireLocker.NetAmount = decimal.Parse(txtNetAmt.Text == string.Empty ? "0.00" : txtNetAmt.Text);
                            dmnHireLocker.PaidAmount = decimal.Parse(txtPaidAmt.Text == string.Empty ? "0.00" : txtPaidAmt.Text);
                            dmnHireLocker.BalanceAmount = decimal.Parse(txtBalAmt.Text == string.Empty ? "0.00" : txtBalAmt.Text);
                            dmnHireLocker.AmountAdjust = decimal.Parse(txtAmtAdjust.Text == string.Empty ? "0.00" : txtAmtAdjust.Text);
                            dmnHireLocker.DiscountAmount = decimal.Parse(txtDiscountAmt.Text == string.Empty ? "0.00" : txtDiscountAmt.Text);
                            dmnBulkLocker.MultiLockHireID = 0;
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
                            var InsertStatus = bllHireLocker.AddEditData("prdInsert_Upd_LockerHire", dmnHireLocker);
                            if (InsertStatus.LockHireID == 0)
                            {
                                flag = true;
                                MessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        }
                    }
                    if (flag == false)
                    {
                        if (BulkId > 0)
                        {
                            pLocker.SaleBill.FrmSaleInvoice frmSalesInvoice = new pLocker.SaleBill.FrmSaleInvoice(BulkId, Convert.ToInt32(BillType.BulkLockerBill));
                            // Calculate the centered position
                            int centerX = Screen.PrimaryScreen.Bounds.Width / 2 - frmSalesInvoice.Width / 2;
                            int centerY = Screen.PrimaryScreen.Bounds.Height / 2 - frmSalesInvoice.Height / 2;
                            frmSalesInvoice.StartPosition = FormStartPosition.Manual;
                            frmSalesInvoice.Location = new Point(centerX, centerY);
                            // Show the form centered on the screen
                            frmSalesInvoice.ShowDialog();
                            MetroMessageBox.Show(this, cboLocker.Text + "Transaction Completed Sucessfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                            FrmDisplayLockers FrmDisplayLockers = Application.OpenForms["FrmDisplayLockers"] as FrmDisplayLockers;
                            if (FrmDisplayLockers != null) // Check if the form exists
                            {
                                FrmDisplayLockers.FillComboLocker(0);
                            }

                        }
                        Refreshform();
                    }
                    else
                    {
                        MetroMessageBox.Show(this, " Save failed ", " error  ", MessageBoxButtons.OK);
                    }
                }
                           

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        private void Refreshform()
        {
            foreach (string controlName in controlNames)
            {
                Control control = Controls.Find(controlName, true).FirstOrDefault();
                if (control != null)
                {
                    control.Enabled = true;
                }
            }
            txttotal_locker.Text = "0";
            txtNoLockers.Text = "0";
            lblNetAmount.Text = "0";
            cboHireFor.SelectedIndex = 1;
            btnSave.Visible = true;
            btnLockerClose.Visible = false;
            lblToolTipAccName.Visible = false;
            chkHired.Visible = true;
            cboLockStatus.SelectedIndex = -1;
            txtLedgerID.Text = "";
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
      
            FillCombo();
            if (System.Windows.Forms.Application.OpenForms["FrmDisplayLockers"] != null)
            {
                FrmDisplayLockers FrmDisLock = Application.OpenForms["FrmDisplayLockers"] as FrmDisplayLockers;
                FrmDisLock.FillComboLocker(0);
            }
            cmbPaymentMode.SelectedIndex = 0;
            setprefix();
        }
        public void FillCombo()
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                // ============ Locker 
                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = "Id,Locker";
                objSearch.Table = " tblLocker";
                objSearch.WhereNdGroupBy = "where status=1  and Hired=0 and  BranchId=" + BranchId;
                objSearch.OrderBy = "order by value";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                cboLocker.DataSource = gridSource;
                cboLocker.ValueMember = "Id";
                cboLocker.DisplayMember = "Value";
   
                // ============ Locker


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
                objSearch.OrderBy = "order by Id";
                gridSource = bllSearch_HireFor.GetSearchData("prdComboFill", objSearch);
                cboHireFor.DataSource = gridSource;
                cboHireFor.ValueMember = "Id";
                cboHireFor.DisplayMember = "Value";
                cboHireFor.SelectedIndex = 1;

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
            catch (Exception ex)
            {

            }           

        }

        private void txtAccName_DoubleClick(object sender, EventArgs e)
        {
            LedgerPopup();
        }
        public void LedgerPopup()
        {
            try {
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Name,Account no,Mobile,Email,Address,Valid To";
                _objSearch.Select = "L.LedgerId as Id,L.LedgerName as Field1,LedgerCode as Field2,L.Mobile as Field3,L.EmailId as Field4, L.LedgerAddress as Field5, L.vto as Field6";
                _objSearch.Table = " tblAccountLedger L LEFT JOIN tblAccountGroup Ac  ON L.AccountGroupId = Ac.AccountGroupId";
                _objSearch.WhereNdGroupBy = " WHERE isnull(L.Status,0) = 1 And  isnull(L.cardstatus,0)=1 and L.BranchId =" + BranchId;
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
                    BllSearch<DmnHireLocker> bllSearch = new BllSearch<DmnHireLocker>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = " tblAccountLedger tal left Join tblLockerHire tlh on tal.LedgerId=tlh.AccId ";
                    objSearch.WhereNdGroupBy = "Where tal.LedgerId = " + LedgerId;
                    objSearch.OrderBy = "Order By LedgerName ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    txtAccName.Text = Convert.ToString(gridSource[0].LedgerName);
                    txtAccNO.Text = Convert.ToString(gridSource[0].LedgerCode);
                    txtAccAddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                    txtAccPhone.Text = Convert.ToString(gridSource[0].Mobile);
                    txtAccEmail.Text = Convert.ToString(gridSource[0].EmailId); 
                    
                    //bool imageExist = clsDocument.FileExistOrNot(LedgerId.ToString(), DocumentType.Locker);
                    //if (imageExist)
                    //    pbCapture.BackgroundImage = clsDocument.DownloadImage(LedgerId.ToString(), DocumentType.Locker);
                    //pbCapture.BackgroundImageLayout = ImageLayout.Stretch;

                }
            }
            catch(Exception ex)
            {

            }
            
        }

        private void txtAccName_Enter(object sender, EventArgs e)
        {
            lblToolTipAccName.Visible = true;
        }
        private void txtAccName_Leave(object sender, EventArgs e)
        {
            lblToolTipAccName.Visible = false;
        }

        private void txtAccName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
            if (e.KeyCode == Keys.F12)
                LedgerPopup();
            // cboHireFor.Focus();
        }
        private void txtAccName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtAccName.Text.Length <= 0) return;
            string s = txtAccName.Text.Substring(0, 1);
            if (s != s.ToUpper())
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

        private void txtICardNo_2_Enter(object sender, EventArgs e)
        {
            lblRfid.Visible = true;
            timer1.Start();
        }

        private void txtICardNo_2_Leave(object sender, EventArgs e)
        {
            lblRfid.Visible = false;
            timer1.Stop();
        }

        private void txtAccNO_Enter(object sender, EventArgs e)
        {
            lbltooltip2.Visible = true;
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
        private void txtAccNO_Leave(object sender, EventArgs e)
        {
            lbltooltip2.Visible = false;
        }

    
    
        private void cboHireFor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboHireFor.SelectedIndex == 0)
                cboHireFor.SelectedIndex = 1;
            calcamt();
            lDateChange();
            lhourChange();
            RefreshHiredetails();
        }
        public void lhourChange()
        {
            try
            {
                if (txtNoLockers.Text != "" && txtNoLockers.Text != null)
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
            }
            catch(Exception ex)
            {

            }
            

        }
        public void lDateChange()
        {
            try
            {
                if (txtNoLockers.Text != "" && txtNoLockers.Text != null)
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
            }
            catch(Exception ex)
            {

            }
            
        }
        private void calcamt()
        {
            try
            {
                if (txtNoLockers.Text != "" && txtNoLockers.Text != null)
                {
                    int n1;
                    decimal Amount = 0;
                    Decimal nRate;
                    decimal netamt_temp = 0;
                    txtBalAmt.Text = "0";
                    txtAmount.Text = "0";
                    txtDiscountAmt.Text = "0";
                    txtAmtAdjust.Text = "0";
                    txtNetAmt.Text = "0";
                    BllSearch<DmnLocker> bllSearch = new BllSearch<DmnLocker>();
                    DmnSearch objSearch = new DmnSearch();
                    BllSearch<DmnGridFields> bllSearch_locker = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch_locker = new DmnSearch();
                    objSearch_locker.Select = " TOP " + Convert.ToInt32(txtNoLockers.Text) + " *,COUNT(*) OVER() AS Field1,Id as Field2";
                    objSearch_locker.Table = "tblLocker";
                    objSearch_locker.WhereNdGroupBy = "Where isnull(status,0)=1 and Hired =0 and BranchId=" + BranchId +
                        " and id >= " + cboLocker.SelectedValue;
                    var itemSource_locker = bllSearch_locker.GetSearchData("prdSearch", objSearch_locker);
                    if (itemSource_locker.Count > 0)
                    {
                        for (int i = 0; i < itemSource_locker.Count; i++)
                        {
                            objSearch.Select = "*";
                            objSearch.Table = "tblLocker";
                            objSearch.WhereNdGroupBy = "Where ID = " + itemSource_locker[i].Field2;
                            //objSearch.OrderBy = "Order By I.ItemName ";
                            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                            if (itemSource.Count > 0)
                            {
                                nRate = 0;
                                n1 = Convert.ToInt32(cboHireFor.SelectedValue);
                                switch (n1)
                                {
                                    case 1:
                                        nRate = Convert.ToDecimal(itemSource[0].HrRate);
                                        lblRate.Text = Convert.ToString(itemSource[0].HrRate);

                                        // code block

                                        break;
                                    case 2:
                                        nRate = Convert.ToDecimal(itemSource[0].DyRate);
                                        lblRate.Text = Convert.ToString(itemSource[0].DyRate);

                                        // code block
                                        break;
                                    case 3:
                                        // code
                                        nRate = Convert.ToDecimal(itemSource[0].MnRate);
                                        lblRate.Text = Convert.ToString(itemSource[0].MnRate);

                                        break;
                                    default:
                                        // code block
                                        break;
                                }

                                if (string.IsNullOrEmpty(txtAmount.Text))
                                    txtAmount.Text = "0";
                                if (string.IsNullOrEmpty(txtDiscountAmt.Text))
                                    txtDiscountAmt.Text = "0";
                                if (string.IsNullOrEmpty(txtAmtAdjust.Text))
                                    txtAmtAdjust.Text = "0";
                                if (string.IsNullOrEmpty(txtBalAmt.Text))
                                    txtBalAmt.Text = "0";

                                Amount = nRate * Convert.ToDecimal(txtHireForNo.Text);          
                                netamt_temp = netamt_temp + Amount;

                            }
                        }
                        lblNetAmount.Text = Convert.ToString(netamt_temp);
                        DAP_Calc();
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        public void DAP_Calc()
        {
            try
            {
                if (string.IsNullOrEmpty(lblNetAmount.Text))
                    lblNetAmount.Text = "0";
                txtAmount.Text = lblNetAmount.Text;
                //Total net calculation
                Decimal nAmt, nNetAmt, nDisc, nAdjAmt, nPaidAmt, nBalAmt;
                nAmt = Convert.ToDecimal(lblNetAmount.Text);
                nDisc = Convert.ToDecimal(txtDiscountAmt.Text);
                nAdjAmt = Convert.ToDecimal(txtAmtAdjust.Text);
                nNetAmt = nAmt - nDisc + nAdjAmt;
                txtNetAmt.Text = Convert.ToString(nNetAmt);

                //balance calculation
                nPaidAmt = Convert.ToDecimal(txtPaidAmt.Text);
                nBalAmt = nNetAmt - nPaidAmt;
                txtBalAmt.Text = Convert.ToString(nBalAmt);
            }
            catch(Exception ex)
            {

            }           

        }
        private void txtDiscountAmt_Enter(object sender, EventArgs e)
        {
            if (txtDiscountAmt.Text == "0.00")
            {
                txtDiscountAmt.Text = "";
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
        private void Frmbulklocker_Load(object sender, EventArgs e)
        {
            grpBoxPrint.Visible = false;
            lblToolTipAccName.Visible = true;
            FillCombo();
            Refreshform();
            txtAccName.Focus();
            setprefix();
        }

        private void btnFrmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void CountLocker()
        {
            try
            {
                if (cboLocker.SelectedIndex == 0)
                {
                    MetroMessageBox.Show(this, "Please Select Locker", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Convert.ToInt32(txtNoLockers.Text) == 0)
                    txttotal_locker.Text = "";

                if (txtNoLockers.Text != null && txtNoLockers.Text != "")
                {
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = " TOP " + Convert.ToInt32(txtNoLockers.Text) + " *,COUNT(*) OVER() AS Field1,Id as Field2";
                    objSearch.Table = "tblLocker";
                    objSearch.WhereNdGroupBy = "Where isnull(status,0)=1 and Hired =0 and BranchId=" + BranchId +
                        " and id >= " + cboLocker.SelectedValue;
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        if (Convert.ToInt32(itemSource[0].Field1) < Convert.ToInt32(txtNoLockers.Text))
                        {
                            MetroMessageBox.Show(this, "Available Locker is " + Convert.ToString(itemSource[0].Field1), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtNoLockers.Text = Convert.ToString(itemSource[0].Field1);
                            txttotal_locker.Text = Convert.ToString(itemSource[0].Field1);
                            return;
                        }
                        txttotal_locker.Text = Convert.ToString(itemSource.Count);
                    }
                }
            }
            catch(Exception ex)
            {
               
            }
        
          
        }

        private void txtNoLockers_Leave(object sender, EventArgs e)
        {
            CountLocker();
            calcamt();
            lDateChange();
            lhourChange();
            if(txtNoLockers.Text !="" && txtNoLockers.Text !=null && cboLocker.SelectedIndex>0)
                RefreshHiredetails();
        }

        private void txtDiscountAmt_Leave_1(object sender, EventArgs e)
        {
            DAP_Calc();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            BulkPopup();
        }
        public void FillAllLocker()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "Id,Locker";
            objSearch.Table = " tblLocker";
            objSearch.WhereNdGroupBy = "where status=1 and  BranchId=" + BranchId;
            objSearch.OrderBy = "order by value";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            cboLocker.DataSource = gridSource;
            cboLocker.ValueMember = "Id";
            cboLocker.DisplayMember = "Value";
        }
        public void BulkPopup()
        {
            try {
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                DmnSearch _objSearch = new DmnSearch();
                BllSearch<DmnGridFields> bllSearchcount = new BllSearch<DmnGridFields>();
                _objSearch.ColumnHeaders = "Id,Invoice No,Name,Mobile,Total Locker,Closed Locker,Assign Time";
                _objSearch.Select = "bl.BulkLockHireID as Id,bl.EntryNo as Field1,bl.AccName as Field2,bl.AccPhone as Field3,bl.TotalLocker as Field4,( select count(*) from tblLockerHire where Hired=0 and Status=1 and BranchId="+BranchId+ " and BulkLockHireID= bl.BulkLockHireID) as Field5, bl.Hiredetail as Field6";
                _objSearch.Table = " tblBulkLockHire bl "; //LEFT JOIN tblLockerHire lh  ON lh.BulkLockHireID = bl.BulkLockHireID
                _objSearch.WhereNdGroupBy = " WHERE bl.Status = 1 And bl.BranchId =" + BranchId;
                _objSearch.OrderBy = "ORDER BY bl.BulkLockHireID DESC";
                var itemSource = bllSearchcount.GetSearchData("prdSearch", _objSearch);
                if (itemSource.Count==0)
                {
                    MetroMessageBox.Show(this, "No Records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }                
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
                    foreach (string controlName in controlNames)
                    {
                        Control control = Controls.Find(controlName, true).FirstOrDefault();
                        if (control != null)
                        {
                            control.Enabled = false;
                        }
                    }

                    btnSave.Visible = false;
                    btnLockerClose.Visible = true;

                    FillAllLocker();
                    bulkid = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnHireLocker> bllSearch = new BllSearch<DmnHireLocker>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = " tblBulkLockHire bl LEFT JOIN tblLockerHire lh  ON lh.BulkLockHireID = bl.BulkLockHireID";
                    objSearch.WhereNdGroupBy = " WHERE bl.Status = 1 and bl.BulkLockHireID= " + bulkid + " and bl.BranchId =" + BranchId;
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    txtAccName.Text = Convert.ToString(gridSource[0].AccName);
                    txtAccAddress.Text = Convert.ToString(gridSource[0].AccAddress);
                    txtAccPhone.Text = Convert.ToString(gridSource[0].AccPhone);
                    txtAccEmail.Text = Convert.ToString(gridSource[0].AccEmail);

                    txtNoLockers.Text = Convert.ToString(gridSource[0].TotalLocker);
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
                    cmbPaymentMode.SelectedValue = Convert.ToInt64(gridSource[0].PaymentMode);
                    txtNetAmt.Text = Convert.ToString(gridSource[0].NetAmount);
                    txtPaidAmt.Text = Convert.ToString(gridSource[0].PaidAmount);
                    txtBalAmt.Text = Convert.ToString(gridSource[0].BalanceAmount);
                    txtAmtAdjust.Text = Convert.ToString(gridSource[0].AmountAdjust);
                    txtDiscountAmt.Text = Convert.ToString(gridSource[0].DiscountAmount);

                    //bool imageExist = clsDocument.FileExistOrNot(LedgerId.ToString(), DocumentType.Locker);
                    //if (imageExist)
                    //    pbCapture.BackgroundImage = clsDocument.DownloadImage(LedgerId.ToString(), DocumentType.Locker);
                    //pbCapture.BackgroundImageLayout = ImageLayout.Stretch;

                }
            }
            catch(Exception ex)
            {

            }
            
        }
        public void RefreshHiredetails()
        {
           int  n = Convert.ToInt32(cboHireFor.SelectedValue);
            switch (n)
            {
                case 1:
                    txtHireDetails.Text = txtNoLockers.Text + " Lockers " + " Hire for (" + txtHireForNo.Text + ") Hour";
                    break;
                case 2:
                    txtHireDetails.Text = txtNoLockers.Text + " Lockers " + " Hire for (" + txtHireForNo.Text + ") Day";
                    break;
                case 3:
                    txtHireDetails.Text = txtNoLockers.Text + " Lockers " + " Hire for (" + txtHireForNo.Text + ") Month";
                    break;
            }
            
        }
        private void btnLockerClose_Click(object sender, EventArgs e)
        {
            try
            {
                var InsertStatus1 = new DmnHireLocker();
                DmnHireLocker dmnBulkLocker = new DmnHireLocker();
                BllBulkLocker bllBulkLocker = new BllBulkLocker();
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = " Id as Field1";
                objSearch.Table = "tblLocker l left join tblLockerHire lh on lh.LockID=l.id";
                objSearch.WhereNdGroupBy = "where isnull(l.status,0)=1 and lh.Hired=1 and l.BranchId="+ BranchId + " and lh.BulkLockHireID=" + bulkid;
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    dmnBulkLocker.EntryDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    dmnBulkLocker.ExpDate = Convert.ToDateTime(dtpExpDate.Text);
                    dmnBulkLocker.ExpTime = Convert.ToDateTime(dtpExpTime.Text);
                    dmnBulkLocker.HireDate = Convert.ToDateTime(dtpHireDate.Text);
                    dmnBulkLocker.HireTime = Convert.ToDateTime(dtpHireTime.Text);
                    dmnBulkLocker.BulkLockHireID = bulkid;
                    InsertStatus1 = bllBulkLocker.AddEditData("prdInsert_Upd_BulkLockerHire", dmnBulkLocker);
                }
                if(InsertStatus1.BulkLockHireID==0)
                {
                    foreach (string controlName in controlNames)
                    {
                        Control control = Controls.Find(controlName, true).FirstOrDefault();
                        if (control != null)
                        {
                            control.Enabled = true;
                        }
                    }
                    MetroMessageBox.Show(this, "Locker Closed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmDisplayLockers FrmDisplayLockers = Application.OpenForms["FrmDisplayLockers"] as FrmDisplayLockers;
                    if (FrmDisplayLockers != null) // Check if the form exists
                    {
                        FrmDisplayLockers.FillComboLocker(0);
                    }
                    Refreshform();
                }
                  
            }
            catch (Exception ex)
            {
            }
        }

        private void txtHireForNo_Leave(object sender, EventArgs e)
        {
            RefreshHiredetails();
        }

        private void txtNoLockers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtDiscountAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            string input = (sender as TextBox).Text;
            if (e.KeyChar == '.')
            {
                if ((sender as TextBox).MaxLength == 4)
                    (sender as TextBox).MaxLength = 7;
                else
                {
                    if (input.Contains('.'))
                    {
                        (sender as TextBox).MaxLength = 7;
                    }
                    else
                    {
                        (sender as TextBox).MaxLength = 4;
                    }
                }

            }
            else
            {
                if (input.Contains('.'))
                {
                    (sender as TextBox).MaxLength = 7;
                }
                else
                {
                    (sender as TextBox).MaxLength = 4;
                }
            }
        }
   
    }
}
