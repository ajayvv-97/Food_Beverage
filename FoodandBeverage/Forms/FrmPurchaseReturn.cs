using BLL;
using Domain;
using FoodandBeverage.Forms.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FoodandBeverage.Interfaces.Interfaces;

namespace FoodandBeverage.Forms
{
    public partial class FrmPurchaseReturn : Form
    {
        public FrmPurchaseReturn()
        {
            InitializeComponent();
        }
        #region Datagrid Declaration
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
        public static int inetamount = 11;
        public static int itax = 12;
        public static int itaxtype = 13;
        public static int israte = 14;
        public static int imrp = 15;

        public static int iPurchasemasterid = 16;
        public static int iitemId = 17;
        public static int iproducttype = 18;//entype

        public static int iPurchaseDetailsId = 20;
        public static int ipurchasemasterentryno = 21;
        public static int iprid = 22;
        public static int iorglQty = 23;
        public static int iorgbarcode = 24;
        public static int itaxsettingid = 25;

        /*dgvItemPop*/

        public static int ipopslno = 0;
        public static int ipopEntryNo = 1;
        public static int ipopDescription = 2;
        public static int ipopPurchaseRate = 3;
        public static int ipopNetavaQoty = 5;
        public static int ipopNetAmount = 6;
        public static int ipopEntryDate = 7;
        public static int ipopItemId = 8;
        public static int ipopProductType = 9;
        public static int ipopPurchaseDetailId = 10;
        public static int ipopPurchaseMasterId = 11;
        public static int ipopPrid = 12;
        #endregion

