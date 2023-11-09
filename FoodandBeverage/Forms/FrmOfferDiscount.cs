using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Forms.UserControls;
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
using static FoodandBeverage.Interfaces.Interfaces;

namespace FoodandBeverage.Forms
{
    public partial class FrmOfferDiscount : Form
    {
        public FrmOfferDiscount()
        {
            InitializeComponent();
        }

        #region Declaration 
        TableLayoutPanel tlp_searchproduct_sidebar = new TableLayoutPanel();
        TableLayoutPanel tlp_searchproduct_dashboard = new TableLayoutPanel();

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
            tlp_single.Visible = true;
            tlp_main.Controls.Add(tlp_single);
            tlp_single.Dock = DockStyle.Fill;
            textb_shape();
        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtdescription, txtfinalprice, discountamt, discountper, txtoffername, txtrdisamt, txtrdisper, txtrprice, txtproduct, txtname_search, txtitemcode_serach })
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
                BllSearch<DmnGridFields> Searchcount = new BllSearch<DmnGridFields>();
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Product Name,Price,Discount %,Discount Amount";
                _objSearch.Select = "p.ProductId as Id,p.Product as Field1,p.Price as Field2,p.DiscountPer as Field3,p.DiscountAmt as Field4 ";
                _objSearch.Table = "tblProduct p" +
                    " LEFT JOIN tblMenu m ON p.ProductId=m.ProductId";
                _objSearch.WhereNdGroupBy = "where p.Status=1 and m.Status=1";
                _objSearch.OrderBy = " ORDER BY p.Product ";
                var CheckData = Searchcount.GetSearchData("prdSearch", _objSearch);
                if (CheckData.Count == 0)
                {
                    CMessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))

                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);
                    }
                }
                if (returnValue.ToString() != "")
                {                  
                    //if(checkOffer())
                    //{
                        ClearAll();
                        string pid = Convert.ToString(returnValue);
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
                            txtpid.Text = Convert.ToString(gridSource[0].ProductId);
                            txtproduct.Text = gridSource[0].Product;
                            txtrprice.Text = Convert.ToString(gridSource[0].Price);
                            txtrdisper.Text = Convert.ToString(gridSource[0].DiscountPer);
                            txtrdisamt.Text = Convert.ToString(gridSource[0].DiscountAmt);
                        }
                        txtoffername.Focus();
                    //}                   
                }
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
            txtpid.Text = "0";
            txtproduct.Text = "";
            txtrprice.Text = "";
            txtrdisper.Text ="";
            txtrdisamt.Text = "";
            txtoffername.Text = "";
            discountper.Text = "";
            discountamt.Text = "";
            txtfinalprice.Text = "";
            txtdescription.Text = "";
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

                        dmnOffermaster.Omid = Convert.ToInt32("0");
                        dmnOffermaster.OfferName = txtoffername.Text;
                        dmnOffermaster.OfferType = 0;
                        dmnOffermaster.BranchId = 1;
                        dmnOffermaster.CreatedBy = 1;
                        dmnOffermaster.Status = 1;
                        InsertStatusM = bllmaster.AddEditDataM("prdInsert_Upd_offermaster", dmnOffermaster);
                        if (InsertStatusM.Omid > 0)
                        {
                            DmnOfferDetails dmnOfferdetails = new DmnOfferDetails();
                            BllOffer blldetails = new BllOffer();
                            dmnOfferdetails.Odid = Convert.ToInt32("0");
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
                                CMessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearAll();
                            }
                            else                            
                                CMessageBox.Show("Saved Failed", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            

                        }



                        //for (int i = 0; i < dgvProduct.Rows.Count; i++)
                        //{
                        //    if (dgvProduct.Rows[i].Cells[iitemId].Value != null && dgvProduct.Rows[i].Cells[0].Value != "")
                        //    {
                        //        dmnPurchase.PurchaseReturnDetailId = Convert.ToInt32("0");
                        //        dmnPurchase.PurchaseReturnMasterId = Convert.ToInt32(InsertStatusP.PurchaseReturnMasterId);
                        //        dmnPurchase.Barcode = Convert.ToString(dgvProduct.Rows[i].Cells[ibarcode].Value);
                        //        dmnPurchase.Name = dgvProduct.Rows[i].Cells[iname].Value.ToString();
                        //        dmnPurchase.ItemId = Convert.ToInt32(dgvProduct.Rows[i].Cells[iitemId].Value);
                        //        dmnPurchase.QTY = Convert.ToInt32(dgvProduct.Rows[i].Cells[iqty].Value);
                        //        dmnPurchase.Prate = Convert.ToDecimal(dgvProduct.Rows[i].Cells[iprate].Value.ToString());
                        //        dmnPurchase.Amount = Convert.ToDecimal(dgvProduct.Rows[i].Cells[iamount].Value.ToString());
                        //        dmnPurchase.DisPer = Convert.ToDecimal(dgvProduct.Rows[i].Cells[idisper].Value.ToString());
                        //        dmnPurchase.DisAmnt = Convert.ToDecimal(dgvProduct.Rows[i].Cells[idisamt].Value.ToString());
                        //        dmnPurchase.TotalAmnt = Convert.ToDecimal(dgvProduct.Rows[i].Cells[inetamount].Value.ToString());
                        //        dmnPurchase.Type = dgvProduct.Rows[i].Cells[iproducttype].Value.ToString();
                        //        dmnPurchase.CreatedBy = 1;
                        //        dmnPurchase.BranchId = 1;
                        //        dmnPurchase.Status = 1;
                        //        dmnPurchase.PurchaseMasterId = Convert.ToInt32(dgvProduct.Rows[i].Cells[iPurchasemasterid].Value.ToString());
                        //        dmnPurchase.EntryNo = dgvProduct.Rows[i].Cells[ipurchasemasterentryno].Value.ToString();
                        //        dmnPurchase.Unit = dgvProduct.Rows[i].Cells[iunit].Value.ToString();
                        //        dmnPurchase.PurchaseDetailId = Convert.ToInt32(dgvProduct.Rows[i].Cells[iPurchaseDetailsId].Value.ToString());

                        //        dmnPurchase.TaxType = Convert.ToString(dgvProduct.Rows[i].Cells[itaxtype].Value);
                        //        dmnPurchase.Tax = Convert.ToString(dgvProduct.Rows[i].Cells[itax].Value);

                        //        dmnPurchase.TaxAmount = Convert.ToDecimal(dgvProduct.Rows[i].Cells[itaxamt].Value);
                        //        dmnPurchase.TaxPer = Convert.ToDecimal(dgvProduct.Rows[i].Cells[itaxper].Value);
                        //        dmnPurchase.PrId = Convert.ToInt32(dgvProduct.Rows[i].Cells[iprid].Value);
                        //        var InsertStatus = bllPurchase.AddEditDataDet("prdInsert_Upd_PurchaseReturnDetails", dmnPurchase);
                        //        if (InsertStatus == null)
                        //        {
                        //            CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //            break;
                        //        }
                        //        if (InsertStatus != null)
                        //        {
                        //            insflag = 1;
                        //        }
                        //    }
                        //}
                        //if (insflag == 1)
                        //{
                        //    CMessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    ClearAll();
                        //}
                    }
                }

               
            }
            catch (Exception ex)
            { }
        }
        public bool MandatoryCheck_single()
        {
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
                dmnSearch.Table = "tblProduct";
                dmnSearch.WhereNdGroupBy = "where Status=1 and Fcid= "+ clickedControl.Tag;
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
                        button.BackColor = Color.Gold;

                        // Create a circular Region
                        System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                        path.AddEllipse(0, 0, button.Width, button.Height);
                        button.Region = new Region(path);

                        button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                        //button.Dock = DockStyle.Fill;
                        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                        tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                        columnIndex++;
                    }
                    pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void DashboardButtonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                Button button = new Button();
                button.Text = clickedControl.Text;
                button.Name = "btn_sele_" + clickedControl.Text;
                button.Tag = clickedControl.Tag;
                button.Width = 170;
                button.Height = 50;

                // Set the FlatAppearance properties to remove the border
                button.FlatAppearance.BorderSize = 0; // Set border width to 0
                button.FlatAppearance.BorderColor = button.BackColor;
                button.FlatStyle = FlatStyle.Flat;
                button.BackColor = Color.LightCyan;

                //// Create a circular Region
                //System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                //path.AddEllipse(0, 0, button.Width, button.Height);
                //button.Region = new Region(path);

                button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);          
                flp_selected_item_combo.Controls.Add(button);
                pnl_serachproducts.Visible = false;

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
                txtname_search.ForeColor = Color.LightGray;
            }
        }

        private void txtitemcode_serach_Leave(object sender, EventArgs e)
        {
            if (txtitemcode_serach.Text == "")
            {
                txtitemcode_serach.Text = "Item Code .....";
                txtitemcode_serach.ForeColor = Color.LightGray;
            }
        }
    }
}
