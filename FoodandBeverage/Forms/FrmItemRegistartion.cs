using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Forms.UserControls;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using FoodandBeverage.Interfaces;

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
        public static int iprateno = 6;
        public static int israteno = 7;
        public static int imrpno = 8;
        public static int itaxno = 9;
        public static int itaxtypeno = 10;
        public static int itaxperno = 11;
        public static int idescno = 12;
        public static int istatusno = 13;
        public static int iunitidno = 14;
        public static int incre_categ_idno = 15;

        //Prid
        public static int iprId = 0;
        public static int iprslno = 1;
        public static int iprproduct = 2;
        public static int iprprate = 3;
        public static int iprsrate = 4;
        public static int iprmrp = 5;
        public static int iprqoh = 6;
        public static int iprPtype = 7;
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
                        dgv_item.Rows[i].Cells[iprateno].Value = itemSource[i].Prate;
                        dgv_item.Rows[i].Cells[israteno].Value = itemSource[i].Srate;
                        dgv_item.Rows[i].Cells[imrpno].Value = itemSource[i].MRP;
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
                objSearch.WhereNdGroupBy = "where Status=1 and CategoryType in(0,2) ";
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
                    if (string.IsNullOrWhiteSpace(txtprate.Text))
                        txtprate.Text = "0";
                    if (string.IsNullOrWhiteSpace(txtsrate.Text))
                        txtsrate.Text = "0";
                    if (string.IsNullOrWhiteSpace(txtprid.Text))
                        txtprid.Text = "0";
                    if (string.IsNullOrWhiteSpace(txtqty.Text))
                        txtqty.Text = "0";
                    dmnitemreg.ItemId = Convert.ToInt32(txtitemid.Text);
                    dmnitemreg.Itemname = txtincr_name.Text;
                    dmnitemreg.summary = txtdesc.Text;
                    dmnitemreg.Prate = Convert.ToDecimal(txtprate.Text);
                    dmnitemreg.Srate = Convert.ToDecimal(txtsrate.Text);
                    dmnitemreg.quantity = Convert.ToDecimal(txtqty.Text);
                    dmnitemreg.unit = Convert.ToInt32(cmbunit.SelectedValue);
                    dmnitemreg.Fcid = Convert.ToInt32(cmb_inc_categ.SelectedValue);
                    dmnitemreg.ItemCode_UPC = Convert.ToString(txtbarcode.Text);
                    dmnitemreg.prid = Convert.ToInt32(txtprid.Text);
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

                    dmnitemreg.CreatedBy =   FrmDashboard.UserId;
                    dmnitemreg.BranchId = FrmDashboard.BranchId;

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
            txtprate.Text = "";
            cmbunit.SelectedIndex = -1;
            cmb_inc_categ.SelectedIndex = -1;
            cmbtax.SelectedIndex = 0;
            cmbTaxType.SelectedIndex = 0;
            cmbIgstPrcnt.SelectedIndex = 0;
            cmbTaxType.SelectedIndex = 0;
            btn_del_ingre.Visible = false;
            txtCgstPrcnt.Text = "";
            txtmrp.Text = "";
            txtprate.Text = "";
            txtsrate.Text = "";
            txtSgstPrcnt.Text = "";
            txtprid.Text = "";
            txtitemid.Text = "";
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
                txtincr_name.Focus();
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
            else if(rdb_dsale.Checked!=true)
            {
                if (txtqty.Text == "" || txtqty.Text == null || Convert.ToDecimal(txtqty.Text) < 0)
                {
                    CMessageBox.Show("Please Enter Item Quauntity", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtqty.Focus();
                    return false;
                }
                else if (txtprate.Text == "" || txtprate.Text == null || Convert.ToDecimal(txtprate.Text) < 0)
                {
                    CMessageBox.Show("Please Enter Item Purchase Price", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtprate.Focus();
                    return false;
                }
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
            if ((txtprate.Text == null || txtprate.Text == "" || (Convert.ToDecimal(txtprate.Text) == 0)) &&
         (Convert.ToDecimal(txtsrate.Text) > 0) && (Convert.ToDecimal(txtmrp.Text) > 0))
            {
                CMessageBox.Show("Please Enter Purchase Rate", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtprate.Focus();
                return false;
            }

            if ((txtprate.Text == null || txtprate.Text == "" || (Convert.ToDecimal(txtprate.Text) == 0)) && (Convert.ToDecimal(txtmrp.Text) > 0))
            {
                CMessageBox.Show("Please Enter Purchase Rate", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtprate.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtprate.Text) > 0)
            {
                if (Convert.ToDecimal(txtsrate.Text) < Convert.ToDecimal(txtprate.Text))
                {
                    CMessageBox.Show("Sale Rate Must be graterthan Purchase Rate", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtsrate.Focus();
                    return false;
                }
            }
            if (Convert.ToDecimal(txtsrate.Text) > 0)
            {
                if (Convert.ToDecimal(txtmrp.Text) < Convert.ToDecimal(txtsrate.Text))
                {
                    CMessageBox.Show("MRP Must be graterthan Sale Rate", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmrp.Focus();
                    return false;
                }
            }

            if (Convert.ToDecimal(txtsrate.Text) > 0)
            {
                if (Convert.ToDecimal(txtmrp.Text) < Convert.ToDecimal(txtsrate.Text))
                {
                    CMessageBox.Show("MRP Must be graterthan Sale Rate", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmrp.Focus();
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
            try
            {          
                dgv_item.CurrentCell = dgv_item[0, 0];
                tlp_registration.Visible = false;
                tlp_grid.Visible = true;
                Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
                tlp_main.Controls.Remove(controlToRemove);
                tlp_main.Controls.Add(tlp_grid, 1, 0);
                tlp_grid.Dock = DockStyle.Fill;
                dgv_item.Focus();
            }
            catch(Exception ex)
            {

            }
            
        }

        private void toggle_grid_Click(object sender, EventArgs e)
        {
            tlp_grid.Visible = false;
            tlp_registration.Visible = true;
            Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_registartion_toggle, 1, 0);
            tlp_registartion_toggle.Dock = DockStyle.Fill;
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
            if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
            {
                FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.Item)))
                    btnsave.Enabled = true;
                else
                    btnsave.Enabled = false;
            }
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
        public void filldata(int rowno)
        {
            try
            {
                int incgreid = Convert.ToInt32(dgv_item.Rows[rowno].Cells[idno].Value);
                BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "i.*,pr.SRate,pr.PRate,pr.QOH,pr.Id as PrId";
                objSearch.Table = "tblitem i left join tblProductRate pr on pr.ProductId = i.ItemId";
                objSearch.WhereNdGroupBy = "where i.status = 1 and itemid=" + incgreid;
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count == 1)
                {
                    txtitemid.Text = Convert.ToString(incgreid);
                    txtincr_name.Text = Convert.ToString(itemSource[0].Itemname);
                    txtbarcode.Text = Convert.ToString(itemSource[0].ItemCode_UPC);
                    txtdesc.Text = Convert.ToString(itemSource[0].summary);
                    txtprate.Text = Convert.ToString(itemSource[0].Prate);
                    txtsrate.Text = Convert.ToString(itemSource[0].Srate);
                    txtmrp.Text = Convert.ToString(itemSource[0].MRP);
                    txtqty.Text = Convert.ToString(itemSource[0].quantity);
                    cmbunit.SelectedValue = Convert.ToInt64(itemSource[0].unit);
                    txtmrp.Text = Convert.ToString(itemSource[0].MRP);
                    txtprid.Text = Convert.ToString(itemSource[0].prid);

                    if (Convert.ToInt32(itemSource[0].Saletype) == 1)
                        rdb_dsale.Checked = true;
                    else
                        rdb_notsale.Checked = true;
                    cmbtax.Text = Convert.ToString(itemSource[0].Tax);
                    cmbTaxType.Text = Convert.ToString(itemSource[0].TaxType);
                    if (cmbTaxType.Text == "GST")
                        cmbIgstPrcnt.SelectedValue = Convert.ToInt64(itemSource[0].TaxSettingsId);
                    else
                        cmbVatPrcnt.SelectedValue = Convert.ToInt64(itemSource[0].TaxSettingsId);
                    cmb_inc_categ.SelectedValue = Convert.ToInt64(itemSource[0].Fcid);
                    btn_del_ingre.Visible = true;
                    toggle_grid.PerformClick();
                }
                else if (itemSource.Count > 1)
                {
                    pnlPrRates.Visible = true;
                    pnlPrRates.Location = new Point(this.ClientSize.Width / 2 - pnlPrRates.Size.Width / 2, (this.ClientSize.Height / 3 - pnlPrRates.Size.Height / 3));
                    dgvprDetails.Rows.Clear();
                    dgvprDetails.Rows.Add(itemSource.Count);

                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgvprDetails.Rows[i].Cells[iprslno].Value = i + 1;
                        dgvprDetails.Rows[i].Cells[iprId].Value = itemSource[i].prid;
                        dgvprDetails.Rows[i].Cells[iprproduct].Value = itemSource[i].Itemname;
                        dgvprDetails.Rows[i].Cells[iprprate].Value = itemSource[i].Prate;
                        dgvprDetails.Rows[i].Cells[iprsrate].Value = itemSource[i].Srate;
                        dgvprDetails.Rows[i].Cells[iprmrp].Value = itemSource[i].MRP;
                        dgvprDetails.Rows[i].Cells[iprqoh].Value = itemSource[i].QOH;
                        dgvprDetails.Rows[i].Cells[iprPtype].Value = itemSource[i].Fcid;
                    }
                    dgvprDetails.Focus();
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void dgv_item_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            filldata(e.RowIndex);
        }

        private void cmbTaxType_SelectedValueChanged(object sender, EventArgs e)
        {           
            
            int[] columnsToRemove1 = { 0,1, 2, 3 };
            int[] rowsToRemove1 = { 5,6,7,8 };

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
            lblunit.Dock = DockStyle.Fill;
            lbltax.Dock = DockStyle.Fill;
            lbltaxtype.Dock = DockStyle.Fill;
            txtdesc.Dock = DockStyle.Fill;
            if (cmbTaxType.Text == "GST")
            {
                lbligst.Dock = DockStyle.Fill;
                cmbIgstPrcnt.Dock = DockStyle.Fill;
                tlp_s_c_gst.Dock = DockStyle.Fill;
                tlp_S_C_text_.Dock = DockStyle.Fill;
                lbldes.Dock = DockStyle.Fill;
                txtSgstPrcnt.Dock = DockStyle.Top;
                txtCgstPrcnt.Dock = DockStyle.Top;
                //row1
                tlp_registration.Controls.Add(lblunit, 0, 5);
                tlp_registration.Controls.Add(lbltax, 1, 5);
                tlp_registration.Controls.Add(lbltaxtype, 2, 5);
                tlp_registration.Controls.Add(lbligst, 3, 5);

                //row2
                tlp_registration.Controls.Add(cmbunit, 0, 6);
                tlp_registration.Controls.Add(cmbtax, 1, 6);
                tlp_registration.Controls.Add(cmbTaxType, 2, 6);
                tlp_registration.Controls.Add(cmbIgstPrcnt, 3, 6);

                //row3
                tlp_registration.Controls.Add(tlp_s_c_gst, 0, 7);
                tlp_registration.Controls.Add(lbldes, 1, 7);
                //row4
                tlp_registration.Controls.Add(tlp_S_C_text_, 0, 8);
                tlp_registration.Controls.Add(txtdesc, 1, 8);
                tlp_registration.Controls.Add(btn_del_ingre, 2, 8);

                cmbIgstPrcnt.Focus();
            }
            else if (cmbTaxType.Text == "VAT")
            {
                cmbVatPrcnt.Dock = DockStyle.Fill;
                lblvat.Dock = DockStyle.Fill;
                lbldes.Dock = DockStyle.Fill;
                //row1
                tlp_registration.Controls.Add(lblunit, 0, 5);
                tlp_registration.Controls.Add(lbltax, 1, 5);
                tlp_registration.Controls.Add(lbltaxtype, 2, 5);
                tlp_registration.Controls.Add(lblvat, 3, 5);

                //row2
                tlp_registration.Controls.Add(cmbunit, 0, 6);
                tlp_registration.Controls.Add(cmbtax, 1, 6);
                tlp_registration.Controls.Add(cmbTaxType, 2, 6);
                tlp_registration.Controls.Add(cmbVatPrcnt, 3, 6);

                //row3
                tlp_registration.Controls.Add(lbldes, 0, 7);

                //row4
                tlp_registration.Controls.Add(txtdesc, 0, 8);
                tlp_registration.Controls.Add(btn_del_ingre, 1, 8);

                cmbVatPrcnt.Focus();
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

        private void txtmrp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtmrp.Text))
                txtmrp.Text = "0";
            if (string.IsNullOrWhiteSpace(txtsrate.Text))
                txtsrate.Text = "0";
            //if (Convert.ToDecimal(txtmrp.Text) < Convert.ToDecimal(txtsrate.Text))
            //{
            //    CMessageBox.Show("MRP Rate Must be Greater than Sale Price", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtmrp.Focus();
            //}
        }

        private void dgvprDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowno = dgvprDetails.CurrentCell.RowIndex;
            GridkeydownEnter(rowno);
        }
        public void GridkeydownEnter(int rowno)
        {
            try
            {
                    BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "i.*,pr.QOH,pr.Id as PrId";
                    objSearch.Table = "tblitem i left join tblProductRate pr on pr.ProductId = i.ItemId";
                    objSearch.WhereNdGroupBy = "where i.status = 1 and pr.id=" + dgvprDetails.Rows[rowno].Cells[iprId].Value;
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count == 1)
                {
                    txtitemid.Text = Convert.ToString(itemSource[0].ItemId);
                    txtincr_name.Text = Convert.ToString(itemSource[0].Itemname);
                    txtbarcode.Text = Convert.ToString(itemSource[0].ItemCode_UPC);
                    txtdesc.Text = Convert.ToString(itemSource[0].summary);
                    txtprate.Text = Convert.ToString(itemSource[0].Prate);
                    txtsrate.Text = Convert.ToString(itemSource[0].Srate);
                    txtmrp.Text = Convert.ToString(itemSource[0].MRP);
                    txtqty.Text = Convert.ToString(itemSource[0].quantity);
                    cmbunit.SelectedValue = Convert.ToInt64(itemSource[0].unit);
                    txtmrp.Text = Convert.ToString(itemSource[0].MRP);
                    txtprid.Text = Convert.ToString(itemSource[0].prid);

                    if (Convert.ToInt32(itemSource[0].Saletype) == 1)
                        rdb_dsale.Checked = true;
                    else
                        rdb_notsale.Checked = true;
                    cmbtax.Text = Convert.ToString(itemSource[0].Tax);
                    cmbtax.Text = Convert.ToString(itemSource[0].TaxType);
                    if (cmbtax.Text == "GST")
                        cmbIgstPrcnt.SelectedValue = Convert.ToInt64(itemSource[0].TaxSettingsId);
                    else
                        cmbVatPrcnt.SelectedValue = Convert.ToInt64(itemSource[0].TaxSettingsId);
                    cmb_inc_categ.SelectedValue = Convert.ToInt64(itemSource[0].Fcid);
                    pnlPrRates.Visible = false;
                    btn_del_ingre.Visible = true;
                    toggle_grid.PerformClick();
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void btnPrClose_Click(object sender, EventArgs e)
        {
            pnlPrRates.Visible = false;
        }

        private void dgvprDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                int rowno = dgvprDetails.CurrentCell.RowIndex;
                GridkeydownEnter(rowno);
            }
      
        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                ApplyRoundedTextBox(txtincr_name, 10);
                ApplyRoundedTextBox(txtdesc, 10);
                ApplyRoundedTextBox(txtbarcode, 10);
                ApplyRoundedTextBox(txtmrp, 10);
                ApplyRoundedTextBox(txtqty, 10);
                ApplyRoundedTextBox(txtprate, 10);
                ApplyRoundedTextBox(txtsrate, 10);
                //ApplyRoundedTextBox(txtCgstPrcnt, 10);
                //ApplyRoundedTextBox(txtSgstPrcnt, 10);
                txtincr_name.Font = new Font(txtincr_name.Font.FontFamily, 12);
                txtdesc.Font = new Font(txtdesc.Font.FontFamily, 12);
                txtbarcode.Font = new Font(txtbarcode.Font.FontFamily, 12);
                txtmrp.Font = new Font(txtmrp.Font.FontFamily, 12);
                txtqty.Font = new Font(txtqty.Font.FontFamily, 12);
                txtprate.Font = new Font(txtprate.Font.FontFamily, 12);
                txtsrate.Font = new Font(txtsrate.Font.FontFamily, 12);
                txtCgstPrcnt.Font = new Font(txtCgstPrcnt.Font.FontFamily, 12);
                txtSgstPrcnt.Font = new Font(txtSgstPrcnt.Font.FontFamily, 12);
            }
            catch (Exception)
            {

            }
        }
     
        private void ApplyRoundedTextBox(TextBox textBox, int radius)
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
            textBox.Region = new Region(path);
        }

        #endregion

        private void txtbarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            #region Allow 5 Digit and Two decimal Point
            string input = (sender as TextBox).Text;
            if (e.KeyChar == '.')
            {
                if ((sender as TextBox).MaxLength == 5)
                    (sender as TextBox).MaxLength = 8;
                else
                {
                    if (input.Contains('.'))
                    {
                        (sender as TextBox).MaxLength = 8;
                    }
                    else
                    {
                        (sender as TextBox).MaxLength = 5;
                    }
                }

            }
            else
            {
                if (input.Contains('.'))
                {
                    (sender as TextBox).MaxLength = 8;
                }
                else
                {
                    (sender as TextBox).MaxLength = 5;
                }
            }
            #endregion
        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            
                e.Handled = true;
            
            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1)
            
                e.Handled = true;
            
        }

        private void txtincr_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            int returnValue = 0;
            DataTable dtColumnAlign = new DataTable();
            dtColumnAlign.Columns.Add();
            DmnSearch _objSearch = new DmnSearch();
            _objSearch.ColumnHeaders = "ID,ItemCode_UPC,Itemname";
            _objSearch.Select = " ItemId as Id,ItemCode_UPC as Field1,Itemname as Field2";
            _objSearch.Table = "tblItem";
            _objSearch.WhereNdGroupBy = "where Status = 1";
            _objSearch.OrderBy = " ORDER BY Itemname";
            var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);
            if (gridSource.Count == 0)
            {
                CMessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToInt32(search._retValue);
                    }
                }
                if (returnValue >0)
                {
                    int incgreid = returnValue;
                    BllSearch<DmnItem> bllSearch1 = new BllSearch<DmnItem>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "i.*,pr.SRate,pr.PRate,pr.QOH,pr.Id as PrId";
                    objSearch.Table = "tblitem i left join tblProductRate pr on pr.ProductId = i.ItemId";
                    objSearch.WhereNdGroupBy = "where i.status = 1 and itemid=" + incgreid;
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count == 1)
                    {
                        txtitemid.Text = Convert.ToString(incgreid);
                        txtincr_name.Text = Convert.ToString(itemSource[0].Itemname);
                        txtbarcode.Text = Convert.ToString(itemSource[0].ItemCode_UPC);
                        txtdesc.Text = Convert.ToString(itemSource[0].summary);
                        txtprate.Text = Convert.ToString(itemSource[0].Prate);
                        txtsrate.Text = Convert.ToString(itemSource[0].Srate);
                        txtmrp.Text = Convert.ToString(itemSource[0].MRP);
                        txtqty.Text = Convert.ToString(itemSource[0].quantity);
                        cmbunit.SelectedValue = Convert.ToInt64(itemSource[0].unit);
                        txtmrp.Text = Convert.ToString(itemSource[0].MRP);
                        txtprid.Text = Convert.ToString(itemSource[0].prid);

                        if (Convert.ToInt32(itemSource[0].Saletype) == 1)
                            rdb_dsale.Checked = true;
                        else
                            rdb_notsale.Checked = true;
                        cmbtax.Text = Convert.ToString(itemSource[0].Tax);
                        cmbTaxType.Text = Convert.ToString(itemSource[0].TaxType);
                        if (cmbTaxType.Text == "GST")
                            cmbIgstPrcnt.SelectedValue = Convert.ToInt64(itemSource[0].TaxSettingsId);
                        else
                            cmbVatPrcnt.SelectedValue = Convert.ToInt64(itemSource[0].TaxSettingsId);
                        cmb_inc_categ.SelectedValue = Convert.ToInt64(itemSource[0].Fcid);
                        btn_del_ingre.Visible = true;
                        toggle_grid.PerformClick();
                    }
                    else if (itemSource.Count > 1)
                    {
                        pnlPrRates.Visible = true;
                        pnlPrRates.Location = new Point(this.ClientSize.Width / 2 - pnlPrRates.Size.Width / 2, (this.ClientSize.Height / 3 - pnlPrRates.Size.Height / 3));
                        dgvprDetails.Rows.Clear();
                        dgvprDetails.Rows.Add(itemSource.Count);

                        for (int i = 0; i < itemSource.Count; i++)
                        {
                            dgvprDetails.Rows[i].Cells[iprslno].Value = i + 1;
                            dgvprDetails.Rows[i].Cells[iprId].Value = itemSource[i].prid;
                            dgvprDetails.Rows[i].Cells[iprproduct].Value = itemSource[i].Itemname;
                            dgvprDetails.Rows[i].Cells[iprprate].Value = itemSource[i].Prate;
                            dgvprDetails.Rows[i].Cells[iprsrate].Value = itemSource[i].Srate;
                            dgvprDetails.Rows[i].Cells[iprmrp].Value = itemSource[i].MRP;
                            dgvprDetails.Rows[i].Cells[iprqoh].Value = itemSource[i].QOH;
                            dgvprDetails.Rows[i].Cells[iprPtype].Value = itemSource[i].Fcid;
                        }
                        dgvprDetails.Focus();
                    }
                }
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    Movefocus(1);
                    return true; // Mark the key as handled
                case Keys.Enter:
                    if(dgv_item.Visible)
                    {
                        int rowIndex = dgv_item.CurrentRow.Index;
                        filldata(rowIndex);
                    }
                    else
                        Movefocus(0);
                    return true; // Mark the key as handled
                case Keys.Escape:
                    if(tlp_grid.Visible)
                    {
                        tlp_grid.Visible = false;
                        tlp_registration.Visible = true;
                        Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
                        tlp_main.Controls.Remove(controlToRemove);
                        tlp_main.Controls.Add(tlp_registartion_toggle, 1, 0);
                        tlp_registartion_toggle.Dock = DockStyle.Fill;
                    }
                    else
                    {
                        FrmDashboard _instancedash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                        _instancedash.Closeform();
                    }
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        public void Movefocus(int keytype)
        {
            if (txtincr_name.Focused)
                txtbarcode.Focus();
            else if (txtincr_name.Focused)
                txtbarcode.Focus();
            else if (txtbarcode.Focused)

                cmb_inc_categ.Focus();
            else if (cmb_inc_categ.Focused)
                rdb_dsale.Focus();
            else if (rdb_dsale.Focused)
            {
                //if (keytype == 0)
                //{
                //    if (rdb_dsale.Checked)
                //        rdb_dsale.Checked = true;
                //    else
                //        rdb_dsale.Checked = false;
                //}
                //else if (keytype == 1)
                    rdb_notsale.Focus();
            }
            else if (rdb_notsale.Focused)
            {
                //if (keytype == 0)
                //{
                //    if (rdb_notsale.Checked)
                //        rdb_notsale.Checked = true;
                //    else
                //        rdb_notsale.Checked = false;
                //}
                //else if (keytype == 1)
                    txtprate.Focus();
            }
         
            else if (txtprate.Focused)
                txtsrate.Focus();
            else if (txtsrate.Focused)
                txtmrp.Focus();
            else if (txtmrp.Focused)
                txtqty.Focus();
            else if (txtqty.Focused)
                cmbunit.Focus();
            else if (cmbunit.Focused)
                cmbtax.Focus();
            else if (cmbtax.Focused)
                cmbTaxType.Focus();
            else if (cmbTaxType.Focused)
            {
                if (cmbTaxType.Text == "GST")
                {
                    cmbIgstPrcnt.Focus();
                }
                if (cmbTaxType.Text == "VAT")
                {
                    cmbVatPrcnt.Focus();
                }
            }
            else if (cmbIgstPrcnt.Focused)
                txtdesc.Focus();
            else if (cmbVatPrcnt.Focused)
                txtdesc.Focus();
            else if (txtdesc.Focused)            
                btnsave.Focus();
            else if (keytype == 1)
            {
                if (btnsave.Focused)
                    btnSearch.Focus();
                else if (btnSearch.Focused)
                    btnrefresh.Focus();
                else if (btnrefresh.Focused)
                    btnclose.Focus();
            }
            else if (keytype == 0)
            {
                if (btnsave.Focused)
                    btnsave.PerformClick();
                else if (btnrefresh.Focused)
                    btnrefresh.PerformClick();
                else if (btnSearch.Focused)
                    btnSearch.PerformClick();
                else if (btnclose.Focused)
                    btnclose.PerformClick();
            }

        }

        private void FrmItemRegistartion_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }



    }
}
