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
using MetroFramework;

namespace pLocker.Forms
{
    public partial class FrmLocker : Form
    {
        long BranchId = Forms.frmLogin.BranchId;//16
        long UserId = Forms.frmLogin.UserId;//1
        long FinancialYearId = Convert.ToInt64(FrmLockerDashBoard.FinancialYearId);//10

        long LID = 0;
        const int gLockerID = 0;
        const int gLocker = 1;
        const int gLocation = 2;
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
            txtRfid_2.Text = "";
            txtPinNo.Text = "";
            txtColor.Text = "";
            txtColor.BackColor = Color.White;
            txtDesc.Text = "";
            txtPerHours.Text = "0";
            txtMonth.Text = "0";
            txtPerDay.Text = "0";
            cboLocation.SelectedValue = 0;
            cboLocation.Enabled = true;
            FillLocation();
            FillGridList();
        }
        public void FillLocation()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "LocId,Name";
            objSearch.Table = " tblLocationDetails";
            objSearch.WhereNdGroupBy = "where status=1 and LocId =" + BranchId;
            objSearch.OrderBy = "order by value";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            cboLocation.DataSource = gridSource;
            cboLocation.ValueMember = "Id";
            cboLocation.DisplayMember = "Value";
            cboLocation.SelectedIndex = 1;
            cboLocation.Enabled = false;

        }

        private void FrmLocker_Load(object sender, EventArgs e)
        {
            //cboField.Focus();
            ActiveControl = cboField;
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

                _objSearch.Select = "l.*, isnull(ll.Name,'') as LocationDesc";
                _objSearch.Table = "tblLocker l left JOIN tblLocationDetails ll on l.LocationID = ll.LocId";
                if (!string.IsNullOrWhiteSpace(mtxtFieldValue.Text))
                {
                    switch (Convert.ToInt32(cboField.SelectedValue))
                    {
                        case 1:
                            _objSearch.WhereNdGroupBy = "where l.Locker Like '%" + mtxtFieldValue.Text + "%' and l.status=1 and l.BranchId = " + BranchId;
                            break;
                        case 2:
                            _objSearch.WhereNdGroupBy = "where l.MacID Like '%" + mtxtFieldValue.Text + "%' and l.status=1 and l.BranchId = " + BranchId;
                            break;
                        case 3:
                            _objSearch.WhereNdGroupBy = "where l.RFID Like '%" + mtxtFieldValue.Text + "%' and l.status=1 and l.BranchId = " + BranchId;
                            break;
                        case 4:
                            _objSearch.WhereNdGroupBy = "where l.PINno Like '%" + mtxtFieldValue.Text + "%' and l.status=1 and l.BranchId = " + BranchId;
                            break;
                        case 5:
                            _objSearch.WhereNdGroupBy = "where l.LockerRowno Like '%" + mtxtFieldValue.Text + "%' and l.status=1 and l.BranchId = " + BranchId;
                            break;
                        case 6:
                            _objSearch.WhereNdGroupBy = "where l.LockerColor Like '%" + mtxtFieldValue.Text + "%' and l.status=1 and l.BranchId = " + BranchId;
                            break;
                        case 7:
                            _objSearch.WhereNdGroupBy = "where l.HrRate Like '%" + mtxtFieldValue.Text + "%' and l.status=1 and l.BranchId = " + BranchId;
                            break;
                        case 8:
                            _objSearch.WhereNdGroupBy = "where l.DyRate Like '%" + mtxtFieldValue.Text + "%' and ll.status=1 and l.BranchId = " + BranchId;
                            break;
                        case 9:
                            _objSearch.WhereNdGroupBy = "where l.MnRate Like '%" + mtxtFieldValue.Text + "%' and l.status=1  and l.BranchId = " + BranchId;
                            break;
                        case 10:
                            _objSearch.WhereNdGroupBy = "where ll.Name Like '%" + mtxtFieldValue.Text + "%' and l.status=1 and l.BranchId = " + BranchId;
                            break;
                        default:
                            _objSearch.WhereNdGroupBy = "where l.status=1 and l.BranchId = " + BranchId;
                            break;
                    }
                }            
                else
                    _objSearch.WhereNdGroupBy = "where l.status=1 and l.BranchId=" + BranchId;
                _objSearch.OrderBy = "";
                var gridSource = _bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource.Count > 0)
                {

                    dgvList.Rows.Clear();
                    dgvList.Rows.Add(gridSource.Count);
                    for (int i = 0; i < gridSource.Count(); i++)
                    {

                        dgvList.Rows[i].Cells[gLockerID].Value = gridSource[i].ID.ToString();

                        dgvList.Rows[i].Cells[gLocker].Value = gridSource[i].Locker;

                        if (gridSource[i].LocationDesc != null)
                            dgvList.Rows[i].Cells[gLocation].Value = gridSource[i].LocationDesc.ToString();
                        else
                            dgvList.Rows[i].Cells[gLocation].Value = "";
                        if (gridSource[i].LockerRowno != null)
                            dgvList.Rows[i].Cells[gRowNo].Value = gridSource[i].LockerRowno.ToString();
                        else
                            dgvList.Rows[i].Cells[gRowNo].Value = "";

                        if (gridSource[i].MacID != null)
                            dgvList.Rows[i].Cells[gMacID].Value = gridSource[i].MacID.ToString();

                        else
                            dgvList.Rows[i].Cells[gMacID].Value = "";

                        if (gridSource[i].RFID != null && gridSource[i].RFID != "" && gridSource[i].RFID != "Not Assigned")
                        {
                            string s = "XXXX XX" + gridSource[i].RFID.Substring(gridSource[i].RFID.Length - 6, 6);
                            dgvList.Rows[i].Cells[gRFID].Value = s;
                        }                          
                        else
                            dgvList.Rows[i].Cells[gRFID].Value = "Not Assigned";


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
                    dgvList.Columns[gMnRate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.ToString());
            }
        }

        public bool MandatoryCheck()
        {
            if (string.IsNullOrEmpty(txtLocker.Text))
            {
                MetroMessageBox.Show(this, "Locker Name is mandatory.", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLocker.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtRowNo.Text))
            {
                MetroMessageBox.Show(this, "Row Number is mandatory.", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRowNo.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtMacId.Text))
            {
                MetroMessageBox.Show(this, "Mac ID is mandatory.", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMacId.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtRFID.Text))
            {
                MetroMessageBox.Show(this, "RFID is mandatory.", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRFID.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtPinNo.Text))
            {
                MetroMessageBox.Show(this, "Pin Number is mandatory.", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPinNo.Focus();
                return false;
            }
            //else if (string.IsNullOrEmpty(txtColor.Text))
            //{
            //    MetroMessageBox.Show(this, "Color is mandatory.", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtColor.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(txtDesc.Text))
            //{
            //    MetroMessageBox.Show(this, "Description is mandatory.", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtDesc.Focus();
            //    return false;
            //}
            else if (string.IsNullOrEmpty(txtPerHours.Text) || Convert.ToDecimal(txtPerHours.Text)== 0 )
            {
                MetroMessageBox.Show(this, "Per Hours Rate is mandatory.", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPerHours.Focus();
                return false;
            }           
            else if (string.IsNullOrEmpty(txtPerDay.Text) || Convert.ToDecimal(txtPerDay.Text) == 0)
            {
                MetroMessageBox.Show(this, "Per Day Rate is mandatory.", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPerDay.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtMonth.Text) || Convert.ToDecimal(txtMonth.Text) == 0)
            {
                MetroMessageBox.Show(this, "Per Month Rate is mandatory.", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonth.Focus();
                return false;
            }
            // All fields are filled, continue with your logic here
            return true;            

        }
     
            private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {
                    DmnLocker dmnLocker = new DmnLocker();
                    BllLocker bllLocker = new BllLocker();
                    dmnLocker.ID = Convert.ToInt64(txtID.Text);
                    dmnLocker.Locker = Convert.ToString(txtLocker.Text);
                    dmnLocker.LocationID = Convert.ToInt32(cboLocation.SelectedValue);
                    dmnLocker.LockerRowno = Convert.ToString(txtRowNo.Text);
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
                    dmnLocker.Hired = 0;
                    dmnLocker.Status = 1;

                    bool InsertStatus = bllLocker.AddEditData("prdInsert_Upd_Locker", dmnLocker);
                    if (InsertStatus == true)
                    {
                        if (dmnLocker.ID > 0)
                        {

                            MetroMessageBox.Show(this, txtLocker.Text + "  ", "detail Successfully updated", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            pnlLocker.Visible = false;
                        }
                        else
                            MetroMessageBox.Show(this, " ", "Successfully saved ", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        RefreshForm();
                    }
                    else
                    {

                        MetroMessageBox.Show(this, " ", " Save failed   ", MessageBoxButtons.OK);
                    }
                }               
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
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
        private void btnNew_Click(object sender, EventArgs e)
        {
            RefreshForm();
            pnlLocker.Visible = true;
            ActiveControl = txtLocker;
        }

        private void btnFrmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void EditData(int gRowid)
        {
            pnlLocker.Visible = true;
            txtID.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gLockerID].Value);
            txtLocker.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gLocker].Value);
            txtRowNo.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gRowNo].Value);
            txtMacId.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gMacID].Value);
            txtRFID.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gRFID].Value);
            txtRfid_2.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gRFID].Value);
            txtPinNo.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gPINNo].Value);
            txtColor.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gColor].Value);
            txtDesc.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gLockDesc].Value);
            txtPerHours.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gHrRate].Value);
            txtMonth.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gDyRate].Value);
            txtPerDay.Text = Convert.ToString(dgvList.Rows[gRowid].Cells[gMnRate].Value);
            cboLocation.SelectedValue =Convert.ToInt64(dgvList.Rows[gRowid].Cells[gLocationID].Value);
            cboLocation.Enabled = false;
        }
        private void dgvList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DialogResult dialogResult = MessageBox.Show("Collection Report " + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            DialogResult results = MetroMessageBox.Show(this, "", "Want to Edit ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (results == DialogResult.Yes)
                //EditData(Convert.ToInt32(dgvList.Rows[dgvList.CurrentCell.RowIndex].Cells[gLockerID].Value));
                EditData(dgvList.CurrentCell.RowIndex);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(cboField.SelectedIndex==0)
                MetroMessageBox.Show(this, "Please select Search Type", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if(mtxtFieldValue.Text=="" || mtxtFieldValue.Text == null)
            {
                switch (Convert.ToInt32(cboField.SelectedValue))
                {
                    case 1:
                        MetroMessageBox.Show(this, "Please Enter the Locker Name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    case 2:
                        MetroMessageBox.Show(this, "Please Enter the MacID", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                     
                        break;
                    case 3:
                        MetroMessageBox.Show(this, "Please Enter the RFID", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    case 4:
                        MetroMessageBox.Show(this, "Please Enter the PINno", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    case 5:
                        MetroMessageBox.Show(this, "Please Enter the LockerRowno", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    case 6:
                        MetroMessageBox.Show(this, "Please Enter the LockerColor", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    case 7:
                        MetroMessageBox.Show(this, "Please Enter the HrRate", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    case 8:
                        MetroMessageBox.Show(this, "Please Enter the DyRate", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    case 9:
                        MetroMessageBox.Show(this, "Please Enter the MnRate", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                      
                        break;
                    case 10:
                        MetroMessageBox.Show(this, "Please Enter the LocationName", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                }
                mtxtFieldValue.Focus();
            }
            else
                FillGridList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            pnlLocker.Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshForm();
        }

        public void txtFlow(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
                // e.Handled = true;
            }


        }


        private void Minimize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //save
        private void btnSave_MouseHover(object sender, EventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_3));
        }

        private void btnSave_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_2));
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_1));
        }

        private void btnSave_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_1));
        }

        //refresh
        private void btnRefresh_MouseHover(object sender, EventArgs e)
        {
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_3));
        }

        private void btnRefresh_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_2));
        }

        private void btnRefresh_MouseLeave(object sender, EventArgs e)
        {
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_1));
        }

        private void btnRefresh_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_1));
        }

        //delete
        private void btnDelete_MouseHover(object sender, EventArgs e)
        {
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_3));
        }

        private void btnDelete_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_2));
        }

        private void btnDelete_MouseLeave(object sender, EventArgs e)
        {
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1));
        }

        private void btnDelete_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1));
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

        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private void btnRfid_Click(object sender, EventArgs e)
        {
            string str1 = "";
            txtRFID.Clear();
            str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul();
            if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")

                str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi();
            str1 = RemoveSpecialCharacters(str1.Trim());
            if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")
                MetroMessageBox.Show(this, "Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                BllSearch<DmnLocker> bllSearch = new BllSearch<DmnLocker>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = " tblLocker ";
                objSearch.WhereNdGroupBy = "Where RFID= '" + str1 + "'";
                objSearch.OrderBy = "Order By Locker ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);

                if (gridSource.Count > 0)
                {
                    txtLocker.Text = gridSource[0].Locker;
                    cboLocation.SelectedValue = gridSource[0].LocationID;
                    txtRowNo.Text = gridSource[0].LockerRowno;
                    txtMacId.Text = gridSource[0].MacID;
                    txtRFID.Text = gridSource[0].RFID;
                    txtPinNo.Text = gridSource[0].PinNo;
                    txtColor.Text = gridSource[0].LockerColor;
                    txtDesc.Text = gridSource[0].LockDesc;
                    txtPerHours.Text = gridSource[0].HrRate.ToString();
                    txtMonth.Text = gridSource[0].MnRate.ToString(); 
                    txtPerDay.Text = gridSource[0].DyRate.ToString();
                }
                else
                {
                    txtRFID.Text = str1;
                }

            }
        }

        private void cboField_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(cboField.SelectedIndex==-0)
            {
                FillGridList();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string str1 = "";
            txtRFID.Clear();
             str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul();
            if (str1 != "Card not in contact")
            {
                timer1.Stop();
                if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")
                    str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi();
                str1 = RemoveSpecialCharacters(str1.Trim());
                if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Cardnotincontact")
                {
                    MessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    timer1.Start();
                }
                else
                {
                    BllSearch<DmnLocker> bllSearch = new BllSearch<DmnLocker>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = " tblLocker ";
                    objSearch.WhereNdGroupBy = "Where RFID= '" + str1 + "'";
                    objSearch.OrderBy = "Order By Locker ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);

                    if (gridSource.Count > 0)
                    {
                        txtLocker.Text = gridSource[0].Locker;
                        cboLocation.SelectedValue =Convert.ToInt64(gridSource[0].LocationID);
                        cboLocation.Enabled = false;
                        txtRowNo.Text = gridSource[0].LockerRowno;
                        txtMacId.Text = gridSource[0].MacID;
               
                        string s = "XXXX XX" + gridSource[0].RFID.Substring(gridSource[0].RFID.Length - 6, 6);
                        txtRfid_2.Text = s;
                        txtRFID.Text = gridSource[0].RFID;

                        txtPinNo.Text = gridSource[0].PinNo;
                        txtColor.Text = gridSource[0].LockerColor;
                        txtDesc.Text = gridSource[0].LockDesc;
                        txtPerHours.Text = gridSource[0].HrRate.ToString();
                        txtMonth.Text = gridSource[0].MnRate.ToString();
                        txtPerDay.Text = gridSource[0].DyRate.ToString();
                    }
                    else
                    {
                        //txtLocker.Text = "";
                        //cboLocation.SelectedValue = Convert.ToInt64(frmLogin.BranchId);
                        //cboLocation.Enabled = false;
                        //txtRowNo.Text = "";
                        //txtMacId.Text = "";
                        //txtPinNo.Text = "";
                        //txtColor.Text = "";
                        //txtDesc.Text = "";
                        //txtPerHours.Text = "";
                        //txtMonth.Text = "";
                        //txtPerDay.Text = "";

                        string s = "XXXX XX" + str1.Substring(str1.Length - 6, 6);
                        txtRfid_2.Text = s;
                        txtRFID.Text = str1;

                    }
                }
            }
        } 

        private void txtRfid_2_Enter(object sender, EventArgs e)
        {
            lbl_rfid_info.Visible = true;
            timer1.Start();
        }

        private void txtRfid_2_Leave(object sender, EventArgs e)
        {
            timer1.Stop();
            lbl_rfid_info.Visible = false;
        }
    }
}