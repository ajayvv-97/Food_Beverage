using BLL;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        #endregion

        public FrmPartyBooking()
        {
            InitializeComponent();
        }

        private void FrmPartyBooking_Load(object sender, EventArgs e)
        {
            LoadEventItems();
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
                            ChildPrice,
                            (SELECT COUNT(1) 
                             FROM tblPartyPackDetails d 
                             WHERE d.PartyPackId = tblPartyPackMaster.PartyPackId 
                             AND d.Status = 1) AS DetailsCount";
                search.Table = "tblPartyPackMaster";
                search.WhereNdGroupBy = "WHERE Status = 1 ";
                search.OrderBy = "ORDER BY PackName";

                var list = bll.GetSearchData("prdSearch", search);
                if(list.Count>0)
                {
                    txtchildpackprice.Text = Convert.ToString(list[0].ChildPrice);
                    txtadultpackprice.Text = Convert.ToString(list[0].AdultPrice);


                    for (int col = 0; col < tlp_reg_contrl.ColumnCount; col++)
                    {
                        Control c1 = tlp_reg_contrl.GetControlFromPosition(col, 4);
                        if (c1 != null) tlp_reg_contrl.Controls.Remove(c1);

                        Control c2 = tlp_reg_contrl.GetControlFromPosition(col, 5);
                        if (c2 != null) tlp_reg_contrl.Controls.Remove(c2);
                    }
                    txtchildpackprice.Text = list[0].ChildPrice.ToString();
                    txtadultpackprice.Text = list[0].AdultPrice.ToString();

                    int rowLabel = 4;
                    int rowTextBox = 5;

                    lbladult.Dock = DockStyle.Fill;
                    txtadults.Dock = DockStyle.Fill;

                    tlp_reg_contrl.Controls.Add(lbladult, 0, rowLabel);
                    tlp_reg_contrl.Controls.Add(txtadults, 0, rowTextBox);


                    lbltotalguest.Dock = DockStyle.Fill;
                    txttotalguest.Dock = DockStyle.Fill;

                    tlp_reg_contrl.Controls.Add(lbltotalguest, 1, rowLabel);
                    tlp_reg_contrl.Controls.Add(txttotalguest, 1, rowTextBox);

                    if (list[0].ChildPrice > 0)
                    {
                        lbladult.Dock = DockStyle.Fill;
                        txtadults.Dock = DockStyle.Fill;

                        tlp_reg_contrl.Controls.Add(lblnochild, 2, rowLabel);
                        tlp_reg_contrl.Controls.Add(txtchild, 2, rowTextBox);
                    }

                }
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
                        if (item.DetailsCount > 0)
                        {
                            OpenPackEntryPanel();
                            LoadPartyPackDetails(packId);
                        }
                        else
                        {
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
                BllSearch<DmnEventCommonItems> bllSearch =
                    new BllSearch<DmnEventCommonItems>();

                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = "EventItemId, ItemName";
                objSearch.Table = "tblEventCommonItems";
                objSearch.WhereNdGroupBy =
                    "WHERE Status = 1 AND EventCategoryType = 1 AND BranchId = "
                    + FrmDashboard.BranchId;

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
                    targetRow.Cells[mainppcdiidno].Value =     sourceRow.Cells[selecteditemppcdiidno].Value;
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

       

        private void txtadults_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalGuests();
        }

        private void txtchild_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalGuests();
        }

        private void UpdateTotalGuests()
        {
            int adults = 0;
            int children = 0;

            int.TryParse(txtadults.Text, out adults);
            int.TryParse(txtchild.Text, out children);

            txttotalguest.Text = (adults + children).ToString();
        }

        private void txtchild_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtadults_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }
        private void AllowDecimalInput(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt == null) return;

            // Allow digits, control keys (backspace), and a single decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Block invalid input
            }

            // Only allow one decimal point
            if (e.KeyChar == '.' && txt.Text.Contains("."))
            {
                e.Handled = true;
            }
        }
    }
}
