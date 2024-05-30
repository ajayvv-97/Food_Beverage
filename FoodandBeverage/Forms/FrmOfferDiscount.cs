using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Forms.UserControls;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FoodandBeverage.Interfaces;
using System.Collections.Generic;

namespace FoodandBeverage.Forms
{
    public partial class FrmOfferDiscount : Form
    {
        public FrmOfferDiscount()
        {
            InitializeComponent();
        }
        // Function to create a rounded rectangle region
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        #region Grid Declaration
        public static int comslno = 0;
        public static int cbarcodeno = 1;
        public static int Cproductno = 2;
        public static int cpriceno = 3;
        public static int cqtynoo = 4;
        public static int ctotalamtno = 5;
        public static int ctaxtypeno = 6;
        public static int ctaxno = 7;
        public static int ctaxperno = 8;
        public static int ctaxamtno = 9;
        public static int cdisperno = 10;
        public static int cdisamtno = 11;
        public static int cnetamtno = 12;
        public static int cmrpno = 13;
        public static int cproducttypeno = 14;
        public static int cproductidno = 15;
        public static int cofferdetailsidno = 16;
        public static int cstatusno = 17;
        public static int cpridno = 18;
        public static int Ctaxsettingidno = 19;
        public static int Cavidno = 20;
        public static int Caddonstatusno = 21;


        public static int didno = 0;
        public static int dbarcode_codeno = 1;
        public static int dproductnameno = 2;
        public static int dpriceno = 3;
        public static int dcategoryname = 4;
        public static int dcateidno = 5;
        public static int dproductidno = 6;
        public static int dsaletypeno = 7;

        public static int sslno = 0;
        public static int sbarcodeno = 1;
        public static int sproductno = 2;
        public static int srundisper = 3;
        public static int spriceno = 4;
        public static int staxperno = 5;
        public static int staxamtno = 6;
        public static int staxtypeno = 7;
        public static int staxno = 8;
        public static int soffferdisperno = 9;
        public static int soffferdisamtno = 10;
        public static int sfinalpriceno = 11;
        public static int sdescriptionno = 12;
        public static int sproductidno = 13;
        public static int stabletypeno = 14;
        public static int spridno = 15;
        public static int sactionno = 16;
        public static int sstatusno = 17;
        public static int sofferdetailsidno = 18;
        public static int staxsettingidno = 19;
        #endregion

        #region Declaration 
        TableLayoutPanel tlp_searchproduct_sidebar = new TableLayoutPanel();
        TableLayoutPanel tlp_searchproduct_dashboard = new TableLayoutPanel();
        Color product_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        Color seleproduct_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        TableLayoutPanel tlp_price_list = new TableLayoutPanel();
        public bool LoadFlag = true;
        public bool LoadFlag_Product = true;
        public List<Variation_addon_List> Checked_VariationAddon_List = new List<Variation_addon_List>();
        public bool variflag = true, addonflag = true;
        private Button previousClickedButton = null;
        public int Global_ProductId;
        #endregion
        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsingle_Click(object sender, EventArgs e)
        {
            tlp_button.Visible = false;
            tlp_combo.Visible = false;
            Control controlToRemove = tlp_main.GetControlFromPosition(0, 2);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_single_main.Visible = true;
            tlp_main.Controls.Add(tlp_single_main, 0, 2);
            tlp_single_main.Dock = DockStyle.Fill;
            textb_shape();
            tlp_button.Visible = true;
            // LoadFlag_Product = true;
        }

        private void btn_combo_Click(object sender, EventArgs e)
        {
            tlp_button.Visible = false;
            tlp_single_main.Visible = false;
            Control controlToRemove = tlp_main.GetControlFromPosition(0, 2);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_combo.Visible = true;
            tlp_main.Controls.Add(tlp_combo, 0, 2);
            tlp_combo.Dock = DockStyle.Fill;
            textb_shape();
            tlp_button.Visible = true;
            // LoadFlag = true;
        }

