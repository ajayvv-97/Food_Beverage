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
using System.Collections.Generic;
using FoodandBeverage.Classes;

namespace FoodandBeverage.Forms
{
    public partial class FrmItemRegistartion : Form
    {
        public FrmItemRegistartion()
        {
            InitializeComponent();
            tlp_category.Visible = false;
            tlp_Unit.Visible = false;
            dgv_category.CellDoubleClick += dgv_category_CellDoubleClick;
            dgv_unit.CellDoubleClick += dgv_unit_CellDoubleClick;
        }
        #region Declare Grid
        public  int slno = 0;
        public  int idno = 1;
        public  int incre_nameno = 2;
        public  int incre_categno = 3;
        public  int iunitno = 4;
        public  int iqtyno = 5;
        public  int iprateno = 6;
        public  int israteno = 7;
        public  int imrpno = 8;
        public  int itaxno = 9;
        public  int itaxtypeno = 10;
        public  int itaxperno = 11;
        public  int idescno = 12;
        public  int istatusno = 13;
        public  int iunitidno = 14;
        public int incre_categ_idno = 15;

        public int ihsncodeno = 16;
        public int iReorderLevelno = 17;
        public int iIsexpireno = 18;
        public int iLockSaleRateno = 19;
        public int iBaseunitno = 20;
        public int iConversionfactorno = 21;

        //Prid
        public  int iprId = 0;
        public  int iprslno = 1;
        public  int iprproduct = 2;
        public  int iprprate = 3;
        public  int iprsrate = 4;
        public  int iprmrp = 5;
        public  int iprqoh = 6;
        public  int iprPtype = 7;
        #endregion

        ClsGeneral clsgen = new ClsGeneral();


        List<DmnComboBox> gridSource_Foodcate = new List<DmnComboBox>();
        List<DmnComboBox> gridSource_itemcate = new List<DmnComboBox>();

        public void Fillitem()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "i.ItemId Field1,i.Itemname Field2,u.UnitName Field3," +
                    "(Select sum(QOH) from tblProductRate where ProductId=i.ItemId) as Field4," +
                    "(Select Top(1)Prate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field5," +
                    "(Select Top(1)Srate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field6," +
                    "(Select Top(1)MRP from tblProductRate where ProductId=i.ItemId order by Id desc) as Field7," +
                    "i.Tax Field8,ts.TaxType Field9,ts.IgstPrcnt Field10,ts.VatPrcnt Field11,i.summary Field12,i.Status Field13,u.UId Field114,i.Fcid Field15,c.CategoryName Field16,i.HSNcode Field17,i.ReorderLevel Field18,i.Isexpire Field19,i.LockSaleRate Field20,i.Baseunit Field21,i.Conversionfactor Field22";
                dmnSearch.Table = "tblItem i " +
                    " LEFT JOIN tblUnit u on u.UId=i.unit " +
                    " LEFT JOIN tblTaxSettings ts on ts.TaxSettingsId=i.TaxSettingsId" +
                    " LEFT JOIN tblCategory_FTI c on c.Fcid=i.Fcid";
                dmnSearch.WhereNdGroupBy = "where i.Status=1";
                dmnSearch.OrderBy = "order by i.ItemId desc ";
                var itemSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                dgv_item.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_item.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_item.Rows[i].Cells[slno].Value = i + 1;
                        dgv_item.Rows[i].Cells[idno].Value = itemSource[i].Field1;
                        dgv_item.Rows[i].Cells[incre_nameno].Value = itemSource[i].Field2;
                        dgv_item.Rows[i].Cells[incre_categno].Value = itemSource[i].Field16;
                        dgv_item.Rows[i].Cells[iunitno].Value = itemSource[i].Field3;
                        dgv_item.Rows[i].Cells[iqtyno].Value = itemSource[i].Field4;
                        dgv_item.Rows[i].Cells[iprateno].Value = itemSource[i].Field5;
                        dgv_item.Rows[i].Cells[israteno].Value = itemSource[i].Field6;
                        dgv_item.Rows[i].Cells[imrpno].Value = itemSource[i].Field7;
                        dgv_item.Rows[i].Cells[itaxno].Value = itemSource[i].Field8;
                        dgv_item.Rows[i].Cells[itaxtypeno].Value = itemSource[i].Field9;
                        if (itemSource[i].Field9 == "GST")
                            dgv_item.Rows[i].Cells[itaxperno].Value = itemSource[i].Field10;
                        else
                            dgv_item.Rows[i].Cells[itaxperno].Value = itemSource[i].Field11;
                        dgv_item.Rows[i].Cells[idescno].Value = itemSource[i].Field12;
                        dgv_item.Rows[i].Cells[istatusno].Value = Convert.ToInt32(itemSource[i].Field13);
                        dgv_item.Rows[i].Cells[iunitidno].Value = itemSource[i].Field14;
                        dgv_item.Rows[i].Cells[incre_categ_idno].Value = itemSource[i].Field15;
                        dgv_item.Rows[i].Cells[ihsncodeno].Value = itemSource[i].Field17;
                        dgv_item.Rows[i].Cells[iReorderLevelno].Value = itemSource[i].Field18;
                        dgv_item.Rows[i].Cells[iIsexpireno].Value = itemSource[i].Field19;
                        dgv_item.Rows[i].Cells[iLockSaleRateno].Value = itemSource[i].Field20;
                        dgv_item.Rows[i].Cells[iBaseunitno].Value = itemSource[i].Field21;
                        dgv_item.Rows[i].Cells[iConversionfactorno].Value = itemSource[i].Field22;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Item page Fillitem()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
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
                objSearch.WhereNdGroupBy = "where Status=1 and CategoryType in(2) ";
                objSearch.OrderBy = "";
                gridSource_itemcate = bllSearch.GetSearchData("prdComboFill", objSearch);

                cmb_inc_categ.DataSource = gridSource_itemcate;
                cmb_inc_categ.ValueMember = "Id";
                cmb_inc_categ.DisplayMember = "Value";

                objSearch.WhereNdGroupBy = "where Status=1 and CategoryType in(0) ";
                objSearch.OrderBy = "";
                gridSource_Foodcate = bllSearch.GetSearchData("prdComboFill", objSearch);

                objSearch.Select = " UId,UnitName";
                objSearch.Table = " tblUnit";
                objSearch.WhereNdGroupBy = "where status=1";
                objSearch.OrderBy = "";
                var gridSource1 = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmbunit.DataSource = gridSource1;
                cmbunit.ValueMember = "Id";
                cmbunit.DisplayMember = "Value";

