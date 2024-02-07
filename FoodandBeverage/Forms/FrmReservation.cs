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
using System.Drawing;

using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;

namespace FoodandBeverage.Forms
{
    public partial class FrmReservation : Form
    {
        ClsGeneral ClsGeneral = new ClsGeneral();
        ClsDocument clsDocument = new ClsDocument();
        long BranchId = 1030;
        public int finalqty = 0;
        public int BookmasterId = 0;
        public FrmReservation()
        {
            InitializeComponent();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
           int nLeftRect,
           int nTopRect,
           int nRightRect,
           int nBottomRect,
           int nWidthEllipse,
           int nHeightEllipse
       );
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
        public static int iTaxper = 6;
        public static int iFormType = 7;
        public static int iTax = 8;
        public static int iTaxType = 9;
        public static int istaxamt = 10;
        public static int isamount = 11;
        public static int imgst = 12;
        public static int icgst = 13;
        public static int ivat = 14;
        public static int isgst = 15;
        public static int iprate = 16;
        public static int iunit = 17;
        public static int iitemcode = 18;
        public static int iprid = 19;
        public static int imrp = 20;
        public static int itabletype = 21;
        public static int iproductType = 22;
        public static int imarketprice = 23;
        public static int itaxsettingid = 24;
        public static int iqtyadd = 25;
        public static int idisamt = 26;
        public static int idisper = 27;
        public static int  iQOH = 28;

        public static int slno = 0;
        public static int prId = 1;
        public static int iiDescription = 2;
        public static int Srate = 3;
        public static int qt = 4;

        private int yourTargetColumnIndex = 1;    
     
