using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;


namespace FoodandBeverage.Forms
{
    public partial class FrmPartyPackSetup : Form
    {
        #region Declaration
        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsgen = new ClsGeneral();

        #region Grid Declaration
        public int Rslno = 0;
        public int Ritemno = 1;
        public int Rcategmenuno = 2;
        public int Rqtyno = 3;
        public int Ractionno = 4;
        public int PartyPackDetailsId = 5;

        public int Cmslno = 0;
        public int Cmitemno = 1;
        public int Ccategmenuno = 2;
        public int Cmqtyno = 3;
        public int Cmactionno = 4;
        public int CmPartyPackCustomDetailsId = 5;
        public int Cmpriceno = 6;

        public int Cardslno = 0;
        public int Cardselectno = 1;
        public int Carditemno = 2;
        public int Cardcategmenuno = 3;
        public int Cardqtyno = 4;
        public int CardProductid = 5;
        public int Cardproducttype = 6;
        public int CardPartyPackDetailsId = 7;

        public int Giftslno = 0;
        public int Giftselectno = 1;
        public int Giftitemno = 2;
        public int Giftqtyno = 3;
        public int GiftProductid = 4;
        public int GiftProducttype = 5;
        public int GiftPartyPackDetailsId = 6;

        #endregion

        bool Loaddetailsflag = false;
        bool Loadcustomflag = false;

        int currentPackId = 0;

        private List<DmnCategory> categoryList = new List<DmnCategory>();