                cmbbaseunit.DataSource = gridSource1;
                cmbbaseunit.ValueMember = "Id";
                cmbbaseunit.DisplayMember = "Value";

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page combofill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
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
            catch (Exception ex)
            {
                clsgen.LogException("Item page Taxfill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }

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
                    if (string.IsNullOrWhiteSpace(txtprid.Text))
                        txtprid.Text = "0";
                    if (string.IsNullOrWhiteSpace(txtdisper.Text))
                        txtdisper.Text = "0";
                    if (string.IsNullOrWhiteSpace(txtdisamt.Text))
                        txtdisamt.Text = "0";

                    dmnitemreg.ItemId = Convert.ToInt32(txtitemid.Text);
                    dmnitemreg.Itemname = txtincr_name.Text;
                    dmnitemreg.summary = txtdesc.Text;
                    dmnitemreg.Prate = Convert.ToDecimal(txtprate.Text);
                    dmnitemreg.Srate = Convert.ToDecimal(txtsrate.Text);
                    dmnitemreg.quantity = Convert.ToDecimal(txtqty.Text);
                    dmnitemreg.unit = Convert.ToInt32(cmbunit.SelectedValue);
                    dmnitemreg.Fcid = Convert.ToInt32(cmb_inc_categ.SelectedValue);
                    dmnitemreg.ItemCode_UPC = Convert.ToString(txtbarcode.Text);
                    dmnitemreg.PrId = Convert.ToInt32(txtprid.Text);
                    dmnitemreg.MRP = Convert.ToDecimal(txtmrp.Text);

                    dmnitemreg.DiscountPer = Convert.ToDecimal(txtdisper.Text);
                    dmnitemreg.DiscountAmt = Convert.ToDecimal(txtdisamt.Text);

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

                    dmnitemreg.CreatedBy = FrmDashboard.UserId;
                    dmnitemreg.BranchId = FrmDashboard.BranchId;

                    if (chb_active.Checked)
                        dmnitemreg.Status = 1;
                    else
                        dmnitemreg.Status = 0;

                    dmnitemreg.HSNcode = Convert.ToString(txthsncode.Text);
                    dmnitemreg.ReorderLevel = Convert.ToInt32(txtreorderlevel.Text);
                    dmnitemreg.Isexpire = Convert.ToBoolean(chk_enableexpire.Checked);
                    dmnitemreg.LockSaleRate = Convert.ToBoolean(chklocksalerate.Checked);
                    dmnitemreg.Baseunit = Convert.ToInt32(cmbbaseunit.SelectedValue);
                    dmnitemreg.Conversionfactor = Convert.ToDecimal(txtConversionFactor.Text);

                    var usertype = blltenreg.AddData("prdInsert_upd_ItemReg", dmnitemreg);

                    if (usertype.ItemId > 0)
                    {
                        if (Convert.ToUInt32(txtitemid.Text) > 0)
                            CMessageBox.Show("Update Successfully saved");
                        else
                            CMessageBox.Show("Successfully saved");

                        btnrefresh.PerformClick();
                    }
                    else
                        CMessageBox.Show("Save failed", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page btnsave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }
        public void clearall()
        {
            try
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
                txtdisamt.Text = "0.00";
                txtdisper.Text = "0.00";

                rdb_dsale.Enabled = true;
                rdb_notsale.Enabled = true;
                rdb_notsale.Checked = true;
                chb_active.Checked = true;


                tlp_des_rem.ColumnStyles[0] = new ColumnStyle(SizeType.AutoSize);
                tlp_des_rem.ColumnStyles[1] = new ColumnStyle(SizeType.AutoSize);
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page clearall()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }

        }


        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool MandatoryCheck()
        {
            try
            {
                if (string.IsNullOrEmpty(txtprate.Text))
                    txtprate.Text = "0";
                if (string.IsNullOrEmpty(txtqty.Text))
                    txtqty.Text = "0";
                if (string.IsNullOrEmpty(txtmrp.Text))
                    txtmrp.Text = "0";
                if (string.IsNullOrEmpty(txtsrate.Text))
                    txtsrate.Text = "0";

                if (txtincr_name.Text == "" || txtincr_name.Text == null)
                {
                    CMessageBox.Show("Please Enter Item Name", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtincr_name.Focus();
                    return false;
                }
                else if (string.IsNullOrEmpty(txtbarcode.Text) && rdb_dsale.Checked)
                {
                    CMessageBox.Show("Please Enter Barcode", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtbarcode.Focus();
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
                else if (rdb_dsale.Checked != true)
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
                if (cmbtax.Text == "")
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
                if ((txtprate.Text == null || txtprate.Text == "" || (Convert.ToDecimal(txtprate.Text) == 0)) && (Convert.ToDecimal(txtsrate.Text) > 0) && (Convert.ToDecimal(txtmrp.Text) > 0))
                {
                    CMessageBox.Show("Please Enter Purchase Rate", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtprate.Focus();
                    return false;
                }

                if ((txtprate.Text == null || txtprate.Text == "" || (Convert.ToDecimal(txtprate.Text) == 0)) && (Convert.ToDecimal(txtmrp.Text) > 0) && (rdb_dsale.Checked))
                {
                    CMessageBox.Show("Please Enter Purchase Rate", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtprate.Focus();
                    return false;
                }

                if (Convert.ToDecimal(txtprate.Text) > 0 && (rdb_dsale.Checked))
                {
                    if (Convert.ToDecimal(txtsrate.Text) < Convert.ToDecimal(txtprate.Text))
                    {
                        CMessageBox.Show("Sale Rate Must be Graterthan Purchase Rate", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtsrate.Focus();
                        return false;
                    }
                }
                if (Convert.ToDecimal(txtsrate.Text) > 0 && (rdb_dsale.Checked))
                {
                    if (Convert.ToDecimal(txtmrp.Text) < Convert.ToDecimal(txtsrate.Text))
                    {
                        CMessageBox.Show("MRP Must be Graterthan Sale Rate", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtmrp.Focus();
                        return false;
                    }
                }

                if (Convert.ToDecimal(txtsrate.Text) > 0 && (rdb_dsale.Checked))
                {
                    if (Convert.ToDecimal(txtmrp.Text) < Convert.ToDecimal(txtsrate.Text))
                    {
                        CMessageBox.Show("MRP Must be Graterthan Sale Rate", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtmrp.Focus();
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page Fillitem()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
                return false;
            }



        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Fillitem();
                clearall();
                combofill();
                Taxfill();
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

                Fillcombo(2);
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page btnrefresh_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }

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
                rb_item.Checked = false;
                rb_notsale.Checked = false;
                if(dgv_item.RowCount>0)
                    dgv_item.CurrentCell = dgv_item[0, 0];
                tlp_registration.Visible = false;
                tlp_grid.Visible = true;
                Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
                tlp_main.Controls.Remove(controlToRemove);
                tlp_main.Controls.Add(tlp_grid, 1, 0);
                tlp_grid.Dock = DockStyle.Fill;
                Fillitem();
                dgv_item.Focus();
                UIHelper.RoundCorners(dgv_item, 25, null);
                UIHelper.RoundCorners(tableLayoutPanel2, 25, null);
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page btntoggle_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }

        private void toggle_grid_Click(object sender, EventArgs e)
        {
            try
            {
                tlp_grid.Visible = false;
                tlp_registration.Visible = true;
                Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
                tlp_main.Controls.Remove(controlToRemove);
                tlp_main.Controls.Add(tlp_registartion_toggle, 1, 0);
                tlp_registartion_toggle.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page toggle_grid_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
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
            try
            {
                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    AppSession.RegisterForm(this, (int)LogoutFormList.operation);
                }
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

                dgv_item.BorderStyle = BorderStyle.None;
                cmbTaxType.SelectedIndex = 0;
                cmbtax.SelectedIndex = 0;
                //dgv_item.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                //dgv_item.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                //dgv_item.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                //dgv_item.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                //dgv_item.BackgroundColor = Color.White;

                dgv_item.EnableHeadersVisualStyles = false;
                dgv_item.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgv_item.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgv_item.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                dgvprDetails.BorderStyle = BorderStyle.None;
                dgvprDetails.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvprDetails.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvprDetails.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgvprDetails.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvprDetails.BackgroundColor = Color.White;

                dgvprDetails.EnableHeadersVisualStyles = false;
                dgvprDetails.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvprDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvprDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                Fillcombo(2);
                cmbunit.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page FrmItemRegistartion_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }


        }

        private void btn_del_ingre_Click(object sender, EventArgs e)
        {
            try
            {
                var result = CMessageBox.Show("Do You want Delete This Category", "Confirm The Actionn", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    chb_active.Checked = false;
                    btnsave.PerformClick();
                    Fillitem();
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page btn_del_ingre_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }


        }
        public void filldata(int rowno)
        {
            try
            {
                int incgreid = Convert.ToInt32(dgv_item.Rows[rowno].Cells[idno].Value);
                BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "i.*,pr.SRate,pr.PRate,pr.MRP,pr.QOH,pr.Id as PrId";
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
                    txthsncode.Text = Convert.ToString(itemSource[0].HSNcode);
                    txtreorderlevel.Text = Convert.ToString(itemSource[0].ReorderLevel);
                    chk_enableexpire.Checked = itemSource[0].Isexpire;
                    chklocksalerate.Checked = itemSource[0].LockSaleRate;
                    cmbbaseunit.SelectedValue = Convert.ToInt32(itemSource[0].Baseunit);
                    txtConversionFactor.Text = Convert.ToString(itemSource[0].Conversionfactor);
                    cmb_exp_dates.Text = itemSource[0].ExpiryDate > DateTime.MinValue
                        ? itemSource[0].ExpiryDate.ToString("yyyy-MM-dd")
                        : string.Empty;
                    txtmrp.Text = Convert.ToString(itemSource[0].MRP);
                    txtprid.Text = Convert.ToString(itemSource[0].PrId);

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
                        dgvprDetails.Rows[i].Cells[iprId].Value = itemSource[i].PrId;
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page filldata()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }
        private void dgv_item_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            filldata(e.RowIndex);
        }

        private void cmbTaxType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                ControlArrangement();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Item page cmbTaxType_SelectedValueChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page cmbIgstPrcnt_SelectedIndexChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }

        private void btn_grid_refresh_Click(object sender, EventArgs e)
        {
            Fillitem();
        }

        private void txtmrp_Leave(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page txtmrp_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }


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
                objSearch.Select = "i.ItemId,i.Itemname,i.ItemCode_UPC,i.summary,i.unit,i.Saletype,i.Tax,i.TaxType,i.TaxSettingsId,i.Fcid,pr.QOH,pr.Prate,pr.SRate,pr.MRP,pr.Id as PrId,i.DiscountAmt,i.DiscountPer,i.HSNcode,i.ExpiryDate,i.ReorderLevel,i.Isexpire,i.LockSaleRate,i.Baseunit,i.Conversionfactor";
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
                    txtqty.Text = Convert.ToString(itemSource[0].QOH);
                    cmbunit.SelectedValue = Convert.ToInt64(itemSource[0].unit);
                    txthsncode.Text = Convert.ToString(itemSource[0].HSNcode);
                    txtreorderlevel.Text = Convert.ToString(itemSource[0].ReorderLevel);
                    chk_enableexpire.Checked = itemSource[0].Isexpire;
                    chklocksalerate.Checked = itemSource[0].LockSaleRate;
                    cmbbaseunit.SelectedValue = Convert.ToInt32(itemSource[0].Baseunit);
                    txtConversionFactor.Text = Convert.ToString(itemSource[0].Conversionfactor);
                    cmb_exp_dates.Text = itemSource[0].ExpiryDate > DateTime.MinValue
                        ? itemSource[0].ExpiryDate.ToString("yyyy-MM-dd")
                        : string.Empty;
                    txtprid.Text = Convert.ToString(itemSource[0].PrId);

                    txtdisamt.Text = Convert.ToString(itemSource[0].DiscountAmt);
                    txtdisper.Text = Convert.ToString(itemSource[0].DiscountPer);

                    if (Convert.ToInt32(itemSource[0].Saletype) == 1)
                        rdb_dsale.Checked = true;
                    else
                        rdb_notsale.Checked = true;

                    rdb_dsale.Enabled = false;
                    rdb_notsale.Enabled = false;

                    cmbtax.Text = Convert.ToString(itemSource[0].Tax);
                    cmbtax.Text = Convert.ToString(itemSource[0].TaxType);
                    if (cmbTaxType.Text == "GST")
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page GridkeydownEnter()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
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
                ApplyRoundedTextBox(txtdisper, 10);
                ApplyRoundedTextBox(txtdisamt, 10);
 
                txtincr_name.Font = new Font(txtincr_name.Font.FontFamily, 12);
                txtdesc.Font = new Font(txtdesc.Font.FontFamily, 12);
                txtbarcode.Font = new Font(txtbarcode.Font.FontFamily, 12);
                txtmrp.Font = new Font(txtmrp.Font.FontFamily, 12);
                txtqty.Font = new Font(txtqty.Font.FontFamily, 12);
                txtprate.Font = new Font(txtprate.Font.FontFamily, 12);
                txtsrate.Font = new Font(txtsrate.Font.FontFamily, 12);
                txtCgstPrcnt.Font = new Font(txtCgstPrcnt.Font.FontFamily, 12);
                txtSgstPrcnt.Font = new Font(txtSgstPrcnt.Font.FontFamily, 12);
                txtdisper.Font = new Font(txtdisper.Font.FontFamily, 12);
                txtdisamt.Font = new Font(txtdisamt.Font.FontFamily, 12);
            }
            catch (Exception ex)
            {

                clsgen.LogException("Item page textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
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
                textBox.Region = new Region(path);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Item page ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }


        }

        #endregion

        private void txtbarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page txtbarcode_KeyPress()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }


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
            try
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
                    if (returnValue > 0)
                    {
                        int incgreid = returnValue;
                        BllSearch<DmnItem> bllSearch1 = new BllSearch<DmnItem>();
                        DmnSearch objSearch = new DmnSearch();
                        objSearch.Select = "i.*,pr.SRate,pr.PRate,pr.QOH,pr.MRP,pr.Id as PrId";
                        objSearch.Table = "tblitem i left join tblProductRate pr on pr.ProductId = i.ItemId";
                        objSearch.WhereNdGroupBy = "where i.status = 1 and  pr.QOH>0 and itemid=" + incgreid;
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
                            txtqty.Text = Convert.ToString(itemSource[0].QOH);
                            cmbunit.SelectedValue = Convert.ToInt64(itemSource[0].unit);
                    txthsncode.Text = Convert.ToString(itemSource[0].HSNcode);
                    txtreorderlevel.Text = Convert.ToString(itemSource[0].ReorderLevel);
                    chk_enableexpire.Checked = itemSource[0].Isexpire;
                    chklocksalerate.Checked = itemSource[0].LockSaleRate;
                    cmbbaseunit.SelectedValue = Convert.ToInt32(itemSource[0].Baseunit);
                    txtConversionFactor.Text = Convert.ToString(itemSource[0].Conversionfactor);
                    cmb_exp_dates.Text = itemSource[0].ExpiryDate > DateTime.MinValue
                        ? itemSource[0].ExpiryDate.ToString("yyyy-MM-dd")
                        : string.Empty;
                            txtmrp.Text = Convert.ToString(itemSource[0].MRP);
                            txtprid.Text = Convert.ToString(itemSource[0].PrId);

                            txtdisamt.Text = Convert.ToString(itemSource[0].DiscountAmt);
                            txtdisper.Text = Convert.ToString(itemSource[0].DiscountPer);

                            if (Convert.ToInt32(itemSource[0].Saletype) == 1)
                                rdb_dsale.Checked = true;
                            else
                                rdb_notsale.Checked = true;

                            rdb_dsale.Enabled = false;
                            rdb_notsale.Enabled = false;

                            cmbtax.Text = Convert.ToString(itemSource[0].Tax);
                            cmbTaxType.Text = Convert.ToString(itemSource[0].TaxType);
                            if (cmbTaxType.Text == "GST")
                                cmbIgstPrcnt.SelectedValue = Convert.ToInt64(itemSource[0].TaxSettingsId);
                            else
                                cmbVatPrcnt.SelectedValue = Convert.ToInt64(itemSource[0].TaxSettingsId);
                            cmb_inc_categ.SelectedValue = Convert.ToInt64(itemSource[0].Fcid);
                            btn_del_ingre.Visible = true;

                            tlp_des_rem.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 80);
                            tlp_des_rem.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 20);


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
                                dgvprDetails.Rows[i].Cells[iprId].Value = itemSource[i].PrId;
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page btnSearch_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
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
                    if (dgvprDetails.Visible)
                    {
                        int rowno = dgvprDetails.CurrentCell.RowIndex;
                        GridkeydownEnter(rowno);
                    }
                    else if (dgv_item.Visible && dgv_item.Focused)
                    {
                        int rowIndex = dgv_item.CurrentRow.Index;
                        filldata(rowIndex);
                    }
                    else if (txtitemcode_barcode.Focused)
                    {
                        serachbyname();
                    }
                    else
                        Movefocus(0);
                    return true; // Mark the key as handled
                case Keys.Escape:
                    if (pnlPrRates.Visible)
                    {
                        pnlPrRates.Visible = false;
                    }
                    else if (tlp_grid.Visible)
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
            try
            {
                if (txtincr_name.Focused)
                    txtbarcode.Focus();
                else if (txtincr_name.Focused)
                    txtbarcode.Focus();
                else if (txtbarcode.Focused)
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
                    cmb_inc_categ.Focus();
                }
                else if (cmb_inc_categ.Focused)
                    txtprate.Focus();

                else if (txtprate.Focused)
                    txtsrate.Focus();
                else if (txtsrate.Focused)
                    txtmrp.Focus();
                else if (txtmrp.Focused)
                    txtqty.Focus();
                else if (txtqty.Focused)
                {
                    if (rdb_notsale.Checked)
                        cmbunit.Focus();
                    else
                        txtdisper.Focus();
                }
                else if (txtdisper.Focused)
                    txtdisamt.Focus();
                else if (txtdisamt.Focused)
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page Movefocus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }



        }

        private void FrmItemRegistartion_Resize(object sender, EventArgs e)
        {
            #region setting Corner Round 
            UIHelper.RoundCorners(tlp_registration, 25, null);
            UIHelper.RoundCorners(dgv_item, 25, null);
            UIHelper.RoundCorners(tableLayoutPanel2, 25, null);

            UIHelper.RoundCorners(tlp_navbar, 10, null);
            #endregion
            
            textb_shape();
        }

        private void btnPrClos_Click(object sender, EventArgs e)
        {
            pnlPrRates.Visible = false;
        }

        private void rdb_dsale_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (rdb_dsale.Checked)
                {
                    cmb_inc_categ.DataSource = null;
                    cmb_inc_categ.DataSource = gridSource_Foodcate;
                    cmb_inc_categ.ValueMember = "Id";
                    cmb_inc_categ.DisplayMember = "Value";

                    int[] columnsToRemove1 = { 0, 1, 2, 3 };
                    int[] rowsToRemove1 = { 5, 6, 7, 8 };

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

                    tlp_des_rem.Controls.Add(txtdesc, 0, 0);
                    tlp_des_rem.Controls.Add(btn_del_ingre, 1, 0);
                    if (string.IsNullOrWhiteSpace(txtitemid.Text))
                        txtitemid.Text = "0";
                    if (Convert.ToInt32(txtitemid.Text) > 0)
                    {
                        tlp_des_rem.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 80);
                        tlp_des_rem.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 20);
                    }
                    else
                    {
                        tlp_des_rem.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
                        tlp_des_rem.ColumnStyles[1] = new ColumnStyle(SizeType.AutoSize);
                    }


                    txtdesc.Dock = DockStyle.Fill;
                    tlp_des_rem.Dock = DockStyle.Fill;

                    cmbtax.Dock = DockStyle.Fill;
                    cmbTaxType.Dock = DockStyle.Fill;
                    lblunit.Dock = DockStyle.Fill;
                    lbltax.Dock = DockStyle.Fill;
                    lbltaxtype.Dock = DockStyle.Fill;
                    txtdesc.Dock = DockStyle.Fill;
                    txtdisper.Dock = DockStyle.Fill;
                    txtdisamt.Dock = DockStyle.Fill;
                    lbldisamt.Dock = DockStyle.Fill;
                    lbldisper.Dock = DockStyle.Fill;

                    ControlArrangement();                    
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Item page rdb_dsale_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }


        }

        public void ControlArrangement()
        {
            try
            {
                tlp_registration.SuspendLayout();


                int[] columns = { 0, 1, 2, 3 };
                int[] rows = { 5, 6, 7, 8, 9, 10, 11, 12 };

                foreach (int column in columns)
                {
                    foreach (int row in rows)
                    {
                        Control control = tlp_registration.GetControlFromPosition(column, row);

                        if (control != null)
                            tlp_registration.Controls.Remove(control);
                    }
                }

                // Common docking.
                lblunit.Dock = DockStyle.Fill;
                cmbunit.Dock = DockStyle.Fill;
                lbltax.Dock = DockStyle.Fill;
                cmbtax.Dock = DockStyle.Fill;

                lblbaseunit.Dock = DockStyle.Fill;
                cmbbaseunit.Dock = DockStyle.Fill;
                lblConversionfactor.Dock = DockStyle.Fill;
                txtConversionFactor.Dock = DockStyle.Fill;

                lbltaxtype.Dock = DockStyle.Fill;
                cmbTaxType.Dock = DockStyle.Fill;
                lblhsn.Dock = DockStyle.Fill;
                txthsncode.Dock = DockStyle.Fill;

                lbldisper.Dock = DockStyle.Fill;
                txtdisper.Dock = DockStyle.Fill;
                lbldisamt.Dock = DockStyle.Fill;
                txtdisamt.Dock = DockStyle.Fill;

                lbligst.Dock = DockStyle.Fill;
                cmbIgstPrcnt.Dock = DockStyle.Fill;
                tlp_s_c_gst.Dock = DockStyle.Fill;
                tlp_S_C_text_.Dock = DockStyle.Fill;

                lblreorderlevel.Dock = DockStyle.Fill;
                txtreorderlevel.Dock = DockStyle.Fill;
                chk_enableexpire.Dock = DockStyle.Fill;
                chklocksalerate.Dock = DockStyle.Fill;
                lbldes.Dock = DockStyle.Fill;
                tlp_des_rem.Dock = DockStyle.Fill;
                txtdesc.Dock = DockStyle.Fill;

                // =========================================================
                // GST
                // =========================================================
                if (cmbTaxType.Text == "GST")
                {
                    txtSgstPrcnt.Dock = DockStyle.Top;
                    txtCgstPrcnt.Dock = DockStyle.Top;

                    if (rdb_dsale.Checked)
                    {
                        // Row 5: Discount, Unit and Tax labels.
                        tlp_registration.Controls.Add(lbldisper, 0, 5);
                        tlp_registration.Controls.Add(lbldisamt, 1, 5);
                        tlp_registration.Controls.Add(lblunit, 2, 5);
                        tlp_registration.Controls.Add(lblbaseunit, 3, 5);

                        // Row 6: Discount, Unit and Tax controls.
                        tlp_registration.Controls.Add(txtdisper, 0, 6);
                        tlp_registration.Controls.Add(txtdisamt, 1, 6);
                        tlp_registration.Controls.Add(tlp_pur_unit, 2, 6);
                        tlp_registration.Controls.Add(tlp_baseunit, 3, 6);

                        // Row 7: Base Unit and Conversion Factor labels.
                        tlp_registration.Controls.Add(lblConversionfactor, 0, 7);
                        tlp_registration.Controls.Add(lbltax, 1, 7);
                        tlp_registration.Controls.Add(lbligst, 2, 7);
                        tlp_registration.Controls.Add(tlp_s_c_gst, 3, 7);


                        // Row 8: Base Unit and Conversion Factor controls.
                        tlp_registration.Controls.Add(txtConversionFactor, 0, 8);
                        tlp_registration.Controls.Add(cmbTaxType, 1, 8);
                        tlp_registration.Controls.Add(cmbIgstPrcnt, 2, 8); 
                        tlp_registration.Controls.Add(tlp_S_C_text_, 3, 8);

                        // Row 9: Tax labels.
                        tlp_registration.Controls.Add(lblhsn, 0, 9);
                        tlp_registration.Controls.Add(chk_enableexpire, 1, 9);
                        tlp_registration.Controls.Add(lblreorderlevel, 2, 9);
       

                        // Row 10: Tax controls.
                        tlp_registration.Controls.Add(txthsncode, 0, 10);
                        tlp_registration.Controls.Add(cmb_exp_dates, 1, 10);
                        tlp_registration.Controls.Add(txtreorderlevel, 2, 10);
                        tlp_registration.Controls.Add(chklocksalerate, 3, 10);

                        // Row 11: Expiry, reorder and description labels.
                        tlp_registration.Controls.Add(lbldes, 0, 11);

                        // Row 12: Expiry, reorder and description controls.
                        tlp_registration.Controls.Add(txtdesc, 0, 12);
                        tlp_registration.Controls.Add(btn_del_ingre, 1, 12);

                    }
                    else
                    {
                        // Row 5: Unit, Tax, Tax Type and IGST labels.
                        tlp_registration.Controls.Add(lblunit, 0, 5);
                        tlp_registration.Controls.Add(lblbaseunit, 1, 5);
                        tlp_registration.Controls.Add(lblConversionfactor, 2, 5);
                        tlp_registration.Controls.Add(lbltax, 3, 5);

                        // Row 6: Unit, Tax, Tax Type and IGST controls.
                        tlp_registration.Controls.Add(tlp_pur_unit, 0, 6);
                        tlp_registration.Controls.Add(tlp_baseunit, 1, 6);
                        tlp_registration.Controls.Add(txtConversionFactor, 2, 6);
                        tlp_registration.Controls.Add(cmbtax, 3, 6);

                        // Row 7: Base Unit and Conversion Factor labels.
                        tlp_registration.Controls.Add(lbltaxtype, 0, 7);
                        tlp_registration.Controls.Add(lbligst, 1, 7);
                        tlp_registration.Controls.Add(tlp_s_c_gst, 2, 7);
                        tlp_registration.Controls.Add(lblhsn, 3, 7);

                        // Row 8: Base Unit and Conversion Factor controls.
                        tlp_registration.Controls.Add(cmbTaxType, 0, 8);
                        tlp_registration.Controls.Add(cmbIgstPrcnt, 1, 8);
                        tlp_registration.Controls.Add(tlp_S_C_text_, 2, 8);
                        tlp_registration.Controls.Add(txthsncode, 3, 8);

                        // Row 9: SGST/CGST and HSN labels.
                        tlp_registration.Controls.Add(chk_enableexpire, 0, 9);
                        tlp_registration.Controls.Add(lblreorderlevel, 1, 9);
                        tlp_registration.Controls.Add(lbldes, 3, 9);

                        // Row 10: SGST/CGST and HSN controls.
                        tlp_registration.Controls.Add(cmb_exp_dates, 0, 10);
                        tlp_registration.Controls.Add(txtreorderlevel, 1, 10);
                        tlp_registration.Controls.Add(chklocksalerate, 2, 10);
                        tlp_registration.Controls.Add(txtdesc, 3, 10);

                        // Row 11: Description label.


                        // Row 12: Description controls.
                        tlp_registration.Controls.Add(btn_del_ingre, 0, 12);

                        //if (txtitemid.Text != "" && Convert.ToInt32(txtitemid.Text) > 0)
                        //    tlp_registration.Controls.Add(tlp_des_rem, 1, 12);
                        //else
                        //    tlp_registration.Controls.Add(txtdesc, 1, 12);
                    }

                    cmbIgstPrcnt.Focus();
                }
                // =========================================================
                // VAT
                // =========================================================
                else if (cmbTaxType.Text == "VAT")
                {
                    lblvat.Dock = DockStyle.Fill;
                    cmbVatPrcnt.Dock = DockStyle.Fill;

                    if (rdb_dsale.Checked)
                    {
                        // Row 5: Discount, Unit and Tax labels.
                        tlp_registration.Controls.Add(lbldisper, 0, 5);
                        tlp_registration.Controls.Add(lbldisamt, 1, 5);
                        tlp_registration.Controls.Add(lblunit, 2, 5);
                        tlp_registration.Controls.Add(lblbaseunit, 3, 5);

                        // Row 6: Discount, Unit and Tax controls.
                        tlp_registration.Controls.Add(txtdisper, 0, 6);
                        tlp_registration.Controls.Add(txtdisamt, 1, 6);
                        tlp_registration.Controls.Add(tlp_pur_unit, 2, 6); 
                        tlp_registration.Controls.Add(tlp_baseunit, 3, 6);

                        // Row 7: Base Unit and Conversion Factor labels.
                        tlp_registration.Controls.Add(lblConversionfactor, 0, 7);
                        tlp_registration.Controls.Add(lbltax, 1, 7);
                        tlp_registration.Controls.Add(lbltaxtype, 2, 7);
                        tlp_registration.Controls.Add(lblvat, 3, 7);

                        // Row 8: Base Unit and Conversion Factor controls.
                        tlp_registration.Controls.Add(txtConversionFactor, 0, 8);
                        tlp_registration.Controls.Add(cmbtax, 1, 8);
                        tlp_registration.Controls.Add(cmbTaxType, 1, 8);
                        tlp_registration.Controls.Add(cmbVatPrcnt, 1, 8);

                        // Row 9: Tax labels.
                        tlp_registration.Controls.Add(lblhsn, 0, 9);
                        tlp_registration.Controls.Add(chk_enableexpire, 1, 9);
                        tlp_registration.Controls.Add(lblreorderlevel, 2, 9);


                        // Row 10: Tax controls.
                        tlp_registration.Controls.Add(txthsncode, 0, 10);
                        tlp_registration.Controls.Add(cmb_exp_dates, 1, 10);
                        tlp_registration.Controls.Add(txtreorderlevel, 2, 10);
                        tlp_registration.Controls.Add(chklocksalerate, 3, 10);

                        // Row 11: Reorder and description labels.
                        tlp_registration.Controls.Add(lbldes, 0, 11);

                        // Row 12: Reorder and description controls.
                        tlp_registration.Controls.Add(txtdesc, 0, 12);
                        tlp_registration.Controls.Add(btn_del_ingre, 1, 12);
                    }
                    else
                    {
                        // Row 5: Unit, Tax, Tax Type and VAT labels.
                        tlp_registration.Controls.Add(lblunit, 0, 5);
                        tlp_registration.Controls.Add(lblbaseunit, 1, 5);
                        tlp_registration.Controls.Add(lblConversionfactor, 2, 5);
                        tlp_registration.Controls.Add(lbltax, 3, 5);

                        // Row 6: Unit, Tax, Tax Type and VAT controls.
                        tlp_registration.Controls.Add(tlp_pur_unit, 0, 6);
                        tlp_registration.Controls.Add(tlp_baseunit, 1, 6);
                        tlp_registration.Controls.Add(txtConversionFactor, 2, 6);
                        tlp_registration.Controls.Add(cmbtax, 3, 6);

                        // Row 7: Base Unit and Conversion Factor labels.
                        tlp_registration.Controls.Add(lbltaxtype, 0, 7);
                        tlp_registration.Controls.Add(lblvat, 1, 7);
                        tlp_registration.Controls.Add(lblhsn, 2, 7);
                        tlp_registration.Controls.Add(chk_enableexpire, 3, 7);

                        // Row 8: Base Unit and Conversion Factor controls.
                        tlp_registration.Controls.Add(cmbTaxType, 0, 8);
                        tlp_registration.Controls.Add(cmbVatPrcnt, 1, 8);
                        tlp_registration.Controls.Add(txthsncode, 2, 8);
                        tlp_registration.Controls.Add(cmb_exp_dates, 3, 8);

                        // Row 9: HSN and stock labels.
                        tlp_registration.Controls.Add(lblreorderlevel, 0, 9);
                        tlp_registration.Controls.Add(lbldes, 2, 9);     

                        // Row 10: HSN and stock controls.
                        tlp_registration.Controls.Add(txtreorderlevel, 0, 10);
                        tlp_registration.Controls.Add(chklocksalerate, 1, 10);
                        tlp_registration.Controls.Add(txtdesc, 2, 10);
                        tlp_registration.Controls.Add(btn_del_ingre, 3, 10);

                        // Row 11: Description label.        

                        // Row 12: Description control.

                    }

                    cmbVatPrcnt.Focus();
                }

                tlp_registration.ResumeLayout(true);
                textb_shape();
            }
            catch (Exception ex)
            {
                if (tlp_registration != null)
                    tlp_registration.ResumeLayout(true);

                clsgen.LogException(
                    "Item page ControlArrangement()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Item));
            }
        }

        private void rdb_notsale_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdb_notsale.Checked)
                {
                    cmb_inc_categ.DataSource = null;
                    cmb_inc_categ.DataSource = gridSource_itemcate;
                    cmb_inc_categ.ValueMember = "Id";
                    cmb_inc_categ.DisplayMember = "Value";

                    int[] columnsToRemove1 = { 0, 1, 2, 3 };
                    int[] rowsToRemove1 = { 5, 6, 7, 8 };

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
                    cmbunit.Dock = DockStyle.Fill;

                    tlp_baseunit.Dock = DockStyle.Fill;
                    tlp_pur_unit.Dock = DockStyle.Fill;

                    ControlArrangement();

                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page rdb_notsale_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
            try
            {

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page rdb_dsale_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }

        }

        private void txtdisper_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtdisper.Text != "")
                {
                    if (Convert.ToDecimal(txtdisper.Text) > 100)
                    {
                        CMessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtdisamt.Text = "0.00";
                        txtdisper.Text = "0.00";
                        txtdisper.Focus();
                    }
                    else
                    {
                        txtdisamt.Text = "0.00";
                    }
                }
                discountAmnt();
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page txtdisper_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }


        }

