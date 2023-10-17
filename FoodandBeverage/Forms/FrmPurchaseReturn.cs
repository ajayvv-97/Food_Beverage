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
    public partial class FrmPurchaseReturn : Form
    {
        public FrmPurchaseReturn()
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
            controlToRemove = tlp_regform_downtlp.GetControlFromPosition(0, 3);
            tlp_regform_downtlp.Controls.Remove(controlToRemove);
            tlp_grid_registartion.Controls.Add(tlp_textboxes, 0, 3);
            tlp_textboxes.Dock = DockStyle.Fill;
        }

        private void btnsave_Click(object sender, EventArgs e)
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

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
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
            catch (Exception ex)
            {

            }

        }
        #endregion
       
    }
}
