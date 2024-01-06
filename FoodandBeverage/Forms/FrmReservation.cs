using BLL;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FoodandBeverage.Interfaces;
using CustomMessageBox;

namespace FoodandBeverage.Forms
{
    public partial class FrmReservation : Form
    {
        ClsGeneral ClsGeneral = new ClsGeneral();
        ClsDocument clsDocument = new ClsDocument();
        long BranchId = 1030;

        public int BookmasterId = 0;
        public FrmReservation()
        {
            InitializeComponent();
        }
        public static int itid = 0;
        public static int iTBookingDetailId = 1;
        public static int itablename = 2;
        public static int iprice = 3;
        public static int icapacity = 4;
        public static int idescription = 5;

        public static int imid = 0;
        public static int imBookingDetailId = 1;
        public static int imname = 2;
        public static int imdescription = 3;
        public static int imprice = 4;
        public static int iqty = 5;
        public static int isamount = 6;
        private int yourTargetColumnIndex = 1;    
       
        private void FrmReservation_Load(object sender, EventArgs e)
        {

            BllSearch<DmnHRegistration> bllSearch = new BllSearch<DmnHRegistration>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "BookingInerwell"; 
            objSearch.Table = "tblHotelRegistration";
            objSearch.WhereNdGroupBy = " where Status = 1"; 
            objSearch.OrderBy = "";
            var  result = bllSearch.GetSearchData("prdSearch",objSearch);
            if (result != null)
            {
                txt_HR.Text = result[0].BookingInerwell.ToString();
            }
            FillCombo();
            dgvforms.Rows.Add(1);
            dgvmenu.Rows.Add(1);
            dtpBooking.MinDate = DateTime.Today;
            dtp_from_time.Value = DateTime.Now;


            FrmDashboard frmDash = new FrmDashboard();
            if (frmDash.ChkWritePermission(Convert.ToInt32(formsenum.Reservation)))
            {
                mbtnSave.Enabled = true;

            }
            else
            {
                mbtnSave.Enabled = false;


            }

        }
        public void clear()
        {
            chb_active.Checked = true;
            btn_delt.Visible = false;
            dgvforms.Rows.Clear();
            dgvmenu.Rows.Clear();
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtMob.Text = "";
            txtNam.Text = "";
            txtNOP.Text = "";
            txtRemarks.Text = "";
            cboBtype.SelectedValue = "select";
            cboGender.SelectedValue = "select";
            dgvforms.Rows.Add(1);
            dgvmenu.Rows.Add(1);
            mbtnSave.Visible = true;
            txtBookmasterId.Text = "0";
            lblSTotal.Text = "";
            lblMTotal.Text = "";
            lblGTotal.Text = "";
            discountper.Text = "";
            totaldiscountamt.Text = "0";
            oaamount.Text = "0";
            oadesc.Text = "";
            odamt.Text = "0";
            oddesc.Text = "";
            netamount.Text = "0";
            sterms.Text = "";
            txtadvpa.Text = "0";
            rcvamt.Text = "0";
            lblBalAmt.Text = "0";
            lblNetAmt.Text = "0";
            lblBAmt.Text = "0";
            discountper.Text = "0";
            label21.Visible = false;
            lblBAmt.Visible = false;
        }
        public bool CheckEmailId(Form form, string EmailId)
        {
            Regex mRegxExpression;
            mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
            if (!mRegxExpression.IsMatch(EmailId.Trim()))
            {
                MessageBox.Show("Invalid email format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
       
        public void capasity()
        {
            if (int.TryParse(txtNOP.Text, out int capacity))
            {
                // Assume the DataGridView has a column named "CapacityColumn" containing the capacity for each row
                int totalCapacity = 0;

                foreach (DataGridViewRow row in dgvforms.Rows)
                {
                    if (!row.IsNewRow && row.Cells[icapacity].Value != null)
                    {
                        // Parse the capacity value from the grid
                        if (int.TryParse(row.Cells[icapacity].Value.ToString(), out int personCapacity))
                        {
                            totalCapacity += personCapacity;
                        }
                    }
                }
                if (capacity > totalCapacity)
                {

                    // The capacity entered in the TextBox is greater than or equal to the total capacity in the grid
                    MessageBox.Show("Capacity is not sufficient for the people.");
                    return;

                }

            }
        }
        public bool MandatoryCheck()
        {
            capasity();
            for (int i = 0; i < dgvmenu.Rows.Count - 1; i++)
            {
                if (Convert.ToString(dgvmenu.Rows[i].Cells[iqty].Value) == "0" || Convert.ToString(dgvmenu.Rows[i].Cells[iqty].Value) == "")
                {
                    if (Convert.ToString(dgvmenu.Rows[i].Cells[0].Value) != "")
                    {
                        MessageBox.Show("Quantity cantbe less than one", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        dgvmenu.Focus();
                        return false;
                    }
                }
            }
            if (txtNam.Text == "" || txtNam.Text == string.Empty)
            {
                CustomMessageBox.CMessageBox.Show("UserName is A Mandatory Field ", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //CMessageBox.Show("Please Select IGST %", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show(" UserName is A Mandatory Field ", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtAddress.Text == "" || txtAddress.Text == string.Empty)
            {
                MessageBox.Show("Address is A Mandatory Field ", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (cboGender.SelectedIndex == 0 || cboGender.SelectedIndex == -1)
            {
                MessageBox.Show("Gender Needs To Be Specified", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGender.Focus();
                return false;
            }
            else if (txtMob.Text == "" || txtMob.Text == string.Empty)
            {
                DialogResult result = MessageBox.Show("Mobile number is A Mandatory Field", "Mandatory Field", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    // User clicked Yes
                }

                else if (result == DialogResult.Cancel)
                {
                    // User clicked Cancel
                }

                //MessageBox.Show(" Mobile number is A Mandatory Field ", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                return ClsGeneral.CheckEmailId(this, txtEmail.Text);
            }

            return true;
        }
        public void FillCombo()
        {
            //Gender
            BllSearch<DmnComboBox> bllGender = new BllSearch<DmnComboBox>();
            DmnSearch dmngender = new DmnSearch();
            dmngender.Select = "GTypeSubId,Caption";
            dmngender.Table = " tblGeneralOptions ";
            dmngender.WhereNdGroupBy = "Where GTypeId = 25 ";//AND DepartmentId = " + mcmbDepartmentId.SelectedValue;
            dmngender.OrderBy = "order by Value";
            var gender = bllGender.GetSearchData("prdComboFill", dmngender);
            cboGender.DataSource = gender;
            cboGender.DisplayMember = "Value";
            cboGender.ValueMember = "Id";

            //Reservation
            BllSearch<DmnComboBox> bllReservation = new BllSearch<DmnComboBox>();
            DmnSearch dmnReservation = new DmnSearch();
            dmnReservation.Select = "GTypeSubId,Caption";
            dmnReservation.Table = " tblGeneralOptions ";
            dmnReservation.WhereNdGroupBy = "Where GTypeId = 67 ";
            dmnReservation.OrderBy = "order by Value";
            var table = bllReservation.GetSearchData("prdComboFill", dmnReservation);
            cboBtype.DataSource = table;
            cboBtype.DisplayMember = "Value";
            cboBtype.ValueMember = "Id";
        }

        //private void cboGender_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    if (Convert.ToInt32(cboGender.SelectedValue) == 1)
        //    {
        //        //pictureBox3.Image = FoodandBeverage.Properties.Resources.Profilepicmale;
        //    }
        //    else if (Convert.ToInt32(cboGender.SelectedValue) == 2)
        //    {
        //        //pictureBox3.Image = FoodandBeverage.Properties.Resources.Profilepicfemale;
        //        //pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
        //    }
        //    else if (Convert.ToInt32(cboGender.SelectedValue) == 3)
        //    {
        //        //pictureBox3.Image = FoodandBeverage.Properties.Resources.Admin_1;
        //        //pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
        //    }
        //}

        private void mbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool CUst = false;
                if (MandatoryCheck())
                {

                    BllReservation bllReservation = new BllReservation();
                    DmnReservation dmnReservation = new DmnReservation();
                    //dmnReservation.BookmasterId = BookmasterId;
                    dmnReservation.BookmasterId = Convert.ToInt32(txtBookmasterId.Text);
                    dmnReservation.cusName = txtNam.Text;
                    dmnReservation.Mobile = txtMob.Text;
                    dmnReservation.Address = txtAddress.Text;
                    dmnReservation.EmailId = txtEmail.Text;
                    dmnReservation.Remarks = txtRemarks.Text;
                    dmnReservation.gender = Convert.ToString(cboGender.SelectedValue);
                    dmnReservation.CreatedBy = 1;
                    dmnReservation.Status = 1;
                    dmnReservation.BookingType = Convert.ToString(cboBtype.SelectedValue);
                    dmnReservation.NoOfPeople = txtNOP.Text;
                    dmnReservation.BranchId = 1030;
                    dmnReservation.BookingDateTime = dtpBooking.Value;
                    dmnReservation.ReservationTotal = Convert.ToDecimal(lblSTotal.Text);
                    dmnReservation.MenuTotal = Convert.ToDecimal(lblMTotal.Text);
                    dmnReservation.GrandTotal = Convert.ToDecimal(lblGTotal.Text);
                    dmnReservation.DiscountAmt = Convert.ToDecimal(totaldiscountamt.Text);

                    dmnReservation.DiscountPer = Convert.ToDecimal(discountper.Text);
                    dmnReservation.OtherAdd = Convert.ToDecimal(oaamount.Text);
                    dmnReservation.OtherDed = Convert.ToDecimal(odamt.Text);
                    dmnReservation.OtherAddText = oadesc.Text;
                    dmnReservation.OtherDedText = oddesc.Text;
                    dmnReservation.NetAmount = Convert.ToDecimal(netamount.Text);
                    dmnReservation.Terms = sterms.Text;
                    dmnReservation.BalnceNeed = Convert.ToDecimal(lblBAmt.Text);
                    if (Convert.ToDecimal(txtBookmasterId.Text) != 0)
                    {
                        if (Convert.ToDecimal(lblBalAmt.Text) > 0)
                        {
                            txtadvpa.Text = (Convert.ToDecimal(txtadvpa.Text) + Convert.ToDecimal(rcvamt.Text)).ToString();
                        }
                        else
                        {
                            dmnReservation.FinalAmount = dmnReservation.BalnceNeed;
                            dmnReservation.BalnceNeed = 0;
                        }


                    }
                    else
                    {
                        if (Convert.ToDecimal(lblBalAmt.Text) <= 0 && Convert.ToDecimal(txtadvpa.Text) == 0)
                        {

                            dmnReservation.FinalAmount = dmnReservation.BalnceNeed;
                            dmnReservation.BalnceNeed = 0;
                        }

                    }

                    dmnReservation.AdvancePay = Convert.ToDecimal(txtadvpa.Text);
                    dmnReservation.CashRev = Convert.ToDecimal(rcvamt.Text);
                    dmnReservation.BalnceAmt = Convert.ToDecimal(lblBalAmt.Text);


                    var InsertStatusP = bllReservation.AddData("prdInsert_Upd_bookMaster", dmnReservation);

                    if (InsertStatusP.BookmasterId != 0)
                    {
                        bool datadetails = false;
                        DmnReservationDetails dmnReservation1 = new DmnReservationDetails();
                        for (int i = 0; i < dgvforms.Rows.Count - 1; i++)
                        {

                            if (Convert.ToString(dgvforms.Rows[i].Cells[itid].Value) != null && Convert.ToString(dgvforms.Rows[i].Cells[itid].Value) != "")
                            {
                                dmnReservation1.MTID = Convert.ToInt32(dgvforms.Rows[i].Cells[itid].Value);

                                dmnReservation1.BookmasterId = InsertStatusP.BookmasterId;
                                dmnReservation1.BookdetailId = Convert.ToInt32(dgvforms.Rows[i].Cells[iTBookingDetailId].Value);

                                dmnReservation1.Name = Convert.ToString(dgvforms.Rows[i].Cells[itablename].Value);
                                dmnReservation1.Quantity = Convert.ToInt32(dgvforms.Rows[i].Cells[icapacity].Value);
                                dmnReservation1.Price = Convert.ToInt32(dgvforms.Rows[i].Cells[iprice].Value);
                                dmnReservation1.Description = Convert.ToString(dgvforms.Rows[i].Cells[idescription].Value);
                                dmnReservation1.CreatedBy = 1;
                                dmnReservation1.BranchId = 1030;
                                dmnReservation1.Rid = Convert.ToInt32(TableMenu.Rid);
                                dmnReservation1.Status = 1;

                                datadetails = bllReservation.AddDetails("prdInsert_Upd_bookDetails", dmnReservation1);

                            }

                        }
                        for (int i = 0; i < dgvmenu.Rows.Count - 1; i++)
                        {

                            if (Convert.ToString(dgvmenu.Rows[i].Cells[imid].Value) != null && Convert.ToString(dgvmenu.Rows[i].Cells[imid].Value) != "")
                            {
                                dmnReservation1.MTID = Convert.ToInt32(dgvmenu.Rows[i].Cells[imid].Value);

                                dmnReservation1.BookmasterId = InsertStatusP.BookmasterId;
                                dmnReservation1.BookdetailId = Convert.ToInt32(dgvmenu.Rows[i].Cells[imBookingDetailId].Value);
                                dmnReservation1.Name = Convert.ToString(dgvmenu.Rows[i].Cells[imname].Value);
                                dmnReservation1.Quantity = Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value);
                                dmnReservation1.Price = Convert.ToInt32(dgvmenu.Rows[i].Cells[imprice].Value);
                                dmnReservation1.Description = Convert.ToString(dgvmenu.Rows[i].Cells[imdescription].Value);
                                dmnReservation1.CreatedBy = 1;
                                dmnReservation1.BranchId = 1030;
                                dmnReservation1.Rid = Convert.ToInt32(TableMenu.Mid);
                                dmnReservation1.Status = 1;
                                dmnReservation1.Net = Convert.ToDecimal(dgvmenu.Rows[i].Cells[isamount].Value);
                                datadetails = bllReservation.AddDetails("prdInsert_Upd_bookDetails", dmnReservation1);

                            }

                        }
                        if (datadetails == true)
                        {
                            MessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (datadetails == false)
                        {
                            MessageBox.Show("Unable To Save  ", "Error On Save", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    //else
                    //{
                    //    bool saveStatus = bllReservation.UpdateData("prdInsert_Upd_bookDetails", dmnReservation);


                    //    if (saveStatus == true)
                    //    {
                    //        MessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    }
                    //    else if (saveStatus == false)
                    //    {
                    //        MessageBox.Show("Unable To Save  ", "Error On Save", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //    }
                    //}
                    clear();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtNam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtMob_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cboGender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtNOP_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dtpBooking_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cboBtype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void dgvforms_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == itablename)
            {
                lblToolTipPopup.Visible = true;
            }
            else

            {
                lblToolTipPopup.Visible = false;
            }
        }
        public void menu(int BranchId)
        {
            try
            {
                string returnValue = string.Empty;
                DmnMenu dtColumnAlign = new DmnMenu();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "MId,Description,Price,Name";
                _objSearch.Select = "MId as Id,Name as Field1,p.Price as Field2,m.Description as Field4";
                _objSearch.Table = "tblMenu m left join tblProduct p  on " +
                 "m.ProductId = p.ProductId";
                _objSearch.WhereNdGroupBy = " Where m.Status=1";
                _objSearch.OrderBy = " ";
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);

                    }
                }
                BllSearch<DmnMenu> bllSearch = new BllSearch<DmnMenu>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "MId,m.Description as Description,p.Price,Name as Title";
                objSearch.Table = "tblMenu m left join tblProduct p " +
                "on m.Fcid = p.Fcid";
                objSearch.WhereNdGroupBy = " where MId = " + returnValue;
                objSearch.OrderBy = " ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    int qty = 1;
                    int price = 0;
                    bool flag = true;
                    int i = 0;
                    for (i = 0; i <= dgvmenu.RowCount - 1; i++)
                    {
                        if (Convert.ToInt32(dgvmenu.Rows[i].Cells[imid].Value) == gridSource[0].MId)
                        {
                            flag = false;
                            qty += Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value);
                            price += Convert.ToInt32(dgvmenu.Rows[i].Cells[imprice].Value);


                            break;

                        }
                    }
                    //if (gridSource[0].QOH >= qty)
                    //{
                    if (flag == true)
                    {
                        if (dgvmenu.Rows.Count - 1 == dgvmenu.CurrentCell.RowIndex)
                        {
                            dgvmenu.Rows.Add(1);
                        }

                        dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[imid].Value = gridSource[0].MId;
                        dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[imBookingDetailId].Value = 0;
                        dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[imname].Value = gridSource[0].Title;
                        dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[imprice].Value = gridSource[0].Price;
                        dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[iqty].Value = 1;
                        dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[imdescription].Value = gridSource[0].Description;

                    }
                    else
                    {
                        dgvmenu.Rows[i].Cells[iqty].Value = qty;
                        dgvmenu.Rows[i].Cells[isamount].Value = Convert.ToDecimal(dgvmenu.Rows[i].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[i].Cells[iqty].Value);

                    }

                }

                if (dgvmenu.CurrentCell.ColumnIndex == imname && dgvmenu.CurrentCell.RowIndex >= 0)
                {
                    // Specify the column index for which you want to calculate the sum


                    decimal totalSum = 0;
                    int i = 0;
                    for (i = 0; i <= dgvmenu.RowCount - 1; i++)
                    {
                        if (dgvmenu.Rows[i].Cells[imprice].Value != null)
                        {
                            decimal cellValue;
                            if (decimal.TryParse(dgvmenu.Rows[i].Cells[isamount].Value.ToString(), out cellValue))
                            {
                                totalSum += cellValue;
                            }
                        }
                    }

                    // Display the sum in a TextBox
                    lblMTotal.Text = totalSum.ToString();

                }
                //else
                //{
                //    MessageBox.Show("Avilable Stock Quanity Is " + gridSource[0].QOH, "Insufficent Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    dgvmenu.Rows[i].Cells[iqty].Value = gridSource[0].QOH;


                //}


            }
            catch (Exception ex)
            { }
        }
        public void purchasepop(int BranchId)
        {
            try
            {
                //int timeinterwell = Convert.ToInt32(txt_HR.Text);
                int r = dgvforms.CurrentCell.RowIndex;
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Tid,TableCode,Price,Capcity,Description";
                _objSearch.Select = " Tid as Id,TableCode as Field1,Price as Field2,Capcity as Field3,Description as Field4 ";
                _objSearch.Table = " tblTable_info   ";
                _objSearch.WhereNdGroupBy = " WHERE Tid NOT IN ( SELECT MTID  FROM tblbookMaster m LEFT JOIN tblBookDetails d ON m.BookmasterId = d.BookmasterId" +
                    " WHERE BookingDateTime > DATEADD(MINUTE,-"+(txt_HR.Text) +", SYSDATETIME()) " +
                    " AND BookingDateTime < DATEADD(MINUTE, " + (txt_HR.Text) + ", SYSDATETIME()) and m.Status= 1  ) " +
                    " and Status=1 AND Tid NOT IN (SELECT Tid  FROM tblOrderMaster WHERE CreatedDateTime > DATEADD(MINUTE,-" + (txt_HR.Text) + ", SYSDATETIME()) and Status= 1 ) ";
                _objSearch.OrderBy = " ";
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);

                    }
                }
                BllSearch<Dmntable> bllSearch = new BllSearch<Dmntable>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "Tid,TableCode,Price,Capcity,Description";
                objSearch.Table = "tblTable_info";
                objSearch.WhereNdGroupBy = "Where Tid = " + returnValue;
                objSearch.OrderBy = " ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgvforms.Rows.Add(gridSource.Count);
                if (gridSource.Count > 0)
                {
                    
                    dgvforms.Rows[r].Cells[itid].Value = gridSource[0].Tid;
                    dgvforms.Rows[r].Cells[iTBookingDetailId].Value = 0;
                    dgvforms.Rows[r].Cells[itablename].Value = gridSource[0].TableCode;
                    dgvforms.Rows[r].Cells[iprice].Value = gridSource[0].Price;
                    dgvforms.Rows[r].Cells[icapacity].Value = gridSource[0].Capcity;
                    dgvforms.Rows[r].Cells[idescription].Value = gridSource[0].Decription;
                }
            }
            catch (Exception ex)
            { }
        }
        private void dgvforms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
            else if (e.KeyData == Keys.F8 && dgvforms.CurrentCell.ColumnIndex == itablename)
            {
                lblToolTipPopup.Visible = false;
                if (Convert.ToInt32(cboBtype.SelectedValue) == (Convert.ToInt32(Reservation.TableBooking)))
                {

                    purchasepop(Convert.ToInt32(cboBtype.SelectedValue));
                }
                //dgvforms.Rows.Add(1);
            }
            else if (e.KeyData == Keys.Delete)
            {
                dgvforms.Rows.RemoveAt(dgvforms.CurrentCell.RowIndex);
            }
            if (dgvforms.CurrentCell.ColumnIndex >= 0 && dgvforms.CurrentCell.RowIndex >= 0)
            {
                // Specify the column index for which you want to calculate the sum


                decimal totalSum = 0;

                foreach (DataGridViewRow row in dgvforms.Rows)
                {
                    if (row.Cells[iprice].Value != null)
                    {
                        decimal cellValue;
                        if (decimal.TryParse(row.Cells[iprice].Value.ToString(), out cellValue))
                        {
                            totalSum += cellValue;
                        }
                    }
                }

                // Display the sum in a TextBox
                lblSTotal.Text = totalSum.ToString();
            }
            Sum();
        }



        private void cboBtype_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dgvforms.Rows.Clear();
            dgvforms.Rows.Add(1);
            if (Convert.ToInt32(cboBtype.SelectedValue) == Convert.ToInt32(Reservation.Restaurant))
            {
                dgvforms.Rows.Clear();
                dgvforms.Rows.Add(1);
                BllSearch<Dmntable> bllSearch = new BllSearch<Dmntable>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = " tblTable_info";
                objSearch.WhereNdGroupBy = " Where Status=1";
                objSearch.OrderBy = " ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgvforms.Rows.Add(gridSource.Count);
                for (int i = 0; i < gridSource.Count; i++)
                {


                    dgvforms.Rows[i].Cells[itid].Value = gridSource[i].Tid;
                    dgvforms.Rows[i].Cells[itablename].Value = gridSource[i].TableCode;
                    dgvforms.Rows[i].Cells[iprice].Value = gridSource[i].Price;
                    dgvforms.Rows[i].Cells[icapacity].Value = gridSource[i].Capcity;
                    dgvforms.Rows[i].Cells[idescription].Value = gridSource[i].Decription;

                }
            }
            if (dgvforms.CurrentCell.ColumnIndex >= 0 && dgvforms.CurrentCell.RowIndex >= 0)
            {
                // Specify the column index for which you want to calculate the sum


                decimal totalSum = 0;

                foreach (DataGridViewRow row in dgvforms.Rows)
                {
                    if (row.Cells[iprice].Value != null)
                    {
                        decimal cellValue;
                        if (decimal.TryParse(row.Cells[iprice].Value.ToString(), out cellValue))
                        {
                            totalSum += cellValue;
                        }
                    }
                }

                // Display the sum in a TextBox
                lblSTotal.Text = totalSum.ToString();
            }
            Sum();
        }

        private void dgvmenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
            else if (e.KeyData == Keys.F8 && (dgvmenu.CurrentCell.ColumnIndex == imname))
            {
                label9.Visible = false;

                menu(Convert.ToInt32(cboBtype.SelectedValue));

                //dgvmenu.Rows.Add(1);
            }
            else if (e.KeyData == Keys.Delete)
            {
                dgvmenu.Rows.RemoveAt(dgvmenu.CurrentCell.RowIndex);
            }

        }

