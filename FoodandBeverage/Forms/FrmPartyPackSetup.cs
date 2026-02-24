using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
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
using System.Windows.Controls;
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

        #endregion

        bool Loaddetailsflag = false;
        bool Loadcustomflag = false;

        int currentPackId = 0;

        private List<DmnCategory> categoryList = new List<DmnCategory>();
        #endregion
        public FrmPartyPackSetup()
        {
            InitializeComponent();
        }
        private void FrmPartyPackSetup_Load(object sender, EventArgs e)
        {
            Taxfill();
            LoadMenuCategory();
            LoadMenuCategory_Cutom();

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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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

                    //row3
                    tlp_registration.Controls.Add(btn_del_pack, 2, 6);                    

                }
                else if (cmbTaxType.Text == "VAT")
                {
                    cmbVatPrcnt.Dock = DockStyle.Fill;
                    lblvat.Dock = DockStyle.Fill;

                    //row1
                    tlp_registration.Controls.Add(lblvat, 3, 3);

                    //row2
                    tlp_registration.Controls.Add(cmbVatPrcnt, 3, 4);

                    //row3
                    tlp_registration.Controls.Add(btn_del_pack, 0, 5);

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
                    Status = 1
                };

                var packstatus = bllPack.AddEditData("prdInsert_Upd_PartyPack", dmn);

                if (packstatus.PartyPackId <= 0)
                {
                    MessageBox.Show("Error saving Party Pack");
                    return;
                }

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

                        //if (menuCategoryId == 0)
                        //{
                        //    MessageBox.Show($"Please select category for item: {itemVal}",
                        //        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    continue;
                        //}

                        DmnPartyPackDetails dmndetail = new DmnPartyPackDetails
                        {
                            PartyPackId = packstatus.PartyPackId,
                            PartyPackDetailsId = detailsId,
                            Item = itemVal,
                            Quantity = qty,
                            CreatedBy = FrmDashboard.UserId,
                            BranchId = FrmDashboard.BranchId,
                            Status = 1,
                            MenuCategoryId = menuCategoryId
                        };

                        var result = bllPack.AddData_packdetails("prdInsert_Upd_PartyPackDetails", dmndetail);

                        if (result != null && result.PartyPackDetailsId > 0)
                        {
                            statuscount++;
                        }
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
                    MessageBox.Show("Party Pack Saved Successfully");
                    clearall();
                }
                else
                    MessageBox.Show("Some Items Not Saved Properly");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    MessageBox.Show("Please Enter Party Pack Name", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtpackname.Focus();
                    return false;
                }

                if (Convert.ToDecimal(txtadultprice.Text) <= 0)
                {
                    MessageBox.Show("Please Enter Valid Price", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtadultprice.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(cmbTaxType.Text))
                {
                    MessageBox.Show("Please Select Tax Type", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbTaxType.Focus();
                    return false;
                }
                if (cmbTaxType.Text == "GST")
                {
                    if (cmbIgstPrcnt.SelectedValue == null || Convert.ToInt32(cmbIgstPrcnt.SelectedValue) <= 0)
                    {
                        MessageBox.Show("Please Select IGST %", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbIgstPrcnt.Focus();
                        return false;
                    }
                }
                else if (cmbTaxType.Text == "VAT")
                {
                    if (cmbVatPrcnt.SelectedValue == null || Convert.ToInt32(cmbVatPrcnt.SelectedValue) <= 0)
                    {
                        MessageBox.Show("Please Select VAT %", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                //    MessageBox.Show("Please Add At Least One Item in Pack", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                txtpackname.Text = "";
                txtadultprice.Text = "";
                txtchildprice.Text = "";
                txtDiscAmnt.Text = "";
                txtDisPer.Text = "";
                
                cmbtax.SelectedIndex = 0;
                cmbTaxType.SelectedIndex = 0;
                cmbIgstPrcnt.SelectedIndex = 0;
             
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

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                    if (data.TaxType == "GST")
                        cmbIgstPrcnt.SelectedValue =Convert.ToInt64(data.TaxSettingsId);
                    else
                        cmbVatPrcnt.SelectedValue = Convert.ToInt64(data.TaxSettingsId);

                    btn_del_pack.Visible = true;

                    // Load Details Grid
                    LoadPackDetails(id);
                    LoadCustomMenu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                search.Select = "PartyPackDetailsId, Item, Quantity, MenuCategoryId";
                search.Table = "tblPartyPackDetails";
                search.WhereNdGroupBy = "WHERE PartyPackId = " + packId + " AND Status = 1";

                var list = bll.GetSearchData("prdSearch", search);

                foreach (var item in list)
                {
                    int rowIndex = dgv_packs_default.Rows.Add();
                    var row = dgv_packs_default.Rows[rowIndex];

                    row.Cells[PartyPackDetailsId].Value = item.PartyPackDetailsId;
                    row.Cells[Ritemno].Value = item.Item;
                    row.Cells[Rqtyno].Value = item.Quantity;

                    // ✅ SAFE COMBO ASSIGN (NO ERROR)
                    if (item.MenuCategoryId > 0 &&
                        categoryList != null &&
                        categoryList.Any(x => x.Fcid == item.MenuCategoryId))
                    {
                        row.Cells[Rcategmenuno].Value =Convert.ToInt64(item.MenuCategoryId);
                    }
                    else
                    {
                        row.Cells[Rcategmenuno].Value = DBNull.Value; // prevent crash
                    }
                }

                RefreshSlNo();

                // ✅ Always keep empty row
                if (!HasEmptyRow())
                {
                    dgv_packs_default.Rows.Add();
                }

                LoadCustomMenu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Loaddetailsflag = false;
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
                }

                RefreshCustomSlNo();
                if (!HasEmptyRowCustom())
                {
                    dgv_packcustom.Rows.Add();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteGridRow(int rowIndex)
        {
            try
            {
                var row = dgv_packs_default.Rows[rowIndex];

                if (row.IsNewRow) return;

                DialogResult result = MessageBox.Show(
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
                MessageBox.Show(ex.Message);
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



        private void btn_stanpackage_Click(object sender, EventArgs e)
        {
            dgv_packs_default.BringToFront();
            dgv_packs_default.Visible = true;
        }

        private void btn_custommenu_Click(object sender, EventArgs e)
        {
            dgv_packcustom.BringToFront();
            dgv_packcustom.Visible = true;
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
                MessageBox.Show(ex.Message);
            }
        }
        private void DeleteCustomGridRow(int rowIndex)
        {
            try
            {
                var row = dgv_packcustom.Rows[rowIndex];

                if (row.IsNewRow) return;

                DialogResult result = MessageBox.Show(
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
                MessageBox.Show(ex.Message);
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
                DialogResult result = MessageBox.Show(
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
                    MessageBox.Show("Enter Category Name");
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
                MessageBox.Show("Category Saved");
                txtFcid.Text = "0";
                txtfoodCate.Clear();

                LoadMenuCategory();
                pnl_menucateg.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_del_cate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFcid.Text) || txtFcid.Text == "0")
                {
                    MessageBox.Show("Select Category to Delete");
                    return;
                }

                DialogResult result = MessageBox.Show(
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

                MessageBox.Show("Category Deleted");

                // Reset
                txtFcid.Text = "0";
                txtfoodCate.Clear();

                // 🔥 Reload Combo
                LoadMenuCategory();

                pnl_menucateg.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
    }
}
