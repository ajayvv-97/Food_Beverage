using Domain;
using BLL;
using System;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;
using System.Drawing;

namespace pLocker.Forms
{
    public partial class FrmHireList : Form
    {

        long BranchId = Forms.frmLogin.BranchId;//16
        const int gAccID = 0;
        const int gEntryDate = 1;
        const int gAccNo = 2;
        const int glockid = 3;
        const int glocker = 4;
        const int gAccName = 5;
        const int gdob = 6;
        const int gMobile = 7;
        const int gAccEmail = 8;
        const int ghirefor = 9;
        const int gHireDate = 10;
        const int gExpDate = 11;
        const int gRate = 12;
        const int gAmt = 13;
        const int gLockHireId = 14;



        public FrmHireList()
        {
            InitializeComponent();
        }

        private void FrmKeypad1_Load(object sender, EventArgs e)
        {
            try
            {
                ActiveControl = dtpEnrollFrom;
                cboField.DisplayMember = "Text";
                cboField.ValueMember = "Value";

                var items = new[] {
                new { Text = "-Select-", Value = "0" },
                new { Text = "A/cNo", Value = "1" },
                new { Text = "CustomerName", Value = "2" },
                 new { Text = "Mobile", Value = "3" },
                new { Text = "LockID", Value = "4" },
                new { Text = "Locker", Value = "5" },
               
                };
                cboField.DataSource = items;
                mtxtFieldValue.Text = "";
                mtxtFieldValue.Focus();
                FillGridList();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }      

        private void btnFrmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dtpEnrollFrom.Focus();               
                dgvList.DataSource = null;
                BranchId = 1;
                FillGridList();
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FillGridList()
        {
            try
            {
                BllSearch<DmnHireLocker> _bllSearch = new BllSearch<DmnHireLocker>();
                DmnSearch _objSearch = new DmnSearch();

                _objSearch.Select = "tbl.LedgerId as AccId,EntryDate,LedgerCode as AccNo,LockId,Locker,LedgerName as AccName, dob, AccPhone," +
                                    "AccEmail,HireFor,HireDate,ExpDate,Rate,Amt,tlh.LockHireID";
                _objSearch.Table = "tblLockerHire tlh left join tblAccountLedger tbl on tlh.AccID=tbl.LedgerId " +
                                   "left join tblLocker tl on tl.ID=tlh.LockID";               
                if (!string.IsNullOrWhiteSpace(mtxtFieldValue.Text))
                {
                    switch (Convert.ToInt32(cboField.SelectedValue))
                    {
                        case 1:
                            _objSearch.WhereNdGroupBy = "WHERE LedgerCode Like '%" + mtxtFieldValue.Text + "%' and EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(tlh.Status,0)=1  and tlh.BranchId=" + BranchId;
                            break;
                        case 2:
                            _objSearch.WhereNdGroupBy = "WHERE LedgerName Like '%" + mtxtFieldValue.Text + "%' and EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(tlh.Status,0)=1  and tlh.BranchId=" + BranchId;
                            break;
                        case 3:
                            _objSearch.WhereNdGroupBy = "WHERE Mobile Like '%" + mtxtFieldValue.Text + "%' and EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(tlh.Status,0)=1  and tlh.BranchId=" + BranchId;
                            break;                                           
                        case 4:
                            _objSearch.WhereNdGroupBy = "WHERE LockId Like '%" + mtxtFieldValue.Text + "%' and EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(tlh.Status,0)=1  and tlh.BranchId=" + BranchId;
                            break;
                        case 5:
                            _objSearch.WhereNdGroupBy = "WHERE Locker Like '%" + mtxtFieldValue.Text + "%' and EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(tlh.Status,0)=1  and tlh.BranchId=" + BranchId;
                            break;                      
                        default:
                            _objSearch.WhereNdGroupBy = "WHERE EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(tlh.Status,0)=1  and tlh.BranchId=" + BranchId;
                            break;
                    }
                }
                else
                    _objSearch.WhereNdGroupBy = "WHERE EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(tlh.Status,0)=1  and tlh.BranchId=" + BranchId;
                _objSearch.OrderBy = " Order by LockHireID desc";
                var gridSource = _bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource.Count > 0)
                {
                    dgvList.Rows.Clear();
                    dgvList.Rows.Add(gridSource.Count);
                    for (int i = 0; i < gridSource.Count(); i++)
                    {

                        dgvList.Rows[i].Cells[gAccID].Value = gridSource[i].AccID.ToString();

                        dgvList.Rows[i].Cells[gEntryDate].Value = gridSource[i].EntryDate.ToString();

                        if (gridSource[i].AccNo != null)
                            dgvList.Rows[i].Cells[gAccNo].Value = gridSource[i].AccNo.ToString();
                        else
                            dgvList.Rows[i].Cells[gAccNo].Value = "";

                        if (gridSource[i].LockID != null)
                            dgvList.Rows[i].Cells[glockid].Value = gridSource[i].LockID.ToString();
                        else
                            dgvList.Rows[i].Cells[glockid].Value = "";

                        if (gridSource[i].Locker != null)
                            dgvList.Rows[i].Cells[glocker].Value = gridSource[i].Locker.ToString();
                        else
                            dgvList.Rows[i].Cells[glocker].Value = "";

                        if (gridSource[i].AccName != null)
                            dgvList.Rows[i].Cells[gAccName].Value = gridSource[i].AccName.ToString();
                        else
                            dgvList.Rows[i].Cells[gAccName].Value = "";

                        if (gridSource[i].dob != null)
                            dgvList.Rows[i].Cells[gdob].Value = gridSource[i].dob.ToString();

                        else
                            dgvList.Rows[i].Cells[gdob].Value = "";

                        if (gridSource[i].AccPhone != null)
                            dgvList.Rows[i].Cells[gMobile].Value = gridSource[i].AccPhone.ToString();

                        else
                            dgvList.Rows[i].Cells[gMobile].Value = "";

                        if (gridSource[i].AccEmail != null)
                            dgvList.Rows[i].Cells[gAccEmail].Value = gridSource[i].AccEmail.ToString();

                        else
                            dgvList.Rows[i].Cells[gAccEmail].Value = "";

                        if (gridSource[i].HireFor != null)
                            dgvList.Rows[i].Cells[ghirefor].Value = gridSource[i].HireFor.ToString();

                        else
                            dgvList.Rows[i].Cells[ghirefor].Value = "";

                        if (gridSource[i].HireDate != null)
                            dgvList.Rows[i].Cells[gHireDate].Value = gridSource[i].HireDate.ToString();

                        else
                            dgvList.Rows[i].Cells[gHireDate].Value = "";


                        if (gridSource[i].ExpDate != null)
                            dgvList.Rows[i].Cells[gExpDate].Value = gridSource[i].ExpDate.ToString();

                        else
                            dgvList.Rows[i].Cells[gExpDate].Value = "";

                        if (gridSource[i].Rate != null)
                            dgvList.Rows[i].Cells[gRate].Value = gridSource[i].Rate.ToString();

                        else
                            dgvList.Rows[i].Cells[gRate].Value = "";

                        if (gridSource[i].Amt != null)
                            dgvList.Rows[i].Cells[gAmt].Value = gridSource[i].Amt.ToString();

                        else
                            dgvList.Rows[i].Cells[gAmt].Value = "";

                        if (gridSource[i].LockHireID != null)
                            dgvList.Rows[i].Cells[gLockHireId].Value = gridSource[i].LockHireID;
                        else
                            dgvList.Rows[i].Cells[gLockHireId].Value = 0;
                    }
                    lblTotLocker.Text = gridSource.Count() + " Records";
                    dgvList.Columns[gAccNo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gAccName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[glockid].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[glocker].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gdob].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gAccEmail].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[ghirefor].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gHireDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gExpDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gRate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvList.Columns[gAmt].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                   

                    dgvList.Columns[gAccNo].Width = 150;
                    dgvList.Columns[gAccName].Width = 200;
                    dgvList.Columns[glockid].Width = 80;
                    dgvList.Columns[glocker].Width = 80;
                    dgvList.Columns[gdob].Width = 100;
                    dgvList.Columns[gAccEmail].Width = 150;
                    dgvList.Columns[ghirefor].Width = 80;
                    dgvList.Columns[gHireDate].Width = 150;
                    dgvList.Columns[gExpDate].Width = 150;
                    dgvList.Columns[gRate].Width = 80;
                    dgvList.Columns[gAmt].Width = 80;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.ToString());
            }

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            dtpEnrollFrom.Value = DateTime.Now;
            dtpEnrollTo.Value = DateTime.Now;
            dgvList.DataSource = null;
            FrmHireLocker frmHireLocker = new FrmHireLocker(0, 0);
            frmHireLocker.ShowDialog();
        }