        private void dgvmenu_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == imname)
            {
                label9.Visible = true;
            }
            else

            {
                label9.Visible = false;
            }
        }

        private void mbtnList_Click(object sender, EventArgs e)
        {
            Reservationlist();
        }
        public void Reservationlist()
        {
            try
            {
                btn_delt.Visible = true;
                CalNetAmt();
                label21.Visible = true;
                lblBAmt.Visible = true;
                string returnValue = string.Empty;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DataTable dtColumnAlign = new DataTable();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,cusName,Address,Mobile,Remarks";
                _objSearch.Select = "BookmasterId as Id,cusName as Field1,Address as Field2,Mobile as Field3,Remarks as Field4";
                _objSearch.Table = "tblbookMaster";
                _objSearch.WhereNdGroupBy = " WHERE Status = 1";
                _objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_delt.Visible = false;
                }
                else
                {
                    using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))
                    {
                        var dialogResult = search.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                        {
                            returnValue = Convert.ToString(search._retValue);
                        }
                    }

                    if (returnValue != "")
                    {
                        mbtnSave.Visible = true;
                        DmnSearch dmnSearch = new DmnSearch();
                        BllSearch<DmnReservation> _bllSearch = new BllSearch<DmnReservation>();
                        dmnSearch.Select = "*";
                        dmnSearch.Table = "tblbookMaster";

                        dmnSearch.WhereNdGroupBy = " Where BranchId = +  1030 and BookmasterId ='" + returnValue + "'";
                        dmnSearch.OrderBy = "";
                        var gridSource1 = _bllSearch.GetSearchData("prdSearch", dmnSearch);
                        txtNam.Text = Convert.ToString(gridSource1[0].cusName);
                        txtAddress.Text = Convert.ToString(gridSource1[0].Address);
                        cboGender.SelectedValue = Convert.ToInt64(gridSource1[0].gender);
                        txtMob.Text = Convert.ToString(gridSource1[0].Mobile);
                        txtEmail.Text = Convert.ToString(gridSource1[0].EmailId);
                        txtNOP.Text = Convert.ToString(gridSource1[0].NoOfPeople);
                        cboBtype.SelectedValue = Convert.ToInt64(gridSource1[0].BookingType);
                        txtRemarks.Text = Convert.ToString(gridSource1[0].Remarks);
                        txtBookmasterId.Text = Convert.ToString(gridSource1[0].BookmasterId);
                        lblSTotal.Text = Convert.ToString(gridSource1[0].ReservationTotal);
                        lblMTotal.Text = Convert.ToString(gridSource1[0].MenuTotal);
                        lblGTotal.Text = Convert.ToString(gridSource1[0].GrandTotal);
                        discountper.Text = Convert.ToString(gridSource1[0].DiscountPer);
                        totaldiscountamt.Text = Convert.ToString(gridSource1[0].DiscountAmt);
                        oaamount.Text = Convert.ToString(gridSource1[0].OtherAdd);
                        odamt.Text = Convert.ToString(gridSource1[0].OtherDed);
                        oadesc.Text = Convert.ToString(gridSource1[0].OtherAddText);
                        oddesc.Text = Convert.ToString(gridSource1[0].OtherDedText);
                        netamount.Text = Convert.ToString(gridSource1[0].NetAmount);
                        sterms.Text = Convert.ToString(gridSource1[0].Terms);
                        txtadvpa.Text = Convert.ToString(gridSource1[0].AdvancePay);
                        rcvamt.Text = Convert.ToString(gridSource1[0].CashRev);
                        lblNetAmt.Text = Convert.ToString(gridSource1[0].NetAmount);
                        decimal adpayment = Convert.ToDecimal(txtadvpa.Text);
                        decimal bal = Convert.ToDecimal(lblBalAmt.Text);
                        decimal net = Convert.ToDecimal(netamount.Text);
                        decimal balanceneed = 0;
                        decimal cashreciv = Convert.ToDecimal(rcvamt.Text);
                        decimal finalAmt = Convert.ToDecimal(gridSource1[0].FinalAmount);
                        rcvamt.Text = "0";
                        lblBalAmt.Text = "0";
                        balanceneed = net - (adpayment + finalAmt);
                        lblBAmt.Text = balanceneed.ToString("0.0");
                        //calmenu();

                        if (Convert.ToDecimal(finalAmt) > 0)
                        {
                            label21.Text = "Final Settlement :";
                            lblBAmt.Text = finalAmt.ToString();
                            mbtnSave.Visible = false;
                        }
                        else if (Convert.ToDecimal(lblBAmt.Text) > 0)
                        {
                            label21.Text = "Balance Need To Pay :";
                        }
                        if (Convert.ToDecimal(lblBAmt.Text) == 0)
                        {
                            label21.Text = "Amount Settled :";
                            lblBAmt.Text = "0";
                            mbtnSave.Visible = false;

                        }


                        DmnSearch dmnSearch1 = new DmnSearch();
                        BllSearch<DmnReservationDetails> _bllSearch1 = new BllSearch<DmnReservationDetails>();
                        dmnSearch1.Select = "*";
                        dmnSearch1.Table = "tblBookDetails ";
                        dmnSearch1.WhereNdGroupBy = " Where BookmasterId ='" + returnValue + "'";
                        var gridSource2 = _bllSearch1.GetSearchData("prdSearch", dmnSearch1);
                        if (gridSource2.Count > 0)
                        {
                            dgvforms.Rows.Clear();
                            dgvmenu.Rows.Clear();
                            //dgvforms.Rows.Add(gridSource2.Count);
                            int r = 0;
                            int r1 = 0;
                            for (int i = 0; i < gridSource2.Count; i++)
                            {
                                if (gridSource2[i].Rid == Convert.ToInt32(TableMenu.Rid))
                                {
                                    dgvforms.Rows.Add(1);
                                    //dgvforms.Rows[i].Cells[0].Value = i + 1;

                                    dgvforms.Rows[r].Cells[itid].Value = gridSource2[i].MTID;
                                    dgvforms.Rows[r].Cells[iTBookingDetailId].Value = gridSource2[i].BookdetailId;
                                    dgvforms.Rows[r].Cells[itablename].Value = gridSource2[i].Name;
                                    dgvforms.Rows[r].Cells[iprice].Value = gridSource2[i].Price;
                                    dgvforms.Rows[r].Cells[icapacity].Value = gridSource2[i].Quantity;
                                    dgvforms.Rows[r].Cells[idescription].Value = gridSource2[i].Description;

                                    r++;
                                }
                                else
                                {
                                    dgvmenu.Rows.Add(1);
                                    dgvmenu.Rows[r1].Cells[imid].Value = gridSource2[i].MTID;
                                    dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = gridSource2[i].BookdetailId;
                                    //dgvmenu.Rows[i].Cells[0].Value = i + 1;
                                    //dgvmenu.Rows[i].Cells[BookmasterId].Value = gridSource2[i].BookmasterId;
                                    dgvmenu.Rows[r1].Cells[imname].Value = gridSource2[i].Name;
                                    dgvmenu.Rows[r1].Cells[imprice].Value = gridSource2[i].Price;
                                    dgvmenu.Rows[r1].Cells[iqty].Value = gridSource2[i].Quantity;
                                    dgvmenu.Rows[r1].Cells[imdescription].Value = gridSource2[i].Description;
                                    dgvmenu.Rows[r1].Cells[isamount].Value = gridSource2[i].Net;
                                    r1++;
                                }
                            }
                            dgvforms.Rows.Add(1);
                            dgvmenu.Rows.Add(1);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void guna2DataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == itablename)
            {
                lblToolTipPopup.Visible = true;
            }
            else

            {
                lblToolTipPopup.Visible = false;
            }
        }


        public void CapacityCheck()
        {
            if (int.TryParse(txtNOP.Text, out int capacity))
            {
                // Assume the DataGridView has a column named "CapacityColumn" containing the capacity for each row
                int totalCapacity = 0;

                foreach (DataGridViewRow row in dgvforms.Rows)
                {
                    if (!row.IsNewRow && row.Cells[icapacity].Value != null)
                    {
                        // Parse the capacity value from the grid
                        if (int.TryParse(row.Cells[icapacity].Value.ToString(), out int personCapacity))
                        {
                            totalCapacity += personCapacity;
                        }
                    }
                }
                if (capacity > totalCapacity)
                {

                    // The capacity entered in the TextBox is greater than or equal to the total capacity in the grid
                    MessageBox.Show("Capacity is not sufficient for the people.");


                }

            }

        }
        private void dgvforms_Leave(object sender, EventArgs e)
        {
            CapacityCheck();
            Sum();
        }

        private void dgvforms_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == yourTargetColumnIndex)
            {
                // Show the label control on top of the column header
                lblToolTipPopup.Location = dgvforms.GetCellDisplayRectangle(2, e.RowIndex, false).Location;
                lblToolTipPopup.Visible = true;
            }
        }

        private void dgvforms_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == yourTargetColumnIndex)
            {
                // Hide the label control when leaving the column header
                lblToolTipPopup.Visible = false;
            }
        }

        private void dgvmenu_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == yourTargetColumnIndex)
            {
                // Show the label control on top of the column header
                label9.Location = dgvmenu.GetCellDisplayRectangle(2, e.RowIndex, false).Location;
                label9.Visible = true;
            }
        }

        private void dgvmenu_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == yourTargetColumnIndex)
            {
                // Hide the label control when leaving the column header
                lblToolTipPopup.Visible = false;
            }
        }

        private void dgvforms_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }
        private bool ContainsAlphabets(string text)
        {
            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    return true;
                }
            }
            return false;
        }

        private void dgvmenu_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == Convert.ToInt32(iqty)) // Replace 'yourAlphabeticColumnIndex' with the actual index of the column you want to validate
            {
                string value = e.FormattedValue.ToString();

                // Check if the input contains alphabetic characters
                if (ContainsAlphabets(value))
                {
                    e.Cancel = true;
                    MessageBox.Show("Alphabetic characters are not allowed in this column.");
                }
            }
            dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[isamount].Value = Convert.ToDecimal(dgvmenu.Rows[e.RowIndex].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[e.RowIndex].Cells[iqty].Value);
        }
        public void menutotal()
        {

        }
        private void discountper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void discountper_KeyPress(object sender, KeyPressEventArgs e)
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
        public void sdiscountPer()
        {
            try
            {
                if (discountper.Text == "" || discountper.Text == ".")
                {
                    discountper.Text = "0";
                    totaldiscountamt.Text = "0";

                }

                if (discountper.Text != "" && Convert.ToDecimal(discountper.Text) != 0)
                {
                    decimal discountAmnt = Convert.ToDecimal(discountper.Text.Trim()) * Convert.ToDecimal(lblGTotal.Text) / 100;
                    totaldiscountamt.Text = discountAmnt.ToString("N2");

                }
            }
            catch (Exception ex)
            { }
        }
        public void CalNetAmt()
        {
            try
            {
                if (Convert.ToInt32(txtBookmasterId.Text) == 0)
                {
                    decimal discount = Convert.ToDecimal(totaldiscountamt.Text);
                    decimal otheraddamt = Convert.ToDecimal(oaamount.Text);
                    decimal otherdedamt = Convert.ToDecimal(odamt.Text);
                    decimal adpayment = Convert.ToDecimal(txtadvpa.Text);
                    decimal balanceneed = Convert.ToDecimal(lblBAmt.Text);
                    decimal cashreciv = Convert.ToDecimal(rcvamt.Text);
                    decimal netamt = (Convert.ToDecimal(lblGTotal.Text) - discount - otherdedamt) + otheraddamt;

                    netamount.Text = netamt.ToString("0.00");
                    lblNetAmt.Text = netamt.ToString("0.00");
                    decimal balAmnt = adpayment - Convert.ToDecimal(rcvamt.Text);
                    lblBalAmt.Text = balAmnt.ToString("N2");
                    mbtnSave.Enabled = true;
                }
                if (Convert.ToInt32(txtBookmasterId.Text) != 0)
                {
                    decimal discount = Convert.ToDecimal(totaldiscountamt.Text);
                    decimal otheraddamt = Convert.ToDecimal(oaamount.Text);
                    decimal otherdedamt = Convert.ToDecimal(odamt.Text);
                    decimal adpayment = Convert.ToDecimal(txtadvpa.Text);
                    decimal balanceneed = Convert.ToDecimal(lblBAmt.Text);
                    decimal cashreciv = Convert.ToDecimal(rcvamt.Text);
                    decimal netamt = (Convert.ToDecimal(lblGTotal.Text) - discount - otherdedamt) + otheraddamt;

                    netamount.Text = netamt.ToString("0.00");
                    lblNetAmt.Text = netamt.ToString("0.00");
                    //calmenu();
                }

            }
            catch (Exception ex)
            { }
        }
        private void discountper_Leave(object sender, EventArgs e)
        {
            try
            {
                if (discountper.Text == "")
                {
                    discountper.Text = "0";
                    totaldiscountamt.Text = "0";
                }
                else
                {
                    if (lblGTotal.Text != "Rs:" && lblGTotal.Text != "0")
                    {



                        sdiscountPer();
                        CalNetAmt();

                    }
                }
                if (Convert.ToDecimal(discountper.Text) > 100)
                {
                    MessageBox.Show("Discount Percentage Cantbe More Than 100", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    discountper.Text = "0.00";
                    totaldiscountamt.Text = "0.00";
                    discountper.Focus();
                }
            }
            catch (Exception ex)
            { }
        }

        private void discountper_TextChanged(object sender, EventArgs e)
        {
            CalNetAmt();
        }

        private void discountper_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(discountper.Text, out value))
            {
                discountper.Text = "0.00";
            }
        }

        private void totaldiscountamt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void totaldiscountamt_KeyPress(object sender, KeyPressEventArgs e)
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
        public void sdiscountAmnt()
        {
            try
            {
                if (totaldiscountamt.Text != "" && Convert.ToDecimal(totaldiscountamt.Text) != 0)
                {
                    decimal discountPer = (Convert.ToDecimal(totaldiscountamt.Text) / Convert.ToDecimal(lblGTotal.Text) * 100);
                    discountper.Text = discountPer.ToString("N2");


                    //decimal discountAmnt = Convert.ToDecimal(txtsrdisper.Text.Trim()) * Convert.ToDecimal(lblsrNetAmt.Text) / 100;
                    //txtsrdisamt.Text = discountAmnt.ToString("N2");
                    //CalSRNetAmt();
                }
            }
            catch (Exception ex)
            { }
        }
        private void totaldiscountamt_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (totaldiscountamt.Text == "")
                //{
                //    discountper.Text = "0";
                //    totaldiscountamt.Text = "0";
                //}
                //
                if (totaldiscountamt.Text == "" || totaldiscountamt.Text == ".")
                {
                    totaldiscountamt.Text = "0";
                    discountper.Text = "0";
                }
                else
                {
                    if (lblGTotal.Text != "Rs:" && lblGTotal.Text != "0")
                    {
                        //totaldiscountamt.Text = "0";
                        //discountper.Text = "0";
                        sdiscountAmnt();
                        sdiscountAmnt();
                        CalNetAmt();
                    }
                }
                //discountAmnt();
                if (Convert.ToDecimal(totaldiscountamt.Text) > (Convert.ToDecimal(lblGTotal.Text)))
                {
                    MessageBox.Show("Discount Amount Cant Be More Than Total Amount", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    totaldiscountamt.Text = "0.00";
                    discountper.Text = "0.00";
                    totaldiscountamt.Focus();
                }
            }

            catch (Exception ex)
            { }
        }

        private void totaldiscountamt_TextChanged(object sender, EventArgs e)
        {
            CalNetAmt();
        }

        private void totaldiscountamt_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(totaldiscountamt.Text, out value))
            {
                totaldiscountamt.Text = "0.00";
            }
        }

        private void remarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void oaamount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void oaamount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void oaamount_Leave(object sender, EventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(oaamount.Text, out value))
            {
                oaamount.Text = "0.00";
            }
            if (lblGTotal.Text != "Rs:")
            {


                CalNetAmt();

            }
        }

        private void oadesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void odamt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void odamt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void odamt_Leave(object sender, EventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(odamt.Text, out value))
            {
                odamt.Text = "0.00";
            }
            if (lblGTotal.Text != "Rs:")
            {


                CalNetAmt();

            }
        }

        private void oddesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void netamount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void netamount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void netamount_Validating(object sender, CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(netamount.Text, out value))
            {
                netamount.Text = "0.00";
            }
        }

        private void sterms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void rcvamt_KeyDown(object sender, KeyEventArgs e)
        {
            //CalNetAmt();
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void rcvamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //CalNetAmt();
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

        private void rcvamt_Leave(object sender, EventArgs e)
        {
            if (rcvamt.Text == "" || rcvamt.Text == ".")
            {
                rcvamt.Text = "0.00";
            }

            if (Convert.ToInt32(txtBookmasterId.Text) == 0)
            {
                decimal adpayment = Convert.ToDecimal(txtadvpa.Text);
                decimal balAmnt = adpayment - Convert.ToDecimal(rcvamt.Text);
                lblBalAmt.Text = balAmnt.ToString("N2");
            }
            else
            {
                decimal adpayment = Convert.ToDecimal(txtadvpa.Text);
                decimal bal = Convert.ToDecimal(lblBalAmt.Text);
                decimal net = Convert.ToDecimal(netamount.Text);
                decimal balanceneed = Convert.ToDecimal(lblBAmt.Text);
                decimal cashreciv = Convert.ToDecimal(rcvamt.Text);
                bal = balanceneed - cashreciv;
                lblBalAmt.Text = bal.ToString();

            }

        }
        public void SalesGridCalc(int r, int c)
        {
            try
            {
                decimal taxper = 0, qty = 0, price = 0, amount = 0, taxamt = 0, disamt = 0;
                int taxsettingid = 0;
                qty = Convert.ToDecimal(dgvmenu.Rows[r].Cells[iqty].Value);
                price = Convert.ToDecimal(dgvmenu.Rows[r].Cells[imprice].Value);

                amount = Convert.ToDecimal(dgvmenu.Rows[r].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[r].Cells[iqty].Value);
                dgvmenu.Rows[r].Cells[isamount].Value = amount.ToString("N2");


            }
            catch (Exception ex)
            {

            }
        }
        //public void calmenu()
        //{
        //    if (Convert.ToDecimal(txtBookmasterId.Text) != 0)
        //    {
        //        decimal adpayment = Convert.ToDecimal(txtadvpa.Text);
        //        decimal bal = Convert.ToDecimal(lblBalAmt.Text);
        //        decimal net = Convert.ToDecimal(netamount.Text);
        //        decimal balanceneed = Convert.ToDecimal(lblBAmt.Text);
        //        decimal cashreciv = Convert.ToDecimal(rcvamt.Text);
        //        //bal = balanceneed - cashreciv;
        //        //lblBalAmt.Text = bal.ToString();
        //        balanceneed = net - adpayment;
        //        lblBAmt.Text = balanceneed.ToString("0.0");

        //        if (Convert.ToDecimal(lblBAmt.Text) == Convert.ToDecimal(rcvamt.Text))
        //        {
        //            adpayment = adpayment + cashreciv;
        //            txtadvpa.Text = adpayment.ToString("0.0");
        //        }
        //        //if(Convert.ToDecimal(lblBalAmt.Text)==0)
        //        //{
        //        //    balanceneed = 0;
        //        //    lblBAmt.Text = balanceneed.ToString("0.0");
        //        //}

        //    }
        //}
        private void rcvamt_TextChanged(object sender, EventArgs e)
        {

            if (rcvamt.Text == "" || rcvamt.Text == ".")
            {
                rcvamt.Text = "0.00";
            }
            if (txtBookmasterId.Text != "0")
            {
                lblBalAmt.Text = (Convert.ToDecimal(lblBAmt.Text) - Convert.ToDecimal(rcvamt.Text)).ToString();
            }
            //calmenu();
            //CalNetAmt();
            //decimal balAmnt = Convert.ToDecimal(lblNetAmt.Text) - Convert.ToDecimal(txtsrCashRev.Text);
            //balAmnt = Convert.ToDecimal(lblBalAmt.Text);
        }

        private void txtadvpa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void dgvmenu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void Sum()
        {
            if (decimal.TryParse(lblSTotal.Text, out decimal value1) && decimal.TryParse(lblMTotal.Text, out decimal value2))
            {
                // Calculate the sum
                decimal sum = value1 + value2;

                // Display the sum in the resultTextBox
                lblGTotal.Text = sum.ToString();
            }

        }

        private void dgvforms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvmenu_Leave(object sender, EventArgs e)
        {
            Sum();
        }

        private void dgvforms_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                // Specify the column index for which you want to calculate the sum


                decimal totalSum = 0;

                foreach (DataGridViewRow row in dgvforms.Rows)
                {
                    if (row.Cells[iprice].Value != null)
                    {
                        decimal cellValue;
                        if (decimal.TryParse(row.Cells[iprice].Value.ToString(), out cellValue))
                        {
                            totalSum += cellValue;
                        }
                    }
                }

                // Display the sum in a TextBox
                lblSTotal.Text = totalSum.ToString();
            }
            Sum();
        }
        //public void Arrangepostion()
        //{
        //    panel15.Size = new Size(1182, 59);
        //}
        private void dgvmenu_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[isamount].Value = Convert.ToDecimal(dgvmenu.Rows[e.RowIndex].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[e.RowIndex].Cells[iqty].Value);
            if (e.ColumnIndex == iqty && e.RowIndex >= 0)
            {
                // Specify the column index for which you want to calculate the sum


                decimal totalSum = 0;

                foreach (DataGridViewRow row in dgvmenu.Rows)
                {
                    if (row.Cells[isamount].Value != null)
                    {
                        decimal cellValue;
                        if (decimal.TryParse(row.Cells[isamount].Value.ToString(), out cellValue))
                        {
                            totalSum += cellValue;
                        }
                    }
                }

                // Display the sum in a TextBox
                lblMTotal.Text = totalSum.ToString();
            }

            Sum();
        }

        private void txtadvpa_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtadvpa_Leave(object sender, EventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtadvpa.Text, out value))
            {
                txtadvpa.Text = "0.00";
            }

            if (Convert.ToInt32(txtBookmasterId.Text) == 0)
            {
                decimal adpayment = Convert.ToDecimal(txtadvpa.Text);
                decimal balAmnt = adpayment - Convert.ToDecimal(rcvamt.Text);
                lblBalAmt.Text = balAmnt.ToString("N2");
            }
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void mbtnList_Click_1(object sender, EventArgs e)
        {
            Reservationlist();
        }

        private void mbtnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mbtnRefresh_Click_1(object sender, EventArgs e)
        {
            clear();
        }
        public void save()
        {
            try
            {
                bool CUst = false;
                if (MandatoryCheck())
                {

                    BllReservation bllReservation = new BllReservation();
                    BllTableinfo bllTableinfo = new BllTableinfo();
                    DmnReservation dmnReservation = new DmnReservation();
                    DmnTableinfo dmnTableinfo = new DmnTableinfo();
                    //dmnReservation.BookmasterId = BookmasterId;
                    string dtfrom = dtpBooking.Value.ToString("yyyy-MM-dd") + " " + dtp_from_time.Value.ToString("HH:mm:ss");
                    dmnReservation.BookmasterId = Convert.ToInt32(txtBookmasterId.Text);
                    dmnReservation.cusName = txtNam.Text;
                    dmnReservation.Mobile = txtMob.Text;
                    dmnReservation.Address = txtAddress.Text;
                    dmnReservation.EmailId = txtEmail.Text;
                    dmnReservation.Remarks = txtRemarks.Text;
                    dmnReservation.gender = Convert.ToString(cboGender.SelectedValue);
                    dmnReservation.CreatedBy = 1;
                    dmnReservation.Status = 1;
                    dmnReservation.BookingType = Convert.ToString(cboBtype.SelectedValue);
                    dmnReservation.NoOfPeople = txtNOP.Text;
                    dmnReservation.BranchId = 1030;
                    dmnReservation.BookingDateTime = Convert.ToDateTime(dtfrom);

                    dmnReservation.ReservationTotal = Convert.ToDecimal(lblSTotal.Text);
                    dmnReservation.MenuTotal = Convert.ToDecimal(lblMTotal.Text);
                    dmnReservation.GrandTotal = Convert.ToDecimal(lblGTotal.Text);
                    dmnReservation.DiscountAmt = Convert.ToDecimal(totaldiscountamt.Text);
                    //dmnTableinfo.RevStatus = 1;
                    dmnReservation.DiscountPer = Convert.ToDecimal(discountper.Text);
                    dmnReservation.OtherAdd = Convert.ToDecimal(oaamount.Text);
                    dmnReservation.OtherDed = Convert.ToDecimal(odamt.Text);
                    dmnReservation.OtherAddText = oadesc.Text;
                    dmnReservation.OtherDedText = oddesc.Text;
                    dmnReservation.NetAmount = Convert.ToDecimal(netamount.Text);
                    dmnReservation.Terms = sterms.Text;
                    dmnReservation.BalnceNeed = Convert.ToDecimal(lblBAmt.Text);
                    if (Convert.ToDecimal(txtBookmasterId.Text) != 0)
                    {
                        if (Convert.ToDecimal(lblBalAmt.Text) > 0)
                        {
                            txtadvpa.Text = (Convert.ToDecimal(txtadvpa.Text) + Convert.ToDecimal(rcvamt.Text)).ToString();
                        }
                        else
                        {
                            dmnReservation.FinalAmount = dmnReservation.BalnceNeed;
                            dmnReservation.BalnceNeed = 0;
                        }


                    }
                    else
                    {
                        if (Convert.ToDecimal(lblBalAmt.Text) <= 0 && Convert.ToDecimal(txtadvpa.Text) == 0)
                        {

                            dmnReservation.FinalAmount = dmnReservation.BalnceNeed;
                            dmnReservation.BalnceNeed = 0;
                        }

                    }

                    dmnReservation.AdvancePay = Convert.ToDecimal(txtadvpa.Text);
                    dmnReservation.CashRev = Convert.ToDecimal(rcvamt.Text);
                    dmnReservation.BalnceAmt = Convert.ToDecimal(lblBalAmt.Text);
                    if (chb_active.Checked)
                        dmnReservation.Status = 1;
                    else
                        dmnReservation.Status = 0;

                    var InsertStatusP = bllReservation.AddData("prdInsert_Upd_bookMaster", dmnReservation);

                    if (InsertStatusP.BookmasterId != 0)
                    {
                        bool datadetails = false;
                        DmnReservationDetails dmnReservation1 = new DmnReservationDetails();
                        for (int i = 0; i < dgvforms.Rows.Count - 1; i++)
                        {

                            if (Convert.ToString(dgvforms.Rows[i].Cells[itid].Value) != null && Convert.ToString(dgvforms.Rows[i].Cells[itid].Value) != "")
                            {
                                dmnReservation1.MTID = Convert.ToInt32(dgvforms.Rows[i].Cells[itid].Value);

                                dmnReservation1.BookmasterId = InsertStatusP.BookmasterId;
                                dmnReservation1.BookdetailId = Convert.ToInt32(dgvforms.Rows[i].Cells[iTBookingDetailId].Value);

                                dmnReservation1.Name = Convert.ToString(dgvforms.Rows[i].Cells[itablename].Value);
                                dmnReservation1.Quantity = Convert.ToInt32(dgvforms.Rows[i].Cells[icapacity].Value);
                                dmnReservation1.Price = Convert.ToInt32(dgvforms.Rows[i].Cells[iprice].Value);
                                dmnReservation1.Description = Convert.ToString(dgvforms.Rows[i].Cells[idescription].Value);
                                dmnReservation1.CreatedBy = 1;
                                dmnReservation1.BranchId = 1030;
                                dmnReservation1.Rid = Convert.ToInt32(TableMenu.Rid);


                                if (chb_active.Checked)
                                    dmnReservation1.Status = 1;
                                else
                                    dmnReservation1.Status = 0;

                                datadetails = bllReservation.AddDetails("prdInsert_Upd_bookDetails", dmnReservation1);

                            }

                        }
                        for (int i = 0; i < dgvmenu.Rows.Count - 1; i++)
                        {

                            if (Convert.ToString(dgvmenu.Rows[i].Cells[imid].Value) != null && Convert.ToString(dgvmenu.Rows[i].Cells[imid].Value) != "")
                            {
                                dmnReservation1.MTID = Convert.ToInt32(dgvmenu.Rows[i].Cells[imid].Value);

                                dmnReservation1.BookmasterId = InsertStatusP.BookmasterId;
                                dmnReservation1.BookdetailId = Convert.ToInt32(dgvmenu.Rows[i].Cells[imBookingDetailId].Value);
                                dmnReservation1.Name = Convert.ToString(dgvmenu.Rows[i].Cells[imname].Value);
                                dmnReservation1.Quantity = Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value);
                                dmnReservation1.Price = Convert.ToInt32(dgvmenu.Rows[i].Cells[imprice].Value);
                                dmnReservation1.Description = Convert.ToString(dgvmenu.Rows[i].Cells[imdescription].Value);
                                dmnReservation1.CreatedBy = 1;
                                dmnReservation1.BranchId = 1030;
                                dmnReservation1.Rid = Convert.ToInt32(TableMenu.Mid);
                                dmnReservation1.Status = 1;
                                dmnReservation1.Net = Convert.ToDecimal(dgvmenu.Rows[i].Cells[isamount].Value);
                                datadetails = bllReservation.AddDetails("prdInsert_Upd_bookDetails", dmnReservation1);

                            }

                        }
                        if (chb_active.Checked == false)
                        {
                            CMessageBox.Show("Reservation Deleted", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            btn_delt.Visible = false;
                        }
                        if (datadetails == true && chb_active.Checked == false)
                        {
                            MessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (datadetails == false)
                        {
                            MessageBox.Show("Unable To Save  ", "Error On Save", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    //else
                    //{
                    //    bool saveStatus = bllReservation.UpdateData("prdInsert_Upd_bookDetails", dmnReservation);


                    //    if (saveStatus == true)
                    //    {
                    //        MessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    }
                    //    else if (saveStatus == false)
                    //    {
                    //        MessageBox.Show("Unable To Save  ", "Error On Save", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //    }
                    //}
                    clear();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void mbtnSave_Click_1(object sender, EventArgs e)
        {
            save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void cboGender_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(cboGender.SelectedValue) == 1)
            //{
            //    pictureBox3.Image = FoodandBeverage.Properties.Resources.Profilepicmale;
            //}
            //else if (Convert.ToInt32(cboGender.SelectedValue) == 2)
            //{
            //    pictureBox3.Image = FoodandBeverage.Properties.Resources.Profilepicfemale;
            //    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            //}
            //else if (Convert.ToInt32(cboGender.SelectedValue) == 3)
            //{
            //    pictureBox3.Image = FoodandBeverage.Properties.Resources.Admin_1;
            //    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            //}
        }

        private void btn_delt_Click(object sender, EventArgs e)
        {
            
            var result = CMessageBox.Show("Do You want Delete This Reservation", "Confirm The Action", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                chb_active.Checked = false;
                save();
                //Fillitem();
            }
        }
    }
}
