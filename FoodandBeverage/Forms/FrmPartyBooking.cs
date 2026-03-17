using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms;
using FoodandBeverage.Forms.UserControl;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Control = System.Windows.Forms.Control;
using Font = System.Drawing.Font;

namespace FoodandBeverage
{
    public partial class FrmPartyBooking : Form
    {
        #region Decalation

        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsgen = new ClsGeneral();

        #region  Grid Declaration

        public int showonlyslno = 0;
        public int showonlyitemno = 1;
        public int showonlycategmenuno = 2;
        public int showonlyqtyno = 3;
        public int showonlyactionno = 4;
        public int showonlyppcdiidno = 5;

        public int customitemslno = 0;
        public int customitemselectNo = 1;
        public int customitemno = 2;
        public int customitemcategmenuno = 3;
        public int customitemqtyno = 4;
        public int customitemppcdiidno = 5;

        public int selecteditemslno = 0;
        public int selecteditemitemno = 1;
        public int selecteditemcategmenuno = 2;
        public int selecteditemqtyno = 3;
        public int selecteditemppcdiidno = 4;
        public int selecteditemdeafultno = 5;

        public int pslno = 0;
        public int pactionno = 1;
        public int pitemno = 2;
        public int pcategmenuno = 3;
        public int pqtyno = 4;
        public int pPrice = 5;
        public int pAmount = 6;
        public int ptax = 7;
        public int ptaxtype = 8;
        public int ptaxper = 9;
        public int ptaxamt = 10;
        public int pdiscper = 11;
        public int pdisamt = 12;
        public int pigstamt = 13;
        public int pvatamt = 14;
        public int pnetamt = 15;
        public int pProductid = 16;
        public int pproducttype = 17;
        public int proPartyPackDetailsId = 18;
        public int pPartydeafult = 19;
        public int ptaxsettingid = 20;
        public int ppretaxamt = 21;
        public int pmessageno = 22;
        public int premarksno = 23;
        public int psalemasteridno = 24;
        public int psaledetailidno = 25;
        public int PPackProductTypeno = 26;
        public int ppridno = 27;

        public int Cancelslno = 0;
        public int Cancelproductno = 1;
        public int Cancelqtyno = 2;
        public int Cancelpriceno = 3;


        public int aislno = 0;
        public int aiactionno = 1;
        public int aiitemno = 2;
        public int aicategmenuno = 3;
        public int aiqtyno = 4;
        public int aiPrice = 5;
        public int aiAmount = 6;
        public int aitax = 7;
        public int aitaxtype = 8;
        public int aitaxper = 9;
        public int aitaxamt = 10;
        public int aidiscper = 11;
        public int aidisamt = 12;
        public int aiigstamt = 13;
        public int aivatamt = 14;
        public int ainetamt = 15;
        public int aiProductid = 16;
        public int aiproducttype = 17;
        public int aiproPartyPackDetailsId = 18;
        public int aiPartydeafult = 19;
        public int aitaxsettingid = 20;
        public int aipretaxamt = 21;
        public int aimessageno = 22;
        public int airemarksno = 23;
        public int aisalemasteridno = 24;
        public int aisaledetailidno = 25;
        public int aiPackProductTypeno = 26;


        #endregion

        private List<DmnPartyPackDetails> packDetailsList;
        private List<DmnCategory> categoryList = new List<DmnCategory>();

        List<SelectedCustomItem> selectedCustomItems = new List<SelectedCustomItem>();
        int CurrentPackId = 0;
        List<DmnPartyPackCustom> customMenuList = new List<DmnPartyPackCustom>();

        #region Domain

        public class BookingItemAdjustment
        {
            public int SlNo { get; set; }
            public string Product { get; set; }
            public string ProductCategory { get; set; }
            public decimal Qty { get; set; }
            public decimal Price { get; set; }
            public decimal Amount { get; set; }
            public int ProductId { get; set; }
            public int SaleDetailId { get; set; }
            public int ProductType { get; set; }
            public int PartyBookingDetailsid { get; set; }
            public string AdjustmentType { get; set; } 
        }

        public class SelectedCustomItem
        {
            public int PartyPackCustomDetailsId { get; set; }
            public string ItemName { get; set; }
            public int CategoryId { get; set; }
            public decimal Qty { get; set; }
        }
        #endregion

        // Cake
        private int _cakeId = 0;
        private string _cakeSize = "";
        private string _cakeShape = "";
        private string _cakeMessage = "";
        private string _cakeRemarks = "";
        private decimal _cakePrice = 0;

        // Tax
        private string _cakeTaxType = "";      // GST / VAT
        private decimal _cakeTaxPer = 0;       // 5, 12, 18
        private int _cakeTaxSettingsId = 0;    // Tax Id
        private string _cakeTaxMode = "";      // Inclusive / Exclusive

        // ===== CAKE TAX GLOBALS =====
        private decimal _cakePreTaxAmount = 0;
        private decimal _cakeTaxAmount = 0;
        private decimal _cakeNetAmount = 0;

        int CategoryFontstatus = Convert.ToInt32(ConfigurationManager.AppSettings["categoryfontSettingvalue"]);
        string CategoryFontdata = Convert.ToString(ConfigurationManager.AppSettings["categoryfontdata"]);
        string Categorybg = Convert.ToString(ConfigurationManager.AppSettings["categorybg"]);

        public int screnwidth = Screen.PrimaryScreen.Bounds.Width;
        public int screnheight = Screen.PrimaryScreen.Bounds.Height;
        public string GameDB = Convert.ToString(ConfigurationManager.AppSettings["GameDB"]);

        int Partypackid = 0;
        int PaymentMode = 0;
        bool AdvanceFlag = false;

        decimal dbReceivedAmount = 0;
        decimal dbAdvanceAmount = 0;

        private List<(string PartPayId, string PartAmount)> partPayments = new List<(string, string)>();
        int partCount = 0;
        public bool exitstatus = false;
        int BookingId = 0, SalesMasterId = 0, PartyBookingDetailsId = 0,Partypacksaledetailid = 0;
        decimal MinimumAdvance = 0;

        private readonly Image Grid_Remove = Properties.Resources.Round_Close;
        private readonly Image Blankimage = Properties.Resources.backspace;
        private bool isCategoryExpanded = false;

        List<BookingItemAdjustment> bookingItemAdjustments = new List<BookingItemAdjustment>();

        decimal dbNetAmount = 0;
        decimal dbBalanceAmount = 0;

        private bool isUpdating = false;
        decimal originalGrandTotal = 0;
        #endregion
        public FrmPartyBooking()
        {
            InitializeComponent();
        }

        private void FrmPartyBooking_Load(object sender, EventArgs e)
        {
            try
            {
                lblNetAmt.ForeColor = Color.DarkBlue;
                lbladvamtdata.ForeColor = Color.SeaGreen;
                txtRcvAmt.ForeColor = Color.Black;
                lblBalAmt.ForeColor = Color.IndianRed;

                lblnetcaption.ForeColor = Color.DimGray;
                lbladvamt.ForeColor = Color.DimGray;
                lblreciamt.ForeColor = Color.DimGray;
                lblbalapay.ForeColor = Color.DimGray;

                lblpackname.ForeColor = Color.DimGray;

                ToggleFirstRow(0);
                TogglePayCancel(0);

                textb_shape();
                if (FrmLogin.GameBranchId > 0)
                {
                    btngift.Visible = true;
                    btncard.Visible = true;
                }
                else
                {
                    btngift.Visible = false;
                    btncard.Visible = false;
                }
                LoadEventItems();
            }
            catch(Exception ex)
            {

            }
        }

        #region Events And common Fuction 
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void btnfoodpack_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnl_foodpacks.Visible)
                {
                    pnl_foodpacks.Visible = false;
                    return;
                }
                pnl_foodpacks.Width = (int)(this.ClientSize.Width * 0.7);
                pnl_foodpacks.Height = (int)(this.ClientSize.Height * 0.7);

                pnl_foodpacks.Left = (this.ClientSize.Width - pnl_foodpacks.Width) / 2;
                pnl_foodpacks.Top = (this.ClientSize.Height - pnl_foodpacks.Height) / 2;

                pnl_foodpacks.Visible = true;
                pnl_foodpacks.BringToFront();

