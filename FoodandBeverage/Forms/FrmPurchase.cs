using BLL;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmPurchase : Form
    {
        public FrmPurchase()
        {
            InitializeComponent();
        }

        #region Grid Declaration 
        // Registration Grid
        public static int islno = 0;
        public static int ibarcode = 1;
        public static int iname = 2;
        public static int iunit = 3;
        public static int iqty = 4;
        public static int iprate = 5;
        public static int iamount = 6;
        public static int idisper = 7;
        public static int idisamt = 8;
        public static int itaxper = 9;
        public static int itaxamt = 10;
        public static int itotal = 11;
        public static int iitemid = 12;
        public static int itax = 13;
        public static int itaxtype = 14;
        public static int israte = 15;
        public static int imrp = 16;
        public static int itype = 17;
        public static int ibarcode2 = 18;
        public static int itaxsettingid = 19;
        public static int iunitid = 20; 
        public static int ipridno = 21;
        
        // Popup Grid
        public static int pslno = 0;
        public static int pnameno = 1;
        public static int prateno = 2;
        public static int pqohno = 3;
        public static int pitemidno = 4;
        public static int pcategorytypeno = 5;

        #endregion
        public static bool CellRow = true;
        BllSearch<DmnGridFields> _bllSearch = new BllSearch<DmnGridFields>();
        DmnSearch dmnSearch = new DmnSearch();
        private void btn_vendor_add_Click(object sender, EventArgs e)
        {
            pnlCustomer.Visible = true;
            pnlCustomer.Location = new Point(this.ClientSize.Width / 2 - pnlCustomer.Size.Width / 2, this.ClientSize.Height / 2 - pnlCustomer.Size.Height / 2);
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ClearAll()
        {
            dgvProduct.Rows.Clear();
            dgvProduct.Rows.Add();
            invoiceno.Text = "";
            txtdocno.Text = "";
            custname.SelectedIndex = -1;
            address.Text = "";
            contactno.Text = "";
            txtEmail.Text = "";
            discountper.Text = "0.00";
            totaldiscountamt.Text = "0.00";
            remarks.Text = "";
            oaamount.Text = "0.00";
            oadesc.Text = "";
            odamt.Text = "0.00";
            oddesc.Text = "";
            netamount.Text = "0.00";
            sterms.Text = "";
            rcvamt.Text = "0.00";
            lblNetAmt.Text = "0.00";
            lblBalAmt.Text = "0.00";
            lblTotqty.Text = "0.00";
            lblTotDisc.Text = "0.00";
            lblTotal.Text = "0.00";
            netamount.Text = "0.00";
            lblGrandTot.Text = "0.00";
            lblBalAmt.Text = "0.00";
        }

            #region Grid Calulation Function  totalcellamount cellTaxAmount cellDiscountAmount cellDiscountPercentage  grandTotal totQty totDisc discountAmnt discountPer netAmount
            public void totalcellamount(int cellproductid)
        {
            try
            {
                if (dgvProduct.Rows[cellproductid].Cells[iitemid].Value != "" && dgvProduct.Rows[cellproductid].Cells[iprate].Value != "" && dgvProduct.Rows[cellproductid].Cells[iqty].Value != "")
                {
                    decimal amount = Convert.ToDecimal(dgvProduct.Rows[cellproductid].Cells[iprate].Value) * Convert.ToDecimal(dgvProduct.Rows[cellproductid].Cells[iqty].Value);
                    dgvProduct.Rows[cellproductid].Cells[iamount].Value = amount.ToString("N2");
                    if (dgvProduct.CurrentCell.ColumnIndex == idisamt)
                    {
                        cellDiscountPercentage(cellproductid);
                    }
                    if (dgvProduct.CurrentCell.ColumnIndex == idisper || dgvProduct.CurrentCell.ColumnIndex == iqty || dgvProduct.CurrentCell.ColumnIndex == iprate)
                    {
                        cellDiscountAmount(cellproductid);
                    }
                    decimal discount = Convert.ToDecimal(dgvProduct.Rows[cellproductid].Cells[idisamt].Value);
                    cellTaxAmount(cellproductid, amount - discount);
                    if (Convert.ToString(dgvProduct.Rows[cellproductid].Cells[itax].Value) == "Exclusive")
                    {
                        decimal tot = (amount - Convert.ToDecimal(dgvProduct.Rows[cellproductid].Cells[idisamt].Value))
                        + Convert.ToDecimal(dgvProduct.Rows[cellproductid].Cells[itaxamt].Value);
                        dgvProduct.Rows[cellproductid].Cells[itotal].Value = tot.ToString("N2");
                    }
                    if (Convert.ToString(dgvProduct.Rows[cellproductid].Cells[itax].Value) == "Inclusive")
                    {
                        decimal tot = (amount - Convert.ToDecimal(dgvProduct.Rows[cellproductid].Cells[idisamt].Value));
                        dgvProduct.Rows[cellproductid].Cells[itotal].Value = tot.ToString("N2");
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
                string cellpopupid = Convert.ToString(dgvProduct.Rows[cellproductid].Cells[iitemid].Value);
                if (cellpopupid != "")
                {
                    BllSearch<DmnTaxSettings> _bllSearch1 = new BllSearch<DmnTaxSettings>();
                    dmnSearch.Select = "TaxType, IgstPrcnt,VatPrcnt";
                    dmnSearch.Table = "tblTaxSettings";
                    dmnSearch.WhereNdGroupBy = " where status=1 and TaxSettingsId= " + dgvProduct.Rows[cellproductid].Cells[itaxsettingid].Value;
                    dmnSearch.OrderBy = "";
                    var gridSource = _bllSearch1.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource[0].TaxType == "GST")
                    {
                        decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].IgstPrcnt) * amount / 100);
                        dgvProduct.Rows[cellproductid].Cells[itaxamt].Value = TaxAmount.ToString("N2");
                        dgvProduct.Rows[cellproductid].Cells[itaxper].Value = gridSource[0].IgstPrcnt;
                    }
                    if (gridSource[0].TaxType == "VAT")
                    {
                        decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].VatPrcnt) * amount / 100);
                        dgvProduct.Rows[cellproductid].Cells[itaxamt].Value = TaxAmount.ToString("N2");
                        dgvProduct.Rows[cellproductid].Cells[itaxper].Value = gridSource[0].VatPrcnt;
                    }
                }
            }
            catch(Exception ex)
            {

            }
   
        }
        public void cellDiscountAmount(int cellproductid)
        {
            try
            {
                if (dgvProduct.Rows[cellproductid].Cells[idisper].Value != "" && dgvProduct.Rows[cellproductid].Cells[iamount].Value != "")
                {
                    decimal disAmnt = (Convert.ToDecimal(dgvProduct.Rows[cellproductid].Cells[idisper].Value) * Convert.ToDecimal(dgvProduct.Rows[cellproductid].Cells[iamount].Value)) / 100;
                    dgvProduct.Rows[cellproductid].Cells[idisamt].Value = disAmnt.ToString("N2");
                }
            }
            catch (Exception ex)
            {

            }
            
        }
       
        public void cellDiscountPercentage(int cellproductid)
        {
            try
            {
                if (Convert.ToDecimal(dgvProduct.Rows[cellproductid].Cells[iamount].Value) > 0 && dgvProduct.Rows[cellproductid].Cells[idisamt].Value != "")
                {
                    decimal disPerc = (Convert.ToDecimal(dgvProduct.Rows[cellproductid].Cells[idisamt].Value) / Convert.ToDecimal(dgvProduct.Rows[cellproductid].Cells[iamount].Value)) * 100;
                    dgvProduct.Rows[cellproductid].Cells[idisper].Value = disPerc.ToString("N2");
                }
            }
            catch (Exception ex)
            {

            }
            
        }
   
        public void grandTotal()
        {
            try
            {
                decimal sum = 0, qty = 0, dis = 0;
                int n = dgvProduct.RowCount;
                for (int i = 0; i < n; i++)
                {
                    if (dgvProduct.Rows[i].Cells[iitemid].Value != "" && dgvProduct.Rows[i].Cells[iitemid].Value != null)
                    {
                        sum = sum + Convert.ToDecimal(dgvProduct.Rows[i].Cells[itotal].Value);
                        qty = qty + Convert.ToDecimal(dgvProduct.Rows[i].Cells[iqty].Value);
                        dis = dis + Convert.ToDecimal(dgvProduct.Rows[i].Cells[idisamt].Value);
                    }
                }
                lblTotqty.Text = qty.ToString("N2");
                lblTotDisc.Text = dis.ToString("N2");
                lblTotal.Text = sum.ToString("N2");
                lblGrandTot.Text = sum.ToString("N2");
                netAmount();
            }
            catch (Exception ex)
            {

            }
        }

        public void totQty()
        {
            decimal sum = 0;
            int n = dgvProduct.RowCount;
            for (int i = 0; i < n; i++)
            {
                if (dgvProduct.Rows[i].Cells[iqty].Value != "")
                    sum = sum + Convert.ToDecimal(dgvProduct.Rows[i].Cells[iqty].Value);
            }
            lblTotqty.Text = sum.ToString("N2");
        }

        public void totDisc()
        {
            decimal sum = 0;
            int n = dgvProduct.RowCount;
            for (int i = 0; i < n; i++)
            {
                sum = sum + Convert.ToDecimal(dgvProduct.Rows[i].Cells[idisamt].Value);
            }
            lblTotDisc.Text = sum.ToString("N2");
        }
        public void discountAmnt()
        {
            if (discountper.Text != "")
            {
                if (Convert.ToDecimal(discountper.Text) > 100)
                {
                    MessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    discountper.Focus();
                }
                else
                {
                    if ((string.IsNullOrEmpty(lblGrandTot.Text)))
                        lblGrandTot.Text = "0.00";
                    decimal discountAmnt = Convert.ToDecimal(discountper.Text.Trim()) * Convert.ToDecimal(lblGrandTot.Text) / 100;
                    totaldiscountamt.Text = discountAmnt.ToString("N2");
                }
            }
        }
        public void discountPer()
        {
            if (totaldiscountamt.Text != "" && lblGrandTot.Text != "" && Convert.ToDecimal(lblGrandTot.Text) != 0)
            {
                if (Convert.ToDecimal(totaldiscountamt.Text) > Convert.ToDecimal(lblGrandTot.Text))
                {
                    MessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    totaldiscountamt.Focus();
                }
                else
                {
                    decimal discountPer = (Convert.ToDecimal(totaldiscountamt.Text) / Convert.ToDecimal(lblGrandTot.Text) * 100);
                    discountper.Text = discountPer.ToString("N2");
                }
            }
        }

        public void netAmount()
        {
            try
            {
                // if (oaamount.Text != "" && totaldiscountamt.Text != "" && odamt.Text != "")
                decimal value;
                if (decimal.TryParse(odamt.Text, out value) && decimal.TryParse(totaldiscountamt.Text, out value) && decimal.TryParse(oaamount.Text, out value))
                {
                    decimal netAmount = 0;
                    netAmount = (Convert.ToDecimal(lblGrandTot.Text) + Convert.ToDecimal(oaamount.Text))
                        - (Convert.ToDecimal(totaldiscountamt.Text) + Convert.ToDecimal(odamt.Text));
                    netamount.Text = netAmount.ToString("N2");
                    lblNetAmt.Text = netAmount.ToString("N2");
                    decimal balAmnt = netAmount - Convert.ToDecimal(rcvamt.Text);
                    lblBalAmt.Text = balAmnt.ToString("N2");
                }
            }
            catch(Exception ex)
            {

            }
            
        }
