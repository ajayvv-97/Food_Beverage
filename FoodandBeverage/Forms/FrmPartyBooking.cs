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
using System.IO;
using System.Linq;
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

        #region Grid Declaration

        #region Custom Menu Grid Declaration

        public int mainslno = 0;
        public int mainitemno = 1;
        public int maincategmenuno = 2;
        public int mainqtyno = 3;
        public int mainactionno = 4;
        public int mainppcdiidno = 5;
        public int maindefaultno = 6;

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

        #endregion

        #region Grid Declaration Card Gift Cake

        public int Cardslno = 0;
        public int Cardactionno = 1;
        public int Carditemno = 2;
        public int Cardcategmenuno = 3;
        public int Cardqtyno = 4;
        public int CardPrice = 5;
        public int Cardtax = 6;
        public int Cardtaxtype = 7;
        public int Cardtaxper = 8;
        public int Cardtaxamt = 9;
        public int Carddiscper = 10;
        public int Carddisamt = 11;
        public int Cardigstamt = 12;
        public int Cardvatamt = 13;
        public int Cardnetamt = 14;
        public int CardProductid = 15;
        public int Cardproducttype = 16;
        public int CardPartyPackDetailsId = 17;
        public int CardPartydeafult = 18;
        public int Cardtaxsettingid = 19;
        public int Cardpretaxamt= 20;


        public int Giftslno = 0;
        public int Giftactionno = 1;
        public int Giftitemno = 2;
        public int Giftcategmenuno = 3;
        public int Giftqtyno = 4;
        public int GiftPrice = 5;
        public int Gifttax = 6;
        public int Gifttaxtype = 7;
        public int Gifttaxper = 8;
        public int Gifttaxamt = 9;
        public int Giftdiscper = 10;
        public int Giftdisamt = 11;
        public int Giftigstamt = 12;
        public int Giftvatamt = 13;
        public int Giftnetamt = 14;
        public int GiftProductid = 15;
        public int Giftproducttype = 16;
        public int GiftPartyPackDetailsId = 17;
        public int GiftPartydeafult = 18; 
        public int Gifttaxsettingid = 19; 
        public int Giftpretaxamt = 20;
        public int Giftprid = 21;


        public int CakeSlNo = 0;
        public int CakeActionNo = 1;
        public int CakeItemNo = 2;
        public int CakeQtyNo = 3;
        public int CakeAmountNo = 4; 
        public int CakeTaxAmtNo = 5;
        public int CakeNetAmtNo = 6;
        public int CakeIdno = 7;
        public int CakeTaxNo = 8;
        public int CakeTaxTypeNo = 9;
        public int CakeTaxPerNo = 10;
        public int CakeIgstAmtNo = 11;
        public int CakeVatAmtNo = 12;
        public int CaketaxsettingidNo = 13;
        public int Cakemessageno = 14;
        public int Cakeremarksno = 15;


        #endregion

        private List<DmnPartyPackDetails> packDetailsList;
        private List<DmnCategory> categoryList = new List<DmnCategory>();

        List<SelectedCustomItem> selectedCustomItems = new List<SelectedCustomItem>();
        int CurrentPackId = 0;
        List<DmnPartyPackCustom> customMenuList = new List<DmnPartyPackCustom>();

        #endregion

        #region Domain


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
        #endregion

        public FrmPartyBooking()
        {
            InitializeComponent();
        }

        private void FrmPartyBooking_Load(object sender, EventArgs e)
        {
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
            LoadCakeTypes();
        }


        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        private void btn_packsitems_Click(object sender, EventArgs e)
        {
            LoadPartyPacks();
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
                             AND d.Status = 1) AS DetailsCount";
                search.Table = "tblPartyPackMaster";
                search.WhereNdGroupBy = "WHERE Status = 1 and PartyPackId ="+ pkid;
                search.OrderBy = "ORDER BY PackName";
                var list = bll.GetSearchData("prdSearch", search);
                if (list.Count > 0)
                {
                    txtChildPrice.Text = Convert.ToString(list[0].ChildPrice);
                    txtAdultPrice.Text = Convert.ToString(list[0].AdultPrice);
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

                    LoadPackRelatedItems(Convert.ToInt32(list[0].PartyPackId));


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
                            pnl_foodpacks.Visible = false;

                            int packId = Convert.ToInt32(((Button)s).Tag);
                            Loadprice(packId);
                            if (item.DetailsCount > 0)
                            {
                                OpenPackEntryPanel();
                                LoadPartyPackDetails(packId);
                            }
                            else
                            {
                                lblpackname.Visible = true;
                                lblpacklabel.Visible = true;
                                lblpackname.Text = SetPackName(packId);

                                OpenPackEntryPanel();
                            }
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
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadCakeTypes()
        {
            try
            {
                BllSearch<DmnPartyPackCake> bllSearch = new BllSearch<DmnPartyPackCake>();
                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = "PartyPackCakeId, CakeType";
                objSearch.Table = "tblPartyPackCake";
                objSearch.WhereNdGroupBy = "WHERE Status = 1";
                objSearch.OrderBy = "ORDER BY CakeType";

                var cakeList = bllSearch.GetSearchData("prdSearch", objSearch);
                if (cakeList.Count > 0)
                {
                    cmbcaketype.DataSource = cakeList;
                    cmbcaketype.DisplayMember = "CakeType";
                    cmbcaketype.ValueMember = "PartyPackCakeId";
                    cmbcaketype.SelectedIndex = -1; // optional (no default selection)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                        row.ReadOnly = true;
                    }
                }

                int newRowIndex = dgv_packenter.Rows.Add();
                dgv_packenter.Rows[newRowIndex].Cells[selecteditemslno].Value = sl;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }
        private void btnresturant_menu_Click(object sender, EventArgs e)
        {

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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }
        private bool ValidateEvent()
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text))
            {
                MessageBox.Show("Enter Item Name");
                txtItemName.Focus();
                return false;
            }

            //if (string.IsNullOrWhiteSpace(txtDescription.Text))
            //{
            //    MessageBox.Show("Enter Description");
            //    txtDescription.Focus();
            //    return false;
            //}

            return true;
        }
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

                MessageBox.Show("Saved Successfully");

                LoadEventItems();

                txtItemName.Clear();
                txtDescription.Clear();

                pnl_eventreg.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_del_event_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_del_event.Tag == null)
                {
                    MessageBox.Show("Please search and select item first");
                    return;
                }

                DialogResult result = MessageBox.Show(
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

                MessageBox.Show("Deleted Successfully");

                txtItemName.Clear();
                txtDescription.Clear();
                btn_del_event.Visible = false;
                btn_del_event.Tag = null;

                LoadEventItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    MessageBox.Show("No Records Found");
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_pak_details_cls_Click(object sender, EventArgs e)
        {
            pnl_pak_detailsitems.Visible = false;
        }

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
            catch(Exception ex)
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

                LoadMenuCategory_productview();

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
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadMenuCategory_productview()
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

                DataGridViewComboBoxColumn cmb = (DataGridViewComboBoxColumn)dgv_packs.Columns[selecteditemcategmenuno];

                cmb.DataSource = categoryList;
                cmb.DisplayMember = "CategoryName";
                cmb.ValueMember = "Fcid";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
            catch(Exception ex)
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
            catch(Exception ex)
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
                MessageBox.Show("Error while selecting item.\n" + ex.Message);
            }
        }

        private void dgv_cus_menu_items_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_cus_menu_items.IsCurrentCellDirty)
                dgv_cus_menu_items.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        #endregion

        private void btn_enter_paks_cls_Click(object sender, EventArgs e)
        {
            pnl_enter_paks.Visible = false;
        }

        private void btnaddpack_main_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_packs.Rows.Clear();

                LoadMenuCategory_productview();

                int sl = 1;
                foreach (DataGridViewRow sourceRow in dgv_packenter.Rows)
                {
                    if (sourceRow.IsNewRow) continue;

                    if (sourceRow.Cells[selecteditemitemno].Value == null)
                        continue;

                    int rowIndex = dgv_packs.Rows.Add();
                    DataGridViewRow targetRow = dgv_packs.Rows[rowIndex];

                    targetRow.Cells[mainslno].Value = sl++;
                    targetRow.Cells[mainitemno].Value = sourceRow.Cells[selecteditemitemno].Value;

                    if(sourceRow.Cells[selecteditemcategmenuno].Value !=null)
                        targetRow.Cells[maincategmenuno].Value =Convert.ToInt64(sourceRow.Cells[selecteditemcategmenuno].Value);

                    targetRow.Cells[mainqtyno].Value =   sourceRow.Cells[selecteditemqtyno].Value;
                    targetRow.Cells[mainppcdiidno].Value = sourceRow.Cells[selecteditemppcdiidno].Value;
                    targetRow.Cells[maindefaultno].Value = sourceRow.Cells[selecteditemdeafultno].Value;

                }

                pnl_foodpacks.Visible = false;
                pnl_enter_paks.Visible = false;
                pnl_CustomItems_menu.Visible = false;

                dgv_packenter.Rows.Clear();
                selectedCustomItems.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgv_packs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex == mainactionno)
                {
                    DialogResult result = MessageBox.Show(
                        "Are you sure you want to delete this row?",
                        "Confirm",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        dgv_packs.Rows.RemoveAt(e.RowIndex);
                        ReorderMainSlNo();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReorderMainSlNo()
        {
            int sl = 1;

            foreach (DataGridViewRow row in dgv_packs.Rows)
            {
                if (row.IsNewRow) continue;

                row.Cells[mainslno].Value = sl++;
            }
        }

        private void UpdateTotalGuests()
        {
            int adults = 0;
            int children = 0;

            int.TryParse(txtAdults.Text, out adults);
            int.TryParse(txtChild.Text, out children);

            txttotalguest.Text = (adults + children).ToString();
        }

        private void UpdateGrandTotal()
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
                dmnSearch.WhereNdGroupBy = " where g.GTypeId=58 and g.Status=0 and GTypeSubId in(1,3,5,7)";
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
                        decimal price = Convert.ToDecimal(item.Field2);
                        int taxSettingId = Convert.ToInt32(item.Field4);
                        string taxMode = Convert.ToString(item.Field3); // Inclusive / Exclusive

                        decimal baseAmt = 0;
                        decimal taxAmt = 0;
                        decimal netAmt = 0;

                        decimal igst = 0;
                        decimal cgst = 0;
                        decimal sgst = 0;
                        decimal vat = 0;

                        decimal totalPercent = 0;
                        string taxType = "";

                        // 🔹 FETCH TAX SETTINGS LIKE CAKE
                        if (taxSettingId > 0)
                        {
                            BllSearch<DmnTaxSettings> _bllTax = new BllSearch<DmnTaxSettings>();
                            DmnSearch dmnTax = new DmnSearch();

                            dmnTax.Select = "TaxType, IgstPrcnt, CgstPrcnt, SgstPrcnt, VatPrcnt";
                            dmnTax.Table = $"[{GameDB}].dbo.tblTaxSettings";
                            dmnTax.WhereNdGroupBy = $"WHERE Status=1 AND TaxSettingsId={taxSettingId}";

                            var taxSettings = _bllTax.GetSearchData("prdSearch", dmnTax);

                            if (taxSettings.Count > 0)
                            {
                                igst = taxSettings[0].IgstPrcnt;
                                cgst = taxSettings[0].CgstPrcnt;
                                sgst = taxSettings[0].SgstPrcnt;
                                vat = taxSettings[0].VatPrcnt;

                                taxType = taxSettings[0].TaxType;

                                if (taxType == "GST")
                                {
                                    totalPercent = igst ;
                                }
                                else
                                {
                                    totalPercent = vat;
                                }
                            }
                        }

                        // 🔹 CALCULATE TAX
                        if (taxMode == "Inclusive")
                        {
                            baseAmt = price / (1 + (totalPercent / 100));
                            taxAmt = price - baseAmt;
                            netAmt = price;
                        }
                        else
                        {
                            baseAmt = price;
                            taxAmt = price * totalPercent / 100;
                            netAmt = price + taxAmt;
                        }

                        baseAmt = Math.Round(baseAmt, 2, MidpointRounding.AwayFromZero);
                        taxAmt = Math.Round(taxAmt, 2, MidpointRounding.AwayFromZero);
                        netAmt = Math.Round(netAmt, 2, MidpointRounding.AwayFromZero);

                        int rowIndex = dgv_card.Rows.Add();
                        DataGridViewRow row = dgv_card.Rows[rowIndex];

                        row.Cells[Cardslno].Value = rowIndex + 1;
                        row.Cells[Carditemno].Value = item.Field1;
                        row.Cells[Cardqtyno].Value = 1;

                        row.Cells[CardPrice].Value = price;
                        row.Cells[Cardtax].Value = taxMode;
                        row.Cells[Cardtaxtype].Value = taxType;
                        row.Cells[Cardtaxper].Value = totalPercent;
                        row.Cells[Cardtaxamt].Value = taxAmt;
                        row.Cells[Cardpretaxamt].Value = baseAmt;

                        // 🔹 GST / VAT split
                        if (taxType == "GST")
                        {
                            if (igst > 0)
                            {
                                row.Cells[Cardigstamt].Value = Math.Round(taxAmt * igst / totalPercent, 2);
                            }
                            else
                            {
                                decimal cgstAmt = Math.Round(taxAmt * cgst / totalPercent, 2);
                                decimal sgstAmt = Math.Round(taxAmt * sgst / totalPercent, 2);
                                row.Cells[Cardigstamt].Value = cgstAmt + sgstAmt;
                            }

                            row.Cells[Cardvatamt].Value = 0;
                        }
                        else
                        {
                            row.Cells[Cardvatamt].Value = taxAmt;
                            row.Cells[Cardigstamt].Value = 0;
                        }

                        row.Cells[Cardnetamt].Value = netAmt;

                        row.Cells[CardProductid].Value = item.Id;
                        row.Cells[Cardproducttype].Value = ptypeno;
                        row.Cells[CardPartyPackDetailsId].Value = 0;
                        row.Cells[CardPartydeafult].Value = 0;
                        row.Cells[Cardtaxsettingid].Value = taxSettingId;
                    }

                    CalculateGrandTotal_Card();
                }

                pnl_cardpreview.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RecalculateCardRow(int rowIndex)
        {
            try
            {
                if (rowIndex < 0) return;

                var row = dgv_card.Rows[rowIndex];
                if (row.IsNewRow) return;

                decimal qty = 0;
                decimal price = 0;
                decimal taxPercent = 0;
                decimal discountPer = 0;

                decimal.TryParse(Convert.ToString(row.Cells[Cardqtyno].Value), out qty);
                decimal.TryParse(Convert.ToString(row.Cells[CardPrice].Value), out price);
                decimal.TryParse(Convert.ToString(row.Cells[Cardtaxper].Value), out taxPercent);
                decimal.TryParse(Convert.ToString(row.Cells[Carddiscper].Value), out discountPer);

                if (qty <= 0) qty = 1;

                string taxMode = Convert.ToString(row.Cells[Cardtax].Value);

                // 🔹 Gross
                decimal grossAmount = qty * price;

                // 🔹 Discount
                decimal discountAmt = grossAmount * discountPer / 100;

                // 🔹 After Discount
                decimal amount = grossAmount - discountAmt;

                decimal baseAmt = 0;
                decimal taxAmt = 0;
                decimal netAmt = 0;

                if (taxMode == "Inclusive")
                {
                    baseAmt = amount / (1 + (taxPercent / 100));
                    taxAmt = amount - baseAmt;
                    netAmt = amount;
                }
                else
                {
                    baseAmt = amount;
                    taxAmt = amount * taxPercent / 100;
                    netAmt = amount + taxAmt;
                }

                // 🔹 Set Values
                row.Cells[Cardpretaxamt].Value = Math.Round(baseAmt, 2);
                row.Cells[Cardtaxamt].Value = Math.Round(taxAmt, 2);
                row.Cells[Cardnetamt].Value = Math.Round(netAmt, 2);
                row.Cells[Carddisamt].Value = Math.Round(discountAmt, 2);

                CalculateGrandTotal_Card();
            }
            catch { }
        }
        private void CalculateGrandTotal_Card()
        {
            decimal total = 0;
            decimal totalTax = 0;
            decimal totalPreTax = 0;
            decimal totalDiscount = 0;

            foreach (DataGridViewRow row in dgv_card.Rows)
            {
                if (!row.IsNewRow)
                {
                    decimal qty = 0, price = 0, taxPercent = 0, discountPer = 0;

                    decimal.TryParse(Convert.ToString(row.Cells[Cardqtyno].Value), out qty);
                    decimal.TryParse(Convert.ToString(row.Cells[CardPrice].Value), out price);
                    decimal.TryParse(Convert.ToString(row.Cells[Cardtaxper].Value), out taxPercent);
                    decimal.TryParse(Convert.ToString(row.Cells[Carddiscper].Value), out discountPer);

                    if (qty <= 0) qty = 1;

                    string taxMode = Convert.ToString(row.Cells[Cardtax].Value);

                    decimal grossAmount = qty * price;
                    decimal discountAmt = grossAmount * discountPer / 100;

                    decimal amount = grossAmount - discountAmt;

                    decimal baseAmt = 0;
                    decimal taxAmt = 0;
                    decimal netAmt = 0;

                    if (taxMode == "Inclusive")
                    {
                        baseAmt = amount / (1 + (taxPercent / 100));
                        taxAmt = amount - baseAmt;
                        netAmt = amount;
                    }
                    else
                    {
                        baseAmt = amount;
                        taxAmt = amount * taxPercent / 100;
                        netAmt = amount + taxAmt;
                    }

                    baseAmt = Math.Round(baseAmt, 2);
                    taxAmt = Math.Round(taxAmt, 2);
                    netAmt = Math.Round(netAmt, 2);

                    totalPreTax += baseAmt;
                    totalTax += taxAmt;
                    total += netAmt;
                    totalDiscount += discountAmt;
                }
            }

            txtPreTaxAmount_card.Text = totalPreTax.ToString("0.00");
            txtTotalTaxAmount_card.Text = totalTax.ToString("0.00");
            txtCardGrandTotal.Text = total.ToString("0.00");

            CalculateNetAmount();
        }
        private void dgv_card_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == Cardqtyno)
            {
                decimal qty = 0;
                decimal.TryParse(Convert.ToString(dgv_card.Rows[e.RowIndex].Cells[Cardqtyno].Value), out qty);

                if (qty <= 0)
                {
                    MessageBox.Show("Quantity must be greater than 0");
                    dgv_card.Rows[e.RowIndex].Cells[Cardqtyno].Value = 1;
                    return;
                }
                RecalculateCardRow(e.RowIndex);
            }
        }
        private void ApplyDiscountToCardRows(decimal discountPer, decimal discountAmt, bool isPercentage)
        {
            decimal totalAmount = 0;

            foreach (DataGridViewRow row in dgv_card.Rows)
            {
                if (!row.IsNewRow)
                {
                    decimal qty = 0, price = 0;
                    decimal.TryParse(Convert.ToString(row.Cells[Cardqtyno].Value), out qty);
                    decimal.TryParse(Convert.ToString(row.Cells[CardPrice].Value), out price);

                    if (qty <= 0) qty = 1;

                    totalAmount += qty * price;
                }
            }

            if (totalAmount == 0) return;

            foreach (DataGridViewRow row in dgv_card.Rows)
            {
                if (!row.IsNewRow)
                {
                    decimal qty = 0, price = 0;

                    decimal.TryParse(Convert.ToString(row.Cells[Cardqtyno].Value), out qty);
                    decimal.TryParse(Convert.ToString(row.Cells[CardPrice].Value), out price);

                    if (qty <= 0) qty = 1;

                    decimal rowAmount = qty * price;

                    decimal rowDiscount = 0;
                    decimal rowDiscountPer = 0;

                    if (isPercentage)
                    {
                        rowDiscountPer = discountPer;
                        rowDiscount = rowAmount * discountPer / 100;
                    }
                    else
                    {
    
                        rowDiscount = (rowAmount / totalAmount) * discountAmt;
                        rowDiscountPer = rowAmount == 0 ? 0 : (rowDiscount / rowAmount) * 100;
                    }

                    row.Cells[Carddiscper].Value = Math.Round(rowDiscountPer, 2);
                    row.Cells[Carddisamt].Value = Math.Round(rowDiscount, 2);

                    RecalculateCardRow(row.Index);
                }
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
        private void btn_addcards_Click(object sender, EventArgs e)
        {
            try
            {
                pnlcards.Visible = false;
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
            catch(Exception)
            {
            }

        }

        private void btncards_cls_Click(object sender, EventArgs e)
        {
            pnl_cardpreview.Visible = false;
            pnlcards.Visible = true;
        }
        private void btncard_Click(object sender, EventArgs e)
        {
            if (pnlcards.Visible)
            {
                pnlcards.Visible = false;
                return;
            }
            pnlcards.Width = (int)(this.ClientSize.Width * 0.7);
            pnlcards.Height = (int)(this.ClientSize.Height * 0.7);

            pnlcards.Left = (this.ClientSize.Width - pnlcards.Width) / 2;
            pnlcards.Top = (this.ClientSize.Height - pnlcards.Height) / 2;

            pnlcards.Visible = true;
            pnlcards.BringToFront();
        }

        private void btn_card_cls_Click(object sender, EventArgs e)
        {
            pnlcards.Visible = false;
        }
        private void btncardsubmit_Click(object sender, EventArgs e)
        {
            pnlcards.Visible = false;
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
                MessageBox.Show("Please select category first.",
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                txtcardserach.Clear();   // optional: clear search box
                return;
            }

            LoadProducts(tag, txtcardserach.Text.Trim());
        }

        #endregion

        #region Gift Panel
        private void btngiftprevcls_Click(object sender, EventArgs e)
        {
            pnlgiftpreview.Visible = false;
        }
        private void btngift_Click(object sender, EventArgs e)
        {
            if (pnlgifts.Visible)
            {
                pnlgifts.Visible = false;
                return;
            }

            pnlgifts.Width = (int)(this.ClientSize.Width * 0.7);
            pnlgifts.Height = (int)(this.ClientSize.Height * 0.7);

            pnlgifts.Left = (this.ClientSize.Width - pnlgifts.Width) / 2;
            pnlgifts.Top = (this.ClientSize.Height - pnlgifts.Height) / 2;

            pnlgifts.Visible = true;
            pnlgifts.BringToFront();
        }

        private void btngiftclose_Click(object sender, EventArgs e)
        {
            pnlgifts.Visible = false;
        }

        private void btnaddgifts_Click(object sender, EventArgs e)
        {
            pnlgiftpreview.Visible = true;
            pnlgiftpreview.BringToFront();
            pnlgiftpreview.Left = (this.ClientSize.Width - pnlgiftpreview.Width) / 2;
            pnlgiftpreview.Top = (this.ClientSize.Height - pnlgiftpreview.Height) / 2;
            pnlgifts.Visible = false;
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
        private void ClearPanelControls(Panel panel)
        {
            foreach (Control ctrl in panel.Controls.OfType<TableLayoutPanel>().ToList())
            {
                ctrl.Controls.Clear();
                panel.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
            panel.Controls.Clear();
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show("Error loading product");
            }
        }
        private void dgv_gift_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                if (e.ColumnIndex == Giftactionno) 
                {
                    DataGridViewRow row = dgv_gift.Rows[e.RowIndex];
                    int partyDefault = 0;
                    int.TryParse(Convert.ToString(row.Cells[GiftPartydeafult].Value), out partyDefault);

                    if (partyDefault == 0)
                    {
                        dgv_gift.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        MessageBox.Show("Default item cannot be deleted", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void AddGiftRow(dynamic item, int ptypeno)
        {
            try
            {
                decimal price = Convert.ToDecimal(item.Field2);
                int taxSettingId = Convert.ToInt32(item.Field4);
                string taxMode = Convert.ToString(item.Field3);

                decimal baseAmt = 0, taxAmt = 0, netAmt = 0;
                decimal igst = 0, cgst = 0, sgst = 0, vat = 0;
                decimal totalPercent = 0;
                string taxType = "";

                if (taxSettingId > 0)
                {
                    BllSearch<DmnTaxSettings> _bllTax = new BllSearch<DmnTaxSettings>();
                    DmnSearch dmnTax = new DmnSearch();

                    dmnTax.Select = "TaxType, IgstPrcnt, CgstPrcnt, SgstPrcnt, VatPrcnt";
                    dmnTax.Table = $"[{GameDB}].dbo.tblTaxSettings";
                    dmnTax.WhereNdGroupBy = $"WHERE Status=1 AND TaxSettingsId={taxSettingId}";

                    var taxSettings = _bllTax.GetSearchData("prdSearch", dmnTax);

                    if (taxSettings.Count > 0)
                    {
                        igst = taxSettings[0].IgstPrcnt;
                        cgst = taxSettings[0].CgstPrcnt;
                        sgst = taxSettings[0].SgstPrcnt;
                        vat = taxSettings[0].VatPrcnt;

                        taxType = taxSettings[0].TaxType;
                        totalPercent = taxType == "GST" ? igst : vat;
                    }
                }

                // 🔹 CALCULATION
                if (taxMode == "Inclusive")
                {
                    baseAmt = price / (1 + (totalPercent / 100));
                    taxAmt = price - baseAmt;
                    netAmt = price;
                }
                else
                {
                    baseAmt = price;
                    taxAmt = price * totalPercent / 100;
                    netAmt = price + taxAmt;
                }

                baseAmt = Math.Round(baseAmt, 2);
                taxAmt = Math.Round(taxAmt, 2);
                netAmt = Math.Round(netAmt, 2);

                int rowIndex = dgv_gift.Rows.Add();
                var row = dgv_gift.Rows[rowIndex];

                row.Cells[Giftslno].Value = rowIndex + 1;
                row.Cells[Giftitemno].Value = item.Field1;
                row.Cells[Giftqtyno].Value = 1;

                row.Cells[GiftPrice].Value = price;
                row.Cells[Gifttax].Value = taxMode;
                row.Cells[Gifttaxtype].Value = taxType;
                row.Cells[Gifttaxper].Value = totalPercent;
                row.Cells[Gifttaxamt].Value = taxAmt;
                row.Cells[Giftpretaxamt].Value = baseAmt;
                row.Cells[Giftprid].Value = item.Field5; 

                // GST / VAT
                if (taxType == "GST")
                {
                    if (igst > 0)
                        row.Cells[Giftigstamt].Value = Math.Round(taxAmt * igst / totalPercent, 2);
                    else
                        row.Cells[Giftigstamt].Value = taxAmt;

                    row.Cells[Giftvatamt].Value = 0;
                }
                else
                {
                    row.Cells[Giftvatamt].Value = taxAmt;
                    row.Cells[Giftigstamt].Value = 0;
                }

                row.Cells[Giftnetamt].Value = netAmt;

                row.Cells[GiftProductid].Value = item.Id;
                row.Cells[Giftproducttype].Value = ptypeno;
                row.Cells[GiftPartyPackDetailsId].Value = 0;
                row.Cells[GiftPartydeafult].Value = 0;
                row.Cells[Gifttaxsettingid].Value = taxSettingId;

                CalculateGrandTotal_Gift();
            }
            catch (Exception ex)
            {

            }
        }
        private void RecalculateGiftRow(int rowIndex)
        {
            try
            {
                if (rowIndex < 0) return;

                var row = dgv_gift.Rows[rowIndex];
                if (row.IsNewRow) return;

                decimal qty = 0, price = 0, taxPercent = 0, discountPer = 0;

                decimal.TryParse(Convert.ToString(row.Cells[Giftqtyno].Value), out qty);
                decimal.TryParse(Convert.ToString(row.Cells[GiftPrice].Value), out price);
                decimal.TryParse(Convert.ToString(row.Cells[Gifttaxper].Value), out taxPercent);
                decimal.TryParse(Convert.ToString(row.Cells[Giftdiscper].Value), out discountPer);

                if (qty <= 0) qty = 1;

                string taxMode = Convert.ToString(row.Cells[Gifttax].Value);

                decimal gross = qty * price;
                decimal discountAmt = gross * discountPer / 100;
                decimal amount = gross - discountAmt;

                decimal baseAmt = 0, taxAmt = 0, netAmt = 0;

                if (taxMode == "Inclusive")
                {
                    baseAmt = amount / (1 + (taxPercent / 100));
                    taxAmt = amount - baseAmt;
                    netAmt = amount;
                }
                else
                {
                    baseAmt = amount;
                    taxAmt = amount * taxPercent / 100;
                    netAmt = amount + taxAmt;
                }

                row.Cells[Giftpretaxamt].Value = Math.Round(baseAmt, 2);
                row.Cells[Gifttaxamt].Value = Math.Round(taxAmt, 2);
                row.Cells[Giftnetamt].Value = Math.Round(netAmt, 2);
                row.Cells[Giftdisamt].Value = Math.Round(discountAmt, 2);

                CalculateGrandTotal_Gift();
            }
            catch { }
        }
        private void CalculateGrandTotal_Gift()
        {
            decimal total = 0, totalTax = 0, totalPreTax = 0, totalDiscount = 0;

            foreach (DataGridViewRow row in dgv_gift.Rows)
            {
                if (!row.IsNewRow)
                {
                    decimal qty = 0, price = 0, taxPercent = 0, discountPer = 0;

                    decimal.TryParse(Convert.ToString(row.Cells[Giftqtyno].Value), out qty);
                    decimal.TryParse(Convert.ToString(row.Cells[GiftPrice].Value), out price);
                    decimal.TryParse(Convert.ToString(row.Cells[Gifttaxper].Value), out taxPercent);
                    decimal.TryParse(Convert.ToString(row.Cells[Giftdiscper].Value), out discountPer);

                    if (qty <= 0) qty = 1;

                    string taxMode = Convert.ToString(row.Cells[Gifttax].Value);

                    decimal gross = qty * price;
                    decimal discountAmt = gross * discountPer / 100;
                    decimal amount = gross - discountAmt;

                    decimal baseAmt = 0, taxAmt = 0, netAmt = 0;

                    if (taxMode == "Inclusive")
                    {
                        baseAmt = amount / (1 + (taxPercent / 100));
                        taxAmt = amount - baseAmt;
                        netAmt = amount;
                    }
                    else
                    {
                        baseAmt = amount;
                        taxAmt = amount * taxPercent / 100;
                        netAmt = amount + taxAmt;
                    }

                    totalPreTax += baseAmt;
                    totalTax += taxAmt;
                    total += netAmt;
                    totalDiscount += discountAmt;
                }
            }

            txtPreTaxAmount_gift.Text = totalPreTax.ToString("0.00");
            txtTotalTaxAmount_gift.Text = totalTax.ToString("0.00");
            txtGiftGrandTotal.Text = total.ToString("0.00");

            CalculateNetAmount();
        }
        private void dgv_gift_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == Giftqtyno)
            {
                decimal qty = 0;
                decimal.TryParse(Convert.ToString(dgv_gift.Rows[e.RowIndex].Cells[Giftqtyno].Value), out qty);

                if (qty <= 0)
                {
                    MessageBox.Show("Quantity must be greater than 0");
                    dgv_gift.Rows[e.RowIndex].Cells[Giftqtyno].Value = 1;
                    return;
                }

                RecalculateGiftRow(e.RowIndex);
            }
        }

        private void ApplyDiscountToGiftRows(decimal discountPer, decimal discountAmt, bool isPercentage)
        {
            decimal totalAmount = 0;

            foreach (DataGridViewRow row in dgv_gift.Rows)
            {
                if (!row.IsNewRow)
                {
                    decimal qty = 0, price = 0;

                    decimal.TryParse(Convert.ToString(row.Cells[Giftqtyno].Value), out qty);
                    decimal.TryParse(Convert.ToString(row.Cells[GiftPrice].Value), out price);

                    if (qty <= 0) qty = 1;

                    totalAmount += qty * price;
                }
            }

            if (totalAmount == 0) return;

            foreach (DataGridViewRow row in dgv_gift.Rows)
            {
                if (!row.IsNewRow)
                {
                    decimal qty = 0, price = 0;

                    decimal.TryParse(Convert.ToString(row.Cells[Giftqtyno].Value), out qty);
                    decimal.TryParse(Convert.ToString(row.Cells[GiftPrice].Value), out price);

                    if (qty <= 0) qty = 1;

                    decimal rowAmount = qty * price;

                    decimal rowDiscount = 0;
                    decimal rowDiscountPer = 0;

                    if (isPercentage)
                    {
                        rowDiscountPer = discountPer;
                        rowDiscount = rowAmount * discountPer / 100;
                    }
                    else
                    {
                        rowDiscount = (rowAmount / totalAmount) * discountAmt;
                        rowDiscountPer = (rowDiscount / rowAmount) * 100;
                    }

                    row.Cells[Giftdiscper].Value = Math.Round(rowDiscountPer, 2);
                    row.Cells[Giftdisamt].Value = Math.Round(rowDiscount, 2);

                    RecalculateGiftRow(row.Index);
                }
            }
        }
        private void btngiftsubmit_Click(object sender, EventArgs e)
        {
            pnlgifts.Visible = false;
        }

        #endregion

        #region Party Booking Calculation

        private bool isUpdating = false;

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

                // 🔹 PACK
                decimal packPreTax = GetDecimal(txtPreTaxAmount_pack);
                decimal packTax = GetDecimal(txtTotalTaxAmount_pack);

                // 🔹 CAKE
                decimal cakePreTax = GetDecimal(txtPreTaxAmount_cake);
                decimal cakeTax = GetDecimal(txtTotalTaxAmount_cake);

                // 🔹 CARD
                decimal cardPreTax = GetDecimal(txtPreTaxAmount_card);
                decimal cardTax = GetDecimal(txtTotalTaxAmount_card);

                // 🔹 GIFT
                decimal giftPreTax = GetDecimal(txtPreTaxAmount_gift);
                decimal giftTax = GetDecimal(txtTotalTaxAmount_gift);

                // 🔥 FINAL TOTAL
                decimal totalPreTax = packPreTax + cakePreTax + cardPreTax + giftPreTax;
                decimal totalTax = packTax + cakeTax + cardTax + giftTax;
                decimal finalNet = totalPreTax + totalTax;

                // 🔹 UPDATE UI
                txtPreTaxAmount.Text = totalPreTax.ToString("N2");
                txtTotalTaxAmount.Text = totalTax.ToString("N2");

                txtGrandTotal.Text = finalNet.ToString("N2");  
                lblNetAmt.Text = finalNet.ToString("N2");

                decimal rcvAmt = GetDecimal(txtRcvAmt);
                lblBalAmt.Text = (finalNet - rcvAmt).ToString("N2");
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

                UpdateGrandTotal();

                // 🔹 1️⃣ BASIC VALUES
                decimal grandTotal = GetDecimal(txtGrandTotal_pack);
                decimal discountPer = GetDecimal(discountper);   // 🔥 Discount %
                decimal otherAdd = GetDecimal(txtOAAmount);
                decimal otherDeduct = GetDecimal(txtODAmount);

                // 🔹 2️⃣ CALCULATE DISCOUNT
                decimal discountAmt = 0;

                if (discountPer > 0)
                {
                    discountAmt = (grandTotal * discountPer) / 100;
                }
                else
                {
                    discountAmt = GetDecimal(txtTotalDiscountAmt); // manual entry
                }

                discountAmt = Math.Round(discountAmt, 2);

                txtTotalDiscountAmt.Text = discountAmt.ToString("N2");

                // 🔹 3️⃣ NET BEFORE TAX
                decimal netBeforeTax = grandTotal - discountAmt + otherAdd - otherDeduct;

                // 🔹 TAX VARIABLES
                decimal baseAmt = 0;
                decimal taxAmt = 0;
                decimal netAmt = 0;

                decimal igst = 0;
                decimal cgst = 0;
                decimal sgst = 0;
                decimal vat = 0;
                decimal totalPercent = 0;

                string taxType = "";
                string taxMode = txttax.Text.Trim(); // Inclusive / Exclusive

                // 🔹 4️⃣ GET TAX SETTINGS
                if (!string.IsNullOrEmpty(txtTaxSettingId.Text))
                {
                    int taxSettingId = Convert.ToInt32(txtTaxSettingId.Text);

                    BllSearch<DmnTaxSettings> _bllSearch = new BllSearch<DmnTaxSettings>();
                    DmnSearch dmnSearch = new DmnSearch();

                    dmnSearch.Select = "TaxType, IgstPrcnt, CgstPrcnt, SgstPrcnt, VatPrcnt";
                    dmnSearch.Table = "tblTaxSettings";
                    dmnSearch.WhereNdGroupBy = $"WHERE Status=1 AND TaxSettingsId={taxSettingId}";

                    var taxSettings = _bllSearch.GetSearchData("prdSearch", dmnSearch);

                    if (taxSettings.Count > 0)
                    {
                        igst = taxSettings[0].IgstPrcnt;
                        cgst = taxSettings[0].CgstPrcnt;
                        sgst = taxSettings[0].SgstPrcnt;
                        vat = taxSettings[0].VatPrcnt;

                        taxType = taxSettings[0].TaxType;

                        if (taxType == "GST")
                            totalPercent = igst;  // or cgst+sgst if needed
                        else
                            totalPercent = vat;

                        txtTaxtype_pack.Text = taxType;
                        txtTaxPer_pack.Text = totalPercent.ToString("N2");
                    }
                }

                // 🔹 5️⃣ TAX CALCULATION
                if (taxMode.ToUpper() == "INCLUSIVE")
                {
                    baseAmt = netBeforeTax / (1 + (totalPercent / 100));
                    taxAmt = netBeforeTax - baseAmt;
                    netAmt = netBeforeTax;
                }
                else // EXCLUSIVE
                {
                    baseAmt = netBeforeTax;
                    taxAmt = netBeforeTax * totalPercent / 100;
                    netAmt = netBeforeTax + taxAmt;
                }

                // 🔹 ROUNDING
                baseAmt = Math.Round(baseAmt, 2, MidpointRounding.AwayFromZero);
                taxAmt = Math.Round(taxAmt, 2, MidpointRounding.AwayFromZero);
                netAmt = Math.Round(netAmt, 2, MidpointRounding.AwayFromZero);

                // 🔹 6️⃣ FINAL OUTPUT
                txtPreTaxAmount_pack.Text = baseAmt.ToString("N2");
                txtTotalTaxAmount_pack.Text = taxAmt.ToString("N2");
                txtGrandTotal_pack.Text = netAmt.ToString("N2");
                txtPackTaxPer.Text = totalPercent.ToString("N2");
            }
            catch (Exception ex)
            {
                clsgen.LogException("PartyBooking - CalculatePackValues()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
            }
        }

        public void CalculateDiscountAmount()
        {
            try
            {
                if (string.IsNullOrEmpty(discountper.Text)) discountper.Text = "0";
                if (string.IsNullOrEmpty(txtGrandTotal.Text)) txtGrandTotal.Text = "0";

                decimal discountPer = Convert.ToDecimal(discountper.Text);
                decimal grandTotal = Convert.ToDecimal(txtGrandTotal.Text);

                if (discountPer > 100)
                {
                    CMessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    discountper.Text = "0";
                    discountper.Focus();
                    return;
                }

                decimal discountAmt = grandTotal * discountPer / 100;
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
                decimal grandTotal = Convert.ToDecimal(txtGrandTotal.Text);

                if (discountAmt > grandTotal)
                {
                    CMessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTotalDiscountAmt.Text = "0";
                    txtTotalDiscountAmt.Focus();
                    return;
                }

                decimal discountPer = grandTotal == 0 ? 0 : (discountAmt / grandTotal * 100);
                discountper.Text = discountPer.ToString("N2");

                CalculateNetAmount();
            }
            catch (Exception)
            {

            }
        }

        private void txtOAAmount_TextChanged(object sender, EventArgs e) => CalculateNetAmount();
        private void txtRcvAmt_TextChanged(object sender, EventArgs e) => CalculateNetAmount();
        private void discountper_TextChanged(object sender, EventArgs e)
        {
            CalculateDiscountAmount();
            RecalculateAllCakeRows();
            CalculatePackValues();
        }
        private void txtTotalDiscountAmt_TextChanged(object sender, EventArgs e)
        {
            CalculateDiscountPer();
            RecalculateAllCakeRows();
            CalculatePackValues();
        }
        private void txtODAmount_TextChanged(object sender, EventArgs e) => CalculateNetAmount();

        #endregion

        #region Cake
        private void btncakeclose_Click(object sender, EventArgs e)
        {
            pnlcake.Visible = false;
        }
        private void cmbcaketype_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbcaketype.SelectedValue == null) return;

                _cakeId = Convert.ToInt32(cmbcaketype.SelectedValue);

                BllSearch<DmnPartyPackCake> bll = new BllSearch<DmnPartyPackCake>();

                DmnSearch search = new DmnSearch();
                search.Select = "*";
                search.Table = "tblPartyPackCake";
                search.WhereNdGroupBy = "WHERE PartyPackCakeId = " + _cakeId;

                var list = bll.GetSearchData("prdSearch", search);

                if (list.Count > 0)
                {
                    var item = list[0];

                    // Basic
                    _cakeSize = item.CakeSize;
                    _cakeShape = item.CakeShape;
                    _cakePrice = Convert.ToDecimal(item.Price);

                    // Tax
                    _cakeTaxType = item.TaxType;             // GST / VAT
                    _cakeTaxSettingsId = item.TaxSettingsId;
                    _cakeTaxMode = item.Tax;             // Inclusive / Exclusive

                    // UI
                    txtcakesize.Text = _cakeSize;
                    txtcakeshape.Text = _cakeShape;
                    txtcakeprice.Text = _cakePrice.ToString();


                    txtcakemessage.Clear();
                    txtcakeremarks.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ResetCakeFields()
        {
            txtcakeremarks.Text = "";
            txtcakemessage.Text = "";
            txtcakeshape.Text = "";
            txtcakeprice.Text = "0";
            cmbcaketype.SelectedIndex = -1; 

        }
        private void btnaddcakes_Click(object sender, EventArgs e)
        {
            try
            {
    
                if (cmbcaketype.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cmbcaketype.Text))
                {
                    MessageBox.Show("Please select cake item!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbcaketype.Focus();
                    return;
                }

                foreach (DataGridViewRow dgRow in dgvcake.Rows)
                {
                    if (dgRow.IsNewRow) continue;

                    if (Convert.ToInt32(dgRow.Cells[CakeIdno].Value) == _cakeId)
                    {
                        MessageBox.Show("This cake is already added!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                _cakeMessage = txtcakemessage.Text;
                _cakeRemarks = txtcakeremarks.Text;

                decimal baseAmt = 0;
                decimal taxAmt = 0;
                decimal netAmt = 0;

                decimal igst = 0;
                decimal cgst = 0;
                decimal sgst = 0;
                decimal vat = 0;

                decimal totalPercent = 0;

                if (_cakeTaxSettingsId > 0)
                {
                    int taxSettingId = Convert.ToInt32(_cakeTaxSettingsId);

                    BllSearch<DmnTaxSettings> _bllSearch1 = new BllSearch<DmnTaxSettings>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "TaxType, IgstPrcnt, CgstPrcnt, SgstPrcnt, VatPrcnt";
                    dmnSearch.Table = "tblTaxSettings";
                    dmnSearch.WhereNdGroupBy = $"WHERE Status=1 AND TaxSettingsId={taxSettingId}";

                    var taxSettings = _bllSearch1.GetSearchData("prdSearch", dmnSearch);

                    if (taxSettings.Count > 0)
                    {
                        igst = taxSettings[0].IgstPrcnt;
                        cgst = taxSettings[0].CgstPrcnt;
                        sgst = taxSettings[0].SgstPrcnt;
                        vat = taxSettings[0].VatPrcnt;

                        totalPercent = igst + vat;
                        _cakeTaxType = taxSettings[0].TaxType;
                        _cakeTaxPer = totalPercent;
                    }
                }

                decimal price = _cakePrice;

                // 🔹 Inclusive
                if (_cakeTaxMode == "Inclusive")
                {
                    baseAmt = price / (1 + (totalPercent / 100));
                    taxAmt = price - baseAmt;
                    netAmt = price;
                }
                else // 🔹 Exclusive
                {
                    baseAmt = price;
                    taxAmt = price * totalPercent / 100;
                    netAmt = price + taxAmt;
                }

                baseAmt = Math.Round(baseAmt, 2, MidpointRounding.AwayFromZero);
                taxAmt = Math.Round(taxAmt, 2, MidpointRounding.AwayFromZero);
                netAmt = Math.Round(netAmt, 2, MidpointRounding.AwayFromZero);

                int rowIndex = dgvcake.Rows.Add();
                DataGridViewRow row = dgvcake.Rows[rowIndex];

                row.Cells[CakeSlNo].Value = rowIndex + 1;
                row.Cells[CakeItemNo].Value = cmbcaketype.Text;
                row.Cells[CakeQtyNo].Value = 1;

                row.Cells[CakeAmountNo].Value = price;
                row.Cells[CakeTaxAmtNo].Value = taxAmt;

                row.Cells[CakeTaxNo].Value = _cakeTaxMode;
                row.Cells[CakeTaxTypeNo].Value = _cakeTaxType;
                row.Cells[CakeTaxPerNo].Value = totalPercent;


                if (_cakeTaxType == "GST")
                {
                    if (igst > 0)
                    {
                        row.Cells[CakeIgstAmtNo].Value = Math.Round(taxAmt * igst / totalPercent, 2);
                    }
                    else
                    {
                        decimal cgstAmt = Math.Round(taxAmt * cgst / totalPercent, 2);
                        decimal sgstAmt = Math.Round(taxAmt * sgst / totalPercent, 2);

                        row.Cells[CakeIgstAmtNo].Value = cgstAmt + sgstAmt;
                    }

                    row.Cells[CakeVatAmtNo].Value = 0;
                }
                else // VAT
                {
                    row.Cells[CakeVatAmtNo].Value = taxAmt;
                    row.Cells[CakeIgstAmtNo].Value = 0;
                }

                row.Cells[CakeNetAmtNo].Value = netAmt;
                row.Cells[CakeIdno].Value = _cakeId;
                row.Cells[Cakemessageno].Value = _cakeMessage;
                row.Cells[Cakeremarksno].Value = _cakeRemarks;

                CalculateGrandTotal_Cake();
                ResetCakeFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RecalculateCakeRow(int rowIndex)
        {
            try
            {
                if (rowIndex < 0) return;

                DataGridViewRow row = dgvcake.Rows[rowIndex];
                if (row.IsNewRow) return;

                decimal qty = 0;
                decimal price = 0;
                decimal discountPercent = 0;

                decimal.TryParse(Convert.ToString(row.Cells[CakeQtyNo].Value), out qty);
                decimal.TryParse(Convert.ToString(row.Cells[CakeAmountNo].Value), out price); 
                decimal.TryParse(discountper.Text, out discountPercent);

                if (qty <= 0) qty = 1;

                decimal totalPercent = 0;
                decimal.TryParse(Convert.ToString(row.Cells[CakeTaxPerNo].Value), out totalPercent);

                string taxMode = Convert.ToString(row.Cells[CakeTaxNo].Value);
                string taxType = Convert.ToString(row.Cells[CakeTaxTypeNo].Value); 

                decimal amount = qty * price;

                decimal discountAmt = amount * discountPercent / 100;
                decimal afterDiscount = amount - discountAmt;

                decimal baseAmt = 0;
                decimal taxAmt = 0;
                decimal netAmt = 0;


                if (taxMode == "Inclusive")
                {
                    baseAmt = afterDiscount / (1 + (totalPercent / 100));
                    taxAmt = afterDiscount - baseAmt;
                    netAmt = afterDiscount;
                }
                else // Exclusive
                {
                    baseAmt = afterDiscount;
                    taxAmt = afterDiscount * totalPercent / 100;
                    netAmt = afterDiscount + taxAmt;
                }


                baseAmt = Math.Round(baseAmt, 2, MidpointRounding.AwayFromZero);
                taxAmt = Math.Round(taxAmt, 2, MidpointRounding.AwayFromZero);
                netAmt = Math.Round(netAmt, 2, MidpointRounding.AwayFromZero);


                row.Cells[CakeTaxAmtNo].Value = taxAmt;
                row.Cells[CakeNetAmtNo].Value = netAmt;

                if (taxType == "GST")
                {
                    decimal igst = 0, cgst = 0, sgst = 0;
                    if (totalPercent > 0)
                    {
                        cgst = taxAmt / 2;
                        sgst = taxAmt / 2;
                    }

                    row.Cells[CakeIgstAmtNo].Value = Math.Round(cgst + sgst, 2);
                    row.Cells[CakeVatAmtNo].Value = 0;
                }
                else // VAT
                {
                    row.Cells[CakeVatAmtNo].Value = taxAmt;
                    row.Cells[CakeIgstAmtNo].Value = 0;
                }

                CalculateGrandTotal_Cake();
            }
            catch
            {
            }
        }
        private void RecalculateAllCakeRows()
        {
            for (int i = 0; i < dgvcake.Rows.Count; i++)
            {
                if (!dgvcake.Rows[i].IsNewRow)
                {
                    RecalculateCakeRow(i);
                }
            }

            CalculateGrandTotal_Cake();
        }
        private void CalculateGrandTotal_Cake()
        {
            decimal grandTotal = 0;
            decimal totalPreTax = 0;
            decimal totalTax = 0;

            for (int i = 0; i < dgvcake.Rows.Count; i++)
            {
                if (!dgvcake.Rows[i].IsNewRow)
                {
                    decimal qty = 0, price = 0, discountPercent = 0, taxPercent = 0;

                    decimal.TryParse(Convert.ToString(dgvcake.Rows[i].Cells[CakeQtyNo].Value), out qty);
                    decimal.TryParse(Convert.ToString(dgvcake.Rows[i].Cells[CakeAmountNo].Value), out price);
                    decimal.TryParse(discountper.Text, out discountPercent);
                    decimal.TryParse(Convert.ToString(dgvcake.Rows[i].Cells[CakeTaxPerNo].Value), out taxPercent);

                    if (qty <= 0) qty = 1;

                    string taxMode = Convert.ToString(dgvcake.Rows[i].Cells[CakeTaxNo].Value);

                    decimal amount = qty * price;
                    decimal discountAmt = amount * discountPercent / 100;
                    decimal afterDiscount = amount - discountAmt;

                    decimal baseAmt = 0;
                    decimal taxAmt = 0;
                    decimal netAmt = 0;

                    if (taxMode == "Inclusive")
                    {
                        baseAmt = afterDiscount / (1 + (taxPercent / 100));
                        taxAmt = afterDiscount - baseAmt;
                        netAmt = afterDiscount;
                    }
                    else
                    {
                        baseAmt = afterDiscount;
                        taxAmt = afterDiscount * taxPercent / 100;
                        netAmt = afterDiscount + taxAmt;
                    }

                    baseAmt = Math.Round(baseAmt, 2);
                    taxAmt = Math.Round(taxAmt, 2);
                    netAmt = Math.Round(netAmt, 2);

                    totalPreTax += baseAmt;
                    totalTax += taxAmt;
                    grandTotal += netAmt;
                }
            }

            txtCakeGrandTotal.Text = grandTotal.ToString("0.00");


            txtPreTaxAmount_cake.Text = totalPreTax.ToString("0.00");
            txtTotalTaxAmount_cake.Text = totalTax.ToString("0.00");
        }
        private void dgvcake_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var row = dgvcake.Rows[e.RowIndex];
                if (e.ColumnIndex == CakeQtyNo)
                {
                    decimal qty = 0;
                    decimal.TryParse(Convert.ToString(row.Cells[CakeQtyNo].Value), out qty);
                    if (qty <= 0)
                    {
                        MessageBox.Show("Quantity must be greater than 0!", "Validation",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        row.Cells[CakeQtyNo].Value = 1;
                        dgvcake.CurrentCell = row.Cells[CakeQtyNo];
                        dgvcake.BeginEdit(true);
                        return;
                    }
                }
                if (e.ColumnIndex == CakeQtyNo || e.ColumnIndex == CakeAmountNo)
                {
                    RecalculateCakeRow(e.RowIndex);
                    CalculateGrandTotal_Cake();
                    CalculateNetAmount();
                }
            }
            catch
            {
            }
        }
        private void dgvcake_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == CakeActionNo)
                {
                    var result = MessageBox.Show("Remove this cake?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        dgvcake.Rows.RemoveAt(e.RowIndex);


                        for (int i = 0; i < dgvcake.Rows.Count; i++)
                        {
                            if (!dgvcake.Rows[i].IsNewRow)
                                dgvcake.Rows[i].Cells[CakeSlNo].Value = i + 1;
                        }

                        CalculateGrandTotal_Cake();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btncake_Click(object sender, EventArgs e)
        {
            pnlcake.Visible = true;
            pnlcake.BringToFront();
            pnlcake.Left = (this.ClientSize.Width - pnlcake.Width) / 2;
            pnlcake.Top = (this.ClientSize.Height - pnlcake.Height) / 2;
        }
        private void btncakesubmit_Click(object sender, EventArgs e)
        {
            try
            {
                decimal cakeTotal = 0;

                foreach (DataGridViewRow row in dgvcake.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        decimal net = 0;
                        decimal.TryParse(Convert.ToString(row.Cells[CakeNetAmtNo].Value), out net);
                        cakeTotal += net;
                    }
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
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Common

        private void LoadPackRelatedItems(int partyPackId)
        {
            try
            {
                dgv_card.Rows.Clear();
                dgv_gift.Rows.Clear();

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

                int cardSl = 1;
                int giftSl = 1;

                foreach (var item in list)
                {
                    // ✅ CARD ITEMS
                    if ((PackItemType)item.PackItemType == PackItemType.Card)
                    {
                        int rowIndex = dgv_card.Rows.Add();

                        DataGridViewRow row = dgv_card.Rows[rowIndex];

                        row.Cells[Cardslno].Value = cardSl++;
                        row.Cells[Carditemno].Value = item.Item;
                        row.Cells[Cardcategmenuno].Value = item.MenuCategoryId;
                        row.Cells[Cardqtyno].Value = item.Quantity;
                        row.Cells[CardProductid].Value = item.Productid;
                        row.Cells[Cardproducttype].Value = item.ProductType;
                        row.Cells[CardPartyPackDetailsId].Value = item.PartyPackDetailsId;
                        row.Cells[CardPartydeafult].Value = 1;
                        row.ReadOnly = true;
                    }

                    // ✅ GIFT ITEMS
                    else if ((PackItemType)item.PackItemType == PackItemType.Gift)
                    {
                        int rowIndex = dgv_gift.Rows.Add();

                        DataGridViewRow row = dgv_gift.Rows[rowIndex];

                        row.Cells[Giftslno].Value = giftSl++;
                        row.Cells[Giftitemno].Value = item.Item;
                        row.Cells[Giftqtyno].Value = item.Quantity;
                        row.Cells[GiftProductid].Value = item.Productid;
                        row.Cells[Giftproducttype].Value = item.ProductType;
                        row.Cells[GiftPartyPackDetailsId].Value = item.PartyPackDetailsId;
                        row.Cells[GiftPartydeafult].Value = 1;
                        row.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btndispanel_Click(object sender, EventArgs e)
        {
            decimal kdata = 0;
            using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.percentage)))
            {
                frm.ShowDialog();

                if (!string.IsNullOrEmpty(frm.txtkeypadvalue.Text))
                    kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                txtdiscountreason.Text = frm.txtdesc.Text;
            }
            if (Frmnumdiskeypad.flagpercentage)
            {
                discountper.Text = kdata.ToString("0.00");

                ApplyDiscountToCardRows(kdata, 0, true);
                ApplyDiscountToGiftRows(kdata, 0, true);

                CalculateDiscountAmount();
                CalculatePackValues();
            }
            else
            {
                txtTotalDiscountAmt.Text = kdata.ToString("0.00");

                ApplyDiscountToCardRows(0, kdata, false);
                ApplyDiscountToGiftRows(kdata, 0, false);

                CalculateDiscountPer();
                CalculatePackValues();
            }

            CalculateNetAmount();
        }
        private void txtadults_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalGuests();
            UpdateGrandTotal();

            CalculatePackValues();

            CalculateDiscountAmount();
            CalculateNetAmount();
        }

        private void txtchild_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalGuests();
            UpdateGrandTotal();

            CalculatePackValues();

            CalculateDiscountAmount();
            CalculateNetAmount();
        }

        public void Memoryrelease()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void dgv_card_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (e.ColumnIndex == Cardactionno)
            {
                var row = dgv_card.Rows[e.RowIndex];
                int partyDefault = 0;
                if (row.Cells[CardPartydeafult].Value != null)
                    int.TryParse(row.Cells[CardPartydeafult].Value.ToString(), out partyDefault);

                if (partyDefault == 0)
                {
                    dgv_card.Rows.RemoveAt(e.RowIndex);
                    CalculateGrandTotal_Card();
                }
                else
                {
                    MessageBox.Show("This card is a default party item and cannot be deleted.",
                                    "Delete Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
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

                txtGrandTotal.Text = "0.00";
                txtCakeGrandTotal.Text = "0.00";
                txtCardGrandTotal.Text = "0.00";
                txtGiftGrandTotal.Text = "0.00";

                txttax.Text = "0.00";

                txtPreTaxAmount.Text = "0.00";
                txtPreTaxAmount_cake.Text = "0.00";
                txtPreTaxAmount_card.Text = "0.00";
                txtPreTaxAmount_gift.Text = "0.00";

                txtTotalTaxAmount.Text = "0.00";
                txtTotalTaxAmount_card.Text = "0.00";
                txtTotalTaxAmount_cake.Text = "0.00";
                txtTotalTaxAmount_gift.Text = "0.00";

                sterms.Text = "";

                txtTaxSettingId.Text = "0";

                txtChild.Text = "0";
                txtAdults.Text = "0";
                txttotalguest.Text = "0";

                txtChildPrice.Text = "0.00";
                txtAdultPrice.Text = "0.00";

                lblNetAmt.Text = "0.00";
                lblBalAmt.Text = "0.00";
                txtRcvAmt.Text = "0.00";

                dgv_card.Rows.Clear();   
                dgv_cus_menu_items.Rows.Clear(); 
                dgv_gift.Rows.Clear();       
                dgv_packenter.Rows.Clear();
                dgv_packs.Rows.Clear();
                dgv_packs_items.Rows.Clear();
                dgvcake.Rows.Clear();
           
                Frmnumdiskeypad.flagpercentage = false;
                txtAdults.Focus();

                pnlcake.Visible = false;
                pnlcards.Visible = false;
                pnlgiftpreview.Visible = false;
                pnlgifts.Visible = false;

                pnlSplit.Visible = false;
                pnl_CustomItems_menu.Visible = false;
                pnl_enter_paks.Visible = false;
                pnl_foodpacks.Visible = false;
                pnl_pak_detailsitems.Visible = false;
                pnlPaymode.Visible = false;
                pnlcake.Visible = false;

                pnlgifts.Visible = false;
                pnlcards.Visible = false; 
                pnl_foodpacks.Visible = false;

                lblpackname.Visible = false;
                lblpacklabel.Visible = false;

                txtpartybookingid.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in ClearAll: " + ex.Message);
            }
        }

        #endregion


            private void txtph_KeyPress(object sender, KeyPressEventArgs e)
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
            if (!IsValidEmail(txtemail.Text))
            {
                MessageBox.Show("Please enter a valid email address");
                txtemail.Focus();
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

        private void btn_paynow_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgv_packs.CurrentRow == null)
                {
                    MessageBox.Show("Please select a Party Pack", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int adults = GetInt(txtAdults);
                int children = GetInt(txtChild);

                if (adults == 0 && children == 0)
                {
                    MessageBox.Show("Please enter number of attendees (Adults or Children)", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAdults.Focus();
                    return;
                }
  
                //if (dgv_packs_items.Rows.Count == 0)
                //{
                //    MessageBox.Show("No pack items found. Please select valid pack.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}


                pnlPaymode.Visible = true;
                pnlPaymode.BringToFront();
                pnlPaymode.Left = (this.ClientSize.Width - pnlPaymode.Width) / 2;
                pnlPaymode.Top = (this.ClientSize.Height - pnlPaymode.Height) / 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnadvance_Click(object sender, EventArgs e)
        {
            decimal kdata = 0;
            using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.percentage)))
            {
                frm.ShowDialog();

                if (!string.IsNullOrEmpty(frm.txtkeypadvalue.Text))
                    kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                txtdiscountreason.Text = frm.txtdesc.Text;
            }
            if (kdata > 0)
            {
                AdvanceFlag = true;
                txtadvanceamt.Text = kdata.ToString("N2");
                txtRcvAmt.Text = kdata.ToString("N2");
                pnlSplit.Visible = true;
                pnlSplit.BringToFront();
                pnlSplit.Left = (this.ClientSize.Width - pnlSplit.Width) / 2;
                pnlSplit.Top = (this.ClientSize.Height - pnlSplit.Height) / 2;
                txtadvanceamt.Text = txtRcvAmt.Text;
            }
            Paybill();
        }

        private void txtRcvAmt_Click(object sender, EventArgs e)
        {
            decimal kdata = 0;
            using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.percentage)))
            {
                frm.ShowDialog();

                if (!string.IsNullOrEmpty(frm.txtkeypadvalue.Text))
                    kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                txtdiscountreason.Text = frm.txtdesc.Text;
            }
            txtRcvAmt.Text = kdata.ToString("N2");
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
                MessageBox.Show(ex.Message);
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
        public  void Paybill()
        {
            try
            {
                GetInt(txtpartybookingid);
                txtTaxPer_pack

                BllPartyBooking bll = new BllPartyBooking();

                // ==========================
                // 🔹 MASTER SAVE
                // ==========================
                DmnPartyBooking booking = new DmnPartyBooking();

                booking.PartyBookingId = Convert.ToInt32(txtpartybookingid.Text);
                booking.PartyPackId = Partypackid;

                // 🔹 CUSTOMER INFO
                booking.CustomerName = txtname.Text?.Trim();
                booking.Phone = txtph.Text?.Trim();
                booking.Email = txtemail.Text?.Trim();
                booking.Address = txtaddress.Text?.Trim();

                // 🔹 EVENT INFO
                booking.EventType =Convert.ToInt32(cmbevent.SelectedValue);
                booking.EventDescription = txteventdesc.Text?.Trim();

                // 🔹 DATE FIX
                booking.BookingDate = DateTime.Now.Date;
                booking.BookingTime = DateTime.Today.Add(DateTime.Now.TimeOfDay);

                // 🔹 PEOPLE COUNT
                booking.NoOfAdults = GetInt(txtAdults);
                booking.NoOfChildren = GetInt(txtChild);

                // 🔹 PACK PRICE
                decimal adultPrice = GetDecimal(txtAdultPrice);
                decimal childPrice = GetDecimal(txtChildPrice);

                if (booking.NoOfAdults > 0 && booking.NoOfChildren > 0)
                {
                    booking.PackPrice = adultPrice + childPrice;
                }
                else if (booking.NoOfAdults > 0)
                {
                    booking.PackPrice = adultPrice;
                }
                else if (booking.NoOfChildren > 0)
                {
                    booking.PackPrice = childPrice;
                }
                else
                {
                    booking.PackPrice = 0;
                }

                // 🔹 TAX
                booking.Tax = txttax.Text;
                booking.TaxType = txtTaxtype_pack.Text;
                booking.TaxPer = GetDecimal(txtTaxPer_pack);
                booking.TaxAmount = GetDecimal(txtTotalTaxAmount);

                // 🔹 DISCOUNT
                booking.DiscountPer = GetDecimal(txtDiscountPer);
                booking.DiscountAmount = GetDecimal(txtTotalDiscountAmt);
                booking.DiscountReason = txtdiscountreason.Text;

                // 🔹 AMOUNTS
                booking.PreTaxAmount = GetDecimal(txtPreTaxAmount);
                booking.PostTaxAmount = GetDecimal(txtTotalTaxAmount);
                booking.NetAmount = GetDecimalFromLabel(lblNetAmt);

                // 🔹 PAYMENT
                booking.AdvanceAmount = GetDecimal(txtadvanceamt);
                booking.ReceivedAmount = GetDecimal(txtRcvAmt);
                booking.BalanceAmount = 0; // booking.NetAmount - booking.ReceivedAmount;
                booking.PaymentMode = 0; //PaymentMode;

                // 🔹 AUDIT
                booking.CreatedBy = FrmLogin.EmployeeId;
                booking.BranchId = FrmDashboard.BranchId;
                booking.Status = 1;

                // ==========================
                // 🔹 SAVE MASTER
                // ==========================
                var result = bll.AddEditData("prdInsert_Upd_PartyBooking", booking);

                if (result.PartyBookingId <= 0)
                {
                    MessageBox.Show("Master Save Failed");
                    return;
                }

                int bookingId = result.PartyBookingId;

                #region SAVE PAYMENT

                BllPartyBookingPayment bllPayment = new BllPartyBookingPayment();
                if (bookingId > 0)
                {
                    DmnPartyBookingPayment payment = new DmnPartyBookingPayment
                    {
                        PaymentId = 0,
                        PartyBookingId = bookingId,
                        PaymentMode = PaymentMode,
                        PaymentAmount = booking.NetAmount,
                        RcvAmnt = booking.ReceivedAmount,
                        BalAmnt = booking.NetAmount - booking.ReceivedAmount,
                        IsAdvance = AdvanceFlag,
                        ReferenceNo = "",
                        PaymentDescription = "",
                        CreatedBy = FrmLogin.EmployeeId,
                        BranchId = FrmDashboard.BranchId,
                        Status = 1
                    };

                    var paymentResult = bllPayment.AddEditPayment(payment);
                    if (paymentResult.PaymentId <= 0)
                    {
                        MessageBox.Show("Payment save failed.");
                    }
                    else
                    {
                    }
                }
                #endregion

                #region INSERT PARTY PACK AS DETAILS

                DmnPartyBookingDetails packDetails = new DmnPartyBookingDetails();

                packDetails.PartyBookingDetailsId = 0;
                packDetails.PartyBookingId = bookingId;

                packDetails.ProductId = Partypackid;
                packDetails.ProductType = Convert.ToInt32(PackItemType.Pack);
                packDetails.TableType = 0;

                packDetails.Qty = 1;

                packDetails.Tax = txttax.Text;

                packDetails.TaxType = txtTaxtype_pack.Text;
                packDetails.TaxPer = Convert.ToDecimal(txtTaxPer_pack.Text);

                packDetails.TaxAmount = Convert.ToDecimal(txtTotalTaxAmount_pack.Text);

                packDetails.PreTaxAmount = Convert.ToDecimal(txtPreTaxAmount.Text);
                packDetails.NetAmount = Convert.ToDecimal(txtGrandTotal.Text);

                packDetails.CreatedBy = FrmDashboard.UserId;
                packDetails.BranchId = FrmDashboard.BranchId;
                packDetails.Status = 1;

                bll.AddEditDataDetails("prdInsert_Upd_PartyBookingDetails", packDetails);

                #endregion

                // ======================================
                // 🔥 2️⃣ INSERT CARD ITEMS
                // CardPartyDefault = 0
                // ======================================
                foreach (DataGridViewRow row in dgv_card.Rows)
                {
                    if (row.IsNewRow) continue;

                    int cardDefault = Convert.ToInt32(row.Cells[CardPartydeafult].Value ?? 0);

                    // 🔥 INSERT ONLY IF DEFAULT = 0
                    if (cardDefault == 0)
                    {
                        DmnPartyBookingDetails det = new DmnPartyBookingDetails();

                        det.PartyBookingDetailsId = 0;
                        det.PartyBookingId = bookingId; 

                        det.Product = Convert.ToString(row.Cells[Carditemno].Value ?? "");
                        det.ProductId = Convert.ToInt32(row.Cells[CardProductid].Value ?? 0);
                        det.ProductType = Convert.ToInt32(PackItemType.Card);
                        det.TableType = Convert.ToInt32(ProductType.Card);

                        det.Qty = Convert.ToDecimal(row.Cells[Cardqtyno].Value ?? 0);
                        det.Price = Convert.ToDecimal(row.Cells[CardPrice].Value ?? 0);

                        det.Tax = Convert.ToString(row.Cells[Cardtax].Value ?? "");
                        det.TaxType = Convert.ToString(row.Cells[Cardtaxtype].Value ?? "");
                        det.TaxPer = Convert.ToDecimal(row.Cells[Cardtaxper].Value ?? 0);
                        det.TaxAmount = Convert.ToDecimal(row.Cells[Cardtaxamt].Value ?? 0);

                        det.PreTaxAmount = Convert.ToDecimal(row.Cells[Cardpretaxamt].Value ?? 0);
                        det.NetAmount = Convert.ToDecimal(row.Cells[Cardnetamt].Value ?? 0);

                        det.CreatedBy = FrmDashboard.UserId;
                        det.BranchId = FrmDashboard.BranchId;
                        det.Status = 1;

                        bll.AddEditDataDetails("prdInsert_Upd_PartyBookingDetails", det);
                    }
                }

                // ======================================
                // 🔥 3️⃣ INSERT GIFT ITEMS
                // GiftPartyDefault = 0
                // ======================================
                foreach (DataGridViewRow row in dgv_gift.Rows)
                {
                    if (row.IsNewRow) continue;

                    int giftDefault = Convert.ToInt32(row.Cells[GiftPartydeafult].Value ?? 0);

                    // 🔥 INSERT ONLY IF DEFAULT = 0
                    if (giftDefault == 0)
                    {
                        DmnPartyBookingDetails det = new DmnPartyBookingDetails();

                        det.PartyBookingDetailsId = 0;
                        det.PartyBookingId = bookingId;

                        det.Product = Convert.ToString(row.Cells[Giftitemno].Value ?? "");
                        det.ProductId = Convert.ToInt32(row.Cells[GiftProductid].Value ?? 0);
                        det.ProductType = Convert.ToInt32(PackItemType.Gift);
                        det.TableType = Convert.ToInt32(ProductType.RedemptionTicket);

                        det.Prid = Convert.ToInt32(row.Cells[Giftprid].Value ?? 0);

                        det.Qty = Convert.ToDecimal(row.Cells[Giftqtyno].Value ?? 0);
                        det.Price = Convert.ToDecimal(row.Cells[GiftPrice].Value ?? 0);

                        det.Tax = Convert.ToString(row.Cells[Gifttax].Value ?? "");
                        det.TaxType = Convert.ToString(row.Cells[Gifttaxtype].Value ?? "");
                        det.TaxPer = Convert.ToDecimal(row.Cells[Gifttaxper].Value ?? 0);
                        det.TaxAmount = Convert.ToDecimal(row.Cells[Gifttaxamt].Value ?? 0);

                        det.PreTaxAmount = Convert.ToDecimal(row.Cells[Giftpretaxamt].Value ?? 0);
                        det.NetAmount = Convert.ToDecimal(row.Cells[Giftnetamt].Value ?? 0);

                        det.CreatedBy = FrmDashboard.UserId;
                        det.BranchId = FrmDashboard.BranchId;
                        det.Status = 1;

                        bll.AddEditDataDetails("prdInsert_Upd_PartyBookingDetails", det);
                    }
                }


                // ======================================
                // 🔥 2️⃣ INSERT CAKE ITEMS
                // ======================================
                foreach (DataGridViewRow row in dgvcake.Rows)
                {
                    if (row.IsNewRow) continue;

                    DmnPartyBookingDetails det = new DmnPartyBookingDetails();

                    det.PartyBookingDetailsId = 0;
                    det.PartyBookingId = bookingId;

                    // 🔹 PRODUCT INFO
                    det.Product = Convert.ToString(row.Cells[CakeItemNo].Value ?? "");
                    det.ProductId = Convert.ToInt32(row.Cells[CakeIdno].Value ?? 0);
                    det.ProductType = Convert.ToInt32(PackItemType.Cake);  
                    det.TableType = 0;     

                    // 🔹 QTY & PRICE
                    det.Qty = Convert.ToDecimal(row.Cells[CakeQtyNo].Value ?? 0);
                    det.Price = Convert.ToDecimal(row.Cells[CakeAmountNo].Value ?? 0);

                    // 🔹 TAX
                    det.Tax = Convert.ToString(row.Cells[CakeTaxNo].Value ?? "");
                    det.TaxType = Convert.ToString(row.Cells[CakeTaxTypeNo].Value ?? "");
                    det.TaxPer = Convert.ToDecimal(row.Cells[CakeTaxPerNo].Value ?? 0);
                    det.TaxAmount = Convert.ToDecimal(row.Cells[CakeTaxAmtNo].Value ?? 0);
                    det.TaxSettingId = Convert.ToInt32(row.Cells[CaketaxsettingidNo].Value ?? 0);

                    // 🔹 AMOUNT
                    decimal netAmt = Convert.ToDecimal(row.Cells[CakeNetAmtNo].Value ?? 0);
                    decimal taxAmt = Convert.ToDecimal(row.Cells[CakeTaxAmtNo].Value ?? 0);

                    det.NetAmount = netAmt;
                    det.PreTaxAmount = Convert.ToDecimal(txtPreTaxAmount_cake.Text);
                    det.PostTaxAmount = Convert.ToDecimal(txtTotalTaxAmount_cake.Text);

                    
                    det.CakeRemarks = Convert.ToString(row.Cells[Cakeremarksno].Value ?? "");
                    det.CakeMessage = Convert.ToString(row.Cells[Cakemessageno].Value ?? "");

                    // 🔹 AUDIT
                    det.CreatedBy = FrmDashboard.UserId;
                    det.BranchId = FrmDashboard.BranchId;
                    det.Status = 1;

                    bll.AddEditDataDetails("prdInsert_Upd_PartyBookingDetails", det);
                }


                MessageBox.Show("Party Booking Saved Successfully ✅");
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

        }

        private void btnclos_payment_Click(object sender, EventArgs e)
        {
            pnlPaymode.Visible = false;
        }

        #region Search Party Booking
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                int returnValue = 0;

                DmnSearch search = new DmnSearch();
                search.ColumnHeaders = "ID,Customer,Event,Date";
                search.Select = "PartyBookingId as Id, CustomerName as Field1, EventType as Field2, BookingDate as Field3";
                search.Table = "tblPartyBooking";
                search.WhereNdGroupBy = "WHERE Status = 1";
                search.OrderBy = " ORDER BY BookingDate DESC";

                var gridSource = bllSearch.GetSearchData("prdSearch", search);

                if (gridSource.Count == 0)
                {
                    MessageBox.Show("No Records Found");
                    return;
                }

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

                if (returnValue <= 0) return;

                LoadBookingMaster(returnValue, Convert.ToInt32(txtpartpackid.Text));

                LoadDefaultPackAndBookingItems(Convert.ToInt32(txtpartpackid.Text), returnValue);

                lblpackname.Visible = true;
                lblpacklabel.Visible = true;
                lblpackname.Text = SetPackName(Convert.ToInt32(txtpartpackid.Text));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void LoadBookingMaster(int bookingId,int packid)
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
            txtTotalDiscountAmt.Text = result.DiscountAmount.ToString("0.00");

            txtTaxtype_pack.Text = result.TaxType;
            txtTotalTaxAmount.Text = result.Tax;
            lblNetAmt.Text = result.NetAmount.ToString("0.00");

            txtpartpackid.Text = result.PartyPackId.ToString();
            txtpartybookingid.Text = result.PartyBookingId.ToString();

            // 🔹 EVENT DATE & TIME
            if (result.BookingDate != null)
                dtpEventDate.Value = result.BookingDate;

            if (result.BookingTime != null)
                dtpEventTime.Value = result.BookingTime;

            try
            {
                BllSearch<DmnPartyPack> bllMaster = new BllSearch<DmnPartyPack>();
                DmnSearch searchMaster = new DmnSearch
                {
                    Select = "PartyPackId, PackName, AdultPrice, ChildPrice, DiscountPer, DiscountAmt, TaxType, Tax",
                    Table = "tblPartyPackMaster",
                    WhereNdGroupBy = $"WHERE PartyPackId = {packid} AND Status = 1"
                };

                var master = bllMaster.GetSearchData("prdSearch", searchMaster).FirstOrDefault();

                if (master != null)
                {
                    txtAdultPrice.Text = master.AdultPrice != null ? Convert.ToDecimal(master.AdultPrice).ToString("0.00") : "0.00";
                    txtChildPrice.Text = master.ChildPrice != null ? Convert.ToDecimal(master.ChildPrice).ToString("0.00") : "0.00";

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

                    if ((master.ChildPrice > 0) && (master.AdultPrice > 0))
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
                MessageBox.Show(ex.Message);
            }

        }

        private void LoadDefaultPackAndBookingItems(int partyPackId, int bookingId)
        {
            try
            {
                LoadMenuCategory_productview();

                dgv_card.Rows.Clear();
                dgv_gift.Rows.Clear();
                dgvcake.Rows.Clear();
                dgv_packenter.Rows.Clear();

                int cardSl = 1;
                int giftSl = 1;
                int packSl = 1;
                
                // =======================================
                // 🔹 1️⃣ Load Default Pack Items (tblPartyPackDetails)
                // =======================================
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
                    // 🔹 Card
                    if ((PackItemType)item.PackItemType == PackItemType.Card)
                    {
                        int rowIndex = dgv_card.Rows.Add();
                        var row = dgv_card.Rows[rowIndex];
                        row.Cells[Cardslno].Value = cardSl++;
                        row.Cells[Carditemno].Value = item.Item;
                        row.Cells[Cardqtyno].Value = item.Quantity;
                        //row.Cells[CardProductid].Value = item.ProductId;
                        row.Cells[Cardproducttype].Value = item.ProductType;
                        row.Cells[CardPartyPackDetailsId].Value = item.PartyPackDetailsId;
                        row.Cells[CardPartydeafult].Value = 0; 
                        row.ReadOnly = true;
                    }
                    // 🔹 Gift
                    else if ((PackItemType)item.PackItemType == PackItemType.Gift)
                    {
                        int rowIndex = dgv_gift.Rows.Add();
                        var row = dgv_gift.Rows[rowIndex];
                        row.Cells[Giftslno].Value = giftSl++;
                        row.Cells[Giftitemno].Value = item.Item;
                        row.Cells[Giftqtyno].Value = item.Quantity;
                        //row.Cells[GiftProductid].Value = item.ProductId;
                        row.Cells[Giftproducttype].Value = item.ProductType;
                        row.Cells[GiftPartyPackDetailsId].Value = item.PartyPackDetailsId;
                        row.Cells[GiftPartydeafult].Value = 0; 
                        row.ReadOnly = true;
                    }
                    // 🔹 Food Pack
                    else if ((PackItemType)item.PackItemType == PackItemType.FoodItem)
                    {
                        int rowIndex = dgv_packs.Rows.Add();
                        DataGridViewRow row = dgv_packs.Rows[rowIndex];

                        row.Cells[mainslno].Value = packSl++;
                        row.Cells[mainitemno].Value = item.Item;

                        if(Convert.ToInt32(item.MenuCategoryId)>0)
                            row.Cells[maincategmenuno].Value = Convert.ToInt64(item.MenuCategoryId);

                        row.Cells[mainqtyno].Value = item.Quantity;
                        row.Cells[mainppcdiidno].Value = item.PartyPackDetailsId;
                        row.Cells[maindefaultno].Value = 0;   // 1 = Default Pack Item
                    }
                }

                // =======================================
                // 🔹 2️⃣ Load Booking-Added Items (tblPartyBookingDetails)
                // =======================================
                BllSearch<DmnPartyBookingDetails> bllBooking = new BllSearch<DmnPartyBookingDetails>();
                DmnSearch searchBooking = new DmnSearch
                {
                    Select = "*",
                    Table = "tblPartyBookingDetails",
                    WhereNdGroupBy = $"WHERE PartyBookingId = {bookingId} AND Status = 1"
                };

                var bookingItems = bllBooking.GetSearchData("prdSearch", searchBooking);

                //foreach (var item in bookingItems)
                //{
                //    // 🔹 Card
                //    if (item.ProductType == (int)PackItemType.Card)
                //    {
                //        int rowIndex = dgv_card.Rows.Add();
                //        var row = dgv_card.Rows[rowIndex];

                //        row.Cells[Cardslno].Value = cardSl++;
                //        row.Cells[Carditemno].Value = item.Product;
                //        row.Cells[Cardqtyno].Value = item.Qty;
                //        row.Cells[CardPrice].Value = item.Price;
                //        row.Cells[CardProductid].Value = item.ProductId;
                //        row.Cells[Cardproducttype].Value = item.ProductType;
                //        //row.Cells[CardPartyPackDetailsId].Value = item.PartyPackDetailsId;

                //        row.Cells[Cardtax].Value = item.Tax;
                //        row.Cells[Cardtaxtype].Value = item.TaxType;
                //        row.Cells[Cardtaxper].Value = item.TaxPer;
                //        row.Cells[Cardtaxamt].Value = item.TaxAmount;
                //        row.Cells[Cardpretaxamt].Value = item.PreTaxAmount;
                //        row.Cells[Cardnetamt].Value = item.NetAmount;

                //        row.Cells[CardPartydeafult].Value = 0; // Booking-added item
                //    }
                //    // 🔹 Gift
                //    else if (item.ProductType == (int)PackItemType.Gift)
                //    {
                //        int rowIndex = dgv_gift.Rows.Add();
                //        var row = dgv_gift.Rows[rowIndex];

                //        row.Cells[Giftslno].Value = giftSl++;
                //        row.Cells[Giftitemno].Value = item.Product;
                //        row.Cells[Giftqtyno].Value = item.Qty;
                //        row.Cells[GiftPrice].Value = item.Price;
                //        row.Cells[GiftProductid].Value = item.ProductId;
                //        row.Cells[Giftproducttype].Value = item.ProductType;
                //        //row.Cells[GiftPartyPackDetailsId].Value = item.PartyPackDetailsId;
                //        row.Cells[Giftprid].Value = item.Prid;

                //        row.Cells[Gifttax].Value = item.Tax;
                //        row.Cells[Gifttaxtype].Value = item.TaxType;
                //        row.Cells[Gifttaxper].Value = item.TaxPer;
                //        row.Cells[Gifttaxamt].Value = item.TaxAmount;
                //        row.Cells[Giftpretaxamt].Value = item.PreTaxAmount;
                //        row.Cells[Giftnetamt].Value = item.NetAmount;

                //        row.Cells[GiftPartydeafult].Value = 0; // Booking-added item
                //    }
                //    // 🔹 Cake
                //    else if (item.ProductType == (int)PackItemType.Cake)
                //    {
                //        int rowIndex = dgvcake.Rows.Add();
                //        var row = dgvcake.Rows[rowIndex];

                //        row.Cells[CakeItemNo].Value = item.Product;
                //        row.Cells[CakeQtyNo].Value = item.Qty;
                //        row.Cells[CakeAmountNo].Value = item.Price;
                //        row.Cells[CakeIdno].Value = item.ProductId;
                //        row.Cells[CakeTaxNo].Value = item.Tax;
                //        row.Cells[CakeTaxTypeNo].Value = item.TaxType;
                //        row.Cells[CakeTaxPerNo].Value = item.TaxPer;
                //        row.Cells[CakeTaxAmtNo].Value = item.TaxAmount;
                //        row.Cells[CaketaxsettingidNo].Value = item.TaxSettingId;
                //        row.Cells[CakeNetAmtNo].Value = item.NetAmount;
                //        row.Cells[Cakeremarksno].Value = item.CakeRemarks;
                //        row.Cells[Cakemessageno].Value = item.CakeMessage;
                //    }
                //    // 🔹 Food Pack (Booking-added)
                //    else if (item.ProductType == (int)PackItemType.Pack)
                //    {
                //        int rowIndex = dgv_packenter.Rows.Add();
                //        var row = dgv_packenter.Rows[rowIndex];

                //        row.Cells[selecteditemslno].Value = packSl++;
                //        row.Cells[selecteditemitemno].Value = item.Product;
                //        row.Cells[selecteditemqtyno].Value = item.Qty;
                //        //row.Cells[selecteditemcategmenuno].Value = item.MenuCategoryId;
                //        //row.Cells[selecteditemppcdiidno].Value = item.PartyPackDetailsId;
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
