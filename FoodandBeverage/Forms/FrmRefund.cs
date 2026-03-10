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
using System.Configuration;
using System.Drawing;

namespace FoodandBeverage.Forms
{
    public partial class FrmRefund : Form
    {
        #region Declaration
        ClsGeneral clsgen = new ClsGeneral();
        string reading_RFID = "";
        public string GameDB = Convert.ToString(ConfigurationManager.AppSettings["GameDB"]);
        #region Midascard
        public int CTyp = 0;
        public int DebitCustomerId = 0;
        public string DebitCardNumber = "";
        public int CardTypeno = 0;
        public int fmasterid = 0;
        public int Fmcardtypeno = 0;
        private bool isReadingCard = false;
        int payid = 0;
        #endregion

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
                _objSearch.WhereNdGroupBy = "where (PkId>0 OR OrderType=3) AND Status = 1 AND ISNULL(RefundStatus, 0) <> 1";
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

                        objSearch.Select =
                            "sm.SalesMasterId AS Field1, " +
                            "sm.PkId AS Field2, " +
                            "sm.NetAmount AS Field3, " +
                            "g.Caption AS Field4, " +
                            "sd.ItemName AS Field5, " +
                            "sd.Quantity AS Field6, " +
                            "sd.NetAmount AS Field7, " +
                            "sm.PaymentMode AS Field8, " +
                            "sm.OrderMasterId AS Field9, " +
                            "CASE " +
                            "   WHEN SUM(CASE WHEN od.Status = 2 THEN 1 ELSE 0 END) > 0 THEN 2 " +   // Cooking Started
                            "   WHEN SUM(CASE WHEN od.Status = 1 THEN 1 ELSE 0 END) > 0 THEN 1 " +   // Not Started
                            "   WHEN COUNT(od.OrderDetailsId) = SUM(CASE WHEN od.Status = 3 THEN 1 ELSE 0 END) THEN 3 " + // Completed
                            "   WHEN COUNT(od.OrderDetailsId) = SUM(CASE WHEN od.Status = 0 THEN 1 ELSE 0 END) THEN 0 " + // Cancelled
                            "   ELSE -1 " +
                            "END AS Field10";

                        objSearch.Table =
                            "tblSalesMaster sm " +
                            "LEFT JOIN tblSalesDetails sd ON sd.SalesMasterId = sm.SalesMasterId " +
                            "LEFT JOIN tblOrderMaster om ON om.SalesMasterId = sm.SalesMasterId " +
                            "LEFT JOIN tblOrderDetails od ON om.OrderMasterId = od.OrderMasterId " +
                            "LEFT JOIN tblGeneralOptions g ON g.GTypeId = 2 AND g.GTypeSubId = sm.PaymentMode";

                        objSearch.WhereNdGroupBy =
                            "WHERE sm.SalesMasterId = " + returnValue + " " +
                            "GROUP BY " +
                            "sm.SalesMasterId, sm.PkId, sm.NetAmount, g.Caption, " +
                            "sd.ItemName, sd.Quantity, sd.NetAmount, " +
                            "sm.PaymentMode, sm.OrderMasterId";

                        objSearch.OrderBy = "";

                        var gridSourceserachsd = bllSearchsd.GetSearchData("prdSearch", objSearch);

