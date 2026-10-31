using MarginPOSNew.Classes;
using FoodandBeverage.Classes;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Domain;
using BLL;
using MetroFramework.Controls;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using System.Globalization;

namespace MarginPOSNew.Forms.Masters
{
    public partial class FrmItem : MetroFramework.Forms.MetroForm
    {
        private Form _blurOwner;
        private BlurPopupOverlay _ownerBlurOverlay;

        #region Declaration
        public string _retValue { get; set; }
        ClsGeneral objGeneral = new ClsGeneral();
        ClsDocument clsDocument = new ClsDocument();
        long BranchId = Convert.ToInt32(FrmDashboard.BranchId);
        long UserId = Convert.ToInt32(FrmDashboard.UserId);
        long ItemId = 0;
        public string mtxtPath = string.Empty;
        //string imageFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Images\\Products\\";
        string imageFolder = Path.GetDirectoryName(Application.ExecutablePath) + "\\Resources\\Images\\Products\\";
        //string imageFolder;
        Image img;
        Bitmap bitmap;
        string MainUnit = "";
        int catStatus = 0;
        decimal taxPer = 0;
        bool imageExist;
        long oldCategory;
        string oldCode="";
        string oldBarcode = "";
        #endregion

        #region Functions

        public FrmItem()
        {
            _blurOwner = Form.ActiveForm;
            InitializeComponent();
            RegisterOwnerBlurEvents();
            RefreshForm();
        }
        public FrmItem(long ItemId)
        {
            _blurOwner = Form.ActiveForm;
            InitializeComponent();
            RegisterOwnerBlurEvents();
            LoadFromList(ItemId);
        }

        private void RegisterOwnerBlurEvents()
        {
            if (_blurOwner == this || (_blurOwner != null && _blurOwner.IsDisposed))
                _blurOwner = null;
            Shown += FrmItem_Shown;
            FormClosed += FrmItem_FormClosedForBlur;
        }

        private void FrmItem_Shown(object sender, EventArgs e)
        {
            try
            {
                Form blurOwner = Owner ?? _blurOwner;
                if (blurOwner == null || blurOwner == this || blurOwner.IsDisposed || blurOwner.Disposing)
                    return;
                _ownerBlurOverlay = new BlurPopupOverlay(blurOwner);
                _ownerBlurOverlay.ShowOwnerBlur();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FrmItem owner blur: " + ex);
            }
        }