#endregion
        #region Vendor 
        public bool VendorMandatoryCheck()
        {
            if (string.IsNullOrEmpty(txtVendorName.Text))
            {
                MessageBox.Show("Vendor Name Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtVendorAddress.Text))
            {
                MessageBox.Show("Vendor Address Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtVendorTel.Text))
            {
                MessageBox.Show("Vendor PhNo Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return false;
            }
            return true;
        }

        #endregion
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                bool CUst = false;
                if (MandatoryCheck())
                {
                    DmnPurchase dmnPurchase = new DmnPurchase();
                    BllPurchase bllPurchase = new BllPurchase();
                    dmnPurchase.EntryNo = invoiceno.Text;
                    dmnPurchase.DocNo = txtdocno.Text;
                    dmnPurchase.DocDate = Convert.ToDateTime(dtpdocdate.Value);
                    dmnPurchase.CustomerId = Convert.ToInt32(custname.SelectedValue);
                    dmnPurchase.Address = address.Text;
                    dmnPurchase.ContactNo = contactno.Text;
                    dmnPurchase.Email = txtEmail.Text;
                    dmnPurchase.TotQty = Convert.ToDecimal(lblTotqty.Text);
                    dmnPurchase.TotalAmnt = Convert.ToDecimal(lblTotal.Text);
                    dmnPurchase.GrandTot = Convert.ToDecimal(lblGrandTot.Text);
                    dmnPurchase.TotalDiscount = Convert.ToDecimal(lblTotDisc.Text);
                    dmnPurchase.DisPer = Convert.ToDecimal(discountper.Text);
                    dmnPurchase.DisAmnt = Convert.ToDecimal(totaldiscountamt.Text);
                    dmnPurchase.OaAmount = Convert.ToDecimal(oaamount.Text);
                    dmnPurchase.OaDesc = oadesc.Text;
                    dmnPurchase.OdAmount = Convert.ToDecimal(odamt.Text);
                    dmnPurchase.OdDesc = oddesc.Text;
                    dmnPurchase.NetAmount = Convert.ToDecimal(netamount.Text);
                    dmnPurchase.RcvAmnt = Convert.ToDecimal(rcvamt.Text);
                    dmnPurchase.BalAmnt = Convert.ToDecimal(lblBalAmt.Text);
                    dmnPurchase.Remark = remarks.Text;
                    dmnPurchase.Terms = sterms.Text;
                    dmnPurchase.BranchId = 1;
                    dmnPurchase.PurchaseMasterId = Convert.ToInt32("0");
                    dmnPurchase.CreatedBy = 1;
                    dmnPurchase.Status = 1;
                    var InsertStatusP = bllPurchase.AddEditData("prdInsert_Upd_PurchaseMaster", dmnPurchase);
                    for (int i = 0; i < dgvProduct.Rows.Count; i++)
                    {
                        if (dgvProduct.Rows[i].Cells[islno].Value != null)
                        {
                            dmnPurchase.PurchaseMasterId = Convert.ToInt32(InsertStatusP.PurchaseMasterId);
                            dmnPurchase.Barcode = Convert.ToString(dgvProduct.Rows[i].Cells[ibarcode].Value);
                            dmnPurchase.Name = Convert.ToString(dgvProduct.Rows[i].Cells[iname].Value);
                            dmnPurchase.Unit = Convert.ToString(dgvProduct.Rows[i].Cells[iunit].Value);
                            dmnPurchase.UnitId = Convert.ToInt32(dgvProduct.Rows[i].Cells[iunitid].Value);                            
                            dmnPurchase.QTY = Convert.ToInt32(dgvProduct.Rows[i].Cells[iqty].Value);
                            dmnPurchase.Prate = Convert.ToDecimal(dgvProduct.Rows[i].Cells[iprate].Value);
                            dmnPurchase.Amount = Convert.ToDecimal(dgvProduct.Rows[i].Cells[iamount].Value);
                            dmnPurchase.DisPer = Convert.ToDecimal(dgvProduct.Rows[i].Cells[idisper].Value);
                            dmnPurchase.DisAmnt = Convert.ToDecimal(dgvProduct.Rows[i].Cells[idisamt].Value);
                            dmnPurchase.TaxPer = Convert.ToDecimal(dgvProduct.Rows[i].Cells[itaxper].Value);
                            dmnPurchase.TaxAmount = Convert.ToDecimal(dgvProduct.Rows[i].Cells[itaxamt].Value);
                            dmnPurchase.TotalAmnt = Convert.ToDecimal(dgvProduct.Rows[i].Cells[itotal].Value);
                            dmnPurchase.ItemId = Convert.ToDecimal(dgvProduct.Rows[i].Cells[iitemid].Value);
                            dmnPurchase.Tax = Convert.ToString(dgvProduct.Rows[i].Cells[itax].Value);
                            dmnPurchase.TaxType = Convert.ToString(dgvProduct.Rows[i].Cells[itaxtype].Value);
                            dmnPurchase.Srate = Convert.ToDecimal(dgvProduct.Rows[i].Cells[israte].Value);
                            dmnPurchase.MRP = Convert.ToDecimal(dgvProduct.Rows[i].Cells[imrp].Value);
                            dmnPurchase.Fcid = Convert.ToInt32(dgvProduct.Rows[i].Cells[itype].Value);
                            dmnPurchase.BranchId =1;
                            dmnPurchase.CreatedBy =1;
                            dmnPurchase.Status = 1;
                            dmnPurchase.PurchaseDetailId = Convert.ToInt32("0");
                            dmnPurchase.Amount = Convert.ToDecimal(dgvProduct.Rows[i].Cells[iamount].Value);
                            dmnPurchase.DisPer = Convert.ToDecimal(dgvProduct.Rows[i].Cells[idisper].Value);
                            dmnPurchase.PrId = Convert.ToInt32(dgvProduct.Rows[i].Cells[ipridno].Value);

                            var InsertStatus = bllPurchase.AddEditDataDet("prdInsert_Upd_PurchaseDetails", dmnPurchase);
                            if (InsertStatus == null)
                            {
                                CUst = true;
                                MessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }

                        }

                    }
                    if (CUst == false)
                    {
                        MessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ClearAll();
                }
            }
            catch (Exception ex)
            {

            }
        }
  
        public bool MandatoryCheck()
        {
            if (string.IsNullOrEmpty(txtdocno.Text))
            {
                MessageBox.Show("Document Number Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtdocno.Focus();
                return false;
            }
            //else if (string.IsNullOrEmpty(invoiceno.Text))
            //{
            //    MessageBox.Show("Entry Number Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    invoiceno.Focus();
            //    return false;
            //}
            else if (Convert.ToInt32(custname.SelectedValue) < 0)
            {
                MessageBox.Show("Vendor Name Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                custname.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(address.Text))
            {
                MessageBox.Show("Address Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                address.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(contactno.Text))
            {
                MessageBox.Show("Contact Number Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                contactno.Focus();
                return false;
            }
            //else if (string.IsNullOrEmpty(txtEmail.Text))
            //{
            //    MessageBox.Show("Email Is Mandatory And All Red Color Fileds Are Mandatory", "Mandaatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtEmail.Focus();
            //    return false;
            //}
            else if (string.IsNullOrEmpty(netamount.Text) || netamount.Text.ToString() == "0")
            {
                MessageBox.Show("Netamount Is Not Zero & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return false;
            }
            //else if (dgvProduct.Rows[0].Cells[iitemid].Value == null)
            //{
            //    MessageBox.Show("No product Found !", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    dgvProduct.Focus();
            //    return false;
            //}
            int c = 0;
            if (dgvProduct.Rows.Count > 1)
            {

                c = dgvProduct.Rows.Count - 1;
            }
            else
            {
                c = dgvProduct.Rows.Count;
            }
            //for (int i = 0; i < c; i++)
            //{
            //    if (dgvProduct.Rows[i].Cells[iitemid].Value == "" || dgvProduct.Rows[i].Cells[iitemid].Value == null)
            //    {
            //        MessageBox.Show("Product Is Not Selected", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return false;
            //        break;
            //    }

            //}
            return true;
        }

        private void btn_grid_regitartion_Click(object sender, EventArgs e)
        {
            tlp_grid_registartion.Visible = true;
            tlp_registartion_main.Visible = false;
            Control controlToRemove = tlp_main.GetControlFromPosition(0, 1);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_grid_registartion, 1, 0);
            tlp_grid_registartion.Dock = DockStyle.Fill;
            tlp_registartion_main.Dock = DockStyle.Fill;

            tlp_textboxes.Visible = true;
            controlToRemove =tlp_regform_downtlp.GetControlFromPosition(0, 3);
            tlp_regform_downtlp.Controls.Remove(controlToRemove);
            tlp_grid_registartion.Controls.Add(tlp_textboxes, 0, 3);
            tlp_textboxes.Dock = DockStyle.Fill;
        }      

        private void btn_back_registration_Click(object sender, EventArgs e)
        {
            tlp_grid_registartion.Visible = false;
            tlp_registartion_main.Visible = true;
            Control controlToRemove = tlp_main.GetControlFromPosition(0, 1);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_registartion_main, 1, 0);
            tlp_registartion_main.Dock = DockStyle.Fill;

            tlp_textboxes.Visible = true;
            controlToRemove = tlp_grid_registartion.GetControlFromPosition(0, 3);
            tlp_grid_registartion.Controls.Remove(controlToRemove);
            tlp_regform_downtlp.Controls.Add(tlp_textboxes, 0, 2);
            tlp_textboxes.Dock = DockStyle.Fill;
        }
        private void Btn_view_menu_Click(object sender, EventArgs e)
        {

        }

        private void custname_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BllSearch<DmnGridFields> bllSearch_CountryCategory = new BllSearch<DmnGridFields>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "Address as Field1,Telephone as Field2,Email as Field3";
            objSearch.Table = " tblVendor";
            objSearch.WhereNdGroupBy = "where isnull(status,1)=1 and VendorId = " + custname.SelectedValue;
            objSearch.OrderBy = "";
            var gridSource = bllSearch_CountryCategory.GetSearchData("prdsearch", objSearch);
            if (gridSource.Count > 0)
            {
                address.Text = gridSource[0].Field1;
                contactno.Text = Convert.ToString(gridSource[0].Field2);
                txtEmail.Text = gridSource[0].Field3;
            }
        }
        public void FillCombobox()
        {
            BllSearch<DmnComboBox> bllSearch_CountryCategory = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "VendorId as Id,VendorName as Value";
            objSearch.Table = " tblVendor";
            objSearch.WhereNdGroupBy = "where isnull(status,1)=1 ";// and BranchId=" + BranchId;
            objSearch.OrderBy = "order by value";
            var gridSource = bllSearch_CountryCategory.GetSearchData("prdComboFill", objSearch);
            custname.DataSource = gridSource;
            custname.ValueMember = "Id";
            custname.DisplayMember = "Value";
            custname.SelectedIndex = 0;
        }

        private void FrmPurchase_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillCombobox();
            tlp_textboxes.Visible = true;
            Control controlToRemove = tlp_grid_registartion.GetControlFromPosition(0, 3);
            tlp_grid_registartion.Controls.Remove(controlToRemove);
            tlp_regform_downtlp.Controls.Add(tlp_textboxes, 0, 2);
            tlp_textboxes.Dock = DockStyle.Fill;
        }


        private void dgvProduct_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvProduct.Rows[e.RowIndex].Cells[iitemid].Value != "" && dgvProduct.Rows[e.RowIndex].Cells[iitemid].Value != null)
                {
                    if (e.ColumnIndex == israte)
                    {
                        DataGridViewTextBoxCell cell = dgvProduct[israte, e.RowIndex] as DataGridViewTextBoxCell;
                        if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == "." || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[iprate].Value) > Convert.ToDecimal(cell.EditedFormattedValue.ToString()))
                        {
                            MessageBox.Show("Sale Rate Mustbe Greater Than Purchase Rate", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //SendKeys.Send("{UP}");
                            e.Cancel = true;
                        }
                    }
                    else if (e.ColumnIndex == iprate)
                    {
                        DataGridViewTextBoxCell cell = dgvProduct[iprate, e.RowIndex] as DataGridViewTextBoxCell;
                        if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == "." || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(cell.EditedFormattedValue) <= 0)
                        {
                            MessageBox.Show("Purchase Rate Is A Mandatory Field", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            e.Cancel = true;
                        }
                    }
                    else if (e.ColumnIndex == imrp)
                    {
                        DataGridViewTextBoxCell cell = dgvProduct[imrp, e.RowIndex] as DataGridViewTextBoxCell;
                        if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == "." || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[israte].Value) > Convert.ToDecimal(cell.EditedFormattedValue.ToString()))
                        {
                            MessageBox.Show("MRP Mustbe Greater Than Sale Rate", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            // SendKeys.Send("{UP}");
                            e.Cancel = true;
                        }
                    }
                    else if (e.ColumnIndex == idisper)
                    {
                        DataGridViewTextBoxCell cell = dgvProduct[idisper, e.RowIndex] as DataGridViewTextBoxCell;
                        if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == "." || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(cell.EditedFormattedValue.ToString()) > 100)
                        {
                            MessageBox.Show("Discount % Must Be Less Than 100", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //SendKeys.Send("{UP}");
                            e.Cancel = true;
                        }
                    }
                    else if (e.ColumnIndex == idisamt)
                    {
                        DataGridViewTextBoxCell cell = dgvProduct[idisamt, e.RowIndex] as DataGridViewTextBoxCell;
                        if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == "." || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(cell.EditedFormattedValue.ToString()) > Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[iamount].Value))
                        {
                            MessageBox.Show("Discount Amount Must Be Less Than Amount", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //SendKeys.Send("{UP}");
                            e.Cancel = true;
                        }

                    }
                    else if (e.ColumnIndex == iqty)
                    {
                        int value;
                        DataGridViewTextBoxCell cell = dgvProduct[iqty, e.RowIndex] as DataGridViewTextBoxCell;
                        if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == "." || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(cell.EditedFormattedValue.ToString()) < 1)
                        {
                            MessageBox.Show("Quantity Must Be Grater Than Or Equal To One", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            e.Cancel = true;
                        }
                        else if (!int.TryParse(Convert.ToString(cell.EditedFormattedValue), out value))
                        {
                            //dgvProduct.Rows[e.RowIndex].Cells[iqty].Value = decimal.ToInt32(Convert.ToDecimal(cell.EditedFormattedValue));
                            dgvProduct.EditingControl.Text = Convert.ToString(decimal.ToInt32(Convert.ToDecimal(cell.EditedFormattedValue)));
                            //MessageBox.Show("Decimal Point Is Not Allowed In Quantity", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //e.Cancel = true;
                            return;
                        }
                    }
                    CellRow = true;

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvProduct_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool s = true;
                if (e.ColumnIndex == idisper || e.ColumnIndex == idisamt || e.ColumnIndex == iqty || e.ColumnIndex == iprate)
                {
                    totalcellamount(e.RowIndex);
                }
                if (e.ColumnIndex == 5 || e.ColumnIndex == 15)
                {
                    if (Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[iprate].Value) > Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[israte].Value))
                    {
                        MessageBox.Show("Sale Rate Mustbe Greater Than Purchase Rate", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        SendKeys.Send("{UP}");
                        s = false;
                        //btnsave.Enabled = false;
                        return;
                    }
                }
                else if (e.ColumnIndex == 15 || e.ColumnIndex == 16)
                {
                    if (Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[israte].Value) > Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[imrp].Value))
                    {
                        MessageBox.Show("MRP Mustbe Greater Than Sale Rate", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        SendKeys.Send("{UP}");
                        s = false;

                    }
                }
               
                if (s == true && e.ColumnIndex != ibarcode)
                {
                    SendKeys.Send("{UP}");
                    SendKeys.Send("{RIGHT}");
                    CellRow = false;
                }


                grandTotal();
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
            else if (e.KeyData == Keys.F8 && dgvProduct.CurrentCell.ColumnIndex == iname)
            {
                //lblToolTipPopup.Visible = false;
                purchasepop();///Convert.ToInt32(mcmbbranch.SelectedValue);
            }
            else if (e.KeyData == Keys.Delete)
            {
                dgvProduct.Rows.RemoveAt(dgvProduct.CurrentCell.RowIndex);
            }
        }
        public void purchasepop()
        {
            panelProductPopup.Location = new Point(
                              this.ClientSize.Width / 2 - panelProductPopup.Size.Width / 2,
                              (this.ClientSize.Height / 3 - panelProductPopup.Size.Height / 2));
            panelProductPopup.Anchor = AnchorStyles.None;
            panelProductPopup.Show();
            dgvItemPop.Rows.Clear();
            dmnSearch.Select = "i.Itemname as Field1,(Select Top(1)Prate from tblProductRate) as Field2,(Select Sum(QOH) from tblProductRate) as Field3,i.ItemId as Field4,c.CategoryName as Field5"; 
            dmnSearch.Table = "tblItem i LEFT JOIN tblCategory_FTI c on c.Fcid = i.Fcid";
            dmnSearch.WhereNdGroupBy = "where i.Status=1";// and LocationId =" + BranchId;
            dmnSearch.OrderBy = " ";
            var gridSource = _bllSearch.GetSearchData("prdSearch", dmnSearch);
            if (gridSource.Count > 0)
            {
                dgvProduct.Enabled = false;
                dgvItemPop.Rows.Add(gridSource.Count);
                for (int i = 0; i < gridSource.Count; i++)
                {
                    dgvItemPop.Rows[i].Cells[pslno].Value = i + 1;
                    dgvItemPop.Rows[i].Cells[pnameno].Value = gridSource[i].Field1;
                    dgvItemPop.Rows[i].Cells[prateno].Value = gridSource[i].Field2;
                    dgvItemPop.Rows[i].Cells[pqohno].Value = gridSource[i].Field3;
                    dgvItemPop.Rows[i].Cells[pitemidno].Value = gridSource[i].Field4;
                    dgvItemPop.Rows[i].Cells[pcategorytypeno].Value = gridSource[i].Field5;
                }
            }
            panelProductPopup.Show();
            pnlCustomer.Hide();
            dgvItemPop.Focus();
        }
        private void dgvProduct_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int r = dgvProduct.CurrentCell.ColumnIndex;

            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            TextBox tb = e.Control as TextBox;
            if (tb != null)
            {
                tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
            }
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
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

        private void dgvProduct_Leave(object sender, EventArgs e)
        {
            grandTotal();
        }

        private void dgvProduct_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvProduct.Rows[e.RowIndex].Cells[iitemid].Value != "" && dgvProduct.Rows[e.RowIndex].Cells[iitemid].Value != null && CellRow == true)
            {
                DataGridViewTextBoxCell cell = dgvProduct[israte, e.RowIndex] as DataGridViewTextBoxCell;
                if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[iprate].Value) > Convert.ToDecimal(cell.EditedFormattedValue.ToString()))
                {
                    MessageBox.Show("Sale Rate Mustbe Greater Than Purchase Rate", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                    return;
                }
                cell = dgvProduct[iprate, e.RowIndex] as DataGridViewTextBoxCell;
                if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(cell.EditedFormattedValue) <= 0)
                {
                    MessageBox.Show("Purchase Rate Is A Mandatory Field.", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                    return;
                }

                cell = dgvProduct[imrp, e.RowIndex] as DataGridViewTextBoxCell;
                if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[israte].Value) > Convert.ToDecimal(cell.EditedFormattedValue.ToString()))
                {
                    MessageBox.Show("MRP Mustbe Greater Than Sale Rate", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                    return;
                }
                cell = dgvProduct[idisper, e.RowIndex] as DataGridViewTextBoxCell;
                if (cell.EditedFormattedValue.ToString() != "" && cell.EditedFormattedValue.ToString() != string.Empty && cell.EditedFormattedValue.ToString() != ".")
                {
                    if (Convert.ToDecimal(cell.EditedFormattedValue.ToString()) > 100)
                    {
                        MessageBox.Show("Discount % Must Be Less Than 100", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    cell.Value = "0";
                }
                cell = dgvProduct[idisamt, e.RowIndex] as DataGridViewTextBoxCell;
                if (cell.EditedFormattedValue.ToString() != "" || cell.EditedFormattedValue.ToString() != string.Empty)
                {
                    if (Convert.ToDecimal(cell.EditedFormattedValue.ToString()) > Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[iamount].Value))
                    {
                        MessageBox.Show("Discount Amount Must Be Less Than Amount", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    cell.Value = "0";
                }
                cell = dgvProduct[iqty, e.RowIndex] as DataGridViewTextBoxCell;
                if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(cell.EditedFormattedValue.ToString()) <= 0)
                {
                    MessageBox.Show("Quantity Must Be Grater Than Zero", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                    return;
                }
            }
            CellRow = true;
        }    

        private void oaamount_KeyPress(object sender, KeyPressEventArgs e)
        {
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

        private void oaamount_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(oaamount.Text, out value))
            {
                oaamount.Text = "0";
            }
        }

        private void odamt_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(odamt.Text, out value))
            {
                odamt.Text = "0";
            }
        }

        private void btn_close_popupgrid_Click(object sender, EventArgs e)
        {
            panelProductPopup.Visible = false;
        }

        private void dgvItemPop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillDetails();
        }
        public void FillDetails()
        {
            try
            {
                int i = 0;
                int r = dgvItemPop.CurrentCell.RowIndex;
                int r1 = dgvProduct.CurrentCell.RowIndex;
                BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                dmnSearch.Select = "top(1) i.*,u.UnitName,pr.Id as PrId";
                dmnSearch.Table = "tblItem i LEFT JOIN tblUnit u on u.UId=i.unit left join tblProductRate pr on pr.ProductId=i.ItemId";
                dmnSearch.WhereNdGroupBy = " Where i.status=1 and i.ItemId = " + dgvItemPop.Rows[r].Cells[pitemidno].Value;
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    bool flage = false;
                    for (i = 0; i <= dgvProduct.RowCount - 1; i++)
                    {

                        if (Convert.ToInt32(gridSource[0].ItemId) == Convert.ToInt32(dgvProduct.Rows[i].Cells[iitemid].Value))
                        {
                            flage = true;
                            dgvProduct.Rows[i].Cells[iqty].Value = Convert.ToInt32(dgvProduct.Rows[i].Cells[iqty].Value) + 1;
                            if (r1 != i)
                            {
                                dgvProduct.Rows[r1].Cells[ibarcode].Value = "";
                                totalcellamount(i);
                            }
                            break;
                        }
                    }

                    if (flage == false)
                    {
                        if (r1 == dgvProduct.RowCount - 1)
                        {
                            dgvProduct.Rows.Add(1);
                        }

                        dgvProduct.Rows[r1].Cells[islno].Value = r1 + 1;
                        dgvProduct.Rows[r1].Cells[ibarcode].Value = gridSource[0].ItemCode_UPC;
                        dgvProduct.Rows[r1].Cells[ibarcode2].Value = gridSource[0].ItemCode_UPC;
                        dgvProduct.Rows[r1].Cells[iname].Value = gridSource[0].Itemname;
                        dgvProduct.Rows[r1].Cells[iunit].Value = gridSource[0].UnitName;
                        dgvProduct.Rows[r1].Cells[iunitid].Value = gridSource[0].unit;
                        dgvProduct.Rows[r1].Cells[iqty].Value = "1";
                        dgvProduct.Rows[r1].Cells[iprate].Value = gridSource[0].Prate;
                        dgvProduct.Rows[r1].Cells[israte].Value = gridSource[0].Srate;
                        dgvProduct.Rows[r1].Cells[imrp].Value = gridSource[0].MRP;
                        dgvProduct.Rows[r1].Cells[idisper].Value = "0";
                        dgvProduct.Rows[r1].Cells[idisamt].Value = "0";
                        if (gridSource[0].TaxType == "GST")
                        {
                            dgvProduct.Rows[r1].Cells[itaxper].Value = gridSource[0].IgstPrcnt;
                        }
                        if (gridSource[0].TaxType == "VAT")
                        {
                            dgvProduct.Rows[r1].Cells[itaxper].Value = gridSource[0].VatPrcnt;
                        }
                        dgvProduct.Rows[r1].Cells[iitemid].Value = gridSource[0].ItemId;
                        dgvProduct.Rows[r1].Cells[itax].Value = gridSource[0].Tax;
                        dgvProduct.Rows[r1].Cells[itaxtype].Value = gridSource[0].TaxType;
                        dgvProduct.Rows[r1].Cells[itype].Value = gridSource[0].Fcid;
                        dgvProduct.Rows[r1].Cells[itaxsettingid].Value = gridSource[0].TaxSettingsId;
                        dgvProduct.Rows[r1].Cells[ipridno].Value = gridSource[0].PrId;
                        panelProductPopup.Visible = false;
                        totalcellamount(r1);
                    }
                    panelProductPopup.Visible = false;
                    dgvProduct.Enabled = true;

                    dgvProduct.Focus();
                    dgvProduct.CurrentCell = dgvProduct.Rows[r1].Cells[iqty];
                }
            }
            catch(Exception ex)
            {

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

        private void totaldiscountamt_Leave(object sender, EventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(totaldiscountamt.Text, out value))
            {
                totaldiscountamt.Text = "0";

            }
            discountPer();
            netAmount();
        }

        private void oaamount_TextChanged(object sender, EventArgs e)
        {
            netAmount();
        }

        private void odamt_TextChanged(object sender, EventArgs e)
        {
            netAmount();
        }

        private void rcvamt_TextChanged(object sender, EventArgs e)
        {
            netAmount();
        }

        private void rcvamt_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(rcvamt.Text, out value))
            {
                rcvamt.Text = "0";
            }
        }

        private void discountper_KeyPress(object sender, KeyPressEventArgs e)
        {
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

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                txtbarcode.Text = "";
                pnl_barcode.Visible = true;
                pnl_barcode.Location = new Point(this.ClientSize.Width / 2 - pnl_barcode.Size.Width / 2, (this.ClientSize.Height / 2 - pnl_barcode.Size.Height / 2));
                txtbarcode.Focus();
            }
            else
            {
                txtbarcode.Text = "";
                pnl_barcode.Visible = false;
            }
        }

        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pnl_barcode.Visible = false;
                dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[1].Value = txtbarcode.Text;

                int rowno = dgvProduct.CurrentCell.RowIndex;
                UpcScanner(rowno);
            }
        }
        public void UpcScanner(int rowno)
        {
            panelProductPopup.Hide();
            dgvItemPop.Rows.Clear();
            int cno = dgvProduct.CurrentCell.ColumnIndex;
            dmnSearch.Select = "i.Itemname as Field1,i.price as Field2,i.quantity as Field3,i.ItemId as Field4,c.CategoryName as Field5";
            dmnSearch.Table = "tblItem i LEFT JOIN tblCategory_FTI c on c.Fcid = i.Fcid";
            dmnSearch.WhereNdGroupBy = "where i.Status=1 and i.ItemCode_UPC = '" + dgvProduct.Rows[rowno].Cells[ibarcode].Value + "'";
            dmnSearch.OrderBy = " ";
            var gridSourceitem = _bllSearch.GetSearchData("prdSearch", dmnSearch);
            if (gridSourceitem.Count > 1)
            {
                dgvProduct.Enabled = false;
                dgvItemPop.Rows.Add(gridSourceitem.Count);
                for (int i = 0; i < gridSourceitem.Count; i++)
                {
                    dgvItemPop.Rows[i].Cells[pslno].Value = i + 1;
                    dgvItemPop.Rows[i].Cells[pnameno].Value = gridSourceitem[i].Field1;
                    dgvItemPop.Rows[i].Cells[prateno].Value = gridSourceitem[i].Field2;
                    dgvItemPop.Rows[i].Cells[pqohno].Value = gridSourceitem[i].Field3;
                    dgvItemPop.Rows[i].Cells[pitemidno].Value = gridSourceitem[i].Field4;
                    dgvItemPop.Rows[i].Cells[pcategorytypeno].Value = gridSourceitem[i].Field5;
                }
                panelProductPopup.Show();
                pnlCustomer.Hide();
                dgvItemPop.Focus();
            } 
            else if (gridSourceitem.Count == 1)
            {
                int r1 = rowno;
                BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                dmnSearch.Select = "i.*,u.UnitName";
                dmnSearch.Table = "tblItem i LEFT JOIN tblUnit u on u.UId=i.unit";
                dmnSearch.WhereNdGroupBy = " Where i.status=1 and i.ItemCode_UPC = '" + dgvProduct.Rows[rowno].Cells[ibarcode].Value + "'";
                dmnSearch.OrderBy = "";
                var gridsource_multi = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridsource_multi.Count > 0)
                {
                    bool flage = false;
                    for (int i = 0; i <= dgvProduct.RowCount - 1; i++)
                    {

                        if (Convert.ToInt32(gridsource_multi[0].ItemId) == Convert.ToInt32(dgvProduct.Rows[i].Cells[iitemid].Value))
                        {
                            flage = true;
                            dgvProduct.Rows[i].Cells[iqty].Value = Convert.ToInt32(dgvProduct.Rows[i].Cells[iqty].Value) + 1;
                            if (r1 != i)
                            {
                                dgvProduct.Rows[r1].Cells[ibarcode].Value = "";
                                totalcellamount(i);
                            }
                            break;
                        }
                    }

                    if (flage == false)
                    {
                        if (r1 == dgvProduct.RowCount - 1)
                        {
                            dgvProduct.Rows.Add(1);
                        }

                        dgvProduct.Rows[r1].Cells[islno].Value = r1 + 1;
                        dgvProduct.Rows[r1].Cells[ibarcode].Value = gridsource_multi[0].ItemCode_UPC;
                        dgvProduct.Rows[r1].Cells[ibarcode2].Value = gridsource_multi[0].ItemCode_UPC;
                        dgvProduct.Rows[r1].Cells[iname].Value = gridsource_multi[0].Itemname;
                        dgvProduct.Rows[r1].Cells[iunit].Value = gridsource_multi[0].UnitName;
                        dgvProduct.Rows[r1].Cells[iunitid].Value = gridsource_multi[0].unit;
                        dgvProduct.Rows[r1].Cells[iqty].Value = "1";
                        dgvProduct.Rows[r1].Cells[iprate].Value = gridsource_multi[0].Prate;
                        dgvProduct.Rows[r1].Cells[israte].Value = gridsource_multi[0].Srate;
                        dgvProduct.Rows[r1].Cells[imrp].Value = gridsource_multi[0].MRP;
                        dgvProduct.Rows[r1].Cells[idisper].Value = "0";
                        dgvProduct.Rows[r1].Cells[idisamt].Value = "0";
                        if (gridsource_multi[0].TaxType == "GST")
                        {
                            dgvProduct.Rows[r1].Cells[itaxper].Value = gridsource_multi[0].IgstPrcnt;
                        }
                        if (gridsource_multi[0].TaxType == "VAT")
                        {
                            dgvProduct.Rows[r1].Cells[itaxper].Value = gridsource_multi[0].VatPrcnt;
                        }
                        dgvProduct.Rows[r1].Cells[iitemid].Value = gridsource_multi[0].ItemId;
                        dgvProduct.Rows[r1].Cells[itax].Value = gridsource_multi[0].Tax;
                        dgvProduct.Rows[r1].Cells[itaxtype].Value = gridsource_multi[0].TaxType;
                        dgvProduct.Rows[r1].Cells[itype].Value = gridsource_multi[0].Fcid;
                        dgvProduct.Rows[r1].Cells[itaxsettingid].Value = gridsource_multi[0].TaxSettingsId;
                        dgvProduct.Rows[r1].Cells[ipridno].Value = gridsource_multi[0].PrId;
                        panelProductPopup.Visible = false;
                        totalcellamount(r1);
                    }
                    panelProductPopup.Visible = false;
                    dgvProduct.Enabled = true;

                    dgvProduct.Focus();
                    dgvProduct.CurrentCell = dgvProduct.Rows[r1].Cells[iqty];
                }

            }
            else
            {
                MessageBox.Show("Invalid Product", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //dgvProduct.Rows.RemoveAt(dgvProduct.CurrentCell.RowIndex);
                if (dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[iitemid].Value != "")
                {
                    dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[ibarcode].Value = dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[ibarcode2].Value;
                }
                else
                    dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[ibarcode].Value = "";

            }

        }
    }
}
