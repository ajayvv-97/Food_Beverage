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
using static FoodandBeverage.Interfaces.Interfaces;

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
        public static int pidno = 0;
        public static int qtynoo = 1;
        public static int desno = 2;
        public static int odidno = 3;
        public static int statusno = 4;

        public static int didno = 0;
        public static int dproductnameno = 1;
        public static int dpriceno = 2;
        public static int dcategoryname = 3;
        public static int dcateidno = 4;
        public static int dproductidno = 5;
        public static int dsaletypeno = 6;
        #endregion

        #region Declaration 
        TableLayoutPanel tlp_searchproduct_sidebar = new TableLayoutPanel();
        TableLayoutPanel tlp_searchproduct_dashboard = new TableLayoutPanel();
        Color product_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        Color seleproduct_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);

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
            tlp_combo.Visible = false;
            Control controlToRemove = tlp_main.GetControlFromPosition(0, 2);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_single.Visible = true;
            tlp_main.Controls.Add(tlp_single,0, 2);
            tlp_single.Dock = DockStyle.Fill;
        }

        private void btn_combo_Click(object sender, EventArgs e)
        {
            tlp_single.Visible = false;
            Control controlToRemove = tlp_main.GetControlFromPosition(0, 2);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_combo.Visible = true;
            tlp_main.Controls.Add(tlp_combo, 0, 2);
            tlp_combo.Dock = DockStyle.Fill;
        }

        private void FrmOfferDiscount_Load(object sender, EventArgs e)
        {
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

            dtp_to_date_s.MinDate = DateTime.Now;
            dtp_from_date_s.MinDate = DateTime.Now;

            dtp_to_date.MinDate = DateTime.Now;
            dtp_from_date.MinDate = DateTime.Now;


            txtname_search.Text = "Food Name .....";
            txtitemcode_serach.Text = "Item Code .....";
            tlp_single.Visible = true;
            tlp_main.Controls.Add(tlp_single);
            tlp_single.Dock = DockStyle.Fill;
            textb_shape();
            pnl_serachproducts.BorderStyle = BorderStyle.FixedSingle; // or BorderStyle.Fixed3D
            pnl_serachproducts.BackColor = Color.White; // Set the border color to white
            pnl_serachproducts.Padding = new Padding(5); // Adjust the padding to increase the border thickness

        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtdescription, txtfinalprice, discountamt, discountper, txtoffername, txtrdisamt, txtrdisper, txtrprice, txtproduct, txtname_search, txtitemcode_serach, txtcombodes, txtcombo_finalprice, txtcombo_disamt, txtcombo_disper, txtcombo_totalprice, txtcmbo_offername, txtcombo_run_disamt, txtcombo_run_disper, txtcombo_run_price })
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
            if(e.KeyCode==Keys.F8)
            {
                ProductPopup();
            }
        }
        public void ProductPopup()
        {
            try
            {
                tlp_product_list.Visible = true;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.Select = "p.ProductId as Id,p.Product as Field1,p.Price as Field2,p.DiscountPer as Field3,p.DiscountAmt as Field4 ,0 as Field5,c.CategoryName as Field6 ,c.Fcid as Field7";
                _objSearch.Table = "tblProduct p" + " LEFT JOIN tblMenu m ON p.ProductId=m.ProductId  LEFT JOIN tblCategory_FTI  c ON c.Fcid=p.Fcid where p.Status=1 and m.Status=1 " +
                    "union select  itemid as Id,itemname as Field1,0 Field2,0 as Field3,0 as Field4,Saletype as Field5,c.CategoryName as Field6 ,c.Fcid as Field7 from " +
                    "tblItem i LEFT JOIN tblCategory_FTI  c ON c.Fcid=i.Fcid";
                _objSearch.WhereNdGroupBy = "where i.Status=1 and i.Saletype=1";
                _objSearch.OrderBy = "";
                var Datasource = bllSearch.GetSearchData("prdSearch", _objSearch);
                if(Datasource.Count>0)
                {
                    dgv_product_list.Rows.Clear();
                    dgv_product_list.Rows.Add(Datasource.Count);
                    for(int i=0;i< Datasource.Count;i++)
                    {
                        dgv_product_list.Rows[i].Cells[didno].Value = i + 1;
                        dgv_product_list.Rows[i].Cells[dproductnameno].Value = Datasource[i].Field1;
                        dgv_product_list.Rows[i].Cells[dpriceno].Value = Datasource[i].Field2;
                        dgv_product_list.Rows[i].Cells[dcategoryname].Value = Datasource[i].Field6; 
                        dgv_product_list.Rows[i].Cells[dcateidno].Value = Datasource[i].Field7; 
                        dgv_product_list.Rows[i].Cells[dproductidno].Value = Datasource[i].Id;
                        dgv_product_list.Rows[i].Cells[dsaletypeno].Value = Datasource[i].Field5;
                    }

                }

                #region msg old
                //BllSearch<DmnGridFields> Searchcount = new BllSearch<DmnGridFields>();
                //string returnValue = string.Empty;
                //DataTable dtColumnAlign = new DataTable();
                //dtColumnAlign.Columns.Add();
                //DmnSearch _objSearch = new DmnSearch();
                //_objSearch.ColumnHeaders = "Id,Product Name,Price,Discount %,Discount Amount, ";
                //_objSearch.Select = "p.ProductId as Id,p.Product as Field1,p.Price as Field2,p.DiscountPer as Field3,p.DiscountAmt as Field4 ,0 as Field5";
                //_objSearch.Table = "tblProduct p" + " LEFT JOIN tblMenu m ON p.ProductId=m.ProductId where p.Status=1 and m.Status=1 " +
                //    "union select  itemid as Id,itemname as Field1,0 Field2,0 as Field3,0 as Field4,Saletype as Field5 from tblItem";
                //_objSearch.WhereNdGroupBy = "where Status=1 and Saletype=1";
                //_objSearch.OrderBy = "";
                //var CheckData = Searchcount.GetSearchData("prdSearch", _objSearch);
                //if (CheckData.Count == 0)
                //{
                //    CMessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}

                //using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))

                //{
                //    var dialogResult = search.ShowDialog();
                //    if (dialogResult == DialogResult.OK)
                //    {
                //        returnValue = Convert.ToString(search._retValue);
                //    }
                //}
                //if (returnValue.ToString() != "")
                //{                  
               //if(checkOffer())
                //    //{
                //        ClearAll();
                //        string pid = Convert.ToString(returnValue);
                //        BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                //        DmnSearch objSearch = new DmnSearch();
                //        objSearch = new DmnSearch();
                //        objSearch.Select = "*";
                //        objSearch.Table = "tblProduct";
                //        objSearch.WhereNdGroupBy = "where ProductId=" + pid;
                //        objSearch.OrderBy = "";
                //        var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                //        if (gridSource.Count > 0)
                //        {
                //            txtpid.Text = Convert.ToString(gridSource[0].ProductId);
                //            txtproduct.Text = gridSource[0].Product;
                //            txtrprice.Text = Convert.ToString(gridSource[0].Price);
                //            txtrdisper.Text = Convert.ToString(gridSource[0].DiscountPer);
                //            txtrdisamt.Text = Convert.ToString(gridSource[0].DiscountAmt);
                //        }
                //        txtoffername.Focus();
                //    //}                   
                //}
                #endregion
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error");
            }
        }
        public bool checkOffer()
        {
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = "tblProduct";
            objSearch.WhereNdGroupBy = "Where status=1 and ItemCode = '" + txtproduct.Text + "' ";// and BranchId = " + branch; ; ;
            objSearch.OrderBy = "";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            if (itemSource.Count > 0)
            {
                CMessageBox.Show(" Already Exist on ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        public void ClearAll()
        {           
            if(tlp_combo.Visible)
            {
                txtname_search.Text = "Food Name .....";
                txtitemcode_serach.Text = "Item Code .....";
                foreach (TextBox textBox in new TextBox[] {txtname_search, txtitemcode_serach, txtcombodes, txtcombo_finalprice, txtcombo_disamt, txtcombo_disper, txtcombo_totalprice, txtcmbo_offername, txtcombo_run_disamt, txtcombo_run_disper, txtcombo_run_price })
                {                    
                    textBox.Text = "";
                }
                flp_selected_item_combo.Controls.Clear();
                dgv_sele_cmb_products.Rows.Clear();
                txtcombo_omid.Text = "";
                btn_del_combo.Visible = false;
            }
            else if(tlp_single.Visible)
            {
                btn_del_single.Visible = false;
                txtpid.Text = "0";
                txtproduct.Text = "";
                txtrprice.Text = "";
                txtrdisper.Text = "";
                txtrdisamt.Text = "";
                txtoffername.Text = "";
                discountper.Text = "";
                discountamt.Text = "";
                txtfinalprice.Text = "";
                txtdescription.Text = "";
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
            decimal value;
            if (!decimal.TryParse(discountper.Text, out value))
            {
                discountper.Text = "0";
            }
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
            decimal value;
            if (!decimal.TryParse(discountamt.Text, out value))
            {
                discountamt.Text = "0";
            }
            discountPer();
            netAmount();
        }
        public void discountAmnt()
        {
            if (discountper.Text != "")
            {
                if (Convert.ToDecimal(discountper.Text) > 100)
                {
                    CMessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    discountper.Focus();
                }
                else
                {
                    if ((string.IsNullOrEmpty(txtrprice.Text)))
                        txtrprice.Text = "0.00";
                    decimal discountAmnt = Convert.ToDecimal(discountper.Text.Trim()) * Convert.ToDecimal(txtrprice.Text) / 100;
                    discountamt.Text = discountAmnt.ToString("N2");
                }
            }
        }
        public void discountPer()
        {
            if (discountamt.Text != "" && txtrprice.Text != "" && Convert.ToDecimal(txtrprice.Text) != 0)
            {
                if (Convert.ToDecimal(discountamt.Text) > Convert.ToDecimal(txtrprice.Text))
                {
                    CMessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    discountamt.Focus();
                }
                else
                {
                    decimal discountPer = (Convert.ToDecimal(discountamt.Text) / Convert.ToDecimal(txtrprice.Text) * 100);
                    discountper.Text = discountPer.ToString("N2");
                }
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if(tlp_single.Visible)
                {
                    if (MandatoryCheck_single())
                    {
                        DmnOfferMaster dmnOffermaster = new DmnOfferMaster();
                        DmnOfferMaster InsertStatusM = new DmnOfferMaster();
                        BllOffer bllmaster = new BllOffer();
                        dmnOffermaster.Omid = Convert.ToInt32(txtomid.Text);
                        dmnOffermaster.OfferName = txtoffername.Text;

                        dmnOffermaster.OfferstartDatetime = dtp_from_date_s.Value.Date + dtp_from_time_s.Value.TimeOfDay;
                        dmnOffermaster.OfferEndDatetime = dtp_to_date_s.Value.Date + dtp_to_time_s.Value.TimeOfDay;

                        dmnOffermaster.OfferType = 0;
                        dmnOffermaster.BranchId = 1;
                        dmnOffermaster.CreatedBy = 1;
                        if (chk_single_status.Checked)
                            dmnOffermaster.Status = 1;
                        else
                            dmnOffermaster.Status = 0;
                        InsertStatusM = bllmaster.AddEditDataM("prdInsert_Upd_offermaster", dmnOffermaster);
                        if (InsertStatusM.Omid > 0)
                        {
                            DmnOfferDetails dmnOfferdetails = new DmnOfferDetails();
                            BllOffer blldetails = new BllOffer();
                            dmnOfferdetails.Odid = Convert.ToInt32(txtodid.Text);
                            dmnOfferdetails.Omid = Convert.ToInt32(InsertStatusM.Omid);
                            dmnOfferdetails.ProductId = Convert.ToInt32(txtpid.Text);
                            dmnOfferdetails.Discountper = Convert.ToDecimal(discountper.Text);
                            dmnOfferdetails.DiscountAmt = Convert.ToDecimal(discountamt.Text);
                            dmnOfferdetails.FinalPrice = Convert.ToDecimal(txtfinalprice.Text);
                            dmnOfferdetails.Description = txtdescription.Text;
                            dmnOfferdetails.BranchId = 1;
                            dmnOfferdetails.CreatedBy = 1;
                            dmnOfferdetails.Status = 1;
                            var InsertStatusD = blldetails.AddEditDataD("[prdInsert_Upd_offerdetails]", dmnOfferdetails);
                            if (InsertStatusD.Odid > 0)
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
                else if(tlp_combo.Visible)
                {
                    if (MandatoryCheck_Combo())
                    {
                        int insflag = 0;
                        DmnOfferMaster dmnOffermaster = new DmnOfferMaster();
                        DmnOfferMaster InsertStatusM = new DmnOfferMaster();
                        BllOffer bllmaster = new BllOffer();

                        dmnOffermaster.Omid = Convert.ToInt32(txtcombo_omid.Text);
                        dmnOffermaster.OfferName = txtcmbo_offername.Text;
                        dmnOffermaster.TotalPrice =Convert.ToDecimal(txtcombo_totalprice.Text);

                        dmnOffermaster.OfferstartDatetime = dtp_from_date_s.Value.Date + dtp_from_time_s.Value.TimeOfDay;
                        dmnOffermaster.OfferEndDatetime = dtp_to_date_s.Value.Date + dtp_to_time_s.Value.TimeOfDay;

                        dmnOffermaster.OfferType = 1;
                        dmnOffermaster.BranchId = 1;
                        dmnOffermaster.CreatedBy = 1;       
                        if(chk_combo_status.Checked)
                            dmnOffermaster.Status = 1;
                        else
                            dmnOffermaster.Status = 0;
                        InsertStatusM = bllmaster.AddEditDataM("prdInsert_Upd_offermaster", dmnOffermaster);
                        if (InsertStatusM.Omid > 0)
                        {
                            for (int i = 0; i < dgv_sele_cmb_products.Rows.Count; i++)
                            {
                                if (dgv_sele_cmb_products.Rows[i].Cells[pidno].Value != null && dgv_sele_cmb_products.Rows[i].Cells[qtynoo].Value != "")
                                {
                                    DmnOfferDetails dmnOfferdetails = new DmnOfferDetails();
                                    BllOffer blldetails = new BllOffer();
                                    dmnOfferdetails.Odid = Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[odidno].Value);
                                    dmnOfferdetails.Omid = Convert.ToInt32(InsertStatusM.Omid);
                                    dmnOfferdetails.ProductId = Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[pidno].Value);
                                    dmnOfferdetails.Quantity = Convert.ToDecimal(dgv_sele_cmb_products.Rows[i].Cells[qtynoo].Value);
                                    dmnOfferdetails.Discountper = Convert.ToDecimal(txtcombo_disper.Text);
                                    dmnOfferdetails.DiscountAmt = Convert.ToDecimal(txtcombo_disamt.Text);
                                    dmnOfferdetails.FinalPrice = Convert.ToDecimal(txtcombo_finalprice.Text);
                                    dmnOfferdetails.Description = txtcombodes.Text;
                                    dmnOfferdetails.BranchId = 1;
                                    dmnOfferdetails.CreatedBy = 1;
                                    dmnOfferdetails.Status = Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[statusno].Value);
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
                                if(Convert.ToInt32(txtcombo_omid.Text)>0)
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
            if (string.IsNullOrEmpty(discountamt.Text))
                discountamt.Text = "0";
            if (string.IsNullOrEmpty(discountper.Text))
                discountper.Text = "0";
            if (string.IsNullOrEmpty(txtpid.Text))
            {
                CMessageBox.Show("Please Select the Product ", "Mandaatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtproduct.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtoffername.Text))
            {
                CMessageBox.Show("Please Enter the Offer Name", "Mandaatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtoffername.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtoffername.Text))
            {
                CMessageBox.Show("Please Enter the Offer Name", "Mandaatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtoffername.Focus();
                return false;
            }

            return true;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (tlp_single.Visible)
                {
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    string returnValue = string.Empty;
                    DataTable dtColumnAlign = new DataTable();
                    dtColumnAlign.Columns.Add();
                    DmnSearch _objSearch = new DmnSearch();
                    _objSearch.ColumnHeaders = "ID,Offer Name,Discount %,Discount Amount,Offer Start Date,Offer End Date";
                    _objSearch.Select = "om.Omid as ID,OfferName as Field1,Discountper as Field2,DiscountAmt as Field3,OfferstartDatetime as Field4,OfferEndDatetime as Field5";
                    _objSearch.Table = "tblOfferMaster om left join tblOfferDetails od on od.Omid=om.Omid";
                    _objSearch.WhereNdGroupBy = "where om.Status = 1 and om.OfferType=0";
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
                            objSearch.Select = "om.Omid as Field1,od.odid as Field2,od.ProductId as Field3,OfferName as Field4,od.Discountper as Field5,od.DiscountAmt as Field6,FinalPrice as Field7,p.Product as Field8,p.Price as Field9,od.Description as Field10,p.DiscountPer as Field11,p.DiscountAmt as Field12, om.OfferstartDatetime StartDate, om.OfferEndDatetime EndDate";
                            objSearch.Table = "tblOfferMaster om left join tblOfferDetails od on od.Omid=om.Omid left join tblProduct p on p.ProductId=od.ProductId";
                            objSearch.WhereNdGroupBy = "where om.Status = 1 and om.Omid="+ omid;
                            objSearch.OrderBy = " ORDER BY OfferName";
                            var itemSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                            if (itemSource.Count > 0)
                            {
                                btn_del_single.Visible = true;
                                txtomid.Text = Convert.ToString(itemSource[0].Field1);
                                txtodid.Text = Convert.ToString(itemSource[0].Field2);
                                txtpid.Text = Convert.ToString(itemSource[0].Field3);
                                txtproduct.Text = Convert.ToString(itemSource[0].Field8);
                                txtrprice.Text = Convert.ToString(itemSource[0].Field9);

                                txtrdisper.Text = Convert.ToString(itemSource[0].Field11);
                                txtrdisamt.Text = Convert.ToString(itemSource[0].Field12);

                                discountper.Text = Convert.ToString(itemSource[0].Field5);
                                discountamt.Text = Convert.ToString(itemSource[0].Field6);

                                txtoffername.Text = Convert.ToString(itemSource[0].Field4);             
                                txtfinalprice.Text = Convert.ToString(itemSource[0].Field7);
                                txtdescription.Text = Convert.ToString(itemSource[0].Field10);

                                DateTime fromDateTime = itemSource[0].StartDate;
                                DateTime toDateTime = itemSource[0].EndDate;

                                // Set the "from" date and time
                                dtp_from_date_s.Value = fromDateTime.Date; 
                                dtp_from_time_s.Value = fromDateTime;       

                                // Set the "to" date and time
                                dtp_to_date_s.Value = toDateTime.Date;     
                                dtp_to_time_s.Value = toDateTime;           


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
                    _objSearch.ColumnHeaders = "ID,Offer Name,Discount %,Discount Amount,Offer Start Date,Offer End Date";
                    _objSearch.Select = "om.Omid as ID,OfferName as Field1,Discountper as Field2,DiscountAmt as Field3,OfferstartDatetime as Field4,OfferEndDatetime as Field5";
                    _objSearch.Table = "tblOfferMaster om left join tblOfferDetails od on od.Omid=om.Omid";
                    _objSearch.WhereNdGroupBy = "where om.Status = 1 and om.OfferType=1";
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
                            objSearch.Select = "om.Omid as Field1,od.odid as Field2,od.ProductId as Field3,OfferName as Field4,od.Discountper as Field5,od.DiscountAmt as Field6,FinalPrice as Field7,p.Product as Field8,p.Price as Field9,od.Description as Field10,p.DiscountPer as Field11,p.DiscountAmt as Field12,om.TotalPrice as Field13,od.Quantity as Field14,od.Status as Field15,om.OfferstartDatetime as StartDate,om.OfferEndDatetime as EndDate";
                            objSearch.Table = "tblOfferMaster om left join tblOfferDetails od on od.Omid=om.Omid left join tblProduct p on p.ProductId=od.ProductId";
                            objSearch.WhereNdGroupBy = "where om.Status = 1 and om.Omid=" + omid;
                            objSearch.OrderBy = " ORDER BY OfferName";
                            var itemSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                            if (itemSource.Count > 0)
                            {
                                dgv_sele_cmb_products.Rows.Clear();
                                txtcombo_omid.Text = Convert.ToString(itemSource[0].Field1);
                                flp_selected_item_combo.Controls.Clear();
                                txtcombo_disper.Text = Convert.ToString(itemSource[0].Field5);
                                txtcombo_disamt.Text = Convert.ToString(itemSource[0].Field6);

                                txtcmbo_offername.Text = Convert.ToString(itemSource[0].Field4);
                                txtcombo_totalprice.Text = Convert.ToString(itemSource[0].Field13);
                                txtcombo_finalprice.Text = Convert.ToString(itemSource[0].Field7);
                                txtcombodes.Text = Convert.ToString(itemSource[0].Field10);
                                btn_del_combo.Visible = true;

                                DateTime fromDateTime = itemSource[0].StartDate; 
                                 DateTime toDateTime = itemSource[0].EndDate;

                                dtp_from_date.MinDate= fromDateTime.Date;
                                dtp_to_date.MinDate = toDateTime.Date;

                                // Set the "from" date and time
                                dtp_from_date.Value = fromDateTime.Date;
                                dtp_from_time.Value = fromDateTime;

                                // Set the "to" date and time
                                dtp_to_date.Value = toDateTime.Date;
                                dtp_to_time.Value = toDateTime;

                                dgv_sele_cmb_products.Rows.Add(itemSource.Count);
                                for (int i = 0; i < itemSource.Count; i++)
                                {
                                    if (Convert.ToInt32(itemSource[i].Field15)==1)
                                    {
                                        dgv_sele_cmb_products.Rows[i].Cells[odidno].Value = itemSource[i].Field2;
                                        dgv_sele_cmb_products.Rows[i].Cells[pidno].Value = itemSource[i].Field3;
                                        dgv_sele_cmb_products.Rows[i].Cells[qtynoo].Value = itemSource[i].Field14;
                                        dgv_sele_cmb_products.Rows[i].Cells[statusno].Value = itemSource[i].Field15;

                                        TableLayoutPanel tlp_selected_Item_Box = new TableLayoutPanel();

                                        tlp_selected_Item_Box.Name = "tlp_sele_" + itemSource[i].Field8;
                                        tlp_selected_Item_Box.Width = 245;
                                        tlp_selected_Item_Box.Height = 60;
                                        tlp_selected_Item_Box.ColumnCount = 4;
                                        tlp_selected_Item_Box.BorderStyle = BorderStyle.FixedSingle;
                                        tlp_selected_Item_Box.BackColor = seleproduct_buttoncolor;

                                        //Item Button   
                                        Button Button_Item = new Button();
                                        Button_Item.Text = itemSource[i].Field8;
                                        Button_Item.Name = "btn_sele_" + itemSource[i].Field8;
                                        Button_Item.Tag = itemSource[i].Field3;
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
                                        int intValue = (int)Math.Floor(Convert.ToDecimal(itemSource[i].Field14));
                                        label.Text = Convert.ToString(intValue);
                                        label.Name = "lbl_selected_" + itemSource[i].Field8;
                                        label.Tag = itemSource[i].Field3;
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
                                        Button_Add.Name = "btn_sele_add_" + itemSource[i].Field8;
                                        Button_Add.Tag = itemSource[i].Field8;
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
                                        Button_Remove.Name = "btn_sele_rem_" + itemSource[i].Field8;
                                        Button_Remove.Tag = itemSource[i].Field8;
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
                                pnl_serachproducts.Visible = false;
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
                 decimal netAmount = Convert.ToDecimal(txtrprice.Text) - Convert.ToDecimal(discountamt.Text);
                txtfinalprice.Text = netAmount.ToString("N2");
                
            }
            catch (Exception ex)
            {

            }

        }

        private void txtproduct_Leave(object sender, EventArgs e)
        {
            lblinforF8.Visible = false;           
        }

        private void txtproduct_Enter(object sender, EventArgs e)
        {
            lblinforF8.Visible = true;
        }

        private void btn_AddcomboProduct_Click(object sender, EventArgs e)
        {
            txtitemcode_serach.Text = "";
            txtname_search.Text="";
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
                    button.Text =  gridSource[i].CategoryName;
                    button.Name = "btn" + gridSource[i].CategoryName;
                    button.Tag = gridSource[i].Fcid;
                    button.BackColor = Color.Beige;
                    button.Width = 160;
                    button.Height = 40;
              
                    button.Font = new Font(button.Font.FontFamily, 8, FontStyle.Bold);
                    button.Click += new EventHandler(SidebuttonClickEventHandler);
                    tlp_searchproduct_sidebar.Controls.Add(button, 0, rowIndex);
                    rowIndex++;
                }
                pnl_searchproduct_sidebar.Controls.Add(tlp_searchproduct_sidebar);
            }
        }
        private void SidebuttonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId=p.ProductId ";
                dmnSearch.WhereNdGroupBy = " where p.Status=1 and p.Fcid= "+ clickedControl.Tag + "and m.Status=1";
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
        private void DashboardButtonClickEventHandler(object sender, EventArgs e)
        {
            //item Chosse from serach Box 
            try
            {
                Control clickedControl = sender as Control;
                string buttonname = "btn_sele_" + clickedControl.Text;
                Button buttonitem = this.Controls.Find(buttonname, true).FirstOrDefault() as Button; 
                if(buttonitem!=null)
                {
                    string lblname = "lbl_selected_" + clickedControl.Text;
                    Label lblqty = this.Controls.Find(lblname, true).FirstOrDefault() as Label;
                    int cur_qty = Convert.ToInt32(lblqty.Text) + 1;
                    lblqty.Text = Convert.ToString(cur_qty);
                    CalcTotalAmtCombo(Convert.ToInt32(buttonitem.Tag),1);
                    pnl_serachproducts.Visible = false;
                }
                else
                {
                    dgv_sele_cmb_products.Rows.Add();
                    int rowIndex = dgv_sele_cmb_products.Rows.Count - 1;  
                    dgv_sele_cmb_products.Rows[rowIndex].Cells[pidno].Value = clickedControl.Tag;
                    dgv_sele_cmb_products.Rows[rowIndex].Cells[statusno].Value = 1;

                    CalcTotalAmtCombo(Convert.ToInt32(clickedControl.Tag),1);
                    TableLayoutPanel tlp_selected_Item_Box = new TableLayoutPanel();
                   
                    tlp_selected_Item_Box.Name = "tlp_sele_" + clickedControl.Text;
                    tlp_selected_Item_Box.Width = 245;
                    tlp_selected_Item_Box.Height = 60;
                    tlp_selected_Item_Box.ColumnCount = 4;
                    tlp_selected_Item_Box.BorderStyle = BorderStyle.FixedSingle;
                    tlp_selected_Item_Box.BackColor = seleproduct_buttoncolor;

                     //Item Button   
                     Button Button_Item = new Button();
                    Button_Item.Text = clickedControl.Text;
                    Button_Item.Name = "btn_sele_" + clickedControl.Text;
                    Button_Item.Tag = clickedControl.Tag;
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
                    label.Name = "lbl_selected_" + clickedControl.Text;
                    label.Tag = clickedControl.Text;
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
                    Button_Add.Name = "btn_sele_add_" + clickedControl.Text;
                    Button_Add.Tag = clickedControl.Text;
                    Button_Add.Width = 20;
                    Button_Add.Height = 50;
                    Button_Add.FlatAppearance.BorderSize = 0;
                    Button_Add.FlatAppearance.BorderColor = Button_Add.BackColor;
                    Button_Add.FlatStyle = FlatStyle.Flat;
                    Button_Add.BackColor = Color.White;
                    Button_Add.Click += new EventHandler(Button_AddClickEventHandler);
                    Button_Add.Font = new Font(Button_Add.Font.FontFamily, 10, FontStyle.Bold);
                    Button_Add.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0,0, Button_Add.Width, Button_Add.Height, Button_Add.Width, Button_Add.Height));
                    // Adding the button to the third column
                    tlp_selected_Item_Box.Controls.Add(Button_Add, 2, 0);

                    //Remove Button
                    Button Button_Remove = new Button();
                    Button_Remove.Text = "-";
                    Button_Remove.Name = "btn_sele_rem_" + clickedControl.Text;
                    Button_Remove.Tag = clickedControl.Text;
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
                    netAmount_combo();
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
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblProduct";
                objSearch.WhereNdGroupBy = "where ProductId=" + clickedControl.Tag;
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    //txtpid.Text = Convert.ToString(gridSource[0].ProductId);
                    //txtproduct.Text = gridSource[0].Product;
                    txtcombo_run_price.Text = Convert.ToString(gridSource[0].Price);
                    txtcombo_run_disper.Text = Convert.ToString(gridSource[0].DiscountPer);
                    txtcombo_run_disamt.Text = Convert.ToString(gridSource[0].DiscountAmt);
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void Button_AddClickEventHandler(object sender, EventArgs e)
        {
            try
            {
       
                Control clickedControl = sender as Control;
                string lblname = "lbl_selected_" + clickedControl.Tag;
                string buttonname = "btn_sele_" + clickedControl.Tag;
                // Find the TableLayoutPanel control by name
                Label lblqty = this.Controls.Find(lblname, true).FirstOrDefault() as Label;
                if(lblqty != null)
                {
                    int cur_qty = Convert.ToInt32(lblqty.Text) + 1;
                    lblqty.Text = Convert.ToString(cur_qty);
                    Button buttonitem = this.Controls.Find(buttonname, true).FirstOrDefault() as Button;
                    if (buttonitem != null)                    
                        CalcTotalAmtCombo(Convert.ToInt32(buttonitem.Tag), cur_qty);
                    discountAmnt_combo();
                    discountPer_combo();
                    netAmount_combo();
                }
  
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }
        public void CalcTotalAmtCombo(int pid,int qty)
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
                    if (string.IsNullOrEmpty(txtcombo_totalprice.Text))
                        txtcombo_totalprice.Text = "0";
                    for(int i=0;i< dgv_sele_cmb_products.Rows.Count;i++)
                    {
                        if(Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[pidno].Value)== pid)
                        {
                            dgv_sele_cmb_products.Rows[i].Cells[qtynoo].Value = qty;
                            break;
                        }
                        else
                        {
                            int rowIndex = dgv_sele_cmb_products.Rows.Count - 1;
                            dgv_sele_cmb_products.Rows[rowIndex].Cells[qtynoo].Value = qty;
                            break;
                        }
                    }               
                    decimal totalamt = Convert.ToDecimal(gridSource[0].Price) + Convert.ToDecimal(txtcombo_totalprice.Text);
                    txtcombo_totalprice.Text = Convert.ToString(totalamt);               
                }
            }
            catch(Exception ex)
            {

            }
            
        }
        public void CalcTotalAmtCombo_remove(int pid,int qty)
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
                       if (Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[pidno].Value) == pid)
                        {
                            flag = false;
                            if (qty == 0)
                            {
                                dgv_sele_cmb_products.Rows[i].Cells[qtynoo].Value = qty;
                                dgv_sele_cmb_products.Rows[i].Cells[statusno].Value = 0;
                                break;
                            }
                            else
                            {
                                dgv_sele_cmb_products.Rows[i].Cells[qtynoo].Value = qty;
                                break;
                            }                
                        }                       
                    }
                    if(flag)
                    {
                        int rowIndex = dgv_sele_cmb_products.Rows.Count - 1;
                        dgv_sele_cmb_products.Rows[rowIndex].Cells[qtynoo].Value = qty;
                    }
                    decimal totalamt = Convert.ToDecimal(txtcombo_totalprice.Text) - Convert.ToDecimal(gridSource[0].Price);
                    txtcombo_totalprice.Text = Convert.ToString(totalamt);
                    discountAmnt_combo();
                    discountPer_combo();
                    netAmount_combo();
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void Button_RemoveClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                string lblname = "lbl_selected_" + clickedControl.Tag;
                string itemname = "tlp_sele_" + clickedControl.Tag;
                string buttonname = "btn_sele_" + clickedControl.Tag;
                // Find the TableLayoutPanel control by name
                Label lblqty = this.Controls.Find(lblname, true).FirstOrDefault() as Label;
                if (lblqty != null)
                {
                    int cur_qty = Convert.ToInt32(lblqty.Text) - 1;
                    if(cur_qty==0)
                    {
                        Button buttonitem = this.Controls.Find(buttonname, true).FirstOrDefault() as Button;
                        if (buttonitem != null)
                            CalcTotalAmtCombo_remove(Convert.ToInt32(buttonitem.Tag), cur_qty);
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
                        Button buttonitem = this.Controls.Find(buttonname, true).FirstOrDefault() as Button;
                        if (buttonitem != null)
                            CalcTotalAmtCombo_remove(Convert.ToInt32(buttonitem.Tag), cur_qty);
                    }
                      
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void btn_AddcomboProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                ProductPopup();
            }
        }

        private void btn_serachproduct_close_Click(object sender, EventArgs e)
        {
            pnl_serachproducts.Visible = false;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                //case Keys.Tab:
                //    Movefocus(1);
                //    return true; // Mark the key as handled
                //case Keys.Enter:
                //    return true; // Mark the key as handled
                case Keys.Escape:
                    if(pnl_serachproducts.Visible)
                        pnl_serachproducts.Visible = false;
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
                txtname_search.ForeColor = Color.Black;
            }
        }

        private void txtitemcode_serach_Leave(object sender, EventArgs e)
        {
            if (txtitemcode_serach.Text == "")
            {
                txtitemcode_serach.Text = "Item Code .....";
                txtitemcode_serach.ForeColor = Color.Black;
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
                    CMessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            netAmount_combo();
        }

        private void txtcombo_disamt_Leave(object sender, EventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtcombo_disamt.Text, out value))
            {
                txtcombo_disamt.Text = "0";
            }
            discountPer_combo();
            netAmount_combo();
        }
        public void netAmount_combo()
        {
            try
            {
                if ((string.IsNullOrEmpty(txtcombo_finalprice.Text)))
                    txtcombo_finalprice.Text = "0.00";
                decimal netAmount = Convert.ToDecimal(txtcombo_totalprice.Text) - Convert.ToDecimal(txtcombo_disamt.Text);
                txtcombo_finalprice.Text = netAmount.ToString("N2");
            }
            catch (Exception ex)
            {

            }

        }
        public bool MandatoryCheck_Combo()
        {
            if (string.IsNullOrEmpty(discountamt.Text))
                discountamt.Text = "0";
            if (string.IsNullOrEmpty(discountper.Text))
                discountper.Text = "0";
            if (string.IsNullOrEmpty(txtcombo_omid.Text))
                txtcombo_omid.Text = "0";
            if (flp_selected_item_combo.Controls.Count == 0 && Convert.ToInt32(txtcombo_omid.Text) > 0)
            {
                var result = CMessageBox.Show("Do You want Delete This Combo", "Confirm The Action", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    chk_combo_status.Checked = false;
                    return true;
                }
            }
                if (flp_selected_item_combo.Controls.Count == 1)
            {
                CMessageBox.Show("Minimium Two Products Added To The List", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btn_AddcomboProduct.Focus();
                return false;
            }

            if (flp_selected_item_combo.Controls.Count==0 && Convert.ToInt32(txtcombo_omid.Text)==0 )
            {
                CMessageBox.Show("Please Select the Products", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btn_AddcomboProduct.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtcmbo_offername.Text))
            {
                CMessageBox.Show("Please Enter the Offer Name", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcmbo_offername.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtcombo_finalprice.Text))
            {
                CMessageBox.Show("Please Enter the Final price", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcombo_finalprice.Focus();
                return false;
            }
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

        private void txtname_search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
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
            }
            catch(Exception ex)
            {

            }

        }

        private void txtitemcode_serach_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "*";
                    dmnSearch.Table = "tblProduct";
                    dmnSearch.WhereNdGroupBy = "where Status=1 and ItemCode = '" + txtitemcode_serach.Text+"'";
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
            }
            catch(Exception ex)
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
            if ( Convert.ToDecimal(txtcombo_totalprice.Text)>Convert.ToDecimal(txtcombo_finalprice.Text))
            {
                CMessageBox.Show("Final amount not be greater than Tota Amount ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtcombo_finalprice.Focus();
            }
        }

        private void btn_del_single_Click(object sender, EventArgs e)
        {
            var result = CMessageBox.Show("Do You want Delete This Offer", "Confirm The Action", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                chk_single_status.Checked = false;
                btnsave.PerformClick();
            }
        }


            private void dtp_from_date_s_ValueChanged(object sender, EventArgs e)
            {
                DateTime selectedDate = dtp_from_date_s.Value.Date;
                DateTime currentDate = DateTime.Now.Date;
                if (selectedDate < currentDate)
                {
                    dtp_from_date_s.Value = currentDate;
                }
            }

        private void dtp_to_date_s_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dtp_to_date_s.Value.Date;
            DateTime currentDate = DateTime.Now.Date;
            if (selectedDate < currentDate)
            {
                dtp_to_date_s.Value = currentDate;
            }

            DateTime fromDate = dtp_from_date_s.Value;
            DateTime toDate = dtp_to_date_s.Value;
            if (toDate < fromDate)
            {
                CMessageBox.Show("To Date Cannot Be Before From Date. Please Select a Valid Date.");
                dtp_to_date_s.Value = fromDate;
            }
        }

        private void dtp_from_time_s_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dtp_from_date_s.Value.Date;
            DateTime selectedTime = dtp_from_time_s.Value;

            DateTime currentDateTime = DateTime.Now;
            DateTime selectedDateTime = selectedDate + selectedTime.TimeOfDay;

            if (selectedDateTime < currentDateTime)
            {
                dtp_from_time_s.Value = currentDateTime;
            }
        }

        private void dtp_from_date_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dtp_from_date.Value.Date;
            DateTime currentDate = DateTime.Now.Date;
            if (selectedDate < currentDate)
            {
                dtp_from_date.Value = currentDate;
            }
        }

        private void dtp_from_time_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dtp_from_date.Value.Date;
            DateTime selectedTime = dtp_from_time.Value;

            DateTime currentDateTime = DateTime.Now;
            DateTime selectedDateTime = selectedDate + selectedTime.TimeOfDay;

            if (selectedDateTime < currentDateTime)
            {
                dtp_from_time.Value = currentDateTime;
            }
        }

        private void dtp_to_date_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dtp_to_date.Value.Date;
            DateTime currentDate = DateTime.Now.Date;
            if (selectedDate < currentDate)
            {
                dtp_to_date.Value = currentDate;
            }

            DateTime fromDate = dtp_from_date.Value;
            DateTime toDate = dtp_to_date.Value;
            if (toDate < fromDate)
            {
                CMessageBox.Show("To Date Cannot Be Before From Date. Please Select a Valid Date.");
                dtp_to_date.Value = fromDate;
            }
        }

        private void btn_grid_product_close_Click(object sender, EventArgs e)
        {
            tlp_product_list.Visible = false;
        }

        private void dgv_product_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if(Convert.ToInt32(dgv_product_list.Rows[e.RowIndex].Cells[dsaletypeno].Value)==1 )
            {

            }
            else
            {

            }
        }

    }
}
