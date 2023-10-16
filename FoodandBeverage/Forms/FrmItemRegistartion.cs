using BLL;
using CustomMessageBox;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmItemRegistartion : Form
    {
        public FrmItemRegistartion()
        {
            InitializeComponent();
        }
        #region Declare Grid
        public static int slno = 0;
        public static int idno = 1;
        public static int incre_nameno = 2;
        public static int incre_categno = 3;
        public static int iunitno = 4;
        public static int iqtyno = 5;
        public static int ipriceno = 6;

        public static int itaxno = 7;
        public static int itaxtypeno = 8;
        public static int itaxperno = 9;

        public static int idescno = 10;
        public static int istatusno = 11;
        public static int iunitidno = 12;
        public static int incre_categ_idno = 13;
        #endregion
        public void Fillitem()
        {
            try
            {
                BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "i.*,t.*,u.UnitName,c.CategoryName";
                objSearch.Table = "tblItem i LEFT JOIN tblTaxSettings t on t.TaxSettingsId=i.TaxSettingsId LEFT JOIN tblUnit u on u.UId=i.unit LEFT JOIN tblCategory_FTI c on c.Fcid = i.Fcid";
                objSearch.WhereNdGroupBy = "Where i.status=1 ";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgv_item.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_item.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_item.Rows[i].Cells[slno].Value = i + 1;
                        dgv_item.Rows[i].Cells[idno].Value = itemSource[i].ItemId;
                        dgv_item.Rows[i].Cells[incre_nameno].Value = itemSource[i].Itemname;
                        dgv_item.Rows[i].Cells[incre_categno].Value = itemSource[i].CategoryName;
                        dgv_item.Rows[i].Cells[iunitno].Value = itemSource[i].UnitName;
                        dgv_item.Rows[i].Cells[iqtyno].Value = itemSource[i].quantity;
                        dgv_item.Rows[i].Cells[ipriceno].Value = itemSource[i].price;

                        dgv_item.Rows[i].Cells[itaxno].Value = itemSource[i].Tax;
                        dgv_item.Rows[i].Cells[itaxtypeno].Value = itemSource[i].TaxType;
                        if(itemSource[i].TaxType=="GST")                        
                            dgv_item.Rows[i].Cells[itaxperno].Value = itemSource[i].IgstPrcnt;
                        else
                            dgv_item.Rows[i].Cells[itaxperno].Value = itemSource[i].VatPrcnt;

                        dgv_item.Rows[i].Cells[idescno].Value = itemSource[i].summary;
                        dgv_item.Rows[i].Cells[istatusno].Value = Convert.ToInt32(itemSource[i].Status);
                        dgv_item.Rows[i].Cells[iunitidno].Value = itemSource[i].unit;
                        dgv_item.Rows[i].Cells[incre_categ_idno].Value = itemSource[i].Fcid;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void combofill()
        {

            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Fcid,CategoryName";
                objSearch.Table = " tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where Status=1 and CategoryType=2";
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_inc_categ.DataSource = gridSource;
                cmb_inc_categ.ValueMember = "Id";
                cmb_inc_categ.DisplayMember = "Value";

                objSearch.Select = " UId,UnitName";
                objSearch.Table = " tblUnit";
                objSearch.WhereNdGroupBy = "where status=1";
                objSearch.OrderBy = "";
                var gridSource1 = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmbunit.DataSource = gridSource1;
                cmbunit.ValueMember = "Id";
                cmbunit.DisplayMember = "Value";

            }
            catch (Exception ex)
            {

            }
        }
        public void Taxfill()
        {
            BllSearch<DmnComboBox> fiilgst = new BllSearch<DmnComboBox>();
            BllSearch<DmnComboBox> fiilvat = new BllSearch<DmnComboBox>();
            DmnSearch objSearch1 = new DmnSearch();
            DmnSearch objSearch2 = new DmnSearch();

            objSearch1.Select = " TaxSettingsId as Id, IgstPrcnt as Value";
            objSearch1.Table = " tblTaxSettings";
            objSearch1.WhereNdGroupBy = " where status=1 and TaxType='GST'";
            objSearch1.OrderBy = "order by value";

            objSearch2.Select = " TaxSettingsId as Id, VatPrcnt as Value ";
            objSearch2.Table = " tblTaxSettings";
            objSearch2.WhereNdGroupBy = " where isnull(status,0)=1 and TaxType='VAT'";
            objSearch2.OrderBy = " order by value";

            var gridSource1 = fiilgst.GetSearchData("prdComboFill", objSearch1);
            var gridSource2 = fiilvat.GetSearchData("prdComboFill", objSearch2);

            cmbVatPrcnt.DataSource = gridSource2;
            cmbVatPrcnt.ValueMember = "Id";
            cmbVatPrcnt.DisplayMember = "Value";

            cmbIgstPrcnt.DataSource = gridSource1;
            cmbIgstPrcnt.ValueMember = "Id";
            cmbIgstPrcnt.DisplayMember = "Value";
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {
                    DmnItem dmnitemreg = new DmnItem();
                    BllItem blltenreg = new BllItem();
                    if (string.IsNullOrWhiteSpace(txtitemid.Text))
                        txtitemid.Text = "0";
                    dmnitemreg.ItemId = Convert.ToInt32(txtitemid.Text);
                    dmnitemreg.Itemname = txtincr_name.Text;
                    dmnitemreg.summary = txtdesc.Text;
                    dmnitemreg.price = Convert.ToDecimal(txtprice.Text);
                    dmnitemreg.quantity = Convert.ToDecimal(txtqty.Text);
                    dmnitemreg.unit = Convert.ToInt32(cmbunit.SelectedValue);
                    dmnitemreg.Fcid = Convert.ToInt32(cmb_inc_categ.SelectedValue);
                    dmnitemreg.ItemCode_UPC = Convert.ToString(txtbarcode.Text);
                    if (string.IsNullOrWhiteSpace(txtmrp.Text))
                        txtmrp.Text = "0";
                    dmnitemreg.MRP = Convert.ToDecimal(txtmrp.Text);
                    if (rdb_dsale.Checked)
                        dmnitemreg.Saletype = 1;
                    else
                        dmnitemreg.Saletype = 0;
                    dmnitemreg.Tax = Convert.ToString(cmbtax.Text);

                    if (Convert.ToInt32(cmbTaxType.SelectedIndex) == 0)
                        dmnitemreg.TaxSettingsId = Convert.ToInt32(cmbIgstPrcnt.SelectedValue);
                    else
                        dmnitemreg.TaxSettingsId = Convert.ToInt32(cmbVatPrcnt.SelectedValue);
                    dmnitemreg.TaxType = Convert.ToString(cmbTaxType.Text);

                    dmnitemreg.CreatedBy = 1;
                    dmnitemreg.BranchId = 1;

                    if (chb_active.Checked)
                        dmnitemreg.Status = 1;
                    else
                        dmnitemreg.Status = 0;
                    var usertype = blltenreg.AddData("prdInsert_upd_ItemReg", dmnitemreg);

                    if (usertype.ItemId > 0)
                    {
                        if (Convert.ToUInt32(txtitemid.Text) > 0)
                            CMessageBox.Show("Update Successfully saved");
                        else
                            CMessageBox.Show("Successfully saved");

                        clearall();
                        Fillitem();
                    }
                    else
                        CMessageBox.Show("Save failed", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void clearall()
        {
            txtbarcode.Text = "";
            txtincr_name.Text = "";
            txtdesc.Text = "";
            txtqty.Text = "";
            txtprice.Text = "";
            cmbunit.SelectedIndex = -1;
            cmb_inc_categ.SelectedIndex = -1;
            cmbtax.SelectedIndex = 0;
            cmbTaxType.SelectedIndex = 0;
            cmbIgstPrcnt.SelectedIndex = 0;
            cmbTaxType.SelectedIndex = 0;
            btn_del_ingre.Visible = false;
            txtCgstPrcnt.Text = "";
            txtSgstPrcnt.Text = "";
        }


        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool MandatoryCheck()
        {
            if (txtincr_name.Text == "" || txtincr_name.Text == null)
            {
                CMessageBox.Show("Please Enter Item Name", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtitemid.Focus();
                return false;
            }
            else if (Convert.ToInt32(cmb_inc_categ.SelectedValue) == -1)
            {
                CMessageBox.Show("Please Select Food Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_inc_categ.Focus();
                return false;
            }
            else if (Convert.ToInt32(cmbunit.SelectedValue) == -1)
            {
                CMessageBox.Show("Please Select Unit Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbunit.Focus();
                return false;
            }  
            else if (txtqty.Text == "" || txtqty.Text == null || Convert.ToDecimal(txtqty.Text) < 0)
            {
                CMessageBox.Show("Please Enter Item Quauntity", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtqty.Focus();
                return false;
            }
            else if (txtprice.Text == "" || txtprice.Text == null || Convert.ToDecimal(txtprice.Text) < 0)
            {
                CMessageBox.Show("Please Enter Item Price", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtprice.Focus();
                return false;
            }
            else if (cmbtax.Text == "")
            {
                CMessageBox.Show("Please Select Tax", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbtax.Focus();
                return false;
            }
            else if (cmbTaxType.Text == "")
            {
                CMessageBox.Show("Please Select Tax Type GST or VAT", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTaxType.Focus();
                return false;
            }
            else if (cmbTaxType.Text == "GST")
            {
                if (Convert.ToInt32(cmbIgstPrcnt.SelectedValue) == -1)
                {
                    CMessageBox.Show("Please Select IGST %", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbIgstPrcnt.Focus();
                    return false;
                }
            }
            else if (cmbTaxType.Text == "VAT")
            {
                if (Convert.ToInt32(cmbVatPrcnt.SelectedValue) == -1)
                {
                    CMessageBox.Show("Please Select VAT %", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbVatPrcnt.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            Fillitem();
            clearall();
            combofill();
            Taxfill();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btntoggle_Click(object sender, EventArgs e)
        {
            
            this.Width = 1025;
            this.Height = 617;
            tlp_registration.Visible = false;
            tlp_grid.Visible = true;
            Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_grid, 1, 0);
            tlp_grid.Dock = DockStyle.Fill;
        }

        private void toggle_grid_Click(object sender, EventArgs e)
        {
            this.Width = 919; 
            this.Height = 417;
            tlp_grid.Visible = false;
            tlp_registration.Visible = true;
            Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_registartion_toggle, 1, 0);
            tlp_registartion_toggle.Dock = DockStyle.Fill;
        }

        private void txtprice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtqty_TextChanged(object sender, EventArgs e)
        {

        }

        private void tlp_registration_Paint(object sender, PaintEventArgs e)
        {
            float borderRadius = 10f; 
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(tlp_registration.ClientRectangle.X, tlp_registration.ClientRectangle.Y, tlp_registration.ClientRectangle.Width, tlp_registration.ClientRectangle.Height);
            path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            path.CloseFigure();
            tlp_registration.Region = new Region(path);
        }

        private void FrmItemRegistartion_Load(object sender, EventArgs e)
        {
            Fillitem();
            combofill();
            Taxfill();
            dgv_item.BorderStyle = BorderStyle.None;
            cmbTaxType.SelectedIndex = 0;
            //dgv_item.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            //dgv_item.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            //dgv_item.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            //dgv_item.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            //dgv_item.BackgroundColor = Color.White;

            dgv_item.EnableHeadersVisualStyles = false;
            dgv_item.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_item.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_item.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void btn_del_ingre_Click(object sender, EventArgs e)
        {
            var result = CMessageBox.Show("Do You want Delete This Category", "Confirm The Actionn", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                chb_active.Checked = false;
                btnsave.PerformClick();
                Fillitem();
            }
        }

        private void dgv_item_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int incgreid = Convert.ToInt32(dgv_item.Rows[e.RowIndex].Cells[idno].Value);
                BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblitem";
                objSearch.WhereNdGroupBy = "where status = 1 and itemid=" + incgreid;
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    txtitemid.Text = Convert.ToString(incgreid);
                    txtincr_name.Text = Convert.ToString(itemSource[0].Itemname);
                    txtbarcode.Text = Convert.ToString(itemSource[0].ItemCode_UPC);
                    txtdesc.Text = Convert.ToString(itemSource[0].summary);
                    txtprice.Text = Convert.ToString(itemSource[0].price);
                    txtqty.Text = Convert.ToString(itemSource[0].quantity);
                    cmbunit.SelectedValue = Convert.ToInt64(itemSource[0].unit);
                    txtmrp.Text = Convert.ToString(itemSource[0].MRP);
                    if (Convert.ToInt32(itemSource[0].Saletype) == 1)
                        rdb_dsale.Checked = true;
                    else
                        rdb_notsale.Checked = true;
                    cmbtax.Text = Convert.ToString(itemSource[0].Tax);
                    cmbtax.Text = Convert.ToString(itemSource[0].TaxType);
                    if (cmbtax.Text=="GST")
                        cmbIgstPrcnt.SelectedValue = Convert.ToInt64(itemSource[0].TaxSettingsId);
                    else
                        cmbVatPrcnt.SelectedValue = Convert.ToInt64(itemSource[0].TaxSettingsId);
                    cmb_inc_categ.SelectedValue = Convert.ToInt64(itemSource[0].Fcid);
                    btn_del_ingre.Visible = true;
                    toggle_grid.PerformClick();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void cmbTaxType_SelectedValueChanged(object sender, EventArgs e)
        {           
            int[] columnsToRemove1 = { 0, 1, 2, 3 };
            int[] rowsToRemove1 = { 5,6,7, 8 };

            foreach (int column in columnsToRemove1)
            {
                foreach (int row in rowsToRemove1)
                {
                    Control controlToRemove = tlp_registration.GetControlFromPosition(column, row);
                    if (controlToRemove != null)
                    {
                        tlp_registration.Controls.Remove(controlToRemove);
                    }
                }
            }
            cmbtax.Dock = DockStyle.Fill;
            cmbTaxType.Dock = DockStyle.Fill;

            if (cmbTaxType.Text == "GST")
            {
                lbligst.Dock = DockStyle.Fill;
                cmbIgstPrcnt.Dock = DockStyle.Fill;
                tlp_s_c_gst.Dock = DockStyle.Fill;
                tlp_S_C_text_.Dock = DockStyle.Fill;
                lbldes.Dock = DockStyle.Fill;
                //row1
                tlp_registration.Controls.Add(lbltax, 0, 5);
                tlp_registration.Controls.Add(lbltaxtype, 1, 5);
                tlp_registration.Controls.Add(lbligst, 2, 5);
                tlp_registration.Controls.Add(tlp_s_c_gst, 3, 5);
                //row2
                tlp_registration.Controls.Add(cmbtax, 0, 6);
                tlp_registration.Controls.Add(cmbTaxType, 1, 6);
                tlp_registration.Controls.Add(cmbIgstPrcnt, 2, 6);
                tlp_registration.Controls.Add(tlp_S_C_text_, 3, 6);
                //row3
                tlp_registration.Controls.Add(lbldes, 0, 7);
                //row4
                tlp_registration.Controls.Add(txtdesc, 0, 8);
                tlp_registration.Controls.Add(btn_del_ingre, 1, 8);

            }
            else if (cmbTaxType.Text == "VAT")
            {
                cmbVatPrcnt.Dock = DockStyle.Fill;
                lblvat.Dock = DockStyle.Fill;
                lbldes.Dock = DockStyle.Fill;
                //row1
                tlp_registration.Controls.Add(lbltax, 0, 5);
                tlp_registration.Controls.Add(lbltaxtype, 1, 5);
                tlp_registration.Controls.Add(lblvat, 2, 5);
                tlp_registration.Controls.Add(lbldes, 3, 5);
                //row2
                tlp_registration.Controls.Add(cmbtax, 0, 6);
                tlp_registration.Controls.Add(cmbTaxType, 1, 6);
                tlp_registration.Controls.Add(cmbVatPrcnt, 2, 6);
                tlp_registration.Controls.Add(txtdesc, 3, 6);
                //row3
                tlp_registration.Controls.Add(btn_del_ingre, 0, 8);

            }
        }

        private void cmbIgstPrcnt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIgstPrcnt.SelectedIndex > 0)
            {
                BllSearch<DmnTaxSettings> bllSearch = new BllSearch<DmnTaxSettings>();
                DmnSearch objSearch_tax = new DmnSearch();
                objSearch_tax = new DmnSearch();
                objSearch_tax.Select = " TaxSettingsId,SgstPrcnt,CgstPrcnt ";
                objSearch_tax.Table = " tblTaxSettings ";
                objSearch_tax.WhereNdGroupBy = " Where TaxSettingsId = " + cmbIgstPrcnt.SelectedValue;
                objSearch_tax.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch_tax);
                if (gridSource.Count > 0)
                {
                    txtSgstPrcnt.Text = gridSource[0].SgstPrcnt.ToString();
                    txtCgstPrcnt.Text = gridSource[0].CgstPrcnt.ToString();
                }
            }
        }

        private void btn_grid_refresh_Click(object sender, EventArgs e)
        {
            Fillitem();
        }

        private void txtmrp_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtmrp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtmrp.Text))
                txtmrp.Text = "0";
            if (Convert.ToDecimal(txtmrp.Text) < Convert.ToDecimal(txtprice.Text))
            {
                CMessageBox.Show("MRP Rate Must be Greater than Sale Price", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmrp.Focus();
            }
        }
    }
}