        private void FrmItem_FormClosedForBlur(object sender, FormClosedEventArgs e)
        {
            if (_ownerBlurOverlay != null)
            {
                _ownerBlurOverlay.HideOwnerBlur();
                _ownerBlurOverlay.Dispose();
                _ownerBlurOverlay = null;
            }
        }
        public void LoadFromList(long ItemId)
        {
            RefreshForm();
            
            BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = " tblItem ";
            objSearch.WhereNdGroupBy = "Where ItemId = " + ItemId + " And Status=0 AND BranchId = "+ BranchId;
            objSearch.OrderBy = "Order By ItemName ";
            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
            mcmbCategory.SelectedValue = gridSource[0].CategoryId;
            oldCategory = Convert.ToInt64(mcmbCategory.SelectedValue);
            if (Convert.ToString(gridSource[0].BarcodeMode) == "0")
            {
                mchkWMachine.Checked = false;
            }
            else
            {
                mchkWMachine.Checked = true;
            }
            mcmbMainUnit.SelectedValue = gridSource[0].MainUnitId;
            if (gridSource[0].SubUnitId != null)
                mcmbSubUnit.SelectedValue = gridSource[0].SubUnitId;
            mtxtCode.Text = Convert.ToString(gridSource[0].ItemCode);
            oldCode = Convert.ToString(mtxtCode.Text);
            if (gridSource[0].Image != string.Empty)
            {
                imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + ".jpeg", DocumentType.ItemPhoto);
                if (imageExist)
                    pbItem.Image = clsDocument.DownloadImage(mtxtCode.Text + ".jpeg", DocumentType.ItemPhoto);

            }
            mcmbItemClass.SelectedValue = gridSource[0].ItemClassId;
            mcmbTaxtype.SelectedValue = gridSource[0].TaxType;
            mtxtBarcode.Text = Convert.ToString(gridSource[0].Barcode);
            oldBarcode = Convert.ToString(gridSource[0].Barcode);
            mtxtCessper.Text = Convert.ToString(gridSource[0].CessPer);
            mtxtQuantity.Text = Convert.ToString(gridSource[0].PacketQty);
            mtxtCgstper.Text = Convert.ToString(gridSource[0].CGSTPer);
            mtxtHsn.Text = Convert.ToString(gridSource[0].HSNCode);
            mtxtMrp1.Text = Convert.ToString(gridSource[0].MRP1);
            mtxtMrp2.Text = Convert.ToString(gridSource[0].MRP2);
            mtxtSearchName.Text = Convert.ToString(gridSource[0].SearchName);
            mtxtBrand.Text = Convert.ToString(gridSource[0].BrandName);
            mtxtName.Text = Convert.ToString(gridSource[0].ItemName);
            mtxtPRate1.Text = Convert.ToString(gridSource[0].PurchaseRate1);
            mtxtPRate2.Text = Convert.ToString(gridSource[0].PurchaseRate2);
            mtxtQoh.Text = Convert.ToString(gridSource[0].QuantityOnHand);
            mtxtRack.Text = Convert.ToString(gridSource[0].Rack);
            mtxtRemark.Text = Convert.ToString(gridSource[0].Remarks);
            mtxtSgstper.Text = Convert.ToString(gridSource[0].SGSTPer);
            mtxtTaxper.Text = Convert.ToString(gridSource[0].IGSTPer);
            mtxtSalesPercentage.Text = Convert.ToString(gridSource[0].SellingPer);
            if (Convert.ToString(gridSource[0].ExpiryDate) != string.Empty && mdptExpDate.MinDate < gridSource[0].ExpiryDate)
                mdptExpDate.Value = Convert.ToDateTime(gridSource[0].ExpiryDate);
            mtxtSRate1.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(gridSource[0].PurchaseRate1) + ((Convert.ToDecimal(gridSource[0].SellingPer)) * Convert.ToDecimal(gridSource[0].PurchaseRate1)) / 100).ToString("0.00"));
            mtxtSRate2.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(gridSource[0].PurchaseRate2) + ((Convert.ToDecimal(gridSource[0].SellingPer)) * Convert.ToDecimal(gridSource[0].PurchaseRate2)) / 100).ToString("0.00"));
            if (gridSource[0].SalesTaxIncl == 1)
            {
                mchkSales.Checked = true;
            }
            else
            {
                mchkSales.Checked = false;
            }
            if (gridSource[0].PurchaseTaxIncl == 1)
            {
                mchkPurchase.Checked = true;
            }
            else
            {
                mchkPurchase.Checked = false;
            }
            if (gridSource[0].LockSaleRate == 1)
            {
                mchkLsrYes.Checked = true;
            }
            else
            {
                mchkLsrNo.Checked = true;
            }
            if (gridSource[0].StockItem == 1)
            {
                mchkStckItmYes.Checked = true;
            }
            else
            {
                mchkStckItmNo.Checked = true;
            }
            if (gridSource[0].BarcodeMode == 1)
            {
                mchkWMachine.Checked = true;
            }
            if (gridSource[0].BarcodeMode == 0)
            {
                mchkWMachine.Checked = false;
            }
            mcmbCategory.Focus();
            mbtnDelete.Enabled = false;
            mbtnSave.Enabled = false;
            mbtnList.Enabled = false;
            mbtnRefresh.Enabled = false;
            hlblHeader.Text = "Item [Edit]";

           
        }
        public bool IsSavable()
        {
            Control[] controllist = { mcmbCategory, mtxtName, mtxtCode, mtxtBarcode, mtxtBrand,mtxtMrp1, mtxtName, mtxtQoh, mcmbItemClass,mcmbMainUnit, mcmbTaxtype, mtxtSRate1, mtxtPRate1, mtxtMrp1 };
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
            oldCode = "";
            oldCategory = 0;
            MetroTextBox[] listTbx = { mtxtBrand, mtxtBarcode, mtxtCessper, mtxtCgstper, mtxtCode, mtxtHsn, mtxtMrp1, mtxtMrp2, mtxtName,mtxtPRate1,
                mtxtPRate2, mtxtQoh, mtxtRack, mtxtRemark, mtxtSgstper, mtxtSRate1, mtxtSRate2, mtxtTaxper,mtxtQuantity,mtxtSearchName,mtxtSalesPercentage};
            objGeneral.ResetSelectedMetroTextBoxes(listTbx);
            ItemId = 0;
            taxPer = 0;
            hlblHeader.Text = "Item";
            pbItem.Image = null;
            mchkLsrYes.Checked = true;
            mchkStckItmYes.Checked = true;
            mchkPurchase.Checked = false;
            mchkSales.Checked = false;
            mbtnDelete.Enabled = false;
            mchkWMachine.Checked = false;
            FillCategoryComboBox();
            FillMainUnitComboBox();
            FillSubUnitComboBox();
            FillItemTypeComboBox();
            FillTaxTypeComboBox();
            mcmbCategory.Focus();
            mtxtPath = string.Empty;
            mtxtSearchName.Text = "";
            oldCategory = 0;
            oldCode = "";
            MainUnit = "";
            mtxtBarcode.Text = "";
            mtxtCode.Text = "";
            oldBarcode = "";
            mtxtQoh.Text = "0";
            lblpckt.Enabled = false;
            mbtnSave.Enabled = false;
            #region tooltip

            mttImage.AutoPopDelay = 5000;
            mttImage.InitialDelay = 1000;
            mttImage.ReshowDelay = 500;
            mttImage.ShowAlways = true;
            mttImage.SetToolTip(this.mpbAdd, "Add Product Image");
            mttImage.SetToolTip(this.mpbRemove, "Remove Product Image");
            mttImage.SetToolTip(this.pbItem, "Product Image");

            #endregion
            if(string.IsNullOrWhiteSpace(mtxtName.Text))
            {
                mbtnSave.Enabled = false;
            }
        }
        public void FillCategoryComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "CategoryId, CategoryName";
            objSearch.Table = " tblCategory";
            objSearch.WhereNdGroupBy = " Where (Status = 0 OR Status = 2) AND BranchId = " + BranchId;
            objSearch.OrderBy = " ORDER BY Value ASC";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbCategory.DataSource = gridSource;
            mcmbCategory.ValueMember = "Id";
            mcmbCategory.DisplayMember = "Value";
            mcmbCategory.SelectedIndex = mcmbCategory.FindString(" --Select-- ");
        }
        public void FillMainUnitComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "UnitId,UnitName";
            objSearch.Table = " tblUnit ";
            objSearch.WhereNdGroupBy = " Where (Status = 0 OR Status = 2) AND BranchId = " + BranchId;
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbMainUnit.DataSource = gridSource;
            mcmbMainUnit.ValueMember = "Id";
            mcmbMainUnit.DisplayMember = "Value";
        }
        public void FillSubUnitComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "UnitId,UnitName";
            objSearch.Table = " tblUnit ";
            objSearch.WhereNdGroupBy = " Where (Status = 0 OR Status = 2) AND BranchId = " + BranchId;
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbSubUnit.DataSource = gridSource;
            mcmbSubUnit.ValueMember = "Id";
            mcmbSubUnit.DisplayMember = "Value";
        }
        public void FillItemTypeComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "ItemClassId,ItemClassName";
            objSearch.Table = " tblItemClass ";
            objSearch.WhereNdGroupBy = " Where (Status = 0 OR Status = 2) AND BranchId = " + BranchId;
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbItemClass.DataSource = gridSource;
            mcmbItemClass.ValueMember = "Id";
            mcmbItemClass.DisplayMember = "Value";
            mcmbItemClass.SelectedIndex = mcmbItemClass.FindStringExact("General");
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
            mcmbTaxtype.DataSource = gridSource;
            mcmbTaxtype.ValueMember = "Id";
            mcmbTaxtype.DisplayMember = "Value";
            mcmbTaxtype.SelectedIndex = 1;
        }
        public void SearchText()
        {
            if (mchkWithUnit.Checked == true)
            {
                if(mcmbMainUnit.SelectedIndex > 0)
                {
                    MainUnit = " " + mcmbMainUnit.Text.ToString();
                }
                mtxtSearchName.Text = mtxtBrand.Text.Trim() + " " + mtxtName.Text.Trim() + MainUnit + " " + mtxtQuantity.Text.Trim();
            }
            if(mchkWithUnit.Checked == false)
            {
                mtxtSearchName.Text = mtxtBrand.Text.Trim() + " " + mtxtName.Text.Trim();
            }
        }
        public void GetSerialNo()
        {
            int sel = Convert.ToInt32(mcmbCategory.SelectedIndex);
            if (sel > 0)
            {
                BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "CategoryCode,CategorySrlNoCurrent";
                objSearch.Table = " tblCategory ";
                objSearch.WhereNdGroupBy = "Where CategoryId = " + mcmbCategory.SelectedValue;
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                mtxtCode.Text = Convert.ToString(gridSource[0].CategoryCode) + Convert.ToString(gridSource[0].CategorySrlNoCurrent+1);
                //mtxtBarcode.Text = Convert.ToString(gridSource[0].CategorySrlNoCurrent+1);
                if (Convert.ToInt64(mcmbCategory.SelectedValue) == oldCategory)
                {
                    mtxtCode.Text = oldCode;
                    mtxtBarcode.Text = oldBarcode;
                }
                catStatus = 1;
               if (mtxtBarcode.Text == string.Empty)
                    mtxtBarcode.Text = Convert.ToString(gridSource[0].CategorySrlNoCurrent + 1);
                mchkWMachine.Checked = false;
            }
        }
        public void GetBarcode()
        {
            if (mchkWMachine.Checked == true)
            {
                mtxtBarcode.MaxLength = 5;
                BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "MAX(Barcode) as Barcode";
                objSearch.Table = " tblItem ";
                objSearch.WhereNdGroupBy = " Where BarcodeMode = 1";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                mtxtBarcode.Text = Convert.ToString(Convert.ToInt64(gridSource[0].Barcode) + 1);
                if (ItemId!=0)
                {
                    objSearch.Select = "MAX(Barcode) as Barcode";
                    objSearch.Table = " tblItem ";
                    objSearch.WhereNdGroupBy = " Where BarcodeMode = 1 and ItemId="+ ItemId;
                    gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    mtxtBarcode.Text = Convert.ToString(Convert.ToInt64(gridSource[0].Barcode));
                    if (gridSource.Count > 0)
                    {
                        if(Convert.ToString(gridSource[0].Barcode)=="0"|| Convert.ToString(gridSource[0].Barcode) == null)
                        {
                            objSearch.Select = "MAX(Barcode) as Barcode";
                            objSearch.Table = " tblItem ";
                            objSearch.WhereNdGroupBy = " Where BarcodeMode = 1";
                            gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                            mtxtBarcode.Text = Convert.ToString(Convert.ToInt64(gridSource[0].Barcode) + 1);
                        }
                    }
                }
               
                //else
                //{
                //    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                //    if (gridSource.Count > 0)
                //        mtxtBarcode.Text = Convert.ToString(Convert.ToInt64(gridSource[0].Barcode) + 1);
                //}

            }
            else
            {
                //if (ItemId != 0)
                //{
                //    mtxtBarcode.MaxLength = 18;
                //}
                //if (ItemId == 0)
                //{
                    mtxtBarcode.MaxLength = 18;
                    GetSerialNo();
                //}
            }
        }
        public bool BarcodeExist()
        {
            string barcode = Convert.ToString(mtxtBarcode.Text);
            if (barcode !="")
            {
                BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "Barcode";
                objSearch.Table = " tblItem ";
                objSearch.WhereNdGroupBy = "Where Barcode = '" + barcode + "'";
                if (ItemId != 0)
                {
                    objSearch.WhereNdGroupBy = "Where Barcode = '" + barcode + "' and ItemId!="+ ItemId;
                }
                objSearch.OrderBy = "Order By ItemId ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count >0)
                {
                    mtxtBarcode.Focus();
                    return true;
                }
            }
            return false;
        }
        public void getsalesprice1(string textBoxName)
        {
            decimal SalesRate1;

            if (!string.IsNullOrWhiteSpace(mtxtSalesPercentage.Text) && !string.IsNullOrWhiteSpace(mtxtPRate1.Text) && !string.IsNullOrWhiteSpace(Convert.ToString(mtxtMrp1.Text)) && textBoxName != "mtxtSRate1")
            {
                SalesRate1 = Convert.ToDecimal(mtxtPRate1.Text) + ((Convert.ToDecimal(mtxtSalesPercentage.Text)) * Convert.ToDecimal(mtxtPRate1.Text)) / 100;
                mtxtSRate1.Text = Convert.ToString(SalesRate1.ToString("0.00"));
            }
            else if (!string.IsNullOrWhiteSpace(mtxtSRate1.Text) && !string.IsNullOrWhiteSpace(mtxtPRate1.Text) && !string.IsNullOrWhiteSpace(Convert.ToString(mtxtMrp1.Text)) && textBoxName == "mtxtSRate1")
            {
                mtxtSalesPercentage.Text = Convert.ToString(((Convert.ToDecimal(mtxtSRate1.Text) - Convert.ToDecimal(mtxtPRate1.Text)) * 100) / (Convert.ToDecimal(mtxtPRate1.Text)));
            }
        }
        public void getsalesprice2(string textBoxName)
        {
            decimal SalesRate2;

            if (!string.IsNullOrWhiteSpace(mtxtSalesPercentage.Text) && !string.IsNullOrWhiteSpace(mtxtPRate2.Text) && !string.IsNullOrWhiteSpace(Convert.ToString(mtxtMrp2.Text)) && textBoxName != "mtxtSRate2")
            {
                SalesRate2 = Convert.ToDecimal(mtxtPRate2.Text) + ((Convert.ToDecimal(mtxtSalesPercentage.Text)) * Convert.ToDecimal(mtxtPRate2.Text)) / 100;
                mtxtSRate2.Text = Convert.ToString(SalesRate2.ToString("0.00"));
            }
            else if (!string.IsNullOrWhiteSpace(mtxtSRate2.Text) && !string.IsNullOrWhiteSpace(mtxtPRate2.Text) && !string.IsNullOrWhiteSpace(Convert.ToString(mtxtMrp2.Text)) && textBoxName == "mtxtSRate2")
            {
                mtxtSalesPercentage.Text = Convert.ToString(((Convert.ToDecimal(mtxtSRate2.Text) - Convert.ToDecimal(mtxtPRate2.Text)) * 100) / (Convert.ToDecimal(mtxtPRate2.Text)));
            }
        }
        public bool GetSalesRate1()
        {
            if (!string.IsNullOrWhiteSpace(mtxtSRate1.Text) && !string.IsNullOrWhiteSpace(Convert.ToString(mtxtMrp1.Text)))
            {
                if (Convert.ToDecimal(mtxtSRate1.Text) <= Convert.ToDecimal(mtxtMrp1.Text))
                {
                    return true;
                }
                else
                {
                    mtxtMrp1.Focus();
                }
            }
            return false;
        }
        public bool GetSalesRate2()
        {
            if (!string.IsNullOrWhiteSpace(mtxtSRate2.Text) && !string.IsNullOrWhiteSpace(Convert.ToString(mtxtMrp2.Text)))
            {
                if (Convert.ToDecimal(mtxtSRate2.Text) <= Convert.ToDecimal(mtxtMrp2.Text))
                {
                    return true;
                }
                else
                {
                    mtxtMrp2.Focus();
                }
            }
            return false;
        }
        #endregion

        #region Events

        private void MbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //Image change and remove are here
                if (mtxtPath != string.Empty)
                {
                    MetroMessageBox.Show(this, "Remove File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                }
                else
                {
                    OpenFileDialog openFd = new OpenFileDialog();
                    openFd.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
                    DialogResult dr = openFd.ShowDialog();
                    if (openFd.FileName != "")
                    {
                        string ext = Path.GetExtension(openFd.FileName);
                        pbItem.Image = Image.FromFile(openFd.FileName);

                        #region create directory if not exists

                        bool exists = System.IO.Directory.Exists(imageFolder);
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(imageFolder);
                        }

                        #endregion

                    }
                    mbtnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + ".jpeg", DocumentType.ItemPhoto);
                if (imageExist)
                    clsDocument.DeleteImage(mtxtCode.Text + ".jpeg", DocumentType.ItemPhoto);
                pbItem.Image = null;
                mtxtPath = string.Empty;
                mbtnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void FrmItem_Load(object sender, EventArgs e)
        {
            try
            {
                //if (mtxtName.Text != null && mtxtName.Text !="")
                //    mbtnSave.Enabled = true;
                //RefreshForm();
               
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
                int savestate = 0;
                if (IsSavable())
                {
                    if (Convert.ToString(mcmbMainUnit.SelectedValue) != Convert.ToString(mcmbSubUnit.SelectedValue))
                    {
                        SearchText();

                        //if (BarcodeExist())
                        //{
                        //    MetroMessageBox.Show(this, "Barcode exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        //}
                        if (!GetSalesRate1())
                        {
                            savestate = 1;
                            MetroMessageBox.Show(this, "Sales Rate1 must be less than or equal to MRP1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                        else if ((Convert.ToString(mcmbSubUnit.SelectedValue)!="-1")&&!GetSalesRate2())
                        {
                            savestate = 1;
                            MetroMessageBox.Show(this, "Sales Rate2 must be less than or equal to MRP2", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                        else if ((Convert.ToString(mcmbSubUnit.SelectedValue) != "-1"))
                        {
                            if ((Convert.ToString(mtxtQuantity.Text) == "")||(Convert.ToInt64(mtxtQuantity.Text)<=0))
                            {
                                savestate = 1;
                                MetroMessageBox.Show(this, "Packet quantity not empty must be less than zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                            }
                        }
                        if(savestate == 0)
                        {
                            if (IsSavable())
                            {
                                bool savestatus;
                                DmnItem dmnItem = new DmnItem();
                                BllItem bllItem = new BllItem();
                                dmnItem.Barcode = Convert.ToString(mtxtBarcode.Text.Trim());
                                dmnItem.BranchId = BranchId;
                                dmnItem.CategoryId = Convert.ToInt32(mcmbCategory.SelectedValue);
                                if (mchkIgst.Checked == false)
                                {
                                    dmnItem.SGSTPer = decimal.Parse(mtxtSgstper.Text == string.Empty ? "0.00" : mtxtSgstper.Text);
                                    dmnItem.CGSTPer = decimal.Parse(mtxtCgstper.Text == string.Empty ? "0.00" : mtxtCgstper.Text);
                                    dmnItem.IGSTPer = Convert.ToDecimal("0.00");
                                }
                                else
                                {
                                    dmnItem.SGSTPer = Convert.ToDecimal("0.00");
                                    dmnItem.CGSTPer = Convert.ToDecimal("0.00");
                                    dmnItem.IGSTPer = decimal.Parse(mtxtTaxper.Text == string.Empty ? "0.00" : mtxtTaxper.Text);
                                }
                                dmnItem.CessPer = decimal.Parse(mtxtCessper.Text == string.Empty ? "0.00" : mtxtCessper.Text);
                                dmnItem.CreatedBy = UserId;
                                dmnItem.HSNCode = mtxtHsn.Text.Trim();
                                dmnItem.ItemCode = Convert.ToString(mtxtCode.Text.Trim());

                                dmnItem.ItemClassId = Convert.ToInt32(mcmbItemClass.SelectedValue);
                                dmnItem.ItemName = Convert.ToString(mtxtName.Text);
                                dmnItem.BrandName = Convert.ToString(mtxtBrand.Text);
                                if (mdptExpDate.Checked == true)
                                {
                                    string dateString = mdptExpDate.Text;
                                    string strDate = mdptExpDate.Text;
                                    string[] sa = strDate.Split('/');
                                    string strNew = sa[2] + "-" + sa[1] + "-" + sa[0];
                                    dmnItem.ExpiryDate = Convert.ToDateTime(strNew);
                                }
                                else
                                {
                                    dmnItem.ExpiryDate = null;
                                }

                                dmnItem.PacketQty = long.Parse(mtxtQuantity.Text == string.Empty ? "0" : mtxtQuantity.Text);
                                dmnItem.SearchName = mtxtSearchName.Text.Trim();
                                dmnItem.QuantityOnHand = Convert.ToDecimal(mtxtQoh.Text);
                                if (mchkStckItmYes.Checked == true)
                                {
                                    dmnItem.LockSaleRate = 1;
                                }
                                if (mchkStckItmNo.Checked == true)
                                {
                                    dmnItem.LockSaleRate = 0;
                                }
                                if (mchkLsrYes.Checked == true)
                                {
                                    dmnItem.LockSaleRate = 1;
                                }
                                if (mchkLsrNo.Checked == true)
                                {
                                    dmnItem.LockSaleRate = 0;
                                }
                                if (mchkWMachine.Checked == true)
                                {
                                    dmnItem.BarcodeMode = 1;
                                }
                                if (mchkWMachine.Checked == false)
                                {
                                    dmnItem.BarcodeMode = 0;
                                }
                                dmnItem.MRP1 = decimal.Parse(mtxtMrp1.Text == string.Empty ? "0.00" : mtxtMrp1.Text);
                                dmnItem.MRP2 = decimal.Parse(mtxtMrp2.Text == string.Empty ? "0.00" : mtxtMrp2.Text);
                                dmnItem.PurchaseRate1 = decimal.Parse(mtxtPRate1.Text == string.Empty ? "0.00" : mtxtPRate1.Text);
                                dmnItem.PurchaseRate2 = decimal.Parse(mtxtPRate2.Text == string.Empty ? "0.00" : mtxtPRate2.Text);

                                //last rate update

                                dmnItem.LastMRP1 = decimal.Parse(mtxtMrp1.Text == string.Empty ? "0.00" : mtxtMrp1.Text);
                                dmnItem.LastMRP2 = decimal.Parse(mtxtMrp2.Text == string.Empty ? "0.00" : mtxtMrp2.Text);
                                dmnItem.LastPurchaseRate1 = decimal.Parse(mtxtPRate1.Text == string.Empty ? "0.00" : mtxtPRate1.Text);
                                dmnItem.LastPurchaseRate2 = decimal.Parse(mtxtPRate2.Text == string.Empty ? "0.00" : mtxtPRate2.Text);
                                dmnItem.LastSellingPer = decimal.Parse(mtxtSalesPercentage.Text == string.Empty ? "0.00" : mtxtSalesPercentage.Text);


                                dmnItem.QuantityOnHand = decimal.Parse(mtxtQoh.Text == string.Empty ? "0.00" : mtxtQoh.Text);
                                if (mchkPurchase.Checked == true)
                                {
                                    dmnItem.PurchaseTaxIncl = 1;
                                }
                                else
                                {
                                    dmnItem.PurchaseTaxIncl = 0;
                                }
                                if (mchkSales.Checked == true)
                                {
                                    dmnItem.SalesTaxIncl = 1;
                                }
                                else
                                {
                                    dmnItem.SalesTaxIncl = 0;
                                }
                                dmnItem.Rack = mtxtRack.Text.Trim();
                                dmnItem.Remarks = mtxtRemark.Text.Trim();
                                dmnItem.SellingPer = decimal.Parse(mtxtSalesPercentage.Text == string.Empty ? "0.00" : mtxtSalesPercentage.Text);
                                if (mchkStckItmNo.Checked == true)
                                {
                                    dmnItem.StockItem = 0;
                                }
                                if (mchkStckItmYes.Checked == true)
                                {
                                    dmnItem.StockItem = 1;
                                }
                                dmnItem.TaxType = int.Parse(Convert.ToString(mcmbTaxtype.SelectedValue) == string.Empty ? "0" : Convert.ToString(mcmbTaxtype.SelectedValue));
                                dmnItem.SubUnitId = int.Parse(Convert.ToString(mcmbSubUnit.SelectedValue) == string.Empty ? "0" : Convert.ToString(mcmbSubUnit.SelectedValue));
                                dmnItem.MainUnitId = int.Parse(Convert.ToString(mcmbMainUnit.SelectedValue) == string.Empty ? "0" : Convert.ToString(mcmbMainUnit.SelectedValue));
                                dmnItem.Status = 0;
                                if (ItemId == 0)
                                {
                                    GetSerialNo();
                                    GetBarcode();
                                    dmnItem.ItemCode = Convert.ToString(mtxtCode.Text.Trim());
                                    dmnItem.Barcode = Convert.ToString(mtxtBarcode.Text.Trim());
                                    DmnCategory dmnCategory = new DmnCategory();
                                    BllCategory bllCategory = new BllCategory();
                                    dmnCategory.CategoryId = Convert.ToInt64(mcmbCategory.SelectedValue);
                                    var lstDataSrlno = bllCategory.UpdateSrl("prdUpdateCategorySrl", dmnCategory);
                                    ClsGeneral objGeneral = new ClsGeneral();
                                    Control[] controllist = { mtxtCode };
                                    if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
                                    {
                                    }
                                    else
                                    {
                                        if (pbItem.Image != null)
                                        {
                                            dmnItem.Image = mtxtCode.Text + ".jpeg";
                                            bitmap = new Bitmap(pbItem.Image);
                                        }
                                        else
                                        {
                                            imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + ".jpeg", DocumentType.ItemPhoto);
                                            if (imageExist)
                                                clsDocument.DeleteImage(mtxtCode.Text + ".jpeg", DocumentType.ItemPhoto);
                                            dmnItem.Image = "";
                                        }
                                    }
                                    dmnItem.CreatedBy = UserId;
                                    var lstData = bllItem.AddData("prdInsertItem", dmnItem);
                                    this._retValue = Convert.ToString(lstData.ItemId);
                                   

                                    if (lstData.ItemId>0)
                                    {
                                        if (pbItem.Image != null)
                                            clsDocument.UploadImage(bitmap, mtxtCode.Text + ".jpeg", DocumentType.ItemPhoto);
                                        MetroMessageBox.Show(this, "Successfully Saved " + mtxtName.Text, "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                                    }
                                    else
                                        MetroMessageBox.Show(this, "Save Failed " + mtxtName.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                                }
                                else
                                {
                                    //GetBarcode();
                                    //dmnItem.ItemCode = Convert.ToString(mtxtCode.Text.Trim());
                                    //dmnItem.Barcode = Convert.ToString(mtxtBarcode.Text.Trim());
                                    if (oldCategory != Convert.ToInt64(mcmbCategory.SelectedValue))
                                    {
                                        GetSerialNo();
                                        GetBarcode();
                                        dmnItem.ItemCode = Convert.ToString(mtxtCode.Text.Trim());
                                        dmnItem.Barcode = Convert.ToString(mtxtBarcode.Text.Trim());
                                        DmnCategory dmnCategory = new DmnCategory();
                                        BllCategory bllCategory = new BllCategory();
                                        dmnCategory.CategoryId = Convert.ToInt64(mcmbCategory.SelectedValue);
                                        var lstDataSrlno = bllCategory.UpdateSrl("prdUpdateCategorySrl", dmnCategory);
                                    }
                                    ClsGeneral objGeneral = new ClsGeneral();
                                    Control[] controllist = { mtxtCode };
                                    if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
                                    {
                                    }
                                    else
                                    {
                                        if (pbItem.Image != null)
                                        {
                                            dmnItem.Image = mtxtCode.Text + ".jpeg";
                                            bitmap = new Bitmap(pbItem.Image);
                                        }
                                        else
                                        {
                                            imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + ".jpeg", DocumentType.ItemPhoto);
                                            if (imageExist)
                                                clsDocument.DeleteImage(mtxtCode.Text + ".jpeg", DocumentType.ItemPhoto);
                                            dmnItem.Image = "";
                                        }
                                    }
                                    dmnItem.ModifiedBy = UserId;
                                    dmnItem.ModifiedDateTime = DateTime.Now;
                                    dmnItem.ItemId = ItemId;
                                    var lstData = bllItem.UpdateData("prdUpdateItem", dmnItem);
                                    savestatus = lstData;
                                    if (savestatus)
                                    {
                                        if (pbItem.Image != null)
                                            clsDocument.UploadImage(bitmap, mtxtCode.Text + ".jpeg", DocumentType.ItemPhoto);
                                        imageExist = clsDocument.FileExistOrNot(oldCode + ".jpeg", DocumentType.ItemPhoto);
                                        if (oldCode != Convert.ToString(mtxtCode.Text) && imageExist)
                                            clsDocument.DeleteImage(oldCode + ".jpeg", DocumentType.ItemPhoto);
                                        MetroMessageBox.Show(this, "Updated successfully " + mtxtName.Text, "Updated  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                                    }
                                    else
                                        MetroMessageBox.Show(this, "Save Failed " + mtxtName.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                                }

                                RefreshForm();
                                //fillGridView();
                            }
                        }
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "Main unit and sub unit are same!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    }
                }               
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void FrmItem_KeyDown(object sender, KeyEventArgs e)
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
        private void McmbTaxtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int sel = Convert.ToInt32(mcmbTaxtype.SelectedIndex);
                if (sel >0)
                {
                    BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.Select = "GTypeSubValue1 ";
                    objSearch.Table = " tblGeneralOptions ";
                    objSearch.WhereNdGroupBy = "Where GeneralId = " + mcmbTaxtype.SelectedValue;
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    taxPer = Convert.ToDecimal(gridSource[0].GTypeSubValue1);
                    mtxtTaxper.Text = Convert.ToString(gridSource[0].GTypeSubValue1);
                    mtxtCgstper.Text = Convert.ToString(gridSource[0].GTypeSubValue1 / 2);
                    mtxtSgstper.Text = Convert.ToString(gridSource[0].GTypeSubValue1 / 2);
                    if (Convert.ToInt64(gridSource[0].GTypeSubValue1) == 5)
                        mtxtCessper.Text = "0";
                    else if (Convert.ToInt64(gridSource[0].GTypeSubValue1) == 0)
                        mtxtCessper.Text = "0";
                    else if (Convert.ToInt64(gridSource[0].GTypeSubValue1) < 5)
                        mtxtCessper.Text = "0.25";
                    else if(Convert.ToInt64(gridSource[0].GTypeSubValue1) > 5)
                        mtxtCessper.Text = "1";
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool deletestatus;
                if(ItemId != 0)
                { 
                    DialogResult result = MetroMessageBox.Show(this, "Do You Want to delete " + mtxtName.Text.ToString() + " ? ", "Delete ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DmnItem dmnItem = new DmnItem();
                        BllItem bllItem = new BllItem();
                        dmnItem.ItemId = ItemId;
                        var lstData = bllItem.DeleteById("prdDeleteItem", dmnItem);
                        deletestatus = lstData;

                        if (deletestatus)
                        {
                            imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + ".jpeg", DocumentType.ItemPhoto);
                            if (imageExist)
                                clsDocument.DeleteImage(mtxtCode.Text + ".jpeg", DocumentType.ItemPhoto);
                            dmnItem.Image = "";
                            MetroMessageBox.Show(this, "Successfully Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        }
                        else
                            MetroMessageBox.Show(this, "Delete Failed " + mtxtName.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    }
                }
                RefreshForm();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtQoh_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.DecimalTextBox3digits(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                
                mtxtPath = string.Empty;
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
                _objSearch.ColumnHeaders = "Id,Name,Code,Barcode,Category,Unit,PRate,SRate,MRP,Tax(%),QOH";
                _objSearch.Select = "I.ItemId as Id,I.SearchName as Field1,I.ItemCode as Field2,I.Barcode as Field3,C.CategoryName as Field4,U.UnitName as Field5, I.PurchaseRate1 as Field6," +
                    " (I.PurchaseRate1*(100+I.SellingPer)/100) as Field7, I.MRP1 as Field8, (I.IGSTPer+I.CGSTPer+I.SGSTPer) as Field9, I.QuantityOnHand as Field10";
                _objSearch.Table = " tblItem I LEFT JOIN tblCategory C  ON I.CategoryId = C.CategoryId LEFT JOIN tblUnit U ON I.MainUnitId=U.UnitId";
                _objSearch.WhereNdGroupBy = " WHERE I.Status=0 AND I.BranchId=" + BranchId;
                _objSearch.OrderBy = " ORDER BY I.ItemId DESC ";
                //using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 1200, 400, 100, 100))
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.AllCharacters, this.Width-90, (this.Height-120), this.Location.Y+50, this.Location.X+100,dtColumnAlign))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);
                    }
                }
                if (returnValue.ToString() != "")
                {
                    ItemId = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = " tblItem ";
                    objSearch.WhereNdGroupBy = "Where ItemId = " + ItemId;
                    objSearch.OrderBy = "Order By ItemName ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    mcmbCategory.SelectedValue = gridSource[0].CategoryId;
                    oldCategory = Convert.ToInt64(mcmbCategory.SelectedValue);
                    if(Convert.ToString(gridSource[0].BarcodeMode)=="0")
                    {
                        mchkWMachine.Checked = false;
                    }
                    else
                    {
                        mchkWMachine.Checked = true;
                    }
                    mcmbMainUnit.SelectedValue = gridSource[0].MainUnitId;
                    if (gridSource[0].SubUnitId != null)
                    mcmbSubUnit.SelectedValue = gridSource[0].SubUnitId;
                    mtxtCode.Text = Convert.ToString(gridSource[0].ItemCode);
                    oldCode = Convert.ToString(mtxtCode.Text);
                    if (gridSource[0].Image != string.Empty)
                    {
                        imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + ".jpeg", DocumentType.ItemPhoto);
                        if (imageExist)
                            pbItem.Image = clsDocument.DownloadImage(mtxtCode.Text + ".jpeg", DocumentType.ItemPhoto);

                    }
                    mcmbItemClass.SelectedValue = gridSource[0].ItemClassId;
                    mcmbTaxtype.SelectedValue = Convert.ToString(gridSource[0].TaxType);
                    mtxtBarcode.Text = Convert.ToString(gridSource[0].Barcode);
                    oldBarcode = Convert.ToString(gridSource[0].Barcode);
                    mtxtCessper.Text = Convert.ToString(gridSource[0].CessPer);
                    mtxtQuantity.Text = Convert.ToString(gridSource[0].PacketQty);
                    mtxtCgstper.Text = Convert.ToString(gridSource[0].CGSTPer);
                    mtxtHsn.Text = Convert.ToString(gridSource[0].HSNCode);
                    mtxtMrp1.Text = Convert.ToString(gridSource[0].MRP1);
                    mtxtMrp2.Text = Convert.ToString(gridSource[0].MRP2);
                    mtxtSearchName.Text = Convert.ToString(gridSource[0].SearchName);
                    mtxtBrand.Text = Convert.ToString(gridSource[0].BrandName);
                    mtxtName.Text = Convert.ToString(gridSource[0].ItemName);
                    mtxtPRate1.Text = Convert.ToString(gridSource[0].PurchaseRate1);
                    mtxtPRate2.Text = Convert.ToString(gridSource[0].PurchaseRate2);
                    mtxtQoh.Text = Convert.ToString(gridSource[0].QuantityOnHand);
                    mtxtRack.Text = Convert.ToString(gridSource[0].Rack);
                    mtxtRemark.Text = Convert.ToString(gridSource[0].Remarks);
                    mtxtSgstper.Text = Convert.ToString(gridSource[0].SGSTPer);
                    mtxtTaxper.Text = Convert.ToString(gridSource[0].IGSTPer);
                    mtxtSalesPercentage.Text = Convert.ToString(gridSource[0].SellingPer);
                    if(Convert.ToString(gridSource[0].ExpiryDate)!=string.Empty && mdptExpDate.MinDate < gridSource[0].ExpiryDate)
                    mdptExpDate.Value = Convert.ToDateTime(gridSource[0].ExpiryDate);
                    mtxtSRate1.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(gridSource[0].PurchaseRate1) + ((Convert.ToDecimal(gridSource[0].SellingPer)) * Convert.ToDecimal(gridSource[0].PurchaseRate1)) / 100).ToString("0.00"));
                    mtxtSRate2.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(gridSource[0].PurchaseRate2) + ((Convert.ToDecimal(gridSource[0].SellingPer)) * Convert.ToDecimal(gridSource[0].PurchaseRate2)) / 100).ToString("0.00"));
                    if (gridSource[0].SalesTaxIncl == 1)
                    {
                        mchkSales.Checked = true;
                    }
                    else
                    {
                        mchkSales.Checked = false;
                    }
                    if (gridSource[0].PurchaseTaxIncl == 1)
                    {
                        mchkPurchase.Checked = true;
                    }
                    else
                    {
                        mchkPurchase.Checked = false;
                    }
                    if (gridSource[0].LockSaleRate == 1)
                    {
                        mchkLsrYes.Checked = true;
                    }
                    else
                    {
                        mchkLsrNo.Checked = true;
                    }
                    if (gridSource[0].StockItem == 1)
                    {
                        mchkStckItmYes.Checked = true;
                    }
                    else
                    {
                        mchkStckItmNo.Checked = true;
                    }
                    if (gridSource[0].BarcodeMode == 1)
                    {
                        mchkWMachine.Checked = true;
                    }
                    if (gridSource[0].BarcodeMode == 0)
                    {
                        mchkWMachine.Checked = false;
                    }
                    mcmbCategory.Focus();
                    mbtnDelete.Enabled = true;
                    hlblHeader.Text = "Item [Edit]";
                    //if (mtxtName.Text != null && mtxtName.Text != "")
                    //    mbtnSave.Enabled = true;
                    if(string.IsNullOrWhiteSpace(mtxtName.Text))
                    {
                        mbtnSave.Enabled = false;
                    }
                }
               
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MchkIgst_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (mchkIgst.Checked == true)
                {
                    mtxtTaxper.Text = Convert.ToString(taxPer);
                    mtxtCgstper.Text = "0.00";
                    mtxtSgstper.Text = "0.00";
                }
                if (mchkIgst.Checked == false)
                {
                    mtxtCgstper.Text = Convert.ToString(taxPer / 2);
                    mtxtSgstper.Text = Convert.ToString(taxPer / 2);
                    mtxtTaxper.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mtxtName.Text != null && mtxtName.Text != "")
                    mbtnSave.Enabled = true;
                SearchText();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MchkWithUnit_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                SearchText();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MchkWMachine_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GetBarcode();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void McmbCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                GetSerialNo();
                if (Convert.ToString(mcmbCategory.SelectedIndex) == Convert.ToString(mcmbCategory.FindStringExact("Vegetables")))
                {
                    mtxtBrand.Text = "Vegetables";
                }
                if (Convert.ToString(mcmbCategory.SelectedIndex) == Convert.ToString(mcmbCategory.FindStringExact("Fruits")))
                {
                    mtxtBrand.Text = "Fruits";
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void McmbSubUnit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if ((Control)sender is MetroTextBox)
                {
                    string textboxName = ((MetroTextBox)sender).Name;
                    if ((Convert.ToString(mcmbSubUnit.SelectedValue) != "-1"))
                        lblpckt.Enabled = true;
                    else
                        lblpckt.Enabled = false;
                    getsalesprice1(textboxName);
                    if (mcmbSubUnit.SelectedIndex > -1)
                    {
                        getsalesprice2(textboxName);
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void McmbMainUnit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                if ((Control)sender is MetroTextBox)
                {
                    string textboxName = ((MetroTextBox)sender).Name;
                    getsalesprice1(textboxName);
                    if (mcmbSubUnit.SelectedIndex > -1)
                    {
                        getsalesprice2(textboxName);
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void McmbMainUnit_SelectionChangeCommitted(object sender, KeyEventArgs e)
        {
            try
            {
                string textboxName = ((MetroTextBox)sender).Name;
                if (textboxName == "mtxtSRate2")
                {
                    if (mcmbSubUnit.SelectedIndex > -1)
                    {
                        getsalesprice2(textboxName);
                    }
                    getsalesprice1(textboxName);
                }
                else
                {
                    getsalesprice1(textboxName);
                    if (mcmbSubUnit.SelectedIndex > -1)
                    {
                        getsalesprice2(textboxName);
                    }
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void McmbCategory_Click(object sender, EventArgs e)
        {
            try
            {
                long sel=Convert.ToInt64(mcmbCategory.SelectedValue);
                FillCategoryComboBox();
                mcmbCategory.SelectedValue = sel;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void McmbItemClass_Click(object sender, EventArgs e)
        {
            try
            {
                long sel = Convert.ToInt64(mcmbItemClass.SelectedValue);
                FillItemTypeComboBox();
                mcmbItemClass.SelectedValue = sel;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void McmbTaxtype_Click(object sender, EventArgs e)
        {
            try
            {
                long selt = Convert.ToInt64(mcmbTaxtype.SelectedValue);
                FillTaxTypeComboBox();
                mcmbTaxtype.SelectedValue = selt;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void McmbMainUnit_Click(object sender, EventArgs e)
        {
            try
            {
                long sel = Convert.ToInt64(mcmbMainUnit.SelectedValue);
                FillMainUnitComboBox();
                mcmbMainUnit.SelectedValue = sel;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void McmbSubUnit_Click(object sender, EventArgs e)
        {
            try
            {
                long sel = Convert.ToInt64(mcmbSubUnit.SelectedValue);
                FillSubUnitComboBox();
                mcmbSubUnit.SelectedValue = sel;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void FrmItem_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(mtxtName.Text))
                {
                    if(e.KeyCode!=Keys.F2  && e.KeyCode!=Keys.F3 && e.KeyCode!=Keys.F5 && e.KeyCode!=Keys.F7 && e.KeyCode!=Keys.F12)
                    {
                        mbtnSave.Enabled = true;
                    }
                } 
                if (e.KeyCode == Keys.F2 && mbtnSave.Enabled==true)
                    MbtnSave_Click(sender, e);
                if (e.KeyCode == Keys.F3)
                    MbtnList_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    MbtnRefresh_Click(sender, e);
                if (e.KeyCode == Keys.F7)
                    MbtnDelete_Click(sender, e);
                if (e.KeyCode == Keys.Escape)
                    this.Close();
                if (e.KeyCode == Keys.F10)
                    MbtnAdd_Click(sender, e);
                if (e.KeyCode == Keys.F11)
                    MtnRemove_Click(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void FrmItem_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (_retValue != "")
                    this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mcmbCategory_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F12)
                {
                    FrmCategory frmCategory = new FrmCategory();
                    frmCategory.ShowDialog();
                    FillCategoryComboBox();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mcmbMainUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F12)
                {
                    FrmUnit frmUnit = new FrmUnit();
                    frmUnit.ShowDialog();
                    FillMainUnitComboBox();
                    FillSubUnitComboBox();
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