        public class SelectedCardItem
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Qty { get; set; }
            public int ProductType { get; set; }
        }
        public class DmnGiftProducts
        {
            public long ProductId { get; set; }
            public string Description { get; set; }
        }
        public class DmnCardProducts
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Type { get; set; }
            public string Category { get; set; }  
        }

        List<SelectedCardItem> selectedCardItems = new List<SelectedCardItem>();

        public string GameDB = Convert.ToString(ConfigurationManager.AppSettings["GameDB"]);

        int selectedCakeId = 0;

        private readonly Image Grid_Remove = Properties.Resources.Round_Close;
        #endregion

        public FrmPartyPackSetup()
        {
            InitializeComponent();
        }
        private void FrmPartyPackSetup_Load(object sender, EventArgs e)
        {
            try
            {
                textb_shape();

                if (FrmLogin.GameBranchId > 0)
                {
                    btn_giftpacks.Visible = true;
                    btn_cardpacks.Visible = true;
                }
                else
                {
                    btn_giftpacks.Visible = false;
                    btn_cardpacks.Visible = false;
                }

                Taxfill();
                LoadMenuCategory();
                LoadMenuCategory_Cutom();

                ClearCake();

                dgv_packs_default.BringToFront();
                dgv_packs_default.Visible = true;

                if (dgv_packs_default.Rows.Count == 0)
                    dgv_packs_default.Rows.Add();

                RefreshSlNo();

                dgv_packs_default.CurrentCellDirtyStateChanged += (s, ev) =>
                {
                    if (dgv_packs_default.IsCurrentCellDirty)
                        dgv_packs_default.CommitEdit(DataGridViewDataErrorContexts.Commit);
                };

                dgv_card.CurrentCellDirtyStateChanged += (s, ec) =>
                {
                    if (dgv_card.IsCurrentCellDirty)
                        dgv_card.CommitEdit(DataGridViewDataErrorContexts.Commit);
                };

                if (dgv_packcustom.Rows.Count == 0)
                    dgv_packcustom.Rows.Add();

                RefreshCustomSlNo();

                dgv_packcustom.CurrentCellDirtyStateChanged += (s, ev) =>
                {
                    if (dgv_packcustom.IsCurrentCellDirty)
                        dgv_packcustom.CommitEdit(DataGridViewDataErrorContexts.Commit);
                };

                if (FrmDashboard.DefaultTaxType != "")
                {
                    cmbTaxType.Text = FrmDashboard.DefaultTaxType;
                    if (cmbTaxType.Text == "GST")
                    {
                        cmbIgstPrcnt.SelectedValue = Convert.ToInt64(FrmDashboard.DefaultTaxsettingid);
                    }
                    else if (cmbTaxType.Text == "VAT")
                    {
                        cmbVatPrcnt.SelectedValue = Convert.ToInt64(FrmDashboard.DefaultTaxsettingid);
                    }
                }

                cmbtax.SelectedIndex = 0;

                if (FrmDashboard.DefaultTaxType != "")
                {
                    cmbTaxType_cake.Text = FrmDashboard.DefaultTaxType;
                    if (cmbTaxType_cake.Text == "GST")
                    {
                        cmbIgstPrcnt_cake.SelectedValue = Convert.ToInt64(FrmDashboard.DefaultTaxsettingid);
                    }
                    else if (cmbTaxType_cake.Text == "VAT")
                    {
                        cmbVatPrcnt_cake.SelectedValue = Convert.ToInt64(FrmDashboard.DefaultTaxsettingid);
                    }
                }
                cmbtax_cake.SelectedIndex = 0;
            }
            catch(Exception ex)
            {

            }
            }

        private bool HasEmptyRow()
        {
            foreach (DataGridViewRow row in dgv_packs_default.Rows)
            {
                if (row.IsNewRow) continue;

                var val = row.Cells[Ritemno].Value;

                if (val == null || string.IsNullOrWhiteSpace(val.ToString()))
                    return true;
            }
            return false;
        }

        public void Taxfill()
        {
            try
            {
                BllSearch<DmnComboBox> fiilgst = new BllSearch<DmnComboBox>();
                BllSearch<DmnComboBox> fiilvat = new BllSearch<DmnComboBox>();
                DmnSearch objSearch1 = new DmnSearch();
                DmnSearch objSearch2 = new DmnSearch();

                objSearch1.Select = "TaxSettingsId as Id, IgstPrcnt as Value";
                objSearch1.Table = "tblTaxSettings";
                objSearch1.WhereNdGroupBy = "where status=1 and TaxType='GST'";
                objSearch1.OrderBy = "order by value";

                objSearch2.Select = "TaxSettingsId as Id, VatPrcnt as Value";
                objSearch2.Table = "tblTaxSettings";
                objSearch2.WhereNdGroupBy = "where isnull(status,0)=1 and TaxType='VAT'";
                objSearch2.OrderBy = "order by value";

                var gridSource1 = fiilgst.GetSearchData("prdComboFill", objSearch1);
                var gridSource2 = fiilvat.GetSearchData("prdComboFill", objSearch2);

                cmbVatPrcnt.DataSource = gridSource2;
                cmbVatPrcnt.ValueMember = "Id";
                cmbVatPrcnt.DisplayMember = "Value";

                cmbIgstPrcnt.DataSource = gridSource1;
                cmbIgstPrcnt.ValueMember = "Id";
                cmbIgstPrcnt.DisplayMember = "Value";

                cmbVatPrcnt_cake.DataSource = gridSource2;
                cmbVatPrcnt_cake.ValueMember = "Id";
                cmbVatPrcnt_cake.DisplayMember = "Value";

                cmbIgstPrcnt_cake.DataSource = gridSource1;
                cmbIgstPrcnt_cake.ValueMember = "Id";
                cmbIgstPrcnt_cake.DisplayMember = "Value";
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page Taxfill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void LoadMenuCategory()
        {
            try
            {
                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = "Fcid, CategoryName";
                objSearch.Table = "tblCategory_FTI";
                objSearch.WhereNdGroupBy = "WHERE Status = 1 AND CategoryType = 5";
                objSearch.OrderBy = "ORDER BY CategoryName";

                categoryList = bllSearch.GetSearchData("prdSearch", objSearch);

                // ✅ Bind to DataGridView ComboBox column
                DataGridViewComboBoxColumn cmb =
                    (DataGridViewComboBoxColumn)dgv_packs_default.Columns[Rcategmenuno];

                cmb.DataSource = categoryList;
                cmb.DisplayMember = "CategoryName";
                cmb.ValueMember = "Fcid";     
                
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void LoadMenuCategory_Cutom()
        {
            try
            {
                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = "Fcid, CategoryName";
                objSearch.Table = "tblCategory_FTI";
                objSearch.WhereNdGroupBy = "WHERE Status = 1 AND CategoryType = 5";
                objSearch.OrderBy = "ORDER BY CategoryName";

                categoryList = bllSearch.GetSearchData("prdSearch", objSearch);

                // ✅ Bind to DataGridView ComboBox column
                DataGridViewComboBoxColumn cmb =
                    (DataGridViewComboBoxColumn)dgv_packcustom.Columns[Ccategmenuno];

                cmb.DataSource = categoryList;
                cmb.DisplayMember = "CategoryName";
                cmb.ValueMember = "Fcid";

            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void cmbTaxType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int startRow = 3;
                int endRow = 6;
                int totalColumns = 4;

                for (int row = startRow; row <= endRow; row++)
                {
                    for (int col = 0; col < totalColumns; col++)
                    {
                        if ((row == 3 && col == 0) || (row == 3 && col == 1) || (row == 3 && col == 2))
                            continue;
                        if ((row == 4 && col == 0) || (row == 4 && col == 1) || (row == 4 && col == 2))
                            continue;

                        System.Windows.Forms.Control ctrl = tlp_registration.GetControlFromPosition(col, row);
                        if (ctrl != null)
                        {
                            tlp_registration.Controls.Remove(ctrl);
                        }
                    }
                }

                lbltax.Dock = DockStyle.Fill;
                lbltaxtype.Dock = DockStyle.Fill;

                lblminadvamt.Dock = DockStyle.Fill;
                txtminadvamt.Dock = DockStyle.Fill;
                tlp_time_del_contrl.Dock = DockStyle.Fill;

                if (cmbTaxType.Text == "GST")
                {
                    lbligst.Dock = DockStyle.Fill;
                    cmbIgstPrcnt.Dock = DockStyle.Fill;

                    lblcgst.Dock = DockStyle.Fill;
                    lblsgst.Dock = DockStyle.Fill;

                    //row1
                    tlp_registration.Controls.Add(lbligst, 3, 3);
                    tlp_registration.Controls.Add(lblcgst, 0, 5);
                    tlp_registration.Controls.Add(lblsgst, 1, 5); 

                    //row2
                    tlp_registration.Controls.Add(cmbIgstPrcnt, 3, 4);
                    tlp_registration.Controls.Add(txtCgstPrcnt, 0, 6);
                    tlp_registration.Controls.Add(txtSgstPrcnt, 1, 6);

               
                    tlp_registration.Controls.Add(lblminadvamt, 2, 5);
                    tlp_registration.Controls.Add(txtminadvamt, 2, 6);

                    //row3
                    tlp_registration.Controls.Add(tlp_time_del_contrl, 3, 6);

                }
                else if (cmbTaxType.Text == "VAT")
                {
                    cmbVatPrcnt.Dock = DockStyle.Fill;
                    lblvat.Dock = DockStyle.Fill;

                    //row1
                    tlp_registration.Controls.Add(lblvat, 3, 3);

                    //row2
                    tlp_registration.Controls.Add(cmbVatPrcnt, 3, 4);

                    tlp_registration.Controls.Add(lblminadvamt, 0, 5);
                    tlp_registration.Controls.Add(txtminadvamt, 0, 6);

                    //row3
                    tlp_registration.Controls.Add(tlp_time_del_contrl, 1, 6);

                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Partypacksetup page cmbTaxType_SelectedValueChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }
        private void cmbIgstPrcnt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbIgstPrcnt.SelectedIndex > 0)
                {
                    BllSearch<DmnTaxSettings> bllSearch = new BllSearch<DmnTaxSettings>();
                    DmnSearch objSearch_tax = new DmnSearch();
                    objSearch_tax = new DmnSearch();
                    objSearch_tax.Select = "TaxSettingsId, SgstPrcnt, CgstPrcnt";
                    objSearch_tax.Table = "tblTaxSettings";
                    objSearch_tax.WhereNdGroupBy = "Where TaxSettingsId = " + cmbIgstPrcnt.SelectedValue;
                    objSearch_tax.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch_tax);
                    if (gridSource.Count > 0)
                    {
                        txtSgstPrcnt.Text = gridSource[0].SgstPrcnt.ToString();
                        txtCgstPrcnt.Text = gridSource[0].CgstPrcnt.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Partypacksetup page cmbIgstPrcnt_SelectedIndexChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

       

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnclose_drawer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MandatoryCheck_PartyPack())
                    return;

                BllPartyPack bllPack = new BllPartyPack();

                DmnPartyPack dmn = new DmnPartyPack
                {
                    PartyPackId = currentPackId,
                    PackName = txtpackname.Text,

                    AdultPrice = string.IsNullOrWhiteSpace(txtadultprice.Text) ? 0 : Convert.ToDecimal(txtadultprice.Text),
                    ChildPrice = string.IsNullOrWhiteSpace(txtchildprice.Text) ? 0 : Convert.ToDecimal(txtchildprice.Text),

                    DiscountPer = string.IsNullOrWhiteSpace(txtDisPer.Text) ? 0 : Convert.ToDecimal(txtDisPer.Text),
                    DiscountAmt = string.IsNullOrWhiteSpace(txtDiscAmnt.Text) ? 0 : Convert.ToDecimal(txtDiscAmnt.Text),

                    TaxType = cmbTaxType.Text,

                    TaxSettingsId = cmbTaxType.Text == "GST"
                     ? Convert.ToInt32(cmbIgstPrcnt.SelectedValue)
                     : Convert.ToInt32(cmbVatPrcnt.SelectedValue),

                    Tax = cmbtax.Text,

                    CreatedBy = FrmDashboard.UserId,
                    BranchId = FrmDashboard.BranchId,
                    Status = 1,

                    MinimumAdvanceAmount = string.IsNullOrWhiteSpace(txtminadvamt.Text) ? 0 : Convert.ToDecimal(txtminadvamt.Text)
                };

                var packstatus = bllPack.AddEditData("prdInsert_Upd_PartyPack", dmn);

                if (packstatus.PartyPackId <= 0)
                {
                    CMessageBox.Show("Error saving Party Pack");
                    return;
                }

                #region Package Time (24-Hour Enforced)

                string[] days = {"Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"};
                CheckBox[] chkDays =
                                {
                    chk_sunday,
                    chk_monday,
                    chk_tuesday,
                    chk_wednesday,
                    chk_thursday,
                    chk_friday,
                    chk_saturday
                };

                BllPackageWorkTime bllPackTime = new BllPackageWorkTime();

                for (int i = 1; i <= 7; i++)
                {
                    var fPicker = tlp_timedetails.Controls["ftime" + i] as DateTimePicker;
                    var tPicker = tlp_timedetails.Controls["totime" + i] as DateTimePicker;

                    if (fPicker == null || tPicker == null)
                        continue;

                    int status = chkDays[i - 1].Checked ? 1 : 0;

                    DmnPackageWorkTime dmnPackTime = new DmnPackageWorkTime
                    {
                        PartyPackId = packstatus.PartyPackId,
                        BranchId = FrmDashboard.BranchId,
                        DayId = i,
                        FromTime = fPicker.Value.TimeOfDay,
                        ToTime = tPicker.Value.TimeOfDay,
                        Status = status,
                        CreatedBy = FrmDashboard.UserId,
                        CreatedDateTime = DateTime.Now
                    };

                    bool saveStatus = bllPackTime.AddEditPackageWorkTime("PrdInsert_Upd_PackageWorkTime",dmnPackTime);

                    if (!saveStatus)
                    {
                        MessageBox.Show($"Failed to save package time for {days[i - 1]}","Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }
                }

                #endregion

                #region Default

                int statuscount = 0;
                int gridcount = 0;

                foreach (DataGridViewRow row in dgv_packs_default.Rows)
                {
                    if (row.IsNewRow) continue;

                    string itemVal = row.Cells[Ritemno].Value?.ToString().Trim();
                    string qtyVal = row.Cells[Rqtyno].Value?.ToString().Trim();

                    if (!string.IsNullOrEmpty(itemVal) &&
                        int.TryParse(qtyVal, out int qty) && qty > 0)
                    {
                        gridcount++;

                        int detailsId = 0;
                        int.TryParse(row.Cells[PartyPackDetailsId].Value?.ToString(), out detailsId);

                        int menuCategoryId = 0;
                        int.TryParse(row.Cells[Rcategmenuno].Value?.ToString(), out menuCategoryId);

                        DmnPartyPackDetails dmndetail = new DmnPartyPackDetails
                        {
                            PartyPackId = packstatus.PartyPackId,
                            PartyPackDetailsId = detailsId,
                            Item = itemVal,
                            Quantity = qty,
                            CreatedBy = FrmDashboard.UserId,
                            BranchId = FrmDashboard.BranchId,
                            Status = 1,
                            MenuCategoryId = menuCategoryId,

                            PackItemType = (int)PackItemType.FoodItem,
                            Productid = 0,
                            ProductType = 0
                        };

                        var result = bllPack.AddData_packdetails("prdInsert_Upd_PartyPackDetails", dmndetail);

                        if (result != null && result.PartyPackDetailsId > 0)
                            statuscount++;
                    }
                }

                #endregion

                #region Gift

                int giftStatusCount = 0;
                int giftGridCount = 0;

                foreach (DataGridViewRow row in dgv_gift.Rows)
                {
                    if (row.IsNewRow) continue;

                    bool isSelected = false;

                    if (row.Cells[Giftselectno].Value != null)
                        bool.TryParse(row.Cells[Giftselectno].Value.ToString(), out isSelected);

                    if (!isSelected)
                        continue;

                    string itemVal = row.Cells[Giftitemno].Value?.ToString().Trim();
                    string qtyVal = row.Cells[Giftqtyno].Value?.ToString().Trim();

                    if (!string.IsNullOrEmpty(itemVal) &&
                        int.TryParse(qtyVal, out int qty) && qty > 0)
                    {
                        giftGridCount++;

                        int detailsId = 0;
                        int.TryParse(row.Cells[GiftPartyPackDetailsId].Value?.ToString(), out detailsId);

                        int productId = 0;
                        int.TryParse(row.Cells[GiftProductid].Value?.ToString(), out productId);

                        int productType = 0;
                        int.TryParse(row.Cells[GiftProducttype].Value?.ToString(), out productType);

                        DmnPartyPackDetails dmndetail = new DmnPartyPackDetails
                        {
                            PartyPackId = packstatus.PartyPackId,
                            PartyPackDetailsId = detailsId,
                            Item = itemVal,
                            Quantity = qty,
                            CreatedBy = FrmDashboard.UserId,
                            BranchId = FrmDashboard.BranchId,
                            Status = 1,
                            MenuCategoryId = 0,

                            PackItemType = (int)PackItemType.Gift,
                            Productid = productId,
                            ProductType = productType
                        };

                        var result = bllPack.AddData_packdetails(
                            "prdInsert_Upd_PartyPackDetails",
                            dmndetail);

                        if (result != null && result.PartyPackDetailsId > 0)
                            giftStatusCount++;
                    }
                }

                #endregion

                #region Card

                int cardStatusCount = 0;
                int cardGridCount = 0;

                if (selectedCardItems != null && selectedCardItems.Count > 0)
                {
                    foreach (var card in selectedCardItems)
                    {
                        if (card.ProductId <= 0 || card.Qty <= 0)
                            continue;

                        cardGridCount++;

                        DmnPartyPackDetails dmndetail = new DmnPartyPackDetails
                        {
                            PartyPackId = packstatus.PartyPackId,
                            PartyPackDetailsId = 0,
                            Item = card.ProductName,
                            Quantity = card.Qty,
                            CreatedBy = FrmDashboard.UserId,
                            BranchId = FrmDashboard.BranchId,
                            Status = 1,
                            MenuCategoryId = 0,

                            PackItemType = (int)PackItemType.Card,
                            Productid = card.ProductId,
                            ProductType = card.ProductType
                        };

                        var result = bllPack.AddData_packdetails(
                            "prdInsert_Upd_PartyPackDetails",
                            dmndetail);

                        if (result != null && result.PartyPackDetailsId > 0)
                            cardStatusCount++;
                    }
                }

                #endregion

                #region Custom
                int customStatusCount = 0;
                int customGridCount = 0;

                for (int i = 0; i < dgv_packcustom.Rows.Count; i++)
                {
                    var row = dgv_packcustom.Rows[i];
                    if (row.IsNewRow) continue;

                    string productVal = Convert.ToString(row.Cells[Cmitemno].Value);
                    string qtyVal = Convert.ToString(row.Cells[Cmqtyno].Value);
                    string priceVal = "0";//Convert.ToString(row.Cells[Cmpriceno].Value);
                    int menuid = Convert.ToInt32(row.Cells[Ccategmenuno].Value);

                    if (!string.IsNullOrWhiteSpace(productVal) &&
                        decimal.TryParse(qtyVal, out decimal qty) && qty > 0)
                    {
                        customGridCount++;

                        int detailsId = 0;
                        int.TryParse(Convert.ToString(row.Cells[CmPartyPackCustomDetailsId].Value), out detailsId);

                        DmnPartyPackCustom dmncusmenu = new DmnPartyPackCustom();

                        dmncusmenu.PartyPackCustomDetailsId = detailsId;
                        dmncusmenu.PartyPackId = 0;// packstatus.PartyPackId;
                        dmncusmenu.Product = Convert.ToString(productVal);
                        dmncusmenu.MenuCategoryId = menuid;
                        dmncusmenu.Qty = qty;
                        dmncusmenu.Price = string.IsNullOrEmpty(priceVal) ? 0 : Convert.ToDecimal(priceVal);
                        dmncusmenu.CreatedBy = FrmDashboard.UserId;
                        dmncusmenu.BranchId = FrmDashboard.BranchId;
                        dmncusmenu.Status = 1;
                        var status = bllPack.AddData_packcustom("prdInsert_Upd_PartyPackCustomMenu", dmncusmenu);

                        if (status.PartyPackCustomDetailsId > 0)
                            customStatusCount++;
                    }
                }
                #endregion

                if (gridcount == statuscount)
                {
                    CMessageBox.Show("Party Pack Saved Successfully");
                    clearall();
                }
                else
                    CMessageBox.Show("Some Items Not Saved Properly");
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }


        public bool MandatoryCheck_PartyPack()
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtadultprice.Text))
                    txtadultprice.Text = "0";

                if (string.IsNullOrWhiteSpace(txtDisPer.Text))
                    txtDisPer.Text = "0";

                if (string.IsNullOrWhiteSpace(txtDiscAmnt.Text))
                    txtDiscAmnt.Text = "0";

                if (string.IsNullOrWhiteSpace(txtpackname.Text))
                {
                    CMessageBox.Show("Please Enter Party Pack Name", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtpackname.Focus();
                    return false;
                }

                if (Convert.ToDecimal(txtadultprice.Text) <= 0)
                {
                    CMessageBox.Show("Please Enter Valid Price", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtadultprice.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(cmbTaxType.Text))
                {
                    CMessageBox.Show("Please Select Tax Type", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbTaxType.Focus();
                    return false;
                }
                if (cmbTaxType.Text == "GST")
                {
                    if (cmbIgstPrcnt.SelectedValue == null || Convert.ToInt32(cmbIgstPrcnt.SelectedValue) <= 0)
                    {
                        CMessageBox.Show("Please Select IGST %", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbIgstPrcnt.Focus();
                        return false;
                    }
                }
                else if (cmbTaxType.Text == "VAT")
                {
                    if (cmbVatPrcnt.SelectedValue == null || Convert.ToInt32(cmbVatPrcnt.SelectedValue) <= 0)
                    {
                        CMessageBox.Show("Please Select VAT %", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbVatPrcnt.Focus();
                        return false;
                    }
                }

                bool hasItem = false;
                foreach (DataGridViewRow row in dgv_packs_default.Rows)
                {
                    if (row.IsNewRow) continue;

                    var item = row.Cells[Ritemno].Value;

                    if (item != null && !string.IsNullOrWhiteSpace(item.ToString()))
                    {
                        hasItem = true;
                        var qty = row.Cells[Rqtyno].Value;

                        if (qty == null || Convert.ToInt32(qty) <= 0)
                        {
                            row.Cells[Rqtyno].Value = 1; // default
                        }
                    }
                }

                //if (!hasItem)
                //{
                //    CMessageBox.Show("Please Add At Least One Item in Pack", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    dgv_packs_default.Focus();
                //    return false;
                //}

                return true;
            }
            catch (Exception ex)
            {
                clsgen.LogException("PartyPack MandatoryCheck", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
                return false;
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
        }

        public void clearall()
        {
            try
            {
                currentPackId = 0;
                txtpackname.Text = "";
                txtadultprice.Text = "";
                txtchildprice.Text = "";
                txtDiscAmnt.Text = "";
                txtDisPer.Text = "";
                
                cmbtax.SelectedIndex = 0;
                cmbTaxType.SelectedIndex = 0;
                cmbIgstPrcnt.SelectedIndex = 0;

                txtCgstPrcnt_cake.Text = "";
                txtSgstPrcnt_cake.Text = "";

                txtCgstPrcnt.Text = "";
                txtSgstPrcnt.Text = "";

                cmbTaxType.SelectedIndex = 0;
                dgv_packs_default.Rows.Clear();
                dgv_packs_default.Rows.Add();

                dgv_packcustom.Rows.Clear();
                dgv_packcustom.Rows.Add();
                LoadCustomMenu();
                btn_del_pack.Visible = false;

                txtSgstPrcnt.Text = "";
                txtCgstPrcnt.Text = "";
                chb_active.Checked = true;

                cmbtax.SelectedIndex = 0;

                if (FrmDashboard.DefaultTaxType != "")
                {
                    cmbTaxType.Text = FrmDashboard.DefaultTaxType;
                    if (cmbTaxType.Text == "GST")
                    {
                        cmbIgstPrcnt.SelectedValue = Convert.ToInt64(FrmDashboard.DefaultTaxsettingid);
                    }
                    else if (cmbTaxType.Text == "VAT")
                    {
                        cmbVatPrcnt.SelectedValue = Convert.ToInt64(FrmDashboard.DefaultTaxsettingid);
                    }
                }
                btn_stanpackage.PerformClick();
                txtpackname.Focus();

                LoadMenuCategory_Cutom();

                btndel_cake.Visible = false;

                dgv_card.Rows.Clear();
                dgv_gift.Rows.Clear();
                selectedCardItems.Clear();

                ClearCake();

            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page clearall", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }

        }
        private void dgv_packs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Loaddetailsflag) return;

            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == Ritemno)
            {
                var row = dgv_packs_default.Rows[e.RowIndex];
                var value = row.Cells[Ritemno].Value;

                if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                {
                    // ✅ Default Qty
                    var qtyCell = row.Cells[Rqtyno];

                    if (qtyCell.Value == null ||
                        string.IsNullOrWhiteSpace(qtyCell.Value.ToString()) ||
                        Convert.ToInt32(qtyCell.Value) == 0)
                    {
                        qtyCell.Value = 1;
                    }

                    bool isLastRow = e.RowIndex == dgv_packs_default.Rows.Count - 1;

                    if (isLastRow && !HasEmptyRow())
                    {
                        dgv_packs_default.Rows.Add();
                    }

                    RefreshSlNo();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                int returnValue = 0;

                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();

                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "ID,Pack Name,Adult Price,Child Price";
                _objSearch.Select = "PartyPackId as Id, PackName as Field1, AdultPrice as Field2, ChildPrice as Field3";
                _objSearch.Table = "tblPartyPackMaster";
                _objSearch.WhereNdGroupBy = "WHERE Status = 1";
                _objSearch.OrderBy = " ORDER BY PackName";
                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);

                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records Found");
                }
                else
                {
                    using (var search = new FrmSearch<DmnGridFields>(
                        _objSearch,
                        SearchMode.FirstCharacter,
                        this.Width - 150,
                        this.Height - 150,
                        this.Location.Y + 50,
                        this.Location.X + 20,
                        dtColumnAlign))
                    {
                        var dialogResult = search.ShowDialog();

                        if (dialogResult == DialogResult.OK)
                        {
                            returnValue = Convert.ToInt32(search._retValue);
                        }
                    }

                    if (returnValue > 0)
                    {
                        FillData(returnValue);
                        LoadPackageWorkTime(returnValue);
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private void FillData(int id)
        {
            try
            {
                BllSearch<DmnPartyPack> bll = new BllSearch<DmnPartyPack>();

                DmnSearch search = new DmnSearch();
                search.Select = "*";
                search.Table = "tblPartyPackMaster";
                search.WhereNdGroupBy = "WHERE PartyPackId = " + id;

                var result = bll.GetSearchData("prdSearch", search);

                if (result.Count > 0)
                {
                    var data = result[0];

                    currentPackId = data.PartyPackId;

                    txtpackname.Text = data.PackName;
                    txtadultprice.Text = data.AdultPrice.ToString();
                    txtchildprice.Text = data.ChildPrice.ToString();
                    txtDisPer.Text = data.DiscountPer.ToString();
                    txtDiscAmnt.Text = data.DiscountAmt.ToString();
                    cmbTaxType.Text = data.TaxType;
                    cmbTaxType.Text = data.Tax;
                    txtDiscAmnt.Text = data.MinimumAdvanceAmount.ToString();

                    if (data.TaxType == "GST")
                        cmbIgstPrcnt.SelectedValue = Convert.ToInt64(data.TaxSettingsId);
                    else
                        cmbVatPrcnt.SelectedValue = Convert.ToInt64(data.TaxSettingsId);

                    btn_del_pack.Visible = true;

                    LoadGiftProducts();                
                    LoadCardProducts("ALL");  
                    
                    LoadPackDetails(id);               
                    ApplyCardSelectionToGrid();

                    textb_shape();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private void LoadPackageWorkTime(int branchId)
        {
            try
            {
                BllSearch<DmnPackageWorkTime> bll = new BllSearch<DmnPackageWorkTime>();
                DmnSearch search = new DmnSearch();
                search.Select = "*";
                search.Table = "tblPackageWorkTime";
                search.WhereNdGroupBy = "WHERE BranchId = " + branchId;

                var result = bll.GetSearchData("prdSearch", search);

                if (result.Count > 0)
                {
                    CheckBox[] chkDays =
                    {
                chk_sunday,
                chk_monday,
                chk_tuesday,
                chk_wednesday,
                chk_thursday,
                chk_friday,
                chk_saturday
            };

                    foreach (var data in result)
                    {
                        if (!data.DayId.HasValue)
                            continue;

                        int day = data.DayId.Value;

                        var fPicker = tlp_timedetails.Controls["ftime" + day] as DateTimePicker;
                        var tPicker = tlp_timedetails.Controls["totime" + day] as DateTimePicker;

                        if (fPicker != null && data.FromTime.HasValue)
                            fPicker.Value = DateTime.Today.Add(data.FromTime.Value);

                        if (tPicker != null && data.ToTime.HasValue)
                            tPicker.Value = DateTime.Today.Add(data.ToTime.Value);

                        if (day >= 1 && day <= 7)
                            chkDays[day - 1].Checked = data.Status.HasValue && data.Status.Value == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void LoadPackDetails(int packId = 0)
        {
            try
            {
                Loaddetailsflag = true;

                LoadMenuCategory();

                dgv_packs_default.Rows.Clear();

                BllSearch<DmnPartyPackDetails> bll = new BllSearch<DmnPartyPackDetails>();

                DmnSearch search = new DmnSearch();
                search.Select = "PartyPackDetailsId, Item, Quantity, MenuCategoryId, PackItemType, ProductId, ProductType";
                search.Table = "tblPartyPackDetails";
                search.WhereNdGroupBy = "WHERE PartyPackId = " + packId + " AND Status = 1";

                var list = bll.GetSearchData("prdSearch", search);

                selectedCardItems.Clear();

                foreach (DataGridViewRow gRow in dgv_gift.Rows)
                {
                    if (!gRow.IsNewRow)
                        gRow.Cells[Giftselectno].Value = false;
                }

                foreach (var item in list)
                {
                    // ------------------ FOOD ITEMS ------------------
                    if (item.PackItemType == (int)PackItemType.FoodItem)
                    {
                        int rowIndex = dgv_packs_default.Rows.Add();
                        var row = dgv_packs_default.Rows[rowIndex];

                        row.Cells[PartyPackDetailsId].Value = item.PartyPackDetailsId;
                        row.Cells[Ritemno].Value = item.Item;
                        row.Cells[Rqtyno].Value = item.Quantity;

                        if (item.MenuCategoryId > 0 &&
                            categoryList != null &&
                            categoryList.Any(x => x.Fcid == item.MenuCategoryId))
                        {
                            row.Cells[Rcategmenuno].Value = Convert.ToInt64(item.MenuCategoryId);
                        }
                    }
                    // ------------------ GIFT ITEMS ------------------
                    else if (item.PackItemType == (int)PackItemType.Gift)
                    {
                        foreach (DataGridViewRow gRow in dgv_gift.Rows)
                        {
                            if (gRow.IsNewRow) continue;

                            int gridProductId = 0;
                            int.TryParse(gRow.Cells[GiftProductid].Value?.ToString(), out gridProductId);

                            if (gridProductId == item.Productid)
                            {
                                gRow.Cells[Giftselectno].Value = true;
                                gRow.Cells[Giftqtyno].Value = item.Quantity;
                                gRow.Cells[GiftPartyPackDetailsId].Value = item.PartyPackDetailsId;
                                break;
                            }
                        }
                    }
                    // ------------------ CARD ITEMS ------------------
                    else if (item.PackItemType == (int)PackItemType.Card)
                    {
                        selectedCardItems.Add(new SelectedCardItem
                        {
                            ProductId = item.Productid,
                            ProductName = item.Item,
                            Qty = item.Quantity,
                            ProductType = item.ProductType
                        });
                    }
                }

                RefreshSlNo();

                if (!HasEmptyRow())
                    dgv_packs_default.Rows.Add();

                LoadCustomMenu();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
            finally
            {
                Loaddetailsflag = false;
            }
        }

        private void ApplyCardSelectionToGrid()
        {
            foreach (DataGridViewRow row in dgv_card.Rows)
            {
                if (row.IsNewRow) continue;

                int productId = 0;
                int.TryParse(row.Cells[CardProductid].Value?.ToString(), out productId);

                var selected = selectedCardItems.FirstOrDefault(x => x.ProductId == productId);

                if (selected != null)
                {
                    row.Cells[Cardselectno].Value = true;
                    row.Cells[Cardqtyno].Value = selected.Qty;
                    row.Cells[CardPartyPackDetailsId].Value = 0; 
                }
                else
                {
                    row.Cells[Cardselectno].Value = false;
                }
            }
        }

        private void LoadCustomMenu(int packId=0)
        {
            try
            {
                 Loadcustomflag = true;
                dgv_packcustom.Rows.Clear();

                BllSearch<DmnPartyPackCustom> bll = new BllSearch<DmnPartyPackCustom>();

                DmnSearch search = new DmnSearch();
                search.Select = "PartyPackCustomDetailsId, Product, Qty, Price,MenuCategoryId";
                search.Table = "tblPartyPackCustomMenu";
                if(packId==0)
                    search.WhereNdGroupBy = "WHERE status=1";
                else
                    search.WhereNdGroupBy = "WHERE status=1 and PartyPackId = " + packId;
                var list = bll.GetSearchData("prdSearch", search);

                LoadMenuCategory_Cutom();

                foreach (var item in list)
                {
                    int rowIndex = dgv_packcustom.Rows.Add();

                    dgv_packcustom.Rows[rowIndex].Cells[CmPartyPackCustomDetailsId].Value = item.PartyPackCustomDetailsId;
                    dgv_packcustom.Rows[rowIndex].Cells[Cmitemno].Value = item.Product;
                    dgv_packcustom.Rows[rowIndex].Cells[Cmqtyno].Value = item.Qty;

                    if (item.MenuCategoryId > 0)
                        dgv_packcustom.Rows[rowIndex].Cells[Ccategmenuno].Value = Convert.ToInt64(item.MenuCategoryId);

                    // dgv_packcustom.Rows[rowIndex].Cells[Cmpriceno].Value = item.Price;

                    DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_packcustom.Rows[rowIndex].Cells[Cmactionno];
                    cellcolumntype.Value = Grid_Remove;
                    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }

                RefreshCustomSlNo();
                if (!HasEmptyRowCustom())
                {
                    dgv_packcustom.Rows.Add();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
            finally
            {
                Loadcustomflag = false;
            }
        }

        private bool HasEmptyRowCustom()
        {
            foreach (DataGridViewRow row in dgv_packcustom.Rows)
            {
                if (row.IsNewRow) return true;

                if (row.Cells[Cmitemno].Value == null ||
                    string.IsNullOrWhiteSpace(row.Cells[Cmitemno].Value.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        private void RefreshCustomSlNo()
        {
            for (int i = 0; i < dgv_packcustom.Rows.Count; i++)
            {
                dgv_packcustom.Rows[i].Cells[Cmslno].Value = i + 1;
            }
        }
        private void RefreshSlNo()
        {
            int sl = 1;

            foreach (DataGridViewRow row in dgv_packs_default.Rows)
            {
                if (row.IsNewRow)
                {
                    row.Cells[Rslno].Value = null;
                    continue;
                }

                var val = row.Cells[Ritemno].Value;

                if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                {
                    row.Cells[Rslno].Value = sl++;
                }
                else
                {
                    row.Cells[Rslno].Value = null;
                }
            }
        }


        private void dgv_packs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                if (e.ColumnIndex == Ractionno)
                {
                    var row = dgv_packs_default.Rows[e.RowIndex];

                    if (row.IsNewRow || row.Cells[Ritemno].Value == null)
                        return;

                    DeleteGridRow(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private void DeleteGridRow(int rowIndex)
        {
            try
            {
                var row = dgv_packs_default.Rows[rowIndex];

                if (row.IsNewRow) return;

                DialogResult result = CMessageBox.Show(
                    "Are you sure you want to delete this item?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                int detailsId = 0;
                int.TryParse(Convert.ToString(row.Cells[PartyPackDetailsId].Value), out detailsId);

                if (detailsId == 0)
                {
                    // ✅ New row → direct remove
                    dgv_packs_default.Rows.RemoveAt(rowIndex);
                }
                else
                {
                    // ✅ DB soft delete
                    DmnPartyPackDetails dmn = new DmnPartyPackDetails();
                    BllPartyPack bll = new BllPartyPack();

                    dmn.PartyPackDetailsId = detailsId;
                    dmn.PartyPackId = currentPackId;
                    dmn.Item = Convert.ToString(row.Cells[Ritemno].Value);
                    dmn.Quantity = 0;
                    dmn.CreatedBy = FrmDashboard.UserId;
                    dmn.BranchId = FrmDashboard.BranchId;
                    dmn.Status = 0;

                    bll.AddData_packdetails("prdInsert_Upd_PartyPackDetails", dmn);

                    dgv_packs_default.Rows.RemoveAt(rowIndex);
                }

                // ✅ Always keep one empty row
                if (dgv_packs_default.Rows.Count == 0)
                    dgv_packs_default.Rows.Add();

                RefreshSlNo();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }


        private void dgv_packs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == Ractionno)
            {
                var row = dgv_packs_default.Rows[e.RowIndex];

                if (row.IsNewRow ||
                    row.Cells[Ritemno].Value == null ||
                    string.IsNullOrWhiteSpace(row.Cells[Ritemno].Value.ToString()))
                {
                    e.Value = null; // hide
                }
                else
                {
                    e.Value = Properties.Resources.Round_Close; // ✅ your image
                }
            }
        }



 

        private void dgv_packcustom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                if (e.ColumnIndex == Cmactionno)
                {
                    var row = dgv_packcustom.Rows[e.RowIndex];

                    if (row.IsNewRow || row.Cells[Cmitemno].Value == null)
                        return;

                    DeleteCustomGridRow(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void DeleteCustomGridRow(int rowIndex)
        {
            try
            {
                var row = dgv_packcustom.Rows[rowIndex];

                if (row.IsNewRow) return;

                DialogResult result = CMessageBox.Show(
                    "Are you sure you want to delete this item?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                int detailsId = 0;
                int.TryParse(Convert.ToString(row.Cells[CmPartyPackCustomDetailsId].Value), out detailsId);

                if (detailsId == 0)
                {
                    // ✅ New row → direct remove
                    dgv_packcustom.Rows.RemoveAt(rowIndex);
                }
                else
                {
                    // ✅ DB delete (hard delete OR soft logic)
                    BllPartyPack bll = new BllPartyPack();

                    DmnPartyPackCustom dmn = new DmnPartyPackCustom();
                    dmn.PartyPackCustomDetailsId = detailsId;
                    dmn.PartyPackId = currentPackId;
                    dmn.Product = Convert.ToString(row.Cells[Cmitemno].Value);
                    dmn.Qty = 0;
                    dmn.Price = 0;
                    dmn.CreatedBy = FrmDashboard.UserId;
                    dmn.BranchId = FrmDashboard.BranchId;

                    // 👉 You can create separate delete SP OR handle Qty=0 as delete
                    bll.AddData_packcustom("prdInsert_Upd_PartyPackCustomMenu", dmn);

                    dgv_packcustom.Rows.RemoveAt(rowIndex);
                }

                // ✅ Keep one empty row
                if (dgv_packcustom.Rows.Count == 0)
                    dgv_packcustom.Rows.Add();

                RefreshCustomSlNo();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private void dgv_packcustom_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == Cmactionno)
            {
                var row = dgv_packcustom.Rows[e.RowIndex];

                if (row.IsNewRow ||
                    row.Cells[Cmitemno].Value == null ||
                    string.IsNullOrWhiteSpace(row.Cells[Cmitemno].Value.ToString()))
                {
                    e.Value = null; // hide image
                }
                else
                {
                    e.Value = Properties.Resources.Round_Close; // show image
                }
            }
        }

        private void dgv_packcustom_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Loadcustomflag) return;

            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == Cmitemno)
            {
                var row = dgv_packcustom.Rows[e.RowIndex];
                var value = row.Cells[Cmitemno].Value;

                if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                {
                    // ✅ Default Qty = 1
                    var qtyCell = row.Cells[Cmqtyno];

                    if (qtyCell.Value == null ||
                        string.IsNullOrWhiteSpace(qtyCell.Value.ToString()) ||
                        Convert.ToDecimal(qtyCell.Value) == 0)
                    {
                        qtyCell.Value = 1;
                    }

                    // ✅ Optional: Auto set price (if product has price)
                    // row.Cells[Cmprice].Value = GetProductPrice(Convert.ToInt32(value));

                    bool isLastRow = e.RowIndex == dgv_packcustom.Rows.Count - 1;

                    if (isLastRow && !HasEmptyRowCustom())
                    {
                        dgv_packcustom.Rows.Add();
                    }

                    RefreshCustomSlNo();
                }
            }
        }

        private void btn_del_pack_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Check selected pack
                if (currentPackId == 0)
                {
                    CMessageBox.Show("Please select a pack to delete");
                    return;
                }

                // ✅ Confirmation
                DialogResult result = CMessageBox.Show(
                    "Are you sure you want to delete this pack?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;

                BllPartyPack bllPack = new BllPartyPack();

                DmnPartyPack dmn = new DmnPartyPack
                {
                    PartyPackId = currentPackId, // important for edit
                    PackName = txtpackname.Text,
                    AdultPrice = Convert.ToDecimal(txtadultprice.Text),
                    ChildPrice = Convert.ToDecimal(txtchildprice.Text),
                    DiscountPer = Convert.ToDecimal(txtDisPer.Text),
                    DiscountAmt = Convert.ToDecimal(txtDiscAmnt.Text),
                    TaxType = cmbTaxType.Text,
                    TaxSettingsId = cmbTaxType.Text == "GST"
                        ? Convert.ToInt32(cmbIgstPrcnt.SelectedValue)
                        : Convert.ToInt32(cmbVatPrcnt.SelectedValue),
                    CreatedBy = FrmDashboard.UserId,
                    BranchId = FrmDashboard.BranchId,
                    Status = 0
                };

                var packstatus = bllPack.AddEditData("prdInsert_Upd_PartyPack", dmn);

                CMessageBox.Show("Pack deleted successfully");
                clearall(); 

            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private void dgv_packs_default_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                if (e.ColumnIndex == Rcategmenuno)
                {
                    // Show panel
                    pnl_menucateg.Visible = true;
                    pnl_menucateg.BringToFront();

                    // Calculate 60% size
                    int width = (int)(this.Width * 0.6);
                    int height = (int)(this.Height * 0.3);

                    pnl_menucateg.Width = width;
                    pnl_menucateg.Height = height;

                    // Center panel
                    pnl_menucateg.Left = (this.Width - pnl_menucateg.Width) / 2;
                    pnl_menucateg.Top = (this.Height - pnl_menucateg.Height) / 2;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private void btncls_menucateg_Click(object sender, EventArgs e)
        {
            pnl_menucateg.Visible = false;
        }

        private void btn_savemenucateg_Click(object sender, EventArgs e)
        {
            try
            {
                DmnCategory dmnfood = new DmnCategory();
                BllCategory bllcategory = new BllCategory();

                if (string.IsNullOrWhiteSpace(txtfoodCate.Text))
                {
                    CMessageBox.Show("Enter Category Name");
                    return;
                }

                int fcid = 0;
                int.TryParse(txtFcid.Text, out fcid);

                dmnfood.Fcid = fcid;
                dmnfood.CategoryName = txtfoodCate.Text;
                dmnfood.CategoryType = 5; 
                dmnfood.Description = "";
                dmnfood.CreatedBy = FrmDashboard.UserId;
                dmnfood.BranchId = FrmDashboard.BranchId;
                dmnfood.Status = 1;
                var result = bllcategory.AddeditData("prdInsert_upd_Category", dmnfood);
                CMessageBox.Show("Category Saved");
                txtFcid.Text = "0";
                txtfoodCate.Clear();

                LoadMenuCategory();
                pnl_menucateg.Visible = false;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private void btn_del_cate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFcid.Text) || txtFcid.Text == "0")
                {
                    CMessageBox.Show("Select Category to Delete");
                    return;
                }

                DialogResult result = CMessageBox.Show(
                    "Are you sure you want to delete this category?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                DmnCategory dmnfood = new DmnCategory();
                BllCategory bllcategory = new BllCategory();

                dmnfood.Fcid = Convert.ToInt32(txtFcid.Text);
                dmnfood.CategoryType = 5;
                dmnfood.CreatedBy = FrmDashboard.UserId;
                dmnfood.BranchId = FrmDashboard.BranchId;
                dmnfood.Status = 0; // 🔥 Soft delete

                bllcategory.AddeditData("prdInsert_upd_Category", dmnfood);

                CMessageBox.Show("Category Deleted");

                // Reset
                txtFcid.Text = "0";
                txtfoodCate.Clear();

                // 🔥 Reload Combo
                LoadMenuCategory();

                pnl_menucateg.Visible = false;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private void HideAllGrids()
        {
            dgv_packs_default.Visible = false;
            dgv_packcustom.Visible = false;
            dgv_card.Visible = false;
            dgv_gift.Visible = false;
            tlp_subcategory.Visible = false;
        }

        private void btn_stanpackage_Click(object sender, EventArgs e)
        {
            HideAllGrids();
            dgv_packs_default.Visible = true;
            dgv_packs_default.BringToFront();
        }

        private void btn_custommenu_Click(object sender, EventArgs e)
        {
            HideAllGrids();
            dgv_packcustom.Visible = true;
            dgv_packcustom.BringToFront();
        }

        private void btn_cardpacks_Click(object sender, EventArgs e)
        {
            HideAllGrids();
            dgv_card.Visible = true;
            dgv_card.BringToFront();
            tlp_subcategory.Visible = true;
            LoadCardProducts("ALL");
        }

        private void btn_giftpacks_Click(object sender, EventArgs e)
        {
            HideAllGrids();
            dgv_gift.Visible = true;
            dgv_gift.BringToFront();

            LoadGiftProducts();
        }

        private void btn_cakepacks_Click(object sender, EventArgs e)
        {
            if (pnl_cakeconfig.Visible)
            {
                pnl_cakeconfig.Visible = false;
            }
            else
            {
                if (FrmDashboard.DefaultTaxType != "")
                {
                    cmbTaxType_cake.Text = FrmDashboard.DefaultTaxType;
                    if (cmbTaxType_cake.Text == "GST")
                    {
                        cmbIgstPrcnt_cake.SelectedValue = Convert.ToInt64(FrmDashboard.DefaultTaxsettingid);
                    }
                    else if (cmbTaxType_cake.Text == "VAT")
                    {
                        cmbVatPrcnt_cake.SelectedValue = Convert.ToInt64(FrmDashboard.DefaultTaxsettingid);
                    }
                }
                cmbtax_cake.SelectedIndex = 0;

                //pnl_cakeconfig.Width = (int)(this.ClientSize.Width * 0.5);
                //pnl_cakeconfig.Height = (int)(this.ClientSize.Height * 0.4);

                pnl_cakeconfig.Left = (this.ClientSize.Width - pnl_cakeconfig.Width) / 2;
                pnl_cakeconfig.Top = (this.ClientSize.Height - pnl_cakeconfig.Height) / 2;
                pnl_cakeconfig.BringToFront();
                pnl_cakeconfig.Visible = true;


            }

        }




        #region  Card 

        private void cmbsubcategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string type = cmbsubcategory.Text;

            if (type == "ALL")
                LoadCardProducts("ALL");
            else if (type == "CardProducts")
                LoadCardProducts("1");
            else if (type == "Trampolining")
                LoadCardProducts("2");
            else if (type == "TimePlayProducts")
                LoadCardProducts("3");
            else if (type == "GatePass")
                LoadCardProducts("4");
        }
        private void LoadCardProducts(string type = "ALL")
        {
            try
            {
                dgv_card.Rows.Clear();

                BllSearch<DmnCardProducts> bll = new BllSearch<DmnCardProducts>();
                DmnSearch search = new DmnSearch();

                if (type == "ALL")
                {
                    search.Select = @"
                Product_id AS Id, 
                Product AS Name, 
                1 AS Type, 
                'Card' AS Category";

                    search.Table = "[" + GameDB + @"].dbo.tblCardProducts WHERE Status=1

            UNION ALL

            SELECT 
                ProductId AS Id, 
                Description AS Name, 
                2 AS Type, 
                'Trampoline' AS Category
            FROM [" + GameDB + @"].dbo.tblTrampolining WHERE Status=1

            UNION ALL

            SELECT 
                ProductId AS Id, 
                Description AS Name, 
                3 AS Type, 
                'TimePlay' AS Category
            FROM [" + GameDB + @"].dbo.tblTimePlayProducts WHERE Status=1

            UNION ALL

            SELECT 
                ProductId AS Id, 
                Description AS Name, 
                4 AS Type, 
                'GatePass' AS Category
            FROM [" + GameDB + @"].dbo.tblGatePass WHERE Status=1";

                    search.WhereNdGroupBy = "";
                }
                else
                {
                    if (type == "1")
                    {
                        search.Select = "Product_id AS Id, Product AS Name, 1 AS Type, 'Card' AS Category";
                        search.Table = "[" + GameDB + "].dbo.tblCardProducts";
                    }
                    else if (type == "2")
                    {
                        search.Select = "ProductId AS Id, Description AS Name, 2 AS Type, 'Trampoline' AS Category";
                        search.Table = "[" + GameDB + "].dbo.tblTrampolining";
                    }
                    else if (type == "3")
                    {
                        search.Select = "ProductId AS Id, Description AS Name, 3 AS Type, 'TimePlay' AS Category";
                        search.Table = "[" + GameDB + "].dbo.tblTimePlayProducts";
                    }
                    else if (type == "4")
                    {
                        search.Select = "ProductId AS Id, Description AS Name, 4 AS Type, 'GatePass' AS Category";
                        search.Table = "[" + GameDB + "].dbo.tblGatePass";
                    }

                    search.WhereNdGroupBy = "WHERE Status = 1";
                }

                List<DmnCardProducts> list = bll.GetSearchData("prdSearch", search);

                int sl = 1;

                foreach (var item in list)
                {
                    int rowIndex = dgv_card.Rows.Add();

                    bool isSelected = selectedCardItems.Any(x => x.ProductId == item.Id);

                    dgv_card.Rows[rowIndex].Cells[Cardslno].Value = sl++;

                    dgv_card.Rows[rowIndex].Cells[Cardselectno].Value = isSelected;

                    dgv_card.Rows[rowIndex].Cells[Carditemno].Value = item.Name;
                    dgv_card.Rows[rowIndex].Cells[Cardcategmenuno].Value = item.Category;
                    dgv_card.Rows[rowIndex].Cells[Cardqtyno].Value = 1;
                    dgv_card.Rows[rowIndex].Cells[CardProductid].Value = item.Id;
                    dgv_card.Rows[rowIndex].Cells[Cardproducttype].Value = item.Type;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private void dgv_card_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != Cardselectno)
                return;

            var row = dgv_card.Rows[e.RowIndex];

  
            if (row.Cells[CardProductid].Value == null)
                return;

            bool isChecked = row.Cells[Cardselectno].Value != null &&
                             Convert.ToBoolean(row.Cells[Cardselectno].Value);

            int productId = Convert.ToInt32(row.Cells[CardProductid].Value);

            string name = row.Cells[Carditemno].Value?.ToString() ?? "";

            int qty = 1;
            if (row.Cells[Cardqtyno].Value != null)
                int.TryParse(row.Cells[Cardqtyno].Value.ToString(), out qty);

            int type = 0;
            if (row.Cells[Cardproducttype].Value != null)
                int.TryParse(row.Cells[Cardproducttype].Value.ToString(), out type);

            if (isChecked)
            {
                if (!selectedCardItems.Any(x => x.ProductId == productId))
                {
                    selectedCardItems.Add(new SelectedCardItem
                    {
                        ProductId = productId,
                        ProductName = name,
                        Qty = qty,
                        ProductType = type
                    });
                }
            }
            else
            {
                selectedCardItems.RemoveAll(x => x.ProductId == productId);
            }
        }

        #endregion

        #region Gift 
        private void LoadGiftProducts()
        {
            try
            {

                if (dgv_gift.Rows.Count > 0)
                    return;

                dgv_gift.Rows.Clear();

                BllSearch<DmnGiftProducts> bll = new BllSearch<DmnGiftProducts>();
                DmnSearch search = new DmnSearch();

                search.Select = "ProductId, Description";
                search.Table = "[" + GameDB + "].dbo.tblRedemptionProducts";
                search.WhereNdGroupBy = "WHERE Status = 1";

                List<DmnGiftProducts> list = bll.GetSearchData("prdSearch", search);

                int sl = 1;

                foreach (var item in list)
                {
                    int rowIndex = dgv_gift.Rows.Add();

                    dgv_gift.Rows[rowIndex].Cells[Giftslno].Value = sl++;
                    dgv_gift.Rows[rowIndex].Cells[Giftselectno].Value = false;
                    dgv_gift.Rows[rowIndex].Cells[Giftitemno].Value = item.Description;
                    dgv_gift.Rows[rowIndex].Cells[Giftqtyno].Value = 1;
                    dgv_gift.Rows[rowIndex].Cells[GiftProductid].Value = item.ProductId;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void dgv_card_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_card.IsCurrentCellDirty)
            {
                dgv_card.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        #endregion

        #region Cake Registartion
        private void btnserach_cake_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                int returnValue = 0;

                DmnSearch search = new DmnSearch();
                search.ColumnHeaders = "ID,Cake Type,Size,Shape";
                search.Select = "PartyPackCakeId as Id, CakeType as Field1, CakeSize as Field2, CakeShape as Field3";
                search.Table = "tblPartyPackCake";
                search.WhereNdGroupBy = "WHERE Status = 1";
                search.OrderBy = " ORDER BY CakeType";

                var gridSource = bllSearch.GetSearchData("prdSearch", search);

                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records Found");
                }
                else
                {
                    using (var frm = new FrmSearch<DmnGridFields>(
                        search,
                        SearchMode.FirstCharacter,
                        this.Width - 150,
                        this.Height - 150,
                        this.Location.Y + 50,
                        this.Location.X + 20,
                        null))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            returnValue = Convert.ToInt32(frm._retValue);
                        }
                    }

                    if (returnValue > 0)
                    {
                        LoadCakeById(returnValue);
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void LoadCakeById(int id)
        {
            try
            {
                BllSearch<DmnPartyPackCake> bll = new BllSearch<DmnPartyPackCake>();

                DmnSearch search = new DmnSearch();
                search.Select = "*";
                search.Table = "tblPartyPackCake";
                search.WhereNdGroupBy = "WHERE PartyPackCakeId=" + id;

                var list = bll.GetSearchData("prdSearch", search);

                if (list.Count > 0)
                {
                    btndel_cake.Visible = true;

                    var item = list[0];

                    // ✅ IMPORTANT
                    selectedCakeId = item.PartyPackCakeId;

                    txtcaketype.Text = item.CakeType;
                    txtcakesize.Text = item.CakeSize;
                    txtcakeshape.Text = item.CakeShape;
                    txtcakeprice.Text = item.Price.ToString();


                    cmbTaxType_cake.Text = item.TaxType;
                    cmbIgstPrcnt.SelectedValue =Convert.ToUInt64(item.TaxSettingsId);
                    cmbtax_cake.Text = item.Tax;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void btndel_cake_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedCakeId == 0)
                {
                    CMessageBox.Show("Select Cake First");
                    return;
                }

                BllPartyPack bllPack = new BllPartyPack();
                DmnPartyPackCake cake = new DmnPartyPackCake();

                cake.PartyPackCakeId = selectedCakeId;
                cake.Status = 0;

                cake.CakeType = "";
                cake.CakeSize = "";
                cake.CakeShape = "";
                cake.Qty = 1;
                cake.Price = 0;

                cake.TaxType = "";
                cake.TaxSettingsId = 0;
                cake.Tax = "";

                cake.CreatedBy = FrmDashboard.UserId;
                cake.BranchId = FrmDashboard.BranchId;

                var result = bllPack.AddData_Cake("prdInsert_Upd_PartyPackCake", cake);

                CMessageBox.Show("Deleted Successfully");

                ClearCake();
                selectedCakeId = 0;
                btndel_cake.Visible = false;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private void btnsave_cake_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtcaketype.Text))
                {
                    CMessageBox.Show("Enter Cake Type");
                    return;
                }

                BllPartyPack bllPack = new BllPartyPack();
                DmnPartyPackCake cake = new DmnPartyPackCake();

                cake.PartyPackCakeId = selectedCakeId;

                cake.PartyPackId = 0;
                cake.CakeType = txtcaketype.Text;
                cake.CakeSize = txtcakesize.Text;
                cake.CakeShape = txtcakeshape.Text;
                cake.Qty = 1;
                cake.Price = Convert.ToDecimal(txtcakeprice.Text);

                cake.TaxType = cmbTaxType.Text;
                cake.TaxSettingsId = Convert.ToInt32(cmbIgstPrcnt_cake.SelectedValue);
                cake.Tax = cmbtax_cake.Text;

                cake.CreatedBy = FrmDashboard.UserId;
                cake.BranchId = FrmDashboard.BranchId;
                cake.Status = 1;

                var result = bllPack.AddData_Cake("prdInsert_Upd_PartyPackCake", cake);

                if (result.PartyPackCakeId > 0)
                {
                    CMessageBox.Show(selectedCakeId > 0 ? "Updated Successfully" : "Saved Successfully");

                    ClearCake();
                    selectedCakeId = 0;   
                    btndel_cake.Visible = false;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        public void ClearCake()
        {
            try
            {
                txtcaketype.Text = ""; ;
                txtcakesize.Text = "1";
                txtcakeshape.Text = "";
                txtcakeprice.Text = "0";

                if (FrmDashboard.DefaultTaxType != "")
                {
                    cmbTaxType_cake.Text = FrmDashboard.DefaultTaxType;
                    if (cmbTaxType_cake.Text == "GST")
                    {
                        cmbIgstPrcnt_cake.SelectedValue = Convert.ToInt64(FrmDashboard.DefaultTaxsettingid);
                    }
                    else if (cmbTaxType_cake.Text == "VAT")
                    {
                        cmbVatPrcnt_cake.SelectedValue = Convert.ToInt64(FrmDashboard.DefaultTaxsettingid);
                    }
                }
                cmbtax_cake.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
            }
        }
        #endregion

        private void btnserach_categ_Click(object sender, EventArgs e)
        {

        }

        private void Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null) return;

            // allow control keys
            if (char.IsControl(e.KeyChar))
                return;

            // allow only numbers and dot
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            // allow only one dot
            if (e.KeyChar == '.' && txt.Text.Contains("."))
            {
                e.Handled = true;
                return;
            }

            // allow only 2 decimal places
            if (txt.Text.Contains("."))
            {
                int index = txt.Text.IndexOf('.');
                string decimalPart = txt.Text.Substring(index + 1);

                if (txt.SelectionStart > index && decimalPart.Length >= 2)
                {
                    e.Handled = true;
                }
            }
        }


        private void btncakreg_cls_Click(object sender, EventArgs e)
        {
            pnl_cakeconfig.Visible = false;
        }

        private void cmbTaxType_cake_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int startRow = 3;
                int endRow = 4;
                int totalColumns = 4;


                for (int row = startRow; row <= endRow; row++)
                {
                    for (int col = 0; col < totalColumns; col++)
                    {
                        Control ctrl = tlp_cakereg.GetControlFromPosition(col, row);
                        if (ctrl != null)
                        {
                            tlp_cakereg.Controls.Remove(ctrl);
                        }
                    }
                }

                // 🔹 GST Layout
                if (cmbTaxType_cake.Text == "GST")
                {
                    lbligst_cake.Dock = DockStyle.Fill;
                    cmbIgstPrcnt_cake.Dock = DockStyle.Fill;
                    lblcgst_cake.Dock = DockStyle.Fill;
                    txtCgstPrcnt_cake.Dock = DockStyle.Fill;
                    lblsgst_cake.Dock = DockStyle.Fill;
                    txtSgstPrcnt_cake.Dock = DockStyle.Fill;

                    tlp_cakereg.Controls.Add(lbligst_cake, 0, 3);
                    tlp_cakereg.Controls.Add(cmbIgstPrcnt_cake, 1, 3);

                    tlp_cakereg.Controls.Add(lblcgst_cake, 2, 3);
                    tlp_cakereg.Controls.Add(txtCgstPrcnt_cake, 3, 3);

                    tlp_cakereg.Controls.Add(lblsgst_cake, 0, 4);
                    tlp_cakereg.Controls.Add(txtSgstPrcnt_cake, 1, 4);

                    tlp_cakereg.Controls.Add(btndel_cake, 3, 4);
                }

                // 🔹 VAT Layout
                else if (cmbTaxType_cake.Text == "VAT")
                {
                    lblvat_cake.Dock = DockStyle.Fill;
                    cmbVatPrcnt_cake.Dock = DockStyle.Fill;

                    tlp_cakereg.Controls.Add(lblvat_cake, 0, 3);
                    tlp_cakereg.Controls.Add(cmbVatPrcnt_cake, 1, 3);

                    tlp_cakereg.Controls.Add(btndel_cake, 3, 3);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Cake setup page cmbTaxType_cake_SelectionChangeCommitted()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void cmbIgstPrcnt_cake_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbIgstPrcnt_cake.SelectedIndex > 0)
                {
                    BllSearch<DmnTaxSettings> bllSearch = new BllSearch<DmnTaxSettings>();
                    DmnSearch objSearch_tax = new DmnSearch();
                    objSearch_tax = new DmnSearch();
                    objSearch_tax.Select = "TaxSettingsId, SgstPrcnt, CgstPrcnt";
                    objSearch_tax.Table = "tblTaxSettings";
                    objSearch_tax.WhereNdGroupBy = "Where TaxSettingsId = " + cmbIgstPrcnt_cake.SelectedValue;
                    objSearch_tax.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch_tax);
                    if (gridSource.Count > 0)
                    {
                        txtSgstPrcnt_cake.Text = gridSource[0].SgstPrcnt.ToString();
                        txtCgstPrcnt_cake.Text = gridSource[0].CgstPrcnt.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page cmbIgstPrcnt_cake_SelectedIndexChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void AllowIntegerOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void AllowDecimalOnly(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            // Allow control keys (Backspace)
            if (char.IsControl(e.KeyChar))
                return;

            // Allow digits
            if (char.IsDigit(e.KeyChar))
                return;

            // Allow only one decimal point
            if (e.KeyChar == '.' && !txt.Text.Contains("."))
                return;

            // Block everything else
            e.Handled = true;
        }

        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                foreach (TextBox textBox in new TextBox[] { txtminadvamt, txtCgstPrcnt_cake, txtSgstPrcnt_cake, txtCgstPrcnt, txtSgstPrcnt, txtDiscAmnt, txtDisPer, txtchildprice, txtadultprice, txtpackname, txtminadvamt, txtCgstPrcnt_cake, txtcakeshape, txtcakeprice, txtcakesize, txtcaketype })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("Product page textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void ApplyRoundedTextBox(TextBox textBox, int radius)
        {
            try
            {
                // Create a new GraphicsPath object
                GraphicsPath path = new GraphicsPath();
                // Define the shape of the path using a rounded rectangle
                Rectangle rect = new Rectangle(0, 0, textBox.Width, textBox.Height);
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                // Set the Region property of the textbox to the new path
                textBox.Region = new Region(path);
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        #endregion

        private void FrmPartyPackSetup_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }

        #region Package time Setting 

        private void btn_packtimesett_Click(object sender, EventArgs e)
        {
            pnl_packtime.Visible = true;
            pnl_packtime.BringToFront();
            pnl_packtime.Left = (this.ClientSize.Width - pnl_packtime.Width) / 2;
            pnl_packtime.Top = (this.ClientSize.Height - pnl_packtime.Height) / 2;
        }

        private void btn_submittime_Click(object sender, EventArgs e)
        {
            pnl_packtime.Visible = false;
        }

        private void button1btnpacktimeclose_Click(object sender, EventArgs e)
        {
            pnl_packtime.Visible = false;
        }
        private void chk_time_atv_all_CheckedChanged(object sender, EventArgs e)
        {

            bool check = chk_time_atv_all.Checked;

            chk_sunday.Checked = check;
            chk_monday.Checked = check;
            chk_tuesday.Checked = check;
            chk_wednesday.Checked = check;
            chk_thursday.Checked = check;
            chk_friday.Checked = check;
            chk_saturday.Checked = check;

        }
        #endregion


    }
}
