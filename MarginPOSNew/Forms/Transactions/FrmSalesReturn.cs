using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarginPOSNew.Forms.Transactions
{
    public partial class FrmSalesReturn : MetroForm
    {
        #region Declarations

        private Control _last;//to set focus after mesagebox pop up
        string hostName = Environment.MachineName;
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = Convert.ToInt64(FrmDashboard.BranchId);//16
        long UserId = Convert.ToInt64(FrmDashboard.UserId);//1
        long FinancialYearId = Convert.ToInt64(FrmDashboard.FinancialYearId);//10
        //long SalesId = 0;//used for edit
        long ItemId = 0;
        long SlNo = 0;
        long LedgerId = 0;
        long CustomerId = 0;
        long EmployeeId = 0;//to store the eemployee's id
        decimal TotalSaveAmount = 0;//to hold the total save amount of  the bill
        decimal QuantitytoSubtract = 0;
        //summary
        long SalesReturnMasterId = 0;
        decimal TotalNumber = 0;
        //  long TotalNumber = 0;
        decimal AmountSummary = 0;
        decimal DiscountSummary = 0;
        decimal GrossSummary = 0;
        decimal TaxSummary = 0;
        decimal CessSummary = 0;
        decimal TotalAmtSummary = 0;
        decimal TotalSavings = 0;
        decimal SalesRatemrp = 0;//to restore old sales rate when amnt >mrp enered
        //End Summary
        decimal qty;
        decimal rate;
        decimal taxper;
        decimal disc;
        decimal discper;
        decimal cessper;
        int taxIncluded;
        decimal miscTotal = 0;
        long rowIndexforGridviewCBox = 0;
        string existingAddress = "";//to hold address and mobile fetched from search
        string existingMobile = "";
        bool isStockItem = false;

        #region new Variables

        long TotQuantity = 0;
        decimal TotAmount = 0;
        decimal TotTax = 0;
        decimal TotDisc = 0;
        decimal TotGross = 0;
        decimal TotCess = 0;
        decimal TotNet = 0;

        string LedgerAddress = "";
        int LedgerCreditDays = 0;
        DateTime LedgerDueDate = DateTime.Now.Date;
        string Username = "";


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
        const int iSalesRate = 8;
        const int iMRP = 9;
        const int iAmount = 10;
        const int iDiscPer = 11;
        const int iDiscAmount = 12;
        const int iGrossAmount = 13;
        const int iSalesTaxIncl = 14;
        const int iTaxPer = 15;
        const int iTaxAmount = 16;
        const int iTaxType = 17;
        const int iCessPer = 18;
        const int iCessAmount = 19;
        const int iRate = 20;
        const int iTotalTaxWithCess = 21;
        const int iTotalAmount = 22;//this is the net amount
        const int iSellingPer = 23;
        const int iItemSaveAmount = 24;//savings in each item
        const int iLockSaleRate = 25;
        const int iQohToSubtract = 26;//this amount is decreased from qoh
        const int iSalesReturnDetailsId = 27;//used in edit od Sales
        const int iItemQOH = 28;//to store item qoh
        const int iStockItem = 29;


        #endregion
        #endregion
        #region Functions
        public FrmSalesReturn()
        {
            InitializeComponent();
        }

        public void EmployeeListPopUp()
        {
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
            _objSearch.ColumnHeaders = "Id,Employee Name";
            _objSearch.Select = "EmployeeId as Id, EmployeeName as Field1";
            _objSearch.Table = " tblEmployee";
            _objSearch.WhereNdGroupBy = "where Status = 0 And BranchId = " + BranchId;
            _objSearch.OrderBy = " order by EmployeeName ASC";
            // using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 1200, 400, 100, 100))//need to change it
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
                EmployeeId = Convert.ToInt64(returnValue.ToString());
                BllSearch<DmnEmployee> bllSearch = new BllSearch<DmnEmployee>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "EmployeeName";
                objSearch.Table = " tblEmployee ";
                objSearch.WhereNdGroupBy = "Where EmployeeId = " + EmployeeId;
                //objSearch.OrderBy = "Order By LedgerName ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                // mtxtEmployeeName.Text = Convert.ToString(gridSource[0].EmployeeName);
            }
        }

        public void LedgerSearchPopUp()
        {

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
            _objSearch.ColumnHeaders = "Id,Code,ICard No,Name,Mobile,Credit Days";
            _objSearch.Select = "L.LedgerId as Id,L.LedgerCode as Field1,L.ICardNo as Field2," +
                " L.LedgerName As Field3, L.Mobile as Field4,L.CreditDays as Field5";
            _objSearch.Table = " tblAccountLedger L LEFT JOIN tblAccountGroup Ac  ON L.AccountGroupId = Ac.AccountGroupId";
            _objSearch.WhereNdGroupBy = " WHERE  Ac.AccountGroupName='Customer' And L.Status = 0 And L.BranchId = " + BranchId;
            _objSearch.OrderBy = " ";
            // using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 1200, 400, 100, 100))//need to change it
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
                objSearch.Select = "LedgerName,LedgerAddress,Mobile,CreditDays,ICardNo";
                objSearch.Table = " tblAccountLedger ";
                objSearch.WhereNdGroupBy = "Where LedgerId = " + LedgerId;
                //  objSearch.OrderBy = "Order By LedgerName ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                mtxtiCardNo.Text = Convert.ToString(gridSource[0].ICardNo);
                mtxtLedger.Text = Convert.ToString(gridSource[0].LedgerName);
                LedgerAddress = Convert.ToString(gridSource[0].LedgerAddress);
                existingMobile = Convert.ToString(gridSource[0].Mobile);
                existingAddress = Convert.ToString(gridSource[0].LedgerAddress);
                mtxtMobile.Text = Convert.ToString(gridSource[0].Mobile);
                LedgerCreditDays = Convert.ToInt16(gridSource[0].CreditDays);
                LedgerDueDate = Convert.ToDateTime(DateTime.Now.AddDays(Convert.ToInt16(gridSource[0].CreditDays)));
            }
        }

        public void DeleteSalesDetails(long MasterId)
        {
            DmnSalesReturnDetails dmnSalesReturnDetails = new DmnSalesReturnDetails();
            BllSalesReturnDetails bllSalesReturnDetails = new BllSalesReturnDetails();
            dmnSalesReturnDetails.SalesReturnMasterId = MasterId;
            var DellstData = bllSalesReturnDetails.DeleteByMasterId("prdDeleteSalesReturnGridDetails", dmnSalesReturnDetails);
        }

        public void DeleteMiscCharges(long MasterId)
        {
            DmnSalesReturnMiscCharges dmnMiscCharges = new DmnSalesReturnMiscCharges();
            BllSalesReturnMiscCharges bllSalesmisc = new BllSalesReturnMiscCharges();
            dmnMiscCharges.SalesReturnMasterId = MasterId;
            var DellstData = bllSalesmisc.DeleteMiscCharges("prdDeleteSalesReturnMiscCharges", dmnMiscCharges);
        }

        public void ClearCustomerDetails()
        {

            LedgerCreditDays = 0;
            LedgerDueDate = DateTime.Now;
            mtxtLedger.Clear();
            LedgerAddress = "";
            mtxtMobile.Clear();
        }

        public void RefreshForm()
        {
            //#region refresh payment panel


            //#endregion
            isStockItem = false;
            EmployeeId = 0;
            TotalSaveAmount = 0;
            QuantitytoSubtract = 0;//this * item quantity is used to update qoh
            SalesRatemrp = 0;
            SalesReturnMasterId = 0;
            miscTotal = 0;
            ItemId = 0;
            SlNo = 0;
            LedgerId = 0;
            CustomerId = 0;
            TotalNumber = 0;
            AmountSummary = 0;
            DiscountSummary = 0;
            GrossSummary = 0;
            TaxSummary = 0;
            CessSummary = 0;
            TotalAmtSummary = 0;
            mpnlPayment.Hide();
            mdtpInvDate.Text = Convert.ToString(DateTime.Now.Date);
            LedgerDueDate = DateTime.Now.Date;
            hlblHeader.Text = "Sales Return \t[User : " + Username + "]";
            MetroTextBox[] listTbx = {  mtxtiCardNo, mtxtLedger, mtxtMobile, mtxtRemarks, mtxtpnlNetAmount,
                mtxtBalanceAmount,mtxtpnlNetAmount,mtxtAmountReceived };
            mtxtAdjust.Text = Convert.ToString("0.0");
            mtxtAmount.Text = Convert.ToString("0.00");
            mtxtGrandTot.Text = Convert.ToString("0.00");
            mtxtNetAmount.Text = Convert.ToString("0.00");
            mtxtMiscCharges.Text = Convert.ToString("0.00");
            TotQuantity = 0;
            TotAmount = 0;
            TotTax = 0;
            TotDisc = 0;
            TotGross = 0;
            TotCess = 0;
            TotNet = 0;
            mtxtTotDiscAmnt.Text = Convert.ToString("0.00");
            mtxtTotDiscPer.Text = Convert.ToString("0.00");
            objGeneral.ResetSelectedMetroTextBoxes(listTbx);
            dgvItemDet.Rows.Clear();
            dgvItemDet.Rows[0].Cells[iBarcode].Selected = true;
            FillTaxComboBox();
            FillParticularComboBox();
            FillPaymentComboBox();
            SalesInvoiceNo();
            mlvMisc.Items.Clear();
            mlvMisc.Columns.Add("Sl No", 100, HorizontalAlignment.Left);//100
            mlvMisc.Columns.Add("Particulars", 350, HorizontalAlignment.Left);//350
            mlvMisc.Columns.Add("Price", 200, HorizontalAlignment.Right);//200
            mlvMisc.Columns.Add("Id", 0);//0
            mlvMisc.View = View.Details;

            ActiveControl = dgvItemDet;

            dgvItemDet.CurrentCell = dgvItemDet.Rows[0].Cells[3];
            dgvItemDet.CurrentCell.Selected = true;
            dgvItemDet.Focus();
            mcmbSign.SelectedIndex = 0;
        }


        public void InsertSalesDet(long MasterId)
        {
            for (int i = 0; i < dgvItemDet.Rows.Count; i++)
            {
                string itId = Convert.ToString(dgvItemDet.Rows[i].Cells[iItemId].Value);
                if (itId != "")
                {

                    DmnSalesReturnDetails dmnSalesReturnDetails = new DmnSalesReturnDetails();
                    BllSalesReturnDetails bllSalesReturnDetails = new BllSalesReturnDetails();
                    dmnSalesReturnDetails.SalesReturnMasterId = MasterId;
                    decimal cgst = Convert.ToDecimal(dgvItemDet.Rows[i].Cells[iTaxAmount].Value) / 2;
                    dmnSalesReturnDetails.ItemId = long.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iItemId].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iItemId].Value));
                    dmnSalesReturnDetails.ItemCode = Convert.ToString(dgvItemDet.Rows[i].Cells[iItemCode].Value);
                    dmnSalesReturnDetails.ItemBarCode = Convert.ToString(dgvItemDet.Rows[i].Cells[iBarcode].Value);
                    dmnSalesReturnDetails.UnitId = long.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iUnitId].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iUnitId].Value));
                    dmnSalesReturnDetails.Quantity = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value));
                    //    QuantitytoSubtract = QuantitytoSubtract * Convert.ToInt32(dmnSalesDetails.Quantity.ToString("1"));
                    dmnSalesReturnDetails.QuantitytoSubtract = Decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iQohToSubtract].Value) == string.Empty ? "1" : Convert.ToString(dgvItemDet.Rows[i].Cells[iQohToSubtract].Value));

                    dmnSalesReturnDetails.FreeQuantity = 0;// decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value));
                    dmnSalesReturnDetails.SalesRate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iSalesRate].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iSalesRate].Value));
                    dmnSalesReturnDetails.IRate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iRate].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iRate].Value));//rate in item
                    dmnSalesReturnDetails.Amount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iAmount].Value));
                    dmnSalesReturnDetails.ItemDiscountPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscPer].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscPer].Value));//this is saved to discount amouunt field in details
                    dmnSalesReturnDetails.ItemDiscountAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscAmount].Value));
                    dmnSalesReturnDetails.IGSTAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value));
                    dmnSalesReturnDetails.CGSTAmount = decimal.Parse(Convert.ToString(cgst) == string.Empty ? "0" : Convert.ToString(cgst));
                    dmnSalesReturnDetails.SGSTAmount = decimal.Parse(Convert.ToString(cgst) == string.Empty ? "0" : Convert.ToString(cgst));
                    dmnSalesReturnDetails.GrossAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iGrossAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iGrossAmount].Value));
                    dmnSalesReturnDetails.TaxPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxPer].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxPer].Value));
                    dmnSalesReturnDetails.TaxAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value));
                    dmnSalesReturnDetails.CessPer = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iCessPer].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iCessPer].Value));
                    dmnSalesReturnDetails.CessAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iCessAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iCessAmount].Value));
                    dmnSalesReturnDetails.ItemNetAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTotalAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTotalAmount].Value));
                    dmnSalesReturnDetails.BranchId = BranchId;
                    dmnSalesReturnDetails.FinancialYearId = FinancialYearId;
                    dmnSalesReturnDetails.TaxType = long.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxType].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxType].Value));
                    dmnSalesReturnDetails.Isrno = 0;//check
                    dmnSalesReturnDetails.MRP = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iMRP].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iMRP].Value));
                    dmnSalesReturnDetails.TotalSaveAmount = decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iItemSaveAmount].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iItemSaveAmount].Value));
                    dmnSalesReturnDetails.CreatedBy = UserId;
                    dmnSalesReturnDetails.StockItem = Int32.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iStockItem].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iStockItem].Value));//used to check need to update qoh

                    #region to find quantity to reduce/add

                    BllSearch<DmnGridFields> bllQunatitytoReduceSearch = new BllSearch<DmnGridFields>();

                    DmnSearch objQtySearch = new DmnSearch();
                    objQtySearch = new DmnSearch();
                    objQtySearch.Select = " ItemId as Id,case MainUnitId when '" + dmnSalesReturnDetails.UnitId + "' then 1 " +
                        "Else PacketQty end as Field1,StockItem as Field2";
                    objQtySearch.Table = " tblItem  ";
                    objQtySearch.WhereNdGroupBy = "where ItemId=" + dmnSalesReturnDetails.ItemId;
                    var qtySource = bllQunatitytoReduceSearch.GetSearchData("prdSearch", objQtySearch);
                    dmnSalesReturnDetails.QuantitytoSubtract = Convert.ToDecimal(qtySource[0].Field1);

                    #endregion


                    var DetlstData = bllSalesReturnDetails.AddData("prdInsertSalesReturnDetails", dmnSalesReturnDetails);
                }
            }
        }

        public void SalesInvoiceNo()
        {
            BllSearch<DmnBranch> bllSearch = new BllSearch<DmnBranch>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "SalesReturnSrlNo";
            objSearch.Table = " tblBranch ";
            objSearch.WhereNdGroupBy = " Where BranchId=" + BranchId;
            var srlNo = bllSearch.GetSearchData("prdSearch", objSearch);
            mtxtInvNo.Text = Convert.ToString(Convert.ToUInt64(srlNo[0].SalesReturnSrlNo) + 1);
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
            TotalSavings = 0;

            for (int i = 0; i < dgvItemDet.Rows.Count; ++i)
            {

                TotalNumber += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value));
                //  TotalNumber += Int32.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[i].Cells[iQuantity].Value));

                AmountSummary += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iAmount].Value));
                DiscountSummary += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iDiscAmount].Value));
                GrossSummary += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iGrossAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iGrossAmount].Value));
                TaxSummary += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTaxAmount].Value));
                CessSummary += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iCessAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iCessAmount].Value));
                TotalAmtSummary += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iTotalAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iTotalAmount].Value));
                TotalSavings += decimal.Parse(Convert.ToString(dgvItemDet.Rows[i].Cells[iItemSaveAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[i].Cells[iItemSaveAmount].Value));
            }



            TotQuantity = Convert.ToInt64(TotalNumber);
            TotAmount = AmountSummary;
            TotTax = TaxSummary;
            TotDisc = DiscountSummary;
            TotGross = GrossSummary;
            TotCess = CessSummary;
            TotNet = TotalAmtSummary;
            TotalSaveAmount = TotalSavings;
            mtxtTotSaveAmnt.Text = Convert.ToString(TotalSavings.ToString("0.00"));
            mtxtTotQuantity.Text = Convert.ToString(TotQuantity.ToString("0.00"));

        }

        public void FillGridUnitCombo(long itemId, int CurrentIndex)
        {
            DmnItem dmnItem = new DmnItem();
            dmnItem.ItemId = itemId;

            BllSalesReturnDetails objBll = new BllSalesReturnDetails();
            var unitList = objBll.GetItemUnits(dmnItem);
            DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)dgvItemDet.Rows[CurrentIndex].Cells[iUnit];
            var unitSource = unitList.Select(x => new { x.UnitId, x.UnitName });
            if (unitSource != null)
            {
                var mainUnit = unitList.ElementAt(0).UnitName;

                cbCell.DataSource = unitSource.ToList();
                cbCell.DisplayMember = "UnitName";
                cbCell.ValueMember = "UnitId";
                dgvItemDet.CurrentRow.Cells[iUnit].Value = mainUnit;
            }



        }
        public void FillParticularComboBox()
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
        public void FillTaxComboBox()
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
        }

        public void FillPaymentComboBox()
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
        }
        public void DiscountAmtToPer(int currow, decimal discAmount)//discper
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
            TotDisc = Convert.ToDecimal(DiscountSummary.ToString("0.00"));
        }

        public void DiscountPerToAmt(int currow, decimal discountPer)//discountamount
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
            TotDisc = Convert.ToDecimal(DiscountSummary.ToString("0.00"));
        }

        public void ItemTotal(int currowIndex, decimal editingValue, int columnIndex)// for row
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
            decimal mrp;
            decimal saveamount;

            qty = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value));
            rate = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iSalesRate].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iSalesRate].Value));
            taxper = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iTaxPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iTaxPer].Value));
            discper = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iDiscPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iDiscPer].Value));
            //     disc = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iDiscAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iDiscAmount].Value));
            cessper = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iCessPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iCessPer].Value));
            taxIncluded = int.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iSalesTaxIncl].Value) == string.Empty ? "0" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iSalesTaxIncl].Value));
            mrp = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iMRP].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iMRP].Value));
            if (iSalesRate == columnIndex)
            {
                rate = editingValue;
                dgvItemDet.Rows[currowIndex].Cells[iSalesRate].Value = decimal.Parse(Convert.ToString(rate) == string.Empty ? "0.00" : Convert.ToString(rate));
            }
            else if (iQuantity == columnIndex)
            {
                qty = editingValue;
                dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value = decimal.Parse(Convert.ToString(qty) == string.Empty ? "0.00" : Convert.ToString(qty));

            }
            amount = qty * rate;
            dgvItemDet.Rows[currowIndex].Cells[iAmount].Value = decimal.Parse(Convert.ToString(amount.ToString("0.00")) == string.Empty ? "0.00" : Convert.ToString(amount.ToString("0.00")));
            DiscountPerToAmt(currowIndex, discper);
            disc = decimal.Parse(Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iDiscAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[currowIndex].Cells[iDiscAmount].Value));

            grossAmount = decimal.Parse(Convert.ToString(amount - disc) == string.Empty ? "0.00" : Convert.ToString(amount - disc));
            dgvItemDet.Rows[currowIndex].Cells[iGrossAmount].Value = decimal.Parse(Convert.ToString(grossAmount.ToString("0.00")) == string.Empty ? "0.00" : Convert.ToString(grossAmount.ToString("0.00")));
            if (taxIncluded == (int)ItemTaxMode.TaxExclusive) //Tax Exclusive
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
                    taxRate = (grossAmount * taxper / 100);
                    cessper = 0;
                    cessAmount = 0;
                }
                else//tax with cess
                {
                    taxRate = (grossAmount * taxper / 100);
                    cessAmount = (grossAmount * cessper / 100);
                }
                netAmount = (grossAmount * (100 + taxper + cessper) / 100);
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
                else//tax with cess
                {
                    taxRate = (grossAmount / (100 + taxper + cessper)) * taxper;
                    cessAmount = (grossAmount / (100 + taxper + cessper)) * cessper;
                }

                netAmount = grossAmount;
            }
            //   dgvItemDet.Rows[currowIndex].Cells[iAmount].Value = Convert.ToString(amount.ToString("0.00"));
            dgvItemDet.Rows[currowIndex].Cells[iTaxAmount].Value = Convert.ToString(taxRate.ToString("0.00"));
            dgvItemDet.Rows[currowIndex].Cells[iCessAmount].Value = Convert.ToString(cessAmount.ToString("0.00"));
            //  dgvItemDet.Rows[currowIndex].Cells[iGrossAmount].Value = Convert.ToString(grossAmount.ToString("0.00"));
            dgvItemDet.Rows[currowIndex].Cells[iTotalAmount].Value = Convert.ToString(netAmount.ToString("0.00"));
            // dgvItemDet.Rows[currowIndex].Cells[iQuantity].Value = Convert.ToString(qty.ToString("0.00"));

            saveamount = (mrp * qty) - netAmount;
            dgvItemDet.Rows[currowIndex].Cells[iItemSaveAmount].Value = Convert.ToString(saveamount.ToString("0.00"));
            dgvItemDet.Rows[currowIndex].Cells[iTotalTaxWithCess].Value = Convert.ToString((cessAmount + taxRate).ToString("0.00"));
            GridSummary();
        }

        public void Itemdetails(int CurrentIndex, long ItemId)
        {
            rowIndexforGridviewCBox = CurrentIndex;//this is to store value for grid viewcombo box control
            SlNo = CurrentIndex;
            BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "I.ItemId,I.ItemCode,I.Barcode,I.SearchName As ItemName ,I.SalesTaxIncl,I.MainUnitId,U.UnitName,I.PurchaseRate1," +
                "I.SGSTPer,I.CGSTPer,I.IGSTPer,I.CessPer,I.TaxType,I.MRP1,I.SellingPer,I.LockSaleRate,I.PacketQty,I.QuantityOnhand,I.StockItem";
            objSearch.Table = " tblItem I LEFT JOIN tblUnit U ON I.MainUnitId=U.UnitId";
            objSearch.WhereNdGroupBy = "Where I.ItemId = " + ItemId;
            objSearch.OrderBy = "Order By I.ItemName ";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);

            dgvItemDet.Rows[CurrentIndex].Cells[iSlNo].Value = SlNo + 1;
            string vBarcode = Convert.ToString(itemSource[0].Barcode);
            dgvItemDet.Rows[CurrentIndex].Cells[iItemId].Value = Convert.ToString(itemSource[0].ItemId);
            dgvItemDet.Rows[CurrentIndex].Cells[iItemCode].Value = Convert.ToString(itemSource[0].ItemCode);
            dgvItemDet.Rows[CurrentIndex].Cells[iBarcode].Value = Convert.ToString(itemSource[0].Barcode);
            dgvItemDet.Rows[CurrentIndex].Cells[iName].Value = Convert.ToString(itemSource[0].ItemName);
            dgvItemDet.Rows[CurrentIndex].Cells[iQuantity].Value = 1;
            dgvItemDet.Rows[CurrentIndex].Cells[iUnitId].Value = Convert.ToString(itemSource[0].MainUnitId);
            dgvItemDet.Rows[CurrentIndex].Cells[iUnit].Value = Convert.ToString(itemSource[0].UnitName);//new 
            dgvItemDet.Rows[CurrentIndex].Cells[iTaxType].Value = Convert.ToString(itemSource[0].TaxType);
            dgvItemDet.Rows[CurrentIndex].Cells[iMRP].Value = Convert.ToString(itemSource[0].MRP1);
            dgvItemDet.Rows[CurrentIndex].Cells[iSellingPer].Value = Convert.ToString(itemSource[0].SellingPer);
            dgvItemDet.Rows[CurrentIndex].Cells[iRate].Value = Convert.ToString((itemSource[0].PurchaseRate1) * (100 + itemSource[0].SellingPer) / 100);//this is fixed and does not change with change in qty 
            decimal salesrate = Convert.ToDecimal((itemSource[0].PurchaseRate1) * (100 + itemSource[0].SellingPer) / 100);
            SalesRatemrp = salesrate;
            dgvItemDet.Rows[CurrentIndex].Cells[iSalesRate].Value = salesrate.ToString("#.00");
            dgvItemDet.Rows[CurrentIndex].Cells[iItemSaveAmount].Value = Convert.ToString(itemSource[0].MRP1 - (itemSource[0].PurchaseRate1) * (100 + itemSource[0].SellingPer) / 100);
            dgvItemDet.Rows[CurrentIndex].Cells[iLockSaleRate].Value = Convert.ToString(itemSource[0].LockSaleRate);
            dgvItemDet.Rows[CurrentIndex].Cells[iQohToSubtract].Value = 1;//initially main unit is loaded so default qoh to rduce i 1
            dgvItemDet.Rows[CurrentIndex].Cells[iItemQOH].Value = Convert.ToString(itemSource[0].QuantityOnHand);
            dgvItemDet.Rows[CurrentIndex].Cells[iStockItem].Value = Convert.ToString(itemSource[0].StockItem);




            #region unit from combobox

            FillGridUnitCombo(ItemId, CurrentIndex);
            #endregion

            #region locking salesa rate

            int locksalerate = Convert.ToInt16(dgvItemDet.Rows[CurrentIndex].Cells[iLockSaleRate].Value);
            if (locksalerate == 0)
            {
                dgvItemDet.Rows[CurrentIndex].Cells[iSalesRate].ReadOnly = true;
            }

            #endregion

            dgvItemDet.Rows[CurrentIndex].Cells[iSalesTaxIncl].Value = Convert.ToString(itemSource[0].SalesTaxIncl);

            //else
            //{

            //if (Convert.ToString(itemSource[0].IGSTPer) == "")
            //{
            //    dgvItemDet.Rows[CurrentIndex].Cells[iTaxPer].Value = Convert.ToString(itemSource[0].SGSTPer + itemSource[0].CGSTPer);
            //}
            //else
            //{
            //    dgvItemDet.Rows[CurrentIndex].Cells[iTaxPer].Value = Convert.ToString(itemSource[0].IGSTPer);
            //}

            dgvItemDet.Rows[CurrentIndex].Cells[iTaxPer].Value = Convert.ToString(itemSource[0].SGSTPer + itemSource[0].CGSTPer + itemSource[0].IGSTPer);
            dgvItemDet.Rows[CurrentIndex].Cells[iCessPer].Value = Convert.ToString(itemSource[0].CessPer);
            // }
            int currentrow = CurrentIndex;
            discper = decimal.Parse(Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iDiscPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iDiscPer].Value));
            disc = decimal.Parse(Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iDiscAmount].Value) == string.Empty ? "0.00" : Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iDiscAmount].Value));
            decimal value = 0;
            int columnIndex = 0;
            ItemTotal(currentrow, value, columnIndex);//total amount of a row
            TotalSummary();
            //DiscountPer(currentrow, discper);
            //DiscountAmount(currentrow, disc);
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
            mtxtNetAmount.Text = Convert.ToString(NetAmount.ToString("0.00"));
        }

        public bool IsSavable()
        {
            Control[] controllist = { mcmbTax, mcmbMode };
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }

            return true;
        }
        public void InsertMiscCharges(long MasterId)
        {
            foreach (ListViewItem l in mlvMisc.Items)
            {
                DmnSalesReturnMiscCharges dmnSalesMisc = new DmnSalesReturnMiscCharges();
                BllSalesReturnMiscCharges bllSalesMisc = new BllSalesReturnMiscCharges();
                dmnSalesMisc.SalesReturnMasterId = MasterId;
                dmnSalesMisc.GeneralId = long.Parse(Convert.ToString(l.SubItems[3].Text) == string.Empty ? "0" : Convert.ToString(l.SubItems[3].Text));
                dmnSalesMisc.MiscAmount = decimal.Parse(Convert.ToString(l.SubItems[2].Text) == string.Empty ? "0.00" : Convert.ToString(l.SubItems[2].Text));//this is to be added in table
                dmnSalesMisc.CreatedBy = UserId;
                var MislstData = bllSalesMisc.AddData("prdInsertSalesReturnMiscCharges", dmnSalesMisc);
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

        #endregion


        #region Events
        private void FrmSales_Load(object sender, EventArgs e)
        {
            try
            {
                DmnSearch dmnSearch = new DmnSearch();
                ClsDocument clsDocument = new ClsDocument();
                dmnSearch.Select = " tblUserAccount.Username ";
                dmnSearch.Table = " tblUserAccount ";
                dmnSearch.WhereNdGroupBy = " where UserAccountId= " + FrmDashboard.UserId;
                BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
                var username = bllSearch.GetSearchData("prdSearch", dmnSearch);
                Username = username[0].Username.ToString();
                // hlblHeader.Text += "  \t[User : " + username[0].Username.ToString() + "]";
                RefreshForm();
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
                    if (dgvItemDet.Rows.Count > 1 && Convert.ToDecimal(mtxtNetAmount.Text) > 0)
                    {
                        mpnlPayment.Visible = true;
                        mtxtpnlNetAmount.Text = mtxtNetAmount.Text;
                        //  ActiveControl = mtxtAmountReceived;
                        //mtxtAmountReceived.Focus();
                        ActiveControl = mcmbMode;
                        mcmbMode.Focus();
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "Invalid entry", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
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
                DataGridViewComboBoxCell cel = (DataGridViewComboBoxCell)dgvItemDet.Rows[currentcell.Y].Cells[iUnit];
                cel.Value = sendingCB.EditingControlFormattedValue.ToString();

                if (cmbSelectedIndex != -1)
                {

                    var cmbValue = ((ComboBox)sender).SelectedValue;

                    if (cmbValue is long)
                    {

                    }
                    else
                    {
                        string arr = cmbValue.ToString();
                        string[] values = arr.Split(',');
                        string value = values[0].ToString();
                        string[] unit = value.Split('=');
                        long id = Convert.ToInt32(unit[1].ToString());
                        cmbValue = id;

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
                        " ELSE MRP2 end AS Field3, " +
                        "case MainUnitId when '" + selectedUnitId + "' then PurchaseRate1*(100+SellingPer)/100  " +
                        "ELSE PurchaseRate2*(100+SellingPer)/100 end AS Field4 ," +
                        "case MainUnitId when '" + selectedUnitId + "' then 1 " +
                        "Else PacketQty end as Field5";
                    objSearch.Table = " tblItem  ";
                    objSearch.WhereNdGroupBy = "where ItemId=" + selectedItemId;
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    //   dgvItemDet.Rows[CurrentIndex].Cells[iSalesRate].Value = Convert.ToString(itemSource[0].Field4.ToString("0.00"));
                    dgvItemDet.Rows[CurrentIndex].Cells[iSalesRate].Value = (Convert.ToDecimal(itemSource[0].Field4).ToString("#.00"));
                    dgvItemDet.Rows[CurrentIndex].Cells[iQohToSubtract].Value = Convert.ToString(itemSource[0].Field5);
                    #region changing the mrp and unit id when selection changed

                    dgvItemDet.Rows[CurrentIndex].Cells[iRate].Value = (Convert.ToDecimal(itemSource[0].Field4).ToString("#.00"));//this is the irate
                    dgvItemDet.Rows[CurrentIndex].Cells[iMRP].Value = Convert.ToString(itemSource[0].Field3.ToString());
                    dgvItemDet.Rows[CurrentIndex].Cells[iUnitId].Value = selectedUnitId;
                    dgvItemDet.Rows[CurrentIndex].Cells[iQohToSubtract].Value = (Convert.ToDecimal(itemSource[0].Field5).ToString("#.00"));

                    #endregion

                    #region display error if items more than qoh tried to buy

                    decimal PresentQoH = Convert.ToDecimal(dgvItemDet.Rows[CurrentIndex].Cells[iItemQOH].Value);
                    decimal qty = Convert.ToDecimal(dgvItemDet.Rows[CurrentIndex].Cells[iQuantity].Value);
                    decimal multiplier = Convert.ToDecimal(dgvItemDet.Rows[CurrentIndex].Cells[iQohToSubtract].Value);
                    if (PresentQoH < qty * multiplier)
                    {
                        if (Convert.ToInt16(dgvItemDet.Rows[CurrentIndex].Cells[iStockItem].Value) == 1)
                            MetroMessageBox.Show(this, "\nQuantity is above QOH.\nPlease update item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150);

                    }
                    dgvItemDet.Rows[CurrentIndex].Cells[iItemId].Selected = true;//to set focus after error message
                    #endregion

                    ItemTotal(CurrentIndex, Convert.ToDecimal(itemSource[0].Field4), iSalesRate);
                    TotalSummary();

                    #endregion
                }

            }
            catch (Exception ex)
            {


                MetroMessageBox.Show(this, ex.Message, "Error", 150);
            }
        }

        private void DgvItemDet_ComboBoxEventHandler(object sender, EventArgs e)//temp
                                                                                //  private void DgvItemDet_ComboBoxEventHandler(object sender, DataGridViewEditingControlShowingEventArgs e)//temp
        {
            try
            {

                ComboBox cmb = (ComboBox)sender;
                int selectedUnitId = 0;
                int CurrentIndex = dgvItemDet.CurrentRow.Index;
                if (cmb.SelectedValue != null)
                {
                    selectedUnitId = Convert.ToInt32(cmb.SelectedValue.ToString());

                    long selectedItemId = 0;// 10032;//temporarily value is set
                    selectedItemId = Convert.ToInt64(dgvItemDet.Rows[CurrentIndex].Cells[iItemId].Value);
                    int colIndex = dgvItemDet.CurrentCell.ColumnIndex;

                    if (iUnit == colIndex)
                    {


                        //var rowindex = dgvItemDet.CurrentCell.Value;
                        //string unitid = cnUnit.Selected.ToString();
                        //var value = dgvItemDet.Rows[0].Cells[0].Value;

                        #region set unit rate to the  grid view based on the unit selected

                        //  int CurrentIndex = dgvItemDet.CurrentRow.Index;
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();

                        DmnSearch objSearch = new DmnSearch();
                        objSearch = new DmnSearch();
                        objSearch.Select = " SellingPer as Field1, case MainUnitId when '" + selectedUnitId + "' then PurchaseRate1 ELSE PurchaseRate2 end AS Field2, " +
                            "case MainUnitId when '" + selectedUnitId + "' then MRP1" +
                            " ELSE MRP2 end AS Field3, " +
                            "case MainUnitId when '" + selectedUnitId + "' then PurchaseRate1*(100+SellingPer)/100  " +
                            "ELSE PurchaseRate2*(100+SellingPer)/100 end AS Field4";
                        objSearch.Table = " tblItem  ";
                        objSearch.WhereNdGroupBy = "where ItemId=" + selectedItemId;
                        objSearch.OrderBy = "";
                        var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);


                        // dgvItemDet.Rows[CurrentIndex].Cells[iSalesRate].Value = Convert.ToString(itemSource[0].Field4);
                        dgvItemDet.Rows[CurrentIndex].Cells[iSalesRate].Value = (Convert.ToDecimal(itemSource[0].Field4).ToString("#.00"));
                        //ItemTotal(CurrentIndex, Convert.ToDecimal(itemSource[0].Field4), iRate);
                        //TotalSummary();
                        #endregion
                        //  MetroMessageBox.Show(this, "hello", "Error", 150);
                    }
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", 150);
            }
        }


        public void ItemPOPup()
        {
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
            _objSearch.WhereNdGroupBy = " WHERE I.Status=0 And I.BranchId = " + BranchId;
            _objSearch.OrderBy = "order by I.SearchName ASC ";
            //  using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 1200, 400, 100, 100))
            using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 90, (this.Height / 2), this.Location.Y + 50, this.Location.X + 40, dtColumnAlign))

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
                    //if (Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iTotalAmount].Value) != string.Empty)
                    if (Convert.ToDecimal(dgvItemDet.Rows[CurrentIndex].Cells[iTotalAmount].Value) > 0)
                    {
                        dgvItemDet.Rows.Remove(dgvItemDet.Rows[CurrentIndex]);
                    }
                    dgvItemDet.Rows.Insert(CurrentIndex, 1);
                    ItemId = Convert.ToInt64(returnValue.ToString());
                    dgvItemDet.Rows[CurrentIndex].Cells[iQuantity].Value = 1;
                    Itemdetails(CurrentIndex, ItemId);
                    TotalSummary();

                    dgvItemDet.CurrentCell = dgvItemDet.Rows[CurrentIndex].Cells[iQuantity];
                }
            }
        }
        private void DgvItemDet_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.F8)
                {
                    ItemPOPup();
                    //DataTable dtColumnAlign = new DataTable();
                    //dtColumnAlign.Columns.Add();
                    //dtColumnAlign.Columns.Add();
                    //dtColumnAlign.Rows.Add("6", "R");
                    //dtColumnAlign.Rows.Add("7", "R");
                    //dtColumnAlign.Rows.Add("8", "R");
                    //dtColumnAlign.Rows.Add("9", "R");
                    //dtColumnAlign.Rows.Add("10", "R");
                    //string returnValue = string.Empty;
                    //DmnSearch _objSearch = new DmnSearch();
                    //_objSearch.ColumnHeaders = "Id,Code,Barcode,Name,Category,Unit,PurchaseRate,SalesRate,MRP,Tax(%),QOH";
                    //_objSearch.Select = "I.ItemId as Id,I.ItemCode as Field1,I.Barcode as Field2,I.SearchName as Field3,C.CategoryName as Field4,U.UnitName as Field5, I.PurchaseRate1 as Field6," +
                    //    " (I.PurchaseRate1*(100+I.SellingPer)/100) as Field7, I.MRP1 as Field8, I.IGSTPer as Field9, I.QuantityOnHand as Field10";
                    //_objSearch.Table = " tblItem I LEFT JOIN tblCategory C  ON I.CategoryId = C.CategoryId LEFT JOIN tblUnit U ON I.MainUnitId=U.UnitId";
                    //_objSearch.WhereNdGroupBy = " WHERE I.Status=0 ";
                    //_objSearch.OrderBy = "order by I.SearchName ASC ";
                    ////  using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 1200, 400, 100, 100))
                    //using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 90, (this.Height / 2), this.Location.Y + 50, this.Location.X + 40, dtColumnAlign))

                    //{
                    //    var dialogResult = search.ShowDialog();
                    //    if (dialogResult == DialogResult.OK)
                    //    {
                    //        returnValue = Convert.ToString(search._retValue);
                    //    }
                    //}
                    //if (returnValue != string.Empty)
                    //{

                    //    if (dgvItemDet.CurrentCell == null)
                    //    {
                    //        MetroMessageBox.Show(this, "Select Row!");
                    //    }
                    //    else
                    //    {
                    //        int CurrentIndex = dgvItemDet.CurrentRow.Index;
                    //        //if (Convert.ToString(dgvItemDet.Rows[CurrentIndex].Cells[iTotalAmount].Value) != string.Empty)
                    //        if (Convert.ToDecimal(dgvItemDet.Rows[CurrentIndex].Cells[iTotalAmount].Value) > 0)
                    //        {
                    //            dgvItemDet.Rows.Remove(dgvItemDet.Rows[CurrentIndex]);
                    //        }
                    //        dgvItemDet.Rows.Insert(CurrentIndex, 1);
                    //        ItemId = Convert.ToInt64(returnValue.ToString());
                    //        dgvItemDet.Rows[CurrentIndex].Cells[iQuantity].Value = 1;
                    //        Itemdetails(CurrentIndex, ItemId);
                    //        TotalSummary();

                    //        dgvItemDet.CurrentCell = dgvItemDet.Rows[CurrentIndex].Cells[iQuantity];
                    //    }
                    //}
                }
                if (dgvItemDet.CurrentCell == null)
                {
                    MetroMessageBox.Show(this, "Select Row!");
                }
                else
                {
                    int colIndex = dgvItemDet.CurrentCell.ColumnIndex;
                    if (e.KeyCode == Keys.Enter)
                    {
                        dgvItemDet.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
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

        private void MtxtTotDiscPer_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(mtxtGrandTot.Text) > 0)
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
                    discAmount = ((discountPer * (grandTot + misc)) / 100);
                    mtxtTotDiscAmnt.Text = Convert.ToString(discAmount.ToString("#.00"));
                    TotalSummary();
                    if (e.KeyCode == Keys.Enter)
                    {
                        this.ActiveControl = mtxtTotDiscAmnt;
                    }
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
                if (Convert.ToDecimal(mtxtGrandTot.Text) > 0)
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
                    discountPer = ((discAmount / (grandTot + misc)) * 100);
                    mtxtTotDiscPer.Text = Convert.ToString(discountPer);
                    TotalSummary();
                    if (e.KeyCode == Keys.Enter)
                    {
                        this.ActiveControl = mtxtAdjust;
                    }
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
                if (e.KeyCode == Keys.Enter)
                {
                    this.ActiveControl = mtxtNetAmount;
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void MbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(mtxtAmount.Text) != 0 && mcmbParticular.SelectedIndex != 0)
                {

                    string[] arr = new string[4];
                    ListViewItem itm;
                    //add items to ListView

                    arr[0] = (mlvMisc.Items.Count + 1).ToString();
                    arr[1] = mcmbParticular.Text;
                    //  arr[2] = Convert.ToDecimal(mtxtAmount.Text).ToString("#.00");
                    arr[2] = mcmbSign.SelectedItem + mtxtAmount.Text;
                    arr[3] = mcmbParticular.SelectedValue.ToString();
                    itm = new ListViewItem(arr);
                    mlvMisc.Items.Add(itm);
                    // mlvMisc.Columns[3].Width = 0;
                    miscTotal = Convert.ToDecimal(miscTotal + Convert.ToDecimal(arr[2]));
                    mtxtMiscCharges.Text = Convert.ToString(miscTotal.ToString("0.00"));
                    TotalSummary();
                    mtxtAmount.Text = "0.00";
                    mcmbParticular.SelectedIndex = 0;

                }
                else
                {
                    MetroMessageBox.Show(this, "Particulars and amount are  empty or zero!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
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
            string returnValue = string.Empty;

            using (var search = new FrmListSalesReturn())
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
                hlblHeader.Text = "Sales Return[Edit] \t[User : " + Username + "]";

                //       hlblHeader.Text = "Sales Invoice [Edit]";
                SalesReturnMasterId = Convert.ToInt32(returnValue.ToString());
                BllSearch<DmnSalesReturnMaster> bllSearch = new BllSearch<DmnSalesReturnMaster>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "*";

                objSearch.Table = " tblSalesReturnMaster S Left JOIN tblAccountLedger L ON S.LedgerId=L.LedgerId ";

                objSearch.WhereNdGroupBy = "Where S.SalesReturnMasterId = " + SalesReturnMasterId;
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                //if (gridSource[0].LedgerId != 0)
                if (gridSource.Count > 0)
                {
                    #region Filling customer details in groupbox


                    BllSearch<DmnAccountLedger> bllLedgerSearch = new BllSearch<DmnAccountLedger>();
                    objSearch = new DmnSearch();
                    objSearch.Select = "L.LedgerName,L.LedgerAddress,L.Mobile,L.IcardNo";
                    objSearch.Table = " tblAccountLedger L ";
                    objSearch.WhereNdGroupBy = "Where L.LedgerId =" + gridSource[0].LedgerId;

                    var ledgerSource = bllLedgerSearch.GetSearchData("prdSearch", objSearch);
                    if (ledgerSource.Count > 0)
                    {
                        mtxtLedger.Text = Convert.ToString(ledgerSource[0].LedgerName);
                        LedgerAddress = Convert.ToString(ledgerSource[0].LedgerAddress);
                        mtxtMobile.Text = Convert.ToString(ledgerSource[0].Mobile);
                        mtxtiCardNo.Text = Convert.ToString(ledgerSource[0].ICardNo);
                    }
                    #endregion
                }

                mtxtAmountReceived.Text = Convert.ToString(gridSource[0].PaidAmount);
                mtxtBalanceAmount.Text = Convert.ToString(gridSource[0].BalanceAmount);
                mtxtInvNo.Text = Convert.ToString(gridSource[0].EntryNo);
                mdtpInvDate.Text = Convert.ToString(gridSource[0].EntryDate);
                mcmbTax.SelectedValue = gridSource[0].EType;
                mcmbMode.SelectedValue = gridSource[0].PaymentMode;
                LedgerId = Convert.ToInt64(gridSource[0].LedgerId);
                CustomerId = Convert.ToInt64(gridSource[0].CustomerId);
                LedgerCreditDays = Convert.ToInt16(gridSource[0].CreditDays);
                LedgerDueDate = Convert.ToDateTime(gridSource[0].DueDate);
                EmployeeId = Convert.ToInt64(gridSource[0].EmployeeId);
                mtxtGrandTot.Text = Convert.ToString(gridSource[0].TotalAmount);
                mtxtTotDiscPer.Text = Convert.ToString(gridSource[0].DiscountPer);
                mtxtTotDiscAmnt.Text = Convert.ToString(gridSource[0].DiscountAmount);//total disount given to net amount below grid
                mtxtMiscCharges.Text = Convert.ToString(gridSource[0].TotalMiscCharges);
                miscTotal = Convert.ToDecimal(mtxtMiscCharges.Text);// if this not given it arise an error 
                mtxtRemarks.Text = Convert.ToString(gridSource[0].Remarks);
                mtxtNetAmount.Text = Convert.ToString(gridSource[0].TotalNetAmount);
                mtxtAdjust.Text = Convert.ToString(gridSource[0].AmountAdjust);
                //  mtxtTotalSavings.Text = Convert.ToString(gridSource[0].TotalSaveAmount);
                // TotalSaveAmount = Convert.ToDecimal(gridSource[0].TotalSaveAmount);
                TotalSaveAmount = Convert.ToDecimal(gridSource[0].TotalSaveAmount);

                #region EmployeeName Filling

                //objSearch = new DmnSearch();
                //objSearch.Select = "EmployeeName";
                //objSearch.Table = " tblEmployee  ";
                //objSearch.WhereNdGroupBy = "Where EmployeeId = " + Convert.ToInt64(gridSource[0].EmployeeId);

                //BllSearch<DmnEmployee> bllEmployeeName = new BllSearch<DmnEmployee>();
                //var employeedetails = bllEmployeeName.GetSearchData("prdSearch", objSearch);
                //if (employeedetails.Count > 0)
                //{
                // //   mtxtEmployeeName.Text = employeedetails[0].EmployeeName;
                //}
                #endregion


                #region Miscallenious charge filling

                BllSearch<DmnGridFields> bllMiscSearch = new BllSearch<DmnGridFields>();
                objSearch = new DmnSearch();
                objSearch.Select = " SM.SalesReturnMiscId as Id,G.Caption as Field1,G.GeneralId as Field2 ,SM.MiscAmount as Field3 ";
                objSearch.Table = " tblSalesReturnMiscCharges SM left join tblGeneralOptions G on SM.GeneralId=G.GeneralId  ";
                objSearch.WhereNdGroupBy = " where SM.SalesReturnMasterId = " + SalesReturnMasterId;
                objSearch.OrderBy = "Order By SM.SalesReturnMasterId ";
                var gridMisc = bllMiscSearch.GetSearchData("prdSearch", objSearch);
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
                    arr[1] = Convert.ToString(gridMisc[i].Field1);
                    arr[2] = Convert.ToString(gridMisc[i].Field3);
                    arr[3] = Convert.ToString(gridMisc[i].Id);
                    itm = new ListViewItem(arr);
                    mlvMisc.Items.Add(itm);
                }

                #region temp
                //BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();

                //DmnSearch objSearch = new DmnSearch();


                //DmnSearch _objSearch = new DmnSearch();
                //_objSearch.ColumnHeaders = "Id,First Name,Last Name,Mobile,Address,ICard No";
                //_objSearch.Select = "C.CustomerId as Id,C.FirstName as Field1,C.LastName as Field2,C.Mobile as Field3,C.CustomerAddress as  Field4,C.ICardNo as Field5";
                //_objSearch.Table = " tblCustomerPersonal C ";
                //_objSearch.WhereNdGroupBy = " WHERE C.Status = 0 And C.BranchId = " + BranchId;
                // _objSearch.OrderBy = " ";
                #endregion


                #endregion

                #region grid filling in edit


                objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = " tblSalesReturnDetails S LEFT JOIN tblItem I ON S.ItemId=I.ItemId LEFT JOIN tblUnit U ON S.UnitId=U.UnitId ";
                objSearch.WhereNdGroupBy = "Where S.SalesReturnMasterId = " + SalesReturnMasterId;

                BllSearch<DmnSalesReturnDetails> bllSalesDetailsSearch = new BllSearch<DmnSalesReturnDetails>();
                var gridSourceItem = bllSalesDetailsSearch.GetSearchData("prdSearch", objSearch);
                dgvItemDet.Rows.Add(gridSourceItem.Count);

                for (int i = 0; i < (gridSourceItem.Count); i++)
                {
                    var lst = SelNegativeAllowed();
                    if (lst == 1)
                    {
                        dgvItemDet.Rows[i].Cells[iSlNo].Value = (i + 1).ToString("#");
                        dgvItemDet.Rows[i].Cells[iBarcode].Value = Convert.ToString(gridSourceItem[i].ItemBarCode);
                        dgvItemDet.Rows[i].Cells[iUnit].Value = Convert.ToString(gridSourceItem[i].UnitName);
                        #region Combo

                        FillGridUnitCombo(Convert.ToInt64(gridSourceItem[i].ItemId), i);

                        #endregion

                        dgvItemDet.Rows[i].Cells[iItemId].Value = Convert.ToString(gridSourceItem[i].ItemId);

                        #region display item name 

                        BllSearch<DmnItem> bllSearchItemName = new BllSearch<DmnItem>();
                        objSearch = new DmnSearch();
                        objSearch = new DmnSearch();
                        objSearch.Select = "ItemName,LockSaleRate,SellingPer,SalesTaxIncl,QuantityOnHand,StockItem, " +
                            "case MainUnitId when '" + gridSourceItem[i].UnitId + "' then 1 Else PacketQty  END as PacketQty";
                        objSearch.Table = " tblItem ";
                        objSearch.WhereNdGroupBy = " where ItemId = " + Convert.ToInt64(gridSourceItem[i].ItemId);
                        var itemName = bllSearchItemName.GetSearchData("prdSearch", objSearch);
                        dgvItemDet.Rows[i].Cells[iName].Value = itemName[0].ItemName;
                        dgvItemDet.Rows[i].Cells[iLockSaleRate].Value = itemName[0].LockSaleRate;
                        dgvItemDet.Rows[i].Cells[iSellingPer].Value = itemName[0].SellingPer;
                        dgvItemDet.Rows[i].Cells[iSalesTaxIncl].Value = Convert.ToString(itemName[0].SalesTaxIncl);
                        dgvItemDet.Rows[i].Cells[iQohToSubtract].Value = Convert.ToString(itemName[0].PacketQty);
                        dgvItemDet.Rows[i].Cells[iItemQOH].Value = Convert.ToString(itemName[0].QuantityOnHand);
                        dgvItemDet.Rows[i].Cells[iStockItem].Value = Convert.ToString(itemName[0].StockItem);
                        #endregion

                        dgvItemDet.Rows[i].Cells[iItemCode].Value = Convert.ToString(gridSourceItem[i].ItemCode);



                        // dgvItemDet.Rows[i].Cells[iSalesDetailsId].Value = Convert.ToString(gridSourceItem[i].SalesDetailsId);





                        dgvItemDet.Rows[i].Cells[iUnitId].Value = Convert.ToString(gridSourceItem[i].UnitId);//need to hange unit id tounit name  also ned to bind combo box 
                        DataGridViewComboBoxCell cel = (DataGridViewComboBoxCell)dgvItemDet.Rows[i].Cells[iUnit];

                        cel.Value = gridSourceItem[i].UnitId;

                        dgvItemDet.Rows[i].Cells[iSalesReturnDetailsId].Value = Convert.ToString(gridSourceItem[i].SalesReturnDetailsId);
                        dgvItemDet.Rows[i].Cells[iQuantity].Value = Convert.ToString(gridSourceItem[i].Quantity);
                        dgvItemDet.Rows[i].Cells[iRate].Value = Convert.ToString(gridSourceItem[i].IRate);
                        dgvItemDet.Rows[i].Cells[iSalesRate].Value = Convert.ToString(gridSourceItem[i].SalesRate);
                        dgvItemDet.Rows[i].Cells[iMRP].Value = Convert.ToString(gridSourceItem[i].MRP);
                        dgvItemDet.Rows[i].Cells[iDiscPer].Value = Convert.ToString(gridSourceItem[i].DiscountPer);
                        dgvItemDet.Rows[i].Cells[iDiscAmount].Value = Convert.ToString(gridSourceItem[i].DiscountAmount);

                        #region new 
                        dgvItemDet.Rows[i].Cells[iAmount].Value = Convert.ToString(gridSourceItem[i].Amount);
                        dgvItemDet.Rows[i].Cells[iGrossAmount].Value = Convert.ToString(gridSourceItem[i].GrossAmount);
                        //dgvItemDet.Rows[i].Cells[iSalesTaxIncl].Value = Convert.ToString(gridSourceItem[i].SalesTaxIncl);
                        dgvItemDet.Rows[i].Cells[iTaxPer].Value = Convert.ToString(gridSourceItem[i].TaxPer);
                        dgvItemDet.Rows[i].Cells[iTaxAmount].Value = Convert.ToString(gridSourceItem[i].TaxAmount);
                        dgvItemDet.Rows[i].Cells[iTaxType].Value = Convert.ToString(gridSourceItem[i].TaxType);
                        dgvItemDet.Rows[i].Cells[iCessPer].Value = Convert.ToString(gridSourceItem[i].CessPer);
                        dgvItemDet.Rows[i].Cells[iCessAmount].Value = Convert.ToString(gridSourceItem[i].CessAmount);
                        dgvItemDet.Rows[i].Cells[iTotalAmount].Value = Convert.ToString(gridSourceItem[i].ItemNetAmount);
                        //dgvItemDet.Rows[i].Cells[iSellingPer].Value = Convert.ToString(gridSourceItem[i].CessAmount);
                        dgvItemDet.Rows[i].Cells[iItemSaveAmount].Value = Convert.ToString(gridSourceItem[i].TotalSaveAmount);//totalsave amount in details domain shows individual items save amount
                        #endregion
                        decimal taxamt = Convert.ToDecimal(gridSourceItem[i].TaxAmount.ToString("0.00"));
                        decimal cessamt = Convert.ToDecimal(gridSourceItem[i].CessAmount.ToString("0.00"));
                        dgvItemDet.Rows[i].Cells[iTotalTaxWithCess].Value = Convert.ToString((taxamt + cessamt).ToString("0.00"));

                        ItemTotal(i, gridSourceItem[i].Quantity, iQuantity);
                    }

                }
                TotalSummary();

                #endregion
            }
        }
        private void MtxtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.DecimalTextBoxSign(sender, e);//same function for alldecimal text boxes with sign
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }



        private void MtxtAdjust_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                objGeneral.DecimalTextBoxSign(sender, e);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }




        private void MbtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form openForm in Application.OpenForms)
                {
                    if (string.Compare(openForm.TopLevelControl.Name, "FrmDashboard") == 0)
                    {
                        this.Close();
                        openForm.Activate();
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void TmrClock_Tick(object sender, EventArgs e)
        {
            try
            {
                mlDateTime.Text = DateTime.Now.ToLongDateString() + "    " + DateTime.Now.ToLongTimeString();

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }



        private void dgvItemDet_PreviewKeyDown_1(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.F8)
                {
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
                    _objSearch.WhereNdGroupBy = " WHERE I.Status=0 ";
                    _objSearch.OrderBy = " order by I.SearchName Asc ";
                    //   using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 1200, 400, 100, 100))
                    //  using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 90, (this.Height - 120), this.Location.Y + 50, this.Location.X + 100, dtColumnAlign))
                    //  using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 90, (this.Height / 2), this.Location.Y + 50, this.Location.X + 100, dtColumnAlign))
                    using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 90, (this.Height / 2), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))

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
                            if (Convert.ToDecimal(dgvItemDet.Rows[CurrentIndex].Cells[iTotalAmount].Value) > 0)
                            {
                                dgvItemDet.Rows.Remove(dgvItemDet.Rows[CurrentIndex]);

                            }
                            dgvItemDet.Rows.Insert(CurrentIndex, 1);
                            ItemId = Convert.ToInt64(returnValue.ToString());
                            // FillGridUnitCombo(ItemId, CurrentIndex);//added to get unit diaplayed in f8
                            //    dgvItemDet.Rows[CurrentIndex].Cells[iQuantity].Value = 1;
                            Itemdetails(CurrentIndex, ItemId);
                            dgvItemDet.Rows[CurrentIndex].Cells[iQuantity].Value = 1;
                            TotalSummary();

                            dgvItemDet.CurrentCell = dgvItemDet.Rows[CurrentIndex].Cells[iQuantity];
                            dgvItemDet.Rows[CurrentIndex + 1].Cells[iSlNo].Value = "0";
                            dgvItemDet.Rows[CurrentIndex + 1].Cells[iQuantity].Value = "0";
                        }
                    }
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
                            //mtxtTotDiscAmnt.Focus();
                            //ActiveControl = mtxtTotDiscAmnt;
                            mtxtTotDiscPer.Focus();
                        }
                    }
                    #endregion

                }

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

                    LedgerSearchPopUp();
                }

                if (e.KeyCode == Keys.Delete)
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        LedgerId = 0;
                        mtxtiCardNo.Clear();
                        mtxtLedger.Clear();
                        LedgerAddress = string.Empty;
                        existingMobile = string.Empty;
                        existingAddress = string.Empty;
                        mtxtMobile.Clear();
                        LedgerCreditDays = 0;
                        LedgerDueDate = DateTime.Now.Date;
                    }
                }

                else if (e.KeyCode == Keys.Enter)
                {
                    this.ActiveControl = mcmbMode;
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
                //objGeneral.DecimalTextBox(sender, e);//same function for alldecimal text boxes
                objGeneral.DecimalTextBox3digits(sender, e);//same function for alldecimal text boxes
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                mpnlPayment.Hide();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtAmountReceived_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mtxtpnlNetAmount.Text) && !string.IsNullOrWhiteSpace(mtxtAmountReceived.Text) && mtxtAmountReceived.Text != ".")//also need to check for .
                {
                    mtxtBalanceAmount.Text = (Convert.ToDecimal(mtxtAmountReceived.Text) - Convert.ToDecimal(mtxtpnlNetAmount.Text)).ToString();
                }
                else
                {
                    mtxtBalanceAmount.Text = "0.00";
                }
                if (e.KeyCode == Keys.Enter)
                {
                    this.ActiveControl = mbtnSavePrint;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtAmountReceived_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.DecimalTextBox3digits(sender, e);//same function for alldecimal text boxes
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtiCardNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ClearCustomerDetails();

                if (!string.IsNullOrWhiteSpace(mtxtiCardNo.Text))
                {

                    BllSearch<DmnAccountLedger> bllSearch = new BllSearch<DmnAccountLedger>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.Select = "L.LedgerId,L.ledgername,L.Mobile,L.LedgerAddress,L.creditdays";
                    objSearch.Table = " tblaccountledger L Left join tblAccountGroup A on L.AccountGroupId=A.AccountGroupId ";
                    objSearch.WhereNdGroupBy = "where A.AccountGroupName='Customer' and  L.BranchId='" + BranchId + "'  and L.Status=0 and L.ICardNo='" + mtxtiCardNo.Text.Trim() + "'";
                    var customerlist = bllSearch.GetSearchData("prdSearch", objSearch);

                    if (customerlist.Count > 0)
                    {
                        mtxtLedger.Text = customerlist[0].LedgerName;
                        LedgerAddress = customerlist[0].LedgerAddress;
                        existingAddress = customerlist[0].LedgerAddress;
                        existingMobile = customerlist[0].Mobile;
                        mtxtMobile.Text = customerlist[0].Mobile;
                        LedgerCreditDays = Convert.ToInt16(customerlist[0].CreditDays);
                        LedgerDueDate = DateTime.Now.AddDays(LedgerCreditDays);
                        LedgerId = customerlist[0].LedgerId;

                    }

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mbtnSavePrint_Click(object sender, EventArgs e)
        {
            //  MessageBox.Show(TotalSaveAmount.ToString());
            try
            {


                #region Update Address and Mobile in ledger

                //DmnAccountLedger dmnLegder = new DmnAccountLedger();
                //BllAccountLedger bllLedger = new BllAccountLedger();
                //bool changed = false;
                //if (string.Compare(LedgerAddress, existingAddress) != 0)
                //{
                //    changed = true;
                //}
                //if (string.Compare(mtxtMobile.Text.Trim(), existingMobile) != 0)
                //{
                //    changed = true;
                //}

                //if (changed)
                //{
                //    if (LedgerId != 0)
                //    {
                //        DialogResult result = MetroMessageBox.Show(this, "\nContact details modified.\nDo you want to update in database?", "Update ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, 150);

                //        if (result == DialogResult.OK)
                //        {

                //            dmnLegder.LedgerId = LedgerId;
                //            dmnLegder.LedgerAddress = mtxtAddress.Text.Trim();
                //            dmnLegder.Mobile = mtxtMobile.Text.Trim();
                //            dmnLegder.ModifiedBy = UserId;
                //            var status = bllLedger.UpdateLedgerAddress_Mobile("prdUpdateLedgerAddress_Mobile", dmnLegder);


                //        }
                //    }
                //}



                #endregion


                long savestatus;
                DmnSalesReturnMaster dmnSaleseMaster = new DmnSalesReturnMaster();
                BllSalesReturnMaster bllSalesMaster = new BllSalesReturnMaster();
                string cgst = Convert.ToString((TotTax) / 2);
                dmnSaleseMaster.EType = Convert.ToInt32(mcmbTax.SelectedValue);
                dmnSaleseMaster.EntryNo = mtxtInvNo.Text;//invoice number. need to call a function to fetch latest inv no
                dmnSaleseMaster.EntryDate = Convert.ToDateTime(mdtpInvDate.Text);


                dmnSaleseMaster.CustomerId = 0;
                dmnSaleseMaster.LedgerId = LedgerId;
                //}
                dmnSaleseMaster.SalesAddress = Convert.ToString(LedgerAddress);
                dmnSaleseMaster.PaymentMode = Convert.ToInt32(mcmbMode.SelectedValue);
                dmnSaleseMaster.Telephone = Convert.ToString(mtxtMobile.Text);
                dmnSaleseMaster.Remarks = mtxtRemarks.Text.Trim();
                dmnSaleseMaster.TotalGross = TotGross;//decimal.Parse(mtxtTotGrss.Text == string.Empty ? "0.00" : mtxtTotGrss.Text);
                dmnSaleseMaster.TotalAmount = TotAmount;//decimal.Parse(mtxtTotAmount.Text == string.Empty ? "0.00" : mtxtTotAmount.Text);
                dmnSaleseMaster.TotalDiscount = TotDisc;//decimal.Parse(mtxtTotDisc.Text == string.Empty ? "0.00" : mtxtTotDisc.Text);///total of grid
                dmnSaleseMaster.TotalTax = TotTax;// decimal.Parse(mtxtTotTax.Text == string.Empty ? "0.00" : mtxtTotTax.Text);
                dmnSaleseMaster.TotalIGST = TotTax;// decimal.Parse(mtxtTotTax.Text == string.Empty ? "0.00" : mtxtTotTax.Text);
                dmnSaleseMaster.TotalCGST = decimal.Parse(cgst == string.Empty ? "0.00" : cgst);
                dmnSaleseMaster.TotalSGST = decimal.Parse(cgst == string.Empty ? "0.00" : cgst);
                dmnSaleseMaster.TotalCess = TotCess;// decimal.Parse(mtxtTotCess.Text == string.Empty ? "0.00" : mtxtTotCess.Text);
                dmnSaleseMaster.DiscountPer = decimal.Parse(mtxtTotDiscPer.Text == string.Empty ? "0.00" : mtxtTotDiscPer.Text);
                dmnSaleseMaster.DiscountAmount = decimal.Parse(mtxtTotDiscAmnt.Text == string.Empty ? "0.00" : mtxtTotDiscAmnt.Text);
                dmnSaleseMaster.NetAmount = TotNet;// decimal.Parse(mtxtTot.Text == string.Empty ? "0.00" : mtxtNetAmount.Text);
                dmnSaleseMaster.PaidAmount = decimal.Parse(mtxtAmountReceived.Text == string.Empty ? mtxtNetAmount.Text : mtxtAmountReceived.Text);//if paid amont is zero by defalt net amount will be saved

                dmnSaleseMaster.BalanceAmount = decimal.Parse(mtxtBalanceAmount.Text == string.Empty ? "0.00" : mtxtBalanceAmount.Text);
                dmnSaleseMaster.AmountAdjust = decimal.Parse(mtxtAdjust.Text == string.Empty ? "0.00" : mtxtAdjust.Text);

                dmnSaleseMaster.EmployeeId = EmployeeId;
                dmnSaleseMaster.TotalMiscCharges = decimal.Parse(mtxtMiscCharges.Text == string.Empty ? "0.00" : mtxtMiscCharges.Text);
                dmnSaleseMaster.ICardNo = mtxtiCardNo.Text.Trim();
                dmnSaleseMaster.CreditDays = LedgerCreditDays;
                dmnSaleseMaster.DueDate = Convert.ToDateTime(LedgerDueDate);
                dmnSaleseMaster.BranchId = BranchId;
                dmnSaleseMaster.FinancialYearId = FinancialYearId;
                dmnSaleseMaster.TotalQuantity = TotQuantity;//decimal.ToInt32(Convert.ToDecimal(mtxtNoOfItems.Text));
                dmnSaleseMaster.TotalNetAmount = decimal.Parse(mtxtNetAmount.Text == string.Empty ? "0.00" : mtxtNetAmount.Text);
                // dmnSaleseMaster.TotalSaveAmount = decimal.Parse(mtxtTotalSavings.Text == string.Empty ? "0.00" : mtxtTotalSavings.Text); ///calculate total save amount
                dmnSaleseMaster.TotalSaveAmount = TotalSaveAmount;// decimal.Parse(mtxtTotalSavings.Text == string.Empty ? "0.00" : mtxtTotalSavings.Text);
                dmnSaleseMaster.DeviceDescription = hostName + "__" + Dns.GetHostByName(hostName).AddressList[0].ToString();
                long MasterId = 0;//to hold the master id for ther tables
                string BillNo = string.Empty;
                if (SalesReturnMasterId == 0)//insert
                {
                    dmnSaleseMaster.CreatedBy = UserId;

                    var lstData = bllSalesMaster.AddData("prdInsertSalesReturnMaster", dmnSaleseMaster);
                    savestatus = lstData.SalesReturnMasterId;
                    BillNo = lstData.EntryNo;
                    if (lstData.SalesReturnMasterId != 0)
                    {
                        MasterId = lstData.SalesReturnMasterId;
                        InsertSalesDet(MasterId);
                        InsertMiscCharges(MasterId);
                        //  UpdateBranchSalesSrlNo();
                    }
                    if (savestatus != 0)
                    {
                       // MetroMessageBox.Show(this, "Successfully saved. Bill No:" + BillNo, "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        DialogResult result = MetroMessageBox.Show(this, "Successfully saved. Bill No:" + BillNo, "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        if (result == DialogResult.OK)
                        {
                           RefreshForm();
                            #region Print bill
                            //FrmSalesBill objBill = new FrmSalesBill(SalesMasterId);

                            //objBill.ShowDialog();
                            #endregion

                            #region set focus to barcode 

                            ActiveControl = dgvItemDet;

                            dgvItemDet.CurrentCell = dgvItemDet.Rows[0].Cells[3];
                            dgvItemDet.CurrentCell.Selected = true;
                            dgvItemDet.BeginEdit(true);
                            #endregion
                        }


                    }
                    else
                    {
                        MetroMessageBox.Show(this, "Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    }
                }
                else   ////update

                {
                    bool status;
                    MasterId = SalesReturnMasterId;// need to check reveerse is correct or no6t
                    dmnSaleseMaster.ModifiedBy = UserId;
                    dmnSaleseMaster.SalesReturnMasterId = SalesReturnMasterId;
                    //  var lstData = bllSalesMaster.UpdateData("prdUpdateSalesMaster", dmnSaleseMaster);

                    BllSearch<DmnSalesReturnDetails> bllSearch = new BllSearch<DmnSalesReturnDetails>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "S.ItemId,S.SalesReturnDetailsId,S.UnitId ";
                    objSearch.Table = " tblsalesReturnDetails S  ";
                    objSearch.WhereNdGroupBy = "Where S.SalesReturnMasterId = '" + SalesReturnMasterId + "'";
                    objSearch.OrderBy = "Order By S.SalesReturnDetailsId ";
                    var det = bllSearch.GetSearchData("prdSearch", objSearch);

                    #region this is to update qoh




                    DmnSalesReturnDetails dmnSalesDet = new DmnSalesReturnDetails();
                    for (int i = 0; i < det.Count; i++)
                    {
                        var lst = SelNegativeAllowed();
                        if (lst == 1)
                        {

                            dmnSalesDet.SalesReturnDetailsId = det[i].SalesReturnDetailsId;
                            dmnSalesDet.ItemId = det[i].ItemId;

                            #region to find quantity to reduce/add

                            BllSearch<DmnGridFields> bllQunatitytoReduceSearch = new BllSearch<DmnGridFields>();

                            DmnSearch objQtySearch = new DmnSearch();
                            objQtySearch = new DmnSearch();
                            objQtySearch.Select = " ItemId as Id,case MainUnitId when '" + det[i].UnitId + "' then 1 " +
                                "Else PacketQty end as Field1,StockItem as Field2";
                            objQtySearch.Table = " tblItem  ";
                            objQtySearch.WhereNdGroupBy = "where ItemId=" + det[i].ItemId;
                            var qtySource = bllQunatitytoReduceSearch.GetSearchData("prdSearch", objQtySearch);
                            dmnSalesDet.QuantitytoSubtract = Convert.ToDecimal(qtySource[0].Field1);


                            #endregion


                            if (Convert.ToInt32(qtySource[0].Field2) == 1)//update qoh only if it is a stock item
                            {
                                var UpdateQohLst = bllSalesMaster.UpdateQoh("prdUpdateQohAfterSalesReturn", dmnSalesDet);
                            }
                        }
                    }
                    #endregion


                    DeleteSalesDetails(MasterId);
                    InsertSalesDet(MasterId);
                    DeleteMiscCharges(MasterId);
                    InsertMiscCharges(MasterId);
                    var lstDataUpdate = bllSalesMaster.UpdateData("prdUpdateSalesReturnMaster", dmnSaleseMaster); //status = lstData;
                    status = lstDataUpdate;
                    if (status)
                    {
                        MetroMessageBox.Show(this, "Updated successfully. ", "Success  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        RefreshForm();
                        #region Print bill
                        //FrmSalesBill objBill = new FrmSalesBill(SalesMasterId);
                        //objBill.ShowDialog();


                        #endregion
                        ActiveControl = dgvItemDet;

                        dgvItemDet.CurrentCell = dgvItemDet.Rows[0].Cells[3];
                        dgvItemDet.CurrentCell.Selected = true;
                        dgvItemDet.BeginEdit(true);
                    }
                    else
                        MetroMessageBox.Show(this, "Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                }

               // RefreshForm();


                //ActiveControl = dgvItemDet;

                //dgvItemDet.CurrentCell = dgvItemDet.Rows[0].Cells[3];
                //dgvItemDet.CurrentCell.Selected = true;
                //dgvItemDet.BeginEdit(true);

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
                //Enter key


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

                    }
                    else if (colIndex == iQuantity)
                    {
                        if (dgvItemDet.Rows.Count > currentrow)
                        {
                            currentrow = dgvItemDet.CurrentCell.RowIndex;
                            dgvItemDet.CurrentCell = dgvItemDet.Rows[currentrow].Cells[iBarcode];
                        }
                    }
                    else if (colIndex == iUnit)//when enter is pressed from unit tyhen also need to focus to quantity
                    {
                        if (currentrow < 0)// added to avoid error if barcode is empty and enter pressed
                        {
                            currentrow = 0;
                        }
                        dgvItemDet.CurrentCell = dgvItemDet.Rows[currentrow].Cells[iQuantity];
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
                e.SuppressKeyPress = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtNetAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mtxtpnlNetAmount.Text = mtxtNetAmount.Text;
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

        private void mtxtEmployeeName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    EmployeeListPopUp();
                }
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

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtiCardNo_Enter(object sender, EventArgs e)
        {
            //try
            //{
            //    if (string.IsNullOrWhiteSpace(mtxtiCardNo.Text))
            //    {

            //        LedgerSearchPopUp();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            //}
        }

        private void mtxtMobile_KeyPress(object sender, KeyPressEventArgs e)
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



        private void FrmSalesPOS_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    MbtnSave_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F3)
                {
                    MbtnList_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F5)
                {
                    RefreshForm();
                }
                else if (e.KeyCode == Keys.F6)
                {
                    MbtnClose_Click(sender, e);
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }

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
                    if (iUnit == colIndex)
                    {

                        #region method 1
                        ComboBox comboBox = e.Control as ComboBox;
                        //comboBox.SelectedIndexChanged -= DgvItemDet_ComboBoxEventHandler;
                        //comboBox.SelectedIndexChanged += DgvItemDet_ComboBoxEventHandler;
                        // comboBox.DroppedDown = true;//to make combo box listed when focus comes
                        comboBox.SelectedIndexChanged -= LastColumnComboSelectionChanged;
                        comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
                        #endregion                      
                    }

                }



                //DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
                //tb.KeyUp -= new KeyEventHandler(DgvItemDet_KeyEventHandler);
                //tb.KeyUp += new KeyEventHandler(DgvItemDet_KeyEventHandler);
                //tb.PreviewKeyDown -= new PreviewKeyDownEventHandler(DgvItemDet_PreviewKeyDown);
                //tb.PreviewKeyDown += new PreviewKeyDownEventHandler(DgvItemDet_PreviewKeyDown);

                e.Control.PreviewKeyDown -= new PreviewKeyDownEventHandler(DgvItemDet_PreviewKeyDown);
                e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(DgvItemDet_PreviewKeyDown);
                e.Control.KeyUp -= new KeyEventHandler(DgvItemDet_KeyEventHandler);
                e.Control.KeyUp += new KeyEventHandler(DgvItemDet_KeyEventHandler);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }
        private void DgvItemDet_KeyEventHandler(object sender, KeyEventArgs e)
        {
            try
            {
                _last = (Control)sender;

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

                        #region display error if items more than qoh tried to buy

                        decimal PresentQoH = Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iItemQOH].Value);

                        decimal multiplier = Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iQohToSubtract].Value);

                        if (PresentQoH < qty * multiplier)
                        {

                            if (Convert.ToInt16(dgvItemDet.Rows[currentrow].Cells[iStockItem].Value) == 1)

                            {

                                DialogResult result = MetroMessageBox.Show(this, "\nQuantity  is above QOH.\nPlease update item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150);

                                if (result == DialogResult.OK)
                                {

                                    dgvItemDet.CurrentCell = dgvItemDet.Rows[currentrow].Cells[iQuantity];

                                    //    dgvItemDet.CurrentCell = dgvItemDet[currentrow, iQuantity];
                                    dgvItemDet.Rows[currentrow].Cells[iQuantity].Selected = true;//to set the focis after error message
                                    _last.Focus();


                                }
                            }


                        }



                        #endregion

                        ItemTotal(currentrow, qty, colIndex);
                        discountPer = Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iDiscPer].Value);
                        DiscountPerToAmt(currentrow, discountPer);
                        TotalSummary();
                    }
                    if ((iSalesRate == colIndex) && Convert.ToString(value) != "0.00" && !string.IsNullOrWhiteSpace(Convert.ToString(value)))
                    {
                        colIndex = iSalesRate;
                        decimal rate = decimal.Parse(Convert.ToString(value) == string.Empty ? "0.00" : Convert.ToString(value));

                        if (rate == Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iMRP].Value))
                        {


                            dgvItemDet.CurrentRow.DefaultCellStyle.BackColor = Color.LightSalmon;

                        }

                        else
                        {
                            dgvItemDet.CurrentRow.DefaultCellStyle.BackColor = Color.White;
                        }
                        if (rate > Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iMRP].Value))
                        {
                            MetroMessageBox.Show(this, "Rate cannot be greater than MRP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                            rate = SalesRatemrp;
                        }
                        dgvItemDet.Rows[currentrow].Cells[iSalesRate].Value = Convert.ToString(rate.ToString("0.00"));
                        ItemTotal(currentrow, rate, colIndex);
                        discountPer = Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iDiscPer].Value);
                        DiscountPerToAmt(currentrow, discountPer);
                        TotalSummary();
                    }



                    //    if(iSlNo==colIndex)
                    //{
                    //    dgvItemDet.Rows[currentrow].Selected = true;
                    //}
                    //if((iBarcode == colIndex) && (Convert.ToString(value) == vBarcode) && !string.IsNullOrWhiteSpace(Convert.ToString(value)))
                    if (iBarcode == colIndex)
                    {
                        int CurrentIndex = dgvItemDet.CurrentRow.Index;
                        dgvItemDet.Rows[currentrow].Cells[iBarcode].Value = Convert.ToString(value);
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
                                Itemdetails(CurrentIndex, ItemId);
                                discountPer = Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iDiscPer].Value);
                                DiscountPerToAmt(currentrow, discountPer);
                                TotalSummary();
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
                            //Itemdetails(CurrentIndex, ItemId);
                            //discountPer = Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iDiscPer].Value);
                            //DiscountPerToAmt(currentrow, discountPer);
                            //TotalSummary();
                        }
                    }

                    if (iBarcode == colIndex)
                    {
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

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtNetAmount_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.ActiveControl = mbtnSave;
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mbtnSave_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (IsSavable())
                    {
                        if (dgvItemDet.Rows.Count > 1 && Convert.ToDecimal(mtxtNetAmount.Text) > 0)
                        {
                            mpnlPayment.Visible = true;
                            mtxtpnlNetAmount.Text = mtxtNetAmount.Text;
                            ActiveControl = mtxtAmountReceived;
                            mtxtAmountReceived.Focus();
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "Invalid entry", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtiCardNo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.ActiveControl = mcmbMode;
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mcmbMode_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.ActiveControl = mtxtAmountReceived;
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mpnlPayment_Paint(object sender, PaintEventArgs e)
        {

        }



        private void McmbTax_Enter(object sender, EventArgs e)
        {
            try
            {
                mcmbTax.DroppedDown = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void McmbParticular_Enter(object sender, EventArgs e)
        {
            try
            {
                mcmbParticular.DroppedDown = true;
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
                ItemPOPup();
            }

            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mtxtpnlNetAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mtxtAmountReceived.Text) && (!string.IsNullOrWhiteSpace(mtxtpnlNetAmount.Text)))
                {
                    mtxtBalanceAmount.Text = Convert.ToString(Convert.ToDecimal(mtxtAmountReceived.Text) - Convert.ToDecimal(mtxtpnlNetAmount.Text)).ToString();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        #endregion
    }
}
