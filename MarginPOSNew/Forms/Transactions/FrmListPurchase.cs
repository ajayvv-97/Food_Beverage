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
    public partial class FrmListPurchase : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        public string _retValue { get; set; }
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId =  Convert.ToInt64(FrmDashboard.BranchId);
        long UserId =  Convert.ToInt64(FrmDashboard.UserId);
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
        #endregion
        #region Functions
        public FrmListPurchase()
        {
            InitializeComponent();
        }
        public void fillGridView()
        {
            ToTalAmnt = 0;
            TotalQty = 0;
            dgvLstPurchaseMaster.Columns.Clear();
            dgvLstPurchaseMaster.DataSource = null;
            FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.ColumnHeaders = "MasterId,EntryNo., Entry Date, Doc.No., Ledger, Tot.Qty, Tot. Amt, Tot. Disc., Tot. Tax, Net Amt";
            dmnSearch.Select = "P.PurchaseMasterId as Id,P.EntryNo as Field1,P.EntryDate as Field2,P.DocumentNo as Field3,L.LedgerName as Field4," +
                 "P.TotalQuantity as Field5,P.TotalAmount as Field6,P.TotalDiscount as Field7,P.TotalTax as Field8,P.TotalNetAmount as Field9";
            dmnSearch.Table = " tblPurchaseMaster P LEFT JOIN tblAccountLedger L  ON P.LedgerId = L.LedgerId";
            dmnSearch.WhereNdGroupBy = " WHERE P.EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                        " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND P.Status = 0 And P.BranchId = " + BranchId;
            //dmnSearch.WhereNdGroupBy = " WHERE P.EntryDate BETWEEN convert(varchar(10), cast('" + mdtpFrom.Value + "' as date), 121) AND" +
            //            " convert(varchar(10), cast('" + mdtpTo.Value + "' as date), 121) AND P.Status = 0 And P.BranchId = " + BranchId ;
            dmnSearch.OrderBy = "Order By P.EntryNo DESC ";
            var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
            foreach (DataGridViewColumn col in gridSource.Columns)
            {
                dgvLstPurchaseMaster.Columns.Add(col.Clone() as DataGridViewColumn);
            }
            dgvLstPurchaseMaster.DataSource = gridSource.DataSource;
            dgvLstPurchaseMaster.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstPurchaseMaster.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstPurchaseMaster.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstPurchaseMaster.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstPurchaseMaster.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvLstPurchaseMaster.Columns[1].Width = 80;
            dgvLstPurchaseMaster.Columns[2].Width = 160;
            dgvLstPurchaseMaster.Columns[4].Width = 200;
            dgvLstPurchaseMaster.Columns[5].Width = 80;
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
        private void FrmListPurchase_Load(object sender, EventArgs e)
        {
            try
            {
                FillParty();
                fillGridView();
                if (dgvLstPurchaseMaster.Rows.Count > 0)
                {
                    ActiveControl = dgvLstPurchaseMaster;
                    dgvLstPurchaseMaster.Rows[0].Selected = true;
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

        private void DgvLstPurchaseMaster_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                long purchaseId = Convert.ToInt64(dgvLstPurchaseMaster.CurrentRow.Cells[0].Value);
                dgvPurchaseDetails.Columns.Clear();
                dgvPurchaseDetails.DataSource = null;
                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.ColumnHeaders = "PurchaseDetailId, Name, Barcode, Qty, Pur_Rate, Amt, Disc. Amt, Gross Amt, Net Amnt, Cost, Margin";
                dmnSearch.Select = "P.PurchaseDetailId as Id,I.ItemName as Field1,I.BarCode as Field2,P.Quantity as Field3,P.PurchaseRate as Field4," +
                     "P.Amount as Field5, P.DiscountAmount as Field6, P.GrossAmount as Field7, P.NetAmount as Field8, Cost as Field9,Margin as Field10";
                dmnSearch.Table = " tblPurchaseDetails P LEFT JOIN tblItem I  ON P.ItemId = I.ItemId";
                dmnSearch.WhereNdGroupBy = " WHERE P.PurchaseMasterId = " + purchaseId + " AND P.Status = 0 And P.BranchId = " + BranchId;
                dmnSearch.OrderBy = "Order By P.PurchaseDetailId ASC ";
                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvPurchaseDetails.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvPurchaseDetails.DataSource = gridSource.DataSource;
                dgvPurchaseDetails.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPurchaseDetails.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPurchaseDetails.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPurchaseDetails.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPurchaseDetails.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPurchaseDetails.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPurchaseDetails.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPurchaseDetails.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvPurchaseDetails.Columns[1].Width = 150;
                dgvPurchaseDetails.Columns[3].Width = 80;

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
                dgvLstPurchaseMaster.Columns.Clear();
                dgvLstPurchaseMaster.DataSource = null;
                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                DateTime fromdate = Convert.ToDateTime(mdtpFrom.Text.ToString());
                DateTime todate = Convert.ToDateTime(mdtpTo.Text.ToString());
                dmnSearch.ColumnHeaders = "MasterId,EntryNo., Entry Date, Doc.No., Ledger, Tot.Qty, Tot. Amt, Tot. Disc., Tot. Tax, Net Amt";
                dmnSearch.Select = "P.PurchaseMasterId as Id,P.EntryNo as Field1,P.EntryDate as Field2,P.DocumentNo as Field3,L.LedgerName as Field4," +
                     "P.TotalQuantity as Field5, P.TotalAmount as Field6, P.TotalDiscount as Field7, P.TotalTax as Field8, P.TotalNetAmount as Field9";
                dmnSearch.Table = " tblPurchaseMaster P LEFT JOIN tblAccountLedger L  ON P.LedgerId = L.LedgerId";
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
                    dgvLstPurchaseMaster.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvLstPurchaseMaster.DataSource = gridSource.DataSource;
                dgvLstPurchaseMaster.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLstPurchaseMaster.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLstPurchaseMaster.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLstPurchaseMaster.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLstPurchaseMaster.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvLstPurchaseMaster.Columns[1].Width = 80;
                dgvLstPurchaseMaster.Columns[2].Width = 160;
                dgvLstPurchaseMaster.Columns[4].Width = 200;
                dgvLstPurchaseMaster.Columns[5].Width = 80;
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
                FrmListPurchase_Load(sender,e);
                dgvPurchaseDetails.DataSource = null;
                dgvPurchaseDetails.Refresh();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void DgvLstPurchaseMaster_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvLstPurchaseMaster.Rows.Count > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this._retValue = Convert.ToString(dgvLstPurchaseMaster.CurrentRow.Cells[0].Value);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }

        private void FrmListPurchase_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode==Keys.Escape)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }

        }

        private void DgvLstPurchaseMaster_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvLstPurchaseMaster.Rows.Count > 0)
                    {
                        this.DialogResult = DialogResult.OK;
                        this._retValue = Convert.ToString(dgvLstPurchaseMaster.CurrentRow.Cells[0].Value);
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

        private void MbtnSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dgvLstPurchaseMaster.Focus();
                    if (dgvLstPurchaseMaster.Rows.Count > 0)
                    {
                        dgvLstPurchaseMaster.Rows[0].Selected = true;
                    }

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
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

        private void dgvLstPurchaseMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void dgvPurchaseDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
