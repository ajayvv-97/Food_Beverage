using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Interfaces;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Diagnostics;
using System.Configuration;

namespace MarginPOSNew.Forms.Masters
{
    public partial class FrmListItem : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        private Control _last;
        long BranchId = Convert.ToInt32(FrmDashboard.BranchId);
        long UserId = Convert.ToInt32(FrmDashboard.UserId);
        ClsGeneral objGeneral = new ClsGeneral();
        ClsDocument clsDocument = new ClsDocument();
        long ItemId = 0;
        long oldCategory = 0;
        string oldBarcode = null;
        string oldCode = null;
        bool imageExist;
        decimal lastSrate = 0;
        #region grid
        const int iSlNo = 0;
        const int iCategory = 1;
        const int iCode = 2;
        const int iBarcode = 3;
        const int iBrand = 4;
        const int iName = 5;
        const int iSearchName = 6;
        const int iUnit = 7;
        const int iPrate1 = 8;
        const int iSrate1 = 9;
        const int iMrp1 = 10;
        const int iSellingPer = 11;
        const int iQtyOnHand = 12;
        const int iTaxType = 13;
        const int iHsnSac = 14;
        const int iSubUnit = 15;
        const int iPrate2 = 16;
        const int iSrate2 = 17;
        const int iMrp2 = 18;
        const int iPurchaseInclTax = 19;
        const int iSalesInclTax = 20;
        const int iCgstPer = 21;
        const int iSgstPer = 22;
        const int iIgstPer = 23;
        const int iCessPer = 24;
        const int iPacketQty = 25;
        const int iItemType = 26;
        const int iRack = 27;
        const int iStockItem = 28;
        const int iLockSaleRate = 29;
        const int iExpiryDate = 30;
        const int iItemId = 31;
        const int iCategoryId = 32;
        const int iUnitId = 33;
        const int iItemTypeId = 34;
        const int iTaxTypeId = 35;
        const int iIncPacketQty = 36;
        const int iInclIgst = 37;
        const int iSubUnitId = 38;
        DataTable dt = new DataTable();
       int UserTypeId = 0;
        
       
        int i=0;
        #endregion
        #endregion

        #region Functions
        public FrmListItem()
        {
            InitializeComponent();
        }
        public bool IsSavable(int CurIndex)
        {
            Control[] controllist = { };
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
            mcmbField.DisplayMember = "Text";
            mcmbField.ValueMember = "Value";

            var items = new[] {
                //new { Text = "-Select-", Value = "0" },
                new { Text = "Item Name", Value = "5" },
                new { Text = "Barcode", Value = "1" },
                new { Text = "Product", Value = "6" },
                new { Text = "Brand", Value = "2" },
                new { Text = "Code", Value = "4" },
                new { Text = "Category", Value = "3" }
            };
            mcmbField.DataSource = items;
            mtxtFieldValue.Text = "";
            fillGridViewSearch(150);
            FillSrchCategoryComboBox();
            mtxtFieldValue.Focus();
        }
        //public void FillGridUnitCombo(long itemId, int CurrentIndex)
        //{
        //    DmnItem dmnItem = new DmnItem();
        //    dmnItem.ItemId = itemId;

        //    BllSalesDetails objBll = new BllSalesDetails();
        //    var unitList = objBll.GetItemUnits(dmnItem);
        //    DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)dgvListItem.Rows[CurrentIndex].Cells[iUnit];
        //    var unitSource = unitList.Select(x => new { x.UnitId, x.UnitName });
        //    if (unitSource != null)
        //    {
        //        var mainUnit = unitList.ElementAt(0).UnitName;

