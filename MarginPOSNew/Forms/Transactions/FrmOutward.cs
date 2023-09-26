using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using MetroFramework;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarginPOSNew.Forms.Transactions
{
    public partial class FrmOutward : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        ClsGeneral clsGeneral = new ClsGeneral();
        //long BranchId = Convert.ToInt64(FrmDashboard.BranchId);
        //long UserId = Convert.ToInt64(FrmDashboard.UserId);
        //long FinancialYearId = Convert.ToInt64(FrmDashboard.FinancialYearId);
        long UserId = Forms.UserAccounts.FrmLogin.UserId;//1
        long BranchId = Forms.UserAccounts.FrmLogin.BranchId;
        long pUserType = Forms.UserAccounts.FrmLogin.UserAccountTypeId;
        long FinancialYearId = Convert.ToInt64(FrmDashboard.FinancialYearId);


        long OutwardId = 0; // For Update

        long ItemId = 0;
        long SlNo = 0;
        long LedgerId = 0; //For Customer Popup
        long toid = 0;
        long PDetailId = 0;
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
        decimal PurchaseRatemrp = 0;
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
        const int iPDocNo = 1;
        const int iItemId = 2;
        const int iItemCode = 3;
        const int iBarcode = 4;
        const int iName = 5;
        const int iUnitId = 6;
        const int iUnit = 7;
        const int iQuantity = 8;
        const int iPurchaseRate = 9;
        const int iSellingPer = 10;
        const int iSalesRate = 11;
        const int iMRP = 12;
        const int iAmount = 13;
        const int iDiscPer = 14;
        const int iDiscAmount = 15;
        const int iGrossAmount = 16;
        const int iPurchaseTaxIncl = 17;
        const int iTaxText = 18;
        const int iTaxAmount = 19;
        const int iTaxType = 20;
        const int iCessPer = 21;
        const int iCessAmount = 22;
        const int iOutwardDetailsId = 23;//used in edit od Sales
        const int iRate = 24;
        const int iTotalAmount = 25;
        const int iCost = 26;
        const int iMargin = 27;
        const int iItemSaveAmount = 28;//savings in each item
        const int iLockSaleRate = 29;
        const int iQohToSubtract = 30;//this amount is decreased from qoh
        const int iItemQOH = 31;//to store item qoh
        const int iStockItem = 32;
        const int iItemSelPer = 33;
        const int iItemMrp = 34;
        const int iTaxPer = 35;
        #endregion
        public FrmOutward()
        {
            InitializeComponent();
            RefreshForm();
        }

        #region InvoiceNo. Generation


        public void PurchaseReturnInvoiceNo()
        {
            BllSearch<DmnBranch> bllSearch = new BllSearch<DmnBranch>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "OutwardSrlNo";
            objSearch.Table = " tblBranch ";
            objSearch.WhereNdGroupBy = " Where BranchId=" + BranchId;
            var srlNo = bllSearch.GetSearchData("prdSearch", objSearch);
            mtxtInvNo.Text = Convert.ToString(srlNo[0].OutwardSrlNo + 1);
        }
        #endregion


        public bool IsSavable()
        {
            Control[] controllist = { mtxtDocNo, mtxtInvNo, mdtpInvDate, mcmbTax, mcmbMode, mtxtLedger };
            if (clsGeneral.IsControlNullOrDefault(this, controllist) == true)
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
            OutwardId = 0;
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
            clsGeneral.ResetSelectedMetroTextBoxes(listTbx);
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
            //clsGeneral.ResetSelectedMetroTextBoxes(listTbx);
            dgvItemDet.Rows.Clear();
            //dgvItemDet.Rows[0].Cells[iBarcode].Selected = true;
            mcmbTransNo.DataSource = null;
            mcmbTransNo.Items.Clear();
            FillTaxComboBox();
            FillParticularComboBox();
            FillPaymentComboBox();
            //FillLedgerComboBox();
            FillTaxTypeComboBox();
            //FillTransNoComboBox();
            PurchaseReturnInvoiceNo();
            FillFrom();
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
        public void FillFrom()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();

            objSearch.Select = "BranchId,BranchName as value";
            objSearch.Table = " tblBranch  ";
            objSearch.WhereNdGroupBy = " WHERE Status = 0 And BranchId <> " + BranchId;
            objSearch.OrderBy = "order by value asc";


            //objSearch.Select = "LedgerId,LedgerName";
            //objSearch.Table = " tblAccountLedger ";
            //objSearch.WhereNdGroupBy = " Where Status = 0 a";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbFrom.DataSource = gridSource;
            mcmbFrom.ValueMember = "Id";
            mcmbFrom.DisplayMember = "Value";
        }
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

        public void FillPDocNoCombo()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = "InwardMasterId,EntryNo";
            dmnSearch.Table = "tblInward";
            dmnSearch.WhereNdGroupBy = " Where fromid=" + toid;
            //dmnSearch.OrderBy = " ORDER BY DocumentNo DESC";
            var gridSource = bllSearch.GetSearchData("prdComboFill", dmnSearch);
            CnPDocNo.DataSource = gridSource;
            CnPDocNo.ValueMember = "Id";
            CnPDocNo.DisplayMember = "Value";
        }
        #endregion

        public void Itemdetails(int CurrentIndex, long PDetailId)
        {
            SlNo = CurrentIndex;
            BllSearch<DmnOutwardItemDetails> bllSearch = new BllSearch<DmnOutwardItemDetails>();
            DmnSearch dmnSearch = new DmnSearch();
            if (!string.IsNullOrWhiteSpace(Convert.ToString(dgvItemDet.CurrentRow.Cells[iPDocNo].Value)))
            {
                dmnSearch.Select = "PD.ItemId,PD.ItemCode,PD.ItemBarcode,I.SearchName,I.PurchaseTaxIncl ,PD.UnitId,U.UnitName,PD.PurchaseRate," +
                   "G.GTypeSubValue1 AS IGSTPer,PD.CessPer,PD.TaxType,PD.MRP,I.SellingPer,I.LockSaleRate,I.PacketQty,I.QuantityOnhand,I.SaleRate," +
                   "I.StockItem,G.Caption as TaxTypeName,PD.TaxPer";
                dmnSearch.Table = "tblOutDetails as PD " +
                                  "Left Join tblItem as I on PD.ItemId = I.ItemId " +
                                  "Left Join tblUnit as U on PD.UnitId = U.UnitId " +
                                  "Left Join tblGeneralOptions as G on PD.TaxType = G.GeneralId";
                dmnSearch.WhereNdGroupBy = " Where PD.OutwardDetailsId =" + PDetailId;

            }
            else
            {

                dmnSearch.Select = "I.ItemId,I.ItemCode,I.Barcode as ItemBarcode,I.SearchName,I.PurchaseTaxIncl,I.MainUnitId as UnitId,U.UnitName,I.PurchaseRate1 as PurchaseRate," +
                "I.SGSTPer,I.CGSTPer,I.IGSTPer as TaxPer,I.CessPer,I.TaxType as TaxTypeName,I.MRP1 as MRP,I.SellingPer,I.LockSaleRate,I.PacketQty,I.QuantityOnhand,I.StockItem,G.Caption as TaxTypeName,I.SaleRate as SaleRate";
                dmnSearch.Table = " tblItem I LEFT JOIN tblUnit U ON I.MainUnitId=U.UnitId LEFT JOIN tblGeneralOptions G ON I.TaxType = G.GeneralId";
                dmnSearch.WhereNdGroupBy = "Where I.ItemId = " + PDetailId;
                dmnSearch.OrderBy = "Order By I.ItemName ";

                //dmnSearch.Select = "I.ItemId as ItemId,I.SearchName as SearchName,I.ItemCode as ItemCode,I.Barcode as ItemBarcode,C.CategoryName as Field4,U.UnitName as Field5," +
                //" I.SaleRate as Field6, I.MRP1 as Field7, I.IGSTPer as Field8, I.QuantityOnHand as Field9";
                //dmnSearch.Table = " tblItem I LEFT JOIN tblCategory C  ON I.CategoryId = C.CategoryId LEFT JOIN tblUnit U ON I.MainUnitId=U.UnitId";
                //dmnSearch.WhereNdGroupBy = " WHERE I.Status=0 And I.BranchId = " + BranchId + " and I.ItemId=" + PDetailId ;
                //dmnSearch.OrderBy = "order by I.SearchName ASC ";

            }




            var itemSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
            dgvItemDet.Rows[CurrentIndex].Cells[iSlNo].Value = SlNo + 1;
            dgvItemDet.Rows[CurrentIndex].Cells[iPDocNo].Value = dgvItemDet.CurrentRow.Cells[iPDocNo].Value;
            dgvItemDet.Rows[CurrentIndex].Cells[iItemId].Value = Convert.ToString(itemSource[0].ItemId);
            dgvItemDet.Rows[CurrentIndex].Cells[iItemCode].Value = Convert.ToString(itemSource[0].ItemCode);
            dgvItemDet.Rows[CurrentIndex].Cells[iBarcode].Value = Convert.ToString(itemSource[0].ItemBarcode);
            dgvItemDet.Rows[CurrentIndex].Cells[iName].Value = Convert.ToString(itemSource[0].SearchName);
            dgvItemDet.Rows[CurrentIndex].Cells[iQuantity].Value = 1;

            dgvItemDet.Rows[CurrentIndex].Cells[iUnitId].Value = Convert.ToString(itemSource[0].UnitId);
            dgvItemDet.Rows[CurrentIndex].Cells[iUnit].Value = Convert.ToString(itemSource[0].UnitName);
            dgvItemDet.Rows[CurrentIndex].Cells[iPurchaseRate].Value = Convert.ToString(itemSource[0].PurchaseRate);
            dgvItemDet.Rows[CurrentIndex].Cells[iRate].Value = Convert.ToString(itemSource[0].PurchaseRate);
            dgvItemDet.Rows[CurrentIndex].Cells[iTaxType].Value = Convert.ToString(itemSource[0].TaxType);
            dgvItemDet.Rows[CurrentIndex].Cells[iMRP].Value = Convert.ToString(itemSource[0].MRP);

            //FillGridUnitCombo(Convert.ToInt64(itemSource[0].ItemId), 0);
            FillGridUnitCombo(Convert.ToInt64(itemSource[0].ItemId), CurrentIndex);

            dgvItemDet.Rows[CurrentIndex].Cells[iSellingPer].Value = Convert.ToString(itemSource[0].SellingPer);
            dgvItemDet.Rows[CurrentIndex].Cells[iPurchaseTaxIncl].Value = Convert.ToString(itemSource[0].PurchaseTaxIncl);
            dgvItemDet.Rows[CurrentIndex].Cells[iTaxPer].Value = Convert.ToString((itemSource[0].TaxPer));
            dgvItemDet.Rows[CurrentIndex].Cells[iTaxText].Value = Convert.ToString(itemSource[0].TaxTypeName);

            //decimal PurchaseRate = Convert.ToDecimal((itemSource[0].PurchaseRate) * (100 + itemSource[0].SellingPer) / 100);
            decimal PurchaseRate = Convert.ToDecimal((itemSource[0].SaleRate));
            PurchaseRatemrp = PurchaseRate;

            dgvItemDet.Rows[CurrentIndex].Cells[iPurchaseRate].Value = PurchaseRate.ToString("#.00");
            //dgvItemDet.Rows[CurrentIndex].Cells[iItemSaveAmount].Value = Convert.ToString(itemSource[0].MRP - (itemSource[0].PurchaseRate) * (100 + itemSource[0].SellingPer) / 100);
            dgvItemDet.Rows[CurrentIndex].Cells[iItemSaveAmount].Value = Convert.ToString(itemSource[0].MRP - (itemSource[0].SaleRate));
            dgvItemDet.Rows[CurrentIndex].Cells[iLockSaleRate].Value = Convert.ToString(itemSource[0].LockSaleRate);
            dgvItemDet.Rows[CurrentIndex].Cells[iQohToSubtract].Value = 1;//initially main unit is loaded so default qoh to rduce i 1
            dgvItemDet.Rows[CurrentIndex].Cells[iItemQOH].Value = Convert.ToString(itemSource[0].QuantityOnhand);
            dgvItemDet.Rows[CurrentIndex].Cells[iStockItem].Value = Convert.ToString(itemSource[0].StockItem);

            dgvItemDet.Rows[CurrentIndex].Cells[iItemMrp].Value = Convert.ToString(itemSource[0].MRP);
            dgvItemDet.Rows[CurrentIndex].Cells[iCessPer].Value = Convert.ToString(itemSource[0].CessPer);
            int currentrow = CurrentIndex;
            discper = decimal.Parse(Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iDiscPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iDiscPer].Value));
            disc = decimal.Parse(Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iDiscAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iDiscAmount].Value));
            decimal value = 0;
            int columnIndex = 0;
            ItemTotal(currentrow, value, columnIndex);
            TotalSummary();


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
            //_objSearch.WhereNdGroupBy = " WHERE L.Status = 0 And L.BranchId = " + BranchId;
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
                mtxtLedger.Text = Convert.ToString(gridSource[0].LedgerName);
                mtxtAddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                mtxtMobile.Text = Convert.ToString(gridSource[0].Mobile);
                mtxtCreditDays.Text = Convert.ToString(gridSource[0].CreditDays);
                mdtpDueDate.Value = Convert.ToDateTime(DateTime.Now.AddDays(gridSource[0].CreditDays));
                existingAddress = Convert.ToString(gridSource[0].LedgerAddress);
                existingMobile = Convert.ToString(gridSource[0].Mobile);
                LedgerCode = gridSource[0].LedgerCode;
                FillTransNoComboBox(gridSource[0].LedgerCode);
                FillPDocNoCombo();
            }
        }
        public void ItemPopup()
        {
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
            if (!string.IsNullOrWhiteSpace(Convert.ToString(dgvItemDet.CurrentRow.Cells[iPDocNo].Value)))
            {
                _objSearch.ColumnHeaders = "Id,Name,Code,Barcode,PQty,PurchaseRate";

                _objSearch.Select = "PD.OutwardDetailsId as Id,I.SearchName as Field1 ,PD.ItemCode as Field2,PD.ItemBarCode as Field3,PD.Quantity as Field4,PD.PurchaseRate as Field5 ";
                _objSearch.Table = "tblOutDetails AS PD  left JOIN tblItem as I on PD.ItemId = I.ItemId";
                _objSearch.WhereNdGroupBy = " WHERE PD.OutwardId =" + dgvItemDet.CurrentRow.Cells[iPDocNo].Value;

            }
            else
            {
                //_objSearch.ColumnHeaders = "Id,Item,Code,Barcode,Category,Unit,PurchaseRate,MRP,Tax(%),QOH";

                //_objSearch.Select = "I.ItemId as Id,I.SearchName as Field1,I.ItemCode as Field2,I.Barcode as Field3,C.CategoryName as Field4,U.UnitName as Field5," +
                //                " I.SaleRate as Field6, I.MRP1 as Field7, I.IGSTPer as Field8, I.QuantityOnHand as Field9";
                //_objSearch.Table = " tblItem I LEFT JOIN tblCategory C  ON I.CategoryId = C.CategoryId LEFT JOIN tblUnit U ON I.MainUnitId=U.UnitId";
                //_objSearch.WhereNdGroupBy = " WHERE I.Status=0 And I.BranchId = " + BranchId;
                //_objSearch.OrderBy = "order by I.SearchName ASC ";
                _objSearch.ColumnHeaders = "Id,Name,Barcode,Code,Category,Unit,PurchaseRate,PurchaseRate,MRP,Tax(%),QOH";
                _objSearch.Select = "I.ItemId as Id,I.SearchName as Field1,I.Barcode as Field2, I.ItemCode as Field3, C.CategoryName as Field4,U.UnitName as Field5," +
                    "I.PurchaseRate1 as Field6,I.SaleRate as Field7, I.MRP1 as Field8, (I.IGSTPer) as Field9, I.QuantityOnHand as Field10";
                _objSearch.Table = " tblItem I LEFT JOIN tblCategory C  ON I.CategoryId = C.CategoryId LEFT JOIN tblUnit U ON I.MainUnitId=U.UnitId";
                _objSearch.WhereNdGroupBy = " WHERE I.Status=0 AND I.BranchId = " + BranchId;
                _objSearch.OrderBy = " ORDER BY I.SearchName ASC";


                //_objSearch.WhereNdGroupBy = " WHERE PM.LedgerId =" + LedgerId;
                //_objSearch.OrderBy = " ORDER BY I.SearchName ASC";

            }
            using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.AllCharacters, this.Width - 90, (this.Height - 200), this.Location.Y + 70, this.Location.X + 100, dtColumnAlign))
            //            using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 90, (this.Height / 2), this.Location.Y + 70, this.Location.X + 100, dtColumnAlign))
            {
                var dialogResult = search.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    returnValue = Convert.ToString(search._retValue);
                }
            }
            if (returnValue != string.Empty)
            {
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
                    //dgvItemDet.Rows.Insert(CurrentIndex, 1);


                    if (!string.IsNullOrWhiteSpace(Convert.ToString(dgvItemDet.CurrentRow.Cells[iPDocNo].Value)))
                    {
                        PDetailId = Convert.ToInt64(returnValue.ToString());
                    }
                    else
                    {
                        PDetailId = Convert.ToInt64(returnValue.ToString());
                    }



                    dgvItemDet.Rows.Insert(CurrentIndex, 1);
                    //ItemId = Convert.ToInt64(returnValue.ToString());
                    dgvItemDet.Rows[CurrentIndex].Cells[iQuantity].Value = 1;
                    Itemdetails(CurrentIndex, PDetailId);
                    TotalSummary();
                    dgvItemDet.CurrentCell = dgvItemDet.Rows[CurrentIndex].Cells[iQuantity];
                    dgvItemDet.Rows[CurrentIndex + 1].Cells[iSlNo].Value = "0";


                }
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
            mtxtNoOfItems.Text = Convert.ToString(TotalNumber.ToString("0.00"));
            mtxtTotAmount.Text = Convert.ToString(AmountSummary.ToString("0.00"));
            mtxtTotDisc.Text = Convert.ToString(DiscountSummary.ToString("0.00"));
            mtxtTotGrss.Text = Convert.ToString(GrossSummary.ToString("0.00"));
            mtxtTotTax.Text = Convert.ToString(TaxSummary.ToString("0.00"));
            mtxtTotCess.Text = Convert.ToString(CessSummary.ToString("0.00"));
            mtxtTot.Text = Convert.ToString(TotalAmtSummary.ToString("0.00"));
        }
        public void ItemTotal(int currowIndex, decimal editingValue, int columnIndex)
        {
            if (string.IsNullOrWhiteSpace(Convert.ToString(editingValue)))
            {
                editingValue = 0;
            }
            decimal amount;
            decimal grossAmount;
            decimal taxRate;
            decimal cessAmount;
            decimal netAmount;
            decimal cost;
            decimal margin;

            qty = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value));
            rate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iPurchaseRate].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iPurchaseRate].Value));
            taxper = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iTaxPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iTaxPer].Value));
            discper = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iDiscPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iDiscPer].Value));
            if (taxper > 5)
            {
                cessper = 1;
            }
            else
            {
                cessper = 0;
            }
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
                else if (Convert.ToString(mcmbTax.Text) == "TAX")
                {
                    taxRate = (grossAmount * taxper) / 100;
                    cessper = 0;
                    cessAmount = 0;
                }
                else
                {
                    taxRate = (grossAmount * taxper) / 100;
                    cessAmount = (grossAmount * cessper) / 100;
                }
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
                }
                else if (Convert.ToString(mcmbTax.Text) == "TAX")
                {
                    taxRate = (grossAmount / (100 + taxper)) * taxper;
                    cessper = 0;
                    cessAmount = 0;
                }
                else
                {
                    // taxRate = (grossAmount / (100 + taxper + cessper)) * taxper;
                    taxRate = grossAmount * (taxper / 100);
                    ///cessAmount = (grossAmount / (100 + taxper + cessper)) * cessper;
                    cessAmount = grossAmount * (cessper / 100);
                }
                netAmount = grossAmount + taxRate + cessAmount;
            }
            dgvItemDet.Rows[currowIndex].Cells[iTaxAmount].Value = Convert.ToString(taxRate.ToString("0.00"));
            dgvItemDet.Rows[currowIndex].Cells[iCessPer].Value = cessper;
            dgvItemDet.Rows[currowIndex].Cells[iCessAmount].Value = Convert.ToString(cessAmount.ToString("0.00"));
            dgvItemDet.Rows[currowIndex].Cells[iTotalAmount].Value = Convert.ToString(netAmount.ToString("0.00"));
            if (Convert.ToDecimal(dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value) > 0)
            {
                cost = Convert.ToDecimal(dgvItemDet.Rows[currowIndex].Cells[iTotalAmount].Value) / Convert.ToDecimal(dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value);
                dgvItemDet.Rows[currowIndex].Cells[iCost].Value = Convert.ToString(cost.ToString("0.00"));
                margin = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iMRP].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iMRP].Value)) - cost;
                dgvItemDet.Rows[currowIndex].Cells[iMargin].Value = Convert.ToString(margin.ToString("0.00"));
            }
            GridSummary();
        }
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
        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {

            try
            {
                var sendingCB = sender as DataGridViewComboBoxEditingControl;

                var currentcell = dgvItemDet.CurrentCellAddress;
                //   var sendingCB = sender as DataGridViewComboBoxEditingControl;
                int cmbSelectedIndex = sendingCB.SelectedIndex;
                int CurrentIndex = dgvItemDet.CurrentRow.Index;
                int curCel = dgvItemDet.CurrentCell.ColumnIndex;
                if (curCel == iTaxText && string.IsNullOrWhiteSpace(((ComboBox)sender).SelectedText))
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
                ///MetroMessageBox.Show(this, ex.Message, "Error", 150);
            }
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

        #region Save And Edit Functions
        public void InsertPurchaseReturnDet(long MasterId, decimal QuantitytoSubtract)
        {
            for (int i = 0; i < dgvItemDet.Rows.Count; i++)
            {
                string itId = Convert.ToString(dgvItemDet.Rows[i].Cells[iItemId].Value);
                if (itId != "")
                {
                    BllOutwardDetails BllOutwardDetails = new BllOutwardDetails();
                    DmnOutward DmnOutward = new DmnOutward();
                    DmnOutward.OutwardId = MasterId;
                    decimal cgst = Convert.ToDecimal(dgvItemDet.Rows[i].Cells[iTaxAmount].Value) / 2;
                    DmnOutward.PMDocumentNo = Convert.ToString(dgvItemDet.Rows[i].Cells[iPDocNo].EditedFormattedValue);
                    DmnOutward.ItemId = long.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iItemId].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iItemId].Value));
                    DmnOutward.ItemCode = Convert.ToString(dgvItemDet.Rows[i].Cells[iItemCode].Value);
                    DmnOutward.ItemBarCode = Convert.ToString(dgvItemDet.Rows[i].Cells[iBarcode].Value);
                    DmnOutward.UnitId = long.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iUnitId].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iUnitId].Value));
                    DmnOutward.Quantity = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value));
                    DmnOutward.FreeQuantity = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value));
                    DmnOutward.QuantitytoSubtract = Decimal.Parse(Convert.ToString(QuantitytoSubtract) == string.Empty ? "1" : Convert.ToString(QuantitytoSubtract));
                    DmnOutward.PurchaseRate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iPurchaseRate].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iPurchaseRate].Value));
                    DmnOutward.IRate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iRate].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iRate].Value));
                    DmnOutward.Amount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iAmount].Value));
                    DmnOutward.DiscountPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscPer].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscPer].Value));
                    DmnOutward.DiscountAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscAmount].Value));
                    DmnOutward.IGSTAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value));
                    DmnOutward.CGSTAmount = decimal.Parse(Convert.ToString(cgst) == string.Empty ? "0" : Convert.ToString(cgst));
                    DmnOutward.SGSTAmount = decimal.Parse(Convert.ToString(cgst) == string.Empty ? "0" : Convert.ToString(cgst));
                    DmnOutward.GrossAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iGrossAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iGrossAmount].Value));
                    DmnOutward.TaxPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxPer].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxPer].Value));
                    DmnOutward.TaxAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value));
                    DmnOutward.CessPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iCessPer].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iCessPer].Value));
                    DmnOutward.CessAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iCessAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iCessAmount].Value));
                    DmnOutward.NetAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTotalAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTotalAmount].Value));
                    DmnOutward.BranchId = BranchId;
                    DmnOutward.FinancialYearId = FinancialYearId;
                    DmnOutward.TaxType = long.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxType].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxType].Value));
                    DmnOutward.Cost = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iCost].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iCost].Value)); ;
                    DmnOutward.Margin = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iMargin].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iMargin].Value));
                    DmnOutward.MRP = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iMRP].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iMRP].Value));
                    //DmnOutward.TotalSaveAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iItemSaveAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iItemSaveAmount].Value));
                    DmnOutward.StockItem = int.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iStockItem].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iStockItem].Value));
                    DmnOutward.Status = 0;
                    DmnOutward.CreatedBy = UserId;

                    #region to find quantity to reduce/add

                    BllSearch<DmnGridFields> bllQunatitytoReduceSearch = new BllSearch<DmnGridFields>();

                    DmnSearch objQtySearch = new DmnSearch();
                    objQtySearch = new DmnSearch();
                    objQtySearch.Select = " ItemId as Id,case MainUnitId when '" + DmnOutward.UnitId + "' then 1 " +
                        "Else PacketQty end as Field1,StockItem as Field2";
                    objQtySearch.Table = " tblItem  ";
                    objQtySearch.WhereNdGroupBy = "where ItemId=" + DmnOutward.ItemId;
                    var qtySource = bllQunatitytoReduceSearch.GetSearchData("prdSearch", objQtySearch);
                    DmnOutward.QuantitytoSubtract = Convert.ToDecimal(qtySource[0].Field1);


                    #endregion
                    var DetlstData = BllOutwardDetails.AddData("prdInsertOutwardDetails", DmnOutward);
                }
            }
        }
        public void InsertMiscCharges(long MasterId)
        {
            foreach (ListViewItem l in mlvMisc.Items)
            {
                BllOutwardDetails BllOutwardDetails = new BllOutwardDetails();
                DmnOutward DmnOutward = new DmnOutward();
                DmnOutward.OutwardId = MasterId;
                DmnOutward.GeneralId = long.Parse(Convert.ToString(l.SubItems[3].Text) == string.Empty ? "0" : Convert.ToString(l.SubItems[3].Text));
                DmnOutward.MiscAmount = decimal.Parse(Convert.ToString(l.SubItems[2].Text) == string.Empty ? "0.00" : Convert.ToString(l.SubItems[2].Text));
                DmnOutward.CreatedBy = UserId;
                DmnOutward.Status = 0;
                var MislstData = BllOutwardDetails.AddPurchaseMiscCharges("prdInsertOutwardMiscCharges", DmnOutward);
            }
        }
        public void DeletePurchaseReturnDetails(long MasterId)
        {
            BllOutwardDetails BllOutwardDetails = new BllOutwardDetails();
            DmnOutward DmnOutward = new DmnOutward();
            DmnOutward.OutwardId = MasterId;
            var DellstData = BllOutwardDetails.DeleteByMasterId("prdDeleteOutwardGridDetails", DmnOutward);
        }
        public void DeleteMiscCharges(long MasterId)
        {
            BllOutwardDetails BllOutwardDetails = new BllOutwardDetails();
            DmnOutward DmnOutward = new DmnOutward();
            DmnOutward.OutwardId = MasterId;
            var DellstData = BllOutwardDetails.DeleteMiscCharges("prdDeleteOutwardMiscCharges", DmnOutward);
        }

        #endregion



        #region Events
        private void FrmOutward_Load(object sender, EventArgs e)
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
        private void FrmOutward_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
                if (e.KeyCode == Keys.F6)
                    mbtnSave_Click(sender, e);
                if (e.KeyCode == Keys.F3)
                    mbtnList_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    mbtnRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mtxtLedger_KeyUp(object sender, KeyEventArgs e)
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
        private void mtxtTotDiscPer_KeyUp(object sender, KeyEventArgs e)
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
        private void mtxtTotDiscAmnt_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
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
        private void mtxtTotDiscAmnt_KeyDown(object sender, KeyEventArgs e)
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
        private void mtxtAdjust_KeyUp(object sender, KeyEventArgs e)
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
        private void mtxtAdjust_KeyDown(object sender, KeyEventArgs e)
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
        private void mlvMisc_KeyUp(object sender, KeyEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mtxtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                clsGeneral.DecimalTextBox3digits(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mcmbTax_SelectedIndexChanged(object sender, EventArgs e)
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
        private void mtxtLedger_Enter(object sender, EventArgs e)
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
        private void mtxtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                clsGeneral.IntegerTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mtxtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                clsGeneral.NextConrolOnEnterPress(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mcmbTransNo_Click(object sender, EventArgs e)
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
        private void mcmbTransNo_KeyUp(object sender, KeyEventArgs e)
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
        private void mtxtRemarks_KeyUp(object sender, KeyEventArgs e)
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
        private void dgvItemDet_DataError(object sender, DataGridViewDataErrorEventArgs e)
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
        private void dgvItemDet_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    ItemPopup();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }
        private void dgvItemDet_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    int colIndex = dgvItemDet.CurrentCell.ColumnIndex;
                    int currentrow = dgvItemDet.CurrentCell.RowIndex - 1;

                    if (colIndex == iBarcode)
                    {
                        if (currentrow < 0)// added to avoid error if barcode is empty and enter pressed
                        {
                            currentrow = 0;
                        }
                        dgvItemDet.CurrentCell = dgvItemDet.Rows[currentrow].Cells[iQuantity]; //dgvItemDet.currentrow.Cells[iQuantity];
                        dgvItemDet.CurrentCell.Selected = true;
                        dgvItemDet.BeginEdit(true);
                    }
                    else if (colIndex == iPDocNo)
                    {
                        if (currentrow < 0)// added to avoid error if barcode is empty and enter pressed
                        {
                            currentrow = 0;
                        }
                        dgvItemDet.CurrentCell = dgvItemDet.Rows[currentrow].Cells[iBarcode];
                        dgvItemDet.CurrentCell.Selected = true;
                        dgvItemDet.BeginEdit(true);
                    }
                    else if (colIndex == iQuantity)
                    {
                        if (dgvItemDet.Rows.Count > currentrow)
                        {
                            //currentrow = dgvItemDet.CurrentCell.RowIndex;
                            //dgvItemDet.CurrentCell = dgvItemDet.Rows[currentrow].Cells[iPDocNo];
                            //dgvItemDet.CurrentCell.Selected = true;
                            //dgvItemDet.BeginEdit(true);
                        }
                    }
                    else if (colIndex == iUnit)//when enter is pressed from unit tyhen also need to focus to quantity
                    {
                        if (currentrow < 0)// added to avoid error if barcode is empty and enter pressed
                        {
                            currentrow = 0;
                        }
                        //dgvItemDet.CurrentCell = dgvItemDet.Rows[currentrow].Cells[iQuantity];
                        //dgvItemDet.CurrentCell.Selected = true;
                        //dgvItemDet.BeginEdit(true);
                    }

                }

                else if (e.KeyCode == Keys.F8)
                {
                    ItemPopup();
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
        private void dgvItemDet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                #region Decimal TextBox
                if (e.Control is TextBox)
                {
                    TextBox curtxtbox = e.Control as TextBox;
                    curtxtbox.KeyPress += new KeyPressEventHandler(clsGeneral.DecimalGridviewTextBox);


                    DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
                    tb.KeyUp -= new KeyEventHandler(dgvItemDet_KeyEventHandler);
                    tb.KeyUp += new KeyEventHandler(dgvItemDet_KeyEventHandler);
                    tb.PreviewKeyDown -= new PreviewKeyDownEventHandler(dgvItemDet_PreviewKeyDown);
                    tb.PreviewKeyDown += new PreviewKeyDownEventHandler(dgvItemDet_PreviewKeyDown);
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

                e.Control.PreviewKeyDown -= new PreviewKeyDownEventHandler(dgvItemDet_PreviewKeyDown);
                e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(dgvItemDet_PreviewKeyDown);
                e.Control.KeyUp -= new KeyEventHandler(dgvItemDet_KeyEventHandler);
                e.Control.KeyUp += new KeyEventHandler(dgvItemDet_KeyEventHandler);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void dgvItemDet_CurrentCellDirtyStateChanged(object sender, EventArgs e)
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
        private void dgvItemDet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvItemDet.CurrentCell.ColumnIndex == iBarcode || dgvItemDet.CurrentCell.ColumnIndex == iName)
                    ItemPopup();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void dgvItemDet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int currentRow = dgvItemDet.CurrentCell.RowIndex;
                int currentColumn = dgvItemDet.CurrentCell.ColumnIndex;
                if (currentColumn == iSlNo)
                {
                    dgvItemDet.Rows[currentRow].Selected = true;
                }
                if (currentColumn == iPDocNo)
                {
                    FillPDocNoCombo();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void dgvItemDet_KeyEventHandler(object sender, KeyEventArgs e)
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

                            rate = PurchaseRatemrp;
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
                                _objSearch.ColumnHeaders = "Id,Code,Barcode,Name,Category,Unit,PurchaseRate,PurchaseRate,MRP,Tax(%),QOH";
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

                            decimal SalesRate = (PurchaseRate * (100 + SellingPer)) / 100;
                            dgvItemDet.Rows[currow].Cells[iPurchaseRate].Value = PurchaseRate.ToString("0.00");
                            MetroMessageBox.Show(this, "Sales Rate must be less than or equal to MRP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                        else
                        {
                            dgvItemDet.Rows[currow].Cells[iPurchaseRate].Value = salsrate.ToString("0.00");
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

                            decimal SalesRate = (PurchaseRate * (100 + SellingPer)) / 100;
                            dgvItemDet.Rows[currow].Cells[iPurchaseRate].Value = PurchaseRate.ToString("0.00");
                            MetroMessageBox.Show(this, "Sales Rate must be less than or equal to MRP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                        else
                        {
                            dgvItemDet.Rows[currow].Cells[iPurchaseRate].Value = salsrate.ToString("0.00");
                        }
                    }
                    if (colndex == iPurchaseRate)
                    {
                        decimal SalesRate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iSalesRate].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iPurchaseRate].Value));
                        decimal PurchaseRate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iPurchaseRate].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iPurchaseRate].Value));
                        decimal SellingPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iItemSelPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iItemSelPer].Value));
                        decimal mrp = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currow].Cells[iMRP].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currow].Cells[iMRP].Value));
                        if (mrp < PurchaseRate)
                        {
                            decimal salsrate = (PurchaseRate * (100 + SellingPer)) / 100;
                            dgvItemDet.Rows[currow].Cells[iSellingPer].Value = dgvItemDet.Rows[currow].Cells[iItemSelPer].Value;
                            dgvItemDet.Rows[currow].Cells[iPurchaseRate].Value = Convert.ToDecimal(salsrate.ToString("0.00"));
                            MetroMessageBox.Show(this, "Sales Rate must be less than or equal to MRP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                        }
                        else
                        {
                            SellingPer = ((100 * PurchaseRate) / PurchaseRate) - 100;
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
        private void mbtnLedgerAdd_Click(object sender, EventArgs e)
        {
            try
            {
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


        private void btnTransAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (LedgerCode != string.Empty && (Convert.ToString(mtxtTransportNoAdd.Text) != string.Empty))
                {
                    DmnGeneralOptions dmnGen = new DmnGeneralOptions();
                    BllGeneralOptions bllGen = new BllGeneralOptions();
                    dmnGen.GTypeId = Convert.ToInt32(10033);
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


       

        private void btnClose_Click(object sender, EventArgs e)
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
        private void mbtnAdd_Click(object sender, EventArgs e)
        {
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
        private void mbtnRefresh_Click(object sender, EventArgs e)
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
        private void mbtnSave_Click(object sender, EventArgs e)
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
                            DialogResult results = MetroMessageBox.Show(this, "", "Save Entry No.: " + mtxtInvNo.Text.ToString() + " ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (results == DialogResult.Yes)
                            {
                                long savestatus;
                                BllOutward BllOutward = new BllOutward();
                                DmnOutward DmnOutward = new DmnOutward();
                                string cgst = Convert.ToString(decimal.Parse(mtxtTotTax.Text) / 2);
                                DmnOutward.EType = Convert.ToInt32(mcmbTax.SelectedValue);
                                DmnOutward.EntryNo = Convert.ToString(mtxtInvNo.Text);
                                DmnOutward.EntryDate = Convert.ToDateTime(mdtpInvDate.Text);
                                DmnOutward.DocumentNo = Convert.ToString(mtxtDocNo.Text);
                                DmnOutward.LedgerId = LedgerId;
                                DmnOutward.SalesAddress = Convert.ToString(mtxtAddress.Text);
                                DmnOutward.PaymentMode = Convert.ToInt32(mcmbMode.SelectedValue);
                                DmnOutward.Telephone = Convert.ToString(mtxtMobile.Text);
                                DmnOutward.Remarks = Convert.ToString(mtxtRemarks.Text);
                                DmnOutward.TotalGross = decimal.Parse(mtxtTotGrss.Text == string.Empty ? "0.00" : mtxtTotGrss.Text);
                                DmnOutward.TotalAmount = decimal.Parse(mtxtTotAmount.Text == string.Empty ? "0.00" : mtxtTotAmount.Text);
                                DmnOutward.TotalDiscount = decimal.Parse(mtxtTotDiscAmnt.Text == string.Empty ? "0.00" : mtxtTotDiscAmnt.Text);
                                DmnOutward.TotalIGST = decimal.Parse(mtxtTotTax.Text == string.Empty ? "0.00" : mtxtTotTax.Text);
                                DmnOutward.TotalCGST = decimal.Parse(cgst == string.Empty ? "0.00" : cgst);
                                DmnOutward.TotalSGST = decimal.Parse(cgst == string.Empty ? "0.00" : cgst);
                                DmnOutward.TotalTax = decimal.Parse(mtxtTotTax.Text == string.Empty ? "0.00" : mtxtTotTax.Text);
                                DmnOutward.TotalMiscCharges = decimal.Parse(mtxtMiscCharges.Text == string.Empty ? "0.00" : mtxtMiscCharges.Text);
                                DmnOutward.TotalCess = decimal.Parse(mtxtTotCess.Text == string.Empty ? "0.00" : mtxtTotCess.Text);
                                DmnOutward.DiscountPer = decimal.Parse(mtxtTotDiscPer.Text == string.Empty ? "0.00" : mtxtTotDiscPer.Text);
                                DmnOutward.DiscountAmount = decimal.Parse(mtxtTotDiscAmnt.Text == string.Empty ? "0.00" : mtxtTotDiscAmnt.Text);
                                //DmnOutward.OtherAdditions = decimal.Parse(mtxtAdjust.Text == string.Empty ? "0.00" : mtxtAdjust.Text);
                                DmnOutward.NetAmount = decimal.Parse(mtxtTot.Text == string.Empty ? "0.00" : mtxtNetAmount.Text);
                                //DmnOutward.PaidAmount = decimal.Parse(mtxtNetAmount.Text == string.Empty ? "0.00" : mtxtNetAmount.Text);
                                //DmnOutward.BalanceAmount = decimal.Parse(mtxtNetAmount.Text == string.Empty ? "0.00" : mtxtNetAmount.Text);
                                DmnOutward.AmountAdjust = decimal.Parse(mtxtAdjust.Text == string.Empty ? "0.00" : mtxtAdjust.Text);
                                DmnOutward.TransportNo = (Convert.ToString(mcmbTransNo.SelectedValue) == string.Empty ? "0" : Convert.ToString(mcmbTransNo.SelectedValue));
                                DmnOutward.CreditDays = int.Parse(mtxtCreditDays.Text == string.Empty ? "0" : mtxtCreditDays.Text);
                                DmnOutward.DueDate = Convert.ToDateTime(mdtpDueDate.Text);
                                DmnOutward.DocumentType = "";
                                DmnOutward.BranchId = BranchId;
                                DmnOutward.FinancialYearId = FinancialYearId;
                                DmnOutward.TotalQuantity = Decimal.ToInt32(Convert.ToDecimal(mtxtNoOfItems.Text));
                                DmnOutward.DocumentDate = Convert.ToDateTime(mdtpDocDate.Text);
                                DmnOutward.TotalNetAmount = decimal.Parse(mtxtNetAmount.Text == string.Empty ? "0.00" : mtxtNetAmount.Text);
                                DmnOutward.Status = 0;
                                DmnOutward.toid = Convert.ToInt32(mcmbFrom.SelectedValue);

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
                                    DialogResult result = MetroMessageBox.Show(this, "\nUpdate the Contact details ?", "Update ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150);
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
                                if (OutwardId == 0)
                                {
                                    DmnOutward.CreatedBy = UserId;
                                    var lstData = BllOutward.AddData("prdInsertOutward", DmnOutward);
                                    savestatus = lstData.OutwardId;
                                    if (lstData.OutwardId != 0)
                                    {
                                        MasterId = lstData.OutwardId;
                                        InsertPurchaseReturnDet(MasterId, 1);
                                        InsertMiscCharges(MasterId);
                                        //UpdateBranchPurchaseSrlNo(MasterId);
                                        //MessageBox.Show(" " + MasterId);
                                    }
                                    if (savestatus != 0)
                                    {
                                        ////#region Print bill
                                        //try
                                        //{
                                        //    MasterId = lstData.OutwardId;
                                        //    ReportDocument reportDocument = new ReportDocument();
                                        //    DataSet dsPurchaseReturn = new DataSet();
                                        //    DataTable dtPurchaseReturnHeader = new DataTable("dtPurchaseReturnHeader");
                                        //    DataTable dtPurchaseReturnMaster = new DataTable("dtPurchaseReturnMaster");
                                        //    DataTable dtPurchaseReturndet = new DataTable("dtPurchaseReturndet");
                                        //    DataTable dtPurchaseReturnTax = new DataTable("dtPurchaseReturnTax");

                                        //    BllSearch<DmnOutwardInvoiceBill> bllSearchPurchaseReturnInvoiceBill = new BllSearch<DmnOutwardInvoiceBill>();

                                        //    DmnSearch dmnSearchInvoiceHeaders = new DmnSearch();
                                        //    dmnSearchInvoiceHeaders.Select = " B.BranchName,B.BranchAddress,B.Email,B.Telephone,B.GSTNo,C.CompanyName";
                                        //    dmnSearchInvoiceHeaders.Table = " tblBranch as B " +
                                        //                                    " Left join tblCompany as c on C.CompanyId = B.CompanyId";
                                        //    dmnSearchInvoiceHeaders.WhereNdGroupBy = " where B.BranchId=" + BranchId;
                                        //    var invoicePurchaseReturnHeaders = bllSearchPurchaseReturnInvoiceBill.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);
                                        //    using (var reader = ObjectReader.Create(invoicePurchaseReturnHeaders, "BranchName", "BranchAddress", "Email", "Telephone", "CompanyGSTNo", "CompanyName"))
                                        //    {
                                        //        dtPurchaseReturnHeader.Load(reader);
                                        //    }

                                        //    DmnSearch dmnSearchInvoiceBill = new DmnSearch();
                                        //    dmnSearchInvoiceBill.Select = " S.EntryNo,S.EntryDate,A.LedgerName,(Select Username from tblUserAccount where UserAccountId = '" + UserId + "') as Username, " +
                                        //                                  " S.TotalQuantity,S.TotalAmount,S.TotalDiscount,S.TotalTax,S.TotalCGST,S.TotalSGST," +
                                        //                                  " S.NetAmount as MasterNetAmount,S.TotalCess,g.Caption as PaymentMode, " +
                                        //                                  "A.LedgerAddress as Address,A.Mobile as Phone, A.GSTNo,g1.GTypeSubText2 as TransportationNo,S.NetAmount as AmountInWords," +
                                        //                                  "S.TotalTax as TaxAmountInWords";
                                        //    dmnSearchInvoiceBill.Table = " tblOutward as S " +
                                        //                                    " left join tblGeneralOptions g ON S.PaymentMode = g.GeneralId " +
                                        //                                    " left join tblGeneralOptions g1 ON S.TransportNo = g1.GeneralId " +
                                        //                                    " left join tblAccountLedger as A ON S.LedgerId = A.LedgerId ";
                                        //    dmnSearchInvoiceBill.WhereNdGroupBy = " where S.OutwardId ='" + MasterId + "'";
                                        //    var invoicePurchaseReturnMasters = bllSearchPurchaseReturnInvoiceBill.GetSearchData("prdSearch", dmnSearchInvoiceBill);

                                        //    string input_decimal_number = Convert.ToString(invoicePurchaseReturnMasters[0].MasterNetAmount);
                                        //    string decimal_places = "0";
                                        //    var regex = new System.Text.RegularExpressions.Regex("(?<=[\\.])[0-9]+");
                                        //    if (regex.IsMatch(input_decimal_number))
                                        //    {
                                        //        decimal_places = regex.Match(input_decimal_number).Value;
                                        //    }
                                        //    string AmountinWords = "0";

                                        //    int num = Decimal.ToInt32(invoicePurchaseReturnMasters[0].MasterNetAmount);
                                        //    int dec = Convert.ToInt32(decimal_places);
                                        //    if (dec > 0)
                                        //    {
                                        //        AmountinWords = Convert.ToString(clsGeneral.NumberToWords(num) + " And " + clsGeneral.NumberToWords(dec) + " Paise Only");
                                        //    }
                                        //    else
                                        //        AmountinWords = Convert.ToString(clsGeneral.NumberToWords(num) + " Only");

                                        //    //Tax
                                        //    string decimal_placesTax = "0";
                                        //    string decnum = Convert.ToString(invoicePurchaseReturnMasters[0].TaxAmountInWords);
                                        //    string decplaces = "0";
                                        //    var regexs = new System.Text.RegularExpressions.Regex("(?<=[\\.])[0-9]+");
                                        //    if (regexs.IsMatch(decnum))
                                        //    {
                                        //        decimal_placesTax = regexs.Match(decnum).Value;
                                        //    }
                                        //    string TaxAmountInWords = "0";

                                        //    int numTax = Decimal.ToInt32(invoicePurchaseReturnMasters[0].TotalTax);
                                        //    int decTax = Convert.ToInt32(decimal_placesTax);
                                        //    if (decTax > 0)
                                        //    {
                                        //        TaxAmountInWords = Convert.ToString(clsGeneral.NumberToWords(num) + " And " + clsGeneral.NumberToWords(decTax) + " Paise Only");
                                        //    }
                                        //    else
                                        //        TaxAmountInWords = Convert.ToString(clsGeneral.NumberToWords(numTax) + " Only");

                                        //    invoicePurchaseReturnMasters[0].AmountInWords = Convert.ToString(AmountinWords);
                                        //    invoicePurchaseReturnMasters[0].TaxAmountInWords = Convert.ToString(TaxAmountInWords);
                                        //    using (var reader = ObjectReader.Create(invoicePurchaseReturnMasters, "EntryNo", "EntryDate", "LedgerName", "Username", "TotalQuantity", "TotalAmount",
                                        //        "TotalDiscount", "TotalTax", "TotalCGST", "TotalSGST", "MasterNetAmount", "TotalCess", "PaymentMode", "Address", "Phone", "GSTNo",
                                        //        "TransportationNo", "AmountInWords", "TaxAmountInWords"))
                                        //    {
                                        //        dtPurchaseReturnMaster.Load(reader);
                                        //    }

                                        //    DmnSearch dmnSearchInvoiceDetails = new DmnSearch();
                                        //    dmnSearchInvoiceDetails.Select = "S.InwDetailId as SrlNo, I.SearchName,S.Quantity,S.MRP,S.PurchaseRate,G.GTypeSubValue1 as Caption ,S.NetAmount," +
                                        //        "I.HSNCODE as HSNCode,U.UnitName,S.DiscountAmount,S.Amount";
                                        //    dmnSearchInvoiceDetails.Table = " tblOutDetails as S Left JOIN tblUnit U on S.UnitId=U.Unitid" +
                                        //                                      " Left join tblItem as I on I.ItemId = S.ItemId" +
                                        //                                    " Left join tblGeneralOptions as G on S.TaxType = G.GeneralId ";
                                        //    dmnSearchInvoiceDetails.WhereNdGroupBy = " where S.OutwardId=" + MasterId;
                                        //    var invoicePurchaseReturnDetails = bllSearchPurchaseReturnInvoiceBill.GetSearchData("prdSearch", dmnSearchInvoiceDetails);
                                        //    if (invoicePurchaseReturnDetails.Count > 0)
                                        //    {
                                        //        for (int i = 0; i < invoicePurchaseReturnDetails.Count; i++)
                                        //        {
                                        //            invoicePurchaseReturnDetails[i].SrlNo = i + 1;
                                        //        }
                                        //    }
                                        //    using (var reader = ObjectReader.Create(invoicePurchaseReturnDetails, "SrlNo", "SearchName", "Quantity", "MRP", "PurchaseRate", "Caption",
                                        //        "NetAmount", "HSNCode", "UnitName", "DiscountAmount", "Amount"))
                                        //    {
                                        //        dtPurchaseReturndet.Load(reader);
                                        //    }

                                        //    DmnSearch dmnSearchTaxDetails = new DmnSearch();
                                        //    dmnSearchTaxDetails.Select = " DISTINCT P.HSNCode, " +
                                        //    "(select sum(GrossAmount) from tblOutDetails Where OutwardId = " + MasterId + " AND HSNCode = P.HSNCode) as TotalTax," +
                                        //    "((P.TaxPer) / 2) CGSTPer,P.CGSTAmount, ((P.TaxPer) / 2) SGSTPer, P.SGSTAmount,(P.CGSTAmount + P.SGSTAmount) as TaxAmount";
                                        //    dmnSearchTaxDetails.Table = " tblOutDetails P ";
                                        //    dmnSearchTaxDetails.WhereNdGroupBy = " where P.OutwardId=" + MasterId + " Order By HSNCode ASC ";
                                        //    var invoicePurchaseReturnTaxDetails = bllSearchPurchaseReturnInvoiceBill.GetSearchData("prdSearch", dmnSearchTaxDetails);
                                        //    invoicePurchaseReturnTaxDetails[0].TaxAmountInWords = Convert.ToString(TaxAmountInWords);
                                        //    using (var reader = ObjectReader.Create(invoicePurchaseReturnTaxDetails, "HSNCode", "TotalTax", "CGSTPer", "CGSTAmt", "SGSTPer",
                                        //        "SGSTAmt", "TaxAmount", "TaxAmountInWords"))
                                        //    {
                                        //        dtPurchaseReturnTax.Load(reader);
                                        //    }
                                        //    dsPurchaseReturn.Tables.Add(dtPurchaseReturnHeader);
                                        //    dsPurchaseReturn.Tables.Add(dtPurchaseReturnMaster);
                                        //    dsPurchaseReturn.Tables.Add(dtPurchaseReturndet);
                                        //    dsPurchaseReturn.Tables.Add(dtPurchaseReturnTax);

                                        //    //reportDocument.Load(Application.StartupPath + "\\Reports\\crptPurchaseReturnA4.rpt");
                                        //    //reportDocument.Load(@"C:\Lal\MarginPOS New\MarginPOS New\MarginPOSNew\Reports\crptPurchaseReturnA4.rpt");
                                        //    reportDocument.Load(@"C:\Sophia\New\MarginPOSNew\Reports\crptPurchaseReturnA4.rpt");
                                        //    reportDocument.SetDataSource(dsPurchaseReturn);
                                        //    //crvPurchaseReturnBill.Visible = true;
                                        //    //crvPurchaseReturnBill.ReportSource = reportDocument;
                                        //    int allowdedFormat = (int)(CrystalDecisions.Shared.ViewerExportFormats.PdfFormat);
                                        //    crvPurchaseReturnBill.AllowedExportFormats = allowdedFormat;
                                        //    crvPurchaseReturnBill.ReportSource = reportDocument;
                                        //    crvPurchaseReturnBill.ReuseParameterValuesOnRefresh = true;
                                        //    crvPurchaseReturnBill.RefreshReport();
                                        //    crvPurchaseReturnBill.PrintMode = CrystalDecisions.Windows.Forms.PrintMode.PrintToPrinter;
                                        //    crvPurchaseReturnBill.PrintReport();
                                        //}
                                        //catch (Exception ex)
                                        //{
                                        //    MessageBox.Show(ex.Message);
                                        //}
                                        //#endregion
                                        MetroMessageBox.Show(this, "", "Successfully saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                                        RefreshForm();
                                    }
                                    else
                                        MetroMessageBox.Show(this, "Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                                }
                                else
                                {
                                    bool status;
                                    MasterId = OutwardId;
                                    DmnOutward.ModifiedBy = UserId;
                                    DmnOutward.ModifiedDateTime = DateTime.Now;
                                    DmnOutward.OutwardId = OutwardId;
                                    var lstData = BllOutward.UpdateData("prdUpdateOutward", DmnOutward);

                                    BllSearch<DmnOutward> bllSearch = new BllSearch<DmnOutward>();
                                    DmnSearch objSearch = new DmnSearch();
                                    objSearch.Select = "P.ItemId,P.UnitId,P.OutwardDetailsId ";
                                    objSearch.Table = " tblOutDetails P  ";
                                    objSearch.WhereNdGroupBy = "Where P.OutwardId = '" + OutwardId + "'";
                                    objSearch.OrderBy = "Order By P.OutwardDetailsId ";
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
                                            DmnOutward.OutwardDetailsId = det[0].OutwardDetailsId;
                                            DmnOutward.ItemId = det[0].ItemId;

                                            #region to find quantity to reduce/add

                                            BllSearch<DmnGridFields> bllQunatitytoReduceSearch = new BllSearch<DmnGridFields>();

                                            DmnSearch objQtySearch = new DmnSearch();
                                            objQtySearch = new DmnSearch();
                                            objQtySearch.Select = " ItemId as Id,case MainUnitId when '" + det[i].UnitId + "' then 1 " +
                                                "Else PacketQty end as Field1,StockItem as Field2";
                                            objQtySearch.Table = " tblItem  ";
                                            objQtySearch.WhereNdGroupBy = "where ItemId=" + det[i].ItemId;
                                            var qtySource = bllQunatitytoReduceSearch.GetSearchData("prdSearch", objQtySearch);
                                            DmnOutward.QuantitytoSubtract = Convert.ToDecimal(qtySource[0].Field1);

                                            #endregion
                                            if (Convert.ToInt32(qtySource[0].Field2) == 1)//update qoh only if it is a stock item
                                            {
                                                var UpdateQohLst = BllOutward.UpdateQoh("prdUpdateQohAfterPurchaseReturn", DmnOutward);
                                            }
                                        }
                                    }
                                    DeletePurchaseReturnDetails(MasterId);
                                    InsertPurchaseReturnDet(MasterId, DmnOutward.QuantitytoSubtract);
                                    DeleteMiscCharges(MasterId);
                                    InsertMiscCharges(MasterId);
                                    DmnOutward.ModifiedBy = UserId;
                                    DmnOutward.ModifiedDateTime = DateTime.Now;
                                    DmnOutward.OutwardId = OutwardId;
                                    var lstDataUpdate = BllOutward.UpdateData("prdUpdateOutward", DmnOutward); status = lstData;
                                    status = lstDataUpdate;
                                    //#region Print bill
                                    //                                    try
                                    //                                    {
                                    //                                        ReportDocument reportDocument = new ReportDocument();
                                    //                                        DataSet dsPurchaseReturn = new DataSet();
                                    //                                        DataTable dtPurchaseReturnHeader = new DataTable("dtPurchaseReturnHeader");
                                    //                                        DataTable dtPurchaseReturnMaster = new DataTable("dtPurchaseReturnMaster");
                                    //                                        DataTable dtPurchaseReturndet = new DataTable("dtPurchaseReturndet");
                                    //                                        DataTable dtPurchaseReturnTax = new DataTable("dtPurchaseReturnTax");

                                    //                                        BllSearch<DmnOutwardInvoiceBill> bllSearchPurchaseReturnInvoiceBill = new BllSearch<DmnOutwardInvoiceBill>();

                                    //                                        DmnSearch dmnSearchInvoiceHeaders = new DmnSearch();
                                    //                                        dmnSearchInvoiceHeaders.Select = " B.BranchName,B.BranchAddress,B.Email,B.Telephone,B.GSTNo,C.CompanyName";
                                    //                                        dmnSearchInvoiceHeaders.Table = " tblBranch as B " +
                                    //                                                                        " Left join tblCompany as c on C.CompanyId = B.CompanyId";
                                    //                                        dmnSearchInvoiceHeaders.WhereNdGroupBy = " where B.BranchId=" + BranchId;
                                    //                                        var invoicePurchaseReturnHeaders = bllSearchPurchaseReturnInvoiceBill.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);
                                    //                                        using (var reader = ObjectReader.Create(invoicePurchaseReturnHeaders, "BranchName", "BranchAddress", "Email", "Telephone", "CompanyGSTNo", "CompanyName"))
                                    //                                        {
                                    //                                            dtPurchaseReturnHeader.Load(reader);
                                    //                                        }

                                    //                                        DmnSearch dmnSearchInvoiceBill = new DmnSearch();
                                    //                                        dmnSearchInvoiceBill.Select = " S.EntryNo,S.EntryDate,A.LedgerName,(Select Username from tblUserAccount where UserAccountId = '" + UserId + "') as Username, " +
                                    //                                                                      " S.TotalQuantity,S.TotalAmount,S.TotalDiscount,S.TotalTax,S.TotalCGST,S.TotalSGST," +
                                    //                                                                      " S.NetAmount as MasterNetAmount,S.TotalCess,g.Caption as PaymentMode, " +
                                    //                                                                      "A.LedgerAddress as Address,A.Mobile as Phone, A.GSTNo,g1.GTypeSubText2 as TransportationNo,S.NetAmount as AmountInWords," +
                                    //                                                                      "S.TotalTax as TaxAmountInWords";
                                    //                                        dmnSearchInvoiceBill.Table = " tblOutward as S " +
                                    //                                                                        " left join tblGeneralOptions g ON S.PaymentMode = g.GeneralId " +
                                    //                                                                        " left join tblGeneralOptions g1 ON S.TransportNo = g1.GeneralId " +
                                    //                                                                        " left join tblAccountLedger as A ON S.LedgerId = A.LedgerId ";
                                    //                                        dmnSearchInvoiceBill.WhereNdGroupBy = " where S.OutwardId ='" + MasterId + "'";
                                    //                                        var invoicePurchaseReturnMasters = bllSearchPurchaseReturnInvoiceBill.GetSearchData("prdSearch", dmnSearchInvoiceBill);

                                    //                                        string input_decimal_number = Convert.ToString(invoicePurchaseReturnMasters[0].MasterNetAmount);
                                    //                                        string decimal_places = "0";
                                    //                                        var regex = new System.Text.RegularExpressions.Regex("(?<=[\\.])[0-9]+");
                                    //                                        if (regex.IsMatch(input_decimal_number))
                                    //                                        {
                                    //                                            decimal_places = regex.Match(input_decimal_number).Value;
                                    //                                        }
                                    //                                        string AmountinWords = "0";

                                    //                                        int num = Decimal.ToInt32(invoicePurchaseReturnMasters[0].MasterNetAmount);
                                    //                                        int dec = Convert.ToInt32(decimal_places);
                                    //                                        if (dec > 0)
                                    //                                        {
                                    //                                            AmountinWords = Convert.ToString(clsGeneral.NumberToWords(num) + " And " + clsGeneral.NumberToWords(dec) + " Paise Only");
                                    //                                        }
                                    //                                        else
                                    //                                            AmountinWords = Convert.ToString(clsGeneral.NumberToWords(num) + " Only");

                                    //                                        //Tax
                                    //                                        string decimal_placesTax = "0";
                                    //                                        string decnum = Convert.ToString(invoicePurchaseReturnMasters[0].TaxAmountInWords);
                                    //                                        string decplaces = "0";
                                    //                                        var regexs = new System.Text.RegularExpressions.Regex("(?<=[\\.])[0-9]+");
                                    //                                        if (regexs.IsMatch(decnum))
                                    //                                        {
                                    //                                            decimal_placesTax = regexs.Match(decnum).Value;
                                    //                                        }
                                    //                                        string TaxAmountInWords = "0";

                                    //                                        int numTax = Decimal.ToInt32(invoicePurchaseReturnMasters[0].TotalTax);
                                    //                                        int decTax = Convert.ToInt32(decimal_placesTax);
                                    //                                        if (decTax > 0)
                                    //                                        {
                                    //                                            TaxAmountInWords = Convert.ToString(clsGeneral.NumberToWords(num) + " And " + clsGeneral.NumberToWords(decTax) + " Paise Only");
                                    //                                        }
                                    //                                        else
                                    //                                            TaxAmountInWords = Convert.ToString(clsGeneral.NumberToWords(numTax) + " Only");

                                    //                                        invoicePurchaseReturnMasters[0].AmountInWords = Convert.ToString(AmountinWords);
                                    //                                        invoicePurchaseReturnMasters[0].TaxAmountInWords = Convert.ToString(TaxAmountInWords);
                                    //                                        using (var reader = ObjectReader.Create(invoicePurchaseReturnMasters, "EntryNo", "EntryDate", "LedgerName", "Username", "TotalQuantity", "TotalAmount",
                                    //                                            "TotalDiscount", "TotalTax", "TotalCGST", "TotalSGST", "MasterNetAmount",  "TotalCess", "PaymentMode", "Address", "Phone", "GSTNo",
                                    //                                            "TransportationNo", "AmountInWords", "TaxAmountInWords"))
                                    //                                        {
                                    //                                            dtPurchaseReturnMaster.Load(reader);
                                    //                                        }

                                    //                                        DmnSearch dmnSearchInvoiceDetails = new DmnSearch();
                                    //                                        dmnSearchInvoiceDetails.Select = "S.InwDetailId as SrlNo, I.SearchName,S.Quantity,S.MRP,S.PurchaseRate,G.GTypeSubValue1 as Caption ,S.NetAmount," +
                                    //                                            "I.HSNCODE as HSNCode,U.UnitName,S.DiscountAmount,S.Amount";
                                    //                                        dmnSearchInvoiceDetails.Table = " tblOutDetails as S Left JOIN tblUnit U on S.UnitId=U.Unitid" +
                                    //                                                                          " left join tblItem as I on I.ItemId = S.ItemId" +
                                    //                                                                        " left join tblGeneralOptions as G on S.TaxType = G.GeneralId ";
                                    //                                        dmnSearchInvoiceDetails.WhereNdGroupBy = " where S.OutwardId=" + MasterId;
                                    //                                        var invoicePurchaseReturnDetails = bllSearchPurchaseReturnInvoiceBill.GetSearchData("prdSearch", dmnSearchInvoiceDetails);
                                    //                                        if (invoicePurchaseReturnDetails.Count > 0)
                                    //                                        {
                                    //                                            for (int i = 0; i < invoicePurchaseReturnDetails.Count; i++)
                                    //                                            {
                                    //                                                invoicePurchaseReturnDetails[i].SrlNo = i+1;
                                    //                                            }
                                    //                                        }
                                    //                                        using (var reader = ObjectReader.Create(invoicePurchaseReturnDetails, "SrlNo", "SearchName", "Quantity", "MRP", "PurchaseRate", "Caption",
                                    //                                            "NetAmount", "HSNCode", "UnitName", "DiscountAmount", "Amount"))
                                    //                                        {
                                    //                                            dtPurchaseReturndet.Load(reader);
                                    //                                        }

                                    //                                        DmnSearch dmnSearchTaxDetails = new DmnSearch();
                                    //                                        dmnSearchTaxDetails.Select = " DISTINCT P.HSNCode,"
                                    //                                                                  + "(select sum(P1.GrossAmount) from tblOutDetails P1 Where P1.OutwardId = " + MasterId + " AND P1.HSNCode = P.HSNCode) as TotalTax,"
                                    //                                                                  + "((P.TaxPer) / 2) as CGSTPer,"
                                    //                                                                  + "(select sum(P2.CGSTAmount) from tblOutDetails P2 Where P2.OutwardId = " + MasterId + " AND P2.HSNCode = P.HSNCode) as CGSTAmt,"
                                    //                                                                  + "((P.TaxPer) / 2) as SGSTPer,"
                                    //                                                                  + "(select sum(P3.SGSTAmount) from tblOutDetails P3 Where P3.OutwardId = " + MasterId + " AND P3.HSNCode = P.HSNCode) as SGSTAmt,"
                                    //                                                                  + "(select(sum(CGSTAmount + SGSTAmount)) from tblOutDetails Where OutwardId = " + MasterId + " AND HSNCode = P.HSNCode) as TaxAmount";
                                    //                                        dmnSearchTaxDetails.Table = " tblOutDetails P ";
                                    //                                        dmnSearchTaxDetails.WhereNdGroupBy = " where P.OutwardId=" + MasterId + " Order By HSNCode ASC ";
                                    //                                        var invoicePurchaseReturnTaxDetails = bllSearchPurchaseReturnInvoiceBill.GetSearchData("prdSearch", dmnSearchTaxDetails);
                                    //                                        invoicePurchaseReturnTaxDetails[0].TaxAmountInWords = Convert.ToString(TaxAmountInWords);
                                    //                                        using (var reader = ObjectReader.Create(invoicePurchaseReturnTaxDetails, "HSNCode", "TotalTax", "CGSTPer", "CGSTAmt", "SGSTPer",
                                    //                                            "SGSTAmt", "TaxAmount", "TaxAmountInWords"))
                                    //                                        {
                                    //                                            dtPurchaseReturnTax.Load(reader);
                                    //                                        }
                                    //                                        dsPurchaseReturn.Tables.Add(dtPurchaseReturnHeader);
                                    //                                        dsPurchaseReturn.Tables.Add(dtPurchaseReturnMaster);
                                    //                                        dsPurchaseReturn.Tables.Add(dtPurchaseReturndet);
                                    //                                        dsPurchaseReturn.Tables.Add(dtPurchaseReturnTax);
                                    //                                        //reportDocument.Load(Application.StartupPath + "\\Reports\\crptPurchaseReturnA4.rpt");
                                    //                                        //reportDocument.Load(@"C:\Lal\MarginPOS New\MarginPOS New\MarginPOSNew\Reports\crptPurchaseReturnA4.rpt");
                                    //                                        reportDocument.Load(@"C:\Sophia\MarginPOS\MarginPOSNew\Reports\crptPurchaseReturnA4.rpt");
                                    //                                        reportDocument.SetDataSource(dsPurchaseReturn);
                                    //                                        //crvPurchaseReturnBill.Visible = true;
                                    //                                        //crvPurchaseReturnBill.ReportSource = reportDocument;
                                    //                                        int allowdedFormat = (int)(CrystalDecisions.Shared.ViewerExportFormats.PdfFormat);
                                    //                                        crvPurchaseReturnBill.AllowedExportFormats = allowdedFormat;
                                    //                                        crvPurchaseReturnBill.ReportSource = reportDocument;
                                    //                                        crvPurchaseReturnBill.ReuseParameterValuesOnRefresh = true;
                                    //                                        crvPurchaseReturnBill.RefreshReport();
                                    //                                        crvPurchaseReturnBill.PrintMode = CrystalDecisions.Windows.Forms.PrintMode.PrintToPrinter;
                                    //                                        crvPurchaseReturnBill.PrintReport();
                                    //                                    }
                                    //                                    catch (Exception ex)
                                    //                                    {
                                    //                                        MessageBox.Show(ex.Message);
                                    //                                    }
                                    //#endregion

                                    if (status)
                                    {
                                        RefreshForm();
                                        MetroMessageBox.Show(this, "", "Successfully Updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                                    }
                                    else
                                        MetroMessageBox.Show(this, "Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                                    hlblHeader.Text = "Purchase Return Invoice";
                                }

                            }
                            else
                            {
                                MetroMessageBox.Show(this, "Invalid entry!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                            }
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "Invalid entry!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                string returnValue = string.Empty;
                using (var search = new FrmListOutward())
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
                    OutwardId = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnOutward> bllSearch = new BllSearch<DmnOutward>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "P.EntryNo,P.EntryDate,G.Caption,GP.Caption as PaymentText,P.EType,P.PaymentMode,P.LedgerId,L.LedgerName,L.LedgerAddress,L.Mobile,P.TransportNo," +
                        "P.DocumentNo,P.DocumentDate,L.CreditDays,P.DueDate,P.TotalAmount,P.DiscountPer,P.DiscountAmount,P.TotalMiscCharges,P.Remarks," +
                        "P.TotalNetAmount,P.AmountAdjust";
                    objSearch.Table = " tblOutward P LEFT JOIN tblAccountLedger L ON P.LedgerId=L.LedgerId " +
                        "left join tblGeneralOptions as G on P.EType=G.GeneralId " +
                        "left join tblGeneralOptions as GP on P.PaymentMode=GP.GeneralId";
                    objSearch.WhereNdGroupBy = "Where P.OutwardId = " + OutwardId;
                    objSearch.OrderBy = "Order By P.OutwardId ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    mtxtInvNo.Text = Convert.ToString(gridSource[0].EntryNo);
                    mdtpInvDate.Text = Convert.ToString(gridSource[0].EntryDate);
                    mcmbTax.SelectedIndex = mcmbTax.FindString(gridSource[0].Caption);
                    mcmbMode.SelectedIndex = mcmbMode.FindString(gridSource[0].PaymentText);
                    // mcmbTax.SelectedValue = gridSource[0].EType;
                    //mcmbMode.SelectedValue = gridSource[0].PaymentMode;
                    LedgerId = Convert.ToInt64(gridSource[0].LedgerId);
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
                    objSearch.Table = " tblOutDetails P LEFT JOIN tblItem I ON P.ItemId=I.ItemId LEFT JOIN tblUnit U ON P.UnitId=U.UnitId" +
                        " LEFT JOIN tblGeneralOptions G ON P.TaxType = G.GeneralId";
                    objSearch.WhereNdGroupBy = "Where P.OutwardId = " + OutwardId;
                    objSearch.OrderBy = "Order By P.OutwardId ";
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
                                dgvItemDet.Rows[i].Cells[iOutwardDetailsId].Value = Convert.ToString(gridSourceItem[i].OutwardDetailsId);
                                dgvItemDet.Rows[i].Cells[iPDocNo].Value = Convert.ToString(gridSourceItem[i].PMDocumentNo);
                                dgvItemDet.Rows[i].Cells[iItemId].Value = Convert.ToString(gridSourceItem[i].ItemId);
                                dgvItemDet.Rows[i].Cells[iItemCode].Value = Convert.ToString(gridSourceItem[i].ItemCode);
                                dgvItemDet.Rows[i].Cells[iBarcode].Value = Convert.ToString(gridSourceItem[i].ItemBarCode);
                                dgvItemDet.Rows[i].Cells[iName].Value = Convert.ToString(gridSourceItem[i].ItemName);
                                dgvItemDet.Rows[i].Cells[iUnitId].Value = Convert.ToString(gridSourceItem[i].UnitId);
                                dgvItemDet.Rows[i].Cells[iUnit].Value = Convert.ToString(gridSourceItem[i].UnitName);
                                dgvItemDet.Rows[i].Cells[iLockSaleRate].Value = gridSourceItem[i].LockSaleRate;
                                dgvItemDet.Rows[i].Cells[iSellingPer].Value = gridSourceItem[i].SellingPer;
                                dgvItemDet.Rows[i].Cells[iQohToSubtract].Value = Convert.ToString(gridSourceItem[i].PacketQty);
                                dgvItemDet.Rows[i].Cells[iItemQOH].Value = Convert.ToString(gridSourceItem[i].QuantityOnHand);
                                dgvItemDet.Rows[i].Cells[iStockItem].Value = Convert.ToString(gridSourceItem[i].StockItem);
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

                                dgvItemDet.Rows[i].Cells[iItemMrp].Value = Convert.ToString(gridSourceItem[i].MRP);
                                dgvItemDet.Rows[i].Cells[iItemSelPer].Value = Convert.ToString(gridSourceItem[i].SellingPer);

                                ItemTotal(i, gridSourceItem[i].Quantity, iQuantity);
                            }
                        }
                    }
                    objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = " tblOutwardMiscCharges M LEFT JOIN tblGeneralOptions G ON M.GeneralId=G.GeneralId ";
                    objSearch.WhereNdGroupBy = "Where M.OutwardId = " + OutwardId;
                    objSearch.OrderBy = "Order By M.OutwardId ";
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

        #endregion



        private void dgvItemDet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int currowIndex = dgvItemDet.CurrentRow.Index;

            qty = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value));
            rate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iPurchaseRate].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iPurchaseRate].Value));
            decimal amount = qty * rate;
            decimal grossAmount = decimal.Parse(Convert.ToString(amount - disc) == string.Empty ? "0.00" : Convert.ToString(amount - disc));
            cessper = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iCessPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iCessPer].Value));
            taxper = Convert.ToDecimal(dgvItemDet.Rows[currowIndex].Cells[iTaxPer].Value);

            // taxRate = (grossAmount / (100 + taxper + cessper)) * taxper;
            decimal taxRate = grossAmount * (taxper / 100);
            ///cessAmount = (grossAmount / (100 + taxper + cessper)) * cessper;
            decimal cessAmount = grossAmount * (cessper / 100);

            decimal netAmount = grossAmount + taxRate + cessAmount;
            dgvItemDet.Rows[currowIndex].Cells[iTaxAmount].Value = Convert.ToString(taxRate.ToString("0.00"));
            dgvItemDet.Rows[currowIndex].Cells[iCessAmount].Value = Convert.ToString(cessAmount.ToString("0.00"));
            dgvItemDet.Rows[currowIndex].Cells[iTotalAmount].Value = Convert.ToString(netAmount.ToString("0.00"));

        }
        private void dgvItemDet_KeyPress(object sender, KeyPressEventArgs e)
        {
            int row = dgvItemDet.CurrentRow.Index;
            int col = dgvItemDet.CurrentCell.ColumnIndex;
            if (e.KeyChar == (char)Keys.Enter)
            {
                dgvItemDet.Focus();
                dgvItemDet.CurrentCell = dgvItemDet[dgvItemDet.CurrentCell.ColumnIndex + 1, dgvItemDet.CurrentCell.RowIndex];
            }
        }

        private void dgvItemDet_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {


            }

        }

        private void mbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void dgvItemDet_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            int iColumn = dgvItemDet.CurrentCell.ColumnIndex;
            int iRow = dgvItemDet.CurrentCell.RowIndex;

            if (iColumn == dgvItemDet.ColumnCount - 1)
            {
                if (dgvItemDet.RowCount > (iRow + 1))
                {
                    dgvItemDet.CurrentCell = dgvItemDet[1, iRow + 1];
                }
                else
                {
                    //focus next control
                }
            }
            else
                if (iColumn == 5 || iColumn == 16 || iColumn == 19)
            {
                iColumn = iColumn + 1;
            }
            else if (iColumn > 21)
            {
                dgvItemDet.CurrentCell = dgvItemDet[1, iRow + 1];
            }
            else
            {
                dgvItemDet.CurrentCell = dgvItemDet[iColumn + 1, iRow];
            }


        }

        private void FrmOutward_KeyDown(object sender, KeyEventArgs e)
        {

        }



        private void dgvItemDet_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mcmbFrom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            toid = Convert.ToInt32(mcmbFrom.SelectedValue);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}