        private void FrmOfferDiscount_Load(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
            {
                FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.Discount_And_Offer)))
                    btnsave.Enabled = true;
                else
                    btnsave.Enabled = false;
            }
            dgv_product_list.BorderStyle = BorderStyle.None;
            dgv_product_list.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_product_list.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_product_list.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_product_list.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_product_list.BackgroundColor = Color.White;

            dgv_product_list.EnableHeadersVisualStyles = false;
            dgv_product_list.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_product_list.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_product_list.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgv_sigle_product.BorderStyle = BorderStyle.None;
            dgv_sigle_product.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_sigle_product.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_sigle_product.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_sigle_product.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_sigle_product.BackgroundColor = Color.White;

            dgv_sigle_product.EnableHeadersVisualStyles = false;
            dgv_sigle_product.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_sigle_product.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_sigle_product.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            dgv_sele_cmb_products.BorderStyle = BorderStyle.None;
            dgv_sele_cmb_products.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_sele_cmb_products.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_sele_cmb_products.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_sele_cmb_products.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_sele_cmb_products.BackgroundColor = Color.White;

            dgv_sele_cmb_products.EnableHeadersVisualStyles = false;
            dgv_sele_cmb_products.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_sele_cmb_products.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_sele_cmb_products.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            #region Taxfill GST frist time
            Control controlToRemove1 = tlp_comboproduct_view.GetControlFromPosition(2, 2);
            tlp_comboproduct_view.Controls.Remove(controlToRemove1);
            Control controlToRemove2 = tlp_comboproduct_view.GetControlFromPosition(2, 3);
            tlp_comboproduct_view.Controls.Remove(controlToRemove2);
            txttaxamt.Dock = DockStyle.Fill;
            lbligst.Visible = true;
            cmbIgstPrcnt.Visible = true;

            lblvat.Visible = false;
            cmbVatPrcnt.Visible = false;

            controlToRemove1 = tlp_taxtype_taxamt_lbl.GetControlFromPosition(0, 1);
            tlp_taxtype_taxamt_lbl.Controls.Remove(controlToRemove1);
            tlp_taxtype_taxamt_lbl.Controls.Add(lbligst, 0, 0);

            controlToRemove2 = tlp_taxtype_taxamt_txt.GetControlFromPosition(0, 1);
            tlp_taxtype_taxamt_txt.Controls.Remove(controlToRemove2);
            tlp_taxtype_taxamt_txt.Controls.Add(cmbIgstPrcnt, 0, 0);

            tlp_taxtype_taxamt_lbl.Visible = true;
            tlp_taxtype_taxamt_txt.Visible = true;
            tlp_taxtype_taxamt_lbl.Dock = DockStyle.Fill;
            tlp_taxtype_taxamt_txt.Dock = DockStyle.Fill;

            lbligst.Dock = DockStyle.Fill;
            cmbIgstPrcnt.Dock = DockStyle.Fill;


            tlp_comboproduct_view.Controls.Add(tlp_taxtype_taxamt_lbl, 2, 2);
            tlp_comboproduct_view.Controls.Add(tlp_taxtype_taxamt_txt, 2, 3);
            #endregion

            dtp_to_date_s.MinDate = DateTime.Now;
            dtp_from_date_s.MinDate = DateTime.Now;

            dtp_to_time_s.MinDate = DateTime.Now;
            dtp_from_time_s.MinDate = DateTime.Now;

            dtp_to_date.MinDate = DateTime.Now;
            dtp_from_date.MinDate = DateTime.Now;

            dtp_to_time.MinDate = DateTime.Now;
            dtp_from_time.MinDate = DateTime.Now;

            //dtp_to_date.Value = DateTime.Now;
            //dtp_from_date.Value = DateTime.Now;

            //dtp_to_time.Value = DateTime.Now;
            //dtp_from_time.Value = DateTime.Now;

            LoadFlag = false;
            LoadFlag_Product = false;


            txtname_search.Text = "Food Name .....";
            txtitemcode_serach.Text = "Item Code .....";
            tlp_single_main.Visible = true;
            tlp_main.Controls.Add(tlp_single_main);
            tlp_single_main.Dock = DockStyle.Fill;

            pnl_serachproducts.BorderStyle = BorderStyle.FixedSingle; // or BorderStyle.Fixed3D
            pnl_serachproducts.BackColor = Color.White; // Set the border color to white
            pnl_serachproducts.Padding = new Padding(5); // Adjust the padding to increase the border thickness
            Taxfill();
            textb_shape();
            combofill();
            txt_prodlist_serach.Text = "Barcode / Item Code";
            txtnamepro_offer.Text = "Name Serach";
            txtnamepro_offer.ForeColor = Color.DarkGray;

            dgv_sigle_product.Rows.Clear();
            dgv_sigle_product.Rows.Add();
            DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_sigle_product.Rows[0].Cells[sactionno];
            cellcolumntype.Value = Properties.Resources.grid_unslect_color;
            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

            tlp_desc_del.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            tlp_desc_del.ColumnStyles[1] = new ColumnStyle(SizeType.AutoSize);

        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtname_search, txtitemcode_serach, txtcombodes, txtcombo_finalprice, txtcombo_disamt, txtcombo_disper, txtcombo_totalprice, txtcmbo_offername, txt_prodlist_serach, txtofferdes_s, txtofferdsiper_s, txtoffername_s, txttaxamt, txtnamepro_offer })
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
        private void txtproduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                ProductPopup(0);
            }
        }
        public void ProductPopup(int fcid)
        {
            try
            {
                if (string.IsNullOrEmpty(txtofferdsiper_s.Text))
                    txtofferdsiper_s.Text = "0";
                if (Convert.ToDecimal(txtofferdsiper_s.Text) <= 0)
                {
                    CMessageBox.Show("Please Enter Discount %", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtofferdsiper_s.Focus();
                    return;
                }

                tlp_product_list.Visible = true;
                tlp_product_list.Location = new Point(this.ClientSize.Width / 2 - tlp_product_list.Size.Width / 2, this.ClientSize.Height / 2 - tlp_product_list.Size.Height / 2);
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.Select = "p.ProductId as Id,p.Product as Field1,p.Price as Field2,p.DiscountPer as Field3,p.DiscountAmt as Field4 ,0 as Field5,c.CategoryName as Field6 ,c.Fcid as Field7,p.ItemCode as Field8";
                if (fcid == 0)
                {
                    _objSearch.Table = "tblProduct p" + " LEFT JOIN tblMenu m ON p.ProductId=m.ProductId  LEFT JOIN tblCategory_FTI  c ON c.Fcid=p.Fcid where p.Status=1 and m.Status=1 " +
                        "union select  itemid as Id,itemname as Field1,0 Field2,0 as Field3,0 as Field4,Saletype as Field5,c.CategoryName as Field6 ,c.Fcid as Field7,i.ItemCode_UPC as Field8 from " +
                         "tblItem i LEFT JOIN tblCategory_FTI  c ON c.Fcid=i.Fcid";
                    _objSearch.WhereNdGroupBy = "where i.Status=1 and i.Saletype=1";
                }
                else
                {
                    _objSearch.Table = "tblProduct p" + " LEFT JOIN tblMenu m ON p.ProductId=m.ProductId  LEFT JOIN tblCategory_FTI  c ON c.Fcid=p.Fcid where p.Status=1 and m.Status=1 and p.Fcid= " + fcid +
                        "union select  itemid as Id,itemname as Field1,0 Field2,0 as Field3,0 as Field4,Saletype as Field5,c.CategoryName as Field6 ,c.Fcid as Field7,i.ItemCode_UPC as Field8 from " +
                         "tblItem i LEFT JOIN tblCategory_FTI  c ON c.Fcid=i.Fcid";
                    _objSearch.WhereNdGroupBy = "where i.Status=1 and i.Saletype= 1 and i.Fcid=" + fcid;
                }
                _objSearch.OrderBy = "";
                var Datasource = bllSearch.GetSearchData("prdSearch", _objSearch);
                if (Datasource.Count > 0)
                {
                    dgv_product_list.Rows.Clear();
                    dgv_product_list.Rows.Add(Datasource.Count);
                    for (int i = 0; i < Datasource.Count; i++)
                    {
                        dgv_product_list.Rows[i].Cells[didno].Value = i + 1;
                        dgv_product_list.Rows[i].Cells[dbarcode_codeno].Value = Datasource[i].Field8;
                        dgv_product_list.Rows[i].Cells[dproductnameno].Value = Datasource[i].Field1;
                        dgv_product_list.Rows[i].Cells[dpriceno].Value = Datasource[i].Field2;
                        dgv_product_list.Rows[i].Cells[dcategoryname].Value = Datasource[i].Field6;
                        dgv_product_list.Rows[i].Cells[dcateidno].Value = Datasource[i].Field7;
                        dgv_product_list.Rows[i].Cells[dproductidno].Value = Datasource[i].Id;
                        dgv_product_list.Rows[i].Cells[dsaletypeno].Value = Datasource[i].Field5;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error");
            }
        }
        public bool checkOffer_product(int pid, int saletype)
        {
            try
            {
                DateTime offerdatetime = dtp_to_date_s.Value.Date + dtp_to_time_s.Value.TimeOfDay;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblOfferDetails fd left join tblOfferMaster fm on fm.Omid=fd.Omid";
                objSearch.WhereNdGroupBy = "where fm.Status = 1 and ProductId = " + pid + " and offertype = 0  and  (SELECT CONVERT(datetime, '" + offerdatetime + "', 105))  > fm.OfferEndDatetime ";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    CMessageBox.Show("offer Already Running On This Product ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                return true;
            }

            catch (Exception ex)
            {
                return false;
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
        public void ClearAll()
        {
            if (tlp_combo.Visible)
            {
                LoadFlag = true;
                dtp_to_date.MinDate = DateTime.Now.Date;
                dtp_from_date.MinDate = DateTime.Now.Date;

                dtp_to_time.MinDate = DateTime.Now;
                dtp_from_time.MinDate = DateTime.Now;

                dtp_to_date.Value = DateTime.Now;
                dtp_from_date.Value = DateTime.Now;

                dtp_to_time.Value = DateTime.Now;
                dtp_from_time.Value = DateTime.Now;

                dtp_from_date.Enabled = true;
                dtp_from_time.Enabled = true;

                LoadFlag = false;


                txtname_search.Text = "Food Name .....";
                txtitemcode_serach.Text = "Item Code .....";
                foreach (TextBox textBox in new TextBox[] { txtname_search, txtitemcode_serach, txtcombodes, txtcombo_finalprice, txtcombo_disamt, txtcombo_disper, txtcombo_totalprice, txtcmbo_offername, txt_prodlist_serach })
                {
                    textBox.Text = "";
                }

                flp_selected_item_combo.Controls.Clear();
                dgv_sele_cmb_products.Rows.Clear();
                txtcombo_omid.Text = "";
                btn_del_combo.Visible = false;
                txttaxamt.Text = "";

                #region tax fill reset
                Control controlToRemove1 = tlp_comboproduct_view.GetControlFromPosition(2, 2);
                tlp_comboproduct_view.Controls.Remove(controlToRemove1);
                Control controlToRemove2 = tlp_comboproduct_view.GetControlFromPosition(2, 3);
                tlp_comboproduct_view.Controls.Remove(controlToRemove2);
                txttaxamt.Dock = DockStyle.Fill;


                lbligst.Visible = true;
                cmbIgstPrcnt.Visible = true;

                lblvat.Visible = false;
                cmbVatPrcnt.Visible = false;

                controlToRemove1 = tlp_taxtype_taxamt_lbl.GetControlFromPosition(0, 1);
                tlp_taxtype_taxamt_lbl.Controls.Remove(controlToRemove1);
                tlp_taxtype_taxamt_lbl.Controls.Add(lbligst, 0, 0);

                controlToRemove2 = tlp_taxtype_taxamt_txt.GetControlFromPosition(0, 1);
                tlp_taxtype_taxamt_txt.Controls.Remove(controlToRemove2);
                tlp_taxtype_taxamt_txt.Controls.Add(cmbIgstPrcnt, 0, 0);

                tlp_taxtype_taxamt_lbl.Visible = true;
                tlp_taxtype_taxamt_txt.Visible = true;
                tlp_taxtype_taxamt_lbl.Dock = DockStyle.Fill;
                tlp_taxtype_taxamt_txt.Dock = DockStyle.Fill;

                lbligst.Dock = DockStyle.Fill;
                cmbIgstPrcnt.Dock = DockStyle.Fill;


                tlp_comboproduct_view.Controls.Add(tlp_taxtype_taxamt_lbl, 2, 2);
                tlp_comboproduct_view.Controls.Add(tlp_taxtype_taxamt_txt, 2, 3);
                cmbIgstPrcnt.Focus();
                tlp_taxtype_taxamt_txt.Visible = true;
                tlp_taxtype_taxamt_lbl.Visible = true;

                #endregion

                btn_combo_gridview.Visible = false;
                dgv_sele_cmb_products.Rows.Clear();
                tlp_desc_del.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
                tlp_desc_del.ColumnStyles[1] = new ColumnStyle(SizeType.AutoSize);

                textb_shape();

            }
            else if (tlp_single_main.Visible)
            {
                combofill();

                LoadFlag_Product = true;
                dtp_to_date_s.MinDate = DateTime.Now.Date;
                dtp_from_date_s.MinDate = DateTime.Now.Date;

                dtp_to_time_s.MinDate = DateTime.Now;
                dtp_from_time_s.MinDate = DateTime.Now;

                dtp_to_date_s.Value = DateTime.Now;
                dtp_from_date_s.Value = DateTime.Now;

                dtp_to_time_s.Value = DateTime.Now;
                dtp_from_time_s.Value = DateTime.Now;

                dtp_from_date_s.Enabled = true;
                dtp_from_time_s.Enabled = true;

                LoadFlag_Product = false;

                txtofferdes_s.Text = "";
                txtofferdsiper_s.Text = "";
                txtoffername_s.Text = "";
                dgv_sigle_product.Rows.Clear();
                dgv_sigle_product.Rows.Add();
                DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_sigle_product.Rows[0].Cells[sactionno];
                cellcolumntype.Value = Properties.Resources.grid_unslect_color;
                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                txtomid.Text = "";
                txtnamepro_offer.Text = "Name Serach";
                txtnamepro_offer.ForeColor = Color.DarkGray;
            }
        }
        public void Discount_Check()
        {
            try
            {
                if (txtofferdsiper_s.Text != "")
                {
                    if (Convert.ToDecimal(txtofferdsiper_s.Text) > 100)
                    {
                        CMessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtofferdsiper_s.Focus();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void discountper_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void discountper_Leave(object sender, EventArgs e)
        {
            discountAmnt();
            netAmount();
        }

        private void totaldiscountamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void totaldiscountamt_Leave(object sender, EventArgs e)
        {

            discountPer();
            netAmount();
        }
        public void discountAmnt()
        {
            //if (discountper.Text != "")
            //{
            //    if (Convert.ToDecimal(discountper.Text) > 100)
            //    {
            //        CMessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        discountper.Focus();
            //    }
            //    else
            //    {
            //        if ((string.IsNullOrEmpty(txtrprice.Text)))
            //            txtrprice.Text = "0.00";
            //        decimal discountAmnt = Convert.ToDecimal(discountper.Text.Trim()) * Convert.ToDecimal(txtrprice.Text) / 100;
            //        discountamt.Text = discountAmnt.ToString("N2");
            //    }
            //}
        }
        public void discountPer()
        {
            //if (discountamt.Text != "" && txtrprice.Text != "" && Convert.ToDecimal(txtrprice.Text) != 0)
            //{
            //    if (Convert.ToDecimal(discountamt.Text) > Convert.ToDecimal(txtrprice.Text))
            //    {
            //        CMessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        discountamt.Focus();
            //    }
            //    else
            //    {
            //        decimal discountPer = (Convert.ToDecimal(discountamt.Text) / Convert.ToDecimal(txtrprice.Text) * 100);
            //        discountper.Text = discountPer.ToString("N2");
            //    }
            //}
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (tlp_single_main.Visible)
                {
                    if (MandatoryCheck_single())
                    {
                        DmnOfferMaster dmnOffermaster = new DmnOfferMaster();
                        DmnOfferMaster InsertStatusM = new DmnOfferMaster();
                        BllOffer bllmaster = new BllOffer();
                        dmnOffermaster.Omid = Convert.ToInt32(txtomid.Text);
                        dmnOffermaster.OfferName = txtoffername_s.Text;
                        dmnOffermaster.OfferstartDatetime = dtp_from_date_s.Value.Date + dtp_from_time_s.Value.TimeOfDay;
                        dmnOffermaster.OfferEndDatetime = dtp_to_date_s.Value.Date + dtp_to_time_s.Value.TimeOfDay;
                        dmnOffermaster.Discountper = Convert.ToDecimal(txtofferdsiper_s.Text);
                        dmnOffermaster.Description = txtofferdes_s.Text;
                        dmnOffermaster.FinalPrice = 0;
                        dmnOffermaster.OfferType = 0;
                        dmnOffermaster.BranchId = FrmDashboard.BranchId;
                        dmnOffermaster.CreatedBy = FrmDashboard.UserId;
                        dmnOffermaster.Status = 1;
                        InsertStatusM = bllmaster.AddEditDataM("prdInsert_Upd_offermaster", dmnOffermaster);
                        if (InsertStatusM.Omid > 0)
                        {
                            bool flag = false;
                            for (int i = 0; i < dgv_sigle_product.RowCount; i++)
                            {
                                if (Convert.ToInt32(dgv_sigle_product.Rows[i].Cells[sproductidno].Value) > 0)
                                {
                                    DmnOfferDetails dmnOfferdetails = new DmnOfferDetails();
                                    BllOffer blldetails = new BllOffer();
                                    dmnOfferdetails.Odid = Convert.ToInt32(dgv_sigle_product.Rows[i].Cells[sofferdetailsidno].Value);
                                    dmnOfferdetails.Omid = Convert.ToInt32(InsertStatusM.Omid);
                                    dmnOfferdetails.ProductId = Convert.ToInt32(dgv_sigle_product.Rows[i].Cells[sproductidno].Value);
                                    dmnOfferdetails.Discountper = Convert.ToDecimal(txtofferdsiper_s.Text);
                                    dmnOfferdetails.DiscountAmt = Convert.ToDecimal(dgv_sigle_product.Rows[i].Cells[soffferdisamtno].Value);
                                    dmnOfferdetails.Productname = Convert.ToString(dgv_sigle_product.Rows[i].Cells[sproductno].Value);
                                    dmnOfferdetails.Description = Convert.ToString(dgv_sigle_product.Rows[i].Cells[sdescriptionno].Value);
                                    dmnOfferdetails.Prid = Convert.ToInt32(dgv_sigle_product.Rows[i].Cells[spridno].Value);
                                    dmnOfferdetails.Price = Convert.ToDecimal(dgv_sigle_product.Rows[i].Cells[spriceno].Value);
                                    dmnOfferdetails.FinalPrice = Convert.ToDecimal(dgv_sigle_product.Rows[i].Cells[sfinalpriceno].Value);
                                    dmnOfferdetails.Tax = Convert.ToString(dgv_sigle_product.Rows[i].Cells[staxno].Value);
                                    dmnOfferdetails.TaxType = Convert.ToString(dgv_sigle_product.Rows[i].Cells[staxtypeno].Value);
                                    dmnOfferdetails.TaxSettingsId = Convert.ToInt32(dgv_sigle_product.Rows[i].Cells[staxsettingidno].Value);
                                    dmnOfferdetails.ItemCode_UPC = Convert.ToString(dgv_sigle_product.Rows[i].Cells[sbarcodeno].Value);

                                    if (Convert.ToInt32(dgv_sigle_product.Rows[i].Cells[spridno].Value) == 0)
                                        dmnOfferdetails.Producttype = Convert.ToInt32(Tabletype.Product);
                                    else
                                        dmnOfferdetails.Producttype = Convert.ToInt32(Tabletype.DirectSale);

                                    dmnOfferdetails.BranchId = FrmDashboard.BranchId;
                                    dmnOfferdetails.CreatedBy = FrmDashboard.UserId;
                                    dmnOfferdetails.Status = Convert.ToInt32(dgv_sigle_product.Rows[i].Cells[sstatusno].Value);
                                    var InsertStatusD = blldetails.AddEditDataD("[prdInsert_Upd_offerdetails]", dmnOfferdetails);
                                    if (InsertStatusD.Odid > 0)
                                        flag = true;
                                    else
                                        flag = false;
                                }
                            }

                            if (flag)
                            {
                                if (Convert.ToInt32(txtomid.Text) > 0)
                                    CMessageBox.Show("Update Successfuly", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    CMessageBox.Show("Saved Successfuly ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearAll();
                            }
                            else
                                CMessageBox.Show("Saved Failed", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }

                    }
                }
                else if (tlp_combo.Visible)
                {
                    if (MandatoryCheck_Combo())
                    {
                        int insflag = 0;
                        DmnOfferMaster dmnOffermaster = new DmnOfferMaster();
                        DmnOfferMaster InsertStatusM = new DmnOfferMaster();
                        BllOffer bllmaster = new BllOffer();

                        dmnOffermaster.Omid = Convert.ToInt32(txtcombo_omid.Text);
                        dmnOffermaster.OfferName = txtcmbo_offername.Text;
                        dmnOffermaster.TotalPrice = Convert.ToDecimal(txtcombo_totalprice.Text);
                        dmnOffermaster.FinalPrice = Convert.ToDecimal(txtcombo_finalprice.Text);

                        dmnOffermaster.TaxPer = Convert.ToDecimal(txttaxper_combo.Text);
                        dmnOffermaster.TaxAmount = Convert.ToDecimal(txttaxamt.Text);

                        dmnOffermaster.OfferstartDatetime = dtp_from_date.Value.Date + dtp_from_time.Value.TimeOfDay;
                        dmnOffermaster.OfferEndDatetime = dtp_to_date.Value.Date + dtp_to_time.Value.TimeOfDay;

                        dmnOffermaster.Discountper = Convert.ToDecimal(txtcombo_disper.Text);
                        dmnOffermaster.DiscountAmt = Convert.ToDecimal(txtcombo_disamt.Text);
                        dmnOffermaster.Tax = Convert.ToString(cmbtax.Text);
                        dmnOffermaster.TaxType = Convert.ToString(cmbtaxtype.Text);

                        if (cmbtaxtype.Text == "GST")
                            dmnOffermaster.TaxSettingsId = Convert.ToInt32(cmbIgstPrcnt.SelectedValue);
                        else
                            dmnOffermaster.TaxSettingsId = Convert.ToInt32(cmbVatPrcnt.SelectedValue);

                        dmnOffermaster.OfferType = 1;
                        dmnOffermaster.BranchId = FrmDashboard.BranchId;
                        dmnOffermaster.CreatedBy = FrmDashboard.UserId;
                        if (chk_combo_status.Checked)
                            dmnOffermaster.Status = 1;
                        else
                            dmnOffermaster.Status = 0;
                        InsertStatusM = bllmaster.AddEditDataM("prdInsert_Upd_offermaster", dmnOffermaster);
                        if (InsertStatusM.Omid > 0)
                        {
                            for (int i = 0; i < dgv_sele_cmb_products.Rows.Count; i++)
                            {
                                if (dgv_sele_cmb_products.Rows[i].Cells[cproductidno].Value != null && dgv_sele_cmb_products.Rows[i].Cells[cqtynoo].Value != "")
                                {
                                    DmnOfferDetails dmnOfferdetails = new DmnOfferDetails();
                                    BllOffer blldetails = new BllOffer();
                                    dmnOfferdetails.Odid = Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[cofferdetailsidno].Value);
                                    dmnOfferdetails.Omid = Convert.ToInt32(InsertStatusM.Omid);
                                    dmnOfferdetails.ProductId = Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[cproductidno].Value);
                                    dmnOfferdetails.Quantity = Convert.ToDecimal(dgv_sele_cmb_products.Rows[i].Cells[cqtynoo].Value);
                                    dmnOfferdetails.Prid = Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[cpridno].Value);
                                    if (Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[cpridno].Value) == 0)
                                        dmnOfferdetails.Producttype = Convert.ToInt32(Tabletype.DirectSale);
                                    else
                                        dmnOfferdetails.Producttype = Convert.ToInt32(Tabletype.Product);
                                    dmnOfferdetails.Productname = Convert.ToString(dgv_sele_cmb_products.Rows[i].Cells[Cproductno].Value);

                                    dmnOfferdetails.Price = Convert.ToDecimal(dgv_sele_cmb_products.Rows[i].Cells[cpriceno].Value);
                                    dmnOfferdetails.Tax = Convert.ToString(dgv_sele_cmb_products.Rows[i].Cells[ctaxno].Value);
                                    dmnOfferdetails.TaxType = Convert.ToString(dgv_sele_cmb_products.Rows[i].Cells[ctaxtypeno].Value);
                                    dmnOfferdetails.TaxSettingsId = Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[Ctaxsettingidno].Value);
                                    dmnOfferdetails.ItemCode_UPC = Convert.ToString(dgv_sele_cmb_products.Rows[i].Cells[cbarcodeno].Value);

                                    dmnOfferdetails.Description = txtcombodes.Text;
                                    dmnOfferdetails.BranchId = FrmDashboard.BranchId;
                                    dmnOfferdetails.CreatedBy = FrmDashboard.UserId;
                                    dmnOfferdetails.Status = Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[cstatusno].Value);
                                    var InsertStatusD = blldetails.AddEditDataD("prdInsert_Upd_offerdetails", dmnOfferdetails);
                                    if (InsertStatusD == null)
                                    {
                                        CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                    }
                                    if (InsertStatusD != null)
                                    {
                                        insflag = 1;
                                    }
                                }
                            }
                            if (insflag == 1)
                            {
                                if (Convert.ToInt32(txtcombo_omid.Text) > 0)
                                    CMessageBox.Show("Update Successfuly", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    CMessageBox.Show("Saved Successfuly ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                ClearAll();
                            }

                        }
                    }
                }


            }
            catch (Exception ex)
            {

            }
        }
        public bool MandatoryCheck_single()
        {
            if (string.IsNullOrEmpty(txtomid.Text))
                txtomid.Text = "0";
            if (string.IsNullOrEmpty(txtodid.Text))
                txtodid.Text = "0";
            if (string.IsNullOrEmpty(txtofferdsiper_s.Text))
                txtofferdsiper_s.Text = "0";

            if (string.IsNullOrEmpty(txtoffername_s.Text))
            {
                CMessageBox.Show("Please Enter Offer Name", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtoffername_s.Focus();
                return false;
            }

            else if (dtp_from_date_s.Value.Date == dtp_to_date_s.Value.Date && dtp_to_time_s.Value.TimeOfDay == dtp_from_time_s.Value.TimeOfDay)
            {
                CMessageBox.Show("From' date cannot be the same as the 'To' date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtp_to_date.Focus();
                return false;
            }
            else if (dtp_from_date_s.Value > dtp_to_date_s.Value)
            {
                CMessageBox.Show("To Date Cannot Be Before From Date. Please Select a Valid Date.");
                dtp_to_date_s.Focus();
                return false;
            }
            else if (Convert.ToDecimal(txtofferdsiper_s.Text) == 0)
            {
                CMessageBox.Show("Please Enter Discount %", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtofferdsiper_s.Focus();
                return false;
            }


            return true;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (tlp_single_main.Visible)
                {
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    string returnValue = string.Empty;
                    DataTable dtColumnAlign = new DataTable();
                    dtColumnAlign.Columns.Add();
                    DmnSearch _objSearch = new DmnSearch();
                    _objSearch.ColumnHeaders = "ID,Offer Name,Discount %,Offer Start Date,Offer End Date";
                    _objSearch.Select = "Omid as ID,OfferName as Field1,Discountper as Field2,OfferstartDatetime as Field3,OfferEndDatetime as Field4";
                    _objSearch.Table = "tblOfferMaster ";
                    _objSearch.WhereNdGroupBy = "where Status = 1 and OfferType=0";
                    _objSearch.OrderBy = " ORDER BY OfferName";
                    var gridSourceserach = bllSearch.GetSearchData("prdSearch", _objSearch);
                    if (gridSourceserach.Count == 0)
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
                            int omid = Convert.ToInt32(returnValue);
                            DmnSearch objSearch = new DmnSearch();
                            objSearch = new DmnSearch();
                            objSearch.Select = "fd.Productname Field1,fd.DiscountPer Field2,fd.Price Field3,tx.IgstPrcnt Field4,tx.VatPrcnt Field5,fd.TaxType Field6,fd.Tax Field7,fd.ProductId Field8,fd.producttype Field9,fd.Prid Field10,fd.FinalPrice Field11,fd.DiscountAmt Field12," +
                                "fd.Description Field13,fd.Odid Field14,fd.ItemCode_UPC Field15,OfferstartDatetime StartDate,OfferEndDatetime EndDate," +
                                            "(select DiscountPer from tblProduct where status = 1 and ProductId = fd.ProductId) Field16," +
                                    "(select DiscountPer from tblItem where status = 1 and ItemId = fd.ProductId) Field17,fm.Offername Field18 ,fm.Discountper Field19,fm.description Field20,tx.TaxSettingsId Field21";
                            objSearch.Table = "tblOfferDetails  fd LEFT JOIN tblOfferMaster fm ON fm.Omid=fd.Omid LEFT JOIN tblTaxSettings tx on tx.TaxSettingsId=fd.TaxSettingsId";
                            objSearch.WhereNdGroupBy = "where fd.status = 1 and fd.omid =" + omid;
                            objSearch.OrderBy = "";
                            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                            if (gridSource.Count > 0)
                            {
                                txtomid.Text = Convert.ToString(omid);
                                txtoffername_s.Text = gridSource[0].Field18;
                                txtofferdsiper_s.Text = gridSource[0].Field19;
                                txtofferdes_s.Text = gridSource[0].Field20;

                                DateTime fromDateTime = gridSource[0].StartDate;
                                DateTime toDateTime = gridSource[0].EndDate;

                                if (gridSource[0].StartDate >= DateTime.Now)
                                {
                                    dtp_from_date_s.MinDate = DateTime.Now;
                                    dtp_from_time_s.MinDate = DateTime.Now;
                                    dtp_from_date_s.Value = gridSource[0].StartDate;
                                    dtp_from_time_s.Value = gridSource[0].StartDate;
                                    dtp_from_date_s.Enabled = true;
                                    dtp_from_time_s.Enabled = true;
                                }
                                else
                                {
                                    dtp_from_date_s.Enabled = false;
                                    dtp_from_time_s.Enabled = false;

                                    dtp_from_date_s.MinDate = gridSource[0].StartDate;
                                    dtp_from_date_s.Value = gridSource[0].StartDate;

                                    dtp_from_time_s.MinDate = gridSource[0].StartDate;
                                    dtp_from_time_s.Value = gridSource[0].StartDate;

                                }

                                if (gridSource[0].EndDate >= DateTime.Now)
                                {
                                    dtp_to_date_s.MinDate = DateTime.Now;
                                    dtp_to_date_s.Value = gridSource[0].EndDate;
                                    dtp_to_time_s.Value = gridSource[0].EndDate;
                                }
                                else
                                {
                                    dtp_to_date_s.MinDate = gridSource[0].EndDate.Date;
                                    // Set the "to" date and time
                                    dtp_to_date_s.Value = toDateTime.Date;

                                }
                                dtp_to_time_s.MinDate = toDateTime;
                                dtp_to_time_s.Value = toDateTime;


                                dgv_sigle_product.Rows.Clear();
                                dgv_sigle_product.Rows.Add(gridSource.Count);
                                for (int r = 0; r < gridSource.Count; r++)
                                {
                                    dgv_sigle_product.Rows[r].Cells[sslno].Value = r + 1;
                                    dgv_sigle_product.Rows[r].Cells[sbarcodeno].Value = gridSource[r].Field15;
                                    dgv_sigle_product.Rows[r].Cells[sproductno].Value = gridSource[r].Field1;

                                    if (Convert.ToInt32(gridSource[r].Field9) == Convert.ToInt32(Tabletype.DirectSale))
                                        dgv_sigle_product.Rows[r].Cells[srundisper].Value = gridSource[r].Field17;
                                    else
                                        dgv_sigle_product.Rows[r].Cells[srundisper].Value = gridSource[r].Field16;

                                    dgv_sigle_product.Rows[r].Cells[spriceno].Value = gridSource[r].Field3;

                                    dgv_sigle_product.Rows[r].Cells[staxtypeno].Value = gridSource[r].Field6;
                                    dgv_sigle_product.Rows[r].Cells[staxno].Value = gridSource[r].Field7;


                                    dgv_sigle_product.Rows[r].Cells[sproductidno].Value = gridSource[r].Field8;
                                    dgv_sigle_product.Rows[r].Cells[sstatusno].Value = 1;
                                    dgv_sigle_product.Rows[r].Cells[stabletypeno].Value = gridSource[r].Field9;

                                    dgv_sigle_product.Rows[r].Cells[spridno].Value = gridSource[r].Field10;
                                    dgv_sigle_product.Rows[r].Cells[sfinalpriceno].Value = gridSource[r].Field11;

                                    dgv_sigle_product.Rows[r].Cells[soffferdisperno].Value = gridSource[r].Field2;
                                    dgv_sigle_product.Rows[r].Cells[soffferdisamtno].Value = gridSource[r].Field12;
                                    dgv_sigle_product.Rows[r].Cells[sdescriptionno].Value = gridSource[r].Field13;
                                    dgv_sigle_product.Rows[r].Cells[sofferdetailsidno].Value = gridSource[r].Field14;
                                    dgv_sigle_product.Rows[r].Cells[staxsettingidno].Value = gridSource[r].Field21;



                                    if (gridSource[0].Field6 == "GST")
                                    {
                                        dgv_sigle_product.Rows[r].Cells[staxperno].Value = gridSource[r].Field4;
                                        decimal TaxAmount = Convert.ToDecimal(gridSource[r].Field4) * (Convert.ToDecimal(dgv_sigle_product.Rows[r].Cells[spriceno].Value) / 100);
                                        dgv_sigle_product.Rows[r].Cells[staxamtno].Value = TaxAmount.ToString("N2");

                                    }
                                    else if (gridSource[0].Field6 == "VAT")
                                    {
                                        dgv_sigle_product.Rows[r].Cells[staxperno].Value = gridSource[r].Field5;
                                        decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[r].Field5) * Convert.ToDecimal(dgv_sigle_product.Rows[r].Cells[spriceno].Value) / 100);
                                        dgv_sigle_product.Rows[r].Cells[staxamtno].Value = TaxAmount.ToString("N2");

                                    }

                                    if (Convert.ToString(dgv_sigle_product.Rows[r].Cells[staxno].Value) == "Exclusive")
                                    {
                                        decimal tot = (Convert.ToDecimal(dgv_sigle_product.Rows[r].Cells[spriceno].Value) - Convert.ToDecimal(dgv_sigle_product.Rows[r].Cells[soffferdisamtno].Value))
                                        + Convert.ToDecimal(dgv_sigle_product.Rows[r].Cells[staxamtno].Value);
                                        dgv_sigle_product.Rows[r].Cells[sfinalpriceno].Value = tot.ToString("N2");
                                    }
                                    else if (Convert.ToString(dgv_sigle_product.Rows[r].Cells[staxno].Value) == "Inclusive")
                                    {
                                        decimal tot = (Convert.ToDecimal(dgv_sigle_product.Rows[r].Cells[spriceno].Value) - Convert.ToDecimal(dgv_sigle_product.Rows[r].Cells[soffferdisamtno].Value));
                                        dgv_sigle_product.Rows[r].Cells[sfinalpriceno].Value = tot.ToString("N2");
                                    }


                                    DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_sigle_product.Rows[r].Cells[sactionno];
                                    cellcolumntype.Value = Properties.Resources.delete;
                                    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                }
                                dgv_sigle_product.Rows.Add();
                                int i = dgv_sigle_product.RowCount - 1;
                                DataGridViewImageCell cellcolumntype1 = (DataGridViewImageCell)dgv_sigle_product.Rows[i].Cells[sactionno];
                                if (i % 2 == 0)
                                    cellcolumntype1.Value = Properties.Resources.grid_unslect_color;
                                else
                                    cellcolumntype1.Value = Properties.Resources.grid_select;
                                cellcolumntype1.ImageLayout = DataGridViewImageCellLayout.Zoom;

                            }
                        }
                    }
                }
                else if (tlp_combo.Visible)
                {
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    string returnValue = string.Empty;
                    DataTable dtColumnAlign = new DataTable();
                    dtColumnAlign.Columns.Add();
                    DmnSearch _objSearch = new DmnSearch();
                    _objSearch.ColumnHeaders = "ID,Offer Name,Price,Discount %,Discount Amount,Offer Start Date,Offer End Date";
                    _objSearch.Select = "Omid as ID,OfferName as Field1,TotalPrice as Field2,Discountper as Field3,DiscountAmt as Field4,OfferstartDatetime as Field5,OfferEndDatetime as Field6";
                    _objSearch.Table = "tblOfferMaster";
                    _objSearch.WhereNdGroupBy = "where Status = 1 and OfferType=1";
                    _objSearch.OrderBy = " ORDER BY OfferName";
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
                            int omid = Convert.ToInt32(returnValue);
                            BllSearch<DmnGridFields> bllSearch1 = new BllSearch<DmnGridFields>();
                            DmnSearch objSearch = new DmnSearch();
                            objSearch.Select = "om.Omid as Field1,od.odid as Field2,od.ProductId as Field3,OfferName as Field4," +
                                "om.Discountper as Field5,om.DiscountAmt as Field6,om.FinalPrice as Field7,p.Product as Field8,p.Price as Field9," +
                                "od.Description as Field10,p.DiscountPer as Field11,p.DiscountAmt as Field12,om.TotalPrice as Field13," +
                                "od.Quantity as Field14,od.Status as Field15,om.OfferstartDatetime as StartDate," +
                                "om.OfferEndDatetime as EndDate,od.Prid Field16,od.productname as Field17,om.TaxSettingsId as Field18,om.Tax as Field19,om.TaxType as Field20";
                            objSearch.Table = "tblOfferMaster om left join tblOfferDetails od on od.Omid=om.Omid left join tblProduct p on p.ProductId=od.ProductId";
                            objSearch.WhereNdGroupBy = "where om.Status = 1 and om.Omid=" + omid;
                            objSearch.OrderBy = " ORDER BY OfferName";
                            var itemSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                            if (itemSource.Count > 0)
                            {
                                tlp_desc_del.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 80);
                                tlp_desc_del.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 20);
                                btn_del_combo.Visible = true;

                                btn_combo_gridview.Visible = true;

                                dgv_sele_cmb_products.Rows.Clear();
                                txtcombo_omid.Text = Convert.ToString(itemSource[0].Field1);
                                flp_selected_item_combo.Controls.Clear();
                                txtcombo_disper.Text = Convert.ToString(itemSource[0].Field5);
                                txtcombo_disamt.Text = Convert.ToString(itemSource[0].Field6);

                                txtcmbo_offername.Text = Convert.ToString(itemSource[0].Field4);
                                txtcombo_totalprice.Text = Convert.ToString(itemSource[0].Field13);
                                txtcombodes.Text = Convert.ToString(itemSource[0].Field10);

                                btn_del_combo.Visible = true;

                                DateTime fromDateTime = itemSource[0].StartDate;
                                DateTime toDateTime = itemSource[0].EndDate;

                                if (itemSource[0].StartDate >= DateTime.Now)
                                {
                                    dtp_from_date.MinDate = DateTime.Now;
                                    dtp_from_time.MinDate = DateTime.Now;
                                    dtp_from_date.Value = itemSource[0].StartDate;
                                    dtp_from_time.Value = itemSource[0].StartDate;
                                    dtp_from_date.Enabled = true;
                                    dtp_from_time.Enabled = true;
                                }
                                else
                                {
                                    dtp_from_date.Enabled = false;
                                    dtp_from_time.Enabled = false;

                                    dtp_from_date.MinDate = itemSource[0].StartDate;
                                    dtp_from_date.Value = itemSource[0].StartDate;

                                    dtp_from_time.MinDate = itemSource[0].StartDate;
                                    dtp_from_time.Value = itemSource[0].StartDate;

                                }

                                if (itemSource[0].EndDate >= DateTime.Now)
                                {
                                    dtp_to_date.MinDate = DateTime.Now;
                                    dtp_to_date.Value = itemSource[0].EndDate;
                                    dtp_to_time.Value = itemSource[0].EndDate;
                                }
                                else
                                {
                                    dtp_to_date.MinDate = itemSource[0].EndDate.Date;
                                    // Set the "to" date and time
                                    dtp_to_date.Value = toDateTime.Date;

                                }
                                dtp_to_time.MinDate = toDateTime;
                                dtp_to_time.Value = toDateTime;

                                cmbtaxtype.Text = itemSource[0].Field20;
                                cmbtax.Text = itemSource[0].Field19;
                                if (itemSource[0].Field20 == "GST")
                                    cmbIgstPrcnt.SelectedValue = Convert.ToInt64(itemSource[0].Field18);
                                else
                                    cmbVatPrcnt.SelectedValue = Convert.ToInt64(itemSource[0].Field18);

                                #region Fill combo products
                                for (int i = 0; i < itemSource.Count; i++)
                                {
                                    if (Convert.ToInt32(itemSource[i].Field15) == 1)
                                    {
                                        BllSearch<DmnGridFields> bllSearch_item = new BllSearch<DmnGridFields>();
                                        DmnSearch objSearch_item = new DmnSearch();
                                        objSearch_item = new DmnSearch();

                                        if (Convert.ToInt32(itemSource[i].Field16) > 0)
                                        {
                                            objSearch_item.Select = "Top(1) pr.SRate Field3 ,i.Itemname Field8,i.DiscountPer Field2,tx.IgstPrcnt Field4,tx.VatPrcnt Field5,i.TaxType Field6,i.Tax Field7,i.ItemId Field1,i.TaxSettingsId Field9,i.ItemCode_UPC Field10,i.mrp Field11,pr.id Field12";
                                            objSearch_item.Table = "tblitem i left join tblProductRate pr on pr.ProductId = i.ItemId LEFT JOIN tblTaxSettings tx on tx.TaxSettingsId=i.TaxSettingsId";
                                            objSearch_item.WhereNdGroupBy = "where i.status = 1 and pr.QOH>0 and i.itemid=" + itemSource[i].Field3;
                                            objSearch_item.OrderBy = "";
                                        }
                                        else
                                        {
                                            objSearch_item.Select = "p.Product Field1,p.DiscountPer Field2,p.Price Field3,tx.IgstPrcnt Field4,tx.VatPrcnt Field5,p.TaxType Field6,p.Tax Field7,p.ProductId Field8,p.TaxSettingsId Field9,p.ItemCode Field10,p.mrp Field11,p.productid Field12";
                                            objSearch_item.Table = "tblProduct  p LEFT JOIN tblTaxSettings tx on tx.TaxSettingsId=p.TaxSettingsId";
                                            objSearch_item.WhereNdGroupBy = "where ProductId=" + itemSource[i].Field3;
                                            objSearch_item.OrderBy = "";
                                        }
                                        var gridSourceitem = bllSearch.GetSearchData("prdSearch", objSearch_item);
                                        if (gridSourceitem.Count > 0)
                                        {
                                            #region Grid Fill
                                            dgv_sele_cmb_products.Rows.Add();
                                            int r = dgv_sele_cmb_products.Rows.Count - 1;
                                            dgv_sele_cmb_products.Rows[r].Cells[sslno].Value = r + 1;
                                            dgv_sele_cmb_products.Rows[r].Cells[cbarcodeno].Value = gridSourceitem[0].Field10;
                                            dgv_sele_cmb_products.Rows[r].Cells[cpriceno].Value = gridSourceitem[0].Field3;
                                            dgv_sele_cmb_products.Rows[r].Cells[cqtynoo].Value = itemSource[i].Field14;
                                            dgv_sele_cmb_products.Rows[r].Cells[cdisperno].Value = gridSourceitem[0].Field2;


                                            if (Convert.ToString(gridSourceitem[0].Field6) == "GST")
                                                dgv_sele_cmb_products.Rows[r].Cells[ctaxperno].Value = gridSourceitem[0].Field4;
                                            else
                                                dgv_sele_cmb_products.Rows[r].Cells[ctaxperno].Value = gridSourceitem[0].Field5;

                                            dgv_sele_cmb_products.Rows[r].Cells[ctaxtypeno].Value = gridSourceitem[0].Field6;
                                            dgv_sele_cmb_products.Rows[r].Cells[cproducttypeno].Value = Convert.ToInt32(Tabletype.Product);
                                            dgv_sele_cmb_products.Rows[r].Cells[ctaxno].Value = gridSourceitem[0].Field7;

                                            dgv_sele_cmb_products.Rows[r].Cells[Ctaxsettingidno].Value = gridSourceitem[0].Field9;
                                            dgv_sele_cmb_products.Rows[r].Cells[cstatusno].Value = 1;

                                            dgv_sele_cmb_products.Rows[r].Cells[cmrpno].Value = gridSourceitem[0].Field11;
                                            dgv_sele_cmb_products.Rows[r].Cells[cofferdetailsidno].Value = itemSource[i].Field2;
                                            if (Convert.ToInt32(itemSource[i].Field16) > 0)
                                            {
                                                dgv_sele_cmb_products.Rows[r].Cells[Cproductno].Value = gridSourceitem[0].Field8;
                                                dgv_sele_cmb_products.Rows[r].Cells[cpridno].Value = gridSourceitem[0].Field12;
                                                dgv_sele_cmb_products.Rows[r].Cells[cproductidno].Value = gridSourceitem[0].Field1;
                                                dgv_sele_cmb_products.Rows[r].Cells[stabletypeno].Value = Convert.ToInt32(Tabletype.DirectSale);
                                            }
                                            else
                                            {
                                                dgv_sele_cmb_products.Rows[r].Cells[Cproductno].Value = gridSourceitem[0].Field1;
                                                dgv_sele_cmb_products.Rows[r].Cells[cpridno].Value = 0;
                                                dgv_sele_cmb_products.Rows[r].Cells[cproductidno].Value = gridSourceitem[0].Field12;
                                                dgv_sele_cmb_products.Rows[r].Cells[stabletypeno].Value = Convert.ToInt32(Tabletype.Product);
                                            }
                                            totalcellamount_Combo(r);
                                            #endregion
                                        }
                                        TableLayoutPanel tlp_selected_Item_Box = new TableLayoutPanel();
                                        if (Convert.ToInt32(itemSource[i].Field16) > 0)
                                        {
                                            tlp_selected_Item_Box.Name = "tlp_sele_" + itemSource[i].Field16 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                            tlp_selected_Item_Box.AccessibleDescription = "2";
                                        }
                                        else
                                        {
                                            tlp_selected_Item_Box.Name = "tlp_sele_" + itemSource[i].Field3 + "_" + Convert.ToInt32(Tabletype.Product);
                                            tlp_selected_Item_Box.AccessibleDescription = "1";
                                            tlp_selected_Item_Box.Tag = itemSource[i].Field3;
                                        }

                                        tlp_selected_Item_Box.Width = 245;
                                        tlp_selected_Item_Box.Height = 60;
                                        tlp_selected_Item_Box.ColumnCount = 4;
                                        tlp_selected_Item_Box.BorderStyle = BorderStyle.FixedSingle;
                                        tlp_selected_Item_Box.BackColor = seleproduct_buttoncolor;

                                        //Item Button   
                                        Button Button_Item = new Button();
                                        if (Convert.ToInt32(itemSource[i].Field16) > 0)
                                        {
                                            Button_Item.Text = itemSource[i].Field17;
                                            Button_Item.Name = "btn_sele_" + itemSource[i].Field16 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                            Button_Item.Tag = itemSource[i].Field16;
                                        }
                                        else
                                        {
                                            Button_Item.Text = itemSource[i].Field17;
                                            Button_Item.Name = "btn_sele_" + itemSource[i].Field3 + "_" + Convert.ToInt32(Tabletype.Product);
                                            Button_Item.Tag = itemSource[i].Field3;
                                        }

                                        Button_Item.Width = 150;
                                        Button_Item.Height = 50;
                                        Button_Item.FlatAppearance.BorderSize = 0;
                                        Button_Item.FlatAppearance.BorderColor = Button_Item.BackColor;
                                        Button_Item.FlatStyle = FlatStyle.Flat;
                                        Button_Item.BackColor = Color.White;
                                        Button_Item.Click += new EventHandler(Button_ItemClickEventHandler);
                                        Button_Item.Font = new Font(Button_Item.Font.FontFamily, 10, FontStyle.Bold);
                                        // Adding the button to the third column
                                        tlp_selected_Item_Box.Controls.Add(Button_Item, 0, 0);

                                        // Creating the label for the Qty
                                        Label label = new Label();
                                        if (Convert.ToInt32(itemSource[i].Field16) > 0)
                                        {
                                            int intValue = (int)Math.Floor(Convert.ToDecimal(itemSource[i].Field14));
                                            label.Text = Convert.ToString(intValue);
                                            label.Name = "lbl_selected_" + itemSource[i].Field16 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                            label.Tag = itemSource[i].Field16;
                                            label.AccessibleDescription = "2";
                                        }
                                        else
                                        {
                                            int intValue = (int)Math.Floor(Convert.ToDecimal(itemSource[i].Field14));
                                            label.Text = Convert.ToString(intValue);
                                            label.Name = "lbl_selected_" + itemSource[i].Field3 + "_" + Convert.ToInt32(Tabletype.Product);
                                            label.Tag = itemSource[i].Field3;
                                            label.AccessibleDescription = "1";
                                        }
                                        label.AutoSize = true;
                                        label.Dock = DockStyle.Fill;
                                        label.ForeColor = Color.White;
                                        label.TextAlign = ContentAlignment.MiddleCenter;
                                        label.Font = new Font(label.Font.FontFamily, 10, FontStyle.Bold);
                                        // Adding the label to the second column
                                        tlp_selected_Item_Box.Controls.Add(label, 1, 0);

                                        //Add Button
                                        Button Button_Add = new Button();
                                        if (Convert.ToInt32(itemSource[i].Field16) > 0)
                                        {
                                            Button_Add.Name = "btn_sele_add_" + itemSource[i].Field3 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                            Button_Add.Tag = itemSource[i].Field16;
                                            Button_Add.AccessibleDescription = "2";
                                        }
                                        else
                                        {
                                            Button_Add.Name = "btn_sele_add_" + itemSource[i].Field3 + "_" + Convert.ToInt32(Tabletype.Product);
                                            Button_Add.Tag = itemSource[i].Field3;
                                            Button_Add.AccessibleDescription = "1";
                                        }

                                        Button_Add.Text = "+";
                                        Button_Add.Width = 20;
                                        Button_Add.Height = 50;
                                        Button_Add.FlatAppearance.BorderSize = 0;
                                        Button_Add.FlatAppearance.BorderColor = Button_Add.BackColor;
                                        Button_Add.FlatStyle = FlatStyle.Flat;
                                        Button_Add.BackColor = Color.White;
                                        Button_Add.Click += new EventHandler(Button_AddClickEventHandler);
                                        Button_Add.Font = new Font(Button_Add.Font.FontFamily, 10, FontStyle.Bold);
                                        Button_Add.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Button_Add.Width, Button_Add.Height, Button_Add.Width, Button_Add.Height));
                                        // Adding the button to the third column
                                        tlp_selected_Item_Box.Controls.Add(Button_Add, 2, 0);

                                        //Remove Button
                                        Button Button_Remove = new Button();
                                        if (Convert.ToInt32(itemSource[i].Field16) > 0)
                                        {
                                            Button_Remove.Name = "btn_sele_rem_" + itemSource[i].Field16 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                            Button_Remove.Tag = itemSource[i].Field16;
                                            Button_Remove.AccessibleDescription = "2";
                                        }
                                        else
                                        {
                                            Button_Remove.Name = "btn_sele_rem_" + itemSource[i].Field3 + "_" + Convert.ToInt32(Tabletype.Product);
                                            Button_Remove.Tag = itemSource[i].Field3;
                                            Button_Remove.AccessibleDescription = "1";
                                        }

                                        Button_Remove.Text = "-";
                                        Button_Remove.Width = 20;
                                        Button_Remove.Height = 50;
                                        Button_Remove.Dock = DockStyle.Left;
                                        Button_Remove.FlatAppearance.BorderSize = 0;
                                        Button_Remove.FlatAppearance.BorderColor = Button_Remove.BackColor;
                                        Button_Remove.FlatStyle = FlatStyle.Flat;
                                        Button_Remove.BackColor = Color.White;
                                        Button_Remove.Click += new EventHandler(Button_RemoveClickEventHandler);
                                        Button_Remove.Font = new Font(Button_Remove.Font.FontFamily, 10, FontStyle.Bold);
                                        Button_Remove.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Button_Remove.Width, Button_Remove.Height, Button_Remove.Width, Button_Remove.Height));
                                        // Adding the button to the third column
                                        tlp_selected_Item_Box.Controls.Add(Button_Remove, 3, 0);
                                        flp_selected_item_combo.Controls.Add(tlp_selected_Item_Box);
                                        tlp_selected_Item_Box.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, tlp_selected_Item_Box.Width, tlp_selected_Item_Box.Height, 10, 10));
                                    }
                                }
                                #endregion
                                discountAmnt_combo();
                                discountPer_combo();
                                ClacTaxAmt_combo();
                                pnl_serachproducts.Visible = false;
                                txtcombo_finalprice.Text = Convert.ToString(itemSource[0].Field7);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void netAmount()
        {
            try
            {
                //decimal netAmount = Convert.ToDecimal(txtrprice.Text) - Convert.ToDecimal(discountamt.Text);
                //txtfinalprice.Text = netAmount.ToString("N2");
            }
            catch (Exception ex)
            {

            }

        }

        private void btn_AddcomboProduct_Click(object sender, EventArgs e)
        {
            txtitemcode_serach.Text = "";
            txtname_search.Text = "";
            txtname_search.Text = "Food Name .....";
            txtitemcode_serach.Text = "Item Code .....";
            pnl_serachproducts.Location = new Point(this.ClientSize.Width / 2 - pnl_serachproducts.Size.Width / 2, this.ClientSize.Height / 2 - pnl_serachproducts.Size.Height / 2);
            pnl_serachproducts.Visible = true;
            BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = "*";
            dmnSearch.Table = "tblCategory_FTI";
            dmnSearch.WhereNdGroupBy = "where Status=1 and CategoryType=0";
            dmnSearch.OrderBy = "";
            var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
            if (gridSource.Count > 0)
            {
                pnl_searchproduct_sidebar.Controls.Clear();
                tlp_searchproduct_sidebar.Controls.Clear();
                tlp_searchproduct_sidebar.Dock = DockStyle.Fill;
                tlp_searchproduct_sidebar.AutoScroll = true;
                int rowIndex = 1;
                for (int i = 0; i < gridSource.Count; i++)
                {
                    // Configure the Button
                    Button button = new Button();
                    button.Text = gridSource[i].CategoryName;
                    button.Name = "btn" + gridSource[i].CategoryName;
                    button.Tag = gridSource[i].Fcid;
                    button.AccessibleDescription = "1";
                    button.BackColor = Color.Beige;
                    button.Width = 160;
                    button.Height = 40;

                    button.Font = new Font(button.Font.FontFamily, 8, FontStyle.Bold);
                    button.Click += new EventHandler(SidebuttonClickEventHandler);
                    tlp_searchproduct_sidebar.Controls.Add(button, 0, rowIndex);
                    rowIndex++;
                }
                pnl_searchproduct_sidebar.Controls.Add(tlp_searchproduct_sidebar);
                tlp_searchproduct_sidebar.Focus();
            }
        }

        private void SidebuttonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "p.Product as Field1,p.ProductId as Field2,0 as Field3";
                dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId=p.ProductId where p.Status=1 and p.Fcid= " + clickedControl.Tag + "and m.Status=1 " +
                    " Union Select Itemname as Field1,ItemId as Field2,Saletype as Field3 from tblItem ";//i left join tblProductRate pr on pr.ProductId = i.ItemId
                dmnSearch.WhereNdGroupBy = "where Fcid= " + clickedControl.Tag + " and Saletype=1 and Status=1 ";//and pr.QOH>0
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                pnl_serachproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                tlp_searchproduct_dashboard.AutoScroll = true;
                if (gridSource.Count > 0)
                {
                    int rowIndex = 1;
                    int columnIndex = 0;
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        if (columnIndex == 3)
                        {
                            columnIndex = 0;
                            rowIndex++;
                        }

                        // Configure the Button
                        Button button = new Button();
                        button.Text = gridSource[i].Field1;
                        if (Convert.ToInt32(gridSource[i].Field3) == 0)
                            button.Name = "btn" + gridSource[i].Field2 + "_" + Convert.ToInt32(Tabletype.Product);
                        else
                            button.Name = "btn" + gridSource[i].Field2 + "_" + Convert.ToInt32(Tabletype.DirectSale);

                        button.Tag = gridSource[i].Field2;
                        button.AccessibleDescription = gridSource[i].Field3;
                        button.Width = 170;
                        button.Height = 50;

                        // Set the FlatAppearance properties to remove the border
                        button.FlatAppearance.BorderSize = 0; // Set border width to 0
                        button.FlatAppearance.BorderColor = button.BackColor;
                        button.FlatStyle = FlatStyle.Flat;
                        button.BackColor = product_buttoncolor;
                        button.ForeColor = Color.White;

                        //// Create a circular Region
                        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                        //System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                        //path.AddEllipse(0, 0, button.Width, button.Height);
                        //button.Region = new Region(path);

                        button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                        //button.Dock = DockStyle.Fill;
                        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                        tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                        columnIndex++;
                    }
                    pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                }
                else
                {
                    CMessageBox.Show("No Products On This Category", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void SetRoundedButton(Button button, int cornerRadius)
        {
            using (var path = new GraphicsPath())
            {
                Rectangle rect = new Rectangle(0, 0, button.Width, button.Height);

                path.StartFigure();
                path.AddArc(rect.Left, rect.Top, cornerRadius, cornerRadius, 180, 90);
                path.AddArc(rect.Right - cornerRadius, rect.Top, cornerRadius, cornerRadius, 270, 90);
                path.AddArc(rect.Right - cornerRadius, rect.Bottom - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                path.AddArc(rect.Left, rect.Bottom - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                path.CloseFigure();

                button.Region = new Region(path);
            }
        }
        public void totalcellamount_Combo(int rowno)
        {
            try
            {
                if (dgv_sele_cmb_products.Rows[rowno].Cells[cproductidno].Value != "" && dgv_sele_cmb_products.Rows[rowno].Cells[cpriceno].Value != "" && dgv_sele_cmb_products.Rows[rowno].Cells[cqtynoo].Value != "")
                {
                    decimal amount = Convert.ToDecimal(dgv_sele_cmb_products.Rows[rowno].Cells[cpriceno].Value) * Convert.ToDecimal(dgv_sele_cmb_products.Rows[rowno].Cells[cqtynoo].Value);
                    dgv_sele_cmb_products.Rows[rowno].Cells[ctotalamtno].Value = amount.ToString("N2");

                    cellDiscountAmount_combo(rowno);

                    decimal discount = Convert.ToDecimal(dgv_sele_cmb_products.Rows[rowno].Cells[cdisamtno].Value);
                    cellTaxAmount_combo(rowno, amount - discount);

                    if (Convert.ToString(dgv_sele_cmb_products.Rows[rowno].Cells[ctaxno].Value) == "Exclusive")
                    {
                        decimal tot = (amount - Convert.ToDecimal(dgv_sele_cmb_products.Rows[rowno].Cells[cdisamtno].Value))
                        + Convert.ToDecimal(dgv_sele_cmb_products.Rows[rowno].Cells[ctaxamtno].Value);
                        dgv_sele_cmb_products.Rows[rowno].Cells[cnetamtno].Value = tot.ToString("N2");
                    }
                    if (Convert.ToString(dgv_sele_cmb_products.Rows[rowno].Cells[ctaxno].Value) == "Inclusive")
                    {
                        decimal tot = (amount - Convert.ToDecimal(dgv_sele_cmb_products.Rows[rowno].Cells[cdisamtno].Value));
                        dgv_sele_cmb_products.Rows[rowno].Cells[cnetamtno].Value = tot.ToString("N2");
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        #region Variation And Addon
        public void ShowVariationAddon(int pid)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                pnl_variation_addon.Location = new Point(this.Width / 2 - pnl_variation_addon.Size.Width / 2, this.Height / 2 - pnl_variation_addon.Size.Height / 2);

                dmnSearch.Select = "pavid Field1,Caption Field2,pav.Price Field3,pav.Aid Field4,a.Price Field5,pav.Type Field6,pav.ProductId Field7,pav.CCode Field8";
                dmnSearch.Table = "tblProduct_Addon_Variation pav left join tblAddon a on a.Aid=pav.Aid and a.Status=1 ";
                dmnSearch.WhereNdGroupBy = " where pav.Status=1 and ProductId= " + pid; //type= "+Convert.ToInt32(Product_Addon_Variation_Type.Variation) +" and
                dmnSearch.OrderBy = "order by pav.pavid desc";
                var gridSource_va = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource_va.Count > 0)
                {
                    Global_ProductId = 0;
                    previousClickedButton = null;
                    addonflag = true; variflag = true;
                    lblsizetotal.Text = "0";
                    lbladdontotal.Text = "0";

                    flp_addons.Controls.Clear();
                    flp_variation.Controls.Clear();

                    #region Variation 
                    foreach (var item in gridSource_va)
                    {

                        if (Convert.ToInt32(item.Field6) == 1)
                        {
                            Global_ProductId = pid;
                            TableLayoutPanel tbl_button_vari = new TableLayoutPanel();
                            tbl_button_vari.Name = "tbl_button_vari" + item.Field1;
                            tbl_button_vari.Tag = item.Field1;
                            tbl_button_vari.Dock = DockStyle.None;
                            tbl_button_vari.BorderStyle = BorderStyle.FixedSingle;
                            //tbl_button_vari.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                            tbl_button_vari.RowStyles.Add(new RowStyle(SizeType.AutoSize, 1));
                            tbl_button_vari.RowStyles.Add(new RowStyle(SizeType.AutoSize, 1));
                            tbl_button_vari.AutoSize = true;
                            tbl_button_vari.Height = 40;
                            tbl_button_vari.Margin = new Padding(15, 5, 0, 5);

                            Button button = new Button();
                            button.Text = item.Field2;
                            button.AutoSize = true;
                            button.Dock = DockStyle.Fill;
                            button.BackColor = ColorTranslator.FromHtml("#3f88c5");
                            button.Name = "btnVariation_" + item.Field1;
                            button.Tag = item.Field1;
                            button.AccessibleDescription = item.Field3;
                            button.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Regular);
                            //button.Margin = new Padding(10, 5, 0, 20);
                            button.Click += ButtonVariation_Click;

                            Label label = new Label();
                            label.Text = item.Field3;
                            label.AutoSize = true;
                            label.Dock = DockStyle.Top;
                            label.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Regular);
                            label.TextAlign = ContentAlignment.MiddleCenter;
                            //label.Margin = new Padding(10, 5, 0, 20);

                            tbl_button_vari.Controls.Add(button, 0, 0);
                            tbl_button_vari.Controls.Add(label, 0, 1);

                            flp_variation.Controls.Add(tbl_button_vari);

                            variflag = false;
                        }
                    }
                    #endregion

                    #region Addon
                    foreach (var item in gridSource_va)
                    {
                        if (Convert.ToInt32(item.Field6) == 2)
                        {
                            TableLayoutPanel tlp_selected_Item_Box = new TableLayoutPanel();

                            tlp_selected_Item_Box.Name = "tlp_addon_" + item.Field8 + "_" + item.Field1;
                            tlp_selected_Item_Box.AccessibleDescription = "1";
                            tlp_selected_Item_Box.Tag = item.Field8 + "_" + item.Field1;
                            tlp_selected_Item_Box.AutoSize = true;
                            //tlp_selected_Item_Box.Width = 220;
                            tlp_selected_Item_Box.Height = 30;
                            tlp_selected_Item_Box.ColumnCount = 4;

                            tlp_selected_Item_Box.BorderStyle = BorderStyle.None;
                            tlp_selected_Item_Box.BackColor = seleproduct_buttoncolor;

                            tlp_selected_Item_Box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                            tlp_selected_Item_Box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                            tlp_selected_Item_Box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                            tlp_selected_Item_Box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                            //Item Button   
                            Button Button_Item = new Button();
                            Button_Item.Text = item.Field2;
                            Button_Item.Name = "btn_addon_" + item.Field8 + "_" + item.Field1;
                            Button_Item.Tag = item.Field8 + "_" + item.Field1;
                            Button_Item.AccessibleDescription = item.Field5;
                            Button_Item.AutoSize = true;
                            //Button_Item.Width = 150;
                            Button_Item.Height = 20;
                            Button_Item.FlatAppearance.BorderSize = 0;
                            Button_Item.FlatAppearance.BorderColor = Button_Item.BackColor;
                            Button_Item.FlatStyle = FlatStyle.Flat;
                            Button_Item.BackColor = Color.White;
                            Button_Item.Click += new EventHandler(Button_addonaddClickEventHandler);
                            Button_Item.Font = new Font(Button_Item.Font.FontFamily, 10, FontStyle.Bold);
                            tlp_selected_Item_Box.Controls.Add(Button_Item, 0, 0);

                            // Creating the label for the Qty
                            Label label = new Label();
                            label.Text = "0";
                            label.Name = "lbl_addon_qty_" + item.Field8 + "_" + item.Field1;
                            label.Tag = item.Field8 + "_" + item.Field1;
                            label.AutoSize = true;
                            label.Dock = DockStyle.Fill;
                            label.ForeColor = Color.White;
                            label.TextAlign = ContentAlignment.MiddleCenter;
                            label.Font = new Font(label.Font.FontFamily, 10, FontStyle.Bold);
                            label.Visible = false;
                            tlp_selected_Item_Box.Controls.Add(label, 1, 0);

                            //Remove Button
                            Button Button_Remove = new Button();
                            Button_Remove.Text = "-";
                            Button_Remove.Name = "btn_addon_remqty_" + item.Field8 + "_" + item.Field1;
                            Button_Remove.AccessibleDescription = "1";
                            Button_Remove.Tag = item.Field1;
                            Button_Remove.AutoSize = true;
                            //Button_Remove.Width = 20;
                            Button_Remove.Height = 30;
                            Button_Remove.Dock = DockStyle.Fill;
                            Button_Remove.FlatAppearance.BorderSize = 0;
                            Button_Remove.FlatAppearance.BorderColor = Button_Remove.BackColor;
                            Button_Remove.FlatStyle = FlatStyle.Flat;
                            Button_Remove.BackColor = Color.White;
                            Button_Remove.Click += new EventHandler(Button_Remove_addonqtyClickEventHandler);
                            Button_Remove.Font = new Font(Button_Remove.Font.FontFamily, 10, FontStyle.Bold);
                            Button_Remove.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Button_Remove.Width, Button_Remove.Height, 50, 50));
                            tlp_selected_Item_Box.Controls.Add(Button_Remove, 2, 0);
                            flp_addons.Controls.Add(tlp_selected_Item_Box);
                            //tlp_selected_Item_Box.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, tlp_selected_Item_Box.Width, tlp_selected_Item_Box.Height, 10, 10));
                            addonflag = false;
                        }
                    }

                    #endregion

                    if (variflag)
                    {
                        label35.Visible = false;
                        flp_variation.Visible = false;
                        lblsizetotal.Visible = false;
                        label40.Visible = false;

                    }
                    else
                    {
                        label35.Visible = true;
                        flp_variation.Visible = true;
                        lblsizetotal.Visible = true;
                        label40.Visible = true;
                    }

                    if (addonflag)
                    {
                        label38.Visible = false;
                        flp_addons.Visible = false;
                        lbladdontotal.Visible = false;
                        label46.Visible = false;
                    }
                    else
                    {
                        label38.Visible = true;
                        flp_addons.Visible = true;
                        lbladdontotal.Visible = true;
                        label46.Visible = true;
                    }
                    pnl_variation_addon.Visible = true;
                    pnl_variation_addon.BringToFront();
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void ButtonVariation_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedControl = (Button)sender;
                if (previousClickedButton != null && previousClickedButton == clickedControl)
                {
                    clickedControl.BackColor = ColorTranslator.FromHtml("#3f88c5");
                    previousClickedButton = null;
                    lblsizetotal.Text = "0";
                }
                else
                {
                    if (previousClickedButton != null)
                    {
                        previousClickedButton.BackColor = ColorTranslator.FromHtml("#3f88c5");
                    }
                    clickedControl.BackColor = Color.Red;
                    previousClickedButton = clickedControl;


                    var existingItem = Checked_VariationAddon_List.FirstOrDefault(item => item.AVType == Convert.ToInt32(Product_Addon_Variation_Type.Variation));
                    if (existingItem != null)
                    {
                        int indexToUpdate = Checked_VariationAddon_List.IndexOf(existingItem);
                        Checked_VariationAddon_List[indexToUpdate] = new Variation_addon_List
                        {
                            Pid = Global_ProductId,
                            Vid = Convert.ToInt32(previousClickedButton.Tag),
                            Aid = Convert.ToInt32(clickedControl.Tag),
                            Pavid = Convert.ToInt32(clickedControl.Tag),
                            Qty = 1,
                            Price = Convert.ToDecimal(clickedControl.AccessibleDescription),
                            AVType = Convert.ToInt32(Product_Addon_Variation_Type.Variation),
                            Caption = Convert.ToString(clickedControl.Text),
                        };
                    }
                    else
                    {
                        Checked_VariationAddon_List.Add(new Variation_addon_List
                        {
                            Pid = Global_ProductId,
                            Vid = Convert.ToInt32(previousClickedButton.Tag),
                            Aid = Convert.ToInt32(clickedControl.Tag),
                            Pavid = Convert.ToInt32(clickedControl.Tag),
                            Qty = 1,
                            Price = Convert.ToDecimal(clickedControl.AccessibleDescription),
                            AVType = Convert.ToInt32(Product_Addon_Variation_Type.Variation),
                            Caption = Convert.ToString(clickedControl.Text),
                        });
                    }
                    lblsizetotal.Text = Convert.ToString(clickedControl.AccessibleDescription);
                }


            }
            catch (Exception ex)
            {

            }
        }
        private void Button_addonaddClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                if (Checked_VariationAddon_List.Count > 10)
                {
                    CMessageBox.Show("You can only add up to 10 Addons.", "Maximum Add-ons Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Control clickedControl = sender as Control;
                    string tlpname = "tlp_addon_" + clickedControl.Tag;
                    TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;

                    if (tlp_sele != null)
                    {
                        Label lblqty = tlp_sele.Controls.Find("lbl_addon_qty_" + clickedControl.Tag, true).FirstOrDefault() as Label;
                        if (lblqty != null)
                        {
                            if (lblqty.Text == "0")
                            {
                                lblqty.Visible = true;
                                lblqty.Text = "1";
                                string val = Convert.ToString(clickedControl.Tag);
                                string[] parts = val.Split('_');
                                Checked_VariationAddon_List.Add(new Variation_addon_List
                                {
                                    Pid = Global_ProductId,
                                    Vid = 0,
                                    CCode = parts[0],
                                    Pavid = Convert.ToInt32(parts[1]),
                                    Qty = 1,
                                    Price = Convert.ToDecimal(clickedControl.AccessibleDescription),
                                    AVType = Convert.ToInt32(Product_Addon_Variation_Type.Addon),
                                    Caption = Convert.ToString(clickedControl.Text),
                                });
                                decimal t = Convert.ToDecimal(lbladdontotal.Text) + Convert.ToDecimal(clickedControl.AccessibleDescription);
                                lbladdontotal.Text = Convert.ToString(t);
                            }
                            else
                            {
                                int cur_qty = Convert.ToInt32(lblqty.Text) + 1;
                                lblqty.Text = Convert.ToString(cur_qty);
                                var existingItem = Checked_VariationAddon_List.FirstOrDefault(item => item.Aid == Convert.ToInt32(clickedControl.Tag));
                                int indexToRemove = Checked_VariationAddon_List.IndexOf(existingItem);
                                Checked_VariationAddon_List[indexToRemove].Qty = cur_qty;
                                decimal t = Convert.ToDecimal(lbladdontotal.Text) + Convert.ToDecimal(clickedControl.AccessibleDescription);
                                lbladdontotal.Text = Convert.ToString(t);
                            }
                            Button btnrem = tlp_sele.Controls.Find("btn_addon_remqty_" + clickedControl.Tag, true).FirstOrDefault() as Button;
                            //btnrem.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnrem.Width, btnrem.Height, btnrem.Width, btnrem.Height));
                            btnrem.Dock = DockStyle.Right;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void Button_Remove_addonqtyClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                string tlpname = "tlp_addon_" + clickedControl.Tag;
                TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;

                if (tlp_sele != null)
                {
                    Label lblqty = tlp_sele.Controls.Find("lbl_addon_qty_" + clickedControl.Tag, true).FirstOrDefault() as Label;
                    if (lblqty != null)
                    {
                        int cur_qty = Convert.ToInt32(lblqty.Text);
                        if (cur_qty > 0)
                        {
                            cur_qty--;
                            lblqty.Text = Convert.ToString(cur_qty);
                            var existingItem = Checked_VariationAddon_List.FirstOrDefault(item => item.Aid == Convert.ToInt32(clickedControl.Tag));
                            if (existingItem != null)
                            {
                                int indexToRemove = Checked_VariationAddon_List.IndexOf(existingItem);
                                Checked_VariationAddon_List[indexToRemove].Qty = cur_qty;
                                decimal t = Convert.ToDecimal(lbladdontotal.Text) - Convert.ToDecimal(Checked_VariationAddon_List[indexToRemove].Price);
                                lbladdontotal.Text = Convert.ToString(t);
                            }

                        }
                        if (cur_qty == 0)
                        {
                            lblqty.Visible = false;
                            var existingItem = Checked_VariationAddon_List.FirstOrDefault(item => item.Aid == Convert.ToInt32(clickedControl.Tag));
                            if (existingItem != null)
                            {
                                int indexToRemove = Checked_VariationAddon_List.IndexOf(existingItem);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        private void DashboardButtonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                DmnSearch objSearch = new DmnSearch();
                Control clickedControl = sender as Control;

                if (Convert.ToInt32(clickedControl.AccessibleDescription) == 1)
                {
                    CheckMultiplePrice(Convert.ToInt32(clickedControl.Tag));
                }
                else
                {
                    #region Variation And Addon Checking
                    dmnSearch.Select = "count(*)  Field1";
                    dmnSearch.Table = "tblProduct_Addon_Variation ";
                    dmnSearch.WhereNdGroupBy = " where Status=1 and  ProductId= " + clickedControl.Tag;
                    dmnSearch.OrderBy = "";
                    var gridSource_va = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource_va.Count > 0)
                    {
                        this.SuspendLayout();
                        pnl_serachproducts.Visible = false;
                        if (Convert.ToInt32(gridSource_va[0].Field1) > 0)
                        {
                            ShowVariationAddon(Convert.ToInt32(clickedControl.Tag));
                        }
                        this.ResumeLayout();
                    }
                    #endregion

                    #region Normal Product
                    if (Convert.ToInt32(gridSource_va[0].Field1) == 0)
                    {
                        string buttonname = "btn_sele_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                        Button buttonitem = this.Controls.Find(buttonname, true).FirstOrDefault() as Button;
                        if (buttonitem != null)
                        {
                            string lblname = "lbl_selected_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                            Label lblqty = this.Controls.Find(lblname, true).FirstOrDefault() as Label;
                            int cur_qty = Convert.ToInt32(lblqty.Text) + 1;
                            lblqty.Text = Convert.ToString(cur_qty);
                            CalcTotalAmtCombo(Convert.ToInt32(buttonitem.Tag), 1);
                            pnl_serachproducts.Visible = false;
                        }
                        else
                        {
                            objSearch = new DmnSearch();
                            objSearch.Select = "p.Product Field1,p.DiscountPer Field2,p.Price Field3,tx.IgstPrcnt Field4,tx.VatPrcnt Field5,p.TaxType Field6,p.Tax Field7,p.ProductId Field8,p.TaxSettingsId Field9,p.ItemCode Field10,p.mrp Field11";
                            objSearch.Table = "tblProduct  p LEFT JOIN tblTaxSettings tx on tx.TaxSettingsId=p.TaxSettingsId";
                            objSearch.WhereNdGroupBy = "where ProductId=" + clickedControl.Tag;
                            objSearch.OrderBy = "";
                            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                            if (gridSource.Count > 0)
                            {
                                #region Grid Fill
                                dgv_sele_cmb_products.Rows.Add();
                                int r = dgv_sele_cmb_products.Rows.Count - 1;
                                dgv_sele_cmb_products.Rows[r].Cells[sslno].Value = r + 1;
                                dgv_sele_cmb_products.Rows[r].Cells[cbarcodeno].Value = gridSource[0].Field10;
                                dgv_sele_cmb_products.Rows[r].Cells[Cproductno].Value = gridSource[0].Field1;
                                dgv_sele_cmb_products.Rows[r].Cells[cdisperno].Value = gridSource[0].Field2;

                                dgv_sele_cmb_products.Rows[r].Cells[cpriceno].Value = Convert.ToDecimal(gridSource[0].Field3);
                                dgv_sele_cmb_products.Rows[r].Cells[cqtynoo].Value = 1;
                                if (Convert.ToString(gridSource[0].Field6) == "GST")
                                    dgv_sele_cmb_products.Rows[r].Cells[ctaxperno].Value = gridSource[0].Field4;
                                else
                                    dgv_sele_cmb_products.Rows[r].Cells[ctaxperno].Value = gridSource[0].Field5;

                                dgv_sele_cmb_products.Rows[r].Cells[ctaxtypeno].Value = gridSource[0].Field6;
                                dgv_sele_cmb_products.Rows[r].Cells[cproducttypeno].Value = Convert.ToInt32(Tabletype.Product);
                                dgv_sele_cmb_products.Rows[r].Cells[ctaxno].Value = gridSource[0].Field7;
                                dgv_sele_cmb_products.Rows[r].Cells[cproductidno].Value = gridSource[0].Field8;
                                dgv_sele_cmb_products.Rows[r].Cells[Ctaxsettingidno].Value = gridSource[0].Field9;
                                dgv_sele_cmb_products.Rows[r].Cells[cstatusno].Value = 1;
                                dgv_sele_cmb_products.Rows[r].Cells[stabletypeno].Value = Convert.ToInt32(Tabletype.Product);
                                dgv_sele_cmb_products.Rows[r].Cells[cpridno].Value = 0;
                                dgv_sele_cmb_products.Rows[r].Cells[cmrpno].Value = gridSource[0].Field11;
                                totalcellamount_Combo(r);
                                #endregion

                                CalcTotalAmtCombo(Convert.ToInt32(clickedControl.Tag), 1);
                                TableLayoutPanel tlp_selected_Item_Box = new TableLayoutPanel();

                                tlp_selected_Item_Box.Name = "tlp_sele_" + gridSource[0].Field8 + "_" + Convert.ToInt32(Tabletype.Product);
                                tlp_selected_Item_Box.AccessibleDescription = "1";
                                tlp_selected_Item_Box.Tag = gridSource[0].Field8;
                                tlp_selected_Item_Box.Width = 245;
                                tlp_selected_Item_Box.Height = 60;
                                tlp_selected_Item_Box.ColumnCount = 4;
                                tlp_selected_Item_Box.BorderStyle = BorderStyle.FixedSingle;
                                tlp_selected_Item_Box.BackColor = seleproduct_buttoncolor;

                                //Item Button   
                                Button Button_Item = new Button();
                                Button_Item.Text = clickedControl.Text;
                                Button_Item.Name = "btn_sele_" + gridSource[0].Field8 + "_" + Convert.ToInt32(Tabletype.Product);
                                Button_Item.Tag = gridSource[0].Field8;
                                Button_Item.Width = 150;
                                Button_Item.Height = 50;
                                Button_Item.FlatAppearance.BorderSize = 0;
                                Button_Item.FlatAppearance.BorderColor = Button_Item.BackColor;
                                Button_Item.FlatStyle = FlatStyle.Flat;
                                Button_Item.BackColor = Color.White;
                                Button_Item.Click += new EventHandler(Button_ItemClickEventHandler);
                                Button_Item.Font = new Font(Button_Item.Font.FontFamily, 10, FontStyle.Bold);
                                // Adding the button to the third column
                                tlp_selected_Item_Box.Controls.Add(Button_Item, 0, 0);

                                // Creating the label for the Qty
                                Label label = new Label();
                                label.Text = "1";  // Set your desired text
                                label.Name = "lbl_selected_" + gridSource[0].Field8 + "_" + Convert.ToInt32(Tabletype.Product);
                                label.Tag = gridSource[0].Field8;
                                label.AutoSize = true;
                                label.Dock = DockStyle.Fill;
                                label.ForeColor = Color.White;
                                label.TextAlign = ContentAlignment.MiddleCenter;
                                label.Font = new Font(label.Font.FontFamily, 10, FontStyle.Bold);
                                // Adding the label to the second column
                                tlp_selected_Item_Box.Controls.Add(label, 1, 0);

                                //Add Button
                                Button Button_Add = new Button();
                                Button_Add.Text = "+";
                                Button_Add.Name = "btn_sele_add_" + gridSource[0].Field8 + "_" + Convert.ToInt32(Tabletype.Product);
                                Button_Add.Tag = gridSource[0].Field8;
                                Button_Add.AccessibleDescription = "1";
                                Button_Add.Width = 20;
                                Button_Add.Height = 50;
                                Button_Add.FlatAppearance.BorderSize = 0;
                                Button_Add.FlatAppearance.BorderColor = Button_Add.BackColor;
                                Button_Add.FlatStyle = FlatStyle.Flat;
                                Button_Add.BackColor = Color.White;
                                Button_Add.Click += new EventHandler(Button_AddClickEventHandler);
                                Button_Add.Font = new Font(Button_Add.Font.FontFamily, 10, FontStyle.Bold);
                                Button_Add.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Button_Add.Width, Button_Add.Height, Button_Add.Width, Button_Add.Height));
                                // Adding the button to the third column
                                tlp_selected_Item_Box.Controls.Add(Button_Add, 2, 0);

                                //Remove Button
                                Button Button_Remove = new Button();
                                Button_Remove.Text = "-";
                                Button_Remove.Name = "btn_sele_rem_" + gridSource[0].Field8 + "_" + Convert.ToInt32(Tabletype.Product);
                                Button_Remove.AccessibleDescription = "1";
                                Button_Remove.Tag = gridSource[0].Field8;
                                Button_Remove.Width = 20;
                                Button_Remove.Height = 50;
                                Button_Remove.Dock = DockStyle.Left;
                                Button_Remove.FlatAppearance.BorderSize = 0;
                                Button_Remove.FlatAppearance.BorderColor = Button_Remove.BackColor;
                                Button_Remove.FlatStyle = FlatStyle.Flat;
                                Button_Remove.BackColor = Color.White;
                                Button_Remove.Click += new EventHandler(Button_RemoveClickEventHandler);
                                Button_Remove.Font = new Font(Button_Remove.Font.FontFamily, 10, FontStyle.Bold);
                                Button_Remove.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Button_Remove.Width, Button_Remove.Height, Button_Remove.Width, Button_Remove.Height));
                                // Adding the button to the third column
                                tlp_selected_Item_Box.Controls.Add(Button_Remove, 3, 0);
                                flp_selected_item_combo.Controls.Add(tlp_selected_Item_Box);
                                tlp_selected_Item_Box.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, tlp_selected_Item_Box.Width, tlp_selected_Item_Box.Height, 10, 10));
                                pnl_serachproducts.Visible = false;
                                discountAmnt_combo();
                                discountPer_combo();
                                ClacTaxAmt_combo();
                                // netAmount_combo();

                                if (flp_selected_item_combo.Controls.Count > 0)
                                    btn_combo_gridview.Visible = true;
                            }
                        }
                    }
                    #endregion

                }

            }
            catch (Exception ex)
            {
            }
        }

        public void CheckMultiplePrice(int id)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch1 = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Top(1) pr.SRate Field3 ,i.Itemname Field8,i.DiscountPer Field2,tx.IgstPrcnt Field4,tx.VatPrcnt Field5,i.TaxType Field6,i.Tax Field7,i.ItemId Field1,i.TaxSettingsId Field9,i.ItemCode_UPC Field10,i.mrp Field11,pr.id Field12";
                objSearch.Table = "tblitem i left join tblProductRate pr on pr.ProductId = i.ItemId LEFT JOIN tblTaxSettings tx on tx.TaxSettingsId=i.TaxSettingsId";
                objSearch.WhereNdGroupBy = "where i.status = 1 and pr.SRate>0  and i.itemid=" + id;//and pr.QOH>0
                objSearch.OrderBy = "";
                var gridSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    string buttonname = "btn_sele_" + gridSource[0].Field12 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    Button buttonitem = this.Controls.Find(buttonname, true).FirstOrDefault() as Button;
                    if (buttonitem != null)
                    {
                        string lblname = "lbl_selected_" + gridSource[0].Field12 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                        Label lblqty = this.Controls.Find(lblname, true).FirstOrDefault() as Label;
                        int cur_qty = Convert.ToInt32(lblqty.Text) + 1;
                        lblqty.Text = Convert.ToString(cur_qty);
                        CalcTotalAmtCombo_prid(Convert.ToInt32(gridSource[0].Field12), 1);
                        pnl_serachproducts.Visible = false;
                    }
                    else
                    {
                        #region Grid Fill
                        dgv_sele_cmb_products.Rows.Add();
                        int r = dgv_sele_cmb_products.Rows.Count - 1;
                        dgv_sele_cmb_products.Rows[r].Cells[sslno].Value = r + 1;
                        dgv_sele_cmb_products.Rows[r].Cells[cbarcodeno].Value = gridSource[0].Field10;
                        dgv_sele_cmb_products.Rows[r].Cells[Cproductno].Value = gridSource[0].Field8;
                        dgv_sele_cmb_products.Rows[r].Cells[cdisperno].Value = gridSource[0].Field2;

                        dgv_sele_cmb_products.Rows[r].Cells[cpriceno].Value = Convert.ToDecimal(gridSource[0].Field3);

                        dgv_sele_cmb_products.Rows[r].Cells[cqtynoo].Value = 0;
                        if (Convert.ToString(gridSource[0].Field6) == "GST")
                            dgv_sele_cmb_products.Rows[r].Cells[ctaxperno].Value = gridSource[0].Field4;
                        else
                            dgv_sele_cmb_products.Rows[r].Cells[ctaxperno].Value = gridSource[0].Field5;
                        dgv_sele_cmb_products.Rows[r].Cells[ctaxtypeno].Value = gridSource[0].Field6;
                        dgv_sele_cmb_products.Rows[r].Cells[cproducttypeno].Value = Convert.ToInt32(Tabletype.DirectSale);
                        dgv_sele_cmb_products.Rows[r].Cells[ctaxno].Value = gridSource[0].Field7;
                        dgv_sele_cmb_products.Rows[r].Cells[cproductidno].Value = gridSource[0].Field1;
                        dgv_sele_cmb_products.Rows[r].Cells[Ctaxsettingidno].Value = gridSource[0].Field9;
                        dgv_sele_cmb_products.Rows[r].Cells[cstatusno].Value = 1;
                        dgv_sele_cmb_products.Rows[r].Cells[cpridno].Value = gridSource[0].Field12;
                        dgv_sele_cmb_products.Rows[r].Cells[cmrpno].Value = gridSource[0].Field11;
                        #endregion

                        CalcTotalAmtCombo_prid(Convert.ToInt32(gridSource[0].Field12), 1);

                        TableLayoutPanel tlp_selected_Item_Box = new TableLayoutPanel();

                        tlp_selected_Item_Box.Name = "tlp_sele_" + gridSource[0].Field12 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                        tlp_selected_Item_Box.AccessibleDescription = "2";
                        tlp_selected_Item_Box.Width = 245;
                        tlp_selected_Item_Box.Height = 60;
                        tlp_selected_Item_Box.ColumnCount = 4;
                        tlp_selected_Item_Box.BorderStyle = BorderStyle.FixedSingle;
                        tlp_selected_Item_Box.BackColor = seleproduct_buttoncolor;

                        //Item Button   
                        Button Button_Item = new Button();
                        Button_Item.Text = gridSource[0].Field8;
                        Button_Item.Name = "btn_sele_" + gridSource[0].Field12 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                        Button_Item.Tag = gridSource[0].Field12;
                        Button_Item.Width = 150;
                        Button_Item.Height = 50;
                        Button_Item.FlatAppearance.BorderSize = 0;
                        Button_Item.FlatAppearance.BorderColor = Button_Item.BackColor;
                        Button_Item.FlatStyle = FlatStyle.Flat;
                        Button_Item.BackColor = Color.White;
                        Button_Item.Click += new EventHandler(Button_ItemClickEventHandler);
                        Button_Item.Font = new Font(Button_Item.Font.FontFamily, 10, FontStyle.Bold);
                        // Adding the button to the third column
                        tlp_selected_Item_Box.Controls.Add(Button_Item, 0, 0);

                        // Creating the label for the Qty
                        Label label = new Label();
                        label.Text = "1";  // Set your desired text
                        label.Name = "lbl_selected_" + gridSource[0].Field12 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                        label.Tag = gridSource[0].Field12;
                        label.AutoSize = true;
                        label.Dock = DockStyle.Fill;
                        label.ForeColor = Color.White;
                        label.TextAlign = ContentAlignment.MiddleCenter;
                        label.Font = new Font(label.Font.FontFamily, 10, FontStyle.Bold);
                        // Adding the label to the second column
                        tlp_selected_Item_Box.Controls.Add(label, 1, 0);

                        //Add Button
                        Button Button_Add = new Button();
                        Button_Add.Text = "+";
                        Button_Add.Name = "btn_sele_add_" + gridSource[0].Field12 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                        Button_Add.AccessibleDescription = "2";
                        Button_Add.Tag = gridSource[0].Field12;
                        Button_Add.Width = 20;
                        Button_Add.Height = 50;
                        Button_Add.FlatAppearance.BorderSize = 0;
                        Button_Add.FlatAppearance.BorderColor = Button_Add.BackColor;
                        Button_Add.FlatStyle = FlatStyle.Flat;
                        Button_Add.BackColor = Color.White;
                        Button_Add.Click += new EventHandler(Button_AddClickEventHandler);
                        Button_Add.Font = new Font(Button_Add.Font.FontFamily, 10, FontStyle.Bold);
                        Button_Add.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Button_Add.Width, Button_Add.Height, Button_Add.Width, Button_Add.Height));
                        // Adding the button to the third column
                        tlp_selected_Item_Box.Controls.Add(Button_Add, 2, 0);

                        //Remove Button
                        Button Button_Remove = new Button();
                        Button_Remove.Text = "-";
                        Button_Remove.Name = "btn_sele_rem_" + gridSource[0].Field12 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                        Button_Remove.AccessibleDescription = "2";
                        Button_Remove.Tag = gridSource[0].Field12;
                        Button_Remove.Width = 20;
                        Button_Remove.Height = 50;
                        Button_Remove.Dock = DockStyle.Left;
                        Button_Remove.FlatAppearance.BorderSize = 0;
                        Button_Remove.FlatAppearance.BorderColor = Button_Remove.BackColor;
                        Button_Remove.FlatStyle = FlatStyle.Flat;
                        Button_Remove.BackColor = Color.White;
                        Button_Remove.Click += new EventHandler(Button_RemoveClickEventHandler);
                        Button_Remove.Font = new Font(Button_Remove.Font.FontFamily, 10, FontStyle.Bold);
                        Button_Remove.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Button_Remove.Width, Button_Remove.Height, Button_Remove.Width, Button_Remove.Height));
                        // Adding the button to the third column
                        tlp_selected_Item_Box.Controls.Add(Button_Remove, 3, 0);
                        flp_selected_item_combo.Controls.Add(tlp_selected_Item_Box);
                        tlp_selected_Item_Box.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, tlp_selected_Item_Box.Width, tlp_selected_Item_Box.Height, 10, 10));
                        pnl_serachproducts.Visible = false;
                        discountAmnt_combo();
                        discountPer_combo();
                        ClacTaxAmt_combo();
                        //netAmount_combo();
                        pnlprice.Visible = false;
                    }

                    //pnl_serachproducts.Visible = false;
                    //Control controlclose;
                    //pnlprice.Controls.Clear();
                    //tlp_price_list.Controls.Clear();
                    //tlp_price_list.Dock = DockStyle.Fill;
                    //tlp_price_list.ColumnCount = 1;
                    //tlp_price_list.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                    //tlp_price_list.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                    //tlp_price_list.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                    //Button closebutton = new Button();
                    //closebutton.Text = "X";
                    //controlclose = closebutton;
                    //closebutton.BackgroundImage = FoodandBeverage.Properties.Resources.close_price_change;
                    //closebutton.BackgroundImageLayout = ImageLayout.Stretch;
                    //closebutton.FlatStyle = FlatStyle.Flat;
                    //closebutton.ForeColor = System.Drawing.Color.White;
                    //closebutton.Font = new Font("Georgia", 14);
                    //closebutton.Width = 50;
                    //closebutton.Height = 50;
                    //closebutton.Dock = DockStyle.Right;
                    //closebutton.Click += new EventHandler(closebuttonClickEventHandler);
                    //tlp_price_list.RowCount++;
                    //tlp_price_list.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    //tlp_price_list.Controls.Add(controlclose, 0, 0);

                    //Control control1, controlsrate, controlqty;
                    //int rowIndex = 2;
                    //for (int i = 0; i < gridSource.Count; i++)
                    //{
                    //    Button pricebutton = new Button();
                    //    pricebutton.Text = "RS. " + (gridSource[i].Field2).ToString() + "  QTY : " + (gridSource[i].Field3).ToString(); ;
                    //    pricebutton.Name = (gridSource[i].Field1).ToString();
                    //    pricebutton.Tag = gridSource[i].Field5;
                    //    pricebutton.AccessibleDescription = gridSource[i].Field4;

                    //    control1 = pricebutton;
                    //    //pricebutton.Dock = DockStyle.Fill;
                    //    pricebutton.BackgroundImage = FoodandBeverage.Properties.Resources.Button_Price_change;
                    //    pricebutton.BackgroundImageLayout = ImageLayout.Stretch;
                    //    pricebutton.FlatStyle = FlatStyle.Flat;
                    //    pricebutton.ForeColor = System.Drawing.Color.White;
                    //    pricebutton.Font = new Font("Georgia", 14);
                    //    pricebutton.Width = 300;
                    //    pricebutton.Height = 70;
                    //    // Add the event handler for the button
                    //    pricebutton.Click += new EventHandler(MyButtonComboClickEventHandler);
                    //    // Add the controls to the row
                    //    tlp_price_list.RowCount++;
                    //    tlp_price_list.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    //    tlp_price_list.Controls.Add(control1, 0, rowIndex);
                    //    rowIndex++;
                    //}
                    //// Add the TableLayoutPanel to the form

                    //pnlprice.Visible = true;
                    //pnlprice.Location = new Point(this.ClientSize.Width / 2 - pnlprice.Size.Width / 2, this.ClientSize.Height / 2 - pnlprice.Size.Height / 2);
                    //pnlprice.Controls.Add(tlp_price_list);

                    if (flp_selected_item_combo.Controls.Count > 0)
                        btn_combo_gridview.Visible = true;

                }
                else
                {
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Button_ItemClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                string tlpname = "tlp_sele_" + clickedControl.Text;
                // Find the TableLayoutPanel control by name
                TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                //if (tlp_sele != null)
                //{
                //    if (Convert.ToInt32(tlp_sele.AccessibleDescription) == 0)
                //    {
                //        BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                //        DmnSearch objSearch = new DmnSearch();
                //        objSearch = new DmnSearch();
                //        objSearch.Select = "*";
                //        objSearch.Table = "tblProduct";
                //        objSearch.WhereNdGroupBy = "where ProductId=" + clickedControl.Tag;
                //        objSearch.OrderBy = "";
                //        var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                //        if (gridSource.Count > 0)
                //        {
                //            txtcombo_run_price.Text = Convert.ToString(gridSource[0].Price);
                //            txtcombo_run_disper.Text = Convert.ToString(gridSource[0].DiscountPer);
                //            txtcombo_run_disamt.Text = Convert.ToString(gridSource[0].DiscountAmt);
                //        }

                //    }
                //    else
                //    {
                //        for (int i = 0; i < dgv_sele_cmb_products.RowCount; i++)
                //        {
                //            if (Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[pridno].Value) == Convert.ToInt32(clickedControl.Tag))
                //            {
                //                txtcombo_run_price.Text = Convert.ToString(dgv_sele_cmb_products.Rows[i].Cells[pidno].Value);
                //                txtcombo_run_disper.Text = Convert.ToString(dgv_sele_cmb_products.Rows[i].Cells[pidno].Value);
                //                txtcombo_run_disamt.Text = Convert.ToString(dgv_sele_cmb_products.Rows[i].Cells[pidno].Value);
                //                break;
                //            }
                //        }
                //    }

                //}

            }
            catch (Exception ex)
            {
            }
        }
        private void Button_AddClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                txtcombo_disamt.Text = "0";
                txtcombo_disper.Text = "0";
                txtcombo_finalprice.Text = "0";
                string lblname = "", tlpname = "";
                Control clickedControl = sender as Control;
                if (clickedControl.AccessibleDescription == "1")
                {
                    lblname = "lbl_selected_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                    tlpname = "tlp_sele_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                }
                else
                {
                    lblname = "lbl_selected_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    tlpname = "tlp_sele_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                }
                // Find the TableLayoutPanel control by name
                Label lblqty = this.Controls.Find(lblname, true).FirstOrDefault() as Label;
                TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                if (lblqty != null)
                {
                    int cur_qty = Convert.ToInt32(lblqty.Text) + 1;
                    lblqty.Text = Convert.ToString(cur_qty);
                    if (tlp_sele != null)
                    {
                        if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.Product))
                            CalcTotalAmtCombo(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                        else
                            CalcTotalAmtCombo_prid(Convert.ToInt32(clickedControl.Tag), cur_qty);
                    }
                    discountAmnt_combo();
                    discountPer_combo();
                    ClacTaxAmt_combo();
                    // netAmount_combo();
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }

        #region ADD Total Amount single Price and Multiple Price
        public void CalcTotalAmtCombo_prid(int prid, int qty)
        {
            try
            {
                int rowno = 0;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "SRate as Field1";
                objSearch.Table = "tblProductRate";
                objSearch.WhereNdGroupBy = "where id = " + prid;
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    if (string.IsNullOrEmpty(txtcombo_totalprice.Text))
                        txtcombo_totalprice.Text = "0";

                    for (int i = 0; i < dgv_sele_cmb_products.RowCount; i++)
                    {
                        if (prid == Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[cpridno].Value) &&
                            Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[cproducttypeno].Value) == Convert.ToInt32(Tabletype.DirectSale))
                        {
                            dgv_sele_cmb_products.Rows[i].Cells[cqtynoo].Value = Convert.ToDecimal(dgv_sele_cmb_products.Rows[i].Cells[cqtynoo].Value) + 1;
                            totalcellamount_Combo(i);
                            rowno = i;
                            break;
                        }
                    }

                    //discount calculation on item  reg time 
                    if (Convert.ToDecimal(dgv_sele_cmb_products.Rows[rowno].Cells[cdisperno].Value) > 0)
                    {
                        decimal disAmnt = Convert.ToDecimal(dgv_sele_cmb_products.Rows[rowno].Cells[cdisperno].Value) * Convert.ToDecimal(gridSource[0].Field1) / 100;
                        decimal finamt = Convert.ToDecimal(gridSource[0].Field1) - disAmnt;
                        decimal totalamt = finamt + Convert.ToDecimal(txtcombo_totalprice.Text);
                        txtcombo_totalprice.Text = Convert.ToString(totalamt);
                    }
                    else
                    {
                        decimal totalamt = Convert.ToDecimal(gridSource[0].Field1) + Convert.ToDecimal(txtcombo_totalprice.Text);
                        txtcombo_totalprice.Text = Convert.ToString(totalamt);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void CalcTotalAmtCombo(int pid, int qty)
        {
            try
            {
                int rowno = 0;
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblProduct";
                objSearch.WhereNdGroupBy = "where ProductId=" + pid;
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    if (string.IsNullOrEmpty(txtcombo_totalprice.Text))
                        txtcombo_totalprice.Text = "0";
                    for (int i = 0; i < dgv_sele_cmb_products.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[cproductidno].Value) == pid)
                        {
                            dgv_sele_cmb_products.Rows[i].Cells[cqtynoo].Value = qty;
                            totalcellamount_Combo(i);
                            rowno = i;
                            break;
                        }
                        else
                        {
                            int rowIndex = dgv_sele_cmb_products.Rows.Count - 1;
                            dgv_sele_cmb_products.Rows[rowIndex].Cells[cqtynoo].Value = qty;
                            rowno = i;
                            totalcellamount_Combo(rowIndex);
                            break;
                        }
                    }
                    //discount calculation on product  reg time 
                    if (Convert.ToInt32(gridSource[0].DiscountPer) > 0)
                    {
                        decimal disAmnt = Convert.ToDecimal(gridSource[0].DiscountPer) * Convert.ToDecimal(gridSource[0].Price) / 100;
                        decimal finamt = Convert.ToDecimal(gridSource[0].Price) - disAmnt;
                        decimal totalamt = finamt + Convert.ToDecimal(txtcombo_totalprice.Text);
                        txtcombo_totalprice.Text = Convert.ToString(totalamt);
                    }
                    else
                    {
                        decimal totalamt = Convert.ToDecimal(gridSource[0].Price) + Convert.ToDecimal(txtcombo_totalprice.Text);
                        txtcombo_totalprice.Text = Convert.ToString(totalamt);
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
        #endregion

        private void Button_RemoveClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                txtcombo_disamt.Text = "0";
                txtcombo_disper.Text = "0";
                txtcombo_finalprice.Text = "0";
                string lblname = "", tlpname = "", itemname = "";
                Control clickedControl = sender as Control;
                if (clickedControl.AccessibleDescription == "1")
                {
                    lblname = "lbl_selected_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                    tlpname = "tlp_sele_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                    itemname = "tlp_sele_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                }
                else
                {
                    lblname = "lbl_selected_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    tlpname = "tlp_sele_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    itemname = "tlp_sele_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                }

                // Find the TableLayoutPanel control by name
                Label lblqty = this.Controls.Find(lblname, true).FirstOrDefault() as Label;
                if (lblqty != null)
                {
                    int cur_qty = Convert.ToInt32(lblqty.Text) - 1;
                    if (cur_qty == 0)
                    {
                        TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                        if (tlp_sele != null)
                        {
                            if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.Product))
                                CalcTotalAmtCombo_remove(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                            else
                                CalcTotalAmtCombo_remove_Prid(Convert.ToInt32(clickedControl.Tag), cur_qty);
                        }
                        foreach (Control control in flp_selected_item_combo.Controls)
                        {
                            if (control.Name == itemname)
                            {
                                flp_selected_item_combo.Controls.Remove(control);
                                break;
                            }
                        }

                    }
                    else
                    {
                        lblqty.Text = Convert.ToString(cur_qty);
                        TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                        if (tlp_sele != null)
                        {
                            if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.Product))
                                CalcTotalAmtCombo_remove(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                            else
                                CalcTotalAmtCombo_remove_Prid(Convert.ToInt32(clickedControl.Tag), cur_qty);
                        }
                    }

                }
                if (flp_selected_item_combo.Controls.Count == 0)
                    btn_combo_gridview.Visible = false;

            }
            catch (Exception ex)
            {
            }
        }

        #region REMOVE Total Amount single Price and Multiple Price
        public void CalcTotalAmtCombo_remove_Prid(int prid, int qty)
        {
            try
            {
                string lblqtyname = "lbl_selected_" + prid + "_" + Convert.ToInt32(Tabletype.DirectSale);
                Label lblqty = this.Controls.Find(lblqtyname, true).FirstOrDefault() as Label;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "SRate as Field1";
                objSearch.Table = "tblProductRate";
                objSearch.WhereNdGroupBy = "where id = " + prid;
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    if (string.IsNullOrEmpty(txtcombo_totalprice.Text))
                        txtcombo_totalprice.Text = "0";
                    if (qty == 0)
                    {
                        for (int i = 0; i <= dgv_sele_cmb_products.RowCount - 1; i++)
                        {
                            if (prid == Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[cpridno].Value) && Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[cproducttypeno].Value) == Convert.ToInt32(Tabletype.DirectSale))//&& Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[istatusno].Value) == 1
                            {
                                //discount calculation on item  reg time 
                                if (Convert.ToDecimal(dgv_sele_cmb_products.Rows[i].Cells[cdisperno].Value) > 0)
                                {
                                    decimal disAmnt = Convert.ToDecimal(dgv_sele_cmb_products.Rows[i].Cells[cdisperno].Value) * Convert.ToDecimal(gridSource[0].Field1) / 100;
                                    decimal finamt = Convert.ToDecimal(gridSource[0].Field1) - disAmnt;
                                    decimal totalamt = Convert.ToDecimal(txtcombo_totalprice.Text) - finamt;
                                    txtcombo_totalprice.Text = Convert.ToString(totalamt);
                                }
                                else
                                {
                                    decimal totalamt = Convert.ToDecimal(txtcombo_totalprice.Text) - Convert.ToDecimal(gridSource[0].Field1);
                                    txtcombo_totalprice.Text = Convert.ToString(totalamt);
                                }

                                dgv_sele_cmb_products.Rows.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (lblqty != null)
                            lblqty.Text = Convert.ToString(qty);
                        for (int i = 0; i <= dgv_sele_cmb_products.RowCount - 1; i++)
                        {
                            if (prid == Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[cpridno].Value) && Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[cproducttypeno].Value) == Convert.ToInt32(Tabletype.DirectSale))
                            {
                                dgv_sele_cmb_products.Rows[i].Cells[cqtynoo].Value = Convert.ToDecimal(dgv_sele_cmb_products.Rows[i].Cells[cqtynoo].Value) - 1;
                                //discount calculation on item  reg time 
                                if (Convert.ToDecimal(dgv_sele_cmb_products.Rows[i].Cells[cdisperno].Value) > 0)
                                {
                                    decimal disAmnt = Convert.ToDecimal(dgv_sele_cmb_products.Rows[i].Cells[cdisperno].Value) * Convert.ToDecimal(gridSource[0].Field1) / 100;
                                    decimal finamt = Convert.ToDecimal(gridSource[0].Field1) - disAmnt;
                                    decimal totalamt = Convert.ToDecimal(txtcombo_totalprice.Text) - finamt;
                                    txtcombo_totalprice.Text = Convert.ToString(totalamt);
                                }
                                else
                                {
                                    decimal totalamt = Convert.ToDecimal(gridSource[0].Field1) - Convert.ToDecimal(txtcombo_totalprice.Text);
                                    txtcombo_totalprice.Text = Convert.ToString(totalamt);
                                }
                                totalcellamount_Combo(i);
                                break;
                            }
                        }

                    }
                    discountAmnt_combo();
                    discountPer_combo();
                    ClacTaxAmt_combo();

                }
            }
            catch (Exception ex)
            {

            }

        }
        public void CalcTotalAmtCombo_remove(int pid, int qty)
        {
            try
            {
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblProduct";
                objSearch.WhereNdGroupBy = "where ProductId=" + pid;
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    bool flag = true;
                    if (string.IsNullOrEmpty(txtcombo_totalprice.Text))
                        txtcombo_totalprice.Text = "0";

                    for (int i = 0; i < dgv_sele_cmb_products.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[cproductidno].Value) == pid && Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[cproducttypeno].Value) == Convert.ToInt32(Tabletype.Product))
                        {
                            flag = false;
                            if (qty == 0)
                            {
                                dgv_sele_cmb_products.Rows.RemoveAt(i);
                                break;
                            }
                            else
                            {
                                dgv_sele_cmb_products.Rows[i].Cells[cqtynoo].Value = qty;
                                totalcellamount_Combo(i);
                                break;
                            }
                        }
                    }

                    if (Convert.ToInt32(gridSource[0].DiscountPer) > 0)
                    {
                        decimal disAmnt = Convert.ToDecimal(gridSource[0].DiscountPer) * Convert.ToDecimal(gridSource[0].Price) / 100;
                        decimal finamt = Convert.ToDecimal(gridSource[0].Price) - disAmnt;
                        decimal totalamt = Convert.ToDecimal(txtcombo_totalprice.Text) - finamt;
                        txtcombo_totalprice.Text = Convert.ToString(totalamt);
                    }
                    else
                    {
                        decimal totalamt = Convert.ToDecimal(txtcombo_totalprice.Text) - Convert.ToDecimal(gridSource[0].Price);
                        txtcombo_totalprice.Text = Convert.ToString(totalamt);
                    }

                    if (flag)
                    {
                        int rowIndex = dgv_sele_cmb_products.Rows.Count - 1;
                        dgv_sele_cmb_products.Rows[rowIndex].Cells[cqtynoo].Value = qty;
                    }


                    discountAmnt_combo();
                    discountPer_combo();
                    ClacTaxAmt_combo();
                    //netAmount_combo();
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion



        private void btn_AddcomboProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                ProductPopup(0);
            }
        }

        private void btn_serachproduct_close_Click(object sender, EventArgs e)
        {
            pnl_serachproducts.Visible = false;
        }
        public void Movefocus_Combo(int keytype)
        {
            if (btn_AddcomboProduct.Focused)
            {
                txtcmbo_offername.Focus();
            }
            else if (txtcmbo_offername.Focused)
            {
                dtp_from_date.Focus();
            }
            else if (dtp_from_date.Focused)
            {
                dtp_from_time.Focus();
            }
            else if (dtp_from_time.Focused)
            {
                dtp_to_date.Focus();
            }
            else if (dtp_to_date.Focused)
            {
                dtp_to_time.Focus();
            }
            else if (dtp_to_time.Focused)
            {
                cmbtax.Focus();
            }
            else if (cmbtax.Focused)
            {
                cmbtaxtype.Focus();
            }

            else if (cmbtaxtype.Focused)
            {
                if (cmbtaxtype.Text == "GST")
                {
                    cmbIgstPrcnt.Focus();
                }
                else if (cmbtaxtype.Text == "VAT")
                {
                    cmbVatPrcnt.Focus();
                }
            }
            else if (cmbIgstPrcnt.Focused)
                txtcombo_totalprice.Focus();
            else if (cmbVatPrcnt.Focused)
                txtcombo_totalprice.Focus();
            else if (txtcombo_totalprice.Focused)
            {
                txtcombo_disper.Focus();
            }
            else if (txtcombo_disper.Focused)
            {
                txtcombo_disamt.Focus();
            }
            else if (txtcombo_disamt.Focused)
            {
                txtcombodes.Focus();
            }
            else if (txtcombodes.Focused)
            {
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

        public void Movefocus_single(int keytype)
        {
            if (txtoffername_s.Focused)
            {
                dtp_from_date_s.Focus();
            }
            else if (dtp_from_date_s.Focused)
            {
                dtp_from_time_s.Focus();
            }
            else if (dtp_from_time_s.Focused)
            {
                dtp_to_date_s.Focus();
            }
            else if (dtp_to_date_s.Focused)
            {
                dtp_to_time_s.Focus();
            }
            else if (dtp_to_time_s.Focused)
            {
                txtofferdsiper_s.Focus();
            }
            else if (txtofferdsiper_s.Focused)
            {
                txtofferdes_s.Focus();
            }
            else if (txtofferdes_s.Focused)
            {
                dgv_sigle_product.Focus();
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
        #region Grid Flow Fuction
        private int GetNextVisibleColumn(int currentColumnIndex)
        {
            try
            {
                for (int i = currentColumnIndex + 1; i < dgv_sigle_product.ColumnCount; i++)
                {
                    if (dgv_sigle_product.Columns[i].Visible)
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
                for (int i = currentRowIndex + 1; i < dgv_sigle_product.RowCount; i++)
                {
                    if (dgv_sigle_product.Rows[i].Visible)
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
                for (int i = 0; i < dgv_sigle_product.ColumnCount; i++)
                {
                    if (dgv_sigle_product.Columns[i].Visible)
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
                    if (tlp_combo.Visible)
                    {
                        Movefocus_Combo(1);
                    }
                    else if (tlp_single.Visible)
                    {
                        if (dgv_sigle_product.Focused)
                        {
                            try
                            {
                                int currentRowIndex = dgv_sigle_product.CurrentCell.RowIndex;
                                int currentColumnIndex = dgv_sigle_product.CurrentCell.ColumnIndex;

                                int nextColumnIndex = GetNextVisibleColumn(currentColumnIndex);

                                if (nextColumnIndex != -1)
                                {
                                    dgv_sigle_product.CurrentCell = dgv_sigle_product[nextColumnIndex, currentRowIndex];
                                }
                                else
                                {
                                    int nextRowIndex = GetNextVisibleRow(currentRowIndex);
                                    if (nextRowIndex != -1)
                                    {
                                        dgv_sigle_product.CurrentCell = dgv_sigle_product[GetFirstVisibleColumn(), nextRowIndex];
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
                            Movefocus_single(1);
                    }
                    return true;
                case Keys.Enter:
                    if (txtitemcode_serach.Focused)
                        if (txtitemcode_serach.Text != "" && txtitemcode_serach.Text != null)
                            ItemCodeSerach_Combo();
                        else if (txtname_search.Focused)
                            if (txtname_search.Text != "" && txtname_search.Text != null)
                                NameSerach_Combo();
                            else if (txtnamepro_offer.Focused)
                            {
                                if (txtnamepro_offer.Text != "" && txtnamepro_offer.Text != null && txtnamepro_offer.Text != "Name Serach")
                                    NameSerachProoffer();
                            }
                            else if (txt_prodlist_serach.Focused)
                            {
                                UpcScanner(txt_prodlist_serach.Text);
                            }
                            else if (tlp_combo.Visible)
                            {
                                Movefocus_Combo(0);
                            }
                            else if (tlp_single.Visible)
                            {
                                if (dgv_sigle_product.Focused)
                                {
                                    try
                                    {
                                        int currentRowIndex = dgv_sigle_product.CurrentCell.RowIndex;
                                        int currentColumnIndex = dgv_sigle_product.CurrentCell.ColumnIndex;

                                        int nextColumnIndex = GetNextVisibleColumn(currentColumnIndex);

                                        if (nextColumnIndex != -1)
                                        {
                                            dgv_sigle_product.CurrentCell = dgv_sigle_product[nextColumnIndex, currentRowIndex];
                                        }
                                        else
                                        {
                                            int nextRowIndex = GetNextVisibleRow(currentRowIndex);
                                            if (nextRowIndex != -1)
                                            {
                                                dgv_sigle_product.CurrentCell = dgv_sigle_product[GetFirstVisibleColumn(), nextRowIndex];
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
                                    Movefocus_single(0);
                            }

                            else if (txt_prodlist_serach.Focused)
                                UpcScanner(txt_prodlist_serach.Text);
                            else if (txtbarcode.Focused)
                                UpcScanner_single(txtbarcode.Text);
                    return true; // Mark the key as handled
                case Keys.Escape:
                    if (pnl_serachproducts.Visible)
                        pnl_serachproducts.Visible = false;
                    else if (tlp_gridview.Visible)
                        tlp_gridview.Visible = false;
                    else if (pnl_variation_addon.Visible)
                        pnl_variation_addon.Visible = false;
                    else if (pnl_serachproducts.Visible)
                        pnl_serachproducts.Visible = false;
                    else
                    {
                        FrmDashboard _instancedash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                        _instancedash.Closeform();
                    }
                    return true;
                case Keys.Right:
                    Control focusedControl = this.ActiveControl;
                    if (focusedControl is Button)
                    {
                        Button focusedButton = (Button)focusedControl;
                        if (focusedButton.AccessibleDescription == "1")
                        {
                            Control firstControlInPanel = pnl_serachproduct_dashboard.Controls.OfType<Control>().FirstOrDefault();
                            if (firstControlInPanel != null)
                                firstControlInPanel.Focus();
                        }

                    }
                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void txtname_search_Enter(object sender, EventArgs e)
        {
            if (txtname_search.Text == "Food Name .....")
            {
                txtname_search.Text = "";
                txtname_search.ForeColor = Color.Black;
            }

        }

        private void txtitemcode_serach_Enter(object sender, EventArgs e)
        {
            if (txtitemcode_serach.Text == "Item Code .....")
            {
                txtitemcode_serach.Text = "";
                txtitemcode_serach.ForeColor = Color.Black;
            }
        }

        private void txtname_search_Leave(object sender, EventArgs e)
        {
            if (txtname_search.Text == "")
            {
                txtname_search.Text = "Food Name .....";
                txtname_search.ForeColor = Color.DarkGray;
            }
        }

        private void txtitemcode_serach_Leave(object sender, EventArgs e)
        {
            if (txtitemcode_serach.Text == "")
            {
                txtitemcode_serach.Text = "Item Code .....";
                txtitemcode_serach.ForeColor = Color.DarkGray;
            }
        }

        private void txtcombo_disper_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtcombo_disamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        public void discountAmnt_combo()
        {
            if (txtcombo_disper.Text != "")
            {
                if (Convert.ToDecimal(txtcombo_disper.Text) > 100)
                {
                    CMessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcombo_disper.Focus();
                }
                else
                {
                    if ((string.IsNullOrEmpty(txtcombo_totalprice.Text)))
                        txtcombo_totalprice.Text = "0.00";
                    decimal discountAmnt = Convert.ToDecimal(txtcombo_disper.Text.Trim()) * Convert.ToDecimal(txtcombo_totalprice.Text) / 100;
                    txtcombo_disamt.Text = discountAmnt.ToString("N2");
                }
            }
        }
        public void discountPer_combo()
        {
            if (txtcombo_disamt.Text != "" && txtcombo_totalprice.Text != "" && Convert.ToDecimal(txtcombo_totalprice.Text) != 0)
            {
                if (Convert.ToDecimal(txtcombo_disamt.Text) > Convert.ToDecimal(txtcombo_totalprice.Text))
                {
                    CMessageBox.Show("Discount Amount Greater than Total Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcombo_disamt.Focus();
                }
                else
                {
                    decimal discountPer = (Convert.ToDecimal(txtcombo_disamt.Text) / Convert.ToDecimal(txtcombo_totalprice.Text) * 100);
                    txtcombo_disper.Text = discountPer.ToString("N2");
                }
            }
        }

        private void txtcombo_disper_Leave(object sender, EventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtcombo_disamt.Text, out value))
            {
                txtcombo_disamt.Text = "0";
            }
            discountAmnt_combo();
            ClacTaxAmt_combo();
            //netAmount_combo();
        }

        private void txtcombo_disamt_Leave(object sender, EventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtcombo_disamt.Text, out value))
            {
                txtcombo_disamt.Text = "0";
            }
            discountPer_combo();
            ClacTaxAmt_combo();
            //netAmount_combo();
        }
        //public void netAmount_combo()
        //{
        //    try
        //    {
        //        if ((string.IsNullOrEmpty(txtcombo_finalprice.Text)))
        //            txtcombo_finalprice.Text = "0.00";
        //        decimal netAmount = Convert.ToDecimal(txtcombo_totalprice.Text);
        //        txtcombo_finalprice.Text = netAmount.ToString("N2");
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
        public void ClacTaxAmt_combo()
        {
            try
            {
                if ((string.IsNullOrEmpty(txttaxamt.Text)))
                    txttaxamt.Text = "0.00";
                if ((string.IsNullOrEmpty(txtcombo_totalprice.Text)))
                    txtcombo_totalprice.Text = "0.00";
                if ((string.IsNullOrEmpty(txtcombo_finalprice.Text)))
                    txtcombo_finalprice.Text = "0.00";
                if ((string.IsNullOrEmpty(txttaxper_combo.Text)))
                    txttaxper_combo.Text = "0";

                if (cmbtaxtype.Text == "GST")
                {
                    string val = cmbIgstPrcnt.GetItemText(cmbIgstPrcnt.SelectedItem);
                    decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(val) * Convert.ToDecimal(txtcombo_totalprice.Text) / 100);
                    txttaxamt.Text = TaxAmount.ToString("N2");
                    txttaxper_combo.Text = val;
                }
                else if (cmbtaxtype.Text == "VAT")
                {
                    string val = cmbIgstPrcnt.GetItemText(cmbVatPrcnt.SelectedItem);
                    decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(val) * Convert.ToDecimal(txtcombo_totalprice.Text) / 100);
                    txttaxamt.Text = TaxAmount.ToString("N2");
                    txttaxper_combo.Text = val;

                }

                if (cmbtax.Text == "Exclusive")
                {
                    decimal tot = (Convert.ToDecimal(txtcombo_totalprice.Text) - Convert.ToDecimal(txtcombo_disamt.Text))
                    + Convert.ToDecimal(txttaxamt.Text);
                    txtcombo_finalprice.Text = tot.ToString("N2");
                }
                else if (cmbtax.Text == "Inclusive")
                {
                    decimal tot = (Convert.ToDecimal(txtcombo_totalprice.Text) - Convert.ToDecimal(txtcombo_disamt.Text));
                    txtcombo_finalprice.Text = tot.ToString("N2");
                }
            }
            catch (Exception ex)
            {

            }

        }




        public bool MandatoryCheck_Combo()
        {
            if ((string.IsNullOrEmpty(txtcombo_omid.Text)))
                txtcombo_omid.Text = "0";
            if (flp_selected_item_combo.Controls.Count == 0 && Convert.ToInt32(txtcombo_omid.Text) > 0)
            {
                var result = CMessageBox.Show("Do You want Delete This Combo", "Confirm The Action", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    chk_combo_status.Checked = false;
                    return true;
                }
                else
                    return false;

            }
            else if (flp_selected_item_combo.Controls.Count == 1)
            {
                CMessageBox.Show("Minimium Two Products Added To The List", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btn_AddcomboProduct.Focus();
                return false;
            }

            else if (flp_selected_item_combo.Controls.Count == 0 && Convert.ToInt32(txtcombo_omid.Text) == 0)
            {
                CMessageBox.Show("Please Select the Products", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btn_AddcomboProduct.Focus();
                return false;
            }

            else if (string.IsNullOrEmpty(txtcmbo_offername.Text))
            {
                CMessageBox.Show("Please Enter the Offer Name", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcmbo_offername.Focus();
                return false;
            }
            else if (dtp_to_date.Value == dtp_from_date.Value && dtp_to_date.Value.TimeOfDay == dtp_from_date.Value.TimeOfDay)
            {
                CMessageBox.Show("From' date cannot be the same as the 'To' date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtp_to_date.Focus();
                return false;
            }
            else if (dtp_from_date.Value > dtp_to_date.Value)
            {
                CMessageBox.Show("To Date Cannot Be Before From Date. Please Select a Valid Date.");
                dtp_to_date.Focus();
                return false;
            }
            else if (cmbtax.Text == "")
            {
                CMessageBox.Show("Please select Tax", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbtax.Focus();
                return false;
            }
            else if (cmbtaxtype.Text == "")
            {
                CMessageBox.Show("Please select TaxType", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbtaxtype.Focus();
                return false;
            }
            else if (cmbtaxtype.Text == "GST")
            {
                if (cmbIgstPrcnt.SelectedIndex == 0)
                {
                    CMessageBox.Show("Please select IGST %", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbIgstPrcnt.Focus();
                    return false;
                }

            }
            else if (cmbtaxtype.Text == "VAT")
            {
                if (cmbVatPrcnt.SelectedIndex == 0)
                {
                    CMessageBox.Show("Please select VAT %", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbVatPrcnt.Focus();
                    return false;
                }

            }
            //else if (string.IsNullOrEmpty(txtcombo_disper.Text) || Convert.ToDecimal(txtcombo_disper.Text)==0)
            //{
            //    CMessageBox.Show("Please Enter the Discount %", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtcombo_disper.Focus();
            //    return false;
            //}
            //if (string.IsNullOrEmpty(txtcombo_finalprice.Text))
            //{
            //    CMessageBox.Show("Please Enter the Final price", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtcombo_finalprice.Focus();
            //    return false;
            //}
            //if (Convert.ToDecimal(txtcombo_totalprice.Text) > Convert.ToDecimal(txtcombo_finalprice.Text))
            //{
            //    CMessageBox.Show("Final amount not be greater than Total Amount ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtcombo_finalprice.Focus();
            //    return false;
            //}

            return true;
        }

        private void txtcombo_totalprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtcombo_finalprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void tlp_comboproduct_view_Paint(object sender, PaintEventArgs e)
        {
            float borderRadius = 10f; // adjust this value to change the roundness of the corners
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(tlp_comboproduct_view.ClientRectangle.X, tlp_comboproduct_view.ClientRectangle.Y, tlp_comboproduct_view.ClientRectangle.Width, tlp_comboproduct_view.ClientRectangle.Height);
            path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            path.CloseFigure();
            tlp_comboproduct_view.Region = new Region(path);
        }

        private void flp_selected_item_combo_Paint(object sender, PaintEventArgs e)
        {
            float borderRadius = 10f; // adjust this value to change the roundness of the corners
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(flp_selected_item_combo.ClientRectangle.X, flp_selected_item_combo.ClientRectangle.Y, flp_selected_item_combo.ClientRectangle.Width, flp_selected_item_combo.ClientRectangle.Height);
            path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            path.CloseFigure();
            flp_selected_item_combo.Region = new Region(path);
        }

        private void pnl_serachproduct_dashboard_Paint(object sender, PaintEventArgs e)
        {
            float borderRadius = 10f; // adjust this value to change the roundness of the corners
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(pnl_serachproduct_dashboard.ClientRectangle.X, pnl_serachproduct_dashboard.ClientRectangle.Y, pnl_serachproduct_dashboard.ClientRectangle.Width, pnl_serachproduct_dashboard.ClientRectangle.Height);
            path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            path.CloseFigure();
            pnl_serachproduct_dashboard.Region = new Region(path);
        }
        public void NameSerach_Combo()
        {
            try
            {

                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblProduct";
                dmnSearch.WhereNdGroupBy = "where Status=1 and Product LIKE '%" + txtname_search.Text + "%'";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                pnl_serachproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                tlp_searchproduct_dashboard.AutoScroll = true;
                if (gridSource.Count > 0)
                {
                    int rowIndex = 1;
                    int columnIndex = 0;
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        if (columnIndex == 3)
                        {
                            columnIndex = 0;
                            rowIndex++;
                        }
                        // Configure the Button
                        Button button = new Button();
                        button.Text = gridSource[i].Product;
                        button.Name = "btn" + gridSource[i].Product;
                        button.Tag = gridSource[i].ProductId;
                        button.Width = 170;
                        button.Height = 50;

                        // Set the FlatAppearance properties to remove the border
                        button.FlatAppearance.BorderSize = 0; // Set border width to 0
                        button.FlatAppearance.BorderColor = button.BackColor;
                        button.FlatStyle = FlatStyle.Flat;
                        button.BackColor = product_buttoncolor;
                        button.ForeColor = Color.White;

                        // Create a circular Region
                        //System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                        //path.AddEllipse(0, 0, button.Width, button.Height);
                        //button.Region = new Region(path);
                        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));

                        button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                        //button.Dock = DockStyle.Fill;
                        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                        tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                        columnIndex++;
                    }
                    pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                }
                else
                    CMessageBox.Show("No Products", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {

            }
        }
        public void ItemCodeSerach_Combo()
        {
            try
            {

                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblProduct";
                dmnSearch.WhereNdGroupBy = "where Status=1 and ItemCode = '" + txtitemcode_serach.Text + "'";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                pnl_serachproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                tlp_searchproduct_dashboard.AutoScroll = true;
                if (gridSource.Count > 0)
                {

                    int rowIndex = 1;
                    int columnIndex = 0;
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        if (columnIndex == 3)
                        {
                            columnIndex = 0;
                            rowIndex++;
                        }
                        // Configure the Button
                        Button button = new Button();
                        button.Text = gridSource[i].Product;
                        button.Name = "btn" + gridSource[i].Product;
                        button.Tag = gridSource[i].ProductId;
                        button.Width = 170;
                        button.Height = 50;

                        // Set the FlatAppearance properties to remove the border
                        button.FlatAppearance.BorderSize = 0; // Set border width to 0
                        button.FlatAppearance.BorderColor = button.BackColor;
                        button.FlatStyle = FlatStyle.Flat;
                        button.BackColor = product_buttoncolor;
                        button.ForeColor = Color.White;

                        // Create a circular Region
                        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                        //System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                        //path.AddEllipse(0, 0, button.Width, button.Height);
                        //button.Region = new Region(path);

                        button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                        //button.Dock = DockStyle.Fill;
                        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                        tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                        columnIndex++;
                    }
                    pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                }
                else
                    CMessageBox.Show("No Products", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {

            }
        }


        private void txtname_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                e.Handled = true;
        }

        private void txtitemcode_serach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                e.Handled = true;
        }

        private void btn_del_combo_Click(object sender, EventArgs e)
        {
            var result = CMessageBox.Show("Do You want Delete This Combo", "Confirm The Action", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                chk_combo_status.Checked = false;
                btnsave.PerformClick();
            }
        }

        private void txtcombo_finalprice_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtcombo_totalprice.Text))
                txtcombo_totalprice.Text = "0";
            if (string.IsNullOrEmpty(txtcombo_finalprice.Text))
                txtcombo_finalprice.Text = "0";
            //if (Convert.ToDecimal(txtcombo_totalprice.Text) > Convert.ToDecimal(txtcombo_finalprice.Text))
            //{
            //    CMessageBox.Show("Final amount not be greater than Tota Amount ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtcombo_finalprice.Focus();
            //}
        }

        #region Product offer datetime evnet
        private void dtp_from_date_s_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (LoadFlag_Product == false)
                {
                    DateTime selectedDate = dtp_from_date_s.Value.Date;
                    DateTime selectedDate_To = dtp_to_date_s.Value.Date;
                    DateTime currentDate = DateTime.Now.Date;

                    if (selectedDate == currentDate)
                    {
                        dtp_from_time_s.MinDate = DateTime.Now;
                        dtp_from_time_s.Value = DateTime.Now;
                    }
                    else
                    {
                        dtp_from_time_s.MinDate = dtp_from_date_s.Value.Date;
                    }

                    if (selectedDate > selectedDate_To)
                        dtp_to_date_s.Value = dtp_from_date_s.Value;
                    dtp_to_date_s.MinDate = dtp_from_date_s.Value;

                    selectedDate_To = dtp_to_date_s.Value.Date;
                    if (selectedDate_To == currentDate)
                    {
                        dtp_to_time_s.MinDate = DateTime.Now;
                        dtp_to_time_s.Value = DateTime.Now;
                    }
                    else
                        dtp_to_time_s.MinDate = dtp_to_date_s.Value.Date;
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void dtp_to_date_s_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (LoadFlag_Product == false)
                {
                    DateTime selectedDate = dtp_to_date_s.Value.Date;
                    DateTime currentDate = DateTime.Now.Date;
                    if (selectedDate == currentDate)
                    {
                        dtp_to_time_s.MinDate = DateTime.Now;
                        dtp_to_time_s.Value = DateTime.Now;
                    }
                    else
                    {
                        dtp_to_time_s.MinDate = dtp_to_date_s.Value.Date;
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
        #endregion


        #region Combo offer
        private void dtp_to_date_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (LoadFlag == false)
                {
                    DateTime selectedDate = dtp_to_date.Value.Date;
                    DateTime currentDate = DateTime.Now.Date;
                    if (selectedDate == currentDate)
                    {
                        dtp_to_time.MinDate = DateTime.Now;
                        dtp_to_time.Value = DateTime.Now;
                    }
                    else
                    {
                        dtp_to_time.MinDate = dtp_to_date.Value.Date;
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }
        private void dtp_from_date_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (LoadFlag == false)
                {
                    DateTime selectedDate = dtp_from_date.Value.Date;
                    DateTime selectedDate_To = dtp_to_date.Value.Date;
                    DateTime currentDate = DateTime.Now.Date;

                    if (selectedDate == currentDate)
                    {
                        dtp_from_time.MinDate = DateTime.Now;
                        dtp_from_time.Value = DateTime.Now;
                    }
                    else
                    {
                        dtp_from_time.MinDate = dtp_from_date.Value.Date;
                    }

                    if (selectedDate > selectedDate_To)
                        dtp_to_date.Value = dtp_from_date.Value;
                    dtp_to_date.MinDate = dtp_from_date.Value;

                    selectedDate_To = dtp_to_date.Value.Date;
                    if (selectedDate_To == currentDate)
                    {
                        dtp_to_time.MinDate = DateTime.Now;
                        dtp_to_time.Value = DateTime.Now;
                    }
                    else
                        dtp_to_time.MinDate = dtp_to_date.Value.Date;
                }

            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        private void btn_grid_product_close_Click(object sender, EventArgs e)
        {
            tlp_product_list.Visible = false;
        }
        public void cellCaulcation(int rowno)
        {
            try
            {
                cellDiscountAmount(rowno);
                cellTaxAmount(rowno, Convert.ToDecimal(dgv_sigle_product.Rows[rowno].Cells[spriceno].Value));

                if (Convert.ToString(dgv_sigle_product.Rows[rowno].Cells[staxno].Value) == "Exclusive")
                {
                    decimal tot = (Convert.ToDecimal(dgv_sigle_product.Rows[rowno].Cells[spriceno].Value) - Convert.ToDecimal(dgv_sigle_product.Rows[rowno].Cells[soffferdisamtno].Value))
                    + Convert.ToDecimal(dgv_sigle_product.Rows[rowno].Cells[staxamtno].Value);
                    dgv_sigle_product.Rows[rowno].Cells[sfinalpriceno].Value = tot.ToString("N2");
                }
                if (Convert.ToString(dgv_sigle_product.Rows[rowno].Cells[staxno].Value) == "Inclusive")
                {
                    decimal tot = (Convert.ToDecimal(dgv_sigle_product.Rows[rowno].Cells[spriceno].Value) - Convert.ToDecimal(dgv_sigle_product.Rows[rowno].Cells[soffferdisamtno].Value));
                    dgv_sigle_product.Rows[rowno].Cells[sfinalpriceno].Value = tot.ToString("N2");
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void dgv_product_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                int rowindex = e.RowIndex;
                if (Convert.ToInt32(dgv_product_list.Rows[e.RowIndex].Cells[dsaletypeno].Value) == 1)
                {
                    int pid = Convert.ToInt32(dgv_product_list.Rows[e.RowIndex].Cells[dproductidno].Value);
                    int saletype = Convert.ToInt32(dgv_product_list.Rows[e.RowIndex].Cells[dsaletypeno].Value);
                    if (checkOffer_product(pid, saletype))
                    {
                        #region Check MultiplePrice
                        BllSearch<DmnGridFields> bllSearch1 = new BllSearch<DmnGridFields>();
                        DmnSearch objSearch = new DmnSearch();
                        objSearch.Select = "Top(1) pr.SRate Field3,i.DiscountPer Field2,i.Itemname Field1,tx.IgstPrcnt Field4,tx.VatPrcnt Field5,i.TaxType Field6,i.Tax Field7,i.ItemId Field8,pr.Id Field9,i.TaxSettingsId Field10,i.ItemCode_UPC Field11";
                        objSearch.Table = "tblitem i left join tblProductRate pr on pr.ProductId = i.ItemId LEFT JOIN tblTaxSettings tx on tx.TaxSettingsId=i.TaxSettingsId";
                        objSearch.WhereNdGroupBy = "where i.status = 1 and  pr.QOH > 0 and i.itemid=" + Convert.ToInt32(dgv_product_list.Rows[e.RowIndex].Cells[dproductidno].Value);
                        objSearch.OrderBy = "";
                        var gridSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                        if (gridSource.Count > 0)
                        {
                            for (int i = 0; i < dgv_sigle_product.RowCount; i++)
                            {
                                if (Convert.ToInt32(dgv_sigle_product.Rows[i].Cells[sproductidno].Value) == Convert.ToInt32(gridSource[0].Field8) &&
                                   Convert.ToInt32(dgv_sigle_product.Rows[i].Cells[stabletypeno].Value) == Convert.ToInt32(Tabletype.DirectSale))
                                {
                                    CMessageBox.Show("Item Already on Offer list", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }

                            tlp_product_list.Visible = false;
                            int r = dgv_sigle_product.Rows.Count - 1;
                            dgv_sigle_product.Rows[r].Cells[sslno].Value = r + 1;
                            dgv_sigle_product.Rows[r].Cells[sbarcodeno].Value = gridSource[0].Field11;
                            dgv_sigle_product.Rows[r].Cells[sproductno].Value = gridSource[0].Field1;
                            dgv_sigle_product.Rows[r].Cells[srundisper].Value = gridSource[0].Field2;
                            dgv_sigle_product.Rows[r].Cells[spriceno].Value = gridSource[0].Field3;
                            if (Convert.ToString(gridSource[0].Field6) == "GST")
                                dgv_sigle_product.Rows[r].Cells[staxperno].Value = gridSource[0].Field4;
                            else
                                dgv_sigle_product.Rows[r].Cells[staxperno].Value = gridSource[0].Field5;
                            dgv_sigle_product.Rows[r].Cells[staxtypeno].Value = gridSource[0].Field6;
                            dgv_sigle_product.Rows[r].Cells[staxno].Value = gridSource[0].Field7;
                            dgv_sigle_product.Rows[r].Cells[sproductidno].Value = gridSource[0].Field8;
                            dgv_sigle_product.Rows[r].Cells[spridno].Value = gridSource[0].Field9;
                            dgv_sigle_product.Rows[r].Cells[staxsettingidno].Value = gridSource[0].Field10;
                            dgv_sigle_product.Rows[r].Cells[sstatusno].Value = 1;
                            dgv_sigle_product.Rows[r].Cells[stabletypeno].Value = Convert.ToInt32(Tabletype.DirectSale);
                            cellCaulcation(r);


                            DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_sigle_product.Rows[r].Cells[sactionno];
                            cellcolumntype.Value = Properties.Resources.delete;
                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            dgv_sigle_product.Rows.Add();

                            r = dgv_sigle_product.Rows.Count - 1;
                            cellcolumntype = (DataGridViewImageCell)dgv_sigle_product.Rows[r].Cells[sactionno];
                            if (r % 2 == 0)
                                cellcolumntype.Value = Properties.Resources.grid_unslect_color;
                            else
                                cellcolumntype.Value = Properties.Resources.grid_select;
                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

                        }
                    }

                    #endregion
                }
                else
                {
                    tlp_product_list.Visible = false;
                    int pid = Convert.ToInt32(dgv_product_list.Rows[e.RowIndex].Cells[dproductidno].Value);
                    int saletype = Convert.ToInt32(dgv_product_list.Rows[e.RowIndex].Cells[dsaletypeno].Value);
                    if (checkOffer_product(pid, saletype))
                    {
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                        DmnSearch objSearch = new DmnSearch();
                        objSearch = new DmnSearch();
                        objSearch.Select = "p.Product Field1,p.DiscountPer Field2,p.Price Field3,tx.IgstPrcnt Field4,tx.VatPrcnt Field5,p.TaxType Field6,p.Tax Field7,p.ProductId Field8,p.TaxSettingsId Field9,p.ItemCode Field10";
                        objSearch.Table = "tblProduct  p LEFT JOIN tblTaxSettings tx on tx.TaxSettingsId=p.TaxSettingsId";
                        objSearch.WhereNdGroupBy = "where ProductId=" + Convert.ToInt32(dgv_product_list.Rows[e.RowIndex].Cells[dproductidno].Value);
                        objSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                        if (gridSource.Count > 0)
                        {
                            for (int i = 0; i < dgv_sigle_product.RowCount; i++)
                            {
                                if (Convert.ToInt32(dgv_sigle_product.Rows[i].Cells[sproductidno].Value) == Convert.ToInt32(gridSource[0].Field8) &&
                                   Convert.ToInt32(dgv_sigle_product.Rows[i].Cells[stabletypeno].Value) == Convert.ToInt32(Tabletype.Product))
                                {
                                    CMessageBox.Show("Item Already on Offer list", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            int r = dgv_sigle_product.Rows.Count - 1;
                            dgv_sigle_product.Rows[r].Cells[sslno].Value = r + 1;
                            dgv_sigle_product.Rows[r].Cells[sbarcodeno].Value = gridSource[0].Field10;
                            dgv_sigle_product.Rows[r].Cells[sproductno].Value = gridSource[0].Field1;
                            dgv_sigle_product.Rows[r].Cells[srundisper].Value = gridSource[0].Field2;
                            dgv_sigle_product.Rows[r].Cells[spriceno].Value = gridSource[0].Field3;
                            if (Convert.ToString(gridSource[0].Field6) == "GST")
                                dgv_sigle_product.Rows[r].Cells[staxperno].Value = gridSource[0].Field4;
                            else
                                dgv_sigle_product.Rows[r].Cells[staxperno].Value = gridSource[0].Field5;
                            dgv_sigle_product.Rows[r].Cells[staxtypeno].Value = gridSource[0].Field6;
                            dgv_sigle_product.Rows[r].Cells[staxno].Value = gridSource[0].Field7;
                            dgv_sigle_product.Rows[r].Cells[sproductidno].Value = gridSource[0].Field8;
                            dgv_sigle_product.Rows[r].Cells[staxsettingidno].Value = gridSource[0].Field9;
                            dgv_sigle_product.Rows[r].Cells[sstatusno].Value = 1;
                            dgv_sigle_product.Rows[r].Cells[stabletypeno].Value = Convert.ToInt32(Tabletype.Product);
                            cellCaulcation(r);

                            DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_sigle_product.Rows[r].Cells[sactionno];
                            cellcolumntype.Value = Properties.Resources.delete;
                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            dgv_sigle_product.Rows.Add();

                            r = dgv_sigle_product.Rows.Count - 1;
                            cellcolumntype = (DataGridViewImageCell)dgv_sigle_product.Rows[r].Cells[sactionno];
                            if (r % 2 == 0)
                                cellcolumntype.Value = Properties.Resources.grid_unslect_color;
                            else
                                cellcolumntype.Value = Properties.Resources.grid_select;
                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void closebuttonClickEventHandler(object sender, EventArgs e)
        {
            pnlprice.Visible = false;
        }

        //private void MyButtonComboClickEventHandler(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Control clickedControl = sender as Control;
        //        string buttonname = "btn_sele_" + clickedControl.AccessibleDescription;
        //        Button buttonitem = this.Controls.Find(buttonname, true).FirstOrDefault() as Button;
        //        if (buttonitem != null)
        //        {
        //            string lblname = "lbl_selected_" + clickedControl.AccessibleDescription;
        //            Label lblqty = this.Controls.Find(lblname, true).FirstOrDefault() as Label;
        //            int cur_qty = Convert.ToInt32(lblqty.Text) + 1;
        //            lblqty.Text = Convert.ToString(cur_qty);
        //            CalcTotalAmtCombo_prid(Convert.ToInt32(clickedControl.AccessibleDescription), 1);
        //            pnl_serachproducts.Visible = false;
        //        }
        //        else
        //        {
        //            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
        //            DmnSearch objSearch = new DmnSearch();
        //            objSearch = new DmnSearch();
        //            objSearch.Select = "p.Product Field1,p.DiscountPer Field2,p.Price Field3,tx.IgstPrcnt Field4,tx.VatPrcnt Field5,p.TaxType Field6,p.Tax Field7,p.ProductId Field8,p.TaxSettingsId Field9,p.ItemCode Field10";
        //            objSearch.Table = "tblProduct  p LEFT JOIN tblTaxSettings tx on tx.TaxSettingsId=p.TaxSettingsId";
        //            objSearch.WhereNdGroupBy = "where ProductId=" + clickedControl.Tag;
        //            objSearch.OrderBy = "";
        //            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
        //            if (gridSource.Count > 0)
        //            {
        //                #region Grid Fill
        //                dgv_sele_cmb_products.Rows.Add();                    
        //                int r = dgv_sele_cmb_products.Rows.Count - 1;
        //                dgv_sele_cmb_products.Rows[r].Cells[sslno].Value = r + 1;
        //                dgv_sele_cmb_products.Rows[r].Cells[cbarcodeno].Value = gridSource[0].Field10;
        //                dgv_sele_cmb_products.Rows[r].Cells[Cproductno].Value = gridSource[0].Field1;
        //                dgv_sele_cmb_products.Rows[r].Cells[cpriceno].Value = gridSource[0].Field3;
        //                dgv_sele_cmb_products.Rows[r].Cells[cqtynoo].Value = 1;
        //                if (Convert.ToString(gridSource[0].Field6) == "GST")
        //                    dgv_sele_cmb_products.Rows[r].Cells[ctaxperno].Value = gridSource[0].Field4;
        //                else
        //                    dgv_sele_cmb_products.Rows[r].Cells[ctaxperno].Value = gridSource[0].Field5;
        //                dgv_sele_cmb_products.Rows[r].Cells[ctaxtypeno].Value = gridSource[0].Field6;
        //                dgv_sele_cmb_products.Rows[r].Cells[cproducttypeno].Value = Convert.ToInt32(Tabletype.Product);
        //                dgv_sele_cmb_products.Rows[r].Cells[ctaxno].Value = gridSource[0].Field7;
        //                dgv_sele_cmb_products.Rows[r].Cells[cproductidno].Value = gridSource[0].Field8;
        //                dgv_sele_cmb_products.Rows[r].Cells[Ctaxsettingidno].Value = gridSource[0].Field9;
        //                dgv_sele_cmb_products.Rows[r].Cells[cstatusno].Value = 1;
        //                dgv_sele_cmb_products.Rows[r].Cells[stabletypeno].Value = Convert.ToInt32(Tabletype.Product);
        //                dgv_sele_cmb_products.Rows[r].Cells[cpridno].Value = clickedControl.AccessibleDescription; 

        //                 //cellCaulcation(r);

        //                #endregion
        //            }

        //            CalcTotalAmtCombo_prid(Convert.ToInt32(clickedControl.AccessibleDescription), 1);
        //            TableLayoutPanel tlp_selected_Item_Box = new TableLayoutPanel();

        //            tlp_selected_Item_Box.Name = "tlp_sele_" + clickedControl.AccessibleDescription;
        //            tlp_selected_Item_Box.AccessibleDescription = "1";
        //            tlp_selected_Item_Box.Width = 245;
        //            tlp_selected_Item_Box.Height = 60;
        //            tlp_selected_Item_Box.ColumnCount = 4;
        //            tlp_selected_Item_Box.BorderStyle = BorderStyle.FixedSingle;
        //            tlp_selected_Item_Box.BackColor = seleproduct_buttoncolor;

        //            //Item Button   
        //            Button Button_Item = new Button();
        //            Button_Item.Text = clickedControl.Name;
        //            Button_Item.Name = "btn_sele_" + clickedControl.AccessibleDescription;
        //            Button_Item.Tag = clickedControl.AccessibleDescription;
        //            Button_Item.Width = 150;
        //            Button_Item.Height = 50;
        //            Button_Item.FlatAppearance.BorderSize = 0;
        //            Button_Item.FlatAppearance.BorderColor = Button_Item.BackColor;
        //            Button_Item.FlatStyle = FlatStyle.Flat;
        //            Button_Item.BackColor = Color.White;
        //            Button_Item.Click += new EventHandler(Button_ItemClickEventHandler);
        //            Button_Item.Font = new Font(Button_Item.Font.FontFamily, 10, FontStyle.Bold);
        //            // Adding the button to the third column
        //            tlp_selected_Item_Box.Controls.Add(Button_Item, 0, 0);

        //            // Creating the label for the Qty
        //            Label label = new Label();
        //            label.Text = "1";  // Set your desired text
        //            label.Name = "lbl_selected_" + clickedControl.AccessibleDescription;
        //            label.Tag = clickedControl.AccessibleDescription;
        //            label.AutoSize = true;
        //            label.Dock = DockStyle.Fill;
        //            label.ForeColor = Color.White;
        //            label.TextAlign = ContentAlignment.MiddleCenter;
        //            label.Font = new Font(label.Font.FontFamily, 10, FontStyle.Bold);
        //            // Adding the label to the second column
        //            tlp_selected_Item_Box.Controls.Add(label, 1, 0);

        //            //Add Button
        //            Button Button_Add = new Button();
        //            Button_Add.Text = "+";
        //            Button_Add.Name = "btn_sele_add_" + clickedControl.AccessibleDescription;
        //            Button_Add.Tag = clickedControl.AccessibleDescription;
        //            Button_Add.Width = 20;
        //            Button_Add.Height = 50;
        //            Button_Add.FlatAppearance.BorderSize = 0;
        //            Button_Add.FlatAppearance.BorderColor = Button_Add.BackColor;
        //            Button_Add.FlatStyle = FlatStyle.Flat;
        //            Button_Add.BackColor = Color.White;
        //            Button_Add.Click += new EventHandler(Button_AddClickEventHandler);
        //            Button_Add.Font = new Font(Button_Add.Font.FontFamily, 10, FontStyle.Bold);
        //            Button_Add.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Button_Add.Width, Button_Add.Height, Button_Add.Width, Button_Add.Height));
        //            // Adding the button to the third column
        //            tlp_selected_Item_Box.Controls.Add(Button_Add, 2, 0);

        //            //Remove Button
        //            Button Button_Remove = new Button();
        //            Button_Remove.Text = "-";
        //            Button_Remove.Name = "btn_sele_rem_" + clickedControl.AccessibleDescription;
        //            Button_Remove.Tag = clickedControl.AccessibleDescription;
        //            Button_Remove.Width = 20;
        //            Button_Remove.Height = 50;
        //            Button_Remove.Dock = DockStyle.Left;
        //            Button_Remove.FlatAppearance.BorderSize = 0;
        //            Button_Remove.FlatAppearance.BorderColor = Button_Remove.BackColor;
        //            Button_Remove.FlatStyle = FlatStyle.Flat;
        //            Button_Remove.BackColor = Color.White;
        //            Button_Remove.Click += new EventHandler(Button_RemoveClickEventHandler);
        //            Button_Remove.Font = new Font(Button_Remove.Font.FontFamily, 10, FontStyle.Bold);
        //            Button_Remove.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Button_Remove.Width, Button_Remove.Height, Button_Remove.Width, Button_Remove.Height));
        //            // Adding the button to the third column
        //            tlp_selected_Item_Box.Controls.Add(Button_Remove, 3, 0);
        //            flp_selected_item_combo.Controls.Add(tlp_selected_Item_Box);
        //            tlp_selected_Item_Box.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, tlp_selected_Item_Box.Width, tlp_selected_Item_Box.Height, 10, 10));
        //            pnl_serachproducts.Visible = false;
        //            discountAmnt_combo();
        //            discountPer_combo();
        //            netAmount_combo();
        //            pnlprice.Visible = false;
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }

        //}



        private void btn_view_Click(object sender, EventArgs e)
        {
            if (dgv_sele_cmb_products.Visible)
                dgv_sele_cmb_products.Visible = false;
            else
                dgv_sele_cmb_products.Visible = true;
        }

        private void cmbtaxtype_SelectedIndexChanged(object sender, EventArgs e)
        {

            Control controlToRemove1 = tlp_comboproduct_view.GetControlFromPosition(2, 2);
            tlp_comboproduct_view.Controls.Remove(controlToRemove1);
            Control controlToRemove2 = tlp_comboproduct_view.GetControlFromPosition(2, 3);
            tlp_comboproduct_view.Controls.Remove(controlToRemove2);
            txttaxamt.Dock = DockStyle.Fill;

            if (cmbtaxtype.Text == "GST")
            {
                lbligst.Visible = true;
                cmbIgstPrcnt.Visible = true;

                lblvat.Visible = false;
                cmbVatPrcnt.Visible = false;

                controlToRemove1 = tlp_taxtype_taxamt_lbl.GetControlFromPosition(0, 1);
                tlp_taxtype_taxamt_lbl.Controls.Remove(controlToRemove1);
                tlp_taxtype_taxamt_lbl.Controls.Add(lbligst, 0, 0);

                controlToRemove2 = tlp_taxtype_taxamt_txt.GetControlFromPosition(0, 1);
                tlp_taxtype_taxamt_txt.Controls.Remove(controlToRemove2);
                tlp_taxtype_taxamt_txt.Controls.Add(cmbIgstPrcnt, 0, 0);

                tlp_taxtype_taxamt_lbl.Visible = true;
                tlp_taxtype_taxamt_txt.Visible = true;
                tlp_taxtype_taxamt_lbl.Dock = DockStyle.Fill;
                tlp_taxtype_taxamt_txt.Dock = DockStyle.Fill;

                lbligst.Dock = DockStyle.Fill;
                cmbIgstPrcnt.Dock = DockStyle.Fill;


                tlp_comboproduct_view.Controls.Add(tlp_taxtype_taxamt_lbl, 2, 2);
                tlp_comboproduct_view.Controls.Add(tlp_taxtype_taxamt_txt, 2, 3);
                cmbIgstPrcnt.Focus();
            }
            else if (cmbtaxtype.Text == "VAT")
            {
                lbligst.Visible = false;
                cmbIgstPrcnt.Visible = false;

                controlToRemove1 = tlp_taxtype_taxamt_lbl.GetControlFromPosition(0, 1);
                tlp_taxtype_taxamt_lbl.Controls.Remove(controlToRemove1);
                tlp_taxtype_taxamt_lbl.Controls.Add(lblvat, 0, 0);

                controlToRemove2 = tlp_taxtype_taxamt_txt.GetControlFromPosition(0, 1);
                tlp_taxtype_taxamt_txt.Controls.Remove(controlToRemove2);
                tlp_taxtype_taxamt_txt.Controls.Add(cmbVatPrcnt, 0, 0);

                tlp_taxtype_taxamt_lbl.Visible = true;
                tlp_taxtype_taxamt_txt.Visible = true;
                tlp_taxtype_taxamt_lbl.Dock = DockStyle.Fill;
                tlp_taxtype_taxamt_txt.Dock = DockStyle.Fill;

                tlp_taxtype_taxamt_lbl.Visible = true;
                tlp_taxtype_taxamt_txt.Visible = true;
                tlp_taxtype_taxamt_lbl.Dock = DockStyle.Fill;
                tlp_taxtype_taxamt_txt.Dock = DockStyle.Fill;

                lblvat.Visible = true;
                cmbVatPrcnt.Visible = true;
                lblvat.Dock = DockStyle.Fill;
                cmbVatPrcnt.Dock = DockStyle.Fill;
                tlp_comboproduct_view.Controls.Add(tlp_taxtype_taxamt_lbl, 2, 2);
                tlp_comboproduct_view.Controls.Add(tlp_taxtype_taxamt_txt, 2, 3);
                cmbVatPrcnt.Focus();
            }
            textb_shape();

            discountAmnt_combo();
            discountPer_combo();
            ClacTaxAmt_combo();
            //netAmount_combo();
        }

        private void FrmOfferDiscount_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }

        private void btn_refresh_productlist_Click(object sender, EventArgs e)
        {
            pnl_serachproduct_dashboard.Controls.Clear();
            txtitemcode_serach.Text = "Item Code .....";
            txtname_search.Text = "Food Name .....";
            txtitemcode_serach.ForeColor = Color.DarkGray;
            txtname_search.ForeColor = Color.DarkGray;

        }

        private void txt_prodlist_serach_Enter(object sender, EventArgs e)
        {
            if (txt_prodlist_serach.Text == "Barcode / Item Code")
            {
                txt_prodlist_serach.Text = "";
                txt_prodlist_serach.ForeColor = Color.Black;
            }
        }

        private void txt_prodlist_serach_Leave(object sender, EventArgs e)
        {
            if (txt_prodlist_serach.Text == "")
            {
                txt_prodlist_serach.Text = "Barcode / Item Code";
                txt_prodlist_serach.ForeColor = Color.DarkGray;
            }
        }

        private void txt_prodlist_serach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }
        public void combofill()
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "fcid,CategoryName";
                objSearch.Table = " tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where Status=1 and CategoryType=0";
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                if (gridSource.Count > 0)
                {
                    cmb_productlist.DataSource = gridSource;
                    cmb_productlist.ValueMember = "Id";
                    cmb_productlist.DisplayMember = "Value";
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void cmb_productlist_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ProductPopup(Convert.ToInt32(cmb_productlist.SelectedValue));
        }

        private void txt_prodlist_serach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpcScanner(txt_prodlist_serach.Text);
            }
        }


        public void NameSerachProoffer()
        {
            try
            {
                tlp_product_list.Visible = true;
                int panelWidthPercentage = 80;
                int panelheightPercentage = 70;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                tlp_product_list.Size = new Size(panelWidth, panelHeight);
                tlp_product_list.Location = new Point(this.ClientSize.Width / 2 - tlp_product_list.Size.Width / 2, this.ClientSize.Height / 2 - tlp_product_list.Size.Height / 2);

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.Select = "p.ProductId as Id,p.Product as Field1,p.Price as Field2,p.DiscountPer as Field3,p.DiscountAmt as Field4 ,0 as Field5,c.CategoryName as Field6 ,c.Fcid as Field7,p.ItemCode as Field8";
                _objSearch.Table = "tblProduct p" + " LEFT JOIN tblMenu m ON p.ProductId=m.ProductId  LEFT JOIN tblCategory_FTI  c ON c.Fcid=p.Fcid where p.Status=1 and m.Status=1 and p.Product like '%" + txtnamepro_offer.Text +
                   "%' union select  itemid as Id,itemname as Field1,0 Field2,0 as Field3,0 as Field4,Saletype as Field5,c.CategoryName as Field6 ,c.Fcid as Field7,i.ItemCode_UPC as Field8 from " +
                    "tblItem i LEFT JOIN tblCategory_FTI  c ON c.Fcid=i.Fcid";
                _objSearch.WhereNdGroupBy = "where i.Status=1 and i.Saletype=1 and i.Itemname like '%" + txtnamepro_offer.Text + "%'";
                _objSearch.OrderBy = "";
                var Datasource = bllSearch.GetSearchData("prdSearch", _objSearch);
                if (Datasource.Count > 0)
                {
                    dgv_product_list.Rows.Clear();
                    dgv_product_list.Rows.Add(Datasource.Count);
                    for (int i = 0; i < Datasource.Count; i++)
                    {
                        dgv_product_list.Rows[i].Cells[didno].Value = i + 1;
                        dgv_product_list.Rows[i].Cells[dbarcode_codeno].Value = Datasource[i].Field8;
                        dgv_product_list.Rows[i].Cells[dproductnameno].Value = Datasource[i].Field1;
                        dgv_product_list.Rows[i].Cells[dpriceno].Value = Datasource[i].Field2;
                        dgv_product_list.Rows[i].Cells[dcategoryname].Value = Datasource[i].Field6;
                        dgv_product_list.Rows[i].Cells[dcateidno].Value = Datasource[i].Field7;
                        dgv_product_list.Rows[i].Cells[dproductidno].Value = Datasource[i].Id;
                        dgv_product_list.Rows[i].Cells[dsaletypeno].Value = Datasource[i].Field5;
                    }

                }
                else
                    CMessageBox.Show("No Match Found ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error");
            }
        }

        public void UpcScanner(string code)
        {
            try
            {
                tlp_product_list.Visible = true;

                int panelWidthPercentage = 80;
                int panelheightPercentage = 70;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                tlp_product_list.Size = new Size(panelWidth, panelHeight);

                tlp_product_list.Location = new Point(this.ClientSize.Width / 2 - tlp_product_list.Size.Width / 2, this.ClientSize.Height / 2 - tlp_product_list.Size.Height / 2);
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.Select = "p.ProductId as Id,p.Product as Field1,p.Price as Field2,p.DiscountPer as Field3,p.DiscountAmt as Field4 ,0 as Field5,c.CategoryName as Field6 ,c.Fcid as Field7,p.ItemCode as Field8";
                _objSearch.Table = "tblProduct p" + " LEFT JOIN tblMenu m ON p.ProductId=m.ProductId  LEFT JOIN tblCategory_FTI  c ON c.Fcid=p.Fcid where p.Status=1 and m.Status=1 and p.ItemCode= '" + code +
                   "' union select  itemid as Id,itemname as Field1,0 Field2,0 as Field3,0 as Field4,Saletype as Field5,c.CategoryName as Field6 ,c.Fcid as Field7,i.ItemCode_UPC as Field8 from " +
                    "tblItem i LEFT JOIN tblCategory_FTI  c ON c.Fcid=i.Fcid";
                _objSearch.WhereNdGroupBy = "where i.Status=1 and i.Saletype=1 and i.ItemCode_UPC='" + code + "'";
                _objSearch.OrderBy = "";
                var Datasource = bllSearch.GetSearchData("prdSearch", _objSearch);
                if (Datasource.Count > 0)
                {
                    dgv_product_list.Rows.Clear();
                    dgv_product_list.Rows.Add(Datasource.Count);
                    for (int i = 0; i < Datasource.Count; i++)
                    {
                        dgv_product_list.Rows[i].Cells[didno].Value = i + 1;
                        dgv_product_list.Rows[i].Cells[dbarcode_codeno].Value = Datasource[i].Field8;
                        dgv_product_list.Rows[i].Cells[dproductnameno].Value = Datasource[i].Field1;
                        dgv_product_list.Rows[i].Cells[dpriceno].Value = Datasource[i].Field2;
                        dgv_product_list.Rows[i].Cells[dcategoryname].Value = Datasource[i].Field6;
                        dgv_product_list.Rows[i].Cells[dcateidno].Value = Datasource[i].Field7;
                        dgv_product_list.Rows[i].Cells[dproductidno].Value = Datasource[i].Id;
                        dgv_product_list.Rows[i].Cells[dsaletypeno].Value = Datasource[i].Field5;
                    }

                }
                else
                    CMessageBox.Show("No Match Found ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error");
            }
        }

        private void dgv_sigle_product_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                ProductPopup(0);
            }
            else if (e.KeyData == Keys.Delete)
            {
                dgv_sigle_product.Rows.RemoveAt(dgv_sigle_product.CurrentRow.Index);
                int n = dgv_sigle_product.RowCount;
                int count = 0;
                if (n > 0)
                {
                    for (int i = 0; i < n; i++)
                        dgv_sigle_product.Rows[i].Cells[sslno].Value = i + 1;
                    dgv_sigle_product.Rows.Add(1);

                    n = dgv_sigle_product.RowCount;
                    for (int i = 0; i < n; i++)
                    {
                        if (dgv_sigle_product.Rows[i].Cells[sproductidno].Value == null)
                        {
                            count = count + 1;
                        }
                    }
                    if (count > 1)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            if (dgv_sigle_product.Rows[i].Cells[sproductidno].Value == null)
                            {
                                dgv_sigle_product.Rows.RemoveAt(i);
                                break;
                            }
                        }
                    }

                }

                if (n == 0)
                {
                    dgv_sigle_product.Rows.Add(1);
                    DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_sigle_product.Rows[0].Cells[sactionno];
                    cellcolumntype.Value = Properties.Resources.grid_unslect_color;
                    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }
                rearrange_slno();
            }
        }
        public void rearrange_slno()
        {
            for (int i = 0; i < dgv_sigle_product.RowCount; i++)
            {
                dgv_sigle_product.Rows[i].Cells[sslno].Value = i + 1;
            }
        }
        public void UpcScanner_single(string barcode)
        {
            try
            {
                #region Check MultiplePrice
                BllSearch<DmnGridFields> bllSearch1 = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Top(1) pr.SRate Field3,i.Itemname Field1,tx.IgstPrcnt Field4,tx.VatPrcnt Field5,i.TaxType Field6,i.Tax Field7,i.ItemId Field8,pr.Id Field9,i.TaxSettingsId Field10,i.ItemCode_UPC Field11";
                objSearch.Table = "tblitem i left join tblProductRate pr on pr.ProductId = i.ItemId LEFT JOIN tblTaxSettings tx on tx.TaxSettingsId=i.TaxSettingsId";
                objSearch.WhereNdGroupBy = "where i.status = 1 and  pr.QOH > 0 and i.ItemCode_UPC= '" + barcode + "'";
                objSearch.OrderBy = "";
                var gridSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {

                    //txtpid.Text = Convert.ToString(gridSource[0].Field4);
                    //txtproduct.Text = gridSource[0].Field1;
                    //txtrprice.Text = Convert.ToString(gridSource[0].Field2);
                    //txtprid_s.Text = Convert.ToString(gridSource[0].Field3);
                    //txtproductname.Text = Convert.ToString(gridSource[0].Field1);
                    //txtrdisper.Text = "0";
                    //txtrdisamt.Text = "0";
                    //pnlprice.Visible = false;
                    //txtoffername.Focus();
                    for (int i = 0; i < dgv_sigle_product.RowCount; i++)
                    {
                        if (Convert.ToInt32(dgv_sigle_product.Rows[i].Cells[sproductidno].Value) == Convert.ToInt32(gridSource[0].Field8) &&
                           Convert.ToInt32(dgv_sigle_product.Rows[i].Cells[stabletypeno].Value) == Convert.ToInt32(Tabletype.DirectSale))
                        {
                            CMessageBox.Show("Item Already on Offer list", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    tlp_product_list.Visible = false;
                    int r = dgv_sigle_product.Rows.Count - 1;
                    dgv_sigle_product.Rows[r].Cells[sslno].Value = r + 1;
                    dgv_sigle_product.Rows[r].Cells[sbarcodeno].Value = gridSource[0].Field11;
                    dgv_sigle_product.Rows[r].Cells[sproductno].Value = gridSource[0].Field1;
                    dgv_sigle_product.Rows[r].Cells[srundisper].Value = gridSource[0].Field2;
                    dgv_sigle_product.Rows[r].Cells[spriceno].Value = gridSource[0].Field3;
                    if (Convert.ToString(gridSource[0].Field6) == "GST")
                        dgv_sigle_product.Rows[r].Cells[staxperno].Value = gridSource[0].Field4;
                    else
                        dgv_sigle_product.Rows[r].Cells[staxperno].Value = gridSource[0].Field5;
                    dgv_sigle_product.Rows[r].Cells[staxtypeno].Value = gridSource[0].Field6;
                    dgv_sigle_product.Rows[r].Cells[staxno].Value = gridSource[0].Field7;
                    dgv_sigle_product.Rows[r].Cells[sproductidno].Value = gridSource[0].Field8;
                    dgv_sigle_product.Rows[r].Cells[spridno].Value = gridSource[0].Field9;
                    dgv_sigle_product.Rows[r].Cells[staxsettingidno].Value = gridSource[0].Field10;
                    dgv_sigle_product.Rows[r].Cells[sstatusno].Value = 1;
                    dgv_sigle_product.Rows[r].Cells[stabletypeno].Value = Convert.ToInt32(Tabletype.DirectSale);
                    cellCaulcation(r);

                    DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_sigle_product.Rows[r].Cells[sactionno];
                    cellcolumntype.Value = Properties.Resources.delete;
                    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    dgv_sigle_product.Rows.Add();

                    r = dgv_sigle_product.Rows.Count - 1;
                    cellcolumntype = (DataGridViewImageCell)dgv_sigle_product.Rows[r].Cells[sactionno];
                    if (r % 2 == 0)
                        cellcolumntype.Value = Properties.Resources.grid_unslect_color;
                    else
                        cellcolumntype.Value = Properties.Resources.grid_select;
                    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

                }
                else
                {
                    CMessageBox.Show("No Match Found ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtbarcode.Text = "";
                    txtbarcode.Focus();
                }
                #endregion
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error");
            }
        }
        private void dgv_sigle_product_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dgv_sigle_product_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgv_sigle_product_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == sbarcodeno)
                {
                    txtbarcode.Text = "";
                    txtbarcode.Visible = true;
                    txtbarcode.Focus();
                }
                else if (e.ColumnIndex == sactionno)
                {
                    txtbarcode.Text = "";
                    txtbarcode.Visible = false;

                    if (Convert.ToInt32(dgv_sigle_product.Rows[e.RowIndex].Cells[sofferdetailsidno].Value) > 0)
                    {
                        var result = CMessageBox.Show("Do You want Delete " + dgv_sigle_product.Rows[e.RowIndex].Cells[sproductno].Value, "Confirm The Action", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            DmnOfferDetails dmnOfferdetails = new DmnOfferDetails();
                            BllOffer blldetails = new BllOffer();
                            dmnOfferdetails.Odid = Convert.ToInt32(dgv_sigle_product.Rows[e.RowIndex].Cells[sofferdetailsidno].Value);
                            dmnOfferdetails.BranchId = FrmDashboard.BranchId;
                            dmnOfferdetails.CreatedBy = FrmDashboard.UserId;
                            var InsertStatusD = blldetails.Removeoffer("prd_remove_offerdetails", dmnOfferdetails);

                            dgv_sigle_product.Rows.RemoveAt(e.RowIndex);
                            int n = dgv_sigle_product.RowCount;
                            if (n == 0)
                            {
                                dgv_sigle_product.Rows.Add(1);
                                DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_sigle_product.Rows[0].Cells[sactionno];
                                cellcolumntype.Value = Properties.Resources.grid_unslect_color;
                                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            }
                            rearrange_slno();
                        }
                    }
                    else
                    {
                        dgv_sigle_product.Rows.RemoveAt(e.RowIndex);
                        int n = dgv_sigle_product.RowCount;
                        int count = 0;
                        if (n > 0)
                        {
                            for (int i = 0; i < n; i++)
                                dgv_sigle_product.Rows[i].Cells[sslno].Value = i + 1;
                            dgv_sigle_product.Rows.Add(1);

                            n = dgv_sigle_product.RowCount;
                            for (int i = 0; i < n; i++)
                            {
                                if (dgv_sigle_product.Rows[i].Cells[sproductidno].Value == null)
                                {
                                    count = count + 1;
                                }
                            }
                            if (count > 1)
                            {
                                for (int i = 0; i < n; i++)
                                {
                                    if (dgv_sigle_product.Rows[i].Cells[sproductidno].Value == null)
                                    {
                                        dgv_sigle_product.Rows.RemoveAt(i);
                                        break;
                                    }
                                }
                            }

                        }

                        if (n == 0)
                        {
                            dgv_sigle_product.Rows.Add(1);
                            DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_sigle_product.Rows[0].Cells[sactionno];
                            cellcolumntype.Value = Properties.Resources.grid_unslect_color;
                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        }
                        rearrange_slno();
                    }
                }
                else
                {
                    txtbarcode.Text = "";
                    txtbarcode.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void cellTaxAmount_combo(int rowno, decimal amount)
        {
            try
            {
                string cellpopupid = Convert.ToString(dgv_sele_cmb_products.Rows[rowno].Cells[cproductidno].Value);
                if (cellpopupid != "")
                {
                    BllSearch<DmnTaxSettings> _bllSearch1 = new BllSearch<DmnTaxSettings>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "TaxType, IgstPrcnt,VatPrcnt";
                    dmnSearch.Table = "tblTaxSettings";
                    dmnSearch.WhereNdGroupBy = " where status=1 and TaxSettingsId= " + dgv_sele_cmb_products.Rows[rowno].Cells[Ctaxsettingidno].Value;
                    dmnSearch.OrderBy = "";
                    var gridSource = _bllSearch1.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource[0].TaxType == "GST")
                    {
                        decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].IgstPrcnt) * amount / 100);
                        dgv_sele_cmb_products.Rows[rowno].Cells[ctaxamtno].Value = TaxAmount.ToString("N2");
                        dgv_sele_cmb_products.Rows[rowno].Cells[ctaxperno].Value = gridSource[0].IgstPrcnt;
                    }
                    if (gridSource[0].TaxType == "VAT")
                    {
                        decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].VatPrcnt) * amount / 100);
                        dgv_sele_cmb_products.Rows[rowno].Cells[ctaxamtno].Value = TaxAmount.ToString("N2");
                        dgv_sele_cmb_products.Rows[rowno].Cells[ctaxperno].Value = gridSource[0].VatPrcnt;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void cellTaxAmount(int cellproductid, decimal amount)
        {
            try
            {
                string cellpopupid = Convert.ToString(dgv_sigle_product.Rows[cellproductid].Cells[sproductidno].Value);
                if (cellpopupid != "")
                {
                    BllSearch<DmnTaxSettings> _bllSearch1 = new BllSearch<DmnTaxSettings>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "TaxType, IgstPrcnt,VatPrcnt";
                    dmnSearch.Table = "tblTaxSettings";
                    dmnSearch.WhereNdGroupBy = " where status=1 and TaxSettingsId= " + dgv_sigle_product.Rows[cellproductid].Cells[staxsettingidno].Value;
                    dmnSearch.OrderBy = "";
                    var gridSource = _bllSearch1.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource[0].TaxType == "GST")
                    {
                        decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].IgstPrcnt) * amount / 100);
                        dgv_sigle_product.Rows[cellproductid].Cells[staxamtno].Value = TaxAmount.ToString("N2");
                        dgv_sigle_product.Rows[cellproductid].Cells[staxperno].Value = gridSource[0].IgstPrcnt;
                    }
                    if (gridSource[0].TaxType == "VAT")
                    {
                        decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].VatPrcnt) * amount / 100);
                        dgv_sigle_product.Rows[cellproductid].Cells[staxamtno].Value = TaxAmount.ToString("N2");
                        dgv_sigle_product.Rows[cellproductid].Cells[staxperno].Value = gridSource[0].VatPrcnt;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void cellDiscountAmount_combo(int cellproductid)
        {
            try
            {
                if (dgv_sele_cmb_products.Rows[cellproductid].Cells[cdisperno].Value != "" && dgv_sele_cmb_products.Rows[cellproductid].Cells[cpriceno].Value != "")
                {
                    decimal disAmnt = Convert.ToDecimal(dgv_sele_cmb_products.Rows[cellproductid].Cells[cdisperno].Value) * Convert.ToDecimal(dgv_sele_cmb_products.Rows[cellproductid].Cells[ctotalamtno].Value) / 100;
                    dgv_sele_cmb_products.Rows[cellproductid].Cells[cdisamtno].Value = disAmnt.ToString("N2");
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void cellDiscountAmount(int cellproductid)
        {
            try
            {
                if (dgv_sigle_product.Rows[cellproductid].Cells[soffferdisperno].Value != "" && dgv_sigle_product.Rows[cellproductid].Cells[spriceno].Value != "")
                {
                    decimal disAmnt = (Convert.ToDecimal(txtofferdsiper_s.Text) * Convert.ToDecimal(dgv_sigle_product.Rows[cellproductid].Cells[spriceno].Value)) / 100;
                    dgv_sigle_product.Rows[cellproductid].Cells[soffferdisamtno].Value = disAmnt.ToString("N2");
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void ProductOfferFill()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "fd.Productname Field1,fd.DiscountPer Field2,fd.Price Field3,tx.IgstPrcnt Field4,tx.VatPrcnt Field5,fd.TaxType Field6,fd.Tax Field7,fd.ProductId Field8,fd.Saletype Field9,fd.Prid Field10,fd.FinalPrice Field11,fd.DiscountAmt Field12,fd.Description Field13,fd.Odid Field14,fd.ItemCode_UPC Field15," +
                    "(select DiscountPer from tblProduct where status = 1 and ProductId = fd.ProductId) Field16," +
                    "(select DiscountPer from tblItem where status = 1 and ItemId = fd.ProductId) Field17";
                objSearch.Table = "tblOfferDetails  fd LEFT JOIN tblOfferMaster fm ON fm.Omid=fd.Omid LEFT JOIN tblTaxSettings tx on tx.TaxSettingsId=fd.TaxSettingsId";
                objSearch.WhereNdGroupBy = "where fd.status = 1 ";
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    dgv_sigle_product.Rows.Clear();
                    dgv_sigle_product.Rows.Add(gridSource.Count);
                    for (int r = 0; r < gridSource.Count; r++)
                    {
                        dgv_sigle_product.Rows[r].Cells[sslno].Value = r + 1;
                        dgv_sigle_product.Rows[r].Cells[sbarcodeno].Value = gridSource[r].Field15;
                        dgv_sigle_product.Rows[r].Cells[sproductno].Value = gridSource[r].Field1;

                        if (Convert.ToInt32(gridSource[r].Field9) == Convert.ToInt32(Tabletype.DirectSale))
                            dgv_sigle_product.Rows[r].Cells[srundisper].Value = gridSource[r].Field17;
                        else
                            dgv_sigle_product.Rows[r].Cells[srundisper].Value = gridSource[r].Field16;

                        dgv_sigle_product.Rows[r].Cells[spriceno].Value = gridSource[r].Field3;
                        if (Convert.ToString(gridSource[0].Field6) == "GST")
                            dgv_sigle_product.Rows[r].Cells[staxperno].Value = gridSource[r].Field4;
                        else
                            dgv_sigle_product.Rows[r].Cells[staxperno].Value = gridSource[r].Field5;
                        dgv_sigle_product.Rows[r].Cells[staxtypeno].Value = gridSource[r].Field6;
                        dgv_sigle_product.Rows[r].Cells[staxno].Value = gridSource[r].Field7;
                        dgv_sigle_product.Rows[r].Cells[sproductidno].Value = gridSource[r].Field8;
                        dgv_sigle_product.Rows[r].Cells[sstatusno].Value = 1;
                        dgv_sigle_product.Rows[r].Cells[stabletypeno].Value = gridSource[r].Field9;

                        dgv_sigle_product.Rows[r].Cells[spridno].Value = gridSource[r].Field10;
                        dgv_sigle_product.Rows[r].Cells[sfinalpriceno].Value = gridSource[r].Field11;

                        dgv_sigle_product.Rows[r].Cells[soffferdisperno].Value = gridSource[r].Field2;
                        dgv_sigle_product.Rows[r].Cells[soffferdisamtno].Value = gridSource[r].Field12;
                        dgv_sigle_product.Rows[r].Cells[sdescriptionno].Value = gridSource[r].Field13;
                        dgv_sigle_product.Rows[r].Cells[sofferdetailsidno].Value = gridSource[r].Field14;

                        DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_sigle_product.Rows[r].Cells[sactionno];
                        cellcolumntype.Value = Properties.Resources.delete;
                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    }
                    dgv_sigle_product.Rows.Add();
                    int i = dgv_sigle_product.RowCount - 1;
                    DataGridViewImageCell cellcolumntype1 = (DataGridViewImageCell)dgv_sigle_product.Rows[i].Cells[sactionno];
                    if (i % 2 == 0)
                        cellcolumntype1.Value = Properties.Resources.grid_unslect_color;
                    else
                        cellcolumntype1.Value = Properties.Resources.grid_select;
                    cellcolumntype1.ImageLayout = DataGridViewImageCellLayout.Zoom;

                }
                else
                {
                    dgv_sigle_product.Rows.Clear();
                    dgv_sigle_product.Rows.Add();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void dgv_sigle_product_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == sproductno)
            {
                if (Convert.ToInt32(dgv_sigle_product.Rows[e.RowIndex].Cells[sproductidno].Value) == 0 || dgv_sigle_product.Rows[e.RowIndex].Cells[sproductidno].Value == null)
                {
                    dgv_sigle_product.Rows[e.RowIndex].Cells[sproductno].Value = "F8 For Prodcut List";
                    dgv_sigle_product.Rows[e.RowIndex].Cells[sproductno].Style.ForeColor = Color.Red;
                }
            }
        }

        private void dgv_sigle_product_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == sproductno)
            {
                if (Convert.ToInt32(dgv_sigle_product.Rows[e.RowIndex].Cells[sproductidno].Value) > 0)
                {
                    dgv_sigle_product.Rows[e.RowIndex].Cells[sproductno].Style.ForeColor = Color.Black;
                }
                else
                {
                    dgv_sigle_product.Rows[e.RowIndex].Cells[sproductno].Value = "";
                }
            }
        }

        private void txtofferdsiper_s_KeyPress(object sender, KeyPressEventArgs e)
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
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtofferdis_amt_s_KeyPress(object sender, KeyPressEventArgs e)
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
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtofferdsiper_s_Leave(object sender, EventArgs e)
        {
            try
            {
                Discount_Check();
                for (int i = 0; i < dgv_sigle_product.RowCount; i++)
                {
                    if (Convert.ToInt32(dgv_sigle_product.Rows[i].Cells[sproductidno].Value) > 0 && Convert.ToDecimal(dgv_sigle_product.Rows[i].Cells[spriceno].Value) > 0)
                        cellCaulcation(i);
                }
            }
            catch (Exception ex)
            {

            }


        }

        private void btn_combo_gridview_Click(object sender, EventArgs e)
        {

            int tlpWidthPercentage = 95;
            int tlpheightPercentage = 70;
            int tlpWidth = (int)(this.Width * tlpWidthPercentage / 100);
            int tlpHeight = (int)(this.Height * tlpheightPercentage / 100);

            if (tlp_gridview.Visible)
                tlp_gridview.Visible = false;
            else
                tlp_gridview.Visible = true;

            tlp_gridview.Size = new Size(tlpWidth, tlpHeight);
            tlp_gridview.Location = new Point(this.ClientSize.Width / 2 - tlp_gridview.Size.Width / 2, this.ClientSize.Height / 2 - tlp_gridview.Size.Height / 2);
        }

        private void btn_gridview_close_Click(object sender, EventArgs e)
        {
            tlp_gridview.Visible = false;
        }



        private void cmbIgstPrcnt_SelectionChangeCommitted(object sender, EventArgs e)
        {
            discountAmnt_combo();
            discountPer_combo();
            ClacTaxAmt_combo();
            // netAmount_combo();
        }

        private void cmbVatPrcnt_SelectionChangeCommitted(object sender, EventArgs e)
        {
            discountAmnt_combo();
            discountPer_combo();
            ClacTaxAmt_combo();
            // netAmount_combo();
        }

        private void txtnamepro_offer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }

        private void txtnamepro_offer_Leave(object sender, EventArgs e)
        {
            if (txtnamepro_offer.Text == "")
            {
                txtnamepro_offer.Text = "Name Serach";
                txtnamepro_offer.ForeColor = Color.DarkGray;
            }
        }

        private void txtnamepro_offer_Enter(object sender, EventArgs e)
        {
            if (txtnamepro_offer.Text == "Name Serach")
            {
                txtnamepro_offer.Text = "";
                txtnamepro_offer.ForeColor = Color.Black;
            }
        }

        private void txtnamepro_offer_TextChanged(object sender, EventArgs e)
        {
            if (txtnamepro_offer.Text != "" && txtnamepro_offer.Text != null && txtnamepro_offer.Text != "Name Serach")
            {
                NameSerachProoffer();
            }
        }

        private void txtname_search_TextChanged(object sender, EventArgs e)
        {
            if (txtname_search.Text != "" && txtname_search.Text != null && txtname_search.Text != "Food Name .....")
                NameSerach_Combo();
        }

        private void cmbtax_SelectionChangeCommitted(object sender, EventArgs e)
        {
            discountAmnt_combo();
            discountPer_combo();
            ClacTaxAmt_combo();
        }

        private void btn_va_ok_Click(object sender, EventArgs e)
        {
            try
            {
                decimal Total_Addon_Amt = 0;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                if (variflag == false && previousClickedButton == null)// addonflag 
                {
                    CMessageBox.Show("Please Select Size", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                pnl_variation_addon.SuspendLayout();
                pnl_variation_addon.Visible = false;
                int vid = 0, pid = 0;
                string combinedId = "";
                bool Addonflag = false;
                Checked_VariationAddon_List = Checked_VariationAddon_List.OrderBy(item => item.Pavid).ToList();
                var existingItem = Checked_VariationAddon_List.FirstOrDefault(item => item.AVType == Convert.ToInt32(Product_Addon_Variation_Type.Variation));
                if (existingItem != null)
                {
                    int indexToUpdate = Checked_VariationAddon_List.IndexOf(existingItem);
                    vid = Checked_VariationAddon_List[indexToUpdate].Pavid;
                    pid = Checked_VariationAddon_List[indexToUpdate].Pid;
                }

                int AddonCount = 0;
                for (int i = 0; i < Checked_VariationAddon_List.Count; i++)
                {
                    var item = Checked_VariationAddon_List[i];
                    if (item.AVType == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                    {
                        combinedId = combinedId + $"{item.CCode}" + $"{item.Qty}";
                        AddonCount = AddonCount + 1;
                    }
                }
                string Av_ID = pid + vid + "_" + combinedId;
                string buttonname = "btn_sele_" + Av_ID + "_" + Convert.ToInt32(Tabletype.Product);
                Button buttonitem = this.Controls.Find(buttonname, true).FirstOrDefault() as Button;
                if (buttonitem != null)
                {
                    string lblname = "lbl_selected_" + Av_ID + "_" + Convert.ToInt32(Tabletype.Product);
                    Label lblqty = this.Controls.Find(lblname, true).FirstOrDefault() as Label;
                    int cur_qty = Convert.ToInt32(lblqty.Text) + 1;
                    lblqty.Text = Convert.ToString(cur_qty);
                    CalcTotalAmtCombo(Convert.ToInt32(buttonitem.Tag), 1);
                    pnl_serachproducts.Visible = false;
                }
                else
                {
                    objSearch = new DmnSearch();
                    objSearch.Select = "p.Product Field1,p.DiscountPer Field2,p.Price Field3,tx.IgstPrcnt Field4,tx.VatPrcnt Field5,p.TaxType Field6,p.Tax Field7,p.ProductId Field8,p.TaxSettingsId Field9,p.ItemCode Field10,p.mrp Field11";
                    objSearch.Table = "tblProduct  p LEFT JOIN tblTaxSettings tx on tx.TaxSettingsId=p.TaxSettingsId";
                    objSearch.WhereNdGroupBy = "where ProductId=" + pid;
                    objSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource.Count > 0)
                    {
                        #region Grid Fill Main product
                        dgv_sele_cmb_products.Rows.Add();
                        int r = dgv_sele_cmb_products.Rows.Count - 1;
                        dgv_sele_cmb_products.Rows[r].Cells[sslno].Value = r + 1;
                        dgv_sele_cmb_products.Rows[r].Cells[cbarcodeno].Value = gridSource[0].Field10;
                        dgv_sele_cmb_products.Rows[r].Cells[Cproductno].Value = gridSource[0].Field1;
                        dgv_sele_cmb_products.Rows[r].Cells[cdisperno].Value = gridSource[0].Field2;

                        var Variationprice = Checked_VariationAddon_List.FirstOrDefault(item => item.AVType == Convert.ToInt32(Product_Addon_Variation_Type.Variation));
                        if (Variationprice != null)
                        {
                            int indexToRemove = Checked_VariationAddon_List.IndexOf(Variationprice);
                            dgv_sele_cmb_products.Rows[r].Cells[cpriceno].Value = Convert.ToDecimal(Checked_VariationAddon_List[indexToRemove].Price) + Total_Addon_Amt;
                        }

                        dgv_sele_cmb_products.Rows[r].Cells[cqtynoo].Value = 1;
                        if (Convert.ToString(gridSource[0].Field6) == "GST")
                            dgv_sele_cmb_products.Rows[r].Cells[ctaxperno].Value = gridSource[0].Field4;
                        else
                            dgv_sele_cmb_products.Rows[r].Cells[ctaxperno].Value = gridSource[0].Field5;

                        dgv_sele_cmb_products.Rows[r].Cells[ctaxtypeno].Value = gridSource[0].Field6;
                        dgv_sele_cmb_products.Rows[r].Cells[cproducttypeno].Value = Convert.ToInt32(Tabletype.Product);
                        dgv_sele_cmb_products.Rows[r].Cells[ctaxno].Value = gridSource[0].Field7;
                        dgv_sele_cmb_products.Rows[r].Cells[cproductidno].Value = gridSource[0].Field8;
                        dgv_sele_cmb_products.Rows[r].Cells[Ctaxsettingidno].Value = gridSource[0].Field9;
                        dgv_sele_cmb_products.Rows[r].Cells[cstatusno].Value = 1;
                        dgv_sele_cmb_products.Rows[r].Cells[stabletypeno].Value = Convert.ToInt32(Tabletype.Product);
                        dgv_sele_cmb_products.Rows[r].Cells[cpridno].Value = 0;
                        dgv_sele_cmb_products.Rows[r].Cells[cmrpno].Value = gridSource[0].Field11;
                        totalcellamount_Combo(r);
                        r = r + 1;
                        #endregion

                        #region Variation or Addon Grid Inseration

                        int k = 0;
                        for (int i = 0; i < Checked_VariationAddon_List.Count; i++)
                        {
                            var item = Checked_VariationAddon_List[i];
                            if (item.AVType == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                            {
                                Addonflag = true;
                                dgv_sele_cmb_products.Rows.Add(); 
                                dgv_sele_cmb_products.Rows[r].Cells[sslno].Value =  k+ 1;
                                dgv_sele_cmb_products.Rows[r].Cells[cbarcodeno].Value = gridSource[0].Field10;
                                dgv_sele_cmb_products.Rows[r].Cells[Cproductno].Value = item.Caption;
                                dgv_sele_cmb_products.Rows[r].Cells[cdisperno].Value = 0;

                                dgv_sele_cmb_products.Rows[r].Cells[cpriceno].Value = item.Price;
                                dgv_sele_cmb_products.Rows[r].Cells[cqtynoo].Value = item.Qty;   
                                dgv_sele_cmb_products.Rows[r].Cells[cproductidno].Value = item.Pavid;
                                dgv_sele_cmb_products.Rows[r].Cells[cstatusno].Value = 1;

                                dgv_sele_cmb_products.Rows[r].DefaultCellStyle.BackColor = Color.OliveDrab;
                                dgv_sele_cmb_products.Rows[r].Cells[Cavidno].Value = Av_ID;
                                dgv_sele_cmb_products.Rows[r].Cells[Caddonstatusno].Value  = Convert.ToInt32(Product_Addon_Variation_Type.Addon);
                                Total_Addon_Amt = Total_Addon_Amt + Convert.ToDecimal(item.Price);
                                k = k + 1;
                                r = r + 1;
                            }
                        }       
                        #endregion

                        //CalcTotalAmtCombo(Convert.ToInt32(clickedControl.Tag), 1);

                        TableLayoutPanel tlp_selected_Item_Box = new TableLayoutPanel();
                        tlp_selected_Item_Box.Name = "tlp_sele_" + Av_ID + "_" + Convert.ToInt32(Tabletype.Product);
                        tlp_selected_Item_Box.AccessibleDescription = "3";
                        tlp_selected_Item_Box.Tag = Av_ID;
                        tlp_selected_Item_Box.Width = 245;
                        tlp_selected_Item_Box.Height = 60;
                        tlp_selected_Item_Box.ColumnCount = 4;
                        tlp_selected_Item_Box.BorderStyle = BorderStyle.FixedSingle;
                        tlp_selected_Item_Box.BackColor = seleproduct_buttoncolor;

                        //Item Button   
                        Button Button_Item = new Button();
                        Button_Item.Text = gridSource[0].Field1;
                        Button_Item.Name = "btn_sele_" + Av_ID + "_" + Convert.ToInt32(Tabletype.Product);
                        Button_Item.Tag = Av_ID;
                        Button_Item.Width = 150;
                        Button_Item.Height = 50;
                        Button_Item.FlatAppearance.BorderSize = 0;
                        Button_Item.FlatAppearance.BorderColor = Button_Item.BackColor;
                        Button_Item.FlatStyle = FlatStyle.Flat;
                        Button_Item.BackColor = Color.White;
                        Button_Item.Click += new EventHandler(Button_ItemClickEventHandler);
                        Button_Item.Font = new Font(Button_Item.Font.FontFamily, 10, FontStyle.Bold);
                        // Adding the button to the third column
                        tlp_selected_Item_Box.Controls.Add(Button_Item, 0, 0);

                        // Creating the label for the Qty
                        Label label = new Label();
                        label.Text = "1";  // Set your desired text
                        label.Name = "lbl_selected_" + Av_ID + "_" + Convert.ToInt32(Tabletype.Product);
                        label.Tag = Av_ID;
                        label.AutoSize = true;
                        label.Dock = DockStyle.Fill;
                        label.ForeColor = Color.White;
                        label.TextAlign = ContentAlignment.MiddleCenter;
                        label.Font = new Font(label.Font.FontFamily, 10, FontStyle.Bold);
                        // Adding the label to the second column
                        tlp_selected_Item_Box.Controls.Add(label, 1, 0);

                        //Add Button
                        Button Button_Add = new Button();
                        Button_Add.Text = "+";
                        Button_Add.Name = "btn_sele_add_" + Av_ID + "_" + Convert.ToInt32(Tabletype.Product);
                        Button_Add.Tag = Av_ID;
                        Button_Add.AccessibleDescription = "3";
                        Button_Add.Width = 20;
                        Button_Add.Height = 50;
                        Button_Add.FlatAppearance.BorderSize = 0;
                        Button_Add.FlatAppearance.BorderColor = Button_Add.BackColor;
                        Button_Add.FlatStyle = FlatStyle.Flat;
                        Button_Add.BackColor = Color.White;
                        Button_Add.Click += new EventHandler(Button_AddClickEventHandler);
                        Button_Add.Font = new Font(Button_Add.Font.FontFamily, 10, FontStyle.Bold);
                        Button_Add.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Button_Add.Width, Button_Add.Height, Button_Add.Width, Button_Add.Height));
                        // Adding the button to the third column
                        tlp_selected_Item_Box.Controls.Add(Button_Add, 2, 0);

                        //Remove Button
                        Button Button_Remove = new Button();
                        Button_Remove.Text = "-";
                        Button_Remove.Name = "btn_sele_rem_" + Av_ID + "_" + Convert.ToInt32(Tabletype.Product);
                        Button_Remove.AccessibleDescription = "3";
                        Button_Remove.Tag = Av_ID;
                        Button_Remove.Width = 20;
                        Button_Remove.Height = 50;
                        Button_Remove.Dock = DockStyle.Left;
                        Button_Remove.FlatAppearance.BorderSize = 0;
                        Button_Remove.FlatAppearance.BorderColor = Button_Remove.BackColor;
                        Button_Remove.FlatStyle = FlatStyle.Flat;
                        Button_Remove.BackColor = Color.White;
                        Button_Remove.Click += new EventHandler(Button_RemoveClickEventHandler);
                        Button_Remove.Font = new Font(Button_Remove.Font.FontFamily, 10, FontStyle.Bold);
                        Button_Remove.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Button_Remove.Width, Button_Remove.Height, Button_Remove.Width, Button_Remove.Height));
                        // Adding the button to the third column
                        tlp_selected_Item_Box.Controls.Add(Button_Remove, 3, 0);
                        flp_selected_item_combo.Controls.Add(tlp_selected_Item_Box);
                        tlp_selected_Item_Box.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, tlp_selected_Item_Box.Width, tlp_selected_Item_Box.Height, 10, 10));
                        pnl_serachproducts.Visible = false;
                        discountAmnt_combo();
                        discountPer_combo();
                        ClacTaxAmt_combo();
                        // netAmount_combo();

                        if (flp_selected_item_combo.Controls.Count > 0)
                            btn_combo_gridview.Visible = true;
                    }
                }

            }
            catch(Exception ex)
            {

            }

         }
    

        private void btn_va_cancel_Click(object sender, EventArgs e)
        {
            pnl_variation_addon.Visible = false;
            pnl_serachproducts.Visible = true;
        }

        private void btn_va_close_Click(object sender, EventArgs e)
        {
            pnl_variation_addon.Visible = false;
            pnl_serachproducts.Visible = true;
        }

        private void txtcombo_totalprice_Leave(object sender, EventArgs e)
        {
            discountPer_combo();
            ClacTaxAmt_combo();
        }

        //private void dgv_sigle_product_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    if (dgv_sigle_product.CurrentCell.ColumnIndex == soffferdisamtno || dgv_sigle_product.CurrentCell.ColumnIndex == soffferdisperno)
        //    {
        //        e.Control.KeyPress -= new KeyPressEventHandler(dgvProduct_KeyPress);
        //        System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;
        //        if (tb != null)
        //        {
        //            tb.KeyPress += new KeyPressEventHandler(dgvProduct_KeyPress);
        //        }
        //    }
        //}
        //private void dgvProduct_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
        //    {
        //        e.Handled = true;
        //    }
        //    // only allow one decimal point   
        //    if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
        //    {
        //        e.Handled = true;
        //    }
        //}

        //private void dgv_sigle_product_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        //{
        //    try
        //    {
        //        if (dgv_sigle_product.Rows[e.RowIndex].Cells[sproductidno].Value != "" && dgv_sigle_product.Rows[e.RowIndex].Cells[sproductidno].Value != null)
        //        {

        //            if (e.ColumnIndex == soffferdisperno)
        //            {
        //                DataGridViewTextBoxCell cell = dgv_sigle_product[soffferdisperno, e.RowIndex] as DataGridViewTextBoxCell;
        //                if ((cell.EditedFormattedValue.ToString() != "") || (cell.EditedFormattedValue.ToString() != string.Empty))
        //                {
        //                    if (Convert.ToDecimal(cell.EditedFormattedValue.ToString()) > 100)
        //                    {
        //                        CMessageBox.Show("Discount % Must Be Less Than 100", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                        e.Cancel = true;
        //                    }
        //                }
        //                else
        //                {
        //                    cell.Value = "0";
        //                }
        //            }
        //            else if (e.ColumnIndex == soffferdisamtno)
        //            {
        //                DataGridViewTextBoxCell cell = dgv_sigle_product[soffferdisamtno, e.RowIndex] as DataGridViewTextBoxCell;
        //                if ((cell.EditedFormattedValue.ToString() != "") || (cell.EditedFormattedValue.ToString() != string.Empty))
        //                {
        //                    if (Convert.ToDecimal(cell.EditedFormattedValue.ToString()) > Convert.ToDecimal(dgv_sigle_product.Rows[e.RowIndex].Cells[spriceno].Value))
        //                    {
        //                        CMessageBox.Show("Discount Amount Must Be Less Than Amount", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                        e.Cancel = true;
        //                    }
        //                }
        //                else
        //                {
        //                    cell.Value = "0";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void dgv_sigle_product_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        //{
        //    try
        //    {
        //        DataGridViewTextBoxCell disamtcell = dgv_sigle_product[soffferdisamtno, e.RowIndex] as DataGridViewTextBoxCell;
        //        DataGridViewTextBoxCell dispercell = dgv_sigle_product[soffferdisperno, e.RowIndex] as DataGridViewTextBoxCell;
        //        if (dgv_sigle_product.Rows[e.RowIndex].Cells[sproductidno].Value != "" && dgv_sigle_product.Rows[e.RowIndex].Cells[sproductidno].Value != null)
        //        {

        //            if ((dispercell.EditedFormattedValue.ToString() != "") || (dispercell.EditedFormattedValue.ToString() != string.Empty))
        //            {
        //                if (Convert.ToDecimal(dispercell.EditedFormattedValue.ToString()) > 100)
        //                {
        //                    CMessageBox.Show("Discount % Must Be Less Than 100", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                    //SendKeys.Send("{UP}");
        //                    e.Cancel = true;
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                dispercell.Value = "0";
        //            }

        //            if ((dispercell.EditedFormattedValue.ToString() != "") || (disamtcell.EditedFormattedValue.ToString() != string.Empty))
        //            {
        //                if (Convert.ToDecimal(disamtcell.EditedFormattedValue.ToString()) > Convert.ToDecimal(dgv_sigle_product.Rows[e.RowIndex].Cells[spriceno].Value))
        //                {
        //                    CMessageBox.Show("Discount Amount Must Be Less Than Amount", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                    e.Cancel = true;
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                disamtcell.Value = "0";
        //            }                   
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

    }
}
