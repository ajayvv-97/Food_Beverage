using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using MetroFramework;
using MetroFramework.Controls;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MarginPOSNew.Forms.Transactions
{
    public partial class FrmPurchase : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        ClsGeneral objGeneral = new ClsGeneral();
        //long BranchId = Convert.ToInt64(FrmDashboard.BranchId);
        //long UserId = Convert.ToInt64(FrmDashboard.UserId);
        //long FinancialYearId = Convert.ToInt64(FrmDashboard.FinancialYearId);
        long UserId = Forms.UserAccounts.FrmLogin.UserId;//1
        long BranchId =Forms.UserAccounts.FrmLogin.BranchId;
        long pUserType = Forms.UserAccounts.FrmLogin.UserAccountTypeId;
        long FinancialYearId = Convert.ToInt64(FrmDashboard.FinancialYearId);


        long PurchaseMasterId = 0; // For Update
        long ItemId = 0;
        long SlNo = 0;
        long LedgerId = 0; //For Customer Popup
        string LedgerCode = "";
        string existingAddress = "";
        string existingMobile = "";

        #region edit item master

        decimal sellper;
        decimal purchaserate;
        decimal mrp;

        #endregion

        //summary

        decimal TotalNumber = 0;
        decimal AmountSummary = 0;
        decimal DiscountSummary = 0;
        decimal GrossSummary = 0;
        decimal TaxSummary = 0;
        decimal CessSummary = 0;
        decimal TotalAmtSummary = 0;
        decimal SalesRatemrp = 0;
        //End Summary

        decimal qty;
        decimal rate;
        decimal taxper;
        decimal disc;
        decimal discper;
        decimal cessper;
        int taxIncluded;
        decimal miscTotal = 0;
        #endregion

        #region GridIndex

        const int iSlNo = 0;
        const int iItemId = 1;
        const int iItemCode = 2;
        const int iBarcode = 3;
        const int iName = 4;
        const int iUnitId = 5;
        const int iUnit = 6;
        const int iQuantity = 7;
        const int iPurchaseRate = 8;
        const int iSellingPer = 9;
        const int iSalesRate = 10;
        const int iMRP = 11;
        const int iAmount = 12;
        const int iDiscPer = 13;
        const int iDiscAmount = 14;
        const int iGrossAmount = 15;
        const int iPurchaseTaxIncl = 16;
        const int iTaxText = 17;
        const int iTaxAmount = 18;
        const int iTaxType = 19;
        const int iCessPer = 20;
        const int iCessAmount = 21;
        const int iPurchaseDetailId = 22;//used in edit od Sales
        const int iRate = 23;
        const int iTotalAmount = 24;
        const int iCost = 25;
        const int iMargin = 26;
        const int iItemSaveAmount = 27;//savings in each item
        const int iLockSaleRate = 28;
        const int iQohToSubtract = 29;//this amount is decreased from qoh
        const int iItemQOH = 30;//to store item qoh
        const int iStockItem = 31;
        const int iItemSelPer = 32;
        const int iItemMrp = 33;
        const int iTaxPer = 34;
        const int iExpdate = 35;
        #endregion

        #region Functions
        public FrmPurchase()
        {
            InitializeComponent();
        }
        #region InvoiceNo. Generation
        public void PurchaseInvoiceNo()
        {
            BllSearch<DmnBranch> bllSearch = new BllSearch<DmnBranch>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "PurchaseSrlNo";
            objSearch.Table = " tblBranch ";
            objSearch.WhereNdGroupBy = " Where BranchId=" + BranchId;
            var srlNo = bllSearch.GetSearchData("prdSearch", objSearch);
            mtxtInvNo.Text = Convert.ToString(srlNo[0].PurchaseSrlNo + 1);
        }
        #endregion
        public bool IsSavable()
        {
            Control[] controllist = { mtxtDocNo, mtxtInvNo, mdtpInvDate, mcmbTax, mcmbMode, mtxtLedger };
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void RefreshForm()
        {
            PurchaseMasterId = 0;
            miscTotal = 0;
            ItemId = 0;
            SlNo = 0;
            existingAddress = "";
            existingMobile = "";
            TotalNumber = 0;
            AmountSummary = 0;
            DiscountSummary = 0;
            GrossSummary = 0;
            TaxSummary = 0;
            CessSummary = 0;
            TotalAmtSummary = 0;
            mdtpInvDate.Text = Convert.ToString(DateTime.Now.Date);
            mdtpDueDate.Text = Convert.ToString(DateTime.Now.Date);
            mdtpDocDate.Text = Convert.ToString(DateTime.Now.Date);
            MetroTextBox[] listTbx = { mtxtLedger, mtxtAddress, mtxtCreditDays, mtxtDocNo, mtxtLedger, mtxtMobile, mtxtRemarks };
            objGeneral.ResetSelectedMetroTextBoxes(listTbx);
            mtxtAdjust.Text = Convert.ToString("0.00");
            mtxtAmount.Text = Convert.ToString("0.00");
            mtxtGrandTot.Text = Convert.ToString("0.00");
            mtxtNetAmount.Text = Convert.ToString("0.00");
            mtxtMiscCharges.Text = Convert.ToString("0.00");
            mtxtNoOfItems.Text = Convert.ToString("0");
            mtxtTot.Text = Convert.ToString("0.00");
            mtxtTotAmount.Text = Convert.ToString("0.00");
            mtxtTotCess.Text = Convert.ToString("0.00");
            mtxtTotDisc.Text = Convert.ToString("0.00");
            mtxtTotDiscAmnt.Text = Convert.ToString("0.00");
            mtxtTotDiscPer.Text = Convert.ToString("0.00");
            mtxtTotGrss.Text = Convert.ToString("0.00");
            mtxtTotTax.Text = Convert.ToString("0.00");
            //objGeneral.ResetSelectedMetroTextBoxes(listTbx);
            dgvItemDet.Rows.Clear();
            //dgvItemDet.Rows[0].Cells[iBarcode].Selected = true;
            mcmbTransNo.DataSource = null;
            mcmbTransNo.Items.Clear();
            FillTaxComboBox();
            FillParticularComboBox();
            FillPaymentComboBox();
            FillLedgerComboBox();
            FillTaxTypeComboBox();
            //FillTransNoComboBox();
            PurchaseInvoiceNo();
            mlvMisc.Items.Clear();
            mtxtTransportNoAdd.Text = "";
            mcmbTransNo.SelectedText = mtxtTransportNoAdd.Text.Trim();
            pnlTransPort.Hide();
            mtxtDocNo.Focus();
            //Focus

            //ActiveControl = dgvItemDet;
            //dgvItemDet.CurrentCell = dgvItemDet.Rows[0].Cells[3];
            //dgvItemDet.CurrentCell.Selected = true;
            ////dgvItemDet.Focus();
            //mcmbSign.SelectedItem = "+";


            //end focus


        }

        #region Combobox Filling
        public void FillParticularComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "GeneralId,Caption";
            objSearch.Table = " tblGeneralOptions g LEFT JOIN tblGType gt ON g.GTypeId =  gt.GTypeId ";
            objSearch.WhereNdGroupBy = " Where gt.GType='MISCELLANEOUS CHARGES' AND g.Status = 0";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbParticular.DataSource = gridSource;
            mcmbParticular.ValueMember = "Id";
            mcmbParticular.DisplayMember = "Value";

        }
        public void FillTransNoComboBox(string ledgerCode)// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "GeneralId,g.GTypeSubText2";
            objSearch.Table = " tblGeneralOptions g LEFT JOIN tblGType gt ON g.GTypeId =  gt.GTypeId ";
            objSearch.WhereNdGroupBy = " Where gt.GType='TRANSPORTATION NO' AND g.GTypeSubText1='" + ledgerCode + "' AND g.Status = 0";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbTransNo.DataSource = gridSource;
            mcmbTransNo.ValueMember = "Id";
            mcmbTransNo.DisplayMember = "Value";

        }
        public void FillTaxComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "GeneralId,Caption";
            objSearch.Table = " tblGeneralOptions g LEFT JOIN tblGType gt ON g.GTypeId =  gt.GTypeId ";
            objSearch.WhereNdGroupBy = " Where gt.GType='TAX MODE' AND g.Status = 0";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbTax.DataSource = gridSource;
            mcmbTax.ValueMember = "Id";
            mcmbTax.DisplayMember = "Value";
            mcmbTax.SelectedIndex = mcmbTax.FindString("TAX WITH CESS");
            // mcmbTax.SelectedItem = "TAX";
        }
        public void FillTaxTypeComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "GeneralId,Caption";
            objSearch.Table = " tblGeneralOptions g LEFT JOIN tblGType gt ON g.GTypeId =  gt.GTypeId ";
            objSearch.WhereNdGroupBy = " Where gt.GType='TAX TYPE' AND (g.Status = 0 OR g.Status = 2)";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            cnTaxText.DataSource = gridSource;
            cnTaxText.ValueMember = "Id";
            cnTaxText.DisplayMember = "Value";
            //cnTaxPer.SelectedIndex = cnTaxPer.FindString("EXEMPT GST");
        }
        public void FillPaymentComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "GeneralId,Caption";
            objSearch.Table = " tblGeneralOptions g LEFT JOIN tblGType gt ON g.GTypeId =  gt.GTypeId ";
            objSearch.WhereNdGroupBy = " Where gt.GType='PAYMENT MODE' AND g.Status = 0";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbMode.DataSource = gridSource;
            mcmbMode.ValueMember = "Id";
            mcmbMode.DisplayMember = "Value";
            mcmbMode.SelectedIndex = mcmbMode.FindString("CASH");
            //mcmbMode.SelectedItem = "CASH";
        }
        #endregion
        public void FillLedgerComboBox()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = "LedgerId,LedgerName as value";
            dmnSearch.Table = " tblAccountLedger L LEFT JOIN tblAccountGroup Ac  ON L.AccountGroupId = Ac.AccountGroupId ";
            dmnSearch.WhereNdGroupBy = " WHERE (Ac.AccountGroupName = 'Sundry Creditors') AND L.Status = 0 And L.BranchId = " + BranchId;
            dmnSearch.OrderBy = "order by value asc";
            var comboSource = bllSearch.GetSearchData("prdComboFill", dmnSearch);
            mcmbLedger.DataSource = comboSource;
            mcmbLedger.ValueMember = "Id";
            mcmbLedger.DisplayMember = "Value";
            mcmbLedger.SelectedIndex = mcmbLedger.FindString("CASH PURCHASE");
        }

        public void Itempopup()
        {
            string returnValue = string.Empty;
            DataTable dtColumnAlign = new DataTable();
            dtColumnAlign.Columns.Add();
            dtColumnAlign.Columns.Add();
            dtColumnAlign.Rows.Add("6", "R");
            dtColumnAlign.Rows.Add("7", "N2");
            dtColumnAlign.Rows.Add("8", "R");
            dtColumnAlign.Rows.Add("9", "R");
            dtColumnAlign.Rows.Add("10", "R");
            DmnSearch _objSearch = new DmnSearch();
            _objSearch.ColumnHeaders = "Id,Name,Barcode,Code,Category,Unit,PurchaseRate,SalesRate,MRP,Tax(%),QOH";
            _objSearch.Select = "I.ItemId as Id,I.SearchName as Field1,I.Barcode as Field2, I.ItemCode as Field3, C.CategoryName as Field4,U.UnitName as Field5," +
                "I.PurchaseRate1 as Field6,I.SaleRate as Field7, I.MRP1 as Field8, (I.IGSTPer) as Field9, I.QuantityOnHand as Field10";
            _objSearch.Table = " tblItem I LEFT JOIN tblCategory C  ON I.CategoryId = C.CategoryId LEFT JOIN tblUnit U ON I.MainUnitId=U.UnitId";
            _objSearch.WhereNdGroupBy = " WHERE I.Status=0 AND I.BranchId = " + BranchId;
            _objSearch.OrderBy = " ORDER BY I.SearchName ASC";
            using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.AllCharacters, this.Width - 90, (this.Height - 200), this.Location.Y + 70, this.Location.X + 100, dtColumnAlign))
            {
                var dialogResult = search.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    returnValue = Convert.ToString(search._retValue);
                }
            }
            if (returnValue != string.Empty)
            {
                //int RepeatRowIndex = -1;
                //for (int i = 0; i < dgvItemDet.Rows.Count; ++i)
                //{
                //    if (returnValue == Convert.ToString(dgvItemDet.Rows[i].Cells[iItemId].Value))
                //    {
                //        RepeatRowIndex = i;
                //    }
                //}
                //if (RepeatRowIndex > -1)
                //{
                //    dgvItemDet.CurrentCell = dgvItemDet.Rows[RepeatRowIndex].Cells[iQuantity];
                //    MetroMessageBox.Show(this, "Item already exist!");
                //}
                //if (RepeatRowIndex == -1 && !string.IsNullOrWhiteSpace(returnValue))
                //{
                if (dgvItemDet.CurrentCell == null)
                {
                    MetroMessageBox.Show(this, "Select Row!");
                }
                else
                {
                    int CurrentIndex = dgvItemDet.CurrentRow.Index;
                    if (decimal.Parse(Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iTotalAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iTotalAmount].Value)) > 0)
                    {
                        dgvItemDet.Rows.Remove(dgvItemDet.Rows[CurrentIndex]);
                    }
                    dgvItemDet.Rows.Insert(CurrentIndex, 1);
                    ItemId = Convert.ToInt64(returnValue.ToString());
                    dgvItemDet.Rows[CurrentIndex].Cells[iQuantity].Value = 1;
                    Itemdetails(CurrentIndex, ItemId);
                    TotalSummary();

                    dgvItemDet.CurrentCell = dgvItemDet.Rows[CurrentIndex].Cells[iQuantity];
                    dgvItemDet.Rows[CurrentIndex + 1].Cells[iSlNo].Value = "0";
                }
                //}
            }
        }
        public void DiscountAmtToPer(int currow, decimal discAmount)
        {
            decimal amount;
            decimal discountPer;
            DiscountSummary = 0;
            dgvItemDet.Rows[currow].Cells[iDiscAmount].Value = discAmount;
            amount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iAmount].Value));
            discountPer = (discAmount / amount) * 100;
            for (int i = 0; i < dgvItemDet.Rows.Count; ++i)
            {
                DiscountSummary += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscAmount].Value));
            }

            dgvItemDet.CurrentRow.Cells[iDiscPer].Value = Convert.ToString(discountPer.ToString("0.00"));
            mtxtTotDisc.Text = Convert.ToString(DiscountSummary.ToString("0.00"));
        }
        public void DiscountPerToAmt(int currow, decimal discountPer)
        {
            decimal discAmount;
            decimal amount;
            DiscountSummary = 0;
            amount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iAmount].Value));
            discAmount = (discountPer * amount) / 100;
            dgvItemDet.Rows[currow].Cells[iDiscAmount].Value = Convert.ToString(discAmount.ToString("0.00"));
            for (int i = 0; i < dgvItemDet.Rows.Count; ++i)
            {
                DiscountSummary += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscAmount].Value));
            }
            mtxtTotDisc.Text = Convert.ToString(DiscountSummary.ToString("0.00"));
        }
        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var sendingCB = sender as DataGridViewComboBoxEditingControl;
                var currentcell = dgvItemDet.CurrentCellAddress;
                //var sendingCB = sender as DataGridViewComboBoxEditingControl;
                int cmbSelectedIndex = sendingCB.SelectedIndex;
                int CurrentIndex = dgvItemDet.CurrentRow.Index;
                int curCel = dgvItemDet.CurrentCell.ColumnIndex;
                if (curCel == iTaxText)
                {
                    long cmbValue = Convert.ToInt64(((ComboBox)sender).SelectedValue);
                  if (cmbValue is long && cmbValue > 0)
                    {
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();

                        DmnSearch objSearch = new DmnSearch();
                        objSearch = new DmnSearch();
                        objSearch.Select = "GTypeSubValue1 AS Field1 ";
                        objSearch.Table = " tblGeneralOptions ";
                        objSearch.WhereNdGroupBy = " WHERE GeneralId =" + cmbValue;
                        objSearch.OrderBy = "";
                        var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                        dgvItemDet.Rows[CurrentIndex].Cells[iTaxType].Value = Convert.ToString(cmbValue);
                        dgvItemDet.Rows[CurrentIndex].Cells[iTaxPer].Value = Convert.ToString(itemSource[0].Field1);
                        if (Convert.ToDecimal(dgvItemDet.Rows[CurrentIndex].Cells[iTaxPer].Value) >= 5 && mcmbTax.Text == "TAX WITH CESS")
                        {
                            dgvItemDet.Rows[CurrentIndex].Cells[iCessPer].Value = 1;
                        }
                        else
                        {
                            dgvItemDet.Rows[CurrentIndex].Cells[iCessPer].Value = 0;
                        }
                        ItemTotal(CurrentIndex, Convert.ToDecimal(dgvItemDet.CurrentRow.Cells[iPurchaseRate].Value), iPurchaseRate);
                        TotalSummary();
                    }
                }
                if (curCel == iUnit)
                {
                    DataGridViewComboBoxCell cel = (DataGridViewComboBoxCell)dgvItemDet.Rows[currentcell.Y].Cells[iUnit];
                    cel.Value = sendingCB.EditingControlFormattedValue.ToString();

                    if (cmbSelectedIndex != -1)
                    {

                        var cmbValue = ((ComboBox)sender).SelectedValue;
                        if (cmbValue != null)
                        {
                            if (cmbValue is long)
                            {

                            }
                            else
                            {
                                string arr = cmbValue.ToString();
                                string[] values = arr.Split(',');
                                string value = values[0].ToString();
                                string[] unit = value.Split('=');
                                int lgnth = unit.Length;
                                if (lgnth > 1)
                                {
                                    long id = Convert.ToInt32(unit[1].ToString());
                                    cmbValue = id;
                                }

                            }
                            int selectedUnitId = Convert.ToInt32(cmbValue);
                            long selectedItemId = Convert.ToInt64(dgvItemDet.Rows[CurrentIndex].Cells[iItemId].Value);

                            //                 MessageBox.Show(cmbSelectedIndex.ToString() + "\n" + selectedUnitId.ToString() + "\n" + selectedItemId.ToString());
                            #region set unit rate to the  grid view based on the unit selected

                            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();

                            DmnSearch objSearch = new DmnSearch();
                            objSearch = new DmnSearch();
                            objSearch.Select = " SellingPer as Field1, case MainUnitId when '" + selectedUnitId + "' then PurchaseRate1 ELSE PurchaseRate2 end AS Field2, " +
                                "case MainUnitId when '" + selectedUnitId + "' then MRP1" +
                                " ELSE MRP2 end AS Field3,case MainUnitId when '" + selectedUnitId + "' then PurchaseRate1" +
                                " ELSE PurchaseRate2 end AS Field4, " +
                                "case MainUnitId when '" + selectedUnitId + "' then PurchaseRate1*(100+SellingPer)/100  " +
                                "ELSE PurchaseRate2*(100+SellingPer)/100 end AS Field5 ," +
                                "case MainUnitId when '" + selectedUnitId + "' then 1 " +
                                "Else PacketQty end as Field6";
                            objSearch.Table = " tblItem  ";
                            objSearch.WhereNdGroupBy = "where ItemId=" + selectedItemId;
                            objSearch.OrderBy = "";
                            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                            dgvItemDet.Rows[CurrentIndex].Cells[iPurchaseRate].Value = Convert.ToDecimal(itemSource[0].Field4.ToString());
                            dgvItemDet.Rows[CurrentIndex].Cells[iSalesRate].Value = (Convert.ToDecimal(itemSource[0].Field5).ToString("#.00"));
                            dgvItemDet.Rows[CurrentIndex].Cells[iQohToSubtract].Value = Convert.ToString(itemSource[0].Field6);
                            #region changing the mrp and unit id when selection changed

                            dgvItemDet.Rows[CurrentIndex].Cells[iRate].Value = (Convert.ToDecimal(itemSource[0].Field4).ToString("#.00"));//this is the irate
                            dgvItemDet.Rows[CurrentIndex].Cells[iMRP].Value = Convert.ToString(itemSource[0].Field3.ToString());
                            dgvItemDet.Rows[CurrentIndex].Cells[iUnitId].Value = selectedUnitId;
                            dgvItemDet.Rows[CurrentIndex].Cells[iQohToSubtract].Value = (Convert.ToDecimal(itemSource[0].Field6).ToString("#.00"));

                            #endregion

                            //#region display error if items more than qoh tried to buy

                            //decimal PresentQoH = Convert.ToDecimal(dgvItemDet.Rows[CurrentIndex].Cells[iItemQOH].Value);
                            //decimal qty = Convert.ToDecimal(dgvItemDet.Rows[CurrentIndex].Cells[iQuantity].Value);
                            //decimal multiplier = Convert.ToDecimal(dgvItemDet.Rows[CurrentIndex].Cells[iQohToSubtract].Value);
                            //if (PresentQoH < (qty * multiplier))
                            //{
                            //    MetroMessageBox.Show(this, "\nQuantity is above QOH.\nPlease update item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150);
                            //}
                            //dgvItemDet.Rows[CurrentIndex].Cells[iItemId].Selected = true;//to set focus after error message
                            //#endregion

                            dgvItemDet.Rows[CurrentIndex].Cells[iItemMrp].Value = Convert.ToString(itemSource[0].Field3);

                            ItemTotal(CurrentIndex, Convert.ToDecimal(itemSource[0].Field4), iPurchaseRate);
                            TotalSummary();
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
               // MetroMessageBox.Show(this, ex.Message, "Error", 150);
            }
        }
        public void ItemTotal(int currowIndex, decimal editingValue, int columnIndex)
        {
            if (string.IsNullOrWhiteSpace(Convert.ToString(editingValue)))
            {
                editingValue = 0;
            }
            decimal amount;
            decimal grossAmount;
            decimal taxRate=0;
            decimal cessAmount=0;
            decimal netAmount;
            decimal cost;
            decimal margin;

            qty = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value));
            rate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iPurchaseRate].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iPurchaseRate].Value));
            taxper = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iTaxPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iTaxPer].Value));
            discper = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iDiscPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iDiscPer].Value));

            cessper = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iCessPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iCessPer].Value));
            taxIncluded = int.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iPurchaseTaxIncl].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iPurchaseTaxIncl].Value));
            if (iPurchaseRate == columnIndex)
            {
                rate = editingValue;
                dgvItemDet.Rows[currowIndex].Cells[iPurchaseRate].Value = decimal.Parse(Convert.ToString(rate) == string.Empty ? "0.00" : Convert.ToString(rate));
            }
            else if (iQuantity == columnIndex)
            {
                qty = editingValue;
                dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value = decimal.Parse(Convert.ToString(qty) == string.Empty ? "0.00" : Convert.ToString(qty));
            }
            amount = qty * rate;
            dgvItemDet.Rows[currowIndex].Cells[iAmount].Value = Convert.ToString(amount.ToString("0.00"));
            //DiscountAmtToPer(currowIndex, disc);
            DiscountPerToAmt(currowIndex, discper);
            disc = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iDiscAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iDiscAmount].Value));
            grossAmount = decimal.Parse(Convert.ToString(amount - disc) == string.Empty ? "0.00" : Convert.ToString(amount - disc));
            dgvItemDet.Rows[currowIndex].Cells[iGrossAmount].Value = Convert.ToString(grossAmount.ToString("0.00"));
        
            if (taxIncluded == 0) //Tax Exclusive
            {
                if (Convert.ToString(mcmbTax.Text) == "NON TAX")
                {
                    taxper = 0;
                    taxRate = 0;
                    cessper = 0;
                    cessAmount = 0;
                }
                //else if (Convert.ToString(mcmbTax.Text) == "TAX")
                //{
                //    taxRate = (grossAmount * taxper) / 100;
                //    cessper = 0;
                //    cessAmount = 0;
                //}
                //else
                //{
                //    taxRate = (grossAmount * taxper) / 100;
                //    cessAmount = (grossAmount * cessper) / 100;
                //}
                netAmount = (grossAmount * (100 + taxper + cessper)) / 100;
            }
            else //Tax Inclusive
            {
                    if (Convert.ToString(mcmbTax.Text) == "NON TAX")
                    {
                        taxper = 0;
                        taxRate = 0;
                        cessper = 0;
                        cessAmount = 0;
                    dgvItemDet.Rows[currowIndex].Cells[iCessPer].Value = 0; 
                    dgvItemDet.Rows[currowIndex].Cells[iTaxPer].Value = 0;
                    dgvItemDet.Rows[currowIndex].Cells[iTaxType].Value = 7;

                }
                    else if (Convert.ToString(mcmbTax.Text) == "TAX")
                    {
                    //taxRate = (grossAmount / (100 + taxper)) * taxper;
                    taxRate = (grossAmount * ( taxper/100));
                    cessper = 0;
                    cessAmount = 0;
                    dgvItemDet.Rows[currowIndex].Cells[iCessPer].Value = 0;

                }
                else if (Convert.ToString(mcmbTax.Text) == "TAX WITH CESS")
                {
                    taxRate = (grossAmount * (taxper / 100)); 
                    cessAmount = 0;
                    if (taxper > 5)
                    {
                        cessper = 1;
                        cessAmount = grossAmount * (cessper / 100);
                    }
                }
            }
            
            netAmount = grossAmount+ taxRate+ cessAmount;
            
            dgvItemDet.Rows[currowIndex].Cells[iTaxAmount].Value = Convert.ToString(Math.Round(taxRate, 2));
            dgvItemDet.Rows[currowIndex].Cells[iCessAmount].Value = Convert.ToString(Math.Round(cessAmount, 2));
            dgvItemDet.Rows[currowIndex].Cells[iTotalAmount].Value = Convert.ToString(Math.Round(netAmount, 2));
            if (Convert.ToDecimal(dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value) > 0)
            {
                cost = Convert.ToDecimal(dgvItemDet.Rows[currowIndex].Cells[iTotalAmount].Value) / Convert.ToDecimal(dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value);
                dgvItemDet.Rows[currowIndex].Cells[iCost].Value = Convert.ToString(cost.ToString("0.00"));
                margin = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iMRP].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iMRP].Value)) - cost;
                dgvItemDet.Rows[currowIndex].Cells[iMargin].Value = Convert.ToString(margin.ToString("0.00"));
            }
            GridSummary();
        }
        public void GridSummary()
        {
            TotalNumber = 0;
            AmountSummary = 0;
            DiscountSummary = 0;
            GrossSummary = 0;
            TaxSummary = 0;
            CessSummary = 0;
            TotalAmtSummary = 0;

            for (int i = 0; i < dgvItemDet.Rows.Count; ++i)
            {

                TotalNumber += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value));
                AmountSummary += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iAmount].Value));
                DiscountSummary += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscAmount].Value));
                GrossSummary += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iGrossAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iGrossAmount].Value));
                TaxSummary += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value));
                CessSummary += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iCessAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iCessAmount].Value));
                TotalAmtSummary += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTotalAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTotalAmount].Value));
            }
            mtxtNoOfItems.Text = Convert.ToString(TotalNumber.ToString("0.000"));
            mtxtTotAmount.Text = Convert.ToString(AmountSummary.ToString("0.00"));
            mtxtTotDisc.Text = Convert.ToString(DiscountSummary.ToString("0.00"));
            mtxtTotGrss.Text = Convert.ToString(GrossSummary.ToString("0.00"));
            mtxtTotTax.Text = Convert.ToString(TaxSummary.ToString("0.00"));
            mtxtTotCess.Text = Convert.ToString(CessSummary.ToString("0.00"));
            mtxtTot.Text = Convert.ToString(TotalAmtSummary.ToString("0.00"));
        }

        #region Fill Item Details in gridview
        public void Itemdetails(int CurrentIndex, long ItemId)
        {
            SlNo = CurrentIndex;
            BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "I.ItemId,I.ItemCode,I.Barcode,I.SearchName,I.PurchaseTaxIncl,I.MainUnitId,U.UnitName,I.PurchaseRate1," +
                "I.SGSTPer,I.CGSTPer,I.IGSTPer,I.CessPer,I.TaxType,I.MRP1,I.SellingPer,I.LockSaleRate,I.PacketQty,I.QuantityOnhand,I.StockItem,G.Caption,I.SaleRate";
            objSearch.Table = " tblItem I LEFT JOIN tblUnit U ON I.MainUnitId=U.UnitId LEFT JOIN tblGeneralOptions G ON I.TaxType = G.GeneralId";
            objSearch.WhereNdGroupBy = "Where I.ItemId = " + ItemId;
            objSearch.OrderBy = "Order By I.ItemName ";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            dgvItemDet.Rows[CurrentIndex].Cells[iSlNo].Value = SlNo + 1;
            string vBarcode = Convert.ToString(itemSource[0].Barcode);
            dgvItemDet.Rows[CurrentIndex].Cells[iItemId].Value = Convert.ToString(itemSource[0].ItemId);
            dgvItemDet.Rows[CurrentIndex].Cells[iItemCode].Value = Convert.ToString(itemSource[0].ItemCode);
            dgvItemDet.Rows[CurrentIndex].Cells[iBarcode].Value = Convert.ToString(itemSource[0].Barcode);
            dgvItemDet.Rows[CurrentIndex].Cells[iName].Value = Convert.ToString(itemSource[0].SearchName);
            dgvItemDet.Rows[CurrentIndex].Cells[iQuantity].Value = 1;
            dgvItemDet.Rows[CurrentIndex].Cells[iUnit].Value = Convert.ToString(itemSource[0].UnitName);
            dgvItemDet.Rows[CurrentIndex].Cells[iUnitId].Value = Convert.ToString(itemSource[0].MainUnitId);
            dgvItemDet.Rows[CurrentIndex].Cells[iPurchaseRate].Value = Convert.ToString(itemSource[0].PurchaseRate1);
            dgvItemDet.Rows[CurrentIndex].Cells[iRate].Value = Convert.ToString(itemSource[0].PurchaseRate1);
            if (itemSource[0].LastPurchaseRate1 != null)
            {
                DialogResult result = MetroMessageBox.Show(this, "\nUpdate Last Purchase Rate is " + itemSource[0].LastPurchaseRate1 + "   ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150);
                if (result == DialogResult.Yes)
                {
                    dgvItemDet.Rows[CurrentIndex].Cells[iPurchaseRate].Value = Convert.ToString(itemSource[0].LastPurchaseRate1);
                    dgvItemDet.Rows[CurrentIndex].Cells[iRate].Value = Convert.ToString(itemSource[0].LastPurchaseRate1);
                }
            }

            dgvItemDet.Rows[CurrentIndex].Cells[iTaxType].Value = Convert.ToString(itemSource[0].TaxType);
            dgvItemDet.Rows[CurrentIndex].Cells[iMRP].Value = Convert.ToString(itemSource[0].MRP1);
            if (itemSource[0].LastMRP1 != null)
            {
                DialogResult result = MetroMessageBox.Show(this, "\nUpdate Last MRP is " + itemSource[0].LastMRP1 + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150);
                if (result == DialogResult.Yes)
                {
                    dgvItemDet.Rows[CurrentIndex].Cells[iMRP].Value = Convert.ToString(itemSource[0].LastMRP1);
                }
            }
            dgvItemDet.Rows[CurrentIndex].Cells[iSellingPer].Value = Convert.ToString(itemSource[0].SellingPer);
            if (itemSource[0].LastSellingPer != null)
            {
                DialogResult result = MetroMessageBox.Show(this, "\nLast Sales Rate is " + itemSource[0].LastSellingPer + "Update ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150);
                if (result == DialogResult.Yes)
                {
                    dgvItemDet.Rows[CurrentIndex].Cells[iSellingPer].Value = Convert.ToString(itemSource[0].LastSellingPer);
                }
            }
            dgvItemDet.Rows[CurrentIndex].Cells[iPurchaseTaxIncl].Value = Convert.ToString(itemSource[0].PurchaseTaxIncl);
            dgvItemDet.Rows[CurrentIndex].Cells[iTaxPer].Value = Convert.ToString( itemSource[0].IGSTPer);
            dgvItemDet.Rows[CurrentIndex].Cells[iTaxText].Value = Convert.ToString(itemSource[0].Caption);

            // decimal salesrate = Convert.ToDecimal((itemSource[0].PurchaseRate1) * (100 + itemSource[0].SellingPer) / 100);
            decimal salesrate = Convert.ToDecimal(itemSource[0].SaleRate) ;
            SalesRatemrp = salesrate;
            dgvItemDet.Rows[CurrentIndex].Cells[iSalesRate].Value = salesrate.ToString("#.00");
            dgvItemDet.Rows[CurrentIndex].Cells[iItemSaveAmount].Value = Convert.ToString(itemSource[0].MRP1 - (itemSource[0].PurchaseRate1) * (100 + itemSource[0].SellingPer) / 100);
            dgvItemDet.Rows[CurrentIndex].Cells[iLockSaleRate].Value = Convert.ToString(itemSource[0].LockSaleRate);
            dgvItemDet.Rows[CurrentIndex].Cells[iQohToSubtract].Value = 1;//initially main unit is loaded so default qoh to rduce i 1
            dgvItemDet.Rows[CurrentIndex].Cells[iItemQOH].Value = Convert.ToString(itemSource[0].QuantityOnHand);
            dgvItemDet.Rows[CurrentIndex].Cells[iStockItem].Value = Convert.ToString(itemSource[0].StockItem);

            dgvItemDet.Rows[CurrentIndex].Cells[iItemMrp].Value = Convert.ToString(itemSource[0].MRP1);
            dgvItemDet.Rows[CurrentIndex].Cells[iItemSelPer].Value = Convert.ToString(itemSource[0].SellingPer);

            #region unit from combobox

            FillGridUnitCombo(ItemId, CurrentIndex);
            #endregion

            //if (Convert.ToString(itemSource[0].IGSTPer) == "")
            //{
            //    dgvItemDet.Rows[CurrentIndex].Cells[iTaxPer].Value = Convert.ToString(itemSource[0].SGSTPer + itemSource[0].CGSTPer);
            //}
            //else
            //{
            //    dgvItemDet.Rows[CurrentIndex].Cells[iTaxPer].Value = Convert.ToString(itemSource[0].IGSTPer);
            //}
            dgvItemDet.Rows[CurrentIndex].Cells[iCessPer].Value = Convert.ToString(itemSource[0].CessPer);
            int currentrow = CurrentIndex;
            discper = decimal.Parse(Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iDiscPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iDiscPer].Value));
            disc = decimal.Parse(Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iDiscAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iDiscAmount].Value));
            decimal value = 0;
            int columnIndex = 0;
            ItemTotal(currentrow, value, columnIndex);
            TotalSummary();
            //DiscountPer(currentrow, discper);
            //DiscountAmount(currentrow, disc);
        }
        #endregion
        #region Save And Edit Functions
        public void DeletePurchaseDetails(long MasterId)
        {
            DmnPurchaseMaster dmnPurchaseMaster = new DmnPurchaseMaster();
            BllPurchaseDetails bllPurchaseDetails = new BllPurchaseDetails();
            dmnPurchaseMaster.PurchaseMasterId = MasterId;
            var DellstData = bllPurchaseDetails.DeleteByMasterId("prdDeletePurchaseGridDetails", dmnPurchaseMaster);
        }
        public void InsertPurchaseDet(long MasterId, decimal QuantitytoSubtract)
        {
            for (int i = 0; i < dgvItemDet.Rows.Count; i++)
            {
                string itId = Convert.ToString(dgvItemDet.Rows[i].Cells[iItemId].Value);
                if (itId != "")
                {
                    DmnPurchaseMaster dmnPurchaseMaster = new DmnPurchaseMaster();
                    BllPurchaseDetails bllPurchaseDetails = new BllPurchaseDetails();
                    dmnPurchaseMaster.PurchaseMasterId = MasterId;
                    decimal cgst = Convert.ToDecimal(dgvItemDet.Rows[i].Cells[iTaxAmount].Value) / 2;
                    dmnPurchaseMaster.ItemId = long.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iItemId].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iItemId].Value));
                    dmnPurchaseMaster.ItemCode = Convert.ToString(dgvItemDet.Rows[i].Cells[iItemCode].Value);
                    dmnPurchaseMaster.ItemBarCode = Convert.ToString(dgvItemDet.Rows[i].Cells[iBarcode].Value);
                    dmnPurchaseMaster.UnitId = long.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iUnitId].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iUnitId].Value));
                    dmnPurchaseMaster.Quantity = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value));
                    dmnPurchaseMaster.FreeQuantity = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value));
                    dmnPurchaseMaster.QuantitytoSubtract = Decimal.Parse(Convert.ToString(QuantitytoSubtract) == string.Empty ? "1" : Convert.ToString(QuantitytoSubtract));
                    dmnPurchaseMaster.PurchaseRate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iPurchaseRate].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iPurchaseRate].Value));
                    dmnPurchaseMaster.IRate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iRate].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iRate].Value));
                    dmnPurchaseMaster.Amount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iAmount].Value));
                    dmnPurchaseMaster.ItemDiscountPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscPer].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscPer].Value));
                    dmnPurchaseMaster.ItemDiscountAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscAmount].Value));
                    dmnPurchaseMaster.IGSTAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value));
                    dmnPurchaseMaster.CGSTAmount = decimal.Parse(Convert.ToString(cgst) == string.Empty ? "0" : Convert.ToString(cgst));
                    dmnPurchaseMaster.SGSTAmount = decimal.Parse(Convert.ToString(cgst) == string.Empty ? "0" : Convert.ToString(cgst));
                    dmnPurchaseMaster.GrossAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iGrossAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iGrossAmount].Value));
                    dmnPurchaseMaster.TaxPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxPer].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxPer].Value));
                    dmnPurchaseMaster.TaxAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value));
                    dmnPurchaseMaster.CessPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iCessPer].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iCessPer].Value));
                    dmnPurchaseMaster.CessAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iCessAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iCessAmount].Value));
                    dmnPurchaseMaster.ItemNetAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTotalAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTotalAmount].Value));
                    dmnPurchaseMaster.BranchId = BranchId;
                    dmnPurchaseMaster.FinancialYearId = FinancialYearId;
                    dmnPurchaseMaster.TaxType = long.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxType].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxType].Value));
                    dmnPurchaseMaster.Cost = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iCost].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iCost].Value)); ;
                    dmnPurchaseMaster.Margin = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iMargin].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iMargin].Value));
                    dmnPurchaseMaster.MRP = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iMRP].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iMRP].Value));
                    dmnPurchaseMaster.TotalSaveAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iItemSaveAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iItemSaveAmount].Value));
                    dmnPurchaseMaster.StockItem = int.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iStockItem].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iStockItem].Value));
                    dmnPurchaseMaster.Status = 0;
                    dmnPurchaseMaster.CreatedBy = UserId;
                    // dmnPurchaseMaster.ExpDate= DateTime.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iExpdate].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iExpdate].Value));
                    if (dgvItemDet.Rows[i].Cells[iExpdate].Value != null)
                    {
                        dmnPurchaseMaster.ExpDate = Convert.ToDateTime(dgvItemDet.Rows[i].Cells[iExpdate].Value);
                    }
                    else
                    {
                        dmnPurchaseMaster.ExpDate=Convert.ToDateTime("01/01/2010");
                    }
                        #region to find quantity to reduce/add

                    BllSearch <DmnGridFields> bllQunatitytoReduceSearch = new BllSearch<DmnGridFields>();

                    DmnSearch objQtySearch = new DmnSearch();
                    objQtySearch = new DmnSearch();
                    objQtySearch.Select = " ItemId as Id,case MainUnitId when '" + dmnPurchaseMaster.UnitId + "' then 1 " +
                        "Else PacketQty end as Field1,StockItem as Field2";
                    objQtySearch.Table = " tblItem  ";
                    objQtySearch.WhereNdGroupBy = "where ItemId=" + dmnPurchaseMaster.ItemId;
                    var qtySource = bllQunatitytoReduceSearch.GetSearchData("prdSearch", objQtySearch);
                    dmnPurchaseMaster.QuantitytoSubtract = Convert.ToDecimal(qtySource[0].Field1);


                    #endregion

                    var DetlstData = bllPurchaseDetails.AddData("prdInsertPurchaseDetails", dmnPurchaseMaster);
                }
            }
        }
        public void DeleteMiscCharges(long MasterId)
        {
            DmnPurchaseMaster dmnPurchaseMaster = new DmnPurchaseMaster();
            BllPurchaseDetails bllPurchaseDetails = new BllPurchaseDetails();
            dmnPurchaseMaster.PurchaseMasterId = MasterId;
            var DellstData = bllPurchaseDetails.DeleteMiscCharges("prdDeletePurchaseMiscCharges", dmnPurchaseMaster);
        }
        public void InsertMiscCharges(long MasterId)
        {
            foreach (ListViewItem l in mlvMisc.Items)
            {
                DmnPurchaseMaster dmnPurchaseMaster = new DmnPurchaseMaster();
                BllPurchaseDetails bllPurchaseDetails = new BllPurchaseDetails();
                dmnPurchaseMaster.PurchaseMasterId = MasterId;
                dmnPurchaseMaster.GeneralId = long.Parse(Convert.ToString(l.SubItems[3].Text) == string.Empty ? "0" : Convert.ToString(l.SubItems[3].Text));
                dmnPurchaseMaster.MiscAmount = decimal.Parse(Convert.ToString(l.SubItems[2].Text) == string.Empty ? "0.00" : Convert.ToString(l.SubItems[2].Text));
                dmnPurchaseMaster.CreatedBy = UserId;
                dmnPurchaseMaster.Status = 0;
                var MislstData = bllPurchaseDetails.AddPurchaseMiscCharges("prdInsertPurchaseMiscCharges", dmnPurchaseMaster);
            }
        }
        public void UpdateBranchPurchaseSrlNo(long MasterId)
        {
            DmnPurchaseMaster dmnPurchaseMaster = new DmnPurchaseMaster();
            BllPurchaseDetails bllPurchaseDetails = new BllPurchaseDetails();
            dmnPurchaseMaster.BranchId = BranchId;
           // var UpDatelstData = bllPurchaseDetails.UpdateBranchPurchaseSrlNo("prdUpdateBranchPurchaseSrlNo", dmnPurchaseMaster);
        }
        public int SelNegativeAllowed()
        {
            BllSearch<DmnGeneralOptions> bllSearch = new BllSearch<DmnGeneralOptions>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "G.GTypeSubValue1";
            objSearch.Table = " tblGeneralOptions G  ";
            objSearch.WhereNdGroupBy = "Where G.Caption = 'QOH VALIDATION'";
            objSearch.OrderBy = " ";
            var det = bllSearch.GetSearchData("prdSearch", objSearch);
            return Convert.ToInt32(det[0].GTypeSubValue1);
        }
        #endregion
        public void TotalSummary()
        {
            decimal GrandTot = 0;
            decimal DiscAmount = 0;
            decimal MiscCharges = 0;
            decimal Adjust = 0;
            decimal NetAmount = 0;
            for (int i = 0; i < dgvItemDet.Rows.Count; ++i)
            {
                GrandTot += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTotalAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTotalAmount].Value));
            }
            mtxtGrandTot.Text = Convert.ToString(GrandTot);
            DiscAmount = decimal.Parse(Convert.ToString(mtxtTotDiscAmnt.Text) == string.Empty ? "0.00" : Convert.ToString(mtxtTotDiscAmnt.Text));
            MiscCharges = decimal.Parse(Convert.ToString(mtxtMiscCharges.Text) == string.Empty ? "0.00" : Convert.ToString(mtxtMiscCharges.Text));
            Adjust = decimal.Parse(Convert.ToString(mtxtAdjust.Text) == string.Empty ? "0.00" : Convert.ToString(mtxtAdjust.Text));
            NetAmount = (GrandTot) + (MiscCharges) + (Adjust) - (DiscAmount);
            mtxtNetAmount.Text = Convert.ToString(NetAmount);
        }
        public void LedgerPopup()
        {
            mtxtAddress.Focus();
            string returnValue = string.Empty;
            DataTable dtColumnAlign = new DataTable();
            dtColumnAlign.Columns.Add();
            dtColumnAlign.Columns.Add();
            dtColumnAlign.Rows.Add("6", "R");
            dtColumnAlign.Rows.Add("7", "R");
            dtColumnAlign.Rows.Add("8", "R");
            dtColumnAlign.Rows.Add("9", "R");
            dtColumnAlign.Rows.Add("10", "R");
            DmnSearch _objSearch = new DmnSearch();
            _objSearch.ColumnHeaders = "Id,Name,Code,Account Group,Telephone,Mobile,Ledger Address";
            _objSearch.Select = "L.LedgerId as Id,L.LedgerName as Field1,L.LedgerCode as Field2,Ac.AccountGroupName as Field3,L.Telephone as Field4,L.Mobile as Field5, L.LedgerAddress as Field6";
            _objSearch.Table = " tblAccountLedger L LEFT JOIN tblAccountGroup Ac  ON L.AccountGroupId = Ac.AccountGroupId";
            _objSearch.WhereNdGroupBy = " WHERE L.Status = 0 And L.BranchId = " + BranchId;
            _objSearch.WhereNdGroupBy = " WHERE (Ac.AccountGroupName = 'Sundry Creditors') AND L.Status = 0 And L.BranchId = " + BranchId;
            _objSearch.OrderBy = " ORDER BY L.LedgerName ASC";
            using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 90, (this.Height - 120), this.Location.Y + 50, this.Location.X + 100, dtColumnAlign))
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
                BllSearch<DmnAccountLedger> bllSearch = new BllSearch<DmnAccountLedger>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = " tblAccountLedger ";
                objSearch.WhereNdGroupBy = "Where LedgerId = " + LedgerId;
                objSearch.OrderBy = "Order By LedgerName ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                //mtxtLedgerId.Text = Convert.ToString(LedgerId);
                //LedgerId = Convert.ToInt64(gridSource[0].LedgerId);
                mcmbLedger.SelectedValue = gridSource[0].LedgerId;

                mtxtLedger.Text = Convert.ToString(gridSource[0].LedgerName);
                mtxtAddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                mtxtMobile.Text = Convert.ToString(gridSource[0].Mobile);
                mtxtCreditDays.Text = Convert.ToString(gridSource[0].CreditDays);
                mdtpDueDate.Value = Convert.ToDateTime(DateTime.Now.AddDays(gridSource[0].CreditDays));
                existingAddress = Convert.ToString(gridSource[0].LedgerAddress);
                existingMobile = Convert.ToString(gridSource[0].Mobile);
                LedgerCode = gridSource[0].LedgerCode;
                FillTransNoComboBox(gridSource[0].LedgerCode);
            }
        }

        public void FillGridUnitCombo(long itemId, int CurrentIndex)
        {
            DmnItem dmnItem = new DmnItem();
            dmnItem.ItemId = itemId;

            BllSalesDetails objBll = new BllSalesDetails();
            var unitList = objBll.GetItemUnits(dmnItem);
            DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)dgvItemDet.Rows[CurrentIndex].Cells[iUnit];
            var unitSource = unitList.Select(x => new { x.UnitId, x.UnitName });
            if (unitSource != null)
            {
                var mainUnit = unitList.ElementAt(0).UnitName;
                cbCell.DataSource = unitSource.ToList();
                cbCell.DisplayMember = "UnitName";
                cbCell.ValueMember = "UnitId";
                // dgvItemDet.CurrentRow.Cells[iUnit].Value = mainUnit;
            }
        }
        #endregion

        #region Events
        private void FrmPurchase_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshForm();
                mlvMisc.Columns.Add("Sl No", 100, HorizontalAlignment.Left);
                mlvMisc.Columns.Add("Particulars", 350, HorizontalAlignment.Left);
                mlvMisc.Columns.Add("Price", 200, HorizontalAlignment.Right);
                mlvMisc.Columns.Add("", 0);
                // this.mlvMisc. = HorizontalAlignment.Center;
                mlvMisc.View = View.Details;
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
                    if (dgvItemDet.Rows.Count > 1)
                    {
                        decimal net = decimal.Parse(mtxtNetAmount.Text == string.Empty ? "0.00" : mtxtNetAmount.Text);
                        if (net > 0)
                        {
                            DialogResult results = MetroMessageBox.Show(this, "\nsave Entry No.: " + mtxtInvNo.Text.ToString() + " ? ", "Save confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (results == DialogResult.Yes)
                            {
                                long savestatus;
                                DmnPurchaseMaster dmnPurchaseMaster = new DmnPurchaseMaster();
                                BllPurchaseMaster bllPurchaseMaster = new BllPurchaseMaster();
                                string cgst = Convert.ToString(decimal.Parse(mtxtTotTax.Text) / 2);
                                dmnPurchaseMaster.EType = Convert.ToInt32(mcmbTax.SelectedValue);
                                dmnPurchaseMaster.EntryNo = Convert.ToString(mtxtInvNo.Text);
                                dmnPurchaseMaster.EntryDate = Convert.ToDateTime(mdtpInvDate.Text);
                                dmnPurchaseMaster.DocumentNo = Convert.ToString(mtxtDocNo.Text);
                                dmnPurchaseMaster.LedgerId = LedgerId;
                                dmnPurchaseMaster.PurchaseAddress = Convert.ToString(mtxtAddress.Text);
                                dmnPurchaseMaster.PaymentMode = Convert.ToInt32(mcmbMode.SelectedValue);
                                dmnPurchaseMaster.Telephone = Convert.ToString(mtxtMobile.Text);
                                dmnPurchaseMaster.Remarks = Convert.ToString(mtxtRemarks.Text);
                                dmnPurchaseMaster.TotalGross = decimal.Parse(mtxtTotGrss.Text == string.Empty ? "0.00" : mtxtTotGrss.Text);
                                dmnPurchaseMaster.TotalAmount = decimal.Parse(mtxtTotAmount.Text == string.Empty ? "0.00" : mtxtTotAmount.Text);
                                dmnPurchaseMaster.TotalDiscount = decimal.Parse(mtxtTotDiscAmnt.Text == string.Empty ? "0.00" : mtxtTotDiscAmnt.Text);
                                dmnPurchaseMaster.TotalIGST = decimal.Parse(mtxtTotTax.Text == string.Empty ? "0.00" : mtxtTotTax.Text);
                                dmnPurchaseMaster.TotalCGST = decimal.Parse(cgst == string.Empty ? "0.00" : cgst);
                                dmnPurchaseMaster.TotalSGST = decimal.Parse(cgst == string.Empty ? "0.00" : cgst);
                                dmnPurchaseMaster.TotalTax = decimal.Parse(mtxtTotTax.Text == string.Empty ? "0.00" : mtxtTotTax.Text);
                                dmnPurchaseMaster.TotalMiscCharges = decimal.Parse(mtxtMiscCharges.Text == string.Empty ? "0.00" : mtxtMiscCharges.Text);
                                dmnPurchaseMaster.TotalCess = decimal.Parse(mtxtTotCess.Text == string.Empty ? "0.00" : mtxtTotCess.Text);
                                dmnPurchaseMaster.DiscountPer = decimal.Parse(mtxtTotDiscPer.Text == string.Empty ? "0.00" : mtxtTotDiscPer.Text);
                                dmnPurchaseMaster.DiscountAmount = decimal.Parse(mtxtTotDiscAmnt.Text == string.Empty ? "0.00" : mtxtTotDiscAmnt.Text);
                                dmnPurchaseMaster.OtherAdditions = decimal.Parse(mtxtAdjust.Text == string.Empty ? "0.00" : mtxtAdjust.Text);
                                dmnPurchaseMaster.NetAmount = decimal.Parse(mtxtTot.Text == string.Empty ? "0.00" : mtxtNetAmount.Text);
                                dmnPurchaseMaster.PaidAmount = decimal.Parse(mtxtNetAmount.Text == string.Empty ? "0.00" : mtxtNetAmount.Text);
                                dmnPurchaseMaster.BalanceAmount = decimal.Parse(mtxtNetAmount.Text == string.Empty ? "0.00" : mtxtNetAmount.Text);
                                dmnPurchaseMaster.AmountAdjust = decimal.Parse(mtxtAdjust.Text == string.Empty ? "0.00" : mtxtAdjust.Text);
                                dmnPurchaseMaster.TransportNo = int.Parse(Convert.ToString(mcmbTransNo.SelectedValue) == string.Empty ? "0" : Convert.ToString(mcmbTransNo.SelectedValue));
                                dmnPurchaseMaster.CreditDays = int.Parse(mtxtCreditDays.Text == string.Empty ? "0" : mtxtCreditDays.Text);
                                dmnPurchaseMaster.DueDate = Convert.ToDateTime(mdtpDueDate.Text);
                                dmnPurchaseMaster.DocumentType = "";
                                dmnPurchaseMaster.BranchId = BranchId;
                                dmnPurchaseMaster.FinancialYearId = FinancialYearId;
                                dmnPurchaseMaster.TotalQuantity = Decimal.ToInt32(Convert.ToDecimal(mtxtNoOfItems.Text));
                                dmnPurchaseMaster.DocumentDate = Convert.ToDateTime(mdtpDocDate.Text);
                                dmnPurchaseMaster.TotalNetAmount = decimal.Parse(mtxtNetAmount.Text == string.Empty ? "0.00" : mtxtNetAmount.Text);
                                dmnPurchaseMaster.Status = 0;
                                long MasterId;
                                bool changed = false;
                                if (string.Compare(mtxtAddress.Text.Trim(), existingAddress) != 0)
                                {
                                    changed = true;
                                }
                                if (string.Compare(mtxtMobile.Text.Trim(), existingMobile) != 0)
                                {
                                    changed = true;
                                }
                                if (changed)
                                {
                                    DialogResult result = MetroMessageBox.Show(this, "\n Update the Party details ?", "Update ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, 150);
                                    if (result == DialogResult.OK)
                                    {
                                        DmnAccountLedger dmnLegder = new DmnAccountLedger();
                                        BllAccountLedger bllLedger = new BllAccountLedger();
                                        dmnLegder.LedgerId = LedgerId;
                                        dmnLegder.LedgerAddress = mtxtAddress.Text.Trim();
                                        dmnLegder.Mobile = mtxtMobile.Text.Trim();
                                        dmnLegder.ModifiedBy = UserId;
                                        var status = bllLedger.UpdateLedgerAddress_Mobile("prdUpdateLedgerAddress_Mobile", dmnLegder);
                                    }
                                }
                                if (PurchaseMasterId == 0)
                                {
                                    dmnPurchaseMaster.CreatedBy = UserId;
                                    var lstData = bllPurchaseMaster.AddData("prdInsertPurchaseMaster", dmnPurchaseMaster);
                                    savestatus = lstData.PurchaseMasterId;
                                    if (lstData.PurchaseMasterId != 0)
                                    {
                                        MasterId = lstData.PurchaseMasterId;

                                        InsertPurchaseDet(MasterId, 1);
                                        InsertMiscCharges(MasterId);
                                        //UpdateBranchPurchaseSrlNo(MasterId);
                                    }
                                    if (savestatus != 0)
                                    {
                                        MetroMessageBox.Show(this, "Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                                        RefreshForm();
                                        ActiveControl = dgvItemDet;

                                        dgvItemDet.CurrentCell = dgvItemDet.Rows[0].Cells[3];
                                        dgvItemDet.CurrentCell.Selected = true;
                                        dgvItemDet.BeginEdit(true);
                                    }
                                    else
                                        MetroMessageBox.Show(this, "Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                                }
                                else
                                {
                                    bool status;
                                    MasterId = PurchaseMasterId;
                                    dmnPurchaseMaster.ModifiedBy = UserId;
                                    dmnPurchaseMaster.ModifiedDateTime = DateTime.Now;
                                    dmnPurchaseMaster.PurchaseMasterId = PurchaseMasterId;
                                    var lstData = bllPurchaseMaster.UpdateData("prdUpdatePurchaseMaster", dmnPurchaseMaster);

                                    BllSearch<DmnPurchaseMaster> bllSearch = new BllSearch<DmnPurchaseMaster>();
                                    DmnSearch objSearch = new DmnSearch();
                                    objSearch.Select = "P.ItemId,P.PurchaseDetailId,P.UnitId ";
                                    objSearch.Table = " tblPurchaseDetails P  ";
                                    objSearch.WhereNdGroupBy = "Where P.PurchaseMasterId = '" + PurchaseMasterId + "'";
                                    objSearch.OrderBy = "Order By P.PurchaseDetailId ";
                                    var det = bllSearch.GetSearchData("prdSearch", objSearch);
                                    for (int i = 0; i < det.Count; i++)
                                    {
                                        var lst = SelNegativeAllowed();
                                        if (lst == 1)
                                        {
                                            //    objSearch = new DmnSearch();
                                            //    objSearch.Select = "P.ItemId,P.PurchaseDetailId";
                                            //    objSearch.Table = " tblPurchaseDetails P LEFT JOIN tblItem I ON  P.ItemId=I.ItemId ";
                                            //    objSearch.WhereNdGroupBy = "Where P.PurchaseDetailId = '" + det[0].PurchaseDetailId + "'";
                                            //    objSearch.OrderBy = "Order By P.PurchaseDetailId ";
                                            //    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                                            dmnPurchaseMaster.PurchaseDetailId = det[0].PurchaseDetailId;
                                            dmnPurchaseMaster.ItemId = det[0].ItemId;

                                            #region to find quantity to reduce/add


                                            BllSearch<DmnGridFields> bllQunatitytoReduceSearch = new BllSearch<DmnGridFields>();
                                            DmnSearch objQtySearch = new DmnSearch();
                                            objQtySearch = new DmnSearch();
                                            objQtySearch.Select = " ItemId as Id,case MainUnitId when '" + det[i].UnitId + "' then 1 " +
                                                "Else PacketQty end as Field1,StockItem as Field2";
                                            objQtySearch.Table = " tblItem  ";
                                            objQtySearch.WhereNdGroupBy = "where ItemId=" + det[i].ItemId;
                                            var qtySource = bllQunatitytoReduceSearch.GetSearchData("prdSearch", objQtySearch);
                                            dmnPurchaseMaster.QuantitytoSubtract = Convert.ToDecimal(qtySource[0].Field1);


                                            #endregion


                                            if (Convert.ToInt32(qtySource[0].Field2) == 1)//update qoh only if it is a stock item
                                            {
                                                var UpdateQohLst = bllPurchaseMaster.UpdateQoh("prdUpdateQoh", dmnPurchaseMaster);
                                            }


                                        }
                                    }
                                    DeletePurchaseDetails(MasterId);
                                    InsertPurchaseDet(MasterId, dmnPurchaseMaster.QuantitytoSubtract);
                                    DeleteMiscCharges(MasterId);
                                    InsertMiscCharges(MasterId);
                                    dmnPurchaseMaster.ModifiedBy = UserId;
                                    dmnPurchaseMaster.ModifiedDateTime = DateTime.Now;
                                    dmnPurchaseMaster.PurchaseMasterId = PurchaseMasterId;
                                    var lstDataUpdate = bllPurchaseMaster.UpdateData("prdUpdatePurchaseMaster", dmnPurchaseMaster); status = lstData;
                                    status = lstDataUpdate;
                                    if (status)
                                    {
                                        MetroMessageBox.Show(this, "Updated successfully ", "Updated  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                        RefreshForm();
                                        ActiveControl = dgvItemDet;

                                        dgvItemDet.CurrentCell = dgvItemDet.Rows[0].Cells[3];
                                        dgvItemDet.CurrentCell.Selected = true;
                                        dgvItemDet.BeginEdit(true);
                                    }
                                    else
                                        MetroMessageBox.Show(this, "Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                                    hlblHeader.Text = "Purchase Invoice";
                                }
                            }
                            else
                            {
                                MetroMessageBox.Show(this, "Invalid entry!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                            }
                        }
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "Invalid entry!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    }

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtCreditDays_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (mtxtCreditDays.Text != "")
                {
                    int cdays = Convert.ToInt32(mtxtCreditDays.Text);
                    mdtpDueDate.Value = Convert.ToDateTime(DateTime.Now.AddDays(cdays));
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void DgvItemDet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                #region Decimal TextBox
                if (e.Control is TextBox)
                {
                    TextBox curtxtbox = e.Control as TextBox;
                    curtxtbox.KeyPress += new KeyPressEventHandler(objGeneral.DecimalGridviewTextBox);


                    DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
                    tb.KeyUp -= new KeyEventHandler(DgvItemDet_KeyEventHandler);
                    tb.KeyUp += new KeyEventHandler(DgvItemDet_KeyEventHandler);
                    tb.PreviewKeyDown -= new PreviewKeyDownEventHandler(DgvItemDet_PreviewKeyDown);
                    tb.PreviewKeyDown += new PreviewKeyDownEventHandler(DgvItemDet_PreviewKeyDown);
                }
                #endregion
                else if (e.Control is ComboBox)//this region is for unit editing
                {
                    int colIndex = dgvItemDet.CurrentCell.ColumnIndex;
                    //ComboBox cmb = e.Control as ComboBox;
                    //if (cmb != null)
                    //{
                    //    cmb.SelectedIndex = 0;
                    //}

                    if (iUnit == colIndex || iTaxText == colIndex)
                    {

                        #region method 1
                        ComboBox comboBox = e.Control as ComboBox;
                        //comboBox.SelectedIndexChanged -= DgvItemDet_ComboBoxEventHandler;
                        //comboBox.SelectedIndexChanged += DgvItemDet_ComboBoxEventHandler;

                        comboBox.SelectedIndexChanged -= LastColumnComboSelectionChanged;
                        comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
                        #endregion                      
                    }
                }

                e.Control.PreviewKeyDown -= new PreviewKeyDownEventHandler(DgvItemDet_PreviewKeyDown);
                e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(DgvItemDet_PreviewKeyDown);
                e.Control.KeyUp -= new KeyEventHandler(DgvItemDet_KeyEventHandler);
                e.Control.KeyUp += new KeyEventHandler(DgvItemDet_KeyEventHandler);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void DgvItemDet_KeyEventHandler(object sender, KeyEventArgs e)
        {
            try
            {
                if (sender is TextBox)
                {
                    decimal discountPer;
                    int currentrow = dgvItemDet.CurrentRow.Index;
                    TextBox curTxt = (TextBox)sender;
                    string value = Convert.ToString(curTxt.Text);

                    int colIndex = dgvItemDet.CurrentCell.ColumnIndex;
                    if (string.IsNullOrWhiteSpace(Convert.ToString(value)))
                    {
                        value = "0";
                    }
                    if (value == ".")
                    {
                        value = "0.";
                        dgvItemDet.Rows[currentrow].Cells[colIndex].Value = "0.";
                    }
                    string columnName = this.dgvItemDet.Columns[dgvItemDet.CurrentCell.ColumnIndex].Name;
                    if ((iDiscPer == colIndex) && Convert.ToString(value) != "0.00" && !string.IsNullOrWhiteSpace(Convert.ToString(value)))
                    {
                        discper = decimal.Parse(Convert.ToString(curTxt.Text) == string.Empty ? "0.00" : Convert.ToString(curTxt.Text));//dot
                        dgvItemDet.Rows[currentrow].Cells[iDiscPer].Value = Convert.ToString(discper.ToString("0.00"));
                        DiscountPerToAmt(currentrow, discper);
                        ItemTotal(currentrow, disc, colIndex);
                        TotalSummary();
                    }
                    if ((iDiscAmount == colIndex) && Convert.ToString(value) != "0.00" && !string.IsNullOrWhiteSpace(Convert.ToString(value)))
                    {
                        disc = decimal.Parse(Convert.ToString(value) == string.Empty ? "0.00" : Convert.ToString(value));
                        dgvItemDet.Rows[currentrow].Cells[iDiscAmount].Value = Convert.ToString(disc.ToString("0.00"));
                        DiscountAmtToPer(currentrow, disc);
                        ItemTotal(currentrow, disc, colIndex);
                        TotalSummary();
                    }
                    if ((iQuantity == colIndex) && Convert.ToString(value) != "0.00" && !string.IsNullOrWhiteSpace(Convert.ToString(value)))
                    {
                        colIndex = iQuantity;
                        decimal qty = Convert.ToDecimal(value);
                        dgvItemDet.Rows[currentrow].Cells[iQuantity].Value = Convert.ToString(qty.ToString("0.00"));
                        //#region display error if items more than qoh tried to buy

                        //decimal PresentQoH = Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iItemQOH].Value);

                        //decimal multiplier = Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iQohToSubtract].Value);
                        //if (PresentQoH < qty * multiplier)
                        //{
                        //    MetroMessageBox.Show(this, "\nQuantity  is above QOH.\nPlease update item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150);
                        //}
                        //dgvItemDet.Rows[currentrow].Cells[iQuantity].Selected = true;//to set the focis after error message
                        //#endregion
                        ItemTotal(currentrow, qty, colIndex);
                        discountPer = Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iDiscPer].Value);
                        DiscountPerToAmt(currentrow, discountPer);
                        TotalSummary();
                    }
                    if ((iPurchaseRate == colIndex) && Convert.ToString(value) != "0.00" && !string.IsNullOrWhiteSpace(Convert.ToString(value)))
                    {
                        colIndex = iPurchaseRate;
                        decimal rate = decimal.Parse(Convert.ToString(value) == string.Empty ? "0.00" : Convert.ToString(value));
                        if (rate == decimal.Parse(Convert.ToString(dgvItemDet.Rows[currentrow].Cells[iMRP].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currentrow].Cells[iMRP].Value)))
                        {
                            dgvItemDet.CurrentRow.DefaultCellStyle.BackColor = Color.LightSalmon;
                        }

                        else
                        {
                            dgvItemDet.CurrentRow.DefaultCellStyle.BackColor = Color.White;
                        }
                        if (rate > decimal.Parse(Convert.ToString(dgvItemDet.Rows[currentrow].Cells[iMRP].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currentrow].Cells[iMRP].Value)))

                        {
                            MetroMessageBox.Show(this, "Rate cannot be greater than MRP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                            rate = SalesRatemrp;
                        }
                        dgvItemDet.Rows[currentrow].Cells[iPurchaseRate].Value = Convert.ToString(rate.ToString("0.00"));
                        ItemTotal(currentrow, rate, colIndex);
                        discountPer = Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iDiscPer].Value);
                        DiscountPerToAmt(currentrow, discountPer);
                        TotalSummary();
                    }
                    //if((iBarcode == colIndex) && (Convert.ToString(value) == vBarcode) && !string.IsNullOrWhiteSpace(Convert.ToString(value)))
                    if (iBarcode == colIndex)
                    {
                        int CurrentIndex = dgvItemDet.CurrentRow.Index;
                        dgvItemDet.Rows[CurrentIndex].Cells[iBarcode].Value = Convert.ToString(value);
                        BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                        DmnSearch objSearch = new DmnSearch();
                        objSearch = new DmnSearch();
                        objSearch.Select = "I.ItemId,I.Barcode";
                        objSearch.Table = " tblItem I ";
                        objSearch.WhereNdGroupBy = "Where I.Barcode = '" + value + "'";
                        objSearch.OrderBy = "Order By I.ItemName ";
                        var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                        if (itemSource.Count > 0)
                        {
                            if (itemSource.Count == 1)
                            {
                                ItemId = Convert.ToInt64(itemSource[0].ItemId);

                                //int RepeatRowIndex = -1;
                                //for (int i = 0; i < dgvItemDet.Rows.Count; ++i)
                                //{
                                //    if (ItemId == Convert.ToInt64(dgvItemDet.Rows[i].Cells[iItemId].Value))
                                //    {
                                //        RepeatRowIndex = i;
                                //    }
                                //}
                                //if (RepeatRowIndex > -1)
                                //{
                                //    dgvItemDet.CurrentCell = dgvItemDet.Rows[RepeatRowIndex].Cells[iQuantity];
                                //    MetroMessageBox.Show(this, "Item already exist!");
                                //}
                                //if (RepeatRowIndex <= -1)
                                //{
                                Itemdetails(CurrentIndex, ItemId);
                                discountPer = Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iDiscPer].Value);
                                DiscountPerToAmt(currentrow, discountPer);
                                TotalSummary();

                                //}
                            }
                            else
                            {
                                #region make a pop up to choose items

                                DataTable dtColumnAlign = new DataTable();
                                dtColumnAlign.Columns.Add();
                                dtColumnAlign.Columns.Add();
                                dtColumnAlign.Rows.Add("6", "R");
                                dtColumnAlign.Rows.Add("7", "R");
                                dtColumnAlign.Rows.Add("8", "R");
                                dtColumnAlign.Rows.Add("9", "R");
                                dtColumnAlign.Rows.Add("10", "R");
                                string returnValue = string.Empty;
                                DmnSearch _objSearch = new DmnSearch();
                                _objSearch.ColumnHeaders = "Id,Code,Barcode,Name,Category,Unit,PurchaseRate,SalesRate,MRP,Tax(%),QOH";
                                _objSearch.Select = "I.ItemId as Id,I.ItemCode as Field1,I.Barcode as Field2,I.SearchName as Field3,C.CategoryName as Field4,U.UnitName as Field5, I.PurchaseRate1 as Field6," +
                                    " (I.PurchaseRate1*(100+I.SellingPer)/100) as Field7, I.MRP1 as Field8, I.IGSTPer as Field9, I.QuantityOnHand as Field10";
                                _objSearch.Table = " tblItem I LEFT JOIN tblCategory C  ON I.CategoryId = C.CategoryId LEFT JOIN tblUnit U ON I.MainUnitId=U.UnitId";
                                _objSearch.WhereNdGroupBy = " WHERE I.Barcode='" + value + "'";
                                _objSearch.OrderBy = "order by I.SearchName ASC ";
                                //   using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 1200, 400, 100, 100))
                                //  using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 90, (this.Height - 120), this.Location.Y + 50, this.Location.X + 100, dtColumnAlign))
                                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 90, (this.Height / 2), this.Location.Y + 50, this.Location.X + 100, dtColumnAlign))

                                {
                                    var dialogResult = search.ShowDialog();
                                    if (dialogResult == DialogResult.OK)
                                    {
                                        returnValue = Convert.ToString(search._retValue);
                                    }
                                    else

                                    {
                                        // MessageBox.Show("Search cancelled");
                                        //if (dgvItemDet.Rows.Count > 1)
                                        //{
                                        ItemId = 0;
                                        // dgvItemDet.Rows.RemoveAt(dgvItemDet.CurrentRow.Index + 1);
                                        // }
                                    }
                                }
                                if (returnValue != string.Empty)
                                {
                                    ItemId = Convert.ToInt64(returnValue);
                                    Itemdetails(CurrentIndex, ItemId);
                                    dgvItemDet.CurrentCell = dgvItemDet.Rows[currentrow].Cells[iQuantity];//to move focus to quantity else multille pop nup appears
                                    discountPer = Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iDiscPer].Value);
                                    DiscountPerToAmt(currentrow, discountPer);
                                    TotalSummary();
                                }

                                #endregion
                            }
                        }

                        if (e.KeyCode == Keys.Delete)
                        {
                            if ((dgvItemDet.RowCount > 0) && (currentrow != (dgvItemDet.Rows.Count - 1)))
                            {
                                dgvItemDet.Rows.RemoveAt(currentrow);
                                for (int i = 0; i < dgvItemDet.Rows.Count; ++i)
                                {
                                    dgvItemDet.Rows[i].Cells[iSlNo].Value = i + 1;
                                }
                                GridSummary();
                                TotalSummary();

                            }
                        }
                    }
                    int colndex = dgvItemDet.CurrentCell.ColumnIndex;
                    int currow = dgvItemDet.CurrentCell.RowIndex;
                    if (colndex == iPurchaseRate)
                    {
                        decimal SellingPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iSellingPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iSellingPer].Value));

                        decimal PurchaseRate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iPurchaseRate].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iPurchaseRate].Value));

                        decimal salsrate = (PurchaseRate * (100 + SellingPer)) / 100;
                        decimal mrp = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iMRP].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iMRP].Value));
                        if (mrp < salsrate)
                        {
                            dgvItemDet.Rows[currow].Cells[iSellingPer].Value = dgvItemDet.Rows[currow].Cells[iItemSelPer].Value;
                            SellingPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iSellingPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iSellingPer].Value));

                            decimal salesrate = (PurchaseRate * (100 + SellingPer)) / 100;
                            dgvItemDet.Rows[currow].Cells[iSalesRate].Value = salesrate.ToString("0.00");
                            MetroMessageBox.Show(this, "Sales Rate must be less than or equal to MRP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                        else
                        {
                            dgvItemDet.Rows[currow].Cells[iSalesRate].Value = salsrate.ToString("0.00");
                        }
                    }
                    if (colndex == iSellingPer)
                    {
                        decimal SellingPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iSellingPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iSellingPer].Value));

                        decimal PurchaseRate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iPurchaseRate].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iPurchaseRate].Value));

                        decimal salsrate = (PurchaseRate * (100 + SellingPer)) / 100;
                        decimal mrp = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iMRP].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iMRP].Value));
                        if (mrp < salsrate)
                        {
                            dgvItemDet.Rows[currow].Cells[iSellingPer].Value = dgvItemDet.Rows[currow].Cells[iItemSelPer].Value;
                            SellingPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iSellingPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iSellingPer].Value));

                            decimal salesrate = (PurchaseRate * (100 + SellingPer)) / 100;
                            dgvItemDet.Rows[currow].Cells[iSalesRate].Value = salesrate.ToString("0.00");
                            MetroMessageBox.Show(this, "Sales Rate must be less than or equal to MRP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                        else
                        {
                            dgvItemDet.Rows[currow].Cells[iSalesRate].Value = salsrate.ToString("0.00");
                        }
                    }
                    if (colndex == iSalesRate)
                    {
                        decimal SalesRate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iSalesRate].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iSalesRate].Value));
                        decimal PurchaseRate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iPurchaseRate].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iPurchaseRate].Value));
                        decimal SellingPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iItemSelPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iItemSelPer].Value));
                        decimal mrp = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iMRP].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iMRP].Value));
                        if (mrp < SalesRate)
                        {
                            decimal salsrate = (PurchaseRate * (100 + SellingPer)) / 100;
                            dgvItemDet.Rows[currow].Cells[iSellingPer].Value = dgvItemDet.Rows[currow].Cells[iItemSelPer].Value;
                            dgvItemDet.Rows[currow].Cells[iSalesRate].Value = Convert.ToDecimal(salsrate.ToString("0.00"));
                            MetroMessageBox.Show(this, "Sales Rate must be less than or equal to MRP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                        }
                        else
                        {
                            SellingPer = ((100 * SalesRate) / PurchaseRate) - 100;
                            dgvItemDet.Rows[currow].Cells[iSellingPer].Value = SellingPer.ToString("0.00");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void DgvItemDet_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //objGeneral.DecimalTextBoxSignNormalTexbox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void DgvItemDet_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    Itempopup();
                }
                if (dgvItemDet.CurrentCell == null)
                {
                    MetroMessageBox.Show(this, "Select Row!");
                }
                else
                {
                    // int colIndex = dgvItemDet.CurrentCell.ColumnIndex;

                    #region move controlout of grid in last entry

                    int rowindex = dgvItemDet.Rows.Count - 1;

                    if (dgvItemDet.CurrentCell.RowIndex == rowindex && (Convert.ToString(dgvItemDet.CurrentRow.Cells[iBarcode].Value) == ""))
                    {


                        if (e.KeyCode == Keys.Enter)
                        {
                            mtxtTotDiscAmnt.Focus();
                            ActiveControl = mtxtTotDiscAmnt;
                        }
                    }
                    #endregion
                }
                //else
                //{
                //    int colIndex = dgvItemDet.CurrentCell.ColumnIndex;
                //    if (e.KeyCode == Keys.Enter)
                //    {
                //        dgvItemDet.Focus();
                //    }
                //}

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        //1
        private void DgvItemDet_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode == Keys.Enter)
                //{

                //    int colIndex = dgvItemDet.CurrentCell.ColumnIndex+1;
                //    int currentrow = dgvItemDet.CurrentCell.RowIndex - 1;
                //    if (currentrow < 0)// added to avoid error if barcode is empty and enter pressed
                //    {
                //        currentrow = 0;
                //    }

                //    if (colIndex == iBarcode)
                //    {
                //        if (currentrow < 0)// added to avoid error if barcode is empty and enter pressed
                //        {
                //            currentrow = 0;
                //        }
                //        dgvItemDet.CurrentCell = dgvItemDet.Rows[currentrow].Cells[iQuantity]; //dgvItemDet.currentrow.Cells[iQuantity];
                //        dgvItemDet.CurrentCell.Selected = true;
                //        dgvItemDet.BeginEdit(true);
                //    }
                                      
                //     else if (colIndex == iUnit)//when enter is pressed from unit tyhen also need to focus to quantity
                //    {
                //        if (currentrow < 0)// added to avoid error if barcode is empty and enter pressed
                //        {
                //            currentrow = 0;
                //        }
                //        dgvItemDet.CurrentCell = dgvItemDet.Rows[currentrow].Cells[iQuantity];
                //        dgvItemDet.CurrentCell.Selected = true;
                //        dgvItemDet.BeginEdit(true);
                //    }
                //    else if (colIndex==iCessAmount)
                //    {
                //        if (currentrow < 0)// added to avoid error if barcode is empty and enter pressed
                //        {
                //            currentrow = 0;
                //        }
                //        dgvItemDet.CurrentCell = dgvItemDet.Rows[currentrow].Cells[iTotalAmount];
                //        dgvItemDet.CurrentCell.Selected = true;
                //        dgvItemDet.BeginEdit(true);
                //    }
                //    else if (colIndex == iMargin)
                //    {
                //        if (currentrow < 0)// added to avoid error if barcode is empty and enter pressed
                //        {
                //            currentrow = 0;
                //        }
                //        dgvItemDet.CurrentCell = dgvItemDet.Rows[currentrow+1].Cells[iBarcode];
                //        dgvItemDet.CurrentCell.Selected = true;
                //        dgvItemDet.BeginEdit(true);
                //    }
                //    else
                //    {
                //        if (dgvItemDet.Rows.Count > currentrow)
                //        {
                //            currentrow = dgvItemDet.CurrentCell.RowIndex - 1;
                //            if (dgvItemDet.Rows[currentrow].Cells[colIndex].Visible == true)
                //            {
                //                dgvItemDet.CurrentCell = dgvItemDet.Rows[currentrow].Cells[colIndex];
                //            }
                //            else
                //            {
                //                dgvItemDet.CurrentCell = dgvItemDet.Rows[currentrow].Cells[colIndex + 1];

                //            }
                //            dgvItemDet.CurrentCell.Selected = true;
                //            dgvItemDet.BeginEdit(true);
                //        }
                //    }

                //}

                 if (e.KeyCode == Keys.F8)
                {
                    Itempopup();
                }
                else if (e.KeyCode == Keys.F7)
                {
                    string returnValue = string.Empty;
                    using (var search = new MarginPOSNew.Forms.Masters.FrmItem())
                    {
                        var dialogResult = search.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                        {
                            returnValue = Convert.ToString(search._retValue);
                        }
                    }
                    if (returnValue != "" && returnValue != null)
                    {
                        int indx = dgvItemDet.CurrentCell.RowIndex;
                        dgvItemDet.Rows.Add();
                        ItemId = Convert.ToInt64(returnValue.ToString());
                        Itemdetails(indx, ItemId);
                        ItemTotal(indx, 1, iQuantity);
                    }
                }

                else if (e.KeyCode == Keys.Delete)
                {
                        if (iName == dgvItemDet.CurrentCell.ColumnIndex || iBarcode == dgvItemDet.CurrentCell.ColumnIndex || iSlNo == dgvItemDet.CurrentCell.ColumnIndex)
                    {
                        {
                            if ((dgvItemDet.RowCount > 0) && (dgvItemDet.CurrentRow.Index != (dgvItemDet.Rows.Count - 1)))
                            {
                                dgvItemDet.Rows.RemoveAt(dgvItemDet.CurrentRow.Index);
                                for (int i = 0; i < dgvItemDet.Rows.Count; ++i)
                                {
                                    dgvItemDet.Rows[i].Cells[iSlNo].Value = i + 1;
                                }
                                GridSummary();
                                TotalSummary();

                            }
                        }

                    }
                }

                //SendKeys.Send("{TAB}");
                e.SuppressKeyPress = true;
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
                DialogResult dialogResult = MessageBox.Show("Are you sure want to Refresh " + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    RefreshForm();
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtTotDiscPer_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                decimal GrandTot = decimal.Parse(Convert.ToString(mtxtGrandTot.Text) == string.Empty ? "0.00" : Convert.ToString(mtxtGrandTot.Text));
                if (GrandTot > 0)
                {
                    if (Convert.ToString(mtxtTotDiscPer.Text) == ".")
                    {
                        mtxtTotDiscPer.Text = "0.";
                    }
                    decimal discountPer;
                    decimal discAmount;
                    decimal grandTot;
                    decimal misc;
                    discountPer = decimal.Parse(Convert.ToString(mtxtTotDiscPer.Text) == string.Empty ? "0.00" : Convert.ToString(mtxtTotDiscPer.Text));
                    grandTot = decimal.Parse(Convert.ToString(mtxtGrandTot.Text) == string.Empty ? "0.00" : Convert.ToString(mtxtGrandTot.Text));
                    misc = decimal.Parse(Convert.ToString(mtxtMiscCharges.Text) == string.Empty ? "0.00" : Convert.ToString(mtxtMiscCharges.Text));
                    discAmount = ((discountPer * (misc + grandTot)) / 100);
                    mtxtTotDiscAmnt.Text = Convert.ToString(discAmount.ToString("#.00"));
                    TotalSummary();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtTotDiscAmnt_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode == Keys.Enter)
                //{
                //    objGeneral.NextConrolOnEnterPress(sender, e);
                //}
                decimal GrandTot = decimal.Parse(Convert.ToString(mtxtGrandTot.Text) == string.Empty ? "0.00" : Convert.ToString(mtxtGrandTot.Text));
                if (GrandTot > 0)
                {
                    if (Convert.ToString(mtxtTotDiscAmnt.Text) == ".")
                    {
                        mtxtTotDiscAmnt.Text = "0.";
                    }
                    decimal discountPer;
                    decimal discAmount;
                    decimal grandTot;
                    decimal misc;
                    discAmount = decimal.Parse(Convert.ToString(mtxtTotDiscAmnt.Text) == string.Empty ? "0.00" : Convert.ToString(mtxtTotDiscAmnt.Text));
                    grandTot = decimal.Parse(Convert.ToString(mtxtGrandTot.Text) == string.Empty ? "0.00" : Convert.ToString(mtxtGrandTot.Text));
                    misc = decimal.Parse(Convert.ToString(mtxtMiscCharges.Text) == string.Empty ? "0.00" : Convert.ToString(mtxtMiscCharges.Text));
                    discountPer = ((discAmount / (misc + grandTot)) * 100);
                    mtxtTotDiscPer.Text = Convert.ToString(discountPer);
                    TotalSummary();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtAdjust_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                if (mtxtAdjust.Text == ".")
                {
                    mtxtAdjust.Text = "0.";
                }
                if (mtxtAdjust.Text != "-")
                {
                    TotalSummary();
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtLedger_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    LedgerPopup();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnLedgerAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //MarginPOSNew.Forms.Masters.FrmAccountLedger frmLedger = new MarginPOSNew.Forms.Masters.FrmAccountLedger();
                //frmLedger.ShowDialog();
                string returnValue = string.Empty;
                using (var search = new MarginPOSNew.Forms.Masters.FrmAccountLedger())
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);
                    }
                }
                if (returnValue != "" && returnValue != null)
                {
                    LedgerId = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnAccountLedger> bllSearch = new BllSearch<DmnAccountLedger>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = " tblAccountLedger ";
                    objSearch.WhereNdGroupBy = "Where LedgerId = " + LedgerId;
                    objSearch.OrderBy = "Order By LedgerName ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    //mtxtLedgerId.Text = Convert.ToString(LedgerId);
                    mtxtLedger.Text = Convert.ToString(gridSource[0].LedgerName);
                    mtxtAddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                    mtxtMobile.Text = Convert.ToString(gridSource[0].Mobile);
                    mtxtCreditDays.Text = Convert.ToString(gridSource[0].CreditDays);
                    mdtpDueDate.Value = Convert.ToDateTime(DateTime.Now.AddDays(gridSource[0].CreditDays));
                    existingAddress = Convert.ToString(gridSource[0].LedgerAddress);
                    existingMobile = Convert.ToString(gridSource[0].Mobile);
                    FillTransNoComboBox(gridSource[0].LedgerCode);
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void MbtnAdd_Click(object sender, EventArgs e)
        {
            //ListView mlvMisc = new ListView();
            try
            {
                decimal GrandTot = decimal.Parse(Convert.ToString(mtxtAmount.Text) == string.Empty ? "0.00" : Convert.ToString(mtxtAmount.Text));
                if (Convert.ToDecimal(GrandTot) > 0 && mcmbParticular.SelectedIndex != 0)
                {
                    string[] arr = new string[4];
                    ListViewItem itm;
                    //add items to ListView

                    arr[0] = (mlvMisc.Items.Count + 1).ToString();
                    arr[1] = mcmbParticular.Text;
                    arr[2] = mcmbSign.SelectedItem + mtxtAmount.Text;
                    arr[3] = mcmbParticular.SelectedValue.ToString();
                    itm = new ListViewItem(arr);
                    mlvMisc.Items.Add(itm);
                    miscTotal = Convert.ToDecimal(miscTotal + Convert.ToDecimal(arr[2]));
                    mtxtMiscCharges.Text = Convert.ToString(miscTotal.ToString("0.00"));
                    TotalSummary();
                    mtxtAmount.Clear();
                    mcmbParticular.SelectedIndex = 0;

                }
                else
                {
                    MetroMessageBox.Show(this, "Particulars And Amount are not empty or zero!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MlvMisc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (mlvMisc.Items.Count > 0)
                {
                    var amt = mlvMisc.SelectedItems[0].SubItems[2].Text;
                    miscTotal = Convert.ToDecimal(miscTotal - Convert.ToDecimal(amt));
                    mtxtMiscCharges.Text = Convert.ToString(miscTotal.ToString("0.00"));
                    mlvMisc.SelectedItems[0].Remove();
                    TotalSummary();
                }
            }
        }
        private void MbtnList_Click(object sender, EventArgs e)
        {
            try                                                                                                                                   
            {
                string returnValue = string.Empty;
                using (var search = new FrmListPurchase())
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);
                    }
                }
                if (returnValue.ToString() != "")
                {
                    RefreshForm();
                    PurchaseMasterId = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnPurchaseMaster> bllSearch = new BllSearch<DmnPurchaseMaster>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.Select = "P.EntryNo,P.EntryDate,P.EType,P.PaymentMode,P.LedgerId,L.LedgerName,L.LedgerAddress,L.Mobile,P.TransportNo," +
                        "P.DocumentNo,P.DocumentDate,L.CreditDays,P.DueDate,P.TotalAmount,P.DiscountPer,P.DiscountAmount,P.TotalMiscCharges,P.Remarks," +
                        "P.TotalNetAmount,P.AmountAdjust";
                    objSearch.Table = " tblPurchaseMaster P LEFT JOIN tblAccountLedger L ON P.LedgerId=L.LedgerId ";
                    objSearch.WhereNdGroupBy = "Where P.PurchaseMasterId = " + PurchaseMasterId;
                    objSearch.OrderBy = "Order By P.PurchaseMasterId ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    mtxtInvNo.Text = Convert.ToString(gridSource[0].EntryNo);
                    mdtpInvDate.Text = Convert.ToString(gridSource[0].EntryDate);
                    mcmbTax.SelectedValue = gridSource[0].EType;
                    mcmbMode.SelectedValue = gridSource[0].PaymentMode;
                    LedgerId = Convert.ToInt64(gridSource[0].LedgerId);
                    mcmbLedger.SelectedValue = gridSource[0].LedgerId;
                    mtxtLedger.Text = Convert.ToString(gridSource[0].LedgerName);
                    mtxtAddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                    mtxtMobile.Text = Convert.ToString(gridSource[0].Mobile);
                    LedgerCode = gridSource[0].LedgerCode;

                    existingAddress = Convert.ToString(gridSource[0].LedgerAddress);
                    existingMobile = Convert.ToString(gridSource[0].Mobile);


                    mcmbTransNo.SelectedValue = Convert.ToString(gridSource[0].TransportNo);
                    mtxtDocNo.Text = Convert.ToString(gridSource[0].DocumentNo);
                    DateTime date1 = gridSource[0].DocumentDate;
                    DateTime date2 = mdtpDocDate.MinDate;
                    int value = DateTime.Compare(date1, date2);
                    if (value > 0)
                        mdtpDocDate.Text = Convert.ToString(gridSource[0].DocumentDate);
                    mtxtCreditDays.Text = Convert.ToString(gridSource[0].CreditDays);
                    mdtpDueDate.Value = Convert.ToDateTime(gridSource[0].DueDate);
                    mtxtGrandTot.Text = Convert.ToString(gridSource[0].TotalAmount);
                    mtxtTotDiscPer.Text = Convert.ToString(gridSource[0].DiscountPer);
                    mtxtTotDiscAmnt.Text = Convert.ToString(gridSource[0].DiscountAmount);
                    mtxtMiscCharges.Text = Convert.ToString(gridSource[0].TotalMiscCharges);
                    mtxtRemarks.Text = Convert.ToString(gridSource[0].Remarks);
                    mtxtNetAmount.Text = Convert.ToString(gridSource[0].TotalNetAmount);
                    mtxtAdjust.Text = Convert.ToString(gridSource[0].AmountAdjust);
                    objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = " tblPurchaseDetails P LEFT JOIN tblItem I ON P.ItemId=I.ItemId LEFT JOIN tblUnit U ON P.UnitId=U.UnitId" +
                        " LEFT JOIN tblGeneralOptions G ON P.TaxType = G.GeneralId";
                    objSearch.WhereNdGroupBy = "Where P.PurchaseMasterId = " + PurchaseMasterId;
                    objSearch.OrderBy = "Order By P.PurchaseMasterId ";
                    var gridSourceItem = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSourceItem.Count > 0)
                    {
                        dgvItemDet.Rows.Add(gridSourceItem.Count);
                        var lst = SelNegativeAllowed();
                        if (lst == 1)
                        {
                            for (int i = 0; i < (gridSourceItem.Count); i++)
                            {

                                dgvItemDet.Rows[i].Cells[iSlNo].Value = (i + 1).ToString("#");
                                dgvItemDet.Rows[i].Cells[iPurchaseDetailId].Value = Convert.ToString(gridSourceItem[i].PurchaseDetailId);
                                dgvItemDet.Rows[i].Cells[iItemId].Value = Convert.ToString(gridSourceItem[i].ItemId);
                                dgvItemDet.Rows[i].Cells[iItemCode].Value = Convert.ToString(gridSourceItem[i].ItemCode);
                                dgvItemDet.Rows[i].Cells[iBarcode].Value = Convert.ToString(gridSourceItem[i].ItemBarCode);
                                dgvItemDet.Rows[i].Cells[iName].Value = Convert.ToString(gridSourceItem[i].SearchName);
                                dgvItemDet.Rows[i].Cells[iUnitId].Value = Convert.ToString(gridSourceItem[i].UnitId);
                                dgvItemDet.Rows[i].Cells[iUnit].Value = Convert.ToString(gridSourceItem[i].UnitName);
                                dgvItemDet.Rows[i].Cells[iLockSaleRate].Value = gridSourceItem[i].LockSaleRate;
                                dgvItemDet.Rows[i].Cells[iSellingPer].Value = gridSourceItem[i].SellingPer;
                                dgvItemDet.Rows[i].Cells[iSalesRate].Value = gridSourceItem[i].SaleRate;
                                dgvItemDet.Rows[i].Cells[iQohToSubtract].Value = Convert.ToString(gridSourceItem[i].PacketQty);
                                dgvItemDet.Rows[i].Cells[iItemQOH].Value = Convert.ToString(gridSourceItem[i].QuantityOnHand);
                                dgvItemDet.Rows[i].Cells[iStockItem].Value = Convert.ToString(gridSourceItem[i].StockItem);
                                dgvItemDet.Rows[i].Cells[iExpdate].Value = Convert.ToDateTime(gridSourceItem[i].ExpDate);


                                #region Combo

                                FillGridUnitCombo(Convert.ToInt64(gridSourceItem[i].ItemId), i);

                                #endregion

                                dgvItemDet.Rows[i].Cells[iUnitId].Value = Convert.ToString(gridSourceItem[i].UnitId);//need to hange unit id tounit name  also ned to bind combo box 
                                DataGridViewComboBoxCell cel = (DataGridViewComboBoxCell)dgvItemDet.Rows[i].Cells[iUnit];

                                dgvItemDet.Rows[i].Cells[iQuantity].Value = Convert.ToString(gridSourceItem[i].Quantity);
                                dgvItemDet.Rows[i].Cells[iPurchaseRate].Value = Convert.ToString(gridSourceItem[i].PurchaseRate);
                                dgvItemDet.Rows[i].Cells[iMRP].Value = Convert.ToString(gridSourceItem[i].MRP);
                                dgvItemDet.Rows[i].Cells[iDiscPer].Value = Convert.ToString(gridSourceItem[i].DiscountPer);
                                dgvItemDet.Rows[i].Cells[iDiscAmount].Value = Convert.ToString(gridSourceItem[i].DiscountAmount);
                                dgvItemDet.Rows[i].Cells[iPurchaseTaxIncl].Value = Convert.ToString(gridSourceItem[i].PurchaseTaxIncl);
                                dgvItemDet.Rows[i].Cells[iTaxPer].Value = Convert.ToString(gridSourceItem[i].TaxPer);
                                dgvItemDet.Rows[i].Cells[iTaxAmount].Value = Convert.ToString(gridSourceItem[i].TaxAmount);
                                dgvItemDet.Rows[i].Cells[iTaxText].Value = Convert.ToString(gridSourceItem[i].Caption);
                                dgvItemDet.Rows[i].Cells[iTaxType].Value = Convert.ToString(gridSourceItem[i].TaxType);
                                dgvItemDet.Rows[i].Cells[iCessPer].Value = Convert.ToString(gridSourceItem[i].CessPer);
                                dgvItemDet.Rows[i].Cells[iCessAmount].Value = Convert.ToString(gridSourceItem[i].CessAmount);
                                dgvItemDet.Rows[i].Cells[iRate].Value = Convert.ToString(gridSourceItem[i].IRate);
                                dgvItemDet.Rows[i].Cells[iSalesRate].Value = Convert.ToString(gridSourceItem[i].SellingPer);

                                dgvItemDet.Rows[i].Cells[iItemMrp].Value = Convert.ToString(gridSourceItem[i].MRP1);
                                dgvItemDet.Rows[i].Cells[iItemSelPer].Value = Convert.ToString(gridSourceItem[i].SellingPer);

                                ItemTotal(i, gridSourceItem[i].Quantity, iQuantity);
                            }
                        }
                    }
                    objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = " tblPurchaseMiscCharges M LEFT JOIN tblGeneralOptions G ON M.GeneralId=G.GeneralId ";
                    objSearch.WhereNdGroupBy = "Where M.PurchaseMasterId = " + PurchaseMasterId;
                    objSearch.OrderBy = "Order By M.PurchaseMasterId ";
                    var gridMisc = bllSearch.GetSearchData("prdSearch", objSearch);
                    mlvMisc.Clear();
                    mlvMisc.Columns.Add("Sl No", 100, HorizontalAlignment.Left);
                    mlvMisc.Columns.Add("Particulars", 350, HorizontalAlignment.Left);
                    mlvMisc.Columns.Add("Price", 200, HorizontalAlignment.Right);
                    mlvMisc.Columns.Add("Id", 0);
                    // this.mlvMisc. = HorizontalAlignment.Center;
                    mlvMisc.View = View.Details;
                    for (int i = 0; i < (gridMisc.Count); i++)
                    {
                        string[] arr = new string[4];
                        ListViewItem itm;
                        //add items to ListView

                        arr[0] = (i + 1).ToString();
                        arr[1] = Convert.ToString(gridMisc[i].Caption);
                        arr[2] = Convert.ToString(gridMisc[i].MiscAmount);
                        arr[3] = Convert.ToString(gridMisc[i].GeneralId);
                        itm = new ListViewItem(arr);
                        mlvMisc.Items.Add(itm);
                    }
                    TotalSummary();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //objGeneral.DecimalTextBoxSign(sender, e);
                objGeneral.DecimalTextBox3digits(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void McmbTax_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvItemDet.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvItemDet.Rows.Count; ++i)
                    {
                        decimal editingValue = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value));
                        ItemTotal(i, editingValue, iQuantity);
                    }
                    TotalSummary();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtLedger_Enter(object sender, EventArgs e)
        {
            try
            {
                LedgerPopup();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void MtxtMobile_KeyPress(object sender, KeyPressEventArgs e)
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

        private void FrmPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
                //objGeneral.NextConrolOnEnterPress(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }



        private void DgvItemDet_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void DgvItemDet_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                dgvItemDet.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        public DateTimePicker oDateTimePicker;
        private void DgvItemDet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int currentRow = dgvItemDet.CurrentCell.RowIndex;
                int currentColumn = dgvItemDet.CurrentCell.ColumnIndex;
                if (currentColumn == iSlNo)
                {
                    dgvItemDet.Rows[currentRow].Selected = true;
                }
                if (dgvItemDet.CurrentCell.ColumnIndex==iExpdate)
                {
                     oDateTimePicker = new DateTimePicker();  //DateTimePicker 

                    //Adding DateTimePicker control into DataGridView
                    dgvItemDet.Controls.Add(oDateTimePicker);

                    // Intially made it invisible
                    oDateTimePicker.Visible = false;

                    // Setting the format (i.e. 2014-10-10)
                    oDateTimePicker.Format = DateTimePickerFormat.Custom;
                    oDateTimePicker.CustomFormat = "dd/MM/yyyy";
                    //oDateTimePicker.Format = DateTimePickerFormat.Short;  //

                    oDateTimePicker.TextChanged += new EventHandler(dateTimePicker_OnTextChange);

                    // Now make it visible
                    oDateTimePicker.Visible = true;

                    // It returns the retangular area that represents the Display area for a cell
                    Rectangle oRectangle = dgvItemDet.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                    //Setting area for DateTimePicker Control
                    oDateTimePicker.Size = new Size(oRectangle.Width, oRectangle.Height);

                    // Setting Location
                    oDateTimePicker.Location = new Point(oRectangle.X, oRectangle.Y);

                    oDateTimePicker.CloseUp += new EventHandler(oDateTimePicker_CloseUp);


                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void dateTimePicker_OnTextChange(object sender, EventArgs e)
        {
           
            dgvItemDet.CurrentCell.Value = oDateTimePicker.Text.ToString();
        }
        void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            oDateTimePicker.Visible = false;
        }

        private void FrmPurchase_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F6)
                    MbtnSave_Click(sender, e);
                if (e.KeyCode == Keys.F3)
                    MbtnList_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    MbtnRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void DgvItemDet_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool savestatus;
                DmnItem dmnItem = new DmnItem();
                BllItem bllItem = new BllItem();
                if (dgvItemDet.CurrentCell.ColumnIndex == iPurchaseRate)
                {
                    if (string.Compare(Convert.ToString(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iPurchaseRate].Value), Convert.ToString((dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iRate].Value))) != 0)
                    {
                        //If MsgBox("Old Rate " & Nz(rs1!prate, 0) & " Update New Rate...???", vbQuestion + vbYesNo + vbDefaultButton2, "Error") = vbYes Then
                        DialogResult results = MetroMessageBox.Show(this, "\n Update the Purchase Rate : " + dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iPurchaseRate].Value.ToString() + " ? ", "Save confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (results == DialogResult.Yes)
                        {
                            dmnItem.ItemId = Convert.ToInt64(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iItemId].Value);
                            dmnItem.FieldValue = decimal.Parse(Convert.ToString(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iPurchaseRate].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iPurchaseRate].Value));

                            dmnItem.UnitId = Convert.ToInt64(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iUnitId].Value);
                            dmnItem.FieldName = "PurchaseRate";
                            dmnItem.ModifiedBy = UserId;
                            var lstData = bllItem.UpdateItemFromPurchaseGrid("prdUpdateItemFromPurchaseGrid", dmnItem);
                            savestatus = lstData;
                            if (!savestatus)
                                MetroMessageBox.Show(this, "Save failed " + dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iPurchaseRate].Value.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                    }
                }
               
                if (dgvItemDet.CurrentCell.ColumnIndex == iSellingPer)
                {
                    if (string.Compare(Convert.ToString(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iSellingPer].Value), Convert.ToString((dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iItemSelPer].Value))) != 0)
                    {

                        DialogResult results = MetroMessageBox.Show(this, "\n Update the Selling % : " + dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iSellingPer].Value.ToString() + " ? ", "Save confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (results == DialogResult.Yes)
                        {
                            dmnItem.ItemId = Convert.ToInt64(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iItemId].Value);
                            dmnItem.FieldValue = Convert.ToDecimal(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iSellingPer].Value);
                            dmnItem.UnitId = Convert.ToInt64(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iUnitId].Value);
                            dmnItem.FieldName = "SellingPer";
                            dmnItem.ModifiedBy = UserId;
                            var lstData = bllItem.UpdateItemFromPurchaseGrid("prdUpdateItemFromPurchaseGrid", dmnItem);
                            savestatus = lstData;

                            dmnItem.ItemId = Convert.ToInt64(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iItemId].Value);
                            dmnItem.FieldValue = Convert.ToDecimal(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iSalesRate].Value);
                            dmnItem.UnitId = Convert.ToInt64(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iUnitId].Value);
                            dmnItem.FieldName = "SaleRate";
                            dmnItem.ModifiedBy = UserId;
                            var lstData1 = bllItem.UpdateItemFromPurchaseGrid("prdUpdateItemFromPurchaseGrid", dmnItem);
                            savestatus = lstData1;


                            if (!savestatus)
                                MetroMessageBox.Show(this, "Save failed " + dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iSellingPer].Value.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                    }
                }
                if (dgvItemDet.CurrentCell.ColumnIndex == iMRP)
                {
                    if (string.Compare(Convert.ToString(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iMRP].Value), Convert.ToString((dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iItemMrp].Value))) != 0)
                    {
                        DialogResult results = MetroMessageBox.Show(this, "\n Update MRP changes : " + dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iMRP].Value.ToString() + " ? ", "Save confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (results == DialogResult.Yes)
                        {
                            dmnItem.ItemId = Convert.ToInt64(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iItemId].Value);
                            dmnItem.FieldValue = Convert.ToDecimal(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iMRP].Value);
                            dmnItem.UnitId = Convert.ToInt64(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iUnitId].Value);
                            dmnItem.FieldName = "MRP";
                            dmnItem.ModifiedBy = UserId;
                            var lstData = bllItem.UpdateItemFromPurchaseGrid("prdUpdateItemFromPurchaseGrid", dmnItem);
                            savestatus = lstData;
                            if (!savestatus)
                                MetroMessageBox.Show(this, "Save failed " + dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iMRP].Value.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                    }
                }

                ////....29/01/2020--tax rate updation in to mastre table by rahul

                //if (dgvItemDet.CurrentCell.ColumnIndex == iExpdate)
                //{
                //    if (string.Compare(Convert.ToString(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iExpdate].Value), Convert.ToString((dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iExpdate].Value))) != 0)
                //    {
                //        DialogResult results = MetroMessageBox.Show(this, "\n Update MRP changes : " + dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iExpdate].Value.ToString() + " ? ", "Save confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //        if (results == DialogResult.Yes)
                //        {
                //            dmnItem.ItemId = Convert.ToInt64(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iItemId].Value);
                //            dmnItem.FieldValue = Convert.ToDecimal(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iExpdate].Value);
                //            dmnItem.UnitId = Convert.ToInt64(dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iUnitId].Value);
                //            dmnItem.FieldName = "ExpDate";
                //            dmnItem.ModifiedBy = UserId;
                //            var lstData = bllItem.UpdateItemFromPurchaseGrid("prdUpdateItemFromPurchaseGrid", dmnItem);
                //            savestatus = lstData;
                //            if (!savestatus)
                //                MetroMessageBox.Show(this, "Save failed " + dgvItemDet.Rows[dgvItemDet.CurrentCell.RowIndex].Cells[iMRP].Value.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                //        }
                //    }
                //}


            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void MtxtRemarks_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Tab)
                {
                    mbtnSave.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        private void MtxtTotDiscAmnt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    mtxtAdjust.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void DgvItemDet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvItemDet.CurrentCell.ColumnIndex == iBarcode || dgvItemDet.CurrentCell.ColumnIndex == iName)
                    Itempopup();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void McmbTransNo_Click(object sender, EventArgs e)
        {
            try
            {
                long sel = Convert.ToInt64(mcmbTransNo.SelectedValue);
                FillTransNoComboBox(LedgerCode);
                mcmbTransNo.SelectedValue = sel;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void McmbTransNo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    pnlTransPort.Show();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void BtnTransAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (LedgerCode != string.Empty && (Convert.ToString(mtxtTransportNoAdd.Text) != string.Empty))
                {
                    DmnGeneralOptions dmnGen = new DmnGeneralOptions();
                    BllGeneralOptions bllGen = new BllGeneralOptions();
                    dmnGen.GTypeId = Convert.ToInt32(10033);
                    //dmnGen.GTypeId = Convert.ToInt32(ClsGeneral.Get_Gtype_ID("Gtype", "TRANSPORTATION NO"));
                    //dmnGen.Caption = "TRANSPORTATION NO";
                    //dmnGen.GTypeSubValue1 = decimal.Parse("");
                    //dmnGen.GTypeSubValue2 = decimal.Parse(null);
                    //dmnGen.GTypeSubValue3 = decimal.Parse(null);
                    //dmnGen.GTypeSubDate1 = null;
                    //dmnGen.GTypeSubDate2 = null;
                    //dmnGen.GTypeSubDate3 = null;
                    dmnGen.GTypeSubText1 = LedgerCode.Trim();
                    dmnGen.GTypeSubText2 = mtxtTransportNoAdd.Text.Trim();
                    //dmnGen.GTypeSubText3 = null;
                    dmnGen.CreatedBy = UserId;
                    bool insertStatus = bllGen.AddData("prdInsertGeneralOptions", dmnGen);
                    {
                        if (insertStatus)
                        {
                            MetroMessageBox.Show(this, "\nSuccessfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                            FillTransNoComboBox(LedgerCode);
                            mcmbTransNo.SelectedIndex = mcmbTransNo.FindString(mtxtTransportNoAdd.Text);
                            mtxtTransportNoAdd.Text = "";
                            pnlTransPort.Hide();
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "Save Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                pnlTransPort.Hide();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtAdjust_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    mbtnSave.Focus();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        #endregion

        private void mcmbLedger_SelectedValueChanged(object sender, EventArgs e)
        {
            //GetLedgerDetails();
        }
        public void GetLedgerDetails()
        {
            if (LedgerId > 0)
            {

            
            //LedgerId = Convert.ToInt32(mcmbLedger.SelectedItem);
            BllSearch<DmnAccountLedger> bllSearch = new BllSearch<DmnAccountLedger>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = " tblAccountLedger ";
            objSearch.WhereNdGroupBy = "Where LedgerId = " + LedgerId;
            objSearch.OrderBy = "Order By LedgerName ";
            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
            //mtxtLedgerId.Text = Convert.ToString(LedgerId);
            mtxtLedger.Text = Convert.ToString(gridSource[0].LedgerName);
            mtxtAddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
            mtxtMobile.Text = Convert.ToString(gridSource[0].Mobile);
            mtxtCreditDays.Text = Convert.ToString(gridSource[0].CreditDays);
            mdtpDueDate.Value = Convert.ToDateTime(DateTime.Now.AddDays(gridSource[0].CreditDays));
            existingAddress = Convert.ToString(gridSource[0].LedgerAddress);
            existingMobile = Convert.ToString(gridSource[0].Mobile);
            LedgerCode = gridSource[0].LedgerCode;
            FillTransNoComboBox(gridSource[0].LedgerCode);
            }

        }

      private void mcmbLedger_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LedgerId = Convert.ToInt32(mcmbLedger.SelectedValue);
            GetLedgerDetails();

        }

        private void mcmbLedger_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                LedgerPopup(); 

            }

        }

        private void mbtnDelete_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvItemDet_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                e.SuppressKeyPress = true;
                int iColumn = dgvItemDet.CurrentCell.ColumnIndex;
                int iRow = dgvItemDet.CurrentCell.RowIndex;

                if (iColumn == dgvItemDet.ColumnCount - 1)
                {
                    if (dgvItemDet.RowCount > (iRow + 1))
                    {
                        dgvItemDet.CurrentCell = dgvItemDet[3, iRow + 1];
                    }
                    else
                    {
                        //focus next control
                    }
                }
                else
                {
                    if (iColumn == 4 || iColumn == 15 || iColumn == 18)
                    {
                        dgvItemDet.CurrentCell = dgvItemDet[iColumn + 2, iRow];
                    }
                    else if (iColumn == 21)
                    {
                        dgvItemDet.CurrentCell = dgvItemDet[iColumn + 3, iRow];
                    }
                    else if (iColumn == 26)
                    {
                        iColumn = 35;
                        dgvItemDet.CurrentCell = dgvItemDet[iColumn, iRow];
                    }
                    else
                    {
                        dgvItemDet.CurrentCell = dgvItemDet[iColumn + 1, iRow];
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
    }
}