        decimal current_qty;
        string purchaseID;
        string vendorID;
        int insflag;
        decimal PRDtotalqty;
        int qcount = 0;
        bool purchasereturnstatus = false;
        DmnPurchase dmnPurchase = new DmnPurchase();
        BllPurchaseReturnMaster bllPurchase = new BllPurchaseReturnMaster();
        BllSearch<DmnPurchaseReturnMaster> _bllSearch = new BllSearch<DmnPurchaseReturnMaster>();
        BllSearch<DmnPurchaseReturnDetails> _bllSearchitem = new BllSearch<DmnPurchaseReturnDetails>();
        BllSearch<DmnGridFields> _bllSearchTax = new BllSearch<DmnGridFields>();
        BllSearch<DmnGridFields> _bllSearchvendor = new BllSearch<DmnGridFields>();
        DmnSearch dmnSearch = new DmnSearch();
        public static bool CellRow = true;

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
        public bool MandatoryCheck()
        {
            if (oddesc.Text == "Description...")
                oddesc.Text = "";
            if (oadesc.Text == "Description...")
                oadesc.Text = "";

            //if (string.IsNullOrEmpty(txtpurchaseentry.Text))
            //{
            //    MessageBox.Show("Please Select Purchase Entry No", "Mandaatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            else if (dgvProduct.Rows[0].Cells[iitemId].Value == null)
            {
                MessageBox.Show("No Product Found", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {
                    dmnPurchase.PurchaseMasterId = Convert.ToInt32(purchaseID);
                    dmnPurchase.PurchaseReturnMasterId = Convert.ToInt32("0");
                    dmnPurchase.EntryNo = invoiceno.Text;//purchasereturn entry no
                    dmnPurchase.DocNo = txtdocno.Text;
                    dmnPurchase.DocDate = Convert.ToDateTime(dtpdocdate.Value);
                    //vendor detaild
                    dmnPurchase.CustomerId = Convert.ToInt32(vendorID);
                    dmnPurchase.Address = address.Text;
                    dmnPurchase.ContactNo = contactno.Text;
                    dmnPurchase.Email = txtEmail.Text;
                    //vendor--------
                    DateTime now = DateTime.Now;
                    dmnPurchase.TotQty = Convert.ToDecimal(lblTotqty.Text);
                    dmnPurchase.TotalAmnt = Convert.ToDecimal(lblTotal.Text);///**not clear
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
                    dmnPurchase.CreatedBy = 1;
                    dmnPurchase.Status = 1;

                    var InsertStatusP = bllPurchase.AddEditData("prdInsert_Upd_PurchaseReturnMaster", dmnPurchase);

                    for (int i = 0; i < dgvProduct.Rows.Count; i++)
                    {
                        if (dgvProduct.Rows[i].Cells[iitemId].Value != null && dgvProduct.Rows[i].Cells[0].Value != "")
                        {
                            dmnPurchase.PurchaseReturnDetailId = Convert.ToInt32("0");
                            dmnPurchase.PurchaseReturnMasterId = Convert.ToInt32(InsertStatusP.PurchaseReturnMasterId);
                            dmnPurchase.Barcode = Convert.ToString(dgvProduct.Rows[i].Cells[ibarcode].Value);
                            dmnPurchase.Name = dgvProduct.Rows[i].Cells[iname].Value.ToString();
                            dmnPurchase.ItemId = Convert.ToInt32(dgvProduct.Rows[i].Cells[iitemId].Value);
                            dmnPurchase.QTY = Convert.ToInt32(dgvProduct.Rows[i].Cells[iqty].Value);
                            dmnPurchase.Prate = Convert.ToDecimal(dgvProduct.Rows[i].Cells[iprate].Value.ToString());
                            dmnPurchase.Amount = Convert.ToDecimal(dgvProduct.Rows[i].Cells[iamount].Value.ToString());
                            dmnPurchase.DisPer = Convert.ToDecimal(dgvProduct.Rows[i].Cells[idisper].Value.ToString());
                            dmnPurchase.DisAmnt = Convert.ToDecimal(dgvProduct.Rows[i].Cells[idisamt].Value.ToString());
                            dmnPurchase.TotalAmnt = Convert.ToDecimal(dgvProduct.Rows[i].Cells[inetamount].Value.ToString());
                            dmnPurchase.Type = dgvProduct.Rows[i].Cells[iproducttype].Value.ToString();
                            dmnPurchase.CreatedBy = 1;
                            dmnPurchase.BranchId = 1;
                            dmnPurchase.Status = 1;
                            dmnPurchase.PurchaseMasterId = Convert.ToInt32(dgvProduct.Rows[i].Cells[iPurchasemasterid].Value.ToString());
                            dmnPurchase.EntryNo = dgvProduct.Rows[i].Cells[ipurchasemasterentryno].Value.ToString();
                            dmnPurchase.Unit = dgvProduct.Rows[i].Cells[iunit].Value.ToString();
                            dmnPurchase.PurchaseDetailId = Convert.ToInt32(dgvProduct.Rows[i].Cells[iPurchaseDetailsId].Value.ToString());

                            dmnPurchase.TaxType = Convert.ToString(dgvProduct.Rows[i].Cells[itax].Value);
                            dmnPurchase.Tax = Convert.ToString(dgvProduct.Rows[i].Cells[itaxtype].Value);

                            dmnPurchase.TaxAmount = Convert.ToDecimal(dgvProduct.Rows[i].Cells[itaxamt].Value);
                            dmnPurchase.TaxPer = Convert.ToDecimal(dgvProduct.Rows[i].Cells[itaxper].Value);
                            dmnPurchase.PrId = Convert.ToInt32(dgvProduct.Rows[i].Cells[iprid].Value);
                            var InsertStatus = bllPurchase.AddEditDataDet("prdInsert_Upd_PurchaseReturnDetails", dmnPurchase);
                            if (InsertStatus == null)
                            {
                                MessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                            if (InsertStatus != null)
                            {
                                insflag = 1;
                            }
                        }
                    }
                    if (insflag == 1)
                    {
                        MessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearAll();
                    }
                }
                else
                {
                    if (oddesc.Text == "Description..." || oddesc.Text == "")
                    {
                        oddesc.Text = "Description...";
                        oddesc.ForeColor = Color.LightGray;
                    }
                    if (oadesc.Text == "Description..." || oadesc.Text == "")
                    {
                        oadesc.Text = "Description...";
                        oadesc.ForeColor = Color.LightGray;
                    }
                }
            }
            catch (Exception ex)
            { }
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
                if (dgvProduct.Rows[cellproductid].Cells[iitemId].Value != "" && dgvProduct.Rows[cellproductid].Cells[iprate].Value != "" && dgvProduct.Rows[cellproductid].Cells[iqty].Value != "")
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
                        dgvProduct.Rows[cellproductid].Cells[iamount].Value = tot.ToString("N2");
                    }
                    if (Convert.ToString(dgvProduct.Rows[cellproductid].Cells[itax].Value) == "Inclusive")
                    {
                        decimal tot = (amount - Convert.ToDecimal(dgvProduct.Rows[cellproductid].Cells[idisamt].Value));
                        dgvProduct.Rows[cellproductid].Cells[iamount].Value = tot.ToString("N2");
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
                string cellpopupid = Convert.ToString(dgvProduct.Rows[cellproductid].Cells[iitemId].Value);
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
                    if (dgvProduct.Rows[i].Cells[iitemId].Value != "" && dgvProduct.Rows[i].Cells[iitemId].Value != null)
                    {
                        sum = sum + Convert.ToDecimal(dgvProduct.Rows[i].Cells[iamount].Value);
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

        private void txtpurchaseentry_Enter(object sender, EventArgs e)
        {
            lblToolTippurchaseentry.Visible = true;
        }

        private void txtpurchaseentry_Leave(object sender, EventArgs e)
        {
            lblToolTippurchaseentry.Visible = false;
        }

        private void txtpurchaseentry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
                LedgerPopup();

            if (e.KeyData == Keys.Enter)
            {
                dgvProduct.Focus();
            }
        }

        public void LedgerPopup()
        {
            try
            {
                BllSearch<DmnGridFields> Searchcount = new BllSearch<DmnGridFields>();
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,EntryNo,DocumentNo,VendorName,Purchase TotalQuantity,TotalDiscount,NetAmount";
                _objSearch.Select = "PurchaseMasterId as Id,EntryNo as Field1,DocumentNo as Field2,v.VendorName as Field3,(Select Sum(Quantity) from tblPurchaseDetails pd where pd.PurchaseMasterId = pm.PurchaseMasterId ) as Field4,TotalDiscount as Field5, NetAmount as Field6";
                _objSearch.Table = "tblPurchaseMaster pm " +
                    "LEFT JOIN tblVendor v ON pm.LedgerId  =  v.VendorId ";
                _objSearch.WhereNdGroupBy = "where isnull(pm.Status,0) = 1 And  isnull(v.Status,0)=1";
                _objSearch.OrderBy = " ORDER BY PurchaseMasterId  desc";
                var CheckData = Searchcount.GetSearchData("prdSearch", _objSearch);
                if (CheckData.Count == 0)
                {
                    MessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if(CheckData.Count>0)
                {
                    pnl_purchaseList.Location = new Point(this.ClientSize.Width / 2 - pnl_purchaseList.Size.Width / 2, (this.ClientSize.Height / 3 - pnl_purchaseList.Size.Height / 3));
                    pnl_purchaseList.Visible = true;
                    dgv_purchaseList.Rows.Add(CheckData.Count);
                    for (int i = 0; i < CheckData.Count; i++)
                    {
                        dgv_purchaseList.Rows[i].Cells[0].Value = i + 1;
                        dgv_purchaseList.Rows[i].Cells[1].Value = CheckData[i].Id;
                        dgv_purchaseList.Rows[i].Cells[2].Value = CheckData[i].Field1;
                        dgv_purchaseList.Rows[i].Cells[3].Value = CheckData[i].Field2;
                        dgv_purchaseList.Rows[i].Cells[4].Value = CheckData[i].Field3;
                        dgv_purchaseList.Rows[i].Cells[5].Value = CheckData[i].Field4;
                        dgv_purchaseList.Rows[i].Cells[6].Value = CheckData[i].Field5;
                        dgv_purchaseList.Rows[i].Cells[7].Value = CheckData[i].Field6;

                    }
                }
        
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

                    
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
        }
        
        public void CheckQty_popclclick(int rowid)
        {
            try
            {
                decimal entered_qty = 0, purch_qty = 0, ret_qty = 0, current_qty = 0;
                int dgvProduct_rowid = 0;
                BllSearch<DmnPurchaseReturnMaster> bllSearch = new BllSearch<DmnPurchaseReturnMaster>();
                DmnSearch dmnSearch = new DmnSearch();
                DmnSearch dmnSearch1 = new DmnSearch();
                BllSearch<DmnGridFields> _bllSearch1 = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch2 = new DmnSearch();
                BllSearch<DmnGridFields> _bllSearch2 = new BllSearch<DmnGridFields>();

                int purchaseID = Convert.ToInt32(dgvItemPop.Rows[rowid].Cells[ipopPurchaseMasterId].Value);
                if (Convert.ToInt32(purchaseID) > 0 && (dgvItemPop.Rows[rowid].Cells[ipopPurchaseDetailId].Value) != null)
                {
                    string purchasedetailsid = Convert.ToString(dgvItemPop.Rows[rowid].Cells[ipopPurchaseDetailId].Value);

                    dmnSearch.Select = " b.Quantity as Quantity,b.ItemId as ItemId,b.ProductType as ProductType,b.PurchaseDetailId as PurchaseDetailId,b.PurchaseMasterId as PurchaseMasterId,pr.QOH as AvaQOH";
                    dmnSearch.Table = "tblPurchaseMaster a LEFT JOIN tblPurchaseDetails b ON b.PurchaseMasterId = a.PurchaseMasterId LEFT JOIN tblProductRate pr ON pr.id=b.PrId";
                    dmnSearch.WhereNdGroupBy = " Where  a.PurchaseMasterId='" + purchaseID + "'";
                    dmnSearch.OrderBy = " Order By a.EntryNo ASC ";
                    var gridSource = _bllSearch.GetSearchData("prdSearch", dmnSearch);//purchase orignal qty



                    dmnSearch1.Select = " SUM(prd.Quantity) as Field1,prd.ItemId as Field2";
                    dmnSearch1.Table = "tblPurchaseReturnDetails prd left join tblPurchaseReturnMaster prm on prm.PurchaseReturnMasterId=prd.PurchaseReturnMasterId";
                    dmnSearch1.WhereNdGroupBy = "where prd.PurchaseDetailId='" + Convert.ToString(dgvItemPop.Rows[rowid].Cells[ipopPurchaseDetailId].Value) + "' and ItemId='" + Convert.ToString(dgvItemPop.Rows[rowid].Cells[ipopItemId].Value) + "'";
                    dmnSearch1.OrderBy = "GROUP BY prd.itemid";
                    var gridSource1 = _bllSearch1.GetSearchData("prdSearch", dmnSearch1);//purchase return all qty             \

                    for (int i = 0; i < dgvProduct.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dgvItemPop.Rows[rowid].Cells[ipopPurchaseDetailId].Value) == Convert.ToInt32(dgvProduct.Rows[i].Cells[iPurchaseDetailsId].Value))
                        {
                            dgvProduct_rowid = i;
                        }
                    }

                    if (gridSource1.Count > 0)//multiple purchase return check qty
                    {
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            for (int j = 0; j < gridSource1.Count; j++)
                            {
                                if (gridSource[i].ItemId == gridSource1[j].Field2)
                                {
                                    entered_qty = Convert.ToDecimal(dgvProduct.Rows[dgvProduct_rowid].Cells[iqty].Value);
                                    purch_qty = Convert.ToDecimal(gridSource[i].Quantity);
                                    ret_qty = Convert.ToDecimal(gridSource1[j].Field1);
                                    current_qty = Convert.ToInt32(gridSource[i].AvaQOH);
                                    //totalqty = Convert.ToDecimal(gridSource[i].Quantity) - Convert.ToDecimal(gridSource1[j].Field1);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < gridSource.Count; i++)//new purchase return check qty
                        {
                            if (gridSource[i].PurchaseDetailId == Convert.ToString(dgvProduct.Rows[dgvProduct_rowid].Cells[iPurchaseDetailsId].Value))
                            {
                                entered_qty = Convert.ToDecimal(dgvProduct.Rows[dgvProduct_rowid].Cells[iqty].Value);
                                purch_qty = Convert.ToDecimal(gridSource[i].Quantity);
                                current_qty = Convert.ToInt32(gridSource[i].AvaQOH);

                            }
                        }

                    }
                    if ((purch_qty < (entered_qty + ret_qty)) || (current_qty < entered_qty))
                    {
                        decimal temp = purch_qty - ret_qty;
                        if (temp > current_qty)
                        {
                            MessageBox.Show("Available Quantity is " + current_qty, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            dgvProduct.Rows[dgvProduct_rowid].Cells[iqty].Value = current_qty;
                        }
                        else
                        {
                            MessageBox.Show("Available Quantity is " + temp, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            dgvProduct.Rows[dgvProduct_rowid].Cells[iqty].Value = temp;
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void btn_close_dgvItemPop_Click(object sender, EventArgs e)
        {
            pnlpurchasedetails.Visible = false;
        }

        private void dgvItemPop__CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int cellpopupid = dgvItemPop.CurrentCell.RowIndex;
                int n = dgvProduct.RowCount;
                int cellproductid = dgvProduct.CurrentCell.RowIndex;
                string tablename = null;
                int dgvProduct_rowid = 0;
                if (dgvItemPop.Rows[cellpopupid].Cells[iqty].Value == "Out of stock")
                {
                    MessageBox.Show("Avilable PurChase Quantity is Zero ! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (Convert.ToInt32(dgvItemPop.Rows[cellpopupid].Cells[ipopNetavaQoty].Value) == 0)
                {
                    MessageBox.Show("Avilable Stock Quantity is Zero ! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                dmnSearch.Select = "a.ItemBarCode as ItemBarCode,i.itemName as ItemName,a.Quantity as Quantity,a.PurchaseRate as PurchaseRate,a.Amount as Amount,a.DiscountPer as DiscountPer,a.DiscountAmount as DiscountAmount,a.taxper as TaxPer, a.TaxAmount as TaxAmount,a.NetAmount as  NetAmount,a.Tax as  taxname,a.Srate as srate ,a.MRP as  MRP,a.PurchaseMasterId as PurchaseMasterId,a.ItemId as ItemId ,a.ProductType as ProductType,a.PurchaseDetailId as PurchaseDetailsId,b.EntryNo as EntryNo,(Select Sum(QOH) from tblProductRate pr where pr.ProductId = i.itemid )as QuantityOnHand,i.TaxType";
                tablename = "tblPurchaseDetails a LEFT JOIN tblPurchaseMaster b ON b.PurchaseMasterId = a.PurchaseMasterId " +
                    " LEFT JOIN tblitem i ON i.ItemId = a.ItemId " +
                " left join  tblProductRate pr on pr.ProductId =i.ItemId";
                dmnSearch.Table = tablename;
                dmnSearch.WhereNdGroupBy = " Where a.PurchaseDetailId  = " + dgvItemPop.Rows[cellpopupid].Cells[ipopPurchaseDetailId].Value;
                dmnSearch.OrderBy = "";
                var gridSource = _bllSearchitem.GetSearchData("prdSearch", dmnSearch);

                decimal temp = 0;
                n = dgvProduct.RowCount;
                for (int i = 0; i < n; i++)
                {
                    if (dgvProduct.Rows[i].Cells[iPurchaseDetailsId].Value == null)
                    {
                        dgvProduct.Rows[i].Cells[iPurchaseDetailsId].Value = 0;
                    }

                    else if ((dgvProduct.Rows[i].Cells[iPurchaseDetailsId].Value.ToString() == Convert.ToString(gridSource[0].PurchasedetailsId)))//&& ((dgvProduct.Rows[i].Cells[10].Value).ToString() == gridSource[0].Field13)
                    {
                        temp = Convert.ToDecimal(dgvProduct.Rows[i].Cells[iqty].Value) + 1;
                        dgvProduct.Rows[i].Cells[iqty].Value = temp;
                        dgvProduct_rowid = i;
                        dgvProduct.Rows[cellproductid].Cells[ibarcode].Value = "";
                        totalcellamount(cellproductid);
                        grandTotal();
                        totQty();
                        totDisc();
                        netAmount();
                        pnlpurchasedetails.Hide();
                        txtpurchaseentry.Enabled = true;
                        txtpurchaseentry.BackColor = Color.White;
                        dgvProduct.Rows[cellproductid].Cells[iname].Style.ForeColor = Color.Black;
                        dgvProduct.Focus();
                    }


                }
                if (temp == 0)
                {
                    dgvProduct.Rows[cellproductid].Cells[ibarcode].ReadOnly = true;
                    dgvProduct.Rows[cellproductid].Cells[iname].ReadOnly = true;
                    dgvProduct.Rows[cellproductid].Cells[iunit].ReadOnly = true;
                    dgvProduct.Rows[cellproductid].Cells[iprate].ReadOnly = true;
                    dgvProduct.Rows[cellproductid].Cells[inetamount].ReadOnly = true;
                    dgvProduct.Rows[cellproductid].Cells[itaxper].ReadOnly = true;
                    dgvProduct.Rows[cellproductid].Cells[itaxamt].ReadOnly = true;
                    dgvProduct.Rows[cellproductid].Cells[iamount].ReadOnly = true;
                    dgvProduct.Rows[cellproductid].Cells[itax].ReadOnly = true;
                    dgvProduct.Rows[cellproductid].Cells[itax].ReadOnly = true;
                    dgvProduct.Rows[cellproductid].Cells[israte].ReadOnly = true;
                    dgvProduct.Rows[cellproductid].Cells[imrp].ReadOnly = true;
                    dgvProduct.Rows[cellproductid].Cells[itaxtype].ReadOnly = true;
                    dgvProduct.Rows[cellproductid].Cells[iname].Style.ForeColor = Color.Black;

                    dgvProduct.Rows[cellproductid].Cells[islno].Value = cellproductid + 1;
                    dgvProduct.Rows[cellproductid].Cells[ibarcode].Value = gridSource[0].ItemBarCode;
                    dgvProduct.Rows[cellproductid].Cells[iname].Value = gridSource[0].ItemName;
                    dgvProduct.Rows[cellproductid].Cells[iqty].Value = "1";
                    dgvProduct.Rows[cellproductid].Cells[iunit].Value = "Number";
                    dgvProduct.Rows[cellproductid].Cells[iprate].Value = gridSource[0].PurchaseRate;
                    dgvProduct.Rows[cellproductid].Cells[iamount].Value = gridSource[0].Amount;
                    dgvProduct.Rows[cellproductid].Cells[idisper].Value = 0;
                    dgvProduct.Rows[cellproductid].Cells[idisamt].Value = 0;
                    dgvProduct.Rows[cellproductid].Cells[itaxper].Value = gridSource[0].TaxPer;
                    dgvProduct.Rows[cellproductid].Cells[itaxamt].Value = gridSource[0].TaxAmount;
                    dgvProduct.Rows[cellproductid].Cells[inetamount].Value = gridSource[0].NetAmount;
                    dgvProduct.Rows[cellproductid].Cells[itax].Value = gridSource[0].taxname;
                    dgvProduct.Rows[cellproductid].Cells[itaxtype].Value = gridSource[0].TaxType; 
                    dgvProduct.Rows[cellproductid].Cells[israte].Value = gridSource[0].srate;
                    dgvProduct.Rows[cellproductid].Cells[imrp].Value = gridSource[0].MRP;

                    dgvProduct.Rows[cellproductid].Cells[iPurchasemasterid].Value = gridSource[0].PurchaseMasterId;///purchasemasterid
                    dgvProduct.Rows[cellproductid].Cells[iitemId].Value = gridSource[0].ItemId;//item id 
                    dgvProduct.Rows[cellproductid].Cells[iproducttype].Value = gridSource[0].ProductType; //entype product
                    dgvProduct.Rows[cellproductid].Cells[iPurchaseDetailsId].Value = gridSource[0].PurchasedetailsId;
                    dgvProduct.Rows[cellproductid].Cells[ipurchasemasterentryno].Value = gridSource[0].EntryNo;
                    dgvProduct.Rows[cellproductid].Cells[iprid].Value = dgvItemPop.Rows[cellpopupid].Cells[ipopPrid].Value;
                    dgvProduct.Rows[cellproductid].Cells[iorglQty].Value = dgvItemPop.Rows[cellpopupid].Cells[iqty].Value;
                    dgvProduct.Rows[cellproductid].Cells[iorgbarcode].Value = gridSource[0].ItemBarCode;
                    dgvProduct.Rows[cellproductid].Cells[ibarcode].ReadOnly = true;
                    totalcellamount(cellproductid);
                    grandTotal();
                    totQty();
                    totDisc();
                    netAmount();
                    dgvProduct.Rows.Add(1);
                    dgvProduct.Focus();
                    pnlpurchasedetails.Hide();
                    txtpurchaseentry.Enabled = true;
                    txtpurchaseentry.BackColor = Color.White;
                    dgvProduct.Focus();

                }
                else
                {
                    CheckQty_popclclick(cellpopupid);
                    totalcellamount(dgvProduct_rowid);
                    grandTotal();
                    totQty();
                    totDisc();
                    netAmount();
                    pnlpurchasedetails.Hide();
                    txtpurchaseentry.Enabled = true;
                    txtpurchaseentry.BackColor = Color.White;
                    dgvProduct.Rows[dgvProduct_rowid].Cells[iname].Style.ForeColor = Color.Black;
                    dgvProduct.Focus();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void FrmPurchaseReturn_Load(object sender, EventArgs e)
        {
            ClearAll();
            tlp_textboxes.Visible = true;
            Control controlToRemove = tlp_grid_registartion.GetControlFromPosition(0, 3);
            tlp_grid_registartion.Controls.Remove(controlToRemove);
            tlp_regform_downtlp.Controls.Add(tlp_textboxes, 0, 2);
            tlp_textboxes.Dock = DockStyle.Fill;
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

        private void oaamount_TextChanged(object sender, EventArgs e)
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

        private void dgvItemPop_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvProduct.Columns[e.ColumnIndex].Index == 4)
                {
                    if (e.Value != null)
                    {
                        string stringValue = (e.Value).ToString();
                        if (stringValue == "Out of stock")
                        {
                            e.CellStyle.ForeColor = Color.Red;
                        }
                        else if (stringValue == "0.000")
                        {
                            dgvItemPop.Rows[e.RowIndex].Cells[iqty].Value = "Out of stock";
                        }

                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void btn_close_purchaselist_Click(object sender, EventArgs e)
        {
            pnl_purchaseList.Visible = false;
        }

        private void dgv_purchaseList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearAll();
            dgvProduct.Focus();
            purchaseID =Convert.ToString(dgv_purchaseList.Rows[dgv_purchaseList.CurrentCell.RowIndex].Cells[1].Value); ;
            BllSearch<DmnPurchase> bllSearch = new BllSearch<DmnPurchase>();
            DmnSearch objSearch = new DmnSearch();
            BllSearch<DmnGridFields> bllSearchcount = new BllSearch<DmnGridFields>();
            DmnSearch dmnSearchcount = new DmnSearch();
            objSearch = new DmnSearch();

            /*-----------------check all item return or not particular id--------------*/
            dmnSearchcount.Select = "pm.TotalQuantity as Field1,(Select Sum(Quantity) from tblPurchaseReturnDetails prd  left join tblPurchaseReturnMaster prm on prm.PurchaseReturnMasterId=prd.PurchaseReturnMasterId   where prd.PurchaseReturnMasterId = prm.PurchaseReturnMasterId  and prm.PurchaseMasterId = " + purchaseID + " ) as Field2";
            dmnSearchcount.Table = "tblPurchaseMaster pm";
            dmnSearchcount.WhereNdGroupBy = "where  pm.PurchaseMasterId=" + purchaseID;
            dmnSearchcount.OrderBy = "";
            var gridSourcecountsum = bllSearchcount.GetSearchData("prdSearch", dmnSearchcount);
            if (gridSourcecountsum.Count > 0)
            {
                if (Convert.ToDecimal(gridSourcecountsum[0].Field1) == Convert.ToDecimal(gridSourcecountsum[0].Field2))
                {
                    MessageBox.Show("These Product Are Already Return !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            /*-----------------------------------------------------------------*/


            objSearch.Select = " EntryNo as EntryNo ,v.VendorName as Customer,v.Address as Address,v.email as Email,pm.Telephone as ContactNo,v.Vendorid as Product_id,pm.DocumentNo as DocNo, EntryDate as  DocDate";
            objSearch.Table = "[tblVendor]  v LEFT JOIN [tblPurchaseMaster]  pm on  pm.LedgerId  =  v.VendorId";
            objSearch.WhereNdGroupBy = "where pm.PurchaseMasterId = '" + purchaseID + "'  and isnull(pm.Status,0) = 1 And  isnull(v.Status,0)=1";

            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
            txtpurchaseentry.Text = gridSource[0].EntryNo;
            txtvedorname.Text = gridSource[0].Customer;
            address.Text = Convert.ToString(gridSource[0].Address);
            txtEmail.Text = gridSource[0].Email;
            contactno.Text = gridSource[0].ContactNo;
            txtdocno.Text = gridSource[0].DocNo;
            vendorID = (gridSource[0].Product_id).ToString();
            dtppurchasedate.Value = gridSource[0].DocDate;
            dgvProduct.Focus();
            pnl_purchaseList.Visible = false;
        }

        private void dgvProduct_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    if (dgvProduct.CurrentCell != null)
                    {
                        //compentry.SelectedValue.ToString()

                        int cno = dgvProduct.CurrentCell.ColumnIndex;
                        int rowno = dgvProduct.CurrentCell.RowIndex;
                        bool t = false;
                        decimal temp;


                        if (cno == iname)
                        {
                            if (purchaseID == null || purchaseID == "")
                            {
                                MessageBox.Show("Please Select Purchase Entry No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            txtpurchaseentry.Enabled = false;
                            txtpurchaseentry.BackColor = Color.DarkGray;

                            DmnSearch dmnSearch = new DmnSearch();
                            DmnSearch dmnSearch1 = new DmnSearch();
                            BllSearch<DmnGridFields> _bllSearch1 = new BllSearch<DmnGridFields>();

                            dmnSearch.Select = "a.EntryNo as EntryNo,b.itemName as Description,b.PurchaseRate as PurchaseRate,b.Quantity as Quantity,b.NetAmount as NetAmount,a.EntryDate as EntryDate,b.ItemId as ItemId,b.ProductType as ProductType,b.PurchaseDetailId as PurchaseDetailId,b.PurchaseMasterId as PurchaseMasterId,b.PrId as Prid,pr.QOH as AvaQOH";
                            dmnSearch.Table = "tblPurchaseMaster a LEFT JOIN tblPurchaseDetails b ON b.PurchaseMasterId = a.PurchaseMasterId LEFT JOIN tblProductRate pr on pr.id=b.PrId and pr.Status=1 ";
                            dmnSearch.WhereNdGroupBy = " Where a.PurchaseMasterId=" + purchaseID;// + "and pr.QOH >= b.Quantity";
                            dmnSearch.OrderBy = " Order By a.EntryNo desc ";
                            var gridSource = _bllSearch.GetSearchData("prdSearch", dmnSearch);


                            dmnSearch1.Select = " SUM(prd.Quantity) as field1,prd.itemid as field2";
                            dmnSearch1.Table = "tblPurchaseReturnDetails prd left join tblPurchaseReturnMaster prm on prm.PurchaseReturnMasterId=prd.PurchaseReturnMasterId "
                                + "LEFT JOIN tblitem d ON d.itemid = prd.ItemId";                               
                            dmnSearch1.WhereNdGroupBy = "where prm.PurchaseMasterId='" + purchaseID + "'";
                            dmnSearch1.OrderBy = "GROUP BY prd.itemid";
                            var gridSource1 = _bllSearch1.GetSearchData("prdSearch", dmnSearch1);
                            dgvItemPop.Rows.Clear();//----------------mutiple purchase return happen --------------
                            if (gridSource.Count > 0)
                            {
                                dgvItemPop.Rows.Add(gridSource.Count);
                                for (int i = 0; i < gridSource.Count; i++)
                                {
                                    t = false;
                                    current_qty = 0;
                                    PRDtotalqty = 0;
                                    dgvItemPop.Rows[i].Cells[ipopslno].Value = i + 1;
                                    dgvItemPop.Rows[i].Cells[ipopEntryNo].Value = gridSource[i].EntryNo;
                                    dgvItemPop.Rows[i].Cells[ipopDescription].Value = gridSource[i].Description;
                                    dgvItemPop.Rows[i].Cells[ipopPurchaseRate].Value = gridSource[i].PurchaseRate;


                                    if (gridSource1.Count > 0)
                                    {
                                        for (int j = 0; j < gridSource1.Count; j++)
                                        {
                                            if (gridSource[i].ItemId == gridSource1[j].Field2)
                                            {
                                                PRDtotalqty = Convert.ToDecimal(gridSource[i].Quantity) - Convert.ToDecimal(gridSource1[j].Field1);
                                                t = true;

                                            }

                                        }
                                    }
                                    else
                                    {
                                        PRDtotalqty = Convert.ToDecimal(gridSource[i].Quantity);
                                    }//--------------------------mutiple purchase return happen ---------------------
                                    if (PRDtotalqty == 0 && t)//qty is zero and enter loop true (happen only multiple purchase return)
                                        dgvItemPop.Rows[i].Cells[iqty].Value = PRDtotalqty - current_qty;
                                    else if (PRDtotalqty == 0)//new purchase return happen
                                        dgvItemPop.Rows[i].Cells[iqty].Value = Convert.ToDecimal(gridSource[i].Quantity) - current_qty;
                                    else//new purchase return happen no purchase return happen
                                        dgvItemPop.Rows[i].Cells[iqty].Value = PRDtotalqty - current_qty;

                                    dgvItemPop.Rows[i].Cells[ipopNetavaQoty].Value = gridSource[i].AvaQOH;
                                    dgvItemPop.Rows[i].Cells[ipopNetAmount].Value = gridSource[i].NetAmount;
                                    dgvItemPop.Rows[i].Cells[ipopEntryDate].Value = gridSource[i].EntryDate;
                                    dgvItemPop.Rows[i].Cells[ipopItemId].Value = gridSource[i].ItemId;//itemid
                                    dgvItemPop.Rows[i].Cells[ipopProductType].Value = gridSource[i].ProductType;
                                    dgvItemPop.Rows[i].Cells[ipopPurchaseDetailId].Value = gridSource[i].PurchaseDetailId;
                                    dgvItemPop.Rows[i].Cells[ipopPurchaseMasterId].Value = gridSource[i].PurchaseMasterId;//purchaemasterid
                                    dgvItemPop.Rows[i].Cells[ipopPrid].Value = gridSource[i].Prid;//purchaemasterid

                                }
                            }
                            pnlpurchasedetails.Location = new Point(
                              this.ClientSize.Width / 2 - pnlpurchasedetails.Size.Width / 2,
                              (this.ClientSize.Height / 3 - pnlpurchasedetails.Size.Height / 2));
                            pnlpurchasedetails.Show();
                            dgvItemPop.Focus();

                        }
                    }
                }
                if (e.KeyData == Keys.Delete)
                {
                    dgvProduct.Rows.RemoveAt(dgvProduct.CurrentRow.Index);
                    int n = dgvProduct.RowCount;
                    int count = 0;
                    if (n > 0)
                    {
                        for (int i = 0; i < n; i++)
                            dgvProduct.Rows[i].Cells[islno].Value = i + 1;
                        dgvProduct.Rows.Add(1);

                        n = dgvProduct.RowCount;
                        for (int i = 0; i < n; i++)
                        {
                            if (dgvProduct.Rows[i].Cells[iitemId].Value == null)
                            {
                                count = count + 1;
                            }
                        }
                        if (count > 1)
                        {
                            for (int i = 0; i < n; i++)
                            {
                                if (dgvProduct.Rows[i].Cells[iitemId].Value == null)
                                {
                                    dgvProduct.Rows.RemoveAt(i);
                                    break;
                                }
                            }
                        }

                    }

                    if (n == 0)
                    {
                        dgvProduct.Rows.Add(1);
                    }

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
                //string purchaseID;
                string purchasedetailsid;
                bool t = false;
                decimal totalqty = 0;
                int curre_qty = 0;
                decimal purch_qty = 0;
                decimal ret_qty = 0;
                bool flag = false;
                int rowno = 0;

                if (this.dgvProduct.Columns[e.ColumnIndex].Index == iqty && (e.RowIndex) != -1)
                {
                    if (dgvProduct.Rows[e.RowIndex].Cells[iqty].Value != null && dgvProduct.Rows[e.RowIndex].Cells[iitemId].Value == null)
                    {
                        dgvProduct.Rows[e.RowIndex].Cells[iqty].Value = 0;
                    }
                    if (dgvProduct.Rows[e.RowIndex].Cells[iPurchasemasterid].Value == null || dgvProduct.Rows[e.RowIndex].Cells[iPurchaseDetailsId].Value == null)
                    {
                        //dgvProduct.Rows[e.RowIndex].Cells[15].Value = 0;
                        dgvProduct.Rows[e.RowIndex].Cells[iPurchaseDetailsId].Value = 0;
                        dgvProduct.Rows[e.RowIndex].Cells[iamount].Value = 0;
                    }
                    else
                    {
                        decimal entered_qty = 0;
                        BllSearch<DmnPurchaseReturnMaster> bllSearch = new BllSearch<DmnPurchaseReturnMaster>();
                        DmnSearch dmnSearch = new DmnSearch();
                        DmnSearch dmnSearch1 = new DmnSearch();
                        BllSearch<DmnGridFields> _bllSearch1 = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch2 = new DmnSearch();
                        BllSearch<DmnGridFields> _bllSearch2 = new BllSearch<DmnGridFields>();

                        purchaseID = (dgvProduct.Rows[e.RowIndex].Cells[iPurchasemasterid].Value).ToString();
                        if (Convert.ToInt32(purchaseID) > 0 && (dgvProduct.Rows[e.RowIndex].Cells[iPurchaseDetailsId].Value) != null)
                        {
                            purchasedetailsid = (dgvProduct.Rows[e.RowIndex].Cells[iPurchaseDetailsId].Value).ToString();


                            dmnSearch.Select = " b.Quantity as Quantity,b.ItemId as ItemId,b.ProductType as ProductType,b.PurchaseDetailId as PurchaseDetailId,b.PurchaseMasterId as PurchaseMasterId,pr.QOH as AvaQOH";
                            dmnSearch.Table = "tblPurchaseMaster a LEFT JOIN tblPurchaseDetails b ON b.PurchaseMasterId = a.PurchaseMasterId LEFT JOIN tblProductRate pr ON pr.id=b.PrId";
                            dmnSearch.WhereNdGroupBy = " Where a.PurchaseMasterId='" + purchaseID + "'";
                            dmnSearch.OrderBy = " Order By a.EntryNo ASC ";
                            var gridSource = _bllSearch.GetSearchData("prdSearch", dmnSearch);//purchase orignal qty



                            dmnSearch1.Select = " SUM(prd.Quantity) as Field1,prd.ItemId as Field2";
                            dmnSearch1.Table = "tblPurchaseReturnDetails prd left join tblPurchaseReturnMaster prm on prm.PurchaseReturnMasterId=prd.PurchaseReturnMasterId";
                            dmnSearch1.WhereNdGroupBy = "where prd.PurchaseDetailId='" + dgvProduct.Rows[e.RowIndex].Cells[iPurchaseDetailsId].Value.ToString() + "' and ItemId='" + dgvProduct.Rows[e.RowIndex].Cells[iitemId].Value.ToString() + "'";
                            dmnSearch1.OrderBy = "GROUP BY prd.itemid";
                            var gridSource1 = _bllSearch1.GetSearchData("prdSearch", dmnSearch1);//purchase return all qty                   

                            if (gridSource1.Count > 0)//multiple purchase return check qty
                            {
                                for (int i = 0; i < gridSource.Count; i++)
                                {
                                    for (int j = 0; j < gridSource1.Count; j++)
                                    {
                                        if (gridSource[i].ItemId == gridSource1[j].Field2)
                                        {
                                            entered_qty = Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[iqty].Value);
                                            purch_qty = Convert.ToDecimal(gridSource[i].Quantity);
                                            ret_qty = Convert.ToDecimal(gridSource1[j].Field1);
                                            current_qty = Convert.ToInt32(gridSource[i].AvaQOH);
                                            //totalqty = Convert.ToDecimal(gridSource[i].Quantity) - Convert.ToDecimal(gridSource1[j].Field1);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < gridSource.Count; i++)//new purchase return check qty
                                {
                                    if (gridSource[i].PurchaseDetailId == dgvProduct.Rows[e.RowIndex].Cells[iPurchaseDetailsId].Value.ToString())
                                    {
                                        entered_qty = Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[iqty].Value);
                                        purch_qty = Convert.ToDecimal(gridSource[i].Quantity);
                                        current_qty = Convert.ToInt32(gridSource[i].AvaQOH);

                                    }
                                }

                            }
                            if ((purch_qty < (entered_qty + ret_qty)) || (current_qty < entered_qty))
                            {
                                decimal temp = purch_qty - ret_qty;
                                if (temp > current_qty)
                                {
                                    MessageBox.Show("Available Quantity is " + current_qty, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    dgvProduct.Rows[e.RowIndex].Cells[iqty].Value = current_qty;
                                }
                                else
                                {
                                    MessageBox.Show("Available Quantity is " + temp, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    dgvProduct.Rows[e.RowIndex].Cells[iqty].Value = temp;
                                }

                            }

                        }

                        totalcellamount(e.RowIndex);
                    }

                }

                if (e.ColumnIndex == idisper || e.ColumnIndex == idisamt)
                {
                    if (dgvProduct.Rows[e.RowIndex].Cells[idisper].Value != null && dgvProduct.Rows[e.RowIndex].Cells[iitemId].Value == null)
                    {
                        dgvProduct.Rows[e.RowIndex].Cells[idisper].Value = 0;
                    }
                    if (dgvProduct.Rows[e.RowIndex].Cells[idisamt].Value != null && dgvProduct.Rows[e.RowIndex].Cells[iitemId].Value == null)
                    {
                        dgvProduct.Rows[e.RowIndex].Cells[idisamt].Value = 0;
                    }
                    totalcellamount(e.RowIndex);
                }

                string tablename = null;
                t = false;
                //if (e.ColumnIndex == ibarcode && dgvProduct.Rows[e.RowIndex].Cells[ibarcode].Value != "")
                //{
                //    if (purchaseID == null || purchaseID == "")
                //    {
                //        MessageBox.Show("Please Select Purchase Entry No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }

                //    BllSearch<DmnPurchaseReturnMaster> bllSearch1 = new BllSearch<DmnPurchaseReturnMaster>();
                //    DmnSearch dmnSearch = new DmnSearch();
                //    BllSearch<DmnGridFields> bllSearch2 = new BllSearch<DmnGridFields>();
                //    DmnSearch dmnSearch1 = new DmnSearch();

                //    dmnSearch.Select = "a.EntryNo as EntryNo,b.ItemCode as Description,b.PurchaseRate as PurchaseRate,b.Quantity as Quantity,b.NetAmount as NetAmount,a.EntryDate as EntryDate,b.ItemId as ItemId,b.ProductType as ProductType,b.PurchaseDetailId as PurchaseDetailId,b.PurchaseMasterId as PurchaseMasterId,b.Prid as Prid";
                //    dmnSearch.Table = "tblPurchaseMaster a " +
                //        "LEFT JOIN tblPurchaseDetails b ON b.PurchaseMasterId = a.PurchaseMasterId " +
                //        "";
                //    dmnSearch.WhereNdGroupBy = " Where a.BranchId = " + BranchId + " and " +
                //        "a.PurchaseMasterId='" + purchaseID + "'" + " and b.ItemBarCode='" + dgvProduct.Rows[e.RowIndex].Cells[ibarcode].Value + "'"; ;
                //    dmnSearch.OrderBy = " Order By a.EntryNo ASC ";
                //    var gridSource1 = bllSearch1.GetSearchData("prdSearch", dmnSearch);


                //    //total count  qty ( multiple purchasereturn entries done)
                //    dmnSearch1.Select = " SUM(prd.Quantity) as field1,itemid as field2";
                //    dmnSearch1.Table = "tblPurchaseReturnDetails prd left join tblPurchaseReturnMaster prm on prm.PurchaseReturnMasterId=prd.PurchaseReturnMasterId ";
                //    /*+ "LEFT JOIN tblFoodAndBeverages d ON d.ProductId = prd.ItemId and prd.ProductType = " + Convert.ToInt32(ProductType.FB)
                //    + " LEFT JOIN tblMarchandise m ON m.ProductId = prd.ItemId and prd.ProductType = " + Convert.ToInt32(ProductType.Merchandise)
                //    + " LEFT JOIN tblBowlingAccessories ba ON ba.ProductId = prd.ItemId and prd.ProductType = " + Convert.ToInt32(ProductType.Bowling)
                //    + " LEFT JOIN tblRedemptionProducts rp ON rp.ProductId = prd.ItemId and prd.ProductType = " + Convert.ToInt32(ProductType.RedemptionTicket);*/
                //    dmnSearch1.WhereNdGroupBy = "where prm.PurchaseMasterId='" + purchaseID + "'" + " and prd.ItemBarCode='" + dgvProduct.Rows[e.RowIndex].Cells[ibarcode].Value + "'";
                //    dmnSearch1.OrderBy = "GROUP BY prd.itemid";
                //    var gridSource2 = bllSearch2.GetSearchData("prdSearch", dmnSearch1);

                //    if (gridSource1.Count > 0)
                //    {
                //        if (gridSource1.Count > 1)
                //        {
                //            dgvItemPop.Rows.Clear();//----------------mutiple purchase return happen --------------
                //            dgvItemPop.Rows.Add(gridSource1.Count);
                //            for (int i = 0; i < gridSource1.Count; i++)
                //            {
                //                current_qty = 0;
                //                PRDtotalqty = 0;


                //                dgvItemPop.Rows[i].Cells[ipopslno].Value = i + 1;
                //                dgvItemPop.Rows[i].Cells[ipopEntryNo].Value = gridSource1[i].EntryNo;
                //                dgvItemPop.Rows[i].Cells[ipopDescription].Value = gridSource1[i].Description;
                //                dgvItemPop.Rows[i].Cells[ipopPurchaseRate].Value = gridSource1[i].PurchaseRate;

                //                if (gridSource2.Count > 0)
                //                {
                //                    for (int j = 0; j < gridSource1.Count; j++)
                //                    {
                //                        if (gridSource1[i].ItemId == gridSource2[j].Field2)
                //                        {
                //                            PRDtotalqty = Convert.ToDecimal(gridSource1[i].Quantity) - Convert.ToDecimal(gridSource2[j].Field1);
                //                            t = true;

                //                        }

                //                    }
                //                }
                //                else
                //                {
                //                    PRDtotalqty = Convert.ToDecimal(gridSource1[i].Quantity);
                //                }//--------------------------mutiple purchase return happen end ---------------------


                //                //for (int j = 0; j < dgvProduct.RowCount; j++)//---------for products grid count quantity-------
                //                //{
                //                //    string x1;
                //                //    string x2 = gridSource1[i].PurchaseDetailId;
                //                //    if (dgvProduct.Rows[j].Cells[iPurchaseDetailsId].Value == null)
                //                //    {
                //                //        x1 = null;
                //                //    }
                //                //    else
                //                //    {
                //                //        x1 = (dgvProduct.Rows[j].Cells[iPurchaseDetailsId].Value).ToString();
                //                //    }

                //                //    if (x1 == x2)
                //                //    {
                //                //        current_qty = current_qty + Convert.ToDecimal(dgvProduct.Rows[j].Cells[iqty].Value);
                //                //    }
                //                //}//--------------------------------end for products grid count   quantity--------------------------

                //                if (PRDtotalqty == 0 && t)//qty is zero and enter loop true (happen only multiple purchase return)
                //                    dgvItemPop.Rows[i].Cells[iqty].Value = PRDtotalqty - current_qty;
                //                else if (PRDtotalqty == 0)//new purchase return happen
                //                    dgvItemPop.Rows[i].Cells[iqty].Value = Convert.ToDecimal(gridSource1[i].Quantity) - current_qty;
                //                else//new purchase return happen no purchase return happen
                //                    dgvItemPop.Rows[i].Cells[iqty].Value = PRDtotalqty - current_qty;

                //                dgvItemPop.Rows[i].Cells[ipopNetavaQoty].Value = gridSource1[i].NetAmount;
                //                dgvItemPop.Rows[i].Cells[ipopNetAmount].Value = gridSource1[i].NetAmount;
                //                dgvItemPop.Rows[i].Cells[ipopEntryDate].Value = gridSource1[i].EntryDate;
                //                dgvItemPop.Rows[i].Cells[ipopEntryDate].Value = gridSource1[i].ItemId;//itemid
                //                dgvItemPop.Rows[i].Cells[ipopProductType].Value = gridSource1[i].ProductType;
                //                dgvItemPop.Rows[i].Cells[ipopPurchaseDetailId].Value = gridSource1[i].PurchaseDetailId;
                //                dgvItemPop.Rows[i].Cells[ipopPurchaseMasterId].Value = gridSource1[i].PurchaseMasterId;//purchaemasterid
                //                dgvItemPop.Rows[i].Cells[ipopPrid].Value = gridSource1[i].Prid;//purchaemasterid

                //            }
                //            panelProductPopup.Location = new Point(this.Width / 2 - panelProductPopup.Size.Width / 2, this.Height / 2 - panelProductPopup.Size.Height / 2);
                //            panelProductPopup.Show();

                //        }
                //        else
                //        {
                //            decimal totqty = 0;
                //            if (gridSource2.Count > 0)
                //            {
                //                totqty = Convert.ToDecimal(gridSource1[0].Quantity) - Convert.ToDecimal(gridSource2[0].Field1);
                //            }
                //            else
                //                totqty = 0;

                //            if (Convert.ToDecimal(gridSource1[0].Quantity) >= totqty)
                //            {
                //                int n = dgvProduct.RowCount;

                //                for (int i = 0; i < n; i++)
                //                {
                //                    if (Convert.ToInt32(dgvProduct.Rows[i].Cells[iPurchaseDetailsId].Value) == Convert.ToInt32(gridSource1[0].PurchaseDetailId))
                //                    {
                //                        flag = true;
                //                        rowno = i;
                //                    }
                //                }
                //                if (flag)
                //                {
                //                    decimal temp = Convert.ToDecimal(dgvProduct.Rows[rowno].Cells[iqty].Value) + 1;
                //                    dgvProduct.Rows[rowno].Cells[iqty].Value = temp;
                //                    if (e.RowIndex != rowno)
                //                    {
                //                        if (dgvProduct.Rows[e.RowIndex].Cells[iprid].Value != null)
                //                        {
                //                            dgvProduct.Rows[e.RowIndex].Cells[ibarcode].Value = dgvProduct.Rows[e.RowIndex].Cells[iorgbarcode].Value;
                //                        }
                //                        else
                //                        {
                //                            dgvProduct.Rows[e.RowIndex].Cells[ibarcode].Value = "";
                //                        }

                //                        totalcellamount(e.RowIndex);
                //                        grandTotal();
                //                        totQty();
                //                        totDisc();
                //                        netAmount();
                //                    }
                //                    CheckQty(rowno);
                //                }
                //                else
                //                {
                //                    dmnSearch.Select = "a.ItemBarCode as ItemBarCode,ItemCode as ItemName,a.Quantity as Quantity,a.PurchaseRate as PurchaseRate,a.Amount as Amount,a.DiscountPer as DiscountPer,a.DiscountAmount as DiscountAmount,a.taxper as TaxPer, a.TaxAmount as TaxAmount,a.NetAmount as  NetAmount,a.Tax as  taxname,a.Srate as srate ,a.MRP as  MRP,a.PurchaseMasterId as PurchaseMasterId,a.ItemId as ItemId ,a.ProductType as ProductType,a.PurchaseDetailId as PurchaseDetailsId,b.EntryNo as EntryNo,a.PrId as PrId";

                //                    if (Convert.ToString(gridSource1[0].ProductType) == "3")
                //                        tablename = "tblPurchaseDetails a LEFT JOIN tblPurchaseMaster b ON b.PurchaseMasterId = a.PurchaseMasterId LEFT JOIN tblFoodAndBeverages fb ON fb.ProductId = a.ItemId and a.ProductType = " + Convert.ToInt32(ProductType.FB);
                //                    else if (Convert.ToString(gridSource1[0].ProductType) == "8")
                //                        tablename = "tblPurchaseDetails a LEFT JOIN tblPurchaseMaster b ON b.PurchaseMasterId = a.PurchaseMasterId LEFT JOIN tblMarchandise m ON m.ProductId = a.ItemId and a.ProductType = " + Convert.ToInt32(ProductType.Merchandise);
                //                    else if (Convert.ToString(gridSource1[0].ProductType) == "11")
                //                        tablename = "tblPurchaseDetails a LEFT JOIN tblPurchaseMaster b ON b.PurchaseMasterId = a.PurchaseMasterId LEFT JOIN tblBowlingAccessories ba ON ba.ProductId = a.ItemId and a.ProductType = " + Convert.ToInt32(ProductType.Bowling);
                //                    else if (Convert.ToString(gridSource1[0].ProductType) == "6")
                //                        tablename = "tblPurchaseDetails a LEFT JOIN tblPurchaseMaster b ON b.PurchaseMasterId = a.PurchaseMasterId LEFT JOIN tblRedemptionProducts ba ON ba.ProductId = a.ItemId and a.ProductType =" + Convert.ToInt32(ProductType.RedemptionTicket);
                //                    dmnSearch.Table = tablename;
                //                    dmnSearch.WhereNdGroupBy = " Where a.PurchaseDetailId  = '" + gridSource1[0].PurchaseDetailId + "'";
                //                    dmnSearch.OrderBy = "";
                //                    var gridSource3 = _bllSearchitem.GetSearchData("prdSearch", dmnSearch);

                //                    dgvProduct.Rows[e.RowIndex].Cells[iname].ReadOnly = true;
                //                    dgvProduct.Rows[e.RowIndex].Cells[iunit].ReadOnly = true;
                //                    dgvProduct.Rows[e.RowIndex].Cells[iprate].ReadOnly = true;
                //                    dgvProduct.Rows[e.RowIndex].Cells[inetamount].ReadOnly = true;
                //                    dgvProduct.Rows[e.RowIndex].Cells[itaxper].ReadOnly = true;
                //                    dgvProduct.Rows[e.RowIndex].Cells[itaxamt].ReadOnly = true;
                //                    dgvProduct.Rows[e.RowIndex].Cells[iamount].ReadOnly = true;

                //                    dgvProduct.Rows[e.RowIndex].Cells[inetamount].ReadOnly = true;
                //                    dgvProduct.Rows[e.RowIndex].Cells[itax].ReadOnly = true;
                //                    dgvProduct.Rows[e.RowIndex].Cells[israte].ReadOnly = true;
                //                    dgvProduct.Rows[e.RowIndex].Cells[imrp].ReadOnly = true;
                //                    dgvProduct.Rows[e.RowIndex].Cells[itaxtype].ReadOnly = true;
                //                    dgvProduct.Rows[e.RowIndex].Cells[iname].Style.ForeColor = Color.Black;

                //                    dgvProduct.Rows[e.RowIndex].Cells[islno].Value = e.RowIndex + 1;
                //                    dgvProduct.Rows[e.RowIndex].Cells[ibarcode].Value = gridSource3[0].ItemBarCode;
                //                    dgvProduct.Rows[e.RowIndex].Cells[ibarcode].ReadOnly = true;
                //                    dgvProduct.Rows[e.RowIndex].Cells[iname].Value = gridSource3[0].ItemName;
                //                    dgvProduct.Rows[e.RowIndex].Cells[iqty].Value = "1";
                //                    dgvProduct.Rows[e.RowIndex].Cells[iunit].Value = "Number";
                //                    dgvProduct.Rows[e.RowIndex].Cells[iprate].Value = gridSource3[0].PurchaseRate;
                //                    dgvProduct.Rows[e.RowIndex].Cells[iamount].Value = gridSource3[0].Amount;
                //                    dgvProduct.Rows[e.RowIndex].Cells[idisper].Value = 0;
                //                    dgvProduct.Rows[e.RowIndex].Cells[idisamt].Value = 0;
                //                    dgvProduct.Rows[e.RowIndex].Cells[itaxper].Value = gridSource3[0].TaxPer;
                //                    dgvProduct.Rows[e.RowIndex].Cells[itaxamt].Value = gridSource3[0].TaxAmount;
                //                    dgvProduct.Rows[e.RowIndex].Cells[inetamount].Value = gridSource3[0].NetAmount;
                //                    dgvProduct.Rows[e.RowIndex].Cells[itax].Value = gridSource3[0].taxname;
                //                    dgvProduct.Rows[e.RowIndex].Cells[israte].Value = gridSource3[0].srate;
                //                    dgvProduct.Rows[e.RowIndex].Cells[imrp].Value = gridSource3[0].MRP;

                //                    dgvProduct.Rows[e.RowIndex].Cells[iPurchasemasterid].Value = gridSource3[0].PurchaseMasterId;///purchasemasterid
                //                    dgvProduct.Rows[e.RowIndex].Cells[iitemId].Value = gridSource3[0].ItemId;//item id 
                //                    dgvProduct.Rows[e.RowIndex].Cells[iproducttype].Value = gridSource3[0].ProductType; //entype product
                //                    dgvProduct.Rows[e.RowIndex].Cells[iPurchaseDetailsId].Value = gridSource3[0].PurchasedetailsId;
                //                    dgvProduct.Rows[e.RowIndex].Cells[ipurchasemasterentryno].Value = gridSource3[0].EntryNo;
                //                    dgvProduct.Rows[e.RowIndex].Cells[iprid].Value = gridSource3[0].PrId;
                //                    dgvProduct.Rows[e.RowIndex].Cells[iorglQty].Value = gridSource1[0].Quantity;

                //                    dgvProduct.Rows[e.RowIndex].Cells[ibarcode].ReadOnly = true;
                //                    totalcellamount(e.RowIndex);
                //                    grandTotal();
                //                    totQty();
                //                    totDisc();
                //                    netAmount();
                //                    dgvProduct.Rows.Add(1);
                //                    dgvProduct.Focus();
                //                }
                //            }
                //            else
                //            {
                //                MessageBox.Show("Avilable Quantity is " + dgvProduct.Rows[e.RowIndex].Cells[iorglQty].Value, "Out Of Quantity", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //                return;
                //            }
                //            totalcellamount(e.RowIndex);
                //            grandTotal();
                //            totQty();
                //            totDisc();
                //            netAmount();
                //            dgvProduct.Focus();
                //        }
                //    }
                //    else
                //    {
                //        if ((dgvProduct.Rows[e.RowIndex].Cells[ibarcode].Value == "" || dgvProduct.Rows[e.RowIndex].Cells[ibarcode].Value == null) && dgvProduct.Rows[e.RowIndex].Cells[iprid].Value != null)
                //        {
                //            dgvProduct.Rows[e.RowIndex].Cells[ibarcode].Value = dgvProduct.Rows[e.RowIndex].Cells[iorgbarcode].Value;
                //        }
                //        else
                //        {
                //            MessageBox.Show("Invalid Product", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            if (dgvProduct.Rows[e.RowIndex].Cells[iprid].Value != null)
                //            { dgvProduct.Rows[e.RowIndex].Cells[ibarcode].Value = dgvProduct.Rows[e.RowIndex].Cells[iorgbarcode].Value; }
                //            else
                //                dgvProduct.Rows[e.RowIndex].Cells[ibarcode].Value = "";
                //        }
                //    }
                //    if (flag)
                //    {
                //        dgvProduct.ClearSelection();
                //        dgvProduct.Rows[rowno].Selected = true;
                //    }
                //    else
                //    {
                //        int n = dgvProduct.RowCount - 1;
                //        dgvProduct.CurrentCell = dgvProduct.Rows[n].Cells[ibarcode];
                //    }

                //}
                //else
                //{
                //    SendKeys.Send("{UP}");
                //    SendKeys.Send("{RIGHT}");
                //}
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvProduct_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[iitemId].Value == null)
                {
                    dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[iname].Value = "F8 for Products";
                    dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[iname].Style.ForeColor = Color.Red;
                }
                else
                    dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[iname].Style.ForeColor = Color.Black;

            }
        }

        private void dgvProduct_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[iitemId].Value == null)
                {
                    dgvProduct.Rows[dgvProduct.CurrentCell.RowIndex].Cells[iname].Value = "";
                }
            }
        }

        private void dgvProduct_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex == iqty)
                //{
                //    int newValue;
                //    if (!int.TryParse(e.FormattedValue.ToString(), out newValue))
                //    {
                //        e.Cancel = true;
                //    }
                //    else
                //    {
                //        int minValue = 0; // Replace 0 with your desired minimum value
                //        int maxValue = 100; // Replace 100 with your desired maximum value
                //        if (newValue < minValue || newValue > maxValue)
                //        {
                //            e.Cancel = true;
                //        }
                //    }
                //}
                //if (e.ColumnIndex == idisamt)
                //{
                //    int newValue;
                //    if (!int.TryParse(e.FormattedValue.ToString(), out newValue))
                //    {
                //        e.Cancel = true;
                //    }
                //    else
                //    {
                //        int minValue = 0; // Replace 0 with your desired minimum value
                //        int maxValue = 100; // Replace 100 with your desired maximum value
                //        if (newValue < minValue || newValue > maxValue)
                //        {
                //            e.Cancel = true;
                //        }
                //    }
                //}
                //if (e.ColumnIndex == idisper)
                //{
                //    int newValue;
                //    if (!int.TryParse(e.FormattedValue.ToString(), out newValue))
                //    {
                //        e.Cancel = true;
                //    }
                //    else
                //    {
                //        int minValue = 0; // Replace 0 with your desired minimum value
                //        int maxValue = 100; // Replace 100 with your desired maximum value
                //        if (newValue < minValue || newValue > maxValue)
                //        {
                //            e.Cancel = true;
                //        }
                //    }

                //}


                if (dgvProduct.Rows[e.RowIndex].Cells[iitemId].Value != "" && dgvProduct.Rows[e.RowIndex].Cells[iitemId].Value != null)
                {
                    if (e.ColumnIndex == iqty)
                    {
                        DataGridViewTextBoxCell cell = dgvProduct[iqty, e.RowIndex] as DataGridViewTextBoxCell;
                        if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == string.Empty || Convert.ToDecimal(cell.EditedFormattedValue.ToString()) <= 0)
                        {
                            MessageBox.Show("Quantity Must Be Grater Than Zero", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            e.Cancel = true;
                        }
                    }
                    if (e.ColumnIndex == idisper)
                    {
                        DataGridViewTextBoxCell cell = dgvProduct[idisper, e.RowIndex] as DataGridViewTextBoxCell;
                        if ((cell.EditedFormattedValue.ToString() != "") || (cell.EditedFormattedValue.ToString() != string.Empty))
                        {
                            if (Convert.ToDecimal(cell.EditedFormattedValue.ToString()) > 100)
                            {
                                MessageBox.Show("Discount % Must Be Less Than 100", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            cell.Value = "0";
                        }
                    }
                    else if (e.ColumnIndex == idisamt)
                    {
                        DataGridViewTextBoxCell cell = dgvProduct[idisamt, e.RowIndex] as DataGridViewTextBoxCell;
                        if ((cell.EditedFormattedValue.ToString() != "") || (cell.EditedFormattedValue.ToString() != string.Empty))
                        {
                            if (Convert.ToDecimal(cell.EditedFormattedValue.ToString()) > Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[iamount].Value))
                            {
                                MessageBox.Show("Discount Amount Must Be Less Than Amount", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            cell.Value = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvProduct_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(dgvProduct.CurrentCell) != "")
                {
                    totalcellamount(dgvProduct.CurrentCell.RowIndex);
                    grandTotal();
                    totQty();
                    totDisc();
                    netAmount();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void dgvProduct_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvProduct.CurrentCell.ColumnIndex == 4 || dgvProduct.CurrentCell.ColumnIndex == 7 ||
               dgvProduct.CurrentCell.ColumnIndex == 8)
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
            if (dgvProduct.CurrentCell.ColumnIndex == 4 || dgvProduct.CurrentCell.ColumnIndex == 7 ||
                dgvProduct.CurrentCell.ColumnIndex == 8)
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

        private void dgvProduct_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                DataGridViewTextBoxCell disamtcell = dgvProduct[idisamt, e.RowIndex] as DataGridViewTextBoxCell;
                DataGridViewTextBoxCell dispercell = dgvProduct[idisper, e.RowIndex] as DataGridViewTextBoxCell;
                if (dgvProduct.Rows[e.RowIndex].Cells[iitemId].Value != "" && dgvProduct.Rows[e.RowIndex].Cells[iitemId].Value != null)
                {

                    if ((dispercell.EditedFormattedValue.ToString() != "") || (dispercell.EditedFormattedValue.ToString() != string.Empty))
                    {
                        if (Convert.ToDecimal(dispercell.EditedFormattedValue.ToString()) > 100)
                        {
                            MessageBox.Show("Discount % Must Be Less Than 100", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //SendKeys.Send("{UP}");
                            e.Cancel = true;
                            return;
                        }
                    }
                    else
                    {
                        dispercell.Value = "0";
                    }

                    if ((dispercell.EditedFormattedValue.ToString() != "") || (disamtcell.EditedFormattedValue.ToString() != string.Empty))
                    {
                        if (Convert.ToDecimal(disamtcell.EditedFormattedValue.ToString()) > Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells[iamount].Value))
                        {
                            MessageBox.Show("Discount Amount Must Be Less Than Amount", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            e.Cancel = true;
                            return;
                        }
                    }
                    else
                    {
                        disamtcell.Value = "0";
                    }


                    DataGridViewTextBoxCell qtycell = dgvProduct[iqty, e.RowIndex] as DataGridViewTextBoxCell;
                    if ((qtycell.EditedFormattedValue.ToString() == "") || (qtycell.EditedFormattedValue.ToString() == string.Empty) || (Convert.ToDecimal(qtycell.EditedFormattedValue.ToString()) <= 0))
                    {
                        MessageBox.Show("Quantity Must Be Grater Than Zero", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        e.Cancel = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
