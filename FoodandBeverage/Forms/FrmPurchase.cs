using BLL;
using Domain;
using FoodandBeverage.Forms.UserControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CustomMessageBox;
using System.Drawing.Drawing2D;
using FoodandBeverage.Interfaces;
using FoodandBeverage.Classes;

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
            int panelWidthPercentage = 70;
            int panelheightPercentage = 35;
            int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
            int panelHeight = (int)(this.Height * panelheightPercentage / 100);
            pnlCustomer.Size = new Size(panelWidth, panelHeight);
            pnlCustomer.Location = new Point(this.ClientSize.Width / 2 - pnlCustomer.Size.Width / 2, this.ClientSize.Height / 2 - pnlCustomer.Size.Height / 2);
            txtVendorName.Focus();
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
        public void setperficpurchase()
        {
            try
            {
                BllSearch<DmnPurchaseMaster> bllSearch = new BllSearch<DmnPurchaseMaster>();
                DmnSearch objSearch = new DmnSearch();
                dmnSearch.Select = " PurchasePrefix,PurchaseSrlNo";
                dmnSearch.Table = " tblHotelRegistration";
                dmnSearch.WhereNdGroupBy = " Where BranchId = " + FrmDashboard.BranchId;
                dmnSearch.OrderBy = "";
                var gridSource1 = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource1.Count > 0)
                {
                    invoiceno.Text = gridSource1[0].PurchasePrefix + FrmDashboard.BranchId + DateTime.Now.Year.ToString() + gridSource1[0].PurchaseSrlNo;//PERRET20221001
                }
            }
            catch (Exception ex)
            { }

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
            txtdocno.Focus();
            setperficpurchase();
            btnsave.Visible = true;
            txt_exist_vendorid.Text = "";
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
            catch (Exception ex)
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
                    CMessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    CMessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            catch (Exception ex)
            {

            }

        }
        #endregion
        #region Vendor 
        public bool VendorMandatoryCheck()
        {
            if (string.IsNullOrEmpty(txtVendorName.Text))
            {
                CMessageBox.Show("Vendor Name Is Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtVendorAddress.Text))
            {
                CMessageBox.Show("Vendor Address Is Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtVendorTel.Text))
            {
                CMessageBox.Show("Vendor PhNo Is Mandatory ", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    DmnPurchaseMaster dmnPurchase = new DmnPurchaseMaster();
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
                    dmnPurchase.BranchId = FrmDashboard.BranchId;
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
                            dmnPurchase.BranchId = FrmDashboard.BranchId;
                            dmnPurchase.CreatedBy = FrmDashboard.UserId;
                            dmnPurchase.Status = 1;
                            dmnPurchase.PurchaseDetailId = Convert.ToInt32("0");
                            dmnPurchase.Amount = Convert.ToDecimal(dgvProduct.Rows[i].Cells[iamount].Value);
                            dmnPurchase.DisPer = Convert.ToDecimal(dgvProduct.Rows[i].Cells[idisper].Value);
                            dmnPurchase.PrId = Convert.ToInt32(dgvProduct.Rows[i].Cells[ipridno].Value);

                            var InsertStatus = bllPurchase.AddEditDataDet("prdInsert_Upd_PurchaseDetails", dmnPurchase);
                            if (InsertStatus == null)
                            {
                                CUst = true;
                                CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }

                        }

                    }
                    if (CUst == false)
                    {
                        CMessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                CMessageBox.Show("Document Number Is Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtdocno.Focus();
                return false;
            }
            //else if (string.IsNullOrEmpty(invoiceno.Text))
            //{
            //    CMessageBox.Show("Entry Number Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    invoiceno.Focus();
            //    return false;
            //}
            else if (Convert.ToInt32(custname.SelectedValue) < 0)
            {
                CMessageBox.Show("Vendor Name Is Mandatory ", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                custname.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(address.Text))
            {
                CMessageBox.Show("Address Is Mandatory ", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                address.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(contactno.Text))
            {
                CMessageBox.Show("Contact Number Is Mandatory ", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                contactno.Focus();
                return false;
            }
            //else if (string.IsNullOrEmpty(txtEmail.Text))
            //{
            //    CMessageBox.Show("Email Is Mandatory And All Red Color Fileds Are Mandatory", "Mandaatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtEmail.Focus();
            //    return false;
            //}
            else if (string.IsNullOrEmpty(netamount.Text) || netamount.Text.ToString() == "0")
            {
                CMessageBox.Show("Netamount Is Not Zero", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return false;
            }
            //else if (dgvProduct.Rows[0].Cells[iitemid].Value == null)
            //{
            //    CMessageBox.Show("No product Found !", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            //        CMessageBox.Show("Product Is Not Selected", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return false;
            //        break;
            //    }

            //}
            return true;
        }

        private void btn_grid_regitartion_Click(object sender, EventArgs e)
        {
            try
            {
                tlp_grid_registartion.Visible = true;
                tlp_registartion_main.Visible = false;
                Control controlToRemove = tlp_main.GetControlFromPosition(0, 1);
                tlp_main.Controls.Remove(controlToRemove);
                tlp_main.Controls.Add(tlp_grid_registartion, 1, 0);
                tlp_grid_registartion.Dock = DockStyle.Fill;
                tlp_registartion_main.Dock = DockStyle.Fill;

                tlp_textboxes.Visible = true;
                controlToRemove = tlp_regform_downtlp.GetControlFromPosition(0, 3);
                tlp_regform_downtlp.Controls.Remove(controlToRemove);
                tlp_grid_registartion.Controls.Add(tlp_textboxes, 0, 3);
                tlp_textboxes.Dock = DockStyle.Fill;
                dgvProduct.Focus();
                dgvProduct.CurrentCell = dgvProduct[0, 0];
            }
            catch (Exception ex)
            {

            }

        }

        private void btn_back_registration_Click(object sender, EventArgs e)
        {
            try
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
                pnl_barcode.Visible = false;
                txtbarcode.Text = "";
                if (string.IsNullOrEmpty(txt_exist_vendorid.Text))
                    txt_exist_vendorid.Text = "0";
                if (Convert.ToInt64(txt_exist_vendorid.Text) > 0)
                    custname.SelectedValue = Convert.ToInt64(txt_exist_vendorid.Text);
            }
            catch (Exception ex)
            {

            }

        }
        private void Btn_view_menu_Click(object sender, EventArgs e)
        {

        }

        private void custname_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch_CountryCategory = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Address as Field1,Telephone as Field2,Email as Field3,VendorId Field4";
                objSearch.Table = " tblVendor";
                objSearch.WhereNdGroupBy = "where isnull(status,1)=1 and VendorId = " + custname.SelectedValue;
                objSearch.OrderBy = "";
                var gridSource = bllSearch_CountryCategory.GetSearchData("prdsearch", objSearch);
                if (gridSource.Count > 0)
                {
                    txt_exist_vendorid.Text = Convert.ToString(gridSource[0].Field4);
                    address.Text = gridSource[0].Field1;
                    contactno.Text = Convert.ToString(gridSource[0].Field2);
                    txtEmail.Text = gridSource[0].Field3;
                }
            }
            catch (Exception ex)
            {

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
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                {
                    FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                    if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.Purchase)))
                        btnsave.Enabled = true;
                    else
                        btnsave.Enabled = false;
                }
                txtdocno.Focus();
                ClearAll();
                FillCombobox();
                tlp_textboxes.Visible = true;
                Control controlToRemove = tlp_grid_registartion.GetControlFromPosition(0, 3);
                tlp_grid_registartion.Controls.Remove(controlToRemove);
                tlp_regform_downtlp.Controls.Add(tlp_textboxes, 0, 2);
                tlp_textboxes.Dock = DockStyle.Fill;

                dgvProduct.BorderStyle = BorderStyle.None;
                dgvProduct.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvProduct.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvProduct.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgvProduct.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvProduct.BackgroundColor = Color.White;

                dgvProduct.EnableHeadersVisualStyles = false;
                dgvProduct.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvProduct.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvProduct.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;




                dgvItemPop.BorderStyle = BorderStyle.None;
                dgvItemPop.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvItemPop.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvItemPop.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgvItemPop.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvItemPop.BackgroundColor = Color.White;

                dgvItemPop.EnableHeadersVisualStyles = false;
                dgvItemPop.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvItemPop.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvItemPop.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                setperficpurchase();
            }
            catch (Exception ex)
            {

            }


        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { invoiceno, txtdocno, address, contactno, txtEmail, txtVendorAddress, txtVendorEmail, txtVendorTel, txtVendorName, odamt, oddesc, oaamount, oadesc, discountper, totaldiscountamt, sterms, remarks, netamount, rcvamt, txtbarcode, txtdes })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                    textBox.BackColor = Color.FromArgb(196, 223, 223);
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
                            CMessageBox.Show("Sale Rate Mustbe Greater Than Purchase Rate", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //SendKeys.Send("{UP}");
                            e.Cancel = true;
                        }
                    }
                    else if (e.ColumnIndex == iprate)
                    {
                        DataGridViewTextBoxCell cell = dgvProduct[iprate, e.RowIndex] as DataGridViewTextBoxCell;
                        if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == "." || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(cell.EditedFormattedValue) <= 0)
                        {
                            CMessageBox.Show("Purchase Rate Is A Mandatory Field", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            e.Cancel = true;
                        }
                    }
                    else if (e.ColumnIndex == imrp)
                    {
                        DataGridViewTextBoxCell cell = dgvProduct[imrp, e.RowIndex] as DataGridViewTextBoxCell;
                        if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == "." || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[israte].Value) > Convert.ToDecimal(cell.EditedFormattedValue.ToString()))
                        {
                            CMessageBox.Show("MRP Mustbe Greater Than Sale Rate", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            // SendKeys.Send("{UP}");
                            e.Cancel = true;
                        }
                    }
                    else if (e.ColumnIndex == idisper)
                    {
                        DataGridViewTextBoxCell cell = dgvProduct[idisper, e.RowIndex] as DataGridViewTextBoxCell;
                        if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == "." || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(cell.EditedFormattedValue.ToString()) > 100)
                        {
                            CMessageBox.Show("Discount % Must Be Less Than 100", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //SendKeys.Send("{UP}");
                            e.Cancel = true;
                        }
                    }
                    else if (e.ColumnIndex == idisamt)
                    {
                        DataGridViewTextBoxCell cell = dgvProduct[idisamt, e.RowIndex] as DataGridViewTextBoxCell;
                        if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == "." || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(cell.EditedFormattedValue.ToString()) > Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[iamount].Value))
                        {
                            CMessageBox.Show("Discount Amount Must Be Less Than Amount", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                            CMessageBox.Show("Quantity Must Be Grater Than Or Equal To One", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            e.Cancel = true;
                        }
                        //else if (!int.TryParse(Convert.ToString(cell.EditedFormattedValue), out value))
                        //{
                        //    //dgvProduct.Rows[e.RowIndex].Cells[iqty].Value = decimal.ToInt32(Convert.ToDecimal(cell.EditedFormattedValue));
                        //    dgvProduct.EditingControl.Text = Convert.ToString(decimal.ToInt32(Convert.ToDecimal(cell.EditedFormattedValue)));
                        //    //CMessageBox.Show("Decimal Point Is Not Allowed In Quantity", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //    //e.Cancel = true;
                        //    return;
                        //}
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
                        CMessageBox.Show("Sale Rate Mustbe Greater Than Purchase Rate", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        CMessageBox.Show("MRP Mustbe Greater Than Sale Rate", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            try
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
                    if (dgvProduct.Rows.Count == 0)
                        dgvProduct.Rows.Add();
                    else
                    {
                        dgvProduct.Rows.RemoveAt(dgvProduct.CurrentCell.RowIndex);
                        if (dgvProduct.Rows.Count == 0)
                            dgvProduct.Rows.Add();
                    }
                    if(dgvProduct.RowCount >0)
                    {
                        for (int i = 0; i < dgvProduct.RowCount; i++)
                        {
                            dgvProduct.Rows[i].Cells[islno].Value = i + 1;
                        }
                    }


                }
            }
            catch(Exception ex)
            {

            }
    
        }
        public void purchasepop()
        {
            panelProductPopup.Location = new Point(
                              this.ClientSize.Width / 2 - panelProductPopup.Size.Width / 2,
                              (this.ClientSize.Height / 2 - panelProductPopup.Size.Height / 2));
            panelProductPopup.Anchor = AnchorStyles.None;
            panelProductPopup.Show();
            dgvItemPop.Rows.Clear();
            dmnSearch.Select = "i.Itemname as Field1,(Select Top(1)Prate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field2,(Select Sum(QOH) from tblProductRate where ProductId=i.ItemId) as Field3,i.ItemId as Field4,c.CategoryName as Field5";
            dmnSearch.Table = "tblItem i LEFT JOIN tblCategory_FTI c on c.Fcid = i.Fcid";
            dmnSearch.WhereNdGroupBy = "where i.Status=1";// and LocationId =" + BranchId;
            dmnSearch.OrderBy = "order by c.CategoryName ";
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
                    CMessageBox.Show("Sale Rate Mustbe Greater Than Purchase Rate", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                    return;
                }
                cell = dgvProduct[iprate, e.RowIndex] as DataGridViewTextBoxCell;
                if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(cell.EditedFormattedValue) <= 0)
                {
                    CMessageBox.Show("Purchase Rate Is A Mandatory Field.", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                    return;
                }

                cell = dgvProduct[imrp, e.RowIndex] as DataGridViewTextBoxCell;
                if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[israte].Value) > Convert.ToDecimal(cell.EditedFormattedValue.ToString()))
                {
                    CMessageBox.Show("MRP Mustbe Greater Than Sale Rate", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                    return;
                }
                cell = dgvProduct[idisper, e.RowIndex] as DataGridViewTextBoxCell;
                if (cell.EditedFormattedValue.ToString() != "" && cell.EditedFormattedValue.ToString() != string.Empty && cell.EditedFormattedValue.ToString() != ".")
                {
                    if (Convert.ToDecimal(cell.EditedFormattedValue.ToString()) > 100)
                    {
                        CMessageBox.Show("Discount % Must Be Less Than 100", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        CMessageBox.Show("Discount Amount Must Be Less Than Amount", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    CMessageBox.Show("Quantity Must Be Grater Than Zero", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                    return;
                }
            }
            CellRow = true;
        }

        private void oaamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
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



        private void dgvItemPop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillDetails();
        }
        public void FillDetails()
        {
            try
            {
                int r = dgvItemPop.CurrentCell.RowIndex;
                int r1 = dgvProduct.CurrentCell.RowIndex;
                dmnSearch.Select = "top (1) i.ItemCode_UPC Field1,i.Itemname Field2,u.UnitName Field3," +
    "(Select Top(1)Prate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field4," +
    "(Select Top(1)Srate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field5," +
    "(Select Top(1)MRP from tblProductRate where ProductId=i.ItemId order by Id desc) as Field6," +
    "ts.IgstPrcnt Field7,ts.VatPrcnt Field8,i.ItemId Field9,i.Tax Field10,i.TaxType Field11,i.Fcid Field12,i.TaxSettingsId Field13," +
    "(Select Top(1)id from tblProductRate where ProductId=i.ItemId order by Id desc) as Field14,u.uid Field15";
                dmnSearch.Table = "tblItem i " +
                    "LEFT JOIN tblUnit u on u.UId=i.unit " +
                    "LEFT JOIN tblProductRate pr on pr.ProductId=i.ItemId " +
                    "LEFT JOIN tblTaxSettings ts on ts.TaxSettingsId=i.TaxSettingsId";
                dmnSearch.WhereNdGroupBy = "where i.Status=1 and i.itemid = '" + dgvItemPop.Rows[r].Cells[pitemidno].Value + "'";
                dmnSearch.OrderBy = "order by pr.Id desc ";
                var gridSourceitem = _bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSourceitem.Count == 1)
                {
                    bool flage = false;
                    for (int i = 0; i <= dgvProduct.RowCount - 1; i++)
                    {

                        if (Convert.ToInt32(gridSourceitem[0].Field9) == Convert.ToInt32(dgvProduct.Rows[i].Cells[iitemid].Value))
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
                        dgvProduct.Rows[r1].Cells[iname].Style.ForeColor = Color.Black;
                        dgvProduct.Rows[r1].Cells[islno].Value = r1 + 1;
                        dgvProduct.Rows[r1].Cells[ibarcode].Value = gridSourceitem[0].Field1;
                        dgvProduct.Rows[r1].Cells[ibarcode2].Value = gridSourceitem[0].Field1;
                        dgvProduct.Rows[r1].Cells[iname].Value = gridSourceitem[0].Field2;
                        dgvProduct.Rows[r1].Cells[iunit].Value = gridSourceitem[0].Field3;
                        dgvProduct.Rows[r1].Cells[iunitid].Value = gridSourceitem[0].Field15;
                        dgvProduct.Rows[r1].Cells[iqty].Value = "1";
                        dgvProduct.Rows[r1].Cells[iprate].Value = gridSourceitem[0].Field4;
                        dgvProduct.Rows[r1].Cells[israte].Value = gridSourceitem[0].Field5;
                        dgvProduct.Rows[r1].Cells[imrp].Value = gridSourceitem[0].Field6;
                        dgvProduct.Rows[r1].Cells[idisper].Value = "0";
                        dgvProduct.Rows[r1].Cells[idisamt].Value = "0";
                        if (gridSourceitem[0].Field11 == "GST")
                        {
                            dgvProduct.Rows[r1].Cells[itaxper].Value = gridSourceitem[0].Field7;
                        }
                        if (gridSourceitem[0].Field11 == "VAT")
                        {
                            dgvProduct.Rows[r1].Cells[itaxper].Value = gridSourceitem[0].Field8;
                        }
                        dgvProduct.Rows[r1].Cells[iitemid].Value = gridSourceitem[0].Field9;
                        dgvProduct.Rows[r1].Cells[itax].Value = gridSourceitem[0].Field10;
                        dgvProduct.Rows[r1].Cells[itaxtype].Value = gridSourceitem[0].Field11;
                        dgvProduct.Rows[r1].Cells[itype].Value = gridSourceitem[0].Field12;
                        dgvProduct.Rows[r1].Cells[itaxsettingid].Value = gridSourceitem[0].Field13;
                        dgvProduct.Rows[r1].Cells[ipridno].Value = gridSourceitem[0].Field14;
                        panelProductPopup.Visible = false;
                        totalcellamount(r1);
                    }

                    panelProductPopup.Visible = false;
                    dgvProduct.Enabled = true;

                    dgvProduct.Focus();
                    dgvProduct.CurrentCell = dgvProduct.Rows[r1].Cells[iqty];


                }
                
            }
            catch (Exception ex)
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
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
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
            if (e.ColumnIndex == ibarcode)
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

            //if (e.KeyCode == Keys.Enter)
            //{
            //    pnl_barcode.Visible = false;
            //    dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[1].Value = txtbarcode.Text;
            //    int rowno = dgvProduct.CurrentCell.RowIndex;
            //    UpcScanner(rowno);
            //}
        }
        public void UpcScanner(int rowno)
        {
            panelProductPopup.Hide();
            dgvItemPop.Rows.Clear();
            int cno = dgvProduct.CurrentCell.ColumnIndex;
            dmnSearch.Select = "top (1) i.ItemCode_UPC Field1,i.Itemname Field2,u.UnitName Field3," +
                "(Select Top(1)Prate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field4," +
                "(Select Top(1)Srate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field5," +
                "(Select Top(1)MRP from tblProductRate where ProductId=i.ItemId order by Id desc) as Field6," +
                "ts.IgstPrcnt Field7,ts.VatPrcnt Field8,i.ItemId Field9,i.Tax Field10,i.TaxType Field11,i.Fcid Field12,i.TaxSettingsId Field13," +
                "(Select Top(1)id from tblProductRate where ProductId=i.ItemId order by Id desc) as Field14,u.uid Field15";
            dmnSearch.Table = "tblItem i " +
                "LEFT JOIN tblUnit u on u.UId=i.unit " +
                "LEFT JOIN tblProductRate pr on pr.ProductId=i.ItemId " +
                "LEFT JOIN tblTaxSettings ts on ts.TaxSettingsId=i.TaxSettingsId";
            dmnSearch.WhereNdGroupBy = "where i.Status=1 and i.ItemCode_UPC = '" + dgvProduct.Rows[rowno].Cells[ibarcode].Value + "'";
            dmnSearch.OrderBy = "order by pr.Id desc ";
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
                panelProductPopup.Location = new Point(this.ClientSize.Width / 2 - panelProductPopup.Size.Width / 2, (this.ClientSize.Height / 2 - panelProductPopup.Size.Height / 2));
                pnlCustomer.Hide();
                dgvItemPop.Focus();
            }

            else if (gridSourceitem.Count == 1)
            {
                int r1 = rowno;

                    bool flage = false;
                for (int i = 0; i <= dgvProduct.RowCount - 1; i++)
                {

                    if (Convert.ToInt32(gridSourceitem[0].Field9) == Convert.ToInt32(dgvProduct.Rows[i].Cells[iitemid].Value))
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
                    dgvProduct.Rows[r1].Cells[iname].Style.ForeColor = Color.Black;
                    dgvProduct.Rows[r1].Cells[islno].Value = r1 + 1;
                    dgvProduct.Rows[r1].Cells[ibarcode].Value = gridSourceitem[0].Field1;
                    dgvProduct.Rows[r1].Cells[ibarcode2].Value = gridSourceitem[0].Field1;
                    dgvProduct.Rows[r1].Cells[iname].Value = gridSourceitem[0].Field2;
                    dgvProduct.Rows[r1].Cells[iunit].Value = gridSourceitem[0].Field3;
                    dgvProduct.Rows[r1].Cells[iunitid].Value = gridSourceitem[0].Field15;
                    dgvProduct.Rows[r1].Cells[iqty].Value = "1";
                    dgvProduct.Rows[r1].Cells[iprate].Value = gridSourceitem[0].Field4;
                    dgvProduct.Rows[r1].Cells[israte].Value = gridSourceitem[0].Field5;
                    dgvProduct.Rows[r1].Cells[imrp].Value = gridSourceitem[0].Field6;
                    dgvProduct.Rows[r1].Cells[idisper].Value = "0";
                    dgvProduct.Rows[r1].Cells[idisamt].Value = "0";
                    if (gridSourceitem[0].Field11 == "GST")
                    {
                        dgvProduct.Rows[r1].Cells[itaxper].Value = gridSourceitem[0].Field7;
                    }
                    if (gridSourceitem[0].Field11 == "VAT")
                    {
                        dgvProduct.Rows[r1].Cells[itaxper].Value = gridSourceitem[0].Field8;
                    }
                    dgvProduct.Rows[r1].Cells[iitemid].Value = gridSourceitem[0].Field9;
                    dgvProduct.Rows[r1].Cells[itax].Value = gridSourceitem[0].Field10;
                    dgvProduct.Rows[r1].Cells[itaxtype].Value = gridSourceitem[0].Field11;
                    dgvProduct.Rows[r1].Cells[itype].Value = gridSourceitem[0].Field12;
                    dgvProduct.Rows[r1].Cells[itaxsettingid].Value = gridSourceitem[0].Field13;
                    dgvProduct.Rows[r1].Cells[ipridno].Value = gridSourceitem[0].Field14;
                    panelProductPopup.Visible = false;
                    totalcellamount(r1);
                }

                panelProductPopup.Visible = false;
                dgvProduct.Enabled = true;
                dgvProduct.Focus();
                //dgvProduct.CurrentCell = dgvProduct.Rows[r1].Cells[iqty];

                txtbarcode.Text = "";
                pnl_barcode.Visible = true;
                pnl_barcode.Location = new Point(this.ClientSize.Width / 2 - pnl_barcode.Size.Width / 2, (this.ClientSize.Height / 2 - pnl_barcode.Size.Height / 2));
                txtbarcode.Focus();


            }
            else
            {
                CMessageBox.Show("Invalid Product", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[iitemid].Value != "")
                {
                    dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[ibarcode].Value = dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[ibarcode2].Value;
                }
                else                
                    dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[ibarcode].Value = "";
                txtbarcode.Text = "";
                pnl_barcode.Visible = true;
                pnl_barcode.Location = new Point(this.ClientSize.Width / 2 - pnl_barcode.Size.Width / 2, (this.ClientSize.Height / 2 - pnl_barcode.Size.Height / 2));
                txtbarcode.Focus();
            }

        }



        private void btnSaveCust_Click(object sender, EventArgs e)
        {
            if (VendorMandatoryCheck())
            {
                DmnPurchaseMaster dmnPurchase = new DmnPurchaseMaster();
                BllPurchase bllPurchase = new BllPurchase();
                dmnPurchase.Customer = txtVendorName.Text;
                dmnPurchase.Address = txtVendorAddress.Text;
                dmnPurchase.Email = txtVendorEmail.Text;
                dmnPurchase.ContactNo = txtVendorTel.Text;
                dmnPurchase.Description = txtdes.Text;
                dmnPurchase.BranchId = FrmDashboard.BranchId;
                dmnPurchase.CreatedBy = FrmDashboard.UserId;
                dmnPurchase.Status = 1;
                dmnPurchase.CustomerId = Convert.ToInt32("0");
                var InsertStatus = bllPurchase.AddEditDataVendor("prdInsert_Upd_Vendor", dmnPurchase);
                if (InsertStatus == null)
                {
                    CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //break;
                }
                if (InsertStatus != null)
                {
                    CMessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtVendorName.Text = "";
                    txtVendorTel.Text = "";
                    txtVendorAddress.Text = "";
                    txtVendorEmail.Text = "";
                }
                pnlCustomer.Hide();
                FillCombobox();
                txtVendorName.Text = "";
                txtVendorEmail.Text = "";
                txtVendorAddress.Text = "";
                txtVendorTel.Text = "";
            }
        }

        private void btnCloseCst_Click(object sender, EventArgs e)
        {
            pnlCustomer.Visible = false;
            txtVendorName.Text = "";
            txtVendorAddress.Text = "";
            txtVendorEmail.Text = "";
            txtVendorTel.Text = "";
            btn_vendor_add.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "ID,BillNo,Date,DocumentNo,Telephone,TotalQuantity,NetAmount";
                _objSearch.Select = "PurchaseMasterId Id,EntryNo  as Field1,EntryDate as Field2,DocumentNo as Field3,Telephone as Field4,TotalQuantity as Field5,NetAmount as Field6";
                _objSearch.Table = "tblPurchaseMaster";
                _objSearch.WhereNdGroupBy = "where Status = 1 ";
                _objSearch.OrderBy = " ORDER BY PurchaseMasterId  desc";
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
                        BllSearch<DmnPurchaseMaster> bllSearch1 = new BllSearch<DmnPurchaseMaster>();
                        DmnSearch objSearch = new DmnSearch();
                        objSearch = new DmnSearch();
                        objSearch.Select = "PurchaseMasterId,EntryNo,EntryDate,DocumentNo,LedgerId,PurchaseAddress," +
                            " Telephone,Email,TotalQuantity,TotalDiscount,TotalAmount,DiscountPer,DiscountAmount,Remarks," +
                            " OaAmount OtherAdditions,OaDesc OtherAddionDesc,OdAmount OtherDeduction," +
                            "OdDesc OtherDeductionDesc,NetAmount,Terms SalesTerms,RcvAmnt PaidAmount,BalAmnt BalanceAmount,NetAmount";
                        objSearch.Table = "tblPurchaseMaster";
                        objSearch.WhereNdGroupBy = "Where PurchaseMasterId = " + returnValue;
                        objSearch.OrderBy = " ";
                        var gridSource1 = bllSearch1.GetSearchData("prdSearch", objSearch);
                        if (gridSource1.Count > 0)
                        {
                            invoiceno.Text = gridSource1[0].EntryNo;
                            txtdocno.Text = gridSource1[0].DocumentNo;
                            dtpdocdate.Value = gridSource1[0].EntryDate;
                            custname.SelectedValue = gridSource1[0].LedgerId;
                            address.Text = gridSource1[0].PurchaseAddress;
                            contactno.Text = gridSource1[0].Telephone;
                            txtEmail.Text = gridSource1[0].Email;
                            lblTotqty.Text = Convert.ToString(gridSource1[0].TotalQuantity);
                            lblTotal.Text = Convert.ToString(gridSource1[0].TotalAmount);
                            lblGrandTot.Text = Convert.ToString(gridSource1[0].TotalAmount);
                            lblTotDisc.Text = Convert.ToString(gridSource1[0].TotalDiscount);
                            discountper.Text = Convert.ToString(gridSource1[0].DiscountPer);
                            totaldiscountamt.Text = Convert.ToString(gridSource1[0].DiscountAmount);
                            oaamount.Text = Convert.ToString(gridSource1[0].OtherAdditions);
                            oadesc.Text = Convert.ToString(gridSource1[0].OtherAddionDesc);
                            odamt.Text = Convert.ToString(gridSource1[0].OtherDeduction);
                            oddesc.Text = gridSource1[0].OtherDeductionDesc;
                            netamount.Text = Convert.ToString(gridSource1[0].NetAmount);
                            lblNetAmt.Text = Convert.ToString(gridSource1[0].NetAmount);
                            rcvamt.Text = Convert.ToString(gridSource1[0].PaidAmount);
                            lblBalAmt.Text = Convert.ToString(gridSource1[0].BalanceAmount);
                            remarks.Text = gridSource1[0].Remarks;
                            sterms.Text = gridSource1[0].SalesTerms;
                            BllSearch<DmnPurchaseDetails> bllSearch2 = new BllSearch<DmnPurchaseDetails>();
                            objSearch = new DmnSearch();
                            objSearch.Select = "pd.*,itemName ItemCode,u.UnitName";
                            objSearch.Table = " tblPurchaseDetails pd LEFT JOIN tblUnit u on u.UId=pd.UnitId";
                            objSearch.WhereNdGroupBy = "Where pd.PurchaseMasterId = " + gridSource1[0].PurchaseMasterId;
                            objSearch.OrderBy = " ";
                            var gridSource2 = bllSearch2.GetSearchData("prdSearch", objSearch);
                            dgvProduct.Rows.Clear();
                            dgvProduct.Rows.Add(gridSource2.Count);
                            for (int i = 0; i < gridSource2.Count; i++)
                            {
                                dgvProduct.Rows[i].Cells[islno].Value = i + 1;
                                dgvProduct.Rows[i].Cells[ibarcode].Value = gridSource2[i].ItemBarCode;
                                dgvProduct.Rows[i].Cells[iname].Value = gridSource2[i].ItemCode;
                                dgvProduct.Rows[i].Cells[iunit].Value = gridSource2[i].UnitName;
                                dgvProduct.Rows[i].Cells[iqty].Value = gridSource2[i].Quantity;
                                dgvProduct.Rows[i].Cells[iprate].Value = gridSource2[i].PurchaseRate;
                                dgvProduct.Rows[i].Cells[iamount].Value = gridSource2[i].Amount;
                                dgvProduct.Rows[i].Cells[idisper].Value = gridSource2[i].DiscountPer;
                                dgvProduct.Rows[i].Cells[idisamt].Value = gridSource2[i].DiscountAmount;
                                dgvProduct.Rows[i].Cells[itaxper].Value = gridSource2[i].TaxPer;
                                dgvProduct.Rows[i].Cells[itaxamt].Value = gridSource2[i].TaxAmount;
                                dgvProduct.Rows[i].Cells[itotal].Value = gridSource2[i].NetAmount;
                                dgvProduct.Rows[i].Cells[itaxtype].Value = gridSource2[i].TaxType;
                                dgvProduct.Rows[i].Cells[itax].Value = gridSource2[i].Tax;
                                dgvProduct.Rows[i].Cells[israte].Value = gridSource2[i].Srate;
                                dgvProduct.Rows[i].Cells[imrp].Value = gridSource2[i].MRP;

                            }
                            btnsave.Visible = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void txtVendorTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void dgvProduct_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == iname)
            {
                if (dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[iitemid].Value == null)
                {
                    dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[iname].Value = "";
                }
            }
        }

        private void dgvProduct_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == iname)
            {
                if (dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[iitemid].Value == null)
                {
                    dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[iname].Value = "F8 for Products";
                    dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[iname].Style.ForeColor = Color.Red;
                }
                else
                    dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[iname].Style.ForeColor = Color.Black;

            }
        }

        private void txtVendorName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
        }

        private void txtVendorEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
        }

        private void contactno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
        }

        private void txtdocno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
        }

        #region Grid Flow Fuction
        private int GetNextVisibleColumn(int currentColumnIndex)
        {
            try
            {
          for (int i = currentColumnIndex + 1; i < dgvProduct.ColumnCount; i++)
            {
                if (dgvProduct.Columns[i].Visible)
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
             for (int i = currentRowIndex + 1; i < dgvProduct.RowCount; i++)
            {
                if (dgvProduct.Rows[i].Visible)
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
                for (int i = 0; i < dgvProduct.ColumnCount; i++)
                {
                    if (dgvProduct.Columns[i].Visible)
                    {
                        return i;
                    }
                }
                return -1;
            }
            catch(Exception ex)
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
                    if (dgvProduct.Focused)
                    {
                        try
                        {
                            if (dgvProduct.Focused)
                            {
                                int currentRowIndex = dgvProduct.CurrentCell.RowIndex;
                                int currentColumnIndex = dgvProduct.CurrentCell.ColumnIndex;

                                int nextColumnIndex = GetNextVisibleColumn(currentColumnIndex);

                                if (nextColumnIndex != -1)
                                {
                                    dgvProduct.CurrentCell = dgvProduct[nextColumnIndex, currentRowIndex];
                                }
                                else
                                {
                                    int nextRowIndex = GetNextVisibleRow(currentRowIndex);
                                    if (nextRowIndex != -1)
                                    {
                                        dgvProduct.CurrentCell = dgvProduct[GetFirstVisibleColumn(), nextRowIndex];
                                    }
                                    else
                                    {
                                        discountper.Focus();
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
                    
                   if (dgvProduct.Focused)
                    {
                        try
                        {
                            if (dgvProduct.Focused)
                            {
                                int currentRowIndex = dgvProduct.CurrentCell.RowIndex;
                                int currentColumnIndex = dgvProduct.CurrentCell.ColumnIndex;

                                int nextColumnIndex = GetNextVisibleColumn(currentColumnIndex);

                                if (nextColumnIndex != -1)
                                {
                                    dgvProduct.CurrentCell = dgvProduct[nextColumnIndex, currentRowIndex];
                                }
                                else
                                {
                                    int nextRowIndex = GetNextVisibleRow(currentRowIndex);
                                    if (nextRowIndex != -1)
                                    {
                                        dgvProduct.CurrentCell = dgvProduct[GetFirstVisibleColumn(), nextRowIndex];
                                    }
                                    else
                                    {
                                        discountper.Focus();
                                    }
                                }

                            }
                        }
                        
                        catch(Exception ex)
                        {
            
                        }                
                    }
                    else if (pnl_barcode.Visible)
                    {
                        pnl_barcode.Visible = false;
                        dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[1].Value = txtbarcode.Text;
                        int rowno = dgvProduct.CurrentCell.RowIndex;
                        UpcScanner(rowno);
                    }
                    else if (panelProductPopup.Visible)
                        FillDetails();
                    else
                        Movefocus(0);
                    return true; // Mark the key as handled
                case Keys.Escape:
                    if (pnlCustomer.Visible)
                        pnlCustomer.Visible = false;
                    else if (panelProductPopup.Visible)
                    {
                        panelProductPopup.Visible = false;
                        dgvProduct.Enabled = true;
                    }
                    else if (tlp_grid_registartion.Visible)
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
                        pnl_barcode.Visible = false;
                        txtbarcode.Text = "";
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
            if (pnlCustomer.Visible)
            {
                if (txtVendorName.Focused)
                    txtVendorTel.Focus();
                else if (txtVendorTel.Focused)
                    txtVendorAddress.Focus();
                else if (txtVendorAddress.Focused)
                    txtVendorEmail.Focus();
                else if (txtVendorEmail.Focused)
                    txtdes.Focus();
                else if (txtdes.Focused)
                    btnSaveCust.Focus();
                else if (keytype == 1)
                {
                    if (btnSaveCust.Focused)
                        btnCloseCst.Focus();
                }
                else if (keytype == 0)
                {
                    if (btnSaveCust.Focused)
                        btnSaveCust.PerformClick();
                    else if (btnCloseCst.Focused)
                        btnCloseCst.PerformClick();
                }
            }
            else
            {
                if (txtdocno.Focused)
                    custname.Focus();
                else if (custname.Focused)
                    btn_vendor_add.Focus();
                else if (btn_vendor_add.Focused)
                {
                    if (keytype == 1)
                        address.Focus();
                    else if (keytype == 0)
                        btn_vendor_add.PerformClick();
                }
                else if (address.Focused)
                    contactno.Focus();
                else if (contactno.Focused)
                    txtEmail.Focus();
                else if (txtEmail.Focused)
                    discountper.Focus();
                else if (discountper.Focused)
                    totaldiscountamt.Focus();
                else if (totaldiscountamt.Focused)
                    remarks.Focus();
                else if (remarks.Focused)
                    oaamount.Focus();
                else if (oaamount.Focused)
                    oadesc.Focus();
                else if (oadesc.Focused)
                    odamt.Focus();
                else if (odamt.Focused)
                    oddesc.Focus();
                else if (oadesc.Focused)
                    oddesc.Focus();
                else if (oddesc.Focused)
                    sterms.Focus();
                else if (sterms.Focused)
                    rcvamt.Focus();
                else if (rcvamt.Focused)
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

        }

        private void FrmPurchase_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }

        private void txtVendorEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtVendorEmail.Text != "")
            {
                ClsGeneral cls = new ClsGeneral();
                if (!cls.CheckEmailId(this, txtVendorEmail.Text))
                {
                    e.Cancel = true;
                }
            }
        }
        private void btn_vendor_close_Click_1(object sender, EventArgs e)
        {
            pnlCustomer.Visible = false;
            txtVendorName.Text = "";
            txtVendorAddress.Text = "";
            txtVendorEmail.Text = "";
            txtVendorTel.Text = "";
            btn_vendor_add.Focus();
        }

        private void btn_productpopup_close_Click(object sender, EventArgs e)
        {
            panelProductPopup.Visible = false;
            dgvProduct.Enabled = true;
        }
    }
}
