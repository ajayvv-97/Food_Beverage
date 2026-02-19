using BLL;
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
        public int Rqtyno = 2;
        public int Ractionno = 3;
        public int PartyPackDetailsId = 4;
        bool Loaddetailsflag = false;
        #endregion

        int currentPackId = 0;

        #endregion
        public FrmPartyPackSetup()
        {
            InitializeComponent();
        }
        private void FrmPartyPackSetup_Load(object sender, EventArgs e)
        {
            Taxfill();

            if (dgv_packs.Rows.Count == 0)
                dgv_packs.Rows.Add();

            RefreshSlNo();

            // 🔥 Important for ComboBox / Immediate Change
            dgv_packs.CurrentCellDirtyStateChanged += (s, ev) =>
            {
                if (dgv_packs.IsCurrentCellDirty)
                    dgv_packs.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }



        private bool HasEmptyRow()
        {
            foreach (DataGridViewRow row in dgv_packs.Rows)
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
                        if ((row == 3 && col == 0) || (row == 3 && col == 1))
                            continue;
                        if ((row == 4 && col == 0)  || (row == 4 && col == 1))
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

                    //row1
                    tlp_registration.Controls.Add(lbligst, 2, 3);
                    tlp_registration.Controls.Add(tlp_s_c_gst_lbl, 3, 3);

                    //row2
                    tlp_registration.Controls.Add(cmbIgstPrcnt, 2, 4);
                    tlp_registration.Controls.Add(tlp_S_C_text, 3, 4);

                    //row3
                    tlp_registration.Controls.Add(btn_del_pack, 0, 6);                    

                }
                else if (cmbTaxType.Text == "VAT")
                {
                    cmbVatPrcnt.Dock = DockStyle.Fill;
                    lblvat.Dock = DockStyle.Fill;

                    //row1
                    tlp_registration.Controls.Add(lblvat, 2, 3);

                    //row2
                    tlp_registration.Controls.Add(cmbVatPrcnt, 2, 4);

                    //row3
                    tlp_registration.Controls.Add(btn_del_pack, 3, 4);

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
                    Price = Convert.ToDecimal(txtprice.Text),
                    DiscountPer = Convert.ToDecimal(txtDisPer.Text),
                    DiscountAmt = Convert.ToDecimal(txtDiscAmnt.Text),
                    TaxType = cmbTaxType.Text,
                    TaxSettingsId = cmbTaxType.Text == "GST"
                        ? Convert.ToInt32(cmbIgstPrcnt.SelectedValue)
                        : Convert.ToInt32(cmbVatPrcnt.SelectedValue),
                    CreatedBy = FrmDashboard.UserId,
                    BranchId = FrmDashboard.BranchId
                };

                var packstatus = bllPack.AddEditData("prdInsert_Upd_PartyPack", dmn);

                if (packstatus.PartyPackId <= 0)
                {
                    MessageBox.Show("Error saving Party Pack");
                    return;
                }

                int statuscount = 0;
                int gridcount = 0;

                for (int i = 0; i < dgv_packs.Rows.Count; i++)
                {
                    var row = dgv_packs.Rows[i];
                    if (row.IsNewRow) continue;

                    string itemVal = Convert.ToString(row.Cells[Ritemno].Value);
                    string qtyVal = Convert.ToString(row.Cells[Rqtyno].Value);

                    if (!string.IsNullOrWhiteSpace(itemVal) &&
                        decimal.TryParse(qtyVal, out decimal qty) && qty > 0)
                    {
                        gridcount++;

                        int detailsId = 0;
                        int.TryParse(Convert.ToString(row.Cells[PartyPackDetailsId].Value), out detailsId);

                        DmnPartyPackDetails dmndetail = new DmnPartyPackDetails();

                        dmndetail.PartyPackId = packstatus.PartyPackId;
                        dmndetail.PartyPackDetailsId = detailsId;
                        dmndetail.Item = itemVal;
                        dmndetail.Quantity = Convert.ToInt32(qty);
                        dmndetail.CreatedBy = FrmDashboard.UserId;
                        dmndetail.BranchId = FrmDashboard.BranchId;
                        dmndetail.Status = 1;                       
                        var status = bllPack.AddData_packdetails("prdInsert_Upd_PartyPackDetails", dmndetail);

                        if (status.PartyPackDetailsId > 0)
                            statuscount++;
                    }
                }

                if (gridcount == 0)
                {
                    MessageBox.Show("Please add at least one item");
                    return;
                }

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
                // Default values
                if (string.IsNullOrWhiteSpace(txtprice.Text))
                    txtprice.Text = "0";

                if (string.IsNullOrWhiteSpace(txtDisPer.Text))
                    txtDisPer.Text = "0";

                if (string.IsNullOrWhiteSpace(txtDiscAmnt.Text))
                    txtDiscAmnt.Text = "0";

                // ✅ Pack Name
                if (string.IsNullOrWhiteSpace(txtpackname.Text))
                {
                    MessageBox.Show("Please Enter Party Pack Name", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtpackname.Focus();
                    return false;
                }

                // ✅ Price
                if (Convert.ToDecimal(txtprice.Text) <= 0)
                {
                    MessageBox.Show("Please Enter Valid Price", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtprice.Focus();
                    return false;
                }

                // ✅ Tax Type
                if (string.IsNullOrWhiteSpace(cmbTaxType.Text))
                {
                    MessageBox.Show("Please Select Tax Type", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbTaxType.Focus();
                    return false;
                }

                // ✅ GST / VAT validation
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

                // ✅ GRID CHECK (VERY IMPORTANT 🔥)
                bool hasItem = false;

                foreach (DataGridViewRow row in dgv_packs.Rows)
                {
                    if (row.IsNewRow) continue;

                    var item = row.Cells[Ritemno].Value;

                    if (item != null && !string.IsNullOrWhiteSpace(item.ToString()))
                    {
                        hasItem = true;

                        // ✅ Qty check
                        var qty = row.Cells[Rqtyno].Value;

                        if (qty == null || Convert.ToInt32(qty) <= 0)
                        {
                            row.Cells[Rqtyno].Value = 1; // default
                        }
                    }
                }

                if (!hasItem)
                {
                    MessageBox.Show("Please Add At Least One Item in Pack", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgv_packs.Focus();
                    return false;
                }

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
                txtprice.Text = "";
                txtDiscAmnt.Text = "";
                txtDisPer.Text = "";
                
                cmbtax.SelectedIndex = 0;
                cmbTaxType.SelectedIndex = 0;
                cmbIgstPrcnt.SelectedIndex = 0;
             
                cmbTaxType.SelectedIndex = 0;
                dgv_packs.Rows.Clear();
                dgv_packs.Rows.Add();
                //btn_del_ingre.Visible = false;

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
                var row = dgv_packs.Rows[e.RowIndex];
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

                    bool isLastRow = e.RowIndex == dgv_packs.Rows.Count - 1;

                    if (isLastRow && !HasEmptyRow())
                    {
                        dgv_packs.Rows.Add();
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
                _objSearch.ColumnHeaders = "ID,Pack Name,Price";
                _objSearch.Select = "PartyPackId as Id,PackName as Field1,Price as Field2";
                _objSearch.Table = "tblPartyPackMaster";
                _objSearch.WhereNdGroupBy = "WHERE Status = 1";
                _objSearch.OrderBy = " ORDER BY PackName";

                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);

                if (gridSource.Count == 0)
                {
                    MessageBox.Show("No Records Found");
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
                    txtprice.Text = data.Price.ToString();
                    txtDisPer.Text = data.DiscountPer.ToString();
                    txtDiscAmnt.Text = data.DiscountAmt.ToString();
                    cmbTaxType.Text = data.TaxType;

                    if (data.TaxType == "GST")
                        cmbIgstPrcnt.SelectedValue =Convert.ToInt64(data.TaxSettingsId);
                    else
                        cmbVatPrcnt.SelectedValue = Convert.ToInt64(data.TaxSettingsId);

                    // Load Details Grid
                    LoadPackDetails(id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadPackDetails(int packId)
        {
            try
            {
                Loaddetailsflag = true;
                dgv_packs.Rows.Clear(); // Clear existing rows

                BllSearch<DmnPartyPackDetails> bll = new BllSearch<DmnPartyPackDetails>();

                DmnSearch search = new DmnSearch();
                search.Select = "PartyPackDetailsId, Item, Quantity";
                search.Table = "tblPartyPackDetails";
                search.WhereNdGroupBy = "WHERE PartyPackId = " + packId + " AND Status = 1";

                var list = bll.GetSearchData("prdSearch", search);

                foreach (var item in list)
                {
                    int rowIndex = dgv_packs.Rows.Add();

                    dgv_packs.Rows[rowIndex].Cells[PartyPackDetailsId].Value = item.PartyPackDetailsId;
                    dgv_packs.Rows[rowIndex].Cells[Ritemno].Value = item.Item;
                    dgv_packs.Rows[rowIndex].Cells[Rqtyno].Value = item.Quantity;
                }

                RefreshSlNo();

                // ✅ Ensure last row is empty
                if (!HasEmptyRow())
                {
                    dgv_packs.Rows.Add();
                }
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



        private void RefreshSlNo()
        {
            int sl = 1;

            foreach (DataGridViewRow row in dgv_packs.Rows)
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
                    var row = dgv_packs.Rows[e.RowIndex];

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
                var row = dgv_packs.Rows[rowIndex];

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
                    dgv_packs.Rows.RemoveAt(rowIndex);
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

                    dgv_packs.Rows.RemoveAt(rowIndex);
                }

                // ✅ Always keep one empty row
                if (dgv_packs.Rows.Count == 0)
                    dgv_packs.Rows.Add();

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
                var row = dgv_packs.Rows[e.RowIndex];

                if (row.IsNewRow ||
                    row.Cells[Ritemno].Value == null ||
                    string.IsNullOrWhiteSpace(row.Cells[Ritemno].Value.ToString()))
                {
                    e.Value = null; // ❌ Hide delete button
                }
            }
        }

    }
}