        private void txtdisper_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void txtdisamt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtdisamt.Text != "" && txtmrp.Text != "" && Convert.ToDecimal(txtdisamt.Text) != 0 && Convert.ToDecimal(txtmrp.Text) != 0)
                {
                    if (Convert.ToDecimal(txtdisamt.Text) > Convert.ToDecimal(txtmrp.Text))
                    {
                        CMessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtdisamt.Text = "0.00";
                        txtdisper.Text = "0.00";
                        txtdisamt.Focus();
                    }
                    else
                    {
                        decimal discountPer = (Convert.ToDecimal(txtdisamt.Text) / Convert.ToDecimal(txtmrp.Text) * 100);
                        txtdisper.Text = discountPer.ToString("N2");
                    }
                }
                discountPer();
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page txtdisamt_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }

        }

        private void txtdisamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page txtdisamt_KeyPress()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }

        }

        public void discountAmnt()
        {
            try
            {
                if (txtdisper.Text != "")
                {
                    if (Convert.ToDecimal(txtdisper.Text) > 100)
                    {
                        CMessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtdisper.Focus();
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(txtsrate.Text))
                            txtsrate.Text = "0";
                        decimal discountAmnt = Convert.ToDecimal(txtdisper.Text.Trim()) * Convert.ToDecimal(txtsrate.Text) / 100;
                        txtdisamt.Text = discountAmnt.ToString("N2");
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page discountAmnt()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }
        public void discountPer()
        {

            try
            {

                if (txtdisamt.Text != "" && txtsrate.Text != "" && Convert.ToDecimal(txtsrate.Text) != 0)
                {
                    if (Convert.ToDecimal(txtdisamt.Text) > Convert.ToDecimal(txtsrate.Text))
                    {
                        CMessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtdisamt.Focus();
                    }
                    else
                    {
                        decimal discountPer = (Convert.ToDecimal(txtdisamt.Text) / Convert.ToDecimal(txtsrate.Text) * 100);
                        txtdisper.Text = discountPer.ToString("N2");
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page discountPer()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }

        }

        private void rb_product_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rb_notsale.Checked)
                {
                    Direct_nosale_product(0);
                    Fillcombo(2);
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page rb_product_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }

        }
        public void Fillcombo(int cattype)
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Fcid,CategoryName";
                objSearch.Table = " tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where Status=1 and CategoryType=" + cattype;
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmbMenu_category.DataSource = gridSource;
                cmbMenu_category.ValueMember = "Id";
                cmbMenu_category.DisplayMember = "Value";
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page Fillcombo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }
        private void cmbMenu_category_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "i.ItemId Field1,i.Itemname Field2,u.UnitName Field3," +
                    "(Select sum(QOH) from tblProductRate where ProductId=i.ItemId) as Field4," +
                    "(Select Top(1)Prate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field5," +
                    "(Select Top(1)Srate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field6," +
                    "(Select Top(1)MRP from tblProductRate where ProductId=i.ItemId order by Id desc) as Field7," +
                    "i.Tax Field8,ts.TaxType Field9,ts.IgstPrcnt Field10,ts.VatPrcnt Field11,i.summary Field12,i.Status Field13,u.UId Field114,i.Fcid Field15,c.CategoryName Field16,i.HSNcode Field17,i.ReorderLevel Field18,i.Isexpire Field19,i.LockSaleRate Field20,i.Baseunit Field21,i.Conversionfactor Field22";
                dmnSearch.Table = "tblItem i " +
                    " LEFT JOIN tblUnit u on u.UId=i.unit " +
                    " LEFT JOIN tblTaxSettings ts on ts.TaxSettingsId=i.TaxSettingsId" +
                    " LEFT JOIN tblCategory_FTI c on c.Fcid=i.Fcid";

                if (rb_notsale.Checked)
                    dmnSearch.WhereNdGroupBy = "where i.Status=1 and i.saletype=0 and i.fcid =" + cmbMenu_category.SelectedValue;
                else if (rb_item.Checked)
                    dmnSearch.WhereNdGroupBy = "where i.Status=1and i.saletype=1 and i.fcid =" + cmbMenu_category.SelectedValue;

                dmnSearch.OrderBy = "order by i.ItemId desc ";
                var itemSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                dgv_item.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_item.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_item.Rows[i].Cells[slno].Value = i + 1;
                        dgv_item.Rows[i].Cells[idno].Value = itemSource[i].Field1;
                        dgv_item.Rows[i].Cells[incre_nameno].Value = itemSource[i].Field2;
                        dgv_item.Rows[i].Cells[incre_categno].Value = itemSource[i].Field16;
                        dgv_item.Rows[i].Cells[iunitno].Value = itemSource[i].Field3;
                        dgv_item.Rows[i].Cells[iqtyno].Value = itemSource[i].Field4;
                        dgv_item.Rows[i].Cells[iprateno].Value = itemSource[i].Field5;
                        dgv_item.Rows[i].Cells[israteno].Value = itemSource[i].Field6;
                        dgv_item.Rows[i].Cells[imrpno].Value = itemSource[i].Field7;
                        dgv_item.Rows[i].Cells[itaxno].Value = itemSource[i].Field8;
                        dgv_item.Rows[i].Cells[itaxtypeno].Value = itemSource[i].Field9;
                        if (itemSource[i].Field9 == "GST")
                            dgv_item.Rows[i].Cells[itaxperno].Value = itemSource[i].Field10;
                        else
                            dgv_item.Rows[i].Cells[itaxperno].Value = itemSource[i].Field11;
                        dgv_item.Rows[i].Cells[idescno].Value = itemSource[i].Field12;
                        dgv_item.Rows[i].Cells[istatusno].Value = Convert.ToInt32(itemSource[i].Field13);
                        dgv_item.Rows[i].Cells[iunitidno].Value = itemSource[i].Field14;
                        dgv_item.Rows[i].Cells[incre_categ_idno].Value = itemSource[i].Field15;
                        dgv_item.Rows[i].Cells[ihsncodeno].Value = itemSource[i].Field17;
                        dgv_item.Rows[i].Cells[iReorderLevelno].Value = itemSource[i].Field18;
                        dgv_item.Rows[i].Cells[iIsexpireno].Value = itemSource[i].Field19;
                        dgv_item.Rows[i].Cells[iLockSaleRateno].Value = itemSource[i].Field20;
                        dgv_item.Rows[i].Cells[iBaseunitno].Value = itemSource[i].Field21;
                        dgv_item.Rows[i].Cells[iConversionfactorno].Value = itemSource[i].Field22;
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page cmbMenu_category_SelectionChangeCommitted()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }

        private void btn_refresh_list_Click(object sender, EventArgs e)
        {
            try
            {
                if (rb_notsale.Checked)
                    Direct_nosale_product(0);
                else if (rb_item.Checked)
                    Direct_nosale_product(1);
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page btn_refresh_list_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }

        public void Direct_nosale_product(int typeno)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "i.ItemId Field1,i.Itemname Field2,u.UnitName Field3," +
                    "(Select sum(QOH) from tblProductRate where ProductId=i.ItemId) as Field4," +
                    "(Select Top(1)Prate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field5," +
                    "(Select Top(1)Srate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field6," +
                    "(Select Top(1)MRP from tblProductRate where ProductId=i.ItemId order by Id desc) as Field7," +
                    "i.Tax Field8,ts.TaxType Field9,ts.IgstPrcnt Field10,ts.VatPrcnt Field11,i.summary Field12,i.Status Field13,u.UId Field114,i.Fcid Field15,c.CategoryName Field16,i.HSNcode Field17,i.ReorderLevel Field18,i.Isexpire Field19,i.LockSaleRate Field20,i.Baseunit Field21,i.Conversionfactor Field22";
                dmnSearch.Table = "tblItem i " +
                    " LEFT JOIN tblUnit u on u.UId=i.unit " +
                    " LEFT JOIN tblTaxSettings ts on ts.TaxSettingsId=i.TaxSettingsId" +
                    " LEFT JOIN tblCategory_FTI c on c.Fcid=i.Fcid";
                if (typeno == 1)
                    dmnSearch.WhereNdGroupBy = "where i.Status=1 and i.saletype=1";
                else
                    dmnSearch.WhereNdGroupBy = "where i.Status=1and i.saletype=0";

                dmnSearch.OrderBy = "order by i.ItemId desc ";
                var itemSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                dgv_item.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_item.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_item.Rows[i].Cells[slno].Value = i + 1;
                        dgv_item.Rows[i].Cells[idno].Value = itemSource[i].Field1;
                        dgv_item.Rows[i].Cells[incre_nameno].Value = itemSource[i].Field2;
                        dgv_item.Rows[i].Cells[incre_categno].Value = itemSource[i].Field16;
                        dgv_item.Rows[i].Cells[iunitno].Value = itemSource[i].Field3;
                        dgv_item.Rows[i].Cells[iqtyno].Value = itemSource[i].Field4;
                        dgv_item.Rows[i].Cells[iprateno].Value = itemSource[i].Field5;
                        dgv_item.Rows[i].Cells[israteno].Value = itemSource[i].Field6;
                        dgv_item.Rows[i].Cells[imrpno].Value = itemSource[i].Field7;
                        dgv_item.Rows[i].Cells[itaxno].Value = itemSource[i].Field8;
                        dgv_item.Rows[i].Cells[itaxtypeno].Value = itemSource[i].Field9;
                        if (itemSource[i].Field9 == "GST")
                            dgv_item.Rows[i].Cells[itaxperno].Value = itemSource[i].Field10;
                        else
                            dgv_item.Rows[i].Cells[itaxperno].Value = itemSource[i].Field11;
                        dgv_item.Rows[i].Cells[idescno].Value = itemSource[i].Field12;
                        dgv_item.Rows[i].Cells[istatusno].Value = Convert.ToInt32(itemSource[i].Field13);
                        dgv_item.Rows[i].Cells[iunitidno].Value = itemSource[i].Field14;
                        dgv_item.Rows[i].Cells[incre_categ_idno].Value = itemSource[i].Field15;
                        dgv_item.Rows[i].Cells[ihsncodeno].Value = itemSource[i].Field17;
                        dgv_item.Rows[i].Cells[iReorderLevelno].Value = itemSource[i].Field18;
                        dgv_item.Rows[i].Cells[iIsexpireno].Value = itemSource[i].Field19;
                        dgv_item.Rows[i].Cells[iLockSaleRateno].Value = itemSource[i].Field20;
                        dgv_item.Rows[i].Cells[iBaseunitno].Value = itemSource[i].Field21;
                        dgv_item.Rows[i].Cells[iConversionfactorno].Value = itemSource[i].Field22;
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page Direct_nosale_product()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));

            }
        }

        private void rb_item_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rb_item.Checked)
                {
                    Fillcombo(0);
                    Direct_nosale_product(1);
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page rb_item_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }

        private void txtitemcode_barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }
        public void serachbyname()
        {
            try
            {
                if (txtitemcode_barcode.Text != null && txtitemcode_barcode.Text != "")
                {
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "i.ItemId Field1,i.Itemname Field2,u.UnitName Field3," +
                        "(Select sum(QOH) from tblProductRate where ProductId=i.ItemId) as Field4," +
                        "(Select Top(1)Prate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field5," +
                        "(Select Top(1)Srate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field6," +
                        "(Select Top(1)MRP from tblProductRate where ProductId=i.ItemId order by Id desc) as Field7," +
                        "i.Tax Field8,ts.TaxType Field9,ts.IgstPrcnt Field10,ts.VatPrcnt Field11,i.summary Field12,i.Status Field13,u.UId Field114,i.Fcid Field15,c.CategoryName Field16,i.HSNcode Field17,i.ReorderLevel Field18,i.Isexpire Field19,i.LockSaleRate Field20,i.Baseunit Field21,i.Conversionfactor Field22";
                    dmnSearch.Table = "tblItem i " +
                        " LEFT JOIN tblUnit u on u.UId=i.unit " +
                        " LEFT JOIN tblTaxSettings ts on ts.TaxSettingsId=i.TaxSettingsId" +
                        " LEFT JOIN tblCategory_FTI c on c.Fcid=i.Fcid";

                    if (rb_notsale.Checked)
                        dmnSearch.WhereNdGroupBy = "where i.Status=1 and i.saletype=0 and ( i.ItemCode_UPC = '" + txtitemcode_barcode.Text + "' or i.itemname = '" + txtitemcode_barcode.Text + "' )";
                    else if (rb_item.Checked)
                        dmnSearch.WhereNdGroupBy = "where i.Status=1and i.saletype=1 and ( i.ItemCode_UPC = '" + txtitemcode_barcode.Text + "' or i.itemname = '" + txtitemcode_barcode.Text + "' )";

                    dmnSearch.OrderBy = "order by i.ItemId desc ";
                    var itemSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    dgv_item.Rows.Clear();
                    if (itemSource.Count > 0)
                    {
                        dgv_item.Rows.Add(itemSource.Count);
                        for (int i = 0; i < itemSource.Count; i++)
                        {
                            dgv_item.Rows[i].Cells[slno].Value = i + 1;
                            dgv_item.Rows[i].Cells[idno].Value = itemSource[i].Field1;
                            dgv_item.Rows[i].Cells[incre_nameno].Value = itemSource[i].Field2;
                            dgv_item.Rows[i].Cells[incre_categno].Value = itemSource[i].Field16;
                            dgv_item.Rows[i].Cells[iunitno].Value = itemSource[i].Field3;
                            dgv_item.Rows[i].Cells[iqtyno].Value = itemSource[i].Field4;
                            dgv_item.Rows[i].Cells[iprateno].Value = itemSource[i].Field5;
                            dgv_item.Rows[i].Cells[israteno].Value = itemSource[i].Field6;
                            dgv_item.Rows[i].Cells[imrpno].Value = itemSource[i].Field7;
                            dgv_item.Rows[i].Cells[itaxno].Value = itemSource[i].Field8;
                            dgv_item.Rows[i].Cells[itaxtypeno].Value = itemSource[i].Field9;
                            if (itemSource[i].Field9 == "GST")
                                dgv_item.Rows[i].Cells[itaxperno].Value = itemSource[i].Field10;
                            else
                                dgv_item.Rows[i].Cells[itaxperno].Value = itemSource[i].Field11;
                            dgv_item.Rows[i].Cells[idescno].Value = itemSource[i].Field12;
                            dgv_item.Rows[i].Cells[istatusno].Value = Convert.ToInt32(itemSource[i].Field13);
                            dgv_item.Rows[i].Cells[iunitidno].Value = itemSource[i].Field14;
                            dgv_item.Rows[i].Cells[incre_categ_idno].Value = itemSource[i].Field15;
                        dgv_item.Rows[i].Cells[ihsncodeno].Value = itemSource[i].Field17;
                        dgv_item.Rows[i].Cells[iReorderLevelno].Value = itemSource[i].Field18;
                        dgv_item.Rows[i].Cells[iIsexpireno].Value = itemSource[i].Field19;
                        dgv_item.Rows[i].Cells[iLockSaleRateno].Value = itemSource[i].Field20;
                        dgv_item.Rows[i].Cells[iBaseunitno].Value = itemSource[i].Field21;
                        dgv_item.Rows[i].Cells[iConversionfactorno].Value = itemSource[i].Field22;
                        }
                        dgv_item.Focus();
                    }
                    else
                    {
                        CMessageBox.Show("No match Found !", "Information", MessageBoxButtons.OK);
                        txtitemcode_barcode.Text = "";
                        txtitemcode_barcode.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page serachbyname()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));

            }
        }

        private void txtincr_name_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtincr_name.Text != "" && (txtitemid.Text == "" || Convert.ToInt32(txtitemid.Text) == 0))
                {
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "ProductID Field1 ";
                    objSearch.Table = "tblProduct WHERE Status = 1 AND Product = '" + txtincr_name.Text + "'  UNION SELECT ItemId Field1 FROM tblItem ";
                    objSearch.WhereNdGroupBy = "Where status=1 and Itemname = '" + txtincr_name.Text + "'";
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        CMessageBox.Show(txtincr_name.Text + " Name Already Exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtincr_name.Text = "";
                        txtincr_name.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Item page txtincr_name_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }

        private void FrmItemRegistartion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                AppSession.UnregisterForm(this);
            }
        }

        private void chk_enableexpire_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_enableexpire.Checked)
            {
                cmb_exp_dates.Enabled = true;
            }
            else
            {
                cmb_exp_dates.Enabled = false;
            }
        }


        private void txtConversionFactor_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys such as Backspace and Delete.
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Allow numbers.
            if (char.IsDigit(e.KeyChar))
            {
                return;
            }

            // Allow only one decimal point.
            if (e.KeyChar == '.' && !txtConversionFactor.Text.Contains("."))
            {
                return;
            }

            // Block alphabets and other special characters.
            e.Handled = true;
        }

        private void txtreorderlevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys such as Backspace and Delete.
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Allow integer digits only.
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #region Category Registration
        private void btnsave_categ_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtfoodCate.Text))
                {
                    CMessageBox.Show("Please enter category name.", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtfoodCate.Focus();
                    return;
                }

                BllSearch<DmnCategory> search = new BllSearch<DmnCategory>();
                DmnSearch query = new DmnSearch();
                query.Select = "Fcid,CategoryName";
                query.Table = "tblCategory_FTI";
                query.WhereNdGroupBy = "WHERE Status=1 AND CategoryType=" + cmb_cate.SelectedIndex +
                    " AND CategoryName='" + txtfoodCate.Text.Replace("'", "''") + "' AND Fcid<>" +
                    (string.IsNullOrWhiteSpace(txtFcid.Text) ? "0" : txtFcid.Text);
                query.OrderBy = "";
                if (search.GetSearchData("prdSearch", query).Count > 0)
                {
                    CMessageBox.Show("This category already exists.", "Duplicate Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DmnCategory category = new DmnCategory();
                category.Fcid = string.IsNullOrWhiteSpace(txtFcid.Text) ? 0 : Convert.ToInt32(txtFcid.Text);
                category.CategoryName = txtfoodCate.Text.Trim();
                category.CategoryType = cmb_cate.SelectedIndex;
                category.Description = txtdescr.Text;
                category.CreatedBy = FrmDashboard.UserId;
                category.BranchId = FrmDashboard.BranchId;
                category.Status = chk_active.Checked ? 1 : 0;

                BllCategory bll = new BllCategory();
                var result = bll.AddeditData("prdInsert_upd_Category", category);
                if (result.Fcid > 0)
                {
                    CMessageBox.Show(category.Fcid > 0 ? "Category updated successfully." : "Category saved successfully.");
                    ClearCategoryFields();
                    FillEmbeddedCategories();
                    combofill();
                }
                else
                    CMessageBox.Show("Unable to save category.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Item page btnsave_categ_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }

        private void btn_refresh_categ_Click(object sender, EventArgs e)
        {
            try
            {
            ClearCategoryFields();
            FillEmbeddedCategories();
        
            } catch (Exception ex)
            {
                clsgen.LogException("Item page btn_refresh_categ_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            } 
        }

        private void btnserach_categ_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_category.CurrentRow != null && dgv_category.CurrentRow.Index >= 0)
                    LoadEmbeddedCategory(Convert.ToInt32(dgv_category.CurrentRow.Cells[1].Value));
                else
                    dgv_category.Focus();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Item page btnserach_categ_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }

        private void btnclose_categ_Click(object sender, EventArgs e)
        {
            try
            {
            tlp_category.Visible = false;
        
            } catch (Exception ex)
            {
                clsgen.LogException("Item page btnclose_categ_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            } 
        }

        private void btn_del_cate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFcid.Text)) return;
                if (CMessageBox.Show("Do you want to deactivate this category?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

                DmnCategory category = new DmnCategory();
                category.Fcid = Convert.ToInt32(txtFcid.Text);
                category.CategoryName = txtfoodCate.Text;
                category.CategoryType = cmb_cate.SelectedIndex;
                category.Description = txtdescr.Text;
                category.CreatedBy = FrmDashboard.UserId;
                category.BranchId = FrmDashboard.BranchId;
                category.Status = 0;
                new BllCategory().AddeditData("prdInsert_upd_Category", category);

                ClearCategoryFields();
                FillEmbeddedCategories();
                combofill();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Item page btn_del_cate_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }

        private void dgv_category_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_category.Rows.Count) return;
            try { LoadEmbeddedCategory(Convert.ToInt32(dgv_category.Rows[e.RowIndex].Cells[1].Value)); }
            catch (Exception ex) { clsgen.LogException("Item page dgv_category_CellDoubleClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item)); }
        }

        private void btnaddcategory_Click(object sender, EventArgs e)
        {
            try
            {
            ShowRegistrationPanel(tlp_category);
            tlp_Unit.Visible = false;
            ClearCategoryFields();
            FillEmbeddedCategories();
        
            } catch (Exception ex)
            {
                clsgen.LogException("Item page btnaddcategory_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            } 
        }

        private void FillEmbeddedCategories()
        {
            try
            {
            BllSearch<DmnCategory> bll = new BllSearch<DmnCategory>();
            DmnSearch search = new DmnSearch();
            search.Select = "Fcid,CategoryName,Description,Status";
            search.Table = "tblCategory_FTI";
            search.WhereNdGroupBy = "WHERE Status=1 AND CategoryType=" + Math.Max(0, cmb_cate.SelectedIndex);
            search.OrderBy = "ORDER BY CategoryName";
            var rows = bll.GetSearchData("prdSearch", search);
            dgv_category.Rows.Clear();
            if (rows.Count == 0) return;
            dgv_category.Rows.Add(rows.Count);
            for (int i = 0; i < rows.Count; i++)
            {
                dgv_category.Rows[i].Cells[0].Value = i + 1;
                dgv_category.Rows[i].Cells[cidno].Value = rows[i].Fcid;
                dgv_category.Rows[i].Cells[2].Value = rows[i].CategoryName;
                dgv_category.Rows[i].Cells[3].Value = rows[i].Description;
                dgv_category.Rows[i].Cells[4].Value = rows[i].Status;
            }
        
            } catch (Exception ex)
            {
                clsgen.LogException("Item page FillEmbeddedCategories()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            } 
        }

        private void LoadEmbeddedCategory(int categoryId)
        {
            try
            {
            BllSearch<DmnCategory> bll = new BllSearch<DmnCategory>();
            DmnSearch search = new DmnSearch();
            search.Select = "*";
            search.Table = "tblCategory_FTI";
            search.WhereNdGroupBy = "WHERE Fcid=" + categoryId;
            search.OrderBy = "";
            var rows = bll.GetSearchData("prdSearch", search);
            if (rows.Count == 0) return;
            txtFcid.Text = Convert.ToString(rows[0].Fcid);
            txtfoodCate.Text = rows[0].CategoryName;
            cmb_cate.SelectedIndex = rows[0].CategoryType;
            txtdescr.Text = rows[0].Description;
            chk_active.Checked = rows[0].Status == 1;
            btn_del_cate.Visible = true;
        
            } catch (Exception ex)
            {
                clsgen.LogException("Item page LoadEmbeddedCategory()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            } 
        }

        private void ClearCategoryFields()
        {
            try
            {
            txtFcid.Text = "0";
            txtfoodCate.Clear();
            txtdescr.Clear();
            chk_active.Checked = true;
            btn_del_cate.Visible = false;
        
            } catch (Exception ex)
            {
                clsgen.LogException("Item page ClearCategoryFields()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            } 
        }
        #endregion

        #region Unit Registration
        private void btn_del_unit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUid.Text)) return;
                if (CMessageBox.Show("Do you want to deactivate this unit?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

                DmnUnit unit = new DmnUnit();
                unit.UId = Convert.ToInt32(txtUid.Text);
                unit.UnitName = txtunit.Text;
                unit.Description = txtdescr.Text;
                unit.CreatedBy = FrmDashboard.UserId;
                unit.BranchId = FrmDashboard.BranchId;
                unit.Status = 0;
                new BllUnit().AddeditData("prdInsert_upd_Unit", unit);
                ClearUnitFields();
                FillEmbeddedUnits();
                combofill();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Item page btn_del_unit_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }

        private void btnsave_unit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtunit.Text))
                {
                    CMessageBox.Show("Please enter unit name.", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtunit.Focus();
                    return;
                }

                BllSearch<DmnUnit> search = new BllSearch<DmnUnit>();
                DmnSearch query = new DmnSearch();
                query.Select = "UId,UnitName";
                query.Table = "tblUnit";
                query.WhereNdGroupBy = "WHERE Status=1 AND UnitName='" + txtunit.Text.Replace("'", "''") +
                    "' AND UId<>" + (string.IsNullOrWhiteSpace(txtUid.Text) ? "0" : txtUid.Text);
                query.OrderBy = "";
                if (search.GetSearchData("prdSearch", query).Count > 0)
                {
                    CMessageBox.Show("This unit already exists.", "Duplicate Unit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DmnUnit unit = new DmnUnit();
                unit.UId = string.IsNullOrWhiteSpace(txtUid.Text) ? 0 : Convert.ToInt32(txtUid.Text);
                unit.UnitName = txtunit.Text.Trim();
                unit.Description = txtdescr.Text;
                unit.CreatedBy = FrmDashboard.UserId;
                unit.BranchId = FrmDashboard.BranchId;
                unit.Status = chk_active.Checked ? 1 : 0;

                var result = new BllUnit().AddeditData("prdInsert_upd_Unit", unit);
                if (result.UId > 0)
                {
                    CMessageBox.Show(unit.UId > 0 ? "Unit updated successfully." : "Unit saved successfully.");
                    ClearUnitFields();
                    FillEmbeddedUnits();
                    combofill();
                }
                else
                    CMessageBox.Show("Unable to save unit.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Item page btnsave_unit_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }

        private void btnsearch_unit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_unit.CurrentRow != null && dgv_unit.CurrentRow.Index >= 0)
                    LoadEmbeddedUnit(Convert.ToInt32(dgv_unit.CurrentRow.Cells[1].Value));
                else
                    dgv_unit.Focus();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Item page btnsearch_unit_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            }
        }

        private void btnrefresh_unit_Click(object sender, EventArgs e)
        {
            try
            {
            ClearUnitFields();
            FillEmbeddedUnits();
        
            } catch (Exception ex)
            {
                clsgen.LogException("Item page btnrefresh_unit_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            } 
        }

        private void btncancel_unit_Click(object sender, EventArgs e)
        {
            try
            {
            tlp_Unit.Visible = false;
        
            } catch (Exception ex)
            {
                clsgen.LogException("Item page btncancel_unit_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            } 
        }

        private void dgv_unit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_unit.Rows.Count) return;
            try { LoadEmbeddedUnit(Convert.ToInt32(dgv_unit.Rows[e.RowIndex].Cells[1].Value)); }
            catch (Exception ex) { clsgen.LogException("Item page dgv_unit_CellDoubleClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item)); }
        }

        private void btn_addunit_Click(object sender, EventArgs e)
        {
            try
            {
            ShowRegistrationPanel(tlp_Unit);
            tlp_category.Visible = false;
            ClearUnitFields();
            FillEmbeddedUnits();
        
            } catch (Exception ex)
            {
                clsgen.LogException("Item page btn_addunit_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            } 
        }

        private void FillEmbeddedUnits()
        {
            try
            {
            BllSearch<DmnUnit> bll = new BllSearch<DmnUnit>();
            DmnSearch search = new DmnSearch();
            search.Select = "UId,UnitName,Description,Status";
            search.Table = "tblUnit";
            search.WhereNdGroupBy = "WHERE Status=1";
            search.OrderBy = "ORDER BY UnitName";
            var rows = bll.GetSearchData("prdSearch", search);
            dgv_unit.Rows.Clear();
            if (rows.Count == 0) return;
            dgv_unit.Rows.Add(rows.Count);
            for (int i = 0; i < rows.Count; i++)
            {
                dgv_unit.Rows[i].Cells[slno].Value = i + 1;
                dgv_unit.Rows[i].Cells[idno].Value = rows[i].UId;
                dgv_unit.Rows[i].Cells[2].Value = rows[i].UnitName;
                dgv_unit.Rows[i].Cells[3].Value = rows[i].Description;
                dgv_unit.Rows[i].Cells[4].Value = rows[i].Status;
            }
        
            } catch (Exception ex)
            {
                clsgen.LogException("Item page FillEmbeddedUnits()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            } 
        }

        private void LoadEmbeddedUnit(int unitId)
        {
            try
            {
            BllSearch<DmnUnit> bll = new BllSearch<DmnUnit>();
            DmnSearch search = new DmnSearch();
            search.Select = "*";
            search.Table = "tblUnit";
            search.WhereNdGroupBy = "WHERE UId=" + unitId;
            search.OrderBy = "";
            var rows = bll.GetSearchData("prdSearch", search);
            if (rows.Count == 0) return;
            txtUid.Text = Convert.ToString(rows[0].UId);
            txtunit.Text = rows[0].UnitName;
            txtdescr.Text = rows[0].Description;
            chk_active.Checked = rows[0].Status == 1;
            btn_del_unit.Visible = true;
        
            } catch (Exception ex)
            {
                clsgen.LogException("Item page LoadEmbeddedUnit()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            } 
        }

        private void ClearUnitFields()
        {
            try
            {
            txtUid.Text = "0";
            txtunit.Clear();
            txtdescr.Clear();
            chk_active.Checked = true;
            btn_del_unit.Visible = false;
        
            } catch (Exception ex)
            {
                clsgen.LogException("Item page ClearUnitFields()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            } 
        }
        #endregion

        private void ShowRegistrationPanel(TableLayoutPanel panel)
        {
            try
            {
            panel.Visible = true;
            panel.BringToFront();
            panel.Location = new Point(
                Math.Max(0, (ClientSize.Width - panel.Width) / 2),
                Math.Max(0, (ClientSize.Height - panel.Height) / 2));
            panel.Anchor = AnchorStyles.None;
        
            } catch (Exception ex)
            {
                clsgen.LogException("Item page ShowRegistrationPanel()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Item));
            } 
        }

       
    }

}
