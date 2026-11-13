using BLL;
using CustomMessageBox;
using Dapper;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmIngredient : Form
    {
        public FrmIngredient()
        {
            InitializeComponent();
            ConfigureInventoryItemCategoryColumn();
            ConfigureIngredientUnitColumn();
            chk_PackagingRequired.CheckedChanged += chk_PackagingRequired_CheckedChanged;
            dgv_registartion.RowsAdded += dgv_registartion_RowsAdded;
            dgv_registartion.CellClick += dgv_registartion_ItemCellClick;
        }

        #region Decalartion

        #region Grid Declaration
        public int Rslno = 0;
        public int Ritemno = 1;
        public int Rqtyno = 2;
        public int Runitno = 3;
        public int RDeductionstageno = 4;
        public int RCategorytypeno = 5;
        public int Raction = 6;
        public int RIng_DetailsIdno = 7;
        public int Ritemidno = 8;

        public int Vslno = 0;
        public int Vactionno = 1;
        public int vvariationno = 2;
        public int vpriceno = 3;
        public int vqtyno = 4;
        public int vdisperno = 5;
        public int vdisamtno = 6;
        public int vproducttid = 7;
        public int vvariatioinidno = 8;
        public int vvariationtypeid = 9;

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

        public int tileH= Convert.ToInt32(ConfigurationManager.AppSettings["Productheight"]);
        public int tileW = Convert.ToInt32(ConfigurationManager.AppSettings["Productwidth"]);
        public int Productcorner = Convert.ToInt32(ConfigurationManager.AppSettings["CornerRadius"]);
        private bool isLoadingColor = false;
        #endregion

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {        
                dgv_registartion.EndEdit();
                if (MandatoryCheck() && ValidateIngredientGrid() && MandatoryCheck_variation())
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
                    dmnproduct.HSNcode = Convert.ToString(txthsncode.Text);
                    dmnproduct.Fcid = Convert.ToInt32(CmbCategory.SelectedValue);
                    dmnproduct.PackagingRequired = chk_PackagingRequired.Checked;

                    dmnproduct.HasVariation = variationEnabled;

                    dmnproduct.PreparationMode = cmbpre_type.SelectedIndex == 0
                        ? Convert.ToInt32(KOTRouting.kitchen)
                        : Convert.ToInt32(KOTRouting.Counter);

                    switch (cmb_FoodPreparationType.SelectedIndex)
                    {
                        case 0:
                            dmnproduct.FoodPreparationType = Convert.ToInt32(FoodPreparationType.MadetoOrder);
                            break;
                        case 1:
                            dmnproduct.FoodPreparationType = Convert.ToInt32(FoodPreparationType.BatchPrepared);
                            break;
                        case 2:
                            dmnproduct.FoodPreparationType = Convert.ToInt32(FoodPreparationType.PartiallyPrepared);
                            break;
                        default:
                            dmnproduct.FoodPreparationType = Convert.ToInt32(FoodPreparationType.NotApplicable);
                            break;
                    }

                    if (rblimited.Checked)
                        dmnproduct.IsStockTracked = true;
                    else
                        dmnproduct.IsStockTracked = false;

                    if (rb_veg.Checked)
                        dmnproduct.FoodType = 1;
                    else
                        dmnproduct.FoodType = 2;

                    if (chb_active.Checked)
                        dmnproduct.Status = 1;
                    else
                        dmnproduct.Status = 0;

                    // Convert Color to Hex
                    string foreHex = ColorTranslator.ToHtml(btnforcolor.BackColor);
                    string backHex = ColorTranslator.ToHtml(btnbgcolor.BackColor);

                    dmnproduct.ForeColor = foreHex;
                    dmnproduct.BackColor = backHex;

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
                                dmnincr.ProductType = chk_PackagingRequired.Checked
                                    ? GetInventoryItemCategoryId(dgv_registartion.Rows[i].Cells[RCategorytypeno].Value)
                                    : Convert.ToInt32(Interfaces.InventoryItemCategory.RawMaterial);
                                dmnincr.Deductionstage = cmb_FoodPreparationType.SelectedIndex == 2
                                    ? GetDeductionStageId(dgv_registartion.Rows[i].Cells[RDeductionstageno].Value)
                                    : 0;
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

                        #region Product Variation Persistence
                        SaveProductVariations(prostatus.ProductId);
                        #endregion

                        #region  Addon Table Inseration DB
                        //if (Checked_Addon_List.Count>0)
                        //{
                        //    foreach (CAddonlist Listdata in Checked_Addon_List)
                        //    {
                        //        DmnAddonVariation dmnvariation = new DmnAddonVariation();
                        //        BllVariation blladdon = new BllVariation();
                        //        dmnvariation.Pavid = Listdata.Pavid;
                        //        dmnvariation.Caption = Listdata.Cnames;
                        //        dmnvariation.VariationCode = "";
                        //        dmnvariation.Aid = Listdata.Id;
                        //        dmnvariation.Type = Convert.ToInt32(Product_Addon_Variation_Type.Addon);
                        //        dmnvariation.Price = Listdata.price;
                        //        dmnvariation.ProductId = Convert.ToInt32(prostatus.ProductId);
                        //        dmnvariation.ItemCode_UPC = Convert.ToString(txtitemcode.Text);
                        //        dmnvariation.CreatedBy = FrmDashboard.UserId;
                        //        dmnvariation.BranchId = FrmDashboard.BranchId;
                        //        dmnvariation.Status = Listdata.Status;
                        //        var usertype = blladdon.AddData("prdInsert_upd_AddonVariation", dmnvariation);
                        //        if (usertype.Pavid > 0)
                        //        {
                        //            varia_count = varia_count + 1;
                        //        }
                        //    }
                        //}
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
                txthsncode.Text = "";
                empImgExt = "";
                txtqty.Text = "";
                txtitemcode.Text = "";
                txtpid.Text = "0";
                chk_PackagingRequired.Checked = false;
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
                dgv_variation.Rows.Clear();
                dgv_variation.Refresh();
                pendingVariationDeletes.Clear();
                loadedVariationProductId = -1;
                variationEnabled = false;
                if (chkVariationEnabled != null)
                    chkVariationEnabled.Checked = false;

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


                txtitemcode.ReadOnly = false;
                txtDiscAmnt.ReadOnly = false;
                txtDisPer.ReadOnly = false;
                txtprice.ReadOnly = false;

                // Default values
                btnbgcolor.BackColor = Color.White;
                btnforcolor.BackColor = Color.Black;

                chk_bgcolor.Checked = false;
                chk_forecolor.Checked = false;

                chk_PackagingRequired.Checked = false;
                UpdateInventoryItemCategoryColumnVisibility();
                cmb_FoodPreparationType.SelectedIndex = 3;
                UpdatePreparationStageColumnVisibility();
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
                else if (cmb_FoodPreparationType.SelectedIndex < 0 ||
                         cmb_FoodPreparationType.SelectedIndex > 3)
                {
                    CMessageBox.Show(this,
                        "Please select a valid Food Preparation Type: MadeToOrder, BatchPrepared, PartiallyPrepared, or NotApplicable.",
                        "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmb_FoodPreparationType.Focus();
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

                textb_shape();
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

                objSearch.Select = "UId AS Id, UnitName AS Value";
                objSearch.Table = " tblUnit";
                objSearch.WhereNdGroupBy = "where status=1";
                objSearch.OrderBy = "";
                var gridSource1 = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_unit.DataSource = gridSource1;
                cmb_unit.ValueMember = "Id";
                cmb_unit.DisplayMember = "Value";
                cmb_unit.ValueType = typeof(long);

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
            return ValidateIngredientRow(rowno, false);
        }

        private bool ValidateIngredientGrid()
        {
            try
            {
                if (dgv_registartion.IsCurrentCellInEditMode)
                    dgv_registartion.EndEdit();

                for (int i = 0; i < dgv_registartion.Rows.Count; i++)
                {
                    if (dgv_registartion.Rows[i].IsNewRow)
                        continue;

                    if (!ValidateIngredientRow(i, true))
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Product page ValidateIngredientGrid()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
                CMessageBox.Show("Unable to validate ingredient details. Please review the ingredient rows and try again.",
                    "Ingredient Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private bool ValidateIngredientRow(int rowIndex, bool allowCompletelyEmptyRow)
        {
            try
            {
                if (rowIndex < 0 || rowIndex >= dgv_registartion.Rows.Count)
                    return false;

                DataGridViewRow row = dgv_registartion.Rows[rowIndex];
                if (row.IsNewRow)
                    return true;

                int itemId = 0;
                int.TryParse(Convert.ToString(row.Cells[Ritemidno].Value), out itemId);

                decimal quantity = 0;
                bool hasQuantity = decimal.TryParse(
                    Convert.ToString(row.Cells[Rqtyno].Value), out quantity);

                int unitId = 0;
                bool hasUnit = int.TryParse(
                    Convert.ToString(row.Cells[Runitno].Value), out unitId) && unitId > 0;

                bool quantityEntered = !string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[Rqtyno].Value));
                bool unitEntered = !string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[Runitno].Value));

                // Ignore the unused trailing row, but do not ignore partially completed rows.
                if (itemId <= 0 && !quantityEntered && !unitEntered && allowCompletelyEmptyRow)
                    return true;

                string itemName = Convert.ToString(row.Cells[Ritemno].Value);
                if (string.IsNullOrWhiteSpace(itemName) || itemName == "F8 for Products")
                    itemName = "Ingredient";

                if (itemId <= 0)
                    return ShowIngredientValidationError(rowIndex, Ritemno,
                        "Please select an ingredient in row " + (rowIndex + 1) + ".");

                if (!hasQuantity || quantity <= 0)
                    return ShowIngredientValidationError(rowIndex, Rqtyno,
                        "Enter a quantity greater than zero for '" + itemName + "' (row " + (rowIndex + 1) + ").");

                if (!hasUnit)
                    return ShowIngredientValidationError(rowIndex, Runitno,
                        "Please select a unit for '" + itemName + "' (row " + (rowIndex + 1) + ").");

                if (cmb_FoodPreparationType.SelectedIndex == 2)
                {
                    int deductionStageId;
                    bool validStage = int.TryParse(
                        Convert.ToString(row.Cells[RDeductionstageno].Value), out deductionStageId)
                        && deductionStageId >= 0 && deductionStageId <= 2;

                    if (!validStage)
                        return ShowIngredientValidationError(rowIndex, RDeductionstageno,
                            "Please select a deduction stage for '" + itemName +
                            "'. Choose Any, Order Preparation, or Advance Preparation.");
                }

                return true;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Product page ValidateIngredientRow()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
                CMessageBox.Show("Unable to validate ingredient row " + (rowIndex + 1) + ". Please check the item, quantity, and unit.",
                    "Ingredient Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private bool ShowIngredientValidationError(int rowIndex, int columnIndex, string message)
        {
            CMessageBox.Show(message, "Ingredient Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (rowIndex >= 0 && rowIndex < dgv_registartion.Rows.Count &&
                columnIndex >= 0 && columnIndex < dgv_registartion.Columns.Count)
            {
                dgv_registartion.CurrentCell = dgv_registartion.Rows[rowIndex].Cells[columnIndex];
                dgv_registartion.Focus();
                dgv_registartion.BeginEdit(true);
            }

            return false;
        }

        private bool IsActiveIngredientStatus(object value)
        {
            if (value == null || value == DBNull.Value)
                return false;

            int status;
            if (int.TryParse(Convert.ToString(value), out status))
                return status == 1;

            bool boolStatus;
            return bool.TryParse(Convert.ToString(value), out boolStatus) && boolStatus;
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
                    chk_PackagingRequired.Checked = gridSource[0].PackagingRequired;
                    UpdateInventoryItemCategoryColumnVisibility();

                    txtPpid.Text = Convert.ToString(gridSource[0].Ppid);

                    if (gridSource[0].IsStockTracked)
                        rblimited.Checked = true;
                    else
                        rbunlimted.Checked = true;

                    if (gridSource[0].FoodType == Convert.ToInt32(FoodType.Veg))
                        rb_veg.Checked = true;
                    else
                        rb_nonveg.Checked = true;

                    // PreparationMode controls KOT routing (Kitchen/Counter).
                    // It must never be reused as FoodPreparationType.
                    int foodPreparationType = Convert.ToInt32(gridSource[0].FoodPreparationType);
                    SetFoodPreparationTypeSelection(foodPreparationType);

                    txthsncode.Text = Convert.ToString(gridSource[0].HSNcode);

                    if (gridSource[0].HasVariation)
                    {
                        txtitemcode.ReadOnly = true;
                        txtDiscAmnt.ReadOnly = true;
                        txtDisPer.ReadOnly = true;
                        txtprice.ReadOnly = true;
                    }
                    else
                    {
                        txtitemcode.ReadOnly = false;
                        txtDiscAmnt.ReadOnly = false;
                        txtDisPer.ReadOnly = false;
                        txtprice.ReadOnly = false;
                    }

                    #region Color Loading

                    isLoadingColor = true;   

                    var item = gridSource.FirstOrDefault();

                    if (item != null)
                    {
                        if (!string.IsNullOrWhiteSpace(item.ForeColor))
                        {
                            btnforcolor.BackColor = ColorTranslator.FromHtml(item.ForeColor);
                            chk_forecolor.Checked = true;
                        }
                        else
                        {
                            chk_forecolor.Checked = false;
                            btnforcolor.BackColor = Color.Black;
                        }

                        if (!string.IsNullOrWhiteSpace(item.BackColor))
                        {
                            btnbgcolor.BackColor = ColorTranslator.FromHtml(item.BackColor);
                            chk_bgcolor.Checked = true;
                        }
                        else
                        {
                            chk_bgcolor.Checked = false;
                            btnbgcolor.BackColor = Color.White;
                        }
                    }

                    isLoadingColor = false;   

                    GeneratePreviewDesign();
                    #endregion


                    //string imageName = Convert.ToString(gridSource[0].ProductId) + ".jpeg";
                    //bool imageExist, brLogoExist;
                    //if (imageName != "" && imageName != null)
                    //{
                    //    if (FrmDashboard.downloadServiceValue == Convert.ToInt32(Interfaces.DownloadService_Type.FTP))
                    //    {
                    //        imageExist = clsDocument.FileExistOrNot(imageName, DocumentType.FoodPhoto);
                    //        if (imageExist)
                    //        {
                    //            pb_view_pic.BackgroundImage = clsDocument.DownloadImage(imageName, DocumentType.FoodPhoto);
                    //            pb_view_pic.BackgroundImageLayout = ImageLayout.Stretch;
                    //            pb_view_pic.Image = null;
                    //            empImgExt = imageName;
                    //        }
                    //    }
                    //    else if (FrmDashboard.downloadServiceValue == Convert.ToInt32(Interfaces.DownloadService_Type.DB))
                    //    {
                    //        int pPid = Convert.ToInt32(txtPpid.Text);
                    //        int ins_type = Convert.ToInt32(Inseration_Type.Product_Photo);
                    //        pb_view_pic.Image = clsDocument.DownloadImageDatabase(ins_type,pPid);
                    //        //pb_menupic.Image = clsDocument.DownloadImageDatabase(ins_type, pPid);
                    //    }

                    //}

                    if (Convert.ToInt32(gridSource[0].PreparationMode) == Convert.ToInt32(KOTRouting.kitchen))
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
                    objSearchDetails.Select = "id.ItemId Field1,i.Itemname Field2,id.quantity Field3,u.uid Field4,id.Ing_DetailsId Field5,id.status Field6,id.Deductionstage Field7,id.ProductType Field8 ";
                    objSearchDetails.Table = "tblProduct p LEFT JOIN tblIngredientDetails id on id.ProductId=p.ProductId left join tblUnit u on u.UId=id.Unit left join tblItem i on i.ItemId=id.ItemId";
                    objSearchDetails.WhereNdGroupBy = "where p.status=1 and  p.ProductId= " + Pid;
                    objSearchDetails.OrderBy = "";
                    var gridSource_pro = bllSearchdetails.GetSearchData("prdSearch", objSearchDetails);
                    dgv_registartion.Rows.Clear();

                    // Use the actual grid row returned by Rows.Add(). The source list can contain
                    // inactive detail rows, so its index is not the same as the visible grid index.
                    for (int i = 0; i < gridSource_pro.Count; i++)
                    {
                        var ingredient = gridSource_pro[i];
                        if (!IsActiveIngredientStatus(ingredient.Field6))
                            continue;

                        int itemId;
                        if (!int.TryParse(Convert.ToString(ingredient.Field1), out itemId) || itemId <= 0)
                            continue;

                        int rowIndex = dgv_registartion.Rows.Add();
                        DataGridViewRow row = dgv_registartion.Rows[rowIndex];

                        row.Cells[Ritemidno].Value = itemId;
                        row.Cells[Ritemno].Value = Convert.ToString(ingredient.Field2);
                        row.Cells[Rqtyno].Value = ingredient.Field3 == null
                            ? (object)null
                            : Convert.ToString(ingredient.Field3);

                        long unitId;
                        if (long.TryParse(Convert.ToString(ingredient.Field4), out unitId) && unitId > 0)
                            row.Cells[Runitno].Value = unitId;

                        int detailId;
                        if (int.TryParse(Convert.ToString(ingredient.Field5), out detailId))
                            row.Cells[RIng_DetailsIdno].Value = detailId;

                        row.Cells[RCategorytypeno].Value = chk_PackagingRequired.Checked
                            ? GetInventoryItemCategoryId(ingredient.Field8)
                            : Convert.ToInt32(Interfaces.InventoryItemCategory.RawMaterial);

                        int deductionStageId = GetDeductionStageId(ingredient.Field7);
                        if (cmb_FoodPreparationType.SelectedIndex == 2)
                            row.Cells[RDeductionstageno].Value = deductionStageId;

                        DataGridViewImageCell deleteCell = row.Cells[Raction] as DataGridViewImageCell;
                        if (deleteCell != null)
                        {
                            deleteCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            deleteCell.Value = Properties.Resources.delete;
                        }
                    }

                    // Keep one empty row at the end for adding the next ingredient.
                    int addRowIndex = dgv_registartion.Rows.Add();
                    dgv_registartion.Rows[addRowIndex].Cells[Rslno].Value = addRowIndex + 1;

                    rearrange_slno();

                    DataGridViewImageCell addCell =
                        dgv_registartion.Rows[addRowIndex].Cells[Raction] as DataGridViewImageCell;
                    if (addCell != null)
                    {
                        addCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        addCell.Value = Properties.Resources.add_row;
                    }

                    #region Fill Product Variation Data
                    variationEnabled = gridSource[0].HasVariation;
                    ConfigureProductVariationGrid();
                    LoadProductVariations(Pid);
                    loadedVariationProductId = Pid;
                    if (variationEnabled && dgv_variation.Rows.Count == 0)
                        AddProductVariationRow();
                    SetVariationEditorEnabled(variationEnabled);
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
        private void PlaceRegistrationControl(Control control, int column, int row)
        {
            try
            {
                if (control == null)
                    return;

                TableLayoutPanelCellPosition position = tlp_registration.GetPositionFromControl(control);
                if (control.Parent == tlp_registration &&
                    position.Column == column && position.Row == row)
                    return;

                tlp_registration.Controls.Add(control, column, row);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Product page PlaceRegistrationControl()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void cmbTaxType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                ControlArrangement();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Product page cmbTaxType_SelectedValueChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        public void ControlArrangement()
        {
            try
            {
                tlp_registration.SuspendLayout();
                for (int row = 5; row <= 9; row++)
                {
                    for (int column = 0; column < tlp_registration.ColumnCount; column++)
                    {
                        if ((row == 5 && column == 0) || (row == 5 && column == 1) || (row == 5 && column == 2))
                            continue;
                        if ((row == 6 && column == 0) || (row == 6 && column == 1) || (row == 6 && column == 2))
                            continue;

                        Control control = tlp_registration.GetControlFromPosition(column, row);
                        if (control != null)
                            tlp_registration.Controls.Remove(control);
                    }
                }

                lbltax.Dock = DockStyle.Fill;
                lbltaxtype.Dock = DockStyle.Fill;
                cmbtax.Dock = DockStyle.Fill;
                cmbTaxType.Dock = DockStyle.Fill;

                if (string.Equals(cmbTaxType.Text, "VAT", StringComparison.OrdinalIgnoreCase))
                {
                    lblvat.Dock = DockStyle.Fill;
                    cmbVatPrcnt.Dock = DockStyle.Fill;

                    tlp_registration.Controls.Add(lblvat, 3, 5);

                    tlp_registration.Controls.Add(cmbVatPrcnt, 3, 6);

                    tlp_registration.Controls.Add(lblfoodtype, 0, 7);
                    //tlp_registration.Controls.Add(, 1, 7);
                    //tlp_registration.Controls.Add(, 2, 6);
                    //tlp_registration.Controls.Add(, 3, 6);

                    tlp_registration.Controls.Add(tlp_foodtype, 0, 8);
                    tlp_registration.Controls.Add(chk_PackagingRequired, 1, 8);
                    tlp_registration.Controls.Add(btnvariation, 2, 8);
                    tlp_registration.Controls.Add(btn_del_ingre, 3, 8);

                    //tlp_registration.Controls.Add(, 0, 9);
                    //tlp_registration.Controls.Add(, 1, 8);
                    //tlp_registration.Controls.Add(, 2, 8);
                    //tlp_registration.Controls.Add(, 3, 8);
                }
                else
                {
                    lbligst.Dock = DockStyle.Fill;
                    cmbIgstPrcnt.Dock = DockStyle.Fill;
                    tlp_s_c_gst_lbl.Dock = DockStyle.Fill;
                    tlp_S_C_text.Dock = DockStyle.Fill;

                    tlp_registration.Controls.Add(lbligst, 3, 5);

                    tlp_registration.Controls.Add(cmbIgstPrcnt, 3, 6);

                    tlp_registration.Controls.Add(tlp_s_c_gst_lbl, 0, 7);
                    tlp_registration.Controls.Add(lblfoodtype, 1, 7);
                    //tlp_registration.Controls.Add(, 2, 6);
                    //tlp_registration.Controls.Add(, 3, 6);

                    tlp_registration.Controls.Add(tlp_S_C_text, 0, 8);
                    tlp_registration.Controls.Add(tlp_foodtype, 1, 8);
                    tlp_registration.Controls.Add(chk_PackagingRequired, 2, 8);
                    tlp_registration.Controls.Add(btnvariation, 3, 8);

                    tlp_registration.Controls.Add(btn_del_ingre, 0, 9);
                    //tlp_registration.Controls.Add(, 1, 8);
                    //tlp_registration.Controls.Add(, 2, 8);
                    //tlp_registration.Controls.Add(, 3, 8);

                    // lbldes txtdes
                }

                tlp_foodtype.Dock = DockStyle.Fill;
                tlp_variation.Dock = DockStyle.Fill;
                txtdes.Dock = DockStyle.Fill;

                ApplyRoundedTextBox(txtdes, 10);
                textb_shape();
                tlp_registration.ResumeLayout();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Product page ControlArrangement()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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


        private void dgv_registartion_ItemCellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex != Ritemno ||
                    e.RowIndex >= dgv_registartion.Rows.Count ||
                    dgv_registartion.Rows[e.RowIndex].IsNewRow)
                    return;

                ItemFillGrid(e.RowIndex);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Product page dgv_registartion_ItemCellClick()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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

                                // Always use tblItem.Baseunit for raw materials; do not let staff select
                                // a different unit. This keeps recipe quantities consistent with conversion.
                                dgv_registartion.Rows[rowno].Cells[Runitno].Value = Convert.ToInt64(gridSource1[0].Baseunit);
                                dgv_registartion.Rows[rowno].Cells[Runitno].ReadOnly = true;
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
                foreach (TextBox textBox in new TextBox[] { txthsncode, txtpid, txtprice, txtproductname, txtSgstPrcnt, txtCgstPrcnt, txtmrp, txtDiscAmnt, txtDisPer, txtdes, txtsearch, txtqty, txtitemcode })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception ex)
            {
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
                if (!variationEnabled)
                    return true;

                return ValidateProductVariationRows();
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Product page MandatoryCheck_variation()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
                return false;
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
            textb_shape();
        }

        private List<int> pendingVariationDeletes = new List<int>();
        private PictureBox variationBackdrop;
        private CheckBox chkVariationEnabled;
        private Button variationAddRowButton;
        private int loadedVariationProductId = -1;
        private bool variationEnabled = false;

        private void ShowVariationBackdrop()
        {
            RemoveVariationBackdrop();

            Bitmap captured = new Bitmap(ClientSize.Width, ClientSize.Height);
            DrawToBitmap(captured, new Rectangle(Point.Empty, ClientSize));

            int smallWidth = Math.Max(1, captured.Width / 18);
            int smallHeight = Math.Max(1, captured.Height / 18);
            using (Bitmap small = new Bitmap(smallWidth, smallHeight))
            using (Graphics g = Graphics.FromImage(small))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                g.DrawImage(captured, new Rectangle(0, 0, smallWidth, smallHeight));
                using (Graphics g2 = Graphics.FromImage(captured))
                {
                    g2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                    g2.DrawImage(small, new Rectangle(0, 0, captured.Width, captured.Height));
                }
            }

            variationBackdrop = new PictureBox();
            variationBackdrop.Name = "variationBackdrop";
            variationBackdrop.Dock = DockStyle.Fill;
            variationBackdrop.Image = captured;
            variationBackdrop.SizeMode = PictureBoxSizeMode.StretchImage;
            variationBackdrop.TabStop = false;
            Controls.Add(variationBackdrop);
            Controls.SetChildIndex(variationBackdrop, 0);
        }

        private void RemoveVariationBackdrop()
        {
            if (variationBackdrop == null) return;
            Image image = variationBackdrop.Image;
            Controls.Remove(variationBackdrop);
            variationBackdrop.Dispose();
            if (image != null) image.Dispose();
            variationBackdrop = null;
        }

        private void CloseProductVariationPopup()
        {
            pnlvariation.Visible = false;
            RemoveVariationBackdrop();
        }

        private void btnvariation_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtproductname.Text))
                {
                    CMessageBox.Show(this, "Enter the product name before adding variations.",
                        "Product Variation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtproductname.Focus();
                    return;
                }

                ShowVariationBackdrop();
                pnlvariation.Visible = true;
                pnlvariation.BringToFront();
                pnlvariation.AutoSize = false;
                pnlvariation.BorderStyle = BorderStyle.None;
                pnlvariation.BackColor = Color.White;
                pnlvariation.Size = new Size(Math.Min(1160, ClientSize.Width - 32), Math.Min(440, ClientSize.Height - 32));
                pnlvariation.Left = (ClientSize.Width - pnlvariation.Width) / 2;
                pnlvariation.Top = (ClientSize.Height - pnlvariation.Height) / 2;

                int productId;
                int.TryParse(txtpid.Text, out productId);

                if (!dgv_variation.Columns.Contains("VariationName"))
                    ConfigureProductVariationGrid();

                // Load a saved product once. Reopening the popup must preserve
                // unsaved grid edits and pending deletes until the main Save button.
                if (loadedVariationProductId != productId)
                {
                    dgv_variation.Rows.Clear();
                    pendingVariationDeletes.Clear();
                    if (productId > 0)
                        LoadProductVariations(productId);
                    loadedVariationProductId = productId;
                }

                if (variationEnabled && dgv_variation.Rows.Count == 0)
                    AddProductVariationRow();

                if (chkVariationEnabled != null)
                    chkVariationEnabled.Checked = variationEnabled;
                SetVariationEditorEnabled(variationEnabled);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Open product variation popup", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Product));
                CMessageBox.Show(this, ex.Message, "Product Variation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CloseProductVariationPopup();
            }
        }

        private void ConfigureProductVariationGrid()
        {
            dgv_variation.SuspendLayout();
            dgv_variation.Columns.Clear();
            dgv_variation.AutoGenerateColumns = false;
            dgv_variation.AllowUserToAddRows = false;
            dgv_variation.AllowUserToDeleteRows = false;
            dgv_variation.AllowUserToResizeRows = false;
            dgv_variation.MultiSelect = false;
            dgv_variation.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_variation.RowHeadersVisible = false;
            dgv_variation.BackgroundColor = Color.White;
            dgv_variation.BorderStyle = BorderStyle.None;
            dgv_variation.GridColor = Color.FromArgb(230, 230, 240);
            dgv_variation.EnableHeadersVisualStyles = false;
            dgv_variation.ColumnHeadersHeight = 42;
            dgv_variation.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24, 27, 75);
            dgv_variation.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_variation.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9F, FontStyle.Bold);
            dgv_variation.DefaultCellStyle.Font = new Font("Poppins", 9F, FontStyle.Regular);
            dgv_variation.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 219, 248);
            dgv_variation.DefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 35, 55);
            dgv_variation.RowTemplate.Height = 42;

            AddVariationTextColumn("VariationSlNo", "#", 48, true);
            DataGridViewButtonColumn action = new DataGridViewButtonColumn();
            action.Name = "VariationAction";
            action.HeaderText = "Action";
            action.Text = "Remove";
            action.UseColumnTextForButtonValue = true;
            action.Width = 84;
            dgv_variation.Columns.Add(action);

            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            name.Name = "VariationName";
            name.HeaderText = "Variation Name";
            name.MinimumWidth = 170;
            name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_variation.Columns.Add(name);

            AddVariationTextColumn("VariationPriceAddOn", "Price Add On", 115, false);
            AddVariationTextColumn("VariationFinalPrice", "Final Price", 110, true);
            AddVariationTextColumn("VariationDiscountPer", "Discount %", 100, false);
            AddVariationTextColumn("VariationDiscountAmt", "Discount Amount", 130, false);

            DataGridViewCheckBoxColumn defaultColumn = new DataGridViewCheckBoxColumn();
            defaultColumn.Name = "VariationDefault";
            defaultColumn.HeaderText = "Default";
            defaultColumn.Width = 75;
            dgv_variation.Columns.Add(defaultColumn);

            DataGridViewTextBoxColumn rowId = new DataGridViewTextBoxColumn();
            rowId.Name = "VariationRowId";
            rowId.Visible = false;
            dgv_variation.Columns.Add(rowId);

            dgv_variation.CellClick -= ProductVariationGrid_CellClick;
            dgv_variation.CellClick += ProductVariationGrid_CellClick;
            dgv_variation.CellValueChanged -= ProductVariationGrid_CellValueChanged;
            dgv_variation.CellValueChanged += ProductVariationGrid_CellValueChanged;
            dgv_variation.CurrentCellDirtyStateChanged -= ProductVariationGrid_CurrentCellDirtyStateChanged;
            dgv_variation.CurrentCellDirtyStateChanged += ProductVariationGrid_CurrentCellDirtyStateChanged;
            dgv_variation.CellEndEdit -= dgv_variation_CellEndEdit;
            dgv_variation.CellEndEdit += dgv_variation_CellEndEdit;
            dgv_variation.EditingControlShowing -= ProductVariationGrid_EditingControlShowing;
            dgv_variation.EditingControlShowing += ProductVariationGrid_EditingControlShowing;

            pnl_titlebar.BackColor = Color.FromArgb(15, 52, 105);
            tlp_vari_nav.BackColor = Color.FromArgb(15, 52, 105);
            tlp_vari_nav.Controls.Clear();

            Label title = new Label();
            title.Text = "Product Variations - " + Convert.ToString(txtproductname.Text).Trim();
            title.Dock = DockStyle.Fill;
            title.ForeColor = Color.White;
            title.Font = new Font("Poppins", 12F, FontStyle.Bold);
            title.TextAlign = ContentAlignment.MiddleLeft;
            title.Padding = new Padding(12, 0, 0, 0);
            tlp_vari_nav.Controls.Add(title, 0, 0);

            Button close = new Button();
            close.Text = "×";
            close.Dock = DockStyle.Right;
            close.Width = 44;
            close.FlatStyle = FlatStyle.Flat;
            close.FlatAppearance.BorderSize = 0;
            close.BackColor = Color.White;
            close.ForeColor = Color.FromArgb(190, 35, 55);
            close.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            close.Click += (s, e) => CloseProductVariationPopup();
            tlp_vari_nav.Controls.Add(close, 2, 0);

            btn_vari_submit.Text = "Submit";
            btn_vari_submit.BackgroundImage = null;
            btn_vari_submit.BackColor = Color.FromArgb(0, 140, 45);
            btn_vari_submit.ForeColor = Color.White;
            btn_vari_submit.Font = new Font("Poppins", 9F, FontStyle.Bold);
            btn_vari_submit.FlatStyle = FlatStyle.Flat;
            btn_vari_submit.FlatAppearance.BorderSize = 0;
            btn_vari_submit.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 105, 35);
            btn_vari_submit.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 80, 28);
            btn_vari_submit.UseVisualStyleBackColor = false;
            btn_vari_submit.Cursor = Cursors.Hand;
            btn_vari_submit.MouseEnter -= VariationSaveButton_MouseEnter;
            btn_vari_submit.MouseEnter += VariationSaveButton_MouseEnter;
            btn_vari_submit.MouseLeave -= VariationSaveButton_MouseLeave;
            btn_vari_submit.MouseLeave += VariationSaveButton_MouseLeave;
            btn_vari_submit.Width = 180;
            btn_vari_submit.Height = 40;
            btn_vari_submit.Dock = DockStyle.Right;

            Label basePriceLabel = new Label();
            basePriceLabel.Name = "lblVariationBasePrice";
            basePriceLabel.Dock = DockStyle.Left;
            basePriceLabel.Width = 230;
            basePriceLabel.TextAlign = ContentAlignment.MiddleLeft;
            basePriceLabel.Font = new Font("Poppins", 10F, FontStyle.Bold);
            basePriceLabel.ForeColor = Color.FromArgb(20, 40, 65);
            basePriceLabel.Text = "Base Price : " + GetProductBasePrice().ToString("0.00");

            Label formulaLabel = new Label();
            formulaLabel.Dock = DockStyle.Fill;
            formulaLabel.TextAlign = ContentAlignment.MiddleCenter;
            formulaLabel.Font = new Font("Poppins", 9F, FontStyle.Regular);
            formulaLabel.Text = "Variation Price = Base Price + Price Add On";

            chkVariationEnabled = new CheckBox();
            chkVariationEnabled.Name = "chkVariationEnabled";
            chkVariationEnabled.Text = "Has Variation";
            chkVariationEnabled.Width = 145;
            chkVariationEnabled.Dock = DockStyle.Right;
            chkVariationEnabled.Font = new Font("Poppins", 9F, FontStyle.Bold);
            chkVariationEnabled.ForeColor = Color.FromArgb(20, 55, 100);
            chkVariationEnabled.Checked = variationEnabled;
            chkVariationEnabled.CheckedChanged += VariationEnabled_CheckedChanged;

            Panel infoPanel = new Panel();
            infoPanel.Dock = DockStyle.Fill;
            infoPanel.BackColor = Color.FromArgb(238, 246, 255);
            infoPanel.Padding = new Padding(12, 4, 12, 4);
            infoPanel.Controls.Add(formulaLabel);
            infoPanel.Controls.Add(chkVariationEnabled);
            infoPanel.Controls.Add(basePriceLabel);

            Button addRowButton = new Button();
            addRowButton.Text = "+ Add Variation";
            addRowButton.Width = 150;
            addRowButton.Height = 40;
            addRowButton.Dock = DockStyle.Left;
            addRowButton.FlatStyle = FlatStyle.Flat;
            addRowButton.FlatAppearance.BorderColor = Color.FromArgb(35, 115, 220);
            addRowButton.ForeColor = Color.FromArgb(20, 75, 160);
            addRowButton.Click += (sender, args) => AddProductVariationRow();
            variationAddRowButton = addRowButton;

            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Width = 110;
            cancelButton.Height = 40;
            cancelButton.Dock = DockStyle.Right;
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.FlatAppearance.BorderColor = Color.FromArgb(210, 45, 55);
            cancelButton.ForeColor = Color.FromArgb(190, 35, 45);
            cancelButton.Click += (sender, args) => CloseProductVariationPopup();

            Panel footerPanel = new Panel();
            footerPanel.Dock = DockStyle.Fill;
            footerPanel.Padding = new Padding(0, 5, 0, 5);
            footerPanel.Controls.Add(cancelButton);
            footerPanel.Controls.Add(btn_vari_submit);
            footerPanel.Controls.Add(addRowButton);

            tlp_grid_main.SuspendLayout();
            tlp_grid_main.Controls.Clear();
            tlp_grid_main.RowCount = 4;
            tlp_grid_main.RowStyles.Clear();
            tlp_grid_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlp_grid_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlp_grid_main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_grid_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlp_grid_main.Controls.Add(pnl_titlebar, 0, 0);
            tlp_grid_main.Controls.Add(infoPanel, 0, 1);
            tlp_grid_main.Controls.Add(dgv_variation, 0, 2);
            tlp_grid_main.Controls.Add(footerPanel, 0, 3);
            dgv_variation.Dock = DockStyle.Fill;
            tlp_grid_main.ResumeLayout(true);
            dgv_variation.ResumeLayout(true);
            SetVariationEditorEnabled(variationEnabled);
        }

        private void VariationEnabled_CheckedChanged(object sender, EventArgs e)
        {
            variationEnabled = chkVariationEnabled != null && chkVariationEnabled.Checked;
            if (variationEnabled && dgv_variation.Rows.Count == 0)
                AddProductVariationRow();
            SetVariationEditorEnabled(variationEnabled);
        }

        private void SetVariationEditorEnabled(bool enabled)
        {
            if (dgv_variation != null)
                dgv_variation.Enabled = enabled;
            if (variationAddRowButton != null)
                variationAddRowButton.Enabled = enabled;
            // When variation is off, the popup is read-only and only its close
            // actions remain available. Submit is enabled after the checkbox is ticked.
            if (btn_vari_submit != null)
                btn_vari_submit.Enabled = enabled;
            if (chkVariationEnabled != null)
                chkVariationEnabled.Enabled = true;
        }

        private void VariationSaveButton_MouseEnter(object sender, EventArgs e)
        {
            btn_vari_submit.BackColor = Color.FromArgb(0, 105, 35);
            btn_vari_submit.ForeColor = Color.White;
        }

        private void VariationSaveButton_MouseLeave(object sender, EventArgs e)
        {
            btn_vari_submit.BackColor = Color.FromArgb(0, 140, 45);
            btn_vari_submit.ForeColor = Color.White;
        }

        private void AddVariationTextColumn(string name, string header, int width, bool readOnly)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = name;
            column.HeaderText = header;
            column.Width = width;
            column.ReadOnly = readOnly;
            dgv_variation.Columns.Add(column);
        }

        private void LoadProductVariations(int productId)
        {
            dgv_variation.Rows.Clear();

            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = @"
                t.VariationId Id,
                t.VariationName Field1,
                t.PriceAddOn Field2,
                t.Price Field3,
                t.DiscountPer Field4,
                t.DiscountAmt Field5,
                t.IsDefault Field6";
            objSearch.Table = "tblProductVariation t";
            objSearch.WhereNdGroupBy = "WHERE t.ProductId = " + productId +
                " AND t.BranchId = " + FrmDashboard.BranchId + " AND t.Status = 1";
            objSearch.OrderBy = "ORDER BY t.VariationId";

            List<DmnGridFields> variations = bllSearch.GetSearchData("prdSearch", objSearch);
            foreach (DmnGridFields variation in variations)
            {
                int index = dgv_variation.Rows.Add();
                DataGridViewRow row = dgv_variation.Rows[index];
                row.Cells["VariationSlNo"].Value = index + 1;
                row.Cells["VariationName"].Value = variation.Field1;
                row.Cells["VariationPriceAddOn"].Value = Convert.ToDecimal(variation.Field2);
                row.Cells["VariationFinalPrice"].Value = Convert.ToDecimal(variation.Field3);
                row.Cells["VariationDiscountPer"].Value = Convert.ToDecimal(variation.Field4);
                row.Cells["VariationDiscountAmt"].Value = Convert.ToDecimal(variation.Field5);
                row.Cells["VariationDefault"].Value =
                    Convert.ToString(variation.Field6) == "True" ||
                    Convert.ToString(variation.Field6) == "1";
                row.Cells["VariationRowId"].Value = variation.Id;
            }
        }

        private void AddProductVariationRow()
        {
            int index = dgv_variation.Rows.Add();
            DataGridViewRow row = dgv_variation.Rows[index];
            row.Cells["VariationSlNo"].Value = index + 1;
            row.Cells["VariationPriceAddOn"].Value = 0m;
            row.Cells["VariationFinalPrice"].Value = GetProductBasePrice();
            row.Cells["VariationDiscountPer"].Value = 0m;
            row.Cells["VariationDiscountAmt"].Value = 0m;
            row.Cells["VariationDefault"].Value = dgv_variation.Rows.Count == 1;
            row.Cells["VariationRowId"].Value = 0;
        }

        private void ProductVariationGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string columnName = dgv_variation.Columns[e.ColumnIndex].Name;
            if (columnName != "VariationAction") return;

            DataGridViewRow row = dgv_variation.Rows[e.RowIndex];
            int id;
            if (int.TryParse(Convert.ToString(row.Cells["VariationRowId"].Value), out id) &&
                id > 0 && !pendingVariationDeletes.Contains(id))
                pendingVariationDeletes.Add(id);

            dgv_variation.Rows.RemoveAt(e.RowIndex);
            RefreshProductVariationSlNo();
            if (dgv_variation.Rows.Count == 0)
                AddProductVariationRow();
            else if (!dgv_variation.Rows.Cast<DataGridViewRow>()
                .Any(r => r.Cells["VariationDefault"].Value != null &&
                          Convert.ToBoolean(r.Cells["VariationDefault"].Value)))
                dgv_variation.Rows[0].Cells["VariationDefault"].Value = true;
        }

        private void ProductVariationGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_variation.IsCurrentCellDirty)
                dgv_variation.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void ProductVariationGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dgv_variation.Columns[e.ColumnIndex].Name != "VariationDefault")
                return;

            object selected = dgv_variation.Rows[e.RowIndex].Cells["VariationDefault"].Value;
            if (selected != null && Convert.ToBoolean(selected))
            {
                for (int i = 0; i < dgv_variation.Rows.Count; i++)
                    if (i != e.RowIndex)
                        dgv_variation.Rows[i].Cells["VariationDefault"].Value = false;
            }
        }

        private void ProductVariationGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox textBox = e.Control as TextBox;
            if (textBox == null || dgv_variation.CurrentCell == null)
                return;

            textBox.KeyPress -= ProductVariationDecimal_KeyPress;
            string columnName = dgv_variation.CurrentCell.OwningColumn.Name;
            if (columnName == "VariationPriceAddOn" ||
                columnName == "VariationDiscountPer" || columnName == "VariationDiscountAmt")
                textBox.KeyPress += ProductVariationDecimal_KeyPress;
        }

        private void ProductVariationDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
                return;
            if (e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))
                return;
            e.Handled = true;
        }

        private void RefreshProductVariationSlNo()
        {
            for (int i = 0; i < dgv_variation.Rows.Count; i++)
                dgv_variation.Rows[i].Cells["VariationSlNo"].Value = i + 1;
        }

        private bool ValidateProductVariationRows()
        {
            HashSet<string> names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            int defaultCount = 0;
            foreach (DataGridViewRow row in dgv_variation.Rows)
            {
                if (row.IsNewRow) continue;
                string name = Convert.ToString(row.Cells["VariationName"].Value).Trim();
                string priceText = Convert.ToString(row.Cells["VariationPriceAddOn"].Value).Trim();
                if (name.Length == 0 && priceText.Length == 0) continue;

                if (row.Cells["VariationDefault"].Value != null &&
                    Convert.ToBoolean(row.Cells["VariationDefault"].Value)) defaultCount++;

                decimal price, discountPer, discountAmt;
                if (name.Length == 0 || !decimal.TryParse(priceText, out price) || price < 0)
                {
                    CMessageBox.Show(this, "Enter a variation name and a valid price.",
                        "Product Variation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!names.Add(name))
                {
                    CMessageBox.Show(this, "Variation names must be unique for this product.",
                        "Product Variation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                decimal.TryParse(Convert.ToString(row.Cells["VariationDiscountPer"].Value), out discountPer);
                decimal.TryParse(Convert.ToString(row.Cells["VariationDiscountAmt"].Value), out discountAmt);
                if (discountPer < 0 || discountPer > 100 || discountAmt < 0)
                {
                    CMessageBox.Show(this, "Discount and quantity values are invalid.",
                        "Product Variation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (variationEnabled && names.Count == 0)
            {
                CMessageBox.Show(this, "Add at least one variation or turn off Has Variation.",
                    "Product Variation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (names.Count > 0 && defaultCount != 1)
            {
                CMessageBox.Show(this, "Select exactly one default variation.",
                    "Product Variation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool HasProductVariations()
        {
            if (!variationEnabled || !dgv_variation.Columns.Contains("VariationName")) return false;
            foreach (DataGridViewRow row in dgv_variation.Rows)
            {
                if (row.IsNewRow) continue;
                string name = Convert.ToString(row.Cells["VariationName"].Value).Trim();
                string priceText = Convert.ToString(row.Cells["VariationPriceAddOn"].Value).Trim();
                decimal price;
                if (name.Length > 0 && decimal.TryParse(priceText, out price) && price >= 0)
                    return true;
            }
            return false;
        }

        private void SaveProductVariations(int productId)
        {
            BllProductVariation bllVariation = new BllProductVariation();

            // When Has Variation is turned off, soft-delete every previously saved
            // variation only after the product master has been saved successfully.
            if (!variationEnabled)
            {
                HashSet<int> deleteIds = new HashSet<int>(pendingVariationDeletes);
                foreach (DataGridViewRow existingRow in dgv_variation.Rows)
                {
                    if (existingRow.IsNewRow) continue;
                    int existingId;
                    if (int.TryParse(Convert.ToString(existingRow.Cells["VariationRowId"].Value), out existingId) &&
                        existingId > 0)
                        deleteIds.Add(existingId);
                }

                foreach (int variationId in deleteIds)
                {
                    DmnProductVariation deletedVariation = new DmnProductVariation();
                    deletedVariation.VariationId = variationId;
                    deletedVariation.ProductId = productId;
                    deletedVariation.BranchId = FrmDashboard.BranchId;
                    deletedVariation.CreatedBy = FrmDashboard.UserId;
                    deletedVariation.Status = 0;
                    bllVariation.DeleteVariation("prdDelete_ProductVariation", deletedVariation);
                }

                pendingVariationDeletes.Clear();
                txtitemcode.ReadOnly = false;
                txtDiscAmnt.ReadOnly = false;
                txtDisPer.ReadOnly = false;
                txtprice.ReadOnly = false;
                return;
            }

            foreach (int variationId in pendingVariationDeletes)
            {
                DmnProductVariation deletedVariation = new DmnProductVariation();
                deletedVariation.VariationId = variationId;
                deletedVariation.ProductId = productId;
                deletedVariation.BranchId = FrmDashboard.BranchId;
                deletedVariation.CreatedBy = FrmDashboard.UserId;
                deletedVariation.Status = 0;
                bllVariation.DeleteVariation("prdDelete_ProductVariation", deletedVariation);
            }

            foreach (DataGridViewRow row in dgv_variation.Rows)
            {
                if (row.IsNewRow) continue;

                string name = Convert.ToString(row.Cells["VariationName"].Value).Trim();
                string priceText = Convert.ToString(row.Cells["VariationPriceAddOn"].Value).Trim();
                if (name.Length == 0 && priceText.Length == 0) continue;

                decimal priceAddOn = Convert.ToDecimal(priceText);
                decimal finalPrice = GetProductBasePrice() + priceAddOn;
                decimal discountPer, discountAmt;
                decimal.TryParse(Convert.ToString(row.Cells["VariationDiscountPer"].Value), out discountPer);
                decimal.TryParse(Convert.ToString(row.Cells["VariationDiscountAmt"].Value), out discountAmt);

                int variationId;
                int.TryParse(Convert.ToString(row.Cells["VariationRowId"].Value), out variationId);

                DmnProductVariation variation = new DmnProductVariation();
                variation.VariationId = variationId;
                variation.ProductId = productId;
                variation.VariationName = name;
                variation.VariationTypeId = null;
                variation.Price = finalPrice;
                variation.PriceAddOn = priceAddOn;
                variation.DiscountPer = discountPer;
                variation.DiscountAmt = discountAmt;
                variation.IsDefault = row.Cells["VariationDefault"].Value != null &&
                    Convert.ToBoolean(row.Cells["VariationDefault"].Value);
                variation.QOH = 0;
                variation.IsUnlimited = true;
                variation.BranchId = FrmDashboard.BranchId;
                variation.CreatedBy = FrmDashboard.UserId;
                variation.Status = 1;

                bllVariation.AddEditData("prdInsert_upd_ProductVariation", variation);
            }

            int activeCount = dgv_variation.Rows.Cast<DataGridViewRow>()
                .Count(row => !row.IsNewRow &&
                    !string.IsNullOrWhiteSpace(Convert.ToString(row.Cells["VariationName"].Value)));

            txtitemcode.ReadOnly = activeCount > 0;
            txtDiscAmnt.ReadOnly = activeCount > 0;
            txtDisPer.ReadOnly = activeCount > 0;
            txtprice.ReadOnly = false;

            pendingVariationDeletes.Clear();
        }

        private decimal GetProductBasePrice()
        {
            decimal basePrice;
            return decimal.TryParse(Convert.ToString(txtprice.Text), out basePrice) ? basePrice : 0m;
        }

        private void btnvaria_cls_Click(object sender, EventArgs e)
        {
            CloseProductVariationPopup();
        }

        private void btn_vari_submit_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_variation.EndEdit();
                if (variationEnabled && !ValidateProductVariationRows()) return;
                CloseProductVariationPopup();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Validate product variations", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Product));
                CMessageBox.Show(this, ex.Message, "Product Variation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddNewVariationRow()
        {
            try
            {
                // 🔹 If no rows → add first row
                if (dgv_variation.Rows.Count == 0)
                {
                    int rowIndex = dgv_variation.Rows.Add();
                    dgv_variation.Rows[rowIndex].Cells[Vslno].Value = 1;
                    return;
                }

                // 🔹 Get last row
                DataGridViewRow lastRow = dgv_variation.Rows[dgv_variation.Rows.Count - 1];

                string variation = Convert.ToString(lastRow.Cells[vvariationno].Value);
                decimal price = 0;
                decimal.TryParse(Convert.ToString(lastRow.Cells[vpriceno].Value), out price);

                // 🔹 Condition check
                if (!string.IsNullOrWhiteSpace(variation) && price > 0)
                {
                    int rowIndex = dgv_variation.Rows.Add();
                    DataGridViewRow newRow = dgv_variation.Rows[rowIndex];

                    newRow.Cells[Vslno].Value = dgv_variation.Rows.Count; // SL No auto
                }
                else
                {
                    CMessageBox.Show("Please select variation and enter valid price.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("AddNewVariationRow()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Product));
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

    
        private void dgv_variation_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgv_variation.Rows.Count) return;
                DataGridViewRow row = dgv_variation.Rows[e.RowIndex];
                decimal addOn;
                decimal.TryParse(Convert.ToString(row.Cells["VariationPriceAddOn"].Value), out addOn);
                row.Cells["VariationFinalPrice"].Value = GetProductBasePrice() + addOn;
            }
            catch (Exception ex)
            {
                clsgen.LogException("dgv_variation_CellEndEdit()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Product));
            }
        }

        // Designer event adapters retained for the existing WinForms event wiring.
        private void dgv_variation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductVariationGrid_CellClick(sender, e);
        }

        private void dgv_variation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Editing is handled by the grid's normal cell-edit behavior.
        }

        private void dgv_variation_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ProductVariationGrid_EditingControlShowing(sender, e);
        }

        // Legacy variation-type master registration and its stored-procedure UI have been removed.
        // Product-specific variations are managed by the popup above.

        #region COLOR SETTING
        private void btncolor_cutsomize_Click(object sender, EventArgs e)
        {
            pnlcolorsetting.Visible = true;
            pnlcolorsetting.BringToFront();

            // Center panel
            pnlcolorsetting.Left = (this.ClientSize.Width - pnlcolorsetting.Width) / 2;
            pnlcolorsetting.Top = (this.ClientSize.Height - pnlcolorsetting.Height) / 2;
        }

        private void btnforcolor_Click(object sender, EventArgs e)
        {
            if (!chk_forecolor.Checked) return;

            if (cd_forecolor.ShowDialog() == DialogResult.OK)
            {
                btnforcolor.BackColor = cd_forecolor.Color;
                GeneratePreviewDesign(); // Live preview
            }
        }

        private void btnbgcolor_Click(object sender, EventArgs e)
        {
            if (!chk_bgcolor.Checked) return;

            if (cd_bgcolor.ShowDialog() == DialogResult.OK)
            {
                btnbgcolor.BackColor = cd_bgcolor.Color;
                GeneratePreviewDesign(); // Live preview
            }
        }

        private void btncls_colorsett_Click(object sender, EventArgs e)
        {
            pnlcolorsetting.Visible = false;
        }

        private void btnsavecolorsett_Click(object sender, EventArgs e)
        {
            // Apply to real product tiles
            foreach (Control tile in flp_productview.Controls)
            {
                // Background
                tile.BackColor = chk_bgcolor.Checked
                    ? btnbgcolor.BackColor
                    : Color.White;

                // ForeColor (Label inside)
                ApplyForeColorRecursive(tile,
                    chk_forecolor.Checked ? btnforcolor.BackColor : Color.Black);
            }

            pnlcolorsetting.Visible = false;
        }

        private void chk_forecolor_CheckedChanged(object sender, EventArgs e)
        {
            if (isLoadingColor) return;   // 🔥 Prevent reset while loading

            btnforcolor.Enabled = chk_forecolor.Checked;

            if (chk_forecolor.Checked)
                btnforcolor.BackColor = Color.Black;

            GeneratePreviewDesign();
        }

        private void chk_bgcolor_CheckedChanged(object sender, EventArgs e)
        {
            if (isLoadingColor) return;   // 🔥 Prevent reset while loading

            btnbgcolor.Enabled = chk_bgcolor.Checked;

            if (chk_bgcolor.Checked)
                btnbgcolor.BackColor = Color.White;

            GeneratePreviewDesign();
        }



        private void GeneratePreviewDesign()
        {
            flp_productview.Controls.Clear();

            string productName = "Chicken Burger";

            Panel tile = new Panel();
            tile.Width = tileW;
            tile.Height = tileH;
            tile.BorderStyle = BorderStyle.None;
            tile.Cursor = Cursors.Hand;

            // Safe default colors
            Color bgColor = chk_bgcolor.Checked ? btnbgcolor.BackColor : Color.White;
            Color foreColor = chk_forecolor.Checked ? btnforcolor.BackColor : Color.Black;

            tile.BackColor = bgColor;

            TableLayoutPanel tlpMain = new TableLayoutPanel();
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.RowCount = 1;
            tlpMain.ColumnCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            Label lblName = new Label();
            lblName.Text = productName;
            lblName.Dock = DockStyle.Fill;
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            lblName.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblName.ForeColor = foreColor;

            tlpMain.Controls.Add(lblName, 0, 0);
            tile.Controls.Add(tlpMain);

            UIHelper.RoundCorners(tile, 15, null);

            // Center preview tile
            tile.Left = (flp_productview.Width - tile.Width) / 2;
            tile.Top = (flp_productview.Height - tile.Height) / 2;

            flp_productview.Controls.Add(tile);
        }

        private void ApplyForeColorRecursive(Control parent, Color color)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Label lbl)
                    lbl.ForeColor = color;

                if (ctrl.HasChildren)
                    ApplyForeColorRecursive(ctrl, color);
            }
        }



        #endregion

        /// <summary>
        /// Ingredient units are derived from the selected raw material's Baseunit.
        /// Staff must not manually change the unit because quantities are stored in this unit.
        /// </summary>
        private void ConfigureIngredientUnitColumn()
        {
            try
            {
                if (dgv_registartion.Columns.Count <= Runitno)
                {
                    return;
                }

                // Bind the grid's Unit combo to unit names. The cell value remains UId,
                // so saving the ingredient still stores the unit ID, not its display text.
                // DmnComboBox exposes the exact Id and Value properties used by
                // DataGridViewComboBoxColumn.ValueMember and DisplayMember.
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch unitSearch = new DmnSearch();
                unitSearch.Select = "UId AS Id, UnitName AS Value";
                unitSearch.Table = "tblUnit";
                unitSearch.WhereNdGroupBy = "WHERE Status = 1";
                unitSearch.OrderBy = "ORDER BY UnitName";

                List<DmnComboBox> units =
                    bllSearch.GetSearchData("prdComboFill", unitSearch);

                DataGridViewComboBoxColumn unitColumn =
                    dgv_registartion.Columns[Runitno] as DataGridViewComboBoxColumn;

                if (unitColumn != null)
                {
                    unitColumn.DataSource = units ?? new List<DmnComboBox>();
                    unitColumn.ValueMember = "Id";
                    unitColumn.DisplayMember = "Value";
                    unitColumn.ValueType = typeof(long);
                    unitColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                    unitColumn.FlatStyle = FlatStyle.Flat;
                    unitColumn.ReadOnly = true;
                    unitColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                    unitColumn.DefaultCellStyle.BackColor = SystemColors.Control;
                    unitColumn.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                    unitColumn.ToolTipText = "Unit is automatically taken from the selected raw material's Base Unit.";
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Product page ConfigureIngredientUnitColumn()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void ConfigureInventoryItemCategoryColumn()
        {
            try
            {
                if (dgv_registartion.Columns.Count <= RCategorytypeno)
                    return;

                DataGridViewComboBoxColumn categoryColumn =
                    dgv_registartion.Columns[RCategorytypeno] as DataGridViewComboBoxColumn;
                if (categoryColumn == null)
                    return;

                DataTable categories = new DataTable();
                categories.Columns.Add("Id", typeof(int));
                categories.Columns.Add("Name", typeof(string));
                categories.Rows.Add(Convert.ToInt32(Interfaces.InventoryItemCategory.RawMaterial), "Raw Material");
                categories.Rows.Add(Convert.ToInt32(Interfaces.InventoryItemCategory.PackagingMaterial), "Packaging Material");

                categoryColumn.ValueMember = "Id";
                categoryColumn.DisplayMember = "Name";
                categoryColumn.ValueType = typeof(int);
                categoryColumn.DataSource = categories;
                categoryColumn.DefaultCellStyle.NullValue = Convert.ToInt32(Interfaces.InventoryItemCategory.RawMaterial);
                categoryColumn.Visible = chk_PackagingRequired.Checked;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Product page ConfigureInventoryItemCategoryColumn()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private int GetInventoryItemCategoryId(object value)
        {
            int categoryId;
            if (value == null || value == DBNull.Value ||
                !int.TryParse(Convert.ToString(value), out categoryId))
            {
                return Convert.ToInt32(Interfaces.InventoryItemCategory.RawMaterial);
            }

            if (categoryId != Convert.ToInt32(Interfaces.InventoryItemCategory.RawMaterial) &&
                categoryId != Convert.ToInt32(Interfaces.InventoryItemCategory.PackagingMaterial))
            {
                return Convert.ToInt32(Interfaces.InventoryItemCategory.RawMaterial);
            }

            return categoryId;
        }

        private void UpdateInventoryItemCategoryColumnVisibility()
        {
            try
            {
                if (dgv_registartion.Columns.Count <= RCategorytypeno)
                    return;

                DataGridViewComboBoxColumn categoryColumn =
                    dgv_registartion.Columns[RCategorytypeno] as DataGridViewComboBoxColumn;
                if (categoryColumn == null)
                    return;

                categoryColumn.Visible = chk_PackagingRequired.Checked;

                foreach (DataGridViewRow row in dgv_registartion.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    if (!chk_PackagingRequired.Checked || row.Cells[RCategorytypeno].Value == null)
                        row.Cells[RCategorytypeno].Value = Convert.ToInt32(Interfaces.InventoryItemCategory.RawMaterial);
                    else
                        row.Cells[RCategorytypeno].Value = GetInventoryItemCategoryId(row.Cells[RCategorytypeno].Value);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Product page UpdateInventoryItemCategoryColumnVisibility()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void chk_PackagingRequired_CheckedChanged(object sender, EventArgs e)
        {
            UpdateInventoryItemCategoryColumnVisibility();
        }

        private void dgv_registartion_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                for (int i = e.RowIndex; i < e.RowIndex + e.RowCount && i < dgv_registartion.Rows.Count; i++)
                {
                    DataGridViewRow row = dgv_registartion.Rows[i];
                    if (!row.IsNewRow && row.Cells[RCategorytypeno].Value == null)
                    {
                        row.Cells[RCategorytypeno].Value = Convert.ToInt32(Interfaces.InventoryItemCategory.RawMaterial);
                    }

                    if (!row.IsNewRow && cmb_FoodPreparationType.SelectedIndex == 2 &&
                        dgv_registartion.Columns.Count > RDeductionstageno &&
                        row.Cells[RDeductionstageno].Value == null)
                    {
                        row.Cells[RDeductionstageno].Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Product page dgv_registartion_RowsAdded()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void SetFoodPreparationTypeSelection(int preparationType)
        {
            try
            {
                if (preparationType == Convert.ToInt32(FoodPreparationType.MadetoOrder))
                    cmb_FoodPreparationType.SelectedIndex = 0;
                else if (preparationType == Convert.ToInt32(FoodPreparationType.BatchPrepared))
                    cmb_FoodPreparationType.SelectedIndex = 1;
                else if (preparationType == Convert.ToInt32(FoodPreparationType.PartiallyPrepared))
                    cmb_FoodPreparationType.SelectedIndex = 2;
                else if (preparationType == Convert.ToInt32(FoodPreparationType.NotApplicable))
                    cmb_FoodPreparationType.SelectedIndex = 3;
                else
                    cmb_FoodPreparationType.SelectedIndex = -1;

                UpdatePreparationStageColumnVisibility();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Product page SetFoodPreparationTypeSelection()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private int GetDeductionStageId(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0;

            int stageId;
            if (int.TryParse(Convert.ToString(value), out stageId))
                return stageId == 1 || stageId == 2 ? stageId : 0;

            string stageName = new string(Convert.ToString(value)
                .Where(char.IsLetterOrDigit)
                .ToArray()).ToLowerInvariant();

            if (stageName == "orderpreparation")
                return 1;

            if (stageName == "advancepreparation")
                return 2;

            return 0;
        }

        private void UpdatePreparationStageColumnVisibility()
        {
            try
            {
                if (dgv_registartion.Columns.Count <= RDeductionstageno)
                    return;

                DataGridViewComboBoxColumn preparationColumn =
                    dgv_registartion.Columns[RDeductionstageno] as DataGridViewComboBoxColumn;
                if (preparationColumn == null)
                    return;

                if (preparationColumn.DataSource == null)
                {
                    preparationColumn.Items.Clear();

                    DataTable preparationStages = new DataTable();
                    preparationStages.Columns.Add("Id", typeof(int));
                    preparationStages.Columns.Add("Name", typeof(string));
                    preparationStages.Rows.Add(0, "Any (Default)");
                    preparationStages.Rows.Add(1, "Order Preparation");
                    preparationStages.Rows.Add(2, "Advance Preparation");

                    preparationColumn.ValueMember = "Id";
                    preparationColumn.DisplayMember = "Name";
                    preparationColumn.ValueType = typeof(int);
                    preparationColumn.DataSource = preparationStages;
                }

                bool isPartiallyPrepared = cmb_FoodPreparationType.SelectedIndex == 2;
                preparationColumn.Visible = isPartiallyPrepared;
                preparationColumn.DefaultCellStyle.NullValue = 0;

                // An explicit Any choice keeps the deduction-stage field valid by default.
                if (isPartiallyPrepared)
                {
                    foreach (DataGridViewRow row in dgv_registartion.Rows)
                    {
                        if (!row.IsNewRow && row.Cells[RDeductionstageno].Value == null)
                            row.Cells[RDeductionstageno].Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Product page UpdatePreparationStageColumnVisibility()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Product));
            }
        }

        private void cmb_FoodPreparationType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdatePreparationStageColumnVisibility();
        }
    }
}
