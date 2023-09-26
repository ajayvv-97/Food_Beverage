using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarginPOSNew.Forms.Transactions
{
    public partial class FrmListPurchaseReturn : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        public string _retValue { get; set; }
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = Convert.ToInt64(FrmDashboard.BranchId);
        long UserId = Convert.ToInt64(FrmDashboard.UserId);
        decimal ToTalAmnt = 0;
        decimal TotalQty = 0;
        #endregion
        #region Functions
        public FrmListPurchaseReturn()
        {
            InitializeComponent();
        }
        #region Combobox Filling
        public void FillParty()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "LedgerId,LedgerName";
            objSearch.Table = " tblAccountLedger ";
            objSearch.WhereNdGroupBy = " Where Status = 0";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbParty.DataSource = gridSource;
            mcmbParty.ValueMember = "Id";
            mcmbParty.DisplayMember = "Value";
        }
        #endregion
        public void fillGridView()
        {
            ToTalAmnt = 0;
            TotalQty = 0;
            dgvLstPurchaseReturnMaster.Columns.Clear();
            dgvLstPurchaseReturnMaster.DataSource = null;
            FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.ColumnHeaders = "MasterId,EntryNo., Entry Date, Doc.No., Ledger, Tot.Qty, Tot. Amt, Tot. Disc., Tot. Tax, Net Amt";
            dmnSearch.Select = "P.PurchaseReturnMasterId as Id,P.EntryNo as Field1,P.EntryDate as Field2,P.DocumentNo as Field3,L.LedgerName as Field4," +
                 "P.TotalQuantity as Field5,P.TotalAmount as Field6,P.TotalDiscount as Field7,P.TotalTax as Field8,P.TotalNetAmount as Field9";
            dmnSearch.Table = " tblPurchaseReturnMaster P LEFT JOIN tblAccountLedger L  ON P.LedgerId = L.LedgerId";
            dmnSearch.WhereNdGroupBy = " WHERE P.EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                        " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND P.Status = 0 And P.BranchId = " + BranchId;
            //dmnSearch.WhereNdGroupBy = " WHERE P.EntryDate BETWEEN convert(varchar(10), cast('" + mdtpFrom.Value + "' as date), 121) AND" +
            //            " convert(varchar(10), cast('" + mdtpTo.Value + "' as date), 121) AND P.Status = 0 And P.BranchId = " + BranchId ;
            dmnSearch.OrderBy = "Order By P.EntryNo DESC ";
            var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
            foreach (DataGridViewColumn col in gridSource.Columns)
            {
                dgvLstPurchaseReturnMaster.Columns.Add(col.Clone() as DataGridViewColumn);
            }
            dgvLstPurchaseReturnMaster.DataSource = gridSource.DataSource;
            dgvLstPurchaseReturnMaster.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstPurchaseReturnMaster.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstPurchaseReturnMaster.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstPurchaseReturnMaster.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstPurchaseReturnMaster.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvLstPurchaseReturnMaster.Columns[1].Width = 80;
            dgvLstPurchaseReturnMaster.Columns[2].Width = 160;
            dgvLstPurchaseReturnMaster.Columns[4].Width = 200;
            dgvLstPurchaseReturnMaster.Columns[5].Width = 80;
            ToTalAmnt = 0;
            TotalQty = 0;
            int rowCount = gridSource.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                ToTalAmnt += Convert.ToDecimal(gridSource[9, i].Value);
                TotalQty += Convert.ToDecimal(gridSource[5, i].Value);
            }
            mlblTotRecords.Text = rowCount + " Records";
            mtxtTotalQty.Text = Convert.ToString(TotalQty);
            mtxtTotalAmt.Text = Convert.ToString(ToTalAmnt);
        }
        #endregion

        #region Events
        private void FrmListPurchaseReturn_Load(object sender, EventArgs e)
        {
            try
            {
                FillParty();
                fillGridView();
                if (dgvLstPurchaseReturnMaster.Rows.Count > 0)
                {
                    ActiveControl = dgvLstPurchaseReturnMaster;
                    dgvLstPurchaseReturnMaster.Rows[0].Selected = true;
                }
                else
                {
                    ActiveControl = mdtpFrom;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void FrmListPurchaseReturn_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }
        private void dgvLstPurchaseReturnMaster_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                long purchaseReturnId = Convert.ToInt64(dgvLstPurchaseReturnMaster.CurrentRow.Cells[0].Value);
                dgvPurchaseReturnDetails.Columns.Clear();
                dgvPurchaseReturnDetails.DataSource = null;
                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.ColumnHeaders = "PurchaseReturnDetailsId, Name, BarCode, Qty, PRate, Amt, Disc. Amt, Gross Amt, Net Amnt, Cost, Margin";
                dmnSearch.Select = "P.PurchaseReturnDetailsId as Id,I.ItemName as Field1,I.BarCode as Field2,P.Quantity as Field3,P.PurchaseRate as Field4," +
                     "P.Amount as Field5, P.DiscountAmount as Field6, P.GrossAmount as Field7, P.NetAmount as Field8, Cost as Field9,Margin as Field10";
                dmnSearch.Table = " tblPurchaseReturnDetails P LEFT JOIN tblItem I  ON P.ItemId = I.ItemId";
                dmnSearch.WhereNdGroupBy = " WHERE P.PurchaseReturnMasterId = " + purchaseReturnId + " AND P.Status = 0 And P.BranchId = " + BranchId;
                dmnSearch.OrderBy = "Order By P.PurchaseReturnDetailsId ASC ";
                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvPurchaseReturnDetails.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvPurchaseReturnDetails.DataSource = gridSource.DataSource;
                dgvPurchaseReturnDetails.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPurchaseReturnDetails.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPurchaseReturnDetails.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPurchaseReturnDetails.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPurchaseReturnDetails.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPurchaseReturnDetails.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPurchaseReturnDetails.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPurchaseReturnDetails.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvPurchaseReturnDetails.Columns[1].Width = 150;
                dgvPurchaseReturnDetails.Columns[3].Width = 80;

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void dgvLstPurchaseReturnMaster_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvLstPurchaseReturnMaster.Rows.Count > 0)
                    {
                        this.DialogResult = DialogResult.OK;
                        this._retValue = Convert.ToString(dgvLstPurchaseReturnMaster.CurrentRow.Cells[0].Value);
                        this.Close();
                    }
                }
                else if (e.KeyCode == Keys.Tab)
                {
                    mdtpFrom.Focus();

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                mdtpFrom.Focus();
                dgvLstPurchaseReturnMaster.Columns.Clear();
                dgvLstPurchaseReturnMaster.DataSource = null;
                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                DateTime fromdate = Convert.ToDateTime(mdtpFrom.Text.ToString());
                DateTime todate = Convert.ToDateTime(mdtpTo.Text.ToString());
                dmnSearch.ColumnHeaders = "MasterId,EntryNo., Entry Date, Doc.No., Ledger, Tot.Qty, Tot. Amt, Tot. Disc., Tot. Tax, Net Amt";
                dmnSearch.Select = "P.PurchaseReturnMasterId as Id,P.EntryNo as Field1,P.EntryDate as Field2,P.DocumentNo as Field3,L.LedgerName as Field4," +
                     "P.TotalQuantity as Field5, P.TotalAmount as Field6, P.TotalDiscount as Field7, P.TotalTax as Field8, P.TotalNetAmount as Field9";
                dmnSearch.Table = " tblPurchaseReturnMaster P LEFT JOIN tblAccountLedger L  ON P.LedgerId = L.LedgerId";
                if (mcmbParty.SelectedIndex == 0)
                {
                    dmnSearch.WhereNdGroupBy = " WHERE P.EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                        " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND P.Status = 0 And P.BranchId = " + BranchId;
                }
                else
                {
                    dmnSearch.WhereNdGroupBy = " WHERE P.EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                        " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND P.LedgerId = " + mcmbParty.SelectedValue + " and P.Status = 0 And P.BranchId = " + BranchId;
                }
                dmnSearch.OrderBy = "Order By P.EntryNo DESC ";
                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvLstPurchaseReturnMaster.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvLstPurchaseReturnMaster.DataSource = gridSource.DataSource;
                dgvLstPurchaseReturnMaster.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLstPurchaseReturnMaster.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLstPurchaseReturnMaster.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLstPurchaseReturnMaster.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLstPurchaseReturnMaster.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvLstPurchaseReturnMaster.Columns[1].Width = 80;
                dgvLstPurchaseReturnMaster.Columns[2].Width = 160;
                dgvLstPurchaseReturnMaster.Columns[4].Width = 200;
                dgvLstPurchaseReturnMaster.Columns[5].Width = 80;
                ToTalAmnt = 0;
                TotalQty = 0;
                int rowCount = gridSource.Rows.Count;
                for (int i = 0; i < rowCount; i++)

                {
                    ToTalAmnt += Convert.ToDecimal(gridSource[9, i].Value);
                    TotalQty += Convert.ToInt32(gridSource[5, i].Value);

                }

                mlblTotRecords.Text = rowCount + " Records";
                mtxtTotalQty.Text = Convert.ToString(TotalQty);
                mtxtTotalAmt.Text = Convert.ToString(ToTalAmnt);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mbtnSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dgvLstPurchaseReturnMaster.Focus();
                    if (dgvLstPurchaseReturnMaster.Rows.Count > 0)
                    {
                        dgvLstPurchaseReturnMaster.Rows[0].Selected = true;
                    }

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mbtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                mdtpFrom.Value = DateTime.Now;
                mdtpTo.Value = DateTime.Now;
                mcmbParty.SelectedIndex = 0;
                //fillGridView();
                FrmListPurchaseReturn_Load(sender, e);
                dgvPurchaseReturnDetails.DataSource = null;
                dgvPurchaseReturnDetails.Refresh();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void dgvLstPurchaseReturnMaster_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvLstPurchaseReturnMaster.Rows.Count > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this._retValue = Convert.ToString(dgvLstPurchaseReturnMaster.CurrentRow.Cells[0].Value);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }
        private void mdtpFrom_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    mdtpTo.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mdtpTo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    mcmbParty.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mcmbParty_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    mbtnSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        #endregion
    }
}