        private void FrmReservation_Load(object sender, EventArgs e)
        {

            richTextBoxPhone.Rtf = "{\\rtf1\\ansi " +
            "Phone No \\i0 *\\ulnone\\i\\b " +
               "}";
            richTextBoxPhone.Select(richTextBoxPhone.Text.IndexOf("*"), 1);
            richTextBoxPhone.SelectionColor = Color.Red;
            richTextBoxPhone.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            richTextBoxName.Rtf = "{\\rtf1\\ansi " +
            "Name \\i0 *\\ulnone\\i\\b " +
               "}";
            richTextBoxName.Select(richTextBoxName.Text.IndexOf("*"), 1);
            richTextBoxName.SelectionColor = Color.Red;
            richTextBoxName.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            richTextBoxAddress.Rtf = "{\\rtf1\\ansi " +
           "Address \\i0 *\\ulnone\\i\\b " +
              "}";
            richTextBoxAddress.Select(richTextBoxAddress.Text.IndexOf("*"), 1);
            richTextBoxAddress.SelectionColor = Color.Red;
            richTextBoxAddress.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            richTextBoxEmail.Rtf = "{\\rtf1\\ansi " +
           "Email \\i0 *\\ulnone\\i\\b " +
              "}";
            richTextBoxEmail.Select(richTextBoxEmail.Text.IndexOf("*"), 1);
            richTextBoxEmail.SelectionColor = Color.Red;
            richTextBoxEmail.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            richTextBoxGender.Rtf = "{\\rtf1\\ansi " +
          "Gender \\i0 *\\ulnone\\i\\b " +
             "}";
            richTextBoxGender.Select(richTextBoxGender.Text.IndexOf("*"), 1);
            richTextBoxGender.SelectionColor = Color.Red;
            richTextBoxGender.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center

            dgvforms.BorderStyle = BorderStyle.None;
            dgvforms.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvforms.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvforms.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvforms.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvforms.BackgroundColor = Color.White;

            dgvforms.EnableHeadersVisualStyles = false;
            dgvforms.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvforms.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvforms.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvmenu.BorderStyle = BorderStyle.None;
            dgvmenu.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvmenu.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvmenu.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvmenu.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvmenu.BackgroundColor = Color.White;

            dgvmenu.EnableHeadersVisualStyles = false;
            dgvmenu.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvmenu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvmenu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvItemPop.BorderStyle = BorderStyle.None;
            dgvItemPop.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvItemPop.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvItemPop.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvItemPop.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvItemPop.BackgroundColor = Color.White;

            dgvItemPop.EnableHeadersVisualStyles = false;
            dgvItemPop.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvItemPop.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvItemPop.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgv_spopup.BorderStyle = BorderStyle.None;
            dgv_spopup.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_spopup.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_spopup.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_spopup.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_spopup.BackgroundColor = Color.White;

            dgv_spopup.EnableHeadersVisualStyles = false;
            dgv_spopup.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_spopup.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_spopup.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //dgvItemPop.Visible = false;
            panelProductPopup.Visible = false;
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
            redpan.Visible = false;

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

        #region TexBox Round function
        public void textb_shape()
        {
            try
            { 
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtNam, txtAddress, txtMob, txtEmail, txtNOP, txtRemarks, discountper, totaldiscountamt, oaamount, oadesc, odamt, oddesc, sterms, netamount, txtadvpa, rcvamt })
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
            txtordermasterid.Text = "0";
            lblSTotal.Text = "";
            lblMTotal.Text = "";
            lblGTotal.Text = "";
            discountper.Text = "";
            totaldiscountamt.Text = "0.00";
            oaamount.Text = "0.00";
            oadesc.Text = "";
            odamt.Text = "0.00";
            oddesc.Text = "";
            netamount.Text = "0.00";
            sterms.Text = "";
            txtadvpa.Text = "0.00";
            rcvamt.Text = "0.00";
            lblBalAmt.Text = "0.00";
            lblNetAmt.Text = "0.00";
            lblBAmt.Text = "0.00";
            discountper.Text = "0.00";
            label21.Visible = false;
            lblBAmt.Visible = false;
            panelProductPopup.Visible = false;
            //pnlCustHead.Hide();
            //dgvItemPop.Visible = false;
            dgvmenu.Enabled = true;
            dgvmenu.Focus();
            dtpBooking.Value = DateTime.Now;
            redpan.Visible = false;
            dtp_from_time.Value = DateTime.Now;

            tableLayoutPanel10.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            tableLayoutPanel10.ColumnStyles[1] = new ColumnStyle(SizeType.AutoSize);
            btn_delt.Visible = false;
        }
        public bool CheckEmailId(Form form, string EmailId)
        {
            Regex mRegxExpression;
            mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
            if (!mRegxExpression.IsMatch(EmailId.Trim()))
            {
                CMessageBox.Show("Invalid email format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    CMessageBox.Show("Capacity is not sufficient for the people", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        CMessageBox.Show("Quantity cannot be less than one. Please enter a valid quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        dgvmenu.Focus();
                        return false;
                    }
                }
            }
            if (txtNam.Text == "" || txtNam.Text == string.Empty)
            {
                CMessageBox.Show("Please enter your username. This field is mandatory.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //CMessageBox.Show("Please Select IGST %", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show(" UserName is A Mandatory Field ", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtMob.Text == "" || txtMob.Text == null)
            {
                DialogResult result = CMessageBox.Show("Please provide your mobile number. This field is mandatory.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtMob.Focus();
                return false;
            }
            else if (txtEmail.Text == "" || txtEmail.Text == string.Empty)
            {
                CMessageBox.Show("Please provide your email address. This field is mandatory.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (cboGender.SelectedIndex == 0 || cboGender.SelectedIndex == -1)
            {
                CMessageBox.Show("Please specify your gender. This information is mandatory.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGender.Focus();
                return false;
            }
            else if (txtAddress.Text == "" || txtAddress.Text == string.Empty)
            {
                CMessageBox.Show("Please enter your address. This field is mandatory.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (Convert.ToString(dgvforms.Rows[0].Cells[itablename].Value) == "" || (Convert.ToString(dgvforms.Rows[0].Cells[itablename].Value) == null))
             {
                    CMessageBox.Show("Please select a table first. This is a mandatory field.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
             }
            else if (lblGTotal.Text== "00")
            {
                CMessageBox.Show("Fill The Balance Details", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            CMessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (datadetails == false)
                        {
                            CMessageBox.Show("Unable To Save ", "Error On Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            

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
            if (e.KeyCode == Keys.Enter )
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
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
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
                txtRemarks.Focus();
            }
        }

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvforms.Focus();
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
                BllSearch<DmnGridFields> _bllSearch = new BllSearch<DmnGridFields>();
                string returnValue = string.Empty;
                
                DmnSearch _objSearch = new DmnSearch();

                panelProductPopup.Location = new Point(
                             this.ClientSize.Width / 2 - panelProductPopup.Size.Width / 2,
                             (this.ClientSize.Height / 3 - panelProductPopup.Size.Height / 2));
                panelProductPopup.Anchor = AnchorStyles.None;
                panelProductPopup.Visible = true;
                int panelWidthPercentage = 57;
                int panelheightPercentage = 25;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                panelProductPopup.Size = new Size(panelWidth, panelHeight);
                //panelProductPopup.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                //redpan.Visible = true;

                //dgvItemPop.Visible = true;
                dgvItemPop.Rows.Clear();
                
                _objSearch.Select = "MId as Id , Name as Field1,p.Price as Field2,  'Menu' as Field3, "+Convert.ToInt32(formsenum.Menu) + " Field4 ,p.Tax as Field5,p.TaxType as Field6 ";
                _objSearch.Table = " tblMenu m   left join tblProduct p on m.ProductId = p.ProductId and " +
                    "   m.BranchId =  "+FrmLogin.BranchId +" where m.Status = 1 and p.Status = 1 " +
                    "   UNION " +
                    "   select i.ItemId  as Id, i.Itemname as Field1," +
                    "(Select Top(1)Srate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field2," +
                    " 'Item' as Field3, " + Convert.ToInt32(formsenum.Item) + " Field4 ,i.Tax as Field5,i.TaxType as Field6 " +
                    " from tblItem i  where Saletype=1" +
                    " UNION select ofm.Omid as Id,ofm.OfferName Field1,ofm.FinalPrice Field2,'Offer' as Field3," + Convert.ToInt32(formsenum.Discount_And_Offer) + " Field4 , ofm.Tax Field5,ofm.TaxType Field6 from " +
                    " tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofm.Omid=ofd.Omid ";
                _objSearch.WhereNdGroupBy = " Where ofd.Status=1 and ofm.OfferType = 1";
                _objSearch.OrderBy = "";
                var gridSource = _bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource.Count > 0)
                {
                    dgvmenu.Enabled = false;
                    dgvItemPop.Rows.Add(gridSource.Count);
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        dgvItemPop.Rows[i].Cells[0].Value = gridSource[i].Id;
                        dgvItemPop.Rows[i].Cells[1].Value = i + 1;
                        dgvItemPop.Rows[i].Cells[2].Value = gridSource[i].Field1;
                        dgvItemPop.Rows[i].Cells[3].Value = gridSource[i].Field2;
                        dgvItemPop.Rows[i].Cells[4].Value = gridSource[i].Field3;
                        dgvItemPop.Rows[i].Cells[5].Value = gridSource[i].Field4;
                        dgvItemPop.Rows[i].Cells[6].Value = gridSource[i].Field5;
                        dgvItemPop.Rows[i].Cells[7].Value = gridSource[i].Field6;
                        
                    }
                }
                panelProductPopup.Show();
                //pnlCustHead.Hide();
               
                dgvItemPop.Focus();




               
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
               
                if (gridSource.Count > 0)
                {
                    int qty = 1;
                    
                    bool flag = true;
                    int i = 0;
                    for (i = 0; i <= dgvforms.RowCount - 1; i++)
                    {
                        if (Convert.ToInt32(dgvforms.Rows[i].Cells[itid].Value) == gridSource[0].Tid)
                        {
                            flag = false;
                            //qty += Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value);
                           


                            break;

                        }

                    }
                    //if (gridSource[0].QOH >= qty)
                    //{
                    if (flag == true)
                    {
                        if (dgvforms.Rows.Count - 1 == dgvforms.CurrentCell.RowIndex)
                        {
                            dgvforms.Rows.Add(1);
                        }
                        dgvforms.Rows[r].Cells[itid].Value = gridSource[0].Tid;
                        dgvforms.Rows[r].Cells[iTBookingDetailId].Value = 0;
                        dgvforms.Rows[r].Cells[itablename].Value = gridSource[0].TableCode;
                        dgvforms.Rows[r].Cells[iprice].Value = gridSource[0].Price;
                        dgvforms.Rows[r].Cells[icapacity].Value = gridSource[0].Capcity;
                        dgvforms.Rows[r].Cells[idescription].Value = gridSource[0].Decription;
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        private void dgvforms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (dgvforms.CurrentCell.ColumnIndex == idescription && dgvforms.CurrentCell.RowIndex == dgvforms.Rows.Count - 1)
                {                    
                        discountper.Focus();                    
                }
                else
                {
                        e.SuppressKeyPress = true;
                        SendKeys.Send("{TAB}");
                }
            }
            if (e.KeyData == Keys.F8 && dgvforms.CurrentCell.ColumnIndex == itablename)
            {
                lblToolTipPopup.Visible = false;
                if (Convert.ToInt32(cboBtype.SelectedValue) == (Convert.ToInt32(Reservation.TableBooking)))
                {

                    purchasepop(Convert.ToInt32(cboBtype.SelectedValue));
                }
                else
                {
                    CMessageBox.Show("First Choose the Booking Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //dgvforms.Rows.Add(1);
            }
            else if (e.KeyData == Keys.Delete)
            {
                dgvforms.Rows.RemoveAt(dgvforms.CurrentCell.RowIndex);
                if (dgvforms.Rows.Count == 0)
                {
                    dgvforms.Rows.Add(1);
                }
            }
            //gridefllow(e);
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
                if(dgvmenu.Rows.Count==0)
                {
                    dgvmenu.Rows.Add(1);
                }
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
                //btn_delt.Visible = true;
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

                        tableLayoutPanel10.ColumnStyles[0] = new ColumnStyle(SizeType.Percent,80);
                        tableLayoutPanel10.ColumnStyles[1] = new ColumnStyle(SizeType.Percent,20);
                        tableLayoutPanel10.Controls.Add(btn_delt, 1,0);
                        btn_delt.Visible = true;

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
                        rcvamt.Text = Convert.ToDecimal(gridSource1[0].CashRev).ToString();
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
                            label21.Text = "Balance To Pay :";
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
                                    dgvmenu.Rows[r1].Cells[iTax].Value = gridSource2[i].Tax;
                                    dgvmenu.Rows[r1].Cells[istaxamt].Value = gridSource2[i].TaxAmt;
                                    dgvmenu.Rows[r1].Cells[iTaxper].Value = gridSource2[i].TaxPer;
                                    dgvmenu.Rows[r1].Cells[iTaxType].Value = gridSource2[i].TaxType;
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
                    CMessageBox.Show("Capacity is not sufficient for the people", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }

            }

        }
        private void dgvforms_Leave(object sender, EventArgs e)
        {
            CapacityCheck();
            Sum();
            CalNetAmt();
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
            try
            {
                if (e.ColumnIndex == Convert.ToInt32(iqty)) // Replace 'yourAlphabeticColumnIndex' with the actual index of the column you want to validate
                {
                    string value = e.FormattedValue.ToString();


                    if (ContainsAlphabets(value))
                    {
                        e.Cancel = true;
                        CMessageBox.Show("Alphabetic characters are not allowed in this column.");
                    dgvmenu.Rows[e.RowIndex].Cells[iqty].Value = "1";
                    }
                }
                if (e.ColumnIndex == iqty && dgvmenu.Rows[e.RowIndex].Cells[iqty].Value != "")
                {
                    int qoh = Convert.ToInt32(dgvmenu.Rows[e.RowIndex].Cells[iQOH].Value);
                    int qty = Convert.ToInt32(dgvmenu.Rows[e.RowIndex].Cells[iqty].EditedFormattedValue);

                    if (qty > qoh && Convert.ToInt32(dgvmenu.Rows[e.RowIndex].Cells[iFormType].Value) == Convert.ToInt32(formsenum.Item))
                    {
                        if (Convert.ToString(dgvmenu.Rows[e.RowIndex].Cells[imid].Value) == "")
                        {
                            dgvmenu.Rows[e.RowIndex].Cells[iqty].Value = qoh;
                        }
                        else
                        {
                            CMessageBox.Show("Avilable Stock Quanity Is " + qoh, "Insufficent Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgvmenu.Rows[e.RowIndex].Cells[iqty].Value = qoh;
                            dgvmenu.CurrentCell = dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[iqty];

                            e.Cancel = true;
                        }
                    }
                    else
                    {

                        dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[isamount].Value = Convert.ToDecimal(dgvmenu.Rows[e.RowIndex].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[e.RowIndex].Cells[iqty].Value);
                        dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[iqty].Value = qty;

                    }

                }
            }
            catch(Exception ex)
            {

            }

            
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
                    //sdiscountAmnt();
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
                oaamount.Focus();
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
                        //sdiscountAmnt();
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
                sterms.Focus();
            }
        }

        private void netamount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtadvpa.Focus();
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
                netamount.Focus();
            }
        }

        private void rcvamt_KeyDown(object sender, KeyEventArgs e)
        {
            //CalNetAmt();
            if (e.KeyData == Keys.Enter)
            {
                mbtnSave.Focus();
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
            decimal value;
            if (!decimal.TryParse(rcvamt.Text, out value))
            {
                rcvamt.Text = "0";
            }
            if (!decimal.TryParse(txtadvpa.Text, out value))
            {
                txtadvpa.Text = "0";
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
            try
            {

                if (rcvamt.Text == "" || rcvamt.Text == "." || rcvamt.Text == ".0.00")
                {
                    rcvamt.Text = "0.00";
                }
                if (txtBookmasterId.Text == "0")
                {
                    lblBalAmt.Text = (Convert.ToDecimal(lblBAmt.Text) - Convert.ToDecimal(rcvamt.Text)).ToString();
                }
            }
            catch(Exception ex)
            { }
        }

        private void txtadvpa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                rcvamt.Focus();
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
            CalNetAmt();
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
                    BllOrder bllOrder = new BllOrder();
                    BllReservation bllReservation = new BllReservation();
                    DmnOrderMaster dmnordermaster = new DmnOrderMaster();
                    DmnOrderDetails dmnOrderDetails = new DmnOrderDetails();
                    DmnReservation dmnReservation = new DmnReservation();


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
                                dmnReservation1.Tax = Convert.ToString(dgvmenu.Rows[i].Cells[iTax].Value);
                                dmnReservation1.TaxType = Convert.ToString(dgvmenu.Rows[i].Cells[iTaxType].Value);
                                dmnReservation1.TaxPer = Convert.ToDecimal(dgvmenu.Rows[i].Cells[iTaxper].Value);
                                dmnReservation1.TaxAmt = Convert.ToString(dgvmenu.Rows[i].Cells[istaxamt].Value);
                                dmnReservation1.IGSTAmount= Convert.ToDecimal(dgvmenu.Rows[i].Cells[imgst].Value);
                                dmnReservation1.CGSTAmount= Convert.ToDecimal(dgvmenu.Rows[i].Cells[icgst].Value);
                                dmnReservation1.SGSTAmount= Convert.ToDecimal(dgvmenu.Rows[i].Cells[isgst].Value);
                                dmnReservation1.VATAmount= Convert.ToDecimal(dgvmenu.Rows[i].Cells[ivat].Value);
                                dmnReservation1.Description = Convert.ToString(dgvmenu.Rows[i].Cells[imdescription].Value);
                                dmnReservation1.CreatedBy = 1;
                                dmnReservation1.BranchId = 1030;
                                dmnReservation1.Rid = Convert.ToInt32(TableMenu.Mid);
                                dmnReservation1.Status = 1;
                                dmnReservation1.Net = Convert.ToDecimal(dgvmenu.Rows[i].Cells[isamount].Value);
                                datadetails = bllReservation.AddDetails("prdInsert_Upd_bookDetails", dmnReservation1);

                            }

                        }
                             int sum = 0;
                        int n = dgvmenu.RowCount;
                        for (int i = 0; i < n; i++)
                        {
                            if (Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value) != null && Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value) > 0)
                                sum = sum + Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value);
                        }



                        dmnordermaster.OrderMasterId = 0;
                        dmnordermaster.EntryNo = "";
                        dmnordermaster.BookmasterId= InsertStatusP.BookmasterId;
                        dmnordermaster.TotalQuantity = Convert.ToInt32(sum);
                        dmnordermaster.Remarks = txtRemarks.Text;
                        dmnordermaster.Address = txtAddress.Text;
                        dmnordermaster.Telephone = txtMob.Text;
                        dmnordermaster.CustomerName = txtNam.Text;
                        dmnordermaster.Gender = Convert.ToInt32(cboGender.SelectedValue);
                        dmnordermaster.Email = txtEmail.Text;
                        dmnordermaster.BranchId = FrmDashboard.BranchId;
                        dmnordermaster.CreatedBy = 1;
                        dmnordermaster.Status = 1;
                        dmnordermaster.Tid = 0;
                        var InsertStatusO = bllOrder.AddData("prdInsert_Upd_OrderMaster", dmnordermaster);



                        if (InsertStatusO.OrderMasterId != 0)
                        {

                            for (int i = 0; i < dgvmenu.Rows.Count - 1; i++)
                            {

                                if (Convert.ToString(dgvmenu.Rows[i].Cells[imid].Value) != null && Convert.ToString(dgvmenu.Rows[i].Cells[imid].Value) != "")
                                {
                                    dmnOrderDetails.ItemId = Convert.ToInt32(dgvmenu.Rows[i].Cells[imid].Value);

                                    dmnOrderDetails.OrderMasterId = InsertStatusO.OrderMasterId;
                                    dmnOrderDetails.OrderDetailsId = 0;
                                    dmnOrderDetails.ItemName = Convert.ToString(dgvmenu.Rows[i].Cells[imname].Value);
                                    dmnOrderDetails.Quantity = Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value);
                                    dmnOrderDetails.Srate = Convert.ToInt32(dgvmenu.Rows[i].Cells[imprice].Value);
                                    dmnOrderDetails.Notes = Convert.ToString(dgvmenu.Rows[i].Cells[imdescription].Value);
                                    dmnOrderDetails.Tax = Convert.ToString(dgvmenu.Rows[i].Cells[iTax].Value);
                                    dmnOrderDetails.TaxType = Convert.ToString(dgvmenu.Rows[i].Cells[iTaxType].Value);
                                    dmnOrderDetails.TaxPer = Convert.ToDecimal(dgvmenu.Rows[i].Cells[iTaxper].Value);
                                    dmnOrderDetails.TaxAmount = Convert.ToDecimal(dgvmenu.Rows[i].Cells[istaxamt].Value);
                                    dmnOrderDetails.MRP = Convert.ToDecimal(dgvmenu.Rows[i].Cells[imrp].Value);
                                    dmnOrderDetails.IGSTAmount = Convert.ToDecimal(dgvmenu.Rows[i].Cells[imgst].Value);
                                    dmnOrderDetails.CGSTAmount = Convert.ToDecimal(dgvmenu.Rows[i].Cells[icgst].Value);
                                    dmnOrderDetails.SGSTAmount = Convert.ToDecimal(dgvmenu.Rows[i].Cells[isgst].Value);
                                    dmnOrderDetails.PurchaseRate = Convert.ToDecimal(dgvmenu.Rows[i].Cells[iprate].Value);
                                    dmnOrderDetails.PrId = Convert.ToInt32(dgvmenu.Rows[i].Cells[iprid].Value);
                                    dmnOrderDetails.VATAmount = Convert.ToDecimal(dgvmenu.Rows[i].Cells[ivat].Value);
                                    dmnOrderDetails.CreatedBy = 1;
                                    dmnOrderDetails.BranchId = 1030;
                                    dmnOrderDetails.Status = 4;
                                    dmnOrderDetails.UnitId = Convert.ToInt32(dgvmenu.Rows[i].Cells[iunit].Value);
                                    dmnOrderDetails.Amount = Convert.ToDecimal(dgvmenu.Rows[i].Cells[isamount].Value);
                                    dmnOrderDetails.ItemBarCode = Convert.ToString(dgvmenu.Rows[i].Cells[iitemcode].Value);
                                    dmnOrderDetails.ProductType = Convert.ToInt32(dgvmenu.Rows[i].Cells[iproductType].Value);
                                    dmnOrderDetails.TableType = Convert.ToInt32(dgvmenu.Rows[i].Cells[itabletype].Value);
                                    dmnOrderDetails.MarketPrice = Convert.ToInt32(dgvmenu.Rows[i].Cells[imarketprice].Value);
                                    dmnOrderDetails.Fcid = Convert.ToInt32(dgvmenu.Rows[i].Cells[iproductType].Value);
                                    dmnOrderDetails.DiscountAmount= Convert.ToDecimal(dgvmenu.Rows[i].Cells[idisamt].Value);
                                    dmnOrderDetails.DiscountPer= Convert.ToDecimal(dgvmenu.Rows[i].Cells[idisper].Value);
                                    dmnOrderDetails.PurchaseRate = Convert.ToInt32(dgvmenu.Rows[i].Cells[iprate].Value);
                                    dmnOrderDetails.NetAmount = Convert.ToDecimal(dgvmenu.Rows[i].Cells[isamount].Value);
                                    datadetails = bllOrder.AddDetail("prdInsert_Upd_OrderDetailsUpdate", dmnOrderDetails);
                                }
                            }
                            }
                            if (chb_active.Checked == false)
                        {
                            CMessageBox.Show("Reservation Cancel", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            btn_delt.Visible = false;
                        }
                        if (datadetails == true)
                        {
                            CMessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (datadetails == false)
                        {
                            CMessageBox.Show("Unable To Save  ", "Error On Save", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {
                        bool saveStatus = bllReservation.UpdateData("prdInsert_Upd_bookDetails", dmnReservation);


                        if (saveStatus == true)
                        {
                            CMessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (saveStatus == false)
                        {
                            CMessageBox.Show("Unable To Save  ", "Error On Save", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
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
            
            var result = CMessageBox.Show("Do You want Cancel This Reservation", "Confirm The Action", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                chb_active.Checked = false;
                save();
                //Fillitem();
            }
        }

        private void lblSTotal_Leave(object sender, EventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(lblSTotal.Text, out value))
            {
                lblSTotal.Text = "0";

            }
        }

        private void lblMTotal_Leave(object sender, EventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(lblMTotal.Text, out value))
            {
                lblMTotal.Text = "0";

            }
        }

        private void lblGTotal_Leave(object sender, EventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(lblMTotal.Text, out value))
            {
                lblMTotal.Text = "0";

            }
        }
       

        private void dgvItemPop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvItemPop.CurrentCell.RowIndex;
                int r1 = dgvmenu.CurrentCell.RowIndex;
                BllSearch<DmnMenu> bllSearch = new BllSearch<DmnMenu>();
                BllSearch<DmnOfferMaster> bllSearch1 = new BllSearch<DmnOfferMaster>();
                DmnSearch objSearch = new DmnSearch();
                int panelWidthPercentage = 20;
                int panelheightPercentage = 20;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                panelProductPopup.Size = new Size(panelWidth, panelHeight);
                if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[5].Value) == Convert.ToInt32(formsenum.Menu))
                {
                    objSearch.Select = "MId ,Name as Title,p.Price,p.Tax,p.TaxType ,IgstPrcnt,VatPrcnt ,ts.TaxSettingsId," +
                        "ItemCode as ItemCode_UPC,CgstPrcnt,SgstPrcnt,p.Fcid," +
                        "p.DiscountAmt as DisAmt,p.DiscountPer as DisPer ";
                    objSearch.Table = " tblMenu m left join tblProduct p on m.ProductId=p.ProductId " +
                        " left join tblTaxSettings ts on p.TaxSettingsId=ts.TaxSettingsId ";
                    objSearch.WhereNdGroupBy = " where MId =" + dgvItemPop.Rows[r].Cells[0].Value;
                    objSearch.OrderBy = "";
                }
                else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[5].Value) == Convert.ToInt32(formsenum.Item))
                {
                    objSearch.Select = " ItemId,Itemname ,p.SRate,p.PRate,p.MRP,i.Tax,i.TaxType,IgstPrcnt,VatPrcnt,CgstPrcnt,SgstPrcnt," +
                        " unit,ItemCode_UPC,p.Id as PrId,Fcid,ts.TaxSettingsId,quantity as QOH,i.DiscountAmt,i.DiscountPer ";
                    objSearch.Table = "  tblItem i  left join tblTaxSettings ts on i.TaxSettingsId=ts.TaxSettingsId " +
                        " left join tblProductRate p   on ProductId=i.ItemId  and p.QOH>0";                       
                    objSearch.WhereNdGroupBy = " where  Saletype=1 and  ItemId =" + dgvItemPop.Rows[r].Cells[0].Value;
                    objSearch.OrderBy = "";
                }
                else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[5].Value) == Convert.ToInt32(formsenum.Discount_And_Offer) )
                {
                    objSearch.Select = "ofm.OfferName ,ofm.FinalPrice,ofm.Omid , ofm.TaxType , ofm.Tax , ofm.TaxSettingsId , ofm.Discountper , ofd.Productname, " +
                        " CgstPrcnt,SgstPrcnt,VatPrcnt,IgstPrcnt ,ofm.DiscountAmt,ofm.Discountper,ofm.TaxSettingsId ";
                       
                    objSearch.Table = "  tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofm.Omid=ofd.Omid  left join tblTaxSettings ts  on ts.TaxSettingsId=ofm.TaxSettingsId ";
                    objSearch.WhereNdGroupBy = " where ofm.Omid =" + dgvItemPop.Rows[r].Cells[0].Value;
                    objSearch.OrderBy = "";
                }
                if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[5].Value) == Convert.ToInt32(formsenum.Menu))
                {
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);