        private void dtpEnrollFrom_ValueChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null,null);
        }

        private void dtpEnrollTo_ValueChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        public void txtFlow(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
                //e.Handled = true;
            }


        }

        private void FrmHireList_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyData == Keys.Escape)
                    this.Close();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult results = MetroMessageBox.Show(this,"", "Want to Edit ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (results == DialogResult.Yes)
            {
                int hLockerID = Convert.ToInt32(dgvList.Rows[dgvList.CurrentCell.RowIndex].Cells[glockid].Value);
                FrmHireLocker frmhirelocker = new FrmHireLocker(hLockerID, 0);
                frmhirelocker.ShowDialog();
            } 
        }

        private void Minimize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //search
        private void btnSearch_MouseHover(object sender, EventArgs e)
        {
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_3));
        }

        private void btnSearch_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_2));
        }

        private void btnSearch_MouseLeave(object sender, EventArgs e)
        {
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_1));
        }

        private void btnSearch_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_1));
        }

        //new
        private void btnNew_MouseHover(object sender, EventArgs e)
        {
            this.btnNew.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.new_3));
        }

        private void btnNew_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnNew.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.new_2));
        }

        private void btnNew_MouseLeave(object sender, EventArgs e)
        {
            this.btnNew.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.New_1));
        }

        private void btnNew_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnNew.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.New_1));
        }

        private void btn_bulk_Click(object sender, EventArgs e)
        {
            Frmbulklocker frmbulk = new Frmbulklocker();
            int centerX = Screen.PrimaryScreen.Bounds.Width / 2 - frmbulk.Width / 2;
            int centerY = Screen.PrimaryScreen.Bounds.Height / 2 - frmbulk.Height / 2;
            frmbulk.StartPosition = FormStartPosition.Manual;
            frmbulk.Location = new Point(centerX, centerY);
            frmbulk.ShowDialog();
        }
    }
}

