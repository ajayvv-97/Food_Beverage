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
using static FoodandBeverage.Interfaces.Interfaces;

namespace FoodandBeverage.Forms
{
    public partial class FrmIngredient : Form
    {
        public FrmIngredient()
        {
            InitializeComponent();
        }
        #region
        public static int Rslno = 0;
        public static int Ritemno = 1;
        public static int Rqtyno = 2;
        public static int Runitno = 3;
        public static int Raction = 4;
        public static int RIng_DetailsIdno = 5;
        public static int Ritemidno = 6;
        #endregion
        public string Compressedimg = Application.StartupPath + "\\CompressedImg";
        ClsDocument clsDocument = new ClsDocument();
        public static string empImgExt = "";
        TableLayoutPanel tlp_Gallery_Main = new TableLayoutPanel();
        TableLayoutPanel tlp_Gallery_Box = new TableLayoutPanel();
        TableLayoutPanel tlp_gallery_sub = new TableLayoutPanel();
        ToolTip toolTip_gallery = new  ToolTip();
        private bool comboBoxChanged = false;
        private Color originalBackColor; // Variable to store the original background color
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {
                    Dmnincrediant status = new Dmnincrediant();
                    DmnProducts prostatus = new DmnProducts();
                    Dmnincrediant dmnincr = new Dmnincrediant();
                    Bllincrediant bllincr = new Bllincrediant();

                    DmnProducts dmnproduct = new DmnProducts();
                    BllProducts  bllProduct = new BllProducts();                   
                    
                    dmnproduct.ProductId = Convert.ToInt32(txtpid.Text);
                    dmnproduct.Product = Convert.ToString(txtproductname.Text);
                    dmnproduct.Price = Convert.ToDecimal(txtprice.Text);
                    dmnproduct.Tax= Convert.ToString(cmbtax.Text);
                    if (Convert.ToInt32(cmbTaxType.SelectedIndex)==0)
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
                    dmnproduct.Quantity = Convert.ToInt32(txtqty.Text);
                    dmnproduct.Description = Convert.ToString(txtdes.Text);
                    dmnproduct.ItemCode = Convert.ToString(txtitemcode.Text);
                    dmnproduct.UPC = "";
                    dmnproduct.CreatedBy = 1;
                    dmnproduct.BranchId = 1;

                    if (chb_active.Checked)
                        dmnproduct.Status = 1;
                    else
                        dmnproduct.Status = 0;
                    prostatus = bllProduct.AddEditData("prdInsert_Upd_Product", dmnproduct);

                    if(prostatus.ProductId > 0)
                    {
                        for (int i = 0; i < dgv_registartion.RowCount; i++)
                        {
                            if (((dgv_registartion.Rows[i].Cells[Ritemidno].Value) != null || (dgv_registartion.Rows[i].Cells[Ritemidno].Value) != "") &&
                                ((dgv_registartion.Rows[i].Cells[Rqtyno].Value) != "" || (Convert.ToDecimal(dgv_registartion.Rows[i].Cells[Rqtyno].Value)) <= 0)
                                )

                            dmnincr.ProductId = Convert.ToInt32(prostatus.ProductId);
                            dmnincr.Ing_DetailsId = Convert.ToInt32(dgv_registartion.Rows[i].Cells[RIng_DetailsIdno].Value);
                            dmnincr.ItemId = Convert.ToInt32(dgv_registartion.Rows[i].Cells[Ritemidno].Value);
                            dmnincr.quantity = Convert.ToDecimal(dgv_registartion.Rows[i].Cells[Rqtyno].Value);
                            dmnincr.unit = Convert.ToInt32(dgv_registartion.Rows[i].Cells[Runitno].Value);
                            // dmnincr.Mid = Convert.ToInt32(cmb_menu.SelectedValue);
                            dmnincr.CreatedBy = 1;
                            dmnincr.BranchId = 1;

                            if (chb_active.Checked)
                                dmnincr.Status = 1;
                            else
                                dmnincr.Status = 0;
                            status = bllincr.AddData("prdInsert_upd_Incrediant", dmnincr);
                        }
                    }
                      

                    if (status.ItemId > 0)
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
            else if (txtprice.Text == "" || txtprice.Text == null || Convert.ToDecimal(txtprice.Text) <= 0)
            {
                CMessageBox.Show("Please Enter Item Price", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtprice.Focus();
                return false;
            }
            else if (txtmrp.Text == "" || txtmrp.Text == null || Convert.ToDecimal(txtmrp.Text) <= 0)
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
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState= FormWindowState.Minimized;
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
            }
            catch (Exception ex)
            {

            }
        }
        public void rearrange_slno()
        {
            for(int i=0;i< dgv_registartion.RowCount;i++)
            {
                dgv_registartion.Rows[i].Cells[Rslno].Value = i+1;
            }
        }