                    if (gridSource.Count > 0)
                    {
                        int qty = 1;
                        int price = 0;
                        bool flag = true;
                        int i = 0;
                        for (i = 0; i <= dgvmenu.RowCount - 1; i++)
                        {
                            if (Convert.ToInt32(dgvmenu.Rows[i].Cells[imid].Value) == gridSource[0].MId && Convert.ToInt32(dgvmenu.Rows[i].Cells[iFormType].Value) == Convert.ToInt32(formsenum.Menu))
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
                            decimal disper = OfferCheckItem(Convert.ToInt32(gridSource[0].ProductId),Convert.ToInt32(Tabletype.Product));
                            if(disper ==0)
                            {
                                dgvmenu.Rows[r1].Cells[idisper].Value = gridSource[0].DisPer;
                            }
                            else
                            {
                                dgvmenu.Rows[r1].Cells[idisper].Value = disper;
                            }
                            dgvmenu.Rows[r1].Cells[imid].Value = gridSource[0].MId;
                            dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = 0;
                            dgvmenu.Rows[r1].Cells[imname].Value = gridSource[0].Title;
                            dgvmenu.Rows[r1].Cells[imprice].Value = gridSource[0].Price;
                            dgvmenu.Rows[r1].Cells[iqty].Value = 1;
                            dgvmenu.Rows[r1].Cells[imdescription].Value = gridSource[0].Description;
                            dgvmenu.Rows[r1].Cells[isamount].Value = Convert.ToDecimal(dgvmenu.Rows[r1].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[r1].Cells[iqty].Value);
                            dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Menu);
                            dgvmenu.Rows[r1].Cells[iTax].Value = gridSource[0].Tax;
                            dgvmenu.Rows[r1].Cells[iTaxType].Value = gridSource[0].TaxType;
                            dgvmenu.Rows[r1].Cells[icgst].Value = gridSource[0].CgstPrcnt;
                            dgvmenu.Rows[r1].Cells[ivat].Value = gridSource[0].VatPrcnt;
                            dgvmenu.Rows[r1].Cells[imgst].Value = gridSource[0].IgstPrcnt;
                            dgvmenu.Rows[r1].Cells[isgst].Value = gridSource[0].SgstPrcnt;
                            dgvmenu.Rows[r1].Cells[iprate].Value = gridSource[0].Prate;
                            dgvmenu.Rows[r1].Cells[imrp].Value = gridSource[0].MRP;
                            //dgvmenu.Rows[r1].Cells[idisamt].Value = gridSource[0].DisAmt;
                            
                            dgvmenu.Rows[r1].Cells[iunit].Value = gridSource[0].unit;
                            dgvmenu.Rows[r1].Cells[iitemcode].Value = gridSource[0].ItemCode_UPC;
                            dgvmenu.Rows[r1].Cells[iprid].Value = gridSource[0].prid;
                            dgvmenu.Rows[r1].Cells[itaxsettingid].Value = gridSource[0].TaxSettingsId;
                            dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.Product);
                           

