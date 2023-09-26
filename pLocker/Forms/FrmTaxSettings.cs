using BLL;
using Domain;
using pLocker.Classes;
using pLocker.Forms;
using pLocker.Forms.UserControls;
using pLocker.Interfaces;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using AForge.Video;
using AForge.Video.DirectShow;
using MetroFramework;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using pLocker.Classes;
using ClsGeneral = pLocker.Classes.ClsGeneral;

namespace pLocker.Forms
{
    public partial class FrmTaxSettings : Form
    {
        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsGeneral = new ClsGeneral();
        FilterInfoCollection FilterInfoCollection;
        VideoCaptureDevice VideoCaptureDevice;


        DmnTaxSettings dmnTaxSettings = new DmnTaxSettings();
        BllTaxSettings bllTaxSettings = new BllTaxSettings();
        long BranchId = Forms.frmLogin.BranchId;//16
        long UserId = Forms.frmLogin.UserId;//1
        long TaxSettingsId = 0;
      
        long FinancialYearId = Convert.ToInt64(FrmLockerDashBoard.FinancialYearId);//10
        public static string empImgExt = "";
        string _retValue;
        long AccID;
        public string Compressedimg = Application.StartupPath + "\\CompressedImg";
        int LedgerId;
        public FrmTaxSettings()
        {
            InitializeComponent();
            //LedgerId = ledgerid;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void Refreshform()
        {
            mcmbTaxType.Text = "select";
            mtxtVatPrcnt.Text = "";
            mtxtIgstPrcnt.Text = "";
            mtxtCgstPrcnt.Text = "";
            mtxtSgstPrcnt.Text = "";
        }
        private void btnrefresh_Click(object sender, EventArgs e)
        {
            Refreshform();
        }

        private void mbtnSave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void mcmbTaxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mcmbTaxType.Text == "VAT")
            {
                mtxtIgstPrcnt.Hide();
                mtxtCgstPrcnt.Hide();
                mtxtSgstPrcnt.Hide();
                mtxtVatPrcnt.Show();
                label2.Hide();
                label3.Hide();
                label4.Hide();
                label1.Show();
                mtxtSgstPrcnt.Text = "";
                mtxtCgstPrcnt.Text = "";
                mtxtIgstPrcnt.Text = "";
                mtxtVatPrcnt.Text = "";
            }
            if (mcmbTaxType.Text == "GST")
            {
                label1.Hide();
                label2.Show();
                label3.Show();
                label4.Show();
                mtxtIgstPrcnt.Show();
                mtxtCgstPrcnt.Show();
                mtxtSgstPrcnt.Show();
                mtxtVatPrcnt.Hide();
                mtxtSgstPrcnt.Text = "";
                mtxtCgstPrcnt.Text = "";
                mtxtIgstPrcnt.Text = "";
                mtxtVatPrcnt.Text = "";
            }
        }
        private void mtxtVatPrcnt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void mtxtVatPrcnt_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(mtxtVatPrcnt.Text, out value))
            {
                mtxtVatPrcnt.Text = "0";
            }
            if (mtxtVatPrcnt.Text != "" && Convert.ToDecimal(mtxtVatPrcnt.Text) > 100)
            {
                MessageBox.Show("Vat% Can't Be written More Than 100", "VAT %", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtxtVatPrcnt.Text = "0";
                return;
            }
        }

        private void mtxtIgstPrcnt_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(mtxtIgstPrcnt.Text, out value))
            {
                mtxtIgstPrcnt.Text = "0";
            }
            if (mtxtIgstPrcnt.Text != "" && Convert.ToDecimal(mtxtIgstPrcnt.Text) > 100)
            {
                MessageBox.Show("GST% Can't Be written More Than 100", "GST %", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtxtIgstPrcnt.Text = "0";
                return;
            }
        }

        private void mtxtIgstPrcnt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void mtxtCgstPrcnt_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(mtxtCgstPrcnt.Text, out value))
            {
                mtxtCgstPrcnt.Text = "";
            }
        }

        private void mtxtCgstPrcnt_TextChanged(object sender, EventArgs e)
        {
            if (mtxtIgstPrcnt.Text != "" && mtxtIgstPrcnt.Text != null)
            {
                decimal value;
                if (!decimal.TryParse(mtxtCgstPrcnt.Text, out value))
                {
                    mtxtCgstPrcnt.Text = "0";
                }

                if (mtxtCgstPrcnt.Text != "")
                {
                    if (Convert.ToDecimal(mtxtIgstPrcnt.Text) < Convert.ToDecimal(mtxtCgstPrcnt.Text))
                    {
                        MessageBox.Show("CGST % Can't Be written More Than GST", "CGST %", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mtxtCgstPrcnt.Text = "";
                        return;
                    }
                    if (mtxtSgstPrcnt.Text == "")
                        mtxtSgstPrcnt.Text = "0";
                    decimal totgst = Convert.ToDecimal(mtxtSgstPrcnt.Text) + Convert.ToDecimal(mtxtCgstPrcnt.Text);

                    if (totgst > Convert.ToDecimal(mtxtIgstPrcnt.Text))
                    {
                        MessageBox.Show("CGST% and SGST%  integrated  Can't Be written More Than GST", "GST", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mtxtCgstPrcnt.Text = "";
                        mtxtSgstPrcnt.Text = "";
                        return;
                    }
                }
            }
        }

        private void mtxtCgstPrcnt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void mtxtCgstPrcnt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void mtxtSgstPrcnt_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(mtxtSgstPrcnt.Text, out value))
            {
                mtxtSgstPrcnt.Text = "";
            }
        }

        private void mtxtSgstPrcnt_TextAlignChanged(object sender, EventArgs e)
        {

        }

        private void mtxtSgstPrcnt_TextChanged(object sender, EventArgs e)
        {
            if (mtxtIgstPrcnt.Text != "" && mtxtIgstPrcnt.Text != null)
            {
                decimal value;
                if (!decimal.TryParse(mtxtSgstPrcnt.Text, out value))
                {
                    mtxtSgstPrcnt.Text = "0";
                }
                if (mtxtSgstPrcnt.Text != "")
                {
                    if (Convert.ToDecimal(mtxtIgstPrcnt.Text) < Convert.ToDecimal(mtxtSgstPrcnt.Text))
                    {
                        MessageBox.Show("SGST % Can't Be written More Than GST", "SGST %", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mtxtSgstPrcnt.Text = "";
                        return;
                    }
                    if (mtxtCgstPrcnt.Text == "")
                        mtxtCgstPrcnt.Text = "0";
                    decimal totgst = Convert.ToDecimal(mtxtSgstPrcnt.Text) + Convert.ToDecimal(mtxtCgstPrcnt.Text);
                    if (totgst > Convert.ToDecimal(mtxtIgstPrcnt.Text))
                    {
                        MessageBox.Show("CGST% and SGST% integrated Can't Be written More Than GST", "GST", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mtxtCgstPrcnt.Text = "";
                        mtxtSgstPrcnt.Text = "";
                        return;
                    }
                }
            }
        }

        private void mtxtSgstPrcnt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void mtxtSgstPrcnt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void mtxtVatPrcnt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void mtxtIgstPrcnt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void mcmbTaxType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void mbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {

                    if (NameExist())
                    {

                        bool savestatus;
                        dmnTaxSettings.TaxSettingsId = Convert.ToInt32(TaxSettingsId);
                        dmnTaxSettings.TaxType = Convert.ToString(mcmbTaxType.Text);
                        if (mtxtIgstPrcnt.Text != "")
                        {
                            dmnTaxSettings.IgstPrcnt = Convert.ToDecimal(mtxtIgstPrcnt.Text);
                            dmnTaxSettings.CgstPrcnt = Convert.ToDecimal(mtxtCgstPrcnt.Text);
                            dmnTaxSettings.SgstPrcnt = Convert.ToDecimal(mtxtSgstPrcnt.Text);
                            dmnTaxSettings.VatPrcnt = Convert.ToDecimal("0.00");
                        }
                        if (mtxtVatPrcnt.Text != "")
                        {
                            dmnTaxSettings.VatPrcnt = Convert.ToDecimal(mtxtVatPrcnt.Text);
                            dmnTaxSettings.IgstPrcnt = Convert.ToDecimal("0.00");
                            dmnTaxSettings.CgstPrcnt = Convert.ToDecimal("0.00");
                            dmnTaxSettings.SgstPrcnt = Convert.ToDecimal("0.00");
                        }
                        dmnTaxSettings.BranchId = Convert.ToInt32(BranchId);
                        dmnTaxSettings.Status = 1;
                        if (TaxSettingsId == 0)
                        {
                            dmnTaxSettings.CreatedBy = Convert.ToInt32(UserId);
                            var lstData = bllTaxSettings.AddData("prdInsert_Upd_TaxSettings", dmnTaxSettings);
                            savestatus = lstData;
                            if (savestatus)
                                MessageBox.Show(" Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            else
                                MessageBox.Show(" Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            dmnTaxSettings.ModifiedBy = Convert.ToInt32(UserId);
                            dmnTaxSettings.ModifiedDateTime = DateTime.Now;
                            dmnTaxSettings.TaxSettingsId = Convert.ToInt32(TaxSettingsId);
                            var lstData = bllTaxSettings.UpdateData("prdInsertdmnTaxSettings", dmnTaxSettings);
                            savestatus = lstData;
                            if (savestatus)
                                MessageBox.Show(" Updated successfully ", "Updated  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            else
                                MessageBox.Show(" Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Refreshform();
                        fillGridView();
                        mcmbTaxType.Text = "";
                    }
                }

            }
            
            catch (Exception ex)
            {

            }
        }
        public Boolean NameExist()
        {

            if (Convert.ToString(mcmbTaxType.Text) != "")
            {
                BllSearch<DmnTaxSettings> bllSearch = new BllSearch<DmnTaxSettings>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.ColumnHeaders = "TaxSettingsId, TaxType ";
                objSearch.Select = " TaxSettingsId As numbr";
                objSearch.Table = " tblTaxSettings ";

                if (TaxSettingsId == 0)
                {
                    if (mcmbTaxType.Text == "VAT")
                    {
                        objSearch.WhereNdGroupBy = " Where TaxType= '" + Convert.ToString(mcmbTaxType.Text) + "' AND VatPrcnt = '" + Convert.ToDecimal(mtxtVatPrcnt.Text).ToString("0.00") + "' AND Status=1 AND BranchId = " + BranchId;
                    }
                    if (mcmbTaxType.Text == "GST")
                    {
                        objSearch.WhereNdGroupBy = " Where TaxType= '" + Convert.ToString(mcmbTaxType.Text) + "' AND IgstPrcnt = '" + Convert.ToDecimal(mtxtIgstPrcnt.Text).ToString("0.00") + "' AND Status=1 AND BranchId = " + BranchId;
                    }
                }
                else
                {
                    if (mcmbTaxType.Text == "VAT")
                    {
                        objSearch.WhereNdGroupBy = " Where TaxSettingsId!= " + TaxSettingsId + " AND TaxType= '" + Convert.ToString(mcmbTaxType.Text) + "' AND VatPrcnt = '" + Convert.ToString(mtxtVatPrcnt.Text) + "' AND Status=0 AND BranchId = " + BranchId;
                    }
                    if (mcmbTaxType.Text == "GST")
                    {
                        objSearch.WhereNdGroupBy = " Where TaxSettingsId!= " + TaxSettingsId + " AND TaxType= '" + Convert.ToString(mcmbTaxType.Text) + "' AND IgstPrcnt = '" + Convert.ToString(mtxtIgstPrcnt.Text) + "' AND Status=0 AND BranchId = " + BranchId;
                    }
                }
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count >= 1)
                {
                    MessageBox.Show("Tax Settings Already Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mcmbTaxType.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        public void fillGridView()
        {
            try
            {
                dgvTaxSettings.Columns.Clear();
                dgvTaxSettings.DataSource = null;
                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.ColumnHeaders = "TaxSettingsId,TaxType,IgstPrcnt,CgstPrcnt,SgstPrcnt,VatPrcnt";
                dmnSearch.Select = "TaxSettingsId as Id,TaxType as Field1,IgstPrcnt as Field2," +
                   "CgstPrcnt as Field3,SgstPrcnt as Field4,VatPrcnt as Field5";

                dmnSearch.Table = " tblTaxSettings";
                dmnSearch.WhereNdGroupBy = " Where Status = 1";// AND BranchId = " + BranchId;


                dmnSearch.OrderBy = "Order By TaxSettingsId ASC ";
                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvTaxSettings.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvTaxSettings.DataSource = gridSource.DataSource;
            }
            catch (Exception ex)
            {

            }
        }
        private void FrmTaxSettings_Load(object sender, EventArgs e)
        {
            try
            {
                fillGridView();
        BranchId = Convert.ToInt32(frmLogin.BranchId);
               
                //if (frmLogin.ChkWritePermission(Convert.ToInt32(formsenum.TaxSettings)))
                //{
                //    mbtnSave.Enabled = true;

                //}
                //else
                //{
                //    mbtnSave.Enabled = false;

                //}
               mcmbTaxType.Text = "GST";
               
                if (mcmbTaxType.Text == "GST")
                {
                    label1.Hide();
                    label2.Show();
                    label3.Show();
                    label4.Show();
                    mtxtIgstPrcnt.Show();
                    mtxtCgstPrcnt.Show();
                    mtxtSgstPrcnt.Show();
                    mtxtVatPrcnt.Hide();

                }

            }
            catch (Exception ex)
            {

            }


}

public bool MandatoryCheck()
        {
            if (mcmbTaxType.Text == "GST")
            {
                if (mtxtIgstPrcnt.Text == "")
                {
                    MessageBox.Show("Please Enter IGST %", "Mandaatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(mtxtSgstPrcnt.Text))
                    mtxtSgstPrcnt.Text = "0";
                if (string.IsNullOrWhiteSpace(mtxtSgstPrcnt.Text))
                    mtxtSgstPrcnt.Text = "0";
                decimal tottax = Convert.ToDecimal(mtxtSgstPrcnt.Text) + Convert.ToDecimal(mtxtCgstPrcnt.Text);
                if (tottax > Convert.ToDecimal(mtxtIgstPrcnt.Text))
                {
                    MessageBox.Show("CGST% and SGST%  integrated  Can't Be written More Than GST", "GST", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mtxtCgstPrcnt.Focus();
                    return false;
                }
                else if (tottax < Convert.ToDecimal(mtxtIgstPrcnt.Text))
                {
                    MessageBox.Show("CGST% and SGST%  integrated  Can't Be written Less Than GST", "GST", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mtxtCgstPrcnt.Focus();
                    return false;
                }
            }
            else if (mcmbTaxType.Text == "VAT")
            {
                if (mtxtVatPrcnt.Text == "")
                {
                    MessageBox.Show("Please Enter VAT %", "Mandaatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }


            return true;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmTaxSettings_Load_1(object sender, EventArgs e)
        {
            try
            {
                fillGridView();
                BranchId = Convert.ToInt32(frmLogin.BranchId);

                //if (frmLogin.ChkWritePermission(Convert.ToInt32(formsenum.TaxSettings)))
                //{
                //    mbtnSave.Enabled = true;

                //}
                //else
                //{
                //    mbtnSave.Enabled = false;

                //}
                mcmbTaxType.Text = "GST";

                if (mcmbTaxType.Text == "GST")
                {
                    //panel2.Hide();
                    panel3.Show();
                }

            }
            catch (Exception ex)
            {

            }
        }
    }

}
       