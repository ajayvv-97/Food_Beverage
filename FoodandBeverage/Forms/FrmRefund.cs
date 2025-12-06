using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmRefund : Form
    {
        #region Declaration
        ClsGeneral clsgen = new ClsGeneral();
        string reading_RFID = "";
        #endregion
        public FrmRefund()
        {
            InitializeComponent();
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRefund_Load(object sender, EventArgs e)
        {
            FillCombo();
        }

        private void FrmRefund_Resize(object sender, EventArgs e)
        {
            #region setting Corner Round 
            UIHelper.RoundCorners(tlp_navbar, 10, null);
            #endregion
        }

        private void btnsearchbill_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "ID,Token No,Net Amount,Created Date";
                _objSearch.Select = "SalesMasterId as Id,EntryNo Field1,NetAmount Field2,CreatedDateTime as Field3";
                _objSearch.Table = "tblSalesMaster ";
                _objSearch.WhereNdGroupBy = "where Status = 1 AND ISNULL(RefundStatus, 0) <> 1";
                _objSearch.OrderBy = " ORDER BY id DEsc";
                var gridSourceserach = bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSourceserach.Count == 0)
                {
                    CMessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))
                    {
                        var dialogResult = search.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                        {
                            returnValue = Convert.ToString(search._retValue);
                            txtsalemasterid.Text = Convert.ToString(search._retValue);
                        }
                    }
                    if (returnValue != "")
                    {                     
                        BllSearch<DmnGridFields> bllSearchsd = new BllSearch<DmnGridFields>();
                        DmnSearch objSearch = new DmnSearch();
                        objSearch.Select = "sm.SalesMasterId Field1,sm.PkId Field2,sm.NetAmount Field3,g.Caption Field4,sd.ItemName Field5,sd.Quantity Field6,sd.NetAmount Field7,sm.PaymentMode Field8,sm.OrderMasterid Field9";
                        objSearch.Table = "tblSalesMaster sm " +
                            "left join tblSalesDetails sd on sd.SalesMasterId=sm.SalesMasterId " +
                            "left join tblGeneralOptions g on g.GTypeId = 2 and g.GTypeSubId=sm.PaymentMode";
                        objSearch.WhereNdGroupBy = "where sm.SalesMasterId = "+ returnValue;
                        objSearch.OrderBy = "";
                        var gridSourceserachsd = bllSearchsd.GetSearchData("prdSearch", objSearch);
                        if (gridSourceserachsd.Count > 0)
                        {
                            dgv_productlist.Rows.Clear();

                            int slno = 1;

                            foreach (var item in gridSourceserachsd)
                            {
                                dgv_productlist.Rows.Add(
                                    slno,
                                    item.Field5,    
                                    item.Field6,     
                                    item.Field7     
                                );

                                slno++;
                            }
                            txtordermasterid.Text = Convert.ToString(gridSourceserachsd[0].Field9);
                            lblpaymenttype.Text = gridSourceserachsd[0].Field4.ToString();
                            lblnetamount.Text = gridSourceserachsd[0].Field3.ToString();
                            lbltoken.Text = gridSourceserachsd[0].Field2.ToString();
                            //if (Convert.ToInt32( gridSourceserachsd[0].Field8)==7)
                            //{
                            //    cmbRefundPayType.SelectedIndex = 1;
                            //}
                            //else
                            //{
                            //    cmbRefundPayType.SelectedIndex = 0;
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Refund page  btnsearchbill_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Refund));
            }
        }
        public void FillCombo()
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = " GTypeSubId as Id, Caption as Value ";
                objSearch.Table = " tblGeneralOptions";
                objSearch.WhereNdGroupBy = " where GTypeId=62 and status=0 ";
                objSearch.OrderBy = " order by Id";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmbRefundPayType.DataSource = gridSource;
                cmbRefundPayType.ValueMember = "Id";
                cmbRefundPayType.DisplayMember = "Value";

                bllSearch = new BllSearch<DmnComboBox>();
                objSearch = new DmnSearch();
                objSearch.Select = " GTypeSubId as Id, Caption as Value ";
                objSearch.Table = " tblGeneralOptions";
                objSearch.WhereNdGroupBy = " where GTypeId=64 and status=0 ";
                objSearch.OrderBy = " order by Id";
                var gridSource2 = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmbRefundReson.DataSource = gridSource2;
                cmbRefundReson.ValueMember = "Id";
                cmbRefundReson.DisplayMember = "Value";
            }
            catch (Exception ex)
            {
                clsgen.LogException("Refund page  FillCombo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Refund));
            }
        }

        private void cmbRefundReson_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbRefundReson.SelectedIndex == 5)
            {
                label16.Visible = true;
                txtRenote.Visible = true;
            }
            else
            {
                label16.Visible = false;
                txtRenote.Visible = false;
                txtRenote.Text = string.Empty;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
        }
        public void clearall()
        {
            try
            {
                pnl_cus_cash_details.Visible = false;
                btnrefund_cash_point.Visible = false;

                label16.Visible = false;
                txtRenote.Visible = false;
                txtRenote.Text = string.Empty;

                txtordermasterid.Text = "0";
                txtsalemasterid.Text = "0";
                lblpaymenttype.Text = string.Empty;
                lblnetamount.Text = "0";
                lbltoken.Text = string.Empty;

                dgv_productlist.Rows.Clear();
                dgv_productlist.Refresh();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Refund page  clearall()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Refund));
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnrefund_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtsalemasterid.Text))
            {
                MessageBox.Show("Please select a bill before continuing.",
                    "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbRefundPayType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a refund Pay Type",
                    "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbRefundReson.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a refund Reason",
                    "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbRefundReson.SelectedIndex == 5)
            {
                if (string.IsNullOrWhiteSpace(txtRenote.Text))
                {
                    MessageBox.Show("Please enter a reason in the note box.",
                        "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRenote.Focus();
                    return;
                }
            }

            DmnRefund dmnRefund = new DmnRefund();
            BllRefund bllRefund = new BllRefund();

            dmnRefund.SalesMasterId = Convert.ToInt32(txtsalemasterid.Text);
            dmnRefund.OrderMasterId = Convert.ToInt32(txtordermasterid.Text);
            dmnRefund.RefundpayType = Convert.ToInt32(cmbRefundPayType.SelectedValue);
            dmnRefund.RefundReason = Convert.ToInt32(cmbRefundReson.SelectedValue);
            dmnRefund.ReasonNote = txtRenote.Text;
            dmnRefund.CashRefund = Convert.ToDecimal(lblcashrefundamt.Text);

            //// Balances
            //dmnRefund.CashBalance = Convert.ToDecimal(lblCashBal.Text);
            //dmnRefund.BonusBalance = Convert.ToDecimal(lblBonusBal.Text);
            //dmnRefund.PointBalnce = Convert.ToDecimal(lblPointBal.Text);  // Correct spelling
            //dmnRefund.TokenBalance = Convert.ToDecimal(lblTokenBal.Text);

            //// Refund amounts
            //dmnRefund.CashBalanceRefund = dmnRefund.CashRefund;
            //dmnRefund.BonusRefund = Convert.ToDecimal(lblReBonusBal.Text);
            //dmnRefund.PointRefund = Convert.ToDecimal(lblRePointBal.Text);
            //dmnRefund.TokenRefund = Convert.ToDecimal(lblReTokenBal.Text);


            dmnRefund.CreatedBy = FrmDashboard.UserId;
            dmnRefund.BranchId = FrmDashboard.BranchId;
            dmnRefund.Status = 1;
            dmnRefund.CreatedDateTime = DateTime.Now;
            var data = bllRefund.AddRefund("prdInsertRefund", dmnRefund);
            if (data.Id > 0)
                clearall();
        }

        private void cmbRefundPayType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbRefundPayType.SelectedIndex == 1)
            {
                btnrefund_cash_point.Text = "Cash Refund";
                btnrefund_cash_point.Tag = 1;
                btnrefund_cash_point.Visible = true;
            }                
            else if (cmbRefundPayType.SelectedIndex == 2)
            {
                btnrefund_cash_point.Text = "Card Tap";
                btnrefund_cash_point.Tag = 2;
                btnrefund_cash_point.Visible = true;
            }
            
        }

        private void btnrefund_cash_point_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnrefund_cash_point.Tag) == 2)
            {
                wificard_timer.Start();
                UIHelper.CenterAndResize(pnlwifi, 40, 45);
            }
            else
            {
                decimal kdata = 0;
                using (FoodandBeverage.Forms.UserControl.Frmnumdiskeypad frm = new FoodandBeverage.Forms.UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.Number)))
                {
                    frm.ShowDialog();
                    kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                    frm.Dispose();
                }
                if (kdata > 0)
                {
                    lblcashrefundamt.Text = kdata.ToString();
                    pnl_cus_cash_details.Visible = true;
                    tlp_cashrefund.BringToFront();
                }
            }
        }
        public string RemoveSpecialCharacters(string str)
        {
            try
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page RemoveSpecialCharacters()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order)); 
                return string.Empty;
            }
        }
        private void wificard_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                reading_RFID = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul(FrmLogin.Compid);
                if (reading_RFID != "Card not in contact")
                {
                    wificard_timer.Stop();
                    if (reading_RFID == "" || reading_RFID == "empty" || string.IsNullOrWhiteSpace(reading_RFID) || reading_RFID == "FailAuthenticationFailAuthenticationFailAuthentication")
                        reading_RFID = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi(FrmLogin.Compid);
                    reading_RFID = RemoveSpecialCharacters(reading_RFID.Trim());
                    if (reading_RFID == "" || string.IsNullOrWhiteSpace(reading_RFID) || reading_RFID == "FailAuthenticationFailAuthenticationFailAuthentication" || reading_RFID == "Cardnotincontact")
                    {
                        CMessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        wificard_timer.Stop();

                        BllSearch<DmnAccountLedger> bllSearch = new BllSearch<DmnAccountLedger>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "*";
                        dmnSearch.Table = " mPOS_DBNew.dbo.tblAccountLedger";
                        dmnSearch.WhereNdGroupBy = "Where status = 1 and  ICardNo = '" + reading_RFID + "'";
                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData_CardAcc("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            pnl_cus_cash_details.Visible = true;
                            tlp_custom.BringToFront();
                            string modified_cardno = "XXXX XX" + reading_RFID.Substring(reading_RFID.Length - 6, 6);
                            lblcardno.Text = Convert.ToString(modified_cardno);
                            lblcbalance.Text = Convert.ToString(gridSource[0].CardBalance);
                            decimal gtotal = Convert.ToDecimal(gridSource[0].CardBalance) + Convert.ToDecimal(lblnetamount.Text);
                            lblrefunbalance.Text =Convert.ToString(gtotal);
                        }
                        else
                        {
                            CMessageBox.Show("No account found for the tapped card. Please try again with a different card.", "Card Not Recognized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            wificard_timer.Start();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page wificard_timer_Tick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btnwificancel_Click(object sender, EventArgs e)
        {
            pnlwifi.Visible = false;
        }
    }
}