                            //dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(formsenum.Menu);
                            dgvmenu.Rows[r1].Cells[iproductType].Value = gridSource[0].Fcid;
                           
                            txtqty.Text = Convert.ToString(dgvmenu.Rows[r1].Cells[iqty].Value);
                            totalcellamount(r1);
                             dgvmenu.Rows[r1].Cells[imarketprice].Value = Convert.ToDecimal(dgvmenu.Rows[r1].Cells[isamount].Value);
                        }
                        else
                        {
                            dgvmenu.Rows[i].Cells[iqty].Value = qty;
                            dgvmenu.Rows[i].Cells[imprice].Value = price;
                            txtqty.Text = Convert.ToString(dgvmenu.Rows[i].Cells[iqty].Value);
                            totalcellamount(i);
                            

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
                        panelProductPopup.Visible = false;
                        //pnlCustHead.Hide();
                       // dgvItemPop.Visible = false;
                        dgvmenu.Enabled = true;
                        dgvmenu.Focus();

                    }
                }
                else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[5].Value) == Convert.ToInt32(formsenum.Item))
                {
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);

                    if (gridSource.Count == 1)
                    {
                        int qty = 1;
                        int price = 0;
                        bool flag = true;
                        int i = 0;
                        for (i = 0; i <= dgvmenu.RowCount - 1; i++)
                        {
                            if (Convert.ToInt32(dgvmenu.Rows[i].Cells[imid].Value) == gridSource[0].ItemId && Convert.ToInt32(dgvmenu.Rows[i].Cells[iFormType].Value) == Convert.ToInt32(formsenum.Item))
                            {
                                flag = false;
                                qty += Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value);
                                price += Convert.ToInt32(dgvmenu.Rows[i].Cells[imprice].Value);


                                break;

                            }

                        }
                        if (gridSource[0].QOH >= qty)
                        {
                            if (flag == true)
                            {
                                if (dgvmenu.Rows.Count - 1 == dgvmenu.CurrentCell.RowIndex)
                                {
                                    dgvmenu.Rows.Add(1);
                                }
                                decimal disper= OfferCheckItem(Convert.ToInt32(gridSource[0].ItemId), Convert.ToInt32(Tabletype.DirectSale));

                                if (disper == 0)
                                {
                                    dgvmenu.Rows[r1].Cells[idisper].Value = gridSource[0].DiscountPer;
                                }
                                else
                                {
                                    dgvmenu.Rows[r1].Cells[idisper].Value = disper;
                                }
                                dgvmenu.Rows[r1].Cells[imid].Value = gridSource[0].ItemId;
                                dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = 0;
                                dgvmenu.Rows[r1].Cells[imname].Value = gridSource[0].Itemname;
                                dgvmenu.Rows[r1].Cells[imprice].Value = gridSource[0].Srate;
                                dgvmenu.Rows[r1].Cells[iqty].Value = 1;
                                dgvmenu.Rows[r1].Cells[isamount].Value = Convert.ToDecimal(dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[iqty].Value);
                                dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Item);
                                dgvmenu.Rows[r1].Cells[iTax].Value = gridSource[0].Tax;
                                dgvmenu.Rows[r1].Cells[iTaxType].Value = gridSource[0].TaxType;
                                dgvmenu.Rows[r1].Cells[icgst].Value = gridSource[0].CgstPrcnt;
                                dgvmenu.Rows[r1].Cells[ivat].Value = gridSource[0].VatPrcnt;
                                dgvmenu.Rows[r1].Cells[imgst].Value = gridSource[0].IgstPrcnt;
                                dgvmenu.Rows[r1].Cells[isgst].Value = gridSource[0].SgstPrcnt;
                                

                                dgvmenu.Rows[r1].Cells[iprate].Value = gridSource[0].Prate;
                                dgvmenu.Rows[r1].Cells[imdescription].Value = gridSource[0].Description;
                                dgvmenu.Rows[r1].Cells[imrp].Value = gridSource[0].MRP;
                                dgvmenu.Rows[r1].Cells[iunit].Value = gridSource[0].unit;
                                dgvmenu.Rows[r1].Cells[iitemcode].Value = gridSource[0].ItemCode_UPC;
                                dgvmenu.Rows[r1].Cells[iprid].Value = gridSource[0].prid;
                                dgvmenu.Rows[r1].Cells[itaxsettingid].Value = gridSource[0].TaxSettingsId;
                                dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.DirectSale);
                                dgvmenu.Rows[r1].Cells[iQOH].Value = gridSource[0].QOH;
                                dgvmenu.Rows[r1].Cells[iproductType].Value = gridSource[0].Fcid;
                                dgvmenu.Rows[r1].Cells[imarketprice].Value = Convert.ToInt32(dgvmenu.Rows[r1].Cells[isamount].Value);
                                txtqty.Text = Convert.ToString(dgvmenu.Rows[r1].Cells[iqty].Value);
                                totalcellamount(r1);

                            }

                            else
                            {
                                dgvmenu.Rows[i].Cells[iqty].Value = qty;

                                dgvmenu.Rows[i].Cells[imprice].Value = price;
                                txtqty.Text = Convert.ToString(dgvmenu.Rows[i].Cells[iqty].Value);
                                totalcellamount(i);

                            }

                        }
                        else
                        {
                            CMessageBox.Show("Avilable Stock Quanity Is " + gridSource[0].QOH, "Insufficent Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //dgvSProduct.Rows[i].Cells[isqty].Value = gridSource[0].QOH;


                        }
                        panelProductPopup.Visible = false;
                        //pnlCustHead.Hide();
                        //dgvItemPop.Visible = false;
                        dgvmenu.Enabled = true;
                        dgvmenu.Focus();
                    }
                    else if (gridSource.Count > 1)
                    {
                        dgv_spopup.Rows.Clear();

                        dgv_spopup.Rows.Add(gridSource.Count);
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            dgv_spopup.Rows[i].Cells[slno].Value = i + 1;
                            dgv_spopup.Rows[i].Cells[prId].Value = gridSource[i].PrId;
                            dgv_spopup.Rows[i].Cells[iiDescription].Value = gridSource[i].Itemname;
                            dgv_spopup.Rows[i].Cells[Srate].Value = gridSource[i].Srate;
                            dgv_spopup.Rows[i].Cells[qt].Value = gridSource[i].QOH;

                        }
                        redpan.Location = new Point(
                          this.ClientSize.Width / 2 - redpan.Size.Width / 2,
                          (this.ClientSize.Height / 3 - redpan.Size.Height / 2));
                        redpan.Anchor = AnchorStyles.None;
                        dgvmenu.Enabled = false;
                        dgv_spopup.Enabled = true;
                        redpan.Visible = true;
                        dgv_spopup.Visible = true;
                        dgv_spopup.Focus();
                        panelProductPopup.Visible = false;
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
                }
                else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[5].Value) == Convert.ToInt32(formsenum.Discount_And_Offer))
                {
                    var gridSource1 = bllSearch1.GetSearchData("prdSearch", objSearch);
                    if (gridSource1.Count > 0)
                    {
                        int qty = 1;
                        int price = 0;
                        bool flag = true;
                        int i = 0;
                        for (i = 0; i <= dgvmenu.RowCount - 1; i++)
                        {
                            if (Convert.ToInt32(dgvmenu.Rows[i].Cells[imid].Value) == gridSource1[0].Omid && Convert.ToInt32(dgvmenu.Rows[i].Cells[iFormType].Value) == Convert.ToInt32(formsenum.Discount_And_Offer))
                            {
                                flag = false;
                                qty += Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value);
                                price += Convert.ToInt32(dgvmenu.Rows[i].Cells[imprice].Value);

                                
                                break;
                            }
                            
                        }
                        if (flag == true)
                        {
                            if (dgvmenu.Rows.Count - 1 == dgvmenu.CurrentCell.RowIndex)
                            {
                                dgvmenu.Rows.Add(1);
                            }

                            dgvmenu.Rows[r1].Cells[imid].Value = gridSource1[0].Omid;
                            dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = 0;
                            dgvmenu.Rows[r1].Cells[imname].Value = gridSource1[0].OfferName;
                            dgvmenu.Rows[r1].Cells[imprice].Value = gridSource1[0].FinalPrice;
                            dgvmenu.Rows[r1].Cells[idisamt].Value = gridSource1[0].DiscountAmt;
                            dgvmenu.Rows[r1].Cells[idisper].Value = gridSource1[0].Discountper;
                            dgvmenu.Rows[r1].Cells[iqty].Value = 1;
                            dgvmenu.Rows[r1].Cells[iqtyadd].Value = Convert.ToInt32(dgvmenu.Rows[r1].Cells[iqty].Value);
                            dgvmenu.Rows[r1].Cells[imdescription].Value ="";
                            dgvmenu.Rows[r1].Cells[isamount].Value = Convert.ToDecimal(dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[iqty].Value);
                            dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Discount_And_Offer);
                            dgvmenu.Rows[r1].Cells[iTax].Value = gridSource1[0].Tax;
                            dgvmenu.Rows[r1].Cells[iTaxType].Value = gridSource1[0].TaxType;
                            dgvmenu.Rows[r1].Cells[icgst].Value = gridSource1[0].CgstPrcnt;
                            dgvmenu.Rows[r1].Cells[ivat].Value = gridSource1[0].VatPrcnt;
                            dgvmenu.Rows[r1].Cells[imgst].Value = gridSource1[0].IgstPrcnt;
                            dgvmenu.Rows[r1].Cells[isgst].Value = gridSource1[0].SgstPrcnt;
                            dgvmenu.Rows[r1].Cells[iprate].Value = 0;
                            dgvmenu.Rows[r1].Cells[imrp].Value = 0;
                            dgvmenu.Rows[r1].Cells[itaxsettingid].Value = gridSource1[0].TaxSettingsId;
                            dgvmenu.Rows[r1].Cells[imarketprice].Value = Convert.ToInt32(dgvmenu.Rows[r1].Cells[isamount].Value);
                            dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.ComboProduct);
                            txtqty.Text = Convert.ToString(dgvmenu.Rows[r1].Cells[iqty].Value);
                            totalcellamount(r1);


                        }
                        else
                        {
                            dgvmenu.Rows[i].Cells[iqty].Value = qty;
                            dgvmenu.Rows[i].Cells[imprice].Value = price;
                            txtqty.Text = Convert.ToString(dgvmenu.Rows[i].Cells[iqty].Value);
                            totalcellamount(i); 
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
                        panelProductPopup.Visible = false;
                        //pnlCustHead.Hide();
                        //dgvItemPop.Visible = false;
                        dgvmenu.Enabled = true;
                        dgvmenu.Focus();
                    }
                    
                }
               
            }
            
            catch (Exception ex)
            { }
            }
        public void totalcellamount(int cellproductid)
        {
            try
            {
               
                if (dgvmenu.Rows[cellproductid].Cells[imid].Value != "" && dgvmenu.Rows[cellproductid].Cells[imprice].Value != "")
                {
                    decimal amount = Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[iqty].Value);
                    dgvmenu.Rows[cellproductid].Cells[isamount].Value = amount.ToString("N2");

                    decimal discount = 0;
                    if (Convert.ToInt32(dgvmenu.Rows[cellproductid].Cells[itabletype].Value) != 3)
                    {
                        cellDiscountAmount(cellproductid);


                        discount=Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[idisamt].Value);
                    }
                    cellTaxAmount(cellproductid, amount - discount);

                    if (Convert.ToString(dgvmenu.Rows[cellproductid].Cells[iTax].Value) == "Exclusive")
                    {
                        if (Convert.ToInt32(dgvmenu.Rows[cellproductid].Cells[itabletype].Value) != 3)
                        {
                            decimal tot = (amount - Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[idisamt].Value))
                        + Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[istaxamt].Value);
                            dgvmenu.Rows[cellproductid].Cells[isamount].Value = tot.ToString("N2");
                        }
                        else
                        {
                            decimal tot = (amount )
                        + Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[istaxamt].Value);
                            dgvmenu.Rows[cellproductid].Cells[isamount].Value = tot.ToString("N2");
                        }
                    }
                    if (Convert.ToString(dgvmenu.Rows[cellproductid].Cells[iTax].Value) == "Inclusive")
                    {
                        decimal tot = (amount - Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[idisamt].Value));
                        dgvmenu.Rows[cellproductid].Cells[isamount].Value = tot.ToString("N2");
                    }
                }


            }
            catch (Exception ex)
            {

            }
        }
        public void cellDiscountAmount(int cellproductid)
        {
            try
            {
                if (dgvmenu.Rows[cellproductid].Cells[idisper].Value != "" && dgvmenu.Rows[cellproductid].Cells[isamount].Value != "")
                {
                    decimal disAmnt = (Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[idisper].Value) * Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[isamount].Value)) / 100;
                    dgvmenu.Rows[cellproductid].Cells[idisamt].Value = disAmnt.ToString("N2");
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void cellDiscountPercentage(int cellproductid)
        {
            try
            {
                if (Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[isamount].Value) > 0 && dgvmenu.Rows[cellproductid].Cells[idisamt].Value != "")
                {
                    decimal disPerc = (Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[idisamt].Value) / Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[isamount].Value)) * 100;
                    dgvmenu.Rows[cellproductid].Cells[idisper].Value = disPerc.ToString("N2");
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void cellTaxAmount(int cellproductid, decimal amount)
        {
            try
            {
                string cellpopupid = Convert.ToString(dgvmenu.Rows[cellproductid].Cells[imid].Value);
                if (cellpopupid != "")
                {
                    BllSearch<DmnTaxSettings> _bllSearch1 = new BllSearch<DmnTaxSettings>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "TaxType, IgstPrcnt,VatPrcnt ";
                    dmnSearch.Table = "tblTaxSettings ";
                    dmnSearch.WhereNdGroupBy = " where status=1 and TaxSettingsId= " + dgvmenu.Rows[cellproductid].Cells[itaxsettingid].Value;
                    dmnSearch.OrderBy = "";
                    var gridSource = _bllSearch1.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource[0].TaxType == "GST")
                    {
                        decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].IgstPrcnt) * amount / 100);
                        dgvmenu.Rows[cellproductid].Cells[istaxamt].Value = TaxAmount.ToString("N2");
                        dgvmenu.Rows[cellproductid].Cells[imgst].Value = TaxAmount.ToString("N2");
                        dgvmenu.Rows[cellproductid].Cells[iTaxper].Value = gridSource[0].IgstPrcnt;
                    }
                    if (gridSource[0].TaxType == "VAT")
                    {
                        decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].VatPrcnt) * Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[isamount].Value) / 100);
                        dgvmenu.Rows[cellproductid].Cells[istaxamt].Value = TaxAmount.ToString("N2");
                        dgvmenu.Rows[cellproductid].Cells[ivat].Value = TaxAmount.ToString("N2");
                        dgvmenu.Rows[cellproductid].Cells[iTaxper].Value = gridSource[0].VatPrcnt;
                    }
                }
                dgvmenu.Rows[cellproductid].Cells[istaxamt].Value = Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[isamount].Value) * Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[iTaxper].Value) / 100;
            }
            catch (Exception ex)
            {

            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            //dgvItemPop.Visible = false;
            panelProductPopup.Visible = false;
            dgvmenu.Enabled = true;
        }

        private void FrmReservation_Resize(object sender, EventArgs e)
        {            
            textb_shape();
        }

        private void txtqty_Leave(object sender, EventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(lblMTotal.Text, out value))
            {
                lblMTotal.Text = "0";

            }
        }

        private void dgvItemPop_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    int r = dgvItemPop.CurrentCell.RowIndex;
                    int r1 = dgvmenu.CurrentCell.RowIndex;
                    BllSearch<DmnMenu> bllSearch = new BllSearch<DmnMenu>();
                    BllSearch<DmnOfferMaster> bllSearch1 = new BllSearch<DmnOfferMaster>();
                    DmnSearch objSearch = new DmnSearch();
                    int panelWidthPercentage = 20;
                    int panelheightPercentage = 20;
                    int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                    int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                    panelProductPopup.Size = new Size(panelWidth, panelHeight);
                    if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[5].Value) == Convert.ToInt32(formsenum.Menu))
                    {
                        objSearch.Select = "MId ,Name as Title,p.Price,p.Tax,p.TaxType ,IgstPrcnt,VatPrcnt ,ts.TaxSettingsId," +
                            "ItemCode as ItemCode_UPC,CgstPrcnt,SgstPrcnt,p.Fcid," +
                            "p.DiscountAmt as DisAmt,p.DiscountPer as DisPer ";
                        objSearch.Table = " tblMenu m left join tblProduct p on m.ProductId=p.ProductId " +
                            " left join tblTaxSettings ts on p.TaxSettingsId=ts.TaxSettingsId ";
                        objSearch.WhereNdGroupBy = " where MId =" + dgvItemPop.Rows[r].Cells[0].Value;
                        objSearch.OrderBy = "";
                    }
                    else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[5].Value) == Convert.ToInt32(formsenum.Item))
                    {
                        objSearch.Select = " ItemId,Itemname ,p.SRate,p.PRate,p.MRP,i.Tax,i.TaxType,IgstPrcnt,VatPrcnt,CgstPrcnt,SgstPrcnt," +
                            " unit,ItemCode_UPC,p.Id as PrId,Fcid,ts.TaxSettingsId,quantity as QOH,i.DiscountAmt,i.DiscountPer ";
                        objSearch.Table = "  tblItem i  left join tblTaxSettings ts on i.TaxSettingsId=ts.TaxSettingsId " +
                            " left join tblProductRate p   on ProductId=i.ItemId  and p.QOH>0";
                        objSearch.WhereNdGroupBy = " where  Saletype=1 and  ItemId =" + dgvItemPop.Rows[r].Cells[0].Value;
                        objSearch.OrderBy = "";
                    }
                    else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[5].Value) == Convert.ToInt32(formsenum.Discount_And_Offer))
                    {
                        objSearch.Select = "ofm.OfferName ,ofm.FinalPrice,ofm.Omid , ofm.TaxType , ofm.Tax , ofm.TaxSettingsId , ofm.Discountper , ofd.Productname, " +
                            " CgstPrcnt,SgstPrcnt,VatPrcnt,IgstPrcnt ,ofm.DiscountAmt,ofm.Discountper,ofm.TaxSettingsId ";

                        objSearch.Table = "  tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofm.Omid=ofd.Omid  left join tblTaxSettings ts  on ts.TaxSettingsId=ofm.TaxSettingsId ";
                        objSearch.WhereNdGroupBy = " where ofm.Omid =" + dgvItemPop.Rows[r].Cells[0].Value;
                        objSearch.OrderBy = "";
                    }
                    if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[5].Value) == Convert.ToInt32(formsenum.Menu))
                    {
                        var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);

                        if (gridSource.Count > 0)
                        {
                            int qty = 1;
                            int price = 0;
                            bool flag = true;
                            int i = 0;
                            for (i = 0; i <= dgvmenu.RowCount - 1; i++)
                            {
                                if (Convert.ToInt32(dgvmenu.Rows[i].Cells[imid].Value) == gridSource[0].MId && Convert.ToInt32(dgvmenu.Rows[i].Cells[iFormType].Value) == Convert.ToInt32(formsenum.Menu))
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
                                decimal disper = OfferCheckItem(Convert.ToInt32(gridSource[0].ProductId), Convert.ToInt32(Tabletype.Product));
                                if (disper == 0)
                                {
                                    dgvmenu.Rows[r1].Cells[idisper].Value = gridSource[0].DisPer;
                                }
                                else
                                {
                                    dgvmenu.Rows[r1].Cells[idisper].Value = disper;
                                }
                                dgvmenu.Rows[r1].Cells[imid].Value = gridSource[0].MId;
                                dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = 0;
                                dgvmenu.Rows[r1].Cells[imname].Value = gridSource[0].Title;
                                dgvmenu.Rows[r1].Cells[imprice].Value = gridSource[0].Price;
                                dgvmenu.Rows[r1].Cells[iqty].Value = 1;
                                dgvmenu.Rows[r1].Cells[imdescription].Value = gridSource[0].Description;
                                dgvmenu.Rows[r1].Cells[isamount].Value = Convert.ToDecimal(dgvmenu.Rows[r1].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[r1].Cells[iqty].Value);
                                dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Menu);
                                dgvmenu.Rows[r1].Cells[iTax].Value = gridSource[0].Tax;
                                dgvmenu.Rows[r1].Cells[iTaxType].Value = gridSource[0].TaxType;
                                dgvmenu.Rows[r1].Cells[icgst].Value = gridSource[0].CgstPrcnt;
                                dgvmenu.Rows[r1].Cells[ivat].Value = gridSource[0].VatPrcnt;
                                dgvmenu.Rows[r1].Cells[imgst].Value = gridSource[0].IgstPrcnt;
                                dgvmenu.Rows[r1].Cells[isgst].Value = gridSource[0].SgstPrcnt;
                                dgvmenu.Rows[r1].Cells[iprate].Value = gridSource[0].Prate;
                                dgvmenu.Rows[r1].Cells[imrp].Value = gridSource[0].MRP;
                                //dgvmenu.Rows[r1].Cells[idisamt].Value = gridSource[0].DisAmt;

                                dgvmenu.Rows[r1].Cells[iunit].Value = gridSource[0].unit;
                                dgvmenu.Rows[r1].Cells[iitemcode].Value = gridSource[0].ItemCode_UPC;
                                dgvmenu.Rows[r1].Cells[iprid].Value = gridSource[0].prid;
                                dgvmenu.Rows[r1].Cells[itaxsettingid].Value = gridSource[0].TaxSettingsId;
                                dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.Product);


                                //dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(formsenum.Menu);
                                dgvmenu.Rows[r1].Cells[iproductType].Value = gridSource[0].Fcid;

                                txtqty.Text = Convert.ToString(dgvmenu.Rows[r1].Cells[iqty].Value);
                                totalcellamount(r1);
                                dgvmenu.Rows[r1].Cells[imarketprice].Value = Convert.ToDecimal(dgvmenu.Rows[r1].Cells[isamount].Value);
                            }
                            else
                            {
                                dgvmenu.Rows[i].Cells[iqty].Value = qty;
                                dgvmenu.Rows[i].Cells[imprice].Value = price;
                                txtqty.Text = Convert.ToString(dgvmenu.Rows[i].Cells[iqty].Value);
                                totalcellamount(i);


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
                            panelProductPopup.Visible = false;
                            //pnlCustHead.Hide();
                            // dgvItemPop.Visible = false;
                            dgvmenu.Enabled = true;
                            dgvmenu.Focus();

                        }
                    }
                    else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[5].Value) == Convert.ToInt32(formsenum.Item))
                    {
                        var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);

                        if (gridSource.Count == 1)
                        {
                            int qty = 1;
                            int price = 0;
                            bool flag = true;
                            int i = 0;
                            for (i = 0; i <= dgvmenu.RowCount - 1; i++)
                            {
                                if (Convert.ToInt32(dgvmenu.Rows[i].Cells[imid].Value) == gridSource[0].ItemId && Convert.ToInt32(dgvmenu.Rows[i].Cells[iFormType].Value) == Convert.ToInt32(formsenum.Item))
                                {
                                    flag = false;
                                    qty += Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value);
                                    price += Convert.ToInt32(dgvmenu.Rows[i].Cells[imprice].Value);


                                    break;

                                }

                            }
                            if (gridSource[0].QOH >= qty)
                            {
                                if (flag == true)
                                {
                                    if (dgvmenu.Rows.Count - 1 == dgvmenu.CurrentCell.RowIndex)
                                    {
                                        dgvmenu.Rows.Add(1);
                                    }
                                    decimal disper = OfferCheckItem(Convert.ToInt32(gridSource[0].ItemId), Convert.ToInt32(Tabletype.DirectSale));

                                    if (disper == 0)
                                    {
                                        dgvmenu.Rows[r1].Cells[idisper].Value = gridSource[0].DiscountPer;
                                    }
                                    else
                                    {
                                        dgvmenu.Rows[r1].Cells[idisper].Value = disper;
                                    }
                                    dgvmenu.Rows[r1].Cells[imid].Value = gridSource[0].ItemId;
                                    dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = 0;
                                    dgvmenu.Rows[r1].Cells[imname].Value = gridSource[0].Itemname;
                                    dgvmenu.Rows[r1].Cells[imprice].Value = gridSource[0].Srate;
                                    dgvmenu.Rows[r1].Cells[iqty].Value = 1;
                                    dgvmenu.Rows[r1].Cells[isamount].Value = Convert.ToDecimal(dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[iqty].Value);
                                    dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Item);
                                    dgvmenu.Rows[r1].Cells[iTax].Value = gridSource[0].Tax;
                                    dgvmenu.Rows[r1].Cells[iTaxType].Value = gridSource[0].TaxType;
                                    dgvmenu.Rows[r1].Cells[icgst].Value = gridSource[0].CgstPrcnt;
                                    dgvmenu.Rows[r1].Cells[ivat].Value = gridSource[0].VatPrcnt;
                                    dgvmenu.Rows[r1].Cells[imgst].Value = gridSource[0].IgstPrcnt;
                                    dgvmenu.Rows[r1].Cells[isgst].Value = gridSource[0].SgstPrcnt;


                                    dgvmenu.Rows[r1].Cells[iprate].Value = gridSource[0].Prate;
                                    dgvmenu.Rows[r1].Cells[imdescription].Value = gridSource[0].Description;
                                    dgvmenu.Rows[r1].Cells[imrp].Value = gridSource[0].MRP;
                                    dgvmenu.Rows[r1].Cells[iunit].Value = gridSource[0].unit;
                                    dgvmenu.Rows[r1].Cells[iitemcode].Value = gridSource[0].ItemCode_UPC;
                                    dgvmenu.Rows[r1].Cells[iprid].Value = gridSource[0].prid;
                                    dgvmenu.Rows[r1].Cells[itaxsettingid].Value = gridSource[0].TaxSettingsId;
                                    dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.DirectSale);
                                    dgvmenu.Rows[r1].Cells[iQOH].Value = gridSource[0].QOH;
                                    dgvmenu.Rows[r1].Cells[iproductType].Value = gridSource[0].Fcid;
                                    dgvmenu.Rows[r1].Cells[imarketprice].Value = Convert.ToInt32(dgvmenu.Rows[r1].Cells[isamount].Value);
                                    txtqty.Text = Convert.ToString(dgvmenu.Rows[r1].Cells[iqty].Value);
                                    totalcellamount(r1);

                                }

                                else
                                {
                                    dgvmenu.Rows[i].Cells[iqty].Value = qty;

                                    dgvmenu.Rows[i].Cells[imprice].Value = price;
                                    txtqty.Text = Convert.ToString(dgvmenu.Rows[i].Cells[iqty].Value);
                                    totalcellamount(i);

                                }

                            }
                            else
                            {
                                CMessageBox.Show("Avilable Stock Quanity Is " + gridSource[0].QOH, "Insufficent Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //dgvSProduct.Rows[i].Cells[isqty].Value = gridSource[0].QOH;


                            }
                            panelProductPopup.Visible = false;
                            //pnlCustHead.Hide();
                            //dgvItemPop.Visible = false;
                            dgvmenu.Enabled = true;
                            dgvmenu.Focus();
                        }
                        else if (gridSource.Count > 1)
                        {
                            dgv_spopup.Rows.Clear();

                            dgv_spopup.Rows.Add(gridSource.Count);
                            for (int i = 0; i < gridSource.Count; i++)
                            {
                                dgv_spopup.Rows[i].Cells[slno].Value = i + 1;
                                dgv_spopup.Rows[i].Cells[prId].Value = gridSource[i].PrId;
                                dgv_spopup.Rows[i].Cells[iiDescription].Value = gridSource[i].Itemname;
                                dgv_spopup.Rows[i].Cells[Srate].Value = gridSource[i].Srate;
                                dgv_spopup.Rows[i].Cells[qt].Value = gridSource[i].QOH;

                            }
                            redpan.Location = new Point(
                              this.ClientSize.Width / 2 - redpan.Size.Width / 2,
                              (this.ClientSize.Height / 3 - redpan.Size.Height / 2));
                            redpan.Anchor = AnchorStyles.None;
                            dgvmenu.Enabled = false;
                            dgv_spopup.Enabled = true;
                            redpan.Visible = true;
                            dgv_spopup.Visible = true;
                            dgv_spopup.Focus();
                            panelProductPopup.Visible = false;
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
                    }
                    else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[5].Value) == Convert.ToInt32(formsenum.Discount_And_Offer))
                    {
                        var gridSource1 = bllSearch1.GetSearchData("prdSearch", objSearch);
                        if (gridSource1.Count > 0)
                        {
                            int qty = 1;
                            int price = 0;
                            bool flag = true;
                            int i = 0;
                            for (i = 0; i <= dgvmenu.RowCount - 1; i++)
                            {
                                if (Convert.ToInt32(dgvmenu.Rows[i].Cells[imid].Value) == gridSource1[0].Omid && Convert.ToInt32(dgvmenu.Rows[i].Cells[iFormType].Value) == Convert.ToInt32(formsenum.Discount_And_Offer))
                                {
                                    flag = false;
                                    qty += Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value);
                                    price += Convert.ToInt32(dgvmenu.Rows[i].Cells[imprice].Value);


                                    break;
                                }

                            }
                            if (flag == true)
                            {
                                if (dgvmenu.Rows.Count - 1 == dgvmenu.CurrentCell.RowIndex)
                                {
                                    dgvmenu.Rows.Add(1);
                                }

                                dgvmenu.Rows[r1].Cells[imid].Value = gridSource1[0].Omid;
                                dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = 0;
                                dgvmenu.Rows[r1].Cells[imname].Value = gridSource1[0].OfferName;
                                dgvmenu.Rows[r1].Cells[imprice].Value = gridSource1[0].FinalPrice;
                                dgvmenu.Rows[r1].Cells[idisamt].Value = gridSource1[0].DiscountAmt;
                                dgvmenu.Rows[r1].Cells[idisper].Value = gridSource1[0].Discountper;
                                dgvmenu.Rows[r1].Cells[iqty].Value = 1;
                                dgvmenu.Rows[r1].Cells[iqtyadd].Value = Convert.ToInt32(dgvmenu.Rows[r1].Cells[iqty].Value);
                                dgvmenu.Rows[r1].Cells[imdescription].Value = "";
                                dgvmenu.Rows[r1].Cells[isamount].Value = Convert.ToDecimal(dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[iqty].Value);
                                dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Discount_And_Offer);
                                dgvmenu.Rows[r1].Cells[iTax].Value = gridSource1[0].Tax;
                                dgvmenu.Rows[r1].Cells[iTaxType].Value = gridSource1[0].TaxType;
                                dgvmenu.Rows[r1].Cells[icgst].Value = gridSource1[0].CgstPrcnt;
                                dgvmenu.Rows[r1].Cells[ivat].Value = gridSource1[0].VatPrcnt;
                                dgvmenu.Rows[r1].Cells[imgst].Value = gridSource1[0].IgstPrcnt;
                                dgvmenu.Rows[r1].Cells[isgst].Value = gridSource1[0].SgstPrcnt;
                                dgvmenu.Rows[r1].Cells[iprate].Value = 0;
                                dgvmenu.Rows[r1].Cells[imrp].Value = 0;
                                dgvmenu.Rows[r1].Cells[itaxsettingid].Value = gridSource1[0].TaxSettingsId;
                                dgvmenu.Rows[r1].Cells[imarketprice].Value = Convert.ToInt32(dgvmenu.Rows[r1].Cells[isamount].Value);
                                dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.ComboProduct);
                                txtqty.Text = Convert.ToString(dgvmenu.Rows[r1].Cells[iqty].Value);
                                totalcellamount(r1);


                            }
                            else
                            {
                                dgvmenu.Rows[i].Cells[iqty].Value = qty;
                                dgvmenu.Rows[i].Cells[imprice].Value = price;
                                txtqty.Text = Convert.ToString(dgvmenu.Rows[i].Cells[iqty].Value);
                                totalcellamount(i);
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
                            panelProductPopup.Visible = false;
                            //pnlCustHead.Hide();
                            //dgvItemPop.Visible = false;
                            dgvmenu.Enabled = true;
                            dgvmenu.Focus();
                        }

                    }

                }
            }

            catch (Exception ex)
            { }
        }

        private void txtMob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNOP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgv_spopup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
                int r1 = dgvmenu.CurrentCell.RowIndex;
                int r = dgvItemPop.CurrentCell.RowIndex;
                BllSearch<DmnMenu> bllSearch = new BllSearch<DmnMenu>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = " ItemId,Itemname ,p.SRate,p.PRate,p.MRP,i.Tax,i.TaxType,IgstPrcnt,VatPrcnt,CgstPrcnt,SgstPrcnt," +
                           " unit,ItemCode_UPC,p.Id as PrId,Fcid,ts.TaxSettingsId,quantity as QOH ";
                objSearch.Table = "  tblItem i  left join tblTaxSettings ts on i.TaxSettingsId=ts.TaxSettingsId " +
                    " left join tblProductRate p   on ProductId=i.ItemId ";
                objSearch.WhereNdGroupBy = " where  Saletype=1 and  p.Id =" + dgv_spopup.Rows[e.RowIndex].Cells[prId].Value;
                objSearch.OrderBy = "";
               
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);

               
                    int qty = 1;
                    int price = 0;
                    bool flag = true;
                    int i = 0;
                    for (i = 0; i <= dgvmenu.RowCount - 1; i++)
                    {
                        if (Convert.ToInt32(dgvmenu.Rows[i].Cells[iprid].Value) == gridSource[0].PrId )
                        {
                            flag = false;
                            qty += Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value);
                            price += Convert.ToInt32(dgvmenu.Rows[i].Cells[imprice].Value);


                            break;

                        }

                    }
                    if (gridSource[0].QOH >= qty)
                    {
                        if (flag == true)
                        {
                            if (dgvmenu.Rows.Count - 1 == dgvmenu.CurrentCell.RowIndex)
                            {
                                dgvmenu.Rows.Add(1);
                            }
                        decimal disper = OfferCheckItem(Convert.ToInt32(gridSource[0].ItemId), Convert.ToInt32(Tabletype.DirectSale));

                        if (disper == 0)
                        {
                            dgvmenu.Rows[r1].Cells[idisper].Value = gridSource[0].DiscountPer;
                        }
                        else
                        {
                            dgvmenu.Rows[r1].Cells[idisper].Value = disper;
                        }
                        dgvmenu.Rows[r1].Cells[imid].Value = gridSource[0].ItemId;
                            dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = 0;
                            dgvmenu.Rows[r1].Cells[imname].Value = gridSource[0].Itemname;
                            dgvmenu.Rows[r1].Cells[imprice].Value = gridSource[0].Srate;
                            dgvmenu.Rows[r1].Cells[iqty].Value = 1;
                            //dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[imdescription].Value = gridSource[0].Description;
                            dgvmenu.Rows[r1].Cells[isamount].Value = Convert.ToDecimal(dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[iqty].Value);
                            dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Item);
                            dgvmenu.Rows[r1].Cells[iTax].Value = gridSource[0].Tax;
                            dgvmenu.Rows[r1].Cells[iTaxType].Value = gridSource[0].TaxType;
                            dgvmenu.Rows[r1].Cells[icgst].Value = gridSource[0].CgstPrcnt;
                            dgvmenu.Rows[r1].Cells[ivat].Value = gridSource[0].VatPrcnt;
                            dgvmenu.Rows[r1].Cells[imgst].Value = gridSource[0].IgstPrcnt;
                            dgvmenu.Rows[r1].Cells[isgst].Value = gridSource[0].SgstPrcnt;
                            dgvmenu.Rows[r1].Cells[iprate].Value = gridSource[0].Prate;
                            dgvmenu.Rows[r1].Cells[imdescription].Value = gridSource[0].Description;
                            dgvmenu.Rows[r1].Cells[imrp].Value = gridSource[0].MRP;
                            dgvmenu.Rows[r1].Cells[iunit].Value = gridSource[0].unit;
                            dgvmenu.Rows[r1].Cells[iitemcode].Value = gridSource[0].ItemCode_UPC;
                            dgvmenu.Rows[r1].Cells[iprid].Value = gridSource[0].PrId;
                            dgvmenu.Rows[r1].Cells[itaxsettingid].Value = gridSource[0].TaxSettingsId;
                            dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.DirectSale);
                            dgvmenu.Rows[r1].Cells[iQOH].Value = gridSource[0].QOH;

                            dgvmenu.Rows[r1].Cells[iproductType].Value = gridSource[0].Fcid;
                            dgvmenu.Rows[r1].Cells[imarketprice].Value = Convert.ToInt32(dgvmenu.Rows[r1].Cells[isamount].Value);
                            txtqty.Text = Convert.ToString(dgvmenu.Rows[r1].Cells[iqty].Value);
                            totalcellamount(r1);

                        }

                        else
                        {
                            dgvmenu.Rows[i].Cells[iqty].Value = qty;

                            dgvmenu.Rows[i].Cells[imprice].Value = price;
                            txtqty.Text = Convert.ToString(dgvmenu.Rows[i].Cells[iqty].Value);
                            totalcellamount(i);

                        }

                    }
                    else
                    {
                        CMessageBox.Show("Avilable Stock Quanity Is " + gridSource[0].QOH, "Insufficent Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //dgvSProduct.Rows[i].Cells[isqty].Value = gridSource[0].QOH;


                    }
                dgvmenu.Enabled = true;
                redpan.Visible = false;
                dgvmenu.Focus();
                panelProductPopup.Visible = false;
                //pnlCustHead.Hide();
                //dgvItemPop.Visible = false;
                dgvmenu.Enabled = true;
                dgvmenu.Focus();
            }
            catch(Exception ex)
            {

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
          
            redpan.Visible = false;
            panelProductPopup.Visible = false;
            //pnlCustHead.Hide();
            //dgvItemPop.Visible = false;
            dgvmenu.Enabled = true;
            dgvmenu.Focus();

        }

        private void dgv_spopup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyData == Keys.Enter)
                {
                    int r1 = dgvmenu.CurrentCell.RowIndex;
                int r = dgvItemPop.CurrentCell.RowIndex;
                BllSearch<DmnMenu> bllSearch = new BllSearch<DmnMenu>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = " ItemId,Itemname ,p.SRate,p.PRate,p.MRP,i.Tax,i.TaxType,IgstPrcnt,VatPrcnt,CgstPrcnt,SgstPrcnt," +
                           " unit,ItemCode_UPC,p.Id as PrId,Fcid,ts.TaxSettingsId,quantity as QOH ";
                objSearch.Table = "  tblItem i  left join tblTaxSettings ts on i.TaxSettingsId=ts.TaxSettingsId " +
                    " left join tblProductRate p   on ProductId=i.ItemId ";
                objSearch.WhereNdGroupBy = " where  Saletype=1 and  p.Id =" + dgv_spopup.Rows[dgv_spopup.CurrentCell.RowIndex].Cells[prId].Value;
                objSearch.OrderBy = "";

                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);


                int qty = 1;
                int price = 0;
                bool flag = true;
                int i = 0;
                for (i = 0; i <= dgvmenu.RowCount - 1; i++)
                {
                    if (Convert.ToInt32(dgvmenu.Rows[i].Cells[iprid].Value) == gridSource[0].PrId)
                    {
                        flag = false;
                        qty += Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value);
                        price += Convert.ToInt32(dgvmenu.Rows[i].Cells[imprice].Value);


                        break;

                    }

                }
                if (gridSource[0].QOH >= qty)
                {
                    if (flag == true)
                    {
                        if (dgvmenu.Rows.Count - 1 == dgvmenu.CurrentCell.RowIndex)
                        {
                            dgvmenu.Rows.Add(1);
                        }

                        dgvmenu.Rows[r1].Cells[imid].Value = gridSource[0].ItemId;
                        dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = 0;
                        dgvmenu.Rows[r1].Cells[imname].Value = gridSource[0].Itemname;
                        dgvmenu.Rows[r1].Cells[imprice].Value = gridSource[0].Srate;
                        dgvmenu.Rows[r1].Cells[iqty].Value = 1;
                        //dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[imdescription].Value = gridSource[0].Description;
                        dgvmenu.Rows[r1].Cells[isamount].Value = Convert.ToDecimal(dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[iqty].Value);
                        dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Item);
                        dgvmenu.Rows[r1].Cells[iTax].Value = gridSource[0].Tax;
                        dgvmenu.Rows[r1].Cells[iTaxType].Value = gridSource[0].TaxType;
                        dgvmenu.Rows[r1].Cells[icgst].Value = gridSource[0].CgstPrcnt;
                        dgvmenu.Rows[r1].Cells[ivat].Value = gridSource[0].VatPrcnt;
                        dgvmenu.Rows[r1].Cells[imgst].Value = gridSource[0].IgstPrcnt;
                        dgvmenu.Rows[r1].Cells[isgst].Value = gridSource[0].SgstPrcnt;
                        dgvmenu.Rows[r1].Cells[iprate].Value = gridSource[0].Prate;
                        dgvmenu.Rows[r1].Cells[imdescription].Value = gridSource[0].Description;
                        dgvmenu.Rows[r1].Cells[imrp].Value = gridSource[0].MRP;
                        dgvmenu.Rows[r1].Cells[iunit].Value = gridSource[0].unit;
                        dgvmenu.Rows[r1].Cells[iitemcode].Value = gridSource[0].ItemCode_UPC;
                        dgvmenu.Rows[r1].Cells[iprid].Value = gridSource[0].PrId;
                        dgvmenu.Rows[r1].Cells[itaxsettingid].Value = gridSource[0].TaxSettingsId;
                        dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.DirectSale);
                        dgvmenu.Rows[r1].Cells[iQOH].Value = gridSource[0].QOH;

                        dgvmenu.Rows[r1].Cells[iproductType].Value = gridSource[0].Fcid;
                        dgvmenu.Rows[r1].Cells[imarketprice].Value = Convert.ToInt32(dgvmenu.Rows[r1].Cells[isamount].Value);
                        txtqty.Text = Convert.ToString(dgvmenu.Rows[r1].Cells[iqty].Value);
                        totalcellamount(r1);

                    }

                    else
                    {
                        dgvmenu.Rows[i].Cells[iqty].Value = qty;

                        dgvmenu.Rows[i].Cells[imprice].Value = price;
                        txtqty.Text = Convert.ToString(dgvmenu.Rows[i].Cells[iqty].Value);
                        totalcellamount(i);

                    }

                }
                else
                {
                    CMessageBox.Show("Avilable Stock Quanity Is " + gridSource[0].QOH, "Insufficent Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //dgvSProduct.Rows[i].Cells[isqty].Value = gridSource[0].QOH;


                }
                    dgvmenu.Enabled = true;
                    redpan.Visible = false;
                    dgvmenu.Focus();
                    panelProductPopup.Visible = false;
                    //pnlCustHead.Hide();
                    //dgvItemPop.Visible = false;
                    dgvmenu.Enabled = true;
                    dgvmenu.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public decimal OfferCheckItem(int ProductId, int ProductType)
        {
            try
            {
                int r = dgvItemPop.CurrentCell.RowIndex;
                int r1 = dgvmenu.CurrentCell.RowIndex;
                BllSearch<DmnMenu> bllSearch = new BllSearch<DmnMenu>();
                BllSearch<DmnOfferMaster> bllSearch1 = new BllSearch<DmnOfferMaster>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";

                objSearch.Table = "tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofm.Omid=ofd.Omid ";

                objSearch.WhereNdGroupBy = " where SYSDATETIME() BETWEEN OfferStartDatetime AND OfferEndDatetime and OfferType=0 and ofd.Producttype=" + ProductType + " and ofd.ProductId =" + ProductId;
                objSearch.OrderBy = "";
                var gridSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    return gridSource[0].Discountper;
                }
                else
                    return 0;
            }

            catch (Exception ex)
            {
                return 0;
            }
        }

        private void dtp_from_time_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboBtype.Focus();
            }
        }
        public void gridefllow(KeyEventArgs e)
        {
            int r = dgvforms.CurrentCell.RowIndex;
            int c = dgvforms.CurrentCell.ColumnIndex;
            if (e.KeyCode == Keys.Enter)
                {
                if (e.KeyData == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                        SendKeys.Send("{TAB}");
                }
                if(e.KeyData==Keys.Up)
                {
                    e.SuppressKeyPress = true;
                    SendKeys.Send("{UP}");
                }
                if (c == idescription)
                { 
                    if (r == dgvforms.RowCount - 1)
                    {
                        discountper.Focus();

                    }
                    else
                        dgvforms.CurrentCell = dgvforms.Rows[r + 1].Cells[itablename];
                }
               
            }
        }

        private void panelProductPopup_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
