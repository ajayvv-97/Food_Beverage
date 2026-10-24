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
        public int Rproductid = 6;
        public int Rproducttype = 7;

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


        // Registration Grid
        // Keep this map identical to FrmPartyBooking.Dgv_Selected_Item's designer column order.
        public int iaction = 0;
        public int islno = 1;
        public int ibarcode = 2;
        public int iproducttypeno = 3;
        public int iname = 4;
        public int iunit = 5;
        public int israte = 6;
        public int iqty = 7;
        public int iprate = 8;
        public int iamount = 9;
        public int idisper = 10;
        public int idisamt = 11;
        public int itaxper = 12;
        public int itotal = 13;
        public int iitemid = 14;
        public int itaxtype = 15;
        public int imrp = 16;
        public int itype = 17;
        public int ibarcode2 = 18;
        public int itaxsettingid = 19;
        public int iunitid = 20;
        public int ipridno = 21;
        public int iprodesno = 22;
        public int istatusno = 23;
        public int iigstamtno = 24;
        public int ivatamtno = 25;
        public int iorderdetailsidno = 26;
        public int isubofferstatus = 27;
        public int ioriginalprice = 28;
        public int isubofferid = 29;
        public int itabletype = 30;
        public int iorderdescno = 31;
        public int ioriginalqty = 32;
        public int iavid = 33;
        public int ivaraiationstatusno = 34;
        public int ipavidno = 35;
        public int iMainProductStatusno = 36;
        public int iAddoncountno = 37;
        public int ividno = 38;
        public int iComboTaxamtno = 39;
        public int iccodeno = 40;
        public int iVariatiionNameno = 41;
        public int iPreparationModeno = 42;
        public int isaledetailsidno = 43;
        public int istocktrackno = 44;
        public int iorgname = 45;
        public int icombotaxamt = 46;
        public int ipretaxamtno = 47;
        public int iSaledisperno = 48;
        public int isalediamtno = 49;
        #endregion

        bool Loaddetailsflag = false;
        bool Loadcustomflag = false;

        int currentPackId = 0;

        private List<DmnCategory> categoryList = new List<DmnCategory>();
        private List<DmnComboBox> partyPackMenuCategorySource;

        private readonly List<DmnGridFields> partyPackMenuAllProducts = new List<DmnGridFields>();
        private List<DmnGridFields> partyPackMenuFilteredProducts = new List<DmnGridFields>();
        private List<DmnGridFields> partyPackMenuPageProducts = new List<DmnGridFields>();
        private FlowLayoutPanel partyPackMenuCategoryPanel;
        private ToolTip partyPackMenuToolTip;
        private int partyPackMenuCurrentPage = 1;
        private int partyPackMenuPageSize = 12;
        private int partyPackMenuCategoryId;
        private bool partyPackMenuProductsLoaded;

        private sealed class PartyPackMenuSelection
        {
            public int ProductId { get; set; }
            public int SaleType { get; set; } // 0 = tblProduct, 1 = tblItem
            public int CategoryId { get; set; }
            public string ProductName { get; set; }
            public string ItemCode { get; set; }
            public decimal Price { get; set; }
            public decimal MRP { get; set; }
            public int UnitId { get; set; }
            public int TaxSettingId { get; set; }
            public string TaxMode { get; set; }
            public string TaxType { get; set; }
        }

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

        // Keep card quantities across category changes; cleared only by the Refresh/reset action.
        private readonly Dictionary<string, decimal> cardProductQuantities =
            new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

        List<SelectedCardItem> selectedCardItems = new List<SelectedCardItem>();

        public string GameDB = Convert.ToString(ConfigurationManager.AppSettings["GameDB"]);

        int selectedCakeId = 0;

        private readonly Image Grid_Remove = Properties.Resources.Round_Close;
        private readonly Image Blankimage = Properties.Resources.backspace;
        #endregion

        public FrmPartyPackSetup()
        {
            InitializeComponent();

            btn_menu_close.Click += (s, e) => ClosePartyPackMenu();
            Dgv_Selected_Item.CellClick += DgvPartyPackMenu_CellClick;
            Dgv_Selected_Item.CellPainting += DgvPartyPackMenu_CellPainting;
            pnl_dgv_menu.Resize += (s, e) => ResizePartyPackMenuContent();
            ApplyPartyPackMenuVisualStyles();
            txtname_search.Enter += PartyPackMenuSearchTextBox_Enter;
            txtname_search.Leave += PartyPackMenuSearchTextBox_Leave;
            txtitemcode_serach.Enter += PartyPackMenuSearchTextBox_Enter;
            txtitemcode_serach.Leave += PartyPackMenuSearchTextBox_Leave;
            txtname_search.KeyDown += PartyPackMenuSearchTextBox_KeyDown;
            txtitemcode_serach.KeyDown += PartyPackMenuSearchTextBox_KeyDown;
        }
        private void FrmPartyPackSetup_Load(object sender, EventArgs e)
        {
            try
            {
      
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
                LoadPartyPackMenuCategorySource();

                ClearCake();

                dgv_packs_default.BringToFront();
                dgv_packs_default.Visible = true;
                if (dgv_packs_default.Rows.Count == 0)
                {
                    int rowIndexDefault = dgv_packs_default.Rows.Add();
                    dgv_packs_default.Rows[rowIndexDefault].Cells[Ractionno].Value = null;
                }

                RefreshSlNo();


                if (dgv_packs_default.Columns[Ractionno] is DataGridViewImageColumn actionColumn)
                {
                    actionColumn.Image = null;
                    actionColumn.DefaultCellStyle.NullValue = null;
                }

                dgv_packs_default.DataError -= dgv_packs_default_DataError;
                dgv_packs_default.DataError += dgv_packs_default_DataError;
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
                {
                    int rowIndexCustom = dgv_packcustom.Rows.Add();
                    DataGridViewImageCell cellCustom = (DataGridViewImageCell)dgv_packcustom.Rows[rowIndexCustom].Cells[Cmactionno];
                    cellCustom.Value = Grid_Remove;
                    cellCustom.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }

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

                textb_shape();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  FrmPartyPackSetup_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private bool HasEmptyRow()
        {
            try
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
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  HasEmptyRow()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
                return false;
            }

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
                clsgen.LogException("Partypacksetup page Taxfill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }
        private void LoadMenuCategory()
        {
            try
            {
                LoadPartyPackMenuCategorySource();

                DataGridViewComboBoxColumn cmb = dgv_packs_default.Columns[Rcategmenuno] as DataGridViewComboBoxColumn;
                if (cmb == null)
                    return;

                cmb.DataSource = partyPackMenuCategorySource;
                cmb.DisplayMember = "Value";
                cmb.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  LoadMenuCategory()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void LoadPartyPackMenuCategorySource()
        {
            try
            {
                if (partyPackMenuCategorySource != null)
                    return;

                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Fcid,CategoryName";
                objSearch.Table = "tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where status = 1 and CategoryType=0";
                objSearch.OrderBy = "";

                List<DmnComboBox> loadedCategories = bllSearch.GetSearchData("prdComboFill", objSearch);
                partyPackMenuCategorySource = loadedCategories ?? new List<DmnComboBox>();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page LoadPartyPackMenuCategorySource()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
                partyPackMenuCategorySource = new List<DmnComboBox>();
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
                clsgen.LogException("Partypacksetup page  LoadMenuCategory_Cutom()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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

                    txtCgstPrcnt.Dock = DockStyle.Fill;
                    txtSgstPrcnt.Dock = DockStyle.Fill;

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

                    // Ensure these controls are sized by their TableLayoutPanel cells, not by
                    // their zero-width Designer sizes or stale Anchor settings.
                    foreach (TextBox taxTextBox in new[] { txtCgstPrcnt, txtSgstPrcnt, txtminadvamt })
                    {
                        taxTextBox.Visible = true;
                        taxTextBox.AutoSize = false;
                        taxTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                        taxTextBox.Dock = DockStyle.Fill;
                        taxTextBox.Margin = new Padding(0, 3, 5, 3);
                        taxTextBox.MinimumSize = new Size(0, 27);
                    }

                    // Reapply docking after all controls have been assigned to their cells.
                    tlp_registration.SetCellPosition(txtCgstPrcnt, new TableLayoutPanelCellPosition(0, 6));
                    tlp_registration.SetCellPosition(txtSgstPrcnt, new TableLayoutPanelCellPosition(1, 6));
                    tlp_registration.SetCellPosition(txtminadvamt, new TableLayoutPanelCellPosition(2, 6));
                    tlp_registration.PerformLayout();
                    tlp_registration.GetControlFromPosition(0, 6)?.PerformLayout();
                    tlp_registration.GetControlFromPosition(1, 6)?.PerformLayout();
                    tlp_registration.GetControlFromPosition(2, 6)?.PerformLayout();

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

                string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
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

                    bool saveStatus = bllPackTime.AddEditPackageWorkTime("PrdInsert_Upd_PackageWorkTime", dmnPackTime);

                    if (!saveStatus)
                    {
                        CMessageBox.Show($"Failed to save package time for {days[i - 1]}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        int productId = 0;
                        int productType = 0;
                        int.TryParse(Convert.ToString(row.Cells[Rproductid].Value), out productId);
                        int.TryParse(Convert.ToString(row.Cells[Rproducttype].Value), out productType);
                        // Manual food rows have ProductId/ProductType = 0; menu-selected food rows retain both IDs.
                        // Both are stored as FoodItem so downstream party-pack handling remains consistent.
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
                            Productid = productId,
                            ProductType = productType
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

                // Commit the active editor before saving; otherwise the last typed quantity
                // may still be inside the DataGridView editing control and not in the row.
                if (dgv_card.IsCurrentCellInEditMode)
                    dgv_card.EndEdit();
                dgv_card.CommitEdit(DataGridViewDataErrorContexts.Commit);

                // Synchronize visible checked card rows into selectedCardItems so the latest
                // grid quantity is saved even if CellValueChanged has not fired yet.
                foreach (DataGridViewRow cardRow in dgv_card.Rows)
                {
                    if (cardRow.IsNewRow || cardRow.Cells[CardProductid].Value == null)
                        continue;

                    bool rowSelected = cardRow.Cells[Cardselectno].Value != null &&
                                       Convert.ToBoolean(cardRow.Cells[Cardselectno].Value);
                    int rowProductId, rowProductType, rowQty;
                    if (!int.TryParse(Convert.ToString(cardRow.Cells[CardProductid].Value), out rowProductId) ||
                        !int.TryParse(Convert.ToString(cardRow.Cells[Cardproducttype].Value), out rowProductType) ||
                        rowProductId <= 0)
                        continue;

                    if (!int.TryParse(Convert.ToString(cardRow.Cells[Cardqtyno].Value), out rowQty) || rowQty <= 0)
                        rowQty = 1;

                    string rowName = Convert.ToString(cardRow.Cells[Carditemno].Value) ?? string.Empty;
                    string rowKey = rowProductType + "_" + rowProductId;
                    cardProductQuantities[rowKey] = rowQty;

                    SelectedCardItem selectedRow = selectedCardItems.FirstOrDefault(
                        x => x.ProductId == rowProductId && x.ProductType == rowProductType);

                    if (rowSelected)
                    {
                        if (selectedRow == null)
                        {
                            selectedCardItems.Add(new SelectedCardItem
                            {
                                ProductId = rowProductId,
                                ProductName = rowName,
                                Qty = rowQty,
                                ProductType = rowProductType
                            });
                        }
                        else
                        {
                            selectedRow.ProductName = rowName;
                            selectedRow.Qty = rowQty;
                        }
                    }
                    else
                    {
                        selectedCardItems.RemoveAll(x =>
                            x.ProductId == rowProductId && x.ProductType == rowProductType);
                    }
                }

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
                clsgen.LogException("Partypacksetup page  btnsave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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

                if (Convert.ToDecimal(txtadultprice.Text) < 0)
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
                int rowIndex = dgv_packs_default.Rows.Add();
                dgv_packs_default.Rows[rowIndex].Cells[Ractionno].Value = null;
                RefreshSlNo();

                dgv_packcustom.Rows.Clear();
                rowIndex = dgv_packcustom.Rows.Add();
                DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_packcustom.Rows[rowIndex].Cells[Cmactionno];
                cellcolumntype.Value = Grid_Remove;
                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

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
                cardProductQuantities.Clear();
                selectedCardItems.Clear();

                ClearCake();

                textb_shape();

                pnl_cakeconfig.Visible = false;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page clearall", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }

        }
        private void dgv_packs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
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
                            int rowi = dgv_packs_default.Rows.Add();
                            dgv_packs_default.Rows[rowi].Cells[Ractionno].Value = null;
                        }
                        RefreshSlNo();
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  dgv_packs_CellValueChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                clsgen.LogException("Partypacksetup page  btnSearch_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                clsgen.LogException("Partypacksetup page  FillData()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                clsgen.LogException("Partypacksetup page  LoadPackageWorkTime()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                    if (item.PackItemType == (int)PackItemType.FoodItem || item.PackItemType == (int)PackItemType.Menu)
                    {
                        int rowIndex = dgv_packs_default.Rows.Add();
                        var row = dgv_packs_default.Rows[rowIndex];
                        bool isMenuSelectedProduct = item.Productid > 0;

                        row.Cells[PartyPackDetailsId].Value = item.PartyPackDetailsId;
                        row.Cells[Ritemno].Value = item.Item;
                        row.Cells[Rqtyno].Value = item.Quantity;
                        // Product ID is the marker for a menu-selected food item. Manual entries remain zero.
                        row.Cells[Rproductid].Value = isMenuSelectedProduct ? item.Productid : 0;
                        row.Cells[Rproducttype].Value = isMenuSelectedProduct ? item.ProductType : 0;

                        if (item.MenuCategoryId > 0)
                        {
                            // ProductId > 0 means this row came from the food menu. ProductType may be 0 or 1;
                            // both use the menu-category source (tblCategory_FTI, CategoryType = 4).
                            if (isMenuSelectedProduct)
                            {
                                LoadPartyPackMenuCategorySource();
                                bool menuCategoryExists = partyPackMenuCategorySource != null &&
                                    partyPackMenuCategorySource.Any(x => Convert.ToInt32(x.Id) == item.MenuCategoryId);
                                if (menuCategoryExists)
                                    row.Cells[Rcategmenuno].Value = item.MenuCategoryId;
                                else
                                    row.Cells[Rcategmenuno].Value = DBNull.Value;
                            }
                            else if (categoryList != null && categoryList.Any(x => x.Fcid == item.MenuCategoryId))
                            {
                                row.Cells[Rcategmenuno].Value = item.MenuCategoryId;
                            }
                        }

                        DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)row.Cells[Ractionno];
                        cellcolumntype.Value = Grid_Remove;
                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
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
                {
                    int emptyRowIndex = dgv_packs_default.Rows.Add();
                    dgv_packs_default.Rows[emptyRowIndex].Cells[Ractionno].Value = null;
                }
                RefreshSlNo();

                LoadCustomMenu();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  LoadPackDetails()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
            finally
            {
                Loaddetailsflag = false;
            }

        }
        private void ApplyCardSelectionToGrid()
        {
            try
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
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  ApplyCardSelectionToGrid()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }
        private void LoadCustomMenu(int packId = 0)
        {
            try
            {
                Loadcustomflag = true;
                dgv_packcustom.Rows.Clear();

                BllSearch<DmnPartyPackCustom> bll = new BllSearch<DmnPartyPackCustom>();

                DmnSearch search = new DmnSearch();
                search.Select = "PartyPackCustomDetailsId, Product, Qty, Price,MenuCategoryId";
                search.Table = "tblPartyPackCustomMenu";
                if (packId == 0)
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
                    int rowIndex = dgv_packcustom.Rows.Add();
                    DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_packcustom.Rows[rowIndex].Cells[Cmactionno];
                    cellcolumntype.Value = Grid_Remove;
                    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  LoadCustomMenu()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
            finally
            {
                Loadcustomflag = false;
            }

        }
        private bool HasEmptyRowCustom()
        {
            try
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
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  HasEmptyRowCustom()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
                return false;
            }
        }
        private void RefreshCustomSlNo()
        {
            try
            {
                for (int i = 0; i < dgv_packcustom.Rows.Count; i++)
                {
                    dgv_packcustom.Rows[i].Cells[Cmslno].Value = i + 1;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  RefreshCustomSlNo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }
        private void RefreshSlNo()
        {

            try
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
                    bool hasItem = val != null && !string.IsNullOrWhiteSpace(val.ToString());

                    if (hasItem)
                    {
                        row.Cells[Rslno].Value = sl++;
                        if (row.Cells[Ractionno] is DataGridViewImageCell actionCell)
                        {
                            actionCell.Value = Grid_Remove;
                            actionCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        }
                    }
                    else
                    {
                        row.Cells[Rslno].Value = null;
                        // Empty/placeholder rows must not display a remove icon.
                        row.Cells[Ractionno].Value = null;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  RefreshSlNo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                clsgen.LogException("Partypacksetup page  dgv_packs_CellContentClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                {
                    int emptyRowIndex = dgv_packs_default.Rows.Add();
                    dgv_packs_default.Rows[emptyRowIndex].Cells[Ractionno].Value = null;
                }

                RefreshSlNo();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  DeleteGridRow()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                clsgen.LogException("Partypacksetup page  dgv_packcustom_CellContentClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                clsgen.LogException("Partypacksetup page  DeleteCustomGridRow()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }


        }

        private void dgv_packcustom_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
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

                        bool isLastRow = e.RowIndex == dgv_packcustom.Rows.Count - 1;

                        if (isLastRow && !HasEmptyRowCustom())
                        {
                            int rowi = dgv_packcustom.Rows.Add();
                            DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_packcustom.Rows[rowi].Cells[Cmactionno];
                            cellcolumntype.Value = Grid_Remove;
                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        }
                        RefreshCustomSlNo();
                    }
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  dgv_packcustom_CellValueChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                clsgen.LogException("Partypacksetup page  btn_del_pack_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                clsgen.LogException("Partypacksetup page  dgv_packs_default_CellDoubleClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                clsgen.LogException("Partypacksetup page  btn_savemenucateg_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                clsgen.LogException("Partypacksetup page  btn_del_cate_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }


        }

        private void HideAllGrids()
        {
            try
            {
                dgv_packs_default.Visible = false;
                dgv_packcustom.Visible = false;
                dgv_card.Visible = false;
                dgv_gift.Visible = false;
                tlp_subcategory.Visible = false;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  HideAllGrids()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }


        }

        private void btn_stanpackage_Click(object sender, EventArgs e)
        {
            try
            {
                HideAllGrids();
                dgv_packs_default.Visible = true;
                dgv_packs_default.BringToFront();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  btn_stanpackage_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }

        }

        private void btn_custommenu_Click(object sender, EventArgs e)
        {
            try
            {
                HideAllGrids();
                dgv_packcustom.Visible = true;
                dgv_packcustom.BringToFront();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  btn_custommenu_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }

        }

        private void btn_cardpacks_Click(object sender, EventArgs e)
        {
            try
            {
                HideAllGrids();
                dgv_card.Visible = true;
                dgv_card.BringToFront();
                tlp_subcategory.Visible = true;
                LoadCardProducts("ALL");
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  btn_cardpacks_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }

        }

        private void btn_giftpacks_Click(object sender, EventArgs e)
        {
            try
            {
                HideAllGrids();
                dgv_gift.Visible = true;
                dgv_gift.BringToFront();

                LoadGiftProducts();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  btn_giftpacks_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }

        }

        private void btn_cakepacks_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  btn_cakepacks_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }




        #region  Card 

        private void cmbsubcategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  cmbsubcategory_SelectionChangeCommitted()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }
        private void LoadCardProducts(string type = "ALL")
        {
            try
            {
                // Preserve edited quantities when switching away from and back to Card Packs.
                // clearall() clears dgv_card, so the Refresh button still resets quantities to 1.
                foreach (DataGridViewRow previousRow in dgv_card.Rows)
                {
                    if (previousRow.IsNewRow)
                        continue;

                    int previousProductId = 0;
                    int previousProductType = 0;
                    decimal previousQuantity = 1m;
                    int.TryParse(Convert.ToString(previousRow.Cells[CardProductid].Value), out previousProductId);
                    int.TryParse(Convert.ToString(previousRow.Cells[Cardproducttype].Value), out previousProductType);
                    decimal.TryParse(Convert.ToString(previousRow.Cells[Cardqtyno].Value), out previousQuantity);

                    if (previousProductId > 0)
                    {
                        string key = previousProductType + "_" + previousProductId;
                        cardProductQuantities[key] = previousQuantity > 0 ? previousQuantity : 1m;
                    }
                }

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
                    string quantityKey = item.Type + "_" + item.Id;
                    dgv_card.Rows[rowIndex].Cells[Cardqtyno].Value = cardProductQuantities.TryGetValue(quantityKey, out decimal savedQuantity)
                        ? savedQuantity
                        : 1m;
                    dgv_card.Rows[rowIndex].Cells[CardProductid].Value = item.Id;
                    dgv_card.Rows[rowIndex].Cells[Cardproducttype].Value = item.Type;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  LoadCardProducts()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }

        }

        private void dgv_card_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 ||
                    (e.ColumnIndex != Cardselectno && e.ColumnIndex != Cardqtyno))
                    return;

                DataGridViewRow row = dgv_card.Rows[e.RowIndex];
                if (row.IsNewRow || row.Cells[CardProductid].Value == null)
                    return;

                int productId;
                int productType;
                if (!int.TryParse(Convert.ToString(row.Cells[CardProductid].Value), out productId) ||
                    !int.TryParse(Convert.ToString(row.Cells[Cardproducttype].Value), out productType) ||
                    productId <= 0)
                    return;

                string name = Convert.ToString(row.Cells[Carditemno].Value) ?? string.Empty;
                string quantityKey = productType + "_" + productId;
                int qty;
                if (!int.TryParse(Convert.ToString(row.Cells[Cardqtyno].Value), out qty) || qty <= 0)
                    qty = 1;

                // Keep the quantity cache and selected list in sync when the quantity cell changes.
                cardProductQuantities[quantityKey] = qty;

                bool isChecked = row.Cells[Cardselectno].Value != null &&
                                 Convert.ToBoolean(row.Cells[Cardselectno].Value);
                SelectedCardItem selected = selectedCardItems.FirstOrDefault(
                    x => x.ProductId == productId && x.ProductType == productType);

                if (isChecked)
                {
                    if (selected == null)
                    {
                        selectedCardItems.Add(new SelectedCardItem
                        {
                            ProductId = productId,
                            ProductName = name,
                            Qty = qty,
                            ProductType = productType
                        });
                    }
                    else
                    {
                        selected.ProductName = name;
                        selected.Qty = qty;
                    }
                }
                else if (e.ColumnIndex == Cardselectno)
                {
                    selectedCardItems.RemoveAll(x =>
                        x.ProductId == productId && x.ProductType == productType);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  dgv_card_CellValueChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                clsgen.LogException("Partypacksetup page  LoadGiftProducts()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }
        private void dgv_card_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {


                if (dgv_card.IsCurrentCellDirty)
                {
                    dgv_card.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  dgv_card_CurrentCellDirtyStateChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                clsgen.LogException("Partypacksetup page  btnserach_cake_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                    cmbIgstPrcnt.SelectedValue = Convert.ToUInt64(item.TaxSettingsId);
                    cmbtax_cake.Text = item.Tax;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  LoadCakeById()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                clsgen.LogException("Partypacksetup page  btndel_cake_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                    pnl_cakeconfig.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  btnsave_cake_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  ClearCake()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }
        #endregion

        private void btnserach_categ_Click(object sender, EventArgs e)
        {

        }

        private void Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  Decimal_KeyPress()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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

                textb_shape();
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
            try
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
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  AllowDecimalOnly()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
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
                clsgen.LogException("Partypacksetup page  textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
                clsgen.LogException("Partypacksetup page  ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
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
            try
            {
                pnl_packtime.Visible = true;
                pnl_packtime.BringToFront();
                pnl_packtime.Left = (this.ClientSize.Width - pnl_packtime.Width) / 2;
                pnl_packtime.Top = (this.ClientSize.Height - pnl_packtime.Height) / 2;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  btn_packtimesett_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }

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
            try
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

            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page  chk_time_atv_all_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }

        }

        #endregion

        private void btncakerefresh_Click(object sender, EventArgs e)
        {
            ClearCake();
            selectedCakeId = 0;
            btndel_cake.Visible = false;
        }

        #region Party Pack Menu Picker

        private void btnmenu_Click(object sender, EventArgs e)
        {
            try
            {
                int width = Math.Min((int)(ClientSize.Width * 0.86), ClientSize.Width - 20);
                int height = Math.Min((int)(ClientSize.Height * 0.86), ClientSize.Height - 20);

                pnl_menu.SuspendLayout();
                tableLayoutPanel43.Dock = DockStyle.Fill;
                tableLayoutPanel43.Margin = Padding.Empty;
                tableLayoutPanel43.Width = width - pnl_menu.Padding.Horizontal;
                pnl_dgv_menu.Dock = DockStyle.Fill;
                pnl_dgv_menu.Margin = Padding.Empty;
                pnl_dgv_menu.Padding = new Padding(3);
                tlp_menu_main.Dock = DockStyle.Fill;
                flp_productview.Dock = DockStyle.Fill;
                Dgv_Selected_Item.Dock = DockStyle.Fill;
                pnl_menu.Width = width;
                pnl_menu.Height = height;
                pnl_menu.Left = (ClientSize.Width - width) / 2;
                pnl_menu.Top = (ClientSize.Height - height) / 2;
                pnl_menu.Visible = true;
                pnl_menu.BringToFront();

                tlp_menu_main.Visible = true;
                tlp_menu_main.BringToFront();
                flp_productview.Visible = true;
                Dgv_Selected_Item.Visible = false;
                btn_cart.BackgroundImage = Properties.Resources.CartButton;
                btn_cart.BackgroundImageLayout = ImageLayout.Stretch;
                btn_submit_menu.Visible = true;

                pnl_menu.ResumeLayout(true);
                pnl_menu.PerformLayout();
                tableLayoutPanel43.PerformLayout();
                pnl_dgv_menu.PerformLayout();
                tlp_menu_main.PerformLayout();
                flp_productview.PerformLayout();
                Dgv_Selected_Item.PerformLayout();

                partyPackMenuPageSize = CalculatePartyPackMenuPageSize();
                partyPackMenuCurrentPage = 1;

                EnsurePartyPackMenuCategoryPanel();
                LoadPartyPackMenuCategories();
                partyPackMenuCurrentPage = 1;
                LoadPartyPackMenuProducts(0);
                SetPartyPackMenuSearchPlaceholders();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page btnmenu_Click()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void btn_menu_cancel_Click(object sender, EventArgs e)
        {
            try
            {
            ClosePartyPackMenu();
            Dgv_Selected_Item.Rows.Clear();
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page btn_menu_cancel_Click()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void btn_submit_menu_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow selectedRow in Dgv_Selected_Item.Rows)
                {
                    if (selectedRow.IsNewRow || !(selectedRow.Tag is PartyPackMenuSelection))
                        continue;

                    PartyPackMenuSelection selection = (PartyPackMenuSelection)selectedRow.Tag;
                    int quantity = 1;
                    int.TryParse(Convert.ToString(selectedRow.Cells[iqty].Value), out quantity);
                    if (quantity < 1)
                        quantity = 1;

                    DataGridViewRow existing = dgv_packs_default.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(row => !row.IsNewRow
                            && string.Equals(Convert.ToString(row.Cells[Rproductid].Value), selection.ProductId.ToString(), StringComparison.Ordinal)
                            && string.Equals(Convert.ToString(row.Cells[Rproducttype].Value), selection.SaleType.ToString(), StringComparison.Ordinal));

                    if (existing != null)
                    {
                        int oldQuantity = 0;
                        int.TryParse(Convert.ToString(existing.Cells[Rqtyno].Value), out oldQuantity);
                        existing.Cells[Rqtyno].Value = oldQuantity + quantity;
                    }
                    else
                    {
                        DataGridViewRow targetRow = dgv_packs_default.Rows
                            .Cast<DataGridViewRow>()
                            .FirstOrDefault(row => !row.IsNewRow
                                && string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[Ritemno].Value)));

                        if (targetRow == null)
                        {
                            int rowIndex = dgv_packs_default.Rows.Add();
                            targetRow = dgv_packs_default.Rows[rowIndex];
                        }

                        targetRow.Cells[Ritemno].Value = selection.ProductName;
                        SetMenuCategoryCellValue(targetRow, selection.CategoryId);
                        targetRow.Cells[Rqtyno].Value = quantity;
                        targetRow.Cells[Ractionno].Value = Grid_Remove;
                        targetRow.Cells[Rproductid].Value = selection.ProductId;
                        targetRow.Cells[Rproducttype].Value = selection.SaleType;
                        targetRow.Cells[PartyPackDetailsId].Value = 0;
                        if (targetRow.Cells[Ractionno] is DataGridViewImageCell imageCell)
                            imageCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    }
                }

                RefreshSlNo();
                Dgv_Selected_Item.Rows.Clear();
                ClosePartyPackMenu();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page btn_submit_menu_Click()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        /// <summary>
        /// Assign a category only when it exists in the DataGridViewComboBoxColumn's actual value list.
        /// This prevents DataGridViewComboBoxCell value-not-valid exceptions when menu and grid categories differ.
        /// </summary>
        private void SetMenuCategoryCellValue(DataGridViewRow row, int categoryId)
        {
            if (row == null || Rcategmenuno < 0 || Rcategmenuno >= row.Cells.Count)
                return;

            DataGridViewComboBoxCell cell = row.Cells[Rcategmenuno] as DataGridViewComboBoxCell;
            if (cell == null)
            {
                row.Cells[Rcategmenuno].Value = DBNull.Value;
                return;
            }

            try
            {
                if (cell.DataSource == null)
                    LoadMenuCategory();

                string valueMember = cell.ValueMember;
                object validValue = null;
                bool found = false;

                if (cell.DataSource is System.Collections.IEnumerable source)
                {
                    foreach (object item in source)
                    {
                        if (item == null) continue;
                        object candidate = item.GetType().GetProperty(valueMember)?.GetValue(item, null);
                        int candidateId;
                        if (candidate != null && int.TryParse(Convert.ToString(candidate), out candidateId) && candidateId == categoryId)
                        {
                            validValue = candidate;
                            found = true;
                            break;
                        }
                    }
                }

                // A blank value is valid for a menu row whose category is not in this grid's category source.
                cell.Value = found ? validValue : DBNull.Value;
            }
            catch
            {
                // Keep the product row even if the category cannot be selected.
                cell.Value = DBNull.Value;
            }
        }

        private void dgv_packs_default_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Combo-box values may be absent from the current category source; don't show the default error dialog.
            e.ThrowException = false;
            e.Cancel = false;
        }

        private void btnprev_Click(object sender, EventArgs e)
        {
            try
            {
            if (partyPackMenuCurrentPage <= 1)
                return;
            partyPackMenuCurrentPage--;
            ShowPartyPackMenuPage();
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page btnprev_Click()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            try
            {
            int totalPages = (int)Math.Ceiling(partyPackMenuFilteredProducts.Count / (double)Math.Max(1, partyPackMenuPageSize));
            if (partyPackMenuCurrentPage >= totalPages)
                return;
            partyPackMenuCurrentPage++;
            ShowPartyPackMenuPage();
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page btnnext_Click()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void btn_showmenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!pnl_menu.Visible)
                    btnmenu_Click(sender, e);

                EnsurePartyPackMenuCategoryPanel();
                partyPackMenuCategoryPanel.Visible = !partyPackMenuCategoryPanel.Visible;
                if (!partyPackMenuCategoryPanel.Visible)
                    return;

                Point screenPoint = btn_showmenu.PointToScreen(new Point(0, btn_showmenu.Height));
                Point location = pnl_menu.PointToClient(screenPoint);
                partyPackMenuCategoryPanel.Width = Math.Min(
                    Math.Max(btn_showmenu.Width, 180), Math.Max(180, pnl_menu.ClientSize.Width - 16));
                partyPackMenuCategoryPanel.Height = Math.Max(120,
                    Math.Min(pnl_menu.ClientSize.Height - location.Y - 16, 360));
                partyPackMenuCategoryPanel.Left = Math.Max(0,
                    Math.Min(location.X, pnl_menu.ClientSize.Width - partyPackMenuCategoryPanel.Width));
                partyPackMenuCategoryPanel.Top = Math.Max(0, location.Y + 3);
                partyPackMenuCategoryPanel.BringToFront();
                LoadPartyPackMenuCategories();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page btn_showmenu_Click()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void btn_serach_Click(object sender, EventArgs e)
        {
            try
            {
            string productName = txtname_search.Text;
            string itemCode = txtitemcode_serach.Text;
            if (productName == "Food Name .....")
                productName = string.Empty;
            if (itemCode == "Item Code .....")
                itemCode = string.Empty;

            partyPackMenuCurrentPage = 1;
            LoadPartyPackMenuProducts(partyPackMenuCategoryId, productName, itemCode);
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page btn_serach_Click()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void btn_cart_Click(object sender, EventArgs e)
        {
            try
            {
                if (!pnl_menu.Visible)
                    btnmenu_Click(sender, e);

                bool cartVisible = Dgv_Selected_Item.Visible && !flp_productview.Visible;
                if (cartVisible)
                {
                    Dgv_Selected_Item.Visible = false;
                    flp_productview.Visible = true;
                    tlp_menu_main.Visible = true;
                    tlp_menu_main.BringToFront();
                    btn_cart.BackgroundImage = Properties.Resources.CartButton;
                }
                else
                {
                    partyPackMenuCategoryPanel?.Hide();
                    flp_productview.Visible = false;
                    Dgv_Selected_Item.Visible = true;
                    Dgv_Selected_Item.BringToFront();
                    btn_cart.BackgroundImage = Properties.Resources.Party_Food_Items_Icon;
                }
                btn_cart.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page btn_cart_Click()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void EnsurePartyPackMenuCategoryPanel()
        {
            try
            {
            if (partyPackMenuCategoryPanel != null && !partyPackMenuCategoryPanel.IsDisposed)
                return;

            partyPackMenuCategoryPanel = new FlowLayoutPanel
            {
                Name = "flp_party_pack_menu_categories",
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Visible = false,
                BackColor = Color.White,
                Padding = new Padding(5),
                Margin = Padding.Empty,
                BorderStyle = BorderStyle.FixedSingle
            };
            partyPackMenuCategoryPanel.HorizontalScroll.Enabled = false;
            partyPackMenuCategoryPanel.HorizontalScroll.Visible = false;
            partyPackMenuCategoryPanel.VerticalScroll.Enabled = true;
            if (partyPackMenuToolTip == null)
                partyPackMenuToolTip = new ToolTip();
            pnl_menu.Controls.Add(partyPackMenuCategoryPanel);
            partyPackMenuCategoryPanel.BringToFront();
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page EnsurePartyPackMenuCategoryPanel()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void LoadPartyPackMenuCategories()
        {
            try
            {
                EnsurePartyPackMenuCategoryPanel();
                BllSearch<DmnCategory> searcher = new BllSearch<DmnCategory>();
                DmnSearch search = new DmnSearch
                {
                    Select = "DISTINCT c.CategoryName, c.Fcid",
                    Table = "tblCategory_FTI c",
                    WhereNdGroupBy = "WHERE c.Status = 1 AND c.CategoryType = "
                        + Convert.ToInt32(CategoryType.Menu_Category)
                        + " AND (EXISTS (SELECT 1 FROM tblProduct p WHERE p.Fcid = c.Fcid AND p.Status = 1)"
                        + " OR EXISTS (SELECT 1 FROM tblItem i WHERE i.Fcid = c.Fcid AND i.Status = 1 AND i.Saletype = 1))",
                    OrderBy = "ORDER BY c.CategoryName"
                };

                List<DmnCategory> categories = searcher.GetSearchData("prdSearch", search);
                partyPackMenuCategoryPanel.SuspendLayout();
                foreach (Control control in partyPackMenuCategoryPanel.Controls)
                    control.Dispose();
                partyPackMenuCategoryPanel.Controls.Clear();

                foreach (DmnCategory category in categories)
                {
                    Button categoryButton = new Button
                    {
                        Text = category.CategoryName,
                        Tag = category.Fcid,
                        Height = Math.Max(36, btn_showmenu.Height - 8),
                        Width = Math.Max(120, partyPackMenuCategoryPanel.ClientSize.Width - 24),
                        Margin = new Padding(4),
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.FromArgb(101, 80, 185),
                        ForeColor = Color.White,
                        Font = AppFonts.GetFont(10),
                        AutoEllipsis = true,
                        Cursor = Cursors.Hand
                    };

                    categoryButton.FlatAppearance.BorderSize = 0;
                    partyPackMenuToolTip.SetToolTip(categoryButton, category.CategoryName);
                    categoryButton.Click += (s, e) =>
                    {
                        int categoryId;
                        if (!int.TryParse(Convert.ToString(categoryButton.Tag), out categoryId))
                            return;
                        partyPackMenuCategoryId = categoryId;
                        partyPackMenuCurrentPage = 1;
                        partyPackMenuCategoryPanel.Hide();
                        LoadPartyPackMenuProducts(categoryId);
                    };

                    partyPackMenuCategoryPanel.Controls.Add(categoryButton);
                }

                partyPackMenuCategoryPanel.ResumeLayout(true);
                if (categories.Count == 0)
                {
                    partyPackMenuCategoryPanel.Visible = false;
                    LoadPartyPackMenuProducts(0);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page LoadPartyPackMenuCategories()", ex, FrmLogin.EmployeName,FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void EnsurePartyPackMenuProductsLoaded()
        {
            try
            {
            if (partyPackMenuProductsLoaded)
                return;

            BllSearch<DmnGridFields> searcher = new BllSearch<DmnGridFields>();
            DmnSearch search = new DmnSearch
            {
                Select = "p.Product AS Field1, p.ProductId AS Field2, 0 AS Field3, 0 AS Field4, p.Price AS Field5, p.MRP AS Field6, p.Tax AS Field7, p.TaxType AS Field8, p.Fcid AS Field9, p.TaxSettingsId AS Field10, p.ItemCode AS Field11, 0 AS Field12, ISNULL(p.foodtype,0) AS Field13, p.ForeColor AS Field14, p.BackColor AS Field15",
                Table = "tblProduct p",
                WhereNdGroupBy = "WHERE p.Status = 1",
                OrderBy = "ORDER BY p.Product"
            };

            // Rebuild the cache from scratch so reopening the menu can never append the same rows again.
            partyPackMenuAllProducts.Clear();
            var productRows = searcher.GetSearchData("prdSearch", search);
            if (productRows != null)
                partyPackMenuAllProducts.AddRange(productRows);

            search.Select = "i.Itemname AS Field1, i.ItemId AS Field2, i.Saletype AS Field3, 0 AS Field4, ISNULL((SELECT TOP 1 pr.SRate FROM tblProductRate pr WHERE pr.ProductId=i.ItemId AND pr.Status=1 ORDER BY pr.Id DESC),0) AS Field5, ISNULL((SELECT TOP 1 pr.MRP FROM tblProductRate pr WHERE pr.ProductId=i.ItemId AND pr.Status=1 ORDER BY pr.Id DESC),0) AS Field6, i.Tax AS Field7, i.TaxType AS Field8, i.Fcid AS Field9, i.TaxSettingsId AS Field10, i.ItemCode_UPC AS Field11, ISNULL(i.UnitId,0) AS Field12, 0 AS Field13, '' AS Field14, '' AS Field15";
            search.Table = "tblItem i";
            search.WhereNdGroupBy = "WHERE i.Status = 1 AND i.Saletype = 1";
            search.OrderBy = "ORDER BY i.Itemname";
            var itemRows = searcher.GetSearchData("prdSearch", search);
            if (itemRows != null)
                partyPackMenuAllProducts.AddRange(itemRows);

            List<DmnGridFields> distinctProducts = partyPackMenuAllProducts
                .GroupBy(p => new
                {
                    ProductId = Convert.ToString(p.Field2),
                    SaleType = Convert.ToString(p.Field3)
                })
                .Select(group => group.First())
                .ToList();

            partyPackMenuAllProducts.Clear();
            partyPackMenuAllProducts.AddRange(distinctProducts);

            partyPackMenuProductsLoaded = true;
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page EnsurePartyPackMenuProductsLoaded()", ex, FrmLogin.EmployeName,FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void LoadPartyPackMenuProducts(int categoryId, string productName = "", string itemCode = "")
        {
            try
            {
                EnsurePartyPackMenuProductsLoaded();
                partyPackMenuCategoryId = categoryId;
                IEnumerable<DmnGridFields> filtered = partyPackMenuAllProducts;

                if (categoryId > 0)
                    filtered = filtered.Where(p => Convert.ToInt32(p.Field9) == categoryId);
                if (!string.IsNullOrWhiteSpace(productName))
                    filtered = filtered.Where(p => (Convert.ToString(p.Field1) ?? string.Empty)
                        .IndexOf(productName.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0);
                if (!string.IsNullOrWhiteSpace(itemCode))
                    filtered = filtered.Where(p => string.Equals(Convert.ToString(p.Field11), itemCode.Trim(),
                        StringComparison.OrdinalIgnoreCase));

                partyPackMenuFilteredProducts = filtered
                    .OrderBy(p => Convert.ToString(p.Field1), StringComparer.CurrentCultureIgnoreCase)
                    .ToList();
                partyPackMenuCurrentPage = 1;
                ShowPartyPackMenuPage();
                tlp_menu_main.Visible = true;
                tlp_menu_main.BringToFront();
                flp_productview.Visible = true;
                Dgv_Selected_Item.Visible = false;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page LoadPartyPackMenuProducts()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private int CalculatePartyPackMenuPageSize()
        {
            try
            {
                int availableWidth = Math.Max(220, flp_productview.ClientSize.Width - flp_productview.Padding.Horizontal);
                int availableHeight = Math.Max(140, flp_productview.ClientSize.Height - flp_productview.Padding.Vertical);
                const int minimumCardWidth = 150;
                int gap = 8;
                int columns = Math.Max(1, availableWidth / (minimumCardWidth + (gap * 2)));
                int cardWidth = Math.Max(110, (availableWidth / columns) - (gap * 2));
                int cardHeight = Math.Max(92, Math.Min(138, (int)(cardWidth * 0.72m)));
                int rows = Math.Max(1, availableHeight / (cardHeight + (gap * 2)));
                return Math.Max(1, columns * rows);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page CalculatePartyPackMenuPageSize()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
                return 12;
            }
        }

        private void ResizePartyPackMenuContent()
        {
            try
            {
                if (pnl_dgv_menu.ClientSize.Width <= 0 || pnl_dgv_menu.ClientSize.Height <= 0)
                    return;
                tlp_menu_main.Dock = DockStyle.Fill;
                flp_productview.Dock = DockStyle.Fill;
                Dgv_Selected_Item.Dock = DockStyle.Fill;
                partyPackMenuPageSize = CalculatePartyPackMenuPageSize();
                if (partyPackMenuProductsLoaded)
                    ShowPartyPackMenuPage();
                ApplyPartyPackMenuVisualStyles();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page ResizePartyPackMenuContent()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void ShowPartyPackMenuPage()
        {
            try
            {
            int totalPages = (int)Math.Ceiling(partyPackMenuFilteredProducts.Count / (double)Math.Max(1, partyPackMenuPageSize));
            if (partyPackMenuCurrentPage < 1)
                partyPackMenuCurrentPage = 1;
            if (totalPages > 0 && partyPackMenuCurrentPage > totalPages)
                partyPackMenuCurrentPage = totalPages;

            partyPackMenuPageProducts = partyPackMenuFilteredProducts
                .Skip((partyPackMenuCurrentPage - 1) * partyPackMenuPageSize)
                .Take(partyPackMenuPageSize)
                .ToList();

            lblpagerinfo.Text = partyPackMenuFilteredProducts.Count == 0
                ? "No products found"
                : $"Page {partyPackMenuCurrentPage} of {Math.Max(1, totalPages)} | Total Records: {partyPackMenuFilteredProducts.Count}";
            btnprev.Enabled = partyPackMenuCurrentPage > 1;
            btnnext.Enabled = partyPackMenuCurrentPage < totalPages;

            flp_productview.SuspendLayout();
            foreach (Control control in flp_productview.Controls)
                control.Dispose();
            flp_productview.Controls.Clear();

            flp_productview.WrapContents = true;
            flp_productview.FlowDirection = FlowDirection.LeftToRight;
            flp_productview.AutoScroll = true;
            flp_productview.Padding = new Padding(6);
            int gap = 8;
            int availableWidth = Math.Max(220, flp_productview.ClientSize.Width - flp_productview.Padding.Horizontal);
            int columns = Math.Max(1, availableWidth / 166);
            int tileWidth = Math.Max(110, (availableWidth / columns) - (gap * 2));
            int tileHeight = Math.Max(92, Math.Min(138, (int)(tileWidth * 0.72m)));

            foreach (DmnGridFields product in partyPackMenuPageProducts)
            {
                int productId, categoryId, saleType, unitId;
                int.TryParse(Convert.ToString(product.Field2), out productId);
                int.TryParse(Convert.ToString(product.Field9), out categoryId);
                int.TryParse(Convert.ToString(product.Field3), out saleType);
                int.TryParse(Convert.ToString(product.Field12), out unitId);
                decimal price;
                decimal.TryParse(Convert.ToString(product.Field5), out price);

                decimal mrp;
                decimal.TryParse(Convert.ToString(product.Field6), out mrp);
                int menuUnitId;
                int.TryParse(Convert.ToString(product.Field12), out menuUnitId);
                int menuTaxSettingId;
                int.TryParse(Convert.ToString(product.Field10), out menuTaxSettingId);

                PartyPackMenuSelection selection = new PartyPackMenuSelection
                {
                    ProductId = productId,
                    SaleType = saleType,
                    CategoryId = categoryId,
                    ProductName = Convert.ToString(product.Field1) ?? string.Empty,
                    ItemCode = Convert.ToString(product.Field11) ?? string.Empty,
                    Price = price,
                    MRP = mrp > 0m ? mrp : price,
                    UnitId = menuUnitId,
                    TaxSettingId = menuTaxSettingId,
                    TaxMode = Convert.ToString(product.Field7) ?? "Exclusive",
                    TaxType = Convert.ToString(product.Field8) ?? "GST"
                };

                Panel tile = new Panel
                {
                    Width = tileWidth,
                    Height = tileHeight,
                    Margin = new Padding(gap),
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.None,
                    Tag = selection,
                    Cursor = Cursors.Hand
                };
                Label productLabel = new Label
                {
                    Text = selection.ProductName,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Padding = new Padding(6),
                    AutoEllipsis = true,
                    ForeColor = Color.Black,
                    Font = AppFonts.GetFont(11),
                    BackColor = Color.Transparent,
                    Tag = selection
                };
                tile.Controls.Add(productLabel);
                tile.Click += (s, e) => AddPartyPackMenuProduct(selection);
                productLabel.Click += (s, e) => AddPartyPackMenuProduct(selection);
                UIHelper.RoundCorners(tile, 15);
                flp_productview.Controls.Add(tile);
            }
            flp_productview.ResumeLayout(true);
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page ShowPartyPackMenuPage()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void AddPartyPackMenuProduct(PartyPackMenuSelection selection)
        {
            try
            {
            if (selection == null || selection.ProductId <= 0 || string.IsNullOrWhiteSpace(selection.ProductName))
                return;

            ConfigurePartyPackMenuCartGrid();

            DataGridViewRow existing = Dgv_Selected_Item.Rows
                .Cast<DataGridViewRow>()
                .FirstOrDefault(row => !row.IsNewRow
                    && row.Tag is PartyPackMenuSelection
                    && ((PartyPackMenuSelection)row.Tag).ProductId == selection.ProductId
                    && ((PartyPackMenuSelection)row.Tag).SaleType == selection.SaleType);

            if (existing != null)
            {
                int quantity = 1;
                int.TryParse(Convert.ToString(existing.Cells[iqty].Value), out quantity);
                existing.Cells[iqty].Value = quantity + 1;
                UpdatePartyPackMenuCartRow(existing, selection);
                ShowPartyPackMenuCart();
                return;
            }

            int rowIndex = Dgv_Selected_Item.Rows.Add();
            DataGridViewRow addedRow = Dgv_Selected_Item.Rows[rowIndex];
            addedRow.Tag = selection;
            UpdatePartyPackMenuCartRow(addedRow, selection);
            ShowPartyPackMenuCart();
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page AddPartyPackMenuProduct()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }


        private void UpdatePartyPackMenuCartRow(DataGridViewRow row, PartyPackMenuSelection selection)
        {
            if (row == null || selection == null)
                return;

            int quantity = 1;
            int.TryParse(Convert.ToString(row.Cells[iqty].Value), out quantity);
            if (quantity < 1)
                quantity = 1;

            decimal amount = Math.Round(selection.Price * quantity, 2, MidpointRounding.AwayFromZero);
            row.Tag = selection;
            row.Cells[iaction].Value = Grid_Remove;
            row.Cells[islno].Value = row.Index + 1;
            row.Cells[iunit].Value = selection.UnitId > 0 ? selection.UnitId.ToString() : "";
            row.Cells[ibarcode].Value = selection.ItemCode;
            row.Cells[iproducttypeno].Value = "Food";
            row.Cells[iname].Value = selection.ProductName;
            row.Cells[iunit].Value = selection.UnitId > 0 ? selection.UnitId.ToString() : "";
            row.Cells[israte].Value = selection.Price;
            row.Cells[iqty].Value = quantity;
            row.Cells[iprate].Value = selection.Price;
            row.Cells[iamount].Value = amount;
            row.Cells[idisper].Value = 0m;
            row.Cells[idisamt].Value = 0m;
            row.Cells[itaxper].Value = 0m;
            row.Cells[itotal].Value = amount;
            row.Cells[iitemid].Value = selection.ProductId;
            row.Cells[itaxtype].Value = string.IsNullOrWhiteSpace(selection.TaxType) ? "GST" : selection.TaxType;
            row.Cells[imrp].Value = selection.MRP > 0m ? selection.MRP : selection.Price;
            row.Cells[itype].Value = selection.SaleType;
            row.Cells[ibarcode2].Value = selection.ItemCode;
            row.Cells[itaxsettingid].Value = selection.TaxSettingId;
            row.Cells[iunitid].Value = selection.UnitId;
            //row.Cells[itax].Value = string.IsNullOrWhiteSpace(selection.TaxMode) ? "Exclusive" : selection.TaxMode;
            row.Cells[ipretaxamtno].Value = amount;
            row.Cells[ioriginalprice].Value = selection.Price;
            row.Cells[istatusno].Value = 1;
            row.Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
            row.Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
            row.Cells[itabletype].Value = Convert.ToInt32(Tabletype.Product);
            row.Cells[iorgname].Value = selection.ProductName;
            row.Cells[istocktrackno].Value = false;

            DataGridViewImageCell actionCell = row.Cells[iaction] as DataGridViewImageCell;
            if (actionCell != null)
                actionCell.ImageLayout = DataGridViewImageCellLayout.Zoom;

            int slno = 1;
            foreach (DataGridViewRow selected in Dgv_Selected_Item.Rows)
            {
                if (!selected.IsNewRow && selected.Tag is PartyPackMenuSelection)
                    selected.Cells[islno].Value = slno++;
            }
        }

        private void ShowPartyPackMenuCart()
        {
            try
            {
                partyPackMenuCategoryPanel?.Hide();
                flp_productview.Visible = false;
                Dgv_Selected_Item.Visible = true;
                Dgv_Selected_Item.BringToFront();
                btn_cart.BackgroundImage = Properties.Resources.Party_Food_Items_Icon;
                btn_cart.BackgroundImageLayout = ImageLayout.Stretch;
                tlp_menu_main.Visible = true;
                tlp_menu_main.SendToBack();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page ShowPartyPackMenuCart()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void ApplyPartyPackMenuVisualStyles()
        {
            try
            {
                Color accentPurple = Color.FromArgb(101, 80, 185);
                Color menuLavender = Color.FromArgb(236, 220, 244);
                Color softLavender = Color.FromArgb(236, 220, 244);
                Color borderLavender = Color.FromArgb(226, 214, 239);
                Color textColor = Color.FromArgb(42, 34, 55);
                flp_productview.BackColor = softLavender;

                flp_productview.Padding = new Padding(6);
                flp_productview.Margin = Padding.Empty;
                flp_productview.BorderStyle = BorderStyle.None;
                tlp_menu_main.BackColor = menuLavender;
                Dgv_Selected_Item.BorderStyle = BorderStyle.None;
                Dgv_Selected_Item.BackgroundColor = Color.White;
                Dgv_Selected_Item.GridColor = borderLavender;
                Dgv_Selected_Item.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                Dgv_Selected_Item.EnableHeadersVisualStyles = false;
                Dgv_Selected_Item.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                Dgv_Selected_Item.ColumnHeadersHeight = 42;
                Dgv_Selected_Item.ColumnHeadersDefaultCellStyle.BackColor = accentPurple;
                Dgv_Selected_Item.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                Dgv_Selected_Item.ColumnHeadersDefaultCellStyle.Font = new Font(AppFonts.GetFont(10), FontStyle.Bold);
                Dgv_Selected_Item.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Dgv_Selected_Item.DefaultCellStyle.BackColor = Color.White;
                Dgv_Selected_Item.DefaultCellStyle.ForeColor = textColor;
                Dgv_Selected_Item.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 218, 244);
                Dgv_Selected_Item.DefaultCellStyle.SelectionForeColor = textColor;
                Dgv_Selected_Item.DefaultCellStyle.Font = AppFonts.GetFont(10);
                Dgv_Selected_Item.DefaultCellStyle.Padding = new Padding(4, 3, 4, 3);
                Dgv_Selected_Item.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 246, 253);
                Dgv_Selected_Item.RowTemplate.Height = 40;
                Dgv_Selected_Item.RowHeadersVisible = false;
                Dgv_Selected_Item.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Dgv_Selected_Item.MultiSelect = false;
                UIHelper.RoundCorners(pnl_dgv_menu, 25);
                UIHelper.RoundCorners(tlp_footer, 25);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page ApplyPartyPackMenuVisualStyles()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void DgvPartyPackMenu_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                    e.CellStyle.ForeColor = Color.Black;
                if (e.RowIndex == -1)
                {
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                    e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                    e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
                    e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
                    e.PaintBackground(e.CellBounds, true);
                    e.PaintContent(e.CellBounds);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page DgvPartyPackMenu_CellPainting()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void ConfigurePartyPackMenuCartGrid()
        {
            try
            {
            Dgv_Selected_Item.ReadOnly = true;
            Dgv_Selected_Item.AllowUserToAddRows = false;
            Dgv_Selected_Item.AutoGenerateColumns = false;
            Dgv_Selected_Item.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Dgv_Selected_Item.RowHeadersVisible = false;
            Dgv_Selected_Item.RowTemplate.Height = 40;
            Dgv_Selected_Item.ColumnHeadersHeight = 42;
            Dgv_Selected_Item.DefaultCellStyle.Padding = new Padding(4, 3, 4, 3);

            // Preserve all designer-defined columns and their current visibility.
            if (Dgv_Selected_Item.Columns.Contains("ipaction"))
            {
                Dgv_Selected_Item.Columns["ipaction"].HeaderText = "";
                Dgv_Selected_Item.Columns["ipaction"].Width = 42;
            }
            if (Dgv_Selected_Item.Columns.Contains("dataGridViewTextBoxColumn8"))
            {
                Dgv_Selected_Item.Columns["dataGridViewTextBoxColumn8"].HeaderText = "Sl.No";
                Dgv_Selected_Item.Columns["dataGridViewTextBoxColumn8"].Width = 58;
            }
            if (Dgv_Selected_Item.Columns.Contains("name"))
            {
                Dgv_Selected_Item.Columns["name"].Visible = true;
                Dgv_Selected_Item.Columns["name"].HeaderText = "Name";
                Dgv_Selected_Item.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Dgv_Selected_Item.Columns["name"].MinimumWidth = 150;
            }
            if (Dgv_Selected_Item.Columns.Contains("Unit"))
                Dgv_Selected_Item.Columns["Unit"].Visible = true;
            if (Dgv_Selected_Item.Columns.Contains("dgv_sele_igstamt"))
                Dgv_Selected_Item.Columns["dgv_sele_igstamt"].Visible = true;
            if (Dgv_Selected_Item.Columns.Contains("dgv_sele_vatamt"))
                Dgv_Selected_Item.Columns["dgv_sele_vatamt"].Visible = true;
            if (Dgv_Selected_Item.Columns.Contains("opretaxamt"))
            {
                Dgv_Selected_Item.Columns["opretaxamt"].Visible = true;
                Dgv_Selected_Item.Columns["opretaxamt"].HeaderText = "PretaxAmt";
            }
            if (Dgv_Selected_Item.Columns.Contains("dataGridViewTextBoxColumn12"))
            {
                Dgv_Selected_Item.Columns["dataGridViewTextBoxColumn12"].HeaderText = "Qty";
                Dgv_Selected_Item.Columns["dataGridViewTextBoxColumn12"].Width = 72;
            }
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page ConfigurePartyPackMenuCartGrid()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void DgvPartyPackMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Dgv_Selected_Item.Columns[e.ColumnIndex].Name != "ipaction")
                return;

            if (e.RowIndex < Dgv_Selected_Item.Rows.Count)
                Dgv_Selected_Item.Rows.RemoveAt(e.RowIndex);

            for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
            {
                DataGridViewRow row = Dgv_Selected_Item.Rows[i];
                row.Cells["dataGridViewTextBoxColumn8"].Value = i + 1;
            }
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page DgvPartyPackMenu_CellClick()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void PartyPackMenuSearchTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;
            string placeholder = textBox == txtname_search ? "Food Name ....." : "Item Code .....";
            if (textBox.Text == placeholder)
                textBox.Clear();
            textBox.ForeColor = Color.FromArgb(42, 34, 55);
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page PartyPackMenuSearchTextBox_Enter()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void PartyPackMenuSearchTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
            TextBox textBox = sender as TextBox;
            if (textBox == null || !string.IsNullOrWhiteSpace(textBox.Text))
                return;
            SetPartyPackMenuSearchPlaceholders();
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page PartyPackMenuSearchTextBox_Leave()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void PartyPackMenuSearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
            if (e.KeyCode == Keys.Enter)
            {
                btn_serach_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page PartyPackMenuSearchTextBox_KeyDown()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void SetPartyPackMenuSearchPlaceholders()
        {
            try
            {
            if (string.IsNullOrWhiteSpace(txtname_search.Text))
            {
                txtname_search.Text = "Food Name .....";
                txtname_search.ForeColor = Color.DarkGray;
            }
            if (string.IsNullOrWhiteSpace(txtitemcode_serach.Text))
            {
                txtitemcode_serach.Text = "Item Code .....";
                txtitemcode_serach.ForeColor = Color.DarkGray;
            }
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page SetPartyPackMenuSearchPlaceholders()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        private void ClosePartyPackMenu()
        {
            try
            {
            pnl_menu.Visible = false;
            partyPackMenuCategoryPanel?.Hide();
        
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partypacksetup page ClosePartyPackMenu()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partypacksetup));
            }
        }

        #endregion
    }
}