                        if (gridSourceserachsd.Count > 0)
                        {
                            dgv_productlist.Rows.Clear();
                            int OrderStatustype =Convert.ToInt32(gridSourceserachsd[0].Field10);
                            lblorderstatus.Visible = true;
                            switch (OrderStatustype)
                            {
                                case 1:
                                    lblorderstatus.Text = "Cooking Not Started";
                                    lblorderstatus.ForeColor = Color.Gray;
                                    break;

                                case 2:
                                    lblorderstatus.Text = "Cooking Started";
                                    lblorderstatus.ForeColor = Color.Orange;
                                    break;

                                case 3:
                                    lblorderstatus.Text = "Completed / Served";
                                    lblorderstatus.ForeColor = Color.Green;
                                    break;

                                case 0:
                                    lblorderstatus.Text = "Cancelled";
                                    lblorderstatus.ForeColor = Color.Red;
                                    break;

                                default:
                                    lblorderstatus.Text = "Partial / Mixed";
                                    lblorderstatus.ForeColor = Color.Blue;
                                    break;
                            }
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
                cmbRefundPayType.SelectedIndex = 0;
                cmbRefundReson.SelectedIndex = 0;
                lblorderstatus.Visible = false;

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
            try
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
                if (cmbRefundPayType.SelectedIndex == 1)
                {
                    if (string.IsNullOrWhiteSpace(lblcashrefundamt.Text) || lblcashrefundamt.Text == "0.00")
                    {
                        MessageBox.Show("Please Enter The Amount to Refund.",
                            "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                if (cmbRefundPayType.SelectedIndex == 2 && Convert.ToString(lblcardno.Text) == "")
                {
                    MessageBox.Show("Please Click on card Tap.",
                        "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                DmnRefund dmnRefund = new DmnRefund();
                BllRefund bllRefund = new BllRefund();

                dmnRefund.SalesMasterId = Convert.ToInt32(txtsalemasterid.Text);
                dmnRefund.OrderMasterId = Convert.ToInt32(txtordermasterid.Text);

                if (Convert.ToInt32(cmbRefundPayType.SelectedValue) == 1)
                    dmnRefund.RefundpayType = Convert.ToInt32(PaymentType.CASH);
                else if (Convert.ToInt32(cmbRefundPayType.SelectedValue) == 2)
                    dmnRefund.RefundpayType = Convert.ToInt32(PaymentType.MidasCard);

                dmnRefund.RefundReason = Convert.ToInt32(cmbRefundReson.SelectedValue);
                dmnRefund.ReasonNote = txtRenote.Text;
                if (cmbRefundPayType.SelectedIndex == 1)
                {
                    dmnRefund.CashRefund = Convert.ToDecimal(lblcashrefundamt.Text);
                }
                if (cmbRefundPayType.SelectedIndex == 2)
                {
                    dmnRefund.DebitLedgerId = Convert.ToInt32(lblDebitLedgerId.Text);
                    dmnRefund.CardType = Convert.ToInt32(lblCardType.Text);
                    dmnRefund.FMCardType = Convert.ToInt32(lblFMCardType.Text);
                    dmnRefund.DebitICardNo = Convert.ToString(lblcardno.Text);
                    dmnRefund.CashBalanceRefund = Convert.ToDecimal(lblnetamount.Text);
                }

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
                dmnRefund.DBname = GameDB;
                dmnRefund.CreatedDateTime = DateTime.Now;
                var data = bllRefund.AddRefund("prdInsertRefund", dmnRefund);
                if (data.Id > 0)
                    MessageBox.Show("Refund Amount Completed",
                           "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clearall();
            }
            catch(Exception ex)
            {

            }
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
                using (FoodandBeverage.Forms.UserControl.Frmnumdiskeypad frm = new FoodandBeverage.Forms.UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
                {
                    frm.ShowDialog();
                    kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                    frm.Dispose();
                }
                if (kdata > 0)
                {
                    if (kdata > Convert.ToDecimal(lblnetamount.Text))
                    {
                        MessageBox.Show("Refund Amount cannot be greater than Payable Amount",
                       "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        lblcashrefundamt.Text = kdata.ToString();
                        pnl_cus_cash_details.Visible = true;
                        tlp_cashrefund.BringToFront();
                    }

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
                if (isReadingCard) return;
                isReadingCard = true;
                string str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul(FrmLogin.CorpID);
                
                if (str1 != "Card not in contact")
                {
                    wificard_timer.Stop();
                    if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Invalid Card")
                        str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi(FrmLogin.CorpID);
                    str1 = RemoveSpecialCharacters(str1.Trim());
                    if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Cardnotincontact" || str1 == "InvalidCard")
                    {
                        MessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnlwifi.Visible = false;
                    }
                    else
                    {
                        wificard_timer.Stop();
                        MidascardCheck(str1);
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page tm_midascard_Tick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));

            }
            finally
            {
                isReadingCard = false;
            }
        }
        public void MidascardCheck(string cardno)
        {
            try
            {
                if (CheckEmployeeCard(cardno))
                {
                    MessageBox.Show("Employee ID cards cannot be used for purchases or recharges.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    CTyp = CheckCardType(cardno);
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    switch ((CardType)CTyp)
                    {
                        case CardType.FamilyCard:
                            if (!FrmDashboard.EnableFamilyCardInPayment)
                            {
                                MessageBox.Show("Family card payment option is disabled.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                wificard_timer.Start();
                                return;
                            }
                            CardTypeno = Convert.ToInt32(CardType.FamilyCard);
                            if (Fmcardtypeno == 1)
                            {
                                objSearch.Select = "FcardDetId Field1, CashBalance Field2";
                                objSearch.Table = GameDB + ".dbo.tblFamilyCardDetails";
                                objSearch.WhereNdGroupBy = "WHERE Status = 1 AND CardNo = '" + cardno + "'";
                            }
                            else
                            {
                                objSearch.Select = "FCardId Field1, CashBalance Field2";
                                objSearch.Table = GameDB + ".dbo.tblFamilyCardsMaster";
                                objSearch.WhereNdGroupBy = "WHERE Status = 1 AND FCardId = " + fmasterid;
                            }
                            break;

                        case CardType.FamilyCardMaster:
                            if (!FrmDashboard.EnableFamilyCardInPayment)
                            {
                                MessageBox.Show("Family card payment option is disabled.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                wificard_timer.Start();
                                return;
                            }
                            CardTypeno = Convert.ToInt32(CardType.FamilyCardMaster);
                            if (Fmcardtypeno == 1)
                            {
                                objSearch.Select = "FcardDetId Field1, CashBalance Field2";
                                objSearch.Table = GameDB + ".dbo.tblFamilyCardDetails";
                                objSearch.WhereNdGroupBy = "WHERE Status = 1 AND CardNo = '" + cardno + "'";
                            }
                            else
                            {
                                objSearch.Select = "FCardId Field1, CashBalance Field2";
                                objSearch.Table = GameDB + ".dbo.tblFamilyCardsMaster";
                                objSearch.WhereNdGroupBy = "WHERE Status = 1 AND MasterCardNo = '" + cardno + "'";
                            }
                            break;

                        case CardType.NormalCard:
                            CardTypeno = Convert.ToInt32(CardType.NormalCard);
                            objSearch.Select = "LedgerId Field1, CardBalance Field2";
                            objSearch.Table = GameDB + ".dbo.tblAccountLedger";
                            objSearch.WhereNdGroupBy = "WHERE Status = 1 AND ICardNo = '" + cardno + "'";
                            break;

                        case CardType.GroupCard:
                            if (!FrmDashboard.EnableGroupCardInPayment)
                            {
                                MessageBox.Show("Group Sale card payment option is disabled.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                wificard_timer.Start();
                                return;
                            }
                            if (CheckGroupsaleExpire(cardno))
                            {
                                wificard_timer.Stop();
                                MessageBox.Show("This Group Sale Card has expired or has no active member.\nPayment cannot be processed for expired cards.", "Group Card Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            CardTypeno = Convert.ToInt32(CardType.GroupCard);
                            objSearch.Select = "GLedgerId Field1,CardBalance Field2";
                            objSearch.Table = GameDB + ".dbo.tblGroup_AccountLedger";
                            objSearch.WhereNdGroupBy = "WHERE Status = 1 AND ICardNo = '" + cardno + "'";
                            break;

                        default:
                            MessageBox.Show("Invalid or unknown card type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                    }
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        pnl_cus_cash_details.Visible = true;
                        tlp_custom.BringToFront();
                        string modified_cardno = "XXXX XX" + cardno.Substring(cardno.Length - 6, 6);
                        lblcardno.Text = Convert.ToString(modified_cardno);
                        lblDebitLedgerId.Text = Convert.ToString(itemSource[0].Field1);
                        lblCardType.Text = Convert.ToString(CardTypeno);
                        lblFMCardType.Text = Convert.ToString(Fmcardtypeno);
                        lblcbalance.Text = Convert.ToString(itemSource[0].Field2);
                        decimal gtotal = Convert.ToDecimal(itemSource[0].Field2) + Convert.ToDecimal(lblnetamount.Text);
                        lblrefunbalance.Text = Convert.ToString(gtotal);
                        pnlwifi.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page wificard_timer_Tick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));

            }
        }

        public bool CheckEmployeeCard(string cardno)
        {
            try
            {
                string mastercardfullno = cardno;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "SUM(cnt) AS Field1";
                objSearch.Table = " ( SELECT COUNT(*) AS cnt FROM " + GameDB + ".dbo.tblEmployee WHERE Status=1 and ICardNo = '" + cardno + "' ) AS counts";
                objSearch.WhereNdGroupBy = "";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    return Convert.ToInt32(itemSource[0].Field1) != 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  CheckEmployeeCard()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
                return false;
            }
        }
        public int CheckCardType(string cardno)
        {
            try
            {
                string mastercardfullno = cardno;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = " " + Convert.ToInt32(CardType.FamilyCard) + " as Field1,FCardId Field2,CardAcessType Field3 from " + GameDB + ".dbo.tblFamilyCardDetails where CardNo='" + cardno + "' and Status=1 " +
                    " Union All Select " + Convert.ToInt32(CardType.FamilyCardMaster) + " as Field1,FCardId Field2,CardAcessType Field3 from " + GameDB + ".dbo.tblFamilyCardsMaster where MasterCardNo='" + cardno + "' and Status=1" +
                    " Union All Select " + Convert.ToInt32(CardType.NormalCard) + " as Field1,0 Field2,0 Field3 from " + GameDB + ".dbo.tblAccountLedger where ICardNo='" + cardno + "' and Status=1" +
                    " Union All Select " + Convert.ToInt32(CardType.GroupCard) + " as Field1,0 Field2,0 Field3";
                objSearch.Table = GameDB + ".dbo.tblGroup_AccountLedger";
                objSearch.WhereNdGroupBy = " where ICardNo='" + cardno + "' and Status=1";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    Fmcardtypeno = Convert.ToInt32(itemSource[0].Field3);
                    fmasterid = Convert.ToInt32(itemSource[0].Field2);
                    return Convert.ToInt32(itemSource[0].Field1);
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
                clsgen.LogException("Order page  CheckCardType()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));


            }
        }
        public bool CheckGroupsaleExpire(string cardno)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch1 = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "count(*) Field1";
                dmnSearch.Table = GameDB + ".dbo.tblGroup_AccountLedger";
                dmnSearch.WhereNdGroupBy = "where Status=1 and ICardNo='" + cardno + "' and ExpireDate>SYSDATETIME();";
                var itemsource = bllSearch1.GetSearchData("prdsearch", dmnSearch);
                if (itemsource.Count > 0)
                {
                    int activeCount = Convert.ToInt32(itemsource[0].Field1);
                    if (activeCount == 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                clsgen.LogException("Order page  CheckGroupsaleExpire()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void btnwificancel_Click(object sender, EventArgs e)
        {
            pnlwifi.Visible = false;
        }

        private void tlp_custom_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
