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
using System.Runtime.InteropServices;

namespace FoodandBeverage.Forms
{
    public partial class FrmIngredient : Form
    {
        public FrmIngredient()
        {
            InitializeComponent();
        }

        #region Grid Declaration
        public  int Rslno = 0;
        public  int Ritemno = 1;
        public  int Rqtyno = 2;
        public  int Runitno = 3;
        public  int Raction = 4;
        public  int RIng_DetailsIdno = 5;
        public  int Ritemidno = 6;

        public  int Arowno = 0;
        public  int avariationno = 1;
        public  int apriceno = 2;
        public  int Acodeno = 3;
        public  int Avidno = 4;
        public  int Apidno = 5;
        public  int Aconsqty = 6;


        #endregion

        public string Compressedimg = Application.StartupPath + "\\CompressedImg";

        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsgen = new ClsGeneral();
        
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
            public decimal price { get; set; }
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

                    if (Convert.ToInt32(cmbpre_type.SelectedIndex) == 0)
                        dmnproduct.PreparationMode = Convert.ToInt32(PreparationType.kitchen);
                    else
                        dmnproduct.PreparationMode = Convert.ToInt32(PreparationType.Counter);


                    if (rblimited.Checked)
                        dmnproduct.IsStockTracked = true;
                    else
                        dmnproduct.IsStockTracked = false;

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
                        if(flp_variation_data.Controls.Count>0)
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
                                    dmnvariation.ConsumedQty = Convert.ToDecimal(dgv_variation.Rows[i].Cells[Aconsqty].Value);
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

                        #region  Addon Table Inseration DB
                        if (Checked_Addon_List.Count>0)
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
                                dmnvariation.Price = Listdata.price;
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
                            ClsImgComprossor.CompressImage(empImgExt, Compressedimg, 30);

