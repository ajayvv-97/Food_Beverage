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
using System.Windows;
using System.Windows.Forms;
using static FoodandBeverage.Interfaces.Interfaces;

namespace FoodandBeverage.Forms
{
    public partial class FrmTaxSetting : Form
    {
        public FrmTaxSetting()
        {
            InitializeComponent();
        }
        #region Declaration
        int  TaxSettingsId = 0;
        DmnTaxSettings dmnTaxSettings = new DmnTaxSettings();
        BllTaxSettings bllTaxSettings = new BllTaxSettings();
        public bool deleteclcik = false;
        #endregion
        #region Declaration Grid
        public static int tslno = 0;
        public static int ttidno = 1;
        public static int ttaxtypeno = 2;
        public static int tigstno = 3;
        public static int tsgstno = 4;
        public static int tcgstno = 5;
        public static int tvatno = 6;
        public static int tstatusno = 7;
       #endregion
        public void RefreshForm()
        {
            try
            {
                TaxSettingsId = 0;
                txtSgstPrcnt.Text = "";
                txtCgstPrcnt.Text = "";
                txtIgstPrcnt.Text = "";
                txtVatPrcnt.Text = "";
                filltax();
                cmbTaxType.Focus();
                cmbTaxType.Text = "GST";
                deleteclcik = false;
                btn_del_tax.Visible = false;
            }
            catch (Exception ex)
            {

            }

        }
        public void filltax()
        {
            try
            {
                try
                {
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "TaxSettingsId as Id,TaxType as Field1,IgstPrcnt as Field2," +
                      "CgstPrcnt as Field3,SgstPrcnt as Field4,VatPrcnt as Field5,status as Field6";
                    objSearch.Table = "tblTaxSettings";
                    objSearch.WhereNdGroupBy = "where status=1";
                    objSearch.OrderBy = "Order By TaxSettingsId ASC";
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    dgvTaxSettings.Rows.Clear();
                    if (itemSource.Count > 0)
                    {                       
                        dgvTaxSettings.Rows.Add(itemSource.Count);
                        for (int i = 0; i < itemSource.Count; i++)
                        {
                            dgvTaxSettings.Rows[i].Cells[tslno].Value = i + 1;
                            dgvTaxSettings.Rows[i].Cells[ttidno].Value = itemSource[i].Id;
                            dgvTaxSettings.Rows[i].Cells[ttaxtypeno].Value = itemSource[i].Field1;
                            dgvTaxSettings.Rows[i].Cells[tigstno].Value = itemSource[i].Field2;
                            dgvTaxSettings.Rows[i].Cells[tsgstno].Value = itemSource[i].Field3;
                            dgvTaxSettings.Rows[i].Cells[tcgstno].Value = itemSource[i].Field4;
                            dgvTaxSettings.Rows[i].Cells[tvatno].Value = itemSource[i].Field5;
                            dgvTaxSettings.Rows[i].Cells[tstatusno].Value = itemSource[i].Field6;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
        public Boolean NameExist()
        {
            if(deleteclcik==false)
            {
                if (Convert.ToString(cmbTaxType.Text) != "")
                {
                    BllSearch<DmnTaxSettings> bllSearch = new BllSearch<DmnTaxSettings>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.ColumnHeaders = "TaxSettingsId, TaxType ";
                    objSearch.Select = " TaxSettingsId As numbr";
                    objSearch.Table = " tblTaxSettings ";

                    if (TaxSettingsId == 0)
                    {
                        if (cmbTaxType.Text == "VAT")
                        {
                            objSearch.WhereNdGroupBy = " Where TaxType= '" + Convert.ToString(cmbTaxType.Text) + "' AND VatPrcnt = '" + Convert.ToDecimal(txtVatPrcnt.Text).ToString("0.00") + "' AND Status=1";// AND BranchId = " + BranchId;
                        }
                        if (cmbTaxType.Text == "GST")
                        {
                            objSearch.WhereNdGroupBy = " Where TaxType= '" + Convert.ToString(cmbTaxType.Text) + "' AND IgstPrcnt = '" + Convert.ToDecimal(txtIgstPrcnt.Text).ToString("0.00") + "' AND Status=1";// AND BranchId = " + BranchId;
                        }
                    }
                    else
                    {
                        if (cmbTaxType.Text == "VAT")
                        {
                            objSearch.WhereNdGroupBy = " Where TaxSettingsId!= " + TaxSettingsId + " AND TaxType= '" + Convert.ToString(cmbTaxType.Text) + "' AND VatPrcnt = '" + Convert.ToString(txtVatPrcnt.Text) + "' AND Status=1";// AND BranchId = " + BranchId;
                        }
                        if (cmbTaxType.Text == "GST")
                        {
                            objSearch.WhereNdGroupBy = " Where TaxSettingsId!= " + TaxSettingsId + " AND TaxType= '" + Convert.ToString(cmbTaxType.Text) + "' AND IgstPrcnt = '" + Convert.ToString(txtIgstPrcnt.Text) + "' AND Status=1";// AND BranchId = " + BranchId;
                        }
                    }
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource.Count >= 1)
                    {
                        CMessageBox.Show("Tax Settings Already Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbTaxType.Focus();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }
        public bool MandatoryCheck()
        {
            if (cmbTaxType.Text == "GST")
            {
                if (txtIgstPrcnt.Text == "")
                {
                    CMessageBox.Show("Please Enter IGST %", "Mandaatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtSgstPrcnt.Text))
                    txtSgstPrcnt.Text = "0";
                if (string.IsNullOrWhiteSpace(txtSgstPrcnt.Text))
                    txtSgstPrcnt.Text = "0";

                decimal tottax = Convert.ToDecimal(txtSgstPrcnt.Text) + Convert.ToDecimal(txtCgstPrcnt.Text);
                if (tottax > Convert.ToDecimal(txtIgstPrcnt.Text))
                {
                    CMessageBox.Show("CGST% and SGST%  integrated  Can't Be written More Than GST", "GST", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCgstPrcnt.Focus();
                    return false;
                }
                else if (tottax < Convert.ToDecimal(txtIgstPrcnt.Text))
                {
                    CMessageBox.Show("CGST% and SGST%  integrated  Can't Be written Less Than GST", "GST", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCgstPrcnt.Focus();
                    return false;
                }

            }
            else if (cmbTaxType.Text == "VAT")
            {
                if (txtVatPrcnt.Text == "")
                {
                    CMessageBox.Show("Please Enter VAT %", "Mandaatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }


            return true;
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {

                    if (NameExist())
                    {
                        bool savestatus;
                        dmnTaxSettings.TaxSettingsId = Convert.ToInt32(TaxSettingsId);
                        dmnTaxSettings.TaxType = Convert.ToString(cmbTaxType.Text);
                        if (txtIgstPrcnt.Text != "")
                        {
                            dmnTaxSettings.IgstPrcnt = Convert.ToDecimal(txtIgstPrcnt.Text);
                            dmnTaxSettings.CgstPrcnt = Convert.ToDecimal(txtCgstPrcnt.Text);
                            dmnTaxSettings.SgstPrcnt = Convert.ToDecimal(txtSgstPrcnt.Text);
                            dmnTaxSettings.VatPrcnt = Convert.ToDecimal("0.00");
                        }
                        if (txtVatPrcnt.Text != "")
                        {
                            dmnTaxSettings.VatPrcnt = Convert.ToDecimal(txtVatPrcnt.Text);
                            dmnTaxSettings.IgstPrcnt = Convert.ToDecimal("0.00");
                            dmnTaxSettings.CgstPrcnt = Convert.ToDecimal("0.00");
                            dmnTaxSettings.SgstPrcnt = Convert.ToDecimal("0.00");
                        }
                        if(chb_active.Checked)
                            dmnTaxSettings.Status = 1;
                        else
                            dmnTaxSettings.Status = 0;
                        dmnTaxSettings.BranchId = FrmDashboard.BranchId;
                        dmnTaxSettings.CreatedBy = 1;

                        var lstData = bllTaxSettings.AddData("prdInsert_Upd_TaxSettings", dmnTaxSettings);
                        savestatus = lstData;
                        if (savestatus)
                        {
                            if (TaxSettingsId == 0)
                                CMessageBox.Show(" Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            else
                                CMessageBox.Show(" Updated successfully ", "Updated  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }

                        else
                            CMessageBox.Show(" Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        RefreshForm();
                        filltax();
                        cmbTaxType.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void cmbTaxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSgstPrcnt.Text = "";
            txtSgstPrcnt.Text = "";
            txtIgstPrcnt.Text = "";
            txtVatPrcnt.Text = "";
            tlp_registration.Controls.Clear();
            btn_del_tax.Width = 50;
            btn_del_tax.Height = 47;
            if (cmbTaxType.Text == "GST")
            {
                lblcgst.Visible = true;
                lblsgst.Visible = true;
                lbligst.Visible = true;
                lblvat.Visible = false;
                txtSgstPrcnt.Visible = true;
                txtCgstPrcnt.Visible = true;
                txtIgstPrcnt.Visible = true;
                txtVatPrcnt.Visible = false;

                lblcgst.Dock = DockStyle.Fill;
                lblsgst.Dock = DockStyle.Fill;
                lbligst.Dock = DockStyle.Fill;
                txtSgstPrcnt.Dock = DockStyle.Fill;
                txtCgstPrcnt.Dock = DockStyle.Fill;
                txtIgstPrcnt.Dock = DockStyle.Fill;
                //row1
                tlp_registration.Controls.Add(lbltaxtype, 0, 0);
                tlp_registration.Controls.Add(lbligst, 1, 0);
                tlp_registration.Controls.Add(lblsgst, 2, 0);
                //row2
                tlp_registration.Controls.Add(cmbTaxType, 0, 1);
                tlp_registration.Controls.Add(txtIgstPrcnt, 1, 1);
                tlp_registration.Controls.Add(txtSgstPrcnt, 2, 1);
                //row3
                tlp_registration.Controls.Add(lblcgst, 0, 2);
                //row 4
                tlp_registration.Controls.Add(txtCgstPrcnt, 0, 3);
                tlp_registration.Controls.Add(btn_del_tax, 1, 3);
              
            }
            else if (cmbTaxType.Text == "VAT")
            {
                lblcgst.Visible = false;
                lblsgst.Visible = false;
                lbligst.Visible = false;
                lblvat.Visible = true;
                txtSgstPrcnt.Visible = false;
                txtCgstPrcnt.Visible = false;
                txtIgstPrcnt.Visible = false;
                txtVatPrcnt.Visible = true;

                lblvat.Dock = DockStyle.Fill;
                txtVatPrcnt.Dock = DockStyle.Fill;

                //row1
                tlp_registration.Controls.Add(lbltaxtype, 0, 0);
                tlp_registration.Controls.Add(lblvat, 1, 0);
                //row2
                tlp_registration.Controls.Add(cmbTaxType, 0, 1);
                tlp_registration.Controls.Add(txtVatPrcnt, 1, 1);
                tlp_registration.Controls.Add(btn_del_tax, 2, 1);

            }
     
        }

        private void txtSgstPrcnt_TextChanged(object sender, EventArgs e)
        {

            if (txtIgstPrcnt.Text != "" && txtIgstPrcnt.Text != null)
            {
                decimal value;
                if (!decimal.TryParse(txtSgstPrcnt.Text, out value))
                {
                    txtSgstPrcnt.Text = "0";
                }
                if (txtSgstPrcnt.Text != "")
                {
                    if (Convert.ToDecimal(txtIgstPrcnt.Text) < Convert.ToDecimal(txtSgstPrcnt.Text))
                    {
                        CMessageBox.Show("SGST % Can't Be written More Than GST", "SGST %", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSgstPrcnt.Text = "";
                        return;
                    }
                    if (txtCgstPrcnt.Text == "")
                        txtCgstPrcnt.Text = "0";
                    decimal totgst = Convert.ToDecimal(txtSgstPrcnt.Text) + Convert.ToDecimal(txtCgstPrcnt.Text);
                    if (totgst > Convert.ToDecimal(txtIgstPrcnt.Text))
                    {
                        CMessageBox.Show("CGST% and SGST% integrated Can't Be written More Than GST", "GST", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCgstPrcnt.Text = "";
                        txtSgstPrcnt.Text = "";
                        return;
                    }
                }
            }
        }

        private void txtSgstPrcnt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCgstPrcnt_TextChanged(object sender, EventArgs e)
        {
            if (txtIgstPrcnt.Text != "" && txtIgstPrcnt.Text != null)
            {
                decimal value;
                if (!decimal.TryParse(txtCgstPrcnt.Text, out value))
                {
                    txtCgstPrcnt.Text = "0";
                }

                if (txtCgstPrcnt.Text != "")
                {
                    if (Convert.ToDecimal(txtIgstPrcnt.Text) < Convert.ToDecimal(txtCgstPrcnt.Text))
                    {
                        CMessageBox.Show("CGST % Can't Be written More Than GST", "CGST %", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCgstPrcnt.Text = "";
                        return;
                    }
                    if (txtSgstPrcnt.Text == "")
                        txtSgstPrcnt.Text = "0";
                    decimal totgst = Convert.ToDecimal(txtSgstPrcnt.Text) + Convert.ToDecimal(txtCgstPrcnt.Text);

                    if (totgst > Convert.ToDecimal(txtIgstPrcnt.Text))
                    {
                        CMessageBox.Show("CGST% and SGST%  integrated  Can't Be written More Than GST", "GST", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCgstPrcnt.Text = "";
                        txtSgstPrcnt.Text = "";
                        return;
                    }
                }
            }
        }

        private void txtIgstPrcnt_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtIgstPrcnt.Text, out value))
            {
                txtIgstPrcnt.Text = "0";
            }
            if (txtIgstPrcnt.Text != "" && Convert.ToDecimal(txtIgstPrcnt.Text) > 100)
            {
                CMessageBox.Show("GST% Can't Be written More Than 100", "GST %", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIgstPrcnt.Text = "0";
                return;
            }

        }

        private void txtVatPrcnt_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtVatPrcnt.Text, out value))
            {
                txtVatPrcnt.Text = "0";
            }
            if (txtVatPrcnt.Text != "" && Convert.ToDecimal(txtVatPrcnt.Text) > 100)
            {
                CMessageBox.Show("Vat% Can't Be written More Than 100", "VAT %", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVatPrcnt.Text = "0";
                return;
            }

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_del_tax_Click(object sender, EventArgs e)
        {
            var result = CMessageBox.Show("Do You want Delete Record", "Confirm The Action", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                deleteclcik = true;
                chb_active.Checked = false;
                btnsave.PerformClick();
                filltax();
            }
        }
        public void filldetails( int taxid)
        {
            try
            {
                BllSearch<DmnTaxSettings> bllSearch = new BllSearch<DmnTaxSettings>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "* ";
                objSearch.Table = "tblTaxSettings";
                objSearch.WhereNdGroupBy = "where status = 1 and TaxSettingsId=" + taxid;
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    TaxSettingsId = taxid;
                    cmbTaxType.Text = Convert.ToString(itemSource[0].TaxType);
                    txtCgstPrcnt.Text = Convert.ToString(itemSource[0].CgstPrcnt);
                    txtSgstPrcnt.Text = Convert.ToString(itemSource[0].SgstPrcnt);
                    txtIgstPrcnt.Text = Convert.ToString(itemSource[0].IgstPrcnt);
                    txtVatPrcnt.Text = Convert.ToString(itemSource[0].VatPrcnt);
                    btn_del_tax.Visible = true;
                    if (cmbTaxType.Text == "VAT")
                    {
                        lblcgst.Visible = false;
                        lblcgst.Visible = false;
                        lbligst.Visible = false;
                        lblvat.Visible = true;
                        txtSgstPrcnt.Visible = false;
                        txtSgstPrcnt.Visible = false;
                        txtIgstPrcnt.Visible = false;
                        txtVatPrcnt.Visible = true;
                    }
                    if (cmbTaxType.Text == "GST")
                    {
                        lblcgst.Visible = true;
                        lblcgst.Visible = true;
                        lbligst.Visible = true;
                        lblvat.Visible = false;
                        txtSgstPrcnt.Visible = true;
                        txtSgstPrcnt.Visible = true;
                        txtIgstPrcnt.Visible = true;
                        txtVatPrcnt.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void dgvTaxSettings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int taxid = Convert.ToInt32(dgvTaxSettings.Rows[e.RowIndex].Cells[ttidno].Value);
            filldetails(taxid);
        }

        private void FrmTaxSetting_Load(object sender, EventArgs e)
        {
            cmbTaxType.Focus();
            filltax();
            dgvTaxSettings.BorderStyle = BorderStyle.None;
            dgvTaxSettings.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvTaxSettings.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTaxSettings.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvTaxSettings.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvTaxSettings.BackgroundColor = Color.White;

            dgvTaxSettings.EnableHeadersVisualStyles = false;
            dgvTaxSettings.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvTaxSettings.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvTaxSettings.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            cmbTaxType.SelectedIndex = 0;


        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtIgstPrcnt,txtVatPrcnt,txtSgstPrcnt,txtCgstPrcnt })
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
        private void btnrefresh_Click(object sender, EventArgs e)
        {
            filltax();
            cmbTaxType.SelectedIndex = 0;
            txtSgstPrcnt.Text = "";
            txtIgstPrcnt.Text = "";
            txtCgstPrcnt.Text = "";
            txtVatPrcnt.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            int returnValue = 0;
            DataTable dtColumnAlign = new DataTable();
            dtColumnAlign.Columns.Add();
            DmnSearch _objSearch = new DmnSearch();
            _objSearch.ColumnHeaders = "ID,TaxType,IgstPrcnt,VatPrcnt";
            _objSearch.Select = "TaxSettingsId as Id,TaxType as Field1,IgstPrcnt as Field2,VatPrcnt as Field3";
            _objSearch.Table = "tblTaxSettings";
            _objSearch.WhereNdGroupBy = "where Status = 1";
            _objSearch.OrderBy = " ORDER BY TaxSettingsId";
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
                    int taxid = Convert.ToInt32(returnValue);
                    BllSearch<DmnTaxSettings> bllSearch1 = new BllSearch<DmnTaxSettings>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "* ";
                    objSearch.Table = "tblTaxSettings";
                    objSearch.WhereNdGroupBy = "where status = 1 and TaxSettingsId=" + taxid;
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        TaxSettingsId = taxid;
                        cmbTaxType.Text = Convert.ToString(itemSource[0].TaxType);
                        txtCgstPrcnt.Text = Convert.ToString(itemSource[0].CgstPrcnt);
                        txtSgstPrcnt.Text = Convert.ToString(itemSource[0].SgstPrcnt);
                        txtIgstPrcnt.Text = Convert.ToString(itemSource[0].IgstPrcnt);
                        txtVatPrcnt.Text = Convert.ToString(itemSource[0].VatPrcnt);
                        btn_del_tax.Visible = true;
                        if (cmbTaxType.Text == "VAT")
                        {
                            lblcgst.Visible = false;
                            lblcgst.Visible = false;
                            lbligst.Visible = false;
                            lblvat.Visible = true;
                            txtSgstPrcnt.Visible = false;
                            txtSgstPrcnt.Visible = false;
                            txtIgstPrcnt.Visible = false;
                            txtVatPrcnt.Visible = true;
                        }
                        if (cmbTaxType.Text == "GST")
                        {
                            lblcgst.Visible = true;
                            lblcgst.Visible = true;
                            lbligst.Visible = true;
                            lblvat.Visible = false;
                            txtSgstPrcnt.Visible = true;
                            txtSgstPrcnt.Visible = true;
                            txtIgstPrcnt.Visible = true;
                            txtVatPrcnt.Visible = false;
                        }
                    }
                }
            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    Movefocus(1);
                    return true; // Mark the key as handled
                case Keys.Enter:
                    if (dgvTaxSettings.Focused)
                    {
                        int taxid = Convert.ToInt32(dgvTaxSettings.Rows[dgvTaxSettings.CurrentCell.RowIndex].Cells[ttidno].Value);
                        filldetails(taxid);
                    }
                    Movefocus(0);
                    return true; // Mark the key as handled
                case Keys.Escape:
                    System.Windows.Forms.Form _instancedash = System.Windows.Forms.Application.OpenForms["FrmDashboard"] as System.Windows.Forms.Form;
                    if (_instancedash != null)
                    {
                        (_instancedash as FrmDashboard)?.Closeform();
                    }
                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        public void Movefocus(int keytype)
        {
            if (cmbTaxType.Focused)
            {
                if (cmbTaxType.Text == "GST")
                    txtIgstPrcnt.Focus();
                else if (cmbTaxType.Text == "VAT")
                    txtVatPrcnt.Focus();
            }
            else if (txtIgstPrcnt.Focused)
                txtSgstPrcnt.Focus();
            else if (txtSgstPrcnt.Focused)
                txtCgstPrcnt.Focus();
            else if (txtCgstPrcnt.Focused)
            {
                if (btn_del_tax.Visible)
                    btn_del_tax.Focus();
                else
                    btnsave.Focus();
            }
            else if (txtVatPrcnt.Focused)
            {
                if (btn_del_tax.Visible)
                    btn_del_tax.Focus();
                else
                    btnsave.Focus();
            }
            else if (btn_del_tax.Focused)
            {
                if (keytype == 1)
                    btnsave.Focus();
                else if (keytype == 0)
                    btn_del_tax.PerformClick();
            }
            else if (keytype == 1)
            {
                if (btnsave.Focused)
                    btnSearch.Focus();
                else if (btnSearch.Focused)
                    btnrefresh.Focus();
                else if (btnrefresh.Focused)
                    btnclose.Focus();
                else if (btnclose.Focused)
                {
                    dgvTaxSettings.Focus();
                    dgvTaxSettings.CurrentCell = dgvTaxSettings[0, 0];
                }
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

        private void FrmTaxSetting_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }
    }
    }
