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
        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsgen = new ClsGeneral();
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
                pnl_foodpacks.Width = (int)(this.ClientSize.Width * 0.9);
                pnl_foodpacks.Height = (int)(this.ClientSize.Height * 0.9);

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
            pnl_foodpacks.Visible=false;
        }

        private void btn_orderdesc_submit_Click(object sender, EventArgs e)
        {
            pnl_foodpacks.Visible=false;
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
                search.Select = "PartyPackId, PackName, AdultPrice, ChildPrice";
                search.Table = "tblPartyPackMaster";
                search.WhereNdGroupBy = "WHERE Status = 1";
                search.OrderBy = "ORDER BY PackName";

                var list = bll.GetSearchData("prdSearch", search);

                foreach (var item in list)
                {
                    // 🔹 MAIN PANEL (CARD)
                    Panel pnl = new Panel();
                    pnl.Width = 220;
                    pnl.Height = 100;
                    pnl.Margin = new Padding(10);
                    pnl.BackColor = Color.White;
                    pnl.BorderStyle = BorderStyle.FixedSingle;
                    pnl.Tag = item.PartyPackId;

                    // 🔹 PACK NAME
                    Label lblName = new Label();
                    lblName.Text = item.PackName;
                    lblName.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    lblName.AutoSize = false;
                    lblName.Width = pnl.Width - 10;
                    lblName.Height = 35;
                    lblName.Location = new Point(5, 5);
                    lblName.TextAlign = ContentAlignment.MiddleLeft;

                    // 🔹 PRICE TEXT
                    string priceText = "";

                    if (item.AdultPrice > 0 && item.ChildPrice > 0)
                        priceText = $"Adult: {item.AdultPrice} | Child: {item.ChildPrice}";
                    else if (item.AdultPrice > 0)
                        priceText = $"Price: {item.AdultPrice}";
                    else if (item.ChildPrice > 0)
                        priceText = $"Price: {item.ChildPrice}";

                    Label lblPrice = new Label();
                    lblPrice.Text = priceText;
                    lblPrice.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                    lblPrice.AutoSize = false;
                    lblPrice.Width = pnl.Width - 10;
                    lblPrice.Height = 25;
                    lblPrice.Location = new Point(5, 40);
                    lblPrice.ForeColor = Color.DarkGreen;

                    // 🔹 VIEW BUTTON
                    Button btnView = new Button();
                    btnView.Text = "View";
                    btnView.Width = 60;
                    btnView.Height = 28;
                    btnView.Location = new Point(pnl.Width - 70, pnl.Height - 35);
                    btnView.BackColor = Color.FromArgb(0, 120, 215);
                    btnView.ForeColor = Color.White;
                    btnView.FlatStyle = FlatStyle.Flat;
                    btnView.Tag = item.PartyPackId;
                    btnView.Click += BtnView_Click;

                    // 🔹 ADD CONTROLS
                    pnl.Controls.Add(lblName);
                    pnl.Controls.Add(lblPrice);
                    pnl.Controls.Add(btnView);

                    // 🔹 ADD TO FLOW PANEL
                    flp_show_pack_resmenu.Controls.Add(pnl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;

                if (btn == null) return;

           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                pnl_eventreg.Height = this.Height * 50 / 100;

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

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Enter Description");
                txtDescription.Focus();
                return false;
            }

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

                // ✅ Refresh
                LoadEventItems();

                // ✅ Clear fields
                txtItemName.Clear();
                txtDescription.Clear();

                // ✅ Hide panel after save
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

                // Reset UI
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

                // ✅ Headers
                _objSearch.ColumnHeaders = "ID,Item Name,Type";

                // ✅ Select
                _objSearch.Select = "EventItemId as Id, ItemName as Field1, EventCategoryType as Field2";

                _objSearch.Table = "tblEventCommonItems";

                _objSearch.WhereNdGroupBy =
                    "WHERE Status = 1 AND BranchId = " + FrmDashboard.BranchId;

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

                // ✅ Load Selected Data
                if (returnValue > 0)
                {
                    FillEventData(returnValue);

                    // 🔥 Enable Delete Button
                    btn_del_event.Visible = true;
                    btn_del_event.Tag = returnValue; // store ID
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

                    // Optional: set type if dropdown exists
                    // cmbType.SelectedValue = item.EventCategoryType;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
