using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pLocker.Forms
{
    public partial class FrmLocker : Form
    {
        long BranchId = Forms.frmLogin.BranchId;//16
        long UserId = Forms.frmLogin.UserId;//1
        long FinancialYearId = Convert.ToInt64(FrmDashBoard.FinancialYearId);//10

        long LID = 0;
        const int gLockerID = 0;
        const int gLocker = 1;
        const int gLocation =2;
        const int gRowNo = 3;
        const int gMacID = 4;
        const int gRFID = 5;
        const int gColor = 6;
        const int gPINNo = 7;
        const int gLockDesc = 8;
        const int gHrRate = 9;
        const int gDyRate = 10;
        const int gMnRate = 11;
        const int gLocationID = 12;

        public FrmLocker()
        {
            InitializeComponent();
        }
        private void RefreshForm()
        {
            LID = 0;
            txtID.Text = "0";
            txtLocker.Text = "";
            txtRowNo.Text = "";
            txtMacId.Text = "";
            txtRFID.Text = "";
            txtPinNo.Text = "";
            txtColor.Text = "";
            txtColor.BackColor = Color.White;
            txtDesc.Text = "";
            txtPerHours.Text = "0";
            txtMonth.Text = "0";
            txtPerDay.Text = "0";
            cboLocation.SelectedValue = 0;
            FillLocation();
            FillGridList();
        }
        public void FillLocation()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();

            objSearch.Select = "LocationId,LocationName ";
            objSearch.Table = " tblLockerLocation";
            objSearch.WhereNdGroupBy = "where BranchId="+BranchId;
            objSearch.OrderBy = "order by value";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            cboLocation.DataSource = gridSource;
            cboLocation.ValueMember = "Id";
            cboLocation.DisplayMember = "Value";
            cboLocation.SelectedIndex = 0;

        }

        private void FrmLocker_Load(object sender, EventArgs e)
        {
            pnlLocker.Visible = false;

            cboField.DisplayMember = "Text";
            cboField.ValueMember = "Value";

            var items = new[] {
                new { Text = "-Select-", Value = "0" },
                new { Text = "Locker", Value = "1" },
                new { Text = "MacID", Value = "2" },
                new { Text = "RFID", Value = "3" },
                new { Text = "PIN No", Value = "4" },
                new { Text = "Rowno", Value = "5" },
                new { Text = "Color", Value = "6" },
                new { Text = "Hour Rate", Value = "7" },
                new { Text = "Day Rate", Value = "8" },
                new { Text = "Month Rate", Value = "9" },
                new { Text = "Location", Value = "10" }

            };
            cboField.DataSource = items;
            mtxtFieldValue.Text = "";
            mtxtFieldValue.Focus();

            RefreshForm();

        }
        private void FillGridList()
        {
            try
            {
                BllSearch<DmnLocker> _bllSearch = new BllSearch<DmnLocker>();
                DmnSearch _objSearch = new DmnSearch();

                _objSearch.Select = "dbo.tblLocker.*, isnull(dbo.tblLockerLocation.LocationName,'')";
                _objSearch.Table = " dbo.tblLocker left JOIN dbo.tblLockerLocation ON dbo.tblLocker.LocationID = dbo.tblLockerLocation.LocationId";
                if (!string.IsNullOrWhiteSpace(mtxtFieldValue.Text) )
                {
                    switch (Convert.ToInt32(cboField.SelectedValue))
                    {
                        case 1:
                            _objSearch.WhereNdGroupBy = "where dbo.tblLocker.Locker Like '%" + mtxtFieldValue.Text + "%' and and isnull(dbo.tblLocker.status,0)=0 and dbo.tblLocker.BranchId=" + BranchId;
                            break;
                        case 2:
                            _objSearch.WhereNdGroupBy = "where dbo.tblLocker.MacID Like '%" + mtxtFieldValue.Text + "%' and and isnull(dbo.tblLocker.status,0)=0 and dbo.tblLocker.BranchId=" + BranchId;
                            break;
                        case 3:
                            _objSearch.WhereNdGroupBy = "where dbo.tblLocker.RFID Like '%" + mtxtFieldValue.Text + "%' and and isnull(dbo.tblLocker.status,0)=0 and dbo.tblLocker.BranchId=" + BranchId;
                            break;
                        case 4:
                            _objSearch.WhereNdGroupBy = "where dbo.tblLocker.PINno Like '%" + mtxtFieldValue.Text + "%' and isnull(status,0) and BranchId=" + BranchId;
                            break;
                        case 5:
                            _objSearch.WhereNdGroupBy = "where dbo.tblLocker.LockerRowno Like '%" + mtxtFieldValue.Text + "%' and isnull(dbo.tblLocker.status,0)=0 and dbo.tblLocker.BranchId=" + BranchId;
                            break;
                        case 6:
                            _objSearch.WhereNdGroupBy = "where dbo.tblLocker.LockerColor Like '%" + mtxtFieldValue.Text + "%' and and isnull(dbo.tblLocker.status,0)=0 and dbo.tblLocker.BranchId=" + BranchId;
                            break;
                        case 7:
                            _objSearch.WhereNdGroupBy = "where dbo.tblLocker.HrRate Like '%" + mtxtFieldValue.Text + "%' and isnull(dbo.tblLocker.status,0)=0 and dbo.tblLocker.BranchId=" + BranchId;
                            break;
                        case 8:
                            _objSearch.WhereNdGroupBy = "where dbo.tblLocker.DyRate Like '%" + mtxtFieldValue.Text + "%' and and isnull(dbo.tblLocker.status,0)=0 and dbo.tblLocker.BranchId=" + BranchId;
                            break;
                        case 9:
                            _objSearch.WhereNdGroupBy = "where dbo.tblLocker.MnRate Like '%" + mtxtFieldValue.Text + "%' and and isnull(dbo.tblLocker.status,0)=0 and dbo.tblLocker.BranchId=" + BranchId;
                            break;

                        case 10:
                            _objSearch.WhereNdGroupBy = "where dbo.tblLockerLocation.LocationName Like '%" + mtxtFieldValue.Text + "%' and isnull(dbo.tblLocker.status,0)=0 and dbo.tblLocker.BranchId=" + BranchId;
                            break;
                        default:
                            _objSearch.WhereNdGroupBy = "where isnull(dbo.tblLocker.status,0)=0 and dbo.tblLocker.BranchId=" + BranchId;
                            break;

                    }
                }


                _objSearch.OrderBy = "";
                var gridSource = _bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource.Count > 0)
                {

                    dgvList.Rows.Clear();
                    dgvList.Rows.Add(gridSource.Count);
                    for (int i = 0; i < gridSource.Count(); i++)
                    {

                        dgvList.Rows[i].Cells[gLockerID].Value = gridSource[i].ID.ToString();
                        dgvList.Rows[i].Cells[gLocker].Value = gridSource[i].Locker.ToString();
                        if (gridSource[i].LocationDesc != null)
                            dgvList.Rows[i].Cells[gLocation].Value = gridSource[i].LocationDesc.ToString();
                        else
                            dgvList.Rows[i].Cells[gLocation].Value = "";
                        if (gridSource[i].LockerRowno != null)
                            dgvList.Rows[i].Cells[gRowNo].Value = gridSource[i].LockerRowno.ToString();
                        else
                            dgvList.Rows[i].Cells[gRowNo].Value = "";

                        if (gridSource[i].MacID  != null)
                            dgvList.Rows[i].Cells[gMacID].Value = gridSource[i].MacID.ToString();

                        else
                            dgvList.Rows[i].Cells[gMacID].Value = "";


                        if (gridSource[i].RFID != null)
                            dgvList.Rows[i].Cells[gRFID].Value = gridSource[i].RFID.ToString();

                        else
                            dgvList.Rows[i].Cells[gRFID].Value = "";


                        if (gridSource[i].LockerColor != null)
                            dgvList.Rows[i].Cells[gColor].Value = gridSource[i].LockerColor.ToString();

                        else
                            dgvList.Rows[i].Cells[gColor].Value = "";

                        if (gridSource[i].PinNo != null)
                            dgvList.Rows[i].Cells[gPINNo].Value = gridSource[i].PinNo.ToString();

                        else
                            dgvList.Rows[i].Cells[gPINNo].Value = "";

                        if (gridSource[i].LockDesc != null)
                            dgvList.Rows[i].Cells[gLockDesc].Value = gridSource[i].LockDesc.ToString();

                        else
                            dgvList.Rows[i].Cells[gLockDesc].Value = "";

                        dgvList.Rows[i].Cells[gHrRate].Value = gridSource[i].HrRate.ToString();
                        dgvList.Rows[i].Cells[gDyRate].Value = gridSource[i].DyRate.ToString();
                        dgvList.Rows[i].Cells[gMnRate].Value = gridSource[i].MnRate.ToString();
                        if (gridSource[i].LocationID != null)
                            dgvList.Rows[i].Cells[gLocationID].Value = Convert.ToString(gridSource[i].LocationID);
                        else
                            dgvList.Rows[i].Cells[gLocationID].Value = "";
                    }
                    lblTotLocker.Text = gridSource.Count() + " Records";
                    dgvList.Columns[gHrRate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvList.Columns[gDyRate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvList.Columns[gMnRate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString());

            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                DmnLocker dmnLocker = new DmnLocker();
                BllLocker bllLocker = new BllLocker();
                dmnLocker.ID = Convert.ToInt64(txtID.Text);
                dmnLocker.Locker = Convert.ToString(txtLocker.Text);
                dmnLocker.LocationID = Convert.ToInt32(cboLocation.SelectedValue);
                dmnLocker.LockerRowno= Convert.ToString(txtRowNo.Text);
                dmnLocker.MacID = Convert.ToString(txtMacId.Text);
                dmnLocker.RFID = Convert.ToString(txtRFID.Text);
                dmnLocker.PinNo = Convert.ToString(txtPinNo.Text);
                dmnLocker.LockerColor = Convert.ToString(txtColor.Text);
                dmnLocker.LockDesc = Convert.ToString(txtDesc.Text);
                dmnLocker.HrRate = Convert.ToDecimal(txtPerHours.Text);
                dmnLocker.MnRate = Convert.ToDecimal(txtMonth.Text);
                dmnLocker.DyRate = Convert.ToDecimal(txtPerDay.Text);

                dmnLocker.CreatedBy = Forms.frmLogin.UserId;
                //dmnLocker.CreatedDateTime = DateTime.Now;
                dmnLocker.ModifiedBy = Forms.frmLogin.UserId;
                //dmnLocker.ModifiedDateTime = DateTime.Now;
                dmnLocker.BranchId = Forms.frmLogin.BranchId;
                dmnLocker.Status = 0;

                bool InsertStatus = bllLocker.AddEditData("prdInsert_Upd_Locker", dmnLocker);
                if (InsertStatus == true)
                {
                    if (dmnLocker.ID>0)
                    {
                        
                        MessageBox.Show(this, txtLocker.Text + " detail Successfully updated ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        pnlLocker.Visible = false;
                    }
                    else
                        MessageBox.Show(this, " Successfully saved ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    RefreshForm();
                }
                else
                {

                    MessageBox.Show(this, " Save failed ", " error  ", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }


        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            txtColor.BackColor = colorDialog1.Color;
            string redHex = ColorTranslator.ToHtml(txtColor.BackColor);
            txtColor.Text = redHex;
        }

        private void txtColor_MouseClick(object sender, MouseEventArgs e)
        {
            colorDialog1.ShowDialog();
            txtColor.BackColor = colorDialog1.Color;
            //string redHex = ColorTranslator.ToHtml(txtColor.BackColor);
            //txtColor.Text = redHex;

            Color c = colorDialog1.Color;

            txtColor.Text = colorDialog1.Color.Name;

        }

        private void FrmLocker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                if (pnlLocker.Visible == true)
                {
                    pnlLocker.Visible = false;
                    RefreshForm();
                    FillGridList();
                }
                else
                {
                    this.Close();
                }

            }
            else if (e.KeyData == Keys.F5)
                RefreshForm();
        }
    //    Private Sub InPutAll_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtAdvamt.KeyDown, txtTot.KeyDown, txtOtamt.KeyDown, txtTlate.KeyDown,
    //                                                                                        txtTlate_amt.KeyDown, txtIncenttar.KeyDown, txtIncentamt.KeyDown, txtOadesc1.KeyDown, txtOaamt.KeyDown, txtOddesc1.KeyDown,
    //                                                                                        txtOdamt.KeyDown, txtNotes.KeyDown, txtBsal.KeyDown, txtDedamt.KeyDown, txtDept.KeyDown, txtEarnamt.KeyDown, txtEdt.KeyDown,
    //                                                                                        txtEdt.KeyDown, txtEmp.KeyDown, txtEsal_payterm.KeyDown, txtHd.KeyDown, txtIncentamt.KeyDown, txtIncenttar.KeyDown, txtMdays.KeyDown,
    //                                                                                       txtNetpayable.KeyDown, txtNotes.KeyDown, txtPayamt.KeyDown, txtPaySlipNo.KeyDown, txtPsal.KeyDown, txtTabs.KeyDown, txtTatt.KeyDown,
    //                                                                                        txtThlf.KeyDown, txtThol.KeyDown, txtTlv.KeyDown, txtToff.KeyDown, txtTprs.KeyDown, txtWHrs.KeyDown, txtWOff.KeyDown
    //If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    //End Sub

        private void btnNew_Click(object sender, EventArgs e)
        {
            RefreshForm();
            pnlLocker.Visible = true;
        }

        private void btnFrmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void EditData(int gRowid)
        {
            pnlLocker.Visible = true;
            txtID.Text =Convert.ToString(dgvList.Rows[gRowid].Cells[gLockerID].Value);
            txtLocker.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gLocker].Value);
            txtRowNo.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gRowNo].Value);
            txtMacId.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gMacID].Value);
            txtRFID.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gRFID].Value);
            txtPinNo.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gPINNo].Value);
            txtColor.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gColor].Value);
            txtDesc.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gLockDesc].Value);
            txtPerHours.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gHrRate].Value);
            txtMonth.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gDyRate].Value);
            txtPerDay.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gMnRate].Value);
            cboLocation.SelectedValue = Convert.ToString(dgvList.Rows[gRowid].Cells[gLocationID].Value);

        }
        private void dgvList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DialogResult dialogResult = MessageBox.Show("Collection Report " + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            DialogResult results = MessageBox.Show("Want to Edit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (results == DialogResult.Yes)
                //EditData(Convert.ToInt32(dgvList.Rows[dgvList.CurrentCell.RowIndex].Cells[gLockerID].Value));
                EditData(dgvList.CurrentCell.RowIndex);
        }

    }
}