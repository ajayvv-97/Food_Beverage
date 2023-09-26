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
    public partial class FrmListOutward : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        public string _retValue { get; set; }
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = Convert.ToInt64(FrmDashboard.BranchId);
        long UserId = Convert.ToInt64(FrmDashboard.UserId);
        decimal ToTalAmnt = 0;
        decimal TotalQty = 0;
        #endregion
        #region Combobox Filling
        public void FillParty()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();

            objSearch.Select = "LedgerId,LedgerName as value";
            objSearch.Table = " tblAccountLedger L LEFT JOIN tblAccountGroup Ac  ON L.AccountGroupId = Ac.AccountGroupId ";
            objSearch.WhereNdGroupBy = " WHERE (Ac.AccountGroupName = 'Sundry Creditors') AND L.Status = 0 And L.BranchId = " + BranchId;
            objSearch.OrderBy = "order by value asc";


            //objSearch.Select = "LedgerId,LedgerName";
            //objSearch.Table = " tblAccountLedger ";
            //objSearch.WhereNdGroupBy = " Where Status = 0 a";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbParty.DataSource = gridSource;
            mcmbParty.ValueMember = "Id";
            mcmbParty.DisplayMember = "Value";
        }
        public FrmListOutward()
        {
            InitializeComponent();
           // FillParty();
            //fillGridView();

        }

        public void fillGridView()
        {
            ToTalAmnt = 0;
            TotalQty = 0;
            dgvLstOutward.Columns.Clear();
            dgvLstOutward.DataSource = null;
            FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
            DmnSearch dmnSearch = new DmnSearch();

            dmnSearch.ColumnHeaders = "OutwardId,EntryNo., Entry Date, Doc.No., Ledger, Tot.Qty, Tot. Amt, Tot. Disc., Tot. Tax, Net Amt";
            dmnSearch.Select = "P.OutwardId as Id,P.EntryNo as Field1,P.EntryDate as Field2,P.DocumentNo as Field3,L.LedgerName as Field4," +
                 "P.TotalQuantity as Field5,P.TotalAmount as Field6,P.TotalDiscount as Field7,P.TotalTax as Field8,P.TotalNetAmount as Field9";
            dmnSearch.Table = " tblOutward P LEFT JOIN tblAccountLedger L  ON P.LedgerId = L.LedgerId";
            dmnSearch.WhereNdGroupBy = " WHERE P.EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                        " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND P.Status = 0 And P.BranchId = " + BranchId;
            //dmnSearch.WhereNdGroupBy = " WHERE P.EntryDate BETWEEN convert(varchar(10), cast('" + mdtpFrom.Value + "' as date), 121) AND" +
            //            " convert(varchar(10), cast('" + mdtpTo.Value + "' as date), 121) AND P.Status = 0 And P.BranchId = " + BranchId ;
            dmnSearch.OrderBy = "Order By P.EntryNo DESC ";
            var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
            foreach (DataGridViewColumn col in gridSource.Columns)
            {
                dgvLstOutward.Columns.Add(col.Clone() as DataGridViewColumn);
            }
            dgvLstOutward.DataSource = gridSource.DataSource;
            dgvLstOutward.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstOutward.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstOutward.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstOutward.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstOutward.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvLstOutward.Columns[1].Width = 80;
            dgvLstOutward.Columns[2].Width = 160;
            dgvLstOutward.Columns[4].Width = 200;
            dgvLstOutward.Columns[5].Width = 80;
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
        private void FrmListOutward_Load(object sender, EventArgs e)
        {
            try
            {
                FillParty();
                fillGridView();
                MbtnSearch_Click(null, null);
                if (dgvLstOutward.Rows.Count > 0)
                {
                    ActiveControl = dgvLstOutward;
                    dgvLstOutward.Rows[0].Selected = true;
                }
                else
                {
                    ActiveControl = mdtpFrom;
                }
                mdtpFrom.Value = DateTime.Now;
                mdtpTo.Value = DateTime.Now;
                mcmbParty.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void dgvLstOutward_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                long purchaseId = Convert.ToInt64(dgvLstOutward.CurrentRow.Cells[0].Value);
                dgvOutwardDetails.Columns.Clear();
                dgvOutwardDetails.DataSource = null;
                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.ColumnHeaders = "OutwardDetailsId, Name, Barcode, Qty, Pur_Rate, Amt, Disc. Amt, Gross Amt, Net Amnt, Cost, Margin";
                dmnSearch.Select = "P.OutwardDetailsId as Id,I.ItemName as Field1,I.BarCode as Field2,P.Quantity as Field3,P.PurchaseRate as Field4," +
                     "P.Amount as Field5, P.DiscountAmount as Field6, P.GrossAmount as Field7, P.NetAmount as Field8, Cost as Field9,Margin as Field10";
                dmnSearch.Table = " tblOutDetails P LEFT JOIN tblItem I  ON P.ItemId = I.ItemId";
                dmnSearch.WhereNdGroupBy = " WHERE P.OutwardId = " + purchaseId + " AND P.Status = 0 And P.BranchId = " + BranchId;
                dmnSearch.OrderBy = "Order By P.OutwardDetailsId ASC ";
                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvOutwardDetails.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvOutwardDetails.DataSource = gridSource.DataSource;
                dgvOutwardDetails.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvOutwardDetails.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvOutwardDetails.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvOutwardDetails.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvOutwardDetails.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvOutwardDetails.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvOutwardDetails.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvOutwardDetails.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvOutwardDetails.Columns[1].Width = 150;
                dgvOutwardDetails.Columns[3].Width = 80;

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }


        private void MbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                mdtpFrom.Focus();
                dgvLstOutward.Columns.Clear();
                dgvLstOutward.DataSource = null;
                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                DateTime fromdate = Convert.ToDateTime(mdtpFrom.Text.ToString());
                DateTime todate = Convert.ToDateTime(mdtpTo.Text.ToString());
                dmnSearch.ColumnHeaders = "OutwardId,EntryNo., Entry Date, Doc.No., Ledger, Tot.Qty, Tot. Amt, Tot. Disc., Tot. Tax, Net Amt";
                dmnSearch.Select = "P.OutwardId as Id,P.EntryNo as Field1,P.EntryDate as Field2,P.DocumentNo as Field3,L.LedgerName as Field4," +
                                   "P.TotalQuantity as Field5, P.TotalAmount as Field6, P.TotalDiscount as Field7, P.TotalTax as Field8, P.TotalNetAmount as Field9";
                dmnSearch.Table = " tblOutward P LEFT JOIN tblAccountLedger L  ON P.LedgerId = L.LedgerId";
                if (mcmbParty.SelectedIndex == 0)
                {
                    dmnSearch.WhereNdGroupBy = " WHERE P.EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                        " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND P.LedgerId = " + mcmbParty.SelectedValue + " and P.Status = 0 And P.BranchId = " + BranchId;
                }
                else
                {
                    dmnSearch.WhereNdGroupBy = " WHERE P.EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                         " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND P.Status = 0 And P.BranchId = " + BranchId;
                }
                dmnSearch.OrderBy = "Order By P.EntryNo DESC ";
                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvLstOutward.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvLstOutward.DataSource = gridSource.DataSource;
                dgvLstOutward.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLstOutward.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLstOutward.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLstOutward.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLstOutward.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvLstOutward.Columns[1].Width = 80;
                dgvLstOutward.Columns[2].Width = 160;
                dgvLstOutward.Columns[4].Width = 200;
                dgvLstOutward.Columns[5].Width = 80;
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
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }

        private void MbtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                mdtpFrom.Value = DateTime.Now;
                mdtpTo.Value = DateTime.Now;
                mcmbParty.SelectedIndex = 0;
                //fillGridView();
                FrmListOutward_Load(sender, e);
                dgvOutwardDetails.DataSource = null;
                dgvOutwardDetails.Refresh();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void dgvLstOutward_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvLstOutward.Rows.Count > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this._retValue = Convert.ToString(dgvLstOutward.CurrentRow.Cells[0].Value);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }

        private void FrmListOutward_KeyUp(object sender, KeyEventArgs e)
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

        private void dgvLstOutward_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void MbtnSearch_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void MdtpFrom_KeyUp(object sender, KeyEventArgs e)
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

        private void MdtpTo_KeyUp(object sender, KeyEventArgs e)
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

        private void McmbParty_KeyUp(object sender, KeyEventArgs e)
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

        private void mdtpFrom_ValueChanged(object sender, EventArgs e)
        {
            MbtnSearch_Click(null, null);
        }

        private void mdtpTo_ValueChanged(object sender, EventArgs e)
        {
            MbtnSearch_Click(null, null);
        }

        private void dgvLstOutward_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mcmbParty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void mlblName_Click(object sender, EventArgs e)
        {

        }

        private void hlblHeader_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

        }

        private void dgvInwardDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mtxtTotalAmt_Click(object sender, EventArgs e)
        {

        }

        private void mtxtTotalQty_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel4_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel3_Click(object sender, EventArgs e)
        {

        }

        private void mlblTotRecords_Click(object sender, EventArgs e)
        {

        }
    }
}