        private void dgv_registartion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Raction)
            {
                if (e.RowIndex == dgv_registartion.Rows.Count - 1)
                {
                    if (IsValidData(e.RowIndex))
                        return;
                    dgv_registartion.Rows.Add();
                    rearrange_slno();
                }
                else
                {
                    if (IsValidData(e.RowIndex))
                        return;
                 
                }
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
            if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == "." || cell.EditedFormattedValue.ToString() == string.Empty )
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

        
        private void dgv_registartion_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (e.RowIndex == dgv_registartion.Rows.Count - 1)
                    e.Value = Properties.Resources.add_row;
                else
                    e.Value = Properties.Resources.edit_row;
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
             
        public void FillData(int Pid)
        {
            try 
            {
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
                    objSearchDetails.Select = "id.ItemId Field1,i.Itemname Field2,id.quantity Field3,u.uid Field4,id.Ing_DetailsId Field5";
                    objSearchDetails.Table = "tblProduct p LEFT JOIN tblIngredientDetails id on id.ProductId=p.ProductId left join tblUnit u on u.UId=id.Unit left join tblItem i on i.ItemId=id.ItemId";
                    objSearchDetails.WhereNdGroupBy = "where p.status=1 and  p.ProductId= " + Pid;
                    objSearchDetails.OrderBy = "";
                    var gridSource_pro = bllSearchdetails.GetSearchData("prdSearch", objSearchDetails);
                    dgv_registartion.Rows.Clear();
                    dgv_registartion.Rows.Add(gridSource_pro.Count);
                    for (int i = 0; i < gridSource_pro.Count; i++)
                    {
                        dgv_registartion.Rows[i].Cells[Ritemidno].Value = Convert.ToInt32(gridSource_pro[i].Field1);      
                        dgv_registartion.Rows[i].Cells[Ritemno].Value = Convert.ToString(gridSource_pro[i].Field2);
                        dgv_registartion.Rows[i].Cells[Rqtyno].Value = Convert.ToDecimal(gridSource_pro[i].Field3);
                        dgv_registartion.Rows[i].Cells[Runitno].Value = Convert.ToInt64(gridSource_pro[i].Field4);
                        dgv_registartion.Rows[i].Cells[RIng_DetailsIdno].Value = Convert.ToInt32(gridSource_pro[i].Field5);

                    }
                    rearrange_slno();
                }
            }
            catch(Exception ex)
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
            int[] rowsToRemove = {  5,6,7,8 }; 

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
                tlp_registration.Controls.Add(btn_del_ingre, 1,8);

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
                tlp_registration.Controls.Add(btn_del_ingre, 1, 8);

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
                    if (string.IsNullOrWhiteSpace(txtmrp.Text))
                        txtmrp.Text = "0";
                    decimal discountAmnt = Convert.ToDecimal(txtDisPer.Text.Trim()) * Convert.ToDecimal(txtmrp.Text) / 100;
                    txtDiscAmnt.Text = discountAmnt.ToString("N2");
                }
            }
        }
        public void discountPer()
        {
            if (txtDiscAmnt.Text != "" && txtmrp.Text != "" && Convert.ToDecimal(txtmrp.Text) != 0)
            {
                if (Convert.ToDecimal(txtDiscAmnt.Text) > Convert.ToDecimal(txtmrp.Text))
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
            if(comboBoxChanged)
            {
                comboBoxChanged = false;
                if (cmbSearchlist.SelectedIndex==0)
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
                    string info = gridSource[i].Product + "," + gridSource[i].Price + "," + gridSource[i].MRP + "," + gridSource[i].Tax + "," + gridSource[i].TaxType + "," + gridSource[i].DiscountPer+"," + gridSource[i].DiscountAmt;

                    TableLayoutPanel tlp_Gallery_Box = new TableLayoutPanel();
                    PictureBox pictureBox = new PictureBox();
                    Label lbltablename = new Label();

                    // Configure the PictureBox
                    string imageName =Convert.ToString(gridSource[i].ProductId)+".jpeg"; 
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
                    pictureBox.Tag= info;

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
                    tlp_Gallery_Box.Tag =info;
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
                tlp_menu_Gallery_main.Controls.Add(tlp_gallery_sub,0,1);
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
                    CMessageBox.Show("Clicked Table ID: " + pid);
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
            tlp_menu_Gallery_main.Dock = DockStyle.Fill;
            Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_menu_Gallery_main, 1, 0);
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
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
            else if (e.KeyData == Keys.F8 && dgv_registartion.CurrentCell.ColumnIndex == Ritemno)
            {
                int cno = dgv_registartion.CurrentCell.ColumnIndex;
                int rowno = dgv_registartion.CurrentCell.RowIndex;
                ItemFillGrid(rowno);
            }
            else if (e.KeyData == Keys.Delete)
            {
                dgv_registartion.Rows.RemoveAt(dgv_registartion.CurrentCell.RowIndex);
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
                _objSearch.WhereNdGroupBy = "where i.Status = 1 ";
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
            catch (Exception ex)
            {
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            int returnValue=0;
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
                if (returnValue >0)
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
            catch(Exception ex)
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
    }
}