        //        cbCell.DataSource = unitSource.ToList();
        //        cbCell.DisplayMember = "UnitName";
        //        cbCell.ValueMember = "UnitId";
        //        dgvListItem.CurrentRow.Cells[iUnit].Value = mainUnit;
        //    }
        //}
        public void FillCategoryComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "CategoryId as Id,CategoryName as Value";
            objSearch.Table = " tblCategory ";
            objSearch.WhereNdGroupBy = " Where BranchId = " + BranchId;
            objSearch.OrderBy = " ORDER BY CategoryName ASC";
            var gridSource = bllSearch.GetSearchData("prdComboFillWithoutSelect", objSearch);
            cnCategory.DataSource = gridSource;
            cnCategory.ValueMember = "Id";
            cnCategory.DisplayMember = "Value";
        }
        public void FillSrchCategoryComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "CategoryId as Id,CategoryName as Value";
            objSearch.Table = " tblCategory ";
            objSearch.WhereNdGroupBy = " Where BranchId = " + BranchId;
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbCategory.DataSource = gridSource;
            mcmbCategory.ValueMember = "Id";
            mcmbCategory.DisplayMember = "Value";
        }
        public void FillGridUnitCombo()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "UnitId as Id,UnitName as Value";
            objSearch.Table = " tblUnit ";
            objSearch.WhereNdGroupBy = " Where (Status = 0) AND BranchId = " + BranchId;
            objSearch.OrderBy = " ORDER BY UnitName ASC";
            var gridSource = bllSearch.GetSearchData("prdComboFillWithoutSelect", objSearch);
            cnUnit.DataSource = gridSource;
            cnUnit.ValueMember = "Id";
            cnUnit.DisplayMember = "Value";
            //cnItemType = cnItemType.FindStringExact("General");
        }
        public void FillGridSubUnitCombo()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "UnitId as Id,UnitName as Value";
            objSearch.Table = " tblUnit ";
            objSearch.WhereNdGroupBy = " Where (Status = 0) AND BranchId = " + BranchId;
            objSearch.OrderBy = " ORDER BY UnitName ASC";
            var gridSource = bllSearch.GetSearchData("prdComboFillWithoutSelect", objSearch);
            cnSubUnit.DataSource = gridSource;
            cnSubUnit.ValueMember = "Id";
            cnSubUnit.DisplayMember = "Value";
            //cnItemType = cnItemType.FindStringExact("General");
        }
        public void FillItemTypeComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "ItemClassId as Value,ItemClassName as Value";
            objSearch.Table = " tblItemClass ";
            objSearch.WhereNdGroupBy = " Where (Status = 0 OR Status = 2) AND BranchId = " + BranchId;
            objSearch.OrderBy = " ORDER BY ItemClassName ASC";
            var gridSource = bllSearch.GetSearchData("prdComboFillWithoutSelect", objSearch);
            cnItemType.DataSource = gridSource;
            cnItemType.ValueMember = "Id";
            cnItemType.DisplayMember = "Value";
            //cnItemType = cnItemType.FindStringExact("General");
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
            cnTaxType.DataSource = gridSource;
            cnTaxType.ValueMember = "Id";
            cnTaxType.DisplayMember = "Value";
            //cnTaxType.SelectedIndex = 1;
        }
        public void fillGridViewSearch(int rowsCount)
        {
            oldCategory = 0;
            oldBarcode = null;
            oldCode = null;

           dgvListItem.DataSource = null;
           dgvListItem.Rows.Clear();
            BllSearch<DmnItem> bllSearch = new BllSearch<Domain.DmnItem>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = " TOP " + rowsCount + " C.CategoryName,C.CategoryId,I.Barcode,I.ItemCode,I.BrandName,I.PacketQty,I.SearchName,I.HSNCode,I.Rack,I.ItemName,SU.UnitName as SubUnitName," +
                "IC.ItemClassName,I.StockItem,I.LockSaleRate,I.ExpiryDate,I.QuantityOnHand,I.SellingPer,U.UnitName,I.PurchaseRate1,I.MRP1,I.PurchaseRate2,I.MRP2,I.PurchaseTaxIncl,I.SalesTaxIncl," +
                "G.Caption,I.IGSTPer,I.CGSTPer,I.SGSTPer,I.CessPer,I.ItemId,I.CategoryId,I.QuantityOnHand,I.MainUnitId,I.SubUnitId,I.ItemClassId,I.TaxType,I.SellingPer,SU.UnitId as SubUnitId,I.SaleRate,I.SubSaleRate";
            objSearch.Table = " tblItem I LEFT JOIN tblCategory C ON I.CategoryId = C.CategoryId LEFT JOIN tblUnit U ON I.MainUnitId = U.UnitId " +
                "LEFT JOIN tblUnit SU ON I.SubUnitId = SU.UnitId LEFT JOIN tblItemClass IC ON I.ItemClassId = IC.ItemClassId LEFT JOIN tblGeneralOptions G ON I.TaxType = g.GeneralId ";
            if (mcmbField.SelectedValue != "0" && mtxtFieldValue.Text != "")
            {
                if (mcmbField.SelectedValue == "1")
                {
                    if (mcmbCategory.SelectedIndex == 0 || mcmbCategory.SelectedIndex == -1)
                        objSearch.WhereNdGroupBy = "Where I.Barcode LIKE '%" + mtxtFieldValue.Text + "%' And I.Status = 0 AND I.BranchId = " + BranchId;
                    else
                        objSearch.WhereNdGroupBy = "Where I.CategoryId = " + mcmbCategory.SelectedValue + " AND I.Barcode LIKE '%" + mtxtFieldValue.Text + "%' And I.Status = 0 AND I.BranchId = " + BranchId;
                }
                else if (mcmbField.SelectedValue == "2")
                {
                    if (mcmbCategory.SelectedIndex == 0 || mcmbCategory.SelectedIndex == -1)
                        objSearch.WhereNdGroupBy = "Where I.BrandName LIKE '%" + mtxtFieldValue.Text + "%' And I.Status = 0 AND I.BranchId = " + BranchId;
                    else
                        objSearch.WhereNdGroupBy = "Where I.CategoryId = " + mcmbCategory.SelectedValue + " AND I.BrandName LIKE '%" + mtxtFieldValue.Text + "%' And I.Status = 0 AND I.BranchId = " + BranchId;
                }
                else if (mcmbField.SelectedValue == "3")
                {
                    if (mcmbCategory.SelectedIndex == 0 || mcmbCategory.SelectedIndex == -1)
                        objSearch.WhereNdGroupBy = "Where C.CategoryName LIKE '%" + mtxtFieldValue.Text + "%' And I.Status = 0 AND I.BranchId = " + BranchId;
                    else
                        objSearch.WhereNdGroupBy = "Where I.CategoryId = " + mcmbCategory.SelectedValue + " AND C.CategoryName LIKE '%" + mtxtFieldValue.Text + "%' And I.Status = 0 AND I.BranchId = " + BranchId;
                }
                else if (mcmbField.SelectedValue == "4")
                {
                    if (mcmbCategory.SelectedIndex == 0 || mcmbCategory.SelectedIndex == -1)
                        objSearch.WhereNdGroupBy = "Where I.ItemCode LIKE '%" + mtxtFieldValue.Text + "%' And I.Status = 0 AND I.BranchId = " + BranchId;
                    else
                        objSearch.WhereNdGroupBy = "Where I.CategoryId = " + mcmbCategory.SelectedValue + " AND I.ItemCode LIKE '%" + mtxtFieldValue.Text + "%' And I.Status = 0 AND I.BranchId = " + BranchId;
                }
                else if (mcmbField.SelectedValue == "5")
                {
                    if (mcmbCategory.SelectedIndex == 0 || mcmbCategory.SelectedIndex == -1)
                        objSearch.WhereNdGroupBy = "Where I.SearchName LIKE '%" + mtxtFieldValue.Text + "%' And I.Status = 0 AND I.BranchId = " + BranchId;
                    else
                        objSearch.WhereNdGroupBy = "Where I.CategoryId = " + mcmbCategory.SelectedValue + " AND I.SearchName LIKE '%" + mtxtFieldValue.Text + "%' And I.Status = 0 AND I.BranchId = " + BranchId;
                }
                else if (mcmbField.SelectedValue == "6")
                {
                    if (mcmbCategory.SelectedIndex == 0 || mcmbCategory.SelectedIndex == -1)
                        objSearch.WhereNdGroupBy = "Where I.ItemName LIKE '%" + mtxtFieldValue.Text + "%' And I.Status = 0 AND I.BranchId = " + BranchId;
                    else
                        objSearch.WhereNdGroupBy = "Where I.CategoryId = " + mcmbCategory.SelectedValue + " AND I.ItemName LIKE '%" + mtxtFieldValue.Text + "%' And I.Status = 0 AND I.BranchId = " + BranchId;
                }
            }
            else
            {
                if (mcmbCategory.SelectedIndex == 0 || mcmbCategory.SelectedIndex == -1)
                        objSearch.WhereNdGroupBy = " Where I.BranchId = " + BranchId;
                    else
                        objSearch.WhereNdGroupBy = " Where I.CategoryId = " + mcmbCategory.SelectedValue + " And I.Status = 0 AND I.BranchId = " + BranchId;
            }
            objSearch.OrderBy = " Order By I.ItemName ASC ";
            var gridSourceItem = bllSearch.GetSearchData("prdSearch", objSearch);
            if (gridSourceItem.Count > 0)
            {
                dgvListItem.Rows.Add(gridSourceItem.Count);
                for (int i = 0; i < (gridSourceItem.Count); i++)
                {
                    dgvListItem.Rows[i].Cells[iSlNo].Value = (i + 1).ToString("#");
                    dgvListItem.Rows[i].Cells[iCategory].Value = Convert.ToString(gridSourceItem[i].CategoryName);

                    oldCategory = Convert.ToInt64(gridSourceItem[i].CategoryId);
                    oldBarcode = Convert.ToString(gridSourceItem[i].Barcode);
                    oldCode = Convert.ToString(gridSourceItem[i].ItemCode);

                    dgvListItem.Rows[i].Cells[iCode].Value = Convert.ToString(gridSourceItem[i].ItemCode);
                    dgvListItem.Rows[i].Cells[iBarcode].Value = Convert.ToString(gridSourceItem[i].Barcode);
                    dgvListItem.Rows[i].Cells[iBrand].Value = Convert.ToString(gridSourceItem[i].BrandName);
                    dgvListItem.Rows[i].Cells[iName].Value = Convert.ToString(gridSourceItem[i].ItemName);
                    dgvListItem.Rows[i].Cells[iPacketQty].Value = gridSourceItem[i].PacketQty;
                    dgvListItem.Rows[i].Cells[iSearchName].Value = gridSourceItem[i].SearchName;
                    dgvListItem.Rows[i].Cells[iHsnSac].Value = Convert.ToString(gridSourceItem[i].HSNCode);
                    dgvListItem.Rows[i].Cells[iRack].Value = Convert.ToString(gridSourceItem[i].Rack);
                    dgvListItem.Rows[i].Cells[iItemType].Value = Convert.ToString(gridSourceItem[i].ItemClassName);

                    if (Convert.ToString(gridSourceItem[i].StockItem) == "0")
                        dgvListItem.Rows[i].Cells[iStockItem].Value = "No";
                    if (Convert.ToString(gridSourceItem[i].StockItem) == "1")
                        dgvListItem.Rows[i].Cells[iStockItem].Value = "Yes";
                    if (Convert.ToString(gridSourceItem[i].LockSaleRate) == "0")
                        dgvListItem.Rows[i].Cells[iLockSaleRate].Value = "No";
                    if (Convert.ToString(gridSourceItem[i].LockSaleRate) == "1")
                        dgvListItem.Rows[i].Cells[iLockSaleRate].Value = "Yes";

                    dgvListItem.Rows[i].Cells[iExpiryDate].Value = Convert.ToString(gridSourceItem[i].ExpiryDate);
                    dgvListItem.Rows[i].Cells[iQtyOnHand].Value = Convert.ToString(gridSourceItem[i].QuantityOnHand);
                    dgvListItem.Rows[i].Cells[iSellingPer].Value = Convert.ToString(gridSourceItem[i].SellingPer);
                    dgvListItem.Rows[i].Cells[iUnit].Value = Convert.ToString(gridSourceItem[i].UnitName);
                    dgvListItem.Rows[i].Cells[iSubUnit].Value = Convert.ToString(gridSourceItem[i].SubUnitName);
                    dgvListItem.Rows[i].Cells[iPrate1].Value = Convert.ToString(gridSourceItem[i].PurchaseRate1);
                    dgvListItem.Rows[i].Cells[iMrp1].Value = Convert.ToString(gridSourceItem[i].MRP1);
                    dgvListItem.Rows[i].Cells[iPrate2].Value = Convert.ToString(gridSourceItem[i].PurchaseRate2);
                    dgvListItem.Rows[i].Cells[iMrp2].Value = Convert.ToString(gridSourceItem[i].MRP2);

                    if (Convert.ToString(gridSourceItem[i].PurchaseTaxIncl) == "0")
                        dgvListItem.Rows[i].Cells[iPurchaseInclTax].Value = "No";
                    if (Convert.ToString(gridSourceItem[i].PurchaseTaxIncl) == "1")
                        dgvListItem.Rows[i].Cells[iPurchaseInclTax].Value = "Yes";

                    if (Convert.ToString(gridSourceItem[i].SalesTaxIncl) == "0")
                        dgvListItem.Rows[i].Cells[iSalesInclTax].Value = "No";
                    if (Convert.ToString(gridSourceItem[i].SalesTaxIncl) == "1")
                        dgvListItem.Rows[i].Cells[iSalesInclTax].Value = "Yes";

                    dgvListItem.Rows[i].Cells[iTaxType].Value = Convert.ToString(gridSourceItem[i].Caption);
                    if (Convert.ToDecimal(gridSourceItem[i].IGSTPer) > 0)
                        dgvListItem.Rows[i].Cells[iInclIgst].Value = 1;
                    else
                        dgvListItem.Rows[i].Cells[iInclIgst].Value = 0;

                    dgvListItem.Rows[i].Cells[iCgstPer].Value = Convert.ToString(gridSourceItem[i].CGSTPer);
                    dgvListItem.Rows[i].Cells[iSgstPer].Value = Convert.ToString(gridSourceItem[i].SGSTPer);
                    dgvListItem.Rows[i].Cells[iIgstPer].Value = Convert.ToString(gridSourceItem[i].IGSTPer);
                    dgvListItem.Rows[i].Cells[iCessPer].Value = Convert.ToString(gridSourceItem[i].CessPer);
                    dgvListItem.Rows[i].Cells[iItemId].Value = Convert.ToString(gridSourceItem[i].ItemId);
                    ItemId = Convert.ToInt64(gridSourceItem[i].ItemId);
                    dgvListItem.Rows[i].Cells[iCategoryId].Value = Convert.ToString(gridSourceItem[i].CategoryId);
                    dgvListItem.Rows[i].Cells[iQtyOnHand].Value = Convert.ToString(gridSourceItem[i].QuantityOnHand);
                    dgvListItem.Rows[i].Cells[iUnitId].Value = Convert.ToString(gridSourceItem[i].MainUnitId);
                    dgvListItem.Rows[i].Cells[iSubUnitId].Value = Convert.ToString(gridSourceItem[i].SubUnitId);
                    dgvListItem.Rows[i].Cells[iItemTypeId].Value = Convert.ToString(gridSourceItem[i].ItemClassId);
                    dgvListItem.Rows[i].Cells[iTaxTypeId].Value = Convert.ToString(gridSourceItem[i].TaxType);
                    decimal srate1 = Convert.ToDecimal(gridSourceItem[i].PurchaseRate1 * (100 + gridSourceItem[i].SellingPer) / 100);
                    dgvListItem.Rows[i].Cells[iSrate1].Value = Convert.ToString(gridSourceItem[i].SaleRate);
                    decimal srate2 = Convert.ToDecimal(gridSourceItem[i].PurchaseRate2 * (100 + gridSourceItem[i].SellingPer) / 100);
                    dgvListItem.Rows[i].Cells[iSrate2].Value = Convert.ToString(gridSourceItem[i].SubSaleRate);
                }
            }
            UserTypeId=Convert.ToInt32(Forms.UserAccounts.FrmLogin.UserAccountTypeId);
            if(UserTypeId!=1)
            {
                
                foreach (DataGridViewColumn column in dgvListItem.Columns)
                {
                    column.ReadOnly = true;
                }
                if (UserTypeId == 4)
                {
                    dgvListItem.Columns["cnSrate1"].ReadOnly = false;
                    dgvListItem.Columns["cnSrate2"].ReadOnly = false;

                }
            }
        }
        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var sendingCB = sender as DataGridViewComboBoxEditingControl;

                var currentcell = dgvListItem.CurrentCellAddress;
                int cmbSelectedIndex = sendingCB.SelectedIndex;
                int CurrentIndex = dgvListItem.CurrentRow.Index;
                int CIndex = dgvListItem.CurrentCell.ColumnIndex;


                if (cmbSelectedIndex >= 0 && Convert.ToString(sendingCB.Text) != string.Empty)
                {
                    if (((ComboBox)sender).SelectedValue is long)
                    {

                        long cmbValue = Convert.ToInt64(((ComboBox)sender).SelectedValue);
                        string selValue = Convert.ToString(((ComboBox)sender).Text);
                        if (iItemType == CIndex)
                        {
                            dgvListItem.Rows[CurrentIndex].Cells[iItemTypeId].Value = cmbValue;
                        }
                        else if (iTaxType == CIndex)
                        {
                            dgvListItem.Rows[CurrentIndex].Cells[iTaxTypeId].Value = cmbValue;
                        }
                        else if (iUnit == CIndex)
                        {
                            dgvListItem.Rows[CurrentIndex].Cells[iUnitId].Value = cmbValue;
                        }
                        else if (iSubUnit == CIndex)
                        {
                            dgvListItem.Rows[CurrentIndex].Cells[iSubUnitId].Value = cmbValue;
                        }
                        else if (iCategory == CIndex)
                        {
                            dgvListItem.Rows[CurrentIndex].Cells[iCategoryId].Value = cmbValue;
                            GetSerialNo(CurrentIndex, cmbValue);
                            if (Convert.ToString(selValue) == Convert.ToString("Vegetables"))
                            {
                                dgvListItem.Rows[CurrentIndex].Cells[iBrand].Value = "Vegetables";
                            }
                            if (Convert.ToString(selValue) == Convert.ToString("Fruits"))
                            {
                                dgvListItem.Rows[CurrentIndex].Cells[iBrand].Value = "Fruits";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", 150);
            }
        }
        public void SearchText(int CurIndex)
        {
            string MainUnit = "";
            if (dgvListItem.Rows[CurIndex].Cells[iIncPacketQty].Value != null)
            {
                if (dgvListItem.Rows[CurIndex].Cells[iUnit].Value != null)
                {
                    MainUnit = " " + dgvListItem.Rows[CurIndex].Cells[iUnit].Value;
                }
                dgvListItem.Rows[CurIndex].Cells[iSearchName].Value = dgvListItem.Rows[CurIndex].Cells[iBrand].Value + " " + dgvListItem.Rows[CurIndex].Cells[iName].Value + MainUnit + " " + dgvListItem.Rows[CurIndex].Cells[iPacketQty].Value;
            }
            if (dgvListItem.Rows[CurIndex].Cells[iIncPacketQty].Value != null)
            {
                dgvListItem.Rows[CurIndex].Cells[iSearchName].Value = dgvListItem.Rows[CurIndex].Cells[iBrand].Value + " " + dgvListItem.Rows[CurIndex].Cells[iName].Value;
            }
        }
        public void GetSerialNo(int CurIndex, long cmbValue)
        {
            if (cmbValue is long)
            {

                if (cmbValue > 0)
                {
                    BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.Select = "CategoryCode,CategorySrlNoCurrent";
                    objSearch.Table = " tblCategory ";
                    objSearch.WhereNdGroupBy = "Where CategoryId = " + cmbValue;
                    objSearch.OrderBy = "Order By CategoryName ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource.Count > 0)
                    {
                        dgvListItem.Rows[CurIndex].Cells[iCode].Value = Convert.ToString(gridSource[0].CategoryCode) + Convert.ToString(gridSource[0].CategorySrlNoCurrent + 1);
                        //mtxtBarcode.Text = Convert.ToString(gridSource[0].CategorySrlNoCurrent+1);
                        if (Convert.ToInt64(dgvListItem.Rows[CurIndex].Cells[iCategoryId].Value) == oldCategory)
                        {
                            dgvListItem.Rows[CurIndex].Cells[iCode].Value = oldCode;
                            dgvListItem.Rows[CurIndex].Cells[iBarcode].Value = oldBarcode;
                        }
                        if (dgvListItem.Rows[CurIndex].Cells[iBarcode].Value == string.Empty)
                            dgvListItem.Rows[CurIndex].Cells[iBarcode].Value = Convert.ToString(gridSource[0].CategorySrlNoCurrent + 1);
                        dgvListItem.Rows[CurIndex].Cells[iIncPacketQty].Value = false;
                    }
                }
            }
        }
        public void getsalesprice1(int CurIndex)
        {
            decimal SalesRate1;
            decimal Sellper1;
            int CurCell = dgvListItem.CurrentCell.ColumnIndex;
            if ((CurCell != iSrate1) && !string.IsNullOrWhiteSpace(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSellingPer].Value)) && !string.IsNullOrWhiteSpace(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPrate1].Value)) && !string.IsNullOrWhiteSpace(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp1].Value)))
            {
                SalesRate1 = Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iPrate1].Value) + ((Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iSellingPer].Value)) * Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iPrate1].Value)) / 100;
                dgvListItem.Rows[CurIndex].Cells[iSrate1].Value = Convert.ToString(SalesRate1.ToString("0.00"));
            }
            else if ((CurCell == iSrate1) && !string.IsNullOrWhiteSpace(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSrate1].Value)) && !string.IsNullOrWhiteSpace(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPrate1].Value)) && !string.IsNullOrWhiteSpace(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp1].Value)))
            {
                if (Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iPrate1].Value) > 0)
                {
                    Sellper1 = ((Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iSrate1].Value) - Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iPrate1].Value)) * 100) / (Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iPrate1].Value));
                    if (Sellper1 >= 0)
                        dgvListItem.Rows[CurIndex].Cells[iSellingPer].Value = Convert.ToString(Math.Round(Sellper1 * 100) / 100);
                }
            }
        }
        public void getsalesprice2(int CurIndex)
        {
            decimal SalesRate2;
            decimal Sellper2;
            int CurCell = dgvListItem.CurrentCell.ColumnIndex;
            if ((CurCell != iSrate2) && !string.IsNullOrWhiteSpace(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSellingPer].Value)) && !string.IsNullOrWhiteSpace(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPrate2].Value)) && !string.IsNullOrWhiteSpace(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp2].Value)))
            {
                SalesRate2 = Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iPrate2].Value) + ((Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iSellingPer].Value)) * Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iPrate1].Value)) / 100;
                dgvListItem.Rows[CurIndex].Cells[iSrate2].Value = Convert.ToString(SalesRate2.ToString("0.00"));
            }
            else if ((CurCell == iSrate2) && !string.IsNullOrWhiteSpace(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSrate2].Value)) && !string.IsNullOrWhiteSpace(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPrate2].Value)) && !string.IsNullOrWhiteSpace(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp2].Value)))
            {
                Sellper2 = ((Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iSrate2].Value) - Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iPrate2].Value)) * 100) / (Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iPrate2].Value));
                if (Sellper2 >= 0)
                    dgvListItem.Rows[CurIndex].Cells[iSellingPer].Value = Convert.ToString(Math.Round(Sellper2 * 100) / 100);
            }
        }
        public bool GetSalesRate1(int CurIndex)
        {
            if (decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSrate1].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSrate1].Value)) <= decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp1].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp1].Value)))
            {
                return true;
            }
            else
            {
                dgvListItem.CurrentCell = dgvListItem.Rows[CurIndex].Cells[iMrp1];
            }
            return false;
        }
        public bool GetSalesRate2(int CurIndex)
        {
            if (decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSrate2].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSrate2].Value)) <= decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp2].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp2].Value)))
            {
                return true;
            }
            else
            {
                dgvListItem.CurrentCell = dgvListItem.Rows[CurIndex].Cells[iMrp2];
            }
            return false;
        }
        #endregion

        #region Events

        private void FrmListItem_Load(object sender, EventArgs e)
        {
            try
            {
                fillGridViewSearch(150);
                FillSrchCategoryComboBox();
                FillCategoryComboBox();
                FillItemTypeComboBox();
                FillTaxTypeComboBox();
                FillGridUnitCombo();
                FillGridSubUnitCombo();  
                RefreshForm();
                dt.Columns.Add("barcode");
                dt.Columns.Add("name");
                dt.Columns.Add("mrp");
                dt.Columns.Add("srate");
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void FrmListItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //objGeneral.NextConrolOnEnterPress(sender, e);
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
                if (e.KeyCode == Keys.F12)
                {
                    FrmItem frmItem = new FrmItem();
                    frmItem.ShowDialog();
                    RefreshForm();
                }
                if (e.KeyCode == Keys.F3)
                {
                    RefreshForm();
                }
                if (e.KeyCode == Keys.F5)
                {
                    RefreshForm();
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
        private void DgvListItem_DataError(object sender, DataGridViewDataErrorEventArgs e)
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
        private void DgvListItem_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is TextBox)
                {
                    int colIndex = dgvListItem.CurrentCell.ColumnIndex;
                    int rowIndex = dgvListItem.CurrentCell.RowIndex;
                    if (colIndex == iPacketQty || colIndex == iQtyOnHand || colIndex == iPrate1 || colIndex == iSellingPer || colIndex == iSrate1 || colIndex == iMrp1 || colIndex == iPrate2 || colIndex == iSrate2 || colIndex == iMrp2 || colIndex == iCgstPer || colIndex == iSgstPer || colIndex == iIgstPer || colIndex == iCessPer)
                    {
                        TextBox curtxtbox = e.Control as TextBox;
                        curtxtbox.KeyPress += new KeyPressEventHandler(objGeneral.DecimalGridviewTextBox);
                    }
                    
                }
                if (e.Control is ComboBox)//this region is for unit editing
                {
                    int colIndex = dgvListItem.CurrentCell.ColumnIndex;
                    if (iCategory == colIndex || iUnit == colIndex || iItemType == colIndex || iTaxType == colIndex || iSubUnitId == colIndex)
                    {

                        #region method 1
                        ComboBox comboBox = e.Control as ComboBox;

                        comboBox.SelectedIndexChanged -= LastColumnComboSelectionChanged;
                        comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
                        #endregion                      
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void DgvListItem_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                dgvListItem.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void DgvListItem_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
        }
        
        private void MbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                fillGridViewSearch(150);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void dgvListItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.F12)
                {
                    if (dgvListItem.CurrentCell.ColumnIndex == iCategory)
                    {
                        FrmCategory frmCategory = new FrmCategory();
                        frmCategory.ShowDialog();
                        FillCategoryComboBox();
                    }
                    else if (dgvListItem.CurrentCell.ColumnIndex == iUnit)
                    {
                        FrmUnit frmUnit = new FrmUnit();
                        frmUnit.ShowDialog();
                        FillGridUnitCombo();
                    }
                }
                if (e.KeyCode == Keys.Delete)
                {
                    //bool deletestatus;
                    //if (ItemId != 0)
                    //{
                    //    //DialogResult result = MessageBox.Show(this, "Confirm to delete " + Convert.ToString(dgvListItem.CurrentRow.Cells[iName].Value) + " ? ", "Delete ? ", MessageBoxButtons.YesNo,MessageBoxDefaultButton.Button2, MessageBoxIcon.Question, MessageBoxOptions.RightAlign);
                    //    DialogResult result = MessageBox.Show(this, "Confirm to delete ?", "Confirmation", MessageBoxButtons.YesNo,MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2,MessageBoxOptions.RightAlign);
                    //    if (result == DialogResult.Yes)
                    //    {
                    //        DmnItem dmnItem = new DmnItem();
                    //        BllItem bllItem = new BllItem();
                    //        dmnItem.ItemId = Convert.ToInt64(dgvListItem.CurrentRow.Cells[iItemId].Value);
                    //        var lstData = bllItem.DeleteById("prdDeleteItem", dmnItem);
                    //        deletestatus = lstData;

                    //        if (deletestatus)
                    //        {
                    //            imageExist = clsDocument.FileExistOrNot(Convert.ToString(dgvListItem.CurrentRow.Cells[iCode].Value) + ".jpeg", DocumentType.ItemPhoto);
                    //            if (imageExist)
                    //                clsDocument.DeleteImage(Convert.ToString(dgvListItem.CurrentRow.Cells[iCode].Value) + ".jpeg", DocumentType.ItemPhoto);
                    //            dmnItem.Image = "";
                    //            MessageBox.Show(this, "Successfully Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //        }
                    //        else
                    //            MessageBox.Show(this, "Delete Failed " + Convert.ToString(dgvListItem.CurrentRow.Cells[iName].Value), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    }
                    //}
                    RefreshForm();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mcmbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //fillGridViewSearch(1000);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mtxtFieldValue_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                fillGridViewSearch(150);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void dgvListItem_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (UserTypeId == 1)
                {
                    DialogResult result = MessageBox.Show(this," [ "+ dgvListItem.CurrentRow.Cells[iSearchName].Value.ToString() + " ] Prepare For Barcode ? " , "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes)
                    {
                        dt.Rows.Add(dgvListItem.CurrentRow.Cells[iBarcode].Value.ToString(), dgvListItem.CurrentRow.Cells[iName].Value.ToString(), dgvListItem.CurrentRow.Cells[iMrp1].Value.ToString(), dgvListItem.CurrentRow.Cells[iSrate1].Value.ToString());
                        label1.Text = "'" + dt.Rows.Count.ToString() + "'Item Selected For Barcode";
                    }
                    else
                    {

                        DialogResult result1 = MessageBox.Show(this," [ " + dgvListItem.CurrentRow.Cells[iSearchName].Value.ToString() + " ] Want to Edit Details ? ", "Confirmation", MessageBoxButtons.YesNo,MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (result1 == DialogResult.Yes)
                        {
                            if (dgvListItem.Rows.Count > 0)
                            {
                                ItemId = Convert.ToInt64(dgvListItem.CurrentRow.Cells[iItemId].Value);
                                FrmItem frmItem = new FrmItem(ItemId);
                                frmItem.ShowDialog();
                            }

                        }


                    }
                }
                else
                {
                    //DialogResult result = MessageBox.Show(this, "For  '" + dgvListItem.CurrentRow.Cells[iSearchName].Value.ToString(), " Create Bar Code ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    DialogResult result = MessageBox.Show(this, dgvListItem.CurrentRow.Cells[iSearchName].Value.ToString() + " Selected for Barcode Creation ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        dt.Rows.Add(dgvListItem.CurrentRow.Cells[iBarcode].Value.ToString(), dgvListItem.CurrentRow.Cells[iName].Value.ToString(), dgvListItem.CurrentRow.Cells[iMrp1].Value.ToString(), dgvListItem.CurrentRow.Cells[iSrate1].Value.ToString());
                        label1.Text = "'"+dt.Rows.Count.ToString()+"'Item Selected For Barcode";
                    }
                }
                label1.Visible = true;
                   
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void dgvListItem_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvListItem.CurrentCell.RowIndex > 0 && dgvListItem.CurrentCell.RowIndex < dgvListItem.Rows.Count)
                    {
                        int colIndx = dgvListItem.CurrentCell.ColumnIndex + 1;
                        int rowIndx = dgvListItem.CurrentCell.RowIndex - 1;
                        if (colIndx < 31)
                        {
                            if (dgvListItem.Rows[rowIndx].Cells[colIndx].Visible == true)
                            {
                                dgvListItem.CurrentCell = dgvListItem.Rows[rowIndx].Cells[colIndx];
                            }
                            else
                            {
                                dgvListItem.CurrentCell = dgvListItem.Rows[rowIndx].Cells[colIndx + 1];
                            }
                            dgvListItem.CurrentCell.Selected = true;
                            dgvListItem.BeginEdit(true);
                        }
                    }
                }
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
                FrmItem frmItem = new FrmItem();
                frmItem.ShowDialog();
                RefreshForm();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void dgvListItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
                int currentRow = dgvListItem.CurrentCell.RowIndex;
                int currentColumn = dgvListItem.CurrentCell.ColumnIndex;

            lastSrate = Convert.ToDecimal(dgvListItem.Rows[currentRow].Cells[iSrate1].Value);
            if(dgvListItem.Columns[currentColumn].ReadOnly ==true)
            {
                MessageBox.Show("Not Granted !!!", "Notification",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }

        }

        #endregion
       
        private void Run_AppBarLabl()
        {
            Process myProcess = new Process();

            myProcess.StartInfo.UseShellExecute = false;
            // You can start any process, HelloWorld is a do-nothing example.
            //myProcess.StartInfo.FileName = @"C:\Windows\notepad.exe";
            myProcess.StartInfo.FileName = @ConfigurationManager.AppSettings["AppBarcode"];
            myProcess.StartInfo.Arguments = "-startoemtsc";
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.Start();
        }
        private void dgvListItem_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int CurIndx = Convert.ToInt32(dgvListItem.CurrentRow.Index);
                if (dgvListItem.CurrentRow != null)
                {
                    // DialogResult result = MetroMessageBox.Show(this,"Want To Save The Changes", " Save ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //DialogResult result = MessageBox.Show("Want To Save The Changes ??", "Confirmation",
                    //             MessageBoxButtons.YesNo,MessageBoxDefaultButton.Button2,
                    //             MessageBoxIcon.Question);

                    decimal prate = Convert.ToDecimal(dgvListItem.Rows[CurIndx].Cells[iPrate1].Value);
                    decimal SalesRate = Convert.ToDecimal(dgvListItem.Rows[CurIndx].Cells[iSrate1].Value);
                    decimal SalePer = (SalesRate / prate * 100) - 100;
                   decimal salperbackup= Convert.ToDecimal(dgvListItem.Rows[CurIndx].Cells[iSellingPer].Value);
                    dgvListItem.Rows[CurIndx].Cells[iSellingPer].Value = SalePer;
                    DialogResult result = MessageBox.Show(this, "Want to Save the Changes ?", "Confirmation", MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);


                  
                    if (result.Equals(DialogResult.Yes))
                    {                      
                        BllSalesDetails bsdet = new BllSalesDetails();
                        getsalesprice1(CurIndx);
                        if (Convert.ToString(dgvListItem.Rows[CurIndx].Cells[iSubUnit].Value) != "")
                        {
                            getsalesprice2(CurIndx);
                        }
                        updategrideviewchange();
                        //decimal SalesRate = Convert.ToDecimal(dgvListItem.Rows[CurIndx].Cells[iSrate1].Value);
                        //decimal SaleRate2 = Convert.ToDecimal(dgvListItem.Rows[CurIndx].Cells[iSrate2].Value);
                        //decimal barcode = Convert.ToDecimal(dgvListItem.Rows[CurIndx].Cells[iBarcode].Value);
                        //decimal prate = Convert.ToDecimal(dgvListItem.Rows[CurIndx].Cells[iPrate1].Value);
                        ////= (C3 / A3 * 100) - 100;
                        //decimal SalePer = (SalesRate / prate * 100) - 100;
                        //dgvListItem.Rows[CurIndx].Cells[iSellingPer].Value = SalePer;
                        //bsdet.UpdatesalesRate(lastSrate, SalesRate, SaleRate2, SalePer, barcode);

                    }
                    else
                    {
                        dgvListItem.Rows[CurIndx].Cells[iSrate1].Value = lastSrate;
                        dgvListItem.Rows[CurIndx].Cells[iSellingPer].Value = salperbackup;
                    }

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void dgvListItem_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Color colour = ColorTranslator.FromHtml("#999092");
            dgvListItem[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = colour;
        }

        private void dgvListItem_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvListItem[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Empty;
        }
        public void updategrideviewchange()
        {
            try
            {
                int savestate = 0;
                int CurIndex = -1;              
                CurIndex = dgvListItem.CurrentRow.Index;
                DmnItem dmnItem = new DmnItem();
                BllItem bllItem = new BllItem(); 

                decimal SalesRate = Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iSrate1].Value);
                decimal SaleRate2 = Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iSrate2].Value);
                decimal barcode = Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iBarcode].Value);
                decimal prate = Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iPrate1].Value);
                decimal SalePer = (SalesRate / prate * 100) - 100;
                dmnItem.SaleRate = Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iSrate1].Value);
                dmnItem.SubSaleRate = Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iSrate2].Value);
                dmnItem.SellingPer = SalePer;
                dgvListItem.Rows[CurIndex].Cells[iSellingPer].Value = SalePer;
                dmnItem.Barcode = Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iBarcode].Value);
                dmnItem.BranchId = BranchId;
                dmnItem.CategoryId = Convert.ToInt32(dgvListItem.Rows[CurIndex].Cells[iCategoryId].Value);
                dmnItem.SGSTPer = decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSgstPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSgstPer].Value));
                dmnItem.CGSTPer = decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iCgstPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iCgstPer].Value));
                dmnItem.IGSTPer = Convert.ToDecimal("0.00");
                dmnItem.CessPer = decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iCessPer].Value)) ;
                dmnItem.CreatedBy = UserId;
                dmnItem.HSNCode = Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iHsnSac].Value);
                dmnItem.ItemCode = Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iCode].Value);
                dmnItem.ItemId = long.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iItemId].Value) == string.Empty ? "0" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iItemId].Value));

                dmnItem.ItemClassId = Convert.ToInt32(dgvListItem.Rows[CurIndex].Cells[iItemTypeId].Value);
                dmnItem.ItemName = Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iName].Value);
                dmnItem.BrandName = Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iBrand].Value);
                dmnItem.PacketQty = long.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPacketQty].Value) == string.Empty ? "0" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPacketQty].Value));
                dmnItem.SearchName = Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSearchName].Value);
                dmnItem.QuantityOnHand = Convert.ToDecimal(dgvListItem.Rows[CurIndex].Cells[iQtyOnHand].Value);
                if (Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iLockSaleRate].Value) == "Yes")
                {
                    dmnItem.LockSaleRate = 1;
                }
                if (Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iLockSaleRate].Value) == "No")
                {
                    dmnItem.LockSaleRate = 0;
                }
                if (Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iStockItem].Value) == "Yes")
                {
                    dmnItem.StockItem = 1;
                }
                if (Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iStockItem].Value) == "No")
                {
                    dmnItem.StockItem = 0;
                }
                dmnItem.MRP1 = decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp1].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp1].Value));
                dmnItem.MRP2 = decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp2].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp2].Value));
                dmnItem.PurchaseRate1 = decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPrate1].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPrate1].Value));
                dmnItem.PurchaseRate2 = decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPrate2].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPrate2].Value));
                dmnItem.QuantityOnHand = decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iQtyOnHand].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iQtyOnHand].Value));

                //last rate update

                dmnItem.LastMRP1 = decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp1].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp1].Value));
                dmnItem.LastMRP2 = decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp2].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iMrp2].Value));
                dmnItem.LastPurchaseRate1 = decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPrate1].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPrate1].Value));
                dmnItem.LastPurchaseRate2 = decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPrate2].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPrate2].Value));
                dmnItem.LastSellingPer = decimal.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSellingPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSellingPer].Value));

                if (Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iPurchaseInclTax].Value) == "Yes")
                {
                    dmnItem.PurchaseTaxIncl = 1;
                }
                else
                {
                    dmnItem.PurchaseTaxIncl = 0;
                }
                if (Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSalesInclTax].Value) == "Yes")
                {
                    dmnItem.SalesTaxIncl = 1;
                }
                else
                {
                    dmnItem.SalesTaxIncl = 0;
                }
                dmnItem.Rack = Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iRack].Value);
                dmnItem.TaxType = int.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iTaxTypeId].Value) == string.Empty ? "0" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iTaxTypeId].Value));
                dmnItem.SubUnitId = int.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSubUnitId].Value) == string.Empty ? "0" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iSubUnitId].Value));
                dmnItem.MainUnitId = int.Parse(Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iUnitId].Value) == string.Empty ? "0" : Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iUnitId].Value));
                dmnItem.Status = 0;
                dmnItem.ItemCode = Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iCode].Value);
                //dmnItem.Barcode = Convert.ToString(dgvListItem.Rows[CurIndex].Cells[iBarcode].Value);
                dmnItem.ModifiedBy = UserId;
                dmnItem.ModifiedDateTime = DateTime.Now;
                var lstData = bllItem.UpdateData("prdUpdateItem", dmnItem);

                //if (savestatus)
                //MetroMessageBox.Show(this, "Updated successfully " + dgvListItem.Rows[CurIndex].Cells[iName].Value, " Updated  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                //else
                //MetroMessageBox.Show(this, "Save Failed " + dgvListItem.Rows[CurIndex].Cells[iName].Value, " Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                //RefreshForm(CurIndex);
                //fillGridView();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void UpdatePLUFile()
        {
            try
            {

                BllSearch<DmnGeneralOptions> bllSearch = new BllSearch<DmnGeneralOptions>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "GTypeSubText1,GTypeSubText2 ";
                objSearch.Table = " tblGeneralOptions ";
                objSearch.WhereNdGroupBy = "Where GTypeId= 24";  // General Setting
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);


                //string fileName = @"C:\Temp\MaheshTX.txt";
                string FilePathPLU = @Convert.ToString(gridSource[0].GTypeSubText1);
                FilePathPLU = FilePathPLU + "\\" + Convert.ToString(gridSource[0].GTypeSubText2);


                // Check if file already exists. If yes, delete it.     
                if (File.Exists(FilePathPLU))
                {
                    File.Delete(FilePathPLU);
                }

                // Create a new file     
                //using (StreamWriter sw = File.CreateText(FilePathPLU))
                //{
                //    sw.WriteLine("Author: Mahesh Chand");
                //    sw.WriteLine("Done! ");
                //}

                BllSearch<DmnItem> bllSearch1 = new BllSearch<DmnItem>();
                DmnSearch objSearchItem = new DmnSearch();
                objSearchItem = new DmnSearch();
                objSearchItem.Select = "I.ItemId,I.ItemCode,I.Barcode,I.ItemName,I.MainUnitId,I.SaleRate ";
                objSearchItem.Table = " tblItem as I";
                objSearchItem.WhereNdGroupBy = "Where I.CategoryId in (24,47,55) and  i.barcodemode=1";  // General Setting
                var gridSource1 = bllSearch1.GetSearchData("prdSearch", objSearchItem);

                using (StreamWriter sw = File.CreateText(FilePathPLU))
                {
                    //id	code	barcode	name	mainunit	srate
                    sw.WriteLine("id" + Convert.ToChar(9) + "code" + Convert.ToChar(9) +
                        "barcode" + Convert.ToChar(9) + "name" + Convert.ToChar(9) +
                        "mainunit" + Convert.ToChar(9) + "srate");

                    foreach (var item in gridSource1)
                    {
                        //lbTransportation.Items.Add(item.GTypeSubText2);
                        sw.WriteLine(Convert.ToString(item.ItemCode) + Convert.ToChar(9) +
                            Convert.ToString(item.Barcode) + Convert.ToChar(9) +
                            Convert.ToString(item.Barcode) + Convert.ToChar(9) +
                            Convert.ToString(item.ItemName) + Convert.ToChar(9) +
                            Convert.ToString(item.MainUnitId) + Convert.ToChar(9) +
                            Convert.ToString(item.SaleRate));

                    }

                    MessageBox.Show("PLU Weight M/c Rate Successfully Updated !!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                //// Write file contents on console.     
                //using (StreamReader sr = File.OpenText(FilePathPLU))
                //{
                //    string s = "";
                //    while ((s = sr.ReadLine()) != null)
                //    {
                //        Console.WriteLine(s);
                //    }
                //}
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mbtnUpdatePLU_Click(object sender, EventArgs e)
        {
            UpdatePLUFile();
        }

        private void mcmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            MbtnList_Click(null, null);
        }
    }
}
