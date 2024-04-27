using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using FoodandBeverage.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace FoodandBeverage.Forms
{
    public partial class FrmIngredient : Form
    {
        public FrmIngredient()
        {
            InitializeComponent();
        }
        #region Grid Declaration
        public static int Rslno = 0;
        public static int Ritemno = 1;
        public static int Rqtyno = 2;
        public static int Runitno = 3;
        public static int Raction = 4;
        public static int RIng_DetailsIdno = 5;
        public static int Ritemidno = 6;

        public static int Arowno = 0;
        public static int avariationno = 1;
        public static int apriceno = 2;
        public static int Acodeno = 3;
        public static int Avidno = 4;
        public static int Apidno = 5;


        #endregion
        public string Compressedimg = Application.StartupPath + "\\CompressedImg";
        ClsDocument clsDocument = new ClsDocument();
        public static string empImgExt = "";
        TableLayoutPanel tlp_Gallery_Main = new TableLayoutPanel();
        TableLayoutPanel tlp_Gallery_Box = new TableLayoutPanel();
        TableLayoutPanel tlp_gallery_sub = new TableLayoutPanel();
        ToolTip toolTip_gallery = new ToolTip();
        private bool comboBoxChanged = false;
        private Color originalBackColor; // Variable to store the original background color
        public int rowindex=1;
        List<string> TextBoxList = new List<string>();
        List<DmnComboBox> product_variation = new List<DmnComboBox>();
        List<DmnComboBox> Addon_List_Data = new List<DmnComboBox>();
        FlowLayoutPanel flp_addonlist = new FlowLayoutPanel();
        public int Prev_rowindex = 0;
        public List<CAddonlist> Checked_Addon_List = new List<CAddonlist>();
        private Timer blinkTimer;
        public int Mandatory_Rowindex = 0;
        private bool SerachFlag = false;
        public class CAddonlist
        {
            public int Id { get; set; }
            public int Pavid { get; set; }
            public string Cnames { get; set; }
            public int Status { get; set; }
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck() && MandatoryCheck_variation())
                {
                    Dmnincrediant status = new Dmnincrediant();
                    DmnProducts prostatus = new DmnProducts();
                    Dmnincrediant dmnincr = new Dmnincrediant();
                    Bllincrediant bllincr = new Bllincrediant();

                    DmnProducts dmnproduct = new DmnProducts();
                    BllProducts bllProduct = new BllProducts();

                    dmnproduct.ProductId = Convert.ToInt32(txtpid.Text);
                    dmnproduct.Product = Convert.ToString(txtproductname.Text);
                    dmnproduct.Price = Convert.ToDecimal(txtprice.Text);
                    dmnproduct.Tax = Convert.ToString(cmbtax.Text);
                    if (Convert.ToInt32(cmbTaxType.SelectedIndex) == 0)
                        dmnproduct.TaxSettingsId = Convert.ToInt32(cmbIgstPrcnt.SelectedValue);
                    else
                        dmnproduct.TaxSettingsId = Convert.ToInt32(cmbVatPrcnt.SelectedValue);

                    dmnproduct.Fcid = Convert.ToInt32(CmbCategory.SelectedValue);
                    dmnproduct.DiscountAmt = Convert.ToDecimal(txtDiscAmnt.Text);
                    dmnproduct.DiscountPer = Convert.ToDecimal(txtDisPer.Text);
                    dmnproduct.OfferDescription = "";
                    dmnproduct.MRP = Convert.ToDecimal(txtmrp.Text);
                    dmnproduct.Description = Convert.ToString(txtdes.Text);
                    dmnproduct.TaxType = Convert.ToString(cmbTaxType.Text);
                    //dmnproduct.Quantity = Convert.ToInt32(txtqty.Text);
                    dmnproduct.Description = Convert.ToString(txtdes.Text);
                    dmnproduct.ItemCode = Convert.ToString(txtitemcode.Text);
                    dmnproduct.UPC = "";
                    dmnproduct.CreatedBy = FrmDashboard.UserId;
                    dmnproduct.BranchId = FrmDashboard.BranchId;

                    if (chb_active.Checked)
                        dmnproduct.Status = 1;
                    else
                        dmnproduct.Status = 0;
                    prostatus = bllProduct.AddEditData("prdInsert_Upd_Product", dmnproduct);
                    int statuscount = 0,gridcount=0,varia_count=0;
                    if (prostatus.ProductId > 0)
                    {
                        #region Ingrediant Details Inseration DB
                        for (int i = 0; i < dgv_registartion.RowCount; i++)
                        {
                            if (((dgv_registartion.Rows[i].Cells[Ritemidno].Value) != null && (dgv_registartion.Rows[i].Cells[Ritemidno].Value) != "") &&
                                ((dgv_registartion.Rows[i].Cells[Rqtyno].Value) != "" && (Convert.ToDecimal(dgv_registartion.Rows[i].Cells[Rqtyno].Value)) > 0)
                                )
                            {
                                gridcount++;
                                dmnincr.ProductId = Convert.ToInt32(prostatus.ProductId);
                                dmnincr.Ing_DetailsId = Convert.ToInt32(dgv_registartion.Rows[i].Cells[RIng_DetailsIdno].Value);
                                dmnincr.ItemId = Convert.ToInt32(dgv_registartion.Rows[i].Cells[Ritemidno].Value);
                                dmnincr.quantity = Convert.ToDecimal(dgv_registartion.Rows[i].Cells[Rqtyno].Value);
                                dmnincr.unit = Convert.ToInt32(dgv_registartion.Rows[i].Cells[Runitno].Value);
                                // dmnincr.Mid = Convert.ToInt32(cmb_menu.SelectedValue);
                                dmnincr.CreatedBy = FrmDashboard.UserId;
                                dmnincr.BranchId = FrmDashboard.BranchId;

                                if (chb_active.Checked)
                                    dmnincr.Status = 1;
                                else
                                    dmnincr.Status = 0;
                                status = bllincr.AddData("prdInsert_upd_Incrediant", dmnincr);
                                if (status.Ing_DetailsId > 0)
                                    statuscount++;
                            }

                        }
                        #endregion

                        #region Variation Table Inseration DB
                        if(chk_variation.Checked)
                        {
                            for (int i = 0; i < dgv_variation.RowCount; i++)
                            {
                                if (dgv_variation.Rows[i].Cells[Arowno].Value != null && Convert.ToInt32(dgv_variation.Rows[i].Cells[Arowno].Value) > 0)
                                {
                                    DmnAddonVariation dmnvariation = new DmnAddonVariation();
                                    BllVariation blladdon = new BllVariation();
                                    dmnvariation.Pavid = Convert.ToInt32(dgv_variation.Rows[i].Cells[Avidno].Value);
                                    dmnvariation.Caption = Convert.ToString(dgv_variation.Rows[i].Cells[avariationno].Value);
                                    dmnvariation.VariationCode = Convert.ToString(dgv_variation.Rows[i].Cells[Acodeno].Value);
                                    dmnvariation.Aid = 0;
                                    dmnvariation.Type = Convert.ToInt32(Product_Addon_Variation_Type.Variation);
                                    dmnvariation.Price = Convert.ToDecimal(dgv_variation.Rows[i].Cells[apriceno].Value);
                                    dmnvariation.ProductId = Convert.ToInt32(prostatus.ProductId);
                                    dmnvariation.ItemCode_UPC = Convert.ToString(txtitemcode.Text);
                                    dmnvariation.CreatedBy = FrmDashboard.UserId;
                                    dmnvariation.BranchId = FrmDashboard.BranchId;
                                    dmnvariation.Status = 1;
                                    var usertype = blladdon.AddData("prdInsert_upd_AddonVariation", dmnvariation);
                                    if (usertype.Pavid > 0)
                                    {
                                        varia_count = varia_count + 1;
                                    }

                                }
                            }
                        }
                        #endregion

                        #region Product Mapping Addon Table Inseration DB
                        if (chk_addons.Checked)
                        {
                            foreach (CAddonlist Listdata in Checked_Addon_List)
                            {
                                DmnAddonVariation dmnvariation = new DmnAddonVariation();
                                BllVariation blladdon = new BllVariation();
                                dmnvariation.Pavid = Listdata.Pavid;
                                dmnvariation.Caption = Listdata.Cnames;
                                dmnvariation.VariationCode = "";
                                dmnvariation.Aid = Listdata.Id;
                                dmnvariation.Type = Convert.ToInt32(Product_Addon_Variation_Type.Addon);
                                dmnvariation.Price = 0;
                                dmnvariation.ProductId = Convert.ToInt32(prostatus.ProductId);
                                dmnvariation.ItemCode_UPC = Convert.ToString(txtitemcode.Text);
                                dmnvariation.CreatedBy = FrmDashboard.UserId;
                                dmnvariation.BranchId = FrmDashboard.BranchId;
                                dmnvariation.Status = Listdata.Status;
                                var usertype = blladdon.AddData("prdInsert_upd_AddonVariation", dmnvariation);
                                if (usertype.Pavid > 0)
                                {
                                    varia_count = varia_count + 1;
                                }
                            }
                        }
                        #endregion
                    }

                    if (statuscount == gridcount)
                    {
                        if (pb_view_pic.BackgroundImage != null)
                        {
                            if (File.Exists(Compressedimg + "\\Image.jpeg"))
                            {
                                bool statuspic = clsDocument.CopyFile(Compressedimg + "\\Image.jpeg", Convert.ToString(prostatus.ProductId) + ".jpeg", DocumentType.FoodPhoto);
                                //if (status == false)
                                //{
                                //    MessageBox.Show(this, "Unable To Upload Image Please Try Again ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                //}
                            }
                        }
                        else
                        {
                            bool imageExist = clsDocument.FileExistOrNot(Convert.ToString(prostatus.ProductId) + ".jpeg", DocumentType.FoodPhoto);
                            if (imageExist)
                            {
                                clsDocument.DeleteImage(Convert.ToString(prostatus.ProductId) + ".jpeg", DocumentType.FoodPhoto);
                            }
                        }
                        if (Convert.ToUInt32(txtpid.Text) > 0)
                            CMessageBox.Show("Update Successfully saved");
                        else
                            CMessageBox.Show("Successfully saved");

                        clearall();
                        //Fillitem();
                    }
                    //else
                    //    CMessageBox.Show("Save failed", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void clearall()
        {
            try
            {
                txtsearch.Text = "Food Name.....";
                txtsearch.ForeColor = Color.Gray;
                pb_view_pic.BackgroundImage = null;
                pb_view_pic.Image = null;
                empImgExt = "";
                txtqty.Text = "";
                txtitemcode.Text = "";
                txtpid.Text = "0";
                txtproductname.Text = "";
                txtprice.Text = "";
                txtDiscAmnt.Text = "";
                txtDisPer.Text = "";
                txtmrp.Text = "";
                txtdes.Text = "";
                cmbtax.SelectedIndex = 0;
                cmbTaxType.SelectedIndex = 0;
                cmbIgstPrcnt.SelectedIndex = 0;
                CmbCategory.SelectedIndex = 0;
                cmbTaxType.SelectedIndex = 0;
                dgv_registartion.Rows.Clear();
                dgv_registartion.Rows.Add();
                btn_del_ingre.Visible = false;
                txtSgstPrcnt.Text = "";
                txtCgstPrcnt.Text = "";

                DataGridViewImageCell cell = (DataGridViewImageCell)dgv_registartion.Rows[0].Cells[Raction];
                cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                cell.Value = Properties.Resources.add_row;

                TextBoxList.Clear();
                flp_addonlist.Controls.Clear();
                Checked_Addon_List.Clear();
                Prev_rowindex = 0;
                Mandatory_Rowindex = 0;

                #region Clear Variation   

                for (int i = dgv_variation.Rows.Count-1; i >=0; i--)
                {
                    string tlpname = "tlp_row_layout" + Convert.ToInt32(dgv_variation.Rows[i].Cells[Arowno].Value);
                    TableLayoutPanel tlp_row_layout = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                    if (tlp_row_layout != null)
                    {
                        tlp_variation.Controls.Remove(tlp_row_layout);
                        dgv_variation.Rows.RemoveAt(i);
                    }
                }
                int rowcount = tlp_variation.RowStyles.Count;
                for (int i = 0; i < rowcount; i++)
                {
                    if(i!=0)
                        tlp_variation.RowStyles.RemoveAt(1);
                }
                #endregion

                chk_variation.Checked = false;
                chk_addons.Checked = false;
                SerachFlag = false;

                txtmrp.Enabled = true;
                txtprice.Enabled = true;
            }
            catch(Exception ex)
            {

            }
            
        }

        public bool MandatoryCheck()
        {
            if (string.IsNullOrWhiteSpace(txtpid.Text))
                txtpid.Text = "0";
            if (string.IsNullOrWhiteSpace(txtqty.Text))
                txtqty.Text = "0";
            if (string.IsNullOrWhiteSpace(txtDiscAmnt.Text))
                txtDiscAmnt.Text = "0";
            if (string.IsNullOrWhiteSpace(txtDisPer.Text))
                txtDisPer.Text = "0";
            if (txtproductname.Text == "" || txtproductname.Text == null)
            {
                CMessageBox.Show("Please Enter Item Name", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtproductname.Focus();
                return false;
            }
            else if ( (txtprice.Text == "" || txtprice.Text == null || Convert.ToDecimal(txtprice.Text) <= 0) && chk_variation.Checked==false)
            {
                CMessageBox.Show("Please Enter Item Price", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtprice.Focus();
                return false;
            }
            else if ((txtmrp.Text == "" || txtmrp.Text == null || Convert.ToDecimal(txtmrp.Text) <= 0) && chk_variation.Checked == false)
            {
                CMessageBox.Show("Please Enter MRP", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmrp.Focus();
                return false;
            }
            else if (Convert.ToInt32(CmbCategory.SelectedValue) == -1)
            {
                CMessageBox.Show("Please Select Food Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CmbCategory.Focus();
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
        private void FrmIncrediant_Load(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
            {
                FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.Product)))
                    btnsave.Enabled = true;
                else
                    btnsave.Enabled = false;
            }
            cmbTaxType.SelectedIndex = 0;
            Taxfill();
            clearall();
            combofill();
            rearrange_slno();
            textb_shape();

            dgv_registartion.BorderStyle = BorderStyle.None;
            dgv_registartion.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_registartion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_registartion.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_registartion.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_registartion.BackgroundColor = Color.White;

            dgv_registartion.EnableHeadersVisualStyles = false;
            dgv_registartion.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_registartion.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_registartion.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            #region menu Picture Folder 
            if (!Directory.Exists(Compressedimg))
            {
                Directory.CreateDirectory(Compressedimg);
                DirectoryInfo dInfo = new DirectoryInfo(Compressedimg);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                dInfo.SetAccessControl(dSecurity);
            }
            else
            {
                foreach (string file in Directory.GetFiles(Compressedimg))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(file);
                }
            }
            #endregion
            txtsearch.Text = "Food Name .....";
            txtsearch.ForeColor = Color.Gray;

            tlp_regform_grid.VerticalScroll.Visible = true;
            tlp_regform_grid.HorizontalScroll.Visible = false;
            tlp_regform_grid.VerticalScroll.Enabled = true;
            tlp_regform_grid.Dock = DockStyle.Fill;

            //SetRowBackgroundColor(tlp_regform_grid, 0, Color.Gold);
            //SetRowBackgroundColor(tlp_regform_grid, 1, Color.Green);
            //SetRowBackgroundColor(tlp_regform_grid, 2, Color.Bisque);
            //SetRowBackgroundColor(tlp_regform_grid, 3, Color.Brown);
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        public void combofill()
        {

            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "fcid,CategoryName";
                objSearch.Table = " tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where Status=1 and CategoryType =0";
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                CmbCategory.DataSource = gridSource;
                CmbCategory.ValueMember = "Id";
                CmbCategory.DisplayMember = "Value";

                cmbSearchlist.DataSource = gridSource;
                cmbSearchlist.ValueMember = "Id";
                cmbSearchlist.DisplayMember = "Value";

                objSearch.Select = " UId,UnitName";
                objSearch.Table = " tblUnit";
                objSearch.WhereNdGroupBy = "where status=1";
                objSearch.OrderBy = "";
                var gridSource1 = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_unit.DataSource = gridSource1;
                cmb_unit.ValueMember = "Id";
                cmb_unit.DisplayMember = "Value";

                objSearch.Select = "fcid id ,CategoryName Value";
                objSearch.Table = " tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where Status=1 and CategoryType =5";
                objSearch.OrderBy = "";
                product_variation = bllSearch.GetSearchData("prdComboFill", objSearch);

                objSearch.Select = "fcid id ,CategoryName Value";
                objSearch.Table = " tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where Status=1 and CategoryType =4";
                objSearch.OrderBy = "";
                Addon_List_Data = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_addon_cate.DataSource = Addon_List_Data;
                cmb_addon_cate.ValueMember = "Id";
                cmb_addon_cate.DisplayMember = "Value";
                

            }
            catch (Exception ex)
            {

            }
        }
        public void rearrange_slno()
        {
            for (int i = 0; i < dgv_registartion.RowCount; i++)
            {
                dgv_registartion.Rows[i].Cells[Rslno].Value = i + 1;
            }
        }

        private void dgv_registartion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == Raction)
                {
                    if (e.RowIndex == dgv_registartion.Rows.Count - 1)
                    {
                        if (IsValidData(e.RowIndex))
                            return;

                        DataGridViewImageCell cell = (DataGridViewImageCell)dgv_registartion.Rows[e.RowIndex].Cells[Raction];
                        cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        cell.Value = Properties.Resources.delete;

                        dgv_registartion.Rows.Add();
                        rearrange_slno();
                        dgv_registartion.CurrentCell = dgv_registartion.Rows[dgv_registartion.Rows.Count - 1].Cells[1];
                        dgv_registartion.Focus();

                        DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_registartion.Rows[dgv_registartion.Rows.Count - 1].Cells[Raction];
                        cell1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        cell1.Value = Properties.Resources.add_row;
                    }
                    else if (dgv_registartion.Rows[e.RowIndex].Cells[RIng_DetailsIdno].Value == null && Convert.ToInt32(dgv_registartion.Rows[e.RowIndex].Cells[Ritemidno].Value) > 0)
                    {
                        dgv_registartion.Rows.RemoveAt(dgv_registartion.CurrentRow.Index);
                        int n = dgv_registartion.RowCount;
                        int count = 0;
                        if (n > 0)
                        {
                            for (int i = 0; i < n; i++)
                                dgv_registartion.Rows[i].Cells[Rslno].Value = i + 1;
                            dgv_registartion.Rows.Add(1);
                            DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_registartion.Rows[dgv_registartion.Rows.Count - 1].Cells[Raction];
                            cell1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            cell1.Value = Properties.Resources.add_row;

                            n = dgv_registartion.RowCount;
                            for (int i = 0; i < n; i++)
                            {
                                if (dgv_registartion.Rows[i].Cells[Ritemidno].Value == null)
                                {
                                    count = count + 1;
                                }
                            }
                            if (count > 1)
                            {
                                for (int i = 0; i < n; i++)
                                {
                                    if (dgv_registartion.Rows[i].Cells[Ritemidno].Value == null)
                                    {
                                        dgv_registartion.Rows.RemoveAt(i);
                                        break;
                                    }
                                }
                            }

                        }

                        if (n == 0)
                        {
                            dgv_registartion.Rows.Add(1);
                        }
                        rearrange_slno();

                    }
                    else if (Convert.ToInt32(dgv_registartion.Rows[e.RowIndex].Cells[RIng_DetailsIdno].Value) > 0 )
                    {
                        var result = CMessageBox.Show("Do You want Delete This Record", "Confirm The Action", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            dgv_registartion.Rows.RemoveAt(dgv_registartion.CurrentRow.Index);
                            int n = dgv_registartion.RowCount;
                            int count = 0;
                            if (n > 0)
                            {
                                for (int i = 0; i < n; i++)
                                    dgv_registartion.Rows[i].Cells[Rslno].Value = i + 1;
                                dgv_registartion.Rows.Add(1);
                                DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_registartion.Rows[dgv_registartion.Rows.Count - 1].Cells[Raction];
                                cell1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                cell1.Value = Properties.Resources.add_row;

                                n = dgv_registartion.RowCount;
                                for (int i = 0; i < n; i++)
                                {
                                    if (dgv_registartion.Rows[i].Cells[Ritemidno].Value == null)
                                    {
                                        count = count + 1;
                                    }
                                }
                                if (count > 1)
                                {
                                    for (int i = 0; i < n; i++)
                                    {
                                        if (dgv_registartion.Rows[i].Cells[Ritemidno].Value == null)
                                        {
                                            dgv_registartion.Rows.RemoveAt(i);
                                            break;
                                        }
                                    }
                                }

                            }

                            if (n == 0)
                            {
                                dgv_registartion.Rows.Add(1);
                            }
                            rearrange_slno();

                        }
                    }                    

                }
            }
            catch(Exception ex)
            {

            }
        }
        private bool IsValidData(int rowno)
        {
            DataGridViewTextBoxCell cell = dgv_registartion.Rows[rowno].Cells[Ritemno] as DataGridViewTextBoxCell;
            string selectedValue = cell.EditedFormattedValue?.ToString();
            if (string.IsNullOrWhiteSpace(selectedValue) || selectedValue == ".")
            {
                CMessageBox.Show("Please Select the item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_registartion.CurrentCell = dgv_registartion.Rows[rowno].Cells[Ritemno];
                dgv_registartion.BeginEdit(false);

                return true;
            }
            DataGridViewTextBoxCell cell1 = dgv_registartion[Rqtyno, rowno] as DataGridViewTextBoxCell;
            if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == "." || cell.EditedFormattedValue.ToString() == string.Empty)
            {
                CMessageBox.Show("Please Enter the Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_registartion.CurrentCell = dgv_registartion.Rows[rowno].Cells[Rqtyno];
                dgv_registartion.BeginEdit(false);
                return true;
            }

            DataGridViewComboBoxCell cell2 = dgv_registartion.Rows[rowno].Cells[Runitno] as DataGridViewComboBoxCell;
            string selectedValue2 = cell2.EditedFormattedValue?.ToString();
            if (string.IsNullOrWhiteSpace(selectedValue2) || selectedValue2 == ".")
            {
                CMessageBox.Show("Please Select the Unit Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_registartion.CurrentCell = dgv_registartion.Rows[rowno].Cells[Runitno];
                dgv_registartion.BeginEdit(false);

                return true;
            }
            return false;
        }
        //private void dgv_registartion_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    if (e.ColumnIndex == 4)
        //    {
        //        if (e.RowIndex == dgv_registartion.Rows.Count - 1)
        //            e.Value = Properties.Resources.add_row;
        //        else
        //            e.Value = Properties.Resources.delete;
        //    }
        //}

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void FillData(int Pid)
        {
            try
            {
                rowindex = 1;
                BllSearch<DmnProducts> bllSearchproduct = new BllSearch<DmnProducts>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblProduct";
                objSearch.WhereNdGroupBy = "where status=1 and ProductId= " + Pid;
                objSearch.OrderBy = "";
                var gridSource = bllSearchproduct.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    btn_del_ingre.Visible = true;
                    tlp_menu_Gallery_main.Visible = false;
                    tlp_registartion_toggle.Visible = true;
                    Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
                    tlp_main.Controls.Remove(controlToRemove);
                    tlp_main.Controls.Add(tlp_registartion_toggle, 1, 0);
                    tlp_registartion_toggle.Dock = DockStyle.Fill;

                    txtpid.Text = Convert.ToString(gridSource[0].ProductId);
                    txtproductname.Text = Convert.ToString(gridSource[0].Product);
                    txtDiscAmnt.Text = Convert.ToString(gridSource[0].DiscountAmt);
                    txtDisPer.Text = Convert.ToString(gridSource[0].DiscountPer);
                    txtmrp.Text = Convert.ToString(gridSource[0].MRP);
                    txtprice.Text = Convert.ToString(gridSource[0].Price);
                    cmbtax.Text = Convert.ToString(gridSource[0].Tax);
                    txtqty.Text = Convert.ToString(gridSource[0].Quantity);
                    CmbCategory.SelectedValue = Convert.ToInt64(gridSource[0].Fcid);
                    cmbTaxType.Text = Convert.ToString(gridSource[0].TaxType);
                    txtdes.Text = Convert.ToString(gridSource[0].Description);
                    txtitemcode.Text = Convert.ToString(gridSource[0].ItemCode);

                    string imageName = Convert.ToString(gridSource[0].ProductId) + ".jpeg";
                    bool imageExist, brLogoExist;
                    if (imageName != "" && imageName != null)
                    {
                        imageExist = clsDocument.FileExistOrNot(imageName, DocumentType.FoodPhoto);
                        if (imageExist)
                        {
                            pb_view_pic.BackgroundImage = clsDocument.DownloadImage(imageName, DocumentType.FoodPhoto);
                            pb_view_pic.BackgroundImageLayout = ImageLayout.Stretch;
                            pb_view_pic.Image = null;
                            empImgExt = imageName;
                        }

                    }

                    if (cmbTaxType.Text == "GST")
                        cmbIgstPrcnt.SelectedValue = Convert.ToInt64(gridSource[0].TaxSettingsId);
                    else
                        cmbVatPrcnt.SelectedValue = Convert.ToInt64(gridSource[0].TaxSettingsId);

                    txtdes.Text = Convert.ToString(gridSource[0].Description);

                    if (gridSource[0].Status == 1)
                        chb_active.Checked = true;
                    else
                        chb_active.Checked = false;

                    BllSearch<DmnGridFields> bllSearchdetails = new BllSearch<DmnGridFields>();
                    DmnSearch objSearchDetails = new DmnSearch();
                    objSearchDetails.Select = "id.ItemId Field1,i.Itemname Field2,id.quantity Field3,u.uid Field4,id.Ing_DetailsId Field5,id.status Field6 ";
                    objSearchDetails.Table = "tblProduct p LEFT JOIN tblIngredientDetails id on id.ProductId=p.ProductId left join tblUnit u on u.UId=id.Unit left join tblItem i on i.ItemId=id.ItemId";
                    objSearchDetails.WhereNdGroupBy = "where p.status=1 and  p.ProductId= " + Pid;
                    objSearchDetails.OrderBy = "";
                    var gridSource_pro = bllSearchdetails.GetSearchData("prdSearch", objSearchDetails);
                    dgv_registartion.Rows.Clear();
                    int cnt = 0;
                    for (int i = 0; i < gridSource_pro.Count; i++)
                    {
                        if(Convert.ToInt32(gridSource_pro[i].Field6)==1)
                        {
                            dgv_registartion.Rows.Add();
                            dgv_registartion.Rows[i].Cells[Ritemidno].Value = Convert.ToInt32(gridSource_pro[i].Field1);
                            dgv_registartion.Rows[i].Cells[Ritemno].Value = Convert.ToString(gridSource_pro[i].Field2);
                            dgv_registartion.Rows[i].Cells[Rqtyno].Value = Convert.ToDecimal(gridSource_pro[i].Field3);
                            dgv_registartion.Rows[i].Cells[Runitno].Value = Convert.ToInt64(gridSource_pro[i].Field4);
                            dgv_registartion.Rows[i].Cells[RIng_DetailsIdno].Value = Convert.ToInt32(gridSource_pro[i].Field5);
                            DataGridViewImageCell cell = (DataGridViewImageCell)dgv_registartion.Rows[i].Cells[Raction];
                            cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            cell.Value = Properties.Resources.delete;
                            cnt = cnt + 1;
                        }
                    }
                    if(dgv_registartion.RowCount==0)
                        dgv_registartion.Rows.Add();
                    rearrange_slno();
                    dgv_registartion.Rows.Add();
                    DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_registartion.Rows[cnt].Cells[Raction];
                    cell1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    cell1.Value = Properties.Resources.add_row;

                    #region Variation and Addon
                    BllSearch<DmnGridFields> bllSearchdetails_VA = new BllSearch<DmnGridFields>();
                    DmnSearch objSearchDetails_VA = new DmnSearch();
                    objSearchDetails_VA.Select = "Pavid Field1,pav.Caption Field2,pav.Price Field3,pav.VariationCode Field4,a.AddonName Field5,pav.Type Field6,pav.Aid Field7";
                    objSearchDetails_VA.Table = "tblProduct_Addon_Variation pav left join tblAddon a on a.Aid=pav.Aid and a.Status=1";
                    objSearchDetails_VA.WhereNdGroupBy = "where pav.status=1 and pav.ProductId= " + Pid;
                    objSearchDetails_VA.OrderBy = "";
                    var gridSource_VA = bllSearchdetails_VA.GetSearchData("prdSearch", objSearchDetails_VA);
                    if(gridSource_VA.Count>0)
                    {
                        TextBoxList.Clear();
                        FillAddon();
                        dgv_variation.Rows.Clear();
                        tlp_regform_grid.SuspendLayout();
                        for (int j = 0; j < gridSource_VA.Count; j++)
                        {
                            if (Convert.ToInt32(gridSource_VA[j].Field6) == 1)
                            {
                                SerachFlag = true;
                                pb_addon.Visible = true;
                                tlp_regform_grid.AutoScroll = true;
                                tlp_regform_grid.VerticalScroll.Visible = false;
                                tlp_regform_grid.HorizontalScroll.Visible = true;
                                tlp_regform_grid.HorizontalScroll.Maximum = 0;
                                tlp_regform_grid.AutoScrollMinSize = new Size(0, 50);

                                tlp_variation.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                tlp_variation.Dock = DockStyle.Top;

                                #region Main TableLayoutPanel and Sub TableLayoutPanel 
                                // Add TableLayoutPanel inside the panel
                                TableLayoutPanel tlp_row_layout = new TableLayoutPanel();
                                tlp_row_layout.Name = "tlp_row_layout" + rowindex;
                                tlp_row_layout.Dock = DockStyle.Fill;
                                tlp_row_layout.RowCount = 1;
                                tlp_row_layout.ColumnCount = 2;
                                tlp_row_layout.Height = 65;
                                tlp_row_layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 95));
                                tlp_row_layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5));

                                //foreach (Control control in tlp_variation.Controls)
                                //{
                                //    if (control is TableLayoutPanel tlp_row_layout_tem)
                                //    {
                                //        tlp_row_layout_tem.RowStyles[0].SizeType = SizeType.Percent;
                                //        tlp_row_layout_tem.RowStyles[0].Height = 65;
                                //    }
                                //}

                                TableLayoutPanel tlp_left = new TableLayoutPanel();
                                tlp_left.Name = "tlp_left_" + rowindex;
                                tlp_left.Dock = DockStyle.Top;
                                tlp_left.RowCount = 2;
                                tlp_left.ColumnCount = 3;
                                tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                                tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                                tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                                #endregion

                                #region Textbox Adding
                                // Add labels and controls
                                string[] labels = { "Size", "Price", "Code" };
                                for (int i = 0; i < 3; i++)
                                {
                                    Label label = new Label();
                                    label.Name = labels[i] + rowindex;
                                    label.Text = labels[i];
                                    label.Dock = DockStyle.Fill;
                                    label.ForeColor = System.Drawing.Color.Black;
                                    label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold);
                                    label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                                    Control control;

                                    TextBox textBox = new TextBox();
                                    textBox.Name = "txt_" + labels[i].ToLower() + rowindex;
                                    textBox.Tag = rowindex;
                                    textBox.Dock = DockStyle.Top;
                                    textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12, System.Drawing.FontStyle.Regular);
                                    textBox.BorderStyle = BorderStyle.None;
                                    textBox.BackColor = System.Drawing.Color.FromArgb(196, 223, 223);
                                    textBox.Multiline = true;
                                    textBox.Height = 30;
                                    if (i == 0)
                                    {
                                        textBox.AccessibleDescription = "Enter Variation Type..";
                                        textBox.Text = gridSource_VA[j].Field2;
                                    }
                                    else if (i == 1)
                                    {
                                        textBox.AccessibleDescription = "Enter Price..";
                                        textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress_variation);
                                        textBox.Text = gridSource_VA[j].Field3;
                                    }
                                    else
                                    {
                                        textBox.Text = gridSource_VA[j].Field4;
                                    }
                                    textBox.Leave += TextBox_Leave;
                                    textBox.Enter += TextBox_Enter;

                                    control = textBox;
                                    string tempname = control.Name;
                                    TextBoxList.Add(tempname);

                                    tlp_left.Controls.Add(label, i, 0);
                                    tlp_left.Controls.Add(control, i, 1);
                                }
                                #endregion

                                #region Grid insert
                                dgv_variation.Rows.Add();
                                int newIndex = dgv_variation.Rows.Count - 1;
                                dgv_variation.Rows[newIndex].Cells[Arowno].Value = rowindex;
                                dgv_variation.Rows[newIndex].Cells[Avidno].Value = gridSource_VA[j].Field1;
                                dgv_variation.Rows[newIndex].Cells[avariationno].Value = gridSource_VA[j].Field2;
                                dgv_variation.Rows[newIndex].Cells[apriceno].Value = gridSource_VA[j].Field3;
                                dgv_variation.Rows[newIndex].Cells[Acodeno].Value = gridSource_VA[j].Field4;
                                #endregion

                                #region Delete Icon
                                PictureBox pb_delrow = new PictureBox()
                                {
                                    Name = "Rowdel_" + rowindex,
                                    Image = Properties.Resources.delete,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Dock = DockStyle.None,
                                    Tag = rowindex,
                                    AccessibleDescription = gridSource_VA[j].Field1,
                                };
                                pb_delrow.Click += pb_delrow_Serach_Click;

                                tlp_row_layout.Controls.Add(tlp_left, 0, 0);
                                tlp_row_layout.Controls.Add(pb_delrow, 1, 0);

                                #endregion

                                tlp_variation.Controls.Add(tlp_row_layout, 0, rowindex);
                                rowindex++;
                                RoundAddonTextbox();
                                chk_variation.Checked = true;

                            }
                            else
                            {
                                #region Adding checkbox Data to the List
                                Checked_Addon_List.Add(new CAddonlist
                                {
                                    Id = Convert.ToInt32(gridSource_VA[j].Field7),
                                    Pavid = Convert.ToInt32(gridSource_VA[j].Field1),
                                    Cnames = gridSource_VA[j].Field2,
                                    Status = 1,

                                });
                                string chkname = "chkaddon_" + Convert.ToInt32(gridSource_VA[j].Field7);
                                CheckBox chkcontrol = this.Controls.Find(chkname, true).FirstOrDefault() as CheckBox;
                                if (chkcontrol != null)
                                    chkcontrol.Checked = true;
                                #endregion

                                flp_addonlist.Visible = true;
                                chk_addons.Checked = true;
                            }
                        }

                        if(chk_variation.Checked)
                        {
                            txtmrp.Enabled = false;
                            txtprice.Enabled = false;
                        }
                        tlp_regform_grid.ResumeLayout();
                    }
                    SerachFlag = false;
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }


        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
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

        private void cmbTaxType_SelectedValueChanged(object sender, EventArgs e)
        {
            int[] columnsToRemove = { 1, 2, 3 };
            int[] rowsToRemove = { 5, 6, 7, 8 };

            foreach (int column in columnsToRemove)
            {
                foreach (int row in rowsToRemove)
                {
                    Control controlToRemove = tlp_registration.GetControlFromPosition(column, row);
                    if (controlToRemove != null)
                    {
                        tlp_registration.Controls.Remove(controlToRemove);
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
                tlp_registration.Controls.Add(lbltax, 0, 5);
                tlp_registration.Controls.Add(lbltaxtype, 1, 5);
                tlp_registration.Controls.Add(lbligst, 2, 5);
                tlp_registration.Controls.Add(tlp_s_c_gst_lbl, 3, 5);

                //row2
                tlp_registration.Controls.Add(cmbtax, 0, 6);
                tlp_registration.Controls.Add(cmbTaxType, 1, 6);
                tlp_registration.Controls.Add(cmbIgstPrcnt, 2, 6);
                tlp_registration.Controls.Add(tlp_S_C_text, 3, 6);

                //row3
                tlp_registration.Controls.Add(lbldes, 0, 7);
                //row4
                tlp_registration.Controls.Add(txtdes, 0, 8);
                tlp_registration.Controls.Add(btn_del_ingre, 1, 8);

            }
            else if (cmbTaxType.Text == "VAT")
            {
                cmbVatPrcnt.Dock = DockStyle.Fill;
                lblvat.Dock = DockStyle.Fill;

                //row1
                tlp_registration.Controls.Add(lbltax, 0, 5);
                tlp_registration.Controls.Add(lbltaxtype, 1, 5);
                tlp_registration.Controls.Add(lblvat, 2, 5);
                tlp_registration.Controls.Add(lbldes, 3, 5);
                //row2
                tlp_registration.Controls.Add(cmbtax, 0, 6);
                tlp_registration.Controls.Add(cmbTaxType, 1, 6);
                tlp_registration.Controls.Add(cmbVatPrcnt, 2, 6);
                tlp_registration.Controls.Add(txtdes, 3, 6);
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

        private void txtDisPer_Leave(object sender, EventArgs e)
        {
            if (txtDisPer.Text != "")
            {
                if (Convert.ToDecimal(txtDisPer.Text) > 100)
                {
                    CMessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDiscAmnt.Text = "0.00";
                    txtDisPer.Text = "0.00";
                    txtDisPer.Focus();
                }
                else
                {
                    txtDiscAmnt.Text = "0.00";
                }
            }
            discountAmnt();
        }

        private void txtDisPer_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtDisPer.Text, out value))
            {
                txtDisPer.Text = "0";
            }
        }

        private void txtDiscAmnt_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtDiscAmnt.Text, out value))
            {
                txtDiscAmnt.Text = "0";
            }
        }

        private void txtDiscAmnt_Leave(object sender, EventArgs e)
        {
            if (txtDiscAmnt.Text != "" && txtmrp.Text != "" && Convert.ToDecimal(txtDiscAmnt.Text) != 0 && Convert.ToDecimal(txtmrp.Text) != 0)
            {
                if (Convert.ToDecimal(txtDiscAmnt.Text) > Convert.ToDecimal(txtmrp.Text))
                {
                    MessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDiscAmnt.Text = "0.00";
                    txtDisPer.Text = "0.00";
                    txtDiscAmnt.Focus();
                }
                else
                {
                    decimal discountPer = (Convert.ToDecimal(txtDiscAmnt.Text) / Convert.ToDecimal(txtmrp.Text) * 100);
                    txtDisPer.Text = discountPer.ToString("N2");
                }
            }
            discountPer();
        }

        private void txtDisPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key

            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
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
        public void discountAmnt()
        {
            if (txtDisPer.Text != "")
            {
                if (Convert.ToDecimal(txtDisPer.Text) > 100)
                {
                    MessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDisPer.Focus();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(txtprice.Text))
                        txtprice.Text = "0";
                    decimal discountAmnt = Convert.ToDecimal(txtDisPer.Text.Trim()) * Convert.ToDecimal(txtprice.Text) / 100;
                    txtDiscAmnt.Text = discountAmnt.ToString("N2");
                }
            }
        }
        public void discountPer()
        {
            if (txtDiscAmnt.Text != "" && txtprice.Text != "" && Convert.ToDecimal(txtprice.Text) != 0)
            {
                if (Convert.ToDecimal(txtDiscAmnt.Text) > Convert.ToDecimal(txtprice.Text))
                {
                    MessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDiscAmnt.Focus();
                }
                else
                {
                    decimal discountPer = (Convert.ToDecimal(txtDiscAmnt.Text) / Convert.ToDecimal(txtmrp.Text) * 100);
                    txtDisPer.Text = discountPer.ToString("N2");
                }
            }
        }

        private void pb_menupic_Click(object sender, EventArgs e)
        {
            try
            {
                if (empImgExt != "" && empImgExt != null)
                {
                    pnl_menupic.Visible = true;
                    pnl_menupic.Location = new Point(this.ClientSize.Width / 2 - pnl_menupic.Size.Width / 2, this.ClientSize.Height / 2 - pnl_menupic.Size.Height / 2);
                }
                else
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
                    DialogResult dialogResult = openFileDialog.ShowDialog();
                    if (openFileDialog.FileName != "")
                    {
                        empImgExt = openFileDialog.FileName;
                        pb_view_pic.BackgroundImage = Image.FromFile(openFileDialog.FileName);
                        pb_view_pic.BackgroundImageLayout = ImageLayout.Stretch;
                        pb_view_pic.Image = null;
                        Bitmap bitmap = new Bitmap(Image.FromFile(openFileDialog.FileName));
                        bitmap.Save(Compressedimg + "\\Image.jpeg");
                    }
                }


            }
            catch (Exception ex)
            {

            }
        }

        private void btn_clear_pic_Click(object sender, EventArgs e)
        {
            empImgExt = "";
            pb_view_pic.BackgroundImage = null;
            pb_view_pic.Image = FoodandBeverage.Properties.Resources.no_pictures;
            pb_view_pic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btn_change_pic_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                Compressedimg = Application.StartupPath + "\\CompressedImg";
                empImgExt = openFileDialog.FileName;
                pb_view_pic.BackgroundImage = Image.FromFile(openFileDialog.FileName);
                pb_view_pic.BackgroundImageLayout = ImageLayout.Stretch;
                pb_view_pic.Image = null;
                Bitmap bitmap = new Bitmap(Image.FromFile(openFileDialog.FileName));
                bitmap.Save(Compressedimg + "\\Image.jpeg");
            }
        }

        #region gallery creation
        public void GalleryView(int serachmode)
        {
            BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = "*";
            dmnSearch.Table = "tblProduct p";// LEFT JOIN tblIngredientDetails id on id.ProductId=p.ProductId";
            if (comboBoxChanged)
            {
                comboBoxChanged = false;
                if (cmbSearchlist.SelectedIndex == 0)
                    dmnSearch.WhereNdGroupBy = "where p.Status=1";
                else
                    dmnSearch.WhereNdGroupBy = "where p.Status=1 and  Fcid = " + serachmode;
            }
            else
                dmnSearch.WhereNdGroupBy = "where p.Status=1";
            dmnSearch.OrderBy = "";
            var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

            if (gridSource.Count > 0)
            {
                tlp_gallery_sub.Controls.Clear();
                int rowIndex = 0;
                int columnIndex = 0;
                for (int i = 0; i < gridSource.Count; i++)
                {
                    if (columnIndex == 5)
                    {
                        columnIndex = 0;
                        rowIndex++;
                    }
                    string info = gridSource[i].Product + "," + gridSource[i].Price + "," + gridSource[i].MRP + "," + gridSource[i].Tax + "," + gridSource[i].TaxType + "," + gridSource[i].DiscountPer + "," + gridSource[i].DiscountAmt;

                    TableLayoutPanel tlp_Gallery_Box = new TableLayoutPanel();
                    PictureBox pictureBox = new PictureBox();
                    Label lbltablename = new Label();

                    // Configure the PictureBox
                    string imageName = Convert.ToString(gridSource[i].ProductId) + ".jpeg";
                    bool imageExist, brLogoExist;
                    if (imageName != "" && imageName != null)
                    {
                        imageExist = clsDocument.FileExistOrNot(imageName, DocumentType.FoodPhoto);
                        if (imageExist)
                            pictureBox.Image = clsDocument.DownloadImage(imageName, DocumentType.FoodPhoto);
                        else
                            pictureBox.Image = FoodandBeverage.Properties.Resources.Deafult_food_pic;
                    }
                    pictureBox.Name = Convert.ToString(gridSource[i].ProductId);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Width = 150;
                    pictureBox.Height = 150;
                    pictureBox.Tag = info;

                    // Configure the Label
                    lbltablename.Name = Convert.ToString(gridSource[i].ProductId);
                    tlp_Gallery_Box.Tag = info;
                    lbltablename.Text = Convert.ToString(gridSource[i].Product);
                    lbltablename.ForeColor = Color.Black;
                    lbltablename.AutoSize = true;
                    lbltablename.BackColor = Color.Transparent;
                    lbltablename.Dock = DockStyle.Top;
                    lbltablename.Width = 60;
                    lbltablename.Font = new Font("Arial", 10, FontStyle.Bold);

                    // Configure the TableLayoutPanel
                    tlp_Gallery_Box.RowCount = 2;
                    // Add the PictureBox and Label to the TableLayoutPanel
                    tlp_Gallery_Box.Controls.Add(pictureBox, 0, 0); // PictureBox in the first row
                    tlp_Gallery_Box.Controls.Add(lbltablename, 0, 1); // Label in the second row
                    tlp_Gallery_Box.Name = Convert.ToString(gridSource[i].ProductId);
                    tlp_Gallery_Box.Tag = info;
                    tlp_Gallery_Box.Dock = DockStyle.None;
                    tlp_Gallery_Box.Margin = new Padding(20, 10, 20, 10);
                    tlp_Gallery_Box.Width = 150;
                    tlp_Gallery_Box.Height = 200;
                    tlp_Gallery_Box.BorderStyle = BorderStyle.FixedSingle;
                    tlp_Gallery_Box.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

                    pictureBox.MouseEnter += TlpTableInner_MouseEnter;
                    lbltablename.MouseEnter += TlpTableInner_MouseEnter;

                    pictureBox.MouseLeave += TlpTableInner_MouseLeave;
                    lbltablename.MouseLeave += TlpTableInner_MouseLeave;

                    tlp_Gallery_Box.MouseEnter += TlpTableInner_MouseEnter;
                    tlp_Gallery_Box.MouseLeave += TlpTableInner_MouseLeave;

                    pictureBox.Click += new EventHandler(ProductClickEventHandler);
                    lbltablename.Click += new EventHandler(ProductClickEventHandler);
                    tlp_Gallery_Box.Click += new EventHandler(ProductClickEventHandler);

                    pictureBox.MouseHover += new EventHandler(ProductMousehoverEventHandler);
                    lbltablename.MouseHover += new EventHandler(ProductMousehoverEventHandler);
                    tlp_Gallery_Box.MouseHover += new EventHandler(ProductMousehoverEventHandler);

                    tlp_gallery_sub.Controls.Add(tlp_Gallery_Box, columnIndex, rowIndex);
                    columnIndex++;
                }
                tlp_Gallery_Box.BorderStyle = BorderStyle.FixedSingle;
                tlp_Gallery_Box.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                tlp_Gallery_Main.Dock = DockStyle.Fill;
                tlp_gallery_sub.Dock = DockStyle.Fill;
                tlp_menu_Gallery_main.Dock = DockStyle.Fill;
                tlp_gallery_sub.AutoScroll = true;
                tlp_menu_Gallery_main.Controls.Add(tlp_gallery_sub, 0, 1);
            }
            else
                CMessageBox.Show("No Products For This Category");

        }
        private void ProductMousehoverEventHandler(object sender, EventArgs e)
        {
            try
            {
                toolTip_gallery.BackColor = Color.Green;
                toolTip_gallery.ForeColor = Color.Black;
                if (sender is PictureBox)
                {
                    PictureBox pictureBox = (PictureBox)sender;
                    string info = Convert.ToString(pictureBox.Tag);
                    // Split the info string by commas
                    string[] infoParts = info.Split(',');
                    string tooltipText = "";
                    tooltipText = $"Name : {infoParts[0]}{Environment.NewLine}price :{infoParts[1]}{Environment.NewLine}Mrp :{infoParts[2]}{Environment.NewLine}Tax :{infoParts[3]}{Environment.NewLine}Tax Type :{infoParts[4]}{Environment.NewLine}Discount % :{infoParts[5]}{Environment.NewLine}Discount Amount :{infoParts[6]}";
                    toolTip_gallery.Show(tooltipText, pictureBox);

                }
                else if (sender is Label)
                {
                    // Handle Label hover
                    Label label = (Label)sender;
                    string info = Convert.ToString(label.Tag);
                    // Split the info string by commas
                    string[] infoParts = info.Split(',');
                    string tooltipText = "";
                    tooltipText = $"Name : {infoParts[0]}{Environment.NewLine}price :{infoParts[1]}{Environment.NewLine}Mrp :{infoParts[2]}{Environment.NewLine}Tax :{infoParts[3]}{Environment.NewLine}Tax Type :{infoParts[4]}{Environment.NewLine}Discount % :{infoParts[5]}{Environment.NewLine}Discount Amount :{infoParts[6]}";
                    toolTip_gallery.Show(tooltipText, label);
                }
                else if (sender is TableLayoutPanel)
                {
                    // Handle TableLayoutPanel hover
                    TableLayoutPanel tlp = (TableLayoutPanel)sender;
                    string info = Convert.ToString(tlp.Tag);
                    // Split the info string by commas
                    string[] infoParts = info.Split(',');
                    string tooltipText = "";
                    tooltipText = $"Name : {infoParts[0]}{Environment.NewLine}price :{infoParts[1]}{Environment.NewLine}Mrp :{infoParts[2]}{Environment.NewLine}Tax :{infoParts[3]}{Environment.NewLine}Tax Type :{infoParts[4]}{Environment.NewLine}Discount % :{infoParts[5]}{Environment.NewLine}Discount Amount :{infoParts[6]}";
                    toolTip_gallery.Show(tooltipText, tlp);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if necessary
            }
        }


        private void ProductClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                if (clickedControl != null)
                {
                    string pid = clickedControl.Name;
                    FillData(Convert.ToInt32(pid));
                    //CMessageBox.Show("Clicked Table ID: " + pid);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void TlpTableInner_MouseEnter(object sender, EventArgs e)
        {
            TableLayoutPanel tlp = sender as TableLayoutPanel;
            if (tlp != null)
            {
                foreach (Control control in tlp.Controls)
                {
                    if (control is Label)
                    {
                        Label lbl = control as Label;
                        originalBackColor = lbl.BackColor;
                        lbl.BackColor = Color.LightGray;
                    }
                }

            }
        }

        private void TlpTableInner_MouseLeave(object sender, EventArgs e)
        {
            TableLayoutPanel tlp = sender as TableLayoutPanel;
            if (tlp != null)
            {
                foreach (Control control in tlp.Controls)
                {
                    if (control is Label)
                    {
                        Label lbl = control as Label;
                        lbl.BackColor = originalBackColor;
                    }
                }

            }
        }
        #endregion

        private void Btn_view_menu_Click(object sender, EventArgs e)
        {
            tlp_registartion_toggle.Visible = false;
            tlp_menu_Gallery_main.Visible = true;

            Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_menu_Gallery_main, 1, 0);
            tlp_menu_Gallery_main.Dock = DockStyle.Fill;
            GalleryView(0);
        }

        private void btn_back_reg_Click(object sender, EventArgs e)
        {
            tlp_menu_Gallery_main.Visible = false;
            tlp_registartion_toggle.Visible = true;
            Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_registartion_toggle, 1, 0);
            tlp_registartion_toggle.Dock = DockStyle.Fill;
        }

        private void btn_close_pic_Click(object sender, EventArgs e)
        {
            pnl_menupic.Visible = false;
        }

        private void txtmrp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtmrp.Text))
                txtmrp.Text = "0";
            if (string.IsNullOrWhiteSpace(txtprice.Text))
                txtprice.Text = "0";
            if (Convert.ToDecimal(txtmrp.Text) < Convert.ToDecimal(txtprice.Text))
            {
                CMessageBox.Show("MRP Rate Must be Greater than Sale Price", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmrp.Focus();
            }
        }

        private void txtprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
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

        private void btn_del_ingre_Click(object sender, EventArgs e)
        {
            var result = CMessageBox.Show("Do You want Delete This Record", "Confirm The Action", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                chb_active.Checked = false;
                btnsave.PerformClick();
                clearall();
            }
        }

        private void txtprice_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtprice.Text, out value))
            {
                txtprice.Text = "0";
            }
        }

        private void txtmrp_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtmrp.Text, out value))
            {
                txtmrp.Text = "0";
            }
        }

        private void txtqty_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtprice.Text, out value))
            {
                txtprice.Text = "0";
            }
        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
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

        private void dgv_registartion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int currentRowIndex = dgv_registartion.CurrentCell.RowIndex;
                int currentColumnIndex = dgv_registartion.CurrentCell.ColumnIndex;

                if (currentColumnIndex == dgv_registartion.ColumnCount - 1 && currentRowIndex == dgv_registartion.RowCount - 1)
                {
                    // You are in the last column and last row, so set focus to btnsave
                    btnsave.Focus();
                }
            }

            else if (e.KeyData == Keys.F8 && dgv_registartion.CurrentCell.ColumnIndex == Ritemno)
            {
                int cno = dgv_registartion.CurrentCell.ColumnIndex;
                int rowno = dgv_registartion.CurrentCell.RowIndex;
                ItemFillGrid(rowno);
            }
            else if (e.KeyData == Keys.Delete)
            {
                dgv_registartion.Rows.RemoveAt(dgv_registartion.CurrentRow.Index);
                int n = dgv_registartion.RowCount;
                int count = 0;
                if (n > 0)
                {
                    for (int i = 0; i < n; i++)
                        dgv_registartion.Rows[i].Cells[Rslno].Value = i + 1;
                    dgv_registartion.Rows.Add(1);

                    n = dgv_registartion.RowCount;
                    for (int i = 0; i < n; i++)
                    {
                        if (dgv_registartion.Rows[i].Cells[Ritemidno].Value == null)
                        {
                            count = count + 1;
                        }
                    }
                    if (count > 1)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            if (dgv_registartion.Rows[i].Cells[Ritemidno].Value == null)
                            {
                                dgv_registartion.Rows.RemoveAt(i);
                                break;
                            }
                        }
                    }

                }

                if (n == 0)
                {
                    dgv_registartion.Rows.Add(1);
                }
                rearrange_slno();
            }
        }


        private void dgv_registartion_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Ritemno)
            {
                if (dgv_registartion.Rows[dgv_registartion.CurrentCell.RowIndex].Cells[Ritemidno].Value == null)
                {
                    dgv_registartion.Rows[dgv_registartion.CurrentCell.RowIndex].Cells[Ritemno].Value = "F8 for Products";
                    dgv_registartion.Rows[dgv_registartion.CurrentCell.RowIndex].Cells[Ritemno].Style.ForeColor = Color.Red;
                }
                else
                    dgv_registartion.Rows[dgv_registartion.CurrentCell.RowIndex].Cells[Ritemno].Style.ForeColor = Color.Black;

            }
        }

        private void dgv_registartion_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Ritemno)
            {
                if (dgv_registartion.Rows[dgv_registartion.CurrentCell.RowIndex].Cells[Ritemidno].Value == null)
                {
                    dgv_registartion.Rows[dgv_registartion.CurrentCell.RowIndex].Cells[Ritemno].Value = "";
                }
            }
        }
        public void ItemFillGrid(int rowno)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "ID,ItemCode_UPC,Item Name,Category Name";
                _objSearch.Select = "ItemId as Id,ItemCode_UPC as Field1,Itemname as Field2,c.CategoryName  as Field3";
                _objSearch.Table = "tblItem i left join tblCategory_FTI c on c.Fcid=i.Fcid";
                _objSearch.WhereNdGroupBy = "where i.Status = 1 and i.Saletype=0";
                _objSearch.OrderBy = " ORDER BY Itemname ";
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
                            returnValue = Convert.ToString(search._retValue);
                        }
                    }
                    if (returnValue != "")
                    {
                        bool flag = false;
                        for(int i=0;i<dgv_registartion.RowCount;i++)
                        {
                            if (Convert.ToInt32(dgv_registartion.Rows[i].Cells[Ritemidno].Value) == Convert.ToInt32(returnValue))
                            {
                                dgv_registartion.Rows[i].Cells[Rqtyno].Value =Convert.ToDecimal(dgv_registartion.Rows[i].Cells[Rqtyno].Value) +1;
                                flag = true;
                                break;
                            }
                        }
                        if(flag == false)
                        {
                            BllSearch<DmnItem> bllSearch1 = new BllSearch<DmnItem>();
                            DmnSearch objSearch = new DmnSearch();
                            objSearch = new DmnSearch();
                            objSearch.Select = "*";
                            objSearch.Table = "tblItem";
                            objSearch.WhereNdGroupBy = "Where Status =1 and itemid = " + returnValue;
                            objSearch.OrderBy = " ";
                            var gridSource1 = bllSearch1.GetSearchData("prdSearch", objSearch);
                            if (gridSource1.Count > 0)
                            {
                                dgv_registartion.Rows[rowno].Cells[Rslno].Value = rowno + 1;
                                dgv_registartion.Rows[rowno].Cells[Ritemno].Value = gridSource1[0].Itemname;
                                dgv_registartion.Rows[rowno].Cells[Ritemidno].Value = gridSource1[0].ItemId;
                                dgv_registartion.Rows[rowno].Cells[Ritemno].Style.ForeColor = Color.Black;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            int returnValue = 0;
            DataTable dtColumnAlign = new DataTable();
            dtColumnAlign.Columns.Add();
            DmnSearch _objSearch = new DmnSearch();
            _objSearch.ColumnHeaders = "ID,Item Code,Product,";
            _objSearch.Select = "ProductId as Id,Itemcode as Field1,Product as Field2";//Description as Field2";
            _objSearch.Table = "tblProduct";
            _objSearch.WhereNdGroupBy = "where Status = 1";
            _objSearch.OrderBy = " ORDER BY Product";
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
                    FillData(returnValue);
                }
            }
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                btn_Searchitem.Focus();
        }

        private void btn_Searchitem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtsearch.Text == "" || txtsearch.Text == null)
                {
                    CMessageBox.Show("Please Enter Food Name ");
                    txtsearch.Focus();
                    return;
                }
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblProduct p";
                dmnSearch.WhereNdGroupBy = "where p.Status=1 and  Product LIKE '%" + txtsearch.Text + "%'";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    tlp_gallery_sub.Controls.Clear();
                    int rowIndex = 0;
                    int columnIndex = 0;
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        if (columnIndex == 5)
                        {
                            columnIndex = 0;
                            rowIndex++;
                        }
                        string info = gridSource[i].Product + "," + gridSource[i].Price + "," + gridSource[i].MRP + "," + gridSource[i].Tax + "," + gridSource[i].TaxType + "," + gridSource[i].DiscountPer + "," + gridSource[i].DiscountAmt;

                        TableLayoutPanel tlp_Gallery_Box = new TableLayoutPanel();
                        PictureBox pictureBox = new PictureBox();
                        Label lbltablename = new Label();

                        // Configure the PictureBox
                        string imageName = Convert.ToString(gridSource[i].ProductId) + ".jpeg";
                        bool imageExist, brLogoExist;
                        if (imageName != "" && imageName != null)
                        {
                            imageExist = clsDocument.FileExistOrNot(imageName, DocumentType.FoodPhoto);
                            if (imageExist)
                                pictureBox.Image = clsDocument.DownloadImage(imageName, DocumentType.FoodPhoto);
                            else
                                pictureBox.Image = FoodandBeverage.Properties.Resources.Deafult_food_pic;
                        }
                        pictureBox.Name = Convert.ToString(gridSource[i].ProductId);
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox.Width = 150;
                        pictureBox.Height = 150;
                        pictureBox.Tag = info;

                        // Configure the Label
                        lbltablename.Name = Convert.ToString(gridSource[i].ProductId);
                        tlp_Gallery_Box.Tag = info;
                        lbltablename.Text = Convert.ToString(gridSource[i].Product);
                        lbltablename.ForeColor = Color.Black;
                        lbltablename.AutoSize = true;
                        lbltablename.BackColor = Color.Transparent;
                        lbltablename.Dock = DockStyle.Top;
                        lbltablename.Width = 60;
                        lbltablename.Font = new Font("Arial", 10, FontStyle.Bold);

                        // Configure the TableLayoutPanel
                        tlp_Gallery_Box.RowCount = 2;
                        // Add the PictureBox and Label to the TableLayoutPanel
                        tlp_Gallery_Box.Controls.Add(pictureBox, 0, 0); // PictureBox in the first row
                        tlp_Gallery_Box.Controls.Add(lbltablename, 0, 1); // Label in the second row
                        tlp_Gallery_Box.Name = Convert.ToString(gridSource[i].ProductId);
                        tlp_Gallery_Box.Tag = info;
                        tlp_Gallery_Box.Dock = DockStyle.None;
                        tlp_Gallery_Box.Margin = new Padding(20, 10, 20, 10);
                        tlp_Gallery_Box.Width = 150;
                        tlp_Gallery_Box.Height = 200;
                        tlp_Gallery_Box.BorderStyle = BorderStyle.FixedSingle;
                        tlp_Gallery_Box.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

                        pictureBox.MouseEnter += TlpTableInner_MouseEnter;
                        lbltablename.MouseEnter += TlpTableInner_MouseEnter;

                        pictureBox.MouseLeave += TlpTableInner_MouseLeave;
                        lbltablename.MouseLeave += TlpTableInner_MouseLeave;

                        tlp_Gallery_Box.MouseEnter += TlpTableInner_MouseEnter;
                        tlp_Gallery_Box.MouseLeave += TlpTableInner_MouseLeave;

                        pictureBox.Click += new EventHandler(ProductClickEventHandler);
                        lbltablename.Click += new EventHandler(ProductClickEventHandler);
                        tlp_Gallery_Box.Click += new EventHandler(ProductClickEventHandler);

                        pictureBox.MouseHover += new EventHandler(ProductMousehoverEventHandler);
                        lbltablename.MouseHover += new EventHandler(ProductMousehoverEventHandler);
                        tlp_Gallery_Box.MouseHover += new EventHandler(ProductMousehoverEventHandler);

                        tlp_gallery_sub.Controls.Add(tlp_Gallery_Box, columnIndex, rowIndex);
                        columnIndex++;
                    }
                    tlp_Gallery_Box.BorderStyle = BorderStyle.FixedSingle;
                    tlp_Gallery_Box.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    tlp_Gallery_Main.Dock = DockStyle.Fill;
                    tlp_gallery_sub.Dock = DockStyle.Fill;
                    tlp_menu_Gallery_main.Dock = DockStyle.Fill;
                    tlp_gallery_sub.AutoScroll = true;
                    tlp_menu_Gallery_main.Controls.Add(tlp_gallery_sub, 0, 1);
                }
                else
                    CMessageBox.Show("No Products For This Category");

            }
            catch (Exception ex)
            {

            }

        }

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            if (txtsearch.Text == "Food Name .....")
            {
                txtsearch.Text = "";
                txtsearch.ForeColor = Color.Black;
            }
        }

        private void txtsearch_Leave(object sender, EventArgs e)
        {
            if (txtsearch.Text == "")
            {
                txtsearch.Text = "Food Name .....";
                txtsearch.ForeColor = Color.LightGray;
            }
        }

        private void btn_refreshlist_Click(object sender, EventArgs e)
        {
            GalleryView(0);
        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtpid, txtprice, txtproductname, txtSgstPrcnt, txtCgstPrcnt, txtmrp, txtDiscAmnt, txtDisPer, txtdes, txtsearch, txtqty, txtitemcode })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

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
            // Set the Region property of the textbox to the new path
            textBox.Region = new Region(path);
        }

        #endregion

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBoxChanged = true;
            GalleryView(Convert.ToInt32(cmbSearchlist.SelectedValue));
        }

        private void txtproductname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
        }

        private void txtitemcode_Leave(object sender, EventArgs e)
        {
            if (txtitemcode.Text != "")
            {
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblProduct";
                objSearch.WhereNdGroupBy = "Where status=1 and ItemCode = '" + txtitemcode.Text + "' ";// and BranchId = " + branch; ; ;
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    CMessageBox.Show(itemSource[0].ItemCode + " Already Exist on " + itemSource[0].Product, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtitemcode.Text = "";
                    txtitemcode.Focus();
                }
            }
        }
        #region Grid Flow Fuction
        private int GetNextVisibleColumn(int currentColumnIndex)
        {
            try
            {
                for (int i = currentColumnIndex + 1; i < dgv_registartion.ColumnCount; i++)
                {
                    if (dgv_registartion.Columns[i].Visible)
                    {
                        return i;
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        private int GetNextVisibleRow(int currentRowIndex)
        {
            try
            {
                for (int i = currentRowIndex + 1; i < dgv_registartion.RowCount; i++)
                {
                    if (dgv_registartion.Rows[i].Visible)
                    {
                        return i;
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }
        private int GetFirstVisibleColumn()
        {
            try
            {
                for (int i = 0; i < dgv_registartion.ColumnCount; i++)
                {
                    if (dgv_registartion.Columns[i].Visible)
                    {
                        return i;
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }
        #endregion
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    if (dgv_registartion.Focused)
                    {
                        try
                        {
                            if (dgv_registartion.Focused)
                            {
                                int currentRowIndex = dgv_registartion.CurrentCell.RowIndex;
                                int currentColumnIndex = dgv_registartion.CurrentCell.ColumnIndex;

                                int nextColumnIndex = GetNextVisibleColumn(currentColumnIndex);

                                if (nextColumnIndex != -1)
                                {
                                    dgv_registartion.CurrentCell = dgv_registartion[nextColumnIndex, currentRowIndex];
                                }
                                else
                                {
                                    int nextRowIndex = GetNextVisibleRow(currentRowIndex);
                                    if (nextRowIndex != -1)
                                    {
                                        dgv_registartion.CurrentCell = dgv_registartion[GetFirstVisibleColumn(), nextRowIndex];
                                    }
                                    else
                                    {
                                        btnsave.Focus();
                                    }
                                }

                            }
                        }

                        catch (Exception ex)
                        {

                        }
                    }
                    else
                        Movefocus(1);
                    return true; 
                case Keys.Enter:
                    if (dgv_registartion.Focused)
                    {
                        try
                        {
        
                                int currentRowIndex = dgv_registartion.CurrentCell.RowIndex;
                                int currentColumnIndex = dgv_registartion.CurrentCell.ColumnIndex;

                                int nextColumnIndex = GetNextVisibleColumn(currentColumnIndex);

                                if (nextColumnIndex != -1)
                                {
                                    dgv_registartion.CurrentCell = dgv_registartion[nextColumnIndex, currentRowIndex];
                                }
                                else
                                {
                                    int nextRowIndex = GetNextVisibleRow(currentRowIndex);
                                    if (nextRowIndex != -1)
                                    {
                                        dgv_registartion.CurrentCell = dgv_registartion[GetFirstVisibleColumn(), nextRowIndex];
                                    }
                                    else
                                    {
                                        btnsave.Focus();
                                    }
                                }

                            
                        }

                        catch (Exception ex)
                        {

                        }
                    }
                    else
                        Movefocus(0);
                    return true; 
                case Keys.Escape:
                    if (pnl_menupic.Visible)
                        pnl_menupic.Visible = false;
                    if(tlp_menu_Gallery_main.Visible)
                    {
                        tlp_menu_Gallery_main.Visible = false;
                        tlp_registartion_toggle.Visible = true;
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
            if (txtproductname.Focused)
            {
                txtitemcode.Focus();
            }
            else if (txtitemcode.Focused)
            {
                txtprice.Focus();
            }
            else if (txtprice.Focused)
            {
                txtmrp.Focus();
            }
            else if (txtmrp.Focused)
            {
                CmbCategory.Focus();
            }
            else if (CmbCategory.Focused)
            {
                txtDisPer.Focus();
            }
            //else if (txtqty.Focused)
            //{
            //    txtDisPer.Focus();
            //}
            else if (txtDisPer.Focused)
            {
                txtDiscAmnt.Focus();
            }
            else if (txtDiscAmnt.Focused)
            {
                cmbtax.Focus();
            }
            else if (cmbtax.Focused)
            {
                cmbTaxType.Focus();
            }
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
                txtdes.Focus();
            else if (cmbVatPrcnt.Focused)
                txtdes.Focus();
            else if (txtdes.Focused)
            {
                dgv_registartion.Focus();
                dgv_registartion.CurrentCell = dgv_registartion[0, 0];
            }
                
            else if(dgv_registartion.Focused)
            {
                int currentRowIndex = dgv_registartion.CurrentCell.RowIndex;
                int currentColumnIndex = 4;
                if (currentColumnIndex == 4 && currentRowIndex == dgv_registartion.RowCount - 1)
                    btnsave.Focus();                
            }
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

        private void dgv_registartion_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgv_registartion_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgv_registartion.CurrentCell.ColumnIndex == Rqtyno)
            {
                e.Control.KeyPress -= new KeyPressEventHandler(dgvProduct_KeyPress);
                System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(dgvProduct_KeyPress);
                }
            }
        }
        private void dgvProduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgv_registartion.CurrentCell.ColumnIndex == Rqtyno )
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
        }

        private void txtproductname_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtproductname.Text != "" && (txtpid.Text == "" || Convert.ToInt32(txtpid.Text) == 0))
                {
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "ProductID Field1 ";
                    objSearch.Table = "tblProduct WHERE Status = 1 AND Product = '" + txtproductname.Text + "'  UNION SELECT ItemId Field1 FROM tblItem ";
                    objSearch.WhereNdGroupBy = "Where status=1 and Itemname = '" + txtproductname.Text + "'";
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        CMessageBox.Show(txtproductname.Text + " Name Already Exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtproductname.Text = "";
                        txtproductname.Focus();
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }


        private void textBox_KeyPress_variation(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (e.KeyChar == '\r')
            {
                e.Handled = true; 
                return;
            }
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && textBox.Text.Contains('.'))
            {
                e.Handled = true;
                return;
            }
            int maxLength = textBox.Text.Contains('.') ? 7 : 4;
            if (textBox.Text.Length >= maxLength && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }
        private void pb_addon_Click(object sender, EventArgs e)
        {
            try
            {
               if (MandatoryCheck_Addon(rowindex))
                //{
                tlp_regform_grid.VerticalScroll.Visible = false;
                tlp_regform_grid.HorizontalScroll.Visible = true;

                //tlp_variation.SuspendLayout();
                tlp_variation.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                // Add TableLayoutPanel inside the panel
                TableLayoutPanel tlp_row_layout = new TableLayoutPanel();
                tlp_row_layout.Name = "tlp_row_layout" + rowindex;
                tlp_row_layout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                tlp_row_layout.Dock = DockStyle.Top;
                tlp_row_layout.Height = 65;
                tlp_row_layout.RowCount = 1;
                tlp_row_layout.ColumnCount = 2;
                tlp_row_layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 95));
                tlp_row_layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5));

                TableLayoutPanel tlp_left = new TableLayoutPanel();
                tlp_left.Name = "tlp_left_" + rowindex;
                //tlp_left.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                tlp_left.Dock = DockStyle.Fill;
                tlp_left.RowCount = 2;
                tlp_left.ColumnCount = 3;
                tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                //tlp_left.BackColor = Color.Red;
                //tlp_left.AutoScroll = true;
                // Add labels and controls
                string[] labels = { "Size", "Price", "Code" };
                for (int i = 0; i < 3; i++)
                {
                    Label label = new Label();
                    label.Name = labels[i] + rowindex;
                    label.Text = labels[i];
                    label.Dock = DockStyle.Fill;
                    label.ForeColor = System.Drawing.Color.Black;
                    label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold);
                    label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    Control control;

                    TextBox textBox = new TextBox();
                    textBox.Name = "txt_" + labels[i].ToLower() + rowindex;
                    textBox.Tag = rowindex;
                    textBox.Dock = DockStyle.Top;
                    textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12, System.Drawing.FontStyle.Regular);
                    textBox.BorderStyle = BorderStyle.None;
                    textBox.BackColor = System.Drawing.Color.FromArgb(196, 223, 223);
                    textBox.Multiline = true;
                    textBox.Height = 30;
                    if (i == 0)
                    {
                        textBox.AccessibleDescription = "Enter Variation Type..";
                    }
                    else if (i == 1)
                    {
                        textBox.AccessibleDescription = "Enter Price..";
                        textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress_variation);
                    }
                    textBox.Leave += TextBox_Leave;
                    textBox.Enter += TextBox_Enter;

                    control = textBox;
                    string tempname = control.Name;
                    TextBoxList.Add(tempname);

                    tlp_left.Controls.Add(label, i, 0);
                    tlp_left.Controls.Add(control, i, 1);

                }

                PictureBox pb_delrow = new PictureBox()
                {
                    Name = "Rowdel_" + rowindex,
                    Image = Properties.Resources.delete,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Dock = DockStyle.None,
                    Tag = rowindex,
                    AccessibleDescription = "",
                };

                pb_delrow.Click += pb_delrow_Click;

                tlp_row_layout.Controls.Add(tlp_left, 0, 0);
                tlp_row_layout.Controls.Add(pb_delrow, 1, 0);

                //#region Grid Inseration
                //int prov_rowindex = rowindex - 1;
                //string sizename = "txt_size" + prov_rowindex;
                //TextBox txtsize = this.Controls.Find(sizename, true).FirstOrDefault() as TextBox;
                //string pricename = "txt_price" + prov_rowindex;
                //TextBox txtprice = this.Controls.Find(pricename, true).FirstOrDefault() as TextBox;
                //string codename = "txt_code" + prov_rowindex;
                //TextBox txtcode = this.Controls.Find(codename, true).FirstOrDefault() as TextBox;

                //dgv_variation.Rows.Add();
                //dgv_variation.Rows[dgv_variation.CurrentCell.RowIndex].Cells[Arowno].Value = prov_rowindex;

                //if (txtsize != null)
                //    dgv_variation.Rows[dgv_variation.CurrentCell.RowIndex].Cells[avariationno].Value = txtsize.Text;
                //if (txtprice != null)
                //    dgv_variation.Rows[dgv_variation.CurrentCell.RowIndex].Cells[apriceno].Value = txtprice.Text;
                //if (txtcode != null)
                //    dgv_variation.Rows[dgv_variation.CurrentCell.RowIndex].Cells[Acodeno].Value = txtcode.Text;
                //#endregion

                tlp_variation.Controls.Add(tlp_row_layout, 0, rowindex);
                string sizename = "txt_size" + rowindex;
                TextBox txtsize = this.Controls.Find(sizename, true).FirstOrDefault() as TextBox;
                rowindex++;
                tlp_variation.ResumeLayout();

                RoundAddonTextbox();
                tlp_left.Focus();
                txtsize.Focus();
                if (tlp_left.Controls.Count > 0)
                {
                    Control lastControl = tlp_left.Controls[tlp_left.Controls.Count - 1];
                    lastControl.Focus();
                }
                int c = tlp_variation.RowCount;
                //
                //}
            }
            catch (Exception ex)
            {

            }
        }

        
                

        private void TextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                int prov_rowindex = Convert.ToInt32(textBox.Tag);
                TextBox txtsize = this.Controls.Find("txt_size" + prov_rowindex, true).FirstOrDefault() as TextBox;
                TextBox txtprice = this.Controls.Find("txt_price" + prov_rowindex, true).FirstOrDefault() as TextBox;
                if ( (txtsize.Text == "Enter Variation Type.." || txtsize.Text == "" ) && Convert.ToInt32( txtsize.Tag) == Mandatory_Rowindex)
                {
                    txtsize.Text = "";
                    txtsize.ForeColor = Color.Black;
                    StopBlinking();
                }
                if ( (txtprice.Text == "Enter Price.." || txtprice.Text == "") && Convert.ToInt32(txtprice.Tag) == Mandatory_Rowindex)
                {
                    txtprice.Text = "";
                    txtprice.ForeColor = Color.Black;
                    StopBlinking();
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void TextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                 bool flagexit = true;
                TextBox textBox = (TextBox)sender;
                int prov_rowindex = Convert.ToInt32(textBox.Tag);
                for (int i = 0; i < dgv_variation.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dgv_variation.Rows[i].Cells[Arowno].Value) == prov_rowindex)
                    {
                        TextBox txtsize = this.Controls.Find("txt_size" + prov_rowindex, true).FirstOrDefault() as TextBox;
                        TextBox txtprice = this.Controls.Find("txt_price" + prov_rowindex, true).FirstOrDefault() as TextBox;
                        TextBox txtcode = this.Controls.Find("txt_code" + prov_rowindex, true).FirstOrDefault() as TextBox;
                        if (txtsize != null)
                            dgv_variation.Rows[i].Cells[avariationno].Value = txtsize.Text;
                        if (txtprice != null)
                        {
                            if (string.IsNullOrWhiteSpace(txtprice.Text))
                                dgv_variation.Rows[i].Cells[apriceno].Value = "0";
                            else
                                dgv_variation.Rows[i].Cells[apriceno].Value = txtprice.Text;
                        }

                        if (txtcode != null)
                            dgv_variation.Rows[i].Cells[Acodeno].Value = txtcode.Text;
                        Prev_rowindex = i;
                        flagexit = false;
                        break;
                    }
                }
                if (flagexit)
                {
                    int rowIndex = dgv_variation.Rows.Add();
                    dgv_variation.Rows[rowIndex].Cells[Arowno].Value = prov_rowindex;
                    TextBox txtsize = this.Controls.Find("txt_size" + prov_rowindex, true).FirstOrDefault() as TextBox;
                    TextBox txtprice = this.Controls.Find("txt_price" + prov_rowindex, true).FirstOrDefault() as TextBox;
                    TextBox txtcode = this.Controls.Find("txt_code" + prov_rowindex, true).FirstOrDefault() as TextBox;
                    if (txtsize != null)
                        dgv_variation.Rows[rowIndex].Cells[avariationno].Value = txtsize.Text;
                    if (txtprice != null)
                        dgv_variation.Rows[rowIndex].Cells[apriceno].Value = txtprice.Text;
                    if (txtcode != null)
                        dgv_variation.Rows[rowIndex].Cells[Acodeno].Value = txtcode.Text;
                }
            }
            catch (Exception ex)
            {

            }
    
        }

        private void SetRowBackgroundColor(TableLayoutPanel tableLayoutPanel, int rowIndex, Color color)
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                if (tableLayoutPanel.GetRow(control) == rowIndex)
                {
                    control.BackColor = color;
                }
            }
        }
        private void pb_delrow_Serach_Click(object sender, EventArgs e)
        {
            try
            {
                PictureBox clickedPictureBox = (PictureBox)sender;
                int id = Convert.ToInt32(clickedPictureBox.Tag);
                var result = CMessageBox.Show("Do You want Delete This Variation", "Confirm The Action", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string tlpname = "tlp_row_layout" + clickedPictureBox.Tag;
                    TableLayoutPanel tlp_row_layout = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                    if (tlp_row_layout != null)
                    {
                        tlp_variation.Controls.Remove(tlp_row_layout);
                        tlp_row_layout.Dispose();

                        for (int i = 0; i < dgv_variation.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dgv_variation.Rows[i].Cells[Arowno].Value) == Convert.ToInt32(clickedPictureBox.Tag))
                            {
                                dgv_variation.Rows.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    if (dgv_variation.Rows.Count == 0)
                    {
                        chk_variation.Checked = false;
                    }
                    tlp_regform_grid.RowStyles[2].SizeType = SizeType.AutoSize;
                    tlp_regform_grid.RowStyles[3].SizeType = SizeType.AutoSize;

                    #region Deletion code
                    DmnAddonVariation dmnvariation = new DmnAddonVariation();
                    BllVariation blladdon = new BllVariation();
                    dmnvariation.Pavid = Convert.ToInt32(clickedPictureBox.AccessibleDescription);
                    dmnvariation.CreatedBy = FrmDashboard.UserId;
                    dmnvariation.BranchId = FrmDashboard.BranchId;
                    dmnvariation.Status = 0;
                    var usertype = blladdon.Delete_Variation_Data("prd_delete_Variation", dmnvariation);
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }

        }
        
        private void pb_delrow_Click(object sender, EventArgs e)
        {
            try
            {
                PictureBox clickedPictureBox = (PictureBox)sender;
                int id = Convert.ToInt32(clickedPictureBox.Tag);
                string tlpname = "tlp_row_layout" + clickedPictureBox.Tag;
                TableLayoutPanel tlp_row_layout = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                if (tlp_row_layout != null)
                {
                    tlp_variation.Controls.Remove(tlp_row_layout);
                    tlp_row_layout.Dispose();

                    for (int i = 0; i < dgv_variation.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dgv_variation.Rows[i].Cells[Arowno].Value) == Convert.ToInt32(clickedPictureBox.Tag))
                        {
                            dgv_variation.Rows.RemoveAt(i);
                            break;
                        }
                    }
                }
                //if (id < dgv_variation.Rows.Count)
                //{
                //    tlp_variation.RowStyles.RemoveAt(id);
                //}

                if (dgv_variation.Rows.Count == 0)
                {
                    chk_variation.Checked = false;
                }

                tlp_regform_grid.RowStyles[2].SizeType = SizeType.AutoSize;
                tlp_regform_grid.RowStyles[3].SizeType = SizeType.AutoSize;
            }
            catch (Exception ex)
            {

            }

        }
        public void RoundAddonTextbox()
        {
            try
            {               
                foreach (string textBoxName in TextBoxList)
                {
                    TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;
                    if (textBox != null)
                    {
                        ApplyRoundedTextBox(textBox, 10);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        public bool MandatoryCheck_Addon(int rowindex)
        {
            //int prov_rowindex = rowindex - 1;
            //string sizename = "txt_size" + prov_rowindex;
            //ComboBox cmbsize = this.Controls.Find(sizename, true).FirstOrDefault() as ComboBox;
            //string pricename = "txt_price" + prov_rowindex;
            //TextBox txtprice = this.Controls.Find(pricename, true).FirstOrDefault() as TextBox;
            //string codename = "txt_code" + prov_rowindex;
            //TextBox txtcode = this.Controls.Find(codename, true).FirstOrDefault() as TextBox;

            //if (string.IsNullOrWhiteSpace(txtprice.Text))
            //    txtprice.Text = "0";

            //if (Convert.ToInt32(cmbsize.SelectedValue) == -1)
            //{
            //    CMessageBox.Show("Please Select Variation", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    cmbsize.Focus();
            //    return false;
            //}
            //else if (txtprice.Text == "" || txtprice.Text == null || Convert.ToDecimal(txtprice.Text) <= 0)
            //{
            //    CMessageBox.Show("Please Enter Item Price", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtprice.Focus();
            //    return false;
            //}
            //else if (txtcode.Text == "" || txtcode.Text == null)
            //{
            //    CMessageBox.Show("Please Enter Code", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtcode.Focus();
            //    return false;
            //}           
            return true;
        }


        private void chk_variation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(SerachFlag == false)
                {
                    if (chk_variation.Checked)
                    {
                        txtmrp.Enabled = false;
                        txtprice.Enabled = false;

                        txtmrp.Text = "0";
                        txtprice.Text = "0";

                        for (int i = 0; i < tlp_variation.RowStyles.Count; i++)
                        {
                            if (i != 0)
                            {
                                tlp_variation.RowStyles[i].SizeType = SizeType.AutoSize;
                            }
                        }

                        tlp_regform_grid.AutoScroll = true;
                        tlp_regform_grid.VerticalScroll.Visible = false;
                        tlp_regform_grid.HorizontalScroll.Visible = true;
                        tlp_regform_grid.HorizontalScroll.Maximum = 0;
                        tlp_regform_grid.AutoScrollMinSize = new Size(0, 50);

                        pb_addon.Visible = true;

                        tlp_variation.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                        tlp_variation.Dock = DockStyle.Top;

                        // Add TableLayoutPanel inside the panel
                        TableLayoutPanel tlp_row_layout = new TableLayoutPanel();
                        tlp_row_layout.Name = "tlp_row_layout" + rowindex;
                        //tlp_row_layout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                        tlp_row_layout.Dock = DockStyle.Fill;
                        tlp_row_layout.RowCount = 1;
                        tlp_row_layout.ColumnCount = 2;
                        tlp_row_layout.Height = 65;
                        tlp_row_layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 95));
                        tlp_row_layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5));

                        foreach (Control control in tlp_variation.Controls)
                        {
                            if (control is TableLayoutPanel tlp_row_layout_tem)
                            {
                                tlp_row_layout_tem.RowStyles[0].SizeType = SizeType.Percent;
                                tlp_row_layout_tem.RowStyles[0].Height = 65;
                            }
                        }

                        TableLayoutPanel tlp_left = new TableLayoutPanel();
                        tlp_left.Name = "tlp_left_" + rowindex;
                        //tlp_left.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                        tlp_left.Dock = DockStyle.Top;
                        tlp_left.RowCount = 2;
                        tlp_left.ColumnCount = 3;
                        tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                        tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                        tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                        //tlp_left.BackColor = Color.Red;

                        // Add labels and controls
                        string[] labels = { "Size", "Price", "Code" };
                        for (int i = 0; i < 3; i++)
                        {
                            Label label = new Label();
                            label.Name = labels[i] + rowindex;
                            label.Text = labels[i];
                            label.Dock = DockStyle.Fill;
                            label.ForeColor = System.Drawing.Color.Black;
                            label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold);
                            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                            Control control;

                            TextBox textBox = new TextBox();
                            textBox.Name = "txt_" + labels[i].ToLower() + rowindex;
                            textBox.Tag = rowindex;
                            textBox.Dock = DockStyle.Top;
                            textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12, System.Drawing.FontStyle.Regular);
                            textBox.BorderStyle = BorderStyle.None;
                            textBox.BackColor = System.Drawing.Color.FromArgb(196, 223, 223);
                            textBox.Multiline = true;
                            textBox.Height = 30;
                            if (i == 0)
                            {
                                textBox.AccessibleDescription = "Enter Variation Type..";

                            }
                            else if (i == 1)
                            {
                                textBox.AccessibleDescription = "Enter Price..";
                                textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress_variation);
                            }
                            textBox.Leave += TextBox_Leave;
                            textBox.Enter += TextBox_Enter;


                            control = textBox;
                            string tempname = control.Name;
                            TextBoxList.Add(tempname);

                            tlp_left.Controls.Add(label, i, 0);
                            tlp_left.Controls.Add(control, i, 1);

                        }

                        #region Delete Icon
                        PictureBox pb_delrow = new PictureBox()
                        {
                            Name = "Rowdel_" + rowindex,
                            Image = Properties.Resources.delete,
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Dock = DockStyle.None,
                            Tag = rowindex,
                            AccessibleDescription = "",
                        };
                        pb_delrow.Click += pb_delrow_Click;

                        tlp_row_layout.Controls.Add(tlp_left, 0, 0);
                        tlp_row_layout.Controls.Add(pb_delrow, 1, 0);

                        #endregion

                        tlp_variation.Controls.Add(tlp_row_layout, 0, rowindex);

                        #region Grid insert
                        dgv_variation.Rows.Add();
                        int newIndex = dgv_variation.Rows.Count - 1;
                        dgv_variation.Rows[newIndex].Cells[Arowno].Value = rowindex;
                        dgv_variation.Rows[newIndex].Cells[avariationno].Value = "";
                        dgv_variation.Rows[newIndex].Cells[apriceno].Value = "0";
                        dgv_variation.Rows[newIndex].Cells[Acodeno].Value = "";
                        #endregion

                        rowindex++;
                        RoundAddonTextbox();
                        if (tlp_left.Controls.Count > 0)
                        {
                            Control lastControl = tlp_left.Controls[tlp_left.Controls.Count - 1];
                            lastControl.Focus();
                        }

                    }
                    else
                    {
                        txtmrp.Enabled = true;
                        txtprice.Enabled = true;
                        pb_addon.Visible = false;
                        for (int i = 0; i < tlp_variation.RowStyles.Count; i++)
                        {
                            if (i != 0)
                            {
                                tlp_variation.SuspendLayout();
                                tlp_variation.RowStyles[i].Height = 0;
                                tlp_variation.RowStyles[i].SizeType = SizeType.Absolute;
                                tlp_variation.ResumeLayout();
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void chk_addons_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_addons.Checked)
                {
                    cmb_addon_cate.Visible = true;
                    flp_addonlist.Visible = true;
                    tlp_regform_grid.AutoScroll = true;
                    tlp_regform_grid.VerticalScroll.Visible = false;
                    tlp_regform_grid.HorizontalScroll.Visible = true;
                    tlp_regform_grid.HorizontalScroll.Maximum = 0;
                    tlp_regform_grid.AutoScrollMinSize = new Size(0, 50);

                    tlp_addons.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    tlp_addons.Dock = DockStyle.Top;

                    flp_addonlist.Name = "flp_addonlist";
                    flp_addonlist.Dock = DockStyle.Top;
                    flp_addonlist.AutoSize = true;

                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "t.Aid Field1,t.AddonName Field2";
                    objSearch.Table = "tblAddon t LEFT JOIN tblCategory_FTI c ON C.Fcid = T.Fcid ";
                    objSearch.WhereNdGroupBy = "Where c.status=1 and t.status=1 ";
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        if(flp_addonlist.Controls.Count!= itemSource.Count)
                        {
                            foreach (var item in itemSource)
                            {
                                CheckBox checkbox = new CheckBox();
                                checkbox.Text = item.Field2;
                                checkbox.Name = "chkaddon_" + item.Field1;
                                checkbox.Tag = item.Field1;
                                checkbox.Font = new System.Drawing.Font("Arial", 13, System.Drawing.FontStyle.Regular);
                                //checkbox.BackColor = Color.Yellow;
                                checkbox.Margin = new Padding(10,20,0,20);
                                checkbox.CheckedChanged += Checkbox_CheckedChanged;
                                flp_addonlist.Controls.Add(checkbox);
                            }
                        }                      
                    }
                    if (!tlp_addons.Controls.Contains(flp_addonlist))
                    {
                        tlp_addons.Controls.Add(flp_addonlist, 0, 1);
                    }
                    flp_addonlist.Focus();
                }
                else
                {
                    cmb_addon_cate.Visible = false;
                    flp_addonlist.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cmb_addon_cate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                flp_addonlist.Controls.Clear();
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "t.Aid Field1,t.AddonName Field2";
                objSearch.Table = "tblAddon t LEFT JOIN tblCategory_FTI c ON C.Fcid = T.Fcid ";

                if (Convert.ToInt32(cmb_addon_cate.SelectedValue)==-1)
                    objSearch.WhereNdGroupBy = "Where t.status=1 and c.status=1 ";
                else
                    objSearch.WhereNdGroupBy = "Where t.status=1 and  t.fcid= " + cmb_addon_cate.SelectedValue;

                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    if (flp_addonlist.Controls.Count != itemSource.Count)
                    {
                        foreach (var item in itemSource)
                        {
                            CheckBox checkbox = new CheckBox();
                            checkbox.Text = item.Field2;
                            checkbox.Name = "chkaddon_" + item.Field1;
                            checkbox.Tag = item.Field1;
                            checkbox.Font = new System.Drawing.Font("Arial", 13, System.Drawing.FontStyle.Regular);
                           // checkbox.BackColor = Color.Yellow;
                            checkbox.Margin = new Padding(10, 20, 0, 20);
                            checkbox.CheckedChanged += Checkbox_CheckedChanged;

                            if (Checked_Addon_List.Any(lisdata => lisdata.Id == Convert.ToInt32(item.Field1)))
                            {
                                checkbox.Checked = true;
                            }

                            flp_addonlist.Controls.Add(checkbox);
                        }
                    }

                }

                if (!tlp_addons.Controls.Contains(flp_addonlist))
                {
                    tlp_addons.Controls.Add(flp_addonlist, 0, 1);
                }
                flp_addonlist.Focus();
            }
            catch(Exception ex)
            {

            }
        }
        private void Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            try 
            {
                CheckBox clickedCheckBox = (CheckBox)sender;
                string name = clickedCheckBox.Name;
                int fieldValue = Convert.ToInt32(clickedCheckBox.Tag);
                if (clickedCheckBox.Checked)
                {
                    if (!Checked_Addon_List.Any(item => item.Id == Convert.ToInt32(clickedCheckBox.Tag)))
                    {
                        Checked_Addon_List.Add(new CAddonlist
                        {
                            Id = Convert.ToInt32(clickedCheckBox.Tag),
                            Pavid = 0,
                            Cnames = clickedCheckBox.Text,
                            Status = 1
                        });
                    }
                }
                else
                {
                    CAddonlist itemToRemove = Checked_Addon_List.FirstOrDefault(item => item.Id == Convert.ToInt32(clickedCheckBox.Tag));
                    int indexToRemove = Checked_Addon_List.IndexOf(itemToRemove);
                    if (Convert.ToInt32( Checked_Addon_List[indexToRemove].Pavid)>0 && itemToRemove != null)
                    {
                        Checked_Addon_List[indexToRemove].Status = 0;
                    }
                    else
                    {
                        Checked_Addon_List.Remove(itemToRemove);
                    }
                }
            }
            catch(Exception ex)
            {

            }
      
        }


        //private void dgv_variation_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        //{

        //    //try
        //    //{
        //    //    validatingInProgress = true;

        //    //    if (dgv_variation.Rows[Prev_rowindex].Cells[Arowno].Value != "" && dgv_variation.Rows[Prev_rowindex].Cells[Arowno].Value != null)
        //    //    {
        //    //        int r = Convert.ToInt32(dgv_variation.Rows[Prev_rowindex].Cells[Arowno].Value);
        //    //        string sizename = "txt_size" + r;
        //    //        TextBox txtsize = this.Controls.Find(sizename, true).FirstOrDefault() as TextBox;
        //    //        string pricename = "txt_price" + r;
        //    //        TextBox txtprice = this.Controls.Find(pricename, true).FirstOrDefault() as TextBox;

        //    //        DataGridViewTextBoxCell cell = dgv_variation[avariationno, Prev_rowindex] as DataGridViewTextBoxCell;
        //    //        if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == string.Empty)
        //    //        {
        //    //            CMessageBox.Show("Please Enter Variation Type", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    //            txtsize.Focus();
        //    //            e.Cancel = true;
        //    //        }

        //    //        DataGridViewTextBoxCell pricecell = dgv_variation[apriceno, Prev_rowindex] as DataGridViewTextBoxCell;
        //    //        if (pricecell.EditedFormattedValue.ToString() == "" || pricecell.EditedFormattedValue.ToString() == "." || pricecell.EditedFormattedValue.ToString() == string.Empty || pricecell.EditedFormattedValue.ToString() == "0")
        //    //        {
        //    //            txtprice.Enabled = false;
        //    //            CMessageBox.Show("Please Enter Price", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    //            e.Cancel = true;
        //    //            txtprice.Focus();
        //    //            txtprice.Enabled = true;
        //    //            return;

        //    //        }
        //    //    }
        //    //}
        //    //finally
        //    //{
        //    //    validatingInProgress = false;
        //    //}
        //}

        public bool MandatoryCheck_variation()
        {
            if(chk_variation.Checked)
            {
                Color redcolor = ColorTranslator.FromHtml("#FF474D");

                for (int i = 0; i < dgv_variation.RowCount; i++)
                {
                    int r = Convert.ToInt32(dgv_variation.Rows[i].Cells[Arowno].Value);
                    Mandatory_Rowindex = r;
                    string sizename = "txt_size" + r;
                    TextBox txtsize = this.Controls.Find(sizename, true).FirstOrDefault() as TextBox;
                    string pricename = "txt_price" + r;
                    TextBox txtprice = this.Controls.Find(pricename, true).FirstOrDefault() as TextBox;

                    if (Convert.ToString(dgv_variation.Rows[i].Cells[avariationno].Value) == null || Convert.ToString(dgv_variation.Rows[i].Cells[avariationno].Value) == "")
                    {
                        txtsize.ForeColor = redcolor;
                        txtsize.Text = "Enter Variation Type..";
                        TextBox textBoxToBlink = txtsize;
                        CMessageBox.Show("Please Enter Variation Type", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        StartBlinking(textBoxToBlink);
                        return false;
                    }

                    if (Convert.ToString(dgv_variation.Rows[i].Cells[apriceno].Value) == "" || Convert.ToDecimal(dgv_variation.Rows[i].Cells[apriceno].Value) <= 0)
                    {
                        txtprice.ForeColor = redcolor;
                        TextBox textBoxToBlink = txtprice;
                        txtprice.Text = "Enter Price..";
                        CMessageBox.Show("Please Enter Price", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        StartBlinking(textBoxToBlink);
                        return false;
                    }
                }
              
            }
              return true;
        }

        private void StartBlinking(TextBox textBox)
        {
            blinkTimer = new Timer();
            blinkTimer.Interval = 500; 
            blinkTimer.Tick += (sender, e) =>
            {
                if (textBox.Text == "")
                    textBox.Text = textBox.AccessibleDescription; 
                else
                    textBox.Text = "";
            };
            blinkTimer.Start(); 
        }

        private void StopBlinking()
        {
            if (blinkTimer != null)
            {
                blinkTimer.Stop(); 
                blinkTimer.Dispose();
                blinkTimer = null;
            }
        }

        public void FillAddon()
        {
            try
            {
                flp_addonlist.Controls.Clear();
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "t.Aid Field1,t.AddonName Field2";
                objSearch.Table = "tblAddon t LEFT JOIN tblCategory_FTI c ON C.Fcid = T.Fcid ";
                objSearch.WhereNdGroupBy = "Where t.status=1 and c.status=1 ";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    if (flp_addonlist.Controls.Count != itemSource.Count)
                    {
                        foreach (var item in itemSource)
                        {
                            CheckBox checkbox = new CheckBox();
                            checkbox.Text = item.Field2;
                            checkbox.Name = "chkaddon_" + item.Field1;
                            checkbox.Tag = item.Field1;
                            checkbox.Font = new System.Drawing.Font("Arial", 13, System.Drawing.FontStyle.Regular);
                            checkbox.Margin = new Padding(10, 20, 0, 20);
                            checkbox.CheckedChanged += Checkbox_CheckedChanged;
                            flp_addonlist.Controls.Add(checkbox);
                        }
                    }

                }
                if (!tlp_addons.Controls.Contains(flp_addonlist))
                {
                    tlp_addons.Controls.Add(flp_addonlist, 0, 1);
                }
                flp_addonlist.Visible = false; 
            }
            catch (Exception ex)
            {

            }
        }
    }
}