                            if (FrmDashboard.downloadServiceValue == Convert.ToInt32(Interfaces.DownloadService_Type.DB))
                            {
                                #region  Employee photo upload in Database
                                if (pb_view_pic.BackgroundImage != null)
                                {
                                    Bitmap bitmap = new Bitmap(Compressedimg + "\\Image.jpeg");
                                    int pPid = Convert.ToInt32(txtPpid.Text);
                                    int Emp_branchid = Convert.ToInt32(FrmDashboard.BranchId);
                                    int Upstatus = Convert.ToInt32(1);
                                    int Emp_userid = Convert.ToInt32(FrmLogin.UserId);
                                    int ins_type = Convert.ToInt32(Interfaces.Inseration_Type.Product_Photo);
                                    clsDocument.UploadImage_Database(bitmap, pPid, ins_type, Convert.ToInt32(prostatus.ProductId), Emp_branchid, Upstatus, Emp_userid);
                                }
                                #endregion
                            }                            
                            else
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page btnsave_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        public void clearall()
        {
            try
            {
                rowindex = 1;
                txtsearch.Text = "Food Name.....";
                txtsearch.ForeColor = Color.Gray;

                pb_view_pic.BackgroundImage = null;

                //pb_menupic.BackgroundImage = null;
                //pb_menupic.BackgroundImage = Properties.Resources.FoodPic;

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
                flp_variation_data.SuspendLayout();
                flp_variation_data.Controls.Clear();   
                flp_variation_data.ResumeLayout();
                dgv_variation.Rows.Clear();
                dgv_variation.Refresh();

                pnlvariation.Visible = false;
                pnl_addon.Visible = false;
                #endregion

                chb_active.Checked = true;
                chk_variation.Checked = false;
                chk_addons.Checked = false;
                SerachFlag = false;

                txtmrp.Enabled = true;
                txtprice.Enabled = true;
                tlp_registration.Focus();
                txtproductname.Focus();
                txtPpid.Text = "0";
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
            catch(Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page clearall", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
            
        }

        public bool MandatoryCheck()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtpid.Text))
                    txtpid.Text = "0";
                if (string.IsNullOrWhiteSpace(txtqty.Text))
                    txtqty.Text = "0";
                if (string.IsNullOrWhiteSpace(txtDiscAmnt.Text))
                    txtDiscAmnt.Text = "0";
                if (string.IsNullOrWhiteSpace(txtDisPer.Text))
                    txtDisPer.Text = "0";
                if (string.IsNullOrWhiteSpace(txtmrp.Text))
                    txtmrp.Text = "0";
                if (string.IsNullOrWhiteSpace(txtprice.Text))
                    txtprice.Text = "0";
                if (string.IsNullOrWhiteSpace(txtproductname.Text))
                {
                    CMessageBox.Show(this, "Please Enter Item Name", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtproductname.Focus();
                    return false;
                }
                //else if ((string.IsNullOrWhiteSpace(txtprice.Text) || Convert.ToDecimal(txtprice.Text) <= 0) && flp_variation_data.Controls.Count==0)
                //{
                //    CMessageBox.Show(this, "Please Enter Item Price", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtprice.Focus();
                //    return false;
                //}
                //else if ((string.IsNullOrWhiteSpace(txtmrp.Text) || Convert.ToDecimal(txtmrp.Text) <= 0) && flp_variation_data.Controls.Count == 0)
                //{
                //    CMessageBox.Show(this, "Please Enter MRP", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtmrp.Focus();
                //    return false;
                //}
                else if (Convert.ToInt32(CmbCategory.SelectedValue) == -1)
                {
                    CMessageBox.Show(this, "Please Select Food Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CmbCategory.Focus();
                    return false;
                }
                
                else if (Convert.ToInt32(cmbpre_type.SelectedValue) == -1)
                {
                    CMessageBox.Show(this, "Please Select Preparation Mode", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbpre_type.Focus();
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(cmbtax.Text))
                {
                    CMessageBox.Show(this, "Please Select Tax", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbtax.Focus();
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(cmbTaxType.Text))
                {
                    CMessageBox.Show(this, "Please Select Tax Type GST or VAT", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbTaxType.Focus();
                    return false;
                }
                else if (cmbTaxType.Text == "GST")
                {
                    if (Convert.ToInt32(cmbIgstPrcnt.SelectedValue) == -1)
                    {
                        CMessageBox.Show(this, "Please Select IGST %", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbIgstPrcnt.Focus();
                        return false;
                    }
                }
                else if (cmbTaxType.Text == "VAT")
                {
                    if (Convert.ToInt32(cmbVatPrcnt.SelectedValue) == -1)
                    {
                        CMessageBox.Show(this, "Please Select VAT %", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbVatPrcnt.Focus();
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page MandatoryCheck", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
                return false;
            }
        }

        private void FrmIncrediant_Load(object sender, EventArgs e)
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
                txtproductname.Focus();
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product FrmIncrediant_Load", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page combofill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
                if (e.ColumnIndex == Raction && e.RowIndex>=0)
                {
                    if (e.RowIndex == dgv_registartion.Rows.Count - 1)
                    {
                        if (IsValidData(e.RowIndex))
                        {
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page dgv_registartion_CellContentClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }
        private bool IsValidData(int rowno)
        {
            try
            {
                DataGridViewTextBoxCell cell = dgv_registartion.Rows[rowno].Cells[Ritemno] as DataGridViewTextBoxCell;
                string selectedValue = cell?.EditedFormattedValue?.ToString();
                if (string.IsNullOrWhiteSpace(selectedValue) || selectedValue == ".")
                {
                    CMessageBox.Show("Please Select the item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgv_registartion.CurrentCell = dgv_registartion.Rows[rowno].Cells[Ritemno];
                    dgv_registartion.BeginEdit(false);
                    return false; // Changed from true to false based on expected behavior
                }
                DataGridViewTextBoxCell cell1 = dgv_registartion[Rqtyno, rowno] as DataGridViewTextBoxCell;
                string quantityValue = cell1?.EditedFormattedValue?.ToString();
                if (string.IsNullOrWhiteSpace(quantityValue) || quantityValue == "." || quantityValue == string.Empty)
                {
                    CMessageBox.Show("Please Enter the Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgv_registartion.CurrentCell = dgv_registartion.Rows[rowno].Cells[Rqtyno];
                    dgv_registartion.BeginEdit(false);
                    return false; // Changed from true to false based on expected behavior
                }

                DataGridViewComboBoxCell cell2 = dgv_registartion.Rows[rowno].Cells[Runitno] as DataGridViewComboBoxCell;
                string selectedValue2 = cell2?.EditedFormattedValue?.ToString();
                if (string.IsNullOrWhiteSpace(selectedValue2) || selectedValue2 == ".")
                {
                    CMessageBox.Show("Please Select the Unit Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgv_registartion.CurrentCell = dgv_registartion.Rows[rowno].Cells[Runitno];
                    dgv_registartion.BeginEdit(false);
                    return false; // Changed from true to false based on expected behavior
                }

                return true; // All data is valid
            }
            catch (Exception ex)
            {
                // Handle the exception gracefully
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product IsValidData", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
                return false; 
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
                clearall();
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

                    txtPpid.Text = Convert.ToString(gridSource[0].Ppid);

                    string imageName = Convert.ToString(gridSource[0].ProductId) + ".jpeg";
                    bool imageExist, brLogoExist;
                    if (imageName != "" && imageName != null)
                    {
                        if (FrmDashboard.downloadServiceValue == Convert.ToInt32(Interfaces.DownloadService_Type.FTP))
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
                        else if (FrmDashboard.downloadServiceValue == Convert.ToInt32(Interfaces.DownloadService_Type.DB))
                        {
                            int pPid = Convert.ToInt32(txtPpid.Text);
                            int ins_type = Convert.ToInt32(Inseration_Type.Product_Photo);
                            pb_view_pic.Image = clsDocument.DownloadImageDatabase(ins_type,pPid);
                            //pb_menupic.Image = clsDocument.DownloadImageDatabase(ins_type, pPid);
                        }

                    }

                    if (Convert.ToInt32(gridSource[0].PreparationMode)== Convert.ToInt32(PreparationType.kitchen))
                        cmbpre_type.SelectedIndex = 0;
                    else
                        cmbpre_type.SelectedIndex = 1;

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

                    int x = dgv_registartion.RowCount;
                    rearrange_slno();

                    if (dgv_registartion.RowCount > 0)
                    {
                        var lastRow = dgv_registartion.Rows[dgv_registartion.RowCount - 1];
                        bool isLastRowEmpty = lastRow.Cells[Ritemidno].Value == null || string.IsNullOrWhiteSpace(lastRow.Cells[Ritemidno].Value.ToString());
                        if (!isLastRowEmpty)
                        {
                            int newIndex = dgv_registartion.Rows.Add();
                            dgv_registartion.Rows[newIndex].Cells[Rslno].Value = dgv_registartion.RowCount; 
                        }
                    }
                    else
                    {
                        int newIndex = dgv_registartion.Rows.Add();
                        dgv_registartion.Rows[newIndex].Cells[Rslno].Value = 1;
                    }
                    DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_registartion.Rows[cnt].Cells[Raction];
                    cell1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    cell1.Value = Properties.Resources.add_row;

                    #region Variation and Addon
                    BllSearch<DmnGridFields> bllSearchdetails_VA = new BllSearch<DmnGridFields>();
                    DmnSearch objSearchDetails_VA = new DmnSearch();
                    objSearchDetails_VA.Select = "Pavid Field1,pav.Caption Field2,pav.Price Field3,pav.VariationCode Field4,a.AddonName Field5,pav.Type Field6,pav.Aid Field7,a.price Field8,pav.ConsumedQty Field9";
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

                                // ============================
                                // MAIN ROW PANEL (One row)
                                // ============================
                                TableLayoutPanel tlp_row_layout = new TableLayoutPanel();
                                tlp_row_layout.Name = "tlp_row_layout" + rowindex;

                                // FlowLayoutPanel friendly sizing
                                tlp_row_layout.AutoSize = true;
                                tlp_row_layout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                                tlp_row_layout.Margin = new Padding(10, 5, 10, 5);
                                tlp_row_layout.Padding = new Padding(0);

                                // MOST IMPORTANT — DO NOT USE Dock = Fill
                                tlp_row_layout.Dock = DockStyle.Fill;

                                // Make row fit full width of popup
                                tlp_row_layout.MaximumSize = new Size(flp_variation_data.Width - 40, 0);
                                tlp_row_layout.MinimumSize = new Size(flp_variation_data.Width - 40, 0);

                                tlp_row_layout.ColumnCount = 2;
                                tlp_row_layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 95));
                                tlp_row_layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5));

                                // ============================
                                // LEFT SIDE PANEL (Labels+Inputs)
                                // ============================
                                TableLayoutPanel tlp_left = new TableLayoutPanel();
                                tlp_left.Name = "tlp_left_" + rowindex;

                                tlp_left.AutoSize = true;
                                tlp_left.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                                tlp_left.Dock = DockStyle.Fill;

                                tlp_left.RowCount = 2;
                                tlp_left.ColumnCount = 4;
                                tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                                tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                                tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                                tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

                                // ============================
                                // Add Labels + Textboxes
                                // ============================
                                string[] labels = { "Size", "Price", "Code", "ConsumedQty" };

                                for (int i = 0; i < 4; i++)
                                {
                                    Label lbl = new Label();
                                    lbl.Name = labels[i] + rowindex;
                                    lbl.Text = labels[i];
                                    lbl.Dock = DockStyle.Fill;
                                    lbl.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                                    lbl.TextAlign = ContentAlignment.MiddleCenter;

                                    TextBox txt = new TextBox();
                                    txt.Name = "txt_" + labels[i].ToLower() + rowindex;
                                    txt.Tag = rowindex;
                                    txt.Dock = DockStyle.Fill;
                                    txt.Multiline = true;
                                    txt.Height = 30;
                                    txt.BorderStyle = BorderStyle.None;
                                    txt.BackColor = FrmDashboard.ContrlBGcolor;
                                    txt.Font = new Font("Microsoft Sans Serif", 12);

                                    // Assign values
                                    if (i == 0)
                                        txt.Text = gridSource_VA[j].Field2;    // Size
                                    else if (i == 1)
                                    {
                                        txt.Text = gridSource_VA[j].Field3;    // Price
                                        txt.KeyPress += new KeyPressEventHandler(textBox_KeyPress_variation);
                                    }
                                    else if (i == 2)
                                        txt.Text = gridSource_VA[j].Field4;// Code
                                    else
                                        txt.Text = gridSource_VA[j].Field9;// Consumed Qty
                                    // Events
                                    txt.Enter += TextBox_Enter;
                                    txt.Leave += TextBox_Leave;

                                    tlp_left.Controls.Add(lbl, i, 0);
                                    tlp_left.Controls.Add(txt, i, 1);
                                }

                                // ============================
                                // DELETE ICON
                                // ============================
                                PictureBox pb_delrow = new PictureBox()
                                {
                                    Name = "Rowdel_" + rowindex,
                                    Image = Properties.Resources.delete,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Dock = DockStyle.Fill,
                                    Tag = rowindex,
                                    AccessibleDescription = gridSource_VA[j].Field1
                                };
                                pb_delrow.Click += pb_delrow_Serach_Click;

                                // Add components to row layout
                                tlp_row_layout.Controls.Add(tlp_left, 0, 0);
                                tlp_row_layout.Controls.Add(pb_delrow, 1, 0);

                                // ============================
                                // ADD ROW TO FLOW LAYOUT PANEL
                                // ============================
                                flp_variation_data.Controls.Add(tlp_row_layout);

                                // ============================
                                // ADD ROW TO GRID
                                // ============================
                                dgv_variation.Rows.Add();
                                int newIndex = dgv_variation.Rows.Count - 1;
                                dgv_variation.Rows[newIndex].Cells[Arowno].Value = rowindex;
                                dgv_variation.Rows[newIndex].Cells[Avidno].Value = gridSource_VA[j].Field1;
                                dgv_variation.Rows[newIndex].Cells[avariationno].Value = gridSource_VA[j].Field2;
                                dgv_variation.Rows[newIndex].Cells[apriceno].Value = gridSource_VA[j].Field3;
                                dgv_variation.Rows[newIndex].Cells[Acodeno].Value = gridSource_VA[j].Field4;
                                dgv_variation.Rows[newIndex].Cells[Aconsqty].Value = gridSource_VA[j].Field9;

                                rowindex++;

                                RoundAddonTextbox();
                            }
                            else 
                            {
                                #region Adding checkbox Data to the List
                                Checked_Addon_List.Add(new CAddonlist
                                {
                                    Id = Convert.ToInt32(gridSource_VA[j].Field7),
                                    Pavid = Convert.ToInt32(gridSource_VA[j].Field1),
                                    Cnames = gridSource_VA[j].Field2,
                                    price= Convert.ToDecimal(gridSource_VA[j].Field8),
                                    Status = 1,

                                });
                                string chkname = "chkaddon_" + Convert.ToInt32(gridSource_VA[j].Field7);
                                CheckBox chkcontrol = this.Controls.Find(chkname, true).FirstOrDefault() as CheckBox;
                                if (chkcontrol != null)
                                    chkcontrol.Checked = true;
                                #endregion

                            }
                        }

                        if(flp_variation_data.Controls.Count>0)
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page FillData()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
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
                int startRow = 5;
                int endRow = 8;
                int totalColumns = 4; 

                for (int row = startRow; row <= endRow; row++)
                {
                    for (int col = 0; col < totalColumns; col++)
                    {
                        if (row == 5 && col == 0)
                            continue;
                        if (row == 6 && col == 0)
                            continue;
                        Control ctrl = tlp_registration.GetControlFromPosition(col, row);
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

                    ////row4
                    tlp_registration.Controls.Add(txtdes, 0, 8);
                    tlp_registration.Controls.Add(tlp_vari_addon, 1, 8);
                    tlp_registration.Controls.Add(btn_del_ingre, 2, 8);

                    txtdes.Dock = DockStyle.Fill;
                    ApplyRoundedTextBox(txtdes, 10);
                }
                else if (cmbTaxType.Text == "VAT")
                {
                    cmbVatPrcnt.Dock = DockStyle.Fill;
                    lblvat.Dock = DockStyle.Fill;

                    //row1
                    tlp_registration.Controls.Add(lbltax, 0, 5);
                    tlp_registration.Controls.Add(lbltaxtype, 1, 5);
                    tlp_registration.Controls.Add(lblvat, 2, 5);

                    //row2
                    tlp_registration.Controls.Add(cmbtax, 0, 6);
                    tlp_registration.Controls.Add(cmbTaxType, 1, 6);
                    tlp_registration.Controls.Add(cmbVatPrcnt, 2, 6);

                    //row3
                    tlp_registration.Controls.Add(lbldes, 0, 7);

                    //row4
                    tlp_registration.Controls.Add(txtdes, 0, 8);    
                    tlp_registration.Controls.Add(tlp_vari_addon, 1, 8);
                    tlp_registration.Controls.Add(btn_del_ingre, 2, 8);

                    txtdes.Dock = DockStyle.Fill;
                    ApplyRoundedTextBox(txtdes, 10);
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page cmbTaxType_SelectedValueChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
                clsgen.LogException("Product page cmbIgstPrcnt_SelectedIndexChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void txtDisPer_Leave(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page txtDisPer_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
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
            try
            {
                if (txtDiscAmnt.Text != "" && txtmrp.Text != "" && Convert.ToDecimal(txtDiscAmnt.Text) != 0 && Convert.ToDecimal(txtmrp.Text) != 0)
                {
                    if (Convert.ToDecimal(txtDiscAmnt.Text) > Convert.ToDecimal(txtmrp.Text))
                    {
                        CMessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page txtDiscAmnt_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
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
            try
            {
                if (txtDisPer.Text != "")
                {
                    if (Convert.ToDecimal(txtDisPer.Text) > 100)
                    {
                        CMessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page discountAmnt()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        public void discountPer()
        {
            try
            {
                if (txtDiscAmnt.Text != "" && txtprice.Text != "" && Convert.ToDecimal(txtprice.Text) != 0)
                {
                    if (Convert.ToDecimal(txtDiscAmnt.Text) > Convert.ToDecimal(txtprice.Text))
                    {
                        CMessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDiscAmnt.Focus();
                    }
                    else
                    {
                        decimal discountPer = (Convert.ToDecimal(txtDiscAmnt.Text) / Convert.ToDecimal(txtmrp.Text) * 100);
                        txtDisPer.Text = discountPer.ToString("N2");
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page discountPer()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void pb_menupic_Click(object sender, EventArgs e)
        {
            try
            {
                if ((empImgExt != "" && empImgExt != null) || Convert.ToInt32(txtPpid.Text) > 0)
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

                        //pb_menupic.BackgroundImage = Image.FromFile(openFileDialog.FileName);
                        //pb_menupic.BackgroundImageLayout = ImageLayout.Stretch;
                        //pb_menupic.Image = null;

                        pb_view_pic.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page pb_menupic_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }



        private void btn_clear_pic_Click(object sender, EventArgs e)
        {
            try
            {
                empImgExt = "";
                pb_view_pic.BackgroundImage = null;
                pb_view_pic.Image = FoodandBeverage.Properties.Resources.no_pictures;
                pb_view_pic.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page btn_clear_pic_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void btn_change_pic_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page btn_change_pic_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        #region gallery creation
        public void GalleryView(int serachmode)
        {
            try
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
                        lbltablename.Tag = info;

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
                clsgen.LogException("Product page GalleryView()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }


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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page ProductMousehoverEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page ProductClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
            try
            {
                tlp_registartion_toggle.Visible = false;
                tlp_menu_Gallery_main.Visible = true;

                Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
                tlp_main.Controls.Remove(controlToRemove);
                tlp_main.Controls.Add(tlp_menu_Gallery_main, 1, 0);
                tlp_menu_Gallery_main.Dock = DockStyle.Fill;
                GalleryView(0);
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page Btn_view_menu_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void btn_back_reg_Click(object sender, EventArgs e)
        {
            try
            {
                tlp_menu_Gallery_main.Visible = false;
                tlp_registartion_toggle.Visible = true;

                Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
                tlp_main.Controls.Remove(controlToRemove);
                tlp_main.Controls.Add(tlp_registartion_toggle, 1, 0);
                tlp_registartion_toggle.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page btn_back_reg_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }


        private void btn_close_pic_Click(object sender, EventArgs e)
        {
            pnl_menupic.Visible = false;
        }

        private void txtmrp_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtmrp.Text))
                    txtmrp.Text = "0";
                if (string.IsNullOrWhiteSpace(txtprice.Text))
                    txtprice.Text = "0";

                if (Convert.ToDecimal(txtmrp.Text) < Convert.ToDecimal(txtprice.Text))
                {
                    CMessageBox.Show(this, "MRP Rate Must be Greater than Sale Price", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmrp.Focus();
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page txtmrp_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
            try
            {
                var result = CMessageBox.Show(this, "Do You want Delete This Record", "Confirm The Action", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    chb_active.Checked = false;
                    btnsave.PerformClick(); 
                    clearall(); 
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page btn_del_ingre_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
            try
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
            catch( Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page dgv_registartion_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page ItemFillGrid()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
                _objSearch.ColumnHeaders = "ID,Item Code,Product,";
                _objSearch.Select = "ProductId as Id,Itemcode as Field1,Product as Field2";
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page btnSearch_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page btn_Searchitem_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
                foreach (TextBox textBox in new TextBox[] { txtpid, txtprice, txtproductname, txtSgstPrcnt, txtCgstPrcnt, txtmrp, txtDiscAmnt, txtDisPer, txtdes, txtsearch, txtqty, txtitemcode })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
                // Set the Region property of the textbox to the new path
                textBox.Region = new Region(path);
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        #endregion

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBoxChanged = true;
            GalleryView(Convert.ToInt32(cmbSearchlist.SelectedValue));
        }

        private void txtproductname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')           
                e.Handled = true; 
        }



        private void txtitemcode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtitemcode.Text != "" && Convert.ToInt32(txtpid.Text) == 0)
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page GetNextVisibleColumn()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page GetNextVisibleColumn()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
            try
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

                else if (dgv_registartion.Focused)
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page Movefocus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page txtproductname_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }
        private void textBox_KeyPress_variation(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page textBox_KeyPress_variation()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }
        private void pb_addon_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck_Addon(rowindex))
                {
                    // MAIN ROW PANEL (inside FlowLayoutPanel)
                    TableLayoutPanel tlp_row_layout = new TableLayoutPanel();
                    tlp_row_layout.Name = "tlp_row_layout" + rowindex;
                    tlp_row_layout.AutoSize = false;
                    tlp_row_layout.Width = flp_variation_data.Width - 25; // FILL WIDTH
                    tlp_row_layout.MinimumSize = new Size(flp_variation_data.Width - 25, 70);
                    tlp_row_layout.Height = 70;
                    tlp_row_layout.Margin = new Padding(0, 5, 0, 5);
                    tlp_row_layout.ColumnCount = 2;
                    tlp_row_layout.RowCount = 1;

                    tlp_row_layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 93));
                    tlp_row_layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7));

                    // LEFT SECTION (labels + textboxes)
                    TableLayoutPanel tlp_left = new TableLayoutPanel();
                    tlp_left.Name = "tlp_left_" + rowindex;
                    tlp_left.AutoSize = false;
                    tlp_left.Width = tlp_row_layout.Width - 60;
                    tlp_left.MinimumSize = new Size(tlp_row_layout.Width - 60, 70);
                    tlp_left.Height = 70;
                    tlp_left.Dock = DockStyle.Fill;

                    tlp_left.ColumnCount = 4;
                    tlp_left.RowCount = 2;

                    tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                    tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                    tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                    tlp_left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));


                    // ADD LABELS + TEXTBOXES
                    string[] labels = { "Size", "Price", "Code", "ConsumedQty" };

                    for (int i = 0; i < 4; i++)
                    {
                        Label lbl = new Label();
                        lbl.Name = labels[i] + rowindex;
                        lbl.Text = labels[i];
                        lbl.Dock = DockStyle.Fill;
                        lbl.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                        lbl.TextAlign = ContentAlignment.MiddleCenter;

                        TextBox txt = new TextBox();
                        txt.Name = "txt_" + labels[i].ToLower() + rowindex;
                        txt.Tag = rowindex;
                        txt.Dock = DockStyle.Top;
                        txt.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                        txt.BorderStyle = BorderStyle.None;
                        txt.BackColor = FrmDashboard.ContrlBGcolor;
                        txt.Multiline = true;
                        txt.Height = 32;

                        if (i == 0)
                        {
                            txt.AccessibleDescription = "Enter Variation Type..";
                        }
                        else if (i == 1)
                        {
                            txt.AccessibleDescription = "Enter Price..";
                            txt.KeyPress += new KeyPressEventHandler(textBox_KeyPress_variation);
                        }

                        txt.Leave += TextBox_Leave;
                        txt.Enter += TextBox_Enter;

                        TextBoxList.Add(txt.Name);

                        tlp_left.Controls.Add(lbl, i, 0);
                        tlp_left.Controls.Add(txt, i, 1);
                    }
                    PictureBox pb_delrow = new PictureBox()
                    {
                        Name = "Rowdel_" + rowindex,
                        Image = Properties.Resources.delete,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Dock = DockStyle.Fill,
                        Tag = rowindex
                    };
                    pb_delrow.Click += pb_delrow_Click;

                    tlp_row_layout.Controls.Add(tlp_left, 0, 0);
                    tlp_row_layout.Controls.Add(pb_delrow, 1, 0);
                    flp_variation_data.Controls.Add(tlp_row_layout);

                    TextBox txtsize = this.Controls
                        .Find("txt_size" + rowindex, true)
                        .FirstOrDefault() as TextBox;

                    rowindex++;

                    RoundAddonTextbox();
                    txtsize?.Focus();
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page pb_addon_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page TextBox_Enter()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
                        TextBox txtconsqty = this.Controls.Find("txt_consumedqty" + prov_rowindex, true).FirstOrDefault() as TextBox;
                        if (txtsize != null)
                            dgv_variation.Rows[i].Cells[avariationno].Value = txtsize.Text;
                        if (txtprice != null)
                        {
                            if (string.IsNullOrWhiteSpace(txtprice.Text))
                                dgv_variation.Rows[i].Cells[apriceno].Value = "0";
                            else
                                dgv_variation.Rows[i].Cells[apriceno].Value = txtprice.Text;
                        }
                        if (txtconsqty != null)
                            dgv_variation.Rows[i].Cells[Aconsqty].Value = txtconsqty.Text;

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
                    TextBox txtconsqty = this.Controls.Find("txt_consumedqty" + prov_rowindex, true).FirstOrDefault() as TextBox;

                    if (txtsize != null)
                        dgv_variation.Rows[rowIndex].Cells[avariationno].Value = txtsize.Text;
                    if (txtprice != null)
                        dgv_variation.Rows[rowIndex].Cells[apriceno].Value = txtprice.Text;
                    if (txtcode != null)
                        dgv_variation.Rows[rowIndex].Cells[Acodeno].Value = txtcode.Text;
                    if (txtconsqty != null)
                        dgv_variation.Rows[rowIndex].Cells[Aconsqty].Value = txtconsqty.Text;
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page TextBox_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
                        //tlp_variation_data.Controls.Remove(tlp_row_layout);
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page pb_delrow_Serach_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void pb_delrow_Click(object sender, EventArgs e)
        {
            try
            {
                PictureBox pb = (PictureBox)sender;
                int rowId = Convert.ToInt32(pb.Tag);

                string tlpName = "tlp_row_layout" + rowId;

                TableLayoutPanel row = flp_variation_data.Controls
                    .OfType<TableLayoutPanel>()
                    .FirstOrDefault(x => x.Name == tlpName);

                if (row != null)
                {
                    flp_variation_data.Controls.Remove(row);
                    row.Dispose();
                }

                foreach (DataGridViewRow dgRow in dgv_variation.Rows)
                {
                    if (Convert.ToInt32(dgRow.Cells[Arowno].Value) == rowId)
                    {
                        dgv_variation.Rows.Remove(dgRow);
                        break;
                    }
                }
                if (dgv_variation.Rows.Count == 0)
                {
                    chk_variation.Checked = false;
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page pb_delrow_Click()", ex,
                                    FrmLogin.EmployeName, FrmLogin.Designation,
                                    Convert.ToInt32(FormType.Product));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page RoundAddonTextbox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
            
        }



        private void cmb_addon_cate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                flp_addonlist.Controls.Clear();
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "t.Aid Field1,t.AddonName Field2,t.price Field3";
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
                            checkbox.AccessibleDescription = item.Field3;
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

                flp_addonlist.Focus();
            }
            catch(Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page cmb_addon_cate_SelectionChangeCommitted()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
                            price = Convert.ToDecimal(clickedCheckBox.AccessibleDescription),
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page Checkbox_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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
            try
            {
                if (flp_variation_data.Controls.Count>0)
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
                        var cellValue = dgv_variation.Rows[i].Cells[avariationno]?.Value;

                        if (string.IsNullOrWhiteSpace(cellValue?.ToString()))
                        {
                            //txtsize.ForeColor = redcolor;
                            //txtsize.Text = "Enter Variation Type..";
                            //TextBox textBoxToBlink = txtsize;
                            //CMessageBox.Show("Please Enter Variation Type", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                           // StartBlinking(textBoxToBlink);
                            //return false;
                        }

                        if (Convert.ToString(dgv_variation.Rows[i].Cells[apriceno].Value) == "" || Convert.ToDecimal(dgv_variation.Rows[i].Cells[apriceno].Value) <= 0)
                        {
                           // txtprice.ForeColor = redcolor;
                           // TextBox textBoxToBlink = txtprice;
                           // txtprice.Text = "Enter Price..";
                           // CMessageBox.Show("Please Enter Price", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                           //// StartBlinking(textBoxToBlink);
                           // return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page MandatoryCheck_variation()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
                return false;
            }
        }


        private void StartBlinking(TextBox textBox)
        {
            try
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page StartBlinking()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }


        private void StopBlinking()
        {
            try
            {
                if (blinkTimer != null)
                {
                    blinkTimer.Stop();
                    blinkTimer.Dispose();
                    blinkTimer = null;
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page  StopBlinking()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page FillAddon()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void FrmIngredient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                AppSession.UnregisterForm(this);
            }
        }

        private void FrmIngredient_Resize(object sender, EventArgs e)
        {
            #region setting Corner Round 
            UIHelper.RoundCorners(tlp_registration, 25, null);
            //UIHelper.RoundCorners(tlp_variation, 25, null);
            UIHelper.RoundCorners(dgv_registartion, 25, null);
            UIHelper.RoundCorners(tlp_navbar, 10, null);
            UIHelper.RoundCorners(tlp_addon_navbar, 25, null);
            #endregion

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            bool flag = screenWidth > 1366 ? true : false;
            if (flag)
            {
                tlp_regform_grid.RowStyles[1] = new RowStyle(SizeType.Percent, 100);
            }
            else
            {
                tlp_regform_grid.RowStyles[1] = new RowStyle(SizeType.AutoSize);
            }
        }

        private void btnvariation_Click(object sender, EventArgs e)
        {
            try
            {

                pnl_variation_data.AutoScroll = true;
                pnl_variation_data.AutoSize = false;

                flp_variation_data.FlowDirection = FlowDirection.TopDown;
                flp_variation_data.WrapContents = false;
                flp_variation_data.AutoScroll = true;
                flp_variation_data.AutoSize = false;
                flp_variation_data.Dock = DockStyle.Fill;

                if (SerachFlag == false)
                {
                    if (btnvariation.Visible)
                    {
                        UIHelper.CenterAndResize(pnlvariation);
                        UIHelper.RoundCorners(tlp_vari_nav, 25, null);

                        txtmrp.Enabled = false;
                        txtprice.Enabled = false;

                        txtmrp.Text = "0";
                        txtprice.Text = "0";

                        pb_addon.Visible = true;

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

                        int con_count = flp_variation_data.Controls.Count;
                        if (con_count == 1)
                        {
                            #region New Variation Control adding

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
                            #endregion

                            tlp_row_layout.Controls.Add(tlp_left, 0, 0);
                            tlp_row_layout.Controls.Add(pb_delrow, 1, 0);

                            //tlp_variation_data.Controls.Add(tlp_row_layout, 0, rowindex);

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


                            #endregion
                        }
                    }
                    else
                    {
                        txtmrp.Enabled = true;
                        txtprice.Enabled = true;
                        //pb_addon.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page btnvariation_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void btnaddon_Click(object sender, EventArgs e)
        {
            try
            {
                UIHelper.CenterAndResize(pnl_addon);

                flp_addonlist.WrapContents = false;
                flp_addonlist.AutoScroll = true;
                flp_addonlist.AutoSize = false;
                flp_addonlist.Dock = DockStyle.Fill;

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "t.Aid Field1,t.AddonName Field2,t.Price Field3";
                objSearch.Table = "tblAddon t LEFT JOIN tblCategory_FTI c ON C.Fcid = T.Fcid ";
                objSearch.WhereNdGroupBy = "Where c.status=1 and t.status=1 ";
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
                            checkbox.AccessibleDescription = item.Field3;
                            checkbox.Font = new System.Drawing.Font("Arial", 13, System.Drawing.FontStyle.Regular);
                            //checkbox.BackColor = Color.Yellow;
                            checkbox.Margin = new Padding(10, 20, 0, 20);
                            checkbox.CheckedChanged += Checkbox_CheckedChanged;
                            flp_addonlist.Controls.Add(checkbox);
                        }
                    }
                }
                flp_addonlist.Focus();

            }
            catch (Exception ex)
            {
                clsgen.LogException("Product page btnaddon_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void btnvaria_cls_Click(object sender, EventArgs e)
        {
            pnlvariation.Visible = false;
        }

        private void btn_vari_submit_Click(object sender, EventArgs e)
        {
            pnlvariation.Visible = false;
        }

        private void btn_addon_cls_Click(object sender, EventArgs e)
        {
            pnl_addon.Visible = false;
        }

        private void btn_addon_submit_Click(object sender, EventArgs e)
        {
            pnl_addon.Visible = false;
        }
        
        public void UploadImage_Database(Bitmap bitmap, int Pid, int inseration_type, int Id, int Emp_branchid, int status, int Emp_userid)
        {
            try
            {
                #region Convert Employee photo to binary
                byte[] imageData;
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageData = ms.ToArray();
                }

                //List<byte> rgb565Bytes = new List<byte>();

                //for (int y = 0; y < bitmap.Height; y++)
                //{
                //    for (int x = 0; x < bitmap.Width; x++)
                //    {
                //        Color color = bitmap.GetPixel(x, y);
                //        ushort rgb565 = (ushort)(((color.R & 0xF8) << 8) |
                //                                 ((color.G & 0xFC) << 3) |
                //                                 ((color.B & 0xF8) >> 3));
                //        rgb565Bytes.Add((byte)(rgb565 >> 8)); // High byte
                //        rgb565Bytes.Add((byte)(rgb565 & 0xFF)); // Low byte
                //    }
                //}
                //rgb565Bytes.ToArray();


                BllImageUpload bllimg = new BllImageUpload();
                DmnImageUpload dmnEmployeeph = new DmnImageUpload();
                dmnEmployeeph.PpId = Pid;
                dmnEmployeeph.ProductId = Id;
                dmnEmployeeph.BranchId = Emp_branchid;
                dmnEmployeeph.Status = status;
                dmnEmployeeph.CreatedBy = Emp_userid;
                dmnEmployeeph.Photo = imageData;
                // dmnEmployeeph.Photo = rgb565Bytes.ToArray();
                dmnEmployeeph.Inseration_Type = inseration_type;
                var resph = bllimg.UploadPhoto("prdInsert_Upd_Photo", dmnEmployeeph);
                #endregion
            }
            catch (Exception ex)
            {
            }
        }
    
        public Bitmap DownloadImageDatabase(int inseration_type, int Pid)
        {
            Bitmap bitmap = null;
            BllSearch<DmnImageUpload> bllSearchph = new BllSearch<DmnImageUpload>();
            DmnSearch objSearchph = new DmnSearch();

            switch (inseration_type)
            {
                case 1:
                    objSearchph.Select = "*";
                    objSearchph.Table = "Midas_Eikona.dbo.Employee_Photo";
                    objSearchph.WhereNdGroupBy = "Where status=1 and Epid = " + Pid;
                    var gridSourceph = bllSearchph.GetSearchData("prdSearch", objSearchph);
                    if (gridSourceph.Count > 0 && gridSourceph[0].Photo != null)
                    {
                        byte[] photoData = gridSourceph[0].Photo;

                        using (var mStream = new MemoryStream(photoData))
                        {
                            bitmap = new Bitmap(mStream);
                        }
                    }
                    break;
                case 2:
                    objSearchph.Select = "*";
                    objSearchph.Table = "Midas_Eikona.dbo.tblGame_photo";
                    objSearchph.WhereNdGroupBy = "Where status=1 and GameId = " + Pid;
                    var gridSourcegp = bllSearchph.GetSearchData("prdSearch", objSearchph);
                    if (gridSourcegp.Count > 0 && gridSourcegp[0].Photo != null)
                    {
                        byte[] photoData = gridSourcegp[0].Photo;

                        using (var mStream = new MemoryStream(photoData))
                        {
                            bitmap = new Bitmap(mStream);
                        }
                    }
                    break;
                case 3:
                    objSearchph.Select = "*";
                    objSearchph.Table = "Midas_Eikona.dbo.tblCustomer_Photo";
                    objSearchph.WhereNdGroupBy = "Where status=1 and Cpid = " + Pid;
                    var gridSourcecp = bllSearchph.GetSearchData("prdSearch", objSearchph);
                    if (gridSourcecp.Count > 0 && gridSourcecp[0].Photo != null)
                    {
                        byte[] photoData = gridSourcecp[0].Photo;

                        using (var mStream = new MemoryStream(photoData))
                        {
                            bitmap = new Bitmap(mStream);
                        }
                    }
                    break;

            }
            return bitmap;
        }
    }
}
