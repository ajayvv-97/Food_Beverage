using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarginPOSNew.Forms.Transactions
{
    public partial class FrmListSales : MetroForm
    {


        #region Declarations
        public string _retValue { get; set; }
        ClsGeneral objGeneral = new ClsGeneral();
        //long BranchId = Convert.ToInt64(FrmDashboard.BranchId);
        //long UserId = Convert.ToInt64(FrmDashboard.UserId);
        //long BranchId = Convert.ToInt64(Forms.UserAccounts.FrmLogin.BranchId);
        //long UserId = Convert.ToInt64(Forms.UserAccounts.FrmLogin.UserId);
        
            //IFormatProvider culture = new CultureInfo("en-US", true);
        //  DateTime dateVal = DateTime.ParseExact(DateString, "yyyy-MM-dd", culture);

        decimal ToTalAmnt = 0;
        decimal  TotalQty = 0;
        #endregion

        #region Functions
        public void FillWholeSalesCustomer()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = " L.LedgerId, L.LedgerName";
            objSearch.Table = " tblAccountLedger L left JOIN tblAccountGroup Ac ON L.AccountGroupId=Ac.AccountGroupId ";
            objSearch.WhereNdGroupBy = " WHERE l.AccountGroupId in(19,20) and    L.Status = 0 And L.BranchId = " + Forms.UserAccounts.FrmLogin.BranchId;
            objSearch.OrderBy = "order by value";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbParty.DataSource = gridSource;
            mcmbParty.ValueMember = "Id";
            mcmbParty.DisplayMember = "Value";
        }
        public void FillCustomer()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "CustomerId , (FirstName+' '+LastName ) AS CustomerName  ";
            objSearch.Table = " tblCustomerPersonal ";
            objSearch.WhereNdGroupBy = " Where Status = 0 and BranchId=" + Forms.UserAccounts.FrmLogin.BranchId; ;
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbParty.DataSource = gridSource;
            mcmbParty.ValueMember = "Id";
            mcmbParty.DisplayMember = "Value";
        }
        public void fillGridView()
        {
            ToTalAmnt = 0;
            TotalQty = 0;
            dgvLstSalesMaster.Columns.Clear();
            dgvLstSalesMaster.DataSource = null;
            dgvSalesDetails.Columns.Clear();
            dgvSalesDetails.DataSource = null;
            FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
            DmnSearch dmnSearch = new DmnSearch();
            //DateTime fromdate = Convert.ToDateTime(mdtpFrom.Text.ToString());
            //DateTime todate = Convert.ToDateTime(mdtpTo.Text.ToString());
            //09-12-2019

            DateTime fromdate = Convert.ToDateTime(mdtpFrom.Text.ToString());
            DateTime todate = Convert.ToDateTime(mdtpTo.Text.ToString());

            string fDtstr1 = String.Format("{0:yyyy-MM-dd}", fromdate);
            string tDtstr1 = String.Format("{0:yyyy-MM-dd}", todate);




            dmnSearch.ColumnHeaders = "MasterId,Bill No., Date,  Ledger, Tot.Qty., Tot. Amt,Tot. Misc, Tot. Disc., Tot. Tax, Net Amt";
            dmnSearch.Select = "S.SalesMasterId as Id,S.EntryNo as Field1,S.EntryDate as Field2, L.LedgerName as Field3,S.TotalQuantity as Field4,S.TotalAmount as Field5, S.TotalMiscCharges as Field6,S.TotalDiscount as Field7," +
                "S.TotalTax as Field8,S.TotalNetAmount as Field9 ";
            dmnSearch.Table = "tblSalesMaster S LEFT JOIN tblAccountledger L ON S.CustomerId = L.LedgerId";
            if (mcmbParty.SelectedIndex == 0)
            {
                
                //dmnSearch.WhereNdGroupBy = " WHERE S.EntryDate BETWEEN (SELECT convert(varchar(10), convert(date,'" + mdtpFrom.Text + "' ,105), 23)) AND" +
                //                         " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND S.Status = 0 And S.BranchId = " + BranchId;

                dmnSearch.WhereNdGroupBy = " WHERE S.EntryDate BETWEEN '" + fDtstr1 + "' AND '" + tDtstr1 + "' AND S.Status = 0 And S.BranchId = " + Forms.UserAccounts.FrmLogin.BranchId;


            }

            else
            {
                dmnSearch.WhereNdGroupBy = " WHERE S.EntryDate BETWEEN (select convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "' ,105), 23)) AND" +
               " (Select convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND S.LedgerId = " + mcmbParty.SelectedValue + " and S.Status = 0 And S.BranchId = " + Forms.UserAccounts.FrmLogin.BranchId;
            }

            dmnSearch.OrderBy = "Order By S.EntryNo DESC ";
            var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);

            foreach (DataGridViewColumn col in gridSource.Columns)
            {
                dgvLstSalesMaster.Columns.Add(col.Clone() as DataGridViewColumn);
            }
            dgvLstSalesMaster.DataSource = gridSource.DataSource;
            dgvLstSalesMaster.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstSalesMaster.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstSalesMaster.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstSalesMaster.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLstSalesMaster.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvLstSalesMaster.Columns[1].Width = 80;
            dgvLstSalesMaster.Columns[2].Width = 160;
            dgvLstSalesMaster.Columns[4].Width = 80;//200

            dgvLstSalesMaster.Columns[5].Width = 130;//80  


            int rowCount = gridSource.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                ToTalAmnt += Convert.ToDecimal(gridSource[9, i].Value);
                TotalQty += Convert.ToDecimal(gridSource[4, i].Value);
            }

            mtxtTotalQty.Text = Convert.ToString(TotalQty);
            mtxtTotalAmt.Text = Convert.ToString(ToTalAmnt);
        }
        public FrmListSales()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void FrmListSales_Load(object sender, EventArgs e)
        {
            try
            {
                
                FillWholeSalesCustomer();
                fillGridView();
                if (dgvLstSalesMaster.Rows.Count > 0)
                {
                    ActiveControl = dgvLstSalesMaster;
                    dgvLstSalesMaster.Rows[0].Selected = true;
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
        private void MbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillGridView();
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
                if (dgvLstSalesMaster.Rows.Count > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this._retValue = Convert.ToString(dgvLstSalesMaster.CurrentRow.Cells[0].Value);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }
        private void DgvLstPurchaseMaster_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                long SalesMasterId = Convert.ToInt64(dgvLstSalesMaster.CurrentRow.Cells[0].Value);
                dgvSalesDetails.Columns.Clear();
                dgvSalesDetails.DataSource = null;
                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.ColumnHeaders = "SalesDetailsId, Item, BarCode, Qty, Sales Rate, MRP, Amt, Disc. Amt, Gross Amt, Tax Amt, Net Amnt";
                dmnSearch.Select = "S.SalesDetailsId as Id,I.ItemName as Field1,I.BarCode as Field2,S.Quantity as Field3,S.SalesRate as Field4," +
                     "S.MRP as Field5,S.Amount as Field6, S.DiscountAmount as Field7, S.GrossAmount as Field8, S.TaxAmount as Field9, S.NetAmount as Field10";
                dmnSearch.Table = " tblSalesDetails S LEFT JOIN tblItem I  ON S.ItemId = I.ItemId";
                dmnSearch.WhereNdGroupBy = " WHERE S.SalesMasterId = " + SalesMasterId + " AND S.Status = 0 And S.BranchId = " + Forms.UserAccounts.FrmLogin.BranchId; ;
                dmnSearch.OrderBy = "Order By S.SalesDetailsId ASC ";
                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvSalesDetails.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvSalesDetails.DataSource = gridSource.DataSource;
                dgvSalesDetails.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSalesDetails.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSalesDetails.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSalesDetails.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSalesDetails.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSalesDetails.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSalesDetails.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSalesDetails.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvSalesDetails.Columns[1].Width = 200;
                dgvSalesDetails.Columns[3].Width = 80;

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
                mdtpFrom.Focus();
                fillGridView();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void FrmListSales_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                { this.Close(); }
                //else if(e.KeyCode==Keys.Enter)
                //{
                //    objGeneral.NextConrolOnEnterPress(sender, e);
                //}
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void DgvLstSalesMaster_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvLstSalesMaster.Rows.Count > 0)
                    {
                        this.DialogResult = DialogResult.OK;
                        this._retValue = Convert.ToString(dgvLstSalesMaster.CurrentRow.Cells[0].Value);
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
        private void mbtnSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dgvLstSalesMaster.Focus();
                    if (dgvLstSalesMaster.Rows.Count > 0)
                    {
                        dgvLstSalesMaster.Rows[0].Selected = true;
                    }

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
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

        private void dgvLstSalesMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSalesDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