                LoadPartyPacks();
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Partybooking btnfoodpack_Click()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking)
                );
            }
        }
        private void btn_pack_close_Click(object sender, EventArgs e)
        {
            pnl_foodpacks.Visible = false;
        }
        private void btn_orderdesc_submit_Click(object sender, EventArgs e)
        {
            pnl_foodpacks.Visible = false;
        }
        public void Loadprice(int pkid)
        {
            try
            {
                BllSearch<DmnPartyPack> bll = new BllSearch<DmnPartyPack>();

                DmnSearch search = new DmnSearch();
                search.Select = @"
                            PartyPackId,
                            PackName,
                            AdultPrice,
                            ChildPrice,Tax,TaxSettingsId,
                            (SELECT COUNT(1) 
                             FROM tblPartyPackDetails d 
                             WHERE d.PartyPackId = tblPartyPackMaster.PartyPackId 
                             AND d.Status = 1) AS DetailsCount,MinimumAdvanceAmount";
                search.Table = "tblPartyPackMaster";
                search.WhereNdGroupBy = "WHERE Status = 1 and PartyPackId ="+ pkid;
                search.OrderBy = "ORDER BY PackName";
                var list = bll.GetSearchData("prdSearch", search);
                if (list.Count > 0)
                {
                    txtChildPrice.Text = Convert.ToString(list[0].ChildPrice);
                    txtAdultPrice.Text = Convert.ToString(list[0].AdultPrice);
                    txtTaxSettingId.Text = Convert.ToString(list[0].TaxSettingsId);
                    txttax.Text = Convert.ToString(list[0].Tax);
                    MinimumAdvance= Convert.ToDecimal(list[0].MinimumAdvanceAmount);
                }
     
            }
            catch (Exception ex)
            {
            }
        }
        private void LoadPartyPacks()
        {
            try
            {
                flp_show_pack_resmenu.Controls.Clear();

                BllSearch<DmnPartyPack> bll = new BllSearch<DmnPartyPack>();

                DmnSearch search = new DmnSearch();
                search.Select = @"
                            PartyPackId,
                            PackName,
                            AdultPrice,
                            ChildPrice,Tax,TaxSettingsId,
                            (SELECT COUNT(1) 
                             FROM tblPartyPackDetails d 
                             WHERE d.PartyPackId = tblPartyPackMaster.PartyPackId 
                             AND d.Status = 1) AS DetailsCount";
                search.Table = "tblPartyPackMaster";
                search.WhereNdGroupBy = "WHERE Status = 1 ";
                search.OrderBy = "ORDER BY PackName";

                var list = bll.GetSearchData("prdSearch", search);

                if (list.Count > 0)
                {
                    txtChildPrice.Text = Convert.ToString(list[0].ChildPrice);
                    txtAdultPrice.Text = Convert.ToString(list[0].AdultPrice);

                    txttax.Text = Convert.ToString(list[0].Tax);
                    txtTaxSettingId.Text = Convert.ToString(list[0].TaxSettingsId);


                    for (int col = 1; col < tlp_reg_contrl.ColumnCount; col++)
                    {
                        Control c1 = tlp_reg_contrl.GetControlFromPosition(col, 4);
                        if (c1 != null) tlp_reg_contrl.Controls.Remove(c1);

                        Control c2 = tlp_reg_contrl.GetControlFromPosition(col, 5);
                        if (c2 != null) tlp_reg_contrl.Controls.Remove(c2);
                    }

                    int rowLabel = 4;
                    int rowTextBox = 5;

                    lbladult.Dock = DockStyle.Fill;
                    txtAdults.Dock = DockStyle.Fill;

                    lbltotalguest.Dock = DockStyle.Fill;
                    txttotalguest.Dock = DockStyle.Fill;

                    txtChild.Visible = false;
                    lblnochild.Visible = false;

                    txtAdults.Visible = false;
                    lbladult.Visible = false;
                    tlp_adult_child_label.Dock = DockStyle.Fill;
                    tlp_adult_child_text.Dock = DockStyle.Fill;

                    if ((list[0].ChildPrice > 0) && (list[0].AdultPrice > 0))
                    {
                        tlp_reg_contrl.Controls.Add(tlp_adult_child_label, 1, rowLabel);
                        tlp_reg_contrl.Controls.Add(tlp_adult_child_text, 1, rowTextBox);

                        tlp_reg_contrl.Controls.Add(lbltotalguest, 2, rowLabel);
                        tlp_reg_contrl.Controls.Add(txttotalguest, 2, rowTextBox);

                        txtChild.Visible = true;
                        lblnochild.Visible = true;

                        txtAdults.Visible = true;
                        lbladult.Visible = true;
                    }
                    else if (list[0].ChildPrice > 0)
                    {
                        lblnochild.Dock = DockStyle.Fill;
                        txtChild.Dock = DockStyle.Fill;

                        tlp_reg_contrl.Controls.Add(tlp_adult_child_label, 1, rowLabel);
                        tlp_reg_contrl.Controls.Add(tlp_adult_child_text, 1, rowTextBox);

                        tlp_reg_contrl.Controls.Add(lbltotalguest, 2, rowLabel);
                        tlp_reg_contrl.Controls.Add(txttotalguest, 2, rowTextBox);

                        txtChild.Visible = true;
                        lblnochild.Visible = true;

                        txtAdults.Visible = false;
                        lbladult.Visible = false;
                    }
                    else
                    {
                        tlp_reg_contrl.Controls.Add(tlp_adult_child_label, 1, rowLabel);
                        tlp_reg_contrl.Controls.Add(tlp_adult_child_text, 1, rowTextBox);

                        txtChild.Visible = false;
                        lblnochild.Visible = false;

                        txtAdults.Visible = true;
                        lbladult.Visible = true;
                    }

                    Partypackid = Convert.ToInt32(list[0].PartyPackId);

                    foreach (var item in list)
                    {
                        // 🔷 CARD
                        RoundedPanel card = new RoundedPanel();
                        card.Width = 260;
                        card.Height = 140;
                        card.Margin = new Padding(15);
                        card.BackColor = Color.FromArgb(245, 247, 250); // light grey
                        card.Tag = item.PartyPackId;
                        card.Cursor = Cursors.Hand;

                        // 🔷 SHADOW EFFECT (fake shadow panel)
                        Panel shadow = new Panel();
                        shadow.BackColor = Color.FromArgb(30, 0, 0, 0);
                        shadow.Size = card.Size;
                        shadow.Location = new Point(card.Left + 3, card.Top + 3);

                        // 🔷 HOVER EFFECT
                        card.MouseEnter += (s, e) =>
                        {
                            card.BackColor = Color.FromArgb(230, 240, 255); // soft blue hover
                        card.Location = new Point(card.Location.X, card.Location.Y - 3);
                        };

                        card.MouseLeave += (s, e) =>
                        {
                            card.BackColor = Color.FromArgb(245, 247, 250);
                            card.Location = new Point(card.Location.X, card.Location.Y + 3);
                        };

                        // 🔷 PACK NAME
                        Label lblName = new Label();
                        lblName.Text = item.PackName;
                        lblName.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                        lblName.Width = card.Width - 20;
                        lblName.Location = new Point(10, 10);

                        // 🔷 PRICE
                        Label lblPrice = new Label();
                        lblPrice.Font = new Font("Segoe UI", 9);
                        lblPrice.ForeColor = Color.SeaGreen;
                        lblPrice.Width = card.Width - 20;
                        lblPrice.Location = new Point(10, 40);

                        if (item.AdultPrice > 0 && item.ChildPrice > 0)
                            lblPrice.Text = $"Adult ₹{item.AdultPrice} | Child ₹{item.ChildPrice}";
                        else if (item.AdultPrice > 0)
                            lblPrice.Text = $"₹{item.AdultPrice}";
                        else
                            lblPrice.Text = $"₹{item.ChildPrice}";

                        // 🔷 BIG SELECT BUTTON
                        Button btnSelect = new Button();
                        btnSelect.Text = "SELECT PACK";
                        btnSelect.Width = card.Width - 20;
                        btnSelect.Height = 35;
                        btnSelect.Location = new Point(10, card.Height - 45);
                        btnSelect.BackColor = Color.FromArgb(46, 204, 113);
                        btnSelect.ForeColor = Color.White;
                        btnSelect.FlatStyle = FlatStyle.Flat;
                        btnSelect.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        btnSelect.Tag = item.PartyPackId;

                        btnSelect.Click += (s, e) =>
                        {
                            ClearAll();

                            pnl_foodpacks.Visible = false;

                            lbltotalguest.Visible = true;
                            txttotalguest.Visible = true;
                            tlp_adult_child_label.Visible = true;
                            tlp_adult_child_text.Visible = true;
                            lblpackname.Visible = true;
                            lblpacklabel.Visible = true;

                            int packId = Convert.ToInt32(((Button)s).Tag);
                            Loadprice(packId);
                            if (item.DetailsCount > 0)
                            {
                                OpenPackEntryPanel();
                                LoadPartyPackDetails(packId);
                                LoadPackRelatedItems(packId);
                            }
                            else
                            {
                                lblpackname.Visible = true;
                                lblpacklabel.Visible = true;
                                lblpackname.Text = SetPackName(packId);
                                OpenPackEntryPanel();
                            }
                            textb_shape();
                        };

                        if (item.DetailsCount > 0)
                        {
                            Label lblView = new Label();
                            lblView.Text = "View Details →";
                            lblView.ForeColor = Color.RoyalBlue;
                            lblView.Font = new Font("Segoe UI", 8, FontStyle.Underline);
                            lblView.AutoSize = true;
                            lblView.Location = new Point(10, card.Height - 65);
                            lblView.Cursor = Cursors.Hand;
                            lblView.Tag = item.PartyPackId;

                            lblView.Click += (s, e) =>
                            {
                                int packId = Convert.ToInt32(((Label)s).Tag);

                                pnl_pak_detailsitems.Visible = true;
                                pnl_pak_detailsitems.BringToFront();

                                pnl_pak_detailsitems.Width = (int)(this.Width * 0.5);
                                pnl_pak_detailsitems.Height = (int)(this.Height * 0.5);

                                pnl_pak_detailsitems.Left = (this.Width - pnl_pak_detailsitems.Width) / 2;
                                pnl_pak_detailsitems.Top = (this.Height - pnl_pak_detailsitems.Height) / 2;

                                LoadPackItems(packId);
                            };

                            card.Controls.Add(lblView);
                        }

                        card.Click += (s, e) =>
                        {
                            int packId = Convert.ToInt32(card.Tag);
                        };

                        card.Controls.Add(lblName);
                        card.Controls.Add(lblPrice);
                        card.Controls.Add(btnSelect);

                        flp_show_pack_resmenu.Controls.Add(card);
                    }
                }
                else
                {
                    CMessageBox.Show("No packages found ", "Package Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void LoadPartyPackDetails(int packid)
        {
            try
            {
                dgv_packenter.Rows.Clear();

                int sl = 1;

                BllSearch<DmnPartyPackDetails> bll = new BllSearch<DmnPartyPackDetails>();
                DmnSearch search = new DmnSearch();
                search.Select = "PartyPackDetailsId, Item, Quantity, MenuCategoryId";
                search.Table = "tblPartyPackDetails";
                search.WhereNdGroupBy = $"WHERE PartyPackId = {packid} AND Status = 1 and PackItemType =" + Convert.ToInt32(PackItemType.FoodItem);

                var result = bll.GetSearchData("prdSearch", search);

                if (result != null)
                {
                    lblpackname.Visible = true;
                    lblpacklabel.Visible = true;
                    lblpackname.Text = SetPackName(packid);

                    foreach (var item in result)
                    {
                        int rowIndex = dgv_packenter.Rows.Add();
                        var row = dgv_packenter.Rows[rowIndex];

                        row.Cells[selecteditemslno].Value = sl++;
                        row.Cells[selecteditemitemno].Value = item.Item;

                        if(item.MenuCategoryId!=null &&Convert.ToInt32(item.MenuCategoryId)>0 )
                            row.Cells[selecteditemcategmenuno].Value =Convert.ToInt64(item.MenuCategoryId);

                        row.Cells[selecteditemqtyno].Value = item.Quantity;
                        row.Cells[selecteditemppcdiidno].Value = item.PartyPackDetailsId;
                        row.Cells[selecteditemdeafultno].Value = 1;
                        row.ReadOnly = true;
                    }
                }

                int newRowIndex = dgv_packenter.Rows.Add();
                dgv_packenter.Rows[newRowIndex].Cells[selecteditemslno].Value = sl;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private string SetPackName(int packId)
        {
            BllSearch<DmnPartyPack> bll = new BllSearch<DmnPartyPack>();

            DmnSearch search = new DmnSearch();
            search.Select = "PackName";
            search.Table = "tblPartyPackMaster";
            search.WhereNdGroupBy = $"WHERE PartyPackId = {packId} AND Status = 1";

            var result = bll.GetSearchData("prdSearch", search)
                            .FirstOrDefault();

            if (result != null)
                return result.PackName;

            return "";
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

                DataGridViewComboBoxColumn cmb = (DataGridViewComboBoxColumn)dgv_packs_items.Columns[showonlycategmenuno];

                cmb.DataSource = categoryList;
                cmb.DisplayMember = "CategoryName";
                cmb.ValueMember = "Fcid";
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void LoadMenuCategory_cutom()
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

                DataGridViewComboBoxColumn cmb = (DataGridViewComboBoxColumn)dgv_cus_menu_items.Columns[customitemcategmenuno];

                cmb.DataSource = categoryList;
                cmb.DisplayMember = "CategoryName";
                cmb.ValueMember = "Fcid";
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void LoadPackItems(int packId)
        {
            try
            {
                dgv_packs_items.Rows.Clear();
                flp_pakdet_item_menu.Controls.Clear();

                BllSearch<DmnPartyPackDetails> bll = new BllSearch<DmnPartyPackDetails>();

                DmnSearch search = new DmnSearch();
                search.Select = "PartyPackDetailsId, Item, Quantity, MenuCategoryId";
                search.Table = "tblPartyPackDetails";
                search.WhereNdGroupBy = "WHERE PartyPackId = " + packId + " AND Status = 1 and PackItemType =" + Convert.ToInt32(PackItemType.FoodItem);

                LoadMenuCategory();
                packDetailsList = bll.GetSearchData("prdSearch", search);
                int sl = 1;
                foreach (var item in packDetailsList)
                {
                    int rowIndex = dgv_packs_items.Rows.Add();
                    var row = dgv_packs_items.Rows[rowIndex];

                    row.Cells[showonlyslno].Value = sl++;
                    row.Cells[showonlyitemno].Value = item.Item;

                    if (item.MenuCategoryId != null && item.MenuCategoryId > 0)
                    {
                        row.Cells[showonlycategmenuno].Value = Convert.ToInt64(item.MenuCategoryId);
                    }
                    else
                    {
                        row.Cells[showonlycategmenuno].Value = DBNull.Value; 
                    }
                    row.Cells[showonlyqtyno].Value = item.Quantity;
                }

                 LoadUsedCategories();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void LoadUsedCategories()
        {
            try
            {
                var usedCategoryIds = packDetailsList
                    .Select(x => x.MenuCategoryId)
                    .Distinct()
                    .ToList();

                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = "Fcid, CategoryName";
                objSearch.Table = "tblCategory_FTI";
                objSearch.WhereNdGroupBy =
                    "WHERE Status = 1 AND Fcid IN (" + string.Join(",", usedCategoryIds) + ")";
                objSearch.OrderBy = "ORDER BY CategoryName";

                var categoryList = bllSearch.GetSearchData("prdSearch", objSearch);

                foreach (var cat in categoryList)
                {
                    Button btn = new Button();
                    btn.Text = cat.CategoryName;
                    btn.Width = 120;
                    btn.Height = 35;
                    btn.Tag = cat.Fcid;
                    btn.BackColor = Color.LightGray;
                    btn.FlatStyle = FlatStyle.Flat;

                    btn.Click += CategoryFilter_Click;

                    flp_pakdet_item_menu.Controls.Add(btn);
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void CategoryFilter_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn == null) return;

                int categoryId = Convert.ToInt32(btn.Tag);

                dgv_packs_items.Rows.Clear();

                var filtered = packDetailsList
                    .Where(x => x.MenuCategoryId == categoryId)
                    .ToList();

                int sl = 1;

                LoadMenuCategory();

                foreach (var item in filtered)
                {
                    int rowIndex = dgv_packs_items.Rows.Add();
                    var row = dgv_packs_items.Rows[rowIndex];

                    row.Cells[showonlyslno].Value = sl++;
                    row.Cells[showonlyitemno].Value = item.Item;
                    row.Cells[showonlycategmenuno].Value = Convert.ToInt64(item.MenuCategoryId);
                    row.Cells[showonlyqtyno].Value = item.Quantity;
                }
            }
            catch(Exception ex)
            {
            }
        }
        public class RoundedPanel : Panel
        {
            public int CornerRadius { get; set; } = 15;

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                int r = CornerRadius;

                path.AddArc(0, 0, r, r, 180, 90);
                path.AddArc(this.Width - r, 0, r, r, 270, 90);
                path.AddArc(this.Width - r, this.Height - r, r, r, 0, 90);
                path.AddArc(0, this.Height - r, r, r, 90, 90);
                path.CloseAllFigures();

                this.Region = new Region(path);
            }
        }
        private void PackButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;

                if (btn != null && btn.Tag != null)
                {
                    int packId = Convert.ToInt32(btn.Tag);

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void FrmPartyBooking_Resize(object sender, EventArgs e)
        {

            if (pnl_foodpacks.Visible)
            {
                pnl_foodpacks.Width = (int)(this.ClientSize.Width * 0.8);
                pnl_foodpacks.Height = (int)(this.ClientSize.Height * 0.8);

                pnl_foodpacks.Left = (this.ClientSize.Width - pnl_foodpacks.Width) / 2;
                pnl_foodpacks.Top = (this.ClientSize.Height - pnl_foodpacks.Height) / 2;
            }

            UIHelper.SetRoundCorner(tlp_textboxes, 15);
            UIHelper.SetRoundCorner(tlp_reg_contrl, 15);
            UIHelper.SetRoundCorner(tlp_address, 15);
            UIHelper.SetRoundCorner(tlp_navbar, 15);
            UIHelper.SetRoundCorner(tlpTop, 15);
            UIHelper.SetRoundCorner(tlp_dis_remarks, 15);
            UIHelper.SetRoundCorner(tlp_function_btn, 15);
            
        }      
        private void LoadEventItems()
        {
            try
            {
                BllSearch<DmnEventCommonItems> bllSearch =new BllSearch<DmnEventCommonItems>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "EventItemId, ItemName";
                objSearch.Table = "tblEventCommonItems";
                objSearch.WhereNdGroupBy ="WHERE Status = 1 AND EventCategoryType = 1 AND BranchId = "+ FrmDashboard.BranchId;
                objSearch.OrderBy = "ORDER BY ItemName";
                var eventList = bllSearch.GetSearchData("prdSearch", objSearch);
                cmbevent.DataSource = eventList;
                cmbevent.DisplayMember = "ItemName";
                cmbevent.ValueMember = "EventItemId";
                cmbevent.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void btncls_eventreg_Click(object sender, EventArgs e)
        {
            pnl_eventreg.Visible = false;
        }
        private void btneventreg_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_eventreg.Width = this.Width * 40 / 100;
                pnl_eventreg.Height = this.Height * 25 / 100;

                pnl_eventreg.Left = (this.Width - pnl_eventreg.Width) / 2;
                pnl_eventreg.Top = (this.Height - pnl_eventreg.Height) / 2;

                pnl_eventreg.Visible = true;
                pnl_eventreg.BringToFront();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private bool ValidateEvent()
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text))
            {
                CMessageBox.Show("Enter Item Name");
                txtItemName.Focus();
                return false;
            }

            //if (string.IsNullOrWhiteSpace(txtDescription.Text))
            //{
            //    CMessageBox.Show("Enter Description");
            //    txtDescription.Focus();
            //    return false;
            //}

            return true;
        }
        private void btn_del_event_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_del_event.Tag == null)
                {
                    CMessageBox.Show("Please search and select item first");
                    return;
                }

                DialogResult result = CMessageBox.Show(
                    "Are you sure you want to delete?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                DmnEventCommonItems dmn = new DmnEventCommonItems();
                dmn.EventItemId = Convert.ToInt32(btn_del_event.Tag);
                dmn.CreatedBy = FrmDashboard.UserId;

                BllEventCommonItems bll = new BllEventCommonItems();
                bll.DeleteEventItem("prdDelete_EventCommonItems", dmn);

                CMessageBox.Show("Deleted Successfully");

                txtItemName.Clear();
                txtDescription.Clear();
                btn_del_event.Visible = false;
                btn_del_event.Tag = null;

                LoadEventItems();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void btnserach_event_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                int returnValue = 0;

                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();

                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "ID,Item Name,Type";
                _objSearch.Select = "EventItemId as Id, ItemName as Field1, EventCategoryType as Field2";
                _objSearch.Table = "tblEventCommonItems";
                _objSearch.WhereNdGroupBy ="WHERE Status = 1 AND BranchId = " + FrmDashboard.BranchId;
                _objSearch.OrderBy = "ORDER BY ItemName";
                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records Found");
                    return;
                }

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
                    FillEventData(returnValue);
                    btn_del_event.Visible = true;
                    btn_del_event.Tag = returnValue; 
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void FillEventData(int id)
        {
            try
            {
                BllSearch<DmnEventCommonItems> bll = new BllSearch<DmnEventCommonItems>();

                DmnSearch search = new DmnSearch();

                search.Select = "EventItemId, ItemName, Description, EventCategoryType";
                search.Table = "tblEventCommonItems";
                search.WhereNdGroupBy = "WHERE EventItemId = " + id;

                var data = bll.GetSearchData("prdSearch", search);

                if (data.Count > 0)
                {
                    var item = data[0];
                    txtItemName.Text = item.ItemName;
                    txtDescription.Text = item.Description;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void btn_pak_details_cls_Click(object sender, EventArgs e)
        {
            pnl_pak_detailsitems.Visible = false;
        }
        private void btn_enter_paks_cls_Click(object sender, EventArgs e)
        {
            pnl_enter_paks.Visible = false;
        }
        private void btnaddpack_main_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_product.Rows.Clear();

                int sl = 1;
                foreach (DataGridViewRow sourceRow in dgv_packenter.Rows)
                {
                    if (sourceRow.IsNewRow) continue;

                    if (sourceRow.Cells[selecteditemitemno].Value == null)
                        continue;

                    int rowIndex = dgv_product.Rows.Add();
                    DataGridViewRow targetRow = dgv_product.Rows[rowIndex];

                    targetRow.Cells[pslno].Value = sl++;
                    targetRow.Cells[pitemno].Value = sourceRow.Cells[selecteditemitemno].Value;

                    if (sourceRow.Cells[selecteditemcategmenuno].Value != null)
                    {
                        string categoryName = GetCategoryName(Convert.ToInt32(sourceRow.Cells[selecteditemcategmenuno].Value));
                        targetRow.Cells[pcategmenuno].Value = categoryName;
                    }
                    else
                    {
                        targetRow.Cells[pcategmenuno].Value = "None";
                    }

                    targetRow.Cells[pqtyno].Value = sourceRow.Cells[selecteditemqtyno].Value;
                    targetRow.Cells[proPartyPackDetailsId].Value = sourceRow.Cells[selecteditemppcdiidno].Value;
                    targetRow.Cells[pPartydeafult].Value = sourceRow.Cells[selecteditemdeafultno].Value;

                    DataGridViewImageCell cellcolumntype =(DataGridViewImageCell)dgv_product.Rows[rowIndex].Cells[pactionno];

                    targetRow.DefaultCellStyle.BackColor = Color.Gainsboro;
                    targetRow.DefaultCellStyle.ForeColor = Color.Black;

                    targetRow.ReadOnly = true;

                    cellcolumntype.Value = Blankimage;
                    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }

                pnl_foodpacks.Visible = false;
                pnl_enter_paks.Visible = false;
                pnl_CustomItems_menu.Visible = false;

                dgv_packenter.Rows.Clear();
                selectedCustomItems.Clear();

                PriceChangecalculation();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void AllowDecimalInput(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt == null) return;


            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; 
            }
            if (e.KeyChar == '.' && txt.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        #region PACK ENTRY PANEL + DGV AUTO ROW

        private void dgv_packenter_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgv_packenter.Rows[e.RowIndex];

                if (row.Cells[selecteditemqtyno].Value == null ||
                    row.Cells[selecteditemqtyno].Value.ToString().Trim() == "")
                {
                    row.Cells[selecteditemqtyno].Value = 1;
                }

                if (e.RowIndex == dgv_packenter.Rows.Count - 1)
                {
                    bool hasValue = false;

                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (i == selecteditemslno)
                            continue;

                        if (row.Cells[i].Value != null &&
                            row.Cells[i].Value.ToString().Trim() != "")
                        {
                            hasValue = true;
                            break;
                        }
                    }

                    if (hasValue)
                    {
                        AddNewRow();
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void OpenPackEntryPanel()
        {
            try
            {
                pnl_enter_paks.Visible = true;
                pnl_enter_paks.BringToFront();

                pnl_enter_paks.Width = (int)(this.Width * 0.6);
                pnl_enter_paks.Height = (int)(this.Height * 0.7);

                pnl_enter_paks.Left = (this.Width - pnl_enter_paks.Width) / 2;
                pnl_enter_paks.Top = (this.Height - pnl_enter_paks.Height) / 2;

                LoadMenuCategory_packenter();

                if (dgv_packenter.Rows.Count == 0)
                {
                    AddNewRow();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void AddNewRow()
        {
            int rowIndex = dgv_packenter.Rows.Add();
            dgv_packenter.Rows[rowIndex].Cells[customitemslno].Value = rowIndex + 1;
        }
        private void LoadMenuCategory_packenter()
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

                DataGridViewComboBoxColumn cmb = (DataGridViewComboBoxColumn)dgv_packenter.Columns[selecteditemcategmenuno];

                cmb.DataSource = categoryList;
                cmb.DisplayMember = "CategoryName";
                cmb.ValueMember = "Fcid";

            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Save Menu 
        private void btn_savemenucateg_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateEvent())
                    return;

                DmnEventCommonItems dmn = new DmnEventCommonItems();
                BllEventCommonItems bll = new BllEventCommonItems();

                dmn.EventItemId = 0;
                dmn.EventCategoryType = Convert.ToInt32(EventCategoryType.EventTypes);
                dmn.ItemName = txtItemName.Text.Trim();
                dmn.Quantity = 0;
                dmn.Price = 0;
                dmn.TaxSettingsId = 0;
                dmn.Tax = "";
                dmn.TaxType = "";
                dmn.DiscountPer = 0;
                dmn.DiscountAmt = 0;
                dmn.TotalAmount = 0;
                dmn.Description = txtDescription.Text.Trim();

                dmn.BranchId = FrmDashboard.BranchId;
                dmn.CreatedBy = FrmDashboard.UserId;
                dmn.Status = 1;

                bll.AddeditData("prdInsert_upd_EventCommonItems", dmn);

                CMessageBox.Show("Saved Successfully");

                LoadEventItems();

                txtItemName.Clear();
                txtDescription.Clear();

                pnl_eventreg.Visible = false;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Custom Menu
        private void btn_addCusItems_Click(object sender, EventArgs e)
        {
            try
            {
                selectedCustomItems.Clear();
                pnl_CustomItems_menu.Visible = true;
                pnl_CustomItems_menu.BringToFront();

                pnl_CustomItems_menu.Width = (int)(this.Width * 0.6);
                pnl_CustomItems_menu.Height = (int)(this.Height * 0.7);

                pnl_CustomItems_menu.Left = (this.Width - pnl_CustomItems_menu.Width) / 2;
                pnl_CustomItems_menu.Top = (this.Height - pnl_CustomItems_menu.Height) / 2;

                LoadCustomMenu(CurrentPackId);
            }
            catch (Exception ex)
            {
            }
        }

        private void btn_cus_menu_cls_Click(object sender, EventArgs e)
        {
            pnl_CustomItems_menu.Visible = false;
        }

        private void btn_cus_menu_submit_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_CustomItems_menu.Visible = false;
                int sl = dgv_packenter.Rows.Count;

                foreach (var item in selectedCustomItems)
                {
                    bool alreadyExists = false;

                    foreach (DataGridViewRow dgRow in dgv_packenter.Rows)
                    {
                        if (dgRow.IsNewRow) continue;

                        var itemName = dgRow.Cells[selecteditemitemno].Value?.ToString();
                        var categoryId = dgRow.Cells[selecteditemcategmenuno].Value?.ToString();

                        if (itemName == item.ItemName &&
                            categoryId == item.CategoryId.ToString())
                        {
                            alreadyExists = true;
                            break;
                        }
                    }

                    if (alreadyExists) continue;

                    DataGridViewRow targetRow = null;

                    if (dgv_packenter.Rows.Count > 0)
                    {
                        DataGridViewRow lastRow = dgv_packenter.Rows[dgv_packenter.Rows.Count - 1];

                        bool isEmpty = true;

                        for (int i = 0; i < lastRow.Cells.Count; i++)
                        {
                            if (i == selecteditemslno)
                                continue;

                            if (lastRow.Cells[i].Value != null &&
                                lastRow.Cells[i].Value.ToString().Trim() != "")
                            {
                                isEmpty = false;
                                break;
                            }
                        }

                        if (isEmpty)
                        {
                            targetRow = lastRow;
                        }
                    }

                    if (targetRow == null)
                    {
                        int rowIndex = dgv_packenter.Rows.Add();
                        targetRow = dgv_packenter.Rows[rowIndex];
                        sl++;
                    }

                    targetRow.Cells[selecteditemslno].Value = sl;
                    targetRow.Cells[selecteditemitemno].Value = item.ItemName;
                    targetRow.Cells[selecteditemcategmenuno].Value = Convert.ToInt64(item.CategoryId);
                    targetRow.Cells[selecteditemqtyno].Value = item.Qty;
                    targetRow.Cells[selecteditemppcdiidno].Value = item.PartyPackCustomDetailsId;
                    targetRow.Cells[selecteditemdeafultno].Value = 1;

                }

                if (dgv_packenter.Rows.Count > 0)
                {
                    DataGridViewRow lastRow = dgv_packenter.Rows[dgv_packenter.Rows.Count - 1];

                    bool hasValue = false;

                    for (int i = 0; i < lastRow.Cells.Count; i++)
                    {
                        if (i == selecteditemslno)
                            continue;

                        if (lastRow.Cells[i].Value != null &&
                            lastRow.Cells[i].Value.ToString().Trim() != "")
                        {
                            hasValue = true;
                            break;
                        }
                    }

                    if (hasValue)
                    {
                        int newRowIndex = dgv_packenter.Rows.Add();
                        dgv_packenter.Rows[newRowIndex].Cells[selecteditemslno].Value =
                            dgv_packenter.Rows.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }



        private void LoadCustomMenu(int packId)
        {
            try
            {
                dgv_cus_menu_items.Rows.Clear();
                flp_cus_menuitems.Controls.Clear();

                BllSearch<DmnPartyPackCustom> bll = new BllSearch<DmnPartyPackCustom>();

                DmnSearch search = new DmnSearch();
                search.Select = "PartyPackCustomDetailsId, Product, qty, MenuCategoryId";
                search.Table = "tblPartyPackCustomMenu";
                search.WhereNdGroupBy = "WHERE Status = 1";

                customMenuList = bll.GetSearchData("prdSearch", search);

                LoadCustomUsedCategories();
                BindCustomGrid(customMenuList);
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private void BindCustomGrid(List<DmnPartyPackCustom> list)
        {
            try
            {
                dgv_cus_menu_items.Rows.Clear();
                int sl = 1;
                LoadMenuCategory_cutom();

                foreach (var item in list)
                {

                    int rowIndex = dgv_cus_menu_items.Rows.Add();
                    var row = dgv_cus_menu_items.Rows[rowIndex];

                    row.Cells[customitemslno].Value = sl++;
                    row.Cells[customitemno].Value = item.Product;
                    row.Cells[customitemcategmenuno].Value = Convert.ToInt64(item.MenuCategoryId);
                    row.Cells[customitemqtyno].Value = item.Qty;
                    row.Cells[customitemppcdiidno].Value = item.PartyPackCustomDetailsId;

                    bool isChecked = selectedCustomItems
                        .Any(x => x.PartyPackCustomDetailsId == item.PartyPackCustomDetailsId);
                    row.Cells[customitemselectNo].Value = isChecked;

                    row.Tag = item;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadCustomUsedCategories()
        {
            try
            {
                flp_cus_menuitems.Controls.Clear();

                Button btnAll = new Button();
                btnAll.Text = "All";
                btnAll.Tag = 0;
                btnAll.Width = 120;
                btnAll.Height = 35;
                btnAll.BackColor = Color.LightBlue;
                btnAll.FlatStyle = FlatStyle.Flat;

                btnAll.Click += (s, e) =>
                {
                    BindCustomGrid(customMenuList);
                };

                flp_cus_menuitems.Controls.Add(btnAll);

                var usedCategoryIds = customMenuList
                    .Select(x => x.MenuCategoryId)
                    .Distinct()
                    .ToList();

                foreach (var catId in usedCategoryIds)
                {
                    Button btn = new Button();
                    btn.Text = categoryList
                        .FirstOrDefault(x => x.Fcid == catId)?.CategoryName ?? "Unknown";
                    btn.Tag = catId;
                    btn.Width = 120;
                    btn.Height = 35;
                    btn.BackColor = Color.LightGray;
                    btn.FlatStyle = FlatStyle.Flat;

                    btn.Click += (s, e) =>
                    {
                        int cid = Convert.ToInt32(((Button)s).Tag);

                        var filtered = customMenuList
                            .Where(x => x.MenuCategoryId == cid)
                            .ToList();

                        BindCustomGrid(filtered);
                    };

                    flp_cus_menuitems.Controls.Add(btn);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void dgv_cus_menu_items_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == customitemselectNo)
                {
                    var row = dgv_cus_menu_items.Rows[e.RowIndex];

                    if (row.Cells[customitemppcdiidno].Value == null)
                        return;

                    int id = Convert.ToInt32(row.Cells[customitemppcdiidno].Value);

                    string itemName = Convert.ToString(row.Cells[customitemno].Value ?? "");

                    int catId = row.Cells[customitemcategmenuno].Value != null
                        ? Convert.ToInt32(row.Cells[customitemcategmenuno].Value)
                        : 0;

                    decimal qty = row.Cells[customitemqtyno].Value != null
                        ? Convert.ToDecimal(row.Cells[customitemqtyno].Value)
                        : 0;

                    bool isChecked = row.Cells[customitemselectNo].Value != null && (bool)row.Cells[customitemselectNo].Value;

                    if (isChecked)
                    {
                        if (!selectedCustomItems.Any(x => x.PartyPackCustomDetailsId == id))
                        {
                            selectedCustomItems.Add(new SelectedCustomItem
                            {
                                PartyPackCustomDetailsId = id,
                                ItemName = itemName,
                                CategoryId = catId,
                                Qty = qty
                            });
                        }
                    }
                    else
                    {
                        selectedCustomItems.RemoveAll(x => x.PartyPackCustomDetailsId == id);
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show("Error while selecting item.\n" + ex.Message);
            }
        }

        private void dgv_cus_menu_items_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_cus_menu_items.IsCurrentCellDirty)
                dgv_cus_menu_items.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        #endregion

        #region Card 

        public void FillForms()
        {
            try
            {
                foreach (Control ctrl in flw_categ.Controls)
                {
                    ctrl.Dispose();
                }
                flw_categ.Controls.Clear();
                int top = 5;
                decimal s = 0;
                int left = 0;

                BllSearch<DmnGridFields> bllacctype = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = " TOP 8 g.Caption as Field1,g.GTypeSubId as Field2";
                dmnSearch.Table = " tblGeneralOptions g ";
                dmnSearch.WhereNdGroupBy = " where g.GTypeId=58 and g.Status=0";// and GTypeSubId in(1,3,5,7)";
                dmnSearch.OrderBy = "order by GTypeSubId";
                var gridesource = bllacctype.GetSearchData("prdSearch", dmnSearch);
                if (gridesource.Count > 0)
                {
                    int columns = 4;
                    int spacing = 1;

                    for (int i = 0; i < gridesource.Count; i++)
                    {
                        s = Convert.ToDecimal(i) / 8;
                        Button dynamicButton = new Button();
                        dynamicButton.Width = flw_categ.Width/5;
                        dynamicButton.Height = (int)(flw_categ.Height * 0.8m);

                        if (i > 3)
                            dynamicButton.Visible = false;
                        else
                            dynamicButton.Visible = true;

                        if (!string.IsNullOrEmpty(Categorybg))
                        {
                            dynamicButton.BackgroundImage = LoadButtonImage(Categorybg, null);
                        }
                        else
                        {
                            if (gridesource[i].Field3 != null && gridesource[i].Field3 != "")
                            {
                                dynamicButton.BackgroundImage = LoadButtonImage(gridesource[i].Field3, null);
                            }
                            else
                                dynamicButton.BackgroundImage = LoadButtonImage(null, Properties.Resources.cancel_32);
                        }

                        dynamicButton.BackgroundImageLayout = ImageLayout.Stretch;
                        dynamicButton.FlatStyle = FlatStyle.Flat;
                        dynamicButton.FlatAppearance.BorderSize = 0;
                        dynamicButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
                        dynamicButton.ForeColor = System.Drawing.Color.White;

                        if (gridesource[i].Field1 == "GeneralMerchandise")
                        {
                            dynamicButton.Text = "General Merchandise";
                        }
                        else if (gridesource[i].Field1 == "BowlingAccessories")
                        {
                            dynamicButton.Text = "Bowling Accessories";
                        }
                        else
                            dynamicButton.Text = gridesource[i].Field1;


                        dynamicButton.Tag = gridesource[i].Field2;
                        dynamicButton.Name = "DynamicButton" + i;

                        if (CategoryFontstatus == 1 && !string.IsNullOrEmpty(CategoryFontdata))
                        {
                            #region saved font
                            try
                            {
                                string[] fontParts = CategoryFontdata.Split(',');
                                if (fontParts.Length == 3)
                                {
                                    string fontName = fontParts[0].Trim();
                                    float fontSize = float.Parse(fontParts[1].Trim());
                                    string fontStyleStr = fontParts[2].Trim().ToLower();
                                    FontStyle style = fontStyleStr == "bold" ? FontStyle.Bold : FontStyle.Regular;

                                    dynamicButton.Font = new Font(fontName, fontSize, style);
                                }
                                else
                                {
                                    dynamicButton.Font = new Font("Georgia", 14); // fallback if config format is wrong
                                }
                            }
                            catch
                            {
                                dynamicButton.Font = new Font("Georgia", 14); // fallback on exception
                            }
                            #endregion
                        }
                        else
                        {
                            #region Defult Font 
                            if (string.IsNullOrEmpty(gridesource[i].Field4))
                            {
                                dynamicButton.Font = new Font("Georgia", 14);
                            }
                            else
                            {
                                try
                                {
                                    string[] textSplit = gridesource[i].Field4.Split(',');
                                    if (textSplit.Length >= 2)
                                    {
                                        string fontName = string.Join(",", textSplit.Take(textSplit.Length - 1)).Trim();
                                        float fontSize = float.Parse(textSplit.Last().Trim());

                                        dynamicButton.Font = new Font(fontName, fontSize);
                                    }
                                    else
                                    {
                                        dynamicButton.Font = new Font("Georgia", 14); // fallback
                                    }
                                }
                                catch
                                {
                                    dynamicButton.Font = new Font("Georgia", 14); // fallback on error
                                }
                            }
                            #endregion
                        }


                        //dynamicButton.MouseDown += DynamicButton_MouseDown;
                        //dynamicButton.MouseUp += DynamicButton_MouseUp;
                        dynamicButton.Click += new EventHandler(DynamicButton_Click);

                        flw_categ.Controls.Add(dynamicButton);

                    }
                }
            }
            catch (Exception ex)
            {
               // clsGeneral.LogException("POSDashboard page  FillForms()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.POSDashboard));
            }
        }
        private void DynamicButton_Click(object sender, EventArgs e)
        {
            try
            {
                Memoryrelease();
                int n = 5;
                decimal s = 0;
                int r = 0, c = 0;

                foreach (Control ctrl in pnlitembuttons.Controls)
                {
                    ctrl.Dispose();
                }
                pnlitembuttons.Controls.Clear();

                int tag = Convert.ToInt32((sender as Button).Tag);
                BllSearch<DmnGridFields> _bllSearch = new BllSearch<DmnGridFields>();
                string table = "";
                DmnSearch dmnSearch = new DmnSearch();
                int tabtype = 0;
                int gsubid = 0;
                string heading = "";
                string productcolumnname = "";

                int locationId = FrmLogin.GameBranchId; 
                switch ((PosSalProduct)tag)
                {
                    case PosSalProduct.Card:
                        table = "tblCardProducts";
                        tabtype = (int)ProductType.Card;
                        heading = "Card Products";
                        productcolumnname = "p.Product_id";
                        break;

                    case PosSalProduct.GateAccess:
                        table = "tblGatePass";
                        tabtype = (int)ProductType.GateAccess;
                        heading = "Gate Pass";
                        productcolumnname = "p.Productid";
                        break;

                    case PosSalProduct.Trampoline:
                        table = "tblTrampolining";
                        tabtype = (int)ProductType.Trampoline;
                        heading = "Trampolining";
                        productcolumnname = "p.Productid";
                        break;

                    case PosSalProduct.TimePlay:
                        table = "tblTimePlayProducts";
                        tabtype = (int)ProductType.TimePlay;
                        heading = "TimePlay Products";
                        productcolumnname = "p.ProductId";
                        break;

                    default:
                        throw new ArgumentException("Invalid product tag");
                }

             
                txtTable.Text = table;
                lblProductHeading.Text = heading;

             
                dmnSearch.Select = $@"
                                    TOP 50
                                    {productcolumnname} as Field1,
                                    p.Description as Field2,
                                    0 as Field3,
                                    0 as Field4";
                                                
                dmnSearch.Table = $@"
                                    [{GameDB}].dbo.{table} p
                                    LEFT JOIN [{GameDB}].dbo.tblCardProductLocMapping pm 
                                        ON pm.Product = {productcolumnname} AND pm.ProductType = {tabtype}";
                                              
                dmnSearch.WhereNdGroupBy = $@"
                                    WHERE pm.Location = {locationId} 
                                    AND pm.Status = 1 and p.Status=1
                                    AND pm.ProductType = {tabtype}";

                dmnSearch.OrderBy = "ORDER BY p.Description";

                var gridesource = _bllSearch.GetSearchData("prdSearch", dmnSearch);
                    if (gridesource.Count > 0)
                    {
                        if (tag == Convert.ToInt32(PosSalProduct.Card))
                        {
                            // REMOVE DUPLICATES BY SubCategoryId (Field5)
                            var filteredSource = gridesource
                               .GroupBy(x =>
                                   x.Field6 == null || Convert.ToInt32(x.Field6) == 0
                                       ? Guid.NewGuid().ToString()
                                       : x.Field5.ToString())
                               .Select(g => g.First())
                               .ToList();

                            if (filteredSource.Count > 0)
                            {
                                txtTable.Text = table;

                                TableLayoutPanel tlpProduct = new TableLayoutPanel();
                                tlpProduct.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                                tlpProduct.Size = pnlitembuttons.Size;
                                tlpProduct.AutoScroll = true;
                                tlpProduct.Name = "tlp_" + table.Trim();
                                tlpProduct.AutoScrollMargin = new Size(10, 10);
                                tlpProduct.ColumnCount = (screnwidth > 1365 && screnheight > 765) ? 6 : 5;

                                for (int i = 0; i < filteredSource.Count; i++)
                                {
                                    var item = filteredSource[i];

                                    Button dynamicButton = new Button();
                                    dynamicButton.Height = 70;
                                    dynamicButton.Width = (pnlitembuttons.Width - 43) / tlpProduct.ColumnCount;
                                    dynamicButton.Dock = DockStyle.None;

                                    if (Convert.ToInt32(item.Field5) > 0)
                                    {
                                        dynamicButton.Text = item.Field6;
                                        dynamicButton.Tag = item.Field5;
                                        dynamicButton.AccessibleDescription = "1";
                                    }
                                    else
                                    {
                                        dynamicButton.Text = item.Field2;
                                        dynamicButton.Tag = item.Field1;
                                        dynamicButton.AccessibleDescription = "0";
                                    }


                                    dynamicButton.Name = "DynamicButton" + i;
                                    dynamicButton.FlatStyle = FlatStyle.Flat;
                                    dynamicButton.ForeColor = Color.White;

                                    // ENABLE / DISABLE LOGIC
                                    if (tabtype == (int)ProductType.FB ||
                                        tabtype == (int)ProductType.Merchandise ||
                                        tabtype == (int)ProductType.Bowling)
                                    {
                                        if (item.Field5 == null || Convert.ToInt32(item.Field5) <= 0)
                                        {
                                            dynamicButton.Enabled = false;
                                            dynamicButton.BackgroundImage = LoadButtonImage("inactive", null);
                                        }
                                        else
                                        {
                                            dynamicButton.BackgroundImage =
                                                string.IsNullOrEmpty(item.Field3)
                                                    ? LoadButtonImage(null, Properties.Resources.Untitled_2_2030)
                                                    : LoadButtonImage(item.Field3, null);
                                        }
                                    }
                                    else
                                    {
                                        dynamicButton.BackgroundImage = LoadButtonImage(null, Properties.Resources.Untitled_2_2030);
                                    }

                                    dynamicButton.BackgroundImageLayout = ImageLayout.Stretch;

                             
                                if (string.IsNullOrEmpty(item.Field4) || Convert.ToInt32(item.Field4) == 0)
                                {
                                        dynamicButton.Font = (screnwidth > 1365 && screnheight > 765)
                                            ? new Font("Georgia", 14)
                                            : new Font("Georgia", 10);
                                    }
                                    else
                                    {
                                        string[] textSplit = item.Field4.Split(',');
                                        string fontName = textSplit[0];
                                        float fontSize = float.Parse(textSplit[textSplit.Length - 1]);

                                        dynamicButton.Font = new Font(fontName, fontSize);
                                    }

                                    // CONTEXT MENU
                                    ContextMenuStrip contextMenu = new ContextMenuStrip();
                                    ToolStripMenuItem item1 = new ToolStripMenuItem("BG Image");
                                    ToolStripMenuItem item2 = new ToolStripMenuItem("Font Style");

                                    item1.Image = Properties.Resources.background_mouse_clcik;
                                    item2.Image = Properties.Resources.font_mouselick;
                                    item1.Font = item2.Font = new Font("Arial", 12, FontStyle.Bold);
                                    item1.ForeColor = item2.ForeColor = Color.Black;

                                    //item1.Click += Item1_Click;
                                    //item2.Click += Item2_Click;

                                    contextMenu.Items.Add(item1);
                                    contextMenu.Items.Add(item2);
                                    dynamicButton.ContextMenuStrip = contextMenu;

                                    dynamicButton.Click += DynamicButtonProduct_Click;

                                    // GRID POSITIONING
                                    tlpProduct.Controls.Add(dynamicButton, c, r);
                                    c++;

                                    if (c >= tlpProduct.ColumnCount)
                                    {
                                        c = 0;
                                        r++;
                                    }
                                }

                                pnlitembuttons.Controls.Add(tlpProduct);
                            }
                        }
                        else
                        {
                            txtTable.Text = table;
                            TableLayoutPanel tlpProduct = new TableLayoutPanel();
                            tlpProduct.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                            tlpProduct.Size = pnlitembuttons.Size;
                            tlpProduct.AutoScroll = true;
                            tlpProduct.AutoScrollMargin = new System.Drawing.Size(10, 10);
                            //tlpProduct.Name = tlpProduct + table;
                            if (screnwidth > 1365 && screnheight > 765)
                            {
                                tlpProduct.ColumnCount = 6;
                            }
                            else
                            {
                                tlpProduct.ColumnCount = 5;
                            }
                            for (int i = 0; i < gridesource.Count; i++)
                            {
                                s = Convert.ToDecimal(i % 6);
                                Button dynamicButton = new Button();
                                dynamicButton.Height = 70;
                                if (screnwidth > 1365 && screnheight > 765)
                                {
                                    dynamicButton.Width = (pnlitembuttons.Width - 43) / tlpProduct.ColumnCount;
                                    dynamicButton.Dock = DockStyle.None;
                                }
                                else
                                {
                                    dynamicButton.Width = (pnlitembuttons.Width - 43) / tlpProduct.ColumnCount;//dynamicButton.Width = 156;
                                    dynamicButton.Dock = DockStyle.None;
                                }

                                if (tabtype == Convert.ToInt32(ProductType.FB) || tabtype == Convert.ToInt32(ProductType.Merchandise) || tabtype == Convert.ToInt32(ProductType.Bowling))
                                {
                                    if (Convert.ToInt32(gridesource[i].Field5) <= 0)
                                    {
                                        dynamicButton.Enabled = false;
                                        dynamicButton.BackgroundImage = new Bitmap(Application.StartupPath + "\\Background_Images\\inactive.png");
                                        dynamicButton.BackgroundImageLayout = ImageLayout.Stretch;
                                        dynamicButton.FlatStyle = FlatStyle.Flat;
                                        dynamicButton.ForeColor = System.Drawing.Color.White;
                                    }
                                    else
                                    {
                                        if (gridesource[i].Field3 == null || gridesource[i].Field3 == "")
                                        {
                                            dynamicButton.BackgroundImage = Properties.Resources.Untitled_2_2030;
                                        }
                                        else
                                        {
                                            dynamicButton.BackgroundImage = new Bitmap(Application.StartupPath + "\\Background_Images\\" + gridesource[i].Field3 + ".png");
                                        }
                                        dynamicButton.BackgroundImageLayout = ImageLayout.Stretch;
                                        dynamicButton.FlatStyle = FlatStyle.Flat;
                                        dynamicButton.ForeColor = System.Drawing.Color.White;
                                    }
                                }
                                else
                                {
                                    if (gridesource[i].Field3 == null || gridesource[i].Field3 == "")
                                    {
                                        dynamicButton.BackgroundImage = LoadButtonImage(null, Properties.Resources.Untitled_2_2030);
                                    }
                                    else
                                    {
                                        dynamicButton.BackgroundImage = LoadButtonImage(gridesource[i].Field3, Properties.Resources.Untitled_2_2030);
                                    }
                                    dynamicButton.BackgroundImageLayout = ImageLayout.Stretch;
                                    dynamicButton.FlatStyle = FlatStyle.Flat;
                                    dynamicButton.ForeColor = System.Drawing.Color.White;
                                }

                                if (s == 0 && i != 0)
                                {
                                    r = r + 1;
                                    c = 0;
                                }
                                dynamicButton.Text = gridesource[i].Field2;
                                dynamicButton.Tag = gridesource[i].Field1;
                                dynamicButton.Name = "DynamicButton_P" + i;
                                if ((gridesource[i].Field4 == null) || (gridesource[i].Field4 == "") || (Convert.ToInt32(gridesource[i].Field4) == 0))
                                {
                                    if (screnwidth > 1365 && screnheight > 765)
                                    {
                                        dynamicButton.Font = new Font("Georgia", 14);
                                    }
                                    else
                                    {
                                        dynamicButton.Font = new Font("Georgia", 10);
                                    }

                                }
                                else
                                {
                                    string strng = gridesource[i].Field4;
                                    string[] textSplit = strng.Split(',');
                                    int f = textSplit.Length - 2;
                                    string fontn = "";
                                    for (int t = 0; t < textSplit.Length - 1; t++)
                                    {
                                        fontn = fontn + textSplit[f];
                                    }
                                    float fontsz = float.Parse(textSplit[textSplit.Length - 1]);
                                    dynamicButton.Font = new Font(fontn, fontsz);
                                }

                                /* Button sub menu list */
                                ContextMenuStrip contextMenu = new ContextMenuStrip();
                                ToolStripMenuItem item1 = new ToolStripMenuItem("BG Image");
                                ToolStripMenuItem item2 = new ToolStripMenuItem("Font Style");
                                contextMenu.Items.Add(item1);
                                contextMenu.Items.Add(item2);

                                item1.Image = FoodandBeverage.Properties.Resources.background_mouse_clcik;
                                item2.Image = FoodandBeverage.Properties.Resources.font_mouselick;
                                item1.ForeColor = Color.Black;
                                item2.ForeColor = Color.Black;
                                item1.Font = new Font("Arial", 12, FontStyle.Bold);
                                item2.Font = new Font("Arial", 12, FontStyle.Bold);

                                dynamicButton.ContextMenuStrip = contextMenu;
                                //item1.Click += new EventHandler(Item1_Click);
                                //item2.Click += new EventHandler(Item2_Click);


                                dynamicButton.Click += new EventHandler(DynamicButtonProduct_Click);
                                tlpProduct.Controls.Add(dynamicButton, c, r);

                                c++;
                            }

                            pnlitembuttons.Controls.Add(tlpProduct);
                        }
                    }
                
            }
            catch (Exception ex)
            {
                //clsGeneral.LogException("POSDashboard page  DynamicButton_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.POSDashboard));
            }
        }
        private void DynamicButtonProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string table = string.IsNullOrEmpty(txtTable.Text) ? "tblCardProducts" : txtTable.Text;
                int tag = Convert.ToInt32((sender as Button).Tag);
                int subcatestatus = Convert.ToInt32((sender as Button).AccessibleDescription);

                BllSearch<DmnGridFields> _bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                int ptypeno = 0;

                // CARD PRODUCTS
                if (table == "tblCardProducts")
                {
                    ptypeno = Convert.ToInt32(ProductType.Card);


                    if (subcatestatus == 1)
                    {
                        pnlitembuttons.Controls.Clear();

                        dmnSearch.Select = @"
                    TOP 50 
                    p.Product_Id as Field1,
                    p.Description as Field2,
                    p.SubCategoryid as Field5,
                    sp.SubCategoryName as Field6,
                    pm.ProductType as Field7,
                    pm.Location as Field8";

                        dmnSearch.Table = $@"
                    [{GameDB}].dbo.tblCardProducts p
                    LEFT JOIN [{GameDB}].dbo.tblCardProductLocMapping pm
                        ON pm.Product = p.ProductId AND pm.ProductType = {ptypeno}
                    LEFT JOIN [{GameDB}].dbo.tblCardSubCategory sp
                        ON sp.Id = p.SubCategoryid AND sp.Status = 1";

                        dmnSearch.WhereNdGroupBy = $@"
                    WHERE p.Status = 1 
                        AND p.SubCategoryid = {tag} 
                        AND pm.Location = {FrmLogin.GameBranchId} 
                        AND pm.ProductType = {ptypeno}";

                        dmnSearch.OrderBy = "ORDER BY p.Description";

                        var gridesource = _bllSearch.GetSearchData("prdSearch", dmnSearch);

                        TableLayoutPanel tlpProduct = new TableLayoutPanel
                        {
                            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom,
                            Size = pnlitembuttons.Size,
                            AutoScroll = true,
                            Name = "tlp_" + table.Trim(),
                            AutoScrollMargin = new Size(10, 10),
                            ColumnCount = (screnwidth > 1365 && screnheight > 765) ? 6 : 5
                        };

                        int c = 0, r = 0;
                        foreach (var item in gridesource)
                        {
                            Button dynamicButton = new Button
                            {
                                Height = 70,
                                Width = (pnlitembuttons.Width - 43) / tlpProduct.ColumnCount,
                                Dock = DockStyle.None,
                                Text = item.Field2,
                                Tag = item.Field1,
                                AccessibleDescription = "0",
                                Name = "DynamicButton" + c,
                                FlatStyle = FlatStyle.Flat,
                                ForeColor = Color.White,
                                BackgroundImageLayout = ImageLayout.Stretch
                            };

                            dynamicButton.BackgroundImage =
                                string.IsNullOrEmpty(item.Field3)
                                ? LoadButtonImage(null, Properties.Resources.Untitled_2_2030)
                                : LoadButtonImage(item.Field3, null);

                            if (string.IsNullOrEmpty(item.Field4))
                                dynamicButton.Font = (screnwidth > 1365 && screnheight > 765) ? new Font("Georgia", 14) : new Font("Georgia", 10);
                            else
                            {
                                string[] textSplit = item.Field4.Split(',');
                                string fontName = textSplit[0];
                                float fontSize = float.Parse(textSplit[textSplit.Length - 1]);
                                dynamicButton.Font = new Font(fontName, fontSize);
                            }

                            dynamicButton.Click += DynamicButtonProduct_Click;

                            tlpProduct.Controls.Add(dynamicButton, c, r);
                            c++;
                            if (c >= tlpProduct.ColumnCount)
                            {
                                c = 0;
                                r++;
                            }
                        }

                        pnlitembuttons.Controls.Add(tlpProduct);

                        // Add Back button
                        Button btnBack = new Button
                        {
                            Name = "btnBack",
                            Text = "Back",
                            Height = 45,
                            Width = 120,
                            Font = new Font("Segoe UI", 12, FontStyle.Bold),
                            ForeColor = Color.White,
                            FlatStyle = FlatStyle.Flat,
                            BackColor = Color.Transparent,
                            UseVisualStyleBackColor = false,
                            BackgroundImage = LoadButtonImage(null, Properties.Resources.Untitled_2_2030),
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                            Location = new Point(10, pnlitembuttons.Height - 55)
                        };
                        btnBack.Click += BtnBack_Click;
                        pnlitembuttons.Controls.Add(btnBack);
                        btnBack.BringToFront();

                        return;
                    }
                    else
                    {
                        // Single product load for Card
                        dmnSearch.Select = $@"
                    p.Product_id as Id,
                    p.Description as Field1,
                    p.SuggestedPrice as Field2,
                    p.Tax as Field3,
                    p.TaxSettingsId as Field4";
                        dmnSearch.Table = $"[{GameDB}].dbo.{table} p";
                        dmnSearch.WhereNdGroupBy = $"WHERE p.Product_id = {tag}";
                    }
                }
                // TIME PLAY
                else if (table == "tblTimePlayProducts")
                {
                    ptypeno = Convert.ToInt32(ProductType.TimePlay);
                    dmnSearch.Select = $@"
                p.Productid as Id,
                p.Description as Field1,
                p.SuggestedPrice as Field2,
                p.Tax as Field3,
                p.TaxSettingsId as Field4,
                p.MinDuration as Field5,
                p.RoundsPerPlay as Field6,
                p.GameInterval as Field7";
                    dmnSearch.Table = $"[{GameDB}].dbo.{table} p";
                    dmnSearch.WhereNdGroupBy = $"WHERE p.Productid = {tag}";
                }
                // GATE PASS
                else if (table == "tblGatePass")
                {
                    ptypeno = Convert.ToInt32(ProductType.GateAccess);
                    dmnSearch.Select = $@"
                p.Productid as Id,
                p.Description as Field1,
                p.SuggestedPrice as Field2,
                p.Tax as Field3,
                p.TaxSettingsId as Field4,
                p.CashBalance as Field5,
                p.CashBonusBalance as Field6,
                p.TokenBalance as Field7,
                p.Points as Field8";
                    dmnSearch.Table = $"[{GameDB}].dbo.{table} p";
                    dmnSearch.WhereNdGroupBy = $"WHERE p.Productid = {tag}";
                }
                // TRAMPOLINE
                else if (table == "tblTrampolining")
                {
                    ptypeno = Convert.ToInt32(ProductType.Trampoline);
                    dmnSearch.Select = $@"
                p.Productid as Id,
                p.Description as Field1,
                p.SuggestedPrice as Field2,
                p.Tax as Field3,
                p.TaxSettingsId as Field4,
                p.CashBalance as Field5,
                p.CashBonusBalance as Field6,
                p.TokenBalance as Field7,
                p.Points as Field8";
                    dmnSearch.Table = $"[{GameDB}].dbo.{table} p";
                    dmnSearch.WhereNdGroupBy = $"WHERE p.Productid = {tag}";
                }

                var gridSource = _bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (gridSource != null && gridSource.Count > 0)
                {
                    foreach (var item in gridSource)
                    {
                        bool exists = dgv_product.Rows.Cast<DataGridViewRow>()
                            .Any(r => !r.IsNewRow
                                      && Convert.ToInt32(r.Cells[pProductid].Value ?? 0) == item.Id
                                      && Convert.ToInt32(r.Cells[PPackProductTypeno].Value ?? 0) == (int)PackItemType.Card);

                        if (exists)
                        {
                            CMessageBox.Show("This product is already added!", "Duplicate",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            continue;
                        }

                        int rowIndex = dgv_product.Rows.Add();
                        DataGridViewRow row = dgv_product.Rows[rowIndex];

                        row.Cells[pslno].Value = rowIndex + 1;
                        row.Cells[pProductid].Value = item.Id;
                        row.Cells[pitemno].Value = item.Field1;
                        row.Cells[pcategmenuno].Value = ((ProductType)ptypeno).ToString();
                        row.Cells[pqtyno].Value = 1;
                        row.Cells[pPrice].Value = Convert.ToDecimal(item.Field2);
                        row.Cells[pAmount].Value = Convert.ToDecimal(item.Field2);

                        row.Cells[pdiscper].Value = 0;
                        row.Cells[pdisamt].Value = 0;
                        row.Cells[ptax].Value = item.Field3 ?? "";
                        row.Cells[ptaxtype].Value = "";
                        row.Cells[ptaxper].Value = 0;
                        row.Cells[ptaxamt].Value = 0;
                        row.Cells[ppretaxamt].Value = Convert.ToDecimal(item.Field2);
                        row.Cells[pigstamt].Value = 0;
                        row.Cells[pvatamt].Value = 0;
                        row.Cells[pnetamt].Value = Convert.ToDecimal(item.Field2);
                        row.Cells[ppridno].Value = 0;
                        row.Cells[PPackProductTypeno].Value = (int)PackItemType.Card;
                        row.Cells[pproducttype].Value = ptypeno;
                        row.Cells[proPartyPackDetailsId].Value = 0;
                        row.Cells[pPartydeafult].Value = 0;
                        row.Cells[ptaxsettingid].Value = Convert.ToInt32(item.Field4);

                        DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)row.Cells[pactionno];
                        cellcolumntype.Value = Grid_Remove;
                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

                        CalculateAndApplyTax(rowIndex, (int)PackItemType.Card,Convert.ToInt32(item.Id));

                    }
                }

                pnl_cardpreview.Visible = false;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            FillForms();
        }
        private Image LoadButtonImage(string fileName, Image fallbackImage)
        {
            if (string.IsNullOrEmpty(fileName)) return fallbackImage;

            string imagePath = Path.Combine(Application.StartupPath, "Background_Images", fileName + ".png");
            if (!File.Exists(imagePath)) return fallbackImage;

            using (Image img = Image.FromFile(imagePath))
            {
                return new Bitmap(img);
            }

        }
        private void btncards_cls_Click(object sender, EventArgs e)
        {
            pnl_cardpreview.Visible = false;
        }
        private void btncard_Click(object sender, EventArgs e)
        {
            pnl_cardpreview.Visible = true;
            pnl_cardpreview.BringToFront();

            int widthPercent = 80;
            int heightPercent = 80;

            pnl_cardpreview.Width = this.ClientSize.Width * widthPercent / 100;
            pnl_cardpreview.Height = this.ClientSize.Height * heightPercent / 100;

            pnl_cardpreview.Left = (this.ClientSize.Width - pnl_cardpreview.Width) / 2;
            pnl_cardpreview.Top = (this.ClientSize.Height - pnl_cardpreview.Height) / 2;

            FillForms();
        }
        private void LoadProducts(int tag, string searchText = "")
        {
            try
            {
                Memoryrelease();

                pnlitembuttons.Controls.Clear();

                int r = 0, c = 0;

                BllSearch<DmnGridFields> _bllSearch = new BllSearch<DmnGridFields>();
                string table = "";
                DmnSearch dmnSearch = new DmnSearch();
                int tabtype = 0;
                string heading = "";
                string productcolumnname = "";

                int locationId = FrmLogin.GameBranchId;

                switch ((PosSalProduct)tag)
                {
                    case PosSalProduct.Card:
                        table = "tblCardProducts";
                        tabtype = (int)ProductType.Card;
                        heading = "Card Products";
                        productcolumnname = "p.Product_id";
                        break;

                    case PosSalProduct.GateAccess:
                        table = "tblGatePass";
                        tabtype = (int)ProductType.GateAccess;
                        heading = "Gate Pass";
                        productcolumnname = "p.Productid";
                        break;

                    case PosSalProduct.Trampoline:
                        table = "tblTrampolining";
                        tabtype = (int)ProductType.Trampoline;
                        heading = "Trampolining";
                        productcolumnname = "p.Productid";
                        break;

                    case PosSalProduct.TimePlay:
                        table = "tblTimePlayProducts";
                        tabtype = (int)ProductType.TimePlay;
                        heading = "TimePlay Products";
                        productcolumnname = "p.ProductId";
                        break;

                    default:
                        throw new ArgumentException("Invalid product tag");
                }

                txtTable.Text = table;
                lblProductHeading.Text = heading;

                string searchCondition = "";
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    searchCondition = $" AND p.Description LIKE '%{searchText.Replace("'", "''")}%'";
                }

                dmnSearch.Select = $@"
            TOP 100
            {productcolumnname} as Field1,
            p.Description as Field2,
            0 as Field3,
            0 as Field4";

                dmnSearch.Table = $@"
            [{GameDB}].dbo.{table} p
            LEFT JOIN [{GameDB}].dbo.tblCardProductLocMapping pm 
                ON pm.Product = {productcolumnname} 
                AND pm.ProductType = {tabtype}";

                dmnSearch.WhereNdGroupBy = $@"
            WHERE pm.Location = {locationId} 
            AND pm.Status = 1 
            AND p.Status = 1
            AND pm.ProductType = {tabtype}
            {searchCondition}";

                dmnSearch.OrderBy = "ORDER BY p.Description";

                var gridesource = _bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (gridesource.Count == 0)
                    return;

                TableLayoutPanel tlpProduct = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    ColumnCount = (screnwidth > 1365 && screnheight > 765) ? 6 : 5
                };

                foreach (var item in gridesource)
                {
                    if (c >= tlpProduct.ColumnCount)
                    {
                        c = 0;
                        r++;
                    }
                    Button btn = new Button
                    {
                        Height = 70,
                        Width = (pnlitembuttons.Width - 43) / tlpProduct.ColumnCount,
                        Text = item.Field2,
                        Tag = item.Field1,
                        FlatStyle = FlatStyle.Flat,
                        ForeColor = Color.White,
                        BackgroundImage = LoadButtonImage(null, Properties.Resources.Untitled_2_2030),
                        BackgroundImageLayout = ImageLayout.Stretch
                    };

                    btn.Click += DynamicButtonProduct_Click;

                    tlpProduct.Controls.Add(btn, c, r);
                    c++;
                }

                pnlitembuttons.Controls.Add(tlpProduct);
            }
            catch (Exception ex)
            {
               
            }
        }
        private void btncardserach_Click(object sender, EventArgs e)
        {

        }
        private void txtcardserach_TextChanged(object sender, EventArgs e)
        {
            int tag = 0;

            if (lblProductHeading.Text == "Card Products")
                tag = (int)PosSalProduct.Card;
            else if (lblProductHeading.Text == "Gate Pass")
                tag = (int)PosSalProduct.GateAccess;
            else if (lblProductHeading.Text == "Trampolining")
                tag = (int)PosSalProduct.Trampoline;
            else if (lblProductHeading.Text == "TimePlay Products")
                tag = (int)PosSalProduct.TimePlay;

            if (tag == 0)
            {
                CMessageBox.Show("Please select category first.",
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                txtcardserach.Clear();   // optional: clear search box
                return;
            }

            LoadProducts(tag, txtcardserach.Text.Trim());
        }

        #endregion

        #region Gift 
        private void btngiftprevcls_Click(object sender, EventArgs e)
        {
            pnlgiftpreview.Visible = false;
        }
        private void btngift_Click(object sender, EventArgs e)
        {
            pnlgiftpreview.Visible = true;
            pnlgiftpreview.BringToFront();
            pnlgiftpreview.Left = (this.ClientSize.Width - pnlgiftpreview.Width) / 2;
            pnlgiftpreview.Top = (this.ClientSize.Height - pnlgiftpreview.Height) / 2;
            Redeemproducts();
        }
        private void txtserachgift_TextChanged(object sender, EventArgs e)
        {
            Redeemproducts(txtserachgift.Text.Trim());
        }
        private void btn_searchgift_Click(object sender, EventArgs e)
        {
            Redeemproducts(txtserachgift.Text.Trim());
        }
        private void Redeemproducts(string searchText = "")
        {
            try
            {
                Memoryrelease();

                flp_itemredproducts.Controls.Clear();

                BllSearch<DmnGridFields> _bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                lblProductHeading.Text = "Gift Products";

                dmnSearch.Select = @"r.ProductId as Id, 
                            UPC As Field1, 
                            Description As Field2, 
                            SuggestedPointValue As Field3, 
                            (Select top(1) Pr.SRate 
                                from [" + GameDB + @"].dbo.tblProductRate pr 
                                where pr.ProductId = r.ProductId 
                                and pr.ProductType = " + (int)ProductType.RedemptionTicket +
                                           " and pr.BranchId=" + FrmLogin.GameBranchId + @" 
                                order by pr.Id desc) As Field4,

                            (Select Sum(QOH) 
                                from [" + GameDB + @"].dbo.tblProductRate pr 
                                where pr.ProductId = r.ProductId
                                and pr.ProductType = " + (int)ProductType.RedemptionTicket +
                                           " and pr.BranchId = " + FrmLogin.GameBranchId + @") As Field5, 

                            0 as Field6, 
                            0 as Field7";

                dmnSearch.Table = " [" + GameDB + @"].dbo.tblRedemptionProducts r " +
                                  " left join [" + GameDB + @"].dbo.tblCardProductLocMapping l " +
                                  " on l.Product=r.ProductId and l.Location= " + FrmLogin.GameBranchId +
                                  " and l.Status=1 and l.ProductType = " + (int)ProductType.RedemptionTicket;

                // 🔍 SEARCH FILTER ADDED HERE
                string searchFilter = "";
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    searchFilter = " AND r.Description LIKE '%" + searchText.Replace("'", "''") + "%'";
                }

                dmnSearch.WhereNdGroupBy = "Where r.Status=1  and l.Location = " + FrmLogin.GameBranchId +
                                          " and (Select Sum(QOH) from [" + GameDB + @"].dbo.tblProductRate pr " +
                                          " where pr.ProductId = r.ProductId " +
                                          " and pr.ProductType = " + (int)ProductType.RedemptionTicket +
                                          " and pr.BranchId=" + FrmLogin.GameBranchId + ") > 0"
                                          + searchFilter;

                var gridesource = _bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridesource.Count == 0)
                    return;

                foreach (var item in gridesource)
                {
                    Button btn = CreateProductButton(item);
                    btn.Margin = new Padding(5);
                    flp_itemredproducts.Controls.Add(btn);
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private Button CreateProductButton(DmnGridFields item)
        {
            Button btn = new Button
            {
                Height = 70,
                FlatStyle = FlatStyle.Flat,
                BackgroundImageLayout = ImageLayout.Stretch,
                ForeColor = Color.White,
                Tag = item.Id,
                Text = item.Field2,
                Name = "DynamicButton_P" + item.Id
            };

            int columns = (screnwidth > 1365 && screnheight > 765) ? 6 : 5;
            btn.Width = (pnlitembuttons.Width - 43) / columns;

            if (Convert.ToInt32(item.Field5) <= 0)
            {
                btn.Enabled = false;
                btn.BackgroundImage = Properties.Resources.INACTIVEBUTTON;
            }
            else
            {
                if (string.IsNullOrEmpty(item.Field6))
                    btn.BackgroundImage = Properties.Resources.N_dashboard_button;//Properties.Resources.Untitled_2_2030;
                else
                {
                    string path = Path.Combine(Application.StartupPath, "Background_Images", item.Field6 + ".png");
                    if (File.Exists(path))
                        btn.BackgroundImage = Image.FromFile(path);
                    else
                        btn.BackgroundImage = Properties.Resources.N_dashboard_button;
                }
            }

            if (!string.IsNullOrEmpty(item.Field7))
            {
                try
                {
                    string[] split = item.Field7.Split(',');
                    float size = float.Parse(split.Last());
                    string fontName = string.Join(",", split.Take(split.Length - 1));
                    btn.Font = new Font(fontName, size);
                }
                catch { btn.Font = new Font("Georgia", 10); }
            }
            else
            {
                btn.Font = (screnwidth > 1365 && screnheight > 765)
                    ? new Font("Georgia", 14)
                    : new Font("Georgia", 10);
            }

            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem bgItem = new ToolStripMenuItem("BG Image", FoodandBeverage.Properties.Resources.background_mouse_clcik);
            ToolStripMenuItem fontItem = new ToolStripMenuItem("Font Style", FoodandBeverage.Properties.Resources.font_mouselick);
            bgItem.ForeColor = fontItem.ForeColor = Color.Black;
            bgItem.Font = fontItem.Font = new Font("Arial", 12, FontStyle.Bold);
            menu.Items.AddRange(new[] { bgItem, fontItem });
            btn.ContextMenuStrip = menu;
            btn.Click += RedemptionProduct_clcik;
            return btn;
        }

        public void RedemptionProduct_clcik(object sender, EventArgs e)
        {
            try
            {
                int rdprid = 0;
                Button clickedButton = sender as Button;

                if (clickedButton != null)
                    rdprid = Convert.ToInt32(clickedButton.Tag);

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = @"
            r.ProductId as Id,
            r.Description as Field1,
            pr.SRate as Field2,
            ISNULL(r.Tax,'Exclusive') as Field3,
            ISNULL(ts.TaxSettingsId,0) as Field4, pr.id as Field5";

                objSearch.Table = $"[{GameDB}].dbo.tblRedemptionProducts r " +
                                  $"LEFT JOIN [{GameDB}].dbo.tblProductRate pr ON pr.ProductId = r.ProductId " +
                                  $"LEFT JOIN [{GameDB}].dbo.tblTaxSettings ts ON ts.TaxSettingsId = r.TaxSettingsId";

                objSearch.WhereNdGroupBy = "WHERE r.ProductId = " + rdprid +
                                          " AND pr.ProductType = " + Convert.ToInt32(ProductType.RedemptionTicket) +
                                          " AND pr.BranchId = " + FrmLogin.GameBranchId;

                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);

                if (gridSource != null && gridSource.Count > 0)
                {
                    foreach (var item in gridSource)
                    {
                        AddGiftRow(item, (int)ProductType.RedemptionTicket);
                    }
                    pnlgiftpreview.Visible = false;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show("Error loading product");
            }
        }
        private void AddGiftRow(dynamic item, int ptypeno)
        {
            try
            {
                foreach (DataGridViewRow dgRow in dgv_product.Rows)
                {
                    if (dgRow.IsNewRow) continue;

                    int pid = Convert.ToInt32(dgRow.Cells[pProductid].Value ?? 0);
                    int ptype = Convert.ToInt32(dgRow.Cells[PPackProductTypeno].Value ?? 0);
                    if (pid == item.Id && ptype == (int)PackItemType.Gift)
                    {
                        CMessageBox.Show("This product already added!", "Duplicate",
                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                int rowIndex = dgv_product.Rows.Add();
                var row = dgv_product.Rows[rowIndex];

                row.Cells[pslno].Value = rowIndex + 1;
                row.Cells[pProductid].Value = item.Id;
                row.Cells[pitemno].Value = item.Field1;
                row.Cells[pcategmenuno].Value = "Redemption";
                row.Cells[pqtyno].Value = 1;
                row.Cells[pPrice].Value = Convert.ToDecimal(item.Field2);
                row.Cells[pAmount].Value = Convert.ToDecimal(item.Field2);

                row.Cells[ptax].Value = Convert.ToString(item.Field3);       
                row.Cells[ptaxtype].Value = "";                              
                row.Cells[ptaxper].Value = 0;
                row.Cells[ptaxamt].Value = 0;
                row.Cells[ppretaxamt].Value = Convert.ToDecimal(item.Field2); // Pre-tax = price
                row.Cells[ppridno].Value = item.Field5;

                row.Cells[pigstamt].Value = 0;
                row.Cells[pvatamt].Value = 0;
                row.Cells[pnetamt].Value = Convert.ToDecimal(item.Field2);

                row.Cells[pproducttype].Value = ptypeno;
                row.Cells[proPartyPackDetailsId].Value = 0;
                row.Cells[pPartydeafult].Value = 0;
                row.Cells[ptaxsettingid].Value = Convert.ToInt32(item.Field4);

                row.Cells[PPackProductTypeno].Value = Convert.ToInt32(PackItemType.Gift);

                // Action column (Remove button)
                DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)row.Cells[pactionno];
                cellcolumntype.Value = Grid_Remove;
                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

                CalculateAndApplyTax(rowIndex, (int)PackItemType.Gift, Convert.ToInt32(item.Id));
            }
            catch (Exception ex)
            {
                // Optionally log ex.Message
            }
        }

        #endregion

        #region Party 
        private void UpdateTotalGuests()
        {
            int adults = 0;
            int children = 0;

            int.TryParse(txtAdults.Text, out adults);
            int.TryParse(txtChild.Text, out children);

            txttotalguest.Text = (adults + children).ToString();
        }
        private void UpdateGrandTotal_Pack()
        {
            decimal adultPrice = 0;
            decimal childPrice = 0;
            int adults = 0;
            int children = 0;

            decimal.TryParse(txtAdultPrice.Text, out adultPrice);
            decimal.TryParse(txtChildPrice.Text, out childPrice);
            int.TryParse(txtAdults.Text, out adults);
            int.TryParse(txtChild.Text, out children);

            decimal grandTotal = (adultPrice * adults) + (childPrice * children);

            txtGrandTotal_pack.Text = grandTotal.ToString("N2");
        }
        private decimal GetDecimal(TextBox txt)
        {
            decimal.TryParse(txt.Text, out decimal val);
            return val;
        }
        private void CalculatePackValues()
        {
            try
            {
                decimal GetDecimal(TextBox txt)
                {
                    decimal.TryParse(txt.Text, out decimal val);
                    return val;
                }

                UpdateGrandTotal_Pack(); 

                decimal grandTotal = GetDecimal(txtGrandTotal_pack);
                decimal discountPer = 0;//GetDecimal(discountper);
                decimal otherAdd = GetDecimal(txtOAAmount);
                decimal otherDeduct = GetDecimal(txtODAmount);

                // 🔹 TAX SETTINGS
                decimal igst = 0, cgst = 0, sgst = 0, vat = 0;
                decimal totalPercent = 0;
                string taxType = "";
                string taxMode = txttax.Text.Trim(); // Inclusive / Exclusive

                if (!string.IsNullOrEmpty(txtTaxSettingId.Text))
                {
                    int taxSettingId = Convert.ToInt32(txtTaxSettingId.Text);

                    BllSearch<DmnTaxSettings> _bllSearch = new BllSearch<DmnTaxSettings>();
                    DmnSearch dmnSearch = new DmnSearch
                    {
                        Select = "TaxType, IgstPrcnt, CgstPrcnt, SgstPrcnt, VatPrcnt",
                        Table = "tblTaxSettings",
                        WhereNdGroupBy = $"WHERE Status=1 AND TaxSettingsId={taxSettingId}"
                    };

                    var taxSettings = _bllSearch.GetSearchData("prdSearch", dmnSearch);

                    if (taxSettings.Count > 0)
                    {
                        igst = taxSettings[0].IgstPrcnt;
                        cgst = taxSettings[0].CgstPrcnt;
                        sgst = taxSettings[0].SgstPrcnt;
                        vat = taxSettings[0].VatPrcnt;

                        taxType = taxSettings[0].TaxType;

                        totalPercent = taxType == "GST" ? igst : vat;

                        txtTaxtype_pack.Text = taxType;
                        txtTaxPer_pack.Text = totalPercent.ToString("N2");
                    }
                }

                decimal discountAmt = 0;// discountPer > 0 ? (grandTotal * discountPer) / 100 : GetDecimal(txtTotalDiscountAmt);
                txttotaldisamt_pack.Text = discountAmt.ToString("0.00");

                decimal netBeforeTax = grandTotal - discountAmt + otherAdd - otherDeduct;

                decimal baseAmt = 0, taxAmt = 0, netAmt = 0;

                if (taxMode.Equals("INCLUSIVE", StringComparison.OrdinalIgnoreCase))
                {
                    baseAmt = netBeforeTax / (1 + (totalPercent / 100));
                    taxAmt = netBeforeTax - baseAmt;
                    netAmt = netBeforeTax;
                }
                else
                {
                    baseAmt = netBeforeTax;
                    taxAmt = netBeforeTax * totalPercent / 100;
                    netAmt = netBeforeTax + taxAmt;
                }

                txtPreTaxAmount_pack.Text = baseAmt.ToString("0.00");
                txtTotalTaxAmount_pack.Text = taxAmt.ToString("0.00");
                txtGrandTotal_pack.Text = netAmt.ToString("0.00");

                if (taxType == "GST")
                {
                    decimal igstAmt = igst > 0 ? taxAmt : 0;
                    decimal cgstAmt = cgst > 0 ? taxAmt * (cgst / totalPercent) : 0;
                    decimal sgstAmt = sgst > 0 ? taxAmt * (sgst / totalPercent) : 0;

                    txtIGSTAmt_pack.Text = igstAmt.ToString("0.00");
                    txtVatamt_pack.Text = (cgstAmt + sgstAmt).ToString("0.00");
                }
                else
                {
                    txtVatamt_pack.Text = taxAmt.ToString("0.00");
                    txtIGSTAmt_pack.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("PartyBooking - CalculatePackValues()", ex,FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void CalculateDiscountAmount()
        {
            try
            {
                if (string.IsNullOrEmpty(discountper.Text)) discountper.Text = "0";
                if (string.IsNullOrEmpty(txtGrandTotal.Text)) txtGrandTotal.Text = "0";

                decimal discountPer = Convert.ToDecimal(discountper.Text);
                GetGrandTotal_All();

                if (discountPer > 100)
                {
                    CMessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    discountper.Text = "0";
                    discountper.Focus();
                    return;
                }

                decimal discountAmt = originalGrandTotal * discountPer / 100;
                txtTotalDiscountAmt.Text = discountAmt.ToString("N2");

                CalculateNetAmount();
            }
            catch (Exception ex)
            {

            }

        }
        public void CalculateDiscountPer()
        {
            try
            {

                if (string.IsNullOrEmpty(txtTotalDiscountAmt.Text)) txtTotalDiscountAmt.Text = "0";
                if (string.IsNullOrEmpty(txtGrandTotal.Text)) txtGrandTotal.Text = "0";

                decimal discountAmt = Convert.ToDecimal(txtTotalDiscountAmt.Text);
                GetGrandTotal_All();

                if (discountAmt > originalGrandTotal)
                {
                    CMessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTotalDiscountAmt.Text = "0";
                    txtTotalDiscountAmt.Focus();
                    return;
                }
                decimal discountPer = originalGrandTotal == 0 ? 0 : (discountAmt / originalGrandTotal * 100);
                discountper.Text = discountPer.ToString("0.000");

                CalculateNetAmount();
            }
            catch (Exception)
            {

            }
        }
        private void txtOAAmount_TextChanged(object sender, EventArgs e) => CalculateNetAmount();
        private void txtRcvAmt_TextChanged(object sender, EventArgs e)
        {
            GetDecimal(txtRcvAmt);
            CalculateNetAmount();
        }
        private void txtODAmount_TextChanged(object sender, EventArgs e) => CalculateNetAmount();

        #endregion

        #region Cake
        private void btncakeclose_Click(object sender, EventArgs e)
        {
            pnlcake.Visible = false;
        }
        private void ResetCakeFields()
        {
            txtcakeremarks.Text = "";
            txtcakemessage.Text = "";
            txtcakeshape.Text = "";
            txtcakeprice.Text = "0";
        }
        private void btnaddcakes_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow dgRow in dgv_product.Rows)
                {
                    if (dgRow.IsNewRow) continue;

                    int productType = Convert.ToInt32(dgRow.Cells[PPackProductTypeno].Value ?? 0);
                    int productId = Convert.ToInt32(dgRow.Cells[pProductid].Value ?? 0);

                    if (productId == _cakeId && productType == (int)PackItemType.Cake)
                    {
                        CMessageBox.Show("This cake is already added!", "Duplicate",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                _cakeMessage = txtcakemessage.Text;
                _cakeRemarks = txtcakeremarks.Text;

                decimal qty = 1;

                int rowIndex = dgv_product.Rows.Add();
                DataGridViewRow row = dgv_product.Rows[rowIndex];

                row.Cells[pslno].Value = rowIndex + 1;
                row.Cells[pitemno].Value = lblcakename.Text;
                row.Cells[pcategmenuno].Value = "Cake";
                row.Cells[pqtyno].Value = qty;
                row.Cells[pPrice].Value = _cakePrice;
                row.Cells[pAmount].Value = _cakePrice;
                row.Cells[pProductid].Value = _cakeId;
                row.Cells[pmessageno].Value = _cakeMessage;
                row.Cells[premarksno].Value = _cakeRemarks;
                row.Cells[PPackProductTypeno].Value = (int)PackItemType.Cake;

                row.Cells[ptaxamt].Value = 0;
                row.Cells[ptax].Value = _cakeTaxMode;
                row.Cells[ptaxtype].Value = _cakeTaxType;
                row.Cells[ptaxper].Value = _cakeTaxPer;
                row.Cells[ptaxsettingid].Value = _cakeTaxSettingsId;
                row.Cells[ppretaxamt].Value = _cakePrice;
                row.Cells[pnetamt].Value = _cakePrice;
                row.Cells[pigstamt].Value = 0;
                row.Cells[pvatamt].Value = 0;

                DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)row.Cells[pactionno];
                cellcolumntype.Value = Grid_Remove;
                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

                CalculateAndApplyTax(rowIndex, (int)PackItemType.Cake, Convert.ToInt32(_cakeId));

                ResetCakeFields();
                LoadAllcakeTotal();

                pnl_cakeregister.Visible = false;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private void LoadCakeList()
        {
            try
            {
                flp_cake.Controls.Clear();

                BllSearch<DmnPartyPackCake> bllSearch = new BllSearch<DmnPartyPackCake>();
                DmnSearch objSearch = new DmnSearch
                {
                    Select = "PartyPackCakeId, CakeType",
                    Table = "tblPartyPackCake",
                    WhereNdGroupBy = "WHERE Status = 1",
                    OrderBy = "ORDER BY CakeType"
                };

                var cakeList = bllSearch.GetSearchData("prdSearch", objSearch);

                if (cakeList.Count > 0)
                {
                    foreach (var cake in cakeList)
                    {
                        Button btn = new Button
                        {
                            Text = cake.CakeType,
                            Tag = cake.PartyPackCakeId,
                            Width = 120,
                            Height = 40,
                            Margin = new Padding(5),
                            BackColor = Color.LightPink,
                            FlatStyle = FlatStyle.Flat,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold)
                        };

                        btn.Click += (s, e) =>
                        {
                            Button clickedBtn = s as Button;
                            int cakeId = Convert.ToInt32(clickedBtn.Tag);

                            LoadCakeDetails(cakeId);

                            // Show panel centered
                            pnl_cakeregister.Visible = true;
                            pnl_cakeregister.BringToFront();
                            pnl_cakeregister.Left = (this.ClientSize.Width - pnl_cakeregister.Width) / 2;
                            pnl_cakeregister.Top = (this.ClientSize.Height - pnl_cakeregister.Height) / 2;
                        };

                        flp_cake.Controls.Add(btn);
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show("Error loading cakes: " + ex.Message);
            }
        }
        private void LoadCakeDetails(int cakeId)
        {
            try
            {
                BllSearch<DmnPartyPackCake> bll = new BllSearch<DmnPartyPackCake>();

                DmnSearch search = new DmnSearch
                {
                    Select = "*",
                    Table = "tblPartyPackCake",
                    WhereNdGroupBy = "WHERE PartyPackCakeId = " + cakeId
                };

                var list = bll.GetSearchData("prdSearch", search);

                if (list.Count > 0)
                {
                    var item = list[0];

                    lblcakename.Text= item.CakeType;

                    // Basic
                    _cakeId = item.PartyPackCakeId;
                    _cakeSize = item.CakeSize;
                    _cakeShape = item.CakeShape;
                    _cakePrice = Convert.ToDecimal(item.Price);

                    // Tax
                    _cakeTaxType = item.TaxType;             // GST / VAT
                    _cakeTaxSettingsId = item.TaxSettingsId;
                    _cakeTaxMode = item.Tax;                 // Inclusive / Exclusive

                    // UI
                    txtcakesize.Text = _cakeSize;
                    txtcakeshape.Text = _cakeShape;
                    txtcakeprice.Text = _cakePrice.ToString("0.00");

                    txtcakemessage.Clear();
                    txtcakeremarks.Clear();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void btncake_Click(object sender, EventArgs e)
        {
            pnlcake.Visible = true;
            pnlcake.BringToFront();
            pnlcake.Left = (this.ClientSize.Width - pnlcake.Width) / 2;
            pnlcake.Top = (this.ClientSize.Height - pnlcake.Height) / 2;

            LoadCakeList(); 
        }

        private void btncakesubmit_Click(object sender, EventArgs e)
        {
            pnlcake.Visible = false;
        }
        public void LoadAllcakeTotal()
        {
            try
            {
                decimal cakeTotal = 0;

                foreach (DataGridViewRow row in dgv_product.Rows)
                {
                    if (row.IsNewRow) continue;

                    int productType = Convert.ToInt32(row.Cells[PPackProductTypeno].Value ?? 0);
                    if (productType != (int)PackItemType.Cake) continue;

                    decimal net = 0;
                    decimal.TryParse(Convert.ToString(row.Cells[pnetamt].Value), out net);
                    cakeTotal += net;
                }

                txtCakeGrandTotal.Text = cakeTotal.ToString("0.00");
                decimal mainTotal = 0;
                decimal.TryParse(txtGrandTotal.Text, out mainTotal);

                CalculateNetAmount();

                pnlcake.Visible = false;
                ResetCakeFields();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Common
        public void ToggleFirstRow(int mode)
        {

            tlp_textboxes.ColumnCount = 2;
            tlp_textboxes.ColumnStyles[0].SizeType = SizeType.Percent;
            tlp_textboxes.ColumnStyles[1].SizeType = SizeType.Percent;
            tlp_textboxes.ColumnStyles[0].Width = 50;
            tlp_textboxes.ColumnStyles[1].Width = 50;

            if (mode == 1) 
            {
                tlp_textboxes.RowCount = 4;

                for (int i = 0; i < 4; i++)
                {
                    tlp_textboxes.RowStyles[i].SizeType = SizeType.Percent;
                    tlp_textboxes.RowStyles[i].Height = 25f;
                }

                foreach (Control c in tlp_textboxes.Controls)
                {
                    if (tlp_textboxes.GetRow(c) == 0)
                        c.Visible = true;
                }
            }
            else 
            {
                tlp_textboxes.RowStyles[0].Height = 0f;

                for (int i = 1; i < 4; i++)
                {
                    tlp_textboxes.RowStyles[i].SizeType = SizeType.Percent;
                    tlp_textboxes.RowStyles[i].Height = 33.33f;
                }

                foreach (Control c in tlp_textboxes.Controls)
                {
                    if (tlp_textboxes.GetRow(c) == 0)
                        c.Visible = false;
                }
            }
        }
        private string GetCategoryName(int fcid)
        {
            string categoryName = "";
            BllSearch<DmnCategory> bll = new BllSearch<DmnCategory>();
            DmnSearch search = new DmnSearch();
            search.Select = "CategoryName";
            search.Table = "tblCategory_FTI";
            search.WhereNdGroupBy = "WHERE Status = 1 AND CategoryType = 5 AND Fcid = " + fcid;
            var list = bll.GetSearchData("prdSearch", search);
            if (list != null && list.Count > 0)
                categoryName = Convert.ToString(list[0].CategoryName);

            return categoryName;
        }
        private void LoadPackRelatedItems(int partyPackId)
        {
            try
            {
                dgv_product.Rows.Clear();

                BllSearch<DmnPartyPackDetails> bll = new BllSearch<DmnPartyPackDetails>();
                DmnSearch search = new DmnSearch();

                search.Select = @"PartyPackDetailsId,
                          PartyPackId,
                          Item,
                          Quantity,
                          PackItemType,
                          ProductId,
                          ProductType,
                          MenuCategoryId";

                search.Table = "tblPartyPackDetails";
                search.WhereNdGroupBy = "WHERE Status = 1 AND PartyPackId = " + partyPackId;

                var list = bll.GetSearchData("prdSearch", search);

                int sl = 1;

                foreach (var item in list)
                {
                    int rowIndex = dgv_product.Rows.Add();
                    DataGridViewRow row = dgv_product.Rows[rowIndex];

                    row.Cells[pslno].Value = sl++;
                    row.Cells[pitemno].Value = item.Item;
                    row.Cells[pqtyno].Value = item.Quantity;

                   if (item.MenuCategoryId != null)
                        row.Cells[pcategmenuno].Value = item.MenuCategoryId;                    

                    row.Cells[ppridno].Value = item.Productid;

                    row.Cells[PPackProductTypeno].Value = item.PackItemType;

                    row.Cells[proPartyPackDetailsId].Value = item.PartyPackDetailsId;
                    row.Cells[pPartydeafult].Value = 1;

                    row.ReadOnly = true;

                    // Default package color
                    row.DefaultCellStyle.BackColor = Color.Gainsboro;
                    row.DefaultCellStyle.ForeColor = Color.Black;

                    DataGridViewImageCell cellcolumntype =  (DataGridViewImageCell)dgv_product.Rows[rowIndex].Cells[pactionno];

                    cellcolumntype.Value = Blankimage;
                    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private void btndispanel_Click(object sender, EventArgs e)
        {
            try
            {
                decimal kdata = 0;
                using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.percentage)))
                {
                    frm.ShowDialog();

                    if (!string.IsNullOrEmpty(frm.txtkeypadvalue.Text))
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);

                    txtdiscountreason.Text = frm.txtdesc.Text.Trim() == "Enter Remarks Here....." ? "" : frm.txtdesc.Text;

                }

                if (Frmnumdiskeypad.flagpercentage)
                {
                    discountper.Text = "0";
                    txtTotalDiscountAmt.Text = "0";

                    discountper.Text = kdata.ToString("0.000");
                    CalculateDiscountAmount();

                }
                else
                {
                    discountper.Text = "0";
                    txtTotalDiscountAmt.Text = "0";

                    txtTotalDiscountAmt.Text = kdata.ToString("0.000");
                    CalculateDiscountPer();
                }

                CalculateNetAmount();
            }
            catch(Exception ex)
            {

            }
        }
        private void txtadults_TextChanged(object sender, EventArgs e)
        {
            PriceChangecalculation();
        }
        public void PriceChangecalculation()
        {
            UpdateTotalGuests();
            UpdateGrandTotal_Pack();

            CalculatePackValues();

            CalculateDiscountAmount();
            CalculateNetAmount();
        }
        private void txtchild_TextChanged(object sender, EventArgs e)
        {
            PriceChangecalculation();
        }

        public void Memoryrelease()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            try
            {
                dtpEventDate.Value = DateTime.Now.Date;  
                dtpEventTime.Value = DateTime.Now;        

                txteventdesc.Text = "";
                cmbevent.Text = "";
                txtaddress.Text = "";
                txtemail.Text = "";
                txtph.Text = "";
                txtname.Text = "";

                oadesc.Text = "";
                txtpartpackid.Text = "0";

                txtOAAmount.Text = "0.00";

                oddesc.Text = "";
                txtODAmount.Text = "0.00";

                txtTable.Text = "";

                txtTotalDiscountAmt.Text = "0.00";
                discountper.Text = "0.00";
                txtDiscountPer.Text = "0.00";

                txttax.Text = "";

                txtGrandTotal.Text = "0.00";
                txtTotalTaxAmount.Text = "0.00";
                txtPreTaxAmount.Text = "0.00";

                txtCakeGrandTotal.Text = "0.00";
                txtCardGrandTotal.Text = "0.00";
                txtGiftGrandTotal.Text = "0.00";
                txtGrandTotal_pack.Text = "0.00";

                txtPreTaxAmount_cake.Text = "0.00";
                txtPreTaxAmount_card.Text = "0.00";
                txtPreTaxAmount_gift.Text = "0.00";
                txtPreTaxAmount_pack.Text = "0.00";

                txtTotalTaxAmount_card.Text = "0.00";
                txtTotalTaxAmount_cake.Text = "0.00";
                txtTotalTaxAmount_gift.Text = "0.00";
                txtTotalTaxAmount_pack.Text = "0.00";

                txtIGSTAmt_pack.Text = "0.00";
                txtVatamt_pack.Text = "0.00";

                sterms.Text = "";

                txtTaxSettingId.Text = "0";

                txtChild.Text = "";
                txtAdults.Text = "";
                txttotalguest.Text = "";

                txtChildPrice.Text = "0.00";
                txtAdultPrice.Text = "0.00";

                lblNetAmt.Text = "0.00";
                lblBalAmt.Text = "0.00";
                txtRcvAmt.Text = "0.00";
                
                dgv_cus_menu_items.Rows.Clear();      
                dgv_packenter.Rows.Clear();
                dgv_packs_items.Rows.Clear();
                dgv_product.Rows.Clear();

                Frmnumdiskeypad.flagpercentage = false;
                txtAdults.Focus();

                pnlcake.Visible = false;
                pnlgiftpreview.Visible = false;

                pnlSplit.Visible = false;
                pnl_CustomItems_menu.Visible = false;
                pnl_enter_paks.Visible = false;
                pnl_foodpacks.Visible = false;
                pnl_pak_detailsitems.Visible = false;
                pnlPaymode.Visible = false;
                pnlcake.Visible = false;

                pnl_foodpacks.Visible = false;

                lblpackname.Visible = false;
                lblpacklabel.Visible = false;

                txtpartybookingid.Text = "0";

                lbltotalguest.Visible = false;
                txttotalguest.Visible = false;
                tlp_adult_child_label.Visible = false;
                tlp_adult_child_text.Visible = false;
                lblpackname.Visible = false;
                lblpacklabel.Visible = false;

                // 🔹 PACK
                txtIGSTAmt_pack.Text = "0.00";
                txtVatamt_pack.Text = "0.00";

                // 🔹 CAKE
                txtIGSTAmt_cake.Text = "0.00";
                txtVATAmt_cake.Text = "0.00";

                // 🔹 CARD
                txtIGSTAmt_card.Text = "0.00";
                txtVATAmt_card.Text = "0.00";

                // 🔹 GIFT
                txtIGSTAmt_gift.Text = "0.00";
                txtVATAmt_gift.Text = "0.00";

                // 🔹 FINAL TOTAL
                txtTotalIGSTAmt.Text = "0.00";
                txtTotalVATAmt.Text = "0.00";

                txtdeafultpackid.Text = "0";

                AdvanceFlag = false;

                lblNetAmt.Text = "0.00";
                txtRcvAmt.Text = "0.00";
                lbladvamtdata.Text = "0.00";

                dbReceivedAmount = 0;
                dbAdvanceAmount = 0;

                lblBalAmt.Text = "0.00";

                TogglePayCancel(0);
                BookingId = 0;
                SalesMasterId = 0;

                bookingItemAdjustments.Clear();
                dgv_cancelleditems.Rows.Clear();

                dbNetAmount = 0;
                dbBalanceAmount = 0;
                originalGrandTotal = 0;

                txtextracharge_des.Text = ""; 
                txtextracharge_amt.Text = "0";

                txtaddcharge.Text = "0";
            }
            catch (Exception ex)
            {
                CMessageBox.Show("Error in ClearAll: " + ex.Message);
            }
        }

        #endregion

        #region Reegistartion event And Fuctions
        private void txtph_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                e.Handled = true;

            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtph_TextChanged(object sender, EventArgs e)
        {
            string digitsOnly = new string(txtph.Text.Where(char.IsDigit).ToArray());

            if (txtph.Text != digitsOnly)
            {
                int selectionStart = txtph.SelectionStart;
                txtph.Text = digitsOnly;
                txtph.SelectionStart = Math.Min(selectionStart, txtph.Text.Length);
            }
        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                return System.Text.RegularExpressions.Regex.IsMatch(
                    email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase
                );
            }
            catch
            {
                return false;
            }
        }

        private void txtemail_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtemail.Text))
            {
                if (!IsValidEmail(txtemail.Text))
                {
                    CMessageBox.Show("Please enter a valid email address");
                    txtemail.Focus();
                }
            }
        }
        private void OnlyInteger_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (Backspace, Delete)
            if (char.IsControl(e.KeyChar))
                return;

            // Allow only digits
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region Payment Realted Fuction event
        private void btn_paynow_Click(object sender, EventArgs e)
        {
            try
            {

                decimal adultPrice = 0, childPrice = 0;
                decimal.TryParse(txtAdultPrice.Text, out adultPrice);
                decimal.TryParse(txtChildPrice.Text, out childPrice);

                if (adultPrice <= 0 && childPrice <= 0)
                {
                    CMessageBox.Show("Please select a Party Pack", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int adults = GetInt(txtAdults);
                int children = GetInt(txtChild);

                if (adults == 0 && children == 0)
                {
                    CMessageBox.Show("Please enter number of attendees (Adults or Children)", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAdults.Focus();
                    return;
                }


                decimal rcvAmt = 0;
                decimal.TryParse(txtRcvAmt.Text, out rcvAmt);

                if (!string.IsNullOrEmpty(txtpartybookingid.Text) && int.TryParse(txtpartybookingid.Text, out int bookingId) && bookingId > 0)
                {
                    decimal lblNet = decimal.TryParse(lblNetAmt.Text, out decimal na) ? na : 0;
                    decimal advancePaid = decimal.TryParse(lbladvamtdata.Text, out decimal aa) ? aa : 0;

                    decimal totalAdjustment = 0;

                    if (bookingItemAdjustments.Count > 0)
                    {
                        foreach (var adj in bookingItemAdjustments)
                            totalAdjustment += adj.Amount;
                    }

                    decimal newNetAmount = lblNet + totalAdjustment;
                    decimal totalPaid = advancePaid + rcvAmt;

                    decimal finalBalance = decimal.TryParse(lblBalAmt.Text, out decimal ba) ? ba : 0;

                    bool adjustmentExists = bookingItemAdjustments.Count > 0;
                    bool netChanged = newNetAmount != dbNetAmount;
                    bool paymentChanged = rcvAmt > 0;

                    if ((adjustmentExists || netChanged || paymentChanged) && finalBalance != 0)
                    {
                        ShowPaymentSettlementPanel(lblNet, advancePaid, rcvAmt);
                        return;
                    }
                }

                if (rcvAmt == 0)
                {
                    CMessageBox.Show("Please type received amount", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtRcvAmt.Text = "0";
                    txtRcvAmt.Focus();
                    return;
                }

                if (MinimumAdvance > 0 && rcvAmt < MinimumAdvance)
                {
                    CMessageBox.Show(
                        $"Minimum advance for package '{lblpackname.Text}' is {MinimumAdvance}",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtRcvAmt.Focus();
                    return;
                }

                pnlPaymode.Visible = true;
                pnlPaymode.BringToFront();
                pnlPaymode.Left = (this.ClientSize.Width - pnlPaymode.Width) / 2;
                pnlPaymode.Top = (this.ClientSize.Height - pnlPaymode.Height) / 2;
            }
            catch (Exception ex)
            {
                CMessageBox.Show("Error: " + ex.Message);
            }
        }

        private void txtRcvAmt_Click(object sender, EventArgs e)
        {
            try
            {
                decimal typedAmount = 0;

                using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.NoPercentageReason)))
                {
                    frm.ShowDialog();

                    if (!string.IsNullOrEmpty(frm.txtkeypadvalue.Text))
                        typedAmount = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                }

                if (typedAmount <= 0)
                {
                    CMessageBox.Show("Please enter a valid received amount", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRcvAmt.Focus();
                    return;
                }

                decimal netAmt = Convert.ToDecimal(lblNetAmt.Text);

                txtRcvAmt.Text = typedAmount.ToString("N2");

                decimal totalAdvance = dbAdvanceAmount + typedAmount;

                if (totalAdvance < netAmt)
                {
                    AdvanceFlag = true;
                    ToggleFirstRow(1);

                    lbladvamtdata.Text = totalAdvance.ToString("N2");
                    lblBalAmt.Text = (netAmt - totalAdvance).ToString("N2");
                }
                else
                {
                    AdvanceFlag = false;
                    ToggleFirstRow(0);
                    lbladvamtdata.Text = netAmt.ToString("N2");
                    lblBalAmt.Text = "0.00";
                }

                textb_shape();
            }
            catch
            {
            }
        }

        private void btncash_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                switch (clickedButton.Name)
                {
                    case "btncash":
                        PaymentMode = Convert.ToInt32(PaymentType.CASH);
                        break;
                    case "btndebit":
                        PaymentMode = Convert.ToInt32(PaymentType.DEBITCARD);
                        break;
                    case "btnupi":
                        PaymentMode = Convert.ToInt32(PaymentType.UPI);
                        break;
                    case "btnnetbanking":
                        PaymentMode = Convert.ToInt32(PaymentType.BANKINGONLINE);
                        break;
                    case "btncreditcard":
                        PaymentMode = Convert.ToInt32(PaymentType.CREDITCARD);
                        break;
                    case "btncomple":
                        PaymentMode = Convert.ToInt32(PaymentType.COMPLEMENTRY);
                        break;
                    case "btnnocharge":
                        PaymentMode = Convert.ToInt32(PaymentType.Nocharge);
                        break;
                }
                Paybill();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private int GetInt(TextBox txt)
        {
            if (txt == null || string.IsNullOrWhiteSpace(txt.Text))
                return 0;

            int.TryParse(txt.Text, out int val);
            return val;
        }
        private decimal GetDecimalFromLabel(Label lbl)
        {
            if (lbl == null || string.IsNullOrWhiteSpace(lbl.Text))
                return 0;

            decimal.TryParse(lbl.Text, out decimal val);
            return val;
        }
        public void setperfixsale()
        {
            try
            {
                BllSearch<DmnSalemaster> bllSearch = new BllSearch<DmnSalemaster>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "SalesPrefix ,SalesSerialNo";
                dmnSearch.Table = " tblHotelRegistration";
                dmnSearch.WhereNdGroupBy = " Where BranchId = " + FrmDashboard.BranchId;
                dmnSearch.OrderBy = "";
                var gridSource1 = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource1.Count > 0)
                {
                    txtinvoiceno.Text = gridSource1[0].SalesPrefix + FrmDashboard.BranchId + DateTime.Now.Year.ToString() + gridSource1[0].SalesSerialNo;//PERRET20221001
                }
            }
            catch (Exception ex)
            {

                clsgen.LogException("Order page  setperfixsale()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public  void Paybill()
        {
            try
            {
                int bookingId = 0;
                GetInt(txtpartybookingid);
                GetInt(txtdeafultpackid);
                GetDecimal(txtaddcharge);

                BllPartyBooking bll = new BllPartyBooking();
                DmnPartyBooking booking = new DmnPartyBooking();

                #region BOOKING MASTER AND DETAILS

                #region PARTY MASTER

                booking.PartyBookingId = GetInt(txtpartybookingid);
                booking.PartyPackId = Partypackid;

                // 🔹 CUSTOMER INFO
                booking.CustomerName = txtname.Text?.Trim();
                booking.Phone = txtph.Text?.Trim();
                booking.Email = txtemail.Text?.Trim();
                booking.Address = txtaddress.Text?.Trim();

                // 🔹 EVENT INFO
                booking.EventType =Convert.ToInt32(cmbevent.SelectedValue);
                booking.EventDescription = txteventdesc.Text?.Trim();

                // 🔹 DATE
                booking.BookingDate = DateTime.Now.Date;
                booking.BookingTime = DateTime.Today.Add(DateTime.Now.TimeOfDay);

                // 🔹 PEOPLE COUNT
                booking.NoOfAdults = GetInt(txtAdults);
                booking.NoOfChildren = GetInt(txtChild);

                // 🔹 PACK PRICE
                decimal adultPrice = GetDecimal(txtAdultPrice);
                decimal childPrice = GetDecimal(txtChildPrice);

                if (booking.NoOfAdults > 0 && booking.NoOfChildren > 0)
                    booking.PackPrice = adultPrice + childPrice;
                else if (booking.NoOfAdults > 0)
                    booking.PackPrice = adultPrice;
                else if (booking.NoOfChildren > 0)
                    booking.PackPrice = childPrice;
                else
                    booking.PackPrice = 0;

                // 🔹 TAX
                booking.Tax = txttax.Text ?? "";
                booking.TaxType = txtTaxtype_pack.Text ?? "";
                booking.TaxPer = GetDecimal(txtTaxPer_pack);
                booking.TaxAmount = GetDecimal(txtTotalTaxAmount);

                // 🔹 GST / VAT
                decimal igst = GetDecimal(txtTotalIGSTAmt);
                booking.IGSTAmt = igst;
                booking.CGSTAmt = igst / 2;
                booking.SGSTAmt = igst / 2;
                booking.VATAmt = GetDecimal(txtTotalVATAmt);

                // 🔹 DISCOUNT
                booking.DiscountPer = GetDecimal(txtDiscountPer);
                booking.DiscountAmount = GetDecimal(txtTotalDiscountAmt);
                booking.DiscountReason = txtdiscountreason.Text ?? "";

                // 🔹 AMOUNTS
                booking.PreTaxAmount = GetDecimal(txtPreTaxAmount);
                booking.PostTaxAmount = GetDecimal(txtTotalTaxAmount);
                booking.NetAmount = GetDecimalFromLabel(lblNetAmt);

                // 🔹 PAYMENT
                decimal currentReceived = GetDecimal(txtRcvAmt), totalAdvance=0;
                booking.ReceivedAmount = currentReceived;

                decimal lblBalance = decimal.TryParse(lblBalAmt.Text, out decimal bal) ? bal : 0;

                if (lblBalance == 0)
                {
                    booking.AdvanceAmount = 0;
                    booking.ReceivedAmount = 0;
                    booking.BalanceAmount = 0;
                }
                else
                {
                     totalAdvance = dbAdvanceAmount + currentReceived;

                    if (totalAdvance >= booking.NetAmount)
                    {
                        booking.AdvanceAmount = booking.NetAmount;
                        booking.BalanceAmount = 0;
                    }
                    else
                    {
                        booking.AdvanceAmount = totalAdvance;
                        booking.BalanceAmount = booking.NetAmount - totalAdvance;
                    }
                }

                booking.PaymentMode = 0;

                // 🔹 CATEGORY TOTALS
                booking.CakeGrandTotal = GetDecimal(txtCakeGrandTotal);
                booking.CakePreTaxAmount = GetDecimal(txtPreTaxAmount_cake);
                booking.CakeTaxAmount = GetDecimal(txtTotalTaxAmount_cake);

                booking.CardGrandTotal = GetDecimal(txtCardGrandTotal);
                booking.CardPreTaxAmount = GetDecimal(txtPreTaxAmount_card);
                booking.CardTaxAmount = GetDecimal(txtTotalTaxAmount_card);

                booking.GiftGrandTotal = GetDecimal(txtGiftGrandTotal);
                booking.GiftPreTaxAmount = GetDecimal(txtPreTaxAmount_gift);
                booking.GiftTaxAmount = GetDecimal(txtTotalTaxAmount_gift);

                booking.PackGrandTotal = GetDecimal(txtGrandTotal_pack);
                booking.PackPreTaxAmount = GetDecimal(txtPreTaxAmount_pack);
                booking.PackTaxAmount = GetDecimal(txtTotalTaxAmount_pack);

                // 🔹 AUDIT
                booking.CreatedBy = FrmLogin.EmployeeId;
                booking.BranchId = FrmDashboard.BranchId;
                booking.Status = 1;

                booking.AdditionalChargeAmt = GetDecimal(txtaddcharge);
                booking.AdditionalChargeDescription = txtextracharge_des.Text;

                var result = bll.AddEditData("prdInsert_Upd_PartyBooking", booking);

                if (result.PartyBookingId <= 0)
                {
                    CMessageBox.Show("Master Save Failed");
                    return;
                }

                 bookingId = result.PartyBookingId;

                #endregion

                #region SAVE PAYMENT
                if (bookingId > 0)
                {
                    BllPartyBookingPayment bllPayment = new BllPartyBookingPayment();

                    if (PaymentMode == Convert.ToInt32(PaymentType.SPLIT))
                    {
                        decimal splitTotal = 0;

                        foreach (var p in partPayments)
                        {
                            if (decimal.TryParse(p.PartAmount, out decimal amt))
                                splitTotal += amt;
                        }

                        totalAdvance = dbAdvanceAmount + splitTotal;

                        decimal balance;
                        if (totalAdvance >= booking.NetAmount)
                        {
                            totalAdvance = 0;
                            balance = 0;
                        }
                        else
                        {
                            balance = booking.NetAmount - totalAdvance;
                        }

                        foreach (var p in partPayments)
                        {
                            if (!decimal.TryParse(p.PartAmount, out decimal amt))
                                continue;

                            DmnPartyBookingPayment payment = new DmnPartyBookingPayment
                            {
                                PaymentId = 0,
                                PartyBookingId = bookingId,
                                PaymentMode = Convert.ToInt32(p.PartPayId),

                                PaymentAmount = booking.NetAmount,
                                RcvAmnt = amt,
                                BalAmnt = balance,

                                IsAdvance = AdvanceFlag,
                                ReferenceNo = "",
                                PaymentDescription = "",

                                CreatedBy = FrmLogin.EmployeeId,
                                BranchId = FrmDashboard.BranchId,
                                Status = 1,
                                IsSplitPayment = true
                            };

                            var paymentResult = bllPayment.AddEditPayment(payment);

                            if (paymentResult.PaymentId <= 0)
                            {
                                CMessageBox.Show("Split payment save failed.");
                                return;
                            }
                        }
                    }
                    else
                    {
                        currentReceived = booking.ReceivedAmount;

                        totalAdvance = dbAdvanceAmount + currentReceived;

                        decimal balance;
                        if (totalAdvance >= booking.NetAmount)
                        {
                            totalAdvance = 0;
                            balance = 0;
                        }
                        else
                        {
                            balance = booking.NetAmount - totalAdvance;
                        }

                        DmnPartyBookingPayment payment = new DmnPartyBookingPayment
                        {
                            PaymentId = 0,
                            PartyBookingId = bookingId,
                            PaymentMode = PaymentMode,

                            PaymentAmount = booking.NetAmount,
                            RcvAmnt = currentReceived,
                            BalAmnt = balance,

                            IsAdvance = AdvanceFlag,
                            ReferenceNo = "",
                            PaymentDescription = "",

                            CreatedBy = FrmLogin.EmployeeId,
                            BranchId = FrmDashboard.BranchId,
                            Status = 1,
                            IsSplitPayment = false
                        };

                        var paymentResult = bllPayment.AddEditPayment(payment);

                        if (paymentResult.PaymentId <= 0)
                        {
                            CMessageBox.Show("Payment save failed.");
                        }
                    }
                }
                #endregion

                #region INSERT PARTY PACK AS DETAILS

                DmnPartyBookingDetails packDetails = new DmnPartyBookingDetails();

                packDetails.PartyBookingDetailsId = GetInt(txtdeafultpackid);
                packDetails.PartyBookingId = bookingId;
               
                packDetails.Product = lblpackname.Text;
                packDetails.ProductId = Partypackid;
                packDetails.ProductType = Convert.ToInt32(PackItemType.Pack);
                packDetails.TableType = 0;

                packDetails.Qty = 1;

                packDetails.Tax = txttax.Text;

                packDetails.TaxType = txtTaxtype_pack.Text;
                packDetails.TaxPer = Convert.ToDecimal(txtTaxPer_pack.Text);

                packDetails.TaxAmount = Convert.ToDecimal(txtTotalTaxAmount_pack.Text);
                igst = Convert.ToDecimal(txtIGSTAmt_pack.Text);
                packDetails.IGSTAmt = igst;
                packDetails.CGSTAmt = igst / 2;
                packDetails.SGSTAmt = igst / 2;
                packDetails.VATAmt = Convert.ToDecimal(txtVatamt_pack.Text);

                packDetails.PostTaxAmount = Convert.ToDecimal(txtTotalTaxAmount_pack.Text);
                packDetails.PreTaxAmount = Convert.ToDecimal(txtPreTaxAmount_pack.Text);
                packDetails.NetAmount = Convert.ToDecimal(txtGrandTotal_pack.Text);

                packDetails.CreatedBy = FrmDashboard.UserId;
                packDetails.BranchId = FrmDashboard.BranchId;
                packDetails.Status = 1;
                packDetails.IsPackage = true;

                var resultpack = bll.AddEditDataDetails("prdInsert_Upd_PartyBookingDetails", packDetails);
                PartyBookingDetailsId = resultpack.PartyBookingDetailsId;

                #endregion

                #region CARD GIFT CAKE
                foreach (DataGridViewRow row in dgv_product.Rows)
                {
                    if (row.IsNewRow) continue;

                    int productType = Convert.ToInt32(row.Cells[PPackProductTypeno].Value ?? 0);
                    int productId = Convert.ToInt32(row.Cells[pProductid].Value ?? 0);


                    if (productType <= 0 || productId <= 0)
                        continue;


                    DmnPartyBookingDetails det = new DmnPartyBookingDetails();

                    det.PartyBookingDetailsId = Convert.ToInt32(row.Cells[proPartyPackDetailsId].Value ?? 0);
                    det.PartyBookingId = bookingId;

                    // PRODUCT INFO
                    det.Product = Convert.ToString(row.Cells[pitemno].Value ?? "");
                    det.CategoryName = Convert.ToString(row.Cells[pcategmenuno].Value ?? "");
                    det.ProductId = productId;
                    det.ProductType = productType;

                    // Assign TableType based on product type
                    if (productType == (int)PackItemType.Card)
                        det.TableType = Convert.ToInt32(ProductType.Card);
                    else if (productType == (int)PackItemType.Gift)
                        det.TableType = Convert.ToInt32(ProductType.RedemptionTicket);
                    else if (productType == (int)PackItemType.Cake)
                        det.TableType = 0;
                    else
                        det.TableType = 0;

                    // QTY & PRICE
                    det.Qty = Convert.ToDecimal(row.Cells[pqtyno].Value ?? 0);
                    det.Price = Convert.ToDecimal(row.Cells[pPrice].Value ?? 0);
                    det.Amount = Convert.ToDecimal(row.Cells[pAmount].Value ?? 0);

                    // TAX
                    det.Tax = Convert.ToString(row.Cells[ptax].Value ?? "");
                    det.TaxType = Convert.ToString(row.Cells[ptaxtype].Value ?? "");
                    det.TaxPer = Convert.ToDecimal(row.Cells[ptaxper].Value ?? 0);
                    det.TaxAmount = Convert.ToDecimal(row.Cells[ptaxamt].Value ?? 0);
                    det.TaxSettingId = Convert.ToInt32(row.Cells[ptaxsettingid].Value ?? 0);
                    det.IGSTAmt = Convert.ToDecimal(row.Cells[pigstamt].Value ?? 0);
                    det.VATAmt = Convert.ToDecimal(row.Cells[pvatamt].Value ?? 0);

                    // DISCOUNT
                    det.DiscountPer = Convert.ToDecimal(row.Cells[pdiscper].Value ?? 0);
                    det.DiscountAmount = Convert.ToDecimal(row.Cells[pdisamt].Value ?? 0);

                    // AMOUNT
                    det.NetAmount = Convert.ToDecimal(row.Cells[pnetamt].Value ?? 0);
                    det.PreTaxAmount = productType == (int)PackItemType.Cake
                                        ? Convert.ToDecimal(txtPreTaxAmount_cake.Text)
                                        : Convert.ToDecimal(row.Cells[ppretaxamt].Value ?? 0);
                    det.PostTaxAmount = productType == (int)PackItemType.Cake
                                        ? Convert.ToDecimal(txtTotalTaxAmount_cake.Text)
                                        : Convert.ToDecimal(row.Cells[ptaxamt].Value ?? 0);

                    // CAKE INFO
                    det.CakeRemarks = productType == (int)PackItemType.Cake ? Convert.ToString(row.Cells[premarksno].Value ?? "") : null;
                    det.CakeMessage = productType == (int)PackItemType.Cake ? Convert.ToString(row.Cells[pmessageno].Value ?? "") : null;

                    // AUDIT
                    det.CreatedBy = FrmDashboard.UserId;
                    det.BranchId = FrmDashboard.BranchId;
                    det.Status = 1;

                    // Insert/Update
                    var resultBooking = bll.AddEditDataDetails("prdInsert_Upd_PartyBookingDetails", det);
                    row.Cells[proPartyPackDetailsId].Value = resultBooking.PartyBookingDetailsId;

                }
                #endregion

                #endregion

                #region SALE MASTER AND DETAILS

                #region SALE MASTER

                BllSale bllSales = new BllSale();
                DmnSalemaster sale = new DmnSalemaster();

                sale.SalesMasterId = SalesMasterId;
                setperfixsale();
                sale.EntryNo = txtinvoiceno.Text;
                sale.EntryDate = dtpEventDate.Value.Date.Add(dtpEventTime.Value.TimeOfDay);

                sale.PayId = PaymentMode;

                sale.TotalAmnt = booking.NetAmount;
                sale.NetAmount = booking.NetAmount;
                sale.TotalTax = booking.TaxAmount;

                sale.TotalIGST = booking.IGSTAmt;
                sale.TotalCGST = booking.CGSTAmt;
                sale.TotalSGST = booking.SGSTAmt;
                sale.TotalVAT = booking.VATAmt;

                sale.RcvAmnt = booking.ReceivedAmount;
                sale.BalAmnt = booking.BalanceAmount;

                sale.Remark = txtdiscountreason.Text;

                sale.CreatedBy = FrmLogin.EmployeeId;
                sale.BranchId = FrmDashboard.BranchId;
                sale.Status = 1;

                sale.mDeviceIP = " [ " + System.Environment.MachineName + " ]  -" + Dns.GetHostByName(System.Environment.MachineName).AddressList[0].ToString();
                sale.mDevice = clsgen.FetchMacId();

                sale.TotalPreTaxAmt = booking.PreTaxAmount;
                sale.DisAmnt = booking.DiscountAmount;
                sale.DisPer = booking.DiscountPer;

                sale.PartyBookingId = bookingId;

                sale.AdditionalChargeAmt = GetDecimal(txtaddcharge);
                sale.AdditionalChargeDescription = txtextracharge_des.Text;

                var saleResult = bllSales.AddEditPartyBookingSale("prdInsert_Upd_SalesMaster_PartyBooking", sale);

                if (saleResult.SalesMasterId <= 0)
                {
                    CMessageBox.Show("Sale Master Save Failed");
                    return;
                }

                SalesMasterId = saleResult.SalesMasterId;

                #endregion

                #region INSERT PACK SALE DETAILS

                DmnSaledetails detPack = new DmnSaledetails();

                detPack.SalesMasterId = SalesMasterId;
                detPack.SalesDetailsId = Partypacksaledetailid;

                detPack.itemid = Partypackid;
                detPack.ItemName = lblpackname.Text;

                detPack.Quantity = 1;
                detPack.SalesRate = booking.PackPrice;
                detPack.MRP = booking.PackPrice;

                detPack.Amount = booking.PackPreTaxAmount;

                detPack.DiscountPer = booking.DiscountPer;
                detPack.DiscountAmount = booking.DiscountAmount;

                detPack.Tax = booking.Tax;
                detPack.TaxType = booking.TaxType;
                detPack.TaxPer = booking.TaxPer;

                detPack.TaxAmount = booking.PackTaxAmount;

                detPack.IGSTAmount = booking.IGSTAmt;
                detPack.CGSTAmount = booking.CGSTAmt;
                detPack.SGSTAmount = booking.SGSTAmt;

                detPack.VatAmount = booking.VATAmt;

                detPack.MarketPrice = booking.PackPrice;

                detPack.PreTaxAmt = booking.PackPreTaxAmount;
                detPack.NetAmount = booking.PackGrandTotal;

                detPack.IsPackage = true;
                detPack.PackageType = 1;
                detPack.PartyBookingId = bookingId;

                detPack.PartyBookingDetailsId = PartyBookingDetailsId;
       
                detPack.CreatedBy = FrmDashboard.UserId;
                detPack.BranchId = FrmDashboard.BranchId;
                detPack.Status = 1;

                bllSales.AddEditPartyBookingSaleDetails("prdInsert_Upd_SalesDetails_PartyBooking", detPack);

                #endregion

                #region CARD GIFT CAKE SALE DETAILS

                foreach (DataGridViewRow row in dgv_product.Rows)
                {
                    if (row.IsNewRow) continue;

                    int productType = Convert.ToInt32(row.Cells[pProductid].Value ?? 0);

                    if (productType <= 0) continue;

                    DmnSaledetails detSale = new DmnSaledetails();

                    detSale.SalesMasterId = SalesMasterId;
                    detSale.SalesDetailsId = Convert.ToInt32(row.Cells[psaledetailidno].Value ?? 0);
                    detSale.itemid = Convert.ToInt32(row.Cells[pProductid].Value ?? 0);
                    detSale.ItemName = Convert.ToString(row.Cells[pitemno].Value ?? "");

                    detSale.Quantity = Convert.ToDecimal(row.Cells[pqtyno].Value ?? 0);
                    detSale.SalesRate = Convert.ToDecimal(row.Cells[pPrice].Value ?? 0);
                    detSale.MRP = detSale.SalesRate;

                    // PreTaxAmt and Amount
                    detSale.Amount = Convert.ToDecimal(row.Cells[pAmount].Value ?? 0);
                    detSale.PreTaxAmt = Convert.ToDecimal(row.Cells[ppretaxamt].Value ?? 0);
                    detSale.NetAmount = Convert.ToDecimal(row.Cells[pnetamt].Value ?? 0);

                    // Discount
                    detSale.DiscountPer = Convert.ToDecimal(row.Cells[pdiscper].Value ?? 0);
                    detSale.DiscountAmount = Convert.ToDecimal(row.Cells[pdisamt].Value ?? 0);

                    // Tax
                    detSale.Tax = Convert.ToString(row.Cells[ptax].Value ?? "");
                    detSale.TaxType = Convert.ToString(row.Cells[ptaxtype].Value ?? "");
                    detSale.TaxPer = Convert.ToDecimal(row.Cells[ptaxper].Value ?? 0);
                    detSale.TaxAmount = Convert.ToDecimal(row.Cells[ptaxamt].Value ?? 0);

                    detSale.IGSTAmount = Convert.ToDecimal(row.Cells[pigstamt].Value ?? 0);
                    detSale.CGSTAmount = detSale.IGSTAmount / 2;
                    detSale.SGSTAmount = detSale.IGSTAmount / 2;
                    detSale.VatAmount = Convert.ToDecimal(row.Cells[pvatamt].Value ?? 0);

                    detSale.MarketPrice = detSale.SalesRate;

                    // Cake specific info
                    if (productType == (int)PackItemType.Cake)
                    {
                        detSale.CakeMessage = Convert.ToString(row.Cells[pmessageno].Value ?? "");
                        detSale.PackageType = (int)PackItemType.Cake;
                    }
                    else if (productType == (int)PackItemType.Card)
                        detSale.PackageType = 2; // Card
                    else if (productType == (int)PackItemType.Gift)
                        detSale.PackageType = 3; // Gift

                    detSale.IsPackage = false;
                    detSale.PartyBookingId = bookingId;
                    detSale.PartyBookingDetailsId = Convert.ToInt32(row.Cells[proPartyPackDetailsId].Value ?? 0);

                    // Audit
                    detSale.CreatedBy = FrmDashboard.UserId;
                    detSale.BranchId = FrmDashboard.BranchId;
                    detSale.Status = 1;

                    bllSales.AddEditPartyBookingSaleDetails("prdInsert_Upd_SalesDetails_PartyBooking", detSale);
                }

                #endregion

                #endregion

                CMessageBox.Show("Party Booking Saved Successfully");
                ClearAll();
            }
            catch(Exception ex)
            {
            }
        }

        private void btncomple_Click(object sender, EventArgs e)
        {
            PaymentMode = Convert.ToInt32(PaymentType.COMPLEMENTRY);
            Paybill();
        }

        private void btn_other_Click(object sender, EventArgs e)
        {
            try
            {
                PaymentMode = Convert.ToInt32(PaymentType.SPLIT);
                pnlPaymode.Visible = false;
                btnPartPaynow.Visible = false;
                pnlSplit.Visible = true;
                pnlSplit.BringToFront();
                pnlSplit.Location = new Point(this.ClientSize.Width / 2 - pnlSplit.Size.Width / 2, (this.ClientSize.Height / 2 - pnlSplit.Size.Height / 2));
                partPayments.Clear();
                lblpartpayment1.Text = "Payment 1";
                lblpartAmt2.Text = "Payment 2";
                lblpartAmt1.Text = "0.00";
                lblpartAmt2.Text = "0.00";
                cash_part.Enabled = true;
                credit_part.Enabled = true;
                Debit_part.Enabled = true;
                upi_part.Enabled = true;
                netbanking_part.Enabled = true;
                midas_part.Enabled = true;

                lblPartNetAmt.Text = txtRcvAmt.Text;
                lblPartBalAmt.Text = txtRcvAmt.Text;
            }
            catch(Exception)
            { 
            }
        }

        private void btnclos_payment_Click(object sender, EventArgs e)
        {
            pnlPaymode.Visible = false;
        }

        #endregion

        #region Search Party Booking
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                int returnValue = 0;

                DmnSearch search = new DmnSearch();
                search.ColumnHeaders = "ID,Customer,Event,Date,PhoneNo,Email";
                search.Select = "PartyBookingId as Id, CustomerName as Field1, e.ItemName as Field2, BookingDate as Field3, pb.Phone as Field4, pb.Email as Field5";
                search.Table = "tblPartyBooking pb " +
                    "left join tblEventCommonItems e on e.EventItemId=pb.EventType and e.Status = 1 AND EventCategoryType = 1 " +
                    "left join tblPartyPackMaster pm on pm.PartyPackId=pb.PartyPackId and pm.status=1";
                search.WhereNdGroupBy = "WHERE pb.Status = 1 and pb.refundstatus!=1";
                search.OrderBy = " ORDER BY BookingDate DESC";

                var gridSource = bllSearch.GetSearchData("prdSearch", search);

                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records Found");
                    return;
                }

                using (var frm = new FrmSearch<DmnGridFields>(
                    search,
                    SearchMode.FirstCharacter,
                    this.Width - 300,
                    this.Height - 300,
                    this.Location.Y + 50,
                    this.Location.X + 20,
                    null))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        returnValue = Convert.ToInt32(frm._retValue);
                    }
                }

                if (returnValue <= 0) return;

                ClearAll();

                BookingId = returnValue;

                TogglePayCancel(1);

                LoadBookingMaster(returnValue, Convert.ToInt32(txtpartpackid.Text));

                LoadDefaultPackAndBookingItems(Convert.ToInt32(txtpartpackid.Text), returnValue);

                lblpackname.Visible = true;
                lblpacklabel.Visible = true;
                lblpackname.Text = SetPackName(Convert.ToInt32(txtpartpackid.Text));

                textb_shape();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private decimal GetTotalPaidAmount(int bookingId)
        {
            try
            {
                decimal totalPaid = 0;

                BllSearch<DmnPartyBookingPayment> bll = new BllSearch<DmnPartyBookingPayment>();
                DmnSearch search = new DmnSearch();

                search.Select = "ISNULL(SUM(PaidAmount),0) AS PaidAmount";
                search.Table = "tblPartyBookingPayment";
                search.WhereNdGroupBy = $"WHERE PartyBookingId = {bookingId} AND Status = 1";

                var result = bll.GetSearchData("prdSearch", search).FirstOrDefault();

                if (result != null)
                    totalPaid = result.PaidAmount;

                return totalPaid;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        private void LoadBookingMaster(int bookingId,int packid)
        {
            try
            {
                LoadEventItems();

                BllSearch<DmnPartyBooking> bll = new BllSearch<DmnPartyBooking>();
                DmnSearch search = new DmnSearch();
                search.Select = "*";
                search.Table = "tblPartyBooking";
                search.WhereNdGroupBy = $"WHERE PartyBookingId = {bookingId} AND Status = 1";

                var result = bll.GetSearchData("prdSearch", search).FirstOrDefault();
                if (result == null) return;

                txtname.Text = result.CustomerName;
                txtph.Text = result.Phone;
                txtemail.Text = result.Email;
                txtaddress.Text = result.Address;

                cmbevent.SelectedValue = Convert.ToInt32(result.EventType);

                txteventdesc.Text = result.EventDescription;
                txtAdults.Text = result.NoOfAdults.ToString();
                txtChild.Text = result.NoOfChildren.ToString();

                txtDiscountPer.Text = result.DiscountPer.ToString("0.00");
                txtTotalTaxAmount.Text = result.TaxAmount.ToString();

                txtextracharge_amt.Text = Convert.ToString(result.AdditionalChargeAmt);
                txtextracharge_des.Text =result.AdditionalChargeDescription;
                txtaddcharge.Text = Convert.ToString(result.AdditionalChargeAmt);

                decimal netAmt = result.NetAmount;
                decimal advAmt = result.AdvanceAmount;

                txtRcvAmt.Text = "0.00";
                lblNetAmt.Text = netAmt.ToString("0.00");
                lbladvamtdata.Text = advAmt.ToString("0.00");

                lblBalAmt.Text = (netAmt - advAmt).ToString("0.00");

                dbReceivedAmount = result.ReceivedAmount;
                dbAdvanceAmount = result.AdvanceAmount;

                decimal balance = netAmt - advAmt;

                txtRcvAmt.Text = "0.00";
                lblNetAmt.Text = netAmt.ToString("0.00");
                lbladvamtdata.Text = advAmt.ToString("0.00");

                if (Convert.ToDecimal(result.BalanceAmount) != 0)
                {
                    lblBalAmt.Text = balance.ToString("0.00");
                    dbBalanceAmount = balance;
                }
                else
                {
                    lblBalAmt.Text = "0.00";
                    dbBalanceAmount = 0;
                }

                dbNetAmount = netAmt;

                if (result.AdvanceAmount > 0)
                {
                    ToggleFirstRow(1);
                }
                else
                {
                    ToggleFirstRow(0);
                }

                txtTotalDiscountAmt.Text = result.DiscountAmount.ToString("0.00");

                txtpartpackid.Text = result.PartyPackId.ToString();
                Partypackid = Convert.ToInt32(result.PartyPackId);
                txtpartybookingid.Text = result.PartyBookingId.ToString();

                // ===============================
                // 🔹 CAKE TOTALS
                // ===============================
                txtCakeGrandTotal.Text = result.CakeGrandTotal.ToString("0.00");
                txtPreTaxAmount_cake.Text = result.CakePreTaxAmount.ToString("0.00");
                txtTotalTaxAmount_cake.Text = result.CakeTaxAmount.ToString("0.00");

                // ===============================
                // 🔹 CARD TOTALS
                // ===============================
                txtCardGrandTotal.Text = result.CardGrandTotal.ToString("0.00");
                txtPreTaxAmount_card.Text = result.CardPreTaxAmount.ToString("0.00");
                txtTotalTaxAmount_card.Text = result.CardTaxAmount.ToString("0.00");

                // ===============================
                // 🔹 GIFT TOTALS
                // ===============================
                txtGiftGrandTotal.Text = result.GiftGrandTotal.ToString("0.00");
                txtPreTaxAmount_gift.Text = result.GiftPreTaxAmount.ToString("0.00");
                txtTotalTaxAmount_gift.Text = result.GiftTaxAmount.ToString("0.00");

                // ===============================
                // 🔹 PACKAGE TOTALS
                // ===============================
                txtGrandTotal_pack.Text = result.PackGrandTotal.ToString("0.00");
                txtPreTaxAmount_pack.Text = result.PackPreTaxAmount.ToString("0.00");
                txtTotalTaxAmount_pack.Text = result.PackTaxAmount.ToString("0.00");

                // 🔹 EVENT DATE & TIME
                if (result.BookingDate != null)
                    dtpEventDate.Value = result.BookingDate;

                if (result.BookingTime != null)
                    dtpEventTime.Value = result.BookingTime;

                BllSearch<DmnPartyPack> bllMaster = new BllSearch<DmnPartyPack>();
                DmnSearch searchMaster = new DmnSearch
                {
                    Select = "PartyPackId, PackName, AdultPrice, ChildPrice, DiscountPer, DiscountAmt, TaxType, Tax,TaxSettingsId",
                    Table = "tblPartyPackMaster",
                    WhereNdGroupBy = $"WHERE PartyPackId = {Convert.ToInt32(txtpartpackid.Text)} AND Status = 1"
                };

                var master = bllMaster.GetSearchData("prdSearch", searchMaster).FirstOrDefault();

                if (master != null)
                {
                    txtAdultPrice.Text = master.AdultPrice != null ? Convert.ToDecimal(master.AdultPrice).ToString("0.00") : "0.00";
                    txtChildPrice.Text = master.ChildPrice != null ? Convert.ToDecimal(master.ChildPrice).ToString("0.00") : "0.00";
                    txtTaxSettingId.Text = Convert.ToString(master.TaxSettingsId);

                    for (int col = 1; col < tlp_reg_contrl.ColumnCount; col++)
                    {
                        Control c1 = tlp_reg_contrl.GetControlFromPosition(col, 4);
                        if (c1 != null) tlp_reg_contrl.Controls.Remove(c1);

                        Control c2 = tlp_reg_contrl.GetControlFromPosition(col, 5);
                        if (c2 != null) tlp_reg_contrl.Controls.Remove(c2);
                    }

                    int rowLabel = 4;
                    int rowTextBox = 5;

                    tlp_adult_child_label.Dock = DockStyle.Fill;
                    tlp_adult_child_text.Dock = DockStyle.Fill;

                    lbltotalguest.Dock = DockStyle.Fill;
                    txttotalguest.Dock = DockStyle.Fill;

                    lbltotalguest.Visible = true;
                    txttotalguest.Visible = true;
                    tlp_adult_child_label.Visible = true;
                    tlp_adult_child_text.Visible = true;
                    lblpackname.Visible = true;
                    lblpacklabel.Visible = true;

                    if ((master.ChildPrice > 0) && (master.AdultPrice > 0))
                    {

                        tlp_reg_contrl.Controls.Add(tlp_adult_child_label, 1, rowLabel);
                        tlp_reg_contrl.Controls.Add(tlp_adult_child_text, 1, rowTextBox);

                        tlp_reg_contrl.Controls.Add(lbltotalguest, 2, rowLabel);
                        tlp_reg_contrl.Controls.Add(txttotalguest, 2, rowTextBox);

                        lbltotalguest.Visible = true;
                        txttotalguest.Visible = true;
                        tlp_adult_child_label.Visible = true;
                        tlp_adult_child_text.Visible = true;
                        lblpackname.Visible = true;
                        lblpacklabel.Visible = true;
                    }
                    else if (master.ChildPrice > 0)
                    {
                        lblnochild.Dock = DockStyle.Fill;
                        txtChild.Dock = DockStyle.Fill;

                        tlp_reg_contrl.Controls.Add(tlp_adult_child_label, 1, rowLabel);
                        tlp_reg_contrl.Controls.Add(tlp_adult_child_text, 1, rowTextBox);

                        tlp_reg_contrl.Controls.Add(lbltotalguest, 2, rowLabel);
                        tlp_reg_contrl.Controls.Add(txttotalguest, 2, rowTextBox);

                        txtChild.Visible = true;
                        lblnochild.Visible = true;

                        txtAdults.Visible = false;
                        lbladult.Visible = false;

                    }
                    else
                    {
                        tlp_reg_contrl.Controls.Add(tlp_adult_child_label, 1, rowLabel);
                        tlp_reg_contrl.Controls.Add(tlp_adult_child_text, 1, rowTextBox);

                        txtChild.Visible = false;
                        lblnochild.Visible = false;

                        txtAdults.Visible = true;
                        lbladult.Visible = true;
                    }

                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }

        }

        private void LoadDefaultPackAndBookingItems(int partyPackId, int bookingId)
        {
            try
            {
                dgv_packenter.Rows.Clear();

                int productSl = 1;
                int giftSl = 1;
                int packSl = 1;

                #region Load Default Pack ,Cake ,Card ,Gift

                BllSearch<DmnPartyPackDetails> bllPack = new BllSearch<DmnPartyPackDetails>();

                DmnSearch searchPack = new DmnSearch
                {
                    Select = "*",
                    Table = "tblPartyPackDetails",
                    WhereNdGroupBy = $"WHERE Status = 1 AND PartyPackId = {partyPackId}"
                };

                var packItems = bllPack.GetSearchData("prdSearch", searchPack);

                foreach (var item in packItems)
                {
                    int rowIndex = dgv_product.Rows.Add();
                    DataGridViewRow row = dgv_product.Rows[rowIndex];

                    row.Cells[pslno].Value = productSl++;
                    row.Cells[pitemno].Value = item.Item;
                    row.Cells[pqtyno].Value = item.Quantity;

                    row.Cells[ppridno].Value = item.Productid;
                    row.Cells[pproducttype].Value = item.ProductType;

                    row.Cells[proPartyPackDetailsId].Value = item.PartyPackDetailsId;
                    row.Cells[pPartydeafult].Value = 1;

                    if (item.MenuCategoryId != null && Convert.ToInt32(item.MenuCategoryId) > 0)
                    {
                        string categoryName = GetCategoryName(Convert.ToInt32(item.MenuCategoryId));
                        row.Cells[pcategmenuno].Value = categoryName;
                    }
                    else
                    {
                        row.Cells[pcategmenuno].Value = "None";
                    }

                    row.Cells[PPackProductTypeno].Value = item.PackItemType;

                    row.ReadOnly = true;
                    row.DefaultCellStyle.BackColor = Color.Gainsboro;
                    row.DefaultCellStyle.ForeColor = Color.Black;


                    DataGridViewImageCell cellcolumntype =(DataGridViewImageCell)dgv_product.Rows[rowIndex].Cells[pactionno];
                    cellcolumntype.Value = Blankimage;
                    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }

                #endregion

                #region Load Custom Product, card , Gifts
                BllSearch<DmnPartyBookingDetails> bllBooking = new BllSearch<DmnPartyBookingDetails>();
                DmnSearch searchBooking = new DmnSearch
                {
                    Select = "*",
                    Table = "tblPartyBookingDetails",
                    WhereNdGroupBy = $"WHERE PartyBookingId = {bookingId} AND Status = 1"
                };

                var bookingItems = bllBooking.GetSearchData("prdSearch", searchBooking);
                if (bookingItems.Count > 0)
                {
                    foreach (var item in bookingItems)
                    {
                        #region INSERT PARTY PACK AS DETAILS
                        if (item.IsPackage)
                        {
                            txttax.Text = item.Tax;
                            txtTaxtype_pack.Text = item.TaxType;
                            txtTaxPer_pack.Text = item.TaxPer.ToString();
                            txtTotalTaxAmount_pack.Text = item.TaxAmount.ToString();
                            txtPreTaxAmount_pack.Text = item.PreTaxAmount.ToString();
                            txtGrandTotal_pack.Text = item.NetAmount.ToString();
                            txtdeafultpackid.Text = item.PartyBookingDetailsId.ToString();
                            Partypacksaledetailid = item.SalesDetailsId;
                            SalesMasterId = item.SalesMasterId;
                        }
                        #endregion

                        #region CARD CAKE GIFT
                        if (item.Status == 1 &&
                           (item.ProductType == (int)PackItemType.Cake ||
                            item.ProductType == (int)PackItemType.Card ||
                            item.ProductType == (int)PackItemType.Gift))
                        {
                            int rowIndex = dgv_product.Rows.Add();

                            var row = dgv_product.Rows[rowIndex];

                            row.Cells[pslno].Value = productSl++;
                            row.Cells[proPartyPackDetailsId].Value = item.PartyBookingDetailsId;

                            row.Cells[pitemno].Value = item.Product;
                            row.Cells[pqtyno].Value = item.Qty;
                            row.Cells[pPrice].Value = item.Price;
                            row.Cells[pcategmenuno].Value = item.CategoryName;
                            row.Cells[pAmount].Value = item.Amount;

                            row.Cells[pProductid].Value = item.ProductId;
                            row.Cells[ppridno].Value = item.Prid;
                            row.Cells[pproducttype].Value = item.ProductType;

                            // 🔹 Tax
                            row.Cells[ptax].Value = item.Tax;
                            row.Cells[ptaxtype].Value = item.TaxType;
                            row.Cells[ptaxper].Value = item.TaxPer;
                            row.Cells[ptaxamt].Value = item.TaxAmount;
                            row.Cells[pigstamt].Value = item.IGSTAmt;
                            row.Cells[pvatamt].Value = item.VATAmt;
                            row.Cells[ptaxsettingid].Value = item.TaxSettingId;

                            // 🔹 Amounts
                            row.Cells[ppretaxamt].Value = item.PreTaxAmount;
                            row.Cells[pnetamt].Value = item.NetAmount;

                            // 🔹 Discount
                            row.Cells[pdiscper].Value = item.DiscountPer;
                            row.Cells[pdisamt].Value = item.DiscountAmount;

                            // 🔹 Cake special fields
                            if (item.ProductType == (int)PackItemType.Cake)
                            {
                                row.Cells[premarksno].Value = item.CakeRemarks;
                                row.Cells[pmessageno].Value = item.CakeMessage;
                            }

                            row.Cells[pPartydeafult].Value = 0;
                            row.Cells[PPackProductTypeno].Value = item.ProductType;

                            row.Cells[psaledetailidno].Value = item.SalesDetailsId;
                            row.Cells[psalemasteridno].Value = item.SalesMasterId;

                            DataGridViewImageCell cell =(DataGridViewImageCell)row.Cells[pactionno];
                            cell.Value = Grid_Remove;
                            cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        }

                        #endregion  

                        //#region Food Pack
                        //else if (item.ProductType == (int)PackItemType.Pack && item.Status == 1 && !item.IsPackage)
                        //{
                        //    int rowIndex = dgv_product.Rows.Add();
                        //    var row = dgv_product.Rows[rowIndex];

                        //    row.Cells[pslno].Value = rowIndex + 1;                        
                        //    row.Cells[pitemno].Value = item.Product;                      
                        //    row.Cells[pqtyno].Value = item.Qty;                            

                        //    row.Cells[pProductid].Value = item.ProductId;                 
                        //    row.Cells[ppretaxamt].Value = 0;                              
                        //    row.Cells[pnetamt].Value = 0;                               
                        //    row.Cells[premarksno].Value = "";                          
                        //    row.Cells[pmessageno].Value = "";                          
                        //    row.Cells[PPackProductTypeno].Value = (int)PackItemType.Pack;  
                        //    row.Cells[pdiscper].Value = 0;                                 
                        //    row.Cells[pdisamt].Value = 0;                                  
                        //    row.Cells[ptax].Value = "";                                    
                        //    row.Cells[ptaxtype].Value = "";                                 
                        //    row.Cells[ptaxper].Value = 0;                                   
                        //    row.Cells[ptaxamt].Value = 0;                                   
                        //    row.Cells[pigstamt].Value = 0;                                
                        //    row.Cells[pvatamt].Value = 0;                                   
                        //    row.Cells[ptaxsettingid].Value = 0;                            

                        //    DataGridViewImageCell cell = (DataGridViewImageCell)row.Cells[pactionno];
                        //    cell.Value = Grid_Remove;
                        //    cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        //}
                        //#endregion
                    }
                }
              
                #endregion
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                foreach (TextBox textBox in new TextBox[] { txtdiscountreason, txteventdesc, txtemail, txtph, txtname, txtRcvAmt, txtTotalDiscountAmt, txtTotalTaxAmount, txtaddress, txtChild, txtAdults, txttotalguest, txtRenote, txtrefundamt, txtcakeremarks, txtcakemessage, txtcakeshape, txtcakesize, txtcakeprice, txtextracharge_des, txtextracharge_amt, txtaddcharge })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    //textBox.Font = new Font(textBox.Font.FontFamily, 12);
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

        #region Split Payment
        private void btnPartRefresh_Click(object sender, EventArgs e)
        {
            lblpartpayment1.Text = "Payment 1";
            lblpartAmt2.Text = "Payment 2";
            lblpartAmt1.Text = "0.00";
            lblpartAmt2.Text = "0.00";
            lblPartBalAmt.Text = lblPartNetAmt.Text;
            btnPartPaynow.Visible = false;
            cash_part.Enabled = true;
            Debit_part.Enabled = true;
            credit_part.Enabled = true;
            netbanking_part.Enabled = true;
            midas_part.Enabled = true;
            upi_part.Enabled = true;
        }
        private void btnPartPaynow_Click(object sender, EventArgs e)
        {
            Paybill();
        }

        private void btnPartClose_Click(object sender, EventArgs e)
        {
            pnlSplit.Visible = false;
        }
        private void cash_part_Click(object sender, EventArgs e)
        {
            try
            {
                partCount = Convert.ToInt32(partPayments.Count);
                decimal bal = Convert.ToDecimal(lblPartBalAmt.Text);
                if (partCount == 0)
                {
                    decimal kdata = 0;
                    using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
                    {
                        frm.ShowDialog();
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                        frm.Dispose();
                    }

                    if (kdata >= bal || kdata <= 0)
                    {
                        if (exitstatus == false)
                            CMessageBox.Show("Amount should not be greater than TotalAmount.\nCURRENT BALANCE: " + bal, "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        lblpartAmt1.Text = "0.00";
                    }
                    else
                    {
                        txtPayid1.Text = Convert.ToInt32(PaymentType.CASH).ToString();
                        lblpartpayment1.Text = "CASH";
                        lblpartAmt1.Text = kdata.ToString("0.##");
                        partPayments.Add((txtPayid1.Text, lblpartAmt1.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt1.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }
                }
                else if (partCount == 1)
                {
                    if (txtPayid1.Text != Convert.ToInt32(PaymentType.CASH).ToString())
                    {
                        txtPayid2.Text = Convert.ToInt32(PaymentType.CASH).ToString();
                        lblpartAmt2.Text = "CASH";
                        lblpartAmt2.Text = bal.ToString("0.##");
                        partPayments.Add((txtPayid2.Text, lblpartAmt2.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt2.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }
                    else
                    {
                        CMessageBox.Show("Similar Paymode Twice Not Valid", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        lblpartAmt2.Text = "Payment 2";
                    }


                }
                if (string.IsNullOrWhiteSpace(lblPartBalAmt.Text) || lblPartBalAmt.Text == "0.00")
                {
                    if (partPayments.Count == 2)
                    {
                        btnPartPaynow.Visible = true;
                        cash_part.Enabled = false;
                        Debit_part.Enabled = false;
                        credit_part.Enabled = false;
                        netbanking_part.Enabled = false;
                        midas_part.Enabled = false;
                        upi_part.Enabled = false;
                    }

                }
            }

            catch (Exception ex)
            {
                clsgen.LogException("POSDashboard page cash_part_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
        private void Debit_part_Click(object sender, EventArgs e)
        {
            try
            {
                partCount = Convert.ToInt32(partPayments.Count);
                decimal bal = Convert.ToDecimal(lblPartBalAmt.Text);
                if (partCount == 0)
                {
                    decimal kdata = 0;
                    using (FoodandBeverage.Forms.UserControl.Frmnumdiskeypad frm = new FoodandBeverage.Forms.UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
                    {
                        frm.ShowDialog();
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                        frm.Dispose();
                    }

                    if (kdata >= bal || kdata <= 0)
                    {
                        if (exitstatus == false)
                            CMessageBox.Show(
                                            "Amount should not be greater than TotalAmount.\nCURRENT BALANCE: " + bal,
                                            "Warning",
                                            MessageBoxButtons.RetryCancel,
                                            MessageBoxIcon.Warning
);
                        lblpartAmt1.Text = "0.00";
                    }
                    else
                    {
                        txtPayid1.Text = Convert.ToInt32(PaymentType.DEBITCARD).ToString();
                        lblpartpayment1.Text = "DEBITCARD";
                        lblpartAmt1.Text = kdata.ToString("0.##");
                        partPayments.Add((txtPayid1.Text, lblpartAmt1.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt1.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }



                }
                else if (partCount == 1)
                {
                    if (txtPayid1.Text != Convert.ToInt32(PaymentType.DEBITCARD).ToString())
                    {

                        txtPayid2.Text = Convert.ToInt32(PaymentType.DEBITCARD).ToString();
                        lblpartpayment2.Text = "DEBITCARD";
                        lblpartAmt2.Text = bal.ToString("0.##");
                        partPayments.Add((txtPayid2.Text, lblpartAmt2.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt2.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }
                    else
                    {
                        CMessageBox.Show("Similar Paymode Twice Not Valid", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        lblpartAmt2.Text = "Payment 2";
                    }
                }

                if (string.IsNullOrWhiteSpace(lblPartBalAmt.Text) || lblPartBalAmt.Text == "0.00")
                {
                    if (partPayments.Count == 2)
                    {
                        btnPartPaynow.Visible = true;
                        cash_part.Enabled = false;
                        Debit_part.Enabled = false;
                        credit_part.Enabled = false;
                        netbanking_part.Enabled = false;
                        midas_part.Enabled = false;
                        upi_part.Enabled = false;
                    }

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("POSDashboard page Debit_part_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
        private void upi_part_Click(object sender, EventArgs e)
        {
            try
            {
                partCount = Convert.ToInt32(partPayments.Count);
                decimal bal = Convert.ToDecimal(lblPartBalAmt.Text);
                if (partCount == 0)
                {

                    decimal kdata = 0;
                    using (FoodandBeverage.Forms.UserControl.Frmnumdiskeypad frm = new FoodandBeverage.Forms.UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
                    {
                        frm.ShowDialog();
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                        frm.Dispose();
                    }

                    if (kdata >= bal || kdata <= 0)
                    {
                        if (exitstatus == false)
                            CMessageBox.Show(
                                            "Amount should not be greater than TotalAmount.\nCURRENT BALANCE: " + bal,
                                            "Warning",
                                            MessageBoxButtons.RetryCancel,
                                            MessageBoxIcon.Warning
                                        );
                        lblpartAmt1.Text = "0.00";
                    }
                    else
                    {
                        txtPayid1.Text = Convert.ToInt32(PaymentType.UPI).ToString();
                        lblpartpayment1.Text = "UPI";
                        lblpartAmt1.Text = kdata.ToString("0.##");
                        partPayments.Add((txtPayid1.Text, lblpartAmt1.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt1.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }



                }
                else if (partCount == 1)
                {
                    if (txtPayid1.Text != Convert.ToInt32(PaymentType.UPI).ToString())
                    {

                        txtPayid2.Text = Convert.ToInt32(PaymentType.UPI).ToString();
                        lblpartpayment2.Text = "UPI";
                        lblpartAmt2.Text = bal.ToString("0.##");
                        partPayments.Add((txtPayid2.Text, lblpartAmt2.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt2.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }
                    else
                    {
                        CMessageBox.Show("Similar Paymode Twice Not Valid", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        lblpartAmt2.Text = "Payment 2";
                    }

                }
                if (string.IsNullOrWhiteSpace(lblPartBalAmt.Text) || lblPartBalAmt.Text == "0.00")
                {
                    if (partPayments.Count == 2)
                    {
                        btnPartPaynow.Visible = true;
                        cash_part.Enabled = false;
                        Debit_part.Enabled = false;
                        credit_part.Enabled = false;
                        netbanking_part.Enabled = false;
                        midas_part.Enabled = false;
                        upi_part.Enabled = false;
                    }


                }
            }

            catch (Exception ex)
            {
                clsgen.LogException("POSDashboard page upi_part_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
        private void netbanking_part_Click(object sender, EventArgs e)
        {
            try
            {
                partCount = Convert.ToInt32(partPayments.Count);
                decimal bal = Convert.ToDecimal(lblPartBalAmt.Text);
                if (partCount == 0)
                {
                    decimal kdata = 0;
                    using (FoodandBeverage.Forms.UserControl.Frmnumdiskeypad frm = new FoodandBeverage.Forms.UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
                    {
                        frm.ShowDialog();
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                        frm.Dispose();
                    }
                    if (kdata >= bal || kdata <= 0)
                    {
                        if (exitstatus == false)
                            CMessageBox.Show(
                                            "Amount should not be greater than TotalAmount.\nCURRENT BALANCE: " + bal,
                                            "Warning",
                                            MessageBoxButtons.RetryCancel,
                                            MessageBoxIcon.Warning
                                        );
                        lblpartAmt1.Text = "0.00";
                    }
                    else
                    {
                        txtPayid1.Text = Convert.ToInt32(PaymentType.BANKINGONLINE).ToString();
                        lblpartpayment1.Text = "NETBANKING";
                        lblpartAmt1.Text = kdata.ToString("0.##");
                        partPayments.Add((txtPayid1.Text, lblpartAmt1.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt1.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }
                }
                else if (partCount == 1)
                {
                    if (txtPayid1.Text != Convert.ToInt32(PaymentType.BANKINGONLINE).ToString())
                    {

                        txtPayid2.Text = Convert.ToInt32(PaymentType.BANKINGONLINE).ToString();
                        lblpartpayment2.Text = "NETBANKING";
                        lblpartAmt2.Text = bal.ToString("0.##");
                        partPayments.Add((txtPayid2.Text, lblpartAmt2.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt2.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }
                    else
                    {
                        CMessageBox.Show("Similar Paymode Twice Not Valid", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        lblpartAmt2.Text = "Payment 2";
                    }

                }
                if (string.IsNullOrWhiteSpace(lblPartBalAmt.Text) || lblPartBalAmt.Text == "0.00")
                {
                    if (partPayments.Count == 2)
                    {
                        btnPartPaynow.Visible = true;
                        cash_part.Enabled = false;
                        Debit_part.Enabled = false;
                        credit_part.Enabled = false;
                        netbanking_part.Enabled = false;
                        midas_part.Enabled = false;
                        upi_part.Enabled = false;
                    }

                }
            }

            catch (Exception ex)
            {
                clsgen.LogException("POSDashboard page netbanking_part_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
        private void credit_part_Click(object sender, EventArgs e)
        {
            try
            {
                partCount = Convert.ToInt32(partPayments.Count);
                decimal bal = Convert.ToDecimal(lblPartBalAmt.Text);
                if (partCount == 0)
                {

                    decimal kdata = 0;
                    using (FoodandBeverage.Forms.UserControl.Frmnumdiskeypad frm = new FoodandBeverage.Forms.UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
                    {
                        frm.ShowDialog();
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                        frm.Dispose();
                    }
                    if (kdata >= bal || kdata <= 0)
                    {
                        if (exitstatus == false)
                            CMessageBox.Show(
                                        "Amount should not be greater than TotalAmount.\nCURRENT BALANCE: " + bal,
                                        "Warning",
                                        MessageBoxButtons.RetryCancel,
                                        MessageBoxIcon.Warning
                                    );
                        lblpartAmt1.Text = "0.00";
                    }
                    else
                    {
                        txtPayid1.Text = Convert.ToInt32(PaymentType.CREDITCARD).ToString();
                        lblpartpayment1.Text = "CREDITCARD";
                        lblpartAmt1.Text = kdata.ToString("0.##");
                        partPayments.Add((txtPayid1.Text, lblpartAmt1.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt1.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }

                }

                else if (partCount == 1)
                {
                    if (txtPayid1.Text != Convert.ToInt32(PaymentType.CREDITCARD).ToString())
                    {
                        txtPayid2.Text = Convert.ToInt32(PaymentType.CREDITCARD).ToString();
                        lblpartpayment2.Text = "CREDITCARD";
                        lblpartAmt2.Text = bal.ToString("0.##");
                        partPayments.Add((txtPayid2.Text, lblpartAmt2.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt2.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }
                    else
                    {
                        CMessageBox.Show("Similar Paymode Twice Not Valid", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        lblpartAmt2.Text = "Payment 2";
                    }

                }
                if (string.IsNullOrWhiteSpace(lblPartBalAmt.Text) || lblPartBalAmt.Text == "0.00")
                {
                    if (partPayments.Count == 2)
                    {
                        btnPartPaynow.Visible = true;
                        cash_part.Enabled = false;
                        Debit_part.Enabled = false;
                        credit_part.Enabled = false;
                        netbanking_part.Enabled = false;
                        midas_part.Enabled = false;
                        upi_part.Enabled = false;
                    }

                }
            }

            catch (Exception ex)
            {
                clsgen.LogException("POSDashboard page credit_part_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }

        private void midas_part_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Refund And Cancel booking
        public void TogglePayCancel(int showCancel)
        {
            if (showCancel == 1)
            {
                btncancelbooking.Visible = true;

                tlp_pay_cancel.ColumnStyles[0].SizeType = SizeType.Percent;
                tlp_pay_cancel.ColumnStyles[0].Width = 50;
                tlp_pay_cancel.ColumnStyles[3].SizeType = SizeType.Percent;
                tlp_pay_cancel.ColumnStyles[3].Width = 50;
            }
            else
            {
                btncancelbooking.Visible = false;

                tlp_pay_cancel.ColumnStyles[0].SizeType = SizeType.Percent;
                tlp_pay_cancel.ColumnStyles[0].Width = 50;
                tlp_pay_cancel.ColumnStyles[3].SizeType = SizeType.Percent;
                tlp_pay_cancel.ColumnStyles[3].Width = 50;
            }
        }

        private void btncancelbooking_Click(object sender, EventArgs e)
        {
            try
            {
                decimal totalPaid = GetTotalPaidAmount(BookingId);
                lbltotalcancelamt.Text = totalPaid.ToString("N2");
                pnl_bookingcancel.Visible = true;
                pnl_bookingcancel.BringToFront();
                pnl_bookingcancel.Left = (this.ClientSize.Width - pnl_bookingcancel.Width) / 2;
                pnl_bookingcancel.Top = (this.ClientSize.Height - pnl_bookingcancel.Height) / 2;
            }
            catch(Exception ex)
            {

            }
        }
        private void btncls_bookingcancel_Click(object sender, EventArgs e)
        {
            pnl_bookingcancel.Visible = false;
        }

        private void btnrefundfull_Click(object sender, EventArgs e)
        {
            txtrefundamt.Text = lbltotalcancelamt.Text;
        }

        private void txtrefundamt_Click(object sender, EventArgs e)
        {
            try
            {
                decimal typedAmount = 0;
                using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.percentage)))
                {
                    frm.ShowDialog();

                    if (!string.IsNullOrEmpty(frm.txtkeypadvalue.Text))
                        typedAmount = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                }

                txtrefundamt.Text = typedAmount.ToString("N2");
            }
            catch(Exception ex)
            {
            }
        }

        private void ProcessPartyBookingRefund(int  refundMode)
        {
            try
            {
                DmnRefund dmnRefund = new DmnRefund();
                BllRefund bllRefund = new BllRefund();

                dmnRefund.PartyBookingId = BookingId;
                dmnRefund.RefundAmount = Convert.ToDecimal(txtrefundamt.Text);
                dmnRefund.RefundMode = refundMode;
                dmnRefund.RefundReason = 5;
                dmnRefund.ReasonNote = txtRenote.Text;
                dmnRefund.SalesMasterId = SalesMasterId;

                dmnRefund.CreatedBy = FrmDashboard.UserId;
                dmnRefund.BranchId = FrmDashboard.BranchId;
                dmnRefund.Status = 1;
                dmnRefund.DBname = GameDB;
                dmnRefund.CreatedDateTime = DateTime.Now;

                var data = bllRefund.AddRefundBooking("prdInsertRefund_PartyBooking", dmnRefund);

                if (data.Id > 0)
                {
                    pnl_bookingcancel.Visible = false;
                    ClearAll();
                    CMessageBox.Show("Refund Amount Completed","Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
            }
        }

        private void btncash_refund_Click(object sender, EventArgs e)
        {
            ProcessPartyBookingRefund(Convert.ToInt32(PaymentType.CASH));
        }

        private void btnupi_refund_Click(object sender, EventArgs e)
        {
            ProcessPartyBookingRefund(Convert.ToInt32(PaymentType.UPI));
        }

        private void btndebit_refund_Click(object sender, EventArgs e)
        {
            ProcessPartyBookingRefund(Convert.ToInt32(PaymentType.DEBITCARD));
        }

        private void btninterbanking_refund_Click(object sender, EventArgs e)
        {
            ProcessPartyBookingRefund(Convert.ToInt32(PaymentType.BANKINGONLINE));
        }

        private void btncredit_refund_Click(object sender, EventArgs e)
        {
            ProcessPartyBookingRefund(Convert.ToInt32(PaymentType.CREDITCARD));
        }

        private void btncakereg_cls_Click(object sender, EventArgs e)
        {
            pnl_cakeregister.Visible = false;
        }

        private void btn_bookingvoid_Click(object sender, EventArgs e)
        {
            try
            {
                BllPartyBooking bll = new BllPartyBooking();
                DmnPartyBooking booking = new DmnPartyBooking();

                booking.PartyBookingId = GetInt(txtpartybookingid);
                booking.CreatedBy = FrmLogin.EmployeeId;
                booking.BranchId = FrmDashboard.BranchId;
                booking.Status = 0;

                var result = bll.VoidData("prdVoid_PartyBooking", booking);

                if (result.PartyBookingId > 0)
                {
                    pnl_bookingcancel.Visible = false;
                    CMessageBox.Show("Booking Voided Successfully");
                    ClearAll();
                }
                else
                {
                    CMessageBox.Show("Void Failed");
                }
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region Main Datagridview 
        private void dgv_product_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgv_product.CurrentCell.ColumnIndex == pqtyno) // your quantity column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    // Remove previous handler to avoid multiple subscriptions
                    tb.KeyPress -= QuantityColumn_KeyPress;
                    tb.KeyPress += QuantityColumn_KeyPress;
                }
            }
        }

        private void QuantityColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (like backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void dgv_product_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgv_product.CurrentCell == null) return;

            // Allow only decimal input in Qty column
            int qtyCol = pqtyno; // Replace with your qty column index
            if (dgv_product.CurrentCell.ColumnIndex == qtyCol)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                // Only allow one decimal point
                if (e.KeyChar == '.' && dgv_product.CurrentCell.Value != null && dgv_product.CurrentCell.Value.ToString().Contains("."))
                {
                    e.Handled = true;
                }
            }
        }
        private void dgv_product_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int actionCol = pactionno;

            if (e.ColumnIndex != actionCol) return;

            var row = dgv_product.Rows[e.RowIndex];

            int defaultType = Convert.ToInt32(row.Cells[pPartydeafult].Value ?? 0);
            string productName = Convert.ToString(row.Cells[pitemno].Value ?? "");

            if (defaultType == 1)
            {
                CMessageBox.Show($"Cannot delete default item '{productName}'!",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int partyPackDetailsId = Convert.ToInt32(row.Cells[proPartyPackDetailsId].Value ?? 0);
            if (partyPackDetailsId > 0)
            {
                var result = CMessageBox.Show(
                    $"Are you sure you want to delete the product '{productName}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

                BookingItemAdjustment item = new BookingItemAdjustment()
                {
                    SlNo = bookingItemAdjustments.Count + 1,
                    Product = productName,
                    Qty = Convert.ToDecimal(row.Cells[pqtyno].Value ?? 0),
                    Price = Convert.ToDecimal(row.Cells[pnetamt].Value ?? 0),
                    Amount = Convert.ToDecimal(row.Cells[pPrice].Value ?? 0),
                    ProductCategory = Convert.ToString(row.Cells[pcategmenuno].Value ?? ""),
                    ProductType = Convert.ToInt32(row.Cells[PPackProductTypeno].Value ?? 0),
                    ProductId = Convert.ToInt32(row.Cells[pProductid].Value ?? 0),
                    SaleDetailId = Convert.ToInt32(row.Cells[psaledetailidno].Value ?? 0),
                    PartyBookingDetailsid = partyPackDetailsId
                };
                bookingItemAdjustments.Add(item);
            }

            dgv_product.Rows.RemoveAt(e.RowIndex);
            CalculateDiscountPer();
            RecalculateAllRowsAndTotals();
        }

        private void dgv_product_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var row = dgv_product.Rows[e.RowIndex];

            if (e.ColumnIndex == pqtyno)
            {
                decimal qty = 0;
                decimal.TryParse(Convert.ToString(row.Cells[pqtyno].Value ?? "0"), out qty);
                row.Cells[pqtyno].Value = Math.Round(qty, 2);

                int productType = Convert.ToInt32(row.Cells[PPackProductTypeno].Value ?? 0);
                int productId = Convert.ToInt32(row.Cells[pProductid].Value ?? 0);

                CalculateAndApplyTax(e.RowIndex, productType, productId);
            }
        }

        #endregion

        #region  Payment Settled

        private void btn_paynow_ref_add_Click(object sender, EventArgs e)
        {
            try
            { 
                pnlPaymode.Visible = true;
                pnlPaymode.BringToFront();
                pnlPaymode.Left = (this.ClientSize.Width - pnlPaymode.Width) / 2;
                pnlPaymode.Top = (this.ClientSize.Height - pnlPaymode.Height) / 2;
            }
            catch (Exception ex)
            {
            }
        }

        private void btn_Paymentsettled_cls_Click(object sender, EventArgs e)
        {
            pnl_Paymentsettled.Visible = false;
        }

        private void btn_ref_add_cancel_Click(object sender, EventArgs e)
        {
            pnl_Paymentsettled.Visible = false;
        }
        private decimal GetAdjustmentTotal()
        {
            decimal total = 0;

            foreach (var item in bookingItemAdjustments)
            {
                total += item.Price;
            }

            return total;
        }
        private void LoadAdjustmentGrid()
        {
            dgv_cancelleditems.Rows.Clear();

            int sl = 1;

            foreach (var item in bookingItemAdjustments)
            {
                dgv_cancelleditems.Rows.Add(
                    sl,
                    item.Product,
                    item.ProductCategory,
                    item.Qty,
                    item.Price
                );

                sl++;
            }
        }

        private void ShowPaymentSettlementPanel(decimal netAmount, decimal advancePaid, decimal receivedNow)
        {
            try
            {
                pnl_Paymentsettled.Visible = true;
                pnl_Paymentsettled.BringToFront();
                pnl_Paymentsettled.Left = (this.ClientSize.Width - pnl_Paymentsettled.Width) / 2;
                pnl_Paymentsettled.Top = (this.ClientSize.Height - pnl_Paymentsettled.Height) / 2;

                lbl_ps_partyname.Text = lblpackname.Text;
                lbl_ps_partydatetime.Text = dtpEventDate.Text + " " + dtpEventTime.Text;
                lbl_ps_phone.Text = txtph.Text;
                lbl_ps_cusname.Text = txtname.Text;
                lbl_ps_eventname.Text = cmbevent.Text;

                if (bookingItemAdjustments.Count > 0)
                    LoadAdjustmentGrid();

                decimal adjAmount = bookingItemAdjustments.Sum(x => x.Price);
                decimal oldNetAmount = netAmount + adjAmount;

                lbl_sum_netamt.Text = oldNetAmount.ToString("0.00");
                lbl_sum_newnetamt.Text = netAmount.ToString("0.00");
                lbl_sum_advamt.Text = advancePaid.ToString("0.00");

                lbl_sum_netamt.ForeColor = Color.Black;
                lbl_sum_newnetamt.ForeColor = Color.Blue;
                lbl_sum_advamt.ForeColor = Color.DarkGreen;

                pnl_ref_ad_msg.Visible = false;

                decimal totalPaid = advancePaid + receivedNow;

                decimal diff = netAmount - totalPaid;

                if (diff < 0)
                    lbl_sum_adjamt.ForeColor = Color.Red;
                else if (diff > 0)
                    lbl_sum_adjamt.ForeColor = Color.Green;
                else
                    lbl_sum_adjamt.ForeColor = Color.Black;

                dgv_cancelleditems.Visible = false;

                if (diff == 0)
                {
                    lbl_paymentsubhead.Text = "Payment Settled";

                    lbl_sum_dueamt.Text = "0.00";
                    lbl_sum_revamt.Text = "0.00";

                    lbl_sum_dueamt.ForeColor = Color.Green;
                    lbl_sum_revamt.ForeColor = Color.Green;

                    pnl_ref_ad_msg.Visible = true;
                    pnl_ref_ad_msg.BackColor = Color.Honeydew;

                    lblrefund_ad_msg.Text = "Payment Settled Successfully";
                    lblrefund_ad_msg.ForeColor = Color.Green;

                    tlp_Paymentsettled_cntrl.Controls.Add(tlp_paymentbalanced, 1, 7);
                    tlp_paymentbalanced.Dock = DockStyle.Fill;
                }
                else if (diff < 0)
                {
                    dgv_cancelleditems.Visible = true;

                    decimal refundAmt = Math.Abs(diff);

                    lbl_paymentsubhead.Text = "Refund Required";

                    lbl_sum_adjamt.Text = diff.ToString("0.00");

                    lbl_sum_revamt.Text = receivedNow.ToString("0.00");
                    lbl_sum_dueamt.Text = "0.00";

                    lbl_sum_revamt.ForeColor = Color.Red;

                    pnl_ref_ad_msg.Visible = true;
                    pnl_ref_ad_msg.BackColor = Color.MistyRose;

                    lblrefund_ad_msg.Text = $"Refund ₹ {refundAmt:0.00} to customer.";
                    lblrefund_ad_msg.ForeColor = Color.Red;

                    tlp_Paymentsettled_cntrl.Controls.Add(pnl_ref_ad_msg, 1, 7);
                    pnl_ref_ad_msg.Dock = DockStyle.Fill;
                }
                else
                {
                    dgv_cancelleditems.Visible = false;

                    decimal dueAmt = Math.Abs(diff);

                    lbl_sum_adjamt.Text = "+"+dueAmt.ToString("0.00");

                    lbl_paymentsubhead.Text = "Additional Payment Required";

                    lbl_sum_dueamt.Text =  receivedNow.ToString("0.00");
                    lbl_sum_revamt.Text = "0.00";

                    lbl_sum_dueamt.ForeColor = Color.DarkOrange;

                    pnl_ref_ad_msg.Visible = true;
                    pnl_ref_ad_msg.BackColor = Color.LemonChiffon;

                    lblrefund_ad_msg.Text = $"₹ {dueAmt:0.00} due from customer.";
                    lblrefund_ad_msg.ForeColor = Color.DarkOrange;
                    lbl_sum_dueamt.Text= dueAmt.ToString("0.00");

                    tlp_Paymentsettled_cntrl.Controls.Add(pnl_ref_ad_msg, 1, 7);
                    pnl_ref_ad_msg.Dock = DockStyle.Fill;
                }

            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Net Amount And Tax Calculation

        private void btnadd_cus_Click(object sender, EventArgs e)
        {
            cmbevent.SelectedIndex = 0;
            txteventdesc.Text = "Birthday Party";
            txtaddress.Text = "Chalakudy, Kerala";
            txtemail.Text = "Rahul@gmail.com";
            txtph.Text = "9876543210";
            txtname.Text = "Rahul";
        }
        private void CalculateAndApplyTax(int rowIndex, int productType, int productId)
        {
            try
            {
                var row = dgv_product.Rows[rowIndex];
                if (row.IsNewRow) return;

                decimal qty = Convert.ToDecimal(row.Cells[pqtyno].Value ?? 0);
                decimal price = Convert.ToDecimal(row.Cells[pPrice].Value ?? 0);
                decimal discountPer = Convert.ToDecimal(row.Cells[pdiscper].Value ?? 0);
                int taxSettingId = Convert.ToInt32(row.Cells[ptaxsettingid].Value ?? 0);
                string taxMode = Convert.ToString(row.Cells[ptax].Value ?? "");

                decimal igst = 0, cgst = 0, sgst = 0, vat = 0;
                decimal totalPercent = 0;
                string taxType = "";

                string tableName = (productType == (int)PackItemType.Card ||
                                    productType == (int)PackItemType.Gift)
                                    ? $"[{GameDB}].dbo.tblTaxSettings"
                                    : "tblTaxSettings";

                if (taxSettingId > 0)
                {
                    BllSearch<DmnTaxSettings> _bllTax = new BllSearch<DmnTaxSettings>();
                    DmnSearch dmnTax = new DmnSearch
                    {
                        Select = "TaxType, IgstPrcnt, CgstPrcnt, SgstPrcnt, VatPrcnt",
                        Table = tableName,
                        WhereNdGroupBy = $"WHERE Status=1 AND TaxSettingsId={taxSettingId}"
                    };

                    var taxSettings = _bllTax.GetSearchData("prdSearch", dmnTax);

                    if (taxSettings.Count > 0)
                    {
                        igst = taxSettings[0].IgstPrcnt;
                        cgst = taxSettings[0].CgstPrcnt;
                        sgst = taxSettings[0].SgstPrcnt;
                        vat = taxSettings[0].VatPrcnt;

                        taxType = taxSettings[0].TaxType;
                        totalPercent = (taxType == "GST") ? igst : vat;
                    }
                }

                decimal gross = qty * price;
                decimal discountAmt = gross * discountPer / 100;
                decimal amountAfterDiscount = gross - discountAmt;

                decimal baseAmt = 0, taxAmt = 0, netAmt = 0;

                if (taxMode == "Inclusive")
                {
                    baseAmt = amountAfterDiscount / (1 + (totalPercent / 100));
                    taxAmt = amountAfterDiscount - baseAmt;
                    netAmt = amountAfterDiscount;
                }
                else
                {
                    baseAmt = amountAfterDiscount;
                    taxAmt = amountAfterDiscount * totalPercent / 100;
                    netAmt = amountAfterDiscount + taxAmt;
                }

                row.Cells[pAmount].Value = gross.ToString("N2");
                row.Cells[pdisamt].Value = discountAmt.ToString("N2"); 
                row.Cells[ppretaxamt].Value = baseAmt.ToString("N2"); 
                row.Cells[ptaxamt].Value = taxAmt.ToString("N2"); 
                row.Cells[pnetamt].Value = netAmt.ToString("N2");
                row.Cells[ptaxtype].Value = taxType;
                row.Cells[ptaxper].Value = totalPercent.ToString("N2"); 

                if (taxType == "GST")
                {
                    if (totalPercent > 0)
                        row.Cells[pigstamt].Value = (taxAmt * igst / totalPercent).ToString("N2"); 
                    else
                        row.Cells[pigstamt].Value = (cgst + sgst).ToString("N2"); 

                    row.Cells[pvatamt].Value = 0;
                }
                else
                {
                    row.Cells[pvatamt].Value = taxAmt.ToString("N2"); ;
                    row.Cells[pigstamt].Value = 0;
                }

                switch ((PackItemType)productType)
                {
                    case PackItemType.Pack:
                        txtPreTaxAmount_pack.Text = baseAmt.ToString("N2");
                        txtTotalTaxAmount_pack.Text = taxAmt.ToString("N2");
                        txtVatamt_pack.Text = (taxType != "GST" ? taxAmt : 0).ToString("N2");
                        txtIGSTAmt_pack.Text = (taxType == "GST" ? (taxAmt * igst / totalPercent) : 0).ToString("N2");
                        txtGrandTotal_pack.Text = netAmt.ToString("N2");
                        break;

                    case PackItemType.Cake:
                        txtPreTaxAmount_cake.Text = baseAmt.ToString("N2");
                        txtTotalTaxAmount_cake.Text = taxAmt.ToString("N2");
                        txtVATAmt_cake.Text = (taxType != "GST" ? taxAmt : 0).ToString("N2");
                        txtIGSTAmt_cake.Text = (taxType == "GST" ? (taxAmt * igst / totalPercent) : 0).ToString("N2");
                        txtCakeGrandTotal.Text = netAmt.ToString("N2");
                        break;

                    case PackItemType.Card:
                        txtPreTaxAmount_card.Text = baseAmt.ToString("N2");
                        txtTotalTaxAmount_card.Text = taxAmt.ToString("N2");
                        txtVATAmt_card.Text = (taxType != "GST" ? taxAmt : 0).ToString("N2");
                        txtIGSTAmt_card.Text = (taxType == "GST" ? (taxAmt * igst / totalPercent) : 0).ToString("N2");
                        txtCardGrandTotal.Text = netAmt.ToString("N2");
                        break;

                    case PackItemType.Gift:
                        txtPreTaxAmount_gift.Text = baseAmt.ToString("N2");
                        txtTotalTaxAmount_gift.Text = taxAmt.ToString("N2");
                        txtVATAmt_gift.Text = (taxType != "GST" ? taxAmt : 0).ToString("N2");
                        txtIGSTAmt_gift.Text = (taxType == "GST" ? (taxAmt * igst / totalPercent) : 0).ToString("N2");
                        txtGiftGrandTotal.Text = netAmt.ToString("N2");
                        break;
                }

                CalculateDiscountPer();
                CalculateNetAmount();

            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        private void RecalculateAllRowsAndTotals()
        {
            try
            {
                decimal preTax_card = 0, tax_card = 0, net_card = 0;
                decimal preTax_cake = 0, tax_cake = 0, net_cake = 0;
                decimal preTax_gift = 0, tax_gift = 0, net_gift = 0;
                decimal preTax_pack = 0, tax_pack = 0, net_pack = 0;

                foreach (DataGridViewRow row in dgv_product.Rows)
                {
                    if (row.IsNewRow) continue;

                    int productType = Convert.ToInt32(row.Cells[PPackProductTypeno].Value ?? 0);
                    decimal qty = Convert.ToDecimal(row.Cells[pqtyno].Value ?? 0);
                    decimal price = Convert.ToDecimal(row.Cells[pPrice].Value ?? 0);
                    decimal discountPer = Convert.ToDecimal(row.Cells[pdiscper].Value ?? 0);
                    int taxSettingId = Convert.ToInt32(row.Cells[ptaxsettingid].Value ?? 0);
                    string taxMode = Convert.ToString(row.Cells[ptax].Value ?? "");

                    decimal igst = 0, cgst = 0, sgst = 0, vat = 0;
                    decimal totalPercent = 0;
                    string taxType = "";

                    if (taxSettingId > 0)
                    {
                        BllSearch<DmnTaxSettings> _bllTax = new BllSearch<DmnTaxSettings>();
                        DmnSearch dmnTax = new DmnSearch
                        {
                            Select = "TaxType, IgstPrcnt, CgstPrcnt, SgstPrcnt, VatPrcnt",
                            Table = $"[{GameDB}].dbo.tblTaxSettings",
                            WhereNdGroupBy = $"WHERE Status=1 AND TaxSettingsId={taxSettingId}"
                        };

                        var taxSettings = _bllTax.GetSearchData("prdSearch", dmnTax);

                        if (taxSettings.Count > 0)
                        {
                            igst = taxSettings[0].IgstPrcnt;
                            cgst = taxSettings[0].CgstPrcnt;
                            sgst = taxSettings[0].SgstPrcnt;
                            vat = taxSettings[0].VatPrcnt;

                            taxType = taxSettings[0].TaxType;
                            totalPercent = (taxType == "GST") ? igst : vat;
                        }
                    }

                    // Calculate amounts
                    decimal gross = qty * price;
                    decimal discountAmt = gross * discountPer / 100;
                    decimal amountAfterDiscount = gross - discountAmt;
                    decimal baseAmt = 0, taxAmt = 0, netAmt = 0;

                    if (taxMode == "Inclusive")
                    {
                        baseAmt = amountAfterDiscount / (1 + (totalPercent / 100));
                        taxAmt = amountAfterDiscount - baseAmt;
                        netAmt = amountAfterDiscount;
                    }
                    else
                    {
                        baseAmt = amountAfterDiscount;
                        taxAmt = amountAfterDiscount * totalPercent / 100;
                        netAmt = amountAfterDiscount + taxAmt;
                    }


                    row.Cells[pAmount].Value = gross;
                    row.Cells[pdisamt].Value = discountAmt;
                    row.Cells[ppretaxamt].Value = baseAmt;
                    row.Cells[ptaxamt].Value = taxAmt;
                    row.Cells[pnetamt].Value = netAmt;
                    row.Cells[ptaxtype].Value = taxType;
                    row.Cells[ptaxper].Value = totalPercent;

                    if (taxType == "GST")
                    {
                        row.Cells[pigstamt].Value = (totalPercent > 0) ? taxAmt * igst / totalPercent : cgst + sgst;
                        row.Cells[pvatamt].Value = 0;
                    }
                    else
                    {
                        row.Cells[pvatamt].Value = taxAmt;
                        row.Cells[pigstamt].Value = 0;
                    }

                    switch ((PackItemType)productType)
                    {
                        case PackItemType.Pack:
                            preTax_pack += baseAmt;
                            tax_pack += taxAmt;
                            net_pack += netAmt;
                            break;

                        case PackItemType.Cake:
                            preTax_cake += baseAmt;
                            tax_cake += taxAmt;
                            net_cake += netAmt;
                            break;

                        case PackItemType.Card:
                            preTax_card += baseAmt;
                            tax_card += taxAmt;
                            net_card += netAmt;
                            break;

                        case PackItemType.Gift:
                            preTax_gift += baseAmt;
                            tax_gift += taxAmt;
                            net_gift += netAmt;
                            break;
                    }
                }

                // Update UI totals
                txtPreTaxAmount_card.Text = preTax_card.ToString("N2");
                txtTotalTaxAmount_card.Text = tax_card.ToString("N2");
                txtVATAmt_card.Text = (preTax_card > 0 && tax_card > 0 ? tax_card : 0).ToString("N2");
                txtIGSTAmt_card.Text = (preTax_card > 0 && tax_card > 0 ? tax_card : 0).ToString("N2");
                txtCardGrandTotal.Text = net_card.ToString("N2");

                txtPreTaxAmount_cake.Text = preTax_cake.ToString("N2");
                txtTotalTaxAmount_cake.Text = tax_cake.ToString("N2");
                txtVATAmt_cake.Text = (preTax_cake > 0 && tax_cake > 0 ? tax_cake : 0).ToString("N2");
                txtIGSTAmt_cake.Text = (preTax_cake > 0 && tax_cake > 0 ? tax_cake : 0).ToString("N2");
                txtCakeGrandTotal.Text = net_cake.ToString("N2");

                txtPreTaxAmount_gift.Text = preTax_gift.ToString("N2");
                txtTotalTaxAmount_gift.Text = tax_gift.ToString("N2");
                txtVATAmt_gift.Text = (preTax_gift > 0 && tax_gift > 0 ? tax_gift : 0).ToString("N2");
                txtIGSTAmt_gift.Text = (preTax_gift > 0 && tax_gift > 0 ? tax_gift : 0).ToString("N2");
                txtGiftGrandTotal.Text = net_gift.ToString("N2");

                CalculatePackValues();

                CalculateNetAmount();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        public void CalculateNetAmount()
        {
            if (isUpdating) return;

            try
            {
                isUpdating = true;

                decimal GetDecimal(TextBox txt)
                {
                    decimal.TryParse(txt.Text, out decimal val);
                    return val;
                }

                decimal Tdiacounamt = GetDecimal(txtTotalDiscountAmt);
                decimal discountPer = 0;
                decimal.TryParse(discountper.Text, out discountPer);


                // PACK
                decimal packPreTax = GetDecimal(txtPreTaxAmount_pack);
                decimal packTax = GetDecimal(txtTotalTaxAmount_pack);
                decimal packVAT = GetDecimal(txtVatamt_pack);
                decimal packIGST = GetDecimal(txtIGSTAmt_pack);
                decimal PackTotalamt = GetDecimal(txtGrandTotal_pack);

                // CAKE
                decimal cakePreTax = GetDecimal(txtPreTaxAmount_cake);
                decimal cakeTax = GetDecimal(txtTotalTaxAmount_cake);
                decimal cakeVAT = GetDecimal(txtVATAmt_cake);
                decimal cakeIGST = GetDecimal(txtIGSTAmt_cake);
                decimal CakeTotalamt = GetDecimal(txtCakeGrandTotal);

                // CARD
                decimal cardPreTax = GetDecimal(txtPreTaxAmount_card);
                decimal cardTax = GetDecimal(txtTotalTaxAmount_card);
                decimal cardVAT = GetDecimal(txtVATAmt_card);
                decimal cardIGST = GetDecimal(txtIGSTAmt_card);
                decimal CardTotalamt = GetDecimal(txtCardGrandTotal);

                // GIFT
                decimal giftPreTax = GetDecimal(txtPreTaxAmount_gift);
                decimal giftTax = GetDecimal(txtTotalTaxAmount_gift);
                decimal giftVAT = GetDecimal(txtVATAmt_gift);
                decimal giftIGST = GetDecimal(txtIGSTAmt_gift);
                decimal GiftTotalamt = GetDecimal(txtGiftGrandTotal);

                // TOTALS
                decimal totalPreTax = packPreTax + cakePreTax + cardPreTax + giftPreTax;
                decimal totalTax = packTax + cakeTax + cardTax + giftTax;
                decimal totalVAT = packVAT + cakeVAT + cardVAT + giftVAT;
                decimal totalIGST = packIGST + cakeIGST + cardIGST + giftIGST;

                // Additional Charge
                decimal addCharge = GetDecimal(txtextracharge_amt);
                decimal addChargeAfterDiscount = addCharge - (addCharge * discountPer / 100);
                txtaddcharge.Text = addChargeAfterDiscount.ToString("N2");

                decimal subtotal = PackTotalamt + CakeTotalamt + CardTotalamt + GiftTotalamt+ addCharge;
                decimal netAfterDiscount = subtotal - Tdiacounamt;
                decimal finalNet = netAfterDiscount;

                // Apply discount on tax
                decimal discountTax = totalTax * discountPer / 100;
                decimal finalTax = totalTax - discountTax;

                txtPreTaxAmount.Text = totalPreTax.ToString("N2");
                txtTotalTaxAmount.Text = finalTax.ToString("N2");
                txtTotalVATAmt.Text = totalVAT.ToString("N2");
                txtTotalIGSTAmt.Text = totalIGST.ToString("N2");

                txtGrandTotal.Text = finalNet.ToString("N2");
                lblNetAmt.Text = finalNet.ToString("N2");

                // ------------------------
                // Received & Balance
                // ------------------------
                decimal rcvAmt = GetDecimal(txtRcvAmt);
                decimal totalAdvance = dbAdvanceAmount + rcvAmt;

                lbladvamtdata.Text = totalAdvance.ToString("N2");
                lblBalAmt.Text = (finalNet - totalAdvance).ToString("N2");
            }
            catch (Exception ex)
            {
                clsgen.LogException("PartyBooking - CalculateNetAmount()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
            }
            finally
            {
                isUpdating = false;
            }
        }

        private void GetGrandTotal_All()
        {
            try
            {
                decimal grandTotal = 0;
                decimal packTotal = 0;
                decimal.TryParse(txtGrandTotal_pack.Text, out packTotal);
                grandTotal += packTotal;

                decimal cardTotal = 0, giftTotal = 0, cakeTotal = 0,addcharge=0;
                addcharge = GetDecimal(txtextracharge_amt);
                foreach (DataGridViewRow row in dgv_product.Rows)
                {
                    if (row.IsNewRow) continue;

                    int productType = Convert.ToInt32(row.Cells[PPackProductTypeno].Value ?? 0);
                    decimal netAmt = Convert.ToDecimal(row.Cells[pnetamt].Value ?? 0);

                    switch ((PackItemType)productType)
                    {
                        case PackItemType.Card:
                            cardTotal += netAmt;
                            break;
                        case PackItemType.Gift:
                            giftTotal += netAmt;
                            break;
                        case PackItemType.Cake:
                            cakeTotal += netAmt;
                            break;
                    }
                }

                grandTotal += cardTotal + giftTotal + cakeTotal+ addcharge;
                txtGrandTotal.Text = grandTotal.ToString("N2");
                originalGrandTotal = grandTotal;

            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Extra Charge

        private void btnotherarange_Click(object sender, EventArgs e)
        {
            pnl_extracharge.Left = (this.ClientSize.Width - pnl_extracharge.Width) / 2;
            pnl_extracharge.Top = (this.ClientSize.Height - pnl_extracharge.Height) / 2;

            pnl_extracharge.Visible = true;
            pnl_extracharge.BringToFront();
            txtextracharge_amt.Focus(); 
        }

        private void btn_ecclose_Click(object sender, EventArgs e)
        {
            pnl_extracharge.Visible = false;
        }


        private void btn_ec_submit_Click(object sender, EventArgs e)
        {
            pnl_extracharge.Visible = false;
            txtaddcharge.Text = txtextracharge_amt.Text;

            CalculateDiscountPer();
            CalculateNetAmount();
        }

        private void btn_ec_cancel_Click(object sender, EventArgs e)
        {
            pnl_extracharge.Visible = false;
        }


        private void txtextracharge_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;


            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && txt.Text.Contains("."))
            {
                e.Handled = true;
                return;
            }
            if (txt.Text.Contains("."))
            {
                int index = txt.Text.IndexOf(".");
                string decimals = txt.Text.Substring(index + 1);

                if (txt.SelectionStart > index && decimals.Length >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        #endregion
    }
}
