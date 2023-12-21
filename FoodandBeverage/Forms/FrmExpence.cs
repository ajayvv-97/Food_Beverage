using BLL;
using CustomMessageBox;
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
    public partial class FrmExpence : Form
    {
        public FrmExpence()
        {
            InitializeComponent();
        }
        #region declaration
        public static int Eslno = 0;
        public static int Ereasonno = 1;
        public static int Eamountno = 2;
        public static int Edecriptionno = 3;
        public static int Eemployenameno = 4;
        public static int Epaymentypeno = 5;
        public static int Eactiono = 6;
        public static int Eemployeidno = 7;
        public static int Eepidno = 8;
        public static int Estatusno = 9;
        #endregion

        private void FrmExpence_Load(object sender, EventArgs e)
        {
            dgv_expense.Rows.Add(1);
            rearrange_slno();
            dgv_expense.BorderStyle = BorderStyle.None;
            dgv_expense.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_expense.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_expense.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_expense.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_expense.BackgroundColor = Color.White;

            dgv_expense.EnableHeadersVisualStyles = false;
            dgv_expense.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_expense.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_expense.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            combofill();
            FillExpence();

        }
        public void FillExpence()
        {
            try
            {              
                
                BllSearch<Dmnexpence> bllSearch = new BllSearch<Dmnexpence>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblExpence";
                objSearch.WhereNdGroupBy = "Where status=1 ";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgv_expense.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_expense.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_expense.Rows[i].Cells[Eslno].Value = i + 1;
                        dgv_expense.Rows[i].Cells[Eepidno].Value = itemSource[i].Eid;
                        dgv_expense.Rows[i].Cells[Ereasonno].Value =Convert.ToInt64(itemSource[i].reason);
                        dgv_expense.Rows[i].Cells[Eamountno].Value = itemSource[i].Amount;
                        dgv_expense.Rows[i].Cells[Edecriptionno].Value = itemSource[i].description;
                        dgv_expense.Rows[i].Cells[Eemployenameno].Value = Convert.ToInt64(itemSource[i].Empolyeeid);
                        dgv_expense.Rows[i].Cells[Epaymentypeno].Value = Convert.ToInt64(itemSource[i].paymentid);
                        dgv_expense.Rows[i].Cells[Estatusno].Value = Convert.ToInt32(itemSource[i].Status);
                    }
                }
                else
                {
                    dgv_expense.Rows.Add(1);
                    rearrange_slno();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnl_serachBox.Visible = true;
            AdjustSearchBoxPanelSize();
        }
        private void AdjustSearchBoxPanelSize()
        {
            int panelWidthPercentage = 80; 
            int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
            int panelHeight = pnl_serachBox.PreferredSize.Height;
            pnl_serachBox.Size = new Size(panelWidth, 178);
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            if (VendorMandatoryCheck())
            {
                Dmnexpence dmnexpencer = new Dmnexpence();
                Dmnexpence InsertStatus = new Dmnexpence();
                BllExpence bllExpence = new BllExpence();
                //if (string.IsNullOrWhiteSpace(txtvid.Text))
                //    txtvid.Text = "0";
                for (int i = 0; i < dgv_expense.Rows.Count; i++)
                {
                    if (dgv_expense.Rows[i].Cells[Ereasonno].Value != null && Convert.ToInt32(dgv_expense.Rows[i].Cells[Ereasonno].Value) > 0)
                    {
                        dmnexpencer.Eid = Convert.ToInt32(dgv_expense.Rows[i].Cells[Eepidno].Value);
                        dmnexpencer.reason = Convert.ToInt32(dgv_expense.Rows[i].Cells[Ereasonno].Value);
                        dmnexpencer.Amount = Convert.ToDecimal(dgv_expense.Rows[i].Cells[Eamountno].Value);
                        dmnexpencer.description = Convert.ToString(dgv_expense.Rows[i].Cells[Edecriptionno].Value);
                        dmnexpencer.Empolyeeid = Convert.ToInt32(dgv_expense.Rows[i].Cells[Eemployenameno].Value);
                        dmnexpencer.paymentid = Convert.ToInt32(dgv_expense.Rows[i].Cells[Epaymentypeno].Value);
                        dmnexpencer.BranchId = FrmDashboard.BranchId;
                        dmnexpencer.CreatedBy = FrmDashboard.UserId;
                        //if (chk_active.Checked)
                        dmnexpencer.Status = 1;
                        //else
                        //    dmnvendor.Status = 0;
                         InsertStatus = bllExpence.AddEditDataExpence("prdInsert_Upd_Expence", dmnexpencer);
                    }
                }
                if (InsertStatus == null)
                {
                    CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (InsertStatus != null)
                {
                    CMessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillExpence();
                    //clearall();
                }
            }
        }
        public bool VendorMandatoryCheck()
        {
            //if (string.IsNullOrEmpty(txtVendorName.Text))
            //{
            //    CMessageBox.Show("Vendor Name Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtVendorName.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(txtVenderAddress.Text))
            //{
            //    CMessageBox.Show("Vendor Address Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtVenderAddress.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(txtVendorTel.Text))
            //{
            //    CMessageBox.Show("Vendor PhNo Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtVendorTel.Focus();
            //    return false;
            //}
            return true;
        }
        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void combofill()
        {

            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Fcid,CategoryName";
                objSearch.Table = " tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where Status=1";
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_reasontype.DataSource = gridSource;
                cmb_reasontype.ValueMember = "Id";
                cmb_reasontype.DisplayMember = "Value";

                cmb_search_reason.DataSource = gridSource;
                cmb_search_reason.ValueMember = "Id";
                cmb_search_reason.DisplayMember = "Value";



                objSearch.Select = "  EmployeeId,EmployeeName";
                objSearch.Table = " tblEmployee";
                objSearch.WhereNdGroupBy = "where status=1";
                objSearch.OrderBy = "";
                var gridSource1 = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_employee.DataSource = gridSource1;
                cmb_employee.ValueMember = "Id";
                cmb_employee.DisplayMember = "Value";

                cmb_search_Employee.DataSource = gridSource1;
                cmb_search_Employee.ValueMember = "Id";
                cmb_search_Employee.DisplayMember = "Value";

                objSearch.Select = "GTypeSubId,Caption";
                objSearch.Table = "tblGeneralOptions";
                objSearch.WhereNdGroupBy = "where GTypeId=2";
                var gridSource2 = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_paymenttype.DataSource = gridSource2;
                cmb_paymenttype.ValueMember = "Id";
                cmb_paymenttype.DisplayMember = "Value";

                cmb_search_payment.DataSource = gridSource2;
                cmb_search_payment.ValueMember = "Id";
                cmb_search_payment.DisplayMember = "Value"; 

            }
            catch (Exception ex)
            {

            }
        }

        private void dgv_expense_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == Eactiono)
                {
                    if (e.RowIndex == dgv_expense.Rows.Count - 1)
                    {
                        if (IsValidData(e.RowIndex))
                            return;
                        dgv_expense.Rows.Add();
                        rearrange_slno();
                        dgv_expense.CurrentCell = dgv_expense.Rows[dgv_expense.Rows.Count - 1].Cells[Ereasonno];
                        dgv_expense.Focus();
                    }
                    else
                    {
                        if (IsValidData(e.RowIndex))
                            return;

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void rearrange_slno()
        {
            for (int i = 0; i < dgv_expense.RowCount; i++)
            {
                dgv_expense.Rows[i].Cells[Eslno].Value = i + 1;
            }
        }

        private bool IsValidData(int rowno)
        {
            DataGridViewComboBoxCell cell = dgv_expense[Ereasonno, rowno] as DataGridViewComboBoxCell;
            string selectedValue = cell.EditedFormattedValue?.ToString();

            DataGridViewTextBoxCell cell1 = dgv_expense[Eamountno, rowno] as DataGridViewTextBoxCell;

            DataGridViewComboBoxCell cell2 = dgv_expense.Rows[rowno].Cells[Eemployenameno] as DataGridViewComboBoxCell;
            string selectedValue2 = cell2.EditedFormattedValue?.ToString();

            DataGridViewComboBoxCell cell3 = dgv_expense.Rows[rowno].Cells[Epaymentypeno] as DataGridViewComboBoxCell;
            string selectedValue3 = cell3.EditedFormattedValue?.ToString();

            if (string.IsNullOrWhiteSpace(selectedValue) || selectedValue == ".")
            {
                CMessageBox.Show("Please Select the Reason Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_expense.CurrentCell = dgv_expense.Rows[rowno].Cells[Ereasonno];
                dgv_expense.BeginEdit(false);

                return true;
            }
            else if (cell1.EditedFormattedValue.ToString() == "" || cell1.EditedFormattedValue.ToString() == "." || cell1.EditedFormattedValue.ToString() == string.Empty)
            {
                CMessageBox.Show("Please Enter the Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_expense.CurrentCell = dgv_expense.Rows[rowno].Cells[Eamountno];
                dgv_expense.BeginEdit(false);
                return true;
            }
            else if (string.IsNullOrWhiteSpace(selectedValue2) || selectedValue2 == ".")
            {
                CMessageBox.Show("Please Select the Employee", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_expense.CurrentCell = dgv_expense.Rows[rowno].Cells[Eemployenameno];
                dgv_expense.BeginEdit(false);

                return true;
            }
           else  if (string.IsNullOrWhiteSpace(selectedValue3) || selectedValue3 == ".")
            {
                CMessageBox.Show("Please Select the payment Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_expense.CurrentCell = dgv_expense.Rows[rowno].Cells[Epaymentypeno];
                dgv_expense.BeginEdit(false);

                return true;
            }
            return false;
        }

        private void dgv_expense_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == Eactiono)
            {
                if (e.RowIndex == dgv_expense.Rows.Count - 1)
                    e.Value = Properties.Resources.add_row;
                else
                    e.Value = Properties.Resources.edit_row;
            }
        }

        private void dgv_expense_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                dgv_expense.Rows.RemoveAt(dgv_expense.CurrentRow.Index);
                int n = dgv_expense.RowCount;
                int count = 0;
                if (n > 0)
                {
                    for (int i = 0; i < n; i++)
                        dgv_expense.Rows[i].Cells[Eslno].Value = i + 1;
                    dgv_expense.Rows.Add(1);

                    n = dgv_expense.RowCount;
                    for (int i = 0; i < n; i++)
                    {
                        if (dgv_expense.Rows[i].Cells[Eepidno].Value == null)
                        {
                            count = count + 1;
                        }
                    }
                    if (count > 1)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            if (dgv_expense.Rows[i].Cells[Eepidno].Value == null)
                            {
                                dgv_expense.Rows.RemoveAt(i);
                                break;
                            }
                        }
                    }

                }

                if (n == 0)
                {
                    dgv_expense.Rows.Add(1);
                }
                rearrange_slno();

            }
        }


        private void dgv_expense_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgv_expense.CurrentCell.ColumnIndex == Eamountno)
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

        private void dgv_expense_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgv_expense.CurrentCell.ColumnIndex == Eamountno )
            {
                e.Control.KeyPress -= new KeyPressEventHandler(dgv_expense_KeyPress);
                System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(dgv_expense_KeyPress);
                }
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            FillExpence();
        }

        private void btn_searchbox_close_Click(object sender, EventArgs e)
        {
            pnl_serachBox.Visible = false;
        }

       private void cmb_searchtype_SelectedIndexChanged(object sender, EventArgs e)
       {

            Control controlToRemove = tlp_serachtype_controls.GetControlFromPosition(2, 0);
            tlp_serachtype_controls.Controls.Remove(controlToRemove);
             controlToRemove = tlp_serachtype_controls.GetControlFromPosition(2, 1);
            tlp_serachtype_controls.Controls.Remove(controlToRemove);
            lblserach_reason.Visible = false;
            cmb_search_reason.Visible = false;
            lbl_search_amount.Visible = false;
            txtsearch_amount.Visible = false;
            lblserach_employee.Visible = false;
            cmb_search_Employee.Visible = false;
            lblserach_payment.Visible = false;
            cmb_search_payment.Visible = false;
            if (cmb_searchtype.SelectedIndex == 0)
            {
                lblserach_reason.Visible = true;
                cmb_search_reason.Visible = true;

                tlp_serachtype_controls.Controls.Add(lblserach_reason, 2, 0);
                tlp_serachtype_controls.Controls.Add(cmb_search_reason, 2, 1);
                lblserach_reason.Dock = DockStyle.Fill;
                cmb_search_reason.Dock = DockStyle.Fill;
            }
            else if (cmb_searchtype.SelectedIndex == 1)
            {
                lbl_search_amount.Visible = true;
                txtsearch_amount.Visible = true;

                tlp_serachtype_controls.Controls.Add(lbl_search_amount, 2, 0);
                tlp_serachtype_controls.Controls.Add(txtsearch_amount, 2, 1);
                lbl_search_amount.Dock = DockStyle.Fill;
                txtsearch_amount.Dock = DockStyle.Fill;
            }
            else if (cmb_searchtype.SelectedIndex == 2)
            {
                lblserach_employee.Visible = true;
                cmb_search_Employee.Visible = true;

                tlp_serachtype_controls.Controls.Add(lblserach_employee, 2, 0);
                tlp_serachtype_controls.Controls.Add(cmb_search_Employee, 2, 1);
                lblserach_employee.Dock = DockStyle.Fill;
                cmb_search_Employee.Dock = DockStyle.Fill;
            }
            else if (cmb_searchtype.SelectedIndex == 3)
            {
                lblserach_payment.Visible = true;
                cmb_search_payment.Visible = true;
                tlp_serachtype_controls.Controls.Add(lblserach_payment, 2, 0);
                tlp_serachtype_controls.Controls.Add(cmb_search_payment, 2, 1);
                lblserach_payment.Dock = DockStyle.Fill;
                cmb_search_payment.Dock = DockStyle.Fill;
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                if(cmb_searchtype.SelectedIndex ==-1)
                {
                    CMessageBox.Show("Please Select the Serach Type", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmb_searchtype.Focus();
                    return;
                }
                pnl_serachBox.Visible = false;
                BllSearch<Dmnexpence> bllSearch = new BllSearch<Dmnexpence>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblExpence";
                DateTime sfrom = dtp_from_date.Value.Date + dtp_from_time.Value.TimeOfDay;
                DateTime sto = dtp_to_date.Value.Date + dtp_to_time.Value.TimeOfDay;

                if (cmb_searchtype.SelectedIndex == 0)
                {
                    objSearch.WhereNdGroupBy = " WHERE Status = 1 AND reason = "+ cmb_search_reason.SelectedIndex + " AND CreatedOn BETWEEN CONVERT(datetime, '" + sfrom+ "',105) AND CONVERT(datetime,'" + sto + "',105)";
                }
                else if (cmb_searchtype.SelectedIndex == 1)
                {
                    objSearch.WhereNdGroupBy = $" WHERE Status = 1 AND Amount = " + txtsearch_amount.Text + " AND CreatedOn BETWEEN CONVERT(datetime, '" + sfrom + "',105) AND CONVERT(datetime,'" + sto + "',105)";
                }
                else if (cmb_searchtype.SelectedIndex == 2)
                {
                    objSearch.WhereNdGroupBy = $" WHERE Status = 1 AND Empolyeeid = " + cmb_search_Employee.SelectedIndex + " AND CreatedOn BETWEEN CONVERT(datetime, '" + sfrom + "',105) AND CONVERT(datetime,'" + sto + "',105)";
                }
                else if (cmb_searchtype.SelectedIndex == 3)
                {
                    objSearch.WhereNdGroupBy = $" WHERE Status = 1 AND paymentid =" + cmb_search_payment.SelectedIndex + " AND CreatedOn BETWEEN CONVERT(datetime, '" + sfrom + "',105) AND CONVERT(datetime,'" + sto + "',105)";
                }

                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);

                if (itemSource.Count > 0)
                {
                    dgv_expense.Rows.Clear();
                    dgv_expense.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_expense.Rows[i].Cells[Eslno].Value = i + 1;
                        dgv_expense.Rows[i].Cells[Eepidno].Value = itemSource[i].Eid;
                        dgv_expense.Rows[i].Cells[Ereasonno].Value = Convert.ToInt64(itemSource[i].reason);
                        dgv_expense.Rows[i].Cells[Eamountno].Value = itemSource[i].Amount;
                        dgv_expense.Rows[i].Cells[Edecriptionno].Value = itemSource[i].description;
                        dgv_expense.Rows[i].Cells[Eemployenameno].Value = Convert.ToInt64(itemSource[i].Empolyeeid);
                        dgv_expense.Rows[i].Cells[Epaymentypeno].Value = Convert.ToInt64(itemSource[i].paymentid);
                        dgv_expense.Rows[i].Cells[Estatusno].Value = Convert.ToInt32(itemSource[i].Status);
                    }
                }
                else
                {
                    //dgv_expense.Rows.Add(1);
                    //rearrange_slno();
                    CMessageBox.Show("No match Found", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dgv_expense_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:

                    if (pnl_serachBox.Visible)
                        pnl_serachBox.Visible = false;
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
    }
}
